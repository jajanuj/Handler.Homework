using ArtCommonLib;
using ArtData;
using ArtEQ._2_Function_流程_.BaseProc;
using ArtEQ._2_Function_流程_.Proc;

namespace ArtEQ._2_Function_流程_.AutoRun
{
    /// <summary>
    /// 驅動 Sort Arm 自動運轉：從 OK_Lane 逐格找 AoiResult=Ng 且還沒搬走的格子，
    /// 撿起來放到 NG_Lane 裡下一個空格。OK 判定的格子完全不動、不觸碰。
    /// 每次 Pick+Place 一格；找不到下一個 NG 格子時，代表這一輪(對應目前 OK_Lane 那盤)分料已完成，
    /// 設定 bIsSortDone，供 AR_NG_Lane 在「每盤出料」模式下讀取。
    /// </summary>
    internal class AR_Sort_Arm : clsThreadProc
    {
        #region Fields

        /// <summary>
        /// 本次分料鎖定的格位：Pick 從 OK_Lane 撿的位置、Place 放到 NG_Lane 的位置。
        /// 兩邊位置不一定相同(NG_Lane 是找空格循序塞，不是同位置對應)。
        /// </summary>
        private int m_iPickCol;

        private int m_iPickRow;
        private int m_iPlaceCol;
        private int m_iPlaceRow;

        #endregion

        #region Constructors

        #region //===================== 建構子 =====================

        public AR_Sort_Arm(string p_strLogName) : base(p_strLogName)
        {
        }

        #endregion

        #endregion

        #region Protected Methods

        #region //===================== Scenario =====================

        protected override void Scenario()
        {
            switch (iStepIndex)
            {
                #region //============== 前置作業 ==============

                case 1:
                    bIsReady = false;
                    Restart();
                    clsLog.Log(clsEnum.enuLogName.ProcessLog, strThreadLogName + ", AutoRun - Start");
                    iStepIndex = 100000;
                    break;

                #endregion

                #region //============== 閒置判別 ==============

                case 100000:
                    if (!Arm().IsProcOK()) break;

                    if (CanSort(out m_iPickCol, out m_iPickRow, out m_iPlaceCol, out m_iPlaceRow))
                    {
                        bIsSortDone = false;
                        iStepIndex = 200000;
                    }
                    else
                    {
                        MarkSortDoneIfNothingLeft();
                    }

                    break;

                #endregion

                #region //============== Pick（從 OK_Lane 撿 NG 格） ==============

                case 200000:
                    if (!Arm().IsProcOK()) break;

                    Arm().RunPick(clsEnum.PPStation.OK, m_iPickCol, m_iPickRow);
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : RunPick Col[{m_iPickCol}] Row[{m_iPickRow}]");
                    iStepIndex = 200100;
                    break;

                case 200100:
                    if (!Arm().IsProcOK()) break;

                    if (Arm().m_enuAction == BaseArm.enuAction.Pick_Done)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Pick Done → 準備 Place");
                        iStepIndex = 300000;
                    }
                    else if (Arm().m_enuAction == BaseArm.enuAction.Pick_Fail)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Pick Fail → 回閒置等處理");
                        iStepIndex = 100000;
                    }

                    break;

                #endregion

                #region //============== Place（放到 NG_Lane 的空格） ==============

                case 300000:
                    if (!Arm().IsProcOK()) break;

                    Arm().RunPlace(clsEnum.PPStation.NG, m_iPlaceCol, m_iPlaceRow);
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : RunPlace Col[{m_iPlaceCol}] Row[{m_iPlaceRow}]");
                    iStepIndex = 300100;
                    break;

                case 300100:
                    if (!Arm().IsProcOK()) break;

                    if (Arm().m_enuAction == BaseArm.enuAction.Place_Done)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Place Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (Arm().m_enuAction == BaseArm.enuAction.Place_Fail)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Place Fail → 回閒置等處理");
                        iStepIndex = 100000;
                    }

                    break;

                #endregion

                default:
                    iStepIndex = -1;
                    Stop();
                    bIsProcessing = false;
                    break;
            }
        }

        #endregion

        #endregion

        #region //===================== 全域變數 =====================

        public bool bIsReady { get; protected set; }

        /// <summary>
        /// 本輪(對應目前 OK_Lane 那盤)NG 分料是否已經做完。
        /// 這裡自己在偵測到「OK_Lane 沒有殘留未搬的 NG 格子」時設成 true；
        /// 下一輪重新開始分料(偵測到 OK_Lane 出現新的未搬 NG 格子)時，自己清回 false。
        /// AR_NG_Lane 只讀，不負責清除。
        /// </summary>
        public bool bIsSortDone { get; private set; }

        #endregion

        #region //===================== Singleton =====================

        private static AR_Sort_Arm m_Singleton;
        private static object m_objLock = new object();

        public static AR_Sort_Arm GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new AR_Sort_Arm("AR_Sort_Arm");
                    }
                }
            }

            return m_Singleton;
        }

        #endregion

        #region //===================== Private Helper =====================

        /// <summary>
        /// 分料手臂
        /// </summary>
        private Proc_Sort_Arm Arm() => Proc_Sort_Arm.GetSingleton();

        /// <summary>
        /// 撿料來源(OK流道，NG 判定的格子從這裡撿走)
        /// </summary>
        private Proc_OK_Lane OK_Lane() => Proc_OK_Lane.GetSingleton();

        /// <summary>
        /// 放料目的(NG流道)
        /// </summary>
        private Proc_NG_Lane NG_Lane() => Proc_NG_Lane.GetSingleton();

        #endregion

        #region //===================== 條件判斷 =====================

        /// <summary>
        /// 在 OK_Lane 裡找下一個「有料、AoiResult=NG、還沒搬走」的格子。
        /// OK 判定的格子完全跳過，不動它。
        /// </summary>
        private bool FindNextNGCell(out int r_iCol, out int r_iRow)
        {
            r_iCol = -1;
            r_iRow = -1;

            var tray = OK_Lane().m_Temp_Tray_Info;
            if (tray == null)
                return false;

            int iCount = tray.iRows * tray.iCols;

            for (int i = 0; i < iCount; i++)
            {
                var record = tray.AssyRecords[i];
                if (record.IsExist && record.AoiResult == clsEnum.AoiResult.Ng)
                {
                    if (tray.GetRowColFromIndex(i, out r_iRow, out r_iCol))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 在 NG_Lane 裡找下一個空格(循序塞，不是跟 OK_Lane 同位置對應)。
        /// </summary>
        private bool FindNextEmptyNGLaneCell(out int r_iCol, out int r_iRow)
        {
            r_iCol = -1;
            r_iRow = -1;

            var tray = NG_Lane().m_Temp_Tray_Info;
            if (tray == null)
                return false;

            int iCount = tray.iRows * tray.iCols;

            for (int i = 0; i < iCount; i++)
            {
                if (!tray.AssyRecords[i].IsExist)
                {
                    if (tray.GetRowColFromIndex(i, out r_iRow, out r_iCol))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 確認可以執行一次分料(Pick + Place)
        /// 條件：兩邊流道都有帳、到位訊號都在、OK_Lane 找得到待搬的 NG 格、NG_Lane 找得到空格
        /// </summary>
        private bool CanSort(out int r_iPickCol, out int r_iPickRow, out int r_iPlaceCol, out int r_iPlaceRow)
        {
            r_iPickCol = -1;
            r_iPickRow = -1;
            r_iPlaceCol = -1;
            r_iPlaceRow = -1;

            bool rValue = true;

            // 1. OK_Lane 帳料 + 到位訊號確認
            rValue &= OK_Lane().m_Temp_Tray_Info.bIsExist;
            rValue &= OK_Lane().ArrivalSignal;

            // 2. NG_Lane 帳料 + 到位訊號確認(要有一個空 Tray 在等著收)
            rValue &= NG_Lane().m_Temp_Tray_Info.bIsExist;
            rValue &= NG_Lane().ArrivalSignal;

            if (!rValue)
                return false;

            // 3. 找得到待搬的 NG 格 + NG_Lane 找得到空格
            if (!FindNextNGCell(out r_iPickCol, out r_iPickRow))
                return false;

            return FindNextEmptyNGLaneCell(out r_iPlaceCol, out r_iPlaceRow);
        }

        /// <summary>
        /// 閒置判別時，如果找不到待搬的 NG 格了，代表這一輪分料做完了，設定 bIsSortDone。
        /// 只看「OK_Lane 還有沒有 NG 沒搬完」，不管 NG_Lane 有沒有空格——沒空格時 CanSort() 會失敗，
        /// 但那是「卡住等 NG_Lane 出料」，不是「這輪分完了」，不能誤設成 true。
        ///
        /// 注意：這裡刻意不判斷「OK_Lane 現在有沒有帳(bIsExist)」。原本有一個
        /// `if (!OK_Lane().bIsExist) return;` 的提早跳出，用意是「OK_Lane 還沒收到任何 Tray 時
        /// 不要誤判成做完」，但這個判斷會踩到一個競態：Sort_Arm 搬完最後一顆 NG 之後，回到這裡
        /// 檢查的時間點如果晚於 OK_Lane 自己完成 Unload(帳已經被清掉、bIsExist 變 false)，
        /// 這個提早跳出就會讓 bIsSortDone 永遠設不成 true——正常運轉時下一輪 OK_Lane 進料還能
        /// 讓 CanSort() 重新抓到新的 NG、間接把這個問題蓋過去，但結批的最後一輪沒有下一輪可以
        /// 蓋過去，會永久卡住(NG_Lane 最後一盤出不去，實測發生過)。拿掉這個判斷後，
        /// FindNextNGCell() 掃到「沒有 Tray」或「Tray 剛被清空」都會直接回傳 false，
        /// 效果等同「沒有 NG 待搬」，邏輯上是安全的，不需要另外判斷 bIsExist。
        /// </summary>
        private void MarkSortDoneIfNothingLeft()
        {
            int dummyCol, dummyRow;
            if (FindNextNGCell(out dummyCol, out dummyRow))
                return;

            bIsSortDone = true;
        }

        #endregion

        #region //===================== Public 函式 =====================

        public bool IsProcOK() => !bIsProcessing && bIsReady;

        public void RunInitial()
        {
            iStepIndex = -1;
            bIsReady = true;
            bIsSortDone = false;
            clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), strThreadLogName + " : RunInitial Done");
        }

        public void Run_AutoRun()
        {
            if (IsProcOK() && clsCmData.g_bIsinitialized)
            {
                iStepIndex = 1;
                bIsProcessing = true;
            }
        }

        #endregion
    }
}
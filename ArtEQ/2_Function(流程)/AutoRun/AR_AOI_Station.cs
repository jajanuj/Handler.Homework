using ArtCommonLib;
using ArtControlLib;
using ArtEQ._2_Function_流程_.BaseProc;
using ArtEQ._2_Function_流程_.Proc;
using static ArtData.clsEnum;

namespace ArtEQ._2_Function_流程_.AutoRun
{
    /// <summary>
    /// 驅動 AOI Station 自動運轉：AOI Lane 上還有格子沒檢測完成時，逐格呼叫 RunInspect(col, row)。
    /// 跟 Press Station(一次整盤處理完)不同，AOI 是逐格移動鏡頭檢測，一次只能處理一格。
    /// </summary>
    internal class AR_AOI_Station : clsThreadProc
    {
        #region Constructors

        #region //===================== 建構子 =====================

        public AR_AOI_Station(string p_strLogName) : base(p_strLogName)
        {
        }

        #endregion

        #endregion

        #region Properties

        #region //===================== 全域變數 =====================

        public bool bIsReady { get; protected set; }

        /// <summary>
        /// Recipe是否啟用檢測站
        /// </summary>
        bool EnableAoiStation => ucParameter.GetValueBool(enuPmtName.Rec_Enable_Aoi);

        #endregion

        #endregion

        #region Fields

        /// <summary>
        /// 本次檢測鎖定的格子 Row/Column。
        /// </summary>
        private int m_iCol;
        private int m_iRow;

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
                    clsLog.Log(enuLogName.ProcessLog, strThreadLogName + ", AutoRun - Start");
                    iStepIndex = 100000;
                    break;

                #endregion

                #region //============== 閒置判別 ==============

                case 100000:
                    if (!Station().IsProcOK()) break;

                    if (!EnableAoiStation)
                    {
                        break;
                    }

                    if (CanInspect(out m_iCol, out m_iRow))
                    {
                        iStepIndex = 200000;
                    }

                    break;

                #endregion

                #region //============== 檢測 ==============

                case 200000:
                    if (!Station().IsProcOK()) break;

                    Station().RunInspect(m_iCol, m_iRow);
                    clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : RunInspect Col[{m_iCol}] Row[{m_iRow}]");
                    iStepIndex = 200100;
                    break;

                case 200100:
                    if (!Station().IsProcOK()) break;

                    if (Station().m_enuAction == BaseAoiStation.enuAction.AOI_Done)
                    {
                        clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : AOI Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (Station().m_enuAction == BaseAoiStation.enuAction.AOI_Fail)
                    {
                        clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : AOI Fail → 回閒置等處理");
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

        #region //===================== Singleton =====================

        private static AR_AOI_Station m_Singleton;
        private static object m_objLock = new object();

        public static AR_AOI_Station GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new AR_AOI_Station("AR_AOI_Station");
                    }
                }
            }

            return m_Singleton;
        }

        #endregion

        #region //===================== Private Helper =====================

        private Proc_AOI_Station Station() => Proc_AOI_Station.GetSingleton();
        private Proc_AOI_Lane AOI_Lane() => Proc_AOI_Lane.GetSingleton();

        #endregion

        #region //===================== 條件判斷 =====================

        /// <summary>
        /// 找出下一個要檢測的格位：有料但還沒檢測過的格子。
        /// </summary>
        private bool FindNextInspectCell(out int r_iCol, out int r_iRow)
        {
            r_iCol = -1;
            r_iRow = -1;

            var tray = AOI_Lane().m_Temp_Tray_Info;
            if (tray == null)
                return false;

            int iCount = tray.iRows * tray.iCols;

            for (int i = 0; i < iCount; i++)
            {
                var record = tray.AssyRecords[i];
                if (record.IsExist && !record.IsAoiInspected)
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
        /// 確認可以執行檢測
        /// 條件：AOI Lane 有帳、到位、Station 就緒、且盤子裡還有格子有料但尚未檢測
        /// </summary>
        private bool CanInspect(out int r_iCol, out int r_iRow)
        {
            r_iCol = -1;
            r_iRow = -1;

            bool rValue = true;

            // 1. AOI Lane 帳料 + 到位訊號確認
            rValue &= AOI_Lane().m_Temp_Tray_Info.bIsExist;
            rValue &= AOI_Lane().ArrivalSignal;
            rValue &= AOI_Lane().IsProcOK();

            // 2. Station 流程就緒
            rValue &= Station().IsProcOK();

            // 3. Station 狀態確認
            rValue &= Station().m_enuAction == BaseAoiStation.enuAction.Initial_Done ||
                      Station().m_enuAction == BaseAoiStation.enuAction.AOI_Done ||
                      Station().m_enuAction == BaseAoiStation.enuAction.AOI_Fail;

            if (!rValue)
                return false;

            // 4. 找得到還沒檢測的格位
            return FindNextInspectCell(out r_iCol, out r_iRow);
        }

        #endregion

        #region //===================== Public 函式 =====================

        public bool IsProcOK() => !bIsProcessing && bIsReady;

        public void RunInitial()
        {
            iStepIndex = -1;
            bIsReady = true;
            clsLog.Log(nameof(enuLogName.ProcessLog), strThreadLogName + " : RunInitial Done");
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

using ArtCommonLib;
using ArtData;
using ArtEQ._2_Function_流程_.BaseProc;
using ArtEQ._2_Function_流程_.Proc;

namespace ArtEQ._2_Function_流程_.AutoRun
{
    /// <summary>
    /// 驅動 ASM Arm 自動運轉：從 HS Lane 撿散熱片，貼合到 ASM Lane 上已定位的 IC 帳料。
    /// </summary>
    internal class AR_ASM_Arm : clsThreadProc
    {
        #region Constructors

        #region //===================== 建構子 =====================

        public AR_ASM_Arm(string p_strLogName) : base(p_strLogName)
        {
        }

        #endregion

        #endregion

        #region Properties

        #region //===================== 全域變數 =====================

        public bool bIsReady { get; protected set; }

        #endregion

        #endregion

        #region Fields

        /// <summary>
        /// 本次組裝作業鎖定的格子 Row/Column（Pick 與 Place 共用同一個格位）。
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
                    clsLog.Log(clsEnum.enuLogName.ProcessLog, strThreadLogName + ", AutoRun - Start");
                    iStepIndex = 100000;
                    break;

                #endregion

                #region //============== 閒置判別 ==============

                case 100000:
                    if (!Arm().IsProcOK()) break;

                    if (CanAssemble(out m_iCol, out m_iRow))
                    {
                        iStepIndex = 200000;
                    }

                    break;

                #endregion

                #region //============== Pick（從 HS Lane 撿散熱片） ==============

                case 200000:
                    if (!Arm().IsProcOK()) break;

                    Arm().RunPick(clsEnum.PPStation.HeatSink, m_iCol, m_iRow);
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : RunPick Col[{m_iCol}] Row[{m_iRow}]");
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

                #region //============== Place（貼合到 ASM Lane 的 IC 上） ==============

                case 300000:
                    if (!Arm().IsProcOK()) break;

                    Arm().RunPlace(clsEnum.PPStation.IC, m_iCol, m_iRow);
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : RunPlace Col[{m_iCol}] Row[{m_iRow}]");
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

        #region //===================== Singleton =====================

        private static AR_ASM_Arm m_Singleton;
        private static object m_objLock = new object();

        public static AR_ASM_Arm GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new AR_ASM_Arm("AR_ASM_Arm");
                    }
                }
            }

            return m_Singleton;
        }

        #endregion

        #region //===================== Private Helper =====================

        /// <summary>
        /// 組裝手臂
        /// </summary>
        private Proc_ASM_Arm Arm() => Proc_ASM_Arm.GetSingleton();

        /// <summary>
        /// 撿料來源(HS流道)
        /// </summary>
        private Proc_HS_Lane HS_Lane() => Proc_HS_Lane.GetSingleton();

        /// <summary>
        /// 放料目的(ASM流道)
        /// </summary>
        private Proc_ASM_Lane ASM_Lane() => Proc_ASM_Lane.GetSingleton();

        #endregion

        #region //===================== 條件判斷 =====================

        /// <summary>
        /// 找出下一個可以組裝的格位：HS Lane 該格有散熱片、ASM Lane 同一格有 IC 帳料且尚未組裝。
        /// 兩邊 Tray 用同一套 Row/Col 座標對應（格位對格位），找到就回傳該格的 Row/Col。
        /// </summary>
        private bool FindNextAssemblyCell(out int r_iCol, out int r_iRow)
        {
            r_iCol = -1;
            r_iRow = -1;

            var hsTray = HS_Lane().m_Temp_Tray_Info;
            var asmTray = ASM_Lane().m_Temp_Tray_Info;

            if (hsTray == null || asmTray == null)
                return false;

            int iCount = System.Math.Min(hsTray.iRows * hsTray.iCols, asmTray.iRows * asmTray.iCols);

            for (int i = 0; i < iCount; i++)
            {
                bool bHsReady = hsTray.GetItemStatus(i) == clsEnum.TrayItemStatus.HeatSink;
                bool bAsmNeedsAssembly = asmTray.GetItemStatus(i) == clsEnum.TrayItemStatus.Substrate;

                if (bHsReady && bAsmNeedsAssembly)
                {
                    if (asmTray.GetRowColFromIndex(i, out r_iRow, out r_iCol))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 確認可以執行一次組裝動作(Pick + Place)
        /// 條件：兩邊流道都有帳、Arrival訊號都在、且找得到可組裝的格位
        /// </summary>
        private bool CanAssemble(out int r_iCol, out int r_iRow)
        {
            r_iCol = -1;
            r_iRow = -1;

            bool rValue = true;

            // 1. HS Lane 帳料 + 到位訊號確認
            rValue &= HS_Lane().m_Temp_Tray_Info.bIsExist;
            rValue &= HS_Lane().ArrivalSignal;

            // 2. ASM Lane 帳料 + 到位訊號確認
            rValue &= ASM_Lane().m_Temp_Tray_Info.bIsExist;
            rValue &= ASM_Lane().ArrivalSignal;

            if (!rValue)
                return false;

            // 3. 找得到可組裝的格位
            return FindNextAssemblyCell(out r_iCol, out r_iRow);
        }

        #endregion

        #region //===================== Public 函式 =====================

        public bool IsProcOK() => !bIsProcessing && bIsReady;

        public void RunInitial()
        {
            iStepIndex = -1;
            bIsReady = true;
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

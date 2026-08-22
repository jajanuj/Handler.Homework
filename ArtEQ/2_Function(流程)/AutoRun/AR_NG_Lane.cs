using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtEQ._2_Function_流程_.Proc;
using System.Linq;

namespace ArtEQ._2_Function_流程_.AutoRun
{
    internal class AR_NG_Lane : clsThreadProc
    {
        #region Constructors

        #region //===================== 建構子 =====================

        public AR_NG_Lane(string p_strLogName) : base(p_strLogName)
        {
        }

        #endregion

        #endregion

        #region Properties

        #region //===================== 全域變數 =====================

        public bool bIsReady { get; protected set; }

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
                    if (!NG_Lane().IsProcOK()) break;

                    if (CanLoad())
                    {
                        iStepIndex = 200000;
                    }
                    else if (CanUnload())
                    {
                        iStepIndex = 300000;
                    }

                    break;

                #endregion

                #region //============== Load（等 NG Feed Magazine 推料） ==============

                case 200000:

                    #region 可否執行

                    if (!NG_Lane().IsProcOK()) break;

                    if (CanLoad())
                    {
                        iStepIndex = 200100;
                    }

                    #endregion

                    break;

                case 200100:

                    #region 執行

                    if (!NG_Lane().IsProcOK()) break;

                    NG_Lane().RunLoad();
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : RunLoad");
                    iStepIndex = 200200;

                    #endregion

                    break;

                case 200200:

                    #region 執行結果

                    if (!NG_Lane().IsProcOK()) break;

                    if (NG_Lane().m_enuAction == BaseLane.enuAction.Load_Done)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Load Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (NG_Lane().m_enuAction == BaseLane.enuAction.Load_Fail)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Load Fail → 回閒置等處理");
                        iStepIndex = 100000;
                    }

                    #endregion

                    break;

                #endregion

                #region //============== Unload (卸載至Magazine) ==============

                case 300000:

                    #region 可否執行

                    if (!NG_Lane().IsProcOK()) break;
                    if (CanUnload())
                    {
                        iStepIndex = 300100;
                    }

                    #endregion

                    break;

                case 300100:

                    #region 執行

                    NG_Lane().RunUnload();
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : RunUnload");
                    iStepIndex = 300200;

                    #endregion

                    break;

                case 300200:

                    #region 執行結果

                    if (NG_Lane().m_enuAction == BaseLane.enuAction.Unload_Done)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Unload Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (NG_Lane().m_enuAction == BaseLane.enuAction.Unload_Fail)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Unload Fail → 回閒置等處理");
                        iStepIndex = 100000;
                    }

                    #endregion

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

        private static AR_NG_Lane m_Singleton;
        private static object m_objLock = new object();

        public static AR_NG_Lane GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new AR_NG_Lane("AR_NG_Lane");
                    }
                }
            }

            return m_Singleton;
        }

        #endregion

        #region //===================== Private Helper =====================

        /// <summary>
        /// 本站(NG流道)
        /// </summary>
        /// <returns></returns>
        private Proc_NG_Lane NG_Lane() => Proc_NG_Lane.GetSingleton();

        /// <summary>
        /// 上游料盒(NG入料)
        /// </summary>
        /// <returns></returns>
        private Proc_NG_Feed_Magazine Mag_NG_Feed() => Proc_NG_Feed_Magazine.GetSingleton();

        /// <summary>
        /// 下游料盒(NG收料)
        /// </summary>
        /// <returns></returns>
        private Proc_NG_Discharge_Magazine Mag_NG_Discharge() => Proc_NG_Discharge_Magazine.GetSingleton();

        /// <summary>
        /// OK流道(即時出料模式要看它這輪分完了沒)
        /// </summary>
        private Proc_OK_Lane OK_Lane() => Proc_OK_Lane.GetSingleton();

        #endregion

        #region //===================== 條件判斷 =====================

        /// <summary>
        /// Step2: 確認可以載入
        /// 條件：Load位置無帳料 + Magazine已推出帳料
        /// </summary>
        private bool CanLoad()
        {
            bool rValue = true;

            // 1. 上游Magazine狀態確認
            rValue &= Mag_NG_Feed().m_enuAction == BaseMagazine.enuAction.Initial_Done ||
                      Mag_NG_Feed().m_enuAction == BaseMagazine.enuAction.Magazine_Load_Waiting;

            //2. Lane料帳確認
            rValue &= !NG_Lane().m_Temp_Tray_Info.bIsExist;

            //3. Lane是否流程就緒
            rValue &= NG_Lane().IsProcOK();

            //4. Lane狀態確認
            rValue &= NG_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      NG_Lane().m_enuAction == BaseLane.enuAction.Unload_Done;

            return rValue;
        }

        /// <summary>
        /// Step5: 確認可以卸載
        /// 條件：Load位置有帳料 + 下游 NG Discharge Magazine 準備收料
        /// </summary>
        private bool CanUnload()
        {
            bool rValue = true;

            // 1. 下游狀態確認
            rValue &= Mag_NG_Discharge().m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Waiting ||
                      Mag_NG_Discharge().m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Done ||
                      Mag_NG_Discharge().m_enuAction == BaseMagazine.enuAction.Initial_Done;

            //2. Lane料帳確認
            rValue &= NG_Lane().m_Temp_Tray_Info.bIsExist;

            //3. Lane是否流程就緒
            rValue &= NG_Lane().IsProcOK();

            //4. Lane狀態確認
            rValue &= NG_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      NG_Lane().m_enuAction == BaseLane.enuAction.Load_Done;

            if (!rValue)
                return false;

            //5. 依 NG 出料模式，判斷這盤 NG 夠不夠出料
            switch (GetNGDischargeMode())
            {
                case clsEnum.NGDischargeMode.PerCycle:
                    // 這一輪(對應目前 OK_Lane 那盤)分料已經做完，而且這輪真的有收到東西才出，
                    // 避免 OK_Lane 那盤剛好整盤都是 OK、NG_Lane 這輪沒收到任何一顆卻白跑一次出料。
                    rValue &= AR_Sort_Arm.GetSingleton().bIsSortDone;
                    rValue &= NG_Lane().m_Temp_Tray_Info.AssyRecords.Any(v => v.IsExist);
                    break;

                case clsEnum.NGDischargeMode.FullTray:
                    // NG_Lane 自己收滿整盤(每一格都有帳)才出料
                    rValue &= NG_Lane().m_Temp_Tray_Info.AssyRecords.All(v => v.IsExist);
                    break;

                default:
                    rValue = false;
                    break;
            }

            return rValue;
        }

        /// <summary>
        /// 讀取分料模式
        /// </summary>
        private clsEnum.NGDischargeMode GetNGDischargeMode()
        {
            return (clsEnum.NGDischargeMode)ucParameter.GetValueInt(clsEnum.enuPmtName.Rec_Sort_Type);
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
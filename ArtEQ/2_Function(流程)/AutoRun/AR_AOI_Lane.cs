using System.Linq;
using ArtCommonLib;
using ArtControlLib;
using ArtEQ._2_Function_流程_.Proc;
using static ArtData.clsEnum;

namespace ArtEQ._2_Function_流程_.AutoRun
{
    internal class AR_AOI_Lane : clsThreadProc
    {
        #region Constructors

        #region //===================== 建構子 =====================

        public AR_AOI_Lane(string p_strLogName) : base(p_strLogName)
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
                    if (!AOI_Lane().IsProcOK()) break;

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

                #region //============== Load（等 Press Lane 出料） ==============

                case 200000:

                    #region 可否執行

                    if (!AOI_Lane().IsProcOK()) break;

                    if (CanLoad())
                    {
                        iStepIndex = 200100;
                    }

                    #endregion

                    break;

                case 200100:

                    #region 執行

                    if (!AOI_Lane().IsProcOK()) break;

                    AOI_Lane().RunLoad();
                    clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : RunLoad");
                    iStepIndex = 200200;

                    #endregion

                    break;

                case 200200:

                    #region 執行結果

                    if (!AOI_Lane().IsProcOK()) break;

                    if (AOI_Lane().m_enuAction == BaseLane.enuAction.Load_Done)
                    {
                        clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : Load Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (AOI_Lane().m_enuAction == BaseLane.enuAction.Load_Fail)
                    {
                        clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : Load Fail → 回閒置等處理");
                        iStepIndex = 100000;
                    }

                    #endregion

                    break;

                #endregion

                #region //============== Unload (檢測完成，卸載至 OK Lane) ==============

                case 300000:

                    #region 可否執行

                    if (!AOI_Lane().IsProcOK()) break;
                    if (CanUnload())
                    {
                        iStepIndex = 300100;
                    }

                    #endregion

                    break;

                case 300100:

                    #region 執行

                    AOI_Lane().RunUnload();
                    clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : RunUnload");
                    iStepIndex = 300200;

                    #endregion

                    break;

                case 300200:

                    #region 執行結果

                    if (AOI_Lane().m_enuAction == BaseLane.enuAction.Unload_Done)
                    {
                        clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : Unload Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (AOI_Lane().m_enuAction == BaseLane.enuAction.Unload_Fail)
                    {
                        clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : Unload Fail → 回閒置等處理");
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

        private static AR_AOI_Lane m_Singleton;
        private static object m_objLock = new object();

        public static AR_AOI_Lane GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new AR_AOI_Lane("AR_AOI_Lane");
                    }
                }
            }

            return m_Singleton;
        }

        #endregion

        #region //===================== Private Helper =====================

        /// <summary>
        /// 檢測站
        /// </summary>
        private Proc_AOI_Station AOI_Station() => Proc_AOI_Station.GetSingleton();

        /// <summary>
        /// 本站(AOI流道)
        /// </summary>
        /// <returns></returns>
        private Proc_AOI_Lane AOI_Lane() => Proc_AOI_Lane.GetSingleton();

        /// <summary>
        /// 上游流道(壓合)
        /// </summary>
        /// <returns></returns>
        private Proc_Press_Lane Press_Lane() => Proc_Press_Lane.GetSingleton();

        /// <summary>
        /// 下游流道(OK)
        /// </summary>
        /// <returns></returns>
        private Proc_OK_Lane OK_Lane() => Proc_OK_Lane.GetSingleton();

        #endregion

        #region //===================== 條件判斷 =====================

        /// <summary>
        /// 確認可以載入
        /// 條件：Load位置無帳料 + 上游 Press Lane 已經出料等待
        /// </summary>
        private bool CanLoad()
        {
            bool rValue = true;

            // 1. 上游 Press Lane 狀態確認
            rValue &= Press_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      Press_Lane().m_enuAction == BaseLane.enuAction.Unload_Waiting;

            //2. Lane料帳確認
            rValue &= !AOI_Lane().m_Temp_Tray_Info.bIsExist;

            //3. Lane是否流程就緒
            rValue &= AOI_Lane().IsProcOK();

            //4. Lane狀態確認
            rValue &= AOI_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      AOI_Lane().m_enuAction == BaseLane.enuAction.Unload_Done;

            return rValue;
        }

        /// <summary>
        /// 確認可以卸載
        /// 條件：Load位置有帳料 + 每個有料的格子都已經檢測完成 + 下游 OK Lane 準備收料
        /// </summary>
        private bool CanUnload()
        {
            bool rValue = true;

            // 1. 下游 OK Lane 狀態確認
            rValue &= OK_Lane().m_enuAction == BaseLane.enuAction.Load_Waiting ||
                      OK_Lane().m_enuAction == BaseLane.enuAction.Unload_Done ||
                      OK_Lane().m_enuAction == BaseLane.enuAction.Initial_Done;

            //2. Lane料帳確認
            rValue &= AOI_Lane().m_Temp_Tray_Info.bIsExist;

            //3. Lane是否流程就緒
            rValue &= AOI_Lane().IsProcOK();

            //4. Lane狀態確認
            rValue &= AOI_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      AOI_Lane().m_enuAction == BaseLane.enuAction.Load_Done;

            //5. 判斷盤子裡有料的格子是否都已經檢測完成或不啟用檢測站，避免未檢測的半成品被卸走
            rValue &= !AOI_Lane().m_Temp_Tray_Info.AssyRecords.Any(v => v.IsExist && !v.IsAoiInspected) || !EnableAoiStation;

            rValue &= AOI_Station().IsProcOK();

            return rValue;
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

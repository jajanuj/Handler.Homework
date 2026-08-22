using ArtCommonLib;
using ArtData;
using ArtEQ._2_Function_流程_.Proc;
using System.Linq;

namespace ArtEQ._2_Function_流程_.AutoRun
{
    internal class AR_ASM_Lane : clsThreadProc
    {
        #region Constructors

        #region //===================== 建構子 =====================

        public AR_ASM_Lane(string p_strLogName) : base(p_strLogName)
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
                    if (!ASM_Lane().IsProcOK()) break;

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

                #region //============== Load（等 IC Magazine 推料） ==============

                case 200000:

                    #region 可否執行

                    if (!ASM_Lane().IsProcOK()) break;

                    if (CanLoad())
                    {
                        iStepIndex = 200100;
                    }

                    #endregion

                    break;

                case 200100:

                    #region 執行

                    if (!ASM_Lane().IsProcOK()) break;

                    ASM_Lane().RunLoad();
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : RunLoad");
                    iStepIndex = 200200;

                    #endregion

                    break;

                case 200200:

                    #region 執行結果

                    if (!ASM_Lane().IsProcOK()) break;

                    if (ASM_Lane().m_enuAction == BaseLane.enuAction.Load_Done)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Load Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (ASM_Lane().m_enuAction == BaseLane.enuAction.Load_Fail)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Load Fail → 回閒置等處理");
                        iStepIndex = 100000;
                    }

                    #endregion

                    break;

                #endregion

                #region //============== Unload (組裝完成，卸載至 Press Lane) ==============

                case 300000:

                    #region 可否執行

                    if (!ASM_Lane().IsProcOK()) break;
                    if (CanUnload())
                    {
                        iStepIndex = 300100;
                    }

                    #endregion

                    break;

                case 300100:

                    #region 執行

                    ASM_Lane().RunUnload();
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : RunUnload");
                    iStepIndex = 300200;

                    #endregion

                    break;

                case 300200:

                    #region 執行結果

                    if (ASM_Lane().m_enuAction == BaseLane.enuAction.Unload_Done)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Unload Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (ASM_Lane().m_enuAction == BaseLane.enuAction.Unload_Fail)
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

        private static AR_ASM_Lane m_Singleton;
        private static object m_objLock = new object();

        public static AR_ASM_Lane GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new AR_ASM_Lane("AR_ASM_Lane");
                    }
                }
            }

            return m_Singleton;
        }

        #endregion

        #region //===================== Private Helper =====================

        /// <summary>
        /// 本站(ASM流道)
        /// </summary>
        /// <returns></returns>
        private Proc_ASM_Lane ASM_Lane() => Proc_ASM_Lane.GetSingleton();

        /// <summary>
        /// 上游料盒(IC入料)
        /// </summary>
        /// <returns></returns>
        private Proc_IC_Feed_Magazine Mag_IC_Feed() => Proc_IC_Feed_Magazine.GetSingleton();

        /// <summary>
        /// 下游流道(壓合)
        /// </summary>
        /// <returns></returns>
        private Proc_Press_Lane Press_Lane() => Proc_Press_Lane.GetSingleton();

        #endregion

        #region //===================== 條件判斷 =====================

        /// <summary>
        /// 確認可以載入
        /// 條件：Load位置無帳料 + IC Magazine已推出帳料
        /// </summary>
        private bool CanLoad()
        {
            bool rValue = true;

            // 1. 上游Magazine狀態確認
            rValue &= Mag_IC_Feed().m_enuAction == BaseMagazine.enuAction.Initial_Done ||
                      Mag_IC_Feed().m_enuAction == BaseMagazine.enuAction.Magazine_Load_Waiting;

            //2. Lane料帳確認
            rValue &= !ASM_Lane().m_Temp_Tray_Info.bIsExist;

            //3. Lane是否流程就緒
            rValue &= ASM_Lane().IsProcOK();

            //4. Lane狀態確認
            rValue &= ASM_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      ASM_Lane().m_enuAction == BaseLane.enuAction.Unload_Done;

            return rValue;
        }

        /// <summary>
        /// 確認可以卸載
        /// 條件：Load位置有帳料 + 每個有料的格子都已完成組裝(貼合散熱片) + 下游 Press Lane 準備收料
        /// </summary>
        private bool CanUnload()
        {
            bool rValue = true;

            // 1. 下游 Press Lane 狀態確認
            rValue &= Press_Lane().m_enuAction == BaseLane.enuAction.Load_Waiting ||
                      Press_Lane().m_enuAction == BaseLane.enuAction.Unload_Done ||
                      Press_Lane().m_enuAction == BaseLane.enuAction.Initial_Done;

            //2. Lane料帳確認
            rValue &= ASM_Lane().m_Temp_Tray_Info.bIsExist;

            //3. Lane是否流程就緒
            rValue &= ASM_Lane().IsProcOK();

            //4. Lane狀態確認
            rValue &= ASM_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      ASM_Lane().m_enuAction == BaseLane.enuAction.Load_Done;

            //5. 判斷盤子裡有料的格子是否都已經組裝完成(貼上散熱片)，避免半成品被卸走
            rValue &= !ASM_Lane().m_Temp_Tray_Info.AssyRecords.Any(v => v.IsExist && !v.IsAssembled);

            return rValue;
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

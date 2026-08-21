using System.Linq;
using ArtCommonLib;
using ArtData;
using ArtEQ._2_Function_流程_.Proc;

namespace ArtEQ._2_Function_流程_.AutoRun
{
    internal class AR_HS_Lane : clsThreadProc
    {
        #region //===================== Singleton =====================
        private static AR_HS_Lane m_Singleton;
        private static object m_objLock = new object();
        public static AR_HS_Lane GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new AR_HS_Lane("AR_HS_Lane");
                    }
                }
            }
            return m_Singleton;
        }
        #endregion

        #region //===================== 全域變數 =====================
        public bool bIsReady { get; protected set; }
        #endregion

        #region //===================== 建構子 =====================
        public AR_HS_Lane(string p_strLogName) : base(p_strLogName) { }
        #endregion

        #region //===================== Private Helper =====================
        private Proc_HS_Lane HS_Lane() => Proc_HS_Lane.GetSingleton();
        private Proc_HS_Discharge_Magazine Mag_HS_Discharge() => Proc_HS_Discharge_Magazine.GetSingleton();
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
            rValue &= Mag_HS_Discharge().m_enuAction == BaseMagazine.enuAction.Initial_Done ||
                      Mag_HS_Discharge().m_enuAction == BaseMagazine.enuAction.Magazine_Load_Done;

            //2. Lane料帳確認
            rValue &= !HS_Lane().m_Temp_Tray_Info.bIsExist;

            //3. Lane是否流程就緒
            rValue &= HS_Lane().IsProcOK();

            //4. Lane狀態確認
            rValue &= HS_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      HS_Lane().m_enuAction == BaseLane.enuAction.Unload_Done;

            return rValue;
        }

        /// <summary>
        /// Step5: 確認可以卸載
        /// 條件：Load位置有帳料 + Magazine無帳料
        /// </summary>
        private bool CanUnload()
        {
            bool rValue = true;

            // 1. 下游狀態確認
            rValue &= Mag_HS_Discharge().m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Waiting ||
                      Mag_HS_Discharge().m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Done ||
                      Mag_HS_Discharge().m_enuAction == BaseMagazine.enuAction.Initial_Done;

            //2. Lane料帳確認
            rValue &= HS_Lane().m_Temp_Tray_Info.bIsExist;

            //3. Lane是否流程就緒
            rValue &= HS_Lane().IsProcOK();

            //4. Lane狀態確認
            rValue &= HS_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      HS_Lane().m_enuAction == BaseLane.enuAction.Load_Done;

            //5. 判斷盤子是否都OK品
            rValue &= !HS_Lane().m_Temp_Tray_Info.AssyRecords.Any(v => v.IsExist == true);

            return rValue;
        }
        #endregion

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
                    if (!HS_Lane().IsProcOK()) break;

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

                #region //============== Load（等 Magazine 推料） ==============
                case 200000:
                    #region 可否執行
                    if (!HS_Lane().IsProcOK()) break;

                    if (CanLoad())
                    {
                        iStepIndex = 200100;
                    }
                    #endregion
                    break;

                case 200100:
                    #region 執行
                    if (!HS_Lane().IsProcOK()) break;

                    HS_Lane().RunLoad();
                    clsLog.Log(clsEnum.enuLogName.ProcessLog.ToString(), $"{strThreadLogName} : RunLoad");
                    iStepIndex = 200200;
                    #endregion
                    break;

                case 200200:
                    #region 執行結果
                    if (!HS_Lane().IsProcOK()) break;

                    if (HS_Lane().m_enuAction == BaseLane.enuAction.Load_Done)
                    {
                        clsLog.Log(clsEnum.enuLogName.ProcessLog.ToString(), $"{strThreadLogName} : Load Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (HS_Lane().m_enuAction == BaseLane.enuAction.Load_Fail)
                    {
                        clsLog.Log(clsEnum.enuLogName.ProcessLog.ToString(), $"{strThreadLogName} : Load Fail → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    #endregion
                    break;
                #endregion

                #region //============== Unload (卸載至Magazine) ==============
                case 300000:
                    #region 可否執行
                    if (!HS_Lane().IsProcOK()) break;
                    if (CanUnload())
                    {
                        iStepIndex = 300100;
                    }
                    #endregion
                    break;

                case 300100:
                    #region 執行
                    HS_Lane().RunUnload();
                    clsLog.Log(clsEnum.enuLogName.ProcessLog.ToString(), $"{strThreadLogName} : RunUnload");
                    iStepIndex = 300200;
                    #endregion
                    break;

                case 300200:
                    #region 執行結果
                    if (HS_Lane().m_enuAction == BaseLane.enuAction.Unload_Done)
                    {
                        clsLog.Log(clsEnum.enuLogName.ProcessLog.ToString(), $"{strThreadLogName} : Unload Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (HS_Lane().m_enuAction == BaseLane.enuAction.Unload_Fail)
                    {
                        clsLog.Log(clsEnum.enuLogName.ProcessLog.ToString(), $"{strThreadLogName} : Unload Fail → 回閒置等處理");
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

        #region //===================== Public 函式 =====================
        public bool IsProcOK() => !bIsProcessing && bIsReady;
        public void RunInitial()
        {
            iStepIndex = -1;
            bIsReady = true;
            clsLog.Log(clsEnum.enuLogName.ProcessLog.ToString(), strThreadLogName + " : RunInitial Done");
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

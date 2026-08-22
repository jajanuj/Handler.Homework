using ArtCommonLib;
using ArtData;
using ArtEQ._2_Function_流程_.Proc;
using System.Linq;

namespace ArtEQ._2_Function_流程_.AutoRun
{
    internal class AR_OK_Lane : clsThreadProc
    {
        #region Constructors

        #region //===================== 建構子 =====================

        public AR_OK_Lane(string p_strLogName) : base(p_strLogName)
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
                    if (!OK_Lane().IsProcOK()) break;

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

                #region //============== Load（等 AOI Lane 出料） ==============

                case 200000:

                    #region 可否執行

                    if (!OK_Lane().IsProcOK()) break;

                    if (CanLoad())
                    {
                        iStepIndex = 200100;
                    }

                    #endregion

                    break;

                case 200100:

                    #region 執行

                    if (!OK_Lane().IsProcOK()) break;

                    OK_Lane().RunLoad();
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : RunLoad");
                    iStepIndex = 200200;

                    #endregion

                    break;

                case 200200:

                    #region 執行結果

                    if (!OK_Lane().IsProcOK()) break;

                    if (OK_Lane().m_enuAction == BaseLane.enuAction.Load_Done)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Load Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (OK_Lane().m_enuAction == BaseLane.enuAction.Load_Fail)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Load Fail → 回閒置等處理");
                        iStepIndex = 100000;
                    }

                    #endregion

                    break;

                #endregion

                #region //============== Unload (卸載至 OK Discharge Magazine) ==============

                case 300000:

                    #region 可否執行

                    if (!OK_Lane().IsProcOK()) break;
                    if (CanUnload())
                    {
                        iStepIndex = 300100;
                    }

                    #endregion

                    break;

                case 300100:

                    #region 執行

                    OK_Lane().RunUnload();
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : RunUnload");
                    iStepIndex = 300200;

                    #endregion

                    break;

                case 300200:

                    #region 執行結果

                    if (OK_Lane().m_enuAction == BaseLane.enuAction.Unload_Done)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Unload Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (OK_Lane().m_enuAction == BaseLane.enuAction.Unload_Fail)
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

        private static AR_OK_Lane m_Singleton;
        private static object m_objLock = new object();

        public static AR_OK_Lane GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new AR_OK_Lane("AR_OK_Lane");
                    }
                }
            }

            return m_Singleton;
        }

        #endregion

        #region //===================== Private Helper =====================

        /// <summary>
        /// 本站(OK流道)
        /// </summary>
        /// <returns></returns>
        private Proc_OK_Lane OK_Lane() => Proc_OK_Lane.GetSingleton();

        /// <summary>
        /// 上游流道(AOI)
        /// </summary>
        /// <returns></returns>
        private Proc_AOI_Lane AOI_Lane() => Proc_AOI_Lane.GetSingleton();

        /// <summary>
        /// 下游料盒(OK收料)
        /// </summary>
        /// <returns></returns>
        private Proc_OK_Discharge_Magazine Mag_OK_Discharge() => Proc_OK_Discharge_Magazine.GetSingleton();

        #endregion

        #region //===================== 條件判斷 =====================

        /// <summary>
        /// 確認可以載入
        /// 條件：Load位置無帳料 + 上游 AOI Lane 已經出料等待
        /// </summary>
        private bool CanLoad()
        {
            bool rValue = true;

            // 1. 上游 AOI Lane 狀態確認
            rValue &= AOI_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      AOI_Lane().m_enuAction == BaseLane.enuAction.Unload_Waiting;

            //2. Lane料帳確認
            rValue &= !OK_Lane().m_Temp_Tray_Info.bIsExist;

            //3. Lane是否流程就緒
            rValue &= OK_Lane().IsProcOK();

            //4. Lane狀態確認
            rValue &= OK_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      OK_Lane().m_enuAction == BaseLane.enuAction.Unload_Done;

            return rValue;
        }

        /// <summary>
        /// 確認可以卸載
        /// 條件：Load位置有帳料 + 下游 OK Discharge Magazine 準備收料
        /// </summary>
        private bool CanUnload()
        {
            bool rValue = true;

            // 1. 下游 OK Discharge Magazine 狀態確認
            rValue &= Mag_OK_Discharge().m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Waiting ||
                      Mag_OK_Discharge().m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Done ||
                      Mag_OK_Discharge().m_enuAction == BaseMagazine.enuAction.Initial_Done;

            //2. Lane料帳確認
            rValue &= OK_Lane().m_Temp_Tray_Info.bIsExist;

            //3. Lane是否流程就緒
            rValue &= OK_Lane().IsProcOK();

            //4. Lane狀態確認
            rValue &= OK_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      OK_Lane().m_enuAction == BaseLane.enuAction.Load_Done;

            //5. 這盤 Tray 裡不能還有殘留的 NG 判定格子，要等 Sort Arm 把 NG 都搬完
            rValue &= !OK_Lane().m_Temp_Tray_Info.AssyRecords.Any(v => v.IsExist && v.AoiResult == clsEnum.AoiResult.Ng);

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

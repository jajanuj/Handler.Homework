using ArtCommonLib;
using ArtData;
using ArtEQ._2_Function_流程_.AutoRun;

namespace ArtEQ
{
    /// <summary> 自動運轉流程規劃 </summary>
    public class ProcAutoRun : clsThreadProc
    {
        #region //=====================  全域變數設置 =====================

        /// <summary> LotID </summary>
        static public string sLotID = "";

        /// <summary> 結批 </summary>
        static public bool bIsLotEnd = false;

        /// <summary> 結批流程已啟動無法取消(bIsLotEnd) </summary>
        static public bool bIsAlreadyStartLotEnd = false;

        /// <summary> 停止入料 </summary>
        static public bool bIsStopLoad = false;

        /// <summary> 執行模式</summary>
        static public bool bIsManualMode = false;

        /// <summary> 執行模式</summary>
        static public bool bIsAutoRunMode = false;

        #endregion

        #region //=====================  區域變數設置 =====================

        #endregion

        #region //=====================  必要函式設置 =====================

        public ProcAutoRun(string p_strLogName)
            : base(p_strLogName)
        {
        }

        protected override void Scenario()
        {
            switch (iStepIndex)
            {
                case 0:
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), strThreadLogName + " : ===== Proc Auto Run Start =====");
                    clsCmData.g_NowEqStatus = clsCmData.enuEqStatus.Run;

                    bIsLotEnd = false;
                    bIsAlreadyStartLotEnd = false;
                    bIsStopLoad = false;
                    iStepIndex = 1000;
                    break;

                case 1000:
                    AR_Mag_IC_Feed.GetSingleton().Run_AutoRun();
                    AR_Mag_HS_Feed.GetSingleton().Run_AutoRun();
                    AR_HS_Lane.GetSingleton().Run_AutoRun();
                    AR_ASM_Lane.GetSingleton().Run_AutoRun();
                    AR_ASM_Arm.GetSingleton().Run_AutoRun();
                    AR_Mag_HS_Discharge.GetSingleton().Run_AutoRun();
                    AR_Press_Lane.GetSingleton().Run_AutoRun();
                    AR_Press_Station.GetSingleton().Run_AutoRun();
                    //AR_Mag_UnLoadNG.GetSingleton().Run_AutoRun();
                    //AR_Lane_Top.GetSingleton().Run_AutoRun();
                    //AR_Lane_Bottom.GetSingleton().Run_AutoRun();
                    //AR_Lane_FrontBack.GetSingleton().Run_AutoRun();
                    //AR_Lane_LeftRight.GetSingleton().Run_AutoRun();
                    //AR_Lane_OK.GetSingleton().Run_AutoRun();
                    //AR_Lane_NG.GetSingleton().Run_AutoRun();
                    //AR_AOI_Top.GetSingleton().Run_AutoRun();
                    //AR_AOI_Bottom.GetSingleton().Run_AutoRun();
                    //AR_AOI_FrontBack.GetSingleton().Run_AutoRun();
                    //AR_AOI_LeftRight.GetSingleton().Run_AutoRun();
                    //AR_Pick_FrontBack.GetSingleton().Run_AutoRun();
                    //AR_Pick_LeftRight.GetSingleton().Run_AutoRun();
                    //AR_Pick_OKNG.GetSingleton().Run_AutoRun();

                    iStepIndex = 2000;
                    break;

                case 2000:
                {
                    if (bIsLotEnd || bIsStopLoad)
                    {
                        //AllProcessOK &= !Proc_Lane_Top.GetSingleton().m_Temp_Tray_Info.isExist;
                        //AllProcessOK &= !Proc_Lane_Bottom.GetSingleton().m_Temp_Tray_Info.isExist;
                        //AllProcessOK &= !Proc_Lane_FrontBack.GetSingleton().m_Temp_Tray_Info.isExist;
                        //AllProcessOK &= !Proc_Lane_LeftRight.GetSingleton().m_Temp_Tray_Info.isExist;
                        //AllProcessOK &= !Proc_Lane_OK.GetSingleton().m_Temp_Tray_Info.isExist;
                        //AllProcessOK &= !Proc_Lane_NG.GetSingleton().m_Temp_Tray_Info.isExist;
                        //AllProcessOK &= !Proc_Pick_OKNG.GetSingleton().m_Temp_TagMoveData.Tag.IsExist;
                        //AllProcessOK &= Proc_Lane_NG.GetSingleton().m_enuAction == _2_Function_流程_.Base.BaseLane.enuAction.Lane_Unload_Done;
                        //if (AllProcessOK)
                        //{
                        //    this.iStepIndex = 3000;
                        //}
                    }
                }
                break;

                case 3000:
                    iStepIndex = 9000;
                    break;

                case 9000: //機台正常停機
                    ProcInitial.ParameterSet_Idle();
                    clsCmData.g_NowEqStatus = clsCmData.enuEqStatus.Idle;
                    bIsAutoRunMode = false;
                    bIsManualMode = false;
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), strThreadLogName + " : ===== Proc Auto Run End =====");
                    iStepIndex = -1;
                    bIsProcessing = false;
                    clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.LOT_batch_completed);
                    break;

                default:
                    iStepIndex = -1;
                    Stop();
                    bIsProcessing = false;
                    break;
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        #endregion

        #region //===================== private 函式設置 =====================

        #endregion
    }
}
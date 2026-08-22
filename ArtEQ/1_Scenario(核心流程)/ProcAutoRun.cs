using ArtCommonLib;
using ArtData;
using ArtEQ._2_Function_流程_.AutoRun;
using ArtEQ._2_Function_流程_.Proc;

namespace ArtEQ
{
    /// <summary> 自動運轉流程規劃 </summary>
    public class ProcAutoRun : clsThreadProc
    {
        #region Public Methods

        #region //===================== public 函式設置 =====================

        /// <summary>
        /// 結批期間判斷用：OK_Lane 或更上游(HS/ASM/Press/AOI)是否還有料在流，
        /// 代表之後還可能分出新的 NG。AR_Mag_NG_Feed(要不要繼續補空盤)、
        /// AR_NG_Lane(空盤要不要強制出料)共用同一個判斷，避免兩邊各寫一份、以後改一邊漏一邊。
        /// </summary>
        static public bool HasUpstreamWorkPendingSort()
        {
            return Proc_HS_Lane.GetSingleton().m_Temp_Tray_Info.bIsExist
                   || Proc_ASM_Lane.GetSingleton().m_Temp_Tray_Info.bIsExist
                   || Proc_Press_Lane.GetSingleton().m_Temp_Tray_Info.bIsExist
                   || Proc_AOI_Lane.GetSingleton().m_Temp_Tray_Info.bIsExist
                   || Proc_OK_Lane.GetSingleton().m_Temp_Tray_Info.bIsExist;
        }

        #endregion

        #endregion

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
                    AR_AOI_Lane.GetSingleton().Run_AutoRun();
                    AR_AOI_Station.GetSingleton().Run_AutoRun();
                    AR_OK_Lane.GetSingleton().Run_AutoRun();
                    AR_Mag_OK_Discharge.GetSingleton().Run_AutoRun();
                    AR_Mag_NG_Feed.GetSingleton().Run_AutoRun();
                    AR_NG_Lane.GetSingleton().Run_AutoRun();
                    AR_Mag_NG_Discharge.GetSingleton().Run_AutoRun();
                    AR_Sort_Arm.GetSingleton().Run_AutoRun();
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
                        bool bAllDrained = true;

                        // 六條流道都要淨空(沒有帳)才算流完。
                        bAllDrained &= !Proc_HS_Lane.GetSingleton().m_Temp_Tray_Info.bIsExist;
                        bAllDrained &= !Proc_ASM_Lane.GetSingleton().m_Temp_Tray_Info.bIsExist;
                        bAllDrained &= !Proc_Press_Lane.GetSingleton().m_Temp_Tray_Info.bIsExist;
                        bAllDrained &= !Proc_AOI_Lane.GetSingleton().m_Temp_Tray_Info.bIsExist;
                        bAllDrained &= !Proc_OK_Lane.GetSingleton().m_Temp_Tray_Info.bIsExist;
                        bAllDrained &= !Proc_NG_Lane.GetSingleton().m_Temp_Tray_Info.bIsExist;

                        // 兩隻手臂也要閒置——流道剛好淨空的那一瞬間，手臂有可能正夾著料
                        // 走在 Pick 完、Place 還沒完成的半路上，這段期間料只存在手臂身上，
                        // 不會反映在任何一條流道的帳上，只看流道會漏掉這個狀態。
                        bAllDrained &= Proc_ASM_Arm.GetSingleton().IsProcOK();
                        bAllDrained &= Proc_Sort_Arm.GetSingleton().IsProcOK();

                        // 三個 Feed Magazine 也要閒置——結批按下的當下如果剛好在推料，
                        // 料還沒真正過帳到下游流道之前，同樣不會反映在流道的帳上。同樣查 Proc_Xxx。
                        bAllDrained &= Proc_IC_Feed_Magazine.GetSingleton().IsProcOK();
                        bAllDrained &= Proc_HS_Feed_Magazine.GetSingleton().IsProcOK();
                        bAllDrained &= Proc_NG_Feed_Magazine.GetSingleton().IsProcOK();

                        // 強制結批：AR_NG_Lane.CanUnload() 在 bIsLotEnd 時已經改成不用等收滿
                        if (bAllDrained)
                        {
                            clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), strThreadLogName + " : 結批 - 所有流道已淨空，準備停機");
                            iStepIndex = 3000;
                        }
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
                    bIsLotEnd = false;
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

        #region //===================== private 函式設置 =====================

        #endregion
    }
}
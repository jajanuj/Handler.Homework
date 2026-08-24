using System;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtEQ._2_Function_流程_.AutoRun;
using ArtEQ._2_Function_流程_.Proc;
using ArtEQ._2_Function_流程_.Proc.Arm;
using ArtEQ._3_UI_介面管理_._1_Operator_操作模式_;
using ArtProcModuleLib;
using ArtSystem;
using ArtSystem.MultiSystem;

//using ArtDeepCloner;

namespace ArtEQ
{
    /// <summary> Initial 流程規劃 </summary>
    public class ProcInitial : clsThreadProc
    {
        #region //=====================  變數設置 =====================

        private double dInitialSpendTime_ms_Log = 0;
        clsHiPerfTimer mTimer_Initial = new clsHiPerfTimer();

        #endregion

        #region //===================== Enum 宣告 =====================

        #endregion

        #region //=====================  必要函式設置 =====================

        public ProcInitial(string p_strLogName)
            : base(p_strLogName)
        {
        }

        protected override void Scenario() //tao autorun init
        {
            switch (iStepIndex)
            {
                case 0:
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), "========= Initial Start =========");
                    ShowProcBar("Initializing...", 10);

                    mTimer_Initial.Restart(); // 紀錄 Initial 運作時間總長

                    clsCmData.g_NowEqStatus = clsCmData.enuEqStatus.Initial;
                    clsCmData.g_bIsinitialized = false;

                    SetMotionServo(false);

                    // 初始化參數
                    ParameterSet_Idle(); // 軟體內部參數
                    SetCardPmt();        // 軸卡參數(硬體參數)
                    AxisInitial();       // 馬達參數(軟體參數)

                    // PM模組 參數初始化
                    // PM.GetSingleton().InitialSetModule();

                    iStepIndex = 500;
                    Restart();
                    break;

                case 500:
                    ShowProcBar("Initializing...", 20);

                    if (IsTimeOut(500, clsCmData.enuSecUnit.MilliSec))
                    {
                        SetMotionServo(true);

                        iStepIndex = 1000;
                        Restart();
                    }

                    break;

                case 1000:

                    #region //Initial - Step 1：Tarot AR 狀態初始化

                    ShowProcBar("Initializing...", 30);

                    // 這裡只清 AR 自己的流程狀態，不做機構復歸。
                    // 真正機構復歸在 Step 2000 呼叫 Proc_xxx.AR_Initialize()。

                    AR_HS_Lane.GetSingleton().RunInitial();
                    AR_Mag_HS_Feed.GetSingleton().RunInitial();
                    AR_Mag_IC_Feed.GetSingleton().RunInitial();
                    AR_ASM_Lane.GetSingleton().RunInitial();
                    AR_ASM_Arm.GetSingleton().RunInitial();
                    AR_Mag_HS_Discharge.GetSingleton().RunInitial();
                    AR_Press_Lane.GetSingleton().RunInitial();
                    AR_Press_Station.GetSingleton().RunInitial();
                    AR_AOI_Lane.GetSingleton().RunInitial();
                    AR_AOI_Station.GetSingleton().RunInitial();
                    AR_OK_Lane.GetSingleton().RunInitial();
                    AR_Mag_OK_Discharge.GetSingleton().RunInitial();
                    AR_Mag_NG_Feed.GetSingleton().RunInitial();
                    AR_NG_Lane.GetSingleton().RunInitial();
                    AR_Mag_NG_Discharge.GetSingleton().RunInitial();
                    AR_Sort_Arm.GetSingleton().RunInitial();

                    //範例
                    //AR_Mag_LoadOK.GetSingleton().RunInitial();
                    //AR_LoadCup_lane.GetSingleton().RunInitial();
                    //AR_FillTea_Lane.GetSingleton().RunInitial();
                    //AR_Seal_Lane.GetSingleton().RunInitial();
                    //AR_AOI_Lane.GetSingleton().RunInitial();
                    //AR_OK_Lane.GetSingleton().RunInitial();
                    //AR_Mag_UnloadOK.GetSingleton().RunInitial();

                    //AR_Mag_LoadNG.GetSingleton().RunInitial();
                    //AR_NG_Lane.GetSingleton().RunInitial();
                    //AR_Mag_UnloadNG.GetSingleton().RunInitial();
                    //AR_Station_Sort.GetSingleton().RunInitial();


                    iStepIndex = 1010;
                    Restart();

                    #endregion

                    break;

                #region //等待 Tarot AR 初始化完成

                case 1010:
                    {
                        ShowProcBar("Initializing...", 40);

                        bool ProcInitialDone = true;

                        ProcInitialDone &= AR_HS_Lane.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_Mag_HS_Feed.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_Mag_IC_Feed.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_ASM_Lane.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_ASM_Arm.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_Mag_HS_Discharge.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_Press_Lane.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_Press_Station.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_AOI_Lane.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_AOI_Station.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_OK_Lane.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_Mag_OK_Discharge.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_Mag_NG_Feed.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_NG_Lane.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_Mag_NG_Discharge.GetSingleton().IsProcOK();
                        ProcInitialDone &= AR_Sort_Arm.GetSingleton().IsProcOK();

                        if (ProcInitialDone)
                        {
                            iStepIndex = 1050;
                            Restart();
                        }
                        else if (IsTimeOut(60000, clsCmData.enuSecUnit.MilliSec))
                        {
                            iStepIndex = -9000;
                            Restart();
                        }
                    }

                    #endregion

                    break;

                case 1050:
                    // 帳初始化
                    // 目前帳務清除交給各 Proc 的 AR_Initialize() 做。
                    iStepIndex = 2000;
                    Restart();
                    break;

                case 2000:

                    #region //Initial - Step 2：Proc 機構 / 帳務初始化

                    ShowProcBar("Initializing...", 50);

                    //範例
                    Proc_HS_Feed_Magazine.GetSingleton().RunInitial();
                    Proc_HS_Lane.GetSingleton().RunInitial();
                    Proc_IC_Feed_Magazine.GetSingleton().RunInitial();
                    Proc_ASM_Lane.GetSingleton().RunInitial();
                    Proc_ASM_Arm.GetSingleton().RunInitial();
                    Proc_HS_Discharge_Magazine.GetSingleton().RunInitial();
                    Proc_Press_Lane.GetSingleton().RunInitial();
                    Proc_Press_Station.GetSingleton().RunInitial();
                    Proc_AOI_Lane.GetSingleton().RunInitial();
                    Proc_AOI_Station.GetSingleton().RunInitial();
                    Proc_OK_Lane.GetSingleton().RunInitial();
                    Proc_OK_Discharge_Magazine.GetSingleton().RunInitial();
                    Proc_NG_Feed_Magazine.GetSingleton().RunInitial();
                    Proc_NG_Lane.GetSingleton().RunInitial();
                    Proc_NG_Discharge_Magazine.GetSingleton().RunInitial();
                    Proc_Sort_Arm.GetSingleton().RunInitial();

                    iStepIndex = 2010;
                    Restart();

                    #endregion

                    break;

                #region //等待 Proc 初始化完成

                case 2010:
                    {
                        ShowProcBar("Initializing...", 60);

                        bool ProcInitialDone = true;

                        //範例
                        ProcInitialDone &= Proc_HS_Feed_Magazine.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_HS_Lane.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_IC_Feed_Magazine.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_ASM_Lane.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_ASM_Arm.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_HS_Discharge_Magazine.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_Press_Lane.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_Press_Station.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_AOI_Lane.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_AOI_Station.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_OK_Lane.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_OK_Discharge_Magazine.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_NG_Feed_Magazine.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_NG_Lane.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_NG_Discharge_Magazine.GetSingleton().IsProcOK();
                        ProcInitialDone &= Proc_Sort_Arm.GetSingleton().IsProcOK();

                        if (ProcInitialDone)
                        {
                            iStepIndex = 3000;
                            Restart();
                        }
                        else if (IsTimeOut(60000, clsCmData.enuSecUnit.MilliSec))
                        {
                            iStepIndex = -9000;
                            Restart();
                        }
                    }


                    break;

                #endregion

                case 3000:

                    #region //Initial - Step 3
                    InitAllData();
                    ShowProcBar("Initializing...", 70);
                    iStepIndex = 3010;
                    Restart();

                    #endregion

                    break;

                #region //等待 Step 3 完成

                case 3010:
                    {
                        ShowProcBar("Initializing...", 80);
                        iStepIndex = 9000;
                        Restart();
                    }

                    break;

                #endregion

                case 9000:

                    #region //初始化完成

                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), "========= Initial Finish =========");

                    HideProcBar();

                    ParameterSet_Idle(); // 軟體內部參數

                    // Initial 完成後清掉節批旗標
                    ProcAutoRun.bIsLotEnd = false;
                    ProcAutoRun.bIsAlreadyStartLotEnd = false;
                    ProcAutoRun.bIsStopLoad = false;
                    ProcAutoRun.bIsAutoRunMode = false;
                    ProcAutoRun.bIsManualMode = false;

                    clsCmData.g_bIsinitialized = true;
                    clsCmData.g_NowEqStatus = clsCmData.enuEqStatus.Idle;

                    ucMotionSetting.SetAllSpeed();

                    iStepIndex = -1;

                    mTimer_Initial.Stop();
                    dInitialSpendTime_ms_Log = mTimer_Initial.ElapsedMilliseconds;

                    #endregion

                    break;

                case -9000:

                    #region //初始化失敗

                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), "========= Initial Fail =========");

                    HideProcBar();

                    ParameterSet_Idle();
                    ucMotionSetting.SetAllSpeed();

                    iStepIndex = -1;

                    #endregion

                    break;

                default:
                    clsEditRunThread.EqStop();

                    iStepIndex = -1;
                    Stop();
                    bIsProcessing = false;
                    break;
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        /// <summary> 設置Idle參數 (清除軟體內部資料 , 例如 : 帳) </summary>
        static public void ParameterSet_Idle()
        {
        }

        /// <summary> 設置卡片參數 (已經交給ArtSystem處理) </summary>
        static public void SetCardPmt()
        {
            try
            {
                if (clsArtSystem.bIsSoftwareSimulate)
                {
                    return;
                }

                clsMultiSystem.SetCardParameter();
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        /// <summary> 設置所有軸參數 </summary>
        static public void AxisInitial()
        {
            clsAxisInfo AxisInfo = new clsAxisInfo();
            foreach (clsEnum.enuAxis eAxis in clsDioMotion.mDic_AxisInfo.Keys)
            {
                clsMotionCtrl.GetAxisInfo(eAxis, ref AxisInfo);
                clsMotionCtrl.SetConvertToMmValue(eAxis, ucMotionSetting.GetMmPerCir(eAxis), ucMotionSetting.GetPulsePerCir(eAxis));
            }

            clsProcCtrl.GetSingleton().InitialAxisState(); //PM模組
        }

        /// <summary> 設置所有軸Server On/Off </summary>
        static public void SetMotionServo(bool p_IsOn)
        {
            foreach (clsEnum.enuAxis eAxis in clsDioMotion.mDic_AxisInfo.Keys)
            {
                clsMotionCtrl.SetServo(eAxis, p_IsOn);
            }
        }

        /// <summary> 設置所有軸減速停止 </summary>
        static public void SetMotionStop()
        {
            foreach (clsEnum.enuAxis eAxis in clsDioMotion.mDic_AxisInfo.Keys)
            {
                clsMotionCtrl.SlowDownStop(eAxis);
            }
        }

        #endregion

        #region //===================== private 函式設置 =====================

        private void InitAllData()
        {
            if (PublicDeclare.bIsSimulate)
            {
                foreach (var keyValueParir in ucAutoRun.GetSingleton().m_Magazines)
                {
                    var magazine = keyValueParir.Value;
                    var magazineName = keyValueParir.Key;

                    if (magazine == null)
                        continue;

                    // 依這顆 Magazine 現在實際的 Slot 數灌測試資料
                    for (int slotNo = 1; slotNo <= magazine.GetMagazineSlotCount(); slotNo++)
                    {
                        switch (magazineName)
                        {
                            case "IC_Feed":
                                magazine.CreateIcTrayInfo(slotNo);
                                break;
                            case "HS_Feed":
                                magazine.CreateHeatSinkTrayInfo(slotNo);
                                break;
                            case "NG_Feed":
                                magazine.CreateEmptyMaterialTrayInfo(slotNo);
                                break;
                            default:
                                magazine.CreateEmptyTrayInfo(slotNo);
                                break;
                        }
                    }
                }
            }
        }

        private void ShowProcBar(string sMessage, int iPercentage)
        {
            if (dInitialSpendTime_ms_Log != 0)
            {
                iPercentage = (int)(100 * mTimer_Initial.ElapsedMilliseconds / dInitialSpendTime_ms_Log);
                if (iPercentage > 99)
                {
                    iPercentage = 99;
                }
            }

            if (iPercentage % 5 == 0)
            {
                if (formProcBar.GetSingleton().iProcValue != iPercentage)
                {
                    ucArtMain_Design.GetSingleton().ShowProc(sMessage, iPercentage);
                }
            }
        }

        private void HideProcBar()
        {
            ucArtMain_Design.GetSingleton().HideProc();
        }

        #endregion
    }
}
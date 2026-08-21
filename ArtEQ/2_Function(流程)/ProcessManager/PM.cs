using System.Collections.Generic;
using ArtControlLib;
using ArtData;
using ArtProcModuleLib;
using ArtSystem;
using ArtSystem.MultiSystem;

namespace ArtEQ
{
    /// <summary> 模組收集 </summary>
    public class PM
    {
        #region //=====================  模組建置 =====================

        public string ModuleRecipeName = "PM_SMEMA_Load";
        public Dictionary<clsEnum.enuProcName, clsBaseProc> mDic_PorcManager = new Dictionary<clsEnum.enuProcName, clsBaseProc>();
        public Dictionary<clsEnum.enuProcName, AutoProcess> mDic_AutoProcess = new Dictionary<clsEnum.enuProcName, AutoProcess>();

        #endregion

        #region //=====================  必要函式設置 =====================

        private static PM m_Singleton;
        public static PM GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new PM("PM");
            }
            return m_Singleton;
        }

        public PM(string p_strLogName)
        {
            CreateModule();
            InitialSetModule();
            clsProcCtrl.GetSingleton().UpdateControls();
        }


        #endregion

        #region //===================== Public 函式設置 ======================

        /// <summary> 建立模組流程 </summary>
        public void CreateModule()
        {
            if (ucParameter.GetValueDouble(clsEnum.enuPmtName.Sys_Timeout_HandShank) == 0)
            { ucParameter.SaveValue(clsEnum.enuPmtType.System, clsEnum.enuPmtName.Sys_Timeout_HandShank, 30000); }
            if (ucParameter.GetValueDouble(clsEnum.enuPmtName.Sys_Timeout_LaneTransfer) == 0)
            { ucParameter.SaveValue(clsEnum.enuPmtType.System, clsEnum.enuPmtName.Sys_Timeout_HandShank, 30000); }

            clsProcCtrl.GetSingleton().g_bSoftSimulate = clsArtSystem.bIsSoftwareSimulate;

            //指定ModuleRecipeName
            ModuleRecipeName = clsEnum.enuProcName.PM_SMEMA_Load.ToString();

            //建立所有PM
            mDic_PorcManager.Add(clsEnum.enuProcName.PM_SMEMA_Load, new PM_SMEMA(clsEnum.enuProcName.PM_SMEMA_Load.ToString(), (int)clsEnum.enuProcName.PM_SMEMA_Load, 1));

            mDic_PorcManager.Add(clsEnum.enuProcName.PM_Lane, new PM_Lane(clsEnum.enuProcName.PM_Lane.ToString(), (int)clsEnum.enuProcName.PM_Lane));

            mDic_PorcManager.Add(clsEnum.enuProcName.PM_SMEMA_Unload, new PM_SMEMA(clsEnum.enuProcName.PM_SMEMA_Unload.ToString(), (int)clsEnum.enuProcName.PM_SMEMA_Unload, 1));


            //建立所有AP
            mDic_AutoProcess.Add(clsEnum.enuProcName.AP_Lane, new AutoProcess(clsEnum.enuProcName.AP_Lane.ToString(), (int)clsEnum.enuProcName.AP_Lane));
        }

        /// <summary> 設定初始化參數 </summary>
        public void InitialSetModule()
        {
            InitialSet_Lane();
            InitialSet_SMEMA();
        }

        public clsBaseProc GetPM(clsEnum.enuProcName eProcName)
        {
            clsBaseProc rValue = null;
            if (mDic_PorcManager.ContainsKey(eProcName) == true)
            {
                rValue = mDic_PorcManager[eProcName];
            }
            return rValue;
        }
        public PM_Lane GetPM_Lane(clsEnum.enuProcName eProcName)
        {
            PM_Lane rValue = null;
            if (mDic_PorcManager.ContainsKey(eProcName) == true)
            {
                if (mDic_PorcManager[eProcName] is PM_Lane)
                {
                    rValue = (PM_Lane)mDic_PorcManager[eProcName];
                }
            }
            return rValue;
        }
        public PM_SMEMA GetPM_SMEMA(clsEnum.enuProcName eProcName)
        {
            PM_SMEMA rValue = null;
            if (mDic_PorcManager.ContainsKey(eProcName) == true)
            {
                if (mDic_PorcManager[eProcName] is PM_SMEMA)
                {
                    rValue = (PM_SMEMA)mDic_PorcManager[eProcName];
                }
            }
            return rValue;
        }


        public AutoProcess GetAP(clsEnum.enuProcName eAPName)
        {
            AutoProcess rValue = null;
            if (mDic_AutoProcess.ContainsKey(eAPName) == true)
            {
                rValue = mDic_AutoProcess[eAPName];
            }
            return rValue;
        }
        #endregion

        #region //===================== Private 函式設置 (Initial PM,AP, StatusChagned-Event) ======================

        private void InitialSet_Lane()
        {
            {
                PM_Lane mPM_Lane = GetPM_Lane(clsEnum.enuProcName.PM_Lane);
                mPM_Lane.ModuleStatusChanged += new ModuleStatusEventHandler(PM_ModuleStatusChanged);//Event事件

                #region//參數處理
                mPM_Lane.InitialBaseSystemPMT(clsBaseProc.enuBaseSystemPMT.DryRunMode, clsEnum.enuPmtName.Sys_MachineDryRun);
                //PM_Lane.InitialSystemPmt(PM_Lane.enuSystemPmt.Sys_PusherPreAction_Pitch, clsEnum.enuPmtName.Sys_LaneChange_Pusher_PreAction_Pitch_mm);
                #endregion

                #region//新增DI Sensor
                //mPM_Lane.InitialDI(PM_Lane.enuModuleDi.Sensor_Load, clsEnum.enuDi.Lane_In_B, clsEnum.enuPmtName.Sys_Delay_NoUse, clsEnum.enuPmtName.Sys_Timeout_LaneTransfer, true);
                //mPM_Lane.InitialDI(PM_Lane.enuModuleDi.Sensor_Slow, clsEnum.enuDi.Lane_Slow_B, clsEnum.enuPmtName.Sys_Delay_NoUse, clsEnum.enuPmtName.Sys_Timeout_LaneTransfer, true);
                //mPM_Lane.InitialDI(PM_Lane.enuModuleDi.Sensor_Arrive, clsEnum.enuDi.Lane_Arrived_B, clsEnum.enuPmtName.Sys_Delay_LaneArrived, clsEnum.enuPmtName.Sys_Timeout_LaneTransfer, true);
                //mPM_Lane.InitialDI(PM_Lane.enuModuleDi.Sensor_Unload, clsEnum.enuDi.Lane_Out_B, clsEnum.enuPmtName.Sys_Delay_NoUse, clsEnum.enuPmtName.Sys_Timeout_LaneTransfer, true);
                #endregion

                #region//新增DO Sensor
                mPM_Lane.InitialOutPut(PM_Lane.enuModuleDo.LoadPowerWheel_Work, clsEnum.enuDo.Lane_LoadPowerWheel_CW, clsEnum.enuPmtName.Sys_Delay_NoUse, false);
                mPM_Lane.InitialOutPut(PM_Lane.enuModuleDo.UnloadPowerWheel_Work, clsEnum.enuDo.Lane_UnloadPowerWheel_CW, clsEnum.enuPmtName.Sys_Delay_NoUse, false);
                #endregion

                #region//新增RollerMotor (皮帶馬達)
                clsCtrlRollerMotor LaneChange_Motor = ucDevices.GetSingleton().GetDevice_RollerMotor(0);
                if (LaneChange_Motor != null)
                {
                    clsBaseProc.RollerPMT m_Roller = new clsBaseProc.RollerPMT(LaneChange_Motor.mMotionRoller, false, clsEnum.enuPmtName.Sys_LaneMotorHighSpeed, clsEnum.enuPmtName.Sys_LaneMotorLowSpeed);
                    mPM_Lane.InitialRoller(m_Roller);
                }
                else
                {

                }
                #endregion

                #region//新增Cylinder
                {
                    Dictionary<PM_Lane.enuModuleCylinder, clsBoxCylinder> AddCylinder = new Dictionary<ArtProcModuleLib.PM_Lane.enuModuleCylinder, clsBoxCylinder>()
                    {
                        {PM_Lane.enuModuleCylinder.Stopper_ToWork, new clsBoxCylinder()},
                        {PM_Lane.enuModuleCylinder.Pusher_Extend, new clsBoxCylinder()},
                        {PM_Lane.enuModuleCylinder.LoadPowerWheel_ToDown, new clsBoxCylinder()},
                        {PM_Lane.enuModuleCylinder.UnloadPowerWheel_ToDown, new clsBoxCylinder()},
                    };

                    //AddCylinder[PM_Lane.enuModuleCylinder.Stopper_ToWork].Initial(
                    //               clsEnum.enuDo.Lane_Stopper, false,
                    //               clsEnum.enuDi.Lane_Stopper_Down, false, clsEnum.enuDi.Lane_Stopper_Up, false,
                    //               (uint)ucParameter.GetValueInt(clsEnum.enuPmtName.Sys_Timeout_ShortCylinder),
                    //               clsEnum.enuAlarm.Cylinder_Timeout_Lane_StopperDown.ToString("d"),
                    //               clsEnum.enuAlarm.Cylinder_Timeout_Lane_StopperUp.ToString("d"));

                    //AddCylinder[PM_Lane.enuModuleCylinder.Pusher_Extend].Initial(
                    //               clsEnum.enuDo.Lane_SidePusher, false,
                    //               clsEnum.enuDi.Lane_SidePusher_Back, false, clsEnum.enuDi.Lane_SidePusher_Back, true,
                    //               (uint)ucParameter.GetValueInt(clsEnum.enuPmtName.Sys_Timeout_ShortCylinder),
                    //               clsEnum.enuAlarm.Cylinder_Timeout_Lane_SidePusher_Back.ToString("d"),
                    //               clsEnum.enuAlarm.Cylinder_Timeout_Lane_SidePusher_Extend.ToString("d"));

                    //AddCylinder[PM_Lane.enuModuleCylinder.Stopper_ToWork].Initial(
                    //               clsEnum.enuDo.Lane_LoadPowerWheel_Down, false,
                    //               clsEnum.enuDi.Lane_LoadPowerWheel_Back, false, clsEnum.enuDi.Lane_LoadPowerWheel_Back, true,
                    //               (uint)ucParameter.GetValueInt(clsEnum.enuPmtName.Sys_Timeout_ShortCylinder),
                    //               clsEnum.enuAlarm.Cylinder_Timeout_Lane_LoadPowerWheel_Back.ToString("d"),
                    //               clsEnum.enuAlarm.Cylinder_Timeout_Lane_LoadPowerWheel_Extend.ToString("d"));

                    //AddCylinder[PM_Lane.enuModuleCylinder.Stopper_ToWork].Initial(
                    //               clsEnum.enuDo.Lane_UnloadPowerWheel_Down, false,
                    //               clsEnum.enuDi.Lane_UnloadPowerWheel_Back, false, clsEnum.enuDi.Lane_UnloadPowerWheel_Back, true,
                    //               (uint)ucParameter.GetValueInt(clsEnum.enuPmtName.Sys_Timeout_ShortCylinder),
                    //               clsEnum.enuAlarm.Cylinder_Timeout_Lane_UnloadPowerWheel_Back.ToString("d"),
                    //               clsEnum.enuAlarm.Cylinder_Timeout_Lane_UnloadPowerWheel_Extend.ToString("d"));


                    //將建立好的汽缸註冊到PMLane內
                    foreach (PM_Lane.enuModuleCylinder eCylinder in AddCylinder.Keys)
                    {
                        mPM_Lane.InitialCylinder(eCylinder, AddCylinder[eCylinder]);
                    }
                }
                #endregion

                #region//新增Axis : LaneChnage Y
                {
                    //clsBoxMotion m_SlideY = new clsBoxMotion();
                    //m_SlideY.iSimulateDelayTime = 300;
                    //Dictionary<clsEnum.enuProcName, clsEnum.enuPosName> m_DctSlide_Y = new Dictionary<clsEnum.enuProcName, clsEnum.enuPosName>();
                    ////DctPos(m_DctSlide_Y, clsEnum.enuProcName.PM_Lane_Change_Load, clsEnum.enuPosName.LaneChangePuller_Safe);
                    //DctPos(m_DctSlide_Y, clsEnum.enuProcName.PM_SMEMA_Port1_UnloadOut, clsEnum.enuPosName.LaneChangeY_To_SMEMA1_UnloadOut);
                    //DctPos(m_DctSlide_Y, clsEnum.enuProcName.PM_SMEMA_Port2_LoadBack, clsEnum.enuPosName.LaneChangeY_To_SMEMA2_LoadBack);
                    //DctPos(m_DctSlide_Y, clsEnum.enuProcName.PM_MagazineArm_Up, clsEnum.enuPosName.LaneChangeY_To_MgzArm_Up);
                    //DctPos(m_DctSlide_Y, clsEnum.enuProcName.PM_MagazineArm_Middle, clsEnum.enuPosName.LaneChangeY_To_MgzArm_Middle);
                    //DctPos(m_DctSlide_Y, clsEnum.enuProcName.PM_MagazineArm_Down, clsEnum.enuPosName.LaneChangeY_To_MgzArm_Down);
                    //DctPos(m_DctSlide_Y, clsEnum.enuProcName.PM_BoatBarcode, clsEnum.enuPosName.LaneChangeY_To_BoatBarcode);
                    //m_SlideY.Initial(clsEnum.enuAxis.Axis5, MotionUnit.Minimeter,
                    //  (uint)ucMotionSetting.GetTimeOut(clsEnum.enuAxis.Axis5), PublicDeclare.MotionAlarmCode(clsEnum.enuAxis.Axis5));
                    //clsBaseProc.AxisPMT SlideY = new clsBaseProc.AxisPMT(m_SlideY, clsProcInfo.clsInfo_Axis.enuAxisDirection.Normal, m_DctSlide_Y);
                    //PM_Lane.InitialAxis(PM_Lane.enuModuleAxis.Axis_SlideY, SlideY);
                }
                #endregion
            }
        }

        private void InitialSet_SMEMA()
        {
            {
                PM_SMEMA mPM_SMEMA = GetPM_SMEMA(clsEnum.enuProcName.PM_SMEMA_Load);
                mPM_SMEMA.ModuleStatusChanged += new ModuleStatusEventHandler(PM_ModuleStatusChanged);//Event事件

                //參數處理
                mPM_SMEMA.InitialBaseSystemPMT(clsBaseProc.enuBaseSystemPMT.DryRunMode, clsEnum.enuPmtName.Sys_MachineDryRun);
                mPM_SMEMA.InitialSystemPmt(PM_SMEMA.enuSystemPmt.Sys_SMEMAEnable, clsEnum.enuPmtName.Sys_EnableSMEMA);

                //設定DI,DO
                //mPM_SMEMA.InitialSensor(PM_SMEMA.enuModuleDi.Sensor_SMEMA, clsEnum.enuDi.SMEMA_UpStream_Load, clsEnum.enuPmtName.Sys_Delay_SMEMASingal, clsEnum.enuPmtName.Sys_Timeout_LaneTransfer);
                mPM_SMEMA.InitialOutPut(PM_SMEMA.enuModuleDo.OutPut_SMEMA, clsEnum.enuDo.SMEMA_UpStream_Load, clsEnum.enuPmtName.Sys_Delay_SMEMASingal);

            }

            {
                PM_SMEMA mPM_SMEMA = GetPM_SMEMA(clsEnum.enuProcName.PM_SMEMA_Unload);
                mPM_SMEMA.ModuleStatusChanged += new ModuleStatusEventHandler(PM_ModuleStatusChanged);//Event事件

                //參數處理
                mPM_SMEMA.InitialBaseSystemPMT(clsBaseProc.enuBaseSystemPMT.DryRunMode, clsEnum.enuPmtName.Sys_MachineDryRun);
                mPM_SMEMA.InitialSystemPmt(PM_SMEMA.enuSystemPmt.Sys_SMEMAEnable, clsEnum.enuPmtName.Sys_EnableSMEMA);

                //設定DI,DO
                //mPM_SMEMA.InitialSensor(PM_SMEMA.enuModuleDi.Sensor_SMEMA, clsEnum.enuDi.SMEMA_DownStream_Unload, clsEnum.enuPmtName.Sys_Delay_SMEMASingal, clsEnum.enuPmtName.Sys_Timeout_LaneTransfer);
                mPM_SMEMA.InitialOutPut(PM_SMEMA.enuModuleDo.OutPut_SMEMA, clsEnum.enuDo.SMEMA_DownStream_Unload, clsEnum.enuPmtName.Sys_Delay_SMEMASingal);
            }
        }

        private void PM_ModuleStatusChanged(clsBaseProc sender, ModuleStatusEventArgs e)
        {
            switch (sender.strThreadLogName)
            {
                case "PM_Lane":
                    if (e.StatusName == PM_Lane.enuWorkState.Initial_End_Done.ToString())//範例
                    {
                    }
                    break;
                case "PM_SMEMA_Load":
                    if (e.StatusName == PM_SMEMA.enuWorkState.Source_PreWork.ToString())
                    {
                        PM.GetSingleton().GetPM_SMEMA(clsEnum.enuProcName.PM_SMEMA_Load).CreateBoatData();
                    }
                    break;
                case "PM_SMEMA_Unload":
                    break;
            }
        }

        #endregion

        #region //===================== Private 函式設置 ======================

        /// <summary> Dictionary新增防呆保護 </summary>
        private void DctPos(Dictionary<clsEnum.enuProcName, clsEnum.enuPosName> p_NeedCheck, clsEnum.enuProcName p_key, clsEnum.enuPosName p_value)
        {
            if (p_NeedCheck.ContainsKey(p_key))
            {
                return;
            }
            p_NeedCheck.Add(p_key, p_value);
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using ArtTeach;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtCommunication;
using ArtProcModuleLib;
using ArtModuleData;
namespace ArtEQ
{
    /// <summary> 模組收集 </summary>
    public class PM
    {
        #region //=====================  模組建置 =====================

        public  Dictionary<clsEnum.enuProcName, clsBaseProc> mDic_PorcManager = new Dictionary<clsEnum.enuProcName, clsBaseProc>();
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
            RunInitial();
            clsProcCtrl.GetSingleton().UpdateControls();
        }


        #endregion

        #region //===================== Public 函式設置 ======================

        /// <summary> 執行初始化 </summary>
        public void RunInitial()
        {
            //Init_LoadLane();

            mDic_PorcManager.Add(clsEnum.enuProcName.Lane_Change_Load, new PM_Lane("PM-" + clsEnum.enuProcName.Lane_Change_Load.ToString(), (int)clsEnum.enuProcName.Lane_Change_Load));
            mDic_PorcManager.Add(clsEnum.enuProcName.Lane_Change_LoadBack, new PM_Lane("PM-" + clsEnum.enuProcName.Lane_Change_LoadBack.ToString(), (int)clsEnum.enuProcName.Lane_Change_LoadBack));
            mDic_PorcManager.Add(clsEnum.enuProcName.SMEMA_Port1_UnloadOut, new PM_SMEMA("PM-" + clsEnum.enuProcName.SMEMA_Port1_UnloadOut.ToString(), (int)clsEnum.enuProcName.SMEMA_Port1_UnloadOut, 1));
            mDic_PorcManager.Add(clsEnum.enuProcName.SMEMA_Port2_LoadIn, new PM_SMEMA("PM-" + clsEnum.enuProcName.SMEMA_Port2_LoadIn.ToString(), (int)clsEnum.enuProcName.SMEMA_Port2_LoadIn, 2));
            mDic_PorcManager.Add(clsEnum.enuProcName.MagazineArm_Up, new PM_MgzArm("PM-" + clsEnum.enuProcName.MagazineArm_Up.ToString(), (int)clsEnum.enuProcName.MagazineArm_Up, 3));
            mDic_PorcManager.Add(clsEnum.enuProcName.MagazineArm_Middle, new PM_MgzArm("PM-" + clsEnum.enuProcName.MagazineArm_Middle.ToString(), (int)clsEnum.enuProcName.MagazineArm_Middle, 4));
            mDic_PorcManager.Add(clsEnum.enuProcName.MagazineArm_Down, new PM_MgzArm("PM-" + clsEnum.enuProcName.MagazineArm_Down.ToString(), (int)clsEnum.enuProcName.MagazineArm_Down, 5));
            mDic_PorcManager.Add(clsEnum.enuProcName.MagazineLoadLane, new PM_MgzLane("PM-" + clsEnum.enuProcName.MagazineLoadLane.ToString(), (int)clsEnum.enuProcName.MagazineLoadLane));

            ((PM_Lane)mDic_PorcManager[clsEnum.enuProcName.Lane_Change_Load]).InitialDI(ArtProcModuleLib.PM_Lane.enuModuleDi.Sensor_Load, clsEnum.enuDi.DI019,
                clsEnum.enuPmtName.Sys_Delay_LaneSensor, clsEnum.enuPmtName.Sys_Timeout_LaneTransfer, true);
            ((PM_Lane)mDic_PorcManager[clsEnum.enuProcName.Lane_Change_Load]).InitialDI(ArtProcModuleLib.PM_Lane.enuModuleDi.Sensor_Slow, clsEnum.enuDi.DI019,
                clsEnum.enuPmtName.Sys_Delay_LaneSensor, clsEnum.enuPmtName.Sys_Timeout_LaneTransfer, true);
            ((PM_Lane)mDic_PorcManager[clsEnum.enuProcName.Lane_Change_Load]).InitialDI(ArtProcModuleLib.PM_Lane.enuModuleDi.Sensor_Arrive, clsEnum.enuDi.DI019,
                clsEnum.enuPmtName.Sys_Delay_LaneSensor, clsEnum.enuPmtName.Sys_Timeout_LaneTransfer, true);
            ((PM_Lane)mDic_PorcManager[clsEnum.enuProcName.Lane_Change_Load]).InitialDI(ArtProcModuleLib.PM_Lane.enuModuleDi.Sensor_Unload, clsEnum.enuDi.DI019,
                clsEnum.enuPmtName.Sys_Delay_LaneSensor, clsEnum.enuPmtName.Sys_Timeout_LaneTransfer, true);
            ((PM_Lane)mDic_PorcManager[clsEnum.enuProcName.Lane_Change_Load]).InitialDI(ArtProcModuleLib.PM_Lane.enuModuleDi.Sensor_Unload_CanPush, clsEnum.enuDi.DI019,
                clsEnum.enuPmtName.Sys_Delay_LaneSensor, clsEnum.enuPmtName.Sys_Timeout_LaneTransfer, true);

            ((PM_SMEMA)mDic_PorcManager[clsEnum.enuProcName.SMEMA_Port1_UnloadOut]).InitialSensor(PM_SMEMA.enuModuleDi.Sensor_SMEMA, clsEnum.enuDi.DI012, clsEnum.enuPmtName.Sys_Delay_SMEMASingal, clsEnum.enuPmtName.Sys_Timeout_LaneTransfer);
            ((PM_SMEMA)mDic_PorcManager[clsEnum.enuProcName.SMEMA_Port1_UnloadOut]).InitialOutPut(PM_SMEMA.enuModuleDo.OutPut_SMEMA, clsEnum.enuDo.DO012, clsEnum.enuPmtName.Sys_Delay_SMEMASingal);
            ((PM_SMEMA)mDic_PorcManager[clsEnum.enuProcName.SMEMA_Port2_LoadIn]).InitialSensor(PM_SMEMA.enuModuleDi.Sensor_SMEMA, clsEnum.enuDi.DI013, clsEnum.enuPmtName.Sys_Delay_SMEMASingal, clsEnum.enuPmtName.Sys_Timeout_LaneTransfer);
            ((PM_SMEMA)mDic_PorcManager[clsEnum.enuProcName.SMEMA_Port2_LoadIn]).InitialOutPut(PM_SMEMA.enuModuleDo.OutPut_SMEMA, clsEnum.enuDo.DO013, clsEnum.enuPmtName.Sys_Delay_SMEMASingal);

            GetPM_Lane(clsEnum.enuProcName.Lane_Change_LoadBack).m_LaneData = GetPM_Lane(clsEnum.enuProcName.Lane_Change_Load).GetBoatData();
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
        public PM_MgzArm GetPM_MgzArm(clsEnum.enuProcName eProcName)
        {
            PM_MgzArm rValue = null;
            if (mDic_PorcManager.ContainsKey(eProcName) == true)
            {
                if (mDic_PorcManager[eProcName] is PM_MgzArm)
                {
                    rValue = (PM_MgzArm)mDic_PorcManager[eProcName];
                }
            }
            return rValue;
        }
        public PM_MgzLane GetPM_MgzLane(clsEnum.enuProcName eProcName)
        {
            PM_MgzLane rValue = null;
            if (mDic_PorcManager.ContainsKey(eProcName) == true)
            {
                if (mDic_PorcManager[eProcName] is PM_MgzLane)
                {
                    rValue = (PM_MgzLane)mDic_PorcManager[eProcName];
                }
            }
            return rValue;
        }
        #endregion



        #region //===================== LoadLane ======================
        private void Init_LoadLane()
        {
            Init_LoadLane_DI();
            Init_LoadLane_Cylinder();
            Init_LoadLane_Roller();
            Init_LoadLane_Axis();
            Init_LoadLane_DO();
        }

        private void Init_LoadLane_DI()
        {
            //Proc_LoadLane.InitialDI(clsEnum.enuDi.PutCoverLane_Load_Btype, new clsBaseProc.SensorPMT(clsEnum.enuPmtName.Delay_LoadIn, clsEnum.enuPmtName.TimeOut_Lane_Load, true)
            // , clsEnum.enuDi.PutCoverLane_Arrive_Btype, new clsBaseProc.SensorPMT(clsEnum.enuPmtName.Delay_Arrive, clsEnum.enuPmtName.TimeOut_Lane_Arrive, true)
            // , clsEnum.enuDi.PutCoverLane_Unload_Btype, new clsBaseProc.SensorPMT(clsEnum.enuPmtName.Delay_Unload, clsEnum.enuPmtName.TimeOut_Lane_Unload, true)
            // , clsEnum.enuDi.PutCoverLane_Slow_Btype, new clsBaseProc.SensorPMT(clsEnum.enuPmtName.Delay_Slow, clsEnum.enuPmtName.TimeOut_Lane_Slow, true)
            // , clsEnum.enuDi.PutCoverLane_IsEixstandCanPush, new clsBaseProc.SensorPMT(clsEnum.enuPmtName.Delay_IsExist, clsEnum.enuPmtName.TimeOut_Lane_IsExist, true));

            //Proc_LoadLane.InitialDI(PM_Lane.enuModuleDi.Sensor_OutSideProtect1, clsEnum.enuDi.LaserLaneA_Load_Btype,
            //    clsEnum.enuPmtName.Delay_LoadIn, clsEnum.enuPmtName.TimeOut_Lane_Load, true);
            //Proc_LoadLane.InitialDI(PM_Lane.enuModuleDi.Sensor_OutSideProtect2, clsEnum.enuDi.LaserLaneB_Load_Btype,
            //    clsEnum.enuPmtName.Delay_LoadIn, clsEnum.enuPmtName.TimeOut_Lane_Load, true);
            //Proc_LoadLane.InitialDI(PM_Lane.enuModuleDi.Sensor_OutSideProtect3, clsEnum.enuDi.ReflowB_Unload_Btype,
            //    clsEnum.enuPmtName.Delay_Unload, clsEnum.enuPmtName.TimeOut_Lane_Unload, true);
            //Proc_LoadLane.InitialDI(PM_Lane.enuModuleDi.Sensor_OutSideProtect4, clsEnum.enuDi.LoaderA_DetectBugle_Btype,
            //   clsEnum.enuPmtName.Delay_MagazineLUL, clsEnum.enuPmtName.TimeOut_EmptyMgzPort, true);
            //Proc_LoadLane.InitialDI(PM_Lane.enuModuleDi.Sensor_OutSideProtect5, clsEnum.enuDi.LoaderB_DetectBugle_Btype,
            //   clsEnum.enuPmtName.Delay_MagazineLUL, clsEnum.enuPmtName.TimeOut_EmptyMgzPort, true);
        }
        private void Init_LoadLane_Roller()
        {
            //clsBaseProc.RollerPMT m_Roller = new clsBaseProc.RollerPMT(m_PutCovarRoller, false,
            //    clsEnum.enuPmtName.Sys_LaneMotorHighSpeed, clsEnum.enuPmtName.Sys_LaneMotorLowSpeed);
            //Proc_LoadLane.InitialRoller(m_Roller);
        }

        private void Init_LoadLane_Cylinder()
        {
            //clsBoxCylinder m_Separate = new clsBoxCylinder();
            //clsBoxCylinder m_Side = new clsBoxCylinder();
            //clsBoxCylinder m_Stopper = new clsBoxCylinder();
            //clsBoxCylinder m_OtherStopper = new clsBoxCylinder();
            //clsBoxCylinder m_Wheel = new clsBoxCylinder();

            //m_Separate.Initial(clsEnum.enuDo.PutCoverLane_Separate_ToUp,
            //   clsEnum.enuDi.PutCoverLane_Separate_Down, false,
            //   clsEnum.enuDi.PutCoverLane_Separate_Up, false,
            //   (uint)ucParameter.GetValueInt(clsEnum.enuPmtName.TimeOut_Lane_Separate),
            //   null,
            //   clsEnum.enuAlarm.LoadLane_Separate_Error.ToString());

            //m_Side.Initial(clsEnum.enuDo.PutCoverLane_SidePusher_Extend,
            //   clsEnum.enuDi.PutCoverLane_SidePusher, false,
            //   clsEnum.enuDi.PutCoverLane_SidePusher, true,
            //   (uint)ucParameter.GetValueInt(clsEnum.enuPmtName.TimeOut_Side),
            //   null,
            //   clsEnum.enuAlarm.LoadLane_Silde_Error.ToString());

            //m_Stopper.Initial(clsEnum.enuDo.PutCoverLane_LoadStoper_ToUp,
            //               clsEnum.enuDi.PutCoverLane_LoadStoper_Down, false,
            //               clsEnum.enuDi.PutCoverLane_LoadStoper_Up, false,
            //               (uint)ucParameter.GetValueInt(clsEnum.enuPmtName.TimeOut_Lane_Stopper),
            //               null,
            //               clsEnum.enuAlarm.LoadLane_LDStopper_Error.ToString());

            //m_OtherStopper.Initial(clsEnum.enuDo.PutCoverLane_UnloadStoper_ToUp,
            //   clsEnum.enuDi.PutCoverLane_UnloadStoper_Down, false,
            //   clsEnum.enuDi.PutCoverLane_UnloadStoper_Up, false,
            //   (uint)ucParameter.GetValueInt(clsEnum.enuPmtName.TimeOut_Lane_Stopper),
            //   null,
            //   clsEnum.enuAlarm.LoadLane_ULDStopper_Error.ToString());

            //m_Wheel.Initial(clsEnum.enuDo.PutCoverLane_Wheel_ToDown,
            //   clsEnum.enuDi.PutCoverLane_Wheel_Down, false,
            //   clsEnum.enuDi.PutCoverLane_Wheel_Up, false,
            //   (uint)ucParameter.GetValueInt(clsEnum.enuPmtName.TimeOut_Lane_Wheel),
            //   null,
            //   clsEnum.enuAlarm.LoadLane_PowerWheel_Error.ToString());

            //Proc_LoadLane.InitialCylinder(PM_Lane.enuModuleCylinder.Separate_ToWork, m_Separate);
            //Proc_LoadLane.InitialCylinder(PM_Lane.enuModuleCylinder.SidePush_ToWork, m_Side);
            //Proc_LoadLane.InitialCylinder(PM_Lane.enuModuleCylinder.Stopper_ToWork, m_Stopper);
            //Proc_LoadLane.InitialCylinder(PM_Lane.enuModuleCylinder.OtherStopper_ToWork, m_OtherStopper);
            //Proc_LoadLane.InitialCylinder(PM_Lane.enuModuleCylinder.LoadPowerWheel_ToDown, m_Wheel);
        }
        private void Init_LoadLane_Axis()
        {
            //clsBoxMotion m_SlideY = new clsBoxMotion();
            //Dictionary<clsEnum.enuProcName, clsEnum.enuPosName> m_DctSlide_Y = new Dictionary<clsEnum.enuProcName, clsEnum.enuPosName>();

            //DctPos(m_DctSlide_Y, clsEnum.enuProcName.Proc_ReMoveCoverLane, clsEnum.enuPosName.CoverLC_Y_SafePos);
            //DctPos(m_DctSlide_Y, clsEnum.enuProcName.Proc_CoverPP, clsEnum.enuPosName.CoverLC_Y_PutCover);
            //DctPos(m_DctSlide_Y, clsEnum.enuProcName.Proc_BoatPP, clsEnum.enuPosName.CoverLC_Y_GetBoat);
            //DctPos(m_DctSlide_Y, clsEnum.enuProcName.Proc_OkAMagazine_Up, clsEnum.enuPosName.CoverLC_Y_LoaderA);
            //DctPos(m_DctSlide_Y, clsEnum.enuProcName.Proc_OkAMagazine_Mid, clsEnum.enuPosName.CoverLC_Y_LoaderA);
            //DctPos(m_DctSlide_Y, clsEnum.enuProcName.Proc_OkAMagazine_Down, clsEnum.enuPosName.CoverLC_Y_LoaderA);
            //DctPos(m_DctSlide_Y, clsEnum.enuProcName.Proc_OkBMagazine_Up, clsEnum.enuPosName.CoverLC_Y_LoaderB);
            //DctPos(m_DctSlide_Y, clsEnum.enuProcName.Proc_OkBMagazine_Mid, clsEnum.enuPosName.CoverLC_Y_LoaderB);
            //DctPos(m_DctSlide_Y, clsEnum.enuProcName.Proc_OkBMagazine_Down, clsEnum.enuPosName.CoverLC_Y_LoaderB);
            //DctPos(m_DctSlide_Y, clsEnum.enuProcName.Proc_ReflowB, clsEnum.enuPosName.CoverLC_Y_ReflowB);
            //m_SlideY.Initial(clsEnum.enuAxis.Axis5, MotionUnit.Minimeter, (uint)ucMotionSetting.GetTimeOut(clsEnum.enuAxis.Axis5),
            //    PublicDeclare.MotionAlarmCode(clsEnum.enuAxis.Axis5));

            //clsBaseProc.AxisPMT SlideY = new clsBaseProc.AxisPMT(m_SlideY, clsProcInfo.clsInfo_Axis.enuAxisDirection.Normal, m_DctSlide_Y);

            //Proc_LoadLane.InitialAxis(PM_Lane.enuModuleAxis.Axis_SlideY, SlideY);
        }
        private void Init_LoadLane_DO()
        {
            //Proc_LoadLane.InitialOutPut(PM_Lane.enuModuleDo.LoadPowerWheel_Work, clsEnum.enuDo.PutCoverLane_Wheel_Run, clsEnum.enuPmtName.Delay_Wheel);
        }
        #endregion

        private void DctPos(Dictionary<clsEnum.enuProcName, clsEnum.enuPosName> p_NeedCheck, clsEnum.enuProcName p_key, clsEnum.enuPosName p_value)
        {
            if (p_NeedCheck.ContainsKey(p_key))
            {
                return;
            }
            p_NeedCheck.Add(p_key, p_value);
        }
    }
}

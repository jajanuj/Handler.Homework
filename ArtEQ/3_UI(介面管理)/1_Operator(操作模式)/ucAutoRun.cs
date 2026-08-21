using System;
using System.Drawing;
using ArtCommonLib;
using ArtEQ._2_Function_流程_.Proc;
using ArtEQ.B_Tools;

namespace ArtEQ._3_UI_介面管理_._1_Operator_操作模式_
{
    public partial class ucAutoRun : ucBaseUserControl
    {
        public static ucAutoRun GetSingleton() => GetSingletonInstance(() => new ucAutoRun());

        #region Process

        private Proc_IC_Feed_Magazine m_IC_Magazine;
        private Proc_OK_Discharge_Magazine m_OK_Discharge_Magazine;
        private Proc_HS_Feed_Magazine m_HS_Feed_Magazine;
        private Proc_NG_Feed_Magazine m_NG_Feed_Magazine;
        private Proc_HS_Discharge_Magazine m_HS_Discharge_Magazine;
        private Proc_NG_Discharge_Magazine m_NG_Discharge_Magazine;

        private Proc_ASM_Lane m_ASM_Lane;
        private Proc_Press_Lane m_Press_Lane;
        private Proc_AOI_Lane m_AOI_Lane;
        private Proc_OK_Lane m_OK_Lane;
        private Proc_HS_Lane m_HS_Lane;
        private Proc_NG_Lane m_NG_Lane;

        private Proc_ASM_Arm m_ASM_Arm;
        private Proc_Sort_Arm m_Sort_Arm;

        private Proc_Press_Station m_Press_Station;
        private Proc_AOI_Station m_AOI_Station;

        #endregion

        public ucAutoRun()
        {
            InitializeComponent();
            BindProcessSingleton();
        }

        protected static T GetSingletonInstance<T>(Func<T> factory) where T : class => SingletonHelper<T>.GetOrCreate(factory);

        #region //=====================  UI刷新 =====================
        protected override void ReflashTimerFunc()
        {
            UpdateICMagazine();
            UpdateHSFeedMagazine();
            UpdateHSDischargeMagazine();
            UpdateNGFeedMagazine();
            UpdateNGDischargeMagazine();
            UpdateOKDischargeMagazine();

            UpdateAsmLane();
            UpdatePressLane();
            UpdateAOILane();
            UpdateOKLane();
            UpdateHSLane();
            UpdateNGLane();
            UpdateAsmArm();
            UpdateSortArm();

            UpdatePressStation();
            UpdateAOIStation();
        }

        private void UpdateAOIStation()
        {
            lblAoiStationStatus.Text = m_AOI_Station.m_enuAction.ToString();
        }

        private void UpdatePressStation()
        {
            lblPressStationStatus.Text = m_Press_Station.m_enuAction.ToString();
        }

        private void UpdateSortArm()
        {
            lblSortArmStatus.Text = m_Sort_Arm.m_enuAction.ToString();
            grpSortArm.BackColor = m_Sort_Arm.AssyRecord.IsExist ? Color.LimeGreen : SystemColors.Control;
        }

        private void UpdateAsmArm()
        {
            lblAsmArmStatus.Text = m_ASM_Arm.m_enuAction.ToString();
            grpAsmArm.BackColor = m_ASM_Arm.AssyRecord.IsExist ? Color.LimeGreen : SystemColors.Control;
        }

        /// <summary>
        /// 更新 IC Magazine 的狀態與示意圖，將 Magazine 的各種訊號與帳料狀態同步到 ucIC_Feed_Magazine_View 元件。
        /// </summary>
        private void UpdateICMagazine()
        {
            lblICMagazineStatus.Text = m_IC_Magazine.m_enuAction.ToString();

            ucIC_Feed_Magazine_View.Present = m_IC_Magazine.PresentSignal;
            ucIC_Feed_Magazine_View.PushFwd = m_IC_Magazine.PushFwdSignal;
            ucIC_Feed_Magazine_View.PushBwd = m_IC_Magazine.PushBwdSignal;
            ucIC_Feed_Magazine_View.OverPress = m_IC_Magazine.OverPressSignal;

            ucIC_Feed_Magazine_View.ReflashTimerFunc();
        }

        /// <summary>
        /// 更新 HS Feed Magazine 的狀態與示意圖，將 Magazine 的各種訊號與帳料狀態同步到 ucHS_Feed_Magazine_View 元件。
        /// </summary>
        private void UpdateHSFeedMagazine()
        {
            lblHSFeedMagazineStatus.Text = m_HS_Feed_Magazine.m_enuAction.ToString();

            ucHS_Feed_Magazine_View.Present = m_HS_Feed_Magazine.PresentSignal;
            ucHS_Feed_Magazine_View.PushFwd = m_HS_Feed_Magazine.PushFwdSignal;
            ucHS_Feed_Magazine_View.PushBwd = m_HS_Feed_Magazine.PushBwdSignal;
            ucHS_Feed_Magazine_View.OverPress = m_HS_Feed_Magazine.OverPressSignal;

            ucHS_Feed_Magazine_View.ReflashTimerFunc();
        }

        /// <summary>
        /// 更新 HS Discharge Magazine 的狀態與示意圖，將 Magazine 的各種訊號與帳料狀態同步到 ucHS_Discharge_Magazine_View 元件。
        /// </summary>
        private void UpdateHSDischargeMagazine()
        {
            lblHSDischargeMagazineStatus.Text = m_HS_Discharge_Magazine.m_enuAction.ToString();

            ucHS_Discharge_Magazine_View.Present = m_HS_Discharge_Magazine.PresentSignal;
            ucHS_Discharge_Magazine_View.ReflashTimerFunc();
        }

        /// <summary>
        /// 更新 NG Feed Magazine 的狀態與示意圖，將 Magazine 的各種訊號與帳料狀態同步到 ucNG_Feed_Magazine_View 元件。
        /// </summary>
        private void UpdateNGFeedMagazine()
        {
            lblNGFeedMagazineStatus.Text = m_NG_Feed_Magazine.m_enuAction.ToString();

            ucNG_Feed_Magazine_View.Present = m_NG_Feed_Magazine.PresentSignal;
            ucNG_Feed_Magazine_View.PushFwd = m_NG_Feed_Magazine.PushFwdSignal;
            ucNG_Feed_Magazine_View.PushBwd = m_NG_Feed_Magazine.PushBwdSignal;
            ucNG_Feed_Magazine_View.OverPress = m_NG_Feed_Magazine.OverPressSignal;

            ucNG_Feed_Magazine_View.ReflashTimerFunc();
        }

        /// <summary>
        /// 更新 NG Discharge Magazine 的狀態與示意圖，將 Magazine 的各種訊號與帳料狀態同步到 ucNG_Discharge_Magazine_View 元件。
        /// </summary>
        private void UpdateNGDischargeMagazine()
        {
            lblNGDischargeMagazineStatus.Text = m_NG_Discharge_Magazine.m_enuAction.ToString();
            ucNG_Discharge_Magazine_View.Present = m_NG_Discharge_Magazine.PresentSignal;
            ucNG_Discharge_Magazine_View.ReflashTimerFunc();
        }

        /// <summary>
        /// 更新 OK Discharge Magazine 的狀態與示意圖，將 Magazine 的各種訊號與帳料狀態同步到 ucOK_Discharge_Magazine_View 元件。
        /// </summary>
        private void UpdateOKDischargeMagazine()
        {
            lblOKDischargeMagazineStatus.Text = m_OK_Discharge_Magazine.m_enuAction.ToString();
            ucOK_Discharge_Magazine_View.Present = m_OK_Discharge_Magazine.PresentSignal;
            ucOK_Discharge_Magazine_View.ReflashTimerFunc();
        }

        /// <summary>
        /// 更新 ASM Lane 的狀態與 Tray 示意圖，將 Lane 的各種訊號與帳料狀態同步到 ucAsmTrayView 元件。
        /// </summary>
        private void UpdateAsmLane()
        {
            lblAsmLaneStatus.Text = m_ASM_Lane.m_enuAction.ToString();

            ucAsmTrayView.LoadSignal = m_ASM_Lane.LoadSignal;
            ucAsmTrayView.SlowSignal = m_ASM_Lane.SlowSignal;
            ucAsmTrayView.ArrivalSignal = m_ASM_Lane.ArrivalSignal;
            ucAsmTrayView.ForwardSignal = m_ASM_Lane.StopperExtendSignal;
            ucAsmTrayView.BackwardSignal = m_ASM_Lane.StopperRetractSignal;
            ucAsmTrayView.bCylinderOn = m_ASM_Lane.StopperExtendSignal;
            ucAsmTrayView.bTrayExist = m_ASM_Lane.HasTrayBill();
            ucAsmTrayView.ReflashTimerFunc();
        }

        /// <summary>
        /// 更新 Press Lane 的狀態與 Tray 示意圖，將 Lane 的各種訊號與帳料狀態同步到 ucPressTrayView 元件。
        /// </summary>
        private void UpdatePressLane()
        {
            lblPressLaneStatus.Text = m_Press_Lane.m_enuAction.ToString();

            ucPressTrayView.LoadSignal = m_Press_Lane.LoadSignal;
            ucPressTrayView.SlowSignal = m_Press_Lane.SlowSignal;
            ucPressTrayView.ArrivalSignal = m_Press_Lane.ArrivalSignal;
            ucPressTrayView.ForwardSignal = m_Press_Lane.StopperExtendSignal;
            ucPressTrayView.BackwardSignal = m_Press_Lane.StopperRetractSignal;
            ucPressTrayView.bCylinderOn = m_Press_Lane.StopperExtendSignal;
            ucPressTrayView.bTrayExist = m_Press_Lane.HasTrayBill();
            ucPressTrayView.ReflashTimerFunc();
        }

        /// <summary>
        /// 更新 AOI Lane 的狀態與 Tray 示意圖，將 Lane 的各種訊號與帳料狀態同步到 ucAOITrayView 元件。
        /// </summary>
        private void UpdateAOILane()
        {
            lblAOILaneStatus.Text = m_AOI_Lane.m_enuAction.ToString();

            ucAOITrayView.LoadSignal = m_AOI_Lane.LoadSignal;
            ucAOITrayView.SlowSignal = m_AOI_Lane.SlowSignal;
            ucAOITrayView.ArrivalSignal = m_AOI_Lane.ArrivalSignal;
            ucAOITrayView.ForwardSignal = m_AOI_Lane.StopperExtendSignal;
            ucAOITrayView.BackwardSignal = m_AOI_Lane.StopperRetractSignal;
            ucAOITrayView.bCylinderOn = m_AOI_Lane.StopperExtendSignal;
            ucAOITrayView.bTrayExist = m_AOI_Lane.HasTrayBill();
            ucAOITrayView.ReflashTimerFunc();
        }

        /// <summary>
        /// 更新 OK Lane 的狀態與 Tray 示意圖，將 Lane 的各種訊號與帳料狀態同步到 ucOKTrayView 元件。
        /// </summary>
        private void UpdateOKLane()
        {
            lblOKLaneStatus.Text = m_OK_Lane.m_enuAction.ToString();

            ucOKTrayView.LoadSignal = m_OK_Lane.LoadSignal;
            ucOKTrayView.SlowSignal = m_OK_Lane.SlowSignal;
            ucOKTrayView.ArrivalSignal = m_OK_Lane.ArrivalSignal;
            ucOKTrayView.ForwardSignal = m_OK_Lane.StopperExtendSignal;
            ucOKTrayView.BackwardSignal = m_OK_Lane.StopperRetractSignal;
            ucOKTrayView.bCylinderOn = m_OK_Lane.StopperExtendSignal;
            ucOKTrayView.bTrayExist = m_OK_Lane.HasTrayBill();
            ucOKTrayView.ReflashTimerFunc();
        }

        /// <summary>
        /// 更新 HS Lane 的狀態與 Tray 示意圖，將 Lane 的各種訊號與帳料狀態同步到 ucHSTrayView 元件。
        /// </summary>
        private void UpdateHSLane()
        {
            lblHSLaneStatus.Text = m_HS_Lane.m_enuAction.ToString();

            ucHSTrayView.LoadSignal = m_HS_Lane.LoadSignal;
            ucHSTrayView.SlowSignal = m_HS_Lane.SlowSignal;
            ucHSTrayView.ArrivalSignal = m_HS_Lane.ArrivalSignal;
            ucHSTrayView.ForwardSignal = m_HS_Lane.StopperExtendSignal;
            ucHSTrayView.BackwardSignal = m_HS_Lane.StopperRetractSignal;
            ucHSTrayView.bCylinderOn = m_HS_Lane.StopperExtendSignal;
            ucHSTrayView.bTrayExist = m_HS_Lane.HasTrayBill();
            ucHSTrayView.ReflashTimerFunc();
        }

        /// <summary>
        /// 更新 NG Lane 的狀態與 Tray 示意圖，將 Lane 的各種訊號與帳料狀態同步到 ucNGTrayView 元件。
        /// </summary>
        private void UpdateNGLane()
        {
            lblNGLaneStatus.Text = m_NG_Lane.m_enuAction.ToString();

            ucNGTrayView.LoadSignal = m_NG_Lane.LoadSignal;
            ucNGTrayView.SlowSignal = m_NG_Lane.SlowSignal;
            ucNGTrayView.ArrivalSignal = m_NG_Lane.ArrivalSignal;
            ucNGTrayView.ForwardSignal = m_NG_Lane.StopperExtendSignal;
            ucNGTrayView.BackwardSignal = m_NG_Lane.StopperRetractSignal;
            ucNGTrayView.bCylinderOn = m_NG_Lane.StopperExtendSignal;
            ucNGTrayView.bTrayExist = m_NG_Lane.HasTrayBill();
            ucNGTrayView.ReflashTimerFunc();
        }

        #endregion

        /// <summary>
        /// 綁定所有 Proc 物件的 Singleton 參照，快取到本類別的成員變數。
        /// </summary>
        private void BindProcessSingleton()
        {
            #region Magazine
            m_IC_Magazine = Proc_IC_Feed_Magazine.GetSingleton();
            m_OK_Discharge_Magazine = Proc_OK_Discharge_Magazine.GetSingleton();
            m_HS_Feed_Magazine = Proc_HS_Feed_Magazine.GetSingleton();
            m_HS_Discharge_Magazine = Proc_HS_Discharge_Magazine.GetSingleton();
            m_NG_Feed_Magazine = Proc_NG_Feed_Magazine.GetSingleton();
            m_NG_Discharge_Magazine = Proc_NG_Discharge_Magazine.GetSingleton();

            #endregion

            #region Lane
            m_ASM_Lane = Proc_ASM_Lane.GetSingleton();
            m_Press_Lane = Proc_Press_Lane.GetSingleton();
            m_AOI_Lane = Proc_AOI_Lane.GetSingleton();
            m_OK_Lane = Proc_OK_Lane.GetSingleton();
            m_HS_Lane = Proc_HS_Lane.GetSingleton();
            m_NG_Lane = Proc_NG_Lane.GetSingleton();
            #endregion

            #region Arm
            m_ASM_Arm = Proc_ASM_Arm.GetSingleton();
            m_Sort_Arm = Proc_Sort_Arm.GetSingleton();
            #endregion

            m_Press_Station = Proc_Press_Station.GetSingleton();
            m_AOI_Station = Proc_AOI_Station.GetSingleton();
        }
    }
}

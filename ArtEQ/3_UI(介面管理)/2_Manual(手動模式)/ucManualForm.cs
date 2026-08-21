using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArtCommonLib;
using ArtEQ._2_Function_流程_.Proc;
using static ArtData.clsEnum;
using static ArtEQ.BaseMagazine;

namespace ArtEQ._3_UI_介面管理_._2_Manual_手動模式_
{
    public partial class ucManualForm : ucBaseUserControl
    {
        #region Constant

        private const int m_iSlotMax = 5;
        private static ucManualForm m_singleton;
        private static readonly object s_lock = new object();

        #endregion

        #region Fields

        private Dictionary<BaseMagazine, ComboBox> m_MagazineSlotComboBoxes = new Dictionary<BaseMagazine, ComboBox>();
        private Dictionary<string, BaseMagazine> m_Magazines = new Dictionary<string, BaseMagazine>();


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
        #endregion

        #region Constructors

        public ucManualForm()
        {
            InitializeComponent();
            BindProcessSingleton();
            InitializeLaneButtons();
            InitializeMagazineButtons();
            InitializeTrayView();
            InitMagazineView();
            InitializeSlotComboBoxes();
            BindPpStationComboBox();
        }

        #endregion

        #region Public Methods

        public static ucManualForm GetSingleton()
        {
            if (m_singleton == null)
            {
                lock (s_lock)
                {
                    m_singleton = new ucManualForm();
                }

                return m_singleton;
            }

            return m_singleton;
        }

        #endregion

        #region Private Methods

        private void BindPpStationComboBox()
        {
            cboAsmArmPpStation.DataSource = new List<PPStation>
            {
                PPStation.None,
                PPStation.IC,
                PPStation.HeatSink
            };

            cboSortArmPpStation.DataSource = new List<PPStation>
            {
                PPStation.None,
                PPStation.OK,
                PPStation.NG
            };

            cboAsmArmPpStation.SelectedIndex = 0;
            cboSortArmPpStation.SelectedIndex = 0;
        }

        private void InitializeSlotComboBoxes()
        {
            m_MagazineSlotComboBoxes.Clear();
            m_MagazineSlotComboBoxes.Add(m_IC_Magazine, cboICFeedSlot);
            m_MagazineSlotComboBoxes.Add(m_OK_Discharge_Magazine, cboOKDischargeSlot);
            m_MagazineSlotComboBoxes.Add(m_HS_Feed_Magazine, cboHSFeedSlot);
            m_MagazineSlotComboBoxes.Add(m_HS_Discharge_Magazine, cboHSDischargeSlot);
            m_MagazineSlotComboBoxes.Add(m_NG_Feed_Magazine, cboNGFeedSlot);
            m_MagazineSlotComboBoxes.Add(m_NG_Discharge_Magazine, cboNGDischargeSlot);
        }

        /// <summary>
        /// 初始化所有站別的 Tray 盤示意圖，將各站的 TrayInfo 綁定到對應的 ucTrayDisplay 元件。
        /// </summary>
        /// <remarks>
        /// 每個 Tray 元件都必須呼叫 Initial() 才會顯示真實資料；
        /// 若漏掉某個元件的 Initial()，該元件不會報錯，只會靜默顯示預設的 2x3 全 Pending(藍色) 畫面。
        /// </remarks>
        private void InitializeTrayView()
        {
            ucAsmTrayView.Initial(m_ASM_Lane.m_Temp_Tray_Info);
            ucPressTrayView.Initial(m_Press_Lane.m_Temp_Tray_Info);
            ucAOITrayView.Initial(m_AOI_Lane.m_Temp_Tray_Info);
            ucHSTrayView.Initial(m_HS_Lane.m_Temp_Tray_Info);
            ucOKTrayView.Initial(m_OK_Lane.m_Temp_Tray_Info);
            ucNGTrayView.Initial(m_NG_Lane.m_Temp_Tray_Info);
        }

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

        /// <summary>
        /// 初始化 IC Magazine 的料盒示意圖，綁定料盒資訊物件與連動的 Slot 選取 ComboBox。
        /// </summary>
        private void InitMagazineView()
        {
            m_Magazines.Clear();
            m_Magazines.Add("IC_Feed", m_IC_Magazine);
            m_Magazines.Add("OK_Discharge", m_OK_Discharge_Magazine);
            m_Magazines.Add("HS_Feed", m_HS_Feed_Magazine);
            m_Magazines.Add("HS_Discharge", m_HS_Discharge_Magazine);
            m_Magazines.Add("NG_Feed", m_NG_Feed_Magazine);
            m_Magazines.Add("NG_Discharge", m_NG_Discharge_Magazine);

            ucIC_Feed_Magazine_View.Initial(m_IC_Magazine.m_MagazineInfo, cboICFeedSlot);
            ucOK_Discharge_Magazine_View.Initial(m_OK_Discharge_Magazine.m_MagazineInfo, cboOKDischargeSlot);
            ucHS_Feed_Magazine_View.Initial(m_HS_Feed_Magazine.m_MagazineInfo, cboHSFeedSlot);
            ucHS_Discharge_Magazine_View.Initial(m_HS_Discharge_Magazine.m_MagazineInfo, cboHSDischargeSlot);
            ucNG_Feed_Magazine_View.Initial(m_NG_Feed_Magazine.m_MagazineInfo, cboNGFeedSlot);
            ucNG_Discharge_Magazine_View.Initial(m_NG_Discharge_Magazine.m_MagazineInfo, cboNGDischargeSlot);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var nowValue = m_IC_Magazine.PresentSignal;
            m_IC_Magazine.SetDiValue(enuDi.IC_Feed_Mag_Present, !nowValue);
        }

        private void btnAllInit_Click(object sender, EventArgs e)
        {
            foreach (var magazine in m_Magazines.Values)
            {
                magazine.RunInitial();
            }

            m_ASM_Lane.RunInitial();
            m_Press_Lane.RunInitial();
            m_AOI_Lane.RunInitial();
            m_OK_Lane.RunInitial();
            m_HS_Lane.RunInitial();
            m_NG_Lane.RunInitial();

            m_ASM_Arm.RunInitial();
            m_Sort_Arm.RunInitial();

            m_Press_Station.RunInitial();
            m_AOI_Station.RunInitial();

            btnAddData.Enabled = true;
        }

        private void btnAddData_Click(object sender, EventArgs e)
        {
            foreach (var keyValueParir in m_Magazines)
            {
                var magazine = keyValueParir.Value;
                var magazineName = keyValueParir.Key;

                if (magazine == null)
                    continue;

                for (int slotNo = 1; slotNo <= m_iSlotMax; slotNo++)
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

            var log = $"Add Data, {m_IC_Magazine.m_MagazineInfo.GetHashCode().ToString()}, {GetHashCode().ToString()}";
            clsLog.Log(nameof(enuLogName.ProcessLog), log);

            if (m_ASM_Lane == null)
                return;
        }

        private void btnIcMagazineInit_Click(object sender, EventArgs e)
        {
            m_IC_Magazine.RunInitial();
        }

        private void btnIcMagazineLoad_Click(object sender, EventArgs e)
        {
            if (cboICFeedSlot.SelectedItem == null)
                return;

            var slotNo = (int)cboICFeedSlot.SelectedItem;
            m_IC_Magazine.RunLoad(slotNo);
        }

        /// <summary>
        /// 集中設定各流道按鈕（初始化/入料/出料）與對應 Lane 物件的關聯，
        /// 透過 Button.Tag 存放對應的 Lane 實例，並將同類型按鈕統一掛載共用事件
        /// （btnLaneInit_Click / btnLaneLoad_Click / btnLaneUnload_Click），
        /// 避免每新增一條流道就要重複撰寫對應的 Click handler。
        /// 新增流道時，只需在此方法內補上該流道三顆按鈕的 Tag 設定與事件掛載。
        /// </summary>
        private void InitializeLaneButtons()
        {
            // 集中設定「按鈕 -> 對應的Lane物件」，之後加流道只要在這裡加一行

            #region Tag

            btnAsmLaneInit.Tag = m_ASM_Lane;
            btnAsmLoad.Tag = m_ASM_Lane;
            btnASMUnload.Tag = m_ASM_Lane;

            btnPressLaneInit.Tag = m_Press_Lane;
            btnPressLaneLoad.Tag = m_Press_Lane;
            btnPressLaneUnload.Tag = m_Press_Lane;

            btnAOILaneInit.Tag = m_AOI_Lane;
            btnAOILaneLoad.Tag = m_AOI_Lane;
            btnAOILaneUnload.Tag = m_AOI_Lane;

            btnOKLaneInit.Tag = m_OK_Lane;
            btnOKLaneLoad.Tag = m_OK_Lane;
            btnOKLaneUnload.Tag = m_OK_Lane;

            btnHSLaneInit.Tag = m_HS_Lane;
            btnHSLaneLoad.Tag = m_HS_Lane;
            btnHSLaneUnload.Tag = m_HS_Lane;

            btnNGLaneInit.Tag = m_NG_Lane;
            btnNGLaneLoad.Tag = m_NG_Lane;
            btnNGLaneUnload.Tag = m_NG_Lane;

            #endregion

            // 統一掛同一組事件
            btnAsmLaneInit.Click += btnLaneInit_Click;
            btnPressLaneInit.Click += btnLaneInit_Click;
            btnAOILaneInit.Click += btnLaneInit_Click;
            btnOKLaneInit.Click += btnLaneInit_Click;
            btnHSLaneInit.Click += btnLaneInit_Click;
            btnNGLaneInit.Click += btnLaneInit_Click;

            btnAsmLoad.Click += btnLaneLoad_Click;
            btnPressLaneLoad.Click += btnLaneLoad_Click;
            btnAOILaneLoad.Click += btnLaneLoad_Click;
            btnOKLaneLoad.Click += btnLaneLoad_Click;
            btnHSLaneLoad.Click += btnLaneLoad_Click;
            btnNGLaneLoad.Click += btnLaneLoad_Click;

            btnASMUnload.Click += btnLaneUnload_Click;
            btnPressLaneUnload.Click += btnLaneUnload_Click;
            btnAOILaneUnload.Click += btnLaneUnload_Click;
            btnOKLaneUnload.Click += btnLaneUnload_Click;
            btnHSLaneUnload.Click += btnLaneUnload_Click;
            btnNGLaneUnload.Click += btnLaneUnload_Click;
        }

        private void InitializeMagazineButtons()
        {
            // 集中設定「按鈕 -> 對應的Magazine物件」，之後加Magazine只要在這裡加一行

            #region Tag

            btnIcMagazineInit.Tag = m_IC_Magazine;
            btnIcMagazineLoad.Tag = m_IC_Magazine;
            btnHSFeedMagazineInit.Tag = m_HS_Feed_Magazine;
            btnHSFeedMagazineLoad.Tag = m_HS_Feed_Magazine;
            btnHSDischargeMagazineInit.Tag = m_HS_Discharge_Magazine;
            btnHSDischargeMagazineUnload.Tag = m_HS_Discharge_Magazine;
            btnNGFeedMagazineInit.Tag = m_NG_Feed_Magazine;
            btnNGFeedMagazineLoad.Tag = m_NG_Feed_Magazine;
            btnNGDischargeMagazineInit.Tag = m_NG_Discharge_Magazine;
            btnNGDischargeMagazineUnload.Tag = m_NG_Discharge_Magazine;
            btnOKDischargeMagazineInit.Tag = m_OK_Discharge_Magazine;
            btnOKDischargeMagazineUnload.Tag = m_OK_Discharge_Magazine;

            #endregion

            // 統一掛同一組事件
            btnIcMagazineInit.Click += btnMagazineInit_Click;
            btnIcMagazineLoad.Click += btnMagazineLoad_Click;
            btnHSFeedMagazineInit.Click += btnMagazineInit_Click;
            btnHSFeedMagazineLoad.Click += btnMagazineLoad_Click;
            btnHSDischargeMagazineInit.Click += btnMagazineInit_Click;
            btnHSDischargeMagazineUnload.Click += btnMagazineUnload_Click;
            btnNGFeedMagazineInit.Click += btnMagazineInit_Click;
            btnNGFeedMagazineLoad.Click += btnMagazineLoad_Click;
            btnNGDischargeMagazineInit.Click += btnMagazineInit_Click;
            btnNGDischargeMagazineUnload.Click += btnMagazineUnload_Click;
            btnOKDischargeMagazineInit.Click += btnMagazineInit_Click;
            btnOKDischargeMagazineUnload.Click += btnMagazineUnload_Click;
        }

        private void btnLaneInit_Click(object sender, EventArgs e)
        {
            GetLaneFromSender(sender)?.RunInitial();
        }

        private void btnLaneLoad_Click(object sender, EventArgs e)
        {
            GetLaneFromSender(sender)?.RunLoad();
        }

        private void btnLaneUnload_Click(object sender, EventArgs e)
        {
            GetLaneFromSender(sender)?.RunUnload();
        }

        private BaseLane GetLaneFromSender(object sender)
        {
            return (sender as Button)?.Tag as BaseLane;
        }

        private void btnMagazineInit_Click(object sender, EventArgs e)
        {
            GetMagazineFromSender(sender)?.RunInitial();
        }

        private void btnMagazineLoad_Click(object sender, EventArgs e)
        {
            int iSlot = GetSelectedSlotFromSender(sender);
            if (iSlot == 0)
            {
                return;
            }

            GetMagazineFromSender(sender)?.RunLoad(iSlot);
        }

        private void btnMagazineUnload_Click(object sender, EventArgs e)
        {
            int iSlot = GetSelectedSlotFromSender(sender);
            if (iSlot == 0)
            {
                return;
            }

            GetMagazineFromSender(sender)?.RunUnload(iSlot);
        }

        /// <summary>
        /// 依觸發按鈕的 Tag 找出對應的料盒 ComboBox，回傳目前選取的 Slot 編號；
        /// 按鈕無效、找不到對應 ComboBox、或尚未選取時回傳 null。
        /// </summary>
        private int GetSelectedSlotFromSender(object sender)
        {
            Button button = sender as Button;
            if (button == null || button.Tag == null)
            {
                return 0;
            }

            BaseMagazine magazine = button.Tag as BaseMagazine;
            if (magazine == null || !m_MagazineSlotComboBoxes.TryGetValue(magazine, out ComboBox comboBox) || comboBox.SelectedItem == null)
            {
                return 0;
            }

            return (int)comboBox.SelectedItem;
        }

        private BaseMagazine GetMagazineFromSender(object sender)
        {
            return (sender as Button)?.Tag as BaseMagazine;
        }

        #endregion


        #region UI 更新

        /// <summary>
        /// 每次 tmrScan 計時器觸發時，更新所有 Magazine 與 Lane 的狀態與示意圖，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrScan_Tick(object sender, EventArgs e)
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

            btnAddData.Enabled = m_IC_Magazine.m_enuAction == enuAction.Initial_Done;
            btnIcMagazineLoad.Enabled = cboICFeedSlot.SelectedItem != null;
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

        private void btnAsmArmInit_Click(object sender, EventArgs e)
        {
            m_ASM_Arm.RunInitial();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool exist = m_ASM_Arm.AssyRecord.IsExist;
            m_HS_Lane.m_Temp_Tray_Info.SetItemStatus(0, TrayItemStatus.Empty);
        }

        private void btnAsmArmPick_Click(object sender, EventArgs e)
        {
            var column = (int)nudAsmColumn.Value;
            var row = (int)nudAsmRow.Value;
            var selectedStation = (PPStation)cboAsmArmPpStation.SelectedItem;

            if (selectedStation == PPStation.None)
            {
                m_ASM_Arm.RunPick(PPStation.HeatSink, column, row);
            }
            else
            {
                m_ASM_Arm.RunPick(selectedStation, column, row);
            }
        }

        private void btnAsmArmPlace_Click(object sender, EventArgs e)
        {
            var column = (int)nudAsmColumn.Value;
            var row = (int)nudAsmRow.Value;
            var selectedStation = (PPStation)cboAsmArmPpStation.SelectedItem;

            if (selectedStation == PPStation.None)
            {
                m_ASM_Arm.RunPlace(PPStation.IC, column, row);
            }
            else
            {
                m_ASM_Arm.RunPlace(selectedStation, column, row);
            }
        }

        private void btnSortArmInit_Click(object sender, EventArgs e)
        {
            m_Sort_Arm.RunInitial();
        }

        private void btnSortArmPick_Click(object sender, EventArgs e)
        {
            var column = (int)nudSortColumn.Value;
            var row = (int)nudSortRow.Value;
            var selectedStation = (PPStation)cboSortArmPpStation.SelectedItem;

            if (selectedStation == PPStation.None)
            {
                return;
            }
            else
            {
                m_Sort_Arm.RunPick(selectedStation, column, row);
            }
        }

        private void btnSortArmPlace_Click(object sender, EventArgs e)
        {
            var column = (int)nudSortColumn.Value;
            var row = (int)nudSortRow.Value;
            var selectedStation = (PPStation)cboSortArmPpStation.SelectedItem;

            if (selectedStation == PPStation.None)
            {
                return;
            }
            else
            {
                m_Sort_Arm.RunPlace(selectedStation, column, row);
            }
        }

        private void btnPressStationInit_Click(object sender, EventArgs e)
        {
            m_Press_Station.RunInitial();
        }

        private void btnPressWork_Click(object sender, EventArgs e)
        {
            m_Press_Station.RunPress();
        }

        private void btnAoiStationInit_Click(object sender, EventArgs e)
        {
            m_AOI_Station.RunInitial();
        }

        private void btnAOIInspect_Click(object sender, EventArgs e)
        {
            var column = (int)nudAoiColumn.Value;
            var row = (int)nudAoiRow.Value;
            m_AOI_Station.RunInspect(column, row);
        }
    }
}
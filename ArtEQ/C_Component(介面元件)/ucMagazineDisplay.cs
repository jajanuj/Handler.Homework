using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ArtData;

namespace ArtEQ
{
    /// <summary>
    /// 料盒 (Magazine) Slot 示意圖元件。
    /// 版面採用 TableLayoutPanel (左：Slot 固定寬 90px／右：燈號 AutoSize) + 2 個 FlowLayoutPanel，
    /// 由 WinForms 版面引擎自動排列，不再手動計算座標。
    /// 依 MagazineInfo 動態顯示每個 Slot 的空/有料狀態，
    /// 點擊 Slot 可選取 (橘框標示)，並與外部 ComboBox 雙向連動。
    /// </summary>
    public partial class ucMagazineDisplay : UserControl
    {
        #region //=====================  區域變數設置 =====================

        private clsMagazineInfo m_pMagInfo = null;
        private ComboBox m_cboSlotLink = null;
        private readonly List<Button> m_lstSlotBtn = new List<Button>();

        private readonly Color m_colorExist = Color.FromArgb(46, 125, 50);          // 有料 - 深綠
        private readonly Color m_colorEmpty = SystemColors.Control;                  // 空槽 - 系統灰
        private readonly Color m_colorSelectedBorder = Color.FromArgb(255, 140, 0);  // 選中 - 橘色邊框
        private readonly Color m_colorNormalBorder = Color.Silver;

        private const int SLOT_COLUMN_WIDTH = 90; // 左欄 (Slot) 固定寬度
        private const int SLOT_BTN_WIDTH = 84;    // 按鈕實際寬度 (留一點邊界給 Padding)
        private const int SLOT_HEIGHT = 28;
        private const int SLOT_MARGIN_BOTTOM = 1;

        // --- 燈號區 (MagazineType) ---
        private clsEnum.MagazineType m_MagazineType = clsEnum.MagazineType.Load;
        private readonly ucSignalIndicator[] m_arrSignalIndicator = new ucSignalIndicator[4];
        private static readonly string[] SIGNAL_LABELS = { "Present", "Over Press", "Push Fwd", "Push Bwd" };

        private const int SIGNAL_TEXT_WIDTH = 74; // 燈號文字寬度 (足夠放 "Over Press")
        private const int SIGNAL_HEIGHT = 18;
        private const int SIGNAL_MARGIN_BOTTOM = 3;

        // --- 版面容器 ---
        private TableLayoutPanel m_tableLayout;
        private FlowLayoutPanel m_pnlSlots;   // 左欄：Slot 按鈕
        private FlowLayoutPanel m_pnlSignals; // 右欄：燈號

        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucMagazineDisplay()
        {
            InitializeComponent();

            this.MinimumSize = new Size(190, 150);

            BuildLayoutContainers();
            BuildSignalIndicators();
        }

        /// <summary> 建立 TableLayoutPanel (左右兩欄) 與內部的 2 個 FlowLayoutPanel </summary>
        private void BuildLayoutContainers()
        {
            //m_tableLayout = new TableLayoutPanel();
            //m_tableLayout.Dock = DockStyle.Fill;
            //m_tableLayout.ColumnCount = 2;
            //m_tableLayout.RowCount = 1;
            //m_tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, SLOT_COLUMN_WIDTH)); // 左：Slot 固定寬
            //m_tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));                     // 右：燈號 自動寬
            //m_tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            //m_pnlSlots = new FlowLayoutPanel();
            //m_pnlSlots.Dock = DockStyle.Fill;
            //m_pnlSlots.FlowDirection = FlowDirection.TopDown;
            //m_pnlSlots.WrapContents = false;
            //m_pnlSlots.AutoScroll = true;
            //m_pnlSlots.Padding = new Padding(2);

            //m_pnlSignals = new FlowLayoutPanel();
            //m_pnlSignals.AutoSize = true;
            //m_pnlSignals.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //m_pnlSignals.Dock = DockStyle.Fill;
            //m_pnlSignals.FlowDirection = FlowDirection.TopDown;
            //m_pnlSignals.WrapContents = false;
            //m_pnlSignals.Padding = new Padding(4, 2, 2, 2);

            //m_tableLayout.Controls.Add(m_pnlSlots, 0, 0);
            //m_tableLayout.Controls.Add(m_pnlSignals, 1, 0);

            //this.Controls.Add(m_tableLayout);
        }

        /// <summary> 建立 4 個燈號元件 (Present / Over Press / Push Fwd / Push Bwd)，固定建立一次，不隨 Slot 數量重建 </summary>
        private void BuildSignalIndicators()
        {
            for (int i = 0; i < m_arrSignalIndicator.Length; i++)
            {
                ucSignalIndicator ctrl = new ucSignalIndicator();
                ctrl.SignalText = SIGNAL_LABELS[i];
                ctrl.IndicatorSize = 10;
                ctrl.Width = SIGNAL_TEXT_WIDTH;
                ctrl.Height = SIGNAL_HEIGHT;
                ctrl.Margin = new Padding(0, 0, 0, SIGNAL_MARGIN_BOTTOM);

                m_arrSignalIndicator[i] = ctrl;
                //m_pnlSignals.Controls.Add(ctrl);
                flpIndicator.Controls.Add(ctrl);
            }

            UpdateSignalVisibility();
        }

        /// <summary>
        /// 初始化元件，綁定料盒資料物件與要連動的外部 ComboBox
        /// </summary>
        /// <param name="p_MagInfo">料盒資訊物件 (必要)</param>
        /// <param name="cboSlot">要雙向連動的外部 ComboBox，若不需要連動可傳 null</param>
        public void Initial(clsMagazineInfo p_MagInfo, ComboBox cboSlot)
        {
            try
            {
                m_pMagInfo = p_MagInfo;

                if (m_cboSlotLink != null)
                {
                    m_cboSlotLink.SelectedIndexChanged -= cboSlotLink_SelectedIndexChanged;
                }

                m_cboSlotLink = cboSlot;

                if (m_cboSlotLink != null)
                {
                    m_cboSlotLink.SelectedIndexChanged += cboSlotLink_SelectedIndexChanged;
                }

                BuildSlots();
            }
            catch (Exception)
            {
            }
        }

        /// <summary> 依 MagazineInfo 的 Slot 數量動態產生 Slot 按鈕，加入左側 FlowLayoutPanel，並同步 ComboBox 選項 </summary>
        private void BuildSlots()
        {
            //m_pnlSlots.SuspendLayout();
            flpSlot.SuspendLayout();

            foreach (Button btn in m_lstSlotBtn)
            {
                btn.Click -= SlotBtn_Click;
                flpSlot.Controls.Remove(btn);
                btn.Dispose();
            }
            m_lstSlotBtn.Clear();

            if (m_pMagInfo == null)
            {
                flpSlot.ResumeLayout();
                return;
            }

            // 同步 ComboBox 選項 (1 ~ iSlotCount)
            if (m_cboSlotLink != null)
            {
                m_cboSlotLink.SelectedIndexChanged -= cboSlotLink_SelectedIndexChanged;
                m_cboSlotLink.Items.Clear();
                for (int i = 1; i <= m_pMagInfo.m_trayInfo.Count; i++)
                {
                    m_cboSlotLink.Items.Add(i);
                }
                m_cboSlotLink.SelectedIndexChanged += cboSlotLink_SelectedIndexChanged;
            }

            for (int i = 0; i < m_pMagInfo.m_trayInfo.Count; i++)
            {
                Button btn = new Button();
                btn.Tag = i;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 2;
                btn.Font = new Font(this.Font, FontStyle.Bold);
                btn.ForeColor = Color.White;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.UseCompatibleTextRendering = false;
                btn.Padding = new Padding(0);
                btn.Margin = new Padding(0, 0, 0, SLOT_MARGIN_BOTTOM);
                btn.AutoSize = false;
                btn.Width = SLOT_BTN_WIDTH;
                btn.Height = SLOT_HEIGHT;
                btn.Click += SlotBtn_Click;

                m_lstSlotBtn.Add(btn);
                //m_pnlSlots.Controls.Add(btn);
                flpSlot.Controls.Add(btn);
            }

            flpSlot.ResumeLayout();
            RefreshSlots();
        }

        #endregion

        #region //===================== public 屬性設置 =====================

        /// <summary>
        /// 料盒用途類型：Load 只顯示 Present 燈號，Unload 顯示全部 4 個燈號。
        /// </summary>
        [Category("燈號")]
        [Description("料盒用途類型 (Load/Unload)，Load 只顯示 Present，Unload 顯示全部燈號")]
        public clsEnum.MagazineType MagazineType
        {
            get { return m_MagazineType; }
            set
            {
                if (m_MagazineType != value)
                {
                    m_MagazineType = value;
                    UpdateSignalVisibility();
                }
            }
        }

        /// <summary> Present 燈號狀態 </summary>
        [Category("燈號")]
        [Description("Present 燈號 On/Off")]
        public bool Present
        {
            get { return m_arrSignalIndicator[0].On; }
            set { m_arrSignalIndicator[0].On = value; }
        }

        /// <summary> Over Press 燈號狀態 (僅 Unload 模式顯示) </summary>
        [Category("燈號")]
        [Description("Over Press 燈號 On/Off (僅 Unload 模式顯示)")]
        public bool OverPress
        {
            get { return m_arrSignalIndicator[1].On; }
            set { m_arrSignalIndicator[1].On = value; }
        }

        /// <summary> Push Fwd 燈號狀態 (僅 Unload 模式顯示) </summary>
        [Category("燈號")]
        [Description("Push Fwd 燈號 On/Off (僅 Unload 模式顯示)")]
        public bool PushFwd
        {
            get { return m_arrSignalIndicator[2].On; }
            set { m_arrSignalIndicator[2].On = value; }
        }

        /// <summary> Push Bwd 燈號狀態 (僅 Unload 模式顯示) </summary>
        [Category("燈號")]
        [Description("Push Bwd 燈號 On/Off (僅 Unload 模式顯示)")]
        public bool PushBwd
        {
            get { return m_arrSignalIndicator[3].On; }
            set { m_arrSignalIndicator[3].On = value; }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        /// <summary> 定時刷新畫面，依 MagazineInfo 目前狀態重繪每個 Slot (沿用專案 Reflash 慣例) </summary>
        public void ReflashTimerFunc()
        {
            RefreshSlots();
        }

        /// <summary> 若料盒層數在執行期變更，呼叫此方法重建畫面 </summary>
        public void RebuildSlots()
        {
            BuildSlots();
        }

        #endregion

        #region //===================== private 函式設置 =====================

        /// <summary> 依目前 MagazineInfo 狀態重繪所有 Slot 外觀 (顏色/文字/選取框) </summary>
        private void RefreshSlots()
        {
            try
            {
                if (m_pMagInfo == null) { return; }

                for (int i = 0; i < m_lstSlotBtn.Count; i++)
                {
                    Button btn = m_lstSlotBtn[i];

                    // m_trayInfo 使用 1-based key
                    int key = i + 1;
                    bool bExist = m_pMagInfo.m_trayInfo[key].bIsExist;

                    btn.BackColor = bExist ? m_colorExist : m_colorEmpty;
                    btn.ForeColor = bExist ? Color.White : Color.Black;
                    btn.Text = bExist
                        ? string.Format("Slot {0:D2}", i + 1)
                        : string.Format("Slot {0:D2} (Empty)", i + 1);

                    bool bSelected = (i == m_pMagInfo.iSelectedIndex);
                    btn.FlatAppearance.BorderColor = bSelected ? m_colorSelectedBorder : m_colorNormalBorder;
                    btn.FlatAppearance.BorderSize = 2; // 固定粗細，只換顏色，避免切換選取時文字區域跳動
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary> 選取指定 Slot，並依需要同步外部 ComboBox </summary>
        /// <param name="iIndex">要選取的 Slot Index (0-based)</param>
        /// <param name="bSyncCombo">是否要把選取結果同步回 ComboBox (由 Slot 點擊觸發時傳 true，由 ComboBox 觸發時傳 false 避免互相觸發)</param>
        private void SelectSlot(int iIndex, bool bSyncCombo)
        {
            if (m_pMagInfo == null) { return; }
            if (iIndex < 0 || iIndex >= m_pMagInfo.m_trayInfo.Count) { return; }

            m_pMagInfo.iSelectedIndex = iIndex;
            RefreshSlots();

            if (bSyncCombo && m_cboSlotLink != null)
            {
                m_cboSlotLink.SelectedIndexChanged -= cboSlotLink_SelectedIndexChanged;
                if (m_cboSlotLink.Items.Count > iIndex)
                {
                    m_cboSlotLink.SelectedIndex = iIndex;
                }
                m_cboSlotLink.SelectedIndexChanged += cboSlotLink_SelectedIndexChanged;
            }
        }

        /// <summary>
        /// 依 MagazineType 切換燈號顯示：Load 只顯示 Present，Unload 顯示全部。
        /// FlowLayoutPanel 會自動略過 Visible=false 的控制項並重新排列，不用額外處理間距。
        /// </summary>
        private void UpdateSignalVisibility()
        {
            m_arrSignalIndicator[0].Visible = true; // Present 永遠顯示

            bool bShowExtra = (m_MagazineType == clsEnum.MagazineType.Unload);
            for (int i = 1; i < m_arrSignalIndicator.Length; i++)
            {
                m_arrSignalIndicator[i].Visible = bShowExtra;
            }
        }

        #endregion

        #region //===================== 以下為事件處理 =====================

        private void SlotBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn == null) { return; }

                int iIndex = (int)btn.Tag;
                SelectSlot(iIndex, true);
            }
            catch (Exception)
            {
            }
        }

        private void cboSlotLink_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_cboSlotLink == null) { return; }
                SelectSlot(m_cboSlotLink.SelectedIndex, false);
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}
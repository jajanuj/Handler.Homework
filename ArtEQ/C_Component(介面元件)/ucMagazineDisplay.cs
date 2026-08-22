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
        private const int SLOT_BTN_WIDTH = 82;    // 按鈕實際寬度 (留邊界給 Padding)
        private const int SLOT_MARGIN_BOTTOM = 1;

        // 按鈕高度/字型大小不再寫死猜一個固定值——固定 13px/6.5pt 那版實測在 7 格時就已經溢出
        // (WinForms 實際畫出來的按鈕高度比設定值多，導致還是跑出捲軸、最後一格被裁到看不見)。
        // 改成 BuildSlots() 時依 flpSlot 目前實際可視高度、當下真正的 Slot 數現算，
        // 保證不管 Slot 數多少(1~m_iSlotMax)都剛好塞滿可視範圍，不用猜。
        private const int SLOT_MIN_HEIGHT = 12;    // 極端情況(Slot 數很多)的高度下限，避免算出負值或 0
        private const int SLOT_SAFETY_MARGIN = 6;  // 高度計算緩衝，寧可底部留白也不要抓太滿

        // 字型大小改用「7 格時的高度」當基準，不是每次都照當下實際 Slot 數重算——
        // 使用者反饋 7 格時字大小剛好，超過 7 格再繼續縮字就太小了。所以 Slot 數 > 7 時，
        // 字型維持跟 7 格時一樣大，犧牲一點按鈕本身的留白，也不要讓字越縮越小。
        // 按鈕本身的高度(SLOT_MIN_HEIGHT 那組計算)還是照實際 Slot 數縮，才能保證不會冒出捲軸。
        private const int FONT_REFERENCE_SLOT_COUNT = 7;

        // 字型大小用 ComputeFittingFont() 實際量測找最大能塞下的大小，不用「高度 * 比例」猜公式。
        // 上限/下限都調低，量測時扣的安全邊界也加大——這台機器的 DPI 縮放讓「量出來剛好」跟
        // 「實際畫出來剛好」對不上，量測結果本身沒錯，但跟實際渲染的可用空間有系統性落差，
        // 與其再猜一次比例，寧可整批往小調、留更大緩衝。
        private const float SLOT_MIN_FONT_SIZE = 5f;
        private const float SLOT_MAX_FONT_SIZE = 6.5f;
        private const string SLOT_FONT_SAMPLE_TEXT = "Slot 00"; // 量測用樣本文字，跟實際顯示格式等寬

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
                btn.Paint -= SlotBtn_Paint;
                flpSlot.Controls.Remove(btn);
                btn.Dispose();
            }
            m_lstSlotBtn.Clear();

            if (m_pMagInfo == null)
            {
                flpSlot.ResumeLayout();
                return;
            }

            // 只讀一次 Count 存成區域變數，這個方法後面全部用同一個數字——
            // 背景 Proc 執行緒可能同時在重建 m_trayInfo(RunInitial()/InitialSlot())，
            // 如果 ComboBox、按鈕迴圈、高度計算各自重新讀一次 .Count，三個地方有機會讀到不同的瞬間值。
            int iSlotCount = m_pMagInfo.m_trayInfo.Count;

            // 同步 ComboBox 選項 (1 ~ iSlotCount)
            if (m_cboSlotLink != null)
            {
                m_cboSlotLink.SelectedIndexChanged -= cboSlotLink_SelectedIndexChanged;
                m_cboSlotLink.Items.Clear();
                for (int i = 1; i <= iSlotCount; i++)
                {
                    m_cboSlotLink.Items.Add(i);
                }
                m_cboSlotLink.SelectedIndexChanged += cboSlotLink_SelectedIndexChanged;
            }

            // 依 flpSlot 目前實際可視高度、當下真正的 Slot 數現算按鈕高度，不用猜固定值。
            // 這樣不管 Slot 數是多少(1~m_iSlotMax)，這批按鈕本來就會剛好塞滿可視範圍。
            // 扣掉 SLOT_SAFETY_MARGIN 當緩衝：WinForms 實際排版可能跟 ClientSize 算出來的有一點誤差，
            // 寧可讓最下面留一點點空白，也不要抓太滿導致又要靠 AutoScroll 幫忙(AutoScroll 已經關掉，
            // 因為它會在子控制項被點選/取得焦點時自動捲動，反而把最上面的 Slot 推出畫面)。
            int iAvailableHeight = Math.Max(flpSlot.ClientSize.Height - SLOT_SAFETY_MARGIN, 1);
            int iBtnHeight = iSlotCount > 0
                ? Math.Max(SLOT_MIN_HEIGHT, (iAvailableHeight - (iSlotCount - 1) * SLOT_MARGIN_BOTTOM) / iSlotCount)
                : SLOT_MIN_HEIGHT;

            // 字型大小用「7 格時的高度」當基準，Slot 數超過 7 時字型不再跟著縮小(見上面常數註解)。
            int iFontRefSlotCount = Math.Min(Math.Max(iSlotCount, 1), FONT_REFERENCE_SLOT_COUNT);
            int iFontRefBtnHeight = Math.Max(SLOT_MIN_HEIGHT, (iAvailableHeight - (iFontRefSlotCount - 1) * SLOT_MARGIN_BOTTOM) / iFontRefSlotCount);
            Font fontSlot = ComputeFittingFont(SLOT_BTN_WIDTH, iFontRefBtnHeight);

            for (int i = 0; i < iSlotCount; i++)
            {
                Button btn = new Button();
                btn.Tag = i;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.Font = fontSlot;
                btn.ForeColor = Color.White;
                // 文字不交給 Button 內建的 TextAlign 排版——GDI 的預設文字置中會保留一段固定的
                // 內部留白(不會隨控制項變矮而跟著縮小)，按鈕矮到這個程度時，留白佔比就會很明顯，
                // 看起來像文字沒有真的置中。改成 btn.Text 留空、自己在 SlotBtn_Paint 用
                // TextFormatFlags.NoPadding 手動精準置中，不受這個固定留白影響。
                btn.Padding = new Padding(0);
                btn.Margin = new Padding(0, 0, 0, SLOT_MARGIN_BOTTOM);
                btn.AutoSize = false;
                btn.Width = SLOT_BTN_WIDTH;
                btn.Height = iBtnHeight;
                btn.Click += SlotBtn_Click;
                btn.Paint += SlotBtn_Paint;

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
            // Slot 數量可能因為 Recipe 換了新值、重新 Init 而改變(m_trayInfo 被 InitialSlot() 整個重建)。
            // 按鈕顆數跟資料筆數對不上時要整批重建，不能只靠 RefreshSlots() 重畫既有按鈕的顏色/文字，
            // 不然畫面按鈕顆數永遠停在上一次 BuildSlots() 當下的舊數量，不會跟著新 Slot 數增減。
            if (m_pMagInfo != null && m_lstSlotBtn.Count != m_pMagInfo.m_trayInfo.Count)
            {
                BuildSlots();
                return;
            }

            RefreshSlots();
        }

        /// <summary> 若料盒層數在執行期變更，呼叫此方法重建畫面 </summary>
        public void RebuildSlots()
        {
            BuildSlots();
        }

        #endregion

        #region //===================== private 函式設置 =====================

        /// <summary>
        /// 從 SLOT_MAX_FONT_SIZE 往下量測，找出樣本文字能完整塞進按鈕範圍(扣一點邊界)的最大字型大小。
        /// 用 TextRenderer.MeasureText() 實際量測，不是用比例公式猜——不同 DPI / 佈景主題下，
        /// 按鈕實際可用文字區域跟猜測公式常常對不上，之前用「高度 * 比例」的版本在使用者那邊還是被裁到。
        /// </summary>
        private Font ComputeFittingFont(int p_iBtnWidth, int p_iBtnHeight)
        {
            int iMaxWidth = Math.Max(p_iBtnWidth - 10, 1);
            int iMaxHeight = Math.Max(p_iBtnHeight - 8, 1);

            for (float fSize = SLOT_MAX_FONT_SIZE; fSize >= SLOT_MIN_FONT_SIZE; fSize -= 0.5f)
            {
                using (Font fontTry = new Font(this.Font.FontFamily, fSize, FontStyle.Regular))
                {
                    Size sz = TextRenderer.MeasureText(SLOT_FONT_SAMPLE_TEXT, fontTry);
                    if (sz.Width <= iMaxWidth && sz.Height <= iMaxHeight)
                    {
                        return new Font(this.Font.FontFamily, fSize, FontStyle.Regular);
                    }
                }
            }

            return new Font(this.Font.FontFamily, SLOT_MIN_FONT_SIZE, FontStyle.Regular);
        }

        /// <summary> 依目前 MagazineInfo 狀態重繪所有 Slot 外觀 (顏色/文字/選取框) </summary>
        private void RefreshSlots()
        {
            if (m_pMagInfo == null) { return; }

            for (int i = 0; i < m_lstSlotBtn.Count; i++)
            {
                try
                {
                    Button btn = m_lstSlotBtn[i];

                    // m_trayInfo 使用 1-based key。用 TryGetValue 不用索引子直接取——
                    // 背景 Proc 執行緒可能同時在重建 m_trayInfo(RunInitial()/InitialSlot())，
                    // 這一格暫時取不到帳就先跳過，等下一次 Reflash 再補上，不要讓單一格失敗
                    // 拖累到 for 迴圈提早中斷、後面的格子跟著全部沒畫到(這是之前把 try/catch
                    // 包在整個迴圈外面、'最後一格空白' 那個問題的成因)。
                    int key = i + 1;
                    clsTrayInfo tray;
                    if (!m_pMagInfo.m_trayInfo.TryGetValue(key, out tray) || tray == null)
                    {
                        continue;
                    }

                    bool bExist = tray.bIsExist;

                    btn.BackColor = bExist ? m_colorExist : m_colorEmpty;
                    // ForeColor 決定 SlotBtn_Paint 畫文字的顏色；文字內容本身也是 SlotBtn_Paint
                    // 依 btn.Tag 現算的，這裡不用再設 btn.Text(見 BuildSlots() 的說明)。
                    btn.ForeColor = bExist ? Color.White : Color.Black;

                    bool bSelected = (i == m_pMagInfo.iSelectedIndex);
                    btn.FlatAppearance.BorderColor = bSelected ? m_colorSelectedBorder : m_colorNormalBorder;
                    btn.FlatAppearance.BorderSize = 1; // 固定粗細，只換顏色，避免切換選取時文字區域跳動
                    btn.Invalidate();
                }
                catch (Exception)
                {
                    // 單一格重繪失敗不影響其他格繼續畫。
                }
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

        /// <summary>
        /// 手動畫 Slot 文字，取代 Button 內建的 TextAlign 置中。
        /// GDI 文字置中預設會保留一段固定的內部留白，不會隨控制項變矮而跟著縮小，按鈕矮到
        /// 這個程度時留白比例會很明顯；改成自己用 TextFormatFlags.NoPadding 量出實際文字範圍、
        /// 手動算出正中間的座標再畫，不受那段固定留白影響。
        /// </summary>
        private void SlotBtn_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn == null || !(btn.Tag is int)) { return; }

                int iIndex = (int)btn.Tag;
                string sText = string.Format("Slot {0:D2}", iIndex + 1);

                const TextFormatFlags flags = TextFormatFlags.HorizontalCenter
                    | TextFormatFlags.VerticalCenter
                    | TextFormatFlags.NoPadding
                    | TextFormatFlags.SingleLine;

                TextRenderer.DrawText(e.Graphics, sText, btn.Font, btn.ClientRectangle, btn.ForeColor, flags);
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
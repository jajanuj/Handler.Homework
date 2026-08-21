using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ArtEQ
{
    /// <summary>
    /// 訊號指示燈元件：左側方塊顯示 On(綠)/Off(灰) 狀態，右側顯示文字說明。
    /// 使用方式：外部直接設定 <see cref="On"/> 屬性即可 (例如 Timer Tick 裡：ctrl.On = xxx;)，設定後會自動重繪。
    /// </summary>
    public partial class ucSignalIndicator : UserControl
    {
        #region //=====================  區域變數設置 =====================

        private bool m_bOn = false;
        private string m_strSignalText = "Signal Text";

        private readonly Color m_colorOn = Color.FromArgb(76, 175, 80);   // On - 綠色
        private readonly Color m_colorOff = Color.FromArgb(158, 158, 158); // Off - 灰色
        private readonly Color m_colorBorder = Color.FromArgb(100, 100, 100);

        private int m_iIndicatorSize = 10;
        private const int INDICATOR_SIZE_MIN = 10;
        private const int INDICATOR_SIZE_MAX = 30;

        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucSignalIndicator()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);
        }

        #endregion

        #region //===================== public 屬性設置 =====================

        /// <summary> 訊號目前 On/Off 狀態，設定後會自動重繪 </summary>
        [Category("訊號狀態")]
        [Description("訊號目前 On/Off 狀態")]
        [DefaultValue(false)]
        public bool On
        {
            get { return m_bOn; }
            set
            {
                if (m_bOn != value)
                {
                    m_bOn = value;
                    Invalidate();
                }
            }
        }

        /// <summary> 顯示於指示燈右側的說明文字，可在 Designer 或執行期設定 </summary>
        [Category("訊號狀態")]
        [Description("顯示於指示燈右側的文字")]
        [DefaultValue("Signal Text")]
        public string SignalText
        {
            get { return m_strSignalText; }
            set
            {
                m_strSignalText = value ?? string.Empty;
                Invalidate();
            }
        }

        /// <summary> 指示燈方塊大小，範圍限制 10~30px，超出範圍會自動夾在邊界內；預設 10 </summary>
        [Category("外觀")]
        [Description("指示燈方塊大小 (10~30 px)")]
        [DefaultValue(10)]
        public int IndicatorSize
        {
            get { return m_iIndicatorSize; }
            set
            {
                int iClamped = Math.Max(INDICATOR_SIZE_MIN, Math.Min(INDICATOR_SIZE_MAX, value));
                if (m_iIndicatorSize != iClamped)
                {
                    m_iIndicatorSize = iClamped;
                    Invalidate();
                }
            }
        }

        #endregion

        #region //===================== 以下為繪圖處理 =====================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            try
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                int iDrawSize = Math.Min(m_iIndicatorSize, Math.Max(Height - 4, INDICATOR_SIZE_MIN));
                int iTop = (Height - iDrawSize) / 2;
                Rectangle rectIndicator = new Rectangle(2, iTop, iDrawSize, iDrawSize);

                Color colorFill = m_bOn ? m_colorOn : m_colorOff;
                using (SolidBrush brush = new SolidBrush(colorFill))
                {
                    e.Graphics.FillRectangle(brush, rectIndicator);
                }
                using (Pen pen = new Pen(m_colorBorder, 1))
                {
                    e.Graphics.DrawRectangle(pen, rectIndicator);
                }

                // 字體大小改以控制項 Height 為基準 (而非跟著 10px 小方塊)，讓文字比方塊明顯大一點、更好辨識
                //float fFontSize = Math.Max(9f, (Height - 2) * 0.85f);
                float fFontSize = 10;
                using (Font fontDraw = new Font(this.Font.FontFamily, fFontSize, this.Font.Style, GraphicsUnit.Pixel))
                {
                    int iTextLeft = rectIndicator.Right + 6;
                    Rectangle rectText = new Rectangle(iTextLeft, 0, Math.Max(Width - iTextLeft, 0), Height);
                    TextRenderer.DrawText(
                        e.Graphics,
                        m_strSignalText,
                        fontDraw,
                        rectText,
                        ForeColor,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
                }
            }
            catch (Exception)
            {
                if (DesignMode) { return; } // 設計期發生的例外通常不影響實際執行，不記錄避免干擾 Designer
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            Invalidate();
        }

        #endregion
    }
}

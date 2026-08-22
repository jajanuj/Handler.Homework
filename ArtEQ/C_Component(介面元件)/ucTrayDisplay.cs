using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ArtData;

namespace ArtEQ
{
    /// <summary>
    /// Tray 盤示意圖元件：上方訊號列 (Load/Slow/Arrival/Unload)、中間畫布 (綠色流道＋Tray 盤格子＋右側擋料氣缸)、下方訊號列 (Forward/Backward)。
    /// 版面由 Designer 的 TableLayoutPanel (tlpMain) 管理：第 1 列＝flpLaneSignal，第 2 列(Percent 100%)＝pnlCanvas，第 3 列＝flpStopper。
    /// </summary>
    public partial class ucTrayDisplay : UserControl
    {
        #region //=====================  區域變數設置 =====================

        private int m_iRows = 2;
        private int m_iCols = 3;
        private bool m_bTrayExist = true;
        private bool m_bCylinderOn = true;

        private clsTrayInfo m_pTrayInfo = null;

        private readonly Color m_colorLane = Color.FromArgb(46, 139, 87);        // 綠色流道
        private readonly Color m_colorTrayBody = Color.White;
        private readonly Color m_colorTrayBorder = Color.FromArgb(90, 90, 190);  // 紫藍色邊框
        private readonly Color m_colorCylinderFill = Color.FromArgb(160, 160, 160);
        private readonly Color m_colorCylinderBorder = Color.Black;

        private readonly Color m_colorPendingFill = Color.FromArgb(91, 155, 213);   // 未執行 - 藍
        private readonly Color m_colorPendingBorder = Color.FromArgb(46, 95, 138);
        private readonly Color m_colorOKFill = Color.FromArgb(76, 175, 80);         // OK - 綠
        private readonly Color m_colorOKBorder = Color.FromArgb(46, 125, 50);
        private readonly Color m_colorNGFill = Color.FromArgb(244, 67, 54);         // NG - 紅
        private readonly Color m_colorNGBorder = Color.FromArgb(183, 28, 28);
        private readonly Color m_colorEmptyFill = Color.FromArgb(158, 158, 158);    // Empty - 灰 (跟空槽一致)
        private readonly Color m_colorEmptyBorder = Color.FromArgb(97, 97, 97);
        private readonly Color m_colorAssemblyFill = Color.FromArgb(255, 152, 0);   // Assembly - 橘 (進行中)
        private readonly Color m_colorAssemblyBorder = Color.FromArgb(230, 81, 0);
        private readonly Color m_colorSubstrateFill = Color.FromArgb(103, 58, 183); // Substrate - 藍紫
        private readonly Color m_colorSubstrateBorder = Color.FromArgb(69, 39, 160);
        private readonly Color m_colorHeatSinkFill = Color.FromArgb(0, 188, 212);   // HeatSink - 青
        private readonly Color m_colorHeatSinkBorder = Color.FromArgb(0, 131, 143);
        private readonly Color m_colorPressedFill = Color.FromArgb(237, 221, 95);    // Pressed - 棕黃
        private readonly Color m_colorPressedBorder = Color.FromArgb(78, 52, 46);
        private readonly Color m_colorAoiInspectedFill = Color.FromArgb(233, 30, 99);   // AoiInspected - 粉紅
        private readonly Color m_colorAoiInspectedBorder = Color.FromArgb(173, 20, 87);

        private const int LANE_HEIGHT = 14;
        private const int CYLINDER_WIDTH = 16;
        private const int MARGIN = 4;
        private const int TRAY_OVERLAP = 4;
        private const int GRID_PADDING = 4;
        private const int GRID_GAP = 3;
        private const int CORNER_RADIUS = 6;

        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucTrayDisplay()
        {
            InitializeComponent();
            UpdateSignalVisibility();
        }

        #endregion

        #region //===================== public 屬性設置 =====================

        /// <summary> Tray 盤列數 (Rows)，可於 Designer 或執行期設定 </summary>
        [Category("Tray 版面")]
        [Description("Tray 盤列數 (Rows)")]
        [DefaultValue(2)]
        public int iRows
        {
            get { return m_iRows; }
            set
            {
                int iClamped = Math.Max(1, value);
                if (m_iRows != iClamped)
                {
                    m_iRows = iClamped;
                    pnlCanvas.Invalidate();
                }
            }
        }

        /// <summary> Tray 盤欄數 (Cols)，可於 Designer 或執行期設定 </summary>
        [Category("Tray 版面")]
        [Description("Tray 盤欄數 (Cols)")]
        [DefaultValue(3)]
        public int iCols
        {
            get { return m_iCols; }
            set
            {
                int iClamped = Math.Max(1, value);
                if (m_iCols != iClamped)
                {
                    m_iCols = iClamped;
                    pnlCanvas.Invalidate();
                }
            }
        }

        /// <summary> 是否有 Tray 盤存在，false 時只顯示上下綠色流道與右側氣缸 </summary>
        [Category("Tray 狀態")]
        [Description("是否有 Tray 盤存在")]
        [DefaultValue(true)]
        public bool bTrayExist
        {
            get { return m_bTrayExist; }
            set
            {
                if (m_bTrayExist != value)
                {
                    m_bTrayExist = value;
                    pnlCanvas.Invalidate();
                }
            }
        }

        /// <summary> 擋料氣缸是否顯示 (true=灰黑色顯示, false=不顯示) </summary>
        [Category("Tray 狀態")]
        [Description("擋料氣缸是否顯示")]
        [DefaultValue(true)]
        public bool bCylinderOn
        {
            get { return m_bCylinderOn; }
            set
            {
                if (m_bCylinderOn != value)
                {
                    m_bCylinderOn = value;
                    pnlCanvas.Invalidate();
                }
            }
        }

        /// <summary> 是否使用出料信號 (true=使用, false=不使用)，只有 Unload 燈號受此控制，其餘訊號永遠顯示 </summary>
        [Category("Tray 狀態")]
        [Description("是否使用出料信號")]
        public bool UseUnloadSignal { get; set; }

        /// <summary> Load 燈號狀態 </summary>
        [Category("燈號")]
        [Description("Load 燈號 On/Off")]
        public bool LoadSignal
        {
            get { return ucSignalLoad.On; }
            set { ucSignalLoad.On = value; }
        }

        /// <summary> Slow 燈號狀態 </summary>
        [Category("燈號")]
        [Description("Slow 燈號 On/Off")]
        public bool SlowSignal
        {
            get { return ucSignalSlow.On; }
            set { ucSignalSlow.On = value; }
        }

        /// <summary> Arrival 燈號狀態 </summary>
        [Category("燈號")]
        [Description("Arrival 燈號 On/Off")]
        public bool ArrivalSignal
        {
            get { return ucSignalArrival.On; }
            set { ucSignalArrival.On = value; }
        }

        /// <summary> Unload 燈號狀態 (顯示與否受 UseUnloadSignal 控制) </summary>
        [Category("燈號")]
        [Description("Unload 燈號 On/Off")]
        public bool UnloadSignal
        {
            get { return ucSignalUnload.On; }
            set { ucSignalUnload.On = value; }
        }

        /// <summary> Forward 燈號狀態 </summary>
        [Category("燈號")]
        [Description("Forward 燈號 On/Off")]
        public bool ForwardSignal
        {
            get { return ucSignalForward.On; }
            set { ucSignalForward.On = value; }
        }

        /// <summary> Backward 燈號狀態 </summary>
        [Category("燈號")]
        [Description("Backward 燈號 On/Off")]
        public bool BackwardSignal
        {
            get { return ucSignalBackward.On; }
            set { ucSignalBackward.On = value; }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        /// <summary> 綁定 TrayInfo，並依 TrayInfo 目前的 Rows/Cols 同步版面 </summary>
        public void Initial(clsTrayInfo pTrayInfo)
        {
            m_pTrayInfo = pTrayInfo;
            if (m_pTrayInfo != null)
            {
                iRows = m_pTrayInfo.iRows;
                iCols = m_pTrayInfo.iCols;
            }
            pnlCanvas.Invalidate();
        }

        /// <summary> 定時刷新，依 TrayInfo 目前狀態重繪 (沿用專案 Reflash 慣例) </summary>
        public void ReflashTimerFunc()
        {
            // 格數(欄/列)可能因為 Recipe 換了新值、綁定的 Tray 換了一顆新的而改變，
            // Initial() 只在綁定當下同步一次，這裡每次刷新都要重新同步，
            // 不然畫面永遠停在 Initial() 當下那一刻的舊格數，不會跟著新 Tray 更新。
            if (m_pTrayInfo != null)
            {
                iRows = m_pTrayInfo.iRows;
                iCols = m_pTrayInfo.iCols;
            }

            pnlCanvas.Invalidate();
        }

        #endregion

        #region //===================== private 函式設置 =====================

        /// <summary> Unload 燈號顯示與否，由 UseUnloadSignal 控制；其餘 5 個燈號永遠顯示 </summary>
        private void UpdateSignalVisibility()
        {
            ucSignalUnload.Visible = UseUnloadSignal;
        }

        #endregion

        #region //===================== 以下為繪圖處理 =====================

        /// <summary> pnlCanvas 的 Paint 事件 (事件註冊於 Designer.cs)，繪製流道／Tray 盤／擋料氣缸 </summary>
        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                int iWidth = pnlCanvas.Width;
                int iHeight = pnlCanvas.Height;

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // --- 上下綠色流道 (永遠顯示，貫穿全寬) ---
                Rectangle rectLaneTop = new Rectangle(0, 0, iWidth, LANE_HEIGHT);
                Rectangle rectLaneBottom = new Rectangle(0, iHeight - LANE_HEIGHT, iWidth, LANE_HEIGHT);
                using (SolidBrush brushLane = new SolidBrush(m_colorLane))
                {
                    e.Graphics.FillRectangle(brushLane, rectLaneTop);
                    e.Graphics.FillRectangle(brushLane, rectLaneBottom);
                }

                int iCylinderAreaWidth = m_bCylinderOn ? (CYLINDER_WIDTH + MARGIN) : 0;

                // --- Tray 盤本體 ---
                if (m_bTrayExist)
                {
                    Rectangle rectTray = new Rectangle(
                        MARGIN,
                        LANE_HEIGHT - TRAY_OVERLAP,
                        Math.Max(iWidth - MARGIN * 2 - iCylinderAreaWidth, 10),
                        Math.Max(iHeight - 2 * (LANE_HEIGHT - TRAY_OVERLAP), 10));

                    using (GraphicsPath path = GetRoundedRectPath(rectTray, CORNER_RADIUS))
                    using (SolidBrush brushBody = new SolidBrush(m_colorTrayBody))
                    using (Pen penBorder = new Pen(m_colorTrayBorder, 2))
                    {
                        e.Graphics.FillPath(brushBody, path);
                        e.Graphics.DrawPath(penBorder, path);
                    }

                    DrawGrid(e.Graphics, rectTray);
                }

                // --- 右側擋料氣缸 ---
                if (m_bCylinderOn)
                {
                    Rectangle rectCylinder = new Rectangle(
                        iWidth - MARGIN - CYLINDER_WIDTH,
                        LANE_HEIGHT - TRAY_OVERLAP,
                        CYLINDER_WIDTH,
                        Math.Max(iHeight - 2 * (LANE_HEIGHT - TRAY_OVERLAP), 10));

                    using (GraphicsPath path = GetRoundedRectPath(rectCylinder, CORNER_RADIUS))
                    using (SolidBrush brushCyl = new SolidBrush(m_colorCylinderFill))
                    using (Pen penCylBorder = new Pen(m_colorCylinderBorder, 2))
                    {
                        e.Graphics.FillPath(brushCyl, path);
                        e.Graphics.DrawPath(penCylBorder, path);
                    }
                }
            }
            catch (Exception)
            {
                if (this.DesignMode) { return; }
            }
        }

        /// <summary> 繪製 Tray 盤內的 Rows x Cols 格子 </summary>
        private void DrawGrid(Graphics g, Rectangle rectTray)
        {
            Rectangle rectGrid = Rectangle.Inflate(rectTray, -GRID_PADDING, -GRID_PADDING);
            if (rectGrid.Width <= 0 || rectGrid.Height <= 0) { return; }

            float fCellWidth = (rectGrid.Width - GRID_GAP * (m_iCols - 1)) / (float)m_iCols;
            float fCellHeight = (rectGrid.Height - GRID_GAP * (m_iRows - 1)) / (float)m_iRows;
            if (fCellWidth <= 0 || fCellHeight <= 0) { return; }

            using (Font fontNum = new Font(this.Font.FontFamily, Math.Max(8f, fCellHeight * 0.4f), FontStyle.Bold))
            {
                for (int iRow = 0; iRow < m_iRows; iRow++)
                {
                    for (int iCol = 0; iCol < m_iCols; iCol++)
                    {
                        int iIndex = iRow * m_iCols + iCol;

                        float fX = rectGrid.Left + iCol * (fCellWidth + GRID_GAP);
                        float fY = rectGrid.Top + iRow * (fCellHeight + GRID_GAP);
                        RectangleF rectCell = new RectangleF(fX, fY, fCellWidth, fCellHeight);

                        clsEnum.TrayItemStatus status = (m_pTrayInfo != null)
                            ? m_pTrayInfo.GetItemStatus(iIndex)
                            : clsEnum.TrayItemStatus.Pending;

                        Color colorFill, colorBorder;
                        GetStatusColor(status, out colorFill, out colorBorder);

                        using (SolidBrush brushCell = new SolidBrush(colorFill))
                        using (Pen penCell = new Pen(colorBorder, 2))
                        {
                            g.FillRectangle(brushCell, rectCell);
                            g.DrawRectangle(penCell, rectCell.X, rectCell.Y, rectCell.Width, rectCell.Height);
                        }

                        TextRenderer.DrawText(
                            g,
                            (iIndex + 1).ToString(),
                            fontNum,
                            Rectangle.Round(rectCell),
                            Color.Black,
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }
                }
            }
        }

        /// <summary> 依狀態取得對應的填色與邊框顏色 </summary>
        private void GetStatusColor(clsEnum.TrayItemStatus status, out Color colorFill, out Color colorBorder)
        {
            switch (status)
            {
                case clsEnum.TrayItemStatus.OK:
                    colorFill = m_colorOKFill;
                    colorBorder = m_colorOKBorder;
                    break;
                case clsEnum.TrayItemStatus.NG:
                    colorFill = m_colorNGFill;
                    colorBorder = m_colorNGBorder;
                    break;
                case clsEnum.TrayItemStatus.Empty:
                    colorFill = m_colorEmptyFill;
                    colorBorder = m_colorEmptyBorder;
                    break;
                case clsEnum.TrayItemStatus.Assembly:
                    colorFill = m_colorAssemblyFill;
                    colorBorder = m_colorAssemblyBorder;
                    break;
                case clsEnum.TrayItemStatus.Substrate:
                    colorFill = m_colorSubstrateFill;
                    colorBorder = m_colorSubstrateBorder;
                    break;
                case clsEnum.TrayItemStatus.HeatSink:
                    colorFill = m_colorHeatSinkFill;
                    colorBorder = m_colorHeatSinkBorder;
                    break;
                case clsEnum.TrayItemStatus.Pressed:
                    colorFill = m_colorPressedFill;
                    colorBorder = m_colorPressedBorder;
                    break;
                case clsEnum.TrayItemStatus.AoiInspected:
                    colorFill = m_colorAoiInspectedFill;
                    colorBorder = m_colorAoiInspectedBorder;
                    break;
                default:
                    colorFill = m_colorPendingFill;
                    colorBorder = m_colorPendingBorder;
                    break;
            }
        }

        /// <summary> 建立圓角矩形路徑 </summary>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int iRadius)
        {
            GraphicsPath path = new GraphicsPath();
            int iDiameter = iRadius * 2;

            path.AddArc(rect.X, rect.Y, iDiameter, iDiameter, 180, 90);
            path.AddArc(rect.Right - iDiameter, rect.Y, iDiameter, iDiameter, 270, 90);
            path.AddArc(rect.Right - iDiameter, rect.Bottom - iDiameter, iDiameter, iDiameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - iDiameter, iDiameter, iDiameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        #endregion
    }

    /// <summary>
    /// 具雙緩衝的 Panel，專用於 Tray 示意圖的畫布區域 (放在 tlpMain 第二列)，避免閃爍。
    /// </summary>
    public class DrawPanel : Panel
    {
        public DrawPanel()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);
        }
    }
}
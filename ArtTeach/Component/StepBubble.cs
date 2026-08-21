using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Threading;

namespace ArtTeach
{
    public partial class StepBubble : UserControl
    {
        Bitmap bmpList;
        Graphics gList;
        SolidBrush m_BrushBubbleColor;
        SolidBrush m_BrushSelectedBubbleColor;
        SolidBrush m_BrushFrameColor;
        SolidBrush m_BrushMouseOverColor;
        SolidBrush m_BrushMouseDownColor;
        SolidBrush m_BrushText;
        SolidBrush tmpBrush;
        SolidBrush m_BrushShadow1;
        SolidBrush m_BrushShadow2;
        Pen m_PenSelected;
        ToolTip toolTip = new ToolTip();

        private bool bIsMouseEnter = false;
        private bool bIsMouseClickCheck = false;
        private bool bIsMouseDown = false;
        private float fSelectedRatio = 1.0f;
        private int iMouseY = 0;
        private int iMouseX = 0;
        private int iMouseDownX = 0;
        private int iMouseDownY = 0;
        private int tmpSelectedIndex = 0;
        private List<string> _listBubbleString = new List<string>();

        public int SelectedIndex
        {
            get;
            private set;
        }

        public enum UIStyle
        {
            Horizontal,
            Vertical,
        }

        public enum BubbleStringStyle
        {
            None,
            ToolTip,
            DirectShow,
        }

        /// <summary>List項目</summary>
        [Description("List項目")]
        [Category("User define"), Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design", typeof(UITypeEditor))]
        public List<string> _BubbleString
        {
            get
            {
                return _listBubbleString;
            }
            set
            {
                _listBubbleString = value;
            }
        }

        [Description("泡泡數")]
        [Category("User define"), Browsable(true)]
        public int _BubbleCount
        {
            get;
            set;
        }

        [Description("泡泡顏色")]
        [Category("User define"), Browsable(true)]
        public Color _BubbleColor
        {
            get;
            set;
        }

        [Description("外框顏色")]
        [Category("User define"), Browsable(true)]
        public Color _FrameColor
        {
            get;
            set;
        }

        [Description("選擇顏色")]
        [Category("User define"), Browsable(true)]
        public Color _SelectedBubbleColor
        {
            get;
            set;
        }

        [Description("選擇顏色")]
        [Category("User define"), Browsable(true)]
        public Color _SelectedFrameColor
        {
            get;
            set;
        }

        [Description("滑鼠經過顏色")]
        [Category("User define"), Browsable(true)]
        public Color _MouseOverColor
        {
            get;
            set;
        }

        [Description("滑鼠按下顏色")]
        [Category("User define"), Browsable(true)]
        public Color _MouseDownColor
        {
            get;
            set;
        }

        [Description("選擇泡泡比例")]
        [Category("User define"), Browsable(true)]
        public float _SelectedRatio
        {
            get
            {
                return fSelectedRatio;
            }
            set
            {
                if (value < 1.0f)
                {
                    fSelectedRatio = 1.0f;
                }
                else
                {
                    fSelectedRatio = value;
                }
            }
        }

        [Description("文字顏色")]
        [Category("User define"), Browsable(true)]
        public Color _TextColor
        {
            get;
            set;
        }

        [Description("顯示類型")]
        [Category("User define"), Browsable(true)]
        public UIStyle _Style
        {
            get;
            set;
        }

        [Description("顯示陰影")]
        [Category("User define"), Browsable(true)]
        public bool _Shadow
        {
            get;
            set;
        }

        [Description("顯示ToolTip")]
        [Category("User define"), Browsable(true)]
        public BubbleStringStyle _BubbleStringStyle
        {
            get;
            set;
        }

        public delegate void BubbleClickDelegate(object sender, EventArgs e); // 自定一個委派
        public event BubbleClickDelegate BubbleClick; //使用此委派宣告event

        public StepBubble()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void DrawBubble(bool p_forceReflash)
        {
            if (p_forceReflash)
            {
                //建立外型                
                pbCanvas.Width = this.Width;
                pbCanvas.Height = this.Height;
                pbCanvas.Location = new Point(0, 0);

                if (pbCanvas.Location.Y < this.Height - pbCanvas.Height)
                {
                    pbCanvas.Location = new Point(0, this.Height - pbCanvas.Height);
                }
                else if (pbCanvas.Location.Y > 0)
                {
                    pbCanvas.Location = new Point(0, 0);
                }

                try
                {
                    if (bmpList.Height != pbCanvas.Height)
                    {
                        m_BrushBubbleColor = new SolidBrush(_BubbleColor);
                        m_BrushSelectedBubbleColor = new SolidBrush(_SelectedBubbleColor);
                        m_BrushFrameColor = new SolidBrush(_FrameColor);
                        m_BrushMouseOverColor = new SolidBrush(_MouseOverColor);
                        m_BrushMouseDownColor = new SolidBrush(_MouseDownColor);
                        m_BrushText = new SolidBrush(_TextColor);
                        m_BrushShadow1 = new SolidBrush(Color.FromArgb(128, 128, 128, 128));
                        m_BrushShadow2 = new SolidBrush(Color.FromArgb(192, 192, 192, 192));
                        m_PenSelected = new Pen(_SelectedFrameColor);

                        bmpList = new Bitmap(pbCanvas.Width, pbCanvas.Height);
                        GC.Collect();
                    }
                }
                catch
                {
                    if (pbCanvas.Width == 0)
                    {
                        pbCanvas.Width = 100;
                    }
                    if (pbCanvas.Height == 0)
                    {
                        pbCanvas.Height = 100;
                    }
                    m_BrushBubbleColor = new SolidBrush(_BubbleColor);
                    m_BrushSelectedBubbleColor = new SolidBrush(_SelectedBubbleColor);
                    m_BrushFrameColor = new SolidBrush(_FrameColor);
                    m_BrushMouseOverColor = new SolidBrush(_MouseOverColor);
                    m_BrushMouseDownColor = new SolidBrush(_MouseDownColor);
                    m_BrushText = new SolidBrush(_TextColor);
                    m_BrushShadow1 = new SolidBrush(Color.FromArgb(128, 128, 128, 128));
                    m_BrushShadow2 = new SolidBrush(Color.FromArgb(192, 192, 192, 192));
                    m_PenSelected = new Pen(_SelectedFrameColor);

                    bmpList = new Bitmap(pbCanvas.Width, pbCanvas.Height);
                    GC.Collect();
                }
                gList = Graphics.FromImage(bmpList);
                gList.Clear(this.BackColor);

                try
                {
                    float fUnitCount = (_BubbleCount - 1) * 0.5f + (_BubbleCount - 1) + (float)_SelectedRatio;
                    float fUnitSize = 0;
                    float fItemStartY = 0;
                    float fItemEndY = 0;
                    float fItemStartX = 0;
                    float fItemEndX = 0;
                    float fItemWidth=0;
                    float fItemHeight =0;
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    Font drawFont;

                    if (_Style == UIStyle.Horizontal)
                    {
                        fUnitSize = (this.Width - 4) / fUnitCount;

                        if (_Shadow)
                        {
                            gList.FillRectangle(m_BrushShadow2,
                                0.5f * fUnitSize + 2,
                                ((float)_SelectedRatio / 2.0f - 0.1f) * fUnitSize + 2,
                                this.Width - fUnitSize + 1,
                                0.2f * fUnitSize + 1);

                            for (int i = 0; i < _BubbleCount; i++)
                            {
                                if (i < SelectedIndex)
                                {
                                    fItemStartY = ((float)_SelectedRatio / 2.0f - 0.5f) * fUnitSize;
                                    fItemEndY = fItemStartY + fUnitSize;
                                    fItemStartX = i * fUnitSize + i * 0.5f * fUnitSize;
                                    fItemEndX = fItemStartX + fUnitSize;
                                }
                                else if (i > SelectedIndex)
                                {
                                    fItemStartY = ((float)_SelectedRatio / 2.0f - 0.5f) * fUnitSize;
                                    fItemEndY = fItemStartY + fUnitSize;
                                    fItemStartX = (i - 1) * fUnitSize + i * 0.5f * fUnitSize + (float)_SelectedRatio * fUnitSize;
                                    fItemEndX = fItemStartX + fUnitSize;
                                }
                                else
                                {
                                    fItemStartY = 0;
                                    fItemEndY = (float)_SelectedRatio * fUnitSize;
                                    fItemStartX = i * fUnitSize + i * 0.5f * fUnitSize;
                                    fItemEndX = fItemStartX + (float)_SelectedRatio * fUnitSize;
                                }

                                fItemWidth = fItemEndX - fItemStartX;
                                fItemHeight = fItemEndY - fItemStartY;

                                gList.FillEllipse(m_BrushShadow2,
                                    fItemStartX + 2,
                                    fItemStartY + 2,
                                    fItemWidth + 1,
                                    fItemHeight + 1);
                            }
                        }

                        gList.FillRectangle(m_BrushFrameColor,
                            0.5f * fUnitSize,
                            ((float)_SelectedRatio / 2.0f - 0.1f) * fUnitSize,
                            this.Width - fUnitSize,
                            0.2f * fUnitSize);

                        for (int i = 0; i < _BubbleCount; i++)
                        {
                            if (i < SelectedIndex)
                            {
                                fItemStartY = ((float)_SelectedRatio / 2.0f - 0.5f) * fUnitSize;
                                fItemEndY = fItemStartY + fUnitSize;
                                fItemStartX = i * fUnitSize + i * 0.5f * fUnitSize;
                                fItemEndX = fItemStartX + fUnitSize;
                            }
                            else if (i > SelectedIndex)
                            {
                                fItemStartY = ((float)_SelectedRatio / 2.0f - 0.5f) * fUnitSize;
                                fItemEndY = fItemStartY + fUnitSize;
                                fItemStartX = (i - 1) * fUnitSize + i * 0.5f * fUnitSize + (float)_SelectedRatio * fUnitSize;
                                fItemEndX = fItemStartX + fUnitSize;
                            }
                            else
                            {
                                fItemStartY = 0;
                                fItemEndY = (float)_SelectedRatio * fUnitSize;
                                fItemStartX = i * fUnitSize + i * 0.5f * fUnitSize;
                                fItemEndX = fItemStartX + (float)_SelectedRatio * fUnitSize;                                
                            }

                            fItemWidth = fItemEndX - fItemStartX;
                            fItemHeight = fItemEndY - fItemStartY;

                            gList.FillEllipse(m_BrushFrameColor,
                                fItemStartX,
                                fItemStartY,
                                fItemWidth,
                                fItemHeight);

                            if (iMouseX > fItemStartX &&
                                iMouseX < fItemEndX &&
                                iMouseY > fItemStartY &&
                                iMouseY < fItemEndY
                                )
                            {
                                if (bIsMouseEnter)
                                {
                                    if (bIsMouseDown)
                                    {
                                        tmpBrush = m_BrushMouseDownColor;
                                        tmpSelectedIndex = i;
                                    }
                                    else
                                    {
                                        tmpBrush = m_BrushMouseOverColor;
                                    }
                                    if (_BubbleStringStyle == BubbleStringStyle.ToolTip)
                                    {
                                        toolTip.SetToolTip(pbCanvas, _listBubbleString[i]);
                                    }
                                }
                                else
                                {
                                    tmpBrush = m_BrushBubbleColor;
                                }                                
                            }
                            else
                            {
                                if (i == SelectedIndex)
                                {
                                    tmpBrush = m_BrushSelectedBubbleColor;
                                }
                                else
                                {
                                    tmpBrush = m_BrushBubbleColor;
                                }
                            }

                            if (i != SelectedIndex)
                            {
                                gList.FillEllipse(tmpBrush,
                                    fItemStartX + 0.15f * fUnitSize,
                                    fItemStartY + 0.15f * fUnitSize,
                                    fItemWidth - 0.3f * fUnitSize,
                                    fItemHeight - 0.3f * fUnitSize);

                                drawFont = new Font("Verdana", fUnitSize * 0.3f, FontStyle.Regular);
                                gList.DrawString((i + 1).ToString(),
                                        drawFont,
                                        m_BrushText,
                                        new RectangleF(fItemStartX + 0.15f * fUnitSize,
                                            fItemStartY + 0.15f * fUnitSize,
                                            fItemWidth - 0.3f * fUnitSize,
                                            fItemHeight - 0.3f * fUnitSize),
                                        sf);
                            }
                            else
                            {
                                gList.FillEllipse(tmpBrush,
                                    fItemStartX + 0.15f * fUnitSize,
                                    fItemStartY + 0.15f * fUnitSize,
                                    fItemWidth - 0.3f * fUnitSize,
                                    fItemHeight - 0.3f * fUnitSize);

                                m_PenSelected.Width = 0.2f * fUnitSize; 
                                gList.DrawEllipse(m_PenSelected,
                                    fItemStartX + 0.09f * fUnitSize,
                                    fItemStartY + 0.09f * fUnitSize,
                                    fItemWidth - 0.18f * fUnitSize,
                                    fItemHeight - 0.18f * fUnitSize);

                                drawFont = new Font("Verdana", fUnitSize * 0.5f, FontStyle.Regular);
                                gList.DrawString((i + 1).ToString(),
                                        drawFont,
                                        m_BrushText,
                                        new RectangleF(fItemStartX + 0.15f * fUnitSize,
                                            fItemStartY + 0.15f * fUnitSize,
                                            fItemWidth - 0.3f * fUnitSize,
                                            fItemHeight - 0.3f * fUnitSize),
                                        sf);
                            }
                        }
                    }
                    else if (_Style == UIStyle.Vertical)
                    {
                        fUnitSize = (this.Height - 4) / fUnitCount;
                        drawFont = new Font("Verdana", fUnitSize * 0.2f, FontStyle.Regular);

                        if (_Shadow)
                        {
                            gList.FillRectangle(m_BrushShadow2,
                                ((float)_SelectedRatio / 2.0f - 0.1f) * fUnitSize + 2,
                                0.5f * fUnitSize + 2,
                                0.2f * fUnitSize + 1,
                                this.Height - fUnitSize + 1); 

                            for (int i = 0; i < _BubbleCount; i++)
                            {
                                fItemStartX = 0;
                                fItemEndX = this.Width;

                                if (i < SelectedIndex)
                                {
                                    fItemStartX = ((float)_SelectedRatio / 2.0f - 0.5f) * fUnitSize;
                                    fItemEndX = fItemStartX + fUnitSize;
                                    fItemStartY = i * fUnitSize + i * 0.5f * fUnitSize;
                                    fItemEndY = fItemStartY + fUnitSize;
                                }
                                else if (i > SelectedIndex)
                                {
                                    fItemStartX = ((float)_SelectedRatio / 2.0f - 0.5f) * fUnitSize;
                                    fItemEndX = fItemStartX + fUnitSize;
                                    fItemStartY = (i - 1) * fUnitSize + i * 0.5f * fUnitSize + (float)_SelectedRatio * fUnitSize;
                                    fItemEndY = fItemStartY + fUnitSize;
                                }
                                else
                                {
                                    fItemStartX = 0;
                                    fItemEndX = (float)_SelectedRatio * fUnitSize;
                                    fItemStartY = i * fUnitSize + i * 0.5f * fUnitSize;
                                    fItemEndY = fItemStartY + (float)_SelectedRatio * fUnitSize;                                    
                                }

                                fItemWidth = fItemEndX - fItemStartX;
                                fItemHeight = fItemEndY - fItemStartY;

                                gList.FillEllipse(m_BrushShadow2,
                                    fItemStartX + 2,
                                    fItemStartY + 2,
                                    fItemWidth + 1,
                                    fItemHeight + 1);
                            }
                        }

                        gList.FillRectangle(m_BrushFrameColor,
                            ((float)_SelectedRatio / 2.0f - 0.1f) * fUnitSize,
                            0.5f * fUnitSize,
                            0.2f * fUnitSize,
                            this.Height - fUnitSize);                        

                        for (int i = 0; i < _BubbleCount; i++)
                        {
                            fItemStartX = 0;
                            fItemEndX = this.Width;

                            if (i < SelectedIndex)
                            {
                                fItemStartX = ((float)_SelectedRatio / 2.0f - 0.5f) * fUnitSize;
                                fItemEndX = fItemStartX + fUnitSize;
                                fItemStartY = i * fUnitSize + i * 0.5f * fUnitSize;
                                fItemEndY = fItemStartY + fUnitSize;
                            }
                            else if (i > SelectedIndex)
                            {
                                fItemStartX = ((float)_SelectedRatio / 2.0f - 0.5f) * fUnitSize;
                                fItemEndX = fItemStartX + fUnitSize;
                                fItemStartY = (i - 1) * fUnitSize + i * 0.5f * fUnitSize + (float)_SelectedRatio * fUnitSize;
                                fItemEndY = fItemStartY + fUnitSize;
                            }
                            else
                            {
                                fItemStartX = 0;
                                fItemEndX = (float)_SelectedRatio * fUnitSize;
                                fItemStartY = i * fUnitSize + i * 0.5f * fUnitSize;
                                fItemEndY = fItemStartY + (float)_SelectedRatio * fUnitSize;
                            }

                            fItemWidth = fItemEndX - fItemStartX;
                            fItemHeight = fItemEndY - fItemStartY;

                            gList.FillEllipse(m_BrushFrameColor,
                                fItemStartX,
                                fItemStartY,
                                fItemWidth,
                                fItemHeight);

                            if (iMouseY > fItemStartY &&
                                iMouseY < fItemEndY &&
                                iMouseX > fItemStartX &&
                                iMouseX < fItemEndX
                                )
                            {
                                if (bIsMouseEnter)
                                {
                                    if (bIsMouseDown)
                                    {
                                        tmpBrush = m_BrushMouseDownColor;
                                        tmpSelectedIndex = i;
                                    }
                                    else
                                    {
                                        tmpBrush = m_BrushMouseOverColor;
                                    }
                                    if (_BubbleStringStyle == BubbleStringStyle.ToolTip)
                                    {
                                        toolTip.SetToolTip(pbCanvas, _listBubbleString[i]);
                                    }
                                }
                                else
                                {
                                    tmpBrush = m_BrushBubbleColor;
                                }                                
                            }
                            else
                            {
                                if (i == SelectedIndex)
                                {
                                    tmpBrush = m_BrushSelectedBubbleColor;
                                }
                                else
                                {
                                    tmpBrush = m_BrushBubbleColor;
                                }                                
                            }

                            if (i != SelectedIndex)
                            {
                                gList.FillEllipse(tmpBrush,
                                    fItemStartX + 0.15f * fUnitSize,
                                    fItemStartY + 0.15f * fUnitSize,
                                    fItemWidth - 0.3f * fUnitSize,
                                    fItemHeight - 0.3f * fUnitSize);

                                drawFont = new Font("Verdana", fUnitSize * 0.3f, FontStyle.Regular);
                                gList.DrawString((i + 1).ToString(),
                                        drawFont,
                                        m_BrushText,
                                        new RectangleF(fItemStartX + 0.15f * fUnitSize,
                                            fItemStartY + 0.15f * fUnitSize,
                                            fItemWidth - 0.3f * fUnitSize,
                                            fItemHeight - 0.3f * fUnitSize),
                                        sf);
                            }
                            else
                            {
                                gList.FillEllipse(tmpBrush,
                                    fItemStartX + 0.15f * fUnitSize,
                                    fItemStartY + 0.15f * fUnitSize,
                                    fItemWidth - 0.3f * fUnitSize,
                                    fItemHeight - 0.3f * fUnitSize);

                                m_PenSelected.Width = 0.2f * fUnitSize;
                                gList.DrawEllipse(m_PenSelected,
                                    fItemStartX + 0.09f * fUnitSize,
                                    fItemStartY + 0.09f * fUnitSize,
                                    fItemWidth - 0.18f * fUnitSize,
                                    fItemHeight - 0.18f * fUnitSize);

                                drawFont = new Font("Verdana", fUnitSize * 0.5f, FontStyle.Regular);
                                gList.DrawString((i + 1).ToString(),
                                        drawFont,
                                        m_BrushText,
                                        new RectangleF(fItemStartX + 0.15f * fUnitSize,
                                            fItemStartY + 0.15f * fUnitSize,
                                            fItemWidth - 0.3f * fUnitSize,
                                            fItemHeight - 0.3f * fUnitSize),
                                        sf);
                            }
                        }
                    }

                    pbCanvas.BackgroundImage = bmpList;
                    pbCanvas.Refresh();
                }
                catch
                {
                }
            }
        }

        private void StepBubble_Resize(object sender, EventArgs e)
        {
            pbCanvas.Width = this.Width;
            pbCanvas.BackColor = this.BackColor;
            DrawBubble(true);
        }

        private void StepBubble_Load(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                this.BackColor = this.Parent.BackColor;
            }
            pbCanvas.Width = this.Width;
            pbCanvas.BackColor = this.BackColor;
            DrawBubble(true);
        }

        private void StepBubble_MouseDown(object sender, MouseEventArgs e)
        {
            bIsMouseClickCheck = true;
            bIsMouseDown = true;
            iMouseDownX = e.X;
            iMouseDownY = e.Y;
            DrawBubble(true);
        }

        private void StepBubble_MouseMove(object sender, MouseEventArgs e)
        {
            if (bIsMouseEnter)
            {
                iMouseX = e.X;
                iMouseY = e.Y;
                
                DrawBubble(true);
            }
            else
            {
                iMouseY = -1;
            }
        }

        private void StepBubble_MouseUp(object sender, MouseEventArgs e)
        {
            if (Math.Abs(e.X - iMouseDownX) > 10
                || Math.Abs(e.Y - iMouseDownY) > 10)
            {
                bIsMouseClickCheck = false;
            }
            else
            {
                SelectedIndex = tmpSelectedIndex;
            }
            bIsMouseDown = false;
            DrawBubble(true);
            if (BubbleClick != null)
            {
                BubbleClick(sender, e);
            }
        }

        private void StepBubble_MouseEnter(object sender, EventArgs e)
        {
            bIsMouseEnter = true;
            pbCanvas.Focus();
        }

        private void StepBubble_MouseLeave(object sender, EventArgs e)
        {
            if (_BubbleStringStyle == BubbleStringStyle.ToolTip)
            {
                toolTip.Hide(pbCanvas);
            }
            bIsMouseEnter = false;
            DrawBubble(true);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using System.IO;
using ArtCommonLib;
using ArtCommunication;
using ArtControlLib;
using ArtData;

namespace ArtSystem
{
    /// <summary> ucRoundButton </summary>
    public class ucRoundButton : Button
    {
        #region //=====================  屬性變數設置 =====================
        private Color m_Color = SystemColors.Control;
        public Color _Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                bool needupdateimage = m_Color != value;
                this.BackColor = Color.Transparent;
                m_Color = value;
                if (needupdateimage)
                {
                    UpdateBGImage();
                }
            }
        }
        private Color m_MouseOnColor = SystemColors.Control;
        public Color _MouseOnColor
        {
            get
            {
                return m_MouseOnColor;
            }
            set
            {
                bool needupdateimage = m_MouseOnColor != value;
                m_MouseOnColor = value;
                if (needupdateimage)
                {
                    UpdateBGImage();
                }
            }
        }
        private Color m_EdgeColor = SystemColors.Control;
        public Color _EdgeColor
        {
            get
            {
                return m_EdgeColor;
            }
            set
            {
                m_EdgeColor = value;
                UpdateBGImage();
            }
        }
        public bool _ReadOnly
        {
            get
            {
                return !m_TextBox.Enabled;
            }
            set
            {
                m_TextBox.Enabled = !value;
            }
        }
        public Color _TextColor
        {
            get
            {
                return m_TextBox.ForeColor;
            }
            set
            {
                m_TextBox.ForeColor = value;
            }
        }
        public Font _Font
        {
            get
            {
                return this.Font;
            }
            set
            {
                this.Font = value;
                ControlSizeChanged();
            }
        }
        private int m_Radius = 0;
        public int _Radius
        {
            get
            {
                return m_Radius;
            }
            set
            {
                bool NeedUpdate = m_Radius != value;
                m_Radius = value;
                if (NeedUpdate)
                {
                    UpdateBGImage();
                }
            }
        }
        private bool m_NeedEdge = false;
        public bool _NeedEdge
        {
            get
            {
                return m_NeedEdge;
            }
            set
            {
                bool NeedUpdate = m_NeedEdge != value;
                m_NeedEdge = value;
                if (NeedUpdate)
                {
                    UpdateBGImage();
                }
            }
        }

        private bool m_AutoMouseOnColor = false;
        public bool _AutoMouseOnColor
        {
            get
            {
                return m_AutoMouseOnColor;
            }
            set
            {
                bool NeedUpdate = m_AutoMouseOnColor != value;
                m_AutoMouseOnColor = value;
                if (NeedUpdate)
                {
                    UpdateBGImage();
                }
            }
        }
        #endregion

        #region //=====================  區域變數設置 =====================

        private Bitmap m_BGImage = new Bitmap(100, 100);
        private Label m_TextBox = new Label();
        private bool _IsMouseOn = false;

        #endregion

        #region //=====================  建構式 =====================
        /// <summary> ucRoundTextBox 建構式 </summary>
        public ucRoundButton()
        {
            InitializeComponent();

            //這些得帶上，不然會有黑邊
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;

            this.Controls.Add(m_TextBox);
            this.EnabledChanged += new EventHandler(EnableChangedEvent);
            ControlSizeChanged();
            UpdateBGImage();
        }
        #endregion

        #region //===================== public 函式設置 =====================

        #endregion

        #region //===================== private 函式設置 =====================
        protected override void OnPaint(PaintEventArgs pevent)
        {
            return;
        }
        protected override void OnTextChanged(EventArgs e)
        {
            _IsMouseOn = false;
            UpdateBGImage();
        }
        private void ControlSizeChanged()
        {
            m_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            m_TextBox.BackColor = Color.Transparent;
            m_TextBox.Left = 0;
            m_TextBox.Top = 0;
            m_TextBox.Width = this.Width;
            m_TextBox.Height = this.Height;
            m_TextBox.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void UpdateBGImage()
        {
            m_BGImage = new Bitmap(m_BGImage, this.Width, this.Height);
            Graphics m_GPImage = Graphics.FromImage(m_BGImage);
            Rectangle bounds = new Rectangle(0, 0, this.Width, this.Height);
            m_GPImage.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            try
            {
                m_GPImage.Clear(this.BackColor);
            }
            catch (Exception ex)
            {
                m_GPImage.Clear(SystemColors.Control);
            }
            Color MouseOnColor = m_MouseOnColor;
            if (m_AutoMouseOnColor == true)
            {
                int GrayValue = (m_Color.R + m_Color.G + m_Color.B) / 3;
                if (GrayValue > 128)
                {
                    int cR = m_Color.R;
                    int cG = m_Color.G;
                    int cB = m_Color.B;
                    if (cR < 30)
                    { cR = 0; }
                    else
                    { cR -= 30; }
                    if (cG < 30)
                    { cG = 0; }
                    else
                    { cG -= 30; }
                    if (cB < 30)
                    { cB = 0; }
                    else
                    { cB -= 30; }
                    MouseOnColor = Color.FromArgb(cR, cG, cB);
                }
                else
                {
                    int cR = m_Color.R;
                    int cG = m_Color.G;
                    int cB = m_Color.B;
                    if (cR > 255 - 30)
                    { cR = 255; }
                    else
                    { cR += 30; }
                    if (cG > 255 - 30)
                    { cG = 255; }
                    else
                    { cG += 30; }
                    if (cB > 255 - 30)
                    { cB = 255; }
                    else
                    { cB += 30; }
                    MouseOnColor = Color.FromArgb(cR, cG, cB);
                }
            }
            if (m_Radius == 0)
            {
                SolidBrush mBrush = new SolidBrush(this.Enabled == false ? Color.Gray :
                    (_IsMouseOn ) ? MouseOnColor : m_Color);
                m_GPImage.FillRectangle(mBrush, bounds);
            }
            else
            {
                SolidBrush mBrush = new SolidBrush(this.Enabled == false ? Color.Gray :
                    (_IsMouseOn ) ? MouseOnColor : m_Color);
                int diameter = m_Radius * 2;
                bounds.Width = diameter;
                bounds.Height = diameter;
                m_GPImage.FillPie(mBrush, bounds, 0, 360);
                bounds.X = this.Width - diameter - 1;
                m_GPImage.FillPie(mBrush, bounds, 0, 360);
                bounds.Y = this.Height - diameter - 1;
                m_GPImage.FillPie(mBrush, bounds, 0, 360);
                bounds.X = 1;
                m_GPImage.FillPie(mBrush, bounds, 0, 360);

                bounds.X = m_Radius;
                bounds.Y = 0;
                bounds.Width = this.Width - diameter;
                bounds.Height = this.Height;
                m_GPImage.FillRectangle(mBrush, bounds);
                bounds.X = 0;
                bounds.Y = m_Radius;
                bounds.Width = this.Width;
                bounds.Height = this.Height - diameter;
                m_GPImage.FillRectangle(mBrush, bounds);
                if (_NeedEdge)
                {
                    bounds.X = 0;
                    bounds.Y = 0;
                    bounds.Width = diameter;
                    bounds.Height = diameter;
                    m_GPImage.DrawArc(new Pen(this.Enabled == false ? Color.Gray : m_EdgeColor, 2), bounds, 180, 90);
                    bounds.X = this.Width - diameter - 1;
                    m_GPImage.DrawArc(new Pen(this.Enabled == false ? Color.Gray : m_EdgeColor, 2), bounds, 270, 90);
                    bounds.Y = this.Height - diameter - 1;
                    m_GPImage.DrawArc(new Pen(this.Enabled == false ? Color.Gray : m_EdgeColor, 2), bounds, 0, 90);
                    bounds.X = 1;
                    m_GPImage.DrawArc(new Pen(this.Enabled == false ? Color.Gray : m_EdgeColor, 2), bounds, 90, 90);
                    m_GPImage.DrawLine(new Pen(this.Enabled == false ? Color.Gray : m_EdgeColor, 2), new Point(m_Radius, 0), new Point(this.Width - m_Radius, 0));
                    m_GPImage.DrawLine(new Pen(this.Enabled == false ? Color.Gray : m_EdgeColor, 2), new Point(m_Radius, this.Height - 1), new Point(this.Width - m_Radius, this.Height - 1));
                    m_GPImage.DrawLine(new Pen(this.Enabled == false ? Color.Gray : m_EdgeColor, 2), new Point(0, m_Radius), new Point(0, this.Height - m_Radius));
                    m_GPImage.DrawLine(new Pen(this.Enabled == false ? Color.Gray : m_EdgeColor, 2), new Point(this.Width - 1, m_Radius), new Point(this.Width - 1, this.Height - m_Radius));
                }

            }
            this.BackgroundImage = m_BGImage;
            m_TextBox.Text = this.Text;
        }
        #endregion

        #region //===================== 以下為事件處理 =====================

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ucRoundButton
            // 
            this.LocationChanged += new System.EventHandler(this.ucRoundButton_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.ucRoundTextBox_SizeChanged);
            m_TextBox.Click += new System.EventHandler(this.m_TextBoxClick);
            m_TextBox.MouseEnter += new System.EventHandler(this.m_MouseEnter);
            m_TextBox.MouseLeave += new System.EventHandler(this.m_MouseLeave);
            this.ResumeLayout(false);

        }
        private void m_MouseEnter(object sender, EventArgs e)
        {
            _IsMouseOn = true;
            UpdateBGImage();
            this.OnMouseEnter(e);
        }
        private void m_MouseLeave(object sender, EventArgs e)
        {
            _IsMouseOn = false;
            UpdateBGImage();
            this.OnMouseLeave(e);
        }
        private void EnableChangedEvent(object sender, EventArgs e)
        {
            UpdateBGImage();
        }
        private void m_TextBoxClick(object sender, EventArgs e)
        {
            try
            {
                this.OnClick(e);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void ucRoundTextBox_SizeChanged(object sender, EventArgs e)
        {
            ControlSizeChanged();
            UpdateBGImage();
        }
        private void ucRoundButton_LocationChanged(object sender, EventArgs e)
        {
            ControlSizeChanged();
            UpdateBGImage();
        }

        #endregion
    }
}

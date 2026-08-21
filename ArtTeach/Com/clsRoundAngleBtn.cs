using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using ArtCommonLib;
using ArtControlLib;
using ArtData;


namespace ArtTeach
{

    /// <summary> clsRoundAngleBtn 資訊 </summary>
    public class clsRoundAngleBtn : Button
    {
        #region //=====================  區域變數設置 =====================

        enum model
        {
            hover,
            enter,
            press,
            enable
        }

        public Color HoverBackColor { get; set; }
        public Color EnterBackColor { get; set; }
        public Color PressBackColor { get; set; }
        public Color HoverForeColor { get; set; }
        public Color EnterForeColor { get; set; }
        public Color PressForeColor { get; set; }
        public Color DisableColor { get; set; }
        public int Radius { get; set; }

        model paintmodel = model.hover;

        private bool bAutoAdjustColor = false;
        [Description("自動調整事件顏色")]
        [DisplayName("Auto Adjust Back Color"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public bool _AutoAdjustBackColor
        {
            get
            {
                return bAutoAdjustColor;
            }
            set
            {
                bAutoAdjustColor = value;
            }
        }

        #endregion

        #region //=====================  建構式 =====================

        public clsRoundAngleBtn() 
        {
            //InitializeComponent() ;

            //這些得帶上，不然會有黑邊
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0) ;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;

            SetDefaultColor() ;
        }

        #endregion

        #region //===================== public 函式設置 =====================

        public void SetDefaultColor() 
        {
            //給個初始值
            HoverBackColor = Color.LightBlue;
            EnterBackColor = Color.Blue;
            PressBackColor = Color.DarkBlue;

            HoverForeColor = Color.White;
            EnterForeColor = Color.White;
            PressForeColor = Color.White;

            Radius = 10;
        }

        protected override void OnPaint(PaintEventArgs e) 
        {
            base.OnPaint(e) ;//這個不能去，而且得放在前面，不然會有黑框之類的莫名其妙的東西
            var colorback = HoverBackColor;
            var colorfore = HoverForeColor;
            switch (paintmodel) 
            {
                case model.hover:
                    colorback = HoverBackColor;
                    colorfore = HoverForeColor;
                    break;
                case model.enter:
                    if (_AutoAdjustBackColor == true)
                    {
                        int R = HoverBackColor.R < 180 ? HoverBackColor.R + 30 : HoverBackColor.R - 30;
                        int G = HoverBackColor.G < 180 ? HoverBackColor.G + 30 : HoverBackColor.G - 30;
                        int B = HoverBackColor.B < 180 ? HoverBackColor.B + 30 : HoverBackColor.B - 30;
                        colorback = Color.FromArgb(R, G, B);
                        colorfore = EnterForeColor;
                    }
                    else
                    {
                        colorback = EnterBackColor;
                        colorfore = EnterForeColor;
                    }
                    break;
                case model.press:
                    if (_AutoAdjustBackColor == true)
                    {
                        int R = HoverBackColor.R < 180 ? HoverBackColor.R + 60 : HoverBackColor.R - 60;
                        int G = HoverBackColor.G < 180 ? HoverBackColor.G + 60 : HoverBackColor.G - 60;
                        int B = HoverBackColor.B < 180 ? HoverBackColor.B + 60 : HoverBackColor.B - 60;
                        colorback = Color.FromArgb(R, G, B);
                        colorfore = PressForeColor;
                    }
                    else
                    {
                        colorback = PressBackColor;
                        colorfore = PressForeColor;
                    }
                    break;
                case model.enable:
                    colorback = DisableColor;
                    break;
                default:
                    colorback = HoverBackColor;
                    colorfore = HoverForeColor;
                    break;
            }
            Draw(e.ClipRectangle, e.Graphics, false, colorback) ;
            DrawText(e.ClipRectangle, e.Graphics, colorfore) ;
        }
        protected override void OnMouseEnter(EventArgs e) 
        {
            paintmodel = model.enter;
            base.OnMouseEnter(e) ;


        }
        protected override void OnMouseLeave(EventArgs e) 
        {
            paintmodel = model.hover;
            base.OnMouseLeave(e) ;

        }
        protected override void OnMouseDown(MouseEventArgs mevent) 
        {
            paintmodel = model.press;
            base.OnMouseDown(mevent) ;
        }
        protected override void OnEnabledChanged(EventArgs e) 
        {
            paintmodel = Enabled ? model.hover : model.enable;
            Invalidate() ;//false 轉換為true的時候不會刷新 這裏強制刷新下
            base.OnEnabledChanged(e) ;

        }

        void Draw(Rectangle rectangle, Graphics g, bool cusp, Color begin_color, Color? end_colorex = null) 
        {
            Color end_color = end_colorex == null ? begin_color : (Color) end_colorex;
            int span = 2;
            //抗鋸齒
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //漸變填充
            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush(rectangle, begin_color, end_color, LinearGradientMode.Vertical) ;
            //畫尖角
            if (cusp) 
            {
                span = 10;
                PointF p1 = new PointF(rectangle.Width - 12, rectangle.Y + 10) ;
                PointF p2 = new PointF(rectangle.Width - 12, rectangle.Y + 30) ;
                PointF p3 = new PointF(rectangle.Width, rectangle.Y + 20) ;
                PointF[] ptsArray = { p1, p2, p3 };
                g.FillPolygon(myLinearGradientBrush, ptsArray) ;
            }
            //填充
            g.FillPath(myLinearGradientBrush, DrawRoundRect(rectangle.X, rectangle.Y, rectangle.Width - span, rectangle.Height - 1, Radius) ) ;
            if (this.BackgroundImage != null)
            {
                g.DrawImage(new Bitmap( this.BackgroundImage, this.Width, this.Height), new Point(0, 0));
            }
        }
        void DrawText(Rectangle rectangle, Graphics g, Color color) 
        {
            SolidBrush sbr = new SolidBrush(color) ;
            var rect = new RectangleF();
            rect = getTextRec(rectangle, g);
            if (TextAlign == ContentAlignment.BottomCenter
                || TextAlign == ContentAlignment.BottomLeft
                || TextAlign == ContentAlignment.BottomRight)
            {
                rect.Y = this.Height - rect.Height - 4;
            }
            if (TextAlign == ContentAlignment.TopCenter
                || TextAlign == ContentAlignment.TopLeft
                || TextAlign == ContentAlignment.TopRight)
            {
                rect.Y = 4;
            }
            if (TextAlign == ContentAlignment.TopLeft
                || TextAlign == ContentAlignment.BottomLeft
                || TextAlign == ContentAlignment.MiddleLeft)
            {
                rect.X = 4;
            }
            if (TextAlign == ContentAlignment.TopRight
                || TextAlign == ContentAlignment.BottomRight
                || TextAlign == ContentAlignment.MiddleRight)
            {
                rect.X = this.Width - rect.Width - 4;
            }
            g.DrawString(Text, Font, sbr, rect) ;
        }
        RectangleF getTextRec(Rectangle rectangle, Graphics g) 
        {
            var rect = new RectangleF() ;
            var size = g.MeasureString(Text, Font) ;
            if (size.Width > rectangle.Width || size.Height > rectangle.Height) 
            {
                rect = rectangle;
            }
            else
            {
                rect.Size = size;
                rect.Location = new PointF(rectangle.X + (rectangle.Width - size.Width) / 2, rectangle.Y + (rectangle.Height - size.Height) / 2) ;
            }
            return rect;
        }
        GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius) 
        {
            //四邊圓角
            GraphicsPath gp = new GraphicsPath() ;
            gp.AddArc(x, y, radius, radius, 180, 90) ;
            gp.AddArc(width - radius, y, radius, radius, 270, 90) ;
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90) ;
            gp.AddArc(x, height - radius, radius, radius, 90, 90) ;
            gp.CloseAllFigures() ;
            return gp;
        }


        #endregion

        #region //===================== private 函式設置 =====================

        #endregion

        #region //===================== 以下為事件處理 =====================
        
        #endregion
    }
}

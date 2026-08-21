using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using ArtCommonLib;
using ArtData;

namespace ArtControlLib
{
    /// <summary></summary>
    public class PointD
    {
        /// <summary></summary>
        public double X { get; set; }
        /// <summary></summary>
        public double Y { get; set; }

        /// <summary></summary>
        public PointD()
        { 
        
        }

        /// <summary></summary>
        public PointD(double p_X, double p_Y)
        {
            X = p_X;
            Y = p_Y;
        }
    }

    /// <summary></summary>
    public class SizeD
    {
        /// <summary></summary>
        public double Width { get; set; }
        /// <summary></summary>
        public double Height { get; set; }

        /// <summary></summary>
        public SizeD(double p_Width, double p_Height)
        {
            Width = p_Width;
            Height = p_Height;
        }
    }

    /// <summary>圖像按鈕元件</summary>
    public partial class dwgCanvas : PictureBox
    {
        #region //=====================  區域變數設置 =====================
        List<DrawStatus> m_lstDrawingAction = new List<DrawStatus>();
        #endregion

        /// <summary>
        /// 圖像按鈕元件建構式
        /// </summary>
        public dwgCanvas()
        {
            InitializeComponent();
            this.BackgroundImageLayout = ImageLayout.Zoom;
            this.Paint += new PaintEventHandler(dwgCanvas_Paint);
        }

        /// <summary>新增線條 </summary>
        /// <param name="p_ptStart">線條起始點</param>
        /// <param name="p_ptEnd">線條結束點</param>
        /// <param name="p_clrLineColor">線條顏色</param>
        /// <param name="p_dLineWidth">線條寬度</param>
        /// <param name="p_bIsFillColor">是否填滿顏色</param>
        /// <param name="p_clrFillColor">填滿顏色</param>
        public void AddLine(PointD p_ptStart, PointD p_ptEnd, Color p_clrLineColor, double p_dLineWidth = 2,
            bool p_bIsFillColor = false, Color p_clrFillColor = new Color())
        {
            DrawStatus drwStatus = new DrawStatus();
            drwStatus.DrawAction = enuDrawAction.DrawLine;

            drwStatus.DrawValue.Add(new Pen(p_clrLineColor, (float)p_dLineWidth));
            drwStatus.DrawValue.Add(new PointF((float)p_ptStart.X, (float)p_ptStart.Y));
            drwStatus.DrawValue.Add(new PointF((float)p_ptEnd.X, (float)p_ptEnd.Y));

            m_lstDrawingAction.Add(drwStatus);
        }

        /// <summary>新增矩形</summary>
        /// <param name="p_ptOrg">矩形左上座標</param>
        /// <param name="p_ptSize">矩形長寬</param>
        /// <param name="p_clrLineColor">線條顏色</param>
        /// <param name="p_dLineWidth">線條寬度</param>
        /// <param name="p_bIsFillColor">是否填滿顏色</param>
        /// <param name="p_clrFillColor">填滿顏色</param>
        /// <param name="p_fRotateAngle">旋轉角度</param>
        public void AddRectangle(PointD p_ptOrg, SizeD p_ptSize, Color p_clrLineColor, double p_dLineWidth = 2,
            bool p_bIsFillColor = false, Color p_clrFillColor = new Color(), float p_fRotateAngle = 0)
        {
            DrawStatus drwStatus = new DrawStatus();
            drwStatus.DrawAction = enuDrawAction.DrawRectangle;
            drwStatus.bIsFillColor = p_bIsFillColor;
            drwStatus.clrFillColor = p_clrFillColor;
            drwStatus.fRotateAngle = p_fRotateAngle;

            drwStatus.DrawValue.Add(new Pen(p_clrLineColor, (float)p_dLineWidth));
            drwStatus.DrawValue.Add((float)p_ptOrg.X);
            drwStatus.DrawValue.Add((float)p_ptOrg.Y);
            drwStatus.DrawValue.Add((float)p_ptSize.Width);
            drwStatus.DrawValue.Add((float)p_ptSize.Height);

            m_lstDrawingAction.Add(drwStatus);
        }

        /// <summary>新增弧形</summary>
        /// <param name="p_ptOrg">弧形左上座標</param>
        /// <param name="p_ptSize">圓型長寬</param>
        /// <param name="p_dStartAngle">起始角度</param>
        /// <param name="p_dSweepAngle">延伸角度 90畫1/4圈</param>
        /// <param name="p_clrLineColor">線條顏色</param>
        /// <param name="p_dLineWidth">線條寬度</param>
        /// <param name="p_bIsFillColor">是否填滿顏色</param>
        /// <param name="p_clrFillColor">填滿顏色</param>
        public void AddArc(PointD p_ptOrg, SizeD p_ptSize, double p_dStartAngle, double p_dSweepAngle,
            Color p_clrLineColor, double p_dLineWidth = 2,
            bool p_bIsFillColor = false, Color p_clrFillColor = new Color())
        {
            DrawStatus drwStatus = new DrawStatus();
            drwStatus.DrawAction = enuDrawAction.DrawArc;
            drwStatus.bIsFillColor = p_bIsFillColor;
            drwStatus.clrFillColor = p_clrFillColor;

            drwStatus.DrawValue.Add(new Pen(p_clrLineColor, (float)p_dLineWidth));
            drwStatus.DrawValue.Add((float)p_ptOrg.X);
            drwStatus.DrawValue.Add((float)p_ptOrg.Y);
            drwStatus.DrawValue.Add((float)p_ptSize.Width);
            drwStatus.DrawValue.Add((float)p_ptSize.Height);
            drwStatus.DrawValue.Add((float)p_dStartAngle);
            drwStatus.DrawValue.Add((float)p_dSweepAngle);

            m_lstDrawingAction.Add(drwStatus);
        }

        /// <summary>新增橢圓形</summary>
        /// <param name="p_ptOrg">橢圓左上座標</param>
        /// <param name="p_ptSize">圓型長寬</param>
        /// <param name="p_clrLineColor">線條顏色</param>
        /// <param name="p_dLineWidth">線條寬度</param>
        /// <param name="p_bIsFillColor">是否填滿顏色</param>
        /// <param name="p_clrFillColor">填滿顏色</param>
        public void AddEllipse(PointD p_ptOrg, SizeD p_ptSize, Color p_clrLineColor, double p_dLineWidth = 2,
            bool p_bIsFillColor = false, Color p_clrFillColor = new Color())
        {
            DrawStatus drwStatus = new DrawStatus();
            drwStatus.DrawAction = enuDrawAction.DrawEllipse;
            drwStatus.bIsFillColor = p_bIsFillColor;
            drwStatus.clrFillColor = p_clrFillColor;

            drwStatus.DrawValue.Add(new Pen(p_clrLineColor, (float)p_dLineWidth));
            drwStatus.DrawValue.Add((float)p_ptOrg.X);
            drwStatus.DrawValue.Add((float)p_ptOrg.Y);
            drwStatus.DrawValue.Add((float)p_ptSize.Width);
            drwStatus.DrawValue.Add((float)p_ptSize.Height);

            m_lstDrawingAction.Add(drwStatus);
        }

        /// <summary>新增文字</summary>
        /// <param name="p_strValue">文字內容</param>
        /// <param name="p_ptOrg">文字左上座標</param>
        /// <param name="p_ptSize">文字長寬</param>
        /// <param name="p_clrFontColor">文字顏色</param>
        /// <param name="p_dFontSize">文字大小</param>
        /// <param name="p_FontStyle">文字風格</param>
        /// <param name="p_format">文字格式</param>
        public void AddString(string p_strValue, PointD p_ptOrg, SizeD p_ptSize,
            Color p_clrFontColor, double p_dFontSize = 10, 
            FontStyle p_FontStyle = FontStyle.Regular, StringFormat p_format = null)
        {
            SizeF sizeString = new SizeF();
            double p_dFontSizeTemp = p_dFontSize;
            DrawStatus drwStatus = new DrawStatus();
            drwStatus.DrawAction = enuDrawAction.DrawString;
            drwStatus.clrFillColor = p_clrFontColor;

            drwStatus.DrawValue.Add(p_strValue);

            //drwStatus.DrawValue.Add(new Font("", (float)p_dFontSize, p_FontStyle));
            //drwStatus.DrawValue.Add(new Font("Verdana", (float)p_dFontSize, FontStyle.Regular));
            //drwStatus.DrawValue.Add(new Font(FontFamily.GenericSerif, (float)p_dFontSize, FontStyle.Regular));

            //自動調整字型大小於範圍內
            sizeString = this.CreateGraphics().MeasureString(
                p_strValue,
                new Font("", (float)p_dFontSizeTemp, p_FontStyle));

            while (sizeString.Width + sizeString.Width / 20 > (int)p_ptSize.Width || sizeString.Height + sizeString.Height / 20 > (int)p_ptSize.Height)
            {
                p_dFontSizeTemp -= 0.1;
                if (p_dFontSizeTemp > 0)
                {
                    sizeString = this.CreateGraphics().MeasureString(
                        p_strValue,
                        new Font("", (float)p_dFontSizeTemp, p_FontStyle));
                }
                else
                {
                    p_dFontSizeTemp = 0.1;
                    break;
                }
            }

            drwStatus.DrawValue.Add(new Font("", (float)p_dFontSizeTemp, p_FontStyle));
            drwStatus.DrawValue.Add(new RectangleF((float)p_ptOrg.X, (float)p_ptOrg.Y, (float)p_ptSize.Width, (float)p_ptSize.Height));
            drwStatus.DrawValue.Add(p_format);

            m_lstDrawingAction.Add(drwStatus);
        }

        /// <summary>新增圖案</summary>
        /// <param name="p_Image">圖案</param>
        /// <param name="p_ptOrg">矩形左上座標</param>
        /// <param name="p_ptSize">矩形長寬</param>
        /// <param name="p_dRotate">旋轉角度</param>
        public void AddImage(Image p_Image, PointD p_ptOrg, SizeD p_ptSize, double p_dRotate = 0)
        {
            DrawStatus drwStatus = new DrawStatus();
            drwStatus.DrawAction = enuDrawAction.DrawImage;
            drwStatus.bIsFillColor = false;
            Bitmap imgTemp = new Bitmap(p_Image);

            if (p_dRotate != 0)
            {
                imgTemp = RotateBitmap(imgTemp, p_dRotate);
            }

            drwStatus.DrawValue.Add(imgTemp);
            drwStatus.DrawValue.Add((float)p_ptOrg.X);
            drwStatus.DrawValue.Add((float)p_ptOrg.Y);
            drwStatus.DrawValue.Add((float)p_ptSize.Width);
            drwStatus.DrawValue.Add((float)p_ptSize.Height);

            m_lstDrawingAction.Add(drwStatus);
        }

        /// <summary>清除畫布內容</summary>
        public void Clear()
        {
            m_lstDrawingAction.Clear();
        }

        /// <summary>
        /// 計算旋轉後影像Size
        /// </summary>
        /// <param name="p_iWidth"></param>
        /// <param name="p_iHeight"></param>
        /// <param name="p_dRotateAngle"></param>
        /// <returns></returns>
        private Size CalculateNewSize(int p_iWidth, int p_iHeight, double p_dRotateAngle)
        {
            double r = Math.Sqrt(Math.Pow((double)p_iWidth / 2d, 2d) + Math.Pow((double)p_iHeight / 2d, 2d)); //半徑L
            double OriginalAngle = Math.Acos((p_iWidth / 2d) / r) / Math.PI * 180d;  //對角線和X軸的角度θ
            double minW = 0d, maxW = 0d, minH = 0d, maxH = 0d; //最大和最小的 X、Y座標
            double[] drawPoint = new double[4];

            drawPoint[0] = (-OriginalAngle + p_dRotateAngle) * Math.PI / 180d;
            drawPoint[1] = (OriginalAngle + p_dRotateAngle) * Math.PI / 180d;
            drawPoint[2] = (180f - OriginalAngle + p_dRotateAngle) * Math.PI / 180d;
            drawPoint[3] = (180f + OriginalAngle + p_dRotateAngle) * Math.PI / 180d;

            foreach (double point in drawPoint) //由四個角的點算出X、Y的最大值及最小值
            {
                double x = r * Math.Cos(point);
                double y = r * Math.Sin(point);

                if (x < minW)
                {
                    minW = x;
                }

                if (x > maxW)
                {
                    maxW = x;
                }

                if (y < minH)
                {
                    minH = y;
                }

                if (y > maxH)
                {
                    maxH = y;
                }
            }

            return new Size((int)(maxW - minW), (int)(maxH - minH));
        }

        /// <summary>
        /// 旋轉圖片之函式
        /// </summary>
        /// <param name="p_image">要旋轉的圖片</param>
        /// <param name="p_dRotateAngle">旋轉角度</param>
        /// <returns>回傳轉好的圖片</returns>
        private Bitmap RotateBitmap(Bitmap p_image, double p_dRotateAngle)
        {
            Size newSize = CalculateNewSize(p_image.Width, p_image.Height, p_dRotateAngle);
            Bitmap rotatedBmp = new Bitmap(newSize.Width, newSize.Height);
            PointF centerPoint = new PointF((float)rotatedBmp.Width / 2f, (float)rotatedBmp.Height / 2f);
            Graphics g = Graphics.FromImage(rotatedBmp);

            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.TranslateTransform(centerPoint.X, centerPoint.Y);
            g.RotateTransform((float)p_dRotateAngle);
            g.TranslateTransform(-centerPoint.X, -centerPoint.Y);

            g.DrawImage(p_image, 
                (float)(newSize.Width - p_image.Width) / 2f, 
                (float)(newSize.Height - p_image.Height) / 2f, 
                p_image.Width, p_image.Height);

            g.Dispose();
            return rotatedBmp;
        }

        private double PointRotation(Point PointA, Point PointB)
        {
            //var Pa = new Point(PointA.X, PointA.Y);
            //var Pb = new Point(PointB.X, PointB.Y);
            var DeltaX = PointB.X - PointA.X;
            var DeltaY = PointB.Y - PointA.Y;
            var radianRotation = Math.Atan2(DeltaY, DeltaX);
            var degreeRotation = radianRotation / Math.PI * 180;
            return degreeRotation;
        }

        //===================== 以下為事件處理 =====================
        void dwgCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (!this.Parent.IsHandleCreated)
            {
                return;
            }

            try
            {
                //e.Graphics.Clear(this.BackColor);
                e.Dispose();

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                foreach (DrawStatus drawStatus in m_lstDrawingAction)
                {
                    switch (drawStatus.DrawAction)
                    {
                        case enuDrawAction.DrawArc:
                            e.Graphics.DrawArc(
                                (Pen)drawStatus.DrawValue[0],
                                (float)drawStatus.DrawValue[1],
                                (float)drawStatus.DrawValue[2],
                                (float)drawStatus.DrawValue[3],
                                (float)drawStatus.DrawValue[4],
                                (float)drawStatus.DrawValue[5],
                                (float)drawStatus.DrawValue[6]
                                );

                            if (drawStatus.bIsFillColor)
                            {
                                e.Graphics.FillPie(
                                    new SolidBrush(drawStatus.clrFillColor),
                                    (float)drawStatus.DrawValue[1],
                                    (float)drawStatus.DrawValue[2],
                                    (float)drawStatus.DrawValue[3],
                                    (float)drawStatus.DrawValue[4],
                                    (float)drawStatus.DrawValue[5],
                                    (float)drawStatus.DrawValue[6]
                                    );
                            }
                            break;

                        case enuDrawAction.DrawEllipse:
                            e.Graphics.DrawEllipse(
                                (Pen)drawStatus.DrawValue[0],
                                (float)drawStatus.DrawValue[1],
                                (float)drawStatus.DrawValue[2],
                                (float)drawStatus.DrawValue[3],
                                (float)drawStatus.DrawValue[4]
                                );

                            if (drawStatus.bIsFillColor)
                            {
                                e.Graphics.FillEllipse(
                                    new SolidBrush(drawStatus.clrFillColor),
                                    (float)drawStatus.DrawValue[1],
                                    (float)drawStatus.DrawValue[2],
                                    (float)drawStatus.DrawValue[3],
                                    (float)drawStatus.DrawValue[4]
                                    );
                            }
                            break;

                        case enuDrawAction.DrawLine:
                            e.Graphics.DrawLine(
                                (Pen)drawStatus.DrawValue[0],
                                (PointF)drawStatus.DrawValue[1],
                                (PointF)drawStatus.DrawValue[2]
                                );
                            break;

                        case enuDrawAction.DrawRectangle:
                            Matrix myMatrix = new Matrix();
                            myMatrix.Rotate(drawStatus.fRotateAngle, MatrixOrder.Append);
                            e.Graphics.Transform = myMatrix;

                            e.Graphics.DrawRectangle(
                                (Pen)drawStatus.DrawValue[0],
                                (float)drawStatus.DrawValue[1],
                                (float)drawStatus.DrawValue[2],
                                (float)drawStatus.DrawValue[3],
                                (float)drawStatus.DrawValue[4]
                                );

                            if (drawStatus.bIsFillColor)
                            {
                                e.Graphics.FillRectangle(
                                    new SolidBrush(drawStatus.clrFillColor),
                                    (float)drawStatus.DrawValue[1],
                                    (float)drawStatus.DrawValue[2],
                                    (float)drawStatus.DrawValue[3],
                                    (float)drawStatus.DrawValue[4]
                                    );
                            }
                            break;

                        case enuDrawAction.DrawString:
                            e.Graphics.DrawString(
                                (string)drawStatus.DrawValue[0],
                                (Font)drawStatus.DrawValue[1],
                                new SolidBrush(drawStatus.clrFillColor),
                                (RectangleF)drawStatus.DrawValue[2],
                                (StringFormat)drawStatus.DrawValue[3]
                                );

                            break;

                        case enuDrawAction.DrawImage:
                            e.Graphics.DrawImage(
                                (Image)drawStatus.DrawValue[0],
                                (float)drawStatus.DrawValue[1],
                                (float)drawStatus.DrawValue[2],
                                (float)drawStatus.DrawValue[3],
                                (float)drawStatus.DrawValue[4]
                                );

                            

                            break;


                        default:
                            break;
                    }

                }
            }
            catch { }
        }

        enum enuDrawAction
        {
            DrawLine,
            DrawString,
            DrawRectangle,
            DrawEllipse,
            DrawArc,
            DrawImage,
        }

        class DrawStatus
        {
            List<object> m_drawValue = new List<object>();

            public enuDrawAction DrawAction { get; set; }
            public bool bIsFillColor { get; set; }
            public Color clrFillColor { get; set; }
            public float fRotateAngle { get; set; }
            public List<object> DrawValue
            {
                get { return m_drawValue; }
                set { m_drawValue = value; }
            }
        }

    }
}

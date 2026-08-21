using System;
using System.Collections.Generic;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtTeach;

namespace ArtTeach
{
    public partial class clsSwitchButton : UserControl
    {
        private Color OffButtonColor = SystemColors.Control;
        private Color OnButtonColor = SystemColors.Control;
        private Color OffColor = SystemColors.Control;
        private Color OnColor = Color.LawnGreen;
        private Color EdgeColor = Color.DarkGray;


        public Color _OffButtonColor
        {
            get
            {
                return OffButtonColor;
            }
            set
            {
                OffButtonColor = value;
                ReflashControl() ;
            }
        }

        public Color _OnButtonColor
        {
            get
            {
                return OnButtonColor;
            }
            set
            {
                OnButtonColor = value;
                ReflashControl() ;
            }
        }
        public Color _OffColor
        {
            get
            {
                return OffColor;
            }
            set
            {
                OffColor = value;
                ReflashControl() ;
            }
        }
        public Color _OnColor
        {
            get
            {
                return OnColor;
            }
            set
            {
                OnColor = value;
                ReflashControl() ;
            }
        }
        public Color _EdgeColor
        {
            get
            {
                return EdgeColor;
            }
            set
            {
                EdgeColor = value;
                ReflashControl() ;
            }
        }
        public bool _Status
        {
            get
            {
                return bStatus;
            }
            set
            {
                if (bStatus != value) 
                {
                    bStatus = value;
                    ReflashControl() ;
                }
            }
        }

        #region //===================== 區域變數設置 =====================

        private Bitmap bmImage;
        private Graphics gpImage;
        private bool bStatus = false;

        #endregion

        #region //===================== 必要函式設置 =====================

        #endregion

        #region //===================== public 函式設置 ==================

        public clsSwitchButton() 
        {
            InitializeComponent() ;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            bmImage = new Bitmap(25, 60) ;
            gpImage = Graphics.FromImage(bmImage) ;
            ReflashControl() ;
        }

        public void ReflashControl() 
        {
            gpImage.Clear(Color.Transparent) ;
            SolidBrush OnOffbrush = new SolidBrush(_Status ? OnColor : OffColor) ;
            SolidBrush Buttonbrush = new SolidBrush(_Status ? OnButtonColor: OffButtonColor) ;
            Pen edge = new Pen(EdgeColor) ;
            gpImage.FillPie(OnOffbrush, 4, 8, 16, 16, 0, 360) ;
            gpImage.FillPie(OnOffbrush, 4, this.Height - 16 - 1-8, 16, 16, 0, 360) ;
            gpImage.DrawArc(edge, 4, 8, 16, 16, 0, 360) ;
            gpImage.DrawArc(edge, 4, this.Height - 16 - 1-8, 16, 16, 0, 360) ;
            gpImage.FillRectangle(OnOffbrush, 4, 12+8, 16, this.Height - 16-16 - 2) ;
            gpImage.DrawLine(edge, 4, 8+8, 4, this.Height - 8-8 + 1) ;
            gpImage.DrawLine(edge, 20, 8+8, 20, this.Height - 8-8 + 1) ;
            gpImage.FillPie(Buttonbrush, 0, _Status ? 0 : this.Height - 24 - 1, 24, 24, 0, 360) ;
            gpImage.DrawArc(edge, 0, _Status ? 0 : this.Height - 24 - 1, 24, 24, 0, 360) ;


            this.BackgroundImage = bmImage;
            this.Refresh() ;
        }

        #endregion

        #region //===================== private 函式設置 =================


        #endregion

        #region//===================== 以下為事件處理 ===================


        #endregion

    }
}

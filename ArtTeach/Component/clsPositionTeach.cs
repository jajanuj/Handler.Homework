using System;
using System.Collections.Generic;
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
using ArtCommunication;
using System.IO;

namespace ArtTeach
{
    public partial class clsPositionTeach: ArtCommonLib.ucBaseUserControl
    {
        #region //===================== 區域變數設置 =====================


        #endregion

        #region //===================== 必要函式設置 =====================


        /// <summary>
        /// 物件建立請利用 GetSingleton()，除非特殊需求
        /// </summary>
        public clsPositionTeach()
        {
            InitializeComponent();
            ucParameter.Add(this);
        }

        #endregion

        #region //===================== public 函式設置 ==================


        #endregion

        #region //===================== private 函式設置 =================

        #endregion

        #region//===================== 以下為事件處理 ===================

        #endregion
    }
    public class clsJogTeach
    {
        #region 參數

        /// <summary> 設定Job移動方向 </summary>
        public enuMoveDir _JogMoveDir;

        /// <summary> 移動速度百分比 </summary>
        public int _TeachSpeed;

        /// <summary> 設定安全位置 </summary>
        public double _SafePosition;

        /// <summary> 設定目標位置 </summary>
        public double _Target;

        /// <summary> 設定復原位置 </summary>
        public double _Undo;

        /// <summary> JogA文字 </summary>
        public string _JogAText;

        /// <summary> JogB文字 </summary>
        public string _JogBText;

        /// <summary> 軸動標題名稱 </summary>
        public string _AxisTitle;

        /// <summary> 軸動單元 </summary>
        public clsJogAndTeach.enuUnit _AxisUnit;

        /// <summary> 指定移動軸名稱 </summary>
        public clsEnum.enuAxis _AxisName;

        /////<summary> 對應參數名稱 </summary>
        //public clsEnum.enuPmtName? _PmtName;

        /////<summary> 對應參數種類 </summary>
        //public clsEnum.enuPmtType? _PmtType;

        ///// <summary> JogA Engter圖片 </summary>
        //public Image _JobAImageEnter;

        ///// <summary> JogB Engter圖片 </summary>
        //public Image _JobBImageEnter;

        #endregion

        #region 建構式 與 解建構式

        public clsJogTeach()
        {
        }
        ~clsJogTeach()
        {
        }

        #endregion
    }

    public class Station
    {
        private string _StationName;
        private double _Lon = 103;
        private double _Lat = 38;
        private Color _color;
        private string _file = string.Empty;
        private Font _font;
        public string FileName
        {
            get { return _file; }
            set { _file = value; }
        }
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }
        public string StationName
        {
            get { return _StationName; }
            set { _StationName = value; }
        }
        public double Lon
        {
            get { return _Lon; }
            set { _Lon = value; }
        }
        public double Lat
        {
            get { return _Lat; }
            set { _Lat = value; }
        }
    }  
}

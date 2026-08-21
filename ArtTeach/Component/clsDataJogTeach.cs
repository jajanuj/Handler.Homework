using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtTeach
{
    //[Serializable() ] 
    public class clsDataJogTeach
    {
        public enum ActionMode
        {
            RelativeMove,
            AbsoluteMove,
            ContinueMove,
        }
        public enum enuDisplayMode
        {
            ShowAll,
            MotionCtrl,
            SetPosition,
        }

        #region 參數
        ///<summary> 是否可使用此元件 </summary>
        [Description("JogTeach Enable")]
        public bool _JogTeach_Enable = true;
        /// <summary> 軸動標題名稱 </summary>
        [Description("Axis Title")]
        public string _AxisTitle_String = "Axis Name";
        /// <summary> 軸動單位 </summary>
        [Description("Axis Unit Text")]
        public string _AxisUnit_String = "mm";
        /// <summary> 指定移動軸名稱 </summary>
        [Description("Axis Enum")]
        public string _AxisEnum_String;

        ///<summary> 是否可使用設定點位 </summary>
        [Description("Pos Set Enable")]
        public bool _PosSet_Enable = true;
        /// <summary> 設定寫入位置名稱 </summary>
        [Description("Pos Name")]
        public string _PosName_String = "Position Name";
        /// <summary> 設定寫入位置名稱 </summary>
        [Description("Pos Enum")]
        public string _PosEnum_String = "";

        /// <summary> 設定安全位置 </summary>
        [Description("Safe Position Enum")]
        public string _SafePositionEnum_String = "";
        /// <summary> 設定安全位置 </summary>
        [Description("Safe Position")]
        public double _SafePosition = 0;

        /// <summary> 設定Job移動方向 </summary>
        [Description("Jog Move Direction")]
        public enuMoveDir _JogMoveDir = enuMoveDir.Positive;
        /// <summary> JogA文字 </summary>
        [Description("JobA Text")]
        public string _JogAText_String = "Jog+";
        /// <summary> JogA Enter圖片 </summary>
        [Description("JobA Img Path")]
        public string _JobAImg_String;
        /// <summary> JogA Image 方向位置 </summary>
        [Description("JogA Image Location")]
        public Point _JogAImageDirPoint = new Point() ;
        /// <summary> JogB文字 </summary>
        [Description("JobB Text")]
        public string _JogBText_String = "Jog-";
        /// <summary> JogB Engter圖片 </summary>
        [Description("JobB Img Path")]
        public string _JobBImg_String;
        /// <summary> JogB Image 方向位置 </summary>
        [Description("JogB Image Location")]
        public Point _JogBImageDirPoint = new Point() ;
        /// <summary> 元件顯示的樣式 </summary>
        [Description("Display Mode")]
        public enuDisplayMode _enuDisplayMode = enuDisplayMode.ShowAll;
        /// <summary> 追隨上個控制元件, 元件位置變動 </summary>
        [Description("Follow Upper Controls")]
        public bool _FollowUpperControls = false;
        #endregion

        #region 建構式 與 解建構式

        public clsDataJogTeach() 
        {
        }
        ~clsDataJogTeach() 
        {
        }

        #endregion

    }
}

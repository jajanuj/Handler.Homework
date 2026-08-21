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
    public class clsDataDIO
    {
        #region 參數
        ///<summary> 是否可使用此元件 </summary>
        [Description("DIO Enable")]
        public bool _IsDIOEnable = false;
        [Description("Do On Btn Color")]
        public Color _DoOnBtnColor = Color.LawnGreen;
        [Description("Do On Back Color")]
        public Color _DoOnBackColor = Color.Aquamarine;
        [Description("Do Off Btn Color")]
        public Color _DoOffBtnColor = SystemColors.Control;
        [Description("Do Off Back Color")]
        public Color _DoOffBackColor = Color.DarkGray;
        [Description("Di Reach Color")]
        public Color _DiReachColor = Color.Green;
        [Description("Di Home Color")]
        public Color _DiHomeColor = Color.Green;

        [Description("Di Reach Name")]
        public string _DiReachName = "";
        [Description("Di Home Name")]
        public string _DiHomeName = "";
        [Description("DO Name")]
        public string _DIOName = "";

        ///<summary> 原點訊號 </summary>
        [Description("DI Home Enum")]
        public string _DIHomeEnum_String = "";
        ///<summary> 到位訊號 </summary>
        [Description("DI Reach Enum")]
        public string _DIReachEnum_String = "";
        ///<summary> 原點觸發 </summary>
        [Description("DO Trigger 1 Enum")]
        public string _DOTriggerEnum_String = "";
        ///<summary> 原點觸發 </summary>
        [Description("DO Trigger 2 Enum")]
        public string _DOTrigger2Enum_String = "";

        ///<summary> DI原點訊號是否B接反向 </summary>
        [Description("DI Home Invert")]
        public bool _DIHome_Invert = false;
        ///<summary> DI到位訊號是否B接反向 </summary>
        [Description("DI Reach Invert")]
        public bool _DIReach_Invert = false;
        ///<summary> DO觸發訊號是否B接反向 </summary>
        [Description("DO Trigger Invert")]
        public bool _DOTrigger_Invert = false;


        #region //ucDIOInfo1版本使用的
        ///<summary> DI Color </summary>
        [Description("(Skip)DI Color")]
        public Color _DIColor;
        ///<summary> DO Color </summary>
        [Description("(Skip)DO Color")]
        public Color _DOColor;
        ///<summary> DI LED 是否橫向顯示 </summary>
        [Description("(Skip)DI LED Horizontal Display")]
        public bool _IsDILEDHorizontalDisplay;
        ///<summary> Home Reach是否反向 </summary>
        [Description("(Skip)Home Reach Invert")]
        public bool _IsHomeReachInvert;
        #endregion

        #endregion

        #region 建構式 與 解建構式

        public clsDataDIO() 
        {
        }
        ~clsDataDIO() 
        {
        }
        #endregion


    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtTeach
{

    public partial class ucJogTeach : UserControl
    {
        #region//========== Event ==========
        public delegate void EventMouseDown(ucJogTeach sender) ;
        public delegate void EventMouseUp(ucJogTeach sender) ;
        public delegate void EventGoValueClick(ucJogTeach sender) ;
        public delegate void EventGoSafeClick(ucJogTeach sender) ;
        public delegate void EventSetValueChanged(ucJogTeach sender, clsEnum.enuPosName p_PosName, double OraginalValue, double NewValue) ;
        public delegate void EventJogMouseEnter(ucJogTeach sender, Point p_Position, string s_ImagePath) ;
        public delegate void EventJogMouseLeave(ucJogTeach sender) ;
        public delegate void EventFollowControlChanged(ucJogTeach sender) ;
        
        [Description("連續移動事件, 如果無宣告則使用內部預設函式") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventMouseDown m_EventJogAMouseDown;
        [Description("連續移動停止事件, 如果無宣告則使用內預設立函式") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventMouseUp m_EventJogAMouseUp;
        [Description("連續移動事件, 如果無宣告則使用內部預設函式") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventMouseDown m_EventJogBMouseDown;
        [Description("連續移動停止事件, 如果無宣告則使用內預設立函式") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventMouseUp m_EventJogBMouseUp;
        [Description("絕對移動事件, 如果無宣告則使用內部預設函式") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventGoValueClick m_EventGoValueClick;
        [Description("絕對移動事件, 如果無宣告則使用內部預設函式") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventGoSafeClick m_EventGoSafeClick;

        [Description("設定參數變更事件") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventSetValueChanged m_EventSetValueChanged;

        [Description("鼠標進入Jog按鍵事件") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventJogMouseEnter m_EventJogAMouseEnter;
        [Description("鼠標進入Jog按鍵事件") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventJogMouseEnter m_EventJogBMouseEnter;
        [Description("鼠標離開Jog按鍵事件") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventJogMouseLeave m_EventJogAMouseLeave;
        [Description("鼠標離開Jog按鍵事件") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventJogMouseLeave m_EventJogBMouseLeave;

        [Description("元件追隨設定變更事件") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventFollowControlChanged m_EventFollowControlChanged;
        #endregion

        #region//========== 屬性參數 ===========

        [Description("是否使用JobTeach元件") ]
        [DisplayName("_Enable Jog Teach") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public bool _JogTeach_Enable
        {
            set
            {
                m_clsJogTeachInfo._JogTeach_Enable = value;
                this.Visible = value;
            }
            get
            {
                return m_clsJogTeachInfo._JogTeach_Enable;
            }
        }

        [Description("取得或設定軸動標題名稱") ]
        [DisplayName("Axis Name") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public string _AxisTitle_Str
        {
            set
            {
                m_clsJogTeachInfo._AxisTitle_String = value;
                txtAxisName.Text = value.ToString() ;
            }
            get
            {
                return m_clsJogTeachInfo._AxisTitle_String;
            }
        }

        [Description("軸移動單位") ]
        [DisplayName("Axis Unit") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public string _AxisUnit_String
        {
            set
            {
                m_clsJogTeachInfo._AxisUnit_String = value;
                btnFunction.Text = value;
            }
            get
            {
                return m_clsJogTeachInfo._AxisUnit_String;
            }
        }

        private clsEnum.enuAxis? m_AxisName_Enum = null;
        [Description("取得或設定指定移動軸名稱") ]
        [DisplayName("Axis Enum") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public clsEnum.enuAxis? _AxisName_Enum
        {
            get
            {
                if (m_clsJogTeachInfo._AxisEnum_String == "") 
                {
                    m_AxisName_Enum = null;
                    return null;
                }
                else if (m_clsJogTeachInfo._AxisEnum_String == m_AxisName_Enum.ToString() ) 
                {
                    return m_AxisName_Enum;
                }
                else
                {
                    foreach (clsEnum.enuAxis AxisName in Enum.GetValues(typeof(clsEnum.enuAxis) ) ) 
                    {
                        if (AxisName.ToString() == m_clsJogTeachInfo._AxisEnum_String) 
                        {
                            m_AxisName_Enum = AxisName;
                            return AxisName;
                        }
                    }
                    m_AxisName_Enum = null;
                    return null;
                }
            }
            set
            {
                if (value != null) 
                {
                    m_AxisName_Enum = value;
                    m_clsJogTeachInfo._AxisEnum_String = m_AxisName_Enum.ToString() ;
                }
                else
                {
                    m_AxisName_Enum = value;
                    m_clsJogTeachInfo._AxisEnum_String = "";
                }
            }
        }

        [Description("是否可使用點位教導") ]
        [DisplayName("_Position Set Enable") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public bool _PosSet_Enable
        {
            set
            {
                m_clsJogTeachInfo._PosSet_Enable = value;
                btnSetValue.Enabled = value;
                numSetValue.Enabled = value;
            }
            get
            {
                return m_clsJogTeachInfo._PosSet_Enable;
            }
        }

        [Description("取得或設定對應參數名稱") ]
        [DisplayName("Position Name") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public string _PosName_String
        {
            set
            {
                m_clsJogTeachInfo._PosName_String = value;
                txtPositionName.Text = value;
            }
            get
            {
                return m_clsJogTeachInfo._PosName_String;
            }
        }

        private clsEnum.enuPosName? m_PosName_Enum = null;
        [Description("取得或設定對應參數名稱") ]
        [DisplayName("Position Enum") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public clsEnum.enuPosName? _PosName_Enum
        {
            get
            {
                if (m_clsJogTeachInfo._PosEnum_String == "") 
                {
                    m_PosName_Enum = null;
                    return null;
                }
                else if (m_clsJogTeachInfo._PosEnum_String == m_PosName_Enum.ToString() ) 
                {
                    return m_PosName_Enum;
                }
                else
                {
                    foreach (clsEnum.enuPosName PosName in Enum.GetValues(typeof(clsEnum.enuPosName) ) ) 
                    {
                        if (PosName.ToString() == m_clsJogTeachInfo._PosEnum_String) 
                        {
                            m_PosName_Enum = PosName;
                            return PosName;
                        }
                    }
                    m_clsJogTeachInfo._PosEnum_String = "";
                    m_PosName_Enum = null;
                    return null;
                }
            }
            set
            {
                if (value != null) 
                {
                    m_PosName_Enum = value;
                    m_clsJogTeachInfo._PosEnum_String = m_PosName_Enum.ToString() ;
                }
                else
                {
                    m_PosName_Enum = value;
                    m_clsJogTeachInfo._PosEnum_String = "";
                }
            }
        }
        
        [Description("取得或設定設定安全位") ]
        [DisplayName("Safe Position Value") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public double _SafePosition
        {
            set
            {
                m_clsJogTeachInfo._SafePosition = value;
            }
            get
            {
                return m_clsJogTeachInfo._SafePosition;
            }
        }

        private clsEnum.enuPosName? m_SafePosName_Enum = null;
        [Description("取得或設定對應安全位參數名稱") ]
        [DisplayName("Safe Position Enum") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public clsEnum.enuPosName? _SafePosName_Enum
        {
            get
            {
                if (m_clsJogTeachInfo._SafePositionEnum_String == "") 
                {
                    m_SafePosName_Enum = null;
                    return null;
                }
                else if (m_clsJogTeachInfo._SafePositionEnum_String == m_SafePosName_Enum.ToString() ) 
                {
                    return m_SafePosName_Enum;
                }
                else
                {
                    foreach (clsEnum.enuPosName PosName in Enum.GetValues(typeof(clsEnum.enuPosName) ) ) 
                    {
                        if (PosName.ToString() == m_clsJogTeachInfo._SafePositionEnum_String) 
                        {
                            m_SafePosName_Enum = PosName;
                            return PosName;
                        }
                    }
                    m_clsJogTeachInfo._SafePositionEnum_String = "";
                    m_SafePosName_Enum = null;
                    return null;
                }
            }
            set
            {
                if (value != null) 
                {
                    m_SafePosName_Enum = value;
                    m_clsJogTeachInfo._SafePositionEnum_String = m_SafePosName_Enum.ToString() ;
                }
                else
                {
                    m_SafePosName_Enum = value;
                    m_clsJogTeachInfo._SafePositionEnum_String = "";
                }
            }
        }

        [Description("取得或設定設定移動方向") ]
        [DisplayName("Jog Move Direction") , CategoryAttribute("ArtMMI") , Browsable(true) , DefaultValue(enuMoveDir.Positive) ]
        public enuMoveDir _JogMoveDir
        {
            set
            {
                m_clsJogTeachInfo._JogMoveDir = value;
            }
            get
            {
                return m_clsJogTeachInfo._JogMoveDir;
            }
        }

        [Description("取得或設定JogA文字") ]
        [DisplayName("JogA Text") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public string _JogAText_String
        {
            set
            {
                m_clsJogTeachInfo._JogAText_String = value;
                btnJogA.Text = value;
            }
            get
            {
                return m_clsJogTeachInfo._JogAText_String;
            }
        }

        [Description("選擇移動方向圖片") ]
        [DisplayName("JogA Image") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        [System.ComponentModel.Editor(typeof(FileSelectorTypeEditor) , typeof(System.Drawing.Design.UITypeEditor) ) ]
        public string _JogAImg_String
        {
            set
            {
                m_clsJogTeachInfo._JobAImg_String = value;
                SetBackGroundImage(btnJogA, value) ;
            }
            get
            {
                return m_clsJogTeachInfo._JobAImg_String;
            }
        }

        [Description("選擇移動方向圖片") ]
        [DisplayName("JogA Location") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public Point _JogAImgPoint
        {
            set
            {
                m_clsJogTeachInfo._JogAImageDirPoint = value;
            }
            get
            {
                return m_clsJogTeachInfo._JogAImageDirPoint;
            }
        }

        [Description("取得或設定JogB文字") ]
        [DisplayName("JogB Text") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public string _JogBText_String
        {
            set
            {
                m_clsJogTeachInfo._JogBText_String = value;
                btnJogB.Text = value;
            }
            get
            {
                return m_clsJogTeachInfo._JogBText_String;
            }
        }

        [Description("選擇移動方向圖片") ]
        [DisplayName("JogB Image") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        [System.ComponentModel.Editor(typeof(FileSelectorTypeEditor) , typeof(System.Drawing.Design.UITypeEditor) ) ]
        public string _JogBImg_String
        {
            set
            {
                m_clsJogTeachInfo._JobBImg_String = value;
                SetBackGroundImage(btnJogB, value) ;
            }
            get
            {
                return m_clsJogTeachInfo._JobBImg_String;
            }
        }
        
        [Description("選擇移動方向圖片") ]
        [DisplayName("JogB Location") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public Point _JogBImgPoint
        {
            set
            {
                m_clsJogTeachInfo._JogBImageDirPoint = value;
            }
            get
            {
                return m_clsJogTeachInfo._JogBImageDirPoint;
            }
        }

        [Description("控制元件模式") ]
        [DisplayName("Display Mode") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public clsDataJogTeach.enuDisplayMode _DisplayMode
        {
            get
            {
                return m_clsJogTeachInfo._enuDisplayMode;
            }
            set
            {
                m_clsJogTeachInfo._enuDisplayMode = value;
                UpdateControls(m_clsJogTeachInfo) ;
            }
        }

        [Description("元件追隨設定") ]
        [DisplayName("Follow Control") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public bool _FollowControl
        {
            get
            {
                return m_clsJogTeachInfo._FollowUpperControls;
            }
            set
            {
                m_clsJogTeachInfo._FollowUpperControls = value;
                UpdateControls(m_clsJogTeachInfo) ;

            }
        }


        #endregion

        #region//========== Public 參數 ==========

        private clsDataJogTeach m_clsJogTeachInfo = new clsDataJogTeach() ;
        [Description("資料結果指標") ]
        [DisplayName("clsDataJogTeach") , CategoryAttribute("ArtMMI") , Browsable(false) ]
        public clsDataJogTeach _1clsJogTeachInfo
        {
            set
            {
                if (value != null) 
                {
                    m_clsJogTeachInfo = value;
                    UpdateControls(m_clsJogTeachInfo) ;
                }
            }
            get
            {
                return m_clsJogTeachInfo;
            }
        }

        
        #endregion

        #region//========== Private 參數 ===========
        static private Dictionary<string, double> listUndoValue = new Dictionary<string, double>();

        private ToolTip m_ToolTip = new ToolTip() ;

        private int ReflashCount = 0;
        #endregion
        
        #region//========== 建構式 ==========
        public ucJogTeach() 
        {
            InitializeComponent() ;
        }
        #endregion

        #region//========== public Function ===========
        public void UpdateControls(clsDataJogTeach p_DataJogTeach) 
        {
            try
            {
                Translation();
                //_clsJogTeachInfo = p_DataJogTeach;
                this.Visible = _JogTeach_Enable;
                btnSetValue.Enabled = _PosSet_Enable;
                numSetValue.Enabled = _PosSet_Enable;
                btnJogA.Text = clsLanguage.GetTranslation(_JogAText_String);
                btnJogB.Text = clsLanguage.GetTranslation(_JogBText_String);
                txtAxisName.Text = clsLanguage.GetTranslation(_AxisTitle_Str);
                btnFunction.Text = clsLanguage.GetTranslation(_AxisUnit_String.ToString());
                txtPositionName.Text = clsLanguage.GetTranslation(_PosName_String);
                SetBackGroundImage(btnJogA, _JogAImg_String) ;
                SetBackGroundImage(btnJogB, _JogBImg_String) ;
                int TempHeitht = 142;
                if (_PosName_Enum != null) 
                {
                    double Value = ucPosPmt.GetValueDouble((clsEnum.enuPosName) _PosName_Enum) ;
                    if (Value >= 0 || Value <= 0) 
                    {
                        numSetValue.Text = Value.ToString() ;
                    }
                    else
                    {
                        numSetValue.Text = "Null";
                    }
                }
                else
                {
                    numSetValue.Text = "Null";
                }
                this.MaximumSize = new Size(999, 999);
                this.MinimumSize = new Size(0, 0);
                switch (m_clsJogTeachInfo._enuDisplayMode) 
                {
                    case clsDataJogTeach.enuDisplayMode.MotionCtrl:
                        Panel_MotionCtrl1.Visible = true;
                        Panel_PositionSet.Visible = false;
                        Panel_MotionCtrl1.Top = 0;
                        Panel_PositionSet.Top = 88;
                        TempHeitht = 89;
                        break;
                    case clsDataJogTeach.enuDisplayMode.SetPosition:
                        Panel_MotionCtrl1.Visible = false;
                        Panel_PositionSet.Visible = true;
                        Panel_MotionCtrl1.Top = 0;
                        Panel_PositionSet.Top = 0;
                        TempHeitht = 54;
                        break;
                    case clsDataJogTeach.enuDisplayMode.ShowAll:
                    default:
                        Panel_MotionCtrl1.Visible = true;
                        Panel_PositionSet.Visible = true;
                        Panel_MotionCtrl1.Top = 0;
                        Panel_PositionSet.Top = 88;
                        TempHeitht = 143;
                        break;
                }
                panel2.Height = TempHeitht;
                this.MaximumSize = new Size(panel2.Width + 4, TempHeitht + 4);
                this.MinimumSize = new Size(panel2.Width + 4, TempHeitht + 4);
                this.Size = new Size(panel2.Width + 4, TempHeitht + 4);
                if (m_EventFollowControlChanged != null) 
                {
                    m_EventFollowControlChanged(this) ;
                }
            }
            catch
            {
            }
        }

        public void Translation()
        {
            try
            {
                btnSetValue.Text = clsLanguage.GetTranslation("SET");
                clsRoundAngleBtn1.Text = clsLanguage.GetTranslation("GO");
                btnSVON.Text = clsLanguage.GetTranslation("SVON");
                txtALM.Text = clsLanguage.GetTranslation("ALM");
                txtPEL.Text = clsLanguage.GetTranslation("PEL");
                txtMEL.Text = clsLanguage.GetTranslation("MEL");
                txtORG.Text = clsLanguage.GetTranslation("ORG");
                txtINP.Text = clsLanguage.GetTranslation("INP");
                btnSafe.Text = clsLanguage.GetTranslation("SAFE");
            }
            catch
            {

            }
        }

        public void ReflashControls()
        {
            try
            {
                if (_PosName_Enum != null)
                {
                    string PosValue = ucPosPmt.GetValueString((clsEnum.enuPosName)_PosName_Enum);
                    numSetValue.Text = _PosName_Enum == null ? "null" : PosValue == "" ? "0" : PosValue;
                }

                if (this._DisplayMode == clsDataJogTeach.enuDisplayMode.SetPosition)
                {
                    return;
                }
                if (_AxisName_Enum != null)
                {
                    ReflashCount++;
                    switch (ReflashCount)
                    {
                        case 1:
                            txtALM.DisableColor = clsMotionCtrl.GetIoStatus((clsEnum.enuAxis)_AxisName_Enum, clsMotionCtrl.enuMotionIoName.ALM) == true ? Color.Red : Color.FromArgb(177, 194, 214);
                            break;
                        case 2:
                            txtPEL.DisableColor = clsMotionCtrl.GetIoStatus((clsEnum.enuAxis)_AxisName_Enum, clsMotionCtrl.enuMotionIoName.EL_Plus) == true ? Color.FromArgb(75, 167, 175) : Color.FromArgb(177, 194, 214);
                            break;
                        case 3:
                            txtMEL.DisableColor = clsMotionCtrl.GetIoStatus((clsEnum.enuAxis)_AxisName_Enum, clsMotionCtrl.enuMotionIoName.EL_Minus) == true ? Color.FromArgb(75, 167, 175) : Color.FromArgb(177, 194, 214);
                            break;
                        case 4:
                            txtORG.DisableColor = clsMotionCtrl.GetIoStatus((clsEnum.enuAxis)_AxisName_Enum, clsMotionCtrl.enuMotionIoName.ORG) == true ? Color.FromArgb(75, 167, 175) : Color.FromArgb(177, 194, 214);
                            break;
                        case 5:
                            txtINP.DisableColor = clsMotionCtrl.GetIoStatus((clsEnum.enuAxis)_AxisName_Enum, clsMotionCtrl.enuMotionIoName.INP) == true ? Color.FromArgb(75, 167, 175) : Color.FromArgb(177, 194, 214);
                            break;
                        default:
                            ReflashCount = 0;
                            btnSVON.HoverBackColor = clsMotionCtrl.GetIoStatus((clsEnum.enuAxis)_AxisName_Enum, clsMotionCtrl.enuMotionIoName.SVON) == true ? Color.FromArgb(75, 167, 175) : Color.FromArgb(239, 102, 124);
                            break;

                    }
                    ucMotionSetting.SetAllSpeed();
                    double CurrentPosition = (double)clsMotionCtrl.GetPosMm((clsEnum.enuAxis)_AxisName_Enum);


                    if ((CurrentPosition >= 0 || CurrentPosition <= 0) == true)
                    {
                        txtCurrentPosition.Text = CurrentPosition.ToString("F3");
                    }
                    else
                    {
                        txtCurrentPosition.Text = "Null";
                    }
                }
                else
                {
                    txtALM.DisableColor = Color.FromArgb(177, 194, 214);
                    txtPEL.DisableColor = Color.FromArgb(177, 194, 214);
                    txtMEL.DisableColor = Color.FromArgb(177, 194, 214);
                    txtORG.DisableColor = Color.FromArgb(177, 194, 214);
                    txtINP.DisableColor = Color.FromArgb(177, 194, 214);
                    btnSVON.HoverBackColor = Color.FromArgb(239, 102, 124);
                    txtCurrentPosition.Text = "Null";
                }
            }
            catch
            {
                txtALM.BackColor = Color.Transparent;
                txtPEL.BackColor = Color.Transparent;
                txtMEL.BackColor = Color.Transparent;
                txtORG.BackColor = Color.Transparent;
                txtINP.BackColor = Color.Transparent;
                btnSVON.BackColor = Color.Transparent;
                txtCurrentPosition.Text = "Null";
            }
            this.Refresh();
        }
        #endregion

        #region//========== private Function ===========

        private void SetJogColor(Control Item, bool IsSelected) 
        {
            Item.BackColor = IsSelected ? Color.Gold : Color.Transparent;
            Item.ForeColor = IsSelected ? Color.Blue : Color.Gray;
        }
        private void SetSpeed() 
        {
            clsEnum.enuAxis p_Axis = (clsEnum.enuAxis) _AxisName_Enum;
            int SpeedPercentage = 20;
            double MaxVelocity = ucMotionSetting.GetMaxSpeed(p_Axis) ;
            double TAcc = ucMotionSetting.GetTacc(p_Axis) ;
            double SAcc = ucMotionSetting.GetSacc(p_Axis) ;
            double Velocity = (MaxVelocity * SpeedPercentage) / 100;
            clsMotionCtrl.SetAxisVelMm(p_Axis, Velocity, 0, TAcc, TAcc, SAcc, SAcc) ;
        }
        private void SetBackGroundImage(Control Item, string ImagePath) 
        {
            try
            {
                Item.BackgroundImage = null;
                Item.BackgroundImageLayout = ImageLayout.Zoom;                
                if (System.IO.File.Exists(ImagePath) == true) 
                {
                    Item.BackgroundImage = Image.FromFile(ImagePath) ;
                }
            }
            catch
            {
            }
        }

        #endregion

        #region//========== Controls Event ==========
        private void numSetValue_Click(object sender, EventArgs e) 
        {
            try
            {
                if (numSetValue.Enabled == true) 
                {
                    if (_PosName_Enum == null
                        || m_clsJogTeachInfo._AxisEnum_String == null) 
                    {
                        formMessageBox.Show("Position setting is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
                        return;
                    }
                    else
                    {
                        double OriginalValue = ucPosPmt.GetValueDouble((clsEnum.enuPosName) _PosName_Enum) ;
                        if (FormNumBox.GetSingleton().ShowDialog(this, OriginalValue.ToString() , 99999, -99999, 3) 
                                == DialogResult.OK) 
                        {
                            numSetValue.Text = FormNumBox.GetSingleton().NumBoxValue.ToString() ;
                            ucPosPmt.SaveValue((clsEnum.enuPosName) _PosName_Enum, FormNumBox.GetSingleton().NumBoxValue.ToString() ) ;
                            if (listUndoValue.Keys.Contains(_PosName_Enum.ToString() ) == false) 
                            { listUndoValue.Add(_PosName_Enum.ToString() , 0) ; }
                            listUndoValue[_PosName_Enum.ToString() ] = OriginalValue;
                            if (m_EventSetValueChanged != null) 
                            {
                                m_EventSetValueChanged(this, (clsEnum.enuPosName) _PosName_Enum, OriginalValue, FormNumBox.GetSingleton().NumBoxValue) ;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        private void btnSetValue_Click(object sender, EventArgs e) 
        {
            try
            {
                if (numSetValue.Enabled == true) 
                {
                    if (formMessageBox.Show("Are you sure want to set value?",
                        "Set Value", MessageBoxButtons.YesNo) == DialogResult.Yes) 
                    {
                        if (m_clsJogTeachInfo._AxisEnum_String == null
                            || _PosName_Enum == null) 
                        {
                            formMessageBox.Show("Position setting is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
                            return;
                        }
                        else
                        {
                            double CurrentPos = 0;
                            try
                            {
                                CurrentPos = (double) clsMotionCtrl.GetPosMm((clsEnum.enuAxis) _AxisName_Enum) ;
                                if ((CurrentPos >=0 || CurrentPos <=0) == false) 
                                {
                                    formMessageBox.Show("Get Current Position Fail!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
                                    return;
                                }
                            }
                            catch
                            {
                                formMessageBox.Show("Get Current Position Fail!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
                                return;
                            }
                            double OriginalValue = ucPosPmt.GetValueDouble((clsEnum.enuPosName) _PosName_Enum) ;
                            numSetValue.Text = CurrentPos.ToString("F3") ;
                            ucPosPmt.SaveValue((clsEnum.enuPosName) _PosName_Enum, CurrentPos.ToString("F3") ) ;
                            if (listUndoValue.Keys.Contains(_PosName_Enum.ToString() ) == false) 
                            { listUndoValue.Add(_PosName_Enum.ToString() , 0) ; }
                            listUndoValue[_PosName_Enum.ToString()] = OriginalValue;
                            if (m_EventSetValueChanged != null) 
                            {
                                m_EventSetValueChanged(this, (clsEnum.enuPosName) _PosName_Enum, OriginalValue, (double) CurrentPos) ;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        private void btnGoValue_Click(object sender, EventArgs e) 
        {
            try
            {
                if (m_EventGoValueClick != null) 
                {
                    m_EventGoValueClick(this) ;
                }
                else
                {
                    formMessageBox.Show("m_EventGoValueClick is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
                    return;
                }
            }
            catch
            {
            }
        }
        private void btnSafe_Click(object sender, EventArgs e) 
        {
            try
            {
                if (m_EventGoValueClick != null) 
                {
                    m_EventGoSafeClick(this) ;
                }
                else
                {
                    formMessageBox.Show("m_EventGoSafeClick is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
                    return;
                }
            }
            catch
            {
            }

        }
        private void btnSVON_Click(object sender, EventArgs e) 
        {
            try
            {
                if (_AxisName_Enum != null) 
                {
                    clsMotionCtrl.EmgStop((clsEnum.enuAxis) _AxisName_Enum) ;
                    if (clsMotionCtrl.GetIoStatus((clsEnum.enuAxis) _AxisName_Enum, clsMotionCtrl.enuMotionIoName.SVON) == true)
                    {
                        clsMotionCtrl.SetServo((clsEnum.enuAxis) _AxisName_Enum, false) ;
                        clsLog.Log("ButtonLog" , "(Controls) ServoOff Click => Axis Name : " + _AxisName_Enum.ToString() ) ;
                    }
                    else
                    {
                        clsMotionCtrl.SetServo((clsEnum.enuAxis) _AxisName_Enum, true) ;
                        clsLog.Log("ButtonLog", "(Controls) ServoOn Click => Axis Name : " + _AxisName_Enum.ToString());
                    } 
                    ReflashCount = -1;
                }
                else
                {
                    formMessageBox.Show("Position setting is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
                    return;
                }
            }
            catch
            {
            }
        }
        private void btnJogA_MouseEnter(object sender, EventArgs e) 
        {
            try
            {
                if (m_EventJogAMouseEnter != null) 
                {
                    m_EventJogAMouseEnter(this, _JogAImgPoint, _JogAImg_String) ;
                }
            }
            catch
            {
            }
        }
        private void btnJogA_MouseLeave(object sender, EventArgs e) 
        {
            try
            {
                if (m_EventJogAMouseLeave != null) 
                {
                    m_EventJogAMouseLeave(this) ;
                }
            }
            catch
            {
            }
        }
        private void btnJogB_MouseEnter(object sender, EventArgs e) 
        {
            try
            {
                if (m_EventJogBMouseEnter != null) 
                {
                    m_EventJogBMouseEnter(this, _JogBImgPoint, _JogBImg_String) ;
                }
            }
            catch
            {
            }
        }
        private void btnJogB_MouseLeave(object sender, EventArgs e) 
        {
            try
            {
                if (m_EventJogBMouseLeave != null) 
                {
                    m_EventJogBMouseLeave(this) ;
                }
            }
            catch
            {
            }
        }
        private void btnJogA_MouseDown(object sender, MouseEventArgs e) 
        {
            try
            {
                if (m_EventJogAMouseDown != null) 
                {
                    m_EventJogAMouseDown(this) ;
                }
            }
            catch
            {
            }
        }
        private void btnJogA_MouseUp(object sender, MouseEventArgs e) 
        {
            try
            {
                if (m_EventJogAMouseUp != null) 
                {
                    m_EventJogAMouseUp(this) ;
                }
            }
            catch
            {
            }
        }
        private void btnJogB_MouseDown(object sender, MouseEventArgs e) 
        {
            try
            {
                if (m_EventJogBMouseDown != null) 
                {
                    m_EventJogBMouseDown(this) ;
                }
            }
            catch
            {
            }
        }
        private void btnJogB_MouseUp(object sender, MouseEventArgs e) 
        {
            try
            {
                if (m_EventJogBMouseUp != null) 
                {
                    m_EventJogBMouseUp(this) ;
                }
            }
            catch
            {
            }
        }
        #endregion


        #region//========== 輔助工具 屬性選擇路徑 ==========

        /// <summary>
        /// Customer UITypeEditor that pops up a
        /// file selector dialog
        /// </summary>
        public class FileSelectorTypeEditor : System.Drawing.Design.UITypeEditor
        {
            public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) 
            {
                if (context == null || context.Instance == null) 
                    return base.GetEditStyle(context) ;
                return System.Drawing.Design.UITypeEditorEditStyle.Modal;
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) 
            {
                IWindowsFormsEditorService editorService;

                if (context == null || context.Instance == null || provider == null) 
                    return value;

                try
                {
                    // get the editor service, just like in windows forms
                    editorService = (IWindowsFormsEditorService) 
                       provider.GetService(typeof(IWindowsFormsEditorService) ) ;

                    OpenFileDialog dlg = new OpenFileDialog() ;
                    dlg.Filter = "All Files (*.*) |*.*";
                    dlg.CheckFileExists = true;

                    string filename = (string) value;
                    if (! System.IO.File.Exists(filename) ) 
                        filename = null;
                    dlg.FileName = filename;

                    using (dlg) 
                    {
                        DialogResult res = dlg.ShowDialog() ;
                        if (res == DialogResult.OK) 
                        {
                            filename = dlg.FileName;
                        }
                    }
                    return filename;

                }
                finally
                {
                    editorService = null;
                }
            }
        }


        #endregion


    }
}
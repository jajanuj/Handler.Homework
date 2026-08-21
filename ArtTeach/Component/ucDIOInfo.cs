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
    public partial class ucDIOInfo : ArtCommonLib.ucBaseUserControl
    {
        #region //===================== 區域變數設置 =====================

        event EventHandler DoTrirgger;
        public event EventHandler _DoTrirgger
        {
            remove
            {
                DoTrirgger -= value;
            }
            add
            {
                DoTrirgger += value;
            }
        }

        event EventHandler DIOLog;
        public event EventHandler _DIOLog
        {
            remove
            {
                DIOLog -= value;
            }
            add
            {
                DIOLog += value;
            }
        }

        ToolTip m_ToolTipDioName = new ToolTip() ;

        private clsDataDIO m_clsDIOInfo = new clsDataDIO() ;
        public clsDataDIO _1clsDIOInfo
        {
            set
            {
                if (value != null) 
                {
                    m_clsDIOInfo = value;
                    UpdateControls(value) ;
                }
            }
            get
            {
                return m_clsDIOInfo;
            }
        }

        /// <summary> 是否使用DIO元件 </summary>
        [Description("是否使用DIO元件")]
        [DisplayName("Enable DIO"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public bool _bIsDIOEnable
        {
            set
            {
                m_clsDIOInfo._IsDIOEnable = value;
                this.Visible = value;
            }
            get
            {
                return m_clsDIOInfo._IsDIOEnable;
            }
        }

        /// <summary>取得或設定DO 狀態顏色</summary>
        [Description("取得或設定DO 狀態顏色")]
        [DisplayName("DO On Button Color"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public Color _DoOnBtnColor
        {
            set
            {
                m_clsDIOInfo._DoOnBtnColor = value;
            }
            get
            {
                return m_clsDIOInfo._DoOnBtnColor;
            }
        }

        /// <summary>取得或設定DO 狀態顏色</summary>
        [Description("取得或設定DO 狀態顏色")]
        [DisplayName("DO On Back Color"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public Color _DoOnBackColor
        {
            set
            {
                m_clsDIOInfo._DoOnBackColor = value;
            }
            get
            {
                return m_clsDIOInfo._DoOnBackColor;
            }
        }

        /// <summary>取得或設定DO 狀態顏色</summary>
        [Description("取得或設定DO 狀態顏色")]
        [DisplayName("DO Off Button Color"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public Color _DoOffBtnColor
        {
            set
            {
                m_clsDIOInfo._DoOffBtnColor = value;
            }
            get
            {
                return m_clsDIOInfo._DoOffBtnColor;
            }
        }

        /// <summary>取得或設定DO 狀態顏色</summary>
        [Description("取得或設定DO 狀態顏色")]
        [DisplayName("DO Off Back Color"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public Color _DoOffBackColor
        {
            set
            {
                m_clsDIOInfo._DoOffBackColor = value;
            }
            get
            {
                return m_clsDIOInfo._DoOffBackColor;
            }
        }

        /// <summary>取得或設定DI 狀態顏色</summary>
        [Description("取得或設定DI 狀態顏色") ]
        [DisplayName("DI Reach Color") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public Color _DiReachColor
        {
            set
            {
                m_clsDIOInfo._DiReachColor = value;
            }
            get
            {
                return m_clsDIOInfo._DiReachColor;
            }
        }

        /// <summary>取得或設定DI 狀態顏色</summary>
        [Description("取得或設定DI 狀態顏色")]
        [DisplayName("DI Home Color"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public Color _DiHomeColor
        {
            set
            {
                m_clsDIOInfo._DiHomeColor = value;
            }
            get
            {
                return m_clsDIOInfo._DiHomeColor;
            }
        }

        /// <summary>取得或設定DIO 名稱</summary>
        [Description("取得或設定DIO 名稱")]
        [DisplayName("Name DI Reach"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public string _DiReachName
        {
            set
            {
                m_clsDIOInfo._DiReachName = value;
                lblDIReach.Text = _DIReach == null ? "null" : value;
            }
            get
            {
                return m_clsDIOInfo._DiReachName;
            }
        }

        /// <summary>取得或設定DIO 名稱</summary>
        [Description("取得或設定DIO 名稱")]
        [DisplayName("Name DI Home"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public string _DiHomeName
        {
            set
            {
                m_clsDIOInfo._DiHomeName = value;
                lblDIHome.Text = _DIHome == null ? "null" : value;
            }
            get
            {
                return m_clsDIOInfo._DiHomeName;
            }
        }

        /// <summary>取得或設定DIO 名稱</summary>
        [Description("取得或設定DIO 名稱")]
        [DisplayName("Name DO"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public string _DIOName
        {
            set
            {
                m_clsDIOInfo._DIOName = value;
                btnDOTrigger.Text = value;
            }
            get
            {
                return m_clsDIOInfo._DIOName;
            }
        }


        private clsEnum.enuDi? m_DIHome = null;
        /// <summary>取得或設定DI Home對應參數名稱</summary>
        [Description("取得或設定DI Home對應參數名稱")]
        [DisplayName("DI Home"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public clsEnum.enuDi? _DIHome
        {
            get
            {
                if (m_clsDIOInfo._DIHomeEnum_String == "")
                {
                    m_DIHome = null;
                    return null;
                }
                else if (m_clsDIOInfo._DIHomeEnum_String == m_DIHome.ToString())
                {
                    return m_DIHome;
                }
                else
                {
                    foreach (clsEnum.enuDi DIName in Enum.GetValues(typeof(clsEnum.enuDi)))
                    {
                        if (DIName.ToString() == m_clsDIOInfo._DIHomeEnum_String)
                        {
                            m_DIHome = DIName;
                            return m_DIHome;
                        }
                    }
                    m_clsDIOInfo._DIHomeEnum_String = "";
                    m_DIHome = null;
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    m_DIHome = value;
                    m_clsDIOInfo._DIHomeEnum_String = m_DIHome.ToString();
                }
                else
                {
                    m_DIHome = value;
                    m_clsDIOInfo._DIHomeEnum_String = "";
                }
            }
        }

        /// <summary>取得或設定DI Home參數是否反向</summary>
        [Description("取得或設定DI Home參數是否反向")]
        [DisplayName("DI Home Invert"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public bool _DIHome_Invert
        {
            set
            {
                m_clsDIOInfo._DIHome_Invert = value;
            }
            get
            {
                return m_clsDIOInfo._DIHome_Invert;
            }
        }

        private clsEnum.enuDi? m_DIReach = null;
        /// <summary>取得或設定DI Reach對應參數名稱</summary>
        [Description("取得或設定DI Reach對應參數名稱")]
        [DisplayName("DI Reach"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public clsEnum.enuDi? _DIReach
        {
            get
            {
                if (m_clsDIOInfo._DIReachEnum_String == "")
                {
                    m_DIReach = null;
                    return null;
                }
                else if (m_clsDIOInfo._DIReachEnum_String == m_DIReach.ToString())
                {
                    return m_DIReach;
                }
                else
                {
                    foreach (clsEnum.enuDi DIName in Enum.GetValues(typeof(clsEnum.enuDi)))
                    {
                        if (DIName.ToString() == m_clsDIOInfo._DIReachEnum_String)
                        {
                            m_DIReach = DIName;
                            return m_DIReach;
                        }
                    }
                    m_clsDIOInfo._DIReachEnum_String = "";
                    m_DIReach = null;
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    m_DIReach = value;
                    m_clsDIOInfo._DIReachEnum_String = m_DIReach.ToString();
                }
                else
                {
                    m_DIReach = value;
                    m_clsDIOInfo._DIReachEnum_String = "";
                }
            }
        }

        /// <summary>取得或設定DI Reach參數是否反向</summary>
        [Description("取得或設定DI Reach參數是否反向")]
        [DisplayName("DI Reach Invert"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public bool _DIReach_Invert
        {
            set
            {
                m_clsDIOInfo._DIReach_Invert = value;
            }
            get
            {
                return m_clsDIOInfo._DIReach_Invert;
            }
        }

        private clsEnum.enuDo? m_DOTrigger = null;
        /// <summary>取得或設定DO Trigger 1對應參數名稱</summary>
        [Description("取得或設定DO Trigger 1對應參數名稱")]
        [DisplayName("DO Trigger 1"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public clsEnum.enuDo? _DOTrigger
        {
            get
            {
                if (m_clsDIOInfo._DOTriggerEnum_String == "")
                {
                    m_DOTrigger = null;
                    return null;
                }
                else if (m_clsDIOInfo._DOTriggerEnum_String == m_DOTrigger.ToString())
                {
                    return m_DOTrigger;
                }
                else
                {
                    foreach (clsEnum.enuDo DoName in Enum.GetValues(typeof(clsEnum.enuDo)))
                    {
                        if (DoName.ToString() == m_clsDIOInfo._DOTriggerEnum_String)
                        {
                            m_DOTrigger = DoName;
                            return m_DOTrigger;
                        }
                    }
                    m_clsDIOInfo._DOTriggerEnum_String = "";
                    m_DOTrigger = null;
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    m_DOTrigger = value;
                    m_clsDIOInfo._DOTriggerEnum_String = m_DOTrigger.ToString();
                }
                else
                {
                    m_DOTrigger = value;
                    m_clsDIOInfo._DOTriggerEnum_String = "";
                }
            }
        }


        private clsEnum.enuDo? m_DOTrigger2 = null;
        /// <summary>取得或設定DO Trigger 2對應參數名稱</summary>
        [Description("取得或設定DO Trigger 2對應參數名稱")]
        [DisplayName("DO Trigger 2"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public clsEnum.enuDo? _DOTrigger2
        {
            get
            {
                if (m_clsDIOInfo._DOTrigger2Enum_String == "")
                {
                    m_DOTrigger2 = null;
                    return null;
                }
                else if (m_clsDIOInfo._DOTrigger2Enum_String == m_DOTrigger2.ToString())
                {
                    return m_DOTrigger2;
                }
                else
                {
                    foreach (clsEnum.enuDo DoName in Enum.GetValues(typeof(clsEnum.enuDo)))
                    {
                        if (DoName.ToString() == m_clsDIOInfo._DOTrigger2Enum_String)
                        {
                            m_DOTrigger2 = DoName;
                            return m_DOTrigger2;
                        }
                    }
                    m_clsDIOInfo._DOTrigger2Enum_String = "";
                    m_DOTrigger2 = null;
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    m_DOTrigger2 = value;
                    m_clsDIOInfo._DOTrigger2Enum_String = m_DOTrigger2.ToString();
                }
                else
                {
                    m_DOTrigger2 = value;
                    m_clsDIOInfo._DOTrigger2Enum_String = "";
                }
            }
        }

        /// <summary>取得或設定DO Trigger參數是否反向</summary>
        [Description("取得或設定DO Trigger參數是否反向")]
        [DisplayName("DO Trigger Invert"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public bool _DOTrigger_Invert
        {
            set
            {
                m_clsDIOInfo._DOTrigger_Invert = value;
            }
            get
            {
                return m_clsDIOInfo._DOTrigger_Invert;
            }
        }

        #endregion

        #region //===================== 必要函式設置 =====================


        /// <summary>
        /// 物件建立請利用 GetSingleton() ，除非特殊需求
        /// </summary>
        public ucDIOInfo() 
        {
            InitializeComponent() ;
            ucParameter.Add(this) ;
        }

        #endregion

        #region //===================== public 函式設置 ==================
        public void UpdateControls(clsDataDIO p_DataDIO) 
        {
            try
            {
                this._bIsDIOEnable = p_DataDIO._IsDIOEnable;
                this._DoOnBtnColor = p_DataDIO._DoOnBtnColor;
                this._DoOnBackColor = p_DataDIO._DoOnBackColor;
                this._DoOffBtnColor = p_DataDIO._DoOffBtnColor;
                this._DoOffBackColor = p_DataDIO._DoOffBackColor;
                this._DiReachColor = p_DataDIO._DiReachColor;
                this._DiHomeColor = p_DataDIO._DiHomeColor;
                this._DiReachName = clsLanguage.GetTranslation(p_DataDIO._DiReachName);
                this._DiHomeName = clsLanguage.GetTranslation(p_DataDIO._DiHomeName);
                this._DIOName = clsLanguage.GetTranslation(p_DataDIO._DIOName);
                this._DIHome_Invert = p_DataDIO._DIHome_Invert;
                this._DIReach_Invert = p_DataDIO._DIReach_Invert;
                this._DOTrigger_Invert = p_DataDIO._DOTrigger_Invert;
            }
            catch
            {
            }
        }
        /// <summary> 更新DIO 狀態 </summary>
        public void ReflashControls() 
        {
            if (_DIHome != null)
            {
                if (clsDioCtrl.GetDi((clsEnum.enuDi)_DIHome) ^ _DIHome_Invert)
                {
                    if (lblDIHomeLED.DisableColor != m_clsDIOInfo._DiHomeColor)
                    {
                        lblDIHomeLED.DisableColor = m_clsDIOInfo._DiHomeColor;
                        this.Refresh();
                    }
                }
                else
                {
                    if (lblDIHomeLED.DisableColor != this._DoOffBackColor)
                    {
                        lblDIHomeLED.DisableColor = this._DoOffBackColor;
                        this.Refresh();
                    }
                }
            }
            else
            {
                if (lblDIHomeLED.DisableColor != this._DoOffBackColor)
                {
                    lblDIHomeLED.DisableColor = this._DoOffBackColor;
                    this.Refresh();
                }
            }

            if (_DIReach != null) 
            {
                if (clsDioCtrl.GetDi((clsEnum.enuDi) _DIReach) ^ _DIReach_Invert) 
                {
                    if (lblDIReachLED.DisableColor != m_clsDIOInfo._DiReachColor)
                    {
                        lblDIReachLED.DisableColor = m_clsDIOInfo._DiReachColor;
                        this.Refresh();
                    }
                }
                else
                {
                    if (lblDIReachLED.DisableColor != this._DoOffBackColor)
                    {
                        lblDIReachLED.DisableColor = this._DoOffBackColor;
                        this.Refresh();
                    }
                }
            }
            else
            {
                if (lblDIReachLED.DisableColor != this._DoOffBackColor)
                {
                    lblDIReachLED.DisableColor = this._DoOffBackColor;
                    this.Refresh();
                }
            }
            if (_DOTrigger != null)
            {
                clsSwitchButton1.Visible = true;
                if (_DOTrigger_Invert) 
                {
                    clsSwitchButton1._Status = !clsDioCtrl.GetDo((clsEnum.enuDo) _DOTrigger);
                }
                else
                {
                    clsSwitchButton1._Status = clsDioCtrl.GetDo((clsEnum.enuDo) _DOTrigger);
                }
            }
            else
            {
                clsSwitchButton1.Visible = false;
            }
        }

        #endregion

        #region //===================== private 函式設置 =================

        private void UpdateControl() 
        {

        }

        #endregion

        #region//===================== 以下為事件處理 ===================

        private void clsSwitchButton1_Click(object sender, EventArgs e) 
        {
            if (DoTrirgger != null)
            {
                DoTrirgger(this, e);
            }
        }

        private void lblDIReach_MouseEnter(object sender, EventArgs e)
        {
            string sMessage = "null";
            if (sender == btnDOTrigger
                || sender == clsSwitchButton1)
            {
                if (this._DOTrigger != null)
                {
                    sMessage = "[Y" + (int)this._DOTrigger + "]" + this._DOTrigger.ToString();
                }
                m_ToolTipDioName.SetToolTip((Control)sender, sMessage);
            }
            else if (sender == lblDIReach)
            {
                if (this._DIReach != null)
                {
                    sMessage = "[X" + (int)this._DIReach + "]" + this._DIReach.ToString();
                }
                m_ToolTipDioName.SetToolTip((Control)sender, sMessage);
            }
            else if (sender == lblDIHome)
            {
                if (this._DIHome != null)
                {
                    sMessage = "[X" + (int)this._DIHome + "]" + this._DIHome.ToString();
                }
                m_ToolTipDioName.SetToolTip((Control)sender, sMessage);
            }
        }

        private void lblDIReach_MouseLeave(object sender, EventArgs e)
        {
            m_ToolTipDioName.RemoveAll();
        }

        #endregion
    }
}

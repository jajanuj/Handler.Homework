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
    public partial class ucDIOInfo1 : ArtCommonLib.ucBaseUserControl
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
        public clsDataDIO _clsDIOInfo
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

        /// <summary>取得或設定DI 狀態顏色</summary>
        [Description("取得或設定DI 狀態顏色") ]
        [DisplayName("DI Color") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public Color _DIColor
        {
            set
            {
                m_clsDIOInfo._DIColor = value;
            }
            get
            {
                return m_clsDIOInfo._DIColor;
            }
        }

        /// <summary>取得或設定DO 狀態顏色</summary>
        [Description("取得或設定DO 狀態顏色") ]
        [DisplayName("DO Color") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public Color _DOColor
        {
            set
            {
                m_clsDIOInfo._DOColor = value;
            }
            get
            {
                return m_clsDIOInfo._DOColor;
            }
        }


        private clsEnum.enuDi? m_DIHome = null;
        /// <summary>取得或設定DI Home對應參數名稱</summary>
        [Description("取得或設定DI Home對應參數名稱") ]
        [DisplayName("DI Home") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public clsEnum.enuDi? _DIHome
        {
            get
            {
                if (m_clsDIOInfo._DIHomeEnum_String == "") 
                {
                    m_DIHome = null;
                    return null;
                }
                else if (m_clsDIOInfo._DIHomeEnum_String == m_DIHome.ToString() ) 
                {
                    return m_DIHome;
                }
                else
                {
                    foreach (clsEnum.enuDi DIName in Enum.GetValues(typeof(clsEnum.enuDi) ) ) 
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
                    m_clsDIOInfo._DIHomeEnum_String = m_DIHome.ToString() ;
                    if (value != null) 
                    {
                        lblDIHome.Text = "X" + ((int) m_DIHome).ToString() ;
                        lblDIHomeLED.Text = "H";
                    }
                    else
                    {
                        lblDIHome.Text = "";
                        lblDIHomeLED.Text = "";
                    }
                }
                else
                {
                    m_DIHome = value;
                    m_clsDIOInfo._DIHomeEnum_String = "";
                }
            }
        }

        /// <summary>取得或設定DI Home參數是否反向</summary>
        [Description("取得或設定DI Home參數是否反向") ]
        [DisplayName("DI Home Invert") , CategoryAttribute("ArtMMI") , Browsable(true) ]
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
        [Description("取得或設定DI Reach對應參數名稱") ]
        [DisplayName("DI Reach") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public clsEnum.enuDi? _DIReach
        {
            get
            {
                if (m_clsDIOInfo._DIReachEnum_String == "") 
                {
                    m_DIReach = null;
                    return null;
                }
                else if (m_clsDIOInfo._DIReachEnum_String == m_DIReach.ToString() ) 
                {
                    return m_DIReach;
                }
                else
                {
                    foreach (clsEnum.enuDi DIName in Enum.GetValues(typeof(clsEnum.enuDi) ) ) 
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
                    m_clsDIOInfo._DIReachEnum_String = m_DIReach.ToString() ;
                    if (value != null) 
                    {
                        lblDIReach.Text = "X" + ((int) m_DIReach).ToString() ;
                        lblDIReachLED.Text = "R";
                    }
                    else
                    {
                        lblDIReach.Text = "";
                        lblDIReachLED.Text = "";
                    }
                }
                else
                {
                    m_DIReach = value;
                    m_clsDIOInfo._DIReachEnum_String = "";
                }
            }
        }

        /// <summary>取得或設定DI Reach參數是否反向</summary>
        [Description("取得或設定DI Reach參數是否反向") ]
        [DisplayName("DI Reach Invert") , CategoryAttribute("ArtMMI") , Browsable(true) ]
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
        /// <summary>取得或設定DO Trigger對應參數名稱</summary>
        [Description("取得或設定DO Trigger對應參數名稱") ]
        [DisplayName("DO Trigger") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public clsEnum.enuDo? _DOTrigger
        {
            get
            {
                if (m_clsDIOInfo._DOTriggerEnum_String == "") 
                {
                    m_DOTrigger = null;
                    return null;
                }
                else if (m_clsDIOInfo._DOTriggerEnum_String == m_DOTrigger.ToString() ) 
                {
                    return m_DOTrigger;
                }
                else
                {
                    foreach (clsEnum.enuDo DoName in Enum.GetValues(typeof(clsEnum.enuDo) ) ) 
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
                    m_clsDIOInfo._DOTriggerEnum_String = m_DOTrigger.ToString() ;
                    if (value != null) 
                    {
                        lblDOTrigger.Text = "Y" + ((int) m_DOTrigger).ToString() ;
                    }
                    else
                    {
                        lblDOTrigger.Text = "";
                    }
                }
                else
                {
                    m_DOTrigger = value;
                    m_clsDIOInfo._DOTriggerEnum_String = "";
                }
            }
        }

        /// <summary>取得或設定DO Trigger參數是否反向</summary>
        [Description("取得或設定DO Trigger參數是否反向") ]
        [DisplayName("DO Trigger Invert") , CategoryAttribute("ArtMMI") , Browsable(true) ]
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

        /// <summary>取得或設定DI LED 是否橫向顯示</summary>
        [Description("取得或設定DI LED 是否橫向顯示") ]
        [DisplayName("LED Horizontal") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public bool _IsDiLedHorizontalDisplay
        {
            set
            {
                m_clsDIOInfo._IsDILEDHorizontalDisplay = value;
                SetBtnValue() ;
            }
            get
            {
                return m_clsDIOInfo._IsDILEDHorizontalDisplay;
            }
        }

        /// <summary>取得或設定DI Home Reach 是否反向顯示</summary>
        [Description("取得或設定DI Home Reach 是否反向顯示") ]
        [DisplayName("H/R Invert") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public bool _IsHomeReachInvert
        {
            set
            {
                m_clsDIOInfo._IsHomeReachInvert = value;
                SetBtnValue() ;
            }
            get
            {
                return m_clsDIOInfo._IsHomeReachInvert;
            }
        }

        /// <summary> 是否使用DIO元件 </summary>
        [Description("是否使用DIO元件") ]
        [DisplayName("Enable DIO") , CategoryAttribute("ArtMMI") , Browsable(true) ]
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

        /// <summary>取得或設定DIO 名稱</summary>
        [Description("取得或設定DIO 名稱") ]
        [DisplayName("Name DIO") , CategoryAttribute("ArtMMI") , Browsable(true) ]
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

        #endregion

        #region //===================== 必要函式設置 =====================


        /// <summary>
        /// 物件建立請利用 GetSingleton() ，除非特殊需求
        /// </summary>
        public ucDIOInfo1() 
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
                this._DIColor = p_DataDIO._DIColor;
                //this._DIHome = p_DataDIO._DIHome;
                if (this._DIHome != null) 
                {
                    lblDIHome.Text = "X" + ((int) this._DIHome).ToString() ;
                    lblDIHomeLED.Text = "H";
                }
                else
                {
                    lblDIHome.Text = "";
                    lblDIHomeLED.Text = "";
                }
                this._DIHome_Invert = p_DataDIO._DIHome_Invert;
                this._DIOName = p_DataDIO._DIOName;
                //this._DIReach = p_DataDIO._DIReach;
                if (this._DIReach != null) 
                {
                    lblDIReach.Text = "X" + ((int) this._DIReach).ToString() ;
                    lblDIReachLED.Text = "R";
                }
                else
                {
                    lblDIReach.Text = "";
                    lblDIReachLED.Text = "";
                }
                this._DIReach_Invert = p_DataDIO._DIReach_Invert;
                this._DOColor = p_DataDIO._DOColor;
                //this._DOTrigger = p_DataDIO._DOTrigger;
                if (this._DOTrigger != null) 
                {
                    lblDOTrigger.Text = "Y" + ((int) this._DOTrigger).ToString() ;
                }
                else
                {
                    lblDOTrigger.Text = "";
                }
                this._DOTrigger_Invert = p_DataDIO._DOTrigger_Invert;
                this._IsDiLedHorizontalDisplay = p_DataDIO._IsDILEDHorizontalDisplay;
                this._IsHomeReachInvert = p_DataDIO._IsHomeReachInvert;
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
                if (clsDioCtrl.GetDi((clsEnum.enuDi) _DIHome) ^ _DIHome_Invert) 
                {
                    lblDIHome.ForeColor = m_clsDIOInfo._DIColor;
                    lblDIHomeLED.ForeColor = m_clsDIOInfo._DIColor;
                }
                else
                {
                    lblDIHome.ForeColor = this.BackColor;
                    lblDIHomeLED.ForeColor = this.BackColor;
                }
            }

            if (_DIReach != null) 
            {
                if (clsDioCtrl.GetDi((clsEnum.enuDi) _DIReach) ^ _DIReach_Invert) 
                {
                    lblDIReach.ForeColor = m_clsDIOInfo._DIColor;
                    lblDIReachLED.ForeColor = m_clsDIOInfo._DIColor;
                }
                else
                {
                    lblDIReach.ForeColor = this.BackColor;
                    lblDIReachLED.ForeColor = this.BackColor;
                }
            }

            if (_DOTrigger != null) 
            {
                if (clsDioCtrl.GetDo((clsEnum.enuDo) _DOTrigger) ^ _DOTrigger_Invert) 
                {
                    btnDOTrigger.BackColor = m_clsDIOInfo._DOColor;
                }
                else
                {
                    btnDOTrigger.BackColor = this.BackColor;
                }
                btnDOTrigger.BackColor = clsDioCtrl.GetDo((clsEnum.enuDo) _DOTrigger) ? _DOColor : Color.Gray;
                lblDOTrigger.BackColor = clsDioCtrl.GetDo((clsEnum.enuDo) _DOTrigger) ? _DOColor : Color.Gray;
            }

        }

        #endregion

        #region //===================== private 函式設置 =================

        private void UpdateControl() 
        {

        }

        private void SetBtnValue() 
        {
            if (_IsDiLedHorizontalDisplay) 
            {
                if (!_IsHomeReachInvert) 
                {
                    lblDIHome.Location = new Point(12, 4) ;
                    lblDIReach.Location = new Point(this.Width - (int) (lblDIReach.Width + 10) , 4) ;


                    lblDIHomeLED.Location = new Point(1, 15) ;
                    lblDIReachLED.Location = new Point(this.Width - (int) (lblDIReachLED.Width + 2) , 15) ;
                }
                else
                {
                    lblDIReach.Location = new Point(12, 4) ;
                    lblDIHome.Location = new Point(this.Width - (int) (lblDIReach.Width + 10) , 4) ;


                    lblDIReachLED.Location = new Point(1, 15) ;
                    lblDIHomeLED.Location = new Point(this.Width - (int) (lblDIReachLED.Width + 2) , 15) ;
                }
            }
            else
            {
                if (!_IsHomeReachInvert) 
                {
                    lblDIHome.Location = new Point(12, 4) ;
                    lblDIReach.Location = new Point(12, this.Height - lblDIReach.Height - 4) ;

                    lblDIHomeLED.Location = new Point(1, 15) ;
                    lblDIReachLED.Location = new Point(1, this.Height - lblDIReachLED.Height - 11) ;
                }
                else
                {
                    lblDIReach.Location = new Point(12, 4) ;
                    lblDIHome.Location = new Point(12, this.Height - lblDIReach.Height - 4) ;

                    lblDIReachLED.Location = new Point(1, 15) ;
                    lblDIHomeLED.Location = new Point(1, this.Height - lblDIReachLED.Height - 11) ;
                }
            }
        }

        #endregion

        #region//===================== 以下為事件處理 ===================

        private void btnDOTrigger_MouseEnter(object sender, EventArgs e) 
        {
            //throw new NotImplementedException() ;

            string strDioName = "";

            if (sender == btnDOTrigger) 
            {
                strDioName = _DOTrigger.ToString() ;
            }

            if (sender == lblDIHome) 
            {
                strDioName = _DIHome.ToString() ;
            }

            if (sender == lblDIReach) 
            {
                strDioName = _DIReach.ToString() ;
            }

            m_ToolTipDioName.SetToolTip((Control) sender, strDioName) ;
        }

        private void btnDIO_MouseMove(object sender, MouseEventArgs e) 
        {
            string strDioName = "";

            if (sender == btnDOTrigger) 
            {
                strDioName = _DOTrigger.ToString() ;
            }
            
            if (sender == lblDIHome) 
            {
                strDioName = _DIHome.ToString() ;
            }
            
            if (sender == lblDIReach) 
            {
                strDioName = _DIReach.ToString() ;
            }

            m_ToolTipDioName.SetToolTip((Control) sender, strDioName) ;
        }

        private void btnDOTrigger_Click(object sender, EventArgs e) 
        {
            if (_DOTrigger != null) 
            {
                clsDioCtrl.SetDo((clsEnum.enuDo) _DOTrigger, !clsDioCtrl.GetDo((clsEnum.enuDo) _DOTrigger) ) ;
            }
        }

        #endregion

        private void btnDOTrigger_MouseDown(object sender, MouseEventArgs e) 
        {
            if (DIOLog != null) 
            {
                DIOLog(this, e) ;
            }
        }
    }
}

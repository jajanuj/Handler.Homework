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
using ArtCommunication;
using ArtTeach;

namespace ArtTeach
{
    public partial class clsDIO : ArtCommonLib.ucBaseUserControl
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

        ToolTip m_ToolTipDioName = new ToolTip();

        private clsDIOInfo m_clsDIOInfo = new clsDIOInfo();
        public clsDIOInfo _clsDIOInfo
        {
            set
            {
                if (value != null)
                {
                    m_clsDIOInfo = value;
                }
            }
            get
            {
                return m_clsDIOInfo;
            }
        }




        /// <summary>取得或設定DI 狀態顏色</summary>
        [Description("取得或設定DI 狀態顏色")]
        [DisplayName("DI Color"), CategoryAttribute("ArtMMI"), Browsable(true)]
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
        [Description("取得或設定DO 狀態顏色")]
        [DisplayName("DO Color"), CategoryAttribute("ArtMMI"), Browsable(true)]
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


        /// <summary>取得或設定DI Home對應參數名稱</summary>
        [Description("取得或設定DI Home對應參數名稱")]
        [DisplayName("DI Home"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public clsEnum.enuDi? _DIHome
        {
            set
            {
                m_clsDIOInfo._DIHome = value;
                if (value != null)
                {
                    lblDIHome.Text = "X" + ((int)m_clsDIOInfo._DIHome).ToString();
                }
                lblDIHomeLED.Text = "H";
            }
            get
            {
                return m_clsDIOInfo._DIHome;
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

        /// <summary>取得或設定DI Reach對應參數名稱</summary>
        [Description("取得或設定DI Reach對應參數名稱")]
        [DisplayName("DI Reach"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public clsEnum.enuDi? _DIReach
        {
            set
            {
                m_clsDIOInfo._DIReach = value;
                if (value != null)
                {
                    lblDIReach.Text = "X" + ((int)m_clsDIOInfo._DIReach).ToString();
                }
                lblDIReachLED.Text = "R";
            }
            get
            {
                return m_clsDIOInfo._DIReach;
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

        /// <summary>取得或設定DO Trigger對應參數名稱</summary>
        [Description("取得或設定DO Trigger對應參數名稱")]
        [DisplayName("DO Trigger"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public clsEnum.enuDo? _DOTrigger
        {
            set
            {
                m_clsDIOInfo._DOTrigger = value;
                if (value != null)
                {
                    lblDOTrigger.Text = "Y" + ((int)m_clsDIOInfo._DOTrigger).ToString();
                }
            }
            get
            {
                return m_clsDIOInfo._DOTrigger;
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

        /// <summary>取得或設定DI LED 是否橫向顯示</summary>
        [Description("取得或設定DI LED 是否橫向顯示")]
        [DisplayName("LED Horizontal"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public bool _IsDiLedHorizontalDisplay
        {
            set
            {
                m_clsDIOInfo._IsDILEDHorizontalDisplay = value;
                SetBtnValue();
            }
            get
            {
                return m_clsDIOInfo._IsDILEDHorizontalDisplay;
            }
        }

        /// <summary>取得或設定DI Home Reach 是否反向顯示</summary>
        [Description("取得或設定DI Home Reach 是否反向顯示")]
        [DisplayName("H/R Invert"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public bool _IsHomeReachInvert
        {
            set
            {
                m_clsDIOInfo._IsHomeReachInvert = value;
                SetBtnValue();
            }
            get
            {
                return m_clsDIOInfo._IsHomeReachInvert;
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

        /// <summary>取得或設定DIO 名稱</summary>
        [Description("取得或設定DIO 名稱")]
        [DisplayName("Name DIO"), CategoryAttribute("ArtMMI"), Browsable(true)]
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
        /// 物件建立請利用 GetSingleton()，除非特殊需求
        /// </summary>
        public clsDIO()
        {
            InitializeComponent();
            ucParameter.Add(this);
        }

        #endregion

        #region //===================== public 函式設置 ==================

        /// <summary> 更新DIO 狀態 </summary>
        public void RefreshStatic()
        {
            if (_DIHome != null)
            {
                if (clsDioCtrl.GetDi((clsEnum.enuDi)_DIHome) ^ _DIHome_Invert)
                {
                    lblDIHome.ForeColor = m_clsDIOInfo._DIColor;
                    lblDIHomeLED.BackColor = m_clsDIOInfo._DIColor;
                }
                else
                {
                    lblDIHome.ForeColor = this.BackColor;
                    lblDIHomeLED.BackColor = this.BackColor;
                }
            }

            if (_DIReach != null)
            {
                if (clsDioCtrl.GetDi((clsEnum.enuDi)_DIReach) ^ _DIReach_Invert)
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
                if (clsDioCtrl.GetDo((clsEnum.enuDo)_DOTrigger) ^ _DOTrigger_Invert)
                {
                    btnDOTrigger.BackColor = m_clsDIOInfo._DOColor;
                }
                else
                {
                    btnDOTrigger.BackColor = this.BackColor;
                }
            }

            btnDOTrigger.BackColor = clsDioCtrl.GetDo((clsEnum.enuDo)_DOTrigger) ? _DOColor : Color.Gray;
            lblDOTrigger.BackColor = clsDioCtrl.GetDo((clsEnum.enuDo)_DOTrigger) ? _DOColor : Color.Gray;
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
                    lblDIHome.Location = new Point(12, 4);
                    lblDIReach.Location = new Point(this.Width - (int)(lblDIReach.Width + 10), 4);


                    lblDIHomeLED.Location = new Point(1, 15);
                    lblDIReachLED.Location = new Point(this.Width - (int)(lblDIReachLED.Width + 2), 15);
                }
                else
                {
                    lblDIReach.Location = new Point(12, 4);
                    lblDIHome.Location = new Point(this.Width - (int)(lblDIReach.Width + 10), 4);


                    lblDIReachLED.Location = new Point(1, 15);
                    lblDIHomeLED.Location = new Point(this.Width - (int)(lblDIReachLED.Width + 2), 15);
                }
            }
            else
            {
                if (!_IsHomeReachInvert)
                {
                    lblDIHome.Location = new Point(12, 4);
                    lblDIReach.Location = new Point(12, this.Height - lblDIReach.Height - 4);

                    lblDIHomeLED.Location = new Point(1, 15);
                    lblDIReachLED.Location = new Point(1, this.Height - lblDIReachLED.Height - 11);
                }
                else
                {
                    lblDIReach.Location = new Point(12, 4);
                    lblDIHome.Location = new Point(12, this.Height - lblDIReach.Height - 4);

                    lblDIReachLED.Location = new Point(1, 15);
                    lblDIHomeLED.Location = new Point(1, this.Height - lblDIReachLED.Height - 11);
                }
            }
        }

        #endregion

        #region//===================== 以下為事件處理 ===================

        private void btnDOTrigger_MouseEnter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            string strDioName = "";

            if (sender == btnDOTrigger)
            {
                strDioName = _DOTrigger.ToString();
            }

            if (sender == lblDIHome)
            {
                strDioName = _DIHome.ToString();
            }

            if (sender == lblDIReach)
            {
                strDioName = _DIReach.ToString();
            }

            m_ToolTipDioName.SetToolTip((Control)sender, strDioName);
        }

        private void btnDIO_MouseMove(object sender, MouseEventArgs e)
        {
            string strDioName = "";

            if (sender == btnDOTrigger)
            {
                strDioName = _DOTrigger.ToString();
            }
            
            if (sender == lblDIHome)
            {
                strDioName = _DIHome.ToString();
            }
            
            if (sender == lblDIReach)
            {
                strDioName = _DIReach.ToString();
            }

            m_ToolTipDioName.SetToolTip((Control)sender, strDioName);
        }

        private void btnDOTrigger_Click(object sender, EventArgs e)
        {
            if (_DOTrigger != null)
            {
                clsDioCtrl.SetDo((clsEnum.enuDo)_DOTrigger, !clsDioCtrl.GetDo((clsEnum.enuDo)_DOTrigger));
            }
        }

        #endregion

        private void btnDOTrigger_MouseDown(object sender, MouseEventArgs e)
        {
            if (DIOLog != null)
            {
                DIOLog(this, e);
            }
        }
    }


    //[Serializable()]
    public class clsDIOInfo
    {

        #region 參數

        ///<summary> DI Color </summary>
        public Color _DIColor;

        ///<summary> DO Color </summary>
        public Color _DOColor;

        ///<summary> 原點訊號 </summary>
        public clsEnum.enuDi? _DIHome;

        ///<summary> 到位訊號 </summary>
        public clsEnum.enuDi? _DIReach;

        ///<summary> 原點觸發 </summary>
        public clsEnum.enuDo? _DOTrigger;

        ///<summary> DIO名稱 </summary>
        public string _DIOName;

        ///<summary> DI原點訊號是否B接反向 </summary>
        public bool _DIHome_Invert;

        ///<summary> DI到位訊號是否B接反向 </summary>
        public bool _DIReach_Invert;

        ///<summary> DO觸發訊號是否B接反向 </summary>
        public bool _DOTrigger_Invert;

        ///<summary> DI LED 是否橫向顯示 </summary>
        public bool _IsDILEDHorizontalDisplay;

        ///<summary> Home Reach是否反向 </summary>
        public bool _IsHomeReachInvert;

        ///<summary> 是否可使用此元件 </summary>
        public bool _IsDIOEnable;

        #endregion

        #region 建構式 與 解建構式

        public clsDIOInfo()
        {
            Initial();
        }
        ~clsDIOInfo()
        {
        }

        public void Initial()
        {
            _DIOName = "";
            _DIColor = Color.Green;
            _DOColor = Color.Red;
            _DIHome = null;
            _DIReach = null;
            _DOTrigger = null;
            _DIHome_Invert = false;
            _DIReach_Invert = false;
            _DOTrigger_Invert = false;
            _IsDILEDHorizontalDisplay = false;
            _IsHomeReachInvert = false;
            _IsDIOEnable = true;
        }
        #endregion
    }
}

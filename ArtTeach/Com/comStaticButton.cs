using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ArtData;

namespace ArtControlLib
{
    /// <summary> DIO 狀態按鈕元件 </summary>
    public partial class comStaticButton : Button
    {
        #region //=====================  區域變數設置 =====================
        private Color m_colorDI = Color.DeepSkyBlue;
        private Color m_colorDO = Color.Lime;

        private clsEnum.enuDi? m_enuDi1;
        private clsEnum.enuDi? m_enuDi2;
        private clsEnum.enuDo? m_enuDo1;

        private bool m_IsLedOnly = false;
        ToolTip m_ToolTipDioName = new ToolTip();

        /// <summary>取得或設定DI 狀態顏色</summary>
        [Description("取得或設定DI 狀態顏色")]
        [Category("User define")]
        public Color _DiColor
        {
            get
            {
                return m_colorDI;
            }
            set
            {
                m_colorDI = value;
            }
        }

        /// <summary>取得或設定DO 狀態顏色</summary>
        [Description("取得或設定DO 狀態顏色")]
        [Category("User define")]
        public Color _DoColor
        {
            get
            {
                return m_colorDO;
            }
            set
            {
                m_colorDO = value;
            }
        }


        /// <summary>取得或設定DI1對應參數名稱</summary>
        [Description("取得或設定DI1對應參數名稱")]
        [Category("User define")]
        public clsEnum.enuDi? _DI1
        {
            get
            {
                return m_enuDi1;
            }
            set
            {
                m_enuDi1 = value;
                SetBtnValue();
            }
        }

        /// <summary>取得或設定DI1參數是否反向</summary>
        [Description("取得或設定DI1參數是否反向")]
        [Category("User define")]
        public bool _DI1_Invert
        {
            get;
            set;
        }

        /// <summary>取得或設定DI2對應參數名稱</summary>
        [Description("取得或設定DI2對應參數名稱")]
        [Category("User define")]
        public clsEnum.enuDi? _DI2
        {
            get
            {
                return m_enuDi2;
            }
            set
            {
                m_enuDi2 = value;
                SetBtnValue();
            }
        }

        /// <summary>取得或設定DI2參數是否反向</summary>
        [Description("取得或設定DI2參數是否反向")]
        [Category("User define")]
        public bool _DI2_Invert
        {
            get;
            set;
        }

        /// <summary>取得或設定DO1對應參數名稱</summary>
        [Description("取得或設定DO1對應參數名稱")]
        [Category("User define")]
        public clsEnum.enuDo? _DO1
        {
            get
            {
                return m_enuDo1;
            }
            set
            {
                m_enuDo1 = value;
                SetBtnValue();
            }
        }

        /// <summary>取得或設定DO1參數是否反向</summary>
        [Description("取得或設定DO1參數是否反向")]
        [Category("User define")]
        public bool _DO1_Invert
        {
            get;
            set;
        }

        /// <summary>是否只顯示狀態 (只能顯示一個)</summary>
        [Description("是否只顯示狀態 (只能顯示一個)")]
        [Category("User define")]
        public bool _IsLedOnly
        {
            get
            {
                return m_IsLedOnly;
            }
            set
            {
                m_IsLedOnly = value;
                SetBtnValue();
            }
        }

        /// <summary>取得或設定Di Led 是否橫向顯示</summary>
        [Description("取得或設定Di Led 是否橫向顯示")]
        [Category("User define")]
        public bool _IsDiLedHorizontalDisplay
        {
            get;
            set;
        }

        #endregion

        /// <summary> DIO 狀態按鈕元件建構式 </summary>
        public comStaticButton()
        {
            InitializeComponent();
            this.SizeChanged += new EventHandler(comStaticButton_SizeChanged);

            //btnDO1.MouseMove += new MouseEventHandler(btnDIO_MouseMove);
            //btnDO1.MouseEnter += new EventHandler(btnDO1_MouseEnter);

            //btnDI1.MouseMove += new MouseEventHandler(btnDIO_MouseMove);
            //btnDI2.MouseMove += new MouseEventHandler(btnDIO_MouseMove);

            SetBtnValue();
        }

        void btnDO1_MouseEnter(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void btnDIO_MouseMove(object sender, MouseEventArgs e)
        {
            string strDioName ="";
            if (sender == btnDO1)
            {
                strDioName = _DO1.ToString();
            }
            else if (sender == btnDI1)
            {
                strDioName = _DI1.ToString();
            }
            else if (sender == btnDI2)
            {
                strDioName = _DI2.ToString();
            }
            else
            {
                return;
            }

            m_ToolTipDioName.SetToolTip((Control)sender, strDioName);
        }

        /// <summary> 更新DIO 狀態 </summary>
        public void RefreshStatic()
        {
            if (_DI1 != null)
            {
                if (clsDioCtrl.GetDi((clsEnum.enuDi)_DI1) ^ _DI1_Invert)
                {
                    btnDI1.BackColor = m_colorDI;
                }
                else
                {
                    btnDI1.BackColor = Color.White;
                }
            }

            if (_DI2 != null)
            {
                if (clsDioCtrl.GetDi((clsEnum.enuDi)_DI2) ^ _DI2_Invert)
                {
                    btnDI2.BackColor = m_colorDI;
                }
                else
                {
                    btnDI2.BackColor = Color.White;
                }
            }

            if (_DO1 != null)
            {
                if (clsDioCtrl.GetDo((clsEnum.enuDo)_DO1) ^ _DO1_Invert)
                {
                    btnDO1.BackColor = m_colorDO;
                }
                else
                {
                    btnDO1.BackColor = Color.White;
                }
            }
        }

        private void SetBtnValue()
        {
            btnDI1.Visible = _DI1 != null;
            btnDI2.Visible = _DI2 != null;
            btnDO1.Visible = _DO1 != null;

            if (_IsLedOnly)
            {
                if (_DI1 != null)
                {
                    //btnDI1.Visible = true;
                    btnDI1.Size = this.Size;
                    btnDI1.Capture = true;

                    btnDI1.Location = new Point(0, 0);

                    btnDI2.Visible = false;
                    btnDO1.Visible = false;
                }
                else if (_DI2 != null)
                {
                    //btnDI2.Visible = true;
                    btnDI2.Size = this.Size;
                    btnDI2.Location = new Point(0, 0);

                    btnDI1.Visible = false;
                    btnDO1.Visible = false;
                }
                else if (_DO1 != null)
                {
                    //btnDO1.Visible = true;
                    btnDO1.Size = this.Size;
                    btnDO1.Location = new Point(0, 0);

                    btnDI1.Visible = false;
                    btnDI2.Visible = false;
                }
            }
            else
            {
                if (_IsDiLedHorizontalDisplay)
                {
                    btnDI1.Width = (int)(this.Height * 0.2);
                    btnDI1.Height = (int)(this.Height * 0.2);
                    btnDI1.Location = new Point(3, 3);

                    btnDO1.Width = (int)(this.Height * 0.2);
                    btnDO1.Height = (int)(this.Height * 0.2);
                    btnDO1.Location = new Point(3, this.Height - btnDO1.Height - 3);

                    btnDI2.Width = (int)(this.Height * 0.2);
                    btnDI2.Height = (int)(this.Height * 0.2);
                    btnDI2.Location = new Point(this.Width - (int)(btnDI2.Width + 3), 3);
                }
                else
                {
                    btnDI1.Width = (int)(this.Height * 0.2);
                    btnDI1.Height = (int)(this.Height * 0.2);
                    btnDI1.Location = new Point(3, 3);

                    btnDI2.Width = (int)(this.Height * 0.2);
                    btnDI2.Height = (int)(this.Height * 0.2);
                    btnDI2.Location = new Point(3, this.Height - btnDI2.Height - 3);

                    btnDO1.Width = (int)(this.Height * 0.2);
                    btnDO1.Height = (int)(this.Height * 0.2);
                    btnDO1.Location = new Point(this.Width - (int)(btnDO1.Width + 3), 3);
                }
            }
        }

        private void comStaticButton_SizeChanged(object sender, EventArgs e)
        {
            SetBtnValue();
        }




    }
}

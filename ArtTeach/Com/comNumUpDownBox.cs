using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtCommonLib;
using ArtData;

namespace ArtControlLib
{
    /// <summary>
    /// 參數數值元件
    /// </summary>
    public partial class comNumUpDownBox : PictureBox
    {
        #region //=====================  區域變數設置 =====================
        comNumBox m_numBox = new comNumBox();
        Button m_btnUp = new Button();
        Button m_btnDown = new Button();

        bool m_IsbtnUpPress = false;
        bool m_IsbtnDownPress = false;

        Timer m_timer = new Timer();
        clsHiPerfTimer m_hpTimer = new clsHiPerfTimer();
        enuButtonDir m_btnAlign = enuButtonDir.Vertical;

        /// <summary> 按鈕方向</summary>
        public enum enuButtonDir
        {
            /// <summary> 水平方向</summary>
            Horizontal,
            /// <summary> 垂直方向</summary>
            Vertical,
        }

        /// <summary>按鈕配置位置</summary>
        [Description("按鈕配置位置")]
        [Category("User define")]
        public enuButtonDir _ButtonAlign
        {
            get
            {
                return m_btnAlign;
            }
            set
            {
                m_btnAlign = value;
                SetButtonSize();
            }
        }

        /// <summary>表示每按一下按鈕所增加/減少的數量</summary>
        [Description("表示每按一下按鈕所增加/減少的數量")]
        [Category("User define")]
        public double _Increment
        {
            get;
            set;
        }

        /// <summary>取得或設定對應參數名稱</summary>
        [Description("取得或設定對應參數名稱")]
        [Category("User define")]
        public clsEnum.enuPmtName? _PmtName
        {
            get
            {
                return m_numBox._PmtName;
            }
            set
            {
                m_numBox._PmtName = value;
            }
        }

        /// <summary>取得或設定對應參數種類</summary>
        [Description("取得或設定對應參數種類")]
        [Category("User define")]
        public clsEnum.enuPmtType? _PmtType
        {
            get
            {
                return m_numBox._PmtType;
            }
            set
            {
                m_numBox._PmtType = value;
            }
        }

        /// <summary>取得或設定預設值</summary>
        [Description("取得或設定預設值")]
        [Category("User define")]
        public decimal _DefaultValue
        {
            get
            {
                return m_numBox._DefaultValue;
            }
            set
            {
                m_numBox._DefaultValue = value;
            }
        }

        /// <summary>取得或設定最大許可值</summary>
        [Description("取得或設定最大許可值")]
        [Category("User define")]
        public decimal _Maximum
        {
            get
            {
                return m_numBox._Maximum;
            }
            set
            {
                m_numBox._Maximum = value;
            }
        }

        /// <summary>取得或設定最小許可值</summary>
        [Description("取得或設定最小許可值")]
        [Category("User define")]
        public decimal _Minimum
        {
            get
            {
                return m_numBox._Minimum;
            }
            set
            {
                m_numBox._Minimum = value;
            }
        }

        /// <summary>取得或設定小數點位數</summary>
        [Description("取得或設定小數點位數")]
        [Category("User define")]
        public int _DecimalPlaces
        {
            get
            {
                return m_numBox._DecimalPlaces;
            }
            set
            {
                m_numBox._DecimalPlaces = value;
            }
        }

        /// <summary>取得或設定數值</summary>
        [Description("取得或設定數值")]
        [Category("User define")]
        public decimal _Value
        {
            get
            {
                return m_numBox._Value;
            }
            set
            {
                m_numBox._Value = value;
            }
        }

        /// <summary>取得或設定是否寫入Ini File</summary>
        [Description("取得或設定是否寫入Ini File")]
        [Category("User define")]
        public bool _IsSaveToIni
        {
            get
            {
                return m_numBox._IsSaveToIni;
            }
            set
            {
                m_numBox._IsSaveToIni = value;
            }
        }

        /// <summary>取得或設定數值改變時是否寫入Button Log</summary>
        [Description("取得或設定數值改變時是否寫入Button Log")]
        [Category("User define")]
        public bool _IsSaveToLog
        {
            get
            {
                return m_numBox._IsSaveToLog;
            }
            set
            {
                m_numBox._IsSaveToLog = value;
            }
        }

        /// <summary>取得或設定是否顯示輸入視窗</summary>
        [Description("取得或設定是否顯示輸入視窗")]
        [Category("User define")]
        public bool _IsShowPopForm
        {
            get
            {
                return m_numBox._IsShowPopForm;
            }
            set
            {
                m_numBox._IsShowPopForm = value;
            }
        }

        #endregion

        #region //=====================  必要函式設置 =====================
        /// <summary>
        /// 參數數值元件建構式
        /// </summary>
        public comNumUpDownBox()
        {
            InitializeComponent();

            if (_DefaultValue >= _Maximum)
            {
                _DefaultValue = _Maximum;
            }
            else if (_DefaultValue <= _Minimum)
            {
                _DefaultValue = _Minimum;
            }

            _Increment = 1;
            m_btnUp.BackgroundImage = Properties.Resources.btnUp;
            m_btnDown.BackgroundImage = Properties.Resources.btnDown;
            m_btnUp.BackgroundImageLayout = m_btnDown.BackgroundImageLayout = ImageLayout.Zoom;
            this._ButtonAlign = enuButtonDir.Horizontal;

            this.Controls.Add(m_btnUp);
            this.Controls.Add(m_numBox);
            this.Controls.Add(m_btnDown);

            //this.Click += new System.EventHandler(this.textBoxNum_MouseClick);
            //this.TextChanged += new System.EventHandler(this.textBoxNum_TextChanged);
            this.SizeChanged += new EventHandler(comNumUnDownBox_SizeChanged);
            m_btnUp.Click += new EventHandler(m_btnUp_Click);
            m_btnDown.Click += new EventHandler(m_btnDown_Click);

            m_btnUp.MouseUp += new MouseEventHandler(m_btnUp_MouseUp);
            m_btnUp.MouseDown += new MouseEventHandler(m_btnUp_MouseDown);

            m_btnDown.MouseUp += new MouseEventHandler(m_btnDown_MouseUp);
            m_btnDown.MouseDown += new MouseEventHandler(m_btnDown_MouseDown);

            m_timer.Interval = 50;
            m_timer.Enabled = false;
            m_timer.Tick += new EventHandler(m_timer_Tick);

            SetButtonSize();
        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            if (m_hpTimer.Elapsed(clsCmData.enuSecUnit.Sec) > 1)
            {
                if (m_IsbtnUpPress)
                {
                    m_btnUp_Click(null, null);
                }
                else if (m_IsbtnDownPress)
                {
                    m_btnDown_Click(null, null);
                }
            }
        }

        #endregion

        private void SetButtonSize()
        {
            if (_ButtonAlign == enuButtonDir.Vertical)
            {
                m_btnUp.Width = m_btnDown.Width = m_numBox.Width = this.Width;
                m_btnUp.Height = m_btnDown.Height = (this.Height - m_numBox.Height - 6) / 2;

                m_numBox.Top = m_btnUp.Bottom + 3;
                m_btnDown.Top = m_numBox.Bottom + 3;

                m_btnUp.Left = m_btnDown.Left = m_numBox.Left = 0;
            }
            else
            {
                m_btnUp.Width = m_btnDown.Width = m_btnUp.Height = m_btnDown.Height = this.Height;
                m_numBox.Width = this.Width - m_btnUp.Width - m_btnDown.Width - 6;
                m_btnUp.Height = m_btnDown.Height = this.Height;

                m_btnDown.Top = m_btnUp.Top;
                m_numBox.Top = m_btnDown.Height / 2 - m_numBox.Height / 2;

                m_numBox.Left = m_btnUp.Right + 3;
                m_btnDown.Left = m_numBox.Right + 3;
            }
        }

        //===================== 以下為事件處理 =====================
        void comNumUnDownBox_SizeChanged(object sender, EventArgs e)
        {
            SetButtonSize();
        }

        void m_btnUp_Click(object sender, EventArgs e)
        {
            m_numBox._Value += (decimal)this._Increment;
        }

        void m_btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            m_IsbtnUpPress = false;
            m_timer.Enabled = false;
            m_hpTimer.Stop();
        }

        void m_btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            m_IsbtnUpPress = true;
            m_timer.Enabled = true;
            m_hpTimer.Restart();
        }
        


        void m_btnDown_Click(object sender, EventArgs e)
        {
            m_numBox._Value -= (decimal)this._Increment;
        }

        void m_btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            m_IsbtnDownPress = false;
            m_timer.Enabled = false;
            m_hpTimer.Stop();
        }

        void m_btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            m_IsbtnDownPress = true;
            m_timer.Enabled = true;
            m_hpTimer.Restart();
        }


    }
}

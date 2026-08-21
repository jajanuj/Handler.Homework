using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ArtCommonLib;
using ArtData;

namespace ArtControlLib
{
    /// <summary>
    /// 參數數值元件
    /// </summary>
    public partial class comNumTrackBar : PictureBox
    {
        #region //=====================  區域變數設置 =====================
        private TrackBar m_trackBar = new TrackBar();
        private Label m_lblTitle = new Label();
        private Label m_lblMin = new Label();
        private Label m_lblMax = new Label();
        private Label m_lblValue = new Label();

        /// <summary>與控制項關聯的文件</summary>
        [Description("與控制項關聯的文件")]
        [Category("User define")]
        [Browsable(true)]
        public override string Text
        {
            get
            {
                return m_lblTitle.Text;
            }
            set
            {
                m_lblTitle.Text = value;
                comNumTrackBar_SizeChanged(null, null);
            }
        }

        /// <summary>取得或設定對應參數名稱</summary>
        [Description("取得或設定對應參數名稱")]
        [Category("User define")]
        public clsEnum.enuPmtName? _PmtName
        {
            get;
            set;
        }

        /// <summary>取得或設定對應參數種類</summary>
        [Description("取得或設定對應參數種類")]
        [Category("User define")]
        public clsEnum.enuPmtType? _PmtType
        {
            get;
            set;
        }

        /// <summary>取得或設定預設值</summary>
        [Description("取得或設定預設值")]
        [Category("User define")]
        public int _DefaultValue
        {
            get;
            set;
        }

        /// <summary>暫存資料</summary>
        [Description("暫存資料")]
        [Category("User define")]
        public object _TempValue
        {
            get;
            set;
        }

        /// <summary>取得或設定最大許可值</summary>
        [Description("取得或設定最大許可值")]
        [Category("User define")]
        public int _Maximum
        {
            get
            {
                return m_trackBar.Maximum;
            }
            set
            {
                m_trackBar.Maximum = value;
                m_lblMax.Text = m_trackBar.Maximum.ToString();
                comNumTrackBar_SizeChanged(null, null);
            }
        }

        /// <summary>取得或設定最小許可值</summary>
        [Description("取得或設定最小許可值")]
        [Category("User define")]
        public int _Minimum
        {
            get
            {
                return m_trackBar.Minimum;
            }
            set
            {
                m_trackBar.Minimum = value;
                m_lblMin.Text = m_trackBar.Minimum.ToString();
                comNumTrackBar_SizeChanged(null, null);
            }
        }

        /// <summary>取得或設定數值</summary>
        [Description("取得或設定數值")]
        [Category("User define")]
        public int _Value
        {
            get
            {
                return m_trackBar.Value;
            }

            set
            {
                if (m_trackBar.Minimum >= value)
                {
                    m_trackBar.Value = m_trackBar.Minimum;
                }
                else if (m_trackBar.Maximum <= value)
                {
                    m_trackBar.Value = m_trackBar.Maximum;
                }
                else
                {
                    m_trackBar.Value = value;
                }

                if (Parent != null && Parent.IsHandleCreated)
                {
                    if (this._PmtName != null)
                    {
                        if (this._IsSaveToLog && ucParameter.GetValue((clsEnum.enuPmtName)this._PmtName) != m_trackBar.Value)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog,
                                this.Parent.Name + "-" + this.Name + " => " 
                                + this._PmtName + ":" 
                                + ucParameter.GetValue((clsEnum.enuPmtName)this._PmtName) + " =>" + m_trackBar.Value);
                        }

                        ucParameter.SetValue((clsEnum.enuPmtName)this._PmtName, _Value);
                        if (this._IsSaveToIni || ucParameter.IsAlwaySave)
                        {
                            ucParameter.SaveValue(this._PmtType, this._PmtName, _Value.ToString());
                        }
                    }
                }
            }
        }

        /// <summary>取得或設定是否寫入Ini File</summary>
        [Description("取得或設定是否寫入Ini File")]
        [Category("User define")]
        public bool _IsSaveToIni
        {
            get;
            set;
        }

        /// <summary>取得或設定數值改變時是否寫入Button Log</summary>
        [Description("取得或設定數值改變時是否寫入Button Log")]
        [Category("User define")]
        public bool _IsSaveToLog
        {
            get;
            set;
        }
        public event EventHandler ValueChanged;
        #endregion

        #region //=====================  必要函式設置 =====================
        /// <summary>
        /// 參數數值元件建構式
        /// </summary>
        public comNumTrackBar()
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

            m_lblTitle.AutoSize = m_lblMax.AutoSize = m_lblMin.AutoSize = m_lblValue.AutoSize = true;
            this._IsSaveToIni = true;
            this._IsSaveToLog = true;

            m_lblMax.Text = _Maximum.ToString();
            m_lblMin.Text = _Minimum.ToString();
            m_lblValue.Text = _Value.ToString();

            this.Controls.Add(m_lblTitle);
            this.Controls.Add(m_lblMax);
            this.Controls.Add(m_lblMin);
            this.Controls.Add(m_lblValue);
            this.Controls.Add(m_trackBar);

            this.SizeChanged += new EventHandler(comNumTrackBar_SizeChanged);
            m_trackBar.ValueChanged += new EventHandler(m_trackBar_ValueChanged);

            comNumTrackBar_SizeChanged(null, null);
        }

        #endregion

        //===================== 以下為事件處理 =====================
        void m_trackBar_ValueChanged(object sender, EventArgs e)
        {
            this._Value = m_trackBar.Value;
            m_lblValue.Text = this._Value.ToString();

            double dLoactionX = Math.Abs(((double)(this._Value - m_trackBar.Minimum) / (m_trackBar.Maximum - m_trackBar.Minimum))) * (m_trackBar.Right - m_trackBar.Left - 28)
                + m_trackBar.Left - (m_lblValue.Width / 2) + 15;
            Point pointValueLoction = new Point((int)dLoactionX, m_trackBar.Location.Y + 28);
            m_lblValue.Location = pointValueLoction;

            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        void comNumTrackBar_SizeChanged(object sender, EventArgs e)
        {
            this.Height = m_trackBar.Height + m_lblTitle.Height;

            m_lblTitle.Location = new Point((this.Width / 2) - (m_lblTitle.Width / 2), 0);
            m_lblMin.Location = new Point(0, m_lblTitle.Bottom);
            m_trackBar.Location = new Point(m_lblMin.Right, m_lblTitle.Bottom);
            m_trackBar.Width = this.Width - m_lblMin.Width - m_lblMax.Width;
            m_trackBar.TickFrequency = (m_trackBar.Maximum - m_trackBar.Minimum) / 10;

            m_lblMax.Location = new Point(this.Width - m_lblMax.Width, m_lblTitle.Bottom);
            
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }

            //m_trackBar_ValueChanged(sender, e);
        }

    }
}

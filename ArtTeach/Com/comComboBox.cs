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
    public partial class comComboBox : ComboBox
    {
        #region //=====================  區域變數設置 =====================
        comNumBox m_numBox = new comNumBox();

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
                if (value > this.Items.Count || value == 0)
                {
                    m_numBox._Value = 0;
                    this.SelectedIndex = -1;
                }
                else
                {
                    m_numBox._Value = value;
                    this.SelectedIndex = (int)m_numBox._Value - 1;
                }
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


        #endregion

        #region //=====================  必要函式設置 =====================
        /// <summary>
        /// 參數數值元件建構式
        /// </summary>
        public comComboBox()
        {
            InitializeComponent();

            this.FlatStyle = FlatStyle.Flat;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.SelectedValueChanged += new EventHandler(m_comboBox_SelectedValueChanged);

            this.Controls.Add(m_numBox);
            m_numBox.Visible = false;
        }

        #endregion

        //===================== 以下為事件處理 =====================

        void m_comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this._Value = this.SelectedIndex + 1;
        }

    }
}

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
    public partial class comTextBox : TextBox
    {
        #region //=====================  區域變數設置 =====================
        private string m_strValue = "";

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
        public string _DefaultValue
        {
            get;
            set;
        }

        /// <summary>取得或設定數值</summary>
        [Description("取得或設定數值")]
        [Category("User define")]
        public string _Value
        {
            get
            {
                return m_strValue;
            }

            set
            {
                Text = m_strValue = value;

                try
                {
                    if (Parent != null && Parent.IsHandleCreated)
                    {
                        if (this._IsSaveToLog && ucParameter.GetValueString((clsEnum.enuPmtName)this._PmtName) != Text)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, 
                                this.Parent.Name + "-" + this.Name + " => " 
                                + this._PmtName + ":"
                                + ucParameter.GetValueString((clsEnum.enuPmtName)this._PmtName) + " =>" + Text);
                        }

                        ucParameter.SetValue((clsEnum.enuPmtName)this._PmtName, m_strValue);
                        if (this._IsSaveToIni || ucParameter.IsAlwaySave)
                        {
                            ucParameter.SaveValue(this._PmtType, this._PmtName, _Value.ToString());
                        }
                    }
                }
                catch { }
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

        /// <summary>取得或設定是否只能使用數字</summary>
        [Description("取得或設定是否只能使用數字")]
        [Category("User define")]
        public bool _OnlyNumerical
        {            
            get;
            set;
        }

        /// <summary>取得或設定是否顯示輸入視窗</summary>
        [Description("取得或設定是否顯示輸入視窗")]
        [Category("User define")]
        public bool _IsShowPopForm
        {
            get;
            set;
        }

        /// <summary>取得或設定是否顯示輸入視窗</summary>
        [Description("取得或設定是否顯示輸入視窗")]
        [Category("User define")]
        public bool _IsPassWord
        {
            get;
            set;
        }

        #endregion

        #region //=====================  必要函式設置 =====================
        /// <summary>
        /// 參數數值元件建構式
        /// </summary>
        public comTextBox()
        {
            InitializeComponent();

            //this.ReadOnly = true;
            this.TextAlign = HorizontalAlignment.Center;
            this._IsSaveToIni = true;
            this._IsShowPopForm = true;
            this._IsSaveToLog = true;

            //this.Click += new System.EventHandler(this.textBoxNum_MouseClick);
            this.TextChanged += new EventHandler(comTextBox_TextChanged);
        }
        #endregion

        //===================== 以下為事件處理 =====================
        private void comNumBox_SizeChanged(object sender, EventArgs e)
        {
            this.Height = this.Height;
        }

        private void comNumBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (_IsShowPopForm)
            {
                DialogResult result = formKeyBox.GetSingleton().ShowDialog(this,_Value,_IsPassWord);
                

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    _Value = formKeyBox.GetSingleton().KeyBoxValue;
                }
            }
        }

        void comTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_OnlyNumerical)
            {
                try
                {
                    Convert.ToDouble(this.Text);
                    _Value = this.Text;
                }
                catch
                {
                    if (this.Text != "")
                    {
                        this.Text = _Value;
                        this.SelectionStart = this.Text.Length;
                    }
                }
            }
            else
            {
                _Value = this.Text;
            }
        }

    }
}

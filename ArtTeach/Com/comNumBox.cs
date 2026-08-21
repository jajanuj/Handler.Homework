using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

using ArtCommonLib;
using ArtControlLib;
using ArtData;
namespace ArtTeach
{
    /// <summary>
    /// 參數數值元件
    /// </summary>
    public partial class comNumBox : TextBox
    {
        #region //=====================  區域變數設置 =====================
        private decimal m_dDefaultValue = 0;
        private NumericUpDown m_numUpDown = new NumericUpDown() ;

        /// <summary>與控制項關聯的文件</summary>
        [Description("與控制項關聯的文件") ]
        [Category("User define") ]
        [Browsable(false) ]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                //try
                {
                    base.Text = value;
                }
                //catch
                //{ 
                
                //}
            }
        }

        /// <summary>取得或設定對應參數名稱</summary>
        [Description("取得或設定對應參數名稱") ]
        [Category("User define") ]
        public clsEnum.enuPosName? _enuName
        {
            get;
            set;
        }

        /// <summary>取得或設定最大許可值</summary>
        [Description("取得或設定最大許可值") ]
        [Category("User define") ]
        public decimal _Maximum
        {
            get
            {
                return m_numUpDown.Maximum;
            }
            set
            {
                m_numUpDown.Maximum = value;
                this.Text = m_numUpDown.Value.ToString() ;
            }
        }

        /// <summary>取得或設定最小許可值</summary>
        [Description("取得或設定最小許可值") ]
        [Category("User define") ]
        public decimal _Minimum
        {
            get
            {
                return m_numUpDown.Minimum;
            }

            set
            {
                m_numUpDown.Minimum = value;
                this.Text = m_numUpDown.Value.ToString() ;
            }
        }

        /// <summary>取得或設定小數點位數</summary>
        [Description("取得或設定小數點位數") ]
        [Category("User define") ]
        public int _DecimalPlaces
        {
            get
            {
                return m_numUpDown.DecimalPlaces;
            }

            set
            {
                m_numUpDown.DecimalPlaces = value;
                this.Text = m_numUpDown.Value.ToString() ;
            }
        }

        /// <summary>取得或設定數值</summary>
        [Description("取得或設定數值") ]
        [Category("User define") ]
        public decimal _TargetValue
        {
            get
            {
                return m_numUpDown.Value;
            }
            set
            {
                if (m_numUpDown.Minimum >= value) 
                {
                    m_numUpDown.Value = m_numUpDown.Minimum;
                }
                else if (m_numUpDown.Maximum <= value) 
                {
                    m_numUpDown.Value = m_numUpDown.Maximum;
                }
                else
                {
                    m_numUpDown.Value = value;
                }

                if (Parent != null && Parent.IsHandleCreated) 
                {
                    if (value == 0) 
                    {

                    }

                    if (this._enuName != null) 
                    {
                        if (this._IsSaveToLog && ucPosPmt.GetValue((clsEnum.enuPosName) this._enuName) != m_numUpDown.Value) 
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog,
                                this.Parent.Name + "-" + this.Name + " => "
                                + this._enuName + ":"
                                + ucPosPmt.GetValue((clsEnum.enuPosName) this._enuName) + " =>" + m_numUpDown.Value) ;
                        }

                        ucPosPmt.SetValue((clsEnum.enuPosName) this._enuName, _TargetValue.ToString() ) ;

                        if (this._IsSaveToIni) 
                        {
                            ucPosPmt.SaveValue((clsEnum.enuPosName) this._enuName, _TargetValue.ToString() ) ;
                        }
                    }
                }

                string strFormat = "0.";
                for (int iPlacesNum = 0; iPlacesNum < this._DecimalPlaces; iPlacesNum++) 
                {
                    strFormat += "#";
                }

                this.Text = m_numUpDown.Value.ToString(strFormat) ;
            }
        }

        /// <summary>取得或設定是否寫入Ini File</summary>
        [Description("取得或設定是否寫入Ini File") ]
        [Category("User define") ]
        public bool _IsSaveToIni
        {            
            get;
            set;
        }

        /// <summary>取得或設定數值改變時是否寫入Button Log</summary>
        [Description("取得或設定數值改變時是否寫入Button Log") ]
        [Category("User define") ]
        public bool _IsSaveToLog
        {
            get;
            set;
        }

        /// <summary>取得或設定是否顯示輸入視窗</summary>
        [Description("取得或設定是否顯示輸入視窗") ]
        [Category("User define") ]
        public bool _IsShowPopForm
        {
            get;
            set;
        }

        #endregion

        #region //=====================  必要函式設置 =====================
        /// <summary>
        /// 參數數值元件建構式
        /// </summary>
        public comNumBox() 
        {
            InitializeComponent() ;

            this.Text = m_numUpDown.Value.ToString() ;
            this.ReadOnly = true;
            this.TextAlign = HorizontalAlignment.Center;
            _IsSaveToIni = true;
            _IsShowPopForm = true;
            _IsSaveToLog = true;

            //this.Multiline = true;
            //this.FontHeight = this.Height;
            //this.Click += new System.EventHandler(this.textBoxNum_MouseClick) ;
            //this.TextChanged += new System.EventHandler(this.textBoxNum_TextChanged) ;
        }

       
        
        #endregion

        //===================== 以下為事件處理 =====================
        private void comNumBox_SizeChanged(object sender, EventArgs e) 
        {
            //this.FontHeight = this.Height;
            //this.Height = this.Height;
            //this.Font.Size = this.Height / 3;
            //this.Font = new System.Drawing.Font(this.Font.FontFamily, this.Height / 3) ;
        }

        private void comNumBox_MouseClick(object sender, MouseEventArgs e) 
        {
            if (_IsShowPopForm) 
            {
                DialogResult result = FormNumBox.GetSingleton().ShowDialog(this, _TargetValue.ToString() , (int) _Maximum, (int) _Minimum, _DecimalPlaces) ;

                if (result == System.Windows.Forms.DialogResult.OK) 
                {
                    _TargetValue = (decimal) FormNumBox.GetSingleton().NumBoxValue;
                }
            }
        }

    }


    //public class CornerIndexConverter : EnumConverter
    //{
    //    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
    //    {
    //        return sourceType == typeof(string) ;
    //    }
    //    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) 
    //    {
    //        return Enum.Parse(typeof(CORNER_INDEX) , value.ToString().ToUpper().Replace(' ', '_') ) ;
    //    }
    //    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) 
    //    {
    //        return destinationType == typeof(string) ;
    //    }
    //    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) 
    //    {
    //        string s = value.ToString() ;
    //        int i = s.IndexOf('_') ;
    //        return s[0] + s.Substring(1, i - 1).ToLower() + ' ' + s[i + 1] + s.Substring(i + 2).ToLower() ;
    //    }
    //    public CornerIndexConverter() : base(typeof(CORNER_INDEX) ) { }
    //}
}

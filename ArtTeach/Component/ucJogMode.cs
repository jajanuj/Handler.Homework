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
    public partial class ucJogMode : UserControl
    {
        #region//========== Event ==========
        #endregion

        #region//========== 屬性參數 ===========

        private int m_TeachSpeed = 10;
        [Description("取得或設定 移動速度百分比") ]
        [DisplayName("Teach Speed") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public int _TeachSpeed
        {
            set
            {
                trackBar1.Value = value;
                m_TeachSpeed = trackBar1.Value;
                txtSpeed.Text = trackBar1.Value.ToString() ;
            }
            get
            {
                return m_TeachSpeed;
            }
        }

        private double m_RelativeValue = 1;
        [Description("取得或設定 相對移動距離") ]
        [DisplayName("Relative Target") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public double _RelativeValue
        {
            set
            {
                if (value > 50) 
                {
                    m_RelativeValue = 50;
                }
                else
                {
                    m_RelativeValue = value;
                }
                btnSetRelativeValue.Text = m_RelativeValue.ToString();
                _IsContinueMode = false;
            }
            get
            {
                return m_RelativeValue;
            }
        }

        private bool m_IsContinueMode = false;
        [Description("取得或設定 是否為連續移動模式") ]
        [DisplayName("Is Continue Mode") , CategoryAttribute("ArtMMI") , Browsable(true) ]
        public bool _IsContinueMode
        {
            set
            {
                m_IsContinueMode = value;
                btnSetRelativeValue.Text = m_RelativeValue.ToString();
                SetJogColor(btnSetContinue, m_IsContinueMode) ;
                SetJogColor(btnRelativeMode, !m_IsContinueMode) ;
            }
            get
            {
                return m_IsContinueMode;
            }
        }

        #endregion

        #region//========== Public 參數 ==========

        
        #endregion

        #region//========== Private 參數 ===========
       

        #endregion
        
        #region//========== 建構式 ==========
        public ucJogMode() 
        {
            InitializeComponent() ;
        }
        #endregion

        #region//========== public Function ===========
        public void UpdateControls()
        {
            label_Speed.Text = clsLanguage.GetTranslation("Speed %:");
            btnSetContinue.Text = clsLanguage.GetTranslation("Continue");
            btnRelativeMode.Text = clsLanguage.GetTranslation("Relative");
        }
        public void ReflashControls() 
        {
        }
        #endregion

        #region//========== private Function ===========

        private void SetJogColor(Control Item, bool IsSelected) 
        {
            Item.BackColor = IsSelected ? Color.Gold : Color.Transparent;
            Item.ForeColor = IsSelected ? Color.Blue : Color.Gray;
        }

        #endregion

        #region//========== Controls Event ==========

        private void JogModeClick(object sender, EventArgs e) 
        {
            Control Item = (Control) sender;
            if (sender == btnSetContinue) 
            {
                _IsContinueMode = true;
            }
            if (sender == btnRelativeMode) 
            {
                _IsContinueMode = false;
            }
            else if (sender == btnSetRelativeValue) 
            {
                _IsContinueMode = false;
                if (FormNumBox.GetSingleton().ShowDialog(this, _RelativeValue.ToString() , 50, 0, 3) 
                    == DialogResult.OK) 
                {
                    _RelativeValue = FormNumBox.GetSingleton().NumBoxValue;
                }
                else
                {
                    return;
                }
            }
            else if (sender == btnRelativeValue1) 
            {
                _IsContinueMode = false;
                _RelativeValue = Convert.ToDouble(btnRelativeValue1.Text) ;
            }
            else if (sender == btnRelativeValue2) 
            {
                _IsContinueMode = false;
                _RelativeValue = Convert.ToDouble(btnRelativeValue2.Text) ;
            }
            else if (sender == btnRelativeValue3) 
            {
                _IsContinueMode = false;
                _RelativeValue = Convert.ToDouble(btnRelativeValue3.Text) ;
            }
            else if (sender == btnRelativeValue4) 
            {
                _IsContinueMode = false;
                _RelativeValue = Convert.ToDouble(btnRelativeValue4.Text) ;
            }
            else
            {
                return;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e) 
        {
            _TeachSpeed = trackBar1.Value;

        }

        private void ChangetrackBar1BackColor(object sender)
        {
            Control UC = (Control)sender;
            if (UC.BackColor == Color.Transparent)
            {
                if (UC.Parent != null)
                {
                    ChangetrackBar1BackColor(UC.Parent);
                }
                else
                {
                    UC.ParentChanged += new EventHandler(ucJogMode_ParentChanged);
                }
            }
            else
            {
                trackBar1.BackColor = UC.BackColor;
                UC.BackColorChanged += new EventHandler(ucJogMode_BackColorChanged);
            }
        }

        #endregion


        private void ucJogMode_ParentChanged(object sender, EventArgs e)
        {
            Control UC = (Control)sender;
            ChangetrackBar1BackColor(sender);
        }

        private void ucJogMode_BackColorChanged(object sender, EventArgs e)
        {
            ChangetrackBar1BackColor(sender);
        }







    }
}
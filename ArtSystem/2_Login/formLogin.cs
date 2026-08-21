using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtData;
using ArtCommonLib;
using ArtControlLib;

namespace ArtSystem.Login
{
    /// <summary> TQIC 通告
    /// 有鑑於近日因廠外應用工程團隊支援各處裝交機及後續機台維護工作需求
    /// 部分設備未能有一致的帳號權限,導致人員維護困難
    /// TQIC系控會議於2022/7/4討論後決議
    /// 
    /// 即日起,未來所有設備需於出廠前增設ArtAE帳號
    /// 帳號: ArtAE
    /// 密碼:076071828
    /// 
    /// 此帳號分級為 Level 1
    /// 
    /// 帳號分級設定規格如下
    /// SuperUser Level 0
    /// ArtAE  Level 1
    /// 調機者 Level 3
    /// 工程師 Level 5
    /// 操作員 Level 9
    /// 
    /// 煩請各開發單位配合,謝謝
    /// 
    /// 敬會 生技處、品保處
    /// 廠內調機工作均以ArtAE帳號進行,如帳號功能權限開通不足者,需通知RD單位進行設定,確保出機時
    /// ArtAE帳號有足夠的調機功能權限,謝謝
    /// </summary>
    public partial class formLogin : Form
    {
        #region //=====================  區域變數設置 =====================
        private bool bIsLogin = false;
        static formLogin m_Singleton;

        int iMouseRightCount = 0;
        int iMouseLeftCount = 0;

        public string str_LoginDate = "";
        public string str_LoginTime = "";

        #endregion
        
        #region //=====================  必要函式設置 =====================
        /// <summary>
        /// 取得唯一物件，避免重覆設置
        /// </summary>
        /// <returns>回傳物件</returns>
        static public formLogin GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new formLogin();
            }
            return m_Singleton;
        }

        /// <summary>
        /// 物件建立請利用 GetSingleton() ，除非特殊需求
        /// </summary>
        private formLogin()
        {
            InitializeComponent();
            iMouseLeftCount = 0;
            iMouseRightCount = 0;
        }

        /// <summary>
        /// 設置畫面
        /// </summary>
        private void UpdateControls()
        {

        }

        #endregion
        
        #region //===================== public 函式設置 =====================

        /// <summary> 顯示登入視窗 </summary>
        public bool _ShowLoginForm()
        {
            clsCmData.g_strNowUser = "";
            clsCmData.g_iNowUserLevel = 0;
            textBoxPassWord.Text = "";
            textBoxUserName.Text = "";
            ucArtMain_Design.GetSingleton()._HideAllFunc();

            formAlarmReport.GetSingleton().bIsAutoShow = false;
            if (formLogin.GetSingleton().Visible == false)
            {
                if (ucArtMain_Design.GetSingleton().Parent != null)
                {
                    formLogin.GetSingleton().ShowDialog(ucArtMain_Design.GetSingleton().Parent);
                }
                else
                {
                    formLogin.GetSingleton().ShowDialog(ucArtMain_Design.GetSingleton());
                }
            }
            else
            {
                formLogin.GetSingleton().Activate();
            }
            return bIsLogin;
        }

        /// <summary> 登入帳號 </summary>
        public bool _Login(string UserName, string Password)
        {
            bIsLogin = false;
            if (UserName == "Operator")
            {
                clsCmData.g_strNowUser = UserName;
                clsCmData.g_iNowUserLevel = 9;
                clsCmData.g_strNowUserPassword = "";
                bIsLogin = true;
            }
            else if (UserName == "ArtAE" && Password == "076071828")
            {
                clsCmData.g_strNowUser = UserName;
                clsCmData.g_iNowUserLevel = 1;
                clsCmData.g_strNowUserPassword = "";
                bIsLogin = true;
            }
            else
            {
                bIsLogin = PassWordCheck(textBoxUserName.Text, textBoxPassWord.Text);
            }
            //登入後處理
            if (bIsLogin)
            {
                //寫入Log
                clsLog.Log(clsCmData.enuLogType.LoginLog, "[Login (LV" + clsCmData.g_iNowUserLevel + ")] User Name : " + clsCmData.g_strNowUser);

                str_LoginDate = DateTime.Now.ToString("yyyy/MM/dd");
                str_LoginTime = DateTime.Now.ToString("HH:mm:ss");

                //更新 ucUserAccount 介面
                ucUserAccount.GetSingleton().UpdateControls();


                //設置ACM 按鈕權限
                for (int iBtnNum = 0; iBtnNum < clsCmData.g_dctAcmBtnLib.Count; iBtnNum++)
                {
                    bool bIsUsable = ucUserAccount.GetSingleton().IsSubFuncUsable(clsCmData.g_dctAcmBtnLib.ElementAt(iBtnNum).Key);

                    ((Button)clsCmData.g_dctAcmBtnLib.ElementAt(iBtnNum).Value).Enabled = bIsUsable;
                }

                ucArtMain_Design.GetSingleton()._UpdateControls();
               

                this.Hide();
                if (this.Owner != null)
                {
                    this.Owner.Focus();
                }
                formAlarmReport.GetSingleton().bIsAutoShow = true;
            }
            return bIsLogin;
        }

        #endregion

        #region //===================== private 函式設置 =====================
        private bool PassWordCheck(string p_strName, string p_strPassWord)
        {
            string strIsCheckOk = ucUserAccount.GetSingleton().IsCheckPassWordOk(p_strName, p_strPassWord);
            bool bIsLogin = false;

            if (strIsCheckOk == "Super")
            {
                clsCmData.g_strNowUser = "SuperUser";
                clsCmData.g_iNowUserLevel = 0;
                clsCmData.g_strNowUserPassword = "XXXXXX";

                bIsLogin = true;
            }
            else if (strIsCheckOk == "OK")
            {
                clsCmData.g_strNowUser = p_strName;
                clsCmData.g_iNowUserLevel = ucUserAccount.GetSingleton().GetUserLevel(p_strName);
                clsCmData.g_strNowUserPassword = p_strPassWord;
                if (clsCmData.g_iNowUserLevel < 0)
                {
                    formMessageBox.Show("Read Ini file Error, Can not login !!");
                    return false;
                }
                bIsLogin = true;
            }
            else
            {
                formMessageBox.Show("User name or password error, retype again !!");
            }

            return bIsLogin;
        }
        #endregion

        #region //===================== 以下為事件處理 =====================

        /// <summary> 登入帳號(正常登入) </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            this._Login(textBoxUserName.Text, textBoxPassWord.Text);
        }
        /// <summary> 登入帳號(OP登入) </summary>
        private void btnOperator1_Click(object sender, EventArgs e)
        {
            this._Login("Operator", textBoxPassWord.Text);
        }
        /// <summary> 輸入密碼後按下Enter 登入 </summary>
        private void KeyDownEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
        /// <summary> 使用 formKeyBox()輸入UserName </summary>
        private void btnUserName_ShowKeyBoard_Click(object sender, EventArgs e)
        {
            DialogResult result = formKeyBox.GetSingleton().ShowDialog(this, "", false);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                textBoxUserName.Text = formKeyBox.GetSingleton().KeyBoxValue;
            }

        }
        /// <summary> 使用 formKeyBox()輸入Password </summary>
        private void btnPassword_ShowKeyBoard_Click(object sender, EventArgs e)
        {
            DialogResult result = formKeyBox.GetSingleton().ShowDialog(this, "", false);
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                textBoxPassWord.Text = formKeyBox.GetSingleton().KeyBoxValue;
            }

        }
        /// <summary> SuperUser 帳號快速輸入 </summary>
        private void palPassWD_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                iMouseRightCount++;
                if (iMouseRightCount > 3
                    || iMouseLeftCount > 2)
                {
                    iMouseRightCount = 0;
                    iMouseLeftCount = 0;
                }
                else if (iMouseRightCount == 3
                    && iMouseLeftCount == 2)
                {
                    clsLog.Log(clsCmData.enuLogType.LoginLog, "[" + this.Name + "] Super User Account Set.");
                    textBoxUserName.Text = "ArtAE";
                    textBoxPassWord.Text = "076071828";
                    iMouseRightCount = 0;
                    iMouseLeftCount = 0;
                }
                else if (iMouseLeftCount >= iMouseRightCount)
                {
                    iMouseRightCount = 0;
                    iMouseLeftCount = 0;
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                iMouseLeftCount++;
                if (iMouseRightCount > 2
                   || iMouseLeftCount > 2)
                {
                    iMouseRightCount = 0;
                    iMouseLeftCount = 0;
                }
                else if (iMouseLeftCount != iMouseRightCount)
                {
                    iMouseRightCount = 0;
                    iMouseLeftCount = 0;
                }
            }
        }
        /// <summary> 確保有使用者登入後才能關閉Login Form() </summary>
        private void formLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clsCmData.g_strNowUser == "")
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
        }

        #endregion
    }
}

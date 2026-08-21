using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtControlLib;
using ArtCommonLib;
using ArtSystem;
using ArtData;
using ArtEQ;

namespace ArtEQ
{
    public partial class ucTitle : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================


        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucTitle m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucTitle GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucTitle();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucTitle()
        {
            InitializeComponent();
            //如果程式有開啟才執行以下內容,否則開發程式會很卡
            if (clsArtSystem.bIsProgramOpen == true)
            {
                ucParameter.Add(this);//將此UC掛載到ucParameter內 (這樣ucParameter.RefreshControlData()時會自動更新此介面的參數物件)
                this.TimerInterval = 100;//將更新週期設定為100ms [-->ReflashTimerFunc()]
                UpdateControls();
            }
        }
        
        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            BaseControls_Update();//物件重置 - 標準元件、刪除Log
            this.comSignalTower1.SetReflashTimerStart(false);
        }

        /// <summary> 自動更新介面參數 </summary>
        protected override void ReflashTimerFunc()
        {
            BaseControls_Reflash();//自動更新介面 : 標準元件、
            this.comSignalTower1.SetReflashTimerStart(false);
        }

        private void ucTitle_PC_VisibleChanged(object sender, EventArgs e)
        {
            //this.comSignalTower1.SetReflashTimerStart(this.Visible);
        }
        #endregion

        #region //===================== private 函式設置 =====================

        /// <summary> 物件重置 - 標準元件 </summary>
        private void BaseControls_Update()
        {
            //顯示目前版本號
            textVersion.Text = System.Windows.Forms.Application.ProductVersion.ToString();
        }
        /// <summary> 自動更新介面 - (標準元件, 機台狀態 , 警告訊息) </summary>
        private void BaseControls_Reflash()
        {
            #region//Text顯示: 日期、時間、NowUser、Recipe名稱
            textBoxDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            textBoxTime.Text = DateTime.Now.ToString("HH:mm:ss");
            btnLogin.Text = clsCmData.g_strNowUser;
            btnRecipeId.Text = System.IO.Path.GetFileName(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe));
            #endregion

            #region//機台狀態
            if (btnEqStatus.Text != clsLanguage.GetTranslation(clsCmData.g_strNowEqStatus))
            {
                btnEqStatus.Text = clsLanguage.GetTranslation(clsCmData.g_strNowEqStatus);
                comSignalTower1._SignalTowerStatus = ucSignalTowerManage.GetSingleton().GetSingalTowerStatus(clsCmData.g_strNowEqStatus);
            }
            if (formAlarmReport.IsAlarmOccur()
                && DateTime.Now.Second % 2 == 1)
            {
                btnEqStatus._Color = Color.IndianRed;
            }
            else
            {
                btnEqStatus._Color = Color.FromArgb(82, 101, 126);
            }
            #endregion

            #region//警告訊息
            PublicDeclare.ReflashWarnningMessage();
            label_WarningMessage.Text = clsLanguage.GetTranslation("Warning Message") + " : " + ucWarnningMessage.GetSingleton()._GetWarningCount().ToString();
            btnWarningMessage.Text = ucWarnningMessage.GetSingleton()._GetWarnningMessage();
            if (ucWarnningMessage.GetSingleton()._GetWarningCount() > 0
                && DateTime.Now.Second % 2 == 1)
            {
                btnWarningMessage._Color = Color.Orange;
            }
            else
            {
                btnWarningMessage._Color = Color.FromArgb(82, 101, 126);
            }
            #endregion
        }
      

        #endregion

        #region//===================== 以下為事件處理 (btnLogin, btnAllring-DebugForm, btnWarnningMessage, btnRecipeId ) =====================

        #region//===== btnLogin =====
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (formMessageBox.Show("Are you sure logout?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                clsLog.Log(clsEnum.enuLogName.LoginLog, clsCmData.g_strNowUser + " ," + this.Name + ",Logout Clicked");
                ucArtMain_Design.GetSingleton()._HideAllFunc();
                formLogin.GetSingleton()._ShowLoginForm();
                formAlarmReport.GetSingleton().bIsAutoShow = false;
            }
        }
        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            Control Item = (Control)sender;
            string strMessage = "";
            strMessage += clsLanguage.GetTranslation("User ID: ") + clsCmData.g_strNowUser + "\r\n";
            strMessage += clsLanguage.GetTranslation("User Level: ") + clsCmData.g_iNowUserLevel + "\r\n";
            strMessage += clsLanguage.GetTranslation("Login Date: ") + formLogin.GetSingleton().str_LoginDate + "\r\n";
            strMessage += clsLanguage.GetTranslation("Login Time: ") + formLogin.GetSingleton().str_LoginTime + "\r\n";


            strMessage += "\r\n";
            if (ArtSystem.Login.ucAutoLogout.GetSingleton()._GetAutoLogout_Enable() == true)
            {
                int iLogoutLevel = ArtSystem.Login.ucAutoLogout.GetSingleton()._GetAutoLogout_Level();
                if (clsCmData.g_iNowUserLevel < iLogoutLevel)
                {
                    strMessage += clsLanguage.GetTranslation("Auto Logout Level: ") + iLogoutLevel.ToString("F0") +"\r\n";
                    double dRemainTime = ArtSystem.Login.ucAutoLogout.GetSingleton()._GetRemainTime_Minute();
                    if (dRemainTime >= 1)
                    {
                        strMessage += clsLanguage.GetTranslation("Auto Logout Remain Time: ")
                            + dRemainTime.ToString("F0") + " " + clsLanguage.GetTranslation("(minute)") + "\r\n";
                    }
                    else
                    {
                        dRemainTime *= 60;
                        strMessage += clsLanguage.GetTranslation("Auto Logout Remain Time: ")
                            + dRemainTime.ToString("F0") + " " + clsLanguage.GetTranslation("(sec)") + "\r\n";
                    }
                }
            }

            strMessage += clsLanguage.GetTranslation("(Click To Logout)") ;
            toolTip1.AutoPopDelay = 10000;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(Item.Controls[0], strMessage);
            toolTip1.ShowAlways = true;
        }
        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.RemoveAll();
            toolTip1.Hide(this);
        }
        #endregion

        #region//===== btnAllring-DebugForm =====
        private void btnAllring_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (ucUserAccount.GetSingleton().IsSubFuncUsable(ucArtMain_Design.enuACM_Premit.ArtSystem_ACM_ShowDebugForm.ToString()) == true)
                {
                    formDebug.GetSingleton().ShowDebugForm(this);
                }
            }
        }
        #endregion

        #region//===== btnWarnningMessage =====
        private void btnWarningMessage_Click(object sender, EventArgs e)
        {
            ucWarnningMessage.GetSingleton()._ShowForm();
        }
        private void btnWarningMessage_MouseEnter(object sender, EventArgs e)
        {
            Control Item = (Control)sender;
            string strMessage = ucWarnningMessage.GetSingleton()._GetWarnningMessage_List();
            strMessage += "\r\n\r\n";
            strMessage += clsLanguage.GetTranslation("(Click To Check Warning Message)");
            toolTip1.AutoPopDelay = 10000;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(Item.Controls[0], strMessage);
            toolTip1.ShowAlways = true;
        }
        private void btnWarningMessage_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.RemoveAll();
            toolTip1.Hide(this);
        }
        #endregion

        #region//===== btnRecipeId =====
        private void btnRecipeId_Click(object sender, EventArgs e)
        {
            ucRecipeManager.GetSingleton()._ShowForm();
        }
        private void btnRecipeId_MouseEnter(object sender, EventArgs e)
        {
            Control Item = (Control)sender;
            string strMessage = clsLanguage.GetTranslation("Current Recipe Path :");
            strMessage += "\r\n" + ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe);
            strMessage += "\r\n\r\n";
            strMessage += clsLanguage.GetTranslation("(Click To Change Recipe)");
            toolTip1.AutoPopDelay = 10000;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(Item.Controls[0], strMessage);
            toolTip1.ShowAlways = true;
        }
        private void btnRecipeId_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.RemoveAll();
            toolTip1.Hide(this);
        }
        #endregion


        #endregion
    }
}

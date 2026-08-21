using System;
using System.Drawing;
using System.Windows.Forms;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtSystem;

namespace ArtEQ
{
    public partial class ucTitle_Panel : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================

        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucTitle_Panel m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucTitle_Panel GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucTitle_Panel();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucTitle_Panel()
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
            ArtSystem.MultiSystem.clsInitialControsl.Initial_comSignalTower(this.comSignalTower1);
            this.comSignalTower1.SetReflashTimerStart(true);
        }

        /// <summary> 自動更新介面參數 </summary>
        protected override void ReflashTimerFunc()
        {
            //this.comSignalTower1.SetReflashTimerStart(true);
            BaseControls_Reflash();//自動更新介面 - 標準元件、刪除Log、備份artEqParameter.ini、自動登出/登入
            this.comSignalTower1.SetReflashTimerStart(true);
            btnLockDoor.Text = clsDioCtrl.GetDo(clsEnum.enuDo.Safety_Door_Lock) ? "Unlock Door" : "Lock Door";
        }
        private void ucTitle_Panel_VisibleChanged(object sender, EventArgs e)
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
        /// <summary> 自動更新介面 - 標準元件 </summary>
        private void BaseControls_Reflash()
        {
            #region//Text顯示: 日期、時間、NowUser、Recipe名稱
            textBoxDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            textBoxTime.Text = DateTime.Now.ToString("HH:mm:ss");
            btnLogin.Text = clsCmData.g_strNowUser;
            btnRecipeId.Text = System.IO.Path.GetFileName(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe));
            #endregion

            #region//機台狀態
            btnEqStatus.Text = clsLanguage.GetTranslation(clsCmData.g_strNowEqStatus);
            comSignalTower1._SignalTowerStatus = ucSignalTowerManage.GetSingleton().GetSingalTowerStatus(clsCmData.g_strNowEqStatus);
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
        }

        #endregion

        #region//===================== 以下為事件處理 (btnLogin, ) =====================
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (formMessageBox.Show("Are you sure logout?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                clsLog.Log(clsEnum.enuLogName.LoginLog, clsCmData.g_strNowUser + " ," + this.Name + ",Logout - " + "Ver：" + System.Windows.Forms.Application.ProductVersion.ToString());
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
            strMessage += "\r\n" + clsLanguage.GetTranslation("(Click To Logout)");
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


        private void ucRoundButton1_Click(object sender, EventArgs e)
        {
            ucRecipeManager.GetSingleton()._ShowForm();
        }
        #endregion

        private void btnLockDoor_Click(object sender, EventArgs e)
        {
            clsDioCtrl.SetDo(clsEnum.enuDo.Safety_Door_Lock, !clsDioCtrl.GetDo(clsEnum.enuDo.Safety_Door_Lock));
        }

    }
}

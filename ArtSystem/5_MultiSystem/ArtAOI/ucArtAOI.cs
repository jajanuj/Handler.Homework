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
using ArtData;
using ArtSystem;

namespace ArtSystem.MultiSystem
{
    public partial class ucArtAOI : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================

        private bool bIsEditing = false;
        public clsArtAOI mArtAOI = new clsArtAOI();

        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucArtAOI m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucArtAOI GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucArtAOI();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucArtAOI()
        {
            InitializeComponent();
            //if (clsArtSystem.bIsProgramOpen == false)
            //{  return; }
            this.TimerInterval = 100;
            bIsEditing = false;
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                if (this.Parent != null)
                {
                    this.Size = this.Parent.ClientSize;
                }
                btnSave_ArtGrabSetting.Enabled = bIsEditing;
                ConvertDataToUI();
            }
            catch (Exception ex)
            {
                clsLog.Log(clsArtSystem.g_strCatchLogName, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        /// <summary> 自動更新介面參數 </summary>
        protected override void ReflashTimerFunc()
        {
            try
            {
                SetBtnColorFlash(btnSave_ArtGrabSetting, bIsEditing);;
            }
            catch (Exception ex)
            {
                clsLog.Log(clsArtSystem.g_strCatchLogName, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        public ArtGrab.clsArtGrabCtrl GetArtAOIGrabCtrl()
        {
            return mArtAOI.mGrab;
        }

        #endregion

        #region //===================== private 函式設置 (SetBtnColor, Covert-Data&UI) =====================

        private void SetBtnColorFlash(Button pButton, bool Flash)
        {
            if (Flash && DateTime.Now.Second % 2 == 1)
            {
                pButton.BackColor = Color.Lime;
            }
            else
            {
                pButton.BackColor = this.BackColor;
                pButton.UseVisualStyleBackColor = true;
            }
        }

        private void ConvertDataToUI()
        {
            try
            {

            }
            catch (Exception ex)
            {
                clsLog.Log(clsArtSystem.g_strCatchLogName, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }
        private void ConvertUIToData()
        {
            try
            {

            }
            catch (Exception ex)
            {
                clsLog.Log(clsArtSystem.g_strCatchLogName, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        #endregion

        #region//===================== 以下為事件處理 (VisibleChanged, tabPageChanged, dgvEnableChanged) =====================

        private void ucCardSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                bIsEditing = false;
                mArtAOI.Load(mArtAOI.sINIPath);
                UpdateControls();
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bIsEditing = false;
            mArtAOI.Load(mArtAOI.sINIPath);
            UpdateControls();
        }

        private void btnSave_ArtGrabSetting_EnabledChanged(object sender, EventArgs e)
        {
            btnCancel_ArtGrabSetting.Enabled = btnSave_ArtGrabSetting.Enabled;
        }
        #endregion

        #region//===================== 以下為事件處理 (Edit, Cancel, Save) =====================

        private void btnEdit_CardSetting_Click(object sender, EventArgs e)
        {
            bIsEditing = !bIsEditing;
            mArtAOI.Load(mArtAOI.sINIPath);
            UpdateControls();
        }
        private void btnCancel_CardSetting_Click(object sender, EventArgs e)
        {
            bIsEditing = false;
            mArtAOI.Load(mArtAOI.sINIPath);
            UpdateControls();
        }
        private void btnSave_CardSetting_Click(object sender, EventArgs e)
        {
            if (formMessageBox.Show("Save setting need to restart program.", "Save ART-AOI Setting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConvertUIToData();
                bIsEditing = false;
                mArtAOI.Save(mArtAOI.sINIPath);
                UpdateControls();
                clsMultiSystem.SetSettingChangeFlag();
            }
        }

        #endregion

        #region//===================== 以下為事件處理 (OpenGrapSettingForm) =====================

        private void btn_OpenGrapSettingForm_Click(object sender, EventArgs e)
        {
            ArtGrab.clsArtGrabCtrl.IniPath = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath + "\\GrabSetting.ini";
            ArtGrab.clsArtGrabCtrl.SysDir = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath;
            ArtGrab.clsArtGrabCtrl.SetGrabSetting();
        }

        #endregion

    }
}

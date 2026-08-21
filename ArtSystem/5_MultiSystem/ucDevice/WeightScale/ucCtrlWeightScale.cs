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
    public partial class ucCtrlWeightScale : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public Dictionary<clsPmtWeightScale.enuPmtName, string> pPmt = null;
        public clsCtrlWeightScale pWeightScale = null;
        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucCtrlWeightScale()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            ucArtMain_Design.GetSingleton()._CollectChangeLanguageControls(this);
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                if (pPmt != null)
                {
                    labName.Text = clsLanguage.GetTranslation(pPmt[clsPmtWeightScale.enuPmtName.sControllerName], false);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 自動更新介面參數 </summary>
        protected override void ReflashTimerFunc()
        {
            try
            {
                if (pWeightScale != null)
                {
                    txt_LaserValue.Text = Math.Round(pWeightScale.GetWeightResult(), 5).ToString();
                    //if (pWeightScale.eErrorCode == clsCtrlWeightScale.enuErrorCode.None)
                    //{
                    //    txt_LaserValue.Text = pWeightScale.LastHeightValue.ToString("F3");
                    //}
                    //else
                    //{
                    //    txt_LaserValue.Text = pWeightScale.eErrorCode.ToString();
                    //}
                    //if (bLive == true)
                    //{
                    //    btnWeightScale_Live.BackColor = Color.Lime;
                    //    pWeightScale._GetHeightValue();
                    //}
                    //else
                    //{
                    //    btnWeightScale_Live.BackColor = SystemColors.Control;
                    //}
                    btnWeightScale_CancelCmd.BackColor = pWeightScale.IsBusy() ? Color.Lime : SystemColors.Control;
                    btnWeightScale_Connect.BackColor = pWeightScale.Com_IsConnected() ? Color.Lime : SystemColors.Control;
                    btnWeightScale_Disconnect.BackColor = pWeightScale.Com_IsConnected() ? SystemColors.Control : pWeightScale.bIsSimulatorMode ? Color.Gray : Color.Red;
                    if (pWeightScale.bIsSimulatorMode == true)
                    {
                        btnWeightScale_Disconnect.Text = clsLanguage.GetTranslation("Simulate", false);
                    }
                    else
                    {
                        btnWeightScale_Disconnect.Text = clsLanguage.GetTranslation("Disconnect", false);
                    }
                    //btnWeightScale_Bypass.BackgroundImage = pWeightScale._GetValue(clsCtrlWeightScale.enuParameterName.ByPass) == 1 ? global::ArtSystem.Properties.Resources.Yes : global::ArtSystem.Properties.Resources.No;
                    //btnWeightScale_Simulate.BackgroundImage = pWeightScale._GetValue(clsCtrlWeightScale.enuParameterName.Simulate) == 1 ? global::ArtSystem.Properties.Resources.Yes : global::ArtSystem.Properties.Resources.No;
                    //txtWeightScale_Delay_ms.Text = pWeightScale._GetValue(clsCtrlWeightScale.enuParameterName.DelayTime_ms).ToString("");
                    //txtWeightScale_Timeout_ms.Text = pWeightScale._GetValue(clsCtrlWeightScale.enuParameterName.TimeOut_ms).ToString("");
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================


        #endregion

        #region //===================== private 函式設置 () =====================

        #endregion

        #region//===================== 以下為事件處理 () =====================

        private void ucCtrlWeightScale_VisibleChanged(object sender, EventArgs e)
        {
            this.SetReflashTimerStart(this.Visible);
        }
        private void btnWeightScale_Read_Click(object sender, EventArgs e)
        {
            if (pWeightScale != null)
            {
                pWeightScale.GetWeight_Immediately();
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pWeightScale.g_sName + " -> " + ((Control)sender).Name + ", Clicked");
            }
        }
        private void btnWeightScale_Live_Click(object sender, EventArgs e)
        {
            if (pWeightScale != null)
            {
                pWeightScale.RunGetWeight_Stable();
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pWeightScale.g_sName + " -> " + ((Control)sender).Name + ", Clicked");
            }
        }
        private void btnWeightScale_ResetZero_Click(object sender, EventArgs e)
        {
            if (pWeightScale != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pWeightScale.g_sName + " -> " + ((Control)sender).Name + ", Clicked");
                pWeightScale.RunResetValue();
            }
        }
        private void btnWeightScale_CancelCmd_Click(object sender, EventArgs e)
        {
            if (pWeightScale != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pWeightScale.g_sName + " -> " + ((Control)sender).Name + ", Clicked");
                pWeightScale.RunCancelCmd();
            }
        }
        private void btnWeightScale_Connect_Click(object sender, EventArgs e)
        {
            if (pWeightScale != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pWeightScale.g_sName + " -> " + ((Control)sender).Name + ", Clicked");
                pWeightScale.Com_Open();
            }
        }
        private void btnWeightScale_Disconnect_Click(object sender, EventArgs e)
        {
            if (pWeightScale != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pWeightScale.g_sName + " -> " + ((Control)sender).Name + ", Clicked");
                pWeightScale.Com_Close();
            }
        }
        private void btnWeightScale_Calibration_Click(object sender, EventArgs e)
        {
            if (pWeightScale != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pWeightScale.g_sName + " -> " + ((Control)sender).Name + ", Clicked");
                ucWeightScaleCalibration.GetSingleton()._ShowForm(pWeightScale);
            }
        }
        private void btnWeightScale_SetLimit_Click(object sender, EventArgs e)
        {

        }

        #endregion

    }
}

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
    public partial class ucCtrlBottomCCD : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public Dictionary<clsPmtBottomCCD.enuPmtName, string> pPmt = null;
        public clsCtrlBottomCCD pBottomCCD = null;
        private bool bLive = false;
        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucCtrlBottomCCD()
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
                bLive = false;
                if (pPmt != null)
                {
                    lblTitle.Text = clsLanguage.GetTranslation(pPmt[clsPmtBottomCCD.enuPmtName.SensorName], false);
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
                if (pBottomCCD != null)
                {
                    //if (pBottomCCD.eErrorCode == clsCtrlBottomCCD.enuErrorCode.None)
                    //{
                    //    txt_CCDValue.Text = pBottomCCD.LastHeightValue.ToString("F3");
                    //}
                    //else
                    //{
                    //    txt_CCDValue.Text = pBottomCCD.LastHeightValue.ToString("F3") + "(" + pBottomCCD.eErrorCode.ToString() + ")";
                    //}
                    //if (bLive == true)
                    //{
                    //    btnBottomCCD_Live.BackColor = Color.Lime;
                    //    pBottomCCD._GetHeightValue();
                    //}
                    //else
                    //{
                    //    btnBottomCCD_Live.BackColor = SystemColors.Control;
                    //}
                    //btnBottomCCD_Connect.BackColor = pBottomCCD._IsConnected() ? Color.Lime : SystemColors.Control;
                    //btnBottomCCD_Bypass.BackgroundImage = pBottomCCD._GetValue(clsCtrlBottomCCD.enuParameterName.ByPass) == 1 ? global::ArtSystem.Properties.Resources.Yes : global::ArtSystem.Properties.Resources.No;
                    //btnBottomCCD_Simulate.BackgroundImage = pBottomCCD._GetValue(clsCtrlBottomCCD.enuParameterName.Simulate) == 1 ? global::ArtSystem.Properties.Resources.Yes : global::ArtSystem.Properties.Resources.No;
                    //txtBottomCCD_Delay_ms.Text = pBottomCCD._GetValue(clsCtrlBottomCCD.enuParameterName.DelayTime_ms).ToString("");
                    //txtBottomCCD_Timeout_ms.Text = pBottomCCD._GetValue(clsCtrlBottomCCD.enuParameterName.TimeOut_ms).ToString("");
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

        private void ucCtrlBottomCCD_VisibleChanged(object sender, EventArgs e)
        {
            this.SetReflashTimerStart(this.Visible);
        }
        private void btnBottomCCD_Read_Click(object sender, EventArgs e)
        {
            if (pBottomCCD != null)
            {
                pBottomCCD._SingleGrab();
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pBottomCCD.sName + " -> " + ((Control)sender).Name + ", Clicked");
            }
        }
        private void btnBottomCCD_Live_Click(object sender, EventArgs e)
        {
            if (pBottomCCD != null)
            {
                bLive = !bLive;
                pBottomCCD._SingleGrab();
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pBottomCCD.sName + " -> " + ((Control)sender).Name + ", Clicked");
            }
        }
        private void btnBottomCCD_ResetZero_Click(object sender, EventArgs e)
        {
            if (pBottomCCD != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pBottomCCD.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pBottomCCD._ResetZero();
            }
        }
        private void btnBottomCCD_ClearOffset_Click(object sender, EventArgs e)
        {
            if (pBottomCCD != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pBottomCCD.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pBottomCCD._ClearOffset();
            }
        }
        private void btnBottomCCD_Connect_Click(object sender, EventArgs e)
        {
            if (pBottomCCD != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pBottomCCD.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pBottomCCD._Connect();
            }
        }
        private void btnBottomCCD_Disconnect_Click(object sender, EventArgs e)
        {
            if (pBottomCCD != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pBottomCCD.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pBottomCCD._Disconnect();
            }
        }

        private void btnBottomCCD_Bypass_Click(object sender, EventArgs e)
        {
            if (pBottomCCD != null)
            {
                double PreviousValue = pBottomCCD._GetValue(clsCtrlBottomCCD.enuParameterName.ByPass);
                if (PreviousValue == 1)
                {
                    pBottomCCD._SaveValue(clsCtrlBottomCCD.enuParameterName.ByPass, 0);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pBottomCCD.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 1 -> 0");
                }
                else
                {
                    pBottomCCD._SaveValue(clsCtrlBottomCCD.enuParameterName.ByPass, 1);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pBottomCCD.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 0 -> 1");
                }
            }
        }
        private void btnBottomCCD_Simulate_Click(object sender, EventArgs e)
        {
            if (pBottomCCD != null)
            {
                double PreviousValue = pBottomCCD._GetValue(clsCtrlBottomCCD.enuParameterName.Simulate);
                if (PreviousValue == 1)
                {
                    pBottomCCD._SaveValue(clsCtrlBottomCCD.enuParameterName.Simulate, 0);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pBottomCCD.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 1 -> 0");
                }
                else
                {
                    pBottomCCD._SaveValue(clsCtrlBottomCCD.enuParameterName.Simulate, 1);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pBottomCCD.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 0 -> 1");
                }
            }
        }
        private void txtBottomCCD_Delay_ms_Click(object sender, EventArgs e)
        {
            if (pBottomCCD != null)
            {
                double PreviousValue = pBottomCCD._GetValue(clsCtrlBottomCCD.enuParameterName.DelayTime_ms);
                if (FormNumBox.GetSingleton().ShowDialog(this, PreviousValue.ToString(), 99999999, 0, 0) == DialogResult.OK)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pBottomCCD.sName + " -> " + ((Control)sender).Name
                        + ", Clicked, Value : " + PreviousValue + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                    pBottomCCD._SaveValue(clsCtrlBottomCCD.enuParameterName.DelayTime_ms, FormNumBox.GetSingleton().NumBoxValue);
                }
            }
        }
        private void txtBottomCCD_Timeout_ms_Click(object sender, EventArgs e)
        {
            if (pBottomCCD != null)
            {
                double PreviousValue = pBottomCCD._GetValue(clsCtrlBottomCCD.enuParameterName.TimeOut_ms);
                if (FormNumBox.GetSingleton().ShowDialog(this, PreviousValue.ToString(), 99999999, 0, 0) == DialogResult.OK)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pBottomCCD.sName + " -> " + ((Control)sender).Name
                        + ", Clicked, Value : " + PreviousValue + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                    pBottomCCD._SaveValue(clsCtrlBottomCCD.enuParameterName.TimeOut_ms, FormNumBox.GetSingleton().NumBoxValue);
                }
            }
        }




        #endregion

        private void btnSingleGrab_Click(object sender, EventArgs e)
        {
            if (pBottomCCD != null)
            {
                pBottomCCD._SingleGrab();
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pBottomCCD.sName + " -> " + ((Control)sender).Name + ", Clicked");
            }
        }
    }
}

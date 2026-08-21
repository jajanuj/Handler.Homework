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
    public partial class ucCtrlHighSensor : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public Dictionary<clsPmtHighSensor.enuPmtName, string> pPmt = null;
        public clsCtrlHighSensor pHighSensor = null;
        private bool bLive = false;
        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucCtrlHighSensor()
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
                    labName.Text = clsLanguage.GetTranslation(pPmt[clsPmtHighSensor.enuPmtName.SensorName], false);
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
                if (pHighSensor != null)
                {
                    if (pHighSensor.eErrorCode == clsCtrlHighSensor.enuErrorCode.None)
                    {
                        txt_LaserValue.Text = pHighSensor.LastHeightValue.ToString("F3");
                    }
                    else
                    {
                        txt_LaserValue.Text = pHighSensor.LastHeightValue.ToString("F3") + "(" + pHighSensor.eErrorCode.ToString() + ")";
                    }
                    if (bLive == true)
                    {
                        btnHighSensor_Live.BackColor = Color.Lime;
                        pHighSensor._GetHeightValue();
                    }
                    else
                    {
                        btnHighSensor_Live.BackColor = SystemColors.Control;
                    }
                    btnHighSensor_Connect.BackColor = pHighSensor._IsConnected() ? Color.Lime : SystemColors.Control;
                    btnHighSensor_Bypass.BackgroundImage = pHighSensor._GetValue(clsCtrlHighSensor.enuParameterName.ByPass) == 1 ? global::ArtSystem.Properties.Resources.Yes : global::ArtSystem.Properties.Resources.No;
                    btnHighSensor_Simulate.BackgroundImage = pHighSensor._GetValue(clsCtrlHighSensor.enuParameterName.Simulate) == 1 ? global::ArtSystem.Properties.Resources.Yes : global::ArtSystem.Properties.Resources.No;
                    txtHighSensor_Delay_ms.Text = pHighSensor._GetValue(clsCtrlHighSensor.enuParameterName.DelayTime_ms).ToString("");
                    txtHighSensor_Timeout_ms.Text = pHighSensor._GetValue(clsCtrlHighSensor.enuParameterName.TimeOut_ms).ToString("");
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

        private void ucCtrlHighSensor_VisibleChanged(object sender, EventArgs e)
        {
            this.SetReflashTimerStart(this.Visible);
        }
        private void btnHighSensor_Read_Click(object sender, EventArgs e)
        {
            if (pHighSensor != null)
            {
                pHighSensor._GetHeightValue();
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pHighSensor.sName + " -> " + ((Control)sender).Name + ", Clicked");
            }
        }
        private void btnHighSensor_Live_Click(object sender, EventArgs e)
        {
            if (pHighSensor != null)
            {
                bLive = !bLive;
                pHighSensor._GetHeightValue();
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pHighSensor.sName + " -> " + ((Control)sender).Name + ", Clicked");
            }
        }
        private void btnHighSensor_ResetZero_Click(object sender, EventArgs e)
        {
            if (pHighSensor != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pHighSensor.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pHighSensor._ResetZero();
            }
        }
        private void btnHighSensor_ClearOffset_Click(object sender, EventArgs e)
        {
            if (pHighSensor != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pHighSensor.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pHighSensor._ClearOffset();
            }
        }
        private void btnHighSensor_Connect_Click(object sender, EventArgs e)
        {
            if (pHighSensor != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pHighSensor.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pHighSensor._Connect();
            }
        }
        private void btnHighSensor_Disconnect_Click(object sender, EventArgs e)
        {
            if (pHighSensor != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pHighSensor.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pHighSensor._Disconnect();
            }
        }

        private void btnHighSensor_Bypass_Click(object sender, EventArgs e)
        {
            if (pHighSensor != null)
            {
                double PreviousValue = pHighSensor._GetValue(clsCtrlHighSensor.enuParameterName.ByPass);
                if (PreviousValue == 1)
                {
                    pHighSensor._SaveValue(clsCtrlHighSensor.enuParameterName.ByPass, 0);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pHighSensor.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 1 -> 0");
                }
                else
                {
                    pHighSensor._SaveValue(clsCtrlHighSensor.enuParameterName.ByPass, 1);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pHighSensor.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 0 -> 1");
                }
            }
        }
        private void btnHighSensor_Simulate_Click(object sender, EventArgs e)
        {
            if (pHighSensor != null)
            {
                double PreviousValue = pHighSensor._GetValue(clsCtrlHighSensor.enuParameterName.Simulate);
                if (PreviousValue == 1)
                {
                    pHighSensor._SaveValue(clsCtrlHighSensor.enuParameterName.Simulate, 0);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pHighSensor.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 1 -> 0");
                }
                else
                {
                    pHighSensor._SaveValue(clsCtrlHighSensor.enuParameterName.Simulate, 1);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pHighSensor.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 0 -> 1");
                }
            }
        }
        private void txtHighSensor_Delay_ms_Click(object sender, EventArgs e)
        {
            if (pHighSensor != null)
            {
                double PreviousValue = pHighSensor._GetValue(clsCtrlHighSensor.enuParameterName.DelayTime_ms);
                if (FormNumBox.GetSingleton().ShowDialog(this, PreviousValue.ToString(), 99999999, 0, 0) == DialogResult.OK)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pHighSensor.sName + " -> " + ((Control)sender).Name
                        + ", Clicked, Value : " + PreviousValue + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                    pHighSensor._SaveValue(clsCtrlHighSensor.enuParameterName.DelayTime_ms, FormNumBox.GetSingleton().NumBoxValue);
                }
            }
        }
        private void txtHighSensor_Timeout_ms_Click(object sender, EventArgs e)
        {
            if (pHighSensor != null)
            {
                double PreviousValue = pHighSensor._GetValue(clsCtrlHighSensor.enuParameterName.TimeOut_ms);
                if (FormNumBox.GetSingleton().ShowDialog(this, PreviousValue.ToString(), 99999999, 0, 0) == DialogResult.OK)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pHighSensor.sName + " -> " + ((Control)sender).Name
                        + ", Clicked, Value : " + PreviousValue + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                    pHighSensor._SaveValue(clsCtrlHighSensor.enuParameterName.TimeOut_ms, FormNumBox.GetSingleton().NumBoxValue);
                }
            }
        }

        #endregion




    }
}

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
    public partial class ucCtrlReader : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public Dictionary<clsPmtReader.enuPmtName, string> pPmt = null;
        public clsCtrlReader pReader = null;
        private bool bLive = false;
        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucCtrlReader()
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
                    labName.Text = clsLanguage.GetTranslation(pPmt[clsPmtReader.enuPmtName.ReaderName], false);
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
                if (pReader != null)
                {
                    labName.BackColor = pReader._IsConnected() ? Color.Lime : SystemColors.Control;
                    btnReader_Read.BackColor = pReader._ReadAck() == true ? Color.Lime : pReader._ReadAck() == null ? SystemColors.Control : Color.Red;
                    txt_ReciveData.Text = pReader._ReadResult();
                    btnReader_Bypass.BackgroundImage = pReader._GetValue(clsCtrlReader.enuParameterName.ByPass) == 1 ? global::ArtSystem.Properties.Resources.Yes : global::ArtSystem.Properties.Resources.No;
                    btnReader_Simulate.BackgroundImage = pReader._GetValue(clsCtrlReader.enuParameterName.Simulate) == 1 ? global::ArtSystem.Properties.Resources.Yes : global::ArtSystem.Properties.Resources.No;
                    txtReader_Delay_ms.Text = pReader._GetValue(clsCtrlReader.enuParameterName.DelayTime_ms).ToString("");
                    txtReader_Timeout_ms.Text = pReader._GetValue(clsCtrlReader.enuParameterName.TimeOut_ms).ToString("");
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

        private decimal SetNumValue(comNumBox pControl,decimal Value)
        {
            decimal rValue = Value;
            if (Value > pControl._Maximum)
            {
                Value = pControl._Maximum;
            }
            if(Value < pControl._Minimum)
            {
                Value = pControl._Minimum;
            }
            pControl._Value = Value;
            rValue = pControl._Value;
            return rValue;
        }

        #endregion

        #region//===================== 以下為事件處理 () =====================

        private void ucCtrlReader_VisibleChanged(object sender, EventArgs e)
        {
            this.SetReflashTimerStart(this.Visible);
        }
        private void btnReader_Read_Click(object sender, EventArgs e)
        {
            if (pReader != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pReader.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pReader._SendRead();
            }
        }
        private void btnReader_Stop_Click(object sender, EventArgs e)
        {
            if (pReader != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pReader.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pReader._SendStop();
            }
        }
        private void btnReader_Reset_Click(object sender, EventArgs e)
        {
            if (pReader != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pReader.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pReader._SendReset();
            }
        }
        private void btnReader_Connect_Click(object sender, EventArgs e)
        {
            if (pReader != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pReader.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pReader._Connect();
            }
        }
        private void btnReader_Disconnect_Click(object sender, EventArgs e)
        {
            if (pReader != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pReader.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pReader._Disconnect();
            }
        }

        private void btnReader_Bypass_Click(object sender, EventArgs e)
        {
            if (pReader != null)
            {
                double PreviousValue = pReader._GetValue(clsCtrlReader.enuParameterName.ByPass);
                if (PreviousValue == 1)
                {
                    pReader._SaveValue(clsCtrlReader.enuParameterName.ByPass, 0);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pReader.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 1 -> 0");
                }
                else
                {
                    pReader._SaveValue(clsCtrlReader.enuParameterName.ByPass, 1);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pReader.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 0 -> 1");
                }
            }
        }
        private void btnReader_Simulate_Click(object sender, EventArgs e)
        {
            if (pReader != null)
            {
                double PreviousValue = pReader._GetValue(clsCtrlReader.enuParameterName.Simulate);
                if (PreviousValue == 1)
                {
                    pReader._SaveValue(clsCtrlReader.enuParameterName.Simulate, 0);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pReader.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 1 -> 0");
                }
                else
                {
                    pReader._SaveValue(clsCtrlReader.enuParameterName.Simulate, 1);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pReader.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 0 -> 1");
                }
            }
        }
        private void txtReader_Delay_ms_Click(object sender, EventArgs e)
        {
            if (pReader != null)
            {
                double PreviousValue = pReader._GetValue(clsCtrlReader.enuParameterName.DelayTime_ms);
                if (FormNumBox.GetSingleton().ShowDialog(this, PreviousValue.ToString(), 99999999, 0, 0) == DialogResult.OK)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pReader.sName + " -> " + ((Control)sender).Name
                        + ", Clicked, Value : " + PreviousValue + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                    pReader._SaveValue(clsCtrlReader.enuParameterName.DelayTime_ms, FormNumBox.GetSingleton().NumBoxValue);
                }
            }

        }
        private void txtReader_Timeout_ms_Click(object sender, EventArgs e)
        {
            if (pReader != null)
            {
                double PreviousValue = pReader._GetValue(clsCtrlReader.enuParameterName.TimeOut_ms);
                if (FormNumBox.GetSingleton().ShowDialog(this, PreviousValue.ToString(), 99999999, 0, 0) == DialogResult.OK)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pReader.sName + " -> " + ((Control)sender).Name
                        + ", Clicked, Value : " + PreviousValue + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                    pReader._SaveValue(clsCtrlReader.enuParameterName.TimeOut_ms, FormNumBox.GetSingleton().NumBoxValue);
                }
            }
        }

        #endregion
    }
}

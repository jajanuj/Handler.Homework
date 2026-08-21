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
    public partial class ucCtrlTCPLink : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public Dictionary<clsPmtTCPLink.enuPmtName, string> pPmt = null;
        public clsCtrlTCPLink pTCPLink = null;
        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucCtrlTCPLink()
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
                    labName.Text = clsLanguage.GetTranslation(pPmt[clsPmtTCPLink.enuPmtName.Name], false);
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
                if (pTCPLink != null)
                {
                    labName.BackColor = pTCPLink._IsConnected() ? Color.Lime : SystemColors.Control;
                    txt_ReciveData.Text = pTCPLink._ArrivalMessageLatest;
                    txtTCPIP.Text = pPmt[clsPmtTCPLink.enuPmtName.TCP_IP];
                    txtTCPPort.Text = pPmt[clsPmtTCPLink.enuPmtName.TCP_Port];
                    labStation.Text = pTCPLink._bIsServer ? clsLanguage.GetTranslation("Server") : clsLanguage.GetTranslation("Client");
                    btnTCPLink_Bypass.BackgroundImage = pTCPLink._GetValue(clsCtrlTCPLink.enuParameterName.ByPass) == 1 ? global::ArtSystem.Properties.Resources.Yes : global::ArtSystem.Properties.Resources.No;
                    btnTCPLink_Simulate.BackgroundImage = pTCPLink._GetValue(clsCtrlTCPLink.enuParameterName.Simulate) == 1 ? global::ArtSystem.Properties.Resources.Yes : global::ArtSystem.Properties.Resources.No;
                    txtTCPLink_Delay_ms.Text = pTCPLink._GetValue(clsCtrlTCPLink.enuParameterName.DelayTime_ms).ToString("");
                    txtTCPLink_Timeout_ms.Text = pTCPLink._GetValue(clsCtrlTCPLink.enuParameterName.TimeOut_ms).ToString("");
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

        private void ucCtrlTCPLink_VisibleChanged(object sender, EventArgs e)
        {
            this.SetReflashTimerStart(this.Visible);
        }

        private void btnTCPLink_Clear_Click(object sender, EventArgs e)
        {
            if (pTCPLink != null)
            {
                pTCPLink._ClearLastArrivedMessage();
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pTCPLink.sName + " -> " + ((Control)sender).Name + ", Clicked");
            }
        }
        private void btnTCPLink_Send_Click(object sender, EventArgs e)
        {
            if (pTCPLink != null)
            {
                if (txtSendData.Text != null && txtSendData.Text.Length > 0)
                {
                    pTCPLink._Send(txtSendData.Text);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pTCPLink.sName + " -> " + ((Control)sender).Name + ", Clicked, SendData : " + txtSendData.Text);
                }
            }
        }
        private void btnTCPLink_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                if (pTCPLink != null)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pTCPLink.sName + " -> " + ((Control)sender).Name + ", Clicked");
                    pTCPLink._Connect(txtTCPIP.Text, Convert.ToInt32(txtTCPPort.Text));
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void btnTCPLink_Disconnect_Click(object sender, EventArgs e)
        {
            if (pTCPLink != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pTCPLink.sName + " -> " + ((Control)sender).Name + ", Clicked");
                pTCPLink._Disconnect();
            }
        }


        private void txtTCPIP_Click(object sender, EventArgs e)
        {
            try
            {
                if (pPmt != null)
                {
                    if (pPmt.ContainsKey(clsPmtTCPLink.enuPmtName.TCP_IP) == true)
                    {
                        if (formKeyBox.GetSingleton().ShowDialog(this, pPmt[clsPmtTCPLink.enuPmtName.TCP_IP], false) == DialogResult.OK)
                        {
                            string sPath = ucTCPLinkSetting.GetSingleton().mPmt.sINIPath;
                            if (System.IO.File.Exists(sPath) == true)
                            {
                                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pTCPLink.sName + " -> " + ((Control)sender).Name
                                       + ", TCPLink Name : " + pPmt[clsPmtTCPLink.enuPmtName.Name]
                                       + ", Change IP : " + pPmt[clsPmtTCPLink.enuPmtName.TCP_IP] + "-> " + formKeyBox.GetSingleton().KeyBoxValue);
                                pPmt[clsPmtTCPLink.enuPmtName.TCP_IP] = formKeyBox.GetSingleton().KeyBoxValue.ToString();
                                clsIniFile mFile = new clsIniFile(sPath);
                                mFile.WriteValue(pPmt[clsPmtTCPLink.enuPmtName.Name], clsPmtTCPLink.enuPmtName.TCP_IP.ToString(), pPmt[clsPmtTCPLink.enuPmtName.TCP_IP]);
                                txtTCPIP.Text = pPmt[clsPmtTCPLink.enuPmtName.TCP_IP];
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void txtTCPPort_Click(object sender, EventArgs e)
        {

            try
            {
                if (pPmt != null)
                {
                    if (pPmt.ContainsKey(clsPmtTCPLink.enuPmtName.TCP_Port) == true)
                    {
                        if (FormNumBox.GetSingleton().ShowDialog(this, pPmt[clsPmtTCPLink.enuPmtName.TCP_Port], 99999999, 0, 0) == DialogResult.OK)
                        {
                            string sPath = ucTCPLinkSetting.GetSingleton().mPmt.sINIPath;
                            if (System.IO.File.Exists(sPath) == true)
                            {
                                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pTCPLink.sName + " -> " + ((Control)sender).Name
                                       + ", TCPLink Name : " + pPmt[clsPmtTCPLink.enuPmtName.Name]
                                       + ", Change IP : " + pPmt[clsPmtTCPLink.enuPmtName.TCP_Port] + "-> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                                pPmt[clsPmtTCPLink.enuPmtName.TCP_Port] = FormNumBox.GetSingleton().NumBoxValue.ToString();
                                clsIniFile mFile = new clsIniFile(sPath);
                                mFile.WriteValue(pPmt[clsPmtTCPLink.enuPmtName.Name], clsPmtTCPLink.enuPmtName.TCP_Port.ToString(), pPmt[clsPmtTCPLink.enuPmtName.TCP_Port]);
                                txtTCPPort.Text = pPmt[clsPmtTCPLink.enuPmtName.TCP_Port];
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void btnTCPLink_Bypass_Click(object sender, EventArgs e)
        {
            if (pTCPLink != null)
            {
                double PreviousValue = pTCPLink._GetValue(clsCtrlTCPLink.enuParameterName.ByPass);
                if (PreviousValue == 1)
                {
                    pTCPLink._SaveValue(clsCtrlTCPLink.enuParameterName.ByPass, 0);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pTCPLink.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 1 -> 0");
                }
                else
                {
                    pTCPLink._SaveValue(clsCtrlTCPLink.enuParameterName.ByPass, 1);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pTCPLink.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 0 -> 1");
                }
            }
        }
        private void btnTCPLink_Simulate_Click(object sender, EventArgs e)
        {
            if (pTCPLink != null)
            {
                double PreviousValue = pTCPLink._GetValue(clsCtrlTCPLink.enuParameterName.Simulate);
                if (PreviousValue == 1)
                {
                    pTCPLink._SaveValue(clsCtrlTCPLink.enuParameterName.Simulate, 0);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pTCPLink.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 1 -> 0");
                }
                else
                {
                    pTCPLink._SaveValue(clsCtrlTCPLink.enuParameterName.Simulate, 1);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pTCPLink.sName + " -> " + ((Control)sender).Name + ", Clicked, Value : 0 -> 1");
                }
            }
        }
        private void txtTCP_Timout_ms_Click(object sender, EventArgs e)
        {
            if (pTCPLink != null)
            {
                double PreviousValue = pTCPLink._GetValue(clsCtrlTCPLink.enuParameterName.TimeOut_ms);
                if (FormNumBox.GetSingleton().ShowDialog(this, PreviousValue.ToString(), 99999999, 0, 0) == DialogResult.OK)
                {
                    string sPath = ucTCPLinkSetting.GetSingleton().mPmt.sINIPath;
                    if (System.IO.File.Exists(sPath) == true)
                    {
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pTCPLink.sName + " -> " + ((Control)sender).Name
                            + ", Clicked, Value : " + PreviousValue + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                        pPmt[clsPmtTCPLink.enuPmtName.TimeOut_ms] = FormNumBox.GetSingleton().NumBoxValue.ToString();
                        pTCPLink._SaveValue(clsCtrlTCPLink.enuParameterName.TimeOut_ms, FormNumBox.GetSingleton().NumBoxValue);
                    }
                }
            }
        }
        private void txtTCP_Delay_ms_Click(object sender, EventArgs e)
        {
            if (pTCPLink != null)
            {
                double PreviousValue = pTCPLink._GetValue(clsCtrlTCPLink.enuParameterName.DelayTime_ms);
                if (FormNumBox.GetSingleton().ShowDialog(this, PreviousValue.ToString(), 99999999, 0, 0) == DialogResult.OK)
                {
                    string sPath = ucTCPLinkSetting.GetSingleton().mPmt.sINIPath;
                    if (System.IO.File.Exists(sPath) == true)
                    {
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pTCPLink.sName + " -> " + ((Control)sender).Name
                            + ", Clicked, Value : " + PreviousValue + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                        pPmt[clsPmtTCPLink.enuPmtName.DelayTime_ms] = FormNumBox.GetSingleton().NumBoxValue.ToString();
                        pTCPLink._SaveValue(clsCtrlTCPLink.enuParameterName.DelayTime_ms, FormNumBox.GetSingleton().NumBoxValue);
                    }
                }
            }
        }

        #endregion

    }
}

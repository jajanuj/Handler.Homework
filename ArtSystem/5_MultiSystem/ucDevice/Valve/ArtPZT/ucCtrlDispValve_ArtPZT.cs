using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using System.Threading;
using System.IO;

namespace ArtSystem.MultiSystem
{
    public partial class ucCtrlDispValve_ArtPZT : UserControl
    {
        #region //========== 參數 =========

        public clsCtrlDispValve_ArtPZT g_CtrlDispValve = null;
        #endregion

        #region //========== 變數 =========
        /// <summary> 讓外部填寫目前壓力值顯示在UI上 </summary>
        public double CurrentValvePressure = 0;
        public bool bReadingData = false;
        #endregion

        #region //========== 必要函式 ==========
        public ucCtrlDispValve_ArtPZT()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            ucArtMain_Design.GetSingleton()._CollectChangeLanguageControls(this);
        }

        public void UpdateControls()
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    gb_ValveAction.Text = clsLanguage.GetTranslation("Valve Action", false) + " (" + g_CtrlDispValve.g_sName + ")";
                    DataToUI();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void ReflashFunc()
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    if (g_CtrlDispValve.g_PmtFileLastEdit != System.IO.File.GetLastWriteTime(g_CtrlDispValve.g_sCurrentPmtPath))
                    {
                        g_CtrlDispValve.LoadPmt(g_CtrlDispValve.g_sCurrentPmtPath);
                        DataToUI();
                    }
                    clsPmtArtPZT mPmt = (clsPmtArtPZT)g_CtrlDispValve.g_PmtValue;
                    if (g_CtrlDispValve.g_bLoadFileSuccess == false)
                    {
                        tbx_FilePath.Text = "[Load File Error] : " + g_CtrlDispValve.g_sCurrentPmtPath;
                    }
                    else
                    {
                        tbx_FilePath.Text = g_CtrlDispValve.g_sCurrentPmtPath;
                    }
                    lb_PortStatus.Text = g_CtrlDispValve.g_ValveModbus.ComPort_IsOpen() == false ? clsLanguage.GetTranslation("Disconnect", false)
                        : g_CtrlDispValve.g_ValveModbus.ComPort_IsCommunicationError() ? clsLanguage.GetTranslation("Connect Error", false) : clsLanguage.GetTranslation("Connected", false);

                    #region//參數變更閃爍
                    ControlValueChangeFlashColor(nud_Open_Volt, nud_Open_Volt.Value != (decimal)mPmt.Open_Volt);
                    ControlValueChangeFlashColor(nud_Lock_Volt, nud_Lock_Volt.Value != (decimal)mPmt.Lock_Volt);
                    ControlValueChangeFlashColor(nud_Hold_Open_Time, nud_Hold_Open_Time.Value != (decimal)mPmt.Hold_Open_Time);
                    ControlValueChangeFlashColor(nud_Hold_Lock_Time, nud_Hold_Lock_Time.Value != (decimal)mPmt.Hold_Lock_Time);
                    ControlValueChangeFlashColor(nud_Lock_Time, nud_Lock_Time.Value != (decimal)mPmt.Lock_Time);
                    ControlValueChangeFlashColor(nud_Open_Time, nud_Open_Time.Value != (decimal)mPmt.Open_Time);
                    #endregion

                    tbx_Pressure.Text = CurrentValvePressure.ToString("F0");
                    #region//通訊讀取
                    if (g_CtrlDispValve.g_ValveModbus.ComPort_IsOpen() == true
                        && g_CtrlDispValve.g_ValveModbus.ComPort_IsCommunicationError() == false)
                    {
                        var Status = g_CtrlDispValve.GetValveStatus();
                        lb_ValveStatus.Text = clsLanguage.GetTranslation(Status.ToString(), false);
                        if (g_CtrlDispValve.g_ValveModbus.ComPort_IsCommunicationError() == false)
                        {
                            tbx_PztTemp.Text = (g_CtrlDispValve.GetModbusValue((int)clsCtrlDispValve_ArtPZT.enuModbusAddress.Counter_Temp) / 10).ToString("F1"); // _mPzt_Allring().Get_CounterTemp().ToString();
                            tbx_CavityTemp.Text = (g_CtrlDispValve.GetModbusValue((int)clsCtrlDispValve_ArtPZT.enuModbusAddress.Cavity_Temp) / 10).ToString("F1"); //_mPzt_Allring().Get_CavityTemp().ToString();
                            tbx_PztCounter.Text = g_CtrlDispValve.GetValveCounter().ToString();

                            tbx_LockValue.Text = (g_CtrlDispValve.GetModbusValue((int)clsCtrlDispValve_ArtPZT.enuModbusAddress.PZT_Lock_Position_um) / 10).ToString("F1");// (Convert.ToDouble(_mPzt_Allring().Get_Lock_Position_um()) / 10).ToString();

                            if ((int)Status == 20)//!!
                            {
                                btn_ValveLock.Image = ArtSystem.Properties.Resources.Metroid_0011_unLOCK_48px_532337_easyicon_net;
                            }
                            else
                            {
                                btn_ValveLock.Image = ArtSystem.Properties.Resources.Metroid_0011_LOCK_48px_532337_easyicon_net;
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //========== Private Function ==========

        private void ControlValueChangeFlashColor(object sender, bool IsValueChange)
        {
            Control Item = (Control)sender;
            if (IsValueChange)
            {
                Item.BackColor = DateTime.Now.Millisecond > 500 ? Color.Yellow : Color.White;
            }
            else
            {
                Item.BackColor = Color.White;
            }

        }

        private void DataToUI()
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    clsPmtArtPZT mPmt = (clsPmtArtPZT)g_CtrlDispValve.g_PmtValue;
                    nud_Open_Volt.Value = mPmt.Open_Volt;//Low
                    nud_Lock_Volt.Value = mPmt.Lock_Volt;//High
                    nud_Hold_Open_Time.Value = mPmt.Hold_Open_Time;
                    nud_Hold_Lock_Time.Value = mPmt.Hold_Lock_Time;
                    nud_Lock_Time.Value = mPmt.Lock_Time;
                    nud_Open_Time.Value = mPmt.Open_Time;
                    tbx_PztPeriod.Text = g_CtrlDispValve.GetCycleTime_ms().ToString("F1");
                    tbx_PztFrequency.Text = (1000.0 / g_CtrlDispValve.GetCycleTime_ms()).ToString("F");
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void UIToData()
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    clsPmtArtPZT mPmt = (clsPmtArtPZT)g_CtrlDispValve.g_PmtValue;
                    mPmt.Open_Volt = (ushort)nud_Open_Volt.Value;//Low
                    mPmt.Lock_Volt = (ushort)nud_Lock_Volt.Value;//High
                    mPmt.Hold_Open_Time = (ushort)nud_Hold_Open_Time.Value;
                    mPmt.Hold_Lock_Time = (ushort)nud_Hold_Lock_Time.Value;
                    mPmt.Lock_Time = (ushort)nud_Lock_Time.Value;
                    mPmt.Open_Time = (ushort)nud_Open_Time.Value;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //========== Event ==========
        private void btn_ResetValve_Click(object sender, EventArgs e)
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    //_mPzt_Allring().Set_CtrlCommand(clsPzt_Allring.enuPZTCommand.SoftReset);
                    g_CtrlDispValve.SetCommand(clsCtrlDispValve_ArtPZT.enuCommand.SoftReset);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 顯示提示 </summary>
        private void ToolTip_On(object sender, EventArgs e)
        {
            try
            {
                Button Item = (Button)sender;
                if (sender == btn_ValveLock)
                {
                    toolTip1.SetToolTip(Item, clsLanguage.GetTranslation("Valve Lock On/Off", false));
                }
                else if (sender == btn_LockAdjust)
                {
                    toolTip1.SetToolTip(Item, clsLanguage.GetTranslation("Lock Adjust", false));
                }
                else if (sender == btn_GlueValve)
                {
                    toolTip1.SetToolTip(Item, clsLanguage.GetTranslation("Start Glue", false));
                }
                else if (sender == btn_GlueStop)
                {
                    toolTip1.SetToolTip(Item, clsLanguage.GetTranslation("Stop Glue", false));
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 關閉提示 </summary>
        private void ToolTip_Off(object sender, EventArgs e)
        {
            try
            {
                toolTip1.RemoveAll();
                toolTip1.Hide(this);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }


        #endregion

        /// <summary> 顯示Pzt進階功能視窗 </summary>
        private void btn_PZT_Help_Click(object sender, EventArgs e)
        {
            try
            {
                //PZT_Controller.Form1 Form_Pzt = new PZT_Controller.Form1();
                //Form_Pzt.PztCtrl.evtMBGetData += new PZT_UIControl_dll.PZT_Ctrl.eMBGetData(PztCtrl_evtMBGetData);
                //Form_Pzt.PztCtrl.evtMBSetData += new PZT_UIControl_dll.PZT_Ctrl.eMBSetData(PztCtrl_evtMBSetData);
                //Form_Pzt.Show();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void btn_GetDispMonitorData_Click(object sender, EventArgs e)//出膠監控相關
        {
            try
            {
                ////防止惡意連點
                //if (_mPzt_Allring().Get_PZTController_Status() != clsPzt_Allring.enuValveStatus.Ready
                //|| bReadingData == true)
                //{
                //    return;
                //}
                //bReadingData = true;
                //btn_GetDispMonitorData.Enabled = false;
                //btn_GetDispMonitorData.BackColor = Color.Green;
                //clsLog.Log(clsEnum.enuLogName.DispMonitorLog, "[btn_GetDispMonitorData Clisk]");
                //int ThersholdValue=0;
                //int Open_Ref = 0;
                //int Lock_Ref = 0;
                //int Distance = 0;
                //int Open_Mon = 0;
                //int Lock_Mon = 0;
                //PublicDeclare.mDispArm[clsEnum.enuDispArm.Arm1].mValve.GetPurgeData_RefanceData(ref ThersholdValue, ref Open_Ref, ref Lock_Ref);
                //PublicDeclare.mDispArm[clsEnum.enuDispArm.Arm1].mValve.GetPurgeData_MonitorData(ref Distance, ref Open_Mon, ref Lock_Mon);
                //clsLog.Log(clsEnum.enuLogName.DispMonitorLog, "[ThersholdValue]:" + ThersholdValue + ",[Open_Ref]:" + Open_Ref + ",[Lock_Ref]:" + Lock_Ref);
                //clsLog.Log(clsEnum.enuLogName.DispMonitorLog, "[Distance]:" + Distance + ",[Open_Mon]:" + Open_Mon + ",[Lock_Mon]:" + Lock_Mon);
                //PublicDeclare.mDispArm[clsEnum.enuDispArm.Arm1].mValve.GetPurgeData_RefanceCurve();
                //PublicDeclare.mDispArm[clsEnum.enuDispArm.Arm1].mValve.GetPurgeData_MonitorCurve();
                //bReadingData = false;
                //btn_GetDispMonitorData.BackColor = SystemColors.Control;
                //btn_GetDispMonitorData.Enabled = true;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }




        #region //========== Event (出膠,閉鎖,停止) ==========

        private void btn_ValveLock_Click(object sender, EventArgs e)
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    if (g_CtrlDispValve.GetValveStatus() == clsCtrlDispValve_ArtPZT.enuValveStatus.Ready)
                    {
                        g_CtrlDispValve.SetCommand(clsCtrlDispValve_ArtPZT.enuCommand.PZTLift);//釋放
                    }
                    else
                    {
                        g_CtrlDispValve.SetCommand(clsCtrlDispValve_ArtPZT.enuCommand.PZTLock);//上鎖(正常狀態)
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btn_LockAdjust_Click(object sender, EventArgs e)
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    g_CtrlDispValve.g_ucPztLockAdjust._ShowForm(g_CtrlDispValve);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btn_GlueValve_Click(object sender, EventArgs e)
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    UIToData();
                    g_CtrlDispValve.SetPmt();
                    g_CtrlDispValve.SetOutputValue((double)nud_DispTime.Value);
                    System.Threading.Thread.Sleep(50);
                    g_CtrlDispValve.SoftwareTrigger();
                }

            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btnGlueStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    g_CtrlDispValve.SetDo(clsCtrlDispValve.enuDo.Interrupt, true);
                    g_CtrlDispValve.SetCommand(clsCtrlDispValve_ArtPZT.enuCommand.EMS);
                    System.Threading.Thread.Sleep(50);
                    g_CtrlDispValve.SetCommand(clsCtrlDispValve_ArtPZT.enuCommand.PZTLock);
                    //_btnGlueStop_Click(sender, e);
                    //_mPzt_Allring().Set_CtrlCommand(clsPzt_Allring.enuPZTCommand.EMS);
                    //SpinWait.SpinUntil(() => false, 50);
                    ////_mPzt_Allring().Set_Switch_BarrelPressure(true);
                    ////SpinWait.SpinUntil(() => false, 50);
                    //_mPzt_Allring().Set_CtrlCommand(clsPzt_Allring.enuPZTCommand.PZTLock);
                }

            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //========== Event (參數 Load,Save) ==========

        /// <summary> 更換膠閥參數文件 </summary>
        private void btn_ChangePztFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    string sPath = g_CtrlDispValve.g_sCurrentPmtPath;
                    string sDirectory = System.IO.Path.GetDirectoryName(sPath);
                    OpenFileDialog mFileDiaglog = new OpenFileDialog();
                    mFileDiaglog.InitialDirectory = sDirectory;
                    mFileDiaglog.FileName = "Default"; // Default file name
                    mFileDiaglog.DefaultExt = ".ini"; // Default file extension
                    mFileDiaglog.Filter = "Text documents (.ini)|*.ini"; // Filter files by extension
                                                                         // Show save file dialog box
                    DialogResult mResult = mFileDiaglog.ShowDialog();
                    if (mResult == DialogResult.OK)
                    {
                        string sSelectPath = mFileDiaglog.FileName;
                        string sSelectDirectory = System.IO.Path.GetDirectoryName(sSelectPath);
                        if (sSelectDirectory == sDirectory)
                        {
                            g_CtrlDispValve.LoadPmt(sSelectPath);
                            UpdateControls();
                            ucArtMain_Design.GetSingleton()._ArtMainFunc(ucArtMain_Design.enuFunc.ParameterPathChange);
                        }
                        else
                        {
                            formMessageBox.Show(clsLanguage.GetTranslation("Select Directory Error!", false));
                        }
                    }
                }
                else
                {
                    formMessageBox.Show("Object Is Empty!");
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 載入膠閥參數文件  </summary>
        private void btn_LoadPztFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    string sPath = g_CtrlDispValve.g_sCurrentPmtPath;
                    g_CtrlDispValve.LoadPmt(sPath);
                    UpdateControls();
                }
                else
                {
                    formMessageBox.Show("Object Is Empty!");
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 儲存膠閥參數文件 </summary>
        private void btn_SavePztFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    string sPath = g_CtrlDispValve.g_sCurrentPmtPath;
                    UIToData();
                    g_CtrlDispValve.SavePmt(sPath);
                }
                else
                {
                    formMessageBox.Show("Object Is Empty!");
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 另存膠閥參數文件 </summary>
        private void btn_SaveAsPztFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    string sPath = g_CtrlDispValve.g_sCurrentPmtPath;
                    string sDirectory = System.IO.Path.GetDirectoryName(sPath);

                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.InitialDirectory = sDirectory;
                    dlg.FileName = "Default"; // Default file name
                    dlg.DefaultExt = ".ini"; // Default file extension
                    dlg.Filter = "Text documents (.ini)|*.ini"; // Filter files by extension
                                                                // Show save file dialog box
                    DialogResult result = dlg.ShowDialog();
                    // Process save file dialog box results
                    if (result == DialogResult.OK)
                    {
                        string sSelectPath = dlg.FileName;
                        string sSelectDirectory = System.IO.Path.GetDirectoryName(sSelectPath);
                        if (sSelectDirectory == sDirectory)
                        {
                            UIToData();
                            g_CtrlDispValve.SavePmt(sSelectPath);
                            ucArtMain_Design.GetSingleton()._ArtMainFunc(ucArtMain_Design.enuFunc.ParameterPathChange);
                        }
                        else
                        {
                            formMessageBox.Show(clsLanguage.GetTranslation("Select Directory Error!", false));
                        }
                    }
                }
                else
                {
                    formMessageBox.Show("Object Is Empty!");
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion


    }
}

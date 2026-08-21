using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtSystem;

namespace ArtSystem.MultiSystem
{
    public partial class ucCtrlDispValve_ArtSpray : UserControl
    {
        #region //========== 參數 =========
        public clsCtrlDispValve_ArtSpray g_CtrlDispValve = null;
        #endregion

        #region //========== 必要函式 ==========
        public ucCtrlDispValve_ArtSpray()
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
                    groupBox3.Text = clsLanguage.GetTranslation("Valve Action", false) + " (" + g_CtrlDispValve.g_sName + ")";
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
                    clsPmtArtSpray mPmt = (clsPmtArtSpray)g_CtrlDispValve.g_PmtValue;
                    if (g_CtrlDispValve.g_bLoadFileSuccess == false)
                    {
                        textBox1.Text = "[Load File Error] : " + g_CtrlDispValve.g_sCurrentPmtPath;
                    }
                    else
                    {
                        textBox1.Text = g_CtrlDispValve.g_sCurrentPmtPath;
                    }
                    txtPortStatus.Text = g_CtrlDispValve.g_ValveModbus.ComPort_IsOpen() == false ? clsLanguage.GetTranslation("Disconnect", false)
                        : g_CtrlDispValve.g_ValveModbus.ComPort_IsCommunicationError() ? clsLanguage.GetTranslation("Connect Error", false) : clsLanguage.GetTranslation("Connected", false);
                    #region//參數變更閃爍
                    ControlValueChangeFlashColor(nud_OpValvePreTime, nud_OpValvePreTime._Value != (decimal)mPmt.OpValvePreTime);
                    ControlValueChangeFlashColor(nud_OpLockTime, nud_OpLockTime._Value != (decimal)mPmt.OpLockTime);
                    ControlValueChangeFlashColor(nud_OpInsideAtmTime, nud_OpInsideAtmTime._Value != (decimal)mPmt.OpInsideAtmTime);
                    ControlValueChangeFlashColor(nud_OpOutsideAtmTime, nud_OpOutsideAtmTime._Value != (decimal)mPmt.OpOutsideAtmTime);
                    ControlValueChangeFlashColor(nud_ClValvePreTime, nud_ClValvePreTime._Value != (decimal)mPmt.ClValvePreTime);
                    ControlValueChangeFlashColor(nud_ClLockTime, nud_ClLockTime._Value != (decimal)mPmt.ClLockTime);
                    ControlValueChangeFlashColor(nud_ClInsideAtmTime, nud_ClInsideAtmTime._Value != (decimal)mPmt.ClInsideAtmTime);
                    ControlValueChangeFlashColor(nud_ClOutsideAtmTime, nud_ClOutsideAtmTime._Value != (decimal)mPmt.ClOutsideAtmTime);
                    //ControlValueChangeFlashColor(nud_DispTime, nud_DispTime._Value != (decimal)mPneumatic.mPneumaticData.PnuematicDispTime);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //========== Public Function ==========
        //public double GetUIOutputValue()
        //{
        //    return Convert.ToDouble(nud_DispTime._Value);
        //}
        //public clsPneumatic.clsPneumaticPmt GetUIPmtValue()
        //{
        //    clsPneumatic.clsPneumaticPmt mPmt = new clsPneumatic.clsPneumaticPmt();
        //    mPmt.OpValvePreTime = (ushort)nud_OpValvePreTime._Value;
        //    mPmt.OpLockTime = (ushort)nud_OpLockTime._Value;
        //    mPmt.OpInsideAtmTime = (ushort)nud_OpInsideAtmTime._Value;
        //    mPmt.OpOutsideAtmTime = (ushort)nud_OpOutsideAtmTime._Value;
        //    mPmt.ClValvePreTime = (ushort)nud_ClValvePreTime._Value;
        //    mPmt.ClLockTime = (ushort)nud_ClLockTime._Value;
        //    mPmt.ClInsideAtmTime = (ushort)nud_ClInsideAtmTime._Value;
        //    mPmt.ClOutsideAtmTime = (ushort)nud_ClOutsideAtmTime._Value;
        //    return mPmt;
        //}
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
                    clsPmtArtSpray mPmt = (clsPmtArtSpray)g_CtrlDispValve.g_PmtValue;
                    nud_OpValvePreTime._Value = (decimal)mPmt.OpValvePreTime;
                    nud_OpLockTime._Value = (decimal)mPmt.OpLockTime;
                    nud_OpInsideAtmTime._Value = (decimal)mPmt.OpInsideAtmTime;
                    nud_OpOutsideAtmTime._Value = (decimal)mPmt.OpOutsideAtmTime;
                    nud_ClValvePreTime._Value = (decimal)mPmt.ClValvePreTime;
                    nud_ClLockTime._Value = (decimal)mPmt.ClLockTime;
                    nud_ClInsideAtmTime._Value = (decimal)mPmt.ClInsideAtmTime;
                    nud_ClOutsideAtmTime._Value = (decimal)mPmt.ClOutsideAtmTime;
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
                    clsPmtArtSpray mPmt = (clsPmtArtSpray)g_CtrlDispValve.g_PmtValue;
                    mPmt.OpValvePreTime = (ushort)nud_OpValvePreTime._Value;
                    mPmt.OpLockTime = (ushort)nud_OpLockTime._Value;
                    mPmt.OpInsideAtmTime = (ushort)nud_OpInsideAtmTime._Value;
                    mPmt.OpOutsideAtmTime = (ushort)nud_OpOutsideAtmTime._Value;
                    mPmt.ClValvePreTime = (ushort)nud_ClValvePreTime._Value;
                    mPmt.ClLockTime = (ushort)nud_ClLockTime._Value;
                    mPmt.ClInsideAtmTime = (ushort)nud_ClInsideAtmTime._Value;
                    mPmt.ClOutsideAtmTime = (ushort)nud_ClOutsideAtmTime._Value;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //========== Event (出膠,停止) ==========

        private void btnGlueValve_Click(object sender, EventArgs e)
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    UIToData();
                    g_CtrlDispValve.SetPmt();
                    g_CtrlDispValve.SetOutputValue((double)nud_DispTime._Value);
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
                }

            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }


        #endregion

        
        #region //========== Event (參數 Load,Save) ==========
        /// <summary> 選取別的膠閥參數文件 </summary>
        private void btnChangePztParaFile_Click(object sender, EventArgs e)
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

        /// <summary> 重新載入膠閥參數文件  </summary>
        private void btnLoadPztParaFile_Click(object sender, EventArgs e)
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
        private void btnSavePztFile_Click(object sender, EventArgs e)
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
        private void btnSaveAsPztFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (g_CtrlDispValve != null)
                {
                    string sPath = g_CtrlDispValve.g_sCurrentPmtPath;
                    string sDirectory = System.IO.Path.GetDirectoryName(sPath);

                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.InitialDirectory = System.IO.Path.GetDirectoryName(sDirectory);
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

        #region //========== Event (開啟Tool視窗) ==========
        private void btnTools_Click(object sender, EventArgs e)
        {
            try
            {
                if (g_CtrlDispValve != null)
                {

                    Form mForm = new Form();
                    ArtValve.ucJetting AddItem = new ArtValve.ucJetting();
                    AddItem.SetExLink(new ArtValve.JettingControllerTester.eMBGetData(evtMBGetData),
                        new ArtValve.JettingControllerTester.eMBSetData(evtMBSetData), g_CtrlDispValve.g_ValveModbus.g_iStationID, clsArtSystem.bIsSoftwareSimulate);
                    mForm.Controls.Add(AddItem);
                    mForm.ClientSize = AddItem.Size;
                    mForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        public bool evtMBGetData(ushort addr, ushort count, ref short[] data)
        {
            bool rValue = false;
            try
            {
                if (g_CtrlDispValve != null)
                {
                    ushort[] udata = g_CtrlDispValve.g_ValveModbus.GetData(addr, count);
                    if (udata.Length == 0)
                    {
                        return false;
                    }
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (i < udata.Length)
                        {
                            data[i] = (short)udata[i];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }

            return rValue;
        }

        public bool evtMBSetData(ushort addr, ushort count, short[] data)
        {
            bool rValue = false;
            try
            {
                ushort[] udata = new ushort[count];
                for (int i = 0; i < count; i++)
                {
                    if (i < data.Length)
                    { udata[i] = (ushort)data[i]; }
                }
                return g_CtrlDispValve.g_ValveModbus.SetData(addr, count, udata);

            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        #endregion
    }
}

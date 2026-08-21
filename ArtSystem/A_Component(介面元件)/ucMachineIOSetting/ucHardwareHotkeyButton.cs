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
using ArtSystem.MultiSystem;

namespace ArtSystem
{
    public partial class ucHardwareHotkeyButton : ucBaseUserControl2
    {
        #region //===================== 變數設置 =====================
        /// <summary> 存檔參數 </summary>
        public List<clsMachineIOSetting> g_LstHotkey = new List<clsMachineIOSetting>();
        /// <summary> 存檔參數 </summary>
        public Dictionary<clsCmData.enuEqStatus, clsMachineHotkeyLEDManage> g_DecHotkeyLEDManage = new Dictionary<clsCmData.enuEqStatus, clsMachineHotkeyLEDManage>();



        private ucMachineButton m_SelectingHotkey = null;
        private clsMachineIOSetting m_SelectingHotkeySetting = null;
        private bool m_bEditing_Hotkey = false;
        private clsHiPerfTimer mTimer_HotkeyLED = new clsHiPerfTimer();
        private bool m_bFlashFlag = false;
        private Dictionary<clsEnum.enuDi, string> m_DicDIName = null;
        private Dictionary<clsEnum.enuDo, string> m_DicDOName = null;
        #endregion

        #region //===================== 必要函式設置 =====================

        static private object objLock = new object();
        static private ucHardwareHotkeyButton m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucHardwareHotkeyButton GetSingleton()
        {
            lock (objLock)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucHardwareHotkeyButton();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucHardwareHotkeyButton()
        {
            InitializeComponent();
            if (ArtSystem.clsArtSystem.bIsProgramOpen == false)
            {
                return;
            }
            this.VisibleChanged += new EventHandler(UserControl_VisibleChanged);
            Load_Hotkey();
            mTimer_HotkeyLED.Restart();
            #region//Hotkey Dgv Set
            dataGridView_Hotkey.Rows.Clear();
            int iRow = 0;
            foreach (clsCmData.enuEqStatus eStatus in Enum.GetValues(typeof(clsCmData.enuEqStatus)))
            {
                dataGridView_Hotkey.Rows.Add();
                dataGridView_Hotkey[dgvEqStatus.Index, iRow].Value = eStatus.ToString();
                iRow++;
            }
            dgvGreenLight.Items.Clear();
            dgvRedLight.Items.Clear();
            dgvBlueLight.Items.Clear();
            foreach (clsMachineHotkeyLEDManage.enuLED eLED in Enum.GetValues(typeof(clsMachineHotkeyLEDManage.enuLED)))
            {
                dgvGreenLight.Items.Add(eLED.ToString());
                dgvRedLight.Items.Add(eLED.ToString());
                dgvBlueLight.Items.Add(eLED.ToString());
            }
            #endregion
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                this.Dock = DockStyle.Fill;
                btnSave_Hotkey.Enabled = btnCancel_Hotkey.Enabled = panel_Hotkey.Enabled = m_bEditing_Hotkey;
                m_SelectingHotkeySetting = null;
                for (int i = 0; i < g_LstHotkey.Count; i++)
                {
                    if (g_LstHotkey[i].g_ucControlItem == null)
                    {
                        switch (g_LstHotkey[i].g_eHotKeyButton)
                        {
                            case clsMachineIOSetting.enuHotKeyButton.Run:
                                g_LstHotkey[i].g_ucControlItem = ucMachineButton_Run;
                                break;
                            case clsMachineIOSetting.enuHotKeyButton.Stop:
                                g_LstHotkey[i].g_ucControlItem = ucMachineButton_Stop;
                                break;
                            case clsMachineIOSetting.enuHotKeyButton.Reset:
                                g_LstHotkey[i].g_ucControlItem = ucMachineButton_Reset;
                                break;
                            default:
                                break;
                        }
                    }
                    if (g_LstHotkey[i].g_ucControlItem is ucMachineButton)
                    {
                        ucMachineButton Temp = (ucMachineButton)g_LstHotkey[i].g_ucControlItem;
                        Temp.UpdateControls(g_LstHotkey[i]);
                    }

                    UpdateSettingInfo(g_LstHotkey[i]);
                }
                UpdateDGV_HotkeyLED();
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
                btnSave_Hotkey.BackColor = m_bEditing_Hotkey == true ? (DateTime.Now.Millisecond > 500 ? Color.Lime : SystemColors.Control) : SystemColors.Control;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 進入此介面時,自動執行UpdateControls </summary>
        protected void UserControl_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_bEditing_Hotkey == true)
                { Load_Hotkey(); }
                m_bEditing_Hotkey = false;
                if (this.Visible == true)
                {
                    if (m_DicDIName != null)
                    { m_DicDIName.Clear(); }
                    m_DicDIName = ArtSystem.MultiSystem.clsDioMotion.GetDiName();

                    if (m_DicDOName != null)
                    { m_DicDOName.Clear(); }
                    m_DicDOName = ArtSystem.MultiSystem.clsDioMotion.GetDoName();
                    this.UpdateControls();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region//===================== Public 函式 =====================
        public bool DetectReflash(clsMachineIOSetting.enuActionStatus p_eActionStauts)
        {
            bool rValue = false;
            try
            {
                foreach (clsMachineIOSetting pHotkey in this.g_LstHotkey)
                {
                    pHotkey.ReflashDetector(p_eActionStauts);
                }
                #region//Set Hotkey LED DO
                {
                    if (g_DecHotkeyLEDManage.ContainsKey(clsCmData.g_NowEqStatus) == true)
                    {
                        clsMachineHotkeyLEDManage LEDManage = g_DecHotkeyLEDManage[clsCmData.g_NowEqStatus];
                        if (mTimer_HotkeyLED.IsTimeOut(LEDManage.g_iFlashSpeed_ms, clsCmData.enuSecUnit.MilliSec) == true)
                        {
                            mTimer_HotkeyLED.Restart();
                            m_bFlashFlag = !m_bFlashFlag;
                        }
                        foreach (clsMachineIOSetting pHotkey in this.g_LstHotkey)
                        {
                            switch (pHotkey.g_eHotKeyButton)
                            {
                                case clsMachineIOSetting.enuHotKeyButton.Run:
                                    LEDManage.SetLEDGreen_NeedOutput(pHotkey.g_eDO_ID, m_bFlashFlag);
                                    break;
                                case clsMachineIOSetting.enuHotKeyButton.Stop:
                                    LEDManage.SetLEDRed_NeedOutput(pHotkey.g_eDO_ID, m_bFlashFlag);
                                    break;
                                case clsMachineIOSetting.enuHotKeyButton.Reset:
                                    LEDManage.SetLEDBlue_NeedOutput(pHotkey.g_eDO_ID, m_bFlashFlag);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        public void Load_Hotkey()
        {
            try
            {
                string sFileName = clsMultiSystem.strSystemINIPath;
                sFileName += "\\MachineHotkey.ini";
                var Temp = ArtSystem.Files.JsonHelper.JsonDeserializeFromFile<List<clsMachineIOSetting>>(sFileName, Encoding.Unicode);
                if (Temp != null)
                {
                    clsClassFunc.Copy(Temp, g_LstHotkey);
                }
                else
                {
                    g_LstHotkey.Clear();
                }


                string sFileName_LED = clsMultiSystem.strSystemINIPath;
                sFileName_LED += "\\MachineHotkeyLED.ini";
                var Temp_LED = ArtSystem.Files.JsonHelper.JsonDeserializeFromFile<Dictionary<clsCmData.enuEqStatus, clsMachineHotkeyLEDManage>>(sFileName_LED, Encoding.Unicode);
                if (Temp_LED != null)
                {
                    clsClassFunc.Copy(Temp_LED, g_DecHotkeyLEDManage);
                }
                else
                {
                    g_DecHotkeyLEDManage.Clear();
                }
                SetDefault_Hotkey();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void Save_Hotkey()
        {
            try
            {
                string sFileName = clsMultiSystem.strSystemINIPath;
                sFileName += "\\MachineHotkey.ini";
                SetDefault_Hotkey();
                ArtSystem.Files.JsonHelper.JsonSerializeToFile(g_LstHotkey, sFileName, Encoding.Unicode);

                string sFileName_LED = clsMultiSystem.strSystemINIPath;
                sFileName_LED += "\\MachineHotkeyLED.ini";
                ArtSystem.Files.JsonHelper.JsonSerializeToFile(g_DecHotkeyLEDManage, sFileName_LED, Encoding.Unicode);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void SetDefault_Hotkey()
        {
            try
            {
                foreach (clsCmData.enuEqStatus eEqStatus in Enum.GetValues(typeof(clsCmData.enuEqStatus)))
                {
                    if (g_DecHotkeyLEDManage.ContainsKey(eEqStatus) == false)
                    { g_DecHotkeyLEDManage.Add(eEqStatus, new clsMachineHotkeyLEDManage()); }
                }
                for (int i = g_LstHotkey.Count; i < 3; i++)
                { g_LstHotkey.Add(new clsMachineIOSetting()); }
                for (int i = g_LstHotkey.Count - 1; i >= 3; i--)
                {
                    if (g_LstHotkey[i].g_ucControlItem != null)
                    {
                        g_LstHotkey[i].g_ucControlItem.Dispose();
                        g_LstHotkey[i].g_ucControlItem = null;
                    }
                    g_LstHotkey.RemoveAt(i);
                }
                if (g_LstHotkey.Count == 3)
                {
                    g_LstHotkey[0].g_eCtrlType = clsMachineIOSetting.enuCtrlType.Button_DI;
                    g_LstHotkey[1].g_eCtrlType = clsMachineIOSetting.enuCtrlType.Button_DI;
                    g_LstHotkey[2].g_eCtrlType = clsMachineIOSetting.enuCtrlType.Button_DI;
                    g_LstHotkey[0].g_eHotKeyButton = clsMachineIOSetting.enuHotKeyButton.Run;
                    g_LstHotkey[1].g_eHotKeyButton = clsMachineIOSetting.enuHotKeyButton.Stop;
                    g_LstHotkey[2].g_eHotKeyButton = clsMachineIOSetting.enuHotKeyButton.Reset;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }

        }

        #endregion

        #region//===================== Private 函式 =====================

        private string GetDIName(clsEnum.enuDi? p_eDi)
        {
            string rValue = "Null";
            if (p_eDi != null)
            {
                rValue = p_eDi.ToString();
                if (m_DicDIName != null)
                {
                    if (m_DicDIName.ContainsKey((clsEnum.enuDi)p_eDi) == true)
                    {
                        rValue += " - " + m_DicDIName[(clsEnum.enuDi)p_eDi];
                    }
                }
            }
            return rValue;
        }
        private string GetDOName(clsEnum.enuDo? p_eDo)
        {
            string rValue = "Null";
            if (p_eDo != null)
            {
                rValue = p_eDo.ToString();
                if (m_DicDOName != null)
                {
                    if (m_DicDOName.ContainsKey((clsEnum.enuDo)p_eDo) == true)
                    {
                        rValue += " - " + m_DicDOName[(clsEnum.enuDo)p_eDo];
                    }
                }
            }
            return rValue;
        }
        private void UpdateSettingInfo(clsMachineIOSetting p_MachineHotKeySetting)
        {
            try
            {
                if (p_MachineHotKeySetting != null)
                {
                    switch (p_MachineHotKeySetting.g_eCtrlType)
                    {
                        case clsMachineIOSetting.enuCtrlType.Button_DI:
                            #region//更新設定(Button_DI)
                            switch (p_MachineHotKeySetting.g_eHotKeyButton)
                            {
                                case clsMachineIOSetting.enuHotKeyButton.Run:
                                    txt_Hotkey_RunDI.Text = GetDIName(p_MachineHotKeySetting.g_eDI_ID);
                                    txt_Hotkey_RunDO.Text = GetDOName(p_MachineHotKeySetting.g_eDO_ID);
                                    break;
                                case clsMachineIOSetting.enuHotKeyButton.Stop:
                                    txt_Hotkey_StopDI.Text = GetDIName(p_MachineHotKeySetting.g_eDI_ID);
                                    txt_Hotkey_StopDO.Text = GetDOName(p_MachineHotKeySetting.g_eDO_ID);
                                    break;
                                case clsMachineIOSetting.enuHotKeyButton.Reset:
                                    txt_Hotkey_ResetDI.Text = GetDIName(p_MachineHotKeySetting.g_eDI_ID);
                                    txt_Hotkey_ResetDO.Text = GetDOName(p_MachineHotKeySetting.g_eDO_ID);
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        case clsMachineIOSetting.enuCtrlType.Sensor_DI:
                            break;
                        case clsMachineIOSetting.enuCtrlType.Sensor_AI:
                            break;
                        case clsMachineIOSetting.enuCtrlType.SetOutput_DO:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void UpdateDGV_HotkeyLED()
        {
            try
            {
                for (int iRow = 0; iRow < dataGridView_Hotkey.Rows.Count; iRow++)
                {
                    string sEqStatus = dataGridView_Hotkey[dgvEqStatus.Index, iRow].Value.ToString();
                    if (Enum.IsDefined(typeof(clsCmData.enuEqStatus), sEqStatus) == true)
                    {
                        clsCmData.enuEqStatus eEqStatus = (clsCmData.enuEqStatus)Enum.Parse(typeof(clsCmData.enuEqStatus), sEqStatus);
                        if (g_DecHotkeyLEDManage.ContainsKey(eEqStatus) == false)
                        { g_DecHotkeyLEDManage.Add(eEqStatus, new clsMachineHotkeyLEDManage()); }
                        dataGridView_Hotkey[dgvDescription.Index, iRow].Value = g_DecHotkeyLEDManage[eEqStatus].g_sDescription;
                        dataGridView_Hotkey[dgvGreenLight.Index, iRow].Value = g_DecHotkeyLEDManage[eEqStatus].g_eGreenLight.ToString();
                        dataGridView_Hotkey[dgvRedLight.Index, iRow].Value = g_DecHotkeyLEDManage[eEqStatus].g_eRedLight.ToString();
                        dataGridView_Hotkey[dgvBlueLight.Index, iRow].Value = g_DecHotkeyLEDManage[eEqStatus].g_eBlueLight.ToString();
                        dataGridView_Hotkey[dgvFlashSpeed.Index, iRow].Value = g_DecHotkeyLEDManage[eEqStatus].g_iFlashSpeed_ms.ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }


        #endregion

        #region//===================== 事件處理  (Save, Cancel, Edit) =====================
        private void btnSave_Hotkey_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_bEditing_Hotkey = false;
                Save_Hotkey();
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btnCancel_Hotkey_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_bEditing_Hotkey = false;
                Load_Hotkey();
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btnEdit_Hotkey_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_bEditing_Hotkey = !this.m_bEditing_Hotkey;
                Load_Hotkey();
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region//===================== 事件處理 (Hotkey)  =====================
        private void txt_Hotkey_DI_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (g_LstHotkey.Count == 3)
                {
                    if (sender == txt_Hotkey_RunDI)
                    {
                        string sOrgValue = g_LstHotkey[0].g_eDI_ID.ToString();
                        if (ucGetEnumDI.GetSingleton()._ShowFormDialog(ref g_LstHotkey[0].g_eDI_ID) == DialogResult.OK)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstHotkey[" + g_LstHotkey[0].g_eHotKeyButton.ToString() + "].g_eDI_ID  = "
                                       + sOrgValue + " -> " + g_LstHotkey[0].g_eDI_ID);
                            UpdateControls();
                        }
                    }
                    else if (sender == txt_Hotkey_RunDO)
                    {
                        string sOrgValue = g_LstHotkey[0].g_eDO_ID.ToString();
                        if (ucGetEnumDO.GetSingleton()._ShowFormDialog(ref g_LstHotkey[0].g_eDO_ID) == DialogResult.OK)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstHotkey[" + g_LstHotkey[0].g_eHotKeyButton.ToString() + "].g_eDO_ID  = "
                                       + sOrgValue + " -> " + g_LstHotkey[0].g_eDO_ID);
                            UpdateControls();
                        }
                    }
                    else if (sender == txt_Hotkey_StopDI)
                    {
                        string sOrgValue = g_LstHotkey[1].g_eDI_ID.ToString();
                        if (ucGetEnumDI.GetSingleton()._ShowFormDialog(ref g_LstHotkey[1].g_eDI_ID) == DialogResult.OK)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstHotkey[" + g_LstHotkey[1].g_eHotKeyButton.ToString() + "].g_eDI_ID  = "
                                       + sOrgValue + " -> " + g_LstHotkey[1].g_eDI_ID);
                            UpdateControls();
                        }
                    }
                    else if (sender == txt_Hotkey_StopDO)
                    {
                        string sOrgValue = g_LstHotkey[1].g_eDO_ID.ToString();
                        if (ucGetEnumDO.GetSingleton()._ShowFormDialog(ref g_LstHotkey[1].g_eDO_ID) == DialogResult.OK)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstHotkey[" + g_LstHotkey[1].g_eHotKeyButton.ToString() + "].g_eDO_ID  = "
                                       + sOrgValue + " -> " + g_LstHotkey[1].g_eDO_ID);
                            UpdateControls();
                        }
                    }
                    else if (sender == txt_Hotkey_ResetDI)
                    {
                        string sOrgValue = g_LstHotkey[2].g_eDI_ID.ToString();
                        if (ucGetEnumDI.GetSingleton()._ShowFormDialog(ref g_LstHotkey[2].g_eDI_ID) == DialogResult.OK)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstHotkey[" + g_LstHotkey[2].g_eHotKeyButton.ToString() + "].g_eDI_ID  = "
                                       + sOrgValue + " -> " + g_LstHotkey[2].g_eDI_ID);
                            UpdateControls();
                        }
                    }
                    else if (sender == txt_Hotkey_ResetDO)
                    {
                        string sOrgValue = g_LstHotkey[2].g_eDO_ID.ToString();
                        if (ucGetEnumDO.GetSingleton()._ShowFormDialog(ref g_LstHotkey[2].g_eDO_ID) == DialogResult.OK)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstHotkey[" + g_LstHotkey[2].g_eHotKeyButton.ToString() + "].g_eDO_ID  = "
                                       + sOrgValue + " -> " + g_LstHotkey[2].g_eDO_ID);
                            UpdateControls();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void dataGridView_Hotkey_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int iRow = e.RowIndex;
                int iColumn= e.ColumnIndex;
                string sEqStatus = dataGridView_Hotkey[dgvEqStatus.Index, iRow].Value.ToString();
                if (Enum.IsDefined(typeof(clsCmData.enuEqStatus), sEqStatus) == true)
                {
                    clsCmData.enuEqStatus eEqStatus = (clsCmData.enuEqStatus)Enum.Parse(typeof(clsCmData.enuEqStatus), sEqStatus);
                    if (g_DecHotkeyLEDManage.ContainsKey(eEqStatus) == false)
                    { g_DecHotkeyLEDManage.Add(eEqStatus, new clsMachineHotkeyLEDManage()); }
                    if (iColumn == dgvDescription.Index)
                    {
                        string sOrgValue = g_DecHotkeyLEDManage[eEqStatus].g_sDescription;
                        if (clsDialogShow.InputString("[Input String]", "Description :", ref g_DecHotkeyLEDManage[eEqStatus].g_sDescription) == DialogResult.OK)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                + ", g_DecHotkeyLEDManage[" + eEqStatus.ToString() + "].g_sDescription  = "
                                + sOrgValue + " -> " + g_DecHotkeyLEDManage[eEqStatus].g_sDescription);
                        }
                    }
                    else if (iColumn == dgvFlashSpeed.Index)
                    {
                        string sOrgValue = g_DecHotkeyLEDManage[eEqStatus].g_iFlashSpeed_ms.ToString();
                        if (FormNumBox.GetSingleton().ShowDialog(this, sOrgValue, 0, 99999, 0) == DialogResult.OK)
                        {
                            g_DecHotkeyLEDManage[eEqStatus].g_iFlashSpeed_ms = Convert.ToInt32(FormNumBox.GetSingleton().NumBoxValue);
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                + ", g_DecHotkeyLEDManage[" + eEqStatus.ToString() + "].g_iFlashSpeed_ms  = "
                                + sOrgValue + " -> " + g_DecHotkeyLEDManage[eEqStatus].g_iFlashSpeed_ms);
                        }
                    }
                    else if (iColumn == dgvGreenLight.Index)
                    {
                        //string sOrgValue = g_DecHotkeyLEDManage[eEqStatus].g_eGreenLight.ToString();
                        //List<string> LstOption = Enum.GetNames(typeof(clsMachineHotkeyLEDManage.enuLED)).ToList();
                        //if (clsDialogShow.SelectItem("Select Mo", LstOption, ref sOrgValue) == DialogResult.OK)
                        //{
                        //    if (Enum.IsDefined(typeof(clsMachineHotkeyLEDManage.enuLED), sOrgValue) == true)
                        //    {
                        //        g_DecHotkeyLEDManage[eEqStatus].g_eGreenLight = Enum.GetNames(typeof(clsMachineHotkeyLEDManage.enuLED));
                        //    }
                        //}
                    }
                }
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }

        }


        private void dataGridView_Hotkey_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int iRow = e.RowIndex;
                string sEqStatus = dataGridView_Hotkey[dgvEqStatus.Index, iRow].Value.ToString();
                clsCmData.enuEqStatus eEqStatus = (clsCmData.enuEqStatus)Enum.Parse(typeof(clsCmData.enuEqStatus), sEqStatus);
                if (g_DecHotkeyLEDManage.ContainsKey(eEqStatus) == false)
                { g_DecHotkeyLEDManage.Add(eEqStatus, new clsMachineHotkeyLEDManage()); }
                if (e.ColumnIndex == dgvGreenLight.Index)
                {
                    #region//LED Setting (Green)
                    {
                        string sOrgValue = g_DecHotkeyLEDManage[eEqStatus].g_eGreenLight.ToString();
                        string sNewValue = dataGridView_Hotkey[dgvGreenLight.Index, iRow].Value.ToString();
                        if (sOrgValue != sNewValue)
                        {
                            if (Enum.IsDefined(typeof(clsMachineHotkeyLEDManage.enuLED), sNewValue) == true)
                            {
                                g_DecHotkeyLEDManage[eEqStatus].g_eGreenLight = (clsMachineHotkeyLEDManage.enuLED)Enum.Parse(typeof(clsMachineHotkeyLEDManage.enuLED), sNewValue);
                                clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                    + ", g_DecHotkeyLEDManage[" + eEqStatus.ToString() + "].g_eGreenLight  = "
                                    + sOrgValue + " -> " + g_DecHotkeyLEDManage[eEqStatus].g_eGreenLight);
                            }
                        }
                    }
                    #endregion
                }
                else if (e.ColumnIndex == dgvRedLight.Index)
                {
                    #region//LED Setting (Red)
                    {
                        string sOrgValue = g_DecHotkeyLEDManage[eEqStatus].g_eRedLight.ToString();
                        string sNewValue = dataGridView_Hotkey[dgvRedLight.Index, iRow].Value.ToString();
                        if (sOrgValue != sNewValue)
                        {
                            if (Enum.IsDefined(typeof(clsMachineHotkeyLEDManage.enuLED), sNewValue) == true)
                            {
                                g_DecHotkeyLEDManage[eEqStatus].g_eRedLight = (clsMachineHotkeyLEDManage.enuLED)Enum.Parse(typeof(clsMachineHotkeyLEDManage.enuLED), sNewValue);
                                clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                    + ", g_DecHotkeyLEDManage[" + eEqStatus.ToString() + "].g_eRedLight  = "
                                    + sOrgValue + " -> " + g_DecHotkeyLEDManage[eEqStatus].g_eRedLight);
                            }
                        }
                    }
                    #endregion
                }
                else if (e.ColumnIndex == dgvBlueLight.Index)
                {
                    #region//LED Setting (Blue)
                    {
                        string sOrgValue = g_DecHotkeyLEDManage[eEqStatus].g_eBlueLight.ToString();
                        string sNewValue = dataGridView_Hotkey[dgvBlueLight.Index, iRow].Value.ToString();
                        if (sOrgValue != sNewValue)
                        {
                            if (Enum.IsDefined(typeof(clsMachineHotkeyLEDManage.enuLED), sNewValue) == true)
                            {
                                g_DecHotkeyLEDManage[eEqStatus].g_eBlueLight = (clsMachineHotkeyLEDManage.enuLED)Enum.Parse(typeof(clsMachineHotkeyLEDManage.enuLED), sNewValue);
                                clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                    + ", g_DecHotkeyLEDManage[" + eEqStatus.ToString() + "].g_eBlueLight  = "
                                    + sOrgValue + " -> " + g_DecHotkeyLEDManage[eEqStatus].g_eBlueLight);
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

    }
}

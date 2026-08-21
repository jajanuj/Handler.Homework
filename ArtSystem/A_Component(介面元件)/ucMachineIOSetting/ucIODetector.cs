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
    public partial class ucIODetector : ucBaseUserControl2
    {
        #region //===================== 變數設置 =====================
        /// <summary> 存檔參數 </summary>
        public List<clsMachineIOSetting> g_LstDetector = new List<clsMachineIOSetting>();

        private ucMachineButton m_SelectingDetector = null;
        private clsMachineIOSetting m_SelectingDetectorSetting = null;
        private int m_iSelectingDetectorID = -1;
        private bool m_bEditing_Detector = false;
        private Dictionary<clsEnum.enuDi, string> m_DicDIName = null;
        private Dictionary<clsEnum.enuDo, string> m_DicDOName = null;

        private clsMachineIOSetting.enuCtrlType m_eSelectingType = clsMachineIOSetting.enuCtrlType.Sensor_DI;

        private Dictionary<clsMachineIOSetting.enuCtrlType, TabPage> m_DicTabpage_IODetector = new Dictionary<clsMachineIOSetting.enuCtrlType, TabPage>();
        #endregion

        #region //===================== 必要函式設置 =====================

        static private object objLock = new object();
        static private ucIODetector m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucIODetector GetSingleton()
        {
            lock (objLock)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucIODetector();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucIODetector()
        {
            InitializeComponent();
            if (ArtSystem.clsArtSystem.bIsProgramOpen == false)
            {
                return;
            }
            this.VisibleChanged += new EventHandler(UserControl_VisibleChanged);
            Load_Detector();
            m_DicTabpage_IODetector.Add(clsMachineIOSetting.enuCtrlType.Sensor_DI, tP_SensorDI);
            m_DicTabpage_IODetector.Add(clsMachineIOSetting.enuCtrlType.SetOutput_DO, tP_OutputDO);
            m_DicTabpage_IODetector.Add(clsMachineIOSetting.enuCtrlType.Sensor_AI, tP_SensorAI);

        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                this.Dock = DockStyle.Fill;
                btnSave_Detector.Enabled = btnCancel_Detector.Enabled = panel_Detector.Enabled = m_bEditing_Detector;
                m_SelectingDetectorSetting = null;
                m_iSelectingDetectorID = -1;
                Point FirstItemLocation = new Point(3, 4);
                Size ItemSize = new Size(406, 45);
                Point SelectButtonLocation = new Point(0, 15);
                Size SelectButtonSize = new Size(69, 27);
                int iGap = 4;
                List<clsMachineIOSetting> g_LstShowDetector = new List<clsMachineIOSetting>();
                for (int i = 0; i < g_LstDetector.Count; i++)
                {
                    if (g_LstDetector[i].g_eCtrlType == m_eSelectingType)
                    {
                        g_LstShowDetector.Add(g_LstDetector[i]);
                    }
                    else if (g_LstDetector[i].g_ucControlItem != null)
                    {
                        if (g_LstDetector[i].g_ucControlItem is ucMachineButton)
                        {
                            ucMachineButton Temp = (ucMachineButton)g_LstDetector[i].g_ucControlItem;
                            Temp.Visible = false;
                        }
                    }
                }

                for (int i = 0; i < g_LstShowDetector.Count; i++)
                {
                    var Item = g_LstShowDetector[i];
                    if (Item.g_ucControlItem == null)
                    {
                        Item.g_ucControlItem = new ucMachineButton();
                        Item.g_ucControlItem.Parent = panel_ExtralContrals;
                        Item.g_ucControlItem.Size = ItemSize;
                        Item.g_ucControlItem.Left = FirstItemLocation.X;
                        Item.g_ucControlItem.Click += new EventHandler(btnSelect_Click);
                    }
                    if (Item.g_ucControlItem is ucMachineButton)
                    {
                        ucMachineButton Temp = (ucMachineButton)Item.g_ucControlItem;
                        Temp.UpdateControls(Item);
                        Temp.Visible = true;
                    }
                    Item.g_ucControlItem.Top = FirstItemLocation.Y + i * (iGap + ItemSize.Height) + panel_ExtralContrals.AutoScrollPosition.Y;

                    if (m_SelectingDetector == Item.g_ucControlItem)
                    {
                        Item.g_ucControlItem.BackColor = Color.LightGreen;
                        m_SelectingDetectorSetting = Item;
                        m_iSelectingDetectorID = i;
                    }
                    else
                    {
                        Item.g_ucControlItem.BackColor = Color.Silver;
                    }

                }
                if (panel_ExtralContrals.VerticalScroll.Visible == true)
                {
                    for (int i = 0; i < g_LstDetector.Count; i++)
                    {
                        if (g_LstDetector[i].g_ucControlItem != null)
                        {
                            g_LstDetector[i].g_ucControlItem.Width = ItemSize.Width - 20;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < g_LstDetector.Count; i++)
                    {
                        if (g_LstDetector[i].g_ucControlItem != null)
                        {
                            g_LstDetector[i].g_ucControlItem.Width = ItemSize.Width;
                        }
                    }
                }
                UpdateSettingInfo(m_SelectingDetectorSetting);
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
                btnSave_Detector.BackColor = m_bEditing_Detector == true ? (DateTime.Now.Millisecond > 500 ? Color.Lime : SystemColors.Control) : SystemColors.Control;
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_AI)
                    {
                        if (m_SelectingDetectorSetting.g_eDI_ID!= null)
                        {
                            txt_SensorAI_CurrentValue.Text = clsDioCtrl.GetAi((clsEnum.enuDi)m_SelectingDetectorSetting.g_eDI_ID).ToString("0.###");
                        }
                        else
                        {
                            txt_SensorAI_CurrentValue.Text = "";
                        }
                    }

                }
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
                if (m_bEditing_Detector == true)
                { Load_Detector(); }
                m_bEditing_Detector = false;
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
                foreach (clsMachineIOSetting pDetector in this.g_LstDetector)
                {
                    rValue |= pDetector.ReflashDetector(p_eActionStauts);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        public void Load_Detector()
        {
            try
            {
                string sFileName = clsMultiSystem.strSystemINIPath;
                sFileName += "\\MachineDetector_Other.ini";
                var Temp = ArtSystem.Files.JsonHelper.JsonDeserializeFromFile<List<clsMachineIOSetting>>(sFileName, Encoding.Unicode);
                if (Temp != null)
                {
                    foreach (clsMachineIOSetting temp in g_LstDetector)
                    {
                        if (temp.g_ucControlItem != null)
                        {
                            temp.g_ucControlItem.Parent = null;
                            temp.g_ucControlItem.Dispose();
                            temp.g_ucControlItem = null;
                        }
                    }
                    clsClassFunc.Copy(Temp, g_LstDetector);
                }
                else
                {
                    foreach (clsMachineIOSetting temp in g_LstDetector)
                    {
                        if (temp.g_ucControlItem != null)
                        {
                            temp.g_ucControlItem.Parent = null;
                            temp.g_ucControlItem.Dispose();
                            temp.g_ucControlItem = null;
                        }
                    }
                    g_LstDetector.Clear();
                }
                SortList(g_LstDetector);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void Save_Detector()
        {
            try
            {
                SortList(g_LstDetector);
                string sFileName = clsMultiSystem.strSystemINIPath;
                sFileName += "\\MachineDetector_Other.ini";
                ArtSystem.Files.JsonHelper.JsonSerializeToFile(g_LstDetector, sFileName, Encoding.Unicode);

                string sPathAlarmList = System.IO.Path.GetDirectoryName(clsCmData.g_strSystemIniFilePath);
                sPathAlarmList += "\\AlarmList.ini";
                if (System.IO.File.Exists(sPathAlarmList) == true)
                {
                    clsIniFile mFile = new clsIniFile(sPathAlarmList);
                    foreach (clsMachineIOSetting pDetector in this.g_LstDetector)
                    {
                        switch (pDetector.g_eCtrlType)
                        {
                            case clsMachineIOSetting.enuCtrlType.Button_DI:
                                break;
                            case clsMachineIOSetting.enuCtrlType.Sensor_DI:
                                {
                                    int iAlarmCode = pDetector.GetAlarmCode_DI();
                                    WriteData(mFile, iAlarmCode.ToString(), "Level", pDetector.g_iAlarmLevel.ToString());
                                    WriteData(mFile, iAlarmCode.ToString(), "Type", "0");
                                    WriteData(mFile, iAlarmCode.ToString(), "Reset", "1");
                                    WriteData(mFile, iAlarmCode.ToString(), "Skip", "0");
                                    WriteData(mFile, iAlarmCode.ToString(), "Retry", "0");
                                    WriteData(mFile, iAlarmCode.ToString(), "Continue", "0");
                                    WriteData(mFile, iAlarmCode.ToString(), "MessageTC", "設備異常 : " + pDetector.g_sSensorName_TC);
                                    WriteData(mFile, iAlarmCode.ToString(), "TroubleShootingTC", "請確認 " + pDetector.g_eDI_ID.ToString()
                                        + ", " + (pDetector.g_bDI_BType ? "是否為On" : "是否為Off"));
                                    WriteData(mFile,iAlarmCode.ToString(), "MessageEN", "Machine Error : " + pDetector.g_sSensorName_TC);
                                    WriteData(mFile, iAlarmCode.ToString(), "TroubleShootingEN", "Please Confirm " + pDetector.g_eDI_ID.ToString()
                                        + ", " + (pDetector.g_bDI_BType ? "Is On" : "Is Off"));
                                    if (mFile.GetString(iAlarmCode.ToString(), "MessageJP", "") == "")
                                    { WriteData(mFile, iAlarmCode.ToString(), "MessageJP", "Machine Error : " + pDetector.g_sSensorName_TC); }
                                    if (mFile.GetString(iAlarmCode.ToString(), "TroubleShootingJP", "") == "")
                                    {
                                        WriteData(mFile, iAlarmCode.ToString(), "TroubleShootingJP", "Please Confirm " + pDetector.g_eDI_ID.ToString()
                                          + ", " + (pDetector.g_bDI_BType ? "Is On" : "Is Off"));
                                    }
                                }
                                break;
                            case clsMachineIOSetting.enuCtrlType.Sensor_AI:
                                {
                                    int iAlarmCode = pDetector.GetAlarmCode_DI();
                                    WriteData(mFile, iAlarmCode.ToString(), "Level", pDetector.g_iAlarmLevel.ToString());
                                    WriteData(mFile, iAlarmCode.ToString(), "Type", "0");
                                    WriteData(mFile, iAlarmCode.ToString(), "Reset", "1");
                                    WriteData(mFile, iAlarmCode.ToString(), "Skip", "0");
                                    WriteData(mFile, iAlarmCode.ToString(), "Retry", "0");
                                    WriteData(mFile, iAlarmCode.ToString(), "Continue", "0");
                                    WriteData(mFile, iAlarmCode.ToString(), "MessageTC", "設備異常 : " + pDetector.g_sSensorName_TC);
                                    WriteData(mFile, iAlarmCode.ToString(), "TroubleShootingTC", "請確認 " + pDetector.g_eDI_ID.ToString()
                                        + ", 類比訊號回饋是否正常");
                                    WriteData(mFile, iAlarmCode.ToString(), "MessageEN", "Machine Error : " + pDetector.g_sSensorName_TC);
                                    WriteData(mFile, iAlarmCode.ToString(), "TroubleShootingEN", "Please Confirm " + pDetector.g_eDI_ID.ToString()
                                        + ", AI-Input Value Is Normal");
                                    if (mFile.GetString(iAlarmCode.ToString(), "MessageJP", "") == "")
                                    { WriteData(mFile, iAlarmCode.ToString(), "MessageJP", "Machine Error : " + pDetector.g_sSensorName_TC); }
                                    if (mFile.GetString(iAlarmCode.ToString(), "TroubleShootingJP", "") == "")
                                    {
                                        WriteData(mFile, iAlarmCode.ToString(), "TroubleShootingJP", "Please Confirm " + pDetector.g_eDI_ID.ToString()
                                            + ", AI-Input Value Is Normal");
                                    }
                                }
                                break;
                                break;
                            case clsMachineIOSetting.enuCtrlType.SetOutput_DO:
                                break;
                            default:
                                break;
                        }
                    }
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
                    foreach (clsMachineIOSetting.enuCtrlType eCtrlType in m_DicTabpage_IODetector.Keys)
                    {
                        if (eCtrlType == p_MachineHotKeySetting.g_eCtrlType)
                        {
                            m_DicTabpage_IODetector[eCtrlType].Parent = tabControl1;
                        }
                        else
                        {
                            m_DicTabpage_IODetector[eCtrlType].Parent = null;
                        }
                    }
                    switch (p_MachineHotKeySetting.g_eCtrlType)
                    {
                        case clsMachineIOSetting.enuCtrlType.Button_DI:
                            break;
                        case clsMachineIOSetting.enuCtrlType.Sensor_DI:
                            #region//更新設定(Sensor_DI)
                            {
                                txt_SensorDI_ID.Text = GetDIName(p_MachineHotKeySetting.g_eDI_ID);
                                txt_SensorDI_NameEN.Text = p_MachineHotKeySetting.g_sSensorName_EN;
                                txt_SensorDI_NameTC.Text = p_MachineHotKeySetting.g_sSensorName_TC;
                                txt_SensorDI_ScanMode.Text = p_MachineHotKeySetting.g_eActionStatue.ToString();
                                txt_SensorDI_AlarmLevel.Text = p_MachineHotKeySetting.g_iAlarmLevel.ToString();
                                btn_SensorDI_BType.BackgroundImage = p_MachineHotKeySetting.g_bDI_BType ? global::ArtSystem.Properties.Resources.Yes : global::ArtSystem.Properties.Resources.No;
                                txt_SensorDI_AlarmCode.Text = p_MachineHotKeySetting.GetAlarmCode_DI().ToString();
                                txt_SensorDI_ConditionDO.Text = GetDOName(p_MachineHotKeySetting.g_eDO_ID);
                                if (p_MachineHotKeySetting.g_bDO_LogicInvert == true)
                                { rBtn_SensorDI_OnDetect.Checked = true; }
                                else
                                { rBtn_SensorDI_OffDetect.Checked = true; }
                                txt_SensorDI_ConfirmDelay.Text = p_MachineHotKeySetting.g_dDI_ConfirmDelay_ms.ToString("F0");
                            }
                            #endregion
                            break;
                        case clsMachineIOSetting.enuCtrlType.Sensor_AI:
                            #region//更新設定(Sensor_AI)
                            {
                                txt_SensorAI_ID.Text = GetDIName(p_MachineHotKeySetting.g_eDI_ID);
                                txt_SensorAI_NameEN.Text = p_MachineHotKeySetting.g_sSensorName_EN;
                                txt_SensorAI_NameTC.Text = p_MachineHotKeySetting.g_sSensorName_TC;
                                txt_SensorAI_ScanMode.Text = p_MachineHotKeySetting.g_eActionStatue.ToString();
                                txt_SensorAI_AlarmLevel.Text = p_MachineHotKeySetting.g_iAlarmLevel.ToString();
                                txt_SensorAI_ThresholdValue.Text = p_MachineHotKeySetting.g_dAO_Threshold.ToString("0.###");
                                //btn_SensorAI_BType.BackgroundImage = p_MachineHotKeySetting.g_bDI_BType ? global::ArtSystem.Properties.Resources.Yes : global::ArtSystem.Properties.Resources.No;
                                txt_SensorAI_AlarmCode.Text = p_MachineHotKeySetting.GetAlarmCode_DI().ToString();
                                txt_SensorAI_ConditionDO.Text = GetDOName(p_MachineHotKeySetting.g_eDO_ID);
                                if (p_MachineHotKeySetting.g_bDI_BType == true)
                                { rBtn_SensorAI_BType_On.Checked = true; }
                                else
                                { rBtn_SensorAI_BType_Off.Checked = true; }
                                if (p_MachineHotKeySetting.g_bDO_LogicInvert == true)
                                { rBtn_SensorAI_DO_OnDetect.Checked = true; }
                                else
                                { rBtn_SensorAI_DO_OffDetect.Checked = true; }
                                txt_SensorAI_ConfirmDelay.Text = p_MachineHotKeySetting.g_dDI_ConfirmDelay_ms.ToString("F0");
                            }
                            #endregion
                            break;
                        case clsMachineIOSetting.enuCtrlType.SetOutput_DO:
                            #region//更新設定(SetOutput_DO)
                            {
                                txt_OutputDO_ID.Text = GetDOName(p_MachineHotKeySetting.g_eDO_ID);
                                txt_OutputDO_ScanMode.Text = p_MachineHotKeySetting.g_eActionStatue.ToString();
                                switch (p_MachineHotKeySetting.g_eDoOutputMode)
                                {
                                    case clsMachineIOSetting.enuDoOutputMode.OnOff:
                                        rBtn_OutputDO_OutputMode1.Checked = true;
                                        break;
                                    case clsMachineIOSetting.enuDoOutputMode.On:
                                        rBtn_OutputDO_OutputMode2.Checked = true;
                                        break;
                                    case clsMachineIOSetting.enuDoOutputMode.Off:
                                        rBtn_OutputDO_OutputMode3.Checked = true;
                                        break;
                                    default:
                                        rBtn_OutputDO_OutputMode1.Checked = false;
                                        rBtn_OutputDO_OutputMode2.Checked = false;
                                        rBtn_OutputDO_OutputMode3.Checked = false;
                                        break;
                                }
                                if (p_MachineHotKeySetting.g_bDoOuputOnce == false)
                                { rBtn_OutputDO_OutputOnce_False.Checked = true; }
                                else
                                { rBtn_OutputDO_OutputOnce_true.Checked = true; }
                                txt_OutputDO_ConditionDI.Text = GetDIName(p_MachineHotKeySetting.g_eDI_ID);
                                if (p_MachineHotKeySetting.g_bDI_BType)
                                { rBtn_OutputDO_ConditionDILogic_False.Checked = true; }
                                else
                                { rBtn_OutputDO_ConditionDILogic_True.Checked = true; }
                            }
                            #endregion
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    foreach (clsMachineIOSetting.enuCtrlType eCtrlType in m_DicTabpage_IODetector.Keys)
                    {
                        m_DicTabpage_IODetector[eCtrlType].Parent = null;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void WriteData(clsIniFile p_INIFile, string p_sSection, string p_sName, string p_sValue)
        {
            try
            {
                if (p_INIFile.GetString(p_sSection, p_sName, "") != p_sValue)
                {
                    p_INIFile.WriteValue(p_sSection, p_sName, p_sValue);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void SortList(List<clsMachineIOSetting> p_ListItem)
        {
            try
            {
                for (int i = 0; i < p_ListItem.Count; i++)
                {
                    int j = i + 1;
                    int iMin = i;
                    int iMinValue = 0;
                    if (p_ListItem[i].g_eDI_ID != null 
                        && (p_ListItem[i].g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_DI
                        || p_ListItem[i].g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_AI))
                    {
                        iMinValue = (int)p_ListItem[i].g_eDI_ID;

                    }
                    else if (p_ListItem[i].g_eDO_ID != null
                        && p_ListItem[i].g_eCtrlType == clsMachineIOSetting.enuCtrlType.SetOutput_DO)
                    {
                        iMinValue = (int)p_ListItem[i].g_eDO_ID + 10000;
                    }
                    for (; j < p_ListItem.Count; j++)
                    {
                        if (p_ListItem[j].g_eDI_ID == null
                            && p_ListItem[j].g_eDO_ID == null)
                        {
                            iMin = j;
                            break;
                        }
                        else if (p_ListItem[j].g_eDI_ID != null
                            && (p_ListItem[j].g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_DI
                            || p_ListItem[j].g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_AI))
                        {
                            int iDI = (int)p_ListItem[j].g_eDI_ID;
                            if (iDI  < iMinValue)
                            {
                                iMinValue = iDI;
                                iMin = j;
                            }
                        }
                        else if (p_ListItem[j].g_eDO_ID != null
                            && p_ListItem[j].g_eCtrlType == clsMachineIOSetting.enuCtrlType.SetOutput_DO)
                        {
                            int iDO = (int)p_ListItem[j].g_eDO_ID + 10000;
                            if (iDO < iMinValue)
                            {
                                iMinValue = iDO;
                                iMin = j;
                            }
                        }
                    }
                    if (i != iMin)
                    {
                        var Temp = p_ListItem[i];
                        p_ListItem[i] = p_ListItem[iMin];
                        p_ListItem[iMin] = Temp;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region//===================== 事件處理  (Save, Cancel, Edit) =====================
        private void btnSave_Detector_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_bEditing_Detector = false;
                Save_Detector();
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btnCancel_Detector_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_bEditing_Detector = false;
                Load_Detector();
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btnEdit_Detector_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_bEditing_Detector = !this.m_bEditing_Detector;
                Load_Detector();
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region//===================== 事件處理 (Detector)  =====================

        private void btn_AddDI_Click(object sender, EventArgs e)
        {
            try
            {
                clsMachineIOSetting AddItem = new clsMachineIOSetting();
                AddItem.g_eCtrlType = clsMachineIOSetting.enuCtrlType.Sensor_DI;
                this.g_LstDetector.Add(AddItem);
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btn_Add_AI_Click(object sender, EventArgs e)
        {
            try
            {
                clsMachineIOSetting AddItem = new clsMachineIOSetting();
                AddItem.g_eCtrlType = clsMachineIOSetting.enuCtrlType.Sensor_AI;
                this.g_LstDetector.Add(AddItem);
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btn_AddDO_Click(object sender, EventArgs e)
        {
            try
            {
                clsMachineIOSetting AddItem = new clsMachineIOSetting();
                AddItem.g_eCtrlType = clsMachineIOSetting.enuCtrlType.SetOutput_DO;
                this.g_LstDetector.Add(AddItem);
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btn_RemoveDetectorItem_Click(object sender, EventArgs e)
        {
            try
            {
                clsMachineIOSetting AddItem = new clsMachineIOSetting();
                AddItem.g_eCtrlType = clsMachineIOSetting.enuCtrlType.SetOutput_DO;
                for(int i=0;i< this.g_LstDetector.Count;i++)
                {
                    if (m_SelectingDetector == this.g_LstDetector[i].g_ucControlItem)
                    {
                        if (this.g_LstDetector[i].g_ucControlItem != null)
                        {
                            this.g_LstDetector[i].g_ucControlItem.Dispose();
                            this.g_LstDetector[i].g_ucControlItem = null;
                        }
                        this.g_LstDetector.RemoveAt(i);
                        break;
                    }
                }
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is ucMachineButton)
                {
                    m_SelectingDetector = (ucMachineButton)sender;
                    UpdateControls();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region//========== Edit DI ==========
        private void txt_DI_ID_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_DI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_eDI_ID.ToString();
                        if (ucGetEnumDI.GetSingleton()._ShowFormDialog(ref m_SelectingDetectorSetting.g_eDI_ID) == DialogResult.OK)
                        {
                            if (m_SelectingDetectorSetting.g_eDI_ID != null)
                            {
                                if (m_SelectingDetectorSetting.g_sSensorName_EN == "")
                                {
                                    var IOName = clsDioMotion.GetDiName_EN();
                                    if (IOName.ContainsKey((clsEnum.enuDi)m_SelectingDetectorSetting.g_eDI_ID) == true)
                                    {
                                        m_SelectingDetectorSetting.g_sSensorName_EN = IOName[(clsEnum.enuDi)m_SelectingDetectorSetting.g_eDI_ID];
                                    }
                                }
                                if (m_SelectingDetectorSetting.g_sSensorName_TC == "")
                                {
                                    var IOName = clsDioMotion.GetDiName_TC();
                                    if (IOName.ContainsKey((clsEnum.enuDi)m_SelectingDetectorSetting.g_eDI_ID) == true)
                                    {
                                        m_SelectingDetectorSetting.g_sSensorName_TC = IOName[(clsEnum.enuDi)m_SelectingDetectorSetting.g_eDI_ID];
                                    }
                                }
                            }
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_eDI_ID  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_eDI_ID + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void txt_DIName_EN_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_DI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_sSensorName_EN.ToString();
                        if (clsDialogShow.InputString("Input Name", "English Name :", ref m_SelectingDetectorSetting.g_sSensorName_EN) == DialogResult.OK)
                        {
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_sSensorName_EN  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_sSensorName_EN + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void txt_DIName_TC_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_DI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_sSensorName_TC.ToString();
                        if (clsDialogShow.InputString("Input Name", "Chinese Name :", ref m_SelectingDetectorSetting.g_sSensorName_TC) == DialogResult.OK)
                        {
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_sSensorName_TC  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_sSensorName_TC + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void txt_DI_ScanMode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_DI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_eActionStatue.ToString();
                        List<string> LstItems = Enum.GetNames(typeof(clsMachineIOSetting.enuActionStatus)).ToList<string>();
                        if (clsDialogShow.SelectItem("[Select Enum]", LstItems, ref sOrgValue) == DialogResult.OK)
                        {
                            m_SelectingDetectorSetting.g_eActionStatue = (clsMachineIOSetting.enuActionStatus)Enum.Parse(typeof(clsMachineIOSetting.enuActionStatus), sOrgValue);
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_eActionStatue  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_eActionStatue + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void btn_DI_BType_Click(object sender, EventArgs e)
        {

            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_DI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_bDI_BType.ToString();
                        m_SelectingDetectorSetting.g_bDI_BType = !m_SelectingDetectorSetting.g_bDI_BType;
                        UpdateControls();
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                   + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_bDI_BType  = "
                                   + sOrgValue + " -> " + m_SelectingDetectorSetting.g_bDI_BType + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }

        }
        private void txtSensorDI_AlarmLevel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_DI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_iAlarmLevel.ToString();
                        if (FormNumBox.GetSingleton().ShowDialog(this, sOrgValue, 9, 1, 0) == DialogResult.OK)
                        {
                            m_SelectingDetectorSetting.g_iAlarmLevel = Convert.ToInt32(FormNumBox.GetSingleton().NumBoxValue);
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_iAlarmLevel  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_iAlarmLevel + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void txt_DI_ConditionDO_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_DI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_eDO_ID.ToString();
                        if (ucGetEnumDO.GetSingleton()._ShowFormDialog(ref m_SelectingDetectorSetting.g_eDO_ID) == DialogResult.OK)
                        {
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_eDO_ID  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_eDO_ID + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btn_ConditionDO_Logic_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    string sOrgValue = m_SelectingDetectorSetting.g_bDO_LogicInvert.ToString();
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_DI)
                    {
                        m_SelectingDetectorSetting.g_bDO_LogicInvert = !m_SelectingDetectorSetting.g_bDO_LogicInvert;
                        UpdateControls();
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                   + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_bDO_LogicInvert  = "
                                   + sOrgValue + " -> " + m_SelectingDetectorSetting.g_bDO_LogicInvert + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void txtSensorDI_ConfirmDelay_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_DI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_dDI_ConfirmDelay_ms.ToString();
                        if (FormNumBox.GetSingleton().ShowDialog(this, sOrgValue, 99999999, 0, 0) == DialogResult.OK)
                        {
                            m_SelectingDetectorSetting.g_dDI_ConfirmDelay_ms = Convert.ToInt32(FormNumBox.GetSingleton().NumBoxValue);
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_dDI_ConfirmDelay_ms  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_dDI_ConfirmDelay_ms + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    string sOrgValue = m_SelectingDetectorSetting.g_bDO_LogicInvert.ToString();
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_DI)
                    {
                        if (sender == rBtn_SensorDI_OnDetect)
                        {
                            m_SelectingDetectorSetting.g_bDO_LogicInvert = true;
                        }
                        else if (sender == rBtn_SensorDI_OffDetect)
                        {
                            m_SelectingDetectorSetting.g_bDO_LogicInvert = false;

                        }
                        //m_SelectingDetectorSetting.g_bDO_LogicInvert = !m_SelectingDetectorSetting.g_bDO_LogicInvert;
                        UpdateControls();
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                   + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_bDO_LogicInvert  = "
                                   + sOrgValue + " -> " + m_SelectingDetectorSetting.g_bDO_LogicInvert + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region//========== Edit DO ==========

        private void txt_OutputDO_ID_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.SetOutput_DO)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_eDO_ID.ToString();
                        if (ucGetEnumDO.GetSingleton()._ShowFormDialog(ref m_SelectingDetectorSetting.g_eDO_ID) == DialogResult.OK)
                        {
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_eDO_ID  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_eDO_ID + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void txt_OutputDO_ScanMode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.SetOutput_DO)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_eActionStatue.ToString();
                        List<string> LstItems = Enum.GetNames(typeof(clsMachineIOSetting.enuActionStatus)).ToList<string>();
                        if (clsDialogShow.SelectItem("[Select Enum]", LstItems, ref sOrgValue) == DialogResult.OK)
                        {
                            m_SelectingDetectorSetting.g_eActionStatue = (clsMachineIOSetting.enuActionStatus)Enum.Parse(typeof(clsMachineIOSetting.enuActionStatus), sOrgValue);
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_eActionStatue  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_eActionStatue + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void txt_OutputDO_ConditionDI_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.SetOutput_DO)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_eDI_ID.ToString();
                        if (ucGetEnumDI.GetSingleton()._ShowFormDialog(ref m_SelectingDetectorSetting.g_eDI_ID) == DialogResult.OK)
                        {
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_eDI_ID  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_eDI_ID + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void rBtn_OutputDO_OutputMode1_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    string sOrgValue = m_SelectingDetectorSetting.g_eDoOutputMode.ToString();
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.SetOutput_DO)
                    {
                        if (rBtn_OutputDO_OutputMode1.Checked == true)
                        {
                            m_SelectingDetectorSetting.g_eDoOutputMode = clsMachineIOSetting.enuDoOutputMode.OnOff;
                        }
                        else if (rBtn_OutputDO_OutputMode2.Checked == true)
                        {
                            m_SelectingDetectorSetting.g_eDoOutputMode = clsMachineIOSetting.enuDoOutputMode.On;
                        }
                        else if (rBtn_OutputDO_OutputMode3.Checked == true)
                        {
                            m_SelectingDetectorSetting.g_eDoOutputMode = clsMachineIOSetting.enuDoOutputMode.Off;
                        }
                        //m_SelectingDetectorSetting.g_bDO_LogicInvert = !m_SelectingDetectorSetting.g_bDO_LogicInvert;
                        UpdateControls();
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                   + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_eDoOutputMode  = "
                                   + sOrgValue + " -> " + m_SelectingDetectorSetting.g_eDoOutputMode + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void rBtn_OutputDO_OutputOnce_False_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    string sOrgValue = m_SelectingDetectorSetting.g_bDoOuputOnce.ToString();
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.SetOutput_DO)
                    {
                        if (sender == rBtn_OutputDO_OutputOnce_False)
                        {
                            m_SelectingDetectorSetting.g_bDoOuputOnce = false;
                        }
                        else if (sender == rBtn_OutputDO_OutputOnce_true)
                        {
                            m_SelectingDetectorSetting.g_bDoOuputOnce = true;
                        }
                        UpdateControls();
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                   + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_bDoOuputOnce  = "
                                   + sOrgValue + " -> " + m_SelectingDetectorSetting.g_bDoOuputOnce + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void rBtn_OutputDO_ConditionDILogic_False_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    string sOrgValue = m_SelectingDetectorSetting.g_bDI_BType.ToString();
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.SetOutput_DO)
                    {
                        if (sender == rBtn_OutputDO_ConditionDILogic_False)
                        {
                            m_SelectingDetectorSetting.g_bDI_BType = true;
                        }
                        else if(sender == rBtn_OutputDO_ConditionDILogic_True)
                        {
                            m_SelectingDetectorSetting.g_bDI_BType = false;
                        }
                        UpdateControls();
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                   + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_bDI_BType  = "
                                   + sOrgValue + " -> " + m_SelectingDetectorSetting.g_bDI_BType + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region//========== Edit AI ==========

        private void txt_SensorAI_ID_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_AI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_eDI_ID.ToString();
                        if (ucGetEnumAI.GetSingleton()._ShowFormDialog(ref m_SelectingDetectorSetting.g_eDI_ID) == DialogResult.OK)
                        {
                            if (m_SelectingDetectorSetting.g_eDI_ID != null)
                            {
                                if (m_SelectingDetectorSetting.g_sSensorName_EN == "")
                                {
                                    var IOName = clsDioMotion.GetDiName_EN();
                                    if (IOName.ContainsKey((clsEnum.enuDi)m_SelectingDetectorSetting.g_eDI_ID) == true)
                                    {
                                        m_SelectingDetectorSetting.g_sSensorName_EN = IOName[(clsEnum.enuDi)m_SelectingDetectorSetting.g_eDI_ID];
                                    }
                                }
                                if (m_SelectingDetectorSetting.g_sSensorName_TC == "")
                                {
                                    var IOName = clsDioMotion.GetDiName_TC();
                                    if (IOName.ContainsKey((clsEnum.enuDi)m_SelectingDetectorSetting.g_eDI_ID) == true)
                                    {
                                        m_SelectingDetectorSetting.g_sSensorName_TC = IOName[(clsEnum.enuDi)m_SelectingDetectorSetting.g_eDI_ID];
                                    }
                                }
                            }
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_eDI_ID  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_eDI_ID + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void txt_SensorAI_NameEN_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_AI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_sSensorName_EN.ToString();
                        if (clsDialogShow.InputString("Input Name", "English Name :", ref m_SelectingDetectorSetting.g_sSensorName_EN) == DialogResult.OK)
                        {
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_sSensorName_EN  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_sSensorName_EN + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void txt_SensorAI_NameTC_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_AI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_sSensorName_TC.ToString();
                        if (clsDialogShow.InputString("Input Name", "Chinese Name :", ref m_SelectingDetectorSetting.g_sSensorName_TC) == DialogResult.OK)
                        {
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_sSensorName_TC  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_sSensorName_TC + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void txt_SensorAI_ScanMode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_AI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_eActionStatue.ToString();
                        List<string> LstItems = Enum.GetNames(typeof(clsMachineIOSetting.enuActionStatus)).ToList<string>();
                        if (clsDialogShow.SelectItem("[Select Enum]", LstItems, ref sOrgValue) == DialogResult.OK)
                        {
                            m_SelectingDetectorSetting.g_eActionStatue = (clsMachineIOSetting.enuActionStatus)Enum.Parse(typeof(clsMachineIOSetting.enuActionStatus), sOrgValue);
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_eActionStatue  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_eActionStatue + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void txt_SensorAI_AlarmLevel_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_AI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_iAlarmLevel.ToString();
                        if (FormNumBox.GetSingleton().ShowDialog(this, sOrgValue, 9, 1, 0) == DialogResult.OK)
                        {
                            m_SelectingDetectorSetting.g_iAlarmLevel = Convert.ToInt32(FormNumBox.GetSingleton().NumBoxValue);
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_iAlarmLevel  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_iAlarmLevel + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void txt_SensorAI_ConfirmDelay_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_AI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_dDI_ConfirmDelay_ms.ToString();
                        if (FormNumBox.GetSingleton().ShowDialog(this, sOrgValue, 99999999, 0, 0) == DialogResult.OK)
                        {
                            m_SelectingDetectorSetting.g_dDI_ConfirmDelay_ms = Convert.ToInt32(FormNumBox.GetSingleton().NumBoxValue);
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_dDI_ConfirmDelay_ms  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_dDI_ConfirmDelay_ms + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void txt_SensorAI_ThresholdValue_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_AI)
                    {
                        string sOrgValue = m_SelectingDetectorSetting.g_dAO_Threshold.ToString();
                        if (FormNumBox.GetSingleton().ShowDialog(this, sOrgValue, 99999999, 0, 3) == DialogResult.OK)
                        {
                            m_SelectingDetectorSetting.g_dAO_Threshold = Convert.ToDouble(FormNumBox.GetSingleton().NumBoxValue);
                            UpdateControls();
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                       + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_dAO_Threshold  = "
                                       + sOrgValue + " -> " + m_SelectingDetectorSetting.g_dAO_Threshold + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void txt_SensorAI_ConditionDO_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    string sOrgValue = m_SelectingDetectorSetting.g_bDO_LogicInvert.ToString();
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_AI)
                    {
                        m_SelectingDetectorSetting.g_bDO_LogicInvert = !m_SelectingDetectorSetting.g_bDO_LogicInvert;
                        UpdateControls();
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                   + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_bDO_LogicInvert  = "
                                   + sOrgValue + " -> " + m_SelectingDetectorSetting.g_bDO_LogicInvert + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void rBtn_SensorAI_BType_On_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    string sOrgValue = m_SelectingDetectorSetting.g_bDI_BType.ToString();
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_AI)
                    {
                        if (sender == rBtn_SensorAI_BType_Off)
                        {
                            m_SelectingDetectorSetting.g_bDI_BType = false;
                        }
                        else if (sender == rBtn_SensorAI_BType_On)
                        {
                            m_SelectingDetectorSetting.g_bDI_BType = true;
                        }
                        UpdateControls();
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                   + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_bDI_BType  = "
                                   + sOrgValue + " -> " + m_SelectingDetectorSetting.g_bDI_BType + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void rBtn_SensorAI_DO_OnDetect_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_SelectingDetectorSetting != null)
                {
                    string sOrgValue = m_SelectingDetectorSetting.g_bDO_LogicInvert.ToString();
                    if (m_SelectingDetectorSetting.g_eCtrlType == clsMachineIOSetting.enuCtrlType.Sensor_AI)
                    {
                        if (sender == rBtn_SensorAI_DO_OnDetect)
                        {
                            m_SelectingDetectorSetting.g_bDO_LogicInvert = true;
                        }
                        else if (sender == rBtn_SensorAI_DO_OffDetect)
                        {
                            m_SelectingDetectorSetting.g_bDO_LogicInvert = false;

                        }
                        //m_SelectingDetectorSetting.g_bDO_LogicInvert = !m_SelectingDetectorSetting.g_bDO_LogicInvert;
                        UpdateControls();
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                   + ", g_LstDetector[" + m_iSelectingDetectorID.ToString() + "].g_bDO_LogicInvert  = "
                                   + sOrgValue + " -> " + m_SelectingDetectorSetting.g_bDO_LogicInvert + ", CtrlType = " + m_SelectingDetectorSetting.g_eCtrlType.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl2.SelectedIndex == 0)
                {
                    m_eSelectingType = clsMachineIOSetting.enuCtrlType.Sensor_DI;
                }
                else if (tabControl2.SelectedIndex == 1)
                {
                    m_eSelectingType = clsMachineIOSetting.enuCtrlType.SetOutput_DO;
                }
                else if (tabControl2.SelectedIndex == 2)
                {
                    m_eSelectingType = clsMachineIOSetting.enuCtrlType.Sensor_AI;
                }
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtProcModuleLib;
using ArtControlLib;
using ArtCommonLib;
using ArtData;
using ArtSystem;

namespace ArtEQ
{
    public partial class ucProcessControl : ucBaseUserControl
    {

        #region //=====================  Class定義 =====================
        private class AlarmCodeInfo
        {
            public int Code = 0;
            /// <summary> Level 2 = (Down) </summary>
            public int Level = 1;
            public int Type = 0;
            public int Reset = 0;
            public int Skip = 0;
            public int Retry = 0;
            public int Continue = 0;
            public string MessageTC = "";
            public string TroubleShootingTC = "";
            public string MessageEN = "";
            public string TroubleShootingEN = "";
            public string MessageJP = "";
            public string TroubleShootingJP = "";

            public AlarmCodeInfo(int p_Code)
            {
                Code = p_Code;
            }
            public AlarmCodeInfo(List<string> InputString)
            {
                foreach (string Data in InputString)
                {
                    if (Data.Contains("[") && Data.Contains("]"))
                    {
                        Code = Convert.ToInt32(Data.Replace("[", "").Replace("]", ""));
                    }
                    else if (Data.Contains("Level="))
                    {
                        Level = Convert.ToInt32(Data.Replace("Level=", ""));
                    }
                    else if (Data.Contains("Type="))
                    {
                        Type = Convert.ToInt32(Data.Replace("Type=", ""));
                    }
                    else if (Data.Contains("Reset="))
                    {
                        Reset = Convert.ToInt32(Data.Replace("Reset=", ""));
                    }
                    else if (Data.Contains("Skip="))
                    {
                        Skip = Convert.ToInt32(Data.Replace("Skip=", ""));
                    }
                    else if (Data.Contains("Retry="))
                    {
                        Retry = Convert.ToInt32(Data.Replace("Retry=", ""));
                    }
                    else if (Data.Contains("Continue="))
                    {
                        Continue = Convert.ToInt32(Data.Replace("Continue=", ""));
                    }
                    else if (Data.Contains("MessageTC="))
                    {
                        MessageTC = Data.Replace("MessageTC=", "");
                    }
                    else if (Data.Contains("TroubleShootingTC="))
                    {
                        TroubleShootingTC = Data.Replace("TroubleShootingTC=", "");
                    }
                    else if (Data.Contains("MessageEN="))
                    {
                        MessageEN = Data.Replace("MessageEN=", "");
                    }
                    else if (Data.Contains("TroubleShootingEN="))
                    {
                        TroubleShootingEN = Data.Replace("TroubleShootingEN=", "");
                    }
                    else if (Data.Contains("MessageJP="))
                    {
                        MessageJP = Data.Replace("MessageJP=", "");
                    }
                    else if (Data.Contains("TroubleShootingJP="))
                    {
                        TroubleShootingJP = Data.Replace("TroubleShootingJP=", "");
                    }
                }
            }
        }

        #endregion

        #region //=====================  區域變數設置 =====================

        private int iOrgWidth = 0;
        private int iOrgHeight = 0;
        private float fOrgScale = 0;

        private List<string> LstAlarmCode = new List<string>();
        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucProcessControl m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucProcessControl GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucProcessControl();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucProcessControl()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            if (iOrgWidth == 0)
            {
                iOrgHeight = this.Height;
                iOrgWidth = this.Width;
                fOrgScale = 1;
            }
            this.TimerInterval = 100;
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                if (this.Parent != null)
                {
                    this.Scale(new SizeF(1 / fOrgScale, 1 / fOrgScale));
                    float wScale = (float)this.Parent.Width / (float)iOrgWidth;
                    float hScale = (float)this.Parent.Height / (float)iOrgHeight;
                    if (wScale != 0 && hScale != 0)
                    {
                        fOrgScale = wScale;
                        if (fOrgScale > hScale)
                        { fOrgScale = hScale; }
                        this.Scale(new SizeF(fOrgScale,fOrgScale));
                    }
                    else
                    {
                        fOrgScale = 1;
                    }
                }
                string strSelectItem = "";
                if (listBox1.SelectedItem != null)
                { strSelectItem = listBox1.SelectedItem.ToString(); }
                listBox1.Items.Clear();
                foreach (clsBaseProc pBaseProc in clsBaseProc.m_dctProcData.Values)
                {
                    listBox1.Items.Add("[" + pBaseProc.m_ModuleID.ToString("000") + "]" + pBaseProc.GetModuelName());
                }
                foreach (AutoProcess pBaseProc in AutoProcess.m_dctProcData.Values)
                {
                    listBox1.Items.Add("[" + pBaseProc.APID.ToString("000") + "]" + pBaseProc.Name);
                }
                if (strSelectItem != ""
                    && listBox1.Items.Contains(strSelectItem) == true)
                {
                    listBox1.SelectedItem = strSelectItem;
                }
                ReloadINI();
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        /// <summary> 自動更新介面參數 </summary>
        protected override void ReflashTimerFunc()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        private void ucMachineStatus_VisibleChanged(object sender, EventArgs e)
        {
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            if (this.Visible == true)
            {
                this.UpdateControls();
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================


        #endregion

        #region //===================== private 函式設置 =====================


        private void ReloadINI()
        {
            try
            {
                string AlarmList_INIPath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\AlarmList.ini";
                if (System.IO.File.Exists(AlarmList_INIPath) == false)
                {
                    System.IO.File.Create(AlarmList_INIPath);
                }
                List<string> Datas = System.IO.File.ReadAllLines(AlarmList_INIPath).ToList<string>();
                LstAlarmCode.Clear();
                foreach (string sMessage in Datas)
                {
                    if (sMessage.Contains("[") && sMessage.Contains("]"))
                    {
                        LstAlarmCode.Add(sMessage.Replace("[", "").Replace("]", ""));
                    }
                }
                richTextBox1.Lines = Datas.ToArray();
                ucAlarmManage.GetSingleton().UpdateControls();
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }
        private void UpdateAlarmList()
        {
            checkedListBox1.Items.Clear();
            string strSelectItem = "";
            if (listBox1.SelectedItem != null)
            {
                strSelectItem = listBox1.SelectedItem.ToString();
                if (strSelectItem.Contains("]") == true)
                {
                    strSelectItem = strSelectItem.Split(']')[1];
                    if (clsBaseProc.m_dctProcData.ContainsKey(strSelectItem) == true)
                    {
                        List<clsAlarmName> LstAlarm = clsBaseProc.m_dctProcData[strSelectItem].GetAlarmList();
                        for (int i = 0; i < LstAlarm.Count; i++)
                        {
                            if (radioButton2.Checked == true)
                            {
                                string sMeg = "[" + LstAlarm[i].iAlarmCode + "]" + LstAlarm[i].sAlarmName_TC;
                                checkedListBox1.Items.Add(sMeg, LstAlarmCode.Contains(LstAlarm[i].iAlarmCode.ToString()));
                            }
                            else
                            {
                                string sMeg = "[" + LstAlarm[i].iAlarmCode + "]" + LstAlarm[i].sAlarmName_EN;
                                checkedListBox1.Items.Add(sMeg, LstAlarmCode.Contains(LstAlarm[i].iAlarmCode.ToString()));
                            }
                        }
                    }
                    if (AutoProcess.m_dctProcData.ContainsKey(strSelectItem) == true)
                    {
                        List<clsAlarmName> LstAlarm = AutoProcess.m_dctProcData[strSelectItem].GetAlarmList();
                        for (int i = 0; i < LstAlarm.Count; i++)
                        {
                            if (radioButton2.Checked == true)
                            {
                                string sMeg = "[" + LstAlarm[i].iAlarmCode + "]" + LstAlarm[i].sAlarmName_TC;
                                checkedListBox1.Items.Add(sMeg, LstAlarmCode.Contains(LstAlarm[i].iAlarmCode.ToString()));
                            }
                            else
                            {
                                string sMeg = "[" + LstAlarm[i].iAlarmCode + "]" + LstAlarm[i].sAlarmName_EN;
                                checkedListBox1.Items.Add(sMeg, LstAlarmCode.Contains(LstAlarm[i].iAlarmCode.ToString()));
                            }
                        }
                    }
                }
            }
        }
        private void AlarmCodeBuilder_SinglePM()
        {
            try
            {
                Dictionary<string, AlarmCodeInfo> AlarmList_Dictionary = new Dictionary<string, AlarmCodeInfo>();
                string AlarmList_INIPath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\AlarmList.ini";
                if (System.IO.File.Exists(AlarmList_INIPath) == true)
                {
                    #region//載入舊的AlarmList.INI
                    string[] AlarmList_ReadAllLines = System.IO.File.ReadAllLines(AlarmList_INIPath);
                    for (int i = 0; i < AlarmList_ReadAllLines.Length; i++)
                    {
                        try
                        {
                            if (AlarmList_ReadAllLines[i].Contains("[") && AlarmList_ReadAllLines[i].Contains("]"))
                            {
                                int AlarmCode = 0;
                                List<string> AlarmData_TempStringList = new List<string>();
                                AlarmData_TempStringList.Add(AlarmList_ReadAllLines[i]);
                                AlarmCode = Convert.ToInt32(AlarmList_ReadAllLines[i].Replace("[", "").Replace("]", ""));
                                i++;
                                for (; i < AlarmList_ReadAllLines.Length; i++)
                                {
                                    if (AlarmList_ReadAllLines[i].Contains("[") && AlarmList_ReadAllLines[i].Contains("]"))
                                    {
                                        i -= 2;
                                        break;
                                    }
                                    AlarmData_TempStringList.Add(AlarmList_ReadAllLines[i]);
                                }
                                AlarmList_Dictionary.Add(AlarmCode.ToString(), new AlarmCodeInfo(AlarmData_TempStringList));
                            }
                        }
                        catch
                        {
                        }
                    }
                    #endregion
                }
                
                #region//重新加入enuAlarm裡面的定義.
                string strSelectItem = "";
                if (listBox1.SelectedItem != null)
                {
                    strSelectItem = listBox1.SelectedItem.ToString();
                    if (strSelectItem.Contains("]") == true)
                    {
                        strSelectItem = strSelectItem.Split(']')[1];
                        List<clsAlarmName> LstAlarm = new List<clsAlarmName>();
                        if (AutoProcess.m_dctProcData.ContainsKey(strSelectItem) == true)
                        {
                           LstAlarm = AutoProcess.m_dctProcData[strSelectItem].GetAlarmList();
                        }

                        if (clsBaseProc.m_dctProcData.ContainsKey(strSelectItem) == true)
                        {
                           LstAlarm = clsBaseProc.m_dctProcData[strSelectItem].GetAlarmList();
                        }
                        for (int i = 0; i < LstAlarm.Count; i++)
                        {
                            string sMeg = "";
                            if (radioButton2.Checked == true)
                            { sMeg = "[" + LstAlarm[i].iAlarmCode + "]" + LstAlarm[i].sAlarmName_TC; }
                            else
                            { sMeg = "[" + LstAlarm[i].iAlarmCode + "]" + LstAlarm[i].sAlarmName_EN; }

                            if (checkedListBox1.Items.Contains(sMeg) == true)
                            {
                                int index = checkedListBox1.FindString(sMeg, 0);
                                if (checkedListBox1.GetItemChecked(index) == true)
                                {
                                    try
                                    {
                                        string AlarmCode = LstAlarm[i].iAlarmCode.ToString();
                                        if (AlarmList_Dictionary.Keys.Contains(AlarmCode) == false)
                                        {
                                            AlarmList_Dictionary.Add(AlarmCode, new AlarmCodeInfo(LstAlarm[i].iAlarmCode));
                                        }
                                        AlarmList_Dictionary[AlarmCode].Level = 1;
                                        AlarmList_Dictionary[AlarmCode].MessageEN = LstAlarm[i].sAlarmName_EN;
                                        AlarmList_Dictionary[AlarmCode].TroubleShootingEN = AlarmList_Dictionary[AlarmCode].MessageEN;
                                        AlarmList_Dictionary[AlarmCode].MessageTC = LstAlarm[i].sAlarmName_TC;
                                        AlarmList_Dictionary[AlarmCode].MessageJP = AlarmList_Dictionary[AlarmCode].MessageEN;
                                        AlarmList_Dictionary[AlarmCode].TroubleShootingTC = AlarmList_Dictionary[AlarmCode].MessageEN;
                                        AlarmList_Dictionary[AlarmCode].TroubleShootingJP = AlarmList_Dictionary[AlarmCode].MessageEN;
                                        AlarmList_Dictionary[AlarmCode].Reset = 1;
                                    }
                                    catch (Exception ex)
                                    {
                                        clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region//重新創健一個新的AlarmList.INI
                List<int> LstAlarmCode = new List<int>();
                foreach (string sKey in AlarmList_Dictionary.Keys)
                {
                    if (LstAlarmCode.Contains(AlarmList_Dictionary[sKey].Code) == false)
                    {
                        LstAlarmCode.Add(AlarmList_Dictionary[sKey].Code);
                    }
                }
                LstAlarmCode.Sort();



                List<string> AlarmList_New = new List<string>();
                foreach (int iKey in LstAlarmCode)
                {
                    string sKey = iKey.ToString();
                    if (AlarmList_Dictionary.ContainsKey(sKey) == true)
                    {
                        AlarmList_New.Add("[" + AlarmList_Dictionary[sKey].Code + "]");
                        AlarmList_New.Add("Level=" + AlarmList_Dictionary[sKey].Level);
                        AlarmList_New.Add("Type=" + AlarmList_Dictionary[sKey].Type);
                        AlarmList_New.Add("Reset=" + AlarmList_Dictionary[sKey].Reset);
                        AlarmList_New.Add("Skip=" + AlarmList_Dictionary[sKey].Skip);
                        AlarmList_New.Add("Retry=" + AlarmList_Dictionary[sKey].Retry);
                        AlarmList_New.Add("Continue=" + AlarmList_Dictionary[sKey].Continue);
                        AlarmList_New.Add("MessageTC=" + AlarmList_Dictionary[sKey].MessageTC);
                        AlarmList_New.Add("TroubleShootingTC=" + AlarmList_Dictionary[sKey].TroubleShootingTC);
                        AlarmList_New.Add("MessageEN=" + AlarmList_Dictionary[sKey].MessageEN);
                        AlarmList_New.Add("TroubleShootingEN=" + AlarmList_Dictionary[sKey].TroubleShootingEN);
                        AlarmList_New.Add("MessageJP=" + AlarmList_Dictionary[sKey].MessageJP);
                        AlarmList_New.Add("TroubleShootingJP=" + AlarmList_Dictionary[sKey].TroubleShootingJP);
                        AlarmList_New.Add("");
                    }
                }
                System.IO.File.WriteAllLines(AlarmList_INIPath, AlarmList_New, Encoding.Unicode);

                #endregion

                #region//如果是MultiSystem 將INIcopy到 SystemINI內
                if (ArtSystem.MultiSystem.clsMultiSystem.bIsMultiSystem == true)
                {
                    try
                    {
                        string strMultiSystemAlarmINI_Path = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath;
                        strMultiSystemAlarmINI_Path += "\\AlarmList.ini";
                        if (System.IO.File.Exists(strMultiSystemAlarmINI_Path) == true
                            && System.IO.File.Exists(AlarmList_INIPath) == true)
                        {
                            System.IO.File.Copy(AlarmList_INIPath, strMultiSystemAlarmINI_Path, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }
        private void AlarmCodeBuilder_AllPM()
        {
            try
            {
                Dictionary<string, AlarmCodeInfo> AlarmList_Dictionary = new Dictionary<string, AlarmCodeInfo>();
                string AlarmList_INIPath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\AlarmList.ini";
                if (System.IO.File.Exists(AlarmList_INIPath) == true)
                {
                    #region//載入舊的AlarmList.INI
                    string[] AlarmList_ReadAllLines = System.IO.File.ReadAllLines(AlarmList_INIPath);
                    for (int i = 0; i < AlarmList_ReadAllLines.Length; i++)
                    {
                        try
                        {
                            if (AlarmList_ReadAllLines[i].Contains("[") && AlarmList_ReadAllLines[i].Contains("]"))
                            {
                                int AlarmCode = 0;
                                List<string> AlarmData_TempStringList = new List<string>();
                                AlarmData_TempStringList.Add(AlarmList_ReadAllLines[i]);
                                AlarmCode = Convert.ToInt32(AlarmList_ReadAllLines[i].Replace("[", "").Replace("]", ""));
                                i++;
                                for (; i < AlarmList_ReadAllLines.Length; i++)
                                {
                                    if (AlarmList_ReadAllLines[i].Contains("[") && AlarmList_ReadAllLines[i].Contains("]"))
                                    {
                                        i -= 2;
                                        break;
                                    }
                                    AlarmData_TempStringList.Add(AlarmList_ReadAllLines[i]);
                                }
                                AlarmList_Dictionary.Add(AlarmCode.ToString(), new AlarmCodeInfo(AlarmData_TempStringList));
                            }
                        }
                        catch
                        {
                        }
                    }
                    #endregion
                }

                #region//重新加入enuAlarm裡面的定義.
                string strSelectItem = "";
                //if (listBox1.SelectedItem != null)
                for (int iPM = 0; iPM < listBox1.Items.Count; iPM++)
                {
                    strSelectItem = listBox1.Items[iPM].ToString();
                    if (strSelectItem.Contains("]") == true)
                    {
                        strSelectItem = strSelectItem.Split(']')[1];
                        List<clsAlarmName> LstAlarm = new List<clsAlarmName>();
                        if (AutoProcess.m_dctProcData.ContainsKey(strSelectItem) == true)
                        {
                            LstAlarm = AutoProcess.m_dctProcData[strSelectItem].GetAlarmList();
                        }

                        if (clsBaseProc.m_dctProcData.ContainsKey(strSelectItem) == true)
                        {
                            LstAlarm = clsBaseProc.m_dctProcData[strSelectItem].GetAlarmList();
                        }
                        for (int i = 0; i < LstAlarm.Count; i++)
                        {
                            //string sMeg = "";
                            //if (radioButton2.Checked == true)
                            //{ sMeg = "[" + LstAlarm[i].iAlarmCode + "]" + LstAlarm[i].sAlarmName_TC; }
                            //else
                            //{ sMeg = "[" + LstAlarm[i].iAlarmCode + "]" + LstAlarm[i].sAlarmName_EN; }

                            //if (checkedListBox1.Items.Contains(sMeg) == true)
                            {
                                //int index = checkedListBox1.FindString(sMeg, 0);
                                //if (checkedListBox1.GetItemChecked(index) == true)
                                {
                                    try
                                    {
                                        string AlarmCode = LstAlarm[i].iAlarmCode.ToString();
                                        if (AlarmList_Dictionary.Keys.Contains(AlarmCode) == false)
                                        {
                                            AlarmList_Dictionary.Add(AlarmCode, new AlarmCodeInfo(LstAlarm[i].iAlarmCode));
                                        }
                                        AlarmList_Dictionary[AlarmCode].Level = 1;
                                        AlarmList_Dictionary[AlarmCode].MessageEN = LstAlarm[i].sAlarmName_EN;
                                        AlarmList_Dictionary[AlarmCode].TroubleShootingEN = AlarmList_Dictionary[AlarmCode].MessageEN;
                                        AlarmList_Dictionary[AlarmCode].MessageTC = LstAlarm[i].sAlarmName_TC;
                                        AlarmList_Dictionary[AlarmCode].MessageJP = AlarmList_Dictionary[AlarmCode].MessageEN;
                                        AlarmList_Dictionary[AlarmCode].TroubleShootingTC = AlarmList_Dictionary[AlarmCode].MessageEN;
                                        AlarmList_Dictionary[AlarmCode].TroubleShootingJP = AlarmList_Dictionary[AlarmCode].MessageEN;
                                        AlarmList_Dictionary[AlarmCode].Reset = 1;
                                    }
                                    catch (Exception ex)
                                    {
                                        clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region//重新創健一個新的AlarmList.INI
                List<int> LstAlarmCode = new List<int>();
                foreach (string sKey in AlarmList_Dictionary.Keys)
                {
                    if (LstAlarmCode.Contains(AlarmList_Dictionary[sKey].Code) == false)
                    {
                        LstAlarmCode.Add(AlarmList_Dictionary[sKey].Code);
                    }
                }
                LstAlarmCode.Sort();



                List<string> AlarmList_New = new List<string>();
                foreach (int iKey in LstAlarmCode)
                {
                    string sKey = iKey.ToString();
                    if (AlarmList_Dictionary.ContainsKey(sKey) == true)
                    {
                        AlarmList_New.Add("[" + AlarmList_Dictionary[sKey].Code + "]");
                        AlarmList_New.Add("Level=" + AlarmList_Dictionary[sKey].Level);
                        AlarmList_New.Add("Type=" + AlarmList_Dictionary[sKey].Type);
                        AlarmList_New.Add("Reset=" + AlarmList_Dictionary[sKey].Reset);
                        AlarmList_New.Add("Skip=" + AlarmList_Dictionary[sKey].Skip);
                        AlarmList_New.Add("Retry=" + AlarmList_Dictionary[sKey].Retry);
                        AlarmList_New.Add("Continue=" + AlarmList_Dictionary[sKey].Continue);
                        AlarmList_New.Add("MessageTC=" + AlarmList_Dictionary[sKey].MessageTC);
                        AlarmList_New.Add("TroubleShootingTC=" + AlarmList_Dictionary[sKey].TroubleShootingTC);
                        AlarmList_New.Add("MessageEN=" + AlarmList_Dictionary[sKey].MessageEN);
                        AlarmList_New.Add("TroubleShootingEN=" + AlarmList_Dictionary[sKey].TroubleShootingEN);
                        AlarmList_New.Add("MessageJP=" + AlarmList_Dictionary[sKey].MessageJP);
                        AlarmList_New.Add("TroubleShootingJP=" + AlarmList_Dictionary[sKey].TroubleShootingJP);
                        AlarmList_New.Add("");
                    }
                }
                System.IO.File.WriteAllLines(AlarmList_INIPath, AlarmList_New, Encoding.Unicode);

                #endregion

                #region//如果是MultiSystem 將INIcopy到 SystemINI內
                if (ArtSystem.MultiSystem.clsMultiSystem.bIsMultiSystem == true)
                {
                    try
                    {
                        string strMultiSystemAlarmINI_Path = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath;
                        strMultiSystemAlarmINI_Path += "\\AlarmList.ini";
                        if (System.IO.File.Exists(strMultiSystemAlarmINI_Path) == true
                            && System.IO.File.Exists(AlarmList_INIPath) == true)
                        {
                            System.IO.File.Copy(AlarmList_INIPath, strMultiSystemAlarmINI_Path, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }
        #endregion

        #region//===================== 以下為事件處理 =====================


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAlarmList();
        }

        private void btn_ReloadAlarmINIToRichText_Click(object sender, EventArgs e)
        {
            ReloadINI();
        }

        private void btn_WriteAlarmCodeToINI_Click(object sender, EventArgs e)
        {
            AlarmCodeBuilder_SinglePM();
            ReloadINI();
        }

        private void btn_WriteAllPMAlarmCodeToINI_Click(object sender, EventArgs e)
        {
            AlarmCodeBuilder_AllPM();
            ReloadINI();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            UpdateAlarmList();
        }


        private void btn_CheckAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        #endregion
    }
}

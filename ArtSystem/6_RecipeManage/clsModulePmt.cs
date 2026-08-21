using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Threading.Tasks;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtCommunication;
using System.IO;

namespace ArtSystem
{
    public class clsModulePmt
    {
        bool NeedReload = false;
        public string strModuleName = "";

        #region//========== Private Function ==========
        private static readonly System.Text.RegularExpressions.Regex rxNonDigits = new System.Text.RegularExpressions.Regex(@"[^\d]+");
        private Dictionary<string, List<Control>> Lst_Contols = new Dictionary<string, List<Control>>();
        private Dictionary<clsEnum.enuPmtType, Dictionary<string, string>> mDic_Parameter = new Dictionary<clsEnum.enuPmtType, Dictionary<string, string>>();
        private string ConvertValue(string strValue)
        {
            string rValue = "0";
            if (strValue.Contains(".") == true)
            {
                string[] SplitData = strValue.Split('.');
                string NewData1 = rxNonDigits.Replace(SplitData[0], "");
                if (NewData1 == null || NewData1 == "")
                { NewData1 = "0"; }
                string NewData2 = rxNonDigits.Replace(SplitData[1], "");
                if (NewData2 == null || NewData2 == "")
                { NewData2 = "0"; }
                rValue = NewData1 + "." + NewData2;
            }
            else
            {
                rValue = rxNonDigits.Replace(strValue, "");
            }
            return rValue;
        }
        private void UpdateControlValue(Control p_Control)
        {
            if (p_Control.Focused == true)
            {
                try
                {
                    return;
                    //Form mForm = p_Control.FindForm();
                    //if (mForm != null)
                    //{
                    //    p_Control.FindForm().ActiveControl = null;
                    //}
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }
            if (p_Control.Parent != null)
            {
                if (p_Control.Parent.InvokeRequired == true)
                {
                    p_Control.Parent.BeginInvoke(new Action(() =>
                    {
                        UpdateControlValue(p_Control);
                    }));
                }
                else
                {
                    try
                    {
                        if (p_Control is ArtControlLib.comNumBox)
                        {
                            ArtControlLib.comNumBox Item = (ArtControlLib.comNumBox)p_Control;
                            string sPmt = Item.Tag.ToString();
                            string tValue = ConvertValue(Pmt_GetString(sPmt));
                            if (tValue == "")
                            { Item._Value = 0; }
                            else if (Item._Value != Convert.ToDecimal(tValue))
                            { Item._Value = Convert.ToDecimal(tValue); }
                            if (Pmt_GetDecimal(sPmt) < Item._Minimum)
                            {
                                private_PmtSaveString((clsEnum.enuPmtType)Item._PmtType, sPmt, Item._Minimum.ToString()); 
                            }
                            else if (Pmt_GetDecimal(sPmt) > Item._Maximum)
                            {
                                private_PmtSaveString((clsEnum.enuPmtType)Item._PmtType, sPmt, Item._Maximum.ToString()); 
                            }
                        }
                        else if (p_Control is ArtControlLib.comImgButton)
                        {
                            ArtControlLib.comImgButton Item = (ArtControlLib.comImgButton)p_Control;
                            string sPmt = Item.Tag.ToString();
                            string tValue = ConvertValue(Pmt_GetString(sPmt));
                            if (tValue == "")
                            { Item._Status = false; }
                            else if (Item._Status != Convert.ToDecimal(tValue) > 0)
                            { Item._Status = Convert.ToDecimal(tValue) > 0; }
                        }
                        else if (p_Control is ArtControlLib.comSwButton)
                        {
                            ArtControlLib.comSwButton Item = (ArtControlLib.comSwButton)p_Control;
                            string sPmt = Item.Tag.ToString();
                            string tValue = ConvertValue(Pmt_GetString(sPmt));
                            if (tValue == "")
                            { Item._StatusUp = false; }
                            else if (Item._StatusUp != Convert.ToDecimal(tValue) > 0)
                            { Item._StatusUp = Convert.ToDecimal(tValue) > 0; }
                        }
                        else if (p_Control is ArtControlLib.comTextBox)
                        {
                            ArtControlLib.comTextBox Item = (ArtControlLib.comTextBox)p_Control;
                            string sPmt = Item.Tag.ToString();
                            if (Item._Value == null
                                || Item._Value != Pmt_GetString(sPmt))
                            { Item._Value = Pmt_GetString(sPmt); }
                        }
                        else if (p_Control is ArtControlLib.comNumUpDownBox)
                        {
                            ArtControlLib.comNumUpDownBox Item = (ArtControlLib.comNumUpDownBox)p_Control;
                            string sPmt = Item.Tag.ToString();
                            string tValue = ConvertValue(Pmt_GetString(sPmt));
                            if (tValue == "")
                            { Item._Value = 0; }
                            else if (Item._Value != Convert.ToDecimal(tValue))
                            { Item._Value = Convert.ToDecimal(tValue); }
                            if (Pmt_GetDecimal(sPmt) < Item._Minimum)
                            {
                                private_PmtSaveString((clsEnum.enuPmtType)Item._PmtType, sPmt, Item._Minimum.ToString()); 
                            }
                            else if (Pmt_GetDecimal(sPmt) > Item._Maximum)
                            {
                                private_PmtSaveString((clsEnum.enuPmtType)Item._PmtType, sPmt, Item._Maximum.ToString());
                            }
                        }
                        else if (p_Control is ArtControlLib.comNumTrackBar)
                        {
                            ArtControlLib.comNumTrackBar Item = (ArtControlLib.comNumTrackBar)p_Control;
                            for (int i = 0; i < Item.Controls.Count; i++)
                            {
                                if (Item.Controls[i] is TrackBar)
                                {
                                    TrackBar tTrackBar = (TrackBar)Item.Controls[i];
                                    if (tTrackBar.Focused == false)
                                    {
                                        string sPmt = Item.Tag.ToString();
                                        string tValue = ConvertValue(Pmt_GetString(sPmt));
                                        if (tValue == "")
                                        { Item._Value = 0; }
                                        else if (Item._Value != Convert.ToInt32(tValue))
                                        { Item._Value = Convert.ToInt32(tValue); }
                                        if (Pmt_GetDecimal(sPmt) < Item._Minimum)
                                        {
                                            private_PmtSaveString((clsEnum.enuPmtType)Item._PmtType, sPmt, Item._Minimum.ToString());
                                        }
                                        else if (Pmt_GetDecimal(sPmt) > Item._Maximum)
                                        {
                                            private_PmtSaveString((clsEnum.enuPmtType)Item._PmtType, sPmt, Item._Maximum.ToString()); 
                                        }
                                    }
                                }
                            }
                        }
                        else if (p_Control is ArtControlLib.comComboBox)
                        {
                            ArtControlLib.comComboBox Item = (ArtControlLib.comComboBox)p_Control;
                            string sPmt = Item.Tag.ToString();
                            string tValue = ConvertValue(Pmt_GetString(sPmt));
                            if (tValue == "")
                            { Item._Value = 0; }
                            else if (Item._Value != Convert.ToDecimal(tValue))
                            { Item._Value = Convert.ToDecimal(tValue); }
                        }

                    }
                    catch (Exception ex)
                    {
                        clsArtSystem.CatchLog(ex);
                    }
                }
            }
        }
        private void p_Control_Click(object sender, EventArgs e)
        {
            if (sender is ArtControlLib.comImgButton)
            {
                ArtControlLib.comImgButton Item = (ArtControlLib.comImgButton)sender;
                string sPmtName = Item.Tag.ToString();
                clsEnum.enuPmtType pType = clsEnum.enuPmtType.Recipe;
                if (Item._PmtType != null)
                { pType = (clsEnum.enuPmtType)Item._PmtType; }
                if (Item._IsSaveToIni)
                { private_PmtSaveString(pType, sPmtName, Pmt_GetDecimal(sPmtName) > 0 ? "0" : "1"); }
                UpdateControlsValue(sPmtName);
                if (Item._IsSaveToLog)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + "[" + Item.Name + "] Value Changed : " + (!Item._Status).ToString() + "->" + Item._Status.ToString());
                }
            }
            else if (sender is ArtControlLib.comSwButton)
            {
                ArtControlLib.comSwButton Item = (ArtControlLib.comSwButton)sender;
                string sPmtName = Item.Tag.ToString();
                clsEnum.enuPmtType pType = clsEnum.enuPmtType.Recipe;
                if (Item._PmtType != null)
                { pType = (clsEnum.enuPmtType)Item._PmtType; } if (Item._IsSaveToIni)
                if (Item._IsSaveToIni)
                { private_PmtSaveString(pType, sPmtName, Pmt_GetDecimal(sPmtName) > 0 ? "0" : "1"); }
                UpdateControlsValue(sPmtName);
                if (Item._IsSaveToLog)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + "[" + Item.Name + "] Value Changed : " + (!Item._StatusUp).ToString() + "->" + Item._StatusUp.ToString());
                   
                }
            }
        }
        private void Item_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is TrackBar)
            {
                TrackBar tItem = (TrackBar)sender;
                if (tItem.Parent is ArtControlLib.comNumTrackBar)
                {
                    ArtControlLib.comNumTrackBar Item = (ArtControlLib.comNumTrackBar)tItem.Parent;
                    string sPmtName = Item.Tag.ToString();
                    clsEnum.enuPmtType pType = clsEnum.enuPmtType.Recipe;
                    if (Item._PmtType != null)
                    { pType = (clsEnum.enuPmtType)Item._PmtType; }
                    string OrgValue = Pmt_GetString(sPmtName);
                    if (Item._IsSaveToIni)
                    { private_PmtSaveString(pType, sPmtName, Item._Value.ToString()); }
                    UpdateControlsValue(sPmtName);
                    if (Item._IsSaveToLog)
                    {
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + "[" + Item.Name + "] Value Changed : " + OrgValue.ToString() + "->" + Item._Value.ToString());
                    }
                }
            }
        }
        private void p_Control_TextChanged(object sender, EventArgs e)
        {
            if (sender is ArtControlLib.comNumBox)
            {
                ArtControlLib.comNumBox Item = (ArtControlLib.comNumBox)sender;
                string sPmtName = Item.Tag.ToString();
                clsEnum.enuPmtType pType = clsEnum.enuPmtType.Recipe;
                if (Item._PmtType != null)
                { pType = (clsEnum.enuPmtType)Item._PmtType; }
                string OrgValue = Pmt_GetString(sPmtName);
                if (Item._IsSaveToIni)
                { private_PmtSaveString(pType, sPmtName, Item._Value.ToString()); }
                UpdateControlsValue(sPmtName);
                if (Item._IsSaveToLog)
                {
                    if (OrgValue != Item._Value.ToString())
                    {
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + "[" + this.strModuleName + "->" + Item.Name + "] Value Changed : " + OrgValue + "->" + Item._Value.ToString());
                    }
                }
            }
            else if (sender is ArtControlLib.comTextBox)
            {
                ArtControlLib.comTextBox Item = (ArtControlLib.comTextBox)sender;
                string sPmtName = Item.Tag.ToString();
                clsEnum.enuPmtType pType = clsEnum.enuPmtType.Recipe;
                if (Item._PmtType != null)
                { pType = (clsEnum.enuPmtType)Item._PmtType; }
                if (Item._Value == null)
                { Item._Value = ""; }
                string OrgValue = Pmt_GetString(sPmtName);
                if (Item._IsSaveToIni)
                { private_PmtSaveString(pType, sPmtName, Item._Value.ToString()); }
                UpdateControlsValue(sPmtName);
                if (Item._IsSaveToLog)
                {
                    if (OrgValue != Item._Value.ToString())
                    {
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + "[" + this.strModuleName + "->" + Item.Name + "] Value Changed : " + OrgValue.ToString() + "->" + Item._Value.ToString());
                    }
                }
            }
            else if (sender is ArtControlLib.comNumUpDownBox)
            {
                ArtControlLib.comNumUpDownBox Item = (ArtControlLib.comNumUpDownBox)sender;
                string sPmtName = Item.Tag.ToString();
                clsEnum.enuPmtType pType = clsEnum.enuPmtType.Recipe;
                if (Item._PmtType != null)
                { pType = (clsEnum.enuPmtType)Item._PmtType; }
                string OrgValue = Pmt_GetString(sPmtName);
                if (Item._IsSaveToIni)
                { private_PmtSaveString(pType, sPmtName, Item._Value.ToString()); }
                UpdateControlsValue(sPmtName);
                if (Item._IsSaveToLog)
                {
                    if (OrgValue != Item._Value.ToString())
                    {
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + "[" + this.strModuleName + "->" + Item.Name + "] Value Changed : " + OrgValue + "->" + Item._Value.ToString());
                    }
                }
            }
            else if (sender is ArtControlLib.comComboBox)
            {
                ArtControlLib.comComboBox Item = (ArtControlLib.comComboBox)sender;
                string sPmtName = Item.Tag.ToString();
                clsEnum.enuPmtType pType = clsEnum.enuPmtType.Recipe;
                if (Item._PmtType != null)
                { pType = (clsEnum.enuPmtType)Item._PmtType; }
                string OrgValue = Pmt_GetString(sPmtName);
                if (Item._IsSaveToIni)
                { private_PmtSaveString(pType, sPmtName, Item._Value.ToString()); }
                UpdateControlsValue(sPmtName);
                if (Item._IsSaveToLog)
                {
                    if (OrgValue != Item._Value.ToString())
                    {
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + "[" + this.strModuleName + "->" + Item.Name + "] Value Changed : " + OrgValue + "->" + Item._Value.ToString());
                    }
                }
            }
        }
        private void clsModulePmt_PathChanged(ucParameter.PathEventArgs e)
        {
            ReloadPmt();
        }
        private void private_PmtSaveString(clsEnum.enuPmtType pPmtType, string PmtName, string PmtValue)
        {
            try
            {
                string sPath = ucParameter.GetFilePath(pPmtType);
                clsIniFile mFile = new clsIniFile(sPath);
                mFile.WriteValue(strModuleName, PmtName, PmtValue);
                if (mDic_Parameter.ContainsKey(pPmtType) == false)
                {
                    mDic_Parameter.Add(pPmtType, new Dictionary<string, string>());
                }
                if (mDic_Parameter[pPmtType].ContainsKey(PmtName) == true)
                {
                    mDic_Parameter[pPmtType][PmtName] = PmtValue;
                }
                else
                {
                    mDic_Parameter[pPmtType].Add(PmtName, PmtValue);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region//========== Public Function ==========
        public clsModulePmt(string sModuleName)
        {
            strModuleName = sModuleName;
            ucParameter.GetSingleton().PathChanged += new ucParameter.eventPathChanged(clsModulePmt_PathChanged);
            ReloadPmt();
        }
        public void ReloadPmt()
        {
            foreach (clsEnum.enuPmtType eType in Enum.GetValues(typeof(clsEnum.enuPmtType)))
            {
                if (mDic_Parameter.ContainsKey(eType) == false)
                { mDic_Parameter.Add(eType, new Dictionary<string, string>()); }
                if (System.IO.File.Exists(ucParameter.GetFilePath(eType)) == true)
                {
                    clsIniFile mFile = new clsIniFile(ucParameter.GetFilePath(eType), false);
                    Dictionary<string, string> astrKey = mFile.GetSectionValuesAsDictionary(strModuleName.ToString());
                    mDic_Parameter[eType].Clear();
                    foreach (string strName in astrKey.Keys)
                    {
                        try
                        {
                            if (mDic_Parameter[eType].ContainsKey(strName) == false)
                            {
                                mDic_Parameter[eType].Add(strName, mFile.GetString(strModuleName, strName, ""));
                            }
                            else
                            {
                                mDic_Parameter[eType][strName] = mFile.GetString(strModuleName, strName, "");
                            }
                        }
                        catch (Exception ex)
                        {
                            clsArtSystem.CatchLog(ex);
                        }
                    }
                }
            }
            UpdateControlsValue();
            NeedReload = false;
        }
        public string Pmt_GetString(string PmtName)
        {
            string rValue = "";
            string sPmtName = PmtName;
            if (NeedReload == true)
            { ReloadPmt(); }
            foreach (clsEnum.enuPmtType eType in Enum.GetValues(typeof(clsEnum.enuPmtType)))
            {
                if (mDic_Parameter[eType].ContainsKey(sPmtName) == true)
                {
                    rValue = mDic_Parameter[eType][sPmtName];
                    break;
                }
            }
            return rValue;
        }
        public decimal Pmt_GetDecimal(string PmtName)
        {
            decimal rValue = 0;
            string sValue = ConvertValue(Pmt_GetString(PmtName));
            if (sValue.Length > 0)
            {
                rValue = Convert.ToDecimal(sValue);
            }
            return rValue;
        }
        public void Pmt_SaveString(clsEnum.enuPmtType pPmtType, string PmtName, string PmtValue)
        {
            private_PmtSaveString(pPmtType, PmtName, PmtValue);
            UpdateControlsValue(PmtName);
        }
        public void CollectPmtControl(Control p_Control, string sPmtName = "")
        {
            try
            {
                if (p_Control is ArtControlLib.comNumBox
                    || p_Control is ArtControlLib.comImgButton
                    || p_Control is ArtControlLib.comSwButton
                    || p_Control is ArtControlLib.comTextBox
                    || p_Control is ArtControlLib.comNumUpDownBox
                    || p_Control is ArtControlLib.comNumTrackBar
                    || p_Control is ArtControlLib.comComboBox)
                {
                    if (p_Control.Parent != null)
                    {
                        if (Lst_Contols.ContainsKey(sPmtName) == false)
                        {  Lst_Contols.Add(sPmtName, new List<Control>()); }
                        Lst_Contols[sPmtName].Add(p_Control);
                        if (sPmtName != "")
                        {
                            p_Control.Tag = sPmtName;
                        }
                        if (p_Control is ArtControlLib.comNumBox
                          || p_Control is ArtControlLib.comTextBox
                          || p_Control is ArtControlLib.comNumUpDownBox)
                        {
                            p_Control.TextChanged -= new EventHandler(p_Control_TextChanged);
                        }
                        else if (p_Control is ArtControlLib.comComboBox)
                        {
                            ArtControlLib.comComboBox Item = (ArtControlLib.comComboBox)p_Control;
                            Item.SelectedIndexChanged -= new EventHandler(p_Control_TextChanged);
                        }
                        else if (p_Control is ArtControlLib.comImgButton
                            || p_Control is ArtControlLib.comSwButton)
                        {

                            p_Control.Click -= new EventHandler(p_Control_Click);
                        }
                        else if (p_Control is ArtControlLib.comNumTrackBar)
                        {
                            ArtControlLib.comNumTrackBar Item = (ArtControlLib.comNumTrackBar)p_Control;
                            for (int i = 0; i < Item.Controls.Count; i++)
                            {
                                if (Item.Controls[i] is TrackBar)
                                {
                                    TrackBar tTrackBar = (TrackBar)Item.Controls[i];
                                    tTrackBar.MouseUp -= new MouseEventHandler(Item_MouseUp);
                                }
                            }
                        }
                        UpdateControlValue(p_Control);
                        if (p_Control is ArtControlLib.comNumBox
                          || p_Control is ArtControlLib.comTextBox
                          || p_Control is ArtControlLib.comNumUpDownBox)
                        {
                            p_Control.TextChanged += new EventHandler(p_Control_TextChanged);
                        }
                        else if (p_Control is ArtControlLib.comComboBox)
                        {
                            ArtControlLib.comComboBox Item = (ArtControlLib.comComboBox)p_Control;
                            Item.SelectedIndexChanged += new EventHandler(p_Control_TextChanged);
                        }
                        else if (p_Control is ArtControlLib.comImgButton
                            || p_Control is ArtControlLib.comSwButton)
                        {
                            p_Control.Click += new EventHandler(p_Control_Click);
                        }
                        else if (p_Control is ArtControlLib.comNumTrackBar)
                        {
                            ArtControlLib.comNumTrackBar Item = (ArtControlLib.comNumTrackBar)p_Control;
                            for (int i = 0; i < Item.Controls.Count; i++)
                            {
                                if (Item.Controls[i] is TrackBar)
                                {
                                    TrackBar tTrackBar = (TrackBar)Item.Controls[i];
                                    tTrackBar.MouseUp += new MouseEventHandler(Item_MouseUp);
                                }
                            }
                        }
                    }

                }
                else
                {
                    foreach (Control CtrlProc in p_Control.Controls)
                    {
                        if (CtrlProc is Form
                            || CtrlProc is Panel
                            || CtrlProc is FlowLayoutPanel
                            || CtrlProc is GroupBox
                            || CtrlProc is SplitContainer
                            || CtrlProc is SplitterPanel
                            || CtrlProc is TableLayoutPanel
                            || CtrlProc is TabControl
                            || CtrlProc is TabPage
                            || CtrlProc is ucBaseUserControl)
                        {
                            //如果是容器，再深入收集                    
                            CollectPmtControl(CtrlProc);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        public void UpdateControlsValue()
        {
            foreach (string sPmtName in Lst_Contols.Keys)
            {
                for (int i = 0; i < Lst_Contols[sPmtName].Count; i++)
                {
                    UpdateControlValue(Lst_Contols[sPmtName][i]);
                }
            }
        }
        public void UpdateControlsValue(string sPmtName)
        {
            if (Lst_Contols.ContainsKey(sPmtName) == true)
            {
                for (int i = 0; i < Lst_Contols[sPmtName].Count; i++)
                {
                    UpdateControlValue(Lst_Contols[sPmtName][i]);
                }
            }
        }
        #endregion

    }
}
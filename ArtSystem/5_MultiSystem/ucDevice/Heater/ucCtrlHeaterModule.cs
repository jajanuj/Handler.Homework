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
    public partial class ucCtrlHeaterModule : ucBaseUserControl2
    {
        #region //=====================  區域變數設置 =====================
        public Dictionary<clsPmtHeaterModule.enuPmtName, string> pPmt = null;
        public clsCtrlHeaterModule pCtrlHeaterModule = null;
        private clsEnum.enuPmtName? ePmtShifOffset = null;
        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucCtrlHeaterModule()
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
                    labName.Text = clsLanguage.GetTranslation(pPmt[clsPmtHeaterModule.enuPmtName.sControllerName], false);
                    if (Enum.IsDefined(typeof(clsEnum.enuPmtName), pPmt[clsPmtHeaterModule.enuPmtName.enuSys_Temp_ShiftOffset]) == true)
                    {
                        ePmtShifOffset = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), pPmt[clsPmtHeaterModule.enuPmtName.enuSys_Temp_ShiftOffset]);
                    }
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
                if (pCtrlHeaterModule != null)
                {
                    labName.Text = clsLanguage.GetTranslation(pPmt[clsPmtHeaterModule.enuPmtName.sControllerName], false);
                    labStation.Text = pCtrlHeaterModule.m_CtrlHeater.m_StationNumber.ToString();
                    txt_CurrentTemp.Text = pCtrlHeaterModule.GetCurrentTemp().ToString("F1");
                    txt_TargetTemp.Text = pCtrlHeaterModule.m_CtrlHeater.m_dSettingTemp.ToString("F1");
                    txt_LimitTemp.Text = pPmt[clsPmtHeaterModule.enuPmtName.Temp_Limit];
                    txt_ErrorRange.Text = pPmt[clsPmtHeaterModule.enuPmtName.Temp_ErrorRange];
                    if (ePmtShifOffset != null)
                    {
                        txt_ShiftOffset.Text = ucParameter.GetValueDouble((clsEnum.enuPmtName)ePmtShifOffset).ToString();
                    }

                    if (pCtrlHeaterModule.IsConnected() == true)
                    {
                        btnHeaterModule_Connect.BackColor = Color.Lime;
                    }
                    else if (pCtrlHeaterModule.IsOpen() == true)
                    {
                        btnHeaterModule_Connect.BackColor = Color.Orange;
                    }
                    else
                    {
                        btnHeaterModule_Connect.BackColor = SystemColors.Control;
                    }
                    if(pCtrlHeaterModule.GetOnOff() == true)
                    {
                        btnHeaterModule_OnOff.Text = clsLanguage.GetTranslation("On", false);
                    }
                    else
                    {
                        btnHeaterModule_OnOff.Text = clsLanguage.GetTranslation("Off", false);
                    }
                    clsCtrlHeaterModule.enuHeaterStatus eNowStatus = pCtrlHeaterModule.GetStatus();
                    if (DateTime.Now.Second % 2 == 1 && eNowStatus != clsCtrlHeaterModule.enuHeaterStatus.Ready)
                    {
                        labName.Text = clsLanguage.GetTranslation(eNowStatus.ToString(), false);
                    }
                    switch (pCtrlHeaterModule.GetStatus())
                    {
                        case clsCtrlHeaterModule.enuHeaterStatus.Heating:
                        case clsCtrlHeaterModule.enuHeaterStatus.Cooling:
                            ucRoundButton1._Color = Color.Orange;
                            break;
                        case clsCtrlHeaterModule.enuHeaterStatus.Ready:
                            ucRoundButton1._Color = Color.Lime;
                            break;
                        case clsCtrlHeaterModule.enuHeaterStatus.OverHeat:
                            ucRoundButton1._Color = Color.Red;
                            break;
                        case clsCtrlHeaterModule.enuHeaterStatus.OverHeat_DI:
                            ucRoundButton1._Color = Color.Red;
                            break;
                        case clsCtrlHeaterModule.enuHeaterStatus.Off:
                            ucRoundButton1._Color = SystemColors.Control;
                            break;
                        default:
                            ucRoundButton1._Color = SystemColors.Control;
                            break;
                    }
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

        private void ucCtrlHeaterModule_VisibleChanged(object sender, EventArgs e)
        {
            this.SetReflashTimerStart(this.Visible);
        }

        private void btnHeaterModule_Connect_Click(object sender, EventArgs e)
        {
            if (pCtrlHeaterModule != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pCtrlHeaterModule.m_strMoudleName + " -> " + ((Control)sender).Name + ", Clicked");
                pCtrlHeaterModule.Open();
            }
        }
        private void btnHeaterModule_Disconnect_Click(object sender, EventArgs e)
        {
            if (pCtrlHeaterModule != null)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pCtrlHeaterModule.m_strMoudleName + " -> " + ((Control)sender).Name + ", Clicked");
                pCtrlHeaterModule.Close();
            }
        }
        private void btnHeaterModule_OnOff_Click(object sender, EventArgs e)
        {
            if (pCtrlHeaterModule != null)
            {
                if (pCtrlHeaterModule.GetOnOff() == true)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pCtrlHeaterModule.m_strMoudleName + " -> " + ((Control)sender).Name + ", Clicked - Off");
                    pCtrlHeaterModule.SetOnOff(clsCtrlHeaterModule.enuSwitch.Off);
                }
                else
                {
                    if (Enum.IsDefined(typeof(clsEnum.enuPmtName), pPmt[clsPmtHeaterModule.enuPmtName.enuSys_Temp_ShiftOffset]) == true)
                    {
                        clsEnum.enuPmtName ePmtName = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), pPmt[clsPmtHeaterModule.enuPmtName.enuSys_Temp_ShiftOffset]);
                        double dShiftOffset = ucParameter.GetValueDouble(ePmtName);
                        pCtrlHeaterModule.SetTempShift(dShiftOffset);
                    }
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pCtrlHeaterModule.m_strMoudleName + " -> " + ((Control)sender).Name + ", Clicked - On");
                    pCtrlHeaterModule.SetOnOff(clsCtrlHeaterModule.enuSwitch.On);
                }
            }
        }

        private void txt_TargetTemp_Click(object sender, EventArgs e)
        {
            if (pCtrlHeaterModule != null)
            {
                int iMaxTemp = 150;
                int.TryParse(pPmt[clsPmtHeaterModule.enuPmtName.Temp_Limit], out iMaxTemp);
                string strSettingTemp = pCtrlHeaterModule.m_CtrlHeater.m_dSettingTemp.ToString();
                if (FormNumBox.GetSingleton().ShowDialog(this, strSettingTemp, iMaxTemp, 0, 1) == DialogResult.OK)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pCtrlHeaterModule.m_strMoudleName + " -> " + ((Control)sender).Name
                        + ", Temp_Target : " + strSettingTemp + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                    pCtrlHeaterModule.SetTemp(FormNumBox.GetSingleton().NumBoxValue);
                }
            }
        }
        private void txt_LimitTemp_Click(object sender, EventArgs e)
        {
            if (pCtrlHeaterModule != null)
            {
                if (FormNumBox.GetSingleton().ShowDialog(this, pPmt[clsPmtHeaterModule.enuPmtName.Temp_Limit], 999, 0, 1) == DialogResult.OK)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pCtrlHeaterModule.m_strMoudleName + " -> " + ((Control)sender).Name
                        + ", Temp_Limit : " + pPmt[clsPmtHeaterModule.enuPmtName.Temp_Limit] + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                    pPmt[clsPmtHeaterModule.enuPmtName.Temp_Limit] = FormNumBox.GetSingleton().NumBoxValue.ToString();
                    ucHeaterModuleSetting.GetSingleton().mPmt.Save(ucHeaterModuleSetting.GetSingleton().mPmt.sINIPath);
                }
            }
        }
        private void txt_ErrorRange_Click(object sender, EventArgs e)
        {
            if (pCtrlHeaterModule != null)
            {
                if (FormNumBox.GetSingleton().ShowDialog(this, pPmt[clsPmtHeaterModule.enuPmtName.Temp_ErrorRange], 100, 0, 1) == DialogResult.OK)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pCtrlHeaterModule.m_strMoudleName + " -> " + ((Control)sender).Name
                        + ", Temp_ErrorRange : " + pPmt[clsPmtHeaterModule.enuPmtName.Temp_ErrorRange] + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                    pPmt[clsPmtHeaterModule.enuPmtName.Temp_ErrorRange] = FormNumBox.GetSingleton().NumBoxValue.ToString();
                    ucHeaterModuleSetting.GetSingleton().mPmt.Save(ucHeaterModuleSetting.GetSingleton().mPmt.sINIPath);
                }
            }
        }
        private void txt_ShiftOffset_Click(object sender, EventArgs e)
        {
            if (pCtrlHeaterModule != null)
            {
                string sSource = txt_ShiftOffset.Text;
                if (FormNumBox.GetSingleton().ShowDialog(this, sSource, 100, 0, 3) == DialogResult.OK)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pCtrlHeaterModule.m_strMoudleName + " -> " + ((Control)sender).Name
                        + ", Temp_SiftOffset : " + sSource + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                    ucHeaterModuleSetting.GetSingleton().mPmt.Save(ucHeaterModuleSetting.GetSingleton().mPmt.sINIPath);
                    pCtrlHeaterModule.SetTempShift(FormNumBox.GetSingleton().NumBoxValue);
                    txt_ShiftOffset.Text = FormNumBox.GetSingleton().NumBoxValue.ToString();
                    if (ePmtShifOffset != null)
                    {
                        ucParameter.SaveValue(clsEnum.enuPmtType.System, (clsEnum.enuPmtName)ePmtShifOffset, (decimal)FormNumBox.GetSingleton().NumBoxValue);
                    }
                }
            }
        }

        #endregion

    }
}

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
    public partial class ucCtrlRollerMotor : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public Dictionary<clsPmtRollerMotor.enuPmtName, string> pPmt = null;
        public clsCtrlRollerMotor pMotionRoller = null;
        #endregion

        #region //===================== Event Define =====================
        /// <summary> TCPIP Message Arrivel 事件</summary>
        private event ValueChangeEvent m_ValueChangeEvent = null;
        public delegate void ValueChangeEvent(string sSenderName, string PmtName, string OrgValue, string NewValue);
        /// <summary> 接收到訊息事件 </summary>
        public event ValueChangeEvent _MessageArrivelEvent
        {
            remove
            {
                m_ValueChangeEvent -= value;
            }
            add
            {
                m_ValueChangeEvent += value;
            }
        }
        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucCtrlRollerMotor()
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
                    panel_DO.Visible = pPmt[clsPmtRollerMotor.enuPmtName.RollerType] == clsPmtRollerMotor.enuRollerType.DIOControl_Only.ToString();
                    rBtn_Start.Text = ToString(pMotionRoller.eDO_Start);
                    rBtn_Slow.Text = ToString(pMotionRoller.eDO_Slow);
                    rBtn_Reverse.Text = ToString(pMotionRoller.eDO_Reverse);
                    rBtn_Start.Visible = pMotionRoller.eDO_Start != null;
                    rBtn_Slow.Visible = pMotionRoller.eDO_Slow != null;
                    rBtn_Reverse.Visible = pMotionRoller.eDO_Reverse != null;
                    labStation.Text = pPmt[clsPmtRollerMotor.enuPmtName.Motor_ID];
                    labName.Text = clsLanguage.GetTranslation(pPmt[clsPmtRollerMotor.enuPmtName.RollerName]);
                    pPmt[clsPmtRollerMotor.enuPmtName.HighSpeed_pps] = SetNumValue(numHighSpeed, Convert.ToDecimal(pPmt[clsPmtRollerMotor.enuPmtName.HighSpeed_pps])).ToString();
                    pPmt[clsPmtRollerMotor.enuPmtName.LowSpeed_pps] = SetNumValue(numLowSpeed, Convert.ToDecimal(pPmt[clsPmtRollerMotor.enuPmtName.LowSpeed_pps])).ToString();
                    string sEnumPmtLowSpeed = pPmt[clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed];
                    if (Enum.IsDefined(typeof(clsEnum.enuPmtName), sEnumPmtLowSpeed) == true)
                    {
                        clsEnum.enuPmtName ePmt = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), sEnumPmtLowSpeed);
                        if (ucParameter.GetValueString(ePmt) == "")
                        { ucParameter.SaveValue(clsEnum.enuPmtType.System, ePmt, pPmt[clsPmtRollerMotor.enuPmtName.LowSpeed_pps]); }
                        SetNumValue(numLowSpeed, Convert.ToDecimal(ucParameter.GetValueString(ePmt))).ToString();
                    }
                    string sEnumPmtHighSpeed = pPmt[clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed];
                    if (Enum.IsDefined(typeof(clsEnum.enuPmtName), sEnumPmtHighSpeed) == true)
                    {
                        clsEnum.enuPmtName ePmt = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), sEnumPmtHighSpeed);
                        if (ucParameter.GetValueString(ePmt) == "")
                        { ucParameter.SaveValue(clsEnum.enuPmtType.System, ePmt, pPmt[clsPmtRollerMotor.enuPmtName.HighSpeed_pps]); }
                        SetNumValue(numHighSpeed, Convert.ToDecimal(ucParameter.GetValueString(ePmt))).ToString();
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
                if (pMotionRoller.eDO_Start != null)
                {
                    rBtn_Start.Checked = clsDioCtrl.GetDo((clsEnum.enuDo)pMotionRoller.eDO_Start);
                }
                if (pMotionRoller.eDO_Slow != null)
                {
                    rBtn_Slow.Checked = clsDioCtrl.GetDo((clsEnum.enuDo)pMotionRoller.eDO_Slow);
                }
                if (pMotionRoller.eDO_Reverse != null)
                {
                    rBtn_Reverse.Checked = clsDioCtrl.GetDo((clsEnum.enuDo)pMotionRoller.eDO_Reverse);
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

        private string ToString(clsEnum.enuDo? sender)
        {
            string rValue = "";
            if (sender != null)
            {
                int iDO = (int)sender;
                rValue = "[DO" + iDO.ToString("000") + "] " + sender.ToString();
            }
            return rValue;
        }
        #endregion

        #region//===================== 以下為事件處理 () =====================

        private void ucCtrlRollerMotor_VisibleChanged(object sender, EventArgs e)
        {
            this.SetReflashTimerStart(this.Visible);
        }
        private void btnKeepMove_N_MouseDown(object sender, MouseEventArgs e)
        {
            if (pMotionRoller != null)
            {
                if (cBox_Slow.Checked == true)
                {
                    pMotionRoller.RunSlow(enuMoveDir.Negative);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pMotionRoller.sName + " -> " + ((Control)sender).Name
                            + ",[" + pMotionRoller.sName + "] Low Speed Move(N) Click.");
                }
                else
                {
                    pMotionRoller.RunFast(enuMoveDir.Negative);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pMotionRoller.sName + " -> " + ((Control)sender).Name
                            + ",[" + pMotionRoller.sName + "]  High Speed Move(N) Click.");
                }
            }
        }
        private void btnKeepMove_P_MouseDown(object sender, MouseEventArgs e)
        {
            if (pMotionRoller != null)
            {
                if (cBox_Slow.Checked == true)
                {
                    pMotionRoller.RunSlow(enuMoveDir.Positive);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pMotionRoller.sName + " -> " + ((Control)sender).Name
                            + ",[" + pMotionRoller.sName + "] Low Speed Move(P) Click.");
                }
                else
                {
                    pMotionRoller.RunFast(enuMoveDir.Positive);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pMotionRoller.sName + " -> " + ((Control)sender).Name
                            + ",[" + pMotionRoller.sName + "]  High Speed Move(P) Click.");
                }
            }
        }
        private void btnKeepMove_N_MouseUp(object sender, MouseEventArgs e)
        {
            if (cBox_KeepMove.Checked == true)
            { return; }
            if (pMotionRoller != null)
            {
                pMotionRoller.StopRun();
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " +  pMotionRoller.sName + " -> " + ((Control)sender).Name
                        + ",[" + pMotionRoller.sName + "] Mouse Up Stop.");
            }
        }
        private void btnKeepMove_P_MouseUp(object sender, MouseEventArgs e)
        {
            if (cBox_KeepMove.Checked == true)
            { return; }
            if (pMotionRoller != null)
            {
                pMotionRoller.StopRun();
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pMotionRoller.sName + " -> " + ((Control)sender).Name
                        + ",[" + pMotionRoller.sName + "] Mouse Up Stop.");
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (pMotionRoller != null)
            {
                pMotionRoller.StopRun();
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pMotionRoller.sName + " -> " + ((Control)sender).Name
                        + ",[" + pMotionRoller.sName + "] Stop Run Click.");
            }
        }

        #endregion

        #region//===================== 以下為事件處理 (Control Roller Motor) =====================

        private void numLowSpeed_Click(object sender, EventArgs e)
        {
            try
            {
                if (pPmt != null)
                {
                    if (pPmt.ContainsKey(clsPmtRollerMotor.enuPmtName.LowSpeed_pps) == true)
                    {
                        if (FormNumBox.GetSingleton().ShowDialog(this, pPmt[clsPmtRollerMotor.enuPmtName.LowSpeed_pps], 99999, 1, 0) == DialogResult.OK)
                        {
                            string OrgValue = pPmt[clsPmtRollerMotor.enuPmtName.LowSpeed_pps];
                            string sPath = ucRollerMotorSetting.GetSingleton().mPmt.sINIPath;
                           
                            string sEnumPmtLowSpeed = pPmt[clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed];
                            if (Enum.IsDefined(typeof(clsEnum.enuPmtName), sEnumPmtLowSpeed) == true)
                            {
                                clsEnum.enuPmtName ePmt = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), sEnumPmtLowSpeed);
                                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pMotionRoller.sName + " -> " + ((Control)sender).Name
                                       + ", Pmt Name : " + sEnumPmtLowSpeed
                                       + ", Change Low Speed(pps) : " + ucParameter.GetValueDouble(ePmt) + "-> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                                ucParameter.SaveValue(clsEnum.enuPmtType.System, ePmt, (decimal)FormNumBox.GetSingleton().NumBoxValue);
                            }
                            else if (System.IO.File.Exists(sPath) == true)
                            {
                                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pMotionRoller.sName + " -> " + ((Control)sender).Name
                                       + ", Roller Motor Name : " + pPmt[clsPmtRollerMotor.enuPmtName.RollerName]
                                       + ", Change Low Speed(pps) : " + pPmt[clsPmtRollerMotor.enuPmtName.LowSpeed_pps] + "-> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                                pPmt[clsPmtRollerMotor.enuPmtName.LowSpeed_pps] = FormNumBox.GetSingleton().NumBoxValue.ToString();
                                clsIniFile mFile = new clsIniFile(sPath);
                                mFile.WriteValue(pPmt[clsPmtRollerMotor.enuPmtName.RollerName], clsPmtRollerMotor.enuPmtName.LowSpeed_pps.ToString(),
                                    pPmt[clsPmtRollerMotor.enuPmtName.LowSpeed_pps]);
                                pPmt[clsPmtRollerMotor.enuPmtName.LowSpeed_pps] = SetNumValue(numLowSpeed, Convert.ToDecimal(pPmt[clsPmtRollerMotor.enuPmtName.LowSpeed_pps])).ToString();
                            }
                            if (m_ValueChangeEvent != null)
                            {
                                m_ValueChangeEvent(pPmt[clsPmtRollerMotor.enuPmtName.RollerName], "HighSpeed_pps", OrgValue, FormNumBox.GetSingleton().NumBoxValue.ToString());
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
        private void numHighSpeed_Click(object sender, EventArgs e)
        {
            try
            {
                if (pPmt != null)
                {
                    if (pPmt.ContainsKey(clsPmtRollerMotor.enuPmtName.HighSpeed_pps) == true)
                    {
                        if (FormNumBox.GetSingleton().ShowDialog(this, pPmt[clsPmtRollerMotor.enuPmtName.HighSpeed_pps], 99999, 1, 0) == DialogResult.OK)
                        {
                            string OrgValue = pPmt[clsPmtRollerMotor.enuPmtName.HighSpeed_pps];
                            string sPath = ucRollerMotorSetting.GetSingleton().mPmt.sINIPath;
                            string sEnumPmtHighSpeed = pPmt[clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed];
                            if (Enum.IsDefined(typeof(clsEnum.enuPmtName), sEnumPmtHighSpeed) == true)
                            {
                                clsEnum.enuPmtName ePmt = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), sEnumPmtHighSpeed);
                                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pMotionRoller.sName + " -> " + ((Control)sender).Name
                                       + ", Pmt Name : " + sEnumPmtHighSpeed
                                       + ", Change High Speed(pps) : " + ucParameter.GetValueDouble(ePmt) + "-> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                                ucParameter.SaveValue(clsEnum.enuPmtType.System, ePmt, (decimal)FormNumBox.GetSingleton().NumBoxValue);
                            }
                            else if (System.IO.File.Exists(sPath) == true)
                            {
                                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pMotionRoller.sName + " -> " + ((Control)sender).Name
                                       + ", Roller Motor Name : " + pPmt[clsPmtRollerMotor.enuPmtName.RollerName]
                                       + ", Change High Speed(pps) : " + pPmt[clsPmtRollerMotor.enuPmtName.HighSpeed_pps] + "-> " + FormNumBox.GetSingleton().NumBoxValue.ToString());
                                pPmt[clsPmtRollerMotor.enuPmtName.HighSpeed_pps] = FormNumBox.GetSingleton().NumBoxValue.ToString();
                                clsIniFile mFile = new clsIniFile(sPath);
                                mFile.WriteValue(pPmt[clsPmtRollerMotor.enuPmtName.RollerName], clsPmtRollerMotor.enuPmtName.HighSpeed_pps.ToString(),
                                    pPmt[clsPmtRollerMotor.enuPmtName.HighSpeed_pps]);
                                pPmt[clsPmtRollerMotor.enuPmtName.HighSpeed_pps] = SetNumValue(numHighSpeed, Convert.ToDecimal(pPmt[clsPmtRollerMotor.enuPmtName.HighSpeed_pps])).ToString();

                            }
                            if (m_ValueChangeEvent != null)
                            {
                                m_ValueChangeEvent(pPmt[clsPmtRollerMotor.enuPmtName.RollerName], "HighSpeed_pps", OrgValue, FormNumBox.GetSingleton().NumBoxValue.ToString());
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
        private void rBtn_Start_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void rBtn_Start_MouseUp(object sender, MouseEventArgs e)
        {
            if (pMotionRoller.eDO_Start != null)
            {
                bool SetDoState = clsDioCtrl.GetDo((clsEnum.enuDo)pMotionRoller.eDO_Start) == false;
                clsDioCtrl.SetDo((clsEnum.enuDo)pMotionRoller.eDO_Start, SetDoState);
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pMotionRoller.sName + " -> " + ((Control)sender).Name
                        + ",[" + pMotionRoller.sName + "] Set DO (" + SetDoState + "): " + pMotionRoller.eDO_Start.ToString());
            }
        }
        private void rBtn_Slow_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void rBtn_Slow_MouseUp(object sender, MouseEventArgs e)
        {
            if (pMotionRoller.eDO_Slow != null)
            {
                bool SetDoState = clsDioCtrl.GetDo((clsEnum.enuDo)pMotionRoller.eDO_Slow) == false;
                clsDioCtrl.SetDo((clsEnum.enuDo)pMotionRoller.eDO_Slow, SetDoState);
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pMotionRoller.sName + " -> " + ((Control)sender).Name
                        + ",[" + pMotionRoller.sName + "] Set DO (" + SetDoState + "): " + pMotionRoller.eDO_Slow.ToString());
            }
        }
        private void rBtn_Reverse_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void rBtn_Reverse_MouseUp(object sender, MouseEventArgs e)
        {
            if (pMotionRoller.eDO_Reverse != null)
            {
                bool SetDoState = clsDioCtrl.GetDo((clsEnum.enuDo)pMotionRoller.eDO_Reverse) == false;
                clsDioCtrl.SetDo((clsEnum.enuDo)pMotionRoller.eDO_Reverse, SetDoState);
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + pMotionRoller.sName + " -> " + ((Control)sender).Name
                        + ",[" + pMotionRoller.sName + "] Set DO (" + SetDoState + "): " + pMotionRoller.eDO_Reverse.ToString());
            }
        }
        private void rBtn_Start_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay = 3000;
            toolTip1.SetToolTip((Control)sender, ToString(pMotionRoller.eDO_Start));
        }
        private void rBtn_Slow_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay = 3000;
            toolTip1.SetToolTip((Control)sender, ToString(pMotionRoller.eDO_Slow));
        }
        private void rBtn_Reverse_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay = 3000;
            toolTip1.SetToolTip((Control)sender, ToString(pMotionRoller.eDO_Reverse));
        }
        #endregion
    }
}

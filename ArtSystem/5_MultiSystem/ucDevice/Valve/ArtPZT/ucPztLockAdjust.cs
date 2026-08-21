using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtData;
using ArtCommonLib;
using ArtControlLib;
using ArtSystem;

namespace ArtSystem.MultiSystem
{
    public partial class ucPztLockAddjust : UserControl
    {
        #region//========== 參數 ==========
        public clsPmtValveLockAdjust g_PmtLockAdjust = new clsPmtValveLockAdjust();
        public clsResultValveLockAdjust g_ResultLockAdjust = new clsResultValveLockAdjust();
        #endregion

        public class clsPmtValveLockAdjust
        {
            public int g_iTriggerNum = 100;
            public double g_dMaxValue = 260;
            public double g_dMinValue = 190;
            public double g_dSlopSpec = 35;
            public double g_dVSpecPrecentage = 25;
            public double g_dLowerPointValue = -0.0008;
            public int g_iLockAdjustTotalCount = 4;
            /// <summary> 誤差範圍 </summary>
            public List<double> g_LstLockErrorRange = new List<double>();
            public clsPmtValveLockAdjust()
            {
                g_LstLockErrorRange.Clear();
                for (int i = g_LstLockErrorRange.Count; i < g_iLockAdjustTotalCount - 1; i++)
                { g_LstLockErrorRange.Add(5); }
            }
        }

        public class clsResultValveLockAdjust
        {
            ///// <summary>當前閥名稱</summary>
            public string strValveName = "";
            /// <summary>當前校正結果</summary>
            public bool? bAdjustResult = null;
            /// <summary>當前校正數值</summary>
            public List<double> lstAdjustValue = new List<double>();
        }
        #region//========== 變數 ==========
        clsCtrlDispValve_ArtPZT m_CtrlDispValve = null;
        public ucPZTCalibChart m_ucPZTCalibChart = null;
        clsHiPerfTimer mTimer = new clsHiPerfTimer();
        static private object objLockPZT = new object();

        private int m_iStepIndex = 0;
        private int m_iLockAdjustTotalCount = 4;
        private int m_iAdjustIndex = 0;

        /// <summary> 閉鎖校正數值列表</summary>
        public List<double> m_LstLockValue = new List<double>();
        /// <summary> 校正異常訊息 </summary>
        public string m_sErrorMessage = "";
        /// <summary> 進行到第幾輪</summary>
        public int m_iProcessIndex = 0;
        /// <summary> 是否由多閥校正模組呼叫</summary>
        public bool m_bIsMultiAdjust = false;
        /// <summary> 當前校正進度%數 </summary>
        public int m_iSchedule = 0;
        /// <summary> 第一次校正是否Pass</summary>
        public string strPass1 = "";
        /// <summary> 第二次校正是否Pass</summary>
        public string strPass2 = "";
        /// <summary> 第三次校正是否Pass</summary>
        public string strPass3 = "";
        #endregion

        #region //========== 必要函式 ==========

        static private object objLock = new object();
        private static ucPztLockAddjust m_Singleton;
        public System.Timers.Timer m_Timer = new System.Timers.Timer(100); // 100ms

        //public static ucPztLockAddjust GetSingleton()
        //{
        //    lock (objLock)
        //    {
        //        if (m_Singleton == null)
        //        {
        //            m_Singleton = new ucPztLockAddjust();
        //        }
        //    }
        //    return m_Singleton;
        //}
        public ucPztLockAddjust()
        {
            InitializeComponent();
            initialSize = this.Size;
            ucArtMain_Design.GetSingleton()._CollectChangeLanguageControls(this);
            m_Timer.Elapsed += M_Timer_Tick;
            m_Timer.AutoReset = true;
            m_ucPZTCalibChart = new ucPZTCalibChart();
            for (int i = m_LstLockValue.Count; i < m_iLockAdjustTotalCount; i++)
            { m_LstLockValue.Add(0); }
            for (int i = g_PmtLockAdjust.g_LstLockErrorRange.Count; i < m_iLockAdjustTotalCount - 1; i++)
            { g_PmtLockAdjust.g_LstLockErrorRange.Add(5); }
        }

        public void UpdateControls()
        {
            try
            {
                m_iStepIndex = -1;
                m_iProcessIndex = -1;
                m_iAdjustIndex = 0;
                if (m_ucPZTCalibChart != null)
                {
                    m_ucPZTCalibChart.Visible = false;
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
                m_Timer.Enabled = false;

                CallProcess();

                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => ReflashFunc()));
                    return;
                }
                #region//更新閉鎖參數
                tBox_TriggerNum.Text = g_PmtLockAdjust.g_iTriggerNum.ToString();
                tBox_LockMax.Text = g_PmtLockAdjust.g_dMaxValue.ToString();
                tBox_LockMin.Text = g_PmtLockAdjust.g_dMinValue.ToString();

                tBox_SlopSpce.Text = g_PmtLockAdjust.g_dSlopSpec.ToString();
                tBox_UpperLinePercentage.Text = g_PmtLockAdjust.g_dVSpecPrecentage.ToString();
                tBox_LowerPointValue.Text = g_PmtLockAdjust.g_dLowerPointValue.ToString();
                //for (int i = 0; i < m_iLockAdjustTotalCount; i++)
                //{
                //    if (i >= m_LstLockValue.Count)
                //    { m_LstLockValue.Add(0); }
                //}
                txtLockValue1.Text = m_LstLockValue[0].ToString("F3");
                txtLockValue2.Text = m_LstLockValue[1].ToString("F3");
                txtLockValue3.Text = m_LstLockValue[2].ToString("F3");
                txtLockValue4.Text = m_LstLockValue[3].ToString("F3");
                #endregion

                #region//Reflash UI
                if (m_iSchedule != 100)
                {
                    m_iSchedule = 13 * (m_iAdjustIndex + 1);
                }
                if (progressBar1.Value != 100)
                {
                    progressBar1.Value = 13 * (m_iAdjustIndex + 1);
                }

                if (m_iProcessIndex > 2)
                {
                    ErrorValue1.Text = (m_LstLockValue[1] - m_LstLockValue[0]).ToString("F3");
                }
                if (m_iProcessIndex > 4)
                {
                    ErrorValue2.Text = (m_LstLockValue[2] - m_LstLockValue[1]).ToString("F3");
                }
                if (m_iProcessIndex > 6)
                {
                    ErrorValue3.Text = (m_LstLockValue[3] - m_LstLockValue[2]).ToString("F3");
                }
                groupBox1.Enabled = m_iStepIndex == -1;
                label28.Text = progressBar1.Value.ToString() + "%";
                txtValveAdjustMessage.Text = this.m_sErrorMessage;
                txtProcess1.BackColor = m_iProcessIndex >= 0 ? Color.LightGray : SystemColors.Control;
                txtProcess2.BackColor = m_iProcessIndex >= 1 ? Color.LightGray : SystemColors.Control;
                txtProcess3.BackColor = m_iProcessIndex >= 2 ? Color.LightGray : SystemColors.Control;
                txtProcess4.BackColor = m_iProcessIndex >= 3 ? Color.LightGray : SystemColors.Control;
                txtProcess5.BackColor = m_iProcessIndex >= 4 ? Color.LightGray : SystemColors.Control;
                txtProcess6.BackColor = m_iProcessIndex >= 5 ? Color.LightGray : SystemColors.Control;
                txtProcess7.BackColor = m_iProcessIndex >= 6 ? Color.LightGray : SystemColors.Control;
                //Arrow1.Visible = m_iProcessIndex == 0;
                //Arrow2.Visible = m_iProcessIndex == 1;
                //Arrow3.Visible = m_iProcessIndex == 2;
                //Arrow4.Visible = m_iProcessIndex == 3;
                //Arrow5.Visible = m_iProcessIndex == 4;
                //Arrow6.Visible = m_iProcessIndex == 5;
                //Arrow7.Visible = m_iProcessIndex == 6;
                #endregion

                if (this.Parent == null && m_Timer.Enabled == false)
                {
                    m_Timer.Enabled = false;
                }
                else
                {
                    m_Timer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }

        }
        private bool? m_bFlowStatus = false;
        public bool? CallProcess()
        {
            try
            {
                bool? result = null;
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => result = CallProcess()));
                    return result;
                }
                //if (this.Parent != null && m_CtrlDispValve != null)
                if (m_CtrlDispValve != null)
                {
                    #region//Process Step
                    switch (m_iStepIndex)
                    {
                        case 0:
                            m_bFlowStatus = null;
                            m_sErrorMessage = "Start";
                            m_iAdjustIndex = 0;
                            m_iProcessIndex = 0;
                            clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "=======================[PZT Lock Adjust - Start]=========================");
                            clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "ArtPZT Name : " + m_CtrlDispValve.g_sName);
                            m_CtrlDispValve.SetLockAdjust_Value(0);
                            m_CtrlDispValve.SetLockAdjust_Pass(false);
                            #region//將所有閉鎖值設定為，0
                            for (int i = 0; i < m_iLockAdjustTotalCount; i++)
                            {
                                if (i >= m_LstLockValue.Count)
                                { m_LstLockValue.Add(0); }
                                else
                                { m_LstLockValue[i] = 0; }
                            }
                            #endregion
                            //ErrorResult1.Visible = false;
                            //ErrorResult2.Visible = false;
                            //ErrorResult3.Visible = false;
                            #region//舊的(Skip)
                            //ucParameter.SaveValue(clsEnum.enuPmtType.System, LockValue1, 0);
                            //ucParameter.SaveValue(clsEnum.enuPmtType.System, LockValue2, 0);
                            //ucParameter.SaveValue(clsEnum.enuPmtType.System, LockValue3, 0);
                            //ucParameter.SaveValue(clsEnum.enuPmtType.System, LockValue4, 0);
                            //ucParameter.SaveValue(clsEnum.enuPmtType.System, FinalLockValue_um, 0);
                            ////HS 2023/05/08 啟動時檢查Z軸校正
                            //if (iValveIndex == 0)
                            //{
                            //    PublicDeclare.mDispArm[clsEnum.enuDispArm.Arm1].bCalibrationZDone_Valve1 = false;//@1.0.0.53-15@
                            //    ucParameter.SaveValue(clsEnum.enuPmtType.System, clsEnum.enuPmtName.Sys_Valve_PZT_LockAdjustFail, 1);
                            //}
                            //else if (iValveIndex == 1)
                            //{
                            //    PublicDeclare.mDispArm[clsEnum.enuDispArm.Arm1].bCalibrationZDone_Valve2 = false;//@1.0.0.53-15@
                            //    ucParameter.SaveValue(clsEnum.enuPmtType.System, clsEnum.enuPmtName.Sys_Valve2_PZT_LockAdjustFail, 1);
                            //}
                            //ErrorValue1.Text = "0";
                            //ErrorValue2.Text = "0";
                            //ErrorValue3.Text = "0";
                            //ErrorResult1.Visible = false;
                            //ErrorResult2.Visible = false;
                            //ErrorResult3.Visible = false;
                            #endregion

                            #region //參數初始化
                            m_iAdjustIndex = 0;
                            m_iProcessIndex = 0;
                            m_sErrorMessage = "";
                            strPass1 = "";
                            strPass2 = "";
                            strPass3 = "";
                            m_iSchedule = 0;
                            #endregion

                            #region //EVariable初始化
                            g_ResultLockAdjust.bAdjustResult = null;
                            g_ResultLockAdjust.strValveName = m_CtrlDispValve.g_sName;
                            g_ResultLockAdjust.lstAdjustValue.Clear();
                            #endregion

                            m_iStepIndex = 100;
                            break;
                        case 100:
                            #region//開始閉鎖校正
                            m_sErrorMessage = "Start Adjust";
                            clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust Round : " + (m_iAdjustIndex + 1));
                            m_CtrlDispValve.SetLockAdjust_Start();
                            mTimer.Restart();
                            m_iStepIndex = 110;
                            break;
                        case 110:
                            m_sErrorMessage = "Adjusting...";// + m_CtrlDispValve.g_ucPztLockAdjust.m_sErrorMessage;
                            if (mTimer.IsTimeOut(500, clsCmData.enuSecUnit.MilliSec))
                            {
                                double Voltage_mV = m_CtrlDispValve.GetModbusValue((int)clsCtrlDispValve_ArtPZT.enuModbusAddress.Counter_SP_Volt);
                                m_sErrorMessage =  m_CtrlDispValve.g_eLockAdjust_Status + "," + Voltage_mV + "(mV)";
                                if (m_CtrlDispValve.GetLoakAdjust_IsProcessing() == false)
                                {
                                    double LockValue = m_CtrlDispValve.GetModbusValue((int)clsCtrlDispValve_ArtPZT.enuModbusAddress.PZT_Lock_Position_um) / 10;
                                    if (m_CtrlDispValve.g_ValveModbus.bIsSimulatorMode == true)
                                    { LockValue = 199; }
                                    m_LstLockValue[m_iAdjustIndex] = LockValue;
                                    if (m_CtrlDispValve.g_eLockAdjust_Status != clsCtrlDispValve_ArtPZT.enuLockAdjust_Status.Done)
                                    {
                                        m_iStepIndex = -999;
                                        progressBar1.Value = 0;
                                        m_iSchedule = 0;
                                        clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust Status : " + m_CtrlDispValve.g_eLockAdjust_Status.ToString());
                                        clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust Value : " + LockValue.ToString() + "(um)");
                                    }
                                    else
                                    {
                                        clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust Status : " + m_CtrlDispValve.g_eLockAdjust_Status.ToString());
                                        clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust Value : " + LockValue.ToString() + "(um)");
                                        m_iStepIndex = 120;
                                    }
                                }
                                else if (mTimer.IsTimeOut(60000, clsCmData.enuSecUnit.MilliSec)
                                    || m_CtrlDispValve.g_eLockAdjust_ErrorMessage != clsCtrlDispValve_ArtPZT.enuLockAdjust_ErrorMessage.None)
                                {
                                    m_sErrorMessage = "Respond Timeout (" + m_CtrlDispValve.g_eLockAdjust_ErrorMessage.ToString() + ")";
                                    clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust Fail : " + m_sErrorMessage);
                                    progressBar1.Value = 0;
                                    m_iStepIndex = -999;
                                }
                            }
                            #endregion
                            break;

                        case 120:
                            #region//閉鎖曲線判斷
                            {
                                m_sErrorMessage = "Curve Calculation";
                                Label ucLable = null;
                                #region//ucLabel 連結 ErrorResult
                                switch (m_iAdjustIndex)
                                {
                                    case 1:
                                        ucLable = ErrorResult1;
                                        break;
                                    case 2:
                                        ucLable = ErrorResult2;
                                        break;
                                    case 3:
                                        ucLable = ErrorResult3;
                                        break;
                                    default:
                                        break;
                                }
                                #endregion
                                double LockValue = m_LstLockValue[m_iAdjustIndex];

                                #region //EVariable加入校正結果
                                g_ResultLockAdjust.lstAdjustValue.Add(m_LstLockValue[m_iAdjustIndex]);
                                #endregion

                                if (Get_Curve_BufferData() == true)
                                {
                                    ucPZTCalibChart.enuCurveResult eCurveResult = m_ucPZTCalibChart.Chk_Curve_succse((int)g_PmtLockAdjust.g_dSlopSpec, (int)g_PmtLockAdjust.g_dVSpecPrecentage, g_PmtLockAdjust.g_dLowerPointValue);
                                    if (eCurveResult != ucPZTCalibChart.enuCurveResult.Pass)
                                    {
                                        m_sErrorMessage = "Curve Error : " + eCurveResult.ToString();
                                        clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust , Curve Data : Fail - " + eCurveResult.ToString());
                                        progressBar1.Value = 0;
                                        m_iSchedule = 0;
                                        m_CtrlDispValve.SetLockAdjust_Stop();
                                        m_iStepIndex = -999;
                                        switch (m_iAdjustIndex)
                                        {
                                            case 1:
                                                strPass1 = "NG";
                                                break;
                                            case 2:
                                                strPass2 = "NG";
                                                break;
                                            case 3:
                                                strPass3 = "NG";
                                                break;
                                            default:
                                                break;
                                        }
                                        if (ucLable != null)
                                        {
                                            ucLable.Text = "NG";
                                            ucLable.ForeColor = Color.Red;
                                            ucLable.Visible = true;
                                        }
                                    }
                                    else
                                    {
                                        clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust , Curve Data : Pass ");
                                        m_iStepIndex = 140;
                                    }
                                }
                                else
                                {
                                    m_iStepIndex = -999;
                                    m_sErrorMessage = "Get Curve Data Fail";
                                    clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust Fail : " + m_sErrorMessage);
                                }
                            }
                            #endregion
                            break;

                        case 140:
                            #region//2,3,4 與前一次的誤差 判斷
                            {
                                m_sErrorMessage = "Check Range";
                                bool bPass = true;
                                Label ucLable = null;
                                #region//ucLabel 連結 ErrorResult
                                switch (m_iAdjustIndex)
                                {
                                    case 1:
                                        ucLable = ErrorResult1;
                                        break;
                                    case 2:
                                        ucLable = ErrorResult2;
                                        break;
                                    case 3:
                                        ucLable = ErrorResult3;
                                        break;
                                    default:
                                        break;
                                }
                                #endregion

                                #region//判斷,最大最小及誤差範圍
                                double MaxValuve = g_PmtLockAdjust.g_dMaxValue;
                                double MinValuve = g_PmtLockAdjust.g_dMinValue;
                                if (m_LstLockValue[m_iAdjustIndex] > MaxValuve)
                                {
                                    bPass = false;
                                    clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust Fail (Lock Value Too Big): " + m_LstLockValue[m_iAdjustIndex] + " > " + MaxValuve);
                                    m_sErrorMessage = "Lock Value Too Big(um): " + m_LstLockValue[m_iAdjustIndex] + " > " + MaxValuve;

                                }
                                else if (m_LstLockValue[m_iAdjustIndex] < MinValuve)
                                {
                                    bPass = false;
                                    clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust Fail (Lock Value Too Small): " + m_LstLockValue[m_iAdjustIndex] + " < " + MinValuve);
                                    m_sErrorMessage = "Lock Value Too Small(um): " + m_LstLockValue[m_iAdjustIndex] + " < " + MinValuve;
                                }
                                if (m_iAdjustIndex > 0)
                                {
                                    double ErrorValue = m_LstLockValue[m_iAdjustIndex] - m_LstLockValue[m_iAdjustIndex - 1];
                                    double ErrorRange = g_PmtLockAdjust.g_LstLockErrorRange[m_iAdjustIndex - 1];
                                    if (ErrorValue > ErrorRange)
                                    {
                                        bPass = false;
                                        clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust Fail (Over Range Error): " + ErrorValue + " > " + ErrorRange);
                                        m_sErrorMessage = "Over Range Error(um) : " + ErrorValue + " > " + ErrorRange;

                                    }
                                }
                                #endregion

                                if (bPass == true)
                                {
                                    if (ucLable != null)
                                    {
                                        ucLable.Text = "Pass";
                                        ucLable.ForeColor = Color.Green;
                                        ucLable.Visible = true;
                                    }
                                    switch (m_iAdjustIndex)
                                    {
                                        case 1:
                                            strPass1 = "Pass";
                                            break;
                                        case 2:
                                            strPass2 = "Pass";
                                            break;
                                        case 3:
                                            strPass3 = "Pass";
                                            break;
                                        default:
                                            break;
                                    }
                                    mTimer.Restart();
                                    m_iStepIndex = 200;
                                }
                                else
                                {
                                    switch (m_iAdjustIndex)
                                    {
                                        case 1:
                                            strPass1 = "NG";
                                            break;
                                        case 2:
                                            strPass2 = "NG";
                                            break;
                                        case 3:
                                            strPass3 = "NG";
                                            break;
                                        default:
                                            break;
                                    }
                                    m_iSchedule = 0;
                                    if (ucLable != null)
                                    {
                                        ucLable.Text = "NG";
                                        ucLable.ForeColor = Color.Red;
                                        ucLable.Visible = true;
                                    }
                                    progressBar1.Value = 0;
                                    m_iStepIndex = -999;
                                }
                            }

                            #endregion
                            break;

                        case 200:
                            #region//判斷是否要繼續 ,(完成去999, 繼續去210 -> 100)
                            if (mTimer.IsTimeOut(500, clsCmData.enuSecUnit.MilliSec))
                            {
                                m_CtrlDispValve.SetCommand(clsCtrlDispValve_ArtPZT.enuCommand.PZTLock);
                                m_iProcessIndex++;
                                if (m_iAdjustIndex + 1 >= m_iLockAdjustTotalCount)
                                {
                                    m_iStepIndex = 999;//完成
                                }
                                else
                                {
                                    m_iAdjustIndex++;
                                    mTimer.Restart();
                                    m_iStepIndex = 210;
                                }
                            }
                            #endregion
                            break;

                        case 210:
                            #region//Trigger 然後去 100
                            m_sErrorMessage = "Delay 500";
                            if (mTimer.IsTimeOut(500, clsCmData.enuSecUnit.MilliSec))
                            {
                                m_CtrlDispValve.SetTriggerMode(clsCtrlDispValve_ArtPZT.enuTriggerMode.Continue);
                                m_CtrlDispValve.SetPmt();
                                progressBar1.Value = 13 * (m_iAdjustIndex + 1);
                                m_iSchedule = 13 * (m_iAdjustIndex + 1);
                                m_CtrlDispValve.SetOutputValue(g_PmtLockAdjust.g_iTriggerNum);
                                m_iStepIndex = 220;
                                mTimer.Restart();
                            }
                            break;

                        case 220:
                            if (mTimer.IsTimeOut(50, clsCmData.enuSecUnit.MilliSec))
                            {
                                m_sErrorMessage = "Trigger Output";
                                if (m_CtrlDispValve.GetValveStatus() == clsCtrlDispValve_ArtPZT.enuValveStatus.Ready)
                                {
                                    m_CtrlDispValve.SoftwareTrigger();
                                    m_iStepIndex = 230;
                                    mTimer.Restart();
                                }
                                else if (mTimer.IsTimeOut(10000, clsCmData.enuSecUnit.MilliSec))
                                {
                                    m_sErrorMessage = "Respond Timeout";
                                    clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust Fail : " + m_sErrorMessage);
                                    progressBar1.Value = 0;
                                    m_iStepIndex = -999;
                                }
                                else
                                {
                                    m_CtrlDispValve.SetCommand(clsCtrlDispValve_ArtPZT.enuCommand.PZTLock);
                                }
                            }
                            break;

                        case 230:
                            m_sErrorMessage = "Wait Trigger Done";
                            if ((m_CtrlDispValve.GetValveStatus() == clsCtrlDispValve_ArtPZT.enuValveStatus.Ready || m_CtrlDispValve.g_ValveModbus.bIsSimulatorMode == true)
                                && mTimer.IsTimeOut(100, clsCmData.enuSecUnit.MilliSec))
                            {
                                m_iProcessIndex++;
                                m_iStepIndex = 100;
                            }
                            else if (mTimer.IsTimeOut(60000, clsCmData.enuSecUnit.MilliSec))
                            {
                                m_sErrorMessage = "Respond Timeout";
                                clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust Fail : " + m_sErrorMessage);
                                progressBar1.Value = 0;
                                m_iSchedule = 0;
                                m_iStepIndex = -999;
                            }
                            #endregion
                            break;

                        case 999://完成
                            {
                                #region //EVariable加入校正結果
                                g_ResultLockAdjust.bAdjustResult = true;
                                #endregion
                                progressBar1.Value = 100;
                                m_iSchedule = 100;
                                m_sErrorMessage = "Done";
                                double LockValue = m_CtrlDispValve.GetModbusValue((int)clsCtrlDispValve_ArtPZT.enuModbusAddress.PZT_Lock_Position_um) / 10;
                                m_CtrlDispValve.SetLockAdjust_Value(LockValue);
                                m_CtrlDispValve.SetLockAdjust_Pass(true);
                                clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "Lock Adjust , Final Result : Pass , Lock Pos(um) = " + LockValue.ToString());
                                clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "=======================[PZT Lock Adjust - End (Pass)]=========================");
                                m_iStepIndex = -1;
                                m_bFlowStatus = true;
                            }
                            break;

                        case -999://失敗
                            {
                                #region //EVariable加入校正結果
                                g_ResultLockAdjust.bAdjustResult = false;
                                #endregion
                                if (m_sErrorMessage == "Start")
                                { m_sErrorMessage = "Fail"; }
                                m_iProcessIndex = 0;
                                m_iSchedule = 0;
                                m_CtrlDispValve.SetLockAdjust_Stop();
                                clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, "=======================[PZT Lock Adjust - End (Fail)]=========================");
                                m_CtrlDispValve.SetLockAdjust_Pass(false);
                                m_iStepIndex = -1;
                                m_bFlowStatus = false;
                            }
                            break;
                        default:
                            m_Timer.Enabled = false;
                            m_iStepIndex = -1;
                            break;
                    }
                    #endregion
                }
                if (m_iStepIndex != -1)
                {
                    m_Timer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                m_Timer.Enabled = false;
                #region //EVariable加入校正結果
                g_ResultLockAdjust.bAdjustResult = false;
                #endregion
                m_iProcessIndex = 0;
                m_iSchedule = 0;
                m_bFlowStatus = false;
                clsArtSystem.CatchLog(ex);
            }
            return m_bFlowStatus;
        }

        static bool m_bBusy = false;
        private void M_Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                bool NeedWork = false;
                lock (objLockPZT)
                {
                    if (m_bBusy == false)
                    {
                        m_bBusy = true;
                        NeedWork = true;
                    }
                }
                if (NeedWork == true)
                {
                    ReflashFunc();
                    m_bBusy = false;
                }
            }
            catch (Exception ex)
            {
                m_bBusy = false;
                clsArtSystem.CatchLog(ex);
            }

        }

        public bool GetIsAdjusting()
        {
            bool rValue = false;
            try
            {
                rValue = !(m_iStepIndex == 0 || m_iStepIndex == -1);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }


        #endregion

        #region //========== ShowForm 函式設置 ==========

        private Size initialSize = new Size();
        /// <summary> 使用Form顯示 </summary>
        public void _ShowForm(clsCtrlDispValve_ArtPZT p_CtrlDispValve, bool Dialog = true)
        {
            m_CtrlDispValve = p_CtrlDispValve;
            if (this.Parent != null && this.Parent is Form == true)
            {
                Form mForm = (Form)this.Parent;
                if (Dialog == true)
                {
                    mForm.ShowDialog();
                    mForm.BringToFront();
                }
                else
                {
                    mForm.Show();
                    mForm.BringToFront();
                }
            }
            else
            {
                Form mForm = new Form();
                mForm.WindowState = FormWindowState.Normal;
                mForm.ClientSize = this.initialSize;
                mForm.StartPosition = FormStartPosition.CenterScreen;
                mForm.Text = this.Name;
                mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
                mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
                this.Parent = mForm;
                this.Dock = DockStyle.Fill;
                this.m_Timer.Enabled = true;
                this.UpdateControls();
                if (Dialog == true)
                {
                    mForm.ShowDialog();
                }
                else
                {
                    mForm.Show();
                }
            }
        }
        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (CanCloseForm() == true)
                {
                    if (m_CtrlDispValve != null)
                    {
                        m_CtrlDispValve.SetCommand(clsCtrlDispValve_ArtPZT.enuCommand.PZTLock);
                        m_CtrlDispValve.SetCommand(clsCtrlDispValve_ArtPZT.enuCommand.SoftReset);
                        m_CtrlDispValve.SetTriggerMode(clsCtrlDispValve_ArtPZT.enuTriggerMode.Continue);
                    }
                    this.Parent = null;
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void mForm_Deactivate(object sender, EventArgs e)
        {
            try
            {
                //this.Parent = null;
                //Form mForm = (Form)sender;
                //mForm.Close();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //========== Private Function ==========
        private bool CanCloseForm()
        {
            bool rValue = false;
            try
            {
                if (m_iStepIndex == -1)
                {
                    if (m_CtrlDispValve != null)
                    {
                        if (m_CtrlDispValve.GetLockAdjust_IsPass() == false)
                        {
                            if (formMessageBox.Show("Are You Sure Want To Exist?", "Valve Lock Adjust Fail", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                rValue = true;
                            }
                        }
                        else
                        {
                            rValue = true;
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

        public bool Get_Curve_BufferData()
        {
            bool rValue = false;
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => rValue = Get_Curve_BufferData()));
                    return rValue;
                }
                m_ucPZTCalibChart.dSlopSpec = 100;

                m_ucPZTCalibChart.m_lstX_DAC.Clear();

                m_ucPZTCalibChart.m_lstY_mV.Clear();

                ushort[] Buffer = new ushort[600];


                double Buffersize = m_CtrlDispValve.GetModbusValue((int)clsCtrlDispValve_ArtPZT.enuModbusAddress.PS_Voltage_Array_Items);// clsPzt_Allring.enuAddress.PS_Voltage_Array_Items);
                if (Buffersize > 0)
                {
                    Buffer = m_CtrlDispValve.Get_PS_Voltage_Array((ushort)Buffersize);
                    if (Buffer.Length > 0)
                    {
                        for (int i = 0; i < Buffer.Length; i++)
                        {
                            m_ucPZTCalibChart.m_lstX_DAC.Add((uint)(15 * i));
                            m_ucPZTCalibChart.m_lstY_mV.Add((uint)Buffer[i]);
                        }
                        if (m_ucPZTCalibChart.m_lstX_DAC[m_ucPZTCalibChart.m_lstX_DAC.Count - 1] > 4095)
                        {
                            m_ucPZTCalibChart.m_lstX_DAC[m_ucPZTCalibChart.m_lstX_DAC.Count - 1] = 4095;
                        }
                        rValue = true;
                    }
                }
                else if (m_CtrlDispValve.g_ValveModbus.bIsSimulatorMode == true)
                {
                    List<UInt16> LstData = new List<ushort>()
                    {
                        48,
                        48,
                        48,
                        48,
                        50,
                        50,
                        52,
                        52,
                        54,
                        54,
                        56,
                        58,
                        58,
                        60,
                        62,
                        62,
                        64,
                        66,
                        68,
                        68,
                        70,
                        72,
                        74,
                        76,
                        78,
                        78,
                        80,
                        82,
                        84,
                        86,
                        88,
                        90,
                        92,
                        94,
                        96,
                        100,
                        102,
                        104,
                        106,
                        108,
                        110,
                        114,
                        116,
                        118,
                        122,
                        124,
                        128,
                        130,
                        132,
                        136,
                        138,
                        142,
                        144,
                        148,
                        152,
                        154,
                        158,
                        162,
                        164,
                        168,
                        172,
                        176,
                        178,
                        182,
                        186,
                        190,
                        194,
                        198,
                        202,
                        206,
                        212,
                        216,
                        220,
                        224,
                        228,
                        234,
                        238,
                        242,
                        248,
                        252,
                        258,
                        262,
                        266,
                        272,
                        278,
                        282,
                        288,
                        292,
                        298,
                        304,
                        308,
                        314,
                        320,
                        326,
                        332,
                        336,
                        342,
                        348,
                        354,
                        360,
                        366,
                        372,
                        376,
                        382,
                        390,
                        396,
                        402,
                        408,
                        414,
                        420,
                        426,
                        432,
                        438,
                        444,
                        450,
                        454,
                        460,
                        466,
                        472,
                        478,
                        484,
                        490,
                        498,
                        502,
                        508,
                        514,
                        522,
                        528,
                        534,
                        538,
                        544,
                        550,
                        556,
                        562,
                        568,
                        574,
                        578,
                        584,
                        590,
                        596,
                        600,
                        606,
                        612,
                        618,
                        624,
                        628,
                        636,
                        640,
                        646,
                        652,
                        658,
                        662,
                        668,
                        674,
                        680,
                        684,
                        690,
                        696,
                        700,
                        706,
                        712,
                        716,
                        722,
                        728,
                        732,
                        738,
                        744,
                        750,
                        756,
                        762,
                        766,
                        772,
                        778,
                        784,
                        788,
                        794,
                        800,
                        804,
                        810,
                        814,
                        820,
                        824,
                        830,
                        836,
                        840,
                        844,
                        848,
                        854,
                        858,
                        864,
                        868,
                        872,
                        878,
                        882,
                        888,
                        892,
                        898,
                        902,
                        906,
                        910,
                        914,
                        920,
                        924,
                        928,
                        932,
                        936,
                        942,
                        946,
                        952,
                        956,
                        960,
                        966,
                        970,
                        974,
                        978,
                        982,
                        986,
                        992,
                        996,
                        1000,
                        1004,
                        1008,
                        1012,
                        1016,
                        1020,
                        1026,
                        1030,
                        1034,
                        1038,
                        1044,
                        1048,
                        1052,
                        1056,
                        1060,
                        1064,
                        1068,
                        1072,
                        1076,
                        1080,
                        1084,
                        1086,
                        1088,
                        1088,
                        1090,
                        1092,
                        1092,
                        1094,
                        1094,
                        1094,
                        1096,
                        1096,
                        1096,
                        1098,
                        1098,
                        1100,
                        1100,
                        1100,
                        1102,
                        1102,
                        1104,
                        1104,
                        1104,
                        1106,
                        1106,
                        1106,
                        1108,
                        1108,
                        1108,
                        1110,
                        1110,
                        1110,
                        1112,
                        1112,
                    };
                    if (LstData.Count > 0)
                    {
                        for (int i = 0; i < LstData.Count; i++)
                        {
                            m_ucPZTCalibChart.m_lstX_DAC.Add((uint)(15 * i));
                            m_ucPZTCalibChart.m_lstY_mV.Add((uint)LstData[i]);
                        }
                        if (m_ucPZTCalibChart.m_lstX_DAC[m_ucPZTCalibChart.m_lstX_DAC.Count - 1] > 4095)
                        {
                            m_ucPZTCalibChart.m_lstX_DAC[m_ucPZTCalibChart.m_lstX_DAC.Count - 1] = 4095;
                        }
                        rValue = true;
                    }

                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        #endregion

        #region //========== Event ==========
        private void btn_Calculate_Chart_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_ucPZTCalibChart.Visible == false || m_ucPZTCalibChart.Parent == null)
                {
                    m_ucPZTCalibChart.Parent = this;
                    m_ucPZTCalibChart.Top = 0;
                    m_ucPZTCalibChart.Left = this.initialSize.Width + 3;
                    m_ucPZTCalibChart.Height = this.Height;
                    m_ucPZTCalibChart.Visible = true;
                    Size mSize = this.initialSize;
                    mSize.Width = (this.initialSize.Width + m_ucPZTCalibChart.initialSize.Width + 6);
                    if (this.Parent != null)
                    {
                        if (this.Parent is Form)
                        {
                            Form mForm = (Form)this.Parent;
                            mForm.ClientSize = mSize;
                            mForm.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
                        }
                    }
                    //m_ucPZTCalibChart._ShowForm();

                }
                else
                {
                    m_ucPZTCalibChart.Visible = false;
                    this.Width = this.initialSize.Width;
                    if (this.Parent != null)
                    {
                        if (this.Parent is Form)
                        {
                            Form mForm = (Form)this.Parent;
                            mForm.ClientSize = this.initialSize;
                            mForm.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btn_StartAdjust_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_CtrlDispValve != null)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + "," + this.Name + "-" + ((Control)sender).Name
                        + ", Valve Name : " + m_CtrlDispValve.g_sName);
                    clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, clsCmData.g_strNowUser + "," + this.Name + "-" + ((Control)sender).Name
                         + ", Valve Name : " + m_CtrlDispValve.g_sName);
                    m_iStepIndex = 0;
                    m_Timer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btn_StopAdjust_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_CtrlDispValve != null)
                {
                    m_sErrorMessage = "User Click Cancel";
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + "," + this.Name + "-" + ((Control)sender).Name
                        + ", Valve Name : " + m_CtrlDispValve.g_sName);
                    clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString() + "-" + m_CtrlDispValve.g_sName, clsCmData.g_strNowUser + "," + this.Name + "-" + ((Control)sender).Name
                         + ", Valve Name : " + m_CtrlDispValve.g_sName);
                    m_iStepIndex = -1;
                    m_CtrlDispValve.SetCommand(clsCtrlDispValve_ArtPZT.enuCommand.AdjustStop);
                    System.Threading.Thread.Sleep(50);
                    m_CtrlDispValve.SetCommand(clsCtrlDispValve_ArtPZT.enuCommand.PZTLock);
                    m_Timer.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void tBox_Num_Click(object sender, EventArgs e)
        {
            try
            {
                Control Item = (Control)sender;
                string sValue = Item.Text;
                if (sender == tBox_LockMax)
                {
                    if (FormNumBox.GetSingleton().ShowDialog(this, sValue, 400, 190, 0) == DialogResult.OK)
                    {
                        g_PmtLockAdjust.g_dMaxValue = FormNumBox.GetSingleton().NumBoxValue;
                    }
                }
                else if (sender == tBox_LockMin)
                {
                    if (FormNumBox.GetSingleton().ShowDialog(this, sValue, 260, 160, 0) == DialogResult.OK)
                    {
                        g_PmtLockAdjust.g_dMinValue = FormNumBox.GetSingleton().NumBoxValue;
                    }
                }
                else if (sender == tBox_TriggerNum)
                {
                    if (FormNumBox.GetSingleton().ShowDialog(this, sValue, 9999, 10, 0) == DialogResult.OK)
                    {
                        g_PmtLockAdjust.g_iTriggerNum = (int)FormNumBox.GetSingleton().NumBoxValue;
                    }
                }
                else if (sender == tBox_SlopSpce)
                {
                    if (FormNumBox.GetSingleton().ShowDialog(this, sValue, 100, 1, 0) == DialogResult.OK)
                    {
                        g_PmtLockAdjust.g_dSlopSpec = FormNumBox.GetSingleton().NumBoxValue;
                    }
                }
                else if (sender == tBox_UpperLinePercentage)
                {
                    if (FormNumBox.GetSingleton().ShowDialog(this, sValue, 100, 1, 0) == DialogResult.OK)
                    {
                        g_PmtLockAdjust.g_dVSpecPrecentage = FormNumBox.GetSingleton().NumBoxValue;
                    }
                }
                else if (sender == tBox_LowerPointValue)
                {
                    if (FormNumBox.GetSingleton().ShowDialog(this, sValue, 1, -1, 9) == DialogResult.OK)
                    {
                        g_PmtLockAdjust.g_dLowerPointValue = FormNumBox.GetSingleton().NumBoxValue;//-0.0008
                    }
                }
                else if (sender == tBox_ErrorRange1)
                {
                    if (FormNumBox.GetSingleton().ShowDialog(this, sValue, 20, 1, 0) == DialogResult.OK)
                    {
                        g_PmtLockAdjust.g_LstLockErrorRange[0] = FormNumBox.GetSingleton().NumBoxValue;
                    }
                }
                else if (sender == tBox_ErrorRange2)
                {
                    if (FormNumBox.GetSingleton().ShowDialog(this, sValue, 20, 1, 0) == DialogResult.OK)
                    {
                        g_PmtLockAdjust.g_LstLockErrorRange[1] = FormNumBox.GetSingleton().NumBoxValue;
                    }
                }
                else if (sender == tBox_ErrorRange3)
                {
                    if (FormNumBox.GetSingleton().ShowDialog(this, sValue, 20, 1, 0) == DialogResult.OK)
                    {
                        g_PmtLockAdjust.g_LstLockErrorRange[2] = FormNumBox.GetSingleton().NumBoxValue;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void StartMultiValveAdjust(clsCtrlDispValve_ArtPZT p_lstValvePara)
        {
            m_CtrlDispValve = p_lstValvePara;
            clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + "," + this.Name
                + ", Valve Name : " + m_CtrlDispValve.g_sName);
            clsLog.Log(clsCtrlDispValve_ArtPZT.enuLogName.ValveLockAdjust.ToString(), clsCmData.g_strNowUser + "," + this.Name 
                + ", Valve Name : " + m_CtrlDispValve.g_sName);
            m_bIsMultiAdjust = true;
            //_cts = new CancellationTokenSource();
            m_iStepIndex = 0;
            m_Timer.Start();
        }
        public void Stop()
        {
            if(m_Timer != null)
            {
            m_Timer.Stop();         // 停止 Timer
            }
        }
        public void Exit()
        {
            try
            {
                m_sErrorMessage = "";
                m_iStepIndex = 0;
                m_LstLockValue = new List<double>();
                m_sErrorMessage = "";
                m_iProcessIndex = 0;
                m_iSchedule = 0;
                strPass1 = "";
                strPass2 = "";
                strPass3 = "";
                for (int i = 0; i < m_iLockAdjustTotalCount; i++)
                {
                    if (i >= m_LstLockValue.Count)
                    { m_LstLockValue.Add(0); }
                }
                m_Timer.Stop();         // 停止 Timer
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion
    }
}

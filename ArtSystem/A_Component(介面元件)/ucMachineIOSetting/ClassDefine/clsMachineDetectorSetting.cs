using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtData;
using ArtCommonLib;
using ArtControlLib;
using Newtonsoft.Json;

namespace ArtSystem
{
    public class clsMachineIOSetting
    {
        /// <summary> AlarmCode會自動從這個ID往下新增 </summary>
        static public int g_iAlarmCode_StartID = 790000;

        #region//===== Nnum定義 =====
        public enum enuCtrlType
        {
            Button_DI,
            Sensor_DI,
            Sensor_AI,
            SetOutput_DO,
        }

        public enum enuActionStatus
        {
            /// <summary> 任何時間都會檢測狀態 </summary>
            AnyTime,
            /// <summary> 設備初始化的時候就要偵測 </summary>
            EqInitial,
            /// <summary> 設備完成初始化後偵測 </summary>
            Idle,
            /// <summary> 設備運作時檢測狀態 </summary>
            EqAction,
            /// <summary> 設備進入掛機狀態 </summary>
            Default,
        }

        public enum enuHotKeyButton
        {
            Run,
            Stop,
            Reset,
        }
        public enum enuSignalTower
        {
            LED_Red,
            LED_Green,//幾乎沒有使用
            LED_Blue,
            LED_Yellow,
            Bizzer1,
            Bizzer2,
        }
        public enum enuDoOutputMode
        {
            OnOff,
            On,
            Off,
        }

        #endregion

        #region//===== 委派定義 =====
        /// <summary> 委派給ArtEQ執行 </summary>
        public delegate void evtHardwareButtonClicked(clsMachineIOSetting.enuHotKeyButton p_eHotKeyButton);
        /// <summary> 委派給ArtEQ執行 </summary>
        [JsonIgnore]
        static public evtHardwareButtonClicked g_evtHardwareButtonClicked = null;

        #endregion

        #region//===== Public參數(會存檔) =====

        public enuActionStatus g_eActionStatue = enuActionStatus.AnyTime;
        public int g_iAlarmLevel = 1;
        public enuCtrlType g_eCtrlType = enuCtrlType.Button_DI;
        public enuHotKeyButton g_eHotKeyButton = enuHotKeyButton.Stop;
        public clsEnum.enuDi? g_eDI_ID = null;
        public double g_dDI_ConfirmDelay_ms = 100;
        public bool g_bDI_BType = false;
        public clsEnum.enuDi? g_eAI_ID = null;
        public clsEnum.enuDo? g_eDO_ID = null;
        public double g_dAO_Threshold = 0.5;
        public bool g_bDO_LogicInvert = false;
        public string g_sSensorName_EN = "";
        public string g_sSensorName_TC = "";
        public enuDoOutputMode g_eDoOutputMode = enuDoOutputMode.OnOff;
        public bool g_bDoOuputOnce = false;
        #endregion

        #region//===== Private參數(不會存檔) =====
        [JsonIgnore]
        private DateTime m_dTime_PreviousDIStatusChange = DateTime.Now;
        [JsonIgnore]
        private bool m_bPreviousDIStatus = false;
        [JsonIgnore]
        private bool m_bPreviousDOStatus = false;
        [JsonIgnore]
        private bool m_bPreviousNeedAlarm = false;
        [JsonIgnore]
        private clsHiPerfTimer m_DelayConfirmAlarm = new clsHiPerfTimer();
        [JsonIgnore]
        private enuActionStatus? m_ePreviousActionStatus = null;
        [JsonIgnore]
        private clsCmData.enuEqStatus ? m_ePreviousEqStatus = null;
        #endregion

        #region//===== 介面元件 =====

        [JsonIgnore]
        public ucBaseUserControl2 g_ucControlItem = null;
        #endregion

        #region//===== Function =====
        public bool GetNeedAlarm(enuActionStatus p_eActionStatus)
        {
            bool rValue = false;
            switch (this.g_eActionStatue)
            {
                case enuActionStatus.AnyTime:
                    rValue = true;
                    break;
                case enuActionStatus.EqInitial:
                    if (p_eActionStatus == enuActionStatus.EqInitial
                        //|| p_eActionStatus == enuActionStatus.Idle
                        ||  p_eActionStatus == enuActionStatus.EqAction
                        )
                    {
                        rValue = true;
                    }
                    break;
                case enuActionStatus.Idle:
                    if (p_eActionStatus == enuActionStatus.Idle
                        || p_eActionStatus == enuActionStatus.EqAction)
                    {
                        rValue = true;
                    }
                    break;
                case enuActionStatus.EqAction:
                    if (p_eActionStatus == enuActionStatus.EqAction)
                    {
                        rValue = true;
                    }
                    break;
                case enuActionStatus.Default:
                    if (p_eActionStatus == enuActionStatus.Default)
                    {
                        rValue = true;
                    }
                    break;
                default:
                    break;
            }
            if (this.g_eDO_ID != null)
            {
                clsEnum.enuDo eDO = (clsEnum.enuDo)this.g_eDO_ID;
                bool bDOStatus = clsDioCtrl.GetDo(eDO);
                if (bDOStatus != this.g_bDO_LogicInvert)
                {
                    rValue = false;
                }
                if (rValue == true)
                {
                    if (this.m_bPreviousDOStatus != bDOStatus)
                    {
                        this.m_bPreviousDOStatus = bDOStatus;
                        m_DelayConfirmAlarm.Restart();
                    }
                }
            }
            if (rValue == true)
            {
                if (m_bPreviousNeedAlarm == false)
                {
                    m_DelayConfirmAlarm.Restart();
                }
                m_bPreviousNeedAlarm = true;
            }
            else
            {
                m_bPreviousNeedAlarm = false;
            }
            if (rValue == true)
            {
                if (m_DelayConfirmAlarm.IsTimeOut(this.g_dDI_ConfirmDelay_ms, clsCmData.enuSecUnit.MilliSec) == false)
                {
                    rValue = false;
                }
            }
            return rValue;
        }
        public bool GetNeedOutputDo(enuActionStatus p_eActionStatus)
        {
            bool rValue = false;
            switch (this.g_eActionStatue)
            {
                case enuActionStatus.AnyTime:
                    rValue = true;
                    break;
                case enuActionStatus.Idle:
                    if (p_eActionStatus == enuActionStatus.Idle)
                    {
                        rValue = true;
                    }
                    break;
                case enuActionStatus.EqAction:
                    if (p_eActionStatus == enuActionStatus.EqAction)
                    {
                        rValue = true;
                    }
                    break;
                case enuActionStatus.Default:
                    if (p_eActionStatus == enuActionStatus.Default)
                    {
                        rValue = true;
                    }
                    break;
                default:
                    break;
            }
            if (this.g_eDI_ID != null)
            {
                clsEnum.enuDi eDI = (clsEnum.enuDi)this.g_eDI_ID;
                bool bDIStatus = clsDioCtrl.GetDi(eDI);
                if (bDIStatus != this.g_bDI_BType)
                {
                    rValue = false;
                }
            }
            return rValue;
        }
        public bool ReflashDetector(enuActionStatus p_eActionStatus)
        {
            bool rValue = false;
            try
            {
                switch (this.g_eCtrlType)
                {
                    case enuCtrlType.Button_DI:
                        #region//Button_DI
                        if (this.g_eDI_ID != null)
                        {
                            clsEnum.enuDi eDI = (clsEnum.enuDi)this.g_eDI_ID;
                            bool bStatus = clsDioCtrl.GetDi(eDI);
                            double dClickTime = 0;
                            if (bStatus != this.m_bPreviousDIStatus)
                            {
                                this.m_bPreviousDIStatus = bStatus;
                                dClickTime = (DateTime.Now - m_dTime_PreviousDIStatusChange).TotalSeconds;
                                m_dTime_PreviousDIStatusChange = DateTime.Now;
                                if (bStatus == false && dClickTime < 5 && dClickTime > 0.2)
                                {
                                    if (clsCmData.g_bIsinitialized == true)
                                    {
                                        if (clsMachineIOSetting.g_evtHardwareButtonClicked != null)
                                        {
                                            clsMachineIOSetting.g_evtHardwareButtonClicked(this.g_eHotKeyButton);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        break;
                    case enuCtrlType.Sensor_DI:
                        #region//SensorDI
                        if (this.g_eDI_ID != null)
                        {
                            clsEnum.enuDi eDI = (clsEnum.enuDi)this.g_eDI_ID;
                            bool bStatus = clsDioCtrl.GetDi(eDI);
                            if (bStatus != this.g_bDI_BType)
                            {
                                if (GetNeedAlarm(p_eActionStatus) == true)
                                {
                                    formAlarmReport.ReportAlm(GetAlarmCode_DI().ToString(), null, eDI.ToString());
                                    rValue = true;
                                }
                            }
                            else
                            {
                                this.m_bPreviousNeedAlarm = false;
                            }
                        }
                        #endregion
                        break;
                    case enuCtrlType.Sensor_AI:
                        if (clsArtSystem.bIsSoftwareSimulate == false)
                        {
                            #region//SensorAI
                            if (this.g_eDI_ID != null)
                            {
                                clsEnum.enuDi eDI = (clsEnum.enuDi)this.g_eDI_ID;
                                double dAIValue = clsDioCtrl.GetAi(eDI);
                                bool bAlarm = false;
                                if (this.g_bDI_BType == false)
                                {
                                    if (dAIValue < this.g_dAO_Threshold)
                                    {
                                        bAlarm = true;
                                    }
                                }
                                else
                                {
                                    if (dAIValue > this.g_dAO_Threshold)
                                    {
                                        bAlarm = true;
                                    }
                                }
                                if (bAlarm)
                                {
                                    if (GetNeedAlarm(p_eActionStatus) == true)
                                    {
                                        formAlarmReport.ReportAlm(GetAlarmCode_DI().ToString(), null, eDI.ToString());
                                        rValue = true;
                                    }
                                }
                                else
                                {
                                    this.m_bPreviousNeedAlarm = false;
                                }
                            }
                            #endregion
                        }
                        break;
                    case enuCtrlType.SetOutput_DO:
                        #region//SetOutput_DO
                        if (this.g_eDO_ID != null)
                        {
                            bool bNeedOutput = true;
                            if (this.g_bDoOuputOnce == true)
                            {
                                if (this.m_ePreviousActionStatus == p_eActionStatus
                                    && m_ePreviousEqStatus == clsCmData.g_NowEqStatus)
                                {
                                    bNeedOutput = false;
                                }
                            }
                            if (bNeedOutput == true)
                            {
                                bool bOutput = GetNeedOutputDo(p_eActionStatus);
                                switch (this.g_eDoOutputMode)
                                {
                                    case enuDoOutputMode.OnOff:
                                        clsDioCtrl.SetDo((clsEnum.enuDo)this.g_eDO_ID, bOutput);
                                        break;
                                    case enuDoOutputMode.On:
                                        if (bOutput == true)
                                        {
                                            clsDioCtrl.SetDo((clsEnum.enuDo)this.g_eDO_ID, true);
                                        }
                                        break;
                                    case enuDoOutputMode.Off:
                                        if (bOutput == true)
                                        { clsDioCtrl.SetDo((clsEnum.enuDo)this.g_eDO_ID, false); }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
                m_ePreviousActionStatus = p_eActionStatus;
                m_ePreviousEqStatus = clsCmData.g_NowEqStatus;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        public int GetAlarmCode_DI()
        {
            int rValue = 0;
            try
            {
                if (g_eDI_ID != null)
                {
                    if (g_iAlarmLevel == 2)
                    {
                        rValue = 970000;
                        rValue += 10 * (int)g_eDI_ID;
                        rValue += 1;
                    }
                    else
                    {
                        rValue = 770000;
                        rValue += 10 * (int)g_eDI_ID;
                        rValue += 1;
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
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtSystem;

namespace ArtEQ
{
    /// <summary> IO監控流程規劃 </summary>
    public class ProcDetectIO : clsThreadProc
    {
        #region //=====================  全域變數設置 =====================
        static private Dictionary<enuDetectIOName, bool> mDic_DetectIO_bState = new Dictionary<enuDetectIOName, bool>();
        static public Dictionary<enuDetectIOName, bool> gDic_DetectIO_State
        {
            get
            {
                return mDic_DetectIO_bState;
            }
            private set
            {
                mDic_DetectIO_bState = value;
            }
        }



        static private Dictionary<clsEnum.enuDi, long> TriggerONTick = new Dictionary<clsEnum.enuDi, long>();
        static private Dictionary<clsEnum.enuDi, bool> TriggerONStatus = new Dictionary<clsEnum.enuDi, bool>();

        #endregion

        #region //=====================  Enum (DetectIO, 30ms) =====================
        public enum enuDetectIOName
        {
            EMO_Occured,
            Main_Pressure_Error,
            Power_Status_Error,
            ButtonStart,
            ButtonStop,
            ButtonReset,
            SafeDoor_Error,
            LightGate_Detect,
        }
        #endregion

        #region //=====================  必要函式設置 =====================

        public ProcDetectIO(string p_strLogName)
            : base(p_strLogName)
        {
            foreach (enuDetectIOName eName in Enum.GetValues(typeof(enuDetectIOName)))
            {
                if (mDic_DetectIO_bState.ContainsKey(eName) == false)
                {
                    mDic_DetectIO_bState.Add(eName, false);
                }
            }

        }

        protected override void Scenario()
        {
            PublicDeclare.ReflashAPAlram();
            mDic_DetectIO_bState[enuDetectIOName.EMO_Occured] = clsDioCtrl.IsTrigerDi(clsEnum.enuDi.EMO_B, 30, true);
            mDic_DetectIO_bState[enuDetectIOName.Main_Pressure_Error] = clsDioCtrl.IsTrigerDi(clsEnum.enuDi.Air_Source_B, 30, true);
            mDic_DetectIO_bState[enuDetectIOName.Power_Status_Error] = clsDioCtrl.IsTrigerDi(clsEnum.enuDi.Button_Power, 30, true);
            mDic_DetectIO_bState[enuDetectIOName.SafeDoor_Error] = clsDioCtrl.IsTrigerDi(clsEnum.enuDi.SafeDoor_B, 30, true);
            //mDic_DetectIO_bState[enuDetectIOName.LightGate_Detect] = clsDioCtrl.IsTrigerDi(clsEnum.enuDi.LightGate_B, 30, true);
            mDic_DetectIO_bState[enuDetectIOName.ButtonStart] = IsTriggerDI_OnOff(clsEnum.enuDi.Button_Start, 5000, false);
            mDic_DetectIO_bState[enuDetectIOName.ButtonStop] = IsTriggerDI_OnOff(clsEnum.enuDi.Button_Stop, 5000, false);
            mDic_DetectIO_bState[enuDetectIOName.ButtonReset] = IsTriggerDI_OnOff(clsEnum.enuDi.Button_Reset, 5000, false);

            if (clsArtSystem.bIsSoftwareSimulate == false)
            {
                #region//Machine Error Event
                if (mDic_DetectIO_bState[enuDetectIOName.EMO_Occured] == true)
                {
                    clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.Machine_Error_EMO_Occured, clsEnum.enuDi.EMO_B);
                }
                if (mDic_DetectIO_bState[enuDetectIOName.Power_Status_Error] == true)
                {
                    clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.Machine_Error_Power_Error, clsEnum.enuDi.Button_Power);
                }
                if (mDic_DetectIO_bState[enuDetectIOName.Main_Pressure_Error] == true)
                {
                    clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.Machine_Error_Air_Pressure_Low, clsEnum.enuDi.Air_Source_B);
                }
                if (mDic_DetectIO_bState[enuDetectIOName.SafeDoor_Error] == true
                    && ucParameter.GetValueBool(clsEnum.enuPmtName.Sys_EnableSafeDoor) == true)
                {
                    clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.Machine_Error_SafeDoor_Open, clsEnum.enuDi.SafeDoor_B);
                }
                if (mDic_DetectIO_bState[enuDetectIOName.LightGate_Detect] == true
                    && clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Initial)
                {
                    //clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.Machine_Error_LightGate_Detect, clsEnum.enuDi.LightGate_B);
                }
                #endregion

                #region//Button Run Stop Reset
                if (mDic_DetectIO_bState[enuDetectIOName.ButtonStart] == true
                    && clsCmData.g_bIsinitialized == true)//機台是否完成初始化的旗標(Flag)
                {
                    clsLog.Log(clsEnum.enuLogName.ButtonLog, clsCmData.g_strNowUser + " :  Hardware Button Click -  Run");
                    clsEditRunThread.EqRun(true);
                }
                if (mDic_DetectIO_bState[enuDetectIOName.ButtonStop] == true)
                {
                    clsLog.Log(clsEnum.enuLogName.ButtonLog, clsCmData.g_strNowUser + " :  Hardware Button Click -  Stop");
                    clsEditRunThread.EqStop();
                }
                if (mDic_DetectIO_bState[enuDetectIOName.ButtonReset] == true)
                {
                    clsLog.Log(clsEnum.enuLogName.ButtonLog, clsCmData.g_strNowUser + " :  Hardware Button Click -  Reset");
                    formAlarmReport.ResetAllAlm(clsCmData.enuResetResult.Reset);
                }
                #endregion
            }
        }

        #endregion

        #region //===================== private 函式設置 =====================


        #endregion

        #region //===================== public 函式設置 =====================
        static public bool IsTriggerDI_OnOff(clsEnum.enuDi pDiName, long p_OnOffLessThan_ms, bool BType = false)
        {
            if (TriggerONTick.Keys.Contains(pDiName) == false)
            { TriggerONTick.Add(pDiName, 0); }
            if (TriggerONStatus.Keys.Contains(pDiName) == false)
            { TriggerONStatus.Add(pDiName, false); }
            bool DiOn = !BType;
            if (clsDioCtrl.GetDi(pDiName) == DiOn)
            {
                if (TriggerONStatus[pDiName] == false)
                {
                    TriggerONStatus[pDiName] = true;
                    TriggerONTick[pDiName] = DateTime.Now.Ticks;
                }
            }
            else
            {
                if (TriggerONStatus[pDiName] == true)
                {
                    TriggerONStatus[pDiName] = false;
                    if (DateTime.Now.Ticks - TriggerONTick[pDiName] < (p_OnOffLessThan_ms * 10000))
                    {
                        return true;
                    }
                    TriggerONTick[pDiName] = 0;
                }
            }
            return false;
        }
        #endregion

        #region //===================== Alarm 函式設置 =====================


        #endregion
    }
}

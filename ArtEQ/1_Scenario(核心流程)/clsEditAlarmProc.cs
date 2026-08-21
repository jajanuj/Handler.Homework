using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtCommonLib;
using ArtControlLib;
using ArtData;


namespace ArtEQ
{
    public class clsEditAlarmProc
    {
        /// <summary> Alarm 異常處置事件 (Reset、Retry、Skip、Continue) </summary>
        static public int AlarmCodeProc(clsObjAlarm p_AlarmObject)
        {
            try
            {
                clsEditRunThread.EqStop();
                ucAlarmCounter.GetSingleton().ClrAlarmClick(p_AlarmObject);
                try
                {
                    switch ((clsEnum.enuAlarm)Convert.ToInt32(p_AlarmObject.AlarmCode))
                    {
                        case clsEnum.enuAlarm.Machine_Error_EMO_Occured:
                        case clsEnum.enuAlarm.Machine_Error_Power_Error:
                            switch (p_AlarmObject.enuResetResult)
	                        {
                                case clsCmData.enuResetResult.Continue:
                                    break;
                                case clsCmData.enuResetResult.Reset:
                                    break;
                                case clsCmData.enuResetResult.Retry:
                                    break;
                                case clsCmData.enuResetResult.Skip:
                                    break;
                                default:
                                    break;
	                        }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
            return 0;
        }

        /// <summary> Alarm 觸發事件 </summary>
        static public int ReportProc(clsObjAlarm p_AlarmObject) 
        {
            try
            {
                clsEditRunThread.EqStop();
                ucAlarmCounter.GetSingleton().ReportProc(p_AlarmObject);
                try
                {
                    switch ((clsEnum.enuAlarm)Convert.ToInt32(p_AlarmObject.AlarmCode))
                    {
                        case clsEnum.enuAlarm.Machine_Error_EMO_Occured:
                        case clsEnum.enuAlarm.Machine_Error_Power_Error:
                            clsCmData.g_bIsinitialized = false;//完成初始化的旗標 設為 False (必須要重新初始化)
                            clsEditRunThread.EqStop();
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
                return -1;
            }
            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AxWinSecsLib;
using WinSecsLib;

namespace ArtEQ
{
    class SecsHost_SecondaryIn
    {
        public static void secsEAP_SecondaryIn(SecsEvents_SecondaryInEvent e)
        {
            try
            {
                switch (e.trans.Name)
                {
                    case "S1F1":
                        ucHost_SECS.GetSingleton().WriteSecsLog("S", true, "CIM -> EQP SecondaryIn  [S1F2]", e.trans);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "CIM -> EQP SecondaryIn  [S1F2]");
                        break;

                    case "S1F13":
                        ucHost_SECS.GetSingleton().WriteSecsLog("S", true, "CIM -> EQP SecondaryIn  [S1F14]", e.trans);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "CIM -> EQP SecondaryIn  [S1F14]");
                        break;

                    case "S5F1":
                        ucHost_SECS.GetSingleton().WriteSecsLog("S", true, "CIM -> EQP SecondaryIn  [S5F2]", e.trans);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "CIM -> EQP SecondaryIn  [S5F2]");
                        break;

                    case "S6F11":
                        ucHost_SECS.GetSingleton().WriteSecsLog("S", true, "CIM -> EQP SecondaryIn  [S6F12]", e.trans);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "CIM -> EQP SecondaryIn  [S6F12]");
                        break;

                    case "S10F1":
                        ucHost_SECS.GetSingleton().WriteSecsLog("S", true, "CIM -> EQP SecondaryIn  [S10F2]", e.trans);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "CIM -> EQP SecondaryIn  [S10F2]");
                        break;

                    case "S14F1":
                        ucHost_SECS.GetSingleton().WriteSecsLog("S", true, "CIM -> EQP SecondaryIn  [S14F2]", e.trans);
                        SecondaryInFucntion.SecondaryIn_S14F2(e);
                        break;

                    case "S14F3":
                        ucHost_SECS.GetSingleton().WriteSecsLog("S", true, "CIM -> EQP SecondaryIn  [S14F4]", e.trans);
                        SecondaryInFucntion.SecondaryIn_S14F4(e);
                        break;

                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception Log - [ucHost_SECS]" + ex.ToString());
            }

        }

    }
}

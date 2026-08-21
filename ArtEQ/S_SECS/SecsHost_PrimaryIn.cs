using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AxWinSecsLib;
using WinSecsLib;

namespace ArtEQ
{
    class SecsHost_PrimaryIn
    {
        public static void secsEAP_PrimaryIn(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                switch (e.trans.Name)
                {

                    case "S1F1":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S1F1]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S1F1(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S1F1]");
                        break;

                    case "S1F3":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S1F3]", e.trans);
                        SECSAP.UpdateAllSVID();
                        PrimaryInFucntion.PrimaryIn_S1F3(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S1F3]");
                        break;

                    case "S1F11":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S1F11]", e.trans);
                        SECSAP.UpdateAllSVID();
                        PrimaryInFucntion.PrimaryIn_S1F11(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S1F11]");
                        break;

                    case "S1F13":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S1F13]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S1F13(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S1F13]");
                        break;

                    case "S1F15":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S1F15]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S1F15(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S1F15]");
                        break;

                    case "S1F17":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S1F17]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S1F17(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S1F17]");
                        break;

                    case "S2F13":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S2F13]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S2F13(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S2F13]");
                        break;

                    case "S2F15":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S2F15]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S2F15(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S2F15]");
                        break;

                    case "S2F17":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S2F17]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S2F17(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S2F17]");
                        break;

                    case "S2F29":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S2F29]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S2F29(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S2F29]");
                        break;

                    case "S2F31":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S2F31]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S2F31(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S2F31]");
                        break;

                    case "S2F33":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S2F33]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S2F33(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S2F33]");
                        break;

                    case "S2F35":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S2F35]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S2F35(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S2F35]");
                        break;

                    case "S2F37":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S2F37]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S2F37(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S2F37]");
                        break;

                    case "S2F41":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S2F41]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S2F41(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S2F41]");
                        break;

                    case "S5F3":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S5F3]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S5F3(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S5F3]");
                        break;

                    case "S5F5":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S5F5]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S5F5(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S5F5]");
                        break;

                    case "S5F7":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S5F7]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S5F7(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S5F7]");
                        break;

                    case "S6F15":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S6F15]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S6F15(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S6F15]");
                        break;

                    case "S6F19":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S6F19]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S6F19(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S6F19]");
                        break;

                    case "S7F1":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S7F1]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S7F1(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S7F1]");
                        break;

                    case "S7F3":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S7F3]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S7F3(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S7F3]");
                        break;

                    case "S7F5":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S7F5]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S7F5(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S7F5]");
                        break;

                    case "S7F17":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S7F17]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S7F17(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S7F17]");
                        break;

                    case "S7F19":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S7F19]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S7F19(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S7F19]");
                        break;

                    case "S7F23":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S7F23]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S7F23(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S7F23]");
                        break;

                    case "S7F25":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S7F25]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S7F25(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S7F25]");
                        break;

                    case "S10F3":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S10F3]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S10F3(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S10F3]");
                        break;

                    case "S10F5":
                        ucHost_SECS.GetSingleton().WriteSecsLog("P", true, "CIM -> EQP  PrimaryIn  [S10F5]", e.trans);
                        PrimaryInFucntion.PrimaryIn_S10F5(e);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Host2Equipment, "CIM -> EQP  PrimaryIn  [S10F5]");
                        break;

                    default:
                        PrimaryOutFucntion.PrimaryOut_S9F3(e);
                        PrimaryOutFucntion.PrimaryOut_S9F5(e);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AxWinSecsLib;
using WinSecsLib;

namespace ArtEQ
{
    class PrimaryOutFucntion
    {
        #region S1FX Function (Stream 1 Equipment Status) - PrimaryOut

        /// <summary> 詢問Host,Are You There Request(R) 你在嗎? - 目前沒有使用 </summary>
        private static void PrimaryOut_S1F1()
        {
            try
            {
                SecsTransaction Trans;
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S1F1");
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S1F1]", Trans);
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S1F1()\r\n" + ex.ToString());
            }
        }

        /// <summary>詢問Host, 建立通訊請求 - 目前沒有使</summary>
        private static void PrimaryOut_S1F13()
        {
            try
            {
                SecsTransaction Trans;
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S1F13");
                Trans.Primary.Item[1].Item[1].Format = FormatConstants.wsFormatAscii;
                Trans.Primary.Item[1].Item[1].Value = ucHost_SECS.GetSingleton().sMDLN;
                Trans.Primary.Item[1].Item[2].Format = FormatConstants.wsFormatAscii;
                Trans.Primary.Item[1].Item[2].Value = ucHost_SECS.GetSingleton().sVersion;
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S1F13]", Trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S1F13]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S1F13()\r\n" + ex.ToString());
            }
        }

        #endregion

        #region S5FX Function (Stream 5 Exception Handling) - PrimaryOut

        /// <summary> S5F1 : Alarm Report Send 發送Alarm事件 </summary>
        public static void PrimaryOut_S5F1(bool _IsAlarmStart, int _ALID, string _ALTX)
        {
            try
            {
                SecsTransaction Trans;
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S5F1");
                Trans.Primary.Item[1].Item[1].Format = FormatConstants.wsFormatBinary;
                if (_IsAlarmStart)
                    Trans.Primary.Item[1].Item[1].Value = Convert.ToByte(128);
                else
                    Trans.Primary.Item[1].Item[1].Value = Convert.ToByte(0);
                Trans.Primary.Item[1].Item[2].Format = FormatConstants.wsFormatI4;
                Trans.Primary.Item[1].Item[2].Value = _ALID;
                Trans.Primary.Item[1].Item[3].Format = FormatConstants.wsFormatAscii;
                Trans.Primary.Item[1].Item[3].Value = _ALTX;
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S5F1]", Trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S5F1]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S5F1()\r\n" + ex.ToString());
            }
        }

        #endregion

        #region S6FX Function (Stream 6 Data Collection) - PrimaryOut

        /// <summary> S6F11 : Event Report Send 發送機台作業Event事件(CEID) </summary>
        public static void PrimaryOut_S6F11(int _CEID)
        {
            try
            {
                SECSAP.UpdateAllSVID();
                SecsTransaction Trans;
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S6F11");
                Trans.Primary.Item[1].Item[1].Format = FormatConstants.wsFormatI4;
                Trans.Primary.Item[1].Item[1].Value = ucHost_SECS.GetSingleton().m_SecsState.GetDataID().ToString();
                Trans.Primary.Item[1].Item[2].Format = FormatConstants.wsFormatI4;
                Trans.Primary.Item[1].Item[2].Value = _CEID.ToString();
                if (ucHost_SECS.GetSingleton().m_lstCEID[_CEID].lstLinkReport.Count > 0)
                {

                    for (int j = 0; j < ucHost_SECS.GetSingleton().m_lstCEID[_CEID].lstLinkReport.Count - 1; j++)
                    {
                        Trans.Primary.Item[1].Item[3].Item[1].Duplicate();
                    }

                    for (int j = 1; j < ucHost_SECS.GetSingleton().m_lstCEID[_CEID].lstLinkReport.Count + 1; j++)
                    {
                        if (ucHost_SECS.GetSingleton().m_lstRPTID.ContainsKey(ucHost_SECS.GetSingleton().m_lstCEID[_CEID].lstLinkReport[j - 1]))
                        {
                            int rptid = ucHost_SECS.GetSingleton().m_lstCEID[_CEID].lstLinkReport[j - 1];
                            Trans.Primary.Item[1].Item[3].Item[j].Item[1].Format = FormatConstants.wsFormatU4;
                            Trans.Primary.Item[1].Item[3].Item[j].Item[1].Value = rptid.ToString();
                            Trans.Primary.Item[1].Item[3].Item[j].Item[2].Item[1].Delete();// delete default item
                            for (int i = 0; i < ucHost_SECS.GetSingleton().m_lstRPTID[ucHost_SECS.GetSingleton().m_lstCEID[_CEID].lstLinkReport[j - 1]].lstVID.Count; i++)
                            {
                                int VID = ucHost_SECS.GetSingleton().m_lstRPTID[rptid].lstVID[i];
                                Trans.Primary.Item[1].Item[3].Item[j].Item[2].AddNew(i + 1);
                                Trans.Primary.Item[1].Item[3].Item[j].Item[2].Item[i + 1].Format = FormatConstants.wsFormatAscii;
                                Trans.Primary.Item[1].Item[3].Item[j].Item[2].Item[i + 1].Name = VID.ToString();
                                if (VID >= 3000)
                                {
                                    // DVID
                                    Trans.Primary.Item[1].Item[3].Item[j].Item[2].Item[i + 1].Value = ucHost_SECS.GetSingleton().m_lstDVID[VID].Value;
                                }
                                else
                                {
                                    // SVID
                                    Trans.Primary.Item[1].Item[3].Item[j].Item[2].Item[i + 1].Value = ucHost_SECS.GetSingleton().m_lstSVID[VID].Value;
                                }
                            }
                        }
                        else
                        {
                            Trans.Primary.Item[1].Item[3].Item[j].Delete();
                        }
                    }
                }
                else
                {
                    // this event no report
                    Trans.Primary.Item[1].Item[3].Delete();
                }
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S6F11]" + "  '" + ucHost_SECS.GetSingleton().m_lstCEID[_CEID].Name, Trans);
                //ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM PrimaryOut  [S6F11]" + "  '" + ucHost_SECS.GetSingleton().m_lstCEID[_CEID].Name);
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S6F11() at CEID:" + _CEID.ToString() + "\r\n" + ex.ToString());
            }
        }

        #endregion

        #region S9FX Function (Stream 9 System Errors) - PrimaryOut

        public static void PrimaryOut_S9F1()
        {
            try
            {
                SecsTransaction Trans;
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S9F1");
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S9F1]", Trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S9F1]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S9F1()\r\n" + ex.ToString());
            }
        }

        public static void PrimaryOut_S9F3(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                byte[] bMHead = new byte[10];
                int[] iResult = new int[10];
                string test = string.Empty;
                SecsTransaction Trans;
                ucHost_SECS.GetSingleton().Get_MHEAD_Data(e, ref bMHead);
                for (int i = 0; i < iResult.Length; i++)
                {
                    iResult[i] = (int)bMHead[i];
                    test += iResult[i].ToString();
                }
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S9F3");
                Trans.Primary.Item[1].Value = test;
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S9F3]", Trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S9F3]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S9F3()\r\n" + ex.ToString());
            }
        }

        public static void PrimaryOut_S9F5(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                byte[] bMHead = new byte[10];
                int[] iResult = new int[10];
                string test = string.Empty;
                SecsTransaction Trans;
                ucHost_SECS.GetSingleton().Get_MHEAD_Data(e, ref bMHead);
                for (int i = 0; i < iResult.Length; i++)
                {
                    iResult[i] = (int)bMHead[i];
                    test += iResult[i].ToString();
                }
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S9F5");
                Trans.Primary.Item[1].Value = test;
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S9F5]", Trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S9F5]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S9F5()\r\n" + ex.ToString());
            }
        }

        public static void PrimaryOut_S9F7(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                byte[] bMHead = new byte[10];
                int[] iResult = new int[10];
                string test = string.Empty;
                SecsTransaction Trans;
                ucHost_SECS.GetSingleton().Get_MHEAD_Data(e, ref bMHead);
                for (int i = 0; i < iResult.Length; i++)
                {
                    iResult[i] = (int)bMHead[i];
                    test += iResult[i].ToString();
                }
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S9F7");
                Trans.Primary.Item[1].Value = test;
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S9F7]", Trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S9F7]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S9F7()\r\n" + ex.ToString());
            }
        }

        public static void PrimaryOut_S9F9()
        {
            try
            {
                SecsTransaction Trans;
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S9F9");
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S9F9]", Trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S9F9]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S9F9()\r\n" + ex.ToString());
            }
        }

        public static void PrimaryOut_S9F11()
        {
            try
            {
                SecsTransaction Trans;
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S9F11");
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S9F11]", Trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S9F11]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S9F11()\r\n" + ex.ToString());
            }
        }

        public static void PrimaryOut_S9F13()
        {
            try
            {
                SecsTransaction Trans;
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S9F13");
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S9F13]", Trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S9F13]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S9F13()\r\n" + ex.ToString());
            }
        }

        #endregion 

        #region S10FX Function (Stream 10 Terminal Services) - PrimaryOut

        /// <summary> S10F1 : Terminal Request 傳送訊息給Host端 </summary>
        public  static void PrimaryOut_S10F1(string strSendMsg)
        {
            try
            {
                SecsTransaction Trans;
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S10F1");
                Trans.Primary.Item[1].Item[1].Value = Convert.ToByte(0); ;
                Trans.Primary.Item[1].Item[2].Value = strSendMsg;
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S10F1]", Trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  PrimaryOut  [S10F1]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S9F13()\r\n" + ex.ToString());
            }
        }

        #endregion

        #region S14FX Function (Stream 14 Terminal Services) - PrimaryOut

        public static void PrimaryOut_S14F1(string strOBJID)
        {
            try
            {
                SecsTransaction Trans;
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S14F1");

                Trans.Primary.Item[1].Item[2].Value = "Substrate";
                Trans.Primary.Item[1].Item[3].Item[1].Value = strOBJID;
                Trans.Primary.Item[1].Item[4].Item[1].Item[1].Value = "SubstrateType";
                Trans.Primary.Item[1].Item[4].Item[1].Item[2].Value = "Strip";
                Trans.Primary.Item[1].Item[4].Item[1].Item[3].Format = FormatConstants.wsFormatU1;
                Trans.Primary.Item[1].Item[4].Item[1].Item[3].Value = 0;
                Trans.Primary.Item[1].Item[5].Item[1].Value = "MapData";
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S14F1]", Trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM PrimaryOut  [S14F1]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S14F1()\r\n" + ex.ToString());
            }
        }

        public static void PrimaryOut_S14F3(string strOBJSPEC, string strTYPE, string strOBJID, string strATTRID, string strATTRDATAPath)
        {
            try
            {
                SecsTransaction Trans;
                Trans = ucHost_SECS.GetSingleton().secsEAP.get_NewTransaction("S14F3");

                string strATTRDATA = ucHost_SECS.GetSingleton().strGetFileText(strATTRDATAPath);

                Trans.Primary.Item[1].Item[1].Value = strOBJSPEC;
                Trans.Primary.Item[1].Item[2].Value = strTYPE;
                Trans.Primary.Item[1].Item[3].Item[1].Value = strOBJID;
                Trans.Primary.Item[1].Item[4].Item[1].Value = strATTRID;
                Trans.Primary.Item[1].Item[4].Item[2].Value = strATTRDATA;
                Trans.Send();
                ucHost_SECS.GetSingleton().WriteSecsLog("P", false, "EQP -> CIM PrimaryOut  [S14F3]", Trans);
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryOut_S14F3()\r\n" + ex.ToString());
            }
        }

        #endregion
    }
}

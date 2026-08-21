using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ArtControlLib;
using ArtData;
using ArtModuleData;
using ArtProcModuleLib;
using AxWinSecsLib;
using WinSecsLib;

namespace ArtEQ
{
    class PrimaryInFucntion
    {
        #region S1FX Function (Stream 1 Equipment Status) - PrimaryIn

        /// <summary> Host 下SXF0給EQP，回應SecondaryOut_SXF0() </summary>
        /// <summary> SXF0 : Abort Transaction </summary>
        public static void PrimaryIn_SXF0(SecsEvents_PrimaryInEvent e)
        {
            SecondaryOutFucntion.SecondaryOut_SXF0(e);
        }

        /// <summary> Host 下S1F1給EQP，回應SecondaryOut_S1F2() </summary>
        /// <summary> S1F1 : Are You There Request(R) 你在嗎 請求 </summary>
        public static void PrimaryIn_S1F1(SecsEvents_PrimaryInEvent e)
        {
            SecondaryOutFucntion.SecondaryOut_S1F2(e);
        }

        /// <summary> Host 下S1F3給EQP，回應SecondaryOut_S1F4() </summary>
        /// <summary> S1F3 : Selected Equipment Status Request 詢問機台SVID List的參數值 </summary>
        public static void PrimaryIn_S1F3(SecsEvents_PrimaryInEvent e)
        {
            int iQuery_SVID_Count;
            string[] sQuery_SVID_List = new string[1];

            try
            {
                iQuery_SVID_Count = e.trans.Primary.Item[1].Length;
                sQuery_SVID_List = new string[iQuery_SVID_Count];
                if (iQuery_SVID_Count != 0)
                {
                    for (int i = 1; i <= iQuery_SVID_Count; i++)
                    {
                        sQuery_SVID_List[i - 1] = Convert.ToString(e.trans.Primary.Item[1].Item[i].Value);
                    }
                }
                else
                {
                    int k = 0;
                    sQuery_SVID_List = new string[ucHost_SECS.GetSingleton().m_lstSVID.Count];
                    foreach (KeyValuePair<int, ucHost_SECS.SVID> svid in ucHost_SECS.GetSingleton().m_lstSVID)
                    {
                        sQuery_SVID_List[k] = svid.Value.ID.ToString();
                        k++;
                    }

                }

                SecondaryOutFucntion.SecondaryOut_S1F4(e, sQuery_SVID_List);
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S1F3()\r\n" + ex.ToString());
                return;
            }
        }

        /// <summary> Host 下S1F11給EQP，回應SecondaryOut_S1F12() </summary>
        /// <summary> S1F11 : Status Variable Namelist Reply 詢問機台SVID List的參數細項(名稱、值) </summary>
        public static void PrimaryIn_S1F11(SecsEvents_PrimaryInEvent e)
        {
            string sQuerySVID = "";
            try
            {
                if (e.trans.Primary.Length == 0)
                {
                    SecondaryOutFucntion.SecondaryOut_S1F12(e, "");
                }
                else
                {
                    sQuerySVID = Convert.ToString(e.trans.Primary.Item[1].Item[1].Value);
                    SecondaryOutFucntion.SecondaryOut_S1F12(e, sQuerySVID);
                }
            }
            catch (Exception ex)
            {
                SecondaryOutFucntion.SecondaryOut_S1F12(e, "");
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S1F11()\r\n" + ex.ToString());
                return;
            }

        }

        /// <summary> Host 下S1F13給EQP，回應SecondaryOut_S1F14() </summary>
        /// <summary> S1F13 : Establish Communications Request 建立連線需求 </summary>
        public static void PrimaryIn_S1F13(SecsEvents_PrimaryInEvent e)
        {
            SecondaryOutFucntion.SecondaryOut_S1F14(e);
        }

        /// <summary> Host 下S1F15給EQP，回應SecondaryOut_S1F16() </summary>
        /// <summary> S1F15 : Request OFF-LINE 要求離線 </summary>
        public static void PrimaryIn_S1F15(SecsEvents_PrimaryInEvent e)
        {
            SecondaryOutFucntion.SecondaryOut_S1F16(e);
        }

        /// <summary> Host 下S1F17給EQP，回應SecondaryOut_S1F18() </summary>
        /// <summary> S1F17 : Request ON-LINE 申請恢復連線 </summary>
        public static void PrimaryIn_S1F17(SecsEvents_PrimaryInEvent e)
        {
            SecondaryOutFucntion.SecondaryOut_S1F18(e);
        }

        #endregion

        #region S2FX Function (Stream 2 Equipment Control and Diagnostics) -  PrimaryIn

        /// <summary> Host 下S2F13給EQP，回應SecondaryOut_S2F14() </summary>
        /// <summary> S2F13 : Equipment Constant Request 詢問(指定/全部)機台不By Recipe裡面的參數ECID Value </summary>
        public static void PrimaryIn_S2F13(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                if (e.trans.Primary.Length == 0)
                {
                    SecondaryOutFucntion.SecondaryOut_S2F14(e, new string[0]);
                }
                else
                {
                    int QueryCount = e.trans.Primary.Item[1].ItemCount;
                    string[] QueryECIDs = new string[QueryCount];

                    for (int i = 0; i < QueryCount; i++)
                    {
                        QueryECIDs[i] = e.trans.Primary.Item[1].Item[i + 1].Value.ToString();
                    }
                    SecondaryOutFucntion.SecondaryOut_S2F14(e, QueryECIDs);
                }
            }
            catch (Exception ex)
            {
                SecondaryOutFucntion.SecondaryOut_S2F14(e, new string[0]);
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S2F13()\r\n" + ex.ToString());
                return;
            }
        }

        /// <summary> Host 下S2F15給EQP，回應SecondaryOut_S2F16() </summary>
        /// <summary> S2F15 : Equipment Constant Request 寫入(指定/全部)機台不By Recipe裡面的參數ECID Value </summary>
        public static void PrimaryIn_S2F15(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                if (e.trans.Primary.Length == 0)
                {
                    SecondaryOutFucntion.SecondaryOut_S2F16(e);
                }
                else
                {
                    int Num = e.trans.Primary.Item[1].Length;

                    string[] EditECID = new string[Num];
                    string[] EditValue = new string[Num];
                    for (int i = 0; i < Num; i++)
                    {
                        EditECID[i] = e.trans.Primary.Item[1].Item[i + 1].Item[1].Value.ToString();
                        EditValue[i] = e.trans.Primary.Item[1].Item[i + 1].Item[2].Value.ToString();
                    }

                    SecondaryOutFucntion.SecondaryOut_S2F16(e, EditECID, EditValue);
                }
            }
            catch (Exception ex)
            {
                SecondaryOutFucntion.SecondaryOut_S2F16(e);
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S2F15()\r\n" + ex.ToString());
                return;
            }

        }

        /// <summary> Host 下S2F17給EQP，回應SecondaryOut_S2F18() </summary>
        /// <summary> S2F17 : Date and Time Request 詢問設備時間 </summary>
        public static void PrimaryIn_S2F17(SecsEvents_PrimaryInEvent e)
        {
            SecondaryOutFucntion.SecondaryOut_S2F18(e);
        }

        /// <summary> Host 下S2F29給EQP，回應SecondaryOut_S2F30() </summary>
        /// <summary> S2F29 : Equipment Constant Namelist Request 詢問(指定/全部)機台不By Recipe裡面的參數ECID Value細項(名稱、值) </summary>
        public static void PrimaryIn_S2F29(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                if (e.trans.Primary.Length == 0)
                {
                    SecondaryOutFucntion.SecondaryOut_S2F30(e, new string[0]);
                }
                else
                {
                    string[] QueryECID = new string[e.trans.Primary.Item[1].ItemCount];

                    for (int i = 0; i < QueryECID.Length; i++)
                    {
                        QueryECID[i] = e.trans.Primary.Item[1].Item[i + 1].Value.ToString();
                    }

                    SecondaryOutFucntion.SecondaryOut_S2F30(e, QueryECID);
                }
            }
            catch (Exception ex)
            {
                SecondaryOutFucntion.SecondaryOut_S2F30(e, new string[0]);
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S2F29()\r\n" + ex.ToString());
                return;
            }
        }

        /// <summary> Host 下S2F31給EQP，回應SecondaryOut_S2F32() </summary>
        /// <summary> S2F31 : Date and Time Set Request 設定機台電腦時間 </summary>
        public static void PrimaryIn_S2F31(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                string DateTime = e.trans.Primary.Item[1].Value.ToString();
                DateTime dt1 = new System.DateTime(int.Parse(DateTime.Substring(0, 4)),
                                                   int.Parse(DateTime.Substring(4, 2)),
                                                   int.Parse(DateTime.Substring(6, 2)),
                                                   int.Parse(DateTime.Substring(8, 2)),
                                                   int.Parse(DateTime.Substring(10, 2)),
                                                   int.Parse(DateTime.Substring(12, 2))//,
                                                                                       //int.Parse(DateTime.Substring(14, 2))
                                                   );

                System.Diagnostics.ProcessStartInfo PstartInfoCmd1 = new System.Diagnostics.ProcessStartInfo();
                PstartInfoCmd1.FileName = "cmd.exe";
                PstartInfoCmd1.Arguments = "/C date " + dt1.ToShortDateString();
                PstartInfoCmd1.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                System.Diagnostics.Process proStep1 = new System.Diagnostics.Process();
                proStep1.StartInfo = PstartInfoCmd1;
                proStep1.Start();

                System.Diagnostics.ProcessStartInfo PstartInfoCmd2 = new System.Diagnostics.ProcessStartInfo();
                PstartInfoCmd2.FileName = "cmd.exe";
                PstartInfoCmd2.Arguments = "/C time " + dt1.ToString("HH:mm:ss");
                PstartInfoCmd2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                System.Diagnostics.Process proStep2 = new System.Diagnostics.Process();
                proStep2.StartInfo = PstartInfoCmd2;
                proStep2.Start();

                SecondaryOutFucntion.SecondaryOut_S2F32(e, 0);
            }
            catch (Exception ex)
            {
                SecondaryOutFucntion.SecondaryOut_S2F32(e, 1);
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S2F31()\r\n" + ex.ToString());
            }
        }

        /// <summary> Host 下S2F33給EQP，回應SecondaryOut_S2F34() </summary>
        /// <summary> S2F33 : Define Report 定義/移除 要綁定SVID或要移除SVID的Report(RPTID) </summary>
        public static void PrimaryIn_S2F33(SecsEvents_PrimaryInEvent e)
        {
            //0 = Accepted
            //1 = Denied. Insufficient space
            //2 = Denied. Invalid format
            //3 = Denied. At least on RPTID already defined
            //4 = Denied. At least on VID does not exist

            try
            {
                bool bDefReport = true;
                if (e.trans.Primary.Item[1].Item[2].Length > 0)
                    bDefReport = true;
                else
                    bDefReport = false;

                if (bDefReport)
                {
                    #region Defined Report
                    int iRPTCount = e.trans.Primary.Item[1].Item[2].Length;
                    for (int i = 0; i < iRPTCount; i++)
                    {
                        int ID = int.Parse(Convert.ToString(e.trans.Primary.Item[1].Item[2].Item[i + 1].Item[1].Value));
                        int iVIDCount = e.trans.Primary.Item[1].Item[2].Item[i + 1].Item[2].Length;

                        if (iVIDCount > 0)
                        {
                            string[] sVID = new string[iVIDCount];
                            for (int j = 0; j < iVIDCount; j++)
                            {
                                sVID[j] = Convert.ToString(e.trans.Primary.Item[1].Item[2].Item[i + 1].Item[2].Item[j + 1].Value);
                                if (ucHost_SECS.GetSingleton().m_lstSVID.ContainsKey(int.Parse(sVID[j])) == false && ucHost_SECS.GetSingleton().m_lstDVID.ContainsKey(int.Parse(sVID[j])) == false)
                                {
                                    SecondaryOutFucntion.SecondaryOut_S2F34(e, 4);// Fail SVID
                                    return;
                                }
                            }
                            ucHost_SECS.RPTID rptid = new ucHost_SECS.RPTID(ID);
                            if (ucHost_SECS.GetSingleton().m_lstRPTID.ContainsKey(ID) == true)// RPTID重複定義
                            {
                                SecondaryOutFucntion.SecondaryOut_S2F34(e, 3);
                                return;
                            }
                            for (int j = 0; j < iVIDCount; j++)
                            {
                                rptid.lstVID.Add(int.Parse(sVID[j]));
                            }
                            ucHost_SECS.GetSingleton().m_lstRPTID.Add(ID, rptid);
                        }
                        else
                        {
                            #region Delete Report
                            ucHost_SECS.GetSingleton().m_lstRPTID.Clear();
                            #endregion
                        }
                    }
                    SecondaryOutFucntion.SecondaryOut_S2F34(e, 0);
                    #endregion
                }
                else
                {
                    #region Delete Report
                    ucHost_SECS.GetSingleton().m_lstRPTID.Clear();
                    SecondaryOutFucntion.SecondaryOut_S2F34(e, 0);
                    #endregion
                }
                ucHost_SECS.GetSingleton().DefinedReportToTxtFile();
                ucHost_SECS.GetSingleton().UpdateDGV(ucHost_SECS.GetSingleton().dgvDVID);
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S2F33()\r\n" + ex.ToString());
                SecondaryOutFucntion.SecondaryOut_S2F34(e, 2);
            }
        }

        /// <summary> Host 下S2F35給EQP，回應SecondaryOut_S2F36() </summary>
        /// <summary> S2F35 : Link Event Report 對多個CEID(指定/移除)RPTID進行綁定Link起來 </summary>
        public static void PrimaryIn_S2F35(SecsEvents_PrimaryInEvent e)
        {
            //0 = Accepted
            //1 = Denied. Insufficient space
            //2 = Denied. Invalid format
            //3 = Denied. At least one CEID link already denied.
            //4 = Denied. At least one CEID does not exist.
            //5 = Denied. At least on RPTID does not exist.
            //>5 = Other errors

            try
            {
                bool bDefLink = true;
                if (e.trans.Primary.Item[1].Item[2].Length > 0)
                    bDefLink = true;
                else
                    bDefLink = false;

                if (bDefLink)
                {
                    #region Link Report
                    int rptCount = 0;
                    int iLinkCount = e.trans.Primary.Item[1].Item[2].Length;
                    int[] ceid = new int[iLinkCount];
                    string[] rptid = new string[iLinkCount];

                    for (int i = 0; i < iLinkCount; i++)
                    {
                        rptCount = int.Parse(Convert.ToString(e.trans.Primary.Item[1].Item[2].Item[i + 1].Item[2].Length));
                        ceid[i] = int.Parse(Convert.ToString(e.trans.Primary.Item[1].Item[2].Item[i + 1].Item[1].Value));

                        if (rptCount > 0)   //0329 Jine add
                        {
                            for (int j = 1; j <= rptCount; j++)
                            {
                                rptid[i] += Convert.ToString(e.trans.Primary.Item[1].Item[2].Item[i + 1].Item[2].Item[j].Value) + ",";
                            }

                            for (int j = 1; j <= rptCount; j++)
                            {
                                if (ucHost_SECS.GetSingleton().m_lstRPTID.ContainsKey(int.Parse(Convert.ToString(e.trans.Primary.Item[1].Item[2].Item[i + 1].Item[2].Item[j].Value))) == false)
                                {
                                    SecondaryOutFucntion.SecondaryOut_S2F36(e, 5);
                                    return;
                                }
                            }

                            if (ucHost_SECS.GetSingleton().m_lstCEID.ContainsKey(ceid[i]) == false)
                            {
                                SecondaryOutFucntion.SecondaryOut_S2F36(e, 4);
                                return;
                            }
                            if (ucHost_SECS.GetSingleton().m_lstCEID[ceid[i]].lstLinkReport.Count != 0)
                            {
                                // Event already defined report
                                SecondaryOutFucntion.SecondaryOut_S2F36(e, 3);
                                return;
                            }
                        }
                        else
                        {
                            #region Unlink Report
                            ucHost_SECS.GetSingleton().UnLinkAllEventReport();
                            #endregion
                        }
                    }

                    string[] split_dot = { "," };

                    for (int i = 0; i < iLinkCount; i++)
                    {
                        rptCount = int.Parse(Convert.ToString(e.trans.Primary.Item[1].Item[2].Item[i + 1].Item[2].Length));
                        if (rptCount > 0)   //0329 Jine add
                        {
                            string[] everRPT = rptid[i].Split(split_dot, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < everRPT.Length; j++)
                            {
                                ucHost_SECS.GetSingleton().m_lstCEID[ceid[i]].LinkReport(int.Parse(everRPT[j]));
                            }
                        }
                        else
                        {
                            ucHost_SECS.GetSingleton().m_lstCEID[ceid[i]].DeleteAllReport();   //0329 Jine add
                        }
                    }

                    SecondaryOutFucntion.SecondaryOut_S2F36(e, 0);
                    #endregion
                }
                else
                {
                    #region Unlink Report
                    ucHost_SECS.GetSingleton().UnLinkAllEventReport();
                    SecondaryOutFucntion.SecondaryOut_S2F36(e, 0);
                    #endregion
                }
                ucHost_SECS.GetSingleton().LinkReportToTxtFile();
                ucHost_SECS.GetSingleton().UpdateDGV(ucHost_SECS.GetSingleton().dgvCEID);
            }
            catch (Exception ex)
            {
                SecondaryOutFucntion.SecondaryOut_S2F36(e, 2);
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S2F35()\r\n" + ex.ToString());
            }
        }

        /// <summary> Host 下S2F37給EQP，回應SecondaryOut_S2F38() </summary>
        /// <summary> S2F37 : Enable/Disable Event Report 指定(所有/單一)CEID(Event)=Enable/Disable </summary>
        public static void PrimaryIn_S2F37(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                int index = 0;
                ucHost_SECS.GetSingleton().m_SecsState.bIsEventSendEable = Convert.ToBoolean(e.trans.Primary.Item[1].Item[1].Value);
                int iCEIDCount = e.trans.Primary.Item[1].Item[2].Length;
                string SaveEnableStatus = "";

                if (iCEIDCount > 0)
                {
                    //指定特定CEID Enable/Disable
                    for (int i = 1; i <= iCEIDCount; i++)
                    {
                        int CEID_Value = Convert.ToInt32(e.trans.Primary.Item[1].Item[2].Item[i].Value);
                        index = 0;
                        foreach (KeyValuePair<int, ucHost_SECS.CEID> item in ucHost_SECS.GetSingleton().m_lstCEID)
                        {
                            if (CEID_Value == item.Key)
                            {
                                ucHost_SECS.GetSingleton().m_lstCEID_Enable[index] = ucHost_SECS.GetSingleton().m_SecsState.bIsEventSendEable;
                            }
                            index++;
                        }
                    }
                }
                else
                {
                    //指定所有CEID Enable/Disable
                    for (int j = 0; j < ucHost_SECS.GetSingleton().m_lstCEID_Enable.Count; j++)
                    {
                        ucHost_SECS.GetSingleton().m_lstCEID_Enable[j] = ucHost_SECS.GetSingleton().m_SecsState.bIsEventSendEable;
                    }
                }

                for (int k = 0; k < ucHost_SECS.GetSingleton().m_lstCEID_Enable.Count; k++)
                {
                    if (ucHost_SECS.GetSingleton().m_lstCEID_Enable[k] == true)
                    {
                        SaveEnableStatus = SaveEnableStatus + "1";
                    }
                    else
                    {
                        SaveEnableStatus = SaveEnableStatus + "0";
                    }
                }

                ucParameter.SaveValue(clsEnum.enuPmtType.System, clsEnum.enuPmtName.Sys_SecsEventEnable, SaveEnableStatus);
                SecondaryOutFucntion.SecondaryOut_S2F38(e, 0);
            }
            catch (Exception ex)
            {
                SecondaryOutFucntion.SecondaryOut_S2F38(e, 1);
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S2F37()\r\n" + ex.ToString());
            }
        }

        /// <summary> Host 下S2F41給EQP，回應SecondaryOut_S2F42() </summary>
        /// <summary> S2F41 : Host Command Send IT對機台下CMD指令 </summary>
        public static void PrimaryIn_S2F41(SecsEvents_PrimaryInEvent e)
        {
            //0x00 OK — the command with parameters orcommand with no parameters was valid. Thisvalue is returned when system is idle.
            //0x01 Invalid command—The equipment ignoresthe entire contents of the message.
            //0x02 Cannot execute the command(s) now—the equipment ignores the contents of the message.
            //0x03 Invalid parameter—at least oneparameter is invalid. The equipment ignoresthe entire contents of the message.
            //0x04 OK—command will be performed and later acknowledged with an event.
            //0x05 Command already executing.

            string sRCMD = "";
            try
            {
                sRCMD = Convert.ToString(e.trans.Primary.Item[1].Item[1].Value);
                SECSAP.SecsRCMDParse(e, sRCMD);
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S2F41()" + " at " + sRCMD + "\r\n" + ex.ToString());

            }
        }

        #endregion

        #region S5FX Function (Stream 5 Exception Handling) - PrimaryIn

        /// <summary> Host 下S5F3給EQP，回應SecondaryOut_S5F4() </summary>
        /// <summary> S5F3 : Enable/Disable Alarm IT指定(所有/單一)Alarm=Enable/Disable </summary>
        public static void PrimaryIn_S5F3(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                int Alm_Count = 0;
                string value = Convert.ToString(e.trans.Primary.Item[1].Item[1].Value);
                byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);
                string str_ALID = Convert.ToString(e.trans.Primary.Item[1].Item[2].Value);
                string SaveEnableStatus = "";

                if (BinValue[0] == 128)
                {
                    ucHost_SECS.GetSingleton().m_SecsState.bIsAlarmSendEable = true;
                }
                else
                {
                    ucHost_SECS.GetSingleton().m_SecsState.bIsAlarmSendEable = false;
                }

                if (str_ALID != "")
                {
                    //指定單一Alarm Enable/Disable
                    foreach (string str in ucHost_SECS.GetSingleton().m_lstALARM)
                    {
                        if (str == str_ALID)
                        {
                            ucHost_SECS.GetSingleton().m_lstALARM_Enable[Alm_Count] = ucHost_SECS.GetSingleton().m_SecsState.bIsAlarmSendEable;
                        }

                        if (ucHost_SECS.GetSingleton().m_lstALARM_Enable[Alm_Count] == true)
                        {
                            SaveEnableStatus = SaveEnableStatus + "1";
                        }
                        else
                        {
                            SaveEnableStatus = SaveEnableStatus + "0";
                        }
                        Alm_Count++;
                    }
                }
                else
                {
                    //指定所有Alarm Enable/Disable
                    foreach (string str in ucHost_SECS.GetSingleton().m_lstALARM)
                    {
                        ucHost_SECS.GetSingleton().m_lstALARM_Enable[Alm_Count] = ucHost_SECS.GetSingleton().m_SecsState.bIsAlarmSendEable;

                        if (ucHost_SECS.GetSingleton().m_lstALARM_Enable[Alm_Count] == true)
                        {
                            SaveEnableStatus = SaveEnableStatus + "1";
                        }
                        else
                        {
                            SaveEnableStatus = SaveEnableStatus + "0";
                        }
                        Alm_Count++;
                    }
                }

                ucParameter.SaveValue(clsEnum.enuPmtType.System, clsEnum.enuPmtName.Sys_SecsAlarmEnable, SaveEnableStatus);
                SecondaryOutFucntion.SecondaryOut_S5F4(e);
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S5F3()\r\n" + ex.ToString());
            }

        }

        /// <summary> Host 下S5F5給EQP，回應SecondaryOut_S5F6() </summary>
        /// <summary> S5F5 : List Alarms Request 詢問(單一或全部)Alarm Information </summary>
        public static void PrimaryIn_S5F5(SecsEvents_PrimaryInEvent e)
        {
            try
            {

                string ID = e.trans.Primary.Item[1].Value.ToString();
                SecondaryOutFucntion.SecondaryOut_S5F6(e, int.Parse(ID), false);
            }
            catch (Exception ex)
            {

                SecondaryOutFucntion.SecondaryOut_S5F6(e, 0, true);
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S5F5()\r\n" + ex.ToString());
            }

        }

        /// <summary> Host 下S5F7給EQP，回應SecondaryOut_S5F8() </summary>
        /// <summary> S5F7 : List Enabled Alarm Request 詢問(單一或全部)已啟用Alarm Information </summary>
        public static void PrimaryIn_S5F7(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                SecondaryOutFucntion.SecondaryOut_S5F8(e);
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S5F7()\r\n" + ex.ToString());
            }

        }

        #endregion

        #region S6FX Function (Stream 6 Data Collection) - PrimaryIn

        /// <summary> Host 下S6F15給EQP，回應SecondaryOut_S6F16() </summary>
        /// <summary> S6F15 : Event Report Request 詢問CEID內指定的所有RPTID的VID與值 </summary>
        public static void PrimaryIn_S6F15(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                string ID = e.trans.Primary.Item[1].Value.ToString();
                SecondaryOutFucntion.SecondaryOut_S6F16(e, int.Parse(ID));
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S1F3()\r\n" + ex.ToString());
                return;
            }
        }


        /// <summary> Host 下S6F19給EQP，回應SecondaryOut_S6F20() </summary>
        /// <summary> S6F19 : Individual Report Request 詢問RPTID的VID與值 </summary>
        public static void PrimaryIn_S6F19(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                string ID = e.trans.Primary.Item[1].Value.ToString();
                SecondaryOutFucntion.SecondaryOut_S6F20(e, int.Parse(ID));
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S6F19()\r\n" + ex.ToString());
                return;
            }
        }

        #endregion

        #region S7FX Function (Stream 7 Process Program Management) - PrimaryIn

        /// <summary> Host 下S7F1給EQP，回應SecondaryOut_S7F2() </summary>
        /// <summary> S7F1 : Process Program Load Inquire 一般是設定RecipeName長度上限,卡控條件會依IT需求變化 </summary>
        public static void PrimaryIn_S7F1(SecsEvents_PrimaryInEvent e)
        {
            string ppid = "";
            List<string> lstPPRAM = new List<string>();
            try
            {
                string[] RecipeNameList = System.IO.Directory.GetFiles("D:\\Parameter\\Recipe");
                ppid = Convert.ToString(e.trans.Primary.Item[1].Item[1].Value);

                SecondaryOutFucntion.SecondaryOut_S7F2(e, ppid, 0);
            }
            catch (Exception ex)
            {
                SecondaryOutFucntion.SecondaryOut_S7F2(e, ppid, 3);
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S7F1()\r\n" + ex.ToString());
                return;
            }
        }

        /// <summary> Host 下S7F3給EQP，回應SecondaryOut_S7F4() </summary>
        /// <summary> S7F3 : Process Program Send IT會下Download要設定的RecipeFile或參數，有兩種格式1.壓縮檔Zip 2.參數資料 </summary>
        public static void PrimaryIn_S7F3(SecsEvents_PrimaryInEvent e)
        {
            string ppid = "";
            bool IsRecipeExist = false;
            try
            {
                string[] RecipeNameList = System.IO.Directory.GetFiles("D:\\Parameter\\Recipe");
                ppid = Convert.ToString(e.trans.Primary.Item[1].Item[1].Value);
                string S7F3_GetPPbodyData = "";

                S7F3_GetPPbodyData = e.trans.Primary.Item[1].Item[2].Value.ToString();

                String[] strDataSplit = Regex.Split(S7F3_GetPPbodyData, ":");

                //尋找Recipe是否已存在
                foreach (string Recipe_Name in RecipeNameList)
                {
                    string str = "";
                    FileInfo fileInfo = new FileInfo(Recipe_Name);
                    str = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                    if (str == ppid)
                    {
                        IsRecipeExist = true;
                    }
                }

                string HostSendParamName = "", HostSendParamValue = "";

                if (IsRecipeExist)
                {
                    //Recipe已存在, 修改參數數值
                    FileInfo fileInfo = new FileInfo(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe));
                    string OriginRecipeName = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                    ucHost_SECS.AutoChangeRecipe(ppid, false);

                    for (int i = 0; i <= strDataSplit.Length - 1; i++)
                    {
                        String[] strValueSplit = Regex.Split(strDataSplit[i], ",");
                        HostSendParamName = strValueSplit[1];
                        HostSendParamValue = strValueSplit[4];
                        SaveNewRecipeParam(HostSendParamName, HostSendParamValue);
                    }

                    ucHost_SECS.AutoChangeRecipe(OriginRecipeName, false);
                }
                else
                {
                    //Recipe不存在, 複製建立一支新Recipe
                    FileInfo fileInfo = new FileInfo(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe));
                    string OriginRecipeName = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                    System.IO.File.Copy(fileInfo.DirectoryName + "\\" + OriginRecipeName + ".ini",
                                        fileInfo.DirectoryName + "\\" + ppid + ".ini",
                                        true);
                    ucHost_SECS.AutoChangeRecipe(ppid, false);

                    for (int i = 0; i <= strDataSplit.Length - 1; i++)    //要減掉CCODE, 故從2開始
                    {
                        String[] strValueSplit = Regex.Split(strDataSplit[i], ",");
                        HostSendParamName = strValueSplit[1];
                        HostSendParamValue = strValueSplit[4];
                        SaveNewRecipeParam(HostSendParamName, HostSendParamValue);
                    }

                    ucHost_SECS.AutoChangeRecipe(OriginRecipeName, false);
                }

                SecondaryOutFucntion.SecondaryOut_S7F4(e, ppid, 0);
            }
            catch (Exception ex)
            {
                SecondaryOutFucntion.SecondaryOut_S7F4(e, ppid, 3);
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S7F3()\r\n" + ex.ToString());
                return;
            }
        }

        /// <summary> Host 下S7F5給EQP，回應SecondaryOut_S7F6() </summary>
        /// <summary> S7F5 : Process Program Request IT要求要上傳的RecipeFile或參數，有兩種格式1.壓縮檔Zip 2.參數資料 </summary>
        public static void PrimaryIn_S7F5(SecsEvents_PrimaryInEvent e)
        {
            string ppid = "";
            bool IsRecipeExist = false;
            string[] RecipeNameList = System.IO.Directory.GetFiles("D:\\Parameter\\Recipe");
            try
            {
                ppid = Convert.ToString(e.trans.Primary.Item[1].Value);

                //尋找Recipe是否已存在
                foreach (string Recipe_Name in RecipeNameList)
                {
                    string str = "";
                    FileInfo fileInfo = new FileInfo(Recipe_Name);
                    str = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                    if (str == ppid)
                    {
                        IsRecipeExist = true;
                    }
                }

                if (IsRecipeExist)
                {
                    SecondaryOutFucntion.SecondaryOut_S7F6(e, ppid);
                }
                else
                {
                    ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Recipe is not Exist at PrimaryIn_S7F5()\r\n");
                }
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S7F5()\r\n" + ex.ToString());
                return;
            }
        }

        /// <summary> Host 下S7F17給EQP，回應SecondaryOut_S7F18() </summary>
        /// <summary> S7F17 : Delete Process Program Send IT要求刪除RecipeFile </summary>
        public static void PrimaryIn_S7F17(SecsEvents_PrimaryInEvent e)
        {
            string ppid = "";
            try
            {
                ppid = Convert.ToString(e.trans.Primary.Item[1].Item[1].Value);
                SecondaryOutFucntion.SecondaryOut_S7F18(e, ppid);
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S7F17()\r\n" + ex.ToString());
                return;
            }

        }

        /// <summary> Host 下S7F19給EQP，回應SecondaryOut_S7F20() </summary>
        /// <summary> S7F19 : Current Process Program Dir Request IT詢問機台所有的Recipe Name List </summary>
        public static void PrimaryIn_S7F19(SecsEvents_PrimaryInEvent e)
        {
            ucHost_SECS.SecsGetRecipeList();
            SecondaryOutFucntion.SecondaryOut_S7F20(e);
        }

        /// <summary> Host 下S7F23給EQP，回應SecondaryOut_S7F24() </summary>
        /// <summary> S7F23 : Formatted Process Program Send IT會下Download Recipe或修改Recipe內單一要設定的參數 </summary>
        public static void PrimaryIn_S7F23(SecsEvents_PrimaryInEvent e)
        {
            string ppid = "";
            List<string> lstPPRAM = new List<string>();
            bool IsRecipeExist = false;
            try
            {
                string[] RecipeNameList = System.IO.Directory.GetFiles("D:\\Parameter\\Recipe");

                ppid = Convert.ToString(e.trans.Primary.Item[1].Item[1].Value);
                int ItemPPBodyCount = e.trans.Primary.Item[1].Item[4].ItemCount;

                //尋找Recipe是否已存在
                foreach (string Recipe_Name in RecipeNameList)
                {
                    string str = "";
                    FileInfo fileInfo = new FileInfo(Recipe_Name);
                    str = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                    if (str == ppid)
                    {
                        IsRecipeExist = true;
                    }
                }

                string HostSendParamName = "", HostSendParamValue = "";

                if (IsRecipeExist)
                {
                    //Recipe已存在, 修改參數數值
                    FileInfo fileInfo = new FileInfo(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe));
                    string OriginRecipeName = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                    ucHost_SECS.AutoChangeRecipe(ppid, false);

                    for (int i = 1; i < ItemPPBodyCount; i++)
                    {
                        int ItemCount = e.trans.Primary.Item[1].Item[4].Item[i].Item[2].ItemCount;

                        for (int j = 1; j < ItemCount; j++)
                        {
                            switch (j)
                            {
                                case 1:
                                    HostSendParamName = Convert.ToString(e.trans.Primary.Item[1].Item[4].Item[i].Item[2].Item[1].Value);
                                    break;
                                case 4:
                                    HostSendParamValue = Convert.ToString(e.trans.Primary.Item[1].Item[4].Item[i].Item[2].Item[4].Value);
                                    SaveNewRecipeParam(HostSendParamName, HostSendParamValue);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    ucHost_SECS.AutoChangeRecipe(OriginRecipeName, false);
                }
                else
                {
                    //Recipe不存在, 複製建立一支新Recipe
                    FileInfo fileInfo = new FileInfo(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe));
                    string OriginRecipeName = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                    System.IO.File.Copy(fileInfo.DirectoryName + "\\" + OriginRecipeName + ".ini",
                                        fileInfo.DirectoryName + "\\" + ppid + ".ini",
                                        true);
                    ucHost_SECS.AutoChangeRecipe(ppid, false);

                    for (int i = 1; i < ItemPPBodyCount; i++)
                    {
                        int ItemCount = e.trans.Primary.Item[1].Item[4].Item[i].Item[2].ItemCount;

                        for (int j = 1; j < ItemCount; j++)
                        {
                            switch (j)
                            {
                                case 1:
                                    HostSendParamName = Convert.ToString(e.trans.Primary.Item[1].Item[4].Item[i].Item[2].Item[1].Value);
                                    break;
                                case 4:
                                    HostSendParamValue = Convert.ToString(e.trans.Primary.Item[1].Item[4].Item[i].Item[2].Item[4].Value);
                                    SaveNewRecipeParam(HostSendParamName, HostSendParamValue);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    ucHost_SECS.AutoChangeRecipe(OriginRecipeName, false);
                }

                SecondaryOutFucntion.SecondaryOut_S7F24(e, ppid, lstPPRAM, 0);
            }
            catch (Exception ex)
            {
                SecondaryOutFucntion.SecondaryOut_S7F24(e, ppid, lstPPRAM, 3);
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S7F23()\r\n" + ex.ToString());
                return;
            }
        }

        public static void SaveNewRecipeParam(string ParamName, string value)
        {
            try
            {
                clsEnum.enuPmtName ePmtName = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), ParamName, true);
                ucParameter.SaveValue(clsEnum.enuPmtType.Recipe, ePmtName, value);
            }
            catch { }
        }

        /// <summary> Host 下S7F25給EQP，回應SecondaryOut_S7F25() </summary>
        /// <summary> S7F25 : Formatted Process Program Request IT下詢問指定Reicpe的內容物 </summary>
        public static void PrimaryIn_S7F25(SecsEvents_PrimaryInEvent e)
        {
            string ppid = "";
            bool IsRecipeExist = false;
            string[] RecipeNameList = System.IO.Directory.GetFiles("D:\\Parameter\\Recipe");
            try
            {
                ppid = Convert.ToString(e.trans.Primary.Item[1].Value);

                //尋找Recipe是否已存在
                foreach (string Recipe_Name in RecipeNameList)
                {
                    string str = "";
                    FileInfo fileInfo = new FileInfo(Recipe_Name);
                    str = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                    if (str == ppid)
                    {
                        IsRecipeExist = true;
                    }
                }

                if (IsRecipeExist)
                {
                    SecondaryOut_S7F26(e, ppid);
                }
                else
                {
                    ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Recipe is not Exist at PrimaryIn_S7F25()\r\n");
                }
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S7F25()\r\n" + ex.ToString());
                return;
            }
        }

        /// <summary> S7F26 : Formatted Process Program Data 回覆Recipe Name內容的資料 </summary>
        public static void SecondaryOut_S7F26(SecsEvents_PrimaryInEvent e, string _PPID)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe));
                string OriginRecipeName = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                ucHost_SECS.AutoChangeRecipe(_PPID, false);

                e.trans.Secondary.Item[1].Item[1].Value = _PPID;
                e.trans.Secondary.Item[1].Item[2].Value = ucHost_SECS.GetSingleton().sMDLN;
                e.trans.Secondary.Item[1].Item[3].Value = ucHost_SECS.GetSingleton().sVersion;

                string RecipeName;
                RecipeName = System.IO.Path.GetFileName(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe));
                RecipeName = RecipeName.Substring(0, RecipeName.Length - 4);//省略副檔名

                e.trans.Secondary.Item[1].Item[4].Item[1].Item[1].Format = FormatConstants.wsFormatU4;
                e.trans.Secondary.Item[1].Item[4].Item[1].Item[1].Value = 1;

                #region 上報機台Recipe參數

                int Index = 1;
                foreach (string item in ucHost_SECS.GetSingleton().m_lstPPBody)
                {
                    //string _path = PM.GetSingleton().ModuleRecipeName;
                    string _path = "";

                    string[] str = item.Split(';');
                    string PPBody_ID = str[0];
                    string PPBody_Name = str[1];
                    string PPBody_Unit = str[2];
                    string PPBody_Format = str[3];
                    string PPBody_Min = str[4];
                    string PPBody_Max = str[5];
                    clsModuleEnum.enuPmtName ePmtName = (clsModuleEnum.enuPmtName)Enum.Parse(typeof(clsModuleEnum.enuPmtName), PPBody_Name, true);
                    string PPBody_Value = clsProcCtrl.GetSingleton().GetRecipetInfo(_path).GetValueString(ePmtName);

                    if (Index == 1)
                    {
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[1].Name = "CCODE";
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[1].Format = FormatConstants.wsFormatU4;
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[1].Value = PPBody_ID;

                        //NAME
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[1].Name = "PPARM";
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[1].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[1].Value = PPBody_Name;
                        //Unit
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].AddNew(2);
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[2].Name = "PPARM";
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[2].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[2].Value = PPBody_Unit;
                        //Format
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].AddNew(3);
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[3].Name = "PPARM";
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[3].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[3].Value = PPBody_Format;
                        //Value
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].AddNew(4);
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[4].Name = "PPARM";
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[4].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[4].Value = PPBody_Value;
                        //Min
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].AddNew(5);
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[5].Name = "PPARM";
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[5].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[5].Value = PPBody_Min;
                        //Max
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].AddNew(6);
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[6].Name = "PPARM";
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[6].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[4].Item[1].Item[2].Item[6].Value = PPBody_Max;

                    }
                    else
                    {
                        //創立節點List
                        e.trans.Secondary.Item[1].Item[4].AddNew(Index);
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Format = FormatConstants.wsFormatList;
                        //新增節點第一層Title與格式
                        e.trans.Secondary.Item[1].Item[4].Item[Index].AddNew(1);
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[1].Name = "CCODE";
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[1].Format = FormatConstants.wsFormatU4;
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[1].Value = PPBody_ID;
                        ////新增第二層List容器給Para塞資料
                        e.trans.Secondary.Item[1].Item[4].Item[Index].AddNew(2);
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Format = FormatConstants.wsFormatList;

                        //NAME
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].AddNew(1);
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[1].Name = "PPARM";
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[1].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[1].Value = PPBody_Name;
                        //Unit
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].AddNew(2);
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[2].Name = "PPARM";
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[2].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[2].Value = PPBody_Unit;
                        //Format
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].AddNew(3);
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[3].Name = "PPARM";
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[3].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[3].Value = PPBody_Format;
                        //Value
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].AddNew(4);
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[4].Name = "PPARM";
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[4].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[4].Value = PPBody_Value;
                        //Min
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].AddNew(5);
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[5].Name = "PPARM";
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[5].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[5].Value = PPBody_Min;
                        //Max
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].AddNew(6);
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[6].Name = "PPARM";
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[6].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[4].Item[Index].Item[2].Item[6].Value = PPBody_Max;
                    }
                    Index++;
                }

                #endregion

                ucHost_SECS.AutoChangeRecipe(OriginRecipeName, false);

                // Content Value
                e.trans.Reply();

                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S7F26]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S7F26]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S7F26()\r\n" + ex.ToString());
            }
        }

        #endregion

        #region S10FX Function (Stream 10 Terminal Services) - PrimaryIn

        /// <summary> Host 下S10F3給EQP，回應SecondaryOut_S10F4() </summary>
        /// <summary> S10F3 Terminal Display, Single 顯示Host給的相關訊息(單個) </summary>
        public static void PrimaryIn_S10F3(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                string TID = "";
                string Msg = "";
                string value = Convert.ToString(e.trans.Primary.Item[1].Item[1].Value);
                TID = value;

                Msg = Convert.ToString(e.trans.Primary.Item[1].Item[2].Value);
                ucHost_SECS.GetSingleton().TerminalDisplayTxt("TID: " + TID + ",  Msg: " + Msg);
                formSecsHostMsg.GetSingleton().ShowMessage(Msg);

            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S10F3()\r\n" + ex.ToString());
                return;
            }

            if (e.trans.ReplyExpected == true)
            {
                SecondaryOutFucntion.SecondaryOut_S10F4(e);
            }
            else { }
        }


        /// <summary> Host 下S10F5給EQP，回應SecondaryOut_S10F6() </summary>
        /// <summary> S10F5 Terminal Display, Multi-Block 顯示Host給的相關訊息(多個) </summary>
        public static void PrimaryIn_S10F5(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                int i = 0;
                int intMsgCount = 0;
                string TID = "";
                string strDisplayMsg = "";
                string value = Convert.ToString(e.trans.Primary.Item[1].Item[1].Value);
                TID = value;

                List<string> Msg = new List<string>();

                intMsgCount = e.trans.Primary.Item[1].Item[2].ItemCount;

                for (i = 0; i < intMsgCount; i++)
                {
                    Msg.Add(Convert.ToString(e.trans.Primary.Item[1].Item[2].Item[i + 1].Value));
                    strDisplayMsg = strDisplayMsg + Msg[i] + "\r\n";
                }

                ucHost_SECS.GetSingleton().TerminalDisplayTxt("TID: " + TID + ",  Msg: " + strDisplayMsg);
                formSecsHostMsg.GetSingleton().ShowMessage(strDisplayMsg);

                SecondaryOutFucntion.SecondaryOut_S10F6(e);

            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S10F5()\r\n" + ex.ToString());
            }
        }

        #endregion

    }
}

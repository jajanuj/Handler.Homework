using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ArtData;
using ArtCommonLib;
using ArtControlLib;
using AxWinSecsLib;
using WinSecsLib;
using System.Text.RegularExpressions;

namespace ArtEQ
{
    class SecondaryOutFucntion
    {
        #region S1FX Function (Stream 1 Equipment Status) - SecondaryOut

        public static void SecondaryOut_SXF0(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                e.trans.Secondary.Function = 0;
                e.trans.Secondary.Description = "Abort transaction";
                e.trans.Secondary.Item[1].Delete();
                e.trans.Reply();

                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [SXF0]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [SXF0]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S1F0()\r\n" + ex.ToString());
            }
        }

        /// <summary> S1F2 : 回覆(On Line Data (D)) </summary>
        public static void SecondaryOut_S1F2(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                // Content Value
                e.trans.Secondary.Item[1].Item[1].Format = FormatConstants.wsFormatAscii;
                e.trans.Secondary.Item[1].Item[1].Value = ucHost_SECS.GetSingleton().sMDLN;
                e.trans.Secondary.Item[1].Item[2].Format = FormatConstants.wsFormatAscii;
                e.trans.Secondary.Item[1].Item[2].Value = ucHost_SECS.GetSingleton().sVersion;
                e.trans.Reply();

                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S1F2]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S1F2]");

            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S1F2()\r\n" + ex.ToString());
            }
        }

        /// <summary> S1F4 : Selected Equipment Status Data 回應機台SVID List的參數值 </summary>
        public static void SecondaryOut_S1F4(SecsEvents_PrimaryInEvent e, string[] Query_SVID_List)
        {
            try
            {
                //UpdateSVID_Clock();

                //UpdateSVID_ProcessStatus();

                for (int i = 1; i < Query_SVID_List.Length; i++)
                {
                    e.trans.Secondary.Item[1].AddNew(i + 1);
                }
                for (int i = 1; i <= Query_SVID_List.Length; i++)
                {

                    if (Query_SVID_List[i - 1] != "")
                    {
                        e.trans.Secondary.Item[1].Item[i].Name = Query_SVID_List[i - 1];
                        if (ucHost_SECS.GetSingleton().m_lstSVID.ContainsKey(int.Parse(Query_SVID_List[i - 1])))
                        {
                            e.trans.Secondary.Item[1].Item[i].Value = ucHost_SECS.GetSingleton().m_lstSVID[int.Parse(Query_SVID_List[i - 1])].Value;
                        }
                        else
                        {
                            e.trans.Secondary.Item[1].Item[i].Value = "";
                        }
                    }
                    else
                    {
                        e.trans.Secondary.Item[1].Item[i].Name = "";
                        e.trans.Secondary.Item[1].Item[i].Value = "";
                    }
                }
                e.trans.Reply();
                // WriteLog
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S1F4]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S1F4]");

            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S1F4()\r\n" + ex.ToString());
            }
        }

        /// <summary> S1F12 : Status Variable Namelist Request 回覆機台SVID List的參數細項(名稱、值) </summary>
        public static void SecondaryOut_S1F12(SecsEvents_PrimaryInEvent e, string SVID)
        {
            try
            {
                // Content Value
                if (SVID == "")
                {
                    int index = 0;
                    foreach (KeyValuePair<int, ucHost_SECS.SVID> item in ucHost_SECS.GetSingleton().m_lstSVID)
                    {
                        if (index == 0)
                        {

                            e.trans.Secondary.Item[1].Item[1].Item[1].Format = FormatConstants.wsFormatI4;
                            e.trans.Secondary.Item[1].Item[1].Item[1].Value = item.Value.ID.ToString();
                            e.trans.Secondary.Item[1].Item[1].Item[2].Format = FormatConstants.wsFormatAscii;
                            e.trans.Secondary.Item[1].Item[1].Item[2].Value = item.Value.Name;
                            e.trans.Secondary.Item[1].Item[1].Item[3].Format = FormatConstants.wsFormatAscii;
                            e.trans.Secondary.Item[1].Item[1].Item[3].Value = item.Value.Value;
                        }
                        else
                        {
                            e.trans.Secondary.Item[1].Item[index].Duplicate();
                            e.trans.Secondary.Item[1].Item[index + 1].Item[1].Format = FormatConstants.wsFormatI4;
                            e.trans.Secondary.Item[1].Item[index + 1].Item[1].Value = item.Value.ID.ToString();
                            e.trans.Secondary.Item[1].Item[index + 1].Item[2].Format = FormatConstants.wsFormatAscii;
                            e.trans.Secondary.Item[1].Item[index + 1].Item[2].Value = item.Value.Name;
                            e.trans.Secondary.Item[1].Item[index + 1].Item[3].Format = FormatConstants.wsFormatAscii;
                            e.trans.Secondary.Item[1].Item[index + 1].Item[3].Value = item.Value.Value;
                        }
                        index++;
                    }
                    e.trans.Reply();
                }
                else if (ucHost_SECS.GetSingleton().m_lstSVID.ContainsKey(int.Parse(SVID)))
                {
                    e.trans.Secondary.Item[1].Item[1].Item[1].Format = FormatConstants.wsFormatI4;
                    e.trans.Secondary.Item[1].Item[1].Item[1].Value = SVID;
                    e.trans.Secondary.Item[1].Item[1].Item[2].Format = FormatConstants.wsFormatAscii;
                    e.trans.Secondary.Item[1].Item[1].Item[2].Value = ucHost_SECS.GetSingleton().m_lstSVID[int.Parse(SVID)].Name;
                    e.trans.Secondary.Item[1].Item[1].Item[3].Format = FormatConstants.wsFormatAscii;
                    e.trans.Secondary.Item[1].Item[1].Item[3].Value = ucHost_SECS.GetSingleton().m_lstSVID[int.Parse(SVID)].Value;
                    e.trans.Reply();
                }
                else
                {
                    e.trans.Reply();
                }
                // WriteLog
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S1F12]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S1F12]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S1F12()\r\n" + ex.ToString());
            }
        }

        /// <summary> S1F14 : Establish Communications Request Acknowledge 建立通信請求確認 </summary>
        public static void SecondaryOut_S1F14(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                e.trans.Secondary.Item[1].Item[1].Format = FormatConstants.wsFormatBinary;
                e.trans.Secondary.Item[1].Item[1].Value = Convert.ToByte(0);

                // Content Value
                e.trans.Secondary.Item[1].Item[2].Item[1].Format = FormatConstants.wsFormatAscii;
                e.trans.Secondary.Item[1].Item[2].Item[1].Value = ucHost_SECS.GetSingleton().sMDLN;
                e.trans.Secondary.Item[1].Item[2].Item[2].Format = FormatConstants.wsFormatAscii;
                e.trans.Secondary.Item[1].Item[2].Item[2].Value = ucHost_SECS.GetSingleton().sVersion;
                e.trans.Reply();

                // WriteLog
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S1F14]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S1F14]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S1F14()\r\n" + ex.ToString());
            }
        }

        /// <summary> S1F16 : OFF-LINE Acknowledge 離線確認 </summary>
        public static void SecondaryOut_S1F16(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                // Ack
                e.trans.Secondary.Item[1].Format = FormatConstants.wsFormatBinary;
                e.trans.Secondary.Item[1].Value = Convert.ToByte(0);
                ucHost_SECS.GetSingleton().m_SecsState.sControl_State_Value = "0";
                ucHost_SECS.GetSingleton().UpdateDGV(ucHost_SECS.GetSingleton().dgvSVID);
                e.trans.Reply();

                // WriteLog
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S1F16]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S1F16]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S1F16()\r\n" + ex.ToString());
            }
        }

        /// <summary> S1F18 : Request ON-LINE 恢復連線確認 </summary>
        public static void SecondaryOut_S1F18(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                // Ack
                e.trans.Secondary.Item[1].Format = FormatConstants.wsFormatBinary;
                e.trans.Secondary.Item[1].Value = Convert.ToByte(0);

                ucHost_SECS.GetSingleton().m_SecsState.sControl_State_Value = "2";
                ucHost_SECS.GetSingleton().UpdateDGV(ucHost_SECS.GetSingleton().dgvSVID);
                ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Control_State, "2");
                ucHost_SECS.GetSingleton().SECS_DispatchEvent((int)clsEnum.eSECS_CEID.Control_state_REMOTE);


                e.trans.Reply();

                // WriteLog
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S1F18]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S1F18]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S1F18()\r\n" + ex.ToString());
            }
        }

        #endregion

        #region S2FX Function (Stream 2 Equipment Control and Diagnostics) -  SecondaryOut

        /// <summary> S2F14 : Equipment Constant Data 回覆(指定/全部)機台不By Recipe裡面的參數ECID Value </summary>
        public static void SecondaryOut_S2F14(SecsEvents_PrimaryInEvent e, string[] _ECIDs)
        {
            try
            {
                SECSAP.UpdateAllECID();

                if (_ECIDs.Length == 0)
                {
                    int idx = 0;
                    _ECIDs = new string[ucHost_SECS.GetSingleton().m_lstECID.Count];
                    foreach (KeyValuePair<int, ucHost_SECS.ECID> item in ucHost_SECS.GetSingleton().m_lstECID)
                    {
                        _ECIDs[idx] = item.Value.ID.ToString();
                        idx++;
                    }
                }

                for (int i = 0; i < _ECIDs.Length - 1; i++)
                {
                    e.trans.Secondary.Item[1].Item[1].Duplicate();
                }

                for (int i = 0; i < _ECIDs.Length; i++)
                {
                    if (ucHost_SECS.GetSingleton().m_lstECID.ContainsKey(int.Parse(_ECIDs[i])) == true)
                    {
                        e.trans.Secondary.Item[1].Item[i + 1].Value = ucHost_SECS.GetSingleton().m_lstECID[int.Parse(_ECIDs[i])].Value;
                    }
                    else
                    {
                        e.trans.Secondary.Item[1].Item[i + 1].Value = "";
                    }
                }
                e.trans.Reply();
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S2F14]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S2F14]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S2F14()\r\n" + ex.ToString());
            }
        }

        /// <summary> S2F16 : New Equipment Constant Ack(設定(列表)ECID Value)  </summary>
        public static void SecondaryOut_S2F16(SecsEvents_PrimaryInEvent e, string[] _ECID, string[] _Value)
        {
            try
            {
                // Ack
                int iAck = 0;
                if (clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Initial
                    || clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Run
                    || clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Down
                    || clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Warning
                    || clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.EMS)
                {
                    iAck = 1;
                }
                else
                {

                    for (int i = 0; i < _ECID.Length; i++)
                    {

                        if (ucHost_SECS.GetSingleton().m_lstECID.ContainsKey(int.Parse(_ECID[i])) == false)
                        {
                            iAck = 1;
                            break;
                        }
                        else
                        {
                            SECSAP.SettingAllECID(_ECID[i], _Value[i]);
                            iAck = 0;
                        }
                    }
                }
                e.trans.Secondary.Item[1].Value = Convert.ToByte(iAck);
                e.trans.Reply();

                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S2F16]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S2F16]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S2F16()\r\n" + ex.ToString());
            }
        }

        /// <summary> S2F16 : New Equipment Constant Ack(設定(列表)ECID Value)失敗寫Log </summary>
        public static void SecondaryOut_S2F16(SecsEvents_PrimaryInEvent e)
        {
            int iAck = 1;
            e.trans.Secondary.Item[1].Value = Convert.ToByte(iAck);
            e.trans.Reply();

            // Write Log
            ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S2F16]", e.trans);
        }

        /// <summary> S2F18 : Date and Time Data 回覆機台時間 </summary>
        public static void SecondaryOut_S2F18(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                // Ack
                e.trans.Secondary.Item[1].Value = DateTime.Now.ToString("yyyyMMddHHmmss");
                e.trans.Reply();

                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S2F18]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S2F18]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S2F18()\r\n" + ex.ToString());
            }
        }

        /// <summary> S2F30 : Equipment Constant Namelist 回覆(指定/全部)機台不By Recipe裡面的參數ECID Value細項(名稱、值) </summary>
        public static void SecondaryOut_S2F30(SecsEvents_PrimaryInEvent e, string[] _ECIDs)
        {
            try
            {
                if (_ECIDs.Length == 0)
                {
                    int idx = 0;
                    _ECIDs = new string[ucHost_SECS.GetSingleton().m_lstECID.Count];
                    foreach (KeyValuePair<int, ucHost_SECS.ECID> item in ucHost_SECS.GetSingleton().m_lstECID)
                    {
                        _ECIDs[idx] = item.Value.ID.ToString();
                        idx++;
                    }
                }

                // Ack
                for (int i = 0; i < _ECIDs.Length - 1; i++)
                {
                    e.trans.Secondary.Item[1].Item[1].Duplicate();
                }

                for (int i = 0; i < _ECIDs.Length; i++)
                {
                    if (ucHost_SECS.GetSingleton().m_lstECID.ContainsKey(int.Parse(_ECIDs[i])) == true)
                    {
                        e.trans.Secondary.Item[1].Item[i + 1].Item[1].Value = _ECIDs[i];
                        e.trans.Secondary.Item[1].Item[i + 1].Item[2].Value = ucHost_SECS.GetSingleton().m_lstECID[int.Parse(_ECIDs[i])].Name.ToString();
                        e.trans.Secondary.Item[1].Item[i + 1].Item[3].Value = ucHost_SECS.GetSingleton().m_lstECID[int.Parse(_ECIDs[i])].MinValue.ToString();
                        e.trans.Secondary.Item[1].Item[i + 1].Item[4].Value = ucHost_SECS.GetSingleton().m_lstECID[int.Parse(_ECIDs[i])].MaxValue.ToString();
                        e.trans.Secondary.Item[1].Item[i + 1].Item[5].Value = ucHost_SECS.GetSingleton().m_lstECID[int.Parse(_ECIDs[i])].DefValue.ToString();
                        e.trans.Secondary.Item[1].Item[i + 1].Item[6].Value = ucHost_SECS.GetSingleton().m_lstECID[int.Parse(_ECIDs[i])].Units.ToString();
                    }
                }
                e.trans.Reply();
                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S2F30]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S2F30]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S2F30()\r\n" + ex.ToString());
            }
        }

        /// <summary> S2F32 : Date and Time Set Acknowledge 回覆設定機台電腦時間完成 </summary>
        public static void SecondaryOut_S2F32(SecsEvents_PrimaryInEvent e, int iAck)
        {
            try
            {
                // Ack
                e.trans.Secondary.Item[1].Format = FormatConstants.wsFormatBinary;
                e.trans.Secondary.Item[1].Value = Convert.ToByte(iAck);
                e.trans.Reply();
                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S2F32]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S2F32]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S2F32()\r\n" + ex.ToString());
            }
        }

        /// <summary> S2F34 : Define Report Acknowledge 回覆定義/移除 要綁定SVID或要移除SVID的Report(RPTID)是否完成 </summary>
        public static void SecondaryOut_S2F34(SecsEvents_PrimaryInEvent e, int iAck)
        {
            //0 = Accepted
            //1 = Denied. Insufficient space
            //2 = Denied. Invalid format
            //3 = Denied. At least on RPTID already defined
            //4 = Denied. At least on VID does not exist

            try
            {
                // Ack
                e.trans.Secondary.Item[1].Format = FormatConstants.wsFormatBinary;
                e.trans.Secondary.Item[1].Value = Convert.ToByte(iAck);
                e.trans.Reply();
                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S2F34]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S2F34]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S2F34()\r\n" + ex.ToString());
            }
        }

        /// <summary> S2F36 : Link Event Report Acknowledge 回覆對多個CEID(指定/移除)RPTID進行綁定是否完成 </summary>
        public static void SecondaryOut_S2F36(SecsEvents_PrimaryInEvent e, int iAck)
        {
            //0 = Accepted
            //1 = Denied. Insufficient space
            //2 = Denied. Invalid format
            //3 = Denied. At least on RPTID already defined
            //4 = Denied. At least on VID does not exist

            try
            {
                // Ack
                e.trans.Secondary.Item[1].Format = FormatConstants.wsFormatBinary;
                e.trans.Secondary.Item[1].Value = Convert.ToByte(iAck);
                e.trans.Reply();
                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S2F36]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S2F36]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S2F36()\r\n" + ex.ToString());
            }
        }

        /// <summary> S2F38 : Enable/Disable Event Report Acknowledge 回覆指定(所有/單一)CEID(Event)=Enable/Disable是否完成 </summary>
        public static void SecondaryOut_S2F38(SecsEvents_PrimaryInEvent e, int iAck)
        {
            try
            {
                // Ack
                e.trans.Secondary.Item[1].Format = FormatConstants.wsFormatBinary;
                e.trans.Secondary.Item[1].Value = Convert.ToByte(iAck);
                e.trans.Reply();
                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S2F38]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S2F38]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S2F38()\r\n" + ex.ToString());
            }
        }

        /// <summary> S2F42 : Host Command Acknowledge 回覆IT對機台下CMD指令是否完成 </summary>
        public static void SecondaryOut_S2F42(SecsEvents_PrimaryInEvent e, string _RCMD, int iAck)
        {
            //0x00 OK — the command with parameters or command with no parameters was valid. This value is returned when system is idle.
            //0x01 Invalid command—The equipment ignores the entire contents of the message.
            //0x02 Cannot execute the command(s) now—the equipment ignores the contents of the message.
            //0x03 Invalid parameter—at least one parameter is invalid. The equipment ignores the entire contents of the message.
            //0x04 OK—command will be performed and later acknowledged with an event.
            //0x05 Command already executing.

            try
            {
                e.trans.Secondary.Item[1].Item[1].Format = FormatConstants.wsFormatBinary;
                e.trans.Secondary.Item[1].Item[1].Name = "HCACK";
                e.trans.Secondary.Item[1].Item[1].Value = Convert.ToByte(iAck);
                e.trans.Secondary.Item[1].Item[2].Delete();
                e.trans.Reply();

                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S2F42]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S2F42]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S2F42()" + "\r\n" + ex.ToString());
            }

        }

        #endregion

        #region S5FX Function (Stream 5 Exception Handling) - SecondaryOut

        /// <summary> S5F4 : Enable/Disable Alarm Ack 回覆指定(所有/單一)Alarm=Enable/Disable是否完成 </summary>
        public static void SecondaryOut_S5F4(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                // Ack
                e.trans.Secondary.Item[1].Format = FormatConstants.wsFormatBinary;
                e.trans.Secondary.Item[1].Value = Convert.ToByte(0);

                // Content Value
                e.trans.Reply();

                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S5F4]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S5F4]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S5F4()\r\n" + ex.ToString());
            }
        }

        /// <summary> S5F6 : List Alarm Data 回覆(單一或全部)Alarm Information </summary>
        public static void SecondaryOut_S5F6(SecsEvents_PrimaryInEvent e, int _ALID, bool _ALL)
        {
            try
            {
                if (_ALL == false)
                {
                    // Ack
                    if (ucHost_SECS.GetSingleton().m_lstALARM.Contains(_ALID.ToString()))
                    {
                        clsObjAlarm alm = new clsObjAlarm(_ALID.ToString());
                        ArtCommonLib.ucAlarmManage.GetSingleton().GetAlarmInfo(ref alm);

                        e.trans.Secondary.Item[1].Item[1].Item[1].Format = FormatConstants.wsFormatBinary;
                        e.trans.Secondary.Item[1].Item[1].Item[1].Value = Convert.ToByte(0);

                        e.trans.Secondary.Item[1].Item[1].Item[2].Format = FormatConstants.wsFormatVar;
                        e.trans.Secondary.Item[1].Item[1].Item[2].Value = _ALID;

                        e.trans.Secondary.Item[1].Item[1].Item[3].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[1].Item[3].Value = alm.MessageEN;
                    }
                    else
                    {
                        e.trans.Secondary.Item[1].Item[1].Item[1].Format = FormatConstants.wsFormatBinary;
                        e.trans.Secondary.Item[1].Item[1].Item[1].Value = Convert.ToByte(0);

                        e.trans.Secondary.Item[1].Item[1].Item[2].Format = FormatConstants.wsFormatVar;
                        e.trans.Secondary.Item[1].Item[1].Item[2].Value = _ALID;

                        e.trans.Secondary.Item[1].Item[1].Item[3].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[1].Item[3].Value = "";
                    }
                }
                else
                {

                    int index = 1;
                    foreach (string item in ucHost_SECS.GetSingleton().m_lstALARM)
                    {
                        if (index != 1)
                        {
                            e.trans.Secondary.Item[1].Item[1].Duplicate();
                        }

                        index++;
                    }
                    index = 1;
                    foreach (string item in ucHost_SECS.GetSingleton().m_lstALARM)
                    {
                        clsObjAlarm alm = new clsObjAlarm(item);
                        ArtCommonLib.ucAlarmManage.GetSingleton().GetAlarmInfo(ref alm);

                        e.trans.Secondary.Item[1].Item[index].Item[1].Format = FormatConstants.wsFormatBinary;
                        e.trans.Secondary.Item[1].Item[index].Item[1].Value = Convert.ToByte(0);

                        e.trans.Secondary.Item[1].Item[index].Item[2].Format = FormatConstants.wsFormatVar;
                        e.trans.Secondary.Item[1].Item[index].Item[2].Value = item;

                        e.trans.Secondary.Item[1].Item[index].Item[3].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[index].Item[3].Value = alm.MessageEN;

                        index++;
                    }


                }

                // Content Value
                e.trans.Reply();

                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S5F6]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S5F6]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S5F6()\r\n" + ex.ToString());
            }
        }

        /// <summary> S5F8 : List Enabled Alarm Request 回覆(單一或全部)已啟用Alarm Information </summary>
        public static void SecondaryOut_S5F8(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                e.trans.Secondary.Item[1].Item[1].Delete();
                int index = 1, iCount = 0;
                foreach (bool item in ucHost_SECS.GetSingleton().m_lstALARM_Enable)
                {
                    if (item == true)
                    {
                        if (index != 1)
                        {
                            e.trans.Secondary.Item[1].Item[1].Duplicate();
                        }
                        else
                        {
                            e.trans.Secondary.Item[1].AddNew(1);
                            e.trans.Secondary.Item[1].Item[1].AddNew(1);
                            e.trans.Secondary.Item[1].Item[1].AddNew(1);
                            e.trans.Secondary.Item[1].Item[1].AddNew(1);
                        }
                        index++;
                    }
                }
                index = 1;
                foreach (string item in ucHost_SECS.GetSingleton().m_lstALARM)
                {
                    if (ucHost_SECS.GetSingleton().m_lstALARM_Enable[iCount] == true)
                    {
                        clsObjAlarm alm = new clsObjAlarm(item);
                        ArtCommonLib.ucAlarmManage.GetSingleton().GetAlarmInfo(ref alm);

                        e.trans.Secondary.Item[1].Item[index].Item[1].Format = FormatConstants.wsFormatBinary;
                        e.trans.Secondary.Item[1].Item[index].Item[1].Value = Convert.ToByte(0);

                        e.trans.Secondary.Item[1].Item[index].Item[2].Format = FormatConstants.wsFormatVar;
                        e.trans.Secondary.Item[1].Item[index].Item[2].Value = item;

                        e.trans.Secondary.Item[1].Item[index].Item[3].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[index].Item[3].Value = alm.MessageEN;

                        index++;
                    }
                    iCount++;
                }

                // Content Value
                e.trans.Reply();

                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S5F8]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S5F8]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S5F8()\r\n" + ex.ToString());
            }
        }

        #endregion

        #region S6FX Function (Stream 6 Data Collection) - SecondaryOut

        /// <summary> S6F16 : Event Report Data 回覆CEID內指定的所有RPTID的VID與值 </summary>
        public static void SecondaryOut_S6F16(SecsEvents_PrimaryInEvent e, int sCEID)
        {
            try
            {
                e.trans.Secondary.Item[1].Item[1].Format = FormatConstants.wsFormatU4;
                e.trans.Secondary.Item[1].Item[1].Value = ucHost_SECS.GetSingleton().m_SecsState.GetDataID().ToString();
                e.trans.Secondary.Item[1].Item[2].Format = FormatConstants.wsFormatU4;
                e.trans.Secondary.Item[1].Item[2].Value = sCEID.ToString();

                if (ucHost_SECS.GetSingleton().m_lstCEID[sCEID].lstLinkReport.Count > 0)
                {

                    for (int j = 0; j < ucHost_SECS.GetSingleton().m_lstCEID[sCEID].lstLinkReport.Count - 1; j++)
                    {
                        e.trans.Secondary.Item[1].Item[3].Item[1].Duplicate();
                    }

                    for (int j = 1; j < ucHost_SECS.GetSingleton().m_lstCEID[sCEID].lstLinkReport.Count + 1; j++)
                    {
                        if (ucHost_SECS.GetSingleton().m_lstRPTID.ContainsKey(ucHost_SECS.GetSingleton().m_lstCEID[sCEID].lstLinkReport[j - 1]))
                        {
                            int rptid = ucHost_SECS.GetSingleton().m_lstCEID[sCEID].lstLinkReport[j - 1];
                            e.trans.Secondary.Item[1].Item[3].Item[j].Item[1].Format = FormatConstants.wsFormatU4;
                            e.trans.Secondary.Item[1].Item[3].Item[j].Item[1].Value = rptid.ToString();
                            e.trans.Secondary.Item[1].Item[3].Item[j].Item[2].Item[1].Delete();// delete default item
                            for (int i = 0; i < ucHost_SECS.GetSingleton().m_lstRPTID[ucHost_SECS.GetSingleton().m_lstCEID[sCEID].lstLinkReport[j - 1]].lstVID.Count; i++)
                            {
                                int VID = ucHost_SECS.GetSingleton().m_lstRPTID[rptid].lstVID[i];
                                e.trans.Secondary.Item[1].Item[3].Item[j].Item[2].AddNew(i + 1);
                                e.trans.Secondary.Item[1].Item[3].Item[j].Item[2].Item[i + 1].Format = FormatConstants.wsFormatAscii;
                                e.trans.Secondary.Item[1].Item[3].Item[j].Item[2].Item[i + 1].Name = VID.ToString();
                                if (VID >= 4000)
                                {
                                    // DVID
                                    e.trans.Secondary.Item[1].Item[3].Item[j].Item[2].Item[i + 1].Value = ucHost_SECS.GetSingleton().m_lstDVID[VID].Value;
                                }
                                else
                                {
                                    // SVID
                                    e.trans.Secondary.Item[1].Item[3].Item[j].Item[2].Item[i + 1].Value = ucHost_SECS.GetSingleton().m_lstSVID[VID].Value;
                                }
                            }
                        }
                        else
                        {
                            e.trans.Secondary.Item[1].Item[3].Item[j].Delete();
                        }
                    }
                }
                else
                {
                    // this event no report
                    e.trans.Secondary.Item[1].Item[3].Delete();
                }

                e.trans.Reply();
                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S6F16]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S6F16]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S6F16()\r\n" + ex.ToString());
            }
        }

        /// <summary> S6F20 : Individual Report Data 回覆RPTID的VID與值 </summary>
        public static void SecondaryOut_S6F20(SecsEvents_PrimaryInEvent e, int iRPTID)
        {
            try
            {
                if (ucHost_SECS.GetSingleton().m_lstRPTID.ContainsKey(iRPTID) == true)
                {
                    foreach (KeyValuePair<int, ucHost_SECS.RPTID> item in ucHost_SECS.GetSingleton().m_lstRPTID)
                    {
                        for (int i = 0; i < item.Value.lstVID.Count; i++)
                        {
                            if (iRPTID.ToString() == item.Key.ToString())
                            {
                                if (i == 0)
                                {
                                    int VID = item.Value.lstVID[i];
                                    e.trans.Secondary.Item[1].Item[1].Value = ucHost_SECS.GetSingleton().m_lstSVID[VID].Value;
                                }

                                else
                                {
                                    int VID = item.Value.lstVID[i];
                                    e.trans.Secondary.Item[1].AddNew(i + 1);
                                    e.trans.Secondary.Item[1].Item[i + 1].Format = FormatConstants.wsFormatAscii;
                                    e.trans.Secondary.Item[1].Item[i + 1].Name = "V";
                                    e.trans.Secondary.Item[1].Item[i + 1].Value = ucHost_SECS.GetSingleton().m_lstSVID[VID].Value;
                                }

                            }
                        }
                    }

                }

                e.trans.Reply();
                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S6F20]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S6F20]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S6F20()\r\n" + ex.ToString());
            }
        }

        #endregion

        #region S7FX Function (Stream 7 Process Program Management) - PrimaryIn

        /// <summary> S7F2 : Process Program Load Grant 回覆設定RecipeName長度上限是否完成 </summary>
        public static void SecondaryOut_S7F2(SecsEvents_PrimaryInEvent e, string _PPID, int iAck)
        {
            try
            {
                // Ack
                e.trans.Secondary.Item[1].Value = Convert.ToByte(iAck);
                e.trans.Reply();
                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S7F2]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S7F2]");
            }
            catch (Exception ex)
            {
                e.trans.Secondary.Item[1].Value = Convert.ToByte(3);
                e.trans.Reply();
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S7F2()\r\n" + ex.ToString());
            }
        }

        /// <summary> S7F4 : Process Program Send Acknowledge 回覆Recipe是否建立完成 </summary>
        public static void SecondaryOut_S7F4(SecsEvents_PrimaryInEvent e, string _PPID, int iAck)
        {
            try
            {
                // Ack
                e.trans.Secondary.Item[1].Value = Convert.ToByte(iAck);
                e.trans.Reply();
                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S7F4]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S7F4]");
            }
            catch (Exception ex)
            {
                e.trans.Secondary.Item[1].Value = Convert.ToByte(3);
                e.trans.Reply();
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S7F4()\r\n" + ex.ToString());
            }
        }

        /// <summary> S7F6 : Process Program Data 回覆Recipe內的參數內容 </summary>
        public static void SecondaryOut_S7F6(SecsEvents_PrimaryInEvent e, string _PPID)
        {
            try
            {
                e.trans.Secondary.Item[1].Item[1].Value = _PPID;

                string RecipeName;
                string strS7F5Ack = "";
                RecipeName = System.IO.Path.GetFileName(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe));
                RecipeName = RecipeName.Substring(0, RecipeName.Length - 4);//省略副檔名

                int Index = 1;
                foreach (string item in ucHost_SECS.GetSingleton().m_lstPPBody)
                {
                    string[] str = item.Split(';');
                    string PPBody_ID = str[0];
                    string PPBody_Name = str[1];
                    string PPBody_Unit = str[2];
                    string PPBody_Format = str[3];
                    string PPBody_Min = str[4];
                    string PPBody_Max = str[5];
                    clsEnum.enuPmtName ePmtName = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), PPBody_Name, true);
                    string PPBody_Value = ucParameter.GetValueString(ePmtName);

                    if (Index == 1)
                    {
                        strS7F5Ack = strS7F5Ack + PPBody_ID + ",";     //PPbody Code
                        strS7F5Ack = strS7F5Ack + PPBody_Name + ",";   //PPbody Name
                        strS7F5Ack = strS7F5Ack + PPBody_Unit + ",";   //PPbody Unit
                        strS7F5Ack = strS7F5Ack + PPBody_Format + ","; //PPbody Format
                        strS7F5Ack = strS7F5Ack + PPBody_Value + ",";  //PPbody Value
                        strS7F5Ack = strS7F5Ack + PPBody_Min + ",";    //PPbody Maximum Value
                        strS7F5Ack = strS7F5Ack + PPBody_Max;          //PPbody Minimux Value
                    }
                    else
                    {
                        strS7F5Ack = strS7F5Ack + PPBody_ID + ",";     //PPbody Code
                        strS7F5Ack = strS7F5Ack + PPBody_Name + ",";   //PPbody Name
                        strS7F5Ack = strS7F5Ack + PPBody_Unit + ",";   //PPbody Unit
                        strS7F5Ack = strS7F5Ack + PPBody_Format + ","; //PPbody Format
                        strS7F5Ack = strS7F5Ack + PPBody_Value + ",";  //PPbody Value
                        strS7F5Ack = strS7F5Ack + PPBody_Min + ",";    //PPbody Maximum Value
                        strS7F5Ack = strS7F5Ack + PPBody_Max;          //PPbody Minimux Value
                    }
                    Index++;
                }


                // Content Value
                e.trans.Secondary.Item[1].Item[2].Format = FormatConstants.wsFormatBinary;
                e.trans.Secondary.Item[1].Item[2].Value = strS7F5Ack;
                e.trans.Reply();

                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", true, "EQP -> CIM  SecondaryOut  [S7F6]", e.trans); //20220222
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S7F6]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S7F6()\r\n" + ex.ToString());
            }
        }

        /// <summary> S7F18 : Delete Process Program Acknowledge IT要求刪除RecipeFile是否完成 </summary>
        public static void SecondaryOut_S7F18(SecsEvents_PrimaryInEvent e, string _PPID)
        {
            bool IsRecipeExist = false;
            try
            {
                string[] RecipeNameList = System.IO.Directory.GetFiles("D:\\Parameter\\Recipe");

                //尋找Recipe是否已存在
                foreach (string Recipe_Name in RecipeNameList)
                {
                    string str = "";
                    FileInfo fileInfo = new FileInfo(Recipe_Name);
                    str = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);

                    if (str == _PPID)
                    {
                        IsRecipeExist = true;
                    }
                }

                if (IsRecipeExist)
                {
                    File.Delete("D:\\Parameter\\Recipe\\" + _PPID + ".ini");
                    e.trans.Secondary.Item[1].Value = Convert.ToByte(0);
                }
                else
                {
                    e.trans.Secondary.Item[1].Value = Convert.ToByte(4);
                }
                // Ack

                e.trans.Reply();
                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S7F18]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S7F18]");
            }
            catch (Exception ex)
            {
                e.trans.Secondary.Item[1].Value = Convert.ToByte(5);
                e.trans.Reply();
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S7F18()\r\n" + ex.ToString());
                return;
            }
        }

        /// <summary> S7F20 : Current Process Program Data 回覆機台所有的Recipe Name List </summary>
        public static void SecondaryOut_S7F20(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                // Ack
                for (int i = 0; i < ucHost_SECS.GetSingleton().m_lstPPID.Count; i++)
                {
                    if (i == 0)
                        e.trans.Secondary.Item[1].Item[1].Value = ucHost_SECS.GetSingleton().m_lstPPID[i];
                    else
                    {
                        e.trans.Secondary.Item[1].AddNew(i + 1);
                        e.trans.Secondary.Item[1].Item[i + 1].Name = "PPID";
                        e.trans.Secondary.Item[1].Item[i + 1].Format = FormatConstants.wsFormatAscii;
                        e.trans.Secondary.Item[1].Item[i + 1].Value = ucHost_SECS.GetSingleton().m_lstPPID[i];
                    }
                }

                // Content Value
                e.trans.Reply();

                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S7F20]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S7F20]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S7F20()\r\n" + ex.ToString());
            }
        }

        /// <summary> S7F24 : Formatted Process Program Acknowledge 回覆下載或修改單一值是否修改成功 </summary>
        public static void SecondaryOut_S7F24(SecsEvents_PrimaryInEvent e, string _PPID, List<string> _lstPPRAM, int iAck)
        {
            try
            {
                // Ack
                e.trans.Secondary.Item[1].Value = Convert.ToByte(iAck);
                e.trans.Reply();
                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S7F24]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S7F24]");
            }
            catch (Exception ex)
            {
                e.trans.Secondary.Item[1].Value = Convert.ToByte(3);
                e.trans.Reply();
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S7F24()\r\n" + ex.ToString());
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
                    string[] str = item.Split(';');
                    string PPBody_ID = str[0];
                    string PPBody_Name = str[1];
                    string PPBody_Unit = str[2];
                    string PPBody_Format = str[3];
                    string PPBody_Min = str[4];
                    string PPBody_Max = str[5];
                    clsEnum.enuPmtName ePmtName = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), PPBody_Name, true);
                    string PPBody_Value = ucParameter.GetValueString(ePmtName);

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

        #region S10FX Function (Stream 10 Terminal Services)

        /// <summary> S10F4 Terminal Display, Single Acknowledge 回覆已收到並顯示在UI上 </summary>
        public static void SecondaryOut_S10F4(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                // Ack
                e.trans.Secondary.Item[1].Format = FormatConstants.wsFormatBinary;
                e.trans.Secondary.Item[1].Value = Convert.ToByte(0);

                // Content Value
                e.trans.Reply();

                // Write Log
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S10F4]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S10F4]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S10F4()\r\n" + ex.ToString());
            }
        }

        /// <summary> S10F6 Terminal Display, Multi-Block Acknowledge 回覆已收到並顯示在UI上 </summary>
        public static void SecondaryOut_S10F6(SecsEvents_PrimaryInEvent e)
        {
            try
            {
                e.trans.Secondary.Item[1].Format = FormatConstants.wsFormatBinary;
                e.trans.Secondary.Item[1].Value = Convert.ToByte(0);
                e.trans.Reply();
                ucHost_SECS.GetSingleton().WriteSecsLog("S", false, "EQP -> CIM  SecondaryOut  [S10F6]", e.trans);
                ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.Equipment2Host, "EQP -> CIM  SecondaryOut  [S10F6]");
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at SecondaryOut_S10F5()\r\n" + ex.ToString());
            }
        }


        #endregion

    }

}

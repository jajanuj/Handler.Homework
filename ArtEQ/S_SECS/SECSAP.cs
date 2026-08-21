using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtData;
using ArtCommonLib;
using ArtControlLib;
using AxWinSecsLib;
using WinSecsLib;
using System.IO;
using System.Xml;

namespace ArtEQ
{
    public class SECSAP
    {

        #region //========== SVID Function ==========

        public static void UpdateSVID_Clock()
        {
            ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.CLOCK, DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        public static void UpdateSVID_ProcessStatus()
        {
            if (clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Run)
            { ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Processing_State, "0"); }
            else if (clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Stop)
            { ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Processing_State, "2"); }
            else if (clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Idle)
            { ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Processing_State, "1"); }
            else if (clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Down)
            { ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Processing_State, "3"); }
            else if (clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Default)
            { ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Processing_State, "1"); }
        }

        public static void UpdateSVID_ControlStatus()
        {
            if (ucHost_SECS.GetSingleton().m_SecsState.sControl_State_Value == "0")
            { ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Control_State, "0"); }
            else if (ucHost_SECS.GetSingleton().m_SecsState.sControl_State_Value == "1")
            { ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Control_State, "1"); }
            else if (ucHost_SECS.GetSingleton().m_SecsState.sControl_State_Value == "2")
            { ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Control_State, "2"); }
        }

        public static void UpdateSVID_RecipeName()
        {
            string sRecipeName;
            sRecipeName = System.IO.Path.GetFileName(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe));
            sRecipeName = sRecipeName.Substring(0, sRecipeName.Length - 4);//省略副檔名
            ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.RECIPE_NAME, sRecipeName);
        }

        public static void UpdateAllSVID()
        {
            UpdateSVID_Clock();
            UpdateSVID_ProcessStatus();
            UpdateSVID_ControlStatus();
            UpdateSVID_RecipeName();
        }

        #endregion

        #region //========== ECID Function ==========

        public static void UpdateAllECID()
        {
            ucHost_SECS.GetSingleton().SECS_UpdateECID((int)clsEnum.eSECS_ECID.LaneMotorHighSpeed, ucParameter.GetValueString(clsEnum.enuPmtName.Sys_LaneMotorHighSpeed));
            ucHost_SECS.GetSingleton().SECS_UpdateECID((int)clsEnum.eSECS_ECID.LaneMotorLowSpeed, ucParameter.GetValueString(clsEnum.enuPmtName.Sys_LaneMotorLowSpeed));
        }

        public static void SettingAllECID(string p_sECIDNumber, string p_sECIDValue)
        {
            switch (p_sECIDNumber)
            {
                case "2001":
                    ucHost_SECS.GetSingleton().m_lstECID[int.Parse(p_sECIDNumber)].Value = p_sECIDValue;
                    //ucParameter.SetValue(clsEnum.enuPmtName.Enable_ByPassMag1D, p_sECIDValue);
                    break;

                case "2002":
                    ucHost_SECS.GetSingleton().m_lstECID[int.Parse(p_sECIDNumber)].Value = p_sECIDValue;
                    //ucParameter.SetValue(clsEnum.enuPmtName.Enable_ByPassCap2D, p_sECIDValue);
                    break;

                default:
                    ucHost_SECS.GetSingleton().m_lstECID[int.Parse(p_sECIDNumber)].Value = p_sECIDValue;
                    break;
            }
        }

        #endregion

        #region //========== Host Command ==========

        public static void SecsRCMDParse(SecsEvents_PrimaryInEvent e, string p_sRCMD)
        {
            switch (p_sRCMD)
            {

                #region GOLOCAL
                case "LOCAL":
                    SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 0);
                    ucHost_SECS.GetSingleton().m_SecsState.sControl_State_Value = "1"; 
                    ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Control_State, "1");
                    ucHost_SECS.GetSingleton().SECS_DispatchEvent((int)clsEnum.eSECS_CEID.Control_state_LOCAL);
                    ucHost_SECS.GetSingleton().bIsRemote = false;
                    ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.HostCommand, "Sending S2F41(GOLOCAL) to Equipment Ack : " + "0");
                    break;

                #endregion

                #region GOREMOTE
                case "REMOTE":
                    SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 0);
                    ucHost_SECS.GetSingleton().m_SecsState.sControl_State_Value = "2"; 
                    ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Control_State, "2");
                    ucHost_SECS.GetSingleton().SECS_DispatchEvent((int)clsEnum.eSECS_CEID.Control_state_REMOTE);
                    ucHost_SECS.GetSingleton().bIsRemote = true;
                    ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.HostCommand, "Sending S2F41(GOREMOTE) to Equipment Ack : " + "0");
                    break;

                #endregion

                #region RUN
                case "RUN": case "START":
                    if (clsCmData.g_bIsinitialized == true && clsCmData.g_NowEqStatus != clsCmData.enuEqStatus.Run)
                    {
                        SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 0);
                        clsEditRunThread.EqRun(true);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.HostCommand, "Sending S2F41(RUN) to Equipment Ack : " + "0");
                    }
                    else
                    {
                        SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 2);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.HostCommand, "Sending S2F41(RUN) to Equipment Ack : " + "2");
                    }
                    break;
                #endregion

                #region STOP
                case "STOP":
                    SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 0);
                    clsEditRunThread.EqStop();
                    ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.HostCommand, "Sending S2F41(STOP) to Equipment Ack : " + "0");
                    break;
                #endregion

                #region RESUME
                case "RESUME":
                    SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 0);
                    ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.HostCommand, "Sending S2F41(RESUME) to Equipment Ack : " + "0");
                    break;
                #endregion

                #region PAUSE
                case "PAUSE":
                    SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 0);
                    ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.HostCommand, "Sending S2F41(PAUSE) to Equipment Ack : " + "0");
                    break;
                #endregion

                #region PPSELECT
                case "PPSELECT": case "PP-SELECT":
                    string strPPID = "";

                    if (e.trans.Primary.Item[1].ItemCount == 2)
                    {
                        if (e.trans.Primary.Item[1].Item[2].ItemCount == 1)
                        {
                            if (e.trans.Primary.Item[1].ItemCount == 2)
                            {
                                if (e.trans.Primary.Item[1].Item[2].Item[1].Item[1].Value.ToString() == "PPID")
                                {
                                    strPPID = e.trans.Primary.Item[1].Item[2].Item[1].Item[2].Value.ToString();

                                    p_sRCMD = p_sRCMD + "," + strPPID;
                                }
                            }

                        }

                    }

                    if (strPPID != "")
                    {
                        if (ucHost_SECS.AutoChangeRecipe(strPPID, true) == true)
                        {
                            SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 0);
                            ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.HostCommand, "Sending S2F41(PPSELECT) to Equipment : " + strPPID + " , Ack : " + "0");
                        }
                        else
                        {
                            SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 2);
                            ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.HostCommand, "Sending S2F41(PPSELECT) to Equipment : " + strPPID + " , Ack : " + "2");
                        }
                    }
                    else
                    {
                        SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 2);
                        ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.HostCommand, "Sending S2F41(PPSELECT) to Equipment : " + strPPID + " , Ack : " + "2");
                    }

                    break;
                #endregion

                #region MES_MOVE_IN
                case "MES_MOVE_IN":

                    string strMoveInScheduleID = "";
                    string strMoveInMagazineID = "";
                    string strMoveInResult = "";

                    if (e.trans.Primary.Item[1].ItemCount == 2)
                    {
                        if (e.trans.Primary.Item[1].Item[2].ItemCount == 4)
                        {
                            //LOTID
                            if (e.trans.Primary.Item[1].ItemCount == 2)
                            {
                                if (e.trans.Primary.Item[1].Item[2].Item[1].Item[1].Value.ToString() == "LOTID")
                                {
                                    strMoveInScheduleID = e.trans.Primary.Item[1].Item[2].Item[1].Item[2].Value.ToString();
                                    ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.LotID, strMoveInScheduleID);
                                    //PM.GetSingleton().Proc_MagazineFlip_Arm.GetMagazineData().strLotID = strMoveInScheduleID;
                                    ProcAutoRun.sLotID = strMoveInScheduleID;
                                    //@SECS-LotID-Input@
                                }
                            }

                            //Qty                           
                            if (e.trans.Primary.Item[1].Item[2].Item[2].ItemCount == 2)
                            {
                                if (e.trans.Primary.Item[1].Item[2].Item[2].Item[1].Value.ToString() == "QTY")
                                {
                                    strMoveInMagazineID = e.trans.Primary.Item[1].Item[2].Item[2].Item[2].Value.ToString();
                                }
                            }
                            //MAGAZINEID
                            if (e.trans.Primary.Item[1].Item[2].Item[3].ItemCount == 2)
                            {
                                if (e.trans.Primary.Item[1].Item[2].Item[3].Item[1].Value.ToString() == "MAGAZINEID")
                                {
                                    strMoveInMagazineID = e.trans.Primary.Item[1].Item[2].Item[3].Item[2].Value.ToString();
                                    ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Loader_Magazine_ID, strMoveInMagazineID);
                                }
                            }

                            //MOVE_IN_STATUS
                            if (e.trans.Primary.Item[1].Item[2].Item[4].ItemCount == 2)
                            {
                                if (e.trans.Primary.Item[1].Item[2].Item[4].Item[1].Value.ToString() == "MOVE_IN_STATUS")
                                {
                                    strMoveInResult = e.trans.Primary.Item[1].Item[2].Item[4].Item[2].Value.ToString();
                                }
                            }

                        }


                    }

                    if (strMoveInResult.Equals("PASS"))
                    {
                        SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 0);
                        //ProcAutoRun.IsMoveInPass = true;//@MES_MOVE_IN@
                    }
                    else
                    {
                        SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 0);
                        //ProcAutoRun.IsMoveInPass = false;//@MES_MOVE_IN@
                    }
                    break;

                #endregion

                #region MES_MOVE_OUT
                case "MES_MOVE_OUT":

                    string strMoveOutMagazineID = "";
                    string strMoveOutScheduleID = "";
                    string strMoveOutResult = "";

                    if (e.trans.Primary.Item[1].ItemCount == 2)
                    {
                        //LOTID
                        if (e.trans.Primary.Item[1].ItemCount == 2)
                        {
                            if (e.trans.Primary.Item[1].Item[2].Item[1].Item[1].Value.ToString() == "LOTID")
                            {
                                strMoveOutScheduleID = e.trans.Primary.Item[1].Item[2].Item[1].Item[2].Value.ToString();
                            }
                        }
                        //Qty                           
                        if (e.trans.Primary.Item[1].Item[2].Item[2].ItemCount == 2)
                        {
                            if (e.trans.Primary.Item[1].Item[2].Item[2].Item[1].Value.ToString() == "QTY")
                            {
                                strMoveInMagazineID = e.trans.Primary.Item[1].Item[2].Item[2].Item[2].Value.ToString();
                            }
                        }
                        //MAGAZINEID
                        if (e.trans.Primary.Item[1].Item[2].Item[3].ItemCount == 2)
                        {
                            if (e.trans.Primary.Item[1].Item[2].Item[3].Item[1].Value.ToString() == "MAGAZINEID")
                            {
                                strMoveOutMagazineID = e.trans.Primary.Item[1].Item[2].Item[3].Item[2].Value.ToString();
                            }
                        }

                        //MOVE_OUT_STATUS
                        if (e.trans.Primary.Item[1].Item[2].Item[4].ItemCount == 2)
                        {
                            if (e.trans.Primary.Item[1].Item[2].Item[4].Item[1].Value.ToString() == "MOVE_OUT_STATUS")
                            {
                                strMoveOutResult = e.trans.Primary.Item[1].Item[2].Item[4].Item[2].Value.ToString();
                            }
                        }
                    }

                    if (strMoveOutResult.Equals("PASS"))
                    {
                        SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 0);
                    }
                    else
                    {
                        SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 0);
                    }
                    break;

                #endregion

                #region Boat 2D Verify OK
                case "Boat 2D Verify OK":
                    SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 0);
                    ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.HostCommand, "Sending S2F41(Boat 2D Verify OK) to Equipment Ack : " + "0");
                    break;
                #endregion

                #region Boat 2D Verify NG
                case "Boat 2D Verify NG":
                    SecondaryOutFucntion.SecondaryOut_S2F42(e, "", 0);
                    ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.HostCommand, "Sending S2F41(Boat 2D Verify NG) to Equipment Ack : " + "0");
                    break;
                #endregion

                default:
                    ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("CIM Send unknow Remote Command : " + p_sRCMD);
                    SecondaryOutFucntion.SecondaryOut_S2F42(e, "unknow", 1);
                    ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.HostCommand, ("CIM Send unknow Remote Command : " + p_sRCMD));
                    break;
            }
        }

        #endregion

        #region //========== Map Data上傳和下載解析 ==========

        public void GetMapDataParse(string FileName)
        {
            try
            {
                XmlDocument objDoc = new XmlDocument();
                if (File.Exists(ucHost_SECS.GetSingleton().g_strMapDownloadPath + FileName + ".xml") == true)
                {
                    objDoc.Load(ucHost_SECS.GetSingleton().g_strMapDownloadPath + FileName + ".xml");


                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(objDoc.NameTable);
                    XmlNode FirstNode = objDoc.ChildNodes[1];
                    nsmgr.AddNamespace("Map", FirstNode.NamespaceURI);
                    XmlNode BinCodeMap = objDoc.SelectSingleNode("/Map:MapData/Map:SubstrateMaps/Map:SubstrateMap/Map:Overlay/Map:BinCodeMap", nsmgr);
                    XmlNode BinDefine = objDoc.SelectSingleNode("/Map:MapData/Map:SubstrateMaps/Map:SubstrateMap/Map:Overlay/Map:BinCodeMap/Map:BinDefinitions", nsmgr);

                    XmlNode xRoot;
                    string m_strMappingText;

                    xRoot = (XmlNode)objDoc.DocumentElement;
                    m_strMappingText = xRoot.InnerText;

                    string[] MapArray;
                    MapArray = m_strMappingText.Split('_');
                    List<bool> lstMap = new List<bool>();

                    int intHostGoodDieQTY = 0;
                    int intHostBadDieQTY = 0;
                    //clsDataInfo.lstDownloadSECSMapData.Clear();//@MapData-Download@

                    int iCount = 0;
                    string[] sCombi = new string[4];

                    for (int MapIndex = 0; MapIndex < MapArray.Length; MapIndex++)
                    {
                        string strVal = MapArray[MapIndex];
                        if (strVal.Length > 0) { iCount = 4; } else { iCount++; }

                        if (iCount == 4)
                        {
                            switch (strVal)
                            {
                                case "1":
                                    //clsDataInfo.lstDownloadSECSMapData.Add("OK");//@MapData-Download@
                                    intHostGoodDieQTY++;
                                    break;

                                default:
                                    //clsDataInfo.lstDownloadSECSMapData.Add("NG");//@MapData-Download@
                                    intHostBadDieQTY++;
                                    break;
                            }

                            iCount = 0;
                        }
                    }
                    ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.EquipmentEvent, "S14F2 Total Count : "
                        //+ clsDataInfo.lstDownloadSECSMapData.Count.ToString() //@MapData-Download@
                        + " , Good Die : " + intHostGoodDieQTY.ToString() 
                        + " , Bad Die : " + intHostBadDieQTY.ToString());
                }
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S14F2()\r\n" + ex.ToString());
            }
        }

        public void SetMapDataProduce(string FileName, string sMaxX, string sMaxY, string sGoodDie, string sBadDie, string sNoDie)
        {
            try
            {
                XmlDocument objDoc = new XmlDocument();

                if (File.Exists(ucHost_SECS.GetSingleton().g_strMapUploadPath + "StripMap.xml") == true)
                {
                    objDoc.Load(ucHost_SECS.GetSingleton().g_strMapUploadPath + "StripMap.xml");

                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(objDoc.NameTable);
                    XmlNode FirstNode = objDoc.ChildNodes[1];
                    nsmgr.AddNamespace("Map", FirstNode.NamespaceURI);

                    XmlNode xmlSubstrateMaps = objDoc.SelectSingleNode("/Map:MapData/Map:SubstrateMaps", nsmgr);

                    foreach (XmlNode Code in xmlSubstrateMaps.ChildNodes)
                    {
                        XmlElement XmlSubstrateMaps = (XmlElement)Code;

                        //MaxX
                        XmlAttribute XmlSubstrateTypeValue = XmlSubstrateMaps.GetAttributeNode("MaxX");
                        XmlSubstrateTypeValue.Value = sMaxX;

                        //MaxY
                        XmlAttribute XmlSubstrateIdValue = XmlSubstrateMaps.GetAttributeNode("MaxY");
                        XmlSubstrateIdValue.Value = sMaxY;

                    }

                    XmlNode xmlBinDefinitions = objDoc.SelectSingleNode("/Map:MapData/Map:SubstrateMaps/Map:SubstrateMap/Map:Overlay/Map:BinCodeMap/Map:BinDefinitions", nsmgr);

                    foreach (XmlNode Code in xmlBinDefinitions.ChildNodes)
                    {
                        XmlElement XmlBinDefinitions = (XmlElement)Code;

                        XmlAttribute XmlBinCode = XmlBinDefinitions.GetAttributeNode("BinCode");
                        XmlAttribute XmlBinQuality = XmlBinDefinitions.GetAttributeNode("BinQuality");
                        XmlAttribute XmlBinCount = XmlBinDefinitions.GetAttributeNode("BinCount");

                        switch (XmlBinCode.Value)
                        {
                            case "1":
                                XmlBinCount.Value = sGoodDie; //Good Die
                                break;

                            case "1EM":
                                XmlBinCount.Value = sBadDie; //BadDie
                                break;

                            case "1ND":
                                XmlBinCount.Value = sNoDie; //Empty Die
                                break;

                            default:
                                break;
                        }

                    }

                    XmlNode xmlBinCodeMap = objDoc.SelectSingleNode("/Map:MapData/Map:SubstrateMaps/Map:SubstrateMap/Map:Overlay/Map:BinCodeMap", nsmgr);


                    List<string> lstBinCodeSegmentation = new List<string>();

                    for (int iRow = 0; iRow < Int32.Parse(sMaxY); iRow++)
                    {
                        string strInexText = "";

                        for (int iCol = 0; iCol < Int32.Parse(sMaxX); iCol++)
                        {
                            int iChipID = (iRow * (Int32.Parse(sMaxX) - 1)) + iCol;

                            // 將前面補_，補足長度為4
                            String sBinCode = "";// clsDataInfo.lstUploadSECSMapData[iChipID];
                            //sBinCode = clsDataInfo.lstUploadSECSMapData[iChipID];//@MapData-BinCode@
                            sBinCode = sBinCode.PadLeft(4, '_');
                            strInexText += sBinCode;
                        }

                        lstBinCodeSegmentation.Add(strInexText);
                    }

                    //Set BinCode
                    int iNode = 0;

                    XmlElement XmlCode = (XmlElement)xmlBinCodeMap;

                    foreach (XmlNode Code in XmlCode.ChildNodes)
                    {
                        if (Code.Name == "BinCode")
                        {
                            string strInexText = "";

                            strInexText = lstBinCodeSegmentation[iNode];

                            Code.InnerText = strInexText;

                            iNode++;
                        }
                    }

                    objDoc.Save(ucHost_SECS.GetSingleton().g_strMapUploadPath + FileName + ".xml"); ;//存檔成xml

                    string sMappingValue = objDoc.InnerXml.ToString();
                    sMappingValue = sMappingValue.Replace("\n", "");
                }
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S14F2()\r\n" + ex.ToString());
            }
        }

        #endregion

        #region //========== Traceability 資料上傳 ==========

        public static void SetTraceabilityToXML_1(string sSubstrateType, string sSubstrateId, string sHostName, string sLotId, string sLotNo, string sScheduleNo, DateTime dMapCreateTime, DateTime dMapUpdataTime, string sPassCount, string sFailCount, string sUglyCount, DateTime dTestStartTime, DateTime dTestEndTime, string sCoverId)
        {
            try
            {

                XmlDocument objDoc = new XmlDocument();

                if (File.Exists(ucHost_SECS.GetSingleton().g_strTraceabilityDataPath + "Traceability.xml") == true)
                {
                    objDoc.Load(ucHost_SECS.GetSingleton().g_strTraceabilityDataPath + "Traceability.xml");

                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(objDoc.NameTable);
                    XmlNode FirstNode = objDoc.ChildNodes[1];
                    nsmgr.AddNamespace("Map", FirstNode.NamespaceURI);

                    XmlNode xmlSubstrates = objDoc.SelectSingleNode("/Map:MapData/Map:Substrates", nsmgr);

                    foreach (XmlNode Code in xmlSubstrates.ChildNodes)
                    {
                        XmlElement XmlSubstrateData = (XmlElement)Code;

                        //SubstrateType
                        XmlAttribute XmlSubstrateTypeValue = XmlSubstrateData.GetAttributeNode("SubstrateType");
                        XmlSubstrateTypeValue.Value = sSubstrateType;

                        //SubstrateId
                        XmlAttribute XmlSubstrateIdValue = XmlSubstrateData.GetAttributeNode("SubstrateId");
                        XmlSubstrateIdValue.Value = sSubstrateId;

                        //HostName
                        XmlAttribute XmlHostNameValue = XmlSubstrateData.GetAttributeNode("HostName");
                        XmlHostNameValue.Value = sHostName;

                        //LotId
                        XmlAttribute XmlLotIdValue = XmlSubstrateData.GetAttributeNode("LotId");
                        XmlLotIdValue.Value = sLotId;

                        //LotNo
                        XmlAttribute XmlLotNoValue = XmlSubstrateData.GetAttributeNode("LotNo");
                        XmlLotNoValue.Value = sLotNo;

                        //ScheduleNo
                        XmlAttribute XmlScheduleNoValue = XmlSubstrateData.GetAttributeNode("ScheduleNo");
                        XmlScheduleNoValue.Value = sScheduleNo;

                        //MapCreateTime
                        XmlAttribute XmlMapCreateTimeValue = XmlSubstrateData.GetAttributeNode("MapCreateTime");
                        XmlMapCreateTimeValue.Value = dMapCreateTime.ToString("yyyy/MM/dd\tHH:mm:ss");
                        
                        //MapUpdataTime
                        XmlAttribute XmlMapUpdateTimeValue = XmlSubstrateData.GetAttributeNode("MapUpdateTime");
                        XmlMapUpdateTimeValue.Value = dMapUpdataTime.ToString("yyyy/MM/dd\tHH:mm:ss");

                        //PassCount
                        XmlAttribute XmlPassCountValue = XmlSubstrateData.GetAttributeNode("PassCount");
                        XmlPassCountValue.Value = sPassCount;

                        //FailCount
                        XmlAttribute XmlFailCountValue = XmlSubstrateData.GetAttributeNode("FailCount");
                        XmlFailCountValue.Value = sFailCount;

                        //UglyCount
                        XmlAttribute XmlUglyCountValue = XmlSubstrateData.GetAttributeNode("UglyCount");
                        XmlUglyCountValue.Value = sUglyCount;

                    }

                    XmlNode xmlAliasIds = objDoc.SelectSingleNode("/Map:MapData/Map:Substrates/Map:Substrate/Map:AliasIds", nsmgr);

                    foreach (XmlNode Code in xmlAliasIds.ChildNodes)
                    {
                        XmlElement XmlSAliasIdsData = (XmlElement)Code;

                        //AliasID Type
                        XmlAttribute XmlAliasIDTypeValue = XmlSAliasIdsData.GetAttributeNode("Value");
                        XmlAliasIDTypeValue.Value = sCoverId;
                    }

                    objDoc.Save(ucHost_SECS.GetSingleton().g_strTraceabilityDataPath + sSubstrateId + ".xml");

                    string sTraceabilityValue = objDoc.InnerXml.ToString();
                    ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.TransferDatabySubstarate_1, sTraceabilityValue);
                    
                    ucHost_SECS.GetSingleton().SECS_DispatchEvent((int)clsEnum.eSECS_CEID.TransferDatabySubstarate_1);

                }

            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at Traceability 資料上傳\r\n" + ex.ToString());
            }
        }

        public static void SetTraceabilityToXML_2(string sSubstrateType, string sSubstrateId, string sHostName, string sLotId, string sLotNo, string sScheduleNo, DateTime dMapCreateTime, DateTime dMapUpdataTime, string sPassCount, string sFailCount, string sUglyCount, DateTime dTestStartTime, DateTime dTestEndTime, string sCoverId)
        {
            try
            {

                XmlDocument objDoc = new XmlDocument();

                if (File.Exists(ucHost_SECS.GetSingleton().g_strTraceabilityDataPath + "Traceability.xml") == true)
                {
                    objDoc.Load(ucHost_SECS.GetSingleton().g_strTraceabilityDataPath + "Traceability.xml");

                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(objDoc.NameTable);
                    XmlNode FirstNode = objDoc.ChildNodes[1];
                    nsmgr.AddNamespace("Map", FirstNode.NamespaceURI);

                    XmlNode xmlSubstrates = objDoc.SelectSingleNode("/Map:MapData/Map:Substrates", nsmgr);

                    foreach (XmlNode Code in xmlSubstrates.ChildNodes)
                    {
                        XmlElement XmlSubstrateData = (XmlElement)Code;

                        //SubstrateType
                        XmlAttribute XmlSubstrateTypeValue = XmlSubstrateData.GetAttributeNode("SubstrateType");
                        XmlSubstrateTypeValue.Value = sSubstrateType;

                        //SubstrateId
                        XmlAttribute XmlSubstrateIdValue = XmlSubstrateData.GetAttributeNode("SubstrateId");
                        XmlSubstrateIdValue.Value = sSubstrateId;

                        //HostName
                        XmlAttribute XmlHostNameValue = XmlSubstrateData.GetAttributeNode("HostName");
                        XmlHostNameValue.Value = sHostName;

                        //LotId
                        XmlAttribute XmlLotIdValue = XmlSubstrateData.GetAttributeNode("LotId");
                        XmlLotIdValue.Value = sLotId;

                        //LotNo
                        XmlAttribute XmlLotNoValue = XmlSubstrateData.GetAttributeNode("LotNo");
                        XmlLotNoValue.Value = sLotNo;

                        //ScheduleNo
                        XmlAttribute XmlScheduleNoValue = XmlSubstrateData.GetAttributeNode("ScheduleNo");
                        XmlScheduleNoValue.Value = sScheduleNo;

                        //MapCreateTime
                        XmlAttribute XmlMapCreateTimeValue = XmlSubstrateData.GetAttributeNode("MapCreateTime");
                        XmlMapCreateTimeValue.Value = dMapCreateTime.ToString("yyyy/MM/dd\tHH:mm:ss");

                        //MapUpdataTime
                        XmlAttribute XmlMapUpdateTimeValue = XmlSubstrateData.GetAttributeNode("MapUpdateTime");
                        XmlMapUpdateTimeValue.Value = dMapUpdataTime.ToString("yyyy/MM/dd\tHH:mm:ss");

                        //PassCount
                        XmlAttribute XmlPassCountValue = XmlSubstrateData.GetAttributeNode("PassCount");
                        XmlPassCountValue.Value = sPassCount;

                        //FailCount
                        XmlAttribute XmlFailCountValue = XmlSubstrateData.GetAttributeNode("FailCount");
                        XmlFailCountValue.Value = sFailCount;

                        //UglyCount
                        XmlAttribute XmlUglyCountValue = XmlSubstrateData.GetAttributeNode("UglyCount");
                        XmlUglyCountValue.Value = sUglyCount;

                    }

                    XmlNode xmlAliasIds = objDoc.SelectSingleNode("/Map:MapData/Map:Substrates/Map:Substrate/Map:AliasIds", nsmgr);

                    foreach (XmlNode Code in xmlAliasIds.ChildNodes)
                    {
                        XmlElement XmlSAliasIdsData = (XmlElement)Code;

                        //AliasID Type
                        XmlAttribute XmlAliasIDTypeValue = XmlSAliasIdsData.GetAttributeNode("Value");
                        XmlAliasIDTypeValue.Value = sCoverId;
                    }

                    objDoc.Save(ucHost_SECS.GetSingleton().g_strTraceabilityDataPath + sSubstrateId + ".xml");

                    string sTraceabilityValue = objDoc.InnerXml.ToString();
                    ucHost_SECS.GetSingleton().SECS_UpdateSVID((int)clsEnum.eSECS_SVID.TransferDatabySubstarate_2, objDoc.InnerXml.ToString());
                    ucHost_SECS.GetSingleton().SECS_DispatchEvent((int)clsEnum.eSECS_CEID.TransferDatabySubstarate_2);

                }

            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at Traceability 資料上傳\r\n" + ex.ToString());
            }
        }

        public string ReturnRes(string m_Res)
        {
            string p_Res = "0";
            if (m_Res != null)
            {
                p_Res = m_Res;
            }
            return p_Res;
        }
        #endregion

    }
}

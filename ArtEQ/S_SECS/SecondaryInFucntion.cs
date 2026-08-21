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
using System.Xml;

namespace ArtEQ
{
    class SecondaryInFucntion
    {
        #region S14FX Function (Stream 14 Terminal Services)

        public static List<string> m_lstMapData = new List<string>();

        public static void SecondaryIn_S14F2(SecsEvents_SecondaryInEvent e)
        {
            try
            {
                string strOBJID;
                string strATTRDATA;

                strOBJID = e.trans.Secondary.Item[1].Item[1].Item[1].Item[1].Value.ToString();
                strATTRDATA = e.trans.Secondary.Item[1].Item[1].Item[1].Item[2].Item[1].Item[2].Value.ToString();

                string sSubstrateID = e.trans.Secondary.Item[1].Item[1].Item[1].Item[1].Value.ToString();
                string sAttributeID = e.trans.Secondary.Item[1].Item[1].Item[1].Item[2].Item[1].Item[1].Value.ToString();
                string sAttributeData = e.trans.Secondary.Item[1].Item[1].Item[1].Item[2].Item[1].Item[2].Value.ToString();
                string sObjAck = "";

                sObjAck = e.trans.Secondary.Item[1].Item[2].Item[1].Value.ToString();

                string strFilePath = ucHost_SECS.GetSingleton().g_strMapDownloadPath;
                string strFileName = sSubstrateID + ".xml";
                SaveFile(strFilePath, strFileName, sAttributeData);
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S14F2()\r\n" + ex.ToString());
            }
        }

        public static void MapFileDelete(string FileName)
        {
            string _filePath = ucHost_SECS.GetSingleton().g_strMapDownloadPath + FileName + ".xml";
            File.Delete(_filePath);
        }

        public static void SaveFile(string PathName, string FileName, string Content)
        {
            try
            {
                if (!System.IO.Directory.Exists(PathName))
                    // 如不存在，建立資料夾
                    System.IO.Directory.CreateDirectory(PathName);
                StreamWriter swWriter = new StreamWriter(PathName + FileName, false, System.Text.Encoding.UTF8);
                swWriter.Write(Content);
                swWriter.Close();
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S14F2()\r\n" + ex.ToString());
            }
        }

        public static void GetMapData(string FileName)
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
                    m_lstMapData.Clear();

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
                                    m_lstMapData.Add("OK");
                                    intHostGoodDieQTY++;
                                    break;

                                default:
                                    m_lstMapData.Add("NG");
                                    intHostBadDieQTY++;
                                    break;
                            }

                            iCount = 0;
                        }
                    }
                    ucHost_SECS.GetSingleton().TraceLog(ucHost_SECS.TraceLogType.EquipmentEvent, "S14F2 Total Count : " + m_lstMapData.Count.ToString() + " , Good Die : " + intHostGoodDieQTY.ToString() + " , Bad Die : " + intHostBadDieQTY.ToString());
                }
            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S14F2()\r\n" + ex.ToString());
            }
        }

        public static void SecondaryIn_S14F4(SecsEvents_SecondaryInEvent e)
        {
            try
            {
                string strOBJID;
                string strATTRDATA;

                strOBJID = e.trans.Secondary.Item[1].Item[1].Item[1].Item[1].Value.ToString();
                strATTRDATA = e.trans.Secondary.Item[1].Item[1].Item[1].Item[2].Item[1].Item[2].Value.ToString();

            }
            catch (Exception ex)
            {
                ucHost_SECS.GetSingleton().m_TraceLog.WriteLog("Exception at PrimaryIn_S14F4()\r\n" + ex.ToString());
            }
        }

        #endregion

    }
}

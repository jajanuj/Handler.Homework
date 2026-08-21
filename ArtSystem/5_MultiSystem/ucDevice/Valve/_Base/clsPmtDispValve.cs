using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

using System.Net;
using System.Net.Sockets;
using FtdAdapter;
using Modbus.Data;
using Modbus.Device;
using Modbus.Utility;
using ArtData;
using ArtCommonLib;
using ArtControlLib;

namespace ArtSystem.MultiSystem
{
    public class clsPmtDispValve
    {
        #region//========== Enum 定義 ==========

        public enum enuPmtName
        {
            sValveName,
            /// <summary> 0:None, 1:ArtSpray, 2:ArtPZT </summary>
            eValveType,

            Serial_COMID,
            Serial_BaudRate,//19200
            Serial_DataBits,//8
            Serial_StationID,//1
            Serial_TimeOut,//1000
            Serial_Handshake,//None
            Serial_StopBits,//One
            Serial_Parity,//None
            //Serial_bDtr,

            eDI_Ready,
            eDI_Done,
            eDO_ManualTrigger,
            eDO_TriggerEnable,
            eDO_Interrupt,
        }

        #endregion

        #region//========== 參數 ==========

        public string sINIPath
        {
            get;
            private set;
        }

        public Dictionary<string, Dictionary<enuPmtName, string>> mDic_mPmtValue = new Dictionary<string, Dictionary<enuPmtName, string>>();

        public Dictionary<string, clsCtrlDispValve> mDic_CtrlDispValve = new Dictionary<string, clsCtrlDispValve>();

        #endregion

        #region//========== Public函式 ==========

        public object GetDispValve(int iIndex)
        {
            if (iIndex >= 0 && iIndex < mDic_CtrlDispValve.Count)
            {
                return mDic_CtrlDispValve.ElementAt(iIndex).Value;
            }
            return null;
        }
        public object GetDispValve(string sKey)
        {
            if (mDic_CtrlDispValve.ContainsKey(sKey))
            {
                return mDic_CtrlDispValve[sKey];
            }
            return null;
        }
        public void InitialDispValve()
        {
            string sPath = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath + "\\DispValve.ini";
            if (System.IO.File.Exists("D:\\Parameter\\INI\\DispValve.ini") == true)
            { sPath = "D:\\Parameter\\INI\\DispValve.ini"; }
            Load(sPath);
            try
            {
                foreach (string sKey in mDic_mPmtValue.Keys)
                {
                    
                    clsCtrlDispValve.enuValveType eControllerType = clsCtrlDispValve.enuValveType.Non;
                    Enum.TryParse<clsCtrlDispValve.enuValveType>(mDic_mPmtValue[sKey][enuPmtName.eValveType], out eControllerType);
                    int iSerial_COMID = 1;
                    int iSerial_BaudRate = 9600;
                    int iSerial_DataBits = 8;
                    int iSerial_StationID = 0;
                    int iSerial_Timeout = 1000;
                    Handshake eSerial_Handshake = Handshake.None;
                    StopBits eSerial_StopBits = StopBits.One;
                    Parity eSerial_Parity = Parity.None;
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_COMID], ref iSerial_COMID);
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_BaudRate], ref iSerial_BaudRate);
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_DataBits], ref iSerial_DataBits);
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_StationID], ref iSerial_StationID);
                    Enum.TryParse<Handshake>(mDic_mPmtValue[sKey][enuPmtName.Serial_Handshake], out eSerial_Handshake);
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_TimeOut], ref iSerial_Timeout);
                    Enum.TryParse<StopBits>(mDic_mPmtValue[sKey][enuPmtName.Serial_StopBits], out eSerial_StopBits);
                    Enum.TryParse<Parity>(mDic_mPmtValue[sKey][enuPmtName.Serial_Parity], out eSerial_Parity);

                    switch (eControllerType)
                    {
                        case clsCtrlDispValve.enuValveType.Non:
                            mDic_CtrlDispValve.Add(sKey, new clsCtrlDispValve());
                            mDic_CtrlDispValve[sKey].InitialValve(sKey, "COM" + iSerial_COMID, iSerial_StationID,
                               clsArtSystem.bIsSoftwareSimulate, iSerial_Timeout);
                            break;
                        case clsCtrlDispValve.enuValveType.ArtSpray:
                            {
                                mDic_CtrlDispValve.Add(sKey, new clsCtrlDispValve_ArtSpray());
                                mDic_CtrlDispValve[sKey].InitialValve(sKey, "COM" + iSerial_COMID, iSerial_StationID,
                                   clsArtSystem.bIsSoftwareSimulate, iSerial_Timeout);
                            }
                            break;
                        case clsCtrlDispValve.enuValveType.ArtPZT:
                            {
                                mDic_CtrlDispValve.Add(sKey, new clsCtrlDispValve_ArtPZT());
                                mDic_CtrlDispValve[sKey].InitialValve(sKey, "COM" + iSerial_COMID, iSerial_StationID,
                                   clsArtSystem.bIsSoftwareSimulate, iSerial_Timeout);
                            }
                            break;
                        default:
                            break;
                    }

                    if (Enum.IsDefined(typeof(clsEnum.enuDi), mDic_mPmtValue[sKey][enuPmtName.eDI_Ready]) == true)
                    { mDic_CtrlDispValve[sKey].g_eDI_Ready = (clsEnum.enuDi)Enum.Parse(typeof(clsEnum.enuDi), mDic_mPmtValue[sKey][enuPmtName.eDI_Ready]); }
                    else
                    { mDic_CtrlDispValve[sKey].g_eDI_Ready = null; }
                    if (Enum.IsDefined(typeof(clsEnum.enuDi), mDic_mPmtValue[sKey][enuPmtName.eDI_Done]) == true)
                    { mDic_CtrlDispValve[sKey].g_eDI_Done = (clsEnum.enuDi)Enum.Parse(typeof(clsEnum.enuDi), mDic_mPmtValue[sKey][enuPmtName.eDI_Done]); }
                    else
                    { mDic_CtrlDispValve[sKey].g_eDI_Done = null; }

                    if (Enum.IsDefined(typeof(clsEnum.enuDo), mDic_mPmtValue[sKey][enuPmtName.eDO_ManualTrigger]) == true)
                    { mDic_CtrlDispValve[sKey].g_eDO_ManualTrigger = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), mDic_mPmtValue[sKey][enuPmtName.eDO_ManualTrigger]); }
                    else
                    { mDic_CtrlDispValve[sKey].g_eDO_ManualTrigger = null; }
                    if (Enum.IsDefined(typeof(clsEnum.enuDo), mDic_mPmtValue[sKey][enuPmtName.eDO_TriggerEnable]) == true)
                    { mDic_CtrlDispValve[sKey].g_eDO_TriggerEnable = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), mDic_mPmtValue[sKey][enuPmtName.eDO_TriggerEnable]); }
                    else
                    { mDic_CtrlDispValve[sKey].g_eDO_TriggerEnable = null; }
                    if (Enum.IsDefined(typeof(clsEnum.enuDo), mDic_mPmtValue[sKey][enuPmtName.eDO_Interrupt]) == true)
                    { mDic_CtrlDispValve[sKey].g_eDO_Interrupt = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), mDic_mPmtValue[sKey][enuPmtName.eDO_Interrupt]); }
                    else
                    { mDic_CtrlDispValve[sKey].g_eDO_Interrupt = null; }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                formMessageBox.Show("Initial Disp Valve, Catch Error");
            }
        }

        #endregion

        #region//========== private函式 ==========

        private void ConvertStringToInt(string sValue, ref int iValue)
        {
            if (sValue.Length > 0)
            {
                try
                { 
                    iValue = Convert.ToInt32(sValue);
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }
        }

        #endregion

        #region//========== Public函式(Save,Load) ==========

        /// <summary> 儲存參數 </summary>
        public void Save(string sPath)
        {
            try
            {
                sINIPath = sPath;
                clsIniFile mFile = new clsIniFile(sPath);
                foreach (string SectionName in mDic_mPmtValue.Keys)
                {
                    foreach (enuPmtName PmtName in mDic_mPmtValue[SectionName].Keys)
                    {
                        mFile.WriteValue(SectionName, PmtName.ToString(), mDic_mPmtValue[SectionName][PmtName]);
                    }
                }
                Load(sINIPath);
            }
            catch (Exception ex)
            {

                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 寫入參數 </summary>
        public void Load(string sPath)
        {
            try
            {
                sINIPath = sPath;
                //mDic_mPmtValue.Clear();
                if (System.IO.File.Exists(sPath) == true)
                {
                    clsIniFile mFile = new clsIniFile(sPath);
                    string[] SectionName = mFile.GetSectionNames();
                    List<string> RemoveItems = new List<string>();
                    foreach (string sKey in mDic_mPmtValue.Keys)
                    {
                        RemoveItems.Add(sKey);
                    }
                    for (int iSection = 0; iSection < SectionName.Length; iSection++)
                    {
                        if (mDic_mPmtValue.ContainsKey(SectionName[iSection]) == false)
                        {
                            mDic_mPmtValue.Add(SectionName[iSection], new Dictionary<enuPmtName, string>());
                        }
                        else
                        {
                            if(RemoveItems.Contains( SectionName[iSection]) == true)
                            {
                                RemoveItems.Remove(SectionName[iSection]);
                            }
                        }
                    }
                    foreach (string sKey in RemoveItems)
                    {
                        mDic_mPmtValue.Remove(sKey);
                    }
                    for (int iSection = 0; iSection < SectionName.Length; iSection++)
                    {
                        string[] PmtName = mFile.GetKeyNames(SectionName[iSection]);
                        for (int iKey = 0; iKey < PmtName.Length; iKey++)
                        {
                            if (Enum.IsDefined(typeof(enuPmtName), PmtName[iKey]) == true)
                            {
                                enuPmtName mPmt = (enuPmtName)Enum.Parse(typeof(enuPmtName), PmtName[iKey]);
                                if (Enum.IsDefined(typeof(enuPmtName), mPmt) == true)
                                {
                                    mDic_mPmtValue[SectionName[iSection]][mPmt] = mFile.GetString(SectionName[iSection], PmtName[iKey], "");
                                    mDic_mPmtValue[SectionName[iSection]][enuPmtName.sValveName] = SectionName[iSection];
                                }
                            }
                        }
                        foreach (enuPmtName pPmtName in Enum.GetValues(typeof(enuPmtName)))
                        {
                            if (mDic_mPmtValue[SectionName[iSection]].ContainsKey(pPmtName) == false)
                            {
                                mDic_mPmtValue[SectionName[iSection]].Add(pPmtName, "");
                            }
                        }
                    }
                }
                else
                {
                    mDic_mPmtValue.Clear();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion
    }
}

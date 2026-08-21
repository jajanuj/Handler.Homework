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
    public class clsPmtWeightScale
    {
        #region//========== Enum 定義 ==========

        public enum enuPmtName
        {
            sControllerName,
            eControllerType,

            Serial_COMID,
            Serial_BaudRate,//9600
            Serial_DataBits,//8
            //Serial_StationID,
            Serial_TimeOut,//1000
            Serial_Handshake,//None
            Serial_StopBits,//One
            Serial_Parity,//None
        }

        #endregion

        #region//========== 參數 ==========

        public string sINIPath
        {
            get;
            private set;
        }

        public Dictionary<string, Dictionary<enuPmtName, string>> mDic_mPmtValue = new Dictionary<string, Dictionary<enuPmtName, string>>();

        public Dictionary<string, clsCtrlWeightScale> mDic_CtrlWeightScale = new Dictionary<string, clsCtrlWeightScale>();

        #endregion

        #region//========== Public函式 ==========

        public clsCtrlWeightScale GetWeightScale(int iIndex)
        {
            if (iIndex >= 0 && iIndex < mDic_CtrlWeightScale.Count)
            {
                return mDic_CtrlWeightScale.ElementAt(iIndex).Value;
            }
            return null;
        }
        public clsCtrlWeightScale GetWeightScale(string sKey)
        {
            if (mDic_CtrlWeightScale.ContainsKey(sKey))
            {
                return mDic_CtrlWeightScale[sKey];
            }
            return null;
        }
        public void InitialWeightScale()
        {
            string sPath = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath + "\\WeightScale.ini";
            if (System.IO.File.Exists("D:\\Parameter\\INI\\WeightScale.ini") == true)
            { sPath = "D:\\Parameter\\INI\\WeightScale.ini"; }
            Load(sPath);
            try
            {
                foreach (string sKey in mDic_mPmtValue.Keys)
                {

                    clsCtrlWeightScale.enuModuleType eControllerType = clsCtrlWeightScale.enuModuleType.MettlerToledoWX;
                    Enum.TryParse<clsCtrlWeightScale.enuModuleType>(mDic_mPmtValue[sKey][enuPmtName.eControllerType], out eControllerType);

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
                    //ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_StationID], ref iSerial_StationID);
                    Enum.TryParse<Handshake>(mDic_mPmtValue[sKey][enuPmtName.Serial_Handshake], out eSerial_Handshake);
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_TimeOut], ref iSerial_Timeout);
                    Enum.TryParse<StopBits>(mDic_mPmtValue[sKey][enuPmtName.Serial_StopBits], out eSerial_StopBits);
                    Enum.TryParse<Parity>(mDic_mPmtValue[sKey][enuPmtName.Serial_Parity], out eSerial_Parity);
                    
                    mDic_CtrlWeightScale.Add(sKey, new clsCtrlWeightScale(eControllerType, sKey));
                    mDic_CtrlWeightScale[sKey].bIsSimulatorMode = clsArtSystem.bIsSoftwareSimulate;
                    mDic_CtrlWeightScale[sKey].Com_SetPmt("COM" + iSerial_COMID, iSerial_BaudRate, 
                        eSerial_Parity, iSerial_DataBits, eSerial_StopBits, true, iSerial_Timeout, eSerial_Handshake);

                    mDic_CtrlWeightScale[sKey].Com_Open();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                formMessageBox.Show("Initial Heater Module, Catch Error");
            }
        }

        #endregion

        #region//========== private函式 ==========

        private void ConvertStringToInt(string sValue, ref int iValue)
        {
            if (sValue.Length > 0)
            {
                try
                { iValue = Convert.ToInt32(sValue); }
                catch (Exception ex)
                { clsArtSystem.CatchLog(ex); }
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
                if (System.IO.File.Exists(sPath) == true)
                { System.IO.File.Delete(sPath); }
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
                                    mDic_mPmtValue[SectionName[iSection]][enuPmtName.sControllerName] = SectionName[iSection];
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

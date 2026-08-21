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
    public class clsPmtHeaterModule
    {
        #region//========== Enum 定義 ==========

        public enum enuPmtName
        {
            sControllerName,
            eControllerType,

            Serial_COMID,
            Serial_BaudRate,//9600
            Serial_DataBits,//8
            Serial_StationID,
            Serial_TimeOut,//1000
            Serial_Handshake,//None
            Serial_StopBits,//One
            Serial_Parity,//None
            //Serial_bDtr,

            Temp_ErrorRange,
            Temp_Limit,
            enuSys_Temp_ShiftOffset,

            eDo_Enable,
            eDi_OverHeat_BType,
            //ePmt_Software_Enable,
            //ePmt_Software_TempOverHeat,
            //ePmt_Software_TempErrorRange,
            //ePmt_Software_TempTarget,
            //ePmt_Software_TempSiftOffset,

            //Simulate,
            //ByPass,
        }

        #endregion

        #region//========== 參數 ==========

        public string sINIPath
        {
            get;
            private set;
        }

        public Dictionary<string, Dictionary<enuPmtName, string>> mDic_mPmtValue = new Dictionary<string, Dictionary<enuPmtName, string>>();

        public Dictionary<string, clsCtrlHeaterModule> mDic_CtrlHeaterModule = new Dictionary<string, clsCtrlHeaterModule>();

        #endregion

        #region//========== Public函式 ==========

        public clsCtrlHeaterModule GetHeaterModule(int iIndex)
        {
            if (iIndex >= 0 && iIndex < mDic_CtrlHeaterModule.Count)
            {
                return mDic_CtrlHeaterModule.ElementAt(iIndex).Value;
            }
            return null;
        }
        public clsCtrlHeaterModule GetHeaterModule(string sKey)
        {
            if (mDic_CtrlHeaterModule.ContainsKey(sKey))
            {
                return mDic_CtrlHeaterModule[sKey];
            }
            return null;
        }
        public void InitialHeaterModule()
        {
            clsCtrlHeaterModule.IsSimulatorMode = ArtSystem.clsArtSystem.bIsSoftwareSimulate;
            string sPath = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath + "\\HeaterModule.ini";
            if (System.IO.File.Exists("D:\\Parameter\\INI\\HeaterModule.ini") == true)
            { sPath = "D:\\Parameter\\INI\\HeaterModule.ini"; }
            Load(sPath);
            try
            {
                foreach (string sKey in mDic_mPmtValue.Keys)
                {

                    clsCtrlHeaterModule.enuControllerType eControllerType = clsCtrlHeaterModule.enuControllerType.DTK4848_V12;
                    Enum.TryParse<clsCtrlHeaterModule.enuControllerType>(mDic_mPmtValue[sKey][enuPmtName.eControllerType], out eControllerType);

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
                    
                    mDic_CtrlHeaterModule.Add(sKey, new clsCtrlHeaterModule(sKey));
                    mDic_CtrlHeaterModule[sKey].InitialHeater(eControllerType, iSerial_StationID, "COM" + iSerial_COMID,
                        iSerial_BaudRate, iSerial_DataBits, eSerial_Handshake, iSerial_Timeout, eSerial_StopBits, eSerial_Parity);
                    mDic_CtrlHeaterModule[sKey].Open();
                    if (clsArtSystem.bIsSoftwareSimulate == true)
                    {
                        mDic_CtrlHeaterModule[sKey].m_CtrlHeater.m_dCurrentTemp = 30;
                    }

                    
                    if (Enum.IsDefined(typeof(clsEnum.enuDi), mDic_mPmtValue[sKey][enuPmtName.eDi_OverHeat_BType]) == true)
                    { mDic_CtrlHeaterModule[sKey].eDi_OverHeat = (clsEnum.enuDi)Enum.Parse(typeof(clsEnum.enuDi), mDic_mPmtValue[sKey][enuPmtName.eDi_OverHeat_BType]); }
                    else
                    { mDic_CtrlHeaterModule[sKey].eDi_OverHeat = null; }

                    if (Enum.IsDefined(typeof(clsEnum.enuDo), mDic_mPmtValue[sKey][enuPmtName.eDo_Enable]) == true)
                    { mDic_CtrlHeaterModule[sKey].eDo_Enable = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), mDic_mPmtValue[sKey][enuPmtName.eDo_Enable]); }
                    else
                    { mDic_CtrlHeaterModule[sKey].eDo_Enable = null; }

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

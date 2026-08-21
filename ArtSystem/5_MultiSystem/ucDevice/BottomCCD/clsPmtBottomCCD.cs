using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using System.IO.Ports;
using System.Net.Sockets;
using System.Net;

namespace ArtSystem.MultiSystem
{
    public class clsPmtBottomCCD
    {
        #region//========== Enum 定義 ==========

        public enum enuPmtName
        {
            SensorName,
            SensorModule,
            SensorType,

            TCP_IP,
            TCP_Port,

            Serial_COMID,
            Serial_BaudRate,
            Serial_DataBits,
            Serial_StationID,

            Invert,

            eDo_Cylinder,
            eDi_CCDResult,

            TimeOut_ms,//3000
            DelayTime_ms,
            Simulate,
            ByPass,
            SavePath,
            CCDType,
        }

        #endregion

        #region//========== 參數 ==========

        public string sINIPath
        {
            get;
            private set;
        }

        public Dictionary<string, Dictionary<enuPmtName, string>> mDic_mPmtValue = new Dictionary<string, Dictionary<enuPmtName, string>>();

        public Dictionary<string, clsCtrlBottomCCD> mDic_CtrlBottomCCD = new Dictionary<string, clsCtrlBottomCCD>();

        #endregion

        #region//========== Public函式 ==========

        public clsCtrlBottomCCD GetBottomCCD(int iIndex)
        {
            if (iIndex >= 0 && iIndex < mDic_CtrlBottomCCD.Count)
            {
                return mDic_CtrlBottomCCD.ElementAt(iIndex).Value;
            }
            return null;
        }
        public clsCtrlBottomCCD GetBottomCCD(string sKey)
        {
            if (mDic_CtrlBottomCCD.ContainsKey(sKey))
            {
                return mDic_CtrlBottomCCD[sKey];
            }
            return null;
        }
        public void InitialBottomCCD()
        {
            string sPath = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath + "\\BottomCCD.ini";
            if (System.IO.File.Exists("D:\\Parameter\\INI\\BottomCCD.ini") == true)
            { sPath = "D:\\Parameter\\INI\\BottomCCD.ini"; }
            Load(sPath);
            try
            {
                foreach (string sKey in mDic_mPmtValue.Keys)
                {
                    clsCtrlBottomCCD.enuCCDType eSensorType = (clsCtrlBottomCCD.enuCCDType)Enum.Parse(typeof(clsCtrlBottomCCD.enuCCDType), mDic_mPmtValue[sKey][enuPmtName.SensorType]);
                    if (Enum.IsDefined(typeof(clsCtrlBottomCCD.enuCCDType), eSensorType) == false)
                    { eSensorType = clsCtrlBottomCCD.enuCCDType.IV3; }

                    mDic_CtrlBottomCCD.Add(sKey, new clsCtrlBottomCCD(sKey));


                    int iTCPIP_Port = 0;
                    int iSerial_COMID = 1;
                    int iSerial_BaudRate = 9600;
                    int iSerial_DataBits = 8;
                    int iSerial_StationID = 0;
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.TCP_Port], ref iTCPIP_Port);
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_BaudRate], ref iSerial_BaudRate);
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_DataBits], ref iSerial_DataBits);
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_StationID], ref iSerial_StationID);

                    if (Enum.IsDefined(typeof(clsEnum.enuDi), mDic_mPmtValue[sKey][enuPmtName.eDi_CCDResult]) == true)
                    { mDic_CtrlBottomCCD[sKey].eDICCD = (clsEnum.enuDi)Enum.Parse(typeof(clsEnum.enuDi), mDic_mPmtValue[sKey][enuPmtName.eDi_CCDResult]); }
                    else
                    { mDic_CtrlBottomCCD[sKey].eDICCD = null;  }
                    if (Enum.IsDefined(typeof(clsEnum.enuDo), mDic_mPmtValue[sKey][enuPmtName.eDo_Cylinder]) == true)
                    { mDic_CtrlBottomCCD[sKey].eDOCylinder = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), mDic_mPmtValue[sKey][enuPmtName.eDo_Cylinder]); }
                    else
                    { mDic_CtrlBottomCCD[sKey].eDOCylinder = null; }

                    switch (eSensorType)
                    {
                        case clsCtrlBottomCCD.enuCCDType.IV3:
                            mDic_CtrlBottomCCD[sKey].Initial_IV3_TCPIP(mDic_mPmtValue[sKey][enuPmtName.TCP_IP], iTCPIP_Port);
                            break;
                        case clsCtrlBottomCCD.enuCCDType.CCD:
                            //mDic_CtrlBottomCCD[sKey].Initial_ILCCDSensor_ComPort("COM" + iSerial_COMID, iSerial_BaudRate, iSerial_DataBits, iSerial_StationID, 1000);
                            break;
                        default:
                            break;
                    }
                    mDic_CtrlBottomCCD[sKey].bLogicInvert = mDic_mPmtValue[sKey][enuPmtName.Invert] == "1";
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                formMessageBox.Show("Initial Bottom CCD, Catch Error");
            }
        }

        public void Release()
        {
            try
            {
                foreach (string sKey in mDic_CtrlBottomCCD.Keys)
                {
                    switch (mDic_CtrlBottomCCD[sKey].eSensorType)
                    {
                        case clsCtrlBottomCCD.enuCCDType.IV3:
                            break;
                        case clsCtrlBottomCCD.enuCCDType.CCD:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
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
                    {  RemoveItems.Add(sKey); }
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
                            enuPmtName mPmt = (enuPmtName)Enum.Parse(typeof(enuPmtName), PmtName[iKey]);
                            if (Enum.IsDefined(typeof(enuPmtName), mPmt) == true)
                            {
                                mDic_mPmtValue[SectionName[iSection]][mPmt] = mFile.GetString(SectionName[iSection], PmtName[iKey], "");
                                mDic_mPmtValue[SectionName[iSection]][enuPmtName.SensorName] = SectionName[iSection];
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

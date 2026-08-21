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
    public class clsPmtHighSensor
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
            eDi_LaserResult,

            TimeOut_ms,//3000
            DelayTime_ms,
            Simulate,
            ByPass,
        }

        #endregion

        #region//========== 參數 ==========

        public string sINIPath
        {
            get;
            private set;
        }

        public Dictionary<string, Dictionary<enuPmtName, string>> mDic_mPmtValue = new Dictionary<string, Dictionary<enuPmtName, string>>();

        public Dictionary<string, clsCtrlHighSensor> mDic_CtrlHighSensor = new Dictionary<string, clsCtrlHighSensor>();

        #endregion

        #region//========== Public函式 ==========

        public clsCtrlHighSensor GetHighSensor(int iIndex)
        {
            if (iIndex >= 0 && iIndex < mDic_CtrlHighSensor.Count)
            {
                return mDic_CtrlHighSensor.ElementAt(iIndex).Value;
            }
            return null;
        }
        public clsCtrlHighSensor GetHighSensor(string sKey)
        {
            if (mDic_CtrlHighSensor.ContainsKey(sKey))
            {
                return mDic_CtrlHighSensor[sKey];
            }
            return null;
        }
        public void InitialHighSensor()
        {
            string sPath = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath + "\\HighSensor.ini";
            if (System.IO.File.Exists("D:\\Parameter\\INI\\HighSensor.ini") == true)
            { sPath = "D:\\Parameter\\INI\\HighSensor.ini"; }
            Load(sPath);
            try
            {
                foreach (string sKey in mDic_mPmtValue.Keys)
                {
                    clsCtrlHighSensor.enuSensorModule eSensorModule = (clsCtrlHighSensor.enuSensorModule)Enum.Parse(typeof(clsCtrlHighSensor.enuSensorModule), mDic_mPmtValue[sKey][enuPmtName.SensorModule]);
                    if (Enum.IsDefined(typeof(clsCtrlHighSensor.enuSensorModule), eSensorModule) == false)
                    { eSensorModule = clsCtrlHighSensor.enuSensorModule.ILLaserSensor_ComPort; }

                    clsCtrlHighSensor.enuSensorType eSensorType = (clsCtrlHighSensor.enuSensorType)Enum.Parse(typeof(clsCtrlHighSensor.enuSensorType), mDic_mPmtValue[sKey][enuPmtName.SensorType]);
                    if (Enum.IsDefined(typeof(clsCtrlHighSensor.enuSensorType), eSensorType) == false)
                    { eSensorType = clsCtrlHighSensor.enuSensorType.Laser; }

                    mDic_CtrlHighSensor.Add(sKey, new clsCtrlHighSensor(sKey));


                    int iTCPIP_Port = 0;
                    int iSerial_COMID = 1;
                    int iSerial_BaudRate = 9600;
                    int iSerial_DataBits = 8;
                    int iSerial_StationID = 0;
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.TCP_Port], ref iTCPIP_Port);
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_COMID], ref iSerial_COMID);
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_BaudRate], ref iSerial_BaudRate);
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_DataBits], ref iSerial_DataBits);
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.Serial_StationID], ref iSerial_StationID);

                    if (Enum.IsDefined(typeof(clsEnum.enuDi), mDic_mPmtValue[sKey][enuPmtName.eDi_LaserResult]) == true)
                    { mDic_CtrlHighSensor[sKey].eDILaser = (clsEnum.enuDi)Enum.Parse(typeof(clsEnum.enuDi), mDic_mPmtValue[sKey][enuPmtName.eDi_LaserResult]); }
                    else
                    { mDic_CtrlHighSensor[sKey].eDILaser = null;  }
                    if (Enum.IsDefined(typeof(clsEnum.enuDo), mDic_mPmtValue[sKey][enuPmtName.eDo_Cylinder]) == true)
                    { mDic_CtrlHighSensor[sKey].eDOCylinder = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), mDic_mPmtValue[sKey][enuPmtName.eDo_Cylinder]); }
                    else
                    { mDic_CtrlHighSensor[sKey].eDOCylinder = null; }

                    switch (eSensorModule)
                    {
                        case clsCtrlHighSensor.enuSensorModule.ILLaserSensor_TCPIP:
                            mDic_CtrlHighSensor[sKey].Initial_ILLaserSensor_TCPIP(mDic_mPmtValue[sKey][enuPmtName.TCP_IP], iTCPIP_Port);
                            break;
                        case clsCtrlHighSensor.enuSensorModule.ILLaserSensor_ComPort:
                            //PublicDeclare.gHMModule[enuDispArm.Arm1].Initial_ILLaserSensor_ComPort("COM6", 38400, 8, 0, 1000);
                            mDic_CtrlHighSensor[sKey].Initial_ILLaserSensor_ComPort("COM" + iSerial_COMID, iSerial_BaudRate, iSerial_DataBits, iSerial_StationID, 1000);
                            break;
                        case clsCtrlHighSensor.enuSensorModule.GTContactSensor:
                            mDic_CtrlHighSensor[sKey].Initial_GTContactSensor("COM" + iSerial_COMID, iSerial_BaudRate, iSerial_DataBits, iSerial_StationID, 1000);
                            break;
                        case clsCtrlHighSensor.enuSensorModule.KeyenceLaser_ComPort:
                            //PublicDeclare.gHMModule[enuDispArm.Arm1].Initial_KeyenceLaser_ComPort("COM6", 115200, 8, 0, 1000);
                            mDic_CtrlHighSensor[sKey].Initial_KeyenceLaser_ComPort("COM" + iSerial_COMID, iSerial_BaudRate, iSerial_DataBits, iSerial_StationID, 1000);
                            break;
                        case clsCtrlHighSensor.enuSensorModule.ILD1420:
                            mDic_CtrlHighSensor[sKey].Initial_ILD1420Sensor("COM" + iSerial_COMID);
                            break;
                        case clsCtrlHighSensor.enuSensorModule.DI_Laser:
                            mDic_CtrlHighSensor[sKey].eSensorType = clsCtrlHighSensor.enuSensorType.Laser;
                            mDic_CtrlHighSensor[sKey].eSensorModule = clsCtrlHighSensor.enuSensorModule.DI_Laser;
                            break;
                        case clsCtrlHighSensor.enuSensorModule.KeyenceLaser_TCPIP_CL3000:
                            mDic_CtrlHighSensor[sKey].Initial_KeyenceLaser_TCPIP_CL3000(mDic_mPmtValue[sKey][enuPmtName.TCP_IP], iTCPIP_Port, iSerial_StationID);
                            break; ;
                        default:
                            break;
                    }
                    mDic_CtrlHighSensor[sKey].bLogicInvert = mDic_mPmtValue[sKey][enuPmtName.Invert] == "1";
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                formMessageBox.Show("Initial High Sensor, Catch Error");
            }
        }

        public void Release()
        {
            try
            {
                foreach (string sKey in mDic_CtrlHighSensor.Keys)
                {
                    switch (mDic_CtrlHighSensor[sKey].eSensorModule)
                    {
                        case clsCtrlHighSensor.enuSensorModule.ILLaserSensor_TCPIP:
                            break;
                        case clsCtrlHighSensor.enuSensorModule.ILLaserSensor_ComPort:
                            break;
                        case clsCtrlHighSensor.enuSensorModule.GTContactSensor:
                            break;
                        case clsCtrlHighSensor.enuSensorModule.KeyenceLaser_ComPort:
                            break;
                        case clsCtrlHighSensor.enuSensorModule.ILD1420:
                            mDic_CtrlHighSensor[sKey].Close_ILD1420Sensor();
                            break;
                        case clsCtrlHighSensor.enuSensorModule.KeyenceLaser_TCPIP_CL3000:
                            break;
                        case clsCtrlHighSensor.enuSensorModule.DI_Laser:
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

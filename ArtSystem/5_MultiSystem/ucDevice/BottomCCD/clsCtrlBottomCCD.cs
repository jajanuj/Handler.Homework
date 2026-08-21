using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtCommunication;
using ArtData;
using System.IO.Ports;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using MicroEpsilon;

namespace ArtSystem.MultiSystem
{
    /// <summary> (ILD1420)需要載入 MEDAQLib.NET.dll，MEDAQLib.dll </summary>
    public class clsCtrlBottomCCD
    {
        #region //=====================  區域變數設置 =====================

        /// <summary> Name </summary>
        public string sName = "";
        /// <summary> 模擬參數 </summary>
        public bool IsSimulatorMode
        {
            get
            {
                return clsArtSystem.bIsSoftwareSimulate;
            }
        }

        public enuCCDType eSensorType = enuCCDType.CCD;
        public enuErrorCode eErrorCode = enuErrorCode.None;

        public double LastHeightValue = -99999;
        public bool bLogicInvert = false;
        public double dSoftwareOffset = 0;
        #endregion

        #region //=====================  通訊模組 =====================

        /// <summary> 通訊模組 TCP </summary>
        static private Dictionary<string, TcpClient> mDicTcpClient = new Dictionary<string, TcpClient>();
        private TcpClient mTcpClient = null;
        static private Dictionary<string, Object> mDicObjLock = new Dictionary<string, object>();
        private Object mObjLock = null;

        private Thread _thread = null;
        /// <summary> 通訊模組 TCP - IP </summary>
        private IPAddress mTcpClient_IP = null;
        /// <summary> 通訊模組 TCP - Port </summary>
        private int mTcpClient_Port = 0;


        /// <summary> 通訊模組 SerialPort </summary>
        public SerialPort mSerialPort = null;
        static int NowIndex = 0;
        public int mSerialPort_StationNo = 0;
        private int mSerialPort_TimeOut = 300;
        public bool mSerialPort_IsConnected = false;
        private System.Diagnostics.Stopwatch mSerialPort_TimeCount = new System.Diagnostics.Stopwatch();
        public clsEnum.enuDi? eDICCD = null;
        public clsEnum.enuDo? eDOCylinder = null;
        static private Dictionary<string, SerialPort> mDic_SerialPort = new Dictionary<string, SerialPort>();
        static private Dictionary<string, Dictionary<string, string>> mSerialPort_strReciveData = new Dictionary<string, Dictionary<string, string>>();
        private string _SerialPort_strReciveData
        {

            get
            {
                if (mSerialPort != null)
                {
                    if (mSerialPort_strReciveData.Keys.Contains(mSerialPort.PortName) == false)
                    { mSerialPort_strReciveData.Add(mSerialPort.PortName, new Dictionary<string, string>()); }
                    if (mSerialPort_strReciveData[mSerialPort.PortName].Keys.Contains(NowIndex.ToString()) == false)
                    { mSerialPort_strReciveData[mSerialPort.PortName].Add(NowIndex.ToString(), ""); }
                    return mSerialPort_strReciveData[mSerialPort.PortName][NowIndex.ToString()];
                }
                return "";
            }
            set
            {
                if (mSerialPort != null)
                {
                    if (mSerialPort_strReciveData.Keys.Contains(mSerialPort.PortName) == false)
                    { mSerialPort_strReciveData.Add(mSerialPort.PortName, new Dictionary<string, string>()); }
                    if (mSerialPort_strReciveData[mSerialPort.PortName].Keys.Contains(NowIndex.ToString()) == false)
                    { mSerialPort_strReciveData[mSerialPort.PortName].Add(NowIndex.ToString(), ""); }
                    mSerialPort_strReciveData[mSerialPort.PortName][NowIndex.ToString()] = value;
                }
            }
        }
        
        /// <summary> 通訊模組 mILD1420 </summary>
        public MEDAQLib mILD1420 = null;

        #endregion

        #region//===================== Enum =====================

        public enum enuCCDType
        {
            CCD,
            IV3,
        }
        
        public enum enuErrorCode
        {
            None,
            Simulate,
            NonResult,
            CatchError,
            Disconnect,
            NetworkCannotRead,
            RespondTimeout,
            RespondDataError,
            ResetZeroOutOfRange,
        }

        public enum enuParameterName
        {
            TimeOut_ms,//3000
            DelayTime_ms,
            Simulate,
            ByPass,
        }

        #endregion

        #region //===================== 必要函式設置 =====================

        public clsCtrlBottomCCD(string p_sBottomCCDName)
        {
            sName = p_sBottomCCDName;
            if (_GetValue(enuParameterName.TimeOut_ms) == 0)
            { _SaveValue(enuParameterName.TimeOut_ms, 3000); }
            _SaveValue(enuParameterName.ByPass, 0);
            _SaveValue(enuParameterName.Simulate, 0);
        }

        ~clsCtrlBottomCCD()
        {
            if (mSerialPort != null && mSerialPort.IsOpen) mSerialPort.Close();
            if (mTcpClient != null && mTcpClient.Connected) mTcpClient.Close();
            try
            {
                if (mILD1420 != null)
                {
                    mILD1420.CloseSensor();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region//===================== Public函式 =====================

        static private object m_object = new object();

        /// <summary> 設定單張檢測  </summary>
        public double _SingleGrab()
        {
            lock (m_object)
            {
                if (IsSimulatorMode == true)
                {
                    eErrorCode = enuErrorCode.Simulate;
                    LastHeightValue = 0.01;
                    return LastHeightValue; 
                }
                else if (_IsConnected() == false)
                {
                    eErrorCode = enuErrorCode.Disconnect;
                    LastHeightValue = -999;
                    return LastHeightValue;
                }

                LastHeightValue = -999;
                eErrorCode = enuErrorCode.NonResult;
                NowIndex = mSerialPort_StationNo;
                switch (eSensorType)
                {
                    case enuCCDType.IV3:
                        eErrorCode = enuErrorCode.None;
                        //LastHeightValue = ILCCDSensor_TCPIP_GetHeightValue();
                        break;
                    case enuCCDType.CCD:
                        eErrorCode = enuErrorCode.None;
                        //LastHeightValue = ILCCDSensor_TCPIP_GetHeightValue();
                        break;
                    default:
                        break;
                }
                LastHeightValue += dSoftwareOffset;
                return LastHeightValue;
            }
        }

        /// <summary> 軟體Offset-每次重新啟動程式都需要重新ResetZero(可以不使用) </summary>
        public bool _ResetZero()
        {
            bool rValue = false;
            lock (m_object)
            {
                if (IsSimulatorMode == true)
                { return false; }
                LastHeightValue = -999;
                eErrorCode = enuErrorCode.NonResult;
                NowIndex = mSerialPort_StationNo;
                switch (eSensorType)
                {
                    case enuCCDType.IV3:
                        eErrorCode = enuErrorCode.None;
                        //LastHeightValue = ILCCDSensor_TCPIP_GetHeightValue();
                        break;
                    case enuCCDType.CCD:
                        eErrorCode = enuErrorCode.None;
                        //LastHeightValue = ILCCDSensor_ComPort_GetHeightValue();
                        break;
                    default:
                        break;
                }
            }
            if (eErrorCode == enuErrorCode.None)
            {
                if (Math.Abs(LastHeightValue) < 5)
                {
                    dSoftwareOffset = -LastHeightValue;
                    LastHeightValue = 0;
                    rValue = true;
                }
                else
                {
                    eErrorCode = enuErrorCode.ResetZeroOutOfRange;
                }
            }
            return rValue;
        }

        /// <summary> 清除軟體Offset參數 </summary>
        public void _ClearOffset()
        {
            eErrorCode = enuErrorCode.None;
            LastHeightValue = -99999;
            dSoftwareOffset = 0;
        }

        /// <summary> 連線 </summary>
        public void _Connect()
        {
            if (IsSimulatorMode == true)
            { return; }
            if (_IsConnected() == false)
            {
                switch (eSensorType)
                {
                    case enuCCDType.IV3:
                        IV3_TCPIP_Connect();
                        break;
                    case enuCCDType.CCD:
                        //ILCCDSensor_ComPort_Connect();
                        break;
                    default:
                        break;
                }
            }
            return;
        }

        /// <summary> 斷線 </summary>
        public void _Disconnect()
        {
            if (IsSimulatorMode == true)
            { return; }
            if (_IsConnected() == true)
            {
                switch (eSensorType)
                {
                    case enuCCDType.IV3:
                        IV3_TCPIP_Disconnect();
                        break;
                    case enuCCDType.CCD:
                        //ILCCDSensor_ComPort_Disconnect();
                        break;
                }
            }
            return;
        }

        /// <summary> 確認連線 </summary>
        public bool _IsConnected()
        {
            if (IsSimulatorMode == true)
            { return false; }
            switch (eSensorType)
            {
                case enuCCDType.IV3:
                    return IV3_TCPIP_IsConnected();
                case enuCCDType.CCD:
                    //return ILCCDSensor_ComPort_IsConnected();
                    return true;
            }
            return false;
        }


        /// <summary> 設定暫存參數，且儲存到INI [\\INI\\Device.ini] </summary>
        public void _SaveValue(enuParameterName p_ParameterName, double p_Value)
        {
            try
            {
                string sPath = ucBottomCCDSetting.GetSingleton().mPmt.sINIPath;
                if (System.IO.File.Exists(sPath) == true
                    && ucBottomCCDSetting.GetSingleton().mPmt.mDic_mPmtValue.ContainsKey(sName) == true
                    && Enum.IsDefined(typeof(clsPmtBottomCCD.enuPmtName), p_ParameterName.ToString()) == true)
                {
                    clsPmtBottomCCD.enuPmtName ePmtName = (clsPmtBottomCCD.enuPmtName)Enum.Parse(typeof(clsPmtBottomCCD.enuPmtName), p_ParameterName.ToString());
                    string PreviousValue = "";
                    if (ucBottomCCDSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].ContainsKey(ePmtName) == true)
                    {
                        PreviousValue = ucBottomCCDSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName];
                    }
                    else
                    {
                        ucBottomCCDSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].Add(ePmtName, "");
                    }
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : "
                           + ", Name : " + sName
                           + ", Pmt Name : " + ePmtName.ToString()
                           + ", Change Value : " + PreviousValue + "-> " + p_Value.ToString());
                    ucBottomCCDSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName] = p_Value.ToString();
                    clsIniFile mFile = new clsIniFile(sPath);
                    mFile.WriteValue(sName, ePmtName.ToString(), ucBottomCCDSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName]);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 讀取暫存參數，暫存參數可以使用LoadINIPrameter() 載入[\\INI\\Device.ini]檔案內容 </summary>
        public double _GetValue(enuParameterName p_ParameterName)
        {
            double rValue = 0;
            try
            {
                string sPath = ucBottomCCDSetting.GetSingleton().mPmt.sINIPath;
                if (System.IO.File.Exists(sPath) == true
                    && ucBottomCCDSetting.GetSingleton().mPmt.mDic_mPmtValue.ContainsKey(sName) == true
                    && Enum.IsDefined(typeof(clsPmtReader.enuPmtName), p_ParameterName.ToString()) == true)
                {
                    clsPmtBottomCCD.enuPmtName ePmtName = (clsPmtBottomCCD.enuPmtName)Enum.Parse(typeof(clsPmtBottomCCD.enuPmtName), p_ParameterName.ToString());
                    if (ucBottomCCDSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].ContainsKey(ePmtName) == false)
                    {
                        ucBottomCCDSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].Add(ePmtName, "");
                    }
                    rValue = Convert.ToDouble(ucBottomCCDSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName]);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        #endregion

        #region//========== Private Function (Switch Module) ==========

        #region IV3_TCPIP (TCPIP)
        public void Initial_IV3_TCPIP(string ip, int port)
        {
            if (IsSimulatorMode == true)
            { return; }
            try
            {
                eSensorType = enuCCDType.IV3;
                mTcpClient_IP = IPAddress.Parse(ip);
                mTcpClient_Port = port;
                string sKey = ip + "-" + port;
                if (mDicTcpClient.ContainsKey(sKey) == false)
                {
                    mDicTcpClient.Add(sKey, new TcpClient());
                    mDicTcpClient[sKey].Connect(mTcpClient_IP, mTcpClient_Port);
                }
                if (mDicObjLock.ContainsKey(sKey) == false)
                {
                    mDicObjLock.Add(sKey, new object());
                }
                mObjLock = mDicObjLock[sKey];
                mTcpClient = mDicTcpClient[sKey];
                //mTcpClient.Connect(mTcpClient_IP, mTcpClient_Port);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void IV3_TCPIP_Connect()
        {
            try
            {
                if (mTcpClient_IP != null)
                {
                    mTcpClient.Connect(mTcpClient_IP, mTcpClient_Port);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void IV3_TCPIP_Disconnect()
        {
            try
            {
                if (mTcpClient != null)
                {
                    mTcpClient.Close();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private bool IV3_TCPIP_IsConnected()
        {
            if (mTcpClient_IP != null)
            {
                return mTcpClient.Connected;
            }
            return false;
        }

        /// <summary>
        /// 設定IV3單張檢測
        /// </summary>
        public bool SetSingleInspect_IV3()
        {
            try
            {
                bool InspResult = false;
                //StartTrigResult();
                //System.Diagnostics.Stopwatch TimeCount = new System.Diagnostics.Stopwatch();
                //TimeCount.Restart();

                //while (TimeCount.ElapsedMilliseconds < 1000)
                //{
                //    List<string> ResultDat = sResult.Split(',').ToList<string>();
                //    if (PublicDeclare.bIsSoftwareSimulate
                //        && System.IO.File.Exists(istrIV3Img_Path + "\\00000.jpeg"))
                //    {
                //        FileName = istrIV3Img_Path + "\\00000.jpeg";
                //        bIsImgSaveFinish = true;
                //        dLastWriteTime = DateTime.Now;
                //        InspResult = true;
                //        break;
                //    }
                //    else
                //    {
                //        if (ResultDat.Count > 3)
                //        {
                //            string Result = ResultDat[2];
                //            if (Result.Contains("OK") || Result.Contains("NG"))
                //            {
                //                FileName = istrIV3Img_Path + "\\00000.jpeg";
                //                bIsImgSaveFinish = true;
                //                dLastWriteTime = DateTime.Now;
                //                if (Result.Contains("OK"))
                //                {
                //                    InspResult = true;
                //                }
                //                else
                //                {
                //                    InspResult = false;
                //                }
                //                break;
                //            }
                //        }
                //    }
                //    Thread.Sleep(50);
                //}
                return InspResult;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #endregion

        #region//========== Private Function (Event) ==========


        #endregion
    }
}

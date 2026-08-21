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
    public class clsCtrlHighSensor
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

        public enuSensorType eSensorType = enuSensorType.Laser;
        public enuSensorModule eSensorModule = enuSensorModule.KeyenceLaser_ComPort;
        public enuErrorCode eErrorCode = enuErrorCode.None;

        public double LastHeightValue = -99999;
        public bool bLogicInvert = false;
        public double dSoftwareOffset = 0;

        public delegate double SimulateDistanceSensor(string DeviceName);
        static public SimulateDistanceSensor g_SimulateDistanceSensor = null;
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
        public clsEnum.enuDi? eDILaser = null;
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

        public enum enuSensorType
        {
            Laser,
            Contect,
        }
        public enum enuSensorModule
        {
            ILLaserSensor_TCPIP,
            ILLaserSensor_ComPort,
            GTContactSensor,
            KeyenceLaser_ComPort,
            ILD1420,
            KeyenceLaser_TCPIP_CL3000,
            DI_Laser,
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

        public clsCtrlHighSensor(string p_sHighSensorName)
        {
            sName = p_sHighSensorName;
            if (_GetValue(enuParameterName.TimeOut_ms) == 0)
            { _SaveValue(enuParameterName.TimeOut_ms, 3000); }
            _SaveValue(enuParameterName.ByPass, 0);
            _SaveValue(enuParameterName.Simulate, 0);
        }

        ~clsCtrlHighSensor()
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

        /// <summary> Sensor越接近產品,數據越小 (mm) </summary>
        public double _GetHeightValue()
        {
            lock (m_object)
            {
                if (IsSimulatorMode == true)
                {
                    if (g_SimulateDistanceSensor != null)
                    {
                        LastHeightValue = g_SimulateDistanceSensor(this.sName);
                        return LastHeightValue += dSoftwareOffset;
                    }
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
                switch (eSensorModule)
                {
                    case enuSensorModule.ILLaserSensor_TCPIP:
                        eErrorCode = enuErrorCode.None;
                        LastHeightValue = ILLaserSensor_TCPIP_GetHeightValue();
                        break;
                    case enuSensorModule.ILLaserSensor_ComPort:
                        eErrorCode = enuErrorCode.None;
                        LastHeightValue = ILLaserSensor_ComPort_GetHeightValue();
                        break;
                    case enuSensorModule.GTContactSensor:
                        eErrorCode = enuErrorCode.None;
                        LastHeightValue = GTContactSensor_GetHeightValue();
                        break;
                    case enuSensorModule.KeyenceLaser_ComPort:
                        eErrorCode = enuErrorCode.None;
                        LastHeightValue = KeyenceLaser_ComPort_GetHeightValue();
                        break;
                    case enuSensorModule.ILD1420:
                        eErrorCode = enuErrorCode.None;
                        LastHeightValue = ILD1420Sensor_GetHeightValue();
                        if (LastHeightValue == -96 || LastHeightValue == -999)//Retry 1
                        { LastHeightValue = ILD1420Sensor_GetHeightValue(); }//@1.0.0.55-2-10@
                        break;
                    case enuSensorModule.KeyenceLaser_TCPIP_CL3000:
                        eErrorCode = enuErrorCode.None;
                        if (NowIndex > 1)
                        {
                            LastHeightValue = KeyenceLaser_TCPIP_CL3000_GetHeightValue(NowIndex);
                        }
                        else
                        {
                            LastHeightValue = KeyenceLaser_TCPIP_CL3000_GetHeightValue();
                        }
                        break;
                    case enuSensorModule.DI_Laser:
                        if (eDILaser != null)
                        {
                            LastHeightValue = clsDioCtrl.GetDi((clsEnum.enuDi)eDILaser) ? 1 : 0;
                            eErrorCode = enuErrorCode.None;
                        }
                        else
                        {
                            LastHeightValue = -999;
                        }
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
                switch (eSensorModule)
                {
                    case enuSensorModule.ILLaserSensor_TCPIP:
                        eErrorCode = enuErrorCode.None;
                        LastHeightValue = ILLaserSensor_TCPIP_GetHeightValue();
                        break;
                    case enuSensorModule.ILLaserSensor_ComPort:
                        eErrorCode = enuErrorCode.None;
                        LastHeightValue = ILLaserSensor_ComPort_GetHeightValue();
                        break;
                    case enuSensorModule.GTContactSensor:
                        eErrorCode = enuErrorCode.None;
                        LastHeightValue = GTContactSensor_GetHeightValue();
                        break;
                    case enuSensorModule.KeyenceLaser_ComPort:
                        eErrorCode = enuErrorCode.None;
                        LastHeightValue = KeyenceLaser_ComPort_GetHeightValue();
                        break;
                    case enuSensorModule.ILD1420:
                        eErrorCode = enuErrorCode.None;
                        LastHeightValue = ILD1420Sensor_GetHeightValue();
                        if (LastHeightValue == -96 || LastHeightValue == -999)//Retry 1
                        { LastHeightValue = ILD1420Sensor_GetHeightValue(); }//@1.0.0.55-2-10@
                        break;
                    case enuSensorModule.DI_Laser:
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
                switch (eSensorModule)
                {
                    case enuSensorModule.ILLaserSensor_TCPIP:
                        //ILLaserSensor_TCPIP_Disconnect();
                        ILLaserSensor_TCPIP_Connect();
                        break;
                    case enuSensorModule.ILLaserSensor_ComPort:
                        //ILLaserSensor_ComPort_Disconnect();
                        ILLaserSensor_ComPort_Connect();
                        break;
                    case enuSensorModule.GTContactSensor:
                        //GTContactSensor_Disconnect();
                        GTContactSensor_Connect();
                        break;
                    case enuSensorModule.KeyenceLaser_ComPort:
                        //KeyenceLaser_ComPort_Disconnect();
                        KeyenceLaser_ComPort_Connect();
                        break;
                    case enuSensorModule.ILD1420:
                        break;
                    case enuSensorModule.KeyenceLaser_TCPIP_CL3000:
                        //KeyenceLaser_TCPIP_CL3000_Disconnect();
                        KeyenceLaser_TCPIP_CL3000_Connect();
                        break;
                    case enuSensorModule.DI_Laser:
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
                switch (eSensorModule)
                {
                    case enuSensorModule.ILLaserSensor_TCPIP:
                        ILLaserSensor_TCPIP_Disconnect();
                        //ILLaserSensor_TCPIP_Connect();
                        break;
                    case enuSensorModule.ILLaserSensor_ComPort:
                        ILLaserSensor_ComPort_Disconnect();
                        //ILLaserSensor_ComPort_Connect();
                        break;
                    case enuSensorModule.GTContactSensor:
                        GTContactSensor_Disconnect();
                        //GTContactSensor_Connect();
                        break;
                    case enuSensorModule.KeyenceLaser_ComPort:
                        KeyenceLaser_ComPort_Disconnect();
                        //KeyenceLaser_ComPort_Connect();
                        break;
                    case enuSensorModule.ILD1420:
                        break;
                    case enuSensorModule.DI_Laser:
                        break;
                    case enuSensorModule.KeyenceLaser_TCPIP_CL3000:
                        KeyenceLaser_TCPIP_CL3000_Disconnect();
                        //KeyenceLaser_TCPIP_CL3000_Connect();
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
            switch (eSensorModule)
            {
                case enuSensorModule.ILLaserSensor_TCPIP:
                    return ILLaserSensor_TCPIP_IsConnected();
                case enuSensorModule.ILLaserSensor_ComPort:
                    return ILLaserSensor_ComPort_IsConnected();
                case enuSensorModule.GTContactSensor:
                    return GTContactSensor_IsConnected();
                case enuSensorModule.KeyenceLaser_ComPort:
                    return KeyenceLaser_ComPort_IsConnected();
                case enuSensorModule.ILD1420:
                    return true;
                case enuSensorModule.KeyenceLaser_TCPIP_CL3000:
                    return KeyenceLaser_TCPIP_CL3000_IsConnected();
                case enuSensorModule.DI_Laser:
                    return eDILaser != null;
            }
            return false;
        }


        /// <summary> 設定暫存參數，且儲存到INI [\\INI\\Device.ini] </summary>
        public void _SaveValue(enuParameterName p_ParameterName, double p_Value)
        {
            try
            {
                string sPath = ucHighSensorSetting.GetSingleton().mPmt.sINIPath;
                if (System.IO.File.Exists(sPath) == true
                    && ucHighSensorSetting.GetSingleton().mPmt.mDic_mPmtValue.ContainsKey(sName) == true
                    && Enum.IsDefined(typeof(clsPmtHighSensor.enuPmtName), p_ParameterName.ToString()) == true)
                {
                    clsPmtHighSensor.enuPmtName ePmtName = (clsPmtHighSensor.enuPmtName)Enum.Parse(typeof(clsPmtHighSensor.enuPmtName), p_ParameterName.ToString());
                    string PreviousValue = "";
                    if (ucHighSensorSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].ContainsKey(ePmtName) == true)
                    {
                        PreviousValue = ucHighSensorSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName];
                    }
                    else
                    {
                        ucHighSensorSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].Add(ePmtName, "");
                    }
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : "
                           + ", Name : " + sName
                           + ", Pmt Name : " + ePmtName.ToString()
                           + ", Change Value : " + PreviousValue + "-> " + p_Value.ToString());
                    ucHighSensorSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName] = p_Value.ToString();
                    clsIniFile mFile = new clsIniFile(sPath);
                    mFile.WriteValue(sName, ePmtName.ToString(), ucHighSensorSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName]);
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
                string sPath = ucHighSensorSetting.GetSingleton().mPmt.sINIPath;
                if (System.IO.File.Exists(sPath) == true
                    && ucHighSensorSetting.GetSingleton().mPmt.mDic_mPmtValue.ContainsKey(sName) == true
                    && Enum.IsDefined(typeof(clsPmtReader.enuPmtName), p_ParameterName.ToString()) == true)
                {
                    clsPmtHighSensor.enuPmtName ePmtName = (clsPmtHighSensor.enuPmtName)Enum.Parse(typeof(clsPmtHighSensor.enuPmtName), p_ParameterName.ToString());
                    if (ucHighSensorSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].ContainsKey(ePmtName) == false)
                    {
                        ucHighSensorSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].Add(ePmtName, "");
                    }
                    rValue = Convert.ToDouble(ucHighSensorSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName]);
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

        #region//ILLaserSensor_TCPIP (TCPIP)

        public void Initial_ILLaserSensor_TCPIP(string ip, int port)
        {
            if (IsSimulatorMode == true)
            { return; }
            try
            {
                eSensorType = enuSensorType.Laser;
                eSensorModule = enuSensorModule.ILLaserSensor_TCPIP;
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
        private double ILLaserSensor_TCPIP_GetHeightValue()
        {
            try
            {
                NetworkStream ns = mTcpClient.GetStream();
                string strCommand = "SR";
                int intStationNo = 1;
                int intFunction = 38;
                string strSendData = strCommand + "," + string.Format("{0:00}", intStationNo) + "," + string.Format("{0:000}", intFunction) + "\r\n";
                if (ns.CanWrite)
                {
                    byte[] msgByte = Encoding.UTF8.GetBytes(strSendData);
                    ns.Write(msgByte, 0, msgByte.Length);
                }
                if (!ns.CanRead)
                {
                    eErrorCode = enuErrorCode.NetworkCannotRead;
                    return -99;
                }
                byte[] receiveBytes = new byte[mTcpClient.ReceiveBufferSize];
                int numberOfBytesRead = ns.Read(receiveBytes, 0, mTcpClient.ReceiveBufferSize);
                string receiveMsg = System.Text.ASCIIEncoding.UTF8.GetString(receiveBytes, 0, numberOfBytesRead);  // 接收回傳的數值
                return Convert.ToDouble(receiveMsg.Split(',')[3]) * (bLogicInvert ? -1 : 1);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                eErrorCode = enuErrorCode.CatchError;
                return -99;
            }
        }
        private bool ILLaserSensor_TCPIP_IsConnected()
        {
            if (mTcpClient_IP != null)
            {
                return mTcpClient.Connected;
            }
            return false;
        }
        private void ILLaserSensor_TCPIP_Connect()
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
        private void ILLaserSensor_TCPIP_Disconnect()
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

        #endregion

        #region//ILLaserSensor_ComPort (ComPort)

        public void Initial_ILLaserSensor_ComPort(string strPortNumber, int BaudRate, int DataBits, int StationNo, int Timeout)
        {
            if (IsSimulatorMode == true)
            { return; }
            try
            {
                eSensorType = enuSensorType.Laser;
                eSensorModule = enuSensorModule.ILLaserSensor_ComPort;
                if (mSerialPort == null)
                {
                    if (mDic_SerialPort.Keys.Contains(strPortNumber) == false)
                    {
                        mDic_SerialPort.Add(strPortNumber, new SerialPort());
                        mSerialPort_strReciveData.Add(strPortNumber, new Dictionary<string, string>());
                        mDic_SerialPort[strPortNumber].DataReceived += new SerialDataReceivedEventHandler(mSerialPort_DataReceived);
                    }
                    mSerialPort = mDic_SerialPort[strPortNumber];
                }
                if (mSerialPort.IsOpen == false)
                {
                    mSerialPort.BaudRate = BaudRate;
                    mSerialPort.DataBits = DataBits;
                    mSerialPort.PortName = strPortNumber;
                    mSerialPort.ReadTimeout = Timeout;
                    mSerialPort.WriteTimeout = Timeout;
                }
                mSerialPort_StationNo = StationNo;
                mSerialPort_TimeOut = Timeout;
                ILLaserSensor_ComPort_Connect();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private double ILLaserSensor_ComPort_GetHeightValue()
        {
            string strCommand = "SR";

            int intStationNo = mSerialPort_StationNo;

            int intFunction = 38;

            string strSendData = strCommand + "," + string.Format("{0:00}", intStationNo) + "," + string.Format("{0:000}", intFunction) + "\r\n";


            _SerialPort_strReciveData = "";
            NowIndex = mSerialPort_StationNo;
            mSerialPort.WriteLine(strSendData);

            mSerialPort_TimeCount.Restart();

            mSerialPort_IsConnected = true;
            while (_SerialPort_strReciveData == "" || _SerialPort_strReciveData == null)
            {
                if (mSerialPort_TimeCount.ElapsedMilliseconds > mSerialPort_TimeOut)
                {
                    mSerialPort_IsConnected = false;
                    eErrorCode = enuErrorCode.RespondTimeout;
                    break;
                }
                System.Threading.Thread.Sleep(1);
            }

            string strReciveData = _SerialPort_strReciveData;

            double dblReturnValue = 0;
            try
            {
                strReciveData = strReciveData.Replace("\r", "");
                dblReturnValue = Convert.ToDouble(strReciveData.Split(',')[3]) * (bLogicInvert ? -1 : 1);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                eErrorCode = enuErrorCode.CatchError;
                dblReturnValue = -99.1;
            }
            _SerialPort_strReciveData = "";
            strReciveData = "";

            return dblReturnValue;
        }

        //===== 通訊 =====
        private enum ILCommand
        {
            IncreeaseDecreaseDir = 130,
        }
        private enum WriteRead_ILLaserSensor
        {
            SW,
            SR,

        }
        private string ILLaserSensor_ComPort_GetSetValue(ILCommand pGT2Command, WriteRead_ILLaserSensor pWriteRead, params string[] para)
        {

            string strCommand = pWriteRead.ToString();
            string intStationNo = string.Format("{0:00}", mSerialPort_StationNo);
            string intFunction = string.Format("{0:000}", (int)pGT2Command);
            string strSendData = strCommand + "," + intStationNo + "," + intFunction + "," + para[0] + "\r\n";
            _SerialPort_strReciveData = "";
            NowIndex = mSerialPort_StationNo;
            mSerialPort.WriteLine(strSendData);
            mSerialPort_TimeCount.Restart();
            while (_SerialPort_strReciveData == "" || mSerialPort_strReciveData == null)
            {
                if (mSerialPort_TimeCount.ElapsedMilliseconds > mSerialPort_TimeOut)
                {
                    mSerialPort_IsConnected = false;
                    eErrorCode = enuErrorCode.RespondTimeout;
                    break;
                }
                System.Threading.Thread.Sleep(1);
            }
            string strReciveData = _SerialPort_strReciveData;
            return strReciveData;
        }
        private bool ILLaserSensor_ComPort_IsConnected()
        {
            return mSerialPort_IsConnected;
        }
        private void ILLaserSensor_ComPort_Connect()
        {
            try
            {
                mSerialPort_IsConnected = false;
                if (mSerialPort != null)
                {
                    var availablePorts = System.IO.Ports.SerialPort.GetPortNames();
                    if (availablePorts.Contains(mSerialPort.PortName) == true)
                    {
                        if (mSerialPort.IsOpen == false)
                        {
                            mSerialPort.Open();
                        }
                        if (mSerialPort.IsOpen != true)
                        {
                            mSerialPort_IsConnected = false;
                        }
                        string[] tStr = new string[7];
                        tStr[0] = ILLaserSensor_ComPort_GetSetValue(ILCommand.IncreeaseDecreaseDir, WriteRead_ILLaserSensor.SW, "1");//j設定測高數值增量方向
                        mSerialPort_IsConnected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnected = false;
                clsArtSystem.CatchLog(ex);
            }
        }
        private void ILLaserSensor_ComPort_Disconnect()
        {
            try
            {
                if (mSerialPort != null)
                {
                    mSerialPort.Close();
                }
                mSerialPort_IsConnected = false;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnected = false;
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region//GTContactSensor (ComPort)

        public void Initial_GTContactSensor(string strPortNumber, int BaudRate, int DataBits, int StationNo, int Timeout)
        {
            if (IsSimulatorMode == true)
            { return; }
            try
            {
                eSensorType = enuSensorType.Contect;
                eSensorModule = enuSensorModule.GTContactSensor;

                if (mSerialPort == null)
                {
                    if (mDic_SerialPort.Keys.Contains(strPortNumber) == false)
                    {
                        mDic_SerialPort.Add(strPortNumber, new SerialPort());
                        mSerialPort_strReciveData.Add(strPortNumber, new Dictionary<string, string>());
                        mDic_SerialPort[strPortNumber].DataReceived += new SerialDataReceivedEventHandler(mSerialPort_DataReceived);
                    }
                    mSerialPort = mDic_SerialPort[strPortNumber];
                }
                if (mSerialPort.IsOpen == false)
                {
                    mSerialPort.BaudRate = BaudRate;
                    mSerialPort.DataBits = DataBits;
                    mSerialPort.PortName = strPortNumber;
                    mSerialPort.ReadTimeout = Timeout;
                    mSerialPort.WriteTimeout = Timeout;
                }
                mSerialPort_StationNo = StationNo;
                mSerialPort_TimeOut = Timeout;

                GTContactSensor_Connect();

            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private double GTContactSensor_GetHeightValue()
        {
            return GTContactSensor_GetValue(GT2Command.RValue);
        }

        //===== 通訊 =====
        public enum GT2Command
        {
            // 對比值
            PValue = 0,

            // 計算顯示值
            CalculatedisplayedValue,

            // 原始值
            RValue,

            ///峰值
            PeakValue,

            // 谷值
            ValleyValue,

            // 各點IO狀況
            IOStatus,

            ///確認放大器錯誤編號
            GetErrorCode,

            ///50以前只可以讀取數值
            ///50以後才可以讀取數值寫入數值



            //執行請求
            ExcuteRequest = 50,

            //通道切換狀態 (0,1,2,3)
            ChangeChannel,

            // 定時狀態
            TimingStatus,

            //重置請求
            Reset,

            //初始重置請求
            InitReset,

            //錯誤清除
            Alarmreset,

            //鍵鎖
            Keylock,

            //顯示模式
            DisplayMode,

            SetHH_0,
            SetHIGH_0,
            SetLOW_0,
            SetLL_0,
            SetDefault_0,

            SetHH_1,
            SetHIGH_1,
            SetLOW_1,
            SetLL_1,
            SetDefault_1,

            SetHH_2,
            SetHIGH_2,
            SetLOW_2,
            SetLL_2,
            SetDefault_2,

            SetHH_3,
            SetHIGH_3,
            SetLOW_3,
            SetLL_3,
            SetDefault_3,

            ResponseTime = 103,
            IncreeaseDecreaseDir = 110,


        }
        /// <summary> IOStatus 指令可取回的IO狀態 </summary>
        [Flags]
        public enum GT2IOStatus
        {
            HIGH = 0,
            LOW = 1,
            ERROR = 2,
            GO = 3,
            HH = 8,
            LL = 16,
        }
        public enum WriteRead_GTContactSensor
        {
            SW,
            SR,

        }

        private bool GTContactSensor_IsConnected()
        {
            return mSerialPort_IsConnected;
        }
        private void GTContactSensor_Connect()
        {
            try
            {
                mSerialPort_IsConnected = false;
                if (mSerialPort != null)
                {
                    if (mSerialPort.IsOpen == false)
                    {
                        var availablePorts = System.IO.Ports.SerialPort.GetPortNames();
                        if (availablePorts.Contains(mSerialPort.PortName) == true)
                        {
                            mSerialPort.Open();
                            if (mSerialPort.IsOpen != true)
                            {
                                mSerialPort_IsConnected = false;
                            }
                            string[] tStr = new string[7];
                            tStr[0] = GTContactSensor_GetSetValue(GT2Command.IncreeaseDecreaseDir, WriteRead_GTContactSensor.SW, "1");//j設定測高數值增量方向
                            mSerialPort_IsConnected = true;
                        }
                    }
                    mSerialPort_IsConnected = true;
                }
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnected = false;
                clsArtSystem.CatchLog(ex);
            }
        }
        private void GTContactSensor_Disconnect()
        {
            try
            {
                mSerialPort.Close();
                mSerialPort_IsConnected = false;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnected = false;
                clsArtSystem.CatchLog(ex);
            }
        }

        public double GTContactSensor_GetValue(GT2Command pGT2Command)
        {
            if ((int)pGT2Command > 5 || IsSimulatorMode) { return 0; }


            string strReciveData = GTContactSensor_GetReturnString(pGT2Command);

            double dblReturnValue = 0;

            try
            {
                strReciveData = strReciveData.Replace("\r", "");
                dblReturnValue = Convert.ToDouble(strReciveData.Split(',')[3]) * (bLogicInvert ? -1 : 1);
            }
            catch (Exception ex)
            {
                eErrorCode = enuErrorCode.CatchError;
                dblReturnValue = -99;
                clsArtSystem.CatchLog(ex);
            }
            strReciveData = "";
            return dblReturnValue;

        }
        public GT2IOStatus GTContactSensor_GetIOstatus()
        {
            string strReciveData = GTContactSensor_GetReturnString(GT2Command.IOStatus);

            GT2IOStatus tGT2IOStatus = (GT2IOStatus)Enum.Parse(typeof(GT2IOStatus), strReciveData, false);

            return tGT2IOStatus;
        }
        private string GTContactSensor_GetReturnString(GT2Command pGT2Command)
        {
            string strCommand = "SR";

            string intStationNo = string.Format("{0:00}", mSerialPort_StationNo);

            string intFunction = string.Format("{0:000}", (int)pGT2Command);


            //+ "CRLF"
            string strSendData = strCommand + "," + intStationNo + "," + intFunction + "\r\n";


            _SerialPort_strReciveData = "";

            NowIndex = mSerialPort_StationNo;
            mSerialPort.WriteLine(strSendData);

            mSerialPort_TimeCount.Restart();

            mSerialPort_IsConnected = true;
            while (_SerialPort_strReciveData == "" || _SerialPort_strReciveData == null)
            {
                if (mSerialPort_TimeCount.ElapsedMilliseconds > mSerialPort_TimeOut)
                {
                    mSerialPort_IsConnected = false;
                    break;
                }
            }

            string strReciveData = _SerialPort_strReciveData;

            return strReciveData;
        }
        private string GTContactSensor_GetSetValue(GT2Command pGT2Command, WriteRead_GTContactSensor pWriteRead, params string[] para)
        {

            string strCommand = pWriteRead.ToString();

            string intStationNo = string.Format("{0:00}", mSerialPort_StationNo);

            string intFunction = string.Format("{0:000}", (int)pGT2Command);

            string strSendData = strCommand + "," + intStationNo + "," + intFunction + "," + para[0] + "\r\n";

            _SerialPort_strReciveData = "";

            NowIndex = mSerialPort_StationNo;
            mSerialPort.WriteLine(strSendData);

            mSerialPort_TimeCount.Restart();

            while (_SerialPort_strReciveData == "" || _SerialPort_strReciveData == null)
            {
                if (mSerialPort_TimeCount.ElapsedMilliseconds > mSerialPort_TimeOut)
                {
                    mSerialPort_IsConnected = false;
                    break;
                }
            }

            string strReciveData = _SerialPort_strReciveData;

            return strReciveData;
        }

        #endregion

        #region//KeyenceLaser_ComPort (ComPort)

        public void Initial_KeyenceLaser_ComPort(string strPortNumber, int BaudRate, int DataBits, int StationNo, int Timeout)
        {
            if (IsSimulatorMode == true)
            { return; }
            try
            {
                eSensorType = enuSensorType.Laser;
                eSensorModule = enuSensorModule.KeyenceLaser_ComPort;
                if (mSerialPort == null)
                {
                    if (mDic_SerialPort.Keys.Contains(strPortNumber) == false)
                    {
                        mDic_SerialPort.Add(strPortNumber, new SerialPort());
                        mSerialPort_strReciveData.Add(strPortNumber, new Dictionary<string, string>());
                        mDic_SerialPort[strPortNumber].DataReceived += new SerialDataReceivedEventHandler(mSerialPort_DataReceived);
                    }
                    mSerialPort = mDic_SerialPort[strPortNumber];
                }
                if (mSerialPort.IsOpen == false)
                {
                    mSerialPort.BaudRate = BaudRate;
                    mSerialPort.DataBits = DataBits;
                    mSerialPort.PortName = strPortNumber;
                    mSerialPort.ReadTimeout = Timeout;
                    mSerialPort.WriteTimeout = Timeout;
                }
                mSerialPort_StationNo = StationNo;
                mSerialPort_TimeOut = Timeout;
                KeyenceLaser_ComPort_Connect();

            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private double KeyenceLaser_ComPort_GetHeightValue()
        {
            try
            {
                string strCommand = "SR";
                int intStationNo = 0;
                int intFunction = 38;
                string strSendData = "M1" + "\r";

                _SerialPort_strReciveData = "";
                NowIndex = mSerialPort_StationNo;
                strSendData = "M" + (NowIndex + 1) + "" + "\r";
                mSerialPort.WriteLine(strSendData);
                mSerialPort_TimeCount.Restart();
                mSerialPort_IsConnected = true;
                while (_SerialPort_strReciveData == "" || _SerialPort_strReciveData == null)
                {
                    //System.Windows.Forms.Application.DoEvents();
                    if (mSerialPort_TimeCount.ElapsedMilliseconds > mSerialPort_TimeOut)
                    {
                        mSerialPort_IsConnected = false;
                        eErrorCode = enuErrorCode.RespondTimeout;
                        break;
                    }
                }
                string strReciveData = _SerialPort_strReciveData;
                double dblReturnValue = 0;

                try
                {
                    strReciveData = strReciveData.Replace("\r", "");
                    if (strReciveData.Split(',')[0] == "ER")
                    {
                        eErrorCode = enuErrorCode.RespondDataError;
                        dblReturnValue = -98;
                    }
                    else if (strReciveData == "")
                    {
                        eErrorCode = enuErrorCode.RespondDataError;
                        dblReturnValue = -99;
                    }
                    else if (strReciveData.Contains("-FFFFFFF") == true)
                    {
                        eErrorCode = enuErrorCode.RespondDataError;
                        dblReturnValue = -99.999;
                    }
                    else
                    {
                        dblReturnValue = Convert.ToDouble(strReciveData.Split(',')[1]) * (bLogicInvert ? -1 : 1);
                    }
                }
                catch (Exception ex)
                {
                    eErrorCode = enuErrorCode.CatchError;
                    dblReturnValue = -99;
                    clsArtSystem.CatchLog(ex);
                }
                _SerialPort_strReciveData = "";
                strReciveData = "";
                return dblReturnValue;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return -999;
        }

        //===== 通訊 =====
        private enum WriteRead_KeyenceLaser_ComPort
        {
            SW,
            SR,

        }
        private string KeyenceLaser_ComPort_GetSetValue(ILCommand pGT2Command, WriteRead_KeyenceLaser_ComPort pWriteRead, params string[] para)
        {

            string strCommand = pWriteRead.ToString();

            string intStationNo = string.Format("{0:00}", mSerialPort_StationNo);

            string intFunction = string.Format("{0:000}", (int)pGT2Command);

            string strSendData = strCommand + "," + intStationNo + "," + intFunction + "," + para[0] + "\r\n";


            _SerialPort_strReciveData = "";

            NowIndex = mSerialPort_StationNo;
            mSerialPort.WriteLine(strSendData);

            mSerialPort_TimeCount.Restart();

            while (_SerialPort_strReciveData == "" || _SerialPort_strReciveData == null)
            {
                if (mSerialPort_TimeCount.ElapsedMilliseconds > mSerialPort_TimeOut)
                {
                    mSerialPort_IsConnected = false;
                    break;
                }
            }
            //  System.Threading.Thread.Sleep(2000);

            string strReciveData = _SerialPort_strReciveData;

            return strReciveData;
        }
        private bool KeyenceLaser_ComPort_IsConnected()
        {
            return mSerialPort_IsConnected;
        }
        private void KeyenceLaser_ComPort_Connect()
        {
            try
            {
                mSerialPort_IsConnected = false;
                if (mSerialPort != null)
                {
                    var availablePorts = System.IO.Ports.SerialPort.GetPortNames();
                    if (availablePorts.Contains(mSerialPort.PortName) == true)
                    {
                        mSerialPort.Open();
                        if (mSerialPort.IsOpen != true)
                        {
                            mSerialPort_IsConnected = false;
                        }
                        //string[] tStr = new string[7];
                        //tStr[0] = KeyenceLaser_ComPort_GetSetValue(ILCommand.IncreeaseDecreaseDir, WriteRead_KeyenceLaser_ComPort.SW, "1");//j設定測高數值增量方向
                        mSerialPort_IsConnected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnected = false;
                clsArtSystem.CatchLog(ex);
            }
        }
        private void KeyenceLaser_ComPort_Disconnect()
        {
            try
            {
                mSerialPort.Close();
                mSerialPort_IsConnected = false;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnected = false;
                clsArtSystem.CatchLog(ex);
            }
        }


        #endregion

        #region//ILD1420Sensor (ComPort)

        public void Initial_ILD1420Sensor(string strPortNumber)
        {
            if (IsSimulatorMode == true)
            { return; }
            try
            {
                if (mILD1420 == null)
                {
                    mILD1420 = new MEDAQLib(ME_SENSOR.SENSOR_ILD1420);
                }
                eSensorType = enuSensorType.Laser;
                eSensorModule = enuSensorModule.ILD1420;
                if (mSerialPort == null)
                { mSerialPort = new SerialPort(); }
                mSerialPort.PortName = strPortNumber;
                mILD1420.OpenSensorRS232(strPortNumber);

            }
            catch (Exception ex)
            {
                mSerialPort_IsConnected = false;
                clsArtSystem.CatchLog(ex);
            }
        }
        public void Close_ILD1420Sensor()
        {
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

        private double ILD1420Sensor_GetHeightValue()
        {
            //Open();
            //System.Threading.Thread.Sleep(50); // Wait for new data
            double dblReturnValue = 0;
            int[] rawData = { 0 };
            double[] scaledData = { 0 };

            int read = 0;
            double time = 0;

            ERR_CODE ReturnCode = mILD1420.Poll(rawData, scaledData, 1);
            //ERR_CODE ReturnCode = mILD1420.TransferData(rawData, scaledData, 1, ref read);
            //ERR_CODE ReturnCode = mILD1420.TransferDataTS(rawData, scaledData, 1, ref read, ref time);

            if (ReturnCode == ERR_CODE.ERR_NO_SENSORDATA_AVAILABLE)
            {
                //ReturnCode = mILD1420.ExecSCmd("Reset_Boot");
                //ReturnCode = mILD1420.ExecSCmd("Reset_Boot");
                string Error = "";
                mILD1420.GetError(ref Error);
                //mILD1420.CloseSensor();
                //mILD1420.OpenSensorRS232(mSerialPort.PortName);

                eErrorCode = enuErrorCode.NetworkCannotRead;
                return -98;
            }
            else if (ReturnCode == ERR_CODE.ERR_NOT_OPEN)
            {
                mILD1420.OpenSensorRS232(mSerialPort.PortName);
                eErrorCode = enuErrorCode.NetworkCannotRead;
                return -97;
            }
            else if (ReturnCode != ERR_CODE.ERR_WARNING &&
                ReturnCode != ERR_CODE.ERR_NOERROR)
            {
                eErrorCode = enuErrorCode.RespondDataError;
                return -96;
                //dblReturnValue = ILD1420Sensor_GetHeightValue();
                //if (dblReturnValue == -96)
                //{
                //}
                //else
                //{
                //    return dblReturnValue;
                //}
            }

            //string strMsg = string.Format("\rRaw: {0} / Scaled: {1,4:F}", rawData[0], scaledData[0]);

            dblReturnValue = scaledData[0] * (bLogicInvert ? -1 : 1);
            ReturnCode = mILD1420.ExecSCmd("Clear_Buffers");
            if (Math.Abs(dblReturnValue) > 999)
            {
                eErrorCode = enuErrorCode.RespondDataError;
                dblReturnValue = -999;
            }
            return dblReturnValue;
        }

        #endregion

        #region//KeyenceLaser_TCPIP_CL3000 (TCPIP)

        public void Initial_KeyenceLaser_TCPIP_CL3000(string ip, int port, int StationNo)
        {
            if (IsSimulatorMode == true)
            { return; }
            try
            {
                eSensorType = enuSensorType.Laser;
                eSensorModule = enuSensorModule.KeyenceLaser_TCPIP_CL3000;
                mTcpClient_IP = IPAddress.Parse(ip);
                mTcpClient_Port = port;

                mSerialPort_StationNo = StationNo;
                string sKey = ip + "-" + port;
                if (mDicTcpClient.ContainsKey(sKey) == false)
                {
                    mDicTcpClient.Add(sKey, new TcpClient());
                    mTcpClient.Connect(mTcpClient_IP, mTcpClient_Port);
                }
                if (mDicObjLock.ContainsKey(sKey) == false)
                { 
                    mDicObjLock.Add(sKey, new object());
                }
                mObjLock = mDicObjLock[sKey];
                mTcpClient = mDicTcpClient[sKey];

            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private double KeyenceLaser_TCPIP_CL3000_GetHeightValue(int iIndex = 1)
        {
            try
            {
                double rValue = -99;
                if (mObjLock == null)
                {
                    clsLog.Log(clsArtSystem.g_strCatchLogName, "clsCtrlHighSensor : ObjLock = null");
                    eErrorCode = enuErrorCode.CatchError;
                    return -99;
                }
                lock (mObjLock)
                {
                    NetworkStream ns = mTcpClient.GetStream();
                    string strCommand = "MS";
                    string strSendData = strCommand + ",1," + iIndex.ToString() + "\r";
                    if (ns.CanWrite)
                    {
                        byte[] msgByte = Encoding.UTF8.GetBytes(strSendData);
                        ns.Write(msgByte, 0, msgByte.Length);
                    }
                    if (!ns.CanRead)
                    {
                        eErrorCode = enuErrorCode.NetworkCannotRead;
                        return -99;
                    }
                    byte[] receiveBytes = new byte[mTcpClient.ReceiveBufferSize];
                    int numberOfBytesRead = ns.Read(receiveBytes, 0, mTcpClient.ReceiveBufferSize);
                    string receiveMsg = System.Text.ASCIIEncoding.UTF8.GetString(receiveBytes, 0, numberOfBytesRead);  // 接收回傳的數值
                    rValue = Convert.ToDouble(receiveMsg.Split(',')[1]);
                    if (rValue <= -99)
                    {
                        eErrorCode = enuErrorCode.RespondDataError;
                        return -99.999;
                    }
                }
                return rValue * (bLogicInvert ? -1 : 1);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                eErrorCode = enuErrorCode.CatchError;
                return -99;
            }
        }
        private bool KeyenceLaser_TCPIP_CL3000_IsConnected()
        {
            return mTcpClient.Connected;
        }
        private void KeyenceLaser_TCPIP_CL3000_Connect()
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
        private void KeyenceLaser_TCPIP_CL3000_Disconnect()
        {
            try
            {
                mTcpClient.Close();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }


        #endregion

        #endregion

        #region//========== Private Function (Event) ==========

        private void mSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (eSensorModule == enuSensorModule.KeyenceLaser_ComPort)
                {
                    System.Threading.Thread.Sleep(50);
                    if (mSerialPort_strReciveData.Keys.Contains(mSerialPort.PortName) == true)
                    {
                        _SerialPort_strReciveData = mSerialPort.ReadExisting();
                    }
                }
                else
                {
                    if (mSerialPort_strReciveData.Keys.Contains(mSerialPort.PortName) == true)
                    {
                        _SerialPort_strReciveData = mSerialPort.ReadLine();
                    }
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

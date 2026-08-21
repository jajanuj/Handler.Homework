using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtCommunication;
using System.IO.Ports;
using System.Threading;
using Microsoft.Office.Interop.Excel;

namespace ArtSystem.MultiSystem
{
    public class clsCtrlReader
    {
        #region //===================== 區域變數設置 =====================

        public string sName
        {
            get;
            private set;
        }

        private enuReaderType _eReaderType = enuReaderType.Keyence2D;
        public enuReaderType eReaderType
        {
            get
            {
                return _eReaderType;
            }
            private set
            {
                _eReaderType = value;
            }
        }

        public bool IsSimulatorMode { get; set; }
        /// <summary> null 還沒回應, true 成功, false 失敗 </summary>
        public bool? bDataAck { get; private set; }
        /// <summary> Barcode結果 </summary>
        public string sBarcodeResult { get; private set; }
        public int iChannelID = 0;
        private bool bRFIDRetry = false;
        private int iRFIDRetryCnt = 0;

        #endregion

        #region //===================== 通訊模組 =====================

        static public Dictionary<string, RFID_R420_impinj> mRFID_R420_impinj = new Dictionary<string, RFID_R420_impinj>();
        public SocketClient mClientDevice = null;
        private string mTCP_IP = "192.168.100.100";
        private int mTCP_Port = 9004;
        System.IO.Ports.SerialPort m_SerialPort = new System.IO.Ports.SerialPort();

        #endregion

        #region //===================== 列舉 =====================

        public enum enuReaderType
        {
            Keyence1D,
            Keyence2D,
            RFID_R420_impinj,
            RFID_IFM_RS232,
        }

        public enum enuCmd
        {
            SendRead,
            SendStop,
            SendClear,
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

        public clsCtrlReader(string Name, enuReaderType eType, string sTCPIP, int iPort, bool Simalute)
        {
            sName = Name;
            eReaderType = eType;
            IsSimulatorMode = Simalute;
            mTCP_IP = sTCPIP;
            mTCP_Port = iPort;
            if (eType == enuReaderType.RFID_R420_impinj)
            {
                if (mRFID_R420_impinj.ContainsKey(sTCPIP) == false)
                { mRFID_R420_impinj.Add(sTCPIP, new RFID_R420_impinj()); }
                if (IsSimulatorMode == false)
                { mRFID_R420_impinj[sTCPIP].Connect(sTCPIP); }
            }
            else if (eType == enuReaderType.RFID_IFM_RS232)
            {
                this.m_SerialPort.PortName = sTCPIP;
                this.m_SerialPort.BaudRate = iPort;
                this.m_SerialPort.ReadTimeout = 1000;
                this.m_SerialPort.WriteTimeout = 1000;
                this.m_SerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
                this._Connect();
            }
            else
            {
                if (mClientDevice == null)
                {
                    mClientDevice = new SocketClient(enumClientType.SocketBGW, enumStringCode.UPF8);
                    mClientDevice.DataArrivalEvent2 += new SocketClient.DataArrivalEventHandler2(mClientDevice_MessageArrivelEvent);
                    this._Connect();
                }
                //mArtLink._MessageArrivelEvent += new ucArtLink.DataArrivalHandler(mArtLink__MessageArrivelEvent);
            }
            if (_GetValue(enuParameterName.TimeOut_ms) == 0)
            { _SaveValue(enuParameterName.TimeOut_ms, 3000); }
            _SaveValue(enuParameterName.ByPass, 0);
            _SaveValue(enuParameterName.Simulate, 0);
        }

        #endregion

        #region //===================== Event Define =====================

        /// <summary> TCPIP Message Arrivel 事件</summary>
        private event DataArrivalHandler m_MessageArrivelEvent = null;
        public delegate void DataArrivalHandler(string sSenderName, string ArrivelMessage, bool IsSend);
        /// <summary> 接收到訊息事件 </summary>
        public event DataArrivalHandler _MessageArrivelEvent
        {
            remove
            {
                m_MessageArrivelEvent -= value;
            }
            add
            {
                m_MessageArrivelEvent += value;
            }
        }

        #endregion

        #region//===================== Public函式 =====================

        public void _Connect()
        {
            try
            {
                switch (_eReaderType)
                {
                    case enuReaderType.Keyence1D:
                    case enuReaderType.Keyence2D:
                        if (mClientDevice.IsConnected == true)
                        {
                             mClientDevice.Disconnect();
                        }
                        mClientDevice.Connect(IPAddress.Parse(mTCP_IP), mTCP_Port);
                        break;
                    case enuReaderType.RFID_R420_impinj:
                        mRFID_R420_impinj[mTCP_IP].Connect(mRFID_R420_impinj[mTCP_IP].READER_HOSTNAME);
                        break;
                    case enuReaderType.RFID_IFM_RS232:
                        if (this.m_SerialPort.IsOpen == false
                            && this.m_SerialPort.PortName != "" && this.m_SerialPort.PortName != null)
                        {
                            this.m_SerialPort.PortName = mTCP_IP;
                            this.m_SerialPort.BaudRate = mTCP_Port;
                            if (IsSimulatorMode == false )
                            {
                                this.m_SerialPort.Open();
                            }

                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                if ((clsArtSystem.bIsSoftwareSimulate == true
                    && _eReaderType == enuReaderType.RFID_IFM_RS232) == false)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }
        }
        public bool _IsConnected()
        {
            bool rValue = false;
            try
            {
                switch (_eReaderType)
                {
                    case enuReaderType.Keyence1D:
                    case enuReaderType.Keyence2D:
                        rValue = mClientDevice.IsConnected;
                        break;
                    case enuReaderType.RFID_R420_impinj:
                        rValue = mRFID_R420_impinj[mTCP_IP].IsConnected();
                        break;
                    case enuReaderType.RFID_IFM_RS232:
                        rValue = m_SerialPort.IsOpen;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        public void _Disconnect()
        {
            try
            {
                switch (_eReaderType)
                {
                    case enuReaderType.Keyence1D:
                    case enuReaderType.Keyence2D:
                        mClientDevice.Disconnect();
                        break;
                    case enuReaderType.RFID_R420_impinj:
                        mRFID_R420_impinj[mTCP_IP].Disconnect();
                        break;
                    case enuReaderType.RFID_IFM_RS232:
                        m_SerialPort.Close();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void _SendRead()
        {
            try
            {
                sBarcodeResult = "";
                bDataAck = null;
                if (_IsConnected() == false)
                { return; }

                switch (_eReaderType)
                {
                    case enuReaderType.Keyence1D:
                    case enuReaderType.Keyence2D:
                        {
                            string strData = "LON\r";
                            mClientDevice.Send(strData);
                        }
                        break;
                    case enuReaderType.RFID_R420_impinj:
                        Dictionary<string, DateTime> temp = mRFID_R420_impinj[mTCP_IP].GetTagData();
                        foreach (string sKey in temp.Keys)
                        {
                            if (sKey.Contains(1 + ";") == true)
                            {
                                sBarcodeResult = ASCII_ConvertString_HexToDec(sKey.Replace(1 + ";", ""));
                                if (m_MessageArrivelEvent != null)
                                {
                                    m_MessageArrivelEvent(sName, sBarcodeResult, false);
                                }
                                break;
                            }
                        }
                        bDataAck = true;
                        break;
                    case enuReaderType.RFID_IFM_RS232:
                        if (IsSimulatorMode == false)
                        {
                            if (m_SerialPort.IsOpen == true)
                            {
                                bDataAck = false;
                                bRFIDRetry = false;
                                sBarcodeResult = "";
                                m_SerialPort.Write("~00 RMID*");
                            }
                        }
                        else
                        {
                            bRFIDRetry = false;
                            sBarcodeResult = DateTime.Now.ToString();
                            bDataAck = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void _SendStop()
        {
            try
            {
                bDataAck = null;
                if (_IsConnected() == false)
                { return; }
                switch (_eReaderType)
                {
                    case enuReaderType.Keyence1D:
                    case enuReaderType.Keyence2D:
                        {
                            string strData = "LOFF\r";
                            mClientDevice.Send(strData);
                        }
                        break;
                    case enuReaderType.RFID_R420_impinj:
                        break;
                    case enuReaderType.RFID_IFM_RS232:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void _SendReset()
        {
            try
            {
                sBarcodeResult = "";
                bDataAck = null;
                if (_IsConnected() == false)
                { return; }
                switch (_eReaderType)
                {
                    case enuReaderType.Keyence1D:
                    case enuReaderType.Keyence2D:
                        {
                            string strData = "BCLR\r";
                            mClientDevice.Send(strData);
                        }
                        break;
                    case enuReaderType.RFID_R420_impinj:
                        break;
                    case enuReaderType.RFID_IFM_RS232:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public bool? _ReadAck()
        {
            bool? rValue = bDataAck;
            try
            {
                switch (_eReaderType)
                {
                    case enuReaderType.Keyence1D:
                    case enuReaderType.Keyence2D:
                        break;
                    case enuReaderType.RFID_R420_impinj:
                        break;
                    case enuReaderType.RFID_IFM_RS232:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        public string _ReadResult()
        {
            try
            {
                switch (_eReaderType)
                {
                    case enuReaderType.Keyence1D:
                    case enuReaderType.Keyence2D:
                        break;
                    case enuReaderType.RFID_R420_impinj://@1.0.0.55-7@
                        sBarcodeResult = "";
                        Dictionary<string, DateTime> temp = mRFID_R420_impinj[mTCP_IP].GetTagData();
                        foreach (string sKey in temp.Keys)
                        {
                            if (sKey.Contains(iChannelID + ";") == true)
                            {
                                sBarcodeResult = ASCII_ConvertString_HexToDec(sKey.Replace(iChannelID + ";", ""));
                                bDataAck = true;
                                break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return sBarcodeResult;
        }


        /// <summary> 設定暫存參數，且儲存到INI [\\INI\\Device.ini] </summary>
        public void _SaveValue(enuParameterName p_ParameterName, double p_Value)
        {
            try
            {
                string sPath = ucReaderSetting.GetSingleton().mPmt.sINIPath;
                if (System.IO.File.Exists(sPath) == true
                    && ucReaderSetting.GetSingleton().mPmt.mDic_mPmtValue.ContainsKey(sName) == true
                    && Enum.IsDefined(typeof(clsPmtReader.enuPmtName), p_ParameterName.ToString()) == true)
                {
                    clsPmtReader.enuPmtName ePmtName = (clsPmtReader.enuPmtName)Enum.Parse(typeof(clsPmtReader.enuPmtName), p_ParameterName.ToString());
                    string PreviousValue = "";
                    if (ucReaderSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].ContainsKey(ePmtName) == true)
                    {
                        PreviousValue = ucReaderSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName];
                    }
                    else
                    {
                        ucReaderSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].Add(ePmtName, "");
                    }
                    clsLog.Log(clsCmData.enuLogType.SystemLog, clsCmData.g_strNowUser + " : "
                           + ", TCPLink Name : " + sName
                           + ", Pmt Name : " + ePmtName.ToString()
                           + ", Change Value : " + PreviousValue + "-> " + p_Value.ToString());
                    ucReaderSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName] = p_Value.ToString();
                    clsIniFile mFile = new clsIniFile(sPath);
                    mFile.WriteValue(sName, ePmtName.ToString(), ucReaderSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName]);
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
                string sPath = ucReaderSetting.GetSingleton().mPmt.sINIPath;
                if (System.IO.File.Exists(sPath) == true
                    && ucReaderSetting.GetSingleton().mPmt.mDic_mPmtValue.ContainsKey(sName) == true
                    && Enum.IsDefined(typeof(clsPmtReader.enuPmtName), p_ParameterName.ToString()) == true)
                {
                    clsPmtReader.enuPmtName ePmtName = (clsPmtReader.enuPmtName)Enum.Parse(typeof(clsPmtReader.enuPmtName), p_ParameterName.ToString());
                    if (ucReaderSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].ContainsKey(ePmtName) == false)
                    {
                        ucReaderSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].Add(ePmtName, "");
                    }
                    rValue = Convert.ToDouble(ucReaderSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName]);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        #endregion

        #region //===================== private Function =====================

        private string ASCII_ConvertString_HexToDec(string strSource)
        {
            string[] a = strSource.Split(' ');
            int charIdxCount = 0;
            int stringCount = 2;
            var temparray = new List<string>();
            while (strSource.Length > charIdxCount)
            {
                var temp = strSource.Substring(charIdxCount, Math.Min(stringCount, strSource.Length - charIdxCount));
                temparray.Add(temp);
                charIdxCount += stringCount;

            }
            string[] SplitResult = temparray.ToArray();
            string strConvertResult = "";

            foreach (string hex in SplitResult)
            {
                int value = Convert.ToInt32(hex, 16);
                string stringValue = Char.ConvertFromUtf32(value);
                char charValue = (char)value;
                if (charValue != '\0')
                {
                    strConvertResult += charValue;
                }

            }
            return strConvertResult;
        }

        #endregion

        #region //===================== 事件 (通訊) =====================

        private void mClientDevice_MessageArrivelEvent(object sender, SocketClientEventArgs e, string ReceiveData)
        {
            switch (_eReaderType)
            {
                case enuReaderType.Keyence1D:
                case enuReaderType.Keyence2D:
                    sBarcodeResult = ReceiveData.Replace("\r", "");
                    bDataAck = true;
                    if (m_MessageArrivelEvent != null)
                    {  m_MessageArrivelEvent(sName, sBarcodeResult, false); }
                    break;
                case enuReaderType.RFID_R420_impinj:
                    break;
                case enuReaderType.RFID_IFM_RS232:
                    break;
                default:
                    break;
            }
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                switch (_eReaderType)
                {
                    case enuReaderType.Keyence1D:
                        break;
                    case enuReaderType.Keyence2D:
                        break;
                    case enuReaderType.RFID_R420_impinj:
                        break;
                    case enuReaderType.RFID_IFM_RS232:
                        #region//RFID_IFM_RS232
                        if (m_SerialPort.BytesToRead > 0)
                        {
                            string strReceiveData = string.Empty;
                            int ReadCount = 0;
                            byte[] InByte = new byte[m_SerialPort.BytesToRead];
                            ReadCount = m_SerialPort.Read(InByte, 0, m_SerialPort.BytesToRead);

                            if (ReadCount == 0) { return; }
                            else
                            {
                                strReceiveData = Encoding.ASCII.GetString(InByte);
                                Thread.Sleep(50);
                                if (strReceiveData.Contains("~"))
                                {
                                    sBarcodeResult = strReceiveData;
                                }
                                else if (strReceiveData.Contains("*"))
                                {
                                    sBarcodeResult += strReceiveData;
                                }

                                if (sBarcodeResult.Contains("~") &&
                                    sBarcodeResult.Contains("*"))
                                {
                                    sBarcodeResult = sBarcodeResult.Replace("~", "");
                                    sBarcodeResult = sBarcodeResult.Replace("*", "");

                                    string[] strResponse = sBarcodeResult.Split(' ');

                                    if (strResponse[1] == "RUR") //Are you there 
                                    {
                                    }
                                    else if (strResponse[1] == "RMIDR") //Read MID
                                    {
                                        if (strResponse[strResponse.Length - 1] == "TE") //Tag Error
                                        {
                                            if (bRFIDRetry && iRFIDRetryCnt < 3)
                                            {
                                                Thread.Sleep(1000);
                                                m_SerialPort.Write("~00 RMID*");    //retry send Read
                                                iRFIDRetryCnt++;
                                                return;
                                            }
                                            else
                                            {
                                                sBarcodeResult = "TE";
                                            }
                                        }
                                        else
                                        {
                                            sBarcodeResult = strResponse[strResponse.Length - 1];
                                        }
                                        //RightPortMagazineID = m_strRightPortTagID;
                                        bDataAck = true;
                                        bRFIDRetry = false;
                                    }
                                }
                            }
                        }
                        break;
                    #endregion
                    default:
                        break;
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

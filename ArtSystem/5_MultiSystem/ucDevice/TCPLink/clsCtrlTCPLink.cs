using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtCommunication;
using ArtData;
using System.Drawing;
using System.IO.Ports;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace ArtSystem.MultiSystem
{
    public class clsCtrlTCPLink
    {
        #region //=====================  區域變數設置 =====================

        /// <summary> 參數Index </summary>
        public string sName = "";

        /// <summary> 讀取傳遞訊息 </summary>
        public string _ArrivalMessageLatest
        {
            get;
            private set;
        }

        private object m_objLockArrived = new object();
        private object m_objLockSend = new object();
        /// <summary> 讀取傳遞訊息 </summary>
        public bool _bIsServer
        {
            get;
            private set;
        }
        public bool g_bServerSendToAllClient = false;

        #endregion

        #region //=====================  委派事件 =====================
        /// <summary> TCPIP Message Arrivel 事件</summary>
        private event DataArrivalHandler m_MessageArrivelEvent;
        public delegate void DataArrivalHandler(object sender, object p_TCPIPControl, EventArgs e, string ArrivelMessage);

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

        private event DataArrivalHandler m_JSEvent;
        public event DataArrivalHandler _JSEvent
        {
            remove
            {
                m_JSEvent -= value;
            }
            add
            {
                m_JSEvent += value;
            }
        }

        /// <summary> Client 連線成功事件 </summary>
        private event SocketClient.ConnectedEventHandler m_ClientConnectHandler;
        /// <summary> Client 連線成功事件 </summary>
        public event SocketClient.ConnectedEventHandler _ClientConnect
        {
            remove
            {
                m_ClientConnectHandler -= value;
            }
            add
            {
                m_ClientConnectHandler += value;
            }
        }

        /// <summary> Client 連線失敗事件 </summary>
        private event SocketClient.ConnectFailEventHandler m_ClientConnectFailHandler;
        /// <summary> Client 連線失敗事件 </summary>
        public event SocketClient.ConnectFailEventHandler _ClientConnectFail
        {
            remove
            {
                m_ClientConnectFailHandler -= value;
            }
            add
            {
                m_ClientConnectFailHandler += value;
            }
        }

        /// <summary> Client 連線離線事件 </summary>
        private event SocketClient.DisconnectEventHandler m_ClientDisonnectHandler;
        /// <summary> Client 連線離線事件 </summary>
        public event SocketClient.DisconnectEventHandler _ClientDisonnect
        {
            remove
            {
                m_ClientDisonnectHandler -= value;
            }
            add
            {
                m_ClientDisonnectHandler += value;
            }
        }

        /// <summary> Server端 有Client連線事件 </summary>
        private event SocketServer.ClientConnectedEventHandler m_ServerGetClientHandler;
        /// <summary> Server端 有Client連線事件 </summary>
        public event SocketServer.ClientConnectedEventHandler _ServerGetClient
        {
            remove
            {
                m_ServerGetClientHandler -= value;
            }
            add
            {
                m_ServerGetClientHandler += value;
            }
        }

        /// <summary> Server端 有Client離線事件 </summary>
        private event SocketServer.ClientDisconnectEventHandler m_ServerLostClientHandler;
        /// <summary> Server端 有Client離線事件 </summary>
        public event SocketServer.ClientDisconnectEventHandler _ServerLostClient
        {
            remove
            {
                m_ServerLostClientHandler -= value;
            }
            add
            {
                m_ServerLostClientHandler += value;
            }
        }


        #endregion

        #region //=====================  通訊模組 =====================

        private int m_iReceiveBufferSize = 8192;

        /// <summary> 通訊屬性List </summary>
        public SocketServer m_ServerDevice;
        /// <summary> 通訊屬性List </summary>
        public SocketClient m_ClientDevice;

        string sTCP_IP = "127.0.0.1";
        int iTCP_Port = 9004;

        private enumClientType m_ClientType = enumClientType.SocketBGW;
        private enumServerType m_ServerType = enumServerType.Socket;
        private enumStringCode m_ClientStringCode = enumStringCode.UPF8;
        private enumStringCode m_ServerStringCode = enumStringCode.UPF8;
        private enumServerThreadType m_ServerThreadType = enumServerThreadType.BGWorker;

        #endregion

        #region//===================== Enum =====================

        public enum enuParameterName
        {
            TCP_IP,
            TCP_Port,
            TimeOut_ms,//3000
            DelayTime_ms,
            Simulate,
            ByPass,
        }

        #endregion

        #region //===================== 必要函式設置 =====================

        public clsCtrlTCPLink()
        {
            m_ClientDevice = new SocketClient(m_ClientType,m_ClientStringCode);
            m_ClientDevice.DataArrivalEvent2 += new SocketClient.DataArrivalEventHandler2(m_Socket_DataArrivalEvent);
            m_ClientDevice.ConnectedEvent += new SocketClient.ConnectedEventHandler(m_Socket_ClientConnect);
            m_ClientDevice.ConnectFailEvent +=  new SocketClient.ConnectFailEventHandler(m_Socket_ClientConnectFail);
            m_ClientDevice.DisconnectEvent += new SocketClient.DisconnectEventHandler(m_Socket_ClientDisonnect);
            m_ClientDevice.m_iReceiveBufferSize = m_iReceiveBufferSize;
            m_ServerDevice = new SocketServer(m_ServerType, m_ServerThreadType, m_ServerStringCode);
            m_ServerDevice.DataArrivalEvent2 += new SocketServer.DataArrivalEventHandler2(m_Socket_DataArrivalEvent);
            m_ServerDevice.ClientConnectedEvent += new SocketServer.ClientConnectedEventHandler(m_Socket_ServerGetClient);
            m_ServerDevice.ClientDisconnectEvent += new SocketServer.ClientDisconnectEventHandler(m_Socket_ServerLostClient);
            m_ServerDevice.m_iReceiveBufferSize = m_iReceiveBufferSize;
            if (_GetValue(enuParameterName.TimeOut_ms) == 0)
            { _SaveValue(enuParameterName.TimeOut_ms, 3000); }
            _SaveValue(enuParameterName.ByPass, 0);
            _SaveValue(enuParameterName.Simulate, 0);
        }

        ~clsCtrlTCPLink()
        {
        }

        #endregion

        #region//===================== Public 函式 =====================
        public void SetServer()
        {
            if (_bIsServer == false)
            {
                _Disconnect();
                _bIsServer = true;
            }
        }
        public void SetClient()
        {
            if (_bIsServer == true)
            {
                _Disconnect();
                _bIsServer = false;
            }
        }

        /// <summary> TCPIP 連線 </summary>
        public void _Connect()
        {
            _Connect(sTCP_IP, iTCP_Port);
        }
        /// <summary> TCPIP 連線 </summary>
        public void _Connect(string strIP , int iPort)
        {
            try
            {
                sTCP_IP = strIP;
                iTCP_Port = iPort;
                if (_bIsServer == true)
                {
                    m_ServerDevice.Listen(iPort);
                }
                else
                {
                    m_ClientDevice.Connect(IPAddress.Parse(strIP), iPort);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> TCPIP 斷線 </summary>
        public void _Disconnect()
        {
            try
            {
                if (_bIsServer == true)
                {
                    //m_ServerDevice.Send("Close");
                    foreach (Socket pSocketClient in m_ServerDevice.GetClientList())
                    {
                        pSocketClient.Disconnect(false);
                    }
                    m_ServerDevice.Stop();
                }
                else
                {
                    m_ClientDevice.Disconnect();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 讀取TCPIP是否連線 return : True連線,False斷線  </summary>
        public bool _IsConnected()
        {
            bool rValue = false;
            try
            {
                if (_bIsServer == true)
                {
                    rValue = m_ServerDevice.IsConnected;
                }
                else
                {
                    rValue = m_ClientDevice.IsConnected;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        /// <summary> 傳送指令 </summary>
        public void _Send(string s_Message)
        {
            Send_private(s_Message);
        }
        /// <summary> 傳送Byte Datas </summary>
        public void _Send(byte[] s_Message)
        {
            Send_private(s_Message);
        }
        /// <summary> 傳送Image </summary>
        public void _Send(Image Image, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            _Send(ImageToBuffer(Image, imageFormat));
        }

        public void _ClearLastArrivedMessage()
        {
            _ArrivalMessageLatest = "";
        }



        /// <summary> 設定暫存參數，且儲存到INI [\\INI\\Device.ini] </summary>
        public void _SaveValue(enuParameterName p_ParameterName, double p_Value)
        {
            _SaveValue_String(p_ParameterName, p_Value.ToString());
        }
        /// <summary> 設定暫存參數，且儲存到INI [\\INI\\Device.ini] </summary>
        public void _SaveValue_String(enuParameterName p_ParameterName, string p_Value)
        {
            try
            {
                string sPath = ucTCPLinkSetting.GetSingleton().mPmt.sINIPath;
                if (System.IO.File.Exists(sPath) == true
                    && ucTCPLinkSetting.GetSingleton().mPmt.mDic_mPmtValue.ContainsKey(sName) == true
                    && Enum.IsDefined(typeof(clsPmtTCPLink.enuPmtName), p_ParameterName.ToString()) == true)
                {
                    clsPmtTCPLink.enuPmtName ePmtName = (clsPmtTCPLink.enuPmtName)Enum.Parse(typeof(clsPmtTCPLink.enuPmtName), p_ParameterName.ToString());
                    string PreviousValue = "";
                    if (ucTCPLinkSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].ContainsKey(ePmtName) == true)
                    {
                        PreviousValue = ucTCPLinkSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName];
                    }
                    else
                    {
                        ucTCPLinkSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].Add(ePmtName, "");
                    }
                    clsLog.Log(clsCmData.enuLogType.SystemLog, clsCmData.g_strNowUser + " : "
                           + ", TCPLink Name : " + sName
                           + ", Pmt Name : " + ePmtName.ToString()
                           + ", Change Value : " + PreviousValue + "-> " + p_Value);
                    ucTCPLinkSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName] = p_Value;
                    clsIniFile mFile = new clsIniFile(sPath);
                    mFile.WriteValue(sName, ePmtName.ToString(), ucTCPLinkSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName]);
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
                string sValue = _GetValue_String(p_ParameterName);
                if (sValue != null && sValue != "")
                {
                    rValue = Convert.ToDouble(_GetValue_String(p_ParameterName));
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        /// <summary> 讀取暫存參數，暫存參數可以使用LoadINIPrameter() 載入[\\INI\\Device.ini]檔案內容 </summary>
        public string _GetValue_String(enuParameterName p_ParameterName)
        {
            string rValue = "";
            try
            {
                string sPath = ucTCPLinkSetting.GetSingleton().mPmt.sINIPath;
                if (System.IO.File.Exists(sPath) == true
                    && ucTCPLinkSetting.GetSingleton().mPmt.mDic_mPmtValue.ContainsKey(sName) == true
                    && Enum.IsDefined(typeof(clsPmtTCPLink.enuPmtName), p_ParameterName.ToString()) == true)
                {
                    clsPmtTCPLink.enuPmtName ePmtName = (clsPmtTCPLink.enuPmtName)Enum.Parse(typeof(clsPmtTCPLink.enuPmtName), p_ParameterName.ToString());
                    if (ucTCPLinkSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].ContainsKey(ePmtName) == false)
                    {
                        ucTCPLinkSetting.GetSingleton().mPmt.mDic_mPmtValue[sName].Add(ePmtName, "");
                    }
                    rValue = ucTCPLinkSetting.GetSingleton().mPmt.mDic_mPmtValue[sName][ePmtName];
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        #endregion

        #region//===================== private 函式 =====================
        /// <summary> 傳送指令 </summary>
        private void Send_private(string s_Message, int ClientIndex = -1)
        {
            //lock (m_objLockSend) 
            {
                try
                {
                    if (_IsConnected() == true)
                    {
                        if (_bIsServer == true)
                        {
                            if (ClientIndex != -1)
                            {
                                m_ServerDevice.Send(s_Message, ClientIndex);
                            }
                            else
                            {
                                m_ServerDevice.Send(s_Message);
                            }
                        }
                        else
                        {
                            m_ClientDevice.Send(s_Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }
        }

        /// <summary> 傳送指令 </summary>
        private void Send_private(byte[] b_Message, int ClientIndex = -1)
        {
            //lock (m_objLockSend) 
            {
                try
                {
                    if (_IsConnected() == true)
                    {
                        if (_bIsServer == true)
                        {
                            if (ClientIndex != -1)
                            {
                                m_ServerDevice.Send(b_Message, ClientIndex);
                            }
                            else
                            {
                                m_ServerDevice.Send(b_Message);
                            }
                        }
                        else
                        {
                            m_ClientDevice.Send(b_Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }
        }
        #endregion

        #region//===================== Static Public函式 =====================

        /// <summary>  將 Image 轉換為 Byte 陣列。　</summary>
        public static byte[] ImageToBuffer(Image Image, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if (Image == null) { return null; }
            byte[] data = null;
            using (MemoryStream oMemoryStream = new MemoryStream())
            {
                //建立副本
                using (Bitmap oBitmap = new Bitmap(Image))
                {
                    //儲存圖片到 MemoryStream 物件，並且指定儲存影像之格式
                    oBitmap.Save(oMemoryStream, imageFormat);
                    //設定資料流位置
                    oMemoryStream.Position = 0;
                    //設定 buffer 長度
                    data = new byte[oMemoryStream.Length];
                    //將資料寫入 buffer
                    oMemoryStream.Read(data, 0, Convert.ToInt32(oMemoryStream.Length));
                    //將所有緩衝區的資料寫入資料流
                    oMemoryStream.Flush();
                }
            }
            return data;
        }
        /// <summary> 將 Byte 陣列轉換為 Image。 </summary>
        public static Image BufferToImage(byte[] Buffer)
        {
            if (Buffer == null || Buffer.Length == 0) { return null; }
            byte[] data = null;
            Image oImage = null;
            Bitmap oBitmap = null;
            //建立副本
            data = (byte[])Buffer.Clone();
            try
            {
                MemoryStream oMemoryStream = new MemoryStream(Buffer);
                //設定資料流位置
                oMemoryStream.Position = 0;
                oImage = System.Drawing.Image.FromStream(oMemoryStream);
                //建立副本
                oBitmap = new Bitmap(oImage);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return oBitmap;
        }

        #endregion

        #region//========== Private Function (Event) ==========

        private void m_Socket_DataArrivalEvent(object sender, EventArgs e, string ArrivalMessage)
        {
            lock (m_objLockArrived)
            {
                try
                {
                    _ArrivalMessageLatest = ArrivalMessage;
                    if (m_JSEvent != null)
                    {
                        m_JSEvent(this, sender, e, ArrivalMessage);
                    }
                    if (m_MessageArrivelEvent != null)
                    {
                        m_MessageArrivelEvent(this, sender, e, ArrivalMessage);
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }
        }
        private void m_Socket_ClientConnect(object sender, SocketClientEventArgs e)
        {
            try
            {
                if (m_ClientConnectHandler != null)
                {
                    m_ClientConnectHandler(sender, e);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void m_Socket_ClientConnectFail(object sender, SocketClientEventArgs e)
        {
            try
            {
                if (m_ClientConnectFailHandler != null)
                {
                    m_ClientConnectFailHandler(sender, e);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void m_Socket_ClientDisonnect(object sender, SocketClientEventArgs e)
        {
            try
            {
                if (m_ClientDisonnectHandler != null)
                {
                    m_ClientDisonnectHandler(sender, e);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void m_Socket_ServerGetClient(SocketServerClientConnectedEventArgs e)
        {
            try
            {
                if (m_ServerGetClientHandler != null)
                {
                    m_ServerGetClientHandler(e);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void m_Socket_ServerLostClient(SocketServerClientDisconnectEventArgs e)
        {
            try
            {
                if (m_ServerLostClientHandler != null)
                {
                    m_ServerLostClientHandler(e);
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

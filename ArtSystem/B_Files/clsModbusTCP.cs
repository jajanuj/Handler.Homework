using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modbus.Device;      //for modbus master
using System.Net;         //for tcp client
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;

namespace ArtSystem.Files
{
    public partial class clsModbusTCP
    {
        //static System.Windows.Forms.Timer timer1;
        static object m_Lock = new object();

        [DllImport("WININET", CharSet = CharSet.Auto)]
        static extern bool InternetGetConnectedState(ref InternetConnectionState lpdwFlags, int dwReserved);

        enum InternetConnectionState : int
        {
            INTERNET_CONNECTION_MODEM = 0x1,
            INTERNET_CONNECTION_LAN = 0x2,
            INTERNET_CONNECTION_PROXY = 0x4,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }

        TcpClient tcpClient;
        ModbusIpMaster master;
        string ipAddress;   // ip address of ET-7083
        int tcpPort = 502;  // modbus tcp port
        byte slaveID = 1;   // modbus id of ET-7083

        DateTime dtDisconnect = new DateTime();
        DateTime dtNow = new DateTime();

        List<uint> listCntVal = new List<uint>();

        bool NetworkIsOk = false;

        public clsModbusTCP()
        {
            listCntVal.Add(0);
            listCntVal.Add(0);
            listCntVal.Add(0);
            //NetworkIsOk = Connect();
            //timer1 = new System.Windows.Forms.Timer();
            //timer1.Tick += new EventHandler(timer1_Tick);
            //timer1.Interval = 100;
            //timer1.Enabled = true;
            //timer1.Start();
            //timer1_Tick(null, null);
        }
        public bool IsConnect()
        {
            if (tcpClient == null)
            {
                return false;
            }
            if (tcpClient.Client == null)
            {
                return false;
            }
            return tcpClient.Connected;
        }
        private bool Connect()
        {
            ipAddress = "192.168.255.1";

            if (master != null) master.Dispose();
            if (tcpClient != null) tcpClient.Close();

            if (CheckInternet())
            {
                try
                {
                    // tcpClient = new TcpClient(ipAddress, tcpPort);
                    tcpClient = new TcpClient();

                    IAsyncResult asyncResult = tcpClient.BeginConnect(ipAddress, tcpPort, null, null);
                    asyncResult.AsyncWaitHandle.WaitOne(1000, true); // wait for 1 sec
                    if (!asyncResult.IsCompleted)
                    {
                        tcpClient.Close();


                        return false;
                    }

                    // create Modbus TCP Master by the tcp client
                    //document->Modbus.Device.Namespace->ModbusIpMaster Class->Create Method
                    master = ModbusIpMaster.CreateIp(tcpClient);
                    master.Transport.Retries = 0;
                    master.Transport.ReadTimeout = 500;

                    //label3.Text = DateTime.Now.ToString() + " - Connect to ET-7083.";

                    return true;
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    //label3.Text = DateTime.Now.ToString() + " - Connect process " + ex.StackTrace + "==>" + ex.Message;

                    return false;
                }
            }

            return false;
        }

        private bool CheckInternet()
        {
            //http://msdn.microsoft.com/en-us/library/windows/desktop/aa384702(v=vs.85).aspx
            InternetConnectionState flag = InternetConnectionState.INTERNET_CONNECTION_LAN;
            return InternetGetConnectedState(ref flag, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            //MutexWait.WaitOne(); 
            try
            {
                if (NetworkIsOk)
                {
                    // read modbus registers (3xxxx), starting address 16
                    ushort[] register = master.ReadInputRegisters(slaveID, 16, 6);
                    uint cntVal = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        listCntVal[i] = ((uint)(register[i * 2 + 1] << 16) + register[i * 2]);

                    }
                }
                else
                {
                    dtNow = DateTime.Now;

                    if ((dtNow - dtDisconnect) > TimeSpan.FromSeconds(10))
                    {
                        //label3.Text = DateTime.Now.ToString() + " - Start connecting";

                        NetworkIsOk = Connect();
                        if (NetworkIsOk == false)
                        {
                            //label3.Text = DateTime.Now.ToString() + " - Connecting fail. Wait for retry";

                            dtDisconnect = DateTime.Now;
                        }
                    }
                    else
                    {
                        //label3.Text = DateTime.Now.ToString() + " - Wait for retry connecting";
                    }
                }

            }
            catch (Exception exception)
            {
                //Connection exception
                //No response is received from server.
                //The server maybe close the connection, or response timeout.
                if (exception.Source.Equals("System"))
                {
                    NetworkIsOk = false;
                    //label3.Text = exception.Message;

                    //this.Text = "Off line " + DateTime.Now.ToString();
                    dtDisconnect = DateTime.Now;
                }

                //The server return error code.
                //You can get the function code and exception code.
                if (exception.Source.Equals("nModbusPC"))
                {
                    string str = exception.Message;
                    int FunctionCode;
                    string ExceptionCode;

                    str = str.Remove(0, str.IndexOf("\r\n") + 17);
                    FunctionCode = Convert.ToInt16(str.Remove(str.IndexOf("\r\n")));
                    //label3.Text = "Function Code: " + FunctionCode.ToString("X");

                    str = str.Remove(0, str.IndexOf("\r\n") + 17);
                    ExceptionCode = str.Remove(str.IndexOf("-"));
                    //switch (ExceptionCode.Trim()) {
                    //case "1":
                    //    label3.Text = "Exception Code: " + ExceptionCode.Trim() + "-> Illegal function!";
                    //    break;
                    //case "2":
                    //    label3.Text = "Exception Code: " + ExceptionCode.Trim() + "-> Illegal data address!";
                    //    break;
                    //case "3":
                    //    label3.Text = "Exception Code: " + ExceptionCode.Trim() + "-> Illegal data value!";
                    //    break;
                    //case "4":
                    //    label3.Text = "Exception Code: " + ExceptionCode.Trim() + "-> Slave device failure!";
                    //    break;
                    //}
                    /*
                       //Modbus exception codes definition
                            
                       * Code   * Name                                      * Meaning
                         01       ILLEGAL FUNCTION                            The function code received in the query is not an allowable action for the server.
                         
                         02       ILLEGAL DATA ADDRESS                        The data addrdss received in the query is not an allowable address for the server.
                         
                         03       ILLEGAL DATA VALUE                          A value contained in the query data field is not an allowable value for the server.
                           
                         04       SLAVE DEVICE FAILURE                        An unrecoverable error occurred while the server attempting to perform the requested action.
                             
                         05       ACKNOWLEDGE                                 This response is returned to prevent a timeout error from occurring in the client (or master)
                                                                              when the server (or slave) needs a long duration of time to process accepted request.
                          
                         06       SLAVE DEVICE BUSY                           The server (or slave) is engaged in processing a long–duration program command , and the 
                                                                              client (or master) should retransmit the message later when the server (or slave) is free.
                             
                         08       MEMORY PARITY ERROR                         The server (or slave) attempted to read record file, but detected a parity error in the memory.
                             
                         0A       GATEWAY PATH UNAVAILABLE                    The gateway is misconfigured or overloaded.
                             
                         0B       GATEWAY TARGET DEVICE FAILED TO RESPOND     No response was obtained from the target device. Usually means that the device is not present on the network.

                     */
                }
            }
            //MutexWait.ReleaseMutex();
            //timer1.Enabled = true;

        }

        public uint GetVal1()
        {
            if (IsConnect() == false)
            {
                return 0;
            }
            timer1_Tick(null, null);
            return listCntVal[0];
        }

        public uint GetVal2()
        {

            if (IsConnect() == false)
            {
                return 0;
            }
            timer1_Tick(null, null);
            return listCntVal[1];
        }

        public uint GetVal3()
        {
            if (IsConnect() == false)
            {
                return 0;
            }
            timer1_Tick(null, null);
            return listCntVal[2];
        }


        public void RestCount()
        {
            if (IsConnect() == false)
            {
                return;
            }
            // clear the counter value
            //ushort n = 34; // Modbus register 00034 + (channel number)
            try
            {
                for (int i = 0; i < listCntVal.Count; i++)
                {
                    listCntVal[i] = 0;
                }
                if (NetworkIsOk)
                {
                    master.WriteSingleCoil(slaveID, 34, true);
                    master.WriteSingleCoil(slaveID, 35, true);
                    master.WriteSingleCoil(slaveID, 36, true);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                NetworkIsOk = false;
            }
        }
    }
}

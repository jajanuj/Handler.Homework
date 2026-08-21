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
using ArtSystem.Files;

namespace ArtSystem.MultiSystem
{
    public class clsCtrlTCPJSLink<T>
    {
        public string _LastArrivalMachineName = "";
        public string _ReceiveClassName = "";
        public int _LastArrivalMachineIndex = 0;
        public T _RecivedDatas;
        public Dictionary<string, T> _MachineDatas = new Dictionary<string, T>();
        private clsCtrlTCPLink linkFunction = null;
        public string _DeviceName = "";
        private bool bIsArtLinkAllClient = false;
        private string ArrivelMessage = "";

        /// <summary> TCPIP Message Arrivel 事件</summary>
        public event DataArrivalJSHandler m_MessageArrivelJSEvent;
        public delegate void DataArrivalJSHandler(object sender, object p_TCPIPControl, EventArgs e, string ArrivelMessage, T ArrivelClass);

        /// <summary> 本機網路IP </summary>
        private List<IPAddress> listlocalAddress = new List<IPAddress>();

        private void Event(object sender, object p_TCPIPControl, EventArgs e, string p_ArrivelMessage)
        {
            string ReceiveMachineName = "";
            if (linkFunction._bIsServer == true)
            {
                _LastArrivalMachineIndex = ((SocketServerDataArrivalEventArgs)e)._ByteCount;
            }
            ArrivelMessage += p_ArrivelMessage;
            if (ArrivelMessage.Contains("[\\Json]") == true
                && ArrivelMessage.Contains("[Json]") == false)
            {
                ArrivelMessage = "";
            }
            else if (ArrivelMessage.Contains("[Json]") && ArrivelMessage.Contains("[\\Json]"))
            {
                int Index = 0;
                List<string> Datas = new List<string>();
                #region 把ArrivelMessage分割成Datas
                string[] ArrivedMessages = ArrivelMessage.Split(']');
                for (int i = 0; i < ArrivedMessages.Length; i++)
                {
                    if (Datas.Count == 0)
                    {
                        Datas.Add("");
                        Index = 0;
                    }
                    if (i == ArrivedMessages.Length - 1)
                    {
                        Datas[Index] += ArrivedMessages[i];
                    }
                    else
                    {
                        Datas[Index] += ArrivedMessages[i] + "]";
                    }
                    if (Datas[Index].Contains("[Json]") && Datas[Index].Contains("[\\Json]"))
                    {
                        Datas.Add("");
                        Index++;
                    }
                }
                #endregion
                foreach (string Data in Datas)
                {
                    if (Data.Contains("[Json]") && Data.Contains("[\\Json]"))
                    {
                        int pIndex = Data.IndexOf("[Json]");
                        if (pIndex <= 0) { pIndex = 0; }
                        string JsonMessage = Data.Remove(0, 6 + pIndex);
                        JsonMessage = JsonMessage.Replace("[\\Json]", "");

                        if (JsonMessage.IndexOf("[") == 0 && JsonMessage.Contains("]{") == true)
                        {
                            ReceiveMachineName = JsonMessage.Split(']')[0].Replace("[", "");
                            JsonMessage = JsonMessage.Replace("[" + ReceiveMachineName + "]", "");
                            try
                            {
                                _LastArrivalMachineName = ReceiveMachineName.Split(';')[0] + ReceiveMachineName.Split(';')[1];
                                _ReceiveClassName = ReceiveMachineName.Split(';')[2];
                                ReceiveMachineName = _LastArrivalMachineName;
                            }
                            catch
                            {
                            }
                        }
                        T Temp;
                        try
                        {
                            Temp = JsonHelper.JsonDeserialize<T>(JsonMessage, Encoding.Unicode);
                            var Templist = Temp.GetType().GetFields().ToList();
                            if (bIsArtLinkAllClient == true)
                            {
                                try
                                {
                                    if (_MachineDatas.Keys.Contains(ReceiveMachineName) == true)
                                    {
                                        for (int i = 0; i < Templist.Count; i++)
                                        {
                                            try
                                            {
                                                _MachineDatas[ReceiveMachineName].GetType().GetField(Templist[i].Name).SetValue(_MachineDatas[ReceiveMachineName],
                                                    Temp.GetType().GetField(Templist[i].Name).GetValue(Temp));
                                            }
                                            catch
                                            {
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _MachineDatas.Add(ReceiveMachineName, Temp);
                                    }
                                    if (ReceiveMachineName != linkFunction.sName
                                        && linkFunction._bIsServer == true
                                        && linkFunction.g_bServerSendToAllClient == true)
                                    {
                                        ReceiveMachineName = "Server";
                                        ServerReSendClsData(ReceiveMachineName, Temp);
                                    }
                                    _RecivedDatas = _MachineDatas[ReceiveMachineName];
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                try
                                {
                                    if (_MachineDatas.Keys.Contains(_DeviceName + " (Recived) ") == true)
                                    {
                                        for (int i = 0; i < Templist.Count; i++)
                                        {
                                            try
                                            {
                                                _MachineDatas[_DeviceName + " (Recived) "].GetType().GetField(Templist[i].Name).SetValue(_MachineDatas[_DeviceName + " (Recived) "],
                                                    Temp.GetType().GetField(Templist[i].Name).GetValue(Temp));
                                            }
                                            catch
                                            {
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _MachineDatas.Add(_DeviceName + " (Recived) ", Temp);
                                    }
                                    _RecivedDatas = _MachineDatas[_DeviceName + " (Recived) "];
                                }
                                catch
                                {
                                }
                            }
                            try
                            {
                                if (m_MessageArrivelJSEvent != null)
                                {
                                    m_MessageArrivelJSEvent(sender, p_TCPIPControl, e, p_ArrivelMessage, Temp);
                                }
                            }
                            catch
                            {
                            }
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        ArrivelMessage = Data;
                    }
                }
            }


        }

        public clsCtrlTCPJSLink(clsCtrlTCPLink LinkControl)
        {
            linkFunction = LinkControl;
            linkFunction._JSEvent += Event;
            GetCurrnetIP();
        }
        public void _LinkAllClient(bool IsLinkAllClient)
        {
            bIsArtLinkAllClient = IsLinkAllClient;
        }

        public void _Send(T clsData)
        {
            string JsonMessage = JsonHelper.JsonSerialize(clsData);
            string LocalIP = "";
            if (listlocalAddress.Count > 0)
            {
                LocalIP = listlocalAddress[0].ToString();
            }
            string ClassName = clsData.GetType().Name;
            JsonMessage = "[" + _DeviceName + ";" + LocalIP + ";" + ClassName + "]" + JsonMessage;
            if (bIsArtLinkAllClient == false)
            {
                #region (send) 
                if (_MachineDatas.Keys.Contains(_DeviceName + " (Send) ") == true)
                {
                    var Templist = clsData.GetType().GetFields().ToList();
                    for (int i = 0; i < Templist.Count; i++)
                    {
                        try
                        {
                            _MachineDatas[_DeviceName + " (Send) "].GetType().GetField(Templist[i].Name).SetValue(_MachineDatas[_DeviceName + " (Send) "],
                                clsData.GetType().GetField(Templist[i].Name).GetValue(clsData));
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    _MachineDatas.Add(_DeviceName + " (Send) ", clsData);
                }
                #endregion
            }
            else if (linkFunction._bIsServer == true)
            {
                #region LocalIP
                if (_MachineDatas.Keys.Contains(_DeviceName + LocalIP) == true)
                {
                    var Templist = clsData.GetType().GetFields().ToList();
                    for (int i = 0; i < Templist.Count; i++)
                    {
                        try
                        {
                            _MachineDatas[_DeviceName + LocalIP].GetType().GetField(Templist[i].Name).SetValue(_MachineDatas[_DeviceName + LocalIP],
                                clsData.GetType().GetField(Templist[i].Name).GetValue(clsData));

                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    _MachineDatas.Add(_DeviceName + LocalIP, clsData);
                }
                try
                {
                    if (linkFunction.g_bServerSendToAllClient == true)
                    {
                        if (m_MessageArrivelJSEvent != null)
                        {
                            m_MessageArrivelJSEvent(this, null, null, "Server Send To Server", clsData);
                        }
                    }
                }
                catch
                {
                }
                #endregion
            }
            linkFunction._Send("[Json]" + JsonMessage + "[\\Json]");
        }

        private void ServerReSendClsData(string DeviceName, T clsDataInfo)
        {
            string JsonMessage = JsonHelper.JsonSerialize(clsDataInfo);
            JsonMessage = "[" + DeviceName + "]" + JsonMessage;
            linkFunction._Send("[Json]" + JsonMessage + "[\\Json]");
        }

        /// <summary> 尋找 Network TCPIP IPv4 </summary>
        private void GetCurrnetIP()
        {
            listlocalAddress.Clear();
            foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    listlocalAddress.Add(address);
                    break;
                }
            }
        }

    }
}

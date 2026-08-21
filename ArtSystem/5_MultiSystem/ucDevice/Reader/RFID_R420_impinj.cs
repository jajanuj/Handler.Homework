using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Impinj.OctaneSdk;

namespace ArtSystem
{
    /// <summary> RFID通訊模組，Impinj.OctaneSdk.dll需要載入 LLRP.dll 及 LLRP.Impinj.dll </summary>
    public class RFID_R420_impinj
    {
        /// <summary> 通訊元件 </summary>
        public ImpinjReader reader = null;

        /// <summary> 連線ID (EX:192.168.1.110) </summary>
        public string READER_HOSTNAME { get; private set; }

        /// <summary> 異常訊息 </summary>
        public string strErrorMessage { get; private set; }

        public int iTimeout_TagID_ms = 3000;

        private string _strLastTagID = "";
        /// <summary> TagID, 最新的TagID資訊 </summary>
        public string strLastTagID
        {

            get
            {
                if (TagID_Collection.Keys.Contains(_strLastTagID) == true)
                {
                    if (IsTimeOut(TagID_Collection[_strLastTagID], iTimeout_TagID_ms) == true)
                    {
                        _strLastTagID = "";
                    }
                }
                return _strLastTagID;
            }
            set
            {
                _strLastTagID = value;
            }
        }

        /// <summary> TagID, 最新的TagID資訊 </summary>
        public DateTime strLastTagReadTime = DateTime.Now;

        private bool bIsBusy_TagID_Collection = false;
        private bool bIsTransfer_TagID = false;

        /// <summary> TagID收集站 </summary>
        private Dictionary<string, DateTime> TagID_Collection = new Dictionary<string, DateTime>();

        #region//========== 必要函式 ==========
        public RFID_R420_impinj()
        {
            reader = new ImpinjReader();//建立通訊元件

            // Assign an event handler that will be called
            // when keepalive messages are received.
            reader.KeepaliveReceived += OnKeepaliveReceived;

            // Assign an event handler that will be called
            // if the reader stops sending keepalives.
            reader.ConnectionLost += OnConnectionLost;

            // Assign the TagsReported event handler.
            // This specifies which method to call
            // when tags reports are available.
            reader.TagsReported += OnTagsReported;

        }
        #endregion

        #region//========== Public Function ==========

        /// <summary> ReaderName : "192.168.1.100"</summary>
        public void Connect(string strHOSTNAME, int TRANSACTION_TIMEOUT = 6000)
        {
            READER_HOSTNAME = strHOSTNAME;
            System.Threading.Thread mThread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ThreadConnect));
            mThread.Start();
        }

        public void Disconnect()
        {
            if (reader != null)
            {
                // Stop reading.
                //reader.Stop();

                // Disconnect from the reader.
                reader.Disconnect();
            }
        }

        public bool IsConnected()
        {
            if (reader != null)
            {
                return reader.IsConnected;
            }
            return false;
        }

        public Dictionary<string, DateTime> GetTagData()
        {
            bIsTransfer_TagID = true;
            Dictionary<string, DateTime> Data = new Dictionary<string, DateTime>();
            int i = 100;
            while (true)
            {
                System.Threading.Thread.Sleep(1);
                if (i < 0 || bIsBusy_TagID_Collection == false)
                {
                    break;
                }
                i--;
            }
            foreach (string TagID in TagID_Collection.Keys)
            {
                if (IsTimeOut(TagID_Collection[TagID], iTimeout_TagID_ms) == false)
                {

                    Data.Add(TagID, TagID_Collection[TagID]);
                }
            }
            bIsTransfer_TagID = false;
            return Data;
        }

        public bool IsTimeOut(DateTime LstDataTime, int Timeout_ms)
        {
            DateTime dd = DateTime.Now;
            if (LstDataTime.AddMilliseconds(Timeout_ms) > DateTime.Now
                )
            {
                return false;
            }
            return true;
        }
        #endregion

        #region//========== Private Function ==========

        private void ThreadConnect(object sender)
        {
            try
            {
                //Console.WriteLine("Attempting to connect to {0} ({1}).", reader.Name, READER_HOSTNAME);
                strErrorMessage = "Attempting to connect to (" + READER_HOSTNAME + ").";

                // The maximum number of connection attempts
                // before throwing an exception.
                reader.MaxConnectionAttempts = 15;
                // Number of milliseconds before a 
                // connection attempt times out.
                reader.ConnectTimeout = 6000;
                // Connect to the reader.
                // Change the ReaderHostname constant in SolutionConstants.cs 
                // to the IP address or hostname of your reader.
                reader.Connect(READER_HOSTNAME);
                strErrorMessage = "Successfully connected.";
                KeepAliveSettting();
                // Tell the reader to send us any tag reports and 
                // events we missed while we were disconnected.
                reader.ResumeEventsAndReports();
            }
            catch (OctaneSdkException e)
            {
                try
                {
                    ArtCommonLib.clsLog.Log(clsArtSystem.g_strCatchLogName, "Source : " + e.Source + " , StackTrace : " + e.StackTrace + ", Message : " + e.Message);
                }
                catch
                {
                    
                }
                strErrorMessage = "Failed to connect.";
            }
        }

        private void KeepAliveSettting()
        {
            // Get the default settings.
            // We'll use these as a starting point
            // and then modify the settings we're 
            // interested in.
            Settings settings = reader.QueryDefaultSettings();

            // Start the reader as soon as it's configured.
            // This will allow it to run without a client connected.
            settings.AutoStart.Mode = AutoStartMode.Immediate;
            settings.AutoStop.Mode = AutoStopMode.None;

            // Use Advanced GPO to set GPO #1 
            // when an client (LLRP) connection is present.
            settings.Gpos.GetGpo(1).Mode = GpoMode.LLRPConnectionStatus;

            // Tell the reader to include the timestamp in all tag reports.
            settings.Report.IncludeFirstSeenTime = true;
            settings.Report.IncludeLastSeenTime = true;
            settings.Report.IncludeSeenCount = true;
            settings.Report.IncludeAntennaPortNumber = true;

            // If this application disconnects from the 
            // reader, hold all tag reports and events.
            settings.HoldReportsOnDisconnect = true;

            // Enable keepalives.
            settings.Keepalives.Enabled = true;
            settings.Keepalives.PeriodInMs = 5000;

            // Enable link monitor mode.
            // If our application fails to reply to
            // five consecutive keepalive messages,
            // the reader will close the network connection.
            settings.Keepalives.EnableLinkMonitorMode = true;
            settings.Keepalives.LinkDownThreshold = 5;

            // Apply the newly modified settings.
            reader.ApplySettings(settings);

            // Save the settings to the reader's 
            // non-volatile memory. This will
            // allow the settings to persist
            // through a power cycle.
            reader.SaveSettings();
        }


        #endregion

        #region//========== Event (接受訊息) ==========

        private void OnConnectionLost(ImpinjReader reader)
        {
            //// This event handler is called if the reader  
            //// stops sending keepalive messages (connection lost).
            //Console.WriteLine("Connection lost : {0} ({1})", reader.Name, reader.Address);

            //// Cleanup
            //reader.Disconnect();

            //// Try reconnecting to the reader
            //Connect(READER_HOSTNAME);
        }

        private void OnKeepaliveReceived(ImpinjReader reader)
        {
            // This event handler is called when a keepalive 
            // message is received from the reader.
            //Console.WriteLine("Keepalive received from {0} ({1})", reader.Name, reader.Address);
        }

        private void OnTagsReported(ImpinjReader sender, TagReport report)
        {
            if (bIsTransfer_TagID == true)
            {
                return;
            }
            bIsBusy_TagID_Collection = true;
            // This event handler is called asynchronously 
            // when tag reports are available.
            // Loop through each tag in the report 
            // and print the data.
            foreach (Tag tag in report)
            {

                _strLastTagID = tag.AntennaPortNumber.ToString() + ";" + tag.Epc.ToString().Replace(" ", "");//HS 2023/07/31
                strLastTagReadTime = DateTime.Now;

                if (TagID_Collection.Keys.Contains(_strLastTagID) == false)
                {
                    TagID_Collection.Add(_strLastTagID, strLastTagReadTime);
                }
                else
                {
                    TagID_Collection[_strLastTagID] = strLastTagReadTime;
                }
                //Console.WriteLine("EPC : {0} Timestamp : {1}", tag.Epc, tag.LastSeenTime);
            }
            List<string> DeleteLst = new List<string>();
            foreach (string sTagID in TagID_Collection.Keys)
            {
                if (IsTimeOut(TagID_Collection[sTagID], iTimeout_TagID_ms) == true)
                {
                    DeleteLst.Add(sTagID);
                }
            }
            for (int i = 0; i < DeleteLst.Count; i++)
            { TagID_Collection.Remove(DeleteLst[i]); }

            if (TagID_Collection.Count == 0)
            { strLastTagID = ""; }

            bIsBusy_TagID_Collection = false;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using AxWinSecsLib;
using WinSecsLib;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ArtEQ
{
    public partial class ucHost_SECS : ArtCommonLib.ucBaseUserControl
    {

        #region //========== Defined File Path (Static) ==========

        //private string sSECS_SettingPath = "D:\\SECS_Para\\Host SECS\\";
        private string sSECS_SettingPath = Application.StartupPath + "\\SECS_Para\\Host SECS\\";
        public clsWriteLog m_TraceLog = new clsWriteLog("D:\\Log\\Host SECS Trace\\");
        public string g_strMapDownloadPath = "D:\\Map Data\\Mapping\\Download\\";
        public string g_strMapUploadPath = "D:\\Map Data\\Mapping\\Upload\\";
        public string g_strTraceabilityDataPath = "D:\\Map Data\\Traceability\\";

        // lstMessage顯示Log----
        public void TraceLog(TraceLogType type, string Msg)
        {
            if (type == TraceLogType.SystemConnected)
            {
                ListViewItem lvis = new ListViewItem("System");
                lvis.SubItems.Add(Msg);
                lvis.SubItems.Add(DateTime.Now.ToString());
                lvis.BackColor = Color.FromArgb(64, 255, 64);
                lvwMessage.Items.Insert(0, lvis);
            }
            else if (type == TraceLogType.SystemDisconnected)
            {
                ListViewItem lvis = new ListViewItem("System");
                lvis.SubItems.Add(Msg);
                lvis.SubItems.Add(DateTime.Now.ToString());
                lvis.BackColor = Color.FromArgb(255, 64, 64);
                lvwMessage.Items.Insert(0, lvis);
            }
            else if (type == TraceLogType.SystemErrorAlarm)
            {
                ListViewItem lvis = new ListViewItem("System");
                lvis.SubItems.Add(Msg);
                lvis.SubItems.Add(DateTime.Now.ToString());
                lvis.BackColor = Color.FromArgb(255, 0, 0);
                lvwMessage.Items.Insert(0, lvis);
            }
            else if (type == TraceLogType.Host2Equipment)
            {
                ListViewItem lvis = new ListViewItem("H->E");
                lvis.SubItems.Add(Msg);
                lvis.SubItems.Add(DateTime.Now.ToString());
                lvis.BackColor = Color.FromArgb(128, 255, 192);
                lvwMessage.Items.Insert(0, lvis);
            }
            else if (type == TraceLogType.Equipment2Host)
            {
                //ListViewItem lvis = new ListViewItem("H<-E");
                //lvis.SubItems.Add(Msg);
                //lvis.SubItems.Add(DateTime.Now.ToString());
                //lvis.BackColor = Color.FromArgb(255, 196, 128);
                //lvwMessage.Items.Insert(0, lvis);
                TerminalDisplaySECSMsgEquipment2Host(Msg);
            }
            else if (type == TraceLogType.EquipmentEvent)
            {
                //ListViewItem lvis = new ListViewItem("H<-E");
                //lvis.SubItems.Add(Msg);
                //lvis.SubItems.Add(DateTime.Now.ToString());
                //lvis.BackColor = Color.FromArgb(255, 196, 128);
                //lvwMessage.Items.Insert(0, lvis);
                TerminalDisplaySECSMsgEquipmentEvent(Msg);

            }
            else if (type == TraceLogType.HostCommand)
            {
                ListViewItem lvis = new ListViewItem("H->E");
                lvis.SubItems.Add(Msg);
                lvis.SubItems.Add(DateTime.Now.ToString());
                lvis.BackColor = Color.FromArgb(128, 255, 192);
                lvwMessage.Items.Insert(0, lvis);
            }
        }

        private delegate void DelShowMessage(string sMessage);

        private void TerminalDisplaySECSMsgEquipment2Host(string sMessage)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    DelShowMessage del = new DelShowMessage(TerminalDisplaySECSMsgEquipment2Host); //利用委派執行
                    this.Invoke(del, sMessage);
                }
                else
                {
                    ListViewItem lvis = new ListViewItem("H<-E");
                    lvis.SubItems.Add(sMessage);
                    lvis.SubItems.Add(DateTime.Now.ToString());
                    lvis.BackColor = Color.FromArgb(255, 196, 128);
                    lvwMessage.Items.Insert(0, lvis);
                }

            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog(ex.ToString());
            }
        }

        private void TerminalDisplaySECSMsgEquipmentEvent(string sMessage)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    DelShowMessage del = new DelShowMessage(TerminalDisplaySECSMsgEquipmentEvent); //利用委派執行
                    this.Invoke(del, sMessage);
                }
                else
                {
                    ListViewItem lvis = new ListViewItem("H<-E");
                    lvis.SubItems.Add(sMessage);
                    lvis.SubItems.Add(DateTime.Now.ToString());
                    lvis.BackColor = Color.FromArgb(255, 196, 128);
                    lvwMessage.Items.Insert(0, lvis);
                }

            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog(ex.ToString());
            }
        }

        // lstMessage顯示Log----

        private float X;//當前窗體的寬度
        private float Y;//當前窗體的高度

        /// <summary>
        /// 將控制項的寬，高，左邊距，頂邊距和字體大小暫存到tag屬性中
        /// </summary>
        /// <param name="cons">遞歸控制項中的控制項</param>
        private void setTag(Control cons)
        {
            try
            {
                foreach (Control con in cons.Controls)
                {
                    con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                    if (con.Controls.Count > 0)
                        setTag(con);
                }
            }
            catch (Exception ex)
            {
            }
        }

        //根據窗體大小調整控制項大小
        private void setControls(float newx, float newy, Control cons)
        {
            try
            {
                //遍歷窗體中的控制項，重新設置控制項的值
                foreach (Control con in cons.Controls)
                {

                    string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//獲取控制項的Tag屬性值，並分割後存儲字元串數組
                    float a = System.Convert.ToSingle(mytag[0]) * newx;//根據窗體縮放比例確定控制項的值，寬度
                    con.Width = (int)a;//寬度
                    a = System.Convert.ToSingle(mytag[1]) * newy;//高度
                    con.Height = (int)(a);
                    a = System.Convert.ToSingle(mytag[2]) * newx;//左邊距離
                    con.Left = (int)(a);
                    a = System.Convert.ToSingle(mytag[3]) * newy;//上邊緣距離
                    con.Top = (int)(a);
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字體大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ucHost_SECS_Resize(object sender, EventArgs e)
        {
            try
            {
                float newx = (this.Width) / X; //窗體寬度縮放比例
                float newy = (this.Height) / Y;//窗體高度縮放比例
                setControls(newx, newy, this);//隨窗體改變控制項大小

                dgvSVID.Dock = System.Windows.Forms.DockStyle.Fill;
                dgvSVID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvDVID.Dock = System.Windows.Forms.DockStyle.Fill;
                dgvDVID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvCEID.Dock = System.Windows.Forms.DockStyle.Fill;
                dgvCEID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvRPTID.Dock = System.Windows.Forms.DockStyle.Fill;
                dgvRPTID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvPPID.Dock = System.Windows.Forms.DockStyle.Fill;
                dgvPPID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgvECID.Dock = System.Windows.Forms.DockStyle.Fill;
                dgvECID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region 相關基本定義 Class

        public enum TraceLogType
        {
            Host2Equipment,
            Equipment2Host,
            EquipmentEvent,
            HostCommand,
            SystemErrorAlarm,
            SystemConnected,
            SystemDisconnected

        }

        public class clsSendEvent
        {
            private int CEID1 = -1;
            private int CEID2 = -1;
            private Dictionary<int, string> dicDVID = new Dictionary<int, string>();

            public clsSendEvent(int _CEID)
            {
                dicDVID.Clear();
                CEID1 = _CEID;
                CEID2 = -1;
            }

            /// <summary>
            /// 觸發兩個Event, 注意Report DVID需要完全相同, CEID1先送, CEID2後送
            /// </summary>
            /// <param name="_CEID1"></param>
            /// <param name="_CEID2"></param>
            public clsSendEvent(int _CEID1, int _CEID2)
            {
                dicDVID.Clear();
                CEID1 = _CEID1;
                CEID2 = _CEID2;
            }

            public void AddEditDVID(int _DVID, string _Value)
            {
                dicDVID.Add(_DVID, _Value);
            }

            public void Send()
            {
                foreach (KeyValuePair<int, string> item in dicDVID)
                {
                    ucHost_SECS.GetSingleton().SECS_UpdateDVID(item.Key, item.Value);
                }

                if (CEID1 > 0)
                {
                    ucHost_SECS.GetSingleton().SECS_DispatchEvent(CEID1);
                }
                else { }

                if (CEID2 > 0)
                {
                    ucHost_SECS.GetSingleton().SECS_DispatchEvent(CEID2);
                }
                else { }
            }

        }

        public class CEID
        {
            private int m_iID = 0;
            public int ID
            {
                get { return m_iID; }
            }

            private string m_sName = "";
            public string Name
            {
                get { return m_sName; }
            }

            public List<int> lstLinkReport = new List<int>();

            public CEID(int _ID, string _Name)
            {
                m_iID = _ID;
                m_sName = _Name;
            }

            public void LinkReport(int _RPTID)
            {
                if (lstLinkReport.Contains(_RPTID) == false)
                {
                    lstLinkReport.Add(_RPTID);
                }
                else
                {
                    return;
                }
            }

            public void DeleteAllReport()
            {
                lstLinkReport.Clear();
            }

        }

        public class RPTID
        {
            private int m_iID = 0;
            public int ID
            {
                get { return m_iID; }
            }

            public List<int> lstVID = new List<int>();

            public RPTID(int _ID)
            {
                m_iID = _ID;
                lstVID.Clear();
            }

        }

        public class SVID
        {
            private int m_iID = 0;
            public int ID
            {
                get { return m_iID; }
            }

            private string m_sName = "";
            public string Name
            {
                get { return m_sName; }
            }

            private string m_sValue = "";
            public string Value
            {
                get { return m_sValue; }
                set { m_sValue = value; }
            }


            public SVID(int _ID, string _Name)
            {
                this.m_iID = _ID;
                this.m_sName = _Name;
            }

        }

        public class DVID
        {
            private int m_iID = 0;
            public int ID
            {
                get { return m_iID; }
            }

            private string m_sName = "";
            public string Name
            {
                get { return m_sName; }
            }

            private string m_sValue = "";
            public string Value
            {
                get { return m_sValue; }
                set { m_sValue = value; }
            }


            public DVID(int _ID, string _Name)
            {
                this.m_iID = _ID;
                this.m_sName = _Name;
            }

        }

        public class ECID
        {
            private int m_iID = 0;
            public int ID
            {
                get { return m_iID; }
            }

            private string m_sName = "";
            public string Name
            {
                get { return m_sName; }
            }

            private string m_sValue = "";
            public string Value
            {
                get { return m_sValue; }
                set { m_sValue = value; }
            }

            private int m_iMaxValue= 0;
            public int MaxValue
            {
                get { return m_iMaxValue; }
                set { m_iMaxValue = value; }
            }

            private int m_iMinValue = 0;
            public int MinValue
            {
                get { return m_iMinValue; }
                set { m_iMinValue = value; }
            }

            private string m_sDefValue = "";
            public string DefValue
            {
                get { return m_sDefValue; }
                set { m_sDefValue = value; }
            }

            private string m_sUnits = "";
            public string Units
            {
                get { return m_sUnits; }
                set { m_sUnits = value; }
            }

            public ECID(int _ID, string _Name)
            {
                this.m_iID = _ID;
                this.m_sName = _Name;
            }
        }

        public class SecsState
        {
            public bool bIsPortOpen = false;
            public bool bIsSECS_PhysicalConnected = false;
            public string sControl_State_Value = "0";
            public bool bIsAlarmSendEable = true;
            public bool bIsEventSendEable = true;
            public string sRemoteIP = "127.0.0.1";
            public string sLocalIP = "127.0.0.1";
            public int iRemotePort = 5000;
            public int iLocalPort = 5000;
            private long DataID = 1;
            public int iDeviceID = 0;

            public SecsState(){}

            public long GetDataID()
            {
                DataID++;
                return DataID;
            }
        }

        public class Alarm
        {
            private int m_iID = 0;
            public int ID
            {
                get { return m_iID; }
            }

            private string m_sText = "";
            public string Text
            {
                get { return m_sText; }
            }

            private byte m_ALCD = 0;
            public byte ALCD
            {
                get { return m_ALCD; }
            }

            public Alarm(int _ID, string _Text, byte _ALCD)
            {
                m_iID = _ID;
                m_sText = _Text;
                m_ALCD = _ALCD;
            }
        }

        #endregion

        #region 必要函式設置

        static ucHost_SECS m_Singleton;

        /// <summary> 取得唯一物件，避免重覆設置 returns : 回傳物件 </summary>
        public static ucHost_SECS GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucHost_SECS();
            }
            return m_Singleton;
        }

        /// <summary> 物件建立請利用 GetSingleton()，除非特殊需求 </summary>
        private ucHost_SECS()
        {
            InitializeComponent();
            SECS_Initialization();
            ucParameter.Add(this);

            SECS_UpdateSVID(1002, "");
            SECS_UpdateSVID(1003, "");

            string RecipeName;
            RecipeName = System.IO.Path.GetFileName(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe));
            //RecipeNameList = System.IO.Directory.GetFiles(RecipeName);
            RecipeName = RecipeName.Substring(0, RecipeName.Length - 4);//省略副檔名
            
            SECS_UpdateSVID(1007, RecipeName);
        }

        /// <summary> 物件重置/// </summary>
        public void UpdateControls(){}

        /// <summary> 自動更新介面參數/// </summary>
        protected override void ReflashTimerFunc()
        {
            #region SECS UI State Update

            if (m_SecsState.bIsAlarmSendEable == true)
            {
                lblAlarmSendAble.Text = "Enable";
                lblAlarmSendAble.ForeColor = Color.Green;
            }
            else
            {
                lblAlarmSendAble.Text = "Disable";
                lblAlarmSendAble.ForeColor = Color.Red;
            }

            if (m_SecsState.bIsEventSendEable == true)
            {
                lblEventSendAble.Text = "Enable";
                lblEventSendAble.ForeColor = Color.Green;
            }
            else
            {
                lblEventSendAble.Text = "Disable";
                lblEventSendAble.ForeColor = Color.Red;
            }

            if (m_SecsState.bIsPortOpen == true)
            {
                lblSecsPort.Text = "Open";
                lblSecsPort.ForeColor = Color.Green;
            }
            else
            {
                lblSecsPort.Text = "Close";
                lblSecsPort.ForeColor = Color.Red;
            }

            if (m_SecsState.bIsSECS_PhysicalConnected == true)
            {
                lblPhysicalConnectState.Text = "Connect";
                lblPhysicalConnectState.ForeColor = Color.Green;
                btnSECSOpen.Image = ArtEQ.Properties.Resources.Host_Connect;

            }
            else
            {
                lblPhysicalConnectState.Text = "Disconnect";
                lblPhysicalConnectState.ForeColor = Color.Red;
                btnSECSOpen.Image = ArtEQ.Properties.Resources.Host_Disconnect;
            }

            if (m_SecsState.sControl_State_Value == "2")
            {
                lblControlState.Text = "Remote";
                lblControlState.ForeColor = Color.Green;
            }
            else if (m_SecsState.sControl_State_Value == "1")
            {
                lblControlState.Text = "Local";
                lblControlState.ForeColor = Color.Orange;
            }
            else
            {
                lblControlState.Text = "Offline";
                lblControlState.ForeColor = Color.Red;
            }

            #endregion
        }

        private void ucHost_SECS_Load(object sender, EventArgs e)
        {
            X = this.Width;//獲取窗體的寬度
            Y = this.Height;//獲取窗體的高度
            setTag(this);//調用方法  
        }

        #endregion

        #region Defined 參數

        public delegate void dlgUpdateUI(Label lbl, string text);
        public delegate void dlgTerminalDisp(string text);
        public delegate void dlgSecsStateChange();
        public delegate void dlgUpdateDGV(DataGridView dgv);

        public List<string> m_lstALARM = new List<string>();
        public List<bool> m_lstALARM_Enable = new List<bool>();

        public Dictionary<int, SVID> m_lstSVID = new Dictionary<int, SVID>();
        public Dictionary<int, DVID> m_lstDVID = new Dictionary<int, DVID>();
        public Dictionary<int, ECID> m_lstECID = new Dictionary<int, ECID>();

        public Dictionary<int, RPTID> m_lstRPTID = new Dictionary<int, RPTID>();
        public Dictionary<int, CEID> m_lstCEID = new Dictionary<int, CEID>();

        public List<bool> m_lstCEID_Enable = new List<bool>();
        public List<string> m_lstPPID = new List<string>();
        public List<string> m_lstPPBody = new List<string>();

        public string sMDLN = "";
        public string sEQPID = "";
        //private string sVersion = "1.0.0.0";
        public string sVersion = System.Windows.Forms.Application.ProductVersion.ToString();

        private string sSECS_Initialization_Report = "";

        public SecsState m_SecsState = new SecsState();

        private System.Windows.Forms.Timer tmrUpdateUI = new System.Windows.Forms.Timer();

        //==============SECS Cmd旗標==============
        public bool bIsMagID_OK = false;
        public bool bIsMagID_NG = false;
        public bool bIsBoatID_OK = false;
        public bool bIsBoatID_NG = false;
        public bool bIsSubstrateID_OK = false;
        public bool bIsSubstrateID_NG = false;
        public bool bIsRemote = false;
        //========================================

        #endregion

        #region Event Handler

        private void secsEAP_Connect(object sender, EventArgs e)
        {
            m_SecsState.bIsSECS_PhysicalConnected = true;
            m_SecsState.sControl_State_Value = "2";// 連線後至REMOTE State
            TraceLog(TraceLogType.SystemConnected, "----------Connected to Equipment---------");
        }

        private void secsEAP_Disconnect(object sender, SecsEvents_DisconnectEvent e)
        {
            m_SecsState.bIsSECS_PhysicalConnected = false;
            m_SecsState.sControl_State_Value = "0";// 未連線後至Offline State
            TraceLog(TraceLogType.SystemDisconnected, "-----------Disconnected------------");
        }
  
        private void secsEAP_SecondaryOut(object sender, SecsEvents_SecondaryOutEvent e)
        {}

        private void secsEAP_SecondaryIn(object sender, SecsEvents_SecondaryInEvent e)
        {
            try
            {
                SecsHost_SecondaryIn.secsEAP_SecondaryIn(e);
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception Log - [ucHost_SECS]" + ex.ToString());
            }

        }

        private void secsEAP_PrimaryIn(object sender, SecsEvents_PrimaryInEvent e)
        {
            try
            {
                SecsHost_PrimaryIn.secsEAP_PrimaryIn(e);
            }
            catch(Exception ex)
            {
                m_TraceLog.WriteLog("Exception Log - [ucHost_SECS]" + ex.ToString());
            }  
        }

        private void secsEAP_PrimaryOut(object sender, SecsEvents_PrimaryOutEvent e)
        {}

        private void btnSend_Test_Click(object sender, EventArgs e)
        {
            SECS_DispatchEvent(int.Parse(cnb_EventNum.Text));
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDGV(dgvCEID);
            UpdateDGV(dgvPPID);
            UpdateDGV(dgvRPTID);
            UpdateDGV(dgvSVID);
            UpdateDGV(dgvECID);
        }

        private void btnLoadDefaultReportID_Click(object sender, EventArgs e)
        {
            try
            {
                m_lstRPTID.Clear();
                if (File.Exists(sSECS_SettingPath + "Default-RPTID.txt") == true)
                {
                    // Read txt File
                    string sBlock = "";
                    StreamReader sr = new StreamReader(sSECS_SettingPath + "Default-RPTID.txt");
                    sBlock = sr.ReadToEnd();
                    sr.Close();

                    // Get Ever RPTID
                    string[] split_Line = { "\r\n" };
                    string[] split_Unit = { ";" };
                    string[] split_dot = { "," };
                    string[] everLine = sBlock.Split(split_Line, StringSplitOptions.RemoveEmptyEntries);


                    for (int i = 0; i < everLine.Length; i++)
                    {
                        int ID = int.Parse(everLine[i].Split(split_Unit, StringSplitOptions.RemoveEmptyEntries)[0]);
                        RPTID RptId = new RPTID(ID);
                        if (everLine[i].Split(split_Unit, StringSplitOptions.RemoveEmptyEntries).Length > 1)
                        {
                            string[] VIDs = everLine[i].Split(split_Unit, StringSplitOptions.None)[1].Split(split_dot, StringSplitOptions.RemoveEmptyEntries);
                            for (int j = 0; j < VIDs.Length; j++)
                            {
                                RptId.lstVID.Add(int.Parse(VIDs[j]));
                            }
                            m_lstRPTID.Add(ID, RptId);
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at function btnLoadDefaultReportID_Click()!! - " + ex.ToString() + "\r\n");
            }
            DefinedReportToTxtFile();
            UpdateDGV(dgvRPTID);

        }

        private void btnLoadDefaultLinkReport_Click(object sender, EventArgs e)
        {
            try
            {
                m_lstCEID.Clear();
                if (File.Exists(sSECS_SettingPath + "Default-CEID.txt") == true)
                {
                    // Read txt File
                    string sBlock = "";
                    StreamReader sr = new StreamReader(sSECS_SettingPath + "Default-CEID.txt");
                    sBlock = sr.ReadToEnd();
                    sr.Close();

                    // Get Ever CEID
                    string[] split_Line = { "\r\n" };
                    string[] split_Unit = { ";" };
                    string[] split_dot = { "," };
                    string[] everLine = sBlock.Split(split_Line, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < everLine.Length; i++)
                    {
                        int ID = int.Parse(everLine[i].Split(split_Unit, StringSplitOptions.None)[0]);
                        string NAME = everLine[i].Split(split_Unit, StringSplitOptions.None)[1];
                        string RPIDs = everLine[i].Split(split_Unit, StringSplitOptions.None)[2];
                        CEID ceid = new CEID(ID, NAME);

                        string[] everRPTID = RPIDs.Split(split_dot, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < everRPTID.Length; j++)
                        {
                            ceid.lstLinkReport.Add(int.Parse(everRPTID[j]));
                        }

                        m_lstCEID.Add(ID, ceid);
                    }

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at function btnLoadDefaultLinkReport_Click() - " + ex.ToString());
            }
            LinkReportToTxtFile();
            UpdateDGV(dgvCEID);
        }

        private void btnClearSecsMessage_Click(object sender, EventArgs e)
        {
            txtSecsLog.Text = string.Empty;
        }

        private void btnUpdateDGV_Click(object sender, EventArgs e)
        {
            UpdateDGV(dgvCEID);
            UpdateDGV(dgvPPID);
            UpdateDGV(dgvRPTID);
            UpdateDGV(dgvSVID);
            UpdateDGV(dgvECID);
        }

        private void btnWriteCommPara_Click(object sender, EventArgs e)
        {
            clsWinAPI.WritePrivateProfileString("Para", "RemoteIP", txtRemoteIP.Text, sSECS_SettingPath + "Communication.ini");
            clsWinAPI.WritePrivateProfileString("Para", "LocalIP", txtLocalIP.Text, sSECS_SettingPath + "Communication.ini");
            clsWinAPI.WritePrivateProfileString("Para", "RemotePort", txtRemotePort.Text, sSECS_SettingPath + "Communication.ini");
            clsWinAPI.WritePrivateProfileString("Para", "LocalPort", txtLocalPort.Text, sSECS_SettingPath + "Communication.ini");
            clsWinAPI.WritePrivateProfileString("Para", "DeviceID", txtDeviceID.Text, sSECS_SettingPath + "Communication.ini");
            clsWinAPI.WritePrivateProfileString("Para", "T3", tbxHSMST3.Text, sSECS_SettingPath + "Communication.ini");
            clsWinAPI.WritePrivateProfileString("Para", "T5", tbxHSMST5.Text, sSECS_SettingPath + "Communication.ini");
            clsWinAPI.WritePrivateProfileString("Para", "T6", tbxHSMST6.Text, sSECS_SettingPath + "Communication.ini");
            clsWinAPI.WritePrivateProfileString("Para", "T7", tbxHSMST7.Text, sSECS_SettingPath + "Communication.ini");
            clsWinAPI.WritePrivateProfileString("Para", "T8", tbxHSMST8.Text, sSECS_SettingPath + "Communication.ini");
        }

        #endregion

        #region Public Function

        public string strGetFileText(string strFilePath)
        {
            try
            {
                int i = 0;
                string strData = "";
                string strreturnData = "";
                string strSplit = "\r" + "\n";
                string[] tempSplit;

                strData = File.ReadAllText(strFilePath);
                tempSplit = strData.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                for (i = 0; i < tempSplit.Length; i++)
                {
                    strreturnData = strreturnData + tempSplit[i].Trim();
                }
                return strreturnData;
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at function LinkReportToTxtFile()\r\n" + ex.ToString() + "\r\n");
                return "Err";
            }
        }

        /// <summary>
        /// Return [0 : OK],  
        /// [1 : PPNAME Already Contains],  
        /// [-1 : Exception]
        /// </summary>
        /// <param name="_PPNAME"></param>
        /// <returns>0: OK, 1:add fail, -1: Exception</returns>
        public int SECS_AddPPID(string _PPNAME)
        {
            try
            {
                if (m_lstPPID.Contains(_PPNAME) == true)
                {
                    UpdateDGV(dgvPPID);
                    return 1;
                }
                else
                {
                    m_lstPPID.Add(_PPNAME);
                }
                UpdateDGV(dgvPPID);
                return 0;
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at SECS_AddPPID()\r\n" + ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// Return [0 : OK],  
        /// [1 : PPNAME Is not Contains],  
        /// [-1 : Exception]
        /// </summary>
        /// <param name="_PPNAME"></param>
        /// <returns>0: OK, 1:remove fail, -1: Exception</returns>
        public int SECS_RemovePPID(string _PPNAME)
        {
            try
            {
                if (m_lstPPID.Contains(_PPNAME) == true)
                {
                    m_lstPPID.Remove(_PPNAME);
                    UpdateDGV(dgvPPID);
                    return 0;
                }
                else
                {
                    UpdateDGV(dgvPPID);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at SECS_RemovePPID()\r\n" + ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 清除全部的PPID
        /// </summary>
        public void SECS_ClearAllPPID()
        {
            try
            {
                m_lstPPID.Clear();
                UpdateDGV(dgvPPID);
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at SECS_RemoveAllPPID()\r\n" + ex.ToString());
            }
        }

        /// <summary>
        /// return [0 : OK],  
        /// [1 : This SVID Can't be Edit],  
        /// [-1 : Exception]
        /// </summary>
        /// <param name="SVID">SVID</param>
        /// <param name="Value">更新值</param>
        /// <returns>0: OK, 1: SVID is not available</returns>
        public int SECS_UpdateSVID(int _SVID, string _Value)
        {
            try
            {
                m_lstSVID[_SVID].Value = _Value;
                if (dgvSVID.Visible == true)
                {
                    m_lstSVID[_SVID].Value = _Value;
                }
                return 0;
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at SECS_UpdateSVID()\r\n" + ex.ToString());
                return -1;
            }
        }

        public int SECS_UpdateDVID(int _DVID, string _Value)
        {
            try
            {
                m_lstDVID[_DVID].Value = _Value;
                if (dgvDVID.Visible == true)
                {
                    UpdateDGV(dgvDVID);
                }
                return 0;

            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at SECS_UpdateSVID()\r\n" + ex.ToString());
                return -1;
            }
        }

        public int SECS_UpdateECID(int _ECID, string _Value)
        {
            try
            {
                m_lstECID[_ECID].Value = _Value;
                if (dgvECID.Visible == true)
                {
                    UpdateDGV(dgvECID);
                }
                return 0;
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at SECS_UpdateSVID()\r\n" + ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// 取得SVID值, if null SVID, return "Fail SVID."
        /// </summary>
        /// <param name="_SVID"></param>
        /// <returns></returns>
        public string SECS_GetValueSVID(int _SVID)
        {
            try
            {
                if (this.m_lstSVID.ContainsKey(_SVID) == true)
                {
                    return this.m_lstSVID[_SVID].Value;
                }
                else
                {
                    return "Fail SVID.";
                }
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at SECS_GetValueSVID()\r\n" + ex.ToString());
                return "Exception.";
            }

        }

        /// <summary>
        /// 取得SVID名稱, if null SVID, return "Fail SVID."
        /// </summary>
        /// <param name="_SVID"></param>
        /// <returns></returns>
        public string SECS_GetNameSVID(int _SVID)
        {
            try
            {
                if (this.m_lstSVID.ContainsKey(_SVID) == true)
                {
                    return this.m_lstSVID[_SVID].Name;
                }
                else
                {
                    return "Fail SVID.";
                }
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at SECS_GetNameSVID()\r\n" + ex.ToString());
                return "Exception.";
            }
        }

        /// <summary>
        /// Return [0 : OK],  
        /// [1 : CEID List no this item],  
        /// [2 : CEID is not available],  
        /// [-1 : Exception]
        /// </summary>
        /// <param name="_CEID">CEID</param>
        /// <returns>0: OK, 1:CEID is not available, 2: Can't aend event now, 3: Exception</returns>
        public int SECS_DispatchEvent(int _CEID)
        {
            try
            {
                //if (m_SecsState.bIsEventSendEable == false)
                if (IsEventEnable(_CEID) == false)
                {
                    return 2;
                }

                if (m_lstCEID.ContainsKey(_CEID))
                {
                    PrimaryOutFucntion.PrimaryOut_S6F11(_CEID);
                    //TraceLog(TraceLogType.EquipmentEvent, "Equipment Send Event CEID: " + _CEID);
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at SECS_DispatchEvent()\r\n" + ex.ToString());
                return -1;
            }
        }

        private bool IsEventEnable(int _CEID)
        {
            bool res = true;
            int index = 0;

            foreach (KeyValuePair<int, CEID> item in this.m_lstCEID)
            {
                if (item.Key == _CEID)
                {
                    if (m_lstCEID_Enable[index] == false)
                    {
                        res = false;
                    }
                }
                index++;
            }

            return res;
        }

        /// <summary>
        /// Return [0 : OK],  
        /// [1 : Can't send Alarm now],  
        /// [-1 : Exception]
        /// </summary>
        /// <param name="_IsAlarmStart">true: Alarm Start, false: Alarm End</param>
        /// <param name="_ALID">Alarm ID</param>
        /// <param name="_ALTX">Alarm Message</param>
        /// <returns></returns>
        public int SECS_DispatchAlarm(bool _IsAlarmStart, int _ALID, string _ALTX)
        {
            try
            {
                //if (m_SecsState.bIsAlarmSendEable == false)
                if (IsAlarmEnable(_ALID) == false)
                {
                    return 1;
                }

                PrimaryOutFucntion.PrimaryOut_S5F1(_IsAlarmStart, _ALID, _ALTX);
                return 0;
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at SECS_DispatchAlarm()\r\n" + ex.ToString());
                return -1;
            }
        }

        private bool IsAlarmEnable(int _ALID)
        {
            bool res = true;

            for (int i = 1; i <= m_lstALARM.Count; i++)
            {
                if (m_lstALARM[i - 1] == _ALID.ToString())
                {
                    if (m_lstALARM_Enable[i - 1] == false)
                    {
                        res = false;
                    }
                }
            }

            return res;
        }

        #endregion

        #region Private Function

        private void txtSecsLog_Reflash()
        {
            try
            {
                if (txtSecsLog.Text.Length > 1000000)
                {
                    txtSecsLog.Text = "";
                }

                txtSecsLog.Select(txtSecsLog.Text.Length, 0); 
                txtSecsLog.ScrollToCaret();
                txtSecsLog.Refresh();
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at txtSecsLog_Reflash()\r\n" + ex.ToString());
            }
        }

        public void UnLinkAllEventReport()
        {
            try
            {
                foreach (KeyValuePair<int, CEID> item in this.m_lstCEID)
                {
                    item.Value.DeleteAllReport();
                }
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at UnLinkEventReport()\r\n" + ex.ToString());
            }
        }

        private string GetSimpleFormat(FormatConstants format)
        {
            if (format == FormatConstants.wsFormatAscii)
                return "Ascii";
            else if (format == FormatConstants.wsFormatBinary)
                return "Binary";
            else if (format == FormatConstants.wsFormatBoolean)
                return "Boolean";
            else if (format == FormatConstants.wsFormatF4)
                return "F4";
            else if (format == FormatConstants.wsFormatF8)
                return "F8";
            else if (format == FormatConstants.wsFormatI1)
                return "I1";
            else if (format == FormatConstants.wsFormatI2)
                return "I2";
            else if (format == FormatConstants.wsFormatI4)
                return "I4";
            else if (format == FormatConstants.wsFormatI8)
                return "I8";
            else if (format == FormatConstants.wsFormatJis8)
                return "Jis8";
            else if (format == FormatConstants.wsFormatList)
                return "List";
            else if (format == FormatConstants.wsFormatU1)
                return "U1";
            else if (format == FormatConstants.wsFormatU2)
                return "U2";
            else if (format == FormatConstants.wsFormatU4)
                return "U4";
            else if (format == FormatConstants.wsFormatU8)
                return "U8";
            else if (format == FormatConstants.wsFormatVar)
                return "Var";
            else
                return "";
        }

        private void SECS_Initialization()
        {
            sSECS_Initialization_Report = "";
            bool bGetSecsCommSuccess = false;

            #region Get SECS Communication Para

            try
            {

                if (File.Exists(sSECS_SettingPath + "Communication.ini") == true)
                {
                    clsWinAPI.GetPrivateProfileString("Para", "RemoteIP", "", clsWinAPI.GetString, 255, sSECS_SettingPath + "Communication.ini");
                    m_SecsState.sRemoteIP = clsWinAPI.GetString.ToString();

                    clsWinAPI.GetPrivateProfileString("Para", "LocalIP", "", clsWinAPI.GetString, 255, sSECS_SettingPath + "Communication.ini");
                    m_SecsState.sLocalIP = clsWinAPI.GetString.ToString();

                    clsWinAPI.GetPrivateProfileString("Para", "RemotePort", "", clsWinAPI.GetString, 255, sSECS_SettingPath + "Communication.ini");
                    m_SecsState.iRemotePort = int.Parse(clsWinAPI.GetString.ToString());

                    clsWinAPI.GetPrivateProfileString("Para", "LocalPort", "", clsWinAPI.GetString, 255, sSECS_SettingPath + "Communication.ini");
                    m_SecsState.iLocalPort = int.Parse(clsWinAPI.GetString.ToString());

                    clsWinAPI.GetPrivateProfileString("Para", "DeviceID", "", clsWinAPI.GetString, 255, sSECS_SettingPath + "Communication.ini");
                    m_SecsState.iDeviceID = int.Parse(clsWinAPI.GetString.ToString());

                    txtLocalIP.Text = m_SecsState.sLocalIP;
                    txtLocalPort.Text = m_SecsState.iLocalPort.ToString();
                    txtRemoteIP.Text = m_SecsState.sRemoteIP;
                    txtRemotePort.Text = m_SecsState.iRemotePort.ToString();
                    txtDeviceID.Text = m_SecsState.iDeviceID.ToString();
                    bGetSecsCommSuccess = true;
                }
                else
                {
                    sSECS_Initialization_Report += "Communication.ini File Not Exists, Can't Open SECS Port\r\n";
                    bGetSecsCommSuccess = false;
                }

            }
            catch (Exception ex)
            {
                sSECS_Initialization_Report += "Get SECS Communication Para Fail.\r\n" + ex.ToString();
            }
            #endregion

            #region SECS/GEM Open Port

            if (bGetSecsCommSuccess)
            {
                try
                {
                    secsEAP.PortType = PortTypeConstant._PortTypeHSMS;
                    secsEAP.IPAddressRemote = m_SecsState.sRemoteIP;
                    secsEAP.IPAddressLocal = m_SecsState.sLocalIP;
                    secsEAP.IPPortLocal = m_SecsState.iLocalPort;
                    secsEAP.IPPortRemote = m_SecsState.iRemotePort;
                    secsEAP.DefaultDeviceID = m_SecsState.iDeviceID;
                    secsEAP.LinkTestTimer = 60;
                    secsEAP.HSMST3 = 45;
                    secsEAP.HSMST5 = 10;
                    secsEAP.HSMST6 = 5;
                    secsEAP.HSMST7 = 10;
                    secsEAP.HSMST8 = 5;
                    secsEAP.ConnectionMode = ConnectionModeConstant._ConnectionModePassive;
                    secsEAP.SecsHost = false;
                    secsEAP.IgnoreSystemBytes = false;
                    secsEAP.AcceptDuplicateBlocks = true;
                    secsEAP.Interleave = true;
                    secsEAP.AutoDevice = true;
                    secsEAP.MonitorEnabled = false;
                    secsEAP.MultipleOpen = true;
                    secsEAP.SimulateMode = false;
                    secsEAP.SecsHost = false;
                    GEM.BuildGemLibrary(secsEAP);
                    secsEAP.ClosePort();
                    secsEAP.OpenPort();
                    m_SecsState.bIsPortOpen = true;
                }
                catch (Exception ex)
                {
                    sSECS_Initialization_Report += "Open SECS Port Exception.\r\n" + ex.ToString() + "\r\n";
                }

            }
            #endregion

            #region Create SVID List

            try
            {
                m_lstSVID.Clear();
                if (File.Exists(sSECS_SettingPath + "SVID.txt") == true)
                {
                    // Read txt File
                    string sBlock = "";
                    StreamReader sr = new StreamReader(sSECS_SettingPath + "SVID.txt");
                    sBlock = sr.ReadToEnd();
                    sr.Close();

                    // Get Ever SVID
                    string[] split_Line = { "\r\n" };
                    string[] split_Unit = { ";" };
                    string[] everLine = sBlock.Split(split_Line, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < everLine.Length; i++)
                    {
                        int UnitCount = everLine[i].Split(split_Unit, StringSplitOptions.None).Length;
                        if (UnitCount == 2)
                        {
                            int ID = Convert.ToInt32(everLine[i].Split(split_Unit, StringSplitOptions.None)[0]);
                            string Name = everLine[i].Split(split_Unit, StringSplitOptions.None)[1];
                            SVID svid = new SVID(ID, Name);
                            m_lstSVID.Add(ID, svid);
                        }
                        else
                        {
                            sSECS_Initialization_Report += "SVID.txt Format Fail at [" + split_Line[i] + "]\r\n";
                        }
                    }
                }
                else
                {
                    sSECS_Initialization_Report += "SVID.txt File Not Exists, Can't Create SVID List\r\n";
                }
            }
            catch (Exception ex)
            {
                sSECS_Initialization_Report += "Create SVID Exception.\r\n" + ex.ToString() + "\r\n";
            }

            #endregion

            #region Create DVID List
            
            try
            {
                m_lstDVID.Clear();
                if (File.Exists(sSECS_SettingPath + "DVID.txt") == true)
                {
                    // Read txt File
                    string sBlock = "";
                    StreamReader sr = new StreamReader(sSECS_SettingPath + "DVID.txt");
                    sBlock = sr.ReadToEnd();
                    sr.Close();

                    // Get Ever DVID
                    string[] split_Line = { "\r\n" };
                    string[] split_Unit = { ";" };
                    string[] everLine = sBlock.Split(split_Line, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < everLine.Length; i++)
                    {
                        int UnitCount = everLine[i].Split(split_Unit, StringSplitOptions.None).Length;
                        if (UnitCount == 2)
                        {
                            int ID = Convert.ToInt32(everLine[i].Split(split_Unit, StringSplitOptions.None)[0]);
                            string Name = everLine[i].Split(split_Unit, StringSplitOptions.None)[1];
                            DVID dvid = new DVID(ID, Name);
                            m_lstDVID.Add(ID, dvid);
                        }
                        else
                        {
                            sSECS_Initialization_Report += "DVID.txt Format Fail at [" + split_Line[i] + "]\r\n";
                        }
                    }
                }
                else
                {
                    sSECS_Initialization_Report += "DVID.txt File Not Exists, Can't Create DVID List\r\n";
                }
            }
            catch (Exception ex)
            {
                sSECS_Initialization_Report += "Create DVID Exception.\r\n" + ex.ToString() + "\r\n";
            }

            #endregion

            #region Create ECID List

            try
            {
                m_lstECID.Clear();
                if (File.Exists(sSECS_SettingPath + "ECID.txt") == true)
                {
                    // Read txt File
                    string sBlock = "";
                    StreamReader sr = new StreamReader(sSECS_SettingPath + "ECID.txt");
                    sBlock = sr.ReadToEnd();
                    sr.Close();

                    // Get Ever ECID
                    string[] split_Line = { "\r\n" };
                    string[] split_Unit = { ";" };
                    string[] everLine = sBlock.Split(split_Line, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < everLine.Length; i++)
                    {
                        int UnitCount = everLine[i].Split(split_Unit, StringSplitOptions.None).Length;
                        if (UnitCount == 5)
                        {
                            int ID = Convert.ToInt32(everLine[i].Split(split_Unit, StringSplitOptions.None)[0]);
                            string Name = everLine[i].Split(split_Unit, StringSplitOptions.None)[1];
                            string MinValue = everLine[i].Split(split_Unit, StringSplitOptions.None)[2];
                            string MaxValue = everLine[i].Split(split_Unit, StringSplitOptions.None)[3];
                            string Units = everLine[i].Split(split_Unit, StringSplitOptions.None)[4];

                            ECID ecid = new ECID(ID, Name);

                            m_lstECID.Add(ID, ecid);

                            //Set ECID Max,Min,Def Value
                            m_lstECID[ID].MaxValue = int.Parse(MaxValue);
                            m_lstECID[ID].MinValue = int.Parse(MinValue);
                            m_lstECID[ID].DefValue = "0";
                            m_lstECID[ID].Units = Units;
                        }
                        else
                        {
                            sSECS_Initialization_Report += "ECID.txt Format Fail at [" + split_Line[i] + "]\r\n";
                        }
                    }
                }
                else
                {
                    sSECS_Initialization_Report += "ECID.txt File Not Exists, Can't Create ECID List\r\n";
                }
            }
            catch (Exception ex)
            {
                sSECS_Initialization_Report += "Create ECID Exception.\r\n" + ex.ToString() + "\r\n";
            }

            #endregion

            #region Create Alarm List
            m_lstALARM.Clear();
            foreach (clsEnum.enuAlarm Alarm in Enum.GetValues(typeof(clsEnum.enuAlarm)))
            {

                try
                {
                    string AlarmCode = ((int)Alarm).ToString();
                    m_lstALARM.Add(AlarmCode);
                }
                catch (Exception e)
                {

                }
            }

            if (ucParameter.GetValueString(clsEnum.enuPmtName.Sys_SecsAlarmEnable).Length != m_lstALARM.Count)
            {
                for (int i = 1; i <= m_lstALARM.Count; i++)
                {
                    m_lstALARM_Enable.Add(true);
                }
            }
            else
            {
                for (int i = 0; i < m_lstALARM.Count; i++)
                {
                    if (ucParameter.GetValueString(clsEnum.enuPmtName.Sys_SecsAlarmEnable).Substring(i, 1) == "0")
                    {
                        m_lstALARM_Enable.Add(false);
                    }
                    else
                    {
                        m_lstALARM_Enable.Add(true);
                    }
                }
            }
            #endregion

            #region Create RPTID List
            try
            {
                m_lstRPTID.Clear();
                if (File.Exists(sSECS_SettingPath + "RPTID.txt") == true)
                {
                    // Read txt File
                    string sBlock = "";
                    StreamReader sr = new StreamReader(sSECS_SettingPath + "RPTID.txt");
                    sBlock = sr.ReadToEnd();
                    sr.Close();

                    // Get Ever RPTID
                    string[] split_Line = { "\r\n" };
                    string[] split_Unit = { ";" };
                    string[] split_dot = { "," };
                    string[] everLine = sBlock.Split(split_Line, StringSplitOptions.RemoveEmptyEntries);


                    for (int i = 0; i < everLine.Length; i++)
                    {
                        int ID = int.Parse(everLine[i].Split(split_Unit, StringSplitOptions.RemoveEmptyEntries)[0]);
                        RPTID RptId = new RPTID(ID);

                        string[] VIDs = everLine[i].Split(split_Unit, StringSplitOptions.None)[1].Split(split_dot, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < VIDs.Length; j++)
                        {
                            RptId.lstVID.Add(int.Parse(VIDs[j]));
                        }
                        m_lstRPTID.Add(ID, RptId);
                    }
                }
                else
                {
                    sSECS_Initialization_Report += "RPTID.txt File Not Exists, Can't Create RPTID List\r\n";
                }
            }
            catch (Exception ex)
            {
                sSECS_Initialization_Report += "Create RPTID Exception.\r\n" + ex.ToString() + "\r\n";
            }
            #endregion

            #region Create CEID List
            try
            {
                m_lstCEID.Clear();
                if (File.Exists(sSECS_SettingPath + "CEID.txt") == true)
                {
                    // Read txt File
                    string sBlock = "";
                    StreamReader sr = new StreamReader(sSECS_SettingPath + "CEID.txt");
                    sBlock = sr.ReadToEnd();
                    sr.Close();

                    // Get Ever CEID
                    string[] split_Line = { "\r\n" };
                    string[] split_Unit = { ";" };
                    string[] split_dot = { "," };
                    string[] everLine = sBlock.Split(split_Line, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < everLine.Length; i++)
                    {
                        int ID = int.Parse(everLine[i].Split(split_Unit, StringSplitOptions.None)[0]);
                        string NAME = everLine[i].Split(split_Unit, StringSplitOptions.None)[1];
                        string RPID = everLine[i].Split(split_Unit, StringSplitOptions.None)[2];
                        CEID ceid = new CEID(ID, NAME);

                        string[] everRPTID = RPID.Split(split_dot, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < everRPTID.Length; j++)
                        {
                            ceid.lstLinkReport.Add(int.Parse(everRPTID[j]));
                        }

                        m_lstCEID.Add(ID, ceid);
                    }

                }
                else
                {
                    sSECS_Initialization_Report += "CEID.txt File Not Exists, Can't Create CEID List\r\n";
                }
            }
            catch (Exception ex)
            {
                sSECS_Initialization_Report += "Create CEID Exception.\r\n" + ex.ToString() + "\r\n";
            }

            //CEID Enable/Disable List Create
            if (ucParameter.GetValueString(clsEnum.enuPmtName.Sys_SecsEventEnable).Length != m_lstCEID.Count)
            {
                for (int i = 1; i <= m_lstCEID.Count; i++)
                {
                    m_lstCEID_Enable.Add(true);
                }
            }
            else
            {
                for (int i = 0; i < m_lstCEID.Count; i++)
                {
                    if (ucParameter.GetValueString(clsEnum.enuPmtName.Sys_SecsEventEnable).Substring(i, 1) == "0")
                    {
                        m_lstCEID_Enable.Add(false);
                    }
                    else
                    {
                        m_lstCEID_Enable.Add(true);
                    }
                }
            }
            #endregion

            #region Create PPBODY List
            try
            {
                m_lstPPBody.Clear();
                if (File.Exists(sSECS_SettingPath + "PPBODY.txt") == true)
                {
                    // Read txt File
                    string sBlock = "";
                    StreamReader sr = new StreamReader(sSECS_SettingPath + "PPBODY.txt");
                    sBlock = sr.ReadToEnd();
                    sr.Close();

                    // Get Ever PPBODY
                    string[] split_Line = { "\r\n" };
                    string[] split_Unit = { ";" };
                    string[] everLine = sBlock.Split(split_Line, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < everLine.Length; i++)
                    {
                        int UnitCount = everLine[i].Split(split_Unit, StringSplitOptions.None).Length;
                        if (UnitCount == 6)
                        {
                            int ID = Convert.ToInt32(everLine[i].Split(split_Unit, StringSplitOptions.None)[0]);
                            string Name = everLine[i].Split(split_Unit, StringSplitOptions.None)[1];
                            m_lstPPBody.Add(everLine[i]);
                        }
                        else
                        {
                            sSECS_Initialization_Report += "PPBODY.txt Format Fail at [" + everLine[i] + "]\r\n";
                        }
                    }
                }
                else
                {
                    sSECS_Initialization_Report += "PPBODY.txt File Not Exists, Can't Create PPBODY List\r\n";
                }
            }
            catch (Exception ex)
            {
                sSECS_Initialization_Report += "Create PPBODY Exception.\r\n" + ex.ToString() + "\r\n";
            }
            #endregion

            #region Setting Parameter

            try
            {
                if (File.Exists(sSECS_SettingPath + "Setting.ini") == true)
                {
                    clsWinAPI.GetPrivateProfileString("Para", "MDLN", "", clsWinAPI.GetString, 255, sSECS_SettingPath + "Setting.ini");
                    sMDLN = clsWinAPI.GetString.ToString();

                    clsWinAPI.GetPrivateProfileString("Para", "EQPID", "", clsWinAPI.GetString, 255, sSECS_SettingPath + "Setting.ini");
                    sEQPID = clsWinAPI.GetString.ToString();  
                }
            }
            catch (Exception ex)
            {
                sSECS_Initialization_Report += "Get SECS Communication Para Fail.\r\n" + ex.ToString();
            }

            #endregion

            #region Update Static SVID

            lblVersion.Text = sVersion;

            #endregion

            UpdateDGV(dgvCEID);
            UpdateDGV(dgvPPID);
            UpdateDGV(dgvRPTID);
            UpdateDGV(dgvSVID);
            UpdateDGV(dgvDVID);
            UpdateDGV(dgvECID);

            if (sSECS_Initialization_Report == "")
            {
                sSECS_Initialization_Report = "SECS Initial Success.";
            }
            txtSecsLog.Text = sSECS_Initialization_Report;

            // 使用者元件初始化
            lvwMessage.Columns.Add("H / E", 100); //新增欄位，寬度為100
            lvwMessage.Columns.Add("Message", 700); //新增欄位，寬度為700
            lvwMessage.Columns.Add("Time", 200); //新增欄位，寬度為200

            //依內容自動換行
            dgvSVID.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //自動調欄寬
            dgvSVID.AutoResizeColumns();
            //自動調欄高
            dgvSVID.AutoResizeRows();
        }

        public delegate void dlgWriteSecsLog(string type, bool bFromCIM, string title, SecsTransaction trans);
        public void WriteSecsLog(string type, bool bFromCIM, string title, SecsTransaction trans)
        {
            if (this.InvokeRequired)
            {
                dlgWriteSecsLog t = new dlgWriteSecsLog(WriteSecsLog);
                this.BeginInvoke(t, type, bFromCIM, title, trans);
            }
            else
            {
                try
                {
                    string log = "";
                    if (type == "P")
                    {
                        #region Primary
                        log = title + "\r\n";
                        if (trans.Primary.Length == 0)
                        {
                            clsLog.Log("SECSHost", log);
                        }
                        else
                        {
                            if (trans.Primary.Item[1].Format == FormatConstants.wsFormatList)
                            {
                                log += "   <" + trans.Primary.Item[1].Name + "\r\n";
                                for (int L1 = 1; L1 <= trans.Primary.Item[1].Length; L1++)
                                {
                                    #region L1
                                    if (trans.Primary.Item[1].Item[L1].Format != FormatConstants.wsFormatList)
                                    {
                                        if (trans.Primary.Item[1].Item[L1].Format == FormatConstants.wsFormatBinary && bFromCIM)
                                        {
                                            if (trans.Name == "S7F3")
                                            {
                                                string value = Convert.ToString(trans.Primary.Item[1].Item[L1].Value);

                                                // ----------------------轉換字串為Byte()----------------------
                                                string sValue = "";
                                                byte[] aValue = new byte[value.Length];

                                                for (var i = 0; i <= value.Length - 1; i++)
                                                {
                                                    aValue[i] = System.Convert.ToByte(Asc(value.Substring(i, 1)));
                                                }

                                                for (var i = 0; i <= aValue.Length - 1; i++)
                                                {
                                                    if (sValue == "")
                                                    {
                                                        sValue = System.Convert.ToString(aValue[i], 16);
                                                    }
                                                    else
                                                    {
                                                        sValue = sValue + " " + System.Convert.ToString(aValue[i], 16);
                                                    }
                                                }
                                                //------------------------------------------------------------

                                                log += "      <" + trans.Primary.Item[1].Item[L1].Name + " = " + sValue + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Format) + "\r\n";
                                            }
                                            else
                                            {
                                                string value = Convert.ToString(trans.Primary.Item[1].Item[L1].Value);
                                                byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);

                                                if (BinValue.Length > 0)
                                                    log += "      <" + trans.Primary.Item[1].Item[L1].Name + " = " + BinValue[0].ToString() + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Format) + "\r\n";
                                                else
                                                    log += "      <" + trans.Primary.Item[1].Item[L1].Name + " = " + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Format) + "\r\n";
                                            }
                                        }
                                        else
                                        {
                                            log += "      <" + trans.Primary.Item[1].Item[L1].Name + " = " + trans.Primary.Item[1].Item[L1].Value + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Format) + "\r\n";
                                        }
                                    }
                                    else
                                    {
                                        log += "      <" + trans.Primary.Item[1].Item[L1].Name + "\r\n";
                                        for (int L2 = 1; L2 <= trans.Primary.Item[1].Item[L1].Length; L2++)
                                        {
                                            #region L2
                                            if (trans.Primary.Item[1].Item[L1].Item[L2].Format != FormatConstants.wsFormatList)
                                            {
                                                if (trans.Primary.Item[1].Item[L1].Item[L2].Format == FormatConstants.wsFormatBinary && bFromCIM)
                                                {
                                                    string value = Convert.ToString(trans.Primary.Item[1].Item[L1].Item[L2].Value);
                                                    byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);
                                                    if (BinValue.Length > 0)
                                                        log += "         <" + trans.Primary.Item[1].Item[L1].Item[L2].Name + " = " + BinValue[0].ToString() + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Format) + "\r\n";
                                                    else
                                                        log += "         <" + trans.Primary.Item[1].Item[L1].Item[L2].Name + " = " + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Format) + "\r\n";
                                                }
                                                else
                                                {
                                                    log += "         <" + trans.Primary.Item[1].Item[L1].Item[L2].Name + " = " + trans.Primary.Item[1].Item[L1].Item[L2].Value + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Format) + "\r\n";
                                                }
                                            }
                                            else
                                            {
                                                log += "         <" + trans.Primary.Item[1].Item[L1].Item[L2].Name + "\r\n";
                                                for (int L3 = 1; L3 <= trans.Primary.Item[1].Item[L1].Item[L2].Length; L3++)
                                                {
                                                    #region L3
                                                    if (trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Format != FormatConstants.wsFormatList)
                                                    {
                                                        if (trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Format == FormatConstants.wsFormatBinary && bFromCIM)
                                                        {
                                                            string value = Convert.ToString(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Value);
                                                            byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);
                                                            if (BinValue.Length > 0)
                                                                log += "            <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Name + " = " + BinValue[0].ToString() + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Format) + "\r\n";
                                                            else
                                                                log += "            <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Name + " = " + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Format) + "\r\n";
                                                        }
                                                        else
                                                        {
                                                            log += "            <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Name + " = " + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Value + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Format) + "\r\n";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        log += "            <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Name + "\r\n";
                                                        for (int L4 = 1; L4 <= trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Length; L4++)
                                                        {
                                                            #region L4
                                                            if (trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Format != FormatConstants.wsFormatList)
                                                            {
                                                                if (trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Format == FormatConstants.wsFormatBinary && bFromCIM)
                                                                {
                                                                    string value = Convert.ToString(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Value);
                                                                    byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);
                                                                    if (BinValue.Length > 0)
                                                                        log += "               <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Name + " = " + BinValue[0].ToString() + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Format) + "\r\n";
                                                                    else
                                                                        log += "               <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Name + " = " + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Format) + "\r\n";
                                                                }
                                                                else
                                                                {
                                                                    log += "               <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Name + " = " + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Value + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Format) + "\r\n";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                log += "               <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Name + "\r\n";
                                                                for (int L5 = 1; L5 <= trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Length; L5++)
                                                                {
                                                                    #region L5
                                                                    if (trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Format != FormatConstants.wsFormatList)
                                                                    {
                                                                        if (trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Format == FormatConstants.wsFormatBinary && bFromCIM)
                                                                        {
                                                                            string value = Convert.ToString(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Value);
                                                                            byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);
                                                                            if (BinValue.Length > 0)
                                                                                log += "                  <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Name + " = " + BinValue[0].ToString() + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Format) + "\r\n";
                                                                            else
                                                                                log += "                  <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Name + " = " + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Format) + "\r\n";
                                                                        }
                                                                        else
                                                                        {
                                                                            log += "                  <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Name + " = " + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Value + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Format) + "\r\n";
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        log += "                  <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Name + "\r\n";
                                                                        for (int L6 = 1; L6 <= trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Length; L6++)
                                                                        {
                                                                            #region L6
                                                                            if (trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Item[L6].Format == FormatConstants.wsFormatBinary && bFromCIM)
                                                                            {
                                                                                string value = Convert.ToString(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Item[L6].Value);
                                                                                byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);
                                                                                if (BinValue.Length > 0)
                                                                                    log += "                     <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Item[L6].Name + " = " + BinValue[0].ToString() + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Item[L6].Format) + "\r\n";
                                                                                else
                                                                                    log += "                     <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Item[L6].Name + " = " + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Item[L6].Format) + "\r\n";
                                                                            }
                                                                            else
                                                                            {
                                                                                log += "                     <" + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Item[L6].Name + " = " + trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Item[L6].Value + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Item[L6].Format) + "\r\n";
                                                                            }
                                                                            #endregion
                                                                        }
                                                                    }
                                                                    #endregion
                                                                }
                                                                log += "               >\r\n";
                                                            }
                                                            #endregion
                                                        }
                                                        log += "            >\r\n";
                                                    }
                                                    #endregion
                                                }
                                                log += "         >\r\n";
                                            }
                                            #endregion
                                        }
                                        log += "      >\r\n";
                                    }
                                    #endregion
                                }
                                log += "   >\r\n";
                            }
                            else if (bFromCIM && trans.Primary.Item[1].Format == FormatConstants.wsFormatBinary)
                            {
                                string value = Convert.ToString(trans.Primary.Item[1].Value);
                                byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);
                                if (BinValue.Length > 0)
                                    log += "   <" + trans.Primary.Item[1].Name + " = " + BinValue[0].ToString() + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Format) + "\r\n";
                                else
                                    log += "   <" + trans.Primary.Item[1].Name + " = " + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Format) + "\r\n";
                            }
                            else
                            {
                                log += "   <" + trans.Primary.Item[1].Name + " = " + trans.Primary.Item[1].Value + ">  '" + GetSimpleFormat(trans.Primary.Item[1].Format) + "\r\n";
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Secondary
                        log = title + "\r\n";

                        if (trans.Secondary.Length == 0)
                        {
                            clsLog.Log("SECSHost", log);
                        }
                        else
                        {
                            if (trans.Secondary.Item[1].Format == FormatConstants.wsFormatList)
                            {
                                log += "   <" + trans.Secondary.Item[1].Name + "\r\n";
                                for (int L1 = 1; L1 <= trans.Secondary.Item[1].Length; L1++)
                                {
                                    #region L1
                                    if (trans.Secondary.Item[1].Item[L1].Format != FormatConstants.wsFormatList)
                                    {
                                        if (trans.Secondary.Item[1].Item[L1].Format == FormatConstants.wsFormatBinary && bFromCIM)
                                        {
                                            if (trans.Name == "S7F5")
                                            {
                                                string value = Convert.ToString(trans.Secondary.Item[1].Item[L1].Value);

                                                // ----------------------轉換字串為Byte()----------------------
                                                string sValue = "";
                                                byte[] aValue = new byte[value.Length];

                                                for (var i = 0; i <= value.Length - 1; i++)
                                                {
                                                    aValue[i] = System.Convert.ToByte(Asc(value.Substring(i, 1)));
                                                }

                                                for (var i = 0; i <= aValue.Length - 1; i++)
                                                {
                                                    if (sValue == "")
                                                    {
                                                        sValue = System.Convert.ToString(aValue[i], 16);
                                                    }
                                                    else
                                                    {
                                                        sValue = sValue + " " + System.Convert.ToString(aValue[i], 16);
                                                    }
                                                }
                                                //------------------------------------------------------------

                                                log += "      <" + trans.Secondary.Item[1].Item[L1].Name + " = " + sValue + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Format) + "\r\n";

                                            }
                                            else
                                            {
                                                string value = Convert.ToString(trans.Secondary.Item[1].Item[L1].Value);
                                                byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);
                                                if (BinValue.Length > 0)
                                                    log += "      <" + trans.Secondary.Item[1].Item[L1].Name + " = " + BinValue[0].ToString() + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Format) + "\r\n";
                                                else
                                                    log += "      <" + trans.Secondary.Item[1].Item[L1].Name + " = " + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Format) + "\r\n";
                                            }
                                        }
                                        else
                                        {
                                            log += "      <" + trans.Secondary.Item[1].Item[L1].Name + " = " + trans.Secondary.Item[1].Item[L1].Value + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Format) + "\r\n";
                                        }
                                    }
                                    else
                                    {
                                        log += "      <" + trans.Secondary.Item[1].Item[L1].Name + "\r\n";
                                        for (int L2 = 1; L2 <= trans.Secondary.Item[1].Item[L1].Length; L2++)
                                        {
                                            #region L2
                                            if (trans.Secondary.Item[1].Item[L1].Item[L2].Format != FormatConstants.wsFormatList)
                                            {
                                                if (trans.Secondary.Item[1].Item[L1].Item[L2].Format == FormatConstants.wsFormatBinary && bFromCIM)
                                                {
                                                    string value = Convert.ToString(trans.Secondary.Item[1].Item[L1].Item[L2].Value);
                                                    byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);
                                                    if (BinValue.Length > 0)
                                                        log += "         <" + trans.Secondary.Item[1].Item[L1].Item[L2].Name + " = " + BinValue[0].ToString() + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Item[L2].Format) + "\r\n";
                                                    else
                                                        log += "         <" + trans.Secondary.Item[1].Item[L1].Item[L2].Name + " = " + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Item[L2].Format) + "\r\n";
                                                }
                                                else
                                                {
                                                    log += "         <" + trans.Secondary.Item[1].Item[L1].Item[L2].Name + " = " + trans.Secondary.Item[1].Item[L1].Item[L2].Value + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Item[L2].Format) + "\r\n";
                                                }
                                            }
                                            else
                                            {
                                                log += "         <" + trans.Secondary.Item[1].Item[L1].Item[L2].Name + "\r\n";
                                                for (int L3 = 1; L3 <= trans.Secondary.Item[1].Item[L1].Item[L2].Length; L3++)
                                                {
                                                    #region L3
                                                    if (trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Format != FormatConstants.wsFormatList)
                                                    {
                                                        if (trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Format == FormatConstants.wsFormatBinary && bFromCIM)
                                                        {
                                                            string value = Convert.ToString(trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Value);
                                                            byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);
                                                            if (BinValue.Length > 0)
                                                                log += "            <" + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Name + " = " + BinValue[0].ToString() + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Format) + "\r\n";
                                                            else
                                                                log += "            <" + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Name + " = " + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Format) + "\r\n";
                                                        }
                                                        else
                                                        {
                                                            log += "            <" + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Name + " = " + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Value + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Format) + "\r\n";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        log += "            <" + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Name + "\r\n";
                                                        for (int L4 = 1; L4 <= trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Length; L4++)
                                                        {
                                                            #region L4
                                                            if (trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Format != FormatConstants.wsFormatList)
                                                            {
                                                                if (trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Format == FormatConstants.wsFormatBinary && bFromCIM)
                                                                {
                                                                    string value = Convert.ToString(trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Value);
                                                                    byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);
                                                                    if (BinValue.Length > 0)
                                                                        log += "               <" + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Name + " = " + BinValue[0].ToString() + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Format) + "\r\n";
                                                                    else
                                                                        log += "               <" + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Name + " = " + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Format) + "\r\n";
                                                                }
                                                                else
                                                                {
                                                                    log += "               <" + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Name + " = " + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Value + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Format) + "\r\n";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                log += "               <" + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Name + "\r\n";
                                                                for (int L5 = 1; L5 <= trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Length; L5++)
                                                                {
                                                                    #region L5
                                                                    if (trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Format == FormatConstants.wsFormatBinary && bFromCIM)
                                                                    {
                                                                        string value = Convert.ToString(trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Value);
                                                                        byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);
                                                                        if (BinValue.Length > 0)
                                                                            log += "                  <" + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Name + " = " + BinValue[0].ToString() + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Format) + "\r\n";
                                                                        else
                                                                            log += "                  <" + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Name + " = " + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Format) + "\r\n";
                                                                    }
                                                                    else
                                                                    {
                                                                        log += "                  <" + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Name + " = " + trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Value + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Item[L1].Item[L2].Item[L3].Item[L4].Item[L5].Format) + "\r\n";
                                                                    }
                                                                    #endregion
                                                                }
                                                                log += "               >\r\n";
                                                            }
                                                            #endregion
                                                        }
                                                        log += "            >\r\n";
                                                    }
                                                    #endregion
                                                }
                                                log += "         >\r\n";
                                            }
                                            #endregion
                                        }
                                        log += "      >\r\n";
                                    }
                                    #endregion
                                }
                                log += "   >\r\n";
                            }
                            else if (bFromCIM && trans.Secondary.Item[1].Format == FormatConstants.wsFormatBinary)
                            {
                                string value = Convert.ToString(trans.Secondary.Item[1].Value);
                                byte[] BinValue = System.Text.Encoding.Default.GetBytes(value);
                                if (BinValue.Length > 0)
                                    log += "   <" + trans.Secondary.Item[1].Name + " = " + BinValue[0].ToString() + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Format) + "\r\n";
                                else
                                    log += "   <" + trans.Secondary.Item[1].Name + " = " + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Format) + "\r\n";
                            }
                            else
                            {
                                log += "   <" + trans.Secondary.Item[1].Name + " = " + trans.Secondary.Item[1].Value + ">  '" + GetSimpleFormat(trans.Secondary.Item[1].Format) + "\r\n";
                            }
                        }
                        #endregion
                    }

                    clsLog.Log("SECSHost", log);
                    txtSecsLog.Text += log + "\r\n";
                }
                catch (Exception ex)
                {
                    m_TraceLog.WriteLog("Exception at WriteSecsLog() which Title = " + title + "\r\n" + ex.ToString());
                }
            }
        }

        private static int Asc(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new Exception("Character is not valid.");
            }

        }

        private string HexStr2ASCII(string hex_str)
        {
            string result = "";
            string tmp;

            for (int i = 0; i < hex_str.Length; i += 2)
            {
                tmp = hex_str.Substring(i, 2);
                result += Convert.ToChar(Convert.ToUInt32(tmp, 16));
            }
            return result;
        }

        public void UpdateDGV(DataGridView dgv)
        {
            try
            {
                if (dgv.InvokeRequired)
                {
                    dlgUpdateDGV t = new dlgUpdateDGV(UpdateDGV);
                    dgv.Invoke(t, dgv);
                }
                else
                {
                    switch (dgv.Name)
                    {
                        case "dgvSVID":
                            dgv.Rows.Clear();
                            foreach (KeyValuePair<int, SVID> item in this.m_lstSVID)
                            {
                                dgv.Rows.Add(item.Value.ID.ToString(), item.Value.Name, item.Value.Value);
                            }
                            break;

                        case "dgvECID":
                            dgv.Rows.Clear();
                            foreach (KeyValuePair<int, ECID> item in this.m_lstECID)
                            {
                                dgv.Rows.Add(item.Value.ID.ToString(), item.Value.Name, item.Value.Value);
                            }
                            break;

                        case "dgvDVID":
                            dgv.Rows.Clear();
                            foreach (KeyValuePair<int, DVID> item in this.m_lstDVID)
                            {
                                dgv.Rows.Add(item.Value.ID.ToString(), item.Value.Name, item.Value.Value);
                            }
                            break;

                        case "dgvCEID":
                            dgv.Rows.Clear();
                            foreach (KeyValuePair<int, CEID> item in this.m_lstCEID)
                            {
                                string RPT = "";
                                int cnt = 0;
                                foreach (int obj in item.Value.lstLinkReport)
                                {
                                    RPT += obj.ToString();

                                    if (cnt + 1 < item.Value.lstLinkReport.Count)
                                        RPT += ",";

                                    cnt++;
                                }
                                dgv.Rows.Add(item.Value.ID.ToString(), item.Value.Name, RPT);
                            }
                            break;

                        case "dgvRPTID":
                            dgv.Rows.Clear();
                            foreach (KeyValuePair<int, RPTID> item in this.m_lstRPTID)
                            {
                                string sVID = "";
                                for (int i = 0; i < item.Value.lstVID.Count; i++)
                                {
                                    sVID += item.Value.lstVID[i].ToString();
                                    if (i + 1 < item.Value.lstVID.Count)
                                        sVID += ",";
                                }
                                dgv.Rows.Add(item.Value.ID.ToString(), sVID);
                            }
                            break;

                        case "dgvPPID":
                            dgv.Rows.Clear();
                            foreach (string item in this.m_lstPPID)
                            {
                                dgv.Rows.Add(item);
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at UpdateDGV()\r\n" + ex.ToString());
            }
        }

        public void TerminalDisplayTxt(string text)
        {
            try
            {
                if (this.txtSecsLog.InvokeRequired)
                {
                    dlgTerminalDisp t = new dlgTerminalDisp(TerminalDisplayTxt);
                    this.txtSecsLog.Invoke(t);
                }
                else
                {
                    this.txtSecsLog.Text += "***********************Terminal Send Msg***********************\r\n";
                    this.txtSecsLog.Text += text + "\r\n";
                    this.txtSecsLog.Text += "****************************End  Msg****************************\r\n";
                }
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at TerminalDisplayTxt()\r\n" + ex.ToString());
            }

        }

        public void DefinedReportToTxtFile()
        {
            try
            {
                StreamWriter sw = new StreamWriter(sSECS_SettingPath + "RPTID.txt");
                sw.Write("");//Clear all Content

                foreach (KeyValuePair<int, RPTID> item in this.m_lstRPTID)
                {
                    sw.Write(item.Value.ID.ToString() + ";");
                    for (int i = 0; i < item.Value.lstVID.Count; i++)
                    {
                        sw.Write(item.Value.lstVID[i].ToString());
                        if (i + 1 < item.Value.lstVID.Count)
                            sw.Write(",");
                    }
                    sw.Write("\r\n");
                }

                sw.Close();
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at function DefinedReportToTxtFile()\r\n" + ex.ToString() + "\r\n");
            }
        }

        public void LinkReportToTxtFile()
        {
            try
            {
                StreamWriter sw = new StreamWriter(sSECS_SettingPath + "CEID.txt");
                sw.Write("");//Clear all Content

                foreach (KeyValuePair<int, CEID> item in this.m_lstCEID)
                {
                    int cnt = 0;
                    string RPT = "";
                    foreach (int obj in item.Value.lstLinkReport)
                    {
                        RPT += obj.ToString();

                        if (cnt + 1 < item.Value.lstLinkReport.Count)
                            RPT += ",";

                        cnt++;
                    }
                    sw.WriteLine(item.Value.ID.ToString() + ";" + item.Value.Name + ";" + RPT);
                }

                sw.Close();
            }
            catch (Exception ex)
            {
                m_TraceLog.WriteLog("Exception at function LinkReportToTxtFile()\r\n" + ex.ToString() + "\r\n");
            }
        }

        public int Get_MHEAD_Data(SecsEvents_PrimaryInEvent e, ref byte[] Value)
        {
            int iReturncode = 0;

            byte[] bMHead = new byte[10];
            long SB;
            int iDeviceID = e.trans.DeviceID;
            int iBlockNum = e.trans.Primary.Length;
            int iSx = e.trans.Primary.Stream;
            int iFy = e.trans.Primary.Function;
            int RBit = 0;     // Host->EQ Direction
            int WBit = System.Convert.ToInt32(e.trans.ReplyExpected ? 1 : 0);
            int EBit = 0;
            int i;

            bMHead[0] = System.Convert.ToByte(128 * RBit + iDeviceID / 256);
            bMHead[1] = System.Convert.ToByte(iDeviceID % 256);
            bMHead[2] = System.Convert.ToByte(128 * WBit + iSx);
            bMHead[3] = System.Convert.ToByte(iFy);
            bMHead[4] = System.Convert.ToByte(128 * EBit + iBlockNum / 256);
            bMHead[5] = System.Convert.ToByte(iBlockNum % 256);

            SB = e.trans.SystemBytes;
            bMHead[6] = 0; // CByte(SB \ CLng(2 ^ 32))
            bMHead[7] = 0; // CByte(SB \ CLng(2 ^ 16))
            bMHead[8] = 0; // CByte(SB \ CLng(2 ^ 8))
            bMHead[9] = 0; // CByte(SB Mod CLng(2 ^ 8))

            Value = bMHead;

            return iReturncode;
        }

        public static bool AutoChangeRecipe(string RecipeName, bool ReportEvent)
        {
            bool res = false;
            string FileName = "";
            FileInfo fileInfo = new FileInfo(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe));

            if (fileInfo.Exists || fileInfo.Directory.Exists)
            {
                FileName = fileInfo.DirectoryName + "\\" + RecipeName + ".ini";
                res = ucParameter.SetFilePath(clsEnum.enuPmtType.Recipe.ToString(), FileName);
                if (res && ReportEvent)
                {
                    ucHost_SECS.GetSingleton().SECS_UpdateSVID(1007, RecipeName);
                    ucHost_SECS.GetSingleton().SECS_DispatchEvent((int)clsEnum.eSECS_CEID.Changed_Recipe_Complete);
                }
            }

            SecsGetRecipeList();

            return res;
        }

        /// <summary> SECS更新Recipe List </summary>
        public static void SecsGetRecipeList()
        {
            string[] RecipeNameList = System.IO.Directory.GetFiles("D:\\Parameter\\Recipe");
            ucHost_SECS.GetSingleton().SECS_ClearAllPPID();
            foreach (string Recipe_Name in RecipeNameList)
            {
                string str = "";
                FileInfo fileInfo = new FileInfo(Recipe_Name);
                str = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                ucHost_SECS.GetSingleton().SECS_AddPPID(str);
            }
        }


        #endregion

        private void btnSend_Alarm_Click(object sender, EventArgs e)
        {
            SECS_DispatchAlarm(true, int.Parse(tbxAlarmCode.Text), tbxAlarmMessage.Text);
        }

        private void btnSendSMessage_Click(object sender, EventArgs e)
        {
            PrimaryOutFucntion.PrimaryOut_S10F1(tbxSendMessage.Text);
        }

        private void btnS14F1BarcodeSend_Click(object sender, EventArgs e)
        {
            PrimaryOutFucntion.PrimaryOut_S14F1(tbxStripMapBarcode.Text);
        }

        private void btnOFFLINE_Click(object sender, EventArgs e)
        {
            if (bIsRemote == true)
            {
                m_SecsState.sControl_State_Value = "0";
                SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Control_State, "0");
                SECS_DispatchEvent((int)clsEnum.eSECS_CEID.Control_state_OFFLINE);
                bIsRemote = false;
            }
        }

        private void btnLOCAL_Click(object sender, EventArgs e)
        {
            if (bIsRemote == true)
            {
                m_SecsState.sControl_State_Value = "1";
                SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Control_State, "1");
                SECS_DispatchEvent((int)clsEnum.eSECS_CEID.Control_state_LOCAL);
                bIsRemote = false;
            }
        }

        private void btnREMOTE_Click(object sender, EventArgs e)
        {
            if (bIsRemote == false)
            {
                m_SecsState.sControl_State_Value = "2";
                SECS_UpdateSVID((int)clsEnum.eSECS_SVID.Control_State, "2");
                SECS_DispatchEvent((int)clsEnum.eSECS_CEID.Control_state_REMOTE);
                bIsRemote = true;
            }
        }
    }
}

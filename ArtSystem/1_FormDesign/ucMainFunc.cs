using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtControlLib;
using ArtCommonLib;
using ArtData;

namespace ArtSystem.FormDesign
{
    public partial class ucMainFunc : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================

        /// <summary> 收集所有MainFunc的Button </summary>
        private Dictionary<string, Control> lstMainBtn = new Dictionary<string, Control>();

        /// <summary> 目前滑鼠指向MainFunc的Button </summary>
        private string str_MouseOnButton = "";

        static public bool g_bEnableAllMainFunction = false;

        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucMainFunc m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucMainFunc GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucMainFunc();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucMainFunc()
        {
            InitializeComponent();
            this.TimerInterval = 100;
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                _SetMainFuncUsable();
                int iAverageWidth = 0, iOffsetWidth = 0;
                List<Control> lstMainBtnTemp = new List<Control>();
                foreach(Control pControl in lstMainBtn.Values)
                {
                    if (pControl.Visible)
                    {
                        lstMainBtnTemp.Add(pControl);
                    }
                }
                for (int iCtrlNum = 0; iCtrlNum < lstMainBtnTemp.Count; iCtrlNum++)
                {
                    iAverageWidth = lstMainBtnTemp[iCtrlNum].Parent.Width / lstMainBtnTemp.Count;
                    lstMainBtnTemp[iCtrlNum].Width = (int)(iAverageWidth);
                    lstMainBtnTemp[iCtrlNum].Height = (int)(this.Height * 0.9);
                    lstMainBtnTemp[iCtrlNum].Top = 0;
                    iOffsetWidth = (iAverageWidth - lstMainBtnTemp[iCtrlNum].Width) / 2;
                    lstMainBtnTemp[iCtrlNum].Left = iCtrlNum * iAverageWidth + iOffsetWidth;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 自動更新介面參數 </summary>
        protected override void ReflashTimerFunc()
        {
            try
            {
                ucArtMain_Design.GetSingleton()._ReflashFunc();//刪除Log、備份artEqParameter.ini、自動登出/登入
                bool bShowMouseOn = false;
                bool bShowlbAlarm = false;
                bool bShowSelectedColor = false;
                foreach (string sKey in lstMainBtn.Keys)
                {
                    Control Item = lstMainBtn[sKey];
                    if (Item == btnAlarm
                        && formAlarmReport.IsAlarmOccur() == true
                        && DateTime.Now.Second % 2 == 1)
                    {
                        Item.BackColor = Color.IndianRed;
                        lbAlarm.BackColor = Color.IndianRed;
                        lbAlarm.Location = Item.Location;
                        lbAlarm.Width = Item.Width;
                        lbAlarm.Height = this.Height;
                        lbAlarm.SendToBack();
                        //lbAlarm.Visible = true;
                        bShowlbAlarm = true;
                    }
                    else if (Item.Name == str_MouseOnButton)
                    {
                        Item.BackColor = Color.FromArgb(179, 188, 196);
                        MouseOn.BackColor = Color.FromArgb(179, 188, 196);
                        MouseOn.Location = Item.Location;
                        MouseOn.Width = Item.Width;
                        MouseOn.Height = this.Height;
                        MouseOn.SendToBack();
                        //MouseOn.Visible = true;
                        bShowMouseOn = true;
                    }
                    else if (sKey == ucSubFunc._strNowMainFuncName)
                    {
                        Item.BackColor = Color.FromArgb(109, 198, 205);
                        SelectedColor.BackColor = Color.FromArgb(109, 198, 205);
                        SelectedColor.Location = Item.Location;
                        SelectedColor.Width = Item.Width;
                        SelectedColor.Height = this.Height;
                        SelectedColor.SendToBack();
                        //SelectedColor.Visible = true;
                        bShowSelectedColor = true;
                    }
                    else
                    {
                        Item.BackColor = Color.FromArgb(53, 64, 79);
                    }
                }
                MouseOn.Visible = bShowMouseOn;
                lbAlarm.Visible = bShowlbAlarm;
                SelectedColor.Visible = bShowSelectedColor;
                switch (clsCmData.g_NowEqStatus)
                {
                    case clsCmData.enuEqStatus.Run:
                    case clsCmData.enuEqStatus.Initial:
                        foreach (string sKey in lstMainBtn.Keys)
                        {
                            if (sKey != clsCmData.enuMainFunc.Log.ToString()
                                && sKey != clsCmData.enuMainFunc.Operation.ToString()
                                && sKey != "Alarm")
                            {
                                lstMainBtn[sKey].Enabled = g_bEnableAllMainFunction;
                            }
                            else
                            {
                                lstMainBtn[sKey].Enabled = true;
                            }
                        }
                        break;
                    default:
                        foreach (string sKey in lstMainBtn.Keys)
                        {
                            lstMainBtn[sKey].Enabled = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        /// <summary> 只顯示有權限的MainFunc元件(Button) </summary>
        public void _SetMainFuncUsable()
        {
            string m_strMainFuncName = "";
            foreach (Control Ctrl in lstMainBtn.Values)
            {
                switch (((Control)Ctrl).Name)
                {
                    case "btnOperation":
                        m_strMainFuncName = clsCmData.enuMainFunc.Operation.ToString();
                        break;
                    case "btnManual":
                        m_strMainFuncName = clsCmData.enuMainFunc.Manual.ToString();
                        break;
                    case "btnHardware":
                        m_strMainFuncName = clsCmData.enuMainFunc.Hardware.ToString();
                        break;
                    case "btnSoftware":
                        m_strMainFuncName = clsCmData.enuMainFunc.Software.ToString();
                        break;
                    case "btnLog":
                        m_strMainFuncName = clsCmData.enuMainFunc.Log.ToString();
                        break;
                    default:
                        continue;
                }
                int i = ucSubFunc.GetSingleton(m_strMainFuncName).iFuncCount;
                if (ucSubFunc.GetSingleton(m_strMainFuncName).iFuncCount == 0)
                {
                    Ctrl.Visible = false;
                }
                else
                {
                    Ctrl.Visible = true;
                }
            }
            btnExit.Enabled = _GetbtnExitUsable();
        }

        /// <summary> 判斷是否有權限關閉程式 </summary>
        public bool _GetbtnExitUsable()
        {
            return ucUserAccount.GetSingleton().IsSubFuncUsable(ucArtMain_Design.enuACM_Premit.ArtSystem_ACM_ExistForm.ToString());
        }

        /// <summary> 只顯示有權限的SubFunc介面 </summary>
        public void _ShowAllowSubFunc()
        {
            ucArtMain_Design.GetSingleton()._HideAllFunc();
            if (clsCmData.g_strNowUser == null
                || clsCmData.g_strNowUser == "")
            {
                return;
            }
            if (ucSubFunc._strNowMainFuncName == "" || ucSubFunc._strNowSubFuncName == ""
                || (ucSubFunc._strNowSubFuncName != "" && ucUserAccount.GetSingleton().IsSubFuncUsable(ucSubFunc._strNowSubFuncName) == false))
            {
                for (int iFuncNum = 0; iFuncNum < clsCmData.g_dctFuncLib.Count; iFuncNum++)
                {
                    string strMainFuncName = clsCmData.g_dctFuncLib.ElementAt(iFuncNum).Value.MainFunc;
                    string strSubFuncName = clsCmData.g_dctFuncLib.ElementAt(iFuncNum).Value.SubFunc;
                    bool bIsUsable = ucUserAccount.GetSingleton().IsSubFuncUsable(clsCmData.g_dctFuncLib.ElementAt(iFuncNum).Key);
                    if (bIsUsable == true)
                    {
                        ucSubFunc.GetSingleton(strMainFuncName)._ShowSubFunc(strSubFuncName);
                        break;
                    }
                }
            }
            else if (ucUserAccount.GetSingleton().IsSubFuncUsable(ucSubFunc._strNowSubFuncName) == true)
            {
                ucSubFunc.GetSingleton(ucSubFunc._strNowMainFuncName)._ShowSubFunc(ucSubFunc._strNowSubFuncName);
            }
        }

        #endregion

        #region//===================== 以下為事件處理 (ucMainFunc_Load, ucMainFunc_SizeChanged) =====================

        /// <summary> 將所有Button掛載到List內(lstMainBtn) </summary>
        private void ucMainFunc_Load(object sender, EventArgs e)
        {
            //將所有Button掛載到List內(lstMainBtn)
            lstMainBtn.Clear();
            lstMainBtn.Add(clsCmData.enuMainFunc.Operation.ToString(), btnOperation);
            lstMainBtn.Add(clsCmData.enuMainFunc.Manual.ToString(), btnManual);
            lstMainBtn.Add(clsCmData.enuMainFunc.Hardware.ToString(), btnHardware);
            lstMainBtn.Add(clsCmData.enuMainFunc.Software.ToString(), btnSoftware);
            lstMainBtn.Add(clsCmData.enuMainFunc.Log.ToString(), btnLog);
            lstMainBtn.Add("Alarm", btnAlarm);
            lstMainBtn.Add("Exit", btnExit);
            UpdateControls();
        }

        /// <summary> SizeChagned時,重新排版 </summary>
        private void ucMainFunc_SizeChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        #endregion

        #region//===================== 以下為事件處理 (MainFuncManage, btnAlarm_Click, btnExit_Click) =====================

        /// <summary> 關閉程式 -> Button事件 </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                clsArtSystem.BackupINI();
                clsLog.Log(clsArtSystem.g_strStartUpLogName, "[Software Thread] Count : " + System.Diagnostics.Process.GetCurrentProcess().Threads.Count.ToString());
                if (ucArtMain_Design.GetSingleton().Parent != null)
                {
                    if (ucArtMain_Design.GetSingleton().Parent is Form)
                    {
                        Form pForm = (Form)ucArtMain_Design.GetSingleton().Parent;
                        pForm.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 顯示AlarmForm -> Button事件 </summary>
        private void btnAlarm_Click(object sender, EventArgs e)
        {

            try
            {
                clsArtSystem.BackupINI();
                clsLog.Log(clsCmData.enuLogType.ButtonLog.ToString(), clsCmData.g_strNowUser + " Press ucMainFunc - btnAlarm");
                if (formAlarmReport.GetSingleton().Visible == false)
                {
                    formAlarmReport.GetSingleton().Show(this);
                }
                else
                {
                    formAlarmReport.GetSingleton().WindowState = FormWindowState.Normal;
                    formAlarmReport.GetSingleton().BringToFront();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> MainFunc -> ButtonClick事件 </summary>
        private void MainFuncManage(object sender, EventArgs e)
        {
            if (clsCmData.g_strNowUser == null
                || clsCmData.g_strNowUser.Length <= 0)
            {
                return;
            }
            string m_strMainFuncName = "";
            switch (((Control)sender).Name)
            {
                case "btnOperation":
                    m_strMainFuncName = clsCmData.enuMainFunc.Operation.ToString();
                    break;

                case "btnManual":
                    m_strMainFuncName = clsCmData.enuMainFunc.Manual.ToString();
                    break;

                case "btnHardware":
                    m_strMainFuncName = clsCmData.enuMainFunc.Hardware.ToString();
                    break;

                case "btnSoftware":
                    m_strMainFuncName = clsCmData.enuMainFunc.Software.ToString();
                    break;

                case "btnLog":
                    m_strMainFuncName = clsCmData.enuMainFunc.Log.ToString();
                    break;
            }
            clsArtSystem.BackupINI();
            if (ucArtMain_Design.GetSingleton()._ArtMainFunc(ucArtMain_Design.enuFunc.ChangePagePremit) == false)
            {
                clsLog.Log(clsArtSystem.g_strStartUpLogName, clsCmData.g_strNowUser + " Press uMainFunc : " + m_strMainFuncName);
                clsLog.Log(clsCmData.enuLogType.ButtonLog.ToString(), "[Access Denied] Back to Page : " + ucSubFunc._strNowMainFuncName + "->" + ucSubFunc._strNowSubFuncName);
                return;
            }
            ucSubFunc.GetSingleton(m_strMainFuncName).UpdateControls();
            if (m_strMainFuncName != "")
            {
                bool FoundSubForm = false;
                ucSubFunc.GetSingleton(m_strMainFuncName)._showSubFuncListPanel();
                int iFuncCount = ucSubFunc.GetSingleton(m_strMainFuncName).dctFuncClass.Count;
                for (int iFuncNum = 0; iFuncNum < iFuncCount; iFuncNum++)
                {
                    string strSubFuncName = ucSubFunc.GetSingleton(m_strMainFuncName).dctFuncClass.ElementAt(iFuncNum).Value.SubFunc;
                    bool bIsDefaultShowPanel = ucSubFunc.GetSingleton(m_strMainFuncName).dctFuncClass.ElementAt(iFuncNum).Value.IsDefaultShowPanel;
                    bool bIsSubFuncUsable = ucUserAccount.GetSingleton().IsSubFuncUsable(ucSubFunc.GetSingleton(m_strMainFuncName).dctFuncClass.ElementAt(iFuncNum).Value.SubFunc);
                    if (bIsDefaultShowPanel && bIsSubFuncUsable)
                    {
                        ucSubFunc.GetSingleton(m_strMainFuncName)._ShowSubFunc(strSubFuncName);
                        FoundSubForm = true;
                        break;
                    }
                }

                //如果沒有預設功能，自動指定第一個設定的子功能為預設功能
                if (iFuncCount > 0 && FoundSubForm == false)
                {
                    for (int iFuncNum = 0; iFuncNum < iFuncCount; iFuncNum++)
                    {
                        bool bIsSubFuncUsable = ucUserAccount.GetSingleton().IsSubFuncUsable(ucSubFunc.GetSingleton(m_strMainFuncName).dctFuncClass.ElementAt(iFuncNum).Value.SubFunc);

                        if (bIsSubFuncUsable)
                        {
                            ucSubFunc.GetSingleton(m_strMainFuncName).dctFuncClass.ElementAt(iFuncNum).Value.IsDefaultShowPanel = true;
                            ucSubFunc.GetSingleton(m_strMainFuncName)._ShowSubFunc(ucSubFunc.GetSingleton(m_strMainFuncName).dctFuncClass.ElementAt(iFuncNum).Value.SubFunc);
                            break;
                        }
                    }
                }
                else if(FoundSubForm == false)
                {
                    ucSubFunc.GetSingleton(m_strMainFuncName)._HideAllSubFunc();
                }
            }
            this.UpdateControls();
            clsLog.Log(clsCmData.enuLogType.ButtonLog.ToString(), clsCmData.g_strNowUser + " Press uMainFunc : " + ucSubFunc._strNowMainFuncName + "->" + ucSubFunc._strNowSubFuncName);
        }

        #endregion

        #region//===================== 以下為事件處理 (btnControls_MouseEnter, btnControls_MouseLeave) =====================

        /// <summary> 滑鼠進入MainFucnButton事件 (主要紀錄目前滑鼠指向那一個MainButton) </summary>
        private void btnControls_MouseEnter(object sender, EventArgs e)
        {
            str_MouseOnButton = ((Control)sender).Name;
        }

        /// <summary> 滑鼠進入MainFucnButton事件 (主要紀錄目前滑鼠指向那一個MainButton) </summary>
        private void btnControls_MouseLeave(object sender, EventArgs e)
        {
            str_MouseOnButton = "";
        }

        #endregion

    }
}

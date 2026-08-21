using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using ArtControlLib;
using ArtCommonLib;
using ArtData;
using ArtSystem.FormDesign;
using ArtSystem.Login;
using ArtSystem.MultiSystem;


namespace ArtSystem
{
    public partial class ucArtMain_Design : ucBaseUserControl
    {
        #region //===================== (全/區)域變數設置 =====================

        /// <summary> 主介面設計樣式 </summary>
        public enuDesign e_Design = enuDesign.PC;


        /// <summary> 主要畫面的Panel </summary>
        public Panel p_SubFuncPanel = null;

        /// <summary> 收集ACM_Button </summary>
        private List<comStaticButton> m_lstStaticBtn = new List<comStaticButton>();

        /// <summary> 收集需要翻譯的元件 </summary>
        private Dictionary<string, Control> m_LstLanguageConvert = new Dictionary<string, Control>();

        protected ArtSystem.FormDesign.ucPC_Design m_PC_Design = null;
        protected ArtSystem.FormDesign.ucPanel_Design m_Panel_Design = null;

        public clsHiPerfTimer mTimer_AddSubFunc = new clsHiPerfTimer();

        private ucBaseUserControl m_Title_PC = null;
        public ucBaseUserControl _Title_PC
        {
            get
            {
                return m_Title_PC;
            }
        }
        private ucBaseUserControl m_HotKey_PC = null;
        public ucBaseUserControl _HotKey_PC
        {
            get
            {
                return m_HotKey_PC;
            }
        }


        private ucBaseUserControl m_Title_Panel = null;
        public ucBaseUserControl _Title_Panel
        {
            get
            {
                return m_Title_Panel;
            }
        }
        private ucBaseUserControl m_HotKey_Panel = null;
        public ucBaseUserControl _HotKey_Panel
        {
            get
            {
                return m_HotKey_Panel;
            }
        }

        private bool bNeedUpdateControls = false;
        private bool bUpdateControlsIng = false;
        private clsHiPerfTimer mTimer_UpdateControl = new clsHiPerfTimer();


        /// <summary> 紀錄已刪除Log的日期(如果跨日則再次執行刪除Log動作) </summary>
        private string str_DeleteLogDate = "";
        /// <summary> 紀錄目前Recipe名稱 </summary>
        private string str_RecipePath = "";

        private Point mPos_Mouse = new Point();

        private PerformanceCounter mPreformance_CPU = null;
        private PerformanceCounter mPreformance_RAM = null;

        private ucMotionSetting pUcMotionSetting = null;
        private ucSpeedSetting pUcSpeedSetting = null;

        private double dPreviousSoftwareOpenTime_ms = 0;
        private clsHiPerfTimer mTimer_SoftwareOpen = new clsHiPerfTimer();

        private bool m_bSizeChanging = false;
        private System.Drawing.Size m_SizeChanging = new Size();
        private clsHiPerfTimer mTimer_SizeChanging = new clsHiPerfTimer();

        private bool m_bNeedRestoreLanguage = false;
        #endregion

        #region //===================== Enum 宣告 =====================

        /// <summary> 主介面委派功能 </summary>
        public enum enuFunc
        {
            ///<summary> 編輯設置的所有功能 </summary>
            EditFunc,
            ///<summary> 硬體初始化 </summary>
            HardwareInit,
            ///<summary> 硬體結束設置 </summary>
            HardwareDestroy,
            ///<summary> 軟體初始化 </summary>
            SoftwareInit,
            ///<summary> 軟體結束設置 </summary>
            SoftwareDestroy,
            ///<summary> 軟體啟動執行序 </summary>
            ActiveThread,
            ///<summary> 詢問是否能切換頁面 </summary>
            ChangePagePremit,
            ///<summary> 詢問是否能關閉程式 </summary>
            FormClossingPremit,
            ///<summary> 使用者帳號登入 </summary>
            FormLoad_UserLogin,
            ///<summary> 自動登出時間滿足 </summary>
            AutoLogout,
            /// <summary> 參數路徑變更事件 </summary>
            ParameterPathChange,
        }

        /// <summary> 主介面設計樣式 </summary>
        public enum enuDesign
        {
            PC,
            Panel,
        }

        public enum enuACM_Premit
        {
            ArtSystem_ACM_ExistForm,
            ArtSystem_ACM_ShowDebugForm,
        }

        #endregion

        #region //===================== 必要函式設置 =====================

        static private ucArtMain_Design m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucArtMain_Design GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucArtMain_Design();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucArtMain_Design()
        {
            InitializeComponent();
            mTimer_UpdateControl.Restart();
            mTimer_SizeChanging.Restart();
        }

        /// <summary> 初始化-萬潤介面設計 (掛載FormMain) </summary>
        public void _ArtInitial(Form p_Form)
        {
            clsArtSystem.LoadSimulateFlag();

            this.Parent = p_Form;
            p_Form.Text = "[ArtMMI - " + clsMultiSystem.sSystemName + "] " + System.IO.Directory.GetCurrentDirectory();
            this.SizeChanged += new EventHandler(ucArtMain_Design_SizeChanged);

            m_PC_Design = new FormDesign.ucPC_Design();
            m_PC_Design.Parent = this;
            m_PC_Design.Location = new Point(0, 0);
            m_PC_Design.Size = this.Size;

            m_Panel_Design = new FormDesign.ucPanel_Design();
            m_Panel_Design.Parent = this;
            m_Panel_Design.Location = new Point(0, 0);
            m_Panel_Design.Size = this.Size;

            p_Form.Visible = false;
            p_Form.FormClosing += new FormClosingEventHandler(p_Form_FormClosing);
            p_Form.FormClosed += new FormClosedEventHandler(p_Form_FormClosed);
            p_Form.SizeChanged += new EventHandler(p_Form_SizeChanged);
            p_Form.MouseWheel += new MouseEventHandler(p_Form_MouseWheel);

            //功能權限設定
            foreach (enuACM_Premit eACM in Enum.GetValues(typeof(enuACM_Premit)))
            { clsCmData.g_dctAcmBtnLib.Add(eACM.ToString(), new Control()); }

            FormLoad();

            #region//新增滑鼠移動事件 (全域)
            GlobalMouseHandler gmh = new GlobalMouseHandler();
            gmh.TheMouseMoved += new MouseMovedEvent(gmh_TheMouseMoved);
            Application.AddMessageFilter(gmh);
            #endregion

        }

        #endregion

        #region //===================== 委派功能 =====================

        /// <summary> 主介面功能委派(格式) </summary>
        public delegate bool evt_ArtMainFunc(enuFunc e_Function);
        /// <summary> 主介面功能委派 </summary>
        public event evt_ArtMainFunc _evt_ArtMainFunc = null;

        /// <summary> 功能呼叫 </summary>
        public bool _ArtMainFunc(enuFunc e_Function)
        {
            bool rValue = true;
            if (ucArtMain_Design.GetSingleton()._evt_ArtMainFunc != null)
            {
                rValue = ucArtMain_Design.GetSingleton()._evt_ArtMainFunc(e_Function);
            }
            return rValue;
        }

        #endregion

        #region //===================== public 函式設置 =====================

        /// <summary> Form_Load 事件 </summary>
        public void _formMain_Load(object sender, EventArgs e)
        {
            if (sender is Form)
            {
                //將ArtMain_Design掛載到此Form
                this._ArtInitial((Form)sender);
            }
        }
        /// <summary> 重置Controls </summary>
        private void UpdateControls()
        {
            //this.BeginInvoke(new Action(() =>
            //{
                try
                {
                    if (bUpdateControlsIng == true)
                    {
                        if (mTimer_UpdateControl.IsTimeOut(1000, clsCmData.enuSecUnit.MilliSec))
                        {  bUpdateControlsIng = false; }
                        return;
                    }
                    if (mTimer_UpdateControl.IsTimeOut(100, clsCmData.enuSecUnit.MilliSec))
                    {
                        bUpdateControlsIng = true;
                        bNeedUpdateControls = false;
                        mTimer_UpdateControl.Restart();
                        clsLog.Log(clsArtSystem.g_strStartUpLogName, "[ucArtMain_Design()._UpdateControls()] Start.");
                        clsHiPerfTimer mTimer = new clsHiPerfTimer();
                        mTimer.Restart();

                        clsIniFile iniSystem = new clsIniFile(clsCmData.g_strSystemIniFilePath);//artSystem.ini
                        bool bFullScrean = iniSystem.GetString("System", "Form Style", "btnWindowsMode") == "btnFullScrean";
                        bool bTopMost = iniSystem.GetString("System", "Is Topmost", "btnTopFalse") == "btnTopTrue";
                        enuDesign eDesign = iniSystem.GetString("System", "Form Design", "btnPCDesign") == "btnPanelDesign" ? enuDesign.Panel : enuDesign.PC;

                        Size mSize = new Size();
                        if (ucArtMain_Design.GetSingleton().Parent != null)
                        {
                            if (ucArtMain_Design.GetSingleton().Parent is Form)
                            {
                                Form pForm = (Form)ucArtMain_Design.GetSingleton().Parent;
                                bool bVisible = pForm.Visible;
                                pForm.TopMost = bTopMost;
                                if (bFullScrean == true)
                                {
                                    pForm.WindowState = FormWindowState.Normal;
                                    pForm.FormBorderStyle = FormBorderStyle.None;
                                    pForm.Size = Screen.PrimaryScreen.Bounds.Size;
                                    pForm.Location = new Point(0, 0);
                                    mSize = pForm.Size;
                                }
                                else
                                {
                                    if (pForm.FormBorderStyle == FormBorderStyle.None
                                        || clsArtSystem.bIsProgramOpenFinish == false
                                        || eDesign != this.e_Design)
                                    {
                                        if (eDesign == enuDesign.PC)
                                        {
                                            pForm.WindowState = FormWindowState.Maximized;
                                            pForm.FormBorderStyle = FormBorderStyle.Sizable;
                                            pForm.Size = new Size(1280, 1024);
                                        }
                                        else if (eDesign == enuDesign.Panel)
                                        {
                                            pForm.WindowState = FormWindowState.Normal;
                                            pForm.FormBorderStyle = FormBorderStyle.Sizable;
                                            pForm.Size = new Size(1008, 731);//640*480
                                            pForm.Left = (Screen.PrimaryScreen.Bounds.Size.Width - (pForm.ClientSize.Width)) / 2;
                                            pForm.Top = (Screen.PrimaryScreen.Bounds.Size.Height - (pForm.ClientSize.Height)) / 6;
                                        }
                                    }
                                    mSize = pForm.ClientSize;
                                    //mSize.Width = pForm.Width - 16;//扣掉Form的邊框
                                    //mSize.Height = pForm.Height - 39;//扣掉Form的邊框 與 上面的顯示Bar
                                }
                            }
                        }
                        this.e_Design = eDesign;
                        this.Size = mSize;
                        if (e_Design == enuDesign.PC
                            && this.m_PC_Design.Visible == false)
                        {
                            this.m_PC_Design.Visible = true;
                            this.m_Panel_Design.Visible = false;
                            this.m_PC_Design.Location = new Point(0, 0);
                            this.m_PC_Design.Size = this.Size;
                            this.m_PC_Design._UpdateControls();
                        }
                        else if (e_Design == enuDesign.Panel
                            && this.m_Panel_Design.Visible == false)
                        {
                            this.m_PC_Design.Visible = false;
                            this.m_Panel_Design.Visible = true;
                            this.m_Panel_Design.Location = new Point(0, 0);
                            this.m_Panel_Design.Size = this.Size;
                            this.m_Panel_Design._UpdateControls();
                        }



                        //設置Sub Func 介面權限
                        for (int iSubFuncPanelNum = 0; iSubFuncPanelNum < clsCmData.g_astrMainFunc.Length; iSubFuncPanelNum++)
                        {
                            ucSubFunc.GetSingleton(clsCmData.g_astrMainFunc[iSubFuncPanelNum]).UpdateControls();
                        }


                        //設置Main Func 介面權限
                        ucMainFunc.GetSingleton()._SetMainFuncUsable();
                        ucMainFunc.GetSingleton().UpdateControls();
                        ucMainFunc.GetSingleton()._ShowAllowSubFunc(); //預設執行操作頁面


                        switch (ucArtMain_Design.GetSingleton().e_Design)
                        {
                            case ucArtMain_Design.enuDesign.PC:
                                if (ucArtMain_Design.GetSingleton()._HotKey_PC != null)
                                { ucArtMain_Design.GetSingleton()._HotKey_PC.SetReflashTimerStart(true); }
                                if (ucArtMain_Design.GetSingleton()._Title_PC != null)
                                { ucArtMain_Design.GetSingleton()._Title_PC.SetReflashTimerStart(true); }
                                if (ucArtMain_Design.GetSingleton()._HotKey_Panel != null)
                                { ucArtMain_Design.GetSingleton()._HotKey_Panel.SetReflashTimerStart(false); }
                                if (ucArtMain_Design.GetSingleton()._Title_Panel != null)
                                { ucArtMain_Design.GetSingleton()._Title_Panel.SetReflashTimerStart(false); }
                                break;
                            case ucArtMain_Design.enuDesign.Panel:
                                if (ucArtMain_Design.GetSingleton()._HotKey_PC != null)
                                { ucArtMain_Design.GetSingleton()._HotKey_PC.SetReflashTimerStart(false); }
                                if (ucArtMain_Design.GetSingleton()._Title_PC != null)
                                { ucArtMain_Design.GetSingleton()._Title_PC.SetReflashTimerStart(false); }
                                if (ucArtMain_Design.GetSingleton()._HotKey_Panel != null)
                                { ucArtMain_Design.GetSingleton()._HotKey_Panel.SetReflashTimerStart(true); }
                                if (ucArtMain_Design.GetSingleton()._Title_Panel != null)
                                { ucArtMain_Design.GetSingleton()._Title_Panel.SetReflashTimerStart(true); }
                                break;
                            default:
                                break;
                        }
                        clsLog.Log(clsArtSystem.g_strStartUpLogName, "[ucArtMain_Design()._UpdateControls()] Done, Spend Time (" + mTimer.ElapsedMilliseconds.ToString("F3") + " ms), bFullScrean : " + bFullScrean + " , bTopMost : " + bTopMost + " , eFormDesign : " + eDesign.ToString());
                        bUpdateControlsIng = false;
                    }
                }
                catch (Exception ex)
                {
                    bUpdateControlsIng = false;
                    clsArtSystem.CatchLog(ex);
                }
            //}));
        }

        /// <summary> 重置Controls </summary>
        public void _UpdateControls()
        {
            bNeedUpdateControls = true;
        }

        /// <summary> ucMainFunc->ReflashTimerFunc()內已經呼叫此函式(定時執行刪除檔案動作) </summary>
        public void _ReflashFunc()
        {
            #region //定時執行刪除檔案動作
            if (clsArtSystem.bIsProgramOpenFinish == true)
            {
                if (DateTime.Now.ToString("yyyyMMdd") != str_DeleteLogDate)
                {
                    str_DeleteLogDate = DateTime.Now.ToString("yyyyMMdd");
                    ucLogPath.GetSingleton().DelFileThreadWork();//執行刪除Log檔案動作
                }
                ucRemoveFiles._DailyRemove();
            }
            #endregion

            #region//自動登入登出Operator
            if (ucAutoLogout.GetSingleton()._NeedAutoLogout() == true)
            {
                clsCmData.g_strNowUser = "Operator";
                clsCmData.g_iNowUserLevel = 9;

                //設置ACM 按鈕權限
                for (int iBtnNum = 0; iBtnNum < clsCmData.g_dctAcmBtnLib.Count; iBtnNum++)
                {
                    bool bIsUsable = ucUserAccount.GetSingleton().IsSubFuncUsable(clsCmData.g_dctAcmBtnLib.ElementAt(iBtnNum).Key);
                    if (clsCmData.g_dctAcmBtnLib.ElementAt(iBtnNum).Value is Control)
                    {
                        ((Control)clsCmData.g_dctAcmBtnLib.ElementAt(iBtnNum).Value).Enabled = bIsUsable;
                    }
                }

                ucArtMain_Design.GetSingleton()._UpdateControls();
                _ArtMainFunc(enuFunc.AutoLogout);
            }
            #endregion

            #region//尺寸變化UpdateControls(), 為了不要在拉大小的過程中一直updateControls()，所以放這裡(1秒刷新一次)
            if (mTimer_SizeChanging.IsTimeOut(300, clsCmData.enuSecUnit.MilliSec) == true)
            {
                mTimer_SizeChanging.Restart();
                if (m_bSizeChanging == true && this.Parent != null)
                {
                    if (m_SizeChanging == this.Parent.Size)
                    {
                        _UpdateControls();
                        m_bSizeChanging = false;
                    }
                    else
                    {
                        m_SizeChanging = this.Parent.Size;
                    }
                }
            }
            #endregion

            if (this.bNeedUpdateControls == true)
            {
                UpdateControls();
            }
        }

        /// <summary> 掛載ucTitle() </summary>
        public void _Input_ucTitle(enuDesign e_Design, ucBaseUserControl p_Title)
        {
            if (e_Design == enuDesign.PC)
            {
                m_Title_PC = p_Title;
                _CollectChangeLanguageControls(p_Title);
            }
            else if (e_Design == enuDesign.Panel)
            {
                m_Title_Panel = p_Title;
                _CollectChangeLanguageControls(p_Title);
            }
        }

        /// <summary> 掛載ucHotkey() </summary>
        public void _Input_ucHotkey(enuDesign e_Design, ucBaseUserControl p_Hotkey)
        {
            if (e_Design == enuDesign.PC)
            {
                m_HotKey_PC = p_Hotkey;
                _CollectChangeLanguageControls(p_Hotkey);
            }
            else if (e_Design == enuDesign.Panel)
            {
                m_HotKey_Panel = p_Hotkey;
                _CollectChangeLanguageControls(p_Hotkey);
            }
        }

        /// <summary> 隱藏所有子功能頁面 </summary>
        public void _HideAllFunc()
        {
            for (int iSubFuncPanelNum = 0; iSubFuncPanelNum < clsCmData.g_astrMainFunc.Length; iSubFuncPanelNum++)
            {
                ucSubFunc.GetSingleton(clsCmData.g_astrMainFunc[iSubFuncPanelNum])._HideAllSubFunc();
                ucSubFunc.GetSingleton(clsCmData.g_astrMainFunc[iSubFuncPanelNum]).Hide();
            }
        }

        /// <summary> 介面語言切換 ("EN,TC,CN,JP..."), 多載功能 () </summary>
        public void _ChangeLanguage(string Language)
        {
            if (m_bNeedRestoreLanguage == true)
            {
                RestoreLanguage();
            }
            m_bNeedRestoreLanguage = true;
            string sPath = Application.StartupPath + "\\INI\\" + "Language_" + Language;
            if (System.IO.File.Exists(sPath + ".ini") == true)
            {
                clsLanguage.RefreshLib(sPath + ".ini");
                clsCmData.g_strLanguageType = Language;
                string[] Files = System.IO.Directory.GetFiles(Application.StartupPath + "\\INI");
                foreach (string strPath in Files)
                {
                    if (strPath.Contains(sPath + "-") == true)
                    {
                        clsIniFile mIniFile = new clsIniFile(strPath);
                        clsLanguage.AddLanguageLib(mIniFile.GetSectionValues("Translation"));
                    }
                }
            }
            else
            {
                clsLanguage.RefreshLib();
                clsCmData.g_strLanguageType = Language;
                sPath = Application.StartupPath + "\\INI\\" + "Language";
                string[] Files = System.IO.Directory.GetFiles(Application.StartupPath + "\\INI");
                foreach (string strPath in Files)
                {
                    if (strPath.Contains(sPath + "-") == true)
                    {
                        clsIniFile mIniFile = new clsIniFile(strPath);
                        clsLanguage.AddLanguageLib(mIniFile.GetSectionValues("Translation"));
                    }
                }
            }
            string strAxisNameINI = Application.StartupPath + "\\INI\\AxisName.ini";
            if (System.IO.File.Exists(strAxisNameINI) == true)
            {
                clsIniFile mINIFile = new clsIniFile(strAxisNameINI);
                if (mINIFile.GetSectionNames().ToList<string>().Contains("AxisName" + Language) == true)
                {
                    Dictionary<string, string> mDicAxisName = mINIFile.GetSectionValuesAsDictionary("AxisName" + Language);
                    clsLanguage.AddLanguageLib(mDicAxisName);
                }

            }
            ChangeLanguage();
            //更新其他介面語系
            ucAlarmManage.GetSingleton().UpdateControls();
            ucDioMonitor.GetSingleton().UpdateControls();//artDioName.ini
            ucArtMain_Design.GetSingleton().Focus();
        }

        /// <summary> 重置上下滾輪頁面(SubForm) </summary>
        public void _ResetScrollBar()
        {
            try
            {
                if (this.InvokeRequired == true)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        this._ResetScrollBar();
                    }));
                }
                else
                {
                    //重置SubForm的Height.
                    if (e_Design == enuDesign.PC)
                    {
                        m_PC_Design._SetSubFormLocation(0);
                    }
                    else if (e_Design == enuDesign.Panel)
                    {
                        m_Panel_Design._SetSubFormLocation(0);
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 新增功能至g_lstFuncLib 資料集中 </summary>
        public bool _AddFunc(clsObjFunc p_FuncClass, bool p_bCanScrool = true)
        {
            try
            {
                if (clsCmData.g_dctFuncLib.ContainsKey(p_FuncClass.SubFunc))
                {
                    return false;
                }
                AddAutoLogoutSettingInUserAccount(p_FuncClass);
                Reset_ucMotionSetting(p_FuncClass);
                clsCmData.g_dctFuncLib.Add(p_FuncClass.SubFunc, p_FuncClass);
                if (ucArtMain_Design.GetSingleton().p_SubFuncPanel == null)
                {
                    p_FuncClass.Control.Visible = false;
                    p_FuncClass.Control.Parent = ucArtMain_Design.GetSingleton();
                }
                p_FuncClass.Control.Tag = p_bCanScrool;
                p_FuncClass.Control.Dock = DockStyle.Fill;

                //收集管理按鈕
                ucArtMain_Design.GetSingleton()._CollectAcmButton(p_FuncClass.SubFunc, p_FuncClass.Control);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return true;
        }

        /// <summary> 收集指定物件中管理按鈕 (只要Control.Name 包含 "ACM" 字眼就會被納入) </summary>
        public void _CollectAcmButton(string p_strProrFunc, Control p_Control)
        {
            for (int iControlNum = 0; iControlNum < p_Control.Controls.Count; iControlNum++)
            {
                //要處理的元件
                Control CtrlProc = p_Control.Controls[iControlNum];

                if (CtrlProc is Form
                    || CtrlProc is Panel
                    || CtrlProc is FlowLayoutPanel
                    || CtrlProc is GroupBox
                    || CtrlProc is SplitContainer
                    || CtrlProc is SplitterPanel
                    || CtrlProc is TableLayoutPanel
                    || CtrlProc is TabControl
                    || CtrlProc is TabPage
                    || CtrlProc is ucBaseUserControl)
                {
                    //如果是容器，再深入收集                    
                    _CollectAcmButton(p_strProrFunc, CtrlProc);
                }
                else if (CtrlProc is comStaticButton)
                {
                    //收集StaticBtn 以方便統一更新狀態
                    ucArtMain_Design.GetSingleton().m_lstStaticBtn.Add((comStaticButton)CtrlProc);
                }
                else if ((CtrlProc is Button || CtrlProc is Panel) && CtrlProc.Name.Length >= 3)
                {
                    //收集管理按鈕
                    if (CtrlProc.Name.Substring(0, 3) == "ACM")
                    {
                        //string strDctKey = p_strProrFunc + " - " + CtrlProc.Text;
                        string strDctKey = p_strProrFunc + " - " + CtrlProc.Text + "(" + CtrlProc.Name + ")";
                        if (clsCmData.g_dctAcmBtnLib.ContainsKey(strDctKey))
                        {
                            formMessageBox.Show(strDctKey + clsLanguage.GetTranslation(" Find duplicate names,no control !!"));
                        }
                        else
                        {
                            clsCmData.g_dctAcmBtnLib.Add(strDctKey, CtrlProc);
                        }
                    }
                }

            }
        }

        /// <summary> 收集需要翻譯的元件 (建議在clsEditFunc->EditFunc()完成前掛載) </summary>
        public void _CollectChangeLanguageControls(Control p_Control)
        {
            if (p_Control != null)
            {
                string sName = p_Control.Name;
                if (m_LstLanguageConvert.ContainsValue(p_Control) == false)
                {
                    if (m_LstLanguageConvert.ContainsKey(sName) == true)
                    {
                        sName += "_" + DateTime.Now.ToString("HH:mm:ss.fff");
                    }
                    m_LstLanguageConvert.Add(sName, p_Control);
                    if (clsArtSystem.bIsProgramOpenFinish == true)
                    {
                        clsLanguage.SetLanguateToControls(p_Control);
                    }
                }
            }
        }

        /// <summary> 取得目前CPU使用率 (注意：第一次使用會很慢) </summary>
        public double _GetCPUUsage()
        {
            double rValue = 0;
            try
            {
                if (mPreformance_CPU == null)
                {
                    mPreformance_CPU = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName, true);
                }
                rValue = Math.Round(mPreformance_CPU.NextValue() / Environment.ProcessorCount, 2);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        /// <summary> 取得目前RAM使用率 (注意：第一次使用會很慢) </summary>
        public double _GetRAMUsage()
        {
            double rValue = 0;
            try
            {
                if (mPreformance_RAM == null)
                {
                    mPreformance_RAM = new PerformanceCounter("Process", "Private Bytes", Process.GetCurrentProcess().ProcessName, true);
                }
                rValue = Math.Round(mPreformance_RAM.NextValue() / 1024 / 1024, 2);// MB
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        /// <summary> 程式未開啟成功的話會自動處理開啟時間 </summary>
        public void _ShowProcBar(string sMessage , int iPercentage)
        {
            if (clsArtSystem.bIsProgramOpenFinish == true
                || iPercentage == 100)
            {
                formProcBar.GetSingleton().ShowProc(sMessage, iPercentage);
            }
            else
            {
                if (dPreviousSoftwareOpenTime_ms != 0)
                {
                    int iTemp = (int)(100 * mTimer_SoftwareOpen.ElapsedMilliseconds / dPreviousSoftwareOpenTime_ms);
                    if (iTemp > 100)
                    {
                        iTemp = 100;
                    }
                    if (iTemp < iPercentage)
                    {
                        iTemp = iPercentage;
                    }
                    iPercentage = iTemp;
                }
                if (formProcBar.GetSingleton().iProcValue != iPercentage)
                {
                    formProcBar.GetSingleton().ShowProc(sMessage, iPercentage);
                }
            }
        }
        
        /// <summary> 關閉隱藏ProcBar </summary>
        public void HideProc()
        {
            if (clsArtSystem.bIsProgramOpenFinish == true)
            {
                this.BeginInvoke(new Action(() =>
                {
                    formProcBar.GetSingleton().HideProc();
                }));
            }
        }

        /// <summary> 關閉隱藏ProcBar </summary>
        public void ShowProc(string sMessage, int iPercentage)
        {
            this.BeginInvoke(new Action(() =>
            {
                formProcBar.GetSingleton().ShowProc(sMessage, iPercentage);
            }));
        }
        #endregion

        #region //===================== public 函式設置 (Assign ui action to UI thread) =====================
        /// <summary>  Assign ui action to UI thread </summary>
        public void Act(Action act)
        {
            ActionsProcessor.Sgt.Enqueue(() =>
            {
                InternalAct(act);
            });
        }
        private void InternalAct(Action act)
        {
            if (!IsDisposed)
            {
                if (IsHandleCreated)
                {
                    if (InvokeRequired)
                        BeginInvoke(act);
                    else
                        act();
                }
            }
        }
        #endregion

        #region //===================== private 函式設置 =====================

        /// <summary> 載入Form流程 </summary>
        private void FormLoad()
        {
            try
            {
                #region//開始記錄Log (如果Parent為Null,是不會顯示在介面上的)
                ucLogPath.GetSingleton();
                if (ucLogHistory.GetSingleton(clsArtSystem.g_strStartUpLogName).Parent == null)
                {
                    ucLogHistory.GetSingleton(clsArtSystem.g_strStartUpLogName).Parent = this;
                }
                if (ucLogHistory.GetSingleton(clsArtSystem.g_strCatchLogName).Parent == null)
                {
                    ucLogHistory.GetSingleton(clsArtSystem.g_strCatchLogName).Parent = this;
                }
                if (clsMultiSystem.bIsMultiSystem == true)
                {
                    clsLog.Log(clsArtSystem.g_strStartUpLogName, "==================== [formMain() -> Load()] " + clsMultiSystem.sSystemName + " ====================");
                }
                else
                {
                    clsLog.Log(clsArtSystem.g_strStartUpLogName, "==================== [formMain() -> Load()] ====================");
                }
                #endregion
                clsLog.Log(clsArtSystem.g_strStartUpLogName, "==================== [formMain() -> Open Program...] ====================");
                //clsHiPerfTimer mTimer = new clsHiPerfTimer();
                //mTimer.Restart();
                _ShowProcBar(clsLanguage.GetTranslation("Open Program..."), 1);

                dPreviousSoftwareOpenTime_ms = GetINIData_SoftwareOpenTime();

                mTimer_SoftwareOpen.Restart();
                clsLog.Log(clsArtSystem.g_strStartUpLogName, "==================== [formMain() -> clsArtSystem.ReloadINI()] ====================");
                clsArtSystem.ReloadINI();
                clsLog.Log(clsArtSystem.g_strStartUpLogName, "==================== [formMain() -> clsArtSystem.BackupINI()] ====================");
                clsArtSystem.BackupINI();
                clsLog.Log(clsArtSystem.g_strStartUpLogName, "==================== [formMain() -> clsArtSystem.LogPathBuilder()] ====================");
                clsArtSystem.LogPathBuilder();

                //開啟程式時將此參數設定為true，方便建構式判斷是否為真實開啟程式
                clsArtSystem.bIsProgramOpen = true;
                System.Threading.Thread.Sleep(500);


                clsLog.Log(clsArtSystem.g_strStartUpLogName, "==================== [formMain() -> ucParameter.GetSingleton();//建立參數] ====================");
                ucParameter.GetSingleton();//建立參數   //artEqParameter.ini
                ucParameter.GetSingleton().PathChanged += new ucParameter.eventPathChanged(ucArtMain_Design_PathChanged);
                ucAlarmManage.GetSingleton().UpdateControls();


                System.Threading.Thread.Sleep(500);


                //程式開啟前檢查動作
                if (CheckApplicationDuplication())
                {
                    Application.Exit();
                    return;
                }

                if (this.Parent != null)
                { this.Parent.Visible = false; }

                if (clsArtSystem.mSystemPmt == null)
                { clsArtSystem.mSystemPmt = new clsModulePmt("ArtSystem"); }

                //硬體初始化
                _ShowProcBar(clsLanguage.GetTranslation("Hardware Init..."), 20);
                _ArtMainFunc(enuFunc.HardwareInit);

                //軟體初始化
                _ShowProcBar(clsLanguage.GetTranslation("Software Init..."), 40);
                _ArtMainFunc(enuFunc.SoftwareInit);

                //介面初始化
                _ShowProcBar(clsLanguage.GetTranslation("Interface Create..."), 60);
                this.mTimer_AddSubFunc.Restart();
                _ArtMainFunc(enuFunc.EditFunc);
                this.mTimer_AddSubFunc.Stop();

                //開始啟動流程執行序
                _ArtMainFunc(enuFunc.ActiveThread);

                //介面更新
                _ShowProcBar(clsLanguage.GetTranslation("Interface Update..."), 80);
                //_UpdateControls();
                UpdateControls();

                //更新語言
                ucArtMain_Design.GetSingleton()._ChangeLanguage(ucFormSetup.GetSingleton()._GetCurrentLanguage());


                Reset_ucMotionSetting_DefaultValue();
                foreach (clsEnum.enuPmtType eType in Enum.GetValues(typeof(clsEnum.enuPmtType)))
                {
                    clsLog.Log(clsArtSystem.g_strStartUpLogName, "[Startup Path] " + eType.ToString() + " -> " + ucParameter.GetFilePath(eType));
                }

                //完成初始化
                _ShowProcBar("Finish!", 100);
                clsHiPerfTimer mDelay = new clsHiPerfTimer();
                mDelay.Restart();
                Reset_ucMotionSetting_DefaultValue();


                ucLogHistory.GetSingleton(clsArtSystem.g_strStartUpLogName).SendToBack();
                ucLogHistory.GetSingleton(clsArtSystem.g_strCatchLogName).SendToBack();

                while (true)
                {
                    if (mDelay.IsTimeOut(0.5, clsCmData.enuSecUnit.Sec) == true)
                    {
                        break;
                    }
                    //Application.DoEvents();
                    System.Threading.Thread.Sleep(1);//(延遲0.5s讓介面可以看到完成指示)
                }
                mTimer_SoftwareOpen.Stop();
                SaveINIData_SoftwareOpenTime(mTimer_SoftwareOpen.ElapsedMilliseconds);
                formProcBar.GetSingleton().HideProc();
                clsLog.Log(clsArtSystem.g_strStartUpLogName, "[Finish Initial] Delay 500ms.");

                //程式完全開啟後設為True
                clsArtSystem.bIsProgramOpenFinish = true;
                if (this.Parent != null)
                { this.Parent.Visible = true; }

                clsLog.Log(clsArtSystem.g_strStartUpLogName, "[Software Thread] Count : " + System.Diagnostics.Process.GetCurrentProcess().Threads.Count.ToString());
                clsLog.Log(clsArtSystem.g_strStartUpLogName, "==================== [formMain() -> Load()] - Done ====================");
                //clsLog.Log(clsArtSystem.g_strStartUpLogName, "==================== [formMain() -> Load()] - Done (" + mTimer.ElapsedMilliseconds.ToString("F3") + " ms) ====================");

                if (this.ParentForm != null)
                {
                    this.ParentForm.Focus();
                    this.ParentForm.Activate();
                }

                //Login
                int iLoginRetry = 0;
                while (true)
                {
                    if (_ArtMainFunc(enuFunc.FormLoad_UserLogin) == true)
                    {
                        if (clsCmData.g_strNowEqStatus.Length > 1)
                        {
                            break;
                        }
                    }
                    System.Threading.Thread.Sleep(100);
                    Application.DoEvents();
                    iLoginRetry++;
                    if (iLoginRetry >= 100)
                    {
                        formMessageBox.Show("Login Form Opening Fail.");
                        FormClossing();
                        break;
                    }
                }


            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                MessageBox.Show("Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        /// <summary> 詢問是否能夠關閉Form流程 </summary>
        private bool FormClossing()
        {
            bool Cancel = false;

            //程式重覆執行
            if (clsArtSystem.bIsARTMMIDuplication)
            {
                clsLog.Log(clsArtSystem.g_strStartUpLogName, "formMain_FormClosing() ->  p_bATMMIDuplication(true).");
                Cancel = false;
            }
            //確認關閉程式權限(可以透過ucUserLevel管理權限) ACM_BtnExit
            else if (ucMainFunc.GetSingleton()._GetbtnExitUsable() == false)
            {
                clsLog.Log(clsArtSystem.g_strStartUpLogName, clsCmData.g_strNowUser + " Press ucMainFunc - btnExit");
                formMessageBox.Show("[Insufficient permission level]\r\nExit Form Access Denied..", "Insufficient permission level", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cancel = true;
            }
            //(如果有產品不能關閉程式)
            else if (_ArtMainFunc(enuFunc.FormClossingPremit) == false)
            {
                clsLog.Log(clsArtSystem.g_strStartUpLogName, clsCmData.g_strNowUser + " Press ucMainFunc - btnExit");
                Cancel = true;
            }
            //詢問是否關閉程式
            else
            {
                if (formMessageBox.Show("Are you sure to Exit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    clsLog.Log(clsArtSystem.g_strStartUpLogName, "NowUser : " + clsCmData.g_strNowUser + " , formMain_FormClosing() ->  Clicked Yes.");
                    clsLog.Log(clsArtSystem.g_strStartUpLogName, "==================== [formMain() -> FormClosing()]  ====================");
                    clsHiPerfTimer mTimer = new clsHiPerfTimer();
                    mTimer.Restart();

                    formProcBar.GetSingleton().ShowProc(clsLanguage.GetTranslation("Software Destory..."), 30);
                    _ArtMainFunc(enuFunc.SoftwareDestroy);

                    System.Threading.Thread.Sleep(100);

                    formProcBar.GetSingleton().ShowProc(clsLanguage.GetTranslation("Hardware Destory..."), 60);
                    _ArtMainFunc(enuFunc.HardwareDestroy);


                    formProcBar.GetSingleton().ShowProc("Finish!", 100);
                    System.Threading.Thread.Sleep(500);//(延遲0.5s讓介面可以看到完成指示)

                    ActionsProcessor.Sgt.Terminate();

                    //formProcBar.GetSingleton().HideProc();//轉移到Closed裡面執行
                    clsLog.Log(clsArtSystem.g_strStartUpLogName, "==================== [formMain() -> FormClosing()] - Done (" + mTimer.ElapsedMilliseconds.ToString("F3") + " ms) ====================");
                    Cancel = false;
                }
                else
                {
                    clsLog.Log(clsArtSystem.g_strStartUpLogName, "NowUser : " + clsCmData.g_strNowUser + " , formMain_FormClosing() ->  Clicked No.");
                    Cancel = true;
                }
            }
            return Cancel;
        }

        /// <summary> 完成關閉Form流程(目前沒有項目) </summary>
        private void FormClosed()
        {
            //目前沒有項目
            clsArtSystem.bIsProgramOpen = false;
            clsArtSystem.bIsProgramOpenFinish = false;
            clsArtSystem.bIsProgramClosed = true;
            if (clsCmData.g_bIsRunThreadAlive == true)
            {
                clsLog.Log(clsArtSystem.g_strStartUpLogName, "==================== [formMain() -> FormClosed()]  ====================");
                clsHiPerfTimer mTimer = new clsHiPerfTimer();
                mTimer.Restart();
                //_ArtMainFunc(enuFunc.SoftwareDestroy);//Closing裡面已經有執行了
                //System.Threading.Thread.Sleep(100);
                //_ArtMainFunc(enuFunc.HardwareDestroy);
                //System.Threading.Thread.Sleep(500);//(延遲0.5s讓介面可以看到完成指示)
                clsLog.Log(clsArtSystem.g_strStartUpLogName, "==================== [formMain() -> FormClosed()] - Done (" + mTimer.ElapsedMilliseconds.ToString("F3") + " ms) ====================");
            }
            formProcBar.GetSingleton().HideProc();
        }

        /// <summary> 介面語言還原 </summary>
        private void RestoreLanguage()
        {
            m_Singleton.Enabled = false;
            clsLanguage.UpdateRestoreLanguageLib();
            _ShowProcBar("Language Changing...", 10);
            //formProcBar.GetSingleton().iProcValue = 20;
            _ShowProcBar("Language Changing...", 20);
            clsLanguage.SetLanguateToControls(ucArtMain_Design.GetSingleton());
            //formProcBar.GetSingleton().iProcValue = 40;
            _ShowProcBar("Language Changing...", 40);
            clsLanguage.SetLanguateToControls(formLogin.GetSingleton());
            //formProcBar.GetSingleton().iProcValue = 60;
            _ShowProcBar("Language Changing...", 60);
            clsLanguage.SetLanguateToControls(formAlarmReport.GetSingleton());
            //formProcBar.GetSingleton().iProcValue = 80;
            _ShowProcBar("Language Changing...", 80);
            foreach (Control pControl in m_LstLanguageConvert.Values)
            { clsLanguage.SetLanguateToControls(pControl); }
            //formProcBar.GetSingleton().iProcValue = 99;
            _ShowProcBar("Language Changing...", 99);
            if (clsArtSystem.bIsProgramOpenFinish == true)
            {
                formProcBar.GetSingleton().Close();
            }
            m_Singleton.Enabled = true;
        }

        /// <summary> 介面語言切換</summary>
        private void ChangeLanguage()
        {
            if (m_Singleton != null)
            {
                m_Singleton.Enabled = false;
                clsLanguage.UpdateLanguageLib();
                _ShowProcBar("Language Changing...", 10);
                //formProcBar.GetSingleton().iProcValue = 20;
                _ShowProcBar("Language Changing...", 20);
                clsLanguage.SetLanguateToControls(ucArtMain_Design.GetSingleton());
                //formProcBar.GetSingleton().iProcValue = 40;
                _ShowProcBar("Language Changing...", 40);
                clsLanguage.SetLanguateToControls(formLogin.GetSingleton());
                //formProcBar.GetSingleton().iProcValue = 60;
                _ShowProcBar("Language Changing...", 60);
                clsLanguage.SetLanguateToControls(formAlarmReport.GetSingleton());
                //formProcBar.GetSingleton().iProcValue = 80;
                _ShowProcBar("Language Changing...", 80);
                foreach (Control pControl in m_LstLanguageConvert.Values)
                { clsLanguage.SetLanguateToControls(pControl); }
                //formProcBar.GetSingleton().iProcValue = 99;
                _ShowProcBar("Language Changing...", 99);
                if (clsArtSystem.bIsProgramOpenFinish == true)
                {
                    formProcBar.GetSingleton().Close();
                }
                m_Singleton.Enabled = true;
            }
        }

        /// <summary> 新增功能至g_lstFuncLib 資料集中  </summary>
        private bool AddFunc(clsObjFunc p_FuncClass)
        {
            if (clsCmData.g_dctFuncLib.ContainsKey(p_FuncClass.SubFunc))
            {
                return false;
            }

            clsCmData.g_dctFuncLib.Add(p_FuncClass.SubFunc, p_FuncClass);
            ucArtMain_Design.GetSingleton().p_SubFuncPanel.Controls.Add(p_FuncClass.Control);
            //splitContainerSubFunc.Panel1.Controls.Add(p_FuncClass.Control);
            p_FuncClass.Control.Dock = DockStyle.Fill;

            //收集管理按鈕
            _CollectAcmButton(p_FuncClass.SubFunc, p_FuncClass.Control);

            clsLog.Log(clsArtSystem.g_strStartUpLogName, "-> EditFunc()->AddFunc() : " + p_FuncClass.MainFunc + "->" + p_FuncClass.SubFunc
                        + " (" + mTimer_AddSubFunc.ElapsedMilliseconds.ToString("F3") + " ms)");
            return true;
        }

        /// <summary> 檢查程式是否運行中 ,return : true:程式運行中 false:程式未運行 </summary>
        private bool CheckApplicationDuplication()
        {
            string strName;
            strName = string.Join(",", Environment.GetCommandLineArgs());
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(strName);
            strName = myFileVersionInfo.FileName.Substring(strName.LastIndexOf("\\") + 1, (strName.Length - (".exe".Length + 1)) - strName.LastIndexOf("\\"));

            if (strName.Substring(strName.LastIndexOf(".") + 1) != "vshost")
            {
                if (Process.GetProcessesByName(strName).Length > 1)
                {
                    clsArtSystem.bIsARTMMIDuplication = true;
                    formMessageBox.Show("Application Duplication !!", "Execute Error!!!");
                }
            }

            return clsArtSystem.bIsARTMMIDuplication;
        }

        /// <summary> 更新自動登出介面(parent : ucUserAccount) </summary>
        private void ucUserAccount_VisibleChanged(object sender, EventArgs e)
        {
            ucAutoLogout.GetSingleton().UpdateControls();
        }

        /// <summary> 滑鼠移動事件(全域) : 更新自動登出時間 </summary>
        private void gmh_TheMouseMoved()
        {
            if (mPos_Mouse != System.Windows.Forms.Cursor.Position)
            {
                mPos_Mouse.X = System.Windows.Forms.Cursor.Position.X;
                mPos_Mouse.Y = System.Windows.Forms.Cursor.Position.Y;
                Login.fromAutoLogout.GetSingleton().Hide();
                ucAutoLogout.GetSingleton().mAutoLogout_Timer.Restart();
            }
        }

        /// <summary> 把自動登出設定介面加入倒ucUserAccount </summary>
        private void AddAutoLogoutSettingInUserAccount(clsObjFunc p_FuncClass)
        {
            if (p_FuncClass.Control is ucUserAccount)
            {
                try
                {
                    Control[] FindControls = p_FuncClass.Control.Controls.Find("groupBoxUserInfo", true);
                    if (FindControls.Length >= 1)
                    {
                        p_FuncClass.Control.VisibleChanged += new EventHandler(ucUserAccount_VisibleChanged);
                        ucAutoLogout.GetSingleton().Parent = FindControls[0].Parent;
                        ucAutoLogout.GetSingleton().Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                        ucAutoLogout.GetSingleton().Top = FindControls[0].Top + FindControls[0].Height + 30;
                        ucAutoLogout.GetSingleton().Left = FindControls[0].Left;
                        ucAutoLogout.GetSingleton().Width = FindControls[0].Width;
                        ucAutoLogout.GetSingleton().BackColor = FindControls[0].BackColor;
                        ucAutoLogout.GetSingleton().BringToFront();
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }
        }

        /// <summary> 多載系統中重新規劃ucMotionCtrlTab介面 </summary>
        private void Reset_ucMotionSetting(clsObjFunc p_FuncClass)
        {
            if (clsMultiSystem.bIsMultiSystem == true)
            {
                if (p_FuncClass.Control is ucMotionSetting)
                {
                    pUcMotionSetting = (ucMotionSetting)p_FuncClass.Control;
                    try
                    {
                        int iAxisCount = 0;
                        foreach (clsMotionDriver mDriver in clsMotionCtrl.lstMotionDriver)
                        { iAxisCount += mDriver.dctAxisLib.Count; }
                        int PageCount = (iAxisCount / 8) + 1;
                        TabControl tbControl = (TabControl)p_FuncClass.Control.Controls.Find("tabCtrlMotion", true)[0];
                        for (int i = tbControl.TabPages.Count - 1; i >= PageCount; i--)
                        {
                            tbControl.TabPages.RemoveAt(i);
                        }
                    }
                    catch (Exception ex)
                    {
                        clsArtSystem.CatchLog(ex);
                    }
                }
                else if (p_FuncClass.Control is ucSpeedSetting)
                {
                    pUcSpeedSetting = (ucSpeedSetting)p_FuncClass.Control;
                }
            }
        }

        /// <summary> 對ucMotionCtrlTab介面中為0的參數設定預設值 </summary>
        private void Reset_ucMotionSetting_DefaultValue()
        {
            try
            {
                if (pUcMotionSetting != null)
                {
                    #region//如果有參數為0,則填入預設值
                    int iAxisCount = 0;
                    foreach (clsMotionDriver mDriver in clsMotionCtrl.lstMotionDriver)
                    { iAxisCount += mDriver.dctAxisLib.Count; }
                    foreach (clsEnum.enuAxis eAxis in Enum.GetValues(typeof(clsEnum.enuAxis)))
                    {
                        if (iAxisCount == 0)
                        { break; }
                        iAxisCount--;
                        if (ucMotionSetting.GetMmPerCir(eAxis) == 0)
                        {
                            ucMotionSetting.SetValue(eAxis, ucMotionSetting.enuType.MmPerCir, 1);
                            ucMotionSetting.SetValue(eAxis, ucMotionSetting.enuType.MaxSpeed, 100);
                        }
                        if (ucMotionSetting.GetPulsePerCir(eAxis) == 0)
                        {
                            ucMotionSetting.SetValue(eAxis, ucMotionSetting.enuType.PulsePerCir, 1);
                            ucMotionSetting.SetValue(eAxis, ucMotionSetting.enuType.MaxSpeed, 100);
                        }
                        if (ucMotionSetting.GetSacc(eAxis) == 0)
                        {
                            ucMotionSetting.SetValue(eAxis, ucMotionSetting.enuType.Sacc, 0.1);
                        }
                        if (ucMotionSetting.GetTacc(eAxis) == 0)
                        {
                            ucMotionSetting.SetValue(eAxis, ucMotionSetting.enuType.Tacc, 0.3);
                        }
                        if (ucMotionSetting.GetTimeOut(eAxis) == 0)
                        {
                            ucMotionSetting.SetValue(eAxis, ucMotionSetting.enuType.TimeOut, 60000);
                        }
                        if (ucMotionSetting.GetMaxSpeed(eAxis) == 0)
                        {
                        }
                        if (ucMotionSetting.GetStartVel(eAxis) == 0)
                        {
                        }
                    }
                    #endregion
                }
                if (pUcSpeedSetting != null)
                {
                    #region//速度百分比不能為0 (預設10)
                    int iAxisCount = 0;
                    foreach (clsMotionDriver mDriver in clsMotionCtrl.lstMotionDriver)
                    { iAxisCount += mDriver.dctAxisLib.Count; }
                    foreach (clsEnum.enuAxis eAxis in Enum.GetValues(typeof(clsEnum.enuAxis)))
                    {
                        if (iAxisCount == 0)
                        { break; }
                        iAxisCount--;
                        if (ucSpeedSetting.GetSpeedRatio(eAxis) == 0)
                        {
                            ucSpeedSetting.SetSpeedRatio(eAxis, 10);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }


        private double GetINIData_SoftwareOpenTime()
        {
            double rValue = 0;
            string sPath = System.IO.Directory.GetCurrentDirectory() + @"\INI\StartUp.ini";
            if (System.IO.File.Exists(sPath) == true)
            {
                clsIniFile mFile = new clsIniFile(sPath);
                rValue = mFile.GetInt32("StartUp", "ProcessTime_ms", 0);
            }
            return rValue;
        }

        private void SaveINIData_SoftwareOpenTime(double Time_ms)
        {
            string sPath = System.IO.Directory.GetCurrentDirectory() + @"\INI\StartUp.ini";
            clsIniFile mFile = new clsIniFile(sPath);
            mFile.WriteValue("StartUp", "ProcessTime_ms", Time_ms.ToString("F0"));
        }

        #endregion

        #region//===================== 以下為事件處理 (Closing, Closed, SizeChanged) =====================

        /// <summary> 關閉程式前，需要跟使用者確認是否要真的要關閉程式 </summary>
        private void p_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                clsArtSystem.BackupINI();
            }
            e.Cancel = FormClossing();
        }
        /// <summary> 正式關閉程式 </summary>
        private void p_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormClosed();
        }
        /// <summary> 視窗變更,重新調整內部分佈 </summary>
        private void p_Form_SizeChanged(object sender, EventArgs e)
        {
            //_UpdateControls();
            m_bSizeChanging = true;
        }
        /// <summary> Form滑鼠滾輪滑動事件，移動SubForm </summary>
        private void p_Form_MouseWheel(object sender, MouseEventArgs e)
        {
            string sNowSubName = ucSubFunc._strNowSubFuncName;
            if (clsCmData.g_dctFuncLib.ContainsKey(sNowSubName) == true)
            {
                if (clsCmData.g_dctFuncLib[sNowSubName].Control.Tag is bool)
                {
                    if (Convert.ToBoolean(clsCmData.g_dctFuncLib[sNowSubName].Control.Tag) == false)
                    {
                        return;
                    }
                }

            }
            if (e_Design == enuDesign.PC)
            {
                m_PC_Design._Form_MouseWheel(sender, e);
            }
            else if (e_Design == enuDesign.Panel)
            {
                m_Panel_Design._Form_MouseWheel(sender, e);
            }
        }
        /// <summary> 視窗變更,重新調整內部分佈 </summary>
        private void ucArtMain_Design_SizeChanged(object sender, EventArgs e)
        {
            if (e_Design == enuDesign.PC)
            {
                this.m_PC_Design.Visible = true;
                this.m_Panel_Design.Visible = false;
                this.m_PC_Design.Location = new Point(0, 0);
                this.m_PC_Design.Size = this.Size;
                this.m_PC_Design._UpdateControls();
            }
            else if (e_Design == enuDesign.Panel)
            {
                this.m_PC_Design.Visible = false;
                this.m_Panel_Design.Visible = true;
                this.m_Panel_Design.Location = new Point(0, 0);
                this.m_Panel_Design.Size = this.Size;
                this.m_Panel_Design._UpdateControls();
            }
        }
        /// <summary> 參數路徑變更事件，紀錄Log及BackupINI </summary>
        private void ucArtMain_Design_PathChanged(ucParameter.PathEventArgs e)
        {
            clsArtSystem.BackupINI();
            _ArtMainFunc(enuFunc.ParameterPathChange);
            foreach (clsEnum.enuPmtType eType in Enum.GetValues(typeof(clsEnum.enuPmtType)))
            {
                if (e.Type == eType.ToString())
                {
                    string sPath = ucParameter.GetFilePath(eType);
                    clsLog.Log(clsCmData.enuLogType.SystemLog, "[Path Changed] " + eType.ToString() + " -> " + sPath);
                    break;
                }
            }
        }

        #endregion
    }
}



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtControlLib;
using ArtCommonLib;
using ArtData;

namespace ArtSystem.FormDesign
{
    public partial class ucSubFunc : ucBaseUserControl
    {
        #region //===================== static 變數設置 =====================

        /// <summary> 目前顯示中的主功能頁面名稱 </summary>
        static private string strNowMainFunctionName = "";
        /// <summary> 目前顯示中的主功能頁面名稱 </summary>
        static public string _strNowMainFuncName
        {
            get
            {
                return strNowMainFunctionName;
            }
        }

        /// <summary> 目前顯示中的子功能頁面名稱 </summary>
        static private string strNowSubFunctionName = "";
        /// <summary> 目前顯示中的子功能頁面名稱 </summary>
        static public string _strNowSubFuncName
        {
            get
            {
                return strNowSubFunctionName;
            }
        }

        #endregion

        #region //===================== 變數設置 =====================

        /// <summary> 此SubForm對應主功能頁面的名稱 </summary>
        private string m_strMainFuncName;
        /// <summary> 此SubForm對應主功能頁面的名稱 </summary>
        public string strMainFuncName
        {
            get
            {
                return m_strMainFuncName;
            }
        }

        /// <summary> 此SubForm擁有子功能頁面的數量 </summary>
        private int m_iFuncCount;
        /// <summary> 此SubForm擁有子功能頁面的數量 </summary>
        public int iFuncCount
        {
            get
            {
                return m_iFuncCount;
            }
        }

        /// <summary> 此SubForm對應的子功能頁面 </summary>
        private clsDictionary<string, clsObjFunc> m_dctFuncClass = new clsDictionary<string, clsObjFunc>();
        /// <summary> 此SubForm對應的子功能頁面 </summary>
        public clsDictionary<string, clsObjFunc> dctFuncClass
        {
            get
            {
                return m_dctFuncClass;
            }
        }

        private float fScale = 1;
        #endregion

        #region //===================== 必要函式設置 =====================

        /// <summary> 子功能頁面唯一物件 </summary>
        static private clsDictionary<string, ucSubFunc> m_Singleton = new clsDictionary<string, ucSubFunc>();
        /// <summary> 建立子功能頁面 </summary> <param name="p_strMainFuncName">主功能名稱</param> <returns>回傳子功能頁面</returns>
        static public ucSubFunc GetSingleton(string p_strMainFuncName)
        {
            if (m_Singleton.ContainsKey(p_strMainFuncName) == false)
            {
                m_Singleton.Add(p_strMainFuncName, new ucSubFunc(p_strMainFuncName));
            }
            return m_Singleton[p_strMainFuncName];
        }
        /// <summary> 建構式 </summary>
        public ucSubFunc(string p_strMainFuncName)
        {
            InitializeComponent();
            m_strMainFuncName = p_strMainFuncName;
            ReloadSubFunc();

        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                ReloadSubFunc();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }


        /// <summary> 自動更新介面參數 (沒有開啟) </summary>
        protected override void ReflashTimerFunc()
        {
            try
            {
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        /// <summary> 顯示子功能清單頁面</summary>
        public void _showSubFuncListPanel()
        {
            m_Singleton[m_strMainFuncName].Visible = true;

            for (int iSingletonNum = 0; iSingletonNum < m_Singleton.Count; iSingletonNum++)
            {
                if (m_Singleton.ElementAt(iSingletonNum).Value.strMainFuncName == m_strMainFuncName)
                {
                    continue;
                }
                m_Singleton.ElementAt(iSingletonNum).Value.Visible = false;
            }
        }

        /// <summary> 顯示子功能頁面</summary>
        public void _ShowSubFunc(string p_strSubFuncName)
        {
            if(this.InvokeRequired == true)
            {
                this.BeginInvoke(new Action(() =>
                {
                    _ShowSubFunc(p_strSubFuncName);
                }));
            }
            else
            {
                this.Visible = true;
                this.BringToFront();
                Login.ucAutoLogout.GetSingleton().mAutoLogout_Timer.Restart();
                //顏色設置
                for (int iControlNum = 0; iControlNum < this.Controls.Count; iControlNum++)
                {
                    if (this.Controls[iControlNum].Name == p_strSubFuncName)
                    {
                        ((ucRoundButton)this.Controls[iControlNum])._Color = Color.FromArgb(53, 64, 79);
                        ((ucRoundButton)this.Controls[iControlNum])._TextColor = SystemColors.Control;
                        ((ucRoundButton)this.Controls[iControlNum])._MouseOnColor = Color.FromArgb(53, 64, 79);
                    }
                    else
                    {
                        ((ucRoundButton)this.Controls[iControlNum])._Color = Color.FromArgb(219, 228, 236);
                        ((ucRoundButton)this.Controls[iControlNum])._TextColor = Color.FromArgb(51, 71, 100);
                        ((ucRoundButton)this.Controls[iControlNum])._MouseOnColor = Color.FromArgb(179, 188, 196);
                    }
                }

                if (clsCmData.g_dctFuncLib.ContainsKey(p_strSubFuncName))
                {
                    strNowMainFunctionName = m_strMainFuncName;
                    strNowSubFunctionName = p_strSubFuncName;

                    //重置上下滾輪頁面(SubForm)
                    ucArtMain_Design.GetSingleton()._ResetScrollBar();


                    //開始更新顯示頁面參數
                    ((ucBaseUserControl)clsCmData.g_dctFuncLib[p_strSubFuncName].Control).SetReflashTimerStart(true);

                    //更新參數頁面數值
                    if (ucParameter.lstUserControl.Contains(clsCmData.g_dctFuncLib[p_strSubFuncName].Control))
                    {
                        ucParameter.RefreshControlData(clsCmData.g_dctFuncLib[p_strSubFuncName].Control);
                    }

                    SetDefaultShowPanel(p_strSubFuncName);

                    //隱藏不顯示的頁面
                    HideUnprocPanel(clsCmData.g_dctFuncLib[p_strSubFuncName].Control);


                    clsCmData.g_dctFuncLib[p_strSubFuncName].Control.Parent = ucArtMain_Design.GetSingleton().p_SubFuncPanel;
                    clsCmData.g_dctFuncLib[p_strSubFuncName].Control.Location = new Point(0, 0);
                    clsCmData.g_dctFuncLib[p_strSubFuncName].Control.Size = ucArtMain_Design.GetSingleton().p_SubFuncPanel.ClientSize;
                    clsCmData.g_dctFuncLib[p_strSubFuncName].Control.BringToFront();
                    clsCmData.g_dctFuncLib[p_strSubFuncName].Control.Visible = true;
                }

            }
        }

        /// <summary> 隱藏所有子功能頁面</summary>
        public void _HideAllSubFunc()
        {
            for (int iFuncNum = 0; iFuncNum < clsCmData.g_dctFuncLib.Count; iFuncNum++)
            {
                clsCmData.g_dctFuncLib.ElementAt(iFuncNum).Value.Control.Visible = false;
                ((ucBaseUserControl)clsCmData.g_dctFuncLib.ElementAt(iFuncNum).Value.Control).SetReflashTimerStart(false);
            }
        }

        #endregion

        #region //===================== private 函式設置 =====================
        /// <summary> 新增SubFunc頁面呼叫Button </summary>
        private bool AddSubFuncButton(string p_strSubFuncName)
        {
            //新增SubFunc按鈕
            ucRoundButton btnSubFunc = new ucRoundButton();
            btnSubFunc.Size = new Size(90, 50);
            btnSubFunc.Location = new Point(5, 5 + (50 + 6) * m_iFuncCount);
            btnSubFunc.Name = p_strSubFuncName;
            btnSubFunc.Text = clsLanguage.GetTranslation(p_strSubFuncName);
            btnSubFunc._Color = Color.FromArgb(219, 228, 236);
            btnSubFunc._EdgeColor = Color.FromArgb(183, 199, 217);
            btnSubFunc._TextColor = Color.FromArgb(183, 199, 217);
            btnSubFunc._NeedEdge = true;
            btnSubFunc._MouseOnColor = Color.FromArgb(179, 188, 196);
            btnSubFunc._Radius = 6;
            //btnSubFunc.Text = p_strSubFuncName;
            //btnSubFunc.UseVisualStyleBackColor = true;
            //btnSubFunc.FlatStyle = FlatStyle.Flat;
            btnSubFunc.Click += new EventHandler(btnSubFunc_Click);
            btnSubFunc.Visible = true;
            this.Controls.Add(btnSubFunc);
            if (ucArtMain_Design.GetSingleton().p_SubFuncPanel != null)
            {
                try
                {
                    this.Top = 0;
                    this.Left = 0;
                    this.Width = ucArtMain_Design.GetSingleton().p_SubFuncPanel.Width;
                    this.Height = ucArtMain_Design.GetSingleton().p_SubFuncPanel.Height;
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }
            return true;
        }

        /// <summary> 隱藏不顯示的頁面 </summary>
        private void HideUnprocPanel(Control p_ProcPanel)
        {
            for (int iFuncNum = 0; iFuncNum < clsCmData.g_dctFuncLib.Count; iFuncNum++)
            {
                if (clsCmData.g_dctFuncLib.ElementAt(iFuncNum).Value.Control != p_ProcPanel)
                {
                    clsCmData.g_dctFuncLib.ElementAt(iFuncNum).Value.Control.Visible = false;

                    if (clsCmData.g_dctFuncLib.ElementAt(iFuncNum).Value.Control is ucBaseUserControl)
                    {
                        ((ucBaseUserControl)clsCmData.g_dctFuncLib.ElementAt(iFuncNum).Value.Control).SetReflashTimerStart(false);
                    }
                }
            }
        }

        /// <summary> 將目前頁面設為預設頁面，這樣離開MainFunc回來時還會保留開啟此介面 </summary>
        private void SetDefaultShowPanel(string p_strSubFuncName)
        {
            for (int iFuncNum = 0; iFuncNum < m_dctFuncClass.Count; iFuncNum++)
            {
                if (m_dctFuncClass.ElementAt(iFuncNum).Value.SubFunc == p_strSubFuncName)
                {
                    m_dctFuncClass.ElementAt(iFuncNum).Value.IsDefaultShowPanel = true;
                }
                else
                {
                    m_dctFuncClass.ElementAt(iFuncNum).Value.IsDefaultShowPanel = false;
                }
            }
        }

        /// <summary> 整理子功能按鈕 </summary> <returns>true:整理完成 false:資料發生異常</returns>
        private bool ReloadSubFunc()
        {
            try
            {
                m_dctFuncClass.Clear();
                this.Controls.Clear();
                m_iFuncCount = 0;

                for (int iFuncNum = 0; iFuncNum < clsCmData.g_dctFuncLib.Count; iFuncNum++)
                {
                    if (clsCmData.g_dctFuncLib.ElementAt(iFuncNum).Value.MainFunc == m_strMainFuncName)
                    {
                        string strSubFuncName = clsCmData.g_dctFuncLib.ElementAt(iFuncNum).Value.SubFunc;
                        bool bIsUsable = ucUserAccount.GetSingleton().IsSubFuncUsable(clsCmData.g_dctFuncLib.ElementAt(iFuncNum).Key);

                        if (bIsUsable)
                        {
                            //新增SubFunc頁面呼叫Button
                            AddSubFuncButton(strSubFuncName);
                            m_iFuncCount++;
                        }

                        //整理子功能資料集
                        m_dctFuncClass.Add(strSubFuncName, clsCmData.g_dctFuncLib[strSubFuncName]);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return false;
        }

        #endregion

        #region //===================== 以下為事件處理 =====================

        /// <summary> 開啟SubFunct介面Button_Click事件 </summary>
        private void btnSubFunc_Click(object sender, EventArgs e)
        {
            clsArtSystem.BackupINI();
            if (ucArtMain_Design.GetSingleton()._ArtMainFunc(ucArtMain_Design.enuFunc.ChangePagePremit) == false)
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog.ToString(), clsCmData.g_strNowUser + " Press ucSubFunc : " + strMainFuncName + "->" + strNowSubFunctionName);
                clsLog.Log(clsCmData.enuLogType.ButtonLog.ToString(), "[Access Denied] Back to Page : " + ucSubFunc._strNowMainFuncName + "->" + ucSubFunc._strNowSubFuncName);
                return;
            }
            clsLog.Log(clsCmData.enuLogType.ButtonLog.ToString(), clsCmData.g_strNowUser + " Press ucSubFunc : " + strMainFuncName + "->" + strNowSubFunctionName);
            _ShowSubFunc(((Control)sender).Name);
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Diagnostics;

using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtProcModuleLib;

namespace ArtEQ
{
    public partial class formDebug : Form
    {
        #region //=====================  區域變數設置 =====================
        static formDebug m_Singleton;
        ucThreadLog threadLog;

        ucDioMonitor DioMonitor = null;
        #endregion


        #region //=====================  必要函式設置 =====================
        /// <summary>
        /// 取得唯一物件，避免重覆設置
        /// </summary>
        /// <returns>回傳物件</returns>
        public static formDebug GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new formDebug();
                ArtSystem.ucArtMain_Design.GetSingleton()._CollectChangeLanguageControls(m_Singleton);//只能放在這裡(不然很多流程還沒建立,裡面會是空的)
            }
            return m_Singleton;
        }


        /// <summary>
        /// 物件建立請利用 GetSingleton()，除非特殊需求
        /// </summary>
        private formDebug()
        {
            InitializeComponent();

            threadLog = new ucThreadLog();
            threadLog.Dock = DockStyle.Fill;
            tabPage1.Controls.Add(threadLog);

            DioMonitor = new ucDioMonitor(16, true);
            DioMonitor.Dock = DockStyle.Fill;
            tabPage2.Controls.Add(DioMonitor);

            ucStepLog StepLog = new ucStepLog();
            StepLog.Dock = DockStyle.Fill;
            tabPage3.Controls.Add(StepLog);

            this.Deactivate += new EventHandler(formDebug_Deactivate);
        }


        /// <summary>
        /// 設置畫面
        /// </summary>
        private void UpdateControls()
        {
            //threadLog.UpdateControls();
            DioMonitor.UpdateControls();
        }

        #endregion


        #region //===================== public 函式設置 =====================
        /// <summary>
        /// 顯示異常報告視窗
        /// </summary>
        /// <param name="p_objControl">依附視窗</param>
        public void ShowDebugForm(IWin32Window p_objControl)
        {
            if (formDebug.GetSingleton().Visible == false)
            {
                formDebug.GetSingleton().Show(p_objControl);
                try
                {
                    Control[] FindControls = threadLog.Controls.Find("btnThreadLogRefresh", true);
                    if (FindControls.Length > 0)
                    {
                        if (FindControls[0] is Button)
                        {
                            ((Button)FindControls[0]).PerformClick();
                        }
                    }
                    UpdateControls();
                }
                catch (Exception ex)
                {
                    clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
                }
            }
            else
            {
                UpdateControls();
                formDebug.GetSingleton().Activate();
                formDebug.GetSingleton().WindowState = FormWindowState.Normal;
                formDebug.GetSingleton().Focus();
            }         
        }
        #endregion


        #region //===================== private 函式設置 =====================
      

        #endregion


        #region //===================== 以下為事件處理 =====================

        private void formDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
            this.Hide();
        }
        private void formDebug_Deactivate(object sender, EventArgs e)
        {
            if (clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Run
                || clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Initial)
            {
                this.Visible = false;
                this.Hide();
            }
        }
        #endregion
    }
}

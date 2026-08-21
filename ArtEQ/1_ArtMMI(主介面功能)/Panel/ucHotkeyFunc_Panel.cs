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

namespace ArtEQ
{
    public partial class ucHotkeyFunc_Panel : ucBaseUserControl
    {
        #region //=====================  必要函式設置 =====================

        static private ucHotkeyFunc_Panel m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucHotkeyFunc_Panel GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucHotkeyFunc_Panel();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucHotkeyFunc_Panel()
        {
            InitializeComponent();
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        /// <summary> 自動更新介面參數 </summary>
        protected override void ReflashTimerFunc()
        {
            try
            {
                if (clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Run)
                {
                    btnRun.Enabled = false;
                    btnInitial.Enabled = false; 
                }
                else
                {
                    btnRun.Enabled = ArtSystem.FormDesign.ucSubFunc._strNowMainFuncName == clsCmData.enuMainFunc.Operation.ToString();
                    btnInitial.Enabled = ArtSystem.FormDesign.ucSubFunc._strNowMainFuncName == clsCmData.enuMainFunc.Operation.ToString();
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }
        #endregion

        #region//===================== 以下為事件處理 =====================
        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (e is MouseEventArgs)
                {
                    Control Item = (Control)sender;
                    clsLog.Log(clsEnum.enuLogName.ButtonLog, clsCmData.g_strNowUser + " : " + this.Name + " -> " + ((Control)sender).Name + ", Clicked");
                    clsEditRunThread.EqRun(false);
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (e is MouseEventArgs)
                {
                    Control Item = (Control)sender;
                    clsLog.Log(clsEnum.enuLogName.ButtonLog, clsCmData.g_strNowUser + " : " + this.Name + " -> " + ((Control)sender).Name + ", Clicked");
                    clsEditRunThread.EqStop();
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        private void btnInitial_Click(object sender, EventArgs e)
        {
            try
            {
                if (e is MouseEventArgs)
                {
                    Control Item = (Control)sender;
                    clsLog.Log(clsEnum.enuLogName.ButtonLog, clsCmData.g_strNowUser + " : " + this.Name + " -> " + ((Control)sender).Name + ", Clicked");
                    clsEditRunThread.Initial();
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }
        #endregion
    }
}

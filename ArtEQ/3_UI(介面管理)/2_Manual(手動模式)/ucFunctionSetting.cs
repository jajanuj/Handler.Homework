using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtProcModuleLib;
using ArtControlLib;
using ArtCommonLib;
using ArtData;
using ArtSystem;

namespace ArtEQ
{
    public partial class ucFunctionSetting : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================

        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucFunctionSetting m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucFunctionSetting GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucFunctionSetting();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucFunctionSetting()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            ucParameter.Add(this);
            ucParameter.SaveValue(clsEnum.enuPmtType.System, clsEnum.enuPmtName.Sys_MachineDryRun, 0);
            ucParameter.SaveValue(clsEnum.enuPmtType.System, clsEnum.enuPmtName.Sys_EnableSafeDoor, 1);
            clsProcCtrl.GetSingleton().g_bSoftSimulate = clsArtSystem.bIsSoftwareSimulate;
            this.TimerInterval = 100;
           
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                if (ucParameter.GetValueDouble(clsEnum.enuPmtName.Sys_Timeout_HandShank) == 0)
                { ucParameter.SaveValue(clsEnum.enuPmtType.System, clsEnum.enuPmtName.Sys_Timeout_HandShank, 30000); }
                if (ucParameter.GetValueDouble(clsEnum.enuPmtName.Sys_Timeout_LaneTransfer) == 0)
                {  ucParameter.SaveValue(clsEnum.enuPmtType.System, clsEnum.enuPmtName.Sys_Timeout_HandShank, 30000); }

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
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        private void ucMachineStatus_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                this.UpdateControls();
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================


        #endregion

        #region //===================== private 函式設置 =====================

        #endregion

        #region//===================== 以下為事件處理 =====================

        #endregion
    }
}

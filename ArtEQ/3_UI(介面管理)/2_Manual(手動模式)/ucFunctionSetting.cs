using System;
using ArtCommonLib;
using ArtControlLib;
using ArtProcModuleLib;
using ArtSystem;
using static ArtData.clsEnum;
using comNumBox = ArtControlLib.comNumBox;

namespace ArtEQ
{
    public partial class ucFunctionSetting : ucBaseUserControl
    {
        #region Private Methods

        private void cboSortType_DropDownClosed(object sender, EventArgs e)
        {
            comNumBox1._Value = cboNgDischargeMode.SelectedIndex;
        }

        private void comNumBox1_TextChanged(object sender, EventArgs e)
        {
            if (sender is comNumBox comNumBox)
            {
                cboNgDischargeMode.SelectedIndex = (int)comNumBox._Value;
            }
        }

        #endregion

        #region //=====================  區域變數設置 =====================

        #endregion

        #region //=====================  必要函式設置 =====================

        private static ucFunctionSetting m_Singleton;

        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        public static ucFunctionSetting GetSingleton()
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
            {
                return;
            }

            ucParameter.Add(this);
            ucParameter.SaveValue(enuPmtType.System, enuPmtName.Sys_MachineDryRun, 0);
            ucParameter.SaveValue(enuPmtType.System, enuPmtName.Sys_EnableSafeDoor, 1);
            clsProcCtrl.GetSingleton().g_bSoftSimulate = clsArtSystem.bIsSoftwareSimulate;

            BindComboBox();
            TimerInterval = 100;
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                if (ucParameter.GetValueDouble(enuPmtName.Sys_Timeout_HandShank) == 0)
                {
                    ucParameter.SaveValue(enuPmtType.System, enuPmtName.Sys_Timeout_HandShank, 30000);
                }

                if (ucParameter.GetValueDouble(enuPmtName.Sys_Timeout_LaneTransfer) == 0)
                {
                    ucParameter.SaveValue(enuPmtType.System, enuPmtName.Sys_Timeout_HandShank, 30000);
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
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
                clsLog.Log(enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        private void ucMachineStatus_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                UpdateControls();
            }
        }

        private void BindComboBox()
        {
            //cboNgDischargeMode.DataSource = Enum.GetNames(typeof(NGDischargeMode));
            cboNgDischargeMode.SelectedIndex = (int)comNumBox1._Value;
        }

        #endregion

        #region //===================== public 函式設置 =====================

        #endregion

        #region //===================== private 函式設置 =====================

        #endregion

        #region //===================== 以下為事件處理 =====================

        #endregion
    }
}
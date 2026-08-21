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
using ArtSystem;

namespace ArtSystem.MultiSystem
{
    public partial class ucDeviceSetting : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================

        private bool bIsEditing = false;
        public clsCardInfo mCardInfo = new clsCardInfo();

        #endregion

        #region //=====================  必要函式設置 =====================

        static object m_LockObj = new object();
        static private ucDeviceSetting m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucDeviceSetting GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucDeviceSetting();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucDeviceSetting()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            this.TimerInterval = 100;
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                if (this.Parent != null)
                {
                    this.Size = this.Parent.ClientSize;
                    ucRollerMotorSetting.GetSingleton().Parent = tPage_RollerMotor;
                    ucHighSensorSetting.GetSingleton().Parent = tPage_HighSensor;
                    ucReaderSetting.GetSingleton().Parent = tPage_Reader;
                    ucTCPLinkSetting.GetSingleton().Parent = tPage_TCPLink;
                    ucHeaterModuleSetting.GetSingleton().Parent = tPage_HeaterModule;
                    ucWeightScaleSetting.GetSingleton().Parent = tPage_WeightScale;
                    ucDispValveSetting.GetSingleton().Parent = tPage_DispValve;
                    ucBottomCCDSetting.GetSingleton().Parent = tPage_BottomCCD;

                    ucRollerMotorSetting.GetSingleton().Dock = DockStyle.Fill;
                    ucHighSensorSetting.GetSingleton().Dock = DockStyle.Fill;
                    ucReaderSetting.GetSingleton().Dock = DockStyle.Fill;
                    ucTCPLinkSetting.GetSingleton().Dock = DockStyle.Fill;
                    ucHeaterModuleSetting.GetSingleton().Dock = DockStyle.Fill;
                    ucWeightScaleSetting.GetSingleton().Dock = DockStyle.Fill;
                    ucDispValveSetting.GetSingleton().Dock = DockStyle.Fill;
                    ucBottomCCDSetting.GetSingleton().Dock = DockStyle.Fill;

                    ucRollerMotorSetting.GetSingleton().SetReflashTimerStart(false);
                    ucHighSensorSetting.GetSingleton().SetReflashTimerStart(false);
                    ucReaderSetting.GetSingleton().SetReflashTimerStart(false);
                    ucRollerMotorSetting.GetSingleton().SetReflashTimerStart(false);
                    ucHeaterModuleSetting.GetSingleton().SetReflashTimerStart(false);
                    ucWeightScaleSetting.GetSingleton().SetReflashTimerStart(false);
                    ucDispValveSetting.GetSingleton().SetReflashTimerStart(false);
                    ucBottomCCDSetting.GetSingleton().SetReflashTimerStart(false);

                    if (tabControl1.SelectedTab == tPage_HighSensor)
                    {
                        ucHighSensorSetting.GetSingleton().mPmt.Load(ucHighSensorSetting.GetSingleton().mPmt.sINIPath);
                        ucHighSensorSetting.GetSingleton().UpdateControls();
                        ucHighSensorSetting.GetSingleton().SetReflashTimerStart(true);
                    }
                    else if (tabControl1.SelectedTab == tPage_Reader)
                    {
                        ucReaderSetting.GetSingleton().mPmt.Load(ucReaderSetting.GetSingleton().mPmt.sINIPath);
                        ucReaderSetting.GetSingleton().UpdateControls();
                        ucReaderSetting.GetSingleton().SetReflashTimerStart(true);
                    }
                    else if (tabControl1.SelectedTab == tPage_RollerMotor)
                    {
                        ucRollerMotorSetting.GetSingleton().mPmt.Load(ucRollerMotorSetting.GetSingleton().mPmt.sINIPath);
                        ucRollerMotorSetting.GetSingleton().UpdateControls();
                        ucRollerMotorSetting.GetSingleton().SetReflashTimerStart(true);
                    }
                    else if (tabControl1.SelectedTab == tPage_TCPLink)
                    {
                        ucTCPLinkSetting.GetSingleton().mPmt.Load(ucTCPLinkSetting.GetSingleton().mPmt.sINIPath);
                        ucTCPLinkSetting.GetSingleton().UpdateControls();
                        ucTCPLinkSetting.GetSingleton().SetReflashTimerStart(true);
                    }
                    else if (tabControl1.SelectedTab == tPage_HeaterModule)
                    {
                        ucHeaterModuleSetting.GetSingleton().mPmt.Load(ucHeaterModuleSetting.GetSingleton().mPmt.sINIPath);
                        ucHeaterModuleSetting.GetSingleton().UpdateControls();
                        ucHeaterModuleSetting.GetSingleton().SetReflashTimerStart(true);
                    }
                    else if (tabControl1.SelectedTab == tPage_WeightScale)
                    {
                        ucWeightScaleSetting.GetSingleton().mPmt.Load(ucWeightScaleSetting.GetSingleton().mPmt.sINIPath);
                        ucWeightScaleSetting.GetSingleton().UpdateControls();
                        ucWeightScaleSetting.GetSingleton().SetReflashTimerStart(true);
                    }
                    else if (tabControl1.SelectedTab == tPage_DispValve)
                    {
                        ucDispValveSetting.GetSingleton().mPmt.Load(ucDispValveSetting.GetSingleton().mPmt.sINIPath);
                        ucDispValveSetting.GetSingleton().UpdateControls();
                        ucDispValveSetting.GetSingleton().SetReflashTimerStart(true);
                    }
                    else if (tabControl1.SelectedTab == tPage_BottomCCD)
                    {
                        ucBottomCCDSetting.GetSingleton().mPmt.Load(ucBottomCCDSetting.GetSingleton().mPmt.sINIPath);
                        ucBottomCCDSetting.GetSingleton().UpdateControls();
                        ucBottomCCDSetting.GetSingleton().SetReflashTimerStart(true);
                    }
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
                
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        #endregion

        #region //===================== private 函式設置 (SetBtnColor, Covert-Data&UI) =====================

        #endregion

        #region//===================== 以下為事件處理 (VisibleChanged, tabPageChanged, dgvEnableChanged) =====================

        private void ucDeviceSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                UpdateControls();
            }
            else
            {
                ucRollerMotorSetting.GetSingleton().SetReflashTimerStart(false);
                ucHighSensorSetting.GetSingleton().SetReflashTimerStart(false);
                ucReaderSetting.GetSingleton().SetReflashTimerStart(false);
                ucRollerMotorSetting.GetSingleton().SetReflashTimerStart(false);
                ucHeaterModuleSetting.GetSingleton().SetReflashTimerStart(false);
                ucWeightScaleSetting.GetSingleton().SetReflashTimerStart(false);
            }
            this.SetReflashTimerStart(this.Visible);
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        #endregion

    }
}

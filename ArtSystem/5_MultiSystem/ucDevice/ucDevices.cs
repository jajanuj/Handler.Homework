using ArtCommonLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ArtSystem.MultiSystem
{
    public partial class ucDevices : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================

        private bool bIsEditing = false;
        public clsCardInfo mCardInfo = new clsCardInfo();

        private Dictionary<string, TabPage> DicDeviceTabPage = new Dictionary<string, TabPage>();

        #endregion

        #region //=====================  必要函式設置 =====================

        static object m_LockObj = new object();
        static private ucDevices m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucDevices GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucDevices();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucDevices()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            this.TimerInterval = 100;
        }

        private void DisplayTabPages(string sName, ucBaseUserControl Sender, bool Display)
        {
            if (DicDeviceTabPage.ContainsKey(sName) == false)
            {
                DicDeviceTabPage.Add(sName, new TabPage());
            }
            if (Display == true)
            {
                DicDeviceTabPage[sName].Text = clsLanguage.GetTranslation(sName);
                DicDeviceTabPage[sName].Parent = tabControl1;
                Sender.Parent = DicDeviceTabPage[sName];
                Sender.Location = new Point(0, 0);
                Sender.Size = DicDeviceTabPage[sName].ClientSize;
            }
            else
            {
                DicDeviceTabPage[sName].Parent = null;
            }
        }
        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                if (this.Parent != null)
                {
                    this.Size = this.Parent.ClientSize;
                    DisplayTabPages("Roller Motor", ucRollerMotor.GetSingleton(), ucRollerMotorSetting.GetSingleton().mPmt.mDic_CtrlRollerMotor.Count > 0);
                    DisplayTabPages("High Sensor", ucHighSensor.GetSingleton(), ucHighSensorSetting.GetSingleton().mPmt.mDic_CtrlHighSensor.Count > 0);
                    DisplayTabPages("Reader", ucReader.GetSingleton(), ucReaderSetting.GetSingleton().mPmt.mDic_CtrlReader.Count > 0);
                    DisplayTabPages("TCPLink", ucTCPLink.GetSingleton(), ucTCPLinkSetting.GetSingleton().mPmt.mDic_CtrlTCPLink.Count > 0);
                    DisplayTabPages("Heater Module", ucHeaterModule.GetSingleton(), ucHeaterModuleSetting.GetSingleton().mPmt.mDic_CtrlHeaterModule.Count > 0);
                    DisplayTabPages("Weight Scale", ucWeightScale.GetSingleton(), ucWeightScaleSetting.GetSingleton().mPmt.mDic_CtrlWeightScale.Count > 0);
                    DisplayTabPages("Disp Valve", ucDispValve.GetSingleton(), ucDispValveSetting.GetSingleton().mPmt.mDic_CtrlDispValve.Count > 0);
                    DisplayTabPages("AIO Tune", ucAIOTune.GetSingleton(), ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue.Count > 0
                                                || ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue.Count > 0);
                    DisplayTabPages("Bottom CCD", ucBottomCCD.GetSingleton(), ucBottomCCDSetting.GetSingleton().mPmt.mDic_CtrlBottomCCD.Count > 0);
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

        public void HardwareInitial()
        {
            try
            {
                ucRollerMotorSetting.GetSingleton().mPmt.InitialMotionRoller();
                ucHighSensorSetting.GetSingleton().mPmt.InitialHighSensor();
                ucHeaterModuleSetting.GetSingleton().mPmt.InitialHeaterModule();
                ucReaderSetting.GetSingleton().mPmt.InitialReader();
                ucTCPLinkSetting.GetSingleton().mPmt.InitialTCPLink();
                ucWeightScaleSetting.GetSingleton().mPmt.InitialWeightScale();
                ucDispValveSetting.GetSingleton().mPmt.InitialDispValve();
                ucAIOTune.GetSingleton().mPmt.InitialAIOTune();
                ucBottomCCDSetting.GetSingleton().mPmt.InitialBottomCCD();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void HadrwareDestroy()
        {
            try
            {
                ucHighSensorSetting.GetSingleton().mPmt.Release();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region //===================== public 函式設置 =====================

        public clsCtrlRollerMotor GetDevice_RollerMotor(int iIndex)
        {
            clsCtrlRollerMotor rValue = null;
            if (iIndex >= 0 && iIndex < ucRollerMotorSetting.GetSingleton().mPmt.mDic_CtrlRollerMotor.Count)
            {
                rValue = ucRollerMotorSetting.GetSingleton().mPmt.mDic_CtrlRollerMotor.ElementAt(iIndex).Value;
            }
            return rValue;
        }
        public clsCtrlRollerMotor GetDevice_RollerMotor(string sIndex)
        {
            clsCtrlRollerMotor rValue = null;
            if (ucRollerMotorSetting.GetSingleton().mPmt.mDic_CtrlRollerMotor.ContainsKey(sIndex) == true)
            {
                rValue = ucRollerMotorSetting.GetSingleton().mPmt.mDic_CtrlRollerMotor[sIndex];
            }
            return rValue;
        }

        public clsCtrlHighSensor GetDevice_HighSensor(int iIndex)
        {
            clsCtrlHighSensor rValue = null;
            if (iIndex >= 0 && iIndex < ucHighSensorSetting.GetSingleton().mPmt.mDic_CtrlHighSensor.Count)
            {
                rValue = ucHighSensorSetting.GetSingleton().mPmt.mDic_CtrlHighSensor.ElementAt(iIndex).Value;
            }
            return rValue;
        }
        public clsCtrlHighSensor GetDevice_HighSensor(string sIndex)
        {
            clsCtrlHighSensor rValue = null;
            if (ucHighSensorSetting.GetSingleton().mPmt.mDic_CtrlHighSensor.ContainsKey(sIndex) == true)
            {
                rValue = ucHighSensorSetting.GetSingleton().mPmt.mDic_CtrlHighSensor[sIndex];
            }
            return rValue;
        }

        public clsCtrlReader GetDevice_Reader(int iIndex)
        {
            clsCtrlReader rValue = null;
            if (iIndex >= 0 && iIndex < ucReaderSetting.GetSingleton().mPmt.mDic_CtrlReader.Count)
            {
                rValue = ucReaderSetting.GetSingleton().mPmt.mDic_CtrlReader.ElementAt(iIndex).Value;
            }
            return rValue;
        }
        public clsCtrlReader GetDevice_Reader(string sIndex)
        {
            clsCtrlReader rValue = null;
            if (ucReaderSetting.GetSingleton().mPmt.mDic_CtrlReader.ContainsKey(sIndex) == true)
            {
                rValue = ucReaderSetting.GetSingleton().mPmt.mDic_CtrlReader[sIndex];
            }
            return rValue;
        }

        public clsCtrlTCPLink GetDevice_TCPLink(int iIndex)
        {
            clsCtrlTCPLink rValue = null;
            if (iIndex >= 0 && iIndex < ucTCPLinkSetting.GetSingleton().mPmt.mDic_CtrlTCPLink.Count)
            {
                rValue = ucTCPLinkSetting.GetSingleton().mPmt.mDic_CtrlTCPLink.ElementAt(iIndex).Value;
            }
            return rValue;
        }
        public clsCtrlTCPLink GetDevice_TCPLink(string sIndex)
        {
            clsCtrlTCPLink rValue = null;
            if (ucTCPLinkSetting.GetSingleton().mPmt.mDic_CtrlTCPLink.ContainsKey(sIndex) == true)
            {
                rValue = ucTCPLinkSetting.GetSingleton().mPmt.mDic_CtrlTCPLink[sIndex];
            }
            return rValue;
        }

        public clsCtrlHeaterModule GetDevice_HeaterModule(int iIndex)
        {
            clsCtrlHeaterModule rValue = null;
            if (iIndex >= 0 && iIndex < ucHeaterModuleSetting.GetSingleton().mPmt.mDic_CtrlHeaterModule.Count)
            {
                rValue = ucHeaterModuleSetting.GetSingleton().mPmt.mDic_CtrlHeaterModule.ElementAt(iIndex).Value;
            }
            return rValue;
        }
        public clsCtrlHeaterModule GetDevice_HeaterModule(string sIndex)
        {
            clsCtrlHeaterModule rValue = null;
            if (ucHeaterModuleSetting.GetSingleton().mPmt.mDic_CtrlHeaterModule.ContainsKey(sIndex) == true)
            {
                rValue = ucHeaterModuleSetting.GetSingleton().mPmt.mDic_CtrlHeaterModule[sIndex];
            }
            return rValue;
        }

        public clsCtrlWeightScale GetDevice_WeightScale(int iIndex)
        {
            clsCtrlWeightScale rValue = null;
            if (iIndex >= 0 && iIndex < ucWeightScaleSetting.GetSingleton().mPmt.mDic_CtrlWeightScale.Count)
            {
                rValue = ucWeightScaleSetting.GetSingleton().mPmt.mDic_CtrlWeightScale.ElementAt(iIndex).Value;
            }
            return rValue;
        }
        public clsCtrlWeightScale GetDevice_WeightScale(string sIndex)
        {
            clsCtrlWeightScale rValue = null;
            if (ucWeightScaleSetting.GetSingleton().mPmt.mDic_CtrlWeightScale.ContainsKey(sIndex) == true)
            {
                rValue = ucWeightScaleSetting.GetSingleton().mPmt.mDic_CtrlWeightScale[sIndex];
            }
            return rValue;
        }

        public void SetDevice_DispValve_AllSimulate(bool p_bSimulate)
        {
            try
            {
                foreach (var CtrlValve in ucDispValveSetting.GetSingleton().mPmt.mDic_CtrlDispValve.Values)
                {
                    CtrlValve.g_ValveModbus.bIsSimulatorMode = p_bSimulate;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        public clsCtrlDispValve GetDevice_DispValve(int iIndex)
        {
            clsCtrlDispValve rValue = null;
            if (iIndex >= 0 && iIndex < ucDispValveSetting.GetSingleton().mPmt.mDic_CtrlDispValve.Count)
            {
                rValue = ucDispValveSetting.GetSingleton().mPmt.mDic_CtrlDispValve.ElementAt(iIndex).Value;
            }
            return rValue;
        }
        public clsCtrlDispValve GetDevice_DispValve(string sIndex)
        {
            clsCtrlDispValve rValue = null;
            if (ucDispValveSetting.GetSingleton().mPmt.mDic_CtrlDispValve.ContainsKey(sIndex) == true)
            {
                rValue = ucDispValveSetting.GetSingleton().mPmt.mDic_CtrlDispValve[sIndex];
            }
            return rValue;
        }
        #endregion

        #region //===================== private 函式設置 (SetBtnColor, Covert-Data&UI) =====================

        #endregion

        #region//===================== 以下為事件處理 (VisibleChanged, tabPageChanged, dgvEnableChanged) =====================

        private void ucCardSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                UpdateControls();
            }
            ucRollerMotor.GetSingleton().Visible = !this.Visible;
            ucRollerMotor.GetSingleton().Visible = this.Visible;
            ucHighSensor.GetSingleton().Visible = !this.Visible;
            ucHighSensor.GetSingleton().Visible = this.Visible;
            ucReader.GetSingleton().Visible = !this.Visible;
            ucReader.GetSingleton().Visible = this.Visible;
            ucTCPLink.GetSingleton().Visible = !this.Visible;
            ucTCPLink.GetSingleton().Visible = this.Visible;
            ucHeaterModule.GetSingleton().Visible = !this.Visible;
            ucHeaterModule.GetSingleton().Visible = this.Visible;
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        #endregion

    }
}

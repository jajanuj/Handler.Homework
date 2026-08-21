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
using ArtSystem.MultiSystem;

namespace ArtSystem
{
    public partial class ucSignalTowerIOSetting : ucBaseUserControl2
    {
        #region //===================== 變數設置 =====================
        /// <summary> 存檔參數 </summary>
        public clsSignalTowerSetting g_SignalTower = new clsSignalTowerSetting();
        private bool m_bEditing_SignalTower = false;
        private Dictionary<clsEnum.enuDi, string> m_DicDIName = null;
        private Dictionary<clsEnum.enuDo, string> m_DicDOName = null;
        #endregion

        #region //===================== 必要函式設置 =====================

        static private object objLock = new object();
        static private ucSignalTowerIOSetting m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucSignalTowerIOSetting GetSingleton()
        {
            lock (objLock)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucSignalTowerIOSetting();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucSignalTowerIOSetting()
        {
            InitializeComponent();
            if (ArtSystem.clsArtSystem.bIsProgramOpen == false)
            {
                return;
            }
            this.VisibleChanged += new EventHandler(UserControl_VisibleChanged);
            Load_SignalTower();
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                this.Dock = DockStyle.Fill;
                btnSave_SignalTower.Enabled = btnCancel_SignalTower.Enabled = panel_SignalTower.Enabled = m_bEditing_SignalTower;

                txt_Bizzer1.Text = g_SignalTower.GetDoName(clsSignalTowerSetting.enuSignalTower.Bizzer1);
                txt_Bizzer2.Text = g_SignalTower.GetDoName(clsSignalTowerSetting.enuSignalTower.Bizzer2);
                txt_LEDBlue.Text = g_SignalTower.GetDoName(clsSignalTowerSetting.enuSignalTower.LED_Blue);
                txt_LEDGreen.Text = g_SignalTower.GetDoName(clsSignalTowerSetting.enuSignalTower.LED_Green);
                txt_LEDYellow.Text = g_SignalTower.GetDoName(clsSignalTowerSetting.enuSignalTower.LED_Yellow);
                txt_LEDRed.Text = g_SignalTower.GetDoName(clsSignalTowerSetting.enuSignalTower.LED_Red);
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
                btnSave_SignalTower.BackColor = m_bEditing_SignalTower == true ? (DateTime.Now.Millisecond > 500 ? Color.Lime : SystemColors.Control) : SystemColors.Control;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 進入此介面時,自動執行UpdateControls </summary>
        protected void UserControl_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_bEditing_SignalTower == true)
                { Load_SignalTower(); }
                m_bEditing_SignalTower = false;
                if (this.Visible == true)
                {
                    if (m_DicDIName != null)
                    { m_DicDIName.Clear(); }
                    m_DicDIName = ArtSystem.MultiSystem.clsDioMotion.GetDiName();

                    if (m_DicDOName != null)
                    { m_DicDOName.Clear(); }
                    m_DicDOName = ArtSystem.MultiSystem.clsDioMotion.GetDoName();
                    this.UpdateControls();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region//===================== Public 函式 =====================
        public void Load_SignalTower()
        {
            try
            {
                string sFileName = clsMultiSystem.strSystemINIPath;
                sFileName += "\\SignalTowerIO.ini";
                var Temp = ArtSystem.Files.JsonHelper.JsonDeserializeFromFile<clsSignalTowerSetting>(sFileName, Encoding.Unicode);
                if (Temp != null)
                {
                    clsClassFunc.Copy(Temp, g_SignalTower);
                }
                else
                {
                    g_SignalTower.g_DicSignalTower_DOID.Clear();
                    foreach (clsSignalTowerSetting.enuSignalTower eSingalTower in Enum.GetValues(typeof(clsSignalTowerSetting.enuSignalTower)))
                    {
                        g_SignalTower.g_DicSignalTower_DOID.Add(eSingalTower, null);
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void Save_SignalTower()
        {
            try
            {
                string sFileName = clsMultiSystem.strSystemINIPath;
                sFileName += "\\SignalTowerIO.ini";
                ArtSystem.Files.JsonHelper.JsonSerializeToFile(g_SignalTower, sFileName, Encoding.Unicode);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        public void SetSignalTowerControl(comSignalTower p_comSignalTower)
        {
            try
            {
                if (p_comSignalTower != null && g_SignalTower != null)
                {
                    Load_SignalTower();
                    p_comSignalTower._BuzzerDo1 = g_SignalTower.g_DicSignalTower_DOID[clsSignalTowerSetting.enuSignalTower.Bizzer1];
                    p_comSignalTower._BuzzerDo2 = g_SignalTower.g_DicSignalTower_DOID[clsSignalTowerSetting.enuSignalTower.Bizzer2];
                    p_comSignalTower._LightBlueDo = g_SignalTower.g_DicSignalTower_DOID[clsSignalTowerSetting.enuSignalTower.LED_Blue];
                    p_comSignalTower._LightGreenDo = g_SignalTower.g_DicSignalTower_DOID[clsSignalTowerSetting.enuSignalTower.LED_Green];
                    p_comSignalTower._LightYellowDo = g_SignalTower.g_DicSignalTower_DOID[clsSignalTowerSetting.enuSignalTower.LED_Yellow];
                    p_comSignalTower._LightRedDo = g_SignalTower.g_DicSignalTower_DOID[clsSignalTowerSetting.enuSignalTower.LED_Red];
                    p_comSignalTower._ShowLightBlue = g_SignalTower.g_DicSignalTower_DOID[clsSignalTowerSetting.enuSignalTower.LED_Blue] != null;
                    p_comSignalTower._ShowLightGreen = g_SignalTower.g_DicSignalTower_DOID[clsSignalTowerSetting.enuSignalTower.LED_Green] != null;
                    p_comSignalTower._ShowLightYellow = g_SignalTower.g_DicSignalTower_DOID[clsSignalTowerSetting.enuSignalTower.LED_Yellow] != null;
                    p_comSignalTower._ShowLightRed = g_SignalTower.g_DicSignalTower_DOID[clsSignalTowerSetting.enuSignalTower.LED_Red] != null;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region//===================== Private 函式 =====================
        
        #endregion

        #region//===================== 事件處理  =====================

        private void btnSave_SignalTower_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_bEditing_SignalTower = false;
                Save_SignalTower();
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btnCancel_SignalTower_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_bEditing_SignalTower = false;
                Load_SignalTower();
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btnEdit_SignalTower_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_bEditing_SignalTower = !this.m_bEditing_SignalTower;
                Load_SignalTower();
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void txt_LEDRed_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (g_SignalTower != null)
                {
                    clsSignalTowerSetting.enuSignalTower eDOName = clsSignalTowerSetting.enuSignalTower.LED_Green;
                    if (sender == txt_LEDBlue)
                    {
                        eDOName = clsSignalTowerSetting.enuSignalTower.LED_Blue;
                    }
                    else if (sender == txt_LEDRed)
                    {
                        eDOName = clsSignalTowerSetting.enuSignalTower.LED_Red;
                    }
                    else if (sender == txt_LEDGreen)
                    {
                        eDOName = clsSignalTowerSetting.enuSignalTower.LED_Green;
                    }
                    else if (sender == txt_LEDYellow)
                    {
                        eDOName = clsSignalTowerSetting.enuSignalTower.LED_Yellow;
                    }
                    else if (sender == txt_Bizzer1)
                    {
                        eDOName = clsSignalTowerSetting.enuSignalTower.Bizzer1;
                    }
                    else if (sender == txt_Bizzer2)
                    {
                        eDOName = clsSignalTowerSetting.enuSignalTower.Bizzer2;
                    }
                    else
                    {
                        return;
                    }

                    clsEnum.enuDo? eDOID = g_SignalTower.g_DicSignalTower_DOID[eDOName];
                    string sOrgValue = "";
                    if (eDOID != null)
                    { sOrgValue = eDOID.ToString(); }
                    if (ucGetEnumDO.GetSingleton()._ShowFormDialog(ref eDOID) == DialogResult.OK)
                    {
                        string sNewValue = "";
                        if (eDOID != null)
                        { sNewValue = eDOID.ToString();　}
                        g_SignalTower.g_DicSignalTower_DOID[eDOName] = eDOID;
                        UpdateControls();
                        clsLog.Log(clsCmData.enuLogType.ButtonLog, "[" + this.Name + "]" + clsCmData.g_strNowUser + " - Press " + ((Control)sender).Name
                                   + ", g_SignalTower.g_DicSignalTower_DOID[" + eDOName.ToString() + "], DO_ID  = "
                                   + sOrgValue + " -> " + sNewValue);
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion
    }
}

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
using ArtSystem;

namespace ArtSystem
{
    public partial class ucMachineButton : ucBaseUserControl2
    {
        #region //===================== 變數設置 =====================
        private clsMachineIOSetting m_MachineDetectorSetting = null;

        

        #endregion

        #region //===================== 必要函式設置 =====================

        //static private object objLock = new object();
        //static private _ucTapControl m_Singleton;
        ///// <summary> 取得唯一物件，避免重覆設置  </summary>
        //static public _ucTapControl GetSingleton()
        //{
        //    lock (objLock)
        //    {
        //        if (m_Singleton == null)
        //        {
        //            m_Singleton = new _ucTapControl();
        //        }
        //    }
        //    return m_Singleton;
        //}

        /// <summary> 建構式 </summary>
        public ucMachineButton()
        {
            InitializeComponent();
            if (ArtSystem.clsArtSystem.bIsProgramOpen == false)
            {
                return;
            }
            this.VisibleChanged += new EventHandler(UserControl_VisibleChanged);
        }
        public override void UpdateControls()
        {
            try
            {
                if (m_MachineDetectorSetting != null)
                {
                    var DicDIName = ArtSystem.MultiSystem.clsDioMotion.GetDiName();
                    var DicDOName = ArtSystem.MultiSystem.clsDioMotion.GetDoName();
                    switch (m_MachineDetectorSetting.g_eCtrlType)
                    {
                        case clsMachineIOSetting.enuCtrlType.Button_DI:
                            {
                                switch (m_MachineDetectorSetting.g_eHotKeyButton)
                                {
                                    case clsMachineIOSetting.enuHotKeyButton.Run:
                                        label1.BackColor = Color.Lime;
                                        label1.ForeColor = Color.Black;
                                        label1.Text = clsLanguage.GetTranslation("RUN", false);
                                        break;
                                    case clsMachineIOSetting.enuHotKeyButton.Stop:
                                        label1.BackColor = Color.Red;
                                        label1.ForeColor = Color.White;
                                        label1.Text = clsLanguage.GetTranslation("STOP", false);
                                        break;
                                    case clsMachineIOSetting.enuHotKeyButton.Reset:
                                        label1.BackColor = Color.Blue;
                                        label1.ForeColor = Color.White;
                                        label1.Text = clsLanguage.GetTranslation("RESET", false);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        case clsMachineIOSetting.enuCtrlType.Sensor_DI:
                            {
                                label1.BackColor = Color.White;
                                if (m_MachineDetectorSetting.g_eDI_ID != null)
                                {
                                    string sText = m_MachineDetectorSetting.g_eDI_ID.ToString();
                                    if (DicDIName.ContainsKey((clsEnum.enuDi)m_MachineDetectorSetting.g_eDI_ID) == true)
                                    {
                                        sText += " - " + DicDIName[(clsEnum.enuDi)m_MachineDetectorSetting.g_eDI_ID];
                                    }
                                    label1.Text = sText;
                                }
                                else
                                {
                                    label1.Text = "Null";
                                }
                            }
                            break;
                        case clsMachineIOSetting.enuCtrlType.Sensor_AI:
                            {
                                label1.BackColor = Color.White;
                                if (m_MachineDetectorSetting.g_eDI_ID != null)
                                {
                                    string sText = m_MachineDetectorSetting.g_eDI_ID.ToString().Replace("DI","AI");
                                    if (DicDIName.ContainsKey((clsEnum.enuDi)m_MachineDetectorSetting.g_eDI_ID) == true)
                                    {
                                        sText += " - " + DicDIName[(clsEnum.enuDi)m_MachineDetectorSetting.g_eDI_ID];
                                    }
                                    label1.Text = sText;
                                }
                                else
                                {
                                    label1.Text = "Null";
                                }
                            }
                            break;
                        case clsMachineIOSetting.enuCtrlType.SetOutput_DO:
                            {
                                label1.BackColor = Color.White;
                                if (m_MachineDetectorSetting.g_eDO_ID != null)
                                {
                                    string sText = m_MachineDetectorSetting.g_eDO_ID.ToString();
                                    if (DicDOName.ContainsKey((clsEnum.enuDo)m_MachineDetectorSetting.g_eDO_ID) == true)
                                    {
                                        sText += " - " + DicDOName[(clsEnum.enuDo)m_MachineDetectorSetting.g_eDO_ID];
                                    }
                                    label1.Text = sText;
                                }
                                else
                                {
                                    label1.Text = "Null";
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls(clsMachineIOSetting p_MachineDetectorSetting)
        {
            try
            {
                m_MachineDetectorSetting = p_MachineDetectorSetting;
                UpdateControls();
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

        /// <summary> 進入此介面時,自動執行UpdateControls </summary>
        protected void UserControl_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Visible == true)
                {
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



        #endregion

        #region//===================== Private 函式 =====================
        #endregion

        #region//===================== 事件處理 =====================

        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                this.OnClick(e);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            try
            {
                this.OnClick(e);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

    }
}

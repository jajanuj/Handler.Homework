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

namespace ArtSystem.Login
{
    public partial class ucAutoLogout : ucBaseUserControl
    {
        #region //===================== 區域變數設置 =====================

        /// <summary> 自動登出(Enable/Disable) </summary>
        private bool bAutoLogout = false;
        /// <summary> 自動登出等級 (預設Level-1) </summary>
        private int iAutoLogout_Level = 1;
        /// <summary> 自動登出時間 (預設10分鐘) </summary>
        private double dAutoLogout_Timeout_Minute = 10;
        /// <summary> 自動登出-目前等級 </summary>
        private int iAutoLogout_CurrentLevel = 0;
        /// <summary> 自動登出時間 </summary>
        public clsHiPerfTimer mAutoLogout_Timer = new clsHiPerfTimer();

        private bool? bNeedAutoLogout = false;
        public bool bAutoLogoutBlock = false;
        #endregion

        #region //===================== 必要函式設置 =====================

        static private ucAutoLogout m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucAutoLogout GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucAutoLogout();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucAutoLogout()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            {  return; }
            LoadAutoLogoutPmt();
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                LoadAutoLogoutPmt();

                #region//AutoLogout Button Color Set
                if (bAutoLogout == true)
                {
                    btnAutoLogout_Enable.BackColor = Color.Lime;
                    btnAutoLogout_Disable.BackColor = this.BackColor;
                }
                else
                {
                    btnAutoLogout_Enable.BackColor = this.BackColor;
                    btnAutoLogout_Disable.BackColor = Color.Lime;
                }
                if (cNum_AutoLogout_Level.Focused == false)
                {
                    cNum_AutoLogout_Level._Value = this.iAutoLogout_Level;
                }
                if (cNum_AutoLogout_Timeout_Minute.Focused == false)
                {
                    cNum_AutoLogout_Timeout_Minute._Value = (decimal)this.dAutoLogout_Timeout_Minute;
                }
                #endregion

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

        /// <summary> 自動登出條件滿足 (Null表示無法正常自動登出) </summary>
        public bool? _NeedAutoLogout()
        {
            bool? rValue = false;
            if (bAutoLogout == true)
            {
                if (iAutoLogout_CurrentLevel != clsCmData.g_iNowUserLevel)
                {
                    iAutoLogout_CurrentLevel = clsCmData.g_iNowUserLevel;
                    Login.fromAutoLogout.GetSingleton().Hide();
                    this.mAutoLogout_Timer.Restart();
                }
                if (this.dAutoLogout_Timeout_Minute == 0)
                {
                    Login.fromAutoLogout.GetSingleton().Hide();
                    this.mAutoLogout_Timer.Stop();
                }
                else if (this.dAutoLogout_Timeout_Minute != 0
                    && clsCmData.g_strNowUser != null
                    && clsCmData.g_strNowUser != ""
                    && clsCmData.g_iNowUserLevel <= this.iAutoLogout_Level)
                {
                    bool bIsFomrMessageBoxOpen = false;
                    for (int i = 0; i < Application.OpenForms.Count; i++)
                    {
                        if (Application.OpenForms[i] is formMessageBox)
                        {
                            bIsFomrMessageBoxOpen = true;
                            break;
                        }
                    }
                    int MinRemainTime = 10;
                    if ((this.dAutoLogout_Timeout_Minute * 60) < MinRemainTime)
                    {
                        MinRemainTime = (int)(this.dAutoLogout_Timeout_Minute * 60);
                        MinRemainTime /= 2;
                    }
                    if (this.mAutoLogout_Timer.IsTimeOut((this.dAutoLogout_Timeout_Minute * 60) - MinRemainTime, clsCmData.enuSecUnit.Sec) == true)
                    {
                        if (bIsFomrMessageBoxOpen == false)
                        {
                            Login.fromAutoLogout.GetSingleton()._Show(MinRemainTime);
                        }
                    }
                    if (this.mAutoLogout_Timer.IsTimeOut(this.dAutoLogout_Timeout_Minute * 60, clsCmData.enuSecUnit.Sec) == true)
                    {
                        if (bIsFomrMessageBoxOpen == false)
                        {
                            Login.fromAutoLogout.GetSingleton().Hide();
                            this.mAutoLogout_Timer.Restart();
                            rValue = true;
                        }
                        else
                        {
                            rValue = null;
                        }
                    }
                }
            }
            else
            {
                Login.fromAutoLogout.GetSingleton().Hide();
                this.mAutoLogout_Timer.Stop();
            }
            if (bNeedAutoLogout == true
                && rValue == true)
            {
                bAutoLogoutBlock = true;
            }
            else
            {
                bAutoLogoutBlock = false;
            }
            bNeedAutoLogout = rValue;
            return rValue;
        }

        /// <summary> 自動登出參數 : Enable </summary>
        public bool _GetAutoLogout_Enable()
        {
            return this.bAutoLogout;
        }
        /// <summary> 自動登出參數 : 登出等級 (預設Level-1) </summary>
        public int _GetAutoLogout_Level()
        {
            return this.iAutoLogout_Level;
        }
        /// <summary> 自動登出參數 : 登出時間 (預設10分鐘) </summary>
        public double _GetAutoLogout_Timeout_Minute()
        {
            return this.dAutoLogout_Timeout_Minute;
        }

        /// <summary> 取得自動登出剩餘時間(分鐘) </summary>
        public double _GetRemainTime_Minute()
        {
            double rValue = 0;
            rValue = Login.ucAutoLogout.GetSingleton()._GetAutoLogout_Timeout_Minute() * 60 * 1000;
            rValue -= Login.ucAutoLogout.GetSingleton().mAutoLogout_Timer.ElapsedMilliseconds;
            rValue /= (60 * 1000);
            return rValue;
        }
        #endregion

        #region //===================== private 函式設置 =====================

        /// <summary> 載入自動登出的參數 </summary>
        private void LoadAutoLogoutPmt()
        {
            try
            {
                clsIniFile iniSystem = new clsIniFile(clsCmData.g_strSystemIniFilePath);//artSystem.ini
                bAutoLogout = iniSystem.GetString("AutoLogout", "Enable", "0") == "1";
                iAutoLogout_Level = Convert.ToInt32(iniSystem.GetString("AutoLogout", "iAutoLogout_Level", "1"));
                dAutoLogout_Timeout_Minute = Convert.ToDouble(iniSystem.GetString("AutoLogout", "dAutoLogout_Timeout_Minute", "10"));
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }

        }

        #endregion

        #region //===================== 以下為事件處理 =====================

        private void AutoLogoutChange(object sender, EventArgs e)
        {
            clsIniFile iniSystem = new clsIniFile(clsCmData.g_strSystemIniFilePath);//artSystem.ini
            if (sender == btnAutoLogout_Enable)
            {
                clsLog.Log(clsCmData.enuLogType.SystemLog, "[Auto Logout Pmt Changed] -> Enable : " + (this.bAutoLogout ? "1" : "0") + " ->" + "1");
                clsLog.Log(clsCmData.enuLogType.ButtonLog, "[Auto Logout Pmt Changed] -> Enable : " + (this.bAutoLogout ? "1" : "0") + " ->" + "1");
                iniSystem.WriteValue("AutoLogout", "Enable", 1);
            }
            else
            {
                clsLog.Log(clsCmData.enuLogType.SystemLog, "[Auto Logout Pmt Changed] -> Enable : " + (this.bAutoLogout ? "1" : "0") + " ->" + "0");
                clsLog.Log(clsCmData.enuLogType.ButtonLog, "[Auto Logout Pmt Changed] -> Enable : " + (this.bAutoLogout ? "1" : "0") + " ->" + "0");
                iniSystem.WriteValue("AutoLogout", "Enable", 0);
            }
            this.UpdateControls();
            this.mAutoLogout_Timer.Restart();
        }

        private void cNum_AutoLogout_Level_TextChanged(object sender, EventArgs e)
        {
            if (cNum_AutoLogout_Level.Focused == true)
            {
                clsIniFile iniSystem = new clsIniFile(clsCmData.g_strSystemIniFilePath);//artSystem.ini
                if (this.iAutoLogout_Level != cNum_AutoLogout_Level._Value)
                {
                    clsLog.Log(clsCmData.enuLogType.SystemLog, "[Auto Logout Pmt Changed] -> iAutoLogout_Level : " + this.iAutoLogout_Level + " ->" + cNum_AutoLogout_Level._Value);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, "[Auto Logout Pmt Changed] -> iAutoLogout_Level : " + this.iAutoLogout_Level + " ->" + cNum_AutoLogout_Level._Value);
                    iniSystem.WriteValue("AutoLogout", "iAutoLogout_Level",  Convert.ToDouble(cNum_AutoLogout_Level._Value));
                }
            }
            if (cNum_AutoLogout_Timeout_Minute.Focused == true)
            {
                clsIniFile iniSystem = new clsIniFile(clsCmData.g_strSystemIniFilePath);//artSystem.ini
                if ((decimal)this.dAutoLogout_Timeout_Minute != cNum_AutoLogout_Timeout_Minute._Value)
                {
                    this.mAutoLogout_Timer.Restart();
                    clsLog.Log(clsCmData.enuLogType.SystemLog, "[Auto Logout Pmt Changed] -> dAutoLogout_Timeout_Minute : " + this.dAutoLogout_Timeout_Minute + " ->" + cNum_AutoLogout_Timeout_Minute._Value);
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, "[Auto Logout Pmt Changed] -> dAutoLogout_Timeout_Minute : " + this.dAutoLogout_Timeout_Minute + " ->" + cNum_AutoLogout_Timeout_Minute._Value);
                    iniSystem.WriteValue("AutoLogout", "dAutoLogout_Timeout_Minute", Convert.ToDouble(cNum_AutoLogout_Timeout_Minute._Value));
                }
            }
            this.UpdateControls();
        }

        #endregion



    }
}

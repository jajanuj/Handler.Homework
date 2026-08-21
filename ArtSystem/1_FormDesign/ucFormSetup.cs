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

namespace ArtSystem.FormDesign
{
    public partial class ucFormSetup : ucBaseUserControl
    {
        #region //===================== 區域變數設置 =====================

        Dictionary<string, Button> mLst_LanguageButton = new Dictionary<string, Button>();
        private bool bIsNeesFormDesign = true;

        #endregion

        #region //===================== 必要函式設置 =====================

        static private ucFormSetup m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucFormSetup GetSingleton(bool? NeedSetFormDesign = null)
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucFormSetup();
                if (NeedSetFormDesign == true)
                {
                    m_Singleton.bIsNeesFormDesign = true;
                }
                else
                {
                    m_Singleton.bIsNeesFormDesign = false;
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucFormSetup()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            {  return; }

            mLst_LanguageButton.Add("EN", btnEnglish);
            mLst_LanguageButton.Add("TC", btnChinese);
            UpdateLanguageButton();
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                clsIniFile iniSystem = new clsIniFile(clsCmData.g_strSystemIniFilePath);//artSystem.ini
                bool bFullScrean = iniSystem.GetString("System", "Form Style", "btnWindowsMode") == "btnFullScrean";
                bool bTopMost = iniSystem.GetString("System", "Is Topmost", "btnTopFalse") == "btnTopTrue";
                ucArtMain_Design.enuDesign eDesign = iniSystem.GetString("System", "Form Design", "btnPCDesign") == "btnPanelDesign" ? ucArtMain_Design.enuDesign.Panel : ucArtMain_Design.enuDesign.PC;
                string sLanguage = _GetCurrentLanguage();

                #region//Language Button Color Set
                foreach(string sKey in mLst_LanguageButton.Keys)
                {
                    if (sKey == sLanguage)
                    {
                        mLst_LanguageButton[sKey].BackColor = Color.Lime;
                    }
                    else
                    {
                        mLst_LanguageButton[sKey].BackColor = this.BackColor;
                    }
                }
                #endregion
                #region//Top Most Button Color Set
                if (bTopMost == true)
                {
                    btnTopTrue.BackColor = Color.Lime;
                    btnTopFalse.BackColor = this.BackColor;
                }
                else
                {
                    btnTopTrue.BackColor = this.BackColor;
                    btnTopFalse.BackColor = Color.Lime;
                }
                #endregion
                #region//Full Screan Button Color Set
                if (bFullScrean == true)
                {
                    btnWindowsMode.BackColor = this.BackColor;
                    btnFullScrean.BackColor = Color.Lime;
                }
                else
                {
                    btnWindowsMode.BackColor = Color.Lime;
                    btnFullScrean.BackColor = this.BackColor;
                }
                #endregion
                #region//Form Design Button Color Set
                groupBoxFormDesign.Visible = bIsNeesFormDesign;
                if (eDesign == ucArtMain_Design.enuDesign.PC)
                {
                    btnPCDesign.BackColor = Color.Lime;
                    btnPanelDesign.BackColor = this.BackColor;
                }
                else
                {
                    btnPCDesign.BackColor = this.BackColor;
                    btnPanelDesign.BackColor = Color.Lime;
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

        /// <summary> 取得目前設定之語言 </summary>
        public string _GetCurrentLanguage()
        {
            string rValue = "EN";
            clsIniFile iniSystem = new clsIniFile(clsCmData.g_strSystemIniFilePath);
            string sINI_Language = iniSystem.GetString("System", "Language", "btnChinese");
            if (sINI_Language == btnEnglish.Name)
            {
                rValue = "EN";
            }
            else if (sINI_Language == btnChinese.Name)
            {
                rValue = "TC";
            }
            else if(sINI_Language.Contains("btnOther_") == true)
            {
                rValue = sINI_Language.Split('_')[1];
            }
            return rValue;
        }

        #endregion

        #region //===================== private 函式設置 =====================

        /// <summary> 載入INI中其他語言的Button到介面上 </summary>
        private void UpdateLanguageButton()
        {
            string strINIPath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\";
            string[] FileLists = System.IO.Directory.GetFiles(strINIPath);
            foreach (string FileName in FileLists)
            {
                if (FileName.Contains("Language_") == true
                    && FileName.Contains(".ini") == true)
                {
                    string LanguageName = System.IO.Path.GetFileName(FileName).Split('_')[1].Replace(".ini", "").Split('-')[0];
                    if(mLst_LanguageButton.ContainsKey(LanguageName) == false
                        && LanguageName.Length > 0)
                    {
                        AddLanguageButton(LanguageName, new Button());
                    }
                }
            }

        }

        /// <summary> 加入其他語言的Button </summary>
        private void AddLanguageButton(string str_LanguageName , Button p_Button)
        {
            if (mLst_LanguageButton.ContainsKey(str_LanguageName) == false
                && str_LanguageName.Length > 0)
            {
                int PitchX = btnChinese.Left - btnEnglish.Left;
                mLst_LanguageButton.Add(str_LanguageName, p_Button);
                p_Button.Parent = groupBoxLanguage;
                p_Button.Size = btnEnglish.Size;
                p_Button.Top = btnEnglish.Top;
                p_Button.Left = btnEnglish.Left + (PitchX * (mLst_LanguageButton.Count - 1));
                p_Button.Click += new EventHandler(LanguageChange);
                p_Button.Name = "btnOther_" + str_LanguageName;
                p_Button.Text =  clsLanguage.GetTranslation(str_LanguageName);
            }
        }

        #endregion

        #region //===================== 以下為事件處理 =====================

        private void ucFormSetup_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                this.UpdateControls();
            }
        }

        private void FormStyleChange(object sender, EventArgs e)
        {
            clsIniFile iniSystem = new clsIniFile(clsCmData.g_strSystemIniFilePath);
            iniSystem.WriteValue("System", "Form Style", ((Button)sender).Name);//artSystem.ini
            ucArtMain_Design.GetSingleton()._UpdateControls();
            this.UpdateControls();
        }

        private void LanguageChange(object sender, EventArgs e)
        {
            clsIniFile iniSystem = new clsIniFile(clsCmData.g_strSystemIniFilePath);
            iniSystem.WriteValue("System", "Language", ((Button)sender).Name);
            ucArtMain_Design.GetSingleton()._ChangeLanguage(_GetCurrentLanguage());
            this.UpdateControls();
        }

        private void IsTopmost(object sender, EventArgs e)
        {
            clsIniFile iniSystem = new clsIniFile(clsCmData.g_strSystemIniFilePath);
            iniSystem.WriteValue("System", "Is Topmost", ((Button)sender).Name);//artSystem.ini
            bool bTopMost = iniSystem.GetString("System", "Is Topmost", "btnTopFalse") == "btnTopTrue";//artSystem.ini
            ucArtMain_Design.GetSingleton()._UpdateControls();
            this.UpdateControls();
        }

        private void FormDesignChange(object sender, EventArgs e)
        {
            clsIniFile iniSystem = new clsIniFile(clsCmData.g_strSystemIniFilePath);
            iniSystem.WriteValue("System", "Form Design", ((Button)sender).Name);//artSystem.ini
            ucArtMain_Design.GetSingleton()._UpdateControls();
            this.UpdateControls();
        }


        #endregion
    }
}

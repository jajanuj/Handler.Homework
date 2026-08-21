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

namespace ArtSystem
{
    public partial class ucWarnningMessage : ucBaseUserControl
    {
        #region //===================== 區域變數設置 =====================

        /// <summary> 紀錄Warnning訊息 </summary>
        public Dictionary<string, clsWarningInfo> mDic_WarnningMessage = new Dictionary<string, clsWarningInfo>();
        private List<string> mLst_Warning = new List<string>();
        private string strTopWarning = "None";
        private int iWarningTopIndex = 0;
        private Size mSizeOrg = new Size();
        #endregion 

        #region //===================== class資料結構 =====================

        public class clsWarningInfo
        {
            public string sMessage = "";
            public DateTime mTime_Occour = new DateTime();
            public bool bIsWarning = false;
            public bool bIsAutoShowForm = false;
        }

        #endregion

        #region //===================== 必要函式設置 =====================

        static private ucWarnningMessage m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucWarnningMessage GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucWarnningMessage();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucWarnningMessage()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            {  return; }
            this.mSizeOrg = this.Size;
            clsLanguage.SetLanguateToControls(this);
            ucArtMain_Design.GetSingleton()._CollectChangeLanguageControls(this);
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                if (this.InvokeRequired == true)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        UpdateControls();
                    }));
                }
                else
                {
                    dataGridViewCurrentAlarm.Rows.Clear();
                    foreach (string sKey in mDic_WarnningMessage.Keys)
                    {
                        if (mDic_WarnningMessage[sKey].bIsWarning == true)
                        {
                            dataGridViewCurrentAlarm.Rows.Add();
                            int iRowIndex = dataGridViewCurrentAlarm.Rows.Count - 1;
                            dataGridViewCurrentAlarm[dgv_WarningMessage.Index, iRowIndex].Value = clsLanguage.GetTranslation(sKey);
                            dataGridViewCurrentAlarm[dgv_ReportTime.Index, iRowIndex].Value = mDic_WarnningMessage[sKey].mTime_Occour.ToString("yyyy/MM/dd\t HH:mm:ss.fff");
                        }
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

        public void _ShowForm()
        {
            UpdateControls();
            Form mForm = new Form();
            this.Parent = mForm;
            this.Location = new Point(0, 0);
            this.Dock = DockStyle.Fill;
            mForm.Size = new Size(this.mSizeOrg.Width + 16, this.mSizeOrg.Height + 39);
            mForm.StartPosition = FormStartPosition.CenterScreen;
            mForm.Text = clsLanguage.GetTranslation("Warning Message");
            mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
            mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
            mForm.Show();
        }
        public void _SetWarning(string sWarning, bool ActiveWarning, bool AutoShowForm = false)
        {
            if (mDic_WarnningMessage.ContainsKey(sWarning) == false)
            {
                mDic_WarnningMessage.Add(sWarning, new clsWarningInfo());
                mDic_WarnningMessage[sWarning].sMessage = sWarning;
            }
            mDic_WarnningMessage[sWarning].bIsWarning = ActiveWarning;
            mDic_WarnningMessage[sWarning].bIsAutoShowForm = AutoShowForm;
            if (ActiveWarning == true)
            {
                if(mLst_Warning.Contains(sWarning) == false)
                {
                    mLst_Warning.Add(sWarning);
                    mDic_WarnningMessage[sWarning].mTime_Occour = DateTime.Now;
                    if (strTopWarning == "None")
                    {
                        strTopWarning = sWarning;
                        iWarningTopIndex = mDic_WarnningMessage.Keys.ToList<string>().IndexOf(strTopWarning);
                    }
                    else
                    {
                        if (mDic_WarnningMessage.Keys.Contains(sWarning) == true)
                        {
                            int iWarningIndex = mDic_WarnningMessage.Keys.ToList<string>().IndexOf(sWarning);
                            if (iWarningIndex < iWarningTopIndex)
                            {
                                strTopWarning = sWarning;
                                iWarningTopIndex = iWarningIndex;
                            }
                        }
                    }
                    if (AutoShowForm == true)
                    {
                        _ShowForm();
                    }
                }
                else if (strTopWarning == "None")
                {
                    strTopWarning = sWarning;
                    iWarningTopIndex = mDic_WarnningMessage.Keys.ToList<string>().IndexOf(strTopWarning);
                }
            }
            else
            {
                if (mLst_Warning.Contains(sWarning) == true)
                {
                    mLst_Warning.Remove(sWarning);
                }
            }
            if (mLst_Warning.Count == 0)
            {
                strTopWarning = "None";
            }
        }

        public string _GetWarnningMessage()
        {
            string rValue = "None";
            strTopWarning = "None";
            if (mDic_WarnningMessage.Count > 0)
            {
                var FirstItem = mDic_WarnningMessage.Values.Where(v => v.bIsWarning == true).FirstOrDefault();
                if (FirstItem != null)
                {
                    strTopWarning = FirstItem.sMessage;
                }
            }
            rValue = clsLanguage.GetTranslation(strTopWarning);
            return rValue;
        }
        public string _GetWarnningMessage_List()
        {
            string rValue = "";
            if (mDic_WarnningMessage.Count == 0)
            {
                rValue = "None";
            }
            else
            {
                int i = 1;
                foreach(string sWarning in mDic_WarnningMessage.Keys)
                {
                    if (mDic_WarnningMessage[sWarning].bIsWarning == true)
                    {
                        if (i == 1)
                        {
                            rValue += i + ". " + clsLanguage.GetTranslation(sWarning);
                        }
                        else
                        {
                            rValue += "\r\n" + i + ". " + clsLanguage.GetTranslation(sWarning);
                        }
                        i++;
                    }
                }
            }
            return rValue;
        }
        public int _GetWarningCount()
        {
            int rValue = 0;
            rValue = mLst_Warning.Count;
            return rValue;
        }

        #endregion

        #region //===================== private 函式設置 =====================


        #endregion

        #region //===================== 以下為事件處理 =====================

        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GetSingleton().Parent = null;
        }
        private void mForm_Deactivate(object sender, EventArgs e)
        {
            Form mForm = (Form)sender;
            mForm.Close();
        }
        #endregion


    }
}

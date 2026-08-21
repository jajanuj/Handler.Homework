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
    public partial class ucTCPLink : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================

        private Dictionary<string, ucCtrlTCPLink> mDic_ucCtrlTCPLink = new Dictionary<string, ucCtrlTCPLink>();
        List<TabPage> mLst_TabPage = new List<TabPage>();


        #endregion

        #region //=====================  必要函式設置 =====================

        static object m_LockObj = new object();
        static private ucTCPLink m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucTCPLink GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucTCPLink();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucTCPLink()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }

        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                if (this.Parent != null)
                {
                    this.Size = this.Parent.ClientSize;
                    foreach (string sKey in ucTCPLinkSetting.GetSingleton().mPmt.mDic_CtrlTCPLink.Keys)
                    {
                        if (mDic_ucCtrlTCPLink.ContainsKey(sKey) == false)
                        {
                            mDic_ucCtrlTCPLink.Add(sKey, new ucCtrlTCPLink());
                        }
                    }
                    if (mDic_ucCtrlTCPLink.Count > 0)
                    {
                        int iWidth = mDic_ucCtrlTCPLink.ElementAt(0).Value.Width;
                        int iHeight = mDic_ucCtrlTCPLink.ElementAt(0).Value.Height;
                        int iWidthCount = (this.Width - 20) / (iWidth + 20);
                        int iHeightCount = (this.Height - 20) / (iHeight + 20);
                        if (iWidthCount <= 0)
                        { iWidthCount = 1; }
                        if (iHeightCount <= 0)
                        { iHeightCount = 1; }
                        int iSinglePageCount = iWidthCount * iHeightCount;
                        int iTabPageCount = (int)Math.Ceiling((double)mDic_ucCtrlTCPLink.Count / iSinglePageCount);
                      
                        for (int i = mLst_TabPage.Count; i < iTabPageCount; i++)
                        {
                            mLst_TabPage.Add(new TabPage());
                            mLst_TabPage[i].Name = "TabPage" + (i + 1).ToString();
                            mLst_TabPage[i].Text = "Page " + (i + 1).ToString();
                            mLst_TabPage[i].Parent = tabControl1;
                        }
                        for (int i = 0; i < mLst_TabPage.Count; i++)
                        {
                            if (i < iTabPageCount)
                            {
                                mLst_TabPage[i].Parent = tabControl1;
                            }
                            else
                            {
                                mLst_TabPage[i].Parent = null;
                            }
                        }
                        for (int i = 0; i < mDic_ucCtrlTCPLink.Count; i++)
                        {
                            string sKey = mDic_ucCtrlTCPLink.ElementAt(i).Key;
                            ucCtrlTCPLink pValue = mDic_ucCtrlTCPLink.ElementAt(i).Value;
                            int iPageIndex = i / iSinglePageCount;
                            int iTagIndex = i % iSinglePageCount;
                            int iWidthIndex = iTagIndex % iWidthCount;
                            int iHeightIndex = iTagIndex / iWidthCount;
                            pValue.Parent = mLst_TabPage[iPageIndex];
                            pValue.Left = 10 + iWidthIndex * (pValue.Width + 20);
                            pValue.Top = 10 + iHeightIndex * (pValue.Height + 20);
                            if (ucTCPLinkSetting.GetSingleton().mPmt.mDic_CtrlTCPLink.ContainsKey(sKey) == true)
                            {
                                pValue.pTCPLink = ucTCPLinkSetting.GetSingleton().mPmt.mDic_CtrlTCPLink[sKey];
                            }
                            else
                            {
                                pValue.pTCPLink = null;
                            }
                            if (ucTCPLinkSetting.GetSingleton().mPmt.mDic_mPmtValue.ContainsKey(sKey) == true)
                            {
                                pValue.pPmt = ucTCPLinkSetting.GetSingleton().mPmt.mDic_mPmtValue[sKey];
                            }
                            else
                            {
                                pValue.pPmt = null;
                            }
                            pValue.UpdateControls();
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

        #endregion

        #region //===================== private 函式設置 () =====================

        #endregion

        #region//===================== 以下為事件處理 () =====================

        private void ucHighSensorSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                UpdateControls();
            }
            this.SetReflashTimerStart(this.Visible);
            foreach (ucCtrlTCPLink pUC in mDic_ucCtrlTCPLink.Values)
            {
                pUC.Visible = !this.Visible;
                pUC.Visible = this.Visible;
            }
        }
        #endregion

    }
}

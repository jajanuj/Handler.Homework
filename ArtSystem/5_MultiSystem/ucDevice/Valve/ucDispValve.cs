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
    public partial class ucDispValve : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================

        private Dictionary<string, ucCtrlDispValve> mDic_ucCtrlDispValve = new Dictionary<string, ucCtrlDispValve>();
        List<TabPage> mLst_TabPage = new List<TabPage>();
        private int m_iReflashIndex = 0;

        #endregion

        #region //=====================  必要函式設置 =====================

        static object m_LockObj = new object();
        static private ucDispValve m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucDispValve GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucDispValve();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucDispValve()
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
                this.TimerInterval = 500;
                m_iReflashIndex = 0;
                if (this.Parent != null)
                {
                    this.Size = this.Parent.ClientSize;
                    foreach (string sKey in ucDispValveSetting.GetSingleton().mPmt.mDic_CtrlDispValve.Keys)
                    {
                        if (mDic_ucCtrlDispValve.ContainsKey(sKey) == false)
                        {
                            mDic_ucCtrlDispValve.Add(sKey, new ucCtrlDispValve());
                        }
                    }
                    if (mDic_ucCtrlDispValve.Count > 0)
                    {
                        int iWidth = mDic_ucCtrlDispValve.ElementAt(0).Value.Width;
                        int iHeight = mDic_ucCtrlDispValve.ElementAt(0).Value.Height;
                        int iWidthCount = (this.Width - 20) / (iWidth + 20);
                        int iHeightCount = (this.Height - 20) / (iHeight + 20);
                        if (iWidthCount <= 0)
                        { iWidthCount = 1; }
                        if (iHeightCount <= 0)
                        { iHeightCount = 1; }
                        int iSinglePageCount = iWidthCount * iHeightCount;
                        int iTabPageCount = (int)Math.Ceiling((double)mDic_ucCtrlDispValve.Count / iSinglePageCount);
                      
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
                        for (int i = 0; i < mDic_ucCtrlDispValve.Count; i++)
                        {
                            string sKey = mDic_ucCtrlDispValve.ElementAt(i).Key;
                            ucCtrlDispValve pValue = mDic_ucCtrlDispValve.ElementAt(i).Value;
                            int iPageIndex = i / iSinglePageCount;
                            int iTagIndex = i % iSinglePageCount;
                            int iWidthIndex = iTagIndex % iWidthCount;
                            int iHeightIndex = iTagIndex / iWidthCount;
                            pValue.Parent = mLst_TabPage[iPageIndex];
                            pValue.Left = 10 + iWidthIndex * (pValue.Width + 20);
                            pValue.Top = 10 + iHeightIndex * (pValue.Height + 20);
                            if (ucDispValveSetting.GetSingleton().mPmt.mDic_CtrlDispValve.ContainsKey(sKey) == true)
                            {
                                pValue.g_CtrlDispValve = ucDispValveSetting.GetSingleton().mPmt.mDic_CtrlDispValve[sKey];
                            }
                            else
                            {
                                pValue.g_CtrlDispValve = null;
                            }
                            if (ucDispValveSetting.GetSingleton().mPmt.mDic_mPmtValue.ContainsKey(sKey) == true)
                            {
                                pValue.g_Pmt = ucDispValveSetting.GetSingleton().mPmt.mDic_mPmtValue[sKey];
                            }
                            else
                            {
                                pValue.g_Pmt = null;
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
                if (m_iReflashIndex < mDic_ucCtrlDispValve.Count)
                {
                    mDic_ucCtrlDispValve.ElementAt(m_iReflashIndex).Value.ReflashFunc();
                }
                m_iReflashIndex++;
                if (m_iReflashIndex >= mDic_ucCtrlDispValve.Count)
                {
                    m_iReflashIndex = 0;
                }
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

        private void ucHeaterModuleSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                UpdateControls();
            }
            this.SetReflashTimerStart(this.Visible);
            foreach (ucCtrlDispValve pUC in mDic_ucCtrlDispValve.Values)
            {
                pUC.Visible = !this.Visible;
                pUC.Visible = this.Visible;
            }
        }
        #endregion
    }
}

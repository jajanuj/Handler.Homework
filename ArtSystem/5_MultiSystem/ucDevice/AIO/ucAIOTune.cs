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
    public partial class ucAIOTune : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================

        public clsPmtAIOTune mPmt = new clsPmtAIOTune();
        private Dictionary<clsEnum.enuDi, ucCtrlAIOTune> mDic_ucCtrlAITune = new Dictionary<clsEnum.enuDi, ucCtrlAIOTune>();
        private Dictionary<clsEnum.enuDo, ucCtrlAIOTune> mDic_ucCtrlAOTune = new Dictionary<clsEnum.enuDo, ucCtrlAIOTune>();
        List<TabPage> mLst_TabPage = new List<TabPage>();


        #endregion

        #region //=====================  必要函式設置 =====================

        static object m_LockObj = new object();
        static private ucAIOTune m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucAIOTune GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucAIOTune();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucAIOTune()
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
                    #region//建立對應的ucCtrlAIOTune
                    foreach (clsEnum.enuDi sKey in mPmt.mDic_AIPmtValue.Keys)
                    {
                        if (mDic_ucCtrlAITune.ContainsKey(sKey) == false)
                        {
                            mDic_ucCtrlAITune.Add(sKey, new ucCtrlAIOTune());
                        }
                    }
                    foreach (clsEnum.enuDo sKey in mPmt.mDic_AOPmtValue.Keys)
                    {
                        if (mDic_ucCtrlAOTune.ContainsKey(sKey) == false)
                        {
                            mDic_ucCtrlAOTune.Add(sKey, new ucCtrlAIOTune());
                        }
                    }
                    #endregion

                    int iTabPageCount_AI = 0, iSinglePageCount_AI = 0, iWidthCount_AI = 0;
                    int iTabPageCount_AO = 0, iSinglePageCount_AO = 0, iWidthCount_AO = 0;
                    #region//計算AI, AO需要的頁數
                    if (mPmt.mDic_AIPmtValue.Count > 0)
                    {
                        int iWidth = mDic_ucCtrlAITune.ElementAt(0).Value.Width;
                        int iHeight = mDic_ucCtrlAITune.ElementAt(0).Value.Height;
                        int iWidthCount = (this.Width - 20) / (iWidth + 20);
                        int iHeightCount = (this.Height - 20) / (iHeight + 20);
                        if (iWidthCount <= 0)
                        { iWidthCount = 1; }
                        if (iHeightCount <= 0)
                        { iHeightCount = 1; }
                        iWidthCount_AI = iWidthCount;
                        iSinglePageCount_AI = iWidthCount * iHeightCount;
                        iTabPageCount_AI = (int)Math.Ceiling((double)mPmt.mDic_AIPmtValue.Count / iSinglePageCount_AI);
                    }

                    if (mPmt.mDic_AOPmtValue.Count > 0)
                    {
                        int iWidth = mDic_ucCtrlAOTune.ElementAt(0).Value.Width;
                        int iHeight = mDic_ucCtrlAOTune.ElementAt(0).Value.Height;
                        int iWidthCount = (this.Width - 20) / (iWidth + 20);
                        int iHeightCount = (this.Height - 20) / (iHeight + 20);
                        if (iWidthCount <= 0)
                        { iWidthCount = 1; }
                        if (iHeightCount <= 0)
                        { iHeightCount = 1; }
                        iWidthCount_AO = iWidthCount;
                        iSinglePageCount_AO = iWidthCount * iHeightCount;
                        iTabPageCount_AO = (int)Math.Ceiling((double)mPmt.mDic_AOPmtValue.Count / iSinglePageCount_AO);
                    }
                    #endregion

                    #region//建立足夠的TabPages
                    int iTabPageCount = iTabPageCount_AI + iTabPageCount_AO;
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
                    #endregion

                    for (int i = 0; i < mDic_ucCtrlAITune.Count; i++)
                    {
                        clsEnum.enuDi sKey = mDic_ucCtrlAITune.ElementAt(i).Key;
                        ucCtrlAIOTune pValue = mDic_ucCtrlAITune.ElementAt(i).Value;
                        int iPageIndex = i / iSinglePageCount_AI;
                        int iTagIndex = i % iSinglePageCount_AI;
                        int iWidthIndex = iTagIndex % iWidthCount_AI;
                        int iHeightIndex = iTagIndex / iWidthCount_AI;
                        pValue.Parent = mLst_TabPage[iPageIndex];
                        mLst_TabPage[iPageIndex].Text = clsLanguage.GetTranslation("AI Page") + (iPageIndex + 1);
                        pValue.Left = 10 + iWidthIndex * (pValue.Width + 20);
                        pValue.Top = 10 + iHeightIndex * (pValue.Height + 20);
                        pValue.SetAIO(sKey);
                        pValue.UpdateControls();
                    }
                    for (int i = 0; i < mDic_ucCtrlAOTune.Count; i++)
                    {
                        clsEnum.enuDo sKey = mDic_ucCtrlAOTune.ElementAt(i).Key;
                        ucCtrlAIOTune pValue = mDic_ucCtrlAOTune.ElementAt(i).Value;
                        int iPageIndex = i / iSinglePageCount_AO;
                        int iTagIndex = i % iSinglePageCount_AO;
                        int iWidthIndex = iTagIndex % iWidthCount_AO;
                        int iHeightIndex = iTagIndex / iWidthCount_AO;
                        mLst_TabPage[iTabPageCount_AI + iPageIndex].Text = clsLanguage.GetTranslation("AO Page") + (iPageIndex + 1);
                        pValue.Parent = mLst_TabPage[iTabPageCount_AI + iPageIndex];
                        pValue.Left = 10 + iWidthIndex * (pValue.Width + 20);
                        pValue.Top = 10 + iHeightIndex * (pValue.Height + 20);
                        pValue.SetAIO(sKey);
                        pValue.UpdateControls();
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
                foreach (ucCtrlAIOTune Item in mDic_ucCtrlAITune.Values)
                {
                    if (Item.Parent == tabControl1.SelectedTab)
                    {
                        Item.ReflashFunc();
                    }
                }
                foreach (ucCtrlAIOTune Item in mDic_ucCtrlAOTune.Values)
                {
                    if (Item.Parent == tabControl1.SelectedTab)
                    {
                        Item.ReflashFunc();
                    }
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

        private void ucAIOTune_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                UpdateControls();
            }
            this.SetReflashTimerStart(this.Visible);
        }
        #endregion
    }
}

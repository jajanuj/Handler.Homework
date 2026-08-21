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
    public partial class ucMultiSystem : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================

        #endregion

        #region //=====================  必要函式設置 =====================

        static object m_LockObj = new object();
        static private ucMultiSystem m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucMultiSystem GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucMultiSystem();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucMultiSystem()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            _AddTabPage("PC Card Setting", ucCardSetting.GetSingleton());
            //_AddTabPage("Art AOI Setting", ucArtAOI.GetSingleton());
            _AddTabPage("Device Setting", ucDeviceSetting.GetSingleton());
            //_AddTabPage("Define Axis Name", ucDefineAxisName.GetSingleton());
            this.TimerInterval = 100;
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                //foreach (TabPage pPage in tabControl1.TabPages)
                TabPage pPage = tabControl1.SelectedTab;
                if (pPage != null)
                {
                    if (pPage.Controls.Count > 0)
                    {
                        if (pPage.Controls[0] is ucBaseUserControl)
                        {
                            ucBaseUserControl Item = (ucBaseUserControl)pPage.Controls[0];
                            Item.Visible = !this.Visible;
                            Item.Visible = this.Visible;
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
                foreach (TabPage pPage in tabControl1.TabPages)
                {
                    if (pPage.Controls.Count > 0)
                    {
                        if (pPage.Controls[0] is ucBaseUserControl)
                        {
                            ucBaseUserControl Item = (ucBaseUserControl)pPage.Controls[0];
                            if (tabControl1.SelectedTab == pPage)
                            {
                                Item.SetReflashTimerStart(true);
                            }
                            else
                            {
                                Item.SetReflashTimerStart(false);
                            }
                        }
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

        public void _AddTabPage(string Name, Control TabPage)
        {
            try
            {
                tabControl1.TabPages.Add(Name, Name);
                TabPage.Parent = tabControl1.TabPages[Name];
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region//===================== 以下為事件處理 (VisibleChanged) =====================

        private void ucMultiSystem_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                UpdateControls();
            }
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }
        #endregion
    }
}

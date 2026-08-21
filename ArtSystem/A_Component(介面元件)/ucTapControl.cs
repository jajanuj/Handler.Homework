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
    public partial class ucTapControl : ucBaseUserControl
    {
        #region //===================== 變數設置 =====================

        private Dictionary<string, ucBaseUserControl> Dic_RegisterUC = new Dictionary<string, ucBaseUserControl>();
        private List<TabPage> Lst_TabPage = new List<TabPage>();

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
        public ucTapControl()
        {
            InitializeComponent();
            if (ArtSystem.clsArtSystem.bIsProgramOpen == false)
            {
                return;
            }
            this.VisibleChanged += new EventHandler(UserControl_VisibleChanged);
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                foreach (TabPage pTabPage in tabControl1.TabPages)
                {
                    if(Lst_TabPage.Contains(pTabPage) == false)
                    {
                        Lst_TabPage.Add(pTabPage);
                    }
                }
                for (int i = Lst_TabPage.Count; i < Dic_RegisterUC.Count; i++)
                {
                    Lst_TabPage.Add(new TabPage());
                    Lst_TabPage[i].Name = "TabPage" + (i + 1);
                }
                for (int i = 0; i < Lst_TabPage.Count; i++)
                {
                    if (i < Dic_RegisterUC.Count)
                    {
                        Lst_TabPage[i].Parent = tabControl1;
                    }
                    else
                    {
                        Lst_TabPage[i].Parent = null;
                    }
                }
                for (int i = 0; i < Dic_RegisterUC.Count; i++)
                {
                    Lst_TabPage[i].Text = clsLanguage.GetTranslation(Dic_RegisterUC.ElementAt(i).Key, false);
                    Dic_RegisterUC.ElementAt(i).Value.Parent = Lst_TabPage[i];
                    Dic_RegisterUC.ElementAt(i).Value.Dock = DockStyle.Fill;
                    Dic_RegisterUC.ElementAt(i).Value.BringToFront();
                    Dic_RegisterUC.ElementAt(i).Value.Visible = true;
                }
                tabControl1_Selecting(tabControl1, new TabControlCancelEventArgs(tabControl1.SelectedTab, tabControl1.SelectedIndex, false, TabControlAction.Selected));
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
            if (this.Visible == true)
            {
                UpdateControls();
            }
            else
            {
                ucBaseUserControl NowControl = null;
                if (tabControl1.SelectedTab != null)
                {
                    if (tabControl1.SelectedTab.Controls.Count > 0)
                    {
                        if (tabControl1.SelectedTab.Controls[0] is ucBaseUserControl)
                        {
                            NowControl = (ucBaseUserControl)tabControl1.SelectedTab.Controls[0];
                        }
                    }
                }
                foreach (ucBaseUserControl p_Control in Dic_RegisterUC.Values)
                {
                    if (NowControl != null && p_Control == NowControl)
                    {
                        p_Control.Visible = true;
                    }
                    p_Control.SetReflashTimerStart(false);
                    p_Control.Visible = false;
                }
            }
        }

        #endregion

        #region//===================== Public 函式 =====================

        public void _RegisterUC(string strName, ucBaseUserControl p_Control)
        {
            if (Dic_RegisterUC.ContainsKey(strName) == false)
            {
                Dic_RegisterUC.Add(strName, p_Control);
            }
        }


        #endregion

        #region//===================== Private 函式 =====================
        #endregion

        #region//===================== 事件處理 =====================

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    ucBaseUserControl NowControl = null;
            //    if (tabControl1.SelectedTab != null)
            //    {
            //        if (tabControl1.SelectedTab.Controls.Count > 0)
            //        {
            //            if (tabControl1.SelectedTab.Controls[0] is ucBaseUserControl)
            //            {
            //                NowControl = (ucBaseUserControl)tabControl1.SelectedTab.Controls[0];
            //            }
            //        }
            //    }
            //    foreach (ucBaseUserControl p_Control in Dic_RegisterUC.Values)
            //    {
            //        if (NowControl != null && p_Control == NowControl)
            //        {
            //            p_Control.Visible = true;
            //            p_Control.SetReflashTimerStart(true);
            //            continue;
            //        }
            //        p_Control.Visible = false;
            //        p_Control.SetReflashTimerStart(false);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    PublicDeclare.CatchLog(ex);
            //}
        }
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                ucBaseUserControl NowControl = null;
                if (e.TabPage != null)
                {
                    if (e.TabPage.Controls.Count > 0)
                    {
                        if (e.TabPage.Controls[0] is ucBaseUserControl)
                        {
                            NowControl = (ucBaseUserControl)e.TabPage.Controls[0];
                        }
                    }
                }
                foreach (ucBaseUserControl p_Control in Dic_RegisterUC.Values)
                {
                    if (NowControl != null && p_Control == NowControl)
                    {
                        p_Control.Visible = true;
                        p_Control.SetReflashTimerStart(true);
                        continue;
                    }
                    p_Control.Visible = false;
                    p_Control.SetReflashTimerStart(false);
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

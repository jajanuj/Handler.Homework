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

namespace ArtSystem.MultiSystem
{
    public partial class ucCtrlDispValve : ucBaseUserControl2
    {
        #region //========== 變數設置 ==========

        public Dictionary<clsPmtDispValve.enuPmtName, string> g_Pmt = null;
        public clsCtrlDispValve g_CtrlDispValve = null;

        public UserControl g_UcCtrlValve = null;

        #endregion

        #region //========== 必要函式設置 ==========

        //static private object objLock = new object();
        //static private ucCtrlDispValve m_Singleton;
        ///// <summary> 取得唯一物件，避免重覆設置  </summary>
        //static public ucCtrlDispValve GetSingleton()
        //{
        //    lock (objLock)
        //    {
        //        if (m_Singleton == null)
        //        {
        //            m_Singleton = new ucCtrlDispValve();
        //        }
        //    }
        //    return m_Singleton;
        //}

        /// <summary> 建構式 </summary>
        public ucCtrlDispValve()
        {
            InitializeComponent();
            if (ArtSystem.clsArtSystem.bIsProgramOpen == false)
            {
                return;
            }
            this.initialSize = this.Size;
            this.VisibleChanged += new EventHandler(UserControl_VisibleChanged);
        }

        /// <summary> 物件重置 </summary>
        public override void UpdateControls()
        {
            try
            {
                this.BorderStyle = BorderStyle.FixedSingle;
                if (g_UcCtrlValve == null)
                {
                    switch (this.g_CtrlDispValve.g_eValveType)
                    {
                        case clsCtrlDispValve.enuValveType.ArtSpray:
                            g_UcCtrlValve = new ucCtrlDispValve_ArtSpray();
                            break;
                        case clsCtrlDispValve.enuValveType.ArtPZT:
                            g_UcCtrlValve = new ucCtrlDispValve_ArtPZT();
                            break;
                        default:
                            break;
                    }
                }
                if (g_UcCtrlValve != null)
                {
                    if (g_UcCtrlValve is ucCtrlDispValve_ArtSpray
                        && g_CtrlDispValve is clsCtrlDispValve_ArtSpray)
                    {
                        ucCtrlDispValve_ArtSpray ucCtrlValve = (ucCtrlDispValve_ArtSpray)g_UcCtrlValve;
                        ucCtrlValve.g_CtrlDispValve = (clsCtrlDispValve_ArtSpray)g_CtrlDispValve;
                        ucCtrlValve.Parent = this;
                        ucCtrlValve.Dock = DockStyle.Fill;
                        ucCtrlValve.UpdateControls();
                    }
                    else if (g_UcCtrlValve is ucCtrlDispValve_ArtPZT
                        && g_CtrlDispValve is clsCtrlDispValve_ArtPZT)
                    {
                        ucCtrlDispValve_ArtPZT ucCtrlValve = (ucCtrlDispValve_ArtPZT)g_UcCtrlValve;
                        ucCtrlValve.g_CtrlDispValve = (clsCtrlDispValve_ArtPZT)g_CtrlDispValve;
                        ucCtrlValve.Parent = this;
                        ucCtrlValve.Dock = DockStyle.Fill;
                        ucCtrlValve.UpdateControls();
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
                if (g_UcCtrlValve != null)
                {
                    if (g_UcCtrlValve is ucCtrlDispValve_ArtSpray)
                    {
                        ucCtrlDispValve_ArtSpray ucCtrlValve = (ucCtrlDispValve_ArtSpray)g_UcCtrlValve;
                        ucCtrlValve.ReflashFunc();
                    }
                    else if (g_UcCtrlValve is ucCtrlDispValve_ArtPZT)
                    {
                        ucCtrlDispValve_ArtPZT ucCtrlValve = (ucCtrlDispValve_ArtPZT)g_UcCtrlValve;
                        ucCtrlValve.ReflashFunc();
                    }
                }
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
        }

        #endregion

        #region//========== Public 函式 ==========
        #endregion

        #region//========== Private 函式 ==========
        #endregion

        #region //========== ShowForm 函式設置 ==========

        private Size initialSize = new Size();
        /// <summary> 使用Form顯示 </summary>
        public void _ShowForm(bool Dialog = true)
        {
            if (this.Parent != null && this.Parent is Form == true)
            {
                Form mForm = (Form)this.Parent;
                if (Dialog == true)
                {
                    mForm.ShowDialog();
                    mForm.BringToFront();
                }
                else
                {
                    mForm.Show();
                    mForm.BringToFront();
                }
            }
            else
            {
                Form mForm = new Form();
                mForm.WindowState = FormWindowState.Normal;
                mForm.Size = new Size(this.initialSize.Width + 16, this.initialSize.Height + 39);
                mForm.StartPosition = FormStartPosition.CenterScreen;
                mForm.Text = clsLanguage.GetTranslation(this.Name, false);
                mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
                mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
                this.Parent = mForm;
                this.SetReflashTimerStart(true);
                this.Dock = DockStyle.Fill;
                if (Dialog == true)
                {
                    mForm.ShowDialog();
                }
                else
                {
                    mForm.Show();
                }
            }
        }
        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.SetReflashTimerStart(false);
                this.Parent = null;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void mForm_Deactivate(object sender, EventArgs e)
        {
            try
            {
                this.SetReflashTimerStart(false);
                this.Parent = null;
                Form mForm = (Form)sender;
                mForm.Close();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region//========== 事件處理 ==========
        #endregion

    }
}

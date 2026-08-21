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
using ArtSystem.MultiSystem;
using ArtAOI;
using ArtGrab;
using ArtInsp;
using ArtAOI.Align;
using Euresys.Open_eVision_1_2;

namespace ArtSystem.MultiSystem
{
    public partial class ucMultiLightCtrl : UserControl
    {
        #region //=====================  區域變數設置 =====================

        private List<ucLightCtrl> mLightCtrlUC = new List<ucLightCtrl>();
        private List<clsImgLight.clsChLight> listChLight = null;
        private ArtGrab.clsGrabImgMgr mImgMgr = null;
        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucMultiLightCtrl()
        {
            InitializeComponent();
        }


        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                clsArtGrabCtrl p_ArtGrab = ucArtAOI.GetSingleton().GetArtAOIGrabCtrl();
                for (int i = mLightCtrlUC.Count; i < listChLight.Count; i++)
                {
                    mLightCtrlUC.Add(new ucLightCtrl());
                    ucArtMain_Design.GetSingleton()._CollectChangeLanguageControls(mLightCtrlUC[mLightCtrlUC.Count - 1]);
                    if (p_ArtGrab != null)
                    {
                        mLightCtrlUC[mLightCtrlUC.Count - 1].Init(p_ArtGrab.GetLightMgr(),listChLight[i]);
                    }
                }
                for (int i = 0; i < mLightCtrlUC.Count; i++)
                {
                    mLightCtrlUC[i].Parent = this;
                    mLightCtrlUC[i].Left = 0;
                    mLightCtrlUC[i].Top = i * mLightCtrlUC[0].Height;
                }
                if (mLightCtrlUC.Count > 0)
                { this.Width = mLightCtrlUC[0].Width; }
                this.Height = mLightCtrlUC.Count * mLightCtrlUC[0].Height;
            }
            catch (Exception ex)
            {
                clsLog.Log(clsArtSystem.g_strCatchLogName, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================
        public void _Initial(string sPath)
        {
            if (mImgMgr == null)
            {
                if (System.IO.File.Exists(sPath) == true)
                {
                    mImgMgr = clsCommon.LoadFromXml<ArtGrab.clsGrabImgMgr>(sPath);
                    if (mImgMgr != null)
                    {
                        if (mImgMgr.listImgLight.Count > 0)
                        {
                            listChLight = mImgMgr.listImgLight[0].listChLight;
                        }
                    }
                }
            }
            UpdateControls();
        }

        public void _ShowFormDialog(string sCCDName = "")
        {
            Form mForm = new Form();
            this.Parent = mForm;
            this.Location = new Point(0, 0);
            mForm.Size = new Size(600, this.Size.Height + 39);
            mForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            mForm.MaximizeBox = false;
            mForm.MinimizeBox = false;
            mForm.StartPosition = FormStartPosition.CenterScreen;
            mForm.Text = clsLanguage.GetTranslation("Multi Light Control") + ((sCCDName == "") ? ("") : (" - " + sCCDName));
            mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
            mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
            UpdateControls();
            mForm.Show();
        }
        public int _GetChNum()
        {
            return mLightCtrlUC.Count;
        }
        public clsImgLight.clsChLight _GetLightValue(int p_Channelidx)
        {
            clsImgLight.clsChLight clsLight = null;
            if (mLightCtrlUC.Count != 0)
            {
                clsLight = mLightCtrlUC[p_Channelidx].GetLightValue();
            }
            return clsLight;
        }
        #endregion

        #region //===================== private 函式設置 =====================


        #endregion

        #region //===================== 以下為事件處理 =====================

        private void ucMultiLightCtrl_SizeChanged(object sender, EventArgs e)
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Height = this.Size.Height + 39;
            }
        }
        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Parent = null;
        }
        private void mForm_Deactivate(object sender, EventArgs e)
        {
            Form mForm = (Form)sender;
            mForm.Close();
        }

        #endregion





    }
}

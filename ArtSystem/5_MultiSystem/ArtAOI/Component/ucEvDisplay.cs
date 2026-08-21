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
using ArtInspEvShow;

namespace ArtSystem.MultiSystem
{
    public partial class ucEvDisplay : UserControl
    {
        #region //=====================  區域變數設置 =====================
        private ucImageCtrl mImgCtrl = null;
        private int iGapWidth = 0;
        private int iGapHeight = 43;
        private int iChGap = 5;
        private Size mShowImgSize = new Size();
        public Size _ShowImgSize
        {
            get
            {
                return mShowImgSize;
            }
        }
        public bool bInitialDone
        {
            get;
            private set;
        }
        public event artEVShowCtrl.dlgMouseClick _MouseClick = delegate { };

        private bool bShowControl = false;
        public bool _ShowControl
        {
            get
            {
                return bShowControl;
            }
            set
            {
                bShowControl = value;
                UpdateControls();
            }
        }
        private int iGrabChannel = 0;
        private ArtGrab.Camera.clsArtGrab mArtGrab = null;
        public ArtGrab.Camera.clsArtGrab _mArtGrab
        {
            get
            {
                return mArtGrab;
            }
        }
        private ucMultiLightCtrl mMultiLightCtrl = null;
        private bool NeedUpdateCameraDisplay = false;
        private clsHiPerfTimer mTime_Live = new clsHiPerfTimer();
        private bool bIsLive = false;
        private bool bMotionClick = false;
        public bool _bMotionClick
        {
            get
            {
                return bMotionClick;
            }
        }
        private bool bLoadStartImageDone = false;
        Dictionary<int, List<ArtInspEvShow.clsPaintShap>> DrawData = new Dictionary<int, List<ArtInspEvShow.clsPaintShap>>();
        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucEvDisplay()
        {
            InitializeComponent();
            mShowImgSize.Width = this.Width - iGapWidth;
            mShowImgSize.Height = this.Height - iGapHeight;
            this.SizeChanged += new EventHandler(ucDisplay_SizeChanged);
        }


        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                panel1.Width = this.Width;
                panel1.Height = this.Height;
                if (bShowControl == true)
                {
                    panel1.Width -= btnCapture.Width +3;
                    mShowImgSize.Width = this.Width - iGapWidth - btnCapture.Width - 6;
                    mShowImgSize.Height = this.Height - iGapHeight;
                }
                else
                {
                    mShowImgSize.Width = this.Width - iGapWidth;
                    mShowImgSize.Height = this.Height - iGapHeight;
                }

                btnCapture.Left = this.Width - btnCapture.Width - 3;
                btnLive.Left = this.Width - btnLive.Width - 3;
                btnSaveImg.Left = this.Width - btnSaveImg.Width - 3;
                btnLoadImg.Left = this.Width - btnLoadImg.Width - 3;
                btnLightCtrl.Left = this.Width - btnLoadImg.Width - 3;
                btnMotionCtrl.Left = this.Width - btnLoadImg.Width - 3;
                btnCapture.Visible = bShowControl;
                btnLive.Visible = bShowControl;
                btnSaveImg.Visible = bShowControl;
                btnLoadImg.Visible = bShowControl;
                btnLightCtrl.Visible = bShowControl;
                btnMotionCtrl.Visible = bShowControl;
                btnCapture.BringToFront();
                btnLive.BringToFront();
                btnSaveImg.BringToFront();
                btnLoadImg.BringToFront();
                btnLightCtrl.BringToFront();
                btnMotionCtrl.BringToFront();


            }
            catch (Exception ex)
            {
                clsLog.Log(clsArtSystem.g_strCatchLogName, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        public void ReflashFunc(Control pParent)
        {
            this.Parent = pParent;
            if (pParent != null)
            {
                if (this.Size != pParent.Size)
                {
                    this.Size = pParent.Size;
                    UpdateControls();
                }
            }
            if (_mArtGrab != null)
            {
                if (_mArtGrab.bSimulate == false)
                {
                    if (mTime_Live.IsTimeOut(300, clsCmData.enuSecUnit.MilliSec))
                    {
                        bIsLive = false;
                    }
                    else if (NeedUpdateCameraDisplay == true)
                    {
                        bIsLive = true;
                    }
                }
            }
            if (NeedUpdateCameraDisplay == true
                && _mArtGrab.bSimulate == false)
            {
                mTime_Live.Restart();
                EBaseROI CopyImage = clsEvImg.New(mArtGrab.GetEvSrcImg().Type, mArtGrab.GetEvSrcImg().Width, mArtGrab.GetEvSrcImg().Height);
                mArtGrab.GetEvSrcImg().CopyTo(CopyImage);
                mImgCtrl.EVShow.SetEVImageEx(CopyImage);
                NeedUpdateCameraDisplay = false;
            }
            if (_mArtGrab != null)
            {
                if (_mArtGrab.bSimulate == false)
                {
                    if (bIsLive == false)
                    {
                        bMotionClick = false;
                    }
                }
            }
            btnCapture.Enabled = bIsLive == false;
            btnLoadImg.Enabled = bIsLive == false;
            btnSaveImg.Enabled = bIsLive == false;
            btnMotionCtrl.Enabled = bIsLive == true;
            SetBtnColorFlash(btnLive, bIsLive);
            SetBtnColorFlash(btnMotionCtrl, bMotionClick);
            foreach (int iKey in DrawData.Keys)
            {
                foreach (ArtInspEvShow.clsPaintShap pPaint in DrawData[iKey])
                {
                    mImgCtrl.EVShow.AddPaint(iKey, pPaint);
                    mImgCtrl.EVShow.ShowPaint = true;
                }
                DrawData[iKey].Clear();
            }
            if (_mArtGrab != null)
            {
                if (bLoadStartImageDone == false)
                {
                    bLoadStartImageDone = true;
                    EROIBW8 Img = new EImageBW8(_mArtGrab.iWidth, _mArtGrab.iHeight);
                    EasyImage.Oper(EArithmeticLogicOperation.Copy, new EBW8(0), Img);
                    mImgCtrl.EVShow.SetEVImageEx(Img);
                    mImgCtrl.EVShow.FitPanel();
                }
            }
        }
        #endregion

        #region //===================== public 函式設置 =====================

        public void _Initial(int GrabChannel)
        {
            
            clsArtGrabCtrl p_ArtGrab = ucArtAOI.GetSingleton().GetArtAOIGrabCtrl();
            if (p_ArtGrab != null)
            {
                mArtGrab = p_ArtGrab.GetGrab(GrabChannel);
                if (mArtGrab != null)
                {
                    iGrabChannel = GrabChannel;
                    string CCDName = mArtGrab.sCCDName;
                    string strPath_LightSetting = clsMultiSystem.strSystemINIPath + "\\" + mArtGrab.sCCDName + "_ImgLight.xml";
                    if (mMultiLightCtrl == null)
                    { mMultiLightCtrl = new ucMultiLightCtrl(); }
                    mMultiLightCtrl._Initial(strPath_LightSetting);

                    if (mMultiLightCtrl._GetChNum() > 0)
                    { btnLightCtrl.Visible = true; }
                    else
                    { btnLightCtrl.Visible = false; }
                    mArtGrab.p_GrabNotice += new ArtGrab.Camera.clsArtGrab.ImgGrabNotice(GrabNotice);

                }
            }
            if (mImgCtrl == null)
            {
                if (mArtGrab != null)
                {
                    mImgCtrl = new ucImageCtrl(true, mArtGrab.dScaleXum, mArtGrab.dScaleYum, 1000, 0);
                }
                else
                {
                    mImgCtrl = new ucImageCtrl();
                }
                mImgCtrl.Parent = panel1;
                mImgCtrl.Location = new Point(0, 0);
                mImgCtrl.Width = panel1.Width + 3;
                mImgCtrl.Height = panel1.Height + 3;
                mImgCtrl.Visible = true;
                mImgCtrl.BringToFront();
                mImgCtrl.EVShow.FitPanel();
                mImgCtrl.EVShow.evMouseClick += new artEVShowCtrl.dlgMouseClick(EVShow_evMouseClick);
            }
            UpdateControls();
            bInitialDone = true;
        }

        public EBaseROI _GetEvImage()
        {
            if(mImgCtrl != null)
            {
                return mImgCtrl.EVShow.GetSRcEImageEx();
            }
            return null;
        }
        public void _ClearPaint(int GroupId)
        {
            if (DrawData.ContainsKey(GroupId) == true)
            {
                DrawData[GroupId].Clear();
            }
            if (mImgCtrl != null && _GetEvImage() != null)
            {
                mImgCtrl.EVShow.ClearPaint(GroupId);
                mImgCtrl.EVShow.ShowPaint = false;
            }
        }
        public void _ClearAllPaint()
        {
            DrawData.Clear();
            if (mImgCtrl != null && _GetEvImage() != null)
            {
                mImgCtrl.EVShow.ClearAllPaint();
            }
        }
        public void _AddPaint(int GroupId, ArtInspEvShow.clsPaintShap PaintData)
        {
            if(DrawData.ContainsKey(GroupId) == false)
            { DrawData.Add(GroupId, new List< ArtInspEvShow.clsPaintShap>()); }
            DrawData[GroupId].Add(PaintData);
            //if (mImgCtrl != null && _GetEvImage() != null)
            //{
            //    mImgCtrl.EVShow.AddPaint(GroupId, PaintData);
            //    mImgCtrl.EVShow.ShowPaint = true;
            //}
        }

        #endregion

        #region //===================== private 函式設置 =====================


        private void SetBtnColorFlash(Button pButton, bool bIsLime)
        {
            if (bIsLime)
            {
                pButton.BackColor = Color.Lime;
            }
            else
            {
                pButton.BackColor = this.BackColor;
                pButton.UseVisualStyleBackColor = true;
            }
        }
        #endregion

        #region//===================== 以下為事件處理 =====================
        private void GrabNotice(EBaseROI inputImage)
        {
            NeedUpdateCameraDisplay = true;
        }
        private void GrabNotice_Mil(int iInputImage)
        {
            NeedUpdateCameraDisplay = true;
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            if (mImgCtrl != null)
            {
                mImgCtrl.Location = new Point(0, 0);
                mImgCtrl.Width = panel1.Width;
                mImgCtrl.Height = panel1.Height;
            }
        }
        private void ucDisplay_SizeChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void EVShow_evMouseClick(artEVShowCtrl Sender, artEVShowCtrl.clsMouseArgs e)
        {
            _MouseClick(Sender, e);
        }



        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (mArtGrab != null)
            {
                _ClearAllPaint();
                mArtGrab.SingleGrabInsp();
            }
        }
        private void btnLive_Click(object sender, EventArgs e)
        {
            if (mArtGrab != null)
            {
                if (bIsLive == false)
                {
                    if (_mArtGrab.bSimulate == true)
                    {
                        NeedUpdateCameraDisplay = true;
                        bIsLive = true;
                    }
                    _ClearAllPaint();
                    mArtGrab.LiveGrab();
                }
                else
                {
                    if (_mArtGrab.bSimulate == true)
                    {
                        NeedUpdateCameraDisplay = false;
                        bIsLive = false;
                        bMotionClick = false;
                    }
                    mArtGrab.SingleGrabInsp();
                }
            }
        }
        private void btnSaveImg_Click(object sender, EventArgs e)
        {
            SaveFileDialog mDialog_SelectFile = new SaveFileDialog();
            mDialog_SelectFile.Filter = "Image Files(*.bmp;*.jpg)|*.bmp;*.jpg";
            mDialog_SelectFile.Title = "Open Image File";
            if (mDialog_SelectFile.ShowDialog() == System.Windows.Forms.DialogResult.OK && mDialog_SelectFile.FileName != "")
            {
                _GetEvImage().Save(mDialog_SelectFile.FileName);
            }
            mDialog_SelectFile.Dispose();
        }
        private void btnLoadImg_Click(object sender, EventArgs e)
        {
            NeedUpdateCameraDisplay = false;
            OpenFileDialog mDialog_SelectFile = new OpenFileDialog();
            mDialog_SelectFile.Filter = "Image Files(*.bmp;*.jpg)|*.bmp;*.jpg";
            mDialog_SelectFile.Title = "Open Image File";
            if (mDialog_SelectFile.ShowDialog() == System.Windows.Forms.DialogResult.OK && mDialog_SelectFile.FileName != "")
            {
                EImageC24 mEVImg = new EImageC24();
                mEVImg.Load(mDialog_SelectFile.FileName);
                mImgCtrl.EVShow.SetEVImageEx(mEVImg);
                mImgCtrl.EVShow.FitPanel();
                mImgCtrl.Visible = true;
                mImgCtrl.BringToFront();
                _ClearAllPaint();
            }
            mDialog_SelectFile.Dispose();
        }
        private void btnLightCtrl_Click(object sender, EventArgs e)
        {
            if (mMultiLightCtrl != null)
            {
                //mMultiLightCtrl._ShowFormDialog(clsLanguage.GetTranslation("CCD1"));
                mMultiLightCtrl._ShowFormDialog(clsLanguage.GetTranslation(mArtGrab.sCCDName));
            }
        }
        private void btnMotionCtrl_Click(object sender, EventArgs e)
        {
            bMotionClick = !bMotionClick;
        }
        #endregion

    }
}

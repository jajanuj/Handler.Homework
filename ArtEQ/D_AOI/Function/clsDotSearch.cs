using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtAOI;
using ArtInsp;
using ArtAOI.Align;
using ArtAOI.Machine;
using Euresys.Open_eVision_1_2;
using ArtControlLib;
using ArtCommonLib;
using ArtData;

namespace ArtEQ
{
    public class clsDotSearch
    {
        #region//========== 外部檔案參數 ==========
        public bool bIsXML = false;
        public string strPath = "";
        public double dPixelSizeX = 5.5;
        public double dPixelSizeY = 5.5;
        /// <summary> 0=Gray, 1=Red, 2=Green, 3=Blue </summary>
        public int iBandConvert = 0;
        public clsEvEngine.clsEvBlob mEVBlob = new clsEvEngine.clsEvBlob();
        public clsEvEngine.clsEvCircleGauge mEVCircleGauge = new clsEvEngine.clsEvCircleGauge();
        #endregion

        #region//========== 內部參數 ==========

        private string strNgReason_SearchDot = "";
        private double dTestTime_ms = 0;

        #endregion

        #region//========== 必要函式(建構式) ==========

        public clsDotSearch()
        {
        }
        public clsDotSearch(string sPmtPath, double PixelSizeX = 5.5, double PixelSizeY = 5.5)
        {
            strPath = sPmtPath;
            dPixelSizeX = PixelSizeX;
            dPixelSizeY = PixelSizeY;
        }

        #endregion

        #region//========== Public函式 (參數存取) ==========
        public bool Save(bool CanShowMessageBox = true)
        {
            return Save(strPath, CanShowMessageBox);
        }
        public bool Load(bool CanShowMessageBox = true)
        {
            return Load(strPath, CanShowMessageBox);
        }
        public bool Save(string FilePath, bool CanShowMessageBox = true)
        {
            bool rValue = false;
            try
            {
                if (FilePath != "")
                {
                    strPath = FilePath;
                }

                if (System.IO.Path.GetExtension(strPath).Contains("xml") == true)
                {
                    bIsXML = true;
                }
                else
                {
                    bIsXML = false;
                    if (System.IO.Path.GetExtension(strPath).Contains("ini") == false)
                    {
                        strPath = "";
                    }
                }
                if (strPath == "")
                {
                    System.Windows.Forms.SaveFileDialog m_SaveImgDlg = new System.Windows.Forms.SaveFileDialog();
                    if (bIsXML == true)
                    {
                        m_SaveImgDlg.Filter = "XML File(*.xml)|*.xml";
                    }
                    else
                    {
                        m_SaveImgDlg.Filter = "INI File(*.ini)|*.ini";
                    }
                    m_SaveImgDlg.Title = "Save Pmt File";
                    if (m_SaveImgDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK && m_SaveImgDlg.FileName != "")
                    {
                        strPath = m_SaveImgDlg.FileName;
                    }
                    m_SaveImgDlg.Dispose();
                }
                if (bIsXML == true)
                {
                    clsCommon.SaveToXml<clsDotSearch>(this, strPath);
                }
                else
                {
                    ArtSystem.Files.JsonHelper.JsonSerializeToFile(this, strPath, Encoding.Unicode);
                }
                if (System.IO.File.Exists(strPath) == true)
                {
                    rValue = true;
                }
                else
                {
                    if (CanShowMessageBox == true)
                    {
                        formMessageBox.Show("Save File Fail!", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {
                if (CanShowMessageBox == true)
                {
                    formMessageBox.Show("Save File Fail!", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            return rValue;
        }
        public bool Load(string FilePath, bool CanShowMessageBox = true)
        {
            bool rValue = false;
            try
            {
                if (FilePath != "")
                {
                    strPath = FilePath;
                }
                if (System.IO.Path.GetExtension(strPath).Contains("xml") == true)
                {
                    bIsXML = true;
                }
                else
                {
                    bIsXML = false;
                    if (System.IO.Path.GetExtension(strPath).Contains("ini") == false)
                    {
                        strPath = "";
                    }
                }
                if (System.IO.File.Exists(strPath) == true)
                {
                    clsDotSearch Temp = null;
                    string sPath = this.strPath;
                    if (bIsXML == true)
                    {
                        Temp = clsCommon.LoadFromXml<clsDotSearch>(strPath);
                    }
                    else
                    {
                        Temp = ArtSystem.Files.JsonHelper.JsonDeserializeFromFile<clsDotSearch>(strPath, Encoding.Unicode);
                    }
                    if (Temp != null)
                    {
                        ArtDeepCloner.DeepClonerExtensions.DeepCloneTo<clsDotSearch, clsDotSearch>(Temp, this);
                        if (this.strPath != sPath)
                        {
                            Save(sPath, CanShowMessageBox);
                            this.strPath = sPath;
                        }
                        rValue = true;
                    }
                    else
                    {
                        if (CanShowMessageBox == true)
                        {
                            formMessageBox.Show("Load File Fail!", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    if (CanShowMessageBox == true)
                    {
                        formMessageBox.Show("Load File Fail!", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    }
                }
            }
            catch
            {
                if (CanShowMessageBox == true)
                {
                    formMessageBox.Show("Load File Fail!", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            return rValue;
        }
        #endregion

        #region//========== Public函式 (教導介面) ==========
        public bool FormEdit_Blob(EBaseROI pEVImage)
        {
            bool rValue = false;
            EBaseROI SrcImg = null;
            if (pEVImage is EImageBW8)
            {
                SrcImg = pEVImage;
            }
            else
            {
                SrcImg = new EImageBW8(pEVImage.Width, pEVImage.Height);
                switch (iBandConvert)
                {
                    case 1://Red
                        EasyColor.GetComponent((EROIC24)pEVImage, (EROIBW8)SrcImg, 0);
                        break;
                    case 2://Green
                        EasyColor.GetComponent((EROIC24)pEVImage, (EROIBW8)SrcImg, 1);
                        break;
                    case 3://Blue
                        EasyColor.GetComponent((EROIC24)pEVImage, (EROIBW8)SrcImg, 2);
                        break;
                    default://Gray
                        EasyImage.Convert((EROIC24)pEVImage, (EROIBW8)SrcImg);
                        break;
                }
            }
            clsEvEngine.clsEvOutput mEVOutput = new clsEvEngine.clsEvOutput(SrcImg, dPixelSizeX, dPixelSizeY);
            ucBlob.CreateForm(mEVOutput, mEVOutput, mEVBlob, new ucBlob.clsUiOption()).ShowDialog();
            if (formMessageBox.Show("Do you want to Save Pmt?", "Save Circle Gauge Pmt",
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Save();
                rValue = true;
            }
            else
            {
                Load();
            }
            return rValue;
        }
        public bool FormEdit_CircleGauge(EBaseROI pEVImage)
        {
            bool rValue = false;
            EBaseROI SrcImg = null;
            if (pEVImage is EImageBW8)
            {
                SrcImg = pEVImage;
            }
            else
            {
                SrcImg = new EImageBW8(pEVImage.Width, pEVImage.Height);
                switch (iBandConvert)
                {
                    case 1://Red
                        EasyColor.GetComponent((EROIC24)pEVImage, (EROIBW8)SrcImg, 0);
                        break;
                    case 2://Green
                        EasyColor.GetComponent((EROIC24)pEVImage, (EROIBW8)SrcImg, 1);
                        break;
                    case 3://Blue
                        EasyColor.GetComponent((EROIC24)pEVImage, (EROIBW8)SrcImg, 2);
                        break;
                    default://Gray
                        EasyImage.Convert((EROIC24)pEVImage, (EROIBW8)SrcImg);
                        break;
                }
            }
            clsEvEngine.clsEvOutput mEVOutput = new clsEvEngine.clsEvOutput(SrcImg, dPixelSizeX, dPixelSizeY);
            clsEvEngine.clsEvOutput mEVBlobOutput = new clsEvEngine.clsEvOutput(SrcImg, dPixelSizeX, dPixelSizeY);
            if (RunBlob(SrcImg, ref mEVBlobOutput) == true)
            {
                float OrgCenterX = mEVCircleGauge.Gauge.CenterX;
                float OrgCenterY = mEVCircleGauge.Gauge.CenterY;
                //float OrgDimeter = mEVCircleGauge.Gauge.Diameter;
                if (mEVBlobOutput.BlobList.Count > 0)
                {
                    double dOffsetX = 0;
                    double dOffsetY = 0;
                    //double dDimeter = OrgDimeter;
                    double dImgCenterX = SrcImg.Width / 2;
                    double dImgCenterY = SrcImg.Height / 2;
                    for (int i = 0; i < mEVBlobOutput.BlobList.Count; i++)
                    {
                        if (dOffsetX == 0 && dOffsetY == 0)
                        {
                            dOffsetX = mEVBlobOutput.BlobList[i].BBCenterX;
                            dOffsetY = mEVBlobOutput.BlobList[i].BBCenterY;
                            //dDimeter = (mEVBlobOutput.BlobList[i].BBWidth + mEVBlobOutput.BlobList[i].BBHeight) / 2;
                        }
                        else
                        {
                            double PreviousLength = Count_2PointLength(dImgCenterX, dImgCenterY, dOffsetX, dOffsetY);
                            double NewLength = Count_2PointLength(dImgCenterX, dImgCenterY, mEVBlobOutput.BlobList[i].BBCenterX, mEVBlobOutput.BlobList[i].BBCenterY);
                            if (NewLength < PreviousLength)
                            {
                                dOffsetX = mEVBlobOutput.BlobList[i].BBCenterX;
                                dOffsetY = mEVBlobOutput.BlobList[i].BBCenterY;
                                //dDimeter = (mEVBlobOutput.BlobList[i].BBWidth + mEVBlobOutput.BlobList[i].BBHeight) / 2;
                            }
                        }
                    }
                    mEVCircleGauge.Gauge.CenterX = (float)dOffsetX;
                    mEVCircleGauge.Gauge.CenterY = (float)dOffsetY;
                    //mEVCircleGauge.Gauge.Diameter = (float)dDimeter;
                }
            }
            ucCircleGauge.CreateForm(mEVOutput, mEVCircleGauge).ShowDialog();
            if (formMessageBox.Show("Do you want to Save Pmt?", "Save Circle Gauge Pmt",
                System.Windows.Forms.MessageBoxButtons.YesNo,
                System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Save(strPath);
                rValue = true;
            }
            else
            {
                Load(strPath);
            }
            return rValue;
        }
        #endregion

        #region//========== Public函式 (執行功能) ==========
        /// <summary> 搜尋樣品 </summary>
        public bool RunBlob(EBaseROI pEVImage, ref clsEvEngine.clsEvOutput mResult)
        {
            bool rValue = false;
            clsHiPerfTimer mTimer = new clsHiPerfTimer();
            mTimer.Restart();
            try
            {
                if (mResult != null && mResult.ScaleX != 0 && mResult.ScaleY != 0)
                {
                    dPixelSizeX = mResult.ScaleX;
                    dPixelSizeY = mResult.ScaleY;
                }
                EBaseROI SrcImg = Convert_GrayImg(pEVImage, iBandConvert);
                clsEvEngine.clsEvOutput mEVOutput = new clsEvEngine.clsEvOutput(SrcImg, dPixelSizeX, dPixelSizeY);
                if (clsEvEngine.clsEvBlob.Run(mEVOutput, mEVBlob, out mResult) == true)
                {
                    if (mResult.BlobList.Count > 0)
                    {
                        rValue = true;
                    }
                }
            }
            catch
            {
            }
            mTimer.Stop();
            dTestTime_ms = mTimer.ElapsedMilliseconds;
            return rValue;
        }
        /// <summary> 執行Circle尋邊 </summary>
        public bool RunCircleGauge(EBaseROI pEVImage, ref clsEvEngine.clsEvOutput mResult)
        {
            bool rValue = false;
            clsHiPerfTimer mTimer = new clsHiPerfTimer();
            mTimer.Restart();
            try
            {
                if (mResult != null && mResult.ScaleX != 0 && mResult.ScaleY != 0)
                {
                    dPixelSizeX = mResult.ScaleX;
                    dPixelSizeY = mResult.ScaleY;
                }
                EBaseROI SrcImg = Convert_GrayImg(pEVImage, iBandConvert);
                clsEvEngine.clsEvOutput mEVOutput = new clsEvEngine.clsEvOutput(SrcImg, dPixelSizeX, dPixelSizeY);
                if (clsEvEngine.clsEvCircleGauge.Run(mEVOutput, mEVCircleGauge, out mResult) == true)
                {
                    clsVision.clsCircleGaugeRes mCircleResult = null;
                    if (ConvertResult(mResult, out mCircleResult) == true)
                    {
                        if (mCircleResult.Diameter > 0)
                        {
                            rValue = true;
                        }
                    }
                }
            }
            catch
            {
            }
            mTimer.Stop();
            dTestTime_ms = mTimer.ElapsedMilliseconds;
            return rValue;
        }
        /// <summary> 執行尋找Dot </summary>
        public bool RunSearchDot(EBaseROI pEVImage, ref clsEvEngine.clsEvOutput mResult)
        {
            bool rValue = false;
            clsHiPerfTimer mTimer = new clsHiPerfTimer();
            mTimer.Restart();
            strNgReason_SearchDot = "";
            try
            {
                EBaseROI SrcImg = Convert_GrayImg(pEVImage, iBandConvert);
                if (RunBlob(SrcImg, ref mResult) == true)
                {
                    double dOffsetX = 0;
                    double dOffsetY = 0;
                    float OrgCenterX = mEVCircleGauge.Gauge.CenterX;
                    float OrgCenterY = mEVCircleGauge.Gauge.CenterY;
                    double dImgCenterX = SrcImg.Width / 2;
                    double dImgCenterY = SrcImg.Height / 2;
                    if (mResult.BlobList.Count == 0)
                    {
                        strNgReason_SearchDot += (strNgReason_SearchDot == "") ? "" : "\r\n";
                        strNgReason_SearchDot += "Blob Search Empty Items";
                    }
                    else
                    {
                        for (int i = 0; i < mResult.BlobList.Count; i++)
                        {
                            if (dOffsetX == 0 && dOffsetY == 0)
                            {
                                dOffsetX = mResult.BlobList[i].BBCenterX;
                                dOffsetY = mResult.BlobList[i].BBCenterY;
                            }
                            else
                            {
                                double PreviousLength = Count_2PointLength(dImgCenterX, dImgCenterY, dOffsetX, dOffsetY);
                                double NewLength = Count_2PointLength(dImgCenterX, dImgCenterY, mResult.BlobList[i].BBCenterX, mResult.BlobList[i].BBCenterY);
                                if (NewLength < PreviousLength)
                                {
                                    dOffsetX = mResult.BlobList[i].BBCenterX;
                                    dOffsetY = mResult.BlobList[i].BBCenterY;
                                }
                            }
                        }
                        mEVCircleGauge.Gauge.CenterX = (float)dOffsetX;
                        mEVCircleGauge.Gauge.CenterY = (float)dOffsetY;

                        if (RunCircleGauge(SrcImg, ref mResult) == true)
                        {
                            if (mResult.ResInfo is clsVision.clsCircleGaugeRes)
                            {
                                clsVision.clsCircleGaugeRes mCResult = (clsVision.clsCircleGaugeRes)mResult.ResInfo;
                                if (mCResult.Validity == true)
                                {
                                    rValue = true;
                                }
                                else
                                {
                                    strNgReason_SearchDot += (strNgReason_SearchDot == "") ? "" : "\r\n";
                                    strNgReason_SearchDot += "Circle Gauge Result Invalid";
                                }
                            }
                            else
                            {
                                strNgReason_SearchDot += (strNgReason_SearchDot == "") ? "" : "\r\n";
                                strNgReason_SearchDot += "Circle Gauge Result Convert Error";
                            }
                        }
                        else
                        {
                            strNgReason_SearchDot += (strNgReason_SearchDot == "") ? "" : "\r\n";
                            strNgReason_SearchDot += "Run Circle Gauge Fail";
                        }
                        mEVCircleGauge.Gauge.CenterX = OrgCenterX;
                        mEVCircleGauge.Gauge.CenterY = OrgCenterY;
                    }
                }
                else
                {
                    strNgReason_SearchDot += (strNgReason_SearchDot == "") ? "" : "\r\n";
                    strNgReason_SearchDot += "Run Blob Fail";
                }
            }
            catch
            {
            }
            mTimer.Stop();
            dTestTime_ms = mTimer.ElapsedMilliseconds;
            return rValue;
        }
        #endregion

        #region//========== Public函式 (其他 : ConvertResult, GetNgReason) ===========

        /// <summary> 轉換成定位Circle的結果 </summary>
        public bool ConvertResult(clsEvEngine.clsEvOutput mResult, out  clsVision.clsCircleGaugeRes mCircleResult)
        {
            bool rValue = false;
            mCircleResult = null;
            if (mResult != null)
            {
                if (mResult.ResInfo is clsVision.clsCircleGaugeRes)
                {
                    mCircleResult = (clsVision.clsCircleGaugeRes)mResult.ResInfo;
                    rValue = true;
                }
            }
            return rValue;
        }

        public double GetCircleResult_CenterXmm(clsEvEngine.clsEvOutput mResult)
        {
            double rValue = 0;
            if (mResult != null)
            {
                if (mResult.ResInfo is clsVision.clsCircleGaugeRes)
                {
                    //取得結果(pixel)
                    rValue = ((clsVision.clsCircleGaugeRes)mResult.ResInfo).CenterX;
                    //轉換成(um)
                    rValue *= mResult.ScaleX;
                    //轉換成(mm)
                    rValue /= 1000;
                }
            }
            return rValue;
        }
        public double GetCircleResult_CenterYmm(clsEvEngine.clsEvOutput mResult)
        {
            double rValue = 0;
            if (mResult != null)
            {
                if (mResult.ResInfo is clsVision.clsCircleGaugeRes)
                {
                    //取得結果(pixel)
                    rValue = ((clsVision.clsCircleGaugeRes)mResult.ResInfo).CenterY;
                    //轉換成(um)
                    rValue *= mResult.ScaleY;
                    //轉換成(mm)
                    rValue /= 1000;
                }
            }
            return rValue;
        }
        public double GetCircleResult_Diametermm(clsEvEngine.clsEvOutput mResult)
        {
            double rValue = 0;
            if (mResult != null)
            {
                if (mResult.ResInfo is clsVision.clsCircleGaugeRes)
                {
                    //取得結果(pixel)
                    rValue = ((clsVision.clsCircleGaugeRes)mResult.ResInfo).Diameter;
                    //轉換成(um)
                    rValue *= mResult.GetAvgScale();
                    //轉換成(mm)
                    rValue /= 1000;
                }
            }
            return rValue;
        }
        public string GetNgReason()
        {
            return strNgReason_SearchDot;
        }
        public double GetFunctionSpendTime_ms()
        {
            return dTestTime_ms;
        }
        #endregion

        #region//========== Private函式 (Count_2PointLenght) ===========
        /// <summary> 利用兩點座標計算其長度  </summary>
        private double Count_2PointLength(double StartX, double StartY, double EndX, double EndY)
        {
            return Math.Pow(Math.Pow(EndX - StartX, 2) + Math.Pow(EndY - StartY, 2), 0.5);
        }
        /// <summary> 0=Gray, 1=Red, 2=Green, 3=Blue </summary>
        private EBaseROI Convert_GrayImg(EBaseROI pEVImage, int iBand)
        {
            EBaseROI ConvertImg = null;
            if (pEVImage is EImageBW8)
            {
                ConvertImg = pEVImage;
            }
            else
            {
                ConvertImg = new EImageBW8(pEVImage.Width, pEVImage.Height);
                switch (iBand)
                {
                    case 1://Red
                        EasyColor.GetComponent((EROIC24)pEVImage, (EROIBW8)ConvertImg, 0);
                        break;
                    case 2://Green
                        EasyColor.GetComponent((EROIC24)pEVImage, (EROIBW8)ConvertImg, 1);
                        break;
                    case 3://Blue
                        EasyColor.GetComponent((EROIC24)pEVImage, (EROIBW8)ConvertImg, 2);
                        break;
                    default://Gray
                        EasyImage.Convert((EROIC24)pEVImage, (EROIBW8)ConvertImg);
                        break;
                }
            }
            return ConvertImg;
        }
        #endregion

    }
}

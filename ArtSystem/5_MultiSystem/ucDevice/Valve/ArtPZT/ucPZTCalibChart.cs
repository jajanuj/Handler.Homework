using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PZT_Algorithm;
using System.Windows.Forms.DataVisualization.Charting;
using ArtCommonLib;
using ArtData;
using ArtSystem;
using System.Threading;
using System.Timers;

namespace ArtSystem.MultiSystem
{
    public partial class ucPZTCalibChart : UserControl
    {

        public Size initialSize = new Size();

        public ucPZTCalibChart()
        {
            InitializeComponent();
            initialSize = this.Size;
        }

        private static ucPZTCalibChart m_Singleton;

        public List<uint> m_lstX_DAC = new List<uint>();
        public List<uint> m_lstY_mV = new List<uint>();

        List<CurvePoint> m_lstAllCurvePoints = new List<CurvePoint>();

        List<CurvePoint> m_lstCurvePoints = new List<CurvePoint>();

        public List<CurvePoint> m_lstTargetCurvePoints = new List<CurvePoint>();

        int m_iCurveVCount = 0;

        bool m_IsSkipRedLine = true;//false;

        public double m_dbldblDiffSpec = -0.0001;//A507

        public bool bIsAdnormalClose = false;

        private double dTempUpLimit = 0;
        private double daverage = 0;
        /// <summary>
        /// 閉鎖曲線斜率比對值(Default:35)
        /// </summary>
        public int dSlopSpec = 35;
        /// <summary>
        /// 閉鎖曲線初始電壓值(Default:1500)
        /// </summary>
        public uint unUpperDAC = 1500;


        /// <summary>
        /// 閉鎖曲線初始電壓值(Default:4095)
        /// </summary>
        public uint unUpperDAC2 = 3600;//CC0320

        /// <summary>
        /// 閉鎖校正曲線斜率判別上限下移比率(Default:25%)
        /// </summary>
        public int iAverageDownPercentage = 30;
        /// <summary>
        /// 閉鎖校正曲線過濾點數量(Default:5)
        /// </summary>
        public int iVFilterPoint = 5;

        public double dVLowestPointSpec = -0.0008;

        public double dMinPoint = 0;//CC0320
        public double dAveragePoint = 0;//CC0320
        public double dLimit = 0;//CC0320
        #region ===========================Private===========================

        /// <summary>
        /// 計算X DAC與Y mV值
        /// </summary>
        /// <param name="X_DAC"></param>
        /// <param name="Y_mV"></param>
        /// <returns>0:Pass 1:Error</returns>
        public int DrewChart(List<uint> X_DAC, List<uint> Y_mV)
        {
            int iResult = 1;
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => iResult = DrewChart(X_DAC, Y_mV)));
                    return iResult;
                }
                double X_Voltage = 0;

                PZT_Algorithm_ m_PZT_Algorithm = new PZT_Algorithm_();

                m_PZT_Algorithm.Calculate_LockPZT_Position(X_DAC, Y_mV);

                TurningPoint_Prmt tp = m_PZT_Algorithm.tp;

                this.Chart1.Series.Clear();
                m_lstAllCurvePoints.Clear();

                Series NewSerie1 = new Series("Series1");
                NewSerie1.ChartType = SeriesChartType.Line;
                Series NewSerie2 = new Series("Series2");
                NewSerie2.ChartType = SeriesChartType.Line;
                this.Chart1.Series.Add(NewSerie1);
                this.Chart1.Series.Add(NewSerie2);
                //this.Chart1.Series["Series1"].Points.Clear();
                //this.Chart1.Series["Series2"].Points.Clear();

                this.Chart1.ChartAreas[0].AxisY.Maximum = Math.Round(tp.Rate2_Avrg.Max(), 6);
                this.Chart1.ChartAreas[0].AxisY.Minimum = Math.Round(tp.Rate2_Avrg.Min(), 6);
                this.Chart1.ChartAreas[0].AxisY.Interval = Math.Round((tp.Rate2_Avrg.Max() - tp.Rate2_Avrg.Min()) / 6, 6);

                int iMinX = (int)unUpperDAC;
                //int iMaxX = (int)unUpperDAC2;//CC0320
                this.Chart1.ChartAreas[0].AxisX.Minimum = iMinX;
                //this.Chart1.ChartAreas[0].AxisX.Maximum = iMaxX;
                List<double> LstData = new List<double>();
                for (int i = 0; i < tp.Rate2_Avrg.Count(); i++)
                {
                    X_Voltage = X_DAC[i];
                    if (X_Voltage < iMinX)//CC0320
                    {
                        continue;
                    }
                    //                X_Voltage = X_Voltage * 5 * 31.5 / 4095;
                    CurvePoint mPt = new CurvePoint();
                    mPt.Voltage = X_Voltage;
                    mPt.Slop_Avage = tp.Rate2_Avrg[i];
                    m_lstAllCurvePoints.Add(mPt);
                    this.Chart1.Series["Series1"].Points.AddXY(mPt.Voltage, mPt.Slop_Avage);
                    LstData.Add(mPt.Slop_Avage);
                }

                //CC0320
                //for (int i = 0; i < tp.Rate2_Avrg.Count(); i++)
                //{
                //    X_Voltage = X_DAC[i];
                //    if (X_Voltage < iMinX || X_Voltage > iMaxX)
                //    {
                //        continue;
                //    }
                //    //                X_Voltage = X_Voltage * 5 * 31.5 / 4095;
                //    CurvePoint mPt = new CurvePoint();
                //    mPt.Voltage = X_Voltage;
                //    mPt.Slop_Avage = tp.Rate2_Avrg[i];
                //    LstData.Add(mPt.Slop_Avage);
                //}

                #region//20250401 增加最低點上下600 DAC 才算25%
                List<double> LstData_Limit = new List<double>();
                int LowestPoint_Index = 0;
                double LowestPoint_Slop_Avage = 0;
                int LimitVoltage = 600;
                int LimitCount = LimitVoltage / 15;
                int LimitLowIndex = 0;
                int LimitDataCount = 0;


                LowestPoint_Slop_Avage = m_lstAllCurvePoints[0].Slop_Avage;

                for (int i = 0; i < m_lstAllCurvePoints.Count - 1; i++)
                {
                    CurvePoint CurrentPoint = m_lstAllCurvePoints[i];

                    if (CurrentPoint.Slop_Avage < LowestPoint_Slop_Avage)
                    {
                        LowestPoint_Slop_Avage = CurrentPoint.Slop_Avage;
                        LowestPoint_Index = i;
                    }
                }

                if (LowestPoint_Index - LimitCount < 0)
                {
                    LimitLowIndex = 0;
                }
                else
                {
                    LimitLowIndex = LowestPoint_Index - LimitCount;
                }

                if (LowestPoint_Index+(LimitCount * 2 + 1) > LstData.Count - 1)
                {
                    LimitDataCount = LstData.Count - LowestPoint_Index + LimitCount;//之前忘記加LimitCount了
                }
                else
                {
                    LimitDataCount = LimitCount * 2 + 1;
                }



                LstData_Limit = LstData.GetRange(LimitLowIndex, LimitDataCount);
                #endregion

                #region//計算 V 上位卡控數值(增加最低點前後600MDV)
                double dAverageCount = 0;
                {
                    double MinPoint = LstData_Limit.Min();
                    dAverageCount = LstData_Limit.Average();
                    List<int> RemoveIndex = new List<int>();
                    for (int i = 0; i < LstData_Limit.Count; i++)
                    {
                        if (LstData_Limit[i] < dAverageCount)
                        {
                            RemoveIndex.Add(i);
                        }
                    }

                    for (int i = RemoveIndex.Count - 1; i >= 0; i--)
                    {
                        LstData_Limit.RemoveAt(RemoveIndex[i]);
                    }

                    dAverageCount = LstData_Limit.Average();
                    dTempUpLimit = dAverageCount - Math.Abs(dAverageCount - MinPoint) * iAverageDownPercentage / 100;
                    dMinPoint = MinPoint;//CC0320
                    dAveragePoint = dAverageCount;//CC0320
                }
                #endregion


                #region//計算 V 上位卡控數值(原版取全部)
                //double dAverageCount = 0;
                //{
                //    double MinPoint = LstData.Min();
                //    dAverageCount = LstData.Average();
                //    //daverage = dAverageCount;
                //    List<int> RemoveIndex = new List<int>();
                //    for (int i = 0; i < LstData.Count; i++)
                //    {
                //        if (LstData[i] < dAverageCount)
                //        {
                //            RemoveIndex.Add(i);
                //        }
                //    }

                //    for (int i = RemoveIndex.Count - 1; i >= 0; i--)
                //    {
                //        LstData.RemoveAt(RemoveIndex[i]);
                //    }
                //    dAverageCount = LstData.Average();
                //    dTempUpLimit = dAverageCount - Math.Abs(dAverageCount - MinPoint) * iAverageDownPercentage / 100;
                //    dMinPoint = MinPoint;//CC0320
                //    dAveragePoint = dAverageCount;//CC0320
                //}
                #endregion

                if (this.Chart1.ChartAreas.Count > 0)
                {
                    this.Chart1.ChartAreas[0].AxisY.StripLines.Clear();

                    //// 在 ChartArea 添加 StripLine
                    //StripLine stripLine1 = new StripLine()
                    //{
                    //    Interval = 0, // 不重複
                    //    StripWidth = 0, // 單一條線
                    //    IntervalOffset = dAverageCount, // 水平線所在的 Y 值
                    //    BackColor = System.Drawing.Color.Red,
                    //    BorderWidth = 1,
                    //    BorderColor = System.Drawing.Color.Red
                    //};
                    //this.Chart1.ChartAreas[0].AxisY.StripLines.Add(stripLine1);

                    // 在 ChartArea 添加 StripLine
                    StripLine stripLine2 = new StripLine()
                    {
                        Interval = 0, // 不重複
                        StripWidth = 0, // 單一條線
                        IntervalOffset = dTempUpLimit, // 25%水平線所在的 Y 值 CC0320

                        BackColor = System.Drawing.Color.Red,
                        BorderWidth = 1,
                        BorderColor = System.Drawing.Color.Red
                    };
                    this.Chart1.ChartAreas[0].AxisY.StripLines.Add(stripLine2);

                    #region 20250402 偉忠哥說拿掉(這兩條線算是測是用)
                    //StripLine stripLine3 = new StripLine()
                    //{
                    //    Interval = 0, // 不重複
                    //    StripWidth = 0, // 單一條線
                    //    IntervalOffset = daverage, // First average所在的 Y 值 CC0320
                    //    BackColor = System.Drawing.Color.Yellow,
                    //    BorderWidth = 1,
                    //    BorderColor = System.Drawing.Color.Yellow
                    //};
                    //StripLine stripLine4 = new StripLine()
                    //{
                    //    Interval = 0, // 不重複
                    //    StripWidth = 0, // 單一條線
                    //    IntervalOffset = dAveragePoint, // Second average所在的 Y 值 CC0320
                    //    BackColor = System.Drawing.Color.White,
                    //    BorderWidth = 1,
                    //    BorderColor = System.Drawing.Color.White
                    //};
                    //this.Chart1.ChartAreas[0].AxisY.StripLines.Add(stripLine3);
                    //this.Chart1.ChartAreas[0].AxisY.StripLines.Add(stripLine4);
                    #endregion
                }

                this.Chart1.Series["Series2"].Points.AddXY(tp.Result_LockPZT_DAC, this.Chart1.ChartAreas[0].AxisY.Minimum);

                iResult = 0;
            }
            catch (Exception ex)
            {
                WriteTraceLog(ex.ToString());
            }

            return iResult;
        }

        public void ShowCurvePtDGV(List<CurvePoint> mCurvePt)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => ShowCurvePtDGV(mCurvePt)));
                    return;
                }
                dgvVoltage.Columns.Clear();
                dgvVoltage.Rows.Clear();
                dgvVoltage.Columns.Add("Voltage", "Voltage");
                dgvVoltage.Columns[0].Width = 50;
                dgvVoltage.Columns.Add("SlopAvg", "SlopAvg");
                dgvVoltage.Columns[1].Width = 80;
                dgvVoltage.Columns.Add("Mark", "Mark");
                dgvVoltage.Columns[2].Width = 30;
                dgvVoltage.Columns.Add("Percent", "Percent");//CC20250320
                dgvVoltage.Columns[2].Width = 40;
                for (int i = 0; i < mCurvePt.Count(); i++)
                {
                    double dpercent = Math.Abs((dAveragePoint - mCurvePt[i].Slop_Avage) / (Math.Abs(dAveragePoint - dMinPoint)))*100;
                    dgvVoltage.Rows.Add(mCurvePt[i].Voltage, (Math.Round(mCurvePt[i].Slop_Avage, 6, MidpointRounding.AwayFromZero)).ToString("f6"), (mCurvePt[i].bMark) ? "true" : "", dpercent.ToString("f2") + "%");
                }
            }
            catch (Exception ex)
            {
                WriteTraceLog(ex.ToString());
            }
        }

        public void ShowCurvePtDGV2(List<CurvePoint> mCurvePt)//CC0320
        {
            try
            {
                dgvVoltage2.Columns.Clear();
                dgvVoltage2.Rows.Clear();
                dgvVoltage2.Columns.Add(" ", " ");
                dgvVoltage2.Columns[0].Width = 80;
                dgvVoltage2.Columns.Add("SlopAvg", "SlopAvg");
                dgvVoltage2.Columns[1].Width = 80;
                dgvVoltage2.Columns.Add("Percent", "Percent");//CC20250320
                dgvVoltage2.Columns[2].Width = 80;
                double dpercent = Math.Abs((dAveragePoint - daverage) / (Math.Abs(dAveragePoint - dMinPoint))) * 100;
                double dpercent2 = Math.Abs((dAveragePoint - dTempUpLimit) / (Math.Abs(dAveragePoint - dMinPoint))) * 100;
                double dpercent3 = Math.Abs((dAveragePoint - dAveragePoint) / (Math.Abs(dAveragePoint - dMinPoint))) * 100;
                //dgvVoltage2.Rows.Add("First average",(Math.Round(daverage, 6, MidpointRounding.AwayFromZero)).ToString("f6"),  dpercent.ToString("f2") + "%");
                //dgvVoltage2.Rows.Add("Second average",(Math.Round(dAveragePoint, 6, MidpointRounding.AwayFromZero)).ToString("f6"), dpercent3.ToString("f2") + "%");
                dgvVoltage2.Rows.Add(iAverageDownPercentage.ToString() +"% percent",(Math.Round(dTempUpLimit, 6, MidpointRounding.AwayFromZero)).ToString("f6"), dpercent2.ToString("f2") + "%");
                
            }
            catch (Exception ex)
            {
                WriteTraceLog(ex.ToString());
            }
        }


        #region//@FF128-1
        public CurvePoint GetDataFrom_lstAllCurvePoints(double Voltage)
        {
            var Item = m_lstAllCurvePoints.Where(v => v.Voltage == Voltage).FirstOrDefault();
            return Item;
        }


        /// <summary>
        /// 計算Curve Point
        /// </summary>
        /// <returns>0:Pass 1:Error</returns>
        public int Search_Main_CurvePoint()
        {
            int iResult = 1;
            int iKeepCount = 3;
            int iVRange = iVFilterPoint;

            //@FF128-1
            double dblDiffSpec = dTempUpLimit;
            double dblDiffSpecLowerPoint = dVLowestPointSpec;
            //----------
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => iResult = Search_Main_CurvePoint()));
                    return iResult;
                }
                CurvePoint KeyPoint = new CurvePoint();//關鍵DAC值

                List<CurvePoint> KeepDAC = new List<CurvePoint>();

                m_lstCurvePoints.Clear();

                for (int iPoint = 0; iPoint < m_lstAllCurvePoints.Count - 1; iPoint++)
                {
                    CurvePoint CurrentPoint = m_lstAllCurvePoints[iPoint];

                    if (CurrentPoint.Voltage < unUpperDAC)
                        continue;

                    CurvePoint NextPoint = m_lstAllCurvePoints[iPoint + 1];

                    if (KeepDAC.Count == 0)
                    {
                        KeyPoint = CurrentPoint;
                        KeepDAC.Add(CurrentPoint);
                        continue;
                    }

                    if (KeyPoint.Slop_Avage < CurrentPoint.Slop_Avage)
                    {
                        //向上
                        if (KeepDAC[KeepDAC.Count - 1].Slop_Avage <= CurrentPoint.Slop_Avage)
                        {
                            //判斷連續上升
                            KeepDAC.Add(CurrentPoint);
                        }
                        else
                        {
                            //判斷是否反轉向下
                            if (KeepDAC.Count > iKeepCount)
                            {
                                if (NextPoint.Slop_Avage < CurrentPoint.Slop_Avage)
                                {
                                    m_lstCurvePoints.Add(KeepDAC[KeepDAC.Count - 1]);
                                    KeyPoint.Slop_Avage = KeepDAC[KeepDAC.Count - 1].Slop_Avage;
                                    KeyPoint.Voltage = KeepDAC[KeepDAC.Count - 1].Voltage;

                                    KeepDAC.Clear();
                                    KeepDAC.Add(CurrentPoint);
                                }
                            }
                        }
                    }
                    else if (KeyPoint.Slop_Avage > CurrentPoint.Slop_Avage)
                    {
                        //向下
                        if (KeepDAC[KeepDAC.Count - 1].Slop_Avage >= CurrentPoint.Slop_Avage)
                        {
                            //判斷連續下降
                            KeepDAC.Add(CurrentPoint);
                        }
                        else
                        {
                            //判斷是否反轉向上
                            if (KeepDAC.Count > iKeepCount)
                            {
                                if (NextPoint.Slop_Avage > CurrentPoint.Slop_Avage)
                                {
                                    m_lstCurvePoints.Add(KeepDAC[KeepDAC.Count - 1]);
                                    KeyPoint.Slop_Avage = KeepDAC[KeepDAC.Count - 1].Slop_Avage;
                                    KeyPoint.Voltage = KeepDAC[KeepDAC.Count - 1].Voltage;

                                    KeepDAC.Clear();
                                    KeepDAC.Add(CurrentPoint);
                                }
                            }
                        }
                    }
                }

                m_lstTargetCurvePoints.Clear();

                #region//對所有轉折點重新確認是否是V
                List<CurvePoint> Filter = new List<CurvePoint>();
                for (int Index = 1; Index < m_lstCurvePoints.Count; Index++)
                {
                    CurvePoint Pt = m_lstCurvePoints[Index];
                    CurvePoint PrePt = m_lstCurvePoints[Index - 1];
                    double iDiff = PrePt.Slop_Avage - Pt.Slop_Avage;
                    double tCurveV = PrePt.Voltage;
                    CurvePoint Item = GetDataFrom_lstAllCurvePoints(tCurveV);
                    if (Item.Slop_Avage < dblDiffSpec)
                    {
                        int iVIndex = m_lstX_DAC.IndexOf((uint)tCurveV);
                        List<double> LeftSlopData = new List<double>();
                        List<double> RightSlopData = new List<double>();
                        if (iVIndex > iVRange && iVIndex < m_lstX_DAC.Count - iVRange)
                        {
                            for (int i = 1; i <= iVRange; i++)
                            {
                                int iPL = (int)(iVIndex - i);
                                int iPR = (int)(iVIndex + i);
                                CurvePoint ItemL = GetDataFrom_lstAllCurvePoints(m_lstX_DAC[iPL]);
                                CurvePoint ItemR = GetDataFrom_lstAllCurvePoints(m_lstX_DAC[iPR]);
                                LeftSlopData.Add(ItemL.Slop_Avage);
                                RightSlopData.Add(ItemR.Slop_Avage);
                            }
                        }
                        double SlopAvgLeft = Item.Slop_Avage - LeftSlopData.Average();
                        double SlopAvgRight = RightSlopData.Average() - Item.Slop_Avage;
                        if ((SlopAvgLeft < 0 && SlopAvgRight > 0) == false)
                        {
                            Filter.Add(PrePt);
                        }
                    }

                }
                foreach (CurvePoint RemoveItem in Filter)
                {
                    if (m_lstCurvePoints.Contains(RemoveItem) == true)
                    {
                        m_lstCurvePoints.Remove(RemoveItem);
                    }
                }
                #endregion

                

                int SeriesIndex = 3;

                m_iCurveVCount = 0;

                //第1點與最後1點不加入計算
                //2025/02/27 加入第1點的計算(避免過濾掉第一個轉折就是V的那點)
                for (int Index = 1; Index < m_lstCurvePoints.Count; Index++)
                {
                    CurvePoint Pt = m_lstCurvePoints[Index];
                    CurvePoint PrePt = m_lstCurvePoints[Index - 1];

                    double tCurveV = PrePt.Voltage;                             //2025/02/27 改成取PrePt
                    CurvePoint Item = GetDataFrom_lstAllCurvePoints(tCurveV);

                    if (Item.Slop_Avage < dblDiffSpec)
                    {
                        Series NewSeries = new Series("Series" + SeriesIndex.ToString());
                        NewSeries.Color = Color.Blue;
                        NewSeries.Points.AddXY(PrePt.Voltage, this.Chart1.ChartAreas[0].AxisY.Minimum); //2025/02/27 改成取PrePt
                        NewSeries.Points.AddXY(PrePt.Voltage, this.Chart1.ChartAreas[0].AxisY.Maximum); //2025/02/27 改成取PrePt
                        if (this.Chart1.Series.Contains(NewSeries) == false)
                        {
                            this.Chart1.Series["Series" + SeriesIndex.ToString()] = NewSeries;
                        }
                        else
                        {
                            this.Chart1.Series.Add(NewSeries);
                        }
                        m_iCurveVCount++;
                        SeriesIndex++;
                        if (Item.Slop_Avage < dblDiffSpecLowerPoint)
                        {
                            PrePt.bMark = true;//更新內容   //2025/02/27 改成取PrePt
                        }
                    }
                    else
                    {
                        if (m_IsSkipRedLine == false)
                        {
                            Series NewSeries = new Series("Series" + SeriesIndex.ToString());
                            NewSeries.Color = Color.Red;
                            NewSeries.Points.AddXY(PrePt.Voltage, this.Chart1.ChartAreas[0].AxisY.Minimum);
                            NewSeries.Points.AddXY(PrePt.Voltage, this.Chart1.ChartAreas[0].AxisY.Maximum);
                            if (this.Chart1.Series.Contains(NewSeries) == false)
                            {
                                this.Chart1.Series["Series" + SeriesIndex.ToString()] = NewSeries;
                            }
                            else
                            {
                                this.Chart1.Series.Add(NewSeries);
                            }
                            SeriesIndex++;
                        }

                    }
                    m_lstTargetCurvePoints.Add(PrePt);  //2025/02/27 改成取PrePt
                    PrePt = Pt;
                }


                #region//20250402 增加高於30%的V過濾
                 List<CurvePoint> Filter3 = new List<CurvePoint>();
                for (int Index = 0; Index < m_lstTargetCurvePoints.Count; Index++)
                {
                    CurvePoint Pt = m_lstTargetCurvePoints[Index];
                    if (Pt.Slop_Avage > dblDiffSpec)
                    {
                        Filter3.Add(Pt);
                    }
                }
                foreach (CurvePoint RemoveItem in Filter3)
                {
                    if (m_lstTargetCurvePoints.Contains(RemoveItem) == true)
                    {
                        m_lstTargetCurvePoints.Remove(RemoveItem);
                    }
                }
                #endregion


                #region//20250401 增加用前後10點的斜率判斷是否為V
                List<CurvePoint> Filter2 = new List<CurvePoint>();
                for (int Index = 1; Index < m_lstTargetCurvePoints.Count; Index++)
                {
                    CurvePoint Pt = m_lstTargetCurvePoints[Index];

                    bool bIsV = IsValidVShape(Pt);

                    if (!bIsV)
                    {
                        Filter2.Add(Pt);
                    }
                }
                foreach (CurvePoint RemoveItem in Filter2)
                {
                    if (m_lstTargetCurvePoints.Contains(RemoveItem) == true)
                    {
                        m_lstTargetCurvePoints.Remove(RemoveItem);
                    }
                }
                #endregion


                iResult = 0;
            }
            catch (Exception ex)
            {
                WriteTraceLog(ex.ToString());
            }

            return iResult;
        }


        /// <summary>
        /// 透過要檢查的點往前10點和往後10點計算兩條斜率，確認該V點是否為正V
        /// </summary>
        /// <param name="CentorPoint">要檢查的點。</param>
        /// <returns>true | false</returns>
        public bool IsValidVShape(CurvePoint CentorPoint)
        {
            //20250401

            bool bisV = false;

            double dLeftSlopRate = 0;

            double dRightSlopRate = 0;

            int iEndPtIndex = 0;

            int iprePtIndex = 0;

            try
            {
                int iMainIndex = GetIndex_lstAllCurvePoints(CentorPoint.Voltage);

                if (iMainIndex + 10 > m_lstAllCurvePoints.Count - 1)
                {
                    iEndPtIndex = m_lstAllCurvePoints.Count - 1;
                }
                else
                {
                    iEndPtIndex = iMainIndex + 10;
                }

                if (iMainIndex - 10 < 0)
                {
                    iprePtIndex = 0;
                }
                else
                {
                    iprePtIndex = iMainIndex - 10;
                }

                CurvePoint EndPt = m_lstAllCurvePoints[iEndPtIndex];
                CurvePoint prePt = m_lstAllCurvePoints[iprePtIndex];
                CurvePoint LowerPt = m_lstAllCurvePoints[iMainIndex];

                dLeftSlopRate = Math.Round((LowerPt.Slop_Avage - prePt.Slop_Avage) / (LowerPt.Voltage - prePt.Voltage), 10, MidpointRounding.AwayFromZero);
                dRightSlopRate = Math.Round((LowerPt.Slop_Avage - EndPt.Slop_Avage) / (LowerPt.Voltage - EndPt.Voltage), 10, MidpointRounding.AwayFromZero);

                if (dLeftSlopRate < 0 && dRightSlopRate > 0)
                {
                    bisV = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return bisV;
        }

        /// <summary>
        /// 計算Curve Point
        /// </summary>
        /// <returns>0:Pass 1:Error</returns>
        public int Search_Main_CurvePoint_OLD()
        {
            int iResult = 1;
            int iKeepCount = 3;
            int iVRange = iVFilterPoint;

            //@FF128-1
            double dblDiffSpec = dTempUpLimit;
            double dblDiffSpecLowerPoint = dVLowestPointSpec;
            //----------
            try
            {
                CurvePoint KeyPoint = new CurvePoint();//關鍵DAC值

                List<CurvePoint> KeepDAC = new List<CurvePoint>();

                m_lstCurvePoints.Clear();

                for (int iPoint = 0; iPoint < m_lstAllCurvePoints.Count - 1; iPoint++)
                {
                    CurvePoint CurrentPoint = m_lstAllCurvePoints[iPoint];

                    if (CurrentPoint.Voltage < unUpperDAC)
                        continue;

                    CurvePoint NextPoint = m_lstAllCurvePoints[iPoint + 1];

                    if (KeepDAC.Count == 0)
                    {
                        KeyPoint = CurrentPoint;
                        KeepDAC.Add(CurrentPoint);
                        continue;
                    }

                    if (KeyPoint.Slop_Avage < CurrentPoint.Slop_Avage)
                    {
                        //向上
                        if (KeepDAC[KeepDAC.Count - 1].Slop_Avage <= CurrentPoint.Slop_Avage)
                        {
                            //判斷連續上升
                            KeepDAC.Add(CurrentPoint);
                        }
                        else
                        {
                            //判斷是否反轉向下
                            if (KeepDAC.Count > iKeepCount)
                            {
                                if (NextPoint.Slop_Avage < CurrentPoint.Slop_Avage)
                                {
                                    m_lstCurvePoints.Add(KeepDAC[KeepDAC.Count - 1]);
                                    KeyPoint.Slop_Avage = KeepDAC[KeepDAC.Count - 1].Slop_Avage;
                                    KeyPoint.Voltage = KeepDAC[KeepDAC.Count - 1].Voltage;

                                    KeepDAC.Clear();
                                    KeepDAC.Add(CurrentPoint);
                                }
                            }
                        }
                    }
                    else if (KeyPoint.Slop_Avage > CurrentPoint.Slop_Avage)
                    {
                        //向下
                        if (KeepDAC[KeepDAC.Count - 1].Slop_Avage >= CurrentPoint.Slop_Avage)
                        {
                            //判斷連續下降
                            KeepDAC.Add(CurrentPoint);
                        }
                        else
                        {
                            //判斷是否反轉向上
                            if (KeepDAC.Count > iKeepCount)
                            {
                                if (NextPoint.Slop_Avage > CurrentPoint.Slop_Avage)
                                {
                                    m_lstCurvePoints.Add(KeepDAC[KeepDAC.Count - 1]);
                                    KeyPoint.Slop_Avage = KeepDAC[KeepDAC.Count - 1].Slop_Avage;
                                    KeyPoint.Voltage = KeepDAC[KeepDAC.Count - 1].Voltage;

                                    KeepDAC.Clear();
                                    KeepDAC.Add(CurrentPoint);
                                }
                            }
                        }
                    }
                }

                m_lstTargetCurvePoints.Clear();


                #region//對所有轉折點重新確認是否是V
                List<CurvePoint> Filter = new List<CurvePoint>();
                for (int Index = 1; Index < m_lstCurvePoints.Count; Index++)
                {
                    CurvePoint Pt = m_lstCurvePoints[Index];
                    CurvePoint PrePt = m_lstCurvePoints[Index - 1];
                    double iDiff = PrePt.Slop_Avage - Pt.Slop_Avage;
                    double tCurveV = PrePt.Voltage;
                    CurvePoint Item = GetDataFrom_lstAllCurvePoints(tCurveV);
                    if (Item.Slop_Avage < dblDiffSpec)
                    {
                        int iVIndex = m_lstX_DAC.IndexOf((uint)tCurveV);
                        List<double> LeftSlopData = new List<double>();
                        List<double> RightSlopData = new List<double>();
                        if (iVIndex > iVRange && iVIndex < m_lstX_DAC.Count - iVRange)
                        {
                            for (int i = 1; i <= iVRange; i++)
                            {
                                int iPL = (int)(iVIndex - i);
                                int iPR = (int)(iVIndex + i);
                                CurvePoint ItemL = GetDataFrom_lstAllCurvePoints(m_lstX_DAC[iPL]);
                                CurvePoint ItemR = GetDataFrom_lstAllCurvePoints(m_lstX_DAC[iPR]);
                                LeftSlopData.Add(ItemL.Slop_Avage);
                                RightSlopData.Add(ItemR.Slop_Avage);
                            }
                        }
                        double SlopAvgLeft = 0;
                        double SlopAvgRight = 0;

                        if (LeftSlopData.Count > 0)
                        {
                            SlopAvgLeft = LeftSlopData.Average() - Item.Slop_Avage;
                        }
                        if (RightSlopData.Count > 0)
                        {
                            SlopAvgRight = RightSlopData.Average() - Item.Slop_Avage;
                        }

                        if ((SlopAvgLeft > 0 && SlopAvgRight > 0) == false)
                        {
                            Filter.Add(PrePt);
                        }
                    }

                }
                foreach (CurvePoint RemoveItem in Filter)
                {
                    if (m_lstCurvePoints.Contains(RemoveItem) == true)
                    {
                        m_lstCurvePoints.Remove(RemoveItem);
                    }
                }
                #endregion

                int SeriesIndex = 3;

                m_iCurveVCount = 0;

                //第1點與最後1點不加入計算
                for (int Index = 1; Index < m_lstCurvePoints.Count; Index++)
                {
                    CurvePoint Pt = m_lstCurvePoints[Index];
                    CurvePoint PrePt = m_lstCurvePoints[Index - 1];

                    double tCurveV = Pt.Voltage;
                    CurvePoint Item = GetDataFrom_lstAllCurvePoints(tCurveV);

                    if (Item.Slop_Avage < dblDiffSpec)
                    {
                        Series NewSeries = new Series("Series" + SeriesIndex.ToString());
                        NewSeries.Color = Color.Blue;
                        NewSeries.Points.AddXY(Pt.Voltage, this.Chart1.ChartAreas[0].AxisY.Minimum);
                        NewSeries.Points.AddXY(Pt.Voltage, this.Chart1.ChartAreas[0].AxisY.Maximum);
                        if (this.Chart1.Series.Contains(NewSeries) == false)
                        {
                            this.Chart1.Series["Series" + SeriesIndex.ToString()] = NewSeries;
                        }
                        else
                        {
                            this.Chart1.Series.Add(NewSeries);
                        }
                        m_iCurveVCount++;
                        SeriesIndex++;
                        if (Item.Slop_Avage < dblDiffSpecLowerPoint)
                        {
                            Pt.bMark = true;//更新內容
                        }
                    }
                    else
                    {
                        if (m_IsSkipRedLine == false)
                        {
                            Series NewSeries = new Series("Series" + SeriesIndex.ToString());
                            NewSeries.Color = Color.Red;
                            NewSeries.Points.AddXY(PrePt.Voltage, this.Chart1.ChartAreas[0].AxisY.Minimum);
                            NewSeries.Points.AddXY(PrePt.Voltage, this.Chart1.ChartAreas[0].AxisY.Maximum);
                            if (this.Chart1.Series.Contains(NewSeries) == false)
                            {
                                this.Chart1.Series["Series" + SeriesIndex.ToString()] = NewSeries;
                            }
                            else
                            {
                                this.Chart1.Series.Add(NewSeries);
                            }
                            SeriesIndex++;
                        }

                    }
                    m_lstTargetCurvePoints.Add(Pt);
                    PrePt = Pt;
                }

                iResult = 0;
            }
            catch (Exception ex)
            {
                WriteTraceLog(ex.ToString());
            }

            return iResult;
        }
        //-------------------
        #endregion
        private void SaveImagePNG(string FileName)
        {
            try
            {
                string sMainFileName = "D:\\ValveChart";
                string sDayFileName = DateTime.Now.ToString("yyyyMMdd");
                DirectoryInfo DirMainFolder = new DirectoryInfo(sMainFileName);

                DirectoryInfo DirDayFolder = new DirectoryInfo(sMainFileName + "\\" + sDayFileName);
                if (DirMainFolder.Exists == false)
                {
                    DirMainFolder.Create();
                }

                if (DirDayFolder.Exists == false)
                {
                    DirDayFolder.Create();
                }
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => this.Chart1.SaveImage(sMainFileName + "\\" + sDayFileName + "\\" + FileName + ".png", ChartImageFormat.Png)));
                }
                else
                {
                    this.Chart1.SaveImage(sMainFileName + "\\" + sDayFileName + "\\" + FileName + ".png", ChartImageFormat.Png);
                }
            }
            catch (Exception ex)
            {
                //註解掉
                //PublicDeclare.g_Main.WriteTraceLog(ex.ToString());
            }
        }

        private void SaveData(string FileName)
        {
            try
            {
                string sMainFileName = "D:\\ValveChart";
                string sDayFileName = DateTime.Now.ToString("yyyyMMdd");
                DirectoryInfo DirMainFolder = new DirectoryInfo(sMainFileName);

                DirectoryInfo DirDayFolder = new DirectoryInfo(sMainFileName + "\\" + sDayFileName);
                if (DirMainFolder.Exists == false)
                {
                    DirMainFolder.Create();
                }

                if (DirDayFolder.Exists == false)
                {
                    DirDayFolder.Create();
                }

                StreamWriter sw = new StreamWriter(sMainFileName + "\\" + sDayFileName + "\\" + FileName + ".csv", false);
                sw.Write("DAC");
                sw.Write(",");
                sw.Write("mV");
                sw.Write(sw.NewLine);
                int Buffer_Size = m_lstX_DAC.Count();


                for (int i = 0; i < Buffer_Size; i++)
                {
                    sw.Write(m_lstX_DAC[i]);
                    sw.Write(",");
                    sw.Write(m_lstY_mV[i]);
                    sw.Write(sw.NewLine);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                WriteTraceLog(ex.ToString());
            }
        }

        #endregion

        #region ===========================Public===========================

        public static ucPZTCalibChart GetSingleton()
        {
            if (m_Singleton == null || m_Singleton.bIsAdnormalClose)//@FF127 避免強制關閉圖形Exception
            {
                m_Singleton = new ucPZTCalibChart();
            }

            return m_Singleton;
        }

        public void WriteTraceLog(string strDesc)
        {
            try
            {
                clsLog.Log("PZTChart", strDesc);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        private void btn_ReadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            m_lstX_DAC.Clear();
            m_lstY_mV.Clear();

            m_lstAllCurvePoints.Clear();

            m_lstCurvePoints.Clear();

            m_lstTargetCurvePoints.Clear();

            m_iCurveVCount = 0;//default 0

            openFileDialog1.Filter = "CSV|*.csv";
            openFileDialog1.Title = "Select CSV File";
            this.Text = "";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                string[] aryline;
                string strline;
                StreamReader txtfile = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.Default);
                strline = txtfile.ReadLine();
                strline = txtfile.ReadLine();//Q1

                while ((strline = txtfile.ReadLine()) != null)
                {
                    aryline = strline.Split(new char[] { ',' });
                    m_lstX_DAC.Add(Convert.ToUInt32(aryline[0]));
                    m_lstY_mV.Add(Convert.ToUInt32(aryline[1]));
                }
                txtfile.Close();
            }
            this.Text = openFileDialog1.FileName;
            //MessageBox.Show("讀檔成功!!");
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            DrewChart(m_lstX_DAC, m_lstY_mV);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search_Main_CurvePoint();

            ShowCurvePtDGV(m_lstTargetCurvePoints);
            ShowCurvePtDGV2(m_lstTargetCurvePoints);
        }

        private void Chart1_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                int Index = e.HitTestResult.PointIndex;
                DataPoint dpPoint = e.HitTestResult.Series.Points[0];
                CurvePoint NowPt = m_lstCurvePoints.Find(o => o.Voltage == dpPoint.XValue);
                double dSlop_Avg = double.Parse(NowPt.Slop_Avage.ToString());
                e.Text = dpPoint.XValue.ToString() + "," + dSlop_Avg.ToString("f6");
                lblGetValue.Text = string.Format("({0},{1})", dpPoint.XValue.ToString(), dSlop_Avg.ToString("f6"));
            }
        }

        private void btnCalSlopRate_Click(object sender, EventArgs e)
        {
            CalibrateSlopRate();
        }

        /// <summary>
        /// 計算最後結果
        /// </summary>
        /// <returns>Result= 0:Pass 1:Error 2:Deviation Fail 3:Multi-V Curve 4:最低點異常</returns>
        public int CalibrateSlopRate()
        {
            double dblLeftSlopRate = 0;
            double dblRightSlopRate = 0;

            int iResult = 1;

            decimal dSlopDeviation = -99;
            string sResult = "FAIL";

            try
            {
                if (m_iCurveVCount == 1)
                {
                    dSlopDeviation = Math.Abs(CaculaterSlopRate(m_lstTargetCurvePoints, ref dblLeftSlopRate, ref dblRightSlopRate));

                    //A488
                    sResult = (dSlopDeviation < dSlopSpec) ? "PASS" : "斜率異常";
                    iResult = (dSlopDeviation < dSlopSpec) ? 0 : 2;
                    //sResult = (dSlopDeviation < 30) ? "PASS" : "FAIL";
                    //iResult = (dSlopDeviation < 30) ? 0 : 2;
                    //-----------
                }
                else if (m_iCurveVCount > 1)    //2025/02/27 修改異常結果顯示
                {
                    sResult = "曲線異常";
                    iResult = 3;
                    //MessageBox.Show("Occur Mult-V");
                }
                else if (m_iCurveVCount < 1)    //2025/02/27 修改異常結果顯示
                {
                    sResult = "找不到V";
                    iResult = 5;
                }
                //@FF128
                double SlopMin = dVLowestPointSpec;
                if (m_lstAllCurvePoints.Min(p => p.Slop_Avage) >= SlopMin)
                {
                    sResult = "最低點異常";
                    iResult = 4;
                }
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => this.lblResult.Text = sResult));
                }
                else
                {
                    this.lblResult.Text = sResult;
                }

                if (m_lstCurvePoints.Count > 0)
                {
                    string sDateTime = DateTime.Now.ToString("yyyy-MM-dd_hhmmss");
                    SaveData(sDateTime);
                    SaveImagePNG(string.Format("{0}_{1}_Slop{2}", sDateTime, sResult, dSlopDeviation));
                }

              
            }
            catch (Exception ex)
            {
                WriteTraceLog(ex.ToString());
            }

            return iResult;
        }

        /// <summary>
        /// 計算最後結果
        /// </summary>
        /// <returns>Result= 0:Pass 1:Error 2:Deviation Fail 3:Multi-V Curve 4:最低點異常</returns>
        public int CalibrateSlopRate_OLD()
        {
            double dblLeftSlopRate = 0;
            double dblRightSlopRate = 0;

            int iResult = 1;

            decimal dSlopDeviation = -99;
            string sResult = "FAIL";

            try
            {
                if (m_iCurveVCount == 1)
                {
                    dSlopDeviation = Math.Abs(CaculaterSlopRate(m_lstTargetCurvePoints, ref dblLeftSlopRate, ref dblRightSlopRate));

                    //A488
                    sResult = (dSlopDeviation < dSlopSpec) ? "PASS" : "斜率異常";
                    iResult = (dSlopDeviation < dSlopSpec) ? 0 : 2;
                    //sResult = (dSlopDeviation < 30) ? "PASS" : "FAIL";
                    //iResult = (dSlopDeviation < 30) ? 0 : 2;
                    //-----------
                }
                else if (m_iCurveVCount > 1)
                {
                    sResult = "曲線異常";
                    iResult = 3;
                }
                else if (m_iCurveVCount < 1)
                {
                    sResult = "找不到V";
                    iResult = 5;
                    //MessageBox.Show("Occur Mult-V");
                }

                this.lblResult.Text = sResult;

                if (m_lstCurvePoints.Count > 0)
                {
                    string sDateTime = DateTime.Now.ToString("yyyy-MM-dd_hhmmss");
                    SaveData(sDateTime);
                    SaveImagePNG(string.Format("{0}_{1}_Slop{2}", sDateTime, sResult, dSlopDeviation));
                }

                //@FF128
                double value = dVLowestPointSpec;
                double SlopMin = (value == -1) ? -0.0008 : value;
                if (m_lstAllCurvePoints.Min(p => p.Slop_Avage) >= Math.Abs(SlopMin))
                {
                    sResult = "最低點異常";
                    iResult = 4;
                }
            }
            catch (Exception ex)
            {
                WriteTraceLog(ex.ToString());
            }

            return iResult;
        }

        #region//@FF128-1
        private decimal CaculaterSlopRate(List<CurvePoint> mCurvePt, ref double dblLeftRate, ref double dblRightRate)
        {
            decimal dSlopDeviation = -99;

            decimal dLeftSlopRate = 0;

            decimal dRightSlopRate = 0;

            double dLowestPoint = 0; //A053-b

            try
            {
                List<CurvePoint> lstMainPt = new List<CurvePoint>();

                foreach (CurvePoint mPt in mCurvePt)
                {
                    if (mPt.bMark == true)
                    {
                        lstMainPt.Add(mPt);
                    }
                }

                //Filter次數
                int iFilterCount = 0;

                foreach (CurvePoint SlopPt in lstMainPt)
                {
                    //int iMainIndex = m_lstAllCurvePoints.FindIndex(o => o.Voltage == SlopPt.Voltage);
                    int iMainIndex = GetIndex_lstAllCurvePoints(SlopPt.Voltage);

                    #region//往左找上升前第一點,取五條平均
                    CurvePoint EndPt = m_lstAllCurvePoints[iMainIndex];
                    CurvePoint prePt = m_lstAllCurvePoints[iMainIndex];
                    CurvePoint LowerPt = m_lstAllCurvePoints[iMainIndex];
                    dLowestPoint = LowerPt.Slop_Avage; //A053-b
                    //CurvePoint prePt = m_lstAllCurvePoints[0];
                    int ipreContinueUP = 0;
                    int iEndContinueUP = 0;
                    for (int iLeftNo = 4; iLeftNo <= 10; iLeftNo++)
                    {
                        if (iMainIndex - iLeftNo > 0)
                        {
                            CurvePoint Pt1 = m_lstAllCurvePoints[iMainIndex - iLeftNo + 1];
                            CurvePoint Pt2 = m_lstAllCurvePoints[iMainIndex - iLeftNo];
                            if (Pt1.Slop_Avage - Pt2.Slop_Avage <= 0)
                            {
                                //持續往上
                                ipreContinueUP++;
                                prePt = Pt2;
                            }
                            else
                            {
                                //往下
                                //break;
                            }
                        }
                    }
                    for (int iLeftNo = 4; iLeftNo <= 10; iLeftNo++)
                    {
                        if (iMainIndex + iLeftNo > 0)
                        {
                            CurvePoint Pt1 = m_lstAllCurvePoints[iMainIndex + iLeftNo - 1];
                            CurvePoint Pt2 = m_lstAllCurvePoints[iMainIndex + iLeftNo];
                            if (LowerPt.Slop_Avage - Pt2.Slop_Avage <= 0)
                            {
                                //持續往上
                                iEndContinueUP++;
                                EndPt = Pt2;
                            }
                            else
                            {
                                //往下
                                //break;
                            }
                        }
                    }
                    #endregion
                    if (ipreContinueUP >= 5 && iEndContinueUP >= 5)
                    {
                        #region //10條線曲平均 (中心往左10條線)
                        decimal dTanLine = 0;
                        for (int i = 4; i < 14; i++)
                        {
                            CurvePoint StartPt1 = m_lstAllCurvePoints[iMainIndex - i];
                            decimal NowSlopt = Math.Abs((decimal)Math.Round((LowerPt.Slop_Avage - StartPt1.Slop_Avage) / (LowerPt.Voltage - StartPt1.Voltage), 10, MidpointRounding.AwayFromZero));
                            dTanLine += NowSlopt;
                        }
                        dLeftSlopRate = Math.Round(dTanLine / 10, 10, MidpointRounding.AwayFromZero);
                        #endregion

                        #region //10條線曲平均 (中心往右10條線)
                        dTanLine = 0;
                        for (int i = 4; i < 14; i++)
                        {
                            CurvePoint StartPt1 = m_lstAllCurvePoints[iMainIndex + i];
                            decimal NowSlopt = Math.Abs((decimal)Math.Round((LowerPt.Slop_Avage - StartPt1.Slop_Avage) / (LowerPt.Voltage - StartPt1.Voltage), 10, MidpointRounding.AwayFromZero));
                            dTanLine += NowSlopt;
                        }
                        dRightSlopRate = Math.Round(dTanLine / 10, 10, MidpointRounding.AwayFromZero);
                        #endregion

                    }
                    else
                    {
                        dLeftSlopRate = Math.Abs((decimal)Math.Round((LowerPt.Slop_Avage - prePt.Slop_Avage) / (LowerPt.Voltage - prePt.Voltage), 10, MidpointRounding.AwayFromZero));
                        dRightSlopRate = Math.Abs((decimal)Math.Round((LowerPt.Slop_Avage - EndPt.Slop_Avage) / (LowerPt.Voltage - EndPt.Voltage), 10, MidpointRounding.AwayFromZero));
                        //dLeftSlopRate = Math.Round(NowSloptLeft / 10, 10, MidpointRounding.AwayFromZero);
                        //dRightSlopRate = Math.Round(dTanLine / 10, 10, MidpointRounding.AwayFromZero);
                    }

                    string strDrawV = "Series_DrawV";
                    Series NewSeries = new Series(strDrawV);
                    NewSeries.ChartType = SeriesChartType.Line;
                    NewSeries.Color = Color.Red;
                    //NewSeries.Points.AddXY(m_lstAllCurvePoints[0].Voltage, prePt.Slop_Avage);
                    NewSeries.Points.AddXY(prePt.Voltage, prePt.Slop_Avage);
                    NewSeries.Points.AddXY(LowerPt.Voltage, LowerPt.Slop_Avage);
                    NewSeries.Points.AddXY(EndPt.Voltage, EndPt.Slop_Avage);
                    //NewSeries.Points.AddXY(m_lstAllCurvePoints[m_lstAllCurvePoints.Count-1].Voltage, EndPt.Slop_Avage);
                    if (this.Chart1.Series.Contains(NewSeries) == false)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() => this.Chart1.Series[strDrawV] = NewSeries));
                        }
                        else
                        {
                            this.Chart1.Series[strDrawV] = NewSeries;
                        }
                    }
                    else
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() => this.Chart1.Series.Add(NewSeries)));
                        }
                        else
                        {
                            this.Chart1.Series.Add(NewSeries);
                        }
                    }
                }


                //計算誤差值 (%)
                dSlopDeviation = Math.Round((dLeftSlopRate / dRightSlopRate) - 1, 3, MidpointRounding.AwayFromZero) * 100;

                decimal dAvg = (dLeftSlopRate + dRightSlopRate) / 2;
                decimal dSlopDeviationLeft = Math.Round((dLeftSlopRate / dAvg) - 1, 3, MidpointRounding.AwayFromZero) * 100;
                decimal dSlopDeviationRight = Math.Round((dLeftSlopRate / dAvg) - 1, 3, MidpointRounding.AwayFromZero) * 100;

                dSlopDeviation = dSlopDeviationLeft;
                if (dSlopDeviation < dSlopDeviationRight)
                {
                    dSlopDeviation = dSlopDeviationRight;
                }
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => this.txtLeftSlopRate.Text = dLeftSlopRate.ToString("f8")));
                }
                else
                {
                    this.txtLeftSlopRate.Text = dLeftSlopRate.ToString("f8");
                }
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => this.txtRightSlopRate.Text = dRightSlopRate.ToString("f8")));
                }
                else
                {
                    this.txtRightSlopRate.Text = dRightSlopRate.ToString("f8");
                }
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => this.txtSlopDeviation.Text = dSlopDeviation.ToString()));
                }
                else
                {
                    this.txtSlopDeviation.Text = dSlopDeviation.ToString();
                }
                //A053-b
                //double iSlopSpec = ucParameter.GetValueDouble(clsEnum.enuPmtName.Sys_Valve_SlopSPEC);
                //clsLog.Log(clsEnum.enuLogName.CalibrationLog,
                //    "<閉鎖校正>本次閉鎖曲線最低點:" + dLowestPoint.ToString() +
                //    ", 左斜率:" + dLeftSlopRate.ToString() +
                //    ", 右斜率:" + dRightSlopRate.ToString() +
                //    ", 斜率相似度差%:" + Math.Abs(dSlopDeviation).ToString()
                //    + ", 設定卡控斜率差:" + iSlopSpec.ToString()
                //    );

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }

            return dSlopDeviation;
        }

        public int GetIndex_lstAllCurvePoints(double Voltage)
        {
            int Item = m_lstAllCurvePoints.FindIndex(o => o.Voltage == Voltage);
            return Item;
        }
        //--------------
        #endregion
        private void chkRedLineSkip_CheckedChanged(object sender, EventArgs e)
        {
            m_IsSkipRedLine = chkRedLineSkip.Checked;

            DrewChart(m_lstX_DAC, m_lstY_mV);

            Search_Main_CurvePoint();
        }

        private void button_X_Click(object sender, EventArgs e)
        {
            ucPZTCalibChart.GetSingleton().Hide();
        }


        public enum enuCurveResult
        {
            Pass = 0,
            UndefineError = 1,
            SlopError = 2,
            DoubleV = 3,
            LowerPointFail = 4,

        }

        public enuCurveResult Chk_Curve_succse(int p_iSlopSpec = 35, int p_iVSpecPrecentage = 25, double p_dLowerPoint = -0.0008)
        {
            dSlopSpec = p_iSlopSpec;
            iAverageDownPercentage = p_iVSpecPrecentage;
            dVLowestPointSpec = p_dLowerPoint;
            enuCurveResult eResult = enuCurveResult.UndefineError;
            try
            {
                if (this.m_lstX_DAC.Count == 0)
                {
                    eResult = enuCurveResult.UndefineError;
                    return eResult;
                }
                if (this.DrewChart(this.m_lstX_DAC,
                    this.m_lstY_mV) == 0)
                {
                    if (this.Search_Main_CurvePoint() == 0)
                    {
                        this.ShowCurvePtDGV(this.m_lstTargetCurvePoints);
                        int iRet = this.CalibrateSlopRate();
                        if (iRet == 0)
                        {
                            eResult = enuCurveResult.Pass;
                        }
                        else if (iRet == 2)
                        {
                            eResult = enuCurveResult.SlopError;
                        }
                        else if (iRet == 3)
                        {
                            eResult = enuCurveResult.DoubleV;
                        }
                        else
                        {
                            eResult = enuCurveResult.LowerPointFail;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }

            return eResult;
        }
    }

    public struct CurvePoint
    {
        public double Voltage { get; set; }

        public double Slop_Avage { get; set; }

        public bool bMark { get; set; }
    }
}

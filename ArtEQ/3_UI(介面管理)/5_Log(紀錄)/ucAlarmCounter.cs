using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtEQ
{
    public partial class ucAlarmCounter : ArtCommonLib.ucBaseUserControl
    {

        #region //=====================  區域變數設置 =====================

        /// <summary> 靜態物件- 預防重複宣告 </summary>
        private static ucAlarmCounter m_Singleton;
        private static object m_objLock = new object();


        /// <summary> Log 資料夾路徑 </summary>
        private string Directory_AlarmCounter = "D:\\Log\\AlarmCounterData";
        /// <summary> Log 文件路徑 - 依日期 / 依LotID </summary>
        private string Path_AlarmCounter = "";

        /// <summary> True = 資料有變更,需要更新介面 </summary>
        private bool IsNeedUpdateMessage = false;

        private Dictionary<string, AlarmTimeCount> mAlarmTimeCount = new Dictionary<string, AlarmTimeCount>();

        private ucThreadLog threadLog = new ucThreadLog();
        #endregion

        #region //=====================  Enum變數設置 =====================

        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 取得唯一物件，避免重覆設置 </summary> <returns>回傳物件</returns>
        static public ucAlarmCounter GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new ucAlarmCounter();
                    }
                }
            }
            return m_Singleton;
        }

        /// <summary> 物件建立請利用 GetSingleton() ，除非特殊需求 </summary>
        private ucAlarmCounter()
        {
            InitializeComponent();
            if ( ArtSystem.clsArtSystem.bIsProgramOpen == false)
            { return; }
            ucParameter.Add(this);
            this.TimerInterval = 500;
            UpdateControls();
            monthCalendarToday.LostFocus += new EventHandler(monthCalendarToday_LostFocus);
            txt_CopyLogTargetPath.Text = "D:\\LogCopy";
            txt_SelectDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (System.IO.Directory.Exists(Directory_AlarmCounter) == false)
            {
                System.IO.Directory.CreateDirectory(Directory_AlarmCounter);
            }
            Initial();
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            txt_SelectDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary> 自動更新介面參數 </summary>
        protected override void ReflashTimerFunc()
        {
            try
            {
                if (IsNeedUpdateMessage == true)
                {
                    IsNeedUpdateMessage = false;
                    UpdateMessage();
                }
            }
            catch (Exception e)
            {
                clsLog.Log(clsEnum.enuLogName.SystemLog, this.Name + " -> [Catch Error] ReflashTimerFunc() , " + e.Message);//@1.0.0.51-16@
            }
        }
        private void ucAlarmCounter_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                UpdateControls();
            }
            else
            {
            }

        }

        #endregion

        #region //===================== public 函式設置 =====================

        public void Initial()
        {
            Path_AlarmCounter = Directory_AlarmCounter;
            try
            {
                if (System.IO.Directory.Exists(Path_AlarmCounter) == false)
                {
                    System.IO.Directory.CreateDirectory(Path_AlarmCounter);
                }
                Path_AlarmCounter += "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "-AlarmCounterLog.log";
                if (System.IO.File.Exists(Path_AlarmCounter) == false)
                {
                    System.IO.File.WriteAllLines(Path_AlarmCounter, new string[] { });
                }
                string[] ExistFiles = System.IO.Directory.GetFiles(Directory_AlarmCounter);
                ListAlarmCounterFiles.Items.Clear();
                foreach (string sExistFile in ExistFiles)
                {
                    ListAlarmCounterFiles.Items.Add(sExistFile);
                }
                ListAlarmCounterFiles.SelectedItem = Path_AlarmCounter;
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        public int ReportProc(clsObjAlarm p_AlarmObject, string LogID = "")
        {
            if (this.InvokeRequired == true)
            {
                this.BeginInvoke(new Action(() =>
                {
                    ReportProc(p_AlarmObject, LogID);
                }));
            }
            else
            {

                try
                {
                    bool bFound = false;
                    int iAlarmIndex = 0;
                    string[] sMessage;
                    string[] sNewMessage;
                    string sAlarmCode = p_AlarmObject.AlarmCode;
                    string sPath = Directory_AlarmCounter + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + LogID + "-AlarmCounterLog.log";
                    Dictionary<string, int> m_dctAlarmCounter = new Dictionary<string, int>();
                    m_dctAlarmCounter.Clear();
                    StartTimeCounter(sAlarmCode);
                    txtLastOccurAlarm.Text = sAlarmCode;
                    if (Path_AlarmCounter != sPath)
                    {
                        Path_AlarmCounter = sPath;
                    }
                    if (System.IO.File.Exists(Path_AlarmCounter) == false)
                    {
                        System.IO.File.WriteAllLines(Path_AlarmCounter, new string[] { });
                        ListAlarmCounterFiles.Items.Add(Path_AlarmCounter);
                    }
                    sMessage = System.IO.File.ReadAllLines(Path_AlarmCounter);
                    sNewMessage = new string[sMessage.Length + 1];
                    for (int i = 0; i < sMessage.Length; i++)
                    {
                        string sKey = sMessage[i].Split('=')[0];
                        int iCounter = Convert.ToInt32(sMessage[i].Split('=')[1]);
                        double SpendTime = Convert.ToDouble(sMessage[i].Split('=')[3]);
                        double SpendTimeLast = Convert.ToDouble(sMessage[i].Split('=')[4]);
                        if (sKey == sAlarmCode)
                        {
                            bFound = true;
                            iAlarmIndex = i;
                            sMessage[i] = sKey + "=" + iCounter.ToString() + "=" + p_AlarmObject.Message + "=" + SpendTime.ToString("F3") + "=" + SpendTimeLast.ToString("F3");
                        }
                        sNewMessage[i] = sMessage[i];
                    }
                    string TempMessage = "";
                    if (bFound == false)
                    {
                        sNewMessage[sNewMessage.Length - 1] = sAlarmCode + "=0" + "=" + p_AlarmObject.Message + "=0" + "=0";
                        System.IO.File.WriteAllLines(Path_AlarmCounter, sNewMessage);
                    }
                    else
                    {
                        for (int i = iAlarmIndex; i > 0; i--)
                        {
                            int CompareCounter1 = Convert.ToInt32(sMessage[i - 1].Split('=')[1]);
                            int CompareCounter2 = Convert.ToInt32(sMessage[i].Split('=')[1]);
                            if (CompareCounter2 > CompareCounter1)
                            {
                                TempMessage = sMessage[i - 1];
                                sMessage[i - 1] = sMessage[i];
                                sMessage[i] = TempMessage;
                            }
                            else
                            {
                                break;
                            }
                        }
                        System.IO.File.WriteAllLines(Path_AlarmCounter, sMessage);
                    }
                    if (ListAlarmCounterFiles.Items.Contains(Path_AlarmCounter) == false)
                    {
                        ListAlarmCounterFiles.Items.Add(Path_AlarmCounter);
                    }
                    IsNeedUpdateMessage = true;
                }
                catch (Exception ex)
                {
                    clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
                }
            }
            return 0;
        }

        public int ClrAlarmClick(clsObjAlarm p_AlarmObject)
        {
            string sAlarmCode = p_AlarmObject.AlarmCode;
            try
            {
                SetTimeCounter(sAlarmCode);
                double SpendTime = mAlarmTimeCount[sAlarmCode].TimeCounterCycle;
                ClrTimeCounter(sAlarmCode);
                string[] sMessage = System.IO.File.ReadAllLines(Path_AlarmCounter);
                for (int i = 0; i < sMessage.Length; i++)
                {
                    string sKey = sMessage[i].Split('=')[0];
                    if (sKey == sAlarmCode)
                    {
                        int iCounter = Convert.ToInt32(sMessage[i].Split('=')[1]);
                        double pSpendTime = Convert.ToDouble(sMessage[i].Split('=')[3]);
                        pSpendTime = pSpendTime + SpendTime;
                        iCounter++;
                        sMessage[i] = sKey + "=" + iCounter.ToString() + "=" + p_AlarmObject.Message + "=" + pSpendTime.ToString("F3") + "=" + SpendTime.ToString("F3");
                        System.IO.File.WriteAllLines(Path_AlarmCounter, sMessage);
                    }
                }
                IsNeedUpdateMessage = true;
                return 0;
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
                ClrTimeCounter(sAlarmCode);
                return -1;
            }
        }

        #endregion

        #region //===================== private 函式設置 =====================

        /// <summary> 更新介面訊息 </summary>
        private void UpdateMessage()
        {
            try
            {
                int Count = 0;
                double SpendTime = 0;
                double SpendTimeLast = 0;
                double ActiveCount = 0;
                dataGridView1.Rows.Clear();
                string[] sAlarmCounter = System.IO.File.ReadAllLines(ListAlarmCounterFiles.SelectedItem.ToString());
                if (sAlarmCounter.Length > 0)
                {
                    dataGridView1.Rows.Add(sAlarmCounter.Length);
                    foreach (string sAlarmMessage in sAlarmCounter)
                    {
                        dataGridView1.Rows[Count].Cells[0].Value = sAlarmMessage.Split('=')[0];
                        dataGridView1.Rows[Count].Cells[2].Value = sAlarmMessage.Split('=')[1];
                        dataGridView1.Rows[Count].Cells[1].Value = sAlarmMessage.Split('=')[2];
                        SpendTime = Convert.ToDouble(sAlarmMessage.Split('=')[3]);
                        SpendTimeLast = Convert.ToDouble(sAlarmMessage.Split('=')[4]);
                        ActiveCount = Convert.ToDouble(sAlarmMessage.Split('=')[1]);
                        SpendTime = SpendTime / ActiveCount;
                        dataGridView1.Rows[Count].Cells[3].Value = SpendTimeLast.ToString("F3");
                        dataGridView1.Rows[Count].Cells[4].Value = SpendTime.ToString("F3");
                        Count++;
                    }
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        /// <summary> 計時起始點，可以不使用 </summary>
        private void StartTimeCounter(string Name, double Target_s = 0)
        {
            if (mAlarmTimeCount.Keys.Contains(Name))
            {
                mAlarmTimeCount[Name].TimeCounterNow = DateTime.Now;
                mAlarmTimeCount[Name].TimeTarget = Target_s;
            }
            else
            {
                mAlarmTimeCount.Add(Name, new AlarmTimeCount());
            }
        }
        /// <summary> 計時起始 + 結束點 </summary>
        private void SetTimeCounter(string Name)
        {
            if (mAlarmTimeCount.Keys.Contains(Name))
            {
                mAlarmTimeCount[Name].SetNowTime(DateTime.Now);
            }
            else
            {
                mAlarmTimeCount.Add(Name, new AlarmTimeCount());
            }
        }
        /// <summary> 清除計時 </summary>
        private void ClrTimeCounter(string Name)
        {
            if (mAlarmTimeCount.Keys.Contains(Name))
            {
                mAlarmTimeCount.Remove(Name);
            }
        }

        private void Path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrow = new FolderBrowserDialog();
            if (folderBrow.ShowDialog() == DialogResult.OK)
            {
                ((TextBox)sender).Text = folderBrow.SelectedPath;
            }
        }

        private void monthCalendarToday_LostFocus(object sender, EventArgs e)
        {
            monthCalendarToday.Visible = false;
        }
        #endregion

        #region//===================== 以下為事件處理 =====================


        private void button1_Click(object sender, EventArgs e)
        {
            int iValue = 0;
            if (int.TryParse(textBox1.Text, out iValue) == true)
            {
                if (Enum.IsDefined(typeof(clsEnum.enuAlarm), iValue) == true)
                {
                    clsEditRunThread.ReportAlarm((clsEnum.enuAlarm)iValue);
                }
                else
                {
                    formAlarmReport.ReportAlm(textBox1.Text);
                }
            }
            else
            {
                formAlarmReport.ReportAlm(textBox1.Text);
            }
        }

        private void ListAlarmCounterFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMessage();
        }

        private void txt_SelectDate_Click(object sender, EventArgs e)
        {
            monthCalendarToday.Visible = true;
            monthCalendarToday.Focus();
        }

        private void monthCalendarToday_DateSelected(object sender, DateRangeEventArgs e)
        {
            txt_SelectDate.Text = monthCalendarToday.SelectionEnd.ToString("yyyy-MM-dd");
            monthCalendarToday.Visible = false;
            btn_Copy1DayLog.Focus();
        }

        private void btn_Copy1DayLog_Click(object sender, EventArgs e)
        {
            List<string> ListCopyLogFile = new List<string>();
            try
            {
                if (System.IO.Directory.Exists(txt_CopyLogTargetPath.Text) == true)
                {
                    foreach (string Folder in System.IO.Directory.GetDirectories(txt_CopyLogTargetPath.Text))
                    {
                        DirectoryInfo mDirectoryInfo = new DirectoryInfo(Folder);
                        if (mDirectoryInfo.Name.Contains("CopyLog-") == true
                            && mDirectoryInfo.Name.Length == 18)
                        {
                            DeleteFolder(Folder);
                        }
                    }
                }
                foreach (string LogTypeFolder in System.IO.Directory.GetDirectories(@"D:\Log"))
                {
                    foreach (string LogFileName in System.IO.Directory.GetFiles(LogTypeFolder))
                    {
                        if (LogFileName.Contains(txt_SelectDate.Text))
                        {
                            ListCopyLogFile.Add(LogFileName);
                        }
                    }
                }
                string TargetPath = txt_CopyLogTargetPath.Text + "\\CopyLog-" + txt_SelectDate.Text + "\\";
                if (System.IO.Directory.Exists(TargetPath) == false)
                {
                    System.IO.Directory.CreateDirectory(TargetPath);
                }
                foreach (string LogTypeFolder in ListCopyLogFile)
                {
                    FileInfo mFileInfo = new FileInfo(LogTypeFolder);
                    string sFileName = mFileInfo.Name;
                    string sFolderName = mFileInfo.Directory.Name;
                    System.IO.File.Copy(LogTypeFolder, TargetPath + sFileName, true);
                }
                #region//@1.0.0.16-13@
                System.IO.File.Copy(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe), TargetPath + DateTime.Now.ToString("yyyy-MM-dd") + "-RLog.log", true);
                System.IO.File.Copy(ucParameter.GetFilePath(clsEnum.enuPmtType.System), TargetPath + DateTime.Now.ToString("yyyy-MM-dd") + "-SLog.log", true);
                #endregion
                threadLog.SaveLog(TargetPath + DateTime.Now.ToString("yyyy-MM-dd") + "-CaseLog" + ".txt");
                formMessageBox.Show("Copy Log Finish!\r\nTo \"" + TargetPath + "\"");
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
                formMessageBox.Show("Copy Log Fail !!!");
            }
        }

        private void DeleteFolder(string Folder)
        {
            try
            {
                foreach (string FileName in System.IO.Directory.GetFiles(Folder))
                {
                    System.IO.File.Delete(FileName);
                }
                foreach (string sFolder in System.IO.Directory.GetDirectories(Folder))
                {
                    foreach (string FileName in System.IO.Directory.GetFiles(sFolder))
                    {
                        System.IO.File.Delete(FileName);
                    }
                    foreach (string sFolders in System.IO.Directory.GetDirectories(sFolder))
                    {
                        DeleteFolder(sFolders);
                        System.IO.Directory.Delete(sFolders);
                    }
                    System.IO.Directory.Delete(sFolder);
                }
                System.IO.Directory.Delete(Folder);
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }
        #endregion

        public class AlarmTimeCount
        {
            public DateTime TimeCounterNow;
            public double TimeCounterMax;
            public double TimeCounterCycle;
            public double TimeCounterMin;
            public double TimeCounterTotal;
            public double TimeCounterCount;
            public double TimeTarget;
            public double TimeCounterAvg()
            {
                return Convert.ToDouble(Convert.ToInt32((TimeCounterTotal / TimeCounterCount) * 1000)) / 1000;
            }
            public AlarmTimeCount()
            {
                TimeCounterNow = DateTime.Now;
                TimeCounterMax = 0;
                TimeCounterCycle = 0;
                TimeCounterMin = 99999999999;
                TimeCounterTotal = 0;
                TimeCounterCount = 0;
            }
            public void SetNowTime(DateTime NowTime)
            {
                long lTime = NowTime.Ticks - TimeCounterNow.Ticks;
                double dTime = (double)lTime / 10000000;
                if (dTime > TimeCounterMax)
                {
                    TimeCounterMax = dTime;
                }
                if (dTime < TimeCounterMin)
                {
                    TimeCounterMin = dTime;
                }
                TimeCounterCycle = dTime;
                TimeCounterTotal += dTime;
                TimeCounterCount++;
                if (TimeCounterCount >= 1000)
                {
                    TimeCounterTotal = TimeCounterTotal / 2;
                    TimeCounterCount = TimeCounterCount / 2;
                }
                TimeCounterNow = NowTime;
            }

        }



    }
}

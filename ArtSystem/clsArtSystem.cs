using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ART.Security;

namespace ArtSystem
{
    public class clsArtSystem
    {
        /// <summary> 程式開啟時設為True(方便建構式判斷是否為真實開啟程式) </summary>
        static public bool bIsProgramOpen = false;
        /// <summary> 程式完全開啟後設為True </summary>
        static public bool bIsProgramOpenFinish = false;
        /// <summary> 程式已關閉旗標 </summary>
        static public bool bIsProgramClosed = false;
        /// <summary> 程式重疊開啟 </summary>
        static public bool bIsARTMMIDuplication = false;
        /// <summary> 是否為電腦軟體模擬 (沒有設備的情況下使用true,false) - 如果D:\\ArtSimulate.ini 則自動變成模擬 </summary>
        static public bool bIsSoftwareSimulate = false;
        /// <summary> 是否為電腦軟體模擬 (沒有設備的情況下使用true,false) - 如果D:\\ArtSimulate.ini (mFile.GetInt32("ArtSimulate", "bIsArtAOIGrabSimulate", 0) > 0) 則自動變成模擬 </summary>
        static public bool bIsArtAOIGrabSimulate = false;
        /// <summary> 如果D:\\ArtSimulate.ini 文件裡面有對應的Flag,可以透過GetSimulate()取得 </summary>
        static private Dictionary<string, bool> m_bDicSimulateFlage = new Dictionary<string, bool>();

        static public bool bIsArtMachineSetup = false;

        /// <summary> 儲存INI文件的路徑，指定路徑內如果有對應的INI文件則,開啟/關閉程式時會進行載入與備份。 </summary>
        static public string strINIPath = "D:\\Parameter\\INI\\";
        /// <summary> 指定INI文件強制綁訂到clsArtSystem.strINIPath指定路徑內(D:\\Parameter\\INI\\) </summary>
        static public List<string> lst_INIFile_To_strINIPath = new List<string>() { "artEqParameter.ini" };

        static public string g_strCatchLogName = "CatchLog";
        static public string g_strStartUpLogName = "StartUpLog";


        #region//========== 備份INI ==========

        /// <summary> 載入-指定路徑clsArtSystem.strINIPath內如果有對應的INI文件則進行載入 </summary>
        static public void ReloadINI()
        {
            if (clsArtSystem.strINIPath != null && clsArtSystem.strINIPath != "")
            {
                if (System.IO.Directory.Exists(clsArtSystem.strINIPath) == false)
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(clsArtSystem.strINIPath);
                    }
                    catch (Exception ex)
                    {
                        clsArtSystem.CatchLog(ex);
                        return;
                    }
                }
                List<string> MultiSystemINI = MultiSystem.clsMultiSystem.ReloadINI(lst_INIFile_To_strINIPath);
                if (System.IO.Directory.Exists(clsArtSystem.strINIPath) == true)
                {
                    List<string> Files = System.IO.Directory.GetFiles(clsArtSystem.strINIPath).ToList<string>();
                    foreach (string sFilePath in Files)
                    {
                        string sFileName = System.IO.Path.GetFileName(sFilePath);
                        if (MultiSystemINI.Contains(sFileName) == true)//如果...\\Bin\\Debug\\INI\\System\\"SystemName"內已有檔案，則依此檔案為主
                        {
                            continue;
                        }
                        if (System.IO.File.Exists(clsCmData.g_strSystemIniFilePath.Replace("artSystem.ini", "") + "\\" + sFileName) == true
                            || lst_INIFile_To_strINIPath.Contains(sFileName) == true)
                        {
                            try
                            {
                                System.IO.File.Copy(sFilePath, clsCmData.g_strSystemIniFilePath.Replace("artSystem.ini", "") + "\\" + sFileName, true);
                            }
                            catch (Exception ex)
                            {
                                clsArtSystem.CatchLog(ex);
                            }
                        }
                    }
                }
            }
        }

        /// <summary> 備份-指定路徑clsArtSystem.strINIPath內如果有對應的INI文件則進行備份 </summary>
        static public void BackupINI()
        {
            if (clsArtSystem.strINIPath != null && clsArtSystem.strINIPath != "")
            {
                if (System.IO.Directory.Exists(clsArtSystem.strINIPath) == false)
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(clsArtSystem.strINIPath);
                    }
                    catch (Exception ex)
                    {
                        clsArtSystem.CatchLog(ex);
                        return;
                    }
                }
                List<string> MultiSystemINI = MultiSystem.clsMultiSystem.BackupINI(lst_INIFile_To_strINIPath);
                if (System.IO.Directory.Exists(clsArtSystem.strINIPath) == true)
                {
                    List<string> Files = System.IO.Directory.GetFiles(clsCmData.g_strSystemIniFilePath.Replace("artSystem.ini", "")).ToList<string>();
                    foreach (string sFilePath in Files)
                    {
                        string sFileName = System.IO.Path.GetFileName(sFilePath);
                        if (MultiSystemINI.Contains(sFileName) == true)//如果...\\Bin\\Debug\\INI\\System\\"SystemName"內已有檔案，則依此檔案為主
                        {
                            continue;
                        }
                        if (System.IO.File.Exists(clsArtSystem.strINIPath + "\\" + sFileName) == true
                            || clsArtSystem.lst_INIFile_To_strINIPath.Contains(sFileName) == true)
                        {
                            try
                            {
                                if (System.IO.File.Exists(clsArtSystem.strINIPath + "\\" + sFileName) == true
                                    && System.IO.File.GetLastWriteTime(clsArtSystem.strINIPath + "\\" + sFileName) != System.IO.File.GetLastWriteTime(sFilePath))
                                {
                                    if (System.IO.File.GetLastWriteTime(clsArtSystem.strINIPath + "\\" + sFileName) < System.IO.File.GetLastWriteTime(sFilePath))
                                    {
                                        System.IO.File.Copy(sFilePath, clsArtSystem.strINIPath + "\\" + sFileName, true);
                                        clsLog.Log(clsCmData.enuLogType.SystemLog, "[Backup INI] \"" + sFilePath + "\" Copy To \"" + clsArtSystem.strINIPath + "\\" + sFileName + "\"");
                                    }
                                    else
                                    {
                                        System.IO.File.Copy(sFilePath, clsArtSystem.strINIPath + "\\" + sFileName, true);
                                        clsLog.Log(clsCmData.enuLogType.SystemLog, "[Backup INI] \"" + clsArtSystem.strINIPath + "\\" + sFileName + "\"" + "\" Copy To \"" + sFilePath);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                clsArtSystem.CatchLog(ex);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region//========== ArtSystem模擬Flag ==========

        static public void LoadSimulateFlag()
        {
            try
            {
                m_bDicSimulateFlage.Clear();
                if (System.IO.File.Exists("D:\\ArtSimulate.ini") == true)
                {
                    clsArtSystem.bIsSoftwareSimulate = true;
                    m_bDicSimulateFlage.Add("SoftwareSimulate", true);
                    clsIniFile mFile = new clsIniFile("D:\\ArtSimulate.ini");
                    clsArtSystem.bIsArtAOIGrabSimulate = mFile.GetInt32("ArtSimulate", "bIsArtAOIGrabSimulate", 0) > 0;
                    List<string> LstPmtName = mFile.GetKeyNames("ArtSimulate").ToList<string>();
                    foreach (string sPmtName in LstPmtName)
                    {
                        bool bFlag = mFile.GetInt32("ArtSimulate", sPmtName, 0) > 0;
                        m_bDicSimulateFlage.Add(sPmtName, bFlag);

                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 如果D:\\ArtSimulate.ini 文件裡面有對應的Flag,則回傳True </summary>
        static public bool GetSimulate(string p_sName)
        {
            bool rValue = false;
            if (m_bDicSimulateFlage.ContainsKey(p_sName) == true)
            {
                rValue = m_bDicSimulateFlage[p_sName];
            }
            return rValue;
        }

        #endregion

        #region//========== ArtSystem外部參數 ==========
        static public clsModulePmt mSystemPmt = null;//new clsModulePmt("ArtSystem");

        public enum enuParameterName
        {
            /// <summary> 0:Relative, 1:Continue </summary>
            TeachJog_Mode,
            TeachJog_Speed_Percentage,
            TeachJog_Distance,
        }
        static public string GetValueString(enuParameterName ePmtName)
        {
            string rValue = mSystemPmt.Pmt_GetString(ePmtName.ToString());
            return rValue;
        }
        static public decimal GetValueDecimal(enuParameterName ePmtName)
        {
            decimal rValue = mSystemPmt.Pmt_GetDecimal(ePmtName.ToString());
            return rValue;
        }
        static public double GetValueDouble(enuParameterName ePmtName)
        {
            double rValue = (double)mSystemPmt.Pmt_GetDecimal(ePmtName.ToString());
            return rValue;
        }
        static public int GetValueInt(enuParameterName ePmtName)
        {
            int rValue = Convert.ToInt32(mSystemPmt.Pmt_GetDecimal(ePmtName.ToString()));
            return rValue;
        }
        static public bool GetValueBool(enuParameterName ePmtName)
        {
            bool rValue = mSystemPmt.Pmt_GetDecimal(ePmtName.ToString()) > 0;
            return rValue;
        }
        static public void SaveValue(clsEnum.enuPmtType eType, enuParameterName pPmtType, string sValue)
        {
            mSystemPmt.Pmt_SaveString(eType, pPmtType.ToString(), sValue);
        }
        static public void SaveValue(clsEnum.enuPmtType eType, enuParameterName pPmtType, decimal sValue)
        {
            mSystemPmt.Pmt_SaveString(eType, pPmtType.ToString(), sValue.ToString());
        }
        static public void SaveValue(clsEnum.enuPmtType eType, enuParameterName pPmtType, double sValue)
        {
            mSystemPmt.Pmt_SaveString(eType, pPmtType.ToString(), sValue.ToString());
        }
        static public void SaveValue(clsEnum.enuPmtType eType, enuParameterName pPmtType, int sValue)
        {
            mSystemPmt.Pmt_SaveString(eType, pPmtType.ToString(), sValue.ToString());
        }
        static public void CollectPmtControl(Control p_Control, enuParameterName pPmtType)
        {
            mSystemPmt.CollectPmtControl(p_Control, pPmtType.ToString());
        }

        #endregion

        #region//========== Log Path Builder ==========

        /// <summary> 自動建立自動刪除Log的路徑設定 </summary>
        static public void LogPathBuilder()
        {
            try
            {
                List<string> FileData = new List<string>();
                string FilePath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\LogPath.ini";
                string LogPath = "D:\\Log\\";
                bool NeedRebuild = false;
                if (System.IO.File.Exists(FilePath) == true)
                {
                    #region//載入舊的LogPath.INI
                    FileData = System.IO.File.ReadAllLines(FilePath).ToList<string>();
                    #endregion

                    #region//重新加入enuLogTypes裡面的定義.
                    foreach (clsEnum.enuLogName LogName in Enum.GetValues(typeof(clsEnum.enuLogName)))
                    {
                        bool isHaveLogPath = false;
                        foreach (string LineMessage in FileData)
                        {
                            if (LineMessage.Contains(LogName.ToString() + "="))
                            {
                                isHaveLogPath = true;
                                break;
                            }
                        }
                        if (isHaveLogPath == false)
                        {
                            FileData.Add(LogName.ToString() + "=" + LogPath + LogName.ToString());
                            NeedRebuild = true;
                        }
                    }
                    #endregion

                    #region//重新創健一個新的LogPath.INI
                    if (NeedRebuild == true)
                    {
                        System.IO.File.WriteAllLines(FilePath, FileData);
                    }
                    #endregion
                }
                else
                {
                    #region//重新創健一個新的LogPath.INI
                    FileData.Add("[AutoDelLog]");
                    FileData.Add("IsAutoDelLog=1");
                    FileData.Add("AutoDelDays=180");
                    FileData.Add("[LogPath]");
                    foreach (clsEnum.enuLogName LogName in Enum.GetValues(typeof(clsEnum.enuLogName)))
                    {
                        FileData.Add(LogName.ToString() + "=" + LogPath + LogName.ToString());
                    }
                    System.IO.File.WriteAllLines(FilePath, FileData);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region//========== Warnning -> CatchLog 細節 ==========
        /// <summary> 確認CatchLog是否有發生異常 </summary>
        static private DateTime mTimer_CatchLog_Startup { get; set; }

        static private DateTime mLastCheckTime = DateTime.MinValue;

        static private bool bLastCheckResult = false;

        /// <summary> 確認CatchLog是否有發生異常 </summary>
        static public bool bIsCatchOccour()
        {
            bool rValue = false;
            try
            {
                if ((DateTime.Now - mLastCheckTime).TotalSeconds < 10)
                {
                    rValue = bLastCheckResult;
                }
                else
                {
                    string sINIPath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\LogPath.ini";
                    if (System.IO.File.Exists(sINIPath) == true)
                    {
                        clsIniFile mFile = new clsIniFile(sINIPath);
                        string sPath = mFile.GetString("LogPath", clsArtSystem.g_strCatchLogName, @"D:\Log\" + clsArtSystem.g_strCatchLogName + @".log");
                        if (System.IO.Directory.Exists(sPath) == true)
                        {
                            var dirInfo = new DirectoryInfo(sPath);
                            var recentFiles = dirInfo.GetFiles().OrderByDescending(f => f.LastWriteTime).Take(5); // 只取最近 5 個檔案
                            if (mTimer_CatchLog_Startup.Ticks == 0)
                            {
                                foreach (var FileInfo in recentFiles)
                                {
                                    var lastWrite = FileInfo.LastWriteTime;
                                    if (mTimer_CatchLog_Startup < lastWrite)
                                    {
                                        mTimer_CatchLog_Startup = lastWrite;
                                    }

                                }
                            }
                            else
                            {
                                foreach (var FileInfo in recentFiles)
                                {
                                    if (mTimer_CatchLog_Startup < FileInfo.LastWriteTime)
                                    {
                                        rValue = true;
                                        break;
                                    }
                                }
                            }

                        }
                    }
                    mLastCheckTime = DateTime.Now;
                    bLastCheckResult = rValue;
                }
            }
            catch (Exception ex)
            {
            }
            return rValue;
        }
        static public void ResetCatchOccour()
        {
            mTimer_CatchLog_Startup = new DateTime(0);

        }
        static public void CatchLog(Exception ex)
        {
            ArtSystem.UnhandledExceptionMessageBox.Log(ex);
        }

        #endregion
        #region//========== Security Check ==========
        static private bool m_bSecurityCheckPreviousStatus = false;
        static private DateTime m_DateTimeSecurityOccour = DateTime.MinValue;
        static public SecurityError GetSecurityCheckStatus(double p_Timeout_Minute = 5)
        {
            bool bNowError = true;
            SecurityError rValue = SecurityError.NoError;
            if (SecurityChecker.LiscenseAlive == true)
            {
                bNowError = false;
            }
            else if (SecurityChecker.ExpiredDate.Year > 9000)
            {
                bNowError = false;
            }
            if (bNowError == true)
            {
                if (m_bSecurityCheckPreviousStatus == false)
                {
                    m_DateTimeSecurityOccour = DateTime.Now;
                    m_bSecurityCheckPreviousStatus = true;
                }
            }
            else
            {
                m_bSecurityCheckPreviousStatus = false;
            }
            if (bNowError == true && m_DateTimeSecurityOccour.AddMinutes(p_Timeout_Minute) < DateTime.Now)
            {
                rValue = SecurityChecker.Error;
            }
            return rValue;
        }
        #endregion
    }
}

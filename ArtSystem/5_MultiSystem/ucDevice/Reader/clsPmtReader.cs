using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtSystem.MultiSystem
{
    public class clsPmtReader
    {
        #region//========== Enum 定義 ==========
        public enum enuPmtName
        {
            ReaderName,
            ReaderType,
            TCP_IP,
            TCP_Port,
            ChannelID,
            TimeOut_ms,//3000
            DelayTime_ms,
            Simulate,
            ByPass,
        }
        #endregion

        #region//========== 參數 ==========

        public string sINIPath
        {
            get;
            private set;
        }
        public Dictionary<string, Dictionary<enuPmtName, string>> mDic_mPmtValue = new Dictionary<string, Dictionary<enuPmtName, string>>();
        public Dictionary<string, clsCtrlReader> mDic_CtrlReader = new Dictionary<string, clsCtrlReader>();

        #endregion

        #region//========== Public函式 ==========

        public clsCtrlReader GetMotionRoller(int iIndex)
        {
            if (iIndex >= 0 && iIndex < mDic_CtrlReader.Count)
            {
                return mDic_CtrlReader.ElementAt(iIndex).Value;
            }
            return null;
        }
        public clsCtrlReader GetMotionRoller(string sKey)
        {
            if (mDic_CtrlReader.ContainsKey(sKey))
            {
                return mDic_CtrlReader[sKey];
            }
            return null;
        }
        public void InitialReader(bool bSimulate = false)
        {
            string sPath = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath + "\\Reader.ini";
            if (System.IO.File.Exists("D:\\Parameter\\INI\\Reader.ini") == true)
            { sPath = "D:\\Parameter\\INI\\Reader.ini"; }
            Load(sPath);
            try
            {
                foreach (string sKey in mDic_mPmtValue.Keys)
                {
                    string sTCP_IP = mDic_mPmtValue[sKey][enuPmtName.TCP_IP];
                    int iTCP_Port = Convert.ToInt32(mDic_mPmtValue[sKey][enuPmtName.TCP_Port]);
                    clsCtrlReader.enuReaderType eType = (clsCtrlReader.enuReaderType)Enum.Parse(typeof(clsCtrlReader.enuReaderType), mDic_mPmtValue[sKey][enuPmtName.ReaderType]);
                    if (Enum.IsDefined(typeof(clsCtrlReader.enuReaderType), eType) == false)
                    { eType = clsCtrlReader.enuReaderType.Keyence2D; }
                    mDic_CtrlReader.Add(sKey, new clsCtrlReader(sKey, eType, sTCP_IP, iTCP_Port, bSimulate));
                    mDic_CtrlReader[sKey].iChannelID = Convert.ToInt32(mDic_mPmtValue[sKey][enuPmtName.ChannelID]);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                formMessageBox.Show("Initial Reader, Catch Error");
            }
        }
        #endregion

        #region//========== Public函式(Save,Load) ==========

        /// <summary> 儲存參數 </summary>
        public void Save(string sPath)
        {
            try
            {
                sINIPath = sPath;
                if (System.IO.File.Exists(sPath) == true)
                { System.IO.File.Delete(sPath); }
                clsIniFile mFile = new clsIniFile(sPath);
                foreach (string SectionName in mDic_mPmtValue.Keys)
                {
                    foreach (enuPmtName PmtName in mDic_mPmtValue[SectionName].Keys)
                    {
                        mFile.WriteValue(SectionName, PmtName.ToString(), mDic_mPmtValue[SectionName][PmtName]);
                    }
                }
                Load(sINIPath);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 寫入參數 </summary>
        public void Load(string sPath)
        {
            try
            {
                sINIPath = sPath;
                //mDic_mPmtValue.Clear();
                if (System.IO.File.Exists(sPath) == true)
                {
                    clsIniFile mFile = new clsIniFile(sPath);
                    string[] SectionName = mFile.GetSectionNames();
                    List<string> RemoveItems = new List<string>();
                    foreach (string sKey in mDic_mPmtValue.Keys)
                    {
                        RemoveItems.Add(sKey);
                    }
                    for (int iSection = 0; iSection < SectionName.Length; iSection++)
                    {
                        if (mDic_mPmtValue.ContainsKey(SectionName[iSection]) == false)
                        {
                            mDic_mPmtValue.Add(SectionName[iSection], new Dictionary<enuPmtName, string>());
                        }
                        else
                        {
                            if (RemoveItems.Contains(SectionName[iSection]) == true)
                            {
                                RemoveItems.Remove(SectionName[iSection]);
                            }
                        }
                    }
                    foreach (string sKey in RemoveItems)
                    {
                        mDic_mPmtValue.Remove(sKey);
                    }
                    for (int iSection = 0; iSection < SectionName.Length; iSection++)
                    {
                        string[] PmtName = mFile.GetKeyNames(SectionName[iSection]);
                        for (int iKey = 0; iKey < PmtName.Length; iKey++)
                        {
                            enuPmtName mPmt = (enuPmtName)Enum.Parse(typeof(enuPmtName), PmtName[iKey]);
                            if (Enum.IsDefined(typeof(enuPmtName), mPmt) == true)
                            {
                                mDic_mPmtValue[SectionName[iSection]][mPmt] = mFile.GetString(SectionName[iSection], PmtName[iKey], "");
                                mDic_mPmtValue[SectionName[iSection]][enuPmtName.ReaderName] = SectionName[iSection];
                            }
                        }
                        foreach (enuPmtName pPmtName in Enum.GetValues(typeof(enuPmtName)))
                        {
                            if (mDic_mPmtValue[SectionName[iSection]].ContainsKey(pPmtName) == false)
                            {
                                mDic_mPmtValue[SectionName[iSection]].Add(pPmtName, "");
                            }
                        }
                    }
                }
                else
                {
                    mDic_mPmtValue.Clear();
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

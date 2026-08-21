using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using System.IO.Ports;
using System.Net.Sockets;
using System.Net;

namespace ArtSystem.MultiSystem
{
    public class clsPmtTCPLink
    {
        #region//========== Enum 定義 ==========

        public enum enuPmtName
        {
            Name,
            bIsServer,
            TCP_IP,
            TCP_Port,
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

        public Dictionary<string, clsCtrlTCPLink> mDic_CtrlTCPLink = new Dictionary<string, clsCtrlTCPLink>();

        #endregion

        #region//========== Public函式 ==========

        public clsCtrlTCPLink GetTCPLink(int iIndex)
        {
            if (iIndex >= 0 && iIndex < mDic_CtrlTCPLink.Count)
            {
                return mDic_CtrlTCPLink.ElementAt(iIndex).Value;
            }
            return null;
        }
        public clsCtrlTCPLink GetTCPLink(string sKey)
        {
            if (mDic_CtrlTCPLink.ContainsKey(sKey))
            {
                return mDic_CtrlTCPLink[sKey];
            }
            return null;
        }
        public void InitialTCPLink()
        {
            string sPath = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath + "\\TCPLink.ini";
            if (System.IO.File.Exists("D:\\Parameter\\INI\\TCPLink.ini") == true)
            { sPath = "D:\\Parameter\\INI\\TCPLink.ini"; }
            Load(sPath);
            try
            {
                foreach (string sKey in mDic_mPmtValue.Keys)
                {
                    mDic_CtrlTCPLink.Add(sKey, new clsCtrlTCPLink());
                    mDic_CtrlTCPLink[sKey].sName = sKey;

                    int iTCPIP = 0;
                    ConvertStringToInt(mDic_mPmtValue[sKey][enuPmtName.TCP_Port], ref iTCPIP);
                    if(mDic_mPmtValue[sKey][enuPmtName.bIsServer]  == "1")
                    {
                        mDic_CtrlTCPLink[sKey].SetServer();
                    }
                    else
                    {
                        mDic_CtrlTCPLink[sKey].SetClient();
                    }
                    mDic_CtrlTCPLink[sKey]._Connect(mDic_mPmtValue[sKey][enuPmtName.TCP_IP], iTCPIP);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                formMessageBox.Show("Initial High Sensor, Catch Error");
            }
        }

        #endregion

        #region//========== private函式 ==========

        private void ConvertStringToInt(string sValue, ref int iValue)
        {
            if (sValue.Length > 0)
            {
                try
                { iValue = Convert.ToInt32(sValue); }
                catch (Exception ex)
                { clsArtSystem.CatchLog(ex); }
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
                                mDic_mPmtValue[SectionName[iSection]][enuPmtName.Name] = SectionName[iSection];
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

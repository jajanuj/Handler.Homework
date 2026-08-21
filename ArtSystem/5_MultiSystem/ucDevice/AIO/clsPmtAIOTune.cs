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
    public class clsPmtAIOTune
    {
        #region//========== Enum 定義 ==========

        public enum enuPmtName
        {
            Gain,
            Shift,
            Offset,
        }

        #endregion

        #region//========== 參數 ==========

        public string sINIPath
        {
            get;
            private set;
        }

        public Dictionary<clsEnum.enuDi, Dictionary<enuPmtName, string>> mDic_AIPmtValue = new Dictionary<clsEnum.enuDi, Dictionary<enuPmtName, string>>();
        public Dictionary<clsEnum.enuDo, Dictionary<enuPmtName, string>> mDic_AOPmtValue = new Dictionary<clsEnum.enuDo, Dictionary<enuPmtName, string>>();

        #endregion

        #region//========== Public函式 ==========

        public void InitialAIOTune()
        {
            string sPath = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath + "\\AIOTune.ini";
            if (System.IO.File.Exists("D:\\Parameter\\INI\\AIOTune.ini") == true)
            { sPath = "D:\\Parameter\\INI\\AIOTune.ini"; }
            Load(sPath);
            SetAIOPmt();
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
                foreach (clsEnum.enuDi SectionName in mDic_AIPmtValue.Keys)
                {
                    foreach (enuPmtName PmtName in Enum.GetValues(typeof(enuPmtName)))
                    {
                        mFile.WriteValue(SectionName.ToString(), PmtName.ToString(), mDic_AIPmtValue[SectionName][PmtName]);
                    }
                }
                foreach (clsEnum.enuDo SectionName in mDic_AOPmtValue.Keys)
                {
                    foreach (enuPmtName PmtName in Enum.GetValues(typeof(enuPmtName)))
                    {
                        mFile.WriteValue(SectionName.ToString(), PmtName.ToString(), mDic_AOPmtValue[SectionName][PmtName]);
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
                #region//確認所有AIO都有參數功能
                foreach (clsEnum.enuDi eDI in clsDioMotion.mLst_AIEnum)
                {
                    if (mDic_AIPmtValue.ContainsKey(eDI) == false)
                    {
                        mDic_AIPmtValue.Add(eDI, new Dictionary<enuPmtName, string>());
                    }
                    foreach (enuPmtName PmtName in Enum.GetValues(typeof(enuPmtName)))
                    {
                        if (mDic_AIPmtValue[eDI].ContainsKey(PmtName) == false)
                        {
                            if (PmtName == enuPmtName.Gain)
                            {
                                mDic_AIPmtValue[eDI].Add(PmtName, "1");

                            }
                            else
                            {
                                mDic_AIPmtValue[eDI].Add(PmtName, "0");
                            }
                        }
                    }
                }
                foreach (clsEnum.enuDo eDO in clsDioMotion.mLst_AOEnum)
                {
                    if (mDic_AOPmtValue.ContainsKey(eDO) == false)
                    {
                        mDic_AOPmtValue.Add(eDO, new Dictionary<enuPmtName, string>());
                    }
                    foreach (enuPmtName PmtName in Enum.GetValues(typeof(enuPmtName)))
                    {
                        if (mDic_AOPmtValue[eDO].ContainsKey(PmtName) == false)
                        {
                            if (PmtName == enuPmtName.Gain)
                            {
                                mDic_AOPmtValue[eDO].Add(PmtName, "1");

                            }
                            else
                            {
                                mDic_AOPmtValue[eDO].Add(PmtName, "0");
                            }
                        }
                    }
                }
                #endregion
                if (System.IO.File.Exists(sPath) == true)
                {
                    clsIniFile mFile = new clsIniFile(sPath);
                    string[] SectionName = mFile.GetSectionNames();
                    List<string> RemoveItems = new List<string>();
                    foreach (clsEnum.enuDi eDI in mDic_AIPmtValue.Keys)
                    {
                        if (SectionName.Contains(eDI.ToString()) == true)
                        {
                            Dictionary<enuPmtName, string> LstData = mDic_AIPmtValue[eDI];
                            foreach (enuPmtName PmtName in Enum.GetValues(typeof(enuPmtName)))
                            {
                                if (PmtName == enuPmtName.Gain)
                                {
                                    LstData[PmtName] = mFile.GetString(eDI.ToString(), PmtName.ToString(), "1");
                                }
                                else
                                {
                                    LstData[PmtName] = mFile.GetString(eDI.ToString(), PmtName.ToString(), "0");
                                }
                            }
                        }
                        else
                        {
                            foreach (enuPmtName PmtName in Enum.GetValues(typeof(enuPmtName)))
                            {
                                if (PmtName == enuPmtName.Gain)
                                {
                                    mDic_AIPmtValue[eDI][PmtName] = "1";
                                }
                                else
                                {
                                    mDic_AIPmtValue[eDI][PmtName] = "0";
                                }
                            }
                        }
                    }
                    foreach (clsEnum.enuDo eDO in mDic_AOPmtValue.Keys)
                    {
                        if (SectionName.Contains(eDO.ToString()) == true)
                        {
                            foreach (enuPmtName PmtName in Enum.GetValues(typeof(enuPmtName)))
                            {
                                if (PmtName == enuPmtName.Gain)
                                {
                                    mDic_AOPmtValue[eDO][PmtName] = mFile.GetString(eDO.ToString(), PmtName.ToString(), "1");
                                }
                                else
                                {
                                    mDic_AOPmtValue[eDO][PmtName] = mFile.GetString(eDO.ToString(), PmtName.ToString(), "0");
                                }
                            }
                        }
                        else
                        {
                            foreach (enuPmtName PmtName in Enum.GetValues(typeof(enuPmtName)))
                            {
                                if (PmtName == enuPmtName.Gain)
                                {
                                    mDic_AOPmtValue[eDO][PmtName] = "1";
                                }
                                else
                                {
                                    mDic_AOPmtValue[eDO][PmtName] = "0";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        public void SetAIOPmt()
        {
            try
            {
                foreach (clsEnum.enuDi eDI in mDic_AIPmtValue.Keys)
                {
                    double dGain = Convert.ToDouble(mDic_AIPmtValue[eDI][enuPmtName.Gain]);
                    double dShift = Convert.ToDouble(mDic_AIPmtValue[eDI][enuPmtName.Shift]);
                    double dOffset = Convert.ToDouble(mDic_AIPmtValue[eDI][enuPmtName.Offset]);
                    clsDioCtrl.SetAiPara(eDI, dGain, dOffset, dShift, 3);
                }
                foreach (clsEnum.enuDo eDO in mDic_AOPmtValue.Keys)
                {
                    double dGain = Convert.ToDouble(mDic_AOPmtValue[eDO][enuPmtName.Gain]);
                    double dShift = Convert.ToDouble(mDic_AOPmtValue[eDO][enuPmtName.Shift]);
                    double dOffset = Convert.ToDouble(mDic_AOPmtValue[eDO][enuPmtName.Offset]);
                    clsDioCtrl.SetAoPara(eDO, dGain, dOffset, dShift);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                formMessageBox.Show("Initial AIOTune, Catch Error");
            }

        }

        #endregion
    }
}

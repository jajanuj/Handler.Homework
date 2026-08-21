using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtSystem.MultiSystem
{
    public class clsCardInfo
    {
        public string sINIPath
        {
            get;
            private set;
        }

        /// <summary> 軸卡的定義 </summary>
        public List<clsMotionCardSetup> mLstMotionCardInfo = new List<clsMotionCardSetup>();
        /// <summary> DIO卡的數量定義 </summary>
        public List<clsDIOCardSetup> mLstDIOCardInfo = new List<clsDIOCardSetup>();
        /// <summary> AIO類比輸入輸出的定義 </summary>
        public List<clsAIOCardSetup> mLstAIOCardInfo = new List<clsAIOCardSetup>();

        /// <summary> 儲存CardInfo </summary>
        public void Save(string sPath)
        {
            try
            {
                string sDirectory = System.IO.Path.GetDirectoryName(sPath);
                if (System.IO.Directory.Exists(sDirectory) == false)
                {
                    System.IO.Directory.CreateDirectory(sDirectory);
                }
                if (System.IO.Directory.Exists(sDirectory) == true)
                {
                    System.IO.File.Delete(sPath);
                    clsIniFile mIniFile = new clsIniFile(sPath, true);
                    for (int i = 0; i < mLstMotionCardInfo.Count; i++)
                    {
                        mIniFile.WriteValue("mLstMotionCardInfo_" + i.ToString(), "eMotionCard", mLstMotionCardInfo[i].eMotionCard.ToString());
                        mIniFile.WriteValue("mLstMotionCardInfo_" + i.ToString(), "eSlaveType", mLstMotionCardInfo[i].eSlaveType.ToString());
                        mIniFile.WriteValue("mLstMotionCardInfo_" + i.ToString(), "iCardID", mLstMotionCardInfo[i].iCardID.ToString());
                        mIniFile.WriteValue("mLstMotionCardInfo_" + i.ToString(), "iSlaveID", mLstMotionCardInfo[i].iSlaveID.ToString());
                        mIniFile.WriteValue("mLstMotionCardInfo_" + i.ToString(), "iETEL_SlaveNum", mLstMotionCardInfo[i].iETEL_SlaveNum.ToString());
                        mIniFile.WriteValue("mLstMotionCardInfo_" + i.ToString(), "bSimulate", mLstMotionCardInfo[i].bSimulate ? "1" : "0");
                        string sGantryAxis = "";
                        for (int iGantryCard = 0; iGantryCard < mLstMotionCardInfo[i].LstGantryAxis.Count; iGantryCard++)
                        {
                            if (sGantryAxis == "")
                            {
                                sGantryAxis += mLstMotionCardInfo[i].LstGantryAxis[iGantryCard];
                            }
                            else
                            {
                                sGantryAxis += "," + mLstMotionCardInfo[i].LstGantryAxis[iGantryCard];
                            }
                        }
                        mIniFile.WriteValue("mLstMotionCardInfo_" + i.ToString(), "LstGantryAxis", sGantryAxis);
                        if (mLstMotionCardInfo[i].eMotionCard == clsMultiSystem.enuMotionCard.Card7856)
                        {
                            string SaveCardPmtPath = sPath.Replace("CardInfo.ini", "Card7856Pmt_" + i + ".ini");
                            mIniFile.WriteValue("mLstMotionCardInfo_" + i.ToString(), "clsMotionCardPmt_PCI7856", SaveCardPmtPath);
                            if (mLstMotionCardInfo[i].mCartPmt_7856.Count > 0)
                            {
                                ArtSystem.Files.JsonHelper.JsonSerializeToFile(mLstMotionCardInfo[i].mCartPmt_7856, SaveCardPmtPath, Encoding.Unicode);
                            }
                        }
                        else if (mLstMotionCardInfo[i].eMotionCard == clsMultiSystem.enuMotionCard.ApsMasterTPM)
                        {
                            string SaveCardPmtPath = sPath.Replace("CardInfo.ini", "CardTPMPmt_" + i + ".ini");
                            mIniFile.WriteValue("mLstMotionCardInfo_" + i.ToString(), "clsMotionCardPmt_TPM", SaveCardPmtPath);
                            if (mLstMotionCardInfo[i].mCartPmt_TPM.Count > 0)
                            {
                                ArtSystem.Files.JsonHelper.JsonSerializeToFile(mLstMotionCardInfo[i].mCartPmt_TPM, SaveCardPmtPath, Encoding.Unicode);
                            }
                        }
                        else if (mLstMotionCardInfo[i].eMotionCard == clsMultiSystem.enuMotionCard.EtherCatMasterAdv)
                        {
                            string SaveCardPmtPath = sPath.Replace("CardInfo.ini", "CardEtherCatMasterAdvPmt_" + i + ".ini");
                            mIniFile.WriteValue("mLstMotionCardInfo_" + i.ToString(), "clsMotionCardPmt_AdvEtherCAT", SaveCardPmtPath);
                            if (mLstMotionCardInfo[i].mCartPmt_AdvEtherCAT.Count > 0)
                            {
                                ArtSystem.Files.JsonHelper.JsonSerializeToFile(mLstMotionCardInfo[i].mCartPmt_AdvEtherCAT, SaveCardPmtPath, Encoding.Unicode);
                            }
                        }
                    }
                    for (int i = 0; i < mLstDIOCardInfo.Count; i++)
                    {
                        mIniFile.WriteValue("mLstDIOCardInfo_" + i.ToString(), "eSysCard", mLstDIOCardInfo[i].eSysCard.ToString());
                        mIniFile.WriteValue("mLstDIOCardInfo_" + i.ToString(), "eDIOType", mLstDIOCardInfo[i].eDIOType.ToString());
                        mIniFile.WriteValue("mLstDIOCardInfo_" + i.ToString(), "iCardID", mLstDIOCardInfo[i].iCardID.ToString());
                        mIniFile.WriteValue("mLstDIOCardInfo_" + i.ToString(), "iSlaveID", mLstDIOCardInfo[i].iSlaveID.ToString());
                        mIniFile.WriteValue("mLstDIOCardInfo_" + i.ToString(), "eStartDi", "DI" + mLstDIOCardInfo[i].eStartDi.ToString("D3"));
                        mIniFile.WriteValue("mLstDIOCardInfo_" + i.ToString(), "eStartDo", "DO" + mLstDIOCardInfo[i].eStartDo.ToString("D3"));
                        mIniFile.WriteValue("mLstDIOCardInfo_" + i.ToString(), "bSimulate", mLstDIOCardInfo[i].bSimulate ? 1 : 0);
                    }
                    for (int i = 0; i < mLstAIOCardInfo.Count; i++)
                    {
                        mIniFile.WriteValue("mLstAIOCardInfo_" + i.ToString(), "iAICount", mLstAIOCardInfo[i].iAICount.ToString());
                        mIniFile.WriteValue("mLstAIOCardInfo_" + i.ToString(), "iAOCount", mLstAIOCardInfo[i].iAOCount.ToString());
                        mIniFile.WriteValue("mLstAIOCardInfo_" + i.ToString(), "iCardID", mLstAIOCardInfo[i].iCardID.ToString());
                        mIniFile.WriteValue("mLstAIOCardInfo_" + i.ToString(), "iSlaveID", mLstAIOCardInfo[i].iSlaveID.ToString());
                        mIniFile.WriteValue("mLstAIOCardInfo_" + i.ToString(), "eStartAi", "DI" + mLstAIOCardInfo[i].eStartAi.ToString());
                        mIniFile.WriteValue("mLstAIOCardInfo_" + i.ToString(), "eStartAo", "DO" + mLstAIOCardInfo[i].eStartAo.ToString());
                        mIniFile.WriteValue("mLstAIOCardInfo_" + i.ToString(), "eCardType", mLstAIOCardInfo[i].eCardType.ToString());
                    }
                    #region/寫入AxisSetting
                    {
                        Dictionary<clsEnum.enuAxis, clsAxisSetting> mDicAxisSetting = new Dictionary<clsEnum.enuAxis, clsAxisSetting>();
                        for (int i = 0; i < mLstMotionCardInfo.Count; i++)
                        {
                            clsMotionCardSetup pSetup = mLstMotionCardInfo[i];
                            for (int j = 0; j < pSetup.mLst_AxisSetting.Count; j++)
                            {
                                if (pSetup.mLst_AxisSetting[j].p_enuAxis != null)
                                {
                                    if (mDicAxisSetting.ContainsKey((clsEnum.enuAxis)pSetup.mLst_AxisSetting[j].p_enuAxis) == false)
                                    {
                                        mDicAxisSetting.Add((clsEnum.enuAxis)pSetup.mLst_AxisSetting[j].p_enuAxis, pSetup.mLst_AxisSetting[j]);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                }
                            }
                        }
                        string SaveAxisSettingPath = sPath.Replace("CardInfo.ini", "AxisInfoSetting.ini");
                        ArtSystem.Files.JsonHelper.JsonSerializeToFile(mDicAxisSetting, SaveAxisSettingPath, Encoding.Unicode);
                    }
                    #endregion
                    Load(sPath);
                }
                else
                {
                    formMessageBox.Show("Create Directory Fail.");
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 寫入CardInfo </summary>
        public void Load(string sPath)
        {
            try
            {
                mLstMotionCardInfo.Clear();
                mLstDIOCardInfo.Clear();
                mLstAIOCardInfo.Clear();
                if (System.IO.File.Exists(sPath) == true)
                {
                    sINIPath = sPath;
                    clsIniFile mIniFile = new clsIniFile(sPath, true);
                    string[] SectionName = mIniFile.GetSectionNames();
                    Dictionary<clsEnum.enuAxis, bool> mAxisInfo = new Dictionary<clsEnum.enuAxis, bool>();
                    Dictionary<clsEnum.enuAxis, clsAxisSetting> mDicAxisSetting = new Dictionary<clsEnum.enuAxis, clsAxisSetting>();
                    for (int i = 0; i < SectionName.Length; i++)
                    {
                        if (SectionName[i].Contains("mLstMotionCardInfo_") == true)
                        {
                            #region//mLstMotionCardInfo
                            int SectionID = Convert.ToInt32(SectionName[i].Split('_')[1]);
                            if (mLstMotionCardInfo.Count <= SectionID)
                            {
                                for (int j = mLstMotionCardInfo.Count; j < SectionID + 1; j++)
                                {
                                    mLstMotionCardInfo.Add(new clsMotionCardSetup());
                                }
                            }
                            mLstMotionCardInfo[SectionID].eMotionCard = (clsMultiSystem.enuMotionCard)Enum.Parse(typeof(clsMultiSystem.enuMotionCard),
                                mIniFile.GetString("mLstMotionCardInfo_" + SectionID.ToString(), "eMotionCard", mLstMotionCardInfo[SectionID].eMotionCard.ToString()), true);
                            mLstMotionCardInfo[SectionID].eSlaveType = (enuSlaveType_Motion)Enum.Parse(typeof(enuSlaveType_Motion),
                                mIniFile.GetString("mLstMotionCardInfo_" + SectionID.ToString(), "eSlaveType", mLstMotionCardInfo[SectionID].eSlaveType.ToString()), true);
                            mLstMotionCardInfo[SectionID].iCardID = mIniFile.GetInt32("mLstMotionCardInfo_" + SectionID.ToString(), "iCardID", mLstMotionCardInfo[SectionID].iCardID);
                            mLstMotionCardInfo[SectionID].iSlaveID = mIniFile.GetInt32("mLstMotionCardInfo_" + SectionID.ToString(), "iSlaveID", mLstMotionCardInfo[SectionID].iSlaveID);
                            mLstMotionCardInfo[SectionID].iETEL_SlaveNum = mIniFile.GetInt32("mLstMotionCardInfo_" + SectionID.ToString(), "iETEL_SlaveNum", mLstMotionCardInfo[SectionID].iETEL_SlaveNum);//@1.0.0.16-7@
                            mLstMotionCardInfo[SectionID].bSimulate = mIniFile.GetString("mLstMotionCardInfo_" + SectionID.ToString(), "bSimulate", "0") == "1";//@1.0.0.16-7@

                            #region//LstGantryAxis (Etel)
                            string sGantryAxis = mIniFile.GetString("mLstMotionCardInfo_" + SectionID.ToString(), "LstGantryAxis", "");
                            if (sGantryAxis != "")
                            {
                                List<string> sLstGantryAxis = sGantryAxis.Split(',').ToList<string>();
                                mLstMotionCardInfo[i].LstGantryAxis.Clear();
                                for (int iGantryCard = 0; iGantryCard < sLstGantryAxis.Count; iGantryCard++)
                                {
                                    try
                                    {
                                        mLstMotionCardInfo[i].LstGantryAxis.Add(Convert.ToInt32(sLstGantryAxis[iGantryCard]));
                                    }
                                    catch (Exception ex)
                                    {
                                        clsArtSystem.CatchLog(ex);
                                    }
                                }
                            }
                            #endregion
                            #region//Card7856Pmt
                            {
                                string SaveCardPmtPath = mIniFile.GetString("mLstMotionCardInfo_" + SectionID.ToString(), "clsMotionCardPmt_PCI7856", "");
                                SaveCardPmtPath = sPath.Replace("CardInfo.ini", "Card7856Pmt_" + i + ".ini");
                                mLstMotionCardInfo[i].mCartPmt_7856.Clear();
                                if (System.IO.File.Exists(SaveCardPmtPath) == true)
                                {
                                    List<clsMotionCardPmt_PCI7856> JsonData = ArtSystem.Files.JsonHelper.JsonDeserializeFromFile<List<clsMotionCardPmt_PCI7856>>(SaveCardPmtPath, Encoding.Unicode);
                                    if (JsonData != null)
                                    {
                                        mLstMotionCardInfo[i].mCartPmt_7856 = JsonData;
                                    }
                                }
                            }
                            #endregion
                            #region//CardTPMPmt
                            {
                                string SaveCardPmtPath = mIniFile.GetString("mLstMotionCardInfo_" + SectionID.ToString(), "clsMotionCardPmt_TPM", "");
                                SaveCardPmtPath = sPath.Replace("CardInfo.ini", "CardTPMPmt_" + i + ".ini");
                                mLstMotionCardInfo[i].mCartPmt_TPM.Clear();
                                if (System.IO.File.Exists(SaveCardPmtPath) == true)
                                {
                                    List<clsMotionCardPmt_TPM> JsonData = ArtSystem.Files.JsonHelper.JsonDeserializeFromFile<List<clsMotionCardPmt_TPM>>(SaveCardPmtPath, Encoding.Unicode);
                                    if (JsonData != null)
                                    {
                                        mLstMotionCardInfo[i].mCartPmt_TPM = JsonData;
                                    }
                                }
                            }
                            #endregion
                            #region//AdvEtherCAT
                            {
                                string SaveCardPmtPath = mIniFile.GetString("mLstMotionCardInfo_" + SectionID.ToString(), "clsMotionCardPmt_AdvEtherCAT", "");
                                SaveCardPmtPath = sPath.Replace("CardInfo.ini", "CardEtherCatMasterAdvPmt_" + i + ".ini");
                                mLstMotionCardInfo[i].mCartPmt_AdvEtherCAT.Clear();
                                if (System.IO.File.Exists(SaveCardPmtPath) == true)
                                {
                                    List<clsMotionCardPmt_AdvEtherCAT> JsonData = ArtSystem.Files.JsonHelper.JsonDeserializeFromFile<List<clsMotionCardPmt_AdvEtherCAT>>(SaveCardPmtPath, Encoding.Unicode);
                                    if (JsonData != null)
                                    {
                                        mLstMotionCardInfo[i].mCartPmt_AdvEtherCAT = JsonData;
                                    }
                                }
                            }
                            #endregion
                            #endregion

                            #region//計算軸ID
                            {
                                clsMotionCardSetup pSetup = mLstMotionCardInfo[SectionID];
                                pSetup.eStartAxis = clsMultiSystem.SetAxisInfo(ref pSetup, ref mDicAxisSetting);
                            }
                            #endregion
                        }
                        if (SectionName[i].Contains("mLstDIOCardInfo") == true)
                        {
                            #region//mLstDIOCardInfo
                            int SectionID = Convert.ToInt32(SectionName[i].Split('_')[1]);
                            if (mLstDIOCardInfo.Count <= SectionID)
                            {
                                for (int j = mLstDIOCardInfo.Count; j < SectionID + 1; j++)
                                {
                                    mLstDIOCardInfo.Add(new clsDIOCardSetup());
                                }
                            }
                            mLstDIOCardInfo[SectionID].eSysCard = (clsMultiSystem.enuMotionCard)Enum.Parse(typeof(clsMultiSystem.enuMotionCard),
                                mIniFile.GetString("mLstDIOCardInfo_" + SectionID.ToString(), "eSysCard", mLstDIOCardInfo[SectionID].eSysCard.ToString()), true);
                            mLstDIOCardInfo[SectionID].eDIOType = (clsDIOCardSetup.enuDIOType)Enum.Parse(typeof(clsDIOCardSetup.enuDIOType),
                                mIniFile.GetString("mLstDIOCardInfo_" + SectionID.ToString(), "eDIOType", mLstDIOCardInfo[SectionID].eDIOType.ToString()), true);
                            mLstDIOCardInfo[SectionID].eStartDi = Convert.ToInt32(mIniFile.GetString("mLstDIOCardInfo_" + SectionID.ToString(), "eStartDi", mLstDIOCardInfo[SectionID].eStartDi.ToString()).Replace("DI", ""));
                            mLstDIOCardInfo[SectionID].eStartDo = Convert.ToInt32(mIniFile.GetString("mLstDIOCardInfo_" + SectionID.ToString(), "eStartDo", mLstDIOCardInfo[SectionID].eStartDo.ToString()).Replace("DO", ""));
                            mLstDIOCardInfo[SectionID].iCardID = mIniFile.GetInt32("mLstDIOCardInfo_" + SectionID.ToString(), "iCardID", mLstDIOCardInfo[SectionID].iCardID);
                            mLstDIOCardInfo[SectionID].iSlaveID = mIniFile.GetInt32("mLstDIOCardInfo_" + SectionID.ToString(), "iSlaveID", mLstDIOCardInfo[SectionID].iSlaveID);
                            mLstDIOCardInfo[SectionID].bSimulate = mIniFile.GetInt32("mLstDIOCardInfo_" + SectionID.ToString(), "bSimulate", mLstDIOCardInfo[SectionID].bSimulate ? 1 : 0) == 1;

                            #endregion
                        }
                        if (SectionName[i].Contains("mLstAIOCardInfo") == true)
                        {
                            #region//mLstAIOCardInfo
                            int SectionID = Convert.ToInt32(SectionName[i].Split('_')[1]);
                            if (mLstAIOCardInfo.Count <= SectionID)
                            {
                                for (int j = mLstAIOCardInfo.Count; j < SectionID + 1; j++)
                                {
                                    mLstAIOCardInfo.Add(new clsAIOCardSetup());
                                }
                            }
                            string sCardType = mIniFile.GetString("mLstAIOCardInfo_" + SectionID.ToString(), "eCardType", mLstAIOCardInfo[SectionID].eCardType.ToString());
                            #region//新舊版相融問題, 移除 (PCI9112) 改用 (PCI9112_2AO_8AI 和 PCI9112_2AO_16AI)
                            if (sCardType == "PCI9112")
                            {
                                mIniFile.GetInt32("mLstAIOCardInfo_" + SectionID.ToString(), "iAOCount", mLstAIOCardInfo[SectionID].iAOCount);
                                if (mIniFile.GetInt32("mLstAIOCardInfo_" + SectionID.ToString(), "iAICount", mLstAIOCardInfo[SectionID].iAICount) > 10)
                                {
                                    sCardType = clsAIOCardSetup.enuCardType.PCI9112_2AO_16AI.ToString();
                                }
                                else
                                {
                                    sCardType = clsAIOCardSetup.enuCardType.PCI9112_2AO_8AI.ToString();
                                }
                            }
                            #endregion
                            mLstAIOCardInfo[SectionID].eCardType = (clsAIOCardSetup.enuCardType)Enum.Parse(typeof(clsAIOCardSetup.enuCardType), sCardType, true);
                            mLstAIOCardInfo[SectionID].eStartAi = Convert.ToInt32(mIniFile.GetString("mLstAIOCardInfo_" + SectionID.ToString(), "eStartAi", mLstAIOCardInfo[SectionID].eStartAi.ToString()).Replace("DI", ""));
                            mLstAIOCardInfo[SectionID].eStartAo = Convert.ToInt32(mIniFile.GetString("mLstAIOCardInfo_" + SectionID.ToString(), "eStartAo", mLstAIOCardInfo[SectionID].eStartAo.ToString()).Replace("DO", ""));
                            mLstAIOCardInfo[SectionID].iAICount = mIniFile.GetInt32("mLstAIOCardInfo_" + SectionID.ToString(), "iAICount", mLstAIOCardInfo[SectionID].iAICount);
                            mLstAIOCardInfo[SectionID].iAOCount = mIniFile.GetInt32("mLstAIOCardInfo_" + SectionID.ToString(), "iAOCount", mLstAIOCardInfo[SectionID].iAOCount);
                            mLstAIOCardInfo[SectionID].iCardID = mIniFile.GetInt32("mLstAIOCardInfo_" + SectionID.ToString(), "iCardID", mLstAIOCardInfo[SectionID].iCardID);
                            mLstAIOCardInfo[SectionID].iSlaveID = mIniFile.GetInt32("mLstAIOCardInfo_" + SectionID.ToString(), "iSlaveID", mLstAIOCardInfo[SectionID].iSlaveID);
                            switch (mLstAIOCardInfo[SectionID].eCardType)
                            {
                                case clsAIOCardSetup.enuCardType.PCI9112_2AO_8AI:
                                case clsAIOCardSetup.enuCardType.PCI1203_8AI2AO:
                                    mLstAIOCardInfo[SectionID].iAICount = 8;
                                    mLstAIOCardInfo[SectionID].iAOCount = 2;
                                    break;
                                case clsAIOCardSetup.enuCardType.PCI9112_2AO_16AI:
                                    mLstAIOCardInfo[SectionID].iAICount = 16;
                                    mLstAIOCardInfo[SectionID].iAOCount = 2;
                                    break;
                                case clsAIOCardSetup.enuCardType.PCI1203_8AI:
                                    mLstAIOCardInfo[SectionID].iAICount = 8;
                                    mLstAIOCardInfo[SectionID].iAOCount = 0;
                                    break;
                                case clsAIOCardSetup.enuCardType.PCI1203_4AO:
                                    mLstAIOCardInfo[SectionID].iAICount = 0;
                                    mLstAIOCardInfo[SectionID].iAOCount = 4;
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                        }
                    }
                    #region//載入AxisSetting
                    {
                        string SaveAxisSettingPath = sPath.Replace("CardInfo.ini", "AxisInfoSetting.ini");
                        Dictionary<clsEnum.enuAxis, clsAxisSetting> JsonData = ArtSystem.Files.JsonHelper.JsonDeserializeFromFile<Dictionary<clsEnum.enuAxis, clsAxisSetting>>(SaveAxisSettingPath, Encoding.Unicode);
                        if (JsonData != null)
                        {
                            clsClassFunc.Copy(JsonData, mDicAxisSetting);
                        }
                    }
                    #endregion
                }
                else
                {
                    sINIPath = sPath;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
    }
}

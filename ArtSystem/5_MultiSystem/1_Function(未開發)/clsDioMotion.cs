using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using System.Text.RegularExpressions;

namespace ArtSystem.MultiSystem
{
    public class clsDioMotion
    {

        /// <summary> 軸動資訊 </summary>
        static public Dictionary<clsEnum.enuAxis, clsAxisSetting> mDic_AxisInfo = new Dictionary<clsEnum.enuAxis, clsAxisSetting>();
        static public List<clsEnum.enuDi> mLst_DIEnum = new List<clsEnum.enuDi>();
        static public List<clsEnum.enuDo> mLst_DOEnum = new List<clsEnum.enuDo>();
        static public List<clsEnum.enuDi> mLst_AIEnum = new List<clsEnum.enuDi>();
        static public List<clsEnum.enuDo> mLst_AOEnum = new List<clsEnum.enuDo>();



        static private string DicDiName_Source = "";
        static private Dictionary<clsEnum.enuDi, string> DicDiName = new Dictionary<clsEnum.enuDi, string>();
        static public Dictionary<clsEnum.enuDi, string> GetDiName()
        {
            string strDoSectionName = "DiName" + clsCmData.g_strLanguageType;
            try
            {
                string m_strDioNameIniFilePath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\" + "artDioName.ini";
                if (DicDiName_Source != m_strDioNameIniFilePath || DicDiName.Count == 0)
                {
                    if (System.IO.File.Exists(m_strDioNameIniFilePath) == true)
                    {
                        clsIniFile iniDioName = new clsIniFile(m_strDioNameIniFilePath);
                        Dictionary<string, string> dctDioData = iniDioName.GetSectionValues(strDoSectionName);
                        if (dctDioData != null)
                        {
                            foreach (string sKey in dctDioData.Keys)
                            {
                                //string sDIO_ID = sKey.Replace("DO", "").Replace("DI", "").Replace(":", "");
                                string sDIO_ID = Regex.Match(sKey, @"\d+").Value;
                                if (sDIO_ID != null && sDIO_ID != "")
                                {
                                    int iDIO_ID = Int32.Parse(sDIO_ID);
                                    if (Enum.IsDefined(typeof(clsEnum.enuDi), iDIO_ID) == true)
                                    {
                                        clsEnum.enuDi eDi = (clsEnum.enuDi)iDIO_ID;
                                        if (DicDiName.ContainsKey(eDi) == false)
                                        {
                                            DicDiName.Add(eDi, dctDioData[sKey]);
                                        }
                                    }
                                }
                            }
                        }
                        DicDiName_Source = m_strDioNameIniFilePath;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return DicDiName;
        }

        static private string DicDoName_Source = "";
        static private Dictionary<clsEnum.enuDo, string> DicDoName = new Dictionary<clsEnum.enuDo, string>();
        static public Dictionary<clsEnum.enuDo, string> GetDoName()
        {
            string strDoSectionName = "DoName" + clsCmData.g_strLanguageType;
            try
            {
                string m_strDioNameIniFilePath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\" + "artDioName.ini";
                if (DicDoName_Source != m_strDioNameIniFilePath || DicDoName.Count == 0)
                {
                    if (System.IO.File.Exists(m_strDioNameIniFilePath) == true)
                    {
                        clsIniFile iniDioName = new clsIniFile(m_strDioNameIniFilePath);
                        Dictionary<string, string> dctDioData = iniDioName.GetSectionValues(strDoSectionName);
                        if (dctDioData != null)
                        {
                            foreach (string sKey in dctDioData.Keys)
                            {
                                //string sDIO_ID = sKey.Replace("DO", "").Replace("DI", "").Replace(":", "");
                                string sDIO_ID = Regex.Match(sKey, @"\d+").Value;
                                if (sDIO_ID != null && sDIO_ID != "")
                                {
                                    int iDIO_ID = Int32.Parse(sDIO_ID);
                                    if (Enum.IsDefined(typeof(clsEnum.enuDi), iDIO_ID) == true)
                                    {
                                        clsEnum.enuDo eDo = (clsEnum.enuDo)iDIO_ID;
                                        if (DicDoName.ContainsKey(eDo) == false)
                                        {
                                            DicDoName.Add(eDo, dctDioData[sKey]);
                                        }
                                    }
                                }
                            }
                        }
                        DicDoName_Source = m_strDioNameIniFilePath;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return DicDoName;
        }
        static public Dictionary<clsEnum.enuDi, string> GetDiName_EN()
        {
            Dictionary<clsEnum.enuDi, string> rValue = new Dictionary<clsEnum.enuDi, string>();
            string strDoSectionName = "DiNameEN";
            try
            {
                string m_strDioNameIniFilePath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\" + "artDioName.ini";
                if (System.IO.File.Exists(m_strDioNameIniFilePath) == true)
                {
                    clsIniFile iniDioName = new clsIniFile(m_strDioNameIniFilePath);
                    Dictionary<string, string> dctDioData = iniDioName.GetSectionValues(strDoSectionName);
                    if (dctDioData != null)
                    {
                        foreach (string sKey in dctDioData.Keys)
                        {
                            //string sDIO_ID = sKey.Replace("DO", "").Replace("DI", "").Replace(":", "");
                            string sDIO_ID = Regex.Match(sKey, @"\d+").Value;
                            if (sDIO_ID != null && sDIO_ID != "")
                            {
                                int iDIO_ID = Int32.Parse(sDIO_ID);
                                if (Enum.IsDefined(typeof(clsEnum.enuDi), iDIO_ID) == true)
                                {
                                    clsEnum.enuDi eDi = (clsEnum.enuDi)iDIO_ID;
                                    if (rValue.ContainsKey(eDi) == false)
                                    {
                                        rValue.Add(eDi, dctDioData[sKey]);
                                    }
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
            return rValue;
        }

        static public Dictionary<clsEnum.enuDo, string> GetDoName_EN()
        {
            Dictionary<clsEnum.enuDo, string> rValue = new Dictionary<clsEnum.enuDo, string>();
            string strDoSectionName = "DoNameEN";
            try
            {
                string m_strDioNameIniFilePath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\" + "artDioName.ini";
                if (System.IO.File.Exists(m_strDioNameIniFilePath) == true)
                {
                    clsIniFile iniDioName = new clsIniFile(m_strDioNameIniFilePath);
                    Dictionary<string, string> dctDioData = iniDioName.GetSectionValues(strDoSectionName);
                    if (dctDioData != null)
                    {
                        foreach (string sKey in dctDioData.Keys)
                        {
                            string sDIO_ID = Regex.Match(sKey, @"\d+").Value;
                            if (sDIO_ID != null && sDIO_ID != "")
                            {
                                int iDIO_ID = Int32.Parse(sDIO_ID);
                                if (Enum.IsDefined(typeof(clsEnum.enuDi), iDIO_ID) == true)
                                {
                                    clsEnum.enuDo eDo = (clsEnum.enuDo)iDIO_ID;
                                    if (rValue.ContainsKey(eDo) == false)
                                    {
                                        rValue.Add(eDo, dctDioData[sKey]);
                                    }
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
            return rValue;
        }

        static public Dictionary<clsEnum.enuDi, string> GetDiName_TC()
        {
            Dictionary<clsEnum.enuDi, string> rValue = new Dictionary<clsEnum.enuDi, string>();
            string strDoSectionName = "DiNameTC";
            try
            {
                string m_strDioNameIniFilePath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\" + "artDioName.ini";
                if (System.IO.File.Exists(m_strDioNameIniFilePath) == true)
                {
                    clsIniFile iniDioName = new clsIniFile(m_strDioNameIniFilePath);
                    Dictionary<string, string> dctDioData = iniDioName.GetSectionValues(strDoSectionName);
                    if (dctDioData != null)
                    {
                        foreach (string sKey in dctDioData.Keys)
                        {
                            //string sDIO_ID = sKey.Replace("DO", "").Replace("DI", "").Replace(":", "");
                            string sDIO_ID = Regex.Match(sKey, @"\d+").Value;
                            if (sDIO_ID != null && sDIO_ID != "")
                            {
                                int iDIO_ID = Int32.Parse(sDIO_ID);
                                if (Enum.IsDefined(typeof(clsEnum.enuDi), iDIO_ID) == true)
                                {
                                    clsEnum.enuDi eDi = (clsEnum.enuDi)iDIO_ID;
                                    if (rValue.ContainsKey(eDi) == false)
                                    {
                                        rValue.Add(eDi, dctDioData[sKey]);
                                    }
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
            return rValue;
        }

        static public Dictionary<clsEnum.enuDo, string> GetDoName_TC()
        {
            Dictionary<clsEnum.enuDo, string> rValue = new Dictionary<clsEnum.enuDo, string>();
            string strDoSectionName = "DoNameTC";
            try
            {
                string m_strDioNameIniFilePath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\" + "artDioName.ini";
                if (System.IO.File.Exists(m_strDioNameIniFilePath) == true)
                {
                    clsIniFile iniDioName = new clsIniFile(m_strDioNameIniFilePath);
                    Dictionary<string, string> dctDioData = iniDioName.GetSectionValues(strDoSectionName);
                    if (dctDioData != null)
                    {
                        foreach (string sKey in dctDioData.Keys)
                        {
                            string sDIO_ID = Regex.Match(sKey, @"\d+").Value;
                            if (sDIO_ID != null && sDIO_ID != "")
                            {
                                int iDIO_ID = Int32.Parse(sDIO_ID);
                                if (Enum.IsDefined(typeof(clsEnum.enuDi), iDIO_ID) == true)
                                {
                                    clsEnum.enuDo eDo = (clsEnum.enuDo)iDIO_ID;
                                    if (rValue.ContainsKey(eDo) == false)
                                    {
                                        rValue.Add(eDo, dctDioData[sKey]);
                                    }
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
            return rValue;
        }

        static public clsAxisSetting GetAxisSetting(clsEnum.enuAxis eAxisID)
        {
            clsAxisSetting rValue = null;
            if (mDic_AxisInfo.ContainsKey(eAxisID) == true)
            {
                rValue = mDic_AxisInfo[eAxisID];
            }
            return rValue;
        }
        static public void SetAllSpeed()
        {
            foreach (clsEnum.enuAxis eAxis in mDic_AxisInfo.Keys)
            {
                ucMotionSetting.SetAxisSpeed(eAxis);
            }
        }


        static public string GetString_Di(clsEnum.enuDi? p_eDI, Dictionary<clsEnum.enuDi, string> p_DicDiName)
        {
            string rValue = "";
            if (p_eDI != null)
            {
                rValue = p_eDI.ToString();
                if (p_DicDiName.ContainsKey((clsEnum.enuDi)p_eDI) == true)
                {
                    rValue += " (" + p_DicDiName[(clsEnum.enuDi)p_eDI] + ")";
                }
            }
            return rValue;
        }

        static public string GetString_Do(clsEnum.enuDo? p_eDO, Dictionary<clsEnum.enuDo, string> p_DicDoName)
        {
            string rValue = "";
            if (p_eDO != null)
            {
                rValue = p_eDO.ToString();
                if (p_DicDoName.ContainsKey((clsEnum.enuDo)p_eDO) == true)
                {
                    rValue += " (" + p_DicDoName[(clsEnum.enuDo)p_eDO] + ")";
                }
            }
            return rValue;
        }





    }
}

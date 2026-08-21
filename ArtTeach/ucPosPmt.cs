using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtTeach
{

    public partial class ucPosPmt : ArtCommonLib.ucBaseUserControl
    //class ucPosition
    {
        #region //=====================  區域變數設置 =====================

        ///<summary> Position 參數的文件路徑 </summary>
        static private string ParameterPath = "";

        ///<summary> 收集參數 </summary>
        static private clsDictionary<string, string> m_dctDataLib = new clsDictionary<string, string>();
        static private clsDictionary<clsEnum.enuPosName, double> m_dctPosLibDouble = new clsDictionary<clsEnum.enuPosName, double>();
        static private clsDictionary<clsEnum.enuPosName, decimal> m_dctPosLibDecimal = new clsDictionary<clsEnum.enuPosName, decimal>();
        static private clsDictionary<clsEnum.enuPosName, string> m__dctPosLibString = new clsDictionary<clsEnum.enuPosName, string>();

        #endregion

        #region //===================== public 函式設置 =====================

        /// <summary> 設置序列中指定的值 </summary>
        /// <param name="p_Name">索引值</param>
        /// <param name="p_dimValue">設置值</param>
        static public void SetValue(clsEnum.enuPosName p_Name, string p_strValue)
        {
            CheckParameterPath();
            string strName = p_Name.ToString();

            try
            {
                if (m_dctDataLib.ContainsKey(strName))
                {
                    m_dctDataLib[strName] = p_strValue;
                }
                else
                {
                    m_dctDataLib.Add(strName, p_strValue);
                }

                //string
                if (m__dctPosLibString.ContainsKey(p_Name))
                {
                    m__dctPosLibString[p_Name] = p_strValue;
                }
                else
                {
                    m__dctPosLibString.Add(p_Name, p_strValue);
                }


                double dTemp = 0;
                try
                {
                    dTemp = Convert.ToDouble(p_strValue);
                }
                catch { }

                //double
                if (m_dctPosLibDouble.ContainsKey(p_Name))
                {
                    m_dctPosLibDouble[p_Name] = dTemp;
                }
                else
                {
                    m_dctPosLibDouble.Add(p_Name, dTemp);
                }

                //decimal
                if (m_dctPosLibDecimal.ContainsKey(p_Name))
                {
                    m_dctPosLibDecimal[p_Name] = (decimal)dTemp;
                }
                else
                {
                    m_dctPosLibDecimal.Add(p_Name, (decimal)dTemp);
                }

            }
            catch { }
        }

        /// <summary> 傳回序列中指定的 Decimal </summary>
        /// <param name="p_Name">索引值</param>
        /// <returns>回傳值</returns>
        static public decimal GetValue(clsEnum.enuPosName p_Name)
        {
            CheckParameterPath();
            if (m_dctPosLibDecimal.ContainsKey(p_Name))
            {
                return m_dctPosLibDecimal[p_Name];
            }
            else
            {
                return 0;
            }

        }

        /// <summary> 傳回序列中指定的 String </summary>
        /// <param name="p_Name">索引值</param>
        /// <returns>回傳值</returns>
        static public string GetValueString(clsEnum.enuPosName p_Name)
        {
            CheckParameterPath();
            if (m__dctPosLibString.ContainsKey(p_Name))
            {
                return m__dctPosLibString[p_Name];
            }
            else
            {
                return "";
            }
        }

        /// <summary> 傳回序列中指定的 Double </summary>
        /// <param name="p_Name">索引值</param>
        /// <returns>回傳值</returns>
        static public double GetValueDouble(clsEnum.enuPosName p_Name)
        {
            CheckParameterPath();
            try
            {
                if (m_dctPosLibDouble.ContainsKey(p_Name))
                {
                    return m_dctPosLibDouble[p_Name];
                }
            }
            catch { }

            return 0;
        }

        /// <summary> 設置序列中指定的值 </summary>
        /// <param name="p_Name">索引值</param>
        /// <param name="p_dimValue">設置值</param>
        static public void SaveValue(clsEnum.enuPosName p_Name, string p_strValue)
        {
            CheckParameterPath();
            SetValue(p_Name, p_strValue);
            SaveIni(p_Name, p_strValue);
        }

        /// <summary> 載入 Ini File</summary>
        /// 
        static public void LoadIniFile()
        {
            m_dctPosLibDouble.Clear();
            m_dctPosLibDecimal.Clear();
            m__dctPosLibString.Clear();
            m_dctDataLib.Clear();

            clsDictionary<string, string> dctDataTemp = new clsDictionary<string, string>();
            if (System.IO.File.Exists(GetFilePath()) == true)
            {
                clsIniFile IniFile = new clsIniFile(GetFilePath());
                dctDataTemp.Dictionary = IniFile.GetSectionValues(clsEnum.enuPmtType.TeachPos.ToString());

                string[] astrPmtName = Enum.GetNames(typeof(clsEnum.enuPosName));

                Parallel.ForEach(dctDataTemp.Keys, strKey =>
                {
                    try
                    {
                        if (astrPmtName.Contains(strKey))
                        {
                            if (!m_dctDataLib.ContainsKey(strKey))
                            {
                                m_dctDataLib.Add(strKey, "0");
                            }
                            string strValue = dctDataTemp[strKey];
                            m_dctDataLib[strKey] = strValue;
                        }
                    }
                    catch
                    {
                        formMessageBox.Show("Load Pameter Error:" + "clsEnum.enuPosName" + "-" + strKey);
                    }
                });
            }
            var enumTypes = typeof(clsEnum).GetNestedTypes(BindingFlags.Public);

            string[] astrKey = m_dctDataLib.Keys.ToArray();
            Parallel.ForEach(astrKey, strName =>
            {
                try
                {
                    clsEnum.enuPosName m_enukey;
                    Enum.TryParse(strName, true, out m_enukey);

                    if (m__dctPosLibString.ContainsKey(m_enukey))
                    {
                        m__dctPosLibString[m_enukey] = m_dctDataLib[strName];
                    }
                    else
                    {
                        m__dctPosLibString.Add(m_enukey, m_dctDataLib[strName]);
                    }


                    double dTemp = 0;
                    try
                    {
                        dTemp = Convert.ToDouble(m_dctDataLib[strName]);
                    }
                    catch { }

                    //Set Double Lib
                    if (m_dctPosLibDouble.ContainsKey(m_enukey))
                    {
                        m_dctPosLibDouble[m_enukey] = (double)dTemp;
                    }
                    else
                    {
                        m_dctPosLibDouble.Add(m_enukey, (double)dTemp);
                    }

                    //Set Decimal Lib
                    if (m_dctPosLibDecimal.ContainsKey(m_enukey))
                    {
                        try
                        {
                            m_dctPosLibDecimal[m_enukey] = (decimal)dTemp;
                        }
                        catch
                        {
                            m_dctPosLibDecimal[m_enukey] = 0;
                        }
                    }
                    else
                    {
                        try
                        {
                            m_dctPosLibDecimal.Add(m_enukey, (decimal)dTemp);
                        }
                        catch
                        {
                            m_dctPosLibDecimal.Add(m_enukey, 0);
                        }
                    }
                }
                catch (Exception)
                {
                    formMessageBox.Show("Load Pameter Error:" + strName + "-" + m_dctDataLib[strName]);
                }
            });
        }

        /// <summary> 載入 Ini File</summary>
        /// 
        static public void SaveIniFile()
        {
            clsIniFile IniDataInfo;

            try
            {
                string[] astrKey = m_dctDataLib.Keys.ToArray();
                Parallel.ForEach(astrKey, strName =>
                {
                    IniDataInfo = new clsIniFile(ucParameter.GetFilePath(clsEnum.enuPmtType.TeachPos.ToString()));
                    IniDataInfo.WriteValue(clsEnum.enuPmtType.TeachPos.ToString(), strName.ToString(), m_dctDataLib[strName]);
                });
            }
            catch (Exception)
            {
                formMessageBox.Show("The Position INI File Save Error!!");
            }
        }

        /// <summary> 傳回序列中指定種類名稱的檔案路徑 </summary>
        /// <param name="p_strTypeName">索引值</param>
        /// <returns>回傳值</returns>
        public static string GetFilePath()
        {
            return ucParameter.GetFilePath(clsEnum.enuPmtType.TeachPos);
        }


        #endregion

        #region //===================== private 函式設置 =====================

        static private void SaveIni(clsEnum.enuPosName p_Name, string p_strValue)
        {
            clsIniFile IniDataInfo;

            //Save Date
            try
            {
                IniDataInfo = new clsIniFile(ucParameter.GetFilePath(clsEnum.enuPmtType.TeachPos.ToString()));
                IniDataInfo.WriteValue(clsEnum.enuPmtType.TeachPos.ToString(), p_Name.ToString(), p_strValue);
            }
            catch { }
        }

        static private void CheckParameterPath()
        {
            if (System.IO.File.Exists(GetFilePath()) == true)
            {
                if (ParameterPath != GetFilePath())
                {
                    ParameterPath = GetFilePath();
                    LoadIniFile();
                }
            }
        }

        #endregion

        private void InitializeComponent()
        {
            ArtTeach.clsDataJogTeach clsJogTeachInfo1 = new ArtTeach.clsDataJogTeach();
            this.SuspendLayout();
            // 
            // ucPosPmt
            // 
            this.Name = "ucPosPmt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}

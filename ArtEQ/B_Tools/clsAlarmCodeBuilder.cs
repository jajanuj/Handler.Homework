using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtData;
using ArtCommonLib;
using ArtControlLib;
using System.Reflection;
using System.ComponentModel;

namespace ArtEQ
{
    public class clsAlarmCodeBuilder
    {
        /// <summary>
        /// clsEnum.enuAlarm內的int異常代碼，將未定義的AlarmCode自動生成到...\\Bin\\Debug\\INI\\AlarmList.ini內。
        /// 已經定義過的alarmcode是不會複寫的,如果clsEnum.enuAlarm的內容有變更，需要更新AlarmList.ini，請把檔案刪除後重新自動生成。
        /// </summary>
        static public void AlarmCodeBuilder()
        {
            try
            {
                Dictionary<string, AlarmCodeInfo> AlarmList_Dictionary = new Dictionary<string, AlarmCodeInfo>();
                string AlarmList_INIPath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\AlarmList.ini";
                if (System.IO.File.Exists(AlarmList_INIPath) == true)
                {
                    #region//載入舊的AlarmList.INI
                    string[] AlarmList_ReadAllLines = System.IO.File.ReadAllLines(AlarmList_INIPath);
                    for (int i = 0; i < AlarmList_ReadAllLines.Length; i++)
                    {
                        try
                        {
                            if (AlarmList_ReadAllLines[i].Contains("[") && AlarmList_ReadAllLines[i].Contains("]"))
                            {
                                int AlarmCode = 0;
                                List<string> AlarmData_TempStringList = new List<string>();
                                AlarmData_TempStringList.Add(AlarmList_ReadAllLines[i]);
                                AlarmCode = Convert.ToInt32(AlarmList_ReadAllLines[i].Replace("[", "").Replace("]", ""));
                                i++;
                                for (; i < AlarmList_ReadAllLines.Length; i++)
                                {
                                    if (AlarmList_ReadAllLines[i].Contains("[") && AlarmList_ReadAllLines[i].Contains("]"))
                                    {
                                        i -= 2;
                                        break;
                                    }
                                    AlarmData_TempStringList.Add(AlarmList_ReadAllLines[i]);
                                }
                                AlarmList_Dictionary.Add(AlarmCode.ToString(), new AlarmCodeInfo(AlarmData_TempStringList));
                            }
                        }
                        catch
                        {
                        }
                    }
                    #endregion
                }
                foreach (clsEnum.enuAlarm Alarm in Enum.GetValues(typeof(clsEnum.enuAlarm)))
                {
                    #region//重新加入enuAlarm裡面的定義.
                    try
                    {
                        string AlarmCode = ((int)Alarm).ToString();
                        if (AlarmList_Dictionary.Keys.Contains(((int)Alarm).ToString()) == false)
                        {
                            AlarmList_Dictionary.Add(AlarmCode, new AlarmCodeInfo((int)Alarm));
                        }
                        try
                        {
                            int AlarmLevel = Convert.ToInt32(((int)Alarm).ToString().Substring(0, 1));
                            if (AlarmLevel == 9)
                            {
                                AlarmList_Dictionary[AlarmCode].Level = 2;//索引 : @AlarmCodeBuilder-LV2@
                            }
                            else
                            {
                                AlarmList_Dictionary[AlarmCode].Level = 1;
                            }
                        }
                        catch
                        {
                        }
                        if (AlarmList_Dictionary[AlarmCode].MessageEN == "" || AlarmList_Dictionary[AlarmCode].MessageEN == null)
                        {
                            AlarmList_Dictionary[AlarmCode].MessageEN = Alarm.ToString().Replace("_", " ");
                        }
                        if (AlarmList_Dictionary[AlarmCode].TroubleShootingEN == "" || AlarmList_Dictionary[AlarmCode].TroubleShootingEN == null)
                        {
                            AlarmList_Dictionary[AlarmCode].TroubleShootingEN = AlarmList_Dictionary[AlarmCode].MessageEN;
                        }
                        if (AlarmList_Dictionary[AlarmCode].MessageTC == "" || AlarmList_Dictionary[AlarmCode].MessageTC == null)
                        {
                            AlarmList_Dictionary[AlarmCode].MessageTC = GetDescription(Alarm);
                        }
                        if (AlarmList_Dictionary[AlarmCode].MessageJP == "" || AlarmList_Dictionary[AlarmCode].MessageJP == null)
                        {
                            AlarmList_Dictionary[AlarmCode].MessageJP = AlarmList_Dictionary[AlarmCode].MessageEN;
                        }
                        if (AlarmList_Dictionary[AlarmCode].TroubleShootingTC == "" || AlarmList_Dictionary[AlarmCode].TroubleShootingTC == null)
                        {
                            AlarmList_Dictionary[AlarmCode].TroubleShootingTC = AlarmList_Dictionary[AlarmCode].MessageTC;
                        }
                        if (AlarmList_Dictionary[AlarmCode].TroubleShootingJP == "" || AlarmList_Dictionary[AlarmCode].TroubleShootingJP == null)
                        {
                            AlarmList_Dictionary[AlarmCode].TroubleShootingJP = AlarmList_Dictionary[AlarmCode].MessageEN;
                        }
                        if (AlarmList_Dictionary[AlarmCode].Continue == 0
                            && AlarmList_Dictionary[AlarmCode].Skip == 0
                            && AlarmList_Dictionary[AlarmCode].Retry == 0
                            && AlarmList_Dictionary[AlarmCode].Reset == 0
                            )
                        {
                            AlarmList_Dictionary[AlarmCode].Reset = 1;
                        }
                    }
                    catch
                    {
                    }
                    #endregion
                }

                #region//重新創健一個新的AlarmList.INI
                List<string> AlarmList_New = new List<string>();
                foreach (string sKey in AlarmList_Dictionary.Keys)
                {
                    AlarmList_New.Add("[" + AlarmList_Dictionary[sKey].Code + "]");
                    AlarmList_New.Add("Level=" + AlarmList_Dictionary[sKey].Level);
                    AlarmList_New.Add("Type=" + AlarmList_Dictionary[sKey].Type);
                    AlarmList_New.Add("Reset=" + AlarmList_Dictionary[sKey].Reset);
                    AlarmList_New.Add("Skip=" + AlarmList_Dictionary[sKey].Skip);
                    AlarmList_New.Add("Retry=" + AlarmList_Dictionary[sKey].Retry);
                    AlarmList_New.Add("Continue=" + AlarmList_Dictionary[sKey].Continue);
                    AlarmList_New.Add("MessageTC=" + AlarmList_Dictionary[sKey].MessageTC);
                    AlarmList_New.Add("TroubleShootingTC=" + AlarmList_Dictionary[sKey].TroubleShootingTC);
                    AlarmList_New.Add("MessageEN=" + AlarmList_Dictionary[sKey].MessageEN);
                    AlarmList_New.Add("TroubleShootingEN=" + AlarmList_Dictionary[sKey].TroubleShootingEN);
                    AlarmList_New.Add("MessageJP=" + AlarmList_Dictionary[sKey].MessageJP);
                    AlarmList_New.Add("TroubleShootingJP=" + AlarmList_Dictionary[sKey].TroubleShootingJP);
                    AlarmList_New.Add("");
                }
                System.IO.File.WriteAllLines(AlarmList_INIPath, AlarmList_New, Encoding.Unicode);
                #endregion

                #region//如果是MultiSystem 將INIcopy到 SystemINI內
                if (ArtSystem.MultiSystem.clsMultiSystem.bIsMultiSystem == true)
                {
                    try
                    {
                        string strMultiSystemAlarmINI_Path = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath;
                        strMultiSystemAlarmINI_Path += "\\AlarmList.ini";
                        if (System.IO.File.Exists(strMultiSystemAlarmINI_Path) == true
                            && System.IO.File.Exists(AlarmList_INIPath) == true)
                        {
                            System.IO.File.Copy(AlarmList_INIPath, strMultiSystemAlarmINI_Path, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
                    }

                }
                #endregion
            }
            catch
            {
            }
        }

        static private string GetDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        private class AlarmCodeInfo
        {
            public int Code = 0;
            /// <summary> Level 2 = (Down) </summary>
            public int Level = 1;
            public int Type = 0;
            public int Reset = 0;
            public int Skip = 0;
            public int Retry = 0;
            public int Continue = 0;
            public string MessageTC = "";
            public string TroubleShootingTC = "";
            public string MessageEN = "";
            public string TroubleShootingEN = "";
            public string MessageJP = "";
            public string TroubleShootingJP = "";

            public AlarmCodeInfo(int p_Code)
            {
                Code = p_Code;
            }
            public AlarmCodeInfo(List<string> InputString)
            {
                foreach (string Data in InputString)
                {
                    if (Data.Contains("[") && Data.Contains("]"))
                    {
                        Code = Convert.ToInt32(Data.Replace("[", "").Replace("]", ""));
                    }
                    else if (Data.Contains("Level="))
                    {
                        Level = Convert.ToInt32(Data.Replace("Level=", ""));
                    }
                    else if (Data.Contains("Type="))
                    {
                        Type = Convert.ToInt32(Data.Replace("Type=", ""));
                    }
                    else if (Data.Contains("Reset="))
                    {
                        Reset = Convert.ToInt32(Data.Replace("Reset=", ""));
                    }
                    else if (Data.Contains("Skip="))
                    {
                        Skip = Convert.ToInt32(Data.Replace("Skip=", ""));
                    }
                    else if (Data.Contains("Retry="))
                    {
                        Retry = Convert.ToInt32(Data.Replace("Retry=", ""));
                    }
                    else if (Data.Contains("Continue="))
                    {
                        Continue = Convert.ToInt32(Data.Replace("Continue=", ""));
                    }
                    else if (Data.Contains("MessageTC="))
                    {
                        MessageTC = Data.Replace("MessageTC=", "");
                    }
                    else if (Data.Contains("TroubleShootingTC="))
                    {
                        TroubleShootingTC = Data.Replace("TroubleShootingTC=", "");
                    }
                    else if (Data.Contains("MessageEN="))
                    {
                        MessageEN = Data.Replace("MessageEN=", "");
                    }
                    else if (Data.Contains("TroubleShootingEN="))
                    {
                        TroubleShootingEN = Data.Replace("TroubleShootingEN=", "");
                    }
                    else if (Data.Contains("MessageJP="))
                    {
                        MessageJP = Data.Replace("MessageJP=", "");
                    }
                    else if (Data.Contains("TroubleShootingJP="))
                    {
                        TroubleShootingJP = Data.Replace("TroubleShootingJP=", "");
                    }
                }
            }
        }
    }
}

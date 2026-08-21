using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace ArtTeach
{
    /// <summary>
    /// Class 的基本功能 (目前不能提供Array , Dictonary 功能) List可以, //@1.0.0.6-13
    /// </summary>
    [Serializable]
    public class clsDataFunction
    {
        #region //========== Public ==========

        /// <summary> 清除所有內容物 </summary>
        protected void ThisClear()
        {
            Internal_Clr(this);
        }

        /// <summary> this物件 內容值複製到 p_Target (不會自動增加、刪減記憶體空間, 容器物件要注意 例如:List等) </summary>
        protected void CopyTo(object p_Target)
        {
            this.Internal_Copy(this, p_Target);
        }

        /// <summary> p_Source 內容值複製到 this物件 (不會自動增加、刪減記憶體空間, 容器物件要注意 例如:List等) </summary>
        protected void CopyFrom(object p_Source)
        {
            this.Internal_Copy(p_Source, this);
        }

        static public void Clear<T>(T p_Object)
        {
            Static_Internal_Clr(p_Object);
        }
        static public void Copy<T>(T p_Source, T p_Target)
        {
            Static_Internal_Copy(p_Source, p_Target);
        }

        protected bool SaveParameter(string strPath)
        {
            try
            {
                if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(strPath)) == false)
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(strPath));
                }
                List<string> lstTemp = new List<string>();
                var Sourcelist = this.GetType().GetFields().ToList();
                for (int i = 0; i < Sourcelist.Count; i++)
                {
                    if (Sourcelist[i].FieldType.IsClass == false || Sourcelist[i].GetValue(this) is string)
                    {
                        lstTemp.Add(Sourcelist[i].Name + "=" + this.GetType().GetField(Sourcelist[i].Name).GetValue(this));
                    }
                }
                System.IO.File.WriteAllLines(strPath, lstTemp, Encoding.Unicode);
                return true;
            }
            catch
            {
            }
            return false;
        }
        protected bool LoadParameter(string strPath)
        {
            try
            {
                if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(strPath)) == false)
                {
                    return false;
                }
                if (System.IO.File.Exists(strPath) == true)
                {
                    List<string> lstTemp = System.IO.File.ReadAllLines(strPath, Encoding.UTF8).ToList<string>();
                    foreach (string strTemp in lstTemp)
                    {
                        try
                        {
                            string TempName = strTemp.Split('=')[0];
                            string TempValue = strTemp.Split('=')[1];
                            if (this.GetType().GetField(TempName).GetValue(this) is string)
                            {
                                this.GetType().GetField(TempName).SetValue(this, TempValue);
                            }
                            else if (this.GetType().GetField(TempName).GetValue(this) is double)
                            {
                                double decValue = Convert.ToDouble(TempValue);
                                this.GetType().GetField(TempName).SetValue(this, decValue);
                            }
                            else if (this.GetType().GetField(TempName).GetValue(this) is int)
                            {
                                int decValue = Convert.ToInt32(TempValue);
                                this.GetType().GetField(TempName).SetValue(this, decValue);
                            }
                            else if (this.GetType().GetField(TempName).GetValue(this) is bool)
                            {
                                this.GetType().GetField(TempName).SetValue(this, TempValue == "True");
                            }
                        }
                        catch
                        {
                        }
                    }
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        /// <summary> object 內容儲存到Txt </summary>
        protected bool SaveAsFile(string strPath, string strFileName)
        {
            try
            {
                txtLevel = 0;
                SaveTxtText.Clear();
                Internal_GetString(this);
                if (System.IO.Directory.Exists(strPath) == false)
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }
                string FilePath = strPath + "\\" + strFileName;
                FilePath = FilePath.Replace("\\\\", "\\");
                System.IO.File.WriteAllLines(FilePath, SaveTxtText);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected List<string> ShowDataList()
        {
            try
            {
                txtLevel = 0;
                SaveTxtText.Clear();
                Internal_GetString(this);
                string[] ReturnList = new string[SaveTxtText.Count];
                SaveTxtText.CopyTo(ReturnList);
                return ReturnList.ToList<string>();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        #endregion

        #region //========== Private Function ==========

        /// <summary> 內部函式 : object p_Source 複製到 object p_Target </summary>
        private void Internal_Copy(object p_Source, object p_Target)
        {
            Static_Internal_Copy(p_Source, p_Target);
        }

        /// <summary> 內部函式 : 清除所有內容物 </summary>
        private void Internal_Clr(object p_Object)
        {
            Static_Internal_Clr(p_Object);
        }

        /// <summary> 內部函式 : object p_Source 複製到 object p_Target </summary>
        static private void Static_Internal_Copy(object p_Source, object p_Target)
        {
            if (p_Source is string)
            {
                p_Target = p_Source;
            }
            else if (p_Source is IList)
            {
                IList tList = p_Source as IList;
                IList tList2 = p_Target as IList;
                for (int i = 0; i < tList.Count; i++)
                {
                    if (tList[i] is bool)
                    {
                        tList2[i] = tList[i];
                    }
                    else if (tList[i] is string)
                    {
                        tList2[i] = tList[i];
                    }
                    else if (tList[i].GetType().IsValueType)
                    {
                        tList2[i] = tList[i];
                    }
                    else
                    {
                        Static_Internal_Copy(tList[i], tList2[i]);
                    }
                }
            }
            else if (p_Source is IDictionary)
            {
                IDictionary tDictionary = p_Source as IDictionary;
                IDictionary tDictionary2 = p_Target as IDictionary;
                List<object> tKey = new List<object>();
                foreach (var Key in tDictionary.Keys)
                { tKey.Add(Key); }
                foreach (var Key in tKey)
                {
                    {
                        if (tDictionary[Key] is bool)
                        {
                            tDictionary2[Key] = tDictionary[Key];
                        }
                        else if (tDictionary2[Key] is string)
                        {
                            tDictionary2[Key] = tDictionary[Key];
                        }
                        else if (tDictionary[Key].GetType().IsValueType)
                        {
                            tDictionary2[Key] = tDictionary[Key];
                        }
                        else
                        {
                            Static_Internal_Copy(tDictionary[Key], tDictionary2[Key]);
                        }
                    }
                }
            }
            else
            {
                var Templist = p_Source.GetType().GetFields().ToList();
                var Templist2 = p_Target.GetType().GetFields().ToList();
                for (int i = 0; i < Templist.Count; i++)
                {
                    try
                    {
                        if (Templist[i].GetValue(p_Source) is string)
                        {
                            p_Target.GetType().GetField(Templist[i].Name).SetValue(p_Target,
                                    p_Source.GetType().GetField(Templist[i].Name).GetValue(p_Source));
                        }
                        else if (Templist[i].FieldType.IsClass)
                        {
                            Static_Internal_Copy(p_Source.GetType().GetField(Templist[i].Name).GetValue(p_Source)
                                , p_Target.GetType().GetField(Templist[i].Name).GetValue(p_Target));
                            //Static_Internal_Clr(Templist[i].GetValue(p_Object));
                        }
                        else 
                        {
                            p_Target.GetType().GetField(Templist[i].Name).SetValue(p_Target,
                                    p_Source.GetType().GetField(Templist[i].Name).GetValue(p_Source));
                        }
                    }
                    catch
                    {
                    }
                }
            }
            //var Sourcelist = p_Source.GetType().GetFields().ToList();
            //var Targetlist = p_Target.GetType().GetFields().ToList();
            //for (int i = 0; i < Sourcelist.Count; i++)
            //{
            //    try
            //    {
            //        if (Sourcelist[i].GetValue(p_Source) is string)
            //        {
            //            p_Target.GetType().GetField(Sourcelist[i].Name).SetValue(p_Target,
            //                    p_Source.GetType().GetField(Sourcelist[i].Name).GetValue(p_Source));
            //        }
            //        else if (Sourcelist[i].FieldType.IsClass)
            //        {
            //            if (Sourcelist[i].FieldType.FullName.Split('[')[0].Contains("System.Collections.Generic.List") == true)
            //            {
            //                IList objectSourceList = Sourcelist[i].GetValue(p_Source) as IList;
            //                IList objectTargetList = Targetlist[i].GetValue(p_Target) as IList;
            //                for (int ListIndex = 0; ListIndex < objectSourceList.Count; ListIndex++)
            //                {
            //                    if (objectSourceList[ListIndex].GetType().IsValueType == true)
            //                    {
            //                        objectTargetList[ListIndex] = objectSourceList[ListIndex];
            //                    }
            //                    else
            //                    {
            //                        Static_Internal_Copy(objectSourceList[ListIndex], objectTargetList[ListIndex]);
            //                    }
            //                }
            //            }
            //            else if (Sourcelist[i].FieldType.FullName.Split('[')[0].Contains("System.Collections.Generic.Dictionary") == true)
            //            {
            //                IDictionary objectSourceDictionary = Sourcelist[i].GetValue(p_Source) as IDictionary;
            //                IDictionary objectTargetDictionary = Targetlist[i].GetValue(p_Target) as IDictionary;
            //                foreach (var iKey in objectSourceDictionary.Keys)
            //                {
            //                    if (objectSourceDictionary[iKey].GetType().IsValueType == true)
            //                    {
            //                        objectTargetDictionary[iKey] = objectSourceDictionary[iKey];
            //                    }
            //                    else
            //                    {
            //                        Static_Internal_Copy(objectSourceDictionary[iKey], objectTargetDictionary[iKey]);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                Static_Internal_Copy(Sourcelist[i].GetValue(p_Source), Targetlist[i].GetValue(p_Target));
            //            }
            //        }
            //        else
            //        {
            //            p_Target.GetType().GetField(Sourcelist[i].Name).SetValue(p_Target,
            //                    p_Source.GetType().GetField(Sourcelist[i].Name).GetValue(p_Source));
            //        }
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
        }

        /// <summary> 內部函式 : 清除所有內容物 </summary>
        static private void Static_Internal_Clr(object p_Object)
        {
            if (p_Object is string)
            {
                p_Object = "";
            }
            else if (p_Object is IList)
            {
                IList tList = p_Object as IList;
                for (int i = 0; i < tList.Count; i++)
                {
                    if (tList[i] is bool)
                    {
                        tList[i] = false;
                    }
                    else if (tList[i] is string)
                    {
                        tList[i] = "";
                    }
                    else if (tList[i].GetType().IsValueType)
                    {
                        if (tList[i] is float || tList[i] is double)//@1.0.0.8-5
                        {
                            tList[i] = 0.0;
                        }
                        else if (tList[i] is System.Drawing.Point)
                        {
                            tList[i] = new System.Drawing.Point(0, 0);
                        }
                        else
                        {
                            tList[i] = 0;
                        }
                    }
                    else
                    {
                        Static_Internal_Clr(tList[i]);
                    }
                }
            }
            else if (p_Object is IDictionary)
            {
                IDictionary tDictionary = p_Object as IDictionary;
                List<object> tKey = new List<object>();
                foreach (var Key in tDictionary.Keys)
                { tKey.Add(Key); }
                foreach (var Key in tKey)
                {
                    if (tDictionary[Key] is bool)
                    {
                        tDictionary[Key] = false;
                    }
                    else if (tDictionary[Key] is string)
                    {
                        tDictionary[Key] = "";
                    }
                    else if (tDictionary[Key].GetType().IsValueType)
                    {
                        if (tDictionary[Key] is float || tDictionary[Key] is double) //@1.0.0.8-5
                        {
                            tDictionary[Key] = 0.0;
                        }
                        else
                        {
                            tDictionary[Key] = 0;
                        }
                    }
                    else
                    {
                        Static_Internal_Clr(tDictionary[Key]);
                    }
                }
            }
            else
            {
                var Templist = p_Object.GetType().GetFields().ToList();
                for (int i = 0; i < Templist.Count; i++)
                {
                    try
                    {
                        if (Templist[i].GetValue(p_Object) is string)
                        {
                            p_Object.GetType().GetField(Templist[i].Name).SetValue(p_Object, string.Empty);
                        }
                        else if (Templist[i].FieldType.IsClass)
                        {
                            Static_Internal_Clr(Templist[i].GetValue(p_Object));
                        }
                        else
                        {
                            p_Object.GetType().GetField(Templist[i].Name).SetValue(p_Object, null);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private int txtLevel = 0;
        private List<string> SaveTxtText = new List<string>();
        /// <summary> 儲存為TXT 文字 </summary>
        private void Internal_GetString(object p_Object, int CurrentLevel = 0)
        {
            int iCurrentLevel = CurrentLevel;
            var Templist = p_Object.GetType().GetFields().ToList();
            for (int i = 0; i < Templist.Count; i++)
            {
                try
                {
                    if (Templist[i].GetValue(p_Object) is string)
                    {
                        WriteMessage(Templist[i].Name, txtLevel * 2, iCurrentLevel);
                        WriteMessage(Templist[i].GetValue(p_Object), (txtLevel * 2) + 1, iCurrentLevel);
                    }
                    else if (Templist[i].FieldType.IsClass)
                    {
                        if (Templist[i].FieldType.FullName.Contains("List"))
                        {
                            IList objectSourceList = Templist[i].GetValue(p_Object) as IList;
                            for (int ListIndex = 0; ListIndex < objectSourceList.Count; ListIndex++)
                            {
                                txtLevel += 2;
                                Internal_GetString(objectSourceList[ListIndex], iCurrentLevel + 1);
                            }
                        }
                        else
                        {
                            txtLevel += 2;
                            Internal_GetString(Templist[i].GetValue(p_Object), iCurrentLevel + 1);
                        }
                    }
                    else
                    {
                        WriteMessage(Templist[i].Name, txtLevel * 2, iCurrentLevel);
                        WriteMessage(Templist[i].GetValue(p_Object), (txtLevel * 2) + 1, iCurrentLevel);
                    }
                }
                catch
                {
                }
            }
        }
        private void WriteMessage(object Message, int Index, int CurrentLevel)
        {
            string Temp = "";
            try
            {
                Temp = Message.ToString() + ";";
            }
            catch
            {
                Temp = ";";
            }
            for (int i = SaveTxtText.Count; i <= Index; i++)
            {
                SaveTxtText.Add("");
            }
            if (SaveTxtText[Index] == "")
            {
                for (int j = 0; j < CurrentLevel; j++)
                {
                    SaveTxtText[Index] += "\t";
                }
            }
            if (Index < SaveTxtText.Count)
            {
                SaveTxtText[Index] += Temp;
            }
            else
            {
                SaveTxtText.Add(Temp);
            }
        }

        #endregion
    }
}

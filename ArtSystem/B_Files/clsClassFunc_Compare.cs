using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using System.IO.Ports;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Xml.Serialization;

namespace ArtSystem
{
    /// <summary> 複製功能 </summary>
    public partial class clsClassFunc
    {
        static public string Compare<T>(T Item1, T Item2, string p_sParentLayer, bool p_bShortItem = true)
        {
            string rValue = "";
            try
            {
                {
                    List<string> LstMessage = new List<string>();
                    Static_Internal_Compare(Item1, Item2, p_sParentLayer, LstMessage, p_bShortItem);
                    foreach (string sMessage in LstMessage)
                    {
                        if (rValue != "")
                        { rValue += "\r\n"; }
                        rValue += sMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        /// <summary> 內部函式 : object p_Source 複製到 object p_Target </summary>
        static private bool Static_Internal_Compare(object Item1, object Item2, string p_sLayer, List<string> p_LstMessage, bool p_bShortItem = true)
        {
            bool rValue = false;
            try
            {
                string sSourceLayer = p_sLayer;
                //if (ArtSystem.clsArtSystem.bIsProgramOpen == true)
                {
                    if (Item1 == null
                        || Item1.GetType().IsValueType == true
                        || Item1 is string)
                    {
                        rValue = true;
                    }
                    else  if (Item1 is EventArgs
                        || Item1 is System.Threading.Thread)
                    {
                        //Event, Thread 跳過
                    }
                    else if (Item1.GetType().IsArray == true)
                    {
                        #region//Array
                        Array ArrayObj_Source = (Array)Item1;
                        Array ArrayObj_Target = (Array)Item2;
                        List<string> LstIndex = new List<string>();
                        int ArrayRank_Source = ArrayObj_Source.Rank;
                        int ArrayRank_Target = ArrayObj_Target.Rank;
                        if (ArrayObj_Source.Length != ArrayObj_Target.Length)
                        {
                            p_LstMessage.Add(p_sLayer + " = Array.Length" + ArrayObj_Source.Length + "->" + ArrayObj_Target.Length);
                        }
                        else if (ArrayRank_Source > 0)
                        {
                            int[] index_Source = new int[ArrayRank_Source];
                            int[] DimensiongLength = new int[ArrayRank_Source];
                            for (int iRank = 0; iRank < ArrayRank_Source; iRank++)
                            {
                                DimensiongLength[iRank] = ArrayObj_Source.GetLength(iRank);
                            }
                            while (true)
                            {
                                string sIndex = "";
                                for (int i = 0; i < index_Source.Length; i++)
                                {
                                    if (sIndex != "")
                                    { sIndex += ","; }
                                    sIndex += index_Source[i];
                                }
                                sIndex = "(" + sIndex + ")";
                                //if (index_Source.Length > 0)
                                {
                                    if (Static_Internal_Compare(ArrayObj_Source.GetValue(index_Source), ArrayObj_Target.GetValue(index_Source), p_sLayer + "[" + sIndex + "]", p_LstMessage, p_bShortItem) == true)
                                    {
                                        string sItem1Value = "";
                                        string sItem2Value = "";
                                        if (ArrayObj_Source.GetValue(index_Source) != null)
                                        {
                                            sItem1Value = ArrayObj_Source.GetValue(index_Source).ToString();
                                        }
                                        if (ArrayObj_Target.GetValue(index_Source) != null)
                                        {
                                            sItem2Value = ArrayObj_Target.GetValue(index_Source).ToString();
                                        }
                                        if (sItem1Value != sItem2Value)
                                        {
                                            p_LstMessage.Add(p_sLayer + "[" + sIndex + "]" + " = " + sItem1Value + " -> " + sItem2Value);
                                        }
                                    }
                                }
                                #region  // 更新索引（模擬多維計數器）
                                int dim = ArrayRank_Source - 1;
                                while (dim >= 0)
                                {
                                    index_Source[dim]++;
                                    if (index_Source[dim] < DimensiongLength[dim])
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        index_Source[dim] = 0;
                                        dim--;
                                    }
                                }

                                // 如果最高位元也溢位了，表示已完成所有元素
                                if (dim < 0)
                                {
                                    break;
                                }
                                #endregion
                            }
                        }
                        p_sLayer = sSourceLayer;
                        #endregion
                    }
                    else if (Item1 is IList)
                    {
                        #region//List
                        IList IDic_Source = (IList)Item1;
                        IList IDic_Target = (IList)Item2;
                        if (IDic_Source.Count < IDic_Target.Count)
                        {
                            for (int i = IDic_Source.Count; i < IDic_Target.Count; i++)
                            {
                                p_LstMessage.Add(p_sLayer + "= Add List Item[" + i.ToString() + "]");
                                if (p_bShortItem == false)
                                {
                                    var settings = new JsonSerializerSettings();
                                    settings.Converters.Add(new StringEnumConverter());
                                    string jsonData = JsonConvert.SerializeObject(IDic_Target[i], Formatting.Indented, settings);
                                    p_LstMessage.Add("\r\n" + jsonData);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < IDic_Source.Count; i++)
                            {
                                if (i >= IDic_Target.Count)
                                {
                                    p_LstMessage.Add(p_sLayer + "= Remove List Item[" + i.ToString() + "]");
                                    //clsLog.Log("CatchLog", "CopyIList");
                                }
                                else if (i < IDic_Target.Count)
                                {
                                    p_sLayer += "[" + i.ToString() + "]";
                                    if (Static_Internal_Compare(IDic_Source[i], IDic_Target[i], p_sLayer, p_LstMessage, p_bShortItem) == true)
                                    {
                                        string sItem1Value = "";
                                        string sItem2Value = "";
                                        if (IDic_Source[i] != null)
                                        {
                                            sItem1Value = IDic_Source[i].ToString();
                                        }
                                        if (IDic_Target[i] != null)
                                        {
                                            sItem2Value = IDic_Target[i].ToString();
                                        }
                                        if (sItem1Value != sItem2Value)
                                        {
                                            p_sLayer += " = " + sItem1Value + "  -> " + sItem2Value;
                                            p_LstMessage.Add(p_sLayer);
                                        }
                                    }
                                    p_sLayer = sSourceLayer;
                                }
                            }
                        }
                        #endregion
                    }
                    else if (Item1 is IDictionary)
                    {
                        #region//Dictionary
                        IDictionary IDic_Source = (IDictionary)Item1;
                        IDictionary IDic_Target = (IDictionary)Item2;
                        foreach (DictionaryEntry entry in IDic_Target)
                        {
                            if (IDic_Source.Contains(entry.Key) == false)
                            {
                                p_LstMessage.Add(p_sLayer + " = IDictionary, Add(" + entry.Key.ToString() + ")");
                                if (p_bShortItem == false)
                                {
                                    var settings = new JsonSerializerSettings();
                                    settings.Converters.Add(new StringEnumConverter());
                                    string jsonData = JsonConvert.SerializeObject(IDic_Target[entry.Key], Formatting.Indented, settings);
                                    p_LstMessage.Add("\r\n" + jsonData);
                                }
                            }
                        }
                        foreach (DictionaryEntry entry in IDic_Source)
                        {
                            if (IDic_Target.Contains(entry.Key) == false)
                            {
                                p_LstMessage.Add(p_sLayer = " = IDictionary, Remove(" + entry.Key.ToString() + ")");
                            }
                            if (IDic_Target.Contains(entry.Key) == true)
                            {
                                p_sLayer += "[" + entry.Key.ToString() + "]";
                                if (Static_Internal_Compare(IDic_Source[entry.Key], IDic_Target[entry.Key], p_sLayer, p_LstMessage, p_bShortItem) == true)
                                {
                                    string sItem1Value = "";
                                    string sItem2Value = "";
                                    if (IDic_Source[entry.Key] != null)
                                    {
                                        sItem1Value = IDic_Source[entry.Key].ToString();
                                    }
                                    if (IDic_Target[entry.Key] != null)
                                    {
                                        sItem2Value = IDic_Target[entry.Key].ToString();
                                    }
                                    if (sItem1Value != sItem2Value)
                                    {
                                        p_sLayer += " = " + sItem1Value + "  -> " + sItem2Value;
                                        p_LstMessage.Add(p_sLayer);
                                    }
                                }
                                p_sLayer = sSourceLayer;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region//Class
                        var LstFields = Item1.GetType().GetFields().ToList();
                        var LstProperties = Item1.GetType().GetProperties().ToList();
                        foreach (var FieldItem in LstFields)
                        {
                            if (Attribute.IsDefined(FieldItem, typeof(JsonIgnoreAttribute)) == false
                                && Attribute.IsDefined(FieldItem, typeof(XmlIgnoreAttribute)) == false)
                            {
                                if (clsClassFunc.FieldCanWrite(FieldItem) == true)
                                {
                                    string sName = FieldItem.Name;
                                    var SourceValue = FieldItem.GetValue(Item1);
                                    var TargetValue = FieldItem.GetValue(Item2);
                                    if (p_sLayer != "")
                                    { p_sLayer += "."; }
                                    p_sLayer += sName;
                                    if (Static_Internal_Compare(SourceValue, TargetValue, p_sLayer, p_LstMessage, p_bShortItem) == true)
                                    {
                                        string sItem1Value = "";
                                        string sItem2Value = "";
                                        if (SourceValue != null)
                                        {
                                            sItem1Value = SourceValue.ToString();
                                        }
                                        if (TargetValue != null)
                                        {
                                            sItem2Value = TargetValue.ToString();
                                        }
                                        if (sItem1Value != sItem2Value)
                                        {
                                            p_sLayer += " = " + sItem1Value + " -> " + sItem2Value;
                                            p_LstMessage.Add(p_sLayer);
                                        }
                                    }
                                    p_sLayer = sSourceLayer;
                                }
                            }
                        }
                        //if (rValue == false)
                        {
                            foreach (var PropertiesItem in LstProperties)
                            {
                                if (Attribute.IsDefined(PropertiesItem, typeof(JsonIgnoreAttribute)) == false
                                    && Attribute.IsDefined(PropertiesItem, typeof(XmlIgnoreAttribute)) == false)
                                {
                                    MethodInfo setMethod = PropertiesItem.GetSetMethod(/* nonPublic */ true);
                                    bool hasPublicSet = setMethod != null && setMethod.IsPublic;
                                    if (hasPublicSet == true)
                                    {
                                        if (p_sLayer != "")
                                        { p_sLayer += "."; }
                                        p_sLayer += PropertiesItem.Name;
                                        var SourceValue = PropertiesItem.GetValue(Item1, null);
                                        var TargetValue = PropertiesItem.GetValue(Item2, null);
                                        if (Static_Internal_Compare(SourceValue, TargetValue, p_sLayer, p_LstMessage, p_bShortItem) == true)
                                        {
                                            string sItem1Value = "";
                                            string sItem2Value = "";
                                            if (SourceValue != null)
                                            {
                                                sItem1Value = SourceValue.ToString();
                                            }
                                            if (TargetValue != null)
                                            {
                                                sItem2Value = TargetValue.ToString();
                                            }
                                            if (sItem1Value != sItem2Value)
                                            {
                                                p_sLayer += " = " + sItem1Value + "  -> " + sItem2Value;
                                                p_LstMessage.Add(p_sLayer);
                                            }
                                        }
                                    }
                                    p_sLayer = sSourceLayer;
                                }
                            }
                        }
                        #endregion
                    }
                }
                p_sLayer = sSourceLayer;
            }
            catch (Exception ex)
            {
                p_LstMessage.Add(p_sLayer + ", Catch Error");
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
    }
}

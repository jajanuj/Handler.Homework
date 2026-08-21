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
        static public void Clear<T>(T p_Source)
        {
            try
            {
                object pObj = null;
                Static_Internal_Clear(p_Source,out pObj);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 內部函式 : object p_Source 複製到 object p_Target </summary>
        static private bool Static_Internal_Clear(object p_Source, out object OverideObj)
        {
            bool rValue = false;
            OverideObj = null;
            try
            {
                //p_Source.DeepCloneTo(p_Target);
                //if (ArtSystem.clsArtSystem.bIsProgramOpen == true)
                {
                    if (p_Source == null
                        || p_Source.GetType().IsValueType == true
                        || p_Source is string)
                    {
                        rValue = true;
                    }
                    else if (p_Source.GetType().IsArray == true)
                    {
                        #region//Array
                        Array ArrayObj_Source = (Array)p_Source;
                        List<string> LstIndex = new List<string>();
                        int ArrayRank_Source = ArrayObj_Source.Rank;
                        if (ArrayRank_Source > 0)
                        {
                            int[] index_Source = new int[ArrayRank_Source];
                            int[] DimensiongLength = new int[ArrayRank_Source];
                            for (int iRank = 0; iRank < ArrayRank_Source; iRank++)
                            {
                                DimensiongLength[iRank] = ArrayObj_Source.GetLength(iRank);
                            }
                            while (true)
                            {
                                object pObj = null;
                                if (Static_Internal_Clear(ArrayObj_Source.GetValue(index_Source), out pObj) == true)
                                {
                                    if (ArrayObj_Source.GetValue(index_Source) is string)
                                    {
                                        ArrayObj_Source.SetValue("", index_Source);
                                    }
                                    else
                                    {
                                        ArrayObj_Source.SetValue(GetCustomDefaultValue(ArrayObj_Source.GetValue(index_Source).GetType()), index_Source);
                                    }
                                }
                                if (pObj != null)
                                {
                                    ArrayObj_Source.SetValue(pObj, index_Source);
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
                        OverideObj = ArrayObj_Source;
                        #endregion
                    }
                    else if (p_Source is IList)
                    {
                        #region//List
                        IList IDic_Source = (IList)p_Source;
                        for (int i = 0; i < IDic_Source.Count; i++)
                        {
                            object pObj = null;
                            if (Static_Internal_Clear(IDic_Source[i], out pObj) == true)
                            {
                                if (IDic_Source[i] is string)
                                {
                                    IDic_Source[i] = "";
                                }
                                else
                                {
                                    IDic_Source[i] = GetCustomDefaultValue(IDic_Source[i].GetType());
                                }
                            }
                        }
                        #endregion
                    }
                    else if (p_Source is IDictionary)
                    {
                        #region//Dictionary
                        IDictionary IDic_Source = (IDictionary)p_Source;
                        foreach (DictionaryEntry entry in IDic_Source)
                        {
                            object pObj = null;
                            if (Static_Internal_Clear(IDic_Source[entry.Key], out pObj) == true)
                            {
                                if (IDic_Source[entry.Key] is string)
                                {
                                    IDic_Source[entry.Key] = "";
                                }
                                else
                                {
                                    IDic_Source[entry.Key] = GetCustomDefaultValue(IDic_Source[entry.Key].GetType());
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region//Class
                        var LstFields = p_Source.GetType().GetFields().ToList();
                        var LstProperties = p_Source.GetType().GetProperties().ToList();
                        foreach (var FieldItem in LstFields)
                        {
                            if (Attribute.IsDefined(FieldItem, typeof(JsonIgnoreAttribute)) == false
                                && Attribute.IsDefined(FieldItem, typeof(XmlIgnoreAttribute)) == false)
                            {
                                if (clsClassFunc.FieldCanWrite(FieldItem) == true)
                                {
                                    string sName = FieldItem.Name;
                                    var SourceValue = FieldItem.GetValue(p_Source);
                                    object pObj = null;
                                    if (Static_Internal_Clear(SourceValue, out pObj) == true)
                                    {
                                        if (SourceValue is string)
                                        {
                                            FieldItem.SetValue(p_Source, "");
                                        }
                                        else
                                        {
                                            FieldItem.SetValue(p_Source, GetCustomDefaultValue(SourceValue.GetType()));
                                        }
                                    }
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
                                        var SourceValue = PropertiesItem.GetValue(p_Source, null);
                                        object pObj = null;
                                        if (Static_Internal_Clear(SourceValue, out pObj) == true)
                                        {
                                            if (SourceValue is string)
                                            {
                                                PropertiesItem.SetValue(p_Source, "", null);
                                            }
                                            else
                                            {
                                                PropertiesItem.SetValue(p_Source, GetCustomDefaultValue(SourceValue.GetType()), null);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        static private object GetCustomDefaultValue(Type type)
        {
            // 检查是否存在静态属性或方法 `Default`
            var defaultProperty = type.GetProperty("Default", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            if (defaultProperty != null)
            {
                //return defaultProperty.GetValue(null);
            }

            var defaultMethod = type.GetMethod("Default", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            if (defaultMethod != null)
            {
                return defaultMethod.Invoke(null, null);
            }

            // 如果没有自定义默认值，返回语言默认值
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}

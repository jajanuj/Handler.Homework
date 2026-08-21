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
using System.Xml.Serialization;

namespace ArtSystem
{
    /// <summary> 複製功能 </summary>
    public partial class clsClassFunc
    {
        static public void Copy(object p_Source, object p_Target)
        {
            try
            {
                {
                    if (p_Source.GetType() == p_Target.GetType())
                    {
                        object overideTarget = null;
                        Static_Internal_Copy(p_Source, p_Target ,out overideTarget);
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        

        static public bool FieldCanWrite(FieldInfo field)
        {
            bool rValue = false;

            // 判斷是否為靜態欄位 (如果需要靜態欄位則去掉下面這行)
            if (field.IsStatic)
            {
                // 可以考慮靜態欄位是否可以寫入，這裡可選擇忽略靜態欄位
                return false;
            }

            // 判斷欄位是否可以寫入：欄位非 readonly 且非 const 且屬於公有欄位
            if (!field.IsInitOnly && !field.IsLiteral && field.IsPublic)
            {
                // 如果找到一個可寫的欄位，則返回 true
                rValue = true;
            }

            return rValue;
        }
        /// <summary> 內部函式 : object p_Source 複製到 object p_Target </summary>
        static private bool Static_Internal_Copy(object p_Source,object p_Target, out object p_Overide)
        {
            bool rValue = false;
            p_Overide = null;
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
                        try
                        {
                            #region//Array
                            Array ArrayObj_Source = (Array)p_Source;
                            Array ArrayObj_Target = (Array)p_Target;
                            int ArrayRank_Source = ArrayObj_Source.Rank;
                            int ArrayRank_Target = ArrayObj_Target.Rank;
                            if (ArrayRank_Source > 0)
                            {
                                if (ArrayRank_Source != ArrayRank_Target)
                                {
                                    clsLog.Log(clsArtSystem.g_strCatchLogName, "clsClassCopy->Copy() : Array ranks do not match. Source Rank: " + ArrayRank_Source + ", Target Rank: " + ArrayRank_Target);
                                }
                                if (ArrayObj_Source.Length != ArrayObj_Target.Length)
                                {
                                    try
                                    {
                                        // 自動調整目標數組的大小（可選）
                                        Type elementType = ArrayObj_Target.GetType().GetElementType();
                                        Array resizedArray = Array.CreateInstance(elementType, ArrayObj_Source.Length);
                                        Array.Copy(ArrayObj_Target, resizedArray, Math.Min(ArrayObj_Target.Length, ArrayObj_Source.Length));
                                        ArrayObj_Target = resizedArray;
                                        p_Overide = resizedArray;
                                    }
                                    catch (Exception ex)
                                    {
                                        clsLog.Log(clsArtSystem.g_strCatchLogName, "clsClassCopy->Copy() : Array-Length Not Match");
                                        clsArtSystem.CatchLog(ex);
                                    }
                                }
                                // 準備多維數組的索引遍歷
                                int[] index_Source = new int[ArrayRank_Source];
                                int[] lengths = new int[ArrayRank_Source];
                                for (int i = 0; i < ArrayRank_Source; i++)
                                {
                                    lengths[i] = ArrayObj_Source.GetLength(i);
                                }
                                //List<string> LstIndex = new List<string>();
                                //for (int iRank = 0; iRank < LstIndex.Count; iRank++)
                                //{
                                //    index_Source[iRank] = Convert.ToInt32(LstIndex[iRank]);
                                //}
                                // 遍歷多維數組
                                foreach (int[] currentIndex in EnumerateIndices(lengths))
                                {
                                    //if (currentIndex.Length > 0)
                                    {
                                        // 從來源數組取值
                                        object sourceValue = ArrayObj_Source.GetValue(currentIndex);
                                        object targetValue = ArrayObj_Target.GetValue(currentIndex);
                                        object OverideTargetValue = null;
                                        if (Static_Internal_Copy(sourceValue, targetValue, out OverideTargetValue))
                                        {
                                            ArrayObj_Target.SetValue(sourceValue, currentIndex);
                                        }
                                        if (OverideTargetValue != null)
                                        {
                                            ArrayObj_Target.SetValue(OverideTargetValue, currentIndex);
                                        }
                                    }
                                }
                            }
                            p_Overide = ArrayObj_Target;
                            #endregion

                        }
                        catch (Exception ex)
                        {
                            clsArtSystem.CatchLog(ex);
                        }
                    }
                    else if (p_Source is IList)
                    {
                        try
                        {
                            #region//List
                            IList ILst_Source = (IList)p_Source;
                            IList ILst_Target = (IList)p_Target;
                            for (int i = 0; i < ILst_Source.Count; i++)
                            {
                                if (i >= ILst_Target.Count)
                                {
                                    try
                                    {
                                        ILst_Target.Add(CreateNewObject(ILst_Source[i]));
                                    }
                                    catch (Exception ex)
                                    {
                                        clsLog.Log(clsArtSystem.g_strCatchLogName, "clsClassCopy->Copy() : CopyIList");
                                        clsArtSystem.CatchLog(ex);
                                    }
                                }
                                if (i < ILst_Target.Count)
                                {
                                    object overideTarget = null;
                                    if (Static_Internal_Copy(ILst_Source[i], ILst_Target[i], out overideTarget) == true)
                                    {
                                        ILst_Target[i] = ILst_Source[i];
                                    }
                                }
                            }
                            try
                            {
                                for (int i = ILst_Target.Count - 1; i >= ILst_Source.Count; i--)
                                {
                                    ILst_Target.RemoveAt(i);
                                }
                            }
                            catch (Exception ex)
                            {
                                clsLog.Log(clsArtSystem.g_strCatchLogName, "clsClassCopy->Copy() : RemoveIList");
                                clsArtSystem.CatchLog(ex);
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            clsArtSystem.CatchLog(ex);
                        }
                    }
                    else if (p_Source is IDictionary)
                    {
                        try
                        {
                            #region//Dictionary
                            IDictionary IDic_Source = (IDictionary)p_Source;
                            IDictionary IDic_Target = (IDictionary)p_Target;
                            List<object> IDic_TargetKeys = new List<object>();
                            foreach (object TargetKey in IDic_Target.Keys)
                            {
                                IDic_TargetKeys.Add(TargetKey);
                            }
                            foreach (DictionaryEntry entry in IDic_Source)
                            {
                                if (IDic_Target.Contains(entry.Key) == false)
                                {
                                    try
                                    {
                                        IDic_Target.Add(entry.Key, CreateNewObject(IDic_Source[entry.Key]));
                                    }
                                    catch (Exception ex)
                                    {
                                        clsLog.Log(clsArtSystem.g_strCatchLogName, "clsClassCopy->Copy() : CopyIDictionary");
                                        clsArtSystem.CatchLog(ex);
                                    }
                                }
                                if (IDic_TargetKeys.Contains(entry.Key))
                                {
                                    IDic_TargetKeys.Remove(entry.Key);
                                }
                                object overideTarget = null;
                                if (Static_Internal_Copy(IDic_Source[entry.Key], IDic_Target[entry.Key], out overideTarget) == true)
                                {
                                    IDic_Target[entry.Key] = IDic_Source[entry.Key];
                                }
                            }
                            foreach (object TargetKey in IDic_TargetKeys)
                            {
                                IDic_Target.Remove(TargetKey);
                            }
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            clsArtSystem.CatchLog(ex);
                        }
                    }
                    else
                    {
                        #region//Class
                        var LstFields = p_Source.GetType().GetFields().ToList();
                        var LstProperties = p_Source.GetType().GetProperties().ToList();
                        var LstProperties_Target = p_Target.GetType().GetProperties().ToList();
                        foreach (var FieldItem in LstFields)
                        {
                            if (Attribute.IsDefined(FieldItem, typeof(JsonIgnoreAttribute)) == false
                                && Attribute.IsDefined(FieldItem, typeof(XmlIgnoreAttribute)) == false)
                            {
                                if (FieldCanWrite(FieldItem) == true)
                                {
                                    string sName = FieldItem.Name;
                                    var SourceValue = FieldItem.GetValue(p_Source);
                                    var TargetValue = FieldItem.GetValue(p_Target);
                                    object overideTarget = null;
                                    if (sName == "strPatternROI")
                                    {
                                    }
                                    if (SourceValue != null && TargetValue == null)
                                    {
                                        FieldItem.SetValue(p_Target, CreateNewObject(SourceValue));
                                        TargetValue = FieldItem.GetValue(p_Target);
                                        if (Static_Internal_Copy(SourceValue, TargetValue, out overideTarget) == true)
                                        {
                                            if (FieldCanWrite(FieldItem) == true)
                                            {
                                                FieldItem.SetValue(p_Target, SourceValue);
                                            }
                                            else
                                            {
                                            }
                                        }
                                        if (overideTarget != null)
                                        {
                                            FieldItem.SetValue(p_Target, overideTarget);
                                        }
                                    }
                                    else if (Static_Internal_Copy(SourceValue, TargetValue, out overideTarget) == true)
                                    {
                                        if (FieldCanWrite(FieldItem) == true)
                                        {
                                            FieldItem.SetValue(p_Target, SourceValue);
                                        }
                                        else
                                        {
                                        }
                                    }
                                    if (overideTarget != null)
                                    {
                                        FieldItem.SetValue(p_Target, overideTarget);
                                    }
                                }
                            }
                            else
                            {
                                //JsonIgnore
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
                                        var TargetValue = PropertiesItem.GetValue(p_Target, null);
                                        object overideTarget = null;
                                        if (PropertiesItem.Name == "strPatternROI")
                                        {
                                        }
                                        if (Static_Internal_Copy(SourceValue, TargetValue, out overideTarget) == true)
                                        {
                                            if (SourceValue != null && TargetValue == null)
                                            {
                                                if (PropertiesItem.CanWrite == true)
                                                {
                                                    PropertiesItem.SetValue(p_Target, CreateNewObject(SourceValue), null);
                                                    //TargetValue = PropertiesItem.GetValue(p_Target, null);
                                                }
                                            }
                                            if (PropertiesItem.CanWrite == true)
                                            {
                                                PropertiesItem.SetValue(p_Target, SourceValue, null);
                                            }
                                        }
                                        if (overideTarget != null)
                                        {
                                            PropertiesItem.SetValue(p_Target, overideTarget, null);
                                        }
                                    }
                                }
                                else
                                {
                                    //JsonIgnore
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
        static private object CreateNewObject(object sourceValue)
        {
            try
            {
                if (sourceValue == null)
                {
                    // 如果原始值是 null，可以返回一个默认值或新的实例
                    return new object();
                }
                Type sourceType = sourceValue.GetType();
                if (sourceType.IsValueType || sourceType == typeof(string))
                {
                    // 对于值类型或字符串，直接返回原值的副本
                    return sourceValue;
                }
                // 检查是否有无参构造函数
                if (sourceType.GetConstructor(Type.EmptyTypes) != null)
                {
                    // 使用无参构造函数创建新实例
                    return Activator.CreateInstance(sourceType);
                }
                else
                {
                    clsLog.Log(clsArtSystem.g_strCatchLogName, "clsClassCopy->Copy() : CreateNewObject->" + "The type {sourceType.FullName} does not have a parameterless constructor.");
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return new object();
        }
        static private IEnumerable<int[]> EnumerateIndices(int[] lengths)
        {
            int dimensions = lengths.Length; // 獲取數組的維度
            int[] indices = new int[dimensions]; // 初始化索引陣列

            while (true)
            {
                // 返回當前的索引
                yield return (int[])indices.Clone();

                // 遍歷最後一個維度並進行遞增
                int dim = dimensions - 1;
                while (dim >= 0)
                {
                    indices[dim]++;
                    if (indices[dim] < lengths[dim])
                    {
                        break; // 當前維度沒有超界，停止進位
                    }
                    indices[dim] = 0; // 超界，重置當前維度並進位到更高的維度
                    dim--;
                }

                if (dim < 0)
                {
                    yield break; // 所有維度都遍歷完成
                }
            }
        }

    }
}

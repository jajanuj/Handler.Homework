using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtSystem.Files
{
    /// <summary> JsonHelper (Newtonsoft.Json.dll) </summary>
    public class JsonHelper
    {
        /// <summary>
        /// JsonSerialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        static public string JsonSerialize(object target) 
        {
            if (target == null) 
                throw new ArgumentNullException("target");
            return JsonConvert.SerializeObject(target);
        }

        /// <summary>
        /// JsonSerializeEx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        static public byte[] JsonSerializeEx<T>(T target, Encoding encoding) 
        {
            if (target == null) 
                throw new ArgumentNullException("target");
            if (encoding == null) 
                throw new ArgumentNullException("encoding");

            using (var ms = new MemoryStream() ) 
            {
                using (var sw = new StreamWriter(ms, encoding) ) 
                {
                    var serializer = new JsonSerializer { Formatting = Newtonsoft.Json.Formatting.Indented };
                    serializer.Serialize(sw, target);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// JsonSerializeToFile
        /// </summary>
        /// <param name="target"></param>
        /// <param name="fullPath"></param>
        /// <param name="encoding"></param>
        static public void JsonSerializeToFile(object target, string fullPath, Encoding encoding) 
        {
            if (string.IsNullOrEmpty(fullPath) ) 
                throw new ArgumentNullException("path");
            if (target == null) 
                throw new ArgumentNullException("o");
            if (encoding == null) 
                throw new ArgumentNullException("encoding");

            using (var fs = new FileStream(fullPath, FileMode.Create) ) 
            {
                using (var bw = new BinaryWriter(fs) )
                {
                    bw.Write(JsonHelper.JsonSerializeEx(target, encoding));
                }
            }
        }

        /// <summary>
        /// JsonDeserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonText"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        static public T JsonDeserialize<T>(string jsonText, Encoding encoding) 
        {
            if (string.IsNullOrEmpty(jsonText) ) 
                throw new ArgumentNullException("jsonText");
            if (encoding == null) 
                throw new ArgumentNullException("encoding");

            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(jsonText) ) ) 
            {
                using (StreamReader sr = new StreamReader(ms, encoding) ) 
                {
                    JsonSerializer mySerializer = new JsonSerializer();
                    var rValue = (T)JsonConvert.DeserializeObject<T>(jsonText);
                    return rValue;
                    //return (T) mySerializer.Deserialize(sr, typeof(T) );
                }
            }
        }

        /// <summary>
        /// JsonDeserializeFromFile
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullPath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        static public T JsonDeserializeFromFile<T>(string fullPath, Encoding encoding) 
        {
            try
            {
                if (string.IsNullOrEmpty(fullPath))
                {
                    Console.Write("[Json] DeserializeFormFils Error : PathName");
                    return default(T);
                }
                if (System.IO.File.Exists(fullPath) == false)
                {
                    Console.Write("[Json] DeserializeFormFils Error : FileExist");
                    return default(T);
                }
                if (encoding == null)
                {
                    Console.Write("[Json] DeserializeFormFils Error : encoding");
                    return default(T);
                }
                using (var sr = new StreamReader(fullPath, encoding))
                {
                    var serializer = new JsonSerializer();
                    return (T)serializer.Deserialize(sr, typeof(T));
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return default(T);
        }

        //static public DataTable Tabulate(string json) 
        //{
        //    var jsonLinq = JObject.Parse(json);

        //    // Find the first array using Linq
        //    var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
        //    var trgArray = new JArray();
        //    foreach (JObject row in srcArray.Children<JObject>() ) 
        //    {
        //        var cleanRow = new JObject();
        //        foreach (JProperty column in row.Properties() ) 
        //        {
        //            // Only include JValue types
        //            if (column.Value is JValue) 
        //            {
        //                cleanRow.Add(column.Name, column.Value);
        //            }
        //        }

        //        trgArray.Add(cleanRow);
        //    }

        //    return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString() );
        //}

        static public T JsonDeserializeObject<T>(string jsonText, Encoding encoding) 
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Auto
            };

            var serializedObj = JsonConvert.SerializeObject(jsonText);
            var rValue = JsonConvert.DeserializeObject<T>(serializedObj, jsonSerializerSettings);
            return rValue;
        }


        static public byte[] ObjectToByteArray(Object obj) 
        {
            if(obj == null) 
                return null;
        
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
        
            return ms.ToArray();
        }
        
        // Convert a byte array to an Object
        static public T ByteArrayToObject<T>(byte[] data) 
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();

            memStream.Write(data, 0, data.Length);
            memStream.Seek(0, SeekOrigin.Begin);

            T obj = (T) binForm.Deserialize(memStream);

            return (T) obj;
        }


        static public byte[] JsonStringToByteArray(string jsonByteString) 
        {
            byte[] bArray = Encoding.Unicode.GetBytes(jsonByteString);
            return bArray;
        }
    }
}

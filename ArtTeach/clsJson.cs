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
using ArtCommonLib;
using ArtData;
using ArtControlLib;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ArtTeach
{
    /// <summary>
    /// JsonHelper
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// JsonSerialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string JsonSerialize<T>(T target) 
        {
            if (target == null) 
                throw new ArgumentNullException("target") ;
            return JsonConvert.SerializeObject(target) ;
        }

        /// <summary>
        /// JsonSerializeEx
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] JsonSerializeEx<T>(T target, Encoding encoding) 
        {
            if (target == null) 
                throw new ArgumentNullException("target") ;
            if (encoding == null) 
                throw new ArgumentNullException("encoding") ;

            using (var ms = new MemoryStream() ) 
            {
                using (var sw = new StreamWriter(ms, encoding) ) 
                {
                    var serializer = new JsonSerializer { Formatting = Newtonsoft.Json.Formatting.Indented };
                    serializer.Serialize(sw, target) ;
                }
                return ms.ToArray() ;
            }
        }

        /// <summary>
        /// JsonSerializeToFile
        /// </summary>
        /// <param name="target"></param>
        /// <param name="fullPath"></param>
        /// <param name="encoding"></param>
        public static void JsonSerializeToFile(object target, string fullPath, Encoding encoding) 
        {
            if (string.IsNullOrEmpty(fullPath) ) 
                throw new ArgumentNullException("path") ;
            if (target == null) 
                throw new ArgumentNullException("o") ;
            if (encoding == null) 
                throw new ArgumentNullException("encoding") ;

            using (var fs = new FileStream(fullPath, FileMode.Create) ) 
            {
                using (var bw = new BinaryWriter(fs) ) 
                {
                    bw.Write(JsonHelper.JsonSerializeEx(target, encoding) ) ;
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
        public static T JsonDeserialize<T>(string jsonText, Encoding encoding) 
        {
            if (string.IsNullOrEmpty(jsonText) ) 
                throw new ArgumentNullException("jsonText") ;
            if (encoding == null) 
                throw new ArgumentNullException("encoding") ;

            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(jsonText) ) ) 
            {
                using (StreamReader sr = new StreamReader(ms, encoding) ) 
                {
                    JsonSerializer mySerializer = new JsonSerializer() ;
                    return (T) JsonConvert.DeserializeObject<T>(jsonText) ;
                    //return (T) mySerializer.Deserialize(sr, typeof(T) ) ;
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
        public static T JsonDeserializeFromFile<T>(string fullPath, Encoding encoding) 
        {
            if (string.IsNullOrEmpty(fullPath) ) 
                throw new ArgumentNullException("path") ;
            if (encoding == null) 
                throw new ArgumentNullException("encoding") ;
            using (var sr = new StreamReader(fullPath, encoding) ) 
            {
                var serializer = new JsonSerializer() ;
                return (T) serializer.Deserialize(sr, typeof(T) ) ;
            }
        }

        public static DataTable Tabulate(string json) 
        {
            var jsonLinq = JObject.Parse(json) ;

            // Find the first array using Linq
            var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First() ;
            var trgArray = new JArray() ;
            foreach (JObject row in srcArray.Children<JObject>() ) 
            {
                var cleanRow = new JObject() ;
                foreach (JProperty column in row.Properties() ) 
                {
                    // Only include JValue types
                    if (column.Value is JValue) 
                    {
                        cleanRow.Add(column.Name, column.Value) ;
                    }
                }

                trgArray.Add(cleanRow) ;
            }

            return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString() ) ;
        }

        public static T JsonDeserializeObject<T>(string jsonText, Encoding encoding) 
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Auto
            };

            var serializedObj = JsonConvert.SerializeObject(jsonText) ;
            return JsonConvert.DeserializeObject<T>(serializedObj, jsonSerializerSettings) ;
        }


        public static byte[] ObjectToByteArray(Object obj) 
        {
            if(obj == null) 
                return null;
        
            BinaryFormatter bf = new BinaryFormatter() ;
            MemoryStream ms = new MemoryStream() ;
            bf.Serialize(ms, obj) ;
        
            return ms.ToArray() ;
        }
        
        // Convert a byte array to an Object
        public static T ByteArrayToObject<T>(byte[] data) 
        {
            MemoryStream memStream = new MemoryStream() ;
            BinaryFormatter binForm = new BinaryFormatter() ;

            memStream.Write(data, 0, data.Length) ;
            memStream.Seek(0, SeekOrigin.Begin) ;

            T obj = (T) binForm.Deserialize(memStream) ;

            return (T) obj;
        }


        public static byte[] JsonStringToByteArray(string jsonByteString) 
        {
            byte[] bArray = Encoding.ASCII.GetBytes(jsonByteString) ;
            return bArray;
        }
    }
}

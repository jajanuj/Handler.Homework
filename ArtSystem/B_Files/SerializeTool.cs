using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace ArtSystem.Files
{
    static public class SerializeTool
    {
        /// <summary> 讀取XML格式參數檔 </summary>
        /// <param name="tFilePath">檔案路徑</param>
        /// <param name="tMutiCamPara">MutiCam 初始化參數</param>
        /// <returns>true : 讀取成功 , false : 讀取失敗</returns>
        static public T DeserializeFromFile<T>(string tFilePath, ref bool IsSuccess)
        {
            try
            {
                StreamReader sr = new StreamReader(tFilePath);

                string strPara = sr.ReadToEnd();

                sr.Dispose();
                sr.Close();
                IsSuccess = true;
                return Deserialize<T>(strPara);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                IsSuccess = false;
                return default(T);
            }
        }

        /// <summary> 寫入XML初始參數檔 </summary>
        /// <param name="tFilePath"> 檔案路徑 </param>
        static public void SerializeToFile(object p_Pmt, string tFilePath)
        {
            string strPara = Serialize(p_Pmt);

            StreamWriter sr = new StreamWriter(tFilePath);

            sr.WriteLine(strPara);

            sr.Dispose();
            sr.Close();
        }

        /// <summary> 將物件序列化至字串 </summary>
        /// <param name="p_Object"> 傳入要進行序列化的物件 </param>
        /// <returns> 完成序列化的字串 </returns>
        private static string Serialize(object p_Object)
        {
            XmlSerializer tXmlSerializer = new XmlSerializer(p_Object.GetType());

            StringBuilder tStringBuilder = new StringBuilder();

            StringWriter tStringWriter = new StringWriter(tStringBuilder);

            tXmlSerializer.Serialize(tStringWriter, p_Object);

            return tStringBuilder.ToString();
        }

        /// <summary> 將字串反序列化至物件 </summary>
        /// <typeparam name="T"> 物件型態 </typeparam>
        /// <param name="p_String"> 要進行反序列化的字串 </param>
        /// <returns> 完成反序列化的物件 </returns>
        private static T Deserialize<T>(string p_String)
        {
            XmlDocument tXmlDocument = new XmlDocument();

            tXmlDocument.LoadXml(p_String);

            XmlNodeReader tXmlNodeReader = new XmlNodeReader(tXmlDocument.DocumentElement);

            XmlSerializer tXmlSerializer = new XmlSerializer(typeof(T));

            object tObject = tXmlSerializer.Deserialize(tXmlNodeReader);

            return (T)tObject;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
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
using ArtTeach;
using Newtonsoft.Json;

namespace ArtTeach
{
    /// <summary>
    /// Pro File
    /// </summary>
    public class clsProFile
    {
        public class clsMaster
        {
            #region 參數

            public string _strMaster = "";
            //public string _strMark = "";
            public List<clsSlave> _lisSlave;

            #endregion

            #region 建構式 與 解建構式

            public clsMaster() 
            {
                _lisSlave = new List<clsSlave>() ;
            }
            ~clsMaster() 
            {
                _lisSlave.Clear() ;
            }

            #endregion

            /// <summary>複製元件資料</summary>
            /// <returns>回傳複製元件資料</returns>
            public clsMaster Clone(string p_strName)
            {
                clsMaster m_clsMaster = new clsMaster();
                m_clsMaster._strMaster = p_strName;
                for (int i = 0; i < this._lisSlave.Count; i++)
                {
                    m_clsMaster._lisSlave.Add(this._lisSlave[i].Clone(this._lisSlave[i]._strNodeName));
                }
                return m_clsMaster;
            }
        }

        #region 子物件
        public class clsSlave
        {
            #region 參數

            public string _strNodeName = "";
            public string _strMark = "";
            public string _DisplayImagePath = "";
            public string _DiscreptionMessage = "";
            public List<clsDataJogTeach> _lisJogAndTeachInfo = new List<clsDataJogTeach>() ;
            public List<clsDataDIO> _lisDIOInfo = new List<clsDataDIO>();

            #endregion

            #region 建構式 與 解建構式

            public clsSlave(string SlaveName) 
            {
                _strNodeName = SlaveName;

                for (int i = 0; i < 4; i++) 
                {
                    _lisJogAndTeachInfo.Add(new clsDataJogTeach() ) ;
                }

                for (int i = 0; i < 8; i++) 
                {
                    _lisDIOInfo.Add(new clsDataDIO() ) ;
                }

            }
            ~clsSlave() 
            {
                _lisJogAndTeachInfo.Clear() ;
                _lisDIOInfo.Clear() ;
            }

            #endregion

            /// <summary>複製元件資料</summary>
            /// <returns>回傳複製元件資料</returns>
            public clsSlave Clone(string p_strName) 
            {
                clsSlave m_clsSlave = new clsSlave(p_strName) ;
                m_clsSlave._lisJogAndTeachInfo = this.DeepCloneViaJson<clsDataJogTeach>(_lisJogAndTeachInfo) ;
                m_clsSlave._lisDIOInfo = this.DeepCloneViaJson<clsDataDIO>(_lisDIOInfo) ;
                return m_clsSlave;
            }

            /// <summary>序列化複製程序, 深複製</summary>
            public List<T> DeepCopy<T>(List<T> RealObject) 
            {

                using (Stream objectStream = new MemoryStream() ) 
                {
                    //利用 System.Runtime.Serialization序列化与反序列化完成引用對象的复制  
                    BinaryFormatter formatter = new BinaryFormatter() ;
                    formatter.Serialize(objectStream, RealObject) ;
                    objectStream.Seek(0, SeekOrigin.Begin) ;
                    return (List<T>) formatter.Deserialize(objectStream) ;
                }
            }

            /// <summary>
            /// 深層複製(需使用Json.Net組件) 
            /// </summary>
            /// <typeparam name="T">複製對象類別</typeparam>
            /// <param name="source">複製對象</param>
            /// <returns>複製品</returns>
            public List<T> DeepCloneViaJson<T>(List<T> source) 
            {
                if (source != null) 
                {
                    var jsonSerializerSettings = new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    var serializedObj = JsonConvert.SerializeObject(source, Formatting.Indented, jsonSerializerSettings) ;
                    return JsonConvert.DeserializeObject<List<T>>(serializedObj, jsonSerializerSettings) ;
                }
                else
                { return default(List<T>) ; }

            }

        }
        #endregion
    }
}

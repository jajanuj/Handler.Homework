using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace ArtEQ
{
    public class clsWriteLog
    {
        bool m_bEnable = true;
        string m_strLogDirPath = "D:\\Log\\Test\\";
        static Mutex m_muxWriteData = new Mutex();

        public clsWriteLog(string _path)
        {
            m_strLogDirPath = _path;
            if (!Directory.Exists(m_strLogDirPath))
            {
                Directory.CreateDirectory(m_strLogDirPath);
            }
        }

        public void WriteLog(string strLog)
        {
            if (!m_bEnable)
            {
                return;
            }

            try
            {
                try
                {
                    m_muxWriteData.WaitOne();
                    DateTime dt = DateTime.Now;

                    //---
                    char[] tmpChr = strLog.ToCharArray();
                    string newStr = "";
                    //---
                    for (int i = 0; i < tmpChr.Length; i++)
                    {
                        if ((int)tmpChr[i] < 32)
                            newStr += "[" + ((int)tmpChr[i]).ToString() + "]";
                        else
                            newStr += tmpChr[i];
                    }


                    string strLogFile = m_strLogDirPath + dt.Year.ToString() + "_" + dt.Month.ToString() + "_" + dt.Day.ToString() + ".txt";
                    string strWriteLog = "[" + dt.ToLongTimeString() + "]  " + newStr;

                    try
                    {
                        if (!File.Exists(strLogFile))
                        {
                            StreamWriter sw = File.CreateText(strLogFile);
                            sw.WriteLine(strWriteLog);
                            sw.Close();
                            sw.Dispose();
                        }
                        else
                        {
                            using (StreamWriter sw = File.AppendText(strLogFile))
                            {
                                sw.WriteLine(strWriteLog);
                                sw.Close();
                                sw.Dispose();
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                finally
                {
                    m_muxWriteData.ReleaseMutex();
                }
            }
            catch
            {

            }


        }


    }

    class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }


    class CsvFileWriter : StreamWriter
    {
        public CsvFileWriter(Stream stream)
            : base(stream)
        {
        }

        public CsvFileWriter(string filename)
            : base(filename)
        {
        }

        /// <summary>
        /// Writes a single row to a CSV file.
        /// </summary>
        /// <param name="row">The row to be written</param>
        public void WriteRow(CsvRow row)
        {
            StringBuilder builder = new StringBuilder();
            bool firstColumn = true;
            foreach (string value in row)
            {
                // Add separator if this isn't the first value
                if (!firstColumn)
                    builder.Append(',');
                // Implement special handling for values that contain comma or quote
                // Enclose in quotes and double up any double quotes
                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                    builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                else
                    builder.Append(value);
                firstColumn = false;
            }
            row.LineText = builder.ToString();
            WriteLine(row.LineText);
        }
    }

}

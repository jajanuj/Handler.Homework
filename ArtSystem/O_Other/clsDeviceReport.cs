using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using System.IO;

namespace ArtSystem
{
    public class clsDeviceReport
    {
        private static DateTime m_DateTime_ActiveTime = DateTime.MaxValue;
        public static string FirmwareDirectory = @"C:\AllringTech\FrameworkVersion";

        #region//========== 儲存的資料欄位 ==========
        /// <summary> 裝置名稱(可以隨意定義) </summary>
        public string DeviceName { get; set; }
        /// <summary> 裝置類別 </summary>
        public string DeviceType { get; set; }
        /// <summary> 韌體版本 </summary>
        public string FwVersion { get; set; }
        /// <summary> 硬體版本 </summary>
        public string HwVersion { get; set; }
        /// <summary> 其他資訊 </summary>
        public string ExtraInfo { get; set; }

        #endregion

        #region//========== public (Save, Load)==========
        private string FileFullPath = "";
        public void SaveInfoTcpIp(string ip, string port)
        {
            string sFileName = this.DeviceType + "_" + ip + "_" + port;
            Save_ByFileName(sFileName);
        }

        public void SaveInfoSerialPort(int comPort, int stationId)
        {
            string sFileName = this.DeviceType + "_Com" + comPort + "_Station" + stationId;
            Save_ByFileName(sFileName);
        }
        // 儲存與載入功能
        private void Save_ByFileName(string fileName)
        {
            try
            {
                if (DateTime.Now < m_DateTime_ActiveTime)
                { m_DateTime_ActiveTime = DateTime.Now; }
                if (!Directory.Exists(FirmwareDirectory))
                {
                    Directory.CreateDirectory(FirmwareDirectory);
                }
                string safeFileName = string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
                string fullPath = Path.Combine(FirmwareDirectory, safeFileName + ".txt");

                if (File.Exists(fullPath) == true)
                {
                    clsDeviceReport LoadData = new clsDeviceReport();
                    LoadData.Load(fullPath);
                    if (DeviceName == "" && LoadData.DeviceName != "")
                    { DeviceName = LoadData.DeviceName; }
                    if (DeviceType == "" && LoadData.DeviceType != "")
                    { DeviceType = LoadData.DeviceType; }
                    if (FwVersion == "" && LoadData.FwVersion != "")
                    { FwVersion = LoadData.FwVersion; }
                    if (HwVersion == "" && LoadData.HwVersion != "")
                    { HwVersion = LoadData.HwVersion; }
                    if (ExtraInfo == "" && LoadData.ExtraInfo != "")
                    { ExtraInfo = LoadData.ExtraInfo; }
                }

                List<string> AllData = new List<string>();
                AllData.Add(string.Format("DeviceName={0}", DeviceName));
                AllData.Add(string.Format("DeviceType={0}", DeviceType));
                AllData.Add(string.Format("FwVersion={0}", FwVersion));
                AllData.Add(string.Format("HwVersion={0}", HwVersion));
                AllData.Add(string.Format("ExtraInfo={0}", ExtraInfo));
                System.IO.File.WriteAllLines(fullPath, AllData);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }

        }

        public void Load(string fullPath)
        {
            try
            {
                FileFullPath = fullPath;
                if (!File.Exists(fullPath))
                {
                    Console.WriteLine("[Load Error] File not found.");
                    return;
                }
                string[] lines = File.ReadAllLines(fullPath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('=');
                    if (parts.Length != 2) continue;
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    switch (key)
                    {
                        case "DeviceName": DeviceName = value; break;
                        case "DeviceType": DeviceType = value; break;
                        case "FwVersion": FwVersion = value; break;
                        case "HwVersion": HwVersion = value; break;
                        case "ExtraInfo": ExtraInfo = value; break;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        public string GetFileName()
        {
            string rValue = "";
            try
            {
                rValue = System.IO.Path.GetFileName(FileFullPath);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        static public Dictionary<string, clsDeviceReport> GetAllReport()
        {
            Dictionary<string, clsDeviceReport> rValue = new Dictionary<string, clsDeviceReport>();
            try
            {
                List<string> AllFilePath = System.IO.Directory.GetFiles(FirmwareDirectory).ToList();
                foreach(string sFilePath in AllFilePath)
                {
                    if (System.IO.File.GetLastWriteTime(sFilePath) >= m_DateTime_ActiveTime.AddMinutes(-5))
                    {
                        clsDeviceReport AddItem = new clsDeviceReport();
                        AddItem.Load(sFilePath);
                        rValue.Add(sFilePath, AddItem);
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        #endregion
    }
}

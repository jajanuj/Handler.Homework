using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.Versioning;

namespace ArtMMI
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                bool is64Bit = Environment.Is64BitProcess;
                if (is64Bit)
                { g_strExternelDLLDirectory = @"D:\ArtDLL\ArtDLLx64"; }
                else
                { g_strExternelDLLDirectory = @"D:\ArtDLL\ArtDLLx86"; }
                if (InitialDomainDLL() == true)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new formMain());
                }
            }
            catch (Exception ex)
            {
                formMain.HandleUnhandledException(ex);
            }
        }


        #region//========== Public Static (連結外部DLL) ==========
        /// <summary> m_LstProjectDLL 不能使用外部路徑的DLL，(DLL版本管理) </summary>
        #region//不能使用外部路徑的DLL，(DLL版本管理)
        private static List<string> m_LstProjectDLL = new List<string>();
        #endregion

        /// <summary> 外部DLL掛載的絕對路徑 </summary>
        public static string g_strExternelDLLDirectory = @"D:\ArtDLL\";

        /// <summary> 初始化-更改載入DLL路徑功能，部分DLL需要載入專案 </summary>
        public static bool InitialDomainDLL()
        {
            bool rValue = true;
            try
            {
                m_LstProjectDLL.Clear();
                List<string> LstFiles = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory()).ToList<string>();
                foreach (string sFilePath in LstFiles)
                {
                    if (System.IO.Path.GetExtension(sFilePath) == ".dll")
                    {
                        m_LstProjectDLL.Add(System.IO.Path.GetFileName(sFilePath));
                    }
                }
                //string sMessage = "";
                //foreach (string sName in m_LstProjectDLL)
                //{
                //    if (SearchDLL(g_strExternelDLLDirectory, sName) != "")
                //    {
                //        sMessage += sName + "\r\n";
                //        rValue = false;
                //    }
                //}
                //if (rValue == false)
                //{
                //    MessageBox.Show("[Externel DLL Error]\r\n" + sMessage);
                //}
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            }
            catch
            {
                rValue = false;
            }
            return rValue;
        }

        /// <summary> 加載DLL的時候，會從此Function尋找DLL並載入 </summary>
        public static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // 檢查需要的程序集名稱，並返回正確的程序集路徑
            string assemblyName = new System.Reflection.AssemblyName(args.Name).Name;

            System.Reflection.AssemblyName d = new System.Reflection.AssemblyName(args.Name);
            if (m_LstProjectDLL.Contains(assemblyName + ".dll") == false)
            {
                string dllPath = SearchDLL(g_strExternelDLLDirectory, assemblyName);
                if (dllPath != "" && System.IO.File.Exists(dllPath))
                {
                    return System.Reflection.Assembly.UnsafeLoadFrom(dllPath);
                }
            }
            return null;
        }

        /// <summary> 尋找是否有替代的DLL </summary>
        private static string SearchDLL(string strDirectory, string FileName)
        {
            string rValue = strDirectory + "\\" + FileName + ".dll";
            rValue = rValue.Replace("\\\\", "\\");
            if (System.IO.Directory.Exists(strDirectory) == false)
            {
                rValue = "";
            }
            else if (System.IO.File.Exists(rValue) == false)
            {
                rValue = "";
                List<string> FolderList = System.IO.Directory.GetDirectories(strDirectory).ToList<string>();
                foreach (string Folder in FolderList)
                {
                    rValue = SearchDLL(Folder, FileName);
                    if (rValue != "")
                    {
                        break;
                    }
                }
            }
            return rValue.Replace("\\\\", "\\");
        }

        #endregion

    }
}

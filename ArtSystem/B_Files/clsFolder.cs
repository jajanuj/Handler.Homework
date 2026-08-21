using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using Shell32;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ArtSystem
{
    public class clsFolder
    {
        /// <summary> 確認資料夾路徑是t否存在如果不存在嘗試建立，建立失敗回傳false </summary>
        static public bool bCheckCreaeDirectory(string strDirectory)
        {
            bool rValue = false;
            if (System.IO.Directory.Exists(strDirectory) == false)
            {
                try
                {
                    System.IO.Directory.CreateDirectory(strDirectory);
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }
            if (System.IO.Directory.Exists(strDirectory) == true)
            {
                rValue = true;
            }
            return rValue;
        }

        /// <summary> 複製文件,可選擇是否要包含裡面的文件夾 </summary>
        static public bool CopyFolder(string Source, string Target, bool bCopySubFolder = true, bool SkipCopySameFiles = true)
        {
            bool rValue = false;
            try
            {
                if (bCopySubFolder == true)
                {
                    #region//複製資料夾內的所有資料夾
                    List<string> LstSubDirectory = System.IO.Directory.GetDirectories(Source).ToList<string>();
                    foreach (string strSourceSubDirectory in LstSubDirectory)
                    {
                        try
                        {
                            string strSubDirectoryName = System.IO.Path.GetFileName(strSourceSubDirectory);
                            string strTargetSubDirectory = Target + "\\" + strSubDirectoryName;
                            CopyFolder(strSourceSubDirectory, strTargetSubDirectory, bCopySubFolder, SkipCopySameFiles);
                        }
                        catch (Exception ex)
                        {
                            clsArtSystem.CatchLog(ex);
                        }
                    }
                    #endregion
                }
                if (bCheckCreaeDirectory(Source) == true
                    && bCheckCreaeDirectory(Target) == true)
                {
                    #region//複製資料夾內的所有文件
                    List<string> LstINIFiles = System.IO.Directory.GetFiles(Source).ToList<string>();
                    foreach (string strSourceFile in LstINIFiles)
                    {
                        try
                        {
                            string strFileName = System.IO.Path.GetFileName(strSourceFile);
                            string strTargetFile = Target + "\\" + strFileName;
                            if (System.IO.File.Exists(strTargetFile) == false)
                            {
                                System.IO.File.Copy(strSourceFile, strTargetFile);
                            }
                            else
                            {
                                if (System.IO.File.GetLastWriteTime(strSourceFile) != System.IO.File.GetLastWriteTime(strTargetFile)
                                    || SkipCopySameFiles == false)
                                {
                                    System.IO.File.Copy(strSourceFile, strTargetFile, true);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            clsArtSystem.CatchLog(ex);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        #region//DeleteFileToRecycleBin需要用到的宣告
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int wFunc;
            public string pFrom;
            public string pTo;
            public short fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static private extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);
        // SHQUERYRBINFO结构体
        [StructLayout(LayoutKind.Sequential)]
        private struct SHQUERYRBINFO
        {
            public int cbSize;
            public long i64Size;
            public long i64NumItems;
        }

        // SHFILEINFO结构体
        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }
        // P/Invoke 声明
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHQueryRecycleBin(string pszRootPath, ref SHQUERYRBINFO pSHQueryRBInfo);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        #endregion
        static public bool DeleteFileToRecycleBin(string filePath)
        {
            int FO_DELETE = 3;
            int FOF_ALLOWUNDO = 0x40;
            int FOF_NOCONFIRMATION = 0x10;
            SHFILEOPSTRUCT shFileOp = new SHFILEOPSTRUCT
            {
                wFunc = FO_DELETE,
                pFrom = filePath + '\0' + '\0', // 需要以双 '\0' 结尾
                fFlags = (short)(FOF_ALLOWUNDO | FOF_NOCONFIRMATION)
            };

            int result = SHFileOperation(ref shFileOp);

            return result == 0; // 返回值为 0 表示操作成功
        }

        ///// <summary>  DeleteOldFile_Days : 文件刪除日期的保留天數 </summary>
        //static public void DeleteOldFilesFromRecycleBin(uint DeleteOldFile_Days)
        //{
        //    DateTime cutoffDate = DateTime.Now.AddDays(-DeleteOldFile_Days);
        //    Type shellType = Type.GetTypeFromProgID("Shell.Application");
        //    dynamic shell = Activator.CreateInstance(shellType);
        //    Folder recycleBin = shell.NameSpace(10); // 10表示回收站

        //    List<FolderItem> itemsToDelete = new List<FolderItem>();

        //    foreach (FolderItem item in recycleBin.Items())
        //    {
        //        if (NeedDalete(recycleBin, item, DeleteOldFile_Days))
        //        {
        //            itemsToDelete.Add(item);
        //        }
        //    }

        //    foreach (var item in itemsToDelete)
        //    {
        //        DeleteFileWithoutConfirmation(item.Path);
        //        //item.InvokeVerb("delete");
        //        //Console.WriteLine("已删除回收站中的旧文件：" + item.Path);
        //    }
        //}
        //static private bool NeedDalete(Folder recycleBin, FolderItem item, uint DeleteOldFile_Days)
        //{
        //    bool rValue = false;
        //    // 根据删除时间的列索引获取删除时间
        //    string deletedTimeStr = recycleBin.GetDetailsOf(item, 2); // 2是Deleted Date的列索引
        //    Regex regex = new Regex(@"\d{4}\D+\d{1,2}\D+\d{1,2}");
        //    Match match = regex.Match(deletedTimeStr);
        //    if (match.Success == true)
        //    {
        //        string d = match.Value;
        //        Regex regex2 = new Regex(@"\d+");
        //        MatchCollection match2 = regex2.Matches(d);
        //        if (match2.Count == 3)
        //        {
        //            string yyyy = match2[0].Value;
        //            string MM = match2[1].Value;
        //            string dd = match2[2].Value;
        //            int iFileDeleteDate = Convert.ToInt32(yyyy) * 10000 + Convert.ToInt32(MM) * 100 + Convert.ToInt32(dd);
        //            DateTime NeedDeleteDate = DateTime.Now.AddDays(-DeleteOldFile_Days);
        //            int iNeedDeleteDate = NeedDeleteDate.Year * 10000 + NeedDeleteDate.Month * 100 + NeedDeleteDate.Day;
        //            if (iFileDeleteDate < iNeedDeleteDate)
        //            {
        //                rValue = true;
        //            }
        //        }
        //    }
        //    return rValue;
        //}
        //static private void DeleteFileWithoutConfirmation(string path)
        //{
        //    int FO_DELETE = 3;
        //    short FOF_NOCONFIRMATION = 0x10; // No confirmation dialog
        //    SHFILEOPSTRUCT fileOp = new SHFILEOPSTRUCT
        //    {
        //        wFunc = FO_DELETE,
        //        pFrom = path + '\0' + '\0', // Double null-terminated string
        //        fFlags = FOF_NOCONFIRMATION
        //    };

        //    SHFileOperation(ref fileOp);
        //}
    }
}

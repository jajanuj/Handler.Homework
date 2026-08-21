using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtSystem;

namespace ArtSystem
{
    public partial class ucRemoveFiles : ucBaseUserControl
    {
        #region //===================== 變數設置 =====================

        private string strFilePath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\RemoveFiles.ini";
        private bool bIsEditing = false;
        private List<clsRemoveItem> Lst_RemoveItems = new List<clsRemoveItem>();
        private string strLastDeleteDate = "";
        private string strLastDeleteDate_RecycleBin = "";
        private bool bNeedClearRecycleBinOldFiles = false;
        private uint iClearRecycleBinOldFiles_Days = 180;

        private List<string> Lst_FilsType = new List<string>()
        {
            "All",
            ".log",
            ".ini",
            ".txt",
            ".xml",
            ".bmp",
            ".jpeg",
            ".jpg",
        };

        #endregion

        #region //===================== Class 定義 =====================
        public class clsRemoveItem
        {
            public string strPath = "";
            public bool bEnable = false;
            public bool bDeleteSubFolder = false;
            public bool bToRecycleBin = false;
            public int iDays = 180;
            public List<string> RemoveItems = new List<string>();
            public string GetRemoveItems()
            {
                string rValue = ConvertListToString(RemoveItems);
                return rValue;
            }

            public clsRemoveItem()
            {
                bEnable = true;
                RemoveItems.Add(".log");
            }
            static public string ConvertListToString(List<string> LstItem)
            {
                string rValue = "";
                for (int i = 0; i < LstItem.Count; i++)
                {
                    if (i != 0)
                    {
                        rValue += ";";
                    }
                    rValue += LstItem[i];
                }
                return rValue;
            }
        }
        #endregion

        #region //===================== 必要函式設置 =====================

        static private object objLock = new object();
        static private ucRemoveFiles m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucRemoveFiles GetSingleton()
        {
            lock (objLock)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucRemoveFiles();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucRemoveFiles()
        {
            InitializeComponent();
            if (ArtSystem.clsArtSystem.bIsProgramOpen == false)
            {
                return;
            }
            this.VisibleChanged += new EventHandler(UserControl_VisibleChanged);
            LoadSetting();
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                txtPath.Text = strFilePath;
                cBox_ClearRecycleBinOldFiles.Checked = bNeedClearRecycleBinOldFiles;
                tBox_RecycleBinOldFiles_Days.Text = iClearRecycleBinOldFiles_Days.ToString();
                clsFolder.bCheckCreaeDirectory(System.IO.Path.GetDirectoryName(strFilePath));
                dataGridView1.EndEdit();
                dataGridView1.Enabled = btnAdd.Enabled = btnDelete.Enabled = btnSave.Enabled = btnCancel.Enabled = bIsEditing;
                tBox_RecycleBinOldFiles_Days.Enabled = cBox_ClearRecycleBinOldFiles.Enabled = bIsEditing;
                for (int i = dataGridView1.Rows.Count; i < Lst_RemoveItems.Count; i++)
                {
                    dataGridView1.Rows.Add();
                }
                for (int i = dataGridView1.Rows.Count - 1; i >= Lst_RemoveItems.Count; i--)
                {
                    dataGridView1.Rows.RemoveAt(i);
                }
                for (int i = 0; i < Lst_RemoveItems.Count; i++)
                {
                    dataGridView1[dgvEnable.Index, i].Value = Lst_RemoveItems[i].bEnable;
                    dataGridView1[dgvDeleteSubFolder.Index, i].Value = Lst_RemoveItems[i].bDeleteSubFolder;
                    dataGridView1[dgvToRecycleBin.Index, i].Value = Lst_RemoveItems[i].bToRecycleBin;
                    dataGridView1[dgvDays.Index, i].Value = Lst_RemoveItems[i].iDays.ToString();
                    dataGridView1[dgvDeletePath.Index, i].Value = Lst_RemoveItems[i].strPath;
                    dataGridView1[dgvItemName.Index, i].Value = Lst_RemoveItems[i].GetRemoveItems();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 自動更新介面參數 </summary>
        protected override void ReflashTimerFunc()
        {
            try
            {
                this.UC_FlashControl(btnSave, bIsEditing);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 進入此介面時,自動執行UpdateControls </summary>
        protected void UserControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                bIsEditing = false;
                LoadSetting();
                UpdateControls();
            }
        }


        #endregion

        #region//===================== Public 函式 =====================

        public void LoadSetting()
        {
            try
            {
                if (System.IO.File.Exists(strFilePath) == true)
                {
                    clsIniFile mINIFile = new clsIniFile(strFilePath);
                    string RecycleBin = "Recycle Bin";
                    bNeedClearRecycleBinOldFiles = mINIFile.GetString(RecycleBin, "bNeedClearRecycleBinOldFiles", bNeedClearRecycleBinOldFiles.ToString()) == true.ToString();
                    iClearRecycleBinOldFiles_Days = Convert.ToUInt32(mINIFile.GetString(RecycleBin, "iClearRecycleBinOldFiles_Days", iClearRecycleBinOldFiles_Days.ToString()));
                    List<string> SectionList = mINIFile.GetSectionNames().ToList<string>();
                    if (SectionList.Contains(RecycleBin) == true)
                    {
                        SectionList.Remove(RecycleBin);
                    }
                    for (int i = Lst_RemoveItems.Count; i < SectionList.Count - 1; i++)
                    {
                        Lst_RemoveItems.Add(new clsRemoveItem());
                    }
                    for (int i = Lst_RemoveItems.Count - 1; i >= SectionList.Count; i--)
                    {
                        Lst_RemoveItems.RemoveAt(i);
                    }
                    for (int i = 0; i < Lst_RemoveItems.Count; i++)
                    {
                        Lst_RemoveItems[i].strPath = mINIFile.GetString(SectionList[i], "strPath", "");
                        Lst_RemoveItems[i].bEnable = mINIFile.GetString(SectionList[i], "bEnable", false.ToString()) == true.ToString();
                        Lst_RemoveItems[i].bDeleteSubFolder = mINIFile.GetString(SectionList[i], "bDeleteSubFolder", false.ToString()) == true.ToString();
                        Lst_RemoveItems[i].bToRecycleBin = mINIFile.GetString(SectionList[i], "bToRecycleBin", false.ToString()) == true.ToString();
                        Lst_RemoveItems[i].iDays = Convert.ToInt32(mINIFile.GetString(SectionList[i], "iDays", "180"));
                        Lst_RemoveItems[i].RemoveItems.Clear();
                        Lst_RemoveItems[i].RemoveItems = mINIFile.GetString(SectionList[i], "RemoveItems", "").Split(';').ToList<string>();
                    }
                }
                else
                {
                    Lst_RemoveItems.Clear();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void SaveSetting()
        {
            try
            {
                System.IO.File.Delete(strFilePath);
                clsIniFile mINIFile = new clsIniFile(strFilePath);
                string RecycleBin = "Recycle Bin";
                mINIFile.WriteValue(RecycleBin, "bNeedClearRecycleBinOldFiles", bNeedClearRecycleBinOldFiles.ToString());
                mINIFile.WriteValue(RecycleBin, "iClearRecycleBinOldFiles_Days", iClearRecycleBinOldFiles_Days.ToString());
                string strSectionName = "";
                for (int i = 0; i < Lst_RemoveItems.Count; i++)
                {
                    strSectionName = "Remove Files" + (i + 1);
                    mINIFile.WriteValue(strSectionName, "strPath", Lst_RemoveItems[i].strPath);
                    mINIFile.WriteValue(strSectionName, "bEnable", Lst_RemoveItems[i].bEnable.ToString());
                    mINIFile.WriteValue(strSectionName, "bDeleteSubFolder", Lst_RemoveItems[i].bDeleteSubFolder.ToString());
                    mINIFile.WriteValue(strSectionName, "bToRecycleBin", Lst_RemoveItems[i].bToRecycleBin.ToString());
                    mINIFile.WriteValue(strSectionName, "iDays", Lst_RemoveItems[i].iDays);
                    mINIFile.WriteValue(strSectionName, "RemoveItems", Lst_RemoveItems[i].GetRemoveItems());
                }
                LoadSetting();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 依據設定路徑刪除文件 (會自動判斷是否要刪除檔案動作，每日凌晨3點鐘) </summary>
        static public void _DailyRemove()
        {
            if (m_Singleton != null)
            {
                string strNow = DateTime.Now.ToString("yyyyMMdd_HH");
                if (m_Singleton.strLastDeleteDate == strNow
                    || m_Singleton.strLastDeleteDate == "")
                {
                    m_Singleton.strLastDeleteDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "_03";
                    m_Singleton.ThreadRemoveFiles();
                }

                if (m_Singleton.strLastDeleteDate_RecycleBin == strNow
                    || m_Singleton.strLastDeleteDate_RecycleBin == "")
                {
                    m_Singleton.strLastDeleteDate_RecycleBin = DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "_06";
                    m_Singleton.ThreadRecycleBinFiles();
                }
            }
        }
        public void ThreadRemoveFiles()
        {
            System.Threading.Thread mThread = new System.Threading.Thread(new System.Threading.ThreadStart(DoRemove));
            mThread.Start();
        }
        public void ThreadRecycleBinFiles()
        {
            System.Threading.Thread mThread = new System.Threading.Thread(new System.Threading.ThreadStart(DoRemove_RecycleBin));
            mThread.Start();
        }

        #endregion

        #region//===================== Private 函式 =====================


        private void DoRemove()
        {
            foreach (clsRemoveItem pRemoveItem in Lst_RemoveItems)
            {
                RemoveFiles(pRemoveItem.strPath, pRemoveItem);
            }
        }
        private void DoRemove_RecycleBin()
        {
            //clsFolder.DeleteOldFilesFromRecycleBin(iClearRecycleBinOldFiles_Days);
        }
        private void RemoveFiles(string strPath, clsRemoveItem pRemoveItem)
        {
            if (pRemoveItem.bEnable == true)
            {
                if (System.IO.Directory.Exists(strPath) == true)
                {
                    DateTime DeleteDate = DateTime.Now.AddDays(-pRemoveItem.iDays);
                    List<string> LstFiles = System.IO.Directory.GetFiles(strPath).ToList<string>();
                    foreach (string strFile in LstFiles)
                    {
                        if (pRemoveItem.RemoveItems.Count > 0)
                        {
                            string FileExtension = System.IO.Path.GetExtension(strFile);
                            if (pRemoveItem.RemoveItems.Contains("All") == true
                                || pRemoveItem.RemoveItems.Contains(FileExtension) == true)
                            {
                                if (System.IO.File.GetLastWriteTime(strFile) < DeleteDate)
                                {
                                    if (pRemoveItem.bToRecycleBin == true)
                                    {
                                        clsFolder.DeleteFileToRecycleBin(strFile);
                                    }
                                    else
                                    {
                                        System.IO.File.Delete(strFile);
                                    }
                                }
                            }
                        }
                    }
                    if (pRemoveItem.bDeleteSubFolder == true)
                    {
                        List<string> LstFolder = System.IO.Directory.GetDirectories(strPath).ToList<string>();
                        foreach (string strFolder in LstFolder)
                        {
                            RemoveFiles(strFolder, pRemoveItem);
                            if (System.IO.Directory.GetDirectories(strFolder).Length == 0
                                && System.IO.Directory.GetFiles(strFolder).Length == 0)
                            {
                                if (pRemoveItem.bToRecycleBin == true)
                                {
                                    clsFolder.DeleteFileToRecycleBin(strFolder);
                                }
                                else
                                {
                                    System.IO.Directory.Delete(strFolder, true);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void UC_FlashControl(Control p_Control, bool Flash)
        {
            if (Flash == true)
            {
                if (DateTime.Now.Second % 2 == 1)
                {
                    p_Control.BackColor = Color.Lime;
                }
                else
                {
                    p_Control.BackColor = SystemColors.Control;
                    if (p_Control is Button)
                    {
                        ((Button)p_Control).UseVisualStyleBackColor = true;
                    }
                }
            }
            else
            {
                p_Control.BackColor = SystemColors.Control;
                if (p_Control is Button)
                {
                    ((Button)p_Control).UseVisualStyleBackColor = true;
                }
            }
        }

        #endregion

        #region//===================== 事件處理 =====================

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bIsEditing = !bIsEditing;
            LoadSetting();
            UpdateControls();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSetting();
            bIsEditing = false;
            UpdateControls();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadSetting();
            bIsEditing = false;
            UpdateControls();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog mFolderDialog = new FolderBrowserDialog();
            if(mFolderDialog.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.Directory.Exists(mFolderDialog.SelectedPath) == true)
                {
                    clsRemoveItem AddItem = new clsRemoveItem();
                    AddItem.strPath = mFolderDialog.SelectedPath;
                    Lst_RemoveItems.Add(AddItem);
                    UpdateControls();
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentCell != null)
            {
                int iRow = dataGridView1.CurrentCell.RowIndex;
                if (iRow >= 0 && iRow < Lst_RemoveItems.Count)
                {
                    Lst_RemoveItems.RemoveAt(iRow);
                    UpdateControls();
                }
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvDays.Index)
            {
                #region//選擇保留天數
                int iRow = e.RowIndex;
                if (iRow >= 0 && iRow < Lst_RemoveItems.Count)
                {
                    if (FormNumBox.GetSingleton().ShowDialog(this, Lst_RemoveItems[iRow].iDays.ToString(), 99999, 30, 0) == DialogResult.OK)
                    {
                        Lst_RemoveItems[iRow].iDays = Convert.ToInt32(FormNumBox.GetSingleton().NumBoxValue);
                        UpdateControls();
                    }
                }
                #endregion
            }
            else if (e.ColumnIndex == dgvDeletePath.Index)
            {
                #region//重新選擇路徑
                int iRow = e.RowIndex;
                if (iRow >= 0 && iRow < Lst_RemoveItems.Count)
                {
                    FolderBrowserDialog mFolderDialog = new FolderBrowserDialog();
                    mFolderDialog.SelectedPath = Lst_RemoveItems[iRow].strPath;
                    if (mFolderDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (System.IO.Directory.Exists(mFolderDialog.SelectedPath) == true)
                        {
                            Lst_RemoveItems[iRow].strPath = mFolderDialog.SelectedPath;
                            UpdateControls();
                        }
                    }
                }
                #endregion
            }
            else if (e.ColumnIndex == dgvItemName.Index)
            {
                #region//選擇副檔名
                int iRow = e.RowIndex;
                if (iRow >= 0 && iRow < Lst_RemoveItems.Count)
                {
                    List<string> SelectItems = new List<string>();
                    foreach (string strItem in Lst_RemoveItems[iRow].RemoveItems)
                    {
                        SelectItems.Add(strItem);
                    }
                    if (clsDialogShow.SelectMultiItem(clsLanguage.GetTranslation("File Extension", false), Lst_FilsType, ref SelectItems) == DialogResult.OK)
                    {
                        if (SelectItems.Contains("All") == true)
                        {
                            Lst_RemoveItems[iRow].RemoveItems.Clear();
                            Lst_RemoveItems[iRow].RemoveItems.Add("All");
                            UpdateControls();
                        }
                        else
                        {
                            Lst_RemoveItems[iRow].RemoveItems.Clear();
                            foreach (string strItem in SelectItems)
                            {
                                Lst_RemoveItems[iRow].RemoveItems.Add(strItem);
                            }
                            UpdateControls();
                        }
                    }
                }
                #endregion
            }
        }
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgvEnable.Index)
            {
                int iRow = e.RowIndex;
                if (iRow >= 0 && iRow < Lst_RemoveItems.Count)
                {
                    if (dataGridView1[e.ColumnIndex, e.RowIndex].Value is bool)
                    {
                        Lst_RemoveItems[iRow].bEnable = !Lst_RemoveItems[iRow].bEnable;
                        UpdateControls();
                    }
                }
            }
            else if (e.ColumnIndex == dgvDeleteSubFolder.Index)
            {
                int iRow = e.RowIndex;
                if (iRow >= 0 && iRow < Lst_RemoveItems.Count)
                {
                    if (dataGridView1[e.ColumnIndex, e.RowIndex].Value is bool)
                    {
                        Lst_RemoveItems[iRow].bDeleteSubFolder = !Lst_RemoveItems[iRow].bDeleteSubFolder;
                        UpdateControls();
                    }
                }
            }
            else if (e.ColumnIndex == dgvToRecycleBin.Index)
            {
                int iRow = e.RowIndex;
                if (iRow >= 0 && iRow < Lst_RemoveItems.Count)
                {
                    if (dataGridView1[e.ColumnIndex, e.RowIndex].Value is bool)
                    {
                        Lst_RemoveItems[iRow].bToRecycleBin = !Lst_RemoveItems[iRow].bToRecycleBin;
                        UpdateControls();
                    }
                }
            }
        }

        private void tBox_RecycleBinOldFiles_Days_Click(object sender, EventArgs e)
        {
            if (FormNumBox.GetSingleton().ShowDialog(this, this.iClearRecycleBinOldFiles_Days.ToString(), 30, 9999999, 0) == DialogResult.OK)
            {
                this.iClearRecycleBinOldFiles_Days = Convert.ToUInt32(FormNumBox.GetSingleton().NumBoxValue);
            }
            UpdateControls();
        }
        private void cBox_ClearRecycleBinOldFiles_Click(object sender, EventArgs e)
        {
            this.bNeedClearRecycleBinOldFiles = !this.bNeedClearRecycleBinOldFiles;
            UpdateControls();
        }
        #endregion
    }
}

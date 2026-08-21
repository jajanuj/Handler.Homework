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

namespace ArtEQ
{
    public partial class ucRecipeManager : UserControl
    {
        #region //=====================  區域變數設置 =====================
        string strViewDirectory = "";
        string strSelectRecipe = "";
        string strOldSelectRecipe = "";
        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucRecipeManager m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucRecipeManager GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucRecipeManager();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucRecipeManager()
        {
            InitializeComponent();
            if ( clsArtSystem.bIsProgramOpen == false)
            { return; }
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            RecipeName.ReadOnly = true;
            FilePath.ReadOnly = true;
            comTextInspRecipe.Text = ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe);
            comTextMapRecipePath.Text = strViewDirectory;
            if (System.IO.Directory.Exists(strViewDirectory) == true)
            {
                List<string> m_listProductName = new List<string>();
                foreach (var sFileName in System.IO.Directory.GetFiles(strViewDirectory))
                {
                    if (sFileName.Contains(".ini") == true)
                    {
                        m_listProductName.Add(sFileName);
                    }
                }
                dataGridView1.Rows.Clear();
                for (int i = 0; i < m_listProductName.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[RecipeName.Index, i].Value = System.IO.Path.GetFileName(m_listProductName[i]).Replace(".ini", "");
                    dataGridView1[FilePath.Index, i].Value = m_listProductName[i];
                }
            }
            else
            {
                dataGridView1.Rows.Clear();
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================
        /// <summary> 使用Form顯示 </summary>
        public void _ShowForm(bool Dialog = true)
        {
            Form mForm = new Form();
            this.Parent = mForm;
            this.Location = new Point(0, 0);
            this.Size = new System.Drawing.Size(654, 512);
            mForm.Size = new Size(this.Size.Width + 16, this.Size.Height + 39);
            mForm.StartPosition = FormStartPosition.CenterScreen;
            mForm.Text = clsLanguage.GetTranslation("Warning Message");
            mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
            mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
            mForm.SizeChanged += new EventHandler(mForm_SizeChanged);
            if (Dialog == true)
            {
                mForm.ShowDialog();
            }
            else
            {
                mForm.Show();
            }
        }

        /// <summary> 複製Recipe功能 </summary>
        public bool _CopyRecipe(string SourceFile, string TargetFile)
        {
            bool rValue = true;
            try
            {
                rValue = PublicDeclare.RecipeCopy(SourceFile, TargetFile);
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
            return rValue;
        }

        #endregion

        #region //===================== private 函式設置 =====================


        #endregion

        #region //===================== 以下為事件處理 =====================

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (dataGridView1.HitTest(e.X, e.Y).RowIndex >= 0)
                {
                    int iSelectRecipeIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                    strSelectRecipe = dataGridView1[RecipeName.Index, iSelectRecipeIndex].Value.ToString();
                    textSelRecipeName.Text = strSelectRecipe;
                }
            }
        }
        private void btnSetMapRecipetPath_Click(object sender, EventArgs e)
        {
            clsLog.Log(clsEnum.enuLogName.SystemLog, clsCmData.g_strNowUser + " " + this.Name + " - " + ((Control)sender).Name);
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                strViewDirectory = fbd.SelectedPath;
                UpdateControls();
            }
        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            string sNewFileName = "";
            if (clsDialogShow.InputString("Create New Recipe", "New Recipe Name", ref sNewFileName) == DialogResult.OK)
            {
                string sPath = strViewDirectory + "\\" + sNewFileName + ".ini";
                if (System.IO.File.Exists(sPath) == false)
                {
                    clsLog.Log(clsEnum.enuLogName.SystemLog, clsCmData.g_strNowUser + " " + this.Name + " - " + ((Control)sender).Name + ", Create New Recipe Path : " + sPath);
                    System.IO.File.Create(sPath);
                    UpdateControls();
                }
                else
                {
                    formMessageBox.Show(clsLanguage.GetTranslation("File Already Exist."), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void btnLoadProduct_Click(object sender, EventArgs e)
        {
            string sPath = strViewDirectory + "\\" + strSelectRecipe + ".ini";
            if (System.IO.File.Exists(sPath) == true)
            {
                clsLog.Log(clsEnum.enuLogName.SystemLog, clsCmData.g_strNowUser + " " + this.Name + " - " + ((Control)sender).Name + ", Load Recipe Path : " + sPath);
                ucParameter.SetFilePath(clsEnum.enuPmtType.Recipe.ToString(), sPath);
                UpdateControls();
            }
            else
            {
                formMessageBox.Show(clsLanguage.GetTranslation("File Dosn't Exist!"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelProduct_Click(object sender, EventArgs e)
        {
            string sPath = strViewDirectory + "\\" + strSelectRecipe + ".ini";
            if (System.IO.File.Exists(sPath) == true)
            {
                if (formMessageBox.Show("Are You Sure Want To Delete?", "Delete Recipe", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe) != sPath)
                    {
                        clsLog.Log(clsEnum.enuLogName.SystemLog, clsCmData.g_strNowUser + " " + this.Name + " - " + ((Control)sender).Name + ", Delete Recipe Path : " + sPath);
                        System.IO.File.Delete(sPath);
                        if (System.IO.Directory.Exists(sPath.Replace(".ini", "")) == true)//@1.0.0.47-12@
                        {
                            System.IO.Directory.Delete(sPath.Replace(".ini", ""), true);//@1.0.0.37-13@
                        }
                        UpdateControls();
                    }
                    else
                    {
                        formMessageBox.Show(clsLanguage.GetTranslation("File Is Using, Can't Delete."), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            else
            {
                formMessageBox.Show(clsLanguage.GetTranslation("File Dosn't Exist!"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnCopyProduct_Click(object sender, EventArgs e)
        {
            string sNewFileName = "";
            if (clsDialogShow.InputString("Copy New Recipe", "Copy Recipe Name", ref sNewFileName) == DialogResult.OK)
            {
                string sPathTarget = strViewDirectory + "\\" + sNewFileName + ".ini";
                string sPathSource = strViewDirectory + "\\" + strSelectRecipe + ".ini";
                if (System.IO.File.Exists(sPathTarget) == false)
                {
                    clsLog.Log(clsEnum.enuLogName.SystemLog, clsCmData.g_strNowUser + " " + this.Name + " - " + ((Control)sender).Name + ", Copy From Recipe Path : " + sPathSource);
                    clsLog.Log(clsEnum.enuLogName.SystemLog, clsCmData.g_strNowUser + " " + this.Name + " - " + ((Control)sender).Name + ", Copy New Recipe Path : " + sPathTarget);
                    if (_CopyRecipe(sPathSource, sPathTarget) == false)
                    {
                        clsLog.Log(clsEnum.enuLogName.SystemLog, clsCmData.g_strNowUser + " " + this.Name + " - " + ((Control)sender).Name + ", Copy Recipe Error");
                        formMessageBox.Show("Copy Recipe Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    UpdateControls();
                }
                else
                {
                    formMessageBox.Show(clsLanguage.GetTranslation("File Already Exist."), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void textSelRecipeName_TextChanged(object sender, EventArgs e)
        {
            if (strOldSelectRecipe != textSelRecipeName.Text)
            {
                strOldSelectRecipe = textSelRecipeName.Text;
                if (textSelRecipeName.Focused == true)
                {
                    string tRecipe = textSelRecipeName.Text;
                    string sPath = strViewDirectory + "\\" + tRecipe + ".ini";
                    if (System.IO.File.Exists(sPath) == true)
                    {
                        strSelectRecipe = tRecipe;
                        btnLoadProduct_Click(sender, null);
                    }
                }
            }
        }

        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GetSingleton().Parent = null;
        }
        private void mForm_Deactivate(object sender, EventArgs e)
        {
            Form mForm = (Form)sender;
            mForm.Close();
        }
        private void mForm_SizeChanged(object sender, EventArgs e)
        {
            if (ucParameter.GetSingleton().Parent != null)
            {
                ucRecipeManager.GetSingleton().Size = ucRecipeManager.GetSingleton().Parent.ClientSize;
            }
        }

        #endregion

    }
}

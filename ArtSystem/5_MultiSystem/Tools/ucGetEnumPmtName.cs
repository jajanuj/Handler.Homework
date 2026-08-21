using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtControlLib;
using ArtCommonLib;
using ArtData;
using ArtSystem;

namespace ArtSystem.MultiSystem
{
    public partial class ucGetEnumPmtName : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        private DialogResult mDialogResult = DialogResult.Cancel;
        private clsEnum.enuPmtName? m_enuPmtName = null;
        private System.Drawing.Size m_InitialSize = new Size();
        #endregion


        #region //=====================  必要函式設置 =====================

        static private ucGetEnumPmtName m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucGetEnumPmtName GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucGetEnumPmtName();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucGetEnumPmtName()
        {
            InitializeComponent();
            m_InitialSize = this.Size;
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            listBox1.Items.Clear();
            listBox1.Items.Add("");
            foreach (clsEnum.enuPmtName ePmt in Enum.GetValues(typeof(clsEnum.enuPmtName)))
            {
                listBox1.Items.Add(ePmt.ToString());
            }
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        public DialogResult _ShowFormDialog(ref clsEnum.enuPmtName? p_enuPmtName)
        {
            UpdateControls();
            m_enuPmtName = p_enuPmtName;
            if(p_enuPmtName != null)
            {
                if (listBox1.Items.Contains(p_enuPmtName.ToString()) == true)
                {
                    listBox1.SelectedItem = p_enuPmtName.ToString();
                }
            }
            Form mForm = new Form();
            mForm.Size = new Size(this.m_InitialSize.Width + 16, this.m_InitialSize.Height + 39);
            this.Parent = mForm;
            this.Dock = DockStyle.Fill;
            mForm.StartPosition = FormStartPosition.CenterScreen;
            mForm.Text = clsLanguage.GetTranslation("Get Enum Pmt Name");
            mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
            mForm.ShowDialog();
            if (mDialogResult == DialogResult.OK)
            {
                p_enuPmtName = m_enuPmtName;
            }
            return mDialogResult;
        }
        #endregion

        #region //===================== private 函式設置 () =====================


        #endregion

        #region//===================== 以下為事件處理 () =====================

        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GetSingleton().Parent = null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            mDialogResult = DialogResult.OK;
            if (listBox1.SelectedItem != null)
            {
                if (Enum.IsDefined(typeof(clsEnum.enuPmtName), listBox1.SelectedItem.ToString()) == true)
                {
                    m_enuPmtName = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), listBox1.SelectedItem.ToString());
                }
                else
                {
                    m_enuPmtName = null;
                }
            }
            else
            {
                m_enuPmtName = null;
            }
            if (GetSingleton().Parent is Form)
            {
                Form mForm = (Form)GetSingleton().Parent;
                mForm.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mDialogResult = DialogResult.Cancel;
            if (GetSingleton().Parent is Form)
            {
                Form mForm = (Form)GetSingleton().Parent;
                mForm.Close();
            }
        }
        #endregion




    }
}

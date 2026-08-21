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
    public partial class ucGetEnumDO : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        private DialogResult mDialogResult = DialogResult.Cancel;
        private clsEnum.enuDo? m_enuDO = null;
        private System.Drawing.Size m_InitialSize = new Size();
        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucGetEnumDO m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucGetEnumDO GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucGetEnumDO();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucGetEnumDO()
        {
            InitializeComponent();
            m_InitialSize = this.Size;
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
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

        public DialogResult _ShowFormDialog(ref clsEnum.enuDo? p_enuDO, bool AllEnum = false)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("");
            Dictionary<clsEnum.enuDo, string> DicDoName = clsDioMotion.GetDoName();
            foreach (clsEnum.enuDo eDO in Enum.GetValues(typeof(clsEnum.enuDo)))
            {
                if (AllEnum == false)
                {
                    if (clsDioMotion.mLst_DOEnum.Contains(eDO) == false)
                    {
                        continue;
                    }
                }
                if (DicDoName.ContainsKey(eDO) == true)
                {
                    listBox1.Items.Add(eDO.ToString() + "-(" + DicDoName[eDO] + ")");
                }
                else
                {
                    listBox1.Items.Add(eDO.ToString());
                }
            }
            //else
            //{
            //    foreach (clsEnum.enuDo eDO in DicDoName.Keys)
            //    {
            //        listBox1.Items.Add(eDO.ToString() + "-(" + DicDoName[eDO] + ")");
            //    }
            //}
            UpdateControls();
            m_enuDO = p_enuDO;
            listBox1.SelectedItem = null;
            if(p_enuDO != null)
            {
                string sDOItem = p_enuDO.ToString();
                if (AllEnum == false)
                {
                    if (DicDoName.ContainsKey((clsEnum.enuDo)p_enuDO) == true)
                    {
                        sDOItem += "-(" + DicDoName[(clsEnum.enuDo)p_enuDO] + ")";
                    }
                }
                if (listBox1.Items.Contains(sDOItem) == true)
                {
                    listBox1.SelectedItem = sDOItem;
                }
            }
            Form mForm = new Form();
            mForm.Size = new Size(this.m_InitialSize.Width + 16, this.m_InitialSize.Height + 39);
            this.Parent = mForm;
            this.Dock = DockStyle.Fill;
            mForm.StartPosition = FormStartPosition.CenterScreen;
            mForm.Text = clsLanguage.GetTranslation("Get DO Enum");
            mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
            mForm.ShowDialog();
            if (mDialogResult == DialogResult.OK)
            {
                p_enuDO = m_enuDO;
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
                string strSelectItem = listBox1.SelectedItem.ToString();
                strSelectItem = strSelectItem.Split('-')[0];
                if (Enum.IsDefined(typeof(clsEnum.enuDo), strSelectItem) == true)
                {
                    m_enuDO = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), strSelectItem);
                }
                else
                {
                    m_enuDO = null;
                }
            }
            else
            {
                m_enuDO = null;
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

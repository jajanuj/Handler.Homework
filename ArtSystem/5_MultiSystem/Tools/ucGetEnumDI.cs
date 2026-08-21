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
    public partial class ucGetEnumDI : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        private DialogResult mDialogResult = DialogResult.Cancel;
        private clsEnum.enuDi? m_enuDI = null;
        private System.Drawing.Size m_InitialSize = new Size();
        #endregion
        
        #region //=====================  必要函式設置 =====================

        static private ucGetEnumDI m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucGetEnumDI GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucGetEnumDI();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucGetEnumDI()
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

        public DialogResult _ShowFormDialog(ref clsEnum.enuDi? p_enuDI, bool AllEnum = false)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("");
            Dictionary<clsEnum.enuDi, string> DicDiName = clsDioMotion.GetDiName();
            foreach (clsEnum.enuDi eDI in Enum.GetValues(typeof(clsEnum.enuDi)))
            {
                if (AllEnum == false)
                {
                    if (clsDioMotion.mLst_DIEnum.Contains(eDI) == false)
                    {
                        continue;
                    }
                }
                if (DicDiName.ContainsKey(eDI) == true)
                {
                    listBox1.Items.Add(eDI.ToString() + "-(" + DicDiName[eDI] + ")");
                }
                else
                {
                    listBox1.Items.Add(eDI.ToString());
                }
            }
            //else
            //{
            //    foreach (clsEnum.enuDi eDI in DicDiName.Keys)
            //    {
            //        listBox1.Items.Add(eDI.ToString() + "-(" + DicDiName[eDI] + ")");
            //    }
            //}

            UpdateControls();
            m_enuDI = p_enuDI;
            if(p_enuDI != null)
            {
                string sDIItem = p_enuDI.ToString();
                if (AllEnum == false)
                {
                    if (DicDiName.ContainsKey((clsEnum.enuDi)p_enuDI) == true)
                    {
                        sDIItem += "-(" + DicDiName[(clsEnum.enuDi)p_enuDI] + ")";
                    }
                }
                if (listBox1.Items.Contains(sDIItem) == true)
                {
                    listBox1.SelectedItem = sDIItem;
                }
            }
            Form mForm = new Form();
            mForm.Size = new Size(this.m_InitialSize.Width + 16, this.m_InitialSize.Height + 39);
            this.Parent = mForm;
            this.Dock = DockStyle.Fill;
            mForm.StartPosition = FormStartPosition.CenterScreen;
            mForm.Text = clsLanguage.GetTranslation("Get DI Enum");
            mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
            mForm.ShowDialog();
            if (mDialogResult == DialogResult.OK)
            {
                p_enuDI = m_enuDI;
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
                if (Enum.IsDefined(typeof(clsEnum.enuDi), strSelectItem) == true)
                {
                    m_enuDI = (clsEnum.enuDi)Enum.Parse(typeof(clsEnum.enuDi), strSelectItem);
                }
                else
                {
                    m_enuDI = null;
                }
            }
            else
            {
                m_enuDI = null;
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

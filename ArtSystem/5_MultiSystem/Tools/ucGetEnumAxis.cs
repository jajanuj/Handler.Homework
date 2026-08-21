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
    public partial class ucGetEnumAxis : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        private DialogResult mDialogResult = DialogResult.Cancel;
        private clsEnum.enuAxis? m_enuAxis = null;
        private System.Drawing.Size m_InitialSize = new Size();
        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucGetEnumAxis m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucGetEnumAxis GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucGetEnumAxis();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucGetEnumAxis()
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

        public DialogResult _ShowFormDialog(ref clsEnum.enuAxis? p_enuAxis, bool p_bShowAllAxisID = false, List<clsEnum.enuAxis> p_LstShowAxis = null)
        {
            listBox1.Text = "";
            listBox1.SelectedIndex = -1;
            listBox1.Items.Clear();
            if (p_LstShowAxis == null)
            {
                listBox1.Items.Add("");
            }
            List<clsEnum.enuAxis> LstAxisID = clsDioMotion.mDic_AxisInfo.Keys.ToList<clsEnum.enuAxis>();
            foreach (clsEnum.enuAxis eAxis in LstAxisID)
            {
                if (p_bShowAllAxisID == false)
                {
                    if (clsDioMotion.mDic_AxisInfo.ContainsKey(eAxis) == false)
                    {
                        continue;
                    }
                }
                if (p_LstShowAxis != null)
                {
                    if (p_LstShowAxis.Contains(eAxis) == false)
                    {
                        continue;
                    }
                }
                listBox1.Items.Add(eAxis.ToString() + "-(" + clsLanguage.GetTranslation(eAxis.ToString(), false) + ")");
                if (p_enuAxis == eAxis)
                {
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }
            }
            UpdateControls();
            m_enuAxis = p_enuAxis;
            //listBox1.SelectedItem = null;
            Form mForm = new Form();
            int iHeight = this.m_InitialSize.Height;
            if (p_LstShowAxis != null)
            {
                iHeight = 13 * p_LstShowAxis.Count + 120;
            }
            mForm.Size = new Size(this.m_InitialSize.Width + 16, iHeight + 39);
            this.Parent = mForm;
            this.Dock = DockStyle.Fill;
            mForm.StartPosition = FormStartPosition.CenterScreen;
            mForm.Text = clsLanguage.GetTranslation("Get Axis Enum");
            mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
            mForm.ShowDialog();
            if (mDialogResult == DialogResult.OK)
            {
                p_enuAxis = m_enuAxis;
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
                if (Enum.IsDefined(typeof(clsEnum.enuAxis), strSelectItem) == true)
                {
                    m_enuAxis = (clsEnum.enuAxis)Enum.Parse(typeof(clsEnum.enuAxis), strSelectItem);
                }
                else
                {
                    m_enuAxis = null;
                }
            }
            else
            {
                m_enuAxis = null;
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

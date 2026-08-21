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
    public partial class ucSetGantry_Etel : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        private List<int> p_LstGantryCard = null;
        private int iCardNum = 0;
        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucSetGantry_Etel m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucSetGantry_Etel GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucSetGantry_Etel();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucSetGantry_Etel()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                ConvertDataToUI();
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

        public void _ShowFormDialog( int CardNum, ref List<int> pLst_GantryCard)
        {
            iCardNum = CardNum;
            p_LstGantryCard = pLst_GantryCard;
            UpdateControls();
            Form mForm = new Form();
            this.Parent = mForm;
            this.Location = new Point(0, 0);
            mForm.Size = new Size(this.Size.Width + 16, this.Size.Height + 39);
            mForm.StartPosition = FormStartPosition.CenterScreen;
            mForm.Text = clsLanguage.GetTranslation("Set Gantry (Etel)");
            mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
            mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
            mForm.ShowDialog();
            ConvertUIToData();
        }

        #endregion

        #region //===================== private 函式設置 () =====================

        private void ConvertDataToUI()
        {
            try
            {
                #region//將Row行數調整成與mCardInfo.mLstMotionCardInfo.Count相等
                if (dgvEtelGantry.Rows.Count > iCardNum)
                {
                    int RemoveRowCount = dgvEtelGantry.Rows.Count - iCardNum;
                    for (int i = 0; i < RemoveRowCount; i++)
                    {
                        dgvEtelGantry.Rows.RemoveAt(dgvEtelGantry.Rows.Count - 1);
                    }
                }
                else if (dgvEtelGantry.Rows.Count < iCardNum)
                {
                    int AddRowCount = iCardNum - dgvEtelGantry.Rows.Count;
                    for (int i = 0; i < AddRowCount; i++)
                    {
                        dgvEtelGantry.Rows.Add();
                    }
                }
                #endregion
                for (int iRow = 0; iRow < iCardNum; iRow++)
                {
                    dgvEtelGantry[dgvEtelGantry_CardNo.Index, iRow].Value = (iRow + 1).ToString();
                    if (p_LstGantryCard.Contains(iRow) == true)
                    {
                        dgvEtelGantry[dgvEtelGantry_Enable.Index, iRow].Value = true;
                    }
                    else
                    {
                        dgvEtelGantry[dgvEtelGantry_Enable.Index, iRow].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void ConvertUIToData()
        {
            try
            {
                dgvEtelGantry.EndEdit();
                p_LstGantryCard.Clear();
                dgvEtelGantry.Refresh();
                for (int iRow = 0; iRow < iCardNum; iRow++)
                {
                    if (Convert.ToBoolean(dgvEtelGantry[dgvEtelGantry_Enable.Index, iRow].Value) == true)
                    {
                        p_LstGantryCard.Add(iRow);
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region//===================== 以下為事件處理 () =====================

        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GetSingleton().Parent = null;
        }
        private void mForm_Deactivate(object sender, EventArgs e)
        {
            Form mForm = (Form)sender;
            mForm.Close();
        }

        #endregion

    }
}

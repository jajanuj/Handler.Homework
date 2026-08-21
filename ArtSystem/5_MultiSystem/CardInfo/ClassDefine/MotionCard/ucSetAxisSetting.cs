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
    public partial class ucSetAxisSetting : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        private List<clsAxisSetting>  m_LstAxisSetting = null;
        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucSetAxisSetting m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucSetAxisSetting GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucSetAxisSetting();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucSetAxisSetting()
        {
            InitializeComponent();
            initialSize = this.Size;
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


        #region //========== ShowForm 函式設置 ==========

        private Size initialSize = new Size();
        /// <summary> 使用Form顯示 </summary>
        public void _ShowFormDialog(ref List<clsAxisSetting> p_LstAxisSetting)
        {
            try
            {
                m_LstAxisSetting = p_LstAxisSetting;
                UpdateControls();
                Form mForm = new Form();
                this.Parent = mForm;
                this.Location = new Point(0, 0);
                this.Dock = DockStyle.Fill;
                mForm.WindowState = FormWindowState.Normal;
                mForm.ClientSize = this.initialSize;
                mForm.StartPosition = FormStartPosition.CenterScreen;
                mForm.Text = clsLanguage.GetTranslation(this.Name, false);
                mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
                mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
                this.Parent = mForm;
                this.SetReflashTimerStart(true);
                this.Dock = DockStyle.Fill;
                mForm.ShowDialog();
                ConvertUIToData();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.SetReflashTimerStart(false);
                this.Parent = null;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void mForm_Deactivate(object sender, EventArgs e)
        {
            try
            {
                this.SetReflashTimerStart(false);
                this.Parent = null;
                Form mForm = (Form)sender;
                mForm.Close();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion
        #region //===================== public 函式設置 =====================

        #endregion

        #region //===================== private 函式設置 () =====================

        private void ConvertDataToUI()
        {
            try
            {
                #region//將Row行數調整成與m_LstAxisSetting.Count相等
                for (int i = dgvEtelGantry.Rows.Count; i < m_LstAxisSetting.Count; i++)
                {
                    dgvEtelGantry.Rows.Add();
                }
                for (int i = dgvEtelGantry.Rows.Count-1; i >= m_LstAxisSetting.Count; i--)
                {
                    dgvEtelGantry.Rows.RemoveAt(i);
                }
                #endregion
                for (int iRow = 0; iRow < m_LstAxisSetting.Count; iRow++)
                {
                    string sAxisID = "None";
                    int iAxisID = -1;
                    if (m_LstAxisSetting[iRow].p_enuAxis != null)
                    {
                        iAxisID = (int)m_LstAxisSetting[iRow].p_enuAxis;
                        sAxisID = m_LstAxisSetting[iRow].p_enuAxis.ToString(); 
                    }
                    dgvEtelGantry[dgvNo.Index, iRow].Value = iAxisID.ToString();
                    dgvEtelGantry[dgvAxisID.Index, iRow].Value = sAxisID;
                    dgvEtelGantry[dgvAxisName.Index, iRow].Value = clsLanguage.GetTranslation(sAxisID, false);
                    dgvEtelGantry[dgvAxisLogic1.Index, iRow].Value = m_LstAxisSetting[iRow].bLogic_MotionInvert;
                    dgvEtelGantry[dgvABSEncoder.Index, iRow].Value = m_LstAxisSetting[iRow].bABSEncoder;
                    dgvEtelGantry[dgvAxisDisableINP.Index, iRow].Value = m_LstAxisSetting[iRow].bDisableINP;
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
                dgvEtelGantry.Refresh();
                for (int iRow = 0; iRow < m_LstAxisSetting.Count; iRow++)
                {
                    m_LstAxisSetting[iRow].bLogic_MotionInvert = Convert.ToBoolean(dgvEtelGantry[dgvAxisLogic1.Index, iRow].Value);
                    m_LstAxisSetting[iRow].bABSEncoder = Convert.ToBoolean(dgvEtelGantry[dgvABSEncoder.Index, iRow].Value);
                    m_LstAxisSetting[iRow].bDisableINP = Convert.ToBoolean(dgvEtelGantry[dgvAxisDisableINP.Index, iRow].Value);
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

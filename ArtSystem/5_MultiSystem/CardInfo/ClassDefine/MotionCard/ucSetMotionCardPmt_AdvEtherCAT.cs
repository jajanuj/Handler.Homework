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
    public partial class ucSetMotionCardPmt_AdvEtherCAT : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        private List<clsMotionCardPmt_AdvEtherCAT> pListCardPmt = null;
        private int iAxisNum = 0;
        private clsEnum.enuAxis? eStartAxis = null;
        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucSetMotionCardPmt_AdvEtherCAT m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucSetMotionCardPmt_AdvEtherCAT GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucSetMotionCardPmt_AdvEtherCAT();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucSetMotionCardPmt_AdvEtherCAT()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }

            dgvHomeMode.Items.Clear();
            foreach (clsMotionCardPmt_AdvEtherCAT.enuHomeMode eHomeMode in Enum.GetValues(typeof(clsMotionCardPmt_AdvEtherCAT.enuHomeMode)))
            {
                dgvHomeMode.Items.Add(eHomeMode.ToString());
            }
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

        public void _ShowFormDialog(clsEnum.enuAxis eStartAxisID, ref List<clsMotionCardPmt_AdvEtherCAT> p_CardPmt, ArtControlLib.enuSlaveType_Motion eSlaveType)
        {
            iAxisNum = EtherCATMotionSlaveNum(eSlaveType);
            eStartAxis = eStartAxisID;
            pListCardPmt = p_CardPmt;
            for (int i = pListCardPmt.Count; i < iAxisNum; i++)
            { pListCardPmt.Add(new clsMotionCardPmt_AdvEtherCAT()); }
            UpdateControls();
            Form mForm = new Form();
            this.Parent = mForm;
            this.Location = new Point(0, 0);
            mForm.Size = new Size(this.Size.Width + 16, this.Size.Height + 39);
            mForm.StartPosition = FormStartPosition.CenterScreen;
            mForm.Text = clsLanguage.GetTranslation("Set Card Parameter");
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
                if (dgvSetHommingPmt_TPM.Rows.Count > iAxisNum)
                {
                    int RemoveRowCount = dgvSetHommingPmt_TPM.Rows.Count - iAxisNum;
                    for (int i = 0; i < RemoveRowCount; i++)
                    {
                        dgvSetHommingPmt_TPM.Rows.RemoveAt(dgvSetHommingPmt_TPM.Rows.Count - 1);
                    }
                }
                else if (dgvSetHommingPmt_TPM.Rows.Count < iAxisNum)
                {
                    int AddRowCount = iAxisNum - dgvSetHommingPmt_TPM.Rows.Count;
                    for (int i = 0; i < AddRowCount; i++)
                    {
                        dgvSetHommingPmt_TPM.Rows.Add();
                    }
                }
                #endregion
                for (int iRow = 0; iRow < iAxisNum; iRow++)
                {
                    if (iRow < pListCardPmt.Count)
                    {
                        #region//Homming Pmt
                        dgvSetHommingPmt_TPM[dgvAxisID_Homming.Index, iRow].Value = ((int)(eStartAxis + iRow)).ToString();
                        dgvSetHommingPmt_TPM[dgvAxisEnum_Homming.Index, iRow].Value = ((clsEnum.enuAxis)(eStartAxis + iRow)).ToString();
                        foreach (clsMotionCardPmt_AdvEtherCAT.enuPmtName ePmt in Enum.GetValues(typeof(clsMotionCardPmt_AdvEtherCAT.enuPmtName)))
                        {
                            int iValue = pListCardPmt[iRow].mPmtValue[ePmt];
                            switch (ePmt)
                            {
                                case clsMotionCardPmt_AdvEtherCAT.enuPmtName.PRA_HOME_MODE:
                                    {
                                        clsMotionCardPmt_AdvEtherCAT.enuHomeMode eHomeMode = (clsMotionCardPmt_AdvEtherCAT.enuHomeMode)Enum.ToObject(typeof(clsMotionCardPmt_AdvEtherCAT.enuHomeMode), iValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_AdvEtherCAT.enuHomeMode), eHomeMode) == false)
                                        {
                                            eHomeMode = clsMotionCardPmt_AdvEtherCAT.enuHomeMode.SearchOrg;
                                            pListCardPmt[iRow].mPmtValue[ePmt] = (int)eHomeMode;
                                        }
                                        dgvSetHommingPmt_TPM[dgvHomeMode.Index, iRow].Value = eHomeMode.ToString();
                                    }
                                    break;
                                case clsMotionCardPmt_AdvEtherCAT.enuPmtName.PRA_HOME_VM:
                                    dgvSetHommingPmt_TPM[dgvHomeVM.Index, iRow].Value = iValue;
                                    break;
                                case clsMotionCardPmt_AdvEtherCAT.enuPmtName.PRA_HOME_VO:
                                    dgvSetHommingPmt_TPM[dgvHomeVO.Index, iRow].Value = iValue;
                                    break;
                                case clsMotionCardPmt_AdvEtherCAT.enuPmtName.PRA_HOME_ACC:
                                    dgvSetHommingPmt_TPM[dgvHomeAcc.Index, iRow].Value = iValue;
                                    break;

                                default:
                                    break;
                            }
                        }
                        #endregion
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
                dgvSetHommingPmt_TPM.EndEdit();
                dgvSetHommingPmt_TPM.Refresh();
                for (int iRow = 0; iRow < iAxisNum; iRow++)
                {
                    if (iRow < pListCardPmt.Count)
                    {
                        #region//Homming Pmt
                        foreach (clsMotionCardPmt_AdvEtherCAT.enuPmtName ePmt in Enum.GetValues(typeof(clsMotionCardPmt_AdvEtherCAT.enuPmtName)))
                        {
                            switch (ePmt)
                            {
                                case clsMotionCardPmt_AdvEtherCAT.enuPmtName.PRA_HOME_MODE:
                                    {
                                        if (dgvSetHommingPmt_TPM[dgvHomeMode.Index, iRow].Value == null)
                                        {
                                            dgvSetHommingPmt_TPM[dgvHomeMode.Index, iRow].Value = clsMotionCardPmt_AdvEtherCAT.enuHomeMode.SearchOrg.ToString();
                                        }
                                        string sValue = dgvSetHommingPmt_TPM[dgvHomeMode.Index, iRow].Value.ToString();
                                        clsMotionCardPmt_AdvEtherCAT.enuHomeMode eHomeMode = (clsMotionCardPmt_AdvEtherCAT.enuHomeMode)Enum.Parse(typeof(clsMotionCardPmt_AdvEtherCAT.enuHomeMode), sValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_AdvEtherCAT.enuHomeMode), eHomeMode) == false)
                                        {
                                            eHomeMode = clsMotionCardPmt_AdvEtherCAT.enuHomeMode.SearchOrg;
                                            dgvSetHommingPmt_TPM[dgvHomeMode.Index, iRow].Value = eHomeMode.ToString();
                                        }
                                        pListCardPmt[iRow].mPmtValue[ePmt] = (int)eHomeMode;
                                    }
                                    break;
                                case clsMotionCardPmt_AdvEtherCAT.enuPmtName.PRA_HOME_VM:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = Convert.ToInt32(dgvSetHommingPmt_TPM[dgvHomeVM.Index, iRow].Value);
                                    break;
                                case clsMotionCardPmt_AdvEtherCAT.enuPmtName.PRA_HOME_VO:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = Convert.ToInt32(dgvSetHommingPmt_TPM[dgvHomeVO.Index, iRow].Value);
                                    break;
                                case clsMotionCardPmt_AdvEtherCAT.enuPmtName.PRA_HOME_ACC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = Convert.ToInt32(dgvSetHommingPmt_TPM[dgvHomeAcc.Index, iRow].Value);
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private int EtherCATMotionSlaveNum(enuSlaveType_Motion eSlaveType)
        {
            int rValue = 1;
            switch (eSlaveType)
            {
                case enuSlaveType_Motion.Motion_1xAxis:
                    rValue = 1;
                    break;
                case enuSlaveType_Motion.Motion_2xAxis:
                    rValue = 2;
                    break;
                case enuSlaveType_Motion.Motion_4xAxis:
                    rValue = 4;
                    break;
                case enuSlaveType_Motion.Motion_8xAxis:
                    rValue = 8;
                    break;
                default:
                    break;
            }
            return rValue;
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
        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView mDgv = (DataGridView)sender;
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (mDgv.CurrentCell is DataGridViewTextBoxCell) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

    }
}

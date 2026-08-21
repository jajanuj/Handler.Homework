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
    public partial class ucBottomCCDSetting : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public clsPmtBottomCCD mPmt = new clsPmtBottomCCD();
        private int iDeviceNum
        {
            get
            {
                if (mPmt != null)
                {
                    return mPmt.mDic_mPmtValue.Count;
                }
                return 0;
            }
        }
        private bool bIsEditing = false;
        #endregion

        #region //=====================  必要函式設置 =====================

        static object m_LockObj = new object();
        static private ucBottomCCDSetting m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucBottomCCDSetting GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucBottomCCDSetting();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucBottomCCDSetting()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }

            dgvCCDType.Items.Clear();
            foreach (clsCtrlBottomCCD.enuCCDType eEnum in Enum.GetValues(typeof(clsCtrlBottomCCD.enuCCDType)))
            {
                dgvCCDType.Items.Add(eEnum.ToString());
            }
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                if (this.Parent != null)
                {
                    this.Size = this.Parent.ClientSize;
                }
                txt_BottomCCDPath.Text = mPmt.sINIPath;
                dgvSetBottomCCD.Enabled = bIsEditing;
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
                SetBtnColorFlash(btnSave_BottomCCDSetting, bIsEditing);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        public void _ShowFormDialog()
        {
            UpdateControls();
            Form mForm = new Form();
            this.Parent = mForm;
            this.Location = new Point(0, 0);
            mForm.Size = new Size(this.Size.Width + 16, this.Size.Height + 39);
            mForm.StartPosition = FormStartPosition.CenterScreen;
            mForm.Text = clsLanguage.GetTranslation("Set BottomCCD Parameter");
            mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
            mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
            mForm.ShowDialog();
            ConvertUIToData();
        }

        #endregion

        #region //===================== private 函式設置 () =====================

        private void SetBtnColorFlash(Button pButton, bool Flash)
        {
            if (Flash && DateTime.Now.Second % 2 == 1)
            {
                pButton.BackColor = Color.Lime;
            }
            else
            {
                pButton.BackColor = this.BackColor;
                pButton.UseVisualStyleBackColor = true;
            }
        }
        private void ConvertDataToUI()
        {
            try
            {
                #region//將Row行數調整成與mCardInfo.mLstMotionCardInfo.Count相等
                if (dgvSetBottomCCD.Rows.Count > iDeviceNum)
                {
                    int RemoveRowCount = dgvSetBottomCCD.Rows.Count - iDeviceNum;
                    for (int i = 0; i < RemoveRowCount; i++)
                    {
                        dgvSetBottomCCD.Rows.RemoveAt(dgvSetBottomCCD.Rows.Count - 1);
                    }
                }
                else if (dgvSetBottomCCD.Rows.Count < iDeviceNum)
                {
                    int AddRowCount = iDeviceNum - dgvSetBottomCCD.Rows.Count;
                    for (int i = 0; i < AddRowCount; i++)
                    {
                        dgvSetBottomCCD.Rows.Add();
                    }
                }
                #endregion
                for (int iRow = 0; iRow < iDeviceNum; iRow++)
                {
                    if (iRow < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtBottomCCD.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iRow).Value;
                        foreach (clsPmtBottomCCD.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtRollerMotor.enuPmtName)))
                        {
                            if (pDicPmt.ContainsKey(ePmt) == false)
                            {
                                pDicPmt.Add(ePmt, "0");
                            }
                            if (pDicPmt[ePmt] == "")
                            {
                                pDicPmt[ePmt] = "0";
                            }
                        }
                        dgvSetBottomCCD[dgvNo.Index, iRow].Value = (iRow + 1).ToString();
                        dgvSetBottomCCD[dgvSensorName.Index, iRow].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;
                        dgvSetBottomCCD[dgvCCDType.Index, iRow].Value = pDicPmt[clsPmtBottomCCD.enuPmtName.CCDType];
                        dgvSetBottomCCD[dgvTcpIp.Index, iRow].Value = pDicPmt[clsPmtBottomCCD.enuPmtName.TCP_IP];
                        dgvSetBottomCCD[dgvTcpPort.Index, iRow].Value = pDicPmt[clsPmtBottomCCD.enuPmtName.TCP_Port];
                        dgvSetBottomCCD[dgvTimeout.Index, iRow].Value = pDicPmt[clsPmtBottomCCD.enuPmtName.TimeOut_ms];
                        dgvSetBottomCCD[dgvDelayTime.Index, iRow].Value = pDicPmt[clsPmtBottomCCD.enuPmtName.DelayTime_ms];
                        dgvSetBottomCCD[dgvSavePath.Index, iRow].Value = pDicPmt[clsPmtBottomCCD.enuPmtName.SavePath];
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
                dgvSetBottomCCD.EndEdit();
                dgvSetBottomCCD.Refresh();
                for (int iRow = 0; iRow < iDeviceNum; iRow++)
                {
                    if (iRow < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtBottomCCD.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iRow).Value;
                        foreach (clsPmtBottomCCD.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtRollerMotor.enuPmtName)))
                        {
                            if (pDicPmt.ContainsKey(ePmt) == false)
                            {
                                pDicPmt.Add(ePmt, "0");
                            }
                            if (pDicPmt[ePmt] == "")
                            {
                                pDicPmt[ePmt] = "0";
                            }
                        }
                        dgvSetBottomCCD[dgvNo.Index, iRow].Value = (iRow + 1).ToString();
                        dgvSetBottomCCD[dgvSensorName.Index, iRow].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;

                        pDicPmt[clsPmtBottomCCD.enuPmtName.CCDType] = ToString(dgvSetBottomCCD[dgvCCDType.Index, iRow].Value);
                        pDicPmt[clsPmtBottomCCD.enuPmtName.TCP_IP] = ToString(dgvSetBottomCCD[dgvTcpIp.Index, iRow].Value);
                        pDicPmt[clsPmtBottomCCD.enuPmtName.TCP_Port] = ToString(dgvSetBottomCCD[dgvTcpPort.Index, iRow].Value);
                        pDicPmt[clsPmtBottomCCD.enuPmtName.TimeOut_ms] = ToString(dgvSetBottomCCD[dgvTimeout.Index, iRow].Value);
                        pDicPmt[clsPmtBottomCCD.enuPmtName.DelayTime_ms] = ToString(dgvSetBottomCCD[dgvDelayTime.Index, iRow].Value);
                        pDicPmt[clsPmtBottomCCD.enuPmtName.SavePath] = ToString(dgvSetBottomCCD[dgvSavePath.Index, iRow].Value);

                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private string ToString(object sender)
        {
            string rValue = "";
            if (sender != null)
            {
                rValue = sender.ToString();
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
                if (mDgv.CurrentCell.ColumnIndex != dgvTcpIp.Index
                    && mDgv.CurrentCell.ColumnIndex != dgvSavePath.Index)
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
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

        private void ucBottomCCDSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                bIsEditing = false;
                UpdateControls();
            }
            this.SetReflashTimerStart(this.Visible);
        }

        private void dgvSetBottomCCD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView mDgv = (DataGridView)sender;
            if (mDgv.CurrentCell is DataGridViewTextBoxCell) //Desired Column
            {
                if (e.RowIndex == mDgv.CurrentCell.RowIndex
                    && e.ColumnIndex == mDgv.CurrentCell.ColumnIndex)
                {
                    if (e.RowIndex >= 0
                        && e.RowIndex < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtBottomCCD.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(mDgv.CurrentCell.RowIndex).Value;
                    }
                }
            }
            dgvSetBottomCCD.EndEdit();
            ConvertUIToData();

        }
        #endregion

        #region//===================== 以下為事件處理 (Edit, Cancel, Save, Add, Delete) =====================

        private void btnEdit_BottomCCDSetting_Click(object sender, EventArgs e)
        {
            bIsEditing = !bIsEditing;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnSave_BottomCCDSetting_Click(object sender, EventArgs e)
        {
            if (formMessageBox.Show("Save setting need to restart program.", "Save Bottom CCD Setting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConvertUIToData();
                bIsEditing = false;
                mPmt.Save(mPmt.sINIPath);
                UpdateControls();
                clsMultiSystem.SetSettingChangeFlag();
            }
        }
        private void btnCancel_BottomCCDSetting_Click(object sender, EventArgs e)
        {

            bIsEditing = false;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnAdd_BottomCCDSetting_Click(object sender, EventArgs e)
        {
            string NewName  = "";
            if (clsDialogShow.InputString("Add Bottom CCD", "New Sensor Name :", ref NewName) == DialogResult.OK)
            {
                if (mPmt.mDic_mPmtValue.ContainsKey(NewName) == true)
                {
                    formMessageBox.Show("Name Already Exist : \r\n" + NewName);
                    return;
                }
                mPmt.mDic_mPmtValue.Add(NewName, new Dictionary<clsPmtBottomCCD.enuPmtName, string>());
                foreach (clsPmtBottomCCD.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtBottomCCD.enuPmtName)))
                {
                    if (mPmt.mDic_mPmtValue[NewName].ContainsKey(ePmt) == false)
                    {
                        mPmt.mDic_mPmtValue[NewName].Add(ePmt, "0");
                    }
                }
                mPmt.mDic_mPmtValue[NewName][clsPmtBottomCCD.enuPmtName.SensorName] = NewName;
                mPmt.mDic_mPmtValue[NewName][clsPmtBottomCCD.enuPmtName.CCDType] = clsCtrlBottomCCD.enuCCDType.IV3.ToString();
                mPmt.mDic_mPmtValue[NewName][clsPmtBottomCCD.enuPmtName.TCP_IP] = "192.168.1.1";
                mPmt.mDic_mPmtValue[NewName][clsPmtBottomCCD.enuPmtName.TCP_Port] = "1004";
                mPmt.mDic_mPmtValue[NewName][clsPmtBottomCCD.enuPmtName.TimeOut_ms] = "3000";
                mPmt.mDic_mPmtValue[NewName][clsPmtBottomCCD.enuPmtName.DelayTime_ms] = "0";
                mPmt.mDic_mPmtValue[NewName][clsPmtBottomCCD.enuPmtName.SavePath] = "D:\\BottomCCD\\Result.png";
                UpdateControls();
            }


        }
        private void btnDelete_BottomCCDSetting_Click(object sender, EventArgs e)
        {
            if (dgvSetBottomCCD.SelectedCells.Count > 0)
            {
                int iSelectIndex = dgvSetBottomCCD.SelectedCells[0].RowIndex;
                string sSelectName = dgvSetBottomCCD[dgvSensorName.Index, iSelectIndex].Value.ToString();
                if (mPmt.mDic_mPmtValue.ContainsKey(sSelectName) == true)
                {
                    if (formMessageBox.Show(clsLanguage.GetTranslation("Are you sure want to delete :\r\n") + sSelectName, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        mPmt.mDic_mPmtValue.Remove(sSelectName);
                        UpdateControls();
                    }
                }
            }
        }
        private void dgvSetBottomCCD_EnabledChanged(object sender, EventArgs e)
        {
            btnEdit_BottomCCDSetting.Enabled = true;
            btnSave_BottomCCDSetting.Enabled = dgvSetBottomCCD.Enabled;
            btnCancel_BottomCCDSetting.Enabled = dgvSetBottomCCD.Enabled;
            btnAdd_BottomCCDSetting.Enabled = dgvSetBottomCCD.Enabled;
            btnDelete_BottomCCDSetting.Enabled = dgvSetBottomCCD.Enabled;
        }

        #endregion

    }
}

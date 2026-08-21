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
    public partial class ucTCPLinkSetting : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public clsPmtTCPLink mPmt = new clsPmtTCPLink();
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
        static private ucTCPLinkSetting m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucTCPLinkSetting GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucTCPLinkSetting();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucTCPLinkSetting()
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
                if (this.Parent != null)
                {
                    this.Size = this.Parent.ClientSize;
                }
                txt_TCPLinkPath.Text = mPmt.sINIPath;
                dgvSetTCPLink.Enabled = bIsEditing;
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
                SetBtnColorFlash(btnSave_TCPLinkSetting, bIsEditing);
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
            mForm.Text = clsLanguage.GetTranslation("Set TCPLink Parameter");
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
                if (dgvSetTCPLink.Rows.Count > iDeviceNum)
                {
                    int RemoveRowCount = dgvSetTCPLink.Rows.Count - iDeviceNum;
                    for (int i = 0; i < RemoveRowCount; i++)
                    {
                        dgvSetTCPLink.Rows.RemoveAt(dgvSetTCPLink.Rows.Count - 1);
                    }
                }
                else if (dgvSetTCPLink.Rows.Count < iDeviceNum)
                {
                    int AddRowCount = iDeviceNum - dgvSetTCPLink.Rows.Count;
                    for (int i = 0; i < AddRowCount; i++)
                    {
                        dgvSetTCPLink.Rows.Add();
                    }
                }
                #endregion
                for (int iRow = 0; iRow < iDeviceNum; iRow++)
                {
                    if (iRow < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtTCPLink.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iRow).Value;
                        foreach (clsPmtTCPLink.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtTCPLink.enuPmtName)))
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
                        dgvSetTCPLink[dgvNo.Index, iRow].Value = (iRow + 1).ToString();
                        dgvSetTCPLink[dgvReaderName.Index, iRow].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;
                        dgvSetTCPLink[dgvServer.Index, iRow].Value = pDicPmt[clsPmtTCPLink.enuPmtName.bIsServer] == "1";
                        dgvSetTCPLink[dgvTCPIP.Index, iRow].Value = pDicPmt[clsPmtTCPLink.enuPmtName.TCP_IP];
                        dgvSetTCPLink[dgvTCPPort.Index, iRow].Value = pDicPmt[clsPmtTCPLink.enuPmtName.TCP_Port];

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
                dgvSetTCPLink.EndEdit();
                dgvSetTCPLink.Refresh();
                for (int iRow = 0; iRow < iDeviceNum; iRow++)
                {
                    if (iRow < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtTCPLink.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iRow).Value;
                        foreach (clsPmtTCPLink.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtTCPLink.enuPmtName)))
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
                        dgvSetTCPLink[dgvNo.Index, iRow].Value = (iRow + 1).ToString();
                        dgvSetTCPLink[dgvReaderName.Index, iRow].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;
                        pDicPmt[clsPmtTCPLink.enuPmtName.bIsServer] = Convert.ToBoolean(dgvSetTCPLink[dgvServer.Index, iRow].Value) ? "1" : "0";
                        pDicPmt[clsPmtTCPLink.enuPmtName.TCP_IP] = ToString(dgvSetTCPLink[dgvTCPIP.Index, iRow].Value);
                        pDicPmt[clsPmtTCPLink.enuPmtName.TCP_Port] = ToString(dgvSetTCPLink[dgvTCPPort.Index, iRow].Value);
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
                if (mDgv.CurrentCell.ColumnIndex != dgvTCPIP.Index)
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
        private void ucTCPLinkSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                bIsEditing = false;
                UpdateControls();
            }
            this.SetReflashTimerStart(this.Visible);
        }
        #endregion

        #region//===================== 以下為事件處理 (Edit, Cancel, Save, Add, Delete) =====================

        private void btnEdit_TCPLinkSetting_Click(object sender, EventArgs e)
        {
            bIsEditing = !bIsEditing;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnSave_TCPLinkSetting_Click(object sender, EventArgs e)
        {
            if (formMessageBox.Show("Save setting need to restart program.", "Save TCPLink Setting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConvertUIToData();
                bIsEditing = false;
                mPmt.Save(mPmt.sINIPath);
                UpdateControls();
                clsMultiSystem.SetSettingChangeFlag();
            }
        }
        private void btnCancel_TCPLinkSetting_Click(object sender, EventArgs e)
        {

            bIsEditing = false;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnAdd_TCPLinkSetting_Click(object sender, EventArgs e)
        {
            string NewName  = "";
            if (clsDialogShow.InputString("Add TCPLink", "New Sensor Name :", ref NewName) == DialogResult.OK)
            {
                if (mPmt.mDic_mPmtValue.ContainsKey(NewName) == true)
                {
                    formMessageBox.Show("Name Already Exist : \r\n" + NewName);
                    return;
                }
                mPmt.mDic_mPmtValue.Add(NewName, new Dictionary<clsPmtTCPLink.enuPmtName, string>());
                foreach (clsPmtTCPLink.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtReader.enuPmtName)))
                {
                    if (mPmt.mDic_mPmtValue[NewName].ContainsKey(ePmt) == false)
                    {
                        mPmt.mDic_mPmtValue[NewName].Add(ePmt, "0");
                    }
                }
                mPmt.mDic_mPmtValue[NewName][clsPmtTCPLink.enuPmtName.Name] = NewName;
                mPmt.mDic_mPmtValue[NewName][clsPmtTCPLink.enuPmtName.TCP_IP] = "127.0.0.1";
                mPmt.mDic_mPmtValue[NewName][clsPmtTCPLink.enuPmtName.TCP_Port] = "9004";
                mPmt.mDic_mPmtValue[NewName][clsPmtTCPLink.enuPmtName.bIsServer] = "0";
                UpdateControls();
            }


        }
        private void btnDelete_TCPLinkSetting_Click(object sender, EventArgs e)
        {
            if (dgvSetTCPLink.SelectedCells.Count > 0)
            {
                int iSelectIndex = dgvSetTCPLink.SelectedCells[0].RowIndex;
                string sSelectName = dgvSetTCPLink[dgvReaderName.Index, iSelectIndex].Value.ToString();
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
        private void dgvSetTCPLink_EnabledChanged(object sender, EventArgs e)
        {
            btnEdit_TCPLinkSetting.Enabled = true;
            btnSave_TCPLinkSetting.Enabled = dgvSetTCPLink.Enabled;
            btnCancel_TCPLinkSetting.Enabled = dgvSetTCPLink.Enabled;
            btnAdd_TCPLinkSetting.Enabled = dgvSetTCPLink.Enabled;
            btnDelete_TCPLinkSetting.Enabled = dgvSetTCPLink.Enabled;
        }

        #endregion

    }
}

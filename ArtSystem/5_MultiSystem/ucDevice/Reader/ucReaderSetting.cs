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
    public partial class ucReaderSetting : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public clsPmtReader mPmt = new clsPmtReader();
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
        static private ucReaderSetting m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucReaderSetting GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucReaderSetting();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucReaderSetting()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }

            dgvReaderType.Items.Clear();
            foreach (clsCtrlReader.enuReaderType eEnum in Enum.GetValues(typeof(clsCtrlReader.enuReaderType)))
            {
                dgvReaderType.Items.Add(eEnum.ToString());
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
                txt_ReaderPath.Text = mPmt.sINIPath;
                dgvSetReader.Enabled = bIsEditing;
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
                SetBtnColorFlash(btnSave_ReaderSetting, bIsEditing);
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
            mForm.Text = clsLanguage.GetTranslation("Set Reader Parameter");
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
                if (dgvSetReader.Rows.Count > iDeviceNum)
                {
                    int RemoveRowCount = dgvSetReader.Rows.Count - iDeviceNum;
                    for (int i = 0; i < RemoveRowCount; i++)
                    {
                        dgvSetReader.Rows.RemoveAt(dgvSetReader.Rows.Count - 1);
                    }
                }
                else if (dgvSetReader.Rows.Count < iDeviceNum)
                {
                    int AddRowCount = iDeviceNum - dgvSetReader.Rows.Count;
                    for (int i = 0; i < AddRowCount; i++)
                    {
                        dgvSetReader.Rows.Add();
                    }
                }
                #endregion
                for (int iRow = 0; iRow < iDeviceNum; iRow++)
                {
                    if (iRow < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtReader.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iRow).Value;
                        foreach (clsPmtReader.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtReader.enuPmtName)))
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
                        if (pDicPmt[clsPmtReader.enuPmtName.ReaderType] == clsCtrlReader.enuReaderType.RFID_IFM_RS232.ToString())
                        {
                            List<string> lstBoudRate = new List<string>()
                            {
                                "300",
                                "600",
                                "1200",
                                "2400",
                                "4800",
                                "9600",
                                "14400",
                                "19200",
                                "28800",
                                "38400",
                                "56000",
                                "57600",
                                "115200",
                                "128000",
                                "230400",
                                "256000",
                                "460800",
                                "921600",
                            };
                            if (pDicPmt[clsPmtReader.enuPmtName.TCP_IP].StartsWith("COM") == false)
                            {
                                pDicPmt[clsPmtReader.enuPmtName.TCP_IP] = "COM1";
                            }
                            if (lstBoudRate.Contains(pDicPmt[clsPmtReader.enuPmtName.TCP_Port]) == false)
                            {
                                pDicPmt[clsPmtReader.enuPmtName.TCP_Port] = "9600";
                            }
                        }
                        dgvSetReader[dgvNo.Index, iRow].Value = (iRow + 1).ToString();
                        dgvSetReader[dgvReaderName.Index, iRow].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;
                        dgvSetReader[dgvReaderType.Index, iRow].Value = pDicPmt[clsPmtReader.enuPmtName.ReaderType];
                        dgvSetReader[dgvTCPIP.Index, iRow].Value = pDicPmt[clsPmtReader.enuPmtName.TCP_IP];
                        dgvSetReader[dgvTCPPort.Index, iRow].Value = pDicPmt[clsPmtReader.enuPmtName.TCP_Port];
                        dgvSetReader[dgvChannelID.Index, iRow].Value = pDicPmt[clsPmtReader.enuPmtName.ChannelID];

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
                dgvSetReader.EndEdit();
                dgvSetReader.Refresh();
                for (int iRow = 0; iRow < iDeviceNum; iRow++)
                {
                    if (iRow < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtReader.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iRow).Value;
                        foreach (clsPmtReader.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtRollerMotor.enuPmtName)))
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
                        dgvSetReader[dgvNo.Index, iRow].Value = (iRow + 1).ToString();
                        dgvSetReader[dgvReaderName.Index, iRow].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;
                        pDicPmt[clsPmtReader.enuPmtName.ReaderType] = ToString(dgvSetReader[dgvReaderType.Index, iRow].Value);
                        pDicPmt[clsPmtReader.enuPmtName.TCP_IP] = ToString(dgvSetReader[dgvTCPIP.Index, iRow].Value);
                        pDicPmt[clsPmtReader.enuPmtName.TCP_Port] = ToString(dgvSetReader[dgvTCPPort.Index, iRow].Value);
                        pDicPmt[clsPmtReader.enuPmtName.ChannelID] = ToString(dgvSetReader[dgvChannelID.Index, iRow].Value);
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

        private void ucReaderSetting_VisibleChanged(object sender, EventArgs e)
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

        private void btnEdit_ReaderSetting_Click(object sender, EventArgs e)
        {
            bIsEditing = !bIsEditing;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnSave_ReaderSetting_Click(object sender, EventArgs e)
        {
            if (formMessageBox.Show("Save setting need to restart program.", "Save Reader Setting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConvertUIToData();
                bIsEditing = false;
                mPmt.Save(mPmt.sINIPath);
                UpdateControls();
                clsMultiSystem.SetSettingChangeFlag();
            }
        }
        private void btnCancel_ReaderSetting_Click(object sender, EventArgs e)
        {

            bIsEditing = false;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnAdd_ReaderSetting_Click(object sender, EventArgs e)
        {
            string NewName  = "";
            if (clsDialogShow.InputString("Add Reader", "New Sensor Name :", ref NewName) == DialogResult.OK)
            {
                if (mPmt.mDic_mPmtValue.ContainsKey(NewName) == true)
                {
                    formMessageBox.Show("Name Already Exist : \r\n" + NewName);
                    return;
                }
                mPmt.mDic_mPmtValue.Add(NewName, new Dictionary<clsPmtReader.enuPmtName, string>());
                foreach (clsPmtReader.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtReader.enuPmtName)))
                {
                    if (mPmt.mDic_mPmtValue[NewName].ContainsKey(ePmt) == false)
                    {
                        mPmt.mDic_mPmtValue[NewName].Add(ePmt, "0");
                    }
                }
                mPmt.mDic_mPmtValue[NewName][clsPmtReader.enuPmtName.ReaderName] = NewName;
                mPmt.mDic_mPmtValue[NewName][clsPmtReader.enuPmtName.ReaderType] = clsCtrlReader.enuReaderType.Keyence2D.ToString();
                mPmt.mDic_mPmtValue[NewName][clsPmtReader.enuPmtName.TCP_IP] = "192.168.100.100";
                mPmt.mDic_mPmtValue[NewName][clsPmtReader.enuPmtName.TCP_Port] = "9004";
                mPmt.mDic_mPmtValue[NewName][clsPmtReader.enuPmtName.ChannelID] = "0";
                UpdateControls();
            }


        }
        private void btnDelete_ReaderSetting_Click(object sender, EventArgs e)
        {
            if (dgvSetReader.SelectedCells.Count > 0)
            {
                int iSelectIndex = dgvSetReader.SelectedCells[0].RowIndex;
                string sSelectName = dgvSetReader[dgvReaderName.Index, iSelectIndex].Value.ToString();
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
        private void dgvSetReader_EnabledChanged(object sender, EventArgs e)
        {
            btnEdit_ReaderSetting.Enabled = true;
            btnSave_ReaderSetting.Enabled = dgvSetReader.Enabled;
            btnCancel_ReaderSetting.Enabled = dgvSetReader.Enabled;
            btnAdd_ReaderSetting.Enabled = dgvSetReader.Enabled;
            btnDelete_ReaderSetting.Enabled = dgvSetReader.Enabled;
        }

        #endregion


    }
}

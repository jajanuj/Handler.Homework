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
    public partial class ucWeightScaleSetting : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public clsPmtWeightScale mPmt = new clsPmtWeightScale();
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
        private bool bAdvanceSetting = false;
        #endregion

        #region //=====================  必要函式設置 =====================

        static object m_LockObj = new object();
        static private ucWeightScaleSetting m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucWeightScaleSetting GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucWeightScaleSetting();
                }

            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucWeightScaleSetting()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }

            dgvControllerType.Items.Clear();
            foreach (clsCtrlWeightScale.enuModuleType eEnum in Enum.GetValues(typeof(clsCtrlWeightScale.enuModuleType)))
            {
                dgvControllerType.Items.Add(eEnum.ToString());
            }

            dgvCOMParity.Items.Clear();
            foreach (System.IO.Ports.Parity eEnum in Enum.GetValues(typeof(System.IO.Ports.Parity)))
            {
                dgvCOMParity.Items.Add(eEnum.ToString());
            }

            dgvCOMHandshake.Items.Clear();
            foreach (System.IO.Ports.Handshake eEnum in Enum.GetValues(typeof(System.IO.Ports.Handshake)))
            {
                dgvCOMHandshake.Items.Add(eEnum.ToString());
            }

            dgvCOMStopBits.Items.Clear();
            foreach (System.IO.Ports.StopBits eEnum in Enum.GetValues(typeof(System.IO.Ports.StopBits)))
            {
                dgvCOMStopBits.Items.Add(eEnum.ToString());
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
                txt_WeightScalePath.Text = mPmt.sINIPath;
                dgvSetWeightScale.Enabled = bIsEditing;
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
                SetBtnColorFlash(btnSave_WeightScaleSetting, bIsEditing);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
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
                dgvSetWeightScale.Columns[dgvCOMTimeout.Index].Visible = bAdvanceSetting;
                dgvSetWeightScale.Columns[dgvCOMStopBits.Index].Visible = bAdvanceSetting;
                dgvSetWeightScale.Columns[dgvCOMHandshake.Index].Visible = bAdvanceSetting;
                dgvSetWeightScale.Columns[dgvCOMParity.Index].Visible = bAdvanceSetting;

                #region//將Row行數調整成與mCardInfo.mLstMotionCardInfo.Count相等
                if (dgvSetWeightScale.Rows.Count > iDeviceNum)
                {
                    int RemoveRowCount = dgvSetWeightScale.Rows.Count - iDeviceNum;
                    for (int i = 0; i < RemoveRowCount; i++)
                    {
                        dgvSetWeightScale.Rows.RemoveAt(dgvSetWeightScale.Rows.Count - 1);
                    }
                }
                else if (dgvSetWeightScale.Rows.Count < iDeviceNum)
                {
                    int AddRowCount = iDeviceNum - dgvSetWeightScale.Rows.Count;
                    for (int i = 0; i < AddRowCount; i++)
                    {
                        dgvSetWeightScale.Rows.Add();
                    }
                }
                #endregion
                for (int iRow = 0; iRow < iDeviceNum; iRow++)
                {
                    if (iRow < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtWeightScale.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iRow).Value;
                        foreach (clsPmtWeightScale.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtWeightScale.enuPmtName)))
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
                        dgvSetWeightScale[dgvNo.Index, iRow].Value = (iRow + 1).ToString();
                        dgvSetWeightScale[dgvControllerName.Index, iRow].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;
                        dgvSetWeightScale[dgvControllerType.Index, iRow].Value = pDicPmt[clsPmtWeightScale.enuPmtName.eControllerType];
                        dgvSetWeightScale[dgvComPort.Index, iRow].Value = pDicPmt[clsPmtWeightScale.enuPmtName.Serial_COMID];
                        dgvSetWeightScale[dgvCOMBaudRate.Index, iRow].Value = pDicPmt[clsPmtWeightScale.enuPmtName.Serial_BaudRate];
                        dgvSetWeightScale[dgvCOMBits.Index, iRow].Value = pDicPmt[clsPmtWeightScale.enuPmtName.Serial_DataBits];
                        //dgvSetWeightScale[dgvCOMStationID.Index, iRow].Value = pDicPmt[clsPmtWeightScale.enuPmtName.Serial_StationID];
                        dgvSetWeightScale[dgvCOMTimeout.Index, iRow].Value = pDicPmt[clsPmtWeightScale.enuPmtName.Serial_TimeOut];

                        dgvSetWeightScale[dgvCOMHandshake.Index, iRow].Value = pDicPmt[clsPmtWeightScale.enuPmtName.Serial_Handshake];
                        dgvSetWeightScale[dgvCOMStopBits.Index, iRow].Value = pDicPmt[clsPmtWeightScale.enuPmtName.Serial_StopBits];
                        dgvSetWeightScale[dgvCOMParity.Index, iRow].Value = pDicPmt[clsPmtWeightScale.enuPmtName.Serial_Parity];
                      
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
                dgvSetWeightScale.EndEdit();
                dgvSetWeightScale.Refresh();
                for (int iRow = 0; iRow < iDeviceNum; iRow++)
                {
                    if (iRow < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtWeightScale.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iRow).Value;
                        foreach (clsPmtWeightScale.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtWeightScale.enuPmtName)))
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
                        dgvSetWeightScale[dgvNo.Index, iRow].Value = (iRow + 1).ToString();
                        dgvSetWeightScale[dgvControllerName.Index, iRow].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;


                        pDicPmt[clsPmtWeightScale.enuPmtName.eControllerType] = ToString(dgvSetWeightScale[dgvControllerType.Index, iRow].Value);
                        pDicPmt[clsPmtWeightScale.enuPmtName.Serial_COMID] = ToString(dgvSetWeightScale[dgvComPort.Index, iRow].Value);
                        pDicPmt[clsPmtWeightScale.enuPmtName.Serial_BaudRate] = ToString(dgvSetWeightScale[dgvCOMBaudRate.Index, iRow].Value);
                        pDicPmt[clsPmtWeightScale.enuPmtName.Serial_DataBits] = ToString(dgvSetWeightScale[dgvCOMBits.Index, iRow].Value);
                        //pDicPmt[clsPmtWeightScale.enuPmtName.Serial_StationID] = ToString(dgvSetWeightScale[dgvCOMStationID.Index, iRow].Value);
                        pDicPmt[clsPmtWeightScale.enuPmtName.Serial_TimeOut] = ToString(dgvSetWeightScale[dgvCOMTimeout.Index, iRow].Value);
                        pDicPmt[clsPmtWeightScale.enuPmtName.Serial_Handshake] = ToString(dgvSetWeightScale[dgvCOMHandshake.Index, iRow].Value);
                        pDicPmt[clsPmtWeightScale.enuPmtName.Serial_StopBits] = ToString(dgvSetWeightScale[dgvCOMStopBits.Index, iRow].Value);
                        pDicPmt[clsPmtWeightScale.enuPmtName.Serial_Parity] = ToString(dgvSetWeightScale[dgvCOMParity.Index, iRow].Value);
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

        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView mDgv = (DataGridView)sender;
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            e.Control.TextChanged -= new EventHandler(Column1_TextChanged);
            if (mDgv.CurrentCell is DataGridViewTextBoxCell) //Desired Column
            {
                if (true
                    //&& mDgv.CurrentCell.ColumnIndex != dgvOverLimit_DI.Index
                    //&& mDgv.CurrentCell.ColumnIndex != dgvEnable_Do.Index
                    )
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                        tb.TextChanged += new EventHandler(Column1_TextChanged);
                    }
                }
            }
            else if (mDgv.CurrentCell is DataGridViewComboBoxCell) //Desired Column
            {
                ComboBox tb = e.Control as ComboBox;
                if (tb != null)
                {
                    tb.TextChanged -= new EventHandler(Column1_TextChanged);
                    tb.TextChanged += new EventHandler(Column1_TextChanged);
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
        private void Column1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvSetWeightScale.CurrentCell != null)
                {
                    int iCurrentColumn = dgvSetWeightScale.CurrentCell.ColumnIndex;
                    int iCurrentRow = dgvSetWeightScale.CurrentCell.RowIndex;
                    if (iCurrentColumn == dgvCOMBaudRate.Index
                        || iCurrentColumn == dgvCOMBits.Index
                        || iCurrentColumn == dgvCOMTimeout.Index
                        || iCurrentColumn == dgvCOMHandshake.Index
                        || iCurrentColumn == dgvCOMStopBits.Index
                        || iCurrentColumn == dgvCOMParity.Index)
                    {
                        for (int i = 0; i < dgvSetWeightScale.Rows.Count; i++)
                        {
                            if (i == iCurrentRow)
                            {
                                continue;
                            }
                            if (dgvSetWeightScale[dgvComPort.Index, i].Value.ToString() == dgvSetWeightScale[dgvComPort.Index, iCurrentRow].Value.ToString())
                            {
                                if (sender is TextBox)
                                {
                                    TextBox Item = (TextBox)sender;
                                    dgvSetWeightScale[iCurrentColumn, i].Value = Item.Text;
                                }
                                else if (sender is ComboBox)
                                {
                                    ComboBox Item = (ComboBox)sender;
                                    if (Item.SelectedItem != null)
                                    {
                                        dgvSetWeightScale[iCurrentColumn, i].Value = Item.SelectedItem.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void ucWeightScaleSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                bIsEditing = false;
                UpdateControls();
            }
            this.SetReflashTimerStart(this.Visible);
        }

        private void dgvSetWeightScale_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView mDgv = (DataGridView)sender;
            if (mDgv.CurrentCell is DataGridViewTextBoxCell) //Desired Column
            {
                if (mDgv.CurrentCell.RowIndex >= 0
                    && mDgv.CurrentCell.RowIndex < mPmt.mDic_mPmtValue.Count)
                {
                    Dictionary<clsPmtWeightScale.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(mDgv.CurrentCell.RowIndex).Value;
                }
            }
            dgvSetWeightScale.EndEdit();
            ConvertUIToData();

        }
        #endregion

        #region//===================== 以下為事件處理 (Edit, Cancel, Save, Add, Delete) =====================

        private void btnEdit_WeightScaleSetting_Click(object sender, EventArgs e)
        {
            bIsEditing = !bIsEditing;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnSave_WeightScaleSetting_Click(object sender, EventArgs e)
        {
            if (formMessageBox.Show("Save setting need to restart program.", "Save Heater Module Setting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConvertUIToData();
                bIsEditing = false;
                mPmt.Save(mPmt.sINIPath);
                UpdateControls();
                clsMultiSystem.SetSettingChangeFlag();
            }
        }
        private void btnCancel_WeightScaleSetting_Click(object sender, EventArgs e)
        {

            bIsEditing = false;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnAdd_WeightScaleSetting_Click(object sender, EventArgs e)
        {
            string NewName  = "";
            if (clsDialogShow.InputString("Add Heater Module", "New Module Name :", ref NewName) == DialogResult.OK)
            {
                if (mPmt.mDic_mPmtValue.ContainsKey(NewName) == true)
                {
                    formMessageBox.Show("Name Already Exist : \r\n" + NewName);
                    return;
                }
                mPmt.mDic_mPmtValue.Add(NewName, new Dictionary<clsPmtWeightScale.enuPmtName, string>());
                foreach (clsPmtWeightScale.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtWeightScale.enuPmtName)))
                {
                    if (mPmt.mDic_mPmtValue[NewName].ContainsKey(ePmt) == false)
                    {
                        mPmt.mDic_mPmtValue[NewName].Add(ePmt, "0");
                    }
                }
                mPmt.mDic_mPmtValue[NewName][clsPmtWeightScale.enuPmtName.sControllerName] = NewName;
                mPmt.mDic_mPmtValue[NewName][clsPmtWeightScale.enuPmtName.eControllerType] = clsCtrlWeightScale.enuModuleType.MettlerToledoWX.ToString();
                mPmt.mDic_mPmtValue[NewName][clsPmtWeightScale.enuPmtName.Serial_COMID] = mPmt.mDic_mPmtValue.Count.ToString();
                mPmt.mDic_mPmtValue[NewName][clsPmtWeightScale.enuPmtName.Serial_BaudRate] = "9600";
                mPmt.mDic_mPmtValue[NewName][clsPmtWeightScale.enuPmtName.Serial_DataBits] = "8";
                //mPmt.mDic_mPmtValue[NewName][clsPmtWeightScale.enuPmtName.Serial_StationID] = "1";
                mPmt.mDic_mPmtValue[NewName][clsPmtWeightScale.enuPmtName.Serial_TimeOut] = "1000";
                mPmt.mDic_mPmtValue[NewName][clsPmtWeightScale.enuPmtName.Serial_Handshake] = System.IO.Ports.Handshake.None.ToString();
                mPmt.mDic_mPmtValue[NewName][clsPmtWeightScale.enuPmtName.Serial_StopBits] = System.IO.Ports.StopBits.One.ToString();
                mPmt.mDic_mPmtValue[NewName][clsPmtWeightScale.enuPmtName.Serial_Parity] = System.IO.Ports.Parity.None.ToString();
                UpdateControls();
            }


        }
        private void btnDelete_WeightScaleSetting_Click(object sender, EventArgs e)
        {
            if (dgvSetWeightScale.SelectedCells.Count > 0)
            {
                int iSelectIndex = dgvSetWeightScale.SelectedCells[0].RowIndex;
                string sSelectName = dgvSetWeightScale[dgvControllerName.Index, iSelectIndex].Value.ToString();
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
        private void dgvSetWeightScale_EnabledChanged(object sender, EventArgs e)
        {
            btnEdit_WeightScaleSetting.Enabled = true;
            btnSave_WeightScaleSetting.Enabled = dgvSetWeightScale.Enabled;
            btnCancel_WeightScaleSetting.Enabled = dgvSetWeightScale.Enabled;
            btnAdd_WeightScaleSetting.Enabled = dgvSetWeightScale.Enabled;
            btnDelete_WeightScaleSetting.Enabled = dgvSetWeightScale.Enabled;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            bAdvanceSetting = !bAdvanceSetting;
            UpdateControls();
        }

    }
}

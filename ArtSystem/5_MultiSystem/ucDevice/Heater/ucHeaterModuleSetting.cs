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
    public partial class ucHeaterModuleSetting : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public clsPmtHeaterModule mPmt = new clsPmtHeaterModule();
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
        static private ucHeaterModuleSetting m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucHeaterModuleSetting GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucHeaterModuleSetting();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucHeaterModuleSetting()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }

            dgvControllerType.Items.Clear();
            foreach (clsCtrlHeaterModule.enuControllerType eEnum in Enum.GetValues(typeof(clsCtrlHeaterModule.enuControllerType)))
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
                txt_HeaterModulePath.Text = mPmt.sINIPath;
                dgvSetHeaterModule.Enabled = bIsEditing;
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
                SetBtnColorFlash(btnSave_HeaterModuleSetting, bIsEditing);
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
                dgvSetHeaterModule.Columns[dgvCOMTimeout.Index].Visible = bAdvanceSetting;
                dgvSetHeaterModule.Columns[dgvCOMStopBits.Index].Visible = bAdvanceSetting;
                dgvSetHeaterModule.Columns[dgvCOMHandshake.Index].Visible = bAdvanceSetting;
                dgvSetHeaterModule.Columns[dgvCOMParity.Index].Visible = bAdvanceSetting;

                #region//將Row行數調整成與mCardInfo.mLstMotionCardInfo.Count相等
                if (dgvSetHeaterModule.Rows.Count > iDeviceNum)
                {
                    int RemoveRowCount = dgvSetHeaterModule.Rows.Count - iDeviceNum;
                    for (int i = 0; i < RemoveRowCount; i++)
                    {
                        dgvSetHeaterModule.Rows.RemoveAt(dgvSetHeaterModule.Rows.Count - 1);
                    }
                }
                else if (dgvSetHeaterModule.Rows.Count < iDeviceNum)
                {
                    int AddRowCount = iDeviceNum - dgvSetHeaterModule.Rows.Count;
                    for (int i = 0; i < AddRowCount; i++)
                    {
                        dgvSetHeaterModule.Rows.Add();
                    }
                }
                #endregion
                for (int iRow = 0; iRow < iDeviceNum; iRow++)
                {
                    if (iRow < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtHeaterModule.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iRow).Value;
                        foreach (clsPmtHeaterModule.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtHeaterModule.enuPmtName)))
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
                        dgvSetHeaterModule[dgvNo.Index, iRow].Value = (iRow + 1).ToString();
                        dgvSetHeaterModule[dgvControllerName.Index, iRow].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;
                        dgvSetHeaterModule[dgvControllerType.Index, iRow].Value = pDicPmt[clsPmtHeaterModule.enuPmtName.eControllerType];
                        dgvSetHeaterModule[dgvComPort.Index, iRow].Value = pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_COMID];
                        dgvSetHeaterModule[dgvCOMBaudRate.Index, iRow].Value = pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_BaudRate];
                        dgvSetHeaterModule[dgvCOMBits.Index, iRow].Value = pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_DataBits];
                        dgvSetHeaterModule[dgvCOMStationID.Index, iRow].Value = pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_StationID];
                        dgvSetHeaterModule[dgvCOMTimeout.Index, iRow].Value = pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_TimeOut];

                        dgvSetHeaterModule[dgvCOMHandshake.Index, iRow].Value = pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_Handshake];
                        dgvSetHeaterModule[dgvCOMStopBits.Index, iRow].Value = pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_StopBits];
                        dgvSetHeaterModule[dgvCOMParity.Index, iRow].Value = pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_Parity];

                        if (Enum.IsDefined(typeof(clsEnum.enuDo), pDicPmt[clsPmtHeaterModule.enuPmtName.eDo_Enable]) == true)
                        {
                            dgvSetHeaterModule[dgvEnable_Do.Index, iRow].Value = pDicPmt[clsPmtHeaterModule.enuPmtName.eDo_Enable];
                        }
                        else
                        {
                            dgvSetHeaterModule[dgvEnable_Do.Index, iRow].Value = "";
                        }

                        if (Enum.IsDefined(typeof(clsEnum.enuDi), pDicPmt[clsPmtHeaterModule.enuPmtName.eDi_OverHeat_BType]) == true)
                        {
                            dgvSetHeaterModule[dgvOverLimit_DI.Index, iRow].Value = pDicPmt[clsPmtHeaterModule.enuPmtName.eDi_OverHeat_BType];
                        }
                        else
                        {
                            dgvSetHeaterModule[dgvOverLimit_DI.Index, iRow].Value = "";
                        }
                        if (Enum.IsDefined(typeof(clsEnum.enuPmtName), pDicPmt[clsPmtHeaterModule.enuPmtName.enuSys_Temp_ShiftOffset]) == true)
                        {
                            dgvSetHeaterModule[dgvEnumShiftOffsetPmt.Index, iRow].Value = pDicPmt[clsPmtHeaterModule.enuPmtName.enuSys_Temp_ShiftOffset];
                        }
                        else
                        {
                            dgvSetHeaterModule[dgvEnumShiftOffsetPmt.Index, iRow].Value = "";
                        }
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
                dgvSetHeaterModule.EndEdit();
                dgvSetHeaterModule.Refresh();
                for (int iRow = 0; iRow < iDeviceNum; iRow++)
                {
                    if (iRow < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtHeaterModule.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iRow).Value;
                        foreach (clsPmtHeaterModule.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtHeaterModule.enuPmtName)))
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
                        dgvSetHeaterModule[dgvNo.Index, iRow].Value = (iRow + 1).ToString();
                        dgvSetHeaterModule[dgvControllerName.Index, iRow].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;


                        pDicPmt[clsPmtHeaterModule.enuPmtName.eControllerType] = ToString(dgvSetHeaterModule[dgvControllerType.Index, iRow].Value);
                        pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_COMID] = ToString(dgvSetHeaterModule[dgvComPort.Index, iRow].Value);
                        pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_BaudRate] = ToString(dgvSetHeaterModule[dgvCOMBaudRate.Index, iRow].Value);
                        pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_DataBits] = ToString(dgvSetHeaterModule[dgvCOMBits.Index, iRow].Value);
                        pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_StationID] = ToString(dgvSetHeaterModule[dgvCOMStationID.Index, iRow].Value);
                        pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_TimeOut] = ToString(dgvSetHeaterModule[dgvCOMTimeout.Index, iRow].Value);
                        pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_Handshake] = ToString(dgvSetHeaterModule[dgvCOMHandshake.Index, iRow].Value);
                        pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_StopBits] = ToString(dgvSetHeaterModule[dgvCOMStopBits.Index, iRow].Value);
                        pDicPmt[clsPmtHeaterModule.enuPmtName.Serial_Parity] = ToString(dgvSetHeaterModule[dgvCOMParity.Index, iRow].Value);
                        pDicPmt[clsPmtHeaterModule.enuPmtName.eDo_Enable] = ToString(dgvSetHeaterModule[dgvEnable_Do.Index, iRow].Value);
                        pDicPmt[clsPmtHeaterModule.enuPmtName.eDi_OverHeat_BType] = ToString(dgvSetHeaterModule[dgvOverLimit_DI.Index, iRow].Value);
                        pDicPmt[clsPmtHeaterModule.enuPmtName.enuSys_Temp_ShiftOffset] = ToString(dgvSetHeaterModule[dgvEnumShiftOffsetPmt.Index, iRow].Value);
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
                    && mDgv.CurrentCell.ColumnIndex != dgvOverLimit_DI.Index
                    && mDgv.CurrentCell.ColumnIndex != dgvEnable_Do.Index
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
                if (dgvSetHeaterModule.CurrentCell != null)
                {
                    int iCurrentColumn = dgvSetHeaterModule.CurrentCell.ColumnIndex;
                    int iCurrentRow = dgvSetHeaterModule.CurrentCell.RowIndex;
                    if (iCurrentColumn == dgvCOMBaudRate.Index
                        || iCurrentColumn == dgvCOMBits.Index
                        || iCurrentColumn == dgvCOMTimeout.Index
                        || iCurrentColumn == dgvCOMHandshake.Index
                        || iCurrentColumn == dgvCOMStopBits.Index
                        || iCurrentColumn == dgvCOMParity.Index)
                    {
                        for (int i = 0; i < dgvSetHeaterModule.Rows.Count; i++)
                        {
                            if (i == iCurrentRow)
                            {
                                continue;
                            }
                            if (dgvSetHeaterModule[dgvComPort.Index, i].Value.ToString() == dgvSetHeaterModule[dgvComPort.Index, iCurrentRow].Value.ToString())
                            {
                                if (sender is TextBox)
                                {
                                    TextBox Item = (TextBox)sender;
                                    dgvSetHeaterModule[iCurrentColumn, i].Value = Item.Text;
                                }
                                else if (sender is ComboBox)
                                {
                                    ComboBox Item = (ComboBox)sender;
                                    if (Item.SelectedItem != null)
                                    {
                                        dgvSetHeaterModule[iCurrentColumn, i].Value = Item.SelectedItem.ToString();
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

        private void ucHeaterModuleSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                bIsEditing = false;
                UpdateControls();
            }
            this.SetReflashTimerStart(this.Visible);
        }

        private void dgvSetHeaterModule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView mDgv = (DataGridView)sender;
            if (mDgv.CurrentCell is DataGridViewTextBoxCell) //Desired Column
            {
                if (mDgv.CurrentCell.RowIndex >= 0
                    && mDgv.CurrentCell.RowIndex < mPmt.mDic_mPmtValue.Count)
                {
                    Dictionary<clsPmtHeaterModule.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(mDgv.CurrentCell.RowIndex).Value;
                    if (mDgv.CurrentCell.ColumnIndex == dgvOverLimit_DI.Index)
                    {
                        #region//eDi_OverHeat_BType
                        clsEnum.enuDi? CurrentValue = null;
                        if (Enum.IsDefined(typeof(clsEnum.enuDi), pDicPmt[clsPmtHeaterModule.enuPmtName.eDi_OverHeat_BType]) == true)
                        {
                            CurrentValue = (clsEnum.enuDi)Enum.Parse(typeof(clsEnum.enuDi), pDicPmt[clsPmtHeaterModule.enuPmtName.eDi_OverHeat_BType]);
                        }
                        if (ucGetEnumDI.GetSingleton()._ShowFormDialog(ref CurrentValue) == DialogResult.OK)
                        {
                            if (CurrentValue == null)
                            {
                                mDgv.CurrentCell.Value = "";
                            }
                            else
                            {
                                mDgv.CurrentCell.Value = CurrentValue.ToString();
                            }
                        }
                        #endregion
                    }
                    else if (mDgv.CurrentCell.ColumnIndex == dgvEnable_Do.Index)
                    {
                        #region//eDo_Enable
                        clsEnum.enuDo? CurrentValue = null;
                        if (Enum.IsDefined(typeof(clsEnum.enuDo), pDicPmt[clsPmtHeaterModule.enuPmtName.eDo_Enable]) == true)
                        {
                            CurrentValue = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), pDicPmt[clsPmtHeaterModule.enuPmtName.eDo_Enable]);
                        }
                        if (ucGetEnumDO.GetSingleton()._ShowFormDialog(ref CurrentValue) == DialogResult.OK)
                        {
                            if (CurrentValue == null)
                            {
                                mDgv.CurrentCell.Value = "";
                            }
                            else
                            {
                                mDgv.CurrentCell.Value = CurrentValue.ToString();
                            }
                        }
                        #endregion
                    }
                    else if (mDgv.CurrentCell.ColumnIndex == dgvEnumShiftOffsetPmt.Index)
                    {
                        #region//eDo_Enable
                        clsEnum.enuPmtName? CurrentValue = null;
                        if (Enum.IsDefined(typeof(clsEnum.enuPmtName), pDicPmt[clsPmtHeaterModule.enuPmtName.enuSys_Temp_ShiftOffset]) == true)
                        {
                            CurrentValue = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), pDicPmt[clsPmtHeaterModule.enuPmtName.enuSys_Temp_ShiftOffset]);
                        }
                        if (ucGetEnumPmtName.GetSingleton()._ShowFormDialog(ref CurrentValue) == DialogResult.OK)
                        {
                            if (CurrentValue == null)
                            {
                                mDgv.CurrentCell.Value = "";
                            }
                            else
                            {
                                mDgv.CurrentCell.Value = CurrentValue.ToString();
                            }
                        }
                        #endregion
                    }
                }
            }
            dgvSetHeaterModule.EndEdit();
            ConvertUIToData();

        }
        #endregion

        #region//===================== 以下為事件處理 (Edit, Cancel, Save, Add, Delete) =====================

        private void btnEdit_HeaterModuleSetting_Click(object sender, EventArgs e)
        {
            bIsEditing = !bIsEditing;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnSave_HeaterModuleSetting_Click(object sender, EventArgs e)
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
        private void btnCancel_HeaterModuleSetting_Click(object sender, EventArgs e)
        {

            bIsEditing = false;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnAdd_HeaterModuleSetting_Click(object sender, EventArgs e)
        {
            string NewName  = "";
            if (clsDialogShow.InputString("Add Heater Module", "New Module Name :", ref NewName) == DialogResult.OK)
            {
                if (mPmt.mDic_mPmtValue.ContainsKey(NewName) == true)
                {
                    formMessageBox.Show("Name Already Exist : \r\n" + NewName);
                    return;
                }
                mPmt.mDic_mPmtValue.Add(NewName, new Dictionary<clsPmtHeaterModule.enuPmtName, string>());
                foreach (clsPmtHeaterModule.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtHeaterModule.enuPmtName)))
                {
                    if (mPmt.mDic_mPmtValue[NewName].ContainsKey(ePmt) == false)
                    {
                        mPmt.mDic_mPmtValue[NewName].Add(ePmt, "0");
                    }
                }
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.sControllerName] = NewName;
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.eControllerType] = clsCtrlHeaterModule.enuControllerType.DTK4848_V12.ToString();
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.Serial_COMID] = "1";
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.Serial_BaudRate] = "9600";
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.Serial_DataBits] = "8";
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.Serial_StationID] = mPmt.mDic_mPmtValue.Count.ToString();
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.Serial_TimeOut] = "1000";
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.Serial_Handshake] = System.IO.Ports.Handshake.None.ToString();
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.Serial_StopBits] = System.IO.Ports.StopBits.One.ToString();
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.Serial_Parity] = System.IO.Ports.Parity.None.ToString();
                //mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.Temp_Target] = "";
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.Temp_ErrorRange] = "5";
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.Temp_Limit] = "150";
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.enuSys_Temp_ShiftOffset] = "";
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.eDo_Enable] = "";
                mPmt.mDic_mPmtValue[NewName][clsPmtHeaterModule.enuPmtName.eDi_OverHeat_BType] = "";
                UpdateControls();
            }


        }
        private void btnDelete_HeaterModuleSetting_Click(object sender, EventArgs e)
        {
            if (dgvSetHeaterModule.SelectedCells.Count > 0)
            {
                int iSelectIndex = dgvSetHeaterModule.SelectedCells[0].RowIndex;
                string sSelectName = dgvSetHeaterModule[dgvControllerName.Index, iSelectIndex].Value.ToString();
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
        private void dgvSetHeaterModule_EnabledChanged(object sender, EventArgs e)
        {
            btnEdit_HeaterModuleSetting.Enabled = true;
            btnSave_HeaterModuleSetting.Enabled = dgvSetHeaterModule.Enabled;
            btnCancel_HeaterModuleSetting.Enabled = dgvSetHeaterModule.Enabled;
            btnAdd_HeaterModuleSetting.Enabled = dgvSetHeaterModule.Enabled;
            btnDelete_HeaterModuleSetting.Enabled = dgvSetHeaterModule.Enabled;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            bAdvanceSetting = !bAdvanceSetting;
            UpdateControls();
        }

    }
}

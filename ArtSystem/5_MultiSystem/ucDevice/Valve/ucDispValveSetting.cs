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
    public partial class ucDispValveSetting : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public clsPmtDispValve mPmt = new clsPmtDispValve();
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
        static private ucDispValveSetting m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucDispValveSetting GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucDispValveSetting();
                }

            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucDispValveSetting()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            dgvSetDispValve.Columns.Clear();
            dgvSetDispValve.Rows.Clear();
            dgvSetDispValve.Columns.Add("Pmt Name", "Pmt Name");
            dgvSetDispValve.Columns[0].DefaultCellStyle.BackColor = Color.LightGray;
            int iRow = 1;
            dgvSetDispValve.Rows.Add();
            dgvSetDispValve[0, 0].Value = "Pmt Name";
            foreach (clsPmtDispValve.enuPmtName ePmtName in Enum.GetValues(typeof(clsPmtDispValve.enuPmtName)))
            {
                dgvSetDispValve.Rows.Add();
                dgvSetDispValve[0, iRow].Value = ePmtName.ToString();
                iRow++;
            }
            dgvSetDispValve.Rows[0].DefaultCellStyle.BackColor = Color.LightGray;
            dgvSetDispValve.Rows[0].ReadOnly = true;
            dgvSetDispValve.Columns[0].ReadOnly = true;

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
                dgvSetDispValve.Enabled = bIsEditing;
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
                dgvSetDispValve.Columns[0].Width = 200;
                #region//調整Column數量
                {
                    int iColumn = 1;
                    for (int i = 0; i < this.mPmt.mDic_mPmtValue.Count; i++)
                    {
                        string sKey = this.mPmt.mDic_mPmtValue.ElementAt(i).Key;
                        if (dgvSetDispValve.Columns.Count <= iColumn)
                        {
                            dgvSetDispValve.Columns.Add(DateTime.Now.Ticks.ToString(), DateTime.Now.Ticks.ToString());
                            dgvSetDispValve.Columns[iColumn].SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        iColumn++;
                    }
                    for (int i = dgvSetDispValve.Columns.Count - 1; i > this.mPmt.mDic_mPmtValue.Count; i--)
                    {
                        dgvSetDispValve.Columns.RemoveAt(i);
                    }
                }
                #endregion
                {
                    int iRow = 1;
                    foreach (clsPmtDispValve.enuPmtName ePmtName in Enum.GetValues(typeof(clsPmtDispValve.enuPmtName)))
                    {
                        if (ePmtName == clsPmtDispValve.enuPmtName.Serial_BaudRate
                            || ePmtName == clsPmtDispValve.enuPmtName.Serial_DataBits
                            || ePmtName == clsPmtDispValve.enuPmtName.Serial_Handshake
                            || ePmtName == clsPmtDispValve.enuPmtName.Serial_StopBits
                            || ePmtName == clsPmtDispValve.enuPmtName.Serial_Parity
                            )
                        {
                            dgvSetDispValve.Rows[iRow].Visible = bAdvanceSetting;
                        }
                        else
                        {
                            dgvSetDispValve.Rows[iRow].Visible = true;
                        }
                        iRow++;
                    }
                }
                #region//將資料顯示在UI上
                {
                    int iColumn = 1;
                    for (int i = 0; i < this.mPmt.mDic_mPmtValue.Count; i++)
                    {
                        dgvSetDispValve.Columns[iColumn].Width = 300;
                        string sKey = this.mPmt.mDic_mPmtValue.ElementAt(i).Key;
                        dgvSetDispValve[iColumn, 0].Value = sKey;
                        int iRow = 1;
                        var DiName = clsDioMotion.GetDiName();
                        var DoName = clsDioMotion.GetDoName();
                        foreach (clsPmtDispValve.enuPmtName ePmtName in Enum.GetValues(typeof(clsPmtDispValve.enuPmtName)))
                        {
                            if (ePmtName == clsPmtDispValve.enuPmtName.sValveName
                                || ePmtName == clsPmtDispValve.enuPmtName.eValveType)
                            {
                                dgvSetDispValve[iColumn, iRow].ReadOnly = true;
                            }
                            if (ePmtName == clsPmtDispValve.enuPmtName.eDI_Done
                                || ePmtName == clsPmtDispValve.enuPmtName.eDI_Ready)
                            {
                                clsEnum.enuDi? eDI = null;
                                if (Enum.IsDefined(typeof(clsEnum.enuDi), this.mPmt.mDic_mPmtValue[sKey][ePmtName]) == true)
                                { eDI = (clsEnum.enuDi)Enum.Parse(typeof(clsEnum.enuDi), this.mPmt.mDic_mPmtValue[sKey][ePmtName]); }
                                dgvSetDispValve[iColumn, iRow].Value = clsDioMotion.GetString_Di(eDI, DiName);
                            }
                            else if (ePmtName == clsPmtDispValve.enuPmtName.eDO_Interrupt
                                || ePmtName == clsPmtDispValve.enuPmtName.eDO_ManualTrigger
                                || ePmtName == clsPmtDispValve.enuPmtName.eDO_TriggerEnable)
                            {
                                clsEnum.enuDo? eDO = null;
                                if (Enum.IsDefined(typeof(clsEnum.enuDo), this.mPmt.mDic_mPmtValue[sKey][ePmtName]) == true)
                                { eDO = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), this.mPmt.mDic_mPmtValue[sKey][ePmtName]); }
                                dgvSetDispValve[iColumn, iRow].Value = clsDioMotion.GetString_Do(eDO, DoName);
                            }
                            else
                            {
                                dgvSetDispValve[iColumn, iRow].Value = this.mPmt.mDic_mPmtValue[sKey][ePmtName];
                            }

                            iRow++;
                        }
                        iColumn++;
                    }
                    #endregion
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
                dgvSetDispValve.EndEdit();
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
            if (mDgv.CurrentCell is DataGridViewTextBoxCell) //Desired Column
            {
                if (false
                    || mDgv.CurrentCell.RowIndex == (((int)clsPmtDispValve.enuPmtName.Serial_COMID) + 1)
                    || mDgv.CurrentCell.RowIndex == (((int)clsPmtDispValve.enuPmtName.Serial_StationID) + 1)
                    || mDgv.CurrentCell.RowIndex == (((int)clsPmtDispValve.enuPmtName.Serial_TimeOut) + 1)
                    )
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
            try
            {
                DataGridView mDgv = (DataGridView)sender;
                if (mDgv.CurrentCell is DataGridViewTextBoxCell) //Desired Column
                {
                    int iColumn = mDgv.CurrentCell.ColumnIndex - 1;
                    int iRow = mDgv.CurrentCell.RowIndex - 1;
                    if (iColumn >= 0
                        && iColumn < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtDispValve.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iColumn).Value;
                        if (iRow == (int)clsPmtDispValve.enuPmtName.eValveType)
                        {
                            #region//eValveType
                            string sValveType = pDicPmt[clsPmtDispValve.enuPmtName.eValveType];
                            List<string> LstValveType = Enum.GetNames(typeof(clsCtrlDispValve.enuValveType)).ToList<string>();
                            if (clsDialogShow.SelectItem("Select Valve Type", LstValveType, ref sValveType) == DialogResult.OK)
                            {
                                pDicPmt[clsPmtDispValve.enuPmtName.eValveType] = sValveType;
                            }
                            #endregion
                        }
                        else if (iRow == (int)clsPmtDispValve.enuPmtName.eDI_Done
                            || iRow == (int)clsPmtDispValve.enuPmtName.eDI_Ready)
                        {
                            #region//eDi
                            clsEnum.enuDi? CurrentValue = null;
                            if (Enum.IsDefined(typeof(clsEnum.enuDi), pDicPmt[(clsPmtDispValve.enuPmtName)iRow]) == true)
                            {
                                CurrentValue = (clsEnum.enuDi)Enum.Parse(typeof(clsEnum.enuDi), pDicPmt[(clsPmtDispValve.enuPmtName)iRow]);
                            }
                            if (ucGetEnumDI.GetSingleton()._ShowFormDialog(ref CurrentValue) == DialogResult.OK)
                            {
                                if (CurrentValue == null)
                                {
                                    pDicPmt[(clsPmtDispValve.enuPmtName)iRow] = "";
                                }
                                else
                                {
                                    pDicPmt[(clsPmtDispValve.enuPmtName)iRow] = CurrentValue.ToString();
                                }
                            }
                            #endregion
                        }
                        else if (iRow == (int)clsPmtDispValve.enuPmtName.eDO_Interrupt
                            || iRow == (int)clsPmtDispValve.enuPmtName.eDO_ManualTrigger
                            || iRow == (int)clsPmtDispValve.enuPmtName.eDO_TriggerEnable)
                        {
                            #region//eDo
                            clsEnum.enuDo? CurrentValue = null;
                            if (Enum.IsDefined(typeof(clsEnum.enuDo), pDicPmt[(clsPmtDispValve.enuPmtName)iRow]) == true)
                            {
                                CurrentValue = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), pDicPmt[(clsPmtDispValve.enuPmtName)iRow]);
                            }
                            if (ucGetEnumDO.GetSingleton()._ShowFormDialog(ref CurrentValue) == DialogResult.OK)
                            {
                                if (CurrentValue == null)
                                {
                                    pDicPmt[(clsPmtDispValve.enuPmtName)iRow] = "";
                                }
                                else
                                {
                                    pDicPmt[(clsPmtDispValve.enuPmtName)iRow] = CurrentValue.ToString();
                                }
                            }
                            #endregion
                        }
                        else if (iRow == (int)clsPmtDispValve.enuPmtName.Serial_StationID
                            || iRow == (int)clsPmtDispValve.enuPmtName.Serial_COMID)
                        {
                            string sOrgValue = pDicPmt[(clsPmtDispValve.enuPmtName)iRow];
                            if (FormNumBox.GetSingleton().ShowDialog(this, sOrgValue, 99999, 0, 0) == DialogResult.OK)
                            {
                                pDicPmt[(clsPmtDispValve.enuPmtName)iRow] = FormNumBox.GetSingleton().NumBoxValue.ToString();
                            }
                        }
                    }
                }
                dgvSetDispValve.EndEdit();
                ConvertDataToUI();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
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
            string sValveType = "";
            List<string> LstValveType = Enum.GetNames(typeof(clsCtrlDispValve.enuValveType)).ToList<string>();
            if (clsDialogShow.SelectItem("Select Valve Type", LstValveType, ref sValveType) == DialogResult.OK)
            {
                if (clsDialogShow.InputString("Add Heater Module", "New Module Name :", ref NewName) == DialogResult.OK)
                {
                    if (mPmt.mDic_mPmtValue.ContainsKey(NewName) == true)
                    {
                        formMessageBox.Show("Name Already Exist : \r\n" + NewName);
                        return;
                    }
                    mPmt.mDic_mPmtValue.Add(NewName, new Dictionary<clsPmtDispValve.enuPmtName, string>());
                    foreach (clsPmtDispValve.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtDispValve.enuPmtName)))
                    {
                        if (mPmt.mDic_mPmtValue[NewName].ContainsKey(ePmt) == false)
                        {
                            if (ePmt != clsPmtDispValve.enuPmtName.eDI_Done
                                && ePmt != clsPmtDispValve.enuPmtName.eDI_Ready
                                && ePmt != clsPmtDispValve.enuPmtName.eDO_ManualTrigger
                                && ePmt != clsPmtDispValve.enuPmtName.eDO_TriggerEnable
                                && ePmt != clsPmtDispValve.enuPmtName.eDO_Interrupt)
                            {
                                mPmt.mDic_mPmtValue[NewName].Add(ePmt, "0");
                            }
                            else
                            {
                                mPmt.mDic_mPmtValue[NewName].Add(ePmt, "");
                            }
                        }
                    }
                    mPmt.mDic_mPmtValue[NewName][clsPmtDispValve.enuPmtName.sValveName] = NewName;
                    mPmt.mDic_mPmtValue[NewName][clsPmtDispValve.enuPmtName.eValveType] = sValveType;
                    mPmt.mDic_mPmtValue[NewName][clsPmtDispValve.enuPmtName.Serial_COMID] = "0";
                    mPmt.mDic_mPmtValue[NewName][clsPmtDispValve.enuPmtName.Serial_BaudRate] = "19200";
                    mPmt.mDic_mPmtValue[NewName][clsPmtDispValve.enuPmtName.Serial_DataBits] = "8";
                    mPmt.mDic_mPmtValue[NewName][clsPmtDispValve.enuPmtName.Serial_StationID] = "1";
                    mPmt.mDic_mPmtValue[NewName][clsPmtDispValve.enuPmtName.Serial_TimeOut] = "1000";
                    mPmt.mDic_mPmtValue[NewName][clsPmtDispValve.enuPmtName.Serial_Handshake] = System.IO.Ports.Handshake.None.ToString();
                    mPmt.mDic_mPmtValue[NewName][clsPmtDispValve.enuPmtName.Serial_StopBits] = System.IO.Ports.StopBits.One.ToString();
                    mPmt.mDic_mPmtValue[NewName][clsPmtDispValve.enuPmtName.Serial_Parity] = System.IO.Ports.Parity.None.ToString();
                    UpdateControls();
                }
            }
        }
        private void btnDelete_HeaterModuleSetting_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSetDispValve.SelectedCells.Count > 0)
                {
                    int iSelectIndex = dgvSetDispValve.SelectedCells[0].ColumnIndex - 1;
                    if (iSelectIndex >= 0 && iSelectIndex < mPmt.mDic_mPmtValue.Count)
                    {
                        string sKey = mPmt.mDic_mPmtValue.ElementAt(iSelectIndex).Key;
                        if (mPmt.mDic_mPmtValue.ContainsKey(sKey) == true)
                        {
                            mPmt.mDic_mPmtValue.Remove(sKey);
                        }
                    }
                }
                UpdateControls();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);

            }
        }
        private void dgvSetHeaterModule_EnabledChanged(object sender, EventArgs e)
        {
            btnEdit_HeaterModuleSetting.Enabled = true;
            btnSave_HeaterModuleSetting.Enabled = dgvSetDispValve.Enabled;
            btnCancel_HeaterModuleSetting.Enabled = dgvSetDispValve.Enabled;
            btnAdd_HeaterModuleSetting.Enabled = dgvSetDispValve.Enabled;
            btnDelete_HeaterModuleSetting.Enabled = dgvSetDispValve.Enabled;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            bAdvanceSetting = !bAdvanceSetting;
            UpdateControls();
        }

    }
}

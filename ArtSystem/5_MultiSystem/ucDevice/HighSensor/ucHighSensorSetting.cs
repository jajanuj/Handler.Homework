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
    public partial class ucHighSensorSetting : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public clsPmtHighSensor mPmt = new clsPmtHighSensor();
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
        static private ucHighSensorSetting m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucHighSensorSetting GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucHighSensorSetting();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucHighSensorSetting()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }

            dgvSensorModule.Items.Clear();
            foreach (clsCtrlHighSensor.enuSensorModule eEnum in Enum.GetValues(typeof(clsCtrlHighSensor.enuSensorModule)))
            {
                dgvSensorModule.Items.Add(eEnum.ToString());
            }
            dgvSensorType.Items.Clear();
            foreach (clsCtrlHighSensor.enuSensorType eEnum in Enum.GetValues(typeof(clsCtrlHighSensor.enuSensorType)))
            {
                dgvSensorType.Items.Add(eEnum.ToString());
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
                txt_HighSensorPath.Text = mPmt.sINIPath;
                dgvSetHighSensor.Enabled = bIsEditing;
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
                SetBtnColorFlash(btnSave_HighSensorSetting, bIsEditing);
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
            mForm.Text = clsLanguage.GetTranslation("Set High Sensor Parameter");
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
                if (dgvSetHighSensor.Rows.Count > iDeviceNum)
                {
                    int RemoveRowCount = dgvSetHighSensor.Rows.Count - iDeviceNum;
                    for (int i = 0; i < RemoveRowCount; i++)
                    {
                        dgvSetHighSensor.Rows.RemoveAt(dgvSetHighSensor.Rows.Count - 1);
                    }
                }
                else if (dgvSetHighSensor.Rows.Count < iDeviceNum)
                {
                    int AddRowCount = iDeviceNum - dgvSetHighSensor.Rows.Count;
                    for (int i = 0; i < AddRowCount; i++)
                    {
                        dgvSetHighSensor.Rows.Add();
                    }
                }
                #endregion
                for (int iRow = 0; iRow < iDeviceNum; iRow++)
                {
                    if (iRow < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtHighSensor.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iRow).Value;
                        foreach (clsPmtHighSensor.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtRollerMotor.enuPmtName)))
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
                        dgvSetHighSensor[dgvNo.Index, iRow].Value = (iRow + 1).ToString();
                        dgvSetHighSensor[dgvSensorName.Index, iRow].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;
                        dgvSetHighSensor[dgvSensorModule.Index, iRow].Value = pDicPmt[clsPmtHighSensor.enuPmtName.SensorModule];
                        dgvSetHighSensor[dgvSensorType.Index, iRow].Value = pDicPmt[clsPmtHighSensor.enuPmtName.SensorType];
                        dgvSetHighSensor[dgvComPort.Index, iRow].Value = pDicPmt[clsPmtHighSensor.enuPmtName.Serial_COMID];
                        dgvSetHighSensor[dgvCOMBaudRate.Index, iRow].Value = pDicPmt[clsPmtHighSensor.enuPmtName.Serial_BaudRate];
                        dgvSetHighSensor[dgvCOMBits.Index, iRow].Value = pDicPmt[clsPmtHighSensor.enuPmtName.Serial_DataBits];
                        dgvSetHighSensor[dgvCOMStationID.Index, iRow].Value = pDicPmt[clsPmtHighSensor.enuPmtName.Serial_StationID];
                        dgvSetHighSensor[dgvTCPIP.Index, iRow].Value = pDicPmt[clsPmtHighSensor.enuPmtName.TCP_IP];
                        dgvSetHighSensor[dgvTCPPort.Index, iRow].Value = pDicPmt[clsPmtHighSensor.enuPmtName.TCP_Port];
                        dgvSetHighSensor[dgvInvert.Index, iRow].Value = pDicPmt[clsPmtHighSensor.enuPmtName.Invert] == "1" ? true : false;
                        if (Enum.IsDefined(typeof(clsEnum.enuDi), pDicPmt[clsPmtHighSensor.enuPmtName.eDi_LaserResult]) == true)
                        {
                            dgvSetHighSensor[dgvLaserDI.Index, iRow].Value = pDicPmt[clsPmtHighSensor.enuPmtName.eDi_LaserResult];
                        }
                        else
                        {
                            dgvSetHighSensor[dgvLaserDI.Index, iRow].Value = "";
                        }
                        if (Enum.IsDefined(typeof(clsEnum.enuDo), pDicPmt[clsPmtHighSensor.enuPmtName.eDo_Cylinder]) == true)
                        {
                            dgvSetHighSensor[dgvTouchCylinder_Do.Index, iRow].Value = pDicPmt[clsPmtHighSensor.enuPmtName.eDo_Cylinder];
                        }
                        else
                        {
                            dgvSetHighSensor[dgvTouchCylinder_Do.Index, iRow].Value = "";
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
                dgvSetHighSensor.EndEdit();
                dgvSetHighSensor.Refresh();
                for (int iRow = 0; iRow < iDeviceNum; iRow++)
                {
                    if (iRow < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtHighSensor.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iRow).Value;
                        foreach (clsPmtHighSensor.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtRollerMotor.enuPmtName)))
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
                        dgvSetHighSensor[dgvNo.Index, iRow].Value = (iRow + 1).ToString();
                        dgvSetHighSensor[dgvSensorName.Index, iRow].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;


                        pDicPmt[clsPmtHighSensor.enuPmtName.SensorModule] = ToString(dgvSetHighSensor[dgvSensorModule.Index, iRow].Value);
                        pDicPmt[clsPmtHighSensor.enuPmtName.SensorType] = ToString(dgvSetHighSensor[dgvSensorType.Index, iRow].Value);
                        pDicPmt[clsPmtHighSensor.enuPmtName.Serial_COMID] = ToString(dgvSetHighSensor[dgvComPort.Index, iRow].Value);
                        pDicPmt[clsPmtHighSensor.enuPmtName.Serial_BaudRate] = ToString(dgvSetHighSensor[dgvCOMBaudRate.Index, iRow].Value);
                        pDicPmt[clsPmtHighSensor.enuPmtName.Serial_DataBits] = ToString(dgvSetHighSensor[dgvCOMBits.Index, iRow].Value);
                        pDicPmt[clsPmtHighSensor.enuPmtName.Serial_StationID] = ToString(dgvSetHighSensor[dgvCOMStationID.Index, iRow].Value);
                        pDicPmt[clsPmtHighSensor.enuPmtName.TCP_IP] =ToString( dgvSetHighSensor[dgvTCPIP.Index, iRow].Value);
                        pDicPmt[clsPmtHighSensor.enuPmtName.TCP_Port] = ToString(dgvSetHighSensor[dgvTCPPort.Index, iRow].Value);
                        pDicPmt[clsPmtHighSensor.enuPmtName.Invert] = Convert.ToBoolean(dgvSetHighSensor[dgvInvert.Index, iRow].Value) == true ? "1" : "0";
                        pDicPmt[clsPmtHighSensor.enuPmtName.eDi_LaserResult] = ToString(dgvSetHighSensor[dgvLaserDI.Index, iRow].Value);
                        pDicPmt[clsPmtHighSensor.enuPmtName.eDo_Cylinder] = ToString(dgvSetHighSensor[dgvTouchCylinder_Do.Index, iRow].Value);
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
                if (mDgv.CurrentCell.ColumnIndex != dgvTCPIP.Index
                    && mDgv.CurrentCell.ColumnIndex != dgvLaserDI.Index
                    && mDgv.CurrentCell.ColumnIndex != dgvTouchCylinder_Do.Index)
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

        private void ucHighSensorSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                bIsEditing = false;
                UpdateControls();
            }
            this.SetReflashTimerStart(this.Visible);
        }

        private void dgvSetHighSensor_CellClick(object sender, DataGridViewCellEventArgs e)
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
                        Dictionary<clsPmtHighSensor.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(mDgv.CurrentCell.RowIndex).Value;
                        if (e.ColumnIndex == dgvLaserDI.Index)
                        {
                            #region//eDi_LaserResult
                            clsEnum.enuDi? CurrentValue = null;
                            if (Enum.IsDefined(typeof(clsEnum.enuDi), pDicPmt[clsPmtHighSensor.enuPmtName.eDi_LaserResult]) == true)
                            {
                                CurrentValue = (clsEnum.enuDi)Enum.Parse(typeof(clsEnum.enuDi), pDicPmt[clsPmtHighSensor.enuPmtName.eDi_LaserResult]);
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
                        else if (e.ColumnIndex == dgvTouchCylinder_Do.Index)
                        {
                            #region//eDo_Cylinder
                            clsEnum.enuDo? CurrentValue = null;
                            if (Enum.IsDefined(typeof(clsEnum.enuDo), pDicPmt[clsPmtHighSensor.enuPmtName.eDo_Cylinder]) == true)
                            {
                                CurrentValue = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), pDicPmt[clsPmtHighSensor.enuPmtName.eDo_Cylinder]);
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
                    }
                }
            }
            dgvSetHighSensor.EndEdit();
            ConvertUIToData();

        }
        #endregion

        #region//===================== 以下為事件處理 (Edit, Cancel, Save, Add, Delete) =====================

        private void btnEdit_HighSensorSetting_Click(object sender, EventArgs e)
        {
            bIsEditing = !bIsEditing;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnSave_HighSensorSetting_Click(object sender, EventArgs e)
        {
            if (formMessageBox.Show("Save setting need to restart program.", "Save High Sensor Setting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConvertUIToData();
                bIsEditing = false;
                mPmt.Save(mPmt.sINIPath);
                UpdateControls();
                clsMultiSystem.SetSettingChangeFlag();
            }
        }
        private void btnCancel_HighSensorSetting_Click(object sender, EventArgs e)
        {

            bIsEditing = false;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnAdd_HighSensorSetting_Click(object sender, EventArgs e)
        {
            string NewName  = "";
            if (clsDialogShow.InputString("Add High Sensor", "New Sensor Name :", ref NewName) == DialogResult.OK)
            {
                if (mPmt.mDic_mPmtValue.ContainsKey(NewName) == true)
                {
                    formMessageBox.Show("Name Already Exist : \r\n" + NewName);
                    return;
                }
                mPmt.mDic_mPmtValue.Add(NewName, new Dictionary<clsPmtHighSensor.enuPmtName, string>());
                foreach (clsPmtHighSensor.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtHighSensor.enuPmtName)))
                {
                    if (mPmt.mDic_mPmtValue[NewName].ContainsKey(ePmt) == false)
                    {
                        mPmt.mDic_mPmtValue[NewName].Add(ePmt, "0");
                    }
                }
                mPmt.mDic_mPmtValue[NewName][clsPmtHighSensor.enuPmtName.SensorName] = NewName;
                mPmt.mDic_mPmtValue[NewName][clsPmtHighSensor.enuPmtName.SensorModule] = clsCtrlHighSensor.enuSensorModule.KeyenceLaser_ComPort.ToString();
                mPmt.mDic_mPmtValue[NewName][clsPmtHighSensor.enuPmtName.SensorType] = clsCtrlHighSensor.enuSensorType.Laser.ToString();
                mPmt.mDic_mPmtValue[NewName][clsPmtHighSensor.enuPmtName.TCP_IP] = "192.168.1.1";
                mPmt.mDic_mPmtValue[NewName][clsPmtHighSensor.enuPmtName.TCP_Port] = "1004";
                mPmt.mDic_mPmtValue[NewName][clsPmtHighSensor.enuPmtName.Serial_COMID] = "1";
                mPmt.mDic_mPmtValue[NewName][clsPmtHighSensor.enuPmtName.Serial_BaudRate] = "9600";
                mPmt.mDic_mPmtValue[NewName][clsPmtHighSensor.enuPmtName.Serial_DataBits] = "8";
                mPmt.mDic_mPmtValue[NewName][clsPmtHighSensor.enuPmtName.Serial_StationID] = "0";
                UpdateControls();
            }


        }
        private void btnDelete_HighSensorSetting_Click(object sender, EventArgs e)
        {
            if (dgvSetHighSensor.SelectedCells.Count > 0)
            {
                int iSelectIndex = dgvSetHighSensor.SelectedCells[0].RowIndex;
                string sSelectName = dgvSetHighSensor[dgvSensorName.Index, iSelectIndex].Value.ToString();
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
        private void dgvSetHighSensor_EnabledChanged(object sender, EventArgs e)
        {
            btnEdit_HighSensorSetting.Enabled = true;
            btnSave_HighSensorSetting.Enabled = dgvSetHighSensor.Enabled;
            btnCancel_HighSensorSetting.Enabled = dgvSetHighSensor.Enabled;
            btnAdd_HighSensorSetting.Enabled = dgvSetHighSensor.Enabled;
            btnDelete_HighSensorSetting.Enabled = dgvSetHighSensor.Enabled;
        }

        #endregion

    }
}

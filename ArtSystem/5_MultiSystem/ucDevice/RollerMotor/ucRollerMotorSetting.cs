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
    public partial class ucRollerMotorSetting : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        public clsPmtRollerMotor mPmt = new clsPmtRollerMotor();
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
        private List<string> RowHeader = new List<string>();
        #endregion

        #region //=====================  必要函式設置 =====================

        static object m_LockObj = new object();
        static private ucRollerMotorSetting m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucRollerMotorSetting GetSingleton()
        {
            lock(m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucRollerMotorSetting();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucRollerMotorSetting()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }

            dgvRollerType.Items.Clear();
            foreach (clsPmtRollerMotor.enuRollerType eType in Enum.GetValues(typeof(clsPmtRollerMotor.enuRollerType)))
            {
                dgvRollerType.Items.Add(eType.ToString());
            }
            dgvEnumHighSpeed.Items.Clear();
            dgvEnumLowSpeed.Items.Clear();
            dgvEnumHighSpeed.Items.Add("");
            dgvEnumLowSpeed.Items.Add("");
            foreach (clsEnum.enuPmtName ePmtName in Enum.GetValues(typeof(clsEnum.enuPmtName)))
            {
                dgvEnumHighSpeed.Items.Add(ePmtName.ToString());
                dgvEnumLowSpeed.Items.Add(ePmtName.ToString());
            }
            RowHeader.Clear();
            for(int i= 0;i<dgvSetRollerMotor.Columns.Count;i++)
            {
                string strTitle = dgvSetRollerMotor.Columns[i].HeaderText;
                RowHeader.Add(strTitle);
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
                dgvSetRollerMotor.Enabled = bIsEditing;
                txt_FilePath.Text = mPmt.sINIPath;
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
                SetBtnColorFlash(btnSave_RollerMotorSetting, bIsEditing);
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
            mForm.Text = clsLanguage.GetTranslation("Set Roller Motor Parameter");
            mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
            mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
            mForm.ShowDialog();
            ConvertUIToData();
        }

        public string _RollerMotor_GetPmt_Value(int iIndex, clsPmtRollerMotor.enuPmtName pPmtName)
        {
            string rValue = "";
            if (iIndex >= 0 && iIndex < ucRollerMotorSetting.GetSingleton().mPmt.mDic_mPmtValue.Count)
            {
                rValue = ucRollerMotorSetting.GetSingleton().mPmt.mDic_mPmtValue.ElementAt(iIndex).Value[pPmtName];
                if (pPmtName == clsPmtRollerMotor.enuPmtName.HighSpeed_pps)
                {
                    string sEnumPmtHighSpeed = ucRollerMotorSetting.GetSingleton().mPmt.mDic_mPmtValue.ElementAt(iIndex).Value[clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed];
                    if (Enum.IsDefined(typeof(clsEnum.enuPmtName), sEnumPmtHighSpeed) == true)
                    {
                        clsEnum.enuPmtName ePmt = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), sEnumPmtHighSpeed);
                        rValue = ucParameter.GetValueString(ePmt);
                    }
                }
                else if (pPmtName == clsPmtRollerMotor.enuPmtName.LowSpeed_pps)
                {
                    string sEnumPmtLowSpeed = ucRollerMotorSetting.GetSingleton().mPmt.mDic_mPmtValue.ElementAt(iIndex).Value[clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed];
                    if (Enum.IsDefined(typeof(clsEnum.enuPmtName), sEnumPmtLowSpeed) == true)
                    {
                        clsEnum.enuPmtName ePmt = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), sEnumPmtLowSpeed);
                        rValue = ucParameter.GetValueString(ePmt);
                    }
                }
            }
            return rValue;
        }
        public string _RollerMotor_GetPmt_Value(string sIndex, clsPmtRollerMotor.enuPmtName pPmtName)
        {
            string rValue = "";
            if (ucRollerMotorSetting.GetSingleton().mPmt.mDic_mPmtValue.ContainsKey(sIndex) == true)
            {
                rValue = ucRollerMotorSetting.GetSingleton().mPmt.mDic_mPmtValue[sIndex][pPmtName];
                if (pPmtName == clsPmtRollerMotor.enuPmtName.HighSpeed_pps)
                {
                    string sEnumPmtHighSpeed = ucRollerMotorSetting.GetSingleton().mPmt.mDic_mPmtValue[sIndex][clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed];
                    if (Enum.IsDefined(typeof(clsEnum.enuPmtName), sEnumPmtHighSpeed) == true)
                    {
                        clsEnum.enuPmtName ePmt = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), sEnumPmtHighSpeed);
                        if (ucParameter.GetValueDouble(ePmt) == 0)
                        { ucParameter.SaveValue(clsEnum.enuPmtType.System, ePmt, ucRollerMotorSetting.GetSingleton().mPmt.mDic_mPmtValue[sIndex][clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed]); }
                        rValue = ucParameter.GetValueString(ePmt);
                    }
                }
                else if (pPmtName == clsPmtRollerMotor.enuPmtName.LowSpeed_pps)
                {
                    string sEnumPmtLowSpeed = ucRollerMotorSetting.GetSingleton().mPmt.mDic_mPmtValue[sIndex][clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed];
                    if (Enum.IsDefined(typeof(clsEnum.enuPmtName), sEnumPmtLowSpeed) == true)
                    {
                        clsEnum.enuPmtName ePmt = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), sEnumPmtLowSpeed);
                        if (ucParameter.GetValueDouble(ePmt) == 0)
                        { ucParameter.SaveValue(clsEnum.enuPmtType.System, ePmt, ucRollerMotorSetting.GetSingleton().mPmt.mDic_mPmtValue[sIndex][clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed]); }
                        rValue = ucParameter.GetValueString(ePmt);
                    }
                }
            }
            return rValue;
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
                #region//將Column行數調整成與mCardInfo.mLstMotionCardInfo.Count相等
                if (dgvSetRollerMotor.Columns.Count != iDeviceNum
                    || dgvSetRollerMotor.Columns[0].Name == "Column_0"
                    || dgvSetRollerMotor.Rows.Count == 0)
                {
                    dgvSetRollerMotor.Columns.Clear();
                    for (int i = 0; i < iDeviceNum; i++)
                    {
                        Dictionary<clsPmtRollerMotor.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(i).Value;
                        dgvSetRollerMotor.Columns.Add("Column_" + i, clsLanguage.GetTranslation(pDicPmt[clsPmtRollerMotor.enuPmtName.RollerName]));
                        dgvSetRollerMotor.Columns[dgvSetRollerMotor.Columns.Count - 1].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvSetRollerMotor.Columns[dgvSetRollerMotor.Columns.Count - 1].Width = 250;
                    }
                    dgvSetRollerMotor.Rows.Clear();
                    if (iDeviceNum == 0)
                    {
                        dgvSetRollerMotor.Columns.Add("Column_0", "Column_0");
                        dgvSetRollerMotor.Columns[0].Visible = false;
                    }
                    //List<string> RowHeader = new List<string>()
                    //{
                    //    "Roller Type",
                    //    "Com Port",
                    //    "Motor ID",
                    //    "High Speed(pps)",
                    //    "Low Speed(pps)",
                    //    "Invert",
                    //    "High Speed(SysPmt)",
                    //    "Low Speed(SysPmt)",
                    //    "Start(DO).",
                    //    "Slow(DO).",
                    //    "Reverse(DO).",
                    //};
                    dgvSetRollerMotor.RowHeadersWidth = 200;
                    for (int i = 0; i < RowHeader.Count; i++)
                    {
                        dgvSetRollerMotor.Rows.Add();
                        dgvSetRollerMotor.Rows[i].HeaderCell.Value = clsLanguage.GetTranslation(RowHeader[i]);
                        if (RowHeader[i] == "Invert")
                        {
                            for (int c = 0; c < dgvSetRollerMotor.Columns.Count; c++)
                            {
                                dgvSetRollerMotor[c, i] = new DataGridViewCheckBoxCell();
                            }
                        }
                        else if (RowHeader[i] == "Roller Type")
                        {
                            for (int c = 0; c < dgvSetRollerMotor.Columns.Count; c++)
                            {
                                DataGridViewComboBoxCell Item = new DataGridViewComboBoxCell();
                                foreach (clsPmtRollerMotor.enuRollerType eType in Enum.GetValues(typeof(clsPmtRollerMotor.enuRollerType)))
                                {
                                    Item.Items.Add(eType.ToString());
                                }
                                dgvSetRollerMotor[c, i] = Item;
                            }
                        }
                    }
                }
                #endregion
                for (int iNo = 0; iNo < iDeviceNum; iNo++)
                {
                    if (iNo < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtRollerMotor.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iNo).Value;
                        foreach (clsPmtRollerMotor.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtRollerMotor.enuPmtName)))
                        {
                            if (ePmt == clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed
                                && ePmt == clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed)
                            {
                                if (pDicPmt.ContainsKey(ePmt) == false)
                                {
                                    pDicPmt.Add(ePmt, "");
                                }
                            }
                            else
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
                        }
                        //dgvSetRollerMotor[iNo, dgvNo.Index].Value = (iNo + 1).ToString();
                        //dgvSetRollerMotor[iNo, dgvRollerName.Index].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;
                        dgvSetRollerMotor[iNo, dgvComPort.Index].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.COM_Port];
                        dgvSetRollerMotor[iNo, dgvMotorID.Index].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.Motor_ID];
                        dgvSetRollerMotor[iNo, dgvRollerType.Index].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.RollerType];
                        dgvSetRollerMotor[iNo, dgvHighSpeed.Index].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.HighSpeed_pps];
                        dgvSetRollerMotor[iNo, dgvLowSpeed.Index].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.LowSpeed_pps];
                        dgvSetRollerMotor[iNo, dgvInvert.Index].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.Invert] == "1" ? true : false;
                        dgvSetRollerMotor[iNo, dgvCurrentPower.Index].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.CurrentPower];
                        if (Enum.IsDefined(typeof(clsEnum.enuPmtName), pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed]) == true)
                        {
                            dgvSetRollerMotor[iNo, dgvEnumHighSpeed.Index].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed];
                        }
                        else
                        {
                            dgvSetRollerMotor[iNo, dgvEnumHighSpeed.Index].Value = "";
                        }
                        if (Enum.IsDefined(typeof(clsEnum.enuPmtName), pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed]) == true)
                        {
                            dgvSetRollerMotor[iNo, dgvEnumLowSpeed.Index].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed];
                        }
                        else
                        {
                            dgvSetRollerMotor[iNo, dgvEnumLowSpeed.Index].Value = "";
                        }


                        if (Enum.IsDefined(typeof(clsEnum.enuDo), pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Start]) == true)
                        {
                            dgvSetRollerMotor[iNo, dgvDOStart.Index].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Start];
                        }
                        else
                        {
                            dgvSetRollerMotor[iNo, dgvDOStart.Index].Value = "";
                        }

                        if (Enum.IsDefined(typeof(clsEnum.enuDo), pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Slow]) == true)
                        {
                            dgvSetRollerMotor[iNo, dgvDOSlow.Index].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Slow];
                        }
                        else
                        {
                            dgvSetRollerMotor[iNo, dgvDOSlow.Index].Value = "";
                        }

                        if (Enum.IsDefined(typeof(clsEnum.enuDo), pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Reverse]) == true)
                        {
                            dgvSetRollerMotor[iNo, dgvDOReverse.Index].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Reverse];
                        }
                        else
                        {
                            dgvSetRollerMotor[iNo, dgvDOReverse.Index].Value = "";
                        }
                    }
                }
                //return;
                //#region//將Row行數調整成與mCardInfo.mLstMotionCardInfo.Count相等
                //if (dgvSetRollerMotor.Rows.Count > iDeviceNum)
                //{
                //    int RemoveRowCount = dgvSetRollerMotor.Rows.Count - iDeviceNum;
                //    for (int i = 0; i < RemoveRowCount; i++)
                //    {
                //        dgvSetRollerMotor.Rows.RemoveAt(dgvSetRollerMotor.Rows.Count - 1);
                //    }
                //}
                //else if (dgvSetRollerMotor.Rows.Count < iDeviceNum)
                //{
                //    int AddRowCount = iDeviceNum - dgvSetRollerMotor.Rows.Count;
                //    for (int i = 0; i < AddRowCount; i++)
                //    {
                //        dgvSetRollerMotor.Rows.Add();
                //    }
                //}
                //#endregion
                //for (int iRow = 0; iRow < iDeviceNum; iRow++)
                //{
                //    if (iRow < mPmt.mDic_mPmtValue.Count)
                //    {
                //        Dictionary<clsPmtRollerMotor.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iRow).Value;
                //        foreach (clsPmtRollerMotor.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtRollerMotor.enuPmtName)))
                //        {
                //            if (ePmt == clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed
                //                && ePmt == clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed)
                //            {
                //                if (pDicPmt.ContainsKey(ePmt) == false)
                //                {
                //                    pDicPmt.Add(ePmt, "");
                //                }
                //            }
                //            else
                //            {
                //                if (pDicPmt.ContainsKey(ePmt) == false)
                //                {
                //                    pDicPmt.Add(ePmt, "0");
                //                }
                //                if (pDicPmt[ePmt] == "")
                //                {
                //                    pDicPmt[ePmt] = "0";
                //                }
                //            }
                //        }
                //        dgvSetRollerMotor[dgvNo.Index, iRow].Value = (iRow + 1).ToString();
                //        //dgvSetRollerMotor[dgvRollerName.Index, iRow].Value = mPmt.mDic_mPmtValue.ElementAt(iRow).Key;
                //        dgvSetRollerMotor[dgvComPort.Index, iRow].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.COM_Port];
                //        dgvSetRollerMotor[dgvMotorID.Index, iRow].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.Motor_ID];
                //        dgvSetRollerMotor[dgvRollerType.Index, iRow].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.RollerType];
                //        dgvSetRollerMotor[dgvHighSpeed.Index, iRow].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.HighSpeed_pps];
                //        dgvSetRollerMotor[dgvLowSpeed.Index, iRow].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.LowSpeed_pps];
                //        dgvSetRollerMotor[dgvInvert.Index, iRow].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.Invert] == "1" ? true : false;
                //        if (Enum.IsDefined(typeof(clsEnum.enuPmtName), pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed]) == true)
                //        {
                //            dgvSetRollerMotor[dgvEnumHighSpeed.Index, iRow].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed];
                //        }
                //        else
                //        {
                //            dgvSetRollerMotor[dgvEnumHighSpeed.Index, iRow].Value = "";
                //        }
                //        if (Enum.IsDefined(typeof(clsEnum.enuPmtName), pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed]) == true)
                //        {
                //            dgvSetRollerMotor[dgvEnumLowSpeed.Index, iRow].Value = pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed];
                //        }
                //        else
                //        {
                //            dgvSetRollerMotor[dgvEnumLowSpeed.Index, iRow].Value = "";
                //        }
                //    }
                //}
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
                dgvSetRollerMotor.EndEdit();
                dgvSetRollerMotor.Refresh();
                for (int iIndex = 0; iIndex < iDeviceNum; iIndex++)
                {
                    if (iIndex < mPmt.mDic_mPmtValue.Count)
                    {
                        Dictionary<clsPmtRollerMotor.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(iIndex).Value;
                        foreach (clsPmtRollerMotor.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtRollerMotor.enuPmtName)))
                        {
                            if (ePmt != clsPmtRollerMotor.enuPmtName.eDO_Reverse
                                && ePmt != clsPmtRollerMotor.enuPmtName.eDO_Slow
                                && ePmt != clsPmtRollerMotor.enuPmtName.eDO_Start
                                && ePmt != clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed
                                && ePmt != clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed
                                && ePmt != clsPmtRollerMotor.enuPmtName.RollerType)
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
                            else
                            {
                                if (pDicPmt.ContainsKey(ePmt) == false)
                                {
                                    pDicPmt.Add(ePmt, "");
                                }
                            }
                        }
                        //dgvSetRollerMotor[iIndex, dgvNo.Index].Value = (iIndex + 1).ToString();
                        pDicPmt[clsPmtRollerMotor.enuPmtName.COM_Port] = ToString(dgvSetRollerMotor[iIndex, dgvComPort.Index].Value);
                        pDicPmt[clsPmtRollerMotor.enuPmtName.Motor_ID] = ToString(dgvSetRollerMotor[iIndex, dgvMotorID.Index].Value);
                        pDicPmt[clsPmtRollerMotor.enuPmtName.RollerType] = ToString(dgvSetRollerMotor[iIndex, dgvRollerType.Index].Value);
                        pDicPmt[clsPmtRollerMotor.enuPmtName.HighSpeed_pps] = ToString(dgvSetRollerMotor[iIndex, dgvHighSpeed.Index].Value);
                        pDicPmt[clsPmtRollerMotor.enuPmtName.LowSpeed_pps] = ToString(dgvSetRollerMotor[iIndex, dgvLowSpeed.Index].Value);
                        pDicPmt[clsPmtRollerMotor.enuPmtName.Invert] = Convert.ToBoolean(dgvSetRollerMotor[iIndex, dgvInvert.Index].Value) == true ? "1" : "0";
                        pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed] = ToString(dgvSetRollerMotor[iIndex, dgvEnumHighSpeed.Index].Value);
                        pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed] = ToString(dgvSetRollerMotor[iIndex, dgvEnumLowSpeed.Index].Value);
                        pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Start] = ToString(dgvSetRollerMotor[iIndex, dgvDOStart.Index].Value);
                        pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Slow] = ToString(dgvSetRollerMotor[iIndex, dgvDOSlow.Index].Value);
                        pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Reverse] = ToString(dgvSetRollerMotor[iIndex, dgvDOReverse.Index].Value);
                        pDicPmt[clsPmtRollerMotor.enuPmtName.CurrentPower] = ToString(dgvSetRollerMotor[iIndex, dgvCurrentPower.Index].Value);
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
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress_Decimal);
            if (mDgv.CurrentCell is DataGridViewTextBoxCell) //Desired Column
            {
                if (mDgv.CurrentCell.RowIndex == dgvCurrentPower.Index)
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress_Decimal);
                    }
                }
                else if (mDgv.CurrentCell.RowIndex != dgvEnumHighSpeed.Index
                    && mDgv.CurrentCell.RowIndex != dgvEnumLowSpeed.Index
                    && mDgv.CurrentCell.RowIndex != dgvDOStart.Index
                    && mDgv.CurrentCell.RowIndex != dgvDOSlow.Index
                    && mDgv.CurrentCell.RowIndex != dgvDOReverse.Index)
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

        private void Column1_KeyPress_Decimal(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.')
            {
                if (sender is TextBox)
                {
                    string strSender = ((TextBox)sender).Text;
                    if (strSender.Contains(".") == true)
                    {
                        e.Handled = true;
                    }
                }
            }
        }
        private void ucRollerMotorSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                bIsEditing = false;
                UpdateControls();
            }
            this.SetReflashTimerStart(this.Visible);
        }
        private void dgvSetRollerMotor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView mDgv = (DataGridView)sender;
            if (mDgv.CurrentCell is DataGridViewTextBoxCell) //Desired Column
            {
                if(mDgv.CurrentCell.ColumnIndex >= 0
                    && mDgv.CurrentCell.ColumnIndex < mPmt.mDic_mPmtValue.Count)
                {
                    Dictionary<clsPmtRollerMotor.enuPmtName, string> pDicPmt = mPmt.mDic_mPmtValue.ElementAt(mDgv.CurrentCell.ColumnIndex).Value;
                    if (mDgv.CurrentCell.RowIndex == dgvEnumHighSpeed.Index)
                    {
                        #region//Process
                        clsEnum.enuPmtName? CurrentValue = null;
                        if(Enum.IsDefined(typeof(clsEnum.enuPmtName), pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed]) == true)
                        {
                           CurrentValue = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed]);
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
                    else if (mDgv.CurrentCell.RowIndex == dgvEnumLowSpeed.Index)
                    {
                        #region//Process
                        clsEnum.enuPmtName? CurrentValue = null;
                        if (Enum.IsDefined(typeof(clsEnum.enuPmtName), pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed]) == true)
                        {
                            CurrentValue = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), pDicPmt[clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed]);
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
                    else if (mDgv.CurrentCell.RowIndex == dgvDOStart.Index)
                    {
                        #region//Process
                        clsEnum.enuDo? CurrentValue = null;
                        if (Enum.IsDefined(typeof(clsEnum.enuDo), pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Start]) == true)
                        {
                            CurrentValue = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Start]);
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
                    else if (mDgv.CurrentCell.RowIndex == dgvDOSlow.Index)
                    {
                        #region//Process
                        clsEnum.enuDo? CurrentValue = null;
                        if (Enum.IsDefined(typeof(clsEnum.enuDo), pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Slow]) == true)
                        {
                            CurrentValue = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Slow]);
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
                    else if (mDgv.CurrentCell.RowIndex == dgvDOReverse.Index)
                    {
                        #region//Process
                        clsEnum.enuDo? CurrentValue = null;
                        if (Enum.IsDefined(typeof(clsEnum.enuDo), pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Reverse]) == true)
                        {
                            CurrentValue = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), pDicPmt[clsPmtRollerMotor.enuPmtName.eDO_Reverse]);
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
            dgvSetRollerMotor.EndEdit();
            ConvertUIToData();
        }
        #endregion

        #region//===================== 以下為事件處理 (Edit, Cancel, Save, Add, Delete) =====================

        private void btnEdit_RollerMotorSetting_Click(object sender, EventArgs e)
        {
            bIsEditing = !bIsEditing;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnSave_RollerMotorSetting_Click(object sender, EventArgs e)
        {
            if (formMessageBox.Show("Save setting need to restart program.", "Save Roller Motor Setting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConvertUIToData();
                bIsEditing = false;
                mPmt.Save(mPmt.sINIPath);
                UpdateControls();
                clsMultiSystem.SetSettingChangeFlag();
            }
        }
        private void btnCancel_RollerMotorSetting_Click(object sender, EventArgs e)
        {

            bIsEditing = false;
            mPmt.Load(mPmt.sINIPath);
            UpdateControls();
        }
        private void btnAdd_RollerMotorSetting_Click(object sender, EventArgs e)
        {
            string NewName  = "";
            if(clsDialogShow.InputString("Add Roller Motor", "New Motor Name :", ref NewName) == DialogResult.OK)
            {
                if (mPmt.mDic_mPmtValue.ContainsKey(NewName) == true)
                {
                    formMessageBox.Show("Roller Name Already Exist : \r\n" + NewName);
                    return;
                }
                mPmt.mDic_mPmtValue.Add(NewName, new Dictionary<clsPmtRollerMotor.enuPmtName, string>());
                foreach (clsPmtRollerMotor.enuPmtName ePmt in Enum.GetValues(typeof(clsPmtRollerMotor.enuPmtName)))
                {
                    if (mPmt.mDic_mPmtValue[NewName].ContainsKey(ePmt) == false)
                    {
                        mPmt.mDic_mPmtValue[NewName].Add(ePmt, "0");
                    }
                }
                mPmt.mDic_mPmtValue[NewName][clsPmtRollerMotor.enuPmtName.RollerName] = NewName;
                mPmt.mDic_mPmtValue[NewName][clsPmtRollerMotor.enuPmtName.RollerType] = clsMotionRoller.enuRollerType.ExtionCommand.ToString();
                mPmt.mDic_mPmtValue[NewName][clsPmtRollerMotor.enuPmtName.HighSpeed_pps] = "3000";
                mPmt.mDic_mPmtValue[NewName][clsPmtRollerMotor.enuPmtName.LowSpeed_pps] = "1000";
                if (mPmt.mDic_mPmtValue.Count > 1)
                {
                    mPmt.mDic_mPmtValue[NewName][clsPmtRollerMotor.enuPmtName.COM_Port] = mPmt.mDic_mPmtValue.ElementAt(0).Value[clsPmtRollerMotor.enuPmtName.COM_Port];
                }
                else
                {
                    mPmt.mDic_mPmtValue[NewName][clsPmtRollerMotor.enuPmtName.COM_Port] = "1";
                }
                if(mPmt.mDic_mPmtValue.Count > 1)
                {
                    mPmt.mDic_mPmtValue[NewName][clsPmtRollerMotor.enuPmtName.Motor_ID] = (mPmt.mDic_mPmtValue.Count - 1).ToString();
                }
                else
                {
                    mPmt.mDic_mPmtValue[NewName][clsPmtRollerMotor.enuPmtName.Motor_ID] = "1";
                }
                mPmt.mDic_mPmtValue[NewName][clsPmtRollerMotor.enuPmtName.CurrentPower] = "0.5";
                UpdateControls();
            }


        }
        private void btnDelete_RollerMotorSetting_Click(object sender, EventArgs e)
        {
            if (dgvSetRollerMotor.SelectedCells.Count > 0)
            {
                int iSelectIndex = dgvSetRollerMotor.SelectedCells[0].ColumnIndex;
                string sSelectName = dgvSetRollerMotor.Columns[iSelectIndex].HeaderText;
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
        private void dgvSetRollerMotor_EnabledChanged(object sender, EventArgs e)
        {
            btnEdit_RollerMotorSetting.Enabled = true;
            btnSave_RollerMotorSetting.Enabled = dgvSetRollerMotor.Enabled;
            btnCancel_RollerMotorSetting.Enabled = dgvSetRollerMotor.Enabled;
            btnAdd_RollerMotorSetting.Enabled = dgvSetRollerMotor.Enabled;
            btnDelete_RollerMotorSetting.Enabled = dgvSetRollerMotor.Enabled;
        }

        #endregion


    }
}

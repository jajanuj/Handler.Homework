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
    public partial class ucCardSetting : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================

        private bool bIsEditing = false;
        public clsCardInfo mCardInfo = new clsCardInfo();
        public bool bAdvanceSetting = false;

        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucCardSetting m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucCardSetting GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucCardSetting();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucCardSetting()
        {
            InitializeComponent();
            //if (clsArtSystem.bIsProgramOpen == false)
            //{  return; }
            this.TimerInterval = 100;
            dgvMotion_CardType.Items.Clear();
            dgvIOCard_CardType.Items.Clear();
            foreach (clsMultiSystem.enuMotionCard eCard in Enum.GetValues(typeof(clsMultiSystem.enuMotionCard)))
            {
                dgvMotion_CardType.Items.Add(eCard.ToString());
                if(eCard != clsMultiSystem.enuMotionCard.ADVAN_1245
                    && eCard != clsMultiSystem.enuMotionCard.GTS_4
                    && eCard != clsMultiSystem.enuMotionCard.Etel
                    && eCard != clsMultiSystem.enuMotionCard.Etel4)
                {
                    dgvIOCard_CardType.Items.Add(eCard.ToString());
                }
            }
            dgvMotion_SlaveType.Items.Clear();
            foreach (enuSlaveType_Motion eSlaveType in Enum.GetValues(typeof(enuSlaveType_Motion)))
            {
                dgvMotion_SlaveType.Items.Add(eSlaveType.ToString());
            }
            dgvIOCard_SlaveType.Items.Clear();
            foreach (clsDIOCardSetup.enuDIOType eSlaveType in Enum.GetValues(typeof(clsDIOCardSetup.enuDIOType)))
            {
                dgvIOCard_SlaveType.Items.Add(eSlaveType.ToString());
            }
            dgvAIOCard_CardType.Items.Clear();
            foreach (clsAIOCardSetup.enuCardType eCard in Enum.GetValues(typeof(clsAIOCardSetup.enuCardType)))
            {
                dgvAIOCard_CardType.Items.Add(eCard.ToString());
            }
            bIsEditing = false;
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
                txt_MotionCardPath.Text = mCardInfo.sINIPath;
                txt_IOCardPath.Text = mCardInfo.sINIPath;
                txt_AIOCardPath.Text = mCardInfo.sINIPath;
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
                dgvMotion.Enabled = bIsEditing;
                dgvIOCard.Enabled = bIsEditing;
                dgvAIOCard.Enabled = bIsEditing;
                SetBtnColorFlash(btnSave_MotionCardSetting, bIsEditing);
                SetBtnColorFlash(btnSave_IOCardSetting, bIsEditing);
                SetBtnColorFlash(btnSave_AIOCardSetting, bIsEditing);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        #endregion

        #region //===================== private 函式設置 (SetBtnColor, Covert-Data&UI) =====================

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
                if (tabControl1.SelectedTab == tPage_MotionCard)
                {
                    #region//Motion Card
                    {
                        #region//將Row行數調整成與mCardInfo.mLstMotionCardInfo.Count相等
                        if (dgvMotion.Rows.Count > mCardInfo.mLstMotionCardInfo.Count)
                        {
                            int RemoveRowCount = dgvMotion.Rows.Count - mCardInfo.mLstMotionCardInfo.Count;
                            for (int i = 0; i < RemoveRowCount; i++)
                            {
                                dgvMotion.Rows.RemoveAt(dgvMotion.Rows.Count - 1);
                            }
                        }
                        else if (dgvMotion.Rows.Count < mCardInfo.mLstMotionCardInfo.Count)
                        {
                            int AddRowCount = mCardInfo.mLstMotionCardInfo.Count - dgvMotion.Rows.Count;
                            for (int i = 0; i < AddRowCount; i++)
                            {
                                dgvMotion.Rows.Add();
                            }
                        }
                        #endregion
                        #region//資料更新 mCardInfo.mLstMotionCardInfo -> UI
                        int iRowIndex = 0;
                        int AxisID = 0;
                        int AxisNum = 1;
                        foreach (clsMotionCardSetup pSetup in mCardInfo.mLstMotionCardInfo)
                        {
                            dgvMotion[dgvMotion_No.Index, iRowIndex].Value = iRowIndex + 1;
                            dgvMotion[dgvMotion_CardType.Index, iRowIndex].Value = pSetup.eMotionCard.ToString();
                            dgvMotion[dgvMotion_SlaveType.Index, iRowIndex].Value = pSetup.eSlaveType.ToString();
                            dgvMotion[dgvMotion_CardID.Index, iRowIndex].Value = pSetup.iCardID;
                            dgvMotion[dgvMotion_SlaveID.Index, iRowIndex].Value = pSetup.iSlaveID;
                            dgvMotion[dgvSimulate.Index, iRowIndex].Value = pSetup.bSimulate;
                            dgvMotion[dgvMotion_SlaveNum.Index, iRowIndex].Value = pSetup.iETEL_SlaveNum;
                            #region//計算軸數量 (AxisNum)
                            switch (pSetup.eMotionCard)
                            {
                                case clsMultiSystem.enuMotionCard.ADVAN_1245:
                                case clsMultiSystem.enuMotionCard.GTS_4:
                                case clsMultiSystem.enuMotionCard.Card7856:
                                case clsMultiSystem.enuMotionCard.L122:
                                case clsMultiSystem.enuMotionCard.ApsMasterTPM:
                                    AxisNum = 4;
                                    break;
                                case clsMultiSystem.enuMotionCard.Etel:
                                case clsMultiSystem.enuMotionCard.Etel4:
                                case clsMultiSystem.enuMotionCard.YaskawaMP3000:
                                    AxisNum = 2 * pSetup.iETEL_SlaveNum;
                                    break;
                                case clsMultiSystem.enuMotionCard.EtherCatMasterAdv:
                                    switch (pSetup.eSlaveType)
                                    {
                                        case enuSlaveType_Motion.Motion_1xAxis:
                                            AxisNum = 1;
                                            break;
                                        case enuSlaveType_Motion.Motion_2xAxis:
                                            AxisNum = 2;
                                            break;
                                        case enuSlaveType_Motion.Motion_4xAxis:
                                            AxisNum = 4;
                                            break;
                                        case enuSlaveType_Motion.Motion_8xAxis:
                                            AxisNum = 8;
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                default:
                                    AxisNum = 1;
                                    break;
                            }
                            #endregion
                            #region//顯示文字 (sAxisID)  dgvMotion[dgvMotion_AxisID.Index, iRowIndex].Value = sAxisID;
                            string sAxisID = "";
                            if (pSetup.eMotionCard == clsMultiSystem.enuMotionCard.Etel
                                || pSetup.eMotionCard == clsMultiSystem.enuMotionCard.Etel4)
                            {
                                for (int i = 0; i < pSetup.iETEL_SlaveNum; i++)
                                {
                                    if (pSetup.LstGantryAxis.Contains(i) == true)
                                    {
                                        if (sAxisID != "")
                                        { sAxisID += ","; }
                                        sAxisID += (AxisID) + "(" + (AxisID + 1) + ")";
                                    }
                                    else
                                    {
                                        if (sAxisID != "")
                                        { sAxisID += ","; }
                                        sAxisID += (AxisID) + "," + (AxisID + 1);
                                    }
                                    AxisID += 2;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < AxisNum; i++)
                                {
                                    if (sAxisID == "")
                                    {
                                        sAxisID += AxisID;
                                    }
                                    else
                                    {
                                        sAxisID += "," + AxisID;
                                    }
                                    AxisID++;
                                }
                            }
                            dgvMotion[dgvMotion_AxisID.Index, iRowIndex].Value = sAxisID;
                            #endregion
                            if (pSetup.eMotionCard == clsMultiSystem.enuMotionCard.Etel
                                || pSetup.eMotionCard == clsMultiSystem.enuMotionCard.Etel4)
                            {
                                dgvMotion[dgvMotion_Other.Index, iRowIndex].Value = "Set Gantry";
                            }
                            else if (pSetup.eMotionCard == clsMultiSystem.enuMotionCard.Card7856)
                            {
                                dgvMotion[dgvMotion_Other.Index, iRowIndex].Value = "Card Pmt";
                            }
                            else if (pSetup.eMotionCard == clsMultiSystem.enuMotionCard.ApsMasterTPM)
                            {
                                dgvMotion[dgvMotion_Other.Index, iRowIndex].Value = "Card Pmt";
                            }
                            else if (pSetup.eMotionCard == clsMultiSystem.enuMotionCard.EtherCatMasterAdv)
                            {
                                dgvMotion[dgvMotion_Other.Index, iRowIndex].Value = "Card Pmt";
                            }
                            else
                            {
                                dgvMotion[dgvMotion_Other.Index, iRowIndex].Value = "null";
                            }
                            dgvMotion[dgvMotion_AxisSetting.Index, iRowIndex].Value = "Axis Settting";
                            iRowIndex++;
                        }
                        #endregion
                    }
                    #endregion
                }

                if (tabControl1.SelectedTab == tPage_IOCard)
                {
                    #region//DIO Card
                    {
                        #region//將Row行數調整成與mCardInfo.mLstDIOCardInfo.Count相等
                        if (dgvIOCard.Rows.Count > mCardInfo.mLstDIOCardInfo.Count)
                        {
                            int RemoveRowCount = dgvIOCard.Rows.Count - mCardInfo.mLstDIOCardInfo.Count;
                            for (int i = 0; i < RemoveRowCount; i++)
                            {
                                dgvIOCard.Rows.RemoveAt(dgvIOCard.Rows.Count - 1);
                            }
                        }
                        else if (dgvIOCard.Rows.Count < mCardInfo.mLstDIOCardInfo.Count)
                        {
                            int AddRowCount = mCardInfo.mLstDIOCardInfo.Count - dgvIOCard.Rows.Count;
                            for (int i = 0; i < AddRowCount; i++)
                            {
                                dgvIOCard.Rows.Add();
                            }
                        }
                        #endregion

                        dgvIOCard.Columns[dgvIOCard_StartDI.Index].Visible = bAdvanceSetting;
                        dgvIOCard.Columns[dgvIOCard_StartDO.Index].Visible = bAdvanceSetting;

                        #region//資料更新 mCardInfo.mLstDIOCardInfo -> UI
                        int iRowIndex = 0;
                        foreach (clsDIOCardSetup pSetup in mCardInfo.mLstDIOCardInfo)
                        {
                            dgvIOCard[dgvIOCard_No.Index, iRowIndex].Value = iRowIndex + 1;
                            dgvIOCard[dgvIOCard_CardType.Index, iRowIndex].Value = pSetup.eSysCard.ToString();
                            dgvIOCard[dgvIOCard_SlaveType.Index, iRowIndex].Value = pSetup.eDIOType.ToString();
                            dgvIOCard[dgvIOCard_CardID.Index, iRowIndex].Value = pSetup.iCardID;
                            dgvIOCard[dgvIOCard_SlaveID.Index, iRowIndex].Value = pSetup.iSlaveID;
                            dgvIOCard[dgvIOCard_StartDI.Index, iRowIndex].Value = pSetup.eStartDi;
                            dgvIOCard[dgvIOCard_StartDO.Index, iRowIndex].Value = pSetup.eStartDo;
                            dgvIOCard[dgvIOCard_Simulate.Index, iRowIndex].Value = pSetup.bSimulate;
                            iRowIndex++;
                        }
                        #endregion
                    }
                    #endregion
                }

                if (tabControl1.SelectedTab == tPage_AIOCard)
                {
                    #region//AIO Card
                    {
                        #region//將Row行數調整成與mCardInfo.mLstAIOCardInfo.Count相等
                        if (dgvAIOCard.Rows.Count > mCardInfo.mLstAIOCardInfo.Count)
                        {
                            int RemoveRowCount = dgvAIOCard.Rows.Count - mCardInfo.mLstAIOCardInfo.Count;
                            for (int i = 0; i < RemoveRowCount; i++)
                            {
                                dgvAIOCard.Rows.RemoveAt(dgvAIOCard.Rows.Count - 1);
                            }
                        }
                        else if (dgvAIOCard.Rows.Count < mCardInfo.mLstAIOCardInfo.Count)
                        {
                            int AddRowCount = mCardInfo.mLstAIOCardInfo.Count - dgvAIOCard.Rows.Count;
                            for (int i = 0; i < AddRowCount; i++)
                            {
                                dgvAIOCard.Rows.Add();
                            }
                        }
                        #endregion

                        dgvAIOCard.Columns[dgvAIOCard_StartDI.Index].Visible = bAdvanceSetting;
                        dgvAIOCard.Columns[dgvAIOCard_StartDO.Index].Visible = bAdvanceSetting;

                        #region//資料更新 mCardInfo.mLstAIOCardInfo -> UI
                        int iRowIndex = 0;
                        foreach (clsAIOCardSetup pSetup in mCardInfo.mLstAIOCardInfo)
                        {
                            dgvAIOCard[dgvAIOCard_No.Index, iRowIndex].Value = iRowIndex + 1;
                            dgvAIOCard[dgvAIOCard_CardType.Index, iRowIndex].Value = pSetup.eCardType.ToString();
                            dgvAIOCard[dgvAIOCard_CardID.Index, iRowIndex].Value = pSetup.iCardID;
                            dgvAIOCard[dgvAIOCard_SlaveID.Index, iRowIndex].Value = pSetup.iSlaveID;
                            dgvAIOCard[dgvAIOCard_StartDI.Index, iRowIndex].Value = pSetup.eStartAi;
                            dgvAIOCard[dgvAIOCard_StartDO.Index, iRowIndex].Value = pSetup.eStartAo;
                            iRowIndex++;
                        }
                        #endregion
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
                dgvMotion.EndEdit();
                dgvIOCard.EndEdit();
                dgvAIOCard.EndEdit();
                if (tabControl1.SelectedTab == tPage_MotionCard)
                {
                    #region//Motion Card
                    {
                        #region//將mCardInfo.mLstMotionCardInfo.Count調整成與Row行數相等
                        if (mCardInfo.mLstMotionCardInfo.Count > dgvMotion.Rows.Count)
                        {
                            int RemoveRowCount = mCardInfo.mLstMotionCardInfo.Count - dgvMotion.Rows.Count;
                            for (int i = 0; i < RemoveRowCount; i++)
                            {
                                mCardInfo.mLstMotionCardInfo.RemoveAt(dgvMotion.Rows.Count - 1);
                            }
                        }
                        else if (mCardInfo.mLstMotionCardInfo.Count < dgvMotion.Rows.Count)
                        {
                            int AddRowCount = dgvMotion.Rows.Count - mCardInfo.mLstMotionCardInfo.Count;
                            for (int i = 0; i < AddRowCount; i++)
                            {
                                mCardInfo.mLstMotionCardInfo.Add(new clsMotionCardSetup());
                            }
                        }
                        #endregion

                        #region//資料更新 UI -> mCardInfo.mLstMotionCardInfo
                        int iRowIndex = 0;
                        foreach (clsMotionCardSetup pSetup in mCardInfo.mLstMotionCardInfo)
                        {
                            pSetup.eMotionCard = (clsMultiSystem.enuMotionCard)Enum.Parse(typeof(clsMultiSystem.enuMotionCard), dgvMotion[dgvMotion_CardType.Index, iRowIndex].Value.ToString());
                            pSetup.eSlaveType = (enuSlaveType_Motion)Enum.Parse(typeof(enuSlaveType_Motion), dgvMotion[dgvMotion_SlaveType.Index, iRowIndex].Value.ToString());
                            pSetup.iCardID = Convert.ToInt32(dgvMotion[dgvMotion_CardID.Index, iRowIndex].Value);
                            pSetup.iSlaveID = Convert.ToInt32(dgvMotion[dgvMotion_SlaveID.Index, iRowIndex].Value);
                            pSetup.iETEL_SlaveNum = Convert.ToInt32(dgvMotion[dgvMotion_SlaveNum.Index, iRowIndex].Value);
                            pSetup.bSimulate = Convert.ToBoolean(dgvMotion[dgvSimulate.Index, iRowIndex].Value);
                            iRowIndex++;
                        }
                        #endregion
                    }
                    #endregion
                }
                if (tabControl1.SelectedTab == tPage_IOCard)
                {
                    #region//DIO Card
                    {
                        #region//將Row行數調整成與mCardInfo.mLstDIOCardInfo.Count相等
                        if (mCardInfo.mLstDIOCardInfo.Count > dgvIOCard.Rows.Count)
                        {
                            int RemoveRowCount = mCardInfo.mLstDIOCardInfo.Count - dgvIOCard.Rows.Count;
                            for (int i = 0; i < RemoveRowCount; i++)
                            {
                                mCardInfo.mLstDIOCardInfo.RemoveAt(dgvIOCard.Rows.Count - 1);
                            }
                        }
                        else if (mCardInfo.mLstDIOCardInfo.Count < dgvIOCard.Rows.Count)
                        {
                            int AddRowCount = dgvIOCard.Rows.Count - mCardInfo.mLstDIOCardInfo.Count;
                            for (int i = 0; i < AddRowCount; i++)
                            {
                                mCardInfo.mLstDIOCardInfo.Add(new clsDIOCardSetup());
                            }
                        }
                        #endregion

                        #region//資料更新 mCardInfo.mLstDIOCardInfo -> UI
                        int iRowIndex = 0;
                        foreach (clsDIOCardSetup pSetup in mCardInfo.mLstDIOCardInfo)
                        {
                            pSetup.eSysCard = (clsMultiSystem.enuMotionCard)Enum.Parse(typeof(clsMultiSystem.enuMotionCard), dgvIOCard[dgvIOCard_CardType.Index, iRowIndex].Value.ToString());
                            pSetup.eDIOType = (clsDIOCardSetup.enuDIOType)Enum.Parse(typeof(clsDIOCardSetup.enuDIOType), dgvIOCard[dgvIOCard_SlaveType.Index, iRowIndex].Value.ToString());
                            pSetup.iCardID = Convert.ToInt32(dgvIOCard[dgvIOCard_CardID.Index, iRowIndex].Value);
                            pSetup.iSlaveID = Convert.ToInt32(dgvIOCard[dgvIOCard_SlaveID.Index, iRowIndex].Value);
                            pSetup.eStartDi = Convert.ToInt32(dgvIOCard[dgvIOCard_StartDI.Index, iRowIndex].Value);
                            pSetup.eStartDo = Convert.ToInt32(dgvIOCard[dgvIOCard_StartDO.Index, iRowIndex].Value);
                            pSetup.bSimulate = Convert.ToBoolean(dgvIOCard[dgvIOCard_Simulate.Index, iRowIndex].Value);
                            iRowIndex++;
                            switch (pSetup.eSysCard)
                            {
                                case clsMultiSystem.enuMotionCard.L122:
                                case clsMultiSystem.enuMotionCard.ApsMasterTPM:
                                case clsMultiSystem.enuMotionCard.Card7856:
                                    if (pSetup.eDIOType == clsDIOCardSetup.enuDIOType.DI32DO32)
                                    {
                                        formMessageBox.Show("This card don't have DI32DO32.");
                                        dgvIOCard[dgvIOCard_SlaveType.Index, iRowIndex].Value = clsDIOCardSetup.enuDIOType.DI16DO16.ToString();
                                        pSetup.eDIOType = clsDIOCardSetup.enuDIOType.DI16DO16;
                                        return;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                if (tabControl1.SelectedTab == tPage_AIOCard)
                {
                    #region//AIO Card
                    {
                        #region//將Row行數調整成與mCardInfo.mLstAIOCardInfo.Count相等
                        if (mCardInfo.mLstAIOCardInfo.Count > dgvAIOCard.Rows.Count)
                        {
                            int RemoveRowCount = mCardInfo.mLstAIOCardInfo.Count - dgvAIOCard.Rows.Count;
                            for (int i = 0; i < RemoveRowCount; i++)
                            {
                                mCardInfo.mLstAIOCardInfo.RemoveAt(dgvAIOCard.Rows.Count - 1);
                            }
                        }
                        else if (mCardInfo.mLstAIOCardInfo.Count < dgvAIOCard.Rows.Count)
                        {
                            int AddRowCount = dgvAIOCard.Rows.Count - mCardInfo.mLstAIOCardInfo.Count;
                            for (int i = 0; i < AddRowCount; i++)
                            {
                                mCardInfo.mLstAIOCardInfo.Add(new clsAIOCardSetup());
                            }
                        }
                        #endregion

                        #region//資料更新 mCardInfo.mLstDIOCardInfo -> UI
                        int iRowIndex = 0;
                        foreach (clsAIOCardSetup pSetup in mCardInfo.mLstAIOCardInfo)
                        {
                            pSetup.eCardType = (clsAIOCardSetup.enuCardType)Enum.Parse(typeof(clsAIOCardSetup.enuCardType), dgvAIOCard[dgvIOCard_CardType.Index, iRowIndex].Value.ToString());
                            pSetup.iCardID = Convert.ToInt32(dgvAIOCard[dgvAIOCard_CardID.Index, iRowIndex].Value);
                            pSetup.iSlaveID = Convert.ToInt32(dgvAIOCard[dgvAIOCard_SlaveID.Index, iRowIndex].Value);
                            pSetup.eStartAi = Convert.ToInt32(dgvAIOCard[dgvAIOCard_StartDI.Index, iRowIndex].Value);
                            pSetup.eStartAo = Convert.ToInt32(dgvAIOCard[dgvAIOCard_StartDO.Index, iRowIndex].Value);
                            iRowIndex++;
                        }
                        #endregion
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region//===================== 以下為事件處理 (VisibleChanged, tabPageChanged, dgvEnableChanged) =====================

        private void ucCardSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                bIsEditing = false;
                mCardInfo.Load(mCardInfo.sINIPath);
                UpdateControls();
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bIsEditing = false;
            mCardInfo.Load(mCardInfo.sINIPath);
            UpdateControls();
        }

        private void dgvMotion_EnabledChanged(object sender, EventArgs e)
        {
            btnSave_MotionCardSetting.Enabled = dgvMotion.Enabled;
            btnCancel_MotionCardSetting.Enabled = dgvMotion.Enabled;
            btnAdd_MotionCard.Enabled = dgvMotion.Enabled;
            btnDelete_MotionCard.Enabled = dgvMotion.Enabled;
        }
        private void dgvIOCard_EnabledChanged(object sender, EventArgs e)
        {
            btnSave_IOCardSetting.Enabled = dgvMotion.Enabled;
            btnCancel_IOCardSetting.Enabled = dgvMotion.Enabled;
            btnAdd_IOCard.Enabled = dgvIOCard.Enabled;
            btnDelete_IOCard.Enabled = dgvIOCard.Enabled;

        }
        private void dgvAIOCard_EnabledChanged(object sender, EventArgs e)
        {
            btnSave_AIOCardSetting.Enabled = dgvMotion.Enabled;
            btnCancel_AIOCardSetting.Enabled = dgvMotion.Enabled;
            btnAdd_AIOCard.Enabled = dgvAIOCard.Enabled;
            btnDelete_AIOCard.Enabled = dgvAIOCard.Enabled;
        }
        #endregion

        #region//===================== 以下為事件處理 (Edit, Cancel, Save) =====================

        private void btnEdit_CardSetting_Click(object sender, EventArgs e)
        {
            bIsEditing = !bIsEditing;
            mCardInfo.Load(mCardInfo.sINIPath);
            UpdateControls();
        }
        private void btnCancel_CardSetting_Click(object sender, EventArgs e)
        {
            bIsEditing = false;
            mCardInfo.Load(mCardInfo.sINIPath);
            UpdateControls();
        }
        private void btnSave_CardSetting_Click(object sender, EventArgs e)
        {
            if (formMessageBox.Show("Save setting need to restart program.", "Save Card Setting", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConvertUIToData();
                bIsEditing = false;
                mCardInfo.Save(mCardInfo.sINIPath);
                UpdateControls();
                clsMultiSystem.SetSettingChangeFlag();
            }
        }

        #endregion

        #region//===================== 以下為事件處理 (Add, Delete) =====================

        private void btnAdd_MotionCard_Click(object sender, EventArgs e)
        {
            int iIndex = mCardInfo.mLstMotionCardInfo.Count;
            mCardInfo.mLstMotionCardInfo.Add(new clsMotionCardSetup());
            mCardInfo.mLstMotionCardInfo[iIndex].iSlaveID = iIndex + 1;
            UpdateControls();
        }
        private void btnDelete_MotionCard_Click(object sender, EventArgs e)
        {
            if (dgvMotion.SelectedCells.Count > 0)
            {
                int iSelectIndex = dgvMotion.SelectedCells[0].RowIndex;
                if (iSelectIndex >= 0 && iSelectIndex < mCardInfo.mLstMotionCardInfo.Count)
                {
                    if (formMessageBox.Show(clsLanguage.GetTranslation("Are you sure want to delete") + " No." + (iSelectIndex + 1), "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        mCardInfo.mLstMotionCardInfo.RemoveAt(iSelectIndex);
                        UpdateControls();
                    }
                }
            }
        }

        private void btnAdd_IOCard_Click(object sender, EventArgs e)
        {
            int iIndex = mCardInfo.mLstDIOCardInfo.Count;
            mCardInfo.mLstDIOCardInfo.Add(new clsDIOCardSetup());
            mCardInfo.mLstDIOCardInfo[iIndex].iSlaveID = iIndex + 1;
            UpdateControls();
        }
        private void btnDelete_IOCard_Click(object sender, EventArgs e)
        {
            if (dgvIOCard.SelectedCells.Count > 0)
            {
                int iSelectIndex = dgvIOCard.SelectedCells[0].RowIndex;
                if (iSelectIndex >= 0 && iSelectIndex < mCardInfo.mLstDIOCardInfo.Count)
                {
                    if (formMessageBox.Show(clsLanguage.GetTranslation("Are you sure want to delete") + " No." + (iSelectIndex + 1), "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        mCardInfo.mLstDIOCardInfo.RemoveAt(iSelectIndex);
                        UpdateControls();
                    }
                }
            }
        }

        private void btnAdd_AIOCard_Click(object sender, EventArgs e)
        {
            mCardInfo.mLstAIOCardInfo.Add(new clsAIOCardSetup());
            UpdateControls();
        }
        private void btnDelete_AIOCard_Click(object sender, EventArgs e)
        {
            if (dgvAIOCard.SelectedCells.Count > 0)
            {
                int iSelectIndex = dgvAIOCard.SelectedCells[0].RowIndex;
                if (iSelectIndex >= 0 && iSelectIndex < mCardInfo.mLstAIOCardInfo.Count)
                {
                    if (formMessageBox.Show(clsLanguage.GetTranslation("Are you sure want to delete") + " No." + (iSelectIndex + 1), "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        mCardInfo.mLstAIOCardInfo.RemoveAt(iSelectIndex);
                        UpdateControls();
                    }
                }
            }
        }

        #endregion

        #region//===================== 以下為事件處理 (Dgv Cell只能輸入數字) =====================

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

        private void dgvMotion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvSimulate.Index)
            {
                int iNo = dgvMotion.CurrentCell.RowIndex;
                if (iNo >= 0 && iNo < mCardInfo.mLstMotionCardInfo.Count)
                {
                    //if (mCardInfo.mLstMotionCardInfo[iNo].eMotionCard == clsMultiSystem.enuMotionCard.Card7856
                    //    || mCardInfo.mLstMotionCardInfo[iNo].eMotionCard == clsMultiSystem.enuMotionCard.EtherCatMasterAdv)
                    {
                        mCardInfo.mLstMotionCardInfo[iNo].bSimulate = !mCardInfo.mLstMotionCardInfo[iNo].bSimulate;
                    }
                    //else
                    //{
                    //    mCardInfo.mLstMotionCardInfo[iNo].bSimulate = false;
                    //    formMessageBox.Show("Only (\"Card_7856\" , \"EtherCatMasterAdv\")\r\n Can Set Simulate");
                    //}
                    UpdateControls();
                }
            }
        }
        private void dgvMotion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMotion.CurrentCell is DataGridViewButtonCell)
            {
                int iNo = e.RowIndex;// dgvMotion.CurrentCell.RowIndex;
                if (e.ColumnIndex == dgvMotion_Other.Index)
                {
                    #region//OtherSetting
                    if (iNo >= 0 && iNo < mCardInfo.mLstMotionCardInfo.Count)
                    {
                        if (mCardInfo.mLstMotionCardInfo[iNo].eMotionCard == clsMultiSystem.enuMotionCard.Etel
                            || mCardInfo.mLstMotionCardInfo[iNo].eMotionCard == clsMultiSystem.enuMotionCard.Etel4)
                        {
                            ucSetGantry_Etel.GetSingleton()._ShowFormDialog(mCardInfo.mLstMotionCardInfo[iNo].iETEL_SlaveNum, ref mCardInfo.mLstMotionCardInfo[iNo].LstGantryAxis);
                            UpdateControls();
                        }
                        else if (mCardInfo.mLstMotionCardInfo[iNo].eMotionCard == clsMultiSystem.enuMotionCard.Card7856)
                        {
                            if (mCardInfo.mLstMotionCardInfo[iNo].eStartAxis != null)
                            {
                                ucSetMotionCardPmt_PCI7856.GetSingleton()._ShowFormDialog((clsEnum.enuAxis)mCardInfo.mLstMotionCardInfo[iNo].eStartAxis, ref mCardInfo.mLstMotionCardInfo[iNo].mCartPmt_7856);
                                UpdateControls();
                            }
                        }
                        else if (mCardInfo.mLstMotionCardInfo[iNo].eMotionCard == clsMultiSystem.enuMotionCard.ApsMasterTPM)
                        {
                            if (mCardInfo.mLstMotionCardInfo[iNo].eStartAxis != null)
                            {
                                ucSetMotionCardPmt_TPM.GetSingleton()._ShowFormDialog((clsEnum.enuAxis)mCardInfo.mLstMotionCardInfo[iNo].eStartAxis, ref mCardInfo.mLstMotionCardInfo[iNo].mCartPmt_TPM);
                                UpdateControls();
                            }
                        }
                        else if (mCardInfo.mLstMotionCardInfo[iNo].eMotionCard == clsMultiSystem.enuMotionCard.EtherCatMasterAdv)
                        {
                            if (mCardInfo.mLstMotionCardInfo[iNo].eStartAxis != null)
                            {
                                ucSetMotionCardPmt_AdvEtherCAT.GetSingleton()._ShowFormDialog((clsEnum.enuAxis)mCardInfo.mLstMotionCardInfo[iNo].eStartAxis, ref mCardInfo.mLstMotionCardInfo[iNo].mCartPmt_AdvEtherCAT, mCardInfo.mLstMotionCardInfo[iNo].eSlaveType);
                                UpdateControls();
                            }
                        }
                    }
                    #endregion
                }
                else if (e.ColumnIndex == dgvMotion_AxisSetting.Index)
                {
                    ucSetAxisSetting.GetSingleton()._ShowFormDialog(ref mCardInfo.mLstMotionCardInfo[iNo].mLst_AxisSetting);

                }
            }
        }
        private void dgvMotion_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ConvertUIToData();
            UpdateControls();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            bAdvanceSetting = !bAdvanceSetting;
            UpdateControls();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bAdvanceSetting = !bAdvanceSetting;
            UpdateControls();
        }

    }
}

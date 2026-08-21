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
    public partial class ucSetMotionCardPmt_TPM : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        private List<clsMotionCardPmt_TPM> pListCardPmt = null;
        private int iAxisNum = 0;
        private clsEnum.enuAxis? eStartAxis = null;
        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucSetMotionCardPmt_TPM m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucSetMotionCardPmt_TPM GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucSetMotionCardPmt_TPM();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucSetMotionCardPmt_TPM()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }

            dgvHomeMode.Items.Clear();
            foreach (clsMotionCardPmt_TPM.SynTek_HomeMode eHomeMode in Enum.GetValues(typeof(clsMotionCardPmt_TPM.SynTek_HomeMode)))
            {
                dgvHomeMode.Items.Add(eHomeMode.ToString());
            }
            dgvCmdPulse.Items.Clear();
            foreach (clsMotionCardPmt_TPM.SynTek_OutMode eCmdType in Enum.GetValues(typeof(clsMotionCardPmt_TPM.SynTek_OutMode)))
            {
                dgvCmdPulse.Items.Add(eCmdType.ToString());
            }

            dgvELMode.Items.Clear();
            foreach (clsMotionCardPmt_TPM.SynTek_ELMode eELMode in Enum.GetValues(typeof(clsMotionCardPmt_TPM.SynTek_ELMode)))
            {
                dgvELMode.Items.Add(eELMode.ToString());
            }
            dgvEncoderPulse.Items.Clear();
            foreach (clsMotionCardPmt_TPM.SynTek_InputMode eEncoderPulse in Enum.GetValues(typeof(clsMotionCardPmt_TPM.SynTek_InputMode)))
            {
                dgvEncoderPulse.Items.Add(eEncoderPulse.ToString());
            }
            dgvFeedback_Src.Items.Clear();
            foreach (clsMotionCardPmt_TPM.SynTek_Feedback eFeedbackSRC in Enum.GetValues(typeof(clsMotionCardPmt_TPM.SynTek_Feedback)))
            {
                dgvFeedback_Src.Items.Add(eFeedbackSRC.ToString());
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

        public void _ShowFormDialog(clsEnum.enuAxis eStartAxisID, ref List<clsMotionCardPmt_TPM> p_CardPmt)
        {
            iAxisNum = 4;
            eStartAxis = eStartAxisID;
            pListCardPmt = p_CardPmt;
            for (int i = pListCardPmt.Count; i < 4; i++)
            { pListCardPmt.Add(new clsMotionCardPmt_TPM()); }
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
                if (dgvSetMotionCartPmt_TPM.Rows.Count > iAxisNum)
                {
                    int RemoveRowCount = dgvSetMotionCartPmt_TPM.Rows.Count - iAxisNum;
                    for (int i = 0; i < RemoveRowCount; i++)
                    {
                        dgvSetMotionCartPmt_TPM.Rows.RemoveAt(dgvSetMotionCartPmt_TPM.Rows.Count - 1);
                    }
                }
                else if (dgvSetMotionCartPmt_TPM.Rows.Count < iAxisNum)
                {
                    int AddRowCount = iAxisNum - dgvSetMotionCartPmt_TPM.Rows.Count;
                    for (int i = 0; i < AddRowCount; i++)
                    {
                        dgvSetMotionCartPmt_TPM.Rows.Add();
                    }
                }
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
                        #region//Card Pmt
                        dgvSetMotionCartPmt_TPM[dgvAxisID.Index, iRow].Value = ((int)(eStartAxis + iRow)).ToString();
                        dgvSetMotionCartPmt_TPM[dgvAxisEnum.Index, iRow].Value = ((clsEnum.enuAxis)(eStartAxis + iRow)).ToString();
                        foreach (clsMotionCardPmt_TPM.enuPmtName ePmt in Enum.GetValues(typeof(clsMotionCardPmt_TPM.enuPmtName)))
                        {
                            int iValue = pListCardPmt[iRow].mPmtValue[ePmt];
                            switch (ePmt)
                            {
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_SERVO_LOGIC:
                                    dgvSetMotionCartPmt_TPM[dgvLogic_ServoOn.Index, iRow].Value = iValue > 0;
                                    break;
                                //case clsMotionCardPmt_TPM.enuPmtName.PRA_RDY_LOGIC:
                                //    dgvSetMotionCartPmt_TPM[dgvLogic_Ready.Index, iRow].Value = iValue > 0;
                                //    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_INP_LOGIC:
                                    dgvSetMotionCartPmt_TPM[dgvLogic_INP.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_ALM_LOGIC:
                                    dgvSetMotionCartPmt_TPM[dgvLogic_ALM.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_EZ_LOGIC:
                                    dgvSetMotionCartPmt_TPM[dgvLogic_EZ.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_ORG_LOGIC:
                                    dgvSetMotionCartPmt_TPM[dgvLogic_Org.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_EL_LOGIC:
                                    dgvSetMotionCartPmt_TPM[dgvLogic_EL.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_EL_MODE:
                                    {
                                        clsMotionCardPmt_TPM.SynTek_ELMode eELMode = (clsMotionCardPmt_TPM.SynTek_ELMode)Enum.ToObject(typeof(clsMotionCardPmt_TPM.SynTek_ELMode), iValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_TPM.SynTek_ELMode), eELMode) == false)
                                        {
                                            eELMode = clsMotionCardPmt_TPM.SynTek_ELMode.Decelerates_Stop;
                                            pListCardPmt[iRow].mPmtValue[ePmt] = (int)eELMode;
                                        }
                                        dgvSetMotionCartPmt_TPM[dgvELMode.Index, iRow].Value = eELMode.ToString();
                                    }
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_PLS_OPT_MODE:
                                    {
                                        clsMotionCardPmt_TPM.SynTek_OutMode eCmdPulse = (clsMotionCardPmt_TPM.SynTek_OutMode)Enum.ToObject(typeof(clsMotionCardPmt_TPM.SynTek_OutMode), iValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_TPM.SynTek_OutMode), eCmdPulse) == false)
                                        {
                                            eCmdPulse = clsMotionCardPmt_TPM.SynTek_OutMode.CCW_CW_High;
                                            pListCardPmt[iRow].mPmtValue[ePmt] = (int)eCmdPulse;
                                        }
                                        dgvSetMotionCartPmt_TPM[dgvCmdPulse.Index, iRow].Value = eCmdPulse.ToString();
                                    }
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_PLS_IPT_MODE:
                                    {
                                        clsMotionCardPmt_TPM.SynTek_InputMode eEncoderPulse = (clsMotionCardPmt_TPM.SynTek_InputMode)Enum.ToObject(typeof(clsMotionCardPmt_TPM.SynTek_InputMode), iValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_TPM.SynTek_InputMode), eEncoderPulse) == false)
                                        {
                                            eEncoderPulse = clsMotionCardPmt_TPM.SynTek_InputMode.PhaseAB_X4;
                                            pListCardPmt[iRow].mPmtValue[ePmt] = (int)eEncoderPulse;
                                        }
                                        dgvSetMotionCartPmt_TPM[dgvEncoderPulse.Index, iRow].Value = eEncoderPulse.ToString();
                                    }
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_PLS_IPT_LOGIC:
                                    dgvSetMotionCartPmt_TPM[dgvEncoderDirLogic.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_FEEDBACK_SRC:
                                    {
                                        clsMotionCardPmt_TPM.SynTek_Feedback eFeedbackSrc = (clsMotionCardPmt_TPM.SynTek_Feedback)Enum.ToObject(typeof(clsMotionCardPmt_TPM.SynTek_Feedback), iValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_TPM.SynTek_Feedback), eFeedbackSrc) == false)
                                        {
                                            eFeedbackSrc = clsMotionCardPmt_TPM.SynTek_Feedback.External_Position_Counter;
                                            pListCardPmt[iRow].mPmtValue[ePmt] = (int)eFeedbackSrc;
                                        }
                                        dgvSetMotionCartPmt_TPM[dgvFeedback_Src.Index, iRow].Value = eFeedbackSrc.ToString();
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion

                        #region//Homming Pmt
                        dgvSetHommingPmt_TPM[dgvAxisID_Homming.Index, iRow].Value = ((int)(eStartAxis + iRow)).ToString();
                        dgvSetHommingPmt_TPM[dgvAxisEnum_Homming.Index, iRow].Value = ((clsEnum.enuAxis)(eStartAxis + iRow)).ToString();
                        foreach (clsMotionCardPmt_TPM.enuPmtName ePmt in Enum.GetValues(typeof(clsMotionCardPmt_TPM.enuPmtName)))
                        {
                            int iValue = pListCardPmt[iRow].mPmtValue[ePmt];
                            switch (ePmt)
                            {
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_HOME_MODE:
                                    {
                                        clsMotionCardPmt_TPM.SynTek_HomeMode eHomeMode = (clsMotionCardPmt_TPM.SynTek_HomeMode)Enum.ToObject(typeof(clsMotionCardPmt_TPM.SynTek_HomeMode), iValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_TPM.SynTek_HomeMode), eHomeMode) == false)
                                        {
                                            eHomeMode = clsMotionCardPmt_TPM.SynTek_HomeMode.SearchOrg;
                                            pListCardPmt[iRow].mPmtValue[ePmt] = (int)eHomeMode;
                                        }
                                        dgvSetHommingPmt_TPM[dgvHomeMode.Index, iRow].Value = eHomeMode.ToString();
                                    }
                                    break;
                                //case clsMotionCardPmt_TPM.enuPmtName.PRA_HOME_DIR:
                                //    dgvSetHommingPmt_TPM[dgvHomeDir.Index, iRow].Value = iValue > 0;
                                //    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_HOME_VM:
                                    dgvSetHommingPmt_TPM[dgvHomeVM.Index, iRow].Value = iValue;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_HOME_VO:
                                    dgvSetHommingPmt_TPM[dgvHomeVO.Index, iRow].Value = iValue;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_HOME_EZA:
                                    dgvSetHommingPmt_TPM[dgvHomeEZA.Index, iRow].Value = iValue;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_HOME_OFFSET:
                                    dgvSetHommingPmt_TPM[dgvHomeOffset.Index, iRow].Value = iValue;
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
                dgvSetMotionCartPmt_TPM.EndEdit();
                dgvSetMotionCartPmt_TPM.Refresh();
                dgvSetHommingPmt_TPM.EndEdit();
                dgvSetHommingPmt_TPM.Refresh();
                for (int iRow = 0; iRow < iAxisNum; iRow++)
                {
                    if (iRow < pListCardPmt.Count)
                    {
                        #region//Card Pmt
                        foreach (clsMotionCardPmt_TPM.enuPmtName ePmt in Enum.GetValues(typeof(clsMotionCardPmt_TPM.enuPmtName)))
                        {
                            switch (ePmt)
                            {
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_SERVO_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_TPM[dgvLogic_ServoOn.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                //case clsMotionCardPmt_TPM.enuPmtName.PRA_RDY_LOGIC:
                                //    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_TPM[dgvLogic_Ready.Index, iRow].Value) == true) ? 1 : 0;
                                //    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_INP_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_TPM[dgvLogic_INP.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_ALM_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_TPM[dgvLogic_ALM.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_EZ_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_TPM[dgvLogic_EZ.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_ORG_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_TPM[dgvLogic_Org.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_EL_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_TPM[dgvLogic_EL.Index, iRow].Value) == true) ? 1 : 0;
                                    break;

                                case clsMotionCardPmt_TPM.enuPmtName.PRA_EL_MODE:
                                    {
                                        if (dgvSetMotionCartPmt_TPM[dgvELMode.Index, iRow].Value == null)
                                        {
                                            dgvSetMotionCartPmt_TPM[dgvELMode.Index, iRow].Value = clsMotionCardPmt_TPM.SynTek_ELMode.Decelerates_Stop.ToString();
                                        }
                                        string sValue = dgvSetMotionCartPmt_TPM[dgvELMode.Index, iRow].Value.ToString();
                                        clsMotionCardPmt_TPM.SynTek_ELMode eELMode = (clsMotionCardPmt_TPM.SynTek_ELMode)Enum.Parse(typeof(clsMotionCardPmt_TPM.SynTek_ELMode), sValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_TPM.SynTek_ELMode), eELMode) == false)
                                        {
                                            eELMode = clsMotionCardPmt_TPM.SynTek_ELMode.Decelerates_Stop;
                                            dgvSetMotionCartPmt_TPM[dgvELMode.Index, iRow].Value = eELMode.ToString();
                                        }
                                        pListCardPmt[iRow].mPmtValue[ePmt] = (int)eELMode;
                                    }
                                    break;

                                case clsMotionCardPmt_TPM.enuPmtName.PRA_PLS_OPT_MODE:
                                    {
                                        if (dgvSetMotionCartPmt_TPM[dgvCmdPulse.Index, iRow].Value == null)
                                        {
                                            dgvSetMotionCartPmt_TPM[dgvCmdPulse.Index, iRow].Value = clsMotionCardPmt_TPM.SynTek_OutMode.CCW_CW_High.ToString();
                                        }
                                        string sValue = dgvSetMotionCartPmt_TPM[dgvCmdPulse.Index, iRow].Value.ToString();
                                        clsMotionCardPmt_TPM.SynTek_OutMode eCmdPulse = (clsMotionCardPmt_TPM.SynTek_OutMode)Enum.Parse(typeof(clsMotionCardPmt_TPM.SynTek_OutMode), sValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_TPM.SynTek_OutMode), eCmdPulse) == false)
                                        {
                                            eCmdPulse = clsMotionCardPmt_TPM.SynTek_OutMode.CCW_CW_High;
                                            dgvSetMotionCartPmt_TPM[dgvCmdPulse.Index, iRow].Value = eCmdPulse.ToString();
                                        }
                                        pListCardPmt[iRow].mPmtValue[ePmt] = (int)eCmdPulse;
                                    }
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_PLS_IPT_MODE:
                                    {
                                        if (dgvSetMotionCartPmt_TPM[dgvEncoderPulse.Index, iRow].Value == null)
                                        {
                                            dgvSetMotionCartPmt_TPM[dgvEncoderPulse.Index, iRow].Value = clsMotionCardPmt_TPM.SynTek_InputMode.PhaseAB_X4.ToString();
                                        }
                                        string sValue = dgvSetMotionCartPmt_TPM[dgvEncoderPulse.Index, iRow].Value.ToString();
                                        clsMotionCardPmt_TPM.SynTek_InputMode eEncoderPulse = (clsMotionCardPmt_TPM.SynTek_InputMode)Enum.Parse(typeof(clsMotionCardPmt_TPM.SynTek_InputMode), sValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_TPM.SynTek_InputMode), eEncoderPulse) == false)
                                        {
                                            eEncoderPulse = clsMotionCardPmt_TPM.SynTek_InputMode.PhaseAB_X4;
                                            dgvSetMotionCartPmt_TPM[dgvEncoderPulse.Index, iRow].Value = eEncoderPulse.ToString();
                                        }
                                        pListCardPmt[iRow].mPmtValue[ePmt] = (int)eEncoderPulse;
                                    }
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_PLS_IPT_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_TPM[dgvEncoderDirLogic.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_FEEDBACK_SRC:
                                    {
                                        if (dgvSetMotionCartPmt_TPM[dgvFeedback_Src.Index, iRow].Value == null)
                                        {
                                            dgvSetMotionCartPmt_TPM[dgvFeedback_Src.Index, iRow].Value = clsMotionCardPmt_TPM.SynTek_Feedback.External_Position_Counter.ToString();
                                        }
                                        string sValue = dgvSetMotionCartPmt_TPM[dgvFeedback_Src.Index, iRow].Value.ToString();
                                        clsMotionCardPmt_TPM.SynTek_Feedback eFeedbackSrc = (clsMotionCardPmt_TPM.SynTek_Feedback)Enum.Parse(typeof(clsMotionCardPmt_TPM.SynTek_Feedback), sValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_TPM.SynTek_Feedback), eFeedbackSrc) == false)
                                        {
                                            eFeedbackSrc = clsMotionCardPmt_TPM.SynTek_Feedback.External_Position_Counter;
                                            dgvSetMotionCartPmt_TPM[dgvFeedback_Src.Index, iRow].Value = eFeedbackSrc.ToString();
                                        }
                                        pListCardPmt[iRow].mPmtValue[ePmt] = (int)eFeedbackSrc;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion

                        #region//Homming Pmt
                        foreach (clsMotionCardPmt_TPM.enuPmtName ePmt in Enum.GetValues(typeof(clsMotionCardPmt_TPM.enuPmtName)))
                        {
                            switch (ePmt)
                            {
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_HOME_MODE:
                                    {
                                        if (dgvSetHommingPmt_TPM[dgvHomeMode.Index, iRow].Value == null)
                                        {
                                            dgvSetHommingPmt_TPM[dgvHomeMode.Index, iRow].Value = clsMotionCardPmt_TPM.SynTek_HomeMode.SearchOrg.ToString();
                                        }
                                        string sValue = dgvSetHommingPmt_TPM[dgvHomeMode.Index, iRow].Value.ToString();
                                        clsMotionCardPmt_TPM.SynTek_HomeMode eHomeMode = (clsMotionCardPmt_TPM.SynTek_HomeMode)Enum.Parse(typeof(clsMotionCardPmt_TPM.SynTek_HomeMode), sValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_TPM.SynTek_HomeMode), eHomeMode) == false)
                                        {
                                            eHomeMode = clsMotionCardPmt_TPM.SynTek_HomeMode.SearchOrg;
                                            dgvSetHommingPmt_TPM[dgvHomeMode.Index, iRow].Value = eHomeMode.ToString();
                                        }
                                        pListCardPmt[iRow].mPmtValue[ePmt] = (int)eHomeMode;
                                    }
                                    break;
                                //case clsMotionCardPmt_TPM.enuPmtName.PRA_HOME_DIR:
                                //    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetHommingPmt_TPM[dgvHomeDir.Index, iRow].Value) == true) ? 1 : 0;
                                //    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_HOME_VM:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = Convert.ToInt32(dgvSetHommingPmt_TPM[dgvHomeVM.Index, iRow].Value);
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_HOME_VO:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = Convert.ToInt32(dgvSetHommingPmt_TPM[dgvHomeVO.Index, iRow].Value);
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_HOME_EZA:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = Convert.ToInt32(dgvSetHommingPmt_TPM[dgvHomeEZA.Index, iRow].Value);
                                    break;
                                case clsMotionCardPmt_TPM.enuPmtName.PRA_HOME_OFFSET:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = Convert.ToInt32(dgvSetHommingPmt_TPM[dgvHomeOffset.Index, iRow].Value);
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

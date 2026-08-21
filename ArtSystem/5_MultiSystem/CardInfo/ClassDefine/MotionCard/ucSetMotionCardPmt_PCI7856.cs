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
    public partial class ucSetMotionCardPmt_PCI7856 : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        private List<clsMotionCardPmt_PCI7856> pListCardPmt = null;
        private int iAxisNum = 0;
        private clsEnum.enuAxis? eStartAxis = null;
        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucSetMotionCardPmt_PCI7856 m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucSetMotionCardPmt_PCI7856 GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucSetMotionCardPmt_PCI7856();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucSetMotionCardPmt_PCI7856()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }

            dgvHomeMode.Items.Clear();
            foreach(clsMotionCardPmt_PCI7856.enuHomeMode eHomeMode in Enum.GetValues(typeof(clsMotionCardPmt_PCI7856.enuHomeMode)))
            {
                dgvHomeMode.Items.Add(eHomeMode.ToString());
            }
            dgvCmdPulse.Items.Clear();
            foreach (clsMotionCardPmt_PCI7856.enuCmdPulse eCmdType in Enum.GetValues(typeof(clsMotionCardPmt_PCI7856.enuCmdPulse)))
            {
                dgvCmdPulse.Items.Add(eCmdType.ToString());
            }

            dgvELMode.Items.Clear();
            foreach (clsMotionCardPmt_PCI7856.enuELMode eELMode in Enum.GetValues(typeof(clsMotionCardPmt_PCI7856.enuELMode)))
            {
                dgvELMode.Items.Add(eELMode.ToString());
            }
            dgvEncoderPulse.Items.Clear();
            foreach (clsMotionCardPmt_PCI7856.enuEncoderPluse eEncoderPulse in Enum.GetValues(typeof(clsMotionCardPmt_PCI7856.enuEncoderPluse)))
            {
                dgvEncoderPulse.Items.Add(eEncoderPulse.ToString());
            }
            dgvFeedback_Src.Items.Clear();
            foreach (clsMotionCardPmt_PCI7856.enuFeedbackSrc eFeedbackSRC in Enum.GetValues(typeof(clsMotionCardPmt_PCI7856.enuFeedbackSrc)))
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

        public void _ShowFormDialog(clsEnum.enuAxis eStartAxisID, ref List<clsMotionCardPmt_PCI7856> p_CardPmt)
        {
            iAxisNum = 4;
            eStartAxis = eStartAxisID;
            pListCardPmt = p_CardPmt;
            for (int i = pListCardPmt.Count; i < 4; i++)
            { pListCardPmt.Add(new clsMotionCardPmt_PCI7856()); }
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
                if (dgvSetMotionCartPmt_PCI7856.Rows.Count > iAxisNum)
                {
                    int RemoveRowCount = dgvSetMotionCartPmt_PCI7856.Rows.Count - iAxisNum;
                    for (int i = 0; i < RemoveRowCount; i++)
                    {
                        dgvSetMotionCartPmt_PCI7856.Rows.RemoveAt(dgvSetMotionCartPmt_PCI7856.Rows.Count - 1);
                    }
                }
                else if (dgvSetMotionCartPmt_PCI7856.Rows.Count < iAxisNum)
                {
                    int AddRowCount = iAxisNum - dgvSetMotionCartPmt_PCI7856.Rows.Count;
                    for (int i = 0; i < AddRowCount; i++)
                    {
                        dgvSetMotionCartPmt_PCI7856.Rows.Add();
                    }
                }
                if (dgvSetHommingPmt_PCI7856.Rows.Count > iAxisNum)
                {
                    int RemoveRowCount = dgvSetHommingPmt_PCI7856.Rows.Count - iAxisNum;
                    for (int i = 0; i < RemoveRowCount; i++)
                    {
                        dgvSetHommingPmt_PCI7856.Rows.RemoveAt(dgvSetHommingPmt_PCI7856.Rows.Count - 1);
                    }
                }
                else if (dgvSetHommingPmt_PCI7856.Rows.Count < iAxisNum)
                {
                    int AddRowCount = iAxisNum - dgvSetHommingPmt_PCI7856.Rows.Count;
                    for (int i = 0; i < AddRowCount; i++)
                    {
                        dgvSetHommingPmt_PCI7856.Rows.Add();
                    }
                }
                #endregion
                for (int iRow = 0; iRow < iAxisNum; iRow++)
                {
                    if (iRow < pListCardPmt.Count)
                    {
                        #region//Card Pmt
                        dgvSetMotionCartPmt_PCI7856[dgvAxisID.Index, iRow].Value = ((int)(eStartAxis + iRow)).ToString();
                        dgvSetMotionCartPmt_PCI7856[dgvAxisEnum.Index, iRow].Value = ((clsEnum.enuAxis)(eStartAxis + iRow)).ToString();
                        foreach (clsMotionCardPmt_PCI7856.enuPmtName ePmt in Enum.GetValues(typeof(clsMotionCardPmt_PCI7856.enuPmtName)))
                        {
                            int iValue = pListCardPmt[iRow].mPmtValue[ePmt];
                            switch (ePmt)
                            {
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_SERVO_LOGIC:
                                    dgvSetMotionCartPmt_PCI7856[dgvLogic_ServoOn.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_RDY_LOGIC:
                                    dgvSetMotionCartPmt_PCI7856[dgvLogic_Ready.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_INP_LOGIC:
                                    dgvSetMotionCartPmt_PCI7856[dgvLogic_INP.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_ALM_LOGIC:
                                    dgvSetMotionCartPmt_PCI7856[dgvLogic_ALM.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_EZ_LOGIC:
                                    dgvSetMotionCartPmt_PCI7856[dgvLogic_EZ.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_ORG_LOGIC:
                                    dgvSetMotionCartPmt_PCI7856[dgvLogic_Org.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_EL_LOGIC:
                                    dgvSetMotionCartPmt_PCI7856[dgvLogic_EL.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_EL_MODE:
                                    {
                                        clsMotionCardPmt_PCI7856.enuELMode eELMode = (clsMotionCardPmt_PCI7856.enuELMode)Enum.ToObject(typeof(clsMotionCardPmt_PCI7856.enuELMode), iValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_PCI7856.enuELMode), eELMode) == false)
                                        {
                                            eELMode = clsMotionCardPmt_PCI7856.enuELMode.DecStop;
                                            pListCardPmt[iRow].mPmtValue[ePmt] = (int)eELMode;
                                        }
                                        dgvSetMotionCartPmt_PCI7856[dgvELMode.Index, iRow].Value = eELMode.ToString();
                                    }
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_PLS_OPT_MODE:
                                    {
                                        clsMotionCardPmt_PCI7856.enuCmdPulse eCmdPulse = (clsMotionCardPmt_PCI7856.enuCmdPulse)Enum.ToObject(typeof(clsMotionCardPmt_PCI7856.enuCmdPulse), iValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_PCI7856.enuCmdPulse), eCmdPulse) == false)
                                        {
                                            eCmdPulse = clsMotionCardPmt_PCI7856.enuCmdPulse.CW_CCW_AH;
                                            pListCardPmt[iRow].mPmtValue[ePmt] = (int)eCmdPulse;
                                        }
                                        dgvSetMotionCartPmt_PCI7856[dgvCmdPulse.Index, iRow].Value = eCmdPulse.ToString();
                                    }
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_PLS_IPT_MODE:
                                    {
                                        clsMotionCardPmt_PCI7856.enuEncoderPluse eEncoderPulse = (clsMotionCardPmt_PCI7856.enuEncoderPluse)Enum.ToObject(typeof(clsMotionCardPmt_PCI7856.enuEncoderPluse), iValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_PCI7856.enuEncoderPluse), eEncoderPulse) == false)
                                        {
                                            eEncoderPulse = clsMotionCardPmt_PCI7856.enuEncoderPluse.PhaseAB_X4;
                                            pListCardPmt[iRow].mPmtValue[ePmt] = (int)eEncoderPulse;
                                        }
                                        dgvSetMotionCartPmt_PCI7856[dgvEncoderPulse.Index, iRow].Value = eEncoderPulse.ToString();
                                    }
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_PLS_IPT_LOGIC:
                                    dgvSetMotionCartPmt_PCI7856[dgvEncoderDirLogic.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_FEEDBACK_SRC:
                                    {
                                        clsMotionCardPmt_PCI7856.enuFeedbackSrc eFeedbackSrc = (clsMotionCardPmt_PCI7856.enuFeedbackSrc)Enum.ToObject(typeof(clsMotionCardPmt_PCI7856.enuFeedbackSrc), iValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_PCI7856.enuFeedbackSrc), eFeedbackSrc) == false)
                                        {
                                            eFeedbackSrc = clsMotionCardPmt_PCI7856.enuFeedbackSrc.ServoEncoder;
                                            pListCardPmt[iRow].mPmtValue[ePmt] = (int)eFeedbackSrc;
                                        }
                                        dgvSetMotionCartPmt_PCI7856[dgvFeedback_Src.Index, iRow].Value = eFeedbackSrc.ToString();
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion

                        #region//Homming Pmt
                        dgvSetHommingPmt_PCI7856[dgvAxisID_Homming.Index, iRow].Value = ((int)(eStartAxis + iRow)).ToString();
                        dgvSetHommingPmt_PCI7856[dgvAxisEnum_Homming.Index, iRow].Value = ((clsEnum.enuAxis)(eStartAxis + iRow)).ToString();
                        foreach (clsMotionCardPmt_PCI7856.enuPmtName ePmt in Enum.GetValues(typeof(clsMotionCardPmt_PCI7856.enuPmtName)))
                        {
                            int iValue = pListCardPmt[iRow].mPmtValue[ePmt];
                            switch (ePmt)
                            {
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_HOME_MODE:
                                    {
                                        clsMotionCardPmt_PCI7856.enuHomeMode eHomeMode = (clsMotionCardPmt_PCI7856.enuHomeMode)Enum.ToObject(typeof(clsMotionCardPmt_PCI7856.enuHomeMode), iValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_PCI7856.enuHomeMode), eHomeMode) == false)
                                        {
                                            eHomeMode = clsMotionCardPmt_PCI7856.enuHomeMode.SearchOrg;
                                            pListCardPmt[iRow].mPmtValue[ePmt] = (int)eHomeMode;
                                        }
                                        dgvSetHommingPmt_PCI7856[dgvHomeMode.Index, iRow].Value = eHomeMode.ToString();
                                    }
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_HOME_DIR:
                                    dgvSetHommingPmt_PCI7856[dgvHomeDir.Index, iRow].Value = iValue > 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_HOME_VM:
                                    dgvSetHommingPmt_PCI7856[dgvHomeVM.Index, iRow].Value = iValue;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_HOME_VO:
                                    dgvSetHommingPmt_PCI7856[dgvHomeVO.Index, iRow].Value = iValue;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_HOME_EZA:
                                    dgvSetHommingPmt_PCI7856[dgvHomeEZA.Index, iRow].Value = iValue;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_HOME_OFFSET:
                                    dgvSetHommingPmt_PCI7856[dgvHomeOffset.Index, iRow].Value = iValue;
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
                dgvSetMotionCartPmt_PCI7856.EndEdit();
                dgvSetMotionCartPmt_PCI7856.Refresh();
                dgvSetHommingPmt_PCI7856.EndEdit();
                dgvSetHommingPmt_PCI7856.Refresh();
                for (int iRow = 0; iRow < iAxisNum; iRow++)
                {
                    if (iRow < pListCardPmt.Count)
                    {
                        #region//Card Pmt
                        foreach (clsMotionCardPmt_PCI7856.enuPmtName ePmt in Enum.GetValues(typeof(clsMotionCardPmt_PCI7856.enuPmtName)))
                        {
                            switch (ePmt)
                            {
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_SERVO_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_PCI7856[dgvLogic_ServoOn.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_RDY_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_PCI7856[dgvLogic_Ready.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_INP_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_PCI7856[dgvLogic_INP.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_ALM_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_PCI7856[dgvLogic_ALM.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_EZ_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_PCI7856[dgvLogic_EZ.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_ORG_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_PCI7856[dgvLogic_Org.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_EL_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_PCI7856[dgvLogic_EL.Index, iRow].Value) == true) ? 1 : 0;
                                    break;

                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_EL_MODE:
                                    {
                                        if (dgvSetMotionCartPmt_PCI7856[dgvELMode.Index, iRow].Value == null)
                                        {
                                            dgvSetMotionCartPmt_PCI7856[dgvELMode.Index, iRow].Value = clsMotionCardPmt_PCI7856.enuELMode.DecStop.ToString();
                                        }
                                        string sValue = dgvSetMotionCartPmt_PCI7856[dgvELMode.Index, iRow].Value.ToString();
                                        clsMotionCardPmt_PCI7856.enuELMode eELMode = (clsMotionCardPmt_PCI7856.enuELMode)Enum.Parse(typeof(clsMotionCardPmt_PCI7856.enuELMode), sValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_PCI7856.enuELMode), eELMode) == false)
                                        {
                                            eELMode = clsMotionCardPmt_PCI7856.enuELMode.DecStop;
                                            dgvSetMotionCartPmt_PCI7856[dgvELMode.Index, iRow].Value = eELMode.ToString();
                                        }
                                        pListCardPmt[iRow].mPmtValue[ePmt] = (int)eELMode;
                                    }
                                    break;

                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_PLS_OPT_MODE:
                                    {
                                        if (dgvSetMotionCartPmt_PCI7856[dgvCmdPulse.Index, iRow].Value == null)
                                        {
                                            dgvSetMotionCartPmt_PCI7856[dgvCmdPulse.Index, iRow].Value = clsMotionCardPmt_PCI7856.enuCmdPulse.CW_CCW_AH.ToString();
                                        }
                                        string sValue = dgvSetMotionCartPmt_PCI7856[dgvCmdPulse.Index, iRow].Value.ToString();
                                        clsMotionCardPmt_PCI7856.enuCmdPulse eCmdPulse = (clsMotionCardPmt_PCI7856.enuCmdPulse)Enum.Parse(typeof(clsMotionCardPmt_PCI7856.enuCmdPulse), sValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_PCI7856.enuCmdPulse), eCmdPulse) == false)
                                        {
                                            eCmdPulse = clsMotionCardPmt_PCI7856.enuCmdPulse.CW_CCW_AH;
                                            dgvSetMotionCartPmt_PCI7856[dgvCmdPulse.Index, iRow].Value = eCmdPulse.ToString();
                                        }
                                        pListCardPmt[iRow].mPmtValue[ePmt] = (int)eCmdPulse;
                                    }
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_PLS_IPT_MODE:
                                    {
                                        if (dgvSetMotionCartPmt_PCI7856[dgvEncoderPulse.Index, iRow].Value == null)
                                        {
                                            dgvSetMotionCartPmt_PCI7856[dgvEncoderPulse.Index, iRow].Value = clsMotionCardPmt_PCI7856.enuEncoderPluse.PhaseAB_X4.ToString();
                                        }
                                        string sValue = dgvSetMotionCartPmt_PCI7856[dgvEncoderPulse.Index, iRow].Value.ToString();
                                        clsMotionCardPmt_PCI7856.enuEncoderPluse eEncoderPulse = (clsMotionCardPmt_PCI7856.enuEncoderPluse)Enum.Parse(typeof(clsMotionCardPmt_PCI7856.enuEncoderPluse), sValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_PCI7856.enuEncoderPluse), eEncoderPulse) == false)
                                        {
                                            eEncoderPulse = clsMotionCardPmt_PCI7856.enuEncoderPluse.PhaseAB_X4;
                                            dgvSetMotionCartPmt_PCI7856[dgvEncoderPulse.Index, iRow].Value = eEncoderPulse.ToString();
                                        }
                                        pListCardPmt[iRow].mPmtValue[ePmt] = (int)eEncoderPulse;
                                    }
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_PLS_IPT_LOGIC:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetMotionCartPmt_PCI7856[dgvEncoderDirLogic.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_FEEDBACK_SRC:
                                    {
                                        if (dgvSetMotionCartPmt_PCI7856[dgvFeedback_Src.Index, iRow].Value == null)
                                        {
                                            dgvSetMotionCartPmt_PCI7856[dgvFeedback_Src.Index, iRow].Value = clsMotionCardPmt_PCI7856.enuFeedbackSrc.ServoEncoder.ToString();
                                        }
                                        string sValue = dgvSetMotionCartPmt_PCI7856[dgvFeedback_Src.Index, iRow].Value.ToString();
                                        clsMotionCardPmt_PCI7856.enuFeedbackSrc eFeedbackSrc = (clsMotionCardPmt_PCI7856.enuFeedbackSrc)Enum.Parse(typeof(clsMotionCardPmt_PCI7856.enuFeedbackSrc), sValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_PCI7856.enuFeedbackSrc), eFeedbackSrc) == false)
                                        {
                                            eFeedbackSrc = clsMotionCardPmt_PCI7856.enuFeedbackSrc.ServoEncoder;
                                            dgvSetMotionCartPmt_PCI7856[dgvFeedback_Src.Index, iRow].Value = eFeedbackSrc.ToString();
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
                        foreach (clsMotionCardPmt_PCI7856.enuPmtName ePmt in Enum.GetValues(typeof(clsMotionCardPmt_PCI7856.enuPmtName)))
                        {
                            switch (ePmt)
                            {
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_HOME_MODE:
                                    {
                                        if (dgvSetHommingPmt_PCI7856[dgvHomeMode.Index, iRow].Value == null)
                                        {
                                            dgvSetHommingPmt_PCI7856[dgvHomeMode.Index, iRow].Value = clsMotionCardPmt_PCI7856.enuHomeMode.SearchOrg.ToString();
                                        }
                                        string sValue = dgvSetHommingPmt_PCI7856[dgvHomeMode.Index, iRow].Value.ToString();
                                        clsMotionCardPmt_PCI7856.enuHomeMode eHomeMode = (clsMotionCardPmt_PCI7856.enuHomeMode)Enum.Parse(typeof(clsMotionCardPmt_PCI7856.enuHomeMode), sValue);
                                        if (Enum.IsDefined(typeof(clsMotionCardPmt_PCI7856.enuHomeMode), eHomeMode) == false)
                                        {
                                            eHomeMode = clsMotionCardPmt_PCI7856.enuHomeMode.SearchOrg;
                                            dgvSetHommingPmt_PCI7856[dgvHomeMode.Index, iRow].Value = eHomeMode.ToString();
                                        }
                                        pListCardPmt[iRow].mPmtValue[ePmt] = (int)eHomeMode;
                                    }
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_HOME_DIR:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = (Convert.ToBoolean(dgvSetHommingPmt_PCI7856[dgvHomeDir.Index, iRow].Value) == true) ? 1 : 0;
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_HOME_VM:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = Convert.ToInt32(dgvSetHommingPmt_PCI7856[dgvHomeVM.Index, iRow].Value);
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_HOME_VO:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = Convert.ToInt32(dgvSetHommingPmt_PCI7856[dgvHomeVO.Index, iRow].Value);
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_HOME_EZA:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = Convert.ToInt32(dgvSetHommingPmt_PCI7856[dgvHomeEZA.Index, iRow].Value);
                                    break;
                                case clsMotionCardPmt_PCI7856.enuPmtName.PRA_HOME_OFFSET:
                                    pListCardPmt[iRow].mPmtValue[ePmt] = Convert.ToInt32(dgvSetHommingPmt_PCI7856[dgvHomeOffset.Index, iRow].Value);
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

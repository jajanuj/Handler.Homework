using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtSystem.MultiSystem
{
    public class clsMotionCardPmt_TPM
    {
        #region//========== Enum 定義 ==========
        public enum enuPmtName
        {
            PRA_SERVO_LOGIC,//[SERVO output logic] 0:ActiveLow, 1:Active High
            //PRA_RDY_LOGIC,//[RDY input logic] 0: Active high , 1: Active low
            PRA_INP_Enable,//[INP Enable] 0: Disable , 1: Enable
            PRA_INP_LOGIC,//[INP input logic] 0: Active low , 1: Active high
            PRA_ALM_LOGIC,//[Set ALM Logic] 0:ActiveLow, 1:Active High
            PRA_EZ_LOGIC,//[Set EZ Logic] 0:Falling edge, 1:Rising edge
            PRA_ORG_LOGIC,//[ORG input logic] 0:ActiveLow, 1:Active High
            PRA_EL_LOGIC,//[PEL/MEL input logic] 0:Not Inverse, 1:Inverse
            PRA_EL_MODE, //[Mode of stop when end limit On] 0:Deceleration stop, 1:Stop immediately

            PRA_PLS_OPT_MODE,//[Pulse output mode] enuCmdPulse
            PRA_PLS_IPT_MODE,//[Pulse input mode] enuEncoderPluse
            PRA_PLS_IPT_LOGIC,//[Pulse input logic] 0: don not reverse counting direction , 1: reverse counting direction
            PRA_FEEDBACK_SRC,//[Select feedback source] ,  0: EXT. Encoder 1:Stepper Mode 2:ACServo Mode

            PRA_HOME_MODE,//
            PRA_HOME_VM,//[Homing maximum velocity] Unit: pulse/sec (回原點高速 10000 pps)
            PRA_HOME_VO,//[Homing leave home velocity] Unit: pulse/sec (脫離原點高速 152 pps)
            PRA_HOME_OFFSET,//[Homing leave home distance – Specify ORG offset] Unit: pulse (100)
            PRA_HOME_EZA,//[EZ count up value}

            //PRA_HOME_EZ_DIR,//?
            //PRA_LTC_DEST, 1); //0:Command Counter 1:Poition Counter
        }
        public enum SynTek_OutMode
        {
            DIR_Falling_High = 0,
            DIR_Rising_High = 1,
            DIR_Falling_Low = 2,
            DIR_Rising_Low = 3,
            CCW_CW_High = 4,
            AB_Phase = 5,
            BA_Phase = 6,
            CW_CCW_Low = 7,
        }
        public enum SynTek_HomeMode
        {
            SearchOrg = 1,
        }
        public enum SynTek_ELMode
        {
            Immediately_Stop = 0,
            Decelerates_Stop = 1,
        }
        public enum SynTek_InputMode
        {
            PhaseAB_X1 = 0,
            PhaseAB_X2 = 1,
            PhaseAB_X4 = 2,
            CW_CCW = 3,
        }
        public enum SynTek_Feedback
        {
            External_Position_Counter = 0,
            Command_Position_Counter = 1,
            //Command_Command_Counter = 2,
            //External_Command_Counter = 3,
        }
        public enum SynTek_Logic
        {
            Low = 0,
            High = 1
        }

        #endregion

        #region//========== 參數 ==========

        public Dictionary<enuPmtName, int> mPmtValue = new Dictionary<enuPmtName, int>();

        #endregion

        #region//========== 建構式 ==========

        public clsMotionCardPmt_TPM()
        {
            foreach (enuPmtName ePmt in Enum.GetValues(typeof(enuPmtName)))
            {
                if (mPmtValue.ContainsKey(ePmt) == false)
                {
                    mPmtValue.Add(ePmt, 0);
                    switch (ePmt)
                    {
                        case enuPmtName.PRA_EZ_LOGIC:
                        case enuPmtName.PRA_ALM_LOGIC:
                        case enuPmtName.PRA_EL_LOGIC:
                        case enuPmtName.PRA_EL_MODE:
                        case enuPmtName.PRA_HOME_MODE:
                            mPmtValue[ePmt] = 1;
                            break;
                        case enuPmtName.PRA_PLS_IPT_LOGIC:
                            mPmtValue[ePmt] = 0;
                            break;
                        case enuPmtName.PRA_HOME_OFFSET:
                            mPmtValue[ePmt] = 100;
                            break;
                        case enuPmtName.PRA_HOME_VM:
                            mPmtValue[ePmt] = 10000;
                            break;
                        case enuPmtName.PRA_HOME_VO:
                            mPmtValue[ePmt] = 1000;
                            break;
                        case enuPmtName.PRA_PLS_IPT_MODE:
                            mPmtValue[ePmt] = 2;
                            break;
                        case enuPmtName.PRA_PLS_OPT_MODE:
                            mPmtValue[ePmt] = (int)SynTek_OutMode.CCW_CW_High;
                            break;
                        case enuPmtName.PRA_SERVO_LOGIC:
                        case enuPmtName.PRA_INP_LOGIC:
                        case enuPmtName.PRA_ORG_LOGIC:
                            mPmtValue[ePmt] = 0;
                            break;
                        //case enuPmtName.PRA_HOME_EZA:
                        //case enuPmtName.PRA_HOME_EZ_DIR:
                            //break;
                        case enuPmtName.PRA_FEEDBACK_SRC:
                            mPmtValue[ePmt] = (int)SynTek_Feedback.External_Position_Counter;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        #endregion

        #region//========== Public函式 ==========
        static public bool SetupAxis(clsEnum.enuAxis eAxisID, clsMotionCardPmt_TPM pPmt)
        {
            bool rValue = false;
            try
            {
                int iErr = -1;
                clsAxisInfo AxisInfo = new clsAxisInfo();
                clsMotionCtrl.GetAxisInfo(eAxisID, ref AxisInfo);
                ushort m_iRingNum = 0;
                ushort m_iDipNum = (ushort)AxisInfo.NodeNum;
                ushort AxisNumOfCard = (ushort)AxisInfo.AxisNumOfCard;
                clsMotionDriverMnetTPM.IsHomeSearch = true;
                {
                    AxisInfo.HomeVelHigh = pPmt.mPmtValue[enuPmtName.PRA_HOME_VM];
                    AxisInfo.HomeVelLow = pPmt.mPmtValue[enuPmtName.PRA_HOME_VO];
                    AxisInfo.HomeAcc = 0.1;
                    AxisInfo.HomeOffset = pPmt.mPmtValue[enuPmtName.PRA_HOME_OFFSET];
                    SynTek_Logic OrgLogic = pPmt.mPmtValue[enuPmtName.PRA_ORG_LOGIC] == 0 ? SynTek_Logic.High : SynTek_Logic.Low;
                    SynTek_Logic EZLogic = pPmt.mPmtValue[enuPmtName.PRA_EZ_LOGIC] == 0 ? SynTek_Logic.High : SynTek_Logic.Low;
                    SynTek_Logic ELLogic = pPmt.mPmtValue[enuPmtName.PRA_EL_LOGIC] == 0 ? SynTek_Logic.High : SynTek_Logic.Low;
                    SynTek_ELMode ELMode = pPmt.mPmtValue[enuPmtName.PRA_EL_MODE] == 0 ? SynTek_ELMode.Immediately_Stop : SynTek_ELMode.Decelerates_Stop;
                    SynTek_Logic ALMLogic = pPmt.mPmtValue[enuPmtName.PRA_ALM_LOGIC] == 0 ? SynTek_Logic.High : SynTek_Logic.Low;
                    SynTek_Logic INPLogic = pPmt.mPmtValue[enuPmtName.PRA_INP_LOGIC] == 0 ? SynTek_Logic.High : SynTek_Logic.Low;

                    iErr = TPM.MNet.M204._mnet_m204_set_home_config(m_iRingNum, m_iDipNum, AxisNumOfCard, (ushort)pPmt.mPmtValue[enuPmtName.PRA_HOME_MODE], (ushort)OrgLogic, (ushort)EZLogic, (ushort)pPmt.mPmtValue[enuPmtName.PRA_HOME_EZA], 0);
                    iErr = TPM.MNet.M204._mnet_m204_set_el(m_iRingNum, m_iDipNum, AxisNumOfCard, (short)ELMode, (short)ELLogic);
                    iErr = TPM.MNet.M204._mnet_m204_set_pls_outmode(m_iRingNum, m_iDipNum, AxisNumOfCard, (ushort)pPmt.mPmtValue[enuPmtName.PRA_PLS_OPT_MODE]);
                    iErr = TPM.MNet.M204._mnet_m204_set_pls_iptmode(m_iRingNum, m_iDipNum, AxisNumOfCard, (ushort)pPmt.mPmtValue[enuPmtName.PRA_PLS_IPT_MODE], (ushort)pPmt.mPmtValue[enuPmtName.PRA_PLS_IPT_LOGIC]);
                    iErr = TPM.MNet.M204._mnet_m204_set_alm(m_iRingNum, m_iDipNum, AxisNumOfCard, (ushort)ALMLogic, (ushort)ELMode);
                    iErr = TPM.MNet.M204._mnet_m204_set_inp(m_iRingNum, m_iDipNum, AxisNumOfCard, (ushort)pPmt.mPmtValue[enuPmtName.PRA_INP_Enable], (ushort)INPLogic);
                    iErr = TPM.MNet.M204._mnet_m204_set_feedback_src(m_iRingNum, m_iDipNum, AxisNumOfCard, (ushort)pPmt.mPmtValue[enuPmtName.PRA_FEEDBACK_SRC]);
                    iErr = TPM.MNet.M204._mnet_m204_set_move_ratio(m_iRingNum, m_iDipNum, AxisNumOfCard, 1);
                    iErr = TPM.MNet.M204._mnet_m204_reset_command(m_iRingNum, m_iDipNum, AxisNumOfCard);
                    //iErr = TPM.MNet.M204._mnet_m204_reset_position(m_iRingNum, m_iDipNum, AxisNumOfCard);
                    iErr = TPM.MNet.M204._mnet_m204_reset_error_counter(m_iRingNum, m_iDipNum, AxisNumOfCard);
                    iErr = TPM.MNet.M204._mnet_m204_reset_position(m_iRingNum, m_iDipNum, AxisNumOfCard);
                }
                rValue = true;
            }
            catch
            {
            }
            return rValue;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtSystem.MultiSystem
{
    public class clsMotionCardPmt_PCI7856
    {
        #region//========== Enum 定義 ==========
        public enum enuPmtName
        {
            PRA_SERVO_LOGIC,//[SERVO output logic] 0:ActiveLow, 1:Active High
            PRA_RDY_LOGIC,//[RDY input logic] 0: Active high , 1: Active low
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
            PRA_HOME_DIR,//[Homing direction] 0: positive direction , 1: negative direction
            PRA_HOME_VM,//[Homing maximum velocity] Unit: pulse/sec (回原點高速 10000 pps)
            PRA_HOME_VO,//[Homing leave home velocity] Unit: pulse/sec (脫離原點高速 152 pps)
            PRA_HOME_EZA,//[EZ count up value}
            PRA_HOME_OFFSET,//[Homing leave home distance – Specify ORG offset] Unit: pulse (100)

            PRA_HOME_EZ_DIR,//?
            //PRA_LTC_DEST, 1); //0:Command Counter 1:Poition Counter
        }
        public enum enuCmdPulse
        {
            Out_Dir_AL_H = 0,
            Out_Dir_AH_H = 1,
            Out_Dir_AL_L = 2,
            Out_Dir_AH_L = 3,
            CW_CCW_AH = 4,
            CW_CCW_AL = 5,
            AB_OutLeading = 6,
            AB_OutLagging = 7,
        }
        public enum enuHomeMode
        {
            /// <summary> 1:負極限方向找尋Home停止;碰到負極限往正極限方向移動，待脫離Home再回Home停止 </summary>
            SearchOrg = 1,
            /// <summary> 找尋脫離ORG後,EZ觸發即停 (比較慢) </summary>
            SearchOrg_EZStop = 4,
            /// <summary> 找尋脫離ORG後找尋EZ,然後慢速尋找負源點 (比較快) </summary>
            SearchOrg_SearchEZ = 11,
            /// <summary> 碰到負限後找EZ (EZ_Count) </summary>
            NEL_Search_EZ = 27,
        }
        public enum enuELMode
        {
            DecStop = 0,
            EmgStop = 1,
        }
        public enum enuEncoderPluse
        {
            PhaseAB_X1 = 0,
            PhaseAB_X2 = 1,
            PhaseAB_X4 = 2,
            CW_CCW = 3,
        }
        public enum enuFeedbackSrc
        {
            ExternalEncoder = 0,
            Command = 1,
            ServoEncoder = 2,
        }
        #endregion

        #region//========== 參數 ==========

        public Dictionary<enuPmtName, int> mPmtValue = new Dictionary<enuPmtName, int>();

        #endregion

        #region//========== 建構式 ==========

        public clsMotionCardPmt_PCI7856()
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
                        case enuPmtName.PRA_HOME_DIR:
                        case enuPmtName.PRA_RDY_LOGIC:
                        case enuPmtName.PRA_PLS_IPT_LOGIC:
                        case enuPmtName.PRA_HOME_EZA:
                            mPmtValue[ePmt] = 1;
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
                        case enuPmtName.PRA_FEEDBACK_SRC:
                            mPmtValue[ePmt] = 2;
                            break;
                        case enuPmtName.PRA_PLS_OPT_MODE:
                            mPmtValue[ePmt] = (int)enuCmdPulse.CW_CCW_AH;
                            break;
                        case enuPmtName.PRA_ORG_LOGIC:
                        case enuPmtName.PRA_SERVO_LOGIC:
                        case enuPmtName.PRA_INP_LOGIC:
                        //case enuPmtName.PRA_HOME_EZA:
                        //case enuPmtName.PRA_HOME_EZ_DIR:
                            break;

                        default:
                            break;
                    }
                }
            }
        }
        #endregion

        #region//========== Public函式 ==========
        static public bool SetupAxis(clsEnum.enuAxis eAxisID, clsMotionCardPmt_PCI7856 pPmt)
        {
            bool rValue = false;
            try
            {
                clsAxisInfo AxisInfo = new clsAxisInfo();
                clsMotionCtrl.GetAxisInfo(eAxisID, ref AxisInfo);
                drvAdlinkAps168.APS_set_axis_param(AxisInfo.MnetAxisNum, (int)APS_Define.PRA_ACC, 20000);
                drvAdlinkAps168.APS_set_axis_param(AxisInfo.MnetAxisNum, (int)APS_Define.PRA_DEC, 20000);
                foreach(enuPmtName ePmtName in Enum.GetValues(typeof(enuPmtName)))
                {
                    APS_Define eAPS_Define = (APS_Define)Enum.Parse(typeof(APS_Define), ePmtName.ToString());
                    if (Enum.IsDefined(typeof(APS_Define), eAPS_Define) == true)
                    {
                        int iValue = pPmt.mPmtValue[ePmtName];
                        if (ePmtName == enuPmtName.PRA_ORG_LOGIC)
                        {
                            if (iValue == 0)
                            { iValue = 1; }
                            else
                            { iValue = 0; }
                        }
                        int iDefine = (int)eAPS_Define;
                        drvAdlinkAps168.APS_set_axis_param(AxisInfo.MnetAxisNum, iDefine, iValue);
                    }
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

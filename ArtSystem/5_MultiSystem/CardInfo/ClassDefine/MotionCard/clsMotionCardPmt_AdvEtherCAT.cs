using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtSystem.MultiSystem
{
    public class clsMotionCardPmt_AdvEtherCAT
    {
        #region//========== Enum 定義 ==========
        public enum enuPmtName
        {
            PRA_HOME_MODE,//
            PRA_HOME_VM,//[Homing maximum velocity] Unit: pulse/sec (回原點高速 10000 pps)
            PRA_HOME_VO,//[Homing leave home velocity] Unit: pulse/sec (脫離原點高速 152 pps)
            PRA_HOME_ACC,//
        }
        public enum enuHomeMode
        {
            /// <summary> Cia402HomeMode17 </summary>
            SearchNEL_Esc = 117,
            /// <summary> MODE12_AbsSearchReFind </summary>
            SearchOrg = 11,
            /// <summary> MODE16_LmtSearchReFind_Ref </summary>
            NEL_Search_EZ = 15,
            /// <summary> MODE6_Lmt_Ref </summary>
            NELStop_EZ = 5,
            /// <summary> MODE2_Lmt </summary>
            NELStop = 1,
        }

        #endregion

        #region//========== 參數 ==========

        public Dictionary<enuPmtName, int> mPmtValue = new Dictionary<enuPmtName, int>();

        #endregion

        #region//========== 建構式 ==========

        public clsMotionCardPmt_AdvEtherCAT()
        {
            foreach (enuPmtName ePmt in Enum.GetValues(typeof(enuPmtName)))
            {
                if (mPmtValue.ContainsKey(ePmt) == false)
                {
                    mPmtValue.Add(ePmt, 0);
                    switch (ePmt)
                    {
                        case enuPmtName.PRA_HOME_MODE:
                            mPmtValue[ePmt] = (int)enuHomeMode.SearchOrg;
                            break;
                        case enuPmtName.PRA_HOME_VM:
                            mPmtValue[ePmt] = 10000;
                            break;
                        case enuPmtName.PRA_HOME_VO:
                            mPmtValue[ePmt] = 1000;
                            break;
                        case enuPmtName.PRA_HOME_ACC:
                            mPmtValue[ePmt] = 100000;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        #endregion

        #region//========== Public函式 ==========

        static public bool SetupAxis(clsEnum.enuAxis eAxisID, clsMotionCardPmt_AdvEtherCAT pPmt)
        {
            bool rValue = false;
            try
            {
                clsAxisInfo AxisInfo = new clsAxisInfo();
                clsMotionCtrl.GetAxisInfo(eAxisID, ref AxisInfo);
                ushort m_iDipNum = (ushort)AxisInfo.NodeNum;
                ushort AxisNumOfCard = (ushort)AxisInfo.AxisNumOfCard;
                //drvEtherCat_PCI1203.enuHomeMode eHomeMode = drvEtherCat_PCI1203.enuHomeMode.MODE16_LmtSearchReFind_Ref;
                clsMotionCtrl.SetHomeMode(eAxisID, (double)pPmt.mPmtValue[enuPmtName.PRA_HOME_MODE],
                    (double)pPmt.mPmtValue[enuPmtName.PRA_HOME_VM], (double)pPmt.mPmtValue[enuPmtName.PRA_HOME_VO],
                    (double)pPmt.mPmtValue[enuPmtName.PRA_HOME_ACC]);
                rValue = true;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        #endregion
    }
}

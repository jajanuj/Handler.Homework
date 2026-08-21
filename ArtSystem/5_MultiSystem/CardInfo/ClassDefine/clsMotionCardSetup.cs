using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using Newtonsoft.Json;

namespace ArtSystem.MultiSystem
{
    public class clsMotionCardSetup
    {
        public clsMultiSystem.enuMotionCard eMotionCard = clsMultiSystem.enuMotionCard.EtherCatMasterAdv;
        public enuSlaveType_Motion eSlaveType = enuSlaveType_Motion.Motion_1xAxis;
        public int iCardID = 0;
        public int iSlaveID = 0;
        public int iETEL_SlaveNum = 0;
        [JsonIgnore]
        public clsEnum.enuAxis? eStartAxis = null;
        public bool bSimulate = false;
        public List<int> LstGantryAxis = new List<int>();
        public List<clsMotionCardPmt_PCI7856> mCartPmt_7856 = new List<clsMotionCardPmt_PCI7856>();
        public List<clsMotionCardPmt_TPM> mCartPmt_TPM = new List<clsMotionCardPmt_TPM>();
        public List<clsMotionCardPmt_AdvEtherCAT> mCartPmt_AdvEtherCAT = new List<clsMotionCardPmt_AdvEtherCAT>();
        public List<clsAxisSetting> mLst_AxisSetting = new List<clsAxisSetting>();

        public int GetAxisNum()
        {
            int rValue = 1;
            try
            {
                switch (eMotionCard)
                {
                    case clsMultiSystem.enuMotionCard.ADVAN_1245:
                    case clsMultiSystem.enuMotionCard.GTS_4:
                    case clsMultiSystem.enuMotionCard.L122:
                    case clsMultiSystem.enuMotionCard.ApsMasterTPM:
                    case clsMultiSystem.enuMotionCard.Card7856:
                        rValue = 4;
                        break;
                    case clsMultiSystem.enuMotionCard.Etel:
                        rValue = 2 * iETEL_SlaveNum;
                        break;
                    case clsMultiSystem.enuMotionCard.Etel4:
                        rValue = 2 * iETEL_SlaveNum;
                        break;
                    case clsMultiSystem.enuMotionCard.EtherCatMasterAdv:
                        rValue = EtherCATMotionSlaveNum(eSlaveType);
                        break;
                    case clsMultiSystem.enuMotionCard.YaskawaMP3000:
                        rValue = 2 * iETEL_SlaveNum;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        static public int EtherCATMotionSlaveNum(enuSlaveType_Motion eSlaveType)
        {
            int rValue = 1;
            switch (eSlaveType)
            {
                case enuSlaveType_Motion.Motion_1xAxis:
                    rValue = 1;
                    break;
                case enuSlaveType_Motion.Motion_2xAxis:
                    rValue = 2;
                    break;
                case enuSlaveType_Motion.Motion_4xAxis:
                    rValue = 4;
                    break;
                case enuSlaveType_Motion.Motion_8xAxis:
                    rValue = 8;
                    break;
                default:
                    break;
            }
            return rValue;
        }
    }
}

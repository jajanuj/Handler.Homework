using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtSystem.MultiSystem
{
    public class clsDIOCardSetup
    {
        public clsMultiSystem.enuMotionCard eSysCard = clsMultiSystem.enuMotionCard.EtherCatMasterAdv;
        public enuDIOType eDIOType = enuDIOType.DI16DO16;
        public int iCardID = 0;
        public int iSlaveID = 0;
        public int eStartDi = -1;
        public int eStartDo = -1;
        public bool bSimulate = false;
        public enum enuDIOType
        {
            DI32,
            DO32,
            DI16DO16,
            DI32DO32,
        }
    }
}

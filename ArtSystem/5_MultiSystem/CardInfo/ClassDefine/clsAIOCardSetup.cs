using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtSystem.MultiSystem
{
    public class clsAIOCardSetup
    {
        public int iAICount = 8;//舊版本有使用(新版會自動解讀) - 參數保留有效(介面無法設定)
        public int iAOCount = 2;//舊版本有使用(新版會自動解讀) - 參數保留有效(介面無法設定)
        public int iCardID = 0;
        public int iSlaveID = 0;
        public int eStartAi = -1;
        public int eStartAo = -1;
        public enuCardType eCardType = enuCardType.PCI9112_2AO_8AI;
        public enum enuCardType
        {
            //PCI9112,//(原本是有搭配AIOCount，把它整合在CardType內就好)
            PCI9112_2AO_8AI,
            PCI9112_2AO_16AI,
            PCI1203_8AI2AO,
            PCI1203_8AI,
            PCI1203_4AO,
        }
    }
}

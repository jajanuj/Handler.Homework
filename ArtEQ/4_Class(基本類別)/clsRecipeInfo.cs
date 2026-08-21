using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtTeach;

namespace ArtEQ
{


    public class clsRecipeInfo : clsDataFunction
    {
        public bool bIsExist = false;

        public string sTeaID;
        public string sTeaType;
        public int iTeaVolume;
        public string sSugarVolume;
        public string sIceWeight;
        public string sToppingType;

        public void Clear()
        {
            bIsExist = false;

            sTeaID = null;
            sTeaType = null;
            iTeaVolume = 0;
            sSugarVolume = null;
            sIceWeight = null;
            sToppingType = null;
        }

        public void CopyTo(clsRecipeInfo p_Target)
        {
            if (p_Target == null)
                return;

            p_Target.bIsExist = this.bIsExist;

            p_Target.sTeaID = this.sTeaID;
            p_Target.sTeaType = this.sTeaType;
            p_Target.iTeaVolume = this.iTeaVolume;
            p_Target.sSugarVolume = this.sSugarVolume;
            p_Target.sIceWeight = this.sIceWeight;
            p_Target.sToppingType = this.sToppingType;
        }
    }
}

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
    public class clsAxisSetting
    {
        #region //=====================  區/全域變數設置 =====================

        /// <summary> 馬達Enum ID </summary>
        public clsEnum.enuAxis? p_enuAxis = null;

        /// <summary> 取得馬達名稱(帶翻譯) </summary>
        public string strAxisName
        {
            get
            {
                if (p_enuAxis != null)
                {
                    return clsLanguage.GetTranslation(p_enuAxis.ToString());
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary> 正常軸向 : X> ,Y^, Zv, Lifter^ </summary>
        public bool bLogic_MotionInvert = false;

        /// <summary> 絕對式光學尺 </summary>
        public bool bABSEncoder = false;

        public bool bDisableINP = false;

        public bool bSimulate = false;

        [JsonIgnore]
        /// <summary> 軸控系統 </summary>
        public clsMultiSystem.enuMotionCard? eMotionCard = null;

        #endregion

        #region //=====================  必要函式設置 =====================

        public clsAxisSetting()
        {
        }

        public clsAxisSetting(clsEnum.enuAxis pAxis, clsMultiSystem.enuMotionCard pSystem, clsAxisSetting p_AxisSetting, bool Simulate)
        {
            this.p_enuAxis = pAxis;
            this.eMotionCard = pSystem;
            this.bLogic_MotionInvert = p_AxisSetting.bLogic_MotionInvert;
            this.bSimulate = Simulate;
            this.bDisableINP = p_AxisSetting.bDisableINP;
            if (pSystem == clsMultiSystem.enuMotionCard.YaskawaMP3000)
            {
                this.bABSEncoder = true;
            }
            else
            {
                this.bABSEncoder = p_AxisSetting.bABSEncoder;
            }

        }

        public clsAxisSetting(clsAxisSetting　p_AxisSetting)
        {
            this.p_enuAxis = p_AxisSetting.p_enuAxis;
            this.eMotionCard = p_AxisSetting.eMotionCard;
            this.bLogic_MotionInvert = p_AxisSetting.bLogic_MotionInvert;
            this.bABSEncoder = p_AxisSetting.bABSEncoder;
            this.bSimulate = p_AxisSetting.bSimulate;
            this.bDisableINP = p_AxisSetting.bDisableINP;
        }

        #endregion


    }
}

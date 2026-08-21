using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtData;
using ArtCommonLib;
using ArtControlLib;
using Newtonsoft.Json;

namespace ArtSystem
{
    public class clsSignalTowerSetting
    {
        #region//===== Nnum定義 =====

        public enum enuSignalTower
        {
            LED_Red,
            LED_Green,//幾乎沒有使用
            LED_Blue,
            LED_Yellow,
            Bizzer1,
            Bizzer2,
        }

        #endregion


        #region//===== 參數(儲存) =====
        public Dictionary<enuSignalTower, clsEnum.enuDo?> g_DicSignalTower_DOID = new Dictionary<enuSignalTower, clsEnum.enuDo?>();
        #endregion

        #region//===== 建構式 =====
        public clsSignalTowerSetting()
        {
            g_DicSignalTower_DOID.Clear();
            foreach (enuSignalTower eSingalTower in Enum.GetValues(typeof(enuSignalTower)))
            {
                g_DicSignalTower_DOID.Add(eSingalTower, null);
            }
        }
        #endregion

        #region//===== Public Function =====
        public string GetDoName(enuSignalTower p_eSignalTower)
        {
            string rValue = "Null";
            try
            {
                if (g_DicSignalTower_DOID.ContainsKey(p_eSignalTower) == true)
                {
                    if (g_DicSignalTower_DOID[p_eSignalTower] != null)
                    {
                        rValue = g_DicSignalTower_DOID[p_eSignalTower].ToString();
                        var DoName = ArtSystem.MultiSystem.clsDioMotion.GetDoName();
                        if (DoName.ContainsKey((clsEnum.enuDo)g_DicSignalTower_DOID[p_eSignalTower]) == true)
                        {
                            rValue += " - " + DoName[(clsEnum.enuDo)g_DicSignalTower_DOID[p_eSignalTower]];
                        }
                    }
                }
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

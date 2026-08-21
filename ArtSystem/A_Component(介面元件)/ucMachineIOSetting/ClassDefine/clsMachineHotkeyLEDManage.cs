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

    public class clsMachineHotkeyLEDManage
    {
        public enum enuLED
        {
            Non,
            On,
            Off,
            Flash
        }
        public string g_sDescription = "Description";
        public enuLED g_eGreenLight = enuLED.Off;
        public enuLED g_eRedLight = enuLED.Off;
        public enuLED g_eBlueLight = enuLED.Off;
        public int g_iFlashSpeed_ms = 500;

        public void SetLEDGreen_NeedOutput(clsEnum.enuDo? p_eDOID, bool p_FlashFlag)
        {
            try
            {
                if (p_eDOID != null)
                {
                    clsEnum.enuDo eDO = (clsEnum.enuDo)p_eDOID;
                    switch (g_eGreenLight)
                    {
                        case enuLED.Non:
                            break;
                        case enuLED.On:
                            clsDioCtrl.SetDo(eDO, true);
                            break;
                        case enuLED.Off:
                            clsDioCtrl.SetDo(eDO, false);
                            break;
                        case enuLED.Flash:
                            clsDioCtrl.SetDo(eDO, p_FlashFlag);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void SetLEDRed_NeedOutput(clsEnum.enuDo? p_eDOID, bool p_FlashFlag)
        {
            try
            {
                if (p_eDOID != null)
                {
                    clsEnum.enuDo eDO = (clsEnum.enuDo)p_eDOID;
                    switch (g_eRedLight)
                    {
                        case enuLED.Non:
                            break;
                        case enuLED.On:
                            clsDioCtrl.SetDo(eDO, true);
                            break;
                        case enuLED.Off:
                            clsDioCtrl.SetDo(eDO, false);
                            break;
                        case enuLED.Flash:
                            clsDioCtrl.SetDo(eDO, p_FlashFlag);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void SetLEDBlue_NeedOutput(clsEnum.enuDo? p_eDOID, bool p_FlashFlag)
        {
            try
            {
                if (p_eDOID != null)
                {
                    clsEnum.enuDo eDO = (clsEnum.enuDo)p_eDOID;
                    switch (g_eBlueLight)
                    {
                        case enuLED.Non:
                            break;
                        case enuLED.On:
                            clsDioCtrl.SetDo(eDO, true);
                            break;
                        case enuLED.Off:
                            clsDioCtrl.SetDo(eDO, false);
                            break;
                        case enuLED.Flash:
                            clsDioCtrl.SetDo(eDO, p_FlashFlag);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
    }

}

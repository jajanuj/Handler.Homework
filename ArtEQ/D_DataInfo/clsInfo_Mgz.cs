using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtControlLib;
using ArtSystem;

namespace ArtEQ
{
    public class clsInfo_Mgz
    {
        public bool g_bIsExis {  get; set; }

        private string m_sID = "";
        /// <summary> Mgz的條碼 </summary> (1D,2D,RFID)
        public string g_sID
        {
            get
            {
                return m_sID;
            }
            set
            {
                m_sID = value;
            }
        }

        public DateTime g_CreateTime { get; set; }


        public List<clsInfo_Boat> g_LstBoatInfo = new List<clsInfo_Boat>();


        public clsInfo_Mgz()
        {
        }

        public void Create(bool p_bFull = false)
        {
            try
            {
                g_bIsExis = true;
                g_sID = "Mgz_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
                g_CreateTime = DateTime.Now;

                int iSlotNum = ucParameter.GetValueInt(ArtData.clsEnum.enuPmtName.Rec_MgzSlotNum);
                g_LstBoatInfo.Clear();
                for (int i = 0; i < iSlotNum; i++)
                {
                    g_LstBoatInfo.Add(new clsInfo_Boat());
                    if (p_bFull == false)
                    {
                        g_LstBoatInfo[i].Clear();
                    }
                    else
                    {
                        g_LstBoatInfo[i].Create();
                    }
                }
                //g_LstBoatInfo.RemoveRange(iSlotNum, g_LstBoatInfo.Count - iSlotNum);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        public void Clear()
        {
            try
            {
                int iSlotNum = ucParameter.GetValueInt(ArtData.clsEnum.enuPmtName.Rec_MgzSlotNum);
                g_LstBoatInfo.Clear();
                clsClassFunc.Clear(this); 
                for (int i = 0; i < iSlotNum; i++)
                {
                    g_LstBoatInfo.Add(new clsInfo_Boat());
                    g_LstBoatInfo[i].Clear();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);  
            }
        }

        public void CopyTo(clsInfo_Mgz p_Target)
        {
            try
            {
                clsClassFunc.Copy(this, p_Target);  
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        public void TransferTo(clsInfo_Mgz p_Target)
        {
            try
            {
                this.CopyTo(p_Target);
                this.Clear();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
    }
}

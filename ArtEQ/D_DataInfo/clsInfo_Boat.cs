using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtControlLib;
using ArtSystem;

namespace ArtEQ
{
    public class clsInfo_Boat
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

        public List<clsInfo_Unit> clsInfo_Units  = new List<clsInfo_Unit>();


        public clsInfo_Boat()
        {
        }



        public void Create(bool p_bFull = false)
        {
            try
            {
                g_bIsExis = true;
                g_sID = "Boat_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
                int iUnitCount = ucParameter.GetValueInt(ArtData.clsEnum.enuPmtName.Rec_BoatRowNum) * ucParameter.GetValueInt(ArtData.clsEnum.enuPmtName.Rec_BoatColumnNum);
                clsInfo_Units.Clear();
                for (int i = 0; i < iUnitCount; i++)
                {
                    clsInfo_Units.Add(new clsInfo_Unit());
                    if (p_bFull == false)
                    {
                        clsInfo_Units[i].Clear();
                    }
                    else
                    {
                        clsInfo_Units[i].Create();
                    }
                }
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
                clsInfo_Units.Clear();
                clsClassFunc.Clear(this);
                int iUnitCount = ucParameter.GetValueInt(ArtData.clsEnum.enuPmtName.Rec_BoatRowNum) * ucParameter.GetValueInt(ArtData.clsEnum.enuPmtName.Rec_BoatColumnNum);
                for (int i = 0; i < iUnitCount; i++)
                {
                    clsInfo_Units.Add(new clsInfo_Unit());
                    clsInfo_Units[i].Clear();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);  
            }
        }

        public void CopyTo(clsInfo_Boat p_Target)
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

        public void TransferTo(clsInfo_Boat p_Target)
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

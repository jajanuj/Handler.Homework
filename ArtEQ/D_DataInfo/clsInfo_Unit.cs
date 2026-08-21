using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ArtControlLib;
using ArtSystem;

namespace ArtEQ
{
    public class clsInfo_Unit
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

        public Point g_Location = new Point();

        public bool g_bIsAOIAlignDone = false;
        public bool g_bIsHighMeasureDone = false;
        public bool g_bIsDispensingDone = false;


        public clsInfo_Unit()
        {
            g_sID = "";
        }


        public void Create()
        {
            try
            {
                g_bIsExis = true;
                g_sID = "Unit_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
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
                clsClassFunc.Clear(this);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);  
            }
        }

        public void CopyTo(clsInfo_Unit p_Target)
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

        public void TransferTo(clsInfo_Unit p_Target)
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

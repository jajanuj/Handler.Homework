using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtControlLib;
using ArtSystem;

namespace ArtEQ
{
    public class clsDataInfo
    {
        static public clsInfo_Mgz MagazineArm = new clsInfo_Mgz();
        static public clsInfo_Mgz LoadPort = new clsInfo_Mgz();
        static public clsInfo_Mgz UnloadPort = new clsInfo_Mgz();
        //static public List<clsInfo_Mgz> UnloadBuffer = new List<clsInfo_Mgz>();

        static public clsInfo_Boat Roller1 = new clsInfo_Boat();
        static public clsInfo_Boat Roller2 = new clsInfo_Boat();
        static public clsInfo_Boat Roller3 = new clsInfo_Boat();

        static public clsInfo_Boat LaneChange = new clsInfo_Boat();
    }
}

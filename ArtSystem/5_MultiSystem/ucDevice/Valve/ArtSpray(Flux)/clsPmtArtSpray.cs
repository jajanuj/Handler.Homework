using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtSystem.Files;

namespace ArtSystem.MultiSystem
{
    /// <summary> 霧化閥 </summary>
    public class clsPmtArtSpray
    {
        /// <summary> 內霧化的延遲開啟時間 (Enum=27) </summary>
        public double OpInsideAtmTime = 0;

        /// <summary> 外霧化的延遲開啟時間 (Enum=28) </summary>
        public double OpOutsideAtmTime = 0;

        /// <summary> 供膠壓力的延遲開啟時間 (Enum=29) </summary>
        public double OpValvePreTime = 0;

        /// <summary> 閉鎖的的延遲開啟時間 (Enum=30) </summary>
        public double OpLockTime = 0;

        /// <summary> 內霧化的延遲關閉時間 (Enum=31) </summary>
        public double ClInsideAtmTime = 0;

        /// <summary> 外霧化的延遲關閉時間 (Enum=32) </summary>
        public double ClOutsideAtmTime = 0;

        /// <summary> 供膠壓力的延遲關閉時間 (Enum=33) </summary>
        public double ClValvePreTime = 0;

        /// <summary> 閉鎖的的延遲關閉時間 (Enum=34) </summary>
        public double ClLockTime = 0;
    }
}

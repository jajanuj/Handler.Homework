using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtSystem.Files;

namespace ArtSystem.MultiSystem
{
    /// <summary> 霧化閥 </summary>
    public class clsPmtArtPZT
    {
        /// <summary> 上抬量值 (Enum=10)</summary>
        public ushort Open_Volt = 20;
        /// <summary> 閉鎖量值 (Enum=11)</summary>
        public ushort Lock_Volt = 0;
        /// <summary> 開啟時間 (us) (Enum=12)</summary>
        public ushort Hold_Open_Time = 500;
        /// <summary> 閉鎖時間 (us) (Enum=13)</summary>
        public ushort Hold_Lock_Time = 500;
        /// <summary> 下衝時間 (us) (Enum=14)</summary>
        public ushort Lock_Time = 500;
        /// <summary> 上抬時間 (us) (Enum=15)</summary>
        public ushort Open_Time = 500;
    }
}

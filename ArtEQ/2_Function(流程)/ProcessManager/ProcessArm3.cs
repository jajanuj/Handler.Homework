using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtProcModuleLib;
using ArtModuleData;
using System.Drawing;

namespace ArtEQ
{
    public class Proc_ProcessArm2 : BaseProc_ProcessArm
    {

        #region //=====================  必要函式設置 =====================

        static private object objLock = new object();
        static private Proc_ProcessArm2 m_Singleton = null;
        static public Proc_ProcessArm2 GetSingleton()
        {
            lock (objLock)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new Proc_ProcessArm2("Proc_ProcessArm2");
                }
            }
            return m_Singleton;
        }

        public Proc_ProcessArm2(string p_strLogName)
            : base(p_strLogName)
        {

        }


        #endregion

    }
}

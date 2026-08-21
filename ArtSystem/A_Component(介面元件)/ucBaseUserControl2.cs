using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;

namespace ArtSystem
{
    public class ucBaseUserControl2 : ucBaseUserControl
    {
        public virtual void UpdateControls()
        {
        }
        public virtual void ReflashFunc()
        {
            try
            {
                this.ReflashTimerFunc();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex); clsArtSystem.CatchLog(ex);
            }
        }
    }
}

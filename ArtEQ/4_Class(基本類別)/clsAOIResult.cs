using ArtTeach;

namespace ArtEQ
{
    public class clsAOIResult : clsDataFunction
    {
        public bool bIsExist = false;

        public int iFillLevelResult;
        public string sInspectResult;
        public string sAppearanceResult;

        public void Clear()
        {
            bIsExist = false;

            iFillLevelResult = 0;
            sInspectResult = null;
            sAppearanceResult = null;
        }

        public void CopyTo(clsAOIResult p_Target)
        {
            if (p_Target == null)
                return;

            p_Target.bIsExist = this.bIsExist;

            p_Target.iFillLevelResult = this.iFillLevelResult;
            p_Target.sInspectResult = this.sInspectResult;
            p_Target.sAppearanceResult = this.sAppearanceResult;
        }
    }
}
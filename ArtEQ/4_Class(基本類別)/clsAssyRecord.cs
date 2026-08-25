using ArtTeach;
using static ArtData.clsEnum;

namespace ArtEQ._4_Class_基本類別_
{
    public class clsAssyRecord : clsDataFunction
    {
        public bool IsExist { get; set; }

        public bool IsAssembled { get; set; }

        public bool IsPressed { get; set; }

        public bool IsAoiInspected { get; set; }

        public AoiResult AoiResult { get; set; } = AoiResult.None;

        public WorkStationType CurrentStation { get; set; } = WorkStationType.Load;

        public void CopyTo(clsAssyRecord p_Target)
        {
            if (p_Target == null)
                return;

            p_Target.IsExist = IsExist;

            p_Target.IsAssembled = IsAssembled;
            p_Target.IsPressed = IsPressed;
            p_Target.IsAoiInspected = IsAoiInspected;
            p_Target.AoiResult = AoiResult;
            p_Target.CurrentStation = CurrentStation;

        }

        public clsAssyRecord Clone()
        {
            var target = new clsAssyRecord();
            CopyTo(target);
            return target;
        }

        public override string ToString()
        {
            return $"IsExist:{IsExist}, CurrentStation:{CurrentStation}, IsAssembled:{IsAssembled}, IsPressed:{IsPressed}, IsAoiInspected:{IsAoiInspected}";
        }
    }
}

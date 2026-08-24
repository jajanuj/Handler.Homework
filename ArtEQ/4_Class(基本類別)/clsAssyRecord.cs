using ArtTeach;
using static ArtData.clsEnum;

namespace ArtEQ._4_Class_基本類別_
{
    public class clsAssyRecord : clsDataFunction
    {
        public bool IsExist { get; set; }

        public bool IsAssembled { get; set; }

        public bool IsPressed { get; set; }

        /// <summary>
        /// 壓合站被設定關閉(EnablePressStation=false)時，這一格是被跳過壓合流程放行的，
        /// 不代表真的有物理壓合過。流程判斷(這一站算不算做完)要看 IsPressed || IsPressSkipped；
        /// 需要「真的壓合過」的語意(例如上報 Host)一律只看 IsPressed，不要被這個欄位污染。
        /// </summary>
        public bool IsPressSkipped { get; set; }

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
            p_Target.IsPressSkipped = IsPressSkipped;
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
            return $"IsExist:{IsExist}, CurrentStation:{CurrentStation}, IsAssembled:{IsAssembled}, IsPressed:{IsPressed}, IsPressSkipped:{IsPressSkipped}, IsAoiInspected:{IsAoiInspected}";
        }
    }
}

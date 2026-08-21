using ArtData;
using ArtEQ._2_Function_流程_.BaseProc;
using Axis = ArtData.clsEnum.enuAxis;

namespace ArtEQ._2_Function_流程_.Proc
{
    /// <summary>
    /// 組裝手臂
    /// </summary>
    public class Proc_AOI_Station : BaseAoiStation
    {
        public override BaseLane AOILane => Proc_AOI_Lane.GetSingleton();

        #region ===================== Singleton 設置 =====================

        public static Proc_AOI_Station GetSingleton() => GetSingletonInstance(() => new Proc_AOI_Station("AOI_Station"));

        #endregion

        protected Proc_AOI_Station(string p_strName) : base(p_strName)
        {
            BindHardwarePoint();
        }
        protected override void BindHardwarePoint()
        {
            m_Axis_X = Axis.AOI_Arm_X;
            m_Axis_Y = Axis.AOI_Arm_Y;
            m_Axis_Z = Axis.AOI_Arm_Z;

        }

        protected override bool SetTrayWork()
        {
            var tray = AOILane.m_Temp_Tray_Info;
            if (tray != null)
            {
                var index = tray.GetIndexFromRowCol(m_workRow, m_workColumn);
                var assyRecord = tray.AssyRecords[index];
                assyRecord.AoiResult = m_enuAoiResult;
                assyRecord.IsAoiInspected = true;
                assyRecord.CurrentStation = clsEnum.WorkStationType.AOI;
                var trayItemStatus = m_enuAoiResult == clsEnum.AoiResult.Ok ? clsEnum.TrayItemStatus.OK : clsEnum.TrayItemStatus.NG;
                tray.SetItemStatus(index, trayItemStatus);
            }
            return true;
        }
    }
}

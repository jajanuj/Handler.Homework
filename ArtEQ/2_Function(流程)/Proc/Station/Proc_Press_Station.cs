using ArtData;
using ArtEQ._2_Function_流程_.BaseProc;
using Di = ArtData.clsEnum.enuDi;
using Do = ArtData.clsEnum.enuDo;

namespace ArtEQ._2_Function_流程_.Proc
{
    /// <summary>
    /// 壓合站
    /// </summary>
    public class Proc_Press_Station : BasePressStation
    {
        #region ===================== Singleton 設置 =====================

        public static Proc_Press_Station GetSingleton() => GetSingletonInstance(() => new Proc_Press_Station("Press_Station"));

        #endregion

        public override BaseLane PressLane => Proc_Press_Lane.GetSingleton();

        protected Proc_Press_Station(string p_strName) : base(p_strName)
        {
            BindHardwarePoint();
        }

        protected override bool SetTrayWork()
        {
            var tray = PressLane.m_Temp_Tray_Info;
            for (int i = 0; i < tray.AssyRecords.Count; i++)
            {
                var assyRecord = tray.AssyRecords[i];
                if (!assyRecord.IsExist) continue;

                assyRecord.IsPressed = true;
                tray.SetItemStatus(i, clsEnum.TrayItemStatus.Pressed);
                assyRecord.CurrentStation = clsEnum.WorkStationType.Press;
            }
            return true;
        }

        protected override void BindHardwarePoint()
        {
            m_DI_Press_Fwd = Di.Press_Fwd;
            m_DI_Press_Bwd = Di.Press_Bwd;
            m_DI_Press_OverPress_B = Di.Press_Over_Press_B;

            m_DO_Press_Cylinder = Do.Press_Cylinder;
        }

    }
}

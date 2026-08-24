using ArtEQ._2_Function_流程_.BaseProc;
using static ArtData.clsEnum;
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

        /// <summary>
        /// 過帳：整盤有料的格子都推進到「壓合站已放行」。
        /// </summary>
        /// <param name="p_bPhysicallyPressed">
        /// true=氣缸真的有壓合過(case 20400)；false=壓合站被關閉、直接放行(case 21000)，
        /// 只推進流程，不能標記成真的壓合過。
        /// </param>
        protected override bool SetTrayWork(bool p_bPhysicallyPressed)
        {
            var tray = PressLane.m_Temp_Tray_Info;
            for (int i = 0; i < tray.AssyRecords.Count; i++)
            {
                var assyRecord = tray.AssyRecords[i];
                if (!assyRecord.IsExist) continue;

                assyRecord.IsPressed = p_bPhysicallyPressed;
                assyRecord.IsPressSkipped = !p_bPhysicallyPressed;
                TrayItemStatus itemStatus;
                if (p_bPhysicallyPressed)
                {
                    itemStatus = TrayItemStatus.Pressed;
                    tray.SetItemStatus(i, itemStatus);
                    assyRecord.CurrentStation = WorkStationType.Press;
                }
                else
                {
                    itemStatus = TrayItemStatus.Assembly;
                    tray.SetItemStatus(i, itemStatus);
                    assyRecord.CurrentStation = WorkStationType.ASM;
                }

                tray.SetItemStatus(i, itemStatus);
                assyRecord.CurrentStation = WorkStationType.ASM;
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

using ArtData;
using Di = ArtData.clsEnum.enuDi;
using Do = ArtData.clsEnum.enuDo;

namespace ArtEQ._2_Function_流程_.Proc
{
    public class Proc_Press_Lane : BaseLane
    {
        protected override bool UseStopper => true;

        public static Proc_Press_Lane GetSingleton() => GetSingletonInstance(() => new Proc_Press_Lane("Proc_Press_Lane"));

        public Proc_Press_Lane(string p_strName) : base(p_strName)
        {
        }

        public void SetArrivalSignal(bool setValue)
        {
            SetDi(Di.Press_Lane_Arrival_B, !setValue);
        }

        protected override void BindHardwarePoint()
        {
            m_DI_Load = Di.Press_Lane_Load_B;
            m_DI_Slow = Di.Press_Lane_Slow_B;
            m_DI_Arrival = Di.Press_Lane_Arrival_B;
            m_DI_Stopper_Extend = Di.Press_Lane_Stopper_Fwd;
            m_DI_Stopper_Retract = Di.Press_Lane_Stopper_Bwd;
            m_DO_Stopper = Do.Press_Lane_Stopper;
        }

        protected override BaseLane GetPreviousLaneForBill() => Proc_ASM_Lane.GetSingleton();

        protected override BaseLane GetNextLaneForBill() => Proc_AOI_Lane.GetSingleton();

        /// <summary>
        /// 出料前交握：確認下一站 AOI Lane 準備收料
        /// </summary>
        protected override bool ReadyToUnloadToNext()
        {
            Proc_AOI_Lane aoiLane = Proc_AOI_Lane.GetSingleton();
            if (aoiLane == null)
                return false;

            // 沒帳不能出料
            if (!HasTrayBill())
                return false;

            // 下游 Lane 已經有帳，不能再傳過去，跳出錯誤訊息
            if (aoiLane.HasTrayBill())
            {
                clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.Cannot_transfer_Downstream_Lane_Already_Has_Tray);

                return false;
            }

            // AOI Lane 必須處於 Load_Waiting 狀態，準備收料
            return aoiLane.m_enuAction == enuAction.Load_Waiting;
        }

        /// <summary>
        /// 等待下游 本站 Lane 完成收料
        /// </summary>
        protected override bool WaitNextLoadDone()
        {
            Proc_AOI_Lane aoiLane = Proc_AOI_Lane.GetSingleton();
            if (aoiLane == null)
                return false;

            return aoiLane.m_enuAction == enuAction.Load_Done;
        }

        // WaitPreviousDoneLoad()：Lane→Lane 交握不需要覆寫，上游會在自己的 Unload 流程(case 60500)
        // 主動把帳轉過來、清自己的帳，下游不需要回頭確認。吃 BaseLane 的預設值 (return true) 即可。
        // 2026-08-22：原本這裡覆寫成輪詢上游 m_enuAction，導致 Press_Lane 卡在 case 50500
        // （上游轉瞬即逝的狀態，Press_Lane 自己的 sensor 流程比較慢，等到要檢查時上游已經跑到下一輪）。
        // 詳見 LESSONS.md L8、AR_PROC_ARCHITECTURE.md「Lane→Lane 交握」一節。

        protected override bool ReadyToLoad()
        {
            var ASM_Lane = GetPreviousLaneForBill();
            if (ASM_Lane == null)
                return false;

            return ASM_Lane.m_enuAction == enuAction.Unload_Waiting;
        }
    }
}

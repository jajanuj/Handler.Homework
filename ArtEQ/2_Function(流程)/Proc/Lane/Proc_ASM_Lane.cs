using ArtData;
using Di = ArtData.clsEnum.enuDi;
using Do = ArtData.clsEnum.enuDo;

namespace ArtEQ._2_Function_流程_.Proc
{
    public class Proc_ASM_Lane : BaseLane
    {
        protected override bool UseStopper => true;

        public static Proc_ASM_Lane GetSingleton() => GetSingletonInstance(() => new Proc_ASM_Lane("Proc_ASM_Lane"));

        public Proc_ASM_Lane(string p_strName) : base(p_strName)
        {
        }

        protected override void BindHardwarePoint()
        {
            m_DI_Load = Di.ASM_Lane_Load_B;
            m_DI_Slow = Di.ASM_Lane_Slow_B;
            m_DI_Arrival = Di.ASM_Lane_Arrival_B;
            m_DI_Stopper_Extend = Di.ASM_Lane_Stopper_Fwd;
            m_DI_Stopper_Retract = Di.ASM_Lane_Stopper_Bwd;

            m_DO_Stopper = Do.ASM_Lane_Stopper;
        }

        protected override BaseMagazine GetPreviousMagazineForBill()
        {
            return Proc_IC_Feed_Magazine.GetSingleton();
        }

        protected override BaseLane GetNextLaneForBill()
        {
            return Proc_Press_Lane.GetSingleton();
        }

        /// <summary>
        /// 出料前交握：確認下一站 Press Lane 準備收料
        /// </summary>
        protected override bool ReadyToUnloadToNext()
        {
            Proc_Press_Lane pressLane = Proc_Press_Lane.GetSingleton();
            if (pressLane == null)
                return false;

            // 沒帳不能出料
            if (!HasTrayBill())
                return false;

            // 下游 Lane 已經有帳，不能再傳過去，跳出錯誤訊息
            if (pressLane.HasTrayBill())
            {
                clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.Cannot_transfer_Downstream_Lane_Already_Has_Tray);

                return false;
            }

            // Press Lane 必須處於 Load_Waiting 狀態，準備收料
            return pressLane.m_enuAction == enuAction.Load_Waiting;
        }

        /// <summary>
        /// 等待下游 Press Lane 完成收料
        /// </summary>
        protected override bool WaitNextLoadDone()
        {
            Proc_Press_Lane pressLane = Proc_Press_Lane.GetSingleton();
            if (pressLane == null)
                return false;

            // 等待 Press Lane 完成 Loading
            return pressLane.m_enuAction == enuAction.Loading;
        }

        protected override bool WaitPreviousDoneLoad()
        {
            var IC_Feed_Magazine = GetPreviousMagazineForBill();
            if (IC_Feed_Magazine == null)
                return false;
            return IC_Feed_Magazine.m_enuAction == BaseMagazine.enuAction.Magazine_Transfer_Done;
        }

        protected override bool ReadyToLoad()
        {
            var IC_Magazine = GetPreviousMagazineForBill();
            if (IC_Magazine == null)
                return false;

            return IC_Magazine.m_enuAction == BaseMagazine.enuAction.Magazine_Load_Waiting;
        }

    }
}

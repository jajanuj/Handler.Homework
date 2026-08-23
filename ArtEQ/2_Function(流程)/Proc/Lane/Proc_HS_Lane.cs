using Di = ArtData.clsEnum.enuDi;
using Do = ArtData.clsEnum.enuDo;

namespace ArtEQ._2_Function_流程_.Proc
{
    public class Proc_HS_Lane : BaseLane
    {
        #region Constructors

        public Proc_HS_Lane(string p_strName) : base(p_strName)
        {
        }

        #endregion

        #region Properties

        protected override bool UseStopper => true;

        #endregion

        #region Public Methods

        public static Proc_HS_Lane GetSingleton() => GetSingletonInstance(() => new Proc_HS_Lane("Proc_HS_Lane"));

        #endregion

        #region Protected Methods

        protected override void BindHardwarePoint()
        {
            m_DI_Load = Di.HS_Lane_Load_B;
            m_DI_Slow = Di.HS_Lane_Slow_B;
            m_DI_Arrival = Di.HS_Lane_Arrival_B;
            m_DI_Stopper_Extend = Di.HS_Lane_Stopper_Fwd;
            m_DI_Stopper_Retract = Di.HS_Lane_Stopper_Bwd;

            m_DO_Stopper = Do.HS_Lane_Stopper;
        }

        protected override BaseMagazine GetPreviousMagazineForBill() => Proc_HS_Feed_Magazine.GetSingleton();

        /// <summary>
        /// 入料前交握：確認上游 HS Feed Magazine 準備推料。
        /// 照 Proc_ASM_Lane.cs 的樣板補上——原本沒覆寫，吃 BaseLane 預設值(永遠 return true)，
        /// 導致 Lane 自己的入料模擬(靠計時器跑完 50100~50999)完全不等磁盒實際有沒有推料，
        /// 兩邊狀態會脫鉤(Lane 顯示 Load_Done 時磁盒可能都還沒開始推)。
        /// </summary>
        protected override bool ReadyToLoad()
        {
            var HS_Magazine = GetPreviousMagazineForBill();
            if (HS_Magazine == null)
                return false;

            return HS_Magazine.m_enuAction == BaseMagazine.enuAction.Magazine_Load_Waiting;
        }

        /// <summary>
        /// 出料前交握：確認下一站 HS Magazine 準備收料
        /// </summary>
        protected override bool ReadyToUnloadToNext()
        {
            var hsMagazine = GetNextMagazineForBill();
            if (hsMagazine == null)
                return false;

            // 沒帳不能出料
            if (!HasTrayBill())
                return false;

            // HS Magazine 必須處於 Magazine_Unload_Waiting 狀態，準備收料
            return hsMagazine.m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Waiting;
        }

        /// <summary>
        /// 等待下游 HS Magazine 完成收料
        /// </summary>
        protected override bool WaitNextLoadDone()
        {
            var hsDischargeMagazine = GetNextMagazineForBill();
            if (hsDischargeMagazine == null)
                return false;

            // 等待 HS Magazine 完成 Unload 動作，才算出料完成
            return hsDischargeMagazine.m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Done;
        }

        protected override BaseMagazine GetNextMagazineForBill() => Proc_HS_Discharge_Magazine.GetSingleton();

        #endregion
    }
}
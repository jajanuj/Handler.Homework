using Di = ArtData.clsEnum.enuDi;
using Do = ArtData.clsEnum.enuDo;

namespace ArtEQ._2_Function_流程_.Proc
{
    public class Proc_NG_Lane : BaseLane
    {
        #region Constructors

        public Proc_NG_Lane(string p_strName) : base(p_strName)
        {
        }

        #endregion

        #region Properties

        protected override bool UseStopper => true;

        #endregion

        #region Public Methods

        public static Proc_NG_Lane GetSingleton() => GetSingletonInstance(() => new Proc_NG_Lane("Proc_NG_Lane"));

        #endregion

        #region Protected Methods

        protected override void BindHardwarePoint()
        {
            m_DI_Load = Di.NG_Lane_Load_B;
            m_DI_Slow = Di.NG_Lane_Slow_B;
            m_DI_Arrival = Di.NG_Lane_Arrival_B;
            m_DI_Stopper_Extend = Di.NG_Lane_Stopper_Fwd;
            m_DI_Stopper_Retract = Di.NG_Lane_Stopper_Bwd;
            m_DO_Stopper = Do.NG_Lane_Stopper;
        }

        protected override BaseMagazine GetPreviousMagazineForBill() => Proc_NG_Feed_Magazine.GetSingleton();

        /// <summary>
        /// 出料前交握：確認下一站 NG Magazine 準備收料
        /// </summary>
        protected override bool ReadyToUnloadToNext()
        {
            var ngMagazine = GetNextMagazineForBill();
            if (ngMagazine == null)
                return false;

            // 沒帳不能出料
            if (!HasTrayBill())
                return false;

            // NG Magazine 必須處於 Magazine_Unload_Waiting 狀態，準備收料
            return ngMagazine.m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Waiting;
        }

        /// <summary>
        /// 等待下游 NG Magazine 完成收料
        /// </summary>
        protected override bool WaitNextLoadDone()
        {
            var ngDischargeMagazine = GetNextMagazineForBill();
            if (ngDischargeMagazine == null)
                return false;

            // 等待 NG Magazine 完成 Unload 動作，才算出料完成
            return ngDischargeMagazine.m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Done;
        }

        protected override BaseMagazine GetNextMagazineForBill() => Proc_NG_Discharge_Magazine.GetSingleton();

        #endregion
    }
}
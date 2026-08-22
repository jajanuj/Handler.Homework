using Di = ArtData.clsEnum.enuDi;
using Do = ArtData.clsEnum.enuDo;

namespace ArtEQ._2_Function_流程_.Proc
{
    public class Proc_OK_Lane : BaseLane
    {
        #region Constructors

        public Proc_OK_Lane(string p_strName) : base(p_strName)
        {
        }

        #endregion

        #region Properties

        protected override bool UseStopper => true;

        #endregion

        #region Public Methods

        public static Proc_OK_Lane GetSingleton() => GetSingletonInstance(() => new Proc_OK_Lane("Proc_OK_Lane"));

        #endregion

        #region Protected Methods

        protected override void BindHardwarePoint()
        {
            m_DI_Load = Di.OK_Lane_Load_B;
            m_DI_Slow = Di.OK_Lane_Slow_B;
            m_DI_Arrival = Di.OK_Lane_Arrival_B;
            m_DI_Stopper_Extend = Di.OK_Lane_Stopper_Fwd;
            m_DI_Stopper_Retract = Di.OK_Lane_Stopper_Bwd;
            m_DO_Stopper = Do.OK_Lane_Stopper;
        }

        protected override BaseLane GetPreviousLaneForBill() => Proc_AOI_Lane.GetSingleton();

        protected override BaseMagazine GetNextMagazineForBill() => Proc_OK_Discharge_Magazine.GetSingleton();

        /// <summary>
        /// 出料前交握：確認下一站 OK Magazine 準備收料
        /// </summary>
        protected override bool ReadyToUnloadToNext()
        {
            Proc_OK_Discharge_Magazine okMagazine = Proc_OK_Discharge_Magazine.GetSingleton();
            if (okMagazine == null)
                return false;

            // 沒帳不能出料
            if (!HasTrayBill())
                return false;

            // OK Magazine 必須處於 Load_Waiting 狀態，準備收料
            return okMagazine.m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Waiting;
        }

        /// <summary>
        /// 等待下游 OK Magazine 完成收料
        /// </summary>
        protected override bool WaitNextLoadDone()
        {
            Proc_OK_Discharge_Magazine okMagazine = Proc_OK_Discharge_Magazine.GetSingleton();
            if (okMagazine == null)
                return false;

            // 等待 OK Magazine 完成 Load_Done
            return okMagazine.m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Done;
        }

        // WaitPreviousDoneLoad()：Lane→Lane 交握不需要覆寫，吃 BaseLane 的預設值 (return true) 即可。
        // 見 LESSONS.md L8、AR_PROC_ARCHITECTURE.md「Lane→Lane 交握」一節。

        protected override bool ReadyToLoad()
        {
            var AOI_Lane = GetPreviousLaneForBill();
            if (AOI_Lane == null)
                return false;

            return AOI_Lane.m_enuAction == enuAction.Unload_Waiting;
        }

        #endregion
    }
}
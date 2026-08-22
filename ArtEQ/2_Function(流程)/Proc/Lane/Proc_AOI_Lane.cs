using ArtData;
using Di = ArtData.clsEnum.enuDi;
using Do = ArtData.clsEnum.enuDo;

namespace ArtEQ._2_Function_流程_.Proc
{
    public class Proc_AOI_Lane : BaseLane
    {
        #region Constructors

        public Proc_AOI_Lane(string p_strName) : base(p_strName)
        {
        }

        #endregion

        #region Properties

        protected override bool UseStopper => true;

        #endregion

        #region Public Methods

        public static Proc_AOI_Lane GetSingleton() => GetSingletonInstance(() => new Proc_AOI_Lane("Proc_AOI_Lane"));

        #endregion

        #region Protected Methods

        protected override void BindHardwarePoint()
        {
            m_DI_Load = Di.AOI_Lane_Load_B;
            m_DI_Slow = Di.AOI_Lane_Slow_B;
            m_DI_Arrival = Di.AOI_Lane_Arrival_B;
            m_DI_Stopper_Extend = Di.AOI_Lane_Stopper_Fwd;
            m_DI_Stopper_Retract = Di.AOI_Lane_Stopper_Bwd;
            m_DO_Stopper = Do.AOI_Lane_Stopper;
        }

        protected override BaseLane GetPreviousLaneForBill() => Proc_Press_Lane.GetSingleton();

        protected override BaseLane GetNextLaneForBill() => Proc_OK_Lane.GetSingleton();

        /// <summary>
        /// 出料前交握：確認下一站 OK Lane 準備收料
        /// </summary>
        protected override bool ReadyToUnloadToNext()
        {
            Proc_OK_Lane okLane = Proc_OK_Lane.GetSingleton();
            if (okLane == null)
                return false;

            // 沒帳不能出料
            if (!HasTrayBill())
                return false;

            // 下游 Lane 已經有帳，不能再傳過去，跳出錯誤訊息
            if (okLane.HasTrayBill())
            {
                clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.Cannot_transfer_Downstream_Lane_Already_Has_Tray);

                return false;
            }

            // OK Lane 必須處於 Load_Waiting 狀態，準備收料
            return okLane.m_enuAction == enuAction.Load_Waiting;
        }

        /// <summary>
        /// 等待下游 OK Lane 完成收料
        /// </summary>
        protected override bool WaitNextLoadDone()
        {
            Proc_OK_Lane okLane = Proc_OK_Lane.GetSingleton();
            if (okLane == null)
                return false;

            // 等待 OK Lane 完成 Load Done
            return okLane.m_enuAction == enuAction.Load_Done;
        }

        // WaitPreviousDoneLoad()：Lane→Lane 交握不需要覆寫，上游會在自己的 Unload 流程(case 60500)
        // 主動把帳轉過來、清自己的帳，下游不需要回頭確認。吃 BaseLane 的預設值 (return true) 即可。
        // 2026-08-22：Proc_Press_Lane.cs 原本用同樣的覆寫方式卡住過，這裡先一起拿掉，
        // 避免同一個病根之後在這條 Lane 上也發作。詳見 LESSONS.md L8。

        protected override bool ReadyToLoad()
        {
            var Press_Lane = GetPreviousLaneForBill();
            if (Press_Lane == null)
                return false;

            return Press_Lane.m_enuAction == enuAction.Unload_Waiting;
        }

        #endregion
    }
}
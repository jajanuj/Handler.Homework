using ArtData;

namespace ArtEQ._2_Function_流程_.Proc
{
    public class Proc_HS_Discharge_Magazine : BaseMagazine
    {
        #region Constructors

        #region ===================== 建構子 =====================

        public Proc_HS_Discharge_Magazine(string p_strName) : base(p_strName)
        {
            BindHardwarePoint();
        }

        #endregion

        #endregion

        #region Public Methods

        #region ===================== Singleton 設置 =====================

        public static Proc_HS_Discharge_Magazine GetSingleton() => GetSingletonInstance(() => new Proc_HS_Discharge_Magazine("HS_Discharge_Magazine"));

        #endregion

        #endregion

        #region ===================== 流程邏輯覆寫 =====================

        /// <summary>
        /// 判斷上游是否可以送料進 Magazine。
        /// </summary>
        protected override bool ReadyToLoad()
        {
            var hsLane = PreviousLane;
            if (hsLane == null)
                return false;

            return hsLane.m_enuAction == BaseLane.enuAction.Unload_Waiting_Sign;
        }

        /// <summary>
        /// 前一站的 Lane 是HSLane，因為 Magazine 是收料站。
        /// </summary>
        public override BaseLane PreviousLane => Proc_HS_Lane.GetSingleton();

        /// <summary>
        /// 確認收料完成。
        /// 目前沒有做收料流程，先回 true。
        /// </summary>
        protected override bool CheckLoadIsDone()
        {
            return true;
        }

        /// <summary>
        /// Base 的 30140 會呼叫 CheckStatus()。
        /// 如果你未來要做收料進 Magazine，這裡要改成真正的 DI 判斷。
        /// </summary>
        protected override bool CheckStatus()
        {
            return true;
        }

        /// <summary>
        /// 收料完成後，帳料從上游轉移進 Magazine。
        /// </summary>
        protected override void TransferBillAfterLoading()
        {
            // 1. Copy Lane 帳到 Magazine Slot
            // 2. 呼叫 Lane.ClearTrayBill() 清除 Lane 帳
            var okLane = PreviousLane;
            var upstreamTray = new clsTrayInfo();
            okLane.m_Temp_Tray_Info.CopyTo(upstreamTray);
            okLane.ClearTrayBill();
            m_MagazineInfo.m_trayInfo[m_iSlotNo] = upstreamTray;
        }

        /// <summary>
        /// 將實體硬體的 Axis/Pos/IO 綁定到本類別的欄位。
        /// 子類別需 override 並在此方法中指定。
        /// </summary>
        /// <remarks>
        /// - 此綁定通常於物件初始化時呼叫一次，供流程使用硬體點位。 
        /// - 實作時請同時考量模擬模式與實機模式，並避免在運行中頻繁變更綁定。
        /// </remarks>
        protected override void BindHardwarePoint()
        {
            //收料料槽不使用推桿
            UsePushCylinder = false;

            //DI
            m_DI_Magazine_Present = clsEnum.enuDi.HS_Discharge_Mag_Present;

            //Motion
            m_Motor_Z = clsEnum.enuAxis.HS_Discharge_Mag_Z;
        }

        #endregion
    }
}
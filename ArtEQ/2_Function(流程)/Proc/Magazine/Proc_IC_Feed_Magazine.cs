using ArtCommonLib;
using ArtData;

namespace ArtEQ._2_Function_流程_.Proc
{
    public class Proc_IC_Feed_Magazine : BaseMagazine
    {
        #region ===================== Singleton 設置 =====================

        public static Proc_IC_Feed_Magazine GetSingleton() => GetSingletonInstance(() => new Proc_IC_Feed_Magazine("IC_Feed_Magazine"));

        #endregion

        #region ===================== 建構子 =====================
        public Proc_IC_Feed_Magazine(string p_strName) : base(p_strName)
        {
            BindHardwarePoint();
        }
        #endregion

        #region ===================== 流程邏輯覆寫 =====================

        /// <summary>
        /// 判斷下游是否可以收料。
        /// 目前先回 true，方便你手動測試推桿流程。
        /// 正式版要改成 Proc_loadCup_lane 或下游站的交握判斷。
        /// </summary>
        protected override bool ReadyToUnload()
        {
            //todo: 這裡要改成下游站的交握判斷。
            var laneASM = Proc_ASM_Lane.GetSingleton();

            if (laneASM == null)
                return false;

            // 1. 先檢查帳：
            //    Slot 有帳、下游沒帳、沒有等待上一筆 ACK。
            if (!CanTransferBillToDownstream())
            {
                // 下游 Lane 已經有帳了，無法再推料。
                clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.Cannot_transfer_Downstream_Lane_Already_Has_Tray);
                return false;
            }

            // 2. 再檢查交握狀態：Lane 必須已經進入 Load_Waiting 或 Loading。
            return laneASM.m_enuAction == BaseLane.enuAction.Load_Waiting || laneASM.m_enuAction == BaseLane.enuAction.Loading;
        }

        /// <summary>
        /// 判斷上游是否可以送料進 Magazine。
        /// 如果你目前只做「Magazine 推料出去」，可以先回 false。
        /// </summary>
        protected override bool ReadyToLoad()
        {
            return false;
        }

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
        /// 推料完成後，帳料從 Magazine 轉移給下游。
        /// 目前先只寫 Log。
        /// </summary>
        protected override void TransferBillAfterUnloading()
        {
            //todo: 這裡要改成下游站的交握判斷。

            EnsureMagazineInfo();

            BaseLane lane = GetDownstreamLaneForBill();

            if (lane == null)
                return;

            if (!m_MagazineInfo.m_trayInfo.ContainsKey(m_iSlotNo))
                return;

            clsTrayInfo slotTray = m_MagazineInfo.m_trayInfo[m_iSlotNo];

            if (slotTray == null || !slotTray.bIsExist)
            {
                clsLog.Log(
                    clsEnum.enuLogName.ProcessLog.ToString(),
                    $"{strThreadLogName} : Slot[{m_iSlotNo}] no bill. Mechanism done only.");
                return;
            }

            if (lane.HasTrayBill())
            {
                clsLog.Log(
                    clsEnum.enuLogName.ProcessLog.ToString(),
                    $"{strThreadLogName} : Downstream already has bill. Transfer bill denied.");
                return;
            }

            // 1. Magazine Slot 帳 Copy 給 Lane
            lane.ReceiveTrayBillFromPrevious(slotTray);

            // 2. Magazine Slot 帳先保留，等待 Lane Load_Done ACK
            m_bWaitDownstreamLoadDone = true;
            m_iPendingClearSlotNo = m_iSlotNo;

            clsLog.Log(
                clsEnum.enuLogName.ProcessLog.ToString(),
                $"{strThreadLogName} : Copy Bill Slot[{m_iSlotNo}] Magazine -> Downstream. Wait Load_Done ACK.");
        }

        /// <summary>
        /// 收料完成後，帳料從上游轉移進 Magazine。
        /// 目前暫不處理。
        /// </summary>
        protected override void TransferBillAfterLoading()
        {
        }

        #region //===================== 帳料上下游設定 =====================

        protected override BaseLane GetDownstreamLaneForBill() => Proc_ASM_Lane.GetSingleton();

        #endregion

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
            //DI
            m_DI_Magazine_Present = clsEnum.enuDi.IC_Feed_Mag_Present;
            m_DI_OverPress_B = clsEnum.enuDi.IC_Feed_Mag_Over_Press_B;
            m_DI_OutPush_Extend = clsEnum.enuDi.IC_Feed_Mag_Push_Fwd;
            m_DI_OutPush_Retract = clsEnum.enuDi.IC_Feed_Mag_Push_Bwd;

            //DO
            m_DO_OutPush_Extend = clsEnum.enuDo.IC_Feed_Mag_Push;

            //Motion
            m_Motor_Z = clsEnum.enuAxis.IC_Feed_Mag_Z;
        }

        #endregion

        public void SetDiValue(clsEnum.enuDi di, bool bOn)
        {
            SetDi(di, bOn);
        }

    }
}

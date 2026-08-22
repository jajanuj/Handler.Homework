using System;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtEQ.B_Tools;
using static ArtData.clsEnum;

namespace ArtEQ
{
    public abstract class BaseLane : clsThreadProc
    {
        #region Enums

        #region //===================== Action 定義 =====================

        public enum enuAction
        {
            None,

            Initial = 10000,
            Initial_Done,
            Initial_Fail,

            /// <summary>
            /// 入料開始
            /// </summary>
            Load = 50000,

            /// <summary>
            /// 等待上游 Magazine / Lane 準備好送料
            /// </summary>
            Load_Waiting,

            /// <summary>
            /// 物料進入 Lane，Load 檢知 ON，Roller 高速運轉中
            /// </summary>
            Loading,
            Load_Waiting_Sign,
            Load_Done,
            Load_Fail,

            Unload = 60000,
            Unload_Waiting,
            Unloading,
            Unload_Waiting_Sign,
            Unload_Transfer_Done,
            Unload_Done,
            Unload_Fail,
        }

        #endregion

        #endregion

        #region Constructors

        #region //===================== Constructor =====================

        protected BaseLane(string p_strName) : base(p_strName)
        {
            m_enuAction = enuAction.None;
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>是否使用 Stopper。</summary>
        protected virtual bool UseStopper => false;

        /// <summary>是否使用 Align。</summary>
        protected virtual bool UseAlign => false;

        /// <summary>
        /// 是否使用Unload檢知
        /// </summary>
        protected virtual bool UseUnloadSensor => false;

        #endregion

        #region Protected Methods

        #region //===================== Scenario =====================

        protected override void Scenario()
        {
            switch (iStepIndex)
            {
                #region //===================== Initial (初始化流程 10000-10999) =====================

                case 10000:
                    // 【初始化開始】重新初始化 Lane 硬體設備
                    // - 建立 Roller 通訊物件
                    // - 初始化 Stopper / Align 氣缸
                    // - Roller 先停止
                    InitialLaneHardware(true);
                    m_Roller.SlowDownStop();
                    iStepIndex = 10100;
                    break;

                case 10100:
                    // 【Align 縮回-1】檢查是否使用 Align
                    // 若不使用，直接跳到 Stopper 流程
                    if (!UseAlign)
                    {
                        iStepIndex = 10200;
                        break;
                    }

                    // 加入 Align 縮回動作到控制盒
                    m_CtrlBox.Clear();
                    AddCylinder(m_Align, false); // false = 縮回
                    iStepIndex = 10110;
                    break;

                case 10110:
                    // 【Align 縮回-2】等待 Align 縮回完成
                    // 成功跳到 10200，失敗跳到 10998
                    m_CtrlBox.Action(ref iStepIndex, 10200, 10998);
                    break;

                case 10200:
                    // 【Stopper 縮回-1】檢查是否使用 Stopper
                    // 若不使用，初始化完成
                    if (!UseStopper)
                    {
                        iStepIndex = 10999;
                        break;
                    }

                    // 加入 Stopper 縮回動作到控制盒
                    m_CtrlBox.Clear();
                    AddCylinder(m_Stopper, false); // false = 縮回
                    iStepIndex = 10210;
                    break;

                case 10210:
                    // 【Stopper 縮回-2】等待 Stopper 縮回完成
                    // 成功跳到 10220，失敗跳到 10998
                    m_CtrlBox.Action(ref iStepIndex, 10220, 10998);
                    break;

                case 10220:
                    // 【初始化完成】設定狀態，結束流程
                    if (b_Simulation)
                    {
                        clsDioCtrl.SetDi(m_DI_Load, false);
                        clsDioCtrl.SetDi(m_DI_Slow, false);
                        clsDioCtrl.SetDi(m_DI_Arrival, false);
                    }

                    iStepIndex = 10999;
                    break;

                case 10998:
                    // 【初始化失敗】設定狀態，結束流程
                    m_enuAction = enuAction.Initial_Fail;
                    m_bIsReady = false;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                case 10999:
                    // 【 初始化完成】Lane 已就緒，可以開始 Load / Unload
                    m_enuAction = enuAction.Initial_Done;
                    m_bIsReady = true;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                #endregion

                #region //===================== Load 入料主流程 (50000-50999) =====================

                case 50000:
                    // 【Load 流程開始】
                    // 如果使用 Stopper，先伸出擋料
                    if (UseStopper)
                    {
                        m_CtrlBox.Clear();
                        AddCylinder(m_Stopper, true); // true = 伸出
                        iStepIndex = 50010;
                    }
                    else
                    {
                        // 不使用 Stopper，直接進入等待上游
                        iStepIndex = 50100;
                    }

                    break;

                case 50010:
                    // 【等待 Stopper 伸出】Stopper 伸出完成後才能接料
                    m_CtrlBox.Action(ref iStepIndex, 50100, 50998);
                    break;

                case 50100:
                    m_enuAction = enuAction.Load_Waiting;

                    // 檢查上游是否準備好
                    if (!ReadyToLoad())
                        break; // 尚未準備好，停在這裡

                    // 模擬模式：清除 Slow / Present Sensor
                    if (b_Simulation)
                    {
                        clsDioCtrl.SetDi(m_DI_Slow, false);
                        clsDioCtrl.SetDi(m_DI_Arrival, false);
                    }

                    // 啟動 Roller 高速正轉
                    m_Roller.SetAxisVel(MotorHighSpeed);
                    m_Roller.KeepMove(enuMoveDir.Positive);

                    iStepIndex = 50110;
                    break;

                case 50110:
                    // 【等待 Load Sensor ON】等待料進入 Lane 入口
                    // Load Sensor ON = 料已進入 Lane

                    // 模擬模式：自動設定 Load Sensor ON
                    if (b_Simulation && !clsDioCtrl.GetDi(m_DI_Load))
                    {
                        clsDioCtrl.SetDi(m_DI_Load, true);
                        Restart();
                    }

                    // 檢查 Load Sensor 是否 ON
                    if (clsDioCtrl.GetDi(m_DI_Load))
                    {
                        // 模擬模式：確保 Sensor 保持 ON 一段時間，讓 UI 可以看到
                        if (b_Simulation)
                        {
                            if (!IsTimeOut(SimSlowSensorHoldMs, clsCmData.enuSecUnit.MilliSec))
                            {
                                break;
                            }
                        }

                        iStepIndex = 50200; // Load Sensor ON，進入下一階段
                    }

                    break;

                case 50200:
                    m_enuAction = enuAction.Loading;
                    iStepIndex = 50210;
                    break;

                case 50210:
                    // 【等待 Slow Sensor ON】等待料進入減速區
                    // Slow Sensor ON = 料即將到達定位點，需要減速

                    // 模擬模式：自動設定 Slow Sensor ON，Load Sensor OFF
                    if (b_Simulation && !clsDioCtrl.GetDi(m_DI_Slow))
                    {
                        clsDioCtrl.SetDi(m_DI_Load, false);
                        clsDioCtrl.SetDi(m_DI_Slow, true);
                        Restart();
                    }

                    // 檢查 Slow Sensor 是否 ON
                    if (clsDioCtrl.GetDi(m_DI_Slow))
                    {
                        // 模擬模式：確保 Sensor 保持 ON 一段時間
                        if (b_Simulation)
                        {
                            if (!IsTimeOut(SimSlowSensorHoldMs, clsCmData.enuSecUnit.MilliSec))
                            {
                                break;
                            }
                        }

                        iStepIndex = 50300; // Slow Sensor ON，開始減速
                    }

                    break;

                case 50300:
                    // 【Roller 切換低速】料進入減速區，降低速度
                    m_Roller.SetAxisVel(MotorLowSpeed);
                    m_Roller.KeepMove(enuMoveDir.Positive);

                    iStepIndex = 50310;
                    break;

                case 50310:
                    // 【等待 Present Sensor ON】等待料到達定位點
                    // Present Sensor ON = 料已到達正確位置，可以停止

                    // 模擬模式：自動設定 Present Sensor ON，Slow Sensor OFF
                    if (b_Simulation && !clsDioCtrl.GetDi(m_DI_Arrival))
                    {
                        clsDioCtrl.SetDi(m_DI_Slow, false);
                        clsDioCtrl.SetDi(m_DI_Arrival, true);
                    }

                    // 檢查 Present Sensor 是否 ON
                    if (clsDioCtrl.GetDi(m_DI_Arrival))
                        iStepIndex = 50400; // Present Sensor ON，料已定位
                    break;

                case 50400:
                    // 【料已定位，停止 Roller】Present Sensor ON，停止輸送
                    // 準備 Align 伸出靠位

                    // 模擬模式：清除所有 Sensor 除了 Present
                    if (b_Simulation)
                    {
                        clsDioCtrl.SetDi(m_DI_Load, false);
                        clsDioCtrl.SetDi(m_DI_Slow, false);
                        clsDioCtrl.SetDi(m_DI_Arrival, true);
                    }

                    // Roller 減速停止
                    m_Roller.SlowDownStop();

                    // 如果使用 Align，伸出靠位
                    if (UseAlign)
                    {
                        m_CtrlBox.Clear();
                        AddCylinder(m_Align, true); // true = 伸出
                        iStepIndex = 50410;
                    }
                    else
                    {
                        // 不使用 Align，直接完成
                        iStepIndex = 50500;
                    }

                    break;

                case 50410:
                    // 【等待 Align 伸出】Align 靠位中
                    m_CtrlBox.Action(ref iStepIndex, 50500, 50998);
                    break;

                case 50500:
                    // 【Load 完成】入料流程完成
                    // 等待上游 Magazine/lane 完成推料/出料動作
                    if (!WaitPreviousDoneLoad())
                    {
                        break; // 上游尚未完成，等待
                    }

                    m_enuAction = enuAction.Load_Waiting_Sign;
                    iStepIndex = 50510;
                    break;
                case 50510:
                    // 通知上游 Magazine/Lane 已收到料，可以清除 Magazine/lane Slot 帳
                    NotifyPreviousLoadDone();
                    iStepIndex = 50999;
                    break;

                case 50998:
                    // 【Load 失敗】入料流程失敗
                    m_enuAction = enuAction.Load_Fail;
                    m_bIsReady = false;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                case 50999:

                    // 模擬模式：確保 Present Sensor 維持 ON
                    if (b_Simulation)
                    {
                        clsDioCtrl.SetDi(m_DI_Load, false);
                        clsDioCtrl.SetDi(m_DI_Slow, false);
                        clsDioCtrl.SetDi(m_DI_Arrival, true);
                    }

                    m_enuAction = enuAction.Load_Done;
                    m_bIsReady = true;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                #endregion

                #region //===================== Unload 出料主流程 (60000-60999) =====================

                case 60000:
                    // 【Unload 流程開始】
                    m_enuAction = enuAction.Unload;

                    // 如果使用 Align，先縮回
                    if (UseAlign)
                    {
                        m_CtrlBox.Clear();
                        AddCylinder(m_Align, false); // false = 縮回
                        iStepIndex = 60010;
                    }
                    else
                    {
                        // 不使用 Align，直接進入 Stopper 流程
                        iStepIndex = 60100;
                    }

                    break;

                case 60010:
                    // 【等待 Align 縮回】Align 縮回中
                    // 注意：這裡仍然是 Unload_Waiting，還沒開始送料
                    // 成功跳到 60100，失敗跳到 60998
                    m_CtrlBox.Action(ref iStepIndex, 60100, 60998);
                    break;

                case 60100:
                    // 如果使用 Stopper，縮回放行
                    if (UseStopper)
                    {
                        m_CtrlBox.Clear();
                        AddCylinder(m_Stopper, false); // false = 縮回
                        iStepIndex = 60110;
                    }
                    else
                    {
                        // 不使用 Stopper，直接進入等待下游
                        iStepIndex = 60200;
                    }

                    break;

                case 60110:
                    // 【等待 Stopper 縮回】Stopper 縮回中
                    m_CtrlBox.Action(ref iStepIndex, 60200, 60998);
                    break;

                case 60200:
                    // 【等待下游準備收料】Stopper 已縮回，等待下游 Magazine/lane 準備好
                    m_enuAction = enuAction.Unload_Waiting;

                    // 檢查下游是否準備好收料
                    if (!ReadyToUnloadToNext())
                        break; // 下游尚未準備好，停在這裡等待

                    iStepIndex = 60210;
                    break;

                case 60210:
                    // 下游準備好，開始送料
                    m_enuAction = enuAction.Unloading;

                    // 啟動 Roller 高速送料
                    m_Roller.SetAxisVel(MotorHighSpeed);
                    m_Roller.KeepMove(enuMoveDir.Positive);

                    iStepIndex = 60220;
                    break;
                case 60220:
                    // 模擬模式：清除 Present Sensor
                    if (b_Simulation)
                    {
                        clsDioCtrl.SetDi(m_DI_Arrival, false);
                    }

                    iStepIndex = 60230;
                    break;

                case 60230:
                    // 如果使用 Unload Sensor，等待 Sensor ON
                    if (UseUnloadSensor)
                    {
                        // 模擬模式：自動設定 Unload Sensor ON
                        if (b_Simulation && !clsDioCtrl.GetDi(m_DI_Unload))
                        {
                            clsDioCtrl.SetDi(m_DI_Unload, true);
                            m_dtUnloadSensorOnTime = DateTime.Now;
                        }

                        iStepIndex = 60300;
                    }
                    else
                    {
                        iStepIndex = 60400;
                    }

                    break;

                case 60300:
                    // 【等待 Unload Sensor ON】Roller 正在送料，等待料到出口
                    m_enuAction = enuAction.Unloading;

                    // 檢查 Unload Sensor 是否 ON
                    if (clsDioCtrl.GetDi(m_DI_Unload))
                    {
                        iStepIndex = 60400; // Unload Sensor ON，料已到出口
                    }

                    break;

                case 60400:
                    // 【料已到出口，停止 Roller】Unload Sensor ON，停止輸送
                    m_Roller.SlowDownStop();

                    // 模擬模式：清除 Unload Sensor（如果有使用）
                    if (b_Simulation && UseUnloadSensor)
                    {
                        clsDioCtrl.SetDi(m_DI_Unload, false);
                    }

                    iStepIndex = 60500;
                    break;

                case 60500:
                    // 【等待下游收料完成】等待下游 Magazine/Lane 收料並轉移帳料
                    m_enuAction = enuAction.Unload_Waiting_Sign;

                    // 檢查下游是否完成收料
                    if (!WaitNextLoadDone())
                        break; // 下游尚未完成，等待

                    // ========== 帳料轉移：只處理 Lane → Lane 情境 ==========
                    // Lane → Lane：由本站主動轉移帳
                    BaseLane nextLane = GetNextLaneForBill();
                    if (nextLane != null)
                    {
                        //ClearTrayBill();
                        TransferTrayBillToNextLane(); // 轉移帳料
                        ClearTrayBill();              // 清除本站帳料
                    }

                    // Lane → Magazine：Magazine 在 TransferBillAfterLoading() 中會：
                    // 1. Copy Lane 帳到 Magazine Slot
                    // 2. 呼叫 Lane.ClearTrayBill() 清除 Lane 帳
                    // 所以這裡不需要額外處理
                    m_enuAction = enuAction.Unload_Transfer_Done;

                    iStepIndex = 60999; // 下游完成，Unload 流程結束
                    break;

                case 60998:
                    // 【Unload 失敗】出料流程失敗
                    m_enuAction = enuAction.Unload_Fail;
                    m_bIsReady = false;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                case 60999:
                    // 【Unload 完成】出料流程正式完成
                    // 料已送到下游，帳已清除
                    m_enuAction = enuAction.Unload_Done;
                    m_Temp_Tray_Info.bTrayDone = false; // 清除 TrayDone 狀態，避免下一次 Load 直接被判定為 Done
                    m_bIsReady = true;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                    #endregion
            }
        }

        #endregion

        #endregion

        #region //===================== DI =====================

        // Load：本站入口 Sensor。上游把料送到本站入口後，這顆會 ON。
        // 在入口簽收版中，這顆是 CheckCanLoad() 的核心條件之一。
        protected clsEnum.enuDi m_DI_Load;

        // Slow：減速 Sensor。料進入定位前的減速區後 ON，用來切低速。
        protected clsEnum.enuDi m_DI_Slow;

        // Arrival：定位 Sensor。料到本站定位點後 ON。
        // 注意：Arrival ON 才代表料可被 ARM / 下一段流程取用。
        protected clsEnum.enuDi m_DI_Arrival;

        // Unload：本站出口 Sensor。料被本站送到下游入口後 ON。
        // 並不是每一條 Lane 都有，用 UseUnloadSensor 決定。
        protected clsEnum.enuDi m_DI_Unload;

        protected clsEnum.enuDi m_DI_Stopper_Extend;
        protected clsEnum.enuDi m_DI_Stopper_Retract;

        // 用UseAlignSensor決定
        protected clsEnum.enuDi m_DI_Align_Extend;
        protected clsEnum.enuDi m_DI_Align_Retract;

        public bool bReady { get; protected set; }

        public bool m_bIsReady
        {
            get { return bReady; }
            protected set { bReady = value; }
        }

        public bool IsProcOK() => !bIsProcessing && m_bIsReady;

        //duncan

        #endregion

        #region //===================== DO =====================

        protected clsEnum.enuDo m_DO_Stopper;
        protected clsEnum.enuDo m_DO_Align;

        #endregion

        #region //===================== Roller =====================

        // CV / Roller 控制物件。
        // Load / Unload 時透過 SetAxisVel()、KeepMove()、SlowDownStop() 控制流道馬達。
        protected clsMotionRoller m_Roller;

        // Roller 通訊 Port，由各 Proc 的 LaneInitial() 指定，例如 COM1 / COM2 / COM3。
        protected string m_strPortName = "";

        #endregion

        #region //===================== Cylinder =====================

        // 控制盒：把多個氣缸 / 軸控動作加進來後，用 Action() 等待完成。
        protected clsControlBox m_CtrlBox = new clsControlBox();

        // Stopper：擋料用。Load 時通常伸出擋住定位，Unload 時縮回放行。
        protected clsBoxCylinder m_Stopper = new clsBoxCylinder();

        // Align：靠位用。料到 Present 後伸出靠位；Unload 前先縮回避免卡料。
        protected clsBoxCylinder m_Align = new clsBoxCylinder();

        #endregion

        #region //===================== Parameter =====================

        /// <summary>
        /// 氣缸逾時時間 3000ms
        /// </summary>
        private uint CylinderTimeout => 3000;

        private int MotorHighSpeed
        {
            get { return ucParameter.GetValueInt(enuPmtName.Sys_LaneMotorHighSpeed); }
        }

        private int MotorLowSpeed
        {
            get { return ucParameter.GetValueInt(enuPmtName.Sys_LaneMotorLowSpeed); }
        }

        #endregion

        #region //===================== 狀態變數 =====================

        /// <summary>目前動作，AR / UI 透過此觀察流程狀態。</summary>
        public enuAction m_enuAction { get; protected set; }

        private System.DateTime m_dtUnloadSensorOnTime = DateTime.Now;

        private const int SimUnloadSensorHoldMs = 1000;

        /// <summary>虛擬模式下，Load / Slow Sensor 至少保持 ON 的時間，避免 UI Timer 看不到燈號。</summary>
        private const int SimSlowSensorHoldMs = 1000;

        /// <summary>
        /// 是否模擬
        /// </summary>
        /// <returns></returns>
        private bool b_Simulation => PublicDeclare.bIsSimulate;

        #region //===================== 帳料 =====================

        /// <summary>
        /// Lane 目前持有的 Tray 帳。
        /// Magazine 推料完成後，帳會先複製到這裡。
        /// </summary>
        public clsTrayInfo m_Temp_Tray_Info { get; set; } = new clsTrayInfo();

        /// <summary>
        /// 防止同一次 Load_Done 重複通知上一站清帳。
        /// </summary>
        private bool m_bNotifyPreviousLoadDone = false;

        #endregion

        #endregion

        #region //===================== Public Run Function =====================

        /// <summary>UI / AR 呼叫：初始化 / 復歸。</summary>
        public void RunInitial()
        {
            // Initial 時清除 Lane 帳
            ClearTrayBill();

            clsThreadProcManage.bIsStepProc = false; //(100號停止主要Flag)
            clsThreadProcManage.bStartStepRun = false;

            // 重新允許下一次 Load_Done 通知上一站清帳
            m_bNotifyPreviousLoadDone = false;

            m_enuAction = enuAction.Initial;
            m_bIsReady = false;
            bIsProcessing = true;
            iStepIndex = 10000;
        }

        /// <summary>UI / AR 呼叫：簡易 Load。</summary>
        public void RunLoad()
        {
            m_enuAction = enuAction.Load;
            m_bIsReady = false;
            bIsProcessing = true;
            iStepIndex = 50000;

            // 每次新的 Load 流程，都允許重新通知上一站一次
            m_bNotifyPreviousLoadDone = false;
        }

        /// <summary>UI / AR 呼叫：簡易 Unload。</summary>
        public void RunUnload()
        {
            clsThreadProcManage.bIsStepProc = false;
            m_enuAction = enuAction.Unload;
            m_bIsReady = false;
            bIsProcessing = true;
            iStepIndex = 60000;
        }

        public clsTrayInfo GetTrayBill()
        {
            if (!HasTrayBill())
                return null;

            return m_Temp_Tray_Info;
        }

        #endregion

        #region //===================== UI Sensor Signal =====================

        public bool LoadSignal => clsDioCtrl.GetDi(m_DI_Load);

        public bool SlowSignal => clsDioCtrl.GetDi(m_DI_Slow);

        public bool ArrivalSignal => clsDioCtrl.GetDi(m_DI_Arrival);

        public bool AlignExtendSignal
        {
            get
            {
                if (!UseAlign)
                    return false;

                return clsDioCtrl.GetDi(m_DI_Align_Extend);
            }
        }

        public bool AlignRetractSignal
        {
            get
            {
                if (!UseAlign)
                    return false;

                return clsDioCtrl.GetDi(m_DI_Align_Retract);
            }
        }

        /// <summary>給 UI 顯示：Stopper 伸出 Sensor。</summary>
        public bool StopperExtendSignal
        {
            get
            {
                if (!UseStopper)
                    return false;

                return clsDioCtrl.GetDi(m_DI_Stopper_Extend);
            }
        }

        /// <summary>給 UI 顯示：Stopper 縮回 Sensor。</summary>
        public bool StopperRetractSignal
        {
            get
            {
                if (!UseStopper)
                    return false;

                return clsDioCtrl.GetDi(m_DI_Stopper_Retract);
            }
        }

        public bool UnloadSignal
        {
            get
            {
                if (!UseUnloadSensor)
                    return false;

                return clsDioCtrl.GetDi(m_DI_Unload);
            }
        }

        public bool IsDownstreamReceiveCompleted { get; private set; }

        #endregion

        #region //===================== Protected Function =====================

        protected void ClearDownstreamReceiveCompletedFlag() => IsDownstreamReceiveCompleted = false;

        protected static T GetSingletonInstance<T>(Func<T> factory) where T : class => SingletonHelper<T>.GetOrCreate(factory);

        protected abstract void BindHardwarePoint();

        protected void AddCylinder(clsBoxCylinder p_Cylinder, bool p_bExtend)
        {
            // 統一氣缸動作設定。
            // p_bExtend = true  伸出
            // p_bExtend = false 縮回
            // 100 / 100 為前後 Delay，維持原本簡化寫法。
            p_Cylinder.SetActionValue(p_bExtend, false, 100, 100, 0);
            m_CtrlBox.Add(p_Cylinder);
        }

        protected virtual bool ReadyToLoad()
        {
            return true;
        }

        protected virtual bool WaitPreviousDoneLoad()
        {
            return true;
        }

        protected virtual bool ReadyToUnloadToNext()
        {
            return true;
        }

        protected virtual bool WaitNextLoadDone()
        {
            return true;
        }

        /// <summary>
        /// 初始化 Lane 硬體設備
        /// 此方法負責設定和初始化 Lane 的所有硬體元件，包括：
        /// 1. Roller (輸送帶馬達)
        /// 2. Stopper (擋料氣缸)
        /// 3. Align (靠位氣缸)
        /// </summary>
        /// <param name="p_bForceCreateRoller">是否強制重新建立 Roller 物件</param>
        protected void InitialLaneHardware(bool p_bForceCreateRoller)
        {
            // ========== 第一段：呼叫子類別自訂初始化 ==========
            // 用途：設定該 Lane 專屬的 DI/DO/Port 等參數
            // 例如：Proc_loadCup_lane 會在這裡指定 m_DI_Load、m_DI_Slow、m_strPortName 等
            BindHardwarePoint();

            // ========== 第二段：建立或確認 Roller 物件 ==========
            // Roller 是控制輸送帶馬達的通訊物件
            // 參數說明：
            // - p_bForceCreateRoller = true：強制重新建立 Roller （用於 Initial 流程）
            // - m_Roller == null：如果 Roller 尚未建立，也要建立
            // - m_strPortName：通訊 Port，例如 "COM1"、"COM2" 等
            // - 0：Roller ID （通常為 0 ）
            if (p_bForceCreateRoller || m_Roller == null)
                m_Roller = new clsMotionRoller(m_strPortName, 0);

            // ========== 第三段：初始化 Stopper 氣缸 ==========
            // Stopper 是擋料用的氣缸，用於 Load 時擋住料件定位
            // 只有當該 Lane 使用 Stopper 時才初始化
            if (UseStopper)
            {
                // 初始化 Stopper 氣缸參數：
                // 參數 1：m_DO_Stopper - 控制 Stopper 的 DO (Digital Output)
                // 參數 2：m_DI_Stopper_Retract - Stopper 縮回的 DI (Digital Input)
                // 參數 3：false - DI 邏輯反向 (false = 正邏輯，ON = 到位)
                // 參數 4：m_DI_Stopper_Extend - Stopper 伸出的 DI
                // 參數 5：false - DI 邏輯反向
                // 參數 6：CylinderTimeout - 氣缸動作超時時間
                // 參數 7：縮回超時的報警訊息 ID
                // 參數 8：伸出超時的報警訊息 ID
                m_Stopper.Initial(
                    m_DO_Stopper,                                                // DO: 控制 Stopper 動作
                    m_DI_Stopper_Retract, false,                                 // DI: 縮回到位感測器 (正邏輯)
                    m_DI_Stopper_Extend, false,                                  // DI: 伸出到位感測器 (正邏輯)
                    CylinderTimeout,                                             // 超時時間
                    enuAlarm.Lane_Stopper_Retract_Timeout.ToString("d"), // 縮回超時報警
                    enuAlarm.Lane_Stopper_Extend_Timeout.ToString("d")); // 伸出超時報警
            }

            // ========== 第四段：初始化 Align 氣缸 ==========
            // Align 是靠位用的氣缸，用於料件到達 Present 後靠位定位
            // 只有當該 Lane 使用 Align 時才初始化
            if (UseAlign)
            {
                // 初始化 Align 氣缸參數：
                // 參數結構與 Stopper 相同
                // 參數 1：m_DO_Align - 控制 Align 的 DO
                // 參數 2：m_DI_Align_Retract - Align 縮回的 DI
                // 參數 3：false - DI 邏輯反向
                // 參數 4：m_DI_Align_Extend - Align 伸出的 DI
                // 參數 5：false - DI 邏輯反向
                // 參數 6：CylinderTimeout - 氣缸動作超時時間 (3000ms)
                // 參數 7：縮回超時的報警訊息 ID
                // 參數 8：伸出超時的報警訊息 ID
                m_Align.Initial(
                    m_DO_Align,                                                // DO: 控制 Align 動作
                    m_DI_Align_Retract, false,                                 // DI: 縮回到位感測器 (正邏輯)
                    m_DI_Align_Extend, false,                                  // DI: 伸出到位感測器 (正邏輯)
                    CylinderTimeout,                                           // 超時時間: 3000ms
                    enuAlarm.Lane_Align_Retract_Timeout.ToString("d"), // 縮回超時報警 
                    enuAlarm.Lane_Align_Extend_Timeout.ToString("d")); // 伸出超時報警
            }
        }

        /// <summary>
        /// 單動流程保護：確保硬體已初始化
        /// 用於 Load / Unload 等單動流程開始前，確保硬體物件已建立
        /// 若尚未初始化，會自動補初始化（但不強制重建 Roller ）
        /// </summary>
        protected void EnsureHardware()
        {
            // 呼叫 InitialLaneHardware，但不強制重建 Roller
            // p_bForceCreateRoller = false：只在 Roller 不存在時才建立
            InitialLaneHardware(false);
        }

        /// <summary>
        /// 相容舊寫法：確保 Roller 已建立
        /// 此方法為向下相容保留，實際上呼叫 EnsureHardware
        /// </summary>
        protected void EnsureRoller()
        {
            EnsureHardware();
        }

        #endregion

        #region //===================== 帳料方法 =====================

        /// <summary>
        /// Lane 目前是否有帳。
        /// </summary>
        public bool HasTrayBill()
        {
            return m_Temp_Tray_Info != null && m_Temp_Tray_Info.bIsExist;
        }

        /// <summary>
        /// 接收上一站(料盒)傳過來的 Tray 帳。
        /// 注意：這裡只接帳，不代表上一站可以清帳。
        /// 上一站清帳要等 Lane Load_Done 後 ACK。
        /// </summary>
        public virtual void ReceiveTrayBillFromPrevious(clsTrayInfo p_TrayInfo)
        {
            if (p_TrayInfo == null)
                return;

            if (m_Temp_Tray_Info == null)
                m_Temp_Tray_Info = new clsTrayInfo();

            m_Temp_Tray_Info.Clear();
            p_TrayInfo.CopyTo(m_Temp_Tray_Info);

            for (int i = 0; i < m_Temp_Tray_Info.Materials.Count; i++)
            {
                var currentStation = m_Temp_Tray_Info.AssyRecords[i].CurrentStation;
                if (currentStation == WorkStationType.Load)
                {
                    var itemStatus = TrayItemStatus.Empty;
                    //if (m_Temp_Tray_Info.AssyRecords[i].IsExist)
                    {
                        itemStatus = m_Temp_Tray_Info.Materials[i].MaterialType == MaterialType.HeatSink ?
                        TrayItemStatus.HeatSink : TrayItemStatus.Substrate;
                    }

                    m_Temp_Tray_Info.SetItemStatus(i, itemStatus);
                    m_Temp_Tray_Info.AssyRecords[i].IsExist = true;
                }
                else
                {
                    m_Temp_Tray_Info.SetItemStatus(i, TrayItemStatus.Empty);
                    m_Temp_Tray_Info.AssyRecords[i].IsExist = false;
                }
            }

            clsLog.Log(
                enuLogName.ProcessLog.ToString(),
                $"{strThreadLogName} : Receive Tray Bill From Previous. TrayID={m_Temp_Tray_Info.sTrayID}");
        }

        /// <summary>
        /// 相容 Magazine 呼叫名稱。
        /// </summary>
        public void ReceiveTrayBillFromMagazine(clsTrayInfo p_TrayInfo)
        {
            ReceiveTrayBillFromPrevious(p_TrayInfo);
        }

        /// <summary>
        /// 清除 Lane 帳。
        /// 之後 ARM 拿走料或 Lane 出料完成時再呼叫。
        /// </summary>
        public void ClearTrayBill()
        {
            if (m_Temp_Tray_Info == null)
                m_Temp_Tray_Info = new clsTrayInfo();

            m_Temp_Tray_Info.Clear();

            clsLog.Log(
                enuLogName.ProcessLog.ToString(),
                $"{strThreadLogName} : Clear Lane Tray Bill.");
        }

        /// <summary>
        /// 給 UI 或 Log 顯示目前 Lane 帳。
        /// </summary>
        public string GetTrayBillText()
        {
            if (!HasTrayBill())
                return "Empty";

            return $"TrayID={m_Temp_Tray_Info.sTrayID}";
        }

        /// <summary>
        /// 指定上一站 Magazine。
        /// 預設 null，讓子類別 Proc_loadCup_lane 自己指定。
        /// </summary>
        protected virtual BaseMagazine GetPreviousMagazineForBill()
        {
            return null;
        }

        /// <summary>
        /// Lane Load_Done 後通知上一站：我已經收到料，可以清上一站帳。
        /// 支援上游為 Magazine 或 Lane 兩種情境
        /// </summary>
        protected virtual void NotifyPreviousLoadDone()
        {
            if (m_bNotifyPreviousLoadDone)
                return;

            // 情境 1: 上游是 Magazine
            BaseMagazine mag = GetPreviousMagazineForBill();
            if (mag != null)
            {
                mag.ConfirmDownstreamLoadDone();
                m_bNotifyPreviousLoadDone = true;

                clsLog.Log(
                    enuLogName.ProcessLog.ToString(),
                    $"{strThreadLogName} : Notify Previous Magazine Load Done.");
                return;
            }

            // 情境 2: 上游是 Lane
            BaseLane prevLane = GetPreviousLaneForBill();
            if (prevLane != null)
            {
                // Lane → Lane 的情況，上游 Lane 在 Unload_Done 後會自動清帳
                // 這裡只需要確認上游已經完成 Unload 即可
                m_bNotifyPreviousLoadDone = true;

                clsLog.Log(
                    enuLogName.ProcessLog.ToString(),
                    $"{strThreadLogName} : Notify Previous Lane Load Done.");
                return;
            }

            // 沒有上游站點，直接標記完成
            m_bNotifyPreviousLoadDone = true;
        }

        #endregion

        #region //===================== Lane to Lane 帳料支援 =====================

        /// <summary>
        /// 指定上游 Lane (用於 Lane → Lane 傳遞)
        /// 預設 null，讓子類別自己指定
        /// </summary>
        protected virtual BaseLane GetPreviousLaneForBill()
        {
            return null;
        }

        /// <summary>
        /// 指定下游 Lane (用於 Lane → Lane 傳遞)
        /// 預設 null，讓子類別自己指定
        /// </summary>
        protected virtual BaseLane GetNextLaneForBill()
        {
            return null;
        }

        /// <summary>
        /// 指定下游 Magazine (用於 Lane → Magazine 傳遞)
        /// 預設 null，讓子類別自己指定
        /// 僅用於判斷下游類型，實際轉移由 Magazine 處理
        /// </summary>
        protected virtual BaseMagazine GetNextMagazineForBill()
        {
            return null;
        }

        /// <summary>
        /// 從上游 Lane 接收帳
        /// </summary>
        public virtual void ReceiveTrayBillFromLane(clsTrayInfo p_TrayInfo)
        {
            if (p_TrayInfo == null)
                return;

            if (m_Temp_Tray_Info == null)
                m_Temp_Tray_Info = new clsTrayInfo();

            m_Temp_Tray_Info.Clear();
            p_TrayInfo.CopyTo(m_Temp_Tray_Info);

            for (int i = 0; i < m_Temp_Tray_Info.AssyRecords.Count; i++)
            {
                var currentStation = m_Temp_Tray_Info.AssyRecords[i].CurrentStation;
                var trayItemStatud = TrayItemStatus.Empty;
                if (currentStation == WorkStationType.Press)
                {
                    trayItemStatud = TrayItemStatus.Pressed;
                }
                if (currentStation == WorkStationType.AOI)
                {
                    trayItemStatud = m_Temp_Tray_Info.ConvertToItemStatus(m_Temp_Tray_Info.AssyRecords[i].AoiResult);
                }
                if (currentStation == WorkStationType.ASM)
                {
                    trayItemStatud = TrayItemStatus.Assembly;
                }
                else if (currentStation == WorkStationType.Load)
                {
                    var materialType = m_Temp_Tray_Info.Materials[i].MaterialType;
                    trayItemStatud = materialType == MaterialType.IC ? TrayItemStatus.Substrate : TrayItemStatus.Empty;
                    var record = m_Temp_Tray_Info.AssyRecords[i];
                }
                m_Temp_Tray_Info.SetItemStatus(i, trayItemStatud);
            }

            IsDownstreamReceiveCompleted = true;

            clsLog.Log(
                enuLogName.ProcessLog.ToString(),
                $"{strThreadLogName} : Receive Tray Bill From Previous Lane. TrayID={m_Temp_Tray_Info.sTrayID}");
        }

        /// <summary>
        /// 將帳傳給下游 Lane
        /// </summary>
        protected virtual void TransferTrayBillToNextLane()
        {
            BaseLane nextLane = GetNextLaneForBill();

            if (nextLane == null)
                return;

            if (!HasTrayBill())
                return;

            // 將帳 Copy 給下游 Lane
            nextLane.ReceiveTrayBillFromLane(m_Temp_Tray_Info);

            clsLog.Log(
                enuLogName.ProcessLog.ToString(),
                $"{strThreadLogName} : Transfer Tray Bill To Next Lane. TrayID={m_Temp_Tray_Info.sTrayID}");
        }

        #endregion

        public bool SetDi(enuDi p_enuDi, bool p_bValue) => clsDioCtrl.SetDi(p_enuDi, p_bValue);

    }
}
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtEQ.B_Tools;
using ArtTeach;
using System;
using System.Collections.Generic;
using static ArtData.clsEnum;


namespace ArtEQ
{
    public abstract class BaseMagazine : clsThreadProc
    {
        #region Enums

        #region //===================== Action 定義 =====================

        public enum enuAction
        {
            None,

            #region ── 初始化 ──

            Initial = 10000,
            Initial_Done,
            Initial_Fail,

            #endregion

            #region ── 料盒出料給下游 ──

            Magazine_Load = 20000,

            /// <summary>
            /// 等待下游準備完成
            /// </summary>
            Magazine_Load_Waiting,

            /// <summary>
            /// 推桿推出中
            /// </summary>
            Magazine_Loading,
            Magazine_Transfer_Done,
            Magazine_Load_Done,
            Magazine_Load_Fail,

            #endregion

            #region ── 上游入料到料盒 ──

            Magazine_Unload = 30000,

            /// <summary>
            /// 等待上游送來 Boat
            /// </summary>
            Magazine_Unload_Waiting,

            /// <summary>
            /// 等待 Boat 進入 Slot
            /// </summary>
            Magazine_Unloading,
            Magazine_Unload_Done,
            Magazine_Unload_Fail,

            #endregion
        }

        #endregion

        #endregion

        #region Constructors

        #region //===================== Constructor =====================

        protected BaseMagazine(string p_strName) : base(p_strName)
        {
            m_enuAction = enuAction.None;

            // 預設先依目前設定的 Slot 數建立帳。
            // UI 之後可在 RunInitial(slotCount) 前重新指定 m_iUseSlotCount。
            InitialMagazineBill();
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// 是否使用推桿氣缸
        /// </summary>
        public bool UsePushCylinder { get; set; } = true;

        public bool PresentSignal => GetDi(enuDi.IC_Feed_Mag_Present);
        public bool PushFwdSignal => GetDi(enuDi.IC_Feed_Mag_Push_Fwd);
        public bool PushBwdSignal => GetDi(enuDi.IC_Feed_Mag_Push_Bwd);
        public bool OverPressSignal => GetDi(enuDi.IC_Feed_Mag_Over_Press_B);

        public virtual BaseLane PreviousLane => null;

        /// <summary>
        /// 下游過帳完成
        /// </summary>
        public bool DownstreamPostingDone => !m_bWaitDownstreamLoadDone;

        #endregion

        #region Protected Methods

        #region //===================== Scenario =====================

        protected override void Scenario()
        {
            switch (iStepIndex)
            {
                #region //===================前置動作(0)===================

                case 0:
                    // 【前置動作】流程開始，設定未就緒狀態並重啟計時器
                    m_bIsReady = false;
                    Restart();
                    iStepIndex = 50;
                    break;

                case 50:
                    // 【狀態轉換】根據當前 Action 狀態跳轉到對應流程
                    // 並記錄到 ProcessLog
                    iStepIndex = (int)m_enuAction;
                    clsLog.Log(nameof(enuLogName.ProcessLog), strThreadLogName + " : " + m_enuAction.ToString());
                    break;

                #endregion

                #region //===================Initial(10000~19999)===================

                case 10000:
                    // 【初始化流程開始】進入初始化主流程

                    if (UsePushCylinder)
                    {
                        iStepIndex = 10300; // 推桿氣缸初始化
                    }
                    else
                    {
                        iStepIndex = 10400; // Z 軸 Home
                    }

                    break;

                #region // 推桿氣缸初始化

                case 10300:
                    // 【初始化推桿氣缸】設定推桿氣缸的控制參數
                    // 包含：伸出/縮回 DO、伸出/縮回 DI、超時設定等

                    #region 設定氣缸

                    m_CtrlBox.Clear();
                    m_Cylinder_OutPush.Initial(
                        m_DO_OutPush_Extend,                // 推桿伸出 DO
                        m_DI_OutPush_Retract, false,        // 推桿縮回 DI
                        m_DI_OutPush_Extend, false,         // 推桿伸出 DI
                        (uint)m_iPutterTimeout,             // 超時時間
                        "MoveHomeAlarm", "MoveReachAlarm"); // 報警訊息
                    iStepIndex = 10310;

                    #endregion

                    break;

                case 10310:
                    // 【加入推桿縮回動作】將推桿縮回加入控制盒待執行清單

                    #region 加入推桿縮回

                    m_CtrlBox.Clear();
                    Add_Cylinder(m_Cylinder_OutPush, false, m_iPutterAfterDelay, m_iPutterBeforeDelay);
                    iStepIndex = 10330;

                    #endregion

                    break;

                case 10330:
                    // 【執行推桿縮回】實際執行推桿縮回動作
                    // 模擬模式：直接跳過，視為完成
                    // 實際模式：等待氣缸動作完成

                    #region 執行推桿縮回

                    m_CtrlBox.Action(ref iStepIndex, 10400, 10997);

                    #endregion

                    break;

                #endregion

                #region Z 軸 Home

                case 10400:
                    // 【加入 Z 軸 Home 動作】將 Z 軸回原點動作加入控制盒

                    #region Z軸加入Home動作

                    m_CtrlBox.Clear();
                    m_CtrlBox.Add(m_Motion_Z);
                    iStepIndex = 10410;

                    #endregion

                    break;

                case 10410:
                    // 【執行 Z 軸 Home】執行 Z 軸回原點動作
                    // 完成後跳到 10420，失敗跳到 10997

                    #region 執行Z軸Home

                    m_CtrlBox.Home(ref iStepIndex, 10420, 10997);

                    #endregion

                    break;

                case 10420:
                    // 【Z 軸 Home 完成】重啟計時器，準備設定 Z 軸位置為 0
                    m_Timer.Restart();
                    iStepIndex = 10430;
                    break;
                case 10430:
                    // 【等待延遲後設定位置】等待 HomeDelay 時間後，將 Z 軸位置歸零
                    if (m_Timer.IsTimeOut(m_iHomeDelay, clsCmData.enuSecUnit.MilliSec))
                    {
                        clsMotionCtrl.SetPos(m_Motor_Z, 0);
                        iStepIndex = 10999;
                    }

                    break;

                #endregion

                case 10997:
                    // 【初始化失敗】設定狀態為初始化失敗，跳到結束流程
                    m_enuAction = enuAction.Initial_Fail;
                    iStepIndex = 999;
                    break;

                case 10999:
                    // 【初始化完成】設定狀態為初始化完成，跳到結束流程
                    m_enuAction = enuAction.Initial_Done;
                    iStepIndex = 999;
                    break;

                #endregion

                #region //=================== Magazine_Load (料盒出料給下游) (20000~29999) ===================

                case 20000:
                    // 【推料流程開始】進入推料等待狀態
                    m_enuAction = enuAction.Magazine_Load_Waiting;
                    iStepIndex = 20010;
                    break;
                case 20010:
                    if (b_Simulation)
                    {
                        SetDi(m_DI_Magazine_Present, true);
                    }

                    iStepIndex = 20020;
                    break;
                case 20020:
                    // 【檢查 Magazine 在席】確認 Magazine 是否在位

                    #region 確認 Magazine 在席

                    if (GetDi(m_DI_Magazine_Present))
                    {
                        iStepIndex = 20100; // Magazine 在席，繼續推料流程
                    }
                    else
                    {
                        // Magazine 不在席，報警
                        clsEditRunThread.ReportAlarm(enuAlarm.Empty_Magazine);
                        iStepIndex = 20998;
                    }

                    #endregion

                    break;

                case 20100:
                    // 【Z 軸移動到指定 Slot】計算目標 Slot 的 Z 軸位置並開始移動

                    #region Z軸移動

                    m_CtrlBox.Clear();
                    m_Motion_Z.SetActionValue(
                        enuMoveType.Absolute,         // 絕對位置移動
                        enuCurve.T_Curve,             // T 曲線移動
                        CalculateSlotPosZ(m_iSlotNo), // 計算 Slot 的 Z 座標
                        200);                         // 移動速度
                    m_CtrlBox.Add(m_Motion_Z);
                    iStepIndex = 20120;

                    #endregion

                    break;

                case 20120:
                    // 【等待 Z 軸到位】等待 Z 軸移動到目標位置
                    // 成功跳到 20130，失敗跳到 20998

                    #region 等待Z軸到位

                    m_CtrlBox.Action(ref iStepIndex, 20130, 20998);

                    #endregion

                    break;

                case 20130:
                    // 【等待下游淨空】確認下游已淨空，可以接收 Tray
                    // 若下游未淨空，維持等待狀態
                    // 若下游已淨空，進入推料中狀態

                    #region 等待下游 Area 淨空

                    if (ReadyToUnload())
                    {
                        m_enuAction = enuAction.Magazine_Loading;
                        iStepIndex = 20200;
                    }

                    #endregion

                    break;

                case 20200:
                    // 【加入推桿伸出動作】將推桿伸出加入控制盒待執行清單

                    #region 加入推桿伸出

                    m_CtrlBox.Clear();
                    Add_Cylinder(m_Cylinder_OutPush, true,
                        m_iPutterAfterDelay, m_iPutterBeforeDelay);
                    iStepIndex = 20210;

                    #endregion

                    break;

                case 20210:
                    //if (b_Simulation)
                    //{
                    //    // 模擬模式：直接認定推桿伸出完成
                    //    SetDi(m_DI_OutPush_Extend, true);
                    //    SetDi(m_DI_OutPush_Retract, false);
                    //    break;
                    //}

                    iStepIndex = 20220;
                    break;

                case 20220:
                    // 【執行推桿伸出】推桿伸出，將 Tray 推出給下游
                    // 模擬模式：直接設定 DI 並跳到下一步
                    // 實際模式：等待氣缸動作完成

                    #region 執行推桿伸出

                    //過壓檢查
                    if (!GetDi(m_DI_OverPress_B))
                    {
                        // 推桿過壓，報警
                        clsEditRunThread.ReportAlarm(enuAlarm.Over_Press_Alarm);
                        clsDioCtrl.SetDo(m_DO_OutPush_Extend, false); // 強制縮回推桿
                        iStepIndex = 20998;
                        break;
                    }

                    m_CtrlBox.Action(ref iStepIndex, 20300, 20998);

                    #endregion

                    break;

                case 20300:
                    // 【加入推桿縮回動作】推料完成，將推桿縮回加入控制盒

                    #region 加入推桿縮回

                    m_CtrlBox.Clear();
                    Add_Cylinder(m_Cylinder_OutPush, false, m_iPutterAfterDelay, m_iPutterBeforeDelay);
                    iStepIndex = 20310;

                    #endregion

                    break;

                case 20310:
                    // 【執行推桿縮回】推桿縮回回到原位
                    // 模擬模式：直接設定 DI 並跳到完成
                    // 實際模式：等待氣缸動作完成

                    #region 執行推桿縮回

                    m_CtrlBox.Action(ref iStepIndex, 20400, 20998);

                    #endregion

                    break;

                case 20400:
                    // 帳料從 Magazine 轉移給下游
                    TransferBillAfterUnloading();
                    m_enuAction = enuAction.Magazine_Transfer_Done;

                    iStepIndex = 20410;
                    break;
                case 20410:
                    //等待下游過帳完成
                    if (DownstreamPostingDone)
                    {
                        iStepIndex = 20999;
                    }

                    break;

                case 20997:
                    // 【強制停止】設備停止
                    clsEditRunThread.EqStop();
                    iStepIndex = 20998;
                    break;

                case 20998:
                    // 【推料失敗】設定狀態為推料失敗，跳到結束流程
                    m_enuAction = enuAction.Magazine_Load_Fail;
                    iStepIndex = 999;
                    break;

                case 20999:
                    m_enuAction = enuAction.Magazine_Load_Done;
                    iStepIndex = 999;
                    break;

                #endregion

                #region //=================== Magazine_Unload (上游入料到料盒) (30000~39999) ===================

                case 30000:
                    // 【收料流程開始】進入收料等待狀態
                    m_enuAction = enuAction.Magazine_Unload_Waiting;
                    iStepIndex = 30010;
                    break;

                #region 模擬情況時設定在籍檢知

                case 30010:

                    if (b_Simulation)
                    {
                        SetDi(m_DI_Magazine_Present, true);
                        iStepIndex = 30020;
                    }

                    break;

                #endregion

                #region 確認 Magazine 在籍且有空位

                case 30020:
                    // 確認 Magazine 在籍檢知
                    if (GetDi(m_DI_Magazine_Present))
                    {
                        iStepIndex = 30100;
                    }
                    else
                    {
                        // 報警
                        clsEditRunThread.ReportAlarm(enuAlarm.Empty_Magazine);
                        iStepIndex = 30998;
                    }

                    break;

                #endregion

                #region 【Z 軸移動到指定 Slot】計算目標 Slot 的 Z 軸位置並開始移動

                case 30100:

                    m_CtrlBox.Clear();
                    m_Motion_Z.SetActionValue(
                        enuMoveType.Absolute,         // 絕對位置移動
                        enuCurve.T_Curve,             // T 曲線移動
                        CalculateSlotPosZ(m_iSlotNo), // 計算 Slot 的 Z 座標
                        200);                         // 移動速度
                    m_CtrlBox.Add(m_Motion_Z);
                    iStepIndex = 30110;

                    break;

                #endregion

                #region 【等待 Z 軸到位】等待 Z 軸移動到目標位置

                case 30110:

                    // 成功跳到 30120，失敗跳到 30998
                    m_CtrlBox.Action(ref iStepIndex, 30120, 30998);

                    break;

                #endregion

                #region 等待上游 Lane Unload 送來 Tray

                case 30120:

                    // 若上游尚未送料，維持等待狀態
                    // 若上游已送料，進入收料中狀態
                    if (ReadyToLoad())
                    {
                        TransferBillAfterLoading(); // 帳料從上游轉移進 Magazine
                        m_enuAction = enuAction.Magazine_Unloading;
                        iStepIndex = 30130;
                    }

                    break;

                #endregion

                case 30130:
                    // 【確認 Tray 進入 Slot】檢查 Tray 是否已完全進入 Magazine Slot
                    // 模擬模式：直接設定 Present 信號為 true
                    // 實際模式：等待 DI 信號或超時

                    #region 確認 Tray 已進入 Slot

                    if (b_Simulation)
                    {
                        SetDi(m_DI_Magazine_Present, true);
                        iStepIndex = 30140;
                    }

                    if (GetDi(m_DI_Magazine_Present))
                    {
                        iStepIndex = 30140; // Tray 已進入 Slot
                    }
                    else if (IsTimeOut(5000, clsCmData.enuSecUnit.MilliSec))
                    {
                        // 超時仍未偵測到 Tray，報警
                        clsEditRunThread.ReportAlarm(enuAlarm.Cylinder_Timeout_Lane_Unload);
                        iStepIndex = 30998;
                    }

                    #endregion

                    break;

                case 30140:
                    // 【確認收料完成】最終檢查，確認收料動作確實完成

                    #region 完成

                    if (CheckStatus())
                    {
                        iStepIndex = 30999;
                    }

                    #endregion

                    break;

                case 30997:
                    // 【強制停止】設備停止
                    clsEditRunThread.EqStop();
                    iStepIndex = 30998;
                    break;

                case 30998:
                    // 【收料失敗】設定狀態為收料失敗，跳到結束流程
                    m_enuAction = enuAction.Magazine_Unload_Fail;
                    this.iStepIndex = 999;
                    break;

                case 30999:
                    // 【收料完成】執行帳料轉移（上游 → Magazine），設定狀態為完成
                    //TransferBillAfterLoading(); // 帳料從上游轉移進 Magazine
                    m_enuAction = enuAction.Magazine_Unload_Done;
                    this.iStepIndex = 999;
                    break;

                #endregion

                case 999:
                    // 【流程結束】記錄最終狀態，設定為就緒，結束流程
                    clsLog.Log(nameof(enuLogName.ProcessLog), strThreadLogName + " : " + m_enuAction.ToString());
                    m_bIsReady = true;
                    iStepIndex = -1;
                    break;

                default:
                    // 【異常狀態】非預期的 Step，停止流程
                    iStepIndex = -1;
                    Stop();
                    bIsProcessing = false;
                    break;
            }
        }

        #endregion

        protected void LogProcess(string msg)
        {
            clsLog.Log(enuLogName.ProcessLog, strThreadLogName + " : " + msg);
        }

        #endregion

        #region //===================== Axis / Pos / IO =====================

        /// <summary>
        /// Magazine 最大 Slot 數，硬體上限。
        /// </summary>
        protected int m_iSlotMax = 10;

        /// <summary>
        /// 目前使用者設定要使用的 Slot 數。
        /// 例如使用者設定 3，代表只建立 Slot 1~3 的帳。
        /// </summary>
        protected int m_iUseSlotCount = 5;

        protected double m_dMoveSpeed = 200;
        protected int m_iSlotNo = 0;

        /// <summary>
        /// 馬達復歸後，等待多久再將位置歸零 (ms)。
        /// </summary>
        protected readonly int m_iHomeDelay = 1000;

        /// <summary>
        /// 料盒升降馬達Z
        /// </summary>
        protected clsEnum.enuAxis m_Motor_Z;

        protected clsEnum.enuPosName m_posSlot1_Z;

        /// <summary>
        /// 推桿氣缸前進DI
        /// </summary>
        protected clsEnum.enuDi m_DI_OutPush_Extend;

        /// <summary>
        /// 推桿氣缸後退DI
        /// </summary>
        protected clsEnum.enuDi m_DI_OutPush_Retract;

        /// <summary>
        /// 推桿氣缸DO
        /// </summary>
        protected clsEnum.enuDo m_DO_OutPush_Extend;

        /// <summary>
        /// 推桿過壓檢知-B接
        /// </summary>
        protected clsEnum.enuDi m_DI_OverPress_B;

        /// <summary>
        /// 料盒在籍檢知
        /// </summary>
        protected clsEnum.enuDi m_DI_Magazine_Present;

        /// <summary>
        /// 是否模擬
        /// </summary>
        /// <returns></returns>
        public bool b_Simulation => PublicDeclare.bIsSimulate;

        #endregion

        #region //===================== 控制元件 =====================

        /// <summary>
        /// 控制元件
        /// </summary>
        protected clsControlBox m_CtrlBox = new clsControlBox();

        /// <summary>
        /// 控制元件馬達
        /// </summary>
        protected clsBoxMotion m_Motion_Z = new clsBoxMotion();

        /// <summary>
        /// 控制元件推桿氣缸
        /// </summary>
        protected clsBoxCylinder m_Cylinder_OutPush = new clsBoxCylinder();

        #endregion

        #region //===================== 狀態變數 =====================

        /// <summary>
        /// 流程狀態(啟動狀態)
        /// </summary>
        public bool m_bIsReady { get; set; }

        /// <summary>
        /// 流程進行狀態
        /// </summary>
        public enuAction m_enuAction { get; protected set; }

        #region //===================== 帳料 =====================

        /// <summary>
        /// Magazine 帳料資訊
        /// </summary>
        public clsMagazineInfo m_MagazineInfo { get; set; } = new clsMagazineInfo();

        /// <summary>
        /// 是否正在等待下游的 Load_Done 確認 (ACK)。
        /// </summary>
        /// <remarks>
        /// <para>true：已將指定 Slot 的帳料複製並傳送給下游，但尚未收到下游的 Load_Done 確認；</para>
        /// <para>此期間該 Slot 的帳料不得被再次推出或覆蓋。</para>
        /// <para>false：未在等待 ACK 狀態，該 Slot 可依流程進行後續操作（推出、覆寫等）。</para>
        /// <para>此旗標由推料流程在複製帳料給下游時設為 true；</para>
        /// <para>收到下游確認（例如呼叫 ConfirmDownstreamLoadDone）時設回 false。</para>
        /// </remarks>
        protected bool m_bWaitDownstreamLoadDone = false;

        /// <summary>
        /// 等下游 Load_Done 後要清除的 Slot。
        /// </summary>
        protected int m_iPendingClearSlotNo = 0;

        /// <summary>
        /// 測試用 Tray ID 流水號。
        /// </summary>
        private static int m_iTestTrayID = 1;

        #endregion

        #endregion

        #region //===================== Protected 函式設置 (Null) =====================

        protected static T GetSingletonInstance<T>(Func<T> factory) where T : class => SingletonHelper<T>.GetOrCreate(factory);

        protected abstract void BindHardwarePoint();

        /// <summary> 判斷下游 Setup Area 是否淨空，可以推料 </summary>
        protected virtual bool ReadyToUnload() => false;

        /// <summary> 判斷上游 Unload Lane 是否送來 Boat </summary>
        protected virtual bool ReadyToLoad() => false;

        /// <summary> 確認收料動作確實完成 </summary>
        protected virtual bool CheckLoadIsDone() => true;

        /// <summary> 推料完成後，帳料從 Magazine 轉移給下游 </summary>
        protected virtual void TransferBillAfterUnloading()
        {
        }

        /// <summary> 收料完成後，帳料從上游轉移進 Magazine </summary>
        protected virtual void TransferBillAfterLoading()
        {
        }

        protected bool GetDi(clsEnum.enuDi p_enuDi) => clsDioCtrl.GetDi(p_enuDi);

        protected bool SetDi(clsEnum.enuDi p_enuDi, bool p_bCondition) => clsDioCtrl.SetDi(p_enuDi, p_bCondition);

        protected bool GetDo(clsEnum.enuDo p_enuDo) => clsDioCtrl.GetDo(p_enuDo);

        /// <summary>
        /// 計算指定料盒槽位Slot的 Z 軸目標位置。
        /// 依第1槽基準位置 + (槽號-1) × 槽距 等距公式推算，
        /// </summary>
        /// <param name="p_iSlotNo">槽位編號（從1開始）</param>
        protected double CalculateSlotPosZ(int p_iSlotNo)
        {
            //todo: 要補上UI點位元件
            double firstSlotPos = ucPosPmt.GetValueDouble(m_posSlot1_Z);
            double pitch = GetPmt(enuPmtName.Rec_Load_Magazine_Pitch);

            return firstSlotPos + (p_iSlotNo - 1) * pitch;
        }

        protected virtual bool CheckStatus()
        {
            return false;
        }

        #region //===================== 帳料方法 =====================

        protected void CreateMagazineInfo()
        {
            if (m_MagazineInfo == null)
            {
                m_MagazineInfo = new clsMagazineInfo();
            }

            if (m_MagazineInfo.m_trayInfo == null)
            {
                m_MagazineInfo.m_trayInfo = new Dictionary<int, clsTrayInfo>();
            }

            if (b_Simulation)
            {
                SetDi(m_DI_Magazine_Present, true);
            }
        }

        public void CreateEmptyMaterialTrayInfo(int p_iSlotNo)
        {
            if (!IsSlotInUseRange(p_iSlotNo))
            {
                clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : CreateTrayInfo failed. Slot[{p_iSlotNo}] out of use range.");
                return;
            }

            EnsureMagazineInfo();

            if (m_MagazineInfo == null || m_MagazineInfo.m_trayInfo == null)
                return;

            if (!m_MagazineInfo.m_trayInfo.ContainsKey(p_iSlotNo))
                return;

            m_MagazineInfo.bIsExist = true;
            clsTrayInfo tray = m_MagazineInfo.m_trayInfo[p_iSlotNo];

            if (tray == null)
            {
                tray = new clsTrayInfo();
                m_MagazineInfo.m_trayInfo[p_iSlotNo] = tray;
            }

            // 1. 先全部清乾淨
            tray.Clear();

            // 2. 只建立 Tray 層級的帳
            tray.bIsExist = true;
            tray.sTrayID = (m_iTestTrayID++).ToString();
            tray.iRowID = p_iSlotNo;
            tray.iColumnID = 1;
            tray.bTrayDone = false;
            tray.SetMaterialType(MaterialType.Empty);

            for (int i = 0; i < tray.iCols * tray.iRows; i++)
            {
                tray.AssyRecords[i].CurrentStation = WorkStationType.Ng;
                tray.SetItemStatus(i, TrayItemStatus.Empty);
            }

            m_MagazineInfo.m_trayInfo[p_iSlotNo] = tray;
            var log = $"CreateTrayInfo slot {p_iSlotNo} trayHash={tray.GetHashCode()} bIsExist={tray.bIsExist} sTrayID={tray.sTrayID}";
            clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : {log}");
            clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : Create Tray Info. Slot={p_iSlotNo}, TrayID={tray.sTrayID}");
        }

        public void CreateEmptyTrayInfo(int p_iSlotNo)
        {
            if (!IsSlotInUseRange(p_iSlotNo))
            {
                clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : CreateTrayInfo failed. Slot[{p_iSlotNo}] out of use range.");
                return;
            }

            EnsureMagazineInfo();

            if (m_MagazineInfo == null || m_MagazineInfo.m_trayInfo == null)
                return;

            if (!m_MagazineInfo.m_trayInfo.ContainsKey(p_iSlotNo))
                return;

            m_MagazineInfo.bIsExist = true;
            clsTrayInfo tray = m_MagazineInfo.m_trayInfo[p_iSlotNo];

            if (tray == null)
            {
                tray = new clsTrayInfo();
                m_MagazineInfo.m_trayInfo[p_iSlotNo] = tray;
            }

            // 1. 先全部清乾淨
            tray.Clear();

            // 2. 只建立 Tray 層級的帳
            tray.bIsExist = false;
            tray.sTrayID = (m_iTestTrayID++).ToString();
            tray.iRowID = p_iSlotNo;
            tray.iColumnID = 1;
            tray.bTrayDone = false;
            tray.SetMaterialType(MaterialType.Empty);
            tray.SetItemStatus(p_iSlotNo, TrayItemStatus.Empty);

            m_MagazineInfo.m_trayInfo[p_iSlotNo] = tray;
            var log = $"CreateTrayInfo slot {p_iSlotNo} trayHash={tray.GetHashCode()} bIsExist={tray.bIsExist} sTrayID={tray.sTrayID}";
            clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : {log}");
            clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : Create Tray Info. Slot={p_iSlotNo}, TrayID={tray.sTrayID}");
        }

        public void CreateIcTrayInfo(int p_iSlotNo)
        {
            if (!IsSlotInUseRange(p_iSlotNo))
            {
                clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : CreateTrayInfo failed. Slot[{p_iSlotNo}] out of use range.");
                return;
            }

            EnsureMagazineInfo();

            if (m_MagazineInfo == null || m_MagazineInfo.m_trayInfo == null)
                return;

            if (!m_MagazineInfo.m_trayInfo.ContainsKey(p_iSlotNo))
                return;

            m_MagazineInfo.bIsExist = true;
            clsTrayInfo tray = m_MagazineInfo.m_trayInfo[p_iSlotNo];

            if (tray == null)
            {
                tray = new clsTrayInfo();
                m_MagazineInfo.m_trayInfo[p_iSlotNo] = tray;
            }

            // 1. 先全部清乾淨
            tray.Clear();

            // 2. 只建立 Tray 層級的帳
            tray.bIsExist = true;
            tray.sTrayID = (m_iTestTrayID++).ToString();
            tray.iRowID = p_iSlotNo;
            tray.iColumnID = 1;
            tray.bTrayDone = false;
            tray.SetMaterialType(MaterialType.IC);

            m_MagazineInfo.m_trayInfo[p_iSlotNo] = tray;
            var log = $"CreateTrayInfo slot {p_iSlotNo} trayHash={tray.GetHashCode()} bIsExist={tray.bIsExist} sTrayID={tray.sTrayID}";
            clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : {log}");
            clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : Create Tray Info. Slot={p_iSlotNo}, TrayID={tray.sTrayID}");
        }

        public void CreateHeatSinkTrayInfo(int p_iSlotNo)
        {
            if (!IsSlotInUseRange(p_iSlotNo))
            {
                clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : CreateTrayInfo failed. Slot[{p_iSlotNo}] out of use range.");
                return;
            }

            EnsureMagazineInfo();

            if (m_MagazineInfo == null || m_MagazineInfo.m_trayInfo == null)
                return;

            if (!m_MagazineInfo.m_trayInfo.ContainsKey(p_iSlotNo))
                return;

            m_MagazineInfo.bIsExist = true;
            clsTrayInfo tray = m_MagazineInfo.m_trayInfo[p_iSlotNo];

            if (tray == null)
            {
                tray = new clsTrayInfo();
                m_MagazineInfo.m_trayInfo[p_iSlotNo] = tray;
            }

            // 1. 先全部清乾淨
            tray.Clear();

            // 2. 只建立 Tray 層級的帳
            tray.bIsExist = true;
            tray.sTrayID = (m_iTestTrayID++).ToString();
            tray.iRowID = p_iSlotNo;
            tray.iColumnID = 1;
            tray.bTrayDone = false;
            tray.SetMaterialType(MaterialType.HeatSink);

            m_MagazineInfo.m_trayInfo[p_iSlotNo] = tray;
            var log = $"CreateTrayInfo slot {p_iSlotNo} trayHash={tray.GetHashCode()} bIsExist={tray.bIsExist} sTrayID={tray.sTrayID}";
            clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : {log}");
            clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : Create Tray Info. Slot={p_iSlotNo}, TrayID={tray.sTrayID}");
        }

        /// <summary>
        /// 指定下游 Lane。
        /// 預設 null，子類別自己指定。
        /// </summary>
        protected virtual BaseLane GetDownstreamLaneForBill()
        {
            return null;
        }

        /// <summary>
        /// 確保 Magazine Slot 帳存在。
        /// 此方法負責初始化和維護 Magazine 的帳料結構，
        /// 確保每個使用中的 Slot 都有對應的 TrayInfo 物件。
        /// </summary>
        protected void EnsureMagazineInfo()
        {
            // ========== 第一段：確保 Magazine 帳物件存在 ==========
            // 如果 Magazine 資訊物件尚未建立，建立新的實例

            // ========== 第二段：確保 Tray 字典存在 ==========
            // 如果 Tray 資訊字典尚未建立，建立新的 Dictionary
            // Key = Slot 編號 (int)，Value = Tray 資訊 (clsTrayInfo)

            CreateMagazineInfo();

            // ========== 第三段：移除超出使用範圍的 Slot ==========
            // 只保留目前使用者設定的 Slot 1 ~ m_iUseSlotCount。
            // 例如：使用者設定只用 3 個 Slot，就不應該保留 Slot 4、Slot 5 的帳。
            // 建立待移除的 Slot 清單
            List<int> removeKeys = new List<int>();

            // ========== 第四段：找出超出範圍的 Slot ==========
            // 遍歷所有現有的 Slot 編號
            foreach (int slot in m_MagazineInfo.m_trayInfo.Keys)
            {
                // 如果 Slot 編號 < 1 或 > 使用者設定的數量，標記為待移除
                // 例如：m_iUseSlotCount = 3，則 Slot 4、5 會被加入 removeKeys
                if (slot < 1 || slot > m_iUseSlotCount)
                    removeKeys.Add(slot);
            }

            // ========== 第五段：實際移除超出範圍的 Slot ==========
            // 將標記的 Slot 從字典中移除
            foreach (int slot in removeKeys)
            {
                m_MagazineInfo.m_trayInfo.Remove(slot);
            }

            // ========== 第六段：補齊缺少的 Slot 帳 ==========
            // 確保 Slot 1 ~ m_iUseSlotCount 都有對應的 TrayInfo 物件
            for (int slot = 1; slot <= m_iUseSlotCount; slot++)
            {
                // 如果字典中沒有該 Slot，新增一個空的 TrayInfo
                if (!m_MagazineInfo.m_trayInfo.ContainsKey(slot))
                    m_MagazineInfo.m_trayInfo.Add(slot, new clsTrayInfo());

                // 如果該 Slot 的 TrayInfo 是 null，重新建立一個新實例
                // 這是雙重保險，避免後續存取時發生 NullReferenceException
                if (m_MagazineInfo.m_trayInfo[slot] == null)
                    m_MagazineInfo.m_trayInfo[slot] = new clsTrayInfo();
            }
        }

        /// <summary>
        /// 指定 Slot 是否有帳。
        /// </summary>
        public bool HasSlotTrayBill(int p_iSlotNo)
        {
            var log =
                $"HasSlotTrayBill slot {p_iSlotNo} trayHash={m_MagazineInfo.m_trayInfo[p_iSlotNo].GetHashCode()} bIsExist={m_MagazineInfo.m_trayInfo[p_iSlotNo].bIsExist}";
            clsLog.Log(nameof(enuLogName.ProcessLog), log);
            log = $"HasSlotTrayBill, {m_MagazineInfo.GetHashCode().ToString()}, {GetHashCode().ToString()}";
            clsLog.Log(nameof(enuLogName.ProcessLog), log);

            if (!IsSlotInUseRange(p_iSlotNo))
                return false;

            EnsureMagazineInfo();

            if (!m_MagazineInfo.m_trayInfo.ContainsKey(p_iSlotNo))
                return false;

            clsTrayInfo tray = m_MagazineInfo.m_trayInfo[p_iSlotNo];

            return tray != null && tray.bIsExist;
        }

        /// <summary>
        /// 取得指定 Slot 的帳。
        /// </summary>
        public clsTrayInfo GetSlotTrayBill(int p_iSlotNo)
        {
            if (!IsSlotInUseRange(p_iSlotNo))
                return null;

            EnsureMagazineInfo();

            if (!m_MagazineInfo.m_trayInfo.ContainsKey(p_iSlotNo))
                return null;

            return m_MagazineInfo.m_trayInfo[p_iSlotNo];
        }

        /// <summary>
        /// 測試用：建立一筆 Tray 帳。
        /// 正式版不要由 UI 亂建帳，應該由上游流程建立。
        /// </summary>
        /// <summary>
        /// 測試用：建立一筆 Tray 帳。
        /// 正式版不要由 UI 亂建帳，應該由上游流程建立。
        /// </summary>
        public void SetTestTrayBill(int p_iSlotNo)
        {
            if (!IsSlotInUseRange(p_iSlotNo))
            {
                clsLog.Log(
                    nameof(enuLogName.ProcessLog),
                    $"{strThreadLogName} : SetTestTrayBill failed. Slot[{p_iSlotNo}] out of use range. UseSlotCount={m_iUseSlotCount}");
                return;
            }

            EnsureMagazineInfo();

            if (!m_MagazineInfo.m_trayInfo.ContainsKey(p_iSlotNo))
                return;

            clsTrayInfo tray = m_MagazineInfo.m_trayInfo[p_iSlotNo];

            tray.Clear();

            tray.bIsExist = true;
            tray.sTrayID = (m_iTestTrayID++).ToString();
            tray.iRowID = p_iSlotNo;
            tray.iColumnID = 1;

            //todo: delete
            // 測試用：只有 Cup 有帳，Recipe / AOIResult 都沒有
            //if (tray.m_CupInfo != null)
            //{
            //    tray.m_CupInfo.bIsExist = true;
            //    tray.m_CupInfo.sCupID = tray.sTrayID;

            //    if (tray.m_CupInfo.m_RecipeInfo != null)
            //        tray.m_CupInfo.m_RecipeInfo.Clear();

            //    if (tray.m_CupInfo.m_AOIResult != null)
            //        tray.m_CupInfo.m_AOIResult.Clear();
            //}


            // 【新增】六杯獨立帳料也一併建立，維持與 m_CupInfo 一致
            //if (tray.m_CupInfoList != null)
            //{
            //    for (int i = 0; i < clsTrayInfo.CUP_COUNT; i++)
            //    {
            //        if (tray.m_CupInfoList[i] == null)
            //            continue;

            //        tray.m_CupInfoList[i].bIsExist = true;
            //        tray.m_CupInfoList[i].sCupID = $"{tray.sTrayID}-{i + 1}";

            //        if (tray.m_CupInfoList[i].m_RecipeInfo != null)
            //            tray.m_CupInfoList[i].m_RecipeInfo.Clear();

            //        if (tray.m_CupInfoList[i].m_AOIResult != null)
            //            tray.m_CupInfoList[i].m_AOIResult.Clear();
            //    }
            //}

            //clsLog.Log(
            //    clsEnum.enuLogName.ProcessLog.ToString(),
            //    $"{strThreadLogName} : Set Point Test Tray Bill. Slot={p_iSlotNo}, TrayID={tray.sTrayID}, CupCount={clsTrayInfo.CUP_COUNT}");
        }

        public void ClearSlotTrayBill(int p_iSlotNo)
        {
            if (!IsSlotInUseRange(p_iSlotNo))
                return;

            EnsureMagazineInfo();

            if (!m_MagazineInfo.m_trayInfo.ContainsKey(p_iSlotNo))
                return;

            m_MagazineInfo.m_trayInfo[p_iSlotNo].Clear();

            clsLog.Log(
                nameof(enuLogName.ProcessLog),
                $"{strThreadLogName} : Clear Slot Bill. Slot={p_iSlotNo}");
        }

        /// <summary>
        /// 清除所有 Slot 帳。
        /// </summary>
        public void ClearAllSlotTrayBill()
        {
            EnsureMagazineInfo();

            foreach (var item in m_MagazineInfo.m_trayInfo)
            {
                if (item.Value != null)
                    item.Value.Clear();
            }

            m_bWaitDownstreamLoadDone = false;
            m_iPendingClearSlotNo = 0;

            clsLog.Log(
                nameof(enuLogName.ProcessLog),
                $"{strThreadLogName} : Clear All Magazine Slot Bill.");
        }

        /// <summary>
        /// 給 UI 或 Log 顯示 Slot 帳。
        /// </summary>
        public string GetSlotBillText(int p_iSlotNo)
        {
            if (!IsSlotInUseRange(p_iSlotNo))
                return "No Slot";

            EnsureMagazineInfo();

            if (!m_MagazineInfo.m_trayInfo.ContainsKey(p_iSlotNo))
                return "No Slot";

            clsTrayInfo tray = m_MagazineInfo.m_trayInfo[p_iSlotNo];

            if (tray == null || !tray.bIsExist)
                return "Empty";

            return $"TrayID={tray.sTrayID}";
        }

        /// <summary>
        /// 推料前檢查帳是否允許轉移。
        /// </summary>
        protected bool CanTransferBillToDownstream()
        {
            // 上一筆已經複製給下游，但還沒收到下游 ACK，不准再推
            if (m_bWaitDownstreamLoadDone)
                return false;

            // 指定 Slot 必須有帳
            if (!HasSlotTrayBill(m_iSlotNo))
                return false;

            BaseLane lane = GetDownstreamLaneForBill();

            if (lane == null)
                return false;

            // 下游已經有帳，不准覆蓋
            if (lane.HasTrayBill())
                return false;

            return true;
        }

        /// <summary>
        /// 下游 Load_Done 後呼叫。
        /// 這裡才真正清除 Magazine Slot 帳。
        /// </summary>
        public void ConfirmDownstreamLoadDone()
        {
            EnsureMagazineInfo();

            if (!m_bWaitDownstreamLoadDone)
                return;

            if (!m_MagazineInfo.m_trayInfo.ContainsKey(m_iPendingClearSlotNo))
                return;

            clsTrayInfo slotTray = m_MagazineInfo.m_trayInfo[m_iPendingClearSlotNo];

            if (slotTray != null)
                slotTray.Clear();

            clsLog.Log(
                nameof(enuLogName.ProcessLog),
                $"{strThreadLogName} : Downstream Load_Done ACK. Clear Slot[{m_iPendingClearSlotNo}] Bill.");

            m_bWaitDownstreamLoadDone = false;
            m_iPendingClearSlotNo = 0;
        }

        #endregion

        #endregion

        #region ===================== Public 函式 =====================

        public void InitialSet()
        {
            // 推桿氣缸初始化
            m_Cylinder_OutPush.Initial(m_DO_OutPush_Extend, m_DI_OutPush_Retract, true, m_DI_OutPush_Extend, true, (uint)m_iPutterTimeout, "Putter_HomeAlarm",
                "Putter_ReachAlarm");

            // Z 軸初始化
            m_Motion_Z.Initial(m_Motor_Z, MotionUnit.Minimeter, 10000, "Z-Axis Timeout");
            m_Motion_Z.iSimulateDelayTime = 500;
            clsMotionCtrl.SetServo(m_Motor_Z, true);


            if (b_Simulation)
            {
                SetDi(m_DI_Magazine_Present, false);
                SetDi(m_DI_OutPush_Extend, true);
                SetDi(m_DI_OutPush_Retract, false);
                SetDi(m_DI_OverPress_B, true);
            }
        }

        public bool IsProcOK() => !bIsProcessing && m_bIsReady;

        public bool IsProcStop()
        {
            if (IsProcOK()) return true;
            if (iStepIndex > 0 &&
                clsThreadProcManage.bIsStepProc &&
                !bIsKeepProc &&
                iStepIndex % 100 == 0)
                return true;
            return false;
        }

        public void RunInitial()
        {
            // 依 Recipe 設定使用中的 Slot 數(NormalizeSlotCount 會夾在 1~m_iSlotMax 硬體上限之間，
            // Recipe 還沒載入、GetPmt 回傳預設 0 時也會被夾到 1，不會出錯)。
            SetMagazineSlotCount(GetPmt(enuPmtName.Rec_Magazine_Slot_Number));

            RunInitialCore();
        }

        /// <summary>
        /// UI 可呼叫：先設定使用 Slot 數，再執行 Initial，這次呼叫不會被 Recipe 值蓋掉。
        /// 例如 RunInitial(3) 代表本次只建立 Slot 1~3 的帳。
        /// </summary>
        public void RunInitial(int p_iSlotCount)
        {
            SetMagazineSlotCount(p_iSlotCount);
            RunInitialCore();
        }

        /// <summary>
        /// Initial 流程共用核心：建立 Slot 帳、重置流程狀態、啟動硬體初始化。
        /// 呼叫前要先確定 m_iUseSlotCount 已經是這次要用的值——
        /// RunInitial() 用 Recipe 設，RunInitial(int) 用外部指定的值設，兩者互斥、不會互相蓋掉。
        /// </summary>
        private void RunInitialCore()
        {
            // Init 時先依目前使用者設定的 Slot 數建立 Magazine Slot 帳
            InitialMagazineBill();

            clsThreadProcManage.bIsStepProc = false; //(100號停止主要Flag)
            clsThreadProcManage.bStartStepRun = false;

            // 重要：
            // 初始化 Magazine 會用到的推桿氣缸與 Z 軸 Motion。
            // 如果沒有先做這個，10400 加入 m_P_Z_Motion 可能失敗，
            // 10410 就會出現 Control Box is empty。
            InitialSet();

            m_enuAction = enuAction.Initial;
            m_bIsReady = false;
            bIsProcessing = true;
            iStepIndex = 0;
        }

        /// <summary> 推料給下游 </summary>
        public void RunLoad(int p_iSlot)
        {
            if (!IsSlotInUseRange(p_iSlot))
            {
                clsLog.Log(
                    nameof(enuLogName.ProcessLog),
                    $"{strThreadLogName} : RunLoad failed. Slot[{p_iSlot}] out of use range. UseSlotCount={m_iUseSlotCount}");
                return;
            }

            if (IsProcOK())
            {
                m_iSlotNo = p_iSlot;
                RunAction(enuAction.Magazine_Load);
            }
        }

        /// <summary> 收料回 Magazine </summary>
        public void RunUnload(int p_iSlot)
        {
            if (!IsSlotInUseRange(p_iSlot))
            {
                clsLog.Log(
                    nameof(enuLogName.ProcessLog),
                    $"{strThreadLogName} : RunUnload failed. Slot[{p_iSlot}] out of use range. UseSlotCount={m_iUseSlotCount}");
                return;
            }

            if (IsProcOK())
            {
                m_iSlotNo = p_iSlot;
                RunAction(enuAction.Magazine_Unload);
            }
        }

        public bool DI_MagExist() => clsDioCtrl.GetDi(m_DI_Magazine_Present);

        public bool IsPointTray(clsTrayInfo tray)
        {
            if (tray == null)
                return false;

            if (!tray.bIsExist)
                return false;

            //todo : delete
            //if (tray.m_CupInfo == null || !tray.m_CupInfo.bIsExist)
            //    return false;

            //if (tray.m_CupInfo.m_RecipeInfo != null && tray.m_CupInfo.m_RecipeInfo.bIsExist)
            //    return false;

            //if (tray.m_CupInfo.m_AOIResult != null && tray.m_CupInfo.m_AOIResult.bIsExist)
            //    return false;

            return true;
        }

        #region //===================== Magazine Slot 帳初始化 =====================

        /// <summary>
        /// 將 Slot 數量限制在 1~m_iSlotMax。
        /// </summary>
        private int NormalizeSlotCount(int p_iSlotCount)
        {
            if (p_iSlotCount < 1)
                return 1;

            if (p_iSlotCount > m_iSlotMax)
                return m_iSlotMax;

            return p_iSlotCount;
        }

        /// <summary>
        /// Slot 是否在目前使用者設定範圍內。
        /// 例如 m_iUseSlotCount = 3，只有 Slot 1~3 合法。
        /// </summary>
        public bool IsSlotInUseRange(int p_iSlotNo)
        {
            return p_iSlotNo >= 1 && p_iSlotNo <= m_iUseSlotCount;
        }

        /// <summary>
        /// 設定本次 Magazine 要使用的 Slot 數。
        /// 最小 1，最大 m_iSlotMax，目前 m_iSlotMax = 10。
        /// </summary>
        public void SetMagazineSlotCount(int p_iSlotCount)
        {
            m_iUseSlotCount = NormalizeSlotCount(p_iSlotCount);
        }

        /// <summary>
        /// 取得目前使用 Slot 數。
        /// UI 可以用這個更新 ComboBox。
        /// </summary>
        public int GetMagazineSlotCount()
        {
            return m_iUseSlotCount;
        }

        /// <summary>
        /// Init 時建立 Magazine Slot 帳。
        /// 例如 m_iUseSlotCount = 3，就建立 Slot 1~3。
        /// </summary>
        protected void InitialMagazineBill()
        {
            m_iUseSlotCount = NormalizeSlotCount(m_iUseSlotCount);

            if (m_MagazineInfo == null)
                m_MagazineInfo = new clsMagazineInfo();

            // Init 時依使用者設定的 Slot 數重新建立總帳。
            // 例如 m_iUseSlotCount = 3，只建立 Slot 1~3。
            m_MagazineInfo.InitialSlot(m_iUseSlotCount);

            // Init 重新建帳時，清掉等待下游 ACK 的狀態。
            m_bWaitDownstreamLoadDone = false;
            m_iPendingClearSlotNo = 0;

            clsLog.Log(nameof(enuLogName.ProcessLog), $"{strThreadLogName} : Initial Magazine Bill. SlotCount={m_iUseSlotCount}");
        }

        #endregion

        #endregion

        #region //===================== 區域變數=====================

        private int m_iPutterTimeout => GetPmt(enuPmtName.Sys_Timeout_Putter);
        private int m_iPutterBeforeDelay => GetPmt(enuPmtName.Sys_Delay_Putter_Before);
        private int m_iPutterAfterDelay => GetPmt(enuPmtName.Sys_Delay_Putter_After);
        private int GetPmt(clsEnum.enuPmtName name) => ucParameter.GetValueInt(name);
        private clsHiPerfTimer m_Timer = new clsHiPerfTimer();

        #endregion

        #region //===================== Private 函式 =====================

        private void Add_Cylinder(clsBoxCylinder p_Cylinder, bool p_bExtend, double p_dAfterDelay = 0, double p_dBeforeDelay = 0)
        {
            if (b_Simulation)
            {
                if (p_bExtend)
                {
                    // 模擬推桿伸出
                    SetDi(m_DI_OutPush_Extend, true);
                    SetDi(m_DI_OutPush_Retract, false);
                }
                else
                {
                    // 模擬推桿縮回
                    SetDi(m_DI_OutPush_Extend, false);
                    SetDi(m_DI_OutPush_Retract, true);
                }
            }

            p_Cylinder.SetActionValue(p_bExtend, false, (uint)p_dBeforeDelay, (uint)p_dAfterDelay, 0);
            m_CtrlBox.Add(p_Cylinder);
        }

        private void RunAction(enuAction p_enuAction)
        {
            if (p_enuAction == enuAction.Initial)
            {
                RunInitial();
                return;
            }

            if (IsProcOK())
            {
                iStepIndex = 0;
                m_enuAction = p_enuAction;
                bIsProcessing = true;
                bIsKeepProc = true;
            }
        }

        #endregion
    }
}
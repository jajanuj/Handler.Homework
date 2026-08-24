using System;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtEQ._2_Function_流程_.Proc;
using ArtEQ.B_Tools;
using static ArtData.clsEnum;

namespace ArtEQ._2_Function_流程_.BaseProc
{
    public abstract class BasePressStation : clsThreadProc
    {
        #region Enums

        #region ===================== Action 定義 =====================

        public enum enuAction
        {
            None,

            Initial = 10000,
            Initial_Done,
            Initial_Fail,

            /// <summary>
            /// 壓合開始
            /// </summary>
            Press = 20000,

            /// <summary>
            /// 確認壓合站準備完成
            /// </summary>
            Press_Waiting,

            /// <summary>
            /// 壓合站工作中
            /// </summary>
            Press_Working,
            Press_Waiting_Sign,
            Press_Done,
            Press_Fail,
        }

        #endregion

        #endregion

        #region Constructors

        public BasePressStation(string name) : base(name)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// 啟用壓合站功能
        /// </summary>
        public bool EnablePressStation => ucParameter.GetValueBool(enuPmtName.Sys_EnablePressStation);

        private int m_iPutterTimeout => GetPmt(enuPmtName.Sys_Timeout_Putter);
        private int m_iPutterBeforeDelay => GetPmt(enuPmtName.Sys_Delay_Putter_Before);
        private int m_iPutterAfterDelay => GetPmt(enuPmtName.Sys_Delay_Putter_After);

        /// <summary>
        /// 系統設定 壓合時間
        /// </summary>
        public int PressTime => GetPmt(enuPmtName.Sys_Delay_Press_Time);
        /// <summary>
        /// 本站帳料
        /// </summary>
        public clsTrayInfo m_Temp_Tray_Info { get; set; } = new clsTrayInfo();


        public bool bReady { get; protected set; }

        public bool m_bIsReady
        {
            get { return bReady; }
            protected set { bReady = value; }
        }

        /// <summary>目前動作，AR / UI 透過此觀察流程狀態。</summary>
        public enuAction m_enuAction { get; protected set; }

        /// <summary>
        /// 是否模擬
        /// </summary>
        /// <returns></returns>
        private bool b_Simulation => PublicDeclare.bIsSimulate;

        /// <summary>
        /// 壓合站流道
        /// </summary>
        public abstract BaseLane PressLane { get; }

        #endregion

        #region Public Methods

        public bool IsProcOK() => !bIsProcessing && m_bIsReady;


        /// <summary> 壓合流程 </summary>
        public void RunPress()
        {
            if (IsProcOK())
            {
                RunAction(enuAction.Press);
            }
        }

        public void RunInitial()
        {
            // Initial 時清除帳籍
            //ClearTrayBill();

            clsThreadProcManage.bIsStepProc = false; //(100號停止主要Flag)
            clsThreadProcManage.bStartStepRun = false;
            InitialSet();

            m_enuAction = enuAction.Initial;
            m_bIsReady = false;
            bIsProcessing = true;
            iStepIndex = 10000;
        }

        public void InitialSet()
        {
            // 推桿氣缸初始化
            m_Cylinder_Press.Initial(m_DO_Press_Cylinder, m_DI_Press_Bwd, false, m_DI_Press_Fwd, true, (uint)m_iPutterTimeout, "Putter_HomeAlarm",
                "Putter_ReachAlarm");

            if (b_Simulation)
            {
                SetDi(m_DI_Press_Fwd, false);
                SetDi(m_DI_Press_Bwd, true);
                SetDi(m_DI_Press_OverPress_B, true);
            }
        }

        #endregion

        #region Protected Methods

        protected static T GetSingletonInstance<T>(Func<T> factory) where T : class => SingletonHelper<T>.GetOrCreate(factory);

        protected override void Scenario()
        {
            switch (iStepIndex)
            {
                #region ===================== Initial (初始化流程 10000-10999) =====================

                #region 【初始化開始】

                case 10000:
                    iStepIndex = 10200;
                    break;

                #endregion

                //設定壓合氣缸縮回
                case 10200:
                    m_CtrlBox.Clear();
                    AddPressCylinder(false);
                    iStepIndex = 10210;
                    break;

                //執行壓合氣缸縮回動作
                case 10210:
                    m_CtrlBox.Action(ref iStepIndex, 10999, 10998);
                    break;

                #region 【初始化失敗】設定狀態，結束流程

                case 10998:
                    m_enuAction = enuAction.Initial_Fail;
                    m_bIsReady = false;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                #endregion

                #region 【 初始化完成】壓合站 已就緒

                case 10999:
                    m_enuAction = enuAction.Initial_Done;
                    m_bIsReady = true;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                #endregion

                #endregion

                #region //===================== 壓合站 主流程 (20000-20999) =====================

                #region 【壓合站 流程開始】

                case 20000:
                    m_enuAction = enuAction.Press;

                    iStepIndex = EnablePressStation ? 20010 : 21000;
                    break;

                case 20010:
                    // 檢查壓合站帳籍有料 && 壓合流道到位檢知
                    if (PressLane.m_Temp_Tray_Info.bIsExist && PressLane.ArrivalSignal)
                    {
                        iStepIndex = 20200;
                        break;
                    }
                    else
                    {
                        // 壓合站流道無料，報警通知
                        clsEditRunThread.ReportAlarm(enuAlarm.Press_Is_Empty);
                    }

                    break;

                #endregion

                //todo :0821
                //等待取料流道是否準備完成，判斷有無料盤
                case 20200:
                    m_enuAction = enuAction.Press_Waiting;
                    iStepIndex = ReadyToPress() ? 20300 : 20998;
                    break;

                // 設定壓合氣缸前進
                case 20300:
                    m_CtrlBox.Clear();
                    AddPressCylinder(true);
                    iStepIndex = 20310;
                    break;

                // 開始壓合氣缸動作
                case 20310:
                    m_CtrlBox.Action(ref iStepIndex, 20400, 20998);
                    break;

                // 設定壓合時間
                case 20400:
                    Restart();
                    iStepIndex = 20410;
                    break;

                // 等待壓合時間
                case 20410:
                    if (IsTimeOut(PressTime, clsCmData.enuSecUnit.MilliSec))
                    {
                        iStepIndex = 20500;
                    }
                    break;

                // 設定壓合氣缸後退-上升
                case 20500:
                    m_CtrlBox.Clear();
                    AddPressCylinder(false);
                    iStepIndex = 20510;
                    break;

                // 開始壓合氣缸動作
                case 20510:
                    m_CtrlBox.Action(ref iStepIndex, 20600, 20998);
                    break;

                //過帳
                case 20600:
                    SetTrayWork(true);
                    iStepIndex = 20999;
                    break;

                case 21000:
                    // 跳過實體氣缸動作，但流程放行(SetTrayWork)不能跳過，否則
                    // AR_Press_Station.CanPress() 永遠找得到「有料但沒放行」的格子，
                    // 陷入 100000->200000->200100->100000 無限重觸發。傳 false 只推進
                    // 流程(IsPressSkipped=true)，不冒充成真的物理壓合過(IsPressed 維持 false)。
                    SetTrayWork(false);
                    iStepIndex = 20999;
                    break;

                // 【壓合 失敗】取料流程失敗
                case 20998:
                    m_enuAction = enuAction.Press_Fail;
                    m_bIsReady = false;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                case 20999:
                    m_enuAction = enuAction.Press_Done;
                    m_bIsReady = true;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                    #endregion
            }
        }

        /// <summary>
        /// 判斷壓合站有無料，是否可以進行取料動作。
        /// </summary>
        /// <returns></returns>
        protected virtual bool ReadyToPress()
        {
            var pressLane = Proc_Press_Lane.GetSingleton();
            return pressLane.IsProcOK() && pressLane.m_Temp_Tray_Info.bIsExist;
        }

        protected abstract void BindHardwarePoint();

        protected abstract bool SetTrayWork(bool p_bPhysicallyPressed);

        #endregion

        #region Private Methods

        private int GetPmt(clsEnum.enuPmtName name) => ucParameter.GetValueInt(name);

        private void RunAction(enuAction p_enuAction)
        {
            if (p_enuAction == enuAction.Initial)
            {
                RunInitial();
                return;
            }

            if (IsProcOK())
            {
                iStepIndex = (int)p_enuAction;
                m_enuAction = p_enuAction;
                bIsProcessing = true;
                bIsKeepProc = true;
            }
        }

        private void AddPressCylinder(bool p_bExtend)
        {
            Add_Cylinder(m_Cylinder_Press, p_bExtend, m_iPutterAfterDelay, m_iPutterBeforeDelay);
        }

        private void Add_Cylinder(clsBoxCylinder p_Cylinder, bool p_bExtend, double p_dAfterDelay = 0, double p_dBeforeDelay = 0)
        {
            if (b_Simulation)
            {
                if (p_bExtend)
                {
                    // 模擬壓合氣缸伸出
                    SetDi(m_DI_Press_Fwd, true);
                    SetDi(m_DI_Press_Bwd, false);
                }
                else
                {
                    // 模擬壓合氣缸縮回
                    SetDi(m_DI_Press_Fwd, false);
                    SetDi(m_DI_Press_Bwd, true);
                }
            }

            p_Cylinder.SetActionValue(p_bExtend, false, (uint)p_dBeforeDelay, (uint)p_dAfterDelay, 0);
            m_CtrlBox.Add(p_Cylinder);
        }

        private bool GetDi(enuDi p_enuDi) => clsDioCtrl.GetDi(p_enuDi);
        private bool SetDi(enuDi p_enuDi, bool p_bValue) => clsDioCtrl.SetDi(p_enuDi, p_bValue);
        private bool SetDo(enuDo p_enuDo, bool p_bValue) => clsDioCtrl.SetDo(p_enuDo, p_bValue);

        #endregion

        #region ===================== 控制盒 =====================

        /// <summary>
        /// 控制盒：把多個氣缸 / 軸控動作加進來後，用 Action() 等待完成
        /// </summary>
        protected clsControlBox m_CtrlBox = new clsControlBox();

        /// <summary>
        /// 控制元件壓合氣缸
        /// </summary>
        protected clsBoxCylinder m_Cylinder_Press = new clsBoxCylinder();

        #endregion

        #region ===================== Axis / Pos / IO =====================

        /// <summary>
        /// 壓合氣缸前進檢知DI
        /// </summary>
        protected enuDi m_DI_Press_Fwd;

        /// <summary>
        /// 壓合氣缸後退檢知DI
        /// </summary>
        protected enuDi m_DI_Press_Bwd;

        /// <summary>
        /// 壓合氣缸過壓檢知_(B)DI
        /// </summary>
        protected enuDi m_DI_Press_OverPress_B;

        /// <summary>
        /// 壓合氣缸啟動DO
        /// </summary>
        protected enuDo m_DO_Press_Cylinder;

        #endregion
    }
}
using System;
using ArtCommonLib;
using ArtControlLib;
using ArtEQ.B_Tools;
using ArtTeach;
using static ArtData.clsEnum;

namespace ArtEQ._2_Function_流程_.BaseProc
{
    public abstract class BaseAoiStation : clsThreadProc
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
            /// 檢測開始
            /// </summary>
            AOI = 20000,

            /// <summary>
            /// 確認檢測站準備完成
            /// </summary>
            AOI_Waiting,

            /// <summary>
            /// 檢測站工作中
            /// </summary>
            AOI_Working,
            AOI_Waiting_Sign,
            AOI_Done,
            AOI_Fail,
        }

        #endregion

        #endregion

        #region Constant

        /// <summary>
        /// 馬達移動速度
        /// </summary>
        protected const double m_dMotorSpeed = 200;

        #endregion

        #region Fields

        /// <summary>
        /// 模擬視覺工作延遲時間 1000 (ms)。
        /// </summary>
        protected int m_iAoiDelay;
        protected Random m_random = new Random();

        /// <summary>
        /// 馬達復歸後，延遲時間 1000 (ms)。
        /// </summary>
        protected readonly int m_iHomeDelay = 1000;

        /// <summary>
        /// 隨機產生AOI檢測結果
        /// </summary>
        private readonly Random m_rnd = new Random();

        protected double m_dBaseCellPosX = ucPosPmt.GetValueDouble(enuPosName.AOI_BasePos_X);

        protected double m_dBaseCellPosY = ucPosPmt.GetValueDouble(enuPosName.AOI_BasePos_Y);

        protected double m_dFocusPosZ = ucPosPmt.GetValueDouble(enuPosName.AOI_FocusPos_Z);

        /// <summary>
        /// Z軸安全位置
        /// </summary>
        protected double m_dSafePos_Z = ucPosPmt.GetValueDouble(enuPosName.AOI_SafePos_Z);

        #endregion

        #region Constructors

        public BaseAoiStation(string name) : base(name)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// 檢測站壓合經過時間(秒)
        /// </summary>
        public double ElapsedTime { get; private set; }

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
        /// AOI站流道
        /// </summary>
        public abstract BaseLane AOILane { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 重置檢測站工作時間
        /// </summary>
        public void ResetInspectTime() => ElapsedTime = 0;

        public bool IsProcOK() => !bIsProcessing && m_bIsReady;

        /// <summary> 執行檢測流程 </summary>
        public void RunAOI(int col, int row)
        {
            if (IsProcOK())
            {
                RunAction(enuAction.AOI);
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

        public void RunInspect(int p_iCol, int p_iRow)
        {
            if (IsProcOK())
            {
                m_workColumn = p_iCol;
                m_workRow = p_iRow;
                RunAction(enuAction.AOI);
            }
        }

        public void InitialSet()
        {
            // X 軸初始化
            m_Motion_X.Initial(m_Axis_X, MotionUnit.Minimeter, 10000, "X-Axis Timeout");
            m_Motion_X.iSimulateDelayTime = 500;
            clsMotionCtrl.SetServo(m_Axis_X, true);

            // Y 軸初始化
            m_Motion_Y.Initial(m_Axis_Y, MotionUnit.Minimeter, 10000, "Y-Axis Timeout");
            m_Motion_Y.iSimulateDelayTime = 500;
            clsMotionCtrl.SetServo(m_Axis_Y, true);

            // Z 軸初始化
            m_Motion_Z.Initial(m_Axis_Z, MotionUnit.Minimeter, 10000, "Z-Axis Timeout");
            m_Motion_Z.iSimulateDelayTime = 500;
            clsMotionCtrl.SetServo(m_Axis_Z, true);
        }

        public void GetCellCenterPos(int iCol, int iRow, out double dPosX, out double dPosY)
        {
            var m_dCellWidth = GetPmt(enuPmtName.Rec_Cell_Width);
            var m_dCellHeight = GetPmt(enuPmtName.Rec_Cell_Height);
            var m_dCellPitchX = GetPmt(enuPmtName.Rec_Cell_Pitch_X);
            var m_dCellPitchY = GetPmt(enuPmtName.Rec_Cell_Pitch_Y);

            // Step1：從 Row0,Col0 的教點，用 Pitch 換算出指定 Row/Col 那顆 Cell 的左上角座標
            double dCellTopLeftX = m_dBaseCellPosX + iCol * m_dCellPitchX;
            double dCellTopLeftY = m_dBaseCellPosY + iRow * m_dCellPitchY;

            // Step2：從左上角推算中心點 (加上 Cell 自身尺寸的一半)
            dPosX = dCellTopLeftX + m_dCellWidth / 2.0;
            dPosY = dCellTopLeftY + m_dCellHeight / 2.0;
            dPosX = 0;
            dPosY = 0;
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
                    m_enuAction = enuAction.Initial;
                    iStepIndex = 10100;
                    break;

                #endregion

                // 將 Z 軸回原點動作加入控制盒
                case 10100:
                    m_CtrlBox.Clear();
                    m_CtrlBox.Add(m_Motion_Z);
                    iStepIndex = 10110;
                    break;

                // 執行 Z 軸回原點動作
                case 10110:
                    m_CtrlBox.Home(ref iStepIndex, 10120, 10998);
                    break;

                // 重啟計時器，準備設定 Z 軸位置為 0
                case 10120:
                    Restart();
                    iStepIndex = 10130;
                    break;

                // 等待 HomeDelay 時間後，將 Z 軸位置歸零
                case 10130:
                    if (IsTimeOut(m_iHomeDelay, clsCmData.enuSecUnit.MilliSec))
                    {
                        clsMotionCtrl.SetPos(m_Axis_Z, 0);
                        iStepIndex = 10200;
                    }

                    break;

                // 將 X/Y 軸回原點動作加入控制盒
                case 10200:
                    m_CtrlBox.Clear();
                    m_CtrlBox.Add(m_Motion_X);
                    m_CtrlBox.Add(m_Motion_Y);
                    iStepIndex = 10210;
                    break;

                // 執行 X/Y 軸回原點動作
                case 10210:
                    m_CtrlBox.Home(ref iStepIndex, 10220, 10998);
                    break;

                // 重啟計時器，準備設定 X/Y 軸位置為 0
                case 10220:
                    Restart();
                    iStepIndex = 10230;
                    break;

                // 等待 HomeDelay 時間後，將 X/Y 軸位置歸零
                case 10230:
                    if (IsTimeOut(m_iHomeDelay, clsCmData.enuSecUnit.MilliSec))
                    {
                        clsMotionCtrl.SetPos(m_Axis_X, 0);
                        clsMotionCtrl.SetPos(m_Axis_Y, 0);
                        iStepIndex = 10999;
                    }

                    break;

                #region 【初始化失敗】設定狀態，結束流程

                case 10998:
                    m_enuAction = enuAction.Initial_Fail;
                    m_bIsReady = false;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                #endregion

                #region 【 初始化完成】視覺檢測站 已就緒

                case 10999:
                    ResetInspectTime();
                    m_enuAction = enuAction.Initial_Done;
                    m_bIsReady = true;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                #endregion

                #endregion

                #region //===================== 視覺檢測站 主流程 (20000-20999) =====================

                // 視覺檢測站 流程開始
                case 20000:
                    m_enuAction = enuAction.AOI;
                    iStepIndex = 20010;
                    break;

                // 檢查AOI站帳籍有料 && AOI流道到位檢知
                case 20010:
                    if (ReadyToInspect())
                    {
                        iStepIndex = 20200;
                    }
                    else
                    {
                        // AOI站流道無料，報警通知
                        clsEditRunThread.ReportAlarm(enuAlarm.AOI_Is_Empty);
                    }

                    break;

                // 設定Z軸移動到對焦位置
                case 20100:
                    m_CtrlBox.Clear();
                    SetAddMotorZMoveAbsolute(m_dFocusPosZ);
                    iStepIndex = 20110;
                    break;

                // 執行Z軸移動到對焦位置
                case 20110:
                    m_CtrlBox.Action(ref iStepIndex, 20200, 20998);
                    break;

                case 20200:
                    GetCellCenterPos(m_workColumn, m_workRow, out double posX, out double posY);
                    m_CtrlBox.Clear();
                    SetAddMotorXMoveAbsolute(posX);
                    SetAddMotorYMoveAbsolute(posY);
                    iStepIndex = 20310;
                    break;

                case 20310:
                    m_CtrlBox.Action(ref iStepIndex, 20400, 20998);
                    break;

                // AOI準備檢測
                case 20400:
                    m_enuAction = enuAction.AOI_Working;
                    Restart();
                    m_iAoiDelay = m_random.Next(500, 1001);
                    iStepIndex = 20410;
                    break;

                // AOI檢測
                case 20410:
                    ElapsedTime = Math.Round(Elapsed(clsCmData.enuSecUnit.Sec), 2);

                    if (IsTimeOut(m_iAoiDelay, clsCmData.enuSecUnit.MilliSec))
                    {
                        Stop();
                        iStepIndex = 20500;
                    }

                    break;

                //過帳
                case 20500:
                    m_enuAoiResult = (AoiResult)m_rnd.Next(1, 3);
                    SetTrayWork();
                    iStepIndex = 20999;
                    break;

                // AOI失敗 檢測流程失敗
                case 20998:
                    m_enuAction = enuAction.AOI_Fail;
                    m_bIsReady = false;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                // AOI完成
                case 20999:
                    m_enuAction = enuAction.AOI_Done;
                    m_bIsReady = true;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                    #endregion
            }
        }

        /// <summary>
        /// 判斷檢測站有無料，是否可以進行檢測動作。
        /// </summary>
        /// <returns></returns>
        protected virtual bool ReadyToInspect() => AOILane.IsProcOK() && AOILane.m_Temp_Tray_Info.bIsExist && AOILane.ArrivalSignal;

        protected abstract void BindHardwarePoint();

        protected abstract bool SetTrayWork();

        #endregion

        #region Private Methods

        private int GetPmt(enuPmtName name) => ucParameter.GetValueInt(name);

        private void RunAction(enuAction p_enuAction)
        {
            if (p_enuAction == enuAction.Initial)
            {
                RunInitial();
                return;
            }

            if (IsProcOK())
            {
                clsThreadProcManage.bIsStepProc = false;

                iStepIndex = (int)p_enuAction;
                m_enuAction = p_enuAction;
                bIsProcessing = true;
                bIsKeepProc = true;
            }
        }

        private bool GetDi(enuDi p_enuDi) => clsDioCtrl.GetDi(p_enuDi);
        private bool SetDi(enuDi p_enuDi, bool p_bValue) => clsDioCtrl.SetDi(p_enuDi, p_bValue);
        private bool SetDo(enuDo p_enuDo, bool p_bValue) => clsDioCtrl.SetDo(p_enuDo, p_bValue);

        /// <summary>
        /// 設定並加入馬達X絕對移動動作
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        private void SetAddMotorXMoveAbsolute(double position, double speed = m_dMotorSpeed)
        {
            m_Motion_X.SetActionValue(enuMoveType.Absolute, enuCurve.T_Curve, position, speed);
            m_CtrlBox.Add(m_Motion_X);
        }

        /// <summary>
        /// 設定並加入馬達Y絕對移動動作
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        private void SetAddMotorYMoveAbsolute(double position, double speed = m_dMotorSpeed)
        {
            m_Motion_Y.SetActionValue(enuMoveType.Absolute, enuCurve.T_Curve, position, speed);
            m_CtrlBox.Add(m_Motion_Y);
        }

        /// <summary>
        /// 設定並加入馬達Z絕對移動動作
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        private void SetAddMotorZMoveAbsolute(double position, double speed = m_dMotorSpeed)
        {
            m_Motion_Z.SetActionValue(enuMoveType.Absolute, enuCurve.T_Curve, position, speed);
            m_CtrlBox.Add(m_Motion_Z);
        }

        #endregion

        #region ===================== 控制盒 =====================

        /// <summary>
        /// 控制盒：把多個氣缸 / 軸控動作加進來後，用 Action() 等待完成
        /// </summary>
        protected clsControlBox m_CtrlBox = new clsControlBox();

        /// <summary>
        /// 檢測馬達X軸
        /// </summary>
        protected clsBoxMotion m_Motion_X = new clsBoxMotion();

        /// <summary>
        /// 檢測馬達Y軸
        /// </summary>
        protected clsBoxMotion m_Motion_Y = new clsBoxMotion();

        /// <summary>
        /// 檢測馬達Z軸
        /// </summary>
        protected clsBoxMotion m_Motion_Z = new clsBoxMotion();

        #endregion

        #region ===================== Axis / Pos / IO =====================

        /// <summary>
        /// 馬達X軸
        /// </summary>
        protected enuAxis m_Axis_X;

        /// <summary>
        /// 馬達Y軸
        /// </summary>
        protected enuAxis m_Axis_Y;

        /// <summary>
        /// 馬達Z軸
        /// </summary>
        protected enuAxis m_Axis_Z;

        /// <summary>
        /// 工作的欄
        /// </summary>
        protected int m_workColumn;

        /// <summary>
        /// 工作的列
        /// </summary>
        protected int m_workRow;

        /// <summary>
        /// 目前檢測結果
        /// </summary>
        protected AoiResult m_enuAoiResult;

        #endregion
    }
}
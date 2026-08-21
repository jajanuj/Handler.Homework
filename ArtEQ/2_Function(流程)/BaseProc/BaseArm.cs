using ArtCommonLib;
using ArtControlLib;
using ArtEQ._4_Class_基本類別_;
using ArtEQ.B_Tools;
using ArtTeach;
using System;
using static ArtData.clsEnum;

namespace ArtEQ._2_Function_流程_.BaseProc
{
    public enum PickPlace
    {
        None = 0,
        Pick,
        Place
    }

    public abstract class BaseArm : clsThreadProc
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
            /// 取料開始
            /// </summary>
            Pick = 20000,

            /// <summary>
            /// 等待取料站準備完成
            /// </summary>
            Pick_Waiting,

            /// <summary>
            /// 移動中(無物料)
            /// </summary>
            Pick_Moving,

            /// <summary>
            /// 取料中
            /// </summary>
            Picking,
            Pick_Waiting_Sign,
            Pick_Done,
            Pick_Fail,

            Place = 30000,
            Place_Waiting,
            Placing,
            Place_Waiting_Sign,
            Place_Transfer_Done,
            Place_Done,
            Place_Fail,
        }

        #endregion

        #endregion

        #region Constant

        /// <summary>
        /// 馬達移動速度
        /// </summary>
        protected const double m_dMotorSpeed = 200;

        /// <summary>
        /// 吸嘴真空破壞時間
        /// </summary>
        protected const double m_dAirBlowTime = 200;

        private const int m_dBaseCellPosX = 100;
        private const int m_dBaseCellPosY = 100;
        private const int m_dCellPitchY = 3;
        private const int m_dCellPitchX = 2;
        private const double m_dCellWidth = 10;
        private const double m_dCellLength = 10;

        #endregion

        #region Fields

        /// <summary>
        /// 吸嘴真空啟動後延遲時間
        /// </summary>
        protected readonly int m_VacuumDelay = 300;

        /// <summary>
        /// 馬達復歸後，延遲時間 1000 (ms)。
        /// </summary>
        protected readonly int m_iHomeDelay = 1000;

        /// <summary>
        /// Z軸安全位置
        /// </summary>
        protected double m_dSafePos_Z = ucPosPmt.GetValueDouble(enuPosName.SafePos_Z);


        private PPStation m_enuPPStation;
        protected int m_iPickColumn;
        protected int m_iPickRow;
        protected int m_iPlaceColumn;
        protected int m_iPlaceRow;

        protected PickPlace m_pickPlace;

        #endregion

        #region Constructors

        protected BaseArm(string p_strName) : base(p_strName)
        {
            m_enuAction = enuAction.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 取料流道
        /// </summary>
        public BaseLane PickLane { get; protected set; }

        /// <summary>
        /// 放料流道
        /// </summary>
        public BaseLane PlaceLane { get; protected set; }

        public PPStation PPStation { get; protected set; }
        public bool bReady { get; protected set; }

        public bool m_bIsReady
        {
            get { return bReady; }
            protected set { bReady = value; }
        }

        /// <summary>
        /// 手臂吸嘴帳料
        /// </summary>
        public clsAssyRecord AssyRecord { get; protected set; } = new clsAssyRecord();

        #endregion

        #region Public Methods

        /// <summary>
        /// 清除帳籍
        /// </summary>
        public void ClearTrayBill()
        {
            AssyRecord = new clsAssyRecord();
        }

        public bool IsProcOK() => !bIsProcessing && m_bIsReady;

        /// <summary>UI / AR 呼叫：初始化 / 復歸。</summary>
        public void RunInitial()
        {
            // Initial 時清除帳籍
            ClearTrayBill();

            if (b_Simulation)
            {
                SetDi(m_DI_Vacuum, false);
            }

            clsThreadProcManage.bIsStepProc = false; //(100號停止主要Flag)
            clsThreadProcManage.bStartStepRun = false;

            // 重新允許下一次 Load_Done 通知上一站清帳
            m_bNotifyPreviousLoadDone = false;

            m_enuPPStation = PPStation.None;
            m_enuAction = enuAction.Initial;
            m_bIsReady = false;
            bIsProcessing = true;
            iStepIndex = 10000;
        }

        public void RunPick(PPStation p_enuStation, int p_iColumn, int p_iRow)
        {
            PPStation = p_enuStation;
            m_iPickColumn = p_iColumn;
            m_iPickRow = p_iRow;

            m_pickPlace = PickPlace.Pick;
            m_enuAction = enuAction.Pick;
            m_bIsReady = false;
            bIsProcessing = true;
            iStepIndex = 20000;
        }

        public void RunPlace(PPStation p_enuStation, int p_iColumn, int p_iRow)
        {
            PPStation = p_enuStation;
            m_iPlaceColumn = p_iColumn;
            m_iPlaceRow = p_iRow;

            //clsThreadProcManage.bIsStepProc = false;

            m_pickPlace = PickPlace.Place;
            m_enuAction = enuAction.Place;
            m_bIsReady = false;
            bIsProcessing = true;
            iStepIndex = 30000;
        }

        public static void GetCellCenterPos(int iRow, int iCol, out double dPosX, out double dPosY)
        {
            // Step1：從 Row0,Col0 的教點，用 Pitch 換算出指定 Row/Col 那顆 Cell 的左上角座標
            double dCellTopLeftX = m_dBaseCellPosX + iCol * m_dCellPitchX;
            double dCellTopLeftY = m_dBaseCellPosY + iRow * m_dCellPitchY;

            // Step2：從左上角推算中心點 (加上 Cell 自身尺寸的一半)
            dPosX = dCellTopLeftX + m_dCellWidth / 2.0;
            dPosY = dCellTopLeftY + m_dCellLength / 2.0;
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

                #region 【初始化開始】重新初始化 Arm 硬體設備

                case 10000:
                    InitialArmHardware();

                    if (b_Simulation)
                    {
                        SetDi(m_DI_Vacuum, false);
                    }

                    iStepIndex = 10100;
                    break;

                #endregion

                #region 【有料檢查】檢查是否吸嘴上有物料

                case 10100:
                    // 【有料檢查】檢查是否吸嘴上有物料
                    if (!GetDi(m_DI_Vacuum) && !AssyRecord.IsExist)
                    {
                        iStepIndex = 10110;
                        break;
                    }
                    else
                    {
                        // 吸嘴上有料，報警通知
                        clsEditRunThread.ReportAlarm(enuAlarm.Arm_Has_Material_On_Suction_Cup);
                        iStepIndex = 10998;
                    }

                    break;

                #endregion

                #region 關閉真空啟動、真空破壞

                case 10110:
                    SetDo(m_DO_Vacuum_Activate, false);
                    SetDo(m_DO_Vacuum_Break, false);
                    iStepIndex = 10200;
                    break;

                #endregion

                #region 【加入 Z 軸 Home 動作】將 Z 軸回原點動作加入控制盒

                case 10200:
                    m_CtrlBox.Clear();
                    m_CtrlBox.Add(m_Motion_Z);
                    iStepIndex = 10210;
                    break;

                #endregion

                #region 【執行 Z 軸 Home】執行 Z 軸回原點動作

                case 10210:
                    m_CtrlBox.Home(ref iStepIndex, 10220, 10998);
                    break;

                #endregion

                #region 【Z 軸 Home 完成】重啟計時器，準備設定 Z 軸位置為 0

                case 10220:
                    Restart();
                    iStepIndex = 10230;
                    break;

                #endregion

                #region 【等待延遲後設定位置】等待 HomeDelay 時間後，將 Z 軸位置歸零

                case 10230:
                    if (IsTimeOut(m_iHomeDelay, clsCmData.enuSecUnit.MilliSec))
                    {
                        clsMotionCtrl.SetPos(m_Motor_Z, 0);
                        iStepIndex = 10300;
                    }

                    break;

                #endregion

                #region 【加入 XY 軸 Home 動作】將 XY 軸回原點動作加入控制盒

                case 10300:
                    m_CtrlBox.Clear();
                    m_CtrlBox.Add(m_Motion_X);
                    m_CtrlBox.Add(m_Motion_Y);
                    iStepIndex = 10310;
                    break;

                #endregion

                #region 【執行 XY 軸 Home】執行 XY 軸回原點動作

                case 10310:
                    m_CtrlBox.Home(ref iStepIndex, 10320, 10998);
                    break;

                #endregion

                #region 【XY 軸 Home 完成】重啟計時器，準備設定 XY 軸位置為 0

                case 10320:
                    Restart();
                    iStepIndex = 10330;
                    break;

                #endregion

                #region 【等待延遲後設定位置】等待 HomeDelay 時間後，將 XY 軸位置歸零

                case 10330:
                    if (IsTimeOut(m_iHomeDelay, clsCmData.enuSecUnit.MilliSec))
                    {
                        clsMotionCtrl.SetPos(m_Motor_X, 0);
                        clsMotionCtrl.SetPos(m_Motor_Y, 0);
                        iStepIndex = 10999;
                    }

                    break;

                #endregion

                #region 【初始化失敗】設定狀態，結束流程

                case 10998:
                    m_enuAction = enuAction.Initial_Fail;
                    m_bIsReady = false;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                #endregion

                #region 【 初始化完成】Arm 已就緒，可以開始 Pick / Place

                case 10999:
                    if (b_Simulation)
                    {
                        SetDi(m_DI_Vacuum, false);
                    }

                    m_enuAction = enuAction.Initial_Done;
                    m_bIsReady = true;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                #endregion

                #endregion

                #region //===================== Pick 取料主流程 (20000-20999) =====================

                #region 【Pick 流程開始】檢查是否吸嘴上有物料

                case 20000:
                    if (!AssyRecord.IsExist)
                    {
                        m_enuAction = enuAction.Pick_Waiting;
                        iStepIndex = 20100;
                        break;
                    }
                    else
                    {
                        // 吸嘴上有料，報警通知
                        clsEditRunThread.ReportAlarm(enuAlarm.Arm_Has_Material_On_Suction_Cup);
                    }

                    break;

                #endregion

                //馬達Z軸設定安全位置
                case 20100:
                    m_CtrlBox.Clear();
                    SetAddMotorZMoveAbsolute(m_dSafePos_Z);
                    iStepIndex = 20110;
                    break;

                //馬達移動安全位置
                case 20110:
                    m_CtrlBox.Action(ref iStepIndex, 20200, 20998);
                    break;

                //等待取料流道是否準備完成，判斷有無料盤
                case 20200:
                    m_enuAction = enuAction.Pick_Waiting;
                    iStepIndex = ReadyToPick() ? 20300 : 20998;
                    break;

                //計算點位，馬達X/Y設定取料位置
                case 20300:
                    GetCellCenterPos(m_iPickColumn, m_iPickRow, out double posX, out double posY);
                    m_CtrlBox.Clear();
                    SetAddMotorXMoveAbsolute(posX);
                    SetAddMotorYMoveAbsolute(posY);

                    iStepIndex = 20310;
                    break;

                //馬達X/Y軸移動取料位置
                case 20310:
                    m_enuAction = enuAction.Pick_Moving;
                    m_CtrlBox.Action(ref iStepIndex, 20400, 20998);
                    break;

                //馬達Z軸設定取料位置
                case 20400:
                    m_CtrlBox.Clear();

                    //todo: 需要再處理站別轉點位
                    var posZ = ucPosPmt.GetValueDouble(enuPosName.PickPos_HSLane_Z);
                    SetAddMotorZMoveAbsolute(posZ);
                    iStepIndex = 20410;
                    break;

                //馬達Z軸移動取料位置
                case 20410:
                    m_CtrlBox.Action(ref iStepIndex, 20500, 20998);
                    break;

                //吸嘴真空開啟
                case 20500:
                    SetDo(m_DO_Vacuum_Activate, true);
                    iStepIndex = 20510;
                    break;

                //模擬時設定真空檢知On
                case 20510:

                    if (b_Simulation)
                    {
                        SetDi(m_DI_Vacuum, true);
                    }

                    Restart();
                    iStepIndex = 20520;
                    break;

                //判斷取料位置吸嘴真空狀態
                case 20520:
                    if (GetDi(m_DI_Vacuum))
                    {
                        iStepIndex = 20600;
                    }

                    if (IsTimeOut(m_VacuumDelay, clsCmData.enuSecUnit.MilliSec))
                    {
                        clsEditRunThread.ReportAlarm(enuAlarm.Pickup_Vacuum_Failure);
                        iStepIndex = 20998;
                    }

                    break;

                //馬達Z軸設定安全位置
                case 20600:
                    m_CtrlBox.Clear();
                    SetAddMotorZMoveAbsolute(m_dSafePos_Z);
                    iStepIndex = 20610;
                    break;

                //馬達Z軸移動到安全位置
                case 20610:
                    m_CtrlBox.Action(ref iStepIndex, 20620, 20998);
                    Restart();
                    break;

                //判斷安全位置吸嘴真空狀態
                case 20620:
                    if (GetDi(m_DI_Vacuum))
                    {
                        iStepIndex = 20700;
                    }

                    if (IsTimeOut(m_VacuumDelay, clsCmData.enuSecUnit.MilliSec))
                    {
                        clsEditRunThread.ReportAlarm(enuAlarm.Pickup_Vacuum_Failure);
                    }

                    break;

                //過帳
                case 20700:
                    TransferToArm();
                    iStepIndex = 20999;
                    break;

                // 【Pick 失敗】取料流程失敗
                case 20998:
                    m_enuAction = enuAction.Pick_Fail;
                    m_bIsReady = false;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                case 20999:
                    m_enuAction = enuAction.Pick_Done;
                    m_bIsReady = true;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                #endregion

                #region //===================== Place 放料主流程 (30000-30999) =====================

                // 【Place 流程開始】檢查手臂上有無帳
                case 30000:
                    m_enuAction = enuAction.Place;

                    if (!AssyRecord.IsExist)
                    {
                        clsEditRunThread.ReportAlarm(enuAlarm.Place_Suction_Cup_No_Material);
                        iStepIndex = 30998;
                    }
                    else
                    {
                        iStepIndex = 30100;
                    }

                    break;

                //馬達Z軸設定安全位置
                case 30100:
                    m_CtrlBox.Clear();
                    SetAddMotorZMoveAbsolute(m_dSafePos_Z);
                    iStepIndex = 30110;
                    break;

                //馬達移動安全位置
                case 30110:
                    m_CtrlBox.Action(ref iStepIndex, 30200, 30998);
                    break;

                //等待放料流道是否準備完成，判斷有無料盤
                case 30200:
                    m_enuAction = enuAction.Place_Waiting;
                    iStepIndex = ReadyToPlace() ? 30300 : 30998;
                    break;

                //計算點位，馬達X/Y設定放料位置
                case 30300:
                    GetCellCenterPos(m_iPlaceColumn, m_iPlaceRow, out double placeX, out double placeY);
                    m_CtrlBox.Clear();
                    SetAddMotorXMoveAbsolute(placeX);
                    SetAddMotorYMoveAbsolute(placeY);

                    iStepIndex = 30310;
                    break;

                //馬達X/Y軸移動放料位置
                case 30310:
                    m_enuAction = enuAction.Placing;
                    m_CtrlBox.Action(ref iStepIndex, 30400, 30998);
                    break;

                //馬達Z軸設定放料位置
                case 30400:
                    m_CtrlBox.Clear();

                    //todo: 需要再處理站別轉點位
                    var placePosZ = ucPosPmt.GetValueDouble(enuPosName.PlacePos_HSLane_Z);
                    SetAddMotorZMoveAbsolute(placePosZ);
                    iStepIndex = 30410;
                    break;

                //馬達Z軸移動放料位置
                case 30410:
                    m_CtrlBox.Action(ref iStepIndex, 30500, 30998);
                    break;

                //吸嘴真空破壞開啟
                case 30500:
                    SetDo(m_DO_Vacuum_Activate, false);
                    SetDo(m_DO_Vacuum_Break, true);
                    Restart();
                    iStepIndex = 30510;
                    break;

                //等待吸嘴真空破壞時間
                case 30510:

                    if (IsTimeOut(m_dAirBlowTime, clsCmData.enuSecUnit.MilliSec))
                    {
                        SetDo(m_DO_Vacuum_Break, false);
                        iStepIndex = 30600;
                    }

                    break;

                //馬達Z軸設定安全位置
                case 30600:
                    m_CtrlBox.Clear();
                    SetAddMotorZMoveAbsolute(m_dSafePos_Z);
                    iStepIndex = 30610;
                    break;

                //馬達Z軸移動到安全位置
                case 30610:
                    m_CtrlBox.Action(ref iStepIndex, 30700, 30998);
                    Restart();
                    break;

                // 模擬時設定真空檢知Off
                case 30700:
                    if (b_Simulation)
                    {
                        SetDi(m_DI_Vacuum, false);
                    }

                    iStepIndex = 30710;
                    break;

                //檢查吸嘴檢知
                case 30710:
                    if (!GetDi(m_DI_Vacuum))
                    {
                        iStepIndex = 30800;
                    }
                    else
                    {
                        clsEditRunThread.ReportAlarm(enuAlarm.Place_Suction_Cup_Has_Material);
                        iStepIndex = 30998;
                    }

                    break;

                //過帳
                case 30800:
                    TransferToLane();
                    iStepIndex = 30999;
                    break;

                // 【Place 失敗】放料流程失敗
                case 30998:
                    m_enuAction = enuAction.Place_Fail;
                    m_bIsReady = false;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                // 【Place 完成】放料流程正式完成
                case 30999:
                    m_enuAction = enuAction.Place_Done;
                    m_bIsReady = true;
                    bIsProcessing = false;
                    iStepIndex = -1;
                    break;

                #endregion
            }
        }

        /// <summary>
        /// 判斷取料站有無料，是否可以進行取料動作。
        /// </summary>
        /// <returns></returns>
        protected virtual bool ReadyToPick() => false;

        /// <summary>
        /// 判斷放料站有無料，是否可以進行放料動作。
        /// </summary>
        /// <returns></returns>
        protected virtual bool ReadyToPlace() => false;

        protected abstract void BindHardwarePoint();

        /// <summary>
        /// 初始化 Arm 硬體設備
        /// 此方法負責設定和初始化 Arm 的所有硬體元件
        /// </summary>
        protected void InitialArmHardware()
        {
            // ========== 1.呼叫子類別自訂初始化 ==========
            // 用途：設定該 Arm 專屬的 DI/DO/Port 等參數
            BindHardwarePoint();

            // X 軸初始化
            m_Motion_X.Initial(m_Motor_X, MotionUnit.Minimeter, 10000, "X-Axis Timeout");
            m_Motion_X.iSimulateDelayTime = 500;
            clsMotionCtrl.SetServo(m_Motor_X, true);

            // Y 軸初始化
            m_Motion_Y.Initial(m_Motor_Y, MotionUnit.Minimeter, 10000, "Y-Axis Timeout");
            m_Motion_Y.iSimulateDelayTime = 500;
            clsMotionCtrl.SetServo(m_Motor_Y, true);

            // Z 軸初始化
            m_Motion_Z.Initial(m_Motor_Z, MotionUnit.Minimeter, 10000, "Z-Axis Timeout");
            m_Motion_Z.iSimulateDelayTime = 500;
            clsMotionCtrl.SetServo(m_Motor_Z, true);


            if (b_Simulation)
            {
                SetDi(m_DI_Vacuum, false);
            }
        }

        protected abstract BaseLane GetPickLane();
        protected abstract BaseLane GetPlaceLane();

        protected void TransferToArm()
        {
            var pickLane = GetPickLane();
            if (pickLane == null)
                return;

            var index = pickLane.m_Temp_Tray_Info.GetIndexFromRowCol(m_iPickRow, m_iPickColumn);
            pickLane.m_Temp_Tray_Info.AssyRecords[index].CopyTo(AssyRecord);
            AssyRecord.IsExist = true;
            pickLane.m_Temp_Tray_Info.SetItemStatus(index, TrayItemStatus.Empty);
        }

        protected abstract void TransferToLane();

        #endregion

        #region Private Methods

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

        private BaseLane GetDestinationLane()
        {
            BaseLane lane = null;
            switch (m_enuPPStation)
            {
                case PPStation.IC:
                    lane = PlaceLane;
                    break;
                case PPStation.HeatSink:
                    lane = PickLane;
                    break;
                case PPStation.OK:
                case PPStation.NG:
                    lane = PlaceLane;
                    break;
            }

            return lane;
        }

        #endregion

        #region ===================== 控制盒 =====================

        /// <summary>
        /// 控制盒：把多個氣缸 / 軸控動作加進來後，用 Action() 等待完成
        /// </summary>
        protected clsControlBox m_CtrlBox = new clsControlBox();

        /// <summary>
        /// 控制元件馬達X
        /// </summary>
        protected clsBoxMotion m_Motion_X = new clsBoxMotion();

        /// <summary>
        /// 控制元件馬達Y
        /// </summary>
        protected clsBoxMotion m_Motion_Y = new clsBoxMotion();

        /// <summary>
        /// 控制元件馬達Z
        /// </summary>
        protected clsBoxMotion m_Motion_Z = new clsBoxMotion();

        #endregion

        #region ===================== Axis / Pos / IO =====================

        /// <summary>
        /// 手臂馬達X
        /// </summary>
        protected enuAxis m_Motor_X;

        /// <summary>
        /// 手臂馬達Y
        /// </summary>
        protected enuAxis m_Motor_Y;

        /// <summary>
        /// 手臂馬達Z
        /// </summary>
        protected enuAxis m_Motor_Z;

        /// <summary>
        /// 真空檢知DI
        /// </summary>
        protected enuDi m_DI_Vacuum;

        /// <summary>
        /// 真空啟動DO
        /// </summary>
        protected enuDo m_DO_Vacuum_Activate;

        /// <summary>
        /// 真空破壞DO
        /// </summary>
        protected enuDo m_DO_Vacuum_Break;

        #endregion

        #region ===================== 狀態變數 =====================

        /// <summary>目前動作，AR / UI 透過此觀察流程狀態。</summary>
        public enuAction m_enuAction { get; protected set; }

        /// <summary>
        /// 是否模擬
        /// </summary>
        /// <returns></returns>
        private bool b_Simulation => PublicDeclare.bIsSimulate;

        #region ===================== 帳料 =====================

        /// <summary>
        /// Lane 目前持有的 Tray 帳。
        /// Magazine 推料完成後，帳會先複製到這裡。
        /// </summary>
        public clsTrayInfo m_Temp_Tray_Info = new clsTrayInfo();

        /// <summary>
        /// 防止同一次 Load_Done 重複通知上一站清帳。
        /// </summary>
        private bool m_bNotifyPreviousLoadDone = false;

        #endregion

        #endregion
    }
}
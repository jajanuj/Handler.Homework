using System.ComponentModel;

namespace ArtData
{
    /// <summary> 與公司底層共用的Enum定義 </summary>
    public class clsEnum
    {
        public enum WorkStationType
        {
            /// <summary> 尚未進入任何站別 </summary>
            None,
            /// <summary> 上料站 </summary>
            Load,
            /// <summary> 組裝站 </summary>
            ASM,
            /// <summary> 壓合站 </summary>
            Press,
            /// <summary> AOI 檢測站 </summary>
            AOI,
            Ng
        }

        public enum AoiResult
        {
            None = 0,
            Ok,
            Ng,
        }

        /// <summary> NG 出料模式 </summary>
        public enum enuNGDischargeMode
        {
            /// <summary> 即時出料：這一輪 OK_Lane 分料結束就出 </summary>
            Immediate,

            /// <summary> 滿盤出料：NG_Lane 自己收滿整盤才出 </summary>
            FullTray
        }

        public enum PPStation
        {
            None,
            IC = 1,
            HeatSink = 2,
            OK = 3,
            NG
        }

        public enum MagazineType
        {
            Load,
            Unload
        }

        public enum MaterialType
        {
            IC,
            HeatSink,
            Empty
        }

        /// <summary> 移動模式 </summary>
        public enum MoveMode
        {
            RelativeMode_Mm = 0,
            ContinueMode = 1,
            AbsMoveMode_Mm = 2,
        }

        /// <summary> 
        /// Tray 盤內單一物件狀態
        /// </summary>
        public enum TrayItemStatus
        {
            /// <summary> 未執行 - 藍色 </summary>
            Pending,
            /// <summary> 良品 - 綠色 </summary>
            OK,
            /// <summary> 不良品 - 紅色 </summary>
            NG,
            /// <summary> 無料 - 灰色 </summary>
            Empty,
            /// <summary> 組裝中 - 橘色 </summary>
            Assembly,
            /// <summary> 載板 - 藍紫色 </summary>
            Substrate,
            /// <summary> 散熱片 - 青色 </summary>
            HeatSink,
            /// <summary> 已壓合 - 棕色 </summary>
            Pressed,
            /// <summary> 已完成 AOI 檢測 - 粉紅色 </summary>
            AoiInspected
        }

        #region Alarm 代碼
        /// <summary> Alarm 代碼 (ArtEQ.dll) ///</summary>
        public enum enuAlarm
        {
            [Description("未定義錯誤碼")] Error_Code_Undifine = 0,


            #region //Level 2 Alarm (System Down, 掛機) - 第一個數值為9

            //Alarm Code 第一個數值為9 ,clsAlarmCodeBuilder 會自行定義成 Level 2 的 Alarm, 索引 : @AlarmCodeBuilder-LV2@

            [Description("機台異常(EMO)觸發")] Machine_Error_EMO_Occured = 900001, //LV2 (Level 2 , ReportAlarm時 機台狀態會被強制為Down)
            [Description("機台異常-電源訊號錯誤")] Machine_Error_Power_Error = 900002, //LV2 (Level 2 , ReportAlarm時 機台狀態會被強制為Down)

            #endregion

            //異常代碼 1XXXXX 保留給PM/AP模組

            [Description("加工異常,產品不存在")] ProcArm_LaneDataNotExist = 200001,

            #region //異常代碼 (設備異常) 890XXX

            [Description("初始化異常")] Initial_Fail = 890000,
            [Description("請先執行初始化")] Please_Initial_Before_Any_Action = 890001,
            [Description("單動模式已啟動,請先執行初始化")] Please_Initial_After_Manual_Test = 890002,
            [Description("自動模式已啟動,無法執行單動流程")] AutoRun_Mode_Active_Cannot_Manual_Action = 890003,

            [Description("機台異常-安全門開啟")] Machine_Error_SafeDoor_Open = 890101,
            [Description("機台異常-觸碰光柵")] Machine_Error_LightGate_Detect = 890102,
            [Description("機台異常-氣壓來源不足")] Machine_Error_Air_Pressure_Low = 890103,

            #endregion

            #region //異常代碼 (馬達移動超時) 891XXX

            [Description("馬達移動超時(料盒手臂Z)")] Motion_Timeout_MagazineArmZ = 891001,
            [Description("馬達移動超時(料盒手臂上Y)")] Motion_Timeout_MagazineArm_UpY = 891002,
            [Description("馬達移動超時(料盒手臂中Y)")] Motion_Timeout_MagazineArm_MiddleY = 891003,
            [Description("馬達移動超時(料盒手臂下Y)")] Motion_Timeout_MagazineArm_DownY = 891004,
            [Description("馬達移動超時(切換流道Y軸)")] Motion_Timeout_LaneChange_Y = 891005,
            [Description("馬達移動超時(切換流道推桿X軸)")] Motion_Timeout_LaneChange_PusherX = 891006,
            [Description("馬達移動超時(Axis7)")] Motion_Timeout_Axis7 = 891007,
            [Description("馬達移動超時(Axis8)")] Motion_Timeout_Axis8 = 891008,
            [Description("馬達移動超時(Axis9)")] Motion_Timeout_Axis9 = 891009,
            [Description("馬達移動超時(Axis10)")] Motion_Timeout_Axis10 = 891010,
            [Description("馬達移動超時(Axis11)")] Motion_Timeout_Axis11 = 891011,
            [Description("馬達移動超時(Axis12)")] Motion_Timeout_Axis12 = 891012,
            [Description("馬達移動超時(Axis13)")] Motion_Timeout_Axis13 = 891013,
            [Description("馬達移動超時(Axis14)")] Motion_Timeout_Axis14 = 891014,
            [Description("馬達移動超時(Axis15)")] Motion_Timeout_Axis15 = 891015,
            [Description("馬達移動超時(Axis16)")] Motion_Timeout_Axis16 = 891016,
            [Description("馬達移動超時(Axis17)")] Motion_Timeout_Axis17 = 891017,
            [Description("馬達移動超時(Axis18)")] Motion_Timeout_Axis18 = 891018,
            [Description("馬達移動超時(Axis19)")] Motion_Timeout_Axis19 = 891019,
            [Description("馬達移動超時(Axis20)")] Motion_Timeout_Axis20 = 891020,
            [Description("馬達移動超時(Axis21)")] Motion_Timeout_Axis21 = 891021,
            [Description("馬達移動超時(Axis22)")] Motion_Timeout_Axis22 = 891022,
            [Description("馬達移動超時(Axis23)")] Motion_Timeout_Axis23 = 891023,
            [Description("馬達移動超時(Axis24)")] Motion_Timeout_Axis24 = 891024,
            [Description("馬達移動超時(Axis25)")] Motion_Timeout_Axis25 = 891025,
            [Description("馬達移動超時(Axis26)")] Motion_Timeout_Axis26 = 891026,
            [Description("馬達移動超時(Axis27)")] Motion_Timeout_Axis27 = 891027,
            [Description("馬達移動超時(Axis28)")] Motion_Timeout_Axis28 = 891028,
            [Description("馬達移動超時(Axis29)")] Motion_Timeout_Axis29 = 891029,
            [Description("馬達移動超時(Axis30)")] Motion_Timeout_Axis30 = 891030,
            [Description("馬達移動超時(Axis31)")] Motion_Timeout_Axis31 = 891031,
            [Description("馬達移動超時(Axis32)")] Motion_Timeout_Axis32 = 891032,
            [Description("馬達移動超時(Axis33)")] Motion_Timeout_Axis33 = 891033,
            [Description("馬達移動超時(Axis34)")] Motion_Timeout_Axis34 = 891034,
            [Description("馬達移動超時(Axis35)")] Motion_Timeout_Axis35 = 891035,
            [Description("馬達移動超時(Axis36)")] Motion_Timeout_Axis36 = 891036,
            [Description("馬達移動超時(Axis37)")] Motion_Timeout_Axis37 = 891037,
            [Description("馬達移動超時(Axis38)")] Motion_Timeout_Axis38 = 891038,
            [Description("馬達移動超時(Axis39)")] Motion_Timeout_Axis39 = 891039,
            [Description("馬達移動超時(Axis40)")] Motion_Timeout_Axis40 = 891040,
            [Description("馬達移動超時(Axis41)")] Motion_Timeout_Axis41 = 891041,
            [Description("馬達移動超時(Axis42)")] Motion_Timeout_Axis42 = 891042,
            [Description("馬達移動超時(Axis43)")] Motion_Timeout_Axis43 = 891043,
            [Description("馬達移動超時(Axis44)")] Motion_Timeout_Axis44 = 891044,
            [Description("馬達移動超時(Axis45)")] Motion_Timeout_Axis45 = 891045,
            [Description("馬達移動超時(Axis46)")] Motion_Timeout_Axis46 = 891046,
            [Description("馬達移動超時(Axis47)")] Motion_Timeout_Axis47 = 891047,
            [Description("馬達移動超時(Axis48)")] Motion_Timeout_Axis48 = 891048,
            [Description("馬達移動超時(Axis49)")] Motion_Timeout_Axis49 = 891049,
            [Description("馬達移動超時(Axis50)")] Motion_Timeout_Axis50 = 891050,

            #endregion

            #region //異常代碼 (汽缸動作超時) 892XXX

            [Description("汽缸動作超時-流道到位阻擋汽缸下降")] Cylinder_Timeout_Lane_StopperDown = 892010,
            [Description("汽缸動作超時-流道到位阻擋汽缸上升")] Cylinder_Timeout_Lane_StopperUp = 892011,

            [Description("汽缸動作超時-流道側推縮回")] Cylinder_Timeout_Lane_SidePusher_Back = 892020,
            [Description("汽缸動作超時-流道側推伸出")] Cylinder_Timeout_Lane_SidePusher_Extend = 892021,

            [Description("汽缸動作超時-流道入料動力壓輪縮回")] Cylinder_Timeout_Lane_LoadPowerWheel_Back = 892030,
            [Description("汽缸動作超時-流道入料動力壓輪伸出")] Cylinder_Timeout_Lane_LoadPowerWheel_Extend = 892031,

            [Description("汽缸動作超時-流道出料動力壓輪縮回")] Cylinder_Timeout_Lane_UnloadPowerWheel_Back = 892040,
            [Description("汽缸動作超時-流道出料動力壓輪伸出")] Cylinder_Timeout_Lane_UnloadPowerWheel_Extend = 892041,
            Lane_Stopper_Extend_Timeout,
            Lane_Stopper_Retract_Timeout,
            Lane_Align_Extend_Timeout,
            Lane_Align_Retract_Timeout,

            #endregion

            #region //異常代碼 (Process異常) 893XXX

            [Description("需要空料盒負責收料")] Empty_Magazine = 893001,

            [Description("氣缸動作超時-流道出料")] Cylinder_Timeout_Lane_Unload = 893032,
            [Description("氣缸動作超時-流道入料")] Cylinder_Timeout_Lane_Load = 893033,

            [Description("流道出料異常,產品已經存在")] Cannot_transfer_Downstream_Lane_Already_Has_Tray,

            [Description("產品料盒未替換超時")] Magazine_Load_OK_Tray_Timeout = 893101,
            [Description("需要上料盒")] Need_Magazine_To_Load,

            [Description("過壓警報")] Over_Press_Alarm,

            [Description("手臂吸嘴上有物料")] Arm_Has_Material_On_Suction_Cup = 893201,

            [Description("手臂吸嘴真空失敗")] Pickup_Vacuum_Failure,

            [Description("手臂放料吸嘴無料")] Place_Suction_Cup_No_Material = 893301,

            [Description("手臂放料吸嘴有料")] Place_Suction_Cup_Has_Material,

            [Description("壓合站初始化時有料")] Press_Init_Not_Empty = 893401,

            [Description("壓合站壓合前無料")] Press_Is_Empty,

            [Description("AOI站檢測前無料")] AOI_Is_Empty = 893501,


            [Description("LOT 結批完畢")] LOT_batch_completed = 893901,

            #endregion
        }
        #endregion

        #region AxisID[1~15]

        /// <summary> AxisID[1~15] (ArtControlLib.dll) </summary>
        public enum enuAxis
        {
            /// <summary>
            /// IC入料料盒Z軸
            /// </summary>
            IC_Feed_Mag_Z,

            /// <summary>
            /// 散熱片入料料盒Z軸
            /// </summary>
            HS_Feed_Mag_Z,

            /// <summary>
            /// 組裝手臂X軸
            /// </summary>
            ASM_Arm_X,

            /// <summary>
            /// 組裝手臂Y軸
            /// </summary>
            ASM_Arm_Y,

            /// <summary>
            /// 組裝手臂Z軸
            /// </summary>
            ASM_Arm_Z,

            /// <summary>
            /// 收料手臂X軸
            /// </summary>
            Sort_Arm_X,

            /// <summary>
            /// 收料手臂Y軸
            /// </summary>
            Sort_Arm_Y,

            /// <summary>
            /// 收料手臂Z軸
            /// </summary>
            Sort_Arm_Z,

            /// <summary>
            /// AOI手臂X軸
            /// </summary>
            AOI_Arm_X,

            /// <summary>
            /// AOI手臂Y軸
            /// </summary>
            AOI_Arm_Y,

            /// <summary>
            /// AOI手臂Z軸
            /// </summary>
            AOI_Arm_Z,

            /// <summary>
            /// 散熱片出料料盒Z軸
            /// </summary>
            HS_Discharge_Mag_Z,

            /// <summary>
            /// NG入料料盒Z軸
            /// </summary>
            NG_Feed_Mag_Z,

            /// <summary>
            /// OK出料料盒Z軸
            /// </summary>
            OK_Discharge_Mag_Z,

            /// <summary>
            /// NG出料料盒Z軸
            /// </summary>
            NG_Discharge_Mag_Z,
        }

        #endregion

        #region DI-ID[[100~131]~[300~331]]

        /// <summary> DI-ID[[100~131]~[300~331]] (ArtControlLib.dll)  </summary>
        public enum enuDi
        {
            #region DI100-DI131

            /// <summary>電源按鈕</summary>
            Button_Power = 100,

            /// <summary>啟動按鈕</summary>
            Button_Start,

            /// <summary>停止按鈕</summary>
            Button_Stop,

            /// <summary>復歸按鈕</summary>
            Button_Reset,

            /// <summary>緊急停止 (B接) (4串)</summary>
            EMO_B,

            /// <summary>總正壓檢知 (B接) </summary>
            Air_Source_B,

            /// <summary>安全門檢知 (B接) (4串)</summary>
            SafeDoor_B,
            DI107,

            DI108,
            DI109,
            DI110,
            DI111,

            DI112,
            DI113,
            DI114,
            DI115,

            /// <summary>IC入料料盒 推桿氣缸前進</summary>
            IC_Feed_Mag_Push_Fwd,

            /// <summary>IC入料料盒 推桿氣缸後退</summary>
            IC_Feed_Mag_Push_Bwd,

            /// <summary>IC入料料盒 過壓檢知(B)</summary>
            IC_Feed_Mag_Over_Press_B,

            /// <summary>IC入料料盒 在籍檢知</summary>
            IC_Feed_Mag_Present,

            /// <summary>散熱片入料料盒 推桿氣缸前進</summary>
            HS_Feed_Mag_Push_Fwd,

            /// <summary>散熱片入料料盒 推桿氣缸後退</summary>
            HS_Feed_Mag_Push_Bwd,

            /// <summary>散熱片入料料盒 過壓檢知(B)</summary>
            HS_Feed_Mag_Over_Press_B,

            /// <summary>散熱片入料料盒 在籍檢知</summary>
            HS_Feed_Mag_Present,

            /// <summary>組裝流道 入料檢知(B)</summary>
            ASM_Lane_Load_B,

            /// <summary>組裝流道 減速檢知(B)</summary>
            ASM_Lane_Slow_B,

            /// <summary>組裝流道 到位檢知(B)</summary>
            ASM_Lane_Arrival_B,
            DI127,

            /// <summary>壓合流道 入料檢知(B)</summary>
            Press_Lane_Load_B,

            /// <summary>壓合流道 減速檢知(B)</summary>
            Press_Lane_Slow_B,

            /// <summary>壓合流道 到位檢知(B)</summary>
            Press_Lane_Arrival_B,
            DI131,

            #endregion

            #region DI200-DI231

            /// <summary>AOI流道 入料檢知(B)</summary>
            AOI_Lane_Load_B = 200,

            /// <summary>AOI流道 減速檢知(B)</summary>
            AOI_Lane_Slow_B,

            /// <summary>AOI流道 到位檢知(B)</summary>
            AOI_Lane_Arrival_B,
            DI203,

            /// <summary>OK流道 入料檢知(B)</summary>
            OK_Lane_Load_B,

            /// <summary>OK流道 減速檢知(B)</summary>
            OK_Lane_Slow_B,

            /// <summary>OK流道 到位檢知(B)</summary>
            OK_Lane_Arrival_B,
            DI207,

            /// <summary>組裝流道 檔料氣缸前進</summary>
            ASM_Lane_Stopper_Fwd,

            /// <summary>組裝流道 檔料氣缸後退</summary>
            ASM_Lane_Stopper_Bwd,

            /// <summary>壓合流道 檔料氣缸前進</summary>
            Press_Lane_Stopper_Fwd,

            /// <summary>壓合流道 檔料氣缸後退</summary>
            Press_Lane_Stopper_Bwd,

            /// <summary>AOI流道 檔料氣缸前進</summary>
            AOI_Lane_Stopper_Fwd,

            /// <summary>AOI流道 檔料氣缸後退</summary>
            AOI_Lane_Stopper_Bwd,

            /// <summary>OK流道 檔料氣缸前進</summary>
            OK_Lane_Stopper_Fwd,

            /// <summary>OK流道 檔料氣缸後退</summary>
            OK_Lane_Stopper_Bwd,

            /// <summary>壓合氣缸前進</summary>
            Press_Fwd,

            /// <summary>壓合氣缸後退</summary>
            Press_Bwd,

            /// <summary>壓合過壓檢知(B)</summary>
            Press_Over_Press_B,
            DI219,

            /// <summary>散熱片流道 入料檢知(B)</summary>
            HS_Lane_Load_B,

            /// <summary>散熱片流道 減速檢知(B)</summary>
            HS_Lane_Slow_B,

            /// <summary>散熱片流道 到位檢知(B)</summary>
            HS_Lane_Arrival_B,
            DI223,

            /// <summary>散熱片流道 檔料氣缸前進</summary>
            HS_Lane_Stopper_Fwd,

            /// <summary>散熱片流道 檔料氣缸後退</summary>
            HS_Lane_Stopper_Bwd,

            /// <summary>散熱片出料 料盒在籍檢知</summary>
            HS_Discharge_Mag_Present,
            DI227,

            /// <summary>組裝手臂 吸嘴真空檢知</summary>
            ASM_Arm_Vacuum,
            DI229,
            DI230,
            DI231,

            #endregion

            #region DI300-DI331

            /// <summary>NG入料 料盒在籍檢知</summary>
            NG_Feed_Mag_Present = 300,

            /// <summary>NG入料 過壓檢知(B)</summary>
            NG_Feed_Mag_Over_Press_B,

            /// <summary>NG入料 推桿氣缸前進</summary>
            NG_Feed_Mag_Push_Fwd,

            /// <summary>NG入料 推桿氣缸後退</summary>
            NG_Feed_Mag_Push_Bwd,

            /// <summary>NG流道 入料檢知(B)</summary>
            NG_Lane_Load_B,

            /// <summary>NG流道 減速檢知(B)</summary>
            NG_Lane_Slow_B,

            /// <summary>NG流道 到位檢知(B)</summary>
            NG_Lane_Arrival_B,
            DI307,

            /// <summary>NG流道 檔料氣缸前進</summary>
            NG_Lane_Stopper_Fwd,

            /// <summary>NG流道 檔料氣缸後退</summary>
            NG_Lane_Stopper_Bwd,
            DI310,
            DI311,

            /// <summary>分料手臂 吸嘴真空檢知</summary>
            Sort_Arm_Vacuum,
            DI313,
            DI314,
            DI315,

            /// <summary>OK出料 料盒在籍檢知</summary>
            OK_Discharge_Mag_Present,

            /// <summary>NG出料 料盒在籍檢知</summary>
            NG_Discharge_Mag_Present,
            DI318,
            DI319,
            DI320,
            DI321,
            DI322,
            DI323,
            DI324,
            DI325,
            DI326,
            DI327,
            DI328,
            DI329,
            DI330,
            DI331,

            #endregion
        }

        #endregion

        #region DO-ID[[100~131]]

        /// <summary> DO-ID[[100~131]] (ArtControlLib.dll)  </summary>
        public enum enuDo
        {
            #region //DO100 ~ DO131

            /// <summary>啟動按鈕燈</summary>
            Button_Start_Light = 100,

            /// <summary>停止按鈕燈</summary>
            Button_Stop_Light,

            /// <summary>復歸按鈕燈</summary>
            Button_Reset_Light,

            /// <summary>安全門鎖定</summary>
            Safety_Door_Lock,

            /// <summary>警報燈_紅色</summary>
            Signal_Red,

            /// <summary>警報燈_黃色</summary>
            Signal_Yellow,

            /// <summary>警報燈_綠色</summary>
            Signal_Green,

            /// <summary>蜂鳴器</summary>
            Signal_Buzzer,

            /// <summary>IC入料料盒 推桿氣缸</summary>
            IC_Feed_Mag_Push,

            /// <summary>散熱片入料料盒 推桿氣缸</summary>
            HS_Feed_Mag_Push,

            /// <summary>散熱片流道 檔料氣缸</summary>
            HS_Lane_Stopper,
            DO111,

            /// <summary>壓合氣缸</summary>
            Press_Cylinder,
            DO113,
            DO114,
            DO115,

            /// <summary>組裝流道 檔料氣缸</summary>
            ASM_Lane_Stopper,

            /// <summary>壓合流道 檔料氣缸</summary>
            Press_Lane_Stopper,

            /// <summary>AOI流道 檔料氣缸</summary>
            AOI_Lane_Stopper,

            /// <summary>OK流道 檔料氣缸</summary>
            OK_Lane_Stopper,

            /// <summary>組裝手臂 吸嘴真空</summary>
            ASM_Arm_Vacuum,

            /// <summary>組裝手臂 吸嘴破壞</summary>
            ASM_Arm_Air,
            DO122,
            DO123,

            /// <summary>分料手臂 吸嘴真空</summary>
            Sort_Arm_Vacuum,

            /// <summary>分料手臂 吸嘴破壞</summary>
            Sort_Arm_Air,
            DO126,
            DO127,

            /// <summary>NG流道 檔料氣缸</summary>
            NG_Lane_Stopper,

            /// <summary>NG入料料盒 檔料氣缸</summary>
            NG_Feed_Mag_Stopper,
            DO130,
            DO131,

            #endregion
        }

        #endregion

        #region Log Name

        /// <summary> Log Name (ArtCommonLib.dll) </summary>
        public enum enuLogName
        {
            /// <summary> 軟體開啟時系統Initial的Log </summary>
            StartUpLog,

            /// <summary> 系統相關Log </summary>
            SystemLog,

            /// <summary> 使用者登入Log </summary>
            LoginLog,

            /// <summary> 切換RecipeLog </summary>
            RecipeLog,

            /// <summary> 所有Catch都要增加此Log </summary>
            CatchLog,

            /// <summary> 調機Log </summary>
            SetupLog,

            /// <summary> 單動流程開始與結束 </summary>
            ProcessLog,


            AlarmLog,
            ButtonLog,
            TeachLog,
            SECSLog,
            TCPIPLog,
        }

        #endregion

        #region 參數名稱

        /// <summary> 參數名稱 (ArtControlLib.dll) </summary>
        public enum enuPmtName //所有參數的命名都在此(不會分類 System / Recipe)， TeachPos 請宣告在下方的enuPosName位置名稱
        {
            Undefine,

            #region //Recipe_Function (110XXX)

            Rec_NeedEmptyMagazine = 110001,
            Rec_Load_Magazine_Pitch,
            Rec_Cell_Pitch_X,
            Rec_Cell_Pitch_Y,
            Rec_Cell_Width,
            Rec_Cell_Height,

            #endregion

            #region //Sys_Machine (210XXX)

            Sys_MachineDryRun = 210010,
            Sys_TeachEnable = 210011,
            Sys_EnableSafeDoor = 210012,
            Sys_EnableSMEMA = 210030,


            Sys_LaneMotorHighSpeed = 210201,
            Sys_LaneMotorLowSpeed = 210202,
            Sys_Sim_AutoRun,

            #endregion

            #region //Sys_Machine - Heater (215XXX)

            Sys_HeaterShiftOffset1 = 215001,
            Sys_HeaterShiftOffset2 = 215002,
            Sys_HeaterShiftOffset3 = 215003,
            Sys_HeaterShiftOffset4 = 215004,
            Sys_HeaterShiftOffset5 = 215005,
            Sys_HeaterShiftOffset6 = 215006,
            Sys_HeaterShiftOffset7 = 215007,
            Sys_HeaterShiftOffset8 = 215008,
            Sys_HeaterShiftOffset9 = 215009,
            Sys_HeaterShiftOffset10 = 215010,

            #endregion

            #region //Sys_Delay (221XXX)

            Sys_Delay_NoUse = 221000,
            Sys_Delay_AP_SimulateMode = 221001,
            Sys_Delay_LaneArrived = 221002,
            Sys_Delay_SMEMASingal = 221003,
            Sys_Delay_Putter_After,
            Sys_Delay_Putter_Before,

            #endregion

            #region //Sys_Timeout (222XXX)

            Sys_Timeout_NoUse = 222000,
            Sys_Timeout_HandShank = 222001,
            Sys_Timeout_LaneTransfer = 222002,
            Sys_Timeout_ShortCylinder = 222003,
            Sys_Timeout_Putter,
            Lane_Stopper_Extend_Timeout,
            Lane_Stopper_Retract_Timeout,
            Lane_Align_Extend_Timeout,
            Lane_Align_Retract_Timeout,

            #endregion

            #region //Sys_SECS (230XXX)

            Sys_SecsAlarmEnable = 230001,
            Sys_SecsEventEnable = 230002,

            #endregion
        }

        #endregion

        #region 參數種類

        /// <summary> 參數種類 (ArtControlLib.dll) </summary>
        public enum enuPmtType //(每一個參數列舉都有一個獨立的檔案)
        {
            System,   //系統參數
            Recipe,   //Recipe參數
            TeachPos, //點位座標
        }

        #endregion

        #region 位置名稱

        /// <summary> 位置名稱 (ArtTeach.dll) - 以基準點為主(system概念) </summary>
        public enum enuPosName //取得點位座標的參數方式：ucPosPmt.GetValueDouble(enuPosName pPosName);
        {
            SafePos_Z,

            //流道基準點座標 (XYZ) - 有基準點座標才能讓Recipe共用到其他設備上
            BasePos_Lane1_X,
            BasePos_Lane1_Y,
            BasePos_Lane1_Z,

            BasePos_HSLane_X,
            BasePos_HSLane_Y,
            PickPos_HSLane_Z,
            PlacePos_HSLane_Z,

            BasePos_ASMLane_X,
            BasePos_ASMLane_Y,
            PickPos_ASMLane_Z,
            PlacePos_ASMLane_Z,

            AOI_SafePos_Z,
            AOI_BasePos_X,
            AOI_BasePos_Y,
        }

        #endregion

        #region 流程名稱

        /// <summary> 流程名稱 (ArtProcModuleLib.dll) </summary>
        public enum enuProcName
        {
            None,
            PM_SMEMA_Load = 1,
            PM_Lane,
            PM_SMEMA_Unload,
            AP_Lane = 101,
        }

        #endregion

        #region SECS-Enum宣告

        /// <summary>  (ArtEQ.dll)  </summary>
        public enum eSECS_CEID
        {
            Control_state_OFFLINE = 1,
            Control_state_LOCAL = 2,
            Control_state_REMOTE = 3,
            Process_State_Run = 4,
            Process_State_Idle = 5,
            Process_State_Down = 6,
            Process_State_Alarm = 7,
            Changed_Recipe_Complete = 8,
            Changed_Recipe_Fail = 9,
            Equipment_Start = 10,
            Equipment_Stop = 11,
            Magazine_Start = 12,
            Magazine_END = 13,
            AUTOMOVEINPASS = 14,
            AUTOMOVEOUTPASS = 15,
            Magazine1_ID_Read_Successfully = 20,
            Boat1_ID_Read_Successfully = 22,
            Boat2_ID_Read_Successfully = 23,
            Jig1_ID_Read_Successfully = 24,
            Jig2_ID_Read_Successfully = 25,
            Boat1_Unload_Return = 26,
            Boat2_Unload_Return = 27,
            Boat1_Load_Return = 28,
            Boat2_Load_Return = 29,
            Magazine_to_Positioning_Platform = 30,
            Magazine_to_Flip_Hardware = 31,
            Magazine_to_Lift_Platform = 32,
            TransferDatabySubstarate_1 = 51,
            TransferDatabySubstarate_2 = 52,
            Load1_Empty_Return = 115,
            Load1_Full_Return = 116,
            Unload1_Empty_Return = 215,
            Unload1_Full_Return = 216, //MgzDone
            Unload2_Empty_Return = 225,
            Unload2_Full_Return = 226, //MgzDone
        }

        public enum eSECS_SVID
        {
            Control_State = 1,
            Processing_State = 2,
            CLOCK = 3,
            RECIPE_NAME = 4,
            LotID = 5,
            Operation_mode = 11,
            Loader_Magazine_ID = 21,
            Unloader1_Magazine_ID = 31,
            Unloader2_Magazine_ID = 32,
            Boat_ID_1 = 41,
            Boat_ID_2 = 42,
            Jig_ID_1 = 43,
            Jig_ID_2 = 44,
            TransferDatabySubstarate_1 = 51,
            TransferDatabySubstarate_2 = 52,
            Loader_State = 1000,
            Loader_Port1_State = 1011,
            Loader_Port1_magazine_ID = 1012,
            Loader_Port2_State = 1021,
            Loader_Port2_magazine_ID = 1022,
            UnLoader_State = 2000,
            UnLoader_Port1_State = 2011,
            UnLoader_Port1_magazine_ID = 2012,
            UnLoader_Port2_State = 2021,
            UnLoader_Port2_magazine_ID = 2022,
        }

        public enum eSECS_ECID
        {
            LaneMotorHighSpeed = 2001,
            LaneMotorLowSpeed,
        }

        public enum eSECS_DVID
        {
            Unit_Barcode = 3001,
        }

        #endregion
    }
}
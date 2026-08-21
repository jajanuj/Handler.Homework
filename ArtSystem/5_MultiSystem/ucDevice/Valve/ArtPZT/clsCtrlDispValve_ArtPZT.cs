using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtSystem.Files;
using ArtCommonLib;
using ArtControlLib;
using PZT_Algorithm;
using System.Threading;
using Microsoft.Win32;

namespace ArtSystem.MultiSystem
{
    /// <summary> 霧化閥 </summary>
    public class clsCtrlDispValve_ArtPZT : clsCtrlDispValve
    {
        #region//========== Modbus Address ==========
        /// <summary> 參數位址的列舉 (控制器的設定參數) </summary>
        public enum enuModbusAddress
        {
            /// <summary> 韌體版本 (Enum=0)</summary>
            Version = 0,
            /// <summary>  設備編號(Enum=1)</summary>
            ID = 1,
            /// <summary> 壓電控制板的狀態 (Enum=2)</summary>
            Status = 2,
            /// <summary>  異常碼(Enum=3)</summary>
            Error = 3,
            /// <summary> 控制命令 , SetCtrlCommand (Enum=4)</summary>
            Cmd = 4,
            /// <summary> 計數板FW版本 (Enum=5)</summary>
            Counter_Board_FW_Version = 5,
            /// <summary> 單/雙壓電識別碼 (Enum=9)</summary>
            Single_Dual_PZT_ID = 9,
            /// <summary> 撞針上抬量 (Enum=10)</summary>
            Open_Volt = 10,
            /// <summary> 撞針閉鎖量 (Enum=11)</summary>
            Lock_Volt = 11,
            /// <summary> 噴嘴要開啟多久 (Enum=12)</summary>
            Hold_Open_Time = 12,
            /// <summary> 噴嘴閉鎖時間 (Enum=13)</summary>
            Hold_Lock_Time = 13,
            /// <summary> 撞針下衝所需時間 (Enum=14)</summary>
            Lock_Time = 14,
            /// <summary> 撞針上抬所需時間 (Enum=15)</summary>
            Open_Time = 15,
            /// <summary>  (Enum=16)</summary>
            Acc_Rate_Time = 16,
            /// <summary> 點膠次數 (Enum=17)</summary>
            Dot_Amount = 17,
            /// <summary> 點膠模式 , TriggerMode (Enum=18)</summary>
            Action_Mode = 18,
            /// <summary> 閉鎖校正_開始時間 (Enum=19)</summary>
            Gap_Catch_Signal_Time = 19,
            /// <summary> 閉鎖校正_電壓的上限值 (Enum=20)</summary>
            Gap_Volt_Threshold = 20,
            /// <summary> 閉鎖校正_電壓的下限值 (Enum=21)</summary>
            Gap_Volt_Threshold2 = 21,
            /// <summary> 閉鎖校正_目前的Gap電壓 (Enum=22)</summary>
            Gap_Volt = 22,
            /// <summary> 預壓值 (單位:um，未設定上限值，但最大約180um為極限) (Enum=23)</summary>
            Preload = 23,
            ///// <summary> 該次補償所量測到的溫度(測試用) </summary>
            //Comp_Temp = 19,
            ///// <summary> 調整前的溫度(測試用) </summary>
            //Before_Temp = 20,
            ///// <summary> 雙壓電_上升_DAC_Offset(測試用) </summary>
            //Offset_Rising = 21,
            ///// <summary> 雙壓電_下降_DAC_Offset(測試用) </summary>
            //Offset_Falling = 22,
            /// <summary> 膠管壓力值 (Enum=24)</summary>
            Air_Value = 24,
            /// <summary> 開關膠管壓力 (Enum=25)</summary>
            Air_Open = 25,
            /// <summary> 壓電序號 (Enum=26+27)</summary>
            Counter_SN = 26,
            /// <summary> 壓電目前計數值 (Enum=28+29)</summary>
            Counter_NowCount = 28,
            /// <summary> 壓電目前溫度 (Enum=30)</summary>
            Counter_Temp = 30,
            /// <summary> 壓電計數歸零 (Enum=31)</summary>
            Counter_Reset = 31,
            /// <summary> 壓電計數器的狀態 (Enum=32)</summary>
            Counter_Status = 32,
            /// <summary> 壓電計數器全部數值 (Enum=33)</summary>
            Counter_ReadAll = 33,
            /// <summary> 近接開關電壓值 (Enum=34)</summary>
            Counter_SP_Volt = 34,
            /// <summary> 近接開關最小電壓值 (Enum=35)</summary>
            Counter_SP_Volt_Min = 35,
            /// <summary> 近接開關最大電壓值 (Enum=36)</summary>
            Counter_SP_Volt_Max = 36,
            /// <summary> 校正開始指示(1:Start) (Enum=37)</summary>
            Counter_SP_Adj_Start = 37,
            /// <summary> 閉鎖壓電溫度20度的DAC值(測試用) (Enum=38)</summary>
            UP_PZT_1TH_VOLT = 38,
            /// <summary> 閉鎖壓電溫度60度的DAC值(測試用) (Enum=39)</summary>
            UP_PZT_2ND_VOLT = 39,
            /// <summary> 閉鎖壓電的位置 (Enum=40)</summary>
            LockPZT_LockVolt = 40,
            /// <summary> 閉鎖壓電的電壓變化 (Enum=41)</summary>
            LockPZT_RiseVolt = 41,
            /// <summary> 閉鎖壓電的電壓變化周期時間(us) (Enum=42)</summary>
            LockPZT_ACCTime = 42,
            /// <summary> 閉鎖自動校正程序 (Enum=43)</summary>
            Adjust_State = 43,
            /// <summary>  (Enum=44)</summary>
            Lock_PZT_Comp = 44,
            /// <summary> 閉鎖壓電實際DAC值(測試用) (Enum=45)</summary>
            Lock_PZT_Real_volt = 45,
            /// <summary> 補償功能 (Enum=46)</summary>
            Lock_PZT_Comp_Enable = 46,
            /// <summary> 落後補償溫度秒數(測試用) (Enum=47)</summary>
            Lock_PZT_Comp_Temp_Inx = 47,
            /// <summary> 閉鎖壓電溫度20度的位置(測試用) (Enum=48)</summary>
            UP_PZT_1TH_DISTANCE = 48,
            /// <summary> 閉鎖壓電溫度60度的位置(測試用) (Enum=49)</summary>
            UP_PZT_2ND_DISTANCE = 49,
            /// <summary> 點膠次數陣列長度(最多50) (Enum=50)</summary>
            Dot_Amount_BufferLength = 50,
            /// <summary> 點膠次數陣列(從51開始可以使用~100),每筆最大塗膠點數為5000點 (Enum=51)</summary>
            Dot_Amount_Buffer = 51,
            /// <summary> OnflyMode，Trigger到指定次數後進行出膠餘數處理， (Enum=161) </summary>
            ResidueTriggerCount = 161,
            /// <summary> OnflyMode，出膠餘數處理的輸出量， (Enum=162) </summary>
            ResidueOutputCount = 162,
            /// <summary> 膠閥腔體溫度補償值 (Enum=165)</summary>
            Cavity_Compensation_Value = 165,
            /// <summary> 近接開關電壓值儲存筆數(雙壓電) (Enum=179)</summary>
            PS_Voltage_Array_Items = 179,
            /// <summary> 近接開關電壓值(mV)(180~679,500筆)(雙壓電) (Enum=180)</summary>
            PS_Voltage_Array = 180,
            /// <summary> 磁滯曲線點正確性 (Enum=689)</summary>
            PZT_C_Point_Correct = 689,
            /// <summary> 衝擊壓電磁滯曲線點集合(690~730) (Enum=690)</summary>
            PZT_C_Impact_Point = 690,
            /// <summary> 衝擊壓電熱膨脹係數 (Enum=791)</summary>
            PZT_C_Impact_Compensate = 791,
            ///// <summary> 閉鎖壓電磁滯曲線點集合(735~775) </summary>
            //PZT_C_Lock_Point=735,
            ///// <summary> 閉鎖壓電熱膨脹係數 </summary>
            //PZT_C_Lock_Compensate=776,
            /// <summary> 載波頻率,單壓電閉鎖校正用,產生震動 (Enum=792)</summary>
            PZT_Wave_Hz = 792,
            /// <summary> 載波頻率的振幅,單壓電閉鎖校正用,振幅 (Enum=793)</summary>
            PZT_Amplitude = 793,
            /// <summary> 膠閥腔體溫度 (Enum=794)</summary>
            Cavity_Temp = 794,
            /// <summary> 長行程壓電校正後計算得到的開啟100%時的DAC (Enum=797)</summary>
            PZT_Open_Positon_ADC = 797,
            /// <summary> 長行程壓電校正後計算得到的閉鎖0%時的DAC (Enum=798)</summary>
            PZT_Lock_Positon_ADC = 798,
            /// <summary> 雙壓電校正後得到的閉鎖位置um (Enum=799)</summary>
            PZT_Lock_Position_um = 799,
            /// <summary> CMP Trigger次數 (Enum=800)</summary>
            Compare_Trigger_Count = 800,
            /// <summary> 出膠監控相關 (Enum=803)</summary>
            THRESHOLD_VALUE = 803,
            /// <summary> 出膠監控相關 (Enum=806)</summary>
            OPEN_VOLTAGE_M = 806,
            /// <summary> 出膠監控相關 (Enum=808)</summary>
            MON_REF_POINT_INDEX = 808,
            /// <summary> 出膠監控相關 (Enum=560)</summary>
            MON_REF_OPEN_POINT = 560,
            /// <summary> 出膠監控相關 (Enum=620)</summary>
            MON_REF_LOCK_POINT = 620,
        }

        #endregion

        #region//========== Enum 定義 ==========

        /// <summary> 控制命令的列舉(Address:04) </summary>
        public enum enuCommand
        {
            /// <summary> Idle </summary>
            Idle = 0,
            /// <summary> 開始點膠 </summary>
            Trigger = 1,
            /// <summary> 調整模式作動開始 </summary>
            AdjustStart = 2,
            /// <summary> 調整模式作動停止 </summary>
            AdjustStop = 3,
            /// <summary> 緊急停止 </summary>
            EMS = 4,
            /// <summary> 軟體重置 </summary>
            SoftReset = 5,
            /// <summary> 命令PZT上提至原點 </summary>
            PZTLift = 6,//to0Volt
            /// <summary> 命令PZT移動至閉鎖位置 </summary>
            PZTLock = 7,//toLockVolt

            LockPZT_toLock_Volt = 8,
            Rate_Calculate = 10,
            SN_2_CB = 11,
            Impact_Point_2_CB = 12,
            Impact_Compensate_2_CB = 13,
            Lock_Point_2_CB = 14,
            Lock_Compensate_2_CB = 15,

            //出膠監控相關//@1.0.0.56-11-4@
            Read_Ref_Voltage = 31,
            Read_Mon_Voltage = 32,
            Read_Ref_Point = 33,
            Read_Mon_Point = 34,
            //--
        }

        /// <summary> 接受CMP-Trigger的模式(Address:18) (Default : 1 Continue) </summary>
        public enum enuTriggerMode
        {
            None = 0,
            /// <summary> 收到一筆Trigger出膠n點 </summary>
            Continue = 1,
            /// <summary> 變頻模式 </summary>
            OnTheFly = 2,
            /// <summary> 閉鎖校正流程 </summary>
            Adjusting = 3
        }

        // <summary> 閥狀態 String 定義 </summary>
        public enum enuValveStatus
        {
            NoCommunication = -1,
            Ready = 0,
            Busy = 1,
            WireBroken = 2,
            CounterCommunicationError = 3,
            CounterError = 4,
            OverCurrent = 5,
            AmpOverTemp = 6,
            PZTOverTemp = 7,
            /// <summary> 行程監控異常 </summary>
            PZTLockError = 8,
            LockOpen = 20,
        }

        public enum enuLogName
        {
            ValveLockAdjust,
            ValveLockGraph,
        }

        #endregion

        static public List<clsCtrlDispValve_ArtPZT> g_LstDispValvePZT = new List<clsCtrlDispValve_ArtPZT>();
        public clsCtrlDispValve_ArtPZT()
        {
            if (g_LstDispValvePZT.Contains(this) == false)
            {
                g_LstDispValvePZT.Add(this);
            }
        }

        static public clsCtrlDispValve_ArtPZT GetCtrlPzt(int p_iIndex)
        {
            clsCtrlDispValve_ArtPZT rValue = null;
            try
            {
                if (p_iIndex >= 0 && p_iIndex < g_LstDispValvePZT.Count)
                {
                    rValue = g_LstDispValvePZT[p_iIndex];
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        #region//=========== 變數 ==========
        public bool g_bLifeTimeEnd
        {
            get;
            private set;
        }
        private long m_lOutputCountResetPoint = 0;
        public ucPztLockAddjust g_ucPztLockAdjust = new ucPztLockAddjust();

        #endregion

        #region//========== Public Function (ArtPZT) ==========

        /// <summary> 設定控制出膠的模式 </summary>
        public void SetTriggerMode(enuTriggerMode p_eTriggerMode)
        {
            try
            {
                if (p_eTriggerMode == enuTriggerMode.None)
                {
                    p_eTriggerMode = enuTriggerMode.Continue;
                }
                this.SetModbusValue((int)enuModbusAddress.Action_Mode, Convert.ToUInt16(p_eTriggerMode));
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }


        /// <summary> 設定Command </summary>
        public void SetCommand(enuCommand p_eCommand)
        {
            try
            {
                this.SetModbusValue((int)enuModbusAddress.Cmd, Convert.ToUInt16(p_eCommand));
                if (p_eCommand == enuCommand.Trigger)
                { CallEvent_SoftwareTrigger(); }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }


        /// <summary> 取得OutputCount計數(沒有經過處理)(Address : 28+29) </summary>
        public long GetValveCounter()
        {
            long rValue = 0;
            try
            {
                ushort[] tDatas = this.g_ValveModbus.GetData((ushort)enuModbusAddress.Counter_NowCount, 2);
                if (tDatas == null) return 0;
                if (tDatas.Length == 0) return 0;
                ushort tData = (ushort)tDatas[1];
                uint tValue = (uint)tData << 16;
                tData = (ushort)tDatas[0];
                tValue += (uint)tData;
                if (tValue > 900000000)//9億次
                {
                    g_bLifeTimeEnd = true;
                }
                else
                {
                    g_bLifeTimeEnd = false;
                }
                return tValue;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        public bool GetValveIdle(bool p_bNeedLog)
        {
            bool rValue = false;
            try
            {
                enuValveStatus enuValveStatus = GetValveStatus();
                if (p_bNeedLog == true)
                {
                    clsLog.Log(clsCmData.enuLogType.SystemLog, "[" + this.g_sName + " ] PZT Valve Status : " + enuValveStatus.ToString());
                }
                if (enuValveStatus == enuValveStatus.Ready)
                {
                    rValue = true;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        public enuValveStatus GetValveStatus()
        {
            enuValveStatus rValue = enuValveStatus.NoCommunication;
            try
            {
                if (this.g_ValveModbus.ComPort_IsOpen() == true)
                {
                    ushort[] Datas = this.g_ValveModbus.GetData((int)enuModbusAddress.Status, 1);
                    if (Datas != null && Datas.Count() > 0)
                    {
                        rValue = enuValveStatus.Ready + Datas[0];
                    }
                    if (this.g_ValveModbus.ComPort_IsCommunicationError() == true)
                    {
                        rValue = enuValveStatus.NoCommunication;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        /// <summary> 取得閉鎖曲線資料 </summary>
        public ushort[] Get_PS_Voltage_Array(ushort arraysize)
        {
            ushort[] array = new ushort[50];
            ushort count = 0;
            ushort Addr1 = (ushort)enuModbusAddress.PS_Voltage_Array;
            ushort storeCount = 0;

            ushort[] sdata = new ushort[arraysize];

            while (arraysize != 0)
            {
                if (arraysize > 50)
                {
                    count = 50;
                    arraysize -= 50;
                }
                else
                {
                    count = arraysize;
                    arraysize = 0;
                }
                array = this.g_ValveModbus.GetData(Addr1, count);
                Addr1 += count;
                if (array != null)
                {
                    for (int i = 0; i < count; i++)
                    {
                        sdata[storeCount] = array[i];
                        storeCount++;
                    }
                }
                else
                {
                    break;
                }
            }
            return sdata;
        }

        #endregion

        #region//========== Private Function ==========

        /// <summary> 確認時間不超過控制器定義之上下限 (10~20000) </summary>
        /// <param name="p_Value"> 輸入值 </param>
        /// <returns> 輸出值 </returns>
        private ushort CheckTimeLimit(ushort p_Value)
        {
            if (p_Value < 10) { p_Value = 10; }
            if (p_Value > 20000) { p_Value = 20000; }
            return p_Value;
        }
        #endregion

        #region//========== Public Override ==========
        /// <summary> 初始化 </summary>
        public override bool InitialValve(string p_sName, string p_ComPort, int p_StationID, bool p_SimulatorMode, int p_ReadWriteTimeout = 300)
        {
            bool rValue = false;
            this.g_eValveType = enuValveType.ArtPZT;
            this.g_PmtValue = new clsPmtArtPZT();
            this.SetLockAdjust_StartUP();
            rValue = base.InitialValve(p_sName, p_ComPort, p_StationID, p_SimulatorMode, p_ReadWriteTimeout);
            if (clsArtSystem.bIsSoftwareSimulate == false)
            {
                clsDeviceReport mReport = new clsDeviceReport();
                mReport.DeviceName = p_sName;
                mReport.DeviceType = "ArtPZT_Controller";
                mReport.FwVersion = "Ver:" + Convert.ToDouble(this.GetModbusValue((int)enuModbusAddress.Version) / 100).ToString();
                mReport.SaveInfoTcpIp(p_ComPort, "Station" + p_StationID);
                mReport.DeviceName = p_sName;
                mReport.DeviceType = "ArtPZT_CounterBoard";
                mReport.FwVersion = "Ver:" + Convert.ToDouble(this.GetModbusValue((int)enuModbusAddress.Counter_Board_FW_Version) / 100).ToString();
                mReport.SaveInfoTcpIp(p_ComPort, "Station" + p_StationID);
            }
            return rValue;
        }
        /// <summary> 軟體通訊觸發吐膠 </summary>
        public override void SoftwareTrigger()
        {
            try
            {
                this.SetDo(enuDo.Interrupt, false);
                //this.SetDo(enuDo.ManualTrigger, true);
                this.SetCommand(enuCommand.Trigger);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        public override void SoftwareReset()
        {
            try
            {
                this.SetCommand(enuCommand.SoftReset);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> CMP訊號計數重置(Address : 800) </summary>
        public override void ResetCMPTriggerCount()
        {
            try
            {
                this.SetModbusValue((int)enuModbusAddress.Compare_Trigger_Count, 0);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 取得CMP訊號計數(Address : 800) </summary>
        public override long GetCMPTriggerCount()
        {
            long rValue = 0;
            try
            {
                rValue = Convert.ToInt64(this.GetModbusValue((int)enuModbusAddress.Compare_Trigger_Count));
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        /// <summary> OutputCount重置(經過處理不是直接對Modbus取值) </summary>
        public override void ResetOutputCount()
        {
            try
            {
                m_lOutputCountResetPoint = GetValveCounter();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 取得OutputCount計數(經過處理不是直接對Modbus取值) </summary>
        public override long GetOutputCount()
        {
            long rValue = 0;
            try
            {
                rValue = GetValveCounter();
                rValue -= m_lOutputCountResetPoint;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        /// <summary> 取得週期(ms) </summary>
        public override double GetCycleTime_ms()
        {
            double rValue = 1;
            if (this.g_PmtValue is clsPmtArtPZT)
            {
                clsPmtArtPZT PmtValue = (clsPmtArtPZT)this.g_PmtValue;
                rValue = PmtValue.Hold_Open_Time;//us
                rValue += PmtValue.Hold_Lock_Time;//us
                rValue += PmtValue.Open_Time;//us
                rValue += PmtValue.Lock_Time;//us
                rValue = rValue / 1000;//ms
                //return 1000.0 / Math.Round(1000000.0 / (mPZTPmt.Hold_Open_Time + mPZTPmt.Hold_Lock_Time + mPZTPmt.Open_Time + mPZTPmt.Lock_Time), 3);
            }
            return rValue;
        }

        /// <summary> 設定膠閥參數 </summary>
        public override bool SetPmt()
        {
            bool rValue = false;
            try
            {
                if (this.g_PmtValue is clsPmtArtPZT)
                {
                    clsPmtArtPZT mPmtArtValve = (clsPmtArtPZT)this.g_PmtValue;
                    ushort[] SetData = new ushort[]
                    {
                        (ushort)(100 - mPmtArtValve.Open_Volt),//上抬量值 (Enum=10)
                        (ushort)(100 - mPmtArtValve.Lock_Volt),// 閉鎖量值 (Enum=11)
                        CheckTimeLimit(mPmtArtValve.Hold_Open_Time),// 開啟時間 (us) (Enum=12)
                        CheckTimeLimit(mPmtArtValve.Hold_Lock_Time),//閉鎖時間 (us) (Enum=13)
                        CheckTimeLimit(mPmtArtValve.Lock_Time),//下衝時間 (us) (Enum=14)
                        CheckTimeLimit(mPmtArtValve.Open_Time),//上抬時間 (us) (Enum=15)
                    };
                    this.g_ValveModbus.SetData(10, SetData.Length, SetData);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        public override void SetOutputTime_ms(double p_dOutputTime_ms)//Allan
        {
            double rValue = p_dOutputTime_ms;
            int DotCount = (int)Math.Round((p_dOutputTime_ms / GetCycleTime_ms()), 0, MidpointRounding.AwayFromZero);
            try
            {
                rValue *= GetCycleTime_ms();
                m_dLstLatestOutputValue.Clear();
                m_dLstLatestOutputValue.Add(DotCount);
                this.SetModbusValue((int)enuModbusAddress.Dot_Amount_BufferLength, 1);
                this.SetModbusValue((int)enuModbusAddress.Dot_Amount_Buffer, (ushort)DotCount);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return;
        }

        /// <summary> 設定輸出單位(Address : 51)出膠時間[1~5000 Dot] </summary>
        public override double SetOutputValue(double p_dOutput)
        {
            double rValue = p_dOutput;
            try
            {
                rValue *= GetCycleTime_ms();
                m_dLstLatestOutputValue.Clear();
                m_dLstLatestOutputValue.Add(p_dOutput);
                this.SetModbusValue((int)enuModbusAddress.Dot_Amount_BufferLength, 1);
                this.SetModbusValue((int)enuModbusAddress.Dot_Amount_Buffer, (ushort)p_dOutput);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        public override void SetOutputValue(int[] p_dOutput)
        {
            try
            {
                ushort iCount = (ushort)p_dOutput.Length;
                if (iCount > 50)
                { iCount = 50; }
                ushort[] OutputData = new ushort[iCount];
                m_dLstLatestOutputValue.Clear();
                for (int i = 0; i < iCount; i++)
                {
                    m_dLstLatestOutputValue.Add(p_dOutput[i]);
                    OutputData[i] = (ushort)p_dOutput[i];
                }
                this.SetModbusValue((int)enuModbusAddress.Dot_Amount_BufferLength, iCount);
                this.g_ValveModbus.SetData((int)enuModbusAddress.Dot_Amount_Buffer, iCount, OutputData);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }


        public override string GetUnitText()
        {
            string rValue = "dot";
            return rValue;
        }

        private List<double> m_dLstLatestOutputValue = new List<double>();
        public override double GetLastestOutputValue()
        {
            double rValue = 0;
            if (m_dLstLatestOutputValue.Count > 0)
            {
                rValue = m_dLstLatestOutputValue[0];
            }
            return rValue;
        }
        #endregion

        #region//========== Public 閉鎖校正 ==========

        #region//參數 (AdjustPass, AdjustValue)
        private bool m_bValveLockAdjustPass = false;
        private double m_dValveLockAdjustValue = 190;

        public bool GetLockAdjust_IsPass()
        {
            bool rValue = false;
            try
            {
                rValue = m_bValveLockAdjustPass;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        public void SetLockAdjust_Pass(bool p_bPass)
        {
            try
            {
                m_bValveLockAdjustPass = p_bPass;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public double GetLockAdjust_Value()
        {
            double rValue = 190;
            try
            {
                rValue = m_dValveLockAdjustValue;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        public void SetLockAdjust_Value(double p_dValue)
        {
            try
            {
                m_dValveLockAdjustValue = p_dValue;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region//基楚架構
        System.Threading.Thread mThread = null;
        PZT_Algorithm_ m_Pzt_Algorithm = new PZT_Algorithm_();
        string m_sPZT_Algrithm_ErrorMessage = "";
        List<uint> X_DAC = new List<uint>();
        List<uint> Y_mV = new List<uint>();
        public void SetLockAdjust_StartUP()
        {
            try
            {
                //m_timer1 = new System.Timers.Timer(100);
                //m_timer1.Elapsed += timer1_Tick;
                //m_timer1.Stop();
                //m_timer1.Enabled = false;
                g_eLockAdjust_ErrorMessage = enuLockAdjust_ErrorMessage.None;
                g_eLockAdjust_Status = enuLockAdjust_Status.None;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void timer1_Tick()
        {
            try
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(1);
                    Auto_Procedure();
                    if (GetLoakAdjust_IsProcessing() == false)
                    {
                        break;
                    }
                }
                mThread = null;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region//Enum 狀態 (g_eLockAdjust_ErrorMessage, g_eLockAdjust_Status)
        public enum enuLockAdjust_ErrorMessage
        {
            None,
            CommunicationError,
            CancelAction,
            ControlOverSpec,
            Pzt_Algorithm_ResultError,
            GetCurveData_Error,
            NoCurveData,
        }
        public enum enuLockAdjust_Status
        {
            None,

            ThreadStart,
            AdjustWaiting,
            Calculating,

            Done,
            Fail,
            Cancel,
        }
        public enuLockAdjust_ErrorMessage g_eLockAdjust_ErrorMessage
        {
            get;
            private set;
        }
        public enuLockAdjust_Status g_eLockAdjust_Status
        {
            get;
            private set;
        }
        #endregion

        #region//校正流程 (Start, Stop, Auto_Procedure)
        public void SetLockAdjust_Start()
        {
            try
            {
                if (mThread == null)
                {
                    //m_timer1.Enabled = false;
                    g_eLockAdjust_ErrorMessage = enuLockAdjust_ErrorMessage.None;
                    this.SetTriggerMode(enuTriggerMode.Adjusting);
                    this.SetCommand(enuCommand.AdjustStart);
                    if (this.g_ValveModbus.ComPort_IsCommunicationError() == true)
                    {
                        g_eLockAdjust_ErrorMessage = enuLockAdjust_ErrorMessage.CommunicationError;
                    }
                    else
                    {
                        this.SetModbusValue((int)clsCtrlDispValve_ArtPZT.enuModbusAddress.Adjust_State, 1);
                        if (this.g_ValveModbus.ComPort_IsCommunicationError() == true)
                        {
                            g_eLockAdjust_ErrorMessage = enuLockAdjust_ErrorMessage.CommunicationError;
                        }
                        else
                        {
                            m_sPZT_Algrithm_ErrorMessage = "";
                            g_eLockAdjust_Status = enuLockAdjust_Status.ThreadStart;

                            mThread = new System.Threading.Thread(timer1_Tick);
                            mThread.IsBackground = true;
                            mThread.Start();
                            //m_timer1.Start();
                            //m_timer1.Enabled = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public void SetLockAdjust_Stop()
        {
            try
            {
                if (GetLoakAdjust_IsProcessing() == true)
                {
                    SetLockAdjust_Error(enuLockAdjust_ErrorMessage.CancelAction);
                    g_eLockAdjust_Status = enuLockAdjust_Status.Cancel;
                }
                this.SetCommand(enuCommand.AdjustStop);
                this.SetCommand(enuCommand.PZTLock);
                this.SetTriggerMode(enuTriggerMode.Continue);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        public bool GetLoakAdjust_IsProcessing()
        {
            bool rValue = false;
            try
            {
                switch (g_eLockAdjust_Status)
                {
                    case enuLockAdjust_Status.ThreadStart:
                    case enuLockAdjust_Status.AdjustWaiting:
                    case enuLockAdjust_Status.Calculating:
                    default:
                        rValue = true;
                        break;
                    case enuLockAdjust_Status.None:
                    case enuLockAdjust_Status.Done:
                    case enuLockAdjust_Status.Fail:
                    case enuLockAdjust_Status.Cancel:
                        rValue = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        private void SetLockAdjust_Error(enuLockAdjust_ErrorMessage p_eErrorMessage)
        {
            try
            {
                g_eLockAdjust_ErrorMessage = p_eErrorMessage;
                if (p_eErrorMessage == enuLockAdjust_ErrorMessage.CancelAction)
                {
                    g_eLockAdjust_Status = enuLockAdjust_Status.Cancel;
                }
                else
                {
                    g_eLockAdjust_Status = enuLockAdjust_Status.Fail;
                }
                //m_timer1.Enabled = false;
                this.SetCommand(enuCommand.AdjustStop);
                this.SetCommand(enuCommand.PZTLock);
                this.SetTriggerMode(enuTriggerMode.Continue);
                clsLog.Log(enuLogName.ValveLockAdjust.ToString() + "-" + this.g_sName, "Error : " + p_eErrorMessage.ToString());
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void SetAjustSuccess()
        {
            try
            {
                //m_timer1.Enabled = false;
                this.SetModbusValue((int)enuModbusAddress.Adjust_State, 0);
                this.SetCommand(enuCommand.AdjustStop);
                g_eLockAdjust_ErrorMessage = enuLockAdjust_ErrorMessage.None;
                g_eLockAdjust_Status = enuLockAdjust_Status.Done;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }

        }
        private void Auto_Procedure()
        {
            try
            {
                double Adj_State = this.GetModbusValue((int)enuModbusAddress.Adjust_State); //PztCtrl.Get_Adjust_Flag(ref Adj_State);
                if (this.g_ValveModbus.ComPort_IsCommunicationError() == true)
                { SetLockAdjust_Error(enuLockAdjust_ErrorMessage.CommunicationError);  }
                double Switch_Voltage_mV = this.GetModbusValue((int)enuModbusAddress.Counter_SP_Volt);//PztCtrl.Get_Proximity_Switch_Voltage(ref Switch_Voltage_mV);
                if (this.g_ValveModbus.ComPort_IsCommunicationError() == true)
                { SetLockAdjust_Error(enuLockAdjust_ErrorMessage.CommunicationError); }
                if(g_eLockAdjust_ErrorMessage == enuLockAdjust_ErrorMessage.None)
                {
                    switch ((int)Adj_State)// 0=Done, 1=Wait, 2=Calculation, 3=Calculation Done
                    {
                        case 0: //done (完成了) ??
                            SetAjustSuccess();//不應該直接進入這裡
                            break;
                        case 1: //wait
                            g_eLockAdjust_Status = enuLockAdjust_Status.AdjustWaiting;
                            break;
                        case 2: //Calculation
                            g_eLockAdjust_Status = enuLockAdjust_Status.Calculating;
                            {
                                if (Get_Curve(ref X_DAC, ref Y_mV) == true) //算轉折
                                {
                                    if (X_DAC.Count() != 0)
                                    {
                                        m_Pzt_Algorithm.Calculate_LockPZT_Position(X_DAC, Y_mV);
                                        if (m_Pzt_Algorithm.IS_Pass == true)
                                        {
                                            TurningPoint_Prmt tp = m_Pzt_Algorithm.tp;
                                            uint Num_ADC = 0, Num_Volt = 0;
                                            double Num_DACtoVolt = 0;

                                            Num_ADC = tp.Result_LockPZT_DAC;
                                            Num_Volt = tp.Result_Sensor_mV;
                                            Num_DACtoVolt = (((double)tp.Result_LockPZT_DAC) / 4095 * 5 * 31.5);
                                            this.SetModbusValue((int)enuModbusAddress.LockPZT_LockVolt, (ushort)tp.Result_LockPZT_DAC);
                                            this.SetModbusValue((int)enuModbusAddress.Adjust_State, 3);
                                            //SetData(clsPzt_Allring.enuAddress.LockPZT_LockVolt, (ushort)tp.Result_LockPZT_DAC);
                                            //SetData(clsPzt_Allring.enuAddress.Adjust_State, 3);
                                            if (this.g_ValveModbus.ComPort_IsCommunicationError() == true)
                                            { SetLockAdjust_Error(enuLockAdjust_ErrorMessage.CommunicationError); }
                                            else
                                            {
                                                string Message = "Lock DAC: " + tp.Result_LockPZT_DAC.ToString() + "\\"
                                                                + "Lock mV: " + tp.Result_Sensor_mV.ToString();
                                                clsLog.Log(enuLogName.ValveLockAdjust.ToString() + "-" + this.g_sName, Message);
                                            }
                                        }
                                        else
                                        {
                                            m_sPZT_Algrithm_ErrorMessage = m_Pzt_Algorithm.Message;
                                            SetLockAdjust_Error(enuLockAdjust_ErrorMessage.Pzt_Algorithm_ResultError);
                                        }
                                    }
                                    else
                                    {
                                        SetLockAdjust_Error(enuLockAdjust_ErrorMessage.NoCurveData);
                                    }
                                }
                                else
                                {
                                    SetLockAdjust_Error(enuLockAdjust_ErrorMessage.GetCurveData_Error);
                                }
                            }
                            break;
                        case 3: //Calculation Done
                            //bIsAdjustDone = true;
                            //strAdjustStatus = "Calculation Done";
                            SetAjustSuccess();
                            break;
                        case 4:
                            this.SetModbusValue((int)enuModbusAddress.Adjust_State, 0); //Result = PztCtrl.Set_Adjust_Flag(0);
                            this.SetLockAdjust_Error(enuLockAdjust_ErrorMessage.ControlOverSpec);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        public bool Get_Curve(ref List<uint> p_X_DAC, ref List<uint> p_Y_mV)
        {
            bool rValue = false;
            try
            {
                //short[] Buffer = new short[500];
                double Buffersize = this.GetModbusValue((int)enuModbusAddress.PS_Voltage_Array_Items);
                double num_LockPZT_RiseVolt = this.GetModbusValue((int)enuModbusAddress.LockPZT_RiseVolt);
                //GetData(clsPzt_Allring.enuAddress.LockPZT_RiseVolt, ref num_LockPZT_RiseVolt);
                //GetData(clsPzt_Allring.enuAddress.PS_Voltage_Array_Items, ref Buffersize); //PztCtrl.Get_PS_Voltage_Array_Items(ref Buffersize);
                p_X_DAC.Clear();
                p_Y_mV.Clear();
                if (this.g_ValveModbus.ComPort_IsCommunicationError() == false)
                {
                    if (Buffersize > 0)
                    {
                        p_X_DAC.Clear();
                        p_Y_mV.Clear();
                        ushort[] Buffer = Get_PS_Voltage_Array((ushort)Buffersize);//PztCtrl.Get_PS_Voltage_Array(ref Buffer, Buffersize);
                        for (int i = 0; i < Buffersize; i++)
                        {
                            p_X_DAC.Add((uint)(num_LockPZT_RiseVolt * i));
                            p_Y_mV.Add((uint)Buffer[i]);
                        }
                        if (p_X_DAC[p_X_DAC.Count - 1] > 4095)
                        {
                            p_X_DAC[p_X_DAC.Count - 1] = 4095;
                        }
                        rValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        #endregion


        #endregion
    }
}

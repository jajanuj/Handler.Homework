using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtSystem.Files;
using ArtCommonLib;
using ArtControlLib;

namespace ArtSystem.MultiSystem
{
    /// <summary> 霧化閥 </summary>
    public class clsCtrlDispValve_ArtSpray : clsCtrlDispValve
    {
        #region//========== Modbus Address ==========
        /// <summary> 參數位址的列舉 (控制器的設定參數) </summary>
        public enum enuModbusAddress
        {
            /// <summary> 韌體 (Enum=0) </summary>
            FWVersion = 0,
            ///// <summary> 上抬及下衝氣壓比例[0-100%] (Enum=10)</summary>
            //ValvePressure = 10,
            ///// <summary> 膠閥出膠撞擊次數(一個上抬+下衝為一個Cycle) (Enum=11) </summary>
            //JettingCycles = 11,
            ///// <summary> 下衝電磁閥作用時間[ms] (Enum=12) </summary>
            //PinDownTime = 12,
            ///// <summary> 上抬電磁閥作用時間[ms] (Enum=13) </summary>
            //PinUpTime = 13,
            ///// <summary> 膠針預抬昇時間[ms] (Enum=14) </summary>
            //PreLiftTime = 14,
            ///// <summary> 是否開啟膠閥上抬及下衝電磁閥的壓力 (Enum=17) </summary>
            //ValvePressureEnable = 17,
            ///// <summary> 膠管壓力供壓時機 (Enum=18) </summary>
            //BarrelPressureMode = 18,
            ///// <summary> 膠管壓力開啟時的壓力[0-100%] (Enum=19) </summary>
            //BarrelPressure = 19,
            /// <summary> 控制命令 , SetCtrlCommand (Enum=20) [0=待機狀態, 2=寫入目前資料至控制盒的預設值(斷電保持) , 3=由通訊方式觸發開始出膠]</summary>
            Command = 20,
            ///// <summary> 出膠延遲時間[ms]  (Enum=21) </summary>
            //SetJetDelayTime = 21,
            ///// <summary> 上抬的停頓時間[ms] (Enum=22) </summary>
            //PinUpDelayTime = 22,
            ///// <summary> 下衝的停頓時間[ms] (Enum=23) </summary>
            //PinDownDelayTime = 23,
            /// <summary> 觸發出膠次數[0~65535] (Enum=24) </summary>
            TriggerCount = 24,
            ///// <summary> 要用來觸發出膠的方式 (Enum=25) </summary>
            //TriggerMode = 25,
            /// <summary> 氣動脈衝出膠時間[1~60000 ms] (Enum=26) </summary>
            PnuematicDispTime = 26,
            /// <summary> 內霧化的開啟延遲時間[1~60000 ms] (Enum=27) </summary>
            OpInsideAtmTime = 27,
            /// <summary> 外霧化的開啟延遲時間[1~60000 ms] (Enum=28) </summary>
            OpOutsideAtmTime = 28,
            /// <summary> 供膠壓力的開啟延遲時間[1~60000 ms] (Enum=29) </summary>
            OpValvePreTime = 29,
            /// <summary> 閉鎖的開啟延遲時間[1~60000 ms] (Enum=30) </summary>
            OpLockTime = 30,
            /// <summary> 內霧化的關閉延遲時間[1~60000 ms] (Enum=31) </summary>
            ClInsideAtmTime = 31,
            /// <summary> 外霧化的關閉延遲時間[1~60000 ms] (Enum=32) </summary>
            ClOutsideAtmTime = 32,
            /// <summary> 供膠壓力的關閉延遲時間[1~60000 ms] (Enum=33) </summary>
            ClValvePreTime = 33,
            /// <summary> 閉鎖的關閉延遲時間[1~60000 ms] (Enum=34) </summary>
            ClLockTime = 34,
            /// <summary> 點膠次數陣列長度[1~40] (Enum=35) </summary>
            PnuematicDispTimeNumber = 35,
            /// <summary> 出膠的時間Group[1~60000 ms](從36開始可以使用~76),共40筆 (Enum=36~75)</summary>
            PnuematicDispTimeGroup = 36,
            /// <summary> 取得工作模式[0,1] (Enum=76) Mode0:Trigger Data 36~75, Mode1:Trigger Start and Trigger End. (Default 0) </summary>//設定參數
            WorkType = 76,
            /// <summary> 取得Pulse計數[0~65535] (Enum=77) </summary>
            PulseCount = 77,
        }

        #endregion

        #region//========== Enum 定義 ==========

        ///// <summary> 觸發出膠的方式 (Address:25) (註By俊暉:Spray我只看過ContinueMode) </summary>
        //public enum enuTriggerMode
        //{
        //    /// <summary> 收到一個觸發訊號後膠閥連續撞擊出膠(依設定次數) </summary>
        //    Continune,
        //    /// <summary> 收到一個觸發訊號後膠閥撞擊一次 </summary>
        //    OnTheFly,
        //}
        /// <summary> 控制命令的列舉(Address:20)  [0=待機狀態, 2=寫入目前資料至控制盒的預設值(斷電保持) , 3=由通訊方式觸發開始出膠]  </summary>
        public enum enuCommand
        {
            /// <summary> 待機狀態 </summary>
            Idle,
            /// <summary> 寫入目前資料至控制盒的預設值(斷電保持) </summary>
            SaveToROM,
            /// <summary> 由通訊方式觸發開始出膠 </summary>
            SoftwareTrigger,
        }


        /// <summary> 接受CMP-Trigger的模式(Address:76) (Default 0) , (註By俊暉:Spray目前都是使用Mode1) </summary>

        public enum enuWorkType
        {
            /// <summary> Mode0:Trigger Data 35~75 </summary>
            TriggerData,
            /// <summary> Mode1:Trigger Start and Trigger End</summary>
            StartEnd,

        }
        #endregion

        #region//========== Public Function (Spary) ==========
        ///// <summary> 目前主要使用(Continune) </summary>
        //public void SetTriggerMode(enuTriggerMode p_eTriggerMode)
        //{
        //    try
        //    {
        //        switch (p_eTriggerMode)
        //        {
        //            case enuTriggerMode.Continune:
        //                this.SetModbusValue((int)enuModbusAddress.TriggerMode, 0);
        //                break;
        //            case enuTriggerMode.OnTheFly:
        //                this.SetModbusValue((int)enuModbusAddress.TriggerMode, 1);
        //                break;
        //            default:
        //                break;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        clsArtSystem.CatchLog(ex);
        //    }
        //}

        /// <summary> 目前主要使用(StartEnd) </summary>
        public void SetWorkMode(enuWorkType p_eWorkMode)
        {
            try
            {
                switch (p_eWorkMode)
                {
                    case enuWorkType.TriggerData:
                        this.SetModbusValue((int)enuModbusAddress.WorkType, 0);
                        break;
                    case enuWorkType.StartEnd:
                        this.SetModbusValue((int)enuModbusAddress.WorkType, 1);
                        break;
                    default:
                        break;
                }
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
                switch (p_eCommand)
                {
                    case enuCommand.Idle:
                        this.SetModbusValue((int)enuModbusAddress.Command, 0);
                        break;
                    case enuCommand.SaveToROM:
                        this.SetModbusValue((int)enuModbusAddress.Command, 2);
                        break;
                    case enuCommand.SoftwareTrigger:
                        this.SetModbusValue((int)enuModbusAddress.Command, 3);
                        CallEvent_SoftwareTrigger();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }


        #endregion

        #region//========== Public Override ==========
        /// <summary> 初始化 </summary>
        public override bool InitialValve(string p_sName, string p_ComPort, int p_StationID, bool p_SimulatorMode, int p_ReadWriteTimeout = 300)
        {
            bool rValue = false;
            this.g_eValveType = enuValveType.ArtSpray;
            this.g_PmtValue = new clsPmtArtSpray();
            rValue = base.InitialValve(p_sName, p_ComPort, p_StationID, p_SimulatorMode, p_ReadWriteTimeout);
            return rValue;
        }
        /// <summary> 軟體通訊觸發吐膠 </summary>
        public override void SoftwareTrigger()
        {
            try
            {
                this.SetDo(enuDo.Interrupt, false);
                this.SetCommand(enuCommand.SoftwareTrigger);
                //this.SetModbusValue((int)enuModbusAddress.Command, 3);
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
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> CMP訊號計數重置(Address : 77) </summary>
        public override void ResetCMPTriggerCount()
        {
            try
            {
                this.SetModbusValue((int)enuModbusAddress.PulseCount, 0);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 取得CMP訊號計數(Address : 77)(0~65535) </summary>
        public override long GetCMPTriggerCount()
        {
            long rValue = 0;
            try
            {
                rValue = Convert.ToInt64(this.GetModbusValue((int)enuModbusAddress.PulseCount));
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        /// <summary> OutputCount重置(Address : 24)，無法寫入 </summary>
        public override void ResetOutputCount()
        {
            try
            {
                //this.SetModbusValue((int)enuModbusAddress.TriggerCount, 0);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 取得OutputCount計數(Address : 24)(0~65535) </summary>
        public override long GetOutputCount()
        {
            long rValue = 0;
            try
            {
                rValue = Convert.ToInt64(this.GetModbusValue((int)enuModbusAddress.TriggerCount));
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        /// <summary> 設定膠閥參數 </summary>
        public override bool SetPmt()
        {
            bool rValue = false;
            try
            {
                if (this.g_PmtValue is clsPmtArtSpray)
                {
                    clsPmtArtSpray mPmtArtSpare = (clsPmtArtSpray)this.g_PmtValue;
                    ushort[] SetData = new ushort[]
                    {
                       (ushort)mPmtArtSpare.OpInsideAtmTime,// 內霧化的延遲開啟時間 (Enum=27)
                       (ushort)mPmtArtSpare.OpOutsideAtmTime,//外霧化的延遲開啟時間 (Enum=28) 
                       (ushort)mPmtArtSpare.OpValvePreTime,//供膠壓力的延遲開啟時間 (Enum=29)
                       (ushort)mPmtArtSpare.OpLockTime,//閉鎖的的延遲開啟時間 (Enum=30)
                       (ushort)mPmtArtSpare.ClInsideAtmTime,//內霧化的延遲關閉時間 (Enum=31)
                       (ushort)mPmtArtSpare.ClOutsideAtmTime,//外霧化的延遲關閉時間 (Enum=32)
                       (ushort)mPmtArtSpare.ClValvePreTime,//供膠壓力的延遲關閉時間 (Enum=33)
                       (ushort)mPmtArtSpare.ClLockTime,// 閉鎖的的延遲關閉時間 (Enum=34)
                    };
                    this.g_ValveModbus.SetData(27, SetData.Length, SetData);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        /// <summary> 設定輸出單位(Address : 26)出膠時間[1~60000 ms] </summary>
        public override double SetOutputValue(double p_dOutput)
        {
            double rValue = p_dOutput;
            try
            {
                m_dLstLatestOutputValue.Clear();
                m_dLstLatestOutputValue.Add(p_dOutput);
                this.SetModbusValue((int)enuModbusAddress.PnuematicDispTime, (ushort)p_dOutput);
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
                if (iCount > 40)
                { iCount = 40; }
                ushort[] OutputData = new ushort[iCount];
                m_dLstLatestOutputValue.Clear();
                for (int i = 0; i < iCount; i++)
                {
                    m_dLstLatestOutputValue.Add(p_dOutput[i]);
                    OutputData[i] = (ushort)p_dOutput[i];
                }
                this.SetModbusValue((int)enuModbusAddress.PnuematicDispTimeNumber, iCount);
                this.g_ValveModbus.SetData((int)enuModbusAddress.PnuematicDispTimeGroup, iCount, OutputData);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        public override string GetUnitText()
        {
            string rValue = "ms";
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
    }
}

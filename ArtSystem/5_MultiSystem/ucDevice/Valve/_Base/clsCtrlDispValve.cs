using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ArtSystem.Files;
using ArtData;
using ArtCommonLib;
using ArtControlLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArtSystem.MultiSystem
{
    public class clsCtrlDispValve
    {
        #region//========== Enum 定義 ==========

        public enum enuValveType
        {
            Non,
            ArtSpray,//霧化閥
            ArtPZT,//壓電閥
        }
        public enum enuDo
        {
            ManualTrigger,
            TriggerEnable,
            Interrupt,
        }
        public enum enuDi
        {
            Ready,
            Done,
        }

        #endregion

        #region//========== 變數 ==========

        private clsCtrlDispValve.enuValveType m_eValveType = enuValveType.Non;
        /// <summary> 膠閥種類 </summary>
        public clsCtrlDispValve.enuValveType g_eValveType
        {
            get
            {
                return m_eValveType;
            }
            protected set
            {
                m_eValveType = value;
            }
        }

        public bool g_bLoadFileSuccess
        {
            get;
            private set;
        }
        public string g_sName
        {
            get;
            private set;
        }
        /// <summary> 膠閥系統參數(重啟軟體才有效) </summary>
        public clsPmtDispValve g_PmtDispValveSetting = new clsPmtDispValve();

        public object g_PmtValue = null;

        /// <summary> Modbus通訊模組 </summary>
        public clsValveModbus g_ValveModbus = new clsValveModbus();

        public double m_dDotWeight_mg = 1;
        /// <summary> 流率 </summary>
        public double g_dDotWeight_mg
        {
            get
            {
                return m_dDotWeight_mg;
            }
            private set
            {
                m_dDotWeight_mg = value;
            }

        }

        /// <summary> DI-Ready指定ID</summary>
        public clsEnum.enuDi? g_eDI_Ready = null;
        /// <summary> DI-Done指定ID</summary>
        public clsEnum.enuDi? g_eDI_Done = null;
        /// <summary> DO-ManualTrigger指定ID</summary>
        public clsEnum.enuDo? g_eDO_ManualTrigger = null;
        /// <summary> DO-TriggerEnable指定ID</summary>
        public clsEnum.enuDo? g_eDO_TriggerEnable = null;
        /// <summary> DO-Interrupt指定ID</summary>
        public clsEnum.enuDo? g_eDO_Interrupt = null;

        public DateTime g_PmtFileLastEdit = new DateTime();
        #endregion


        static public List< clsCtrlDispValve> g_LstDispValve = new List<clsCtrlDispValve>();
        public clsCtrlDispValve()
        {
            if (g_LstDispValve.Contains(this) == false)
            {
                g_LstDispValve.Add(this);
            }
        }


        #region//========== 委派事件 ==========
        /// <summary> SoftwareTrigger 事件</summary>
        private event evtSoftwareTrigger m_SoftwareTriggerEvent = null;
        public delegate void evtSoftwareTrigger(clsCtrlDispValve sender);
        /// <summary> 接收到訊息事件 </summary>
        public event evtSoftwareTrigger _SoftwareTriggerEvent
        {
            remove
            {
                m_SoftwareTriggerEvent -= value;
            }
            add
            {
                m_SoftwareTriggerEvent += value;
            }
        }

        protected void CallEvent_SoftwareTrigger()
        {
            try
            {
                if (m_SoftwareTriggerEvent != null)
                {
                    m_SoftwareTriggerEvent(this);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region//========== Public Function (需要被Override) ===========
        /// <summary> 初始化 </summary>
        public virtual bool InitialValve(string p_sName, string p_ComPort, int p_StationID, bool p_SimulatorMode, int p_ReadWriteTimeout = 300)
        {
            bool rValue = false;
            g_sName = p_sName;
            rValue = g_ValveModbus.ComPort_Open(p_ComPort, p_StationID, p_SimulatorMode, p_ReadWriteTimeout);
            return rValue;
        }
        /// <summary> 軟體通訊觸發吐膠 </summary>
        public virtual void SoftwareTrigger()
        {
            CallEvent_SoftwareTrigger();
        }
        /// <summary> 軟體通訊觸發吐膠 </summary>
        public virtual void SoftwareReset()
        {
        }


        /// <summary> Trigger重置 </summary>
        public virtual void ResetCMPTriggerCount()
        {
        }
        /// <summary> 取得Trigger計數 </summary>
        public virtual long GetCMPTriggerCount()
        {
            return 0;
        }

        /// <summary> OutputCount重置 </summary>
        public virtual void ResetOutputCount()
        {
        }
        /// <summary> 取得OutputCount計數 </summary>
        public virtual long GetOutputCount()
        {
            return 0;
        }

        /// <summary> 取得週期(ms) , Flux固定1ms </summary>
        public virtual double GetCycleTime_ms()
        {
            return 1;
        }

        /// <summary> 設定輸出單位 </summary>
        public virtual void SetOutputTime_ms(double p_dOutputTime_ms)
        {
        }

        /// <summary> 設定輸出單位 </summary>
        public virtual double SetOutputValue(double p_dOutput)
        {
            double dOutput = p_dOutput;
            return dOutput;
        }
        /// <summary> 設定輸出單位 </summary>
        public virtual void SetOutputValue(int[] p_dOutput)
        {
        }
        /// <summary> 設定膠閥參數 </summary>
        public virtual bool SetPmt()
        {
            return false;
        }

        public virtual string GetUnitText()
        {
            string rValue = "ms";
            return rValue;
        }

        public virtual double GetLastestOutputValue()
        {
            double rValue = 0;
            return rValue;
        }
        #endregion


        #region//========== Public Function(連線功能)  ===========

        public bool IsConnected()
        {
            bool rValue = false;
            try
            {
                rValue = g_ValveModbus.ComPort_IsOpen();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        public bool ComPort_Open()
        {
            bool rValue = false;
            try
            {
                rValue = g_ValveModbus.ComPort_Open();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        /// <summary>  ComPort 關閉連線 </summary>
        public void ComPort_Close()
        {
            try
            {
                g_ValveModbus.ComPort_Close();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary>  ComPort 是否已開啟連線 </summary>
        public bool ComPort_IsOpen()
        {
            bool rValue = false;
            try
            {
                rValue = g_ValveModbus.ComPort_IsOpen();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        /// <summary>  ComPort 是否通訊異常 </summary>
        public bool ComPort_IsCommunicationError()
        {
            bool rValue = false;
            try
            {
                rValue = g_ValveModbus.ComPort_IsCommunicationError();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        #endregion

        #region//========== Public Function  ===========

        /// <summary> 設定Modbus參數 </summary>
        public virtual bool SetModbusValue(int p_iModbusAddress, ushort Value)
        {
            bool rValue = false;
            try
            {
                if (p_iModbusAddress == 4 && Value == 1)
                {
                }
                ushort[] SendData = new ushort[] { Value };
                rValue = g_ValveModbus.SetData(p_iModbusAddress, SendData.Length, SendData);
            }
            catch
            {

            }
            return rValue;
        }
        /// <summary> 取得Modbus參數 </summary>
        public virtual double GetModbusValue(int p_iModbusAddress)
        {
            double rValue = 0;
            try
            {
                ushort[] GetData = g_ValveModbus.GetData(p_iModbusAddress, 1);
                if (GetData != null)
                {
                    if (GetData.Length > 0)
                    {
                        rValue = GetData[0];
                    }
                }
            }
            catch
            {
            }
            return rValue;
        }
        /// <summary> 設定當前流率 (Spray/Flux無效)</summary>
        public virtual void SetCurrentDotWeight(double p_dDotWeight_mg)
        {
            if (this.g_eValveType != enuValveType.ArtSpray)
            {
                g_dDotWeight_mg = p_dDotWeight_mg;
            }
            else
            {

                g_dDotWeight_mg = 1;
            }
        }

        /// <summary> 取得DI狀態 </summary>
        public virtual bool GetDi(enuDi p_eDi)
        {
            bool rValue = false;
            try
            {
                switch (p_eDi)
                {
                    case enuDi.Ready:
                        if (g_eDI_Ready != null)
                        { rValue = clsDioCtrl.GetDi((clsEnum.enuDi)g_eDI_Ready); }
                        break;
                    case enuDi.Done:
                        if (g_eDI_Done != null)
                        { rValue = clsDioCtrl.GetDi((clsEnum.enuDi)g_eDI_Done); }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        /// <summary> 設定DO輸出</summary>
        public virtual void SetDo(enuDo p_eDo, bool p_bOnOff)
        {
            try
            {
                switch (p_eDo)
                {
                    case enuDo.ManualTrigger:
                        if (g_eDO_ManualTrigger != null)
                        {
                            clsDioCtrl.SetDo((clsEnum.enuDo)g_eDO_ManualTrigger, p_bOnOff);
                            clsDioCtrl.RefreshDoData();
                        }
                        break;
                    case enuDo.TriggerEnable:
                        if (g_eDO_TriggerEnable != null)
                        {
                            clsDioCtrl.SetDo((clsEnum.enuDo)g_eDO_TriggerEnable, p_bOnOff);
                            clsDioCtrl.RefreshDoData();
                        }
                        break;
                    case enuDo.Interrupt:
                        if (g_eDO_Interrupt != null)
                        {
                            clsDioCtrl.SetDo((clsEnum.enuDo)g_eDO_Interrupt, p_bOnOff);
                            clsDioCtrl.RefreshDoData();
                        }
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

        /// <summary> 設定DO輸出</summary>
        public virtual void SetDo(bool p_bManualTrigger, bool p_bTriggerEnable, bool p_bInterrupt)
        {
            try
            {
                if (g_eDO_ManualTrigger != null)
                { clsDioCtrl.SetDo((clsEnum.enuDo)g_eDO_ManualTrigger, p_bManualTrigger); }
                if (g_eDO_TriggerEnable != null)
                { clsDioCtrl.SetDo((clsEnum.enuDo)g_eDO_TriggerEnable, p_bTriggerEnable); }
                if (g_eDO_Interrupt != null)
                { clsDioCtrl.SetDo((clsEnum.enuDo)g_eDO_Interrupt, p_bInterrupt); }
                clsDioCtrl.RefreshDoData();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 取得DO狀態</summary>
        public virtual bool GetDo(enuDo p_eDo)
        {
            bool rValue = false;
            try
            {
                switch (p_eDo)
                {
                    case enuDo.ManualTrigger:
                        if (g_eDO_ManualTrigger != null)
                        { rValue = clsDioCtrl.GetDo((clsEnum.enuDo)g_eDO_ManualTrigger); }
                        break;
                    case enuDo.TriggerEnable:
                        if (g_eDO_TriggerEnable != null)
                        { rValue = clsDioCtrl.GetDo((clsEnum.enuDo)g_eDO_TriggerEnable); }
                        break;
                    case enuDo.Interrupt:
                        if (g_eDO_Interrupt != null)
                        { rValue = clsDioCtrl.GetDo((clsEnum.enuDo)g_eDO_Interrupt); }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }


        #endregion

        #region //========== Public Function (LoadPmt, SavePmt) ==========

        private string m_sCurrentPmtPath = "";
        /// <summary> 目前檔案路徑 </summary>
        public string g_sCurrentPmtPath
        {
            get
            {
                try
                {
                    if (clsArtSystem.bIsProgramOpen == true)
                    {
                        if (m_sCurrentPmtPath == "" || System.IO.File.Exists(m_sCurrentPmtPath) == false)
                        {
                            clsLog.Log(clsArtSystem.g_strCatchLogName, "Load Disp Valve Pmt Error : " + g_sName);
                            m_sCurrentPmtPath = System.IO.Path.GetDirectoryName(ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe)) + "\\Valve\\Default.ini";
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
                return m_sCurrentPmtPath;
            }
            private set
            {
                m_sCurrentPmtPath = value;
            }
        }

        /// <summary> 載入參數檔案 </summary>
        public virtual bool LoadPmt(string sPath)
        {
            bool ReturnValue = false;
            try
            {
                g_sCurrentPmtPath = sPath;//記錄目前參數文件的路徑
                if (System.IO.File.Exists(sPath) == false)//確認文件是否存在
                {
                    try
                    {
                        string strDirectory = System.IO.Path.GetDirectoryName(sPath);
                        if (System.IO.Directory.Exists(strDirectory) == false)
                        {
                            System.IO.Directory.CreateDirectory(strDirectory);
                        }
                        JsonHelper.JsonSerializeToFile(this, sPath, Encoding.Unicode);
                        ReturnValue = false;
                    }
                    catch (Exception ex)
                    {
                        clsArtSystem.CatchLog(ex);
                        ReturnValue = false;
                    }
                }
                try
                {
                    using (var sr = new StreamReader(sPath, Encoding.Unicode))
                    {
                        var serializer = new JsonSerializer();
                        var Temp = serializer.Deserialize(sr, this.g_PmtValue.GetType());
                        if (Temp != null)
                        {
                            this.g_PmtFileLastEdit = System.IO.File.GetLastWriteTime(sPath);
                            clsClassFunc.Copy(Temp, this.g_PmtValue);
                            ReturnValue = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            g_bLoadFileSuccess = ReturnValue;
            return g_bLoadFileSuccess;
        }
        /// <summary> 儲存參數檔案 </summary>
        public virtual bool SavePmt(string sPath)
        {
            try
            {
                string sDirectory = System.IO.Path.GetDirectoryName(g_sCurrentPmtPath);
                if (System.IO.Directory.Exists(sDirectory) == false)
                {
                    System.IO.Directory.CreateDirectory(sDirectory);
                    if (System.IO.Directory.Exists(sDirectory) == false)
                    {
                        return false;
                    }
                }
                JsonHelper.JsonSerializeToFile(this.g_PmtValue, sPath, Encoding.Unicode);
                g_sCurrentPmtPath = sPath;
                return true;
            }
            catch
            {
                return false;
            }
        }


        #endregion

        #region//========== Public Function Convertor ==========
        public clsCtrlDispValve_ArtPZT ConvertToArtPZT()
        {
            clsCtrlDispValve_ArtPZT rValue = null;
            try
            {
                if (this is clsCtrlDispValve_ArtPZT)
                {
                    rValue = (clsCtrlDispValve_ArtPZT)this;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        public clsCtrlDispValve_ArtSpray ConvertToArtSpary()
        {
            clsCtrlDispValve_ArtSpray rValue = null;
            try
            {
                if (this is clsCtrlDispValve_ArtSpray)
                {
                    rValue = (clsCtrlDispValve_ArtSpray)this;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        #endregion
    }
}

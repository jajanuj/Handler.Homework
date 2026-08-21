using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

using System.Net;
using System.Net.Sockets;
using FtdAdapter;
using Modbus.Data;
using Modbus.Device;
using Modbus.Utility;
using ArtData;
using ArtCommonLib;
using ArtControlLib;
using Newtonsoft.Json.Linq;
using static ArtControlLib.drvMotionAdvanTech;

namespace ArtSystem.MultiSystem
{
    public class clsCtrlHeaterModule
    {
        #region //===================== 區域變數設置 =====================

        /// <summary> 此模組的名稱 </summary>
        public string m_strMoudleName
        {
            get;
            private set;
        }
        /// <summary> 此Heater模組主要物件 </summary>
        public clsControllerBase m_CtrlHeater = null;
        /// <summary> 此Heater模組的通訊模組-SerialPort </summary>
        private SerialPort mSerialPort = null;



        public clsEnum.enuDi? eDi_OverHeat = null;
        public clsEnum.enuDi? eDi_LowerHeat = null;
        public clsEnum.enuDo? eDo_Enable = null;

        #endregion

        #region //===================== (static)變數設置 =====================

        /// <summary> 是否為模擬 </summary>
        static public bool IsSimulatorMode { get; set; }
        /// <summary> 收集所有Heater模組 </summary>
        static public Dictionary<string, clsCtrlHeaterModule> m_DicCtrlHeater = new Dictionary<string, clsCtrlHeaterModule>();
        /// <summary> 收集所有SerialPort</summary>
        static public Dictionary<string, SerialPort> m_DicSerialPort = new Dictionary<string, SerialPort>();
        /// <summary> 收集所有Cmd統一由一個執行序下達指令 </summary>
        static public List<clsCmdStruct> m_TempCmdList = new List<clsCmdStruct>();
        static private Thread mThread;
        static private bool bThreadStart = false;
        static private enuControllerType mGlobalType = enuControllerType.DTK4848_V12;
        static string strReciveDataBuffer = "";

        #endregion

        #region //===================== Enum 列舉 =====================

        public enum enuControllerType
        {
            MT48,
            XB100,
            RB100,
            NT48,
            Z_TIO,
            SDC15,
            /// <summary> 標準物料 </summary>
            DTK4848_V12,
            E5CC_Omron,
        }
        public enum enuSwitch
        {
            On,
            Off
        }
        public enum enuCmdType
        {
            Type_SetTemp,
            Type_SetOnOff,
            Type_SetTempShift
        }
        public enum enuHeaterStatus
        {
            Off,
            Heating,
            Cooling,
            Ready,
            OverHeat,
            OverHeat_DI,
        }
        #endregion
        
        #region //===================== Class =====================

        /// <summary> Cmd收集站,統一由一個執行序下達指令 </summary>
        public class clsCmdStruct
        {
            public clsCmdStruct(clsCtrlHeaterModule p_CtrlHeater, enuCmdType p_eCmdType, double tValue)
            {
                m_CtrlHeater = p_CtrlHeater;
                m_eCmdType = p_eCmdType;
                dValue = tValue;
            }
            public clsCtrlHeaterModule m_CtrlHeater = null;
            public enuCmdType m_eCmdType=  enuCmdType.Type_SetOnOff;
            public double dValue = 0;
        }
        
        #endregion

        #region //===================== 必要函式設置 =====================

        public clsCtrlHeaterModule(string p_strName)
        {
            m_strMoudleName = p_strName;
        }
        ~clsCtrlHeaterModule()
        {
            try
            {
                bThreadStart = false;
                if (mThread != null)
                {
                    mThread.Abort();
                    mThread = null;
                }
                if (m_DicSerialPort.ContainsKey(m_strMoudleName) == true)
                {
                    m_DicSerialPort[m_strMoudleName].Close();
                    m_DicSerialPort.Remove(m_strMoudleName);
                }
                m_CtrlHeater.mSerialPort = null;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }

        }

        public bool InitialHeater(enuControllerType p_eControllerType, int intStationNo, string PortNumber,
            int BaudRate, int DataBits, Handshake HandshakeType, int Timeout, StopBits pStopBits, Parity pParity, bool bDtr = true)
        {
            bool IsAddSuccess = true;
            try
            {

                #region//建立執行序
                bThreadStart = true;
                if (mThread == null)
                {
                    mThread = new Thread(new ThreadStart(Thread_RefreshFunc));
                    mThread.IsBackground = true;//背景執行緒，若沒釋放，CLR會幫忙釋放
                    mThread.Start();
                }
                #endregion
                #region//建立SerialPort
                if (m_DicSerialPort.ContainsKey(PortNumber) == false)
                {
                    m_DicSerialPort.Add(PortNumber, new SerialPort());
                    try
                    {
                        m_DicSerialPort[PortNumber].BaudRate = BaudRate;
                        m_DicSerialPort[PortNumber].DataBits = DataBits;
                        m_DicSerialPort[PortNumber].Handshake = HandshakeType;
                        m_DicSerialPort[PortNumber].PortName = PortNumber;
                        m_DicSerialPort[PortNumber].StopBits = pStopBits;
                        m_DicSerialPort[PortNumber].Parity = pParity;
                        m_DicSerialPort[PortNumber].DtrEnable = bDtr;
                        m_DicSerialPort[PortNumber].ReadTimeout = Timeout;
                        m_DicSerialPort[PortNumber].WriteTimeout = Timeout;
                        m_DicSerialPort[PortNumber].DataReceived += new SerialDataReceivedEventHandler(mSerialPort_DataReceived);
                        m_DicSerialPort[PortNumber].ErrorReceived += new SerialErrorReceivedEventHandler(mSerialPort_ErrorReceived);
                    }
                    catch (Exception ex)
                    {
                        clsArtSystem.CatchLog(ex);
                    }
                }
                mSerialPort = m_DicSerialPort[PortNumber];
                #endregion
                #region//建立加熱模組
                switch (p_eControllerType)
                {
                    case enuControllerType.MT48:
                        m_CtrlHeater = new clsMT48Controllor(intStationNo, ref mSerialPort);
                        break;

                    case enuControllerType.XB100:
                        m_CtrlHeater = new clsXB100Controllor(intStationNo, ref mSerialPort);
                        break;

                    case enuControllerType.RB100:
                        m_CtrlHeater = new clsXB100Controllor(intStationNo, ref mSerialPort);
                        break;

                    case enuControllerType.NT48:
                        m_CtrlHeater = new clsNT48Controllor(intStationNo, ref mSerialPort);
                        break;

                    case enuControllerType.Z_TIO:
                        m_CtrlHeater = new clsZ_TIOControllor(intStationNo, ref mSerialPort);
                        break;

                    case enuControllerType.SDC15:
                        m_CtrlHeater = new clsSDC15Controllor(intStationNo, ref mSerialPort);
                        break;

                    case enuControllerType.DTK4848_V12:
                        m_CtrlHeater = new clsDTKV4848_V12Controllor(intStationNo, ref mSerialPort);
                        break;

                    case enuControllerType.E5CC_Omron:
                        m_CtrlHeater = new clsE5CC_Omron(intStationNo, ref mSerialPort);
                        break;

                    default:
                        IsAddSuccess = false;
                        break;
                }
                if (m_DicCtrlHeater.ContainsKey(m_strMoudleName) == false)
                {
                    m_DicCtrlHeater.Add(m_strMoudleName, this);
                }
                else
                {
                    m_DicCtrlHeater[m_strMoudleName] = this;
                }
                #endregion
            }
            catch (Exception ex)
            {
                IsAddSuccess = false;
                clsArtSystem.CatchLog(ex);
            }
            return IsAddSuccess;
        }

        public void Open()
        {
            try
            {
                if (mSerialPort.IsOpen == false)
                {
                    List<string> ports = SerialPort.GetPortNames().ToList<string>();
                    if (ports.Contains(mSerialPort.PortName) == true)
                    {
                        mSerialPort.Open();
                        if (ucHeaterModuleSetting.GetSingleton().mPmt.mDic_mPmtValue.ContainsKey(this.m_strMoudleName) == true)
                        {
                            string strShiftOffsetPmt = ucHeaterModuleSetting.GetSingleton().mPmt.mDic_mPmtValue[this.m_strMoudleName][clsPmtHeaterModule.enuPmtName.enuSys_Temp_ShiftOffset];
                            if (Enum.IsDefined(typeof(clsEnum.enuPmtName), strShiftOffsetPmt) == true)
                            {
                                clsEnum.enuPmtName ePmtName = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), strShiftOffsetPmt);
                                double dShiftOffset = ucParameter.GetValueDouble(ePmtName);
                                this.SetTempShift(dShiftOffset);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m_CtrlHeater.bIsConnected = false;
                clsArtSystem.CatchLog(ex);
            }
        }
        public void Close()
        {
            try
            {
                mSerialPort.Close();
                m_CtrlHeater.bIsConnected = false;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        public bool IsOpen()
        {
            return mSerialPort.IsOpen;
        }
        public bool IsConnected()
        {
            if (m_CtrlHeater != null)
            {
                return m_CtrlHeater.bIsConnected;
            }
            return false;
        }
        public enuHeaterStatus GetStatus()
        {
            enuHeaterStatus rValue = enuHeaterStatus.Off;
            if (GetOnOff() == true 
                || clsArtSystem.bIsSoftwareSimulate == true)
            {
                double dCurrentTemp = GetCurrentTemp();
                if (ucHeaterModuleSetting.GetSingleton().mPmt.mDic_mPmtValue.ContainsKey(this.m_strMoudleName) == true)
                {
                    Dictionary<clsPmtHeaterModule.enuPmtName, string> pPmt = ucHeaterModuleSetting.GetSingleton().mPmt.mDic_mPmtValue[this.m_strMoudleName];
                    double dTargetTemp = m_CtrlHeater.m_dSettingTemp;
                    #region//if(OverHeatID == true)
                    if(Enum.IsDefined(typeof(clsEnum.enuDi), pPmt[clsPmtHeaterModule.enuPmtName.eDi_OverHeat_BType]) == true)
                    {
                        clsEnum.enuDi eDI = (clsEnum.enuDi)Enum.Parse(typeof(clsEnum.enuDi), pPmt[clsPmtHeaterModule.enuPmtName.eDi_OverHeat_BType]);
                        if (clsDioCtrl.GetDi(eDI) == false)
                        {
                            rValue = enuHeaterStatus.OverHeat_DI;
                            return rValue;
                        }
                    }
                    #endregion

                    if (dCurrentTemp > ConvertDouble(pPmt[clsPmtHeaterModule.enuPmtName.Temp_Limit]))
                    {
                        rValue = enuHeaterStatus.OverHeat;
                    }
                    else if (this.GetOnOff() == false)
                    {
                        rValue = enuHeaterStatus.Off;
                    }
                    else if (Math.Abs(dCurrentTemp - dTargetTemp)
                        <= ConvertDouble(pPmt[clsPmtHeaterModule.enuPmtName.Temp_ErrorRange]) == true)
                    {
                        rValue = enuHeaterStatus.Ready;
                    }
                    else if (dCurrentTemp > dTargetTemp)
                    {
                        rValue = enuHeaterStatus.Cooling;
                    }
                    else
                    {
                        rValue = enuHeaterStatus.Heating;
                    }
                }
            }
            return rValue;
        }
        #endregion

        #region //===================== Private =====================
        private double ConvertDouble(string strValue)
        {
            double rValue = 0;
            double.TryParse(strValue, out rValue);
            return rValue;
        }
        #endregion

        #region //===================== Static Private (通訊) =====================
        static private void Thread_RefreshFunc()
        {
            while (bThreadStart && clsArtSystem.bIsProgramClosed == false)
            {
                if (0 < m_TempCmdList.Count)
                {
                    if (!bThreadStart)
                        break;
                    if (m_TempCmdList.ElementAt(0).m_CtrlHeater.IsOpen() == false)
                    {
                        m_TempCmdList.RemoveAt(0);
                    }
                    else
                    {
                        switch (m_TempCmdList.ElementAt(0).m_eCmdType)
                        {
                            case enuCmdType.Type_SetTemp:
                                SetTemp_SendCmd(m_TempCmdList.ElementAt(0));
                                m_TempCmdList.RemoveAt(0);
                                break;
                            case enuCmdType.Type_SetOnOff:
                                SetOnOff_SendCmd(m_TempCmdList.ElementAt(0));
                                m_TempCmdList.RemoveAt(0);
                                break;
                            case enuCmdType.Type_SetTempShift:
                                SetTempShift_SendCmd(m_TempCmdList.ElementAt(0));
                                m_TempCmdList.RemoveAt(0);
                                break;
                            default:
                                break;
                        }
                    }
                    System.Threading.Thread.Sleep(300);
                }
                else if(clsArtSystem.bIsProgramOpenFinish == true)
                {

                    int iMaxControllerNum = m_DicCtrlHeater.Count;
                    for (int iControlIndex = 0; iControlIndex < iMaxControllerNum; iControlIndex++)
                    {
                        clsCtrlHeaterModule clsHeatModule =  m_DicCtrlHeater.ElementAt(iControlIndex).Value;
                        clsControllerBase clsHeatCtrl = m_DicCtrlHeater.ElementAt(iControlIndex).Value.m_CtrlHeater;
                        if (!bThreadStart)
                            break;
                        #region//Simulate Temperature Working
                        if (IsSimulatorMode == true)
                        {
                            if (clsHeatCtrl.m_bOnOff == true)
                            {
                                if (clsHeatCtrl.m_dCurrentTemp < clsHeatCtrl.m_dSettingTemp)
                                {
                                    clsHeatCtrl.m_dCurrentTemp += 0.1;
                                }
                                else if (clsHeatCtrl.m_dCurrentTemp > clsHeatCtrl.m_dSettingTemp)
                                {
                                    if (clsHeatCtrl.m_dCurrentTemp > 20)
                                    {
                                        clsHeatCtrl.m_dCurrentTemp -= 0.1;
                                    }
                                }
                            }
                            else
                            {
                                if (clsHeatCtrl.m_dCurrentTemp > 20)
                                {
                                    clsHeatCtrl.m_dCurrentTemp -= 0.1;
                                }
                            }
                        }
                        #endregion
                        //#region//If OverHeat, Off //讓Handler執行(主要是要發Alarm)
                        //enuHeaterStatus eNowStatus  = clsHeatModule.GetStatus();
                        //if(eNowStatus == enuHeaterStatus.OverHeat
                        //    || eNowStatus == enuHeaterStatus.OverHeat_DI)
                        //{
                        //    clsHeatModule.SetOnOff(enuSwitch.Off);
                        //}
                        //#endregion

                        if (clsHeatCtrl.mSerialPort.IsOpen == true)
                        {
                            enuControllerType mType = clsHeatCtrl.m_eunType;
                            mGlobalType = clsHeatCtrl.m_eunType;
                            #region //Continue Get Current Temp And OnOff
                            if (mType == enuControllerType.MT48)
                            {
                                mGlobalType = enuControllerType.MT48;
                                double dblMT48Temp = GetTempInternal(clsHeatCtrl);
                                if (clsHeatCtrl.m_dBufferTemp == 0)
                                {
                                    clsHeatCtrl.m_dBufferTemp = dblMT48Temp;
                                }

                                if (Math.Abs(dblMT48Temp - clsHeatCtrl.m_dBufferTemp) < 1)
                                {
                                    clsHeatCtrl.m_dBufferTemp = dblMT48Temp;
                                    clsHeatCtrl.m_dCurrentTemp = dblMT48Temp;
                                }
                                else
                                {
                                    clsHeatCtrl.m_dBufferTemp = dblMT48Temp;
                                }

                                int iOnOff = GetOnOffInternal(clsHeatCtrl);

                                if (clsHeatCtrl.m_bOnOffBuffer == iOnOff)
                                {

                                    clsHeatCtrl.m_bOnOff = iOnOff == 1;
                                }
                                else
                                {
                                    clsHeatCtrl.m_bOnOffBuffer = iOnOff;
                                }
                            }
                            if (mType == enuControllerType.XB100)
                            {
                                mGlobalType = enuControllerType.XB100;
                                double dblXB100Temp = GetTempInternal(clsHeatCtrl);
                                if (clsHeatCtrl.m_dBufferTemp == 0)
                                {
                                    clsHeatCtrl.m_dBufferTemp = dblXB100Temp;
                                }

                                if (Math.Abs(dblXB100Temp - clsHeatCtrl.m_dBufferTemp) < 1)
                                {
                                    clsHeatCtrl.m_dBufferTemp = dblXB100Temp;
                                    clsHeatCtrl.m_dCurrentTemp = dblXB100Temp;
                                }
                                else
                                {
                                    clsHeatCtrl.m_dBufferTemp = dblXB100Temp;
                                }

                                GetOnOffInternal(clsHeatCtrl);

                            }
                            if (mType == enuControllerType.NT48
                                || mType == enuControllerType.Z_TIO
                                || mType == enuControllerType.SDC15
                                || mType == enuControllerType.DTK4848_V12
                                )
                            {
                                mGlobalType = mType;
                                double CurrentTemperature = GetTempInternal(clsHeatCtrl);
                                if (clsHeatCtrl.m_dBufferTemp == 0)
                                {
                                    clsHeatCtrl.m_dBufferTemp = CurrentTemperature;
                                }

                                if (Math.Abs(CurrentTemperature - clsHeatCtrl.m_dBufferTemp) < 1)
                                {
                                    clsHeatCtrl.m_dBufferTemp = CurrentTemperature;
                                    clsHeatCtrl.m_dCurrentTemp = CurrentTemperature;
                                }
                                else
                                {
                                    clsHeatCtrl.m_dBufferTemp = CurrentTemperature;
                                }

                                int iOnOff = GetOnOffInternal(clsHeatCtrl);

                                if (clsHeatCtrl.m_bOnOffBuffer == iOnOff)
                                {

                                    clsHeatCtrl.m_bOnOff = iOnOff == 1;

                                }
                                else
                                {
                                    clsHeatCtrl.m_bOnOffBuffer = iOnOff;
                                }
                            }
                            #endregion
                            System.Threading.Thread.Sleep(300);

                        }
                        else
                        {
                            System.Threading.Thread.Sleep(300);
                            clsHeatCtrl.bIsConnected = false;
                        }
                    }
                    
                }
            }
        }

        static private void mSerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

        }
        static private void mSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (sender is SerialPort)
            {
                SerialPort tSerialPort = (SerialPort)sender;
                if (mGlobalType == enuControllerType.MT48)
                {
                    try
                    {
                        byte[] tmpData = new byte[100];

                        tSerialPort.Read(tmpData, 0, tmpData.Count());

                        string strData = "";

                        for (int i = 0; i < tmpData.Count(); i++)
                        {
                            if (tmpData[i] != 0)
                            {
                                strData += ((char)tmpData[i]).ToString();
                            }
                        }
                        strReciveDataBuffer += strData;
                    }
                    catch (Exception ex)
                    {
                        clsArtSystem.CatchLog(ex);
                    }
                }
            }
        }
        static private void SetOnOff_SendCmd(clsCmdStruct p_Cmd)
        {
            System.Diagnostics.Stopwatch TimeCount = new System.Diagnostics.Stopwatch();
            if (p_Cmd.m_CtrlHeater != null)
            {
                if (p_Cmd.m_CtrlHeater.m_CtrlHeater != null)
                {
                    clsControllerBase p_CtrlHeater = p_Cmd.m_CtrlHeater.m_CtrlHeater;
                    mGlobalType = p_CtrlHeater.m_eunType;
                    enuSwitch mSwitch = p_Cmd.dValue == (double)enuSwitch.On ? enuSwitch.On : enuSwitch.Off;
                    switch (mGlobalType)
                    {
                        case enuControllerType.MT48:
                            #region //MT48
                            {
                                mGlobalType = enuControllerType.MT48;
                                strReciveDataBuffer = "";

                                if (mSwitch == enuSwitch.On)
                                {
                                    p_CtrlHeater.SetHeatStart();
                                }
                                else
                                {
                                    p_CtrlHeater.SetHeatStop();
                                }

                                string strReturnTxt = "@" + string.Format("{0:00}", p_CtrlHeater.m_StationNumber) + "W";

                                TimeCount.Restart();

                                while (strReciveDataBuffer.Length <= 4
                                    || strReciveDataBuffer.Substring(0, 4) != strReturnTxt
                                    || strReciveDataBuffer.Substring(strReciveDataBuffer.Length - 1, 1) != Convert.ToChar(13).ToString())
                                {
                                    if (TimeCount.ElapsedMilliseconds > 100) { break; }

                                    //SpinWait.SpinUntil(() => false, 10);
                                    Thread.Sleep(10);
                                }
                                TimeCount.Stop();
                            }
                            #endregion
                            break;

                        case enuControllerType.XB100:
                            #region //XB100
                            {
                                mGlobalType = enuControllerType.XB100;
                                strReciveDataBuffer = "";

                                if (mSwitch == enuSwitch.On)
                                {
                                    p_CtrlHeater.SetHeatStart();
                                }
                                else
                                {
                                    p_CtrlHeater.SetHeatStop();
                                }

                                byte[] tmpData = System.Text.Encoding.Default.GetBytes(strReciveDataBuffer);

                                TimeCount.Restart();

                                while (tmpData.Count() != 8)
                                {
                                    tmpData = System.Text.Encoding.Default.GetBytes(strReciveDataBuffer);
                                    if (TimeCount.ElapsedMilliseconds > 100) { break; }
                                    Thread.Sleep(10);
                                }

                                TimeCount.Stop();

                            }
                            #endregion
                            break;

                        case enuControllerType.NT48:
                            #region //NT48
                            {
                                mGlobalType = enuControllerType.NT48;
                                strReciveDataBuffer = "";

                                if (mSwitch == enuSwitch.On)
                                {
                                    p_CtrlHeater.SetHeatStart();
                                }
                                else
                                {
                                    p_CtrlHeater.SetHeatStop();
                                }
                            }
                            #endregion
                            break;

                        case enuControllerType.Z_TIO:
                            #region //Z_TIO
                            {
                                mGlobalType = enuControllerType.Z_TIO;
                                strReciveDataBuffer = "";
                                if (mSwitch == enuSwitch.On)
                                {
                                    p_CtrlHeater.SetHeatStart();
                                }
                                else
                                {
                                    p_CtrlHeater.SetHeatStop();
                                }

                                bool btempValue = p_CtrlHeater.m_bOnOff;
                                int iTempControllerID = p_CtrlHeater.m_StationNumber / 4;
                                string sTempSerialPort = p_CtrlHeater.mSerialPort.PortName;
                                foreach (clsCtrlHeaterModule tController in m_DicCtrlHeater.Values)
                                {
                                    if (tController.m_CtrlHeater.m_eunType == enuControllerType.Z_TIO)
                                    {
                                        if (tController.mSerialPort.PortName == sTempSerialPort)
                                        {
                                            if (tController.m_CtrlHeater.m_StationNumber / 4 == iTempControllerID)
                                            {
                                                tController.m_CtrlHeater.m_bOnOff = btempValue;
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                            break;

                        case enuControllerType.SDC15:
                            #region //SDC15
                            {
                                mGlobalType = enuControllerType.SDC15;
                                strReciveDataBuffer = "";
                                if (mSwitch == enuSwitch.On)
                                {
                                    p_CtrlHeater.SetHeatStart();
                                }
                                else
                                {
                                    p_CtrlHeater.SetHeatStop();
                                }
                            }
                            #endregion
                            break;

                        case enuControllerType.DTK4848_V12:
                            #region //DTK4848_V12
                            {
                                mGlobalType = enuControllerType.DTK4848_V12;
                                strReciveDataBuffer = "";

                                if (mSwitch == enuSwitch.On)
                                {
                                    p_CtrlHeater.SetHeatStart();
                                }
                                else
                                {
                                    p_CtrlHeater.SetHeatStop();
                                }
                            }
                            #endregion
                            break;

                        default:
                            break;
                    }
                }
            }

            TimeCount.Restart();
            while (TimeCount.ElapsedMilliseconds < 5)
            {
                Thread.Sleep(5);
            }
        }
        static private void SetTemp_SendCmd(clsCmdStruct p_Cmd)
        {
            try
            {
                System.Diagnostics.Stopwatch TimeCount = new System.Diagnostics.Stopwatch();

                int intRetry = 0;
                if (p_Cmd.m_CtrlHeater != null)
                {
                    if (p_Cmd.m_CtrlHeater.m_CtrlHeater != null)
                    {
                        double dblTemp = p_Cmd.dValue;
                        clsControllerBase p_CtrlHeater = p_Cmd.m_CtrlHeater.m_CtrlHeater;
                        mGlobalType = p_CtrlHeater.m_eunType;
                        while (intRetry < 3)
                        {
                            p_CtrlHeater.m_dSettingTemp = dblTemp;

                            if (mGlobalType == enuControllerType.MT48)
                            {
                                #region//MT48
                                p_CtrlHeater.SetTemp(dblTemp);
                                string strReturnTxt = "@" + string.Format("{0:00}", p_CtrlHeater.m_StationNumber) + "W";

                                TimeCount.Restart();
                                while (strReciveDataBuffer == ""
                                    || strReciveDataBuffer.Substring(0, 4) != strReturnTxt
                                    || strReciveDataBuffer.Substring(strReciveDataBuffer.Length - 1, 1) != Convert.ToChar(13).ToString())
                                {
                                    if (TimeCount.ElapsedMilliseconds > 100) { break; }
                                    Thread.Sleep(10);
                                }
                                TimeCount.Stop();

                                if (strReciveDataBuffer != "")
                                {
                                    break;
                                }
                                strReciveDataBuffer = "";
                                #endregion
                            }
                            else if (mGlobalType == enuControllerType.XB100)
                            {
                                #region//XB100
                                if (strReciveDataBuffer != "")
                                {
                                    TimeCount.Restart();

                                    while (TimeCount.ElapsedMilliseconds < 100)
                                    {
                                        if (strReciveDataBuffer != "") { break; }
                                    }
                                    strReciveDataBuffer = "";
                                }
                                p_CtrlHeater.SetTemp(dblTemp);
                                byte[] tmpData = System.Text.Encoding.Default.GetBytes(strReciveDataBuffer);
                                TimeCount.Restart();
                                while (strReciveDataBuffer.Length < 8)
                                {
                                    if (TimeCount.ElapsedMilliseconds > 100) { break; }
                                    Thread.Sleep(10);
                                }
                                TimeCount.Stop();
                                if (strReciveDataBuffer != "") { break; }
                                strReciveDataBuffer = "";
                                #endregion
                            }
                            else if (mGlobalType == enuControllerType.NT48
                                    || mGlobalType == enuControllerType.Z_TIO
                                    || mGlobalType == enuControllerType.SDC15
                                    || mGlobalType == enuControllerType.DTK4848_V12
                                    )
                            {
                                #region//Other
                                if (strReciveDataBuffer != "")
                                {
                                    TimeCount.Restart();
                                    while (TimeCount.ElapsedMilliseconds < 1000)
                                    {
                                        if (strReciveDataBuffer != "") { break; }
                                    }
                                    strReciveDataBuffer = "";
                                    TimeCount.Stop();
                                }
                                p_CtrlHeater.SetTemp(dblTemp);
                                if (strReciveDataBuffer != "")
                                {
                                    break;
                                }
                                strReciveDataBuffer = "";
                                #endregion
                            }

                            intRetry++;
                            Thread.Sleep(10);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        static private void SetTempShift_SendCmd(clsCmdStruct p_Cmd)
        {
            try
            {
                System.Diagnostics.Stopwatch TimeCount = new System.Diagnostics.Stopwatch();
                if (p_Cmd.m_CtrlHeater != null)
                {
                    if (p_Cmd.m_CtrlHeater.m_CtrlHeater != null)
                    {
                        double dblOffset = p_Cmd.dValue;
                        clsControllerBase p_CtrlHeater = p_Cmd.m_CtrlHeater.m_CtrlHeater;
                        mGlobalType = p_CtrlHeater.m_eunType;
                        if (mGlobalType == enuControllerType.MT48)
                        {
                            #region//MT48
                            p_CtrlHeater.SetTempShift(dblOffset);
                            string strReturnTxt = "@" + string.Format("{0:00}", p_CtrlHeater.m_StationNumber) + "W";
                            TimeCount.Restart();
                            while (strReciveDataBuffer == ""
                                || strReciveDataBuffer.Substring(0, 4) != strReturnTxt
                                || strReciveDataBuffer.Substring(strReciveDataBuffer.Length - 1, 1) != Convert.ToChar(13).ToString())
                            {
                                if (TimeCount.ElapsedMilliseconds > 100) { break; }
                                Thread.Sleep(10);
                            }
                            TimeCount.Stop();

                            strReciveDataBuffer = "";
                            #endregion
                        }
                        else if (mGlobalType == enuControllerType.XB100)
                        {
                            #region//XB100
                            if (strReciveDataBuffer != "")
                            {
                                TimeCount.Restart();

                                while (TimeCount.ElapsedMilliseconds < 1000)
                                {
                                    if (strReciveDataBuffer != "") { break; }
                                }

                                strReciveDataBuffer = "";
                            }
                            p_CtrlHeater.SetTempShift(dblOffset);

                            byte[] tmpData = System.Text.Encoding.Default.GetBytes(strReciveDataBuffer);

                            TimeCount.Restart();
                            while (strReciveDataBuffer.Length < 8)
                            {
                                if (TimeCount.ElapsedMilliseconds > 100) { break; }
                                //SpinWait.SpinUntil(() => false, 10);
                                Thread.Sleep(10);
                            }
                            TimeCount.Stop();

                            strReciveDataBuffer = "";
                            #endregion
                        }
                        else if (mGlobalType == enuControllerType.NT48
                                || mGlobalType == enuControllerType.Z_TIO
                                || mGlobalType == enuControllerType.SDC15
                                || mGlobalType == enuControllerType.DTK4848_V12
                                )
                        {
                            #region//Other
                            if (strReciveDataBuffer != "")
                            {
                                TimeCount.Restart();
                                while (TimeCount.ElapsedMilliseconds < 1000)
                                {
                                    if (strReciveDataBuffer != "") { break; }
                                }
                                strReciveDataBuffer = "";
                                TimeCount.Stop();
                            }
                            p_CtrlHeater.SetTempShift(dblOffset);
                            strReciveDataBuffer = "";
                            #endregion
                        }
                    }


                    Thread.Sleep(5);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        static private int GetOnOffInternal(clsControllerBase p_CtrlHeater)
        {
            int dblTemp = 0;//Run:1, Stop:0
            System.Diagnostics.Stopwatch TimeCount = new System.Diagnostics.Stopwatch();
            if (p_CtrlHeater != null)
            {
                mGlobalType = p_CtrlHeater.m_eunType;
                if (p_CtrlHeater.m_eunType == enuControllerType.MT48)
                {
                    #region//MT48
                    p_CtrlHeater.GetOnOff();
                    TimeCount.Restart();
                    try
                    {
                        while (strReciveDataBuffer == "")
                        {
                            if (TimeCount.ElapsedMilliseconds > 100)
                            {
                                TimeCount.Reset();
                                break;
                            }
                            Thread.Sleep(10);
                        }

                        TimeCount.Restart();
                        while (true)
                        {
                            if (strReciveDataBuffer.Substring(0, 1) != "@"
                                || strReciveDataBuffer.Substring(strReciveDataBuffer.Length - 1, 1) != Convert.ToChar(13).ToString())
                            {
                                break;
                            }
                            if (TimeCount.ElapsedMilliseconds > 1000)
                            {
                                TimeCount.Reset();
                                break;
                            }
                            Thread.Sleep(10);
                        }
                        TimeCount.Stop();
                    }
                    catch (Exception ex)
                    {
                        clsArtSystem.CatchLog(ex);
                    }

                    try
                    {
                        if (strReciveDataBuffer != ""
                            && (strReciveDataBuffer.Substring(0, 1) == "@"
                            && strReciveDataBuffer.Substring(12, 1) == Convert.ToChar(13).ToString()))
                        {
                            if (strReciveDataBuffer.Substring(3, 1) == "R" && strReciveDataBuffer.Length >= 10)
                            {
                                if (0 == Convert.ToInt16(strReciveDataBuffer.Substring(6, 4)))
                                    dblTemp = 1;
                                else
                                    dblTemp = 0;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        strReciveDataBuffer = "";
                        clsArtSystem.CatchLog(ex);
                    }
                    strReciveDataBuffer = "";
                    #endregion
                }
                else if(mGlobalType == enuControllerType.XB100)
                {
                    if (0 == p_CtrlHeater.GetOnOff())
                    {
                        dblTemp = 1;
                    }
                }
                else 
                //if ( mGlobalType == enuControllerType.NT48
                //    || mGlobalType == enuControllerType.Z_TIO
                //    || mGlobalType == enuControllerType.SDC15
                //    || mGlobalType == enuControllerType.DTK4848_V12
                //    || mGlobalType == enuControllerType.E5CC_Omron
                //    )
                {
                    dblTemp = p_CtrlHeater.GetOnOff();
                }
            }
            return dblTemp;
        }
        static private double GetTempInternal(clsControllerBase p_CtrlHeater)
        {
            double dblTemp = -1;
            if (p_CtrlHeater != null)
            {
                mGlobalType = p_CtrlHeater.m_eunType;

                System.Diagnostics.Stopwatch TimeCount = new System.Diagnostics.Stopwatch();

                int intRetry = 0;

                while (intRetry < 3)
                {
                    if (p_CtrlHeater.m_eunType == enuControllerType.MT48)
                    {
                        #region MT48
                        p_CtrlHeater.GetTemp();
                        TimeCount.Restart();
                        try
                        {
                            while (strReciveDataBuffer == "")
                            {
                                if (TimeCount.ElapsedMilliseconds > 100)
                                {
                                    TimeCount.Reset();
                                    break;
                                }
                                Thread.Sleep(10);
                            }

                            TimeCount.Restart();

                            while (strReciveDataBuffer == ""
                                || (strReciveDataBuffer.Substring(0, 1) == "@"
                                    && Convert.ToInt32(strReciveDataBuffer.Substring(1, 2)) == p_CtrlHeater.m_StationNumber
                                    && strReciveDataBuffer.Contains('\r')) == false)
                            {
                                if (TimeCount.ElapsedMilliseconds > 10000)
                                {
                                    TimeCount.Reset();
                                    break;
                                }
                                Thread.Sleep(10);
                            }
                            TimeCount.Stop();
                        }
                        catch (Exception ex)
                        {
                            clsArtSystem.CatchLog(ex);
                        }

                        try
                        {
                            if (strReciveDataBuffer != "" && (strReciveDataBuffer.Substring(0, 1) == "@" && strReciveDataBuffer.Substring(12, 1) == Convert.ToChar(13).ToString()))
                            {
                                if (strReciveDataBuffer.Substring(3, 1) == "R" && strReciveDataBuffer.Length >= 10)
                                {
                                    dblTemp = Convert.ToDouble(strReciveDataBuffer.Substring(6, 4)) / 10;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            strReciveDataBuffer = "";
                            clsArtSystem.CatchLog(ex);
                        }

                        strReciveDataBuffer = "";
                        #endregion
                    }
                    else
                    //if (p_CtrlHeater.m_eunType == enuControllerType.XB100
                    //    || p_CtrlHeater.m_eunType == enuControllerType.NT48
                    //    || p_CtrlHeater.m_eunType == enuControllerType.Z_TIO
                    //    || p_CtrlHeater.m_eunType == enuControllerType.SDC15
                    //    || p_CtrlHeater.m_eunType == enuControllerType.DTK4848_V12
                    //    )
                    {
                        strReciveDataBuffer = "";
                        dblTemp = p_CtrlHeater.GetTemp();
                        strReciveDataBuffer = "";
                    }
                    if (dblTemp > 0)
                    {
                        p_CtrlHeater.bIsConnected = true;
                        break;
                    }
                    p_CtrlHeater.bIsConnected = false;
                    intRetry++;
                }
            }
            return dblTemp;
        }

        #endregion

        #region //===================== Class Controllor(所有溫空器模組) =====================

        public class clsControllerBase
        {
            public SerialPort mSerialPort;
            public enuControllerType m_eunType;
            public int m_StationNumber;
            public double m_dCurrentTemp = 0;
            public double m_dBufferTemp = 0;
            public double m_dSettingTemp;
            public bool m_bOnOff;
            public int m_bOnOffBuffer = 0;
            public bool bIsConnected = false;

            public bool GetSerialPortStatus()
            {
                return mSerialPort.IsOpen;
            }
            public void SetSerialPort(bool Status)
            {
                if (Status == true)
                {
                    if (mSerialPort.IsOpen == false) { mSerialPort.Open(); }
                }
                else
                {
                    if (mSerialPort.IsOpen == true) { mSerialPort.Close(); }
                }
            }

            public virtual double GetTemp() { return -1; }
            public virtual void SetTemp(double dblTemp) { }
            public virtual void SetTempShift(double dblShift) { }
            public virtual void SetHeatStart() { }
            public virtual void SetHeatStop() { }
            public virtual int GetOnOff() { return -1; }
            public virtual void SetData(string strCommand) { }

            public string DecToHex(int intValue)
            {
                return string.Format("{0:X2}", intValue);
            }
            public string GetCRC(string strValue)
            {
                char[] chrValues = new char[strValue.Length];

                int intXorValue = 0;

                for (int i = 0; i < strValue.Length; i++)
                {
                    chrValues[i] = Convert.ToChar(strValue.Substring(i, 1));

                    intXorValue = intXorValue ^ ((int)chrValues[i]);
                }
                return DecToHex(intXorValue);
            }
        }
        public class clsMT48Controllor : clsControllerBase
        {
            #region //=====================  必要函式設置 =====================
            public clsMT48Controllor(int StationNo, ref SerialPort refSerialPort)
            {
                mSerialPort = refSerialPort;
                m_StationNumber = StationNo;

                m_eunType = enuControllerType.MT48;
            }

            #endregion

            override public double GetTemp()
            {
                string strStationNo = string.Format("{0:00}", m_StationNumber);
                string strData = "@" + strStationNo + "R" + "19";

                SetData(strData);
                return 0;
            }

            override public void SetTemp(double dblTemp)
            {
                string strStationNo = string.Format("{0:00}", m_StationNumber);
                string strTemp = string.Format("{0:0000}", dblTemp * 10);
                string strData = "@" + strStationNo + "W" + "16" + strTemp;

                SetData(strData);
            }

            //ytc+ 20130805+ 新增溫控器offset string
            override public void SetTempShift(double dblShift)
            {
                string strStationNo = string.Format("{0:00}", m_StationNumber);
                string strShift = string.Format("{0:0000}", dblShift * 10);
                string strData = "@" + strStationNo + "W" + "13" + strShift;

                SetData(strData);
            }

            override public void SetHeatStart()
            {
                string strStationNo = string.Format("{0:00}", m_StationNumber);
                string strData = "@" + strStationNo + "W" + "25" + "0000";

                SetData(strData);
            }

            override public void SetHeatStop()
            {
                string strStationNo = string.Format("{0:00}", m_StationNumber);

                string strData = "@" + strStationNo + "W" + "25" + "0001";

                SetData(strData);
            }

            override public int GetOnOff()
            {
                string strStationNo = string.Format("{0:00}", m_StationNumber);
                string strData = "@" + strStationNo + "R" + "25";

                SetData(strData);
                return 0;
            }

            override public void SetData(string strCommand)
            {
                string strCRC = GetCRC(strCommand);

                string strEnd = Convert.ToChar(13).ToString();

                mSerialPort.WriteLine(strCommand + strCRC + strEnd);
            }

        }
        public class clsXB100Controllor : clsControllerBase
        {
            #region //=====================  必要函式設置 =====================
            public clsXB100Controllor(int StationNo, ref SerialPort refSerialPort)
            {
                mSerialPort = refSerialPort;
                m_StationNumber = StationNo;

                m_eunType = enuControllerType.XB100;
            }

            #endregion

            override public double GetTemp()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 0;//Process Value
                ushort numInputs = 1;
                try
                {
                    int iDigits = GetDigits();

                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);

                    if (iDigits != 0 && iDigits != -1)
                    {
                        return (double)inputs[0] / (iDigits * 10);
                    }
                    else
                        // return inputs[0]  20190829 wen fix sometimes show 450
                        return (double)inputs[0] / 10;
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return -1;
                }
            }

            override public void SetTemp(double dblTemp)
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 6;//Set Value(SV1)
                ushort numSetValue = (ushort)(dblTemp);
                try
                {
                    int iDigits = GetDigits();
                    if (iDigits != 0 && iDigits != -1)
                        numSetValue = (ushort)(dblTemp * iDigits * 10);

                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, numSetValue);
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            override public void SetTempShift(double dblTemp)
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 23;
                ushort numSetValue = (ushort)(dblTemp * 10);
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, numSetValue);
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            override public int GetOnOff()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 25;//run: 0 ;stop:1
                ushort numInputs = 1;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);
                    if (inputs[0] == 0)
                    {
                        m_bOnOff = true;
                        return 1;
                    }
                    else
                    {
                        m_bOnOff = false;
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return -1;
                }
            }

            override public void SetHeatStart()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 25;//run: 0 ;stop:1
                ushort numSetValue = 0;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, numSetValue);
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            override public void SetHeatStop()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 25;//run: 0 ;stop:1
                ushort numSetValue = 1;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, numSetValue);
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            /// <summary>
            /// 取得小數點位數
            /// </summary>
            /// <returns>小數點位數</returns>
            private int GetDigits()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 98;
                ushort numInputs = 1;

                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);
                    return inputs[0];
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return -1;
                }
            }

        }
        public class clsNT48Controllor : clsControllerBase
        {
            #region //=====================  必要函式設置 =====================
            public clsNT48Controllor(int StationNo, ref SerialPort refSerialPort)
            {
                mSerialPort = refSerialPort;
                m_StationNumber = StationNo;

                m_eunType = enuControllerType.NT48;
            }

            #endregion

            override public double GetTemp()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 257;//Process Value
                ushort numInputs = 1;
                try
                {
                    int iDigits = GetDigits();

                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateAscii(mSerialPort);
                    ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);
                    if (iDigits != 0 && iDigits != -1)
                    {
                        return (double)inputs[0] / (iDigits * 10);
                    }
                    else
                        return (double)inputs[0];
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return -1;
                }
            }

            override public void SetTemp(double dblTemp)
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 35;//Setting Value 
                ushort numSetValue = (ushort)(dblTemp);
                try
                {
                    int iDigits = GetDigits();
                    if (iDigits != 0 && iDigits != -1)
                        numSetValue = (ushort)(dblTemp * iDigits * 10);

                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateAscii(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, numSetValue);
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            //ytc+ 20130805+ 新增溫控器offset string
            override public void SetTempShift(double dblShift)
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 26;//Setting Value
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateAscii(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, (ushort)dblShift);
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }

            }

            override public void SetHeatStart()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 36;//Controller ON/OFF(0000,0001) 
                ushort numSetValue = 0;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateAscii(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, numSetValue);
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }

            }

            override public void SetHeatStop()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 36;//Controller ON/OFF(0000,0001) 
                ushort numSetValue = 1;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateAscii(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, numSetValue);
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }

            }

            /// <summary>
            /// 取得溫控器狀態
            /// </summary>
            /// <returns>0:Stop; 1:Run</returns>
            override public int GetOnOff()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 36;//Process Value
                ushort numInputs = 1;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateAscii(mSerialPort);
                    ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);

                    if (inputs[0] == 0)
                    {
                        m_bOnOff = true;
                        return 1;
                    }
                    else
                    {
                        m_bOnOff = false;
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return -1;
                }

            }

            /// <summary>
            /// 取得小數點位數
            /// </summary>
            /// <returns>小數點位數</returns>
            private int GetDigits()
            {
                byte SlaveAddress = (byte)(m_StationNumber / 4 + 1);
                ushort startAddress = 25;
                ushort numInputs = 1;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);
                    return inputs[0];
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return -1;
                }
            }

        }
        public class clsZ_TIOControllor : clsControllerBase
        {
            int m_iInZ_TIOsIndex = 0;
            static int m_iTIOControllorCount = 0;

            #region //=====================  必要函式設置 =====================
            public clsZ_TIOControllor(int StationNo, ref SerialPort refSerialPort)
            {
                mSerialPort = refSerialPort;
                m_StationNumber = StationNo;

                m_eunType = enuControllerType.Z_TIO;

                m_iTIOControllorCount++;
                m_iInZ_TIOsIndex = StationNo;// m_iTIOControllorCount - 1;//@1.0.0.41-20@
            }

            #endregion

            override public double GetTemp()
            {
                byte SlaveAddress = (byte)(m_StationNumber / 4 + 1);
                ushort startAddress = 0;//Process Value
                switch (m_iInZ_TIOsIndex % 4)
                {
                    case 0:
                        startAddress = 0;
                        break;
                    case 1:
                        startAddress = 1;
                        break;
                    case 2:
                        startAddress = 2;
                        break;
                    case 3:
                        startAddress = 3;
                        break;

                    default:
                        return -1;
                }

                ushort numInputs = 1;
                try
                {
                    int iDigits = GetDigits();
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);
                    double rValue = (double)(inputs[0]) / (iDigits * 10);
                    if (rValue > 800)
                    { rValue = -2; }
                    return rValue;
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return -1;
                }
            }

            override public void SetTemp(double dblTemp)
            {
                byte SlaveAddress = (byte)(m_StationNumber / 4 + 1);
                ushort startAddress = 142;//Setting Value 

                switch (m_iInZ_TIOsIndex % 4)
                {
                    case 0:
                        startAddress = 142;
                        break;
                    case 1:
                        startAddress = 143;
                        break;
                    case 2:
                        startAddress = 144;
                        break;
                    case 3:
                        startAddress = 145;
                        break;
                }
                try
                {
                    int iDigits = GetDigits();

                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, (ushort)(dblTemp * iDigits * 10));
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }

            }

            //新增溫控器offset string
            override public void SetTempShift(double dblShift)
            {
                //return;
                byte SlaveAddress = (byte)(m_StationNumber / 4 + 1);
                ushort startAddress = 26;//Setting Value
                switch (m_iInZ_TIOsIndex % 4)
                {
                    case 0:
                        startAddress = 210;
                        break;
                    case 1:
                        startAddress = 211;
                        break;
                    case 2:
                        startAddress = 212;
                        break;
                    case 3:
                        startAddress = 213;
                        break;
                }
                try
                {
                    int iDigits = GetDigits();
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, (ushort)(dblShift * iDigits * 10));
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            override public void SetHeatStart()
            {
                byte SlaveAddress = (byte)(m_StationNumber / 4 + 1);
                ushort startAddress = 109;//Controller Run/Stop(0001,0000) 
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, (ushort)1);
                    //GetOnOff();
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }

            }

            override public void SetHeatStop()
            {
                byte SlaveAddress = (byte)(m_StationNumber / 4 + 1);
                ushort startAddress = 109;//Controller Run/Stop(0001,0000) 
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, (ushort)0);
                    //GetOnOff();
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            /// <summary>
            /// 取得溫控器狀態
            /// </summary>
            /// <returns>0:Stop; 1:Run</returns>
            override public int GetOnOff()
            {
                byte SlaveAddress = (byte)(m_StationNumber / 4 + 1);
                ushort startAddress = 109;//Controller Run/Stop(0001,0000) 
                ushort numInputs = 1;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);
                    if (1 == inputs[0])
                        m_bOnOff = true;
                    else
                        m_bOnOff = false;
                    return inputs[0];
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return -1;
                }
            }

            /// <summary>
            /// 設定量測溫度的Type
            /// </summary>
            /// <param name="iType">預設0:熱電偶K type;其他請參考手冊</param>
            public void SetMeasureType(int iType)
            {
                bool bNeedReStart = false;
                if (GetOnOff() == 1)
                {
                    bNeedReStart = true;
                    SetHeatStop();
                }
                byte SlaveAddress = (byte)(m_StationNumber / 4 + 1);
                ushort startAddress = 374;//Setting Value 

                switch (m_iInZ_TIOsIndex)
                {
                    case 0:
                        startAddress = 374;
                        break;
                    case 1:
                        startAddress = 375;
                        break;
                    case 2:
                        startAddress = 376;
                        break;
                    case 3:
                        startAddress = 377;
                        break;
                }
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, (ushort)(iType));
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }

                if (bNeedReStart)
                {
                    SetHeatStart();
                }
            }


            /// <summary>
            /// 取得小數點位數
            /// </summary>
            /// <returns>小數點位數</returns>
            private int GetDigits()
            {
                byte SlaveAddress = (byte)(m_StationNumber / 4 + 1);
                ushort startAddress = 382;//Controller Run/Stop(0001,0000) 
                ushort numInputs = 1;
                switch (m_iInZ_TIOsIndex)
                {
                    case 0:
                        startAddress = 382;
                        break;
                    case 1:
                        startAddress = 383;
                        break;
                    case 2:
                        startAddress = 384;
                        break;
                    case 3:
                        startAddress = 385;
                        break;
                }
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);
                    return inputs[0];
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return -1;
                }
            }

            /// <summary>
            /// 設定小數點位數
            /// </summary>
            /// <param name="iDigits">小數點位數</param>
            private void SetDigits(int iDigits)
            {
                bool bNeedReStart = false;
                if (GetOnOff() == 1)
                {
                    bNeedReStart = true;
                    SetHeatStop();
                }

                byte SlaveAddress = (byte)(m_StationNumber / 4 + 1);
                ushort startAddress = 382;

                switch (m_iInZ_TIOsIndex)
                {
                    case 0:
                        startAddress = 382;
                        break;
                    case 1:
                        startAddress = 383;
                        break;
                    case 2:
                        startAddress = 384;
                        break;
                    case 3:
                        startAddress = 385;
                        break;
                }
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, (ushort)(iDigits));
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }

                if (bNeedReStart)
                    SetHeatStart();
            }

        }
        public class clsSDC15Controllor : clsControllerBase
        {
            private int m_iDigits_SD15 = -1;
            #region //=====================  必要函式設置 =====================
            public clsSDC15Controllor(int StationNo, ref SerialPort refSerialPort)
            {
                mSerialPort = refSerialPort;
                m_StationNumber = StationNo;

                m_eunType = enuControllerType.SDC15;
            }

            #endregion

            override public double GetTemp()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 9101;//Process Value
                ushort numInputs = 1;
                try
                {
                    int iDigits = GetDigits();
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);

                    if (iDigits != 0 && iDigits != -1)
                    {
                        return (double)inputs[0] / (iDigits * 10);
                    }
                    else
                    {
                        return (double)inputs[0] / 10;
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return -1;
                }
            }

            override public void SetTemp(double dblTemp)
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 9102;//Set Value(SV1)

                List<ushort> lstValue = new List<ushort>();
                ushort numSetValue = (ushort)(dblTemp);
                try
                {
                    int iDigits = GetDigits();
                    if (iDigits != 0 && iDigits != -1)
                    {
                        numSetValue = (ushort)(dblTemp * iDigits * 10);
                    }
                    else
                    {
                        numSetValue = (ushort)(dblTemp * 10);
                    }

                    lstValue.Add(numSetValue);
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    //MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, numSetValue);
                    MyModbusSerial.WriteMultipleRegisters(SlaveAddress, startAddress, lstValue.ToArray());
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            override public void SetTempShift(double dblTemp)
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 6008;
                ushort numSetValue = (ushort)(dblTemp * 10);

                List<ushort> lstValue = new List<ushort>();
                lstValue.Add(numSetValue);

                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    //MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, numSetValue);
                    MyModbusSerial.WriteMultipleRegisters(SlaveAddress, startAddress, lstValue.ToArray());
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            override public int GetOnOff()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 9002;//run: 0 ;stop:1
                ushort numInputs = 1;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);
                    if (inputs[0] == 0)
                    {
                        m_bOnOff = true;
                        return 1;
                    }
                    else
                    {
                        m_bOnOff = false;
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return -1;
                }
            }

            override public void SetHeatStart()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 9002;//run: 0 ;stop:1
                List<ushort> lstValue = new List<ushort>();
                lstValue.Add(0);

                //ushort numSetValue = 0;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    //MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, numSetValue);
                    MyModbusSerial.WriteMultipleRegisters(SlaveAddress, startAddress, lstValue.ToArray());
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            override public void SetHeatStop()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 9002;//run: 0 ;stop:1
                List<ushort> lstValue = new List<ushort>();
                lstValue.Add(1);

                //ushort numSetValue = 1;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    MyModbusSerial.WriteMultipleRegisters(SlaveAddress, startAddress, lstValue.ToArray());
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            /// <summary>
            /// 取得小數點位數
            /// </summary>
            /// <returns>小數點位數</returns>
            private int GetDigits()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 5204;
                ushort numInputs = 1;
                

                if (m_iDigits_SD15 == -1)
                {
                    try
                    {
                        ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                        ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);
                        m_iDigits_SD15 = inputs[0];
                        return inputs[0];
                    }
                    catch (Exception ex)
                    {
                        clsArtSystem.CatchLog(ex);
                        return -1;
                    }
                }

                return m_iDigits_SD15;
            }

        }
        public class clsDTKV4848_V12Controllor : clsControllerBase
        {
            #region //=====================  必要函式設置 =====================
            public clsDTKV4848_V12Controllor(int StationNo, ref SerialPort refSerialPort)
            {
                mSerialPort = refSerialPort;
                m_StationNumber = StationNo;


                m_eunType = enuControllerType.DTK4848_V12;
            }

            #endregion

            override public double GetTemp()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 0x1000;//Process Value
                ushort numInputs = 1;
                try
                {
                    if (IsSimulatorMode == false)
                    {
                        int iDigits = GetDigits();
                        ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateAscii(mSerialPort);
                        ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);

                        if (iDigits != 0 && iDigits != -1 && iDigits < 10)//@1.0.0.56-6@
                        {
                            return (double)inputs[0] / (iDigits * 10);
                        }
                        else
                        {
                            return (double)inputs[0] / 10;
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return -1;
                }
                return 0;
            }

            override public void SetTemp(double dblTemp)
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 0x1001;//Set Value(SV1)

                List<ushort> lstValue = new List<ushort>();
                ushort numSetValue = (ushort)(dblTemp);
                try
                {
                    int iDigits = GetDigits();
                    if (iDigits != 0 && iDigits != -1 && iDigits < 10)//@1.0.0.56-6@
                    {
                        numSetValue = (ushort)(dblTemp * iDigits * 10);
                    }
                    else
                    {
                        numSetValue = (ushort)(dblTemp * 10);
                    }

                    lstValue.Add(numSetValue);
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateAscii(mSerialPort);
                    //MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, numSetValue);
                    MyModbusSerial.WriteMultipleRegisters(SlaveAddress, startAddress, lstValue.ToArray());
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            override public void SetTempShift(double dblTemp)
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 0x1016;
                ushort numSetValue = (ushort)(dblTemp * 10);

                List<ushort> lstValue = new List<ushort>();
                lstValue.Add(numSetValue);

                try
                {
                    if (IsSimulatorMode == false && mSerialPort.IsOpen)
                    {
                        ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateAscii(mSerialPort);
                        //MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, numSetValue);
                        MyModbusSerial.WriteMultipleRegisters(SlaveAddress, startAddress, lstValue.ToArray());
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            override public int GetOnOff()//@1.0.0.56-6@
            {
                //該型號沒有讀取開關訊號 //@1.0.0.55-13-8@
                //if (m_bOnOff)
                //    return 1;
                //return 0;
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 0x1018;
                ushort numInputs = 1;
                try
                {
                    if (IsSimulatorMode == false)
                    {
                        ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateAscii(mSerialPort);
                        ushort[] inputs = MyModbusSerial.ReadHoldingRegisters(SlaveAddress, startAddress, numInputs);
                        if (inputs[0] == 1)
                        {
                            m_bOnOff = true;
                            return 1;
                        }
                        else
                        {
                            m_bOnOff = false;
                            return 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return -1;
                }
                return 0;
            }

            override public void SetHeatStart()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 0x1018;//run: 1 ;stop: 0
                List<ushort> lstValue = new List<ushort>();
                lstValue.Add(1);

                //ushort numSetValue = 0;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateAscii(mSerialPort);
                    //MyModbusSerial.WriteSingleRegister(SlaveAddress, startAddress, numSetValue);
                    MyModbusSerial.WriteMultipleRegisters(SlaveAddress, startAddress, lstValue.ToArray());
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            override public void SetHeatStop()
            {
                byte SlaveAddress = (byte)m_StationNumber;
                ushort startAddress = 0x1018;//run: 1 ;stop: 0
                List<ushort> lstValue = new List<ushort>();
                lstValue.Add(0);

                //ushort numSetValue = 1;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateAscii(mSerialPort);
                    MyModbusSerial.WriteMultipleRegisters(SlaveAddress, startAddress, lstValue.ToArray());
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            /// <summary>
            /// 取得小數點位數
            /// </summary>
            /// <returns>小數點位數</returns>
            private int GetDigits()
            {
                return 1;
            }

        }

        public class clsE5CC_Omron : clsControllerBase
        {
            #region //===================== Enum =====================
            public enum E5ccActionCode : byte
            {
                CommWrite = 0x00, // 通信寫入 (CMWT)
                RunStop = 0x01, // 運行/停止
                MultiSP = 0x02, // 多重設定點
                AT = 0x03, // AT 實行/取消
                WriteMode = 0x04, // 寫入模式（備份 / RAM）
                RamSave = 0x05, // RAM 數據保存
                SoftReset = 0x06, // 軟件復位
                ToInitialLevel1 = 0x07, // 轉至設定區域1
                ToProtectMenu = 0x08, // 轉至保護菜單
                AutoManual = 0x09, // 自動 / 手動
                ParamInitialize = 0x0B, // 參數初始化
                AlarmLatchRelease = 0x0C, // 報警闩鎖解除
                SpMode = 0x0D, // SP 模式（標準/斜坡）
                DirectReverse = 0x0E, // 正向/反向
                ProgramRun = 0x11, // 程序啟動（簡易程序）
            }

            public enum adressV2
            {
                Prmt0x2000 = 0x2000,//PV
                Prmt0x2001 = 0x2001,//状态 *1 *2
                Prmt0x2002 = 0x2002,//内部SP *1
                Prmt0x2003 = 0x2003,//加热器电流值1 监控
                Prmt0x2004 = 0x2004,//MV 监控( 加热) 
                Prmt0x2005 = 0x2005,//MV 监控( 冷却) 
                Prmt0x2103 = 0x2103,//SP
                Prmt0x2104 = 0x2104,//报警值1
                Prmt0x2105 = 0x2105,//报警上限1
                Prmt0x2106 = 0x2106,//报警下限1
                Prmt0x2107 = 0x2107,//报警值2
                Prmt0x2108 = 0x2108,//报警上限2
                Prmt0x2109 = 0x2109,//报警下限2
                Prmt0x2402 = 0x2402,//PV
                Prmt0x2403 = 0x2403,//内部SP　*1
                Prmt0x2404 = 0x2404,//多重SP 号码监控
                Prmt0x2406 = 0x2406,//状态*1 *2
                Prmt0x2407 = 0x2407,//状态*1 *3
                Prmt0x2408 = 0x2408,//状态2 *1 *2
                Prmt0x2409 = 0x2409,//状态2 *1 *3
                Prmt0x2410 = 0x2410,//小数点位置监控
                Prmt0x2500 = 0x2500,//操作/ 调整保護
                Prmt0x2501 = 0x2501,//初始设定/ 通信保護
                Prmt0x2502 = 0x2502,// 设定变更保护H'00000000(0) ：OFF( 允许主体显示部的设定变更) 
                Prmt0x2503 = 0x2503,// PF 键保护H'00000000(0) ：OFF
                Prmt0x2504 = 0x2504,// 转至保护菜单H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x2505 = 0x2505,// 转至保护菜单密码H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x2506 = 0x2506,// 参数屏蔽有效H'00000000(0) ：OFF
                Prmt0x2507 = 0x2507,// 仅限已更改的参数H'00000000(0) ：OFF
                Prmt0x2600 = 0x2600,// 手动MV ·标准型
                Prmt0x2601 = 0x2601,// SP SP 下限～ SP 上限操作
                Prmt0x2602 = 0x2602,// 远程SP 监控远程SP 下限-10%FS ～远程SP 上限+10%FS
                Prmt0x2604 = 0x2604,// 加热器电流值1 监控H'00000000 ～ H'00000226(0.0 ～ 55.0) 
                Prmt0x2605 = 0x2605,// MV 监控( 加热) 标准 ：H'FFFFFFCE ～ H'0000041A(-5.0 ～ 105.0) 
                Prmt0x2606 = 0x2606,// MV 监控( 冷却) H'00000000 ～ H'0000041A(0.0 ～ 105.0) 
                Prmt0x2607 = 0x2607,// 阀门开度监控H'FFFFFF9C ～ H'0000044C(-10.0 ～ 110.0) 
                Prmt0x2701 = 0x2701,// 比例带( 冷却) H'00000001 ～ H'0000270F(0.1 ～ 999.9) 调整
                Prmt0x2702 = 0x2702,// 积分时间( 冷却) H'00000000 ～ H'0000270F
                Prmt0x2703 = 0x2703,// 微分时间( 冷却) H'00000000 ～ H'0000270F
                Prmt0x2704 = 0x2704,// 死区H'FFFFF831 ～ H'0000270F
                Prmt0x2705 = 0x2705,// 手动复位值H'00000000 ～ H'000003E8 (0.0 ～ 100.0) 
                Prmt0x2706 = 0x2706,// 滞后( 加热) H'00000001 ～ H'0000270F
                Prmt0x2707 = 0x2707,// 滞后( 冷却) H'00000001 ～ H'0000270F
                Prmt0x2708 = 0x2708,// 控制周期( 加热) H'FFFFFFFE(-2) ：0.1 秒
                Prmt0x2709 = 0x2709,// 控制周期( 冷却) H'FFFFFFFE(-2) ：0.1 秒
                Prmt0x270A = 0x270A,// 位置比例死区H'00000001 ～ H'00000064(0.1 ～ 10.0) 调整
                Prmt0x270B = 0x270B,// 开闭滞后H'00000001 ～ H'000000C8(0.1 ～ 20.0) 
                Prmt0x270C = 0x270C,// SP 斜坡时间单位H'00000000(0) ：EU/ 秒
                Prmt0x270D = 0x270D,// SP 斜坡设定值H'00000000(0) ：OFF
                Prmt0x270E = 0x270E,// SP 斜坡设定值( 下降值) H'FFFFFFFF(-1) ：SAME( 与SP 斜坡设定值一致) 
                Prmt0x270F = 0x270F,// 停止时的MV ·标准型
                Prmt0x2711 = 0x2711,// PV 出错时的MV ·位置比例型
                Prmt0x2713 = 0x2713,// MV 变化率极限H'00000000 ～ H'000003E8 (0.0 ～ 100.0) 
                Prmt0x2718 = 0x2718,// PV 输入斜坡系数H'00000001 ～ H'0000270F(0.001 ～ 9.999) 
                Prmt0x271A = 0x271A,// 加热器电流值1 监控H'00000000 ～ H'00000226(0.0 ～ 55.0) 操作
                Prmt0x271B = 0x271B,// 加热器断线检测1 H'00000000 ～ H'000001F4 (0.0 ～ 50.0) 调整
                Prmt0x271C = 0x271C,// 泄漏电流值1 监控H'00000000 ～ H'00000226(0.0 ～ 55.0) 操作
                Prmt0x271D = 0x271D,// HS 报警1 H'00000000 ～ H'000001F4 (0.0 ～ 50.0) 调整
                Prmt0x2723 = 0x2723,// PV 输入偏移量H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x2724 = 0x2724,// 加热器电流值2 监控H'00000000 ～ H'00000226(0.0 ～ 55.0) 操作
                Prmt0x2725 = 0x2725,// 加热器断线检测2 H'00000000 ～ H'000001F4 (0.0 ～ 50.0) 调整
                Prmt0x2726 = 0x2726,// 泄漏电流值2 监控H'00000000 ～ H'00000226(0.0 ～ 55.0) 操作
                Prmt0x2727 = 0x2727,// HS 报警2 H'00000000 ～ H'000001F4 (0.0 ～ 50.0) 调整
                Prmt0x2728 = 0x2728,// 剩余保温时间监控H'00000000 ～ H'0000270F (0 ～ 9999) 操作
                Prmt0x2729 = 0x2729,// 保温时间H'00000001 ～ H'0000270F(1 ～ 9999) 调整
                Prmt0x272A = 0x272A,// 等待区间H'00000000(0) ：OFF
                Prmt0x272B = 0x272B,// 远程SP 输入偏移量H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x272C = 0x272C,// 远程SP 输入斜坡系数H'00000001 ～ H'0000270F(0.001 ～ 9.999) 
                Prmt0x2800 = 0x2800,// 输入用数字滤波器H'00000000 ～ H'0000270F (0.0 ～ 999.9) 高级功能设定
                Prmt0x2804 = 0x2804,// 移动平均次数H'00000000(0) ：OFF
                Prmt0x2808 = 0x2808,// 平方根的提取H'00000000 ～ H'000003E8 (0.0 ～ 100.0) 调整
                Prmt0x2900 = 0x2900,// SP0 SP 下限～ SP 上限
                Prmt0x2902 = 0x2902,// 报警值1 H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 操作
                Prmt0x2903 = 0x2903,// 报警上限1 H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x2904 = 0x2904,// 报警下限1 H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x2905 = 0x2905,// 报警值2 H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x2906 = 0x2906,// 报警上限2 H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x2907 = 0x2907,// 报警下限2 H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x2908 = 0x2908,// 报警值3 H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x2909 = 0x2909,// 报警上限3 H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x290A = 0x290A,// 报警下限3 H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x290B = 0x290B,// 报警值4 H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x290C = 0x290C,// 报警上限4 H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x290D = 0x290D,// 报警下限4 H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                Prmt0x290E = 0x290E,// SP1 SP 下限～ SP 上限调整
                Prmt0x291C = 0x291C,// SP2 SP 下限～ SP 上限
                Prmt0x292A = 0x292A,// SP3 SP 下限～ SP 上限
                Prmt0x2938 = 0x2938,// SP4 SP 下限～ SP 上限
                Prmt0x2946 = 0x2946,// SP5 SP 下限～ SP 上限
                Prmt0x2954 = 0x2954,// SP6 SP 下限～ SP 上限
                Prmt0x2962 = 0x2962,// SP7 SP 下限～ SP 上限
                Prmt0x2A00 = 0x2A00,// 比例带H'00000001 ～ H'0000270F(0.1 ～ 999.9) 
                Prmt0x2A01 = 0x2A01,// 积分时间标准、加热冷却、位置比例( 闭合) ：
                Prmt0x2A02 = 0x2A02,// 微分时间H'00000000 ～ H'0000270F
                Prmt0x2A05 = 0x2A05,// MV 上限标准控制、或位置比例( 闭合) ：
                Prmt0x2A06 = 0x2A06,// MV 下限 标准控制、或位置比例( 闭合) ：
                Prmt0x2C00 = 0x2C00,// 输入类型H'00000000(0) ：Pt (-200 ～ 850 ℃ / -300 ～ 1500 ℉ ) 
                Prmt0x2C01 = 0x2C01,// 温度单位H'00000000(0) ：℃
                Prmt0x2C09 = 0x2C09,// 比例缩放下限H'FFFFF831 ～比例缩放上限－ 1
                Prmt0x2C0B = 0x2C0B,// 比例缩放上限比例缩放下限＋ 1 ～ H'0000270F
                Prmt0x2C0C = 0x2C0C,// 小数点位置( 只在设定模拟输入时) 
                Prmt0x2C0D = 0x2C0D,// 远程SP 上限输入范围下限～输入范围上限：温度输入
                Prmt0x2C0E = 0x2C0E,// 远程SP 下限输入范围下限～输入范围上限：温度输入
                Prmt0x2C0F = 0x2C0F,// PV 小数点显示H'00000000(0) ：OFF
                Prmt0x2D03 = 0x2D03,// 控制输出1 信号H'00000000(0) ：4-20mA
                Prmt0x2D04 = 0x2D04,// 控制输出2 信号H'00000000(0) ：4-20mA
                Prmt0x2D0F = 0x2D0F,// SP 上限去除了小数点的数值范围如下所示。
                Prmt0x2D10 = 0x2D10,// SP 下限去除了小数点的数值范围如下所示。
                Prmt0x2D11 = 0x2D11,// 标准或加热/ 冷却H'00000000(0) ：标准
                Prmt0x2D12 = 0x2D12,// 正向/ 反向运行H'00000000(0) ：反向运行
                Prmt0x2D13 = 0x2D13,// 闭合 / 浮动H'00000000(0：浮动) 
                Prmt0x2D14 = 0x2D14,// PID、ON/OFF H'00000000(0) ：ON/OFF
                Prmt0x2D15 = 0x2D15,// ST H'00000000(0) ：OFF
                Prmt0x2D16 = 0x2D16,// 程序模式H'00000000(0) ：OFF
                Prmt0x2D18 = 0x2D18,// 远程SP 输入类型H'00000000(0) ：4-20mA
                Prmt0x2D19 = 0x2D19,// 控制输出最小ON/OFF 幅H'00000000 ～ H'000001F4 (0.0 ～ 50.0) 
                Prmt0x2E00 = 0x2E00,// 传送输出类型H'00000000(0) ：OFF
                Prmt0x2E01 = 0x2E01,// 传送输出信号类型H'00000000(0) ：4-20mA
                Prmt0x2E06 = 0x2E06,// 控制输出1 分配控制输出1 为继电器输出、电压输出(SSR 驱动用) 时：
                Prmt0x2E07 = 0x2E07,// 控制输出2 分配控制输出2 为继电器输出、电压输出(SSR 驱动用) 时：
                Prmt0x2E0A = 0x2E0A,// 事件输入分配1 H'00000000(0) ：无
                Prmt0x2E0B = 0x2E0B,// 事件输入分配2 H'00000000 ～ H'0000000D(0 ～ 13) 
                Prmt0x2E0C = 0x2E0C,// 事件输入分配3 H'00000000 ～ H'0000000D(0 ～ 13) 
                Prmt0x2E0D = 0x2E0D,// 事件输入分配4 H'00000000 ～ H'0000000D(0 ～ 13) 
                Prmt0x2E0E = 0x2E0E,// 事件输入分配5 H'00000000 ～ H'0000000D(0 ～ 13) 
                Prmt0x2E0F = 0x2E0F,// 事件输入分配6 H'00000000 ～ H'0000000D(0 ～ 13) 
                Prmt0x2E10 = 0x2E10,// 辅助输出1 分配H'00000000(0) ：无分配
                Prmt0x2E11 = 0x2E11,// 辅助输出2 分配H'00000000 ～ H'00000016(0 ～ 22) ※ 和辅助输出1 分配一致
                Prmt0x2E12 = 0x2E12,// 辅助输出3 分配H'00000000 ～ H'00000016(0 ～ 22) ※ 和辅助输出1 分配一致
                Prmt0x2E13 = 0x2E13,// 辅助输出4 分配H'00000000 ～ H'00000016(0 ～ 22) ※ 和辅助输出1 分配一致
                Prmt0x2E14 = 0x2E14,// 传送输出上限H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) *2 初始设定
                Prmt0x2E15 = 0x2E15,// 传送输出下限H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) *2
                Prmt0x2E24 = 0x2E24,// 平方根的提取启用H'00000000(0) ：OFF
                Prmt0x2E30 = 0x2E30,// 行程时间H'00000001 ～ H'000003E7(1 ～ 999) 
                Prmt0x2F00 = 0x2F00,// 报警1 类型H'00000000(0) ：无报警功能
                Prmt0x2F01 = 0x2F01,// 报警1 闩锁H'00000000(0) ：OFF
                Prmt0x2F02 = 0x2F02,// 报警1 滞后H'00000001 ～ H'0000270F
                Prmt0x2F03 = 0x2F03,// 报警2 类型H'00000000 ～ H'00000013(0 ～ 19) 
                Prmt0x2F04 = 0x2F04,// 报警2 闩锁H'00000000(0) ：OFF
                Prmt0x2F05 = 0x2F05,// 报警2 滞后H'00000001 ～ H'0000270F
                Prmt0x2F06 = 0x2F06,// 报警3 类型H'00000000 ～ H'00000013(0 ～ 19) 
                Prmt0x2F07 = 0x2F07,// 报警3 闩锁H'00000000(0) ：OFF
                Prmt0x2F08 = 0x2F08,// 报警3 滞后H'00000001 ～ H'0000270F
                Prmt0x2F09 = 0x2F09,// 报警4 类型H'00000000 ～ H'00000013(0 ～ 19) 
                Prmt0x2F0A = 0x2F0A,// 报警4 闩锁H'00000000(0) ：OFF
                Prmt0x2F0B = 0x2F0B,// 报警4 滞后H'00000001 ～ H'0000270F
                Prmt0x2F0C = 0x2F0C,// 待机序列复位H'00000000(0) ：条件A
                Prmt0x2F0D = 0x2F0D,// 报警时辅助输出1 开启H'00000000(0) ：关闭
                Prmt0x2F0E = 0x2F0E,// 报警时辅助输出2 开启H'00000000(0) ：关闭
                Prmt0x2F0F = 0x2F0F,// 报警时辅助输出3 开启H'00000000(0) ：关闭
                Prmt0x2F10 = 0x2F10,// 报警时辅助输出4 开启H'00000000(0) ：关闭
                Prmt0x2F11 = 0x2F11,// 报警1 ON 延时H'00000000 ～ H'000003E7(0 ～ 999) 高级功能设定
                Prmt0x2F12 = 0x2F12,// 报警2 ON 延时H'00000000 ～ H'000003E7(0 ～ 999) 
                Prmt0x2F13 = 0x2F13,// 报警3 ON 延时H'00000000 ～ H'000003E7(0 ～ 999) 
                Prmt0x2F14 = 0x2F14,// 报警4ON 延时H'00000000 ～ H'000003E7(0 ～ 999) 
                Prmt0x2F15 = 0x2F15,// 报警1 OFF 延时 H'00000000 ～ H'000003E7(0 ～ 999) 
                Prmt0x2F16 = 0x2F16,// 报警2 OFF 延时 H'00000000 ～ H'000003E7(0 ～ 999) 
                Prmt0x2F17 = 0x2F17,// 报警3 OFF 延时 H'00000000 ～ H'000003E7(0 ～ 999) 
                Prmt0x2F18 = 0x2F18,// 报警4OFF 延时H'00000000 ～ H'000003E7(0 ～ 999) 
                Prmt0x3000 = 0x3000,// “PV/SP(1) ”显示画面选择H'000000000(0) ：无显示
                Prmt0x3001 = 0x3001,// MV 显示选择H'00000000(0) ：MV( 加热) 
                Prmt0x3003 = 0x3003,// 显示自动返回时间H'00000000(0) ：OFF
                Prmt0x3004 = 0x3004,// 显示更新周期H'00000000(0) ：OFF
                Prmt0x3008 = 0x3008,// “PV/SP(2) ”显示画面选择H'00000000 ～ H'00000008(0 ～ 8) 
                Prmt0x300A = 0x300A,// 显示亮度设定H'00000001 ～ H'00000003(1 ～ 3) 
                Prmt0x300B = 0x300B,// MV 显示H'00000000(0) ：OFF
                Prmt0x300C = 0x300C,// 转至保护菜单时间H'00000001 ～ H'0000001E (1 ～ 30) 
                Prmt0x300F = 0x300F,// 自动/ 手动切换显示追加H'00000000(0) ：OFF
                Prmt0x3011 = 0x3011,// PV 状态显示功能H'00000000(0) ：OFF
                Prmt0x3012 = 0x3012,// SV 状态显示功能H'00000000 ～ H'00000008(0 ～ 8) 
                Prmt0x3100 = 0x3100,// 协议选择*1 H'00000000(0) ：CompoWay/F
                Prmt0x3101 = 0x3101,// 通信单位编号*1 H'00000000 ～ H'00000063 (0 ～ 99) 
                Prmt0x3102 = 0x3102,// 通信波特率*1 H'00000003(3) ：9.6
                Prmt0x3103 = 0x3103,// 通信数据位*1 H'00000007(7) ：7
                Prmt0x3104 = 0x3104,// 通信终止位*1 H'00000001(1) ：1
                Prmt0x3105 = 0x3105,// 通信奇偶校验*1 H'00000000(0) ：无
                Prmt0x3106 = 0x3106,// 发送数据等待时间*1 H'00000000 ～ H'00000063 (0 ～ 99) 
                Prmt0x3200 = 0x3200,// PF 设定H'00000000(0) ：无效
                Prmt0x3202 = 0x3202,// 监控／设定项目1 H'000000000(0) ：无效
                Prmt0x3203 = 0x3203,// 监控／设定项目2 H'00000000 ～ H'00000017(0 ～ 23) 
                Prmt0x3204 = 0x3204,// 监控／设定项目3 H'00000000 ～ H'00000017(0 ～ 23) 
                Prmt0x3205 = 0x3205,// 监控／设定项目4 H'00000000 ～ H'00000017(0 ～ 23) 
                Prmt0x3206 = 0x3206,// 监控／设定项目5 H'00000000 ～ H'00000017(0 ～ 23) 
                Prmt0x3301 = 0x3301,// SP 交代H'00000000(0) ：OFF
                Prmt0x3304 = 0x3304,// PV 死区H'00000000 ～ H'0000270F(0 ～ 9999) 
                Prmt0x3305 = 0x3305,// 冷接点补偿方法H'00000000(0) ：OFF
                Prmt0x3309 = 0x3309,// 积分/ 微分时间单位H'00000000(0) ：1s
                Prmt0x330A = 0x330A,// α H'00000000 ～ H'00000064 (0.00 ～ 1.00) 
                Prmt0x330C = 0x330C,// 手动输出方法H'00000000(0) ：HOLD
                Prmt0x330D = 0x330D,// 手动MV 初始值标准控制、或位置比例(闭合) ：
                Prmt0x330F = 0x330F,// AT 算出增益H'00000001 ～ H'00000064(0.1 ～ 10.0) 
                Prmt0x3310 = 0x3310,// AT 滞后H'00000001 ～ H'0000270F(0.1 ～ 999.9：温度输入) 
                Prmt0x3311 = 0x3311,// 有限周期MV 的变动范围H'00000032 ～ H'000001F4(5.0 ～ 50.0) 
                Prmt0x3314 = 0x3314,// 加热器断线闩锁H'00000000(0) ：OFF
                Prmt0x3315 = 0x3315,// 加热器断线滞后H'00000001 ～ H'000001F4 (0.1 ～ 50.0) 
                Prmt0x3316 = 0x3316,// HS 报警闩锁H'00000000(0) ：OFF
                Prmt0x3317 = 0x3317,// HS 报警滞后H'00000001 ～ H'000001F4 (0.1 ～ 50.0) 
                Prmt0x331B = 0x331B,// 多重设定点使用点数H'00000001(1) ：OFF
                Prmt0x331C = 0x331C,// HB ON/OFF H'00000000(0) ：OFF
                Prmt0x331E = 0x331E,// 综合报警分配H'00000000 ～ H'000000FF(0 ～ 255) 
                Prmt0x3320 = 0x3320,// 停止时/PV 出错时的MV 追
                Prmt0x3321 = 0x3321,// ST 稳定带H'00000001 ～ H'0000270F(0.1 ～ 999.9) 
                Prmt0x3322 = 0x3322,// RT H'00000000(0) ：OFF
                Prmt0x3323 = 0x3323,// HS 报警使用H'00000000(0) ：OFF
                Prmt0x3324 = 0x3324,// LBA 检测时间H'00000000 ～ H'0000270F (0 ～ 9999) 
                Prmt0x3325 = 0x3325,// LBA 检测阈值H'00000001 ～ H'0000270F
                Prmt0x3326 = 0x3326,// LBA 检测带H'00000000 ～ H'0000270F
                Prmt0x3327 = 0x3327,// 保温时间单位H'00000000(0) ：分钟
                Prmt0x3328 = 0x3328,// 报警SP 选择H'00000000(0) ：斜坡SP
                Prmt0x3329 = 0x3329,// 远程SP 有效H'00000000(0) ：OFF
                Prmt0x332B = 0x332B,// 手动有效极限H'00000000(0) ：OFF
                Prmt0x332C = 0x332C,// 位置比例MV 直接设定H'00000000(0) ：OFF
                Prmt0x332D = 0x332D,// PV 变化率演算周期H'00000001 ～ H'000003E7(1 ～ 999) 
                Prmt0x332E = 0x332E,// 加热冷却调节方法H'00000000(0) ：与加热通用
                Prmt0x3335 = 0x3335,// LCT 冷却输出最小ON 时间
            }
            #endregion

            #region //===================== 必要函式設置 =====================
            public clsE5CC_Omron(int StationNo, ref SerialPort refSerialPort)
            {
                mSerialPort = refSerialPort;
                m_StationNumber = StationNo;
                m_eunType = enuControllerType.E5CC_Omron;
            }

            #endregion

            override public double GetTemp()
            {
                try
                {
                    short[] Value = new short[10];
                    if (GetData((byte)m_StationNumber, (ushort)adressV2.Prmt0x2000, 1, ref Value) == true)
                    {
                        return ((double)Value[0]) / 10;
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
                return 0;
            }

            override public void SetTemp(double dblTemp)
            {
                try
                {
                    //2103
                    short[] Value = new short[] { (short)(dblTemp * 10) };
                    SetData((byte)m_StationNumber, (ushort)adressV2.Prmt0x2103, 1, Value);
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            override public void SetTempShift(double dblTemp)//我不太確定
            {
                try
                {
                    //Prmt0x2723 = 0x2723,// PV 输入偏移量H'FFFFF831 ～ H'0000270F(-1999 ～ 9999) 
                    short[] Value = new short[] { (short)(dblTemp * 10) };
                    SetData((byte)m_StationNumber, (ushort)adressV2.Prmt0x2723, 1, Value);
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            override public int GetOnOff()//@1.0.0.56-6@
            {
                bool isRun = false;
                try
                {
                    //Prmt0x2407 = 0x2407,//状态*1 *3
                    // 讀「狀態（高位側）」寄存器：手冊位址 0x2407
                    // 注意：NModbus 的暫存器位址是 0-based，如果你的函式庫是 0-based，傳入 0x2407 即可。
                    // （大多數 .NET 實作皆為 0-based；若遇到 1-based 函式庫，這裡要改成 0x2408）
                    const ushort STATUS_HIGH_ADDR = 0x2407;
                    short[] regs = new short[1];
                    if (GetData((byte)m_StationNumber, (ushort)adressV2.Prmt0x2407, 1, ref regs) == true)
                    {
                        short statusHigh = regs[0];

                        // bit24 在高位側中的偏移 = 24 - 16 = bit8
                        bool bit24 = ((statusHigh >> 8) & 0x1) == 1;

                        // 表格定義：bit24 = 0 → 運行；= 1 → 停止
                        isRun = !bit24;
                    }
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
                return isRun ? 1 : 0;

            }

            override public void SetHeatStart()
            {
                try
                {
                    //RunStop = 0x01, // 運行/停止
                    SendActionCommand((byte)m_StationNumber, 0x01, 0x00); // 運行：01 00

                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }

            override public void SetHeatStop()
            {
                try
                {
                    //RunStop = 0x01, // 運行/停止
                    SendActionCommand((byte)m_StationNumber, 0x01, 0x01); // 停止：01 01
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
            }



            public bool SendActionCommand(byte slaveId, byte commandCode, byte info)
            {
                try
                {
                    // 地址欄固定 0000 → 0-based 傳 0
                    ushort actionAddress0Based = 0;

                    // 高位元組＝指令代碼；低位元組＝相關資訊
                    ushort value = (ushort)((commandCode << 8) | info);

                    var master = ModbusSerialMaster.CreateRtu(mSerialPort);
                    master.WriteSingleRegister(slaveId, actionAddress0Based, value); // FC06
                    return true;
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                    return false;
                }
            }



            public bool GetData(byte pComID, ushort addr, ushort count, ref short[] data)
            {
                //   Result = mModbus.SendFc3(Device_ID, addr, count, ref data) ;
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    ushort[] data2 = MyModbusSerial.ReadHoldingRegisters(pComID, addr, count);
                    for (int i = 0; i < data2.Length; i++)
                    {
                        data[i] = (short)data2[i];
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);

                }
                return false;
            }

            /// <summary> 設定控制器資料 </summary>
            /// <param name="addr">參數起始位址</param>
            /// <param name="count">參數量</param>
            /// <param name="data">參數陣列</param>
            /// <returns>True: 設定成功</returns>
            public bool SetData(byte pComID, ushort addr, ushort count, short[] data)
            {
                //Result = mModbus.SendFc16(Device_ID, addr, count, data) ;
                ushort[] data2 = new ushort[count];
                for (int i = 0; i < count; i++)
                {
                    data2[i] = (ushort)data[i];
                }
                try
                {
                    ModbusSerialMaster MyModbusSerial = ModbusSerialMaster.CreateRtu(mSerialPort);
                    MyModbusSerial.WriteMultipleRegisters(pComID, addr, data2);
                    return true;
                }
                catch (Exception ex)
                {
                    clsArtSystem.CatchLog(ex);
                }
                return false;
            }
        }
        

        #endregion

        #region //===================== Public Function (溫控指令) =====================
        public void SetOnOff(enuSwitch p_eunSwitch)
        {
            if (this.eDo_Enable != null)
            {
                clsDioCtrl.SetDo((clsEnum.enuDo)this.eDo_Enable, p_eunSwitch == enuSwitch.On ? true : false);
            }
            if (this.m_CtrlHeater.m_eunType == enuControllerType.Z_TIO)
            {
                p_eunSwitch = enuSwitch.On;
            }
            if (clsArtSystem.bIsSoftwareSimulate == true)
            {
                m_CtrlHeater.m_bOnOff = p_eunSwitch == enuSwitch.On ? true : false;
            }
            clsCmdStruct tTemp = new clsCmdStruct(this, enuCmdType.Type_SetOnOff, (double)p_eunSwitch);
            m_TempCmdList.Add(tTemp);
        }
        public void SetTemp(double p_dTemp)
        {
            if (clsArtSystem.bIsSoftwareSimulate == true)
            {
                this.m_CtrlHeater.m_dSettingTemp = p_dTemp;
            }
            clsCmdStruct tTemp = new clsCmdStruct(this, enuCmdType.Type_SetTemp, p_dTemp);
            m_TempCmdList.Add(tTemp);
        }
        public void SetTempShift(double p_dTemp)
        {
            clsCmdStruct tTemp = new clsCmdStruct(this, enuCmdType.Type_SetTempShift, p_dTemp);
            m_TempCmdList.Add(tTemp);
        }

        /// <summary>取得溫控器啟動或關閉</summary>
        /// <param name="p_iStationNum">站號</param>
        /// <returns>回傳溫控器啟動或關閉</returns>
        public bool GetOnOff()
        {
            bool rValue = false;
            if (m_CtrlHeater != null)
            {
                rValue = m_CtrlHeater.m_bOnOff;
                if (this.m_CtrlHeater.m_eunType == enuControllerType.Z_TIO)
                {
                    if (this.eDo_Enable != null)
                    {
                        rValue &= clsDioCtrl.GetDo((clsEnum.enuDo)this.eDo_Enable);
                    }
                }
            }
            return rValue;
        }


        /// <summary> 取得現在溫度值</summary>
        /// <param name="p_iStationNum">站號</param>
        /// <returns>回傳溫度值</returns>
        public double GetCurrentTemp()
        {
            if (m_CtrlHeater != null)
            {
                return m_CtrlHeater.m_dCurrentTemp;
            }
            return -1;
        }

        /// <summary> 取得溫度設定值</summary>
        /// <param name="p_iStationNum">站號</param>
        /// <returns>回傳設定值</returns>
        public double GetSettingTemp()
        {
            if (m_CtrlHeater != null)
            {
                return m_CtrlHeater.m_dSettingTemp;
            }
            return -1;
        }

        
        #endregion
    }
}

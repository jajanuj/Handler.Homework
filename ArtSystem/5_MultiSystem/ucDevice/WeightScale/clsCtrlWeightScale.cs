using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using ArtCommonLib;

namespace ArtSystem.MultiSystem
{
    public class clsCtrlWeightScale
    {
        #region//====== 參數 =====

        private enuModuleType eModuleType = enuModuleType.MettlerToledoWX;
        public enuModuleType eModule
        {
            get
            {
                return eModuleType;
            }
        }
        public string g_sName
        {
            get;
            private set;
        }
        public SerialPort mSerialPort = new SerialPort();
        private ANDWeightScale m_ANDWeightScale;//M012
        public bool bIsSimulatorMode = false;
        public bool mSerialPort_IsConnect = false;
        private double WeightValue = -999;
        private double WeightRemainCapacity = 0;//@1.0.0.44-8@
        private bool InWaitDataRequestProcess = false;
        private bool InWaitStableResult = false;

        #endregion

        #region//===== Enum 列舉 =====
        public enum enuModuleType
        {
            SartoriusCP,
            MettlerToledoWX = 1,
            ANDWeightScale,
        }
        #endregion

        #region//===== 必要函式 =====

        public clsCtrlWeightScale(enuModuleType mMoudle, string p_sName)
        {
            g_sName = p_sName;
            eModuleType = mMoudle;
            mSerialPort.DataReceived += new SerialDataReceivedEventHandler(mSerialPort_DataReceived);
        }

        #endregion

        #region//===== Public Function ======

        public void Com_SetPmt(string sPortName, int iBaudRate, Parity eParity, int iDataBits, StopBits eStopBits, bool bDtrEnable, int iTimeout = 500, Handshake eHandshake = Handshake.None)
        {
            try
            {
                mSerialPort.PortName = sPortName;
                mSerialPort.BaudRate = iBaudRate;
                mSerialPort.Parity = eParity;
                mSerialPort.DataBits = iDataBits;
                mSerialPort.StopBits = eStopBits;
                mSerialPort.ReadTimeout = iTimeout;
                mSerialPort.WriteTimeout = iTimeout;
                mSerialPort.Handshake = eHandshake;
                mSerialPort.DtrEnable = bDtrEnable;
            }
            catch
            {
            }
        }
        public void Com_Open()
        {
            try
            {
                if (bIsSimulatorMode == true)
                {return;}
                if (mSerialPort.IsOpen == true)
                { mSerialPort.Close(); }
                mSerialPort.Open();
                mSerialPort_IsConnect = true;
            }
            catch
            {

            }
        }
        public void Com_Close()
        {
            try
            {
                if (bIsSimulatorMode == true)
                { return; }
                mSerialPort.Close();
                mSerialPort_IsConnect = false;
            }
            catch
            {

            }
        }
        public bool Com_IsOpen()
        {
            bool bIsOpen = mSerialPort.IsOpen;
            if (bIsOpen == false)
            {
                mSerialPort_IsConnect = false;
            }
            return bIsOpen;
        }
        public bool Com_IsConnected()
        {
            return mSerialPort_IsConnect;
        }

        public double GetWeight_Immediately()
        {
            if (bIsSimulatorMode == true)
            { WeightValue = 0.1; }
            switch (eModuleType)
            {
                case enuModuleType.MettlerToledoWX:
                    return MettlerToledoWX_GetWeight();

                case enuModuleType.SartoriusCP:
                    //return SartoriusCP_GetWeight();
                    break;

                case enuModuleType.ANDWeightScale:
                    return ANDWeightScale_GetWeight();
            }
            return -900;
        }
        public void RunGetWeight()
        {
            if (bIsSimulatorMode == true)
            { WeightValue = 0.1; }
            switch (eModuleType)
            {
                case enuModuleType.MettlerToledoWX:
                    MettlerToledoWX_GetValueImmediatelyCmd();
                    break;

                case enuModuleType.SartoriusCP:
                    GetValueImmediatelyCmd();
                    break;

                case enuModuleType.ANDWeightScale:
                    ANDWeightScale_GetValueImmediatelyCmd();
                    break;
            }
        }
        public void RunGetWeight_Stable()
        {
            if (bIsSimulatorMode == true)
            { WeightValue = 0.1; }
            InWaitStableResult = true;
            switch (eModuleType)
            {
                case enuModuleType.MettlerToledoWX:
                    MettlerToledoWX_GetStableValueCmd();
                    break;

                case enuModuleType.SartoriusCP:
                    GetStableValueCmd();
                    break;

                case enuModuleType.ANDWeightScale:
                    ANDWeightScale_GetStableValueCmd();
                    break;
            }
        }
        public void RunResetValue()
        {
            if (bIsSimulatorMode == true)
            { WeightValue = 0; }
            switch (eModuleType)
            {
                case enuModuleType.MettlerToledoWX:
                    MettlerToledoWX_ResetValueCmd();
                    break;

                case enuModuleType.SartoriusCP:
                    ResetValueCmd();
                    break;

                case enuModuleType.ANDWeightScale:
                    ANDWeightScale_ResetValueCmd();
                    break;
            }
        }
        public void RunCancelCmd()
        {
            switch (eModuleType)
            {
                case enuModuleType.MettlerToledoWX:
                    MettlerToledoWX_CancelCommand();
                    break;

                case enuModuleType.SartoriusCP:
                    CancelCommand();
                    break;
                case enuModuleType.ANDWeightScale:
                    ANDWeightScale_CancelCommand();
                    break;
            }
        }
        public bool IsBusy()
        {
            return InWaitDataRequestProcess || InWaitStableResult;
        }
        public double GetWeightResult()
        {
            return WeightValue;
        }
        public void SetCorrectionCmd()//@1.0.0.43-24@
        {
            switch (eModuleType)
            {
                case enuModuleType.MettlerToledoWX:
                    MettlerToledoWX_SendCorrectionCmd();
                    break;

                case enuModuleType.SartoriusCP:
                    break;
                case enuModuleType.ANDWeightScale:
                    break;
            }
        }
        public void RunGetRemainCapacity()//@1.0.0.44-8@
        {
            switch (eModuleType)
            {
                case enuModuleType.MettlerToledoWX:
                    MettlerToledoWX_SendRemainCapacity();
                    break;

                case enuModuleType.SartoriusCP:
                    //return SartoriusCP_GetWeight();
                    break;

                case enuModuleType.ANDWeightScale:
                    //return ANDWeightScale_GetWeight();
                    break;
            }
        }
        public double GetRemainCapacityResult()//@1.0.0.44-8@
        {
            return WeightRemainCapacity;
        }
        public double GetRemainCapacity_Immediately()//@1.0.0.44-8@
        {
            switch (eModuleType)
            {
                case enuModuleType.MettlerToledoWX:
                    return MettlerToledoWX_GetRemainCapacity();

                case enuModuleType.SartoriusCP:
                    //return SartoriusCP_GetWeight();
                    break;

                case enuModuleType.ANDWeightScale:
                    //return ANDWeightScale_GetWeight();
                    break;
            }
            return 0;
        }

        #endregion

        #region//===== Private Function  ======
        
        private void mSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                switch (eModuleType)
                {
                    case enuModuleType.MettlerToledoWX:
                        MettlerToledoWX_DataRecived();
                        break;
                    case  enuModuleType.SartoriusCP:
                        SartoriusCP_DataRecived();
                        break;
                    case enuModuleType.ANDWeightScale:
                        ANDWeightScale_DataRecived();
                        break;
                }
            }
            catch
            {
                mSerialPort_IsConnect = false;
            }
        }

        #endregion
        
        #region//===== Switch Module =====

        #region//MettlerToledoWX

        private double MettlerToledoWX_GetWeight()
        {
            MettlerToledoWX_GetValueImmediatelyCmd();
            long StartTime = DateTime.Now.Ticks;
            while (InWaitDataRequestProcess)
            {
                if ((DateTime.Now.Ticks - StartTime) > 300 * 10000)
                {
                    MettlerToledoWX_CancelCommand();
                    mSerialPort_IsConnect = false;
                    return -9999.1;
                }
            }
            return WeightValue;
        }
        private void MettlerToledoWX_DataRecived()
        {//int intVlaue = ReceiveDataCollect();     // If U Not Understand How To Set The ReceivedBytesThreshold Value Try This Function !!

            try
            {
                string strReciveData = mSerialPort.ReadLine();

                if (strReciveData.Substring(strReciveData.Length - 1).ToString() != "\r")
                {
                    WeightValue = -901;
                    InWaitDataRequestProcess = false;
                }
                if (strReciveData.Replace("\r","") == "ES")
                {
                    InWaitDataRequestProcess = false;
                }

                if (strReciveData.Length > 3)
                {
                    if (strReciveData.Length > 7)
                    {
                        if (strReciveData.Substring(0, 7) == "I50 B 0")
                        {
                            strReciveData = strReciveData.Replace("I50 B 0", ""); //  Added by Luke Chen on 20160317
                            strReciveData = strReciveData.Replace(" ", "");
                            strReciveData = strReciveData.Replace("\r", "");
                            strReciveData = strReciveData.Replace("m", "");
                            strReciveData = strReciveData.Replace("g", "");
                            WeightRemainCapacity = Convert.ToDouble(strReciveData);
                        }
                    }
                    if (strReciveData.Substring(0, 3) == "Z A")
                    {
                        InWaitDataRequestProcess = false;
                        MettlerToledoWX_GetStableValueCmd();
                    }
                    if (strReciveData.Substring(0, 3) == "S -")
                    {
                        WeightValue = 0;
                        InWaitDataRequestProcess = false;
                    }
                    if (strReciveData.Substring(0, 4) == "C3 B")//@1.0.0.43-24@
                    {
                        WeightValue = -99999.999;
                    }
                    if (strReciveData.Substring(0, 4) == "C3 A")//@1.0.0.43-24@
                    {
                        WeightValue = 0;
                        InWaitDataRequestProcess = false;
                    }

                    //  Modified by Luke Chen on 20160317
                    //if (strReciveData.Substring(0, 3) == "S S" || strReciveData.Substring(0, 3) == "S D")
                    if (strReciveData.Substring(0, 3) == "S S" || strReciveData.Substring(0, 3) == "S D" || strReciveData.Substring(0, 3) == "T S")
                    //  ------------------------------------------------------------
                    {
                        bool isTareCommand = strReciveData.Substring(0, 1) == "T" ? true : false;

                        strReciveData = strReciveData.Replace("T", ""); //  Added by Luke Chen on 20160317

                        strReciveData = strReciveData.Replace("S", "");

                        strReciveData = strReciveData.Replace("D", "");

                        strReciveData = strReciveData.Replace("m", "");

                        strReciveData = strReciveData.Replace("g", "");

                        strReciveData = strReciveData.Replace(" ", "");

                        strReciveData = strReciveData.Replace("\r", "");

                        try
                        {
                            WeightValue = Convert.ToDouble(strReciveData);
                            InWaitStableResult = false;
                        }
                        catch (Exception) { WeightValue = -999; }

                        ////  Added by Luke Chen on 20160419
                        //if (isTareCommand)
                        //{
                        //    if (evtTareWeightReceived != null) evtTareWeightReceived(TareWeightValue);
                        //}
                        ////  -----------------------------------
                        //else
                        //{
                        //    if (evtWeightReceived != null) evtWeightReceived(WeightValue);
                        //}

                        InWaitDataRequestProcess = false;
                    }
                }
                mSerialPort_IsConnect = true;
            }
            catch (Exception)
            {
                InWaitDataRequestProcess = false;
                WeightValue = -999;
            }
        }
        private void MettlerToledoWX_GetStableValueCmd()
        {
            if (bIsSimulatorMode == true) { return; }
            if (InWaitDataRequestProcess == true) { return; }
            WeightValue = -99999.999;//Wait Stable
            mSerialPort.ReceivedBytesThreshold = 17;
            try
            {
                mSerialPort.WriteLine("S" + "\r\n");
                InWaitDataRequestProcess = true;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnect = false;
                WeightValue = -999;
            }
        }
        private void MettlerToledoWX_GetValueImmediatelyCmd()
        {
            if (bIsSimulatorMode == true) { return; }
            if (InWaitDataRequestProcess == true) { return; }
            WeightValue = -99999.999;
            mSerialPort.ReceivedBytesThreshold = 17;
            try
            {
                mSerialPort.WriteLine("SI" + "\r\n");
                InWaitDataRequestProcess = true;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnect = false;
                WeightValue = -999;
            }

        }
        private void MettlerToledoWX_GetValueImmediatelyArrayCmd()
        {
            if (bIsSimulatorMode == true) { return; }
            if (InWaitDataRequestProcess == true) { return; }
            WeightValue = -99999.999;
            mSerialPort.ReceivedBytesThreshold = 17;
            try
            {
                mSerialPort.WriteLine("SIR" + "\r\n");
                InWaitDataRequestProcess = true;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnect = false;
                WeightValue = -999;
            }
        }
        private void MettlerToledoWX_ResetValueCmd()
        {
            if (bIsSimulatorMode == true) { return; }
            if (InWaitDataRequestProcess == true) { return; }
            WeightValue = -99999.999;
            mSerialPort.ReceivedBytesThreshold = 5;
            try
            {
                mSerialPort.WriteLine("Z" + "\r\n");
                InWaitDataRequestProcess = true;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnect = false;
                WeightValue = -999;
            }

        }
        private void MettlerToledoWX_CancelCommand()
        {
            if (bIsSimulatorMode == true) { return; }
            try
            {
                mSerialPort.WriteLine("@" + "\r\n");
            }
            catch (Exception)
            {
                mSerialPort_IsConnect = false;
            }
            InWaitDataRequestProcess = false;
            InWaitStableResult = false;
        }
        private void MettlerToledoWX_SendCorrectionCmd()//@1.0.0.43-24@
        {
            if (bIsSimulatorMode == true) { return; }
            if (InWaitDataRequestProcess == true) { return; }
            WeightValue = -99999.999;
            mSerialPort.ReceivedBytesThreshold = 5;
            try
            {
                mSerialPort.WriteLine("C3" + "\r\n");
                InWaitDataRequestProcess = true;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnect = false;
                WeightValue = -999;
            }
        }
        private void MettlerToledoWX_SendRemainCapacity()//@1.0.0.44-8@
        {
            if (bIsSimulatorMode == true) { return; }
            if (InWaitDataRequestProcess == true) { return; }
            WeightRemainCapacity = 0;
            mSerialPort.ReceivedBytesThreshold = 17;
            try
            {
                mSerialPort.WriteLine("I50" + "\r\n");
                InWaitDataRequestProcess = true;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnect = false;
                WeightRemainCapacity = 0;
            }
        }
        private double MettlerToledoWX_GetRemainCapacity()//@1.0.0.44-8@
        {
            MettlerToledoWX_SendRemainCapacity();
            long StartTime = DateTime.Now.Ticks;
            while (InWaitDataRequestProcess)
            {
                if ((DateTime.Now.Ticks - StartTime) > 300 * 10000)
                {
                    MettlerToledoWX_CancelCommand();
                    mSerialPort_IsConnect = false;
                    return -1;
                }
            }
            return WeightRemainCapacity;
        }
        #endregion

        #region//SartoriusCP

        private enum Command
        {
            None,
            Tare,
            StableValue,
            UnStableValue
        }
        private Command RequestCmd = Command.None;

        private void SartoriusCP_DataRecived()
        {
            try
            {
                string strReciveData = mSerialPort.ReadLine();

                double dblValue = 0;

                if (strReciveData.Substring(0, 1) == "N")
                {
                    dblValue = Convert.ToDouble(strReciveData.Substring(9, 7));

                    if (strReciveData.Substring(6, 1) == "-") { dblValue = dblValue * (-1); }

                    InWaitDataRequestProcess = (strReciveData.Substring(17, 2) == "mg") ? true : false;
                }

                switch (RequestCmd)
                {
                    case Command.Tare:
                        if (InWaitDataRequestProcess == true)
                        {
                            WeightValue = dblValue;
                            if (Math.Abs(WeightValue) < 0.03) { RequestCmd = Command.None; }
                        }
                        else
                        {
                            WeightValue = -99999.999;
                        }
                        break;

                    case Command.UnStableValue:
                        WeightValue = dblValue;
                        RequestCmd = Command.None;
                        break;

                    case Command.None:

                        break;

                    case Command.StableValue:
                        if (InWaitDataRequestProcess == true)
                        {
                            WeightValue = dblValue;
                            RequestCmd = Command.None;
                        }
                        else
                        {
                            WeightValue = -99999.999;
                        }

                        break;
                }
                mSerialPort_IsConnect = true;
            }
            catch (Exception)
            {
                mSerialPort_IsConnect = false;
                WeightValue = -999;
            }
        }

        private void ResetValueCmd()
        {
            WeightValue = -99999.999;
            RequestCmd = Command.Tare;
            try
            {
                byte[] bytCommand = new byte[4];

                bytCommand[0] = Convert.ToByte(27);

                bytCommand[1] = Convert.ToByte('T');

                bytCommand[2] = Convert.ToByte(13);

                bytCommand[3] = Convert.ToByte(10);

                mSerialPort.Write(bytCommand, 0, 4);
                InWaitDataRequestProcess = true;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnect = false;
                WeightValue = -999;
            }
        }
        private void GetStableValueCmd()
        {
            WeightValue = -99999.999;
            InWaitDataRequestProcess = true;
            RequestCmd = Command.StableValue;
        }
        private void GetValueImmediatelyCmd()
        {
            WeightValue = -99999.999;
            InWaitDataRequestProcess = true;
            RequestCmd = Command.UnStableValue;
        }
        public void CancelCommand()
        {
            RequestCmd = Command.None;
        }


        #endregion

        #region//ANDWeightScale//@1.0.0.31-7@

        private double ANDWeightScale_GetWeight()
        {
            ANDWeightScale_GetValueImmediatelyCmd();
            long StartTime = DateTime.Now.Ticks;
            while (InWaitDataRequestProcess)
            {
                if ((DateTime.Now.Ticks - StartTime) > 300 * 10000)
                {
                    ANDWeightScale_CancelCommand();
                    mSerialPort_IsConnect = false;
                    return -9999.1;
                }
            }
            return WeightValue;
        }
        private void ANDWeightScale_DataRecived()
        {
            try
            {
                string strReciveData = mSerialPort.ReadLine();

                if (strReciveData.Substring(strReciveData.Length - 1).ToString() != "\r")
                {
                    throw new Exception("SerialPort ReceivedBytesThreshold Set Error , Please Check String Lengh Again !!");
                }

                if (strReciveData.Length > 3)
                {
                    if (strReciveData.Substring(0, 3) == "Z A")
                    {
                        InWaitDataRequestProcess = false;

                        GetStableValueCmd();
                    }

                    if (strReciveData.Substring(0, 2) == "ST" || strReciveData.Substring(0, 2) == "US")
                    {
                        //bool isTareCommand = strReciveData.Substring(0, 1) == "T" ? true : false;

                        strReciveData = strReciveData.Replace("ST", "");

                        strReciveData = strReciveData.Replace("US", "");

                        strReciveData = strReciveData.Replace(",", "");

                        strReciveData = strReciveData.Replace("+", "");

                        strReciveData = strReciveData.Substring(0, 8);

                        try
                        {
                            //if (isTareCommand)
                            //{
                            //    TareWeightValue = Convert.ToDouble(strReciveData);
                            //}
                            //else
                            //{
                            WeightValue = Convert.ToDouble(strReciveData);
                            InWaitStableResult = false;
                            //}
                        }
                        catch (Exception) { WeightValue = -1; }

                        //if (isTareCommand)
                        //{
                        //    if (evtTareWeightReceived != null) evtTareWeightReceived(TareWeightValue);
                        //}
                        //else
                        //{
                        //if (evtWeightReceived != null) evtWeightReceived(WeightValue);
                        //}

                        InWaitDataRequestProcess = false;
                    }
                }
            }
            catch (Exception)
            {
                InWaitDataRequestProcess = false;
                WeightValue = -999;
            }
        }
        private void ANDWeightScale_GetStableValueCmd()
        {
            if (bIsSimulatorMode == true) { return; }
            if (InWaitDataRequestProcess == true) { return; }
            WeightValue = -99999.999;//Wait Stable
            mSerialPort.ReceivedBytesThreshold = 17;
            try
            {
                mSerialPort.WriteLine("S" + "\r\n");
                InWaitDataRequestProcess = true;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnect = false;
                WeightValue = -999;
            }
        }
        private void ANDWeightScale_GetValueImmediatelyCmd()
        {
            if (bIsSimulatorMode == true) { return; }
            if (InWaitDataRequestProcess == true) { return; }
            WeightValue = -99999.999;
            mSerialPort.ReceivedBytesThreshold = 17;
            try
            {
                mSerialPort.WriteLine("SI" + "\r\n");
                InWaitDataRequestProcess = true;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnect = false;
                WeightValue = -999;
            }

        }
        private void ANDWeightScale_GetValueImmediatelyArrayCmd()
        {
            if (bIsSimulatorMode == true) { return; }
            if (InWaitDataRequestProcess == true) { return; }
            WeightValue = -99999.999;
            mSerialPort.ReceivedBytesThreshold = 17;
            try
            {
                mSerialPort.WriteLine("SIR" + "\r\n");
                InWaitDataRequestProcess = true;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnect = false;
                WeightValue = -999;
            }
        }
        private void ANDWeightScale_ResetValueCmd()
        {
            if (bIsSimulatorMode == true) { return; }
            if (InWaitDataRequestProcess == true) { return; }
            WeightValue = 0;
            mSerialPort.ReceivedBytesThreshold = 5;
            try
            {
                mSerialPort.WriteLine("R" + "\r\n");
                //InWaitDataRequestProcess = true;
            }
            catch (Exception ex)
            {
                mSerialPort_IsConnect = false;
                WeightValue = -999;
            }

        }
        private void ANDWeightScale_CancelCommand()
        {
            if (bIsSimulatorMode == true) { return; }
            try
            {
                mSerialPort.WriteLine("C" + "\r\n");
            }
            catch (Exception)
            {
                mSerialPort_IsConnect = false;
            }
            InWaitDataRequestProcess = false;
            InWaitStableResult = false;
        }

        #endregion



        #endregion

    }

    public class ANDWeightScale
    {
        #region //=====================  區域變數設置 =====================

        public bool IsSimulatorMode { get; set; }

        static ANDWeightScale m_Singleton;

        public SerialPort mSerialPort;

        private bool InWaitDataRequestProcess = false;

        public bool IsWeightAccumulative = true;

        #endregion

        #region //=====================  列舉 =====================
        #endregion

        #region //=====================  屬性設置 =====================

        public double WeightValue { get; set; }

        public double TareWeightValue { get; set; } //  Added by Luke Chen on 20160419

        #endregion

        #region //=====================  事件 =====================

        public delegate void WeightReceived(double Value);
        public event WeightReceived evtWeightReceived;

        public delegate void TareWeightReceived(double Value);
        public event TareWeightReceived evtTareWeightReceived;

        public delegate void CommunicationError();
        public event CommunicationError evtCommunicationError;

        #endregion

        #region //=====================  必要函式設置 =====================

        public ANDWeightScale(int baudRate, int dataBits, Handshake handshake, string portName, StopBits stopBits,
            Parity parity, bool dtrEnable)//  int BaudRate, int DataBits, Handshake HandshakeType, int PortNumber, int Timeout)
        {
            if (mSerialPort == null)
            {
                mSerialPort = new SerialPort();
                mSerialPort.DataReceived += new SerialDataReceivedEventHandler(mSerialPort_DataReceived);
            }
            mSerialPort.BaudRate = baudRate;
            mSerialPort.DataBits =dataBits;
            mSerialPort.Handshake = handshake;
            mSerialPort.PortName = portName;
            mSerialPort.StopBits = stopBits;
            mSerialPort.Parity = parity;
            mSerialPort.DtrEnable = dtrEnable;
            mSerialPort.ReadTimeout = 1000;
            mSerialPort.WriteTimeout = 1000;

        }

        public ANDWeightScale()//  int BaudRate, int DataBits, Handshake HandshakeType, int PortNumber, int Timeout)
        {
            if (mSerialPort == null)
            {
                mSerialPort = new SerialPort();
                mSerialPort.DataReceived += new SerialDataReceivedEventHandler(mSerialPort_DataReceived);
            }
        }


        #endregion

        public void Open()
        {
            if (IsSimulatorMode == true) { return; }

            try
            {
                mSerialPort.Open();
            }
            catch (Exception ex)
            {
                if (evtCommunicationError != null) { evtCommunicationError(); } else { throw new Exception("Port Open Error " + ex.Message); }
            }
        }

        public void ResetValueCmd()
        {
            if (IsSimulatorMode == true) { return; }

            //if (InWaitDataRequestProcess == true) { CancelCommand(); return; }

            //WeightValue = -99999.999;

            //evtWeightReceived(WeightValue);

            //mSerialPort.ReceivedBytesThreshold = 5;

            try
            {
                mSerialPort.WriteLine("R" + "\r\n");

                System.Diagnostics.Stopwatch Time = new System.Diagnostics.Stopwatch();

                Time.Restart();
                while (Time.ElapsedMilliseconds < 2000)
                {
                    System.Threading.Thread.Sleep(10);
                }
                Time.Stop();

                InWaitDataRequestProcess = false;

                GetStableValueCmd();
            }
            catch (Exception ex)
            {
                if (evtCommunicationError != null) { evtCommunicationError(); } else { throw new Exception("Data Get Error " + ex.Message); }
            }

            InWaitDataRequestProcess = true;
        }

        public void GetStableValueCmd()
        {
            if (IsSimulatorMode == true) { return; }

            if (InWaitDataRequestProcess == true) { return; }

            WeightValue = -99999.999;

            evtWeightReceived(WeightValue);

            //mSerialPort.ReceivedBytesThreshold = 17;

            try
            {
                mSerialPort.WriteLine("S" + "\r\n");
            }
            catch (Exception ex)
            {
                if (evtCommunicationError != null) { evtCommunicationError(); } else { throw new Exception("Data Get Error " + ex.Message); }
            }

            InWaitDataRequestProcess = true;
        }

        public void GetValueImmediatelyCmd()
        {
            if (IsSimulatorMode == true) { return; }

            if (InWaitDataRequestProcess == true) { return; }

            WeightValue = -99999.999;

            evtWeightReceived(WeightValue);

            //mSerialPort.ReceivedBytesThreshold = 17;

            try
            {
                mSerialPort.WriteLine("SI" + "\r\n");
            }
            catch (Exception ex)
            {
                if (evtCommunicationError != null) { evtCommunicationError(); } else { throw new Exception("Data Get Error " + ex.Message); }
            }

            InWaitDataRequestProcess = true;
        }

        public void GetValueImmediatelyArrayCmd()
        {
            if (IsSimulatorMode == true) { return; }

            if (InWaitDataRequestProcess == true) { return; }

            WeightValue = -99999.999;

            evtWeightReceived(WeightValue);

            mSerialPort.ReceivedBytesThreshold = 17;

            try
            {
                mSerialPort.WriteLine("SIR" + "\r\n");
            }
            catch (Exception ex)
            {
                if (evtCommunicationError != null) { evtCommunicationError(); } else { throw new Exception("Data Get Error " + ex.Message); }
            }

            //InWaitDataRequestProcess = true;
        }

        public void CancelCommand()
        {
            if (IsSimulatorMode == true) { return; }

            //mSerialPort.ReceivedBytesThreshold = 17;

            try
            {
                mSerialPort.WriteLine("C" + "\r\n");
            }
            catch (Exception)
            {
                // if (evtCommunicationError != null) { evtCommunicationError(); } else { throw new Exception("Data Get Error " + ex.Message); }
            }
        }

        //  Added by Luke Chen on 20160317
        //  Tare - Use T to tare the balance. The next stable weight value will be saved in the tare memory.
        public void TareBalanceCmd()
        {
            if (IsSimulatorMode == true) { return; }

            if (InWaitDataRequestProcess == true) { return; }

            TareWeightValue = -99999.999;

            evtTareWeightReceived(TareWeightValue);

            mSerialPort.ReceivedBytesThreshold = 17;

            try
            {
                mSerialPort.WriteLine("T" + "\r\n");
            }
            catch (Exception ex)
            {
                if (evtCommunicationError != null) { evtCommunicationError(); } else { throw new Exception("Data Get Error " + ex.Message); }
            }

            InWaitDataRequestProcess = true;
        }
        //  ------------------------------------------------------------------------------

        private void mSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //int intVlaue = ReceiveDataCollect();     // If U Not Understand How To Set The ReceivedBytesThreshold Value Try This Function !!

            string strReciveData = mSerialPort.ReadLine();

            if (strReciveData.Substring(strReciveData.Length - 1).ToString() != "\r")
            {
                throw new Exception("SerialPort ReceivedBytesThreshold Set Error , Please Check String Lengh Again !!");
            }

            if (strReciveData.Length > 3)
            {
                if (strReciveData.Substring(0, 3) == "Z A")
                {
                    InWaitDataRequestProcess = false;

                    GetStableValueCmd();
                }

                if (strReciveData.Substring(0, 2) == "ST" || strReciveData.Substring(0, 2) == "US")
                {
                    //bool isTareCommand = strReciveData.Substring(0, 1) == "T" ? true : false;

                    strReciveData = strReciveData.Replace("ST", "");

                    strReciveData = strReciveData.Replace("US", "");

                    strReciveData = strReciveData.Replace(",", "");

                    strReciveData = strReciveData.Replace("+", "");

                    strReciveData = strReciveData.Substring(0, 8);

                    try
                    {
                        //if (isTareCommand)
                        //{
                        //    TareWeightValue = Convert.ToDouble(strReciveData);
                        //}
                        //else
                        //{
                        WeightValue = Convert.ToDouble(strReciveData);
                        //}
                    }
                    catch (Exception) { WeightValue = -1; }

                    //if (isTareCommand)
                    //{
                    //    if (evtTareWeightReceived != null) evtTareWeightReceived(TareWeightValue);
                    //}
                    //else
                    //{
                    if (evtWeightReceived != null) evtWeightReceived(WeightValue);
                    //}

                    InWaitDataRequestProcess = false;
                }
            }
        }
    }
}

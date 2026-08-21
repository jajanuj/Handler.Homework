using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Modbus;
using Modbus.Device;
using System.IO.Ports;

namespace ArtSystem.Files
{

    public abstract class clsModbus
    {
        #region //========== 通訊異常 委派事件 ==========

        /// <summary> 通訊異常事件的委派 </summary>
        public delegate void CommunicationError();
        /// <summary> 表示處理通訊異常事件的方法 </summary>
        public event CommunicationError evtCommunicationError = delegate { };

        #endregion

        #region //========== Parameter 參數 ==========

        /// <summary> 序列埠的實體，用來建立nModbus連線用。 </summary>        
        private SerialPort m_SerialPort = null;
        /// <summary> Modbus 通訊協定。 </summary>
        private IModbusSerialMaster m_Master;

        /// <summary> ComPort 是否已經開啟，如果通訊異常 bIsPortOpen 會為False</summary>
        private bool bIsPortOpen = false;
        /// <summary> ComPort 已經開啟，但是通訊異常 bIsCommunicationError 會為False</summary>
        private bool bIsCommunicationError = false;
        /// <summary> ComPort 的 Slave站號 </summary>
        protected int iStationID = 0;
        private bool _bIsSimulatorMode = false;
        /// <summary> ComPort 是否為模擬模式 </summary>
        public bool bIsSimulatorMode
        {
            get
            {
                return _bIsSimulatorMode;
            }
            set
            {
                _bIsSimulatorMode = value;
            }
        }



        
        #endregion

        #region //========== Function 連線 ==========

        /// <summary> 建立連線 </summary>
        /// <param name="p_ComPort"> 序列阜的名稱 </param>
        /// <param name="p_StationID"> 控制器的站號 </param>
        /// <param name="p_IsSimulatorMode"> 是否為模擬模式 </param>
        /// <returns> 是否建立連線成功 </returns>
        public bool ComPort_Open(string p_ComPort, int p_StationID, bool p_SimulatorMode, int p_ReadWriteTimeout = 300)
        {
            bIsPortOpen = false;
            iStationID = p_StationID;
            bIsSimulatorMode = p_SimulatorMode;
            if (bIsSimulatorMode == true) { return true; }
            try
            {
                //Serial Port Open
                m_SerialPort = new SerialPort(p_ComPort);
                m_SerialPort.BaudRate = 19200;
                m_SerialPort.DataBits = 8;
                m_SerialPort.Parity = Parity.None;
                m_SerialPort.StopBits = StopBits.One;
                m_SerialPort.Handshake = Handshake.None;
                m_SerialPort.ReadTimeout = p_ReadWriteTimeout;
                m_SerialPort.WriteTimeout = p_ReadWriteTimeout;
                m_SerialPort.Open();
                
                //將Modbus模組連結到Serial Port
                m_Master = Modbus.Device.ModbusSerialMaster.CreateRtu(m_SerialPort);
                m_Master.Transport.ReadTimeout = p_ReadWriteTimeout;
                m_Master.Transport.WriteTimeout = p_ReadWriteTimeout;
                m_Master.Transport.Retries = 0;

                bIsPortOpen = true;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>  ComPort 關閉連線 </summary>
        public void ComPort_Close()
        {
            try
            {
                m_SerialPort.Close();
                m_SerialPort.Dispose();
                bIsPortOpen = false;
            }
            catch (Exception)
            {
                bIsPortOpen = false;
            }
        }
        /// <summary>  ComPort 是否已開啟連線 </summary>
        public bool ComPort_IsOpen()
        {
            return bIsPortOpen;
        }
        /// <summary>  ComPort 是否通訊異常 </summary>
        public bool ComPort_IsCommunicationError()
        {
            if (bIsSimulatorMode == true)
            {
                return false;
            }
            return bIsCommunicationError;
        }

        #endregion

        #region //========== Function 通訊 ==========

        /// <summary> 由Modbus Read API讀區資料 </summary>
        /// <param name="p_StartNo"> 設定讀取的資料啟始位址 </param>
        /// <param name="p_DataNum"> 設定讀取的資料量，單位是byte </param>
        /// <returns> 回傳讀取的資料 </returns>
        public ushort[] GetData(int p_StartNo, int p_DataNum)
        {
            ushort[] tValues = new ushort[p_DataNum];
            if (bIsSimulatorMode == true) { return tValues; }
            if (bIsPortOpen == false) { evtCommunicationError(); return null; }

            uint tRetryCnt = 3;
            for (int i = 0; i <= tRetryCnt; i++)
            {
                try
                {
                    tValues = m_Master.ReadHoldingRegisters((byte)iStationID, (ushort)p_StartNo, (ushort)p_DataNum);
                    bIsCommunicationError = false;
                    return tValues;
                }
                catch (Exception)
                {
                    evtCommunicationError();
                    bIsCommunicationError = true;
                }
            }
            return null;
        }

        /// <summary> 由Modbus Write API寫入資料 </summary>
        /// <param name="p_StartNo">設定你要寫的資料啟始位址</param>
        /// <param name="p_DataNum">你要寫的資料長度單位是Byte</param>
        /// <param name="p_Values">要寫的參數值</param>
        /// <returns> 是否寫入成功 </returns>
        public bool SetData(int p_StartNo, int p_DataNum, ushort[] p_Values)
        {
            if (bIsSimulatorMode == true) { return true; }
            if (bIsPortOpen == false) { evtCommunicationError(); return false; }
            ushort[] Temp = new ushort[p_DataNum];
            for (int i = 0; i < p_Values.Length; i++)
            //for (int i = 0; i < p_DataNum; i++)
            {
                if (i < p_DataNum)
                {
                    Temp[i] = p_Values[i];
                }
            }
            bool Result = false;
            try
            {
                //m_Master.WriteMultipleRegisters((byte)iStationID, (ushort)p_StartNo, p_Values);
                m_Master.WriteMultipleRegisters((byte)iStationID, (ushort)p_StartNo, Temp);
                Result = true;
                bIsCommunicationError = false;

            }
            catch (Exception)
            {
                evtCommunicationError();
                Result = false;
                bIsCommunicationError = true;
            }
            return Result;
        }

        #endregion

    }

}

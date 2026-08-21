using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtSystem.MultiSystem
{
    public class clsCtrlRollerMotor
    {
        #region //=====================  區域變數設置 =====================

        private int bMotionStop = 0;
        public string sName = "";
        public clsPmtRollerMotor.enuRollerType eRollerType
        {
            get;
            private set;
        }
        public Dictionary<clsPmtRollerMotor.enuPmtName, string> pPmt = null;
        public clsEnum.enuDo? eDO_Start = null;
        public clsEnum.enuDo? eDO_Slow = null;
        public clsEnum.enuDo? eDO_Reverse = null;
        #endregion

        #region //=====================  必要函式設置 =====================

        public clsMotionRoller mMotionRoller = null;
        public clsCtrlRollerMotor(Dictionary<clsPmtRollerMotor.enuPmtName, string> p_Pmt)
        {
            pPmt = p_Pmt;
            clsPmtRollerMotor.enuRollerType ReadRollerType = clsPmtRollerMotor.enuRollerType.ExtionCommand;
            if(Enum.IsDefined(typeof(clsPmtRollerMotor.enuRollerType), pPmt[clsPmtRollerMotor.enuPmtName.RollerType]) == true)
            {
                ReadRollerType = (clsPmtRollerMotor.enuRollerType)Enum.Parse(typeof(clsPmtRollerMotor.enuRollerType), pPmt[clsPmtRollerMotor.enuPmtName.RollerType]);
            }
            eRollerType = ReadRollerType;
            if (eRollerType == clsPmtRollerMotor.enuRollerType.DIOControl_Only)
            {
            }
            else
            {
                string strCOM = "COM" + pPmt[clsPmtRollerMotor.enuPmtName.COM_Port];
                int iMotorID = Convert.ToInt32(pPmt[clsPmtRollerMotor.enuPmtName.Motor_ID]);
                clsMotionRoller.enuRollerType eType = (clsMotionRoller.enuRollerType)Enum.Parse(typeof(clsMotionRoller.enuRollerType), pPmt[clsPmtRollerMotor.enuPmtName.RollerType]);
                if (Enum.IsDefined(typeof(clsMotionRoller.enuRollerType), eType) == false)
                { eType = clsMotionRoller.enuRollerType.ExtionCommand; }
                mMotionRoller = new clsMotionRoller(strCOM, iMotorID, eType);

                //馬達電流
                double dMotorCurrentPower = Convert.ToDouble(pPmt[clsPmtRollerMotor.enuPmtName.CurrentPower]);
                if (dMotorCurrentPower != 0)
                { mMotionRoller.SetCurrent(dMotorCurrentPower); }
            }
            if (Enum.IsDefined(typeof(clsEnum.enuDo), pPmt[clsPmtRollerMotor.enuPmtName.eDO_Start]) == true)
            {
                eDO_Start = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), pPmt[clsPmtRollerMotor.enuPmtName.eDO_Start]);
            }
            if (Enum.IsDefined(typeof(clsEnum.enuDo), pPmt[clsPmtRollerMotor.enuPmtName.eDO_Slow]) == true)
            {
                eDO_Slow = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), pPmt[clsPmtRollerMotor.enuPmtName.eDO_Slow]);
            }
            if (Enum.IsDefined(typeof(clsEnum.enuDo), pPmt[clsPmtRollerMotor.enuPmtName.eDO_Reverse]) == true)
            {
                eDO_Reverse = (clsEnum.enuDo)Enum.Parse(typeof(clsEnum.enuDo), pPmt[clsPmtRollerMotor.enuPmtName.eDO_Reverse]);
            }
        }

        #endregion

        #region//========== clsMotionRoller Public函式 ==========

        public static byte[] GetCRC(byte[] p_DataBytes)
        {
            byte[] rValue = null;
            rValue = clsMotionRoller.GetCRC(p_DataBytes);
            return rValue;
        }
        public int? GetPos(int p_iRetryCount = 3)
        {
            int? rValue = null;
            if (mMotionRoller != null)
            {
                rValue = mMotionRoller.GetPos(p_iRetryCount);
            }
            return rValue;
        }
        public bool? IsBusy(int p_iRetryCount = 3)
        {
            bool? rValue = null;
            if (mMotionRoller != null)
            {
                rValue = mMotionRoller.IsBusy(p_iRetryCount);
            }
            return rValue;
        }
        public bool KeepMove(enuMoveDir p_enuMoveDir, int p_iRetryCount = 3)
        {
            bool rValue = false;
            if (mMotionRoller != null)
            {
                bMotionStop = 0;
                rValue = mMotionRoller.KeepMove(p_enuMoveDir, p_iRetryCount);
            }
            return rValue;
        }
        public static void Release()
        {
            clsMotionRoller.Release();
        }
        public bool SendData(string p_strSendData)
        {
            bool rValue = false;
            if (mMotionRoller != null)
            {
                rValue = mMotionRoller.SendData(p_strSendData);
            }
            return rValue;
        }
        public bool SetAxisVel(int p_iVel, int p_iRetryCount = 3)
        {
            bool rValue = false;
            if (mMotionRoller != null)
            {
                rValue = mMotionRoller.SetAxisVel(p_iVel, p_iRetryCount);
            }
            return rValue;
        }
        public bool SetCurrent(double p_dValue = 0.5, int p_iRetryCount = 3)
        {
            bool rValue = false;
            if (mMotionRoller != null)
            {
                rValue = mMotionRoller.SetCurrent(p_dValue, p_iRetryCount);
            }
            return rValue;
        }
        public bool SetCurrentDown(double p_dValue = 50, int p_iRetryCount = 3)
        {
            bool rValue = false;
            if (mMotionRoller != null)
            {
                rValue = mMotionRoller.SetCurrentDown(p_dValue, p_iRetryCount);
            }
            return rValue;
        }
        public bool SetModbusData(ushort p_iAddress, ushort p_Values)
        {
            bool rValue = false;
            if (mMotionRoller != null)
            {
                rValue = mMotionRoller.SetModbusData(p_iAddress, p_Values);
            }
            return rValue;
        }
        public bool SetPos(double p_dPosition, int p_iRetryCount = 3)
        {
            bool rValue = false;
            if (mMotionRoller != null)
            {
                rValue = mMotionRoller.SetPos(p_dPosition, p_iRetryCount);
            }
            return rValue;
        }
        public bool SetVelCtrlMode(clsMotionRoller.enuVelCtrlMode p_enuVelCtrlMode, int p_iRetryCount = 3)
        {
            bool rValue = false;
            if (mMotionRoller != null)
            {
                rValue = mMotionRoller.SetVelCtrlMode(p_enuVelCtrlMode, p_iRetryCount);
            }
            return rValue;
        }
        public bool SlowDownStop(int p_iRetryCount = 3)
        {
            bool rValue = false;
            if (mMotionRoller != null)
            {
                rValue = mMotionRoller.SlowDownStop(p_iRetryCount);
            }
            return rValue;
        }
        public bool StartMoveA(double p_dTargetPos, int p_iRetryCount = 3)
        {
            bool rValue = false;
            if (mMotionRoller != null)
            {
                bMotionStop = 0;
                rValue = mMotionRoller.StartMoveA(p_dTargetPos, p_iRetryCount);
            }
            return rValue;
        }
        public bool StartMoveR(double p_dTargetPos, int p_iRetryCount = 3)
        {
            bool rValue = false;
            if (mMotionRoller != null)
            {
                bMotionStop = 0;
                rValue = mMotionRoller.StartMoveR(p_dTargetPos, p_iRetryCount);
            }
            return rValue;
        }


        public string GetFWVersion()
        {
            string rValue = "";
            try
            {
                if (clsArtSystem.bIsSoftwareSimulate == false)
                {
                    string m_strLogName = "MotionRollerLog";
                    string strLogPath = ucLogPath.GetSingleton().GetLogPath(m_strLogName);
                    string strLogFullPath = ucLogPath.GetSingleton().GetLogPath(m_strLogName) + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "-" + m_strLogName + ".log";
                    if (System.IO.Directory.Exists(strLogPath) == false)
                    { System.IO.Directory.CreateDirectory(strLogPath); }
                    if (System.IO.Directory.Exists(strLogPath) == true)
                    {
                        if (System.IO.File.Exists(strLogFullPath) == false)
                        { System.IO.File.Create(strLogFullPath); }
                        DateTime FileLastWriteTime = System.IO.File.GetLastWriteTime(strLogFullPath);
                        SendData("ver");
                        Thread.Sleep(100);
                        string LastData = System.IO.File.ReadLines(strLogFullPath).Last(v => v.Contains("Firmware Ver :"));
                        if (LastData.Contains("Firmware Ver :"))
                        {
                            rValue = LastData.Split(':')[1].Replace(" ", "");
                        }
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

        #region//========== Pirvate 函式 ==========
        private void SetDO(bool Start, bool Slow, bool Reverse)
        {
            if (ucRollerMotorSetting.GetSingleton()._RollerMotor_GetPmt_Value(sName, clsPmtRollerMotor.enuPmtName.Invert) == "1")
            {
                Reverse = !Reverse;
            }
            if (Start == false)
            {
                Reverse = false;
            }
            if (eDO_Reverse != null)
            {
                clsDioCtrl.SetDo((clsEnum.enuDo)eDO_Reverse, Reverse);
            }
            if (eDO_Slow != null)
            {
                clsDioCtrl.SetDo((clsEnum.enuDo)eDO_Slow, Slow);
            }
            if (eDO_Start != null)
            {
                clsDioCtrl.SetDo((clsEnum.enuDo)eDO_Start, Start);
            }
        }
        #endregion

        #region//========== Public函式 ==========

        public bool RunFast(enuMoveDir eDir)
        {
            bool rValue = false;
            bMotionStop = 0;
            int dSpeed = 3000;
            string sValue = ucRollerMotorSetting.GetSingleton()._RollerMotor_GetPmt_Value(sName, clsPmtRollerMotor.enuPmtName.HighSpeed_pps);
            try
            {
                if (sValue != "")
                {
                    dSpeed = int.Parse(sValue);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            if (eRollerType == clsPmtRollerMotor.enuRollerType.ExCmd_WithDIO
                || eRollerType == clsPmtRollerMotor.enuRollerType.DIOControl_Only)
            {
                SetDO(true, false, eDir == enuMoveDir.Positive ? false : true);
            }
            else
            {
                this.SetAxisVel(dSpeed);
                rValue = this.KeepMove(eDir);
            }
            return rValue;
        }

        public bool RunSlow(enuMoveDir eDir)
        {
            bool rValue = false;
            bMotionStop = 0; 
            int dSpeed = 1000;
            string sValue = ucRollerMotorSetting.GetSingleton()._RollerMotor_GetPmt_Value(sName, clsPmtRollerMotor.enuPmtName.LowSpeed_pps);
            try
            {
                if (sValue != "")
                {
                    dSpeed = int.Parse(sValue);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            if (eRollerType == clsPmtRollerMotor.enuRollerType.ExCmd_WithDIO
                || eRollerType == clsPmtRollerMotor.enuRollerType.DIOControl_Only)
            {
                SetDO(true, true, eDir == enuMoveDir.Positive ? false : true);
            }
            else
            {
                this.SetAxisVel(dSpeed);
                rValue = this.KeepMove(eDir);
            }
            return rValue;
        }

        public bool StopRun(bool SendDirect = false)
        {
            bool rValue = false;
            if (eRollerType == clsPmtRollerMotor.enuRollerType.ExCmd_WithDIO
                || eRollerType == clsPmtRollerMotor.enuRollerType.DIOControl_Only)
            {
                SetDO(false, false, false);
                rValue = true;
            }
            else
            {
                if (SendDirect == true)
                { bMotionStop = 0; }
                if (bMotionStop < 5)
                {
                    bMotionStop++;
                    this.SlowDownStop();
                    rValue = true;
                }
            }
            return rValue;
        }

        public bool SendAction(bool Run, enuMoveDir eDir = enuMoveDir.Positive, bool Slow = false)
        {
            bool rValue = false;
            if (Run == true)
            {
                if (Slow == false)
                {
                    rValue = RunFast(eDir);
                }
                else
                {
                    rValue = RunSlow(eDir);
                }
            }
            else
            {
                rValue = StopRun();
            }
            return rValue;
        }

        #endregion
    }
}

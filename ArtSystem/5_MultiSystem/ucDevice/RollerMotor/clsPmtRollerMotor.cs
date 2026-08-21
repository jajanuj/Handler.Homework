using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtSystem.MultiSystem
{
    public class clsPmtRollerMotor
    {
        #region//========== Enum 定義 ==========
        public enum enuPmtName
        {
            RollerName,
            COM_Port,
            Motor_ID,
            RollerType,
            HighSpeed_pps,
            LowSpeed_pps,
            Invert,
            eDO_Start,
            eDO_Slow,
            eDO_Reverse,
            ePmt_HighSpeed,
            ePmt_LowSpeed,
            CurrentPower,
        }
        public enum enuRollerType
        {
            ExtionCommand = 0,
            ExtionKnob = 1,
            Troy_SU24H = 2,
            ExCmd_WithDIO = 3,
            DIOControl_Only = 4,
        }
        #endregion

        #region//========== 參數 ==========

        public string sINIPath
        {
            get;
            private set;
        }
        public Dictionary<string, Dictionary<enuPmtName, string>> mDic_mPmtValue = new Dictionary<string, Dictionary<enuPmtName, string>>();
        //public Dictionary<string, clsMotionRoller> mDic_RollerMotor = new Dictionary<string, clsMotionRoller>();
        public Dictionary<string, clsCtrlRollerMotor> mDic_CtrlRollerMotor = new Dictionary<string, clsCtrlRollerMotor>();

        #endregion

        #region//========== Public函式 ==========

        public clsCtrlRollerMotor GetMotionRoller(int iIndex)
        {
            if (iIndex >= 0 && iIndex < mDic_CtrlRollerMotor.Count)
            {
                return mDic_CtrlRollerMotor.ElementAt(iIndex).Value;
            }
            return null;
        }
        public clsCtrlRollerMotor GetMotionRoller(string sKey)
        {
            if ( mDic_CtrlRollerMotor.ContainsKey(sKey))
            {
                return mDic_CtrlRollerMotor[sKey];
            }
            return null;
        }
        public void ReleaseAllMotionRoller()
        {
            mDic_CtrlRollerMotor.Clear();
            clsMotionRoller.Release();
        }
        public void InitialMotionRoller()
        {
            string sPath = ArtSystem.MultiSystem.clsMultiSystem.strSystemINIPath + "\\RollerMotor.ini";
            if (System.IO.File.Exists("D:\\Parameter\\INI\\RollerMotor.ini") == true)
            {  sPath = "D:\\Parameter\\INI\\RollerMotor.ini";  }
            Load(sPath);
            ReleaseAllMotionRoller();
            try
            {
                foreach (string sKey in mDic_mPmtValue.Keys)
                {
                    //string strCOM = "COM" + mDic_mPmtValue[sKey][enuPmtName.COM_Port];
                    //int iMotorID = Convert.ToInt32(mDic_mPmtValue[sKey][enuPmtName.Motor_ID]);
                    //clsMotionRoller.enuRollerType eType = (clsMotionRoller.enuRollerType)Enum.Parse(typeof(clsMotionRoller.enuRollerType), mDic_mPmtValue[sKey][enuPmtName.RollerType]);
                    //if (Enum.IsDefined(typeof(clsMotionRoller.enuRollerType), eType) == false)
                    //{ eType = clsMotionRoller.enuRollerType.ExtionCommand; }
                    //mDic_CtrlRollerMotor.Add(sKey, new clsCtrlRollerMotor(strCOM, iMotorID, eType));
                    mDic_CtrlRollerMotor.Add(sKey, new clsCtrlRollerMotor(mDic_mPmtValue[sKey]));
                    mDic_CtrlRollerMotor[sKey].sName = sKey;
                    mDic_CtrlRollerMotor[sKey].SendData("ver");
                    GetRollerMotorVerInfo(mDic_CtrlRollerMotor[sKey]);
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
                formMessageBox.Show("Initial Motion Roller, Catch Error");
            }
        }
        #endregion

        #region//========== Private函式==========
        private void GetRollerMotorVerInfo(clsCtrlRollerMotor p_Control)
        {
            if (clsArtSystem.bIsSoftwareSimulate == false)
            {
                clsDeviceReport mDriverReport = new clsDeviceReport();
                mDriverReport.DeviceName = p_Control.sName;
                mDriverReport.DeviceType = "RollerMotor";
                mDriverReport.FwVersion = p_Control.GetFWVersion();
                int iComID = Convert.ToInt32(p_Control.pPmt[clsPmtRollerMotor.enuPmtName.COM_Port]);
                int iMotorID = Convert.ToInt32(p_Control.pPmt[clsPmtRollerMotor.enuPmtName.Motor_ID]);
                mDriverReport.SaveInfoSerialPort(iComID, iMotorID);
            }
        }
        #endregion

        #region//========== Public函式(Save,Load) ==========

        /// <summary> 儲存參數 </summary>
        public void Save(string sPath)
        {
            try
            {
                sINIPath = sPath;
                if (System.IO.File.Exists(sPath) == true)
                { System.IO.File.Delete(sPath); }
                clsIniFile mFile = new clsIniFile(sPath);
                foreach (string SectionName in mDic_mPmtValue.Keys)
                {
                    foreach (enuPmtName PmtName in mDic_mPmtValue[SectionName].Keys)
                    {
                        mFile.WriteValue(SectionName, PmtName.ToString(), mDic_mPmtValue[SectionName][PmtName]);
                        //if (PmtName == enuPmtName.LowSpeed_pps)
                        //{
                        //    string sEnumPmtLowSpeed = mDic_mPmtValue[SectionName][clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed];
                        //    if (Enum.IsDefined(typeof(clsEnum.enuPmtName), sEnumPmtLowSpeed) == true)
                        //    {
                        //        clsEnum.enuPmtName ePmt = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), sEnumPmtLowSpeed);
                        //        ucParameter.SaveValue(clsEnum.enuPmtType.System, ePmt, mDic_mPmtValue[SectionName][PmtName]);
                        //    }
                        //}
                        //if (PmtName == enuPmtName.HighSpeed_pps)
                        //{
                        //    string sEnumPmtHighSpeed = mDic_mPmtValue[SectionName][clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed];
                        //    if (Enum.IsDefined(typeof(clsEnum.enuPmtName), sEnumPmtHighSpeed) == true)
                        //    {
                        //        clsEnum.enuPmtName ePmt = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), sEnumPmtHighSpeed);
                        //        ucParameter.SaveValue(clsEnum.enuPmtType.System, ePmt, mDic_mPmtValue[SectionName][PmtName]);
                        //    }
                        //}
                    }
                }
                Load(sINIPath);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> 寫入參數 </summary>
        public void Load(string sPath)
        {
            try
            {
                sINIPath = sPath;
                //mDic_mPmtValue.Clear();
                if (System.IO.File.Exists(sPath) == true)
                {
                    clsIniFile mFile = new clsIniFile(sPath);
                    string[] SectionName = mFile.GetSectionNames();

                    List<string> RemoveItems = new List<string>();
                    foreach (string sKey in mDic_mPmtValue.Keys)
                    {
                        RemoveItems.Add(sKey);
                    }
                    for (int iSection = 0; iSection < SectionName.Length; iSection++)
                    {
                        if (mDic_mPmtValue.ContainsKey(SectionName[iSection]) == false)
                        {
                            mDic_mPmtValue.Add(SectionName[iSection], new Dictionary<enuPmtName, string>());
                        }
                        else
                        {
                            if (RemoveItems.Contains(SectionName[iSection]) == true)
                            {
                                RemoveItems.Remove(SectionName[iSection]);
                            }
                        }
                    }
                    foreach (string sKey in RemoveItems)
                    {
                        mDic_mPmtValue.Remove(sKey);
                    }  
                    for (int iSection = 0; iSection < SectionName.Length; iSection++)
                    {
                        string[] PmtName = mFile.GetKeyNames(SectionName[iSection]);
                        for (int iKey = 0; iKey < PmtName.Length; iKey++)
                        {
                            if (Enum.IsDefined(typeof(enuPmtName), PmtName[iKey]) == true)
                            {
                                enuPmtName mPmt = (enuPmtName)Enum.Parse(typeof(enuPmtName), PmtName[iKey]);
                                mDic_mPmtValue[SectionName[iSection]][mPmt] = mFile.GetString(SectionName[iSection], PmtName[iKey], "");
                                mDic_mPmtValue[SectionName[iSection]][enuPmtName.RollerName] = SectionName[iSection];
                            }
                        }
                        foreach (enuPmtName pPmtName in Enum.GetValues(typeof(enuPmtName)))
                        {
                            if (mDic_mPmtValue[SectionName[iSection]].ContainsKey(pPmtName) == false)
                            {
                                mDic_mPmtValue[SectionName[iSection]].Add(pPmtName, "");
                            }
                            if (pPmtName == enuPmtName.CurrentPower)
                            {
                                 if (mDic_mPmtValue[SectionName[iSection]][pPmtName] == ""
                                    || mDic_mPmtValue[SectionName[iSection]][pPmtName] == "0")
                                {
                                    mDic_mPmtValue[SectionName[iSection]][pPmtName] = "0.5";
                                }
                            }
                            //if (pPmtName == enuPmtName.LowSpeed_pps)
                            //{
                            //    string sEnumPmtLowSpeed = mDic_mPmtValue[SectionName[iSection]][clsPmtRollerMotor.enuPmtName.ePmt_LowSpeed];
                            //    if (Enum.IsDefined(typeof(clsEnum.enuPmtName), sEnumPmtLowSpeed) == true)
                            //    {
                            //        clsEnum.enuPmtName ePmt = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), sEnumPmtLowSpeed);
                            //        if (ucParameter.GetValueString(ePmt) == "")
                            //        {
                            //            ucParameter.SaveValue(clsEnum.enuPmtType.System, ePmt, mDic_mPmtValue[SectionName[iSection]][pPmtName]);
                            //        }
                            //        mDic_mPmtValue[SectionName[iSection]][pPmtName] = ucParameter.GetValueString(ePmt);
                            //    }
                            //}
                            //if (pPmtName == enuPmtName.HighSpeed_pps)
                            //{
                            //    string sEnumPmtHighSpeed = mDic_mPmtValue[SectionName[iSection]][clsPmtRollerMotor.enuPmtName.ePmt_HighSpeed];
                            //    if (Enum.IsDefined(typeof(clsEnum.enuPmtName), sEnumPmtHighSpeed) == true)
                            //    {
                            //        clsEnum.enuPmtName ePmt = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), sEnumPmtHighSpeed);
                            //        if (ucParameter.GetValueString(ePmt) == "")
                            //        {
                            //            ucParameter.SaveValue(clsEnum.enuPmtType.System, ePmt, mDic_mPmtValue[SectionName[iSection]][pPmtName]);
                            //        }
                            //        mDic_mPmtValue[SectionName[iSection]][pPmtName] = ucParameter.GetValueString(ePmt);
                            //    }
                            //}
                        }
                    }
                }
                else
                {
                    mDic_mPmtValue.Clear();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion
    }
}

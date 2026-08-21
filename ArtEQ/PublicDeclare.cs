using System;
using ART.Security;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtProcModuleLib;
using ArtSystem;
using ArtSystem.MultiSystem;

namespace ArtEQ
{
    public class PublicDeclare
    {
        #region//========== 模擬設定 ==========

        static public bool bIsSimulate
        {
            get
            {
                return clsArtSystem.bIsSoftwareSimulate;
            }
        }

        static public bool bIsDiaTwin = false;
        #endregion

        #region//========== 機器設定 ==========

        /// <summary> 系統名稱 </summary>
        static public string sSystemName
        {
            get
            {
                return clsMultiSystem.sSystemName;
            }
        }


        #endregion

        static public clsInfoBase.clsBoatInfo GetData_Boat(clsEnum.enuProcName p_eProcName)
        {
            clsInfoBase.clsBoatInfo rValue = null;
            //clsBaseProc mPM = PM.GetSingleton().GetPM(clsEnum.enuProcName.PM_Lane);
            //if (mPM != null)
            //{
            //    rValue = mPM.GetBoatData();
            //}
            return rValue;
        }


        #region//========== Recipe管理 ==========

        static public bool RecipeCopy(string SourceFile, string TargetFile)
        {
            bool rValue = true;
            try
            {

            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
            return rValue;
        }

        #endregion

        #region//========== WarnningMessage ==========

        /// <summary> 更新Warnning訊息,等級最高的先寫入(會顯示在最前面) </summary>
        static public void ReflashWarnningMessage()
        {
            ucWarnningMessage.GetSingleton()._SetWarning("Security Check Error", clsArtSystem.GetSecurityCheckStatus(1) != SecurityError.NoError);
            ucWarnningMessage.GetSingleton()._SetWarning("Software_Need_Restart", ArtSystem.MultiSystem.clsMultiSystem.bIsMultiSystem_SettingChanged == true);
            ucWarnningMessage.GetSingleton()._SetWarning("Software_System_Changed", ArtSystem.MultiSystem.clsMultiSystem.bIsMultiSystem_SettingChanged == true);




            //ucWarnningMessage.GetSingleton()._SetWarning(clsEnum.enuWarning.AutoLogout_Block, ArtSystem.Login.ucAutoLogout.GetSingleton().bAutoLogoutBlock == true);
            ucWarnningMessage.GetSingleton()._SetWarning("Software_Catch_Occour", clsArtSystem.bIsCatchOccour());
            #region//參數路徑不要幫定在程式內 (調機程式例外)
            if (clsArtSystem.bIsArtMachineSetup == false)
            {
                foreach (clsEnum.enuPmtType ePmtType in Enum.GetValues(typeof(clsEnum.enuPmtType)))
                {
                    if (ePmtType == clsEnum.enuPmtType.Recipe)
                    {
                        ucWarnningMessage.GetSingleton()._SetWarning("Recipe_Path_In_Code", ucParameter.GetFilePath(clsEnum.enuPmtType.Recipe).Contains("bin\\Debug") == true);
                    }
                    else if (ePmtType == clsEnum.enuPmtType.System)
                    {
                        ucWarnningMessage.GetSingleton()._SetWarning("System_Path_In_Code", ucParameter.GetFilePath(clsEnum.enuPmtType.System).Contains("bin\\Debug") == true);
                    }
                    else
                    {
                        ucWarnningMessage.GetSingleton()._SetWarning("Parameter_Path_In_Code", ucParameter.GetFilePath(clsEnum.enuPmtType.System).Contains("bin\\Debug") == true);
                    }
                }
            }
            #endregion
            //ucWarnningMessage.GetSingleton()._SetWarning(clsEnum.enuWarning.High_CPU_Usage, ucArtMain_Design.GetSingleton()._GetCPUUsage() > 20);
            ucWarnningMessage.GetSingleton()._SetWarning("Software_Simulate", clsArtSystem.bIsSoftwareSimulate);
        }

        static bool GetSecurityChecker()
        {
            bool rValue = false;
            if (SecurityChecker.LiscenseAlive == true)
            {
                rValue = true;
            }
            else if (SecurityChecker.ExpiredDate.Year > 9000)
            {
                rValue = true;
            }
            else
            {
                var dd = SecurityChecker.Error;
            }
            return rValue;
        }

        #endregion

        #region//========== AP Alarm ==========

        static public void ReflashAPAlram()
        {
            try
            {
                foreach (clsBaseProc pBaseProc in clsBaseProc.m_dctProcData.Values)
                {
                    clsEditRunThread.ReportProcAlarm(pBaseProc);
                    pBaseProc.m_dctAlarmMessage.Clear();
                }
                foreach (AutoProcess pBaseProc in AutoProcess.m_dctProcData.Values)
                {
                    clsEditRunThread.ReportModuleAlarm(pBaseProc);
                    pBaseProc.m_DctAPAlarm.Clear();
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        #endregion 

        #region//========== 馬達相關(安全判斷, 異常代碼) ==========

        static public bool MotionAxisSafe(clsEnum.enuAxis eAxisID, clsEnum.MoveMode p_ActionMove, double Position)
        {
            bool rValue = true;
            return rValue;
        }
        static public void MotionAlarmCodeSetting()
        {
            try
            {
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }
        static public string MotionAlarmCode(clsEnum.enuAxis eAxisID)
        {
            string rValue = clsEnum.enuAlarm.Error_Code_Undifine.ToString("d");
            clsEnum.enuAlarm eMotionAlarm = (clsEnum.enuAlarm)Enum.ToObject(typeof(clsEnum.enuAlarm),
                (int)clsEnum.enuAlarm.Motion_Timeout_MagazineArmZ + (int)eAxisID);
            if (Enum.IsDefined(typeof(clsEnum.enuAlarm), eMotionAlarm) == true)
            {
                rValue = eMotionAlarm.ToString("d");
            }
            return rValue;
        }

        #endregion

    }
}

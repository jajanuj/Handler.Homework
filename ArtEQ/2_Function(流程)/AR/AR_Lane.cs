using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtCommunication;
using ArtProcModuleLib;
using ArtModuleData;

namespace ArtEQ
{
    public class AR_LaneChange : clsThreadProc
    {
        #region //=====================  區域變數設置 =====================

        public AutoProcess AP_LaneChange = null;
        public PM_Lane PM_Lane_Load = null;
        public PM_Lane PM_Lane_LoadBack = null;
        public PM_SMEMA PM_SMEMA_Load_Port1 = null;
        public PM_SMEMA PM_SMEMA_Load_Port2 = null;
        public PM_MgzArm PM_MgzArm_Up = null;
        public PM_MgzArm PM_MgzArm_Middle = null;
        public PM_MgzArm PM_MgzArm_Down = null;
        #endregion

        #region //=====================  全域變數設置 =====================

        public bool g_IsProcessing = false;

        /// <summary> 動作是否完成</summary>
        public bool bIsReady
        {
            get;
            protected set;
        }

        /// <summary> 是否完成Initial</summary>
        public bool bIsInitialized
        {
            get;
            protected set;
        }
        public enuAction m_eAction
        {
            get;
            protected set;
        }
        #endregion

        #region //========== Eunm 宣告 ==========

        public enum enuAction
        {
            Non,

            #region//===== Initial (100001) =====
            RunInitial = 100001,
            RunInitialDone,
            RunInitialFail,
            #endregion
        }

        #endregion

         #region //=====================  必要函式設置 =====================

        static private AR_LaneChange m_Singleton = null;
        static public AR_LaneChange GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new AR_LaneChange("Lane Change");
            }
            return m_Singleton;
        }


        public AR_LaneChange(string p_strLogName)
            : base("AR-" + p_strLogName)
        {
            AP_LaneChange = new AutoProcess("AP-" + p_strLogName, 0);
            AP_LaneChange.InitialTimeOut(AutoProcess.enuModuleTimeOut.HandShakeTimeout, clsEnum.enuPmtName.Sys_Timeout_HandShank);
            AP_LaneChange.InitialDelay(AutoProcess.enuModuleDelay.SimulateMode_Delay, clsEnum.enuPmtName.Sys_Delay_AP_SimulateMode);

            PM_Lane_Load = PM.GetSingleton().GetPM_Lane(clsEnum.enuProcName.Lane_Change_Load);
            PM_Lane_LoadBack = PM.GetSingleton().GetPM_Lane(clsEnum.enuProcName.Lane_Change_LoadBack);
            PM_SMEMA_Load_Port1 = PM.GetSingleton().GetPM_SMEMA(clsEnum.enuProcName.SMEMA_Port1_UnloadOut);
            PM_SMEMA_Load_Port2 = PM.GetSingleton().GetPM_SMEMA(clsEnum.enuProcName.SMEMA_Port2_LoadIn);
            PM_MgzArm_Up = PM.GetSingleton().GetPM_MgzArm(clsEnum.enuProcName.MagazineArm_Up);
            PM_MgzArm_Middle = PM.GetSingleton().GetPM_MgzArm(clsEnum.enuProcName.MagazineArm_Middle);
            PM_MgzArm_Down = PM.GetSingleton().GetPM_MgzArm(clsEnum.enuProcName.MagazineArm_Down);
        }
        protected override void Scenario()
        {
            switch (iStepIndex)
            {
                #region =================== 前置動作(0)===================
                case 0:
                case 1:
                    bIsReady = false;
                    this.Restart();
                    clsLog.Log(clsEnum.enuLogName.ProcessLog, this.strThreadLogName + ", Action : " + m_eAction.ToString());
                    this.iStepIndex = (int)m_eAction;
                    break;

                #endregion

                #region =================== RunInitial(100001)===================
                case 100001:
                    //AP_LaneChange.m_dctAlarmMessage.Clear();
                    AP_LaneChange.RunInitial();
                    PM_Lane_Load.RunInitial();
                    PM_Lane_LoadBack.RunInitial();
                    PM_SMEMA_Load_Port1.RunInitial();
                    PM_SMEMA_Load_Port2.RunInitial();
                    PM_MgzArm_Up.RunInitial();
                    PM_MgzArm_Middle.RunInitial();
                    PM_MgzArm_Down.RunInitial();
                    //PM_SMEMA_LoadBcak_Port2.RunInitial();
                    this.iStepIndex = 100101;
                    break;

                case 100101:
                    if (AP_LaneChange.IsProcOK() == true
                        && PM_Lane_Load.IsProcOK() == true
                        && PM_Lane_LoadBack.IsProcOK() == true
                        && PM_SMEMA_Load_Port1.IsProcOK() == true
                        && PM_SMEMA_Load_Port2.IsProcOK() == true
                        && PM_MgzArm_Up.IsProcOK() == true
                        && PM_MgzArm_Middle.IsProcOK() == true
                        && PM_MgzArm_Down.IsProcOK() == true
                        //&& PM_SMEMA_LoadBcak_Port2.IsProcOK() == true
                        )
                    {
                        this.iStepIndex = 100999;
                    }
                    break;

                case -100999:
                    clsEditRunThread.EqStop();
                    this.m_eAction = enuAction.RunInitialFail;
                    this.iStepIndex = 999;
                    break;

                case 100999:
                    this.m_eAction = enuAction.RunInitialDone;
                    this.iStepIndex = 999;
                    break;
                #endregion

                #region =================== 結束動作(-)===================

                case 999://動作完成
                    clsLog.Log(clsEnum.enuLogName.ProcessLog.ToString(), this.strThreadLogName + ", Action : " + m_eAction.ToString());
                    bIsReady = true;
                    iStepIndex = -1;
                    break;

                default:
                    iStepIndex = -1;
                    Stop();
                    bIsProcessing = false;
                    break;

                #endregion
            }
        }

        #endregion

        #region //===================== private 函式設置 =====================

        /// <summary> 是否動作完成(流程) </summary>
        private bool IsProc_ProcessOK(clsBaseProc p_Module)
        {
            return p_Module.IsProcOK();
        }

        /// <summary> 動作執行(流程) </summary>
        private void Proc_Action(clsBaseProc p_Source, clsBaseProc p_Target)
        {
            AP_LaneChange.RunAction(p_Source, p_Target);
        }
        /// <summary> 等待動作完成 </summary>
        private void Proc_GetState(ref int now, int Finsh, int Fail)
        {
            AP_LaneChange.GetState(ref now, Finsh, Fail);
        }

        private void ClearARData()
        {

        }
        #endregion

        #region //===================== Public Function (必要) =====================
        /// <summary> 判斷Process 是否已經完成  </summary>
        public bool IsProcOK()
        {
            return !this.bIsProcessing && this.bIsReady;
        }

        public bool IsProcStop()
        {
            if (IsProcOK() == true
                || this.iStepIndex == -1)
            {
                return true;
            }
            else if (this.iStepIndex > 0)
            {
                if (clsThreadProcManage.bIsStepProc == true
                    && this.iStepIndex % 100 == 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary> 執行初始化 </summary>
        public void Run_Initial()
        {
            if (AP_LaneChange != null)
            {
                iStepIndex = 1;
                bIsReady = false;
                bIsProcessing = true;
                m_eAction = enuAction.RunInitial;
            }
        }

        /// <summary> 執行動作 </summary>
        public void Run_Action(enuAction p_enuAction)
        {
            if (p_enuAction == enuAction.RunInitial)
            {
                Run_Initial();
            }
            else if (IsProcOK())
            {
                if (AP_LaneChange != null)
                {
                    iStepIndex = 1;
                    m_eAction = p_enuAction;
                    bIsReady = false;
                    bIsProcessing = true;
                    this.bIsKeepProc = true;
                }
            }
        }

        public void CancelAction()
        {
            this.iStepIndex = -1;
            this.bIsReady = true;
            this.bIsProcessing = false;
            this.m_eAction = enuAction.Non;
            clsLog.Log(clsEnum.enuLogName.ProcessLog.ToString(), this.strThreadLogName + "[Cancel Action] : " + m_eAction.ToString());
        }
        #endregion
    }
}

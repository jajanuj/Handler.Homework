using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtProcModuleLib;
using ArtModuleData;
using System.Drawing;

namespace ArtEQ
{
    public class Proc_ProcessArm : clsThreadProc
    {
        #region //=====================  區域變數設置 =====================
        Point mUnit = new Point();
        #endregion

        #region //=====================  全域變數設置 =====================


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

        /// <summary> 流程狀態 </summary>
        public enuAction m_eAction
        {
            get;
            protected set;
        }

        private int m_iIndex = 0;
        public int g_iIndex
        {
            get { return m_iIndex; }
            set { m_iIndex = value; }
        }
        #endregion

        #region //========== Eunm 宣告 ==========

        public enum enuAction
        {
            Non,

            #region//===== Initial (100001) =====
            RunInitial = 100001,
            RunInitial_Done,
            RunInitial_Fail,
            #endregion

            #region//===== RunAuto (200001) =====
            DoProcess = 200001,
            DoProcess_Done,
            DoProcess_Fail,
            #endregion
        }

        #endregion

        #region //=====================  必要函式設置 =====================

        static private object objLock = new object();
        static private Proc_ProcessArm m_Singleton = null;
        static public Proc_ProcessArm GetSingleton()
        {
            lock (objLock)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new Proc_ProcessArm("Proc_ProcessArm");
                }
            }
            return m_Singleton;
        }

        public Proc_ProcessArm(string p_strLogName)
            : base(p_strLogName)
        {

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
                    clsLog.Log(clsEnum.enuLogName.ProcessLog, this.strThreadLogName + ", Action : " + m_eAction.ToString() + ", UnitIndex : (" + mUnit.X + "," + mUnit.Y + ")");
                    this.iStepIndex = (int)m_eAction;
                    break;

                #endregion


                #region =================== RunInitial(100001)===================

                case 100001:
                    #region//前置判斷與設定
                    this.iStepIndex = 100101;
                    break;
                    #endregion

                case 100101:
                    #region//動作流程
                    this.Restart();
                    this.iStepIndex = 100111;
                    break;

                case 100111:
                    if(IsTimeOut(1000, clsCmData.enuSecUnit.MilliSec))
                    {
                        this.iStepIndex = 100999;
                    }
                    break;
                    #endregion


                case -100999:
                    #region//流程失敗
                    this.m_eAction = enuAction.RunInitial_Fail;
                    this.iStepIndex = 999;
                    #endregion
                    break;

                case 100999:
                    #region//流程成功
                    this.m_eAction = enuAction.RunInitial_Done;
                    this.iStepIndex = 999;
                    #endregion
                    break;

                #endregion

                #region =================== DoProcess(200001)===================

                case 200001:
                    #region//前置判斷與設定
                    if (PM.GetSingleton().GetPM_Lane(clsEnum.enuProcName.PM_Lane).GetBoatData().bExist == false)
                    {
                        clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.ProcArm_LaneDataNotExist, false, null);
                        this.iStepIndex = -200999;
                    }
                    if (this.iStepIndex == 200001)
                    {
                        this.iStepIndex = 200101;
                    }
                    break;
                    #endregion

                case 200101:
                    #region//動作流程
                    this.Restart();
                    this.iStepIndex = 200111;
                    break;

                case 200111:
                    if (IsTimeOut(1000, clsCmData.enuSecUnit.MilliSec))
                    {
                        PM.GetSingleton().GetPM_Lane(clsEnum.enuProcName.PM_Lane).GetBoatData().Get_UnitInfo(mUnit).enuData_ProductState = clsInfoBase.clsUnitInfo.enuUnitState.OK;
                        this.iStepIndex = 200999;
                    }
                    break;
                    #endregion


                case -200999:
                    #region//流程失敗
                    this.m_eAction = enuAction.DoProcess_Fail;
                    this.iStepIndex = 999;
                    #endregion
                    break;

                case 200999:
                    #region//流程成功
                    this.m_eAction = enuAction.DoProcess_Done;
                    this.iStepIndex = 999;
                    #endregion
                    break;

                #endregion

                #region =================== 結束動作(999)===================

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
      
        #endregion

        #region //===================== Public Function (必要) =====================

        /// <summary> 判斷Process 是否已經完成  </summary>
        public bool IsProcOK()
        {
            return !this.bIsProcessing && this.bIsReady;
        }

        /// <summary> 判斷流程是否停止運行，流程閒置或流程停留在百號</summary>
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
            iStepIndex = 1;
            bIsReady = false;
            bIsProcessing = true;
            m_eAction = enuAction.RunInitial;
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
                iStepIndex = 1;
                m_eAction = p_enuAction;
                bIsReady = false;
                bIsProcessing = true;
                this.bIsKeepProc = true;
            }
        }

        /// <summary> 執行動作 </summary>
        public void Run_Action(enuAction p_enuAction, int p_iIndex)
        {
            if (p_enuAction == enuAction.RunInitial)
            {
                Run_Initial();
            }
            else if (IsProcOK())
            {
                g_iIndex = p_iIndex;
                iStepIndex = 1;
                m_eAction = p_enuAction;
                bIsReady = false;
                bIsProcessing = true;
                this.bIsKeepProc = true;
            }
        }
        public void Run_Action(enuAction p_enuAction, Point p_Unit)
        {
            if (p_enuAction == enuAction.RunInitial)
            {
                Run_Initial();
            }
            else if (IsProcOK())
            {
                mUnit.X = p_Unit.X;
                mUnit.Y = p_Unit.Y;
                iStepIndex = 1;
                m_eAction = p_enuAction;
                bIsReady = false;
                bIsProcessing = true;
                this.bIsKeepProc = true;
            }
        }

        #endregion
    }
}

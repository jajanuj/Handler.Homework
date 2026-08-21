using System.Collections.Generic;
using System.Drawing;
using ArtCommonLib;
using ArtData;

namespace ArtEQ
{
    public class AR_Lane : clsThreadProc
    {
        #region //=====================  區域變數設置 =====================

        #endregion

        #region //=====================  全域變數設置 =====================

        /// <summary> 動作是否完成</summary>
        public bool bIsReady
        {
            get;
            protected set;
        }

        public Point mUnitIndex = new Point();//都要先建立初始值
        #endregion

        #region //=====================  必要函式設置 =====================

        static private object objLock = new object();
        static private AR_Lane m_Singleton = null;
        static public AR_Lane GetSingleton()
        {
            lock (objLock)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new AR_Lane("AR_Lane");
                }
            }
            return m_Singleton;
        }

        public AR_Lane(string p_strLogName)
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
                    clsLog.Log(clsEnum.enuLogName.ProcessLog, this.strThreadLogName + ", AutoRun - Start");
                    this.iStepIndex = 100000;
                    break;

                #endregion


                #region =================== Idle閒置(100000)===================

                case 100000:
                    if (bIsAllProcOK() == true)
                    {
                        if (PublicDeclare.GetData_Boat(clsEnum.enuProcName.PM_Lane).bExist == false)
                        {
                            this.iStepIndex = 200000;//Load
                        }
                        else if (PublicDeclare.GetData_Boat(clsEnum.enuProcName.PM_Lane).bExist == true
                            && PublicDeclare.GetData_Boat(clsEnum.enuProcName.PM_Lane).bWorkDone == false)
                        {
                            this.iStepIndex = 300000;//Work
                        }
                        else if (PublicDeclare.GetData_Boat(clsEnum.enuProcName.PM_Lane).bExist == true
                            && PublicDeclare.GetData_Boat(clsEnum.enuProcName.PM_Lane).bWorkDone == true)
                        {
                            this.iStepIndex = 400000;//Unload
                        }
                    }
                    break;

                #endregion


                #region =================== Load (200000)===================

                case 200000:
                    if (bIsAllProcOK() == true)
                    {
                        this.iStepIndex = 201000;
                    }
                    break;
                case 201000:
                    if (bIsAllProcOK() == true)
                    {
                        Run_AP(clsEnum.enuProcName.PM_SMEMA_Load, clsEnum.enuProcName.PM_Lane);
                        this.iStepIndex = 202000;
                    }
                    break;
                case 202000:
                    if (bIsAllProcOK() == true)
                    {
                        this.iStepIndex = 100000;//成功
                    }
                    break;

                #endregion

                #region =================== Work (300000)===================

                case 300000:
                    if (bIsAllProcOK() == true)
                    {
                        if (CountNeedWorkUnit(ref mUnitIndex) == true)
                        {
                            PublicDeclare.GetData_Boat(clsEnum.enuProcName.PM_Lane).bWorkDone = false;
                            this.iStepIndex = 301000;
                        }
                        else
                        {
                            PublicDeclare.GetData_Boat(clsEnum.enuProcName.PM_Lane).bWorkDone = true;
                            this.iStepIndex = 100000;//全部完成作業
                        }
                    }
                    break;
                case 301000:
                    if (bIsAllProcOK() == true)
                    {
                        this.iStepIndex = 302000;
                    }
                    break;
                case 302000:
                    if (bIsAllProcOK() == true)
                    {
                        this.iStepIndex = 300000;//成功

                    }
                    break;

                #endregion

                #region =================== Unload (400000)===================

                case 400000:
                    if (bIsAllProcOK() == true)
                    {
                        this.iStepIndex = 401000;
                    }
                    break;
                case 401000:
                    if (bIsAllProcOK() == true)
                    {
                        Run_AP(clsEnum.enuProcName.PM_Lane, clsEnum.enuProcName.PM_SMEMA_Unload);
                        this.iStepIndex = 402000;
                    }
                    break;
                case 402000:
                    if (bIsAllProcOK() == true)
                    {
                        this.iStepIndex = 100000;//成功

                    }
                    break;

                #endregion

                #region =================== 結束動作(-)===================

                case 999://動作完成
                    clsLog.Log(clsEnum.enuLogName.ProcessLog, this.strThreadLogName + ", AutoRun - End");
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

        private bool bIsAllProcOK()
        {
            bool rValue = true;
            //todo: 修改
            return rValue;
        }
        private void Run_AP(clsEnum.enuProcName p_Source, clsEnum.enuProcName p_Target)
        {
            //todo: 修改
            //PM.GetSingleton().GetAP(clsEnum.enuProcName.AP_Lane).RunAction(
            //    PM.GetSingleton().GetPM(p_Source), PM.GetSingleton().GetPM(p_Target));
        }
        private bool CountNeedWorkUnit(ref Point r_Point)//計算Func內,不能隨意變更Data內容
        {
            bool rValue = false;
            List<Point> Lst_Unit = new List<Point>();
            //rValue = PM.GetSingleton().GetPM_Lane(clsEnum.enuProcName.PM_Lane).IsUnitCanWork(ref Lst_Unit, AutoProcess.enuUnitNeedWorkState.NotDetected);
            //if (Lst_Unit.Count > 0 && rValue == true)
            //{
            //    r_Point = Lst_Unit[0];
            //}
            //else
            //{
            //    rValue = false;
            //}

            //clsInfoBase.clsBoatInfo p_BoatInfo = PublicDeclare.GetData_Boat(clsEnum.enuProcName.PM_Lane);
            //foreach (Point p_Unit in p_BoatInfo.DctUnitData.Keys)
            //{
            //    if (p_BoatInfo.DctUnitData[p_Unit].bExist == true
            //        && p_BoatInfo.DctUnitData[p_Unit].enuData_ProductState == clsInfoBase.clsUnitInfo.enuUnitState.NotDetected)
            //    {
            //        r_Point.X = p_Unit.X;
            //        r_Point.Y = p_Unit.Y;
            //        rValue = true;
            //    }
            //}
            return rValue;
        }
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
            iStepIndex = -1;
            bIsReady = true;
            bIsProcessing = false;
        }

        /// <summary> 執行動作 </summary>
        public void Run_Action()
        {
            if (IsProcOK())
            {
                iStepIndex = 1;
                bIsReady = false;
                bIsProcessing = true;
                this.bIsKeepProc = true;
            }
        }

        #endregion
    }
}

using ArtCommonLib;
using ArtData;
using ArtEQ._2_Function_流程_.Proc;
using System.Linq;

namespace ArtEQ._2_Function_流程_.AutoRun
{
    internal class AR_Mag_NG_Feed : clsThreadProc
    {
        #region Constructors

        #region //===================== 建構子 =====================

        public AR_Mag_NG_Feed(string p_strLogName) : base(p_strLogName)
        {
        }

        #endregion

        #endregion

        #region Properties

        #region //===================== 全域變數 =====================

        public bool bIsReady { get; protected set; }

        #endregion

        #endregion

        #region Protected Methods

        #region //===================== Scenario =====================

        protected override void Scenario()
        {
            switch (iStepIndex)
            {
                #region //============== 前置作業 ==============

                case 1:
                    bIsReady = false;
                    Restart();
                    clsLog.Log(clsEnum.enuLogName.ProcessLog, strThreadLogName + ", AutoRun - Start");
                    iStepIndex = 100000;
                    break;

                #endregion

                #region //============== 閒置判別 ==============

                case 100000:
                    if (CanLoad())
                    {
                        iStepIndex = 200000;
                    }

                    break;

                #endregion

                #region //============== Load ==============

                case 200000:
                    if (Mag_NG_Feed().IsProcOK())
                    {
                        int slotNo = CheckNextSlotNo();
                        if (slotNo < 0)
                        {
                            clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), strThreadLogName + " : 無有效 Slot，回閒置");
                            iStepIndex = 100000;
                            break;
                        }

                        Mag_NG_Feed().RunLoad(slotNo);
                        clsLog.Log(clsEnum.enuLogName.ProcessLog, $"{strThreadLogName} : RunLoad Slot[{slotNo}]");
                        iStepIndex = 201000;
                    }

                    break;

                case 201000:
                    if (Mag_NG_Feed().IsProcOK()) break;
                    if (Mag_NG_Feed().m_enuAction == BaseMagazine.enuAction.Magazine_Load_Done)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog),
                            $"{strThreadLogName} : {Mag_NG_Feed().m_enuAction.ToString()} : Load Done → 回閒置等處理");
                        iStepIndex = 100000;
                        if (CheckNextSlotNo() < 0)
                        {
                            // TODO 料盒內無料
                        }
                    }
                    else if (Mag_NG_Feed().m_enuAction == BaseMagazine.enuAction.Magazine_Load_Fail)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog),
                            $"{strThreadLogName} : {Mag_NG_Feed().m_enuAction.ToString()} : Load Fail → 回閒置等處理");
                        iStepIndex = 100000;
                        if (CheckNextSlotNo() < 0)
                        {
                            clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.Need_Magazine_To_Load, NeedEqStop: false);
                        }
                    }

                    break;

                #endregion

                default:
                    iStepIndex = -1;
                    Stop();
                    bIsProcessing = false;
                    break;
            }
        }

        #endregion

        #endregion

        #region //===================== Singleton =====================

        private static AR_Mag_NG_Feed m_Singleton;
        private static object m_objLock = new object();

        public static AR_Mag_NG_Feed GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new AR_Mag_NG_Feed("AR_Mag_NG_Feed");
                    }
                }
            }

            return m_Singleton;
        }

        #endregion

        #region //===================== 條件判斷 =====================

        /// <summary>
        /// 判斷是否可以執行 Load
        /// </summary>
        private bool CanLoad()
        {
            bool rValue = true;

            // 1. Magazine 有帳料且尚未結束
            rValue &= Mag_NG_Feed().m_MagazineInfo.bIsExist;

            // 2. 還有 Slot 有料可推
            rValue &= CheckNextSlotNo() >= 0;

            // 3. 下游 Load_Lane 流程就緒
            rValue &= NextLane().m_enuAction == BaseLane.enuAction.Load_Done ||
                      NextLane().m_enuAction == BaseLane.enuAction.Unload_Done ||
                      NextLane().m_enuAction == BaseLane.enuAction.Initial_Done;

            // 4. 下游 Load_Lane 無 Boat、無帳料、狀態為 Lane_Loading 等待中
            rValue &= !NextLane().m_Temp_Tray_Info.bIsExist;

            // 5. 結批：正常運轉一律放行。結批期間不能整批停料——NG_Feed 供應的是「空載具盤」，
            //    不是新的生產原料；只要 OK_Lane 或更上游(HS/ASM/Press/AOI)還有料在流，代表
            //    之後還可能分出新的 NG，NG_Lane 需要有空盤接，Sort_Arm 才有地方放。等這些
            //    上游全部流空了，才代表不會再有新的 NG，這時才真的不需要再補空盤。
            rValue &= !ProcAutoRun.bIsLotEnd || ProcAutoRun.HasUpstreamWorkPendingSort();
            return rValue;
        }

        /// <summary>
        /// 找到下一個有料的 Slot
        /// </summary>
        private int CheckNextSlotNo()
        {
            var nextSlot = Mag_NG_Feed().m_MagazineInfo.m_trayInfo
                .Where(kv => kv.Value.bIsExist)
                .OrderBy(kv => kv.Key)
                .FirstOrDefault();

            return nextSlot.Value != null ? nextSlot.Key : -1;
        }

        #endregion

        #region //===================== Private Helper =====================

        private Proc_NG_Feed_Magazine Mag_NG_Feed() => Proc_NG_Feed_Magazine.GetSingleton();
        private Proc_NG_Lane NextLane() => Proc_NG_Lane.GetSingleton();

        #endregion

        #region //===================== Public 函式 =====================

        public bool IsProcOK() => !bIsProcessing && bIsReady;

        public void RunInitial()
        {
            iStepIndex = -1;
            bIsReady = true;
            clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), strThreadLogName + " : RunInitial Done");
        }

        public void Run_AutoRun()
        {
            if (IsProcOK() && clsCmData.g_bIsinitialized)
            {
                iStepIndex = 1;
                bIsProcessing = true;
            }
        }

        public bool IsIdle()
        {
            return Mag_NG_Feed().IsProcOK();
        }

        #endregion
    }
}

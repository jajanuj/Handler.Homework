using ArtCommonLib;
using ArtData;
using ArtEQ._2_Function_流程_.Proc;
using System.Linq;

namespace ArtEQ._2_Function_流程_.AutoRun
{
    /// <summary>
    /// 驅動 OK Discharge Magazine 自動運轉：等 OK Lane 出料就緒後，收料進 Magazine 空 Slot。
    /// </summary>
    internal class AR_Mag_OK_Discharge : clsThreadProc
    {
        #region Constructors

        #region //===================== 建構子 =====================

        public AR_Mag_OK_Discharge(string p_strLogName) : base(p_strLogName)
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
                    if (CanUnload())
                    {
                        iStepIndex = 200000;
                    }

                    break;

                #endregion

                #region //============== Unload（收料進 Magazine） ==============

                case 200000:
                    if (Mag_OK_Discharge().IsProcOK())
                    {
                        int slotNo = CheckNextEmptySlotNo();
                        if (slotNo < 0)
                        {
                            clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), strThreadLogName + " : 無空 Slot 可收料，回閒置");
                            iStepIndex = 100000;
                            break;
                        }

                        Mag_OK_Discharge().RunUnload(slotNo);
                        clsLog.Log(clsEnum.enuLogName.ProcessLog, $"{strThreadLogName} : RunUnload Slot[{slotNo}]");
                        iStepIndex = 201000;
                    }

                    break;

                case 201000:
                    if (!Mag_OK_Discharge().IsProcOK()) break;
                    if (Mag_OK_Discharge().m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Done)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog),
                            $"{strThreadLogName} : {Mag_OK_Discharge().m_enuAction.ToString()} : Unload Done → 回閒置等處理");
                        iStepIndex = 100000;
                        if (CheckNextEmptySlotNo() < 0)
                        {
                            //clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.Empty_Magazine, NeedEqStop: false);
                        }
                    }
                    else if (Mag_OK_Discharge().m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Fail)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog),
                            $"{strThreadLogName} : {Mag_OK_Discharge().m_enuAction.ToString()} : Unload Fail → 回閒置等處理");
                        iStepIndex = 100000;
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

        private static AR_Mag_OK_Discharge m_Singleton;
        private static object m_objLock = new object();

        public static AR_Mag_OK_Discharge GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new AR_Mag_OK_Discharge("AR_Mag_OK_Discharge");
                    }
                }
            }

            return m_Singleton;
        }

        #endregion

        #region //===================== 條件判斷 =====================

        /// <summary>
        /// 判斷是否可以執行 Unload（收料）
        /// </summary>
        private bool CanUnload()
        {
            bool rValue = true;

            // 1. Magazine 本體有帳(空料盒已上機)且尚未結束
            rValue &= Mag_OK_Discharge().m_MagazineInfo.bIsExist;

            // 2. 還有空 Slot 可以收料
            rValue &= CheckNextEmptySlotNo() >= 0;

            // 3. 上游 OK Lane 已經出料到位，等待被收走
            // 注意：用 Unload_Waiting（case 60200，Lane 卡著等下游準備好的早期狀態），
            // 不能用 Unload_Waiting_Sign（case 60500，料已送出只等下游 ACK 的晚期狀態）。
            // Proc_OK_Lane.ReadyToUnloadToNext() 要 Magazine 先進 Magazine_Unload_Waiting 才放行過 60200，
            // Magazine 要靠這裡呼叫 RunUnload() 才會進 Magazine_Unload_Waiting，
            // 等 Unload_Waiting_Sign 的話 Lane 永遠到不了，會互相卡死。詳見 LESSONS.md。
            rValue &= OK_Lane().m_enuAction == BaseLane.enuAction.Unload_Waiting;

            // 4. Magazine 流程就緒
            rValue &= Mag_OK_Discharge().IsProcOK();

            // 5. Magazine 狀態確認
            rValue &= Mag_OK_Discharge().m_enuAction == BaseMagazine.enuAction.Initial_Done ||
                      Mag_OK_Discharge().m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Done;

            return rValue;
        }

        /// <summary>
        /// 找到下一個空的 Slot（收料方向：找沒有帳的槽位）
        /// </summary>
        private int CheckNextEmptySlotNo()
        {
            var nextSlot = Mag_OK_Discharge().m_MagazineInfo.m_trayInfo
                .Where(kv => !kv.Value.bIsExist)
                .OrderBy(kv => kv.Key)
                .FirstOrDefault();

            return nextSlot.Value != null ? nextSlot.Key : -1;
        }

        #endregion

        #region //===================== Private Helper =====================

        private Proc_OK_Discharge_Magazine Mag_OK_Discharge() => Proc_OK_Discharge_Magazine.GetSingleton();
        private Proc_OK_Lane OK_Lane() => Proc_OK_Lane.GetSingleton();

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
            return Mag_OK_Discharge().IsProcOK();
        }

        #endregion
    }
}

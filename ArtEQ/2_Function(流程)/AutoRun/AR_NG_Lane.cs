using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtEQ._2_Function_流程_.Proc;
using System.Linq;

namespace ArtEQ._2_Function_流程_.AutoRun
{
    internal class AR_NG_Lane : clsThreadProc
    {
        #region Constructors

        #region //===================== 建構子 =====================

        public AR_NG_Lane(string p_strLogName) : base(p_strLogName)
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
                    if (!NG_Lane().IsProcOK()) break;

                    if (CanLoad())
                    {
                        iStepIndex = 200000;
                    }
                    else if (CanUnload())
                    {
                        iStepIndex = 300000;
                    }

                    break;

                #endregion

                #region //============== Load（等 NG Feed Magazine 推料） ==============

                case 200000:

                    #region 可否執行

                    if (!NG_Lane().IsProcOK()) break;

                    if (CanLoad())
                    {
                        iStepIndex = 200100;
                    }

                    #endregion

                    break;

                case 200100:

                    #region 執行

                    if (!NG_Lane().IsProcOK()) break;

                    NG_Lane().RunLoad();
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : RunLoad");
                    iStepIndex = 200200;

                    #endregion

                    break;

                case 200200:

                    #region 執行結果

                    if (!NG_Lane().IsProcOK()) break;

                    if (NG_Lane().m_enuAction == BaseLane.enuAction.Load_Done)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Load Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (NG_Lane().m_enuAction == BaseLane.enuAction.Load_Fail)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Load Fail → 回閒置等處理");
                        iStepIndex = 100000;
                    }

                    #endregion

                    break;

                #endregion

                #region //============== Unload (卸載至Magazine) ==============

                case 300000:

                    #region 可否執行

                    if (!NG_Lane().IsProcOK()) break;
                    if (CanUnload())
                    {
                        iStepIndex = 300100;
                    }

                    #endregion

                    break;

                case 300100:

                    #region 執行

                    NG_Lane().RunUnload();
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : RunUnload");
                    iStepIndex = 300200;

                    #endregion

                    break;

                case 300200:

                    #region 執行結果

                    if (NG_Lane().m_enuAction == BaseLane.enuAction.Unload_Done)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Unload Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (NG_Lane().m_enuAction == BaseLane.enuAction.Unload_Fail)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Unload Fail → 回閒置等處理");
                        iStepIndex = 100000;
                    }

                    #endregion

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

        private static AR_NG_Lane m_Singleton;
        private static object m_objLock = new object();

        public static AR_NG_Lane GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new AR_NG_Lane("AR_NG_Lane");
                    }
                }
            }

            return m_Singleton;
        }

        #endregion

        #region //===================== Private Helper =====================

        /// <summary>
        /// 本站(NG流道)
        /// </summary>
        /// <returns></returns>
        private Proc_NG_Lane NG_Lane() => Proc_NG_Lane.GetSingleton();

        /// <summary>
        /// 上游料盒(NG入料)
        /// </summary>
        /// <returns></returns>
        private Proc_NG_Feed_Magazine Mag_NG_Feed() => Proc_NG_Feed_Magazine.GetSingleton();

        /// <summary>
        /// 下游料盒(NG收料)
        /// </summary>
        /// <returns></returns>
        private Proc_NG_Discharge_Magazine Mag_NG_Discharge() => Proc_NG_Discharge_Magazine.GetSingleton();

        /// <summary>
        /// OK流道(即時出料模式要看它這輪分完了沒)
        /// </summary>
        private Proc_OK_Lane OK_Lane() => Proc_OK_Lane.GetSingleton();

        #endregion

        #region //===================== 條件判斷 =====================

        /// <summary>
        /// Step2: 確認可以載入
        /// 條件：Load位置無帳料 + Magazine已推出帳料
        /// </summary>
        private bool CanLoad()
        {
            bool rValue = true;

            // 1. 上游Magazine狀態確認
            rValue &= Mag_NG_Feed().m_enuAction == BaseMagazine.enuAction.Initial_Done ||
                      Mag_NG_Feed().m_enuAction == BaseMagazine.enuAction.Magazine_Load_Waiting;

            //2. Lane料帳確認
            rValue &= !NG_Lane().m_Temp_Tray_Info.bIsExist;

            //3. Lane是否流程就緒
            rValue &= NG_Lane().IsProcOK();

            //4. Lane狀態確認
            rValue &= NG_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      NG_Lane().m_enuAction == BaseLane.enuAction.Unload_Done;

            return rValue;
        }

        /// <summary>
        /// Step5: 確認可以卸載
        /// 條件：Load位置有帳料 + 下游 NG Discharge Magazine 準備收料
        /// </summary>
        private bool CanUnload()
        {
            bool rValue = true;

            // 1. 下游狀態確認
            rValue &= Mag_NG_Discharge().m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Waiting ||
                      Mag_NG_Discharge().m_enuAction == BaseMagazine.enuAction.Magazine_Unload_Done ||
                      Mag_NG_Discharge().m_enuAction == BaseMagazine.enuAction.Initial_Done;

            //2. Lane料帳確認
            rValue &= NG_Lane().m_Temp_Tray_Info.bIsExist;

            //3. Lane是否流程就緒
            rValue &= NG_Lane().IsProcOK();

            //4. Lane狀態確認
            rValue &= NG_Lane().m_enuAction == BaseLane.enuAction.Initial_Done ||
                      NG_Lane().m_enuAction == BaseLane.enuAction.Load_Done;

            if (!rValue)
                return false;

            //5. 依 NG 出料模式，判斷這盤 NG 夠不夠出料
            if (ProcAutoRun.bIsLotEnd && !ProcAutoRun.HasUpstreamWorkPendingSort())
            {
                // 強制結批：只有在「上游(HS/ASM/Press/AOI/OK Lane)已經完全流空、不會再有新的 NG
                // 進來」時才啟動——不管平常設定哪種模式，這盤不管滿不滿(甚至完全是空的載具盤)
                // 都要讓它出得去，只需要確認 Sort_Arm 目前沒有正在半路上的 Pick/Place(bIsSortDone)，
                // 不然可能夾著最後一顆料還沒放進去。不然結批後這盤會卡死 ProcAutoRun case 2000
                // 的淨空偵測，永遠停不了機。
                //
                // 注意：這裡的條件是「上游已經流空」，不是單純「bIsSortDone==true」——bIsSortDone
                // 在「目前沒有 NG 在等搬」時就是 true，這個狀態在上游還有料時也會出現(例如連續
                // 好幾輪 OK_Lane 都是全 OK)。如果結批期間只看 bIsSortDone 就無條件出料，會把
                // AR_Mag_NG_Feed 剛補的空盤立刻又推出去，兩邊邏輯打架，變成空盤瘋狂進出的迴圈
                // (實測發生過)。也不能只看「這盤有沒有東西」就對 FullTray 模式提早出料——上游
                // 還有料時，FullTray 該等的還是要等，不能因為結批就把還沒滿的盤子提早推出去
                // (也實測發生過：兩輪 OK_Lane 才送完，第一盤 NG 沒滿就被出料了)。
                rValue &= AR_Sort_Arm.GetSingleton().bIsSortDone;
            }
            else
            {
                // 正常運轉，或結批但上游還有料在跑：兩種模式維持原本各自的判斷邏輯，
                // 結批不改變這裡的行為，只是等上游流空後才會進到上面那個強制出料分支。
                switch (GetNGDischargeMode())
                {
                    case clsEnum.NGDischargeMode.PerCycle:
                        // 這一輪(對應目前 OK_Lane 那盤)分料已經做完，而且這輪真的有收到東西才出，
                        // 避免 OK_Lane 那盤剛好整盤都是 OK、NG_Lane 這輪沒收到任何一顆卻白跑一次出料。
                        rValue &= AR_Sort_Arm.GetSingleton().bIsSortDone;
                        rValue &= NG_Lane().m_Temp_Tray_Info.AssyRecords.Any(v => v.IsExist);
                        break;

                    case clsEnum.NGDischargeMode.FullTray:
                        // NG_Lane 自己收滿整盤(每一格都有帳)才出料
                        rValue &= NG_Lane().m_Temp_Tray_Info.AssyRecords.All(v => v.IsExist);
                        break;

                    default:
                        rValue = false;
                        break;
                }
            }

            return rValue;
        }

        /// <summary>
        /// 讀取分料模式
        /// </summary>
        private clsEnum.NGDischargeMode GetNGDischargeMode()
        {
            return (clsEnum.NGDischargeMode)ucParameter.GetValueInt(clsEnum.enuPmtName.Rec_Sort_Type);
        }

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

        #endregion
    }
}
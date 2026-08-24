using System.Linq;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtEQ._2_Function_流程_.BaseProc;
using ArtEQ._2_Function_流程_.Proc;
using static ArtData.clsEnum;

namespace ArtEQ._2_Function_流程_.AutoRun
{
    /// <summary>
    /// 驅動 Press Station 自動運轉：Press Lane 上有帳、到位、且還有格子沒壓合完成時，執行一次 RunPress()。
    /// </summary>
    internal class AR_Press_Station : clsThreadProc
    {
        #region Constructors

        #region //===================== 建構子 =====================

        public AR_Press_Station(string p_strLogName) : base(p_strLogName)
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
                    if (!Station().IsProcOK()) break;

                    if (CanPress())
                    {
                        iStepIndex = 200000;
                    }

                    break;

                #endregion


                #region //============== 壓合 ==============

                case 200000:
                    if (!Station().IsProcOK()) break;

                    Station().RunPress();
                    clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : RunPress");
                    iStepIndex = 200100;
                    break;

                case 200100:
                    if (!Station().IsProcOK()) break;

                    if (Station().m_enuAction == BasePressStation.enuAction.Press_Done)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Press Done → 回閒置等處理");
                        iStepIndex = 100000;
                    }
                    else if (Station().m_enuAction == BasePressStation.enuAction.Press_Fail)
                    {
                        clsLog.Log(nameof(clsEnum.enuLogName.ProcessLog), $"{strThreadLogName} : Press Fail → 回閒置等處理");
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

        #region Private Methods

        #region //===================== 條件判斷 =====================

        /// <summary>
        /// 確認可以執行壓合
        /// 條件：Press Lane 有帳、到位、Station 就緒、且盤子裡還有格子有料但尚未壓合
        /// </summary>
        private bool CanPress()
        {
            bool rValue = true;

            // 1. Press Lane 帳料 + 到位訊號確認
            rValue &= Press_Lane().m_Temp_Tray_Info.bIsExist;
            rValue &= Press_Lane().ArrivalSignal;
            rValue &= Press_Lane().IsProcOK();

            // 2. Station 流程就緒
            rValue &= Station().IsProcOK();

            // 3. Station 狀態確認
            rValue &= Station().m_enuAction == BasePressStation.enuAction.Initial_Done ||
                      Station().m_enuAction == BasePressStation.enuAction.Press_Done ||
                      Station().m_enuAction == BasePressStation.enuAction.Press_Fail;

            if (!rValue)
                return false;

            // 4. 還有格子有料但尚未壓合(或被壓合站關閉放行)完成，才需要觸發；全部完成就不用再觸發。
            //    壓合站關閉時 SetTrayWork(false) 會把 IsPressSkipped 標成 true，一樣算完成，
            //    不需要另外查 Sys_EnablePressStation。
            return Press_Lane().m_Temp_Tray_Info.AssyRecords.Any(v => v.IsExist && !v.IsPressed && !v.IsPressSkipped);
        }

        #endregion

        #endregion

        #region //===================== Singleton =====================

        private static AR_Press_Station m_Singleton;
        private static object m_objLock = new object();

        public static AR_Press_Station GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new AR_Press_Station("AR_Press_Station");
                    }
                }
            }

            return m_Singleton;
        }

        #endregion

        #region //===================== Private Helper =====================

        private Proc_Press_Station Station() => Proc_Press_Station.GetSingleton();
        private Proc_Press_Lane Press_Lane() => Proc_Press_Lane.GetSingleton();

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
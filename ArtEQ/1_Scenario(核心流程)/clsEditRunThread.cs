using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtProcModuleLib;
using ArtSystem;
using ArtSystem.MultiSystem;
namespace ArtEQ
{
    /// <summary> 軟體核心流程 </summary>
    public class clsEditRunThread : ArtCommonLib.clsArtThread
    {
        #region //=====================  變數設置 =====================

        static private ProcInitial m_ProcInitial = new ProcInitial("ProcInitial");
        static private ProcAutoRun m_ProcAutoRun = new ProcAutoRun("ProcAutoRun");
        static private ProcDetectIO m_ProcDetectIO = new ProcDetectIO("ProcDetectIO");

        /// <summary>機台是否動作中</summary>
        static public bool IsRunning//幾乎沒在使用
        {
            get;
            private set;
        }

        /// <summary>設置主流程動作狀態</summary>
        static public bool RunThread
        {
            get;
            set;
        }

        /// <summary>設置Core 2流程動作狀態，需3核心CPU以上</summary>
        static public bool RunThreadCore2
        {
            get;
            set;
        }

        /// <summary>設置Core 3流程動作狀態，需4核心CPU以上</summary>
        static public bool RunThreadCore3
        {
            get;
            set;
        }

        #endregion

        #region //===================== Enum 宣告 =====================

        /// <summary> 運轉模式 </summary>
        public enum enuRunMode
        {
            /// <summary> 初始化流程 </summary>
            Initial,
            /// <summary> 自動流程 </summary>
            AutoRun,
            /// <summary> 單動流程 </summary>
            ManualRun,
        }

        #endregion

        #region //=====================  必要函式設置 =====================
        /// <summary> 建立Thread 並開始動作 </summary>
        static public void ThreadStart()
        {
            clsEditRunThread mRunThread = new clsEditRunThread();
            clsCmData.g_bIsRunThreadAlive = true;

            CreatProc();//把所有流程加入執行緒列表(Debug) 

            RunThread = true;
            mRunThread.start();

            //RunThreadCore2 = true;
            //mRunThread.startCore2();

            //RunThreadCore3 = true;
            //mRunThread.startCore3();

            m_ProcDetectIO.bIsProcessing = true;
        }

        /// <summary> 停止Thread 動作 </summary>
        static public void ThreadStop()
        {
            clsCmData.g_bIsRunThreadAlive = false;
        }

        #endregion

        #region //===================== public 函式設置  (Report Alarm) =====================

        /// <summary>  AP呼叫Alarm </summary>
        static public void ReportModuleAlarm(AutoProcess p_AP, bool p_NeedEqStop = true)
        {
            clsBaseProc m_Source = p_AP.m_Source;
            clsBaseProc m_Target = p_AP.m_Target;
            if (p_AP.m_DctAPAlarm.Count > 0)
            {
                //for (int i = 0; i < p_AP.m_DctAPAlarm.Count; i++)
                foreach (var item in p_AP.m_DctAPAlarm)
                {
                    int NameNum = (int)p_AP.APID;
                    //int ModuleAlarmCode = (int)p_AP.m_DctAPAlarm[i];
                    int ModuleAlarmCode = item.Key;
                    int AlarmCode = 100000 + NameNum * 1000 + ModuleAlarmCode;
                    //if (Enum.IsDefined(typeof(clsEnum.enuAlarm), AlarmCode))
                    if (ucAlarmManage.GetSingleton().GetAlarmInfo(AlarmCode.ToString()) != null)
                    {
                        string TS_Message = "Target : " + p_AP.m_Target.strThreadLogName + "\r\nSource: " + p_AP.m_Source.strThreadLogName;
                        clsEditRunThread.ReportAlarmString(AlarmCode.ToString(), p_NeedEqStop, null, TS_Message);
                    }
                    else
                    {
                        clsEditRunThread.ReportAlarmString(AlarmCode.ToString(), p_NeedEqStop, p_AP.Name.ToString() + " : " + item.Value);
                        //clsEditRunThread.ReportAlarmString(AlarmCode.ToString(), p_NeedEqStop, p_AP.Name.ToString() + " : " + p_AP.mLst_APAlarm[i].ToString());
                    }
                }
            }
            ReportProcAlarm(m_Source, p_NeedEqStop);
            ReportProcAlarm(m_Target, p_NeedEqStop);
        }
        /// <summary>  PM呼叫Alarm </summary>
        static public void ReportProcAlarm(clsBaseProc p_Proc, bool p_NeedEqStop = true)
        {
            if (p_Proc != null)
            {
                if (p_Proc.m_dctAlarmMessage.Count > 0)
                {
                    foreach (var item in p_Proc.m_dctAlarmMessage)
                    {
                        int NameNum = (int)p_Proc.m_ModuleID;
                        int ModuleAlarmCode = item.Key;
                        int AlarmCode = 100000 + NameNum * 1000 + ModuleAlarmCode;
                        if (ucAlarmManage.GetSingleton().GetAlarmInfo(AlarmCode.ToString()) != null)
                        {
                            clsEditRunThread.ReportAlarmString(AlarmCode.ToString(), p_NeedEqStop);
                        }
                        else
                        {
                            clsEditRunThread.ReportAlarmString(AlarmCode.ToString(), p_NeedEqStop, p_Proc.LocalModuleName.ToString() + " : " + item.Value);
                        }
                    }
                }
            }

        }

        static public void ReportAlarm(clsEnum.enuAlarm p_Alarm, clsEnum.enuDi AlarmNote, bool NeedEqStop = true)
        {
            List<clsEnum.enuDi> LstDIEnum = new List<clsEnum.enuDi>();
            if (AlarmNote != null)
            {
                LstDIEnum.Add((clsEnum.enuDi)AlarmNote);
            }
            ReportAlarm(p_Alarm, LstDIEnum, NeedEqStop);
        }
        static public void ReportAlarm(clsEnum.enuAlarm p_Alarm, List<clsEnum.enuDi> AlarmNote, bool NeedEqStop = true)
        {
            string strNote = "";
            for (int i = 0; i < AlarmNote.Count; i++)
            {
                clsEnum.enuDi eDINote = AlarmNote[i];
                if (i + 1 == AlarmNote.Count)
                {
                    strNote = "DI" + eDINote.ToString("D");
                }
                else
                {
                    strNote = "DI" + eDINote.ToString("D") + ", ";
                }
            }
            ReportAlarm(p_Alarm, NeedEqStop, strNote);
        }
        static public void ReportAlarm(clsEnum.enuAlarm p_Alarm, bool NeedEqStop = true, string AlarmNote = null, string p_strTroubleShootingNote = null)
        {
            int Code = (int)p_Alarm;
            if (ucAlarmManage.GetSingleton().GetAlarmInfo(Code.ToString()) != null)
            {
                ReportAlarmString(Code.ToString(), NeedEqStop, AlarmNote, p_strTroubleShootingNote);
            }
            else
            {
                ReportAlarmString(Code.ToString(), NeedEqStop, p_Alarm.ToString(), p_strTroubleShootingNote);
            }
        }
        static public void ReportAlarmString(string p_Alarm, bool NeedEqStop = true, string AlarmNote = null, string p_strTroubleShootingNote = null)
        {
            LinkedList<clsObjAlarm> temp = formAlarmReport.GetSingleton().lstAlarmData;
            foreach (clsObjAlarm alarm in temp)
            {
                if (alarm.AlarmCode == p_Alarm)
                {
                    return;
                }
            }
            if (NeedEqStop == true)
            {
                clsEditRunThread.EqStop();
            }
            #region//ReportAlarm前要先切換狀態 (這樣消除所有Alarm後才會顯示對應的狀態)
            if (clsCmData.g_bIsinitialized == true)
            {
                if (clsCmData.g_NowEqStatus == clsCmData.enuEqStatus.Run && NeedEqStop)
                {
                    clsCmData.g_NowEqStatus = clsCmData.enuEqStatus.Stop;
                }
            }
            else
            {
                clsCmData.g_NowEqStatus = clsCmData.enuEqStatus.Default;
            }
            #endregion
            formAlarmReport.ReportAlm(p_Alarm, null, AlarmNote, p_strTroubleShootingNote);


        }


        #endregion

        #region //===================== 運轉指令 =====================
        //ManualRun, AutoRun 的旗標說明
        //ProcAutoRun.bIsAutoRunMode    :標準來說,啟動AutoRunMode後,無法執行單動流程，如果需要切換成單動模式(請執行Initial)
        //ProcAutoRun.bIsManualMode     :標準來說,啟動ManualMode後,無法執行自動流程，如果需要切換自動模式(請執行Initial)
        //凡是有例外，如果設備需要AutoRun 與 ManualRun 隨意切換，請自行評估風險.

        /// <summary> AutoRun, ManualRun, Intial 動作前的確認 </summary>
        static public bool CheckCanRun(enuRunMode eRunMode)
        {
            bool NoAlarm = true;
            ucMotionSetting.SetAllSpeed();
            if (formAlarmReport.IsAlarmOccur() == true)
            {
                formAlarmReport.GetSingleton().Show();
                return false;
            }
            #region//Auto Run Mode Confirm
            if (eRunMode != enuRunMode.Initial
                && clsCmData.g_bIsinitialized == false)
            {
                formMessageBox.Show("Please Initial Before Do Any Action");
                return false;
            }
            else if (eRunMode == enuRunMode.AutoRun
                && ProcAutoRun.bIsManualMode == true)
            {
                formMessageBox.Show("Please Initial To Change Manual Mode To AutoRun Mode");
                NoAlarm = false;
            }
            else if (eRunMode == enuRunMode.ManualRun
                && ProcAutoRun.bIsAutoRunMode == true)
            {
                formMessageBox.Show("Please Initial To Change AutoRun Mode To Manual Mode");
                return false;
            }
            #endregion
            #region//確保所有所有馬達ServoOn (Initial不用判斷, ProcInitial會自行啟動所有馬達)
            if (eRunMode != enuRunMode.Initial)
            {
                if (ConfirmServoOn() == false)
                {
                    return false;
                }
            }
            #endregion
            #region//SafeDoor 安全門檢查
            if (ucParameter.GetValueBool(clsEnum.enuPmtName.Sys_EnableSafeDoor) == true)
            {
                clsDioCtrl.SetDo(clsEnum.enuDo.Safety_Door_Lock, true);
                clsDioCtrl.RefreshDoData();//強制刷新輸出給DO卡.
                if (clsDioCtrl.GetDi(clsEnum.enuDi.SafeDoor_B) == false)
                {
                    #region//@1.0.0.12-8@ (安全門鎖檢查Error)
                    int Delay = 500;
                    while (Delay > 0)
                    {
                        clsDioCtrl.RefreshDiData();
                        Application.DoEvents();
                        Thread.Sleep(1);
                        Delay--;
                        if (clsDioCtrl.GetDi(clsEnum.enuDi.SafeDoor_B) == true)
                        {
                            break;
                        }
                    }
                    if (clsDioCtrl.GetDi(clsEnum.enuDi.SafeDoor_B) == false)
                    {
                        clsEditRunThread.ReportAlarm(clsEnum.enuAlarm.Machine_Error_SafeDoor_Open, clsEnum.enuDi.SafeDoor_B);
                        return false;
                    }
                    #endregion
                }
            }
            #endregion
            return NoAlarm;
        }
        /// <summary> 確保所有馬達都是ServoOn的狀態 </summary>
        static public bool ConfirmServoOn()
        {
            bool ServoIsOn = true;
            if (PublicDeclare.bIsSimulate == false)
            {
                foreach (clsEnum.enuAxis eAxis in clsDioMotion.mDic_AxisInfo.Keys)
                {
                    //如果是步進馬達需要continue跳過
                    if (false
                        //|| eAxis == clsEnum.enuAxis.Axis1
                        )
                    {
                        continue;
                    }
                    if (clsMotionCtrl.GetIoStatus(eAxis, clsMotionCtrl.enuMotionIoName.SVON) == false)
                    {
                        ServoIsOn = false;
                    }
                }
                if (ServoIsOn == true)
                {
                    return true;
                }
                else if (formMessageBox.Show("Have Motor Servo Off, Click \"OK\" Trun It On.", "Servo On", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    ProcInitial.SetMotionServo(true);
                    Thread.Sleep(100);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary> 動作運轉 (p_IsPass : 是否忽略詢問，主要是給機台實體按鈕使用) </summary>
        static public int EqRun(bool p_IsPass)
        {
            if (CheckCanRun(enuRunMode.AutoRun) == false)
            {
                return 1;
            }
            DialogResult result = DialogResult.No;
            if (!p_IsPass)
            {
                result = formMessageBox.Show("Are you sure to Start Running?", "Auto Run", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
            if (result == DialogResult.Yes || p_IsPass)
            {
                #region//AutoRun Setup
                //[ArtEQ]
                clsEditRunThread.IsRunning = true;
                clsEditRunThread.RunThread = true;

                //[ArtCommonLib] 單步動作流程，清除狀態後繼續未完成動作
                clsThreadProcManage.bIsStepProc = false;//(100號停止主要Flag)
                clsThreadProcManage.bStartStepRun = false;

                //[ArtEQ] ProcAutoRun.cs
                ProcAutoRun.bIsAutoRunMode = true;
                ProcAutoRun.bIsManualMode = false;
                if (m_ProcAutoRun.iStepIndex == -1)
                {
                    m_ProcAutoRun.iStepIndex = 0;
                }
                m_ProcAutoRun.bIsProcessing = true;
                if (ProcAutoRun.bIsLotEnd == true)
                {
                    ProcAutoRun.bIsAlreadyStartLotEnd = true;//啟動後確保無法結批
                }

                clsCmData.g_NowEqStatus = clsCmData.enuEqStatus.Run;
                #endregion
                return 0;
            }
            return -1;
        }
        /// <summary> 強制停止運轉 </summary>
        static public void EqStop()
        {
            ArtSystem.ucArtMain_Design.GetSingleton().HideProc();
            #region//強制停止Initial流程
            m_ProcInitial.iStepIndex = -1;
            m_ProcInitial.bIsProcessing = false;
            #endregion
            SetAllProcessKeepProc();//將所有Process的KeepProc設定為false
            if (clsCmData.g_bIsinitialized == false)//機台是否在正常狀態 (完成Initial)
            {
                #region//不是正常停止 (舉例：EMO, InitialFail, ...)
                clsCmData.g_bIsinitialized = false;
                if (clsCmData.g_NowEqStatus != clsCmData.enuEqStatus.Down)
                {
                    clsCmData.g_NowEqStatus = clsCmData.enuEqStatus.Default;
                }

                m_ProcDetectIO.bIsKeepProc = true;//強制停止運轉-停止所有流程除了(ProcDetectIO)
                clsThreadProcManage.SetAllStop();

                ProcInitial.SetMotionStop();//停止所有馬達
                ProcInitial.SetMotionServo(false);

                clsThreadProcManage.bIsStepProc = true;//EqStopFlag

                clsDioCtrl.SetDo(clsEnum.enuDo.Safety_Door_Lock, false);//開門
                #endregion
            }
            else if (formAlarmReport.IsAlarmOccur() == true)
            {
                clsCmData.g_NowEqStatus = clsCmData.enuEqStatus.Warning;
                clsThreadProcManage.bIsStepProc = true;//EqStopFlag
                clsEditRunThread.RunThread = true;//Always True
            }
            else if (m_ProcAutoRun.bIsProcessing == false)
            {
                clsCmData.g_NowEqStatus = clsCmData.enuEqStatus.Idle;
                clsThreadProcManage.bIsStepProc = true;//EqStopFlag
                clsEditRunThread.RunThread = true;//Always True
            }
            else
            {
                clsCmData.g_NowEqStatus = clsCmData.enuEqStatus.Stop;
                clsThreadProcManage.bIsStepProc = true;//EqStopFlag
                clsEditRunThread.RunThread = true;//Always True
            }
        }
        /// <summary>初始化</summary>
        static public void Initial()
        {
            if (CheckCanRun(enuRunMode.Initial) == false)
            {
                return;
            }
            DialogResult result = DialogResult.No;
            if (ProcAutoRun.sLotID != "" && ProcAutoRun.sLotID != null)
            {
                if (formMessageBox.Show("LotID is not Empty, Clean it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    clsLog.Log(clsEnum.enuLogName.ButtonLog, clsCmData.g_strNowUser + ", Clicked Initial and Clean Lot ID : " + ProcAutoRun.sLotID);
                    ProcAutoRun.sLotID = "";
                }
            }
            result = formMessageBox.Show("Are you sure to Initial?", "Initial", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                clsLog.Log(clsEnum.enuLogName.ButtonLog, clsCmData.g_strNowUser + ", Clicked Initial Start Process");
                clsCmData.g_bIsinitialized = false;//清除 完成初始化的Flag
                clsArtSystem.ResetCatchOccour();//清除 Warning Catch Log 提示
                formAlarmReport.AlarmClear();//清除 所有Alarm Message

                EqStop();

                clsCmData.g_NowEqStatus = clsCmData.enuEqStatus.Initial;//切換 EqStatus

                clsThreadProcManage.bIsStepProc = false;//EqStop的Flag
                clsThreadProcManage.bStartStepRun = false;

                m_ProcInitial.iStepIndex = 0;
                m_ProcInitial.bIsProcessing = true;

                // Initial 才清 Closing 旗標
                ProcAutoRun.bIsLotEnd = false;//duncan
                ProcAutoRun.bIsAlreadyStartLotEnd = false;
                ProcAutoRun.bIsStopLoad = false;

                //[ProcAutRun]
                ProcAutoRun.bIsAutoRunMode = false;//Reset 機台運轉模式
                ProcAutoRun.bIsManualMode = false;//Reset 機台運轉模式
                m_ProcAutoRun.iStepIndex = -1;//停止流程
                m_ProcAutoRun.bIsProcessing = false;//停止流程

                //todo: 範例
                //Proc_Mag_LoadOK.GetSingleton().RunInitial();
                //Proc_loadCup_lane.GetSingleton().RunInitial();
                //Proc_Station_LoadCup.GetSingleton().RunInitial();
                //Proc_FillTea_Lane.GetSingleton().RunInitial();
                //Proc_Station_PourTea.GetSingleton().RunInitial();
                //Proc_Seal_Lane.GetSingleton().RunInitial();
                //Proc_Station_Seal.GetSingleton().RunInitial();
                //Proc_AOI_Lane.GetSingleton().RunInitial();
                //Proc_Station_AOI.GetSingleton().RunInitial(); ;
                //Proc_OK_Lane.GetSingleton().RunInitial();
                //Proc_Mag_UnloadOK.GetSingleton().RunInitial();

                //Proc_Mag_LoadNG.GetSingleton().RunInitial();
                //Proc_NG_Lane.GetSingleton().RunInitial();
                //Proc_Mag_UnloadNG.GetSingleton().RunInitial();
                //Proc_Station_Sort.GetSingleton().RunInitial();

                clsEditRunThread.RunThread = true;//Always True
            }
        }
        /// <summary>手動動作使用</summary>
        static public bool EqManualRun()
        {
            if (CheckCanRun(enuRunMode.ManualRun) == false)
            {
                return false;
            }
            ProcAutoRun.bIsAutoRunMode = false;
            ProcAutoRun.bIsManualMode = true;
            clsThreadProcManage.bIsStepProc = true;//單動流程不可以讓此Flag設為false (如果設定為false，所有流程都會繼續執行)
            //如果要讓單一流程自己執行，可以指定流程中的KeepProc設定為true，讓其單動流程可以運行。
            return true;
        }


        /// <summary> 停止入料 (有需要可自行編輯) </summary>
        static public void StopLoad()
        {
        }

        /// <summary> 強制結批 (有需要可自行編輯)</summary> //duncan
        static public void LotEnd()
        {
            ProcAutoRun.bIsLotEnd = true;
            ProcAutoRun.bIsAlreadyStartLotEnd = true;
            ProcAutoRun.bIsStopLoad = true;

            clsLog.Log(
                clsEnum.enuLogName.ProcessLog,
                "Closing requested. Stop loading new trays and drain current lanes."
            );
        }

        /// <summary> 確認所有流程動作完全停止 </summary>
        static public bool CheckAllProcessStop()//!!
        {
            return true;
        }


        #endregion

        #region //===================== public/private 函式設置 =====================


        /// <summary> 把所有流程加入執行緒列表(Debug) </summary>
        static public void CreatProc()            // tao
        {
            // 如果這裡沒有先呼叫 GetSingleton() 建立流程物件，
            // DebugForm 裡面的 Case Log 有機會無法顯示其流程。

            #region //===================== Tarot Proc =====================

            //! 自動運轉入口如果本身沒有 Singleton，就不要放這裡。
            //TODO: ProcAutoRun 通常由主程式建立，不一定是 GetSingleton 架構。

            // todo: 範例
            //Proc_Mag_LoadOK.GetSingleton();
            //Proc_loadCup_lane.GetSingleton();
            //Proc_FillTea_Lane.GetSingleton(); // 新增
            //Proc_Seal_Lane.GetSingleton(); // 新增
            //Proc_AOI_Lane.GetSingleton(); // 新增
            //Proc_OK_Lane.GetSingleton();
            //Proc_NG_Lane.GetSingleton(); // 新增：獨立 NG 站點線
            //Proc_Mag_LoadNG.GetSingleton(); // 新增
            //Proc_Mag_UnloadNG.GetSingleton(); // 新增
            //Proc_Mag_UnloadOK.GetSingleton();
            //Proc_Station_LoadCup.GetSingleton(); // 新增：LoadCup 展點站
            //Proc_Station_PourTea.GetSingleton(); // 新增：PourTea 展點站
            //Proc_Station_Seal.GetSingleton(); // 新增：Seal 展點站
            //Proc_Station_AOI.GetSingleton(); // 新增：AOI 展點站

            #endregion

            #region //===================== Tarot AR =====================

            // Tarot 主控 AR
            //AR_TarotAutoRun.GetSingleton();

            //// Tarot 小 AR
            //AR_TarotWarehouseOut.GetSingleton();
            //AR_TarotLoadLane.GetSingleton();
            //AR_TarotDrawCard.GetSingleton();
            //AR_TarotReturnToCardPile.GetSingleton();
            //AR_TarotUnloadToWarehouse.GetSingleton();

            #endregion

            #region //===================== Tarot Proc Singleton 初始化 =====================
            // 依目前專案 Proc 資料夾維護。
            // 舊範本的 Proc_Mgz_xxx / Proc_loadCup_lane_xxx / Proc_Laser / Proc_Transfer 全部不要用。

            //Proc_Warehouse.GetSingleton();
            //Proc_PendingLoadLane.GetSingleton();
            //Proc_TransferLane.GetSingleton();
            //Proc_ShuffleModule.GetSingleton();
            //Proc_CardPileLane.GetSingleton();

            //Proc_ArmTransfer.GetSingleton();

            //Proc_AOI.GetSingleton();
            //Proc_ReadCard.GetSingleton();
            //Proc_Recycle.GetSingleton();

            //Proc_UnloadLane.GetSingleton();

            #endregion

        }

        /// <summary> 關閉所有流程的KeepProcessFlag (ProcDetectIO例外) </summary>
        static private void SetAllProcessKeepProc()
        {
            foreach (clsThreadProc ThrdProc in clsThreadProcManage.dctProcLib.Values)
            {
                if (ThrdProc == m_ProcDetectIO)
                {
                    ThrdProc.bIsKeepProc = true;
                }
                else
                {
                    ThrdProc.bIsKeepProc = false;
                }
            }
        }

        #endregion

        #region//===================== 以下為主要流程 =====================

        /// <summary> 主流程非必要勿調整 </summary>
        protected override void ThreadScenario()
        {
            CreatProc();//把所有流程加入執行緒列表(Debug) 
            clsAPI.timeBeginPeriod(1);
            do
            {
                //DIO 更新
                clsDioCtrl.RefreshDiData();//集中收集DI狀態更新至clsDioCtrl
                clsDioCtrl.RefreshDoData();//集中將clsDioCtrl的狀態設定至DO
                if (RunThread)
                {
                    // 先複製一份，避免 foreach 時 Dictionary 被修改
                    List<clsThreadProc> lstProc =
                        new List<clsThreadProc>(
                            clsThreadProcManage.dctProcLib.Values);

                    foreach (clsThreadProc ThrdProc in lstProc) // 不允許平行For回圈 tao
                    {
                        if (ThrdProc.bIsProcessing) // 執行所有Proc流程
                        {
                            try
                            {
                                ThrdProc.Work();
                            }
                            catch (Exception ex)
                            {
                                clsLog.Log(
                                    clsEnum.enuLogName.CatchLog,
                                    "Source : " + ex.Source +
                                    " , StackTrace : " + ex.StackTrace +
                                    ", Message : " + ex.Message);
                            }
                        }
                    }
                }
                Thread.Sleep(1);
            }
            while (clsCmData.g_bIsRunThreadAlive);
            clsAPI.timeEndPeriod(1);
        }

        /// <summary> Core 2動作流程 </summary>
        protected override void ThreadScenarioCore2()
        {
            do
            {
                while (!RunThreadCore2 && clsCmData.g_bIsRunThreadAlive)
                {
                }
                //以下加入流程動作
                Thread.Sleep(1);
            }
            while (clsCmData.g_bIsRunThreadAlive);
        }

        /// <summary> Core 3動作流程 </summary>
        protected override void ThreadScenarioCore3()
        {
            do
            {
                while (!RunThreadCore3 && clsCmData.g_bIsRunThreadAlive)
                {
                }
                //以下加入流程動作
                Thread.Sleep(1);
            }
            while (clsCmData.g_bIsRunThreadAlive);
        }

        #endregion
    }
}

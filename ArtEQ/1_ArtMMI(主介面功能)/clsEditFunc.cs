using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtEQ._3_UI_介面管理_._1_Operator_操作模式_;
using ArtEQ._3_UI_介面管理_._2_Manual_手動模式_;
using ArtSystem;
using ArtSystem.MultiSystem;


namespace ArtEQ
{
    /// <summary> 注意此處編輯為區分常修改處以增加維護性，與 formMain.cs 相同，請避免參數、函式等…重覆 </summary>
    public partial class clsEditFunc : Form
    {
        private clsHiPerfTimer mTimer_AddFunc = new clsHiPerfTimer();

        /// <summary> ArtMainDesign的功能委派 </summary>
        protected bool formMain__evt_ArtMainFunc(ucArtMain_Design.enuFunc e_Function)
        {
            bool rValue = false;
            switch (e_Function)
            {
                case ucArtMain_Design.enuFunc.EditFunc:
                    rValue = EditFunc();
                    break;
                case ucArtMain_Design.enuFunc.HardwareInit:
                    rValue = HardwareInit();
                    break;
                case ucArtMain_Design.enuFunc.HardwareDestroy:
                    rValue = HardwareDestroy();
                    break;
                case ucArtMain_Design.enuFunc.SoftwareInit:
                    rValue = SoftwareInit();
                    break;
                case ucArtMain_Design.enuFunc.SoftwareDestroy:
                    rValue = SoftwareDestroy();
                    break;
                case ucArtMain_Design.enuFunc.ActiveThread:
                    //開始Thread 動作
                    if (!clsCmData.g_bIsRunThreadAlive)
                    {
                        //設定軸卡參數
                        if (clsArtSystem.bIsSoftwareSimulate == false)
                        {
                            ProcInitial.SetCardPmt();
                            ProcInitial.AxisInitial();
                        }
                        clsEditRunThread.IsLockCpu = false;
                        clsEditRunThread.ThreadStart();
                    }
                    rValue = true;
                    break;
                case ucArtMain_Design.enuFunc.ChangePagePremit:
                    rValue = ChangePagePremit();
                    break;
                case ucArtMain_Design.enuFunc.FormClossingPremit:
                    rValue = FormClossingPremit();
                    break;
                case ucArtMain_Design.enuFunc.FormLoad_UserLogin:
                    rValue = FormLoad_UserLogin();
                    break;
                case ucArtMain_Design.enuFunc.AutoLogout:
                    rValue = AutoLogout();
                    break;
                case ucArtMain_Design.enuFunc.ParameterPathChange:
                    rValue = ParameterPathChanged();
                    break;
                default:
                    break;
            }
            return rValue;
        }
        /// <summary> 編輯設置的所有功能  </summary>
        public bool EditFunc()
        {
            clsLog.Log(clsEnum.enuLogName.StartUpLog, "[EditFunc Initial] Start.");
            clsHiPerfTimer mTimer = new clsHiPerfTimer();
            mTimer.Restart();
            mTimer_AddFunc.Restart();
            ucMotionSetting pMotionSetting = ucMotionSetting.GetSingleton(4, ArtSystem.MultiSystem.clsMultiSystem.GetInvisibleAxis());
            ucSpeedSetting pSpeedSetting = ucSpeedSetting.GetSingleton(4, ArtSystem.MultiSystem.clsMultiSystem.GetInvisibleAxis());
            pMotionSetting.Parent = this;
            pSpeedSetting.Parent = this;


            #region//Operation

            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Operation, "Auto Run", ucAutoRun.GetSingleton()));
            //AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Operation, "Machine Status", ucMachineStatus.GetSingleton()));
            //Add_uc2DMappingEtel(clsCmData.enuMainFunc.Operation);
            #endregion

            #region//Manual

            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Manual, "Manual Form", ucManualForm.GetSingleton()));
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Manual, "Function Setting", ucFunctionSetting.GetSingleton()));
            //AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Manual, "Product Setting", ucBoatSetting.GetSingleton()));
            //AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Manual, "Motion Teach", ucMotionTeach.GetSingleton()));
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Manual, "Time Setting", ucTimeSetting.GetSingleton()));
            //AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Manual, "Parameter Setting", ucParameterForm.GetSingleton()));

            #endregion

            #region//Hardware

            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Hardware, "DIO Monitor", ucDioMonitor.GetSingleton(16)));
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Hardware, "Speed Setting", pSpeedSetting));//, clsSystem.LstInvisibleAxis(), clsSystem.mDic_ChangeNameAxis());
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Hardware, "Axis Setting", pMotionSetting));//, clsSystem.LstInvisibleAxis(), clsSystem.mDic_ChangeNameAxis());
            ucMotionCtrlTab mMotionCtrlTab = ucMotionCtrlTab.GetSingleton(ArtSystem.MultiSystem.clsMultiSystem.GetInvisibleAxis());
            mMotionCtrlTab.VisibleChanged += new EventHandler(mMotionCtrlTab_VisibleChanged);
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Hardware, "Axis Control", mMotionCtrlTab));//clsSystem.LstInvisibleAxis(), clsSystem.mDic_ChangeNameAxis())));
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Hardware, "Devices", ucDevices.GetSingleton()));
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Hardware, "Cylinder Test", ucCylinderTest.GetSingleton()));

            #endregion

            #region//Software
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "Parameter Manage", ucRecipe.GetSingleton()));
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "User Account", ucUserAccount.GetSingleton()));
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "Windows Setup", ArtSystem.FormDesign.ucFormSetup.GetSingleton(true)));
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "Log Path Manage", ucLogPath.GetSingleton()));
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "Remove Files", ucRemoveFiles.GetSingleton()));
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "SignalTower Manage", ucSignalTowerManage.GetSingleton()));
            //AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "Data Viewer", ucDataViewer.GetSingleton()));
            //AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "Parameter Display", ucParameterDisplay.GetSingleton()));
            //AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "System Info", ucSystemInfo.GetSingleton()));
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "Parameter Display", ucParameterControl.GetSingleton()));
            if (true)//只有Super User才能進入 (索引:@1023@)
            {
                AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "Process Manage", ucProcessControl.GetSingleton()));
                AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "Alarm Manage", ucAlarmManage.GetSingleton()));//[基本上已經不需要此功能了]
                AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "Multi System", ArtSystem.MultiSystem.ucMultiSystem.GetSingleton()));
            }
            #endregion
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Software, "DLL Version", ArtSystem.ucDLLVersion.GetSingleton()));

            #region//Log
            AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Log, "Alarm Counter", ucAlarmCounter.GetSingleton()));
            foreach (clsEnum.enuLogName LogName in Enum.GetValues(typeof(clsEnum.enuLogName)))
            {
                if (LogName == clsEnum.enuLogName.CatchLog
                    || LogName == clsEnum.enuLogName.StartUpLog)
                {
                    ucLogHistory.GetSingleton(LogName.ToString()).Visible = false;
                    continue;
                }
                AddFunc(new clsObjFunc(clsCmData.enuMainFunc.Log, LogName.ToString(), ucLogHistory.GetSingleton(LogName.ToString())));
            }
            #endregion

            //設置Log 起始
            foreach (clsEnum.enuLogName eLogName in Enum.GetValues(typeof(clsEnum.enuLogName)))
            {
                if (eLogName == clsEnum.enuLogName.CatchLog
                    || eLogName == clsEnum.enuLogName.StartUpLog)
                {
                    continue;
                }
                clsLog.Log(eLogName, "=================== Start Log ===================");
            }

            //掛載需要翻譯的元件
            ucArtMain_Design.GetSingleton()._CollectChangeLanguageControls(formLogin.GetSingleton());
            ucArtMain_Design.GetSingleton()._CollectChangeLanguageControls(ucParameter.GetSingleton());
            ucArtMain_Design.GetSingleton()._CollectChangeLanguageControls(ucRecipeManager.GetSingleton());

            mTimer.Stop();
            clsLog.Log(clsEnum.enuLogName.StartUpLog, "[EditFunc Initial] End, Spend Time (" + mTimer.ElapsedMilliseconds.ToString("F3") + " ms).");
            return true;
        }

        /// <summary> 硬體初始化 </summary>
        public bool HardwareInit()
        {
            clsLog.Log(clsEnum.enuLogName.StartUpLog, "[Hardware Initial] Start.");
            clsHiPerfTimer mTimer = new clsHiPerfTimer();
            mTimer.Restart();
            clsHiPerfTimer mTimer_Sub = new clsHiPerfTimer();
            mTimer_Sub.Restart();

            if (PublicDeclare.bIsDiaTwin == true)
            {
                //ArtTwinDriver.clsTwinDioCtrl.Initial(enuTwinCtrlType.TwinCtrl);
                //ArtTwinDriver.clsTwinMotionCtrl.Initial(enuTwinCtrlType.TwinCtrl);
                //ArtTwinDriver.clsDIATwin_EtherCAT.GetSingleton();
            }

            //多載系統
            if (ArtSystem.MultiSystem.clsMultiSystem.HardwareInitial() == false)
            {
                formMessageBox.Show("Hardware Initial Fail. ");
                clsLog.Log(clsEnum.enuLogName.StartUpLog, "-> HardwareInit()->clsMultiSystem.HardwareInitial(Fail); " + " (" + mTimer_Sub.ElapsedMilliseconds.ToString("F3") + " ms)");
                return false;
            }
            if (PublicDeclare.bIsDiaTwin == true)
            {
                //clsDioCtrl.bIsActual = true;
                //clsMotionCtrl.bIsActual = true;
                //clsCtrlHighSensor.g_SimulateDistanceSensor = new clsCtrlHighSensor.SimulateDistanceSensor(_SimulateDistanceSensor);
            }
            clsLog.Log(clsEnum.enuLogName.StartUpLog, "-> HardwareInit()->clsMultiSystem.HardwareInitial(); " + " (" + mTimer_Sub.ElapsedMilliseconds.ToString("F3") + " ms)");
            mTimer_Sub.Restart();

            System.Threading.Thread.Sleep(500);


            clsLog.Log(clsEnum.enuLogName.StartUpLog, "[Hardware Initial] End, Spend Time (" + mTimer.ElapsedMilliseconds.ToString("F3") + " ms).");

            mTimer.Stop();
            return true;
        }
        static public double _SimulateDistanceSensor(string DeviceName)
        {
            double rValue = 0;
            //List<ArtTwinDriver.clsDIATwin_ItemInfo> Lst_DistanceSensor = ArtTwinDriver.clsDIATwin_ItemInfo.GetComponentItems(ArtTwinDriver.ComponentItemType.DistanceSensor);
            //需要自己匹配

            return rValue;
        }
        /// <summary> 硬體結束設置 </summary>
        public bool HardwareDestroy()
        {
            clsLog.Log(clsEnum.enuLogName.StartUpLog, "[Hardware Destroy] Start.");
            clsHiPerfTimer mTimer = new clsHiPerfTimer();
            mTimer.Restart();

            ArtSystem.MultiSystem.clsMultiSystem.HadrwareDestroy();

            //System.Threading.Thread.Sleep(500);
            clsLog.Log(clsEnum.enuLogName.StartUpLog, "[Hardware Destroy] End, Spend Time (" + mTimer.ElapsedMilliseconds.ToString("F3") + " ms).");
            mTimer.Stop();
            return true;
        }

        /// <summary> 軟體初始化 </summary>
        public bool SoftwareInit()
        {
            clsLog.Log(clsEnum.enuLogName.StartUpLog, "[Software Initial] Start.");
            clsHiPerfTimer mTimer = new clsHiPerfTimer();
            mTimer.Restart();


            //移除LanguageINI內未定義的多於文字
            //LanguageINI_RemoveUndefineTXT();


            ProcInitial.SetMotionServo(false);
            ProcInitial.SetCardPmt();
            ProcInitial.AxisInitial();

            //formAlarmReport設定 (連結函式指標)
            formAlarmReport.GetSingleton().Owner = ucArtMain_Design.GetSingleton().ParentForm;
            formAlarmReport.LinkAlarmProcFunc(clsEditAlarmProc.AlarmCodeProc);
            formAlarmReport.LinkReportProcFunc(clsEditAlarmProc.ReportProc);
            formAlarmReport.ShowForm();
            formAlarmReport.HideForm();

            ArtEQ.clsEditRunThread.CreatProc();//把所有流程加入執行緒列表(Debug) 
            ArtEQ.clsEditRunThread.RunThread = true;

            if (PublicDeclare.bIsSimulate == true)
            {
                clsDioCtrl.SetDi(clsEnum.enuDi.EMO_B, true);
                clsDioCtrl.SetDi(clsEnum.enuDi.SafeDoor_B, true);
                clsDioCtrl.SetDi(clsEnum.enuDi.Air_Source_B, true);
                clsDioCtrl.SetDi(clsEnum.enuDi.Button_Power, true);
            }

            clsLog.Log(clsEnum.enuLogName.StartUpLog, "[Software Initial] End, Spend Time (" + mTimer.ElapsedMilliseconds.ToString("F3") + " ms).");

            mTimer.Stop();
            return true;
        }

        /// <summary> 軟體結束設置 </summary>
        public bool SoftwareDestroy()
        {
            clsLog.Log(clsEnum.enuLogName.StartUpLog, "[Software Destroy] Start.");
            clsHiPerfTimer mTimer = new clsHiPerfTimer();
            mTimer.Restart();

            //停止Thread 動作
            clsEditRunThread.ThreadStop();

            //設置Log 結束
            foreach (clsEnum.enuLogName eLogName in Enum.GetValues(typeof(clsEnum.enuLogName)))
            {
                if (eLogName == clsEnum.enuLogName.CatchLog
                    || eLogName == clsEnum.enuLogName.StartUpLog)
                {
                    continue;
                }
                clsLog.Log(eLogName, "=================== End Log ===================");
            }

            clsLog.Log(clsEnum.enuLogName.StartUpLog, "[Software Destroy] End, Spend Time (" + mTimer.ElapsedMilliseconds.ToString("F3") + " ms).");
            mTimer.Stop();
            return true;
        }

        ///<summary> 詢問是否能切換頁面 </summary>
        public bool ChangePagePremit()
        {
            //[NeedFix]PublicDeclare.bEnable_KeyboardMoveXY = false;//@1.0.0.44-6@ 
            return true;
        }

        ///<summary> 詢問是否能關閉程式 </summary>
        public bool FormClossingPremit()
        {
            return true;
        }

        ///<summary> 使用者帳號登入 </summary>
        public bool FormLoad_UserLogin()
        {

            bool rValue = false;
            if (ArtSystem.clsArtSystem.bIsArtMachineSetup == true)
            {
                rValue = formLogin.GetSingleton()._Login("ArtQC", "");
                rValue = true;
            }
            else
            {
                rValue = formLogin.GetSingleton()._ShowLoginForm();
            }
            //開放所有Level 1 的功能權限
            if (clsCmData.g_iNowUserLevel == 1)
            {
                List<string> LstFunc = ucUserAccount.GetFuncList();
                foreach (string sFunc in LstFunc)
                {
                    if (ucUserAccount.GetSingleton().IsSubFuncUsable(sFunc) == false)
                    {
                        ucUserAccount.SetFuncEnable(1, sFunc, true);
                    }
                }
            }
            return rValue;
        }

        ///<summary> 自動登出條件滿足 </summary>
        public bool AutoLogout()
        {
            bool rValue = false;
            //formLogin.GetSingleton()._ShowLoginForm();
            return rValue;
        }

        /// <summary> 參數路徑變更事件 </summary>
        public bool ParameterPathChanged()
        {
            return true;
        }

        #region //===================== private 函式設置 =====================

        /// <summary> 新增功能至g_lstFuncLib 資料集中 </summary>
        private bool AddFunc(clsObjFunc p_FuncClass)
        {
            if (clsCmData.g_dctFuncLib.ContainsKey(p_FuncClass.SubFunc) == false)
            {
                ucArtMain_Design.GetSingleton()._AddFunc(p_FuncClass);
                clsLog.Log(clsEnum.enuLogName.StartUpLog, "-> EditFunc()->AddFunc() : " + p_FuncClass.MainFunc + "->" + p_FuncClass.SubFunc + " (" + mTimer_AddFunc.ElapsedMilliseconds.ToString("F3") + " ms)");
                mTimer_AddFunc.Restart();
                return true;
            }
            return false;
        }

        /// <summary> 移除LanguageINI內未定義的多於文字 </summary>
        private void LanguageINI_RemoveUndefineTXT()
        {
            try
            {
                string sLanguageINI_Path = clsCmData.g_strSystemIniFilePath.Replace("artSystem.ini", "Language.ini");
                if (System.IO.File.Exists(sLanguageINI_Path) == true)
                {
                    List<string> mLanguageData = System.IO.File.ReadAllLines(sLanguageINI_Path).ToList<string>();
                    int i = 0;
                    for (i = 0; i < mLanguageData.Count; i++)
                    {
                        if (mLanguageData[i] == "[Undefine]")
                        {
                            break;
                        }
                    }
                    mLanguageData.RemoveRange(i + 1, mLanguageData.Count - (i + 1));
                    System.IO.File.WriteAllLines(sLanguageINI_Path, mLanguageData);
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        #endregion

        #region //===================== private 函式設置 (重新排版 ucMotionCtrlTab) =====================

        private void mMotionCtrlTab_VisibleChanged(object sender, EventArgs e)
        {
            return;
            try
            {
                if (sender is ucMotionCtrlTab)
                {
                    ucMotionCtrlTab Item = (ucMotionCtrlTab)sender;
                    if (Item.Visible == true)
                    {
                        if (ucArtMain_Design.GetSingleton().e_Design == ucArtMain_Design.enuDesign.Panel)
                        {
                            int groupBoxPosition_Top = 0;
                            #region//groupBoxPosition
                            {
                                Control[] FindItems = Item.Controls.Find("groupBoxPosition", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Height = 105;
                                    groupBoxPosition_Top = FindItems[i].Top;
                                }
                            }
                            #endregion
                            #region//groupBox2
                            {
                                Control[] FindItems = Item.Controls.Find("groupBox2", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 105 + groupBoxPosition_Top + 6;
                                    FindItems[i].Height = Item.Height - 131 - 50 - FindItems[i].Top;
                                }
                            }
                            #endregion
                            #region//groupBox6
                            {
                                Control[] FindItems = Item.Controls.Find("groupBox6", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 142;
                                    FindItems[i].BringToFront();
                                }
                            }
                            #endregion

                            #region//numStartVel
                            {
                                Control[] FindItems = Item.Controls.Find("numStartVel", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 108;
                                }
                            }
                            #endregion
                            #region//label5
                            {
                                Control[] FindItems = Item.Controls.Find("label5", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 112;
                                }
                            }
                            #endregion
                            #region//numMaxVel
                            {
                                Control[] FindItems = Item.Controls.Find("numMaxVel", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 138;
                                }
                            }
                            #endregion
                            #region//label6
                            {
                                Control[] FindItems = Item.Controls.Find("label6", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 141;
                                }
                            }
                            #endregion
                            #region//numTacc
                            {
                                Control[] FindItems = Item.Controls.Find("numTacc", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 167;
                                }
                            }
                            #endregion
                            #region//label7
                            {
                                Control[] FindItems = Item.Controls.Find("label7", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 170;
                                }
                            }
                            #endregion
                            #region//numTdec
                            {
                                Control[] FindItems = Item.Controls.Find("numTdec", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 196;
                                }
                            }
                            #endregion
                            #region//label9
                            {
                                Control[] FindItems = Item.Controls.Find("label9", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 199;
                                }
                            }
                            #endregion
                            #region//numRepeatDelay
                            {
                                Control[] FindItems = Item.Controls.Find("numRepeatDelay", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 225;
                                }
                            }
                            #endregion
                            #region//label14
                            {
                                Control[] FindItems = Item.Controls.Find("label14", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 228;
                                }
                            }
                            #endregion


                            #region//numSacc
                            {
                                Control[] FindItems = Item.Controls.Find("numSacc", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 251;
                                    FindItems[i].Visible = false;
                                }
                            }
                            #endregion
                            #region//label11
                            {
                                Control[] FindItems = Item.Controls.Find("label11", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 254;
                                    FindItems[i].Visible = false;
                                }
                            }
                            #endregion
                            #region//numSdec
                            {
                                Control[] FindItems = Item.Controls.Find("numSdec", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 276;
                                    FindItems[i].Visible = false;
                                }
                            }
                            #endregion
                            #region//label10
                            {
                                Control[] FindItems = Item.Controls.Find("label10", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 279;
                                    FindItems[i].Visible = false;
                                }
                            }
                            #endregion
                            #region//txtCycleTime
                            {
                                Control[] FindItems = Item.Controls.Find("txtCycleTime", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 342;
                                    FindItems[i].Visible = false;
                                }
                            }
                            #endregion
                            #region//label15
                            {
                                Control[] FindItems = Item.Controls.Find("label15", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 345;
                                    FindItems[i].Visible = false;
                                }
                            }
                            #endregion
                            #region//rbtnCuveT
                            {
                                Control[] FindItems = Item.Controls.Find("rbtnCuveT", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    if (FindItems[i] is RadioButton)
                                    {
                                        RadioButton Temp = (RadioButton)FindItems[i];
                                        Temp.Checked = true;
                                    }
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            int groupBoxPosition_Top = 0;
                            #region//groupBoxPosition
                            {
                                Control[] FindItems = Item.Controls.Find("groupBoxPosition", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Height = 140;
                                    groupBoxPosition_Top = FindItems[i].Top;
                                }
                            }
                            #endregion
                            #region//groupBox2
                            {
                                Control[] FindItems = Item.Controls.Find("groupBox2", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 140 + groupBoxPosition_Top + 6;
                                    FindItems[i].Height = Item.Height - 131 - 50 - FindItems[i].Top;
                                }
                            }
                            #endregion
                            #region//groupBox6
                            {
                                Control[] FindItems = Item.Controls.Find("groupBox6", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 224;
                                    FindItems[i].BringToFront();
                                }
                            }
                            #endregion


                            #region//numStartVel
                            {
                                Control[] FindItems = Item.Controls.Find("numStartVel", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 122;
                                }
                            }
                            #endregion
                            #region//label5
                            {
                                Control[] FindItems = Item.Controls.Find("label5", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 125;
                                }
                            }
                            #endregion
                            #region//numMaxVel
                            {
                                Control[] FindItems = Item.Controls.Find("numMaxVel", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 147;
                                }
                            }
                            #endregion
                            #region//label6
                            {
                                Control[] FindItems = Item.Controls.Find("label6", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 150;
                                }
                            }
                            #endregion
                            #region//numTacc
                            {
                                Control[] FindItems = Item.Controls.Find("numTacc", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 189;
                                }
                            }
                            #endregion
                            #region//label7
                            {
                                Control[] FindItems = Item.Controls.Find("label7", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 192;
                                }
                            }
                            #endregion
                            #region//numTdec
                            {
                                Control[] FindItems = Item.Controls.Find("numTdec", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 214;
                                }
                            }
                            #endregion
                            #region//label9
                            {
                                Control[] FindItems = Item.Controls.Find("label9", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 217;
                                }
                            }
                            #endregion
                            #region//numSacc
                            {
                                Control[] FindItems = Item.Controls.Find("numSacc", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 251;
                                    FindItems[i].Visible = true;
                                }
                            }
                            #endregion
                            #region//label11
                            {
                                Control[] FindItems = Item.Controls.Find("label11", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 254;
                                    FindItems[i].Visible = true;
                                }
                            }
                            #endregion
                            #region//numSdec
                            {
                                Control[] FindItems = Item.Controls.Find("numSdec", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 276;
                                    FindItems[i].Visible = true;
                                }
                            }
                            #endregion
                            #region//label10
                            {
                                Control[] FindItems = Item.Controls.Find("label10", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 279;
                                    FindItems[i].Visible = true;
                                }
                            }
                            #endregion
                            #region//numRepeatDelay
                            {
                                Control[] FindItems = Item.Controls.Find("numRepeatDelay", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 310;
                                }
                            }
                            #endregion
                            #region//label14
                            {
                                Control[] FindItems = Item.Controls.Find("label14", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 313;
                                }
                            }
                            #endregion
                            #region//txtCycleTime
                            {
                                Control[] FindItems = Item.Controls.Find("txtCycleTime", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 342;
                                    FindItems[i].Visible = true;
                                }
                            }
                            #endregion
                            #region//label15
                            {
                                Control[] FindItems = Item.Controls.Find("label15", true);
                                for (int i = 0; i < FindItems.Length; i++)
                                {
                                    FindItems[i].Top = 345;
                                    FindItems[i].Visible = true;
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }


        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtProcModuleLib;
using ArtControlLib;
using ArtCommonLib;
using ArtData;
using ArtSystem;
using ArtModuleData;

namespace ArtEQ
{
    public partial class ucManualTest : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================

        private List<comManualActionButton> m_dicActionButtonAPs = new List<comManualActionButton>();
        #endregion

        #region //=====================  必要函式設置 =====================

        private static object m_objLock = new object();
        static private ucManualTest m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucManualTest GetSingleton()
        {
            lock (m_objLock)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucManualTest();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucManualTest()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            ucParameter.Add(this);
            CollectNumBox(this);
            this.TimerInterval = 100;
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                UpdateControls_BoatSetting();
                ucCtrlLane1.Initial(clsEnum.enuProcName.PM_Lane.ToString());
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        /// <summary> 自動更新介面參數 </summary>
        protected override void ReflashTimerFunc()
        {
            try
            {
                ucCtrlLane1.ReflashTimerFunc();
                UpdateAP_Button();
                ReflashClsBoatView();
                btnArmProcess.BackColor = Proc_ProcessArm.GetSingleton().m_eAction == Proc_ProcessArm.enuAction.DoProcess ? Color.LightGreen : Color.DarkGray;
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        private void ucMachineStatus_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                this.UpdateControls();
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        #endregion

        #region //===================== private 函式設置 =====================

        private void CollectNumBox(Control p_Control)
        {
            foreach (object obj in p_Control.Controls)
            {
                Control control = (Control)obj;
                if (control is Form
                    || control is Panel
                    || control is FlowLayoutPanel
                    || control is GroupBox
                    || control is SplitContainer
                    || control is SplitterPanel
                    || control is TableLayoutPanel
                    || control is TabControl
                    || control is TabPage
                    || control is ucBaseUserControl)
                {
                    CollectNumBox(control);
                }
                else if (control is comManualActionButton)
                {
                    var ManualActionButton = control as comManualActionButton;
                    if (ManualActionButton._ActionAP != null)
                    {
                        m_dicActionButtonAPs.Add(ManualActionButton);
                    }
                }
            }
        }
        private void UpdateAP_Button()
        {
            List<AutoProcess> WorkingList = new List<AutoProcess>();

            foreach (var keyPair in AutoProcess.m_dctProcData)
            {
                if (keyPair.Value.ActionState == AutoProcess.enuState.Working)
                {
                    WorkingList.Add(keyPair.Value);
                }
            }

            foreach (var item in m_dicActionButtonAPs)
            {
                if (item._ActionAP == null
                    || item._Source == null
                    || item._Target == null)
                    continue;

                string AP_Name = item._ActionAP.ToString();
                if (AutoProcess.m_dctProcData.ContainsKey(AP_Name))
                {
                    var AP = AutoProcess.m_dctProcData[AP_Name];

                    item.Enabled = !WorkingList.Contains(AP);

                    item.BackColor = Color.DarkGray;
                    if (AP.m_Source == null || AP.m_Target == null)
                        continue;

                    if (item._Source.ToString() == AP.m_Source.LocalModuleName
                        && item._Target.ToString() == AP.m_Target.LocalModuleName)
                    {
                        switch (AP.ActionState)
                        {
                            case AutoProcess.enuState.Done:
                                item.BackColor = Color.DarkGray;
                                break;

                            case AutoProcess.enuState.Fail:
                                item.BackColor = Color.DarkRed;
                                if (item.IsCanFireAlarm)
                                {
                                    clsEditRunThread.ReportModuleAlarm(AP);
                                    item.IsCanFireAlarm = false;
                                }
                                break;

                            case AutoProcess.enuState.Ready:
                                break;

                            case AutoProcess.enuState.Working:
                                item.BackColor = Color.LightGreen;
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void ReflashClsBoatView()
        {
            try
            {

                string _path = PM.GetSingleton().ModuleRecipeName;
                #region//LaserLaneA
                PM_Lane mPMLane = PM.GetSingleton().GetPM_Lane(clsEnum.enuProcName.PM_Lane);
                clsBoat_Carry.Visible = mPMLane.GetBoatData().bExist;
                if (_path != "" && mPMLane.GetBoatData().bExist)
                {
                    for (int x = 1; x < clsProcCtrl.GetSingleton().GetRecipetInfo(_path).GetValueInt(clsModuleEnum.enuPmtName.Rec_Carrier_Column) + 1; x++)
                    {
                        for (int y = 1; y < clsProcCtrl.GetSingleton().GetRecipetInfo(_path).GetValueInt(clsModuleEnum.enuPmtName.Rec_Carrier_Row) + 1; y++)
                        {
                            Point p_Unit = new Point();
                            p_Unit.X = x;
                            p_Unit.Y = y;
                            if (mPMLane.GetBoatData().DctUnitData.ContainsKey(p_Unit) == true)
                            {
                                switch (mPMLane.GetBoatData().DctUnitData[p_Unit].enuData_ProductState)
                                {
                                    case clsInfoBase.clsUnitInfo.enuUnitState.NotDetected:
                                        clsBoat_Carry.CarryInfo.SetChipStatus(p_Unit, AutoProcess.enuUnitNeedWorkState.NotDetected);
                                        break;
                                    case clsInfoBase.clsUnitInfo.enuUnitState.OK:
                                        if (mPMLane.GetBoatData().DctUnitData[p_Unit].strUnitBarcode == "" ||
                                            mPMLane.GetBoatData().DctUnitData[p_Unit].strUnitBarcode == "ERROR")
                                        {
                                            clsBoat_Carry.CarryInfo.SetChipStatus(p_Unit, AutoProcess.enuUnitNeedWorkState.ReaderFail);
                                        }
                                        else
                                        {
                                            clsBoat_Carry.CarryInfo.SetChipStatus(p_Unit, AutoProcess.enuUnitNeedWorkState.OK);
                                        }
                                        break;
                                    case clsInfoBase.clsUnitInfo.enuUnitState.NG:
                                        if (mPMLane.GetBoatData().DctUnitData[p_Unit].strUnitBarcode == "" ||
                                            mPMLane.GetBoatData().DctUnitData[p_Unit].strUnitBarcode == "ERROR")
                                        {
                                            clsBoat_Carry.CarryInfo.SetChipStatus(p_Unit, AutoProcess.enuUnitNeedWorkState.ReaderFail);
                                        }
                                        else
                                        {
                                            clsBoat_Carry.CarryInfo.SetChipStatus(p_Unit, AutoProcess.enuUnitNeedWorkState.NG);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                clsBoat_Carry.CarryInfo.SetChipStatus(p_Unit, AutoProcess.enuUnitNeedWorkState.Empty);
                            }
                        }
                    }
                    clsBoat_Carry.CanUpdateView = true;
                    clsBoat_Carry.ReflashCarryView();
                    clsBoat_Carry.CanUpdateView = false;
                }
                else
                {
                    clsBoat_Carry.CarryInfo.ResetStatus();
                    clsBoat_Carry.CanUpdateView = true;
                    clsBoat_Carry.ReflashCarryView();
                    clsBoat_Carry.CanUpdateView = false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }


        public void UpdateControls_BoatSetting()
        {
            clsBoat_Carry._IsShowChipIndex = true;
            clsBoat_Carry._IsShowChipName = true;
            string tempRecipe = PM.GetSingleton().ModuleRecipeName;
            clsBoat_Carry.CarryInfo.CreatChip(
                  clsProcCtrl.GetSingleton().GetRecipetInfo(tempRecipe).GetValueInt(clsModuleEnum.enuPmtName.Rec_Carrier_Column)
                  , clsProcCtrl.GetSingleton().GetRecipetInfo(tempRecipe).GetValueInt(clsModuleEnum.enuPmtName.Rec_Carrier_Row)
                  , 1
                  , clsProcCtrl.GetSingleton().GetRecipetInfo(tempRecipe).GetValueInt(clsModuleEnum.enuPmtName.Rec_Carrier_Quadrant)
                  , false
                  , clsProcCtrl.GetSingleton().GetRecipetInfo(tempRecipe).GetValueBool(clsModuleEnum.enuPmtName.Rec_Carrier_Z_Direction)
                  , clsProcCtrl.GetSingleton().GetRecipetInfo(tempRecipe).GetValueDouble(clsModuleEnum.enuPmtName.Rec_Carrier_UnitPitch_X)
                  , clsProcCtrl.GetSingleton().GetRecipetInfo(tempRecipe).GetValueDouble(clsModuleEnum.enuPmtName.Rec_Carrier_UnitPitch_Y)
                  , GoldenX: clsProcCtrl.GetSingleton().GetRecipetInfo(tempRecipe).GetValueDouble(clsModuleEnum.enuPmtName.Rec_Carrier_ToolMatchToUnit_X)
                  , GoldenY: clsProcCtrl.GetSingleton().GetRecipetInfo(tempRecipe).GetValueDouble(clsModuleEnum.enuPmtName.Rec_Carrier_ToolMatchToUnit_Y)
                  , UpToDownType: clsProcCtrl.GetSingleton().GetRecipetInfo(tempRecipe).GetValueBool(clsModuleEnum.enuPmtName.Rec_Carrier_Up_To_Down)
              );
        }
        #endregion

        #region//===================== 以下為事件處理 =====================


        private bool comManualActionButton1__EventBeforeClick(object sender, EventArgs e)
        {
            if (clsEditRunThread.EqManualRun())
            {
                clsLog.Log(clsEnum.enuLogName.ButtonLog, clsCmData.g_strNowUser + " : " + ((Control)sender).Name + ", Clicked");
                return true;
            }
            return false;
        }



        private void btnArmProcess_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsEditRunThread.EqManualRun())
                {
                    clsLog.Log(clsEnum.enuLogName.ButtonLog, clsCmData.g_strNowUser + " : " + ((Control)sender).Name + ", Clicked");
                    Proc_ProcessArm.GetSingleton().Run_Action(Proc_ProcessArm.enuAction.DoProcess, clsBoat_Carry.GetSelectIndex_ID());
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion
        
    }
}

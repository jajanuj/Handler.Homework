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

namespace ArtEQ
{
    public partial class ucCtrlLane : UserControl
    {
        #region //=====================  區域變數設置 =====================

        private string strSource = "";

        public bool bCanCreateData
        {
            get;
            set;
        }
        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucCtrlLane()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            this.DoubleClick += new EventHandler(ucCtrlLane_DoubleClick);
        }

        public void Initial(string strPM_Name)
        {
            if (clsBaseProc.m_dctProcData.ContainsKey(strPM_Name) == true)
            {
                strSource = strPM_Name;
            }
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        /// <summary> 自動更新介面參數 </summary>
        public void ReflashTimerFunc()
        {
            try
            {
                if (clsBaseProc.m_dctProcData.ContainsKey(strSource) == true)
                {
                    //if (clsBaseProc.m_dctProcData[strSource] is PM_MgzArm)
                    {
                        clsBaseProc pBaseProc = clsBaseProc.m_dctProcData[strSource];
                        this.BackColor = pBaseProc.GetBoatExist() == true ? Color.DimGray : SystemColors.Control;
                    }
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        private void ucCtrlLane_VisibleChanged(object sender, EventArgs e)
        {
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            if (this.Visible == true)
            {
                this.UpdateControls();
            }
        }

        #endregion


        #region //===================== public 函式設置 =====================


        #endregion

        #region //===================== private 函式設置 =====================


        #endregion

        #region//===================== 以下為事件處理 =====================

        private void ucCtrlLane_DoubleClick(object sender, EventArgs e)
        {
            if (bCanCreateData == false)
            {
                return;
            }
            if (clsBaseProc.m_dctProcData.ContainsKey(strSource) == true)
            {
                //if (clsBaseProc.m_dctProcData[strSource] is PM_Lane)
                {
                    clsBaseProc pBaseProc = clsBaseProc.m_dctProcData[strSource];
                    if (clsCmData.g_NowEqStatus != clsCmData.enuEqStatus.Run
                        && clsCmData.g_NowEqStatus != clsCmData.enuEqStatus.Initial
                        && pBaseProc != null)
                    {
                        if (pBaseProc.GetBoatExist() == false)
                        {
                            if (formMessageBox.Show("Are you sure want to cerate Boat Data?", "Create Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                pBaseProc.GetBoatData().bExist = true;
                                pBaseProc.GetBoatData().SetAllUnit_Exist(true);
                                clsLog.Log(clsEnum.enuLogName.ButtonLog, clsCmData.g_strNowUser + " : " + ((Control)sender).Name + ", Create Boat Data.");
                            }
                        }
                        else
                        {
                            if (formMessageBox.Show("Are you sure want to Clear Boat Data?", "Clear Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                pBaseProc.GetBoatData().bExist = false;
                                pBaseProc.GetBoatData().SetAllUnit_Exist(false);
                                clsLog.Log(clsEnum.enuLogName.ButtonLog, clsCmData.g_strNowUser + " : " + ((Control)sender).Name + ", Clear Boat Data.");
                            }
                        }
                    }
                }
            }
        }

        #endregion


    }
}

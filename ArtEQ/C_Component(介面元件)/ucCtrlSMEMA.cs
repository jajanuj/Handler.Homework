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
    public partial class ucCtrlSMEMA : UserControl
    {
        #region //=====================  區域變數設置 =====================

        //private PM_SMEMA pPM_SMEMA;
        public string strSource = "";

        #endregion

        #region //=====================  必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucCtrlSMEMA()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
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
                if (clsBaseProc.m_dctProcData.ContainsKey(strSource) == true)
                {
                    if (clsBaseProc.m_dctProcData[strSource] is PM_SMEMA)
                    {
                        PM_SMEMA pPM_SMEMA = (PM_SMEMA)clsBaseProc.m_dctProcData[strSource];
                        Dictionary<string, string> pDiName = ucDioMonitor.GetSingleton().GetDiName();
                        Dictionary<string, string> pDoName = ucDioMonitor.GetSingleton().GetDoName();
                        checkBox1.Visible = pPM_SMEMA.m_IsUseDi(PM_SMEMA.enuModuleDi.Sensor_SMEMA) == true;
                        if (pDiName.ContainsKey(pPM_SMEMA.m_GetDiEnum(PM_SMEMA.enuModuleDi.Sensor_SMEMA).ToString() + ":") == true)
                        { checkBox1.Text = ((int)pPM_SMEMA.m_GetDiEnum(PM_SMEMA.enuModuleDi.Sensor_SMEMA)).ToString("DI000") + ":" + pDiName[pPM_SMEMA.m_GetDiEnum(PM_SMEMA.enuModuleDi.Sensor_SMEMA).ToString() + ":"]; }
                        else if(pPM_SMEMA.m_GetDiEnum(PM_SMEMA.enuModuleDi.Sensor_SMEMA) != null)
                        { checkBox1.Text = ((int)pPM_SMEMA.m_GetDiEnum(PM_SMEMA.enuModuleDi.Sensor_SMEMA)).ToString("DI000") + ":" + pPM_SMEMA.m_GetDiEnum(PM_SMEMA.enuModuleDi.Sensor_SMEMA).ToString(); }
                        else
                        { checkBox1.Text ="Null";  }
                        checkBox2.Top = checkBox1.Visible == false ? checkBox1.Top : checkBox1.Top + checkBox1.Height + 4;
                        checkBox2.Visible = pPM_SMEMA.m_IsUseDo(PM_SMEMA.enuModuleDo.OutPut_SMEMA) == true;
                        if (pDoName.ContainsKey(pPM_SMEMA.m_GetDoEnum(PM_SMEMA.enuModuleDo.OutPut_SMEMA).ToString() + ":") == true)
                        { checkBox2.Text = ((int)pPM_SMEMA.m_GetDoEnum(PM_SMEMA.enuModuleDo.OutPut_SMEMA)).ToString("DO000") + ":" + pDoName[pPM_SMEMA.m_GetDoEnum(PM_SMEMA.enuModuleDo.OutPut_SMEMA).ToString() + ":"]; }
                        else if(pPM_SMEMA.m_GetDoEnum(PM_SMEMA.enuModuleDo.OutPut_SMEMA) != null)
                        { checkBox2.Text = ((int)pPM_SMEMA.m_GetDoEnum(PM_SMEMA.enuModuleDo.OutPut_SMEMA)).ToString("DO000") + ":" + pPM_SMEMA.m_GetDoEnum(PM_SMEMA.enuModuleDo.OutPut_SMEMA).ToString(); }
                        else
                        { checkBox2.Text ="Null";  }

                        checkBox1.Width = this.Width - (checkBox1.Left * 2);
                        checkBox2.Width = this.Width - (checkBox2.Left * 2);
                        checkBox1.Height = (this.Height - (checkBox1.Top * 2)) / 2;
                        checkBox2.Height = (this.Height - (checkBox1.Top * 2)) / 2;
                        checkBox2.Top = this.Height - checkBox1.Height - checkBox1.Top;
                    }
                }
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
                    if (clsBaseProc.m_dctProcData[strSource] is PM_SMEMA)
                    {
                        PM_SMEMA pPM_SMEMA = (PM_SMEMA)clsBaseProc.m_dctProcData[strSource];
                        checkBox1.Checked = pPM_SMEMA.m_GetDiStage(PM_SMEMA.enuModuleDi.Sensor_SMEMA);
                        checkBox2.Checked = pPM_SMEMA.m_GetDoStage(PM_SMEMA.enuModuleDo.OutPut_SMEMA);
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

        /// <summary> 改變CheckBox顯示模式，打勾勾->全亮色 </summary>
        private void checkBox_Paint(object sender, PaintEventArgs e)
        {
            CheckBox Item = (CheckBox)sender;
            CheckState cs = Item.CheckState;
            if (cs == CheckState.Checked)
            {
                using (SolidBrush brush = new SolidBrush(Item.BackColor))
                {
                    e.Graphics.FillRectangle(brush, 0, 1, 14, 14);
                    e.Graphics.FillRectangle(Brushes.Green, 3, 4, 8, 8);
                    e.Graphics.DrawRectangle(Pens.Black, 0, 1, 13, 13);
                }
            }
        }
        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (clsArtSystem.bIsSoftwareSimulate == true)
            {
                if (clsBaseProc.m_dctProcData.ContainsKey(strSource) == true)
                {
                    if (clsBaseProc.m_dctProcData[strSource] is PM_SMEMA)
                    {
                        PM_SMEMA pPM_SMEMA = (PM_SMEMA)clsBaseProc.m_dctProcData[strSource];
                        if (pPM_SMEMA.m_GetDiEnum(PM_SMEMA.enuModuleDi.Sensor_SMEMA) != null)
                        {
                            clsEnum.enuDi eDI = (clsEnum.enuDi)pPM_SMEMA.m_GetDiEnum(PM_SMEMA.enuModuleDi.Sensor_SMEMA);
                            clsDioCtrl.SetDi(eDI, !clsDioCtrl.GetDi(eDI));
                        }
                    }
                }
            }
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (clsArtSystem.bIsSoftwareSimulate == true)
            {
                if (clsBaseProc.m_dctProcData.ContainsKey(strSource) == true)
                {
                    if (clsBaseProc.m_dctProcData[strSource] is PM_SMEMA)
                    {
                        PM_SMEMA pPM_SMEMA = (PM_SMEMA)clsBaseProc.m_dctProcData[strSource];
                        if (pPM_SMEMA.m_GetDoEnum(PM_SMEMA.enuModuleDo.OutPut_SMEMA) != null)
                        {
                            clsEnum.enuDo eDO = (clsEnum.enuDo)pPM_SMEMA.m_GetDoEnum(PM_SMEMA.enuModuleDo.OutPut_SMEMA);
                            clsDioCtrl.SetDo(eDO, !clsDioCtrl.GetDo(eDO));
                        }
                    }
                }
            }
        }
        #endregion



    }
}

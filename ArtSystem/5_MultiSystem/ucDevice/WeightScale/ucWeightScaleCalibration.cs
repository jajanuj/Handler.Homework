using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtControlLib;
using ArtCommonLib;
using ArtData;
using System.Reflection;

namespace ArtSystem.MultiSystem
{
    public partial class ucWeightScaleCalibration : ucBaseUserControl
    {
        #region //===================== Parameter 參數 =====================

        private int iRunStep = -1;
        private int iRepeatCount = 0;
        private int iDelayCount_100ms = 0;
        private clsCtrlWeightScale m_CtrlWeightScale = null;

        #endregion

        #region //===================== 必要函式設置 =====================

        static object m_LockObj = new object();
        static private ucWeightScaleCalibration m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucWeightScaleCalibration GetSingleton()
        {
            lock (m_LockObj)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucWeightScaleCalibration();
                }
            }
            return m_Singleton;
        }


        public ucWeightScaleCalibration()
        {
            InitializeComponent();
            initialSize = this.Size;
            ucArtMain_Design.GetSingleton()._CollectChangeLanguageControls(this);
        }

        public void UpdateControls()
        {

            try
            {
                iRepeatCount = 0;
                iRunStep = -1;
                Label_RemoveGlueCup.BackColor = Color.White;
                Label_ConfirmPlaceCover.BackColor = Color.White;
                Label_ResetZero.BackColor = Color.White;
                Label_Correction.BackColor = Color.White;
                Label_RepeatCount.BackColor = Color.White;
                btn_StartRun.Enabled = false;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        protected override void ReflashTimerFunc()
        {
            try
            {
                if (m_CtrlWeightScale != null)
                {

                    Label_RepeatCount.Text = clsLanguage.GetTranslation("Repeat Count", false) + " (" + iRepeatCount + "/" + num_WeightScale_Correction_AutoRunCount._Value + ")";
                    lblWeight.BackColor = m_CtrlWeightScale.Com_IsConnected() ? Color.Lime : Color.Red;
                    if (m_CtrlWeightScale.IsBusy() == true)
                    {
                        lblWeight.Text = "Waiting...";
                    }
                    else
                    {
                        lblWeight.Text = m_CtrlWeightScale.GetWeightResult().ToString();
                    }
                    if (lblWeight.Text.Contains("999") && iRunStep != 0)
                    {
                        lblWeight.Text = "Adjust Fail";
                        iRunStep = -1;
                        iRepeatCount = 0;
                    }
                    else if (m_CtrlWeightScale.IsBusy() == false
                        || m_CtrlWeightScale.bIsSimulatorMode == true)
                    {
                        switch (iRunStep)
                        {
                            case 0:
                                m_CtrlWeightScale.RunResetValue();
                                Label_ResetZero.BackColor = Color.Green;
                                Label_Correction.BackColor = Color.White;
                                Label_RepeatCount.BackColor = Color.White;
                                iDelayCount_100ms = 3;
                                iRunStep = 1;
                                break;

                            case 1:
                                iDelayCount_100ms--;
                                Label_ResetZero.BackColor = Color.Gray;
                                if (iDelayCount_100ms <= 0)
                                {
                                    iDelayCount_100ms = 0;
                                    iRunStep = 2;
                                }
                                break;

                            case 2:
                                m_CtrlWeightScale.SetCorrectionCmd();
                                Label_ResetZero.BackColor = Color.Gray;
                                Label_Correction.BackColor = Color.Green;
                                Label_RepeatCount.BackColor = Color.White;
                                iDelayCount_100ms = 3;
                                iRunStep = 3;
                                break;

                            case 3:
                                iDelayCount_100ms--;
                                Label_Correction.BackColor = Color.Gray;
                                if (iDelayCount_100ms <= 0)
                                {
                                    iDelayCount_100ms = 0;
                                    iRepeatCount++;
                                    if (iRepeatCount >= num_WeightScale_Correction_AutoRunCount._Value)
                                    {
                                        Label_RepeatCount.BackColor = Color.Gray;
                                        iRunStep = -1;
                                    }
                                    else
                                    {
                                        iRunStep = 0;
                                    }
                                }
                                break;


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //========== ShowForm 函式設置 ==========

        private Size initialSize = new Size();
        /// <summary> 使用Form顯示 </summary>
        public void _ShowForm(clsCtrlWeightScale p_CtrlWeightScale, bool Dialog = true)
        {
            m_CtrlWeightScale = p_CtrlWeightScale;
            this.UpdateControls();
            if (this.Parent != null && this.Parent is Form == true)
            {
                Form mForm = (Form)this.Parent;
                if (Dialog == true)
                {
                    mForm.ShowDialog();
                    mForm.BringToFront();
                }
                else
                {
                    mForm.Show();
                    mForm.BringToFront();
                }
            }
            else
            {
                Form mForm = new Form();
                mForm.WindowState = FormWindowState.Normal;
                mForm.Size = new Size(this.initialSize.Width + 16, this.initialSize.Height + 39);
                mForm.StartPosition = FormStartPosition.CenterScreen;
                mForm.Text = clsLanguage.GetTranslation(this.Name, false);
                mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
                mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
                this.Parent = mForm;
                this.SetReflashTimerStart(true);
                this.Dock = DockStyle.Fill;
                if (Dialog == true)
                {
                    mForm.ShowDialog();
                }
                else
                {
                    mForm.Show();
                }
            }
        }
        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.SetReflashTimerStart(false);
                this.Parent = null;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void mForm_Deactivate(object sender, EventArgs e)
        {
            try
            {
                //this.SetReflashTimerStart(false);
                //this.Parent = null;
                //Form mForm = (Form)sender;
                //mForm.Close();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion


        #region //===================== Public Function =====================

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        #endregion



        #region //===================== Event 事件觸發 =====================
        private void btn_ConfirmRemoveGlueCup_Click(object sender, EventArgs e)
        {
            try
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + m_CtrlWeightScale.g_sName + " -> " + ((Control)sender).Name + ", Clicked");
                Label_RemoveGlueCup.BackColor = Color.Gray;
                btn_StartRun.Enabled = Label_RemoveGlueCup.BackColor == Color.Gray && Label_ConfirmPlaceCover.BackColor == Color.Gray;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void btn_ConfirmPlaceCover_Click(object sender, EventArgs e)
        {
            try
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + m_CtrlWeightScale.g_sName + " -> " + ((Control)sender).Name + ", Clicked");
                Label_ConfirmPlaceCover.BackColor = Color.Gray;
                btn_StartRun.Enabled = Label_RemoveGlueCup.BackColor == Color.Gray && Label_ConfirmPlaceCover.BackColor == Color.Gray;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void btn_StartRun_Click(object sender, EventArgs e)
        {
            try
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + m_CtrlWeightScale.g_sName + " -> " + ((Control)sender).Name + ", Clicked");
                iRepeatCount = 0;
                iRunStep = 0;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            try
            {
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + m_CtrlWeightScale.g_sName + " -> " + ((Control)sender).Name + ", Clicked");
                if (m_CtrlWeightScale != null)
                {
                    iRunStep = -1;
                    iRepeatCount = 0;
                    m_CtrlWeightScale.RunCancelCmd();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void btn_ManualZero_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_CtrlWeightScale != null)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + m_CtrlWeightScale.g_sName + " -> " + ((Control)sender).Name + ", Clicked");
                    if (m_CtrlWeightScale.IsBusy() == false)
                    {
                        m_CtrlWeightScale.RunResetValue();
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        private void btn_ManualCalibration_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_CtrlWeightScale != null)
                {
                    clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " : " + m_CtrlWeightScale.g_sName + " -> " + ((Control)sender).Name + ", Clicked");
                    if (m_CtrlWeightScale.IsBusy() == false)
                    {
                        m_CtrlWeightScale.SetCorrectionCmd();
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void num_WeightScale_Correction_AutoRunCount_TextChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }


        #endregion

    }
}

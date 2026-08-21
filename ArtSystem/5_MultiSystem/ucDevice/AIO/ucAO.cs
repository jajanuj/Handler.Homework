using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtData;
using ArtCommonLib;
using ArtControlLib;

namespace ArtSystem.MultiSystem
{
    public partial class ucAO : UserControl
    {
        public double g_dOffset = 0;
        public double g_dGain = 0;
        public double g_dShift = 0;
        public clsEnum.enuDo? g_eChannelIndex = null;
        public clsPmtAIOTune mAIAOData = ucAIOTune.GetSingleton().mPmt;
        Dictionary<clsEnum.enuDi, string> Dic_DiName = clsDioMotion.GetDiName();

        public ucAO()
        {
            InitializeComponent();
            UpdateControls();
            this.initialSize = this.Size;
            this.mTimer.Tick += MTimer_Tick;
        }

        public void UpdateControls()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int iAiIndex = 0;
                foreach (clsEnum.enuDi sKey in mAIAOData.mDic_AIPmtValue.Keys)
                {
                    dataGridView1.Rows.Add();
                    if (Dic_DiName.ContainsKey(sKey) == true)
                    {
                        dataGridView1[dgvAIID.Index, iAiIndex].Value = sKey;
                        dataGridView1[dgvAIDes.Index, iAiIndex].Value = Dic_DiName[sKey];
                        dataGridView1[dgvAIValue.Index, iAiIndex].Value = clsDioCtrl.GetAi(sKey).ToString("F3"); ;
                        iAiIndex += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }


        public void ReflashFunc()
        {
            try
            {
                if (g_eChannelIndex != null)
                {
                    tb_ChannelIndex.Text = g_eChannelIndex.ToString();
                }
                else
                {
                    tb_ChannelIndex.Text = "null";
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void MTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                ReflashFunc();
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #region //========== ShowForm 函式設置 ==========
        DialogResult mFormResult = DialogResult.No;
        private Size initialSize = new Size();
        private Timer mTimer = new Timer();
        /// <summary> 使用Form顯示 </summary>
        public DialogResult _ShowForm(bool Dialog = true)
        {
            Form mForm = new Form();
            mForm.WindowState = FormWindowState.Normal;
            mForm.ClientSize = this.initialSize;
            mForm.StartPosition = FormStartPosition.CenterScreen;
            mForm.Text = clsLanguage.GetTranslation(this.Name, false);
            mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
            mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
            this.Parent = mForm;
            this.Dock = DockStyle.Fill;
            this.mTimer.Interval = 100;
            this.mTimer.Enabled = true;
            this.mFormResult = DialogResult.No;
            if (Dialog == true)
            {
                mForm.ShowDialog();
            }
            else
            {
                mForm.Show();
            }
            return this.mFormResult;
        }
        private void mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.mTimer.Enabled = false;
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
        private void button1_Click(object sender, EventArgs e)
        {
            short value = 0;
            if (short.TryParse(tb_SettingValue1.Text, out value))
            {
                if (g_eChannelIndex != null)
                {
                    clsDioCtrl.SetAo((clsEnum.enuDo)g_eChannelIndex, value, true);
                }
            }
            else
            {
                MessageBox.Show("Data Setting Value 1 Error");
            }
            UpdateControls();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            short value = 0;
            if (short.TryParse(tb_SettingValue2.Text, out value))
            {
                if (g_eChannelIndex != null)
                {
                    clsDioCtrl.SetAo((clsEnum.enuDo)g_eChannelIndex, value, true);
                }
            }
            else
            {
                MessageBox.Show("Data Setting Value 2 Error");
            }
            UpdateControls();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                double HV1 = double.Parse(tb_HeadValue1.Text);
                double HV2 = double.Parse(tb_HeadValue2.Text);
                double SV1 = double.Parse(tb_SettingValue1.Text);
                double SV2 = double.Parse(tb_SettingValue2.Text);

                double Gain = (HV2 - HV1) / (SV2 - SV1);
                tb_Gain.Text = Gain.ToString("F8");

                int Offset = (int)Math.Round(HV2 - SV2 * Gain, 0, MidpointRounding.AwayFromZero);
                tb_Offset.Text = Offset.ToString();

                double Shift = SV2 - (HV2 - (double)Offset) / Gain;
                tb_Shift.Text = Shift.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Data1、Data2 異常!");
                MessageBox.Show(ex.Message);
            }
            UpdateControls();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                double Gain = double.Parse(tb_Gain.Text);
                int Offset = int.Parse(tb_Offset.Text);
                double Shift = double.Parse(tb_Shift.Text);
                double NowValu = double.Parse(tb_NowValue.Text);
                if (g_eChannelIndex != null)
                {
                    clsDioCtrl.SetAoPara((clsEnum.enuDo)g_eChannelIndex, Gain, Offset, Shift);
                    clsDioCtrl.SetAo((clsEnum.enuDo)g_eChannelIndex, NowValu, false);
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateControls();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                g_dGain = double.Parse(tb_Gain.Text);
                g_dOffset = double.Parse(tb_Offset.Text);
                g_dShift = double.Parse(tb_Shift.Text);
                mFormResult = DialogResult.OK;
                if (this.Parent != null)
                {
                    if (this.Parent is Form)
                    {
                        Form mForm = (Form)this.Parent;
                        mForm.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateControls();
        }

    }
}

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
    public partial class ucAI : UserControl
    {
        public double g_dOffset = 0;
        public double g_dGain = 0;
        public double g_dShift = 0;
        public clsEnum.enuDi? g_eChannelIndex = null;
        public clsPmtAIOTune mAIAOData = ucAIOTune.GetSingleton().mPmt;
        Dictionary<clsEnum.enuDo, string> Dic_DoName = clsDioMotion.GetDoName();

        public ucAI() 
        {
            InitializeComponent();
            UpdateControls();
            initialSize = this.Size;
            this.mTimer.Tick += MTimer_Tick;
        }


        public void UpdateControls()
        {
            try
            {
                dataGridView1.Rows.Clear();
                int iAoIndex = 0;
                foreach (clsEnum.enuDo sKey in mAIAOData.mDic_AOPmtValue.Keys)
                {
                    dataGridView1.Rows.Add();
                    if (Dic_DoName.ContainsKey(sKey) == true)
                    {
                        dataGridView1[dgvAOID.Index, iAoIndex].Value = sKey;
                        dataGridView1[dgvAODes.Index, iAoIndex].Value = Dic_DoName[sKey];
                        dataGridView1[dgvAOValue.Index, iAoIndex].Value = clsDioCtrl.GetAoSetValue(sKey).ToString("F3");
                        iAoIndex += 1;
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
                tb_ChannelIndex.Text = g_eChannelIndex.ToString();
                if (g_eChannelIndex != null)
                {

                    tb_ChannelIndex.Text = g_eChannelIndex.ToString();
                    tb_OriginValue.Text = clsDioCtrl.GetAi((clsEnum.enuDi)(g_eChannelIndex), 1, true).ToString();
                    tb_NowValue.Text = clsDioCtrl.GetAi((clsEnum.enuDi)(g_eChannelIndex), 1, false).ToString();
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
            tb_SampleValue1.Text = tb_OriginValue.Text;
            UpdateControls();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            tb_SampleValue2.Text = tb_OriginValue.Text;
            UpdateControls();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                double HV1 = double.Parse(tb_HeadValue1.Text);
                double HV2 = double.Parse(tb_HeadValue2.Text);
                double SV1 = double.Parse(tb_SampleValue1.Text);
                double SV2 = double.Parse(tb_SampleValue2.Text);

                double Gain = (SV2 - SV1) / (HV2 - HV1);
                tb_Gain.Text = Gain.ToString("F8");

                int Offset = (int)Math.Round(SV2 - HV2 * Gain, 0, MidpointRounding.AwayFromZero);
                tb_Offset.Text = Offset.ToString();

                double Shift = HV2 - (SV2 - (double)Offset) / Gain;
                tb_Shift.Text = Shift.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Data1、Data2 異常!");
                MessageBox.Show(ex.Message);
            }

            UpdateControls();
        }

        private void btnSavePmt_AO_Click(object sender, EventArgs e)
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
                ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)g_eChannelIndex][clsPmtAIOTune.enuPmtName.Gain] = tb_Gain.Text.ToString();
                ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)g_eChannelIndex][clsPmtAIOTune.enuPmtName.Offset] = tb_Offset.Text.ToString();
                ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)g_eChannelIndex][clsPmtAIOTune.enuPmtName.Shift] = tb_Shift.Text.ToString();
                ucAIOTune.GetSingleton().mPmt.Save(ucAIOTune.GetSingleton().mPmt.sINIPath);
                ucAIOTune.GetSingleton().mPmt.SetAIOPmt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateControls();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int iSelectRecipeIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (iSelectRecipeIndex >= 0)
                    {
                        if (dataGridView1.HitTest(e.X, e.Y).ColumnIndex == 2)
                        {
                            clsEnum.enuDo eSelectAO = (clsEnum.enuDo)dataGridView1[dgvAOID.Index, iSelectRecipeIndex].Value;
                            if (FormNumBox.GetSingleton().ShowDialog(this, eSelectAO.ToString(), 4095, 0, 3) == DialogResult.OK)
                            {
                                clsDioCtrl.SetAo((clsEnum.enuDo)eSelectAO, FormNumBox.GetSingleton().NumBoxValue, true);
                                UpdateControls();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

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
using ArtSystem;

namespace ArtSystem.MultiSystem
{
    public partial class ucCtrlAIOTune : ArtCommonLib.ucBaseUserControl
    {
        clsEnum.enuDi? eAI = null;
        clsEnum.enuDo? eAO = null;
        private ucAI mTuneAI = null;
        private ucAO mTuneAO = null;

        #region//========== 必要函式 ==========
        public ucCtrlAIOTune()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            ucParameter.Add(this);
        }
        public void UpdateControls()
        {
            try
            {
                string tAIOName = "";
                if (eAI != null)
                {
                    clsEnum.enuDi eAI_ID = (clsEnum.enuDi)eAI;
                    Dictionary<clsEnum.enuDi, string> Dic_DiName = clsDioMotion.GetDiName();
                    if (Dic_DiName.ContainsKey(eAI_ID) == true)
                    {
                        tAIOName = Dic_DiName[eAI_ID];
                    }
                    else
                    {
                        tAIOName = clsLanguage.GetTranslation(eAI_ID.ToString());
                    }
                    txtID.Text = ToString(eAI);
                    txtName.BackColor = Color.Navy;
                    txtName.Text = clsLanguage.GetTranslation(tAIOName);
                }
                if (eAO != null)
                {
                    clsEnum.enuDo eAO_ID = (clsEnum.enuDo)eAO;
                    Dictionary<clsEnum.enuDo, string> Dic_DoName = clsDioMotion.GetDoName();
                    if (Dic_DoName.ContainsKey(eAO_ID) == true)
                    {
                        tAIOName = Dic_DoName[eAO_ID];
                    }
                    else
                    {
                        tAIOName = clsLanguage.GetTranslation(eAO_ID.ToString());
                    }
                    txtID.Text = ToString(eAO);
                    txtName.BackColor = Color.Maroon;
                    txtName.Text = clsLanguage.GetTranslation(tAIOName);
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
                if (eAI != null)
                {
                    numBox_Pressure.Text = clsDioCtrl.GetAi((clsEnum.enuDi)eAI).ToString("F3");
                    if (ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue.ContainsKey((clsEnum.enuDi)eAI) == true)
                    {
                        numBox_Offset.Text = ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Offset];
                        numBox_Gain.Text = ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Gain];
                        numBox_Shift.Text = ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Shift];
                    }
                }
                if (eAO != null)
                {
                    numBox_Pressure.Text = clsDioCtrl.GetAoSetValue((clsEnum.enuDo)eAO).ToString("F3");
                    if (ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue.ContainsKey((clsEnum.enuDo)eAO) == true)
                    {
                        numBox_Offset.Text = ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Offset].ToString();
                        numBox_Gain.Text = ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Gain].ToString();
                        numBox_Shift.Text = ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Shift].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion

        #region//========== Public Function ==========

        public void SetAIO(clsEnum.enuDi p_AIID)
        {
            eAI = p_AIID;
            eAO = null;

        }


        public void SetAIO(clsEnum.enuDo p_AOID)
        {
            eAO = p_AOID;
            eAI = null;

        }
        #endregion


        #region//========== Private Function ==========

        private string ToString(clsEnum.enuDo? sender)
        {
            string rValue = "";
            try
            {
                if (sender != null)
                {
                    int iDO = (int)sender;
                    rValue = "DO" + iDO.ToString("000");
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }
        private string ToString(clsEnum.enuDi? sender)
        {
            string rValue = "";
            try
            {
                if (sender != null)
                {
                    int iDO = (int)sender;
                    rValue = "DI" + iDO.ToString("000");
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
            return rValue;
        }

        #endregion

        #region//========== Event ==========
        private void numBox_Pressure_Click(object sender, EventArgs e)
        {
            try
            {
                if (eAO != null)
                {
                    if (FormNumBox.GetSingleton().ShowDialog(this, numBox_Pressure.Text, 999, -999, 3) == DialogResult.OK)
                    {
                        clsDioCtrl.SetAo((clsEnum.enuDo)eAO, FormNumBox.GetSingleton().NumBoxValue);
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void numBox_Offset_Click(object sender, EventArgs e)
        {
            try
            {
                if (FormNumBox.GetSingleton().ShowDialog(this, numBox_Offset.Text, 9999999999999, -9999999999999, 9) == DialogResult.OK)
                {
                    if (eAI != null)
                    {
                        if (ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue.ContainsKey((clsEnum.enuDi)eAI) == true)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + "," + this.Name + "-" + ((Control)sender).Name
                                + ", AI ID = " + eAI.ToString() + ", AI Name = " + txtName.Text
                                + ", Offset = " + ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Offset].ToString()
                                + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString("0.############"));
                            ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Offset] = FormNumBox.GetSingleton().NumBoxValue.ToString("0.############");
                            ucAIOTune.GetSingleton().mPmt.Save(ucAIOTune.GetSingleton().mPmt.sINIPath);
                            ucAIOTune.GetSingleton().mPmt.SetAIOPmt();
                        }
                    }
                    if (eAO != null)
                    {
                        if (ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue.ContainsKey((clsEnum.enuDo)eAO) == true)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + "," + this.Name + "-" + ((Control)sender).Name
                                + ", AO ID = " + eAO.ToString() + ", AO Name = " + txtName.Text
                                + ", Offset = " + ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Offset].ToString()
                                + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString("0.############"));
                            ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Offset] = FormNumBox.GetSingleton().NumBoxValue.ToString("0.############");
                            ucAIOTune.GetSingleton().mPmt.Save(ucAIOTune.GetSingleton().mPmt.sINIPath);
                            ucAIOTune.GetSingleton().mPmt.SetAIOPmt();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void numBox_Gain_Click(object sender, EventArgs e)
        {
            try
            {
                if (FormNumBox.GetSingleton().ShowDialog(this, numBox_Gain.Text, 9999999999999, -9999999999999, 9) == DialogResult.OK)
                {
                    if (eAI != null)
                    {
                        if (ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue.ContainsKey((clsEnum.enuDi)eAI) == true)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + "," + this.Name + "-" + ((Control)sender).Name
                                + ", AI ID = " + eAI.ToString() + ", AI Name = " + txtName.Text
                                + ", Gain = " + ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Gain].ToString()
                                + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString("0.############"));
                            ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Gain] = FormNumBox.GetSingleton().NumBoxValue.ToString("0.############");
                            ucAIOTune.GetSingleton().mPmt.Save(ucAIOTune.GetSingleton().mPmt.sINIPath);
                            ucAIOTune.GetSingleton().mPmt.SetAIOPmt();
                        }
                    }
                    if (eAO != null)
                    {
                        if (ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue.ContainsKey((clsEnum.enuDo)eAO) == true)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + "," + this.Name + "-" + ((Control)sender).Name
                                + ", AO ID = " + eAO.ToString() + ", AO Name = " + txtName.Text
                                + ", Gain = " + ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Gain].ToString()
                                + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString("0.############"));
                            ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Gain] = FormNumBox.GetSingleton().NumBoxValue.ToString("0.############");
                            ucAIOTune.GetSingleton().mPmt.Save(ucAIOTune.GetSingleton().mPmt.sINIPath);
                            ucAIOTune.GetSingleton().mPmt.SetAIOPmt();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void numBox_Shift_Click(object sender, EventArgs e)
        {
            try
            {
                if (FormNumBox.GetSingleton().ShowDialog(this, numBox_Shift.Text, 9999999999999, -9999999999999, 12) == DialogResult.OK)
                {
                    if (eAI != null)
                    {
                        if (ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue.ContainsKey((clsEnum.enuDi)eAI) == true)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + "," + this.Name + "-" + ((Control)sender).Name
                                + ", AI ID = " + eAI.ToString() + ", AI Name = " + txtName.Text
                                + ", Shift = " + ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Shift].ToString()
                                + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString("0.############"));
                            ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Shift] = FormNumBox.GetSingleton().NumBoxValue.ToString("0.############");
                            ucAIOTune.GetSingleton().mPmt.Save(ucAIOTune.GetSingleton().mPmt.sINIPath);
                            ucAIOTune.GetSingleton().mPmt.SetAIOPmt();
                        }
                    }
                    if (eAO != null)
                    {
                        if (ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue.ContainsKey((clsEnum.enuDo)eAO) == true)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + "," + this.Name + "-" + ((Control)sender).Name
                                + ", AO ID = " + eAO.ToString() + ", AO Name = " + txtName.Text
                                + ", Shift = " + ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Shift].ToString()
                                + " -> " + FormNumBox.GetSingleton().NumBoxValue.ToString("0.############"));
                            ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Shift] = FormNumBox.GetSingleton().NumBoxValue.ToString("0.############");
                            ucAIOTune.GetSingleton().mPmt.Save(ucAIOTune.GetSingleton().mPmt.sINIPath);
                            ucAIOTune.GetSingleton().mPmt.SetAIOPmt();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        private void btnAIOTune_Click(object sender, EventArgs e)
        {
            try
            {
                if (eAI != null)
                {
                    if (ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue.ContainsKey((clsEnum.enuDi)eAI) == true)
                    {
                        if (mTuneAI == null)
                        { mTuneAI = new ucAI(); }
                        mTuneAI.g_eChannelIndex = eAI;
                        mTuneAI.g_dOffset = Convert.ToDouble(ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Offset]);
                        mTuneAI.g_dGain = Convert.ToDouble(ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Gain]);
                        mTuneAI.g_dShift = Convert.ToDouble(ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Shift]);
                        if (mTuneAI._ShowForm() == DialogResult.OK)
                        {
                            ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Offset] = mTuneAI.g_dOffset.ToString(("0.############"));
                            ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Gain] = mTuneAI.g_dGain.ToString(("0.############"));
                            ucAIOTune.GetSingleton().mPmt.mDic_AIPmtValue[(clsEnum.enuDi)eAI][clsPmtAIOTune.enuPmtName.Shift] = mTuneAI.g_dShift.ToString(("0.############"));
                        }
                    }
                }
                if (eAO != null)
                {
                    if (mTuneAO == null)
                    { mTuneAO = new ucAO(); }
                    mTuneAO.g_eChannelIndex = eAO;
                    mTuneAO.g_dOffset = Convert.ToDouble(ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Offset]);
                    mTuneAO.g_dGain = Convert.ToDouble(ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Gain]);
                    mTuneAO.g_dShift = Convert.ToDouble(ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Shift]);
                    if (mTuneAO._ShowForm() == DialogResult.OK)
                    {
                        ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Offset] = mTuneAO.g_dOffset.ToString(("0.############"));
                        ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Gain] = mTuneAO.g_dGain.ToString(("0.############"));
                        ucAIOTune.GetSingleton().mPmt.mDic_AOPmtValue[(clsEnum.enuDo)eAO][clsPmtAIOTune.enuPmtName.Shift] = mTuneAO.g_dShift.ToString(("0.############"));
                    }
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

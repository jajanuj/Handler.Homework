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
using ArtTeach;
using System.Reflection;

namespace ArtEQ
{
    public partial class ucParameterControl : ucBaseUserControl
    {

        #region //=====================  區域變數設置 =====================

        private int iOrgWidth = 0;
        private int iOrgHeight = 0;
        private float fOrgScale = 0;
        private Dictionary<PmtGroup, List<clsEnum.enuPmtName>> mGroupPmt = new Dictionary<PmtGroup, List<clsEnum.enuPmtName>>();
        private List<clsEnum.enuPosName> mGroupPos = new List<clsEnum.enuPosName>();
        #endregion

        #region //=====================  Enum 列舉 =====================

        private enum PmtGroup
        {
            [Description("Parameter (System)")]
            Sys,
            [Description("Parameter (Recipe)")]
            Rec,
            [Description("File Path (Recipe)")]
            rPath,
            [Description("File Path (System)")]
            sPath,
            [Description("Delay (System)")]
            Sys_Delay,
            [Description("Timeout (System)")]
            Sys_Timeout,
            [Description("Position (TeachPos)")]
            TeachPos,
            [Description("Undifine")]
            Undifine,
        }

        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucParameterControl m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucParameterControl GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucParameterControl();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucParameterControl()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            if (iOrgWidth == 0)
            {
                iOrgHeight = this.Height;
                iOrgWidth = this.Width;
                fOrgScale = 1;
            }
            this.TimerInterval = 100;
            InitialTabPageTitle();
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                if (this.Parent != null)
                {
                    this.Scale(new SizeF(1 / fOrgScale, 1 / fOrgScale));
                    float wScale = (float)this.Parent.Width / (float)iOrgWidth;
                    float hScale = (float)this.Parent.Height / (float)iOrgHeight;
                    if (wScale != 0 && hScale != 0)
                    {
                        fOrgScale = wScale;
                        if (fOrgScale > hScale)
                        { fOrgScale = hScale; }
                        this.Scale(new SizeF(fOrgScale, fOrgScale));
                    }
                    else
                    {
                        fOrgScale = 1;
                    }
                }
                UpdatePmtToUI();
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
                
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        private void ucParameterControl_VisibleChanged(object sender, EventArgs e)
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

        private void InitialTabPageTitle()
        {
            for (int i = tabPmtType.TabPages.Count; i < Enum.GetValues(typeof(PmtGroup)).Length; i++)
            {
                tabPmtType.TabPages.Add(new TabPage("Item" + i));
            }
            int TabPageIndex = 0;
            foreach (PmtGroup pType in Enum.GetValues(typeof(PmtGroup)))
            {
                mGroupPmt.Add(pType, new List<clsEnum.enuPmtName>());
                tabPmtType.TabPages[TabPageIndex].Text = GetDescription(pType);
                TabPageIndex++;
            }
            foreach (clsEnum.enuPmtName PmtName in Enum.GetValues(typeof(clsEnum.enuPmtName)))
            {
                bool bfound = false;
                foreach (PmtGroup pType in Enum.GetValues(typeof(PmtGroup)))
                {
                    if (pType != PmtGroup.TeachPos)
                    {
                        if (PmtName.ToString().Contains(pType.ToString()) == true)
                        {
                            mGroupPmt[pType].Add(PmtName);
                            bfound = true;
                        }
                        if (pType == PmtGroup.Sys_Timeout)
                        {
                            if (PmtName.ToString().Contains("Timeout") == true)
                            {
                                mGroupPmt[pType].Add(PmtName);
                                bfound = true;
                            }
                        }
                    }
                }
                if (bfound == false)
                {
                    mGroupPmt[PmtGroup.Undifine].Add(PmtName);
                }
            }
            mGroupPos.Clear();
            foreach (clsEnum.enuPosName PmtName in Enum.GetValues(typeof(clsEnum.enuPosName)))
            {
                mGroupPos.Add(PmtName);
            }
        }
        private void UpdatePmtToUI()
        {
            int Index = tabPmtType.SelectedIndex;
            if (Index < 0)
            {
                dataGridView1.Rows.Clear();
            }
            else
            {
                PmtGroup pGroup = (PmtGroup)Index;
                dataGridView1.Rows.Clear();
                int RowIndex = 0;
                if (pGroup == PmtGroup.TeachPos)
                {
                    foreach (clsEnum.enuPosName pPmtName in mGroupPos)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1[0, RowIndex].Value = pPmtName.ToString("d");
                        dataGridView1[1, RowIndex].Value = pPmtName.ToString();
                        if (clsLanguage.GetTranslation(pPmtName.ToString()) != pPmtName.ToString())
                        { dataGridView1[2, RowIndex].Value = clsLanguage.GetTranslation(pPmtName.ToString()); }
                        else
                        { dataGridView1[2, RowIndex].Value = clsLanguage.GetTranslation(GetDescription(pPmtName)); }
                        dataGridView1[3, RowIndex].Value = ucPosPmt.GetValueString(pPmtName);
                        RowIndex++;
                    }
                }
                else
                {
                    foreach (clsEnum.enuPmtName pPmtName in mGroupPmt[pGroup])
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1[0, RowIndex].Value = pPmtName.ToString("d");
                        dataGridView1[1, RowIndex].Value = pPmtName.ToString();
                        if (clsLanguage.GetTranslation(pPmtName.ToString()) != pPmtName.ToString())
                        { dataGridView1[2, RowIndex].Value = clsLanguage.GetTranslation(pPmtName.ToString()); }
                        else
                        { dataGridView1[2, RowIndex].Value = clsLanguage.GetTranslation(GetDescription(pPmtName)); }
                        dataGridView1[3, RowIndex].Value = ucParameter.GetValueString(pPmtName);
                        RowIndex++;
                    }
                }
            }
        }
        private string GetDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
        private PmtGroup? GetCurrentPmtGroup()
        {
            PmtGroup? rValue = null;
            int Index = tabPmtType.SelectedIndex;
            if (Enum.IsDefined(typeof(PmtGroup), Index) == true)
            {
                rValue = (PmtGroup)Index;
            }
            return rValue;
        }

        #endregion

        #region//===================== 以下為事件處理 =====================

        private void tabPmtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }
        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Width = (dataGridView1.Width - 50) / dataGridView1.Columns.Count;
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView mDgv = (DataGridView)sender;
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (mDgv.CurrentCell is DataGridViewTextBoxCell) //Desired Column
            {
                PmtGroup? CurrentGroup = GetCurrentPmtGroup();
                if (CurrentGroup == null
                    || CurrentGroup == PmtGroup.sPath
                    || CurrentGroup == PmtGroup.rPath)
                {
                    return;
                }
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar!= '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.')
            {
                if (sender is TextBox)
                {
                    string strSender = ((TextBox)sender).Text;
                    if (strSender.Contains(".") == true)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            PmtGroup? CurrentGroup = GetCurrentPmtGroup();
            if (CurrentGroup != null)
            {
                string sPmtName = dataGridView1[PmtName.Index, e.RowIndex].Value.ToString();
                string sPmtValue = "";
                if(dataGridView1[Value.Index, e.RowIndex].Value != null)
                {
                    sPmtValue = dataGridView1[Value.Index, e.RowIndex].Value.ToString();
                }
                if (CurrentGroup == PmtGroup.TeachPos)
                {
                    if (Enum.IsDefined(typeof(clsEnum.enuPosName), sPmtName) == true)
                    {
                        clsEnum.enuPosName ePmtName = (clsEnum.enuPosName)Enum.Parse(typeof(clsEnum.enuPosName), sPmtName);
                        ucPosPmt.SaveValue(ePmtName, sPmtValue);
                    }
                    else
                    {
                        UpdatePmtToUI();
                    }
                }
                else
                {
                    if (Enum.IsDefined(typeof(clsEnum.enuPmtName), sPmtName) == true)
                    {
                        clsEnum.enuPmtName ePmtName = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), sPmtName);
                        switch (CurrentGroup)
                        {
                            case PmtGroup.Sys:
                            case PmtGroup.sPath:
                            case PmtGroup.Sys_Delay:
                            case PmtGroup.Sys_Timeout:
                                ucParameter.SaveValue(clsEnum.enuPmtType.System, ePmtName, sPmtValue);
                                break;
                            case PmtGroup.Rec:
                            case PmtGroup.rPath:
                                ucParameter.SaveValue(clsEnum.enuPmtType.Recipe, ePmtName, sPmtValue);
                                break;
                            case PmtGroup.Undifine:
                                formMessageBox.Show("Undefine Can't Save Pmt.");
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        UpdatePmtToUI();
                    }
                }
            }
        }

        #endregion
    }
}

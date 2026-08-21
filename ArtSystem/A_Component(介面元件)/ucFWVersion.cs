using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Windows.Forms;
using ArtCommonLib;
using Shell32;


namespace ArtSystem
{
    public partial class ucFWVersion : ucBaseUserControl
    {
        #region //========== 變數設置 ========== 


        #endregion

        #region //========== Class 定義 ========== 


        #endregion

        #region //========== Enum 定義 ========== 


        #endregion

        #region //==========  必要函式設置 ========== 

        static private object objLock = new object();
        static private ucFWVersion m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucFWVersion GetSingleton()
        {
            lock (objLock)
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new ucFWVersion();
                }
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucFWVersion()
        {
            InitializeComponent();
            if (ArtSystem.clsArtSystem.bIsProgramOpen == false)
            {
                return;
            }
            this.VisibleChanged += new EventHandler(UserControl_VisibleChanged);
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                Dictionary<string, clsDeviceReport> LstReport = clsDeviceReport.GetAllReport();
                for(int i=dataGridView1.RowCount;i<LstReport.Count;i++)
                { dataGridView1.Rows.Add(); }
                for (int i = dataGridView1.RowCount - 1; i >= LstReport.Count; i--)
                { dataGridView1.Rows.RemoveAt(i);   }
                int iRow= 0;
                foreach (string sKey in LstReport.Keys)
                {
                    dataGridView1[dgvDeviceName.Index, iRow].Value = LstReport[sKey].DeviceName;
                    dataGridView1[dgvDeviceType.Index, iRow].Value = LstReport[sKey].DeviceType;
                    dataGridView1[dgvFWVersion.Index, iRow].Value = LstReport[sKey].FwVersion;
                    dataGridView1[dgvHWVersion.Index, iRow].Value = LstReport[sKey].HwVersion;
                    dataGridView1[dgvExtralInfo.Index, iRow].Value = LstReport[sKey].ExtraInfo;
                    dataGridView1[dgvFileName.Index, iRow].Value = System.IO.Path.GetFileNameWithoutExtension(sKey);
                    iRow++;
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
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
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 進入此介面時,自動執行UpdateControls </summary>
        protected void UserControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                UpdateControls();
            }
        }


        #endregion

        #region //========== ShowForm 函式設置 ==========
        private Form mForm = null;
        private Control m_OrgParent = null;
        private Size initialSize = new Size();
        /// <summary> 使用Form顯示 </summary>
        public void _ShowForm(bool Dialog = true)
        {
            if (mForm == null)
            {
                mForm = new Form();
                mForm.WindowState = FormWindowState.Normal;
                mForm.ClientSize = this.initialSize;
                mForm.StartPosition = FormStartPosition.CenterScreen;
                mForm.Text = clsLanguage.GetTranslation(this.Name, false);
                mForm.FormClosing += new FormClosingEventHandler(mForm_FormClosing);
                mForm.Deactivate += new EventHandler(mForm_Deactivate);//Lost Focus自動Close Form
                this.m_OrgParent = this.Parent;
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
                this.Parent = this.m_OrgParent;
                this.mForm = null;
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
                if (this.mForm != null)
                {
                    Form mForm = this.mForm;
                    this.SetReflashTimerStart(false);
                    this.Parent = this.m_OrgParent;
                    this.mForm = null;
                    mForm.Close();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        #endregion


        #region//========== 事件處理 ========== 

        #endregion
    }
}

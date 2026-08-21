using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtControlLib;
using ArtCommonLib;
using ArtData;
using ArtSystem;

namespace ArtEQ
{
    public partial class ucRecipe : ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================

        #endregion

        #region //=====================  必要函式設置 =====================

        static private ucRecipe m_Singleton;
        /// <summary> 取得唯一物件，避免重覆設置  </summary>
        static public ucRecipe GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucRecipe();
            }
            return m_Singleton;
        }

        /// <summary> 建構式 </summary>
        public ucRecipe()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            this.TimerInterval = 100;
        }

        /// <summary> 物件重置 </summary>
        public void UpdateControls()
        {
            try
            {
                ucParameter.GetSingleton().Parent = tPage_ucParameter;
                ucParameter.GetSingleton().Location = new Point(0, 0);
                ucParameter.GetSingleton().Size = tPage_ucParameter.Size;

                ucRecipeManager.GetSingleton().Parent = tPage_ucRecipeManage;
                ucRecipeManager.GetSingleton().Location = new Point(0, 0);
                ucRecipeManager.GetSingleton().Size = tPage_ucRecipeManage.Size;
                ucRecipeManager.GetSingleton().UpdateControls();
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
                if (ucRecipeManager.GetSingleton().Parent == null)
                {
                    UpdateControls();
                }
            }
            catch (Exception ex)
            {
                clsLog.Log(clsEnum.enuLogName.CatchLog, "Source : " + ex.Source + " , StackTrace : " + ex.StackTrace + ", Message : " + ex.Message);
            }
        }

        #endregion

        #region //===================== public 函式設置 =====================

        #endregion

        #region//===================== 以下為事件處理 (SizeChanged, VisibleChanged) =====================

        private void ucRecipe_SizeChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }
        private void ucRecipe_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                UpdateControls();
            }
        }

        #endregion
    }
}

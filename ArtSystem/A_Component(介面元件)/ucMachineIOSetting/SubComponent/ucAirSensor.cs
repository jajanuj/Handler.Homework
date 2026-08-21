using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtSystem;

namespace ArtSystem
{
    public partial class ucAirSensor : ucBaseUserControl2
    {
        #region //===================== 變數設置 =====================

        private Dictionary<string, ucBaseUserControl> Dic_RegisterUC = new Dictionary<string, ucBaseUserControl>();
        private List<TabPage> Lst_TabPage = new List<TabPage>();

        #endregion

        #region //===================== 必要函式設置 =====================

        //static private object objLock = new object();
        //static private _ucTapControl m_Singleton;
        ///// <summary> 取得唯一物件，避免重覆設置  </summary>
        //static public _ucTapControl GetSingleton()
        //{
        //    lock (objLock)
        //    {
        //        if (m_Singleton == null)
        //        {
        //            m_Singleton = new _ucTapControl();
        //        }
        //    }
        //    return m_Singleton;
        //}

        /// <summary> 建構式 </summary>
        public ucAirSensor()
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
            try
            {
                if (this.Visible == true)
                {
                    this.UpdateControls();
                }
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region//===================== Public 函式 =====================



        #endregion

        #region//===================== Private 函式 =====================
        #endregion

        #region//===================== 事件處理 =====================


        #endregion
    }
}

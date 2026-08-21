using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArtCommonLib;
using ArtControlLib;
using ArtData;

namespace ArtEQ
{
    public partial class ucTimeSetting : ArtCommonLib.ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        static ucTimeSetting m_Singleton;

        #endregion

        #region //=====================  必要函式設置 =====================
        /// <summary>
        /// 取得唯一物件，避免重覆設置
        /// </summary>
        /// <returns>回傳物件</returns>
        public static ucTimeSetting GetSingleton()
        {
            if (m_Singleton == null)
            {
                m_Singleton = new ucTimeSetting();
            }
            return m_Singleton;
        }

        /// <summary>
        /// 物件建立請利用 GetSingleton()，除非特殊需求
        /// </summary>
        private ucTimeSetting()
        {
            InitializeComponent();
            this.UpdateControls();
            ucParameter.Add(this);
        }

        /// <summary>
        /// 物件重置
        /// </summary>
        public void UpdateControls()
        {
            TimeOutControlUpdate();
            DelayControlUpdate();
        }

        /// <summary>
        /// 自動更新介面參數
        /// </summary>
        protected override void ReflashTimerFunc()
        {
        }

        #endregion

        #region //===================== public 函式設置 =====================

        #endregion

        #region //===================== private 函式設置 =====================

        private void DelayControlUpdate()
        {
            string PmtName = "";
            int Half = 0;
            int bHalf = 0;
            ListBox lPmtName = new ListBox();
            int DelayCount = 0;
            //for (int i = 0; i < Enum.GetValues(typeof(clsEnum.enuPmtName)).Length; i++)
            //{
            //    PmtName = Enum.GetValues(typeof(clsEnum.enuPmtName)).GetValue(i).ToString();
            foreach (clsEnum.enuPmtName ePmtName in Enum.GetValues(typeof(clsEnum.enuPmtName)))
            {
                PmtName = ePmtName.ToString();
                if (PmtName.Contains("Delay_"))
                {
                    lPmtName.Items.Add(PmtName);
                }

            }
            Half = (lPmtName.Items.Count+1) / 2;
            for (int i = 0; i < lPmtName.Items.Count; i++)
            {
                PmtName = lPmtName.Items[i].ToString();
                //if (PmtName.Contains("Delay_"))
                {
                    //if (i >= Half)
                    //{
                    //    bHalf = 1;
                    //}
                    ArtControlLib.comNumBox NumBox = new ArtControlLib.comNumBox();
                    NumBox._DefaultValue = 0;
                    NumBox._IsSaveToIni = true;
                    NumBox._IsSaveToLog = true;
                    NumBox._IsShowPopForm = true;
                    NumBox._Maximum = 10000;
                    NumBox._Minimum = 0;
                    NumBox._PmtName = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), PmtName);
                    NumBox._PmtType = clsEnum.enuPmtType.System;
                    NumBox._Value = 0;
                    NumBox.Size = new System.Drawing.Size(100, 20);
                    NumBox.Left = 250 + 40 + bHalf * (250 + 140);
                    NumBox.Top = 40 + (DelayCount - bHalf*Half) * 30;

                    Label item = new Label();
                    item.Name = "Delay_" + i.ToString();
                    item.Text = PmtName;
                    item.AutoSize = false;
                    item.Size = new System.Drawing.Size(250, 20);
                    item.Left = 20 + bHalf * (250 + 140);
                    item.Top = 40 + (DelayCount - bHalf * Half) * 30;
                    item.TextAlign = ContentAlignment.MiddleRight;
                    item.Text = NumBox._PmtName.ToString();

                    tabControl1.TabPages[0].Controls.Add(NumBox);
                    tabControl1.TabPages[0].Controls.Add(item);

                    DelayCount++;
                }
            }
        }

        private void TimeOutControlUpdate()
        {
            string PmtName = "";
            int Half = 0;
            int bHalf = 0;
            ListBox lPmtName = new ListBox();
            int DelayCount = 0;
            //for (int i = 0; i < Enum.GetValues(typeof(clsEnum.enuPmtName)).Length; i++)
            //{
            //    PmtName = Enum.GetValues(typeof(clsEnum.enuPmtName)).GetValue(i).ToString();
            foreach (clsEnum.enuPmtName ePmtName in Enum.GetValues(typeof(clsEnum.enuPmtName)))
            {
                PmtName = ePmtName.ToString();
                if (PmtName.Contains("TimeOut_") || PmtName.Contains("Timeout_"))
                {
                    lPmtName.Items.Add(PmtName);
                }

            }
            Half = (lPmtName.Items.Count + 1) / 2;
            for (int i = 0; i < lPmtName.Items.Count; i++)
            {
                PmtName = lPmtName.Items[i].ToString();
                //if (PmtName.Contains("Delay_"))
                {
                    //if (i >= Half)
                    //{
                    //    bHalf = 1;
                    //}
                    ArtControlLib.comNumBox NumBox = new ArtControlLib.comNumBox();
                    NumBox._DefaultValue = 0;
                    NumBox._IsSaveToIni = true;
                    NumBox._IsSaveToLog = true;
                    NumBox._IsShowPopForm = true;
                    NumBox._Maximum = 1000000;
                    NumBox._Minimum = 0;
                    NumBox._PmtName = (clsEnum.enuPmtName)Enum.Parse(typeof(clsEnum.enuPmtName), PmtName);
                    NumBox._PmtType = clsEnum.enuPmtType.System;
                    NumBox._Value = 0;
                    NumBox.Size = new System.Drawing.Size(100, 20);
                    NumBox.Left = 250 + 40 + bHalf * (250 + 140);
                    NumBox.Top = 40 + (DelayCount - bHalf * Half) * 30;

                    Label item = new Label();
                    item.Name = "TimeOut_" + i.ToString();
                    item.Text = PmtName;
                    item.AutoSize = false;
                    item.Size = new System.Drawing.Size(250, 20);
                    item.Left = 20 + bHalf * (250 + 140);
                    item.Top = 40 + (DelayCount - bHalf * Half) * 30;
                    item.TextAlign = ContentAlignment.MiddleRight;
                    item.Text = NumBox._PmtName.ToString();

                    tabControl1.TabPages[1].Controls.Add(NumBox);
                    tabControl1.TabPages[1].Controls.Add(item);

                    DelayCount++;
                }
            }
        }

        #endregion

        #region//===================== 以下為事件處理 =====================

        #endregion
    }
}

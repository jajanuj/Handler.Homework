using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ArtCommonLib;
using ArtData;

namespace ArtControlLib
{
    /// <summary>
    /// 圖像按鈕元件
    /// </summary>
    public partial class comImgButton : Button
    {
        #region //=====================  區域變數設置 =====================
        bool m_bStatus = false;
        bool m_DefaultStatus = false;
        bool m_bIsImgBackground = false;        

        Image m_imgTrue;
        Image m_imgFalse;

        /// <summary>取得或設定對應參數名稱</summary>
        [Description("取得或設定對應參數名稱")]
        [Category("User define")]
        public clsEnum.enuPmtName? _PmtName
        {
            get;
            set;
        }

        /// <summary>取得或設定對應參數種類</summary>
        [Description("取得或設定對應參數種類")]
        [Category("User define")]
        public clsEnum.enuPmtType? _PmtType
        {
            get;
            set;
        }

        /// <summary>取得或設定狀態</summary>
        [Description("取得或設定狀態")]
        [Category("User define")]
        public bool _Status
        {
            get
            {
                return m_bStatus;
            }

            set
            {
                m_bStatus = value;
                ShowImg();

                int iStatus = value ? 1 : 0;

                if (Parent != null && Parent.IsHandleCreated)
                {
                    if (this._PmtName != null)
                    {
                        if (this._IsSaveToLog && ucParameter.GetValue((clsEnum.enuPmtName)this._PmtName) != iStatus)
                        {
                            clsLog.Log(clsCmData.enuLogType.ButtonLog,
                                this.Parent.Name + "-" + this.Name + " => " 
                                + this._PmtName + ":" 
                                + ucParameter.GetValue((clsEnum.enuPmtName)this._PmtName) + " =>" + iStatus);
                        }

                        ucParameter.SetValue((clsEnum.enuPmtName)this._PmtName, iStatus);
                        if (this._IsSaveToIni || ucParameter.IsAlwaySave)
                        {
                            ucParameter.SaveValue(this._PmtType, this._PmtName, iStatus.ToString());
                        }
                    }
                }
            }
        }

        /// <summary>取得或設定預設狀態</summary>
        [Description("取得或設定預設狀態")]
        [Category("User define")]
        public bool _DefaultStatus
        {
            get
            {
                return m_DefaultStatus;
            }
            set
            {
                m_DefaultStatus = value;
            }
        }

        /// <summary>取得或設定True 圖片</summary>
        [Description("取得或設定True 圖片")]
        [Category("User define")]
        public Image _ImgTrue
        {
            get
            {
                return m_imgTrue;
            }
            set
            {
                m_imgTrue = value;
                ShowImg();
            }
        }

        /// <summary>取得或設定False 圖片</summary>
        [Description("取得或設定False 圖片")]
        [Category("User define")]
        public Image _ImgFalse
        {
            get
            {
                return m_imgFalse;
            }
            set
            {
                m_imgFalse = value;
                ShowImg();
            }
        }

        /// <summary>取得或設定是否顯示於背景</summary>
        [Description("取得或設定是否顯示於背景")]
        [Category("User define")]
        public bool _ImgBackground
        {
            get
            {
                return m_bIsImgBackground;
            }
            set
            {
                m_bIsImgBackground = value;
                ShowImg();
            }
        }

        /// <summary>取得或設定是否寫入Ini File</summary>
        [Description("取得或設定是否寫入Ini File")]
        [Category("User define")]
        public bool _IsSaveToIni
        {
            get;
            set;
        }

        /// <summary>取得或設定數值改變時是否寫入Button Log</summary>
        [Description("取得或設定數值改變時是否寫入Button Log")]
        [Category("User define")]
        public bool _IsSaveToLog
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 圖像按鈕元件建構式
        /// </summary>
        public comImgButton()
        {
            InitializeComponent();
            this._ImgBackground = true;
            this._IsSaveToIni = true;
            this._IsSaveToLog = true;
            this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        private void ShowImg()
        {
            if (_Status)
            {
                if (_ImgBackground)
                {
                    this.Image = null;
                    this.BackgroundImage = _ImgTrue; 
                }
                else
                {
                    this.Image = _ImgTrue;
                    this.BackgroundImage = null;
                }
            }
            else
            {
                if (_ImgBackground)
                {
                    this.Image = null;
                    this.BackgroundImage = _ImgFalse;
                }
                else
                {
                    this.Image = _ImgFalse;
                    this.BackgroundImage = null;
                }
            }
        }

        //===================== 以下為事件處理 =====================
        private void comImgBox_Click(object sender, EventArgs e)
        {
            _Status = !_Status;
        }


    }
}

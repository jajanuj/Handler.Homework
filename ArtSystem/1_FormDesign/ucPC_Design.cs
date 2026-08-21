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

namespace ArtSystem.FormDesign
{
    public partial class ucPC_Design : ucBaseUserControl
    {

        #region //===================== 版面視窗調整參數設置 =====================

        /// <summary> Hotkey-Width,預設200 </summary>
        static private int c_HotKey_Width = 200;
        /// <summary> Hotkey-Height,預設320 </summary>
        static private int c_HotKey_Height = 320;
        /// <summary> SubFunc-Width,預設100 </summary>
        static private int c_SubFunc_Width = 100;
        /// <summary> MainFunc-Height,預設70 </summary>
        static private int c_MainFunc_Height = 70;

        #endregion

        #region //===================== Enum 宣告 =====================

        /// <summary> 調整版面比例 </summary>
        public enum enuDesignPmt
        {
            /// <summary> Hotkey-Width,預設200 </summary>
            HotKey_Width,
            /// <summary> Hotkey-Height,預設320 </summary>
            HotKey_Height,
            /// <summary> SubFunc-Width,預設100 </summary>
            SubFunc_Width,
            /// <summary> MainFunc-Height,預設70 </summary>
            MainFunc_Height,
        }

        #endregion

        #region //===================== 必要函式設置 =====================

        /// <summary> 建構式 </summary>
        public ucPC_Design()
        {
            InitializeComponent();
        }

        /// <summary> 重置Controls </summary>
        public void _UpdateControls()
        {
            try
            {
                ucArtMain_Design.GetSingleton().p_SubFuncPanel = this.SubForm;

                #region//Size Change (MainPanel)
                this.MainPanel.Location = new Point(0, 0);
                this.MainPanel.Size = this.Size;
                #endregion

                #region//Size Change (ContainerTitle, ContainerHotKey, ContainerMainFunc, ContainerSubFunc, SubForm, SubForm_Control)

                #region//ContainerTitle SizeChange
                this.ContainerTitle.Top = 0;
                this.ContainerTitle.Left = 0;
                this.ContainerTitle.Width = c_HotKey_Width;
                this.ContainerTitle.Height = MainPanel.Height - c_HotKey_Height;
                #endregion

                #region//ContainerHotKey SizeChange
                this.ContainerHotKey.Top = this.ContainerTitle.Height;
                this.ContainerHotKey.Left = 0;
                this.ContainerHotKey.Width = c_HotKey_Width;
                this.ContainerHotKey.Height = c_HotKey_Height;
                #endregion

                #region//ContainerMainFunc SizeChange
                this.ContainerMainFunc.Top = this.MainPanel.Height - c_MainFunc_Height;
                this.ContainerMainFunc.Left = c_HotKey_Width;
                this.ContainerMainFunc.Width = this.MainPanel.Width - c_HotKey_Width;
                this.ContainerMainFunc.Height = c_MainFunc_Height;
                #endregion

                #region//ContainerSubFunc SizeChange
                this.ContainerSubFunc.Top = 0;
                this.ContainerSubFunc.Left = this.MainPanel.Width - c_SubFunc_Width;
                this.ContainerSubFunc.Width = c_SubFunc_Width;
                this.ContainerSubFunc.Height = this.MainPanel.Height - c_MainFunc_Height;
                #endregion

                #region//SubForm SizeChange
                this.SubForm.Top = 0;
                this.SubForm.Left = c_HotKey_Width;
                this.SubForm.Width = MainPanel.Width - c_SubFunc_Width - c_HotKey_Width;
                this.SubForm.Height = MainPanel.Height - c_MainFunc_Height;
                ucArtMain_Design.GetSingleton()._ResetScrollBar();
                #endregion


                #endregion

                #region//SetParent (ucTitle, ucHotkeyFunc, ucMainFunc, ucSubFunc)
                //ucTitle 設置
                if (ucArtMain_Design.GetSingleton()._Title_PC != null)
                {
                    SetParent(ucArtMain_Design.GetSingleton()._Title_PC, this.ContainerTitle);
                    ucArtMain_Design.GetSingleton()._Title_PC.SetReflashTimerStart(true);
                }

                //ucHotkeyFunc 快捷功能頁設置
                if (ucArtMain_Design.GetSingleton()._HotKey_PC != null)
                {
                    SetParent(ucArtMain_Design.GetSingleton()._HotKey_PC, this.ContainerHotKey);
                    ucArtMain_Design.GetSingleton()._HotKey_PC.SetReflashTimerStart(true);
                }

                //ucMainFunc 主要功能頁設置
                SetParent(ucMainFunc.GetSingleton(), this.ContainerMainFunc);
                ucMainFunc.GetSingleton().SetReflashTimerStart(true);

                //ucSubFunc 子功能頁設置
                for (int iSubFuncPanelNum = 0; iSubFuncPanelNum < clsCmData.g_astrMainFunc.Length; iSubFuncPanelNum++)
                {
                    if (iSubFuncPanelNum == 0)
                    {
                        this.ContainerSubFunc.BackColor = ucSubFunc.GetSingleton(clsCmData.g_astrMainFunc[iSubFuncPanelNum]).BackColor;
                    }
                    SetParent(ucSubFunc.GetSingleton(clsCmData.g_astrMainFunc[iSubFuncPanelNum]), this.ContainerSubFunc);
                }

                #endregion

                //Reshow Current SubForm
                ucSubFunc.GetSingleton(ucSubFunc._strNowMainFuncName)._ShowSubFunc(ucSubFunc._strNowSubFuncName);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        /// <summary> 設定SubForm介面上下移動 </summary>
        public void _SetSubFormLocation(int ScrollBarValue)
        {
            try
            {
                if (sBar_SubForm.Value != ScrollBarValue)
                {
                    if (ScrollBarValue >= sBar_SubForm.Minimum
                        && ScrollBarValue <= sBar_SubForm.Maximum)
                    {
                        sBar_SubForm.Value = ScrollBarValue;
                    }
                }
                SubForm.Top = -ScrollBarValue * 10;
                SubForm.Height = MainPanel.Height - c_MainFunc_Height - SubForm.Top;
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }
        /// <summary> Form滑鼠滾輪滑動事件，移動SubForm </summary>
        public void _Form_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    if (sBar_SubForm.Value + 1 < sBar_SubForm.Maximum)
                    {
                        sBar_SubForm.Value += 1;
                    }
                    else
                    {
                        sBar_SubForm.Value = sBar_SubForm.Maximum;
                    }
                }
                else
                {
                    if (sBar_SubForm.Value - 1 > sBar_SubForm.Minimum)
                    {
                        sBar_SubForm.Value -= 1;
                    }
                    else
                    {
                        sBar_SubForm.Value = sBar_SubForm.Minimum;
                    }
                }
                _SetSubFormLocation(sBar_SubForm.Value);
            }
            catch (Exception ex)
            {
                clsArtSystem.CatchLog(ex);
            }
        }

        #endregion

        #region //===================== static public 函式設置 =====================

        /// <summary> 調整版面比例 </summary>
        static public void _SetPanelDesign(enuDesignPmt eDesignPmt, int iValue)
        {
            switch (eDesignPmt)
            {
                case enuDesignPmt.HotKey_Width:
                    c_HotKey_Width = iValue;
                    break;
                case enuDesignPmt.HotKey_Height:
                    c_HotKey_Height = iValue;
                    break;
                case enuDesignPmt.SubFunc_Width:
                    c_SubFunc_Width = iValue;
                    break;
                case enuDesignPmt.MainFunc_Height:
                    c_MainFunc_Height = iValue;
                    break;
                default:
                    break;
            }
        }

        /// <summary> 取得版面設計尺寸 </summary>
        static public int _GetPanelDesign(enuDesignPmt eDesignPmt, int iValue)
        {
            int rValue = 0;
            switch (eDesignPmt)
            {
                case enuDesignPmt.HotKey_Width:
                    rValue = c_HotKey_Width;
                    break;
                case enuDesignPmt.HotKey_Height:
                    rValue = c_HotKey_Height;
                    break;
                case enuDesignPmt.SubFunc_Width:
                    rValue = c_SubFunc_Width;
                    break;
                case enuDesignPmt.MainFunc_Height:
                    rValue = c_MainFunc_Height;
                    break;
                default:
                    break;
            }
            return rValue;
        }

        #endregion

        #region //===================== private 函式設置 =====================

        /// <summary> 將ucControl載入到Panel內,且尺寸相等 </summary>
        private void SetParent(Control p_Item, Panel p_Parent)
        {
            if (p_Item != null)
            {
                p_Item.Parent = p_Parent;
                p_Item.Location = new Point(0, 0);
                p_Item.Size = p_Parent.Size;
            }
        }

        #endregion
    }
}

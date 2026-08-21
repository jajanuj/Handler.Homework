using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtTeach;
using ArtSystem;

namespace ArtEQ
{
    /// <summary>
    /// 此物件為UserControl 範例，請複製此範例修改
    /// 請注意修改 namespace
    /// 使用方式：例如新增usTest
    /// 將_ucXXXXXX 取代為usTest，取代時請注意~~~~查詢項目為"目前文件"，以免修改錯誤
    /// </summary>
    public partial class ucMotionTeach : ArtCommonLib.ucBaseUserControl
    {
        #region //=====================  區域變數設置 =====================
        static ucMotionTeach m_Singleton;
        static object m_objLock = new object();
        bool? Lane_Reverse = null;
        static private clsMotionRoller m_LaneRoller;
        static private bool bIsLaneRoller_Stop = false;//程式啟動後曾經下過STOP
        #endregion


        #region //=====================  必要函式設置 =====================
        /// <summary>
        /// 取得唯一物件，避免重覆設置
        /// </summary>
        /// <returns>回傳物件</returns>
        public static ucMotionTeach GetSingleton()
        {
            if (m_Singleton == null)
            {
                lock (m_objLock)
                {
                    if (m_Singleton == null)
                    {
                        m_Singleton = new ucMotionTeach();
                    }
                }
            }
            return m_Singleton;
        }

        /// <summary>
        /// 物件建立請利用 GetSingleton()，除非特殊需求
        /// </summary>
        private ucMotionTeach()
        {
            InitializeComponent();
            if (clsArtSystem.bIsProgramOpen == false)
            { return; }
            ucParameter.Add(this);
            UpdateControls();
        }

        /// <summary>
        /// 物件重置
        /// </summary>
        public void UpdateControls()
        {
            ucEditUI1.Initial();
        }

        /// <summary>
        /// 自動更新介面參數
        /// </summary>
        protected override void ReflashTimerFunc()
        {
            ucEditUI1.ReflashUI();
            if (ucEditUI1.IsEditUIOpen() == true)
            {
                return;
            }

            //{
            //    bool bIsAllMotionDone = true;
            //    foreach(clsEnum.enuAxis pAxis in Enum.GetValues(typeof(clsEnum.enuAxis)))
            //    {
            //        if (pAxis == clsEnum.enuAxis.Arm_Z)
            //        {
            //            continue;
            //        }
            //        if(clsMotionCtrl.IsMotionDone(pAxis) == false)
            //        {
            //            bIsAllMotionDone = false;
            //        }
            //    }
            //    if (ProcDetectIO.bIsSaftyLightGate == false
            //        && bIsAllMotionDone == false)
            //    {
            //        foreach (clsEnum.enuAxis Axis in Enum.GetValues(typeof(clsEnum.enuAxis)))
            //        {
            //            clsMotionCtrl.SlowDownStop(Axis);
            //        }
            //    }
            //}
        }

        #endregion


        #region //===================== public 函式設置 =====================

        #endregion


        #region //===================== private 函式設置 =====================
        #endregion


        #region//===================== 以下為事件處理 =====================

        private bool ucEditUI1_m_EventAxisSafeCheck_1(ucJogTeach sender, clsEnum.enuAxis p_Axis, clsDataJogTeach.ActionMode p_ActionMode, double MoveTarget)
        {
            clsLog.Log(clsEnum.enuLogName.ButtonLog,
                "ucMotionTeah ButtonClick AxisMove (" + p_Axis.ToString() + ")" + " : " + p_ActionMode.ToString() + "==>" + MoveTarget.ToString("F3"));
            if (ucParameter.GetValueBool(clsEnum.enuPmtName.Sys_TeachEnable) == false)
            {
                formMessageBox.Show("Please Enable Teach Mode To Do Teaching.");
                return false;
            }

            //if(p_Axis == clsEnum.enuAxis.Axis1)
            //{
            //    double Pos_MgzArm_Z = (double)clsMotionCtrl.GetPosMm(clsEnum.enuAxis.Axis1);
            //    double Pos_MgzArm_UpY = (double)clsMotionCtrl.GetPosMm(clsEnum.enuAxis.Axis2);
            //    double Pos_MgzArm_MidY = (double)clsMotionCtrl.GetPosMm(clsEnum.enuAxis.Axis3);
            //    double Pos_MgzArm_DownY = (double)clsMotionCtrl.GetPosMm(clsEnum.enuAxis.Axis4);
            //    if ((p_ActionMode == clsDataJogTeach.ActionMode.RelativeMove
            //        && Math.Abs(MoveTarget) <= 1) == false)
            //    {
            //        if (Math.Abs(Pos_MgzArm_UpY - ucPosPmt.GetValueDouble(clsEnum.enuPosName.MgzArm_SafePos_UpY)) > 0.1)
            //        {
            //            formMessageBox.Show("Magazine Arm (UpY) Not In Safe Pos.\r\nOnly Can Move Lest Then 1(mm).", "[Axis Safe Check Error]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            return false;
            //        }
            //        if (Math.Abs(Pos_MgzArm_MidY - ucPosPmt.GetValueDouble(clsEnum.enuPosName.MgzArm_SafePos_MidY)) > 0.1)
            //        {
            //            formMessageBox.Show("Magazine Arm (UpY) Not In Safe Pos.\r\nOnly Can Move Lest Then 1(mm).", "[Axis Safe Check Error]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            return false;
            //        }
            //        if (Math.Abs(Pos_MgzArm_DownY - ucPosPmt.GetValueDouble(clsEnum.enuPosName.MgzArm_SafePos_DownY)) > 0.1)
            //        {
            //            formMessageBox.Show("Magazine Arm (UpY) Not In Safe Pos.\r\nOnly Can Move Lest Then 1(mm).", "[Axis Safe Check Error]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            return false;
            //        }
            //    }
            //}
            //else if (p_Axis == clsEnum.enuAxis.Axis5)
            //{
            //    double Pos_LC_PusherX = (double)clsMotionCtrl.GetPosMm(clsEnum.enuAxis.Axis6);
            //    if (Math.Abs(Pos_LC_PusherX - ucPosPmt.GetValueDouble(clsEnum.enuPosName.LaneChangePuller_Safe)) > 0.1)
            //    {
            //        formMessageBox.Show("Lane Change Pusher Not In Safe Pos.\r\nOnly Can Move Lest Then 1(mm).", "[Axis Safe Check Error]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return false;
            //    }
            //}
            return true;
        }
        private bool ucEditUI1_m_EventDIOSafeCheck(ucDIOInfo sender, clsEnum.enuDo p_Do, bool p_Status)
        {
            clsLog.Log(clsEnum.enuLogName.ButtonLog,
                "ucMotionTeah ButtonClick DIO (" + p_Do.ToString() + ")" + " : " + p_Status.ToString());
            if (ucParameter.GetValueBool(clsEnum.enuPmtName.Sys_TeachEnable) == false)
            {
                formMessageBox.Show("Please Enable Teach Mode To Do Teaching.");
                return false;
            }
            return true;
        }
        private void ucMotionTeach_SizeChanged(object sender, EventArgs e)
        {
            ucEditUI1.Width = this.Width;
            ucEditUI1.Height = this.Height;
        }
        private void ucEditUI1_m_EventSetValueChanged(ucJogTeach sender, clsEnum.enuPosName p_PosName, double OraginalValue, double NewValue)
        {
            clsLog.Log(clsEnum.enuLogName.TeachLog.ToString(),
                "ucMotionTeah Value Change (" + p_PosName.ToString() + ")" + " : " + OraginalValue.ToString("F3") + " -> " + NewValue.ToString("F3"));

            if (ucParameter.GetValueBool(clsEnum.enuPmtName.Sys_TeachEnable) == false)
            {
                ucPosPmt.SaveValue(p_PosName, OraginalValue.ToString());
                clsLog.Log(clsEnum.enuLogName.TeachLog.ToString(),
                    "ucMotionTeah Value Change Cancel, Teach Mode Off(" + p_PosName.ToString() + ")" + " : " + OraginalValue.ToString("F3"));
                formMessageBox.Show("Please Enable Teach Mode To Do Teaching");
                return;
            }
        }

        #endregion




    }
}

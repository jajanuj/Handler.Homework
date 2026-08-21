using System;
using System.IO;
using System.Collections.Generic;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;

using ArtCommonLib;
using ArtControlLib;
using ArtData;
using ArtCommunication;


namespace ArtTeach
{    
	public partial class clsJogAndTeach: ArtCommonLib.ucBaseUserControl
	{
		#region //===================== 區域變數設置 =====================


		event EventHandler IsAxisSafe;
		public event EventHandler _EventIsAxisSafe
		{
			remove
			{
				IsAxisSafe -= value;
			}
			add
			{
				IsAxisSafe += value;
			}
		}

		event EventHandler SetTarget;
		public event EventHandler _EventSetTarget
		{
			remove
			{
				SetTarget -= value;
			}
			add
			{
				SetTarget += value;
			}
		}

		event EventHandler DirJogAMouseEnter;
		public event EventHandler _DirJogAMouseEnter
		{
			remove
			{
				DirJogAMouseEnter -= value;
			}
			add
			{
				DirJogAMouseEnter += value;
			}
		}

		event EventHandler DirJogBMouseEnter;
		public event EventHandler _DirJogBMouseEnter
		{
			remove
			{
				DirJogBMouseEnter -= value;
			}
			add
			{
				DirJogBMouseEnter += value;
			}
		}

		event EventHandler JogAndTeachLog;
		public event EventHandler _JogAndTeachLog
		{
			remove
			{
				JogAndTeachLog -= value;
			}
			add
			{
				JogAndTeachLog += value;
			}
		}


		/// <summary> ALM顏色 </summary>
		//[Description("取得或設定ALM顏色")]
		//[Category("ArtMMI"), Browsable(false)]
		public Color _ColorAlm
		{
			get
			{
				return lblALM.BackColor;
				
			}
			set
			{
				lblALM.BackColor = value;
			}
		}

		/// <summary> PEL顏色 </summary>
		//[Description("取得或設定PEL顏色")]
		//[Category("ArtMMI"), Browsable(false)]
		public Color _ColorPEL
		{
			get
			{
				return lblPEL.BackColor;
				
			}
			set
			{
				lblPEL.BackColor = value;
			}
		}

		/// <summary> MEL顏色 </summary>
		//[Description("取得或設定MEL顏色")]
		//[Category("ArtMMI"), Browsable(false)]
		public Color _ColorMEL
		{
			get
			{
				return lblMEL.BackColor;
				
			}
			set
			{
				lblMEL.BackColor = value;
			}
		}

		/// <summary> ORG顏色 </summary>
		//[Description("取得或設定ORG顏色")]
		//[Category("ArtMMI"), Browsable(false)]
		public Color _ColorORG
		{
			get
			{
				return lblORG.BackColor;
				
			}
			set
			{
				lblORG.BackColor = value;
			}
		}

		/// <summary> INP顏色 </summary>
		//[Description("取得或設定INP顏色")]
		//[Category("ArtMMI"), Browsable(false)]
		public Color _ColorINP
		{
			get
			{
				return lblINP.BackColor;
				
			}
			set
			{
				lblINP.BackColor = value;
			}
		}

		/// <summary> SVON顏色 </summary>
		//[Description("取得或設定SVON顏色")]
		//[Category("ArtMMI"), Browsable(false)]
		public Color _ColorSVON
		{
			get
			{
				return btnSVON.BackColor;
			}
			set
			{
				btnSVON.BackColor = value;
			}
		}


		/// <summary> 軸動安全 </summary>
		//[Description("取得或設定軸動安全")]
		//[Category("ArtMMI"), Browsable(false)]
		public bool _bAxisIsSafe
		{
			set;
			get;
		}


		/// <summary> 是否使用Job And Teach元件 </summary>
		[Description("是否使用Job And Teach元件")]
		[DisplayName("Enable Teach Component"), CategoryAttribute("ArtMMI"), Browsable(true)]
		public bool _bIsJobAndTeachEnable
		{
			set
			{
				m_clsJogTeachInfo._bIsJobAndTeachEnable = value;
				this.Visible = value;
			}
			get
			{
				return m_clsJogTeachInfo._bIsJobAndTeachEnable;
			}
		}

		/// <summary> 設定Job移動方向 </summary>
		[Description("取得或設定設定移動方向")]
		[DisplayName("Jog Move Direction"), CategoryAttribute("ArtMMI"), Browsable(true), DefaultValue(enuMoveDir.Positive)]
		public enuMoveDir _JogMoveDir
		{
			set
			{
				m_clsJogTeachInfo._JogMoveDir = value;
			}
			get
			{
				return m_clsJogTeachInfo._JogMoveDir;
			}
		}

		/// <summary> JogA文字 </summary>
		[Description("取得或設定JogA文字")]
		[DisplayName("Jog A Text"), CategoryAttribute("ArtMMI"), Browsable(true)]
		public string _JogAText
		{
			set
			{
				m_clsJogTeachInfo._JogAText = value;
				btnJogA.Text = value;
			}
			get
			{
				return m_clsJogTeachInfo._JogAText;
			}
		}

		/// <summary> JogB文字 </summary>
		[Description("取得或設定JogB文字")]
		[DisplayName("Jog B Text"), CategoryAttribute("ArtMMI"), Browsable(true)]
		public string _JogBText
		{
			set
			{
				m_clsJogTeachInfo._JogBText = value;
				btnJogB.Text = value;
			}
			get
			{
				return m_clsJogTeachInfo._JogBText;
			}
		}



		/// <summary> 移動速度百分比 </summary>
		[Description("取得或設定移動速度百分比")]
		[DisplayName("Axis Teach Speed"), CategoryAttribute("ArtMMI"), Browsable(true)]
		/// <summary> 移動速度百分比 </summary>
		public int _TeachSpeed
		{
			set
			{
				m_clsJogTeachInfo._TeachSpeed = value;
			}
			get
			{
				return m_clsJogTeachInfo._TeachSpeed;
			}
		}

		/// <summary> 軸動標題名稱 </summary>
		[Description("取得或設定軸動標題名稱")]
		[DisplayName("Axis Title"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public string _AxisTitle
		{
			set
			{
                m_clsJogTeachInfo._AxisTitle_Str = value;
				lblAxisTitle.Text = value.ToString();
			}
			get
			{
                return m_clsJogTeachInfo._AxisTitle_Str;
			}
		}

		/// <summary> 軸動單元 </summary>
		[Description("取得或設定軸動標題名稱")]
		[DisplayName("Axis Unit"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public clsDataJogTeach.enuUnit _AxisUnit
		{
			set
			{
                m_clsJogTeachInfo._AxisUnit_Enum = value;
				lblUnit.Text = value.ToString();
			}
			get
			{
                return m_clsJogTeachInfo._AxisUnit_Enum;
			}
		}

		/// <summary> 指定移動軸名稱 </summary>
		[Description("取得或設定指定移動軸名稱")]
		[DisplayName("Axis Name"), CategoryAttribute("ArtMMI"), Browsable(true)]
		public clsEnum.enuAxis? _AxisName
		{
			set
			{
				m_clsJogTeachInfo._AxisID_Enum = value;
			}
			get
			{
                return m_clsJogTeachInfo._AxisID_Enum;
			}
		}


		///// <summary> 位置名稱 </summary>
		//[Description("取得或設定設定寫入位置名稱")]
		//[CategoryAttribute("ArtMMI"), Browsable(true)]
		//public string _TargetName
		//{
		//    set
		//    {
		//        //EnsureChildControls();
		//        //if (string.IsNullOrEmpty(value) 
		//        //    || string.IsNullOrWhiteSpace(value)
		//        //    )
		//        //string pattern = @"gr[ae]y\s\S+?[\s\p{P}]";
		//        //if (!Regex.IsMatch(value, @"gr[ae]y\s\S+?[\s\p{P}]"))
		//        if (!string.IsNullOrEmpty(value)
		//            && !Regex.IsMatch(value, "^[A-Za-z0-9]+$"))
		//        {
		//            throw new ArgumentNullException("_PositionName");
		//        }

		//        m_clsJogTeachInfo._TargetName = value;
		//    }
		//    get
		//    {
		//        return m_clsJogTeachInfo._TargetName;
		//    }
		//}

		///<summary> 對應參數名稱 </summary>
		[Description("取得或設定對應參數名稱")]
		[DisplayName("Position Name"), CategoryAttribute("ArtMMI"), Browsable(true)]
		public clsEnum.enuPosName? _PosName
		{
			set
			{
                m_clsJogTeachInfo._PosName_enu = value;
				cnbTarget._enuName = value;
			}
			get
			{
                return m_clsJogTeachInfo._PosName_enu;
			}
		}

		/// <summary> 設定目標位置 </summary>
		[Description("取得或設定目標位置")]
		[DisplayName("Position Value"), CategoryAttribute("ArtMMI"), Browsable(true)]
		public double _PosValue
		{
			set
			{
				//m_clsJogTeachInfo._PosValue = value;
				cnbTarget._TargetValue = (decimal)value;
			}
			get
			{
				//return m_clsJogTeachInfo._Target;
				return (double)cnbTarget._TargetValue;
			}
		}


		///<summary> 對應參數名稱 </summary>
		[Description("取得或設定對應參數名稱")]
		[DisplayName("Undo Name"), CategoryAttribute("ArtMMI"), Browsable(true)]
		public clsEnum.enuPosName? _UndoName
		{
			set
			{
				m_clsJogTeachInfo._UndoName = value;
				cnbUndo._enuName = value;
			}
			get
			{
				return m_clsJogTeachInfo._UndoName;
			}
		}

		/// <summary> 設定備份位置 </summary>
		[Description("取得或設定備份位置")]
		[DisplayName("Undo Value"), CategoryAttribute("ArtMMI"), Browsable(true)]
		public double _UndoValue
		{
			set
			{
				m_clsJogTeachInfo._UndoValue = value;
				cnbUndo._TargetValue = (decimal)value;
			}
			get
			{
				return (double)cnbUndo._TargetValue;
			}
		}

		/// <summary> 設定安全位置 </summary>
		[Description("取得或設定設定安全位")]
		[DisplayName("Safe Position Value"), CategoryAttribute("ArtMMI"), Browsable(true)]
		public double _SafePosition
		{
			set
			{
				m_clsJogTeachInfo._SafePosition = value;
			}
			get
			{
				return m_clsJogTeachInfo._SafePosition;
			}
		}

		/// <summary> 是否可使用點位教導 </summary>
		[Description("是否可使用點位教導")]
		[DisplayName("Enable Set Position"), CategoryAttribute("ArtMMI"), Browsable(true)]
		public bool _bIsSetPositionEnable
		{
			set
			{
				m_clsJogTeachInfo._bIsSetPositionEnable = value;
				btnSET.Enabled = value;
				btnUndo.Enabled = value;
				cnbUndo.Enabled = value;
				cnbTarget.Enabled = value;
			}
			get
			{
				return m_clsJogTeachInfo._bIsSetPositionEnable;
			}
		}

		/// <summary> JogA Engter圖片 </summary>
		[Description("選擇移動方向圖片")]
		[DisplayName("Jog A Engter Picture"), CategoryAttribute("ArtMMI"), Browsable(true)]
		public clsDataJogTeach.enuDirection? _JobADirection
		{
			set
			{
				m_clsJogTeachInfo._JobADirection = value;
				btnJogA.BackgroundImage = GetDirection(value);
			}
			get
			{
				return m_clsJogTeachInfo._JobADirection;
			}
		}

		/// <summary> JogB Engter圖片 </summary>
		[Description("選擇移動方向圖片")]
		[DisplayName("Jog B Engter Picture"), CategoryAttribute("ArtMMI"), Browsable(true)]
        public clsDataJogTeach.enuDirection? _JobBDirection
		{
			set
			{
				m_clsJogTeachInfo._JobBDirection = value;
				btnJogB.BackgroundImage = GetDirection(value);
			}
			get
			{
				return m_clsJogTeachInfo._JobBDirection;
			}
		}

		/// <summary> JogA Image 座標 </summary>
		[Description("選擇移動方向圖片")]
		[DisplayName("Jog A Engter Point"), CategoryAttribute("Direction"), Browsable(true)]
		public Point _ImgJogAPoint
		{
			set
			{
				m_clsJogTeachInfo._JogAImageDirPoint = value;
			}
			get
			{
				return m_clsJogTeachInfo._JogAImageDirPoint;
			}
		}

		/// <summary> JogB Image 座標 </summary>
		[Description("選擇移動方向圖片")]
		[DisplayName("Jog B Engter Point"), CategoryAttribute("Direction"), Browsable(true)]
		public Point _ImgJogBPoint
		{
			set
			{
				m_clsJogTeachInfo._JogBImageDirPoint = value;
			}
			get
			{
				return m_clsJogTeachInfo._JogBImageDirPoint;
			}
		}

		///<summary> 保護機制名稱 </summary>
		[Description("取得或設定對應參數名稱")]
		[DisplayName("Protect Name"), CategoryAttribute("ArtMMI"), Browsable(true)]
		public clsProtect.enuProtect? _ProtectName
		{
			set
			{
				m_clsJogTeachInfo._ProtectName = value;
			}
			get
			{
				return m_clsJogTeachInfo._ProtectName;
			}
		}

		private clsDataJogTeach m_clsJogTeachInfo = new clsDataJogTeach();
		public clsDataJogTeach _clsJogTeachInfo
		{
			set
			{
				if (value != null)
				{
					m_clsJogTeachInfo = value;
					//UpdateControl();
				}
			}
			get
			{
				return m_clsJogTeachInfo;
			}
		}


		/// <summary> 相對位置移動 </summary>
		public bool _IsRelativeMove
		{
			get;
			set;
		}

		double dPitch = 0;
		double dTargetTemp = 0;
		enuMoveDir enuJogAMoveDir = new enuMoveDir();

		#endregion

		#region //===================== 必要函式設置 =====================


		/// <summary>
		/// 物件建立請利用 GetSingleton()，除非特殊需求
		/// </summary>
		public clsJogAndTeach()
		{
			InitializeComponent();
			ucParameter.Add(this);

			PanelAxisInfo.Visible = false;
			UpdateControl();
		}

		#endregion

		#region //===================== public 函式設置 ==================

		public virtual bool GetAxisSafe()
		{
			return false;
		}

        public Image GetDirection(clsDataJogTeach.enuDirection? p_enuDirection)
		{
			Image m_Resources = null;

			switch (p_enuDirection)
			{
                case clsDataJogTeach.enuDirection.Up:
					m_Resources = Properties.Resources.Up;
					break;
                case clsDataJogTeach.enuDirection.Down:
					m_Resources = Properties.Resources.Down;
					break;
                case clsDataJogTeach.enuDirection.Left:
					m_Resources = Properties.Resources.Left;
					break;
                case clsDataJogTeach.enuDirection.Right:
					m_Resources = Properties.Resources.Right;
					break;
                case clsDataJogTeach.enuDirection.TurnL:
					m_Resources = Properties.Resources.TurnL1;
					break;
                case clsDataJogTeach.enuDirection.TurnR:
					m_Resources = Properties.Resources.TurnR1;
					break;
				default:
					break;
			}

			return m_Resources;
		}

		#endregion

		#region //===================== private 函式設置 =================

		private void UpdateControl()
		{
			btnJogA.Text = m_clsJogTeachInfo._JogAText;
			btnJogB.Text = m_clsJogTeachInfo._JogBText;
            lblAxisTitle.Text = m_clsJogTeachInfo._AxisTitle_Str.ToString();
            lblUnit.Text = m_clsJogTeachInfo._AxisUnit_Enum.ToString();


			if (m_clsJogTeachInfo._bIsSetPositionEnable)
			{
				btnSET.Enabled = true;
				btnUndo.Enabled = true;
				cnbUndo.Enabled = true;
				cnbTarget.Enabled = true;
			}
			else
			{
				btnSET.Enabled = false;
				btnUndo.Enabled = false;
				cnbUndo.Enabled = false;
				cnbTarget.Enabled = false;
			}

			this.Visible = m_clsJogTeachInfo._bIsJobAndTeachEnable;
			//btnJogA.BackgroundImage = System.Drawing.Image.FromFile(m_clsJogTeachInfo._JobAImageEnter);
			//btnJogB.BackgroundImage = System.Drawing.Image.FromFile(m_clsJogTeachInfo._JobBImageEnter);
		}

		#endregion

		#region//===================== 以下為事件處理 ===================

		private void btnServo_Click(object sender, EventArgs e)
		{
			clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " On Off " + _AxisName + "Servo");
			Button iButton = (Button)sender;

			if (clsMotionCtrl.GetIoStatus((clsEnum.enuAxis)_AxisName, clsMotionCtrl.enuMotionIoName.SVON) == true)
			{
				clsMotionCtrl.SetServo((clsEnum.enuAxis)_AxisName, false);
				btnSVON.BackColor = Color.Black;
			}
			else
			{
				clsMotionCtrl.SetServo((clsEnum.enuAxis)_AxisName, true);
				iButton.BackColor = Color.Red;
			}
		}
		private void btnSafe_Click(object sender, EventArgs e)
		{
			clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser + " " + this._AxisName + " - Go Safe Position: -" + _SafePosition);

			if (clsCmData.g_bIsinitialized == false)
			{
				return;
			}

			_bAxisIsSafe = false;
			if (IsAxisSafe != null)
			{
				IsAxisSafe(this, e);
			}

			if (_bAxisIsSafe == true)
			{
				ucMotionSetting.SetAxisSpeed((clsEnum.enuAxis)_AxisName, _TeachSpeed);
				clsMotionCtrl.StartMoveMmA((clsEnum.enuAxis)_AxisName, _SafePosition);
			}
		}

		private void btnMouseUp(object sender, MouseEventArgs e)
		{
            try
            {
                if (!_IsRelativeMove)
                {
                    clsMotionCtrl.SlowDownStop((clsEnum.enuAxis)_AxisName);
                }
            }
            catch
            {}
		}
		private void btnMouseLeave(object sender, EventArgs e)
		{
			clsMotionCtrl.SlowDownStop((clsEnum.enuAxis)_AxisName);
		}

		private void btnSET_Click(object sender, EventArgs e)
		{
            try
            {
                if (!clsCmData.g_bIsinitialized) return;
                if (formMessageBox.Show("Are sure to Set Target Value?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                if (SetTarget != null)
                {
                    SetTarget(this, e);
                }

                cnbUndo._TargetValue = cnbTarget._TargetValue;
                cnbTarget._TargetValue = (decimal)clsMotionCtrl.GetPosMm((clsEnum.enuAxis)_AxisName);
                
                clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser +
                            " -> " + _AxisName +
                            " -> " + ((Control)sender).Name +
                            " -> Set (" + cnbTarget._enuName.ToString() + ") = " + cnbTarget._TargetValue + " -> " + cnbUndo._TargetValue
                            );
                clsLog.Log(ArtData.clsEnum.enuLogTypes.TeachLog.ToString(), clsCmData.g_strNowUser +
                            " -> " + _AxisName +
                            " -> " + ((Control)sender).Name +
                            " -> Set (" + cnbTarget._enuName.ToString() + ") = " + cnbTarget._TargetValue + " -> " + cnbUndo._TargetValue
                            );
            }
            catch
            {
            }
		}
		private void btnUndo_Click(object sender, EventArgs e)
		{
			if (!clsCmData.g_bIsinitialized) return;
			if (formMessageBox.Show("Are sure Undo Value to Target Value?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

			dTargetTemp = (double)cnbTarget._TargetValue;
			cnbTarget._TargetValue = cnbUndo._TargetValue;
			cnbUndo._TargetValue = (decimal)dTargetTemp;

			clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser +
						" -> " + _AxisName +
						" -> " + ((Control)sender).Name +
						" -> Set (Undo -> Target) = " + cnbUndo._TargetValue + " -> " + cnbTarget._TargetValue
						);
		}
		private void btnGO_Click(object sender, EventArgs e)
		{
			if (!clsCmData.g_bIsinitialized) return;

			_bAxisIsSafe = false;
			if (IsAxisSafe != null) IsAxisSafe(this, e);
			if (!_bAxisIsSafe) return;

			ucMotionSetting.SetAxisSpeed((clsEnum.enuAxis)_AxisName, _TeachSpeed);
			clsMotionCtrl.StartMoveMmA((clsEnum.enuAxis)_AxisName, (double)cnbTarget._TargetValue);
		}
		private void btnRelativePicth_Click(object sender, EventArgs e)
		{
			_IsRelativeMove = true;

			btnContinue.BackColor = Color.Black;
			cnbPitch.BackColor = Color.Blue;

			if (sender == btn001mm)
			{
				cnbPitch._Value = (decimal)0.01;

				clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser +
							" -> " + _AxisName +
							" -> " + ((Control)sender).Name +
							" -> Set Relative Move Pitch = " + cnbPitch._Value);
			}
			else if (sender == btn01mm)
			{
				cnbPitch._Value = (decimal)0.1;

				clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser +
							" -> " + _AxisName +
							" -> " + ((Control)sender).Name +
							" -> Set Relative Move Pitch = " + cnbPitch._Value);
			}
			else if (sender == btn10mm)
			{
				cnbPitch._Value = (decimal)10;

				clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser +
							" -> " + _AxisName +
							" -> " + ((Control)sender).Name +
							" -> Set Relative Pitch = " + cnbPitch._Value);
			}
			else if (sender == btn1mm)
			{
				cnbPitch._Value = (decimal)1;

				clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser +
							" -> " + _AxisName +
							" -> " + ((Control)sender).Name +
							" -> Set Relative Move Pitch = " + cnbPitch._Value);
			}
			else
			{
				if (JogAndTeachLog != null)
				{
					JogAndTeachLog(sender, e);
				}
			}

		}
		private void btnContinue_Click(object sender, EventArgs e)
		{
			_IsRelativeMove = false;
			btnContinue.BackColor = Color.Blue;
			cnbPitch.BackColor = Color.Black;

			clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser +
							" -> " + _AxisName +
							" -> " + ((Control)sender).Name +
							" -> Set Continue Move ");
		}

		private void btnJogClick(object sender, EventArgs e)
		{
			_bAxisIsSafe = false;
			if (IsAxisSafe != null) IsAxisSafe(this, e);
			if (!_bAxisIsSafe) return;
			
			if (!_IsRelativeMove) return;

			if (sender == btnJogA)
			{
				if (_JogMoveDir == enuMoveDir.Negative)
				{
					dPitch = -(double)cnbPitch._Value;
				}
				else
				{
					dPitch = (double)cnbPitch._Value;
				}
			}
			else if (sender == btnJogB)
			{
				if (_JogMoveDir == enuMoveDir.Negative)
				{
					dPitch = (double)cnbPitch._Value;
				}
				else
				{
					dPitch = -(double)cnbPitch._Value;
				}
			}

			clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser +
						" -> " + _AxisName +
						" -> " + ((Control)sender).Name +
						" -> Relative Move Pitch = " + dPitch);

			if (JogAndTeachLog != null)
			{
				JogAndTeachLog(sender, e);
			}

			if (_AxisName != null)
			{
				clsMotionCtrl.StartMoveMmR((clsEnum.enuAxis)_AxisName, dPitch);
			}
		}
		private void btnJogMouseDown(object sender, MouseEventArgs e)
		{
			_bAxisIsSafe = false;
			if (IsAxisSafe != null) IsAxisSafe(this, e);
			if (!_bAxisIsSafe) return;
				
			if (_IsRelativeMove) return;

			if (sender == btnJogA)
			{
				if (_JogMoveDir == enuMoveDir.Negative)
				{
					enuJogAMoveDir = enuMoveDir.Negative;
				}
				else
				{
					enuJogAMoveDir = enuMoveDir.Positive;
				}
			}
			else if (sender == btnJogB)
			{
				if (_JogMoveDir == enuMoveDir.Negative)
				{
					enuJogAMoveDir = enuMoveDir.Positive;
				}
				else
				{
					enuJogAMoveDir = enuMoveDir.Negative;
				}
			}

			clsLog.Log(clsCmData.enuLogType.ButtonLog, clsCmData.g_strNowUser +
						" -> " + _AxisName +
						" -> " + ((Control)sender).Name +
						" -> Continue Move Dir = " + enuJogAMoveDir.ToString());

			if (JogAndTeachLog != null)
			{
				JogAndTeachLog(sender, e);
			}

			if (_AxisName != null)
			{
				clsMotionCtrl.KeepMove((clsEnum.enuAxis)_AxisName, enuJogAMoveDir);
			}
		}

		private void btnHideAxisInfo_Click(object sender, EventArgs e)
		{
			if (!PanelAxisInfo.Visible)
			{
				PanelAxisInfo.Visible = true;
			}
			else
			{
				PanelAxisInfo.Visible = false;
			}
		}
		private void timerAxisStatus_Tick(object sender, EventArgs e)
		{
			if (clsDioCtrl.bIsActual)
			{
				_ColorAlm = (bool)clsMotionCtrl.GetIoStatus((clsEnum.enuAxis)_AxisName, clsMotionCtrl.enuMotionIoName.ALM) ? Color.Red : Color.Black;
				_ColorINP = (bool)clsMotionCtrl.GetIoStatus((clsEnum.enuAxis)_AxisName, clsMotionCtrl.enuMotionIoName.INP) ? Color.Red : Color.Black;
				_ColorPEL = (bool)clsMotionCtrl.GetIoStatus((clsEnum.enuAxis)_AxisName, clsMotionCtrl.enuMotionIoName.EL_Plus) ? Color.Red : Color.Black;
				_ColorMEL = (bool)clsMotionCtrl.GetIoStatus((clsEnum.enuAxis)_AxisName, clsMotionCtrl.enuMotionIoName.EL_Minus) ? Color.Red : Color.Black;
				_ColorORG = (bool)clsMotionCtrl.GetIoStatus((clsEnum.enuAxis)_AxisName, clsMotionCtrl.enuMotionIoName.ORG) ? Color.Red : Color.Black;
				_ColorSVON = (bool)clsMotionCtrl.GetIoStatus((clsEnum.enuAxis)_AxisName, clsMotionCtrl.enuMotionIoName.SVON) ? Color.Red : Color.Black;
			}
			else
			{
				_ColorAlm = Color.Red;
				_ColorINP = Color.Black;
				_ColorPEL = Color.Black;
				_ColorMEL = Color.Black;
				_ColorORG = Color.Black;
				_ColorSVON = Color.Black;
			}
		}

		private void cnbTarget_TextChanged(object sender, EventArgs e)
		{
			//m_clsJogTeachInfo._PosValue = (double)cnbTarget._TargetValue;
		}
		private void cnbUndo_TextChanged(object sender, EventArgs e)
		{
			m_clsJogTeachInfo._UndoValue = (double)cnbUndo._TargetValue;
		}

		private void btnJogA_MouseMove(object sender, MouseEventArgs e)
		{
			if (DirJogAMouseEnter != null)
			{
				DirJogAMouseEnter(this, e);
			}
		}
		private void btnJogB_MouseMove(object sender, MouseEventArgs e)
		{
			if (DirJogBMouseEnter != null)
			{
				DirJogBMouseEnter(this, e);
			}
		}

		private void btnJogB_MouseEnter(object sender, EventArgs e)
		{
			if (DirJogBMouseEnter != null)
			{
				DirJogBMouseEnter(this, e);
			}
		}
		private void btnJogA_MouseEnter(object sender, EventArgs e)
		{
			if (DirJogAMouseEnter != null)
			{
				DirJogAMouseEnter(this, e);
			}
		}

		#endregion

		private void btnLog_MouseDown(object sender, MouseEventArgs e)
		{
			if (JogAndTeachLog != null)
			{
				JogAndTeachLog(sender, e);
			}
		}


	}

}

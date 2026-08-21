using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using ArtCommonLib;
using ArtControlLib;
using ArtData;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ArtTeach
{
	public partial class ucEditUI : ArtCommonLib.ucBaseUserControl
	{
		#region //===================== 變數設置 =====================


		/// <summary> 靜態物件- 預防重複宣告 </summary>
		private static ucEditUI m_Singleton;
		private static object m_objLock = new object() ;

		ToolTip m_ToolTipMark = new ToolTip() ;
		ToolTip m_ToolTipDecription = new ToolTip() ;

		DataTable m_DataJobAndTeach = new DataTable() ;
		DataTable m_DataDIO = new DataTable() ;

		private List<clsProFile.clsMaster> m_lisMaster = new List<clsProFile.clsMaster>() ;
		private Dictionary<string, object> m_dicObject = new Dictionary<string, object>() ;
		private Dictionary<string, Bitmap> m_dicBitmap = new Dictionary<string, Bitmap>() ;
		private string CurrentNodeMaster = "";
		private string CurrentNodeSlave = "";
        public string MarkMessage = "";
        private bool IsUIEditEnable = false;
        private bool IsOpenUIEditEnable = false;

        public string _CurrentNodeMaster
        {
            private set
            {
            }
            get
            {
                return CurrentNodeMaster;
            }
        }
        public string _CurrentNodeSlave
        {
            private set
            {
            }
            get
            {
                return CurrentNodeSlave;
            }
        }


		#endregion
		
		#region //===================== 必要函式設置 =================
		/// <summary> 取得唯一物件，避免重覆設置 returns : 回傳物件 </summary>
		public static ucEditUI GetSingleton() 
		{
			if (m_Singleton == null) 
			{
				lock (m_objLock) 
				{
					if (m_Singleton == null) 
					{
						m_Singleton = new ucEditUI() ;
					}
				}
			}
			return m_Singleton;
		}

		/// <summary> 物件 建構式 </summary>
        public ucEditUI() 
        {
            InitializeComponent() ;
        }
        public void Initial() 
        {
            ucPosPmt.LoadIniFile();
			ucParameter.Add(this) ;
            CollectEditControls();
			OpenUIFile() ;
			UpdateControls() ;
        }


		/// <summary> 物件重置 </summary>
		public void UpdateControls() 
		{
			this.TimerInterval = 300;
			SetUIEditEnable(false) ;
            UpdateTreeView(true);
            ucJogMode1.UpdateControls();
		}

		/// <summary> 自動更新介面參數 </summary>
		protected override void ReflashTimerFunc()
        {
            if (clsCmData.g_strLanguageType == "EN")
            {
                if (btn_EditUI.Text != "Edit UI")
                {
                    btn_EditUI.Text = "Edit UI";
                    UpdateControls();
                }
            }
            else if (clsCmData.g_strLanguageType == "TC")
            {
                if (btn_EditUI.Text != "編輯 UI")
                {
                    btn_EditUI.Text = "編輯 UI";
                    UpdateControls();
                }
            }
            ucJogTeach1.ReflashControls() ;
            ucJogTeach2.ReflashControls();
            ucJogTeach3.ReflashControls();
            ucJogTeach4.ReflashControls();
            clsDIOInfo_11.ReflashControls();
            clsDIOInfo_12.ReflashControls();
            clsDIOInfo_13.ReflashControls();
            clsDIOInfo_14.ReflashControls();
            clsDIOInfo_15.ReflashControls();
            clsDIOInfo_16.ReflashControls();
            clsDIOInfo_17.ReflashControls();
            clsDIOInfo_18.ReflashControls();

			CheckIsSuperUser() ;


		}

		#endregion
		
		#region //===================== public 函式設置 ==============

        public bool IsEditUIOpen()
        {
            return ucEditUITable1.Visible;
        }

        public void ReflashUI() 
        {
            ReflashTimerFunc() ;
        }

		public void SaveFile() 
		{
			try
			{
				string strJsonData = "";
				string strFilePath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\TeachDesign.ini";

				strJsonData = JsonHelper.JsonSerialize<List<clsProFile.clsMaster>>(m_lisMaster) ;
				System.IO.File.WriteAllLines(strFilePath, new string[] { strJsonData }) ;
				formMessageBox.Show("The Profile Save Success!!") ;
			}
			catch
			{
				formMessageBox.Show("The Profile Save Error!!") ;
			}
		}

		public void OpenFile() 
		{
			m_lisMaster.Clear() ;
			try
			{
				string strFilePath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\TeachDesign.ini";

				string strJsonData = System.IO.File.ReadAllLines(strFilePath) [0];
				m_lisMaster = JsonHelper.JsonDeserialize<List<clsProFile.clsMaster>>(strJsonData, Encoding.Unicode) ;
			}
			catch
			{
				formMessageBox.Show("The Profile Open Error!!") ;
			}

		}

		delegate void SafeEventHandler<T, U>(T sender, U eventArgs) ;
		
		#endregion

		#region //===================== private 函式設置 =============
		/// <summary> 搜尋文件功能 </summary>
		private List<string> SearchFilePath(string Path, string FileName) 
		{
			List<string> Result = new List<string>() ;
			foreach (string sPath in System.IO.Directory.GetDirectories(Path) ) 
			{
				List<string> TempResult = SearchFilePath(sPath, FileName) ;
				foreach (string sTempResult in TempResult) 
				{
					Result.Add(sTempResult) ;
				}
			}
			foreach (string sFile in System.IO.Directory.GetFiles(Path) ) 
			{
				System.IO.FileInfo FileData = new System.IO.FileInfo(sFile) ;
				if (FileData.Name == FileName) 
				{
					Result.Add(sFile) ;
				}
			}
			return Result;
		}

		/// <summary> 取得Slave類別集合 </summary>
		private clsProFile.clsSlave GetSlave(string MasterName, string SlaveName) 
		{
			if (m_lisMaster.Exists(x => x._strMaster == MasterName) ) 
			{
				clsProFile.clsMaster m_clsMaster = m_lisMaster.Find(x => x._strMaster == MasterName) ;
				if (m_clsMaster._lisSlave.Exists(x => x._strNodeName == SlaveName) ) 
				{
					return m_clsMaster._lisSlave.Find(x => x._strNodeName == SlaveName) ;
				}
			}
			return null;
		}

		/// <summary> 輸入字串視窗 </summary>
		private DialogResult InputString(string title, string promptText, ref string value) 
		{
			Form form = new Form() ;
			Label label = new Label() ;
			//Label labe2 = new Label() ;
			TextBox textBox1 = new TextBox() ;
			//TextBox textBox2 = new TextBox() ;
			Button buttonOk = new Button() ;
			Button buttonCancel = new Button() ;

			form.Text = title;
			label.Text = promptText;
			//labe2.Text = "Mark : ";
			textBox1.Text = value;
			//textBox2.Text = value;

			buttonOk.Text = "OK";
			buttonCancel.Text = "Cancel";
			buttonOk.DialogResult = DialogResult.OK;
			buttonCancel.DialogResult = DialogResult.Cancel;

			label.SetBounds(14, 20, 372, 13) ;
			textBox1.SetBounds(12, 36, 372, 20) ;

			//labe2.SetBounds(14, 70, 372, 13) ;
			//textBox2.SetBounds(12, 86, 372, 20) ;

			buttonOk.SetBounds(228, 70, 75, 23) ;
			buttonCancel.SetBounds(309, 70, 75, 23) ;

			label.AutoSize = true;
			textBox1.Anchor = textBox1.Anchor | AnchorStyles.Right;

			//labe2.AutoSize = true;
			//textBox2.Anchor = textBox2.Anchor | AnchorStyles.Right;

			buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

			form.ClientSize = new Size(396, 107) ;
			form.Controls.AddRange(new Control[] { label, textBox1, /*labe2, textBox2,*/ buttonOk, buttonCancel }) ;
			form.ClientSize = new Size(Math.Max(300, label.Right + 10) , form.ClientSize.Height) ;
			form.FormBorderStyle = FormBorderStyle.FixedDialog;
			form.StartPosition = FormStartPosition.CenterScreen;
			form.MinimizeBox = false;
			form.MaximizeBox = false;
			form.AcceptButton = buttonOk;
			form.CancelButton = buttonCancel;

			DialogResult dialogResult = form.ShowDialog() ;
			value = textBox1.Text;
			//strMark = textBox2.Text;
			return dialogResult;
		}

		/// <summary> 輸入軸動列舉視窗 </summary>
		private DialogResult InputAxisBox(string title, string promptText, clsEnum.enuAxis?[] SelecteList, ref string SelectedAxis) 
		{
			Form form = new Form() ;
			Label label = new Label() ;
			ComboBox AxisBox = new ComboBox() ;
			Button buttonOk = new Button() ;
			Button buttonCancel = new Button() ;

			form.Text = title;
			label.Text = promptText;
			AxisBox.Items.Clear() ;
			foreach (clsEnum.enuAxis? tempAxsi in SelecteList) 
			{
				if (tempAxsi != null) 
				{
					AxisBox.Items.Add(tempAxsi.ToString()) ;
				}
			}
			if (AxisBox.Items.Count > 0) 
			{
				AxisBox.SelectedIndex = 0;
			}

			buttonOk.Text = "OK";
			buttonCancel.Text = "Cancel";
			buttonOk.DialogResult = DialogResult.OK;
			buttonCancel.DialogResult = DialogResult.Cancel;

			label.SetBounds(14, 20, 372, 13) ;
			AxisBox.SetBounds(12, 36, 372, 20) ;
			buttonOk.SetBounds(228, 72, 75, 23) ;
			buttonCancel.SetBounds(309, 72, 75, 23) ;

			label.AutoSize = true;
			AxisBox.Anchor = AxisBox.Anchor | AnchorStyles.Right;
			buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

			form.ClientSize = new Size(396, 107) ;
			form.Controls.AddRange(new Control[] { label, AxisBox, buttonOk, buttonCancel }) ;
			form.ClientSize = new Size(Math.Max(300, label.Right + 10) , form.ClientSize.Height) ;
			form.FormBorderStyle = FormBorderStyle.FixedDialog;
			form.StartPosition = FormStartPosition.CenterScreen;
			form.MinimizeBox = false;
			form.MaximizeBox = false;
			form.AcceptButton = buttonOk;
			form.CancelButton = buttonCancel;

			DialogResult dialogResult = form.ShowDialog() ;
			SelectedAxis = AxisBox.Text;
			return dialogResult;
		}

		/// <summary> 收集編集元件 </summary>
		private void CollectEditControls() 
		{
            foreach (Control m_Control in groupBoxJogAndTeach.Controls) 
            {
                if (m_Control is ucJogTeach) 
                {
                    m_dicObject.Add(m_Control.Name, m_Control) ;
                }
            }

			foreach (Control m_Control in groupBoxDIO.Controls) 
			{
				if (m_Control is ucDIOInfo) 
				{
					m_dicObject.Add(m_Control.Name, m_Control) ;
				}
			}
		}

		/// <summary> 讀取圖片 </summary>
		private Bitmap ReadImage(string ImagePath) 
		{
			if (m_dicBitmap.Keys.Contains(ImagePath) == false) 
			{
				try
				{
					m_dicBitmap.Add(ImagePath, new Bitmap(ImagePath) ) ;
					return m_dicBitmap[ImagePath];
				}
				catch
				{
					return null;
				}
			}
			else
			{
				return m_dicBitmap[ImagePath];
			}
		}

		/// <summary> 設置UIEdit是否可用 </summary>
		private void SetUIEditEnable(bool p_bEnable) 
		{
			IsUIEditEnable = p_bEnable;
			plUIEdit.Visible = p_bEnable;
		}

		/// <summary> 回復元件底色 </summary>
		private void ReplyControlColor() 
		{ 
			foreach (Control m_Item in m_dicObject.Values) 
			{
				m_Item.BackColor = this.BackColor;
			}
		}

		/// <summary> 更新機構圖片 </summary>
		private void UpdateGroupImage() 
		{

		}
        /// <summary> 更新機構圖片 </summary>
        private void UpdateMarkMessage() 
        {
            try
            {
                var pSlave = GetSlave(CurrentNodeMaster, CurrentNodeSlave);
                if (pSlave != null)
                {
                    RichTextMarkData.Text = pSlave._strMark;
                }
            }
            catch
            {
            }
        }

		/// <summary> 更新DIO元件 </summary>
		private void UpdateclsDIO() 
		{
			if (CurrentNodeMaster != "" && CurrentNodeSlave != "") 
			{
				clsDIOInfo_11._1clsDIOInfo = GetSlave(CurrentNodeMaster, CurrentNodeSlave)._lisDIOInfo[0];
				clsDIOInfo_12._1clsDIOInfo = GetSlave(CurrentNodeMaster, CurrentNodeSlave)._lisDIOInfo[1];
				clsDIOInfo_13._1clsDIOInfo = GetSlave(CurrentNodeMaster, CurrentNodeSlave)._lisDIOInfo[2];
				clsDIOInfo_14._1clsDIOInfo = GetSlave(CurrentNodeMaster, CurrentNodeSlave)._lisDIOInfo[3];
				clsDIOInfo_15._1clsDIOInfo = GetSlave(CurrentNodeMaster, CurrentNodeSlave)._lisDIOInfo[4];
                clsDIOInfo_16._1clsDIOInfo = GetSlave(CurrentNodeMaster, CurrentNodeSlave)._lisDIOInfo[5];
                clsDIOInfo_17._1clsDIOInfo = GetSlave(CurrentNodeMaster, CurrentNodeSlave)._lisDIOInfo[6];
                clsDIOInfo_18._1clsDIOInfo = GetSlave(CurrentNodeMaster, CurrentNodeSlave)._lisDIOInfo[7];

				UpdatePropertyGrid() ;


			}
		}

		/// <summary> 更新軸動元件 </summary>
		private void UpdateclsJogAndTeach() 
		{
			if (CurrentNodeMaster != "" && CurrentNodeSlave != "") 
			{

				ucJogTeach1._1clsJogTeachInfo = GetSlave(CurrentNodeMaster, CurrentNodeSlave)._lisJogAndTeachInfo[0];
                ucJogTeach2._1clsJogTeachInfo = GetSlave(CurrentNodeMaster, CurrentNodeSlave)._lisJogAndTeachInfo[1];
                ucJogTeach3._1clsJogTeachInfo = GetSlave(CurrentNodeMaster, CurrentNodeSlave)._lisJogAndTeachInfo[2];
                ucJogTeach4._1clsJogTeachInfo = GetSlave(CurrentNodeMaster, CurrentNodeSlave)._lisJogAndTeachInfo[3];
				UpdatePropertyGrid() ;
			}
		}

		/// <summary> 更新屬性列表 </summary>
		private void UpdatePropertyGrid() 
		{
	
		}

		/// <summary> 更新UI全部元件 </summary>
		private void UpdateNodeUI(bool NodeCollapseAll = true)
        {
            UpdateMarkMessage();
            UpdateclsDIO();
            UpdateclsJogAndTeach();
            ReplyControlColor();
            if (NodeCollapseAll == true)
            {
                TreeView_Node.CollapseAll();
            }
		}


		/// <summary> 列表轉換資料Table </summary>
		public void ConvertToDataTable<T>(IList<T> list, ref DataTable m_DataTable) 
		{
			FieldInfo[] m_FieldInfo = typeof(T).GetFields() ;

			foreach (var item in m_FieldInfo) 
			{
				if (!m_DataTable.Columns.Contains(item.Name) ) 
				{
					m_DataTable.Columns.Add(item.Name) ;
				}
			}

			foreach (T item in list) 
			{
				var values = new object[m_FieldInfo.Length];
				for (int i = 0; i < m_FieldInfo.Length; i++) 
				{
					values[i] = m_FieldInfo[i].GetValue(item) ;
				}
				m_DataTable.Rows.Add(values) ;
			}
		}

        private void SetSpeed(clsEnum.enuAxis p_Axis, int SpeedPercentage) 
        {
            double MaxVelocity = ucMotionSetting.GetMaxSpeed(p_Axis) ;
            double TAcc = ucMotionSetting.GetTacc(p_Axis) ;
            double SAcc = ucMotionSetting.GetSacc(p_Axis) ;
            double Velocity = (MaxVelocity * SpeedPercentage) / 100;
            clsMotionCtrl.SetAxisVelMm(p_Axis, Velocity, 0, TAcc, TAcc, SAcc, SAcc) ;
        }
		#region //==編集工具函式==//

		public void CheckIsSuperUser() 
		{
			if (clsCmData.g_strNowUser == "SuperUser"
				&& clsCmData.g_iNowUserLevel == 0) 
			{
                btn_EditUI.Visible = true;
                toolStrip2.Visible = IsOpenUIEditEnable;
                toolStrip3.Visible = IsOpenUIEditEnable;
                RichTextMarkData.ReadOnly = !IsOpenUIEditEnable;
			}
			else
            {
                btn_EditUI.Visible = false;
                RichTextMarkData.ReadOnly = true;
                IsOpenUIEditEnable = false;
				toolStrip2.Visible = false;
				toolStrip3.Visible = false;
			}
		}
		public void SaveUIFile() 
		{
			try
			{
				string strJsonData = "";
				string EditUIFilePath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\TeachDesign.ini";
				//strJsonData = JsonHelper.JsonSerialize<List<clsProFile.clsMaster>>(m_lisMaster) ;
				clsProFile.clsMaster[] m_ArrayJsonData = m_lisMaster.ToArray() ;
				for (int i = 0; i < m_ArrayJsonData.Count() ; i++) 
				{
					foreach (clsProFile.clsSlave item in m_ArrayJsonData[i]._lisSlave) 
					{
						strJsonData += m_ArrayJsonData[i]._strMaster + "M|";
						//strJsonData += m_ArrayJsonData[i]._strMark + "M|";
						strJsonData += JsonHelper.JsonSerialize<clsProFile.clsSlave>(item) ;
						strJsonData += "/r/n";
					}
					//strJsonData += JsonHelper.JsonSerialize<clsProFile.clsMaster>(m_ArrayJsonData[i]) + "M|";
				}
				System.IO.File.WriteAllLines(EditUIFilePath, new string[] { strJsonData }) ;
				formMessageBox.Show("The Profile Save Success!!") ;
			}
			catch
			{
				formMessageBox.Show("The Profile Save Error!!") ;
			}
		}
		public bool OpenUIFile() 
		{
			m_lisMaster.Clear() ;
			try
			{
				string EditUIFilePath = System.IO.Directory.GetCurrentDirectory() + "\\INI\\TeachDesign.ini";
                if (System.IO.File.Exists(EditUIFilePath) == true)
                {
                    string strJsonData = System.IO.File.ReadAllLines(EditUIFilePath)[0];
                    DataToJsonDeserialize(strJsonData);
                }
				//m_lisMaster = JsonHelper.JsonDeserialize<List<clsProFile.clsMaster>>(strJsonData, Encoding.Unicode) ;
				ucPosPmt.LoadIniFile() ;
				return true;
			}
			catch
			{
				formMessageBox.Show("The Profile Open Error!!") ;
			}
			return false;
		}

		public void DataToJsonDeserialize(string p_Data) 
		{
			m_lisMaster.Clear() ;
			List<string> m_ArrayJsonData = p_Data.Replace("/r/n", "^").Split('^').ToList() ;

			for (int i = 0; i < m_ArrayJsonData.Count() ; i++) 
			{
				if (m_ArrayJsonData[i] != null && m_ArrayJsonData[i] != "") 
				{
					List<string> m_Node = m_ArrayJsonData[i].Replace("M|", "^").Split('^').ToList() ;

					if (m_Node[0] != null && m_Node[0] != "") 
					{
						if (m_lisMaster.Exists(x => x._strMaster == m_Node[0]) ) 
						{
							clsProFile.clsMaster m_clsMaster = m_lisMaster.Find(x => x._strMaster == m_Node[0]) ;
                            m_clsMaster._lisSlave.Add(JsonHelper.JsonDeserialize<clsProFile.clsSlave>(m_Node[1], Encoding.Unicode) ) ;
                            #region //確認list數量
                            for (; ; ) 
                            {
                                if (m_clsMaster._lisSlave[m_clsMaster._lisSlave.Count - 1]._lisJogAndTeachInfo.Count < 4) 
                                {
                                    m_clsMaster._lisSlave[m_clsMaster._lisSlave.Count - 1]._lisJogAndTeachInfo.Add(new clsDataJogTeach() ) ;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (; ; ) 
                            {
                                if (m_clsMaster._lisSlave[m_clsMaster._lisSlave.Count - 1]._lisDIOInfo.Count < 8) 
                                {
                                    m_clsMaster._lisSlave[m_clsMaster._lisSlave.Count - 1]._lisDIOInfo.Add(new clsDataDIO() ) ;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion
                        }
						else
						{
							clsProFile.clsMaster m_clsMaster = new clsProFile.clsMaster() ;
							m_clsMaster._strMaster = m_Node[0];
							m_clsMaster._lisSlave.Add(JsonHelper.JsonDeserialize<clsProFile.clsSlave>(m_Node[1], Encoding.Unicode) ) ;
                            m_lisMaster.Add(m_clsMaster) ;
                            #region //確認list數量
                            for (; ; ) 
                            {
                                if (m_clsMaster._lisSlave[m_clsMaster._lisSlave.Count - 1]._lisJogAndTeachInfo.Count < 4) 
                                {
                                    m_clsMaster._lisSlave[m_clsMaster._lisSlave.Count - 1]._lisJogAndTeachInfo.Add(new clsDataJogTeach() ) ;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (; ; ) 
                            {
                                if (m_clsMaster._lisSlave[m_clsMaster._lisSlave.Count - 1]._lisDIOInfo.Count < 8) 
                                {
                                    m_clsMaster._lisSlave[m_clsMaster._lisSlave.Count - 1]._lisDIOInfo.Add(new clsDataDIO() ) ;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            #endregion
                        }
					}
				}
			}
		}

		private void UpdateTreeView(bool UpdateCurrentPage = false) 
		{
			bool FirstNode = true;
			if (UpdateCurrentPage) 
			{
				CurrentNodeMaster = "";
				CurrentNodeSlave = "";
				txtCurrentNode.Text = "(No Select) ";
			}
			try
			{
				TreeView_Node.Nodes.Clear() ;
				for (int i = 0; i < m_lisMaster.Count; i++) 
				{
					TreeView_Node.Nodes.Add(m_lisMaster[i]._strMaster) ;
                    TreeView_Node.Nodes[i].ImageIndex = 0;
                    TreeView_Node.Nodes[i].Name = m_lisMaster[i]._strMaster;
                    TreeView_Node.Nodes[i].Text = clsLanguage.GetTranslation(m_lisMaster[i]._strMaster);
					for (int j = 0; j < m_lisMaster[i]._lisSlave.Count; j++) 
					{
						TreeView_Node.Nodes[i].Nodes.Add(m_lisMaster[i]._lisSlave[j]._strNodeName) ;
                        TreeView_Node.Nodes[i].Nodes[j].ImageIndex = 1;
                        TreeView_Node.Nodes[i].Nodes[j].Name = m_lisMaster[i]._lisSlave[j]._strNodeName;
                        TreeView_Node.Nodes[i].Nodes[j].Text = clsLanguage.GetTranslation(m_lisMaster[i]._lisSlave[j]._strNodeName);
						if (UpdateCurrentPage && FirstNode) 
						{
							FirstNode = false;
                            CurrentNodeMaster = m_lisMaster[i]._strMaster;
							CurrentNodeSlave = m_lisMaster[i]._lisSlave[j]._strNodeName;
							txtCurrentNode.Text = clsLanguage.GetTranslation(CurrentNodeMaster) 
                                            + " - " + clsLanguage.GetTranslation(CurrentNodeSlave);
						}
					}
				}
				TreeView_Node.ExpandAll() ;
				UpdateNodeUI() ;
			}
			catch
			{
				formMessageBox.Show("Update TreeView Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
			}
		}
		private void NodeAddMaster() 
		{
			string InputMasterName = "";
			if (InputString("Add Node(Master) ", "Node Name (Master) :", ref InputMasterName) == DialogResult.OK) 
			{
				if (m_lisMaster.Exists(x => x._strMaster == InputMasterName) ) 
				{
					formMessageBox.Show("Master Node Name Already Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
					return;
				}
				clsProFile.clsMaster m_clsMaster = new clsProFile.clsMaster() ;
				m_clsMaster._strMaster = InputMasterName;
				//m_clsMaster._strMark = InputMasterMark;
				m_lisMaster.Add(m_clsMaster) ;
				UpdateTreeView(true) ;
			}
		}
		private void NodeAddSlave(string MasterName) 
		{
			string InputSlaveName = "";
			if (m_lisMaster.Exists(x => x._strMaster == MasterName) ) 
			{
				clsProFile.clsMaster m_clsMaster = m_lisMaster.Find(x => x._strMaster == MasterName) ;
				if (InputString(MasterName + ":: Add Node(Slave) ", "Node Name (Slave) :", ref InputSlaveName) == DialogResult.OK) 
				{
					if (m_clsMaster._lisSlave.Exists(x => x._strNodeName == InputSlaveName) ) 
					{
						formMessageBox.Show("Slave Node Name Already Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
						return;
					}

					clsProFile.clsSlave m_clsSlave = new clsProFile.clsSlave(InputSlaveName) ;
					m_clsMaster._lisSlave.Add(m_clsSlave) ;
					UpdateTreeView(true) ;
					return;
				}
			}
			else
			{
				formMessageBox.Show("Master Node Name Dose Not Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
			}
			return;
		}
		private void NodeCopySlave(string MasterName, string CopyName) 
		{
			try
			{
				string InputSlaveName = "";
				if (m_lisMaster.Exists(x => x._strMaster == MasterName) ) 
				{
					clsProFile.clsMaster m_clsMaster = m_lisMaster.Find(x => x._strMaster == MasterName) ;
					if (InputString(MasterName + ":: Add Node(Slave) ", "Node Name (Slave) :", ref InputSlaveName) == DialogResult.OK) 
					{
						if (m_clsMaster._lisSlave.Exists(x => x._strNodeName == InputSlaveName) ) 
						{
							formMessageBox.Show("Slave Node Name Already Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
							return;
						}
						else
						{
							//m_clsMaster._lisSlave.Add(new clsProFile.clsMaster.clsSlave(InputSlaveName) ) ;
							clsProFile.clsSlave m_OldSlave = m_clsMaster._lisSlave.Find(x => x._strNodeName == CopyName) ;
							//clsProFile.clsMaster.clsSlave m_NewSlave = m_clsMaster._lisSlave.Find(x => x._strNodeName == InputSlaveName) ;
							clsProFile.clsSlave m_NewSlave = new clsProFile.clsSlave(InputSlaveName) ;
							m_NewSlave = m_OldSlave.Clone(InputSlaveName) ;
							m_clsMaster._lisSlave.Add(m_NewSlave) ;
							UpdateTreeView(true) ;
							return;
						}
					}
				}
				else
				{
					formMessageBox.Show("Master Node Name Dose Not Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
				}
			}
			catch
			{
			}
   
		}
        private void NodeCopyMaster(string MasterName, string CopyName)
        {
            try
            {
                string InputMasterName = "";
                if (m_lisMaster.Exists(x => x._strMaster == MasterName))
                {
                    clsProFile.clsMaster m_clsMaster = m_lisMaster.Find(x => x._strMaster == MasterName);
                    if (InputString(MasterName + ":: Add Node(Master) ", "Node Name (Master) :", ref InputMasterName) == DialogResult.OK)
                    {
                        if (m_lisMaster.Exists(x => x._strMaster == InputMasterName))
                        {
                            formMessageBox.Show("Master Node Name Already Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            m_lisMaster.Add(m_clsMaster.Clone(InputMasterName));
                            UpdateTreeView(true);
                            return;
                        }
                    }
                }
                else
                {
                    formMessageBox.Show("Master Node Name Dose Not Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
            }

        }
		private void NodeMasterRename(string MasterName, string newName) 
		{
			if (m_lisMaster.Exists(x => x._strMaster == MasterName) ) 
			{
				clsProFile.clsMaster m_clsMaster = m_lisMaster.Find(x => x._strMaster == MasterName) ;
				m_clsMaster._strMaster = newName;
				//m_clsMaster._strMark = strMark;
				UpdateTreeView(true) ;
				return;
			}
			formMessageBox.Show("Master Rename Fail!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
			return;
		}
		private void NodeSlaveRename(string MasterName, string SlaveName, string newName) 
		{
			if (m_lisMaster.Exists(x => x._strMaster == MasterName) ) 
			{
				clsProFile.clsMaster m_clsMaster = m_lisMaster.Find(x => x._strMaster == MasterName) ;
				if (m_clsMaster._lisSlave.Exists(x => x._strNodeName == SlaveName) ) 
				{
					clsProFile.clsSlave m_clsSlave = m_clsMaster._lisSlave.Find(x => x._strNodeName == SlaveName) ;
					m_clsSlave._strNodeName = newName;
					//m_clsSlave._strMark = strMark;
					UpdateTreeView(true) ;
					return;
				}
			}
			formMessageBox.Show("Slave Rename Fail!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
			return;
		}
		private void NodeMasterDelete(string MasterName) 
		{
			if (m_lisMaster.Exists(x => x._strMaster == MasterName) ) 
			{
				int MasterIndex = m_lisMaster.FindIndex(x => x._strMaster == MasterName) ;
				if (m_lisMaster[MasterIndex]._lisSlave.Count == 0) 
				{
					m_lisMaster.RemoveAt(MasterIndex) ;
					UpdateTreeView() ;
					return;
				}
				else
				{
					formMessageBox.Show("Master Remove Fail!\r\nRemove Can't Have Slave!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
					return;
				}
			}
			formMessageBox.Show("Master Remove Fail!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
			return;
		}
		private void NodeSlaveDelete(string MasterName, string SlaveName) 
		{
			if (m_lisMaster.Exists(x => x._strMaster == MasterName) ) 
			{
				clsProFile.clsMaster m_clsMaster = m_lisMaster.Find(x => x._strMaster == MasterName) ;
				if (m_clsMaster._lisSlave.Exists(x => x._strNodeName == SlaveName) ) 
				{
					int m_clsSlaveIndex = m_clsMaster._lisSlave.FindIndex(x => x._strNodeName == SlaveName) ;
					m_clsMaster._lisSlave.RemoveAt(m_clsSlaveIndex) ;
					UpdateTreeView(true) ;
				}
				else
				{
					formMessageBox.Show("Slave Remove Fail!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
				}
			}
			else
			{
				formMessageBox.Show("Slave Remove Fail!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
			}
		}
		private void NodeMasterMoveUp(string MasterName) 
		{
			if (m_lisMaster.Exists(x => x._strMaster == MasterName) ) 
			{
				int m_clsMasterIndex = m_lisMaster.FindIndex(x => x._strMaster == MasterName) ;
				if (m_clsMasterIndex != 0) 
				{
					clsProFile.clsMaster m_clsMaster = m_lisMaster[m_clsMasterIndex];
					m_lisMaster[m_clsMasterIndex] = m_lisMaster[m_clsMasterIndex - 1];
					m_lisMaster[m_clsMasterIndex - 1] = m_clsMaster;
					UpdateControls() ;
				}
			}
		}
		private void NodeMasterMoveDown(string MasterName) 
		{
			if (m_lisMaster.Exists(x => x._strMaster == MasterName) ) 
			{
				int m_clsMasterIndex = m_lisMaster.FindIndex(x => x._strMaster == MasterName) ;
				if (m_clsMasterIndex != m_lisMaster.Count - 1) 
				{
					clsProFile.clsMaster m_clsMaster = m_lisMaster[m_clsMasterIndex];
					m_lisMaster[m_clsMasterIndex] = m_lisMaster[m_clsMasterIndex + 1];
					m_lisMaster[m_clsMasterIndex + 1] = m_clsMaster;
					UpdateControls() ;
				}
			}
		}
		private void NodeSlaveMoveUp(string MasterName, string SlaveName) 
		{
			if (m_lisMaster.Exists(x => x._strMaster == MasterName) ) 
			{
				clsProFile.clsMaster m_clsMaster = m_lisMaster.Find(x => x._strMaster == MasterName) ;
				if (m_clsMaster._lisSlave.Exists(x => x._strNodeName == SlaveName) ) 
				{
					int m_clsSlaveIndex = m_clsMaster._lisSlave.FindIndex(x => x._strNodeName == SlaveName) ;
					if (m_clsSlaveIndex != 0) 
					{
						clsProFile.clsSlave m_clsSlave = m_clsMaster._lisSlave[m_clsSlaveIndex];
						m_clsMaster._lisSlave[m_clsSlaveIndex] = m_clsMaster._lisSlave[m_clsSlaveIndex - 1];
						m_clsMaster._lisSlave[m_clsSlaveIndex - 1] = m_clsSlave;
						UpdateControls() ;
					}
				}
			}
		}
		private void NodeSlaveMoveDown(string MasterName, string SlaveName) 
		{
			if (m_lisMaster.Exists(x => x._strMaster == MasterName) ) 
			{
				clsProFile.clsMaster m_clsMaster = m_lisMaster.Find(x => x._strMaster == MasterName) ;
				if (m_clsMaster._lisSlave.Exists(x => x._strNodeName == SlaveName) ) 
				{
					int m_clsSlaveIndex = m_clsMaster._lisSlave.FindIndex(x => x._strNodeName == SlaveName) ;
					if (m_clsSlaveIndex != m_clsMaster._lisSlave.Count - 1) 
					{
						clsProFile.clsSlave m_clsSlave = m_clsMaster._lisSlave[m_clsSlaveIndex];
						m_clsMaster._lisSlave[m_clsSlaveIndex] = m_clsMaster._lisSlave[m_clsSlaveIndex + 1];
						m_clsMaster._lisSlave[m_clsSlaveIndex + 1] = m_clsSlave;
						UpdateControls() ;
					}
				}
			}
		}

		#endregion

		#endregion

		#region//===================== 以下為事件處理 ===============

		private void btnSave_Click(object sender, EventArgs e) 
		{
			ucPosPmt.SaveIniFile() ;
		}
		private void btnCancel_Click(object sender, EventArgs e) 
		{
			ucPosPmt.LoadIniFile() ;
		}
        private void btn_EditUI_Click(object sender, EventArgs e) 
        {
            IsOpenUIEditEnable = !IsOpenUIEditEnable;
            if (IsOpenUIEditEnable == false) 
            {
                SetUIEditEnable(false) ;
                ucEditUITable1.Visible = false;
                UpdateNodeUI() ;
            }
        }
        private void btnStop_Click(object sender, EventArgs e) 
        {
            foreach (clsEnum.enuAxis Axis in Enum.GetValues(typeof(clsEnum.enuAxis) ) ) 
            {
                clsMotionCtrl.SlowDownStop(Axis) ;
            }
        }
        private void ucEditUITable1_m_ExitEvent(object sender, EventArgs e) 
        {
            UpdateNodeUI() ;
        }
        private void clsDIOInfo_DoTrirgger(object sender, EventArgs e)
        {
            if (m_EventDIOSafeCheck != null)
            {
                ucDIOInfo Item = (ucDIOInfo)sender;
                if (Item._DOTrigger != null)
                {
                    bool Status =  !clsDioCtrl.GetDo((clsEnum.enuDo)Item._DOTrigger);
                    if (m_EventDIOSafeCheck((ucDIOInfo)sender, (clsEnum.enuDo)Item._DOTrigger,Status))
                    {
                        clsDioCtrl.SetDo((clsEnum.enuDo)Item._DOTrigger, Status);
                        if (Item._DOTrigger2 != null)
                        {
                            clsDioCtrl.SetDo((clsEnum.enuDo)Item._DOTrigger2, !Status);
                        }
                    }
                }
                else
                {
                    formMessageBox.Show("DO Value Is Null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                formMessageBox.Show("DIO Safe Click Function Empty.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void ucEditUI_ParentChanged(object sender, EventArgs e)
        {
            ChangeTreeViewBackColor(sender);
        }

        private void ucEditUI_BackColorChanged(object sender, EventArgs e)
        {
            ChangeTreeViewBackColor(sender);
        }

        private void ChangeTreeViewBackColor(object sender)
        {
            Control UC = (Control)sender;
            if (UC.BackColor == Color.Transparent)
            {
                if (UC.Parent != null)
                {
                    ChangeTreeViewBackColor(UC.Parent);
                }
                else
                {
                    UC.ParentChanged += new EventHandler(ucEditUI_ParentChanged);
                }
            }
            else
            {
                TreeView_Node.BackColor = UC.BackColor;
                UC.BackColorChanged += new EventHandler(ucEditUI_BackColorChanged);
            }
        }

        private void groupBoxDIO_SizeChanged(object sender, EventArgs e)
        {
            int gWidth = groupBoxDIO.Width;
            int DIOWidth = (gWidth - 4 * 8) / 4;
            clsDIOInfo_11.Width = DIOWidth;
            clsDIOInfo_12.Width = DIOWidth;
            clsDIOInfo_13.Width = DIOWidth;
            clsDIOInfo_14.Width = DIOWidth;
            clsDIOInfo_15.Width = DIOWidth;
            clsDIOInfo_16.Width = DIOWidth;
            clsDIOInfo_17.Width = DIOWidth;
            clsDIOInfo_18.Width = DIOWidth;

            clsDIOInfo_11.Left = 8 + (DIOWidth + 4) * 0;
            clsDIOInfo_12.Left = 8 + (DIOWidth + 4) * 1;
            clsDIOInfo_13.Left = 8 + (DIOWidth + 4) * 2;
            clsDIOInfo_14.Left = 8 + (DIOWidth + 4) * 3;
            clsDIOInfo_15.Left = 8 + (DIOWidth + 4) * 0;
            clsDIOInfo_16.Left = 8 + (DIOWidth + 4) * 1;
            clsDIOInfo_17.Left = 8 + (DIOWidth + 4) * 2;
            clsDIOInfo_18.Left = 8 + (DIOWidth + 4) * 3;

        }
        private void RichTextMarkData_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (RichTextMarkData.ReadOnly == false
                    && RichTextMarkData.Focused == true)
                {
                    if (RichTextMarkData.Text != GetSlave(CurrentNodeMaster, CurrentNodeSlave)._strMark)
                    {
                        GetSlave(CurrentNodeMaster, CurrentNodeSlave)._strMark = RichTextMarkData.Text;
                    }
                }
            }
            catch
            {
            }
        }
		#endregion

		#region//===================== 元件編集處理 =================

		#region //==編集工具觸發==//

		private void btnEditUISave_Click(object sender, EventArgs e) 
		{
			//SaveTeachUIFile() ;
            SaveUIFile() ;
            SetUIEditEnable(false) ;
            ucEditUITable1.Visible = false;
            UpdateNodeUI() ;
			UpdateControls() ;
		}
		private void btnEditUIOpen_Click(object sender, EventArgs e) 
		{
			//if (OpenTeachUIFile_Select() == true) 
			{
				OpenUIFile() ;
				UpdateControls() ;
				formMessageBox.Show("The Profile Open Success!!") ;
			}
		}
		private void btnEditNodeAddMaster_Click(object sender, EventArgs e) 
		{
			NodeAddMaster() ;
		}
		private void btnEditNodeAddSlave_Click(object sender, EventArgs e) 
		{
			if (TreeView_Node.SelectedNode != null) 
			{
				if (TreeView_Node.SelectedNode.Parent == null) 
				{
					NodeAddSlave(TreeView_Node.SelectedNode.Text) ;
				}
				else
				{
					NodeAddSlave(TreeView_Node.SelectedNode.Parent.Text) ;
				}
			}
			else
			{
				formMessageBox.Show("Please Select Master On TreeVeiw!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
			}
		}
		private void btnEditNodeCopySlave_Click(object sender, EventArgs e) 
		{
			if (TreeView_Node.SelectedNode != null) 
			{
				if (TreeView_Node.SelectedNode.Parent == null) 
				{
					NodeCopyMaster(TreeView_Node.SelectedNode.Text, TreeView_Node.SelectedNode.Text) ;
				}
				else
				{
					NodeCopySlave(TreeView_Node.SelectedNode.Parent.Text, TreeView_Node.SelectedNode.Text) ;
				}
			}
			else
			{
				formMessageBox.Show("Please Select Master On TreeVeiw!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
			}
		}
		private void btnEditNodeRename_Click(object sender, EventArgs e) 
		{
			string InputName = "";
			if (TreeView_Node.SelectedNode != null) 
			{
				if (TreeView_Node.SelectedNode.Parent == null) 
				{
					if (InputString("Rename Node(Master) ", "Node Name (Master) :", ref InputName) == DialogResult.OK) 
					{
						NodeMasterRename(TreeView_Node.SelectedNode.Text, InputName) ;
					}
				}
				else
				{
					if (InputString(TreeView_Node.SelectedNode.Parent.Name + ":: Rename Node(Slave) ", "Node Name (Slave) :", ref InputName) == DialogResult.OK) 
					{
						NodeSlaveRename(TreeView_Node.SelectedNode.Parent.Text, TreeView_Node.SelectedNode.Text, InputName) ;
					}
				}
			}
			else
			{
				formMessageBox.Show("Please Select Node On TreeVeiw!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
			}
		}
		private void btnEditNodeDelete_Click(object sender, EventArgs e) 
		{
			if (TreeView_Node.SelectedNode != null) 
			{
				if (TreeView_Node.SelectedNode.Parent != null) 
				{
					NodeSlaveDelete(TreeView_Node.SelectedNode.Parent.Text, TreeView_Node.SelectedNode.Text) ;
				}
				else
				{
					NodeMasterDelete(TreeView_Node.SelectedNode.Text) ;
				}
			}
			else
			{
				formMessageBox.Show("Please Select Slave/Master On TreeVeiw!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
			}
		}
		private void btnEditNodeMoveUp_Click(object sender, EventArgs e) 
		{
			if (TreeView_Node.SelectedNode != null) 
			{
				if (TreeView_Node.SelectedNode.Parent == null) 
				{
					NodeMasterMoveUp(TreeView_Node.SelectedNode.Text);
				}
				else
				{
					NodeSlaveMoveUp(TreeView_Node.SelectedNode.Parent.Text, TreeView_Node.SelectedNode.Text);
				}

			}
			else
			{
				formMessageBox.Show("Please Select Node On TreeVeiw!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
			}
		}
		private void btnEditNodeMoveDown_Click(object sender, EventArgs e) 
		{

			if (TreeView_Node.SelectedNode != null) 
			{
				if (TreeView_Node.SelectedNode.Parent == null) 
				{
					NodeMasterMoveDown(TreeView_Node.SelectedNode.Text) ;
				}
				else
				{
					NodeSlaveMoveDown(TreeView_Node.SelectedNode.Parent.Text, TreeView_Node.SelectedNode.Text) ;
				}

			}
			else
			{
				formMessageBox.Show("Please Select Node On TreeVeiw!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
			}
		}
		private void btnEditImage_Click(object sender, EventArgs e)
        {

        }
		private void btnUIEdit_Click(object sender, EventArgs e) 
		{
			SetUIEditEnable(!IsUIEditEnable) ;
			ReplyControlColor() ;
		}

		#endregion

		#region //==元件動作觸發==//

		private void btnNodeExpand_Click(object sender, EventArgs e) 
		{
			if (sender == tsbExpand) 
			{
				TreeView_Node.ExpandAll() ;
			}
			else if (sender == tsbCollapse) 
			{
				TreeView_Node.CollapseAll() ;
			}
		}

		private void TreeView_Node_AfterSelect(object sender, TreeViewEventArgs e) 
		{
            try
            {
                if (TreeView_Node.SelectedNode != null) 
                {
                    if (TreeView_Node.SelectedNode.Parent != null) 
                    {
                        CurrentNodeMaster = TreeView_Node.SelectedNode.Parent.Name;
                        CurrentNodeSlave = TreeView_Node.SelectedNode.Name;
                        txtCurrentNode.Text = clsLanguage.GetTranslation( CurrentNodeMaster) + " - " + 
                                        clsLanguage.GetTranslation( CurrentNodeSlave);
                        TreeView_Node.SelectedNode.SelectedImageIndex = 1;
                        UpdateNodeUI(false);
                        if (m_lisMaster.Exists(x => x._strMaster == CurrentNodeMaster) ) 
                        {
                            clsProFile.clsMaster m_clsMaster = m_lisMaster.Find(x => x._strMaster == CurrentNodeMaster) ;
                            if (m_clsMaster._lisSlave.Exists(x => x._strNodeName == CurrentNodeSlave) ) 
                            {
                                clsProFile.clsSlave m_clsSlave = m_clsMaster._lisSlave.Find(x => x._strNodeName == CurrentNodeSlave);
                                ucEditUITable1.m_lisMaster = this.m_lisMaster;
                                ucEditUITable1.UpdateDataTable(m_clsSlave, CurrentNodeMaster);
                            }
                        }
                    }
                    else
                    {
                        int m_clsMasterIndex = m_lisMaster.FindIndex(x => x._strMaster == TreeView_Node.SelectedNode.Text) ;
                        if (m_lisMaster != null) 
                        {
                            clsProFile.clsMaster m_clsMaster = m_lisMaster[m_clsMasterIndex];
                        }
                    }
                }
            }
            catch
            {
            }
		}

		private void propertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e) 
		{
			UpdatePropertyGrid() ;
		}

		private void cmbControlList_SelectedIndexChanged(object sender, EventArgs e) 
		{

		}

		private void clsControl_MouseDown(object sender, MouseEventArgs e) 
		{

		}

		private void clsJogAndTeach_DirJogAMouseEnter(object sender, EventArgs e) 
		{
            //if (((clsJogAndTeach) sender)._JobADirection != null) 
            //{
            //    cmbControlList.SelectedIndex = cmbControlList.FindString(((clsJogAndTeach) sender).Name) ;
            //    ImgJogA.BackgroundImage = ((clsJogAndTeach) sender).GetDirection(((clsJogAndTeach) sender)._JobADirection) ;
            //    ImgJogA.Location = new Point(((clsJogAndTeach) sender)._ImgJogAPoint.X, ((clsJogAndTeach) sender)._ImgJogAPoint.Y) ;
            //    ImgJogA.Visible = true;
            //    ImgJogB.Visible = false;
            //}
            //else
            //{
            //    ImgJogA.BackgroundImage = null;
            //    ImgJogA.Visible = false;
            //    ImgJogB.Visible = false;
            //}
		}
		private void clsJogAndTeach_DirJogBMouseEnter(object sender, EventArgs e) 
		{
            //if (((clsJogAndTeach) sender)._JobBDirection != null) 
            //{
            //    cmbControlList.SelectedIndex = cmbControlList.FindString(((clsJogAndTeach) sender).Name) ;
            //    ImgJogB.BackgroundImage = ((clsJogAndTeach) sender).GetDirection(((clsJogAndTeach) sender)._JobBDirection) ;
            //    ImgJogB.Location = new Point(((clsJogAndTeach) sender)._ImgJogBPoint.X, ((clsJogAndTeach) sender)._ImgJogBPoint.Y) ;
            //    ImgJogB.Visible = true;
            //    ImgJogA.Visible = false;
            //}
            //else
            //{
            //    ImgJogB.BackgroundImage = null;
            //    ImgJogB.Visible = false;
            //    ImgJogA.Visible = false;
            //}
		}

		private TreeNode old_node = null;
		private void ToolTipMark_MouseMove(object sender, MouseEventArgs e) 
		{

		}
		private void ToolTipMark_MouseDown(object sender, MouseEventArgs e) 
		{
			TreeNode node_here = TreeView_Node.GetNodeAt(e.X, e.Y) ;
			m_ToolTipMark.SetToolTip(TreeView_Node, "") ;

			Control senderObject = sender as Control;

			try
			{
				if (senderObject != null) 
				{
					if (node_here != null) 
					{
						if (node_here.Parent != null) 
						{
							if (m_lisMaster.Exists(x => x._strMaster == node_here.Parent.Text) ) 
							{
								clsProFile.clsMaster m_clsMaster = m_lisMaster.Find(x => x._strMaster == node_here.Parent.Text) ;
                                //if (m_clsMaster._lisSlave.Exists(x => x._strNodeName == node_here.Text) ) 
                                //{
                                //    m_ToolTipMark.SetToolTip(senderObject, m_clsMaster._lisSlave.Find(x => x._strNodeName == node_here.Text)._strMark) ;
                                //}
							}
						}
						else
						{
                            //if (m_lisMaster.Exists(x => x._strMaster == node_here.Text) ) 
                            //{
                            //    m_ToolTipMark.SetToolTip(senderObject, m_lisMaster.Find(x => x._strMaster == node_here.Text)._strMark) ;
                            //}
						}
					}
				}
			}
			catch
			{
			}
		}
		private void ToolTipDiscreption_MouseEnter(object sender, EventArgs e) 
		{
			Control senderObject = sender as Control;
			if (senderObject != null) 
			{
				string strText = senderObject.Text.ToString() ;
				m_ToolTipDecription.SetToolTip(senderObject, strText) ;
			}
		}


		//Event
		private void ToolTipDiscreption_MouseEnter(object sender, MouseEventArgs e) 
		{

		}

		private void tsbDataView_Click(object sender, EventArgs e) 
		{
            if (ucEditUITable1.Visible) 
            {
                ucEditUITable1.Visible = false;
            }
            else
            {
                ucEditUITable1.Visible = true;
                ucEditUITable1.BringToFront() ;
                if (m_lisMaster.Exists(x => x._strMaster == CurrentNodeMaster))
                {
                    clsProFile.clsMaster m_clsMaster = m_lisMaster.Find(x => x._strMaster == CurrentNodeMaster);
                    if (m_clsMaster._lisSlave.Exists(x => x._strNodeName == CurrentNodeSlave))
                    {
                        clsProFile.clsSlave m_clsSlave = m_clsMaster._lisSlave.Find(x => x._strNodeName == CurrentNodeSlave);
                        ucEditUITable1.m_lisMaster = this.m_lisMaster;
                        ucEditUITable1.UpdateDataTable(m_clsSlave, CurrentNodeMaster);
                    }
                }
            }
		}

		#endregion


		#endregion

        #region//===================== 委派 =====================
        public delegate bool EventDIOSafeCheck(ucDIOInfo sender, clsEnum.enuDo p_Do, bool p_Status);
        public delegate bool EventAxisSafeCheck(ucJogTeach sender, clsEnum.enuAxis p_Axis, clsDataJogTeach.ActionMode p_ActionMode, double MoveTarget) ;
        public delegate void EventRelativeClick(ucJogTeach sender, clsEnum.enuAxis p_Axis, double RelativeValue) ;
        public delegate void EventContinueMouseDown(ucJogTeach sender, clsEnum.enuAxis p_Axis, enuMoveDir p_Direction) ;
        public delegate void EventContinueMouseUp(ucJogTeach sender, clsEnum.enuAxis p_Axis) ;
        public delegate void EventGoValueClick(ucJogTeach sender, clsEnum.enuAxis p_Axis, double TargetPosition) ;
        public delegate void EventSetValueChanged(ucJogTeach sender, clsEnum.enuPosName p_PosName, double OraginalValue, double NewValue) ;
        public delegate void EventJogMouseEnter(ucJogTeach sender, Point p_Position, string s_ImagePath) ;
        public delegate void EventJogMouseLeave(ucJogTeach sender) ;

        [Description("安全檢查 - DO觸發事件")]
        [CategoryAttribute("ArtMMI"), Browsable(true), DefaultValue(false)]
        public event EventDIOSafeCheck m_EventDIOSafeCheck;
        [Description("安全檢查 - 軸是否能移動") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) , DefaultValue(false) ]
        public event EventAxisSafeCheck m_EventAxisSafeCheck;
        [Description("相對移動事件, 如果無宣告則使用內部預設函式") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventRelativeClick m_EventRelativeClick;
        [Description("連續移動事件, 如果無宣告則使用內部預設函式") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventContinueMouseDown m_EventContinueMouseDown;
        [Description("連續移動停止事件, 如果無宣告則使用內預設立函式") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventContinueMouseUp m_EventContinueMouseUp;
        [Description("絕對移動事件, 如果無宣告則使用內部預設函式") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventGoValueClick m_EventGoValueClick;
        [Description("設定參數變更事件") ]
        [CategoryAttribute("ArtMMI") , Browsable(true) ]
        public event EventSetValueChanged m_EventSetValueChanged;

        private void ucJogTeach_m_EventFollowControlChanged(ucJogTeach sender) 
        {
            int NewGap = 2;
            int SparateGap = 20;
            if (sender == ucJogTeach1)
            {
            }
            if (sender == ucJogTeach1
                || sender == ucJogTeach2)
            {
                if (ucJogTeach2._FollowControl == true)
                {
                    int Destance = ucJogTeach2._DisplayMode != clsDataJogTeach.enuDisplayMode.SetPosition ? NewGap : 0;
                    ucJogTeach2.Top = ucJogTeach1.Top + ucJogTeach1.Height + Destance;
                    ucJogTeach2.BringToFront();
                }
                else
                {
                    int Destance = ucJogTeach2._DisplayMode != clsDataJogTeach.enuDisplayMode.SetPosition ? NewGap : 0;
                    ucJogTeach2.Top = ucJogTeach1.Top + ucJogTeach1.Height + Destance + SparateGap;
                }
            }
            if (sender == ucJogTeach1
                || sender == ucJogTeach2
                || sender == ucJogTeach3)
            {
                if (ucJogTeach3._FollowControl == true)
                {
                    int Destance = ucJogTeach3._DisplayMode != clsDataJogTeach.enuDisplayMode.SetPosition ? NewGap : 0;
                    ucJogTeach3.Top = ucJogTeach2.Top + ucJogTeach2.Height + Destance;
                    ucJogTeach3.BringToFront();
                }
                else
                {
                    int Destance = ucJogTeach3._DisplayMode != clsDataJogTeach.enuDisplayMode.SetPosition ? NewGap : 0;
                    ucJogTeach3.Top = ucJogTeach2.Top + ucJogTeach2.Height + Destance + SparateGap;
                }
            }
            if (sender == ucJogTeach1
                || sender == ucJogTeach2
                || sender == ucJogTeach3
                || sender == ucJogTeach4)
            {
                if (ucJogTeach4._FollowControl == true)
                {
                    int Destance = ucJogTeach4._DisplayMode != clsDataJogTeach.enuDisplayMode.SetPosition ? NewGap : 0;
                    ucJogTeach4.Top = ucJogTeach3.Top + ucJogTeach3.Height + Destance;
                    ucJogTeach4.BringToFront();
                }
                else
                {
                    int Destance = ucJogTeach4._DisplayMode != clsDataJogTeach.enuDisplayMode.SetPosition ? NewGap : 0;
                    ucJogTeach4.Top = ucJogTeach3.Top + ucJogTeach3.Height + Destance + SparateGap;
                }
            }
        }
        private void ucJogTeach_m_EventGoSafeClick(ucJogTeach sender) 
        {
            try
            {
                if (m_EventGoValueClick != null) 
                {
                    m_EventGoValueClick(sender, (clsEnum.enuAxis) sender._AxisName_Enum, sender._SafePosition) ;
                }
                else
                {
                    if (sender._AxisName_Enum == null) 
                    {
                        formMessageBox.Show("Position setting is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
                        return;
                    }
                    if (sender._SafePosName_Enum == null) 
                    {
                        double TargetValue = sender._SafePosition;
                        if (MessageBox.Show("Are you sure Axis: \"" + sender._AxisName_Enum.ToString() + "\" \r\n"
                            + "Safe Position Value : " + TargetValue.ToString() 
                            , "Safe Button Click", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            clsLog.Log("ButtonLog", "(Motion Teach) Safe Button Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                            + " => Safe Position Value = " + TargetValue) ;
                            GoValueClick(sender, (clsEnum.enuAxis) sender._AxisName_Enum, TargetValue) ;
                        }
                    }
                    else
                    {
                        double TargetValue = ucPosPmt.GetValueDouble((clsEnum.enuPosName) sender._SafePosName_Enum) ;
                        if (MessageBox.Show("Are you sure Axis: \"" + sender._AxisName_Enum.ToString() + "\" \r\n"
                            + "Safe Position Name : \"" + sender._SafePosName_Enum.ToString() + "\"\r\n"
                            + "Safe Position Value : " + TargetValue.ToString() 
                            , "Safe Button Click", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) Safe Button Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                            + " => Safe Position Name = " + sender._SafePosName_Enum.ToString() 
                                            + " => Safe Position Value = " + TargetValue) ;
                            GoValueClick(sender, (clsEnum.enuAxis) sender._AxisName_Enum, TargetValue) ;
                        }
                    }
                }
            }
            catch
            {
            }

        }
        private void ucJogTeach_m_EventGoValueClick(ucJogTeach sender) 
        {
            try
            {
                if (m_EventGoValueClick != null) 
                {
                    m_EventGoValueClick(sender, (clsEnum.enuAxis) sender._AxisName_Enum,
                        ucPosPmt.GetValueDouble((clsEnum.enuPosName) sender._PosName_Enum) ) ;
                }
                else
                {
                    if (sender._PosName_Enum == null
                        || sender._AxisName_Enum == null) 
                    {
                        formMessageBox.Show("Position setting is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
                        return;
                    }
                    double TargetValue = ucPosPmt.GetValueDouble((clsEnum.enuPosName) sender._PosName_Enum) ;
                    if (MessageBox.Show("Are you sure Axis: \"" + sender._AxisName_Enum.ToString() + "\" \r\n"
                        + "Position Name : \"" + sender._PosName_Enum.ToString() + "\"\r\n"
                        + "Position Value : " + TargetValue.ToString() , "Go Button Click", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        clsLog.Log("ButtonLog"
                                    , "(Motion Teach) Go Button Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                        + " => Position Name = " + sender._PosName_Enum.ToString() 
                                        + " => Position Value = " + TargetValue) ;
                        GoValueClick(sender, (clsEnum.enuAxis) sender._AxisName_Enum, TargetValue) ;
                    }
                }
            }
            catch
            {
            }
        }
        private void ucJogTeach_m_EventJogAMouseEnter(ucJogTeach sender, Point p_Position, string s_ImagePath) 
        {

        }
        private void ucJogTeach_m_EventJogAMouseLeave(ucJogTeach sender) 
        {

        }
        private void ucJogTeach_m_EventJogBMouseEnter(ucJogTeach sender, Point p_Position, string s_ImagePath) 
        {

        }
        private void ucJogTeach_m_EventJogBMouseLeave(ucJogTeach sender) 
        {

        }
        private void ucJogTeach_m_EventJogAMouseUp(ucJogTeach sender) 
        {
            try
            {
                if (ucJogMode1._IsContinueMode == true) 
                {
                    ContinueMouseUp(sender, (clsEnum.enuAxis) sender._AxisName_Enum) ;
                    if (m_EventContinueMouseUp != null) 
                    {
                        m_EventContinueMouseUp(sender, (clsEnum.enuAxis) sender._AxisName_Enum) ;
                    }
                }
            }
            catch
            {
            }
        }
        private void ucJogTeach_m_EventJogBMouseUp(ucJogTeach sender) 
        {
            try
            {
                if (ucJogMode1._IsContinueMode == true) 
                {
                    ContinueMouseUp(sender, (clsEnum.enuAxis) sender._AxisName_Enum) ;
                    if (m_EventContinueMouseUp != null) 
                    {
                        m_EventContinueMouseUp(sender, (clsEnum.enuAxis) sender._AxisName_Enum) ;
                    }
                }
            }
            catch
            {
            }
        }
        private void ucJogTeach_m_EventJogAMouseDown(ucJogTeach sender) 
        {
            try
            {
                if (sender._1clsJogTeachInfo._AxisEnum_String == null) 
                {
                    formMessageBox.Show("Position setting is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
                    return;
                }
                if (ucJogMode1._IsContinueMode == true) 
                {
                    if (m_EventContinueMouseDown != null) 
                    {
                        if (sender._JogMoveDir == enuMoveDir.Positive)
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogA Continue Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Direction = " + enuMoveDir.Positive.ToString() ) ;
                            m_EventContinueMouseDown(sender, (clsEnum.enuAxis) sender._AxisName_Enum, enuMoveDir.Positive) ;
                        }
                        else
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogA Continue Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Direction = " + enuMoveDir.Negative.ToString() ) ;
                            m_EventContinueMouseDown(sender, (clsEnum.enuAxis) sender._AxisName_Enum, enuMoveDir.Negative) ;
                        }
                    }
                    else
                    {
                        if (sender._JogMoveDir == enuMoveDir.Positive)
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogA Continue Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Direction = " + enuMoveDir.Positive.ToString() ) ;
                            ContinueMouseDown(sender, (clsEnum.enuAxis) sender._AxisName_Enum, enuMoveDir.Positive) ;
                        }
                        else
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogA Continue Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Direction = " + enuMoveDir.Negative.ToString() ) ;
                            ContinueMouseDown(sender, (clsEnum.enuAxis) sender._AxisName_Enum, enuMoveDir.Negative) ;
                        }
                    }
                }
                else
                {
                    if (m_EventRelativeClick != null) 
                    {
                        if (sender._JogMoveDir == enuMoveDir.Positive) 
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogA Relative Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Relative Value = " + ucJogMode1._RelativeValue.ToString() ) ;
                            m_EventRelativeClick(sender, (clsEnum.enuAxis) sender._AxisName_Enum, ucJogMode1._RelativeValue) ;
                        }
                        else
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogA Relative Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Relative Value = -" + ucJogMode1._RelativeValue.ToString() ) ;
                            m_EventRelativeClick(sender, (clsEnum.enuAxis) sender._AxisName_Enum, -ucJogMode1._RelativeValue) ;
                        }
                    }
                    else
                    {
                        if (sender._JogMoveDir == enuMoveDir.Positive)
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogA Relative Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Relative Value = " + ucJogMode1._RelativeValue.ToString() ) ;
                            RelativeMove(sender, (clsEnum.enuAxis) sender._AxisName_Enum, ucJogMode1._RelativeValue) ;
                        }
                        else
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogA Relative Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Relative Value = -" + ucJogMode1._RelativeValue.ToString() ) ;
                            RelativeMove(sender, (clsEnum.enuAxis) sender._AxisName_Enum, -ucJogMode1._RelativeValue) ;
                        }
                    }
                }
            }
            catch
            {
            }
        }
        private void ucJogTeach_m_EventJogBMouseDown(ucJogTeach sender) 
        {
            try
            {
                if (sender._1clsJogTeachInfo._AxisEnum_String == null) 
                {
                    formMessageBox.Show("Position setting is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning) ;
                    return;
                }
                if (ucJogMode1._IsContinueMode == true) 
                {
                    if (m_EventContinueMouseDown != null) 
                    {
                        if (sender._JogMoveDir == enuMoveDir.Positive)
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogB Continue Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Direction = " + enuMoveDir.Negative.ToString() ) ;
                            m_EventContinueMouseDown(sender, (clsEnum.enuAxis) sender._AxisName_Enum, enuMoveDir.Negative) ;
                        }
                        else
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogB Continue Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Direction = " + enuMoveDir.Positive.ToString() ) ;
                            m_EventContinueMouseDown(sender, (clsEnum.enuAxis) sender._AxisName_Enum, enuMoveDir.Positive) ;
                        }
                    }
                    else
                    {
                        if (sender._JogMoveDir == enuMoveDir.Positive)
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogB Continue Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Direction = " + enuMoveDir.Negative.ToString() ) ;
                            ContinueMouseDown(sender, (clsEnum.enuAxis) sender._AxisName_Enum, enuMoveDir.Negative) ;
                        }
                        else
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogB Continue Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Direction = " + enuMoveDir.Positive.ToString() ) ;
                            ContinueMouseDown(sender, (clsEnum.enuAxis) sender._AxisName_Enum, enuMoveDir.Positive) ;
                        }
                    }
                }
                else
                {
                    if (m_EventRelativeClick != null) 
                    {
                        if (sender._JogMoveDir == enuMoveDir.Positive)
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogB Relative Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Relative Value = -" + ucJogMode1._RelativeValue.ToString() ) ;
                            m_EventRelativeClick(sender, (clsEnum.enuAxis) sender._AxisName_Enum, -ucJogMode1._RelativeValue) ;
                        }
                        else
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogB Relative Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Relative Value = " + ucJogMode1._RelativeValue.ToString() ) ;
                            m_EventRelativeClick(sender, (clsEnum.enuAxis) sender._AxisName_Enum, ucJogMode1._RelativeValue) ;
                        }
                    }
                    else
                    {
                        if (sender._JogMoveDir == enuMoveDir.Positive)
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogB Relative Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Relative Value = -" + ucJogMode1._RelativeValue.ToString() ) ;
                            RelativeMove(sender, (clsEnum.enuAxis) sender._AxisName_Enum, -ucJogMode1._RelativeValue) ;
                        }
                        else
                        {
                            clsLog.Log("ButtonLog"
                                        , "(Motion Teach) JogB Relative Click => Axis Name : " + sender._AxisName_Enum.ToString() 
                                                   + " => Relative Value = " + ucJogMode1._RelativeValue.ToString() ) ;
                            RelativeMove(sender, (clsEnum.enuAxis) sender._AxisName_Enum, ucJogMode1._RelativeValue) ;
                        }
                    }
                }
            }
            catch
            {
            }
        }
        private void ucJogTeach_m_EventSetValueChanged(ucJogTeach sender, clsEnum.enuPosName p_PosName, double OraginalValue, double NewValue) 
        {
            if (m_EventSetValueChanged != null) 
            {
                m_EventSetValueChanged(sender, p_PosName, OraginalValue, NewValue) ;
            }
            clsLog.Log("ButtonLog"
                        , "(Motion Teach) Set Value Click => Axis Name : " + sender._AxisName_Enum.ToString()
                            + " => Position Name = " + sender._PosName_Enum.ToString()
                            + " => Position Value = " + OraginalValue + " -> " + NewValue);
            clsLog.Log("ButtonLog", "(Motion Teach) Set Value Click => Axis Name : " + sender._AxisName_Enum.ToString()
                            + " => Position Name = " + sender._PosName_Enum.ToString()
                            + " => Position Value = " + OraginalValue + " -> " + NewValue);
        }

        #endregion

        #region//===================== Function Event =====================

        private void SetSpeed(clsEnum.enuAxis p_Axis) 
        {
            int SpeedPercentage = ucJogMode1._TeachSpeed;
            double MaxVelocity = ucMotionSetting.GetMaxSpeed(p_Axis) ;
            double TAcc = ucMotionSetting.GetTacc(p_Axis) ;
            double SAcc = ucMotionSetting.GetSacc(p_Axis) ;
            double Velocity = (MaxVelocity * SpeedPercentage) / 100;
            clsMotionCtrl.SetAxisVelMm(p_Axis, Velocity, 0, TAcc, TAcc, SAcc, SAcc) ;
        }
        private void GoValueClick(ucJogTeach sender, clsEnum.enuAxis p_Axis, double TargetPosition) 
        {
            if (m_EventAxisSafeCheck != null) 
            {
                if (m_EventAxisSafeCheck(sender, p_Axis, clsDataJogTeach.ActionMode.AbsoluteMove, TargetPosition) == true) 
                {
                    SetSpeed(p_Axis) ;
                    clsMotionCtrl.StartMoveMmA(p_Axis, TargetPosition) ;
                }
            }
            else
            {
                formMessageBox.Show("No Set Safe Check Function") ;
            }
        }
        private void RelativeMove(ucJogTeach sender, clsEnum.enuAxis p_Axis, double RelativeValue) 
        {
            if (m_EventAxisSafeCheck != null) 
            {
                if (m_EventAxisSafeCheck(sender, p_Axis, clsDataJogTeach.ActionMode.RelativeMove, RelativeValue) == true) 
                {
                    SetSpeed(p_Axis) ;
                    clsMotionCtrl.StartMoveMmR(p_Axis, RelativeValue) ;
                }
            }
            else
            {
                formMessageBox.Show("No Set Safe Check Function") ;
            }
        }
        private void ContinueMouseDown(ucJogTeach sender, clsEnum.enuAxis p_Axis, enuMoveDir p_Direction) 
        {
            if (m_EventAxisSafeCheck != null) 
            {
                double MoveTarget = p_Direction == enuMoveDir.Positive ? 9999 : -9999;
                if (m_EventAxisSafeCheck(sender, p_Axis, clsDataJogTeach.ActionMode.ContinueMove, MoveTarget) == true) 
                {
                    SetSpeed(p_Axis) ;
                    clsMotionCtrl.KeepMove(p_Axis, p_Direction) ;
                }
            }
            else
            {
                formMessageBox.Show("No Set Safe Check Function") ;
            }
        }
        private void ContinueMouseUp(ucJogTeach sender, clsEnum.enuAxis p_Axis) 
        {
            clsMotionCtrl.SlowDownStop(p_Axis) ;
        }

        #endregion
    }
}

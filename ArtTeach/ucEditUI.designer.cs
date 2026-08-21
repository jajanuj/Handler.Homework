namespace ArtTeach
{
    partial class ucEditUI
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing) 
        {
            if (disposing && (components != null) ) 
            {
                components.Dispose() ;
            }
            base.Dispose(disposing) ;
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent() 
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("First Position L");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("First Position R");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Barcode");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Rotary Load");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Load", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Get");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Barcode");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Put");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Unload", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8});
            ArtTeach.clsDataJogTeach clsDataJogTeach1 = new ArtTeach.clsDataJogTeach();
            ArtTeach.clsDataJogTeach clsDataJogTeach2 = new ArtTeach.clsDataJogTeach();
            ArtTeach.clsDataJogTeach clsDataJogTeach3 = new ArtTeach.clsDataJogTeach();
            ArtTeach.clsDataJogTeach clsDataJogTeach4 = new ArtTeach.clsDataJogTeach();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucEditUI));
            ArtTeach.clsDataDIO clsDataDIO1 = new ArtTeach.clsDataDIO();
            ArtTeach.clsDataDIO clsDataDIO2 = new ArtTeach.clsDataDIO();
            ArtTeach.clsDataDIO clsDataDIO3 = new ArtTeach.clsDataDIO();
            ArtTeach.clsDataDIO clsDataDIO4 = new ArtTeach.clsDataDIO();
            ArtTeach.clsDataDIO clsDataDIO5 = new ArtTeach.clsDataDIO();
            ArtTeach.clsDataDIO clsDataDIO6 = new ArtTeach.clsDataDIO();
            ArtTeach.clsDataDIO clsDataDIO7 = new ArtTeach.clsDataDIO();
            ArtTeach.clsDataDIO clsDataDIO8 = new ArtTeach.clsDataDIO();
            this.TreeView_Node = new System.Windows.Forms.TreeView();
            this.groupBoxJogAndTeach = new System.Windows.Forms.GroupBox();
            this.ucJogTeach4 = new ArtTeach.ucJogTeach();
            this.ucJogTeach3 = new ArtTeach.ucJogTeach();
            this.ucJogTeach2 = new ArtTeach.ucJogTeach();
            this.ucJogTeach1 = new ArtTeach.ucJogTeach();
            this.ucJogMode1 = new ArtTeach.ucJogMode();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupImage = new System.Windows.Forms.GroupBox();
            this.plUIEdit = new System.Windows.Forms.Panel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.txtCurrentNode = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditUISave = new System.Windows.Forms.ToolStripButton();
            this.btnEditUIOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExpand = new System.Windows.Forms.ToolStripButton();
            this.tsbCollapse = new System.Windows.Forms.ToolStripButton();
            this.groupBoxDIO = new System.Windows.Forms.GroupBox();
            this.clsDIOInfo_18 = new ArtTeach.ucDIOInfo();
            this.clsDIOInfo_17 = new ArtTeach.ucDIOInfo();
            this.clsDIOInfo_16 = new ArtTeach.ucDIOInfo();
            this.clsDIOInfo_15 = new ArtTeach.ucDIOInfo();
            this.clsDIOInfo_14 = new ArtTeach.ucDIOInfo();
            this.clsDIOInfo_13 = new ArtTeach.ucDIOInfo();
            this.clsDIOInfo_12 = new ArtTeach.ucDIOInfo();
            this.clsDIOInfo_11 = new ArtTeach.ucDIOInfo();
            this.btn_EditUI = new System.Windows.Forms.Button();
            this.ucEditUITable1 = new ArtTeach.ucEditUITable();
            this.RichTextMarkData = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxJogAndTeach.SuspendLayout();
            this.groupImage.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.groupBoxDIO.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeView_Node
            // 
            this.TreeView_Node.AllowDrop = true;
            this.TreeView_Node.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeView_Node.BackColor = System.Drawing.SystemColors.Control;
            this.TreeView_Node.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView_Node.Cursor = System.Windows.Forms.Cursors.Default;
            this.TreeView_Node.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeView_Node.ForeColor = System.Drawing.Color.Black;
            this.TreeView_Node.FullRowSelect = true;
            this.TreeView_Node.HotTracking = true;
            this.TreeView_Node.Indent = 30;
            this.TreeView_Node.ItemHeight = 40;
            this.TreeView_Node.LineColor = System.Drawing.Color.White;
            this.TreeView_Node.Location = new System.Drawing.Point(5, 20);
            this.TreeView_Node.Name = "TreeView_Node";
            treeNode1.ImageIndex = 1;
            treeNode1.Name = "Node1";
            treeNode1.Text = "First Position L";
            treeNode2.ImageIndex = 1;
            treeNode2.Name = "Node1";
            treeNode2.Text = "First Position R";
            treeNode3.ImageIndex = 1;
            treeNode3.Name = "Node2";
            treeNode3.Text = "Barcode";
            treeNode4.ImageIndex = 1;
            treeNode4.Name = "Node4";
            treeNode4.Text = "Rotary Load";
            treeNode5.BackColor = System.Drawing.Color.Aquamarine;
            treeNode5.ForeColor = System.Drawing.SystemColors.WindowText;
            treeNode5.Name = "Node0";
            treeNode5.Text = "Load";
            treeNode6.ImageIndex = 1;
            treeNode6.Name = "Node6";
            treeNode6.Text = "Get";
            treeNode7.ImageIndex = 1;
            treeNode7.Name = "Node8";
            treeNode7.Text = "Barcode";
            treeNode8.ImageIndex = 1;
            treeNode8.Name = "Node9";
            treeNode8.Text = "Put";
            treeNode9.Name = "Node5";
            treeNode9.Text = "Unload";
            this.TreeView_Node.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode9});
            this.TreeView_Node.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TreeView_Node.SelectedImageKey = "1369598113_folder_add.png";
            this.TreeView_Node.ShowNodeToolTips = true;
            this.TreeView_Node.Size = new System.Drawing.Size(159, 336);
            this.TreeView_Node.TabIndex = 1105;
            this.TreeView_Node.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Node_AfterSelect);
            this.TreeView_Node.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolTipMark_MouseDown);
            // 
            // groupBoxJogAndTeach
            // 
            this.groupBoxJogAndTeach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxJogAndTeach.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxJogAndTeach.Controls.Add(this.ucJogTeach4);
            this.groupBoxJogAndTeach.Controls.Add(this.ucJogTeach3);
            this.groupBoxJogAndTeach.Controls.Add(this.ucJogTeach2);
            this.groupBoxJogAndTeach.Controls.Add(this.ucJogTeach1);
            this.groupBoxJogAndTeach.Controls.Add(this.ucJogMode1);
            this.groupBoxJogAndTeach.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxJogAndTeach.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxJogAndTeach.ForeColor = System.Drawing.Color.DimGray;
            this.groupBoxJogAndTeach.Location = new System.Drawing.Point(623, 20);
            this.groupBoxJogAndTeach.Name = "groupBoxJogAndTeach";
            this.groupBoxJogAndTeach.Size = new System.Drawing.Size(307, 430);
            this.groupBoxJogAndTeach.TabIndex = 1101;
            this.groupBoxJogAndTeach.TabStop = false;
            this.groupBoxJogAndTeach.Text = "Setting and Control";
            // 
            // ucJogTeach4
            // 
            this.ucJogTeach4._1clsJogTeachInfo = clsDataJogTeach1;
            this.ucJogTeach4._AxisName_Enum = null;
            this.ucJogTeach4._AxisTitle_Str = "Axis Name";
            this.ucJogTeach4._AxisUnit_String = "mm";
            this.ucJogTeach4._DisplayMode = ArtTeach.clsDataJogTeach.enuDisplayMode.SetPosition;
            this.ucJogTeach4._FollowControl = false;
            this.ucJogTeach4._JogAImg_String = null;
            this.ucJogTeach4._JogAImgPoint = new System.Drawing.Point(0, 0);
            this.ucJogTeach4._JogAText_String = "Jog+";
            this.ucJogTeach4._JogBImg_String = null;
            this.ucJogTeach4._JogBImgPoint = new System.Drawing.Point(0, 0);
            this.ucJogTeach4._JogBText_String = "Jog-";
            this.ucJogTeach4._JogTeach_Enable = true;
            this.ucJogTeach4._PosName_Enum = null;
            this.ucJogTeach4._PosName_String = "Position Name";
            this.ucJogTeach4._PosSet_Enable = true;
            this.ucJogTeach4._SafePosition = 0D;
            this.ucJogTeach4._SafePosName_Enum = null;
            this.ucJogTeach4.BackColor = System.Drawing.Color.Transparent;
            this.ucJogTeach4.Location = new System.Drawing.Point(10, 333);
            this.ucJogTeach4.MaximumSize = new System.Drawing.Size(288, 55);
            this.ucJogTeach4.MinimumSize = new System.Drawing.Size(288, 55);
            this.ucJogTeach4.Name = "ucJogTeach4";
            this.ucJogTeach4.Size = new System.Drawing.Size(288, 55);
            this.ucJogTeach4.TabIndex = 1133;
            this.ucJogTeach4.m_EventJogAMouseDown += new ArtTeach.ucJogTeach.EventMouseDown(this.ucJogTeach_m_EventJogAMouseDown);
            this.ucJogTeach4.m_EventJogAMouseUp += new ArtTeach.ucJogTeach.EventMouseUp(this.ucJogTeach_m_EventJogAMouseUp);
            this.ucJogTeach4.m_EventJogBMouseDown += new ArtTeach.ucJogTeach.EventMouseDown(this.ucJogTeach_m_EventJogBMouseDown);
            this.ucJogTeach4.m_EventJogBMouseUp += new ArtTeach.ucJogTeach.EventMouseUp(this.ucJogTeach_m_EventJogBMouseUp);
            this.ucJogTeach4.m_EventGoValueClick += new ArtTeach.ucJogTeach.EventGoValueClick(this.ucJogTeach_m_EventGoValueClick);
            this.ucJogTeach4.m_EventGoSafeClick += new ArtTeach.ucJogTeach.EventGoSafeClick(this.ucJogTeach_m_EventGoSafeClick);
            this.ucJogTeach4.m_EventSetValueChanged += new ArtTeach.ucJogTeach.EventSetValueChanged(this.ucJogTeach_m_EventSetValueChanged);
            this.ucJogTeach4.m_EventJogAMouseEnter += new ArtTeach.ucJogTeach.EventJogMouseEnter(this.ucJogTeach_m_EventJogAMouseEnter);
            this.ucJogTeach4.m_EventJogBMouseEnter += new ArtTeach.ucJogTeach.EventJogMouseEnter(this.ucJogTeach_m_EventJogBMouseEnter);
            this.ucJogTeach4.m_EventJogAMouseLeave += new ArtTeach.ucJogTeach.EventJogMouseLeave(this.ucJogTeach_m_EventJogAMouseLeave);
            this.ucJogTeach4.m_EventJogBMouseLeave += new ArtTeach.ucJogTeach.EventJogMouseLeave(this.ucJogTeach_m_EventJogBMouseLeave);
            this.ucJogTeach4.m_EventFollowControlChanged += new ArtTeach.ucJogTeach.EventFollowControlChanged(this.ucJogTeach_m_EventFollowControlChanged);
            // 
            // ucJogTeach3
            // 
            this.ucJogTeach3._1clsJogTeachInfo = clsDataJogTeach2;
            this.ucJogTeach3._AxisName_Enum = null;
            this.ucJogTeach3._AxisTitle_Str = "Axis Name";
            this.ucJogTeach3._AxisUnit_String = "mm";
            this.ucJogTeach3._DisplayMode = ArtTeach.clsDataJogTeach.enuDisplayMode.SetPosition;
            this.ucJogTeach3._FollowControl = false;
            this.ucJogTeach3._JogAImg_String = null;
            this.ucJogTeach3._JogAImgPoint = new System.Drawing.Point(0, 0);
            this.ucJogTeach3._JogAText_String = "Jog+";
            this.ucJogTeach3._JogBImg_String = null;
            this.ucJogTeach3._JogBImgPoint = new System.Drawing.Point(0, 0);
            this.ucJogTeach3._JogBText_String = "Jog-";
            this.ucJogTeach3._JogTeach_Enable = true;
            this.ucJogTeach3._PosName_Enum = null;
            this.ucJogTeach3._PosName_String = "Position Name";
            this.ucJogTeach3._PosSet_Enable = true;
            this.ucJogTeach3._SafePosition = 0D;
            this.ucJogTeach3._SafePosName_Enum = null;
            this.ucJogTeach3.BackColor = System.Drawing.Color.Transparent;
            this.ucJogTeach3.Location = new System.Drawing.Point(10, 279);
            this.ucJogTeach3.MaximumSize = new System.Drawing.Size(288, 55);
            this.ucJogTeach3.MinimumSize = new System.Drawing.Size(288, 55);
            this.ucJogTeach3.Name = "ucJogTeach3";
            this.ucJogTeach3.Size = new System.Drawing.Size(288, 55);
            this.ucJogTeach3.TabIndex = 1132;
            this.ucJogTeach3.m_EventJogAMouseDown += new ArtTeach.ucJogTeach.EventMouseDown(this.ucJogTeach_m_EventJogAMouseDown);
            this.ucJogTeach3.m_EventJogAMouseUp += new ArtTeach.ucJogTeach.EventMouseUp(this.ucJogTeach_m_EventJogAMouseUp);
            this.ucJogTeach3.m_EventJogBMouseDown += new ArtTeach.ucJogTeach.EventMouseDown(this.ucJogTeach_m_EventJogBMouseDown);
            this.ucJogTeach3.m_EventJogBMouseUp += new ArtTeach.ucJogTeach.EventMouseUp(this.ucJogTeach_m_EventJogBMouseUp);
            this.ucJogTeach3.m_EventGoValueClick += new ArtTeach.ucJogTeach.EventGoValueClick(this.ucJogTeach_m_EventGoValueClick);
            this.ucJogTeach3.m_EventGoSafeClick += new ArtTeach.ucJogTeach.EventGoSafeClick(this.ucJogTeach_m_EventGoSafeClick);
            this.ucJogTeach3.m_EventSetValueChanged += new ArtTeach.ucJogTeach.EventSetValueChanged(this.ucJogTeach_m_EventSetValueChanged);
            this.ucJogTeach3.m_EventJogAMouseEnter += new ArtTeach.ucJogTeach.EventJogMouseEnter(this.ucJogTeach_m_EventJogAMouseEnter);
            this.ucJogTeach3.m_EventJogBMouseEnter += new ArtTeach.ucJogTeach.EventJogMouseEnter(this.ucJogTeach_m_EventJogBMouseEnter);
            this.ucJogTeach3.m_EventJogAMouseLeave += new ArtTeach.ucJogTeach.EventJogMouseLeave(this.ucJogTeach_m_EventJogAMouseLeave);
            this.ucJogTeach3.m_EventJogBMouseLeave += new ArtTeach.ucJogTeach.EventJogMouseLeave(this.ucJogTeach_m_EventJogBMouseLeave);
            this.ucJogTeach3.m_EventFollowControlChanged += new ArtTeach.ucJogTeach.EventFollowControlChanged(this.ucJogTeach_m_EventFollowControlChanged);
            // 
            // ucJogTeach2
            // 
            this.ucJogTeach2._1clsJogTeachInfo = clsDataJogTeach3;
            this.ucJogTeach2._AxisName_Enum = null;
            this.ucJogTeach2._AxisTitle_Str = "Axis Name";
            this.ucJogTeach2._AxisUnit_String = "mm";
            this.ucJogTeach2._DisplayMode = ArtTeach.clsDataJogTeach.enuDisplayMode.SetPosition;
            this.ucJogTeach2._FollowControl = false;
            this.ucJogTeach2._JogAImg_String = null;
            this.ucJogTeach2._JogAImgPoint = new System.Drawing.Point(0, 0);
            this.ucJogTeach2._JogAText_String = "Jog+";
            this.ucJogTeach2._JogBImg_String = null;
            this.ucJogTeach2._JogBImgPoint = new System.Drawing.Point(0, 0);
            this.ucJogTeach2._JogBText_String = "Jog-";
            this.ucJogTeach2._JogTeach_Enable = true;
            this.ucJogTeach2._PosName_Enum = null;
            this.ucJogTeach2._PosName_String = "Position Name";
            this.ucJogTeach2._PosSet_Enable = true;
            this.ucJogTeach2._SafePosition = 0D;
            this.ucJogTeach2._SafePosName_Enum = null;
            this.ucJogTeach2.BackColor = System.Drawing.Color.Transparent;
            this.ucJogTeach2.Location = new System.Drawing.Point(10, 225);
            this.ucJogTeach2.MaximumSize = new System.Drawing.Size(288, 55);
            this.ucJogTeach2.MinimumSize = new System.Drawing.Size(288, 55);
            this.ucJogTeach2.Name = "ucJogTeach2";
            this.ucJogTeach2.Size = new System.Drawing.Size(288, 55);
            this.ucJogTeach2.TabIndex = 1131;
            this.ucJogTeach2.m_EventJogAMouseDown += new ArtTeach.ucJogTeach.EventMouseDown(this.ucJogTeach_m_EventJogAMouseDown);
            this.ucJogTeach2.m_EventJogAMouseUp += new ArtTeach.ucJogTeach.EventMouseUp(this.ucJogTeach_m_EventJogAMouseUp);
            this.ucJogTeach2.m_EventJogBMouseDown += new ArtTeach.ucJogTeach.EventMouseDown(this.ucJogTeach_m_EventJogBMouseDown);
            this.ucJogTeach2.m_EventJogBMouseUp += new ArtTeach.ucJogTeach.EventMouseUp(this.ucJogTeach_m_EventJogBMouseUp);
            this.ucJogTeach2.m_EventGoValueClick += new ArtTeach.ucJogTeach.EventGoValueClick(this.ucJogTeach_m_EventGoValueClick);
            this.ucJogTeach2.m_EventGoSafeClick += new ArtTeach.ucJogTeach.EventGoSafeClick(this.ucJogTeach_m_EventGoSafeClick);
            this.ucJogTeach2.m_EventSetValueChanged += new ArtTeach.ucJogTeach.EventSetValueChanged(this.ucJogTeach_m_EventSetValueChanged);
            this.ucJogTeach2.m_EventJogAMouseEnter += new ArtTeach.ucJogTeach.EventJogMouseEnter(this.ucJogTeach_m_EventJogAMouseEnter);
            this.ucJogTeach2.m_EventJogBMouseEnter += new ArtTeach.ucJogTeach.EventJogMouseEnter(this.ucJogTeach_m_EventJogBMouseEnter);
            this.ucJogTeach2.m_EventJogAMouseLeave += new ArtTeach.ucJogTeach.EventJogMouseLeave(this.ucJogTeach_m_EventJogAMouseLeave);
            this.ucJogTeach2.m_EventJogBMouseLeave += new ArtTeach.ucJogTeach.EventJogMouseLeave(this.ucJogTeach_m_EventJogBMouseLeave);
            this.ucJogTeach2.m_EventFollowControlChanged += new ArtTeach.ucJogTeach.EventFollowControlChanged(this.ucJogTeach_m_EventFollowControlChanged);
            // 
            // ucJogTeach1
            // 
            this.ucJogTeach1._1clsJogTeachInfo = clsDataJogTeach4;
            this.ucJogTeach1._AxisName_Enum = null;
            this.ucJogTeach1._AxisTitle_Str = "Axis Name";
            this.ucJogTeach1._AxisUnit_String = "mm";
            this.ucJogTeach1._DisplayMode = ArtTeach.clsDataJogTeach.enuDisplayMode.ShowAll;
            this.ucJogTeach1._FollowControl = false;
            this.ucJogTeach1._JogAImg_String = null;
            this.ucJogTeach1._JogAImgPoint = new System.Drawing.Point(0, 0);
            this.ucJogTeach1._JogAText_String = "Jog+";
            this.ucJogTeach1._JogBImg_String = null;
            this.ucJogTeach1._JogBImgPoint = new System.Drawing.Point(0, 0);
            this.ucJogTeach1._JogBText_String = "Jog-";
            this.ucJogTeach1._JogTeach_Enable = true;
            this.ucJogTeach1._PosName_Enum = null;
            this.ucJogTeach1._PosName_String = "Position Name";
            this.ucJogTeach1._PosSet_Enable = true;
            this.ucJogTeach1._SafePosition = 0D;
            this.ucJogTeach1._SafePosName_Enum = null;
            this.ucJogTeach1.BackColor = System.Drawing.Color.Transparent;
            this.ucJogTeach1.Location = new System.Drawing.Point(10, 82);
            this.ucJogTeach1.MaximumSize = new System.Drawing.Size(288, 143);
            this.ucJogTeach1.MinimumSize = new System.Drawing.Size(288, 143);
            this.ucJogTeach1.Name = "ucJogTeach1";
            this.ucJogTeach1.Size = new System.Drawing.Size(288, 143);
            this.ucJogTeach1.TabIndex = 1130;
            this.ucJogTeach1.m_EventJogAMouseDown += new ArtTeach.ucJogTeach.EventMouseDown(this.ucJogTeach_m_EventJogAMouseDown);
            this.ucJogTeach1.m_EventJogAMouseUp += new ArtTeach.ucJogTeach.EventMouseUp(this.ucJogTeach_m_EventJogAMouseUp);
            this.ucJogTeach1.m_EventJogBMouseDown += new ArtTeach.ucJogTeach.EventMouseDown(this.ucJogTeach_m_EventJogBMouseDown);
            this.ucJogTeach1.m_EventJogBMouseUp += new ArtTeach.ucJogTeach.EventMouseUp(this.ucJogTeach_m_EventJogBMouseUp);
            this.ucJogTeach1.m_EventGoValueClick += new ArtTeach.ucJogTeach.EventGoValueClick(this.ucJogTeach_m_EventGoValueClick);
            this.ucJogTeach1.m_EventGoSafeClick += new ArtTeach.ucJogTeach.EventGoSafeClick(this.ucJogTeach_m_EventGoSafeClick);
            this.ucJogTeach1.m_EventSetValueChanged += new ArtTeach.ucJogTeach.EventSetValueChanged(this.ucJogTeach_m_EventSetValueChanged);
            this.ucJogTeach1.m_EventJogAMouseEnter += new ArtTeach.ucJogTeach.EventJogMouseEnter(this.ucJogTeach_m_EventJogAMouseEnter);
            this.ucJogTeach1.m_EventJogBMouseEnter += new ArtTeach.ucJogTeach.EventJogMouseEnter(this.ucJogTeach_m_EventJogBMouseEnter);
            this.ucJogTeach1.m_EventJogAMouseLeave += new ArtTeach.ucJogTeach.EventJogMouseLeave(this.ucJogTeach_m_EventJogAMouseLeave);
            this.ucJogTeach1.m_EventJogBMouseLeave += new ArtTeach.ucJogTeach.EventJogMouseLeave(this.ucJogTeach_m_EventJogBMouseLeave);
            this.ucJogTeach1.m_EventFollowControlChanged += new ArtTeach.ucJogTeach.EventFollowControlChanged(this.ucJogTeach_m_EventFollowControlChanged);
            // 
            // ucJogMode1
            // 
            this.ucJogMode1._IsContinueMode = false;
            this.ucJogMode1._RelativeValue = 1D;
            this.ucJogMode1._TeachSpeed = 10;
            this.ucJogMode1.BackColor = System.Drawing.Color.Transparent;
            this.ucJogMode1.Location = new System.Drawing.Point(12, 23);
            this.ucJogMode1.Name = "ucJogMode1";
            this.ucJogMode1.Size = new System.Drawing.Size(284, 66);
            this.ucJogMode1.TabIndex = 1129;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.BackColor = System.Drawing.Color.Gold;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStop.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(443, 383);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(172, 66);
            this.btnStop.TabIndex = 1124;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupImage
            // 
            this.groupImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupImage.BackColor = System.Drawing.Color.Transparent;
            this.groupImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupImage.Controls.Add(this.plUIEdit);
            this.groupImage.Controls.Add(this.toolStrip3);
            this.groupImage.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.groupImage.ForeColor = System.Drawing.Color.DimGray;
            this.groupImage.Location = new System.Drawing.Point(174, 20);
            this.groupImage.Name = "groupImage";
            this.groupImage.Size = new System.Drawing.Size(441, 158);
            this.groupImage.TabIndex = 1102;
            this.groupImage.TabStop = false;
            // 
            // plUIEdit
            // 
            this.plUIEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plUIEdit.Location = new System.Drawing.Point(6, 15);
            this.plUIEdit.Name = "plUIEdit";
            this.plUIEdit.Size = new System.Drawing.Size(148, 97);
            this.plUIEdit.TabIndex = 1116;
            // 
            // toolStrip3
            // 
            this.toolStrip3.AutoSize = false;
            this.toolStrip3.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.toolStrip3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator9,
            this.toolStripButton2,
            this.toolStripButton9});
            this.toolStrip3.Location = new System.Drawing.Point(3, 120);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(435, 35);
            this.toolStrip3.TabIndex = 1114;
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 35);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::ArtTeach.Properties.Resources._1346732147_kcontrol;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(34, 32);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.btnUIEdit_Click);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = global::ArtTeach.Properties.Resources._1369597758_file_edit;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(34, 32);
            this.toolStripButton9.Text = "toolStripButton9";
            this.toolStripButton9.Click += new System.EventHandler(this.tsbDataView_Click);
            // 
            // txtCurrentNode
            // 
            this.txtCurrentNode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentNode.BackColor = System.Drawing.Color.Transparent;
            this.txtCurrentNode.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentNode.ForeColor = System.Drawing.Color.Blue;
            this.txtCurrentNode.Location = new System.Drawing.Point(3, 1);
            this.txtCurrentNode.Name = "txtCurrentNode";
            this.txtCurrentNode.Size = new System.Drawing.Size(827, 25);
            this.txtCurrentNode.TabIndex = 1113;
            this.txtCurrentNode.Text = "Node Name";
            this.txtCurrentNode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.toolStrip2);
            this.groupBox2.Controls.Add(this.TreeView_Node);
            this.groupBox2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.DimGray;
            this.groupBox2.Location = new System.Drawing.Point(1, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(167, 362);
            this.groupBox2.TabIndex = 1116;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Node";
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.btnEditUISave,
            this.btnEditUIOpen,
            this.toolStripButton1,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripSeparator5,
            this.tsbExpand,
            this.tsbCollapse});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(129, 22);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(35, 337);
            this.toolStrip2.TabIndex = 1113;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.toolStripSeparator4.Size = new System.Drawing.Size(23, 6);
            // 
            // btnEditUISave
            // 
            this.btnEditUISave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditUISave.Image = ((System.Drawing.Image)(resources.GetObject("btnEditUISave.Image")));
            this.btnEditUISave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditUISave.Name = "btnEditUISave";
            this.btnEditUISave.Size = new System.Drawing.Size(32, 34);
            this.btnEditUISave.Text = "Save File";
            this.btnEditUISave.ToolTipText = "Save UI";
            this.btnEditUISave.Click += new System.EventHandler(this.btnEditUISave_Click);
            this.btnEditUISave.MouseEnter += new System.EventHandler(this.ToolTipDiscreption_MouseEnter);
            // 
            // btnEditUIOpen
            // 
            this.btnEditUIOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditUIOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnEditUIOpen.Image")));
            this.btnEditUIOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditUIOpen.Name = "btnEditUIOpen";
            this.btnEditUIOpen.Size = new System.Drawing.Size(32, 34);
            this.btnEditUIOpen.Text = "Open File";
            this.btnEditUIOpen.ToolTipText = "Load UI";
            this.btnEditUIOpen.Click += new System.EventHandler(this.btnEditUIOpen_Click);
            this.btnEditUIOpen.MouseEnter += new System.EventHandler(this.ToolTipDiscreption_MouseEnter);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(32, 34);
            this.toolStripButton1.Text = "New Add Master";
            this.toolStripButton1.ToolTipText = "Add Node (Master) ";
            this.toolStripButton1.Click += new System.EventHandler(this.btnEditNodeAddMaster_Click);
            this.toolStripButton1.MouseEnter += new System.EventHandler(this.ToolTipDiscreption_MouseEnter);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(32, 34);
            this.toolStripButton3.Text = "New Add Slave";
            this.toolStripButton3.ToolTipText = "Add Node (Slave) ";
            this.toolStripButton3.Click += new System.EventHandler(this.btnEditNodeAddSlave_Click);
            this.toolStripButton3.MouseEnter += new System.EventHandler(this.ToolTipDiscreption_MouseEnter);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(32, 34);
            this.toolStripButton4.Text = "Node Rename";
            this.toolStripButton4.ToolTipText = "Rename Node";
            this.toolStripButton4.Click += new System.EventHandler(this.btnEditNodeRename_Click);
            this.toolStripButton4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolTipDiscreption_MouseEnter);
            this.toolStripButton4.MouseEnter += new System.EventHandler(this.ToolTipDiscreption_MouseEnter);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(32, 34);
            this.toolStripButton5.Text = "Copy Slave";
            this.toolStripButton5.Click += new System.EventHandler(this.btnEditNodeCopySlave_Click);
            this.toolStripButton5.MouseEnter += new System.EventHandler(this.ToolTipDiscreption_MouseEnter);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(32, 34);
            this.toolStripButton6.Text = "Delete Node";
            this.toolStripButton6.Click += new System.EventHandler(this.btnEditNodeDelete_Click);
            this.toolStripButton6.MouseEnter += new System.EventHandler(this.ToolTipDiscreption_MouseEnter);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(32, 34);
            this.toolStripButton7.Text = "Slave Move Up";
            this.toolStripButton7.ToolTipText = "Node Move Up";
            this.toolStripButton7.Click += new System.EventHandler(this.btnEditNodeMoveUp_Click);
            this.toolStripButton7.MouseEnter += new System.EventHandler(this.ToolTipDiscreption_MouseEnter);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(32, 34);
            this.toolStripButton8.Text = "Slave Move Down";
            this.toolStripButton8.ToolTipText = "Node Move Down";
            this.toolStripButton8.Click += new System.EventHandler(this.btnEditNodeMoveDown_Click);
            this.toolStripButton8.MouseEnter += new System.EventHandler(this.ToolTipDiscreption_MouseEnter);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(32, 6);
            // 
            // tsbExpand
            // 
            this.tsbExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExpand.Image = ((System.Drawing.Image)(resources.GetObject("tsbExpand.Image")));
            this.tsbExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExpand.Name = "tsbExpand";
            this.tsbExpand.Size = new System.Drawing.Size(34, 34);
            this.tsbExpand.Text = "toolStripButton2";
            this.tsbExpand.ToolTipText = "Expand All";
            this.tsbExpand.Click += new System.EventHandler(this.btnNodeExpand_Click);
            // 
            // tsbCollapse
            // 
            this.tsbCollapse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCollapse.Image = ((System.Drawing.Image)(resources.GetObject("tsbCollapse.Image")));
            this.tsbCollapse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCollapse.Name = "tsbCollapse";
            this.tsbCollapse.Size = new System.Drawing.Size(34, 34);
            this.tsbCollapse.Text = "toolStripButton9";
            this.tsbCollapse.ToolTipText = "Collapse All";
            this.tsbCollapse.Click += new System.EventHandler(this.btnNodeExpand_Click);
            // 
            // groupBoxDIO
            // 
            this.groupBoxDIO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDIO.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxDIO.Controls.Add(this.clsDIOInfo_18);
            this.groupBoxDIO.Controls.Add(this.clsDIOInfo_17);
            this.groupBoxDIO.Controls.Add(this.clsDIOInfo_16);
            this.groupBoxDIO.Controls.Add(this.clsDIOInfo_15);
            this.groupBoxDIO.Controls.Add(this.clsDIOInfo_14);
            this.groupBoxDIO.Controls.Add(this.clsDIOInfo_13);
            this.groupBoxDIO.Controls.Add(this.clsDIOInfo_12);
            this.groupBoxDIO.Controls.Add(this.clsDIOInfo_11);
            this.groupBoxDIO.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxDIO.ForeColor = System.Drawing.Color.DimGray;
            this.groupBoxDIO.Location = new System.Drawing.Point(174, 174);
            this.groupBoxDIO.Name = "groupBoxDIO";
            this.groupBoxDIO.Size = new System.Drawing.Size(439, 208);
            this.groupBoxDIO.TabIndex = 1115;
            this.groupBoxDIO.TabStop = false;
            this.groupBoxDIO.Text = "DIO ";
            this.groupBoxDIO.SizeChanged += new System.EventHandler(this.groupBoxDIO_SizeChanged);
            // 
            // clsDIOInfo_18
            // 
            this.clsDIOInfo_18._1clsDIOInfo = clsDataDIO1;
            this.clsDIOInfo_18._bIsDIOEnable = true;
            this.clsDIOInfo_18._DIHome = null;
            this.clsDIOInfo_18._DIHome_Invert = false;
            this.clsDIOInfo_18._DiHomeColor = System.Drawing.Color.Green;
            this.clsDIOInfo_18._DiHomeName = "";
            this.clsDIOInfo_18._DIOName = "DIO Control 8";
            this.clsDIOInfo_18._DIReach = null;
            this.clsDIOInfo_18._DIReach_Invert = false;
            this.clsDIOInfo_18._DiReachColor = System.Drawing.Color.Green;
            this.clsDIOInfo_18._DiReachName = "";
            this.clsDIOInfo_18._DoOffBackColor = System.Drawing.Color.DarkGray;
            this.clsDIOInfo_18._DoOffBtnColor = System.Drawing.SystemColors.Control;
            this.clsDIOInfo_18._DoOnBackColor = System.Drawing.Color.Aquamarine;
            this.clsDIOInfo_18._DoOnBtnColor = System.Drawing.Color.LawnGreen;
            this.clsDIOInfo_18._DOTrigger = null;
            this.clsDIOInfo_18._DOTrigger_Invert = false;
            this.clsDIOInfo_18._DOTrigger2 = null;
            this.clsDIOInfo_18.BackColor = System.Drawing.Color.Transparent;
            this.clsDIOInfo_18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clsDIOInfo_18.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.clsDIOInfo_18.ForeColor = System.Drawing.Color.Black;
            this.clsDIOInfo_18.Location = new System.Drawing.Point(351, 114);
            this.clsDIOInfo_18.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clsDIOInfo_18.Name = "clsDIOInfo_18";
            this.clsDIOInfo_18.Size = new System.Drawing.Size(112, 88);
            this.clsDIOInfo_18.TabIndex = 7;
            this.clsDIOInfo_18._DoTrirgger += new System.EventHandler(this.clsDIOInfo_DoTrirgger);
            this.clsDIOInfo_18.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clsControl_MouseDown);
            // 
            // clsDIOInfo_17
            // 
            this.clsDIOInfo_17._1clsDIOInfo = clsDataDIO2;
            this.clsDIOInfo_17._bIsDIOEnable = true;
            this.clsDIOInfo_17._DIHome = null;
            this.clsDIOInfo_17._DIHome_Invert = false;
            this.clsDIOInfo_17._DiHomeColor = System.Drawing.Color.Green;
            this.clsDIOInfo_17._DiHomeName = "";
            this.clsDIOInfo_17._DIOName = "DIO Control 7";
            this.clsDIOInfo_17._DIReach = null;
            this.clsDIOInfo_17._DIReach_Invert = false;
            this.clsDIOInfo_17._DiReachColor = System.Drawing.Color.Green;
            this.clsDIOInfo_17._DiReachName = "";
            this.clsDIOInfo_17._DoOffBackColor = System.Drawing.Color.DarkGray;
            this.clsDIOInfo_17._DoOffBtnColor = System.Drawing.SystemColors.Control;
            this.clsDIOInfo_17._DoOnBackColor = System.Drawing.Color.Aquamarine;
            this.clsDIOInfo_17._DoOnBtnColor = System.Drawing.Color.LawnGreen;
            this.clsDIOInfo_17._DOTrigger = null;
            this.clsDIOInfo_17._DOTrigger_Invert = false;
            this.clsDIOInfo_17._DOTrigger2 = null;
            this.clsDIOInfo_17.BackColor = System.Drawing.Color.Transparent;
            this.clsDIOInfo_17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clsDIOInfo_17.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.clsDIOInfo_17.ForeColor = System.Drawing.Color.Black;
            this.clsDIOInfo_17.Location = new System.Drawing.Point(236, 114);
            this.clsDIOInfo_17.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clsDIOInfo_17.Name = "clsDIOInfo_17";
            this.clsDIOInfo_17.Size = new System.Drawing.Size(112, 88);
            this.clsDIOInfo_17.TabIndex = 6;
            this.clsDIOInfo_17._DoTrirgger += new System.EventHandler(this.clsDIOInfo_DoTrirgger);
            this.clsDIOInfo_17.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clsControl_MouseDown);
            // 
            // clsDIOInfo_16
            // 
            this.clsDIOInfo_16._1clsDIOInfo = clsDataDIO3;
            this.clsDIOInfo_16._bIsDIOEnable = true;
            this.clsDIOInfo_16._DIHome = null;
            this.clsDIOInfo_16._DIHome_Invert = false;
            this.clsDIOInfo_16._DiHomeColor = System.Drawing.Color.Green;
            this.clsDIOInfo_16._DiHomeName = "";
            this.clsDIOInfo_16._DIOName = "DIO Control 6";
            this.clsDIOInfo_16._DIReach = null;
            this.clsDIOInfo_16._DIReach_Invert = false;
            this.clsDIOInfo_16._DiReachColor = System.Drawing.Color.Green;
            this.clsDIOInfo_16._DiReachName = "";
            this.clsDIOInfo_16._DoOffBackColor = System.Drawing.Color.DarkGray;
            this.clsDIOInfo_16._DoOffBtnColor = System.Drawing.SystemColors.Control;
            this.clsDIOInfo_16._DoOnBackColor = System.Drawing.Color.Aquamarine;
            this.clsDIOInfo_16._DoOnBtnColor = System.Drawing.Color.LawnGreen;
            this.clsDIOInfo_16._DOTrigger = null;
            this.clsDIOInfo_16._DOTrigger_Invert = false;
            this.clsDIOInfo_16._DOTrigger2 = null;
            this.clsDIOInfo_16.BackColor = System.Drawing.Color.Transparent;
            this.clsDIOInfo_16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clsDIOInfo_16.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.clsDIOInfo_16.ForeColor = System.Drawing.Color.Black;
            this.clsDIOInfo_16.Location = new System.Drawing.Point(121, 114);
            this.clsDIOInfo_16.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clsDIOInfo_16.Name = "clsDIOInfo_16";
            this.clsDIOInfo_16.Size = new System.Drawing.Size(112, 88);
            this.clsDIOInfo_16.TabIndex = 5;
            this.clsDIOInfo_16._DoTrirgger += new System.EventHandler(this.clsDIOInfo_DoTrirgger);
            this.clsDIOInfo_16.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clsControl_MouseDown);
            // 
            // clsDIOInfo_15
            // 
            this.clsDIOInfo_15._1clsDIOInfo = clsDataDIO4;
            this.clsDIOInfo_15._bIsDIOEnable = true;
            this.clsDIOInfo_15._DIHome = null;
            this.clsDIOInfo_15._DIHome_Invert = false;
            this.clsDIOInfo_15._DiHomeColor = System.Drawing.Color.Green;
            this.clsDIOInfo_15._DiHomeName = "";
            this.clsDIOInfo_15._DIOName = "DIO Control 5";
            this.clsDIOInfo_15._DIReach = null;
            this.clsDIOInfo_15._DIReach_Invert = false;
            this.clsDIOInfo_15._DiReachColor = System.Drawing.Color.Green;
            this.clsDIOInfo_15._DiReachName = "";
            this.clsDIOInfo_15._DoOffBackColor = System.Drawing.Color.DarkGray;
            this.clsDIOInfo_15._DoOffBtnColor = System.Drawing.SystemColors.Control;
            this.clsDIOInfo_15._DoOnBackColor = System.Drawing.Color.Aquamarine;
            this.clsDIOInfo_15._DoOnBtnColor = System.Drawing.Color.LawnGreen;
            this.clsDIOInfo_15._DOTrigger = null;
            this.clsDIOInfo_15._DOTrigger_Invert = false;
            this.clsDIOInfo_15._DOTrigger2 = null;
            this.clsDIOInfo_15.BackColor = System.Drawing.Color.Transparent;
            this.clsDIOInfo_15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clsDIOInfo_15.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.clsDIOInfo_15.ForeColor = System.Drawing.Color.Black;
            this.clsDIOInfo_15.Location = new System.Drawing.Point(6, 114);
            this.clsDIOInfo_15.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clsDIOInfo_15.Name = "clsDIOInfo_15";
            this.clsDIOInfo_15.Size = new System.Drawing.Size(112, 88);
            this.clsDIOInfo_15.TabIndex = 4;
            this.clsDIOInfo_15._DoTrirgger += new System.EventHandler(this.clsDIOInfo_DoTrirgger);
            this.clsDIOInfo_15.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clsControl_MouseDown);
            // 
            // clsDIOInfo_14
            // 
            this.clsDIOInfo_14._1clsDIOInfo = clsDataDIO5;
            this.clsDIOInfo_14._bIsDIOEnable = true;
            this.clsDIOInfo_14._DIHome = null;
            this.clsDIOInfo_14._DIHome_Invert = false;
            this.clsDIOInfo_14._DiHomeColor = System.Drawing.Color.Green;
            this.clsDIOInfo_14._DiHomeName = "";
            this.clsDIOInfo_14._DIOName = "DIO Control 4";
            this.clsDIOInfo_14._DIReach = null;
            this.clsDIOInfo_14._DIReach_Invert = false;
            this.clsDIOInfo_14._DiReachColor = System.Drawing.Color.Green;
            this.clsDIOInfo_14._DiReachName = "";
            this.clsDIOInfo_14._DoOffBackColor = System.Drawing.Color.DarkGray;
            this.clsDIOInfo_14._DoOffBtnColor = System.Drawing.SystemColors.Control;
            this.clsDIOInfo_14._DoOnBackColor = System.Drawing.Color.Aquamarine;
            this.clsDIOInfo_14._DoOnBtnColor = System.Drawing.Color.LawnGreen;
            this.clsDIOInfo_14._DOTrigger = null;
            this.clsDIOInfo_14._DOTrigger_Invert = false;
            this.clsDIOInfo_14._DOTrigger2 = null;
            this.clsDIOInfo_14.BackColor = System.Drawing.Color.Transparent;
            this.clsDIOInfo_14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clsDIOInfo_14.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.clsDIOInfo_14.ForeColor = System.Drawing.Color.Black;
            this.clsDIOInfo_14.Location = new System.Drawing.Point(351, 23);
            this.clsDIOInfo_14.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clsDIOInfo_14.Name = "clsDIOInfo_14";
            this.clsDIOInfo_14.Size = new System.Drawing.Size(112, 88);
            this.clsDIOInfo_14.TabIndex = 3;
            this.clsDIOInfo_14._DoTrirgger += new System.EventHandler(this.clsDIOInfo_DoTrirgger);
            this.clsDIOInfo_14.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clsControl_MouseDown);
            // 
            // clsDIOInfo_13
            // 
            this.clsDIOInfo_13._1clsDIOInfo = clsDataDIO6;
            this.clsDIOInfo_13._bIsDIOEnable = true;
            this.clsDIOInfo_13._DIHome = null;
            this.clsDIOInfo_13._DIHome_Invert = false;
            this.clsDIOInfo_13._DiHomeColor = System.Drawing.Color.Green;
            this.clsDIOInfo_13._DiHomeName = "";
            this.clsDIOInfo_13._DIOName = "DIO Control 3";
            this.clsDIOInfo_13._DIReach = null;
            this.clsDIOInfo_13._DIReach_Invert = false;
            this.clsDIOInfo_13._DiReachColor = System.Drawing.Color.Green;
            this.clsDIOInfo_13._DiReachName = "";
            this.clsDIOInfo_13._DoOffBackColor = System.Drawing.Color.DarkGray;
            this.clsDIOInfo_13._DoOffBtnColor = System.Drawing.SystemColors.Control;
            this.clsDIOInfo_13._DoOnBackColor = System.Drawing.Color.Aquamarine;
            this.clsDIOInfo_13._DoOnBtnColor = System.Drawing.Color.LawnGreen;
            this.clsDIOInfo_13._DOTrigger = null;
            this.clsDIOInfo_13._DOTrigger_Invert = false;
            this.clsDIOInfo_13._DOTrigger2 = null;
            this.clsDIOInfo_13.BackColor = System.Drawing.Color.Transparent;
            this.clsDIOInfo_13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clsDIOInfo_13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.clsDIOInfo_13.ForeColor = System.Drawing.Color.Black;
            this.clsDIOInfo_13.Location = new System.Drawing.Point(236, 23);
            this.clsDIOInfo_13.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clsDIOInfo_13.Name = "clsDIOInfo_13";
            this.clsDIOInfo_13.Size = new System.Drawing.Size(112, 88);
            this.clsDIOInfo_13.TabIndex = 2;
            this.clsDIOInfo_13._DoTrirgger += new System.EventHandler(this.clsDIOInfo_DoTrirgger);
            this.clsDIOInfo_13.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clsControl_MouseDown);
            // 
            // clsDIOInfo_12
            // 
            this.clsDIOInfo_12._1clsDIOInfo = clsDataDIO7;
            this.clsDIOInfo_12._bIsDIOEnable = true;
            this.clsDIOInfo_12._DIHome = null;
            this.clsDIOInfo_12._DIHome_Invert = false;
            this.clsDIOInfo_12._DiHomeColor = System.Drawing.Color.Green;
            this.clsDIOInfo_12._DiHomeName = "";
            this.clsDIOInfo_12._DIOName = "DIO Control 2";
            this.clsDIOInfo_12._DIReach = null;
            this.clsDIOInfo_12._DIReach_Invert = false;
            this.clsDIOInfo_12._DiReachColor = System.Drawing.Color.Green;
            this.clsDIOInfo_12._DiReachName = "";
            this.clsDIOInfo_12._DoOffBackColor = System.Drawing.Color.DarkGray;
            this.clsDIOInfo_12._DoOffBtnColor = System.Drawing.SystemColors.Control;
            this.clsDIOInfo_12._DoOnBackColor = System.Drawing.Color.Aquamarine;
            this.clsDIOInfo_12._DoOnBtnColor = System.Drawing.Color.LawnGreen;
            this.clsDIOInfo_12._DOTrigger = null;
            this.clsDIOInfo_12._DOTrigger_Invert = false;
            this.clsDIOInfo_12._DOTrigger2 = null;
            this.clsDIOInfo_12.BackColor = System.Drawing.Color.Transparent;
            this.clsDIOInfo_12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clsDIOInfo_12.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.clsDIOInfo_12.ForeColor = System.Drawing.Color.Black;
            this.clsDIOInfo_12.Location = new System.Drawing.Point(121, 23);
            this.clsDIOInfo_12.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clsDIOInfo_12.Name = "clsDIOInfo_12";
            this.clsDIOInfo_12.Size = new System.Drawing.Size(112, 88);
            this.clsDIOInfo_12.TabIndex = 1;
            this.clsDIOInfo_12._DoTrirgger += new System.EventHandler(this.clsDIOInfo_DoTrirgger);
            this.clsDIOInfo_12.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clsControl_MouseDown);
            // 
            // clsDIOInfo_11
            // 
            this.clsDIOInfo_11._1clsDIOInfo = clsDataDIO8;
            this.clsDIOInfo_11._bIsDIOEnable = true;
            this.clsDIOInfo_11._DIHome = null;
            this.clsDIOInfo_11._DIHome_Invert = false;
            this.clsDIOInfo_11._DiHomeColor = System.Drawing.Color.Green;
            this.clsDIOInfo_11._DiHomeName = "";
            this.clsDIOInfo_11._DIOName = "DIO Control 1";
            this.clsDIOInfo_11._DIReach = null;
            this.clsDIOInfo_11._DIReach_Invert = false;
            this.clsDIOInfo_11._DiReachColor = System.Drawing.Color.Green;
            this.clsDIOInfo_11._DiReachName = "";
            this.clsDIOInfo_11._DoOffBackColor = System.Drawing.Color.DarkGray;
            this.clsDIOInfo_11._DoOffBtnColor = System.Drawing.SystemColors.Control;
            this.clsDIOInfo_11._DoOnBackColor = System.Drawing.Color.Aquamarine;
            this.clsDIOInfo_11._DoOnBtnColor = System.Drawing.Color.LawnGreen;
            this.clsDIOInfo_11._DOTrigger = null;
            this.clsDIOInfo_11._DOTrigger_Invert = false;
            this.clsDIOInfo_11._DOTrigger2 = null;
            this.clsDIOInfo_11.BackColor = System.Drawing.Color.Transparent;
            this.clsDIOInfo_11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clsDIOInfo_11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.clsDIOInfo_11.ForeColor = System.Drawing.Color.Black;
            this.clsDIOInfo_11.Location = new System.Drawing.Point(6, 23);
            this.clsDIOInfo_11.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clsDIOInfo_11.Name = "clsDIOInfo_11";
            this.clsDIOInfo_11.Size = new System.Drawing.Size(112, 88);
            this.clsDIOInfo_11.TabIndex = 0;
            this.clsDIOInfo_11._DoTrirgger += new System.EventHandler(this.clsDIOInfo_DoTrirgger);
            this.clsDIOInfo_11.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clsControl_MouseDown);
            // 
            // btn_EditUI
            // 
            this.btn_EditUI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_EditUI.Location = new System.Drawing.Point(836, 3);
            this.btn_EditUI.Name = "btn_EditUI";
            this.btn_EditUI.Size = new System.Drawing.Size(94, 23);
            this.btn_EditUI.TabIndex = 1121;
            this.btn_EditUI.Text = "Edit UI";
            this.btn_EditUI.UseVisualStyleBackColor = true;
            this.btn_EditUI.Click += new System.EventHandler(this.btn_EditUI_Click);
            // 
            // ucEditUITable1
            // 
            this.ucEditUITable1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucEditUITable1.Location = new System.Drawing.Point(169, 17);
            this.ucEditUITable1.Margin = new System.Windows.Forms.Padding(16);
            this.ucEditUITable1.Name = "ucEditUITable1";
            this.ucEditUITable1.Size = new System.Drawing.Size(764, 453);
            this.ucEditUITable1.TabIndex = 1124;
            this.ucEditUITable1.Visible = false;
            this.ucEditUITable1.m_ExitEvent += new System.EventHandler(this.ucEditUITable1_m_ExitEvent);
            // 
            // RichTextMarkData
            // 
            this.RichTextMarkData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextMarkData.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RichTextMarkData.ImeMode = System.Windows.Forms.ImeMode.HangulFull;
            this.RichTextMarkData.Location = new System.Drawing.Point(6, 21);
            this.RichTextMarkData.Name = "RichTextMarkData";
            this.RichTextMarkData.Size = new System.Drawing.Size(422, 48);
            this.RichTextMarkData.TabIndex = 1128;
            this.RichTextMarkData.Text = "RichTextMarkData";
            this.RichTextMarkData.TextChanged += new System.EventHandler(this.RichTextMarkData_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.RichTextMarkData);
            this.groupBox1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.DimGray;
            this.groupBox1.Location = new System.Drawing.Point(3, 377);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 73);
            this.groupBox1.TabIndex = 1129;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mark";
            // 
            // ucEditUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.txtCurrentNode);
            this.Controls.Add(this.btn_EditUI);
            this.Controls.Add(this.groupBoxJogAndTeach);
            this.Controls.Add(this.groupImage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxDIO);
            this.Controls.Add(this.ucEditUITable1);
            this.Name = "ucEditUI";
            this.Size = new System.Drawing.Size(933, 460);
            this.BackColorChanged += new System.EventHandler(this.ucEditUI_BackColorChanged);
            this.ParentChanged += new System.EventHandler(this.ucEditUI_ParentChanged);
            this.groupBoxJogAndTeach.ResumeLayout(false);
            this.groupImage.ResumeLayout(false);
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBoxDIO.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView TreeView_Node;
        private System.Windows.Forms.GroupBox groupBoxJogAndTeach;
        private System.Windows.Forms.GroupBox groupImage;
        private System.Windows.Forms.Label txtCurrentNode;
        private System.Windows.Forms.Panel plUIEdit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBoxDIO;
        private ucDIOInfo clsDIOInfo_16;
        private ucDIOInfo clsDIOInfo_15;
        private ucDIOInfo clsDIOInfo_14;
        private ucDIOInfo clsDIOInfo_13;
        private ucDIOInfo clsDIOInfo_12;
        private ucDIOInfo clsDIOInfo_11;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnEditUISave;
        private System.Windows.Forms.ToolStripButton btnEditUIOpen;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbExpand;
        private System.Windows.Forms.ToolStripButton tsbCollapse;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btn_EditUI;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private ucEditUITable ucEditUITable1;
        private System.Windows.Forms.RichTextBox RichTextMarkData;
        private ucJogMode ucJogMode1;
        private System.Windows.Forms.GroupBox groupBox1;
        private ucJogTeach ucJogTeach1;
        private ucJogTeach ucJogTeach3;
        private ucJogTeach ucJogTeach2;
        private ucJogTeach ucJogTeach4;
        private ucDIOInfo clsDIOInfo_18;
        private ucDIOInfo clsDIOInfo_17;
    }
}

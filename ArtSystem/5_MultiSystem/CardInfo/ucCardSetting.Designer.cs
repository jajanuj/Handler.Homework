namespace ArtSystem.MultiSystem
{
    partial class ucCardSetting
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPage_MotionCard = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_MotionCardPath = new System.Windows.Forms.TextBox();
            this.btnSave_MotionCardSetting = new System.Windows.Forms.Button();
            this.btnCancel_MotionCardSetting = new System.Windows.Forms.Button();
            this.btnEdit_MotionCardSetting = new System.Windows.Forms.Button();
            this.btnAdd_MotionCard = new System.Windows.Forms.Button();
            this.btnDelete_MotionCard = new System.Windows.Forms.Button();
            this.dgvMotion = new System.Windows.Forms.DataGridView();
            this.tPage_IOCard = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_IOCardPath = new System.Windows.Forms.TextBox();
            this.btnSave_IOCardSetting = new System.Windows.Forms.Button();
            this.btnCancel_IOCardSetting = new System.Windows.Forms.Button();
            this.btnEdit_IOCardSetting = new System.Windows.Forms.Button();
            this.btnAdd_IOCard = new System.Windows.Forms.Button();
            this.btnDelete_IOCard = new System.Windows.Forms.Button();
            this.dgvIOCard = new System.Windows.Forms.DataGridView();
            this.tPage_AIOCard = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_AIOCardPath = new System.Windows.Forms.TextBox();
            this.btnSave_AIOCardSetting = new System.Windows.Forms.Button();
            this.btnCancel_AIOCardSetting = new System.Windows.Forms.Button();
            this.btnEdit_AIOCardSetting = new System.Windows.Forms.Button();
            this.btnAdd_AIOCard = new System.Windows.Forms.Button();
            this.btnDelete_AIOCard = new System.Windows.Forms.Button();
            this.dgvAIOCard = new System.Windows.Forms.DataGridView();
            this.dgvMotion_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMotion_CardType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvMotion_CardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMotion_SlaveID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMotion_SlaveType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvMotion_SlaveNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMotion_AxisID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSimulate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvMotion_Other = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgvMotion_AxisSetting = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgvIOCard_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvIOCard_CardType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvIOCard_SlaveType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvIOCard_CardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvIOCard_SlaveID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvIOCard_StartDI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvIOCard_StartDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvIOCard_Simulate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvAIOCard_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAIOCard_CardType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvAIOCard_CardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAIOCard_SlaveID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAIOCard_StartDI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAIOCard_StartDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tPage_MotionCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotion)).BeginInit();
            this.tPage_IOCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIOCard)).BeginInit();
            this.tPage_AIOCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAIOCard)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tPage_MotionCard);
            this.tabControl1.Controls.Add(this.tPage_IOCard);
            this.tabControl1.Controls.Add(this.tPage_AIOCard);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1105, 605);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tPage_MotionCard
            // 
            this.tPage_MotionCard.Controls.Add(this.label1);
            this.tPage_MotionCard.Controls.Add(this.txt_MotionCardPath);
            this.tPage_MotionCard.Controls.Add(this.btnSave_MotionCardSetting);
            this.tPage_MotionCard.Controls.Add(this.btnCancel_MotionCardSetting);
            this.tPage_MotionCard.Controls.Add(this.btnEdit_MotionCardSetting);
            this.tPage_MotionCard.Controls.Add(this.btnAdd_MotionCard);
            this.tPage_MotionCard.Controls.Add(this.btnDelete_MotionCard);
            this.tPage_MotionCard.Controls.Add(this.dgvMotion);
            this.tPage_MotionCard.Location = new System.Drawing.Point(4, 25);
            this.tPage_MotionCard.Name = "tPage_MotionCard";
            this.tPage_MotionCard.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_MotionCard.Size = new System.Drawing.Size(1097, 576);
            this.tPage_MotionCard.TabIndex = 0;
            this.tPage_MotionCard.Text = "Motion Card";
            this.tPage_MotionCard.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "File Path:";
            // 
            // txt_MotionCardPath
            // 
            this.txt_MotionCardPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_MotionCardPath.Location = new System.Drawing.Point(215, 34);
            this.txt_MotionCardPath.Name = "txt_MotionCardPath";
            this.txt_MotionCardPath.Size = new System.Drawing.Size(558, 23);
            this.txt_MotionCardPath.TabIndex = 8;
            // 
            // btnSave_MotionCardSetting
            // 
            this.btnSave_MotionCardSetting.Location = new System.Drawing.Point(3, 5);
            this.btnSave_MotionCardSetting.Name = "btnSave_MotionCardSetting";
            this.btnSave_MotionCardSetting.Size = new System.Drawing.Size(100, 52);
            this.btnSave_MotionCardSetting.TabIndex = 6;
            this.btnSave_MotionCardSetting.Text = "Save";
            this.btnSave_MotionCardSetting.UseVisualStyleBackColor = true;
            this.btnSave_MotionCardSetting.Click += new System.EventHandler(this.btnSave_CardSetting_Click);
            // 
            // btnCancel_MotionCardSetting
            // 
            this.btnCancel_MotionCardSetting.Location = new System.Drawing.Point(109, 5);
            this.btnCancel_MotionCardSetting.Name = "btnCancel_MotionCardSetting";
            this.btnCancel_MotionCardSetting.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_MotionCardSetting.TabIndex = 5;
            this.btnCancel_MotionCardSetting.Text = "Cancel";
            this.btnCancel_MotionCardSetting.UseVisualStyleBackColor = true;
            this.btnCancel_MotionCardSetting.Click += new System.EventHandler(this.btnCancel_CardSetting_Click);
            // 
            // btnEdit_MotionCardSetting
            // 
            this.btnEdit_MotionCardSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_MotionCardSetting.Location = new System.Drawing.Point(991, 5);
            this.btnEdit_MotionCardSetting.Name = "btnEdit_MotionCardSetting";
            this.btnEdit_MotionCardSetting.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_MotionCardSetting.TabIndex = 7;
            this.btnEdit_MotionCardSetting.Text = "Edit";
            this.btnEdit_MotionCardSetting.UseVisualStyleBackColor = true;
            this.btnEdit_MotionCardSetting.Click += new System.EventHandler(this.btnEdit_CardSetting_Click);
            // 
            // btnAdd_MotionCard
            // 
            this.btnAdd_MotionCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd_MotionCard.Location = new System.Drawing.Point(779, 5);
            this.btnAdd_MotionCard.Name = "btnAdd_MotionCard";
            this.btnAdd_MotionCard.Size = new System.Drawing.Size(100, 52);
            this.btnAdd_MotionCard.TabIndex = 3;
            this.btnAdd_MotionCard.Text = "Add";
            this.btnAdd_MotionCard.UseVisualStyleBackColor = true;
            this.btnAdd_MotionCard.Click += new System.EventHandler(this.btnAdd_MotionCard_Click);
            // 
            // btnDelete_MotionCard
            // 
            this.btnDelete_MotionCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete_MotionCard.Location = new System.Drawing.Point(885, 5);
            this.btnDelete_MotionCard.Name = "btnDelete_MotionCard";
            this.btnDelete_MotionCard.Size = new System.Drawing.Size(100, 52);
            this.btnDelete_MotionCard.TabIndex = 2;
            this.btnDelete_MotionCard.Text = "Delete";
            this.btnDelete_MotionCard.UseVisualStyleBackColor = true;
            this.btnDelete_MotionCard.Click += new System.EventHandler(this.btnDelete_MotionCard_Click);
            // 
            // dgvMotion
            // 
            this.dgvMotion.AllowUserToAddRows = false;
            this.dgvMotion.AllowUserToDeleteRows = false;
            this.dgvMotion.AllowUserToResizeColumns = false;
            this.dgvMotion.AllowUserToResizeRows = false;
            this.dgvMotion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMotion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMotion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvMotion_No,
            this.dgvMotion_CardType,
            this.dgvMotion_CardID,
            this.dgvMotion_SlaveID,
            this.dgvMotion_SlaveType,
            this.dgvMotion_SlaveNum,
            this.dgvMotion_AxisID,
            this.dgvSimulate,
            this.dgvMotion_Other,
            this.dgvMotion_AxisSetting});
            this.dgvMotion.Location = new System.Drawing.Point(3, 63);
            this.dgvMotion.MultiSelect = false;
            this.dgvMotion.Name = "dgvMotion";
            this.dgvMotion.RowHeadersVisible = false;
            this.dgvMotion.RowTemplate.Height = 24;
            this.dgvMotion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMotion.Size = new System.Drawing.Size(1091, 509);
            this.dgvMotion.TabIndex = 0;
            this.dgvMotion.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMotion_CellClick);
            this.dgvMotion.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMotion_CellContentClick);
            this.dgvMotion.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMotion_CellEndEdit);
            this.dgvMotion.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            this.dgvMotion.EnabledChanged += new System.EventHandler(this.dgvMotion_EnabledChanged);
            // 
            // tPage_IOCard
            // 
            this.tPage_IOCard.Controls.Add(this.button1);
            this.tPage_IOCard.Controls.Add(this.label2);
            this.tPage_IOCard.Controls.Add(this.txt_IOCardPath);
            this.tPage_IOCard.Controls.Add(this.btnSave_IOCardSetting);
            this.tPage_IOCard.Controls.Add(this.btnCancel_IOCardSetting);
            this.tPage_IOCard.Controls.Add(this.btnEdit_IOCardSetting);
            this.tPage_IOCard.Controls.Add(this.btnAdd_IOCard);
            this.tPage_IOCard.Controls.Add(this.btnDelete_IOCard);
            this.tPage_IOCard.Controls.Add(this.dgvIOCard);
            this.tPage_IOCard.Location = new System.Drawing.Point(4, 25);
            this.tPage_IOCard.Name = "tPage_IOCard";
            this.tPage_IOCard.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_IOCard.Size = new System.Drawing.Size(1097, 576);
            this.tPage_IOCard.TabIndex = 1;
            this.tPage_IOCard.Text = "IO Card";
            this.tPage_IOCard.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(617, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 25);
            this.button1.TabIndex = 16;
            this.button1.Text = "Advance Setting";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "File Path:";
            // 
            // txt_IOCardPath
            // 
            this.txt_IOCardPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_IOCardPath.Location = new System.Drawing.Point(215, 34);
            this.txt_IOCardPath.Name = "txt_IOCardPath";
            this.txt_IOCardPath.Size = new System.Drawing.Size(558, 23);
            this.txt_IOCardPath.TabIndex = 14;
            // 
            // btnSave_IOCardSetting
            // 
            this.btnSave_IOCardSetting.Location = new System.Drawing.Point(3, 5);
            this.btnSave_IOCardSetting.Name = "btnSave_IOCardSetting";
            this.btnSave_IOCardSetting.Size = new System.Drawing.Size(100, 52);
            this.btnSave_IOCardSetting.TabIndex = 12;
            this.btnSave_IOCardSetting.Text = "Save";
            this.btnSave_IOCardSetting.UseVisualStyleBackColor = true;
            this.btnSave_IOCardSetting.Click += new System.EventHandler(this.btnSave_CardSetting_Click);
            // 
            // btnCancel_IOCardSetting
            // 
            this.btnCancel_IOCardSetting.Location = new System.Drawing.Point(109, 5);
            this.btnCancel_IOCardSetting.Name = "btnCancel_IOCardSetting";
            this.btnCancel_IOCardSetting.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_IOCardSetting.TabIndex = 11;
            this.btnCancel_IOCardSetting.Text = "Cancel";
            this.btnCancel_IOCardSetting.UseVisualStyleBackColor = true;
            this.btnCancel_IOCardSetting.Click += new System.EventHandler(this.btnCancel_CardSetting_Click);
            // 
            // btnEdit_IOCardSetting
            // 
            this.btnEdit_IOCardSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_IOCardSetting.Location = new System.Drawing.Point(991, 5);
            this.btnEdit_IOCardSetting.Name = "btnEdit_IOCardSetting";
            this.btnEdit_IOCardSetting.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_IOCardSetting.TabIndex = 13;
            this.btnEdit_IOCardSetting.Text = "Edit";
            this.btnEdit_IOCardSetting.UseVisualStyleBackColor = true;
            this.btnEdit_IOCardSetting.Click += new System.EventHandler(this.btnEdit_CardSetting_Click);
            // 
            // btnAdd_IOCard
            // 
            this.btnAdd_IOCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd_IOCard.Location = new System.Drawing.Point(779, 5);
            this.btnAdd_IOCard.Name = "btnAdd_IOCard";
            this.btnAdd_IOCard.Size = new System.Drawing.Size(100, 52);
            this.btnAdd_IOCard.TabIndex = 10;
            this.btnAdd_IOCard.Text = "Add";
            this.btnAdd_IOCard.UseVisualStyleBackColor = true;
            this.btnAdd_IOCard.Click += new System.EventHandler(this.btnAdd_IOCard_Click);
            // 
            // btnDelete_IOCard
            // 
            this.btnDelete_IOCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete_IOCard.Location = new System.Drawing.Point(885, 5);
            this.btnDelete_IOCard.Name = "btnDelete_IOCard";
            this.btnDelete_IOCard.Size = new System.Drawing.Size(100, 52);
            this.btnDelete_IOCard.TabIndex = 9;
            this.btnDelete_IOCard.Text = "Delete";
            this.btnDelete_IOCard.UseVisualStyleBackColor = true;
            this.btnDelete_IOCard.Click += new System.EventHandler(this.btnDelete_IOCard_Click);
            // 
            // dgvIOCard
            // 
            this.dgvIOCard.AllowUserToAddRows = false;
            this.dgvIOCard.AllowUserToDeleteRows = false;
            this.dgvIOCard.AllowUserToResizeColumns = false;
            this.dgvIOCard.AllowUserToResizeRows = false;
            this.dgvIOCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvIOCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIOCard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvIOCard_No,
            this.dgvIOCard_CardType,
            this.dgvIOCard_SlaveType,
            this.dgvIOCard_CardID,
            this.dgvIOCard_SlaveID,
            this.dgvIOCard_StartDI,
            this.dgvIOCard_StartDO,
            this.dgvIOCard_Simulate});
            this.dgvIOCard.Location = new System.Drawing.Point(3, 63);
            this.dgvIOCard.MultiSelect = false;
            this.dgvIOCard.Name = "dgvIOCard";
            this.dgvIOCard.RowHeadersVisible = false;
            this.dgvIOCard.RowTemplate.Height = 24;
            this.dgvIOCard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvIOCard.Size = new System.Drawing.Size(1091, 509);
            this.dgvIOCard.TabIndex = 8;
            this.dgvIOCard.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            this.dgvIOCard.EnabledChanged += new System.EventHandler(this.dgvIOCard_EnabledChanged);
            // 
            // tPage_AIOCard
            // 
            this.tPage_AIOCard.Controls.Add(this.button2);
            this.tPage_AIOCard.Controls.Add(this.label3);
            this.tPage_AIOCard.Controls.Add(this.txt_AIOCardPath);
            this.tPage_AIOCard.Controls.Add(this.btnSave_AIOCardSetting);
            this.tPage_AIOCard.Controls.Add(this.btnCancel_AIOCardSetting);
            this.tPage_AIOCard.Controls.Add(this.btnEdit_AIOCardSetting);
            this.tPage_AIOCard.Controls.Add(this.btnAdd_AIOCard);
            this.tPage_AIOCard.Controls.Add(this.btnDelete_AIOCard);
            this.tPage_AIOCard.Controls.Add(this.dgvAIOCard);
            this.tPage_AIOCard.Location = new System.Drawing.Point(4, 25);
            this.tPage_AIOCard.Name = "tPage_AIOCard";
            this.tPage_AIOCard.Size = new System.Drawing.Size(1097, 576);
            this.tPage_AIOCard.TabIndex = 2;
            this.tPage_AIOCard.Text = "AIO Card";
            this.tPage_AIOCard.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(617, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(156, 25);
            this.button2.TabIndex = 17;
            this.button2.Text = "Advance Setting";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "File Path:";
            // 
            // txt_AIOCardPath
            // 
            this.txt_AIOCardPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_AIOCardPath.Location = new System.Drawing.Point(215, 34);
            this.txt_AIOCardPath.Name = "txt_AIOCardPath";
            this.txt_AIOCardPath.Size = new System.Drawing.Size(558, 23);
            this.txt_AIOCardPath.TabIndex = 14;
            // 
            // btnSave_AIOCardSetting
            // 
            this.btnSave_AIOCardSetting.Location = new System.Drawing.Point(3, 5);
            this.btnSave_AIOCardSetting.Name = "btnSave_AIOCardSetting";
            this.btnSave_AIOCardSetting.Size = new System.Drawing.Size(100, 52);
            this.btnSave_AIOCardSetting.TabIndex = 12;
            this.btnSave_AIOCardSetting.Text = "Save";
            this.btnSave_AIOCardSetting.UseVisualStyleBackColor = true;
            this.btnSave_AIOCardSetting.Click += new System.EventHandler(this.btnSave_CardSetting_Click);
            // 
            // btnCancel_AIOCardSetting
            // 
            this.btnCancel_AIOCardSetting.Location = new System.Drawing.Point(109, 5);
            this.btnCancel_AIOCardSetting.Name = "btnCancel_AIOCardSetting";
            this.btnCancel_AIOCardSetting.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_AIOCardSetting.TabIndex = 11;
            this.btnCancel_AIOCardSetting.Text = "Cancel";
            this.btnCancel_AIOCardSetting.UseVisualStyleBackColor = true;
            this.btnCancel_AIOCardSetting.Click += new System.EventHandler(this.btnCancel_CardSetting_Click);
            // 
            // btnEdit_AIOCardSetting
            // 
            this.btnEdit_AIOCardSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_AIOCardSetting.Location = new System.Drawing.Point(991, 5);
            this.btnEdit_AIOCardSetting.Name = "btnEdit_AIOCardSetting";
            this.btnEdit_AIOCardSetting.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_AIOCardSetting.TabIndex = 13;
            this.btnEdit_AIOCardSetting.Text = "Edit";
            this.btnEdit_AIOCardSetting.UseVisualStyleBackColor = true;
            this.btnEdit_AIOCardSetting.Click += new System.EventHandler(this.btnEdit_CardSetting_Click);
            // 
            // btnAdd_AIOCard
            // 
            this.btnAdd_AIOCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd_AIOCard.Location = new System.Drawing.Point(779, 5);
            this.btnAdd_AIOCard.Name = "btnAdd_AIOCard";
            this.btnAdd_AIOCard.Size = new System.Drawing.Size(100, 52);
            this.btnAdd_AIOCard.TabIndex = 10;
            this.btnAdd_AIOCard.Text = "Add";
            this.btnAdd_AIOCard.UseVisualStyleBackColor = true;
            this.btnAdd_AIOCard.Click += new System.EventHandler(this.btnAdd_AIOCard_Click);
            // 
            // btnDelete_AIOCard
            // 
            this.btnDelete_AIOCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete_AIOCard.Location = new System.Drawing.Point(885, 5);
            this.btnDelete_AIOCard.Name = "btnDelete_AIOCard";
            this.btnDelete_AIOCard.Size = new System.Drawing.Size(100, 52);
            this.btnDelete_AIOCard.TabIndex = 9;
            this.btnDelete_AIOCard.Text = "Delete";
            this.btnDelete_AIOCard.UseVisualStyleBackColor = true;
            this.btnDelete_AIOCard.Click += new System.EventHandler(this.btnDelete_AIOCard_Click);
            // 
            // dgvAIOCard
            // 
            this.dgvAIOCard.AllowUserToAddRows = false;
            this.dgvAIOCard.AllowUserToDeleteRows = false;
            this.dgvAIOCard.AllowUserToResizeColumns = false;
            this.dgvAIOCard.AllowUserToResizeRows = false;
            this.dgvAIOCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAIOCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAIOCard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvAIOCard_No,
            this.dgvAIOCard_CardType,
            this.dgvAIOCard_CardID,
            this.dgvAIOCard_SlaveID,
            this.dgvAIOCard_StartDI,
            this.dgvAIOCard_StartDO});
            this.dgvAIOCard.Location = new System.Drawing.Point(3, 63);
            this.dgvAIOCard.MultiSelect = false;
            this.dgvAIOCard.Name = "dgvAIOCard";
            this.dgvAIOCard.RowHeadersVisible = false;
            this.dgvAIOCard.RowTemplate.Height = 24;
            this.dgvAIOCard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvAIOCard.Size = new System.Drawing.Size(1091, 509);
            this.dgvAIOCard.TabIndex = 8;
            this.dgvAIOCard.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            this.dgvAIOCard.EnabledChanged += new System.EventHandler(this.dgvAIOCard_EnabledChanged);
            // 
            // dgvMotion_No
            // 
            this.dgvMotion_No.Frozen = true;
            this.dgvMotion_No.HeaderText = "No.";
            this.dgvMotion_No.Name = "dgvMotion_No";
            this.dgvMotion_No.ReadOnly = true;
            this.dgvMotion_No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvMotion_No.Width = 50;
            // 
            // dgvMotion_CardType
            // 
            this.dgvMotion_CardType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvMotion_CardType.HeaderText = "Card Type";
            this.dgvMotion_CardType.Name = "dgvMotion_CardType";
            // 
            // dgvMotion_CardID
            // 
            this.dgvMotion_CardID.HeaderText = "Card ID";
            this.dgvMotion_CardID.Name = "dgvMotion_CardID";
            // 
            // dgvMotion_SlaveID
            // 
            this.dgvMotion_SlaveID.HeaderText = "Slave ID";
            this.dgvMotion_SlaveID.Name = "dgvMotion_SlaveID";
            this.dgvMotion_SlaveID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvMotion_SlaveType
            // 
            this.dgvMotion_SlaveType.HeaderText = "Slave Type (EtherCat)";
            this.dgvMotion_SlaveType.Name = "dgvMotion_SlaveType";
            this.dgvMotion_SlaveType.Width = 200;
            // 
            // dgvMotion_SlaveNum
            // 
            this.dgvMotion_SlaveNum.HeaderText = "Slave Num (Etel)";
            this.dgvMotion_SlaveNum.Name = "dgvMotion_SlaveNum";
            this.dgvMotion_SlaveNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvMotion_AxisID
            // 
            this.dgvMotion_AxisID.HeaderText = "Axis ID";
            this.dgvMotion_AxisID.Name = "dgvMotion_AxisID";
            this.dgvMotion_AxisID.ReadOnly = true;
            this.dgvMotion_AxisID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvSimulate
            // 
            this.dgvSimulate.HeaderText = "Simulate";
            this.dgvSimulate.Name = "dgvSimulate";
            this.dgvSimulate.ReadOnly = true;
            this.dgvSimulate.Width = 80;
            // 
            // dgvMotion_Other
            // 
            this.dgvMotion_Other.HeaderText = "Other";
            this.dgvMotion_Other.Name = "dgvMotion_Other";
            // 
            // dgvAxisSetting
            // 
            this.dgvMotion_AxisSetting.HeaderText = "Axis Setting";
            this.dgvMotion_AxisSetting.Name = "dgvAxisSetting";
            // 
            // dgvIOCard_No
            // 
            this.dgvIOCard_No.Frozen = true;
            this.dgvIOCard_No.HeaderText = "No.";
            this.dgvIOCard_No.Name = "dgvIOCard_No";
            this.dgvIOCard_No.ReadOnly = true;
            this.dgvIOCard_No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvIOCard_No.Width = 50;
            // 
            // dgvIOCard_CardType
            // 
            this.dgvIOCard_CardType.HeaderText = "Card Type";
            this.dgvIOCard_CardType.Name = "dgvIOCard_CardType";
            this.dgvIOCard_CardType.Width = 200;
            // 
            // dgvIOCard_SlaveType
            // 
            this.dgvIOCard_SlaveType.HeaderText = "Slave Type";
            this.dgvIOCard_SlaveType.Name = "dgvIOCard_SlaveType";
            this.dgvIOCard_SlaveType.Width = 150;
            // 
            // dgvIOCard_CardID
            // 
            this.dgvIOCard_CardID.HeaderText = "Card ID";
            this.dgvIOCard_CardID.Name = "dgvIOCard_CardID";
            // 
            // dgvIOCard_SlaveID
            // 
            this.dgvIOCard_SlaveID.HeaderText = "Slave ID";
            this.dgvIOCard_SlaveID.Name = "dgvIOCard_SlaveID";
            this.dgvIOCard_SlaveID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvIOCard_StartDI
            // 
            this.dgvIOCard_StartDI.HeaderText = "Start DI";
            this.dgvIOCard_StartDI.Name = "dgvIOCard_StartDI";
            this.dgvIOCard_StartDI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvIOCard_StartDO
            // 
            this.dgvIOCard_StartDO.HeaderText = "Start DO";
            this.dgvIOCard_StartDO.Name = "dgvIOCard_StartDO";
            this.dgvIOCard_StartDO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvIOCard_Simulate
            // 
            this.dgvIOCard_Simulate.HeaderText = "Simulate";
            this.dgvIOCard_Simulate.Name = "dgvIOCard_Simulate";
            this.dgvIOCard_Simulate.Width = 80;
            // 
            // dgvAIOCard_No
            // 
            this.dgvAIOCard_No.Frozen = true;
            this.dgvAIOCard_No.HeaderText = "No.";
            this.dgvAIOCard_No.Name = "dgvAIOCard_No";
            this.dgvAIOCard_No.ReadOnly = true;
            this.dgvAIOCard_No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvAIOCard_No.Width = 50;
            // 
            // dgvAIOCard_CardType
            // 
            this.dgvAIOCard_CardType.HeaderText = "Card Type";
            this.dgvAIOCard_CardType.Name = "dgvAIOCard_CardType";
            this.dgvAIOCard_CardType.Width = 200;
            // 
            // dgvAIOCard_CardID
            // 
            this.dgvAIOCard_CardID.HeaderText = "Card ID";
            this.dgvAIOCard_CardID.Name = "dgvAIOCard_CardID";
            // 
            // dgvAIOCard_SlaveID
            // 
            this.dgvAIOCard_SlaveID.HeaderText = "Slave ID";
            this.dgvAIOCard_SlaveID.Name = "dgvAIOCard_SlaveID";
            this.dgvAIOCard_SlaveID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvAIOCard_StartDI
            // 
            this.dgvAIOCard_StartDI.HeaderText = "Start DI";
            this.dgvAIOCard_StartDI.Name = "dgvAIOCard_StartDI";
            this.dgvAIOCard_StartDI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvAIOCard_StartDO
            // 
            this.dgvAIOCard_StartDO.HeaderText = "Start DO";
            this.dgvAIOCard_StartDO.Name = "dgvAIOCard_StartDO";
            this.dgvAIOCard_StartDO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ucCardSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tabControl1);
            this.Name = "ucCardSetting";
            this.Size = new System.Drawing.Size(1111, 611);
            this.VisibleChanged += new System.EventHandler(this.ucCardSetting_VisibleChanged);
            this.tabControl1.ResumeLayout(false);
            this.tPage_MotionCard.ResumeLayout(false);
            this.tPage_MotionCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotion)).EndInit();
            this.tPage_IOCard.ResumeLayout(false);
            this.tPage_IOCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIOCard)).EndInit();
            this.tPage_AIOCard.ResumeLayout(false);
            this.tPage_AIOCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAIOCard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPage_MotionCard;
        private System.Windows.Forms.TabPage tPage_IOCard;
        private System.Windows.Forms.TabPage tPage_AIOCard;
        private System.Windows.Forms.Button btnAdd_MotionCard;
        private System.Windows.Forms.Button btnDelete_MotionCard;
        private System.Windows.Forms.DataGridView dgvMotion;
        private System.Windows.Forms.Button btnSave_MotionCardSetting;
        private System.Windows.Forms.Button btnCancel_MotionCardSetting;
        private System.Windows.Forms.Button btnEdit_MotionCardSetting;
        private System.Windows.Forms.Button btnSave_IOCardSetting;
        private System.Windows.Forms.Button btnCancel_IOCardSetting;
        private System.Windows.Forms.Button btnEdit_IOCardSetting;
        private System.Windows.Forms.Button btnAdd_IOCard;
        private System.Windows.Forms.Button btnDelete_IOCard;
        private System.Windows.Forms.DataGridView dgvIOCard;
        private System.Windows.Forms.Button btnSave_AIOCardSetting;
        private System.Windows.Forms.Button btnCancel_AIOCardSetting;
        private System.Windows.Forms.Button btnEdit_AIOCardSetting;
        private System.Windows.Forms.Button btnAdd_AIOCard;
        private System.Windows.Forms.Button btnDelete_AIOCard;
        private System.Windows.Forms.DataGridView dgvAIOCard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_MotionCardPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_IOCardPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_AIOCardPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewButtonColumn dgvMotion_AxisSetting;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMotion_No;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvMotion_CardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMotion_CardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMotion_SlaveID;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvMotion_SlaveType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMotion_SlaveNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMotion_AxisID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvSimulate;
        private System.Windows.Forms.DataGridViewButtonColumn dgvMotion_Other;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvIOCard_No;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvIOCard_CardType;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvIOCard_SlaveType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvIOCard_CardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvIOCard_SlaveID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvIOCard_StartDI;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvIOCard_StartDO;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvIOCard_Simulate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAIOCard_No;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvAIOCard_CardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAIOCard_CardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAIOCard_SlaveID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAIOCard_StartDI;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAIOCard_StartDO;
    }
}

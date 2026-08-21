namespace ArtSystem.MultiSystem
{
    partial class ucWeightScaleSetting
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
            this.dgvSetWeightScale = new System.Windows.Forms.DataGridView();
            this.btnSave_WeightScaleSetting = new System.Windows.Forms.Button();
            this.btnCancel_WeightScaleSetting = new System.Windows.Forms.Button();
            this.btnEdit_WeightScaleSetting = new System.Windows.Forms.Button();
            this.btnAdd_WeightScaleSetting = new System.Windows.Forms.Button();
            this.btnDelete_WeightScaleSetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_WeightScalePath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvControllerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvControllerType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvComPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCOMBaudRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCOMBits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCOMTimeout = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCOMHandshake = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvCOMStopBits = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvCOMParity = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetWeightScale)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSetWeightScale
            // 
            this.dgvSetWeightScale.AllowUserToAddRows = false;
            this.dgvSetWeightScale.AllowUserToDeleteRows = false;
            this.dgvSetWeightScale.AllowUserToResizeColumns = false;
            this.dgvSetWeightScale.AllowUserToResizeRows = false;
            this.dgvSetWeightScale.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSetWeightScale.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetWeightScale.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvNo,
            this.dgvControllerName,
            this.dgvControllerType,
            this.dgvComPort,
            this.dgvCOMBaudRate,
            this.dgvCOMBits,
            this.dgvCOMTimeout,
            this.dgvCOMHandshake,
            this.dgvCOMStopBits,
            this.dgvCOMParity});
            this.dgvSetWeightScale.Location = new System.Drawing.Point(8, 61);
            this.dgvSetWeightScale.MultiSelect = false;
            this.dgvSetWeightScale.Name = "dgvSetWeightScale";
            this.dgvSetWeightScale.RowHeadersVisible = false;
            this.dgvSetWeightScale.RowTemplate.Height = 24;
            this.dgvSetWeightScale.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSetWeightScale.Size = new System.Drawing.Size(1173, 508);
            this.dgvSetWeightScale.TabIndex = 10;
            this.dgvSetWeightScale.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSetWeightScale_CellClick);
            this.dgvSetWeightScale.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            this.dgvSetWeightScale.EnabledChanged += new System.EventHandler(this.dgvSetWeightScale_EnabledChanged);
            // 
            // btnSave_WeightScaleSetting
            // 
            this.btnSave_WeightScaleSetting.Location = new System.Drawing.Point(3, 3);
            this.btnSave_WeightScaleSetting.Name = "btnSave_WeightScaleSetting";
            this.btnSave_WeightScaleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnSave_WeightScaleSetting.TabIndex = 17;
            this.btnSave_WeightScaleSetting.Text = "Save";
            this.btnSave_WeightScaleSetting.UseVisualStyleBackColor = true;
            this.btnSave_WeightScaleSetting.Click += new System.EventHandler(this.btnSave_WeightScaleSetting_Click);
            // 
            // btnCancel_WeightScaleSetting
            // 
            this.btnCancel_WeightScaleSetting.Location = new System.Drawing.Point(109, 3);
            this.btnCancel_WeightScaleSetting.Name = "btnCancel_WeightScaleSetting";
            this.btnCancel_WeightScaleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_WeightScaleSetting.TabIndex = 16;
            this.btnCancel_WeightScaleSetting.Text = "Cancel";
            this.btnCancel_WeightScaleSetting.UseVisualStyleBackColor = true;
            this.btnCancel_WeightScaleSetting.Click += new System.EventHandler(this.btnCancel_WeightScaleSetting_Click);
            // 
            // btnEdit_WeightScaleSetting
            // 
            this.btnEdit_WeightScaleSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_WeightScaleSetting.Location = new System.Drawing.Point(1081, 3);
            this.btnEdit_WeightScaleSetting.Name = "btnEdit_WeightScaleSetting";
            this.btnEdit_WeightScaleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_WeightScaleSetting.TabIndex = 18;
            this.btnEdit_WeightScaleSetting.Text = "Edit";
            this.btnEdit_WeightScaleSetting.UseVisualStyleBackColor = true;
            this.btnEdit_WeightScaleSetting.Click += new System.EventHandler(this.btnEdit_WeightScaleSetting_Click);
            // 
            // btnAdd_WeightScaleSetting
            // 
            this.btnAdd_WeightScaleSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd_WeightScaleSetting.Location = new System.Drawing.Point(869, 3);
            this.btnAdd_WeightScaleSetting.Name = "btnAdd_WeightScaleSetting";
            this.btnAdd_WeightScaleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnAdd_WeightScaleSetting.TabIndex = 15;
            this.btnAdd_WeightScaleSetting.Text = "Add";
            this.btnAdd_WeightScaleSetting.UseVisualStyleBackColor = true;
            this.btnAdd_WeightScaleSetting.Click += new System.EventHandler(this.btnAdd_WeightScaleSetting_Click);
            // 
            // btnDelete_WeightScaleSetting
            // 
            this.btnDelete_WeightScaleSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete_WeightScaleSetting.Location = new System.Drawing.Point(975, 3);
            this.btnDelete_WeightScaleSetting.Name = "btnDelete_WeightScaleSetting";
            this.btnDelete_WeightScaleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnDelete_WeightScaleSetting.TabIndex = 14;
            this.btnDelete_WeightScaleSetting.Text = "Delete";
            this.btnDelete_WeightScaleSetting.UseVisualStyleBackColor = true;
            this.btnDelete_WeightScaleSetting.Click += new System.EventHandler(this.btnDelete_WeightScaleSetting_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "File Path:";
            // 
            // txt_WeightScalePath
            // 
            this.txt_WeightScalePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_WeightScalePath.Location = new System.Drawing.Point(215, 32);
            this.txt_WeightScalePath.Name = "txt_WeightScalePath";
            this.txt_WeightScalePath.Size = new System.Drawing.Size(648, 23);
            this.txt_WeightScalePath.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(707, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 25);
            this.button1.TabIndex = 21;
            this.button1.Text = "Advance Setting";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvNo
            // 
            this.dgvNo.HeaderText = "No.";
            this.dgvNo.Name = "dgvNo";
            this.dgvNo.ReadOnly = true;
            this.dgvNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvNo.Width = 40;
            // 
            // dgvControllerName
            // 
            this.dgvControllerName.HeaderText = "Controller Name";
            this.dgvControllerName.Name = "dgvControllerName";
            this.dgvControllerName.ReadOnly = true;
            this.dgvControllerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvControllerName.Width = 150;
            // 
            // dgvControllerType
            // 
            this.dgvControllerType.HeaderText = "Controller Type";
            this.dgvControllerType.Name = "dgvControllerType";
            this.dgvControllerType.Width = 200;
            // 
            // dgvComPort
            // 
            this.dgvComPort.HeaderText = "COM Port(ID)";
            this.dgvComPort.Name = "dgvComPort";
            this.dgvComPort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvComPort.Width = 80;
            // 
            // dgvCOMBaudRate
            // 
            this.dgvCOMBaudRate.HeaderText = "COM BaudRate";
            this.dgvCOMBaudRate.Name = "dgvCOMBaudRate";
            this.dgvCOMBaudRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCOMBaudRate.Width = 80;
            // 
            // dgvCOMBits
            // 
            this.dgvCOMBits.HeaderText = "COM Bits";
            this.dgvCOMBits.Name = "dgvCOMBits";
            this.dgvCOMBits.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCOMBits.Width = 80;
            // 
            // dgvCOMTimeout
            // 
            this.dgvCOMTimeout.HeaderText = "COM Timeout";
            this.dgvCOMTimeout.Name = "dgvCOMTimeout";
            this.dgvCOMTimeout.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCOMTimeout.Width = 80;
            // 
            // dgvCOMHandshake
            // 
            this.dgvCOMHandshake.HeaderText = "COM Handshake";
            this.dgvCOMHandshake.Name = "dgvCOMHandshake";
            // 
            // dgvCOMStopBits
            // 
            this.dgvCOMStopBits.HeaderText = "COM StopBits";
            this.dgvCOMStopBits.Name = "dgvCOMStopBits";
            this.dgvCOMStopBits.Width = 80;
            // 
            // dgvCOMParity
            // 
            this.dgvCOMParity.HeaderText = "COM Parity";
            this.dgvCOMParity.Name = "dgvCOMParity";
            this.dgvCOMParity.Width = 80;
            // 
            // ucWeightScaleSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_WeightScalePath);
            this.Controls.Add(this.btnSave_WeightScaleSetting);
            this.Controls.Add(this.btnCancel_WeightScaleSetting);
            this.Controls.Add(this.btnEdit_WeightScaleSetting);
            this.Controls.Add(this.btnAdd_WeightScaleSetting);
            this.Controls.Add(this.btnDelete_WeightScaleSetting);
            this.Controls.Add(this.dgvSetWeightScale);
            this.Name = "ucWeightScaleSetting";
            this.Size = new System.Drawing.Size(1184, 572);
            this.VisibleChanged += new System.EventHandler(this.ucWeightScaleSetting_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetWeightScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSetWeightScale;
        private System.Windows.Forms.Button btnSave_WeightScaleSetting;
        private System.Windows.Forms.Button btnCancel_WeightScaleSetting;
        private System.Windows.Forms.Button btnEdit_WeightScaleSetting;
        private System.Windows.Forms.Button btnAdd_WeightScaleSetting;
        private System.Windows.Forms.Button btnDelete_WeightScaleSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_WeightScalePath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvControllerName;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvControllerType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvComPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCOMBaudRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCOMBits;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCOMTimeout;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvCOMHandshake;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvCOMStopBits;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvCOMParity;
    }
}

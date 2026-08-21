namespace ArtSystem.MultiSystem
{
    partial class ucBottomCCDSetting
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
            this.dgvSetBottomCCD = new System.Windows.Forms.DataGridView();
            this.btnSave_BottomCCDSetting = new System.Windows.Forms.Button();
            this.btnCancel_BottomCCDSetting = new System.Windows.Forms.Button();
            this.btnEdit_BottomCCDSetting = new System.Windows.Forms.Button();
            this.btnAdd_BottomCCDSetting = new System.Windows.Forms.Button();
            this.btnDelete_BottomCCDSetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_BottomCCDPath = new System.Windows.Forms.TextBox();
            this.dgvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSensorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCCDType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvTcpIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTcpPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTimeout = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDelayTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSavePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetBottomCCD)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSetBottomCCD
            // 
            this.dgvSetBottomCCD.AllowUserToAddRows = false;
            this.dgvSetBottomCCD.AllowUserToDeleteRows = false;
            this.dgvSetBottomCCD.AllowUserToResizeColumns = false;
            this.dgvSetBottomCCD.AllowUserToResizeRows = false;
            this.dgvSetBottomCCD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSetBottomCCD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetBottomCCD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvNo,
            this.dgvSensorName,
            this.dgvCCDType,
            this.dgvTcpIp,
            this.dgvTcpPort,
            this.dgvTimeout,
            this.dgvDelayTime,
            this.dgvSavePath});
            this.dgvSetBottomCCD.Location = new System.Drawing.Point(8, 61);
            this.dgvSetBottomCCD.MultiSelect = false;
            this.dgvSetBottomCCD.Name = "dgvSetBottomCCD";
            this.dgvSetBottomCCD.RowHeadersVisible = false;
            this.dgvSetBottomCCD.RowTemplate.Height = 24;
            this.dgvSetBottomCCD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSetBottomCCD.Size = new System.Drawing.Size(1173, 508);
            this.dgvSetBottomCCD.TabIndex = 10;
            this.dgvSetBottomCCD.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSetBottomCCD_CellClick);
            this.dgvSetBottomCCD.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            this.dgvSetBottomCCD.EnabledChanged += new System.EventHandler(this.dgvSetBottomCCD_EnabledChanged);
            // 
            // btnSave_BottomCCDSetting
            // 
            this.btnSave_BottomCCDSetting.Location = new System.Drawing.Point(3, 3);
            this.btnSave_BottomCCDSetting.Name = "btnSave_BottomCCDSetting";
            this.btnSave_BottomCCDSetting.Size = new System.Drawing.Size(100, 52);
            this.btnSave_BottomCCDSetting.TabIndex = 17;
            this.btnSave_BottomCCDSetting.Text = "Save";
            this.btnSave_BottomCCDSetting.UseVisualStyleBackColor = true;
            this.btnSave_BottomCCDSetting.Click += new System.EventHandler(this.btnSave_BottomCCDSetting_Click);
            // 
            // btnCancel_BottomCCDSetting
            // 
            this.btnCancel_BottomCCDSetting.Location = new System.Drawing.Point(109, 3);
            this.btnCancel_BottomCCDSetting.Name = "btnCancel_BottomCCDSetting";
            this.btnCancel_BottomCCDSetting.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_BottomCCDSetting.TabIndex = 16;
            this.btnCancel_BottomCCDSetting.Text = "Cancel";
            this.btnCancel_BottomCCDSetting.UseVisualStyleBackColor = true;
            this.btnCancel_BottomCCDSetting.Click += new System.EventHandler(this.btnCancel_BottomCCDSetting_Click);
            // 
            // btnEdit_BottomCCDSetting
            // 
            this.btnEdit_BottomCCDSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_BottomCCDSetting.Location = new System.Drawing.Point(1081, 3);
            this.btnEdit_BottomCCDSetting.Name = "btnEdit_BottomCCDSetting";
            this.btnEdit_BottomCCDSetting.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_BottomCCDSetting.TabIndex = 18;
            this.btnEdit_BottomCCDSetting.Text = "Edit";
            this.btnEdit_BottomCCDSetting.UseVisualStyleBackColor = true;
            this.btnEdit_BottomCCDSetting.Click += new System.EventHandler(this.btnEdit_BottomCCDSetting_Click);
            // 
            // btnAdd_BottomCCDSetting
            // 
            this.btnAdd_BottomCCDSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd_BottomCCDSetting.Location = new System.Drawing.Point(869, 3);
            this.btnAdd_BottomCCDSetting.Name = "btnAdd_BottomCCDSetting";
            this.btnAdd_BottomCCDSetting.Size = new System.Drawing.Size(100, 52);
            this.btnAdd_BottomCCDSetting.TabIndex = 15;
            this.btnAdd_BottomCCDSetting.Text = "Add";
            this.btnAdd_BottomCCDSetting.UseVisualStyleBackColor = true;
            this.btnAdd_BottomCCDSetting.Click += new System.EventHandler(this.btnAdd_BottomCCDSetting_Click);
            // 
            // btnDelete_BottomCCDSetting
            // 
            this.btnDelete_BottomCCDSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete_BottomCCDSetting.Location = new System.Drawing.Point(975, 3);
            this.btnDelete_BottomCCDSetting.Name = "btnDelete_BottomCCDSetting";
            this.btnDelete_BottomCCDSetting.Size = new System.Drawing.Size(100, 52);
            this.btnDelete_BottomCCDSetting.TabIndex = 14;
            this.btnDelete_BottomCCDSetting.Text = "Delete";
            this.btnDelete_BottomCCDSetting.UseVisualStyleBackColor = true;
            this.btnDelete_BottomCCDSetting.Click += new System.EventHandler(this.btnDelete_BottomCCDSetting_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "File Path:";
            // 
            // txt_BottomCCDPath
            // 
            this.txt_BottomCCDPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_BottomCCDPath.Location = new System.Drawing.Point(215, 32);
            this.txt_BottomCCDPath.Name = "txt_BottomCCDPath";
            this.txt_BottomCCDPath.Size = new System.Drawing.Size(648, 23);
            this.txt_BottomCCDPath.TabIndex = 19;
            // 
            // dgvNo
            // 
            this.dgvNo.HeaderText = "No.";
            this.dgvNo.Name = "dgvNo";
            this.dgvNo.ReadOnly = true;
            this.dgvNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvNo.Width = 40;
            // 
            // dgvSensorName
            // 
            this.dgvSensorName.HeaderText = "CCDName";
            this.dgvSensorName.Name = "dgvSensorName";
            // 
            // dgvCCDType
            // 
            this.dgvCCDType.HeaderText = "CCD Type";
            this.dgvCCDType.Name = "dgvCCDType";
            this.dgvCCDType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCCDType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dgvTcpIp
            // 
            this.dgvTcpIp.HeaderText = "TCP IP";
            this.dgvTcpIp.Name = "dgvTcpIp";
            this.dgvTcpIp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvTcpIp.Width = 150;
            // 
            // dgvTcpPort
            // 
            this.dgvTcpPort.HeaderText = "TCP Port";
            this.dgvTcpPort.Name = "dgvTcpPort";
            this.dgvTcpPort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvTcpPort.Width = 80;
            // 
            // dgvTimeout
            // 
            this.dgvTimeout.HeaderText = "Timeout";
            this.dgvTimeout.Name = "dgvTimeout";
            this.dgvTimeout.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvTimeout.Width = 80;
            // 
            // dgvDelayTime
            // 
            this.dgvDelayTime.HeaderText = "Delay Time";
            this.dgvDelayTime.Name = "dgvDelayTime";
            this.dgvDelayTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvDelayTime.Width = 80;
            // 
            // dgvSavePath
            // 
            this.dgvSavePath.HeaderText = "Save Path";
            this.dgvSavePath.Name = "dgvSavePath";
            this.dgvSavePath.Width = 500;
            // 
            // ucBottomCCDSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_BottomCCDPath);
            this.Controls.Add(this.btnSave_BottomCCDSetting);
            this.Controls.Add(this.btnCancel_BottomCCDSetting);
            this.Controls.Add(this.btnEdit_BottomCCDSetting);
            this.Controls.Add(this.btnAdd_BottomCCDSetting);
            this.Controls.Add(this.btnDelete_BottomCCDSetting);
            this.Controls.Add(this.dgvSetBottomCCD);
            this.Name = "ucBottomCCDSetting";
            this.Size = new System.Drawing.Size(1184, 572);
            this.VisibleChanged += new System.EventHandler(this.ucBottomCCDSetting_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetBottomCCD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSetBottomCCD;
        private System.Windows.Forms.Button btnSave_BottomCCDSetting;
        private System.Windows.Forms.Button btnCancel_BottomCCDSetting;
        private System.Windows.Forms.Button btnEdit_BottomCCDSetting;
        private System.Windows.Forms.Button btnAdd_BottomCCDSetting;
        private System.Windows.Forms.Button btnDelete_BottomCCDSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_BottomCCDPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSensorName;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvCCDType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTcpIp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTcpPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTimeout;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDelayTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSavePath;
    }
}

namespace ArtSystem.MultiSystem
{
    partial class ucRollerMotorSetting
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
            this.dgvSetRollerMotor = new System.Windows.Forms.DataGridView();
            this.btnSave_RollerMotorSetting = new System.Windows.Forms.Button();
            this.btnCancel_RollerMotorSetting = new System.Windows.Forms.Button();
            this.btnEdit_RollerMotorSetting = new System.Windows.Forms.Button();
            this.btnAdd_RollerMotorSetting = new System.Windows.Forms.Button();
            this.btnDelete_RollerMotorSetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_FilePath = new System.Windows.Forms.TextBox();
            this.dgvRollerType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvComPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMotorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHighSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvLowSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvInvert = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvEnumHighSpeed = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvEnumLowSpeed = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvDOStart = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvDOSlow = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvDOReverse = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvCurrentPower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetRollerMotor)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSetRollerMotor
            // 
            this.dgvSetRollerMotor.AllowUserToAddRows = false;
            this.dgvSetRollerMotor.AllowUserToDeleteRows = false;
            this.dgvSetRollerMotor.AllowUserToResizeColumns = false;
            this.dgvSetRollerMotor.AllowUserToResizeRows = false;
            this.dgvSetRollerMotor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSetRollerMotor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetRollerMotor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvRollerType,
            this.dgvComPort,
            this.dgvMotorID,
            this.dgvHighSpeed,
            this.dgvLowSpeed,
            this.dgvInvert,
            this.dgvEnumHighSpeed,
            this.dgvEnumLowSpeed,
            this.dgvDOStart,
            this.dgvDOSlow,
            this.dgvDOReverse,
            this.dgvCurrentPower});
            this.dgvSetRollerMotor.Location = new System.Drawing.Point(8, 61);
            this.dgvSetRollerMotor.MultiSelect = false;
            this.dgvSetRollerMotor.Name = "dgvSetRollerMotor";
            this.dgvSetRollerMotor.RowTemplate.Height = 24;
            this.dgvSetRollerMotor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSetRollerMotor.Size = new System.Drawing.Size(1173, 508);
            this.dgvSetRollerMotor.TabIndex = 10;
            this.dgvSetRollerMotor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSetRollerMotor_CellClick);
            this.dgvSetRollerMotor.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            this.dgvSetRollerMotor.EnabledChanged += new System.EventHandler(this.dgvSetRollerMotor_EnabledChanged);
            // 
            // btnSave_RollerMotorSetting
            // 
            this.btnSave_RollerMotorSetting.Location = new System.Drawing.Point(3, 3);
            this.btnSave_RollerMotorSetting.Name = "btnSave_RollerMotorSetting";
            this.btnSave_RollerMotorSetting.Size = new System.Drawing.Size(100, 52);
            this.btnSave_RollerMotorSetting.TabIndex = 17;
            this.btnSave_RollerMotorSetting.Text = "Save";
            this.btnSave_RollerMotorSetting.UseVisualStyleBackColor = true;
            this.btnSave_RollerMotorSetting.Click += new System.EventHandler(this.btnSave_RollerMotorSetting_Click);
            // 
            // btnCancel_RollerMotorSetting
            // 
            this.btnCancel_RollerMotorSetting.Location = new System.Drawing.Point(109, 3);
            this.btnCancel_RollerMotorSetting.Name = "btnCancel_RollerMotorSetting";
            this.btnCancel_RollerMotorSetting.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_RollerMotorSetting.TabIndex = 16;
            this.btnCancel_RollerMotorSetting.Text = "Cancel";
            this.btnCancel_RollerMotorSetting.UseVisualStyleBackColor = true;
            this.btnCancel_RollerMotorSetting.Click += new System.EventHandler(this.btnCancel_RollerMotorSetting_Click);
            // 
            // btnEdit_RollerMotorSetting
            // 
            this.btnEdit_RollerMotorSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_RollerMotorSetting.Location = new System.Drawing.Point(1081, 3);
            this.btnEdit_RollerMotorSetting.Name = "btnEdit_RollerMotorSetting";
            this.btnEdit_RollerMotorSetting.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_RollerMotorSetting.TabIndex = 18;
            this.btnEdit_RollerMotorSetting.Text = "Edit";
            this.btnEdit_RollerMotorSetting.UseVisualStyleBackColor = true;
            this.btnEdit_RollerMotorSetting.Click += new System.EventHandler(this.btnEdit_RollerMotorSetting_Click);
            // 
            // btnAdd_RollerMotorSetting
            // 
            this.btnAdd_RollerMotorSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd_RollerMotorSetting.Location = new System.Drawing.Point(869, 3);
            this.btnAdd_RollerMotorSetting.Name = "btnAdd_RollerMotorSetting";
            this.btnAdd_RollerMotorSetting.Size = new System.Drawing.Size(100, 52);
            this.btnAdd_RollerMotorSetting.TabIndex = 15;
            this.btnAdd_RollerMotorSetting.Text = "Add";
            this.btnAdd_RollerMotorSetting.UseVisualStyleBackColor = true;
            this.btnAdd_RollerMotorSetting.Click += new System.EventHandler(this.btnAdd_RollerMotorSetting_Click);
            // 
            // btnDelete_RollerMotorSetting
            // 
            this.btnDelete_RollerMotorSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete_RollerMotorSetting.Location = new System.Drawing.Point(975, 3);
            this.btnDelete_RollerMotorSetting.Name = "btnDelete_RollerMotorSetting";
            this.btnDelete_RollerMotorSetting.Size = new System.Drawing.Size(100, 52);
            this.btnDelete_RollerMotorSetting.TabIndex = 14;
            this.btnDelete_RollerMotorSetting.Text = "Delete";
            this.btnDelete_RollerMotorSetting.UseVisualStyleBackColor = true;
            this.btnDelete_RollerMotorSetting.Click += new System.EventHandler(this.btnDelete_RollerMotorSetting_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 22;
            this.label3.Text = "File Path:";
            // 
            // txt_FilePath
            // 
            this.txt_FilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_FilePath.Location = new System.Drawing.Point(215, 32);
            this.txt_FilePath.Name = "txt_FilePath";
            this.txt_FilePath.Size = new System.Drawing.Size(648, 23);
            this.txt_FilePath.TabIndex = 21;
            // 
            // dgvRollerType
            // 
            this.dgvRollerType.HeaderText = "Roller Type";
            this.dgvRollerType.Name = "dgvRollerType";
            this.dgvRollerType.Width = 80;
            // 
            // dgvComPort
            // 
            this.dgvComPort.HeaderText = "COM Port(ID)";
            this.dgvComPort.Name = "dgvComPort";
            this.dgvComPort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvComPort.Width = 80;
            // 
            // dgvMotorID
            // 
            this.dgvMotorID.HeaderText = "Motor ID";
            this.dgvMotorID.Name = "dgvMotorID";
            this.dgvMotorID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvMotorID.Width = 80;
            // 
            // dgvHighSpeed
            // 
            this.dgvHighSpeed.HeaderText = "High Speed (pps)";
            this.dgvHighSpeed.Name = "dgvHighSpeed";
            this.dgvHighSpeed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvHighSpeed.Width = 80;
            // 
            // dgvLowSpeed
            // 
            this.dgvLowSpeed.HeaderText = "Low Speed (pps)";
            this.dgvLowSpeed.Name = "dgvLowSpeed";
            this.dgvLowSpeed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvLowSpeed.Width = 80;
            // 
            // dgvInvert
            // 
            this.dgvInvert.HeaderText = "Invert";
            this.dgvInvert.Name = "dgvInvert";
            this.dgvInvert.Width = 80;
            // 
            // dgvEnumHighSpeed
            // 
            this.dgvEnumHighSpeed.HeaderText = "High Speed(Enum)";
            this.dgvEnumHighSpeed.Name = "dgvEnumHighSpeed";
            this.dgvEnumHighSpeed.Width = 80;
            // 
            // dgvEnumLowSpeed
            // 
            this.dgvEnumLowSpeed.HeaderText = "Low Speed(Enum)";
            this.dgvEnumLowSpeed.Name = "dgvEnumLowSpeed";
            this.dgvEnumLowSpeed.Width = 80;
            // 
            // dgvDOStart
            // 
            this.dgvDOStart.HeaderText = "DO Start";
            this.dgvDOStart.Name = "dgvDOStart";
            this.dgvDOStart.Width = 80;
            // 
            // dgvDOSlow
            // 
            this.dgvDOSlow.HeaderText = "DO Slow";
            this.dgvDOSlow.Name = "dgvDOSlow";
            this.dgvDOSlow.Width = 80;
            // 
            // dgvDOReverse
            // 
            this.dgvDOReverse.HeaderText = "DO Reverse";
            this.dgvDOReverse.Name = "dgvDOReverse";
            this.dgvDOReverse.Width = 80;
            // 
            // dgvCurrentPower
            // 
            this.dgvCurrentPower.HeaderText = "Current Power(A)";
            this.dgvCurrentPower.Name = "dgvCurrentPower";
            this.dgvCurrentPower.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ucRollerMotorSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_FilePath);
            this.Controls.Add(this.btnSave_RollerMotorSetting);
            this.Controls.Add(this.btnCancel_RollerMotorSetting);
            this.Controls.Add(this.btnEdit_RollerMotorSetting);
            this.Controls.Add(this.btnAdd_RollerMotorSetting);
            this.Controls.Add(this.btnDelete_RollerMotorSetting);
            this.Controls.Add(this.dgvSetRollerMotor);
            this.Name = "ucRollerMotorSetting";
            this.Size = new System.Drawing.Size(1184, 572);
            this.VisibleChanged += new System.EventHandler(this.ucRollerMotorSetting_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetRollerMotor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSetRollerMotor;
        private System.Windows.Forms.Button btnSave_RollerMotorSetting;
        private System.Windows.Forms.Button btnCancel_RollerMotorSetting;
        private System.Windows.Forms.Button btnEdit_RollerMotorSetting;
        private System.Windows.Forms.Button btnAdd_RollerMotorSetting;
        private System.Windows.Forms.Button btnDelete_RollerMotorSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_FilePath;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvRollerType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvComPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMotorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHighSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvLowSpeed;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvInvert;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvEnumHighSpeed;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvEnumLowSpeed;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvDOStart;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvDOSlow;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvDOReverse;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCurrentPower;



    }
}

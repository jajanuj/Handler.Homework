namespace ArtSystem.MultiSystem
{
    partial class ucHighSensorSetting
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
            this.dgvSetHighSensor = new System.Windows.Forms.DataGridView();
            this.btnSave_HighSensorSetting = new System.Windows.Forms.Button();
            this.btnCancel_HighSensorSetting = new System.Windows.Forms.Button();
            this.btnEdit_HighSensorSetting = new System.Windows.Forms.Button();
            this.btnAdd_HighSensorSetting = new System.Windows.Forms.Button();
            this.btnDelete_HighSensorSetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_HighSensorPath = new System.Windows.Forms.TextBox();
            this.dgvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSensorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSensorModule = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvSensorType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvComPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCOMBaudRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCOMBits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCOMStationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTCPIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTCPPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvInvert = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvTouchCylinder_Do = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvLaserDI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetHighSensor)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSetHighSensor
            // 
            this.dgvSetHighSensor.AllowUserToAddRows = false;
            this.dgvSetHighSensor.AllowUserToDeleteRows = false;
            this.dgvSetHighSensor.AllowUserToResizeColumns = false;
            this.dgvSetHighSensor.AllowUserToResizeRows = false;
            this.dgvSetHighSensor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSetHighSensor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetHighSensor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvNo,
            this.dgvSensorName,
            this.dgvSensorModule,
            this.dgvSensorType,
            this.dgvComPort,
            this.dgvCOMBaudRate,
            this.dgvCOMBits,
            this.dgvCOMStationID,
            this.dgvTCPIP,
            this.dgvTCPPort,
            this.dgvInvert,
            this.dgvTouchCylinder_Do,
            this.dgvLaserDI});
            this.dgvSetHighSensor.Location = new System.Drawing.Point(8, 61);
            this.dgvSetHighSensor.MultiSelect = false;
            this.dgvSetHighSensor.Name = "dgvSetHighSensor";
            this.dgvSetHighSensor.RowHeadersVisible = false;
            this.dgvSetHighSensor.RowTemplate.Height = 24;
            this.dgvSetHighSensor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSetHighSensor.Size = new System.Drawing.Size(1173, 508);
            this.dgvSetHighSensor.TabIndex = 10;
            this.dgvSetHighSensor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSetHighSensor_CellClick);
            this.dgvSetHighSensor.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            this.dgvSetHighSensor.EnabledChanged += new System.EventHandler(this.dgvSetHighSensor_EnabledChanged);
            // 
            // btnSave_HighSensorSetting
            // 
            this.btnSave_HighSensorSetting.Location = new System.Drawing.Point(3, 3);
            this.btnSave_HighSensorSetting.Name = "btnSave_HighSensorSetting";
            this.btnSave_HighSensorSetting.Size = new System.Drawing.Size(100, 52);
            this.btnSave_HighSensorSetting.TabIndex = 17;
            this.btnSave_HighSensorSetting.Text = "Save";
            this.btnSave_HighSensorSetting.UseVisualStyleBackColor = true;
            this.btnSave_HighSensorSetting.Click += new System.EventHandler(this.btnSave_HighSensorSetting_Click);
            // 
            // btnCancel_HighSensorSetting
            // 
            this.btnCancel_HighSensorSetting.Location = new System.Drawing.Point(109, 3);
            this.btnCancel_HighSensorSetting.Name = "btnCancel_HighSensorSetting";
            this.btnCancel_HighSensorSetting.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_HighSensorSetting.TabIndex = 16;
            this.btnCancel_HighSensorSetting.Text = "Cancel";
            this.btnCancel_HighSensorSetting.UseVisualStyleBackColor = true;
            this.btnCancel_HighSensorSetting.Click += new System.EventHandler(this.btnCancel_HighSensorSetting_Click);
            // 
            // btnEdit_HighSensorSetting
            // 
            this.btnEdit_HighSensorSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_HighSensorSetting.Location = new System.Drawing.Point(1081, 3);
            this.btnEdit_HighSensorSetting.Name = "btnEdit_HighSensorSetting";
            this.btnEdit_HighSensorSetting.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_HighSensorSetting.TabIndex = 18;
            this.btnEdit_HighSensorSetting.Text = "Edit";
            this.btnEdit_HighSensorSetting.UseVisualStyleBackColor = true;
            this.btnEdit_HighSensorSetting.Click += new System.EventHandler(this.btnEdit_HighSensorSetting_Click);
            // 
            // btnAdd_HighSensorSetting
            // 
            this.btnAdd_HighSensorSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd_HighSensorSetting.Location = new System.Drawing.Point(869, 3);
            this.btnAdd_HighSensorSetting.Name = "btnAdd_HighSensorSetting";
            this.btnAdd_HighSensorSetting.Size = new System.Drawing.Size(100, 52);
            this.btnAdd_HighSensorSetting.TabIndex = 15;
            this.btnAdd_HighSensorSetting.Text = "Add";
            this.btnAdd_HighSensorSetting.UseVisualStyleBackColor = true;
            this.btnAdd_HighSensorSetting.Click += new System.EventHandler(this.btnAdd_HighSensorSetting_Click);
            // 
            // btnDelete_HighSensorSetting
            // 
            this.btnDelete_HighSensorSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete_HighSensorSetting.Location = new System.Drawing.Point(975, 3);
            this.btnDelete_HighSensorSetting.Name = "btnDelete_HighSensorSetting";
            this.btnDelete_HighSensorSetting.Size = new System.Drawing.Size(100, 52);
            this.btnDelete_HighSensorSetting.TabIndex = 14;
            this.btnDelete_HighSensorSetting.Text = "Delete";
            this.btnDelete_HighSensorSetting.UseVisualStyleBackColor = true;
            this.btnDelete_HighSensorSetting.Click += new System.EventHandler(this.btnDelete_HighSensorSetting_Click);
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
            // txt_HighSensorPath
            // 
            this.txt_HighSensorPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_HighSensorPath.Location = new System.Drawing.Point(215, 32);
            this.txt_HighSensorPath.Name = "txt_HighSensorPath";
            this.txt_HighSensorPath.Size = new System.Drawing.Size(648, 23);
            this.txt_HighSensorPath.TabIndex = 19;
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
            this.dgvSensorName.HeaderText = "Sensor Name";
            this.dgvSensorName.Name = "dgvSensorName";
            this.dgvSensorName.ReadOnly = true;
            this.dgvSensorName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvSensorName.Width = 150;
            // 
            // dgvSensorModule
            // 
            this.dgvSensorModule.HeaderText = "Sensor Module";
            this.dgvSensorModule.Name = "dgvSensorModule";
            this.dgvSensorModule.Width = 200;
            // 
            // dgvSensorType
            // 
            this.dgvSensorType.HeaderText = "Sensor Type";
            this.dgvSensorType.Name = "dgvSensorType";
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
            // dgvCOMStationID
            // 
            this.dgvCOMStationID.HeaderText = "COM StationID";
            this.dgvCOMStationID.Name = "dgvCOMStationID";
            this.dgvCOMStationID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCOMStationID.Width = 80;
            // 
            // dgvTCPIP
            // 
            this.dgvTCPIP.HeaderText = "TCP IP";
            this.dgvTCPIP.Name = "dgvTCPIP";
            this.dgvTCPIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvTCPIP.Width = 150;
            // 
            // dgvTCPPort
            // 
            this.dgvTCPPort.HeaderText = "TCP Port";
            this.dgvTCPPort.Name = "dgvTCPPort";
            this.dgvTCPPort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvTCPPort.Width = 80;
            // 
            // dgvInvert
            // 
            this.dgvInvert.HeaderText = "Invert";
            this.dgvInvert.Name = "dgvInvert";
            this.dgvInvert.Width = 70;
            // 
            // dgvTouchCylinder_Do
            // 
            this.dgvTouchCylinder_Do.HeaderText = "TouchCylinder DO";
            this.dgvTouchCylinder_Do.Name = "dgvTouchCylinder_Do";
            this.dgvTouchCylinder_Do.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvLaserDI
            // 
            this.dgvLaserDI.HeaderText = "Laser DI";
            this.dgvLaserDI.Name = "dgvLaserDI";
            this.dgvLaserDI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ucHighSensorSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_HighSensorPath);
            this.Controls.Add(this.btnSave_HighSensorSetting);
            this.Controls.Add(this.btnCancel_HighSensorSetting);
            this.Controls.Add(this.btnEdit_HighSensorSetting);
            this.Controls.Add(this.btnAdd_HighSensorSetting);
            this.Controls.Add(this.btnDelete_HighSensorSetting);
            this.Controls.Add(this.dgvSetHighSensor);
            this.Name = "ucHighSensorSetting";
            this.Size = new System.Drawing.Size(1184, 572);
            this.VisibleChanged += new System.EventHandler(this.ucHighSensorSetting_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetHighSensor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSetHighSensor;
        private System.Windows.Forms.Button btnSave_HighSensorSetting;
        private System.Windows.Forms.Button btnCancel_HighSensorSetting;
        private System.Windows.Forms.Button btnEdit_HighSensorSetting;
        private System.Windows.Forms.Button btnAdd_HighSensorSetting;
        private System.Windows.Forms.Button btnDelete_HighSensorSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_HighSensorPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSensorName;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvSensorModule;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvSensorType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvComPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCOMBaudRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCOMBits;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCOMStationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTCPIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTCPPort;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvInvert;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTouchCylinder_Do;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvLaserDI;



    }
}

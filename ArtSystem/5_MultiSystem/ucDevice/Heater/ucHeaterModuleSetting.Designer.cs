namespace ArtSystem.MultiSystem
{
    partial class ucHeaterModuleSetting
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
            this.dgvSetHeaterModule = new System.Windows.Forms.DataGridView();
            this.btnSave_HeaterModuleSetting = new System.Windows.Forms.Button();
            this.btnCancel_HeaterModuleSetting = new System.Windows.Forms.Button();
            this.btnEdit_HeaterModuleSetting = new System.Windows.Forms.Button();
            this.btnAdd_HeaterModuleSetting = new System.Windows.Forms.Button();
            this.btnDelete_HeaterModuleSetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_HeaterModulePath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvControllerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvControllerType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvComPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCOMBaudRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCOMBits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCOMStationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCOMTimeout = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCOMHandshake = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvCOMStopBits = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvCOMParity = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvEnable_Do = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvOverLimit_DI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvEnumShiftOffsetPmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetHeaterModule)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSetHeaterModule
            // 
            this.dgvSetHeaterModule.AllowUserToAddRows = false;
            this.dgvSetHeaterModule.AllowUserToDeleteRows = false;
            this.dgvSetHeaterModule.AllowUserToResizeColumns = false;
            this.dgvSetHeaterModule.AllowUserToResizeRows = false;
            this.dgvSetHeaterModule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSetHeaterModule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetHeaterModule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvNo,
            this.dgvControllerName,
            this.dgvControllerType,
            this.dgvComPort,
            this.dgvCOMBaudRate,
            this.dgvCOMBits,
            this.dgvCOMStationID,
            this.dgvCOMTimeout,
            this.dgvCOMHandshake,
            this.dgvCOMStopBits,
            this.dgvCOMParity,
            this.dgvEnable_Do,
            this.dgvOverLimit_DI,
            this.dgvEnumShiftOffsetPmt});
            this.dgvSetHeaterModule.Location = new System.Drawing.Point(8, 61);
            this.dgvSetHeaterModule.MultiSelect = false;
            this.dgvSetHeaterModule.Name = "dgvSetHeaterModule";
            this.dgvSetHeaterModule.RowHeadersVisible = false;
            this.dgvSetHeaterModule.RowTemplate.Height = 24;
            this.dgvSetHeaterModule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSetHeaterModule.Size = new System.Drawing.Size(1173, 508);
            this.dgvSetHeaterModule.TabIndex = 10;
            this.dgvSetHeaterModule.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSetHeaterModule_CellClick);
            this.dgvSetHeaterModule.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            this.dgvSetHeaterModule.EnabledChanged += new System.EventHandler(this.dgvSetHeaterModule_EnabledChanged);
            // 
            // btnSave_HeaterModuleSetting
            // 
            this.btnSave_HeaterModuleSetting.Location = new System.Drawing.Point(3, 3);
            this.btnSave_HeaterModuleSetting.Name = "btnSave_HeaterModuleSetting";
            this.btnSave_HeaterModuleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnSave_HeaterModuleSetting.TabIndex = 17;
            this.btnSave_HeaterModuleSetting.Text = "Save";
            this.btnSave_HeaterModuleSetting.UseVisualStyleBackColor = true;
            this.btnSave_HeaterModuleSetting.Click += new System.EventHandler(this.btnSave_HeaterModuleSetting_Click);
            // 
            // btnCancel_HeaterModuleSetting
            // 
            this.btnCancel_HeaterModuleSetting.Location = new System.Drawing.Point(109, 3);
            this.btnCancel_HeaterModuleSetting.Name = "btnCancel_HeaterModuleSetting";
            this.btnCancel_HeaterModuleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_HeaterModuleSetting.TabIndex = 16;
            this.btnCancel_HeaterModuleSetting.Text = "Cancel";
            this.btnCancel_HeaterModuleSetting.UseVisualStyleBackColor = true;
            this.btnCancel_HeaterModuleSetting.Click += new System.EventHandler(this.btnCancel_HeaterModuleSetting_Click);
            // 
            // btnEdit_HeaterModuleSetting
            // 
            this.btnEdit_HeaterModuleSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_HeaterModuleSetting.Location = new System.Drawing.Point(1081, 3);
            this.btnEdit_HeaterModuleSetting.Name = "btnEdit_HeaterModuleSetting";
            this.btnEdit_HeaterModuleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_HeaterModuleSetting.TabIndex = 18;
            this.btnEdit_HeaterModuleSetting.Text = "Edit";
            this.btnEdit_HeaterModuleSetting.UseVisualStyleBackColor = true;
            this.btnEdit_HeaterModuleSetting.Click += new System.EventHandler(this.btnEdit_HeaterModuleSetting_Click);
            // 
            // btnAdd_HeaterModuleSetting
            // 
            this.btnAdd_HeaterModuleSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd_HeaterModuleSetting.Location = new System.Drawing.Point(869, 3);
            this.btnAdd_HeaterModuleSetting.Name = "btnAdd_HeaterModuleSetting";
            this.btnAdd_HeaterModuleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnAdd_HeaterModuleSetting.TabIndex = 15;
            this.btnAdd_HeaterModuleSetting.Text = "Add";
            this.btnAdd_HeaterModuleSetting.UseVisualStyleBackColor = true;
            this.btnAdd_HeaterModuleSetting.Click += new System.EventHandler(this.btnAdd_HeaterModuleSetting_Click);
            // 
            // btnDelete_HeaterModuleSetting
            // 
            this.btnDelete_HeaterModuleSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete_HeaterModuleSetting.Location = new System.Drawing.Point(975, 3);
            this.btnDelete_HeaterModuleSetting.Name = "btnDelete_HeaterModuleSetting";
            this.btnDelete_HeaterModuleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnDelete_HeaterModuleSetting.TabIndex = 14;
            this.btnDelete_HeaterModuleSetting.Text = "Delete";
            this.btnDelete_HeaterModuleSetting.UseVisualStyleBackColor = true;
            this.btnDelete_HeaterModuleSetting.Click += new System.EventHandler(this.btnDelete_HeaterModuleSetting_Click);
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
            // txt_HeaterModulePath
            // 
            this.txt_HeaterModulePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_HeaterModulePath.Location = new System.Drawing.Point(215, 32);
            this.txt_HeaterModulePath.Name = "txt_HeaterModulePath";
            this.txt_HeaterModulePath.Size = new System.Drawing.Size(648, 23);
            this.txt_HeaterModulePath.TabIndex = 19;
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
            // dgvCOMStationID
            // 
            this.dgvCOMStationID.HeaderText = "COM StationID";
            this.dgvCOMStationID.Name = "dgvCOMStationID";
            this.dgvCOMStationID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCOMStationID.Width = 80;
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
            // dgvEnable_Do
            // 
            this.dgvEnable_Do.HeaderText = "Enable DO";
            this.dgvEnable_Do.Name = "dgvEnable_Do";
            this.dgvEnable_Do.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvEnable_Do.Width = 150;
            // 
            // dgvOverLimit_DI
            // 
            this.dgvOverLimit_DI.HeaderText = "Over Limit DI";
            this.dgvOverLimit_DI.Name = "dgvOverLimit_DI";
            this.dgvOverLimit_DI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvOverLimit_DI.Width = 150;
            // 
            // dgvEnumShiftOffsetPmt
            // 
            this.dgvEnumShiftOffsetPmt.HeaderText = "Shift Offset (Enum)";
            this.dgvEnumShiftOffsetPmt.Name = "dgvEnumShiftOffsetPmt";
            this.dgvEnumShiftOffsetPmt.ReadOnly = true;
            this.dgvEnumShiftOffsetPmt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvEnumShiftOffsetPmt.Width = 150;
            // 
            // ucHeaterModuleSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_HeaterModulePath);
            this.Controls.Add(this.btnSave_HeaterModuleSetting);
            this.Controls.Add(this.btnCancel_HeaterModuleSetting);
            this.Controls.Add(this.btnEdit_HeaterModuleSetting);
            this.Controls.Add(this.btnAdd_HeaterModuleSetting);
            this.Controls.Add(this.btnDelete_HeaterModuleSetting);
            this.Controls.Add(this.dgvSetHeaterModule);
            this.Name = "ucHeaterModuleSetting";
            this.Size = new System.Drawing.Size(1184, 572);
            this.VisibleChanged += new System.EventHandler(this.ucHeaterModuleSetting_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetHeaterModule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSetHeaterModule;
        private System.Windows.Forms.Button btnSave_HeaterModuleSetting;
        private System.Windows.Forms.Button btnCancel_HeaterModuleSetting;
        private System.Windows.Forms.Button btnEdit_HeaterModuleSetting;
        private System.Windows.Forms.Button btnAdd_HeaterModuleSetting;
        private System.Windows.Forms.Button btnDelete_HeaterModuleSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_HeaterModulePath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvControllerName;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvControllerType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvComPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCOMBaudRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCOMBits;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCOMStationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCOMTimeout;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvCOMHandshake;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvCOMStopBits;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvCOMParity;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEnable_Do;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvOverLimit_DI;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEnumShiftOffsetPmt;



    }
}

namespace ArtSystem
{
    partial class ucHardwareHotkeyButton
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
            this.panel_Hotkey = new System.Windows.Forms.Panel();
            this.dataGridView_Hotkey = new System.Windows.Forms.DataGridView();
            this.dgvEqStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvGreenLight = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvRedLight = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvBlueLight = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvFlashSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_Hotkey_ResetDO = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_Hotkey_ResetDI = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_Hotkey_StopDO = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_Hotkey_StopDI = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_Hotkey_RunDO = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucMachineButton_Reset = new ArtSystem.ucMachineButton();
            this.ucMachineButton_Stop = new ArtSystem.ucMachineButton();
            this.ucMachineButton_Run = new ArtSystem.ucMachineButton();
            this.txt_Hotkey_RunDI = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnSave_Hotkey = new System.Windows.Forms.Button();
            this.btnEdit_Hotkey = new System.Windows.Forms.Button();
            this.btnCancel_Hotkey = new System.Windows.Forms.Button();
            this.panel_Hotkey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Hotkey)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Hotkey
            // 
            this.panel_Hotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Hotkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Hotkey.Controls.Add(this.dataGridView_Hotkey);
            this.panel_Hotkey.Controls.Add(this.label18);
            this.panel_Hotkey.Controls.Add(this.txt_Hotkey_ResetDO);
            this.panel_Hotkey.Controls.Add(this.label16);
            this.panel_Hotkey.Controls.Add(this.txt_Hotkey_ResetDI);
            this.panel_Hotkey.Controls.Add(this.label17);
            this.panel_Hotkey.Controls.Add(this.txt_Hotkey_StopDO);
            this.panel_Hotkey.Controls.Add(this.label12);
            this.panel_Hotkey.Controls.Add(this.txt_Hotkey_StopDI);
            this.panel_Hotkey.Controls.Add(this.label15);
            this.panel_Hotkey.Controls.Add(this.txt_Hotkey_RunDO);
            this.panel_Hotkey.Controls.Add(this.label14);
            this.panel_Hotkey.Controls.Add(this.panel1);
            this.panel_Hotkey.Controls.Add(this.txt_Hotkey_RunDI);
            this.panel_Hotkey.Controls.Add(this.label13);
            this.panel_Hotkey.Location = new System.Drawing.Point(0, 61);
            this.panel_Hotkey.Name = "panel_Hotkey";
            this.panel_Hotkey.Size = new System.Drawing.Size(1131, 677);
            this.panel_Hotkey.TabIndex = 26;
            // 
            // dataGridView_Hotkey
            // 
            this.dataGridView_Hotkey.AllowUserToAddRows = false;
            this.dataGridView_Hotkey.AllowUserToDeleteRows = false;
            this.dataGridView_Hotkey.AllowUserToResizeColumns = false;
            this.dataGridView_Hotkey.AllowUserToResizeRows = false;
            this.dataGridView_Hotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Hotkey.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Hotkey.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvEqStatus,
            this.dgvDescription,
            this.dgvGreenLight,
            this.dgvRedLight,
            this.dgvBlueLight,
            this.dgvFlashSpeed});
            this.dataGridView_Hotkey.Location = new System.Drawing.Point(449, 3);
            this.dataGridView_Hotkey.Name = "dataGridView_Hotkey";
            this.dataGridView_Hotkey.RowHeadersVisible = false;
            this.dataGridView_Hotkey.RowTemplate.Height = 24;
            this.dataGridView_Hotkey.Size = new System.Drawing.Size(675, 666);
            this.dataGridView_Hotkey.TabIndex = 31;
            this.dataGridView_Hotkey.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Hotkey_CellDoubleClick);
            this.dataGridView_Hotkey.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Hotkey_CellEndEdit);
            // 
            // dgvEqStatus
            // 
            this.dgvEqStatus.HeaderText = "Stauts";
            this.dgvEqStatus.Name = "dgvEqStatus";
            this.dgvEqStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvDescription
            // 
            this.dgvDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvDescription.HeaderText = "Description";
            this.dgvDescription.Name = "dgvDescription";
            this.dgvDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvGreenLight
            // 
            this.dgvGreenLight.HeaderText = "Green Light";
            this.dgvGreenLight.Name = "dgvGreenLight";
            // 
            // dgvRedLight
            // 
            this.dgvRedLight.HeaderText = "Red Light";
            this.dgvRedLight.Name = "dgvRedLight";
            // 
            // dgvBlueLight
            // 
            this.dgvBlueLight.HeaderText = "Blue Light";
            this.dgvBlueLight.Name = "dgvBlueLight";
            // 
            // dgvFlashSpeed
            // 
            this.dgvFlashSpeed.HeaderText = "Flash Speed (ms)";
            this.dgvFlashSpeed.Name = "dgvFlashSpeed";
            this.dgvFlashSpeed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(5, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(380, 29);
            this.label18.TabIndex = 30;
            this.label18.Text = "Hardware Control Panel Button";
            // 
            // txt_Hotkey_ResetDO
            // 
            this.txt_Hotkey_ResetDO.BackColor = System.Drawing.Color.White;
            this.txt_Hotkey_ResetDO.Location = new System.Drawing.Point(132, 382);
            this.txt_Hotkey_ResetDO.Name = "txt_Hotkey_ResetDO";
            this.txt_Hotkey_ResetDO.ReadOnly = true;
            this.txt_Hotkey_ResetDO.Size = new System.Drawing.Size(311, 23);
            this.txt_Hotkey_ResetDO.TabIndex = 28;
            this.txt_Hotkey_ResetDO.DoubleClick += new System.EventHandler(this.txt_Hotkey_DI_DoubleClick);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label16.Location = new System.Drawing.Point(129, 363);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(145, 16);
            this.label16.TabIndex = 29;
            this.label16.Text = "RESET LED DO (ID) :";
            // 
            // txt_Hotkey_ResetDI
            // 
            this.txt_Hotkey_ResetDI.BackColor = System.Drawing.Color.White;
            this.txt_Hotkey_ResetDI.Location = new System.Drawing.Point(132, 332);
            this.txt_Hotkey_ResetDI.Name = "txt_Hotkey_ResetDI";
            this.txt_Hotkey_ResetDI.ReadOnly = true;
            this.txt_Hotkey_ResetDI.Size = new System.Drawing.Size(311, 23);
            this.txt_Hotkey_ResetDI.TabIndex = 26;
            this.txt_Hotkey_ResetDI.DoubleClick += new System.EventHandler(this.txt_Hotkey_DI_DoubleClick);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label17.Location = new System.Drawing.Point(129, 313);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(160, 16);
            this.label17.TabIndex = 27;
            this.label17.Text = "RESET Button DI (ID) :";
            // 
            // txt_Hotkey_StopDO
            // 
            this.txt_Hotkey_StopDO.BackColor = System.Drawing.Color.White;
            this.txt_Hotkey_StopDO.Location = new System.Drawing.Point(132, 269);
            this.txt_Hotkey_StopDO.Name = "txt_Hotkey_StopDO";
            this.txt_Hotkey_StopDO.ReadOnly = true;
            this.txt_Hotkey_StopDO.Size = new System.Drawing.Size(311, 23);
            this.txt_Hotkey_StopDO.TabIndex = 24;
            this.txt_Hotkey_StopDO.DoubleClick += new System.EventHandler(this.txt_Hotkey_DI_DoubleClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label12.Location = new System.Drawing.Point(129, 250);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(139, 16);
            this.label12.TabIndex = 25;
            this.label12.Text = "STOP LED DO (ID) :";
            // 
            // txt_Hotkey_StopDI
            // 
            this.txt_Hotkey_StopDI.BackColor = System.Drawing.Color.White;
            this.txt_Hotkey_StopDI.Location = new System.Drawing.Point(132, 219);
            this.txt_Hotkey_StopDI.Name = "txt_Hotkey_StopDI";
            this.txt_Hotkey_StopDI.ReadOnly = true;
            this.txt_Hotkey_StopDI.Size = new System.Drawing.Size(311, 23);
            this.txt_Hotkey_StopDI.TabIndex = 22;
            this.txt_Hotkey_StopDI.DoubleClick += new System.EventHandler(this.txt_Hotkey_DI_DoubleClick);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label15.Location = new System.Drawing.Point(129, 200);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(154, 16);
            this.label15.TabIndex = 23;
            this.label15.Text = "STOP Button DI (ID) :";
            // 
            // txt_Hotkey_RunDO
            // 
            this.txt_Hotkey_RunDO.BackColor = System.Drawing.Color.White;
            this.txt_Hotkey_RunDO.Location = new System.Drawing.Point(132, 158);
            this.txt_Hotkey_RunDO.Name = "txt_Hotkey_RunDO";
            this.txt_Hotkey_RunDO.ReadOnly = true;
            this.txt_Hotkey_RunDO.Size = new System.Drawing.Size(311, 23);
            this.txt_Hotkey_RunDO.TabIndex = 20;
            this.txt_Hotkey_RunDO.DoubleClick += new System.EventHandler(this.txt_Hotkey_DI_DoubleClick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label14.Location = new System.Drawing.Point(129, 139);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(129, 16);
            this.label14.TabIndex = 21;
            this.label14.Text = "RUN LED DO (ID) :";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ucMachineButton_Reset);
            this.panel1.Controls.Add(this.ucMachineButton_Stop);
            this.panel1.Controls.Add(this.ucMachineButton_Run);
            this.panel1.Location = new System.Drawing.Point(6, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 350);
            this.panel1.TabIndex = 1;
            // 
            // ucMachineButton_Reset
            // 
            this.ucMachineButton_Reset.BackColor = System.Drawing.Color.Transparent;
            this.ucMachineButton_Reset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucMachineButton_Reset.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMachineButton_Reset.Location = new System.Drawing.Point(11, 235);
            this.ucMachineButton_Reset.Margin = new System.Windows.Forms.Padding(0);
            this.ucMachineButton_Reset.Name = "ucMachineButton_Reset";
            this.ucMachineButton_Reset.Size = new System.Drawing.Size(100, 100);
            this.ucMachineButton_Reset.TabIndex = 2;
            // 
            // ucMachineButton_Stop
            // 
            this.ucMachineButton_Stop.BackColor = System.Drawing.Color.Transparent;
            this.ucMachineButton_Stop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucMachineButton_Stop.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMachineButton_Stop.Location = new System.Drawing.Point(11, 122);
            this.ucMachineButton_Stop.Margin = new System.Windows.Forms.Padding(0);
            this.ucMachineButton_Stop.Name = "ucMachineButton_Stop";
            this.ucMachineButton_Stop.Size = new System.Drawing.Size(100, 100);
            this.ucMachineButton_Stop.TabIndex = 1;
            // 
            // ucMachineButton_Run
            // 
            this.ucMachineButton_Run.BackColor = System.Drawing.Color.Transparent;
            this.ucMachineButton_Run.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucMachineButton_Run.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucMachineButton_Run.Location = new System.Drawing.Point(11, 11);
            this.ucMachineButton_Run.Margin = new System.Windows.Forms.Padding(0);
            this.ucMachineButton_Run.Name = "ucMachineButton_Run";
            this.ucMachineButton_Run.Size = new System.Drawing.Size(100, 100);
            this.ucMachineButton_Run.TabIndex = 0;
            // 
            // txt_Hotkey_RunDI
            // 
            this.txt_Hotkey_RunDI.BackColor = System.Drawing.Color.White;
            this.txt_Hotkey_RunDI.Location = new System.Drawing.Point(132, 108);
            this.txt_Hotkey_RunDI.Name = "txt_Hotkey_RunDI";
            this.txt_Hotkey_RunDI.ReadOnly = true;
            this.txt_Hotkey_RunDI.Size = new System.Drawing.Size(311, 23);
            this.txt_Hotkey_RunDI.TabIndex = 18;
            this.txt_Hotkey_RunDI.DoubleClick += new System.EventHandler(this.txt_Hotkey_DI_DoubleClick);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label13.Location = new System.Drawing.Point(129, 89);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(142, 16);
            this.label13.TabIndex = 19;
            this.label13.Text = "Run Button DI (ID) :";
            // 
            // btnSave_Hotkey
            // 
            this.btnSave_Hotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave_Hotkey.Location = new System.Drawing.Point(816, 3);
            this.btnSave_Hotkey.Name = "btnSave_Hotkey";
            this.btnSave_Hotkey.Size = new System.Drawing.Size(100, 52);
            this.btnSave_Hotkey.TabIndex = 25;
            this.btnSave_Hotkey.Text = "Save";
            this.btnSave_Hotkey.UseVisualStyleBackColor = true;
            this.btnSave_Hotkey.Click += new System.EventHandler(this.btnSave_Hotkey_Click);
            // 
            // btnEdit_Hotkey
            // 
            this.btnEdit_Hotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_Hotkey.Location = new System.Drawing.Point(1028, 3);
            this.btnEdit_Hotkey.Name = "btnEdit_Hotkey";
            this.btnEdit_Hotkey.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_Hotkey.TabIndex = 23;
            this.btnEdit_Hotkey.Text = "Edit";
            this.btnEdit_Hotkey.UseVisualStyleBackColor = true;
            this.btnEdit_Hotkey.Click += new System.EventHandler(this.btnEdit_Hotkey_Click);
            // 
            // btnCancel_Hotkey
            // 
            this.btnCancel_Hotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel_Hotkey.Location = new System.Drawing.Point(922, 3);
            this.btnCancel_Hotkey.Name = "btnCancel_Hotkey";
            this.btnCancel_Hotkey.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_Hotkey.TabIndex = 24;
            this.btnCancel_Hotkey.Text = "Cancel";
            this.btnCancel_Hotkey.UseVisualStyleBackColor = true;
            this.btnCancel_Hotkey.Click += new System.EventHandler(this.btnCancel_Hotkey_Click);
            // 
            // ucHardwareHotkeyButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_Hotkey);
            this.Controls.Add(this.btnSave_Hotkey);
            this.Controls.Add(this.btnEdit_Hotkey);
            this.Controls.Add(this.btnCancel_Hotkey);
            this.Name = "ucHardwareHotkeyButton";
            this.Size = new System.Drawing.Size(1131, 741);
            this.panel_Hotkey.ResumeLayout(false);
            this.panel_Hotkey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Hotkey)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave_Hotkey;
        private System.Windows.Forms.Button btnEdit_Hotkey;
        private System.Windows.Forms.Button btnCancel_Hotkey;
        private System.Windows.Forms.Panel panel_Hotkey;
        private System.Windows.Forms.Panel panel1;
        private ucMachineButton ucMachineButton_Reset;
        private ucMachineButton ucMachineButton_Stop;
        private ucMachineButton ucMachineButton_Run;
        private System.Windows.Forms.TextBox txt_Hotkey_ResetDO;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_Hotkey_ResetDI;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_Hotkey_StopDO;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_Hotkey_StopDI;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_Hotkey_RunDO;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_Hotkey_RunDI;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataGridView dataGridView_Hotkey;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEqStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDescription;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvGreenLight;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvRedLight;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvBlueLight;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFlashSpeed;
    }
}

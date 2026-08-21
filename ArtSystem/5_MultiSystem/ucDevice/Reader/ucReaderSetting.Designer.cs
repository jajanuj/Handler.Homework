namespace ArtSystem.MultiSystem
{
    partial class ucReaderSetting
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
            this.dgvSetReader = new System.Windows.Forms.DataGridView();
            this.btnSave_ReaderSetting = new System.Windows.Forms.Button();
            this.btnCancel_ReaderSetting = new System.Windows.Forms.Button();
            this.btnEdit_ReaderSetting = new System.Windows.Forms.Button();
            this.btnAdd_ReaderSetting = new System.Windows.Forms.Button();
            this.btnDelete_ReaderSetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_ReaderPath = new System.Windows.Forms.TextBox();
            this.dgvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvReaderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvReaderType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvTCPIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTCPPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvChannelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetReader)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSetReader
            // 
            this.dgvSetReader.AllowUserToAddRows = false;
            this.dgvSetReader.AllowUserToDeleteRows = false;
            this.dgvSetReader.AllowUserToResizeColumns = false;
            this.dgvSetReader.AllowUserToResizeRows = false;
            this.dgvSetReader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSetReader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetReader.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvNo,
            this.dgvReaderName,
            this.dgvReaderType,
            this.dgvTCPIP,
            this.dgvTCPPort,
            this.dgvChannelID});
            this.dgvSetReader.Location = new System.Drawing.Point(8, 61);
            this.dgvSetReader.MultiSelect = false;
            this.dgvSetReader.Name = "dgvSetReader";
            this.dgvSetReader.RowHeadersVisible = false;
            this.dgvSetReader.RowTemplate.Height = 24;
            this.dgvSetReader.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSetReader.Size = new System.Drawing.Size(1173, 508);
            this.dgvSetReader.TabIndex = 10;
            this.dgvSetReader.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            this.dgvSetReader.EnabledChanged += new System.EventHandler(this.dgvSetReader_EnabledChanged);
            // 
            // btnSave_ReaderSetting
            // 
            this.btnSave_ReaderSetting.Location = new System.Drawing.Point(3, 3);
            this.btnSave_ReaderSetting.Name = "btnSave_ReaderSetting";
            this.btnSave_ReaderSetting.Size = new System.Drawing.Size(100, 52);
            this.btnSave_ReaderSetting.TabIndex = 17;
            this.btnSave_ReaderSetting.Text = "Save";
            this.btnSave_ReaderSetting.UseVisualStyleBackColor = true;
            this.btnSave_ReaderSetting.Click += new System.EventHandler(this.btnSave_ReaderSetting_Click);
            // 
            // btnCancel_ReaderSetting
            // 
            this.btnCancel_ReaderSetting.Location = new System.Drawing.Point(109, 3);
            this.btnCancel_ReaderSetting.Name = "btnCancel_ReaderSetting";
            this.btnCancel_ReaderSetting.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_ReaderSetting.TabIndex = 16;
            this.btnCancel_ReaderSetting.Text = "Cancel";
            this.btnCancel_ReaderSetting.UseVisualStyleBackColor = true;
            this.btnCancel_ReaderSetting.Click += new System.EventHandler(this.btnCancel_ReaderSetting_Click);
            // 
            // btnEdit_ReaderSetting
            // 
            this.btnEdit_ReaderSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_ReaderSetting.Location = new System.Drawing.Point(1081, 3);
            this.btnEdit_ReaderSetting.Name = "btnEdit_ReaderSetting";
            this.btnEdit_ReaderSetting.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_ReaderSetting.TabIndex = 18;
            this.btnEdit_ReaderSetting.Text = "Edit";
            this.btnEdit_ReaderSetting.UseVisualStyleBackColor = true;
            this.btnEdit_ReaderSetting.Click += new System.EventHandler(this.btnEdit_ReaderSetting_Click);
            // 
            // btnAdd_ReaderSetting
            // 
            this.btnAdd_ReaderSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd_ReaderSetting.Location = new System.Drawing.Point(869, 3);
            this.btnAdd_ReaderSetting.Name = "btnAdd_ReaderSetting";
            this.btnAdd_ReaderSetting.Size = new System.Drawing.Size(100, 52);
            this.btnAdd_ReaderSetting.TabIndex = 15;
            this.btnAdd_ReaderSetting.Text = "Add";
            this.btnAdd_ReaderSetting.UseVisualStyleBackColor = true;
            this.btnAdd_ReaderSetting.Click += new System.EventHandler(this.btnAdd_ReaderSetting_Click);
            // 
            // btnDelete_ReaderSetting
            // 
            this.btnDelete_ReaderSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete_ReaderSetting.Location = new System.Drawing.Point(975, 3);
            this.btnDelete_ReaderSetting.Name = "btnDelete_ReaderSetting";
            this.btnDelete_ReaderSetting.Size = new System.Drawing.Size(100, 52);
            this.btnDelete_ReaderSetting.TabIndex = 14;
            this.btnDelete_ReaderSetting.Text = "Delete";
            this.btnDelete_ReaderSetting.UseVisualStyleBackColor = true;
            this.btnDelete_ReaderSetting.Click += new System.EventHandler(this.btnDelete_ReaderSetting_Click);
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
            // txt_ReaderPath
            // 
            this.txt_ReaderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_ReaderPath.Location = new System.Drawing.Point(215, 32);
            this.txt_ReaderPath.Name = "txt_ReaderPath";
            this.txt_ReaderPath.Size = new System.Drawing.Size(648, 23);
            this.txt_ReaderPath.TabIndex = 19;
            // 
            // dgvNo
            // 
            this.dgvNo.HeaderText = "No.";
            this.dgvNo.Name = "dgvNo";
            this.dgvNo.ReadOnly = true;
            this.dgvNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvNo.Width = 40;
            // 
            // dgvReaderName
            // 
            this.dgvReaderName.HeaderText = "Reader Name";
            this.dgvReaderName.Name = "dgvReaderName";
            this.dgvReaderName.ReadOnly = true;
            this.dgvReaderName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvReaderName.Width = 150;
            // 
            // dgvReaderType
            // 
            this.dgvReaderType.HeaderText = "Reader Type";
            this.dgvReaderType.Name = "dgvReaderType";
            this.dgvReaderType.Width = 200;
            // 
            // dgvTCPIP
            // 
            this.dgvTCPIP.HeaderText = "TCP IP / Port Name";
            this.dgvTCPIP.Name = "dgvTCPIP";
            this.dgvTCPIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvTCPIP.Width = 150;
            // 
            // dgvTCPPort
            // 
            this.dgvTCPPort.HeaderText = "TCP Port / Baud Rate";
            this.dgvTCPPort.Name = "dgvTCPPort";
            this.dgvTCPPort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvTCPPort.Width = 180;
            // 
            // dgvChannelID
            // 
            this.dgvChannelID.HeaderText = "Channel ID";
            this.dgvChannelID.Name = "dgvChannelID";
            this.dgvChannelID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvChannelID.Width = 120;
            // 
            // ucReaderSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_ReaderPath);
            this.Controls.Add(this.btnSave_ReaderSetting);
            this.Controls.Add(this.btnCancel_ReaderSetting);
            this.Controls.Add(this.btnEdit_ReaderSetting);
            this.Controls.Add(this.btnAdd_ReaderSetting);
            this.Controls.Add(this.btnDelete_ReaderSetting);
            this.Controls.Add(this.dgvSetReader);
            this.Name = "ucReaderSetting";
            this.Size = new System.Drawing.Size(1184, 572);
            this.VisibleChanged += new System.EventHandler(this.ucReaderSetting_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetReader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSetReader;
        private System.Windows.Forms.Button btnSave_ReaderSetting;
        private System.Windows.Forms.Button btnCancel_ReaderSetting;
        private System.Windows.Forms.Button btnEdit_ReaderSetting;
        private System.Windows.Forms.Button btnAdd_ReaderSetting;
        private System.Windows.Forms.Button btnDelete_ReaderSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_ReaderPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvReaderName;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvReaderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTCPIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTCPPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvChannelID;
    }
}

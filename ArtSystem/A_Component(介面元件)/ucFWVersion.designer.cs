namespace ArtSystem
{
    partial class ucFWVersion
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgvDeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDeviceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFWVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHWVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvExtralInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvDeviceName,
            this.dgvDeviceType,
            this.dgvFWVersion,
            this.dgvHWVersion,
            this.dgvExtralInfo,
            this.dgvFileName});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(996, 499);
            this.dataGridView1.TabIndex = 0;
            // 
            // dgvDeviceName
            // 
            this.dgvDeviceName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvDeviceName.HeaderText = "Device Name";
            this.dgvDeviceName.Name = "dgvDeviceName";
            // 
            // dgvDeviceType
            // 
            this.dgvDeviceType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvDeviceType.HeaderText = "Device Type";
            this.dgvDeviceType.Name = "dgvDeviceType";
            this.dgvDeviceType.ReadOnly = true;
            // 
            // dgvFWVersion
            // 
            this.dgvFWVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvFWVersion.HeaderText = "FW Ver";
            this.dgvFWVersion.Name = "dgvFWVersion";
            this.dgvFWVersion.ReadOnly = true;
            // 
            // dgvHWVersion
            // 
            this.dgvHWVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvHWVersion.HeaderText = "HW Ver";
            this.dgvHWVersion.Name = "dgvHWVersion";
            // 
            // dgvExtralInfo
            // 
            this.dgvExtralInfo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvExtralInfo.HeaderText = "Other Info";
            this.dgvExtralInfo.Name = "dgvExtralInfo";
            // 
            // dgvFileName
            // 
            this.dgvFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvFileName.HeaderText = "File Name";
            this.dgvFileName.Name = "dgvFileName";
            this.dgvFileName.ReadOnly = true;
            // 
            // ucFWVersion
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.dataGridView1);
            this.Name = "ucFWVersion";
            this.Size = new System.Drawing.Size(996, 499);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDeviceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDeviceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFWVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHWVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvExtralInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFileName;
    }
}

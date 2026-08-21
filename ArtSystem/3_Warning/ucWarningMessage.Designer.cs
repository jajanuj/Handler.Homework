namespace ArtSystem
{
    partial class ucWarnningMessage
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.alarmBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewCurrentAlarm = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgv_WarningMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_ReportTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.alarmBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCurrentAlarm)).BeginInit();
            this.SuspendLayout();
            // 
            // alarmBindingSource
            // 
            this.alarmBindingSource.DataMember = "Alarm";
            // 
            // dataGridViewCurrentAlarm
            // 
            this.dataGridViewCurrentAlarm.AllowUserToAddRows = false;
            this.dataGridViewCurrentAlarm.AllowUserToResizeColumns = false;
            this.dataGridViewCurrentAlarm.AllowUserToResizeRows = false;
            this.dataGridViewCurrentAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCurrentAlarm.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewCurrentAlarm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCurrentAlarm.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCurrentAlarm.ColumnHeadersHeight = 40;
            this.dataGridViewCurrentAlarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewCurrentAlarm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_WarningMessage,
            this.dgv_ReportTime});
            this.dataGridViewCurrentAlarm.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewCurrentAlarm.MultiSelect = false;
            this.dataGridViewCurrentAlarm.Name = "dataGridViewCurrentAlarm";
            this.dataGridViewCurrentAlarm.ReadOnly = true;
            this.dataGridViewCurrentAlarm.RowHeadersVisible = false;
            this.dataGridViewCurrentAlarm.RowTemplate.Height = 30;
            this.dataGridViewCurrentAlarm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCurrentAlarm.Size = new System.Drawing.Size(856, 381);
            this.dataGridViewCurrentAlarm.TabIndex = 50;
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            // 
            // dgv_WarningMessage
            // 
            this.dgv_WarningMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_WarningMessage.HeaderText = "Warning Message";
            this.dgv_WarningMessage.Name = "dgv_WarningMessage";
            this.dgv_WarningMessage.ReadOnly = true;
            // 
            // dgv_ReportTime
            // 
            this.dgv_ReportTime.HeaderText = "Report Time";
            this.dgv_ReportTime.Name = "dgv_ReportTime";
            this.dgv_ReportTime.ReadOnly = true;
            this.dgv_ReportTime.Width = 230;
            // 
            // ucWarnningMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.dataGridViewCurrentAlarm);
            this.Name = "ucWarnningMessage";
            this.Size = new System.Drawing.Size(859, 384);
            ((System.ComponentModel.ISupportInitialize)(this.alarmBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCurrentAlarm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource alarmBindingSource;
        private System.Windows.Forms.DataGridView dataGridViewCurrentAlarm;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_WarningMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ReportTime;


    }
}

namespace ArtEQ
{
    partial class ucAlarmCounter
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
            if (disposing && (components != null) ) 
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
            this.ListAlarmCounterFiles = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtLastOccurAlarm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Copy1DayLog = new System.Windows.Forms.Button();
            this.txt_CopyLogTargetPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_SelectDate = new System.Windows.Forms.TextBox();
            this.monthCalendarToday = new System.Windows.Forms.MonthCalendar();
            this.AlarmCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlarmMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlarmCounter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StopTimeLast = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StopTimeAVG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ListAlarmCounterFiles
            // 
            this.ListAlarmCounterFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListAlarmCounterFiles.FormattingEnabled = true;
            this.ListAlarmCounterFiles.Location = new System.Drawing.Point(6, 19);
            this.ListAlarmCounterFiles.Name = "ListAlarmCounterFiles";
            this.ListAlarmCounterFiles.Size = new System.Drawing.Size(666, 24);
            this.ListAlarmCounterFiles.TabIndex = 347;
            this.ListAlarmCounterFiles.SelectedIndexChanged += new System.EventHandler(this.ListAlarmCounterFiles_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1011, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 24);
            this.button1.TabIndex = 348;
            this.button1.Text = "Test Alarm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(905, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 349;
            this.textBox1.Text = "Alarm1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AlarmCode,
            this.AlarmMessage,
            this.AlarmCounter,
            this.StopTimeLast,
            this.StopTimeAVG});
            this.dataGridView1.Location = new System.Drawing.Point(6, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1095, 562);
            this.dataGridView1.TabIndex = 350;
            // 
            // txtLastOccurAlarm
            // 
            this.txtLastOccurAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLastOccurAlarm.Location = new System.Drawing.Point(690, 19);
            this.txtLastOccurAlarm.Name = "txtLastOccurAlarm";
            this.txtLastOccurAlarm.Size = new System.Drawing.Size(196, 23);
            this.txtLastOccurAlarm.TabIndex = 351;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(687, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 16);
            this.label2.TabIndex = 352;
            this.label2.Text = "Last Occur Alarm :";
            // 
            // btn_Copy1DayLog
            // 
            this.btn_Copy1DayLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Copy1DayLog.Location = new System.Drawing.Point(896, 617);
            this.btn_Copy1DayLog.Name = "btn_Copy1DayLog";
            this.btn_Copy1DayLog.Size = new System.Drawing.Size(205, 24);
            this.btn_Copy1DayLog.TabIndex = 353;
            this.btn_Copy1DayLog.Text = "Copy One Day Log";
            this.btn_Copy1DayLog.UseVisualStyleBackColor = true;
            this.btn_Copy1DayLog.Click += new System.EventHandler(this.btn_Copy1DayLog_Click);
            // 
            // txt_CopyLogTargetPath
            // 
            this.txt_CopyLogTargetPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_CopyLogTargetPath.BackColor = System.Drawing.Color.White;
            this.txt_CopyLogTargetPath.Location = new System.Drawing.Point(173, 617);
            this.txt_CopyLogTargetPath.Name = "txt_CopyLogTargetPath";
            this.txt_CopyLogTargetPath.ReadOnly = true;
            this.txt_CopyLogTargetPath.Size = new System.Drawing.Size(507, 23);
            this.txt_CopyLogTargetPath.TabIndex = 354;
            this.txt_CopyLogTargetPath.Click += new System.EventHandler(this.Path_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 621);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 16);
            this.label1.TabIndex = 355;
            this.label1.Text = "Copy Log Target Path :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(686, 621);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 356;
            this.label3.Text = "Select Date :";
            // 
            // txt_SelectDate
            // 
            this.txt_SelectDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_SelectDate.BackColor = System.Drawing.Color.White;
            this.txt_SelectDate.Location = new System.Drawing.Point(789, 618);
            this.txt_SelectDate.Name = "txt_SelectDate";
            this.txt_SelectDate.ReadOnly = true;
            this.txt_SelectDate.Size = new System.Drawing.Size(101, 23);
            this.txt_SelectDate.TabIndex = 357;
            this.txt_SelectDate.Click += new System.EventHandler(this.txt_SelectDate_Click);
            // 
            // monthCalendarToday
            // 
            this.monthCalendarToday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.monthCalendarToday.Location = new System.Drawing.Point(730, 434);
            this.monthCalendarToday.MaxSelectionCount = 1;
            this.monthCalendarToday.Name = "monthCalendarToday";
            this.monthCalendarToday.TabIndex = 358;
            this.monthCalendarToday.Visible = false;
            this.monthCalendarToday.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarToday_DateSelected);
            // 
            // AlarmCode
            // 
            this.AlarmCode.HeaderText = "Alarm Code";
            this.AlarmCode.Name = "AlarmCode";
            this.AlarmCode.ReadOnly = true;
            this.AlarmCode.Width = 120;
            // 
            // AlarmMessage
            // 
            this.AlarmMessage.HeaderText = "Alarm Message";
            this.AlarmMessage.Name = "AlarmMessage";
            this.AlarmMessage.ReadOnly = true;
            this.AlarmMessage.Width = 410;
            // 
            // AlarmCounter
            // 
            this.AlarmCounter.HeaderText = "Counter";
            this.AlarmCounter.Name = "AlarmCounter";
            this.AlarmCounter.ReadOnly = true;
            // 
            // StopTimeLast
            // 
            this.StopTimeLast.HeaderText = "Stop Time Last (Sec) ";
            this.StopTimeLast.Name = "StopTimeLast";
            this.StopTimeLast.Width = 180;
            // 
            // StopTimeAVG
            // 
            this.StopTimeAVG.HeaderText = "Stop Time Avg (Sec) ";
            this.StopTimeAVG.Name = "StopTimeAVG";
            this.StopTimeAVG.ReadOnly = true;
            this.StopTimeAVG.Width = 180;
            // 
            // ucAlarmCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.monthCalendarToday);
            this.Controls.Add(this.txt_SelectDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_CopyLogTargetPath);
            this.Controls.Add(this.btn_Copy1DayLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLastOccurAlarm);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ListAlarmCounterFiles);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ucAlarmCounter";
            this.Size = new System.Drawing.Size(1112, 647);
            this.VisibleChanged += new System.EventHandler(this.ucAlarmCounter_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ListAlarmCounterFiles;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtLastOccurAlarm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Copy1DayLog;
        private System.Windows.Forms.TextBox txt_CopyLogTargetPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_SelectDate;
        private System.Windows.Forms.MonthCalendar monthCalendarToday;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlarmCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlarmMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlarmCounter;
        private System.Windows.Forms.DataGridViewTextBoxColumn StopTimeLast;
        private System.Windows.Forms.DataGridViewTextBoxColumn StopTimeAVG;



    }
}

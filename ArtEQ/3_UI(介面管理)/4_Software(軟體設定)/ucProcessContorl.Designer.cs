namespace ArtEQ
{
    partial class ucProcessControl
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPage_AlarmCode = new System.Windows.Forms.TabPage();
            this.btn_WriteAllPMAlarmCodeToINI = new System.Windows.Forms.Button();
            this.btn_CheckAll = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.btn_WriteAlarmCodeToINI = new System.Windows.Forms.Button();
            this.btn_ReloadAlarmINIToRichText = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tPage_AlarmCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tPage_AlarmCode);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(888, 553);
            this.tabControl1.TabIndex = 0;
            // 
            // tPage_AlarmCode
            // 
            this.tPage_AlarmCode.Controls.Add(this.btn_WriteAllPMAlarmCodeToINI);
            this.tPage_AlarmCode.Controls.Add(this.btn_CheckAll);
            this.tPage_AlarmCode.Controls.Add(this.radioButton2);
            this.tPage_AlarmCode.Controls.Add(this.radioButton1);
            this.tPage_AlarmCode.Controls.Add(this.checkedListBox1);
            this.tPage_AlarmCode.Controls.Add(this.btn_WriteAlarmCodeToINI);
            this.tPage_AlarmCode.Controls.Add(this.btn_ReloadAlarmINIToRichText);
            this.tPage_AlarmCode.Controls.Add(this.richTextBox1);
            this.tPage_AlarmCode.Controls.Add(this.listBox1);
            this.tPage_AlarmCode.Location = new System.Drawing.Point(4, 25);
            this.tPage_AlarmCode.Name = "tPage_AlarmCode";
            this.tPage_AlarmCode.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_AlarmCode.Size = new System.Drawing.Size(880, 524);
            this.tPage_AlarmCode.TabIndex = 0;
            this.tPage_AlarmCode.Text = "Alarm Code";
            this.tPage_AlarmCode.UseVisualStyleBackColor = true;
            // 
            // btn_WriteAllPMAlarmCodeToINI
            // 
            this.btn_WriteAllPMAlarmCodeToINI.Location = new System.Drawing.Point(6, 476);
            this.btn_WriteAllPMAlarmCodeToINI.Name = "btn_WriteAllPMAlarmCodeToINI";
            this.btn_WriteAllPMAlarmCodeToINI.Size = new System.Drawing.Size(250, 42);
            this.btn_WriteAllPMAlarmCodeToINI.TabIndex = 9;
            this.btn_WriteAllPMAlarmCodeToINI.Text = "Write All PM/AP Alarm Code To INI";
            this.btn_WriteAllPMAlarmCodeToINI.UseVisualStyleBackColor = true;
            this.btn_WriteAllPMAlarmCodeToINI.Click += new System.EventHandler(this.btn_WriteAllPMAlarmCodeToINI_Click);
            // 
            // btn_CheckAll
            // 
            this.btn_CheckAll.Location = new System.Drawing.Point(262, 6);
            this.btn_CheckAll.Name = "btn_CheckAll";
            this.btn_CheckAll.Size = new System.Drawing.Size(84, 23);
            this.btn_CheckAll.TabIndex = 8;
            this.btn_CheckAll.Text = "Check All";
            this.btn_CheckAll.UseVisualStyleBackColor = true;
            this.btn_CheckAll.Click += new System.EventHandler(this.btn_CheckAll_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(131, 8);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(90, 20);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "繁體中文";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(15, 9);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 20);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "English";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(262, 34);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(302, 490);
            this.checkedListBox1.TabIndex = 5;
            // 
            // btn_WriteAlarmCodeToINI
            // 
            this.btn_WriteAlarmCodeToINI.Location = new System.Drawing.Point(352, 6);
            this.btn_WriteAlarmCodeToINI.Name = "btn_WriteAlarmCodeToINI";
            this.btn_WriteAlarmCodeToINI.Size = new System.Drawing.Size(212, 23);
            this.btn_WriteAlarmCodeToINI.TabIndex = 4;
            this.btn_WriteAlarmCodeToINI.Text = "Write Alarm Code To INI";
            this.btn_WriteAlarmCodeToINI.UseVisualStyleBackColor = true;
            this.btn_WriteAlarmCodeToINI.Click += new System.EventHandler(this.btn_WriteAlarmCodeToINI_Click);
            // 
            // btn_ReloadAlarmINIToRichText
            // 
            this.btn_ReloadAlarmINIToRichText.Location = new System.Drawing.Point(570, 6);
            this.btn_ReloadAlarmINIToRichText.Name = "btn_ReloadAlarmINIToRichText";
            this.btn_ReloadAlarmINIToRichText.Size = new System.Drawing.Size(302, 23);
            this.btn_ReloadAlarmINIToRichText.TabIndex = 3;
            this.btn_ReloadAlarmINIToRichText.Text = "Reload";
            this.btn_ReloadAlarmINIToRichText.UseVisualStyleBackColor = true;
            this.btn_ReloadAlarmINIToRichText.Click += new System.EventHandler(this.btn_ReloadAlarmINIToRichText_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(570, 34);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(302, 484);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(6, 34);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(250, 436);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // ucProcessControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tabControl1);
            this.Name = "ucProcessControl";
            this.Size = new System.Drawing.Size(894, 559);
            this.VisibleChanged += new System.EventHandler(this.ucMachineStatus_VisibleChanged);
            this.tabControl1.ResumeLayout(false);
            this.tPage_AlarmCode.ResumeLayout(false);
            this.tPage_AlarmCode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPage_AlarmCode;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btn_WriteAlarmCodeToINI;
        private System.Windows.Forms.Button btn_ReloadAlarmINIToRichText;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button btn_CheckAll;
        private System.Windows.Forms.Button btn_WriteAllPMAlarmCodeToINI;




    }
}

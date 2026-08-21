namespace ArtSystem.Login
{
    partial class ucAutoLogout
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
            this.groupBoxAutoLogout = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cNum_AutoLogout_Level = new ArtControlLib.comNumBox();
            this.cNum_AutoLogout_Timeout_Minute = new ArtControlLib.comNumBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAutoLogout_Enable = new System.Windows.Forms.Button();
            this.btnAutoLogout_Disable = new System.Windows.Forms.Button();
            this.groupBoxAutoLogout.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAutoLogout
            // 
            this.groupBoxAutoLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAutoLogout.Controls.Add(this.label2);
            this.groupBoxAutoLogout.Controls.Add(this.cNum_AutoLogout_Level);
            this.groupBoxAutoLogout.Controls.Add(this.cNum_AutoLogout_Timeout_Minute);
            this.groupBoxAutoLogout.Controls.Add(this.label1);
            this.groupBoxAutoLogout.Controls.Add(this.btnAutoLogout_Enable);
            this.groupBoxAutoLogout.Controls.Add(this.btnAutoLogout_Disable);
            this.groupBoxAutoLogout.Location = new System.Drawing.Point(0, 0);
            this.groupBoxAutoLogout.Name = "groupBoxAutoLogout";
            this.groupBoxAutoLogout.Size = new System.Drawing.Size(347, 157);
            this.groupBoxAutoLogout.TabIndex = 9;
            this.groupBoxAutoLogout.TabStop = false;
            this.groupBoxAutoLogout.Text = "Auto Logout";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 16);
            this.label2.TabIndex = 439;
            this.label2.Text = "Auto Logout Level";
            // 
            // cNum_AutoLogout_Level
            // 
            this.cNum_AutoLogout_Level._DecimalPlaces = 0;
            this.cNum_AutoLogout_Level._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cNum_AutoLogout_Level._IsSaveToIni = false;
            this.cNum_AutoLogout_Level._IsSaveToLog = false;
            this.cNum_AutoLogout_Level._IsShowCurrentValue = false;
            this.cNum_AutoLogout_Level._IsShowPopForm = true;
            this.cNum_AutoLogout_Level._Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.cNum_AutoLogout_Level._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cNum_AutoLogout_Level._PmtName = null;
            this.cNum_AutoLogout_Level._PmtType = null;
            this.cNum_AutoLogout_Level._TempValue = null;
            this.cNum_AutoLogout_Level._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cNum_AutoLogout_Level.BackColor = System.Drawing.Color.White;
            this.cNum_AutoLogout_Level.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cNum_AutoLogout_Level.Location = new System.Drawing.Point(17, 78);
            this.cNum_AutoLogout_Level.Name = "cNum_AutoLogout_Level";
            this.cNum_AutoLogout_Level.ReadOnly = true;
            this.cNum_AutoLogout_Level.Size = new System.Drawing.Size(98, 23);
            this.cNum_AutoLogout_Level.TabIndex = 438;
            this.cNum_AutoLogout_Level.Text = "0";
            this.cNum_AutoLogout_Level.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cNum_AutoLogout_Level.TextChanged += new System.EventHandler(this.cNum_AutoLogout_Level_TextChanged);
            // 
            // cNum_AutoLogout_Timeout_Minute
            // 
            this.cNum_AutoLogout_Timeout_Minute._DecimalPlaces = 1;
            this.cNum_AutoLogout_Timeout_Minute._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cNum_AutoLogout_Timeout_Minute._IsSaveToIni = false;
            this.cNum_AutoLogout_Timeout_Minute._IsSaveToLog = false;
            this.cNum_AutoLogout_Timeout_Minute._IsShowCurrentValue = false;
            this.cNum_AutoLogout_Timeout_Minute._IsShowPopForm = true;
            this.cNum_AutoLogout_Timeout_Minute._Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.cNum_AutoLogout_Timeout_Minute._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cNum_AutoLogout_Timeout_Minute._PmtName = null;
            this.cNum_AutoLogout_Timeout_Minute._PmtType = null;
            this.cNum_AutoLogout_Timeout_Minute._TempValue = null;
            this.cNum_AutoLogout_Timeout_Minute._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cNum_AutoLogout_Timeout_Minute.BackColor = System.Drawing.Color.White;
            this.cNum_AutoLogout_Timeout_Minute.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cNum_AutoLogout_Timeout_Minute.Location = new System.Drawing.Point(17, 105);
            this.cNum_AutoLogout_Timeout_Minute.Name = "cNum_AutoLogout_Timeout_Minute";
            this.cNum_AutoLogout_Timeout_Minute.ReadOnly = true;
            this.cNum_AutoLogout_Timeout_Minute.Size = new System.Drawing.Size(98, 23);
            this.cNum_AutoLogout_Timeout_Minute.TabIndex = 437;
            this.cNum_AutoLogout_Timeout_Minute.Text = "0";
            this.cNum_AutoLogout_Timeout_Minute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cNum_AutoLogout_Timeout_Minute.TextChanged += new System.EventHandler(this.cNum_AutoLogout_Level_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Auto Logout Timeout (minute)";
            // 
            // btnAutoLogout_Enable
            // 
            this.btnAutoLogout_Enable.Location = new System.Drawing.Point(17, 22);
            this.btnAutoLogout_Enable.Name = "btnAutoLogout_Enable";
            this.btnAutoLogout_Enable.Size = new System.Drawing.Size(100, 50);
            this.btnAutoLogout_Enable.TabIndex = 0;
            this.btnAutoLogout_Enable.Tag = "";
            this.btnAutoLogout_Enable.Text = "Enable";
            this.btnAutoLogout_Enable.UseVisualStyleBackColor = true;
            this.btnAutoLogout_Enable.Click += new System.EventHandler(this.AutoLogoutChange);
            // 
            // btnAutoLogout_Disable
            // 
            this.btnAutoLogout_Disable.Location = new System.Drawing.Point(133, 22);
            this.btnAutoLogout_Disable.Name = "btnAutoLogout_Disable";
            this.btnAutoLogout_Disable.Size = new System.Drawing.Size(100, 50);
            this.btnAutoLogout_Disable.TabIndex = 1;
            this.btnAutoLogout_Disable.Tag = "";
            this.btnAutoLogout_Disable.Text = "Disable";
            this.btnAutoLogout_Disable.UseVisualStyleBackColor = true;
            this.btnAutoLogout_Disable.Click += new System.EventHandler(this.AutoLogoutChange);
            // 
            // ucAutoLogout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBoxAutoLogout);
            this.Name = "ucAutoLogout";
            this.Size = new System.Drawing.Size(347, 157);
            this.groupBoxAutoLogout.ResumeLayout(false);
            this.groupBoxAutoLogout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAutoLogout;
        private System.Windows.Forms.Button btnAutoLogout_Enable;
        private System.Windows.Forms.Button btnAutoLogout_Disable;
        private System.Windows.Forms.Label label2;
        private ArtControlLib.comNumBox cNum_AutoLogout_Level;
        private ArtControlLib.comNumBox cNum_AutoLogout_Timeout_Minute;
        private System.Windows.Forms.Label label1;
    }
}

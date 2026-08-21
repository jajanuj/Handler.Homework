namespace ArtTeach
{
    partial class ucDIOInfo1
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
                components.Dispose() ;
            }
            base.Dispose(disposing) ;
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent() 
        {
            this.lblDIHome = new System.Windows.Forms.Label() ;
            this.lblDOTrigger = new System.Windows.Forms.Label() ;
            this.lblDIReachLED = new System.Windows.Forms.Label() ;
            this.lblDIReach = new System.Windows.Forms.Label() ;
            this.lblDIHomeLED = new System.Windows.Forms.Label() ;
            this.label1 = new System.Windows.Forms.Label() ;
            this.btnDOTrigger = new System.Windows.Forms.Button() ;
            this.clsRoundAngleBtn = new ArtTeach.clsRoundAngleBtn();
            this.SuspendLayout() ;
            // 
            // lblDIHome
            // 
            this.lblDIHome.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblDIHome.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblDIHome.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte) (0) ) ) ;
            this.lblDIHome.ForeColor = System.Drawing.Color.White;
            this.lblDIHome.Location = new System.Drawing.Point(12, 4) ;
            this.lblDIHome.Name = "lblDIHome";
            this.lblDIHome.Size = new System.Drawing.Size(35, 12) ;
            this.lblDIHome.TabIndex = 1109;
            this.lblDIHome.Text = "X100";
            this.lblDIHome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDIHome.MouseEnter += new System.EventHandler(this.btnDOTrigger_MouseEnter) ;
            // 
            // lblDOTrigger
            // 
            this.lblDOTrigger.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                        | System.Windows.Forms.AnchorStyles.Right) ) ) ;
            this.lblDOTrigger.BackColor = System.Drawing.Color.Gray;
            this.lblDOTrigger.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblDOTrigger.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte) (0) ) ) ;
            this.lblDOTrigger.ForeColor = System.Drawing.Color.Yellow;
            this.lblDOTrigger.Location = new System.Drawing.Point(34, 24) ;
            this.lblDOTrigger.Name = "lblDOTrigger";
            this.lblDOTrigger.Size = new System.Drawing.Size(31, 14) ;
            this.lblDOTrigger.TabIndex = 1117;
            this.lblDOTrigger.Text = "Y101";
            this.lblDOTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDIReachLED
            // 
            this.lblDIReachLED.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right) ) ) ;
            this.lblDIReachLED.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblDIReachLED.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblDIReachLED.Font = new System.Drawing.Font("Times New Roman", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte) (0) ) ) ;
            this.lblDIReachLED.ForeColor = System.Drawing.Color.White;
            this.lblDIReachLED.Location = new System.Drawing.Point(84, 15) ;
            this.lblDIReachLED.Name = "lblDIReachLED";
            this.lblDIReachLED.Size = new System.Drawing.Size(14, 18) ;
            this.lblDIReachLED.TabIndex = 1115;
            this.lblDIReachLED.Text = "R";
            this.lblDIReachLED.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDIReach
            // 
            this.lblDIReach.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right) ) ) ;
            this.lblDIReach.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblDIReach.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblDIReach.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte) (0) ) ) ;
            this.lblDIReach.ForeColor = System.Drawing.Color.White;
            this.lblDIReach.Location = new System.Drawing.Point(56, 4) ;
            this.lblDIReach.Name = "lblDIReach";
            this.lblDIReach.Size = new System.Drawing.Size(35, 12) ;
            this.lblDIReach.TabIndex = 1113;
            this.lblDIReach.Text = "X101";
            this.lblDIReach.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDIReach.MouseEnter += new System.EventHandler(this.btnDOTrigger_MouseEnter) ;
            // 
            // lblDIHomeLED
            // 
            this.lblDIHomeLED.BackColor = System.Drawing.Color.DarkSlateGray;
            this.lblDIHomeLED.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblDIHomeLED.Font = new System.Drawing.Font("Times New Roman", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte) (0) ) ) ;
            this.lblDIHomeLED.ForeColor = System.Drawing.Color.White;
            this.lblDIHomeLED.Location = new System.Drawing.Point(1, 15) ;
            this.lblDIHomeLED.Name = "lblDIHomeLED";
            this.lblDIHomeLED.Size = new System.Drawing.Size(14, 18) ;
            this.lblDIHomeLED.TabIndex = 1105;
            this.lblDIHomeLED.Text = "H";
            this.lblDIHomeLED.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                        | System.Windows.Forms.AnchorStyles.Right) ) ) ;
            this.label1.BackColor = System.Drawing.Color.SlateGray;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte) (0) ) ) ;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(28, 62) ;
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 0) ;
            this.label1.TabIndex = 1118;
            this.label1.Text = "Y101";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDOTrigger
            // 
            this.btnDOTrigger.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                        | System.Windows.Forms.AnchorStyles.Left) 
                        | System.Windows.Forms.AnchorStyles.Right) ) ) ;
            this.btnDOTrigger.BackColor = System.Drawing.Color.Gray;
            this.btnDOTrigger.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDOTrigger.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDOTrigger.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDOTrigger.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDOTrigger.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte) (0) ) ) ;
            this.btnDOTrigger.ForeColor = System.Drawing.Color.White;
            this.btnDOTrigger.Location = new System.Drawing.Point(15, 15) ;
            this.btnDOTrigger.Name = "btnDOTrigger";
            this.btnDOTrigger.Size = new System.Drawing.Size(70, 70) ;
            this.btnDOTrigger.TabIndex = 1119;
            this.btnDOTrigger.Tag = "-";
            this.btnDOTrigger.Text = "Lifter";
            this.btnDOTrigger.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDOTrigger.UseVisualStyleBackColor = false;
            this.btnDOTrigger.Click += new System.EventHandler(this.btnDOTrigger_Click) ;
            this.btnDOTrigger.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDOTrigger_MouseDown) ;
            this.btnDOTrigger.MouseEnter += new System.EventHandler(this.btnDOTrigger_MouseEnter) ;
            // 
            // clsRoundAngleBtn
            // 
            this.clsRoundAngleBtn.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                        | System.Windows.Forms.AnchorStyles.Left) 
                        | System.Windows.Forms.AnchorStyles.Right) ) ) ;
            this.clsRoundAngleBtn.EnterBackColor = System.Drawing.Color.DarkSlateGray;
            this.clsRoundAngleBtn.EnterForeColor = System.Drawing.Color.DarkSlateGray;
            this.clsRoundAngleBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int) (((byte) (0) ) ) ) , ((int) (((byte) (0) ) ) ) , ((int) (((byte) (0) ) ) ) , ((int) (((byte) (0) ) ) ) ) ;
            this.clsRoundAngleBtn.FlatAppearance.BorderSize = 0;
            this.clsRoundAngleBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.clsRoundAngleBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.clsRoundAngleBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clsRoundAngleBtn.ForeColor = System.Drawing.Color.Transparent;
            this.clsRoundAngleBtn.HoverBackColor = System.Drawing.Color.DarkSlateGray;
            this.clsRoundAngleBtn.HoverForeColor = System.Drawing.Color.DarkSlateGray;
            this.clsRoundAngleBtn.Location = new System.Drawing.Point(0, 3) ;
            this.clsRoundAngleBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4) ;
            this.clsRoundAngleBtn.Name = "clsRoundAngleBtn";
            this.clsRoundAngleBtn.PressBackColor = System.Drawing.Color.DarkSlateGray;
            this.clsRoundAngleBtn.PressForeColor = System.Drawing.Color.DarkSlateGray;
            this.clsRoundAngleBtn.Radius = 25;
            this.clsRoundAngleBtn.Size = new System.Drawing.Size(100, 100) ;
            this.clsRoundAngleBtn.TabIndex = 1104;
            this.clsRoundAngleBtn.UseVisualStyleBackColor = true;
            // 
            // ucDIOInfo1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F) ;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.lblDOTrigger) ;
            this.Controls.Add(this.btnDOTrigger) ;
            this.Controls.Add(this.lblDIHomeLED) ;
            this.Controls.Add(this.lblDIReachLED) ;
            this.Controls.Add(this.lblDIReach) ;
            this.Controls.Add(this.lblDIHome) ;
            this.Controls.Add(this.label1) ;
            this.Controls.Add(this.clsRoundAngleBtn) ;
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic) ;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5) ;
            this.Name = "ucDIOInfo1";
            this.Size = new System.Drawing.Size(100, 100) ;
            this.ResumeLayout(false) ;

        }

        #endregion

        private System.Windows.Forms.Label lblDIHome;
        private ArtTeach.clsRoundAngleBtn clsRoundAngleBtn;
        private System.Windows.Forms.Label lblDOTrigger;
        private System.Windows.Forms.Label lblDIReachLED;
        private System.Windows.Forms.Label lblDIReach;
        private System.Windows.Forms.Label lblDIHomeLED;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDOTrigger;
    }
}

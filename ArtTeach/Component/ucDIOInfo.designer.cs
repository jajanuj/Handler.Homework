namespace ArtTeach
{
    partial class ucDIOInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDIOInfo));
            this.btnDOTrigger = new System.Windows.Forms.Label();
            this.lblDIReach = new System.Windows.Forms.Label();
            this.lblDIHome = new System.Windows.Forms.Label();
            this.lblDIReachLED = new ArtTeach.clsRoundAngleBtn();
            this.lblDIHomeLED = new ArtTeach.clsRoundAngleBtn();
            this.clsSwitchButton1 = new ArtTeach.clsSwitchButton();
            this.SuspendLayout();
            // 
            // btnDOTrigger
            // 
            this.btnDOTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDOTrigger.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDOTrigger.Location = new System.Drawing.Point(3, 0);
            this.btnDOTrigger.Name = "btnDOTrigger";
            this.btnDOTrigger.Size = new System.Drawing.Size(146, 17);
            this.btnDOTrigger.TabIndex = 1;
            this.btnDOTrigger.Text = "BH Vacuum";
            this.btnDOTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDOTrigger.MouseEnter += new System.EventHandler(this.lblDIReach_MouseEnter);
            this.btnDOTrigger.MouseLeave += new System.EventHandler(this.lblDIReach_MouseLeave);
            // 
            // lblDIReach
            // 
            this.lblDIReach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDIReach.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDIReach.Location = new System.Drawing.Point(47, 21);
            this.lblDIReach.Name = "lblDIReach";
            this.lblDIReach.Size = new System.Drawing.Size(102, 15);
            this.lblDIReach.TabIndex = 5;
            this.lblDIReach.Text = "X128";
            this.lblDIReach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDIReach.MouseEnter += new System.EventHandler(this.lblDIReach_MouseEnter);
            this.lblDIReach.MouseLeave += new System.EventHandler(this.lblDIReach_MouseLeave);
            // 
            // lblDIHome
            // 
            this.lblDIHome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDIHome.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDIHome.Location = new System.Drawing.Point(47, 42);
            this.lblDIHome.Name = "lblDIHome";
            this.lblDIHome.Size = new System.Drawing.Size(102, 15);
            this.lblDIHome.TabIndex = 6;
            this.lblDIHome.Text = "On";
            this.lblDIHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDIHome.MouseEnter += new System.EventHandler(this.lblDIReach_MouseEnter);
            this.lblDIHome.MouseLeave += new System.EventHandler(this.lblDIReach_MouseLeave);
            // 
            // lblDIReachLED
            // 
            this.lblDIReachLED._AutoAdjustBackColor = false;
            this.lblDIReachLED.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDIReachLED.DisableColor = System.Drawing.Color.DarkGray;
            this.lblDIReachLED.Enabled = false;
            this.lblDIReachLED.EnterBackColor = System.Drawing.Color.Blue;
            this.lblDIReachLED.EnterForeColor = System.Drawing.Color.White;
            this.lblDIReachLED.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDIReachLED.FlatAppearance.BorderSize = 0;
            this.lblDIReachLED.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.lblDIReachLED.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.lblDIReachLED.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDIReachLED.HoverBackColor = System.Drawing.SystemColors.ControlDark;
            this.lblDIReachLED.HoverForeColor = System.Drawing.Color.White;
            this.lblDIReachLED.Location = new System.Drawing.Point(32, 20);
            this.lblDIReachLED.Name = "lblDIReachLED";
            this.lblDIReachLED.PressBackColor = System.Drawing.Color.DarkBlue;
            this.lblDIReachLED.PressForeColor = System.Drawing.Color.White;
            this.lblDIReachLED.Radius = 10;
            this.lblDIReachLED.Size = new System.Drawing.Size(15, 15);
            this.lblDIReachLED.TabIndex = 8;
            this.lblDIReachLED.UseVisualStyleBackColor = true;
            // 
            // lblDIHomeLED
            // 
            this.lblDIHomeLED._AutoAdjustBackColor = false;
            this.lblDIHomeLED.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDIHomeLED.DisableColor = System.Drawing.Color.DarkGray;
            this.lblDIHomeLED.Enabled = false;
            this.lblDIHomeLED.EnterBackColor = System.Drawing.Color.Blue;
            this.lblDIHomeLED.EnterForeColor = System.Drawing.Color.White;
            this.lblDIHomeLED.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDIHomeLED.FlatAppearance.BorderSize = 0;
            this.lblDIHomeLED.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.lblDIHomeLED.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.lblDIHomeLED.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDIHomeLED.HoverBackColor = System.Drawing.SystemColors.ControlDark;
            this.lblDIHomeLED.HoverForeColor = System.Drawing.Color.White;
            this.lblDIHomeLED.Location = new System.Drawing.Point(32, 41);
            this.lblDIHomeLED.Name = "lblDIHomeLED";
            this.lblDIHomeLED.PressBackColor = System.Drawing.Color.DarkBlue;
            this.lblDIHomeLED.PressForeColor = System.Drawing.Color.White;
            this.lblDIHomeLED.Radius = 10;
            this.lblDIHomeLED.Size = new System.Drawing.Size(15, 15);
            this.lblDIHomeLED.TabIndex = 7;
            this.lblDIHomeLED.UseVisualStyleBackColor = true;
            // 
            // clsSwitchButton1
            // 
            this.clsSwitchButton1._EdgeColor = System.Drawing.Color.DarkGray;
            this.clsSwitchButton1._OffButtonColor = System.Drawing.SystemColors.Control;
            this.clsSwitchButton1._OffColor = System.Drawing.Color.DarkGray;
            this.clsSwitchButton1._OnButtonColor = System.Drawing.Color.MediumTurquoise;
            this.clsSwitchButton1._OnColor = System.Drawing.Color.Aquamarine;
            this.clsSwitchButton1._Status = false;
            this.clsSwitchButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clsSwitchButton1.BackColor = System.Drawing.Color.Transparent;
            this.clsSwitchButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("clsSwitchButton1.BackgroundImage")));
            this.clsSwitchButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.clsSwitchButton1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.clsSwitchButton1.Location = new System.Drawing.Point(4, 10);
            this.clsSwitchButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clsSwitchButton1.MaximumSize = new System.Drawing.Size(25, 60);
            this.clsSwitchButton1.MinimumSize = new System.Drawing.Size(25, 60);
            this.clsSwitchButton1.Name = "clsSwitchButton1";
            this.clsSwitchButton1.Size = new System.Drawing.Size(25, 60);
            this.clsSwitchButton1.TabIndex = 0;
            this.clsSwitchButton1.Click += new System.EventHandler(this.clsSwitchButton1_Click);
            this.clsSwitchButton1.MouseEnter += new System.EventHandler(this.lblDIReach_MouseEnter);
            this.clsSwitchButton1.MouseLeave += new System.EventHandler(this.lblDIReach_MouseLeave);
            // 
            // ucDIOInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.clsSwitchButton1);
            this.Controls.Add(this.btnDOTrigger);
            this.Controls.Add(this.lblDIReachLED);
            this.Controls.Add(this.lblDIHomeLED);
            this.Controls.Add(this.lblDIHome);
            this.Controls.Add(this.lblDIReach);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucDIOInfo";
            this.Size = new System.Drawing.Size(149, 75);
            this.ResumeLayout(false);

        }

        #endregion

        private clsSwitchButton clsSwitchButton1;
        private System.Windows.Forms.Label btnDOTrigger;
        private System.Windows.Forms.Label lblDIReach;
        private System.Windows.Forms.Label lblDIHome;
        private ArtTeach.clsRoundAngleBtn lblDIHomeLED;
        private ArtTeach.clsRoundAngleBtn lblDIReachLED;

    }
}

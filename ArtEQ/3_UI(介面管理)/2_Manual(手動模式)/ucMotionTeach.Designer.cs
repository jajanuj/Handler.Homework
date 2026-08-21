namespace ArtEQ
{
    partial class ucMotionTeach
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
            this.gb_Lane = new System.Windows.Forms.GroupBox();
            this.LEDLabel_DI1_Front = new System.Windows.Forms.Label();
            this.LEDLabel_DI2_Front = new System.Windows.Forms.Label();
            this.LEDLabel_DI3_Front = new System.Windows.Forms.Label();
            this.LEDLabel_DI4_Front = new System.Windows.Forms.Label();
            this.btn_LaneStop_Front = new System.Windows.Forms.Button();
            this.btn_LaneRun_Front = new System.Windows.Forms.Button();
            this.LED_DI4_Front = new ArtTeach.clsRoundAngleBtn();
            this.LED_DI3_Front = new ArtTeach.clsRoundAngleBtn();
            this.LED_DI2_Front = new ArtTeach.clsRoundAngleBtn();
            this.LED_DI1_Front = new ArtTeach.clsRoundAngleBtn();
            this.ucEditUI1 = new ArtTeach.ucEditUI();
            this.gb_Lane.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Lane
            // 
            this.gb_Lane.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gb_Lane.Controls.Add(this.LEDLabel_DI1_Front);
            this.gb_Lane.Controls.Add(this.LEDLabel_DI2_Front);
            this.gb_Lane.Controls.Add(this.LEDLabel_DI3_Front);
            this.gb_Lane.Controls.Add(this.LEDLabel_DI4_Front);
            this.gb_Lane.Controls.Add(this.btn_LaneStop_Front);
            this.gb_Lane.Controls.Add(this.btn_LaneRun_Front);
            this.gb_Lane.Controls.Add(this.LED_DI4_Front);
            this.gb_Lane.Controls.Add(this.LED_DI3_Front);
            this.gb_Lane.Controls.Add(this.LED_DI2_Front);
            this.gb_Lane.Controls.Add(this.LED_DI1_Front);
            this.gb_Lane.Location = new System.Drawing.Point(181, 470);
            this.gb_Lane.Name = "gb_Lane";
            this.gb_Lane.Size = new System.Drawing.Size(281, 90);
            this.gb_Lane.TabIndex = 2;
            this.gb_Lane.TabStop = false;
            this.gb_Lane.Text = "Lane Control";
            this.gb_Lane.Visible = false;
            // 
            // LEDLabel_DI1_Front
            // 
            this.LEDLabel_DI1_Front.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LEDLabel_DI1_Front.Location = new System.Drawing.Point(24, 19);
            this.LEDLabel_DI1_Front.Name = "LEDLabel_DI1_Front";
            this.LEDLabel_DI1_Front.Size = new System.Drawing.Size(161, 15);
            this.LEDLabel_DI1_Front.TabIndex = 9;
            this.LEDLabel_DI1_Front.Text = "Load Sensor";
            this.LEDLabel_DI1_Front.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LEDLabel_DI2_Front
            // 
            this.LEDLabel_DI2_Front.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LEDLabel_DI2_Front.Location = new System.Drawing.Point(24, 34);
            this.LEDLabel_DI2_Front.Name = "LEDLabel_DI2_Front";
            this.LEDLabel_DI2_Front.Size = new System.Drawing.Size(161, 15);
            this.LEDLabel_DI2_Front.TabIndex = 11;
            this.LEDLabel_DI2_Front.Text = "Unload Arrive Sensor";
            this.LEDLabel_DI2_Front.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LEDLabel_DI3_Front
            // 
            this.LEDLabel_DI3_Front.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LEDLabel_DI3_Front.Location = new System.Drawing.Point(24, 49);
            this.LEDLabel_DI3_Front.Name = "LEDLabel_DI3_Front";
            this.LEDLabel_DI3_Front.Size = new System.Drawing.Size(161, 15);
            this.LEDLabel_DI3_Front.TabIndex = 13;
            this.LEDLabel_DI3_Front.Text = "Load Arrive Sensor";
            this.LEDLabel_DI3_Front.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LEDLabel_DI4_Front
            // 
            this.LEDLabel_DI4_Front.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LEDLabel_DI4_Front.Location = new System.Drawing.Point(24, 64);
            this.LEDLabel_DI4_Front.Name = "LEDLabel_DI4_Front";
            this.LEDLabel_DI4_Front.Size = new System.Drawing.Size(161, 15);
            this.LEDLabel_DI4_Front.TabIndex = 15;
            this.LEDLabel_DI4_Front.Text = "Unload Sensor";
            this.LEDLabel_DI4_Front.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_LaneStop_Front
            // 
            this.btn_LaneStop_Front.BackColor = System.Drawing.Color.Red;
            this.btn_LaneStop_Front.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LaneStop_Front.Location = new System.Drawing.Point(191, 50);
            this.btn_LaneStop_Front.Name = "btn_LaneStop_Front";
            this.btn_LaneStop_Front.Size = new System.Drawing.Size(85, 38);
            this.btn_LaneStop_Front.TabIndex = 21;
            this.btn_LaneStop_Front.Text = "Stop";
            this.btn_LaneStop_Front.UseVisualStyleBackColor = false;
            // 
            // btn_LaneRun_Front
            // 
            this.btn_LaneRun_Front.Location = new System.Drawing.Point(191, 9);
            this.btn_LaneRun_Front.Name = "btn_LaneRun_Front";
            this.btn_LaneRun_Front.Size = new System.Drawing.Size(85, 38);
            this.btn_LaneRun_Front.TabIndex = 3;
            this.btn_LaneRun_Front.Text = "Run";
            this.btn_LaneRun_Front.UseVisualStyleBackColor = true;
            // 
            // LED_DI4_Front
            // 
            this.LED_DI4_Front._AutoAdjustBackColor = false;
            this.LED_DI4_Front.DisableColor = System.Drawing.Color.DarkGray;
            this.LED_DI4_Front.Enabled = false;
            this.LED_DI4_Front.EnterBackColor = System.Drawing.Color.Blue;
            this.LED_DI4_Front.EnterForeColor = System.Drawing.Color.White;
            this.LED_DI4_Front.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LED_DI4_Front.FlatAppearance.BorderSize = 0;
            this.LED_DI4_Front.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.LED_DI4_Front.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.LED_DI4_Front.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LED_DI4_Front.HoverBackColor = System.Drawing.SystemColors.ControlDark;
            this.LED_DI4_Front.HoverForeColor = System.Drawing.Color.White;
            this.LED_DI4_Front.Location = new System.Drawing.Point(9, 63);
            this.LED_DI4_Front.Name = "LED_DI4_Front";
            this.LED_DI4_Front.PressBackColor = System.Drawing.Color.DarkBlue;
            this.LED_DI4_Front.PressForeColor = System.Drawing.Color.White;
            this.LED_DI4_Front.Radius = 10;
            this.LED_DI4_Front.Size = new System.Drawing.Size(15, 15);
            this.LED_DI4_Front.TabIndex = 16;
            this.LED_DI4_Front.UseVisualStyleBackColor = true;
            // 
            // LED_DI3_Front
            // 
            this.LED_DI3_Front._AutoAdjustBackColor = false;
            this.LED_DI3_Front.DisableColor = System.Drawing.Color.DarkGray;
            this.LED_DI3_Front.Enabled = false;
            this.LED_DI3_Front.EnterBackColor = System.Drawing.Color.Blue;
            this.LED_DI3_Front.EnterForeColor = System.Drawing.Color.White;
            this.LED_DI3_Front.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LED_DI3_Front.FlatAppearance.BorderSize = 0;
            this.LED_DI3_Front.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.LED_DI3_Front.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.LED_DI3_Front.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LED_DI3_Front.HoverBackColor = System.Drawing.SystemColors.ControlDark;
            this.LED_DI3_Front.HoverForeColor = System.Drawing.Color.White;
            this.LED_DI3_Front.Location = new System.Drawing.Point(9, 48);
            this.LED_DI3_Front.Name = "LED_DI3_Front";
            this.LED_DI3_Front.PressBackColor = System.Drawing.Color.DarkBlue;
            this.LED_DI3_Front.PressForeColor = System.Drawing.Color.White;
            this.LED_DI3_Front.Radius = 10;
            this.LED_DI3_Front.Size = new System.Drawing.Size(15, 15);
            this.LED_DI3_Front.TabIndex = 14;
            this.LED_DI3_Front.UseVisualStyleBackColor = true;
            // 
            // LED_DI2_Front
            // 
            this.LED_DI2_Front._AutoAdjustBackColor = false;
            this.LED_DI2_Front.DisableColor = System.Drawing.Color.DarkGray;
            this.LED_DI2_Front.Enabled = false;
            this.LED_DI2_Front.EnterBackColor = System.Drawing.Color.Blue;
            this.LED_DI2_Front.EnterForeColor = System.Drawing.Color.White;
            this.LED_DI2_Front.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LED_DI2_Front.FlatAppearance.BorderSize = 0;
            this.LED_DI2_Front.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.LED_DI2_Front.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.LED_DI2_Front.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LED_DI2_Front.HoverBackColor = System.Drawing.SystemColors.ControlDark;
            this.LED_DI2_Front.HoverForeColor = System.Drawing.Color.White;
            this.LED_DI2_Front.Location = new System.Drawing.Point(9, 33);
            this.LED_DI2_Front.Name = "LED_DI2_Front";
            this.LED_DI2_Front.PressBackColor = System.Drawing.Color.DarkBlue;
            this.LED_DI2_Front.PressForeColor = System.Drawing.Color.White;
            this.LED_DI2_Front.Radius = 10;
            this.LED_DI2_Front.Size = new System.Drawing.Size(15, 15);
            this.LED_DI2_Front.TabIndex = 12;
            this.LED_DI2_Front.UseVisualStyleBackColor = true;
            // 
            // LED_DI1_Front
            // 
            this.LED_DI1_Front._AutoAdjustBackColor = false;
            this.LED_DI1_Front.DisableColor = System.Drawing.Color.DarkGray;
            this.LED_DI1_Front.Enabled = false;
            this.LED_DI1_Front.EnterBackColor = System.Drawing.Color.Blue;
            this.LED_DI1_Front.EnterForeColor = System.Drawing.Color.White;
            this.LED_DI1_Front.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LED_DI1_Front.FlatAppearance.BorderSize = 0;
            this.LED_DI1_Front.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.LED_DI1_Front.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.LED_DI1_Front.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LED_DI1_Front.HoverBackColor = System.Drawing.SystemColors.ControlDark;
            this.LED_DI1_Front.HoverForeColor = System.Drawing.Color.White;
            this.LED_DI1_Front.Location = new System.Drawing.Point(9, 18);
            this.LED_DI1_Front.Name = "LED_DI1_Front";
            this.LED_DI1_Front.PressBackColor = System.Drawing.Color.DarkBlue;
            this.LED_DI1_Front.PressForeColor = System.Drawing.Color.White;
            this.LED_DI1_Front.Radius = 10;
            this.LED_DI1_Front.Size = new System.Drawing.Size(15, 15);
            this.LED_DI1_Front.TabIndex = 10;
            this.LED_DI1_Front.UseVisualStyleBackColor = true;
            // 
            // ucEditUI1
            // 
            this.ucEditUI1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucEditUI1.AutoScroll = true;
            this.ucEditUI1.AutoSize = true;
            this.ucEditUI1.BackColor = System.Drawing.Color.Transparent;
            this.ucEditUI1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucEditUI1.Location = new System.Drawing.Point(0, 4);
            this.ucEditUI1.Margin = new System.Windows.Forms.Padding(4);
            this.ucEditUI1.Name = "ucEditUI1";
            this.ucEditUI1.Size = new System.Drawing.Size(1160, 631);
            this.ucEditUI1.TabIndex = 1;
            this.ucEditUI1.m_EventDIOSafeCheck += new ArtTeach.ucEditUI.EventDIOSafeCheck(this.ucEditUI1_m_EventDIOSafeCheck);
            this.ucEditUI1.m_EventAxisSafeCheck += new ArtTeach.ucEditUI.EventAxisSafeCheck(this.ucEditUI1_m_EventAxisSafeCheck_1);
            this.ucEditUI1.m_EventSetValueChanged += new ArtTeach.ucEditUI.EventSetValueChanged(this.ucEditUI1_m_EventSetValueChanged);
            // 
            // ucMotionTeach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gb_Lane);
            this.Controls.Add(this.ucEditUI1);
            this.Name = "ucMotionTeach";
            this.Size = new System.Drawing.Size(1162, 633);
            this.SizeChanged += new System.EventHandler(this.ucMotionTeach_SizeChanged);
            this.gb_Lane.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ArtTeach.ucEditUI ucEditUI1;
        private System.Windows.Forms.GroupBox gb_Lane;
        private ArtTeach.clsRoundAngleBtn LED_DI4_Front;
        private System.Windows.Forms.Label LEDLabel_DI4_Front;
        private ArtTeach.clsRoundAngleBtn LED_DI3_Front;
        private System.Windows.Forms.Label LEDLabel_DI3_Front;
        private ArtTeach.clsRoundAngleBtn LED_DI2_Front;
        private System.Windows.Forms.Label LEDLabel_DI2_Front;
        private ArtTeach.clsRoundAngleBtn LED_DI1_Front;
        private System.Windows.Forms.Label LEDLabel_DI1_Front;
        private System.Windows.Forms.Button btn_LaneStop_Front;
        private System.Windows.Forms.Button btn_LaneRun_Front;


    }
}

namespace ArtSystem.MultiSystem
{
    partial class ucDeviceSetting
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
            this.tPage_RollerMotor = new System.Windows.Forms.TabPage();
            this.tPage_Reader = new System.Windows.Forms.TabPage();
            this.tPage_HighSensor = new System.Windows.Forms.TabPage();
            this.tPage_HeaterModule = new System.Windows.Forms.TabPage();
            this.tPage_WeightScale = new System.Windows.Forms.TabPage();
            this.tPage_TCPLink = new System.Windows.Forms.TabPage();
            this.tPage_DispValve = new System.Windows.Forms.TabPage();
            this.tPage_BottomCCD = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tPage_RollerMotor);
            this.tabControl1.Controls.Add(this.tPage_Reader);
            this.tabControl1.Controls.Add(this.tPage_HighSensor);
            this.tabControl1.Controls.Add(this.tPage_HeaterModule);
            this.tabControl1.Controls.Add(this.tPage_WeightScale);
            this.tabControl1.Controls.Add(this.tPage_TCPLink);
            this.tabControl1.Controls.Add(this.tPage_DispValve);
            this.tabControl1.Controls.Add(this.tPage_BottomCCD);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(969, 605);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tPage_RollerMotor
            // 
            this.tPage_RollerMotor.Location = new System.Drawing.Point(4, 25);
            this.tPage_RollerMotor.Name = "tPage_RollerMotor";
            this.tPage_RollerMotor.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_RollerMotor.Size = new System.Drawing.Size(961, 576);
            this.tPage_RollerMotor.TabIndex = 0;
            this.tPage_RollerMotor.Text = "Roller motor";
            this.tPage_RollerMotor.UseVisualStyleBackColor = true;
            // 
            // tPage_Reader
            // 
            this.tPage_Reader.Location = new System.Drawing.Point(4, 25);
            this.tPage_Reader.Name = "tPage_Reader";
            this.tPage_Reader.Size = new System.Drawing.Size(961, 576);
            this.tPage_Reader.TabIndex = 2;
            this.tPage_Reader.Text = "Reader";
            this.tPage_Reader.UseVisualStyleBackColor = true;
            // 
            // tPage_HighSensor
            // 
            this.tPage_HighSensor.Location = new System.Drawing.Point(4, 25);
            this.tPage_HighSensor.Name = "tPage_HighSensor";
            this.tPage_HighSensor.Size = new System.Drawing.Size(961, 576);
            this.tPage_HighSensor.TabIndex = 1;
            this.tPage_HighSensor.Text = "High Sensor";
            this.tPage_HighSensor.UseVisualStyleBackColor = true;
            // 
            // tPage_HeaterModule
            // 
            this.tPage_HeaterModule.Location = new System.Drawing.Point(4, 25);
            this.tPage_HeaterModule.Name = "tPage_HeaterModule";
            this.tPage_HeaterModule.Size = new System.Drawing.Size(961, 576);
            this.tPage_HeaterModule.TabIndex = 4;
            this.tPage_HeaterModule.Text = "Heater Module";
            this.tPage_HeaterModule.UseVisualStyleBackColor = true;
            // 
            // tPage_WeightScale
            // 
            this.tPage_WeightScale.Location = new System.Drawing.Point(4, 25);
            this.tPage_WeightScale.Name = "tPage_WeightScale";
            this.tPage_WeightScale.Size = new System.Drawing.Size(961, 576);
            this.tPage_WeightScale.TabIndex = 5;
            this.tPage_WeightScale.Text = "Weight Scale";
            this.tPage_WeightScale.UseVisualStyleBackColor = true;
            // 
            // tPage_TCPLink
            // 
            this.tPage_TCPLink.Location = new System.Drawing.Point(4, 25);
            this.tPage_TCPLink.Name = "tPage_TCPLink";
            this.tPage_TCPLink.Size = new System.Drawing.Size(961, 576);
            this.tPage_TCPLink.TabIndex = 3;
            this.tPage_TCPLink.Text = "TCP Link";
            this.tPage_TCPLink.UseVisualStyleBackColor = true;
            // 
            // tPage_DispValve
            // 
            this.tPage_DispValve.Location = new System.Drawing.Point(4, 25);
            this.tPage_DispValve.Name = "tPage_DispValve";
            this.tPage_DispValve.Size = new System.Drawing.Size(961, 576);
            this.tPage_DispValve.TabIndex = 6;
            this.tPage_DispValve.Text = "Disp Valve";
            this.tPage_DispValve.UseVisualStyleBackColor = true;
            // 
            // tPage_BottomCCD
            // 
            this.tPage_BottomCCD.Location = new System.Drawing.Point(4, 25);
            this.tPage_BottomCCD.Name = "tPage_BottomCCD";
            this.tPage_BottomCCD.Size = new System.Drawing.Size(961, 576);
            this.tPage_BottomCCD.TabIndex = 8;
            this.tPage_BottomCCD.Text = "Bottom CCD";
            this.tPage_BottomCCD.UseVisualStyleBackColor = true;
            // 
            // ucDeviceSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tabControl1);
            this.Name = "ucDeviceSetting";
            this.Size = new System.Drawing.Size(975, 611);
            this.VisibleChanged += new System.EventHandler(this.ucDeviceSetting_VisibleChanged);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPage_RollerMotor;
        private System.Windows.Forms.TabPage tPage_HighSensor;
        private System.Windows.Forms.TabPage tPage_Reader;
        private System.Windows.Forms.TabPage tPage_TCPLink;
        private System.Windows.Forms.TabPage tPage_HeaterModule;
        private System.Windows.Forms.TabPage tPage_WeightScale;
        private System.Windows.Forms.TabPage tPage_DispValve;
        private System.Windows.Forms.TabPage tPage_BottomCCD;
    }
}

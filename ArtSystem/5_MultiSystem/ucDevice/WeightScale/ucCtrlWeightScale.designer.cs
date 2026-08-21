namespace ArtSystem.MultiSystem
{
    partial class ucCtrlWeightScale
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
            this.labName = new System.Windows.Forms.Label();
            this.btnWeightScale_Read = new System.Windows.Forms.Button();
            this.txt_LaserValue = new System.Windows.Forms.TextBox();
            this.btnWeightScale_ReadStable = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnWeightScale_Connect = new System.Windows.Forms.Button();
            this.btnWeightScale_ResetZero = new System.Windows.Forms.Button();
            this.btnWeightScale_CancelCmd = new System.Windows.Forms.Button();
            this.btnWeightScale_Disconnect = new System.Windows.Forms.Button();
            this.btnWeightScale_Calibration = new System.Windows.Forms.Button();
            this.btnWeightScale_SetLimit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labName
            // 
            this.labName.BackColor = System.Drawing.Color.DarkGray;
            this.labName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labName.Location = new System.Drawing.Point(3, 2);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(238, 26);
            this.labName.TabIndex = 153;
            this.labName.Text = "Sensor Name";
            this.labName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnWeightScale_Read
            // 
            this.btnWeightScale_Read.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWeightScale_Read.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeightScale_Read.Location = new System.Drawing.Point(3, 87);
            this.btnWeightScale_Read.Name = "btnWeightScale_Read";
            this.btnWeightScale_Read.Size = new System.Drawing.Size(115, 45);
            this.btnWeightScale_Read.TabIndex = 157;
            this.btnWeightScale_Read.Text = "Read";
            this.btnWeightScale_Read.UseVisualStyleBackColor = true;
            this.btnWeightScale_Read.Click += new System.EventHandler(this.btnWeightScale_Read_Click);
            // 
            // txt_LaserValue
            // 
            this.txt_LaserValue.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LaserValue.Location = new System.Drawing.Point(3, 54);
            this.txt_LaserValue.Name = "txt_LaserValue";
            this.txt_LaserValue.Size = new System.Drawing.Size(238, 27);
            this.txt_LaserValue.TabIndex = 158;
            // 
            // btnWeightScale_ReadStable
            // 
            this.btnWeightScale_ReadStable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWeightScale_ReadStable.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeightScale_ReadStable.Location = new System.Drawing.Point(124, 87);
            this.btnWeightScale_ReadStable.Name = "btnWeightScale_ReadStable";
            this.btnWeightScale_ReadStable.Size = new System.Drawing.Size(115, 45);
            this.btnWeightScale_ReadStable.TabIndex = 159;
            this.btnWeightScale_ReadStable.Text = "Read Stable";
            this.btnWeightScale_ReadStable.UseVisualStyleBackColor = true;
            this.btnWeightScale_ReadStable.Click += new System.EventHandler(this.btnWeightScale_Live_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 16);
            this.label1.TabIndex = 160;
            this.label1.Text = "Weight Value (mg) :";
            // 
            // btnWeightScale_Connect
            // 
            this.btnWeightScale_Connect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWeightScale_Connect.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeightScale_Connect.Location = new System.Drawing.Point(3, 189);
            this.btnWeightScale_Connect.Name = "btnWeightScale_Connect";
            this.btnWeightScale_Connect.Size = new System.Drawing.Size(115, 45);
            this.btnWeightScale_Connect.TabIndex = 161;
            this.btnWeightScale_Connect.Text = "Connect";
            this.btnWeightScale_Connect.UseVisualStyleBackColor = true;
            this.btnWeightScale_Connect.Click += new System.EventHandler(this.btnWeightScale_Connect_Click);
            // 
            // btnWeightScale_ResetZero
            // 
            this.btnWeightScale_ResetZero.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWeightScale_ResetZero.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeightScale_ResetZero.Location = new System.Drawing.Point(3, 138);
            this.btnWeightScale_ResetZero.Name = "btnWeightScale_ResetZero";
            this.btnWeightScale_ResetZero.Size = new System.Drawing.Size(115, 45);
            this.btnWeightScale_ResetZero.TabIndex = 162;
            this.btnWeightScale_ResetZero.Text = "Reset Zero";
            this.btnWeightScale_ResetZero.UseVisualStyleBackColor = true;
            this.btnWeightScale_ResetZero.Click += new System.EventHandler(this.btnWeightScale_ResetZero_Click);
            // 
            // btnWeightScale_CancelCmd
            // 
            this.btnWeightScale_CancelCmd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWeightScale_CancelCmd.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeightScale_CancelCmd.Location = new System.Drawing.Point(124, 138);
            this.btnWeightScale_CancelCmd.Name = "btnWeightScale_CancelCmd";
            this.btnWeightScale_CancelCmd.Size = new System.Drawing.Size(115, 45);
            this.btnWeightScale_CancelCmd.TabIndex = 163;
            this.btnWeightScale_CancelCmd.Text = "Cancel Cmd";
            this.btnWeightScale_CancelCmd.UseVisualStyleBackColor = true;
            this.btnWeightScale_CancelCmd.Click += new System.EventHandler(this.btnWeightScale_CancelCmd_Click);
            // 
            // btnWeightScale_Disconnect
            // 
            this.btnWeightScale_Disconnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWeightScale_Disconnect.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeightScale_Disconnect.Location = new System.Drawing.Point(124, 189);
            this.btnWeightScale_Disconnect.Name = "btnWeightScale_Disconnect";
            this.btnWeightScale_Disconnect.Size = new System.Drawing.Size(115, 45);
            this.btnWeightScale_Disconnect.TabIndex = 168;
            this.btnWeightScale_Disconnect.Text = "Disconnect";
            this.btnWeightScale_Disconnect.UseVisualStyleBackColor = true;
            this.btnWeightScale_Disconnect.Click += new System.EventHandler(this.btnWeightScale_Disconnect_Click);
            // 
            // btnWeightScale_Calibration
            // 
            this.btnWeightScale_Calibration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWeightScale_Calibration.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeightScale_Calibration.Location = new System.Drawing.Point(3, 240);
            this.btnWeightScale_Calibration.Name = "btnWeightScale_Calibration";
            this.btnWeightScale_Calibration.Size = new System.Drawing.Size(115, 45);
            this.btnWeightScale_Calibration.TabIndex = 169;
            this.btnWeightScale_Calibration.Text = "Calibration";
            this.btnWeightScale_Calibration.UseVisualStyleBackColor = true;
            this.btnWeightScale_Calibration.Click += new System.EventHandler(this.btnWeightScale_Calibration_Click);
            // 
            // btnWeightScale_SetLimit
            // 
            this.btnWeightScale_SetLimit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWeightScale_SetLimit.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeightScale_SetLimit.Location = new System.Drawing.Point(124, 240);
            this.btnWeightScale_SetLimit.Name = "btnWeightScale_SetLimit";
            this.btnWeightScale_SetLimit.Size = new System.Drawing.Size(115, 45);
            this.btnWeightScale_SetLimit.TabIndex = 170;
            this.btnWeightScale_SetLimit.Text = "Set Limit";
            this.btnWeightScale_SetLimit.UseVisualStyleBackColor = true;
            this.btnWeightScale_SetLimit.Visible = false;
            this.btnWeightScale_SetLimit.Click += new System.EventHandler(this.btnWeightScale_SetLimit_Click);
            // 
            // ucCtrlWeightScale
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.btnWeightScale_SetLimit);
            this.Controls.Add(this.btnWeightScale_Calibration);
            this.Controls.Add(this.btnWeightScale_Disconnect);
            this.Controls.Add(this.btnWeightScale_CancelCmd);
            this.Controls.Add(this.btnWeightScale_ResetZero);
            this.Controls.Add(this.btnWeightScale_Connect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnWeightScale_ReadStable);
            this.Controls.Add(this.txt_LaserValue);
            this.Controls.Add(this.btnWeightScale_Read);
            this.Controls.Add(this.labName);
            this.Name = "ucCtrlWeightScale";
            this.Size = new System.Drawing.Size(244, 295);
            this.VisibleChanged += new System.EventHandler(this.ucCtrlWeightScale_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Button btnWeightScale_Read;
        private System.Windows.Forms.TextBox txt_LaserValue;
        private System.Windows.Forms.Button btnWeightScale_ReadStable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnWeightScale_Connect;
        private System.Windows.Forms.Button btnWeightScale_ResetZero;
        private System.Windows.Forms.Button btnWeightScale_CancelCmd;
        private System.Windows.Forms.Button btnWeightScale_Disconnect;
        private System.Windows.Forms.Button btnWeightScale_Calibration;
        private System.Windows.Forms.Button btnWeightScale_SetLimit;
    }
}

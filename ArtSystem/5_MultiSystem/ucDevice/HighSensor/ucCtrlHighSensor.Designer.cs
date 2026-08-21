namespace ArtSystem.MultiSystem
{
    partial class ucCtrlHighSensor
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
            this.btnHighSensor_Read = new System.Windows.Forms.Button();
            this.txt_LaserValue = new System.Windows.Forms.TextBox();
            this.btnHighSensor_Live = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHighSensor_Connect = new System.Windows.Forms.Button();
            this.btnHighSensor_ResetZero = new System.Windows.Forms.Button();
            this.btnHighSensor_ClearOffset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHighSensor_Delay_ms = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHighSensor_Timeout_ms = new System.Windows.Forms.TextBox();
            this.btnHighSensor_Disconnect = new System.Windows.Forms.Button();
            this.btnHighSensor_Bypass = new System.Windows.Forms.Button();
            this.btnHighSensor_Simulate = new System.Windows.Forms.Button();
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
            // btnHighSensor_Read
            // 
            this.btnHighSensor_Read.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHighSensor_Read.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighSensor_Read.Location = new System.Drawing.Point(3, 87);
            this.btnHighSensor_Read.Name = "btnHighSensor_Read";
            this.btnHighSensor_Read.Size = new System.Drawing.Size(115, 45);
            this.btnHighSensor_Read.TabIndex = 157;
            this.btnHighSensor_Read.Text = "Read";
            this.btnHighSensor_Read.UseVisualStyleBackColor = true;
            this.btnHighSensor_Read.Click += new System.EventHandler(this.btnHighSensor_Read_Click);
            // 
            // txt_LaserValue
            // 
            this.txt_LaserValue.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LaserValue.Location = new System.Drawing.Point(3, 54);
            this.txt_LaserValue.Name = "txt_LaserValue";
            this.txt_LaserValue.Size = new System.Drawing.Size(238, 27);
            this.txt_LaserValue.TabIndex = 158;
            // 
            // btnHighSensor_Live
            // 
            this.btnHighSensor_Live.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHighSensor_Live.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighSensor_Live.Location = new System.Drawing.Point(124, 87);
            this.btnHighSensor_Live.Name = "btnHighSensor_Live";
            this.btnHighSensor_Live.Size = new System.Drawing.Size(115, 45);
            this.btnHighSensor_Live.TabIndex = 159;
            this.btnHighSensor_Live.Text = "Live";
            this.btnHighSensor_Live.UseVisualStyleBackColor = true;
            this.btnHighSensor_Live.Click += new System.EventHandler(this.btnHighSensor_Live_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 16);
            this.label1.TabIndex = 160;
            this.label1.Text = "Laser Value (mm) :";
            // 
            // btnHighSensor_Connect
            // 
            this.btnHighSensor_Connect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHighSensor_Connect.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighSensor_Connect.Location = new System.Drawing.Point(3, 189);
            this.btnHighSensor_Connect.Name = "btnHighSensor_Connect";
            this.btnHighSensor_Connect.Size = new System.Drawing.Size(115, 45);
            this.btnHighSensor_Connect.TabIndex = 161;
            this.btnHighSensor_Connect.Text = "Connect";
            this.btnHighSensor_Connect.UseVisualStyleBackColor = true;
            this.btnHighSensor_Connect.Click += new System.EventHandler(this.btnHighSensor_Connect_Click);
            // 
            // btnHighSensor_ResetZero
            // 
            this.btnHighSensor_ResetZero.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHighSensor_ResetZero.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighSensor_ResetZero.Location = new System.Drawing.Point(3, 138);
            this.btnHighSensor_ResetZero.Name = "btnHighSensor_ResetZero";
            this.btnHighSensor_ResetZero.Size = new System.Drawing.Size(115, 45);
            this.btnHighSensor_ResetZero.TabIndex = 162;
            this.btnHighSensor_ResetZero.Text = "Reset Zero";
            this.btnHighSensor_ResetZero.UseVisualStyleBackColor = true;
            this.btnHighSensor_ResetZero.Click += new System.EventHandler(this.btnHighSensor_ResetZero_Click);
            // 
            // btnHighSensor_ClearOffset
            // 
            this.btnHighSensor_ClearOffset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHighSensor_ClearOffset.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighSensor_ClearOffset.Location = new System.Drawing.Point(124, 138);
            this.btnHighSensor_ClearOffset.Name = "btnHighSensor_ClearOffset";
            this.btnHighSensor_ClearOffset.Size = new System.Drawing.Size(115, 45);
            this.btnHighSensor_ClearOffset.TabIndex = 163;
            this.btnHighSensor_ClearOffset.Text = "Clear Offset";
            this.btnHighSensor_ClearOffset.UseVisualStyleBackColor = true;
            this.btnHighSensor_ClearOffset.Click += new System.EventHandler(this.btnHighSensor_ClearOffset_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 16);
            this.label2.TabIndex = 165;
            this.label2.Text = "Delay (ms) :";
            // 
            // txtHighSensor_Delay_ms
            // 
            this.txtHighSensor_Delay_ms.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHighSensor_Delay_ms.Location = new System.Drawing.Point(3, 258);
            this.txtHighSensor_Delay_ms.Name = "txtHighSensor_Delay_ms";
            this.txtHighSensor_Delay_ms.Size = new System.Drawing.Size(115, 27);
            this.txtHighSensor_Delay_ms.TabIndex = 164;
            this.txtHighSensor_Delay_ms.Text = "0";
            this.txtHighSensor_Delay_ms.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHighSensor_Delay_ms.Click += new System.EventHandler(this.txtHighSensor_Delay_ms_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(124, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 16);
            this.label3.TabIndex = 167;
            this.label3.Text = "Timeout (ms) :";
            // 
            // txtHighSensor_Timeout_ms
            // 
            this.txtHighSensor_Timeout_ms.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHighSensor_Timeout_ms.Location = new System.Drawing.Point(124, 258);
            this.txtHighSensor_Timeout_ms.Name = "txtHighSensor_Timeout_ms";
            this.txtHighSensor_Timeout_ms.Size = new System.Drawing.Size(115, 27);
            this.txtHighSensor_Timeout_ms.TabIndex = 166;
            this.txtHighSensor_Timeout_ms.Text = "0";
            this.txtHighSensor_Timeout_ms.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHighSensor_Timeout_ms.Click += new System.EventHandler(this.txtHighSensor_Timeout_ms_Click);
            // 
            // btnHighSensor_Disconnect
            // 
            this.btnHighSensor_Disconnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHighSensor_Disconnect.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighSensor_Disconnect.Location = new System.Drawing.Point(124, 189);
            this.btnHighSensor_Disconnect.Name = "btnHighSensor_Disconnect";
            this.btnHighSensor_Disconnect.Size = new System.Drawing.Size(115, 45);
            this.btnHighSensor_Disconnect.TabIndex = 168;
            this.btnHighSensor_Disconnect.Text = "Disconnect";
            this.btnHighSensor_Disconnect.UseVisualStyleBackColor = true;
            this.btnHighSensor_Disconnect.Click += new System.EventHandler(this.btnHighSensor_Disconnect_Click);
            // 
            // btnHighSensor_Bypass
            // 
            this.btnHighSensor_Bypass.BackgroundImage = global::ArtSystem.Properties.Resources.No;
            this.btnHighSensor_Bypass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHighSensor_Bypass.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighSensor_Bypass.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHighSensor_Bypass.Location = new System.Drawing.Point(3, 291);
            this.btnHighSensor_Bypass.Name = "btnHighSensor_Bypass";
            this.btnHighSensor_Bypass.Size = new System.Drawing.Size(115, 45);
            this.btnHighSensor_Bypass.TabIndex = 169;
            this.btnHighSensor_Bypass.Text = "By Pass";
            this.btnHighSensor_Bypass.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHighSensor_Bypass.UseVisualStyleBackColor = true;
            this.btnHighSensor_Bypass.Click += new System.EventHandler(this.btnHighSensor_Bypass_Click);
            // 
            // btnHighSensor_Simulate
            // 
            this.btnHighSensor_Simulate.BackgroundImage = global::ArtSystem.Properties.Resources.No;
            this.btnHighSensor_Simulate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHighSensor_Simulate.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighSensor_Simulate.Location = new System.Drawing.Point(124, 291);
            this.btnHighSensor_Simulate.Name = "btnHighSensor_Simulate";
            this.btnHighSensor_Simulate.Size = new System.Drawing.Size(115, 45);
            this.btnHighSensor_Simulate.TabIndex = 170;
            this.btnHighSensor_Simulate.Text = "Simulate";
            this.btnHighSensor_Simulate.UseVisualStyleBackColor = true;
            this.btnHighSensor_Simulate.Click += new System.EventHandler(this.btnHighSensor_Simulate_Click);
            // 
            // ucCtrlHighSensor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.btnHighSensor_Simulate);
            this.Controls.Add(this.btnHighSensor_Bypass);
            this.Controls.Add(this.btnHighSensor_Disconnect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHighSensor_Timeout_ms);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHighSensor_Delay_ms);
            this.Controls.Add(this.btnHighSensor_ClearOffset);
            this.Controls.Add(this.btnHighSensor_ResetZero);
            this.Controls.Add(this.btnHighSensor_Connect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHighSensor_Live);
            this.Controls.Add(this.txt_LaserValue);
            this.Controls.Add(this.btnHighSensor_Read);
            this.Controls.Add(this.labName);
            this.Name = "ucCtrlHighSensor";
            this.Size = new System.Drawing.Size(244, 339);
            this.VisibleChanged += new System.EventHandler(this.ucCtrlHighSensor_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Button btnHighSensor_Read;
        private System.Windows.Forms.TextBox txt_LaserValue;
        private System.Windows.Forms.Button btnHighSensor_Live;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHighSensor_Connect;
        private System.Windows.Forms.Button btnHighSensor_ResetZero;
        private System.Windows.Forms.Button btnHighSensor_ClearOffset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtHighSensor_Delay_ms;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHighSensor_Timeout_ms;
        private System.Windows.Forms.Button btnHighSensor_Disconnect;
        private System.Windows.Forms.Button btnHighSensor_Bypass;
        private System.Windows.Forms.Button btnHighSensor_Simulate;




    }
}

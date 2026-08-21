namespace ArtSystem.MultiSystem
{
    partial class ucCtrlTCPLink
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
            this.btnTCPLink_Send = new System.Windows.Forms.Button();
            this.txt_ReciveData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTCPLink_Connect = new System.Windows.Forms.Button();
            this.btnTCPLink_Disconnect = new System.Windows.Forms.Button();
            this.btnTCPLink_Clear = new System.Windows.Forms.Button();
            this.txtSendData = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTCPIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTCPPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labStation = new System.Windows.Forms.Label();
            this.btnTCPLink_Simulate = new System.Windows.Forms.Button();
            this.btnTCPLink_Bypass = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTCPLink_Timeout_ms = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTCPLink_Delay_ms = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labName
            // 
            this.labName.BackColor = System.Drawing.Color.DarkGray;
            this.labName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labName.Location = new System.Drawing.Point(69, 2);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(171, 26);
            this.labName.TabIndex = 153;
            this.labName.Text = "Device Name";
            this.labName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTCPLink_Send
            // 
            this.btnTCPLink_Send.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTCPLink_Send.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTCPLink_Send.Location = new System.Drawing.Point(164, 87);
            this.btnTCPLink_Send.Name = "btnTCPLink_Send";
            this.btnTCPLink_Send.Size = new System.Drawing.Size(75, 45);
            this.btnTCPLink_Send.TabIndex = 157;
            this.btnTCPLink_Send.Text = "Send";
            this.btnTCPLink_Send.UseVisualStyleBackColor = true;
            this.btnTCPLink_Send.Click += new System.EventHandler(this.btnTCPLink_Send_Click);
            // 
            // txt_ReciveData
            // 
            this.txt_ReciveData.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ReciveData.Location = new System.Drawing.Point(3, 54);
            this.txt_ReciveData.Name = "txt_ReciveData";
            this.txt_ReciveData.Size = new System.Drawing.Size(155, 27);
            this.txt_ReciveData.TabIndex = 158;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 160;
            this.label1.Text = "Recive Data";
            // 
            // btnTCPLink_Connect
            // 
            this.btnTCPLink_Connect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTCPLink_Connect.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTCPLink_Connect.Location = new System.Drawing.Point(3, 187);
            this.btnTCPLink_Connect.Name = "btnTCPLink_Connect";
            this.btnTCPLink_Connect.Size = new System.Drawing.Size(115, 45);
            this.btnTCPLink_Connect.TabIndex = 161;
            this.btnTCPLink_Connect.Text = "Connect";
            this.btnTCPLink_Connect.UseVisualStyleBackColor = true;
            this.btnTCPLink_Connect.Click += new System.EventHandler(this.btnTCPLink_Connect_Click);
            // 
            // btnTCPLink_Disconnect
            // 
            this.btnTCPLink_Disconnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTCPLink_Disconnect.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTCPLink_Disconnect.Location = new System.Drawing.Point(124, 187);
            this.btnTCPLink_Disconnect.Name = "btnTCPLink_Disconnect";
            this.btnTCPLink_Disconnect.Size = new System.Drawing.Size(115, 45);
            this.btnTCPLink_Disconnect.TabIndex = 163;
            this.btnTCPLink_Disconnect.Text = "Disconnect";
            this.btnTCPLink_Disconnect.UseVisualStyleBackColor = true;
            this.btnTCPLink_Disconnect.Click += new System.EventHandler(this.btnTCPLink_Disconnect_Click);
            // 
            // btnTCPLink_Clear
            // 
            this.btnTCPLink_Clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTCPLink_Clear.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTCPLink_Clear.Location = new System.Drawing.Point(164, 36);
            this.btnTCPLink_Clear.Name = "btnTCPLink_Clear";
            this.btnTCPLink_Clear.Size = new System.Drawing.Size(75, 45);
            this.btnTCPLink_Clear.TabIndex = 164;
            this.btnTCPLink_Clear.Text = "Clear";
            this.btnTCPLink_Clear.UseVisualStyleBackColor = true;
            this.btnTCPLink_Clear.Click += new System.EventHandler(this.btnTCPLink_Clear_Click);
            // 
            // txtSendData
            // 
            this.txtSendData.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSendData.Location = new System.Drawing.Point(3, 105);
            this.txtSendData.Name = "txtSendData";
            this.txtSendData.Size = new System.Drawing.Size(155, 27);
            this.txtSendData.TabIndex = 165;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 166;
            this.label2.Text = "Send Data";
            // 
            // txtTCPIP
            // 
            this.txtTCPIP.BackColor = System.Drawing.Color.White;
            this.txtTCPIP.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTCPIP.Location = new System.Drawing.Point(3, 154);
            this.txtTCPIP.Name = "txtTCPIP";
            this.txtTCPIP.ReadOnly = true;
            this.txtTCPIP.Size = new System.Drawing.Size(155, 27);
            this.txtTCPIP.TabIndex = 167;
            this.txtTCPIP.Click += new System.EventHandler(this.txtTCPIP_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 168;
            this.label3.Text = "TCP IP";
            // 
            // txtTCPPort
            // 
            this.txtTCPPort.BackColor = System.Drawing.Color.White;
            this.txtTCPPort.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTCPPort.Location = new System.Drawing.Point(164, 154);
            this.txtTCPPort.Name = "txtTCPPort";
            this.txtTCPPort.ReadOnly = true;
            this.txtTCPPort.Size = new System.Drawing.Size(75, 27);
            this.txtTCPPort.TabIndex = 169;
            this.txtTCPPort.Click += new System.EventHandler(this.txtTCPPort_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(164, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 170;
            this.label4.Text = "TCP Port";
            // 
            // labStation
            // 
            this.labStation.BackColor = System.Drawing.Color.Black;
            this.labStation.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStation.ForeColor = System.Drawing.Color.Lime;
            this.labStation.Location = new System.Drawing.Point(3, 2);
            this.labStation.Name = "labStation";
            this.labStation.Size = new System.Drawing.Size(68, 26);
            this.labStation.TabIndex = 171;
            this.labStation.Text = "Client";
            this.labStation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTCPLink_Simulate
            // 
            this.btnTCPLink_Simulate.BackgroundImage = global::ArtSystem.Properties.Resources.No;
            this.btnTCPLink_Simulate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTCPLink_Simulate.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTCPLink_Simulate.Location = new System.Drawing.Point(124, 288);
            this.btnTCPLink_Simulate.Name = "btnTCPLink_Simulate";
            this.btnTCPLink_Simulate.Size = new System.Drawing.Size(115, 45);
            this.btnTCPLink_Simulate.TabIndex = 174;
            this.btnTCPLink_Simulate.Text = "Simulate";
            this.btnTCPLink_Simulate.UseVisualStyleBackColor = true;
            this.btnTCPLink_Simulate.Click += new System.EventHandler(this.btnTCPLink_Simulate_Click);
            // 
            // btnTCPLink_Bypass
            // 
            this.btnTCPLink_Bypass.BackgroundImage = global::ArtSystem.Properties.Resources.No;
            this.btnTCPLink_Bypass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTCPLink_Bypass.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTCPLink_Bypass.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTCPLink_Bypass.Location = new System.Drawing.Point(3, 288);
            this.btnTCPLink_Bypass.Name = "btnTCPLink_Bypass";
            this.btnTCPLink_Bypass.Size = new System.Drawing.Size(115, 45);
            this.btnTCPLink_Bypass.TabIndex = 173;
            this.btnTCPLink_Bypass.Text = "By Pass";
            this.btnTCPLink_Bypass.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTCPLink_Bypass.UseVisualStyleBackColor = true;
            this.btnTCPLink_Bypass.Click += new System.EventHandler(this.btnTCPLink_Bypass_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(124, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 16);
            this.label5.TabIndex = 178;
            this.label5.Text = "Timeout (ms) :";
            // 
            // txtTCPLink_Timeout_ms
            // 
            this.txtTCPLink_Timeout_ms.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTCPLink_Timeout_ms.Location = new System.Drawing.Point(124, 255);
            this.txtTCPLink_Timeout_ms.Name = "txtTCPLink_Timeout_ms";
            this.txtTCPLink_Timeout_ms.Size = new System.Drawing.Size(115, 27);
            this.txtTCPLink_Timeout_ms.TabIndex = 177;
            this.txtTCPLink_Timeout_ms.Text = "0";
            this.txtTCPLink_Timeout_ms.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTCPLink_Timeout_ms.Click += new System.EventHandler(this.txtTCP_Timout_ms_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 16);
            this.label6.TabIndex = 176;
            this.label6.Text = "Delay (ms) :";
            // 
            // txtTCPLink_Delay_ms
            // 
            this.txtTCPLink_Delay_ms.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTCPLink_Delay_ms.Location = new System.Drawing.Point(3, 255);
            this.txtTCPLink_Delay_ms.Name = "txtTCPLink_Delay_ms";
            this.txtTCPLink_Delay_ms.Size = new System.Drawing.Size(115, 27);
            this.txtTCPLink_Delay_ms.TabIndex = 175;
            this.txtTCPLink_Delay_ms.Text = "0";
            this.txtTCPLink_Delay_ms.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTCPLink_Delay_ms.Click += new System.EventHandler(this.txtTCP_Delay_ms_Click);
            // 
            // ucCtrlTCPLink
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTCPLink_Timeout_ms);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTCPLink_Delay_ms);
            this.Controls.Add(this.btnTCPLink_Simulate);
            this.Controls.Add(this.btnTCPLink_Bypass);
            this.Controls.Add(this.labStation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTCPPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTCPIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSendData);
            this.Controls.Add(this.btnTCPLink_Clear);
            this.Controls.Add(this.btnTCPLink_Disconnect);
            this.Controls.Add(this.btnTCPLink_Connect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_ReciveData);
            this.Controls.Add(this.btnTCPLink_Send);
            this.Controls.Add(this.labName);
            this.Name = "ucCtrlTCPLink";
            this.Size = new System.Drawing.Size(244, 337);
            this.VisibleChanged += new System.EventHandler(this.ucCtrlTCPLink_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Button btnTCPLink_Send;
        private System.Windows.Forms.TextBox txt_ReciveData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTCPLink_Connect;
        private System.Windows.Forms.Button btnTCPLink_Disconnect;
        private System.Windows.Forms.Button btnTCPLink_Clear;
        private System.Windows.Forms.TextBox txtSendData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTCPIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTCPPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labStation;
        private System.Windows.Forms.Button btnTCPLink_Simulate;
        private System.Windows.Forms.Button btnTCPLink_Bypass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTCPLink_Timeout_ms;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTCPLink_Delay_ms;




    }
}

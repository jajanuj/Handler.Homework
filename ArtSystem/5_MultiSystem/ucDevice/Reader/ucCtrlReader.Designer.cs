namespace ArtSystem.MultiSystem
{
    partial class ucCtrlReader
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
            this.btnReader_Read = new System.Windows.Forms.Button();
            this.txt_ReciveData = new System.Windows.Forms.TextBox();
            this.btnReader_Stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReader_Connect = new System.Windows.Forms.Button();
            this.btnReader_Reset = new System.Windows.Forms.Button();
            this.btnReader_Disconnect = new System.Windows.Forms.Button();
            this.btnReader_Simulate = new System.Windows.Forms.Button();
            this.btnReader_Bypass = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReader_Timeout_ms = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReader_Delay_ms = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labName
            // 
            this.labName.BackColor = System.Drawing.Color.DarkGray;
            this.labName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labName.Location = new System.Drawing.Point(3, 2);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(237, 26);
            this.labName.TabIndex = 153;
            this.labName.Text = "Reader Name";
            this.labName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReader_Read
            // 
            this.btnReader_Read.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReader_Read.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReader_Read.Location = new System.Drawing.Point(3, 87);
            this.btnReader_Read.Name = "btnReader_Read";
            this.btnReader_Read.Size = new System.Drawing.Size(75, 45);
            this.btnReader_Read.TabIndex = 157;
            this.btnReader_Read.Text = "Read";
            this.btnReader_Read.UseVisualStyleBackColor = true;
            this.btnReader_Read.Click += new System.EventHandler(this.btnReader_Read_Click);
            // 
            // txt_ReciveData
            // 
            this.txt_ReciveData.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ReciveData.Location = new System.Drawing.Point(3, 54);
            this.txt_ReciveData.Name = "txt_ReciveData";
            this.txt_ReciveData.Size = new System.Drawing.Size(237, 23);
            this.txt_ReciveData.TabIndex = 158;
            // 
            // btnReader_Stop
            // 
            this.btnReader_Stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReader_Stop.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReader_Stop.Location = new System.Drawing.Point(84, 87);
            this.btnReader_Stop.Name = "btnReader_Stop";
            this.btnReader_Stop.Size = new System.Drawing.Size(75, 45);
            this.btnReader_Stop.TabIndex = 159;
            this.btnReader_Stop.Text = "Stop";
            this.btnReader_Stop.UseVisualStyleBackColor = true;
            this.btnReader_Stop.Click += new System.EventHandler(this.btnReader_Stop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 160;
            this.label1.Text = "Recive Data";
            // 
            // btnReader_Connect
            // 
            this.btnReader_Connect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReader_Connect.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReader_Connect.Location = new System.Drawing.Point(3, 138);
            this.btnReader_Connect.Name = "btnReader_Connect";
            this.btnReader_Connect.Size = new System.Drawing.Size(115, 45);
            this.btnReader_Connect.TabIndex = 161;
            this.btnReader_Connect.Text = "Connect";
            this.btnReader_Connect.UseVisualStyleBackColor = true;
            this.btnReader_Connect.Click += new System.EventHandler(this.btnReader_Connect_Click);
            // 
            // btnReader_Reset
            // 
            this.btnReader_Reset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReader_Reset.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReader_Reset.Location = new System.Drawing.Point(165, 87);
            this.btnReader_Reset.Name = "btnReader_Reset";
            this.btnReader_Reset.Size = new System.Drawing.Size(75, 45);
            this.btnReader_Reset.TabIndex = 162;
            this.btnReader_Reset.Text = "Reset";
            this.btnReader_Reset.UseVisualStyleBackColor = true;
            this.btnReader_Reset.Click += new System.EventHandler(this.btnReader_Reset_Click);
            // 
            // btnReader_Disconnect
            // 
            this.btnReader_Disconnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReader_Disconnect.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReader_Disconnect.Location = new System.Drawing.Point(124, 138);
            this.btnReader_Disconnect.Name = "btnReader_Disconnect";
            this.btnReader_Disconnect.Size = new System.Drawing.Size(115, 45);
            this.btnReader_Disconnect.TabIndex = 163;
            this.btnReader_Disconnect.Text = "Disconnect";
            this.btnReader_Disconnect.UseVisualStyleBackColor = true;
            this.btnReader_Disconnect.Click += new System.EventHandler(this.btnReader_Disconnect_Click);
            // 
            // btnReader_Simulate
            // 
            this.btnReader_Simulate.BackgroundImage = global::ArtSystem.Properties.Resources.No;
            this.btnReader_Simulate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReader_Simulate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReader_Simulate.Location = new System.Drawing.Point(124, 242);
            this.btnReader_Simulate.Name = "btnReader_Simulate";
            this.btnReader_Simulate.Size = new System.Drawing.Size(115, 45);
            this.btnReader_Simulate.TabIndex = 172;
            this.btnReader_Simulate.Text = "Simulate";
            this.btnReader_Simulate.UseVisualStyleBackColor = true;
            this.btnReader_Simulate.Click += new System.EventHandler(this.btnReader_Simulate_Click);
            // 
            // btnReader_Bypass
            // 
            this.btnReader_Bypass.BackgroundImage = global::ArtSystem.Properties.Resources.No;
            this.btnReader_Bypass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReader_Bypass.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReader_Bypass.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReader_Bypass.Location = new System.Drawing.Point(3, 242);
            this.btnReader_Bypass.Name = "btnReader_Bypass";
            this.btnReader_Bypass.Size = new System.Drawing.Size(115, 45);
            this.btnReader_Bypass.TabIndex = 171;
            this.btnReader_Bypass.Text = "By Pass";
            this.btnReader_Bypass.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReader_Bypass.UseVisualStyleBackColor = true;
            this.btnReader_Bypass.Click += new System.EventHandler(this.btnReader_Bypass_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(124, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 16);
            this.label3.TabIndex = 176;
            this.label3.Text = "Timeout (ms) :";
            // 
            // txtReader_Timeout_ms
            // 
            this.txtReader_Timeout_ms.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReader_Timeout_ms.Location = new System.Drawing.Point(124, 209);
            this.txtReader_Timeout_ms.Name = "txtReader_Timeout_ms";
            this.txtReader_Timeout_ms.Size = new System.Drawing.Size(115, 23);
            this.txtReader_Timeout_ms.TabIndex = 175;
            this.txtReader_Timeout_ms.Text = "0";
            this.txtReader_Timeout_ms.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtReader_Timeout_ms.Click += new System.EventHandler(this.txtReader_Timeout_ms_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 16);
            this.label2.TabIndex = 174;
            this.label2.Text = "Delay (ms) :";
            // 
            // txtReader_Delay_ms
            // 
            this.txtReader_Delay_ms.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReader_Delay_ms.Location = new System.Drawing.Point(3, 209);
            this.txtReader_Delay_ms.Name = "txtReader_Delay_ms";
            this.txtReader_Delay_ms.Size = new System.Drawing.Size(115, 23);
            this.txtReader_Delay_ms.TabIndex = 173;
            this.txtReader_Delay_ms.Text = "0";
            this.txtReader_Delay_ms.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtReader_Delay_ms.Click += new System.EventHandler(this.txtReader_Delay_ms_Click);
            // 
            // ucCtrlReader
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtReader_Timeout_ms);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtReader_Delay_ms);
            this.Controls.Add(this.btnReader_Simulate);
            this.Controls.Add(this.btnReader_Bypass);
            this.Controls.Add(this.btnReader_Disconnect);
            this.Controls.Add(this.btnReader_Reset);
            this.Controls.Add(this.btnReader_Connect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReader_Stop);
            this.Controls.Add(this.txt_ReciveData);
            this.Controls.Add(this.btnReader_Read);
            this.Controls.Add(this.labName);
            this.Name = "ucCtrlReader";
            this.Size = new System.Drawing.Size(244, 292);
            this.VisibleChanged += new System.EventHandler(this.ucCtrlReader_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Button btnReader_Read;
        private System.Windows.Forms.TextBox txt_ReciveData;
        private System.Windows.Forms.Button btnReader_Stop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReader_Connect;
        private System.Windows.Forms.Button btnReader_Reset;
        private System.Windows.Forms.Button btnReader_Disconnect;
        private System.Windows.Forms.Button btnReader_Simulate;
        private System.Windows.Forms.Button btnReader_Bypass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReader_Timeout_ms;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReader_Delay_ms;




    }
}

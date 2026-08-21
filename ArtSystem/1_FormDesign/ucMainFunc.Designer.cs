namespace ArtSystem.FormDesign
{
    partial class ucMainFunc
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
            this.btnOperation = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Label();
            this.btnAlarm = new System.Windows.Forms.Label();
            this.btnLog = new System.Windows.Forms.Label();
            this.btnSoftware = new System.Windows.Forms.Label();
            this.btnHardware = new System.Windows.Forms.Label();
            this.btnManual = new System.Windows.Forms.Label();
            this.lbAlarm = new System.Windows.Forms.Label();
            this.SelectedColor = new System.Windows.Forms.Label();
            this.MouseOn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOperation
            // 
            this.btnOperation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.btnOperation.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOperation.ForeColor = System.Drawing.Color.White;
            this.btnOperation.Image = global::ArtSystem.Properties.Resources.right_01;
            this.btnOperation.Location = new System.Drawing.Point(3, 0);
            this.btnOperation.Name = "btnOperation";
            this.btnOperation.Size = new System.Drawing.Size(120, 64);
            this.btnOperation.TabIndex = 9;
            this.btnOperation.Text = "Operation";
            this.btnOperation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOperation.Click += new System.EventHandler(this.MainFuncManage);
            this.btnOperation.MouseEnter += new System.EventHandler(this.btnControls_MouseEnter);
            this.btnOperation.MouseLeave += new System.EventHandler(this.btnControls_MouseLeave);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = global::ArtSystem.Properties.Resources.right_07;
            this.btnExit.Location = new System.Drawing.Point(816, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 64);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.MouseEnter += new System.EventHandler(this.btnControls_MouseEnter);
            this.btnExit.MouseLeave += new System.EventHandler(this.btnControls_MouseLeave);
            // 
            // btnAlarm
            // 
            this.btnAlarm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.btnAlarm.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlarm.ForeColor = System.Drawing.Color.White;
            this.btnAlarm.Image = global::ArtSystem.Properties.Resources.right_06;
            this.btnAlarm.Location = new System.Drawing.Point(690, 2);
            this.btnAlarm.Name = "btnAlarm";
            this.btnAlarm.Size = new System.Drawing.Size(120, 64);
            this.btnAlarm.TabIndex = 20;
            this.btnAlarm.Text = "Alarm Report";
            this.btnAlarm.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAlarm.Click += new System.EventHandler(this.btnAlarm_Click);
            this.btnAlarm.MouseEnter += new System.EventHandler(this.btnControls_MouseEnter);
            this.btnAlarm.MouseLeave += new System.EventHandler(this.btnControls_MouseLeave);
            // 
            // btnLog
            // 
            this.btnLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.btnLog.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLog.ForeColor = System.Drawing.Color.White;
            this.btnLog.Image = global::ArtSystem.Properties.Resources.right_05;
            this.btnLog.Location = new System.Drawing.Point(512, 2);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(120, 64);
            this.btnLog.TabIndex = 19;
            this.btnLog.Text = "Log";
            this.btnLog.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLog.Click += new System.EventHandler(this.MainFuncManage);
            this.btnLog.MouseEnter += new System.EventHandler(this.btnControls_MouseEnter);
            this.btnLog.MouseLeave += new System.EventHandler(this.btnControls_MouseLeave);
            // 
            // btnSoftware
            // 
            this.btnSoftware.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.btnSoftware.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSoftware.ForeColor = System.Drawing.Color.White;
            this.btnSoftware.Image = global::ArtSystem.Properties.Resources.right_04;
            this.btnSoftware.Location = new System.Drawing.Point(389, 2);
            this.btnSoftware.Name = "btnSoftware";
            this.btnSoftware.Size = new System.Drawing.Size(120, 64);
            this.btnSoftware.TabIndex = 18;
            this.btnSoftware.Text = "Software Setup";
            this.btnSoftware.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSoftware.Click += new System.EventHandler(this.MainFuncManage);
            this.btnSoftware.MouseEnter += new System.EventHandler(this.btnControls_MouseEnter);
            this.btnSoftware.MouseLeave += new System.EventHandler(this.btnControls_MouseLeave);
            // 
            // btnHardware
            // 
            this.btnHardware.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.btnHardware.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHardware.ForeColor = System.Drawing.Color.White;
            this.btnHardware.Image = global::ArtSystem.Properties.Resources.right_03;
            this.btnHardware.Location = new System.Drawing.Point(266, 2);
            this.btnHardware.Name = "btnHardware";
            this.btnHardware.Size = new System.Drawing.Size(120, 64);
            this.btnHardware.TabIndex = 17;
            this.btnHardware.Text = "Hardware Setup";
            this.btnHardware.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHardware.Click += new System.EventHandler(this.MainFuncManage);
            this.btnHardware.MouseEnter += new System.EventHandler(this.btnControls_MouseEnter);
            this.btnHardware.MouseLeave += new System.EventHandler(this.btnControls_MouseLeave);
            // 
            // btnManual
            // 
            this.btnManual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.btnManual.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManual.ForeColor = System.Drawing.Color.White;
            this.btnManual.Image = global::ArtSystem.Properties.Resources.right_02;
            this.btnManual.Location = new System.Drawing.Point(140, 2);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(120, 64);
            this.btnManual.TabIndex = 16;
            this.btnManual.Text = "Manual Mode";
            this.btnManual.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnManual.Click += new System.EventHandler(this.MainFuncManage);
            this.btnManual.MouseEnter += new System.EventHandler(this.btnControls_MouseEnter);
            this.btnManual.MouseLeave += new System.EventHandler(this.btnControls_MouseLeave);
            // 
            // lbAlarm
            // 
            this.lbAlarm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.lbAlarm.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbAlarm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbAlarm.Location = new System.Drawing.Point(780, 4);
            this.lbAlarm.Name = "lbAlarm";
            this.lbAlarm.Size = new System.Drawing.Size(41, 64);
            this.lbAlarm.TabIndex = 24;
            this.lbAlarm.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // SelectedColor
            // 
            this.SelectedColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.SelectedColor.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.SelectedColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.SelectedColor.Location = new System.Drawing.Point(610, 2);
            this.SelectedColor.Name = "SelectedColor";
            this.SelectedColor.Size = new System.Drawing.Size(41, 64);
            this.SelectedColor.TabIndex = 22;
            this.SelectedColor.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // MouseOn
            // 
            this.MouseOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.MouseOn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.MouseOn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.MouseOn.Location = new System.Drawing.Point(643, 4);
            this.MouseOn.Name = "MouseOn";
            this.MouseOn.Size = new System.Drawing.Size(41, 64);
            this.MouseOn.TabIndex = 23;
            this.MouseOn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // ucMainFunc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.Controls.Add(this.lbAlarm);
            this.Controls.Add(this.SelectedColor);
            this.Controls.Add(this.MouseOn);
            this.Controls.Add(this.btnAlarm);
            this.Controls.Add(this.btnLog);
            this.Controls.Add(this.btnSoftware);
            this.Controls.Add(this.btnHardware);
            this.Controls.Add(this.btnManual);
            this.Controls.Add(this.btnOperation);
            this.Controls.Add(this.btnExit);
            this.Name = "ucMainFunc";
            this.Size = new System.Drawing.Size(950, 69);
            this.Load += new System.EventHandler(this.ucMainFunc_Load);
            this.SizeChanged += new System.EventHandler(this.ucMainFunc_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label btnOperation;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.Label btnAlarm;
        private System.Windows.Forms.Label btnLog;
        private System.Windows.Forms.Label btnSoftware;
        private System.Windows.Forms.Label btnHardware;
        private System.Windows.Forms.Label btnManual;
        private System.Windows.Forms.Label lbAlarm;
        private System.Windows.Forms.Label SelectedColor;
        private System.Windows.Forms.Label MouseOn;
    }
}

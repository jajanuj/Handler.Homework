namespace ArtSystem
{
    partial class ucSignalTowerIOSetting
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
            this.panel_SignalTower = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_LEDRed = new System.Windows.Forms.TextBox();
            this.txt_Bizzer2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_Bizzer1 = new System.Windows.Forms.TextBox();
            this.txt_LEDGreen = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_LEDYellow = new System.Windows.Forms.TextBox();
            this.txt_LEDBlue = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSave_SignalTower = new System.Windows.Forms.Button();
            this.btnEdit_SignalTower = new System.Windows.Forms.Button();
            this.btnCancel_SignalTower = new System.Windows.Forms.Button();
            this.panel_SignalTower.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_SignalTower
            // 
            this.panel_SignalTower.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_SignalTower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_SignalTower.Controls.Add(this.panel1);
            this.panel_SignalTower.Controls.Add(this.groupBox1);
            this.panel_SignalTower.Location = new System.Drawing.Point(3, 61);
            this.panel_SignalTower.Name = "panel_SignalTower";
            this.panel_SignalTower.Size = new System.Drawing.Size(1128, 677);
            this.panel_SignalTower.TabIndex = 29;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(313, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(810, 669);
            this.panel1.TabIndex = 43;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_LEDRed);
            this.groupBox1.Controls.Add(this.txt_Bizzer2);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txt_Bizzer1);
            this.groupBox1.Controls.Add(this.txt_LEDGreen);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txt_LEDYellow);
            this.groupBox1.Controls.Add(this.txt_LEDBlue);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 669);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Do ID Setting";
            // 
            // txt_LEDRed
            // 
            this.txt_LEDRed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_LEDRed.BackColor = System.Drawing.Color.White;
            this.txt_LEDRed.Location = new System.Drawing.Point(6, 74);
            this.txt_LEDRed.Name = "txt_LEDRed";
            this.txt_LEDRed.ReadOnly = true;
            this.txt_LEDRed.Size = new System.Drawing.Size(292, 23);
            this.txt_LEDRed.TabIndex = 30;
            this.txt_LEDRed.DoubleClick += new System.EventHandler(this.txt_LEDRed_DoubleClick);
            // 
            // txt_Bizzer2
            // 
            this.txt_Bizzer2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Bizzer2.BackColor = System.Drawing.Color.White;
            this.txt_Bizzer2.Location = new System.Drawing.Point(6, 304);
            this.txt_Bizzer2.Name = "txt_Bizzer2";
            this.txt_Bizzer2.ReadOnly = true;
            this.txt_Bizzer2.Size = new System.Drawing.Size(292, 23);
            this.txt_Bizzer2.TabIndex = 40;
            this.txt_Bizzer2.DoubleClick += new System.EventHandler(this.txt_LEDRed_DoubleClick);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label13.Location = new System.Drawing.Point(3, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 16);
            this.label13.TabIndex = 31;
            this.label13.Text = "LED Red (DO) :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label16.Location = new System.Drawing.Point(3, 285);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(101, 16);
            this.label16.TabIndex = 41;
            this.label16.Text = "Bizzer2 (DO) :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label14.Location = new System.Drawing.Point(3, 145);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(122, 16);
            this.label14.TabIndex = 33;
            this.label14.Text = "LED Green (DO) :";
            // 
            // txt_Bizzer1
            // 
            this.txt_Bizzer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Bizzer1.BackColor = System.Drawing.Color.White;
            this.txt_Bizzer1.Location = new System.Drawing.Point(6, 254);
            this.txt_Bizzer1.Name = "txt_Bizzer1";
            this.txt_Bizzer1.ReadOnly = true;
            this.txt_Bizzer1.Size = new System.Drawing.Size(292, 23);
            this.txt_Bizzer1.TabIndex = 38;
            this.txt_Bizzer1.DoubleClick += new System.EventHandler(this.txt_LEDRed_DoubleClick);
            // 
            // txt_LEDGreen
            // 
            this.txt_LEDGreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_LEDGreen.BackColor = System.Drawing.Color.White;
            this.txt_LEDGreen.Location = new System.Drawing.Point(6, 164);
            this.txt_LEDGreen.Name = "txt_LEDGreen";
            this.txt_LEDGreen.ReadOnly = true;
            this.txt_LEDGreen.Size = new System.Drawing.Size(292, 23);
            this.txt_LEDGreen.TabIndex = 32;
            this.txt_LEDGreen.DoubleClick += new System.EventHandler(this.txt_LEDRed_DoubleClick);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label17.Location = new System.Drawing.Point(3, 235);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(101, 16);
            this.label17.TabIndex = 39;
            this.label17.Text = "Bizzer1 (DO) :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label15.Location = new System.Drawing.Point(3, 190);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(111, 16);
            this.label15.TabIndex = 35;
            this.label15.Text = "LED Blue (DO) :";
            // 
            // txt_LEDYellow
            // 
            this.txt_LEDYellow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_LEDYellow.BackColor = System.Drawing.Color.White;
            this.txt_LEDYellow.Location = new System.Drawing.Point(6, 119);
            this.txt_LEDYellow.Name = "txt_LEDYellow";
            this.txt_LEDYellow.ReadOnly = true;
            this.txt_LEDYellow.Size = new System.Drawing.Size(292, 23);
            this.txt_LEDYellow.TabIndex = 36;
            this.txt_LEDYellow.DoubleClick += new System.EventHandler(this.txt_LEDRed_DoubleClick);
            // 
            // txt_LEDBlue
            // 
            this.txt_LEDBlue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_LEDBlue.BackColor = System.Drawing.Color.White;
            this.txt_LEDBlue.Location = new System.Drawing.Point(6, 209);
            this.txt_LEDBlue.Name = "txt_LEDBlue";
            this.txt_LEDBlue.ReadOnly = true;
            this.txt_LEDBlue.Size = new System.Drawing.Size(292, 23);
            this.txt_LEDBlue.TabIndex = 34;
            this.txt_LEDBlue.DoubleClick += new System.EventHandler(this.txt_LEDRed_DoubleClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.label12.Location = new System.Drawing.Point(3, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(125, 16);
            this.label12.TabIndex = 37;
            this.label12.Text = "LED Yellow (DO) :";
            // 
            // btnSave_SignalTower
            // 
            this.btnSave_SignalTower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave_SignalTower.Location = new System.Drawing.Point(816, 3);
            this.btnSave_SignalTower.Name = "btnSave_SignalTower";
            this.btnSave_SignalTower.Size = new System.Drawing.Size(100, 52);
            this.btnSave_SignalTower.TabIndex = 28;
            this.btnSave_SignalTower.Text = "Save";
            this.btnSave_SignalTower.UseVisualStyleBackColor = true;
            this.btnSave_SignalTower.Click += new System.EventHandler(this.btnSave_SignalTower_Click);
            // 
            // btnEdit_SignalTower
            // 
            this.btnEdit_SignalTower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_SignalTower.Location = new System.Drawing.Point(1028, 3);
            this.btnEdit_SignalTower.Name = "btnEdit_SignalTower";
            this.btnEdit_SignalTower.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_SignalTower.TabIndex = 26;
            this.btnEdit_SignalTower.Text = "Edit";
            this.btnEdit_SignalTower.UseVisualStyleBackColor = true;
            this.btnEdit_SignalTower.Click += new System.EventHandler(this.btnEdit_SignalTower_Click);
            // 
            // btnCancel_SignalTower
            // 
            this.btnCancel_SignalTower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel_SignalTower.Location = new System.Drawing.Point(922, 3);
            this.btnCancel_SignalTower.Name = "btnCancel_SignalTower";
            this.btnCancel_SignalTower.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_SignalTower.TabIndex = 27;
            this.btnCancel_SignalTower.Text = "Cancel";
            this.btnCancel_SignalTower.UseVisualStyleBackColor = true;
            this.btnCancel_SignalTower.Click += new System.EventHandler(this.btnCancel_SignalTower_Click);
            // 
            // ucSignalTowerIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_SignalTower);
            this.Controls.Add(this.btnSave_SignalTower);
            this.Controls.Add(this.btnEdit_SignalTower);
            this.Controls.Add(this.btnCancel_SignalTower);
            this.Name = "ucSignalTowerIO";
            this.Size = new System.Drawing.Size(1131, 741);
            this.panel_SignalTower.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave_SignalTower;
        private System.Windows.Forms.Button btnEdit_SignalTower;
        private System.Windows.Forms.Button btnCancel_SignalTower;
        private System.Windows.Forms.Panel panel_SignalTower;
        private System.Windows.Forms.TextBox txt_Bizzer2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_Bizzer1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_LEDYellow;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_LEDBlue;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_LEDGreen;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_LEDRed;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
    }
}

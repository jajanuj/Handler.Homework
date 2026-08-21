namespace ArtSystem.MultiSystem
{
    partial class ucCtrlAIOTune
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.numBox_Shift = new System.Windows.Forms.Label();
            this.numBox_Gain = new System.Windows.Forms.Label();
            this.numBox_Offset = new System.Windows.Forms.Label();
            this.btnAIOTune = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.numBox_Pressure = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.numBox_Shift);
            this.panel1.Controls.Add(this.numBox_Gain);
            this.panel1.Controls.Add(this.numBox_Offset);
            this.panel1.Controls.Add(this.numBox_Pressure);
            this.panel1.Controls.Add(this.btnAIOTune);
            this.panel1.Controls.Add(this.txtID);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 289);
            this.panel1.TabIndex = 861;
            // 
            // numBox_Shift
            // 
            this.numBox_Shift.BackColor = System.Drawing.Color.White;
            this.numBox_Shift.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.numBox_Shift.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numBox_Shift.Location = new System.Drawing.Point(20, 200);
            this.numBox_Shift.Name = "numBox_Shift";
            this.numBox_Shift.Size = new System.Drawing.Size(195, 23);
            this.numBox_Shift.TabIndex = 867;
            this.numBox_Shift.Text = "0";
            this.numBox_Shift.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.numBox_Shift.Click += new System.EventHandler(this.numBox_Shift_Click);
            // 
            // numBox_Gain
            // 
            this.numBox_Gain.BackColor = System.Drawing.Color.White;
            this.numBox_Gain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.numBox_Gain.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numBox_Gain.Location = new System.Drawing.Point(20, 148);
            this.numBox_Gain.Name = "numBox_Gain";
            this.numBox_Gain.Size = new System.Drawing.Size(195, 23);
            this.numBox_Gain.TabIndex = 866;
            this.numBox_Gain.Text = "0";
            this.numBox_Gain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.numBox_Gain.Click += new System.EventHandler(this.numBox_Gain_Click);
            // 
            // numBox_Offset
            // 
            this.numBox_Offset.BackColor = System.Drawing.Color.White;
            this.numBox_Offset.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.numBox_Offset.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numBox_Offset.Location = new System.Drawing.Point(20, 96);
            this.numBox_Offset.Name = "numBox_Offset";
            this.numBox_Offset.Size = new System.Drawing.Size(195, 23);
            this.numBox_Offset.TabIndex = 865;
            this.numBox_Offset.Text = "0";
            this.numBox_Offset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.numBox_Offset.Click += new System.EventHandler(this.numBox_Offset_Click);
            // 
            // btnAIOTune
            // 
            this.btnAIOTune.Location = new System.Drawing.Point(20, 232);
            this.btnAIOTune.Name = "btnAIOTune";
            this.btnAIOTune.Size = new System.Drawing.Size(195, 45);
            this.btnAIOTune.TabIndex = 863;
            this.btnAIOTune.Text = "Tune";
            this.btnAIOTune.UseVisualStyleBackColor = true;
            this.btnAIOTune.Click += new System.EventHandler(this.btnAIOTune_Click);
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtID.ForeColor = System.Drawing.Color.Black;
            this.txtID.Location = new System.Drawing.Point(151, 64);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(80, 23);
            this.txtID.TabIndex = 862;
            this.txtID.Text = "Di100";
            this.txtID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(16, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 23);
            this.label3.TabIndex = 843;
            this.label3.Text = "Offset";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.Color.Navy;
            this.txtName.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(1, 1);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(230, 23);
            this.txtName.TabIndex = 847;
            this.txtName.Text = "Name";
            this.txtName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(16, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(199, 23);
            this.label9.TabIndex = 844;
            this.label9.Text = "Gain";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label15.ForeColor = System.Drawing.Color.Blue;
            this.label15.Location = new System.Drawing.Point(16, 177);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(199, 23);
            this.label15.TabIndex = 846;
            this.label15.Text = "Shift";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numBox_Pressure
            // 
            this.numBox_Pressure.BackColor = System.Drawing.Color.White;
            this.numBox_Pressure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numBox_Pressure.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numBox_Pressure.Location = new System.Drawing.Point(0, 24);
            this.numBox_Pressure.Name = "numBox_Pressure";
            this.numBox_Pressure.Size = new System.Drawing.Size(231, 40);
            this.numBox_Pressure.TabIndex = 864;
            this.numBox_Pressure.Text = "0";
            this.numBox_Pressure.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.numBox_Pressure.Click += new System.EventHandler(this.numBox_Pressure_Click);
            // 
            // ucCtrlAIOTune
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Name = "ucCtrlAIOTune";
            this.Size = new System.Drawing.Size(231, 289);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label txtID;
        private System.Windows.Forms.Label numBox_Shift;
        private System.Windows.Forms.Label numBox_Gain;
        private System.Windows.Forms.Label numBox_Offset;
        private System.Windows.Forms.Button btnAIOTune;
        private System.Windows.Forms.Label numBox_Pressure;
    }
}

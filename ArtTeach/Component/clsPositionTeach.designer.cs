namespace ArtTeach
{
    partial class clsPositionTeach
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
            this.cnbTarget = new ArtControlLib.comNumBox();
            this.lblFeedBack = new System.Windows.Forms.Label();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblAxisTitle = new System.Windows.Forms.Label();
            this.cnbPitch = new ArtControlLib.comNumBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnJogB = new System.Windows.Forms.Button();
            this.btnGO = new System.Windows.Forms.Button();
            this.btnSET = new System.Windows.Forms.Button();
            this.btnJogA = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cnbTarget
            // 
            this.cnbTarget._DecimalPlaces = 3;
            this.cnbTarget._DefaultValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.cnbTarget._IsSaveToIni = true;
            this.cnbTarget._IsSaveToLog = true;
            this.cnbTarget._IsShowPopForm = true;
            this.cnbTarget._Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.cnbTarget._Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.cnbTarget._PmtName = null;
            this.cnbTarget._PmtType = null;
            this.cnbTarget._TempValue = null;
            this.cnbTarget._Value = new decimal(new int[] {
            9999999,
            0,
            0,
            196608});
            this.cnbTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cnbTarget.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cnbTarget.Font = new System.Drawing.Font("微軟正黑體", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cnbTarget.ForeColor = System.Drawing.Color.OrangeRed;
            this.cnbTarget.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cnbTarget.Location = new System.Drawing.Point(126, 92);
            this.cnbTarget.Name = "cnbTarget";
            this.cnbTarget.ReadOnly = true;
            this.cnbTarget.Size = new System.Drawing.Size(100, 22);
            this.cnbTarget.TabIndex = 1039;
            this.cnbTarget.Text = "9999.999";
            this.cnbTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblFeedBack
            // 
            this.lblFeedBack.BackColor = System.Drawing.Color.Transparent;
            this.lblFeedBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblFeedBack.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFeedBack.ForeColor = System.Drawing.Color.Aqua;
            this.lblFeedBack.Location = new System.Drawing.Point(79, 63);
            this.lblFeedBack.Name = "lblFeedBack";
            this.lblFeedBack.Size = new System.Drawing.Size(150, 31);
            this.lblFeedBack.TabIndex = 1038;
            this.lblFeedBack.Text = "0000.000";
            this.lblFeedBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUnit
            // 
            this.lblUnit.BackColor = System.Drawing.Color.Transparent;
            this.lblUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblUnit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.lblUnit.ForeColor = System.Drawing.Color.Aqua;
            this.lblUnit.Location = new System.Drawing.Point(28, 68);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(45, 23);
            this.lblUnit.TabIndex = 1037;
            this.lblUnit.Text = "(mm)";
            // 
            // lblAxisTitle
            // 
            this.lblAxisTitle.AutoSize = true;
            this.lblAxisTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblAxisTitle.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAxisTitle.ForeColor = System.Drawing.Color.Aqua;
            this.lblAxisTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAxisTitle.Location = new System.Drawing.Point(1, 60);
            this.lblAxisTitle.Name = "lblAxisTitle";
            this.lblAxisTitle.Size = new System.Drawing.Size(31, 31);
            this.lblAxisTitle.TabIndex = 1036;
            this.lblAxisTitle.Text = "R";
            this.lblAxisTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cnbPitch
            // 
            this.cnbPitch._DecimalPlaces = 3;
            this.cnbPitch._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cnbPitch._IsSaveToIni = true;
            this.cnbPitch._IsSaveToLog = true;
            this.cnbPitch._IsShowPopForm = true;
            this.cnbPitch._Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.cnbPitch._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cnbPitch._PmtName = null;
            this.cnbPitch._PmtType = null;
            this.cnbPitch._TempValue = null;
            this.cnbPitch._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cnbPitch.BackColor = System.Drawing.Color.White;
            this.cnbPitch.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cnbPitch.ForeColor = System.Drawing.Color.Yellow;
            this.cnbPitch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cnbPitch.Location = new System.Drawing.Point(78, 35);
            this.cnbPitch.Name = "cnbPitch";
            this.cnbPitch.ReadOnly = true;
            this.cnbPitch.Size = new System.Drawing.Size(75, 25);
            this.cnbPitch.TabIndex = 1035;
            this.cnbPitch.Text = "0";
            this.cnbPitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnContinue
            // 
            this.btnContinue.BackColor = System.Drawing.Color.Blue;
            this.btnContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnContinue.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnContinue.ForeColor = System.Drawing.Color.Yellow;
            this.btnContinue.Location = new System.Drawing.Point(78, 6);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 1028;
            this.btnContinue.Tag = "-";
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = false;
            // 
            // btnJogB
            // 
            this.btnJogB.BackColor = System.Drawing.Color.Transparent;
            this.btnJogB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnJogB.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnJogB.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnJogB.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJogB.ForeColor = System.Drawing.Color.Cyan;
            this.btnJogB.Location = new System.Drawing.Point(159, 4);
            this.btnJogB.Name = "btnJogB";
            this.btnJogB.Size = new System.Drawing.Size(70, 55);
            this.btnJogB.TabIndex = 1030;
            this.btnJogB.Tag = "-";
            this.btnJogB.Text = "Jog +";
            this.btnJogB.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnJogB.UseVisualStyleBackColor = false;
            // 
            // btnGO
            // 
            this.btnGO.BackColor = System.Drawing.Color.Transparent;
            this.btnGO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGO.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnGO.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGO.Font = new System.Drawing.Font("微軟正黑體", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnGO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnGO.Location = new System.Drawing.Point(235, 6);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(50, 50);
            this.btnGO.TabIndex = 1027;
            this.btnGO.Tag = "-";
            this.btnGO.Text = "GO";
            this.btnGO.UseVisualStyleBackColor = false;
            // 
            // btnSET
            // 
            this.btnSET.BackColor = System.Drawing.Color.Transparent;
            this.btnSET.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSET.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSET.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSET.Font = new System.Drawing.Font("微軟正黑體", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSET.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSET.Location = new System.Drawing.Point(235, 64);
            this.btnSET.Name = "btnSET";
            this.btnSET.Size = new System.Drawing.Size(50, 50);
            this.btnSET.TabIndex = 1026;
            this.btnSET.Tag = "-";
            this.btnSET.Text = "SET <-";
            this.btnSET.UseVisualStyleBackColor = false;
            // 
            // btnJogA
            // 
            this.btnJogA.BackColor = System.Drawing.Color.Transparent;
            this.btnJogA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnJogA.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnJogA.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnJogA.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJogA.ForeColor = System.Drawing.Color.Cyan;
            this.btnJogA.Location = new System.Drawing.Point(2, 4);
            this.btnJogA.Name = "btnJogA";
            this.btnJogA.Size = new System.Drawing.Size(70, 55);
            this.btnJogA.TabIndex = 1032;
            this.btnJogA.Tag = "-";
            this.btnJogA.Text = "Jog -";
            this.btnJogA.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnJogA.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Aqua;
            this.label1.Location = new System.Drawing.Point(3, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 23);
            this.label1.TabIndex = 1040;
            this.label1.Text = "Position Name :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // clsPositionTeach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cnbTarget);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFeedBack);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.lblAxisTitle);
            this.Controls.Add(this.cnbPitch);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnJogB);
            this.Controls.Add(this.btnGO);
            this.Controls.Add(this.btnSET);
            this.Controls.Add(this.btnJogA);
            this.Name = "clsPositionTeach";
            this.Size = new System.Drawing.Size(291, 121);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ArtControlLib.comNumBox cnbTarget;
        private System.Windows.Forms.Label lblFeedBack;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblAxisTitle;
        private ArtControlLib.comNumBox cnbPitch;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnJogB;
        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.Button btnSET;
        private System.Windows.Forms.Button btnJogA;
        private System.Windows.Forms.Label label1;

    }
}

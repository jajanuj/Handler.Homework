namespace ArtTeach
{
    partial class clsJogAndTeach
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.cnbTarget = new ArtTeach.comNumBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.PanelAxisInfo = new System.Windows.Forms.Panel();
            this.btnSVON = new System.Windows.Forms.Button();
            this.lblALM = new System.Windows.Forms.Label();
            this.lblINP = new System.Windows.Forms.Label();
            this.lblPEL = new System.Windows.Forms.Label();
            this.lblMEL = new System.Windows.Forms.Label();
            this.lblORG = new System.Windows.Forms.Label();
            this.btnHideAxisInfo = new System.Windows.Forms.Button();
            this.btnSET = new System.Windows.Forms.Button();
            this.btnSafe = new System.Windows.Forms.Button();
            this.btn001mm = new System.Windows.Forms.Button();
            this.btn1mm = new System.Windows.Forms.Button();
            this.lblChange = new System.Windows.Forms.Label();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btn10mm = new System.Windows.Forms.Button();
            this.cnbPitch = new ArtControlLib.comNumBox();
            this.btn01mm = new System.Windows.Forms.Button();
            this.lblFeedBack = new System.Windows.Forms.Label();
            this.btnJogA = new System.Windows.Forms.Button();
            this.btnGO = new System.Windows.Forms.Button();
            this.btnJogB = new System.Windows.Forms.Button();
            this.lblAxisTitle = new System.Windows.Forms.Label();
            this.cnbUndo = new ArtTeach.comNumBox();
            this.panel5.SuspendLayout();
            this.PanelAxisInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel5.Controls.Add(this.cnbTarget);
            this.panel5.Controls.Add(this.lblUnit);
            this.panel5.Controls.Add(this.PanelAxisInfo);
            this.panel5.Controls.Add(this.btnHideAxisInfo);
            this.panel5.Controls.Add(this.btnSET);
            this.panel5.Controls.Add(this.btnSafe);
            this.panel5.Controls.Add(this.btn001mm);
            this.panel5.Controls.Add(this.btn1mm);
            this.panel5.Controls.Add(this.lblChange);
            this.panel5.Controls.Add(this.btnContinue);
            this.panel5.Controls.Add(this.btnUndo);
            this.panel5.Controls.Add(this.btn10mm);
            this.panel5.Controls.Add(this.cnbPitch);
            this.panel5.Controls.Add(this.btn01mm);
            this.panel5.Controls.Add(this.lblFeedBack);
            this.panel5.Controls.Add(this.btnJogA);
            this.panel5.Controls.Add(this.btnGO);
            this.panel5.Controls.Add(this.btnJogB);
            this.panel5.Controls.Add(this.lblAxisTitle);
            this.panel5.Controls.Add(this.cnbUndo);
            this.panel5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(284, 141);
            this.panel5.TabIndex = 1022;
            // 
            // cnbTarget
            // 
            this.cnbTarget._DecimalPlaces = 3;
            this.cnbTarget._enuName = null;
            this.cnbTarget._IsSaveToIni = false;
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
            this.cnbTarget._TargetValue = new decimal(new int[] {
            9999999,
            0,
            0,
            196608});
            this.cnbTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cnbTarget.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cnbTarget.Font = new System.Drawing.Font("微軟正黑體", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))));
            this.cnbTarget.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cnbTarget.Location = new System.Drawing.Point(148, 115);
            this.cnbTarget.Name = "cnbTarget";
            this.cnbTarget.ReadOnly = true;
            this.cnbTarget.Size = new System.Drawing.Size(75, 22);
            this.cnbTarget.TabIndex = 1026;
            this.cnbTarget.Text = "9999.999";
            this.cnbTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cnbTarget.TextChanged += new System.EventHandler(this.cnbTarget_TextChanged);
            // 
            // lblUnit
            // 
            this.lblUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblUnit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.lblUnit.ForeColor = System.Drawing.Color.Aqua;
            this.lblUnit.Location = new System.Drawing.Point(30, 89);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(40, 23);
            this.lblUnit.TabIndex = 981;
            this.lblUnit.Text = "(mm)";
            // 
            // PanelAxisInfo
            // 
            this.PanelAxisInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PanelAxisInfo.Controls.Add(this.btnSVON);
            this.PanelAxisInfo.Controls.Add(this.lblALM);
            this.PanelAxisInfo.Controls.Add(this.lblINP);
            this.PanelAxisInfo.Controls.Add(this.lblPEL);
            this.PanelAxisInfo.Controls.Add(this.lblMEL);
            this.PanelAxisInfo.Controls.Add(this.lblORG);
            this.PanelAxisInfo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelAxisInfo.Location = new System.Drawing.Point(2, 3);
            this.PanelAxisInfo.Name = "PanelAxisInfo";
            this.PanelAxisInfo.Size = new System.Drawing.Size(280, 28);
            this.PanelAxisInfo.TabIndex = 1024;
            // 
            // btnSVON
            // 
            this.btnSVON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSVON.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSVON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSVON.Font = new System.Drawing.Font("微軟正黑體", 6.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSVON.ForeColor = System.Drawing.Color.White;
            this.btnSVON.Location = new System.Drawing.Point(227, 2);
            this.btnSVON.Name = "btnSVON";
            this.btnSVON.Size = new System.Drawing.Size(50, 23);
            this.btnSVON.TabIndex = 986;
            this.btnSVON.Tag = "-";
            this.btnSVON.Text = "SVON";
            this.btnSVON.UseVisualStyleBackColor = false;
            this.btnSVON.Click += new System.EventHandler(this.btnServo_Click);
            // 
            // lblALM
            // 
            this.lblALM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblALM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblALM.Font = new System.Drawing.Font("微軟正黑體", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblALM.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblALM.Location = new System.Drawing.Point(2, 2);
            this.lblALM.Name = "lblALM";
            this.lblALM.Size = new System.Drawing.Size(45, 23);
            this.lblALM.TabIndex = 980;
            this.lblALM.Text = "ALM";
            this.lblALM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblINP
            // 
            this.lblINP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblINP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblINP.Font = new System.Drawing.Font("微軟正黑體", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblINP.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblINP.Location = new System.Drawing.Point(182, 2);
            this.lblINP.Name = "lblINP";
            this.lblINP.Size = new System.Drawing.Size(45, 23);
            this.lblINP.TabIndex = 980;
            this.lblINP.Text = "INP";
            this.lblINP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPEL
            // 
            this.lblPEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPEL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPEL.Font = new System.Drawing.Font("微軟正黑體", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPEL.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblPEL.Location = new System.Drawing.Point(47, 2);
            this.lblPEL.Name = "lblPEL";
            this.lblPEL.Size = new System.Drawing.Size(45, 23);
            this.lblPEL.TabIndex = 980;
            this.lblPEL.Text = "PEL";
            this.lblPEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMEL
            // 
            this.lblMEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMEL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMEL.Font = new System.Drawing.Font("微軟正黑體", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMEL.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblMEL.Location = new System.Drawing.Point(92, 2);
            this.lblMEL.Name = "lblMEL";
            this.lblMEL.Size = new System.Drawing.Size(45, 23);
            this.lblMEL.TabIndex = 980;
            this.lblMEL.Text = "MEL";
            this.lblMEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblORG
            // 
            this.lblORG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblORG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblORG.Font = new System.Drawing.Font("微軟正黑體", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblORG.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblORG.Location = new System.Drawing.Point(137, 2);
            this.lblORG.Name = "lblORG";
            this.lblORG.Size = new System.Drawing.Size(45, 23);
            this.lblORG.TabIndex = 980;
            this.lblORG.Text = "ORG";
            this.lblORG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnHideAxisInfo
            // 
            this.btnHideAxisInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnHideAxisInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHideAxisInfo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnHideAxisInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHideAxisInfo.Font = new System.Drawing.Font("微軟正黑體", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHideAxisInfo.ForeColor = System.Drawing.Color.Gray;
            this.btnHideAxisInfo.Location = new System.Drawing.Point(78, 87);
            this.btnHideAxisInfo.Name = "btnHideAxisInfo";
            this.btnHideAxisInfo.Size = new System.Drawing.Size(5, 25);
            this.btnHideAxisInfo.TabIndex = 1025;
            this.btnHideAxisInfo.Tag = "-";
            this.btnHideAxisInfo.UseVisualStyleBackColor = false;
            this.btnHideAxisInfo.Click += new System.EventHandler(this.btnHideAxisInfo_Click);
            // 
            // btnSET
            // 
            this.btnSET.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSET.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSET.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSET.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSET.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSET.Font = new System.Drawing.Font("微軟正黑體", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSET.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSET.Location = new System.Drawing.Point(229, 87);
            this.btnSET.Name = "btnSET";
            this.btnSET.Size = new System.Drawing.Size(50, 50);
            this.btnSET.TabIndex = 994;
            this.btnSET.Tag = "-";
            this.btnSET.Text = "SET <-";
            this.btnSET.UseVisualStyleBackColor = false;
            this.btnSET.Click += new System.EventHandler(this.btnSET_Click);
            this.btnSET.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLog_MouseDown);
            // 
            // btnSafe
            // 
            this.btnSafe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSafe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSafe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSafe.Font = new System.Drawing.Font("微軟正黑體", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSafe.ForeColor = System.Drawing.Color.White;
            this.btnSafe.Location = new System.Drawing.Point(229, 3);
            this.btnSafe.Name = "btnSafe";
            this.btnSafe.Size = new System.Drawing.Size(50, 23);
            this.btnSafe.TabIndex = 993;
            this.btnSafe.Tag = "-";
            this.btnSafe.Text = "Safe";
            this.btnSafe.UseVisualStyleBackColor = false;
            this.btnSafe.Click += new System.EventHandler(this.btnSafe_Click);
            this.btnSafe.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLog_MouseDown);
            // 
            // btn001mm
            // 
            this.btn001mm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn001mm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn001mm.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn001mm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn001mm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn001mm.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn001mm.ForeColor = System.Drawing.Color.Yellow;
            this.btn001mm.Location = new System.Drawing.Point(5, 3);
            this.btn001mm.Name = "btn001mm";
            this.btn001mm.Size = new System.Drawing.Size(50, 23);
            this.btn001mm.TabIndex = 998;
            this.btn001mm.Tag = "-";
            this.btn001mm.Text = "0.01";
            this.btn001mm.UseVisualStyleBackColor = false;
            this.btn001mm.Click += new System.EventHandler(this.btnRelativePicth_Click);
            this.btn001mm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLog_MouseDown);
            // 
            // btn1mm
            // 
            this.btn1mm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn1mm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn1mm.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn1mm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn1mm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn1mm.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn1mm.ForeColor = System.Drawing.Color.Yellow;
            this.btn1mm.Location = new System.Drawing.Point(117, 3);
            this.btn1mm.Name = "btn1mm";
            this.btn1mm.Size = new System.Drawing.Size(50, 23);
            this.btn1mm.TabIndex = 999;
            this.btn1mm.Tag = "-";
            this.btn1mm.Text = "1";
            this.btn1mm.UseVisualStyleBackColor = false;
            this.btn1mm.Click += new System.EventHandler(this.btnRelativePicth_Click);
            this.btn1mm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLog_MouseDown);
            // 
            // lblChange
            // 
            this.lblChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblChange.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblChange.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChange.ForeColor = System.Drawing.Color.Gray;
            this.lblChange.Location = new System.Drawing.Point(105, 114);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(35, 25);
            this.lblChange.TabIndex = 1005;
            this.lblChange.Text = "<->";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnContinue
            // 
            this.btnContinue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnContinue.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnContinue.ForeColor = System.Drawing.Color.Yellow;
            this.btnContinue.Location = new System.Drawing.Point(77, 58);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 25);
            this.btnContinue.TabIndex = 995;
            this.btnContinue.Tag = "-";
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            this.btnContinue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLog_MouseDown);
            // 
            // btnUndo
            // 
            this.btnUndo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnUndo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUndo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.Font = new System.Drawing.Font("微軟正黑體", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUndo.ForeColor = System.Drawing.Color.Gray;
            this.btnUndo.Location = new System.Drawing.Point(5, 114);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(50, 20);
            this.btnUndo.TabIndex = 995;
            this.btnUndo.Tag = "-";
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            this.btnUndo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLog_MouseDown);
            // 
            // btn10mm
            // 
            this.btn10mm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn10mm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn10mm.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn10mm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn10mm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn10mm.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn10mm.ForeColor = System.Drawing.Color.Yellow;
            this.btn10mm.Location = new System.Drawing.Point(173, 3);
            this.btn10mm.Name = "btn10mm";
            this.btn10mm.Size = new System.Drawing.Size(50, 23);
            this.btn10mm.TabIndex = 1006;
            this.btn10mm.Tag = "-";
            this.btn10mm.Text = "10";
            this.btn10mm.UseVisualStyleBackColor = false;
            this.btn10mm.Click += new System.EventHandler(this.btnRelativePicth_Click);
            this.btn10mm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLog_MouseDown);
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
            this.cnbPitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cnbPitch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cnbPitch.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cnbPitch.ForeColor = System.Drawing.Color.Yellow;
            this.cnbPitch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cnbPitch.Location = new System.Drawing.Point(77, 32);
            this.cnbPitch.Name = "cnbPitch";
            this.cnbPitch.ReadOnly = true;
            this.cnbPitch.Size = new System.Drawing.Size(75, 25);
            this.cnbPitch.TabIndex = 1018;
            this.cnbPitch.Text = "0";
            this.cnbPitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cnbPitch.Click += new System.EventHandler(this.btnRelativePicth_Click);
            // 
            // btn01mm
            // 
            this.btn01mm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn01mm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn01mm.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn01mm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn01mm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn01mm.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn01mm.ForeColor = System.Drawing.Color.Yellow;
            this.btn01mm.Location = new System.Drawing.Point(61, 3);
            this.btn01mm.Name = "btn01mm";
            this.btn01mm.Size = new System.Drawing.Size(50, 23);
            this.btn01mm.TabIndex = 996;
            this.btn01mm.Tag = "-";
            this.btn01mm.Text = "0.1";
            this.btn01mm.UseVisualStyleBackColor = false;
            this.btn01mm.Click += new System.EventHandler(this.btnRelativePicth_Click);
            this.btn01mm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLog_MouseDown);
            // 
            // lblFeedBack
            // 
            this.lblFeedBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFeedBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblFeedBack.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFeedBack.ForeColor = System.Drawing.Color.Aquamarine;
            this.lblFeedBack.Location = new System.Drawing.Point(83, 87);
            this.lblFeedBack.Name = "lblFeedBack";
            this.lblFeedBack.Size = new System.Drawing.Size(150, 31);
            this.lblFeedBack.TabIndex = 983;
            this.lblFeedBack.Text = "0000.000";
            this.lblFeedBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnJogA
            // 
            this.btnJogA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnJogA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnJogA.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnJogA.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnJogA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJogA.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJogA.ForeColor = System.Drawing.Color.Cyan;
            this.btnJogA.Location = new System.Drawing.Point(5, 32);
            this.btnJogA.Name = "btnJogA";
            this.btnJogA.Size = new System.Drawing.Size(65, 50);
            this.btnJogA.TabIndex = 998;
            this.btnJogA.Tag = "-";
            this.btnJogA.Text = "Jog-";
            this.btnJogA.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnJogA.UseVisualStyleBackColor = false;
            this.btnJogA.Click += new System.EventHandler(this.btnJogClick);
            this.btnJogA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogMouseDown);
            this.btnJogA.MouseEnter += new System.EventHandler(this.btnJogA_MouseEnter);
            this.btnJogA.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMouseUp);
            // 
            // btnGO
            // 
            this.btnGO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGO.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnGO.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGO.Font = new System.Drawing.Font("微軟正黑體", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnGO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnGO.Location = new System.Drawing.Point(229, 32);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(50, 50);
            this.btnGO.TabIndex = 995;
            this.btnGO.Tag = "-";
            this.btnGO.Text = "GO";
            this.btnGO.UseVisualStyleBackColor = false;
            this.btnGO.Click += new System.EventHandler(this.btnGO_Click);
            this.btnGO.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLog_MouseDown);
            // 
            // btnJogB
            // 
            this.btnJogB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnJogB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnJogB.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnJogB.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnJogB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJogB.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJogB.ForeColor = System.Drawing.Color.Cyan;
            this.btnJogB.Location = new System.Drawing.Point(158, 32);
            this.btnJogB.Name = "btnJogB";
            this.btnJogB.Size = new System.Drawing.Size(65, 50);
            this.btnJogB.TabIndex = 997;
            this.btnJogB.Tag = "-";
            this.btnJogB.Text = "Jog+";
            this.btnJogB.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnJogB.UseVisualStyleBackColor = false;
            this.btnJogB.Click += new System.EventHandler(this.btnJogClick);
            this.btnJogB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogMouseDown);
            this.btnJogB.MouseEnter += new System.EventHandler(this.btnJogB_MouseEnter);
            this.btnJogB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMouseUp);
            // 
            // lblAxisTitle
            // 
            this.lblAxisTitle.AutoSize = true;
            this.lblAxisTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAxisTitle.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAxisTitle.ForeColor = System.Drawing.Color.Aqua;
            this.lblAxisTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAxisTitle.Location = new System.Drawing.Point(5, 84);
            this.lblAxisTitle.Name = "lblAxisTitle";
            this.lblAxisTitle.Size = new System.Drawing.Size(31, 31);
            this.lblAxisTitle.TabIndex = 979;
            this.lblAxisTitle.Text = "R";
            this.lblAxisTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cnbUndo
            // 
            this.cnbUndo._DecimalPlaces = 3;
            this.cnbUndo._enuName = null;
            this.cnbUndo._IsSaveToIni = false;
            this.cnbUndo._IsSaveToLog = true;
            this.cnbUndo._IsShowPopForm = true;
            this.cnbUndo._Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.cnbUndo._Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.cnbUndo._TargetValue = new decimal(new int[] {
            9999999,
            0,
            0,
            196608});
            this.cnbUndo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cnbUndo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cnbUndo.Font = new System.Drawing.Font("微軟正黑體", 6.75F, System.Drawing.FontStyle.Italic);
            this.cnbUndo.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.cnbUndo.Location = new System.Drawing.Point(42, 121);
            this.cnbUndo.Name = "cnbUndo";
            this.cnbUndo.ReadOnly = true;
            this.cnbUndo.Size = new System.Drawing.Size(75, 12);
            this.cnbUndo.TabIndex = 1027;
            this.cnbUndo.Text = "9999.999";
            this.cnbUndo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // clsJogAndTeach
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.panel5);
            this.Name = "clsJogAndTeach";
            this.Size = new System.Drawing.Size(291, 148);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.PanelAxisInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private ArtControlLib.comNumBox cnbPitch;
        private System.Windows.Forms.Button btn10mm;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btn01mm;
        private System.Windows.Forms.Label lblFeedBack;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnJogB;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Button btnSVON;
        private System.Windows.Forms.Button btnSafe;
        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.Button btn1mm;
        private System.Windows.Forms.Button btn001mm;
        private System.Windows.Forms.Button btnSET;
        private System.Windows.Forms.Label lblINP;
        private System.Windows.Forms.Label lblORG;
        private System.Windows.Forms.Label lblMEL;
        private System.Windows.Forms.Button btnJogA;
        private System.Windows.Forms.Label lblPEL;
        private System.Windows.Forms.Label lblALM;
        private System.Windows.Forms.Label lblAxisTitle;
        private System.Windows.Forms.Panel PanelAxisInfo;
        private System.Windows.Forms.Button btnHideAxisInfo;
        private comNumBox cnbTarget;
        private comNumBox cnbUndo;
    }
}

namespace ArtSystem.MultiSystem
{
    partial class ucCtrlDispValve_ArtSpray
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nud_DispTime = new ArtControlLib.comNumUpDownBox();
            this.btnGlueStop = new System.Windows.Forms.Button();
            this.btnGlueValve = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSaveAsPztFile = new System.Windows.Forms.Button();
            this.txtPortStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nud_ClOutsideAtmTime = new ArtControlLib.comNumUpDownBox();
            this.nud_ClInsideAtmTime = new ArtControlLib.comNumUpDownBox();
            this.nud_ClLockTime = new ArtControlLib.comNumUpDownBox();
            this.nud_ClValvePreTime = new ArtControlLib.comNumUpDownBox();
            this.nud_OpOutsideAtmTime = new ArtControlLib.comNumUpDownBox();
            this.nud_OpInsideAtmTime = new ArtControlLib.comNumUpDownBox();
            this.nud_OpLockTime = new ArtControlLib.comNumUpDownBox();
            this.nud_OpValvePreTime = new ArtControlLib.comNumUpDownBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSavePztFile = new System.Windows.Forms.Button();
            this.btnChangePztParaFile = new System.Windows.Forms.Button();
            this.btnLoadPztParaFile = new System.Windows.Forms.Button();
            this.btnTools = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_DispTime)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ClOutsideAtmTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ClInsideAtmTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ClLockTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ClValvePreTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_OpOutsideAtmTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_OpInsideAtmTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_OpLockTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_OpValvePreTime)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnTools);
            this.groupBox3.Controls.Add(this.nud_DispTime);
            this.groupBox3.Controls.Add(this.btnGlueStop);
            this.groupBox3.Controls.Add(this.btnGlueValve);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("Verdana", 9F);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(591, 69);
            this.groupBox3.TabIndex = 297;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Valve Action";
            // 
            // nud_DispTime
            // 
            this.nud_DispTime._ButtonAlign = ArtControlLib.comNumUpDownBox.enuButtonDir.Horizontal;
            this.nud_DispTime._DecimalPlaces = 0;
            this.nud_DispTime._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_DispTime._Increment = 1D;
            this.nud_DispTime._IsSaveToIni = true;
            this.nud_DispTime._IsSaveToLog = true;
            this.nud_DispTime._IsShowCurrentValue = false;
            this.nud_DispTime._IsShowPopForm = true;
            this.nud_DispTime._Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.nud_DispTime._Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_DispTime._PmtName = null;
            this.nud_DispTime._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.nud_DispTime._Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_DispTime.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_DispTime.Location = new System.Drawing.Point(103, 28);
            this.nud_DispTime.Name = "nud_DispTime";
            this.nud_DispTime.Size = new System.Drawing.Size(154, 23);
            this.nud_DispTime.TabIndex = 340;
            this.nud_DispTime.TabStop = false;
            // 
            // btnGlueStop
            // 
            this.btnGlueStop.BackColor = System.Drawing.Color.Transparent;
            this.btnGlueStop.BackgroundImage = global::ArtSystem.Properties.Resources.iconfinder_waterstop_1054937;
            this.btnGlueStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGlueStop.FlatAppearance.BorderSize = 0;
            this.btnGlueStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGlueStop.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnGlueStop.Location = new System.Drawing.Point(521, 10);
            this.btnGlueStop.Name = "btnGlueStop";
            this.btnGlueStop.Size = new System.Drawing.Size(64, 55);
            this.btnGlueStop.TabIndex = 287;
            this.btnGlueStop.UseVisualStyleBackColor = false;
            this.btnGlueStop.Click += new System.EventHandler(this.btnGlueStop_Click);
            // 
            // btnGlueValve
            // 
            this.btnGlueValve.BackColor = System.Drawing.Color.Transparent;
            this.btnGlueValve.BackgroundImage = global::ArtSystem.Properties.Resources.iconfinder_water_1054937;
            this.btnGlueValve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGlueValve.FlatAppearance.BorderSize = 0;
            this.btnGlueValve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGlueValve.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnGlueValve.Location = new System.Drawing.Point(457, 10);
            this.btnGlueValve.Name = "btnGlueValve";
            this.btnGlueValve.Size = new System.Drawing.Size(64, 55);
            this.btnGlueValve.TabIndex = 170;
            this.btnGlueValve.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGlueValve.UseVisualStyleBackColor = false;
            this.btnGlueValve.Click += new System.EventHandler(this.btnGlueValve_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 27);
            this.label2.TabIndex = 163;
            this.label2.Text = "出膠時間(ms)：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSaveAsPztFile);
            this.groupBox2.Controls.Add(this.txtPortStatus);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.nud_ClOutsideAtmTime);
            this.groupBox2.Controls.Add(this.nud_ClInsideAtmTime);
            this.groupBox2.Controls.Add(this.nud_ClLockTime);
            this.groupBox2.Controls.Add(this.nud_ClValvePreTime);
            this.groupBox2.Controls.Add(this.nud_OpOutsideAtmTime);
            this.groupBox2.Controls.Add(this.nud_OpInsideAtmTime);
            this.groupBox2.Controls.Add(this.nud_OpLockTime);
            this.groupBox2.Controls.Add(this.nud_OpValvePreTime);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.btnSavePztFile);
            this.groupBox2.Controls.Add(this.btnChangePztParaFile);
            this.groupBox2.Controls.Add(this.btnLoadPztParaFile);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 9F);
            this.groupBox2.Location = new System.Drawing.Point(3, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(591, 199);
            this.groupBox2.TabIndex = 298;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Valve Parameter";
            // 
            // btnSaveAsPztFile
            // 
            this.btnSaveAsPztFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnSaveAsPztFile.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSaveAsPztFile.Location = new System.Drawing.Point(457, 21);
            this.btnSaveAsPztFile.Name = "btnSaveAsPztFile";
            this.btnSaveAsPztFile.Size = new System.Drawing.Size(128, 29);
            this.btnSaveAsPztFile.TabIndex = 361;
            this.btnSaveAsPztFile.Text = "Save As";
            this.btnSaveAsPztFile.UseVisualStyleBackColor = false;
            this.btnSaveAsPztFile.Click += new System.EventHandler(this.btnSaveAsPztFile_Click);
            // 
            // txtPortStatus
            // 
            this.txtPortStatus.AutoSize = true;
            this.txtPortStatus.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPortStatus.Location = new System.Drawing.Point(80, 27);
            this.txtPortStatus.Name = "txtPortStatus";
            this.txtPortStatus.Size = new System.Drawing.Size(70, 16);
            this.txtPortStatus.TabIndex = 360;
            this.txtPortStatus.Text = "Connected";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(6, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 359;
            this.label6.Text = "Port Status：";
            // 
            // nud_ClOutsideAtmTime
            // 
            this.nud_ClOutsideAtmTime._ButtonAlign = ArtControlLib.comNumUpDownBox.enuButtonDir.Horizontal;
            this.nud_ClOutsideAtmTime._DecimalPlaces = 0;
            this.nud_ClOutsideAtmTime._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_ClOutsideAtmTime._Increment = 1D;
            this.nud_ClOutsideAtmTime._IsSaveToIni = true;
            this.nud_ClOutsideAtmTime._IsSaveToLog = true;
            this.nud_ClOutsideAtmTime._IsShowCurrentValue = false;
            this.nud_ClOutsideAtmTime._IsShowPopForm = true;
            this.nud_ClOutsideAtmTime._Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_ClOutsideAtmTime._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_ClOutsideAtmTime._PmtName = null;
            this.nud_ClOutsideAtmTime._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.nud_ClOutsideAtmTime._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_ClOutsideAtmTime.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_ClOutsideAtmTime.Location = new System.Drawing.Point(485, 170);
            this.nud_ClOutsideAtmTime.Name = "nud_ClOutsideAtmTime";
            this.nud_ClOutsideAtmTime.Size = new System.Drawing.Size(100, 23);
            this.nud_ClOutsideAtmTime.TabIndex = 358;
            this.nud_ClOutsideAtmTime.TabStop = false;
            this.nud_ClOutsideAtmTime.Visible = false;
            // 
            // nud_ClInsideAtmTime
            // 
            this.nud_ClInsideAtmTime._ButtonAlign = ArtControlLib.comNumUpDownBox.enuButtonDir.Horizontal;
            this.nud_ClInsideAtmTime._DecimalPlaces = 0;
            this.nud_ClInsideAtmTime._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_ClInsideAtmTime._Increment = 1D;
            this.nud_ClInsideAtmTime._IsSaveToIni = true;
            this.nud_ClInsideAtmTime._IsSaveToLog = true;
            this.nud_ClInsideAtmTime._IsShowCurrentValue = false;
            this.nud_ClInsideAtmTime._IsShowPopForm = true;
            this.nud_ClInsideAtmTime._Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_ClInsideAtmTime._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_ClInsideAtmTime._PmtName = null;
            this.nud_ClInsideAtmTime._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.nud_ClInsideAtmTime._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_ClInsideAtmTime.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_ClInsideAtmTime.Location = new System.Drawing.Point(485, 139);
            this.nud_ClInsideAtmTime.Name = "nud_ClInsideAtmTime";
            this.nud_ClInsideAtmTime.Size = new System.Drawing.Size(100, 23);
            this.nud_ClInsideAtmTime.TabIndex = 357;
            this.nud_ClInsideAtmTime.TabStop = false;
            // 
            // nud_ClLockTime
            // 
            this.nud_ClLockTime._ButtonAlign = ArtControlLib.comNumUpDownBox.enuButtonDir.Horizontal;
            this.nud_ClLockTime._DecimalPlaces = 0;
            this.nud_ClLockTime._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_ClLockTime._Increment = 1D;
            this.nud_ClLockTime._IsSaveToIni = true;
            this.nud_ClLockTime._IsSaveToLog = true;
            this.nud_ClLockTime._IsShowCurrentValue = false;
            this.nud_ClLockTime._IsShowPopForm = true;
            this.nud_ClLockTime._Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_ClLockTime._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_ClLockTime._PmtName = null;
            this.nud_ClLockTime._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.nud_ClLockTime._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_ClLockTime.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_ClLockTime.Location = new System.Drawing.Point(485, 110);
            this.nud_ClLockTime.Name = "nud_ClLockTime";
            this.nud_ClLockTime.Size = new System.Drawing.Size(100, 23);
            this.nud_ClLockTime.TabIndex = 356;
            this.nud_ClLockTime.TabStop = false;
            // 
            // nud_ClValvePreTime
            // 
            this.nud_ClValvePreTime._ButtonAlign = ArtControlLib.comNumUpDownBox.enuButtonDir.Horizontal;
            this.nud_ClValvePreTime._DecimalPlaces = 0;
            this.nud_ClValvePreTime._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_ClValvePreTime._Increment = 1D;
            this.nud_ClValvePreTime._IsSaveToIni = true;
            this.nud_ClValvePreTime._IsSaveToLog = true;
            this.nud_ClValvePreTime._IsShowCurrentValue = false;
            this.nud_ClValvePreTime._IsShowPopForm = true;
            this.nud_ClValvePreTime._Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_ClValvePreTime._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_ClValvePreTime._PmtName = null;
            this.nud_ClValvePreTime._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.nud_ClValvePreTime._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_ClValvePreTime.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_ClValvePreTime.Location = new System.Drawing.Point(485, 81);
            this.nud_ClValvePreTime.Name = "nud_ClValvePreTime";
            this.nud_ClValvePreTime.Size = new System.Drawing.Size(100, 23);
            this.nud_ClValvePreTime.TabIndex = 355;
            this.nud_ClValvePreTime.TabStop = false;
            // 
            // nud_OpOutsideAtmTime
            // 
            this.nud_OpOutsideAtmTime._ButtonAlign = ArtControlLib.comNumUpDownBox.enuButtonDir.Horizontal;
            this.nud_OpOutsideAtmTime._DecimalPlaces = 0;
            this.nud_OpOutsideAtmTime._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_OpOutsideAtmTime._Increment = 1D;
            this.nud_OpOutsideAtmTime._IsSaveToIni = true;
            this.nud_OpOutsideAtmTime._IsSaveToLog = true;
            this.nud_OpOutsideAtmTime._IsShowCurrentValue = false;
            this.nud_OpOutsideAtmTime._IsShowPopForm = true;
            this.nud_OpOutsideAtmTime._Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_OpOutsideAtmTime._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_OpOutsideAtmTime._PmtName = null;
            this.nud_OpOutsideAtmTime._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.nud_OpOutsideAtmTime._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_OpOutsideAtmTime.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_OpOutsideAtmTime.Location = new System.Drawing.Point(184, 170);
            this.nud_OpOutsideAtmTime.Name = "nud_OpOutsideAtmTime";
            this.nud_OpOutsideAtmTime.Size = new System.Drawing.Size(100, 23);
            this.nud_OpOutsideAtmTime.TabIndex = 354;
            this.nud_OpOutsideAtmTime.TabStop = false;
            this.nud_OpOutsideAtmTime.Visible = false;
            // 
            // nud_OpInsideAtmTime
            // 
            this.nud_OpInsideAtmTime._ButtonAlign = ArtControlLib.comNumUpDownBox.enuButtonDir.Horizontal;
            this.nud_OpInsideAtmTime._DecimalPlaces = 0;
            this.nud_OpInsideAtmTime._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_OpInsideAtmTime._Increment = 1D;
            this.nud_OpInsideAtmTime._IsSaveToIni = true;
            this.nud_OpInsideAtmTime._IsSaveToLog = true;
            this.nud_OpInsideAtmTime._IsShowCurrentValue = false;
            this.nud_OpInsideAtmTime._IsShowPopForm = true;
            this.nud_OpInsideAtmTime._Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_OpInsideAtmTime._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_OpInsideAtmTime._PmtName = null;
            this.nud_OpInsideAtmTime._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.nud_OpInsideAtmTime._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_OpInsideAtmTime.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_OpInsideAtmTime.Location = new System.Drawing.Point(184, 139);
            this.nud_OpInsideAtmTime.Name = "nud_OpInsideAtmTime";
            this.nud_OpInsideAtmTime.Size = new System.Drawing.Size(100, 23);
            this.nud_OpInsideAtmTime.TabIndex = 353;
            this.nud_OpInsideAtmTime.TabStop = false;
            // 
            // nud_OpLockTime
            // 
            this.nud_OpLockTime._ButtonAlign = ArtControlLib.comNumUpDownBox.enuButtonDir.Horizontal;
            this.nud_OpLockTime._DecimalPlaces = 0;
            this.nud_OpLockTime._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_OpLockTime._Increment = 1D;
            this.nud_OpLockTime._IsSaveToIni = true;
            this.nud_OpLockTime._IsSaveToLog = true;
            this.nud_OpLockTime._IsShowCurrentValue = false;
            this.nud_OpLockTime._IsShowPopForm = true;
            this.nud_OpLockTime._Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_OpLockTime._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_OpLockTime._PmtName = null;
            this.nud_OpLockTime._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.nud_OpLockTime._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_OpLockTime.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_OpLockTime.Location = new System.Drawing.Point(184, 110);
            this.nud_OpLockTime.Name = "nud_OpLockTime";
            this.nud_OpLockTime.Size = new System.Drawing.Size(100, 23);
            this.nud_OpLockTime.TabIndex = 352;
            this.nud_OpLockTime.TabStop = false;
            // 
            // nud_OpValvePreTime
            // 
            this.nud_OpValvePreTime._ButtonAlign = ArtControlLib.comNumUpDownBox.enuButtonDir.Horizontal;
            this.nud_OpValvePreTime._DecimalPlaces = 0;
            this.nud_OpValvePreTime._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_OpValvePreTime._Increment = 1D;
            this.nud_OpValvePreTime._IsSaveToIni = true;
            this.nud_OpValvePreTime._IsSaveToLog = true;
            this.nud_OpValvePreTime._IsShowCurrentValue = false;
            this.nud_OpValvePreTime._IsShowPopForm = true;
            this.nud_OpValvePreTime._Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nud_OpValvePreTime._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_OpValvePreTime._PmtName = null;
            this.nud_OpValvePreTime._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.nud_OpValvePreTime._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nud_OpValvePreTime.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_OpValvePreTime.Location = new System.Drawing.Point(184, 81);
            this.nud_OpValvePreTime.Name = "nud_OpValvePreTime";
            this.nud_OpValvePreTime.Size = new System.Drawing.Size(100, 23);
            this.nud_OpValvePreTime.TabIndex = 351;
            this.nud_OpValvePreTime.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(307, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 14);
            this.label4.TabIndex = 347;
            this.label4.Text = "Close PreTime(ms)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(307, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 14);
            this.label5.TabIndex = 348;
            this.label5.Text = "Close LockTime(ms)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(307, 141);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(163, 14);
            this.label12.TabIndex = 349;
            this.label12.Text = "Close Inside Atomization";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(307, 171);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(173, 14);
            this.label13.TabIndex = 350;
            this.label13.Text = "Close Outside Atomization";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label13.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(6, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 14);
            this.label1.TabIndex = 343;
            this.label1.Text = "Open PreTime(ms)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(6, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 14);
            this.label3.TabIndex = 344;
            this.label3.Text = "Open LockTime(ms)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(6, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(162, 14);
            this.label8.TabIndex = 345;
            this.label8.Text = "Open Inside Atomization";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(6, 171);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(172, 14);
            this.label10.TabIndex = 346;
            this.label10.Text = "Open Outside Atomization";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.Location = new System.Drawing.Point(9, 56);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(535, 22);
            this.textBox1.TabIndex = 341;
            // 
            // btnSavePztFile
            // 
            this.btnSavePztFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnSavePztFile.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSavePztFile.Location = new System.Drawing.Point(323, 21);
            this.btnSavePztFile.Name = "btnSavePztFile";
            this.btnSavePztFile.Size = new System.Drawing.Size(128, 29);
            this.btnSavePztFile.TabIndex = 337;
            this.btnSavePztFile.Text = "Save";
            this.btnSavePztFile.UseVisualStyleBackColor = false;
            this.btnSavePztFile.Click += new System.EventHandler(this.btnSavePztFile_Click);
            // 
            // btnChangePztParaFile
            // 
            this.btnChangePztParaFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnChangePztParaFile.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnChangePztParaFile.Location = new System.Drawing.Point(550, 55);
            this.btnChangePztParaFile.Name = "btnChangePztParaFile";
            this.btnChangePztParaFile.Size = new System.Drawing.Size(35, 23);
            this.btnChangePztParaFile.TabIndex = 290;
            this.btnChangePztParaFile.Text = "...";
            this.btnChangePztParaFile.UseVisualStyleBackColor = false;
            this.btnChangePztParaFile.Click += new System.EventHandler(this.btnChangePztParaFile_Click);
            // 
            // btnLoadPztParaFile
            // 
            this.btnLoadPztParaFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnLoadPztParaFile.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnLoadPztParaFile.Location = new System.Drawing.Point(191, 21);
            this.btnLoadPztParaFile.Name = "btnLoadPztParaFile";
            this.btnLoadPztParaFile.Size = new System.Drawing.Size(128, 29);
            this.btnLoadPztParaFile.TabIndex = 165;
            this.btnLoadPztParaFile.Text = "Load";
            this.btnLoadPztParaFile.UseVisualStyleBackColor = false;
            this.btnLoadPztParaFile.Click += new System.EventHandler(this.btnLoadPztParaFile_Click);
            // 
            // btnTools
            // 
            this.btnTools.Location = new System.Drawing.Point(263, 28);
            this.btnTools.Name = "btnTools";
            this.btnTools.Size = new System.Drawing.Size(65, 23);
            this.btnTools.TabIndex = 341;
            this.btnTools.Text = "Tools";
            this.btnTools.UseVisualStyleBackColor = true;
            this.btnTools.Click += new System.EventHandler(this.btnTools_Click);
            // 
            // ucCtrlDispValve_Spray
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Name = "ucCtrlDispValve_Spray";
            this.Size = new System.Drawing.Size(600, 280);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nud_DispTime)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ClOutsideAtmTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ClInsideAtmTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ClLockTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_ClValvePreTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_OpOutsideAtmTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_OpInsideAtmTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_OpLockTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_OpValvePreTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGlueStop;
        private System.Windows.Forms.Button btnGlueValve;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnChangePztParaFile;
        private System.Windows.Forms.Button btnLoadPztParaFile;
        private System.Windows.Forms.Button btnSavePztFile;
        private System.Windows.Forms.TextBox textBox1;
        private ArtControlLib.comNumUpDownBox nud_ClOutsideAtmTime;
        private ArtControlLib.comNumUpDownBox nud_ClInsideAtmTime;
        private ArtControlLib.comNumUpDownBox nud_ClLockTime;
        private ArtControlLib.comNumUpDownBox nud_ClValvePreTime;
        private ArtControlLib.comNumUpDownBox nud_OpOutsideAtmTime;
        private ArtControlLib.comNumUpDownBox nud_OpInsideAtmTime;
        private ArtControlLib.comNumUpDownBox nud_OpLockTime;
        private ArtControlLib.comNumUpDownBox nud_OpValvePreTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private ArtControlLib.comNumUpDownBox nud_DispTime;
        private System.Windows.Forms.Label txtPortStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSaveAsPztFile;
        private System.Windows.Forms.Button btnTools;
    }
}

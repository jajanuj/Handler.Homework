namespace ArtSystem.MultiSystem
{
    partial class ucCtrlDispValve_ArtPZT
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
            this.components = new System.ComponentModel.Container();
            this.gb_ValveAction = new System.Windows.Forms.GroupBox();
            this.btn_ValveLock = new System.Windows.Forms.Button();
            this.btn_GetDispMonitorData = new System.Windows.Forms.Button();
            this.btn_LockAdjust = new System.Windows.Forms.Button();
            this.tbx_PztCounter = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_GlueStop = new System.Windows.Forms.Button();
            this.btn_GlueValve = new System.Windows.Forms.Button();
            this.nud_DispTime = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.gb_ValveParameter = new System.Windows.Forms.GroupBox();
            this.btn_ResetValve = new System.Windows.Forms.Button();
            this.btn_SaveAsPztFile = new System.Windows.Forms.Button();
            this.nud_Lock_Volt = new System.Windows.Forms.NumericUpDown();
            this.tbx_FilePath = new System.Windows.Forms.TextBox();
            this.btn_Tools = new System.Windows.Forms.Button();
            this.btn_SavePztFile = new System.Windows.Forms.Button();
            this.tbx_CavityTemp = new System.Windows.Forms.TextBox();
            this.tbx_PztTemp = new System.Windows.Forms.TextBox();
            this.nud_Open_Volt = new System.Windows.Forms.NumericUpDown();
            this.tbx_Pressure = new System.Windows.Forms.TextBox();
            this.btn_ChangePztParaFile = new System.Windows.Forms.Button();
            this.nud_Hold_Lock_Time = new System.Windows.Forms.NumericUpDown();
            this.tbx_PztFrequency = new System.Windows.Forms.TextBox();
            this.tbx_PztPeriod = new System.Windows.Forms.TextBox();
            this.nud_Lock_Time = new System.Windows.Forms.NumericUpDown();
            this.nud_Hold_Open_Time = new System.Windows.Forms.NumericUpDown();
            this.tbx_LockValue = new System.Windows.Forms.TextBox();
            this.nud_Open_Time = new System.Windows.Forms.NumericUpDown();
            this.btn_LoadPztParaFile = new System.Windows.Forms.Button();
            this.lbCavity_Temp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblJetFrequency = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblJetPeriod = new System.Windows.Forms.Label();
            this.lblPreLiftTimeTxt = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblOnTimeTxt = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblOffTimeTxt = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.lb_PortStatus = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lb_ValveStatus = new System.Windows.Forms.Label();
            this.gb_ValveAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_DispTime)).BeginInit();
            this.gb_ValveParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Lock_Volt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Open_Volt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Hold_Lock_Time)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Lock_Time)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Hold_Open_Time)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Open_Time)).BeginInit();
            this.SuspendLayout();
            // 
            // gb_ValveAction
            // 
            this.gb_ValveAction.Controls.Add(this.btn_ValveLock);
            this.gb_ValveAction.Controls.Add(this.btn_GetDispMonitorData);
            this.gb_ValveAction.Controls.Add(this.btn_LockAdjust);
            this.gb_ValveAction.Controls.Add(this.tbx_PztCounter);
            this.gb_ValveAction.Controls.Add(this.label10);
            this.gb_ValveAction.Controls.Add(this.btn_GlueStop);
            this.gb_ValveAction.Controls.Add(this.btn_GlueValve);
            this.gb_ValveAction.Controls.Add(this.nud_DispTime);
            this.gb_ValveAction.Controls.Add(this.label2);
            this.gb_ValveAction.Font = new System.Drawing.Font("Verdana", 9F);
            this.gb_ValveAction.Location = new System.Drawing.Point(3, 3);
            this.gb_ValveAction.Name = "gb_ValveAction";
            this.gb_ValveAction.Size = new System.Drawing.Size(591, 69);
            this.gb_ValveAction.TabIndex = 297;
            this.gb_ValveAction.TabStop = false;
            this.gb_ValveAction.Text = "Valve Action";
            // 
            // btn_ValveLock
            // 
            this.btn_ValveLock.BackColor = System.Drawing.Color.Transparent;
            this.btn_ValveLock.BackgroundImage = global::ArtSystem.Properties.Resources.Metroid_0011_LOCK_48px_532337_easyicon_net;
            this.btn_ValveLock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_ValveLock.FlatAppearance.BorderSize = 0;
            this.btn_ValveLock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ValveLock.Location = new System.Drawing.Point(329, 10);
            this.btn_ValveLock.Name = "btn_ValveLock";
            this.btn_ValveLock.Size = new System.Drawing.Size(64, 55);
            this.btn_ValveLock.TabIndex = 339;
            this.btn_ValveLock.UseVisualStyleBackColor = false;
            this.btn_ValveLock.Click += new System.EventHandler(this.btn_ValveLock_Click);
            this.btn_ValveLock.MouseEnter += new System.EventHandler(this.ToolTip_On);
            this.btn_ValveLock.MouseLeave += new System.EventHandler(this.ToolTip_Off);
            // 
            // btn_GetDispMonitorData
            // 
            this.btn_GetDispMonitorData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_GetDispMonitorData.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_GetDispMonitorData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_GetDispMonitorData.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GetDispMonitorData.Location = new System.Drawing.Point(253, 13);
            this.btn_GetDispMonitorData.Name = "btn_GetDispMonitorData";
            this.btn_GetDispMonitorData.Size = new System.Drawing.Size(70, 25);
            this.btn_GetDispMonitorData.TabIndex = 349;
            this.btn_GetDispMonitorData.Text = "Monitor";
            this.btn_GetDispMonitorData.UseVisualStyleBackColor = false;
            this.btn_GetDispMonitorData.Visible = false;
            this.btn_GetDispMonitorData.Click += new System.EventHandler(this.btn_GetDispMonitorData_Click);
            // 
            // btn_LockAdjust
            // 
            this.btn_LockAdjust.BackColor = System.Drawing.Color.Transparent;
            this.btn_LockAdjust.BackgroundImage = global::ArtSystem.Properties.Resources.ValveLockCalibration;
            this.btn_LockAdjust.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_LockAdjust.FlatAppearance.BorderSize = 0;
            this.btn_LockAdjust.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LockAdjust.Location = new System.Drawing.Point(393, 10);
            this.btn_LockAdjust.Name = "btn_LockAdjust";
            this.btn_LockAdjust.Size = new System.Drawing.Size(64, 55);
            this.btn_LockAdjust.TabIndex = 296;
            this.btn_LockAdjust.UseVisualStyleBackColor = false;
            this.btn_LockAdjust.Click += new System.EventHandler(this.btn_LockAdjust_Click);
            this.btn_LockAdjust.MouseEnter += new System.EventHandler(this.ToolTip_On);
            this.btn_LockAdjust.MouseLeave += new System.EventHandler(this.ToolTip_Off);
            // 
            // tbx_PztCounter
            // 
            this.tbx_PztCounter.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbx_PztCounter.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbx_PztCounter.Location = new System.Drawing.Point(181, 40);
            this.tbx_PztCounter.Name = "tbx_PztCounter";
            this.tbx_PztCounter.ReadOnly = true;
            this.tbx_PztCounter.Size = new System.Drawing.Size(113, 23);
            this.tbx_PztCounter.TabIndex = 338;
            this.tbx_PztCounter.Text = "0000000000";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(178, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 16);
            this.label10.TabIndex = 334;
            this.label10.Text = "Dot Count:";
            // 
            // btn_GlueStop
            // 
            this.btn_GlueStop.BackColor = System.Drawing.Color.Transparent;
            this.btn_GlueStop.BackgroundImage = global::ArtSystem.Properties.Resources.iconfinder_waterstop_1054937;
            this.btn_GlueStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_GlueStop.FlatAppearance.BorderSize = 0;
            this.btn_GlueStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_GlueStop.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_GlueStop.Location = new System.Drawing.Point(521, 10);
            this.btn_GlueStop.Name = "btn_GlueStop";
            this.btn_GlueStop.Size = new System.Drawing.Size(64, 55);
            this.btn_GlueStop.TabIndex = 287;
            this.btn_GlueStop.UseVisualStyleBackColor = false;
            this.btn_GlueStop.Click += new System.EventHandler(this.btnGlueStop_Click);
            this.btn_GlueStop.MouseEnter += new System.EventHandler(this.ToolTip_On);
            this.btn_GlueStop.MouseLeave += new System.EventHandler(this.ToolTip_Off);
            // 
            // btn_GlueValve
            // 
            this.btn_GlueValve.BackColor = System.Drawing.Color.Transparent;
            this.btn_GlueValve.BackgroundImage = global::ArtSystem.Properties.Resources.iconfinder_water_1054937;
            this.btn_GlueValve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_GlueValve.FlatAppearance.BorderSize = 0;
            this.btn_GlueValve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_GlueValve.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_GlueValve.Location = new System.Drawing.Point(457, 10);
            this.btn_GlueValve.Name = "btn_GlueValve";
            this.btn_GlueValve.Size = new System.Drawing.Size(64, 55);
            this.btn_GlueValve.TabIndex = 170;
            this.btn_GlueValve.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_GlueValve.UseVisualStyleBackColor = false;
            this.btn_GlueValve.Click += new System.EventHandler(this.btn_GlueValve_Click);
            this.btn_GlueValve.MouseEnter += new System.EventHandler(this.ToolTip_On);
            this.btn_GlueValve.MouseLeave += new System.EventHandler(this.ToolTip_Off);
            // 
            // nud_DispTime
            // 
            this.nud_DispTime.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nud_DispTime.Location = new System.Drawing.Point(87, 30);
            this.nud_DispTime.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nud_DispTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_DispTime.Name = "nud_DispTime";
            this.nud_DispTime.Size = new System.Drawing.Size(64, 23);
            this.nud_DispTime.TabIndex = 161;
            this.nud_DispTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_DispTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(2, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 27);
            this.label2.TabIndex = 163;
            this.label2.Text = "Output Dots: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gb_ValveParameter
            // 
            this.gb_ValveParameter.Controls.Add(this.btn_ResetValve);
            this.gb_ValveParameter.Controls.Add(this.btn_SaveAsPztFile);
            this.gb_ValveParameter.Controls.Add(this.nud_Lock_Volt);
            this.gb_ValveParameter.Controls.Add(this.tbx_FilePath);
            this.gb_ValveParameter.Controls.Add(this.btn_Tools);
            this.gb_ValveParameter.Controls.Add(this.btn_SavePztFile);
            this.gb_ValveParameter.Controls.Add(this.tbx_CavityTemp);
            this.gb_ValveParameter.Controls.Add(this.tbx_PztTemp);
            this.gb_ValveParameter.Controls.Add(this.nud_Open_Volt);
            this.gb_ValveParameter.Controls.Add(this.tbx_Pressure);
            this.gb_ValveParameter.Controls.Add(this.btn_ChangePztParaFile);
            this.gb_ValveParameter.Controls.Add(this.nud_Hold_Lock_Time);
            this.gb_ValveParameter.Controls.Add(this.tbx_PztFrequency);
            this.gb_ValveParameter.Controls.Add(this.tbx_PztPeriod);
            this.gb_ValveParameter.Controls.Add(this.nud_Lock_Time);
            this.gb_ValveParameter.Controls.Add(this.nud_Hold_Open_Time);
            this.gb_ValveParameter.Controls.Add(this.tbx_LockValue);
            this.gb_ValveParameter.Controls.Add(this.nud_Open_Time);
            this.gb_ValveParameter.Controls.Add(this.btn_LoadPztParaFile);
            this.gb_ValveParameter.Controls.Add(this.lbCavity_Temp);
            this.gb_ValveParameter.Controls.Add(this.label1);
            this.gb_ValveParameter.Controls.Add(this.label6);
            this.gb_ValveParameter.Controls.Add(this.lblJetFrequency);
            this.gb_ValveParameter.Controls.Add(this.label3);
            this.gb_ValveParameter.Controls.Add(this.lblJetPeriod);
            this.gb_ValveParameter.Controls.Add(this.lblPreLiftTimeTxt);
            this.gb_ValveParameter.Controls.Add(this.label7);
            this.gb_ValveParameter.Controls.Add(this.lblOnTimeTxt);
            this.gb_ValveParameter.Controls.Add(this.label4);
            this.gb_ValveParameter.Controls.Add(this.lblOffTimeTxt);
            this.gb_ValveParameter.Controls.Add(this.label16);
            this.gb_ValveParameter.Font = new System.Drawing.Font("Verdana", 9F);
            this.gb_ValveParameter.Location = new System.Drawing.Point(3, 74);
            this.gb_ValveParameter.Name = "gb_ValveParameter";
            this.gb_ValveParameter.Size = new System.Drawing.Size(591, 199);
            this.gb_ValveParameter.TabIndex = 298;
            this.gb_ValveParameter.TabStop = false;
            this.gb_ValveParameter.Text = "Valve Parameter";
            // 
            // btn_ResetValve
            // 
            this.btn_ResetValve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_ResetValve.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_ResetValve.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_ResetValve.Location = new System.Drawing.Point(237, 20);
            this.btn_ResetValve.Name = "btn_ResetValve";
            this.btn_ResetValve.Size = new System.Drawing.Size(60, 40);
            this.btn_ResetValve.TabIndex = 350;
            this.btn_ResetValve.Text = "Reset Error";
            this.btn_ResetValve.UseVisualStyleBackColor = false;
            this.btn_ResetValve.Click += new System.EventHandler(this.btn_ResetValve_Click);
            // 
            // btn_SaveAsPztFile
            // 
            this.btn_SaveAsPztFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_SaveAsPztFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_SaveAsPztFile.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_SaveAsPztFile.Location = new System.Drawing.Point(495, 20);
            this.btn_SaveAsPztFile.Name = "btn_SaveAsPztFile";
            this.btn_SaveAsPztFile.Size = new System.Drawing.Size(90, 40);
            this.btn_SaveAsPztFile.TabIndex = 347;
            this.btn_SaveAsPztFile.Text = "Save As";
            this.btn_SaveAsPztFile.UseVisualStyleBackColor = false;
            this.btn_SaveAsPztFile.Click += new System.EventHandler(this.btn_SaveAsPztFile_Click);
            // 
            // nud_Lock_Volt
            // 
            this.nud_Lock_Volt.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nud_Lock_Volt.Location = new System.Drawing.Point(225, 135);
            this.nud_Lock_Volt.Name = "nud_Lock_Volt";
            this.nud_Lock_Volt.Size = new System.Drawing.Size(50, 23);
            this.nud_Lock_Volt.TabIndex = 343;
            this.nud_Lock_Volt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_Lock_Volt.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // tbx_FilePath
            // 
            this.tbx_FilePath.BackColor = System.Drawing.SystemColors.Control;
            this.tbx_FilePath.Location = new System.Drawing.Point(6, 72);
            this.tbx_FilePath.Name = "tbx_FilePath";
            this.tbx_FilePath.ReadOnly = true;
            this.tbx_FilePath.Size = new System.Drawing.Size(535, 22);
            this.tbx_FilePath.TabIndex = 341;
            // 
            // btn_Tools
            // 
            this.btn_Tools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_Tools.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_Tools.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Tools.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Tools.Location = new System.Drawing.Point(184, 27);
            this.btn_Tools.Name = "btn_Tools";
            this.btn_Tools.Size = new System.Drawing.Size(44, 25);
            this.btn_Tools.TabIndex = 346;
            this.btn_Tools.Text = "Tool";
            this.btn_Tools.UseVisualStyleBackColor = false;
            this.btn_Tools.Click += new System.EventHandler(this.btn_PZT_Help_Click);
            // 
            // btn_SavePztFile
            // 
            this.btn_SavePztFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_SavePztFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_SavePztFile.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_SavePztFile.Location = new System.Drawing.Point(399, 20);
            this.btn_SavePztFile.Name = "btn_SavePztFile";
            this.btn_SavePztFile.Size = new System.Drawing.Size(90, 40);
            this.btn_SavePztFile.TabIndex = 337;
            this.btn_SavePztFile.Text = "Save";
            this.btn_SavePztFile.UseVisualStyleBackColor = false;
            this.btn_SavePztFile.Click += new System.EventHandler(this.btn_SavePztFile_Click);
            // 
            // tbx_CavityTemp
            // 
            this.tbx_CavityTemp.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbx_CavityTemp.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbx_CavityTemp.Location = new System.Drawing.Point(529, 165);
            this.tbx_CavityTemp.Name = "tbx_CavityTemp";
            this.tbx_CavityTemp.ReadOnly = true;
            this.tbx_CavityTemp.Size = new System.Drawing.Size(50, 23);
            this.tbx_CavityTemp.TabIndex = 335;
            this.tbx_CavityTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_PztTemp
            // 
            this.tbx_PztTemp.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbx_PztTemp.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbx_PztTemp.Location = new System.Drawing.Point(529, 135);
            this.tbx_PztTemp.Name = "tbx_PztTemp";
            this.tbx_PztTemp.ReadOnly = true;
            this.tbx_PztTemp.Size = new System.Drawing.Size(50, 23);
            this.tbx_PztTemp.TabIndex = 333;
            this.tbx_PztTemp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nud_Open_Volt
            // 
            this.nud_Open_Volt.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nud_Open_Volt.Location = new System.Drawing.Point(225, 165);
            this.nud_Open_Volt.Name = "nud_Open_Volt";
            this.nud_Open_Volt.Size = new System.Drawing.Size(50, 23);
            this.nud_Open_Volt.TabIndex = 331;
            this.nud_Open_Volt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_Open_Volt.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // tbx_Pressure
            // 
            this.tbx_Pressure.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbx_Pressure.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbx_Pressure.Location = new System.Drawing.Point(379, 165);
            this.tbx_Pressure.Name = "tbx_Pressure";
            this.tbx_Pressure.ReadOnly = true;
            this.tbx_Pressure.Size = new System.Drawing.Size(50, 23);
            this.tbx_Pressure.TabIndex = 330;
            this.tbx_Pressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_ChangePztParaFile
            // 
            this.btn_ChangePztParaFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_ChangePztParaFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_ChangePztParaFile.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_ChangePztParaFile.Location = new System.Drawing.Point(547, 68);
            this.btn_ChangePztParaFile.Name = "btn_ChangePztParaFile";
            this.btn_ChangePztParaFile.Size = new System.Drawing.Size(38, 30);
            this.btn_ChangePztParaFile.TabIndex = 290;
            this.btn_ChangePztParaFile.Text = "...";
            this.btn_ChangePztParaFile.UseVisualStyleBackColor = false;
            this.btn_ChangePztParaFile.Click += new System.EventHandler(this.btn_ChangePztFile_Click);
            // 
            // nud_Hold_Lock_Time
            // 
            this.nud_Hold_Lock_Time.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nud_Hold_Lock_Time.Location = new System.Drawing.Point(529, 105);
            this.nud_Hold_Lock_Time.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nud_Hold_Lock_Time.Name = "nud_Hold_Lock_Time";
            this.nud_Hold_Lock_Time.Size = new System.Drawing.Size(50, 23);
            this.nud_Hold_Lock_Time.TabIndex = 202;
            this.nud_Hold_Lock_Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_Hold_Lock_Time.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // tbx_PztFrequency
            // 
            this.tbx_PztFrequency.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbx_PztFrequency.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbx_PztFrequency.Location = new System.Drawing.Point(85, 165);
            this.tbx_PztFrequency.Name = "tbx_PztFrequency";
            this.tbx_PztFrequency.ReadOnly = true;
            this.tbx_PztFrequency.Size = new System.Drawing.Size(50, 23);
            this.tbx_PztFrequency.TabIndex = 322;
            this.tbx_PztFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_PztPeriod
            // 
            this.tbx_PztPeriod.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbx_PztPeriod.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbx_PztPeriod.Location = new System.Drawing.Point(85, 135);
            this.tbx_PztPeriod.Name = "tbx_PztPeriod";
            this.tbx_PztPeriod.ReadOnly = true;
            this.tbx_PztPeriod.Size = new System.Drawing.Size(50, 23);
            this.tbx_PztPeriod.TabIndex = 320;
            this.tbx_PztPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nud_Lock_Time
            // 
            this.nud_Lock_Time.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nud_Lock_Time.Location = new System.Drawing.Point(379, 105);
            this.nud_Lock_Time.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_Lock_Time.Name = "nud_Lock_Time";
            this.nud_Lock_Time.Size = new System.Drawing.Size(50, 23);
            this.nud_Lock_Time.TabIndex = 201;
            this.nud_Lock_Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_Lock_Time.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // nud_Hold_Open_Time
            // 
            this.nud_Hold_Open_Time.BackColor = System.Drawing.SystemColors.Window;
            this.nud_Hold_Open_Time.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nud_Hold_Open_Time.Location = new System.Drawing.Point(225, 105);
            this.nud_Hold_Open_Time.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nud_Hold_Open_Time.Name = "nud_Hold_Open_Time";
            this.nud_Hold_Open_Time.Size = new System.Drawing.Size(50, 23);
            this.nud_Hold_Open_Time.TabIndex = 200;
            this.nud_Hold_Open_Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_Hold_Open_Time.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // tbx_LockValue
            // 
            this.tbx_LockValue.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tbx_LockValue.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbx_LockValue.Location = new System.Drawing.Point(379, 135);
            this.tbx_LockValue.Name = "tbx_LockValue";
            this.tbx_LockValue.ReadOnly = true;
            this.tbx_LockValue.Size = new System.Drawing.Size(50, 23);
            this.tbx_LockValue.TabIndex = 328;
            this.tbx_LockValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nud_Open_Time
            // 
            this.nud_Open_Time.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nud_Open_Time.Location = new System.Drawing.Point(85, 105);
            this.nud_Open_Time.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nud_Open_Time.Name = "nud_Open_Time";
            this.nud_Open_Time.Size = new System.Drawing.Size(50, 23);
            this.nud_Open_Time.TabIndex = 199;
            this.nud_Open_Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_Open_Time.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // btn_LoadPztParaFile
            // 
            this.btn_LoadPztParaFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btn_LoadPztParaFile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_LoadPztParaFile.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_LoadPztParaFile.Location = new System.Drawing.Point(303, 20);
            this.btn_LoadPztParaFile.Name = "btn_LoadPztParaFile";
            this.btn_LoadPztParaFile.Size = new System.Drawing.Size(90, 40);
            this.btn_LoadPztParaFile.TabIndex = 165;
            this.btn_LoadPztParaFile.Text = "Load";
            this.btn_LoadPztParaFile.UseVisualStyleBackColor = false;
            this.btn_LoadPztParaFile.Click += new System.EventHandler(this.btn_LoadPztFile_Click);
            // 
            // lbCavity_Temp
            // 
            this.lbCavity_Temp.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbCavity_Temp.Location = new System.Drawing.Point(429, 165);
            this.lbCavity_Temp.Name = "lbCavity_Temp";
            this.lbCavity_Temp.Size = new System.Drawing.Size(100, 20);
            this.lbCavity_Temp.TabIndex = 334;
            this.lbCavity_Temp.Text = "CavityTemp.(°C)";
            this.lbCavity_Temp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(429, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 332;
            this.label1.Text = "PztTemp.(°C)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(275, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 20);
            this.label6.TabIndex = 329;
            this.label6.Text = "TubePressure(Pa)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblJetFrequency
            // 
            this.lblJetFrequency.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblJetFrequency.Location = new System.Drawing.Point(2, 165);
            this.lblJetFrequency.Name = "lblJetFrequency";
            this.lblJetFrequency.Size = new System.Drawing.Size(85, 20);
            this.lblJetFrequency.TabIndex = 321;
            this.lblJetFrequency.Text = "Freq.(Hz)";
            this.lblJetFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(135, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 20);
            this.label3.TabIndex = 323;
            this.label3.Text = "Lock(%)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblJetPeriod
            // 
            this.lblJetPeriod.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblJetPeriod.Location = new System.Drawing.Point(2, 135);
            this.lblJetPeriod.Name = "lblJetPeriod";
            this.lblJetPeriod.Size = new System.Drawing.Size(85, 20);
            this.lblJetPeriod.TabIndex = 319;
            this.lblJetPeriod.Text = "Period(ms)";
            this.lblJetPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPreLiftTimeTxt
            // 
            this.lblPreLiftTimeTxt.BackColor = System.Drawing.Color.Transparent;
            this.lblPreLiftTimeTxt.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPreLiftTimeTxt.Location = new System.Drawing.Point(275, 105);
            this.lblPreLiftTimeTxt.Name = "lblPreLiftTimeTxt";
            this.lblPreLiftTimeTxt.Size = new System.Drawing.Size(105, 20);
            this.lblPreLiftTimeTxt.TabIndex = 172;
            this.lblPreLiftTimeTxt.Text = "ImpactTime(us)";
            this.lblPreLiftTimeTxt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(275, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 20);
            this.label7.TabIndex = 327;
            this.label7.Text = "LockValue(um)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOnTimeTxt
            // 
            this.lblOnTimeTxt.BackColor = System.Drawing.Color.Transparent;
            this.lblOnTimeTxt.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblOnTimeTxt.Location = new System.Drawing.Point(135, 105);
            this.lblOnTimeTxt.Name = "lblOnTimeTxt";
            this.lblOnTimeTxt.Size = new System.Drawing.Size(90, 20);
            this.lblOnTimeTxt.TabIndex = 67;
            this.lblOnTimeTxt.Text = "OpenTime(us)";
            this.lblOnTimeTxt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(135, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 325;
            this.label4.Text = "Open(%)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOffTimeTxt
            // 
            this.lblOffTimeTxt.BackColor = System.Drawing.Color.Transparent;
            this.lblOffTimeTxt.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblOffTimeTxt.Location = new System.Drawing.Point(2, 105);
            this.lblOffTimeTxt.Name = "lblOffTimeTxt";
            this.lblOffTimeTxt.Size = new System.Drawing.Size(85, 20);
            this.lblOffTimeTxt.TabIndex = 158;
            this.lblOffTimeTxt.Text = "RiseTime(us)";
            this.lblOffTimeTxt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label16.Location = new System.Drawing.Point(429, 105);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 20);
            this.label16.TabIndex = 186;
            this.label16.Text = "LockTime(us)";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(8, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 16);
            this.label5.TabIndex = 344;
            this.label5.Text = "Port Status :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_PortStatus
            // 
            this.lb_PortStatus.AutoSize = true;
            this.lb_PortStatus.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_PortStatus.Location = new System.Drawing.Point(102, 97);
            this.lb_PortStatus.Name = "lb_PortStatus";
            this.lb_PortStatus.Size = new System.Drawing.Size(70, 16);
            this.lb_PortStatus.TabIndex = 345;
            this.lb_PortStatus.Text = "Connected";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(8, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 16);
            this.label9.TabIndex = 348;
            this.label9.Text = "Valve Status :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_ValveStatus
            // 
            this.lb_ValveStatus.AutoSize = true;
            this.lb_ValveStatus.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_ValveStatus.Location = new System.Drawing.Point(102, 120);
            this.lb_ValveStatus.Name = "lb_ValveStatus";
            this.lb_ValveStatus.Size = new System.Drawing.Size(70, 16);
            this.lb_ValveStatus.TabIndex = 345;
            this.lb_ValveStatus.Text = "Connected";
            // 
            // ucCtrlDispValve_ArtPZT
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.gb_ValveAction);
            this.Controls.Add(this.lb_ValveStatus);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lb_PortStatus);
            this.Controls.Add(this.gb_ValveParameter);
            this.Name = "ucCtrlDispValve_ArtPZT";
            this.Size = new System.Drawing.Size(600, 280);
            this.gb_ValveAction.ResumeLayout(false);
            this.gb_ValveAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_DispTime)).EndInit();
            this.gb_ValveParameter.ResumeLayout(false);
            this.gb_ValveParameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Lock_Volt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Open_Volt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Hold_Lock_Time)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Lock_Time)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Hold_Open_Time)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Open_Time)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_ValveAction;
        private System.Windows.Forms.Button btn_ValveLock;
        private System.Windows.Forms.Button btn_LockAdjust;
        private System.Windows.Forms.TextBox tbx_PztCounter;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_GlueStop;
        private System.Windows.Forms.Button btn_GlueValve;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nud_DispTime;
        private System.Windows.Forms.GroupBox gb_ValveParameter;
        private System.Windows.Forms.TextBox tbx_CavityTemp;
        private System.Windows.Forms.Label lbCavity_Temp;
        private System.Windows.Forms.TextBox tbx_PztTemp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nud_Open_Volt;
        private System.Windows.Forms.TextBox tbx_Pressure;
        private System.Windows.Forms.Button btn_ChangePztParaFile;
        private System.Windows.Forms.NumericUpDown nud_Hold_Lock_Time;
        private System.Windows.Forms.TextBox tbx_PztFrequency;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbx_PztPeriod;
        private System.Windows.Forms.NumericUpDown nud_Lock_Time;
        private System.Windows.Forms.Label lblJetFrequency;
        private System.Windows.Forms.NumericUpDown nud_Hold_Open_Time;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx_LockValue;
        private System.Windows.Forms.Label lblJetPeriod;
        private System.Windows.Forms.NumericUpDown nud_Open_Time;
        private System.Windows.Forms.Label lblPreLiftTimeTxt;
        private System.Windows.Forms.Button btn_LoadPztParaFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblOnTimeTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblOffTimeTxt;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btn_SavePztFile;
        private System.Windows.Forms.TextBox tbx_FilePath;
        private System.Windows.Forms.NumericUpDown nud_Lock_Volt;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btn_SaveAsPztFile;
        private System.Windows.Forms.Button btn_ResetValve;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_PortStatus;
        private System.Windows.Forms.Button btn_Tools;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lb_ValveStatus;
        private System.Windows.Forms.Button btn_GetDispMonitorData;
    }
}

namespace ArtSystem.MultiSystem
{
    partial class ucCtrlRollerMotor
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
            this.labName = new System.Windows.Forms.Label();
            this.labStation = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnKeepMove_N = new System.Windows.Forms.Button();
            this.btnKeepMove_P = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cBox_Slow = new System.Windows.Forms.CheckBox();
            this.numLowSpeed = new ArtControlLib.comNumBox();
            this.numHighSpeed = new ArtControlLib.comNumBox();
            this.panel_DO = new System.Windows.Forms.Panel();
            this.panel_ReverseDO = new System.Windows.Forms.Panel();
            this.rBtn_Reverse = new System.Windows.Forms.RadioButton();
            this.panel_SlowDO = new System.Windows.Forms.Panel();
            this.rBtn_Slow = new System.Windows.Forms.RadioButton();
            this.panel_StartDO = new System.Windows.Forms.Panel();
            this.rBtn_Start = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cBox_KeepMove = new System.Windows.Forms.CheckBox();
            this.panel_DO.SuspendLayout();
            this.panel_ReverseDO.SuspendLayout();
            this.panel_SlowDO.SuspendLayout();
            this.panel_StartDO.SuspendLayout();
            this.SuspendLayout();
            // 
            // labName
            // 
            this.labName.BackColor = System.Drawing.Color.DarkGray;
            this.labName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labName.Location = new System.Drawing.Point(39, 2);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(120, 31);
            this.labName.TabIndex = 153;
            this.labName.Text = "Roller Name";
            this.labName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labStation
            // 
            this.labStation.BackColor = System.Drawing.Color.Black;
            this.labStation.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStation.ForeColor = System.Drawing.Color.Lime;
            this.labStation.Location = new System.Drawing.Point(3, 2);
            this.labStation.Name = "labStation";
            this.labStation.Size = new System.Drawing.Size(36, 31);
            this.labStation.TabIndex = 156;
            this.labStation.Text = "1";
            this.labStation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStop.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(3, 205);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(156, 45);
            this.btnStop.TabIndex = 157;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnKeepMove_N
            // 
            this.btnKeepMove_N.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnKeepMove_N.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeepMove_N.Location = new System.Drawing.Point(3, 154);
            this.btnKeepMove_N.Name = "btnKeepMove_N";
            this.btnKeepMove_N.Size = new System.Drawing.Size(75, 45);
            this.btnKeepMove_N.TabIndex = 158;
            this.btnKeepMove_N.Text = "<<(N)";
            this.btnKeepMove_N.UseVisualStyleBackColor = true;
            this.btnKeepMove_N.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnKeepMove_N_MouseDown);
            this.btnKeepMove_N.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnKeepMove_N_MouseUp);
            // 
            // btnKeepMove_P
            // 
            this.btnKeepMove_P.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnKeepMove_P.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeepMove_P.Location = new System.Drawing.Point(84, 154);
            this.btnKeepMove_P.Name = "btnKeepMove_P";
            this.btnKeepMove_P.Size = new System.Drawing.Size(75, 45);
            this.btnKeepMove_P.TabIndex = 159;
            this.btnKeepMove_P.Text = "(P)>>";
            this.btnKeepMove_P.UseVisualStyleBackColor = true;
            this.btnKeepMove_P.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnKeepMove_P_MouseDown);
            this.btnKeepMove_P.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnKeepMove_P_MouseUp);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 37);
            this.label1.TabIndex = 162;
            this.label1.Text = "High Speed";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(84, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 37);
            this.label2.TabIndex = 163;
            this.label2.Text = "Low Speed";
            // 
            // cBox_Slow
            // 
            this.cBox_Slow.AutoSize = true;
            this.cBox_Slow.Location = new System.Drawing.Point(3, 106);
            this.cBox_Slow.Name = "cBox_Slow";
            this.cBox_Slow.Size = new System.Drawing.Size(144, 20);
            this.cBox_Slow.TabIndex = 164;
            this.cBox_Slow.Text = "Low Speed  (pps)";
            this.cBox_Slow.UseVisualStyleBackColor = true;
            // 
            // numLowSpeed
            // 
            this.numLowSpeed._DecimalPlaces = 0;
            this.numLowSpeed._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numLowSpeed._IsSaveToIni = false;
            this.numLowSpeed._IsSaveToLog = false;
            this.numLowSpeed._IsShowCurrentValue = false;
            this.numLowSpeed._IsShowPopForm = false;
            this.numLowSpeed._Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numLowSpeed._Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLowSpeed._PmtName = null;
            this.numLowSpeed._PmtType = ArtData.clsEnum.enuPmtType.System;
            this.numLowSpeed._TempValue = null;
            this.numLowSpeed._Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLowSpeed.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numLowSpeed.Location = new System.Drawing.Point(84, 73);
            this.numLowSpeed.Name = "numLowSpeed";
            this.numLowSpeed.ReadOnly = true;
            this.numLowSpeed.Size = new System.Drawing.Size(75, 27);
            this.numLowSpeed.TabIndex = 165;
            this.numLowSpeed.Text = "1";
            this.numLowSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numLowSpeed.Click += new System.EventHandler(this.numLowSpeed_Click);
            // 
            // numHighSpeed
            // 
            this.numHighSpeed._DecimalPlaces = 0;
            this.numHighSpeed._DefaultValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numHighSpeed._IsSaveToIni = false;
            this.numHighSpeed._IsSaveToLog = false;
            this.numHighSpeed._IsShowCurrentValue = false;
            this.numHighSpeed._IsShowPopForm = false;
            this.numHighSpeed._Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numHighSpeed._Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHighSpeed._PmtName = null;
            this.numHighSpeed._PmtType = ArtData.clsEnum.enuPmtType.System;
            this.numHighSpeed._TempValue = null;
            this.numHighSpeed._Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHighSpeed.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numHighSpeed.Location = new System.Drawing.Point(3, 73);
            this.numHighSpeed.Name = "numHighSpeed";
            this.numHighSpeed.ReadOnly = true;
            this.numHighSpeed.Size = new System.Drawing.Size(75, 27);
            this.numHighSpeed.TabIndex = 165;
            this.numHighSpeed.Text = "1";
            this.numHighSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numHighSpeed.Click += new System.EventHandler(this.numHighSpeed_Click);
            // 
            // panel_DO
            // 
            this.panel_DO.Controls.Add(this.panel_ReverseDO);
            this.panel_DO.Controls.Add(this.panel_SlowDO);
            this.panel_DO.Controls.Add(this.panel_StartDO);
            this.panel_DO.Location = new System.Drawing.Point(3, 38);
            this.panel_DO.Name = "panel_DO";
            this.panel_DO.Size = new System.Drawing.Size(156, 64);
            this.panel_DO.TabIndex = 166;
            // 
            // panel_ReverseDO
            // 
            this.panel_ReverseDO.Controls.Add(this.rBtn_Reverse);
            this.panel_ReverseDO.Location = new System.Drawing.Point(3, 42);
            this.panel_ReverseDO.Name = "panel_ReverseDO";
            this.panel_ReverseDO.Size = new System.Drawing.Size(153, 19);
            this.panel_ReverseDO.TabIndex = 3;
            // 
            // rBtn_Reverse
            // 
            this.rBtn_Reverse.AutoSize = true;
            this.rBtn_Reverse.ForeColor = System.Drawing.SystemColors.Control;
            this.rBtn_Reverse.Location = new System.Drawing.Point(3, -1);
            this.rBtn_Reverse.Name = "rBtn_Reverse";
            this.rBtn_Reverse.Size = new System.Drawing.Size(105, 20);
            this.rBtn_Reverse.TabIndex = 0;
            this.rBtn_Reverse.TabStop = true;
            this.rBtn_Reverse.Text = "DO_Reverse";
            this.rBtn_Reverse.UseVisualStyleBackColor = true;
            this.rBtn_Reverse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rBtn_Reverse_MouseDown);
            this.rBtn_Reverse.MouseEnter += new System.EventHandler(this.rBtn_Reverse_MouseEnter);
            this.rBtn_Reverse.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rBtn_Reverse_MouseUp);
            // 
            // panel_SlowDO
            // 
            this.panel_SlowDO.Controls.Add(this.rBtn_Slow);
            this.panel_SlowDO.Location = new System.Drawing.Point(3, 23);
            this.panel_SlowDO.Name = "panel_SlowDO";
            this.panel_SlowDO.Size = new System.Drawing.Size(153, 19);
            this.panel_SlowDO.TabIndex = 2;
            // 
            // rBtn_Slow
            // 
            this.rBtn_Slow.AutoSize = true;
            this.rBtn_Slow.ForeColor = System.Drawing.SystemColors.Control;
            this.rBtn_Slow.Location = new System.Drawing.Point(3, -1);
            this.rBtn_Slow.Name = "rBtn_Slow";
            this.rBtn_Slow.Size = new System.Drawing.Size(84, 20);
            this.rBtn_Slow.TabIndex = 0;
            this.rBtn_Slow.TabStop = true;
            this.rBtn_Slow.Text = "DO_Slow";
            this.rBtn_Slow.UseVisualStyleBackColor = true;
            this.rBtn_Slow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rBtn_Slow_MouseDown);
            this.rBtn_Slow.MouseEnter += new System.EventHandler(this.rBtn_Slow_MouseEnter);
            this.rBtn_Slow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rBtn_Slow_MouseUp);
            // 
            // panel_StartDO
            // 
            this.panel_StartDO.Controls.Add(this.rBtn_Start);
            this.panel_StartDO.Location = new System.Drawing.Point(3, 3);
            this.panel_StartDO.Name = "panel_StartDO";
            this.panel_StartDO.Size = new System.Drawing.Size(153, 19);
            this.panel_StartDO.TabIndex = 1;
            // 
            // rBtn_Start
            // 
            this.rBtn_Start.AutoSize = true;
            this.rBtn_Start.ForeColor = System.Drawing.SystemColors.Control;
            this.rBtn_Start.Location = new System.Drawing.Point(3, -1);
            this.rBtn_Start.Name = "rBtn_Start";
            this.rBtn_Start.Size = new System.Drawing.Size(87, 20);
            this.rBtn_Start.TabIndex = 0;
            this.rBtn_Start.TabStop = true;
            this.rBtn_Start.Text = "DO_Start";
            this.rBtn_Start.UseVisualStyleBackColor = true;
            this.rBtn_Start.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rBtn_Start_MouseDown);
            this.rBtn_Start.MouseEnter += new System.EventHandler(this.rBtn_Start_MouseEnter);
            this.rBtn_Start.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rBtn_Start_MouseUp);
            // 
            // cBox_KeepMove
            // 
            this.cBox_KeepMove.AutoSize = true;
            this.cBox_KeepMove.Location = new System.Drawing.Point(3, 128);
            this.cBox_KeepMove.Name = "cBox_KeepMove";
            this.cBox_KeepMove.Size = new System.Drawing.Size(99, 20);
            this.cBox_KeepMove.TabIndex = 167;
            this.cBox_KeepMove.Text = "Keep Move";
            this.cBox_KeepMove.UseVisualStyleBackColor = true;
            // 
            // ucCtrlRollerMotor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.cBox_KeepMove);
            this.Controls.Add(this.panel_DO);
            this.Controls.Add(this.numHighSpeed);
            this.Controls.Add(this.numLowSpeed);
            this.Controls.Add(this.cBox_Slow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnKeepMove_P);
            this.Controls.Add(this.btnKeepMove_N);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.labStation);
            this.Controls.Add(this.labName);
            this.Name = "ucCtrlRollerMotor";
            this.Size = new System.Drawing.Size(162, 253);
            this.VisibleChanged += new System.EventHandler(this.ucCtrlRollerMotor_VisibleChanged);
            this.panel_DO.ResumeLayout(false);
            this.panel_ReverseDO.ResumeLayout(false);
            this.panel_ReverseDO.PerformLayout();
            this.panel_SlowDO.ResumeLayout(false);
            this.panel_SlowDO.PerformLayout();
            this.panel_StartDO.ResumeLayout(false);
            this.panel_StartDO.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Label labStation;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnKeepMove_N;
        private System.Windows.Forms.Button btnKeepMove_P;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cBox_Slow;
        private ArtControlLib.comNumBox numLowSpeed;
        private ArtControlLib.comNumBox numHighSpeed;
        private System.Windows.Forms.Panel panel_DO;
        private System.Windows.Forms.Panel panel_ReverseDO;
        private System.Windows.Forms.RadioButton rBtn_Reverse;
        private System.Windows.Forms.Panel panel_SlowDO;
        private System.Windows.Forms.RadioButton rBtn_Slow;
        private System.Windows.Forms.Panel panel_StartDO;
        private System.Windows.Forms.RadioButton rBtn_Start;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cBox_KeepMove;




    }
}

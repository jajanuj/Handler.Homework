namespace ArtSystem.MultiSystem
{
    partial class ucCtrlHeaterModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCtrlHeaterModule));
            this.labName = new System.Windows.Forms.Label();
            this.btnHeaterModule_Connect = new System.Windows.Forms.Button();
            this.btnHeaterModule_Disconnect = new System.Windows.Forms.Button();
            this.btnHeaterModule_OnOff = new System.Windows.Forms.Button();
            this.labStation = new System.Windows.Forms.Label();
            this.labLowerLimit = new System.Windows.Forms.Label();
            this.labHeatTempTxt = new System.Windows.Forms.Label();
            this.labUpperLimit = new System.Windows.Forms.Label();
            this.labHeatShiftTxt = new System.Windows.Forms.Label();
            this.LabUnit = new System.Windows.Forms.Label();
            this.txt_CurrentTemp = new System.Windows.Forms.Label();
            this.txt_TargetTemp = new System.Windows.Forms.Label();
            this.txt_LimitTemp = new System.Windows.Forms.Label();
            this.txt_ErrorRange = new System.Windows.Forms.Label();
            this.txt_ShiftOffset = new System.Windows.Forms.Label();
            this.ucRoundButton1 = new ArtSystem.ucRoundButton();
            this.SuspendLayout();
            // 
            // labName
            // 
            this.labName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labName.BackColor = System.Drawing.Color.DarkGray;
            this.labName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labName.Location = new System.Drawing.Point(3, 2);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(218, 26);
            this.labName.TabIndex = 153;
            this.labName.Text = "Sensor Name";
            this.labName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnHeaterModule_Connect
            // 
            this.btnHeaterModule_Connect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHeaterModule_Connect.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHeaterModule_Connect.Location = new System.Drawing.Point(3, 226);
            this.btnHeaterModule_Connect.Name = "btnHeaterModule_Connect";
            this.btnHeaterModule_Connect.Size = new System.Drawing.Size(106, 40);
            this.btnHeaterModule_Connect.TabIndex = 161;
            this.btnHeaterModule_Connect.Text = "Connect";
            this.btnHeaterModule_Connect.UseVisualStyleBackColor = true;
            this.btnHeaterModule_Connect.Click += new System.EventHandler(this.btnHeaterModule_Connect_Click);
            // 
            // btnHeaterModule_Disconnect
            // 
            this.btnHeaterModule_Disconnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHeaterModule_Disconnect.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHeaterModule_Disconnect.Location = new System.Drawing.Point(115, 226);
            this.btnHeaterModule_Disconnect.Name = "btnHeaterModule_Disconnect";
            this.btnHeaterModule_Disconnect.Size = new System.Drawing.Size(106, 40);
            this.btnHeaterModule_Disconnect.TabIndex = 168;
            this.btnHeaterModule_Disconnect.Text = "Disconnect";
            this.btnHeaterModule_Disconnect.UseVisualStyleBackColor = true;
            this.btnHeaterModule_Disconnect.Click += new System.EventHandler(this.btnHeaterModule_Disconnect_Click);
            // 
            // btnHeaterModule_OnOff
            // 
            this.btnHeaterModule_OnOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHeaterModule_OnOff.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHeaterModule_OnOff.Location = new System.Drawing.Point(115, 32);
            this.btnHeaterModule_OnOff.Name = "btnHeaterModule_OnOff";
            this.btnHeaterModule_OnOff.Size = new System.Drawing.Size(106, 40);
            this.btnHeaterModule_OnOff.TabIndex = 176;
            this.btnHeaterModule_OnOff.Text = "OFF";
            this.btnHeaterModule_OnOff.UseVisualStyleBackColor = true;
            this.btnHeaterModule_OnOff.Click += new System.EventHandler(this.btnHeaterModule_OnOff_Click);
            // 
            // labStation
            // 
            this.labStation.BackColor = System.Drawing.Color.Black;
            this.labStation.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStation.ForeColor = System.Drawing.Color.Lime;
            this.labStation.Location = new System.Drawing.Point(4, 39);
            this.labStation.Name = "labStation";
            this.labStation.Size = new System.Drawing.Size(36, 26);
            this.labStation.TabIndex = 175;
            this.labStation.Text = "1";
            this.labStation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labLowerLimit
            // 
            this.labLowerLimit.BackColor = System.Drawing.Color.DarkGray;
            this.labLowerLimit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labLowerLimit.Location = new System.Drawing.Point(3, 162);
            this.labLowerLimit.Name = "labLowerLimit";
            this.labLowerLimit.Size = new System.Drawing.Size(106, 26);
            this.labLowerLimit.TabIndex = 172;
            this.labLowerLimit.Text = "Error Range";
            this.labLowerLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labHeatTempTxt
            // 
            this.labHeatTempTxt.BackColor = System.Drawing.Color.DarkGray;
            this.labHeatTempTxt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labHeatTempTxt.Location = new System.Drawing.Point(3, 104);
            this.labHeatTempTxt.Name = "labHeatTempTxt";
            this.labHeatTempTxt.Size = new System.Drawing.Size(106, 26);
            this.labHeatTempTxt.TabIndex = 169;
            this.labHeatTempTxt.Text = "Target Temp";
            this.labHeatTempTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labUpperLimit
            // 
            this.labUpperLimit.BackColor = System.Drawing.Color.DarkGray;
            this.labUpperLimit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labUpperLimit.Location = new System.Drawing.Point(3, 133);
            this.labUpperLimit.Name = "labUpperLimit";
            this.labUpperLimit.Size = new System.Drawing.Size(106, 26);
            this.labUpperLimit.TabIndex = 171;
            this.labUpperLimit.Text = "Temp Limit";
            this.labUpperLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labHeatShiftTxt
            // 
            this.labHeatShiftTxt.BackColor = System.Drawing.Color.DarkGray;
            this.labHeatShiftTxt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labHeatShiftTxt.Location = new System.Drawing.Point(4, 191);
            this.labHeatShiftTxt.Name = "labHeatShiftTxt";
            this.labHeatShiftTxt.Size = new System.Drawing.Size(106, 26);
            this.labHeatShiftTxt.TabIndex = 170;
            this.labHeatShiftTxt.Text = "Temp Offset";
            this.labHeatShiftTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabUnit
            // 
            this.LabUnit.BackColor = System.Drawing.Color.DarkGray;
            this.LabUnit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabUnit.Location = new System.Drawing.Point(3, 75);
            this.LabUnit.Name = "LabUnit";
            this.LabUnit.Size = new System.Drawing.Size(106, 26);
            this.LabUnit.TabIndex = 174;
            this.LabUnit.Text = "Current Temp";
            this.LabUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_CurrentTemp
            // 
            this.txt_CurrentTemp.BackColor = System.Drawing.Color.Black;
            this.txt_CurrentTemp.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_CurrentTemp.ForeColor = System.Drawing.Color.Lime;
            this.txt_CurrentTemp.Location = new System.Drawing.Point(115, 75);
            this.txt_CurrentTemp.Name = "txt_CurrentTemp";
            this.txt_CurrentTemp.Size = new System.Drawing.Size(106, 26);
            this.txt_CurrentTemp.TabIndex = 173;
            this.txt_CurrentTemp.Text = "Value";
            this.txt_CurrentTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_TargetTemp
            // 
            this.txt_TargetTemp.BackColor = System.Drawing.Color.Black;
            this.txt_TargetTemp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txt_TargetTemp.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_TargetTemp.ForeColor = System.Drawing.Color.Lime;
            this.txt_TargetTemp.Location = new System.Drawing.Point(115, 104);
            this.txt_TargetTemp.Name = "txt_TargetTemp";
            this.txt_TargetTemp.Size = new System.Drawing.Size(106, 26);
            this.txt_TargetTemp.TabIndex = 182;
            this.txt_TargetTemp.Text = "30.0";
            this.txt_TargetTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_TargetTemp.Click += new System.EventHandler(this.txt_TargetTemp_Click);
            // 
            // txt_LimitTemp
            // 
            this.txt_LimitTemp.BackColor = System.Drawing.Color.Black;
            this.txt_LimitTemp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txt_LimitTemp.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LimitTemp.ForeColor = System.Drawing.Color.Lime;
            this.txt_LimitTemp.Location = new System.Drawing.Point(115, 133);
            this.txt_LimitTemp.Name = "txt_LimitTemp";
            this.txt_LimitTemp.Size = new System.Drawing.Size(106, 26);
            this.txt_LimitTemp.TabIndex = 183;
            this.txt_LimitTemp.Text = "150.0";
            this.txt_LimitTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_LimitTemp.Click += new System.EventHandler(this.txt_LimitTemp_Click);
            // 
            // txt_ErrorRange
            // 
            this.txt_ErrorRange.BackColor = System.Drawing.Color.Black;
            this.txt_ErrorRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txt_ErrorRange.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ErrorRange.ForeColor = System.Drawing.Color.Lime;
            this.txt_ErrorRange.Location = new System.Drawing.Point(115, 162);
            this.txt_ErrorRange.Name = "txt_ErrorRange";
            this.txt_ErrorRange.Size = new System.Drawing.Size(106, 26);
            this.txt_ErrorRange.TabIndex = 184;
            this.txt_ErrorRange.Text = "5.0";
            this.txt_ErrorRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_ErrorRange.Click += new System.EventHandler(this.txt_ErrorRange_Click);
            // 
            // txt_ShiftOffset
            // 
            this.txt_ShiftOffset.BackColor = System.Drawing.Color.Black;
            this.txt_ShiftOffset.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txt_ShiftOffset.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ShiftOffset.ForeColor = System.Drawing.Color.Lime;
            this.txt_ShiftOffset.Location = new System.Drawing.Point(115, 191);
            this.txt_ShiftOffset.Name = "txt_ShiftOffset";
            this.txt_ShiftOffset.Size = new System.Drawing.Size(106, 26);
            this.txt_ShiftOffset.TabIndex = 185;
            this.txt_ShiftOffset.Text = "0.0";
            this.txt_ShiftOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_ShiftOffset.Click += new System.EventHandler(this.txt_ShiftOffset_Click);
            // 
            // ucRoundButton1
            // 
            this.ucRoundButton1._AutoMouseOnColor = true;
            this.ucRoundButton1._Color = System.Drawing.SystemColors.Control;
            this.ucRoundButton1._EdgeColor = System.Drawing.SystemColors.Control;
            this.ucRoundButton1._Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucRoundButton1._MouseOnColor = System.Drawing.SystemColors.Control;
            this.ucRoundButton1._NeedEdge = false;
            this.ucRoundButton1._Radius = 10;
            this.ucRoundButton1._ReadOnly = false;
            this.ucRoundButton1._TextColor = System.Drawing.SystemColors.ControlText;
            this.ucRoundButton1.BackColor = System.Drawing.Color.Transparent;
            this.ucRoundButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucRoundButton1.BackgroundImage")));
            this.ucRoundButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ucRoundButton1.FlatAppearance.BorderSize = 0;
            this.ucRoundButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ucRoundButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ucRoundButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ucRoundButton1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucRoundButton1.Location = new System.Drawing.Point(68, 42);
            this.ucRoundButton1.Name = "ucRoundButton1";
            this.ucRoundButton1.Size = new System.Drawing.Size(20, 20);
            this.ucRoundButton1.TabIndex = 181;
            this.ucRoundButton1.UseVisualStyleBackColor = true;
            // 
            // ucCtrlHeaterModule
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.txt_ShiftOffset);
            this.Controls.Add(this.txt_ErrorRange);
            this.Controls.Add(this.txt_LimitTemp);
            this.Controls.Add(this.txt_TargetTemp);
            this.Controls.Add(this.ucRoundButton1);
            this.Controls.Add(this.btnHeaterModule_OnOff);
            this.Controls.Add(this.labStation);
            this.Controls.Add(this.labLowerLimit);
            this.Controls.Add(this.labHeatTempTxt);
            this.Controls.Add(this.labUpperLimit);
            this.Controls.Add(this.labHeatShiftTxt);
            this.Controls.Add(this.LabUnit);
            this.Controls.Add(this.txt_CurrentTemp);
            this.Controls.Add(this.btnHeaterModule_Disconnect);
            this.Controls.Add(this.btnHeaterModule_Connect);
            this.Controls.Add(this.labName);
            this.Name = "ucCtrlHeaterModule";
            this.Size = new System.Drawing.Size(226, 271);
            this.VisibleChanged += new System.EventHandler(this.ucCtrlHeaterModule_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.Button btnHeaterModule_Connect;
        private System.Windows.Forms.Button btnHeaterModule_Disconnect;
        private System.Windows.Forms.Button btnHeaterModule_OnOff;
        private System.Windows.Forms.Label labStation;
        private System.Windows.Forms.Label labLowerLimit;
        private System.Windows.Forms.Label labHeatTempTxt;
        private System.Windows.Forms.Label labUpperLimit;
        private System.Windows.Forms.Label labHeatShiftTxt;
        private System.Windows.Forms.Label LabUnit;
        private System.Windows.Forms.Label txt_CurrentTemp;
        private ucRoundButton ucRoundButton1;
        private System.Windows.Forms.Label txt_TargetTemp;
        private System.Windows.Forms.Label txt_LimitTemp;
        private System.Windows.Forms.Label txt_ErrorRange;
        private System.Windows.Forms.Label txt_ShiftOffset;




    }
}

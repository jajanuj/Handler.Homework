namespace ArtEQ
{
    partial class ucFunctionSetting
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPage_System = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comImgButton6 = new ArtControlLib.comImgButton();
            this.comImgButton5 = new ArtControlLib.comImgButton();
            this.label42 = new System.Windows.Forms.Label();
            this.cbtn_CheckCorner = new ArtControlLib.comImgButton();
            this.tPage_Recipe = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tPage_System.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tPage_System);
            this.tabControl1.Controls.Add(this.tPage_Recipe);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(888, 553);
            this.tabControl1.TabIndex = 0;
            // 
            // tPage_System
            // 
            this.tPage_System.AllowDrop = true;
            this.tPage_System.Controls.Add(this.label6);
            this.tPage_System.Controls.Add(this.label5);
            this.tPage_System.Controls.Add(this.comImgButton6);
            this.tPage_System.Controls.Add(this.comImgButton5);
            this.tPage_System.Controls.Add(this.label42);
            this.tPage_System.Controls.Add(this.cbtn_CheckCorner);
            this.tPage_System.Location = new System.Drawing.Point(4, 25);
            this.tPage_System.Name = "tPage_System";
            this.tPage_System.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_System.Size = new System.Drawing.Size(880, 524);
            this.tPage_System.TabIndex = 0;
            this.tPage_System.Text = "System Setting";
            this.tPage_System.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(87, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 18);
            this.label6.TabIndex = 454;
            this.label6.Text = "Enable Safe Door";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(482, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 18);
            this.label5.TabIndex = 454;
            this.label5.Text = "Enable Teach Mode";
            // 
            // comImgButton6
            // 
            this.comImgButton6._DefaultStatus = false;
            this.comImgButton6._ImgBackground = true;
            this.comImgButton6._ImgFalse = global::ArtEQ.Properties.Resources.No;
            this.comImgButton6._ImgTrue = global::ArtEQ.Properties.Resources.Yes;
            this.comImgButton6._IsSaveToIni = true;
            this.comImgButton6._IsSaveToLog = true;
            this.comImgButton6._PmtName = ArtData.clsEnum.enuPmtName.Sys_EnableSafeDoor;
            this.comImgButton6._PmtType = ArtData.clsEnum.enuPmtType.System;
            this.comImgButton6._Status = false;
            this.comImgButton6.BackgroundImage = global::ArtEQ.Properties.Resources.No;
            this.comImgButton6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.comImgButton6.Location = new System.Drawing.Point(36, 36);
            this.comImgButton6.Name = "comImgButton6";
            this.comImgButton6.Size = new System.Drawing.Size(45, 45);
            this.comImgButton6.TabIndex = 453;
            this.comImgButton6.UseVisualStyleBackColor = true;
            // 
            // comImgButton5
            // 
            this.comImgButton5._DefaultStatus = false;
            this.comImgButton5._ImgBackground = true;
            this.comImgButton5._ImgFalse = global::ArtEQ.Properties.Resources.No;
            this.comImgButton5._ImgTrue = global::ArtEQ.Properties.Resources.Yes;
            this.comImgButton5._IsSaveToIni = true;
            this.comImgButton5._IsSaveToLog = true;
            this.comImgButton5._PmtName = ArtData.clsEnum.enuPmtName.Sys_TeachEnable;
            this.comImgButton5._PmtType = ArtData.clsEnum.enuPmtType.System;
            this.comImgButton5._Status = false;
            this.comImgButton5.BackgroundImage = global::ArtEQ.Properties.Resources.No;
            this.comImgButton5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.comImgButton5.Location = new System.Drawing.Point(431, 87);
            this.comImgButton5.Name = "comImgButton5";
            this.comImgButton5.Size = new System.Drawing.Size(45, 45);
            this.comImgButton5.TabIndex = 453;
            this.comImgButton5.UseVisualStyleBackColor = true;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(482, 48);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(150, 18);
            this.label42.TabIndex = 446;
            this.label42.Text = "Simulate Dry Run";
            // 
            // cbtn_CheckCorner
            // 
            this.cbtn_CheckCorner._DefaultStatus = false;
            this.cbtn_CheckCorner._ImgBackground = true;
            this.cbtn_CheckCorner._ImgFalse = global::ArtEQ.Properties.Resources.No;
            this.cbtn_CheckCorner._ImgTrue = global::ArtEQ.Properties.Resources.Yes;
            this.cbtn_CheckCorner._IsSaveToIni = true;
            this.cbtn_CheckCorner._IsSaveToLog = true;
            this.cbtn_CheckCorner._PmtName = ArtData.clsEnum.enuPmtName.Rec_NeedEmptyMagazine;
            this.cbtn_CheckCorner._PmtType = ArtData.clsEnum.enuPmtType.System;
            this.cbtn_CheckCorner._Status = false;
            this.cbtn_CheckCorner.BackgroundImage = global::ArtEQ.Properties.Resources.No;
            this.cbtn_CheckCorner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbtn_CheckCorner.Location = new System.Drawing.Point(431, 36);
            this.cbtn_CheckCorner.Name = "cbtn_CheckCorner";
            this.cbtn_CheckCorner.Size = new System.Drawing.Size(45, 45);
            this.cbtn_CheckCorner.TabIndex = 445;
            this.cbtn_CheckCorner.UseVisualStyleBackColor = true;
            // 
            // tPage_Recipe
            // 
            this.tPage_Recipe.Location = new System.Drawing.Point(4, 25);
            this.tPage_Recipe.Name = "tPage_Recipe";
            this.tPage_Recipe.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_Recipe.Size = new System.Drawing.Size(880, 524);
            this.tPage_Recipe.TabIndex = 1;
            this.tPage_Recipe.Text = "Recipe Setting";
            this.tPage_Recipe.UseVisualStyleBackColor = true;
            // 
            // ucFunctionSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tabControl1);
            this.Name = "ucFunctionSetting";
            this.Size = new System.Drawing.Size(894, 559);
            this.VisibleChanged += new System.EventHandler(this.ucMachineStatus_VisibleChanged);
            this.tabControl1.ResumeLayout(false);
            this.tPage_System.ResumeLayout(false);
            this.tPage_System.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPage_System;
        private System.Windows.Forms.TabPage tPage_Recipe;
        private System.Windows.Forms.Label label42;
        private ArtControlLib.comImgButton cbtn_CheckCorner;
        private System.Windows.Forms.Label label5;
        private ArtControlLib.comImgButton comImgButton5;
        private System.Windows.Forms.Label label6;
        private ArtControlLib.comImgButton comImgButton6;




    }
}

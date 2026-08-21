namespace ArtSystem.MultiSystem
{
    partial class ucArtAOI
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
            this.tPage_MotionCard = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_OpenGrapSettingForm = new System.Windows.Forms.Button();
            this.btnSave_ArtGrabSetting = new System.Windows.Forms.Button();
            this.btnCancel_ArtGrabSetting = new System.Windows.Forms.Button();
            this.btnEdit_ArtGrabSetting = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tPage_MotionCard.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tPage_MotionCard);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(969, 605);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tPage_MotionCard
            // 
            this.tPage_MotionCard.Controls.Add(this.panel1);
            this.tPage_MotionCard.Controls.Add(this.btnSave_ArtGrabSetting);
            this.tPage_MotionCard.Controls.Add(this.btnCancel_ArtGrabSetting);
            this.tPage_MotionCard.Controls.Add(this.btnEdit_ArtGrabSetting);
            this.tPage_MotionCard.Location = new System.Drawing.Point(4, 25);
            this.tPage_MotionCard.Name = "tPage_MotionCard";
            this.tPage_MotionCard.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_MotionCard.Size = new System.Drawing.Size(961, 576);
            this.tPage_MotionCard.TabIndex = 0;
            this.tPage_MotionCard.Text = "Art Grab System";
            this.tPage_MotionCard.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_OpenGrapSettingForm);
            this.panel1.Location = new System.Drawing.Point(6, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(949, 507);
            this.panel1.TabIndex = 9;
            // 
            // btn_OpenGrapSettingForm
            // 
            this.btn_OpenGrapSettingForm.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OpenGrapSettingForm.Location = new System.Drawing.Point(24, 84);
            this.btn_OpenGrapSettingForm.Name = "btn_OpenGrapSettingForm";
            this.btn_OpenGrapSettingForm.Size = new System.Drawing.Size(317, 127);
            this.btn_OpenGrapSettingForm.TabIndex = 8;
            this.btn_OpenGrapSettingForm.Text = "Open Garp Setting Form";
            this.btn_OpenGrapSettingForm.UseVisualStyleBackColor = true;
            this.btn_OpenGrapSettingForm.Click += new System.EventHandler(this.btn_OpenGrapSettingForm_Click);
            // 
            // btnSave_ArtGrabSetting
            // 
            this.btnSave_ArtGrabSetting.Location = new System.Drawing.Point(3, 5);
            this.btnSave_ArtGrabSetting.Name = "btnSave_ArtGrabSetting";
            this.btnSave_ArtGrabSetting.Size = new System.Drawing.Size(100, 52);
            this.btnSave_ArtGrabSetting.TabIndex = 6;
            this.btnSave_ArtGrabSetting.Text = "Save";
            this.btnSave_ArtGrabSetting.UseVisualStyleBackColor = true;
            this.btnSave_ArtGrabSetting.EnabledChanged += new System.EventHandler(this.btnSave_ArtGrabSetting_EnabledChanged);
            this.btnSave_ArtGrabSetting.Click += new System.EventHandler(this.btnSave_CardSetting_Click);
            // 
            // btnCancel_ArtGrabSetting
            // 
            this.btnCancel_ArtGrabSetting.Location = new System.Drawing.Point(109, 5);
            this.btnCancel_ArtGrabSetting.Name = "btnCancel_ArtGrabSetting";
            this.btnCancel_ArtGrabSetting.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_ArtGrabSetting.TabIndex = 5;
            this.btnCancel_ArtGrabSetting.Text = "Cancel";
            this.btnCancel_ArtGrabSetting.UseVisualStyleBackColor = true;
            this.btnCancel_ArtGrabSetting.Click += new System.EventHandler(this.btnCancel_CardSetting_Click);
            // 
            // btnEdit_ArtGrabSetting
            // 
            this.btnEdit_ArtGrabSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_ArtGrabSetting.Location = new System.Drawing.Point(855, 5);
            this.btnEdit_ArtGrabSetting.Name = "btnEdit_ArtGrabSetting";
            this.btnEdit_ArtGrabSetting.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_ArtGrabSetting.TabIndex = 7;
            this.btnEdit_ArtGrabSetting.Text = "Edit";
            this.btnEdit_ArtGrabSetting.UseVisualStyleBackColor = true;
            this.btnEdit_ArtGrabSetting.Click += new System.EventHandler(this.btnEdit_CardSetting_Click);
            // 
            // ucArtAOI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tabControl1);
            this.Name = "ucArtAOI";
            this.Size = new System.Drawing.Size(975, 611);
            this.VisibleChanged += new System.EventHandler(this.ucCardSetting_VisibleChanged);
            this.tabControl1.ResumeLayout(false);
            this.tPage_MotionCard.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPage_MotionCard;
        private System.Windows.Forms.Button btnSave_ArtGrabSetting;
        private System.Windows.Forms.Button btnCancel_ArtGrabSetting;
        private System.Windows.Forms.Button btnEdit_ArtGrabSetting;
        private System.Windows.Forms.Button btn_OpenGrapSettingForm;
        private System.Windows.Forms.Panel panel1;


    }
}

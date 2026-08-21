namespace ArtSystem.FormDesign
{
    partial class ucFormSetup
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
            this.groupBoxTopmost = new System.Windows.Forms.GroupBox();
            this.btnTopFalse = new System.Windows.Forms.Button();
            this.btnTopTrue = new System.Windows.Forms.Button();
            this.groupBoxLanguage = new System.Windows.Forms.GroupBox();
            this.btnChinese = new System.Windows.Forms.Button();
            this.btnEnglish = new System.Windows.Forms.Button();
            this.groupBoxFormStyle = new System.Windows.Forms.GroupBox();
            this.btnFullScrean = new System.Windows.Forms.Button();
            this.btnWindowsMode = new System.Windows.Forms.Button();
            this.groupBoxFormDesign = new System.Windows.Forms.GroupBox();
            this.btnPanelDesign = new System.Windows.Forms.Button();
            this.btnPCDesign = new System.Windows.Forms.Button();
            this.groupBoxTopmost.SuspendLayout();
            this.groupBoxLanguage.SuspendLayout();
            this.groupBoxFormStyle.SuspendLayout();
            this.groupBoxFormDesign.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTopmost
            // 
            this.groupBoxTopmost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTopmost.Controls.Add(this.btnTopFalse);
            this.groupBoxTopmost.Controls.Add(this.btnTopTrue);
            this.groupBoxTopmost.Location = new System.Drawing.Point(3, 96);
            this.groupBoxTopmost.Name = "groupBoxTopmost";
            this.groupBoxTopmost.Size = new System.Drawing.Size(1006, 87);
            this.groupBoxTopmost.TabIndex = 7;
            this.groupBoxTopmost.TabStop = false;
            this.groupBoxTopmost.Text = "Is Topmost";
            // 
            // btnTopFalse
            // 
            this.btnTopFalse.Location = new System.Drawing.Point(133, 22);
            this.btnTopFalse.Name = "btnTopFalse";
            this.btnTopFalse.Size = new System.Drawing.Size(100, 50);
            this.btnTopFalse.TabIndex = 0;
            this.btnTopFalse.Tag = "";
            this.btnTopFalse.Text = "No";
            this.btnTopFalse.UseVisualStyleBackColor = true;
            this.btnTopFalse.Click += new System.EventHandler(this.IsTopmost);
            // 
            // btnTopTrue
            // 
            this.btnTopTrue.Location = new System.Drawing.Point(17, 22);
            this.btnTopTrue.Name = "btnTopTrue";
            this.btnTopTrue.Size = new System.Drawing.Size(100, 50);
            this.btnTopTrue.TabIndex = 1;
            this.btnTopTrue.Tag = "";
            this.btnTopTrue.Text = "Yes";
            this.btnTopTrue.UseVisualStyleBackColor = true;
            this.btnTopTrue.Click += new System.EventHandler(this.IsTopmost);
            // 
            // groupBoxLanguage
            // 
            this.groupBoxLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLanguage.Controls.Add(this.btnChinese);
            this.groupBoxLanguage.Controls.Add(this.btnEnglish);
            this.groupBoxLanguage.Location = new System.Drawing.Point(3, 189);
            this.groupBoxLanguage.Name = "groupBoxLanguage";
            this.groupBoxLanguage.Size = new System.Drawing.Size(1006, 87);
            this.groupBoxLanguage.TabIndex = 6;
            this.groupBoxLanguage.TabStop = false;
            this.groupBoxLanguage.Text = "Language";
            // 
            // btnChinese
            // 
            this.btnChinese.Location = new System.Drawing.Point(133, 22);
            this.btnChinese.Name = "btnChinese";
            this.btnChinese.Size = new System.Drawing.Size(100, 50);
            this.btnChinese.TabIndex = 0;
            this.btnChinese.Tag = "";
            this.btnChinese.Text = "繁體中文";
            this.btnChinese.UseVisualStyleBackColor = true;
            this.btnChinese.Click += new System.EventHandler(this.LanguageChange);
            // 
            // btnEnglish
            // 
            this.btnEnglish.Location = new System.Drawing.Point(17, 22);
            this.btnEnglish.Name = "btnEnglish";
            this.btnEnglish.Size = new System.Drawing.Size(100, 50);
            this.btnEnglish.TabIndex = 1;
            this.btnEnglish.Tag = "";
            this.btnEnglish.Text = "English";
            this.btnEnglish.UseVisualStyleBackColor = true;
            this.btnEnglish.Click += new System.EventHandler(this.LanguageChange);
            // 
            // groupBoxFormStyle
            // 
            this.groupBoxFormStyle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFormStyle.Controls.Add(this.btnFullScrean);
            this.groupBoxFormStyle.Controls.Add(this.btnWindowsMode);
            this.groupBoxFormStyle.Location = new System.Drawing.Point(3, 3);
            this.groupBoxFormStyle.Name = "groupBoxFormStyle";
            this.groupBoxFormStyle.Size = new System.Drawing.Size(1006, 87);
            this.groupBoxFormStyle.TabIndex = 5;
            this.groupBoxFormStyle.TabStop = false;
            this.groupBoxFormStyle.Text = "Form Style Setup";
            // 
            // btnFullScrean
            // 
            this.btnFullScrean.Location = new System.Drawing.Point(133, 22);
            this.btnFullScrean.Name = "btnFullScrean";
            this.btnFullScrean.Size = new System.Drawing.Size(100, 50);
            this.btnFullScrean.TabIndex = 0;
            this.btnFullScrean.Tag = "";
            this.btnFullScrean.Text = "Full Screen";
            this.btnFullScrean.UseVisualStyleBackColor = true;
            this.btnFullScrean.Click += new System.EventHandler(this.FormStyleChange);
            // 
            // btnWindowsMode
            // 
            this.btnWindowsMode.Location = new System.Drawing.Point(17, 22);
            this.btnWindowsMode.Name = "btnWindowsMode";
            this.btnWindowsMode.Size = new System.Drawing.Size(100, 50);
            this.btnWindowsMode.TabIndex = 1;
            this.btnWindowsMode.Tag = "";
            this.btnWindowsMode.Text = "Windows Mode";
            this.btnWindowsMode.UseVisualStyleBackColor = true;
            this.btnWindowsMode.Click += new System.EventHandler(this.FormStyleChange);
            // 
            // groupBoxFormDesign
            // 
            this.groupBoxFormDesign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFormDesign.Controls.Add(this.btnPanelDesign);
            this.groupBoxFormDesign.Controls.Add(this.btnPCDesign);
            this.groupBoxFormDesign.Location = new System.Drawing.Point(3, 282);
            this.groupBoxFormDesign.Name = "groupBoxFormDesign";
            this.groupBoxFormDesign.Size = new System.Drawing.Size(1006, 87);
            this.groupBoxFormDesign.TabIndex = 8;
            this.groupBoxFormDesign.TabStop = false;
            this.groupBoxFormDesign.Text = "Form Design";
            // 
            // btnPanelDesign
            // 
            this.btnPanelDesign.Location = new System.Drawing.Point(133, 22);
            this.btnPanelDesign.Name = "btnPanelDesign";
            this.btnPanelDesign.Size = new System.Drawing.Size(100, 50);
            this.btnPanelDesign.TabIndex = 0;
            this.btnPanelDesign.Tag = "";
            this.btnPanelDesign.Text = "Panel Design";
            this.btnPanelDesign.UseVisualStyleBackColor = true;
            this.btnPanelDesign.Click += new System.EventHandler(this.FormDesignChange);
            // 
            // btnPCDesign
            // 
            this.btnPCDesign.Location = new System.Drawing.Point(17, 22);
            this.btnPCDesign.Name = "btnPCDesign";
            this.btnPCDesign.Size = new System.Drawing.Size(100, 50);
            this.btnPCDesign.TabIndex = 1;
            this.btnPCDesign.Tag = "";
            this.btnPCDesign.Text = "PC Design";
            this.btnPCDesign.UseVisualStyleBackColor = true;
            this.btnPCDesign.Click += new System.EventHandler(this.FormDesignChange);
            // 
            // ucFormSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBoxFormDesign);
            this.Controls.Add(this.groupBoxTopmost);
            this.Controls.Add(this.groupBoxLanguage);
            this.Controls.Add(this.groupBoxFormStyle);
            this.Name = "ucFormSetup";
            this.Size = new System.Drawing.Size(1012, 518);
            this.VisibleChanged += new System.EventHandler(this.ucFormSetup_VisibleChanged);
            this.groupBoxTopmost.ResumeLayout(false);
            this.groupBoxLanguage.ResumeLayout(false);
            this.groupBoxFormStyle.ResumeLayout(false);
            this.groupBoxFormDesign.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTopmost;
        private System.Windows.Forms.Button btnTopFalse;
        private System.Windows.Forms.Button btnTopTrue;
        private System.Windows.Forms.GroupBox groupBoxLanguage;
        private System.Windows.Forms.Button btnChinese;
        private System.Windows.Forms.Button btnEnglish;
        private System.Windows.Forms.GroupBox groupBoxFormStyle;
        private System.Windows.Forms.Button btnFullScrean;
        private System.Windows.Forms.Button btnWindowsMode;
        private System.Windows.Forms.GroupBox groupBoxFormDesign;
        private System.Windows.Forms.Button btnPanelDesign;
        private System.Windows.Forms.Button btnPCDesign;
    }
}

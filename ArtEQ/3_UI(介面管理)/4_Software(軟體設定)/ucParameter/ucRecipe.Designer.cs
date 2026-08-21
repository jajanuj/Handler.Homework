namespace ArtEQ
{
    partial class ucRecipe
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
            this.tPage_ucParameter = new System.Windows.Forms.TabPage();
            this.tPage_ucRecipeManage = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tPage_ucRecipeManage);
            this.tabControl1.Controls.Add(this.tPage_ucParameter);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(651, 379);
            this.tabControl1.TabIndex = 0;
            // 
            // tPage_ucParameter
            // 
            this.tPage_ucParameter.Location = new System.Drawing.Point(4, 25);
            this.tPage_ucParameter.Name = "tPage_ucParameter";
            this.tPage_ucParameter.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_ucParameter.Size = new System.Drawing.Size(643, 350);
            this.tPage_ucParameter.TabIndex = 0;
            this.tPage_ucParameter.Text = "Parameter Path";
            this.tPage_ucParameter.UseVisualStyleBackColor = true;
            // 
            // tPage_ucRecipeManage
            // 
            this.tPage_ucRecipeManage.Location = new System.Drawing.Point(4, 25);
            this.tPage_ucRecipeManage.Name = "tPage_ucRecipeManage";
            this.tPage_ucRecipeManage.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_ucRecipeManage.Size = new System.Drawing.Size(643, 350);
            this.tPage_ucRecipeManage.TabIndex = 1;
            this.tPage_ucRecipeManage.Text = "Recipe Manage";
            this.tPage_ucRecipeManage.UseVisualStyleBackColor = true;
            // 
            // ucRecipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tabControl1);
            this.Name = "ucRecipe";
            this.Size = new System.Drawing.Size(654, 385);
            this.SizeChanged += new System.EventHandler(this.ucRecipe_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.ucRecipe_VisibleChanged);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPage_ucParameter;
        private System.Windows.Forms.TabPage tPage_ucRecipeManage;

    }
}

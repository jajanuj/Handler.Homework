namespace ArtControlLib
{
    partial class comStaticButton
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
            this.btnDI1 = new System.Windows.Forms.Button();
            this.btnDI2 = new System.Windows.Forms.Button();
            this.btnDO1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDI1
            // 
            this.btnDI1.Enabled = false;
            this.btnDI1.Location = new System.Drawing.Point(3, 3);
            this.btnDI1.Name = "btnDI1";
            this.btnDI1.Size = new System.Drawing.Size(15, 15);
            this.btnDI1.TabIndex = 0;
            this.btnDI1.UseVisualStyleBackColor = true;
            // 
            // btnDI2
            // 
            this.btnDI2.Enabled = false;
            this.btnDI2.Location = new System.Drawing.Point(3, 657);
            this.btnDI2.Name = "btnDI2";
            this.btnDI2.Size = new System.Drawing.Size(15, 15);
            this.btnDI2.TabIndex = 1;
            this.btnDI2.UseVisualStyleBackColor = true;
            // 
            // btnDO1
            // 
            this.btnDO1.Enabled = false;
            this.btnDO1.Location = new System.Drawing.Point(727, 3);
            this.btnDO1.Name = "btnDO1";
            this.btnDO1.Size = new System.Drawing.Size(15, 15);
            this.btnDO1.TabIndex = 2;
            this.btnDO1.UseVisualStyleBackColor = true;
            // 
            // comStaticButton
            // 
            this.Controls.Add(this.btnDO1);
            this.Controls.Add(this.btnDI2);
            this.Controls.Add(this.btnDI1);
            this.Name = "comStaticButton";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDI1;
        private System.Windows.Forms.Button btnDI2;
        private System.Windows.Forms.Button btnDO1;
    }
}

namespace ArtEQ
{
    partial class ucSignalIndicator
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        /// 指示燈與文字皆由 OnPaint() 直接繪製 (見 ucSignalIndicator.cs)，不需要子控制項。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ucSignalIndicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucSignalIndicator";
            this.Size = new System.Drawing.Size(120, 24);
            this.ResumeLayout(false);
        }

        #endregion
    }
}

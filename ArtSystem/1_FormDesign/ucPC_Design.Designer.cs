namespace ArtSystem.FormDesign
{
    partial class ucPC_Design
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
            this.SubForm = new System.Windows.Forms.Panel();
            this.ContainerSubFunc = new System.Windows.Forms.Panel();
            this.sBar_SubForm = new System.Windows.Forms.VScrollBar();
            this.ContainerTitle = new System.Windows.Forms.Panel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ContainerHotKey = new System.Windows.Forms.Panel();
            this.ContainerMainFunc = new System.Windows.Forms.Panel();
            this.monthCalendarToday = new System.Windows.Forms.MonthCalendar();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SubForm
            // 
            this.SubForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SubForm.Location = new System.Drawing.Point(167, 10);
            this.SubForm.Name = "SubForm";
            this.SubForm.Size = new System.Drawing.Size(954, 572);
            this.SubForm.TabIndex = 6;
            // 
            // ContainerSubFunc
            // 
            this.ContainerSubFunc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContainerSubFunc.Location = new System.Drawing.Point(1150, 10);
            this.ContainerSubFunc.Name = "ContainerSubFunc";
            this.ContainerSubFunc.Size = new System.Drawing.Size(119, 572);
            this.ContainerSubFunc.TabIndex = 8;
            // 
            // sBar_SubForm
            // 
            this.sBar_SubForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sBar_SubForm.Location = new System.Drawing.Point(1124, 10);
            this.sBar_SubForm.Maximum = 1000;
            this.sBar_SubForm.Name = "sBar_SubForm";
            this.sBar_SubForm.Size = new System.Drawing.Size(23, 567);
            this.sBar_SubForm.TabIndex = 10;
            this.sBar_SubForm.Visible = false;
            // 
            // ContainerTitle
            // 
            this.ContainerTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContainerTitle.Location = new System.Drawing.Point(28, 10);
            this.ContainerTitle.Name = "ContainerTitle";
            this.ContainerTitle.Size = new System.Drawing.Size(133, 507);
            this.ContainerTitle.TabIndex = 0;
            // 
            // MainPanel
            // 
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainPanel.Controls.Add(this.SubForm);
            this.MainPanel.Controls.Add(this.ContainerSubFunc);
            this.MainPanel.Controls.Add(this.sBar_SubForm);
            this.MainPanel.Controls.Add(this.ContainerHotKey);
            this.MainPanel.Controls.Add(this.ContainerTitle);
            this.MainPanel.Controls.Add(this.ContainerMainFunc);
            this.MainPanel.Controls.Add(this.monthCalendarToday);
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1274, 732);
            this.MainPanel.TabIndex = 13;
            // 
            // ContainerHotKey
            // 
            this.ContainerHotKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContainerHotKey.Location = new System.Drawing.Point(28, 523);
            this.ContainerHotKey.Name = "ContainerHotKey";
            this.ContainerHotKey.Size = new System.Drawing.Size(133, 201);
            this.ContainerHotKey.TabIndex = 1;
            // 
            // ContainerMainFunc
            // 
            this.ContainerMainFunc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContainerMainFunc.Location = new System.Drawing.Point(167, 585);
            this.ContainerMainFunc.Name = "ContainerMainFunc";
            this.ContainerMainFunc.Size = new System.Drawing.Size(1102, 136);
            this.ContainerMainFunc.TabIndex = 7;
            // 
            // monthCalendarToday
            // 
            this.monthCalendarToday.Location = new System.Drawing.Point(-1, -1);
            this.monthCalendarToday.Name = "monthCalendarToday";
            this.monthCalendarToday.TabIndex = 9;
            this.monthCalendarToday.Visible = false;
            // 
            // ucPC_Design
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainPanel);
            this.Name = "ucPC_Design";
            this.Size = new System.Drawing.Size(1288, 772);
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SubForm;
        private System.Windows.Forms.Panel ContainerSubFunc;
        private System.Windows.Forms.VScrollBar sBar_SubForm;
        private System.Windows.Forms.Panel ContainerTitle;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel ContainerHotKey;
        private System.Windows.Forms.Panel ContainerMainFunc;
        private System.Windows.Forms.MonthCalendar monthCalendarToday;
    }
}

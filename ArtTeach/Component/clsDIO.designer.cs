namespace ArtTeach
{
    partial class clsDIO
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
            this.timerAxisStatus = new System.Windows.Forms.Timer(this.components);
            this.lblDOTrigger = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnJogA = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDOTrigger
            // 
            this.lblDOTrigger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDOTrigger.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblDOTrigger.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblDOTrigger.ForeColor = System.Drawing.Color.Yellow;
            this.lblDOTrigger.Location = new System.Drawing.Point(0, 2);
            this.lblDOTrigger.Name = "lblDOTrigger";
            this.lblDOTrigger.Size = new System.Drawing.Size(92, 25);
            this.lblDOTrigger.TabIndex = 1117;
            this.lblDOTrigger.Text = "PnP Vacuum";
            this.lblDOTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.BackgroundImage = global::ArtTeach.Properties.Resources.Right;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.button1.ForeColor = System.Drawing.Color.Gold;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.button1.Location = new System.Drawing.Point(47, 28);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 45);
            this.button1.TabIndex = 1121;
            this.button1.Tag = "-";
            this.button1.Text = "OFF";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnJogA
            // 
            this.btnJogA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnJogA.BackgroundImage = global::ArtTeach.Properties.Resources.Left;
            this.btnJogA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnJogA.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnJogA.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnJogA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnJogA.Font = new System.Drawing.Font("微軟正黑體", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnJogA.ForeColor = System.Drawing.Color.Gold;
            this.btnJogA.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnJogA.Location = new System.Drawing.Point(0, 28);
            this.btnJogA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnJogA.Name = "btnJogA";
            this.btnJogA.Size = new System.Drawing.Size(45, 45);
            this.btnJogA.TabIndex = 1120;
            this.btnJogA.Tag = "-";
            this.btnJogA.Text = "ON";
            this.btnJogA.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnJogA.UseVisualStyleBackColor = false;
            // 
            // clsDIOInfo_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnJogA);
            this.Controls.Add(this.lblDOTrigger);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "clsDIOInfo_1";
            this.Size = new System.Drawing.Size(93, 75);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerAxisStatus;
        private System.Windows.Forms.Label lblDOTrigger;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnJogA;
    }
}

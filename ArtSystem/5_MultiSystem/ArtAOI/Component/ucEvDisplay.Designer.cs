namespace ArtSystem.MultiSystem
{
    partial class ucEvDisplay
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMotionCtrl = new System.Windows.Forms.Button();
            this.btnLightCtrl = new System.Windows.Forms.Button();
            this.btnLoadImg = new System.Windows.Forms.Button();
            this.btnSaveImg = new System.Windows.Forms.Button();
            this.btnLive = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 386);
            this.panel1.TabIndex = 4;
            this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);
            // 
            // btnMotionCtrl
            // 
            this.btnMotionCtrl.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnMotionCtrl.Image = global::ArtSystem.Properties.Resources.Motion;
            this.btnMotionCtrl.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMotionCtrl.Location = new System.Drawing.Point(384, 322);
            this.btnMotionCtrl.Margin = new System.Windows.Forms.Padding(2);
            this.btnMotionCtrl.Name = "btnMotionCtrl";
            this.btnMotionCtrl.Size = new System.Drawing.Size(70, 60);
            this.btnMotionCtrl.TabIndex = 5;
            this.btnMotionCtrl.Text = "Motion";
            this.btnMotionCtrl.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMotionCtrl.UseVisualStyleBackColor = true;
            this.btnMotionCtrl.Visible = false;
            this.btnMotionCtrl.Click += new System.EventHandler(this.btnMotionCtrl_Click);
            // 
            // btnLightCtrl
            // 
            this.btnLightCtrl.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnLightCtrl.Image = global::ArtSystem.Properties.Resources.Light;
            this.btnLightCtrl.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLightCtrl.Location = new System.Drawing.Point(384, 258);
            this.btnLightCtrl.Margin = new System.Windows.Forms.Padding(2);
            this.btnLightCtrl.Name = "btnLightCtrl";
            this.btnLightCtrl.Size = new System.Drawing.Size(70, 60);
            this.btnLightCtrl.TabIndex = 4;
            this.btnLightCtrl.Text = "Light";
            this.btnLightCtrl.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLightCtrl.UseVisualStyleBackColor = true;
            this.btnLightCtrl.Visible = false;
            this.btnLightCtrl.Click += new System.EventHandler(this.btnLightCtrl_Click);
            // 
            // btnLoadImg
            // 
            this.btnLoadImg.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnLoadImg.Image = global::ArtSystem.Properties.Resources.LoadImg;
            this.btnLoadImg.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLoadImg.Location = new System.Drawing.Point(384, 130);
            this.btnLoadImg.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadImg.Name = "btnLoadImg";
            this.btnLoadImg.Size = new System.Drawing.Size(70, 60);
            this.btnLoadImg.TabIndex = 3;
            this.btnLoadImg.Text = "Load";
            this.btnLoadImg.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLoadImg.UseVisualStyleBackColor = true;
            this.btnLoadImg.Click += new System.EventHandler(this.btnLoadImg_Click);
            // 
            // btnSaveImg
            // 
            this.btnSaveImg.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSaveImg.Image = global::ArtSystem.Properties.Resources.SaveImg;
            this.btnSaveImg.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveImg.Location = new System.Drawing.Point(384, 194);
            this.btnSaveImg.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveImg.Name = "btnSaveImg";
            this.btnSaveImg.Size = new System.Drawing.Size(70, 60);
            this.btnSaveImg.TabIndex = 2;
            this.btnSaveImg.Text = "Save";
            this.btnSaveImg.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveImg.UseVisualStyleBackColor = true;
            this.btnSaveImg.Click += new System.EventHandler(this.btnSaveImg_Click);
            // 
            // btnLive
            // 
            this.btnLive.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnLive.Image = global::ArtSystem.Properties.Resources.Live;
            this.btnLive.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLive.Location = new System.Drawing.Point(384, 66);
            this.btnLive.Margin = new System.Windows.Forms.Padding(2);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(70, 60);
            this.btnLive.TabIndex = 1;
            this.btnLive.Text = "Live";
            this.btnLive.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLive.UseVisualStyleBackColor = true;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCapture.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCapture.Image = global::ArtSystem.Properties.Resources.Capture;
            this.btnCapture.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCapture.Location = new System.Drawing.Point(384, 2);
            this.btnCapture.Margin = new System.Windows.Forms.Padding(2);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(70, 60);
            this.btnCapture.TabIndex = 0;
            this.btnCapture.Text = "Capture";
            this.btnCapture.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // ucEvDisplay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.btnMotionCtrl);
            this.Controls.Add(this.btnLightCtrl);
            this.Controls.Add(this.btnLoadImg);
            this.Controls.Add(this.btnSaveImg);
            this.Controls.Add(this.btnLive);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucEvDisplay";
            this.Size = new System.Drawing.Size(458, 386);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnLive;
        private System.Windows.Forms.Button btnSaveImg;
        private System.Windows.Forms.Button btnLoadImg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLightCtrl;
        private System.Windows.Forms.Button btnMotionCtrl;



    }
}

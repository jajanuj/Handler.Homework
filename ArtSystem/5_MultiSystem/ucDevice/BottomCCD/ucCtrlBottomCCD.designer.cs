namespace ArtSystem.MultiSystem
{
    partial class ucCtrlBottomCCD
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
            this.btn_ButtomImgSave = new System.Windows.Forms.Button();
            this.btn_ButtomInsp = new System.Windows.Forms.Button();
            this.btnSingleGrab = new System.Windows.Forms.Button();
            this.lb_ButtomInspResult = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnLiveGrab = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_ManualOffset_BottomCCD = new System.Windows.Forms.Button();
            this.panel_ucCam1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btn_ButtomImgSave
            // 
            this.btn_ButtomImgSave.Location = new System.Drawing.Point(4, 76);
            this.btn_ButtomImgSave.Name = "btn_ButtomImgSave";
            this.btn_ButtomImgSave.Size = new System.Drawing.Size(80, 42);
            this.btn_ButtomImgSave.TabIndex = 418;
            this.btn_ButtomImgSave.Text = "Save";
            this.btn_ButtomImgSave.UseVisualStyleBackColor = true;
            // 
            // btn_ButtomInsp
            // 
            this.btn_ButtomInsp.Location = new System.Drawing.Point(4, 124);
            this.btn_ButtomInsp.Name = "btn_ButtomInsp";
            this.btn_ButtomInsp.Size = new System.Drawing.Size(80, 42);
            this.btn_ButtomInsp.TabIndex = 417;
            this.btn_ButtomInsp.Text = "Inspect";
            this.btn_ButtomInsp.UseVisualStyleBackColor = true;
            // 
            // btnSingleGrab
            // 
            this.btnSingleGrab.Location = new System.Drawing.Point(4, 28);
            this.btnSingleGrab.Name = "btnSingleGrab";
            this.btnSingleGrab.Size = new System.Drawing.Size(80, 42);
            this.btnSingleGrab.TabIndex = 415;
            this.btnSingleGrab.Text = "Single";
            this.btnSingleGrab.UseVisualStyleBackColor = true;
            this.btnSingleGrab.Click += new System.EventHandler(this.btnSingleGrab_Click);
            // 
            // lb_ButtomInspResult
            // 
            this.lb_ButtomInspResult.AutoSize = true;
            this.lb_ButtomInspResult.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_ButtomInspResult.Location = new System.Drawing.Point(264, 177);
            this.lb_ButtomInspResult.Name = "lb_ButtomInspResult";
            this.lb_ButtomInspResult.Size = new System.Drawing.Size(79, 32);
            this.lb_ButtomInspResult.TabIndex = 413;
            this.lb_ButtomInspResult.Text = "None";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(135, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 32);
            this.label1.TabIndex = 412;
            this.label1.Text = "Result：";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(864, 23);
            this.lblTitle.TabIndex = 796;
            this.lblTitle.Text = "Buttom CCD Setting";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLiveGrab
            // 
            this.btnLiveGrab.Location = new System.Drawing.Point(4, 221);
            this.btnLiveGrab.Name = "btnLiveGrab";
            this.btnLiveGrab.Size = new System.Drawing.Size(80, 42);
            this.btnLiveGrab.TabIndex = 799;
            this.btnLiveGrab.Text = "Live";
            this.btnLiveGrab.UseVisualStyleBackColor = true;
            this.btnLiveGrab.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 284);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(205, 62);
            this.button1.TabIndex = 798;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // btn_ManualOffset_BottomCCD
            // 
            this.btn_ManualOffset_BottomCCD.Location = new System.Drawing.Point(4, 352);
            this.btn_ManualOffset_BottomCCD.Name = "btn_ManualOffset_BottomCCD";
            this.btn_ManualOffset_BottomCCD.Size = new System.Drawing.Size(165, 82);
            this.btn_ManualOffset_BottomCCD.TabIndex = 800;
            this.btn_ManualOffset_BottomCCD.Text = "Manual Offset";
            this.btn_ManualOffset_BottomCCD.UseVisualStyleBackColor = true;
            // 
            // panel_ucCam1
            // 
            this.panel_ucCam1.BackColor = System.Drawing.SystemColors.Control;
            this.panel_ucCam1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_ucCam1.Location = new System.Drawing.Point(352, 28);
            this.panel_ucCam1.Name = "panel_ucCam1";
            this.panel_ucCam1.Size = new System.Drawing.Size(508, 411);
            this.panel_ucCam1.TabIndex = 788;
            // 
            // ucCtrlBottomCCD
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.btn_ButtomImgSave);
            this.Controls.Add(this.panel_ucCam1);
            this.Controls.Add(this.btn_ButtomInsp);
            this.Controls.Add(this.btn_ManualOffset_BottomCCD);
            this.Controls.Add(this.btnSingleGrab);
            this.Controls.Add(this.lb_ButtomInspResult);
            this.Controls.Add(this.btnLiveGrab);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblTitle);
            this.Name = "ucCtrlBottomCCD";
            this.Size = new System.Drawing.Size(864, 444);
            this.VisibleChanged += new System.EventHandler(this.ucCtrlBottomCCD_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }




        #endregion
        private System.Windows.Forms.Button btn_ButtomImgSave;
        private System.Windows.Forms.Button btn_ButtomInsp;
        private System.Windows.Forms.Button btnSingleGrab;
        private System.Windows.Forms.Label lb_ButtomInspResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnLiveGrab;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_ManualOffset_BottomCCD;
        private System.Windows.Forms.Panel panel_ucCam1;
    }
}

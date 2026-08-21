namespace ArtEQ
{
    partial class ucTrayDisplay
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
        /// 版面由 TableLayoutPanel (tlpMain) 管理：第1列訊號、第2列 pnlCanvas 畫布 (繪圖邏輯見 ucTrayDisplay.cs 的 pnlCanvas_Paint)、第3列訊號。
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.ucSignalLoad = new ArtEQ.ucSignalIndicator();
            this.flpLaneSignal = new System.Windows.Forms.FlowLayoutPanel();
            this.ucSignalSlow = new ArtEQ.ucSignalIndicator();
            this.ucSignalArrival = new ArtEQ.ucSignalIndicator();
            this.ucSignalUnload = new ArtEQ.ucSignalIndicator();
            this.flpStopper = new System.Windows.Forms.FlowLayoutPanel();
            this.ucSignalBackward = new ArtEQ.ucSignalIndicator();
            this.ucSignalForward = new ArtEQ.ucSignalIndicator();
            this.pnlCanvas = new ArtEQ.DrawPanel();
            this.tlpMain.SuspendLayout();
            this.flpLaneSignal.SuspendLayout();
            this.flpStopper.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.flpLaneSignal, 0, 0);
            this.tlpMain.Controls.Add(this.pnlCanvas, 0, 1);
            this.tlpMain.Controls.Add(this.flpStopper, 0, 2);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 3;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpMain.Size = new System.Drawing.Size(190, 136);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlCanvas
            // 
            this.pnlCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCanvas.Location = new System.Drawing.Point(0, 28);
            this.pnlCanvas.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(190, 80);
            this.pnlCanvas.TabIndex = 3;
            this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
            // 
            // ucSignalLoad
            // 
            this.ucSignalLoad.Location = new System.Drawing.Point(4, 5);
            this.ucSignalLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucSignalLoad.Name = "ucSignalLoad";
            this.ucSignalLoad.SignalText = "Ld";
            this.ucSignalLoad.Size = new System.Drawing.Size(35, 18);
            this.ucSignalLoad.TabIndex = 0;
            // 
            // flpLaneSignal
            // 
            this.flpLaneSignal.Controls.Add(this.ucSignalLoad);
            this.flpLaneSignal.Controls.Add(this.ucSignalSlow);
            this.flpLaneSignal.Controls.Add(this.ucSignalArrival);
            this.flpLaneSignal.Controls.Add(this.ucSignalUnload);
            this.flpLaneSignal.Location = new System.Drawing.Point(0, 0);
            this.flpLaneSignal.Margin = new System.Windows.Forms.Padding(0);
            this.flpLaneSignal.Name = "flpLaneSignal";
            this.flpLaneSignal.Size = new System.Drawing.Size(190, 28);
            this.flpLaneSignal.TabIndex = 1;
            // 
            // ucSignalSlow
            // 
            this.ucSignalSlow.Location = new System.Drawing.Point(47, 5);
            this.ucSignalSlow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucSignalSlow.Name = "ucSignalSlow";
            this.ucSignalSlow.SignalText = "Slw";
            this.ucSignalSlow.Size = new System.Drawing.Size(35, 18);
            this.ucSignalSlow.TabIndex = 1;
            // 
            // ucSignalArrival
            // 
            this.ucSignalArrival.Location = new System.Drawing.Point(90, 5);
            this.ucSignalArrival.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucSignalArrival.Name = "ucSignalArrival";
            this.ucSignalArrival.SignalText = "Arr";
            this.ucSignalArrival.Size = new System.Drawing.Size(35, 18);
            this.ucSignalArrival.TabIndex = 2;
            // 
            // ucSignalUnload
            // 
            this.ucSignalUnload.Location = new System.Drawing.Point(133, 5);
            this.ucSignalUnload.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucSignalUnload.Name = "ucSignalUnload";
            this.ucSignalUnload.SignalText = "Uld";
            this.ucSignalUnload.Size = new System.Drawing.Size(35, 18);
            this.ucSignalUnload.TabIndex = 3;
            // 
            // flpStopper
            // 
            this.flpStopper.Controls.Add(this.ucSignalBackward);
            this.flpStopper.Controls.Add(this.ucSignalForward);
            this.flpStopper.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpStopper.Location = new System.Drawing.Point(0, 108);
            this.flpStopper.Margin = new System.Windows.Forms.Padding(0);
            this.flpStopper.Name = "flpStopper";
            this.flpStopper.Size = new System.Drawing.Size(190, 28);
            this.flpStopper.TabIndex = 2;
            // 
            // ucSignalBackward
            // 
            this.ucSignalBackward.Location = new System.Drawing.Point(146, 5);
            this.ucSignalBackward.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucSignalBackward.Name = "ucSignalBackward";
            this.ucSignalBackward.SignalText = "Bwd";
            this.ucSignalBackward.Size = new System.Drawing.Size(40, 18);
            this.ucSignalBackward.TabIndex = 0;
            // 
            // ucSignalForward
            // 
            this.ucSignalForward.Location = new System.Drawing.Point(98, 5);
            this.ucSignalForward.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucSignalForward.Name = "ucSignalForward";
            this.ucSignalForward.SignalText = "Fwd";
            this.ucSignalForward.Size = new System.Drawing.Size(40, 18);
            this.ucSignalForward.TabIndex = 1;
            // 
            // ucTrayDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "ucTrayDisplay";
            this.Size = new System.Drawing.Size(190, 136);
            this.tlpMain.ResumeLayout(false);
            this.flpLaneSignal.ResumeLayout(false);
            this.flpStopper.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private ucSignalIndicator ucSignalLoad;
        private System.Windows.Forms.FlowLayoutPanel flpLaneSignal;
        private ucSignalIndicator ucSignalSlow;
        private ucSignalIndicator ucSignalArrival;
        private ucSignalIndicator ucSignalUnload;
        private System.Windows.Forms.FlowLayoutPanel flpStopper;
        private ucSignalIndicator ucSignalBackward;
        private ucSignalIndicator ucSignalForward;
        private DrawPanel pnlCanvas;
    }
}

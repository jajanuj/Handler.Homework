namespace ArtEQ
{
    partial class ucMagazineDisplay
    {
        /// <summary> 必要的設計工具變數 </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 清除任何使用中的資源 </summary>
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。Slot 按鈕皆為執行期動態產生 (見 ucMagazineDisplay.cs 的 BuildSlots())。
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.flpSlot = new System.Windows.Forms.FlowLayoutPanel();
            this.flpIndicator = new System.Windows.Forms.FlowLayoutPanel();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.flpSlot, 0, 0);
            this.tlpMain.Controls.Add(this.flpIndicator, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(190, 150);
            this.tlpMain.TabIndex = 0;
            // 
            // flpSlot
            //
            this.flpSlot.AutoScroll = false;
            this.flpSlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpSlot.Location = new System.Drawing.Point(1, 1);
            this.flpSlot.Margin = new System.Windows.Forms.Padding(1);
            this.flpSlot.Name = "flpSlot";
            this.flpSlot.Size = new System.Drawing.Size(88, 148);
            this.flpSlot.TabIndex = 0;
            // 
            // flpIndicator
            // 
            this.flpIndicator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpIndicator.Location = new System.Drawing.Point(94, 4);
            this.flpIndicator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flpIndicator.Name = "flpIndicator";
            this.flpIndicator.Size = new System.Drawing.Size(92, 142);
            this.flpIndicator.TabIndex = 1;
            // 
            // ucMagazineDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucMagazineDisplay";
            this.Size = new System.Drawing.Size(190, 150);
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.FlowLayoutPanel flpSlot;
        private System.Windows.Forms.FlowLayoutPanel flpIndicator;
    }
}

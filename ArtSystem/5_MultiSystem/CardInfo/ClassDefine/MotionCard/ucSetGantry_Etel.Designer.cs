namespace ArtSystem.MultiSystem
{
    partial class ucSetGantry_Etel
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvEtelGantry = new System.Windows.Forms.DataGridView();
            this.dgvEtelGantry_CardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvEtelGantry_Enable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEtelGantry)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Etel Gantry Card";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvEtelGantry
            // 
            this.dgvEtelGantry.AllowUserToAddRows = false;
            this.dgvEtelGantry.AllowUserToDeleteRows = false;
            this.dgvEtelGantry.AllowUserToResizeColumns = false;
            this.dgvEtelGantry.AllowUserToResizeRows = false;
            this.dgvEtelGantry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEtelGantry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEtelGantry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvEtelGantry_CardNo,
            this.dgvEtelGantry_Enable});
            this.dgvEtelGantry.Location = new System.Drawing.Point(8, 41);
            this.dgvEtelGantry.MultiSelect = false;
            this.dgvEtelGantry.Name = "dgvEtelGantry";
            this.dgvEtelGantry.RowHeadersVisible = false;
            this.dgvEtelGantry.RowTemplate.Height = 24;
            this.dgvEtelGantry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvEtelGantry.Size = new System.Drawing.Size(276, 354);
            this.dgvEtelGantry.TabIndex = 9;
            // 
            // dgvEtelGantry_CardNo
            // 
            this.dgvEtelGantry_CardNo.Frozen = true;
            this.dgvEtelGantry_CardNo.HeaderText = "Card No.";
            this.dgvEtelGantry_CardNo.Name = "dgvEtelGantry_CardNo";
            this.dgvEtelGantry_CardNo.ReadOnly = true;
            this.dgvEtelGantry_CardNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvEtelGantry_Enable
            // 
            this.dgvEtelGantry_Enable.HeaderText = "Enable Gantry";
            this.dgvEtelGantry_Enable.Name = "dgvEtelGantry_Enable";
            this.dgvEtelGantry_Enable.Width = 150;
            // 
            // ucSetGantry_Etel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.dgvEtelGantry);
            this.Controls.Add(this.label1);
            this.Name = "ucSetGantry_Etel";
            this.Size = new System.Drawing.Size(287, 395);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEtelGantry)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvEtelGantry;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEtelGantry_CardNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvEtelGantry_Enable;



    }
}

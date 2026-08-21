namespace ArtSystem.MultiSystem
{
    partial class ucSetAxisSetting
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
            this.dgvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAxisID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAxisName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAxisLogic1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvABSEncoder = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvAxisDisableINP = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEtelGantry)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(762, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Axis Setting";
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
            this.dgvNo,
            this.dgvAxisID,
            this.dgvAxisName,
            this.dgvAxisLogic1,
            this.dgvABSEncoder,
            this.dgvAxisDisableINP});
            this.dgvEtelGantry.Location = new System.Drawing.Point(8, 41);
            this.dgvEtelGantry.MultiSelect = false;
            this.dgvEtelGantry.Name = "dgvEtelGantry";
            this.dgvEtelGantry.RowHeadersVisible = false;
            this.dgvEtelGantry.RowTemplate.Height = 24;
            this.dgvEtelGantry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvEtelGantry.Size = new System.Drawing.Size(757, 258);
            this.dgvEtelGantry.TabIndex = 9;
            // 
            // dgvNo
            // 
            this.dgvNo.HeaderText = "No.";
            this.dgvNo.Name = "dgvNo";
            this.dgvNo.ReadOnly = true;
            // 
            // dgvAxisID
            // 
            this.dgvAxisID.HeaderText = "Axis ID";
            this.dgvAxisID.Name = "dgvAxisID";
            this.dgvAxisID.ReadOnly = true;
            this.dgvAxisID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvAxisName
            // 
            this.dgvAxisName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvAxisName.HeaderText = "Axis Name";
            this.dgvAxisName.Name = "dgvAxisName";
            this.dgvAxisName.ReadOnly = true;
            // 
            // dgvAxisLogic1
            // 
            this.dgvAxisLogic1.HeaderText = "AxisLogic (No Function)";
            this.dgvAxisLogic1.Name = "dgvAxisLogic1";
            this.dgvAxisLogic1.ReadOnly = true;
            this.dgvAxisLogic1.Width = 200;
            // 
            // dgvABSEncoder
            // 
            this.dgvABSEncoder.HeaderText = "ABS Encoder";
            this.dgvABSEncoder.Name = "dgvABSEncoder";
            this.dgvABSEncoder.Width = 150;
            // 
            // dgvAxisDisableINP
            // 
            this.dgvAxisDisableINP.HeaderText = "Disable INP";
            this.dgvAxisDisableINP.Name = "dgvAxisDisableINP";
            // 
            // ucSetAxisSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.dgvEtelGantry);
            this.Controls.Add(this.label1);
            this.Name = "ucSetAxisSetting";
            this.Size = new System.Drawing.Size(768, 299);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEtelGantry)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvEtelGantry;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAxisID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAxisName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvAxisLogic1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvABSEncoder;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvAxisDisableINP;
    }
}

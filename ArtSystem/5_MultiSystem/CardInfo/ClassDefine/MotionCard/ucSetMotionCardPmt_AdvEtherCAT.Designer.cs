namespace ArtSystem.MultiSystem
{
    partial class ucSetMotionCardPmt_AdvEtherCAT
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
            this.dgvSetHommingPmt_TPM = new System.Windows.Forms.DataGridView();
            this.dgvAxisID_Homming = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAxisEnum_Homming = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHomeMode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvHomeVM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHomeVO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHomeAcc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetHommingPmt_TPM)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSetHommingPmt_TPM
            // 
            this.dgvSetHommingPmt_TPM.AllowUserToAddRows = false;
            this.dgvSetHommingPmt_TPM.AllowUserToDeleteRows = false;
            this.dgvSetHommingPmt_TPM.AllowUserToResizeColumns = false;
            this.dgvSetHommingPmt_TPM.AllowUserToResizeRows = false;
            this.dgvSetHommingPmt_TPM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSetHommingPmt_TPM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetHommingPmt_TPM.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvAxisID_Homming,
            this.dgvAxisEnum_Homming,
            this.dgvHomeMode,
            this.dgvHomeVM,
            this.dgvHomeVO,
            this.dgvHomeAcc});
            this.dgvSetHommingPmt_TPM.Location = new System.Drawing.Point(8, 41);
            this.dgvSetHommingPmt_TPM.MultiSelect = false;
            this.dgvSetHommingPmt_TPM.Name = "dgvSetHommingPmt_TPM";
            this.dgvSetHommingPmt_TPM.RowHeadersVisible = false;
            this.dgvSetHommingPmt_TPM.RowTemplate.Height = 24;
            this.dgvSetHommingPmt_TPM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSetHommingPmt_TPM.Size = new System.Drawing.Size(1173, 504);
            this.dgvSetHommingPmt_TPM.TabIndex = 10;
            this.dgvSetHommingPmt_TPM.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            // 
            // dgvAxisID_Homming
            // 
            this.dgvAxisID_Homming.Frozen = true;
            this.dgvAxisID_Homming.HeaderText = "Axis ID";
            this.dgvAxisID_Homming.Name = "dgvAxisID_Homming";
            this.dgvAxisID_Homming.ReadOnly = true;
            this.dgvAxisID_Homming.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvAxisID_Homming.Width = 70;
            // 
            // dgvAxisEnum_Homming
            // 
            this.dgvAxisEnum_Homming.HeaderText = "Axis Enum";
            this.dgvAxisEnum_Homming.Name = "dgvAxisEnum_Homming";
            this.dgvAxisEnum_Homming.ReadOnly = true;
            // 
            // dgvHomeMode
            // 
            this.dgvHomeMode.HeaderText = "Home Mode";
            this.dgvHomeMode.Name = "dgvHomeMode";
            this.dgvHomeMode.Width = 130;
            // 
            // dgvHomeVM
            // 
            this.dgvHomeVM.HeaderText = "Home VM";
            this.dgvHomeVM.Name = "dgvHomeVM";
            // 
            // dgvHomeVO
            // 
            this.dgvHomeVO.HeaderText = "Home VO";
            this.dgvHomeVO.Name = "dgvHomeVO";
            // 
            // dgvHomeAcc
            // 
            this.dgvHomeAcc.HeaderText = "Home Acc";
            this.dgvHomeAcc.Name = "dgvHomeAcc";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1178, 38);
            this.label2.TabIndex = 11;
            this.label2.Text = "PCI/PICe AdvEtherCAT Homming Setting";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucSetMotionCardPmt_AdvEtherCAT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvSetHommingPmt_TPM);
            this.Name = "ucSetMotionCardPmt_AdvEtherCAT";
            this.Size = new System.Drawing.Size(1184, 548);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetHommingPmt_TPM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSetHommingPmt_TPM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAxisID_Homming;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAxisEnum_Homming;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvHomeMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHomeVM;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHomeVO;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHomeAcc;



    }
}

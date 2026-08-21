namespace ArtSystem.MultiSystem
{
    partial class ucSetMotionCardPmt_TPM
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
            this.dgvSetMotionCartPmt_TPM = new System.Windows.Forms.DataGridView();
            this.dgvSetHommingPmt_TPM = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvAxisID_Homming = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAxisEnum_Homming = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHomeMode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvHomeVM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHomeVO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHomeEZA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHomeOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAxisID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAxisEnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvLogic_ServoOn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvLogic_INP = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvLogic_ALM = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvLogic_EZ = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvLogic_Org = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvLogic_EL = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvELMode = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvCmdPulse = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvEncoderPulse = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvEncoderDirLogic = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvFeedback_Src = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetMotionCartPmt_TPM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetHommingPmt_TPM)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1178, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "PCI/PICe L122 (TPM) Setting";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvSetMotionCartPmt_TPM
            // 
            this.dgvSetMotionCartPmt_TPM.AllowUserToAddRows = false;
            this.dgvSetMotionCartPmt_TPM.AllowUserToDeleteRows = false;
            this.dgvSetMotionCartPmt_TPM.AllowUserToResizeColumns = false;
            this.dgvSetMotionCartPmt_TPM.AllowUserToResizeRows = false;
            this.dgvSetMotionCartPmt_TPM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSetMotionCartPmt_TPM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetMotionCartPmt_TPM.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvAxisID,
            this.dgvAxisEnum,
            this.dgvLogic_ServoOn,
            this.dgvLogic_INP,
            this.dgvLogic_ALM,
            this.dgvLogic_EZ,
            this.dgvLogic_Org,
            this.dgvLogic_EL,
            this.dgvELMode,
            this.dgvCmdPulse,
            this.dgvEncoderPulse,
            this.dgvEncoderDirLogic,
            this.dgvFeedback_Src});
            this.dgvSetMotionCartPmt_TPM.Location = new System.Drawing.Point(8, 41);
            this.dgvSetMotionCartPmt_TPM.MultiSelect = false;
            this.dgvSetMotionCartPmt_TPM.Name = "dgvSetMotionCartPmt_TPM";
            this.dgvSetMotionCartPmt_TPM.RowHeadersVisible = false;
            this.dgvSetMotionCartPmt_TPM.RowTemplate.Height = 24;
            this.dgvSetMotionCartPmt_TPM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSetMotionCartPmt_TPM.Size = new System.Drawing.Size(1173, 240);
            this.dgvSetMotionCartPmt_TPM.TabIndex = 9;
            this.dgvSetMotionCartPmt_TPM.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
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
            this.dgvHomeEZA,
            this.dgvHomeOffset});
            this.dgvSetHommingPmt_TPM.Location = new System.Drawing.Point(8, 325);
            this.dgvSetHommingPmt_TPM.MultiSelect = false;
            this.dgvSetHommingPmt_TPM.Name = "dgvSetHommingPmt_TPM";
            this.dgvSetHommingPmt_TPM.RowHeadersVisible = false;
            this.dgvSetHommingPmt_TPM.RowTemplate.Height = 24;
            this.dgvSetHommingPmt_TPM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSetHommingPmt_TPM.Size = new System.Drawing.Size(1173, 220);
            this.dgvSetHommingPmt_TPM.TabIndex = 10;
            this.dgvSetHommingPmt_TPM.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 284);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1178, 38);
            this.label2.TabIndex = 11;
            this.label2.Text = "PCI/PICe L122 (TPM) Homming Setting";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // dgvHomeEZA
            // 
            this.dgvHomeEZA.HeaderText = "Home EZA";
            this.dgvHomeEZA.Name = "dgvHomeEZA";
            // 
            // dgvHomeOffset
            // 
            this.dgvHomeOffset.HeaderText = "Home Offset";
            this.dgvHomeOffset.Name = "dgvHomeOffset";
            // 
            // dgvAxisID
            // 
            this.dgvAxisID.Frozen = true;
            this.dgvAxisID.HeaderText = "Axis ID";
            this.dgvAxisID.Name = "dgvAxisID";
            this.dgvAxisID.ReadOnly = true;
            this.dgvAxisID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvAxisID.Width = 70;
            // 
            // dgvAxisEnum
            // 
            this.dgvAxisEnum.HeaderText = "Axis Enum";
            this.dgvAxisEnum.Name = "dgvAxisEnum";
            this.dgvAxisEnum.ReadOnly = true;
            // 
            // dgvLogic_ServoOn
            // 
            this.dgvLogic_ServoOn.HeaderText = "ServoOn Logic";
            this.dgvLogic_ServoOn.Name = "dgvLogic_ServoOn";
            this.dgvLogic_ServoOn.Width = 60;
            // 
            // dgvLogic_INP
            // 
            this.dgvLogic_INP.HeaderText = "INP Logic";
            this.dgvLogic_INP.Name = "dgvLogic_INP";
            this.dgvLogic_INP.Width = 60;
            // 
            // dgvLogic_ALM
            // 
            this.dgvLogic_ALM.HeaderText = "ALM Logic";
            this.dgvLogic_ALM.Name = "dgvLogic_ALM";
            this.dgvLogic_ALM.Width = 60;
            // 
            // dgvLogic_EZ
            // 
            this.dgvLogic_EZ.HeaderText = "EZ Logic";
            this.dgvLogic_EZ.Name = "dgvLogic_EZ";
            this.dgvLogic_EZ.Width = 60;
            // 
            // dgvLogic_Org
            // 
            this.dgvLogic_Org.HeaderText = "Org Logic";
            this.dgvLogic_Org.Name = "dgvLogic_Org";
            this.dgvLogic_Org.Width = 60;
            // 
            // dgvLogic_EL
            // 
            this.dgvLogic_EL.HeaderText = "EL Logic";
            this.dgvLogic_EL.Name = "dgvLogic_EL";
            this.dgvLogic_EL.Width = 60;
            // 
            // dgvELMode
            // 
            this.dgvELMode.HeaderText = "EL Mode";
            this.dgvELMode.Name = "dgvELMode";
            // 
            // dgvCmdPulse
            // 
            this.dgvCmdPulse.HeaderText = "Cmd Pulse";
            this.dgvCmdPulse.Name = "dgvCmdPulse";
            this.dgvCmdPulse.Width = 130;
            // 
            // dgvEncoderPulse
            // 
            this.dgvEncoderPulse.HeaderText = "Encoder Pulse";
            this.dgvEncoderPulse.Name = "dgvEncoderPulse";
            this.dgvEncoderPulse.Width = 130;
            // 
            // dgvEncoderDirLogic
            // 
            this.dgvEncoderDirLogic.HeaderText = "Encoder Dir Logic";
            this.dgvEncoderDirLogic.Name = "dgvEncoderDirLogic";
            this.dgvEncoderDirLogic.Width = 80;
            // 
            // dgvFeedback_Src
            // 
            this.dgvFeedback_Src.HeaderText = "Feedback SRC";
            this.dgvFeedback_Src.Name = "dgvFeedback_Src";
            this.dgvFeedback_Src.Width = 130;
            // 
            // ucSetMotionCardPmt_TPM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvSetHommingPmt_TPM);
            this.Controls.Add(this.dgvSetMotionCartPmt_TPM);
            this.Controls.Add(this.label1);
            this.Name = "ucSetMotionCardPmt_TPM";
            this.Size = new System.Drawing.Size(1184, 548);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetMotionCartPmt_TPM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetHommingPmt_TPM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvSetMotionCartPmt_TPM;
        private System.Windows.Forms.DataGridView dgvSetHommingPmt_TPM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAxisID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAxisEnum;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvLogic_ServoOn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvLogic_INP;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvLogic_ALM;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvLogic_EZ;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvLogic_Org;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvLogic_EL;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvELMode;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvCmdPulse;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvEncoderPulse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvEncoderDirLogic;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvFeedback_Src;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAxisID_Homming;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAxisEnum_Homming;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgvHomeMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHomeVM;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHomeVO;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHomeEZA;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHomeOffset;



    }
}

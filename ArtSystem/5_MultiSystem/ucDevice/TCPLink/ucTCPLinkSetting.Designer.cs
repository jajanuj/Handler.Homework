namespace ArtSystem.MultiSystem
{
    partial class ucTCPLinkSetting
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
            this.dgvSetTCPLink = new System.Windows.Forms.DataGridView();
            this.btnSave_TCPLinkSetting = new System.Windows.Forms.Button();
            this.btnCancel_TCPLinkSetting = new System.Windows.Forms.Button();
            this.btnEdit_TCPLinkSetting = new System.Windows.Forms.Button();
            this.btnAdd_TCPLinkSetting = new System.Windows.Forms.Button();
            this.btnDelete_TCPLinkSetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_TCPLinkPath = new System.Windows.Forms.TextBox();
            this.dgvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvReaderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvServer = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvTCPIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTCPPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetTCPLink)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSetTCPLink
            // 
            this.dgvSetTCPLink.AllowUserToAddRows = false;
            this.dgvSetTCPLink.AllowUserToDeleteRows = false;
            this.dgvSetTCPLink.AllowUserToResizeColumns = false;
            this.dgvSetTCPLink.AllowUserToResizeRows = false;
            this.dgvSetTCPLink.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSetTCPLink.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetTCPLink.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvNo,
            this.dgvReaderName,
            this.dgvServer,
            this.dgvTCPIP,
            this.dgvTCPPort});
            this.dgvSetTCPLink.Location = new System.Drawing.Point(8, 61);
            this.dgvSetTCPLink.MultiSelect = false;
            this.dgvSetTCPLink.Name = "dgvSetTCPLink";
            this.dgvSetTCPLink.RowHeadersVisible = false;
            this.dgvSetTCPLink.RowTemplate.Height = 24;
            this.dgvSetTCPLink.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSetTCPLink.Size = new System.Drawing.Size(1173, 508);
            this.dgvSetTCPLink.TabIndex = 10;
            this.dgvSetTCPLink.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            this.dgvSetTCPLink.EnabledChanged += new System.EventHandler(this.dgvSetTCPLink_EnabledChanged);
            // 
            // btnSave_TCPLinkSetting
            // 
            this.btnSave_TCPLinkSetting.Location = new System.Drawing.Point(3, 3);
            this.btnSave_TCPLinkSetting.Name = "btnSave_TCPLinkSetting";
            this.btnSave_TCPLinkSetting.Size = new System.Drawing.Size(100, 52);
            this.btnSave_TCPLinkSetting.TabIndex = 17;
            this.btnSave_TCPLinkSetting.Text = "Save";
            this.btnSave_TCPLinkSetting.UseVisualStyleBackColor = true;
            this.btnSave_TCPLinkSetting.Click += new System.EventHandler(this.btnSave_TCPLinkSetting_Click);
            // 
            // btnCancel_TCPLinkSetting
            // 
            this.btnCancel_TCPLinkSetting.Location = new System.Drawing.Point(109, 3);
            this.btnCancel_TCPLinkSetting.Name = "btnCancel_TCPLinkSetting";
            this.btnCancel_TCPLinkSetting.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_TCPLinkSetting.TabIndex = 16;
            this.btnCancel_TCPLinkSetting.Text = "Cancel";
            this.btnCancel_TCPLinkSetting.UseVisualStyleBackColor = true;
            this.btnCancel_TCPLinkSetting.Click += new System.EventHandler(this.btnCancel_TCPLinkSetting_Click);
            // 
            // btnEdit_TCPLinkSetting
            // 
            this.btnEdit_TCPLinkSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_TCPLinkSetting.Location = new System.Drawing.Point(1081, 3);
            this.btnEdit_TCPLinkSetting.Name = "btnEdit_TCPLinkSetting";
            this.btnEdit_TCPLinkSetting.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_TCPLinkSetting.TabIndex = 18;
            this.btnEdit_TCPLinkSetting.Text = "Edit";
            this.btnEdit_TCPLinkSetting.UseVisualStyleBackColor = true;
            this.btnEdit_TCPLinkSetting.Click += new System.EventHandler(this.btnEdit_TCPLinkSetting_Click);
            // 
            // btnAdd_TCPLinkSetting
            // 
            this.btnAdd_TCPLinkSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd_TCPLinkSetting.Location = new System.Drawing.Point(869, 3);
            this.btnAdd_TCPLinkSetting.Name = "btnAdd_TCPLinkSetting";
            this.btnAdd_TCPLinkSetting.Size = new System.Drawing.Size(100, 52);
            this.btnAdd_TCPLinkSetting.TabIndex = 15;
            this.btnAdd_TCPLinkSetting.Text = "Add";
            this.btnAdd_TCPLinkSetting.UseVisualStyleBackColor = true;
            this.btnAdd_TCPLinkSetting.Click += new System.EventHandler(this.btnAdd_TCPLinkSetting_Click);
            // 
            // btnDelete_TCPLinkSetting
            // 
            this.btnDelete_TCPLinkSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete_TCPLinkSetting.Location = new System.Drawing.Point(975, 3);
            this.btnDelete_TCPLinkSetting.Name = "btnDelete_TCPLinkSetting";
            this.btnDelete_TCPLinkSetting.Size = new System.Drawing.Size(100, 52);
            this.btnDelete_TCPLinkSetting.TabIndex = 14;
            this.btnDelete_TCPLinkSetting.Text = "Delete";
            this.btnDelete_TCPLinkSetting.UseVisualStyleBackColor = true;
            this.btnDelete_TCPLinkSetting.Click += new System.EventHandler(this.btnDelete_TCPLinkSetting_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "File Path:";
            // 
            // txt_TCPLinkPath
            // 
            this.txt_TCPLinkPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_TCPLinkPath.Location = new System.Drawing.Point(215, 32);
            this.txt_TCPLinkPath.Name = "txt_TCPLinkPath";
            this.txt_TCPLinkPath.Size = new System.Drawing.Size(648, 23);
            this.txt_TCPLinkPath.TabIndex = 19;
            // 
            // dgvNo
            // 
            this.dgvNo.HeaderText = "No.";
            this.dgvNo.Name = "dgvNo";
            this.dgvNo.ReadOnly = true;
            this.dgvNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvNo.Width = 40;
            // 
            // dgvReaderName
            // 
            this.dgvReaderName.HeaderText = "Reader Name";
            this.dgvReaderName.Name = "dgvReaderName";
            this.dgvReaderName.ReadOnly = true;
            this.dgvReaderName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvReaderName.Width = 150;
            // 
            // dgvServer
            // 
            this.dgvServer.HeaderText = "Server";
            this.dgvServer.Name = "dgvServer";
            // 
            // dgvTCPIP
            // 
            this.dgvTCPIP.HeaderText = "TCP IP";
            this.dgvTCPIP.Name = "dgvTCPIP";
            this.dgvTCPIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvTCPIP.Width = 150;
            // 
            // dgvTCPPort
            // 
            this.dgvTCPPort.HeaderText = "TCP Port";
            this.dgvTCPPort.Name = "dgvTCPPort";
            this.dgvTCPPort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ucTCPLinkSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_TCPLinkPath);
            this.Controls.Add(this.btnSave_TCPLinkSetting);
            this.Controls.Add(this.btnCancel_TCPLinkSetting);
            this.Controls.Add(this.btnEdit_TCPLinkSetting);
            this.Controls.Add(this.btnAdd_TCPLinkSetting);
            this.Controls.Add(this.btnDelete_TCPLinkSetting);
            this.Controls.Add(this.dgvSetTCPLink);
            this.Name = "ucTCPLinkSetting";
            this.Size = new System.Drawing.Size(1184, 572);
            this.VisibleChanged += new System.EventHandler(this.ucTCPLinkSetting_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetTCPLink)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSetTCPLink;
        private System.Windows.Forms.Button btnSave_TCPLinkSetting;
        private System.Windows.Forms.Button btnCancel_TCPLinkSetting;
        private System.Windows.Forms.Button btnEdit_TCPLinkSetting;
        private System.Windows.Forms.Button btnAdd_TCPLinkSetting;
        private System.Windows.Forms.Button btnDelete_TCPLinkSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_TCPLinkPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvReaderName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvServer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTCPIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTCPPort;



    }
}

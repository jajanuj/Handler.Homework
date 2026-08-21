namespace ArtSystem
{
    partial class ucRemoveFiles
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgvEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvDeleteSubFolder = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvToRecycleBin = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDeletePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.cBox_ClearRecycleBinOldFiles = new System.Windows.Forms.CheckBox();
            this.tBox_RecycleBinOldFiles_Days = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 52);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(109, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 52);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(893, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 52);
            this.btnEdit.TabIndex = 21;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvEnable,
            this.dgvDeleteSubFolder,
            this.dgvToRecycleBin,
            this.dgvDays,
            this.dgvDeletePath,
            this.dgvItemName});
            this.dataGridView1.Location = new System.Drawing.Point(3, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(990, 435);
            this.dataGridView1.TabIndex = 22;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseUp);
            // 
            // dgvEnable
            // 
            this.dgvEnable.Frozen = true;
            this.dgvEnable.HeaderText = "Enable";
            this.dgvEnable.Name = "dgvEnable";
            this.dgvEnable.Width = 60;
            // 
            // dgvDeleteSubFolder
            // 
            this.dgvDeleteSubFolder.Frozen = true;
            this.dgvDeleteSubFolder.HeaderText = "Delete Sub Folder";
            this.dgvDeleteSubFolder.Name = "dgvDeleteSubFolder";
            this.dgvDeleteSubFolder.Width = 90;
            // 
            // dgvToRecycleBin
            // 
            this.dgvToRecycleBin.Frozen = true;
            this.dgvToRecycleBin.HeaderText = "To Recycle Bin";
            this.dgvToRecycleBin.Name = "dgvToRecycleBin";
            this.dgvToRecycleBin.Width = 90;
            // 
            // dgvDays
            // 
            this.dgvDays.Frozen = true;
            this.dgvDays.HeaderText = "Days";
            this.dgvDays.Name = "dgvDays";
            this.dgvDays.ReadOnly = true;
            this.dgvDays.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvDays.Width = 80;
            // 
            // dgvDeletePath
            // 
            this.dgvDeletePath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvDeletePath.HeaderText = "Delete Path";
            this.dgvDeletePath.Name = "dgvDeletePath";
            this.dgvDeletePath.ReadOnly = true;
            this.dgvDeletePath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvItemName
            // 
            this.dgvItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvItemName.HeaderText = "Detele Items";
            this.dgvItemName.Name = "dgvItemName";
            this.dgvItemName.ReadOnly = true;
            this.dgvItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(787, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 52);
            this.btnDelete.TabIndex = 23;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(681, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 52);
            this.btnAdd.TabIndex = 24;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "File Path:";
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(292, 32);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(383, 23);
            this.txtPath.TabIndex = 25;
            // 
            // cBox_ClearRecycleBinOldFiles
            // 
            this.cBox_ClearRecycleBinOldFiles.AutoSize = true;
            this.cBox_ClearRecycleBinOldFiles.Location = new System.Drawing.Point(292, 7);
            this.cBox_ClearRecycleBinOldFiles.Name = "cBox_ClearRecycleBinOldFiles";
            this.cBox_ClearRecycleBinOldFiles.Size = new System.Drawing.Size(174, 20);
            this.cBox_ClearRecycleBinOldFiles.TabIndex = 27;
            this.cBox_ClearRecycleBinOldFiles.Text = "Clear Recycle Bin Files";
            this.cBox_ClearRecycleBinOldFiles.UseVisualStyleBackColor = true;
            this.cBox_ClearRecycleBinOldFiles.Visible = false;
            this.cBox_ClearRecycleBinOldFiles.Click += new System.EventHandler(this.cBox_ClearRecycleBinOldFiles_Click);
            // 
            // tBox_RecycleBinOldFiles_Days
            // 
            this.tBox_RecycleBinOldFiles_Days.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tBox_RecycleBinOldFiles_Days.Location = new System.Drawing.Point(596, 5);
            this.tBox_RecycleBinOldFiles_Days.Name = "tBox_RecycleBinOldFiles_Days";
            this.tBox_RecycleBinOldFiles_Days.ReadOnly = true;
            this.tBox_RecycleBinOldFiles_Days.Size = new System.Drawing.Size(79, 23);
            this.tBox_RecycleBinOldFiles_Days.TabIndex = 28;
            this.tBox_RecycleBinOldFiles_Days.Visible = false;
            this.tBox_RecycleBinOldFiles_Days.Click += new System.EventHandler(this.tBox_RecycleBinOldFiles_Days_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(472, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 23);
            this.label2.TabIndex = 29;
            this.label2.Text = "Deleted Days:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Visible = false;
            // 
            // ucRemoveFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tBox_RecycleBinOldFiles_Days);
            this.Controls.Add(this.cBox_ClearRecycleBinOldFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.label2);
            this.Name = "ucRemoveFiles";
            this.Size = new System.Drawing.Size(996, 499);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvEnable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvDeleteSubFolder;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvToRecycleBin;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDays;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDeletePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvItemName;
        private System.Windows.Forms.CheckBox cBox_ClearRecycleBinOldFiles;
        private System.Windows.Forms.TextBox tBox_RecycleBinOldFiles_Days;
        private System.Windows.Forms.Label label2;
    }
}

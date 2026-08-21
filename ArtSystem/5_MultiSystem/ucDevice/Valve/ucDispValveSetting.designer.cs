namespace ArtSystem.MultiSystem
{
    partial class ucDispValveSetting
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
            this.dgvSetDispValve = new System.Windows.Forms.DataGridView();
            this.btnSave_HeaterModuleSetting = new System.Windows.Forms.Button();
            this.btnCancel_HeaterModuleSetting = new System.Windows.Forms.Button();
            this.btnEdit_HeaterModuleSetting = new System.Windows.Forms.Button();
            this.btnAdd_HeaterModuleSetting = new System.Windows.Forms.Button();
            this.btnDelete_HeaterModuleSetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_HeaterModulePath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetDispValve)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSetDispValve
            // 
            this.dgvSetDispValve.AllowUserToAddRows = false;
            this.dgvSetDispValve.AllowUserToDeleteRows = false;
            this.dgvSetDispValve.AllowUserToResizeColumns = false;
            this.dgvSetDispValve.AllowUserToResizeRows = false;
            this.dgvSetDispValve.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSetDispValve.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetDispValve.ColumnHeadersVisible = false;
            this.dgvSetDispValve.Location = new System.Drawing.Point(8, 61);
            this.dgvSetDispValve.MultiSelect = false;
            this.dgvSetDispValve.Name = "dgvSetDispValve";
            this.dgvSetDispValve.RowHeadersVisible = false;
            this.dgvSetDispValve.RowTemplate.Height = 24;
            this.dgvSetDispValve.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSetDispValve.Size = new System.Drawing.Size(1173, 508);
            this.dgvSetDispValve.TabIndex = 10;
            this.dgvSetDispValve.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSetHeaterModule_CellClick);
            this.dgvSetDispValve.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            this.dgvSetDispValve.EnabledChanged += new System.EventHandler(this.dgvSetHeaterModule_EnabledChanged);
            // 
            // btnSave_HeaterModuleSetting
            // 
            this.btnSave_HeaterModuleSetting.Location = new System.Drawing.Point(3, 3);
            this.btnSave_HeaterModuleSetting.Name = "btnSave_HeaterModuleSetting";
            this.btnSave_HeaterModuleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnSave_HeaterModuleSetting.TabIndex = 17;
            this.btnSave_HeaterModuleSetting.Text = "Save";
            this.btnSave_HeaterModuleSetting.UseVisualStyleBackColor = true;
            this.btnSave_HeaterModuleSetting.Click += new System.EventHandler(this.btnSave_HeaterModuleSetting_Click);
            // 
            // btnCancel_HeaterModuleSetting
            // 
            this.btnCancel_HeaterModuleSetting.Location = new System.Drawing.Point(109, 3);
            this.btnCancel_HeaterModuleSetting.Name = "btnCancel_HeaterModuleSetting";
            this.btnCancel_HeaterModuleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnCancel_HeaterModuleSetting.TabIndex = 16;
            this.btnCancel_HeaterModuleSetting.Text = "Cancel";
            this.btnCancel_HeaterModuleSetting.UseVisualStyleBackColor = true;
            this.btnCancel_HeaterModuleSetting.Click += new System.EventHandler(this.btnCancel_HeaterModuleSetting_Click);
            // 
            // btnEdit_HeaterModuleSetting
            // 
            this.btnEdit_HeaterModuleSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit_HeaterModuleSetting.Location = new System.Drawing.Point(1081, 3);
            this.btnEdit_HeaterModuleSetting.Name = "btnEdit_HeaterModuleSetting";
            this.btnEdit_HeaterModuleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnEdit_HeaterModuleSetting.TabIndex = 18;
            this.btnEdit_HeaterModuleSetting.Text = "Edit";
            this.btnEdit_HeaterModuleSetting.UseVisualStyleBackColor = true;
            this.btnEdit_HeaterModuleSetting.Click += new System.EventHandler(this.btnEdit_HeaterModuleSetting_Click);
            // 
            // btnAdd_HeaterModuleSetting
            // 
            this.btnAdd_HeaterModuleSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd_HeaterModuleSetting.Location = new System.Drawing.Point(869, 3);
            this.btnAdd_HeaterModuleSetting.Name = "btnAdd_HeaterModuleSetting";
            this.btnAdd_HeaterModuleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnAdd_HeaterModuleSetting.TabIndex = 15;
            this.btnAdd_HeaterModuleSetting.Text = "Add";
            this.btnAdd_HeaterModuleSetting.UseVisualStyleBackColor = true;
            this.btnAdd_HeaterModuleSetting.Click += new System.EventHandler(this.btnAdd_HeaterModuleSetting_Click);
            // 
            // btnDelete_HeaterModuleSetting
            // 
            this.btnDelete_HeaterModuleSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete_HeaterModuleSetting.Location = new System.Drawing.Point(975, 3);
            this.btnDelete_HeaterModuleSetting.Name = "btnDelete_HeaterModuleSetting";
            this.btnDelete_HeaterModuleSetting.Size = new System.Drawing.Size(100, 52);
            this.btnDelete_HeaterModuleSetting.TabIndex = 14;
            this.btnDelete_HeaterModuleSetting.Text = "Delete";
            this.btnDelete_HeaterModuleSetting.UseVisualStyleBackColor = true;
            this.btnDelete_HeaterModuleSetting.Click += new System.EventHandler(this.btnDelete_HeaterModuleSetting_Click);
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
            // txt_HeaterModulePath
            // 
            this.txt_HeaterModulePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_HeaterModulePath.Location = new System.Drawing.Point(215, 32);
            this.txt_HeaterModulePath.Name = "txt_HeaterModulePath";
            this.txt_HeaterModulePath.Size = new System.Drawing.Size(648, 23);
            this.txt_HeaterModulePath.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(707, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 25);
            this.button1.TabIndex = 21;
            this.button1.Text = "Advance Setting";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucDispValveSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_HeaterModulePath);
            this.Controls.Add(this.btnSave_HeaterModuleSetting);
            this.Controls.Add(this.btnCancel_HeaterModuleSetting);
            this.Controls.Add(this.btnEdit_HeaterModuleSetting);
            this.Controls.Add(this.btnAdd_HeaterModuleSetting);
            this.Controls.Add(this.btnDelete_HeaterModuleSetting);
            this.Controls.Add(this.dgvSetDispValve);
            this.Name = "ucDispValveSetting";
            this.Size = new System.Drawing.Size(1184, 572);
            this.VisibleChanged += new System.EventHandler(this.ucHeaterModuleSetting_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetDispValve)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSetDispValve;
        private System.Windows.Forms.Button btnSave_HeaterModuleSetting;
        private System.Windows.Forms.Button btnCancel_HeaterModuleSetting;
        private System.Windows.Forms.Button btnEdit_HeaterModuleSetting;
        private System.Windows.Forms.Button btnAdd_HeaterModuleSetting;
        private System.Windows.Forms.Button btnDelete_HeaterModuleSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_HeaterModulePath;
        private System.Windows.Forms.Button button1;



    }
}

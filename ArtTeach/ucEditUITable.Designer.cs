namespace ArtTeach
{
    partial class ucEditUITable
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
            if (disposing && (components != null) ) 
            {
                components.Dispose() ;
            }
            base.Dispose(disposing) ;
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent() 
        {
            this.tbEditUIData = new System.Windows.Forms.TabControl();
            this.tbJogAndTeach = new System.Windows.Forms.TabPage();
            this.dgvJogAndTeach = new System.Windows.Forms.DataGridView();
            this.tbDIO = new System.Windows.Forms.TabPage();
            this.dgvDIO = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.BtnPasteDIO = new System.Windows.Forms.Button();
            this.BtnCopyDIO = new System.Windows.Forms.Button();
            this.tbEditUIData.SuspendLayout();
            this.tbJogAndTeach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJogAndTeach)).BeginInit();
            this.tbDIO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDIO)).BeginInit();
            this.SuspendLayout();
            // 
            // tbEditUIData
            // 
            this.tbEditUIData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEditUIData.Controls.Add(this.tbJogAndTeach);
            this.tbEditUIData.Controls.Add(this.tbDIO);
            this.tbEditUIData.Location = new System.Drawing.Point(3, 13);
            this.tbEditUIData.Name = "tbEditUIData";
            this.tbEditUIData.SelectedIndex = 0;
            this.tbEditUIData.Size = new System.Drawing.Size(505, 484);
            this.tbEditUIData.TabIndex = 1121;
            // 
            // tbJogAndTeach
            // 
            this.tbJogAndTeach.Controls.Add(this.dgvJogAndTeach);
            this.tbJogAndTeach.Location = new System.Drawing.Point(4, 22);
            this.tbJogAndTeach.Name = "tbJogAndTeach";
            this.tbJogAndTeach.Padding = new System.Windows.Forms.Padding(3);
            this.tbJogAndTeach.Size = new System.Drawing.Size(497, 458);
            this.tbJogAndTeach.TabIndex = 0;
            this.tbJogAndTeach.Text = "Jog And Teach";
            this.tbJogAndTeach.UseVisualStyleBackColor = true;
            // 
            // dgvJogAndTeach
            // 
            this.dgvJogAndTeach.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvJogAndTeach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvJogAndTeach.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvJogAndTeach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJogAndTeach.Location = new System.Drawing.Point(0, 0);
            this.dgvJogAndTeach.MultiSelect = false;
            this.dgvJogAndTeach.Name = "dgvJogAndTeach";
            this.dgvJogAndTeach.ReadOnly = true;
            this.dgvJogAndTeach.RowTemplate.Height = 24;
            this.dgvJogAndTeach.Size = new System.Drawing.Size(497, 458);
            this.dgvJogAndTeach.TabIndex = 1113;
            this.dgvJogAndTeach.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJogAndTeach_CellClick);
            this.dgvJogAndTeach.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvJogAndTeach_CellValueChanged);
            // 
            // tbDIO
            // 
            this.tbDIO.Controls.Add(this.dgvDIO);
            this.tbDIO.Location = new System.Drawing.Point(4, 22);
            this.tbDIO.Name = "tbDIO";
            this.tbDIO.Padding = new System.Windows.Forms.Padding(3);
            this.tbDIO.Size = new System.Drawing.Size(497, 458);
            this.tbDIO.TabIndex = 1;
            this.tbDIO.Text = "DIO";
            this.tbDIO.UseVisualStyleBackColor = true;
            // 
            // dgvDIO
            // 
            this.dgvDIO.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDIO.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvDIO.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvDIO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDIO.Location = new System.Drawing.Point(0, 0);
            this.dgvDIO.MultiSelect = false;
            this.dgvDIO.Name = "dgvDIO";
            this.dgvDIO.ReadOnly = true;
            this.dgvDIO.RowTemplate.Height = 24;
            this.dgvDIO.Size = new System.Drawing.Size(497, 458);
            this.dgvDIO.TabIndex = 1118;
            this.dgvDIO.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDIO_CellClick);
            this.dgvDIO.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDIO_CellValueChanged);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(444, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(60, 27);
            this.btnExit.TabIndex = 1122;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // BtnPasteDIO
            // 
            this.BtnPasteDIO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPasteDIO.Location = new System.Drawing.Point(358, 3);
            this.BtnPasteDIO.Name = "BtnPasteDIO";
            this.BtnPasteDIO.Size = new System.Drawing.Size(80, 27);
            this.BtnPasteDIO.TabIndex = 1123;
            this.BtnPasteDIO.Text = "Paste DIO";
            this.BtnPasteDIO.UseVisualStyleBackColor = true;
            this.BtnPasteDIO.Click += new System.EventHandler(this.BtnPaste_Click);
            // 
            // BtnCopyDIO
            // 
            this.BtnCopyDIO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCopyDIO.Location = new System.Drawing.Point(272, 3);
            this.BtnCopyDIO.Name = "BtnCopyDIO";
            this.BtnCopyDIO.Size = new System.Drawing.Size(80, 27);
            this.BtnCopyDIO.TabIndex = 1124;
            this.BtnCopyDIO.Text = "Copy DIO";
            this.BtnCopyDIO.UseVisualStyleBackColor = true;
            this.BtnCopyDIO.Click += new System.EventHandler(this.BtnCopy_Click);
            // 
            // ucEditUITable
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.BtnCopyDIO);
            this.Controls.Add(this.BtnPasteDIO);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tbEditUIData);
            this.Name = "ucEditUITable";
            this.Size = new System.Drawing.Size(511, 500);
            this.tbEditUIData.ResumeLayout(false);
            this.tbJogAndTeach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJogAndTeach)).EndInit();
            this.tbDIO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDIO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbEditUIData;
        private System.Windows.Forms.TabPage tbJogAndTeach;
        private System.Windows.Forms.DataGridView dgvJogAndTeach;
        private System.Windows.Forms.TabPage tbDIO;
        private System.Windows.Forms.DataGridView dgvDIO;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button BtnPasteDIO;
        private System.Windows.Forms.Button BtnCopyDIO;
    }
}

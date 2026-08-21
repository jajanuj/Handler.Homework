namespace ArtEQ
{
    partial class ucRecipeManager
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCopyProduct = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comTextMapRecipePath = new ArtControlLib.comTextBox();
            this.btnSetMapRecipetPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textSelRecipeName = new System.Windows.Forms.TextBox();
            this.btnLoadProduct = new System.Windows.Forms.Button();
            this.btnDelProduct = new System.Windows.Forms.Button();
            this.comTextInspRecipe = new ArtControlLib.comTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RecipeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecipeName,
            this.FilePath});
            this.dataGridView1.Location = new System.Drawing.Point(4, 49);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(640, 377);
            this.dataGridView1.TabIndex = 41;
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(10, 59);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Current Recipe :";
            // 
            // btnCopyProduct
            // 
            this.btnCopyProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyProduct.BackColor = System.Drawing.Color.White;
            this.btnCopyProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCopyProduct.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCopyProduct.ForeColor = System.Drawing.Color.Black;
            this.btnCopyProduct.Location = new System.Drawing.Point(560, 16);
            this.btnCopyProduct.Margin = new System.Windows.Forms.Padding(2);
            this.btnCopyProduct.Name = "btnCopyProduct";
            this.btnCopyProduct.Size = new System.Drawing.Size(70, 28);
            this.btnCopyProduct.TabIndex = 40;
            this.btnCopyProduct.Text = "Copy";
            this.btnCopyProduct.UseVisualStyleBackColor = false;
            this.btnCopyProduct.Click += new System.EventHandler(this.btnCopyProduct_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.comTextMapRecipePath);
            this.groupBox1.Controls.Add(this.btnSetMapRecipetPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(650, 43);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Directory Setting";
            // 
            // comTextMapRecipePath
            // 
            this.comTextMapRecipePath._DefaultValue = "D:\\\\Recipe\\\\";
            this.comTextMapRecipePath._IsPassWord = false;
            this.comTextMapRecipePath._IsSaveToIni = true;
            this.comTextMapRecipePath._IsSaveToLog = true;
            this.comTextMapRecipePath._IsShowCurrentValue = false;
            this.comTextMapRecipePath._IsShowPopForm = false;
            this.comTextMapRecipePath._OnlyNumerical = false;
            this.comTextMapRecipePath._PmtName = null;
            this.comTextMapRecipePath._PmtType = null;
            this.comTextMapRecipePath._Value = "D:\\Recipe\\";
            this.comTextMapRecipePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comTextMapRecipePath.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comTextMapRecipePath.Location = new System.Drawing.Point(102, 16);
            this.comTextMapRecipePath.Margin = new System.Windows.Forms.Padding(2);
            this.comTextMapRecipePath.Name = "comTextMapRecipePath";
            this.comTextMapRecipePath.ReadOnly = true;
            this.comTextMapRecipePath.Size = new System.Drawing.Size(493, 23);
            this.comTextMapRecipePath.TabIndex = 6;
            this.comTextMapRecipePath.Text = "D:\\Recipe\\";
            this.comTextMapRecipePath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSetMapRecipetPath
            // 
            this.btnSetMapRecipetPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetMapRecipetPath.BackColor = System.Drawing.SystemColors.Control;
            this.btnSetMapRecipetPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSetMapRecipetPath.ForeColor = System.Drawing.Color.Black;
            this.btnSetMapRecipetPath.Location = new System.Drawing.Point(598, 16);
            this.btnSetMapRecipetPath.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetMapRecipetPath.Name = "btnSetMapRecipetPath";
            this.btnSetMapRecipetPath.Size = new System.Drawing.Size(46, 23);
            this.btnSetMapRecipetPath.TabIndex = 7;
            this.btnSetMapRecipetPath.Text = "...";
            this.btnSetMapRecipetPath.UseVisualStyleBackColor = true;
            this.btnSetMapRecipetPath.Click += new System.EventHandler(this.btnSetMapRecipetPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Recipe File Path :";
            // 
            // textSelRecipeName
            // 
            this.textSelRecipeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textSelRecipeName.BackColor = System.Drawing.Color.Blue;
            this.textSelRecipeName.ForeColor = System.Drawing.Color.Lime;
            this.textSelRecipeName.Location = new System.Drawing.Point(101, 22);
            this.textSelRecipeName.Margin = new System.Windows.Forms.Padding(2);
            this.textSelRecipeName.Name = "textSelRecipeName";
            this.textSelRecipeName.Size = new System.Drawing.Size(225, 23);
            this.textSelRecipeName.TabIndex = 39;
            this.textSelRecipeName.Text = "Recipe Name";
            this.textSelRecipeName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textSelRecipeName.TextChanged += new System.EventHandler(this.textSelRecipeName_TextChanged);
            // 
            // btnLoadProduct
            // 
            this.btnLoadProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadProduct.BackColor = System.Drawing.Color.White;
            this.btnLoadProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoadProduct.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnLoadProduct.ForeColor = System.Drawing.Color.Black;
            this.btnLoadProduct.Location = new System.Drawing.Point(411, 16);
            this.btnLoadProduct.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadProduct.Name = "btnLoadProduct";
            this.btnLoadProduct.Size = new System.Drawing.Size(70, 28);
            this.btnLoadProduct.TabIndex = 6;
            this.btnLoadProduct.Text = "Load";
            this.btnLoadProduct.UseVisualStyleBackColor = false;
            this.btnLoadProduct.Click += new System.EventHandler(this.btnLoadProduct_Click);
            // 
            // btnDelProduct
            // 
            this.btnDelProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelProduct.BackColor = System.Drawing.Color.White;
            this.btnDelProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelProduct.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDelProduct.ForeColor = System.Drawing.Color.Black;
            this.btnDelProduct.Location = new System.Drawing.Point(485, 16);
            this.btnDelProduct.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelProduct.Name = "btnDelProduct";
            this.btnDelProduct.Size = new System.Drawing.Size(70, 28);
            this.btnDelProduct.TabIndex = 5;
            this.btnDelProduct.Text = "Delete";
            this.btnDelProduct.UseVisualStyleBackColor = false;
            this.btnDelProduct.Click += new System.EventHandler(this.btnDelProduct_Click);
            // 
            // comTextInspRecipe
            // 
            this.comTextInspRecipe._DefaultValue = null;
            this.comTextInspRecipe._IsPassWord = false;
            this.comTextInspRecipe._IsSaveToIni = true;
            this.comTextInspRecipe._IsSaveToLog = false;
            this.comTextInspRecipe._IsShowCurrentValue = false;
            this.comTextInspRecipe._IsShowPopForm = false;
            this.comTextInspRecipe._OnlyNumerical = false;
            this.comTextInspRecipe._PmtName = null;
            this.comTextInspRecipe._PmtType = null;
            this.comTextInspRecipe._Value = "";
            this.comTextInspRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comTextInspRecipe.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comTextInspRecipe.Location = new System.Drawing.Point(105, 57);
            this.comTextInspRecipe.Margin = new System.Windows.Forms.Padding(2);
            this.comTextInspRecipe.Name = "comTextInspRecipe";
            this.comTextInspRecipe.ReadOnly = true;
            this.comTextInspRecipe.Size = new System.Drawing.Size(541, 23);
            this.comTextInspRecipe.TabIndex = 21;
            this.comTextInspRecipe.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.textSelRecipeName);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.btnCopyProduct);
            this.groupBox2.Controls.Add(this.btnLoadProduct);
            this.groupBox2.Controls.Add(this.btnDelProduct);
            this.groupBox2.Controls.Add(this.btnAddProduct);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(2, 79);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(650, 431);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Product Manage";
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddProduct.BackColor = System.Drawing.Color.White;
            this.btnAddProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddProduct.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAddProduct.ForeColor = System.Drawing.Color.Black;
            this.btnAddProduct.Location = new System.Drawing.Point(337, 16);
            this.btnAddProduct.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(70, 28);
            this.btnAddProduct.TabIndex = 2;
            this.btnAddProduct.Text = "New";
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(8, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Recipe Name :";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Inspect Recipe Name";
            this.columnHeader1.Width = 859;
            // 
            // RecipeName
            // 
            this.RecipeName.HeaderText = "Recipe Name";
            this.RecipeName.Name = "RecipeName";
            this.RecipeName.Width = 200;
            // 
            // FilePath
            // 
            this.FilePath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FilePath.HeaderText = "File Path";
            this.FilePath.Name = "FilePath";
            // 
            // ucRecipeManager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.comTextInspRecipe);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "ucRecipeManager";
            this.Size = new System.Drawing.Size(654, 512);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCopyProduct;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSetMapRecipetPath;
        private ArtControlLib.comTextBox comTextMapRecipePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSelRecipeName;
        private System.Windows.Forms.Button btnLoadProduct;
        private System.Windows.Forms.Button btnDelProduct;
        private ArtControlLib.comTextBox comTextInspRecipe;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecipeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
    }
}

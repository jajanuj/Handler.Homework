namespace ArtEQ
{
    partial class formLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlLogInMode = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnPassword_ShowKeyBoard = new System.Windows.Forms.Button();
            this.btnUserName_ShowKeyBoard = new System.Windows.Forms.Button();
            this.btnOperator1 = new System.Windows.Forms.Button();
            this.palPassWD = new System.Windows.Forms.Panel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPassWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.tabControlLogInMode.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlLogInMode
            // 
            this.tabControlLogInMode.Controls.Add(this.tabPage2);
            this.tabControlLogInMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlLogInMode.ItemSize = new System.Drawing.Size(120, 35);
            this.tabControlLogInMode.Location = new System.Drawing.Point(0, 0);
            this.tabControlLogInMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControlLogInMode.Name = "tabControlLogInMode";
            this.tabControlLogInMode.SelectedIndex = 0;
            this.tabControlLogInMode.Size = new System.Drawing.Size(255, 361);
            this.tabControlLogInMode.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlLogInMode.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnPassword_ShowKeyBoard);
            this.tabPage2.Controls.Add(this.btnUserName_ShowKeyBoard);
            this.tabPage2.Controls.Add(this.btnOperator1);
            this.tabPage2.Controls.Add(this.palPassWD);
            this.tabPage2.Controls.Add(this.btnLogin);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.textBoxPassWord);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.textBoxUserName);
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage2.Size = new System.Drawing.Size(247, 318);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "User ID";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnPassword_ShowKeyBoard
            // 
            this.btnPassword_ShowKeyBoard.Font = new System.Drawing.Font("新細明體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPassword_ShowKeyBoard.Location = new System.Drawing.Point(194, 116);
            this.btnPassword_ShowKeyBoard.Name = "btnPassword_ShowKeyBoard";
            this.btnPassword_ShowKeyBoard.Size = new System.Drawing.Size(43, 23);
            this.btnPassword_ShowKeyBoard.TabIndex = 10;
            this.btnPassword_ShowKeyBoard.Text = "KB";
            this.btnPassword_ShowKeyBoard.UseVisualStyleBackColor = true;
            this.btnPassword_ShowKeyBoard.Click += new System.EventHandler(this.btnPassword_ShowKeyBoard_Click);
            // 
            // btnUserName_ShowKeyBoard
            // 
            this.btnUserName_ShowKeyBoard.Font = new System.Drawing.Font("新細明體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserName_ShowKeyBoard.Location = new System.Drawing.Point(194, 60);
            this.btnUserName_ShowKeyBoard.Name = "btnUserName_ShowKeyBoard";
            this.btnUserName_ShowKeyBoard.Size = new System.Drawing.Size(43, 23);
            this.btnUserName_ShowKeyBoard.TabIndex = 9;
            this.btnUserName_ShowKeyBoard.Text = "KB";
            this.btnUserName_ShowKeyBoard.UseVisualStyleBackColor = true;
            this.btnUserName_ShowKeyBoard.Click += new System.EventHandler(this.btnUserName_ShowKeyBoard_Click);
            // 
            // btnOperator1
            // 
            this.btnOperator1.Location = new System.Drawing.Point(18, 250);
            this.btnOperator1.Name = "btnOperator1";
            this.btnOperator1.Size = new System.Drawing.Size(200, 50);
            this.btnOperator1.TabIndex = 8;
            this.btnOperator1.Tag = "operator";
            this.btnOperator1.Text = "Operator";
            this.btnOperator1.UseVisualStyleBackColor = true;
            this.btnOperator1.Click += new System.EventHandler(this.btnOperator1_Click);
            // 
            // palPassWD
            // 
            this.palPassWD.Location = new System.Drawing.Point(164, 3);
            this.palPassWD.Name = "palPassWD";
            this.palPassWD.Size = new System.Drawing.Size(76, 50);
            this.palPassWD.TabIndex = 7;
            this.palPassWD.MouseDown += new System.Windows.Forms.MouseEventHandler(this.palPassWD_MouseDown);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(18, 151);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(200, 50);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // textBoxPassWord
            // 
            this.textBoxPassWord.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassWord.Location = new System.Drawing.Point(18, 116);
            this.textBoxPassWord.Name = "textBoxPassWord";
            this.textBoxPassWord.PasswordChar = '*';
            this.textBoxPassWord.Size = new System.Drawing.Size(170, 27);
            this.textBoxPassWord.TabIndex = 2;
            this.textBoxPassWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "User Name";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUserName.Location = new System.Drawing.Point(18, 59);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(170, 27);
            this.textBoxUserName.TabIndex = 0;
            this.textBoxUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownEnter);
            // 
            // formLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 361);
            this.ControlBox = false;
            this.Controls.Add(this.tabControlLogInMode);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "formLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formLogin_FormClosing);
            this.tabControlLogInMode.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlLogInMode;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPassWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel palPassWD;
        private System.Windows.Forms.Button btnOperator1;
        private System.Windows.Forms.Button btnPassword_ShowKeyBoard;
        private System.Windows.Forms.Button btnUserName_ShowKeyBoard;
    }
}

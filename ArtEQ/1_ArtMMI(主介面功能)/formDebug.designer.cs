namespace ArtEQ
{
    partial class formDebug
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
            this.tabControlDebug = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControlDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlDebug
            // 
            this.tabControlDebug.Controls.Add(this.tabPage1);
            this.tabControlDebug.Controls.Add(this.tabPage2);
            this.tabControlDebug.Controls.Add(this.tabPage3);
            this.tabControlDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDebug.ItemSize = new System.Drawing.Size(120, 35);
            this.tabControlDebug.Location = new System.Drawing.Point(0, 0);
            this.tabControlDebug.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControlDebug.Name = "tabControlDebug";
            this.tabControlDebug.SelectedIndex = 0;
            this.tabControlDebug.Size = new System.Drawing.Size(784, 561);
            this.tabControlDebug.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlDebug.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabPage1.Size = new System.Drawing.Size(776, 518);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Thread Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(776, 518);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Dio Monitor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "▲";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(195, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "▼";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 39);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(776, 518);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Module Step Log";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // formDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControlDebug);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "formDebug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Debug Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formDebug_FormClosing);
            this.tabControlDebug.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlDebug;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;

    }
}
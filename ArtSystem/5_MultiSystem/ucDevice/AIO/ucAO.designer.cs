namespace ArtSystem.MultiSystem
{
    partial class ucAO {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.tb_ChannelIndex = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_SettingValue1 = new System.Windows.Forms.TextBox();
            this.tb_SettingValue2 = new System.Windows.Forms.TextBox();
            this.tb_HeadValue2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.tb_Offset = new System.Windows.Forms.TextBox();
            this.tb_Gain = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_NowValue = new System.Windows.Forms.TextBox();
            this.tb_HeadValue1 = new System.Windows.Forms.TextBox();
            this.tb_Shift = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnSavePmt_AO = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgvAIID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAIDes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAIValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(314, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "DO:  ";
            // 
            // tb_ChannelIndex
            // 
            this.tb_ChannelIndex.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_ChannelIndex.Location = new System.Drawing.Point(362, 3);
            this.tb_ChannelIndex.Name = "tb_ChannelIndex";
            this.tb_ChannelIndex.ReadOnly = true;
            this.tb_ChannelIndex.Size = new System.Drawing.Size(123, 27);
            this.tb_ChannelIndex.TabIndex = 1;
            this.tb_ChannelIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(3, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Data1：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(253, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Data2：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(3, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Device Value:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(3, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "AI Value(Org):";
            // 
            // tb_SettingValue1
            // 
            this.tb_SettingValue1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_SettingValue1.Location = new System.Drawing.Point(112, 66);
            this.tb_SettingValue1.Name = "tb_SettingValue1";
            this.tb_SettingValue1.Size = new System.Drawing.Size(123, 27);
            this.tb_SettingValue1.TabIndex = 9;
            this.tb_SettingValue1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_SettingValue2
            // 
            this.tb_SettingValue2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_SettingValue2.Location = new System.Drawing.Point(362, 66);
            this.tb_SettingValue2.Name = "tb_SettingValue2";
            this.tb_SettingValue2.Size = new System.Drawing.Size(123, 27);
            this.tb_SettingValue2.TabIndex = 13;
            this.tb_SettingValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_HeadValue2
            // 
            this.tb_HeadValue2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_HeadValue2.Location = new System.Drawing.Point(362, 132);
            this.tb_HeadValue2.Name = "tb_HeadValue2";
            this.tb_HeadValue2.Size = new System.Drawing.Size(123, 27);
            this.tb_HeadValue2.TabIndex = 12;
            this.tb_HeadValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(253, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "AI Value(Org):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(253, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "Device Value:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(123, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 27);
            this.button1.TabIndex = 14;
            this.button1.Text = "Setting1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(373, 99);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 27);
            this.button2.TabIndex = 15;
            this.button2.Text = "Setting2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(269, 188);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 16);
            this.label9.TabIndex = 16;
            this.label9.Text = "Result :";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button3.Location = new System.Drawing.Point(331, 183);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(154, 27);
            this.button3.TabIndex = 17;
            this.button3.Text = "Calculate";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tb_Offset
            // 
            this.tb_Offset.BackColor = System.Drawing.SystemColors.Window;
            this.tb_Offset.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_Offset.Location = new System.Drawing.Point(59, 226);
            this.tb_Offset.Name = "tb_Offset";
            this.tb_Offset.ReadOnly = true;
            this.tb_Offset.Size = new System.Drawing.Size(176, 27);
            this.tb_Offset.TabIndex = 21;
            this.tb_Offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_Gain
            // 
            this.tb_Gain.BackColor = System.Drawing.SystemColors.Window;
            this.tb_Gain.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_Gain.Location = new System.Drawing.Point(59, 259);
            this.tb_Gain.Name = "tb_Gain";
            this.tb_Gain.ReadOnly = true;
            this.tb_Gain.Size = new System.Drawing.Size(176, 27);
            this.tb_Gain.TabIndex = 20;
            this.tb_Gain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(3, 226);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "Offset :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(3, 259);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 16);
            this.label11.TabIndex = 18;
            this.label11.Text = "Gain :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(253, 228);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 16);
            this.label12.TabIndex = 22;
            this.label12.Text = "Convert Value:";
            // 
            // tb_NowValue
            // 
            this.tb_NowValue.BackColor = System.Drawing.SystemColors.Control;
            this.tb_NowValue.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_NowValue.Location = new System.Drawing.Point(362, 226);
            this.tb_NowValue.Name = "tb_NowValue";
            this.tb_NowValue.Size = new System.Drawing.Size(123, 27);
            this.tb_NowValue.TabIndex = 24;
            this.tb_NowValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_HeadValue1
            // 
            this.tb_HeadValue1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_HeadValue1.Location = new System.Drawing.Point(112, 132);
            this.tb_HeadValue1.Name = "tb_HeadValue1";
            this.tb_HeadValue1.Size = new System.Drawing.Size(123, 27);
            this.tb_HeadValue1.TabIndex = 8;
            this.tb_HeadValue1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_Shift
            // 
            this.tb_Shift.BackColor = System.Drawing.SystemColors.Window;
            this.tb_Shift.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_Shift.Location = new System.Drawing.Point(59, 292);
            this.tb_Shift.Name = "tb_Shift";
            this.tb_Shift.ReadOnly = true;
            this.tb_Shift.Size = new System.Drawing.Size(176, 27);
            this.tb_Shift.TabIndex = 26;
            this.tb_Shift.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(3, 292);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 16);
            this.label13.TabIndex = 25;
            this.label13.Text = "Shift :";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button4.Location = new System.Drawing.Point(256, 259);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(229, 27);
            this.button4.TabIndex = 27;
            this.button4.Text = "Test Convert Output";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label14.Location = new System.Drawing.Point(3, 82);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 16);
            this.label14.TabIndex = 29;
            this.label14.Text = "(0 ~ 4095)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label15.Location = new System.Drawing.Point(253, 82);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 16);
            this.label15.TabIndex = 30;
            this.label15.Text = "(0 ~ 4095)";
            // 
            // btnSavePmt_AO
            // 
            this.btnSavePmt_AO.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSavePmt_AO.Location = new System.Drawing.Point(127, 339);
            this.btnSavePmt_AO.Name = "btnSavePmt_AO";
            this.btnSavePmt_AO.Size = new System.Drawing.Size(229, 73);
            this.btnSavePmt_AO.TabIndex = 31;
            this.btnSavePmt_AO.Text = "Save Pmt And Close";
            this.btnSavePmt_AO.UseVisualStyleBackColor = true;
            this.btnSavePmt_AO.Click += new System.EventHandler(this.button5_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvAIID,
            this.dgvAIDes,
            this.dgvAIValue});
            this.dataGridView1.Location = new System.Drawing.Point(491, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(285, 420);
            this.dataGridView1.TabIndex = 33;
            // 
            // dgvAIID
            // 
            this.dgvAIID.HeaderText = "AI";
            this.dgvAIID.Name = "dgvAIID";
            this.dgvAIID.ReadOnly = true;
            this.dgvAIID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvAIID.Width = 50;
            // 
            // dgvAIDes
            // 
            this.dgvAIDes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvAIDes.HeaderText = "Description";
            this.dgvAIDes.Name = "dgvAIDes";
            // 
            // dgvAIValue
            // 
            this.dgvAIValue.HeaderText = "Value";
            this.dgvAIValue.Name = "dgvAIValue";
            this.dgvAIValue.ReadOnly = true;
            this.dgvAIValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvAIValue.Width = 80;
            // 
            // ucAO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSavePmt_AO);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tb_Shift);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tb_NowValue);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tb_Offset);
            this.Controls.Add(this.tb_Gain);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_SettingValue2);
            this.Controls.Add(this.tb_HeadValue2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tb_SettingValue1);
            this.Controls.Add(this.tb_HeadValue1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_ChannelIndex);
            this.Controls.Add(this.label1);
            this.Name = "ucAO";
            this.Size = new System.Drawing.Size(779, 426);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_ChannelIndex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_SettingValue1;
        private System.Windows.Forms.TextBox tb_SettingValue2;
        private System.Windows.Forms.TextBox tb_HeadValue2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tb_Offset;
        private System.Windows.Forms.TextBox tb_Gain;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_NowValue;
        private System.Windows.Forms.TextBox tb_HeadValue1;
        private System.Windows.Forms.TextBox tb_Shift;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnSavePmt_AO;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAIID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAIDes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAIValue;
    }
}

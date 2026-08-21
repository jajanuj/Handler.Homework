namespace ArtSystem.MultiSystem
{
    partial class ucAI {
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_SampleValue1 = new System.Windows.Forms.TextBox();
            this.tb_SampleValue2 = new System.Windows.Forms.TextBox();
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
            this.tb_OriginValue = new System.Windows.Forms.TextBox();
            this.tb_NowValue = new System.Windows.Forms.TextBox();
            this.tb_HeadValue1 = new System.Windows.Forms.TextBox();
            this.tb_Shift = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnSavePmt_AO = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgvAOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAODes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAOValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(62, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "DI:";
            // 
            // tb_ChannelIndex
            // 
            this.tb_ChannelIndex.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_ChannelIndex.Location = new System.Drawing.Point(105, 10);
            this.tb_ChannelIndex.Name = "tb_ChannelIndex";
            this.tb_ChannelIndex.ReadOnly = true;
            this.tb_ChannelIndex.Size = new System.Drawing.Size(130, 27);
            this.tb_ChannelIndex.TabIndex = 1;
            this.tb_ChannelIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(288, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "AI Value(Org):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(3, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Data1：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(292, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Data2：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(3, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Device Value:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(3, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Sample Value:";
            // 
            // tb_SampleValue1
            // 
            this.tb_SampleValue1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_SampleValue1.Location = new System.Drawing.Point(108, 122);
            this.tb_SampleValue1.Name = "tb_SampleValue1";
            this.tb_SampleValue1.Size = new System.Drawing.Size(127, 27);
            this.tb_SampleValue1.TabIndex = 9;
            this.tb_SampleValue1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_SampleValue2
            // 
            this.tb_SampleValue2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_SampleValue2.Location = new System.Drawing.Point(397, 122);
            this.tb_SampleValue2.Name = "tb_SampleValue2";
            this.tb_SampleValue2.Size = new System.Drawing.Size(127, 27);
            this.tb_SampleValue2.TabIndex = 13;
            this.tb_SampleValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_HeadValue2
            // 
            this.tb_HeadValue2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_HeadValue2.Location = new System.Drawing.Point(397, 79);
            this.tb_HeadValue2.Name = "tb_HeadValue2";
            this.tb_HeadValue2.Size = new System.Drawing.Size(127, 27);
            this.tb_HeadValue2.TabIndex = 12;
            this.tb_HeadValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(292, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Sample Value:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(292, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "Device Value:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(123, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 27);
            this.button1.TabIndex = 14;
            this.button1.Text = "Sample1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(412, 150);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 27);
            this.button2.TabIndex = 15;
            this.button2.Text = "Sample2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(297, 212);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 16);
            this.label9.TabIndex = 16;
            this.label9.Text = "Result :";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button3.Location = new System.Drawing.Point(295, 231);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(154, 27);
            this.button3.TabIndex = 17;
            this.button3.Text = "Calculate";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tb_Offset
            // 
            this.tb_Offset.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_Offset.Location = new System.Drawing.Point(65, 209);
            this.tb_Offset.Name = "tb_Offset";
            this.tb_Offset.Size = new System.Drawing.Size(176, 27);
            this.tb_Offset.TabIndex = 21;
            this.tb_Offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_Gain
            // 
            this.tb_Gain.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_Gain.Location = new System.Drawing.Point(65, 242);
            this.tb_Gain.Name = "tb_Gain";
            this.tb_Gain.Size = new System.Drawing.Size(176, 27);
            this.tb_Gain.TabIndex = 20;
            this.tb_Gain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(9, 209);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "Offset :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(9, 242);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 16);
            this.label11.TabIndex = 18;
            this.label11.Text = "Gain :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(292, 277);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 16);
            this.label12.TabIndex = 22;
            this.label12.Text = "Convert Value:";
            // 
            // tb_OriginValue
            // 
            this.tb_OriginValue.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_OriginValue.Location = new System.Drawing.Point(397, 10);
            this.tb_OriginValue.Name = "tb_OriginValue";
            this.tb_OriginValue.ReadOnly = true;
            this.tb_OriginValue.Size = new System.Drawing.Size(127, 27);
            this.tb_OriginValue.TabIndex = 3;
            this.tb_OriginValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_NowValue
            // 
            this.tb_NowValue.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_NowValue.Location = new System.Drawing.Point(394, 275);
            this.tb_NowValue.Name = "tb_NowValue";
            this.tb_NowValue.ReadOnly = true;
            this.tb_NowValue.Size = new System.Drawing.Size(130, 27);
            this.tb_NowValue.TabIndex = 24;
            this.tb_NowValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_HeadValue1
            // 
            this.tb_HeadValue1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_HeadValue1.Location = new System.Drawing.Point(108, 79);
            this.tb_HeadValue1.Name = "tb_HeadValue1";
            this.tb_HeadValue1.Size = new System.Drawing.Size(127, 27);
            this.tb_HeadValue1.TabIndex = 8;
            this.tb_HeadValue1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_Shift
            // 
            this.tb_Shift.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_Shift.Location = new System.Drawing.Point(65, 275);
            this.tb_Shift.Name = "tb_Shift";
            this.tb_Shift.Size = new System.Drawing.Size(176, 27);
            this.tb_Shift.TabIndex = 26;
            this.tb_Shift.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(9, 275);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 16);
            this.label13.TabIndex = 25;
            this.label13.Text = "Shift :";
            // 
            // btnSavePmt_AO
            // 
            this.btnSavePmt_AO.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSavePmt_AO.Location = new System.Drawing.Point(166, 328);
            this.btnSavePmt_AO.Name = "btnSavePmt_AO";
            this.btnSavePmt_AO.Size = new System.Drawing.Size(229, 73);
            this.btnSavePmt_AO.TabIndex = 32;
            this.btnSavePmt_AO.Text = "Save Pmt And Close";
            this.btnSavePmt_AO.UseVisualStyleBackColor = true;
            this.btnSavePmt_AO.Click += new System.EventHandler(this.btnSavePmt_AO_Click);
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
            this.dgvAOID,
            this.dgvAODes,
            this.dgvAOValue});
            this.dataGridView1.Location = new System.Drawing.Point(530, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(285, 410);
            this.dataGridView1.TabIndex = 34;
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // dgvAOID
            // 
            this.dgvAOID.HeaderText = "AO";
            this.dgvAOID.Name = "dgvAOID";
            this.dgvAOID.ReadOnly = true;
            this.dgvAOID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvAOID.Width = 50;
            // 
            // dgvAODes
            // 
            this.dgvAODes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvAODes.HeaderText = "Description";
            this.dgvAODes.Name = "dgvAODes";
            this.dgvAODes.ReadOnly = true;
            this.dgvAODes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvAOValue
            // 
            this.dgvAOValue.HeaderText = "Value";
            this.dgvAOValue.Name = "dgvAOValue";
            this.dgvAOValue.ReadOnly = true;
            this.dgvAOValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvAOValue.Width = 80;
            // 
            // ucAI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSavePmt_AO);
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
            this.Controls.Add(this.tb_SampleValue2);
            this.Controls.Add(this.tb_HeadValue2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tb_SampleValue1);
            this.Controls.Add(this.tb_HeadValue1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_OriginValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_ChannelIndex);
            this.Controls.Add(this.label1);
            this.Name = "ucAI";
            this.Size = new System.Drawing.Size(817, 416);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_ChannelIndex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_SampleValue1;
        private System.Windows.Forms.TextBox tb_SampleValue2;
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
        private System.Windows.Forms.TextBox tb_OriginValue;
        private System.Windows.Forms.TextBox tb_NowValue;
        private System.Windows.Forms.TextBox tb_HeadValue1;
        private System.Windows.Forms.TextBox tb_Shift;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnSavePmt_AO;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAODes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAOValue;
    }
}

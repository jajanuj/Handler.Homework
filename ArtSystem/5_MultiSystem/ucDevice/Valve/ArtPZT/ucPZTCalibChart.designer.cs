
namespace ArtSystem.MultiSystem
{
    partial class ucPZTCalibChart
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

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvVoltage = new System.Windows.Forms.DataGridView();
            this.lblLeftSlopText = new System.Windows.Forms.Label();
            this.lblRightSlopText = new System.Windows.Forms.Label();
            this.txtLeftSlopRate = new System.Windows.Forms.TextBox();
            this.txtRightSlopRate = new System.Windows.Forms.TextBox();
            this.lblGetValue = new System.Windows.Forms.Label();
            this.btnCalSlopRate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblResultText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSlopDeviation = new System.Windows.Forms.TextBox();
            this.chkRedLineSkip = new System.Windows.Forms.CheckBox();
            this.btn_Calculate = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btn_ReadFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button_X = new System.Windows.Forms.Button();
            this.dgvVoltage2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoltage)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoltage2)).BeginInit();
            this.SuspendLayout();
            // 
            // Chart1
            // 
            this.Chart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            chartArea1.AxisX.Interval = 300D;
            chartArea1.AxisX.Maximum = 4095D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.Title = "壓電電壓(V)";
            chartArea1.AxisY.Interval = 0.001D;
            chartArea1.AxisY.Maximum = 0.01D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.Title = "斜率值";
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            chartArea1.Name = "ChartArea1";
            this.Chart1.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.Chart1.Legends.Add(legend1);
            this.Chart1.Location = new System.Drawing.Point(240, 58);
            this.Chart1.Name = "Chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            this.Chart1.Series.Add(series1);
            this.Chart1.Series.Add(series2);
            this.Chart1.Size = new System.Drawing.Size(768, 310);
            this.Chart1.TabIndex = 77;
            this.Chart1.Text = "chart2";
            this.Chart1.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.Chart1_GetToolTipText);
            // 
            // dgvVoltage
            // 
            this.dgvVoltage.AllowUserToAddRows = false;
            this.dgvVoltage.AllowUserToDeleteRows = false;
            this.dgvVoltage.AllowUserToResizeColumns = false;
            this.dgvVoltage.AllowUserToResizeRows = false;
            this.dgvVoltage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoltage.Location = new System.Drawing.Point(12, 58);
            this.dgvVoltage.Name = "dgvVoltage";
            this.dgvVoltage.RowHeadersVisible = false;
            this.dgvVoltage.RowTemplate.Height = 24;
            this.dgvVoltage.Size = new System.Drawing.Size(222, 147);
            this.dgvVoltage.TabIndex = 80;
            // 
            // lblLeftSlopText
            // 
            this.lblLeftSlopText.AutoSize = true;
            this.lblLeftSlopText.Location = new System.Drawing.Point(13, 13);
            this.lblLeftSlopText.Name = "lblLeftSlopText";
            this.lblLeftSlopText.Size = new System.Drawing.Size(44, 12);
            this.lblLeftSlopText.TabIndex = 81;
            this.lblLeftSlopText.Text = "左斜率:";
            // 
            // lblRightSlopText
            // 
            this.lblRightSlopText.AutoSize = true;
            this.lblRightSlopText.Location = new System.Drawing.Point(188, 13);
            this.lblRightSlopText.Name = "lblRightSlopText";
            this.lblRightSlopText.Size = new System.Drawing.Size(44, 12);
            this.lblRightSlopText.TabIndex = 81;
            this.lblRightSlopText.Text = "右斜率:";
            // 
            // txtLeftSlopRate
            // 
            this.txtLeftSlopRate.Location = new System.Drawing.Point(63, 8);
            this.txtLeftSlopRate.Name = "txtLeftSlopRate";
            this.txtLeftSlopRate.Size = new System.Drawing.Size(100, 22);
            this.txtLeftSlopRate.TabIndex = 82;
            // 
            // txtRightSlopRate
            // 
            this.txtRightSlopRate.Location = new System.Drawing.Point(237, 8);
            this.txtRightSlopRate.Name = "txtRightSlopRate";
            this.txtRightSlopRate.Size = new System.Drawing.Size(100, 22);
            this.txtRightSlopRate.TabIndex = 82;
            // 
            // lblGetValue
            // 
            this.lblGetValue.AutoSize = true;
            this.lblGetValue.Location = new System.Drawing.Point(249, 340);
            this.lblGetValue.Name = "lblGetValue";
            this.lblGetValue.Size = new System.Drawing.Size(28, 12);
            this.lblGetValue.TabIndex = 81;
            this.lblGetValue.Text = "(x,y)";
            // 
            // btnCalSlopRate
            // 
            this.btnCalSlopRate.Location = new System.Drawing.Point(483, 6);
            this.btnCalSlopRate.Name = "btnCalSlopRate";
            this.btnCalSlopRate.Size = new System.Drawing.Size(122, 27);
            this.btnCalSlopRate.TabIndex = 79;
            this.btnCalSlopRate.Text = "計算斜率對稱率";
            this.btnCalSlopRate.UseVisualStyleBackColor = true;
            this.btnCalSlopRate.Click += new System.EventHandler(this.btnCalSlopRate_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Controls.Add(this.lblResult);
            this.panel1.Controls.Add(this.lblResultText);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblLeftSlopText);
            this.panel1.Controls.Add(this.txtSlopDeviation);
            this.panel1.Controls.Add(this.txtRightSlopRate);
            this.panel1.Controls.Add(this.btnCalSlopRate);
            this.panel1.Controls.Add(this.txtLeftSlopRate);
            this.panel1.Controls.Add(this.lblRightSlopText);
            this.panel1.Location = new System.Drawing.Point(198, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(745, 40);
            this.panel1.TabIndex = 83;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(681, 14);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(31, 12);
            this.lblResult.TabIndex = 87;
            this.lblResult.Text = "PASS";
            // 
            // lblResultText
            // 
            this.lblResultText.AutoSize = true;
            this.lblResultText.Location = new System.Drawing.Point(622, 14);
            this.lblResultText.Name = "lblResultText";
            this.lblResultText.Size = new System.Drawing.Size(59, 12);
            this.lblResultText.TabIndex = 86;
            this.lblResultText.Text = "判定結果: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(344, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 12);
            this.label2.TabIndex = 81;
            this.label2.Text = "- 1 =";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(8, 12);
            this.label1.TabIndex = 81;
            this.label1.Text = "/";
            // 
            // txtSlopDeviation
            // 
            this.txtSlopDeviation.Location = new System.Drawing.Point(377, 8);
            this.txtSlopDeviation.Name = "txtSlopDeviation";
            this.txtSlopDeviation.Size = new System.Drawing.Size(100, 22);
            this.txtSlopDeviation.TabIndex = 82;
            // 
            // chkRedLineSkip
            // 
            this.chkRedLineSkip.AutoSize = true;
            this.chkRedLineSkip.Location = new System.Drawing.Point(933, 32);
            this.chkRedLineSkip.Name = "chkRedLineSkip";
            this.chkRedLineSkip.Size = new System.Drawing.Size(87, 16);
            this.chkRedLineSkip.TabIndex = 84;
            this.chkRedLineSkip.Text = "Skip Red line";
            this.chkRedLineSkip.UseVisualStyleBackColor = true;
            this.chkRedLineSkip.Visible = false;
            this.chkRedLineSkip.CheckedChanged += new System.EventHandler(this.chkRedLineSkip_CheckedChanged);
            // 
            // btn_Calculate
            // 
            this.btn_Calculate.Location = new System.Drawing.Point(71, 12);
            this.btn_Calculate.Name = "btn_Calculate";
            this.btn_Calculate.Size = new System.Drawing.Size(40, 27);
            this.btn_Calculate.TabIndex = 79;
            this.btn_Calculate.Text = "計算";
            this.btn_Calculate.UseVisualStyleBackColor = true;
            this.btn_Calculate.Click += new System.EventHandler(this.btn_Calculate_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(135, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(54, 27);
            this.btnSearch.TabIndex = 79;
            this.btnSearch.Text = "劃趨式";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btn_ReadFile
            // 
            this.btn_ReadFile.Location = new System.Drawing.Point(9, 12);
            this.btn_ReadFile.Name = "btn_ReadFile";
            this.btn_ReadFile.Size = new System.Drawing.Size(39, 27);
            this.btn_ReadFile.TabIndex = 78;
            this.btn_ReadFile.Text = "讀檔";
            this.btn_ReadFile.UseVisualStyleBackColor = true;
            this.btn_ReadFile.Click += new System.EventHandler(this.btn_ReadFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 85;
            this.label3.Text = "=>>";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 85;
            this.label4.Text = "=>>";
            this.label4.Visible = false;
            // 
            // button_X
            // 
            this.button_X.BackColor = System.Drawing.Color.Tomato;
            this.button_X.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button_X.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed;
            this.button_X.FlatAppearance.BorderSize = 0;
            this.button_X.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_X.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_X.ForeColor = System.Drawing.Color.Transparent;
            this.button_X.Location = new System.Drawing.Point(990, 2);
            this.button_X.Name = "button_X";
            this.button_X.Size = new System.Drawing.Size(27, 25);
            this.button_X.TabIndex = 86;
            this.button_X.Text = "X";
            this.button_X.UseVisualStyleBackColor = false;
            this.button_X.Click += new System.EventHandler(this.button_X_Click);
            // 
            // dgvVoltage2
            // 
            this.dgvVoltage2.AllowUserToAddRows = false;
            this.dgvVoltage2.AllowUserToDeleteRows = false;
            this.dgvVoltage2.AllowUserToResizeColumns = false;
            this.dgvVoltage2.AllowUserToResizeRows = false;
            this.dgvVoltage2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoltage2.Location = new System.Drawing.Point(12, 211);
            this.dgvVoltage2.Name = "dgvVoltage2";
            this.dgvVoltage2.RowHeadersVisible = false;
            this.dgvVoltage2.RowTemplate.Height = 24;
            this.dgvVoltage2.Size = new System.Drawing.Size(222, 147);
            this.dgvVoltage2.TabIndex = 87;
            // 
            // ucPZTCalibChart
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.dgvVoltage2);
            this.Controls.Add(this.button_X);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkRedLineSkip);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblGetValue);
            this.Controls.Add(this.dgvVoltage);
            this.Controls.Add(this.btn_ReadFile);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btn_Calculate);
            this.Controls.Add(this.Chart1);
            this.Name = "ucPZTCalibChart";
            this.Size = new System.Drawing.Size(1025, 374);
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoltage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoltage2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart Chart1;
        private System.Windows.Forms.DataGridView dgvVoltage;
        private System.Windows.Forms.Label lblLeftSlopText;
        private System.Windows.Forms.Label lblRightSlopText;
        private System.Windows.Forms.TextBox txtLeftSlopRate;
        private System.Windows.Forms.TextBox txtRightSlopRate;
        private System.Windows.Forms.Label lblGetValue;
        private System.Windows.Forms.Button btnCalSlopRate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSlopDeviation;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblResultText;
        private System.Windows.Forms.CheckBox chkRedLineSkip;
        private System.Windows.Forms.Button btn_Calculate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btn_ReadFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_X;
        private System.Windows.Forms.DataGridView dgvVoltage2;
    }
}


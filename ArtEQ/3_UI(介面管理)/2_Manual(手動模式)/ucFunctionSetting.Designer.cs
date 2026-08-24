namespace ArtEQ
{
    partial class ucFunctionSetting
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tPage_System = new System.Windows.Forms.TabPage();
            this.lblEnablePressStation = new System.Windows.Forms.Label();
            this.comImgButton1 = new ArtControlLib.comImgButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comImgButton6 = new ArtControlLib.comImgButton();
            this.comImgButton5 = new ArtControlLib.comImgButton();
            this.label42 = new System.Windows.Forms.Label();
            this.cbtn_CheckCorner = new ArtControlLib.comImgButton();
            this.tPage_Recipe = new System.Windows.Forms.TabPage();
            this.lblNgDischargeMode = new System.Windows.Forms.Label();
            this.comNumBox1 = new ArtControlLib.comNumBox();
            this.cboNgDischargeMode = new System.Windows.Forms.ComboBox();
            this.cnbTrayRowNo = new ArtControlLib.comNumBox();
            this.lblTrayRowNo = new System.Windows.Forms.Label();
            this.cnbTrayColumnNo = new ArtControlLib.comNumBox();
            this.lblTrayColumnNo = new System.Windows.Forms.Label();
            this.cnbMagazineSlotNo = new ArtControlLib.comNumBox();
            this.lblMagazineSlotNo = new System.Windows.Forms.Label();
            this.cnbCellHeight = new ArtControlLib.comNumBox();
            this.lblCellHeight = new System.Windows.Forms.Label();
            this.cnbCellWidthX = new ArtControlLib.comNumBox();
            this.lblCellWidth = new System.Windows.Forms.Label();
            this.cnbCellPitchY = new ArtControlLib.comNumBox();
            this.lblCellPitchY = new System.Windows.Forms.Label();
            this.cnbCellPitchX = new ArtControlLib.comNumBox();
            this.lblCellPitchX = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tPage_System.SuspendLayout();
            this.tPage_Recipe.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tPage_System);
            this.tabControl1.Controls.Add(this.tPage_Recipe);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(888, 553);
            this.tabControl1.TabIndex = 0;
            // 
            // tPage_System
            // 
            this.tPage_System.AllowDrop = true;
            this.tPage_System.Controls.Add(this.lblEnablePressStation);
            this.tPage_System.Controls.Add(this.comImgButton1);
            this.tPage_System.Controls.Add(this.label6);
            this.tPage_System.Controls.Add(this.label5);
            this.tPage_System.Controls.Add(this.comImgButton6);
            this.tPage_System.Controls.Add(this.comImgButton5);
            this.tPage_System.Controls.Add(this.label42);
            this.tPage_System.Controls.Add(this.cbtn_CheckCorner);
            this.tPage_System.Location = new System.Drawing.Point(4, 25);
            this.tPage_System.Name = "tPage_System";
            this.tPage_System.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_System.Size = new System.Drawing.Size(880, 524);
            this.tPage_System.TabIndex = 0;
            this.tPage_System.Text = "System Setting";
            this.tPage_System.UseVisualStyleBackColor = true;
            // 
            // lblEnablePressStation
            // 
            this.lblEnablePressStation.AutoSize = true;
            this.lblEnablePressStation.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnablePressStation.Location = new System.Drawing.Point(408, 48);
            this.lblEnablePressStation.Name = "lblEnablePressStation";
            this.lblEnablePressStation.Size = new System.Drawing.Size(178, 18);
            this.lblEnablePressStation.TabIndex = 456;
            this.lblEnablePressStation.Text = "Enable Press Station";
            // 
            // comImgButton1
            // 
            this.comImgButton1._DefaultStatus = false;
            this.comImgButton1._ImgBackground = true;
            this.comImgButton1._ImgFalse = global::ArtEQ.Properties.Resources.No;
            this.comImgButton1._ImgTrue = global::ArtEQ.Properties.Resources.Yes;
            this.comImgButton1._IsSaveToIni = true;
            this.comImgButton1._IsSaveToLog = true;
            this.comImgButton1._PmtName = ArtData.clsEnum.enuPmtName.Sys_EnablePressStation;
            this.comImgButton1._PmtType = ArtData.clsEnum.enuPmtType.System;
            this.comImgButton1._Status = false;
            this.comImgButton1.BackgroundImage = global::ArtEQ.Properties.Resources.No;
            this.comImgButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.comImgButton1.Location = new System.Drawing.Point(357, 36);
            this.comImgButton1.Name = "comImgButton1";
            this.comImgButton1.Size = new System.Drawing.Size(45, 45);
            this.comImgButton1.TabIndex = 455;
            this.comImgButton1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(87, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 18);
            this.label6.TabIndex = 454;
            this.label6.Text = "Enable Safe Door";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(87, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 18);
            this.label5.TabIndex = 454;
            this.label5.Text = "Enable Teach Mode";
            // 
            // comImgButton6
            // 
            this.comImgButton6._DefaultStatus = false;
            this.comImgButton6._ImgBackground = true;
            this.comImgButton6._ImgFalse = global::ArtEQ.Properties.Resources.No;
            this.comImgButton6._ImgTrue = global::ArtEQ.Properties.Resources.Yes;
            this.comImgButton6._IsSaveToIni = true;
            this.comImgButton6._IsSaveToLog = true;
            this.comImgButton6._PmtName = ArtData.clsEnum.enuPmtName.Sys_EnableSafeDoor;
            this.comImgButton6._PmtType = ArtData.clsEnum.enuPmtType.System;
            this.comImgButton6._Status = false;
            this.comImgButton6.BackgroundImage = global::ArtEQ.Properties.Resources.No;
            this.comImgButton6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.comImgButton6.Location = new System.Drawing.Point(36, 36);
            this.comImgButton6.Name = "comImgButton6";
            this.comImgButton6.Size = new System.Drawing.Size(45, 45);
            this.comImgButton6.TabIndex = 453;
            this.comImgButton6.UseVisualStyleBackColor = true;
            // 
            // comImgButton5
            // 
            this.comImgButton5._DefaultStatus = false;
            this.comImgButton5._ImgBackground = true;
            this.comImgButton5._ImgFalse = global::ArtEQ.Properties.Resources.No;
            this.comImgButton5._ImgTrue = global::ArtEQ.Properties.Resources.Yes;
            this.comImgButton5._IsSaveToIni = true;
            this.comImgButton5._IsSaveToLog = true;
            this.comImgButton5._PmtName = ArtData.clsEnum.enuPmtName.Sys_TeachEnable;
            this.comImgButton5._PmtType = ArtData.clsEnum.enuPmtType.System;
            this.comImgButton5._Status = false;
            this.comImgButton5.BackgroundImage = global::ArtEQ.Properties.Resources.No;
            this.comImgButton5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.comImgButton5.Location = new System.Drawing.Point(36, 138);
            this.comImgButton5.Name = "comImgButton5";
            this.comImgButton5.Size = new System.Drawing.Size(45, 45);
            this.comImgButton5.TabIndex = 453;
            this.comImgButton5.UseVisualStyleBackColor = true;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(87, 99);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(150, 18);
            this.label42.TabIndex = 446;
            this.label42.Text = "Simulate Dry Run";
            // 
            // cbtn_CheckCorner
            // 
            this.cbtn_CheckCorner._DefaultStatus = false;
            this.cbtn_CheckCorner._ImgBackground = true;
            this.cbtn_CheckCorner._ImgFalse = global::ArtEQ.Properties.Resources.No;
            this.cbtn_CheckCorner._ImgTrue = global::ArtEQ.Properties.Resources.Yes;
            this.cbtn_CheckCorner._IsSaveToIni = true;
            this.cbtn_CheckCorner._IsSaveToLog = true;
            this.cbtn_CheckCorner._PmtName = ArtData.clsEnum.enuPmtName.Rec_NeedEmptyMagazine;
            this.cbtn_CheckCorner._PmtType = ArtData.clsEnum.enuPmtType.System;
            this.cbtn_CheckCorner._Status = false;
            this.cbtn_CheckCorner.BackgroundImage = global::ArtEQ.Properties.Resources.No;
            this.cbtn_CheckCorner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.cbtn_CheckCorner.Location = new System.Drawing.Point(36, 87);
            this.cbtn_CheckCorner.Name = "cbtn_CheckCorner";
            this.cbtn_CheckCorner.Size = new System.Drawing.Size(45, 45);
            this.cbtn_CheckCorner.TabIndex = 445;
            this.cbtn_CheckCorner.UseVisualStyleBackColor = true;
            // 
            // tPage_Recipe
            // 
            this.tPage_Recipe.Controls.Add(this.lblNgDischargeMode);
            this.tPage_Recipe.Controls.Add(this.comNumBox1);
            this.tPage_Recipe.Controls.Add(this.cboNgDischargeMode);
            this.tPage_Recipe.Controls.Add(this.cnbTrayRowNo);
            this.tPage_Recipe.Controls.Add(this.lblTrayRowNo);
            this.tPage_Recipe.Controls.Add(this.cnbTrayColumnNo);
            this.tPage_Recipe.Controls.Add(this.lblTrayColumnNo);
            this.tPage_Recipe.Controls.Add(this.cnbMagazineSlotNo);
            this.tPage_Recipe.Controls.Add(this.lblMagazineSlotNo);
            this.tPage_Recipe.Controls.Add(this.cnbCellHeight);
            this.tPage_Recipe.Controls.Add(this.lblCellHeight);
            this.tPage_Recipe.Controls.Add(this.cnbCellWidthX);
            this.tPage_Recipe.Controls.Add(this.lblCellWidth);
            this.tPage_Recipe.Controls.Add(this.cnbCellPitchY);
            this.tPage_Recipe.Controls.Add(this.lblCellPitchY);
            this.tPage_Recipe.Controls.Add(this.cnbCellPitchX);
            this.tPage_Recipe.Controls.Add(this.lblCellPitchX);
            this.tPage_Recipe.Location = new System.Drawing.Point(4, 25);
            this.tPage_Recipe.Name = "tPage_Recipe";
            this.tPage_Recipe.Padding = new System.Windows.Forms.Padding(3);
            this.tPage_Recipe.Size = new System.Drawing.Size(880, 524);
            this.tPage_Recipe.TabIndex = 1;
            this.tPage_Recipe.Text = "Recipe Setting";
            this.tPage_Recipe.UseVisualStyleBackColor = true;
            // 
            // lblNgDischargeMode
            // 
            this.lblNgDischargeMode.AutoSize = true;
            this.lblNgDischargeMode.Location = new System.Drawing.Point(27, 314);
            this.lblNgDischargeMode.Name = "lblNgDischargeMode";
            this.lblNgDischargeMode.Size = new System.Drawing.Size(134, 16);
            this.lblNgDischargeMode.TabIndex = 490;
            this.lblNgDischargeMode.Text = "NG Discharge Mode";
            // 
            // comNumBox1
            // 
            this.comNumBox1._DecimalPlaces = 0;
            this.comNumBox1._DefaultValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.comNumBox1._IsSaveToIni = true;
            this.comNumBox1._IsSaveToLog = true;
            this.comNumBox1._IsShowCurrentValue = false;
            this.comNumBox1._IsShowPopForm = true;
            this.comNumBox1._Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.comNumBox1._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.comNumBox1._PmtName = ArtData.clsEnum.enuPmtName.Rec_Sort_Type;
            this.comNumBox1._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.comNumBox1._TempValue = null;
            this.comNumBox1._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.comNumBox1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comNumBox1.Location = new System.Drawing.Point(186, 352);
            this.comNumBox1.Name = "comNumBox1";
            this.comNumBox1.ReadOnly = true;
            this.comNumBox1.Size = new System.Drawing.Size(67, 26);
            this.comNumBox1.TabIndex = 489;
            this.comNumBox1.Text = "0";
            this.comNumBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.comNumBox1.Visible = false;
            this.comNumBox1.TextChanged += new System.EventHandler(this.comNumBox1_TextChanged);
            // 
            // cboNgDischargeMode
            // 
            this.cboNgDischargeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNgDischargeMode.FormattingEnabled = true;
            this.cboNgDischargeMode.Items.AddRange(new object[] {
            "PerCycle",
            "Full Tray"});
            this.cboNgDischargeMode.Location = new System.Drawing.Point(186, 311);
            this.cboNgDischargeMode.Name = "cboNgDischargeMode";
            this.cboNgDischargeMode.Size = new System.Drawing.Size(86, 24);
            this.cboNgDischargeMode.TabIndex = 488;
            this.cboNgDischargeMode.DropDownClosed += new System.EventHandler(this.cboSortType_DropDownClosed);
            // 
            // cnbTrayRowNo
            // 
            this.cnbTrayRowNo._DecimalPlaces = 0;
            this.cnbTrayRowNo._DefaultValue = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.cnbTrayRowNo._IsSaveToIni = true;
            this.cnbTrayRowNo._IsSaveToLog = true;
            this.cnbTrayRowNo._IsShowCurrentValue = false;
            this.cnbTrayRowNo._IsShowPopForm = true;
            this.cnbTrayRowNo._Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cnbTrayRowNo._Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cnbTrayRowNo._PmtName = ArtData.clsEnum.enuPmtName.Rec_Tray_Row_Number;
            this.cnbTrayRowNo._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.cnbTrayRowNo._TempValue = null;
            this.cnbTrayRowNo._Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cnbTrayRowNo.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cnbTrayRowNo.Location = new System.Drawing.Point(186, 106);
            this.cnbTrayRowNo.Name = "cnbTrayRowNo";
            this.cnbTrayRowNo.ReadOnly = true;
            this.cnbTrayRowNo.Size = new System.Drawing.Size(67, 26);
            this.cnbTrayRowNo.TabIndex = 487;
            this.cnbTrayRowNo.Text = "1";
            this.cnbTrayRowNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTrayRowNo
            // 
            this.lblTrayRowNo.AutoSize = true;
            this.lblTrayRowNo.Location = new System.Drawing.Point(27, 111);
            this.lblTrayRowNo.Name = "lblTrayRowNo";
            this.lblTrayRowNo.Size = new System.Drawing.Size(122, 16);
            this.lblTrayRowNo.TabIndex = 486;
            this.lblTrayRowNo.Text = "Tray Row Number";
            // 
            // cnbTrayColumnNo
            // 
            this.cnbTrayColumnNo._DecimalPlaces = 0;
            this.cnbTrayColumnNo._DefaultValue = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.cnbTrayColumnNo._IsSaveToIni = true;
            this.cnbTrayColumnNo._IsSaveToLog = true;
            this.cnbTrayColumnNo._IsShowCurrentValue = false;
            this.cnbTrayColumnNo._IsShowPopForm = true;
            this.cnbTrayColumnNo._Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cnbTrayColumnNo._Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cnbTrayColumnNo._PmtName = ArtData.clsEnum.enuPmtName.Rec_Tray_Column_Number;
            this.cnbTrayColumnNo._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.cnbTrayColumnNo._TempValue = null;
            this.cnbTrayColumnNo._Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cnbTrayColumnNo.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cnbTrayColumnNo.Location = new System.Drawing.Point(186, 73);
            this.cnbTrayColumnNo.Name = "cnbTrayColumnNo";
            this.cnbTrayColumnNo.ReadOnly = true;
            this.cnbTrayColumnNo.Size = new System.Drawing.Size(67, 26);
            this.cnbTrayColumnNo.TabIndex = 485;
            this.cnbTrayColumnNo.Text = "1";
            this.cnbTrayColumnNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTrayColumnNo
            // 
            this.lblTrayColumnNo.AutoSize = true;
            this.lblTrayColumnNo.Location = new System.Drawing.Point(27, 78);
            this.lblTrayColumnNo.Name = "lblTrayColumnNo";
            this.lblTrayColumnNo.Size = new System.Drawing.Size(142, 16);
            this.lblTrayColumnNo.TabIndex = 484;
            this.lblTrayColumnNo.Text = "Tray Column Number";
            // 
            // cnbMagazineSlotNo
            // 
            this.cnbMagazineSlotNo._DecimalPlaces = 0;
            this.cnbMagazineSlotNo._DefaultValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.cnbMagazineSlotNo._IsSaveToIni = true;
            this.cnbMagazineSlotNo._IsSaveToLog = true;
            this.cnbMagazineSlotNo._IsShowCurrentValue = false;
            this.cnbMagazineSlotNo._IsShowPopForm = true;
            this.cnbMagazineSlotNo._Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cnbMagazineSlotNo._Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.cnbMagazineSlotNo._PmtName = ArtData.clsEnum.enuPmtName.Rec_Magazine_Slot_Number;
            this.cnbMagazineSlotNo._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.cnbMagazineSlotNo._TempValue = null;
            this.cnbMagazineSlotNo._Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.cnbMagazineSlotNo.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cnbMagazineSlotNo.Location = new System.Drawing.Point(186, 35);
            this.cnbMagazineSlotNo.Name = "cnbMagazineSlotNo";
            this.cnbMagazineSlotNo.ReadOnly = true;
            this.cnbMagazineSlotNo.Size = new System.Drawing.Size(67, 26);
            this.cnbMagazineSlotNo.TabIndex = 483;
            this.cnbMagazineSlotNo.Text = "5";
            this.cnbMagazineSlotNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblMagazineSlotNo
            // 
            this.lblMagazineSlotNo.AutoSize = true;
            this.lblMagazineSlotNo.Location = new System.Drawing.Point(27, 40);
            this.lblMagazineSlotNo.Name = "lblMagazineSlotNo";
            this.lblMagazineSlotNo.Size = new System.Drawing.Size(153, 16);
            this.lblMagazineSlotNo.TabIndex = 482;
            this.lblMagazineSlotNo.Text = "Magazine Slot Number";
            // 
            // cnbCellHeight
            // 
            this.cnbCellHeight._DecimalPlaces = 0;
            this.cnbCellHeight._DefaultValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cnbCellHeight._IsSaveToIni = true;
            this.cnbCellHeight._IsSaveToLog = true;
            this.cnbCellHeight._IsShowCurrentValue = false;
            this.cnbCellHeight._IsShowPopForm = true;
            this.cnbCellHeight._Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.cnbCellHeight._Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cnbCellHeight._PmtName = ArtData.clsEnum.enuPmtName.Rec_Cell_Height;
            this.cnbCellHeight._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.cnbCellHeight._TempValue = null;
            this.cnbCellHeight._Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cnbCellHeight.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cnbCellHeight.Location = new System.Drawing.Point(186, 175);
            this.cnbCellHeight.Name = "cnbCellHeight";
            this.cnbCellHeight.ReadOnly = true;
            this.cnbCellHeight.Size = new System.Drawing.Size(67, 26);
            this.cnbCellHeight.TabIndex = 481;
            this.cnbCellHeight.Text = "10";
            this.cnbCellHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCellHeight
            // 
            this.lblCellHeight.AutoSize = true;
            this.lblCellHeight.Location = new System.Drawing.Point(27, 180);
            this.lblCellHeight.Name = "lblCellHeight";
            this.lblCellHeight.Size = new System.Drawing.Size(43, 16);
            this.lblCellHeight.TabIndex = 480;
            this.lblCellHeight.Text = "Heigh";
            // 
            // cnbCellWidthX
            // 
            this.cnbCellWidthX._DecimalPlaces = 0;
            this.cnbCellWidthX._DefaultValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cnbCellWidthX._IsSaveToIni = true;
            this.cnbCellWidthX._IsSaveToLog = true;
            this.cnbCellWidthX._IsShowCurrentValue = false;
            this.cnbCellWidthX._IsShowPopForm = true;
            this.cnbCellWidthX._Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.cnbCellWidthX._Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cnbCellWidthX._PmtName = ArtData.clsEnum.enuPmtName.Rec_Cell_Width;
            this.cnbCellWidthX._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.cnbCellWidthX._TempValue = null;
            this.cnbCellWidthX._Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cnbCellWidthX.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cnbCellWidthX.Location = new System.Drawing.Point(186, 142);
            this.cnbCellWidthX.Name = "cnbCellWidthX";
            this.cnbCellWidthX.ReadOnly = true;
            this.cnbCellWidthX.Size = new System.Drawing.Size(67, 26);
            this.cnbCellWidthX.TabIndex = 479;
            this.cnbCellWidthX.Text = "10";
            this.cnbCellWidthX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCellWidth
            // 
            this.lblCellWidth.AutoSize = true;
            this.lblCellWidth.Location = new System.Drawing.Point(27, 147);
            this.lblCellWidth.Name = "lblCellWidth";
            this.lblCellWidth.Size = new System.Drawing.Size(45, 16);
            this.lblCellWidth.TabIndex = 478;
            this.lblCellWidth.Text = "Width";
            // 
            // cnbCellPitchY
            // 
            this.cnbCellPitchY._DecimalPlaces = 0;
            this.cnbCellPitchY._DefaultValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.cnbCellPitchY._IsSaveToIni = true;
            this.cnbCellPitchY._IsSaveToLog = true;
            this.cnbCellPitchY._IsShowCurrentValue = false;
            this.cnbCellPitchY._IsShowPopForm = true;
            this.cnbCellPitchY._Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cnbCellPitchY._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cnbCellPitchY._PmtName = ArtData.clsEnum.enuPmtName.Rec_Cell_Pitch_Y;
            this.cnbCellPitchY._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.cnbCellPitchY._TempValue = null;
            this.cnbCellPitchY._Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cnbCellPitchY.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cnbCellPitchY.Location = new System.Drawing.Point(186, 241);
            this.cnbCellPitchY.Name = "cnbCellPitchY";
            this.cnbCellPitchY.ReadOnly = true;
            this.cnbCellPitchY.Size = new System.Drawing.Size(67, 26);
            this.cnbCellPitchY.TabIndex = 477;
            this.cnbCellPitchY.Text = "0";
            this.cnbCellPitchY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCellPitchY
            // 
            this.lblCellPitchY.AutoSize = true;
            this.lblCellPitchY.Location = new System.Drawing.Point(27, 246);
            this.lblCellPitchY.Name = "lblCellPitchY";
            this.lblCellPitchY.Size = new System.Drawing.Size(54, 16);
            this.lblCellPitchY.TabIndex = 476;
            this.lblCellPitchY.Text = "Pitch Y";
            // 
            // cnbCellPitchX
            // 
            this.cnbCellPitchX._DecimalPlaces = 0;
            this.cnbCellPitchX._DefaultValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.cnbCellPitchX._IsSaveToIni = true;
            this.cnbCellPitchX._IsSaveToLog = true;
            this.cnbCellPitchX._IsShowCurrentValue = false;
            this.cnbCellPitchX._IsShowPopForm = true;
            this.cnbCellPitchX._Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cnbCellPitchX._Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cnbCellPitchX._PmtName = ArtData.clsEnum.enuPmtName.Rec_Cell_Pitch_X;
            this.cnbCellPitchX._PmtType = ArtData.clsEnum.enuPmtType.Recipe;
            this.cnbCellPitchX._TempValue = null;
            this.cnbCellPitchX._Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cnbCellPitchX.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cnbCellPitchX.Location = new System.Drawing.Point(186, 208);
            this.cnbCellPitchX.Name = "cnbCellPitchX";
            this.cnbCellPitchX.ReadOnly = true;
            this.cnbCellPitchX.Size = new System.Drawing.Size(67, 26);
            this.cnbCellPitchX.TabIndex = 475;
            this.cnbCellPitchX.Text = "1";
            this.cnbCellPitchX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCellPitchX
            // 
            this.lblCellPitchX.AutoSize = true;
            this.lblCellPitchX.Location = new System.Drawing.Point(27, 213);
            this.lblCellPitchX.Name = "lblCellPitchX";
            this.lblCellPitchX.Size = new System.Drawing.Size(54, 16);
            this.lblCellPitchX.TabIndex = 474;
            this.lblCellPitchX.Text = "Pitch X";
            // 
            // ucFunctionSetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tabControl1);
            this.Name = "ucFunctionSetting";
            this.Size = new System.Drawing.Size(894, 559);
            this.VisibleChanged += new System.EventHandler(this.ucMachineStatus_VisibleChanged);
            this.tabControl1.ResumeLayout(false);
            this.tPage_System.ResumeLayout(false);
            this.tPage_System.PerformLayout();
            this.tPage_Recipe.ResumeLayout(false);
            this.tPage_Recipe.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tPage_System;
        private System.Windows.Forms.TabPage tPage_Recipe;
        private System.Windows.Forms.Label label42;
        private ArtControlLib.comImgButton cbtn_CheckCorner;
        private System.Windows.Forms.Label label5;
        private ArtControlLib.comImgButton comImgButton5;
        private System.Windows.Forms.Label label6;
        private ArtControlLib.comImgButton comImgButton6;
        private ArtControlLib.comNumBox cnbCellHeight;
        private System.Windows.Forms.Label lblCellHeight;
        private ArtControlLib.comNumBox cnbCellWidthX;
        private System.Windows.Forms.Label lblCellWidth;
        private ArtControlLib.comNumBox cnbCellPitchY;
        private System.Windows.Forms.Label lblCellPitchY;
        private ArtControlLib.comNumBox cnbCellPitchX;
        private System.Windows.Forms.Label lblCellPitchX;
        private ArtControlLib.comNumBox cnbMagazineSlotNo;
        private System.Windows.Forms.Label lblMagazineSlotNo;
        private ArtControlLib.comNumBox cnbTrayRowNo;
        private System.Windows.Forms.Label lblTrayRowNo;
        private ArtControlLib.comNumBox cnbTrayColumnNo;
        private System.Windows.Forms.Label lblTrayColumnNo;
        private System.Windows.Forms.ComboBox cboNgDischargeMode;
        private ArtControlLib.comNumBox comNumBox1;
        private System.Windows.Forms.Label lblNgDischargeMode;
        private System.Windows.Forms.Label lblEnablePressStation;
        private ArtControlLib.comImgButton comImgButton1;
    }
}

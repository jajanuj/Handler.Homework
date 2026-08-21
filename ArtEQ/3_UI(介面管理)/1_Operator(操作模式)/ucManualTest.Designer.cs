namespace ArtEQ
{
    partial class ucManualTest
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.clsBoat_Carry = new ArtProcModuleLib.clsCarry_TQIC();
            this.ucCtrlLane1 = new ArtEQ.ucCtrlLane();
            this.btn_LaneUnloadBoat = new ArtProcModuleLib.comManualActionButton();
            this.btn_LaneLoadBoat = new ArtProcModuleLib.comManualActionButton();
            this.btnArmProcess = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.clsBoat_Carry);
            this.panel1.Location = new System.Drawing.Point(236, 180);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 225);
            this.panel1.TabIndex = 19;
            // 
            // clsBoat_Carry
            // 
            this.clsBoat_Carry._bIsMultiSelect = false;
            this.clsBoat_Carry._bIsMultiSelectLock = false;
            this.clsBoat_Carry._bIsMultiSelectOnce = false;
            this.clsBoat_Carry._FontSize = 0.3F;
            this.clsBoat_Carry._IsShowChipIndex = false;
            this.clsBoat_Carry._IsShowChipName = false;
            this.clsBoat_Carry._Support_Num_To_En = false;
            this.clsBoat_Carry._Support_Row_Col = false;
            this.clsBoat_Carry._UI_Return_Method_Num = false;
            this.clsBoat_Carry.Location = new System.Drawing.Point(0, 0);
            this.clsBoat_Carry.Name = "clsBoat_Carry";
            this.clsBoat_Carry.Size = new System.Drawing.Size(418, 225);
            this.clsBoat_Carry.TabIndex = 0;
            // 
            // ucCtrlLane1
            // 
            this.ucCtrlLane1.BackColor = System.Drawing.SystemColors.Control;
            this.ucCtrlLane1.bCanCreateData = false;
            this.ucCtrlLane1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucCtrlLane1.Location = new System.Drawing.Point(362, 100);
            this.ucCtrlLane1.Margin = new System.Windows.Forms.Padding(2);
            this.ucCtrlLane1.Name = "ucCtrlLane1";
            this.ucCtrlLane1.Size = new System.Drawing.Size(166, 75);
            this.ucCtrlLane1.TabIndex = 18;
            // 
            // btn_LaneUnloadBoat
            // 
            this.btn_LaneUnloadBoat._ActionAP = ArtData.clsEnum.enuProcName.AP_Lane;
            this.btn_LaneUnloadBoat._Source = ArtData.clsEnum.enuProcName.PM_Lane;
            this.btn_LaneUnloadBoat._Target = ArtData.clsEnum.enuProcName.PM_SMEMA_Unload;
            this.btn_LaneUnloadBoat._UnitActionMode = null;
            this.btn_LaneUnloadBoat.Location = new System.Drawing.Point(533, 100);
            this.btn_LaneUnloadBoat.Name = "btn_LaneUnloadBoat";
            this.btn_LaneUnloadBoat.Size = new System.Drawing.Size(121, 75);
            this.btn_LaneUnloadBoat.TabIndex = 17;
            this.btn_LaneUnloadBoat.Text = "Unload Boat";
            this.btn_LaneUnloadBoat.UseVisualStyleBackColor = true;
            this.btn_LaneUnloadBoat._EventBeforeClick += new ArtProcModuleLib.comManualActionButton.EventBeforeClick(this.comManualActionButton1__EventBeforeClick);
            // 
            // btn_LaneLoadBoat
            // 
            this.btn_LaneLoadBoat._ActionAP = ArtData.clsEnum.enuProcName.AP_Lane;
            this.btn_LaneLoadBoat._Source = ArtData.clsEnum.enuProcName.PM_SMEMA_Load;
            this.btn_LaneLoadBoat._Target = ArtData.clsEnum.enuProcName.PM_Lane;
            this.btn_LaneLoadBoat._UnitActionMode = null;
            this.btn_LaneLoadBoat.Location = new System.Drawing.Point(236, 100);
            this.btn_LaneLoadBoat.Name = "btn_LaneLoadBoat";
            this.btn_LaneLoadBoat.Size = new System.Drawing.Size(121, 75);
            this.btn_LaneLoadBoat.TabIndex = 16;
            this.btn_LaneLoadBoat.Text = "Load Boat";
            this.btn_LaneLoadBoat.UseVisualStyleBackColor = true;
            this.btn_LaneLoadBoat._EventBeforeClick += new ArtProcModuleLib.comManualActionButton.EventBeforeClick(this.comManualActionButton1__EventBeforeClick);
            // 
            // btnArmProcess
            // 
            this.btnArmProcess.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.btnArmProcess.Location = new System.Drawing.Point(20, 103);
            this.btnArmProcess.Name = "btnArmProcess";
            this.btnArmProcess.Size = new System.Drawing.Size(121, 75);
            this.btnArmProcess.TabIndex = 20;
            this.btnArmProcess.Text = "ArmProcess";
            this.btnArmProcess.UseVisualStyleBackColor = true;
            this.btnArmProcess.Click += new System.EventHandler(this.btnArmProcess_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnArmProcess);
            this.groupBox1.Location = new System.Drawing.Point(676, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 327);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Arm";
            // 
            // button1
            // 
            this.button1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.button1.Location = new System.Drawing.Point(20, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 75);
            this.button1.TabIndex = 21;
            this.button1.Text = "Initial";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ucManualTest
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucCtrlLane1);
            this.Controls.Add(this.btn_LaneUnloadBoat);
            this.Controls.Add(this.btn_LaneLoadBoat);
            this.Name = "ucManualTest";
            this.Size = new System.Drawing.Size(1084, 682);
            this.VisibleChanged += new System.EventHandler(this.ucMachineStatus_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ArtProcModuleLib.comManualActionButton btn_LaneLoadBoat;
        private ArtProcModuleLib.comManualActionButton btn_LaneUnloadBoat;
        private ucCtrlLane ucCtrlLane1;
        private System.Windows.Forms.Panel panel1;
        private ArtProcModuleLib.clsCarry_TQIC clsBoat_Carry;
        private System.Windows.Forms.Button btnArmProcess;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
    }
}

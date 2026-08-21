namespace ArtTeach
{
    partial class ucJogTeach
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.Panel_MotionCtrl1 = new System.Windows.Forms.Panel();
            this.txtAxisName = new System.Windows.Forms.Label();
            this.btnFunction = new System.Windows.Forms.Label();
            this.txtCurrentPosition = new System.Windows.Forms.Label();
            this.Panel_PositionSet = new System.Windows.Forms.Panel();
            this.txtPositionName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numSetValue = new System.Windows.Forms.Label();
            this.txtINP = new ArtTeach.clsRoundAngleBtn();
            this.btnSafe = new ArtTeach.clsRoundAngleBtn();
            this.txtORG = new ArtTeach.clsRoundAngleBtn();
            this.btnJogA = new ArtTeach.clsRoundAngleBtn();
            this.txtMEL = new ArtTeach.clsRoundAngleBtn();
            this.txtPEL = new ArtTeach.clsRoundAngleBtn();
            this.txtALM = new ArtTeach.clsRoundAngleBtn();
            this.btnJogB = new ArtTeach.clsRoundAngleBtn();
            this.btnSVON = new ArtTeach.clsRoundAngleBtn();
            this.clsRoundAngleBtn1 = new ArtTeach.clsRoundAngleBtn();
            this.btnSetValue = new ArtTeach.clsRoundAngleBtn();
            this.panel2.SuspendLayout();
            this.Panel_MotionCtrl1.SuspendLayout();
            this.Panel_PositionSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.Panel_MotionCtrl1);
            this.panel2.Controls.Add(this.Panel_PositionSet);
            this.panel2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(284, 144);
            this.panel2.TabIndex = 1025;
            // 
            // Panel_MotionCtrl1
            // 
            this.Panel_MotionCtrl1.Controls.Add(this.txtINP);
            this.Panel_MotionCtrl1.Controls.Add(this.btnSafe);
            this.Panel_MotionCtrl1.Controls.Add(this.txtORG);
            this.Panel_MotionCtrl1.Controls.Add(this.btnJogA);
            this.Panel_MotionCtrl1.Controls.Add(this.txtMEL);
            this.Panel_MotionCtrl1.Controls.Add(this.txtAxisName);
            this.Panel_MotionCtrl1.Controls.Add(this.txtPEL);
            this.Panel_MotionCtrl1.Controls.Add(this.btnFunction);
            this.Panel_MotionCtrl1.Controls.Add(this.txtALM);
            this.Panel_MotionCtrl1.Controls.Add(this.txtCurrentPosition);
            this.Panel_MotionCtrl1.Controls.Add(this.btnJogB);
            this.Panel_MotionCtrl1.Controls.Add(this.btnSVON);
            this.Panel_MotionCtrl1.Location = new System.Drawing.Point(0, 0);
            this.Panel_MotionCtrl1.Name = "Panel_MotionCtrl1";
            this.Panel_MotionCtrl1.Size = new System.Drawing.Size(284, 88);
            this.Panel_MotionCtrl1.TabIndex = 1076;
            // 
            // txtAxisName
            // 
            this.txtAxisName.BackColor = System.Drawing.Color.Transparent;
            this.txtAxisName.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.txtAxisName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtAxisName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtAxisName.Location = new System.Drawing.Point(3, 5);
            this.txtAxisName.Name = "txtAxisName";
            this.txtAxisName.Size = new System.Drawing.Size(152, 22);
            this.txtAxisName.TabIndex = 1058;
            this.txtAxisName.Text = "Bond Hand Y";
            this.txtAxisName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFunction
            // 
            this.btnFunction.BackColor = System.Drawing.Color.Transparent;
            this.btnFunction.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFunction.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFunction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.btnFunction.Location = new System.Drawing.Point(243, 10);
            this.btnFunction.Name = "btnFunction";
            this.btnFunction.Size = new System.Drawing.Size(34, 30);
            this.btnFunction.TabIndex = 1060;
            this.btnFunction.Text = "mm";
            this.btnFunction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCurrentPosition
            // 
            this.txtCurrentPosition.BackColor = System.Drawing.Color.Transparent;
            this.txtCurrentPosition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtCurrentPosition.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.txtCurrentPosition.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentPosition.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtCurrentPosition.Location = new System.Drawing.Point(166, 5);
            this.txtCurrentPosition.Name = "txtCurrentPosition";
            this.txtCurrentPosition.Size = new System.Drawing.Size(77, 40);
            this.txtCurrentPosition.TabIndex = 1059;
            this.txtCurrentPosition.Text = "0000.000";
            this.txtCurrentPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Panel_PositionSet
            // 
            this.Panel_PositionSet.Controls.Add(this.txtPositionName);
            this.Panel_PositionSet.Controls.Add(this.label2);
            this.Panel_PositionSet.Controls.Add(this.numSetValue);
            this.Panel_PositionSet.Controls.Add(this.clsRoundAngleBtn1);
            this.Panel_PositionSet.Controls.Add(this.btnSetValue);
            this.Panel_PositionSet.Location = new System.Drawing.Point(0, 88);
            this.Panel_PositionSet.Name = "Panel_PositionSet";
            this.Panel_PositionSet.Size = new System.Drawing.Size(284, 53);
            this.Panel_PositionSet.TabIndex = 1077;
            // 
            // txtPositionName
            // 
            this.txtPositionName.BackColor = System.Drawing.Color.Transparent;
            this.txtPositionName.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPositionName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtPositionName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPositionName.Location = new System.Drawing.Point(3, 0);
            this.txtPositionName.Name = "txtPositionName";
            this.txtPositionName.Size = new System.Drawing.Size(140, 22);
            this.txtPositionName.TabIndex = 1064;
            this.txtPositionName.Text = "Position Name";
            this.txtPositionName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.label2.Location = new System.Drawing.Point(92, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 30);
            this.label2.TabIndex = 1066;
            this.label2.Text = "mm";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numSetValue
            // 
            this.numSetValue.BackColor = System.Drawing.Color.Transparent;
            this.numSetValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.numSetValue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.numSetValue.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numSetValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.numSetValue.Location = new System.Drawing.Point(6, 23);
            this.numSetValue.Name = "numSetValue";
            this.numSetValue.Size = new System.Drawing.Size(80, 28);
            this.numSetValue.TabIndex = 1063;
            this.numSetValue.Text = "0000.000";
            this.numSetValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.numSetValue.Click += new System.EventHandler(this.numSetValue_Click);
            // 
            // txtINP
            // 
            this.txtINP._AutoAdjustBackColor = true;
            this.txtINP.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(194)))), ((int)(((byte)(214)))));
            this.txtINP.Enabled = false;
            this.txtINP.EnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.txtINP.EnterForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtINP.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtINP.FlatAppearance.BorderSize = 0;
            this.txtINP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.txtINP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.txtINP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtINP.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtINP.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(194)))), ((int)(((byte)(214)))));
            this.txtINP.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtINP.Location = new System.Drawing.Point(131, 30);
            this.txtINP.Name = "txtINP";
            this.txtINP.PressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.txtINP.PressForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtINP.Radius = 5;
            this.txtINP.Size = new System.Drawing.Size(32, 15);
            this.txtINP.TabIndex = 1087;
            this.txtINP.Text = "INP";
            this.txtINP.UseVisualStyleBackColor = true;
            // 
            // btnSafe
            // 
            this.btnSafe._AutoAdjustBackColor = true;
            this.btnSafe.DisableColor = System.Drawing.Color.Empty;
            this.btnSafe.EnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.btnSafe.EnterForeColor = System.Drawing.Color.White;
            this.btnSafe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSafe.FlatAppearance.BorderSize = 0;
            this.btnSafe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSafe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSafe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSafe.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSafe.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(167)))), ((int)(((byte)(175)))));
            this.btnSafe.HoverForeColor = System.Drawing.Color.White;
            this.btnSafe.Location = new System.Drawing.Point(74, 46);
            this.btnSafe.Name = "btnSafe";
            this.btnSafe.PressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.btnSafe.PressForeColor = System.Drawing.Color.White;
            this.btnSafe.Radius = 5;
            this.btnSafe.Size = new System.Drawing.Size(62, 40);
            this.btnSafe.TabIndex = 1088;
            this.btnSafe.Text = "SAFE";
            this.btnSafe.UseVisualStyleBackColor = true;
            this.btnSafe.Click += new System.EventHandler(this.btnSafe_Click);
            // 
            // txtORG
            // 
            this.txtORG._AutoAdjustBackColor = true;
            this.txtORG.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(194)))), ((int)(((byte)(214)))));
            this.txtORG.Enabled = false;
            this.txtORG.EnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.txtORG.EnterForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtORG.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtORG.FlatAppearance.BorderSize = 0;
            this.txtORG.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.txtORG.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.txtORG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtORG.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtORG.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(194)))), ((int)(((byte)(214)))));
            this.txtORG.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtORG.Location = new System.Drawing.Point(101, 30);
            this.txtORG.Name = "txtORG";
            this.txtORG.PressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.txtORG.PressForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtORG.Radius = 5;
            this.txtORG.Size = new System.Drawing.Size(32, 15);
            this.txtORG.TabIndex = 1086;
            this.txtORG.Text = "ORG";
            this.txtORG.UseVisualStyleBackColor = true;
            // 
            // btnJogA
            // 
            this.btnJogA._AutoAdjustBackColor = true;
            this.btnJogA.DisableColor = System.Drawing.Color.Empty;
            this.btnJogA.EnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(84)))), ((int)(((byte)(99)))));
            this.btnJogA.EnterForeColor = System.Drawing.Color.White;
            this.btnJogA.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnJogA.FlatAppearance.BorderSize = 0;
            this.btnJogA.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnJogA.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnJogA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJogA.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.btnJogA.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.btnJogA.HoverForeColor = System.Drawing.Color.White;
            this.btnJogA.Location = new System.Drawing.Point(147, 46);
            this.btnJogA.Name = "btnJogA";
            this.btnJogA.PressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(104)))), ((int)(((byte)(119)))));
            this.btnJogA.PressForeColor = System.Drawing.Color.White;
            this.btnJogA.Radius = 5;
            this.btnJogA.Size = new System.Drawing.Size(62, 40);
            this.btnJogA.TabIndex = 1061;
            this.btnJogA.Text = "Jog+";
            this.btnJogA.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnJogA.UseVisualStyleBackColor = true;
            this.btnJogA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogA_MouseDown);
            this.btnJogA.MouseEnter += new System.EventHandler(this.btnJogA_MouseEnter);
            this.btnJogA.MouseLeave += new System.EventHandler(this.btnJogA_MouseLeave);
            this.btnJogA.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogA_MouseUp);
            // 
            // txtMEL
            // 
            this.txtMEL._AutoAdjustBackColor = true;
            this.txtMEL.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(194)))), ((int)(((byte)(214)))));
            this.txtMEL.Enabled = false;
            this.txtMEL.EnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.txtMEL.EnterForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtMEL.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtMEL.FlatAppearance.BorderSize = 0;
            this.txtMEL.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.txtMEL.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.txtMEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtMEL.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMEL.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(194)))), ((int)(((byte)(214)))));
            this.txtMEL.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtMEL.Location = new System.Drawing.Point(67, 30);
            this.txtMEL.Name = "txtMEL";
            this.txtMEL.PressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.txtMEL.PressForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtMEL.Radius = 5;
            this.txtMEL.Size = new System.Drawing.Size(32, 15);
            this.txtMEL.TabIndex = 1085;
            this.txtMEL.Text = "MEL";
            this.txtMEL.UseVisualStyleBackColor = true;
            // 
            // txtPEL
            // 
            this.txtPEL._AutoAdjustBackColor = true;
            this.txtPEL.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(194)))), ((int)(((byte)(214)))));
            this.txtPEL.Enabled = false;
            this.txtPEL.EnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.txtPEL.EnterForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtPEL.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtPEL.FlatAppearance.BorderSize = 0;
            this.txtPEL.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.txtPEL.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.txtPEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtPEL.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPEL.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(194)))), ((int)(((byte)(214)))));
            this.txtPEL.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtPEL.Location = new System.Drawing.Point(35, 30);
            this.txtPEL.Name = "txtPEL";
            this.txtPEL.PressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.txtPEL.PressForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtPEL.Radius = 5;
            this.txtPEL.Size = new System.Drawing.Size(32, 15);
            this.txtPEL.TabIndex = 1084;
            this.txtPEL.Text = "PEL";
            this.txtPEL.UseVisualStyleBackColor = true;
            // 
            // txtALM
            // 
            this.txtALM._AutoAdjustBackColor = true;
            this.txtALM.DisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(194)))), ((int)(((byte)(214)))));
            this.txtALM.Enabled = false;
            this.txtALM.EnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.txtALM.EnterForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtALM.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtALM.FlatAppearance.BorderSize = 0;
            this.txtALM.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.txtALM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.txtALM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtALM.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtALM.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(194)))), ((int)(((byte)(214)))));
            this.txtALM.HoverForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtALM.Location = new System.Drawing.Point(3, 30);
            this.txtALM.Name = "txtALM";
            this.txtALM.PressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.txtALM.PressForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.txtALM.Radius = 5;
            this.txtALM.Size = new System.Drawing.Size(32, 15);
            this.txtALM.TabIndex = 1083;
            this.txtALM.Text = "ALM";
            this.txtALM.UseVisualStyleBackColor = true;
            // 
            // btnJogB
            // 
            this.btnJogB._AutoAdjustBackColor = true;
            this.btnJogB.DisableColor = System.Drawing.Color.Empty;
            this.btnJogB.EnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(84)))), ((int)(((byte)(99)))));
            this.btnJogB.EnterForeColor = System.Drawing.Color.White;
            this.btnJogB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnJogB.FlatAppearance.BorderSize = 0;
            this.btnJogB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnJogB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnJogB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJogB.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.btnJogB.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.btnJogB.HoverForeColor = System.Drawing.Color.White;
            this.btnJogB.Location = new System.Drawing.Point(219, 46);
            this.btnJogB.Name = "btnJogB";
            this.btnJogB.PressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(104)))), ((int)(((byte)(119)))));
            this.btnJogB.PressForeColor = System.Drawing.Color.White;
            this.btnJogB.Radius = 5;
            this.btnJogB.Size = new System.Drawing.Size(60, 40);
            this.btnJogB.TabIndex = 1062;
            this.btnJogB.Text = "Jog-";
            this.btnJogB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnJogB.UseVisualStyleBackColor = true;
            this.btnJogB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogB_MouseDown);
            this.btnJogB.MouseEnter += new System.EventHandler(this.btnJogB_MouseEnter);
            this.btnJogB.MouseHover += new System.EventHandler(this.btnJogB_MouseLeave);
            this.btnJogB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJogB_MouseUp);
            // 
            // btnSVON
            // 
            this.btnSVON._AutoAdjustBackColor = true;
            this.btnSVON.DisableColor = System.Drawing.Color.Empty;
            this.btnSVON.EnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.btnSVON.EnterForeColor = System.Drawing.Color.White;
            this.btnSVON.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSVON.FlatAppearance.BorderSize = 0;
            this.btnSVON.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSVON.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSVON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSVON.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSVON.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(102)))), ((int)(((byte)(124)))));
            this.btnSVON.HoverForeColor = System.Drawing.Color.White;
            this.btnSVON.Location = new System.Drawing.Point(3, 46);
            this.btnSVON.Name = "btnSVON";
            this.btnSVON.PressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.btnSVON.PressForeColor = System.Drawing.Color.White;
            this.btnSVON.Radius = 5;
            this.btnSVON.Size = new System.Drawing.Size(62, 40);
            this.btnSVON.TabIndex = 1069;
            this.btnSVON.Text = "SVON";
            this.btnSVON.UseVisualStyleBackColor = true;
            this.btnSVON.Click += new System.EventHandler(this.btnSVON_Click);
            // 
            // clsRoundAngleBtn1
            // 
            this.clsRoundAngleBtn1._AutoAdjustBackColor = true;
            this.clsRoundAngleBtn1.DisableColor = System.Drawing.Color.Empty;
            this.clsRoundAngleBtn1.EnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.clsRoundAngleBtn1.EnterForeColor = System.Drawing.Color.White;
            this.clsRoundAngleBtn1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.clsRoundAngleBtn1.FlatAppearance.BorderSize = 0;
            this.clsRoundAngleBtn1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.clsRoundAngleBtn1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.clsRoundAngleBtn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clsRoundAngleBtn1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.clsRoundAngleBtn1.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.clsRoundAngleBtn1.HoverForeColor = System.Drawing.Color.White;
            this.clsRoundAngleBtn1.Location = new System.Drawing.Point(219, 6);
            this.clsRoundAngleBtn1.Name = "clsRoundAngleBtn1";
            this.clsRoundAngleBtn1.PressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.clsRoundAngleBtn1.PressForeColor = System.Drawing.Color.White;
            this.clsRoundAngleBtn1.Radius = 5;
            this.clsRoundAngleBtn1.Size = new System.Drawing.Size(60, 40);
            this.clsRoundAngleBtn1.TabIndex = 1065;
            this.clsRoundAngleBtn1.Text = "GO";
            this.clsRoundAngleBtn1.UseVisualStyleBackColor = true;
            this.clsRoundAngleBtn1.Click += new System.EventHandler(this.btnGoValue_Click);
            // 
            // btnSetValue
            // 
            this.btnSetValue._AutoAdjustBackColor = true;
            this.btnSetValue.DisableColor = System.Drawing.Color.Gray;
            this.btnSetValue.EnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.btnSetValue.EnterForeColor = System.Drawing.Color.White;
            this.btnSetValue.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSetValue.FlatAppearance.BorderSize = 0;
            this.btnSetValue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSetValue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSetValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetValue.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.btnSetValue.HoverBackColor = System.Drawing.Color.Maroon;
            this.btnSetValue.HoverForeColor = System.Drawing.Color.White;
            this.btnSetValue.Location = new System.Drawing.Point(149, 6);
            this.btnSetValue.Name = "btnSetValue";
            this.btnSetValue.PressBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
            this.btnSetValue.PressForeColor = System.Drawing.Color.White;
            this.btnSetValue.Radius = 5;
            this.btnSetValue.Size = new System.Drawing.Size(60, 40);
            this.btnSetValue.TabIndex = 1068;
            this.btnSetValue.Text = "SET";
            this.btnSetValue.UseVisualStyleBackColor = true;
            this.btnSetValue.Click += new System.EventHandler(this.btnSetValue_Click);
            // 
            // ucJogTeach
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel2);
            this.MaximumSize = new System.Drawing.Size(288, 146);
            this.MinimumSize = new System.Drawing.Size(288, 146);
            this.Name = "ucJogTeach";
            this.Size = new System.Drawing.Size(288, 146);
            this.panel2.ResumeLayout(false);
            this.Panel_MotionCtrl1.ResumeLayout(false);
            this.Panel_PositionSet.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private clsRoundAngleBtn btnSetValue;
        private clsRoundAngleBtn clsRoundAngleBtn1;
        private System.Windows.Forms.Label numSetValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtPositionName;
        private clsRoundAngleBtn btnJogB;
        private clsRoundAngleBtn btnJogA;
        private System.Windows.Forms.Label txtCurrentPosition;
        private System.Windows.Forms.Label btnFunction;
        private System.Windows.Forms.Label txtAxisName;
        private System.Windows.Forms.Panel Panel_MotionCtrl1;
        private System.Windows.Forms.Panel Panel_PositionSet;
        private clsRoundAngleBtn btnSVON;
        private clsRoundAngleBtn txtALM;
        private clsRoundAngleBtn txtINP;
        private clsRoundAngleBtn txtORG;
        private clsRoundAngleBtn txtMEL;
        private clsRoundAngleBtn txtPEL;
        private clsRoundAngleBtn btnSafe;

    }
}

namespace ArtEQ
{
    partial class ucTitle_Panel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTitle_Panel));
            this.panel4 = new System.Windows.Forms.Panel();
            this.textVersion = new System.Windows.Forms.Label();
            this.textBoxTime = new System.Windows.Forms.Label();
            this.btnAllring = new System.Windows.Forms.Panel();
            this.comSignalTower1 = new ArtControlLib.comSignalTower();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnLogin = new ArtSystem.ucRoundButton();
            this.btnRecipeId = new ArtSystem.ucRoundButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEqStatus = new ArtSystem.ucRoundButton();
            this.label_EqStatus = new System.Windows.Forms.Label();
            this.btnLockDoor = new ArtSystem.ucRoundButton();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.panel4.Controls.Add(this.textVersion);
            this.panel4.Controls.Add(this.textBoxTime);
            this.panel4.Controls.Add(this.btnAllring);
            this.panel4.Controls.Add(this.comSignalTower1);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(146, 60);
            this.panel4.TabIndex = 933;
            // 
            // textVersion
            // 
            this.textVersion.AutoSize = true;
            this.textVersion.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textVersion.Location = new System.Drawing.Point(91, 3);
            this.textVersion.Name = "textVersion";
            this.textVersion.Size = new System.Drawing.Size(52, 13);
            this.textVersion.TabIndex = 932;
            this.textVersion.Text = "1.0.25.15";
            // 
            // textBoxTime
            // 
            this.textBoxTime.AutoSize = true;
            this.textBoxTime.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTime.Location = new System.Drawing.Point(27, 3);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.Size = new System.Drawing.Size(49, 13);
            this.textBoxTime.TabIndex = 931;
            this.textBoxTime.Text = "15:21:14";
            // 
            // btnAllring
            // 
            this.btnAllring.BackgroundImage = global::ArtEQ.Properties.Resources.Allring;
            this.btnAllring.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAllring.Location = new System.Drawing.Point(25, 10);
            this.btnAllring.Name = "btnAllring";
            this.btnAllring.Size = new System.Drawing.Size(113, 51);
            this.btnAllring.TabIndex = 930;
            this.btnAllring.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAllring_MouseDown);
            // 
            // comSignalTower1
            // 
            this.comSignalTower1._BuzzerDo1 = ArtData.clsEnum.enuDo.Signal_Buzzer;
            this.comSignalTower1._BuzzerDo2 = null;
            this.comSignalTower1._FlashSpeed = 100;
            this.comSignalTower1._IsKeepOnBule = false;
            this.comSignalTower1._IsKeepOnGreen = false;
            this.comSignalTower1._IsKeepOnRed = false;
            this.comSignalTower1._IsKeepOnYellow = false;
            this.comSignalTower1._LightBlueDo = null;
            this.comSignalTower1._LightGreenDo = ArtData.clsEnum.enuDo.Signal_Green;
            this.comSignalTower1._LightRedDo = ArtData.clsEnum.enuDo.Signal_Red;
            this.comSignalTower1._LightYellowDo = ArtData.clsEnum.enuDo.Signal_Yellow;
            this.comSignalTower1._ShowLightBlue = false;
            this.comSignalTower1._ShowLightGreen = true;
            this.comSignalTower1._ShowLightRed = true;
            this.comSignalTower1._ShowLightYellow = true;
            this.comSignalTower1.BackColor = System.Drawing.SystemColors.Control;
            this.comSignalTower1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comSignalTower1.Location = new System.Drawing.Point(4, 5);
            this.comSignalTower1.Margin = new System.Windows.Forms.Padding(4);
            this.comSignalTower1.Name = "comSignalTower1";
            this.comSignalTower1.Size = new System.Drawing.Size(16, 51);
            this.comSignalTower1.TabIndex = 21;
            // 
            // textBoxDate
            // 
            this.textBoxDate.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textBoxDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDate.Enabled = false;
            this.textBoxDate.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxDate.Location = new System.Drawing.Point(87, 42);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.ReadOnly = true;
            this.textBoxDate.Size = new System.Drawing.Size(70, 15);
            this.textBoxDate.TabIndex = 2;
            this.textBoxDate.Text = "20XX/0X/XX";
            this.textBoxDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxDate.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(182, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 934;
            this.label3.Text = "User ID :";
            // 
            // btnLogin
            // 
            this.btnLogin._AutoMouseOnColor = false;
            this.btnLogin._Color = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(101)))), ((int)(((byte)(126)))));
            this.btnLogin._EdgeColor = System.Drawing.SystemColors.Control;
            this.btnLogin._Font = new System.Drawing.Font("新細明體", 9.75F);
            this.btnLogin._MouseOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(131)))), ((int)(((byte)(156)))));
            this.btnLogin._NeedEdge = false;
            this.btnLogin._Radius = 10;
            this.btnLogin._ReadOnly = false;
            this.btnLogin._TextColor = System.Drawing.SystemColors.Control;
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.BackgroundImage")));
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("新細明體", 9.75F);
            this.btnLogin.Location = new System.Drawing.Point(240, 3);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(183, 23);
            this.btnLogin.TabIndex = 935;
            this.btnLogin.Text = "User ID";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.MouseEnter += new System.EventHandler(this.btnLogin_MouseEnter);
            this.btnLogin.MouseLeave += new System.EventHandler(this.btnLogin_MouseLeave);
            // 
            // btnRecipeId
            // 
            this.btnRecipeId._AutoMouseOnColor = false;
            this.btnRecipeId._Color = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(101)))), ((int)(((byte)(126)))));
            this.btnRecipeId._EdgeColor = System.Drawing.SystemColors.Control;
            this.btnRecipeId._Font = new System.Drawing.Font("新細明體", 9.75F);
            this.btnRecipeId._MouseOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(131)))), ((int)(((byte)(156)))));
            this.btnRecipeId._NeedEdge = false;
            this.btnRecipeId._Radius = 10;
            this.btnRecipeId._ReadOnly = false;
            this.btnRecipeId._TextColor = System.Drawing.SystemColors.Control;
            this.btnRecipeId.BackColor = System.Drawing.Color.Transparent;
            this.btnRecipeId.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRecipeId.BackgroundImage")));
            this.btnRecipeId.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRecipeId.FlatAppearance.BorderSize = 0;
            this.btnRecipeId.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRecipeId.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRecipeId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecipeId.Font = new System.Drawing.Font("新細明體", 9.75F);
            this.btnRecipeId.Location = new System.Drawing.Point(241, 59);
            this.btnRecipeId.Name = "btnRecipeId";
            this.btnRecipeId.Size = new System.Drawing.Size(429, 23);
            this.btnRecipeId.TabIndex = 937;
            this.btnRecipeId.Text = "Recipe.ini";
            this.btnRecipeId.UseVisualStyleBackColor = false;
            this.btnRecipeId.Visible = false;
            this.btnRecipeId.Click += new System.EventHandler(this.ucRoundButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(174, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 936;
            this.label1.Text = "Recipe ID :";
            this.label1.Visible = false;
            // 
            // btnEqStatus
            // 
            this.btnEqStatus._AutoMouseOnColor = false;
            this.btnEqStatus._Color = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(101)))), ((int)(((byte)(126)))));
            this.btnEqStatus._EdgeColor = System.Drawing.SystemColors.Control;
            this.btnEqStatus._Font = new System.Drawing.Font("新細明體", 9.75F);
            this.btnEqStatus._MouseOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(101)))), ((int)(((byte)(126)))));
            this.btnEqStatus._NeedEdge = false;
            this.btnEqStatus._Radius = 10;
            this.btnEqStatus._ReadOnly = false;
            this.btnEqStatus._TextColor = System.Drawing.SystemColors.Control;
            this.btnEqStatus.BackColor = System.Drawing.Color.Transparent;
            this.btnEqStatus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEqStatus.BackgroundImage")));
            this.btnEqStatus.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnEqStatus.FlatAppearance.BorderSize = 0;
            this.btnEqStatus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEqStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEqStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEqStatus.Font = new System.Drawing.Font("新細明體", 9.75F);
            this.btnEqStatus.Location = new System.Drawing.Point(240, 32);
            this.btnEqStatus.Name = "btnEqStatus";
            this.btnEqStatus.Size = new System.Drawing.Size(182, 23);
            this.btnEqStatus.TabIndex = 939;
            this.btnEqStatus.Text = "Idle";
            this.btnEqStatus.UseVisualStyleBackColor = false;
            // 
            // label_EqStatus
            // 
            this.label_EqStatus.AutoSize = true;
            this.label_EqStatus.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EqStatus.ForeColor = System.Drawing.Color.Black;
            this.label_EqStatus.Location = new System.Drawing.Point(176, 37);
            this.label_EqStatus.Name = "label_EqStatus";
            this.label_EqStatus.Size = new System.Drawing.Size(58, 13);
            this.label_EqStatus.TabIndex = 938;
            this.label_EqStatus.Text = "Eq Status :";
            // 
            // btnLockDoor
            // 
            this.btnLockDoor._AutoMouseOnColor = true;
            this.btnLockDoor._Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLockDoor._EdgeColor = System.Drawing.Color.White;
            this.btnLockDoor._Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLockDoor._MouseOnColor = System.Drawing.SystemColors.Control;
            this.btnLockDoor._NeedEdge = true;
            this.btnLockDoor._Radius = 10;
            this.btnLockDoor._ReadOnly = false;
            this.btnLockDoor._TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.btnLockDoor.BackColor = System.Drawing.Color.Transparent;
            this.btnLockDoor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLockDoor.BackgroundImage")));
            this.btnLockDoor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnLockDoor.FlatAppearance.BorderSize = 0;
            this.btnLockDoor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLockDoor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLockDoor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLockDoor.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLockDoor.Location = new System.Drawing.Point(544, 22);
            this.btnLockDoor.Name = "btnLockDoor";
            this.btnLockDoor.Size = new System.Drawing.Size(126, 33);
            this.btnLockDoor.TabIndex = 940;
            this.btnLockDoor.Text = "Lock Door";
            this.btnLockDoor.UseVisualStyleBackColor = false;
            this.btnLockDoor.Click += new System.EventHandler(this.btnLockDoor_Click);
            // 
            // ucTitle_Panel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Controls.Add(this.btnLockDoor);
            this.Controls.Add(this.btnEqStatus);
            this.Controls.Add(this.label_EqStatus);
            this.Controls.Add(this.btnRecipeId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.textBoxDate);
            this.Name = "ucTitle_Panel";
            this.Size = new System.Drawing.Size(673, 60);
            this.VisibleChanged += new System.EventHandler(this.ucTitle_Panel_VisibleChanged);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.Panel btnAllring;
        private ArtControlLib.comSignalTower comSignalTower1;
        private System.Windows.Forms.Label label3;
        private ArtSystem.ucRoundButton btnLogin;
        private System.Windows.Forms.ToolTip toolTip1;
        private ArtSystem.ucRoundButton btnRecipeId;
        private System.Windows.Forms.Label label1;
        private ArtSystem.ucRoundButton btnEqStatus;
        private System.Windows.Forms.Label label_EqStatus;
        private System.Windows.Forms.Label textVersion;
        private System.Windows.Forms.Label textBoxTime;
        private ArtSystem.ucRoundButton btnLockDoor;
    }
}

namespace ArtEQ
{
    partial class ucTitle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTitle));
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.btnAllring = new System.Windows.Forms.Panel();
            this.comSignalTower1 = new ArtControlLib.comSignalTower();
            this.textVersion = new System.Windows.Forms.TextBox();
            this.label_UserID = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label_EqStatus = new System.Windows.Forms.Label();
            this.label_RecipeID = new System.Windows.Forms.Label();
            this.label_WarningMessage = new System.Windows.Forms.Label();
            this.btnWarningMessage = new ArtSystem.ucRoundButton();
            this.btnRecipeId = new ArtSystem.ucRoundButton();
            this.btnEqStatus = new ArtSystem.ucRoundButton();
            this.btnLogin = new ArtSystem.ucRoundButton();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.panel4.Controls.Add(this.textBoxTime);
            this.panel4.Controls.Add(this.textBoxDate);
            this.panel4.Controls.Add(this.btnAllring);
            this.panel4.Controls.Add(this.comSignalTower1);
            this.panel4.Controls.Add(this.textVersion);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 100);
            this.panel4.TabIndex = 933;
            // 
            // textBoxTime
            // 
            this.textBoxTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.textBoxTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTime.Enabled = false;
            this.textBoxTime.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxTime.Location = new System.Drawing.Point(79, 3);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.ReadOnly = true;
            this.textBoxTime.Size = new System.Drawing.Size(56, 15);
            this.textBoxTime.TabIndex = 3;
            this.textBoxTime.Text = "15:21:14";
            this.textBoxTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxDate
            // 
            this.textBoxDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.textBoxDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDate.Enabled = false;
            this.textBoxDate.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxDate.Location = new System.Drawing.Point(3, 3);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.ReadOnly = true;
            this.textBoxDate.Size = new System.Drawing.Size(70, 15);
            this.textBoxDate.TabIndex = 2;
            this.textBoxDate.Text = "20XX/0X/XX";
            // 
            // btnAllring
            // 
            this.btnAllring.BackgroundImage = global::ArtEQ.Properties.Resources.Allring;
            this.btnAllring.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAllring.Location = new System.Drawing.Point(24, 33);
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
            this.comSignalTower1.Location = new System.Drawing.Point(167, 24);
            this.comSignalTower1.Margin = new System.Windows.Forms.Padding(4);
            this.comSignalTower1.Name = "comSignalTower1";
            this.comSignalTower1.Size = new System.Drawing.Size(16, 69);
            this.comSignalTower1.TabIndex = 21;
            // 
            // textVersion
            // 
            this.textVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(228)))), ((int)(((byte)(236)))));
            this.textVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textVersion.Enabled = false;
            this.textVersion.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textVersion.Location = new System.Drawing.Point(141, 3);
            this.textVersion.Name = "textVersion";
            this.textVersion.ReadOnly = true;
            this.textVersion.Size = new System.Drawing.Size(55, 15);
            this.textVersion.TabIndex = 44;
            this.textVersion.Text = "1.0.25.15";
            this.textVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label_UserID
            // 
            this.label_UserID.AutoSize = true;
            this.label_UserID.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_UserID.ForeColor = System.Drawing.SystemColors.Control;
            this.label_UserID.Location = new System.Drawing.Point(12, 163);
            this.label_UserID.Name = "label_UserID";
            this.label_UserID.Size = new System.Drawing.Size(52, 13);
            this.label_UserID.TabIndex = 934;
            this.label_UserID.Text = "User ID :";
            // 
            // label_EqStatus
            // 
            this.label_EqStatus.AutoSize = true;
            this.label_EqStatus.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EqStatus.ForeColor = System.Drawing.SystemColors.Control;
            this.label_EqStatus.Location = new System.Drawing.Point(12, 114);
            this.label_EqStatus.Name = "label_EqStatus";
            this.label_EqStatus.Size = new System.Drawing.Size(58, 13);
            this.label_EqStatus.TabIndex = 936;
            this.label_EqStatus.Text = "Eq Status :";
            // 
            // label_RecipeID
            // 
            this.label_RecipeID.AutoSize = true;
            this.label_RecipeID.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RecipeID.ForeColor = System.Drawing.SystemColors.Control;
            this.label_RecipeID.Location = new System.Drawing.Point(12, 215);
            this.label_RecipeID.Name = "label_RecipeID";
            this.label_RecipeID.Size = new System.Drawing.Size(61, 13);
            this.label_RecipeID.TabIndex = 939;
            this.label_RecipeID.Text = "Recipe ID :";
            // 
            // label_WarningMessage
            // 
            this.label_WarningMessage.AutoSize = true;
            this.label_WarningMessage.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_WarningMessage.ForeColor = System.Drawing.SystemColors.Control;
            this.label_WarningMessage.Location = new System.Drawing.Point(12, 289);
            this.label_WarningMessage.Name = "label_WarningMessage";
            this.label_WarningMessage.Size = new System.Drawing.Size(100, 13);
            this.label_WarningMessage.TabIndex = 941;
            this.label_WarningMessage.Text = "Warning Message :";
            // 
            // btnWarningMessage
            // 
            this.btnWarningMessage._AutoMouseOnColor = true;
            this.btnWarningMessage._Color = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(101)))), ((int)(((byte)(126)))));
            this.btnWarningMessage._EdgeColor = System.Drawing.SystemColors.Control;
            this.btnWarningMessage._Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWarningMessage._MouseOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(131)))), ((int)(((byte)(156)))));
            this.btnWarningMessage._NeedEdge = false;
            this.btnWarningMessage._Radius = 10;
            this.btnWarningMessage._ReadOnly = false;
            this.btnWarningMessage._TextColor = System.Drawing.SystemColors.Control;
            this.btnWarningMessage.BackColor = System.Drawing.Color.Transparent;
            this.btnWarningMessage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWarningMessage.BackgroundImage")));
            this.btnWarningMessage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnWarningMessage.FlatAppearance.BorderSize = 0;
            this.btnWarningMessage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnWarningMessage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnWarningMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWarningMessage.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWarningMessage.Location = new System.Drawing.Point(4, 305);
            this.btnWarningMessage.Name = "btnWarningMessage";
            this.btnWarningMessage.Size = new System.Drawing.Size(193, 23);
            this.btnWarningMessage.TabIndex = 942;
            this.btnWarningMessage.Text = "Warning Message";
            this.btnWarningMessage.UseVisualStyleBackColor = false;
            this.btnWarningMessage.Click += new System.EventHandler(this.btnWarningMessage_Click);
            this.btnWarningMessage.MouseEnter += new System.EventHandler(this.btnWarningMessage_MouseEnter);
            this.btnWarningMessage.MouseLeave += new System.EventHandler(this.btnWarningMessage_MouseLeave);
            // 
            // btnRecipeId
            // 
            this.btnRecipeId._AutoMouseOnColor = true;
            this.btnRecipeId._Color = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(101)))), ((int)(((byte)(126)))));
            this.btnRecipeId._EdgeColor = System.Drawing.SystemColors.Control;
            this.btnRecipeId._Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.btnRecipeId.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecipeId.Location = new System.Drawing.Point(3, 234);
            this.btnRecipeId.Name = "btnRecipeId";
            this.btnRecipeId.Size = new System.Drawing.Size(193, 46);
            this.btnRecipeId.TabIndex = 938;
            this.btnRecipeId.Text = "Recipe.ini";
            this.btnRecipeId.UseVisualStyleBackColor = false;
            this.btnRecipeId.Click += new System.EventHandler(this.btnRecipeId_Click);
            this.btnRecipeId.MouseEnter += new System.EventHandler(this.btnRecipeId_MouseEnter);
            this.btnRecipeId.MouseLeave += new System.EventHandler(this.btnRecipeId_MouseLeave);
            // 
            // btnEqStatus
            // 
            this.btnEqStatus._AutoMouseOnColor = false;
            this.btnEqStatus._Color = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(101)))), ((int)(((byte)(126)))));
            this.btnEqStatus._EdgeColor = System.Drawing.SystemColors.Control;
            this.btnEqStatus._Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.btnEqStatus.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEqStatus.Location = new System.Drawing.Point(3, 133);
            this.btnEqStatus.Name = "btnEqStatus";
            this.btnEqStatus.Size = new System.Drawing.Size(193, 23);
            this.btnEqStatus.TabIndex = 937;
            this.btnEqStatus.Text = "Idle";
            this.btnEqStatus.UseVisualStyleBackColor = false;
            // 
            // btnLogin
            // 
            this.btnLogin._AutoMouseOnColor = true;
            this.btnLogin._Color = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(101)))), ((int)(((byte)(126)))));
            this.btnLogin._EdgeColor = System.Drawing.SystemColors.Control;
            this.btnLogin._Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.btnLogin.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(3, 182);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(193, 23);
            this.btnLogin.TabIndex = 935;
            this.btnLogin.Text = "Super User";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.MouseEnter += new System.EventHandler(this.btnLogin_MouseEnter);
            this.btnLogin.MouseLeave += new System.EventHandler(this.btnLogin_MouseLeave);
            // 
            // ucTitle_PC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(64)))), ((int)(((byte)(79)))));
            this.Controls.Add(this.btnWarningMessage);
            this.Controls.Add(this.label_WarningMessage);
            this.Controls.Add(this.label_RecipeID);
            this.Controls.Add(this.btnRecipeId);
            this.Controls.Add(this.btnEqStatus);
            this.Controls.Add(this.label_EqStatus);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label_UserID);
            this.Controls.Add(this.panel4);
            this.Name = "ucTitle_PC";
            this.Size = new System.Drawing.Size(200, 670);
            this.VisibleChanged += new System.EventHandler(this.ucTitle_PC_VisibleChanged);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.Panel btnAllring;
        private ArtControlLib.comSignalTower comSignalTower1;
        private System.Windows.Forms.TextBox textVersion;
        private System.Windows.Forms.Label label_UserID;
        private ArtSystem.ucRoundButton btnLogin;
        private System.Windows.Forms.ToolTip toolTip1;
        private ArtSystem.ucRoundButton btnEqStatus;
        private System.Windows.Forms.Label label_EqStatus;
        private ArtSystem.ucRoundButton btnRecipeId;
        private System.Windows.Forms.Label label_RecipeID;
        private System.Windows.Forms.Label label_WarningMessage;
        private ArtSystem.ucRoundButton btnWarningMessage;
    }
}

namespace CenoCC
{
    partial class userBaseInfo
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
            this.txtUa = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRole = new System.Windows.Forms.Button();
            this.btnTeam = new System.Windows.Forms.Button();
            this.btnUa = new System.Windows.Forms.Button();
            this.btnLoginName = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCurrentPassword = new System.Windows.Forms.TextBox();
            this.btnPassword = new System.Windows.Forms.Button();
            this.cboTeam = new System.Windows.Forms.ComboBox();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.lblTips = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudLimitTheDial = new System.Windows.Forms.NumericUpDown();
            this.btnLimitTheDial = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimitTheDial)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUa
            // 
            this.txtUa.Location = new System.Drawing.Point(152, 60);
            this.txtUa.Name = "txtUa";
            this.txtUa.Size = new System.Drawing.Size(120, 21);
            this.txtUa.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "姓名";
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(152, 116);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(120, 21);
            this.txtLoginName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "登录名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "部门";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "角色";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(152, 284);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(120, 21);
            this.txtPassword.TabIndex = 14;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "密码";
            // 
            // btnRole
            // 
            this.btnRole.Location = new System.Drawing.Point(197, 255);
            this.btnRole.Name = "btnRole";
            this.btnRole.Size = new System.Drawing.Size(75, 23);
            this.btnRole.TabIndex = 12;
            this.btnRole.Tag = "user_baseinfo_role";
            this.btnRole.Text = "确定";
            this.btnRole.UseVisualStyleBackColor = true;
            this.btnRole.Click += new System.EventHandler(this.btnRole_Click);
            // 
            // btnTeam
            // 
            this.btnTeam.Location = new System.Drawing.Point(197, 199);
            this.btnTeam.Name = "btnTeam";
            this.btnTeam.Size = new System.Drawing.Size(75, 23);
            this.btnTeam.TabIndex = 9;
            this.btnTeam.Tag = "user_baseinfo_team";
            this.btnTeam.Text = "确定";
            this.btnTeam.UseVisualStyleBackColor = true;
            this.btnTeam.Click += new System.EventHandler(this.btnTeam_Click);
            // 
            // btnUa
            // 
            this.btnUa.Location = new System.Drawing.Point(197, 87);
            this.btnUa.Name = "btnUa";
            this.btnUa.Size = new System.Drawing.Size(75, 23);
            this.btnUa.TabIndex = 3;
            this.btnUa.Tag = "user_baseinfo_ua";
            this.btnUa.Text = "确定";
            this.btnUa.UseVisualStyleBackColor = true;
            this.btnUa.Click += new System.EventHandler(this.btnUa_Click);
            // 
            // btnLoginName
            // 
            this.btnLoginName.Location = new System.Drawing.Point(197, 143);
            this.btnLoginName.Name = "btnLoginName";
            this.btnLoginName.Size = new System.Drawing.Size(75, 23);
            this.btnLoginName.TabIndex = 6;
            this.btnLoginName.Tag = "user_baseinfo_loginname";
            this.btnLoginName.Text = "确定";
            this.btnLoginName.UseVisualStyleBackColor = true;
            this.btnLoginName.Click += new System.EventHandler(this.btnLoginName_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 314);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "确认密码";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(152, 311);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(120, 21);
            this.txtConfirmPassword.TabIndex = 16;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(12, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "当前密码*";
            // 
            // txtCurrentPassword
            // 
            this.txtCurrentPassword.Location = new System.Drawing.Point(152, 33);
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.Size = new System.Drawing.Size(120, 21);
            this.txtCurrentPassword.TabIndex = 0;
            this.txtCurrentPassword.UseSystemPasswordChar = true;
            // 
            // btnPassword
            // 
            this.btnPassword.Location = new System.Drawing.Point(197, 338);
            this.btnPassword.Name = "btnPassword";
            this.btnPassword.Size = new System.Drawing.Size(75, 23);
            this.btnPassword.TabIndex = 19;
            this.btnPassword.Tag = "user_baseinfo_password";
            this.btnPassword.Text = "确定";
            this.btnPassword.UseVisualStyleBackColor = true;
            this.btnPassword.Click += new System.EventHandler(this.btnPassword_Click);
            // 
            // cboTeam
            // 
            this.cboTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTeam.FormattingEnabled = true;
            this.cboTeam.Location = new System.Drawing.Point(152, 173);
            this.cboTeam.Name = "cboTeam";
            this.cboTeam.Size = new System.Drawing.Size(120, 20);
            this.cboTeam.TabIndex = 8;
            // 
            // cboRole
            // 
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Location = new System.Drawing.Point(152, 229);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(120, 20);
            this.cboRole.TabIndex = 11;
            // 
            // lblTips
            // 
            this.lblTips.AutoSize = true;
            this.lblTips.Location = new System.Drawing.Point(14, 13);
            this.lblTips.Name = "lblTips";
            this.lblTips.Size = new System.Drawing.Size(11, 12);
            this.lblTips.TabIndex = 20;
            this.lblTips.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 369);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "同号码限呼(0不限制)";
            // 
            // nudLimitTheDial
            // 
            this.nudLimitTheDial.Location = new System.Drawing.Point(152, 367);
            this.nudLimitTheDial.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudLimitTheDial.Name = "nudLimitTheDial";
            this.nudLimitTheDial.Size = new System.Drawing.Size(120, 21);
            this.nudLimitTheDial.TabIndex = 22;
            // 
            // btnLimitTheDial
            // 
            this.btnLimitTheDial.Location = new System.Drawing.Point(197, 394);
            this.btnLimitTheDial.Name = "btnLimitTheDial";
            this.btnLimitTheDial.Size = new System.Drawing.Size(75, 23);
            this.btnLimitTheDial.TabIndex = 23;
            this.btnLimitTheDial.Tag = "user_baseinfo_limitthedial";
            this.btnLimitTheDial.Text = "确定";
            this.btnLimitTheDial.UseVisualStyleBackColor = true;
            this.btnLimitTheDial.Click += new System.EventHandler(this.btnLimitTheDial_Click);
            // 
            // userBaseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 439);
            this.Controls.Add(this.btnLimitTheDial);
            this.Controls.Add(this.nudLimitTheDial);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblTips);
            this.Controls.Add(this.cboRole);
            this.Controls.Add(this.cboTeam);
            this.Controls.Add(this.btnPassword);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCurrentPassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.btnLoginName);
            this.Controls.Add(this.btnUa);
            this.Controls.Add(this.btnRole);
            this.Controls.Add(this.btnTeam);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLoginName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUa);
            this.Name = "userBaseInfo";
            this.Text = "基本信息";
            ((System.ComponentModel.ISupportInitialize)(this.nudLimitTheDial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUa;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLoginName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRole;
        private System.Windows.Forms.Button btnTeam;
        private System.Windows.Forms.Button btnUa;
        private System.Windows.Forms.Button btnLoginName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCurrentPassword;
        private System.Windows.Forms.Button btnPassword;
        private System.Windows.Forms.ComboBox cboTeam;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.Label lblTips;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudLimitTheDial;
        private System.Windows.Forms.Button btnLimitTheDial;
    }
}
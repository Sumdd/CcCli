namespace CenoCC
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblTip = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetting = new System.Windows.Forms.Button();
            this.cbxServer = new System.Windows.Forms.ComboBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtDuid = new System.Windows.Forms.TextBox();
            this.txtDpwd = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblDuid = new System.Windows.Forms.Label();
            this.lblDpwd = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbxNetwork = new System.Windows.Forms.ComboBox();
            this.cbxKvp = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAccount
            // 
            this.txtAccount.Font = new System.Drawing.Font("幼圆", 14F, System.Drawing.FontStyle.Bold);
            this.txtAccount.Location = new System.Drawing.Point(23, 126);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(254, 28);
            this.txtAccount.TabIndex = 1;
            this.txtAccount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAccount.TextChanged += new System.EventHandler(this.txtAccount_TextChanged);
            // 
            // txtPwd
            // 
            this.txtPwd.Font = new System.Drawing.Font("幼圆", 14F);
            this.txtPwd.Location = new System.Drawing.Point(23, 160);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(254, 28);
            this.txtPwd.TabIndex = 2;
            this.txtPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPwd.UseSystemPasswordChar = true;
            this.txtPwd.TextChanged += new System.EventHandler(this.txtPwd_TextChanged);
            this.txtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPwd_KeyPress);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("幼圆", 14F, System.Drawing.FontStyle.Bold);
            this.btnOK.Location = new System.Drawing.Point(23, 194);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(254, 28);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "登陆";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("幼圆", 14F);
            this.lblTip.ForeColor = System.Drawing.Color.Red;
            this.lblTip.Location = new System.Drawing.Point(19, 230);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(0, 19);
            this.lblTip.TabIndex = 100;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("幼圆", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(92, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 35);
            this.label2.TabIndex = 100;
            this.label2.Text = "电话客户端";
            // 
            // btnSetting
            // 
            this.btnSetting.Font = new System.Drawing.Font("幼圆", 14F, System.Drawing.FontStyle.Bold);
            this.btnSetting.Location = new System.Drawing.Point(212, 277);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(65, 30);
            this.btnSetting.TabIndex = 5;
            this.btnSetting.Tag = "1";
            this.btnSetting.Text = "精简";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // cbxServer
            // 
            this.cbxServer.Font = new System.Drawing.Font("幼圆", 14F);
            this.cbxServer.FormattingEnabled = true;
            this.cbxServer.Location = new System.Drawing.Point(127, 333);
            this.cbxServer.Name = "cbxServer";
            this.cbxServer.Size = new System.Drawing.Size(150, 27);
            this.cbxServer.TabIndex = 6;
            // 
            // txtDatabase
            // 
            this.txtDatabase.Font = new System.Drawing.Font("幼圆", 14F);
            this.txtDatabase.Location = new System.Drawing.Point(127, 366);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(150, 28);
            this.txtDatabase.TabIndex = 7;
            this.txtDatabase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDuid
            // 
            this.txtDuid.Font = new System.Drawing.Font("幼圆", 14F);
            this.txtDuid.Location = new System.Drawing.Point(127, 400);
            this.txtDuid.Name = "txtDuid";
            this.txtDuid.Size = new System.Drawing.Size(150, 28);
            this.txtDuid.TabIndex = 8;
            this.txtDuid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDpwd
            // 
            this.txtDpwd.Font = new System.Drawing.Font("幼圆", 14F);
            this.txtDpwd.Location = new System.Drawing.Point(127, 434);
            this.txtDpwd.Name = "txtDpwd";
            this.txtDpwd.Size = new System.Drawing.Size(150, 28);
            this.txtDpwd.TabIndex = 9;
            this.txtDpwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDpwd.UseSystemPasswordChar = true;
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("幼圆", 14F);
            this.lblServer.Location = new System.Drawing.Point(19, 336);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(69, 19);
            this.lblServer.TabIndex = 100;
            this.lblServer.Text = "服务器";
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Font = new System.Drawing.Font("幼圆", 14F);
            this.lblDatabase.Location = new System.Drawing.Point(19, 369);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(69, 19);
            this.lblDatabase.TabIndex = 100;
            this.lblDatabase.Text = "数据库";
            // 
            // lblDuid
            // 
            this.lblDuid.AutoSize = true;
            this.lblDuid.Font = new System.Drawing.Font("幼圆", 14F);
            this.lblDuid.Location = new System.Drawing.Point(19, 403);
            this.lblDuid.Name = "lblDuid";
            this.lblDuid.Size = new System.Drawing.Size(49, 19);
            this.lblDuid.TabIndex = 100;
            this.lblDuid.Text = "账户";
            // 
            // lblDpwd
            // 
            this.lblDpwd.AutoSize = true;
            this.lblDpwd.Font = new System.Drawing.Font("幼圆", 14F);
            this.lblDpwd.Location = new System.Drawing.Point(19, 437);
            this.lblDpwd.Name = "lblDpwd";
            this.lblDpwd.Size = new System.Drawing.Size(49, 19);
            this.lblDpwd.TabIndex = 100;
            this.lblDpwd.Text = "密码";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::CenoCC.Properties.Resources.k24;
            this.pictureBox3.Location = new System.Drawing.Point(35, 162);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(24, 24);
            this.pictureBox3.TabIndex = 102;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CenoCC.Properties.Resources.s24;
            this.pictureBox2.Location = new System.Drawing.Point(35, 128);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.TabIndex = 101;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CenoCC.Properties.Resources.xsd;
            this.pictureBox1.Location = new System.Drawing.Point(23, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(58, 52);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // cbxNetwork
            // 
            this.cbxNetwork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxNetwork.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold);
            this.cbxNetwork.FormattingEnabled = true;
            this.cbxNetwork.Location = new System.Drawing.Point(23, 468);
            this.cbxNetwork.Name = "cbxNetwork";
            this.cbxNetwork.Size = new System.Drawing.Size(254, 20);
            this.cbxNetwork.TabIndex = 10;
            this.cbxNetwork.SelectedIndexChanged += new System.EventHandler(this.cbxNetwork_SelectedIndexChanged);
            // 
            // cbxKvp
            // 
            this.cbxKvp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxKvp.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold);
            this.cbxKvp.FormattingEnabled = true;
            this.cbxKvp.Location = new System.Drawing.Point(23, 248);
            this.cbxKvp.Name = "cbxKvp";
            this.cbxKvp.Size = new System.Drawing.Size(254, 20);
            this.cbxKvp.TabIndex = 4;
            this.cbxKvp.SelectedIndexChanged += new System.EventHandler(this.cbxKvp_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(21, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 15);
            this.label1.TabIndex = 105;
            this.label1.Text = "选择呼叫中心服务器";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 515);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxKvp);
            this.Controls.Add(this.cbxNetwork);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblDpwd);
            this.Controls.Add(this.lblDuid);
            this.Controls.Add(this.lblDatabase);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.txtDpwd);
            this.Controls.Add(this.txtDuid);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.cbxServer);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtAccount);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.Resizable = false;
            this.Tag = "";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.ComboBox cbxServer;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TextBox txtDuid;
        private System.Windows.Forms.TextBox txtDpwd;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Label lblDuid;
        private System.Windows.Forms.Label lblDpwd;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ComboBox cbxNetwork;
        private System.Windows.Forms.ComboBox cbxKvp;
        private System.Windows.Forms.Label label1;
    }
}
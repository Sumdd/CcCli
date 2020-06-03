namespace CenoCC {
    partial class Args_Safe {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.layout = new System.Windows.Forms.Panel();
            this.btnYes = new System.Windows.Forms.Button();
            this.safegb = new System.Windows.Forms.GroupBox();
            this.loginNameValue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.updatePwdValue = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.confirmNewPwdValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.newPwdValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.oldPwdValue = new System.Windows.Forms.TextBox();
            this.agentNameValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.layout.SuspendLayout();
            this.safegb.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.btnYes);
            this.layout.Controls.Add(this.safegb);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(420, 400);
            this.layout.TabIndex = 2;
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(333, 365);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 12;
            this.btnYes.Text = "确定";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // safegb
            // 
            this.safegb.Controls.Add(this.loginNameValue);
            this.safegb.Controls.Add(this.label5);
            this.safegb.Controls.Add(this.updatePwdValue);
            this.safegb.Controls.Add(this.label4);
            this.safegb.Controls.Add(this.confirmNewPwdValue);
            this.safegb.Controls.Add(this.label3);
            this.safegb.Controls.Add(this.newPwdValue);
            this.safegb.Controls.Add(this.label2);
            this.safegb.Controls.Add(this.oldPwdValue);
            this.safegb.Controls.Add(this.agentNameValue);
            this.safegb.Controls.Add(this.label1);
            this.safegb.Location = new System.Drawing.Point(10, 10);
            this.safegb.Name = "safegb";
            this.safegb.Size = new System.Drawing.Size(400, 185);
            this.safegb.TabIndex = 0;
            this.safegb.TabStop = false;
            this.safegb.Text = "安全";
            // 
            // loginNameValue
            // 
            this.loginNameValue.Location = new System.Drawing.Point(75, 46);
            this.loginNameValue.Name = "loginNameValue";
            this.loginNameValue.ReadOnly = true;
            this.loginNameValue.Size = new System.Drawing.Size(320, 21);
            this.loginNameValue.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "登录名";
            // 
            // updatePwdValue
            // 
            this.updatePwdValue.AutoSize = true;
            this.updatePwdValue.Location = new System.Drawing.Point(75, 154);
            this.updatePwdValue.Name = "updatePwdValue";
            this.updatePwdValue.Size = new System.Drawing.Size(72, 16);
            this.updatePwdValue.TabIndex = 11;
            this.updatePwdValue.Text = "修改密码";
            this.updatePwdValue.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "确认密码";
            // 
            // confirmNewPwdValue
            // 
            this.confirmNewPwdValue.Location = new System.Drawing.Point(75, 127);
            this.confirmNewPwdValue.Name = "confirmNewPwdValue";
            this.confirmNewPwdValue.Size = new System.Drawing.Size(320, 21);
            this.confirmNewPwdValue.TabIndex = 10;
            this.confirmNewPwdValue.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "新密码";
            // 
            // newPwdValue
            // 
            this.newPwdValue.Location = new System.Drawing.Point(75, 100);
            this.newPwdValue.Name = "newPwdValue";
            this.newPwdValue.Size = new System.Drawing.Size(320, 21);
            this.newPwdValue.TabIndex = 8;
            this.newPwdValue.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "原密码";
            // 
            // oldPwdValue
            // 
            this.oldPwdValue.Location = new System.Drawing.Point(75, 73);
            this.oldPwdValue.Name = "oldPwdValue";
            this.oldPwdValue.Size = new System.Drawing.Size(320, 21);
            this.oldPwdValue.TabIndex = 6;
            this.oldPwdValue.UseSystemPasswordChar = true;
            // 
            // agentNameValue
            // 
            this.agentNameValue.Location = new System.Drawing.Point(75, 19);
            this.agentNameValue.Name = "agentNameValue";
            this.agentNameValue.Size = new System.Drawing.Size(320, 21);
            this.agentNameValue.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "姓名";
            // 
            // Args_Safe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 400);
            this.Controls.Add(this.layout);
            this.Name = "Args_Safe";
            this.Text = "录音";
            this.layout.ResumeLayout(false);
            this.safegb.ResumeLayout(false);
            this.safegb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.GroupBox safegb;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox agentNameValue;
        private System.Windows.Forms.TextBox oldPwdValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newPwdValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox confirmNewPwdValue;
        private System.Windows.Forms.CheckBox updatePwdValue;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.TextBox loginNameValue;
        private System.Windows.Forms.Label label5;
    }
}
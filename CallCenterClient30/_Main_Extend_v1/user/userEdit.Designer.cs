namespace CenoCC
{
    partial class userEdit
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
            this.txtDomainName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSipServerIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtChPassword = new System.Windows.Forms.TextBox();
            this.txtSipPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRegTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUsingSelect = new System.Windows.Forms.Button();
            this.btnUsing = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDomainName
            // 
            this.txtDomainName.Location = new System.Drawing.Point(152, 12);
            this.txtDomainName.Name = "txtDomainName";
            this.txtDomainName.Size = new System.Drawing.Size(120, 21);
            this.txtDomainName.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "分机SIP注册域";
            // 
            // txtSipServerIp
            // 
            this.txtSipServerIp.Location = new System.Drawing.Point(152, 39);
            this.txtSipServerIp.Name = "txtSipServerIp";
            this.txtSipServerIp.Size = new System.Drawing.Size(120, 21);
            this.txtSipServerIp.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "分机SIP注册地址";
            // 
            // txtChPassword
            // 
            this.txtChPassword.Location = new System.Drawing.Point(152, 66);
            this.txtChPassword.Name = "txtChPassword";
            this.txtChPassword.Size = new System.Drawing.Size(120, 21);
            this.txtChPassword.TabIndex = 15;
            // 
            // txtSipPort
            // 
            this.txtSipPort.Location = new System.Drawing.Point(152, 93);
            this.txtSipPort.Name = "txtSipPort";
            this.txtSipPort.Size = new System.Drawing.Size(120, 21);
            this.txtSipPort.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "分机密码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "SIP端口";
            // 
            // txtRegTime
            // 
            this.txtRegTime.Location = new System.Drawing.Point(152, 120);
            this.txtRegTime.Name = "txtRegTime";
            this.txtRegTime.Size = new System.Drawing.Size(120, 21);
            this.txtRegTime.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "SIP注册时间";
            // 
            // btnUsingSelect
            // 
            this.btnUsingSelect.Location = new System.Drawing.Point(197, 226);
            this.btnUsingSelect.Name = "btnUsingSelect";
            this.btnUsingSelect.Size = new System.Drawing.Size(75, 23);
            this.btnUsingSelect.TabIndex = 22;
            this.btnUsingSelect.Tag = "user_sip_update_select";
            this.btnUsingSelect.Text = "选中生效";
            this.btnUsingSelect.UseVisualStyleBackColor = true;
            this.btnUsingSelect.Click += new System.EventHandler(this.btnUsing_Click);
            // 
            // btnUsing
            // 
            this.btnUsing.Location = new System.Drawing.Point(116, 226);
            this.btnUsing.Name = "btnUsing";
            this.btnUsing.Size = new System.Drawing.Size(75, 23);
            this.btnUsing.TabIndex = 21;
            this.btnUsing.Tag = "user_sip_update_all";
            this.btnUsing.Text = "全部生效";
            this.btnUsing.UseVisualStyleBackColor = true;
            this.btnUsing.Click += new System.EventHandler(this.btnUsing_Click);
            // 
            // userEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnUsingSelect);
            this.Controls.Add(this.btnUsing);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRegTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSipPort);
            this.Controls.Add(this.txtChPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSipServerIp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDomainName);
            this.Name = "userEdit";
            this.Text = "用户SIP编辑";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDomainName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSipServerIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtChPassword;
        private System.Windows.Forms.TextBox txtSipPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRegTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnUsingSelect;
        private System.Windows.Forms.Button btnUsing;
    }
}
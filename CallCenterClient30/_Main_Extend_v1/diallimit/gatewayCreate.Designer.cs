namespace CenoCC
{
    partial class gatewayCreate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gatewayCreate));
            this.label1 = new System.Windows.Forms.Label();
            this.txtGwName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboGwType = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtGwOName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtXML = new System.Windows.Forms.TextBox();
            this.btnXML = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSeconds = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "网关IP及端口($number$)";
            // 
            // txtGwName
            // 
            this.txtGwName.Location = new System.Drawing.Point(152, 39);
            this.txtGwName.Name = "txtGwName";
            this.txtGwName.Size = new System.Drawing.Size(320, 21);
            this.txtGwName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "网关类型";
            // 
            // cboGwType
            // 
            this.cboGwType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGwType.FormattingEnabled = true;
            this.cboGwType.Items.AddRange(new object[] {
            "gateway",
            "internal",
            "external"});
            this.cboGwType.Location = new System.Drawing.Point(152, 65);
            this.cboGwType.Name = "cboGwType";
            this.cboGwType.Size = new System.Drawing.Size(320, 20);
            this.cboGwType.TabIndex = 6;
            this.cboGwType.SelectedIndexChanged += new System.EventHandler(this.cboGwType_SelectedIndexChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(397, 384);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 19;
            this.btnOk.Tag = "diallimit_gateway_ok";
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtGwOName
            // 
            this.txtGwOName.Location = new System.Drawing.Point(152, 91);
            this.txtGwOName.Name = "txtGwOName";
            this.txtGwOName.Size = new System.Drawing.Size(320, 21);
            this.txtGwOName.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "自定义网关类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "网关名称";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(152, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(320, 21);
            this.txtName.TabIndex = 2;
            // 
            // txtXML
            // 
            this.txtXML.Location = new System.Drawing.Point(14, 118);
            this.txtXML.Multiline = true;
            this.txtXML.Name = "txtXML";
            this.txtXML.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtXML.Size = new System.Drawing.Size(458, 150);
            this.txtXML.TabIndex = 9;
            this.txtXML.DoubleClick += new System.EventHandler(this.txtXML_DoubleClick);
            // 
            // btnXML
            // 
            this.btnXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXML.Location = new System.Drawing.Point(316, 384);
            this.btnXML.Name = "btnXML";
            this.btnXML.Size = new System.Drawing.Size(75, 23);
            this.btnXML.TabIndex = 18;
            this.btnXML.Tag = "diallimit_gateway_XMLedit";
            this.btnXML.Text = "XML修改";
            this.btnXML.UseVisualStyleBackColor = true;
            this.btnXML.Click += new System.EventHandler(this.btnXML_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(152, 274);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(320, 21);
            this.txtPassword.TabIndex = 11;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(152, 301);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(320, 21);
            this.txtIP.TabIndex = 13;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(152, 328);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(320, 21);
            this.txtPort.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 277);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "密码($password$)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 304);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "IP($ip$)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 331);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "端口($port$)";
            // 
            // txtSeconds
            // 
            this.txtSeconds.Location = new System.Drawing.Point(152, 355);
            this.txtSeconds.Name = "txtSeconds";
            this.txtSeconds.Size = new System.Drawing.Size(320, 21);
            this.txtSeconds.TabIndex = 17;
            this.txtSeconds.Text = "75";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 358);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "过期时间($seconds$)";
            // 
            // gatewayCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 416);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSeconds);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnXML);
            this.Controls.Add(this.txtXML);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtGwOName);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cboGwType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtGwName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "gatewayCreate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "gatewayCreate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGwName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboGwType;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtGwOName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtXML;
        private System.Windows.Forms.Button btnXML;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSeconds;
        private System.Windows.Forms.Label label8;
    }
}
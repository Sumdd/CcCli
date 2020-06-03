namespace CenoCC {
    partial class IMS {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IMS));
            this.btnOK = new System.Windows.Forms.Button();
            this.txtIMSlocation = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtIMSimport = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtXML = new System.Windows.Forms.TextBox();
            this.txtModelLocation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(432, 357);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(40, 23);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtIMSlocation
            // 
            this.txtIMSlocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIMSlocation.Location = new System.Drawing.Point(158, 6);
            this.txtIMSlocation.Name = "txtIMSlocation";
            this.txtIMSlocation.ReadOnly = true;
            this.txtIMSlocation.Size = new System.Drawing.Size(314, 21);
            this.txtIMSlocation.TabIndex = 16;
            this.txtIMSlocation.DoubleClick += new System.EventHandler(this.txtIMSlocation_DoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "IMS输出位置(服务器位置)";
            // 
            // txtIMSimport
            // 
            this.txtIMSimport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIMSimport.Location = new System.Drawing.Point(158, 60);
            this.txtIMSimport.Name = "txtIMSimport";
            this.txtIMSimport.ReadOnly = true;
            this.txtIMSimport.Size = new System.Drawing.Size(314, 21);
            this.txtIMSimport.TabIndex = 18;
            this.txtIMSimport.DoubleClick += new System.EventHandler(this.txtIMSimport_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "IMS数据导入";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(391, 106);
            this.label2.TabIndex = 22;
            this.label2.Text = "说明:对应的生成数据库中的电话号码、网关、软交换中的网关;\r\n$number$标记模板中号码位置;\r\n$password$标记模板中密码位置\r\n$ip$标记模板中" +
    "IP\r\n$port$标记模板中端口\r\n$seconds$注册时长(可空,默认75秒)\r\n$account$绑定坐席登录账号(可空)\r\n$tnumber$真实号码" +
    "(可空)";
            // 
            // txtXML
            // 
            this.txtXML.Location = new System.Drawing.Point(13, 201);
            this.txtXML.Multiline = true;
            this.txtXML.Name = "txtXML";
            this.txtXML.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtXML.Size = new System.Drawing.Size(459, 150);
            this.txtXML.TabIndex = 23;
            // 
            // txtModelLocation
            // 
            this.txtModelLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtModelLocation.Location = new System.Drawing.Point(158, 33);
            this.txtModelLocation.Name = "txtModelLocation";
            this.txtModelLocation.ReadOnly = true;
            this.txtModelLocation.Size = new System.Drawing.Size(314, 21);
            this.txtModelLocation.TabIndex = 24;
            this.txtModelLocation.DoubleClick += new System.EventHandler(this.txtModelLocation_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "IMS模板导入";
            // 
            // IMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 387);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtModelLocation);
            this.Controls.Add(this.txtXML);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIMSimport);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtIMSlocation);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "IMS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IMS号码批量新增";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtIMSlocation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtIMSimport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtXML;
        private System.Windows.Forms.TextBox txtModelLocation;
        private System.Windows.Forms.Label label3;
    }
}
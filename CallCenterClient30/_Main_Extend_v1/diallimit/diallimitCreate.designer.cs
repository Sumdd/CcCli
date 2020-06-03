namespace CenoCC {
    partial class diallimitCreate {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(diallimitCreate));
            this.startNumberValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.endNumberValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.cbxShare = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxGateway = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // startNumberValue
            // 
            this.startNumberValue.Location = new System.Drawing.Point(152, 12);
            this.startNumberValue.Name = "startNumberValue";
            this.startNumberValue.Size = new System.Drawing.Size(120, 21);
            this.startNumberValue.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "添加号码起";
            // 
            // endNumberValue
            // 
            this.endNumberValue.Location = new System.Drawing.Point(152, 39);
            this.endNumberValue.Name = "endNumberValue";
            this.endNumberValue.Size = new System.Drawing.Size(120, 21);
            this.endNumberValue.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "添加号码止";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(12, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(260, 42);
            this.label3.TabIndex = 4;
            this.label3.Text = "注意：号码必须为数字，如果添加为一段号码，那么起止号码都要填写，开始号码不小于结束号码；如果仅添加一个号码，只填写起始号码即可";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(197, 226);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "前缀(0)";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(152, 66);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(120, 21);
            this.txtPrefix.TabIndex = 6;
            // 
            // cbxShare
            // 
            this.cbxShare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxShare.FormattingEnabled = true;
            this.cbxShare.Location = new System.Drawing.Point(152, 143);
            this.cbxShare.Name = "cbxShare";
            this.cbxShare.Size = new System.Drawing.Size(120, 20);
            this.cbxShare.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "号码类别";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "网关";
            // 
            // cbxGateway
            // 
            this.cbxGateway.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGateway.FormattingEnabled = true;
            this.cbxGateway.Location = new System.Drawing.Point(14, 117);
            this.cbxGateway.Name = "cbxGateway";
            this.cbxGateway.Size = new System.Drawing.Size(258, 20);
            this.cbxGateway.TabIndex = 10;
            // 
            // diallimitCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbxGateway);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxShare);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.endNumberValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startNumberValue);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "diallimitCreate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加号码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox startNumberValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox endNumberValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.ComboBox cbxShare;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxGateway;
    }
}
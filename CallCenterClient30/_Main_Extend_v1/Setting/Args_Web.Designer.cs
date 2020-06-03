namespace CenoCC {
    partial class Args_Web {
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
            this.btnReset = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.urlgb = new System.Windows.Forms.GroupBox();
            this.OpenExtendUrlValue = new System.Windows.Forms.CheckBox();
            this.OpenHomeUrlValue = new System.Windows.Forms.CheckBox();
            this.ExtendUrlValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.HomeUrlValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.QuickWebsiteValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.layout.SuspendLayout();
            this.urlgb.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.groupBox1);
            this.layout.Controls.Add(this.btnReset);
            this.layout.Controls.Add(this.btnYes);
            this.layout.Controls.Add(this.urlgb);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(420, 400);
            this.layout.TabIndex = 2;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(252, 365);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(333, 365);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 3;
            this.btnYes.Text = "确定";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // urlgb
            // 
            this.urlgb.Controls.Add(this.OpenExtendUrlValue);
            this.urlgb.Controls.Add(this.OpenHomeUrlValue);
            this.urlgb.Controls.Add(this.ExtendUrlValue);
            this.urlgb.Controls.Add(this.label2);
            this.urlgb.Controls.Add(this.HomeUrlValue);
            this.urlgb.Controls.Add(this.label1);
            this.urlgb.Location = new System.Drawing.Point(10, 10);
            this.urlgb.Name = "urlgb";
            this.urlgb.Size = new System.Drawing.Size(400, 124);
            this.urlgb.TabIndex = 2;
            this.urlgb.TabStop = false;
            this.urlgb.Text = "催收系统";
            // 
            // OpenExtendUrlValue
            // 
            this.OpenExtendUrlValue.AutoSize = true;
            this.OpenExtendUrlValue.Location = new System.Drawing.Point(75, 95);
            this.OpenExtendUrlValue.Name = "OpenExtendUrlValue";
            this.OpenExtendUrlValue.Size = new System.Drawing.Size(72, 16);
            this.OpenExtendUrlValue.TabIndex = 7;
            this.OpenExtendUrlValue.Text = "来电弹屏";
            this.OpenExtendUrlValue.UseVisualStyleBackColor = true;
            // 
            // OpenHomeUrlValue
            // 
            this.OpenHomeUrlValue.AutoSize = true;
            this.OpenHomeUrlValue.Location = new System.Drawing.Point(75, 46);
            this.OpenHomeUrlValue.Name = "OpenHomeUrlValue";
            this.OpenHomeUrlValue.Size = new System.Drawing.Size(120, 16);
            this.OpenHomeUrlValue.TabIndex = 6;
            this.OpenHomeUrlValue.Text = "默认打开催收网站";
            this.OpenHomeUrlValue.UseVisualStyleBackColor = true;
            // 
            // ExtendUrlValue
            // 
            this.ExtendUrlValue.Location = new System.Drawing.Point(75, 68);
            this.ExtendUrlValue.Name = "ExtendUrlValue";
            this.ExtendUrlValue.Size = new System.Drawing.Size(320, 21);
            this.ExtendUrlValue.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "弹屏地址";
            // 
            // HomeUrlValue
            // 
            this.HomeUrlValue.Location = new System.Drawing.Point(75, 19);
            this.HomeUrlValue.Name = "HomeUrlValue";
            this.HomeUrlValue.Size = new System.Drawing.Size(320, 21);
            this.HomeUrlValue.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "网站地址";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.QuickWebsiteValue);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(10, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 219);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "快捷网站";
            // 
            // QuickWebsiteValue
            // 
            this.QuickWebsiteValue.Location = new System.Drawing.Point(75, 19);
            this.QuickWebsiteValue.Multiline = true;
            this.QuickWebsiteValue.Name = "QuickWebsiteValue";
            this.QuickWebsiteValue.Size = new System.Drawing.Size(320, 185);
            this.QuickWebsiteValue.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "换行隔开";
            // 
            // Args_Web
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 400);
            this.Controls.Add(this.layout);
            this.Name = "Args_Web";
            this.Text = "录音";
            this.layout.ResumeLayout(false);
            this.urlgb.ResumeLayout(false);
            this.urlgb.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.GroupBox urlgb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox ExtendUrlValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox OpenHomeUrlValue;
        private System.Windows.Forms.CheckBox OpenExtendUrlValue;
        private System.Windows.Forms.TextBox HomeUrlValue;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox QuickWebsiteValue;
        private System.Windows.Forms.Label label4;
    }
}
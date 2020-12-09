namespace CenoCC
{
    partial class inruleOp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(inruleOp));
            this.txtInruleName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInruleIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrdernum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtInruleSuffix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInruleport = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInruleua = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtInruleName
            // 
            this.txtInruleName.Location = new System.Drawing.Point(152, 12);
            this.txtInruleName.Name = "txtInruleName";
            this.txtInruleName.Size = new System.Drawing.Size(120, 21);
            this.txtInruleName.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "内呼规则名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "内呼规则IP";
            // 
            // txtInruleIP
            // 
            this.txtInruleIP.Location = new System.Drawing.Point(152, 39);
            this.txtInruleIP.Name = "txtInruleIP";
            this.txtInruleIP.Size = new System.Drawing.Size(120, 21);
            this.txtInruleIP.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "唯一索引(不可重复)";
            // 
            // txtOrdernum
            // 
            this.txtOrdernum.Location = new System.Drawing.Point(152, 147);
            this.txtOrdernum.Name = "txtOrdernum";
            this.txtOrdernum.Size = new System.Drawing.Size(120, 21);
            this.txtOrdernum.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "内呼规则前缀";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(197, 194);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "添加";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtInruleSuffix
            // 
            this.txtInruleSuffix.Location = new System.Drawing.Point(152, 120);
            this.txtInruleSuffix.Name = "txtInruleSuffix";
            this.txtInruleSuffix.Size = new System.Drawing.Size(120, 21);
            this.txtInruleSuffix.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "内呼规则端口";
            // 
            // txtInruleport
            // 
            this.txtInruleport.Location = new System.Drawing.Point(152, 66);
            this.txtInruleport.Name = "txtInruleport";
            this.txtInruleport.Size = new System.Drawing.Size(120, 21);
            this.txtInruleport.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "内呼规则ua";
            // 
            // txtInruleua
            // 
            this.txtInruleua.Location = new System.Drawing.Point(152, 93);
            this.txtInruleua.Name = "txtInruleua";
            this.txtInruleua.Size = new System.Drawing.Size(120, 21);
            this.txtInruleua.TabIndex = 8;
            // 
            // inruleOp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 229);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtInruleua);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtInruleport);
            this.Controls.Add(this.txtInruleSuffix);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOrdernum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInruleIP);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtInruleName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "inruleOp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "内呼规则添加";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInruleName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInruleIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOrdernum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtInruleSuffix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtInruleport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtInruleua;
    }
}
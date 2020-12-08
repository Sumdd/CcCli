namespace CenoCC
{
    partial class wblistOp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wblistOp));
            this.txtWbname = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWbnumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrdernum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbxWbtype = new System.Windows.Forms.ComboBox();
            this.cbxWblimittype = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtWbname
            // 
            this.txtWbname.Location = new System.Drawing.Point(152, 12);
            this.txtWbname.Name = "txtWbname";
            this.txtWbname.Size = new System.Drawing.Size(120, 21);
            this.txtWbname.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "号码表达式";
            // 
            // txtWbnumber
            // 
            this.txtWbnumber.Location = new System.Drawing.Point(152, 39);
            this.txtWbnumber.Name = "txtWbnumber";
            this.txtWbnumber.Size = new System.Drawing.Size(120, 21);
            this.txtWbnumber.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "唯一索引(不可重复)";
            // 
            // txtOrdernum
            // 
            this.txtOrdernum.Location = new System.Drawing.Point(152, 66);
            this.txtOrdernum.Name = "txtOrdernum";
            this.txtOrdernum.Size = new System.Drawing.Size(120, 21);
            this.txtOrdernum.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "类型";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(197, 160);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "添加";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbxWbtype
            // 
            this.cbxWbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxWbtype.FormattingEnabled = true;
            this.cbxWbtype.Location = new System.Drawing.Point(152, 93);
            this.cbxWbtype.Name = "cbxWbtype";
            this.cbxWbtype.Size = new System.Drawing.Size(120, 20);
            this.cbxWbtype.TabIndex = 8;
            // 
            // cbxWblimittype
            // 
            this.cbxWblimittype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxWblimittype.FormattingEnabled = true;
            this.cbxWblimittype.Location = new System.Drawing.Point(152, 119);
            this.cbxWblimittype.Name = "cbxWblimittype";
            this.cbxWblimittype.Size = new System.Drawing.Size(120, 20);
            this.cbxWblimittype.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "限制类型";
            // 
            // wblistOp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 195);
            this.Controls.Add(this.cbxWblimittype);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxWbtype);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOrdernum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWbnumber);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtWbname);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "wblistOp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "路由添加";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWbname;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWbnumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOrdernum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbxWbtype;
        private System.Windows.Forms.ComboBox cbxWblimittype;
        private System.Windows.Forms.Label label4;
    }
}
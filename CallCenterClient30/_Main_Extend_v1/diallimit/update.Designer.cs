namespace CenoCC
{
    partial class update
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(update));
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTNumberOK = new System.Windows.Forms.Button();
            this.txtOrderNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOrderNum = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOkSame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "号码";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(152, 12);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(120, 21);
            this.txtNumber.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(197, 71);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Tag = "diallimit_number_update_number";
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "修改号码后对应的通话限制可能会不准确，请确认无误后再修改";
            // 
            // txtTNumber
            // 
            this.txtTNumber.Location = new System.Drawing.Point(152, 100);
            this.txtTNumber.Name = "txtTNumber";
            this.txtTNumber.Size = new System.Drawing.Size(120, 21);
            this.txtTNumber.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "真实号码";
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(12, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(260, 30);
            this.label4.TabIndex = 7;
            this.label4.Text = "通话记录、录音中优先使用真实号码填充，如果不填写默认使用号码填充";
            // 
            // btnTNumberOK
            // 
            this.btnTNumberOK.Location = new System.Drawing.Point(197, 157);
            this.btnTNumberOK.Name = "btnTNumberOK";
            this.btnTNumberOK.Size = new System.Drawing.Size(75, 23);
            this.btnTNumberOK.TabIndex = 8;
            this.btnTNumberOK.Tag = "diallimit_number_update_tnumber";
            this.btnTNumberOK.Text = "确定";
            this.btnTNumberOK.UseVisualStyleBackColor = true;
            this.btnTNumberOK.Click += new System.EventHandler(this.btnTNumberOK_Click);
            // 
            // txtOrderNum
            // 
            this.txtOrderNum.Location = new System.Drawing.Point(152, 186);
            this.txtOrderNum.Name = "txtOrderNum";
            this.txtOrderNum.Size = new System.Drawing.Size(120, 21);
            this.txtOrderNum.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "排序";
            // 
            // btnOrderNum
            // 
            this.btnOrderNum.Location = new System.Drawing.Point(197, 276);
            this.btnOrderNum.Name = "btnOrderNum";
            this.btnOrderNum.Size = new System.Drawing.Size(75, 23);
            this.btnOrderNum.TabIndex = 11;
            this.btnOrderNum.Tag = "diallimit_number_update_ordernum";
            this.btnOrderNum.Text = "确定";
            this.btnOrderNum.UseVisualStyleBackColor = true;
            this.btnOrderNum.Click += new System.EventHandler(this.btnOrderNum_Click);
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(12, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(260, 63);
            this.label6.TabIndex = 12;
            this.label6.Text = "数值可以为负99.998到正99.999之间,正序排列,如果想使用的号码靠前排序请合理填写此数值即可。其中-99.999是首发态值，不可再在此处设定";
            // 
            // btnOkSame
            // 
            this.btnOkSame.Location = new System.Drawing.Point(116, 71);
            this.btnOkSame.Name = "btnOkSame";
            this.btnOkSame.Size = new System.Drawing.Size(75, 23);
            this.btnOkSame.TabIndex = 13;
            this.btnOkSame.Tag = "diallimit_number_update_number_same";
            this.btnOkSame.Text = "重复确定";
            this.btnOkSame.UseVisualStyleBackColor = true;
            this.btnOkSame.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 311);
            this.Controls.Add(this.btnOkSame);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnOrderNum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtOrderNum);
            this.Controls.Add(this.btnTNumberOK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNumber);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "update";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "号码修改";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTNumberOK;
        private System.Windows.Forms.TextBox txtOrderNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOrderNum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOkSame;
    }
}
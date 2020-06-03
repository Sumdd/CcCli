namespace CenoCC
{
    partial class Fc
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
            this.cbxSwitch = new System.Windows.Forms.ComboBox();
            this.btnOKOnce = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.ckbNever = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbxSwitch
            // 
            this.cbxSwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSwitch.FormattingEnabled = true;
            this.cbxSwitch.Location = new System.Drawing.Point(23, 63);
            this.cbxSwitch.Name = "cbxSwitch";
            this.cbxSwitch.Size = new System.Drawing.Size(254, 20);
            this.cbxSwitch.TabIndex = 0;
            // 
            // btnOKOnce
            // 
            this.btnOKOnce.Location = new System.Drawing.Point(202, 112);
            this.btnOKOnce.Name = "btnOKOnce";
            this.btnOKOnce.Size = new System.Drawing.Size(75, 23);
            this.btnOKOnce.TabIndex = 1;
            this.btnOKOnce.Text = "确定";
            this.btnOKOnce.UseVisualStyleBackColor = true;
            this.btnOKOnce.Click += new System.EventHandler(this.btnOKOnce_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(121, 112);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "保存并确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ckbNever
            // 
            this.ckbNever.AutoSize = true;
            this.ckbNever.Location = new System.Drawing.Point(23, 90);
            this.ckbNever.Name = "ckbNever";
            this.ckbNever.Size = new System.Drawing.Size(72, 16);
            this.ckbNever.TabIndex = 3;
            this.ckbNever.Text = "不再显示";
            this.ckbNever.UseVisualStyleBackColor = true;
            // 
            // Fc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 158);
            this.Controls.Add(this.ckbNever);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnOKOnce);
            this.Controls.Add(this.cbxSwitch);
            this.Name = "Fc";
            this.Text = "转换类型";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxSwitch;
        private System.Windows.Forms.Button btnOKOnce;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox ckbNever;
    }
}
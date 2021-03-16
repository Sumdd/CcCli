namespace CenoCC
{
    partial class convert
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(convert));
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxTarget = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConvert = new System.Windows.Forms.Button();
            this.ckbWaitForExit = new System.Windows.Forms.CheckBox();
            this.ckbChildFolder = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxAc = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(122, 12);
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.Size = new System.Drawing.Size(150, 21);
            this.txtSource.TabIndex = 0;
            this.txtSource.DoubleClick += new System.EventHandler(this.txtSource_DoubleClick);
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(122, 39);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.ReadOnly = true;
            this.txtTarget.Size = new System.Drawing.Size(150, 21);
            this.txtTarget.TabIndex = 1;
            this.txtTarget.DoubleClick += new System.EventHandler(this.txtTarget_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "源文件夹";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "目标文件夹";
            // 
            // cbxTarget
            // 
            this.cbxTarget.FormattingEnabled = true;
            this.cbxTarget.Location = new System.Drawing.Point(122, 66);
            this.cbxTarget.Name = "cbxTarget";
            this.cbxTarget.Size = new System.Drawing.Size(150, 20);
            this.cbxTarget.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "转换为";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(197, 226);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 6;
            this.btnConvert.Text = "转换";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // ckbWaitForExit
            // 
            this.ckbWaitForExit.AutoSize = true;
            this.ckbWaitForExit.Location = new System.Drawing.Point(122, 119);
            this.ckbWaitForExit.Name = "ckbWaitForExit";
            this.ckbWaitForExit.Size = new System.Drawing.Size(120, 16);
            this.ckbWaitForExit.TabIndex = 7;
            this.ckbWaitForExit.Text = "等待上个转换结束";
            this.ckbWaitForExit.UseVisualStyleBackColor = true;
            // 
            // ckbChildFolder
            // 
            this.ckbChildFolder.AutoSize = true;
            this.ckbChildFolder.Checked = true;
            this.ckbChildFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbChildFolder.Location = new System.Drawing.Point(122, 141);
            this.ckbChildFolder.Name = "ckbChildFolder";
            this.ckbChildFolder.Size = new System.Drawing.Size(96, 16);
            this.ckbChildFolder.TabIndex = 8;
            this.ckbChildFolder.Text = "转换子文件夹";
            this.ckbChildFolder.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(14, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(258, 52);
            this.label4.TabIndex = 9;
            this.label4.Text = "1.文件夹路径中不可包含中文字符\r\n2.转换为增加完整命令,可以默认选择,也可手动输入,当判断为完整命令时,也会忽略声道设定";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "声道";
            // 
            // cbxAc
            // 
            this.cbxAc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAc.FormattingEnabled = true;
            this.cbxAc.Items.AddRange(new object[] {
            "",
            "1",
            "2"});
            this.cbxAc.Location = new System.Drawing.Point(122, 93);
            this.cbxAc.Name = "cbxAc";
            this.cbxAc.Size = new System.Drawing.Size(150, 20);
            this.cbxAc.TabIndex = 11;
            // 
            // convert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.cbxAc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ckbChildFolder);
            this.Controls.Add(this.ckbWaitForExit);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxTarget);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTarget);
            this.Controls.Add(this.txtSource);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "convert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "格式转换器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxTarget;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.CheckBox ckbWaitForExit;
        private System.Windows.Forms.CheckBox ckbChildFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxAc;
    }
}


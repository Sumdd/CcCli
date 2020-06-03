namespace CenoCC {
    partial class Args_DialSet {
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
            this.layout = new System.Windows.Forms.Panel();
            this.gb = new System.Windows.Forms.GroupBox();
            this.cbxIsUseCopyNumber = new System.Windows.Forms.CheckBox();
            this.cbxIsUseSpRandomTimeout = new System.Windows.Forms.CheckBox();
            this.cbxIsUseSpRandom = new System.Windows.Forms.CheckBox();
            this.cbxIsUseShare = new System.Windows.Forms.CheckBox();
            this.nupWait = new System.Windows.Forms.NumericUpDown();
            this.cbxIsUseCopy = new System.Windows.Forms.CheckBox();
            this.ckbAutoAddDialNumFlag = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ckbQNRegexNumber = new System.Windows.Forms.CheckBox();
            this.layout.SuspendLayout();
            this.gb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupWait)).BeginInit();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.gb);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(420, 400);
            this.layout.TabIndex = 2;
            // 
            // gb
            // 
            this.gb.Controls.Add(this.ckbQNRegexNumber);
            this.gb.Controls.Add(this.cbxIsUseCopyNumber);
            this.gb.Controls.Add(this.cbxIsUseSpRandomTimeout);
            this.gb.Controls.Add(this.cbxIsUseSpRandom);
            this.gb.Controls.Add(this.cbxIsUseShare);
            this.gb.Controls.Add(this.nupWait);
            this.gb.Controls.Add(this.cbxIsUseCopy);
            this.gb.Controls.Add(this.ckbAutoAddDialNumFlag);
            this.gb.Controls.Add(this.label1);
            this.gb.Location = new System.Drawing.Point(10, 10);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(400, 220);
            this.gb.TabIndex = 2;
            this.gb.TabStop = false;
            this.gb.Text = "拨号设置";
            // 
            // cbxIsUseCopyNumber
            // 
            this.cbxIsUseCopyNumber.AutoSize = true;
            this.cbxIsUseCopyNumber.Location = new System.Drawing.Point(74, 68);
            this.cbxIsUseCopyNumber.Name = "cbxIsUseCopyNumber";
            this.cbxIsUseCopyNumber.Size = new System.Drawing.Size(144, 16);
            this.cbxIsUseCopyNumber.TabIndex = 12;
            this.cbxIsUseCopyNumber.Text = "加载号码至复制弹出框";
            this.cbxIsUseCopyNumber.UseVisualStyleBackColor = true;
            this.cbxIsUseCopyNumber.CheckedChanged += new System.EventHandler(this.cbxIsUseCopyNumber_CheckedChanged);
            // 
            // cbxIsUseSpRandomTimeout
            // 
            this.cbxIsUseSpRandomTimeout.AutoSize = true;
            this.cbxIsUseSpRandomTimeout.Checked = true;
            this.cbxIsUseSpRandomTimeout.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxIsUseSpRandomTimeout.Location = new System.Drawing.Point(74, 139);
            this.cbxIsUseSpRandomTimeout.Name = "cbxIsUseSpRandomTimeout";
            this.cbxIsUseSpRandomTimeout.Size = new System.Drawing.Size(294, 16);
            this.cbxIsUseSpRandomTimeout.TabIndex = 11;
            this.cbxIsUseSpRandomTimeout.Text = "是否启用超时自动专线轮呼(需启用共享,设置超时)";
            this.cbxIsUseSpRandomTimeout.UseVisualStyleBackColor = true;
            this.cbxIsUseSpRandomTimeout.CheckedChanged += new System.EventHandler(this.cbxIsUseSpRandomTimeout_CheckedChanged);
            // 
            // cbxIsUseSpRandom
            // 
            this.cbxIsUseSpRandom.AutoSize = true;
            this.cbxIsUseSpRandom.Checked = true;
            this.cbxIsUseSpRandom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxIsUseSpRandom.Location = new System.Drawing.Point(74, 161);
            this.cbxIsUseSpRandom.Name = "cbxIsUseSpRandom";
            this.cbxIsUseSpRandom.Size = new System.Drawing.Size(252, 16);
            this.cbxIsUseSpRandom.TabIndex = 10;
            this.cbxIsUseSpRandom.Text = "是否启用专线轮呼(当不使用共享号码生效)";
            this.cbxIsUseSpRandom.UseVisualStyleBackColor = true;
            this.cbxIsUseSpRandom.CheckedChanged += new System.EventHandler(this.cbxIsUseSpRandom_CheckedChanged);
            // 
            // cbxIsUseShare
            // 
            this.cbxIsUseShare.AutoSize = true;
            this.cbxIsUseShare.Checked = true;
            this.cbxIsUseShare.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxIsUseShare.Location = new System.Drawing.Point(74, 90);
            this.cbxIsUseShare.Name = "cbxIsUseShare";
            this.cbxIsUseShare.Size = new System.Drawing.Size(252, 16);
            this.cbxIsUseShare.TabIndex = 9;
            this.cbxIsUseShare.Text = "使用共享号码(默认等待时间,0为关闭等待)";
            this.cbxIsUseShare.UseVisualStyleBackColor = true;
            this.cbxIsUseShare.CheckedChanged += new System.EventHandler(this.cbxIsUseShare_CheckedChanged);
            // 
            // nupWait
            // 
            this.nupWait.Location = new System.Drawing.Point(74, 112);
            this.nupWait.Name = "nupWait";
            this.nupWait.Size = new System.Drawing.Size(120, 21);
            this.nupWait.TabIndex = 8;
            this.nupWait.Leave += new System.EventHandler(this.nupWait_Leave);
            // 
            // cbxIsUseCopy
            // 
            this.cbxIsUseCopy.AutoSize = true;
            this.cbxIsUseCopy.Checked = true;
            this.cbxIsUseCopy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxIsUseCopy.Location = new System.Drawing.Point(74, 46);
            this.cbxIsUseCopy.Name = "cbxIsUseCopy";
            this.cbxIsUseCopy.Size = new System.Drawing.Size(96, 16);
            this.cbxIsUseCopy.TabIndex = 7;
            this.cbxIsUseCopy.Text = "开启复制拨号";
            this.cbxIsUseCopy.UseVisualStyleBackColor = true;
            this.cbxIsUseCopy.CheckedChanged += new System.EventHandler(this.cbxIsUseCopy_CheckedChanged);
            // 
            // ckbAutoAddDialNumFlag
            // 
            this.ckbAutoAddDialNumFlag.AutoSize = true;
            this.ckbAutoAddDialNumFlag.Checked = true;
            this.ckbAutoAddDialNumFlag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAutoAddDialNumFlag.Location = new System.Drawing.Point(74, 24);
            this.ckbAutoAddDialNumFlag.Name = "ckbAutoAddDialNumFlag";
            this.ckbAutoAddDialNumFlag.Size = new System.Drawing.Size(144, 16);
            this.ckbAutoAddDialNumFlag.TabIndex = 6;
            this.ckbAutoAddDialNumFlag.Text = "自动根据区号加拨前缀";
            this.ckbAutoAddDialNumFlag.UseVisualStyleBackColor = true;
            this.ckbAutoAddDialNumFlag.CheckedChanged += new System.EventHandler(this.ckbAutoAddDialNumFlag_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 1;
            // 
            // ckbQNRegexNumber
            // 
            this.ckbQNRegexNumber.AutoSize = true;
            this.ckbQNRegexNumber.Location = new System.Drawing.Point(74, 183);
            this.ckbQNRegexNumber.Name = "ckbQNRegexNumber";
            this.ckbQNRegexNumber.Size = new System.Drawing.Size(120, 16);
            this.ckbQNRegexNumber.TabIndex = 13;
            this.ckbQNRegexNumber.Text = "兼容32位加密号码";
            this.ckbQNRegexNumber.UseVisualStyleBackColor = true;
            this.ckbQNRegexNumber.CheckedChanged += new System.EventHandler(this.ckbQNRegexNumber_CheckedChanged);
            // 
            // Args_DialSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 400);
            this.Controls.Add(this.layout);
            this.Name = "Args_DialSet";
            this.Text = "录音";
            this.layout.ResumeLayout(false);
            this.gb.ResumeLayout(false);
            this.gb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupWait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.GroupBox gb;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckBox ckbAutoAddDialNumFlag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbxIsUseCopy;
        private System.Windows.Forms.NumericUpDown nupWait;
        private System.Windows.Forms.CheckBox cbxIsUseShare;
        private System.Windows.Forms.CheckBox cbxIsUseSpRandom;
        private System.Windows.Forms.CheckBox cbxIsUseSpRandomTimeout;
        private System.Windows.Forms.CheckBox cbxIsUseCopyNumber;
        private System.Windows.Forms.CheckBox ckbQNRegexNumber;
    }
}
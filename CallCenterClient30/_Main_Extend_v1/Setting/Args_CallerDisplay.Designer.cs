namespace CenoCC {
    partial class Args_CallerDisplay {
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
            this.btnYes = new System.Windows.Forms.Button();
            this.gb = new System.Windows.Forms.GroupBox();
            this.ckbShowAddress = new System.Windows.Forms.CheckBox();
            this.ckbShowRealName = new System.Windows.Forms.CheckBox();
            this.ckbShowNumber = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.cbxSysMsgCall = new System.Windows.Forms.CheckBox();
            this.layout.SuspendLayout();
            this.gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.btnYes);
            this.layout.Controls.Add(this.gb);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(420, 400);
            this.layout.TabIndex = 2;
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(333, 365);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 4;
            this.btnYes.Text = "确定";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // gb
            // 
            this.gb.Controls.Add(this.cbxSysMsgCall);
            this.gb.Controls.Add(this.ckbShowAddress);
            this.gb.Controls.Add(this.ckbShowRealName);
            this.gb.Controls.Add(this.ckbShowNumber);
            this.gb.Controls.Add(this.label1);
            this.gb.Location = new System.Drawing.Point(10, 10);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(400, 122);
            this.gb.TabIndex = 2;
            this.gb.TabStop = false;
            this.gb.Text = "来电显示";
            // 
            // ckbShowAddress
            // 
            this.ckbShowAddress.AutoSize = true;
            this.ckbShowAddress.Checked = true;
            this.ckbShowAddress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbShowAddress.Location = new System.Drawing.Point(74, 68);
            this.ckbShowAddress.Name = "ckbShowAddress";
            this.ckbShowAddress.Size = new System.Drawing.Size(108, 16);
            this.ckbShowAddress.TabIndex = 8;
            this.ckbShowAddress.Text = "显示号码归属地";
            this.ckbShowAddress.UseVisualStyleBackColor = true;
            // 
            // ckbShowRealName
            // 
            this.ckbShowRealName.AutoSize = true;
            this.ckbShowRealName.Checked = true;
            this.ckbShowRealName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbShowRealName.Location = new System.Drawing.Point(74, 46);
            this.ckbShowRealName.Name = "ckbShowRealName";
            this.ckbShowRealName.Size = new System.Drawing.Size(108, 16);
            this.ckbShowRealName.TabIndex = 7;
            this.ckbShowRealName.Text = "显示联系人姓名";
            this.ckbShowRealName.UseVisualStyleBackColor = true;
            // 
            // ckbShowNumber
            // 
            this.ckbShowNumber.AutoSize = true;
            this.ckbShowNumber.Checked = true;
            this.ckbShowNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbShowNumber.Location = new System.Drawing.Point(74, 24);
            this.ckbShowNumber.Name = "ckbShowNumber";
            this.ckbShowNumber.Size = new System.Drawing.Size(96, 16);
            this.ckbShowNumber.TabIndex = 6;
            this.ckbShowNumber.Text = "显示电话号码";
            this.ckbShowNumber.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 1;
            // 
            // cbxSysMsgCall
            // 
            this.cbxSysMsgCall.AutoSize = true;
            this.cbxSysMsgCall.Checked = true;
            this.cbxSysMsgCall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxSysMsgCall.Location = new System.Drawing.Point(74, 90);
            this.cbxSysMsgCall.Name = "cbxSysMsgCall";
            this.cbxSysMsgCall.Size = new System.Drawing.Size(96, 16);
            this.cbxSysMsgCall.TabIndex = 9;
            this.cbxSysMsgCall.Text = "系统通知来电";
            this.cbxSysMsgCall.UseVisualStyleBackColor = true;
            this.cbxSysMsgCall.CheckedChanged += new System.EventHandler(this.cbxSysMsgCall_CheckedChanged);
            // 
            // Args_CallerDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 400);
            this.Controls.Add(this.layout);
            this.Name = "Args_CallerDisplay";
            this.Text = "录音";
            this.layout.ResumeLayout(false);
            this.gb.ResumeLayout(false);
            this.gb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.GroupBox gb;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckBox ckbShowNumber;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckbShowAddress;
        private System.Windows.Forms.CheckBox ckbShowRealName;
        private System.Windows.Forms.CheckBox cbxSysMsgCall;
    }
}
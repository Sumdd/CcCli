namespace CenoCC
{
    partial class Args_CallerDisplay
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
            this.layout = new System.Windows.Forms.Panel();
            this.btnYes = new System.Windows.Forms.Button();
            this.gb = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInlimit_2number = new System.Windows.Forms.TextBox();
            this.cbxIsinlimit_2 = new System.Windows.Forms.CheckBox();
            this.cbxSysMsgCall = new System.Windows.Forms.CheckBox();
            this.ckbShowAddress = new System.Windows.Forms.CheckBox();
            this.ckbShowRealName = new System.Windows.Forms.CheckBox();
            this.ckbShowNumber = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.inlimit_2whatday = new System.Windows.Forms.CheckedListBox();
            this.inlimit_2endtime = new System.Windows.Forms.DateTimePicker();
            this.inlimit_2starttime = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
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
            this.layout.TabIndex = 0;
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(333, 365);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 15;
            this.btnYes.Text = "确定";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // gb
            // 
            this.gb.Controls.Add(this.inlimit_2whatday);
            this.gb.Controls.Add(this.inlimit_2endtime);
            this.gb.Controls.Add(this.inlimit_2starttime);
            this.gb.Controls.Add(this.label8);
            this.gb.Controls.Add(this.label9);
            this.gb.Controls.Add(this.label10);
            this.gb.Controls.Add(this.label2);
            this.gb.Controls.Add(this.txtInlimit_2number);
            this.gb.Controls.Add(this.cbxIsinlimit_2);
            this.gb.Controls.Add(this.cbxSysMsgCall);
            this.gb.Controls.Add(this.ckbShowAddress);
            this.gb.Controls.Add(this.ckbShowRealName);
            this.gb.Controls.Add(this.ckbShowNumber);
            this.gb.Controls.Add(this.label1);
            this.gb.Location = new System.Drawing.Point(10, 10);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(400, 349);
            this.gb.TabIndex = 1;
            this.gb.TabStop = false;
            this.gb.Text = "来电";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(7, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(387, 28);
            this.label2.TabIndex = 8;
            this.label2.Text = "该来电转移设定的优先级小于拨号限制中的呼叫内转号码，且系统管理员需指定好呼叫转移的网关以及配置好网关上的呼叫转移端口组及路由";
            // 
            // txtInlimit_2number
            // 
            this.txtInlimit_2number.Location = new System.Drawing.Point(176, 110);
            this.txtInlimit_2number.Name = "txtInlimit_2number";
            this.txtInlimit_2number.Size = new System.Drawing.Size(180, 21);
            this.txtInlimit_2number.TabIndex = 7;
            // 
            // cbxIsinlimit_2
            // 
            this.cbxIsinlimit_2.AutoSize = true;
            this.cbxIsinlimit_2.Checked = true;
            this.cbxIsinlimit_2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxIsinlimit_2.Location = new System.Drawing.Point(74, 112);
            this.cbxIsinlimit_2.Name = "cbxIsinlimit_2";
            this.cbxIsinlimit_2.Size = new System.Drawing.Size(96, 16);
            this.cbxIsinlimit_2.TabIndex = 6;
            this.cbxIsinlimit_2.Text = "开启来电转移";
            this.cbxIsinlimit_2.UseVisualStyleBackColor = true;
            // 
            // cbxSysMsgCall
            // 
            this.cbxSysMsgCall.AutoSize = true;
            this.cbxSysMsgCall.Checked = true;
            this.cbxSysMsgCall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxSysMsgCall.Location = new System.Drawing.Point(74, 90);
            this.cbxSysMsgCall.Name = "cbxSysMsgCall";
            this.cbxSysMsgCall.Size = new System.Drawing.Size(96, 16);
            this.cbxSysMsgCall.TabIndex = 5;
            this.cbxSysMsgCall.Text = "系统通知来电";
            this.cbxSysMsgCall.UseVisualStyleBackColor = true;
            // 
            // ckbShowAddress
            // 
            this.ckbShowAddress.AutoSize = true;
            this.ckbShowAddress.Checked = true;
            this.ckbShowAddress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbShowAddress.Location = new System.Drawing.Point(74, 68);
            this.ckbShowAddress.Name = "ckbShowAddress";
            this.ckbShowAddress.Size = new System.Drawing.Size(108, 16);
            this.ckbShowAddress.TabIndex = 4;
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
            this.ckbShowRealName.TabIndex = 3;
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
            this.ckbShowNumber.TabIndex = 2;
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
            // inlimit_2whatday
            // 
            this.inlimit_2whatday.FormattingEnabled = true;
            this.inlimit_2whatday.Items.AddRange(new object[] {
            "星期一",
            "星期二",
            "星期三",
            "星期四",
            "星期五",
            "星期六",
            "星期天"});
            this.inlimit_2whatday.Location = new System.Drawing.Point(176, 221);
            this.inlimit_2whatday.Name = "inlimit_2whatday";
            this.inlimit_2whatday.Size = new System.Drawing.Size(180, 116);
            this.inlimit_2whatday.TabIndex = 14;
            // 
            // inlimit_2endtime
            // 
            this.inlimit_2endtime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.inlimit_2endtime.Location = new System.Drawing.Point(176, 194);
            this.inlimit_2endtime.Name = "inlimit_2endtime";
            this.inlimit_2endtime.ShowUpDown = true;
            this.inlimit_2endtime.Size = new System.Drawing.Size(180, 21);
            this.inlimit_2endtime.TabIndex = 12;
            this.inlimit_2endtime.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // inlimit_2starttime
            // 
            this.inlimit_2starttime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.inlimit_2starttime.Location = new System.Drawing.Point(176, 165);
            this.inlimit_2starttime.Name = "inlimit_2starttime";
            this.inlimit_2starttime.ShowUpDown = true;
            this.inlimit_2starttime.Size = new System.Drawing.Size(180, 21);
            this.inlimit_2starttime.TabIndex = 10;
            this.inlimit_2starttime.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(69, 200);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "呼叫内转结束时间";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(93, 325);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "呼叫内转星期";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(69, 171);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "呼叫内转开始时间";
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
        private System.Windows.Forms.CheckBox cbxIsinlimit_2;
        private System.Windows.Forms.TextBox txtInlimit_2number;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox inlimit_2whatday;
        private System.Windows.Forms.DateTimePicker inlimit_2endtime;
        private System.Windows.Forms.DateTimePicker inlimit_2starttime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}
namespace CenoCC
{
    partial class inlimit_2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(inlimit_2));
            this.inlimit_2idstatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.inlimit_2number = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.inlimit_2starttime = new System.Windows.Forms.DateTimePicker();
            this.inlimit_2endtime = new System.Windows.Forms.DateTimePicker();
            this.inlimit_2trycount = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.inlimit_2whatday = new System.Windows.Forms.CheckedListBox();
            this.inlimit_2way = new System.Windows.Forms.CheckedListBox();
            this.inlimit_2id = new System.Windows.Forms.Label();
            this.inlimit_2use = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // inlimit_2idstatus
            // 
            this.inlimit_2idstatus.AutoSize = true;
            this.inlimit_2idstatus.Location = new System.Drawing.Point(12, 15);
            this.inlimit_2idstatus.Name = "inlimit_2idstatus";
            this.inlimit_2idstatus.Size = new System.Drawing.Size(101, 12);
            this.inlimit_2idstatus.TabIndex = 1;
            this.inlimit_2idstatus.Text = "呼叫内转配置ID：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "呼叫内转开始时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "呼叫内转模式";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "呼叫内转星期";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(197, 415);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "添加";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "呼叫内转结束时间";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "呼叫内转号码";
            // 
            // inlimit_2number
            // 
            this.inlimit_2number.Location = new System.Drawing.Point(152, 93);
            this.inlimit_2number.Name = "inlimit_2number";
            this.inlimit_2number.Size = new System.Drawing.Size(120, 21);
            this.inlimit_2number.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(12, 333);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(260, 79);
            this.label8.TabIndex = 16;
            this.label8.Text = "1.呼叫内转结束时间如果小于开始时间则计算到次日，如果大于开始时间则计算到当日。\r\n2.呼叫内转模式如果都勾选会先接坐席，坐席无人接听后尝试设定次数的呼叫内转。\r" +
    "\n3.启用禁用修正为随线路设置的启用禁用状态";
            // 
            // inlimit_2starttime
            // 
            this.inlimit_2starttime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.inlimit_2starttime.Location = new System.Drawing.Point(152, 39);
            this.inlimit_2starttime.Name = "inlimit_2starttime";
            this.inlimit_2starttime.ShowUpDown = true;
            this.inlimit_2starttime.Size = new System.Drawing.Size(120, 21);
            this.inlimit_2starttime.TabIndex = 17;
            this.inlimit_2starttime.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // inlimit_2endtime
            // 
            this.inlimit_2endtime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.inlimit_2endtime.Location = new System.Drawing.Point(152, 66);
            this.inlimit_2endtime.Name = "inlimit_2endtime";
            this.inlimit_2endtime.ShowUpDown = true;
            this.inlimit_2endtime.Size = new System.Drawing.Size(120, 21);
            this.inlimit_2endtime.TabIndex = 18;
            this.inlimit_2endtime.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // inlimit_2trycount
            // 
            this.inlimit_2trycount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inlimit_2trycount.FormattingEnabled = true;
            this.inlimit_2trycount.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.inlimit_2trycount.Location = new System.Drawing.Point(152, 284);
            this.inlimit_2trycount.Name = "inlimit_2trycount";
            this.inlimit_2trycount.Size = new System.Drawing.Size(120, 20);
            this.inlimit_2trycount.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 287);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "呼叫内转尝试次数";
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
            this.inlimit_2whatday.Location = new System.Drawing.Point(152, 120);
            this.inlimit_2whatday.Name = "inlimit_2whatday";
            this.inlimit_2whatday.Size = new System.Drawing.Size(120, 116);
            this.inlimit_2whatday.TabIndex = 21;
            // 
            // inlimit_2way
            // 
            this.inlimit_2way.FormattingEnabled = true;
            this.inlimit_2way.Items.AddRange(new object[] {
            "坐席",
            "内转"});
            this.inlimit_2way.Location = new System.Drawing.Point(152, 242);
            this.inlimit_2way.Name = "inlimit_2way";
            this.inlimit_2way.Size = new System.Drawing.Size(120, 36);
            this.inlimit_2way.TabIndex = 22;
            // 
            // inlimit_2id
            // 
            this.inlimit_2id.AutoSize = true;
            this.inlimit_2id.Location = new System.Drawing.Point(150, 15);
            this.inlimit_2id.Name = "inlimit_2id";
            this.inlimit_2id.Size = new System.Drawing.Size(17, 12);
            this.inlimit_2id.TabIndex = 23;
            this.inlimit_2id.Text = "-1";
            // 
            // inlimit_2use
            // 
            this.inlimit_2use.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inlimit_2use.FormattingEnabled = true;
            this.inlimit_2use.Items.AddRange(new object[] {
            "是",
            "否"});
            this.inlimit_2use.Location = new System.Drawing.Point(152, 310);
            this.inlimit_2use.Name = "inlimit_2use";
            this.inlimit_2use.Size = new System.Drawing.Size(120, 20);
            this.inlimit_2use.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(12, 313);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "启用禁用";
            // 
            // inlimit_2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 450);
            this.Controls.Add(this.inlimit_2use);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.inlimit_2id);
            this.Controls.Add(this.inlimit_2way);
            this.Controls.Add(this.inlimit_2whatday);
            this.Controls.Add(this.inlimit_2trycount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.inlimit_2endtime);
            this.Controls.Add(this.inlimit_2starttime);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.inlimit_2number);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inlimit_2idstatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "inlimit_2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "呼叫内转配置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label inlimit_2idstatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox inlimit_2number;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker inlimit_2starttime;
        private System.Windows.Forms.DateTimePicker inlimit_2endtime;
        private System.Windows.Forms.ComboBox inlimit_2trycount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox inlimit_2whatday;
        private System.Windows.Forms.CheckedListBox inlimit_2way;
        private System.Windows.Forms.Label inlimit_2id;
        private System.Windows.Forms.ComboBox inlimit_2use;
        private System.Windows.Forms.Label label7;
    }
}
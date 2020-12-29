namespace CenoCC {
    partial class parameter {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(parameter));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.limitcountValue = new System.Windows.Forms.NumericUpDown();
            this.limitdurationValue = new System.Windows.Forms.NumericUpDown();
            this.limitthecountValue = new System.Windows.Forms.NumericUpDown();
            this.limitthedurationValue = new System.Windows.Forms.NumericUpDown();
            this.btnUsing = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.limitthedialValue = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.btnUsingSelect = new System.Windows.Forms.Button();
            this.inlimit_2whatday = new System.Windows.Forms.CheckedListBox();
            this.inlimit_2endtime = new System.Windows.Forms.DateTimePicker();
            this.inlimit_2starttime = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.limitcountValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.limitdurationValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.limitthecountValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.limitthedurationValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.limitthedialValue)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "总次数限制";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "总时长限制(秒)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "当日次数限制";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "当日时长限制(秒)";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(232, 400);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(40, 23);
            this.btnOK.TabIndex = 104;
            this.btnOK.Tag = "diallimit_limit_ok";
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(186, 400);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(40, 23);
            this.btnReset.TabIndex = 103;
            this.btnReset.Tag = "diallimit_limit_reset";
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // limitcountValue
            // 
            this.limitcountValue.Location = new System.Drawing.Point(152, 12);
            this.limitcountValue.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.limitcountValue.Name = "limitcountValue";
            this.limitcountValue.Size = new System.Drawing.Size(120, 21);
            this.limitcountValue.TabIndex = 1;
            // 
            // limitdurationValue
            // 
            this.limitdurationValue.Location = new System.Drawing.Point(152, 39);
            this.limitdurationValue.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.limitdurationValue.Name = "limitdurationValue";
            this.limitdurationValue.Size = new System.Drawing.Size(120, 21);
            this.limitdurationValue.TabIndex = 3;
            // 
            // limitthecountValue
            // 
            this.limitthecountValue.Location = new System.Drawing.Point(152, 66);
            this.limitthecountValue.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.limitthecountValue.Name = "limitthecountValue";
            this.limitthecountValue.Size = new System.Drawing.Size(120, 21);
            this.limitthecountValue.TabIndex = 5;
            // 
            // limitthedurationValue
            // 
            this.limitthedurationValue.Location = new System.Drawing.Point(152, 93);
            this.limitthedurationValue.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.limitthedurationValue.Name = "limitthedurationValue";
            this.limitthedurationValue.Size = new System.Drawing.Size(120, 21);
            this.limitthedurationValue.TabIndex = 7;
            this.limitthedurationValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btnUsing
            // 
            this.btnUsing.Location = new System.Drawing.Point(24, 400);
            this.btnUsing.Name = "btnUsing";
            this.btnUsing.Size = new System.Drawing.Size(75, 23);
            this.btnUsing.TabIndex = 101;
            this.btnUsing.Tag = "diallimit_limit_all";
            this.btnUsing.Text = "全部生效";
            this.btnUsing.UseVisualStyleBackColor = true;
            this.btnUsing.Click += new System.EventHandler(this.btnUsing_Click);
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(12, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(260, 19);
            this.label5.TabIndex = 10;
            this.label5.Text = "说明：设置为零表示无限制,可操作多条";
            // 
            // limitthedialValue
            // 
            this.limitthedialValue.Location = new System.Drawing.Point(152, 120);
            this.limitthedialValue.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.limitthedialValue.Name = "limitthedialValue";
            this.limitthedialValue.Size = new System.Drawing.Size(120, 21);
            this.limitthedialValue.TabIndex = 9;
            this.limitthedialValue.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "同号码限呼(次)";
            // 
            // btnUsingSelect
            // 
            this.btnUsingSelect.Location = new System.Drawing.Point(105, 400);
            this.btnUsingSelect.Name = "btnUsingSelect";
            this.btnUsingSelect.Size = new System.Drawing.Size(75, 23);
            this.btnUsingSelect.TabIndex = 102;
            this.btnUsingSelect.Tag = "diallimit_limit_select";
            this.btnUsingSelect.Text = "选中生效";
            this.btnUsingSelect.UseVisualStyleBackColor = true;
            this.btnUsingSelect.Click += new System.EventHandler(this.btnUsingSelect_Click);
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
            this.inlimit_2whatday.Location = new System.Drawing.Point(152, 241);
            this.inlimit_2whatday.Name = "inlimit_2whatday";
            this.inlimit_2whatday.Size = new System.Drawing.Size(120, 116);
            this.inlimit_2whatday.TabIndex = 16;
            // 
            // inlimit_2endtime
            // 
            this.inlimit_2endtime.Enabled = false;
            this.inlimit_2endtime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.inlimit_2endtime.Location = new System.Drawing.Point(152, 214);
            this.inlimit_2endtime.Name = "inlimit_2endtime";
            this.inlimit_2endtime.ShowUpDown = true;
            this.inlimit_2endtime.Size = new System.Drawing.Size(120, 21);
            this.inlimit_2endtime.TabIndex = 14;
            this.inlimit_2endtime.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // inlimit_2starttime
            // 
            this.inlimit_2starttime.Enabled = false;
            this.inlimit_2starttime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.inlimit_2starttime.Location = new System.Drawing.Point(152, 187);
            this.inlimit_2starttime.Name = "inlimit_2starttime";
            this.inlimit_2starttime.ShowUpDown = true;
            this.inlimit_2starttime.Size = new System.Drawing.Size(120, 21);
            this.inlimit_2starttime.TabIndex = 12;
            this.inlimit_2starttime.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(12, 217);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "呼叫内转结束时间";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 345);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "呼叫内转星期";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(12, 190);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 12);
            this.label10.TabIndex = 11;
            this.label10.Text = "呼叫内转开始时间";
            // 
            // parameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 435);
            this.Controls.Add(this.inlimit_2whatday);
            this.Controls.Add(this.inlimit_2endtime);
            this.Controls.Add(this.inlimit_2starttime);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnUsingSelect);
            this.Controls.Add(this.limitthedialValue);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnUsing);
            this.Controls.Add(this.limitthedurationValue);
            this.Controls.Add(this.limitthecountValue);
            this.Controls.Add(this.limitdurationValue);
            this.Controls.Add(this.limitcountValue);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "parameter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "呼叫限制配置";
            ((System.ComponentModel.ISupportInitialize)(this.limitcountValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.limitdurationValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.limitthecountValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.limitthedurationValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.limitthedialValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.NumericUpDown limitcountValue;
        private System.Windows.Forms.NumericUpDown limitdurationValue;
        private System.Windows.Forms.NumericUpDown limitthecountValue;
        private System.Windows.Forms.NumericUpDown limitthedurationValue;
        private System.Windows.Forms.Button btnUsing;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown limitthedialValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnUsingSelect;
        private System.Windows.Forms.CheckedListBox inlimit_2whatday;
        private System.Windows.Forms.DateTimePicker inlimit_2endtime;
        private System.Windows.Forms.DateTimePicker inlimit_2starttime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}
namespace CenoCC {
    partial class _ucPager {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.limitValue = new System.Windows.Forms.ComboBox();
            this.redirectValue = new System.Windows.Forms.TextBox();
            this.mainTip = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pageUp = new System.Windows.Forms.Button();
            this.pageDown = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // limitValue
            // 
            this.limitValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.limitValue.FormattingEnabled = true;
            this.limitValue.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100",
            "200",
            "300",
            "400",
            "500"});
            this.limitValue.Location = new System.Drawing.Point(392, 0);
            this.limitValue.Name = "limitValue";
            this.limitValue.Size = new System.Drawing.Size(50, 20);
            this.limitValue.TabIndex = 0;
            this.limitValue.SelectedIndexChanged += new System.EventHandler(this.limitValue_SelectedIndexChanged);
            // 
            // redirectValue
            // 
            this.redirectValue.Location = new System.Drawing.Point(678, 0);
            this.redirectValue.Multiline = true;
            this.redirectValue.Name = "redirectValue";
            this.redirectValue.Size = new System.Drawing.Size(60, 20);
            this.redirectValue.TabIndex = 2;
            this.redirectValue.Text = "1";
            this.redirectValue.TextChanged += new System.EventHandler(this.redirectValue_TextChanged);
            this.redirectValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.redirectValue_KeyPress);
            // 
            // mainTip
            // 
            this.mainTip.BackColor = System.Drawing.Color.Transparent;
            this.mainTip.Location = new System.Drawing.Point(0, 0);
            this.mainTip.Name = "mainTip";
            this.mainTip.Size = new System.Drawing.Size(350, 20);
            this.mainTip.TabIndex = 3;
            this.mainTip.Text = "页码0/0,显示0到0条,共0条";
            this.mainTip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(448, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "条";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pageUp
            // 
            this.pageUp.Location = new System.Drawing.Point(469, 0);
            this.pageUp.Name = "pageUp";
            this.pageUp.Size = new System.Drawing.Size(75, 20);
            this.pageUp.TabIndex = 5;
            this.pageUp.Text = "上一页";
            this.pageUp.UseVisualStyleBackColor = true;
            this.pageUp.Click += new System.EventHandler(this.pageUp_Click);
            // 
            // pageDown
            // 
            this.pageDown.Location = new System.Drawing.Point(550, 0);
            this.pageDown.Name = "pageDown";
            this.pageDown.Size = new System.Drawing.Size(75, 20);
            this.pageDown.TabIndex = 6;
            this.pageDown.Text = "下一页";
            this.pageDown.UseVisualStyleBackColor = true;
            this.pageDown.Click += new System.EventHandler(this.pageDown_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(356, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "每页";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(631, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "跳转至";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _ucPager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pageDown);
            this.Controls.Add(this.pageUp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mainTip);
            this.Controls.Add(this.redirectValue);
            this.Controls.Add(this.limitValue);
            this.Name = "_ucPager";
            this.Size = new System.Drawing.Size(738, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox limitValue;
        private System.Windows.Forms.TextBox redirectValue;
        private System.Windows.Forms.Label mainTip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button pageUp;
        private System.Windows.Forms.Button pageDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

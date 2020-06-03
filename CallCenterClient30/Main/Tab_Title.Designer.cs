namespace CenoCC
{
    partial class Tab_Title
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.Close_Pan = new System.Windows.Forms.Panel();
            this.TabPic_Pan = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 8);
            this.label1.MaximumSize = new System.Drawing.Size(140, 12);
            this.label1.MinimumSize = new System.Drawing.Size(140, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "青岛新生代呼叫中心 V3.0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
            // 
            // Close_Pan
            // 
            this.Close_Pan.BackColor = System.Drawing.Color.Transparent;
            this.Close_Pan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Close_Pan.Location = new System.Drawing.Point(181, 8);
            this.Close_Pan.Name = "Close_Pan";
            this.Close_Pan.Size = new System.Drawing.Size(10, 10);
            this.Close_Pan.TabIndex = 1;
            this.Close_Pan.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // TabPic_Pan
            // 
            this.TabPic_Pan.BackgroundImage = global::CenoCC.Properties.Resources.ie;
            this.TabPic_Pan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.TabPic_Pan.Location = new System.Drawing.Point(13, 4);
            this.TabPic_Pan.Name = "TabPic_Pan";
            this.TabPic_Pan.Size = new System.Drawing.Size(18, 18);
            this.TabPic_Pan.TabIndex = 2;
            // 
            // Tab_Title
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::CenoCC.Properties.Resources.Top_Tab;
            this.Controls.Add(this.TabPic_Pan);
            this.Controls.Add(this.Close_Pan);
            this.Controls.Add(this.label1);
            this.Name = "Tab_Title";
            this.Size = new System.Drawing.Size(206, 25);
            this.Click += new System.EventHandler(this.Tab_Title_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Close_Pan;
		private System.Windows.Forms.Panel TabPic_Pan;
    }
}

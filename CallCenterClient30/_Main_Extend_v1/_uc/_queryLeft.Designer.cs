namespace CenoCC {
    partial class _queryLeft {
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
            this.queryOperator = new System.Windows.Forms.ComboBox();
            this.queryName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // queryOperator
            // 
            this.queryOperator.FormattingEnabled = true;
            this.queryOperator.Items.AddRange(new object[] {
            "=",
            ">",
            ">=",
            "<",
            "<=",
            "Like"});
            this.queryOperator.Location = new System.Drawing.Point(126, 0);
            this.queryOperator.Name = "queryOperator";
            this.queryOperator.Size = new System.Drawing.Size(50, 20);
            this.queryOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.queryOperator.TabIndex = 1;
            // 
            // queryName
            // 
            this.queryName.Location = new System.Drawing.Point(0, 0);
            this.queryName.Name = "queryName";
            this.queryName.Size = new System.Drawing.Size(120, 20);
            this.queryName.TabIndex = 2;
            this.queryName.Text = "查询条件名称";
            this.queryName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _queryLeft
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.queryName);
            this.Controls.Add(this.queryOperator);
            this.Name = "_queryLeft";
            this.Size = new System.Drawing.Size(176, 20);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox queryOperator;
        private System.Windows.Forms.Label queryName;
    }
}

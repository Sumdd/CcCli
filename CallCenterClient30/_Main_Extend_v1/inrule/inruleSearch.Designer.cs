namespace CenoCC
{
    partial class inruleSearch
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
            this.searchpanel = new System.Windows.Forms.Panel();
            this.ordernumKey = new CenoCC._queryLeft();
            this.ordernumValue = new System.Windows.Forms.TextBox();
            this.inrulesuffixKey = new CenoCC._queryLeft();
            this.inrulesuffixValue = new System.Windows.Forms.TextBox();
            this.inrulenameKey = new CenoCC._queryLeft();
            this.inrulenameValue = new System.Windows.Forms.TextBox();
            this.btnCloseAfterSearch = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.inruleipKey = new CenoCC._queryLeft();
            this.inruleipValue = new System.Windows.Forms.TextBox();
            this.layout.SuspendLayout();
            this.searchpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.searchpanel);
            this.layout.Controls.Add(this.btnCloseAfterSearch);
            this.layout.Controls.Add(this.btnSearch);
            this.layout.Controls.Add(this.btnReset);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(20, 60);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(410, 481);
            this.layout.TabIndex = 0;
            // 
            // searchpanel
            // 
            this.searchpanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchpanel.AutoScroll = true;
            this.searchpanel.Controls.Add(this.inruleipKey);
            this.searchpanel.Controls.Add(this.inruleipValue);
            this.searchpanel.Controls.Add(this.ordernumKey);
            this.searchpanel.Controls.Add(this.ordernumValue);
            this.searchpanel.Controls.Add(this.inrulesuffixKey);
            this.searchpanel.Controls.Add(this.inrulesuffixValue);
            this.searchpanel.Controls.Add(this.inrulenameKey);
            this.searchpanel.Controls.Add(this.inrulenameValue);
            this.searchpanel.Location = new System.Drawing.Point(0, 0);
            this.searchpanel.Name = "searchpanel";
            this.searchpanel.Size = new System.Drawing.Size(410, 360);
            this.searchpanel.TabIndex = 14;
            // 
            // ordernumKey
            // 
            this.ordernumKey.BackColor = System.Drawing.Color.Transparent;
            this.ordernumKey.Location = new System.Drawing.Point(10, 91);
            this.ordernumKey.Name = "ordernumKey";
            this.ordernumKey.Size = new System.Drawing.Size(176, 20);
            this.ordernumKey.TabIndex = 10;
            // 
            // ordernumValue
            // 
            this.ordernumValue.Location = new System.Drawing.Point(192, 90);
            this.ordernumValue.Name = "ordernumValue";
            this.ordernumValue.Size = new System.Drawing.Size(180, 21);
            this.ordernumValue.TabIndex = 11;
            // 
            // inrulesuffixKey
            // 
            this.inrulesuffixKey.BackColor = System.Drawing.Color.Transparent;
            this.inrulesuffixKey.Location = new System.Drawing.Point(10, 64);
            this.inrulesuffixKey.Name = "inrulesuffixKey";
            this.inrulesuffixKey.Size = new System.Drawing.Size(176, 20);
            this.inrulesuffixKey.TabIndex = 8;
            // 
            // inrulesuffixValue
            // 
            this.inrulesuffixValue.Location = new System.Drawing.Point(192, 63);
            this.inrulesuffixValue.Name = "inrulesuffixValue";
            this.inrulesuffixValue.Size = new System.Drawing.Size(180, 21);
            this.inrulesuffixValue.TabIndex = 9;
            // 
            // inrulenameKey
            // 
            this.inrulenameKey.BackColor = System.Drawing.Color.Transparent;
            this.inrulenameKey.Location = new System.Drawing.Point(10, 10);
            this.inrulenameKey.Name = "inrulenameKey";
            this.inrulenameKey.Size = new System.Drawing.Size(176, 20);
            this.inrulenameKey.TabIndex = 0;
            this.inrulenameKey.TabStop = false;
            // 
            // inrulenameValue
            // 
            this.inrulenameValue.Location = new System.Drawing.Point(192, 9);
            this.inrulenameValue.Name = "inrulenameValue";
            this.inrulenameValue.Size = new System.Drawing.Size(180, 21);
            this.inrulenameValue.TabIndex = 1;
            // 
            // btnCloseAfterSearch
            // 
            this.btnCloseAfterSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseAfterSearch.Location = new System.Drawing.Point(325, 446);
            this.btnCloseAfterSearch.Name = "btnCloseAfterSearch";
            this.btnCloseAfterSearch.Size = new System.Drawing.Size(75, 23);
            this.btnCloseAfterSearch.TabIndex = 29;
            this.btnCloseAfterSearch.Text = "查询后关闭";
            this.btnCloseAfterSearch.UseVisualStyleBackColor = true;
            this.btnCloseAfterSearch.Click += new System.EventHandler(this.btnCloseAfterSearch_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(10, 446);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 27;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Location = new System.Drawing.Point(91, 446);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 28;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // inruleipKey
            // 
            this.inruleipKey.BackColor = System.Drawing.Color.Transparent;
            this.inruleipKey.Location = new System.Drawing.Point(10, 37);
            this.inruleipKey.Name = "inruleipKey";
            this.inruleipKey.Size = new System.Drawing.Size(176, 20);
            this.inruleipKey.TabIndex = 2;
            // 
            // inruleipValue
            // 
            this.inruleipValue.Location = new System.Drawing.Point(192, 36);
            this.inruleipValue.Name = "inruleipValue";
            this.inruleipValue.Size = new System.Drawing.Size(180, 21);
            this.inruleipValue.TabIndex = 3;
            // 
            // inruleSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 561);
            this.Controls.Add(this.layout);
            this.Name = "inruleSearch";
            this.Text = "内呼规则管理查询条件";
            this.layout.ResumeLayout(false);
            this.searchpanel.ResumeLayout(false);
            this.searchpanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCloseAfterSearch;
        private _queryLeft inrulenameKey;
        private System.Windows.Forms.TextBox inrulenameValue;
        public System.Windows.Forms.Panel searchpanel;
        private _queryLeft ordernumKey;
        private System.Windows.Forms.TextBox ordernumValue;
        private _queryLeft inrulesuffixKey;
        private System.Windows.Forms.TextBox inrulesuffixValue;
        private _queryLeft inruleipKey;
        private System.Windows.Forms.TextBox inruleipValue;
    }
}
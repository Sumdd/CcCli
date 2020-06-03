namespace CenoCC
{
    partial class shareSearch
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
            this.stateValue = new System.Windows.Forms.ComboBox();
            this.nameKey = new CenoCC._queryLeft();
            this.mainValue = new System.Windows.Forms.ComboBox();
            this.ipKey = new CenoCC._queryLeft();
            this.stateKey = new CenoCC._queryLeft();
            this.mainKey = new CenoCC._queryLeft();
            this.btnCloseAfterSearch = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.nameValue = new System.Windows.Forms.TextBox();
            this.ipValue = new System.Windows.Forms.TextBox();
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
            this.searchpanel.Controls.Add(this.ipValue);
            this.searchpanel.Controls.Add(this.nameValue);
            this.searchpanel.Controls.Add(this.stateValue);
            this.searchpanel.Controls.Add(this.nameKey);
            this.searchpanel.Controls.Add(this.mainValue);
            this.searchpanel.Controls.Add(this.ipKey);
            this.searchpanel.Controls.Add(this.stateKey);
            this.searchpanel.Controls.Add(this.mainKey);
            this.searchpanel.Location = new System.Drawing.Point(0, 0);
            this.searchpanel.Name = "searchpanel";
            this.searchpanel.Size = new System.Drawing.Size(410, 360);
            this.searchpanel.TabIndex = 14;
            // 
            // stateValue
            // 
            this.stateValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stateValue.FormattingEnabled = true;
            this.stateValue.Location = new System.Drawing.Point(192, 88);
            this.stateValue.Name = "stateValue";
            this.stateValue.Size = new System.Drawing.Size(180, 20);
            this.stateValue.TabIndex = 5;
            // 
            // nameKey
            // 
            this.nameKey.BackColor = System.Drawing.Color.Transparent;
            this.nameKey.Location = new System.Drawing.Point(10, 10);
            this.nameKey.Name = "nameKey";
            this.nameKey.Size = new System.Drawing.Size(176, 20);
            this.nameKey.TabIndex = 0;
            // 
            // mainValue
            // 
            this.mainValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mainValue.FormattingEnabled = true;
            this.mainValue.Location = new System.Drawing.Point(192, 62);
            this.mainValue.Name = "mainValue";
            this.mainValue.Size = new System.Drawing.Size(180, 20);
            this.mainValue.TabIndex = 4;
            // 
            // ipKey
            // 
            this.ipKey.BackColor = System.Drawing.Color.Transparent;
            this.ipKey.Location = new System.Drawing.Point(10, 36);
            this.ipKey.Name = "ipKey";
            this.ipKey.Size = new System.Drawing.Size(176, 20);
            this.ipKey.TabIndex = 2;
            // 
            // stateKey
            // 
            this.stateKey.BackColor = System.Drawing.Color.Transparent;
            this.stateKey.Location = new System.Drawing.Point(10, 88);
            this.stateKey.Name = "stateKey";
            this.stateKey.Size = new System.Drawing.Size(176, 20);
            this.stateKey.TabIndex = 10;
            // 
            // mainKey
            // 
            this.mainKey.BackColor = System.Drawing.Color.Transparent;
            this.mainKey.Location = new System.Drawing.Point(10, 62);
            this.mainKey.Name = "mainKey";
            this.mainKey.Size = new System.Drawing.Size(176, 20);
            this.mainKey.TabIndex = 8;
            // 
            // btnCloseAfterSearch
            // 
            this.btnCloseAfterSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseAfterSearch.Location = new System.Drawing.Point(325, 446);
            this.btnCloseAfterSearch.Name = "btnCloseAfterSearch";
            this.btnCloseAfterSearch.Size = new System.Drawing.Size(75, 23);
            this.btnCloseAfterSearch.TabIndex = 3;
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
            this.btnSearch.TabIndex = 1;
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
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // nameValue
            // 
            this.nameValue.Location = new System.Drawing.Point(192, 9);
            this.nameValue.Name = "nameValue";
            this.nameValue.Size = new System.Drawing.Size(180, 21);
            this.nameValue.TabIndex = 1;
            // 
            // ipValue
            // 
            this.ipValue.Location = new System.Drawing.Point(192, 35);
            this.ipValue.Name = "ipValue";
            this.ipValue.Size = new System.Drawing.Size(180, 21);
            this.ipValue.TabIndex = 3;
            // 
            // shareSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 561);
            this.Controls.Add(this.layout);
            this.Name = "shareSearch";
            this.Text = "统计表查询条件";
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
        private _queryLeft nameKey;
        private System.Windows.Forms.ComboBox mainValue;
        private _queryLeft ipKey;
        private _queryLeft mainKey;
        private _queryLeft stateKey;
        public System.Windows.Forms.Panel searchpanel;
        private System.Windows.Forms.ComboBox stateValue;
        private System.Windows.Forms.TextBox ipValue;
        private System.Windows.Forms.TextBox nameValue;
    }
}
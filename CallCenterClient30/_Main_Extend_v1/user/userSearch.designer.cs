namespace CenoCC {
    partial class userSearch {
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
            this.searchpanel = new System.Windows.Forms.Panel();
            this.agentNameValue = new System.Windows.Forms.TextBox();
            this.agentNameKey = new CenoCC._queryLeft();
            this.chTypeKey = new CenoCC._queryLeft();
            this.chTypeValue = new System.Windows.Forms.ComboBox();
            this.loginNameValue = new System.Windows.Forms.TextBox();
            this.loginNameKey = new CenoCC._queryLeft();
            this.chNumValue = new System.Windows.Forms.TextBox();
            this.chNumKey = new CenoCC._queryLeft();
            this.roleKey = new CenoCC._queryLeft();
            this.roleValue = new System.Windows.Forms.ComboBox();
            this.btnCloseAfterSearch = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.IPsKey = new CenoCC._queryLeft();
            this.IPsValue = new System.Windows.Forms.ComboBox();
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
            this.searchpanel.Controls.Add(this.IPsKey);
            this.searchpanel.Controls.Add(this.IPsValue);
            this.searchpanel.Controls.Add(this.agentNameValue);
            this.searchpanel.Controls.Add(this.agentNameKey);
            this.searchpanel.Controls.Add(this.chTypeKey);
            this.searchpanel.Controls.Add(this.chTypeValue);
            this.searchpanel.Controls.Add(this.loginNameValue);
            this.searchpanel.Controls.Add(this.loginNameKey);
            this.searchpanel.Controls.Add(this.chNumValue);
            this.searchpanel.Controls.Add(this.chNumKey);
            this.searchpanel.Controls.Add(this.roleKey);
            this.searchpanel.Controls.Add(this.roleValue);
            this.searchpanel.Location = new System.Drawing.Point(0, 0);
            this.searchpanel.Name = "searchpanel";
            this.searchpanel.Size = new System.Drawing.Size(410, 360);
            this.searchpanel.TabIndex = 14;
            // 
            // agentNameValue
            // 
            this.agentNameValue.Location = new System.Drawing.Point(192, 87);
            this.agentNameValue.Name = "agentNameValue";
            this.agentNameValue.Size = new System.Drawing.Size(180, 21);
            this.agentNameValue.TabIndex = 9;
            // 
            // agentNameKey
            // 
            this.agentNameKey.BackColor = System.Drawing.Color.Transparent;
            this.agentNameKey.Location = new System.Drawing.Point(10, 88);
            this.agentNameKey.Name = "agentNameKey";
            this.agentNameKey.Size = new System.Drawing.Size(176, 20);
            this.agentNameKey.TabIndex = 8;
            // 
            // chTypeKey
            // 
            this.chTypeKey.BackColor = System.Drawing.Color.Transparent;
            this.chTypeKey.Location = new System.Drawing.Point(10, 36);
            this.chTypeKey.Name = "chTypeKey";
            this.chTypeKey.Size = new System.Drawing.Size(176, 20);
            this.chTypeKey.TabIndex = 6;
            // 
            // chTypeValue
            // 
            this.chTypeValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chTypeValue.FormattingEnabled = true;
            this.chTypeValue.Location = new System.Drawing.Point(192, 36);
            this.chTypeValue.Name = "chTypeValue";
            this.chTypeValue.Size = new System.Drawing.Size(180, 20);
            this.chTypeValue.TabIndex = 7;
            // 
            // loginNameValue
            // 
            this.loginNameValue.Location = new System.Drawing.Point(192, 113);
            this.loginNameValue.Name = "loginNameValue";
            this.loginNameValue.Size = new System.Drawing.Size(180, 21);
            this.loginNameValue.TabIndex = 5;
            // 
            // loginNameKey
            // 
            this.loginNameKey.BackColor = System.Drawing.Color.Transparent;
            this.loginNameKey.Location = new System.Drawing.Point(10, 114);
            this.loginNameKey.Name = "loginNameKey";
            this.loginNameKey.Size = new System.Drawing.Size(176, 20);
            this.loginNameKey.TabIndex = 4;
            // 
            // chNumValue
            // 
            this.chNumValue.Location = new System.Drawing.Point(192, 61);
            this.chNumValue.Name = "chNumValue";
            this.chNumValue.Size = new System.Drawing.Size(180, 21);
            this.chNumValue.TabIndex = 3;
            // 
            // chNumKey
            // 
            this.chNumKey.BackColor = System.Drawing.Color.Transparent;
            this.chNumKey.Location = new System.Drawing.Point(10, 62);
            this.chNumKey.Name = "chNumKey";
            this.chNumKey.Size = new System.Drawing.Size(176, 20);
            this.chNumKey.TabIndex = 2;
            // 
            // roleKey
            // 
            this.roleKey.BackColor = System.Drawing.Color.Transparent;
            this.roleKey.Location = new System.Drawing.Point(10, 10);
            this.roleKey.Name = "roleKey";
            this.roleKey.Size = new System.Drawing.Size(176, 20);
            this.roleKey.TabIndex = 0;
            // 
            // roleValue
            // 
            this.roleValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roleValue.FormattingEnabled = true;
            this.roleValue.Location = new System.Drawing.Point(192, 10);
            this.roleValue.Name = "roleValue";
            this.roleValue.Size = new System.Drawing.Size(180, 20);
            this.roleValue.TabIndex = 1;
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
            // IPsKey
            // 
            this.IPsKey.BackColor = System.Drawing.Color.Transparent;
            this.IPsKey.Location = new System.Drawing.Point(10, 140);
            this.IPsKey.Name = "IPsKey";
            this.IPsKey.Size = new System.Drawing.Size(176, 20);
            this.IPsKey.TabIndex = 10;
            // 
            // IPsValue
            // 
            this.IPsValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IPsValue.FormattingEnabled = true;
            this.IPsValue.Location = new System.Drawing.Point(192, 140);
            this.IPsValue.Name = "IPsValue";
            this.IPsValue.Size = new System.Drawing.Size(180, 20);
            this.IPsValue.TabIndex = 11;
            // 
            // userSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 561);
            this.Controls.Add(this.layout);
            this.Name = "userSearch";
            this.Text = "用户管理查询条件";
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
        private _queryLeft roleKey;
        private System.Windows.Forms.ComboBox roleValue;
        public System.Windows.Forms.Panel searchpanel;
        private _queryLeft chNumKey;
        private System.Windows.Forms.TextBox chNumValue;
        private System.Windows.Forms.TextBox loginNameValue;
        private _queryLeft loginNameKey;
        private System.Windows.Forms.TextBox agentNameValue;
        private _queryLeft agentNameKey;
        private _queryLeft chTypeKey;
        private System.Windows.Forms.ComboBox chTypeValue;
        private _queryLeft IPsKey;
        private System.Windows.Forms.ComboBox IPsValue;
    }
}
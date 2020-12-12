namespace CenoCC {
    partial class diallimitSearch {
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
            this.isshareValue = new System.Windows.Forms.ComboBox();
            this.isusecallValue = new System.Windows.Forms.ComboBox();
            this.isusedialValue = new System.Windows.Forms.ComboBox();
            this.isuseValue = new System.Windows.Forms.ComboBox();
            this.dtmfValue = new System.Windows.Forms.ComboBox();
            this.dialprefixValue = new System.Windows.Forms.TextBox();
            this.areanameValue = new System.Windows.Forms.TextBox();
            this.areacodeValue = new System.Windows.Forms.TextBox();
            this.gatewayValue = new System.Windows.Forms.ComboBox();
            this.tnumberKey = new CenoCC._queryLeft();
            this.isshareKey = new CenoCC._queryLeft();
            this.isusecallKey = new CenoCC._queryLeft();
            this.isusedialKey = new CenoCC._queryLeft();
            this.isuseKey = new CenoCC._queryLeft();
            this.dtmfKey = new CenoCC._queryLeft();
            this.dialprefixKey = new CenoCC._queryLeft();
            this.areanameKey = new CenoCC._queryLeft();
            this.areacodeKey = new CenoCC._queryLeft();
            this.gatewayKey = new CenoCC._queryLeft();
            this.numberValue = new System.Windows.Forms.TextBox();
            this.numberKey = new CenoCC._queryLeft();
            this.agentKey = new CenoCC._queryLeft();
            this.agentValue = new System.Windows.Forms.ComboBox();
            this.btnCloseAfterSearch = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.tnumberValue = new System.Windows.Forms.TextBox();
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
            this.searchpanel.Controls.Add(this.tnumberValue);
            this.searchpanel.Controls.Add(this.isshareValue);
            this.searchpanel.Controls.Add(this.isusecallValue);
            this.searchpanel.Controls.Add(this.isusedialValue);
            this.searchpanel.Controls.Add(this.isuseValue);
            this.searchpanel.Controls.Add(this.dtmfValue);
            this.searchpanel.Controls.Add(this.dialprefixValue);
            this.searchpanel.Controls.Add(this.areanameValue);
            this.searchpanel.Controls.Add(this.areacodeValue);
            this.searchpanel.Controls.Add(this.gatewayValue);
            this.searchpanel.Controls.Add(this.tnumberKey);
            this.searchpanel.Controls.Add(this.isshareKey);
            this.searchpanel.Controls.Add(this.isusecallKey);
            this.searchpanel.Controls.Add(this.isusedialKey);
            this.searchpanel.Controls.Add(this.isuseKey);
            this.searchpanel.Controls.Add(this.dtmfKey);
            this.searchpanel.Controls.Add(this.dialprefixKey);
            this.searchpanel.Controls.Add(this.areanameKey);
            this.searchpanel.Controls.Add(this.areacodeKey);
            this.searchpanel.Controls.Add(this.gatewayKey);
            this.searchpanel.Controls.Add(this.numberValue);
            this.searchpanel.Controls.Add(this.numberKey);
            this.searchpanel.Controls.Add(this.agentKey);
            this.searchpanel.Controls.Add(this.agentValue);
            this.searchpanel.Location = new System.Drawing.Point(0, 0);
            this.searchpanel.Name = "searchpanel";
            this.searchpanel.Size = new System.Drawing.Size(410, 360);
            this.searchpanel.TabIndex = 14;
            // 
            // isshareValue
            // 
            this.isshareValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.isshareValue.FormattingEnabled = true;
            this.isshareValue.Location = new System.Drawing.Point(192, 270);
            this.isshareValue.Name = "isshareValue";
            this.isshareValue.Size = new System.Drawing.Size(180, 20);
            this.isshareValue.TabIndex = 32;
            // 
            // isusecallValue
            // 
            this.isusecallValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.isusecallValue.FormattingEnabled = true;
            this.isusecallValue.Location = new System.Drawing.Point(192, 244);
            this.isusecallValue.Name = "isusecallValue";
            this.isusecallValue.Size = new System.Drawing.Size(180, 20);
            this.isusecallValue.TabIndex = 31;
            // 
            // isusedialValue
            // 
            this.isusedialValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.isusedialValue.FormattingEnabled = true;
            this.isusedialValue.Location = new System.Drawing.Point(192, 218);
            this.isusedialValue.Name = "isusedialValue";
            this.isusedialValue.Size = new System.Drawing.Size(180, 20);
            this.isusedialValue.TabIndex = 30;
            // 
            // isuseValue
            // 
            this.isuseValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.isuseValue.FormattingEnabled = true;
            this.isuseValue.Location = new System.Drawing.Point(192, 192);
            this.isuseValue.Name = "isuseValue";
            this.isuseValue.Size = new System.Drawing.Size(180, 20);
            this.isuseValue.TabIndex = 29;
            // 
            // dtmfValue
            // 
            this.dtmfValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dtmfValue.FormattingEnabled = true;
            this.dtmfValue.Location = new System.Drawing.Point(192, 166);
            this.dtmfValue.Name = "dtmfValue";
            this.dtmfValue.Size = new System.Drawing.Size(180, 20);
            this.dtmfValue.TabIndex = 28;
            // 
            // dialprefixValue
            // 
            this.dialprefixValue.Location = new System.Drawing.Point(192, 139);
            this.dialprefixValue.Name = "dialprefixValue";
            this.dialprefixValue.Size = new System.Drawing.Size(180, 21);
            this.dialprefixValue.TabIndex = 27;
            // 
            // areanameValue
            // 
            this.areanameValue.Location = new System.Drawing.Point(192, 113);
            this.areanameValue.Name = "areanameValue";
            this.areanameValue.Size = new System.Drawing.Size(180, 21);
            this.areanameValue.TabIndex = 26;
            // 
            // areacodeValue
            // 
            this.areacodeValue.Location = new System.Drawing.Point(192, 87);
            this.areacodeValue.Name = "areacodeValue";
            this.areacodeValue.Size = new System.Drawing.Size(180, 21);
            this.areacodeValue.TabIndex = 25;
            // 
            // gatewayValue
            // 
            this.gatewayValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gatewayValue.FormattingEnabled = true;
            this.gatewayValue.Location = new System.Drawing.Point(192, 62);
            this.gatewayValue.Name = "gatewayValue";
            this.gatewayValue.Size = new System.Drawing.Size(180, 20);
            this.gatewayValue.TabIndex = 24;
            // 
            // tnumberKey
            // 
            this.tnumberKey.BackColor = System.Drawing.Color.Transparent;
            this.tnumberKey.Location = new System.Drawing.Point(10, 296);
            this.tnumberKey.Name = "tnumberKey";
            this.tnumberKey.Size = new System.Drawing.Size(176, 20);
            this.tnumberKey.TabIndex = 22;
            // 
            // isshareKey
            // 
            this.isshareKey.BackColor = System.Drawing.Color.Transparent;
            this.isshareKey.Location = new System.Drawing.Point(10, 270);
            this.isshareKey.Name = "isshareKey";
            this.isshareKey.Size = new System.Drawing.Size(176, 20);
            this.isshareKey.TabIndex = 20;
            // 
            // isusecallKey
            // 
            this.isusecallKey.BackColor = System.Drawing.Color.Transparent;
            this.isusecallKey.Location = new System.Drawing.Point(10, 244);
            this.isusecallKey.Name = "isusecallKey";
            this.isusecallKey.Size = new System.Drawing.Size(176, 20);
            this.isusecallKey.TabIndex = 18;
            // 
            // isusedialKey
            // 
            this.isusedialKey.BackColor = System.Drawing.Color.Transparent;
            this.isusedialKey.Location = new System.Drawing.Point(10, 218);
            this.isusedialKey.Name = "isusedialKey";
            this.isusedialKey.Size = new System.Drawing.Size(176, 20);
            this.isusedialKey.TabIndex = 16;
            // 
            // isuseKey
            // 
            this.isuseKey.BackColor = System.Drawing.Color.Transparent;
            this.isuseKey.Location = new System.Drawing.Point(10, 192);
            this.isuseKey.Name = "isuseKey";
            this.isuseKey.Size = new System.Drawing.Size(176, 20);
            this.isuseKey.TabIndex = 14;
            // 
            // dtmfKey
            // 
            this.dtmfKey.BackColor = System.Drawing.Color.Transparent;
            this.dtmfKey.Location = new System.Drawing.Point(10, 166);
            this.dtmfKey.Name = "dtmfKey";
            this.dtmfKey.Size = new System.Drawing.Size(176, 20);
            this.dtmfKey.TabIndex = 12;
            // 
            // dialprefixKey
            // 
            this.dialprefixKey.BackColor = System.Drawing.Color.Transparent;
            this.dialprefixKey.Location = new System.Drawing.Point(10, 140);
            this.dialprefixKey.Name = "dialprefixKey";
            this.dialprefixKey.Size = new System.Drawing.Size(176, 20);
            this.dialprefixKey.TabIndex = 10;
            // 
            // areanameKey
            // 
            this.areanameKey.BackColor = System.Drawing.Color.Transparent;
            this.areanameKey.Location = new System.Drawing.Point(10, 114);
            this.areanameKey.Name = "areanameKey";
            this.areanameKey.Size = new System.Drawing.Size(176, 20);
            this.areanameKey.TabIndex = 8;
            // 
            // areacodeKey
            // 
            this.areacodeKey.BackColor = System.Drawing.Color.Transparent;
            this.areacodeKey.Location = new System.Drawing.Point(10, 88);
            this.areacodeKey.Name = "areacodeKey";
            this.areacodeKey.Size = new System.Drawing.Size(176, 20);
            this.areacodeKey.TabIndex = 6;
            // 
            // gatewayKey
            // 
            this.gatewayKey.BackColor = System.Drawing.Color.Transparent;
            this.gatewayKey.Location = new System.Drawing.Point(10, 62);
            this.gatewayKey.Name = "gatewayKey";
            this.gatewayKey.Size = new System.Drawing.Size(176, 20);
            this.gatewayKey.TabIndex = 4;
            // 
            // numberValue
            // 
            this.numberValue.Location = new System.Drawing.Point(192, 35);
            this.numberValue.Name = "numberValue";
            this.numberValue.Size = new System.Drawing.Size(180, 21);
            this.numberValue.TabIndex = 3;
            // 
            // numberKey
            // 
            this.numberKey.BackColor = System.Drawing.Color.Transparent;
            this.numberKey.Location = new System.Drawing.Point(10, 36);
            this.numberKey.Name = "numberKey";
            this.numberKey.Size = new System.Drawing.Size(176, 20);
            this.numberKey.TabIndex = 2;
            // 
            // agentKey
            // 
            this.agentKey.BackColor = System.Drawing.Color.Transparent;
            this.agentKey.Location = new System.Drawing.Point(10, 10);
            this.agentKey.Name = "agentKey";
            this.agentKey.Size = new System.Drawing.Size(176, 20);
            this.agentKey.TabIndex = 0;
            // 
            // agentValue
            // 
            this.agentValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.agentValue.FormattingEnabled = true;
            this.agentValue.Location = new System.Drawing.Point(192, 10);
            this.agentValue.Name = "agentValue";
            this.agentValue.Size = new System.Drawing.Size(180, 20);
            this.agentValue.TabIndex = 1;
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
            // tnumberValue
            // 
            this.tnumberValue.Location = new System.Drawing.Point(192, 295);
            this.tnumberValue.Name = "tnumberValue";
            this.tnumberValue.Size = new System.Drawing.Size(180, 21);
            this.tnumberValue.TabIndex = 33;
            // 
            // diallimitSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 561);
            this.Controls.Add(this.layout);
            this.Name = "diallimitSearch";
            this.Text = "拨号限制查询条件";
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
        private _queryLeft agentKey;
        private System.Windows.Forms.ComboBox agentValue;
        public System.Windows.Forms.Panel searchpanel;
        private _queryLeft numberKey;
        private System.Windows.Forms.TextBox numberValue;
        private _queryLeft gatewayKey;
        private _queryLeft areacodeKey;
        private _queryLeft areanameKey;
        private _queryLeft dialprefixKey;
        private _queryLeft isuseKey;
        private _queryLeft dtmfKey;
        private _queryLeft tnumberKey;
        private _queryLeft isshareKey;
        private _queryLeft isusecallKey;
        private _queryLeft isusedialKey;
        private System.Windows.Forms.ComboBox gatewayValue;
        private System.Windows.Forms.TextBox areacodeValue;
        private System.Windows.Forms.TextBox areanameValue;
        private System.Windows.Forms.TextBox dialprefixValue;
        private System.Windows.Forms.ComboBox dtmfValue;
        private System.Windows.Forms.ComboBox isuseValue;
        private System.Windows.Forms.ComboBox isusecallValue;
        private System.Windows.Forms.ComboBox isusedialValue;
        private System.Windows.Forms.ComboBox isshareValue;
        private System.Windows.Forms.TextBox tnumberValue;
    }
}
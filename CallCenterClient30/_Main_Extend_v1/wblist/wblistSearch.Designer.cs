namespace CenoCC
{
    partial class wblistSearch
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
            this.rtypeValue = new System.Windows.Forms.ComboBox();
            this.rtextKey = new CenoCC._queryLeft();
            this.rnameKey = new CenoCC._queryLeft();
            this.ctypeValue = new System.Windows.Forms.ComboBox();
            this.ctypeKey = new CenoCC._queryLeft();
            this.rnameValue = new System.Windows.Forms.TextBox();
            this.rtypeKey = new CenoCC._queryLeft();
            this.rtextValue = new System.Windows.Forms.TextBox();
            this.btnCloseAfterSearch = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.rnumberKey = new CenoCC._queryLeft();
            this.rnumberValue = new System.Windows.Forms.TextBox();
            this.ordernumKey = new CenoCC._queryLeft();
            this.ordernumValue = new System.Windows.Forms.TextBox();
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
            this.searchpanel.Controls.Add(this.ordernumKey);
            this.searchpanel.Controls.Add(this.ordernumValue);
            this.searchpanel.Controls.Add(this.rnumberKey);
            this.searchpanel.Controls.Add(this.rnumberValue);
            this.searchpanel.Controls.Add(this.rtypeValue);
            this.searchpanel.Controls.Add(this.rtextKey);
            this.searchpanel.Controls.Add(this.rnameKey);
            this.searchpanel.Controls.Add(this.ctypeValue);
            this.searchpanel.Controls.Add(this.ctypeKey);
            this.searchpanel.Controls.Add(this.rnameValue);
            this.searchpanel.Controls.Add(this.rtypeKey);
            this.searchpanel.Controls.Add(this.rtextValue);
            this.searchpanel.Location = new System.Drawing.Point(0, 0);
            this.searchpanel.Name = "searchpanel";
            this.searchpanel.Size = new System.Drawing.Size(410, 360);
            this.searchpanel.TabIndex = 14;
            // 
            // rtypeValue
            // 
            this.rtypeValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rtypeValue.FormattingEnabled = true;
            this.rtypeValue.Location = new System.Drawing.Point(192, 62);
            this.rtypeValue.Name = "rtypeValue";
            this.rtypeValue.Size = new System.Drawing.Size(180, 20);
            this.rtypeValue.TabIndex = 5;
            // 
            // rtextKey
            // 
            this.rtextKey.BackColor = System.Drawing.Color.Transparent;
            this.rtextKey.Location = new System.Drawing.Point(10, 88);
            this.rtextKey.Name = "rtextKey";
            this.rtextKey.Size = new System.Drawing.Size(176, 20);
            this.rtextKey.TabIndex = 6;
            // 
            // rnameKey
            // 
            this.rnameKey.BackColor = System.Drawing.Color.Transparent;
            this.rnameKey.Location = new System.Drawing.Point(10, 10);
            this.rnameKey.Name = "rnameKey";
            this.rnameKey.Size = new System.Drawing.Size(176, 20);
            this.rnameKey.TabIndex = 0;
            this.rnameKey.TabStop = false;
            // 
            // ctypeValue
            // 
            this.ctypeValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctypeValue.FormattingEnabled = true;
            this.ctypeValue.Location = new System.Drawing.Point(192, 36);
            this.ctypeValue.Name = "ctypeValue";
            this.ctypeValue.Size = new System.Drawing.Size(180, 20);
            this.ctypeValue.TabIndex = 3;
            // 
            // ctypeKey
            // 
            this.ctypeKey.BackColor = System.Drawing.Color.Transparent;
            this.ctypeKey.Location = new System.Drawing.Point(10, 36);
            this.ctypeKey.Name = "ctypeKey";
            this.ctypeKey.Size = new System.Drawing.Size(176, 20);
            this.ctypeKey.TabIndex = 2;
            // 
            // rnameValue
            // 
            this.rnameValue.Location = new System.Drawing.Point(192, 9);
            this.rnameValue.Name = "rnameValue";
            this.rnameValue.Size = new System.Drawing.Size(180, 21);
            this.rnameValue.TabIndex = 1;
            // 
            // rtypeKey
            // 
            this.rtypeKey.BackColor = System.Drawing.Color.Transparent;
            this.rtypeKey.Location = new System.Drawing.Point(10, 62);
            this.rtypeKey.Name = "rtypeKey";
            this.rtypeKey.Size = new System.Drawing.Size(176, 20);
            this.rtypeKey.TabIndex = 4;
            // 
            // rtextValue
            // 
            this.rtextValue.Location = new System.Drawing.Point(192, 87);
            this.rtextValue.Name = "rtextValue";
            this.rtextValue.Size = new System.Drawing.Size(180, 21);
            this.rtextValue.TabIndex = 7;
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
            // rnumberKey
            // 
            this.rnumberKey.BackColor = System.Drawing.Color.Transparent;
            this.rnumberKey.Location = new System.Drawing.Point(10, 115);
            this.rnumberKey.Name = "rnumberKey";
            this.rnumberKey.Size = new System.Drawing.Size(176, 20);
            this.rnumberKey.TabIndex = 8;
            // 
            // rnumberValue
            // 
            this.rnumberValue.Location = new System.Drawing.Point(192, 114);
            this.rnumberValue.Name = "rnumberValue";
            this.rnumberValue.Size = new System.Drawing.Size(180, 21);
            this.rnumberValue.TabIndex = 9;
            // 
            // ordernumKey
            // 
            this.ordernumKey.BackColor = System.Drawing.Color.Transparent;
            this.ordernumKey.Location = new System.Drawing.Point(10, 142);
            this.ordernumKey.Name = "ordernumKey";
            this.ordernumKey.Size = new System.Drawing.Size(176, 20);
            this.ordernumKey.TabIndex = 10;
            // 
            // ordernumValue
            // 
            this.ordernumValue.Location = new System.Drawing.Point(192, 141);
            this.ordernumValue.Name = "ordernumValue";
            this.ordernumValue.Size = new System.Drawing.Size(180, 21);
            this.ordernumValue.TabIndex = 11;
            // 
            // routeSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 561);
            this.Controls.Add(this.layout);
            this.Name = "routeSearch";
            this.Text = "通话记录查询条件";
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
        private _queryLeft rnameKey;
        private System.Windows.Forms.ComboBox ctypeValue;
        private _queryLeft ctypeKey;
        private System.Windows.Forms.TextBox rnameValue;
        private _queryLeft rtypeKey;
        private System.Windows.Forms.TextBox rtextValue;
        public System.Windows.Forms.Panel searchpanel;
        private _queryLeft rtextKey;
        private System.Windows.Forms.ComboBox rtypeValue;
        private _queryLeft ordernumKey;
        private System.Windows.Forms.TextBox ordernumValue;
        private _queryLeft rnumberKey;
        private System.Windows.Forms.TextBox rnumberValue;
    }
}
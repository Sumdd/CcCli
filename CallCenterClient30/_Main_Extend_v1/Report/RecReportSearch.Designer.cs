namespace CenoCC
{
    partial class RecReportSearch
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
            this.agentKey = new CenoCC._queryLeft();
            this.agentValue = new System.Windows.Forms.ComboBox();
            this.endSpeakTimeValue = new System.Windows.Forms.DateTimePicker();
            this.endSpeakTimeKey = new CenoCC._queryLeft();
            this.startSpeakTimeValue = new System.Windows.Forms.DateTimePicker();
            this.startDateTimeKey = new CenoCC._queryLeft();
            this.startSpeakTimeKey = new CenoCC._queryLeft();
            this.startDateTimeValue = new System.Windows.Forms.DateTimePicker();
            this.endDateTimeValue = new System.Windows.Forms.DateTimePicker();
            this.endDateTimeKey = new CenoCC._queryLeft();
            this.btnCloseAfterSearch = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.reportTypeKey = new CenoCC._queryLeft();
            this.reportTypeValue = new System.Windows.Forms.ComboBox();
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
            this.searchpanel.Controls.Add(this.reportTypeKey);
            this.searchpanel.Controls.Add(this.reportTypeValue);
            this.searchpanel.Controls.Add(this.agentKey);
            this.searchpanel.Controls.Add(this.agentValue);
            this.searchpanel.Controls.Add(this.endSpeakTimeValue);
            this.searchpanel.Controls.Add(this.endSpeakTimeKey);
            this.searchpanel.Controls.Add(this.startSpeakTimeValue);
            this.searchpanel.Controls.Add(this.startDateTimeKey);
            this.searchpanel.Controls.Add(this.startSpeakTimeKey);
            this.searchpanel.Controls.Add(this.startDateTimeValue);
            this.searchpanel.Controls.Add(this.endDateTimeValue);
            this.searchpanel.Controls.Add(this.endDateTimeKey);
            this.searchpanel.Location = new System.Drawing.Point(0, 0);
            this.searchpanel.Name = "searchpanel";
            this.searchpanel.Size = new System.Drawing.Size(410, 360);
            this.searchpanel.TabIndex = 14;
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
            // endSpeakTimeValue
            // 
            this.endSpeakTimeValue.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.endSpeakTimeValue.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.endSpeakTimeValue.Location = new System.Drawing.Point(192, 113);
            this.endSpeakTimeValue.Name = "endSpeakTimeValue";
            this.endSpeakTimeValue.ShowCheckBox = true;
            this.endSpeakTimeValue.ShowUpDown = true;
            this.endSpeakTimeValue.Size = new System.Drawing.Size(180, 21);
            this.endSpeakTimeValue.TabIndex = 13;
            // 
            // endSpeakTimeKey
            // 
            this.endSpeakTimeKey.BackColor = System.Drawing.Color.Transparent;
            this.endSpeakTimeKey.Location = new System.Drawing.Point(10, 114);
            this.endSpeakTimeKey.Name = "endSpeakTimeKey";
            this.endSpeakTimeKey.Size = new System.Drawing.Size(176, 20);
            this.endSpeakTimeKey.TabIndex = 12;
            // 
            // startSpeakTimeValue
            // 
            this.startSpeakTimeValue.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.startSpeakTimeValue.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.startSpeakTimeValue.Location = new System.Drawing.Point(192, 87);
            this.startSpeakTimeValue.Name = "startSpeakTimeValue";
            this.startSpeakTimeValue.ShowCheckBox = true;
            this.startSpeakTimeValue.ShowUpDown = true;
            this.startSpeakTimeValue.Size = new System.Drawing.Size(180, 21);
            this.startSpeakTimeValue.TabIndex = 11;
            // 
            // startDateTimeKey
            // 
            this.startDateTimeKey.BackColor = System.Drawing.Color.Transparent;
            this.startDateTimeKey.Location = new System.Drawing.Point(10, 36);
            this.startDateTimeKey.Name = "startDateTimeKey";
            this.startDateTimeKey.Size = new System.Drawing.Size(176, 20);
            this.startDateTimeKey.TabIndex = 6;
            // 
            // startSpeakTimeKey
            // 
            this.startSpeakTimeKey.BackColor = System.Drawing.Color.Transparent;
            this.startSpeakTimeKey.Location = new System.Drawing.Point(10, 88);
            this.startSpeakTimeKey.Name = "startSpeakTimeKey";
            this.startSpeakTimeKey.Size = new System.Drawing.Size(176, 20);
            this.startSpeakTimeKey.TabIndex = 10;
            // 
            // startDateTimeValue
            // 
            this.startDateTimeValue.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.startDateTimeValue.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDateTimeValue.Location = new System.Drawing.Point(192, 35);
            this.startDateTimeValue.Name = "startDateTimeValue";
            this.startDateTimeValue.ShowCheckBox = true;
            this.startDateTimeValue.ShowUpDown = true;
            this.startDateTimeValue.Size = new System.Drawing.Size(180, 21);
            this.startDateTimeValue.TabIndex = 7;
            // 
            // endDateTimeValue
            // 
            this.endDateTimeValue.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.endDateTimeValue.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDateTimeValue.Location = new System.Drawing.Point(192, 61);
            this.endDateTimeValue.Name = "endDateTimeValue";
            this.endDateTimeValue.ShowCheckBox = true;
            this.endDateTimeValue.ShowUpDown = true;
            this.endDateTimeValue.Size = new System.Drawing.Size(180, 21);
            this.endDateTimeValue.TabIndex = 9;
            // 
            // endDateTimeKey
            // 
            this.endDateTimeKey.BackColor = System.Drawing.Color.Transparent;
            this.endDateTimeKey.Location = new System.Drawing.Point(10, 62);
            this.endDateTimeKey.Name = "endDateTimeKey";
            this.endDateTimeKey.Size = new System.Drawing.Size(176, 20);
            this.endDateTimeKey.TabIndex = 8;
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
            // reportTypeKey
            // 
            this.reportTypeKey.BackColor = System.Drawing.Color.Transparent;
            this.reportTypeKey.Location = new System.Drawing.Point(10, 140);
            this.reportTypeKey.Name = "reportTypeKey";
            this.reportTypeKey.Size = new System.Drawing.Size(176, 20);
            this.reportTypeKey.TabIndex = 14;
            // 
            // reportTypeValue
            // 
            this.reportTypeValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.reportTypeValue.FormattingEnabled = true;
            this.reportTypeValue.Location = new System.Drawing.Point(192, 140);
            this.reportTypeValue.Name = "reportTypeValue";
            this.reportTypeValue.Size = new System.Drawing.Size(180, 20);
            this.reportTypeValue.TabIndex = 15;
            // 
            // RecReportSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 561);
            this.Controls.Add(this.layout);
            this.Name = "RecReportSearch";
            this.Text = "统计表查询条件";
            this.layout.ResumeLayout(false);
            this.searchpanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCloseAfterSearch;
        private _queryLeft agentKey;
        private System.Windows.Forms.ComboBox agentValue;
        private _queryLeft startDateTimeKey;
        private System.Windows.Forms.DateTimePicker startDateTimeValue;
        private System.Windows.Forms.DateTimePicker endDateTimeValue;
        private _queryLeft endDateTimeKey;
        private _queryLeft startSpeakTimeKey;
        private System.Windows.Forms.DateTimePicker startSpeakTimeValue;
        private System.Windows.Forms.DateTimePicker endSpeakTimeValue;
        private _queryLeft endSpeakTimeKey;
        public System.Windows.Forms.Panel searchpanel;
        private _queryLeft reportTypeKey;
        private System.Windows.Forms.ComboBox reportTypeValue;
    }
}
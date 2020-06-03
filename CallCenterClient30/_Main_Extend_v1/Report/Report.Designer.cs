namespace CenoCC {
    partial class Report {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Report));
            this.layout = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.ucPager = new CenoCC._ucPager();
            this.label2 = new System.Windows.Forms.Label();
            this.totalCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.totalTime = new System.Windows.Forms.TextBox();
            this.list = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.downloadSearch = new System.Windows.Forms.Button();
            this.downloadPage = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearchOpen = new System.Windows.Forms.Button();
            this.contextListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.callNumber = new System.Windows.Forms.ToolStripTextBox();
            this.callNow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddZeroDial = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRecordListenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.recordUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.layout.SuspendLayout();
            this.contextListMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.btnExport);
            this.layout.Controls.Add(this.ucPager);
            this.layout.Controls.Add(this.label2);
            this.layout.Controls.Add(this.totalCount);
            this.layout.Controls.Add(this.label1);
            this.layout.Controls.Add(this.totalTime);
            this.layout.Controls.Add(this.list);
            this.layout.Controls.Add(this.downloadSearch);
            this.layout.Controls.Add(this.downloadPage);
            this.layout.Controls.Add(this.btnReset);
            this.layout.Controls.Add(this.btnSearch);
            this.layout.Controls.Add(this.btnSearchOpen);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(982, 476);
            this.layout.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(430, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(70, 23);
            this.btnExport.TabIndex = 11;
            this.btnExport.Tag = "phonerecords_excel";
            this.btnExport.Text = "导出Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // ucPager
            // 
            this.ucPager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucPager.Location = new System.Drawing.Point(10, 445);
            this.ucPager.Name = "ucPager";
            this.ucPager.Size = new System.Drawing.Size(738, 20);
            this.ucPager.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(596, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "累计通话数量";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalCount
            // 
            this.totalCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.totalCount.Location = new System.Drawing.Point(680, 12);
            this.totalCount.Name = "totalCount";
            this.totalCount.Size = new System.Drawing.Size(100, 21);
            this.totalCount.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(786, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "累计通话时长";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalTime
            // 
            this.totalTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.totalTime.Location = new System.Drawing.Point(870, 12);
            this.totalTime.Name = "totalTime";
            this.totalTime.Size = new System.Drawing.Size(100, 21);
            this.totalTime.TabIndex = 8;
            // 
            // list
            // 
            this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list.FullRowSelect = true;
            this.list.HideSelection = false;
            this.list.Location = new System.Drawing.Point(10, 39);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(960, 400);
            this.list.SmallImageList = this.imageList;
            this.list.TabIndex = 9;
            this.list.TabStop = false;
            this.list.UseCompatibleStateImageBehavior = false;
            this.list.View = System.Windows.Forms.View.Details;
            this.list.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.list_ColumnClick);
            this.list.MouseClick += new System.Windows.Forms.MouseEventHandler(this.list_MouseClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "column.png");
            this.imageList.Images.SetKeyName(1, "asc.png");
            this.imageList.Images.SetKeyName(2, "desc.png");
            // 
            // downloadSearch
            // 
            this.downloadSearch.Location = new System.Drawing.Point(324, 10);
            this.downloadSearch.Name = "downloadSearch";
            this.downloadSearch.Size = new System.Drawing.Size(100, 23);
            this.downloadSearch.TabIndex = 4;
            this.downloadSearch.Tag = "phonerecords_download_all";
            this.downloadSearch.Text = "下载全部录音";
            this.downloadSearch.UseVisualStyleBackColor = true;
            this.downloadSearch.Click += new System.EventHandler(this.downloadSearch_Click);
            // 
            // downloadPage
            // 
            this.downloadPage.Location = new System.Drawing.Point(218, 10);
            this.downloadPage.Name = "downloadPage";
            this.downloadPage.Size = new System.Drawing.Size(100, 23);
            this.downloadPage.TabIndex = 3;
            this.downloadPage.Tag = "phonerecords_download_page";
            this.downloadPage.Text = "下载当前页录音";
            this.downloadPage.UseVisualStyleBackColor = true;
            this.downloadPage.Click += new System.EventHandler(this.downloadPage_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(137, 10);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "重置查询";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(91, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(40, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSearchOpen
            // 
            this.btnSearchOpen.Location = new System.Drawing.Point(10, 10);
            this.btnSearchOpen.Name = "btnSearchOpen";
            this.btnSearchOpen.Size = new System.Drawing.Size(75, 23);
            this.btnSearchOpen.TabIndex = 0;
            this.btnSearchOpen.Text = "搜索条件";
            this.btnSearchOpen.UseVisualStyleBackColor = true;
            this.btnSearchOpen.Click += new System.EventHandler(this.btnSearchOpen_Click);
            // 
            // contextListMenu
            // 
            this.contextListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.callNumber,
            this.callNow,
            this.tsmiAddZeroDial,
            this.tsmiRecordListenTest,
            this.recordUpload});
            this.contextListMenu.Name = "metroListMenu";
            this.contextListMenu.Size = new System.Drawing.Size(161, 117);
            // 
            // callNumber
            // 
            this.callNumber.Name = "callNumber";
            this.callNumber.Size = new System.Drawing.Size(100, 23);
            this.callNumber.Text = "00000000000";
            // 
            // callNow
            // 
            this.callNow.Image = global::CenoCC.Properties.Resources.PickUp;
            this.callNow.Name = "callNow";
            this.callNow.Size = new System.Drawing.Size(160, 22);
            this.callNow.Text = "拨号";
            this.callNow.Click += new System.EventHandler(this.contextListMenu_Click);
            // 
            // tsmiAddZeroDial
            // 
            this.tsmiAddZeroDial.Image = global::CenoCC.Properties.Resources.PickUp;
            this.tsmiAddZeroDial.Name = "tsmiAddZeroDial";
            this.tsmiAddZeroDial.Size = new System.Drawing.Size(160, 22);
            this.tsmiAddZeroDial.Text = "加0拨号";
            this.tsmiAddZeroDial.Click += new System.EventHandler(this.contextListMenu_Click);
            // 
            // tsmiRecordListenTest
            // 
            this.tsmiRecordListenTest.Name = "tsmiRecordListenTest";
            this.tsmiRecordListenTest.Size = new System.Drawing.Size(160, 22);
            this.tsmiRecordListenTest.Text = "录音试听";
            this.tsmiRecordListenTest.Click += new System.EventHandler(this.contextListMenu_Click);
            // 
            // recordUpload
            // 
            this.recordUpload.Name = "recordUpload";
            this.recordUpload.Size = new System.Drawing.Size(160, 22);
            this.recordUpload.Text = "录音下载";
            this.recordUpload.Click += new System.EventHandler(this.contextListMenu_Click);
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 476);
            this.Controls.Add(this.layout);
            this.MinimumSize = new System.Drawing.Size(998, 515);
            this.Name = "Report";
            this.Text = "通话记录";
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this.contextListMenu.ResumeLayout(false);
            this.contextListMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.Button btnSearchOpen;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button downloadPage;
        private System.Windows.Forms.Button downloadSearch;
        private System.Windows.Forms.ListView list;
        private System.Windows.Forms.TextBox totalTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox totalCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip contextListMenu;
        private System.Windows.Forms.ToolStripMenuItem recordUpload;
        private _ucPager ucPager;
        private System.Windows.Forms.ToolStripMenuItem callNow;
        private System.Windows.Forms.ToolStripTextBox callNumber;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ToolStripMenuItem tsmiRecordListenTest;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddZeroDial;
    }
}
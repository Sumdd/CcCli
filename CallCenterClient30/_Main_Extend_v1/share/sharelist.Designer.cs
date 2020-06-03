namespace CenoCC
{
    partial class sharelist
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sharelist));
            this.layout = new System.Windows.Forms.Panel();
            this.btnAreaReload = new System.Windows.Forms.Button();
            this.btnAreaIn = new System.Windows.Forms.Button();
            this.btnAreaOut = new System.Windows.Forms.Button();
            this.btnAreaDelete = new System.Windows.Forms.Button();
            this.btnAreaSet = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.ucPager = new CenoCC._ucPager();
            this.list = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearchOpen = new System.Windows.Forms.Button();
            this.layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.btnAreaReload);
            this.layout.Controls.Add(this.btnAreaIn);
            this.layout.Controls.Add(this.btnAreaOut);
            this.layout.Controls.Add(this.btnAreaDelete);
            this.layout.Controls.Add(this.btnAreaSet);
            this.layout.Controls.Add(this.btnExport);
            this.layout.Controls.Add(this.ucPager);
            this.layout.Controls.Add(this.list);
            this.layout.Controls.Add(this.btnReset);
            this.layout.Controls.Add(this.btnSearch);
            this.layout.Controls.Add(this.btnSearchOpen);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(982, 476);
            this.layout.TabIndex = 0;
            // 
            // btnAreaReload
            // 
            this.btnAreaReload.Location = new System.Drawing.Point(498, 10);
            this.btnAreaReload.Name = "btnAreaReload";
            this.btnAreaReload.Size = new System.Drawing.Size(62, 23);
            this.btnAreaReload.TabIndex = 16;
            this.btnAreaReload.Tag = "share_area_reload";
            this.btnAreaReload.Text = "域重载";
            this.btnAreaReload.UseVisualStyleBackColor = true;
            this.btnAreaReload.Click += new System.EventHandler(this.btnAreaReload_Click);
            // 
            // btnAreaIn
            // 
            this.btnAreaIn.Location = new System.Drawing.Point(338, 10);
            this.btnAreaIn.Name = "btnAreaIn";
            this.btnAreaIn.Size = new System.Drawing.Size(74, 23);
            this.btnAreaIn.TabIndex = 15;
            this.btnAreaIn.Tag = "share_area_in";
            this.btnAreaIn.Text = "加入域申请";
            this.btnAreaIn.UseVisualStyleBackColor = true;
            this.btnAreaIn.Click += new System.EventHandler(this.btnAreaIn_Click);
            // 
            // btnAreaOut
            // 
            this.btnAreaOut.Location = new System.Drawing.Point(418, 10);
            this.btnAreaOut.Name = "btnAreaOut";
            this.btnAreaOut.Size = new System.Drawing.Size(74, 23);
            this.btnAreaOut.TabIndex = 14;
            this.btnAreaOut.Tag = "share_area_out";
            this.btnAreaOut.Text = "退出域申请";
            this.btnAreaOut.UseVisualStyleBackColor = true;
            this.btnAreaOut.Click += new System.EventHandler(this.btnAreaOut_Click);
            // 
            // btnAreaDelete
            // 
            this.btnAreaDelete.Location = new System.Drawing.Point(278, 10);
            this.btnAreaDelete.Name = "btnAreaDelete";
            this.btnAreaDelete.Size = new System.Drawing.Size(54, 23);
            this.btnAreaDelete.TabIndex = 13;
            this.btnAreaDelete.Tag = "share_area_delete";
            this.btnAreaDelete.Text = "删除域";
            this.btnAreaDelete.UseVisualStyleBackColor = true;
            this.btnAreaDelete.Click += new System.EventHandler(this.btnAreaDelete_Click);
            // 
            // btnAreaSet
            // 
            this.btnAreaSet.Location = new System.Drawing.Point(218, 10);
            this.btnAreaSet.Name = "btnAreaSet";
            this.btnAreaSet.Size = new System.Drawing.Size(54, 23);
            this.btnAreaSet.TabIndex = 12;
            this.btnAreaSet.Tag = "share_area_set";
            this.btnAreaSet.Text = "设置域";
            this.btnAreaSet.UseVisualStyleBackColor = true;
            this.btnAreaSet.Click += new System.EventHandler(this.btnAreaSet_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(900, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(70, 23);
            this.btnExport.TabIndex = 11;
            this.btnExport.Text = "导出Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Visible = false;
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
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "column.png");
            this.imageList.Images.SetKeyName(1, "asc.png");
            this.imageList.Images.SetKeyName(2, "desc.png");
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
            // sharelist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 476);
            this.Controls.Add(this.layout);
            this.MinimumSize = new System.Drawing.Size(998, 515);
            this.Name = "sharelist";
            this.Text = "共享域";
            this.layout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.Button btnSearchOpen;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReset;
        public System.Windows.Forms.ListView list;
        private System.Windows.Forms.ImageList imageList;
        private _ucPager ucPager;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnAreaSet;
        private System.Windows.Forms.Button btnAreaDelete;
        private System.Windows.Forms.Button btnAreaIn;
        private System.Windows.Forms.Button btnAreaOut;
        private System.Windows.Forms.Button btnAreaReload;
    }
}
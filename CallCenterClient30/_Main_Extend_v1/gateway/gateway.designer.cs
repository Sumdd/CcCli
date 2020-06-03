namespace CenoCC
{
    partial class gateway
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gateway));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.contextListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.callNumber = new System.Windows.Forms.ToolStripTextBox();
            this.callNow = new System.Windows.Forms.ToolStripMenuItem();
            this.recordUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.layout = new System.Windows.Forms.Panel();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnKill = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.list = new System.Windows.Forms.ListView();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearchOpen = new System.Windows.Forms.Button();
            this.ucPager = new CenoCC._ucPager();
            this.layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "column.png");
            this.imageList.Images.SetKeyName(1, "asc.png");
            this.imageList.Images.SetKeyName(2, "desc.png");
            // 
            // contextListMenu
            // 
            this.contextListMenu.Name = "contextListMenu";
            this.contextListMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // callNumber
            // 
            this.callNumber.Name = "callNumber";
            this.callNumber.Size = new System.Drawing.Size(100, 23);
            // 
            // callNow
            // 
            this.callNow.Name = "callNow";
            this.callNow.Size = new System.Drawing.Size(32, 19);
            // 
            // recordUpload
            // 
            this.recordUpload.Name = "recordUpload";
            this.recordUpload.Size = new System.Drawing.Size(32, 19);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "文本文件|*.txt";
            // 
            // layout
            // 
            this.layout.Controls.Add(this.btnRestart);
            this.layout.Controls.Add(this.btnKill);
            this.layout.Controls.Add(this.btnReload);
            this.layout.Controls.Add(this.btnDelete);
            this.layout.Controls.Add(this.btnEdit);
            this.layout.Controls.Add(this.btnAdd);
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
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(448, 10);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(40, 23);
            this.btnRestart.TabIndex = 16;
            this.btnRestart.Tag = "gateway_restart";
            this.btnRestart.Text = "重启";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnKill
            // 
            this.btnKill.Location = new System.Drawing.Point(356, 10);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(40, 23);
            this.btnKill.TabIndex = 15;
            this.btnKill.Tag = "gateway_kill";
            this.btnKill.Text = "杀死";
            this.btnKill.UseVisualStyleBackColor = true;
            this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(402, 10);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(40, 23);
            this.btnReload.TabIndex = 14;
            this.btnReload.Tag = "gateway_reload";
            this.btnReload.Text = "重载";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(310, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(40, 23);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Tag = "gateway_delete";
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(264, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(40, 23);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Tag = "gateway_edit";
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(218, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(40, 23);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Tag = "gateway_add";
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.list.SelectedIndexChanged += new System.EventHandler(this.list_SelectedIndexChanged);
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
            // ucPager
            // 
            this.ucPager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ucPager.Location = new System.Drawing.Point(10, 445);
            this.ucPager.Name = "ucPager";
            this.ucPager.Size = new System.Drawing.Size(738, 20);
            this.ucPager.TabIndex = 10;
            // 
            // gateway
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 476);
            this.Controls.Add(this.layout);
            this.MinimumSize = new System.Drawing.Size(998, 515);
            this.Name = "gateway";
            this.Text = "网关管理";
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
        private System.Windows.Forms.ContextMenuStrip contextListMenu;
        private System.Windows.Forms.ToolStripMenuItem recordUpload;
        private _ucPager ucPager;
        private System.Windows.Forms.ToolStripMenuItem callNow;
        private System.Windows.Forms.ToolStripTextBox callNumber;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnKill;
    }
}
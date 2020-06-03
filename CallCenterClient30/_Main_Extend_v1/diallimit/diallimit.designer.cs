namespace CenoCC {
    partial class diallimit {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(diallimit));
            this.layout = new System.Windows.Forms.Panel();
            this.btnUpdateNumber = new System.Windows.Forms.Button();
            this.btnIMS = new System.Windows.Forms.Button();
            this.cboGateway = new System.Windows.Forms.ComboBox();
            this.btnGateway = new System.Windows.Forms.Button();
            this.btnCmnset = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnQuick = new System.Windows.Forms.Button();
            this.btnUnuse = new System.Windows.Forms.Button();
            this.btnUse = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.diallimitImport = new System.Windows.Forms.Button();
            this.userListValue = new System.Windows.Forms.ComboBox();
            this.ucPager = new CenoCC._ucPager();
            this.list = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSearchOpen = new System.Windows.Forms.Button();
            this.contextListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.callNumber = new System.Windows.Forms.ToolStripTextBox();
            this.callNow = new System.Windows.Forms.ToolStripMenuItem();
            this.recordUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.btnUpdateNumber);
            this.layout.Controls.Add(this.btnIMS);
            this.layout.Controls.Add(this.cboGateway);
            this.layout.Controls.Add(this.btnGateway);
            this.layout.Controls.Add(this.btnCmnset);
            this.layout.Controls.Add(this.btnCreate);
            this.layout.Controls.Add(this.btnQuick);
            this.layout.Controls.Add(this.btnUnuse);
            this.layout.Controls.Add(this.btnUse);
            this.layout.Controls.Add(this.btnConfig);
            this.layout.Controls.Add(this.btnDel);
            this.layout.Controls.Add(this.diallimitImport);
            this.layout.Controls.Add(this.userListValue);
            this.layout.Controls.Add(this.ucPager);
            this.layout.Controls.Add(this.list);
            this.layout.Controls.Add(this.btnReset);
            this.layout.Controls.Add(this.btnSearch);
            this.layout.Controls.Add(this.btnSearchOpen);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(1063, 476);
            this.layout.TabIndex = 0;
            // 
            // btnUpdateNumber
            // 
            this.btnUpdateNumber.Location = new System.Drawing.Point(492, 10);
            this.btnUpdateNumber.Name = "btnUpdateNumber";
            this.btnUpdateNumber.Size = new System.Drawing.Size(37, 23);
            this.btnUpdateNumber.TabIndex = 23;
            this.btnUpdateNumber.Tag = "diallimit_number_update";
            this.btnUpdateNumber.Text = "号码";
            this.btnUpdateNumber.UseVisualStyleBackColor = true;
            this.btnUpdateNumber.Click += new System.EventHandler(this.btnUpdateNumber_Click);
            // 
            // btnIMS
            // 
            this.btnIMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIMS.Location = new System.Drawing.Point(537, 10);
            this.btnIMS.Name = "btnIMS";
            this.btnIMS.Size = new System.Drawing.Size(35, 23);
            this.btnIMS.TabIndex = 22;
            this.btnIMS.Tag = "diallimit_ims";
            this.btnIMS.Text = "IMS";
            this.btnIMS.UseVisualStyleBackColor = true;
            this.btnIMS.Click += new System.EventHandler(this.btnIMS_Click);
            // 
            // cboGateway
            // 
            this.cboGateway.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboGateway.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGateway.FormattingEnabled = true;
            this.cboGateway.Location = new System.Drawing.Point(578, 12);
            this.cboGateway.Name = "cboGateway";
            this.cboGateway.Size = new System.Drawing.Size(208, 20);
            this.cboGateway.TabIndex = 21;
            this.cboGateway.Tag = "";
            // 
            // btnGateway
            // 
            this.btnGateway.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGateway.Location = new System.Drawing.Point(792, 10);
            this.btnGateway.Name = "btnGateway";
            this.btnGateway.Size = new System.Drawing.Size(40, 23);
            this.btnGateway.TabIndex = 20;
            this.btnGateway.Tag = "diallimit_gateway";
            this.btnGateway.Text = "网关";
            this.btnGateway.UseVisualStyleBackColor = true;
            this.btnGateway.Click += new System.EventHandler(this.btnGateway_Click);
            // 
            // btnCmnset
            // 
            this.btnCmnset.Location = new System.Drawing.Point(424, 10);
            this.btnCmnset.Name = "btnCmnset";
            this.btnCmnset.Size = new System.Drawing.Size(62, 23);
            this.btnCmnset.TabIndex = 19;
            this.btnCmnset.Tag = "diallimit_common";
            this.btnCmnset.Text = "通用配置";
            this.btnCmnset.UseVisualStyleBackColor = true;
            this.btnCmnset.Click += new System.EventHandler(this.btnCmnset_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(965, 10);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(40, 23);
            this.btnCreate.TabIndex = 18;
            this.btnCreate.Tag = "diallimit_number_add";
            this.btnCreate.Text = "新增";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnQuick
            // 
            this.btnQuick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuick.Location = new System.Drawing.Point(1011, 10);
            this.btnQuick.Name = "btnQuick";
            this.btnQuick.Size = new System.Drawing.Size(40, 23);
            this.btnQuick.TabIndex = 17;
            this.btnQuick.Tag = "diallimit_number_assign";
            this.btnQuick.Text = "分配";
            this.btnQuick.UseVisualStyleBackColor = true;
            this.btnQuick.Click += new System.EventHandler(this.btnQuick_Click);
            // 
            // btnUnuse
            // 
            this.btnUnuse.Location = new System.Drawing.Point(264, 10);
            this.btnUnuse.Name = "btnUnuse";
            this.btnUnuse.Size = new System.Drawing.Size(40, 23);
            this.btnUnuse.TabIndex = 16;
            this.btnUnuse.Tag = "diallimit_disabled";
            this.btnUnuse.Text = "禁用";
            this.btnUnuse.UseVisualStyleBackColor = true;
            this.btnUnuse.Click += new System.EventHandler(this.btnUnuse_Click);
            // 
            // btnUse
            // 
            this.btnUse.Location = new System.Drawing.Point(218, 10);
            this.btnUse.Name = "btnUse";
            this.btnUse.Size = new System.Drawing.Size(40, 23);
            this.btnUse.TabIndex = 15;
            this.btnUse.Tag = "diallimit_enable";
            this.btnUse.Text = "启用";
            this.btnUse.UseVisualStyleBackColor = true;
            this.btnUse.Click += new System.EventHandler(this.btnUse_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(356, 10);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(62, 23);
            this.btnConfig.TabIndex = 14;
            this.btnConfig.Tag = "diallimit_limit";
            this.btnConfig.Text = "限制配置";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(310, 10);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(40, 23);
            this.btnDel.TabIndex = 13;
            this.btnDel.Tag = "diallimit_delete";
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // diallimitImport
            // 
            this.diallimitImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.diallimitImport.Enabled = false;
            this.diallimitImport.Location = new System.Drawing.Point(792, 10);
            this.diallimitImport.Name = "diallimitImport";
            this.diallimitImport.Size = new System.Drawing.Size(40, 23);
            this.diallimitImport.TabIndex = 12;
            this.diallimitImport.Text = "导入";
            this.diallimitImport.UseVisualStyleBackColor = true;
            this.diallimitImport.Visible = false;
            this.diallimitImport.Click += new System.EventHandler(this.diallimitImport_Click);
            // 
            // userListValue
            // 
            this.userListValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.userListValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userListValue.FormattingEnabled = true;
            this.userListValue.Location = new System.Drawing.Point(838, 12);
            this.userListValue.Name = "userListValue";
            this.userListValue.Size = new System.Drawing.Size(121, 20);
            this.userListValue.TabIndex = 11;
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
            this.list.Size = new System.Drawing.Size(1041, 400);
            this.list.SmallImageList = this.imageList;
            this.list.TabIndex = 9;
            this.list.TabStop = false;
            this.list.UseCompatibleStateImageBehavior = false;
            this.list.View = System.Windows.Forms.View.Details;
            this.list.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.list_ColumnClick);
            this.list.SelectedIndexChanged += new System.EventHandler(this.list_SelectedIndexChanged);
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
            // diallimit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 476);
            this.Controls.Add(this.layout);
            this.MinimumSize = new System.Drawing.Size(998, 515);
            this.Name = "diallimit";
            this.Text = "拨号限制";
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
        private System.Windows.Forms.Button diallimitImport;
        private System.Windows.Forms.ComboBox userListValue;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnUse;
        private System.Windows.Forms.Button btnUnuse;
        private System.Windows.Forms.Button btnQuick;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCmnset;
        private System.Windows.Forms.Button btnGateway;
        private System.Windows.Forms.ComboBox cboGateway;
        private System.Windows.Forms.Button btnIMS;
        private System.Windows.Forms.Button btnUpdateNumber;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
namespace CenoCC {
    partial class user {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(user));
            this.layout = new System.Windows.Forms.Panel();
            this.btnBaseInfo = new System.Windows.Forms.Button();
            this.btnSIPEdit = new System.Windows.Forms.Button();
            this.btnWeb = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnNotRegister = new System.Windows.Forms.Button();
            this.btnSetAutoCh = new System.Windows.Forms.Button();
            this.btnSetSIPCh = new System.Windows.Forms.Button();
            this.btnResetPwd = new System.Windows.Forms.Button();
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
            this.btnUpdUa = new System.Windows.Forms.Button();
            this.layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.btnUpdUa);
            this.layout.Controls.Add(this.btnBaseInfo);
            this.layout.Controls.Add(this.btnSIPEdit);
            this.layout.Controls.Add(this.btnWeb);
            this.layout.Controls.Add(this.btnExport);
            this.layout.Controls.Add(this.btnRegister);
            this.layout.Controls.Add(this.btnNotRegister);
            this.layout.Controls.Add(this.btnSetAutoCh);
            this.layout.Controls.Add(this.btnSetSIPCh);
            this.layout.Controls.Add(this.btnResetPwd);
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
            // btnBaseInfo
            // 
            this.btnBaseInfo.Location = new System.Drawing.Point(869, 10);
            this.btnBaseInfo.Name = "btnBaseInfo";
            this.btnBaseInfo.Size = new System.Drawing.Size(65, 23);
            this.btnBaseInfo.TabIndex = 19;
            this.btnBaseInfo.Tag = "user_baseinfo";
            this.btnBaseInfo.Text = "基本信息";
            this.btnBaseInfo.UseVisualStyleBackColor = true;
            this.btnBaseInfo.Click += new System.EventHandler(this.btnBaseInfo_Click);
            // 
            // btnSIPEdit
            // 
            this.btnSIPEdit.Location = new System.Drawing.Point(730, 10);
            this.btnSIPEdit.Name = "btnSIPEdit";
            this.btnSIPEdit.Size = new System.Drawing.Size(57, 23);
            this.btnSIPEdit.TabIndex = 18;
            this.btnSIPEdit.Tag = "user_sip_update";
            this.btnSIPEdit.Text = "SIP编辑";
            this.btnSIPEdit.UseVisualStyleBackColor = true;
            this.btnSIPEdit.Click += new System.EventHandler(this.btnSIPEdit_Click);
            // 
            // btnWeb
            // 
            this.btnWeb.Location = new System.Drawing.Point(669, 10);
            this.btnWeb.Name = "btnWeb";
            this.btnWeb.Size = new System.Drawing.Size(55, 23);
            this.btnWeb.TabIndex = 17;
            this.btnWeb.Tag = "user_web";
            this.btnWeb.Text = "Web调用";
            this.btnWeb.UseVisualStyleBackColor = true;
            this.btnWeb.Click += new System.EventHandler(this.btnWeb_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(793, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(70, 23);
            this.btnExport.TabIndex = 16;
            this.btnExport.Tag = "user_excel";
            this.btnExport.Text = "导出Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(610, 10);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(53, 23);
            this.btnRegister.TabIndex = 15;
            this.btnRegister.Tag = "user_register";
            this.btnRegister.Text = "PC注册";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnNotRegister
            // 
            this.btnNotRegister.Location = new System.Drawing.Point(531, 10);
            this.btnNotRegister.Name = "btnNotRegister";
            this.btnNotRegister.Size = new System.Drawing.Size(73, 23);
            this.btnNotRegister.TabIndex = 14;
            this.btnNotRegister.Tag = "user_register_no";
            this.btnNotRegister.Text = "PC非注册";
            this.btnNotRegister.UseVisualStyleBackColor = true;
            this.btnNotRegister.Click += new System.EventHandler(this.btnNotRegister_Click);
            // 
            // btnSetAutoCh
            // 
            this.btnSetAutoCh.Location = new System.Drawing.Point(402, 10);
            this.btnSetAutoCh.Name = "btnSetAutoCh";
            this.btnSetAutoCh.Size = new System.Drawing.Size(123, 23);
            this.btnSetAutoCh.TabIndex = 13;
            this.btnSetAutoCh.Tag = "user_set_auto_channel";
            this.btnSetAutoCh.Text = "设置为自动外呼通道";
            this.btnSetAutoCh.UseVisualStyleBackColor = true;
            this.btnSetAutoCh.Click += new System.EventHandler(this.btnSetAutoCh_Click);
            // 
            // btnSetSIPCh
            // 
            this.btnSetSIPCh.Location = new System.Drawing.Point(299, 10);
            this.btnSetSIPCh.Name = "btnSetSIPCh";
            this.btnSetSIPCh.Size = new System.Drawing.Size(97, 23);
            this.btnSetSIPCh.TabIndex = 12;
            this.btnSetSIPCh.Tag = "user_set_sip_channel";
            this.btnSetSIPCh.Text = "设置为SIP通道";
            this.btnSetSIPCh.UseVisualStyleBackColor = true;
            this.btnSetSIPCh.Click += new System.EventHandler(this.btnSetSIPCh_Click);
            // 
            // btnResetPwd
            // 
            this.btnResetPwd.Location = new System.Drawing.Point(218, 10);
            this.btnResetPwd.Name = "btnResetPwd";
            this.btnResetPwd.Size = new System.Drawing.Size(75, 23);
            this.btnResetPwd.TabIndex = 11;
            this.btnResetPwd.Tag = "user_password_reset";
            this.btnResetPwd.Text = "重置密码";
            this.btnResetPwd.UseVisualStyleBackColor = true;
            this.btnResetPwd.Click += new System.EventHandler(this.btnResetPwd_Click);
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
            // btnUpdUa
            // 
            this.btnUpdUa.Location = new System.Drawing.Point(940, 10);
            this.btnUpdUa.Name = "btnUpdUa";
            this.btnUpdUa.Size = new System.Drawing.Size(37, 23);
            this.btnUpdUa.TabIndex = 20;
            this.btnUpdUa.Tag = "user_update_ua";
            this.btnUpdUa.Text = "重载";
            this.btnUpdUa.UseVisualStyleBackColor = true;
            this.btnUpdUa.Click += new System.EventHandler(this.btnUpdUa_Click);
            // 
            // user
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 476);
            this.Controls.Add(this.layout);
            this.MinimumSize = new System.Drawing.Size(998, 515);
            this.Name = "user";
            this.Text = "用户管理";
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
        private System.Windows.Forms.Button btnResetPwd;
        private System.Windows.Forms.Button btnSetSIPCh;
        private System.Windows.Forms.Button btnSetAutoCh;
        private System.Windows.Forms.Button btnNotRegister;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnWeb;
        private System.Windows.Forms.Button btnSIPEdit;
        private System.Windows.Forms.Button btnBaseInfo;
        private System.Windows.Forms.Button btnUpdUa;
    }
}
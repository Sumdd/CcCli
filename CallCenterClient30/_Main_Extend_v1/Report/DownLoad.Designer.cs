namespace CenoCC {
    partial class DownLoad {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownLoad));
            this.panel = new System.Windows.Forms.Panel();
            this.list = new System.Windows.Forms.ListView();
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.msgTips = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.metroProgressSpinner = new MetroFramework.Controls.MetroProgressSpinner();
            this.contextListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.panel.SuspendLayout();
            this.contextListMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.list);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(20, 60);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(810, 220);
            this.panel.TabIndex = 0;
            // 
            // list
            // 
            this.list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.status,
            this.index,
            this.msgTips,
            this.fileName,
            this.progress});
            this.list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list.FullRowSelect = true;
            this.list.Location = new System.Drawing.Point(0, 0);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(810, 220);
            this.list.SmallImageList = this.imageList;
            this.list.TabIndex = 0;
            this.list.UseCompatibleStateImageBehavior = false;
            this.list.View = System.Windows.Forms.View.Details;
            this.list.MouseClick += new System.Windows.Forms.MouseEventHandler(this.list_MouseClick);
            // 
            // status
            // 
            this.status.Text = "";
            this.status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.status.Width = 30;
            // 
            // index
            // 
            this.index.Text = "序号";
            this.index.Width = 50;
            // 
            // msgTips
            // 
            this.msgTips.Text = "状态";
            this.msgTips.Width = 100;
            // 
            // fileName
            // 
            this.fileName.Text = "文件名";
            this.fileName.Width = 525;
            // 
            // progress
            // 
            this.progress.Text = "进度";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "download.png");
            this.imageList.Images.SetKeyName(1, "ok.png");
            this.imageList.Images.SetKeyName(2, "warn.png");
            this.imageList.Images.SetKeyName(3, "no.png");
            this.imageList.Images.SetKeyName(4, "tick.png");
            // 
            // metroProgressSpinner
            // 
            this.metroProgressSpinner.Location = new System.Drawing.Point(135, 30);
            this.metroProgressSpinner.Maximum = 100;
            this.metroProgressSpinner.Name = "metroProgressSpinner";
            this.metroProgressSpinner.Size = new System.Drawing.Size(16, 16);
            this.metroProgressSpinner.TabIndex = 1;
            this.metroProgressSpinner.UseSelectable = true;
            // 
            // contextListMenu
            // 
            this.contextListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openRecord});
            this.contextListMenu.Name = "contextListMenu";
            this.contextListMenu.Size = new System.Drawing.Size(161, 26);
            // 
            // openRecord
            // 
            this.openRecord.Name = "openRecord";
            this.openRecord.Size = new System.Drawing.Size(160, 22);
            this.openRecord.Text = "在文件夹中显示";
            this.openRecord.Click += new System.EventHandler(this.contextListMenu_Click);
            // 
            // DownLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 300);
            this.Controls.Add(this.metroProgressSpinner);
            this.Controls.Add(this.panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DownLoad";
            this.Text = "下载列表";
            this.panel.ResumeLayout(false);
            this.contextListMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ListView list;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader index;
        private System.Windows.Forms.ColumnHeader fileName;
        private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.ColumnHeader progress;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner;
        private System.Windows.Forms.ColumnHeader msgTips;
        private System.Windows.Forms.ContextMenuStrip contextListMenu;
        private System.Windows.Forms.ToolStripMenuItem openRecord;
    }
}
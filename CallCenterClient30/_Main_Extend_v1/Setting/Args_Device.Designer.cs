namespace CenoCC {
    partial class Args_Device {
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
            this.lfdevice = new System.Windows.Forms.GroupBox();
            this.maclb = new System.Windows.Forms.Label();
            this.maccombo = new System.Windows.Forms.ComboBox();
            this.ysqlb = new System.Windows.Forms.Label();
            this.ysqcombo = new System.Windows.Forms.ComboBox();
            this.layout.SuspendLayout();
            this.lfdevice.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.lfdevice);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(420, 400);
            this.layout.TabIndex = 2;
            // 
            // lfdevice
            // 
            this.lfdevice.Controls.Add(this.maclb);
            this.lfdevice.Controls.Add(this.maccombo);
            this.lfdevice.Controls.Add(this.ysqlb);
            this.lfdevice.Controls.Add(this.ysqcombo);
            this.lfdevice.Location = new System.Drawing.Point(10, 10);
            this.lfdevice.Name = "lfdevice";
            this.lfdevice.Size = new System.Drawing.Size(400, 74);
            this.lfdevice.TabIndex = 2;
            this.lfdevice.TabStop = false;
            this.lfdevice.Text = "设备";
            // 
            // maclb
            // 
            this.maclb.AutoSize = true;
            this.maclb.Location = new System.Drawing.Point(5, 50);
            this.maclb.Name = "maclb";
            this.maclb.Size = new System.Drawing.Size(65, 12);
            this.maclb.TabIndex = 3;
            this.maclb.Text = "麦克风设备";
            // 
            // maccombo
            // 
            this.maccombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.maccombo.FormattingEnabled = true;
            this.maccombo.Location = new System.Drawing.Point(75, 45);
            this.maccombo.Name = "maccombo";
            this.maccombo.Size = new System.Drawing.Size(320, 20);
            this.maccombo.TabIndex = 2;
            this.maccombo.SelectedIndexChanged += new System.EventHandler(this.maccombo_SelectedIndexChanged);
            // 
            // ysqlb
            // 
            this.ysqlb.AutoSize = true;
            this.ysqlb.Location = new System.Drawing.Point(5, 25);
            this.ysqlb.Name = "ysqlb";
            this.ysqlb.Size = new System.Drawing.Size(65, 12);
            this.ysqlb.TabIndex = 1;
            this.ysqlb.Text = "扬声器设备";
            // 
            // ysqcombo
            // 
            this.ysqcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ysqcombo.FormattingEnabled = true;
            this.ysqcombo.Location = new System.Drawing.Point(75, 20);
            this.ysqcombo.Name = "ysqcombo";
            this.ysqcombo.Size = new System.Drawing.Size(320, 20);
            this.ysqcombo.TabIndex = 0;
            this.ysqcombo.SelectedIndexChanged += new System.EventHandler(this.ysqcombo_SelectedIndexChanged);
            // 
            // Args_Device
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 400);
            this.Controls.Add(this.layout);
            this.Name = "Args_Device";
            this.Text = "录音";
            this.layout.ResumeLayout(false);
            this.lfdevice.ResumeLayout(false);
            this.lfdevice.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.GroupBox lfdevice;
        private System.Windows.Forms.Label maclb;
        private System.Windows.Forms.ComboBox maccombo;
        private System.Windows.Forms.Label ysqlb;
        private System.Windows.Forms.ComboBox ysqcombo;
    }
}
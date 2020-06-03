using Model_v1;

namespace CenoCC {
    partial class ArgsfrmSimple {
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
            Model_v1.M_kv m_kv1 = new Model_v1.M_kv();
            Model_v1.M_kv m_kv2 = new Model_v1.M_kv();
            Model_v1.M_kv m_kv3 = new Model_v1.M_kv();
            Model_v1.M_kv m_kv4 = new Model_v1.M_kv();
            Model_v1.M_kv m_kv5 = new Model_v1.M_kv();
            Model_v1.M_kv m_kv6 = new Model_v1.M_kv();
            Model_v1.M_kv m_kv7 = new Model_v1.M_kv();
            Model_v1.M_kv m_kv8 = new Model_v1.M_kv();
            this.leftMenu = new System.Windows.Forms.ListBox();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // leftMenu
            // 
            this.leftMenu.DisplayMember = "value";
            this.leftMenu.FormattingEnabled = true;
            this.leftMenu.ItemHeight = 12;
            m_kv1.key = 0;
            m_kv1.tag = "Args_Device";
            m_kv1.value = "设备";
            m_kv2.key = 1;
            m_kv2.tag = "Args_Record";
            m_kv2.value = "录音";
            m_kv3.key = 2;
            m_kv3.tag = "Args_Web";
            m_kv3.value = "催收系统";
            m_kv4.key = 3;
            m_kv4.tag = "Args_MultiNetwork";
            m_kv4.value = "多网卡";
            m_kv5.key = 4;
            m_kv5.tag = "Args_Safe";
            m_kv5.value = "安全";
            m_kv6.key = 5;
            m_kv6.tag = "Args_NoAnswer";
            m_kv6.value = "未接来电";
            m_kv7.key = 6;
            m_kv7.tag = "Args_DialSet";
            m_kv7.value = "拨号设置";
            m_kv8.key = 7;
            m_kv8.tag = "Args_CallerDisplay";
            m_kv8.value = "来电显示";
            this.leftMenu.Items.AddRange(new object[] {
            m_kv1,
            m_kv2,
            m_kv3,
            m_kv4,
            m_kv5,
            m_kv6,
            m_kv7,
            m_kv8});
            this.leftMenu.Location = new System.Drawing.Point(0, 0);
            this.leftMenu.Name = "leftMenu";
            this.leftMenu.Size = new System.Drawing.Size(80, 400);
            this.leftMenu.TabIndex = 0;
            this.leftMenu.ValueMember = "key";
            this.leftMenu.SelectedIndexChanged += new System.EventHandler(this.leftMenu_SelectedIndexChanged);
            // 
            // rightPanel
            // 
            this.rightPanel.Location = new System.Drawing.Point(80, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(420, 400);
            this.rightPanel.TabIndex = 1;
            this.rightPanel.Tag = "设备面板";
            // 
            // ArgsfrmSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftMenu);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ArgsfrmSimple";
            this.Text = "参数选项";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox leftMenu;
        private System.Windows.Forms.Panel rightPanel;
    }
}
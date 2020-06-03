namespace CenoCC {
    partial class Args_MultiNetwork {
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
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.layout = new System.Windows.Forms.Panel();
            this.networkgb = new System.Windows.Forms.GroupBox();
            this.networkValue = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reRegNowValue = new System.Windows.Forms.CheckBox();
            this.layout.SuspendLayout();
            this.networkgb.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.networkgb);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(420, 400);
            this.layout.TabIndex = 2;
            // 
            // networkgb
            // 
            this.networkgb.Controls.Add(this.reRegNowValue);
            this.networkgb.Controls.Add(this.networkValue);
            this.networkgb.Controls.Add(this.label1);
            this.networkgb.Location = new System.Drawing.Point(10, 10);
            this.networkgb.Name = "networkgb";
            this.networkgb.Size = new System.Drawing.Size(400, 70);
            this.networkgb.TabIndex = 2;
            this.networkgb.TabStop = false;
            this.networkgb.Text = "多网卡";
            // 
            // networkValue
            // 
            this.networkValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.networkValue.FormattingEnabled = true;
            this.networkValue.Location = new System.Drawing.Point(75, 19);
            this.networkValue.Name = "networkValue";
            this.networkValue.Size = new System.Drawing.Size(320, 20);
            this.networkValue.TabIndex = 6;
            this.networkValue.SelectedIndexChanged += new System.EventHandler(this.networkValue_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "网卡列表";
            // 
            // reRegNowValue
            // 
            this.reRegNowValue.AutoSize = true;
            this.reRegNowValue.Checked = true;
            this.reRegNowValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.reRegNowValue.Location = new System.Drawing.Point(75, 46);
            this.reRegNowValue.Name = "reRegNowValue";
            this.reRegNowValue.Size = new System.Drawing.Size(132, 16);
            this.reRegNowValue.TabIndex = 7;
            this.reRegNowValue.Text = "转换网卡后重新注册";
            this.reRegNowValue.UseVisualStyleBackColor = true;
            // 
            // Args_MultiNetwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 400);
            this.Controls.Add(this.layout);
            this.Name = "Args_MultiNetwork";
            this.Text = "录音";
            this.layout.ResumeLayout(false);
            this.networkgb.ResumeLayout(false);
            this.networkgb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.GroupBox networkgb;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox networkValue;
        private System.Windows.Forms.CheckBox reRegNowValue;
    }
}
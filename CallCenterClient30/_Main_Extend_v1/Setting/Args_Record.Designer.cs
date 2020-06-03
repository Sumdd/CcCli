namespace CenoCC {
    partial class Args_Record {
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
            this.recordgb = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudThread = new System.Windows.Forms.NumericUpDown();
            this.ckbSwitch = new System.Windows.Forms.CheckBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxSwitch = new System.Windows.Forms.ComboBox();
            this.btnPathNull = new System.Windows.Forms.Button();
            this.recordSavePath = new System.Windows.Forms.TextBox();
            this.choosePathbtn = new System.Windows.Forms.Button();
            this.recordlb = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.layout.SuspendLayout();
            this.recordgb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).BeginInit();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.recordgb);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(420, 400);
            this.layout.TabIndex = 2;
            // 
            // recordgb
            // 
            this.recordgb.Controls.Add(this.label2);
            this.recordgb.Controls.Add(this.nudThread);
            this.recordgb.Controls.Add(this.ckbSwitch);
            this.recordgb.Controls.Add(this.btnConvert);
            this.recordgb.Controls.Add(this.label1);
            this.recordgb.Controls.Add(this.cbxSwitch);
            this.recordgb.Controls.Add(this.btnPathNull);
            this.recordgb.Controls.Add(this.recordSavePath);
            this.recordgb.Controls.Add(this.choosePathbtn);
            this.recordgb.Controls.Add(this.recordlb);
            this.recordgb.Location = new System.Drawing.Point(10, 10);
            this.recordgb.Name = "recordgb";
            this.recordgb.Size = new System.Drawing.Size(400, 225);
            this.recordgb.TabIndex = 2;
            this.recordgb.TabStop = false;
            this.recordgb.Text = "录音";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "下载线程";
            // 
            // nudThread
            // 
            this.nudThread.Location = new System.Drawing.Point(75, 152);
            this.nudThread.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudThread.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudThread.Name = "nudThread";
            this.nudThread.Size = new System.Drawing.Size(320, 21);
            this.nudThread.TabIndex = 9;
            this.nudThread.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudThread.ValueChanged += new System.EventHandler(this.nudThread_ValueChanged);
            // 
            // ckbSwitch
            // 
            this.ckbSwitch.AutoSize = true;
            this.ckbSwitch.Location = new System.Drawing.Point(75, 100);
            this.ckbSwitch.Name = "ckbSwitch";
            this.ckbSwitch.Size = new System.Drawing.Size(252, 16);
            this.ckbSwitch.TabIndex = 8;
            this.ckbSwitch.Text = "每次下载时弹出格式转换对话框(支持HTTP)";
            this.ckbSwitch.UseVisualStyleBackColor = true;
            this.ckbSwitch.Click += new System.EventHandler(this.ckbSwitch_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(75, 122);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(100, 23);
            this.btnConvert.TabIndex = 7;
            this.btnConvert.Text = "格式转换工具";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "格式转换";
            // 
            // cbxSwitch
            // 
            this.cbxSwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSwitch.FormattingEnabled = true;
            this.cbxSwitch.Location = new System.Drawing.Point(75, 74);
            this.cbxSwitch.Name = "cbxSwitch";
            this.cbxSwitch.Size = new System.Drawing.Size(320, 20);
            this.cbxSwitch.TabIndex = 5;
            this.cbxSwitch.DropDownClosed += new System.EventHandler(this.cbxSwitch_DropDownClosed);
            // 
            // btnPathNull
            // 
            this.btnPathNull.Location = new System.Drawing.Point(156, 45);
            this.btnPathNull.Name = "btnPathNull";
            this.btnPathNull.Size = new System.Drawing.Size(75, 23);
            this.btnPathNull.TabIndex = 4;
            this.btnPathNull.Text = "清空路径";
            this.btnPathNull.UseVisualStyleBackColor = true;
            this.btnPathNull.Click += new System.EventHandler(this.btnPathNull_Click);
            // 
            // recordSavePath
            // 
            this.recordSavePath.Enabled = false;
            this.recordSavePath.Location = new System.Drawing.Point(75, 19);
            this.recordSavePath.Name = "recordSavePath";
            this.recordSavePath.Size = new System.Drawing.Size(320, 21);
            this.recordSavePath.TabIndex = 3;
            // 
            // choosePathbtn
            // 
            this.choosePathbtn.Location = new System.Drawing.Point(75, 45);
            this.choosePathbtn.Name = "choosePathbtn";
            this.choosePathbtn.Size = new System.Drawing.Size(75, 23);
            this.choosePathbtn.TabIndex = 2;
            this.choosePathbtn.Text = "选择";
            this.choosePathbtn.UseVisualStyleBackColor = true;
            this.choosePathbtn.Click += new System.EventHandler(this.choosePathbtn_Click);
            // 
            // recordlb
            // 
            this.recordlb.AutoSize = true;
            this.recordlb.Location = new System.Drawing.Point(5, 25);
            this.recordlb.Name = "recordlb";
            this.recordlb.Size = new System.Drawing.Size(53, 12);
            this.recordlb.TabIndex = 1;
            this.recordlb.Text = "录音路径";
            // 
            // Args_Record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 400);
            this.Controls.Add(this.layout);
            this.Name = "Args_Record";
            this.Text = "录音";
            this.layout.ResumeLayout(false);
            this.recordgb.ResumeLayout(false);
            this.recordgb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThread)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.GroupBox recordgb;
        private System.Windows.Forms.Label recordlb;
        private System.Windows.Forms.Button choosePathbtn;
        private System.Windows.Forms.TextBox recordSavePath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnPathNull;
        private System.Windows.Forms.ComboBox cbxSwitch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.CheckBox ckbSwitch;
        private System.Windows.Forms.NumericUpDown nudThread;
        private System.Windows.Forms.Label label2;
    }
}
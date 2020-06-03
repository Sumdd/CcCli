namespace CenoCC
{
    partial class DeviceOperateFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceOperateFrm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.Speaker_Voice_TrackB = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Record_Voice_TrackB = new System.Windows.Forms.TrackBar();
            this.OnLine_Voice_TrackB = new System.Windows.Forms.TrackBar();
            this.SpaOUT_Voice_TrackB = new System.Windows.Forms.TrackBar();
            this.Mic_Voice_TrackB = new System.Windows.Forms.TrackBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Speaker_Voice_TrackB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Record_Voice_TrackB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnLine_Voice_TrackB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpaOUT_Voice_TrackB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mic_Voice_TrackB)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(60, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(443, 421);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(435, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "设备状态";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(33, 139);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 28);
            this.button3.TabIndex = 3;
            this.button3.Text = "挂机";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(33, 94);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 28);
            this.button2.TabIndex = 2;
            this.button2.Text = "摘机";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(202, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "设备状态：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "打开设备";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBox3);
            this.tabPage2.Controls.Add(this.checkBox2);
            this.tabPage2.Controls.Add(this.checkBox1);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.Speaker_Voice_TrackB);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.Record_Voice_TrackB);
            this.tabPage2.Controls.Add(this.OnLine_Voice_TrackB);
            this.tabPage2.Controls.Add(this.SpaOUT_Voice_TrackB);
            this.tabPage2.Controls.Add(this.Mic_Voice_TrackB);
            this.tabPage2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(435, 383);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "音量控制";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(203, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 14);
            this.label6.TabIndex = 9;
            this.label6.Text = "喇叭";
            // 
            // Speaker_Voice_TrackB
            // 
            this.Speaker_Voice_TrackB.BackColor = System.Drawing.Color.White;
            this.Speaker_Voice_TrackB.Location = new System.Drawing.Point(197, 67);
            this.Speaker_Voice_TrackB.Maximum = 15;
            this.Speaker_Voice_TrackB.Name = "Speaker_Voice_TrackB";
            this.Speaker_Voice_TrackB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.Speaker_Voice_TrackB.Size = new System.Drawing.Size(45, 231);
            this.Speaker_Voice_TrackB.TabIndex = 8;
            this.Speaker_Voice_TrackB.Tag = "doplay";
            this.Speaker_Voice_TrackB.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(365, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "录音";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(283, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "线路";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "耳机";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "麦克风";
            // 
            // Record_Voice_TrackB
            // 
            this.Record_Voice_TrackB.BackColor = System.Drawing.Color.White;
            this.Record_Voice_TrackB.Location = new System.Drawing.Point(363, 67);
            this.Record_Voice_TrackB.Maximum = 7;
            this.Record_Voice_TrackB.Name = "Record_Voice_TrackB";
            this.Record_Voice_TrackB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.Record_Voice_TrackB.Size = new System.Drawing.Size(45, 231);
            this.Record_Voice_TrackB.TabIndex = 3;
            this.Record_Voice_TrackB.Tag = "linein";
            this.Record_Voice_TrackB.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // OnLine_Voice_TrackB
            // 
            this.OnLine_Voice_TrackB.BackColor = System.Drawing.Color.White;
            this.OnLine_Voice_TrackB.Location = new System.Drawing.Point(280, 67);
            this.OnLine_Voice_TrackB.Maximum = 15;
            this.OnLine_Voice_TrackB.Name = "OnLine_Voice_TrackB";
            this.OnLine_Voice_TrackB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.OnLine_Voice_TrackB.Size = new System.Drawing.Size(45, 231);
            this.OnLine_Voice_TrackB.TabIndex = 2;
            this.OnLine_Voice_TrackB.Tag = "lineout";
            this.OnLine_Voice_TrackB.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // SpaOUT_Voice_TrackB
            // 
            this.SpaOUT_Voice_TrackB.BackColor = System.Drawing.Color.White;
            this.SpaOUT_Voice_TrackB.Location = new System.Drawing.Point(114, 67);
            this.SpaOUT_Voice_TrackB.Maximum = 15;
            this.SpaOUT_Voice_TrackB.Name = "SpaOUT_Voice_TrackB";
            this.SpaOUT_Voice_TrackB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.SpaOUT_Voice_TrackB.Size = new System.Drawing.Size(45, 231);
            this.SpaOUT_Voice_TrackB.TabIndex = 1;
            this.SpaOUT_Voice_TrackB.Tag = "speaker";
            this.SpaOUT_Voice_TrackB.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // Mic_Voice_TrackB
            // 
            this.Mic_Voice_TrackB.BackColor = System.Drawing.Color.White;
            this.Mic_Voice_TrackB.Location = new System.Drawing.Point(31, 67);
            this.Mic_Voice_TrackB.Maximum = 7;
            this.Mic_Voice_TrackB.Name = "Mic_Voice_TrackB";
            this.Mic_Voice_TrackB.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.Mic_Voice_TrackB.Size = new System.Drawing.Size(45, 231);
            this.Mic_Voice_TrackB.TabIndex = 0;
            this.Mic_Voice_TrackB.Tag = "mic";
            this.Mic_Voice_TrackB.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(34, 309);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(54, 18);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "禁用";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(114, 309);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(54, 18);
            this.checkBox2.TabIndex = 11;
            this.checkBox2.Text = "静音";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(197, 309);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(54, 18);
            this.checkBox3.TabIndex = 12;
            this.checkBox3.Text = "静音";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // DeviceOperateFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(443, 421);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeviceOperateFrm";
            this.Text = "盒设备操作";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Speaker_Voice_TrackB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Record_Voice_TrackB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnLine_Voice_TrackB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpaOUT_Voice_TrackB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Mic_Voice_TrackB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TrackBar Mic_Voice_TrackB;
        private System.Windows.Forms.TrackBar Record_Voice_TrackB;
        private System.Windows.Forms.TrackBar OnLine_Voice_TrackB;
        private System.Windows.Forms.TrackBar SpaOUT_Voice_TrackB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar Speaker_Voice_TrackB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
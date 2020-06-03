namespace CenoCC
{
    partial class MediaPlayerFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediaPlayerFrm));
            this.SeekForwardBtn = new System.Windows.Forms.Button();
            this.PlayBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.BackwardBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.mediaState = new System.Windows.Forms.Label();
            this.pathTBox = new System.Windows.Forms.TextBox();
            this.OpenFileBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timeState1 = new System.Windows.Forms.Label();
            this.timeState = new System.Windows.Forms.Label();
            this.PlayProgressBar = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayProgressBar)).BeginInit();
            this.SuspendLayout();
            // 
            // SeekForwardBtn
            // 
            this.SeekForwardBtn.Enabled = false;
            this.SeekForwardBtn.Location = new System.Drawing.Point(296, 233);
            this.SeekForwardBtn.Name = "SeekForwardBtn";
            this.SeekForwardBtn.Size = new System.Drawing.Size(75, 23);
            this.SeekForwardBtn.TabIndex = 0;
            this.SeekForwardBtn.Text = "快进";
            this.SeekForwardBtn.UseVisualStyleBackColor = true;
            this.SeekForwardBtn.Click += new System.EventHandler(this.SeekForwardBtn_Click);
            // 
            // PlayBtn
            // 
            this.PlayBtn.Location = new System.Drawing.Point(134, 233);
            this.PlayBtn.Name = "PlayBtn";
            this.PlayBtn.Size = new System.Drawing.Size(75, 23);
            this.PlayBtn.TabIndex = 1;
            this.PlayBtn.Text = "播放";
            this.PlayBtn.UseVisualStyleBackColor = true;
            this.PlayBtn.Click += new System.EventHandler(this.PlayBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.Location = new System.Drawing.Point(215, 233);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(75, 23);
            this.StopBtn.TabIndex = 2;
            this.StopBtn.Text = "停止";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // BackwardBtn
            // 
            this.BackwardBtn.Enabled = false;
            this.BackwardBtn.Location = new System.Drawing.Point(53, 233);
            this.BackwardBtn.Name = "BackwardBtn";
            this.BackwardBtn.Size = new System.Drawing.Size(75, 23);
            this.BackwardBtn.TabIndex = 3;
            this.BackwardBtn.Text = "快退";
            this.BackwardBtn.UseVisualStyleBackColor = true;
            this.BackwardBtn.Click += new System.EventHandler(this.BackwardBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // mediaState
            // 
            this.mediaState.AutoSize = true;
            this.mediaState.Location = new System.Drawing.Point(44, 198);
            this.mediaState.Name = "mediaState";
            this.mediaState.Size = new System.Drawing.Size(35, 12);
            this.mediaState.TabIndex = 5;
            this.mediaState.Text = "     ";
            // 
            // pathTBox
            // 
            this.pathTBox.Location = new System.Drawing.Point(44, 30);
            this.pathTBox.Name = "pathTBox";
            this.pathTBox.Size = new System.Drawing.Size(262, 21);
            this.pathTBox.TabIndex = 8;
            // 
            // OpenFileBtn
            // 
            this.OpenFileBtn.Location = new System.Drawing.Point(313, 30);
            this.OpenFileBtn.Name = "OpenFileBtn";
            this.OpenFileBtn.Size = new System.Drawing.Size(75, 23);
            this.OpenFileBtn.TabIndex = 9;
            this.OpenFileBtn.Text = "浏览";
            this.OpenFileBtn.UseVisualStyleBackColor = true;
            this.OpenFileBtn.Click += new System.EventHandler(this.OpenFileBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.timeState1);
            this.groupBox1.Controls.Add(this.timeState);
            this.groupBox1.Controls.Add(this.PlayProgressBar);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(32, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 173);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(286, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "/";
            // 
            // timeState1
            // 
            this.timeState1.AutoSize = true;
            this.timeState1.Location = new System.Drawing.Point(232, 135);
            this.timeState1.Name = "timeState1";
            this.timeState1.Size = new System.Drawing.Size(53, 12);
            this.timeState1.TabIndex = 16;
            this.timeState1.Text = "00:00:00";
            // 
            // timeState
            // 
            this.timeState.AutoSize = true;
            this.timeState.Location = new System.Drawing.Point(297, 135);
            this.timeState.Name = "timeState";
            this.timeState.Size = new System.Drawing.Size(53, 12);
            this.timeState.TabIndex = 15;
            this.timeState.Text = "00:00:00";
            // 
            // PlayProgressBar
            // 
            this.PlayProgressBar.Enabled = false;
            this.PlayProgressBar.Location = new System.Drawing.Point(14, 111);
            this.PlayProgressBar.Name = "PlayProgressBar";
            this.PlayProgressBar.Size = new System.Drawing.Size(336, 45);
            this.PlayProgressBar.TabIndex = 12;
            this.PlayProgressBar.TabStop = false;
            this.PlayProgressBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.PlayProgressBar.Scroll += new System.EventHandler(this.PlayProgressBar_Scroll);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 84);
            this.panel1.TabIndex = 5;
            // 
            // MediaPlayerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 266);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OpenFileBtn);
            this.Controls.Add(this.pathTBox);
            this.Controls.Add(this.mediaState);
            this.Controls.Add(this.BackwardBtn);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.PlayBtn);
            this.Controls.Add(this.SeekForwardBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(428, 305);
            this.MinimumSize = new System.Drawing.Size(428, 305);
            this.Name = "MediaPlayerFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "音乐播放器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MediaPlayerFrm_FormClosing);
            this.Load += new System.EventHandler(this.MediaPlayerFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlayProgressBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SeekForwardBtn;
        private System.Windows.Forms.Button PlayBtn;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button BackwardBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label mediaState;
        private System.Windows.Forms.TextBox pathTBox;
        private System.Windows.Forms.Button OpenFileBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label timeState1;
        private System.Windows.Forms.Label timeState;
        private System.Windows.Forms.TrackBar PlayProgressBar;
        private System.Windows.Forms.Panel panel1;
    }
}
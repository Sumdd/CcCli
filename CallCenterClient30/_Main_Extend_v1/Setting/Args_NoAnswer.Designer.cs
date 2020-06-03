namespace CenoCC {
    partial class Args_NoAnswer {
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
            this.btnYes = new System.Windows.Forms.Button();
            this.gb = new System.Windows.Forms.GroupBox();
            this.noAnswerUseValue = new System.Windows.Forms.CheckBox();
            this.noAnswerDayValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.layout.SuspendLayout();
            this.gb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.noAnswerDayValue)).BeginInit();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.btnYes);
            this.layout.Controls.Add(this.gb);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(420, 400);
            this.layout.TabIndex = 2;
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(333, 365);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 4;
            this.btnYes.Text = "确定";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // gb
            // 
            this.gb.Controls.Add(this.noAnswerUseValue);
            this.gb.Controls.Add(this.noAnswerDayValue);
            this.gb.Controls.Add(this.label1);
            this.gb.Location = new System.Drawing.Point(10, 10);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(400, 74);
            this.gb.TabIndex = 2;
            this.gb.TabStop = false;
            this.gb.Text = "未接来电";
            // 
            // noAnswerUseValue
            // 
            this.noAnswerUseValue.AutoSize = true;
            this.noAnswerUseValue.Checked = true;
            this.noAnswerUseValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noAnswerUseValue.Location = new System.Drawing.Point(75, 46);
            this.noAnswerUseValue.Name = "noAnswerUseValue";
            this.noAnswerUseValue.Size = new System.Drawing.Size(252, 16);
            this.noAnswerUseValue.TabIndex = 6;
            this.noAnswerUseValue.Text = "启用未接天数（仅显示小于未接天数内的）";
            this.noAnswerUseValue.UseVisualStyleBackColor = true;
            // 
            // noAnswerDayValue
            // 
            this.noAnswerDayValue.Location = new System.Drawing.Point(75, 19);
            this.noAnswerDayValue.Name = "noAnswerDayValue";
            this.noAnswerDayValue.Size = new System.Drawing.Size(320, 21);
            this.noAnswerDayValue.TabIndex = 5;
            this.noAnswerDayValue.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "未接天数";
            // 
            // Args_NoAnswer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 400);
            this.Controls.Add(this.layout);
            this.Name = "Args_NoAnswer";
            this.Text = "录音";
            this.layout.ResumeLayout(false);
            this.gb.ResumeLayout(false);
            this.gb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.noAnswerDayValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel layout;
        private System.Windows.Forms.GroupBox gb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.NumericUpDown noAnswerDayValue;
        private System.Windows.Forms.CheckBox noAnswerUseValue;
        private System.Windows.Forms.Button btnYes;
    }
}
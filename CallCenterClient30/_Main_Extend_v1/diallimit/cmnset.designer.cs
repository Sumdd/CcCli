namespace CenoCC {
    partial class cmnset {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cmnset));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnUsing = new System.Windows.Forms.Button();
            this.btnUsingSelect = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAreaCode = new System.Windows.Forms.TextBox();
            this.txtAreaName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDialPrefix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxDtmf = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectDtmf = new System.Windows.Forms.Button();
            this.btnAllDtmf = new System.Windows.Forms.Button();
            this.cbxCommon = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAllCommon = new System.Windows.Forms.Button();
            this.btnSelectCommon = new System.Windows.Forms.Button();
            this.btnUpdateShare = new System.Windows.Forms.Button();
            this.btnLimitCallRuleAll = new System.Windows.Forms.Button();
            this.btnLimitCallRuleSelect = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxLimitCallRule = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(232, 146);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(40, 23);
            this.btnOK.TabIndex = 13;
            this.btnOK.Tag = "diallimit_common_area_ok";
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(186, 146);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(40, 23);
            this.btnReset.TabIndex = 12;
            this.btnReset.Tag = "diallimit_common_area_reset";
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnUsing
            // 
            this.btnUsing.Location = new System.Drawing.Point(24, 146);
            this.btnUsing.Name = "btnUsing";
            this.btnUsing.Size = new System.Drawing.Size(75, 23);
            this.btnUsing.TabIndex = 10;
            this.btnUsing.Tag = "diallimit_common_area_all";
            this.btnUsing.Text = "全部生效";
            this.btnUsing.UseVisualStyleBackColor = true;
            this.btnUsing.Click += new System.EventHandler(this.btnUsing_Click);
            // 
            // btnUsingSelect
            // 
            this.btnUsingSelect.Location = new System.Drawing.Point(105, 146);
            this.btnUsingSelect.Name = "btnUsingSelect";
            this.btnUsingSelect.Size = new System.Drawing.Size(75, 23);
            this.btnUsingSelect.TabIndex = 11;
            this.btnUsingSelect.Tag = "diallimit_common_area_select";
            this.btnUsingSelect.Text = "选中生效";
            this.btnUsingSelect.UseVisualStyleBackColor = true;
            this.btnUsingSelect.Click += new System.EventHandler(this.btnUsingSelect_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "默认区号";
            // 
            // txtAreaCode
            // 
            this.txtAreaCode.Location = new System.Drawing.Point(152, 12);
            this.txtAreaCode.Name = "txtAreaCode";
            this.txtAreaCode.Size = new System.Drawing.Size(120, 21);
            this.txtAreaCode.TabIndex = 15;
            // 
            // txtAreaName
            // 
            this.txtAreaName.Location = new System.Drawing.Point(152, 39);
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Size = new System.Drawing.Size(120, 21);
            this.txtAreaName.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "默认所在地";
            // 
            // txtDialPrefix
            // 
            this.txtDialPrefix.Location = new System.Drawing.Point(152, 66);
            this.txtDialPrefix.Name = "txtDialPrefix";
            this.txtDialPrefix.Size = new System.Drawing.Size(120, 21);
            this.txtDialPrefix.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 46);
            this.label2.TabIndex = 22;
            this.label2.Text = "说明:显示默认配置,可操作多条。以区号来判断拨打的是否是外地号码以及是否加拨前缀。";
            // 
            // cbxDtmf
            // 
            this.cbxDtmf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDtmf.FormattingEnabled = true;
            this.cbxDtmf.Location = new System.Drawing.Point(152, 198);
            this.cbxDtmf.Name = "cbxDtmf";
            this.cbxDtmf.Size = new System.Drawing.Size(120, 20);
            this.cbxDtmf.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "外地号码加拨前缀";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "dtmf(按键)发送方式";
            // 
            // btnSelectDtmf
            // 
            this.btnSelectDtmf.Location = new System.Drawing.Point(197, 224);
            this.btnSelectDtmf.Name = "btnSelectDtmf";
            this.btnSelectDtmf.Size = new System.Drawing.Size(75, 23);
            this.btnSelectDtmf.TabIndex = 25;
            this.btnSelectDtmf.Tag = "diallimit_common_dtmf_select";
            this.btnSelectDtmf.Text = "选中生效";
            this.btnSelectDtmf.UseVisualStyleBackColor = true;
            this.btnSelectDtmf.Click += new System.EventHandler(this.btnDtmf_Click);
            // 
            // btnAllDtmf
            // 
            this.btnAllDtmf.Location = new System.Drawing.Point(116, 224);
            this.btnAllDtmf.Name = "btnAllDtmf";
            this.btnAllDtmf.Size = new System.Drawing.Size(75, 23);
            this.btnAllDtmf.TabIndex = 26;
            this.btnAllDtmf.Tag = "diallimit_common_dtmf_all";
            this.btnAllDtmf.Text = "全部生效";
            this.btnAllDtmf.UseVisualStyleBackColor = true;
            this.btnAllDtmf.Click += new System.EventHandler(this.btnDtmf_Click);
            // 
            // cbxCommon
            // 
            this.cbxCommon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCommon.FormattingEnabled = true;
            this.cbxCommon.Location = new System.Drawing.Point(152, 269);
            this.cbxCommon.Name = "cbxCommon";
            this.cbxCommon.Size = new System.Drawing.Size(120, 20);
            this.cbxCommon.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "号码类别";
            // 
            // btnAllCommon
            // 
            this.btnAllCommon.Location = new System.Drawing.Point(116, 295);
            this.btnAllCommon.Name = "btnAllCommon";
            this.btnAllCommon.Size = new System.Drawing.Size(75, 23);
            this.btnAllCommon.TabIndex = 30;
            this.btnAllCommon.Tag = "diallimit_common_share_all";
            this.btnAllCommon.Text = "全部生效";
            this.btnAllCommon.UseVisualStyleBackColor = true;
            this.btnAllCommon.Click += new System.EventHandler(this.btnCommon_Click);
            // 
            // btnSelectCommon
            // 
            this.btnSelectCommon.Location = new System.Drawing.Point(197, 295);
            this.btnSelectCommon.Name = "btnSelectCommon";
            this.btnSelectCommon.Size = new System.Drawing.Size(75, 23);
            this.btnSelectCommon.TabIndex = 29;
            this.btnSelectCommon.Tag = "diallimit_common_share_select";
            this.btnSelectCommon.Text = "选中生效";
            this.btnSelectCommon.UseVisualStyleBackColor = true;
            this.btnSelectCommon.Click += new System.EventHandler(this.btnCommon_Click);
            // 
            // btnUpdateShare
            // 
            this.btnUpdateShare.Location = new System.Drawing.Point(12, 295);
            this.btnUpdateShare.Name = "btnUpdateShare";
            this.btnUpdateShare.Size = new System.Drawing.Size(98, 23);
            this.btnUpdateShare.TabIndex = 31;
            this.btnUpdateShare.Tag = "diallimit_common_share_update";
            this.btnUpdateShare.Text = "更新共享号码池";
            this.btnUpdateShare.UseVisualStyleBackColor = true;
            this.btnUpdateShare.Click += new System.EventHandler(this.btnUpdateShare_Click);
            // 
            // btnLimitCallRuleAll
            // 
            this.btnLimitCallRuleAll.Location = new System.Drawing.Point(116, 366);
            this.btnLimitCallRuleAll.Name = "btnLimitCallRuleAll";
            this.btnLimitCallRuleAll.Size = new System.Drawing.Size(75, 23);
            this.btnLimitCallRuleAll.TabIndex = 35;
            this.btnLimitCallRuleAll.Tag = "diallimit_common_limitcallrule_all";
            this.btnLimitCallRuleAll.Text = "全部生效";
            this.btnLimitCallRuleAll.UseVisualStyleBackColor = true;
            this.btnLimitCallRuleAll.Click += new System.EventHandler(this.btnLimitCallRule_Click);
            // 
            // btnLimitCallRuleSelect
            // 
            this.btnLimitCallRuleSelect.Location = new System.Drawing.Point(197, 366);
            this.btnLimitCallRuleSelect.Name = "btnLimitCallRuleSelect";
            this.btnLimitCallRuleSelect.Size = new System.Drawing.Size(75, 23);
            this.btnLimitCallRuleSelect.TabIndex = 34;
            this.btnLimitCallRuleSelect.Tag = "diallimit_common_limitcallrule_select";
            this.btnLimitCallRuleSelect.Text = "选中生效";
            this.btnLimitCallRuleSelect.UseVisualStyleBackColor = true;
            this.btnLimitCallRuleSelect.Click += new System.EventHandler(this.btnLimitCallRule_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 343);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "呼入规则";
            // 
            // cbxLimitCallRule
            // 
            this.cbxLimitCallRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLimitCallRule.FormattingEnabled = true;
            this.cbxLimitCallRule.Location = new System.Drawing.Point(152, 340);
            this.cbxLimitCallRule.Name = "cbxLimitCallRule";
            this.cbxLimitCallRule.Size = new System.Drawing.Size(120, 20);
            this.cbxLimitCallRule.TabIndex = 32;
            // 
            // cmnset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 401);
            this.Controls.Add(this.btnLimitCallRuleAll);
            this.Controls.Add(this.btnLimitCallRuleSelect);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxLimitCallRule);
            this.Controls.Add(this.btnUpdateShare);
            this.Controls.Add(this.btnAllCommon);
            this.Controls.Add(this.btnSelectCommon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxCommon);
            this.Controls.Add(this.btnAllDtmf);
            this.Controls.Add(this.btnSelectDtmf);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxDtmf);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDialPrefix);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtAreaName);
            this.Controls.Add(this.txtAreaCode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnUsingSelect);
            this.Controls.Add(this.btnUsing);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "cmnset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通用限制配置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnUsing;
        private System.Windows.Forms.Button btnUsingSelect;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAreaCode;
        private System.Windows.Forms.TextBox txtAreaName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDialPrefix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxDtmf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectDtmf;
        private System.Windows.Forms.Button btnAllDtmf;
        private System.Windows.Forms.ComboBox cbxCommon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAllCommon;
        private System.Windows.Forms.Button btnSelectCommon;
        private System.Windows.Forms.Button btnUpdateShare;
        private System.Windows.Forms.Button btnLimitCallRuleAll;
        private System.Windows.Forms.Button btnLimitCallRuleSelect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxLimitCallRule;
    }
}
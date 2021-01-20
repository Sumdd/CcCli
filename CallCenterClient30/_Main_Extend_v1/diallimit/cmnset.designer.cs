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
            this.label6 = new System.Windows.Forms.Label();
            this.txtDialLocalPrefix = new System.Windows.Forms.TextBox();
            this.btnPrefixDealFlagAll = new System.Windows.Forms.Button();
            this.btnPrefixDealFlagSelect = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxPrefixDealFlag = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnInlimit_2Reload = new System.Windows.Forms.Button();
            this.btnF99d999OK = new System.Windows.Forms.Button();
            this.btnF99d999Reset = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(232, 166);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(40, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Tag = "diallimit_common_area_ok";
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(186, 166);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(40, 23);
            this.btnReset.TabIndex = 7;
            this.btnReset.Tag = "diallimit_common_area_reset";
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnUsing
            // 
            this.btnUsing.Location = new System.Drawing.Point(24, 166);
            this.btnUsing.Name = "btnUsing";
            this.btnUsing.Size = new System.Drawing.Size(75, 23);
            this.btnUsing.TabIndex = 5;
            this.btnUsing.Tag = "diallimit_common_area_all";
            this.btnUsing.Text = "全部生效";
            this.btnUsing.UseVisualStyleBackColor = true;
            this.btnUsing.Click += new System.EventHandler(this.btnUsing_Click);
            // 
            // btnUsingSelect
            // 
            this.btnUsingSelect.Location = new System.Drawing.Point(105, 166);
            this.btnUsingSelect.Name = "btnUsingSelect";
            this.btnUsingSelect.Size = new System.Drawing.Size(75, 23);
            this.btnUsingSelect.TabIndex = 6;
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
            this.txtAreaCode.TabIndex = 1;
            // 
            // txtAreaName
            // 
            this.txtAreaName.Location = new System.Drawing.Point(152, 39);
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Size = new System.Drawing.Size(120, 21);
            this.txtAreaName.TabIndex = 2;
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
            this.txtDialPrefix.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 46);
            this.label2.TabIndex = 22;
            this.label2.Text = "说明:显示默认配置,可操作多条。以区号来判断拨打的是否是外地号码以及是否加拨外地前缀或是本地前缀";
            // 
            // cbxDtmf
            // 
            this.cbxDtmf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDtmf.FormattingEnabled = true;
            this.cbxDtmf.Location = new System.Drawing.Point(152, 218);
            this.cbxDtmf.Name = "cbxDtmf";
            this.cbxDtmf.Size = new System.Drawing.Size(120, 20);
            this.cbxDtmf.TabIndex = 9;
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
            this.label3.Location = new System.Drawing.Point(12, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "dtmf(按键)发送方式";
            // 
            // btnSelectDtmf
            // 
            this.btnSelectDtmf.Location = new System.Drawing.Point(197, 244);
            this.btnSelectDtmf.Name = "btnSelectDtmf";
            this.btnSelectDtmf.Size = new System.Drawing.Size(75, 23);
            this.btnSelectDtmf.TabIndex = 11;
            this.btnSelectDtmf.Tag = "diallimit_common_dtmf_select";
            this.btnSelectDtmf.Text = "选中生效";
            this.btnSelectDtmf.UseVisualStyleBackColor = true;
            this.btnSelectDtmf.Click += new System.EventHandler(this.btnDtmf_Click);
            // 
            // btnAllDtmf
            // 
            this.btnAllDtmf.Location = new System.Drawing.Point(116, 244);
            this.btnAllDtmf.Name = "btnAllDtmf";
            this.btnAllDtmf.Size = new System.Drawing.Size(75, 23);
            this.btnAllDtmf.TabIndex = 10;
            this.btnAllDtmf.Tag = "diallimit_common_dtmf_all";
            this.btnAllDtmf.Text = "全部生效";
            this.btnAllDtmf.UseVisualStyleBackColor = true;
            this.btnAllDtmf.Click += new System.EventHandler(this.btnDtmf_Click);
            // 
            // cbxCommon
            // 
            this.cbxCommon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCommon.FormattingEnabled = true;
            this.cbxCommon.Location = new System.Drawing.Point(152, 289);
            this.cbxCommon.Name = "cbxCommon";
            this.cbxCommon.Size = new System.Drawing.Size(120, 20);
            this.cbxCommon.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 292);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "号码类别";
            // 
            // btnAllCommon
            // 
            this.btnAllCommon.Location = new System.Drawing.Point(116, 315);
            this.btnAllCommon.Name = "btnAllCommon";
            this.btnAllCommon.Size = new System.Drawing.Size(75, 23);
            this.btnAllCommon.TabIndex = 14;
            this.btnAllCommon.Tag = "diallimit_common_share_all";
            this.btnAllCommon.Text = "全部生效";
            this.btnAllCommon.UseVisualStyleBackColor = true;
            this.btnAllCommon.Click += new System.EventHandler(this.btnCommon_Click);
            // 
            // btnSelectCommon
            // 
            this.btnSelectCommon.Location = new System.Drawing.Point(197, 315);
            this.btnSelectCommon.Name = "btnSelectCommon";
            this.btnSelectCommon.Size = new System.Drawing.Size(75, 23);
            this.btnSelectCommon.TabIndex = 15;
            this.btnSelectCommon.Tag = "diallimit_common_share_select";
            this.btnSelectCommon.Text = "选中生效";
            this.btnSelectCommon.UseVisualStyleBackColor = true;
            this.btnSelectCommon.Click += new System.EventHandler(this.btnCommon_Click);
            // 
            // btnUpdateShare
            // 
            this.btnUpdateShare.Location = new System.Drawing.Point(12, 315);
            this.btnUpdateShare.Name = "btnUpdateShare";
            this.btnUpdateShare.Size = new System.Drawing.Size(98, 23);
            this.btnUpdateShare.TabIndex = 13;
            this.btnUpdateShare.Tag = "diallimit_common_share_update";
            this.btnUpdateShare.Text = "更新共享号码池";
            this.btnUpdateShare.UseVisualStyleBackColor = true;
            this.btnUpdateShare.Click += new System.EventHandler(this.btnUpdateShare_Click);
            // 
            // btnLimitCallRuleAll
            // 
            this.btnLimitCallRuleAll.Location = new System.Drawing.Point(116, 386);
            this.btnLimitCallRuleAll.Name = "btnLimitCallRuleAll";
            this.btnLimitCallRuleAll.Size = new System.Drawing.Size(75, 23);
            this.btnLimitCallRuleAll.TabIndex = 17;
            this.btnLimitCallRuleAll.Tag = "diallimit_common_limitcallrule_all";
            this.btnLimitCallRuleAll.Text = "全部生效";
            this.btnLimitCallRuleAll.UseVisualStyleBackColor = true;
            this.btnLimitCallRuleAll.Click += new System.EventHandler(this.btnLimitCallRule_Click);
            // 
            // btnLimitCallRuleSelect
            // 
            this.btnLimitCallRuleSelect.Location = new System.Drawing.Point(197, 386);
            this.btnLimitCallRuleSelect.Name = "btnLimitCallRuleSelect";
            this.btnLimitCallRuleSelect.Size = new System.Drawing.Size(75, 23);
            this.btnLimitCallRuleSelect.TabIndex = 18;
            this.btnLimitCallRuleSelect.Tag = "diallimit_common_limitcallrule_select";
            this.btnLimitCallRuleSelect.Text = "选中生效";
            this.btnLimitCallRuleSelect.UseVisualStyleBackColor = true;
            this.btnLimitCallRuleSelect.Click += new System.EventHandler(this.btnLimitCallRule_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 363);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "呼入规则";
            // 
            // cbxLimitCallRule
            // 
            this.cbxLimitCallRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLimitCallRule.FormattingEnabled = true;
            this.cbxLimitCallRule.Location = new System.Drawing.Point(152, 360);
            this.cbxLimitCallRule.Name = "cbxLimitCallRule";
            this.cbxLimitCallRule.Size = new System.Drawing.Size(120, 20);
            this.cbxLimitCallRule.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 37;
            this.label6.Text = "本地号码加拨前缀";
            // 
            // txtDialLocalPrefix
            // 
            this.txtDialLocalPrefix.Location = new System.Drawing.Point(152, 93);
            this.txtDialLocalPrefix.Name = "txtDialLocalPrefix";
            this.txtDialLocalPrefix.Size = new System.Drawing.Size(120, 21);
            this.txtDialLocalPrefix.TabIndex = 4;
            // 
            // btnPrefixDealFlagAll
            // 
            this.btnPrefixDealFlagAll.Location = new System.Drawing.Point(116, 457);
            this.btnPrefixDealFlagAll.Name = "btnPrefixDealFlagAll";
            this.btnPrefixDealFlagAll.Size = new System.Drawing.Size(75, 23);
            this.btnPrefixDealFlagAll.TabIndex = 20;
            this.btnPrefixDealFlagAll.Tag = "diallimit_common_prefixdealflag_all";
            this.btnPrefixDealFlagAll.Text = "全部生效";
            this.btnPrefixDealFlagAll.UseVisualStyleBackColor = true;
            this.btnPrefixDealFlagAll.Click += new System.EventHandler(this.btnPrefixDealFlag_Click);
            // 
            // btnPrefixDealFlagSelect
            // 
            this.btnPrefixDealFlagSelect.Location = new System.Drawing.Point(197, 457);
            this.btnPrefixDealFlagSelect.Name = "btnPrefixDealFlagSelect";
            this.btnPrefixDealFlagSelect.Size = new System.Drawing.Size(75, 23);
            this.btnPrefixDealFlagSelect.TabIndex = 21;
            this.btnPrefixDealFlagSelect.Tag = "diallimit_common_prefixdealflag_select";
            this.btnPrefixDealFlagSelect.Text = "选中生效";
            this.btnPrefixDealFlagSelect.UseVisualStyleBackColor = true;
            this.btnPrefixDealFlagSelect.Click += new System.EventHandler(this.btnPrefixDealFlag_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 434);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 12);
            this.label9.TabIndex = 41;
            this.label9.Text = "自动根据区号加拨前缀";
            // 
            // cbxPrefixDealFlag
            // 
            this.cbxPrefixDealFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPrefixDealFlag.FormattingEnabled = true;
            this.cbxPrefixDealFlag.Location = new System.Drawing.Point(152, 431);
            this.cbxPrefixDealFlag.Name = "cbxPrefixDealFlag";
            this.cbxPrefixDealFlag.Size = new System.Drawing.Size(120, 20);
            this.cbxPrefixDealFlag.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 502);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 22;
            this.label10.Text = "呼叫内转配置";
            // 
            // btnInlimit_2Reload
            // 
            this.btnInlimit_2Reload.Location = new System.Drawing.Point(197, 497);
            this.btnInlimit_2Reload.Name = "btnInlimit_2Reload";
            this.btnInlimit_2Reload.Size = new System.Drawing.Size(75, 23);
            this.btnInlimit_2Reload.TabIndex = 23;
            this.btnInlimit_2Reload.Tag = "diallimit_common_inlimit_2_reload";
            this.btnInlimit_2Reload.Text = "重载";
            this.btnInlimit_2Reload.UseVisualStyleBackColor = true;
            this.btnInlimit_2Reload.Click += new System.EventHandler(this.btnInlimit_2Reload_Click);
            // 
            // btnF99d999OK
            // 
            this.btnF99d999OK.Location = new System.Drawing.Point(197, 526);
            this.btnF99d999OK.Name = "btnF99d999OK";
            this.btnF99d999OK.Size = new System.Drawing.Size(75, 23);
            this.btnF99d999OK.TabIndex = 26;
            this.btnF99d999OK.Tag = "diallimit_common_f99d999_ok";
            this.btnF99d999OK.Text = "首发确定";
            this.btnF99d999OK.UseVisualStyleBackColor = true;
            this.btnF99d999OK.Click += new System.EventHandler(this.btnF99d999OK_Click);
            // 
            // btnF99d999Reset
            // 
            this.btnF99d999Reset.Location = new System.Drawing.Point(116, 526);
            this.btnF99d999Reset.Name = "btnF99d999Reset";
            this.btnF99d999Reset.Size = new System.Drawing.Size(75, 23);
            this.btnF99d999Reset.TabIndex = 25;
            this.btnF99d999Reset.Tag = "diallimit_common_f99d999_reset";
            this.btnF99d999Reset.Text = "首发重置";
            this.btnF99d999Reset.UseVisualStyleBackColor = true;
            this.btnF99d999Reset.Click += new System.EventHandler(this.btnF99d999Reset_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 531);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 24;
            this.label11.Text = "设置首发号码";
            // 
            // cmnset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 558);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnF99d999Reset);
            this.Controls.Add(this.btnF99d999OK);
            this.Controls.Add(this.btnInlimit_2Reload);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnPrefixDealFlagAll);
            this.Controls.Add(this.btnPrefixDealFlagSelect);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbxPrefixDealFlag);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDialLocalPrefix);
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDialLocalPrefix;
        private System.Windows.Forms.Button btnPrefixDealFlagAll;
        private System.Windows.Forms.Button btnPrefixDealFlagSelect;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxPrefixDealFlag;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnInlimit_2Reload;
        private System.Windows.Forms.Button btnF99d999OK;
        private System.Windows.Forms.Button btnF99d999Reset;
        private System.Windows.Forms.Label label11;
    }
}
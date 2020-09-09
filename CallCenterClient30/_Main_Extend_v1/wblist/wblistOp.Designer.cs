namespace CenoCC
{
    partial class wblistOp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(routeOp));
            this.txtRname = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRnumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrdernum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.treeView = new System.Windows.Forms.TreeView();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbxCtype = new System.Windows.Forms.ComboBox();
            this.cbxRtype = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtRname
            // 
            this.txtRname.Location = new System.Drawing.Point(152, 12);
            this.txtRname.Name = "txtRname";
            this.txtRname.Size = new System.Drawing.Size(120, 21);
            this.txtRname.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "路由名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "号码表达式";
            // 
            // txtRnumber
            // 
            this.txtRnumber.Location = new System.Drawing.Point(152, 39);
            this.txtRnumber.Name = "txtRnumber";
            this.txtRnumber.Size = new System.Drawing.Size(120, 21);
            this.txtRnumber.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "唯一索引(不可重复)";
            // 
            // txtOrdernum
            // 
            this.txtOrdernum.Location = new System.Drawing.Point(152, 66);
            this.txtOrdernum.Name = "txtOrdernum";
            this.txtOrdernum.Size = new System.Drawing.Size(120, 21);
            this.txtOrdernum.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "路由类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "作用范围";
            // 
            // treeView
            // 
            this.treeView.CheckBoxes = true;
            this.treeView.Location = new System.Drawing.Point(14, 147);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(258, 380);
            this.treeView.TabIndex = 11;
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(197, 533);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "添加";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbxCtype
            // 
            this.cbxCtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCtype.FormattingEnabled = true;
            this.cbxCtype.Location = new System.Drawing.Point(152, 93);
            this.cbxCtype.Name = "cbxCtype";
            this.cbxCtype.Size = new System.Drawing.Size(120, 20);
            this.cbxCtype.TabIndex = 8;
            // 
            // cbxRtype
            // 
            this.cbxRtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRtype.FormattingEnabled = true;
            this.cbxRtype.Location = new System.Drawing.Point(152, 119);
            this.cbxRtype.Name = "cbxRtype";
            this.cbxRtype.Size = new System.Drawing.Size(120, 20);
            this.cbxRtype.TabIndex = 10;
            this.cbxRtype.SelectedIndexChanged += new System.EventHandler(this.cbxRtype_SelectedIndexChanged);
            // 
            // routeOp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 563);
            this.Controls.Add(this.cbxRtype);
            this.Controls.Add(this.cbxCtype);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOrdernum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRnumber);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtRname);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "routeOp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "路由添加";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRname;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRnumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOrdernum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbxCtype;
        private System.Windows.Forms.ComboBox cbxRtype;
    }
}
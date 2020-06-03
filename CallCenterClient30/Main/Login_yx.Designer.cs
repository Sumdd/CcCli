using System.Windows.Forms;
using System.Drawing;
using System;
using Common;
namespace CenoCC
{
    partial class Login_yx
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_yx));
            this.ResetPsw_Lbl = new System.Windows.Forms.LinkLabel();
            this.ErrorInfo_Lab = new System.Windows.Forms.Label();
            this.RemindFlag_CB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Login_Name_Txt = new System.Windows.Forms.TextBox();
            this.Cancel_Btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Confirm_Btn = new Common.TimerButton();
            this.Login_Password_Txt = new System.Windows.Forms.TextBox();
            this.Version_Lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ResetPsw_Lbl
            // 
            this.ResetPsw_Lbl.AutoSize = true;
            this.ResetPsw_Lbl.BackColor = System.Drawing.Color.Transparent;
            this.ResetPsw_Lbl.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.ResetPsw_Lbl.LinkColor = System.Drawing.Color.Black;
            this.ResetPsw_Lbl.Location = new System.Drawing.Point(302, 384);
            this.ResetPsw_Lbl.Name = "ResetPsw_Lbl";
            this.ResetPsw_Lbl.Size = new System.Drawing.Size(53, 12);
            this.ResetPsw_Lbl.TabIndex = 32;
            this.ResetPsw_Lbl.TabStop = true;
            this.ResetPsw_Lbl.Text = "忘记密码";
            this.ResetPsw_Lbl.Visible = false;
            this.ResetPsw_Lbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ResetPsw_Lbl_LinkClicked);
            // 
            // ErrorInfo_Lab
            // 
            this.ErrorInfo_Lab.AutoSize = true;
            this.ErrorInfo_Lab.BackColor = System.Drawing.Color.Transparent;
            this.ErrorInfo_Lab.ForeColor = System.Drawing.Color.Red;
            this.ErrorInfo_Lab.Location = new System.Drawing.Point(140, 410);
            this.ErrorInfo_Lab.Name = "ErrorInfo_Lab";
            this.ErrorInfo_Lab.Size = new System.Drawing.Size(59, 12);
            this.ErrorInfo_Lab.TabIndex = 31;
            this.ErrorInfo_Lab.Text = "         ";
            // 
            // RemindFlag_CB
            // 
            this.RemindFlag_CB.AutoSize = true;
            this.RemindFlag_CB.BackColor = System.Drawing.Color.Transparent;
            this.RemindFlag_CB.Location = new System.Drawing.Point(285, 349);
            this.RemindFlag_CB.Name = "RemindFlag_CB";
            this.RemindFlag_CB.Size = new System.Drawing.Size(72, 16);
            this.RemindFlag_CB.TabIndex = 27;
            this.RemindFlag_CB.Text = "记住账号";
            this.RemindFlag_CB.UseVisualStyleBackColor = false;
            this.RemindFlag_CB.CheckedChanged += new System.EventHandler(this.RemindFlag_CB_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(88, 350);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 29;
            this.label2.Text = "账号：";
            // 
            // Login_Name_Txt
            // 
            this.Login_Name_Txt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Login_Name_Txt.Location = new System.Drawing.Point(135, 345);
            this.Login_Name_Txt.Name = "Login_Name_Txt";
            this.Login_Name_Txt.Size = new System.Drawing.Size(135, 21);
            this.Login_Name_Txt.TabIndex = 24;
            this.Login_Name_Txt.TextChanged += new System.EventHandler(this.Login_Name_Txt_TextChanged);
            // 
            // Cancel_Btn
            // 
            this.Cancel_Btn.Location = new System.Drawing.Point(285, 428);
            this.Cancel_Btn.Name = "Cancel_Btn";
            this.Cancel_Btn.Size = new System.Drawing.Size(75, 33);
            this.Cancel_Btn.TabIndex = 28;
            this.Cancel_Btn.Text = "关闭";
            this.Cancel_Btn.UseVisualStyleBackColor = true;
            this.Cancel_Btn.Click += new System.EventHandler(this.Cancel_Btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(88, 384);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "密码：";
            // 
            // Confirm_Btn
            // 
            this.Confirm_Btn.Location = new System.Drawing.Point(90, 428);
            this.Confirm_Btn.Name = "Confirm_Btn";
            this.Confirm_Btn.Size = new System.Drawing.Size(180, 33);
            this.Confirm_Btn.TabIndex = 26;
            this.Confirm_Btn.Text = "登录";
            this.Confirm_Btn.UseVisualStyleBackColor = true;
            this.Confirm_Btn.Click += new System.EventHandler(this.Confirm_Btn_Click);
            // 
            // Login_Password_Txt
            // 
            this.Login_Password_Txt.Location = new System.Drawing.Point(135, 381);
            this.Login_Password_Txt.Name = "Login_Password_Txt";
            this.Login_Password_Txt.PasswordChar = '*';
            this.Login_Password_Txt.Size = new System.Drawing.Size(135, 21);
            this.Login_Password_Txt.TabIndex = 25;
            this.Login_Password_Txt.TextChanged += new System.EventHandler(this.Login_Password_Txt_TextChanged);
            this.Login_Password_Txt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Login_Password_Txt_KeyPress);
            // 
            // Version_Lbl
            // 
            this.Version_Lbl.AutoSize = true;
            this.Version_Lbl.BackColor = System.Drawing.Color.Transparent;
            this.Version_Lbl.Location = new System.Drawing.Point(70, 314);
            this.Version_Lbl.Name = "Version_Lbl";
            this.Version_Lbl.Size = new System.Drawing.Size(47, 12);
            this.Version_Lbl.TabIndex = 1;
            this.Version_Lbl.Text = "3.0.0.0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(11, 314);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "版本号：";
            // 
            // Login_yx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.BackgroundImage = global::CenoCC.Properties.Resources.yx;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(450, 472);
            this.Controls.Add(this.ResetPsw_Lbl);
            this.Controls.Add(this.ErrorInfo_Lab);
            this.Controls.Add(this.RemindFlag_CB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Login_Name_Txt);
            this.Controls.Add(this.Cancel_Btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Confirm_Btn);
            this.Controls.Add(this.Login_Password_Txt);
            this.Controls.Add(this.Version_Lbl);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login_yx";
            this.Text = "宇信";
            this.Load += new System.EventHandler(this.Login_Frm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_Frm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Login_Frm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Login_Frm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.LinkLabel ResetPsw_Lbl;
        private System.Windows.Forms.Label ErrorInfo_Lab;
        private System.Windows.Forms.CheckBox RemindFlag_CB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Login_Name_Txt;
        private System.Windows.Forms.Button Cancel_Btn;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox Login_Password_Txt;
		private Common.TimerButton Confirm_Btn;


		private void Login_Frm_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mouseOff = new Point(-e.X, -e.Y);
				leftFlag = true;
			}
		}

		private void Login_Frm_MouseMove(object sender, MouseEventArgs e)
		{
			if (leftFlag)
			{
				Point mouseSet = Control.MousePosition;
				mouseSet.Offset(mouseOff.X, mouseOff.Y);
				Location = mouseSet;
			}
		}

		private void Login_Frm_MouseUp(object sender, MouseEventArgs e)
		{
			if (leftFlag)
			{
				leftFlag = false;
			}
		}

		private void Login_Frm_Load(object sender, EventArgs e)
		{
			FormClass.FormCenter(this);
			FormClass.AnimateWindow(this.Handle, 1000, FormClass.AW_BLEND);

		}

        private Label Version_Lbl;
        private Label label1;
    }
}


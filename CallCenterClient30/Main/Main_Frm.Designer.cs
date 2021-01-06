using System.Drawing;
using System.Windows.Forms;
using System;
using Common;
using WebBrowser;
using System.Runtime.InteropServices;

namespace CenoCC
{
	partial class Main_Frm
	{
        public static FormWindowState lastFormWindowState = FormWindowState.Normal;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Frm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.Address_Bar_Pan = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.Address_Url_Bar_Pan = new System.Windows.Forms.Panel();
            this.UrlAddress_Txt = new System.Windows.Forms.TextBox();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.Navigation_Bar_Pan = new System.Windows.Forms.Panel();
            this.goForward = new System.Windows.Forms.Panel();
            this.pHome = new System.Windows.Forms.Panel();
            this.pRefresh = new System.Windows.Forms.Panel();
            this.goBack = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.StatusTxt_Lbl = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.TOP = new System.Windows.Forms.Panel();
            this.Title_Bar_Pan = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.panel19 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Icon_Pan = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.Address_Bar_Pan.SuspendLayout();
            this.panel13.SuspendLayout();
            this.Address_Url_Bar_Pan.SuspendLayout();
            this.Navigation_Bar_Pan.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel10.SuspendLayout();
            this.TOP.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel17);
            this.panel2.Controls.Add(this.Address_Bar_Pan);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1210, 689);
            this.panel2.TabIndex = 2;
            // 
            // panel17
            // 
            this.panel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel17.Location = new System.Drawing.Point(10, 36);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(1189, 653);
            this.panel17.TabIndex = 3;
            // 
            // Address_Bar_Pan
            // 
            this.Address_Bar_Pan.BackgroundImage = global::CenoCC.Properties.Resources.Center_Top_bg;
            this.Address_Bar_Pan.Controls.Add(this.panel13);
            this.Address_Bar_Pan.Controls.Add(this.Navigation_Bar_Pan);
            this.Address_Bar_Pan.Dock = System.Windows.Forms.DockStyle.Top;
            this.Address_Bar_Pan.Location = new System.Drawing.Point(10, 0);
            this.Address_Bar_Pan.Margin = new System.Windows.Forms.Padding(0);
            this.Address_Bar_Pan.Name = "Address_Bar_Pan";
            this.Address_Bar_Pan.Size = new System.Drawing.Size(1189, 36);
            this.Address_Bar_Pan.TabIndex = 2;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.Address_Url_Bar_Pan);
            this.panel13.Controls.Add(this.panel15);
            this.panel13.Controls.Add(this.panel14);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(115, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(1074, 36);
            this.panel13.TabIndex = 1;
            // 
            // Address_Url_Bar_Pan
            // 
            this.Address_Url_Bar_Pan.BackgroundImage = global::CenoCC.Properties.Resources.Center_Top_Right_Center;
            this.Address_Url_Bar_Pan.Controls.Add(this.UrlAddress_Txt);
            this.Address_Url_Bar_Pan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Address_Url_Bar_Pan.Location = new System.Drawing.Point(21, 0);
            this.Address_Url_Bar_Pan.Name = "Address_Url_Bar_Pan";
            this.Address_Url_Bar_Pan.Padding = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.Address_Url_Bar_Pan.Size = new System.Drawing.Size(993, 36);
            this.Address_Url_Bar_Pan.TabIndex = 2;
            // 
            // UrlAddress_Txt
            // 
            this.UrlAddress_Txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.UrlAddress_Txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UrlAddress_Txt.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UrlAddress_Txt.Location = new System.Drawing.Point(5, 10);
            this.UrlAddress_Txt.Name = "UrlAddress_Txt";
            this.UrlAddress_Txt.Size = new System.Drawing.Size(988, 16);
            this.UrlAddress_Txt.TabIndex = 0;
            this.UrlAddress_Txt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UrlAddress_Txt_KeyPress);
            // 
            // panel15
            // 
            this.panel15.BackgroundImage = global::CenoCC.Properties.Resources.Center_Top_Right_Right;
            this.panel15.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel15.Location = new System.Drawing.Point(1014, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(60, 36);
            this.panel15.TabIndex = 1;
            this.panel15.MouseLeave += new System.EventHandler(this.panel15_MouseLeave);
            this.panel15.MouseHover += new System.EventHandler(this.panel15_MouseHover);
            // 
            // panel14
            // 
            this.panel14.BackgroundImage = global::CenoCC.Properties.Resources.Center_Top_Right_Left;
            this.panel14.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(21, 36);
            this.panel14.TabIndex = 0;
            // 
            // Navigation_Bar_Pan
            // 
            this.Navigation_Bar_Pan.BackgroundImage = global::CenoCC.Properties.Resources.Center_Top_Left;
            this.Navigation_Bar_Pan.Controls.Add(this.goForward);
            this.Navigation_Bar_Pan.Controls.Add(this.pHome);
            this.Navigation_Bar_Pan.Controls.Add(this.pRefresh);
            this.Navigation_Bar_Pan.Controls.Add(this.goBack);
            this.Navigation_Bar_Pan.Dock = System.Windows.Forms.DockStyle.Left;
            this.Navigation_Bar_Pan.Location = new System.Drawing.Point(0, 0);
            this.Navigation_Bar_Pan.Name = "Navigation_Bar_Pan";
            this.Navigation_Bar_Pan.Size = new System.Drawing.Size(115, 36);
            this.Navigation_Bar_Pan.TabIndex = 0;
            // 
            // goForward
            // 
            this.goForward.BackColor = System.Drawing.Color.Transparent;
            this.goForward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.goForward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.goForward.Location = new System.Drawing.Point(33, 6);
            this.goForward.Name = "goForward";
            this.goForward.Size = new System.Drawing.Size(24, 24);
            this.goForward.TabIndex = 1;
            this.goForward.Click += new System.EventHandler(this.goForward_Click);
            // 
            // pHome
            // 
            this.pHome.BackColor = System.Drawing.Color.Transparent;
            this.pHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pHome.Location = new System.Drawing.Point(90, 6);
            this.pHome.Name = "pHome";
            this.pHome.Size = new System.Drawing.Size(24, 24);
            this.pHome.TabIndex = 1;
            this.pHome.Click += new System.EventHandler(this.pHome_Click);
            // 
            // pRefresh
            // 
            this.pRefresh.BackColor = System.Drawing.Color.Transparent;
            this.pRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pRefresh.Location = new System.Drawing.Point(62, 6);
            this.pRefresh.Name = "pRefresh";
            this.pRefresh.Size = new System.Drawing.Size(24, 24);
            this.pRefresh.TabIndex = 0;
            this.pRefresh.Click += new System.EventHandler(this.pRefresh_Click);
            // 
            // goBack
            // 
            this.goBack.BackColor = System.Drawing.Color.Transparent;
            this.goBack.BackgroundImage = global::CenoCC.Properties.Resources.Center_Top_Left_Back_Hover;
            this.goBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.goBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.goBack.Location = new System.Drawing.Point(6, 6);
            this.goBack.Name = "goBack";
            this.goBack.Size = new System.Drawing.Size(24, 24);
            this.goBack.TabIndex = 0;
            this.goBack.Click += new System.EventHandler(this.goBack_Click);
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = global::CenoCC.Properties.Resources.Center_Right;
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(1199, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(11, 689);
            this.panel7.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::CenoCC.Properties.Resources.Center_Left;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(10, 689);
            this.panel6.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel10);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 735);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1210, 16);
            this.panel1.TabIndex = 1;
            // 
            // panel10
            // 
            this.panel10.BackgroundImage = global::CenoCC.Properties.Resources.Bottom_Center;
            this.panel10.Controls.Add(this.StatusTxt_Lbl);
            this.panel10.Controls.Add(this.progressBar1);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(15, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1175, 16);
            this.panel10.TabIndex = 2;
            // 
            // StatusTxt_Lbl
            // 
            this.StatusTxt_Lbl.AutoSize = true;
            this.StatusTxt_Lbl.BackColor = System.Drawing.Color.Silver;
            this.StatusTxt_Lbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.StatusTxt_Lbl.Location = new System.Drawing.Point(0, 0);
            this.StatusTxt_Lbl.Name = "StatusTxt_Lbl";
            this.StatusTxt_Lbl.Size = new System.Drawing.Size(0, 12);
            this.StatusTxt_Lbl.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 14);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1175, 2);
            this.progressBar1.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.BackgroundImage = global::CenoCC.Properties.Resources.Bottom_Right;
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(1190, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(20, 16);
            this.panel9.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.BackgroundImage = global::CenoCC.Properties.Resources.Bottom_Left;
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(15, 16);
            this.panel8.TabIndex = 0;
            // 
            // TOP
            // 
            this.TOP.Controls.Add(this.Title_Bar_Pan);
            this.TOP.Controls.Add(this.panel4);
            this.TOP.Controls.Add(this.panel3);
            this.TOP.Dock = System.Windows.Forms.DockStyle.Top;
            this.TOP.Location = new System.Drawing.Point(0, 0);
            this.TOP.Name = "TOP";
            this.TOP.Size = new System.Drawing.Size(1210, 46);
            this.TOP.TabIndex = 0;
            // 
            // Title_Bar_Pan
            // 
            this.Title_Bar_Pan.BackgroundImage = global::CenoCC.Properties.Resources.Top_Center;
            this.Title_Bar_Pan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Title_Bar_Pan.Location = new System.Drawing.Point(55, 0);
            this.Title_Bar_Pan.Name = "Title_Bar_Pan";
            this.Title_Bar_Pan.Padding = new System.Windows.Forms.Padding(0, 21, 0, 0);
            this.Title_Bar_Pan.Size = new System.Drawing.Size(1051, 46);
            this.Title_Bar_Pan.TabIndex = 2;
            this.Title_Bar_Pan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseDown);
            this.Title_Bar_Pan.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseMove);
            this.Title_Bar_Pan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseUp);
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::CenoCC.Properties.Resources.Top_Right;
            this.panel4.Controls.Add(this.panel20);
            this.panel4.Controls.Add(this.panel19);
            this.panel4.Controls.Add(this.panel18);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(1106, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(104, 46);
            this.panel4.TabIndex = 1;
            // 
            // panel20
            // 
            this.panel20.BackgroundImage = global::CenoCC.Properties.Resources.Top_Right_CloseBtn;
            this.panel20.Location = new System.Drawing.Point(59, 9);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(29, 30);
            this.panel20.TabIndex = 2;
            this.panel20.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel20_MouseClick);
            this.panel20.MouseEnter += new System.EventHandler(this.panel20_MouseEnter);
            this.panel20.MouseLeave += new System.EventHandler(this.panel20_MouseLeave);
            // 
            // panel19
            // 
            this.panel19.BackgroundImage = global::CenoCC.Properties.Resources.Top_Right_MaxBtn;
            this.panel19.Location = new System.Drawing.Point(29, 9);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(30, 30);
            this.panel19.TabIndex = 1;
            this.panel19.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel19_MouseClick);
            this.panel19.MouseEnter += new System.EventHandler(this.panel19_MouseEnter);
            this.panel19.MouseLeave += new System.EventHandler(this.panel19_MouseLeave);
            // 
            // panel18
            // 
            this.panel18.BackgroundImage = global::CenoCC.Properties.Resources.Top_Right_MinBtn;
            this.panel18.Location = new System.Drawing.Point(0, 9);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(29, 30);
            this.panel18.TabIndex = 0;
            this.panel18.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel18_MouseClick);
            this.panel18.MouseEnter += new System.EventHandler(this.panel18_MouseEnter);
            this.panel18.MouseLeave += new System.EventHandler(this.panel18_MouseLeave);
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::CenoCC.Properties.Resources.Top_Left;
            this.panel3.Controls.Add(this.Icon_Pan);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(55, 46);
            this.panel3.TabIndex = 0;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseDown);
            this.panel3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseMove);
            this.panel3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseUp);
            // 
            // Icon_Pan
            // 
            this.Icon_Pan.BackgroundImage = global::CenoCC.Properties.Resources.Tubiao;
            this.Icon_Pan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Icon_Pan.Location = new System.Drawing.Point(22, 20);
            this.Icon_Pan.Name = "Icon_Pan";
            this.Icon_Pan.Size = new System.Drawing.Size(22, 22);
            this.Icon_Pan.TabIndex = 0;
            this.Icon_Pan.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Icon_Pan_MouseDoubleClick);
            // 
            // Main_Frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CenoCC.Properties.Resources.WebBrowser_Bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1210, 751);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TOP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main_Frm";
            this.Text = "Main_Frm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_Frm_FormClosed);
            this.Load += new System.EventHandler(this.Main_Frm_Load);
            this.panel2.ResumeLayout(false);
            this.Address_Bar_Pan.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.Address_Url_Bar_Pan.ResumeLayout(false);
            this.Address_Url_Bar_Pan.PerformLayout();
            this.Navigation_Bar_Pan.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.TOP.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel TOP;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel Title_Bar_Pan;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Panel panel10;
		private System.Windows.Forms.Panel Address_Bar_Pan;
		private System.Windows.Forms.Panel Navigation_Bar_Pan;
		private System.Windows.Forms.Panel panel13;
		private System.Windows.Forms.Panel panel14;
		private System.Windows.Forms.Panel panel15;
		private System.Windows.Forms.Panel Address_Url_Bar_Pan;
		private System.Windows.Forms.Panel panel17;
		private System.Windows.Forms.Panel Icon_Pan;
		private System.Windows.Forms.TextBox UrlAddress_Txt;
		private System.Windows.Forms.Panel panel18;
		private System.Windows.Forms.Panel panel19;
		private System.Windows.Forms.Panel panel20;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label StatusTxt_Lbl;

		#region Form Initalize

		private void Icon_Pan_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Close();
		}


		private void panel18_MouseEnter(object sender, EventArgs e)
		{
			this.panel18.BackgroundImage = global::CenoCC.Properties.Resources.Top_Right_MinBtn_Hover;
		}

		private void panel18_MouseLeave(object sender, EventArgs e)
		{
			this.panel18.BackgroundImage = global::CenoCC.Properties.Resources.Top_Right_MinBtn;

		}

		private void panel19_MouseEnter(object sender, EventArgs e)
		{
			this.panel19.BackgroundImage = global::CenoCC.Properties.Resources.Top_Right_MaxBtn_Hover;
		}

		private void panel19_MouseLeave(object sender, EventArgs e)
		{
			this.panel19.BackgroundImage = global::CenoCC.Properties.Resources.Top_Right_MaxBtn;
		}

		private void panel20_MouseEnter(object sender, EventArgs e)
		{
			this.panel20.BackgroundImage = global::CenoCC.Properties.Resources.Top_Right_CloseBtn_Hover;
		}

		private void panel20_MouseLeave(object sender, EventArgs e)
		{
			this.panel20.BackgroundImage = global::CenoCC.Properties.Resources.Top_Right_CloseBtn;
		}

		private void panel18_MouseClick(object sender, MouseEventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}

		private void panel19_MouseClick(object sender, MouseEventArgs e)
		{
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                Main_Frm.lastFormWindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                Main_Frm.lastFormWindowState = FormWindowState.Maximized;
            }
		}

		private void panel20_MouseClick(object sender, MouseEventArgs e)
		{
			this.Close();
		}

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void panel3_MouseDown(object sender, MouseEventArgs e)
		{
            if (e.Button == MouseButtons.Left && this.WindowState != FormWindowState.Maximized)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x0112, 0xF012, 0);
            }
		}

		private void panel3_MouseMove(object sender, MouseEventArgs e)
		{
		}

		private void panel3_MouseUp(object sender, MouseEventArgs e)
		{
		}

		private void Main_Frm_Load(object sender, EventArgs e)
		{
			this.Size = Screen.PrimaryScreen.WorkingArea.Size;
			this.Top = this.Left = 0;
		}

		private void Main_Frm_FormClosed(object sender, FormClosedEventArgs e)
		{
			MinChat.MainBrowserForm = null;
		}

		private void UrlAddress_Txt_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
			{
				try
				{
                    string m_sUrlAddressString = this.UrlAddress_Txt.Text;
					((ExtendedWebBrowser)this.panel17.Controls[0]).Navigate(m_sUrlAddressString);
				}
				catch (Exception ex)
				{
					LogFile.Write(typeof(Main_Frm), LOGLEVEL.ERROR, "open url page error", ex);
				}
				e.Handled = true;
			}
		}


		#endregion

		private Panel goBack;
        private Panel pRefresh;
        private Panel pHome;
        private Panel goForward;
    }
}
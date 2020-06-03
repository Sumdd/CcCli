using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace WebKitBrowserTest
{
	public class NavigationBar : UserControl
	{
		private IContainer components;
		private Button buttonBack;
		private Button buttonFwd;
		private Button buttonRefresh;
		private Button buttonStop;
		private Button buttonHome;
		private ComboBox comboBoxAddress;
		private Button button1;

		public event Default Back;
		//{
		//    [MethodImpl(MethodImplOptions.Synchronized)]
		//    add
		//    {
		//        this.Back = (Default)Delegate.Combine(this.Back, value);
		//    }
		//    [MethodImpl(MethodImplOptions.Synchronized)]
		//    remove
		//    {
		//        this.Back = (Default)Delegate.Remove(this.Back, value);
		//    }
		//}
		public event Default Forward;
		//{
		//    [MethodImpl(MethodImplOptions.Synchronized)]
		//    add
		//    {
		//        this.Forward = (Default)Delegate.Combine(this.Forward, value);
		//    }
		//    [MethodImpl(MethodImplOptions.Synchronized)]
		//    remove
		//    {
		//        this.Forward = (Default)Delegate.Remove(this.Forward, value);
		//    }
		//}
		public new event Default Refresh;
		//{
		//    [MethodImpl(MethodImplOptions.Synchronized)]
		//    add
		//    {
		//        this.Refresh = (Default)Delegate.Combine(this.Refresh, value);
		//    }
		//    [MethodImpl(MethodImplOptions.Synchronized)]
		//    remove
		//    {
		//        this.Refresh = (Default)Delegate.Remove(this.Refresh, value);
		//    }
		//}
		public event Default Stop;
		//{
		//    [MethodImpl(MethodImplOptions.Synchronized)]
		//    add
		//    {
		//        this.Stop = (Default)Delegate.Combine(this.Stop, value);
		//    }
		//    [MethodImpl(MethodImplOptions.Synchronized)]
		//    remove
		//    {
		//        this.Stop = (Default)Delegate.Remove(this.Stop, value);
		//    }
		//}
		public event Default Home;
		//{
		//    [MethodImpl(MethodImplOptions.Synchronized)]
		//    add
		//    {
		//        this.Home = (Default)Delegate.Combine(this.Home, value);
		//    }
		//    [MethodImpl(MethodImplOptions.Synchronized)]
		//    remove
		//    {
		//        this.Home = (Default)Delegate.Remove(this.Home, value);
		//    }
		//}
		public event Default Go;
		//{
		//    [MethodImpl(MethodImplOptions.Synchronized)]
		//    add
		//    {
		//        this.Go = (Default)Delegate.Combine(this.Go, value);
		//    }
		//    [MethodImpl(MethodImplOptions.Synchronized)]
		//    remove
		//    {
		//        this.Go = (Default)Delegate.Remove(this.Go, value);
		//    }
		//}
		public string UrlText
		{
			get
			{
				return this.comboBoxAddress.Text;
			}
			set
			{
				this.comboBoxAddress.Text = value;
			}
		}
		public bool CanGoBack
		{
			set
			{
				this.buttonBack.Enabled = value;
			}
		}
		public bool CanGoForward
		{
			set
			{
				this.buttonFwd.Enabled = value;
			}
		}
		public NavigationBar()
		{
			this.InitializeComponent();
			//this.Back = (Default)Delegate.Combine(this.Back, delegate
			//{
			//    MessageBox.Show("test");
			//});
			//this.Forward = (Default)Delegate.Combine(this.Forward, delegate
			//{
			//});
			//this.Refresh = (Default)Delegate.Combine(this.Refresh, delegate
			//{
			//});
			//this.Stop = (Default)Delegate.Combine(this.Stop, delegate
			//{
			//});
			//this.Home = (Default)Delegate.Combine(this.Home, delegate
			//{
			//});
			//this.Go = (Default)Delegate.Combine(this.Go, delegate
			//{
			//});
		}
		private void buttonBack_Click(object sender, EventArgs e)
		{
			this.Back();
		}
		private void buttonFwd_Click(object sender, EventArgs e)
		{
			this.Forward();
		}
		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			this.Refresh();
		}
		private void buttonStop_Click(object sender, EventArgs e)
		{
			this.Stop();
		}
		private void buttonHome_Click(object sender, EventArgs e)
		{
			this.Home();
		}
		private void button1_Click(object sender, EventArgs e)
		{
			this.Go();
		}
		private void comboBoxAddress_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\n' || e.KeyChar == '\r')
			{
				e.Handled = true;
				this.Go();
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.buttonBack = new Button();
			this.buttonFwd = new Button();
			this.buttonRefresh = new Button();
			this.buttonStop = new Button();
			this.buttonHome = new Button();
			this.comboBoxAddress = new ComboBox();
			this.button1 = new Button();
			base.SuspendLayout();
			this.buttonBack.Enabled = false;
			this.buttonBack.Font = new Font("Microsoft Sans Serif", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.buttonBack.ForeColor = Color.FromArgb(0, 0, 64);
			this.buttonBack.Location = new Point(3, 3);
			this.buttonBack.Name = "buttonBack";
			this.buttonBack.Size = new Size(39, 36);
			this.buttonBack.TabIndex = 0;
			this.buttonBack.Text = "<";
			this.buttonBack.UseVisualStyleBackColor = true;
			this.buttonBack.Click += new EventHandler(this.buttonBack_Click);
			this.buttonFwd.Enabled = false;
			this.buttonFwd.Font = new Font("Microsoft Sans Serif", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.buttonFwd.ForeColor = Color.FromArgb(0, 0, 64);
			this.buttonFwd.Location = new Point(48, 3);
			this.buttonFwd.Name = "buttonFwd";
			this.buttonFwd.Size = new Size(39, 36);
			this.buttonFwd.TabIndex = 1;
			this.buttonFwd.Text = ">";
			this.buttonFwd.UseVisualStyleBackColor = true;
			this.buttonFwd.Click += new EventHandler(this.buttonFwd_Click);
			this.buttonRefresh.Font = new Font("Microsoft Sans Serif", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.buttonRefresh.ForeColor = Color.FromArgb(0, 0, 64);
			this.buttonRefresh.Location = new Point(93, 3);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new Size(39, 36);
			this.buttonRefresh.TabIndex = 2;
			this.buttonRefresh.Text = "R";
			this.buttonRefresh.UseVisualStyleBackColor = true;
			this.buttonRefresh.Click += new EventHandler(this.buttonRefresh_Click);
			this.buttonStop.Font = new Font("Microsoft Sans Serif", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.buttonStop.ForeColor = Color.FromArgb(0, 0, 64);
			this.buttonStop.Location = new Point(138, 3);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new Size(39, 36);
			this.buttonStop.TabIndex = 3;
			this.buttonStop.Text = "X";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new EventHandler(this.buttonStop_Click);
			this.buttonHome.Font = new Font("Microsoft Sans Serif", 18f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.buttonHome.ForeColor = Color.FromArgb(0, 0, 64);
			this.buttonHome.Location = new Point(183, 3);
			this.buttonHome.Name = "buttonHome";
			this.buttonHome.Size = new Size(39, 36);
			this.buttonHome.TabIndex = 4;
			this.buttonHome.Text = "H";
			this.buttonHome.UseVisualStyleBackColor = true;
			this.buttonHome.Click += new EventHandler(this.buttonHome_Click);
			this.comboBoxAddress.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.comboBoxAddress.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.comboBoxAddress.FormattingEnabled = true;
			this.comboBoxAddress.Location = new Point(237, 9);
			this.comboBoxAddress.Name = "comboBoxAddress";
			this.comboBoxAddress.Size = new Size(228, 24);
			this.comboBoxAddress.TabIndex = 5;
			this.comboBoxAddress.KeyPress += new KeyPressEventHandler(this.comboBoxAddress_KeyPress);
			this.button1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.button1.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button1.ForeColor = Color.FromArgb(0, 0, 64);
			this.button1.Location = new Point(480, 7);
			this.button1.Name = "button1";
			this.button1.Size = new Size(40, 27);
			this.button1.TabIndex = 6;
			this.button1.Text = "Go";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.button1);
			base.Controls.Add(this.comboBoxAddress);
			base.Controls.Add(this.buttonHome);
			base.Controls.Add(this.buttonStop);
			base.Controls.Add(this.buttonRefresh);
			base.Controls.Add(this.buttonFwd);
			base.Controls.Add(this.buttonBack);
			base.Name = "NavigationBar";
			base.Size = new Size(527, 66);
			base.ResumeLayout(false);
		}
	}
}
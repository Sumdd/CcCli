using System;
using System.ComponentModel;
using System.Windows.Forms;
using WebKit;
namespace WebKitBrowserTest
{
	public class WebBrowserTabPage : TabPage
	{
		public WebKitBrowser _browser;
		public WebKitBrowser browser
		{
			get
			{
				if (_browser != null)
					return _browser;
				else
					return new WebKitBrowser();
			}
			set { _browser = value; }
		}
		private StatusStrip statusStrip;
		private ToolStripLabel statusLabel;
		private ToolStripLabel iconLabel;
		private ToolStripContainer container;
		private IContainer components;
		public WebBrowserTabPage()
		{

		}
		public WebBrowserTabPage(WebKitBrowser browserControl, bool goHome)
		{
			this.InitializeComponent();
			this.statusStrip = new StatusStrip();
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Visible = true;
			this.statusStrip.SizingGrip = false;
			this.container = new ToolStripContainer();
			this.container.Name = "container";
			this.container.Visible = true;
			this.container.Dock = DockStyle.Fill;
			this.statusLabel = new ToolStripLabel();
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Text = "Done";
			this.statusLabel.Visible = true;
			this.iconLabel = new ToolStripLabel();
			this.iconLabel.Name = "iconLabel";
			this.iconLabel.Text = "No Icon";
			this.iconLabel.Visible = true;
			this.statusStrip.Items.Add(this.statusLabel);
			this.statusStrip.Items.Add(this.iconLabel);
			this.container.BottomToolStripPanel.Controls.Add(this.statusStrip);
			this.browser = browserControl;
			this.browser.Visible = true;
			this.browser.Dock = DockStyle.Fill;
			this.browser.Name = "browser";
			this.container.ContentPanel.Controls.Add(this.browser);
			base.Controls.Add(this.container);
			this.Text = "<New Tab>";
			this.browser.DocumentTitleChanged += delegate(object s, EventArgs e)
			{
				this.Text = this.browser.DocumentTitle;
			};
			this.browser.Navigating += delegate(object s, WebBrowserNavigatingEventArgs e)
			{
				this.statusLabel.Text = "Loading...";
			};
			this.browser.Navigated += delegate(object s, WebBrowserNavigatedEventArgs e)
			{
				this.statusLabel.Text = "Downloading...";
			};
			this.browser.DocumentCompleted += delegate(object s, WebBrowserDocumentCompletedEventArgs e)
			{
				this.statusLabel.Text = "Done";
			};
			if (goHome)
			{
				this.browser.Navigate("http://www.google.com");
			}
		}
		public void Stop()
		{
			this.browser.Stop();
			this.statusLabel.Text = "Stopped";
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
			base.SuspendLayout();
			base.ResumeLayout(false);
		}
	}
}

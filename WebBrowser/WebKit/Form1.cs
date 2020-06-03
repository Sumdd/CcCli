using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WebKit;
namespace WebKitBrowserTest
{
	public class MainForm : Form
	{
		private IContainer components;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem exitToolStripMenuItem;
		private NavigationBar navigationBar;
		private TabControl tabControl;
		private ToolStripMenuItem newTabToolStripMenuItem;
		private ToolStripMenuItem helpToolStripMenuItem;
		private ToolStripMenuItem aboutToolStripMenuItem;
		private ToolStripMenuItem viewToolStripMenuItem;
		private ToolStripMenuItem pageSourceToolStripMenuItem;
		private ToolStripMenuItem editToolStripMenuItem;
		private ToolStripMenuItem copyToolStripMenuItem;
		private ToolStripMenuItem closeTabToolStripMenuItem;
		private ToolStripMenuItem testToolStripMenuItem;
		private ToolStripMenuItem testPageToolStripMenuItem;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem tToolStripMenuItem;
		private ToolStripSeparator toolStripMenuItem2;
		private ToolStripMenuItem printToolStripMenuItem;
		private ToolStripMenuItem pageSetupToolStripMenuItem;
		private ToolStripMenuItem newWindowToolStripMenuItem;
		private ToolStripMenuItem printPreviewToolStripMenuItem;
		private ToolStripMenuItem printImmediatelyToolStripMenuItem;
		private WebBrowserTabPage currentPage;
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
			this.menuStrip1 = new MenuStrip();
			this.fileToolStripMenuItem = new ToolStripMenuItem();
			this.closeTabToolStripMenuItem = new ToolStripMenuItem();
			this.newTabToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem2 = new ToolStripSeparator();
			this.pageSetupToolStripMenuItem = new ToolStripMenuItem();
			this.printToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.exitToolStripMenuItem = new ToolStripMenuItem();
			this.editToolStripMenuItem = new ToolStripMenuItem();
			this.copyToolStripMenuItem = new ToolStripMenuItem();
			this.viewToolStripMenuItem = new ToolStripMenuItem();
			this.pageSourceToolStripMenuItem = new ToolStripMenuItem();
			this.helpToolStripMenuItem = new ToolStripMenuItem();
			this.aboutToolStripMenuItem = new ToolStripMenuItem();
			this.testToolStripMenuItem = new ToolStripMenuItem();
			this.testPageToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.tToolStripMenuItem = new ToolStripMenuItem();
			this.newWindowToolStripMenuItem = new ToolStripMenuItem();
			this.tabControl = new TabControl();
			this.printPreviewToolStripMenuItem = new ToolStripMenuItem();
			this.navigationBar = new NavigationBar();
			this.printImmediatelyToolStripMenuItem = new ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.fileToolStripMenuItem,
				this.editToolStripMenuItem,
				this.viewToolStripMenuItem,
				this.helpToolStripMenuItem,
				this.testToolStripMenuItem
			});
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new Size(662, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.closeTabToolStripMenuItem,
				this.newTabToolStripMenuItem,
				this.toolStripMenuItem2,
				this.pageSetupToolStripMenuItem,
				this.printPreviewToolStripMenuItem,
				this.printToolStripMenuItem,
				this.printImmediatelyToolStripMenuItem,
				this.toolStripSeparator1,
				this.exitToolStripMenuItem
			});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			this.closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
			this.closeTabToolStripMenuItem.Size = new Size(168, 22);
			this.closeTabToolStripMenuItem.Text = "&Close Tab";
			this.closeTabToolStripMenuItem.Click += new EventHandler(this.closeTabToolStripMenuItem_Click);
			this.newTabToolStripMenuItem.Name = "newTabToolStripMenuItem";
			this.newTabToolStripMenuItem.ShortcutKeys = (Keys)131156;
			this.newTabToolStripMenuItem.Size = new Size(168, 22);
			this.newTabToolStripMenuItem.Text = "New &Tab";
			this.newTabToolStripMenuItem.Click += new EventHandler(this.newTabToolStripMenuItem_Click);
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new Size(165, 6);
			this.pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
			this.pageSetupToolStripMenuItem.Size = new Size(168, 22);
			this.pageSetupToolStripMenuItem.Text = "Page &Setup...";
			this.pageSetupToolStripMenuItem.Click += new EventHandler(this.pageSetupToolStripMenuItem_Click);
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.Size = new Size(168, 22);
			this.printToolStripMenuItem.Text = "&Print...";
			this.printToolStripMenuItem.Click += new EventHandler(this.printToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(165, 6);
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new Size(168, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
			this.editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.copyToolStripMenuItem
			});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new Size(102, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			this.copyToolStripMenuItem.Click += new EventHandler(this.copyToolStripMenuItem_Click);
			this.viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.pageSourceToolStripMenuItem
			});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			this.pageSourceToolStripMenuItem.Name = "pageSourceToolStripMenuItem";
			this.pageSourceToolStripMenuItem.Size = new Size(139, 22);
			this.pageSourceToolStripMenuItem.Text = "&Page Source";
			this.pageSourceToolStripMenuItem.Click += new EventHandler(this.pageSourceToolStripMenuItem_Click);
			this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.aboutToolStripMenuItem
			});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new Size(107, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
			this.testToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.testPageToolStripMenuItem,
				this.toolStripMenuItem1,
				this.tToolStripMenuItem,
				this.newWindowToolStripMenuItem
			});
			this.testToolStripMenuItem.Name = "testToolStripMenuItem";
			this.testToolStripMenuItem.Size = new Size(41, 20);
			this.testToolStripMenuItem.Text = "Test";
			this.testPageToolStripMenuItem.Name = "testPageToolStripMenuItem";
			this.testPageToolStripMenuItem.Size = new Size(145, 22);
			this.testPageToolStripMenuItem.Text = "Test Page";
			this.testPageToolStripMenuItem.Click += new EventHandler(this.testPageToolStripMenuItem_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new Size(142, 6);
			this.tToolStripMenuItem.Name = "tToolStripMenuItem";
			this.tToolStripMenuItem.Size = new Size(145, 22);
			this.tToolStripMenuItem.Text = "Test 1";
			this.tToolStripMenuItem.Click += new EventHandler(this.tToolStripMenuItem_Click);
			this.newWindowToolStripMenuItem.Name = "newWindowToolStripMenuItem";
			this.newWindowToolStripMenuItem.Size = new Size(145, 22);
			this.newWindowToolStripMenuItem.Text = "New &Window";
			this.newWindowToolStripMenuItem.Click += new EventHandler(this.newWindowToolStripMenuItem_Click);
			this.tabControl.Dock = DockStyle.Fill;
			this.tabControl.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.tabControl.Location = new Point(0, 73);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new Size(662, 333);
			this.tabControl.SizeMode = TabSizeMode.Fixed;
			this.tabControl.TabIndex = 2;
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new Size(168, 22);
			this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
			this.printPreviewToolStripMenuItem.Click += new EventHandler(this.printPreviewToolStripMenuItem_Click);
			this.navigationBar.Dock = DockStyle.Top;
			this.navigationBar.Location = new Point(0, 24);
			this.navigationBar.Name = "navigationBar";
			this.navigationBar.Size = new Size(662, 49);
			this.navigationBar.TabIndex = 1;
			this.navigationBar.UrlText = "";
			this.printImmediatelyToolStripMenuItem.Name = "printImmediatelyToolStripMenuItem";
			this.printImmediatelyToolStripMenuItem.Size = new Size(168, 22);
			this.printImmediatelyToolStripMenuItem.Text = "Print Immediately";
			this.printImmediatelyToolStripMenuItem.Click += new EventHandler(this.printImmediatelyToolStripMenuItem_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(662, 406);
			base.Controls.Add(this.tabControl);
			base.Controls.Add(this.navigationBar);
			base.Controls.Add(this.menuStrip1);
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "MainForm";
			this.Text = "WebKit Browser Example";
			base.Shown += new EventHandler(this.MainForm_Shown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		public MainForm()
		{
			this.InitializeComponent();
			WebBrowserTabPage value = new WebBrowserTabPage();
			this.tabControl.TabPages.Add(value);
			this.currentPage = value;
			this.RegisterBrowserEvents();
			this.tabControl.SelectedIndexChanged += delegate(object s, EventArgs e)
			{
				if (this.currentPage != null)
				{
					this.UnregisterBrowserEvents();
				}
				this.currentPage = (WebBrowserTabPage)this.tabControl.SelectedTab;
				if (this.currentPage != null)
				{
					this.RegisterBrowserEvents();
					if (this.currentPage.browser.Url != null)
					{
						this.navigationBar.UrlText = this.currentPage.browser.Url.ToString();
					}
					else
					{
						this.navigationBar.UrlText = "";
					}
					this.Text = "WebKit Browser Example - " + this.currentPage.browser.DocumentTitle;
					this.currentPage.browser.Focus();
				}
			};
			this.navigationBar.Back += delegate
			{
				this.currentPage.browser.GoBack();
				this.ActivateBrowser();
			};
			this.navigationBar.Forward += delegate
			{
				this.currentPage.browser.GoForward();
				this.ActivateBrowser();
			};
			this.navigationBar.Go += delegate
			{
				this.currentPage.browser.Navigate(this.navigationBar.UrlText);
				this.ActivateBrowser();
			};
			this.navigationBar.Refresh += delegate
			{
				this.currentPage.browser.Reload();
				this.ActivateBrowser();
			};
			this.navigationBar.Stop += delegate
			{
				this.currentPage.Stop();
				this.ActivateBrowser();
			};
		}
		private void RegisterBrowserEvents()
		{
			this.currentPage.browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.browser_DocumentCompleted);
			this.currentPage.browser.Navigated += new WebBrowserNavigatedEventHandler(this.browser_Navigated);
			this.currentPage.browser.DocumentTitleChanged += new EventHandler(this.browser_DocumentTitleChanged);
			this.currentPage.browser.Error += new WebKitBrowserErrorEventHandler(this.browser_Error);
			this.currentPage.browser.DownloadBegin += new FileDownloadBeginEventHandler(this.browser_DownloadBegin);
			this.currentPage.browser.NewWindowRequest += new NewWindowRequestEventHandler(this.browser_NewWindowRequest);
			this.currentPage.browser.NewWindowCreated += new NewWindowCreatedEventHandler(this.browser_NewWindowCreated);
		}
		private void browser_NewWindowCreated(object sender, NewWindowCreatedEventArgs args)
		{
			this.tabControl.TabPages.Add(new WebBrowserTabPage(args.WebKitBrowser, false));
		}
		private void browser_NewWindowRequest(object sender, NewWindowRequestEventArgs args)
		{
			args.Cancel = (MessageBox.Show(args.Url, "Open new window?", MessageBoxButtons.YesNo) == DialogResult.No);
		}
		private void browser_DownloadBegin(object sender, FileDownloadBeginEventArgs args)
		{
			new DownloadForm(args.Download);
		}
		private void browser_Error(object sender, WebKitBrowserErrorEventArgs args)
		{
			if (this.currentPage != null)
			{
				this.currentPage.browser.DocumentText = "<html><head><title>Error</title></head><center><p>" + args.Description + "</p></center></html>";
			}
		}
		private void browser_DocumentTitleChanged(object sender, EventArgs e)
		{
			this.Text = "WebKit Browser Example - " + this.currentPage.browser.DocumentTitle;
		}
		private void browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			this.navigationBar.UrlText = this.currentPage.browser.Url.ToString();
		}
		private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			this.navigationBar.UrlText = this.currentPage.browser.Url.ToString();
			this.navigationBar.CanGoBack = this.currentPage.browser.CanGoBack;
			this.navigationBar.CanGoForward = this.currentPage.browser.CanGoForward;
		}
		private void UnregisterBrowserEvents()
		{
			this.currentPage.browser.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(this.browser_DocumentCompleted);
			this.currentPage.browser.Navigated -= new WebBrowserNavigatedEventHandler(this.browser_Navigated);
			this.currentPage.browser.DocumentTitleChanged -= new EventHandler(this.browser_DocumentTitleChanged);
			this.currentPage.browser.NewWindowCreated -= new NewWindowCreatedEventHandler(this.browser_NewWindowCreated);
			this.currentPage.browser.NewWindowRequest -= new NewWindowRequestEventHandler(this.browser_NewWindowRequest);
		}
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WebBrowserTabPage value = new WebBrowserTabPage();
			this.tabControl.TabPages.Add(value);
		}
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("www.sourceforge.net/projects/webkitdotnet\n\nWebKitBrowser version " + this.currentPage.browser.Version, "About WebKit.NET");
		}
		private void pageSourceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new SourceViewForm(this.currentPage.browser.DocumentText, this.currentPage.browser).Show();
		}
		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.currentPage.browser.SelectedText != null)
				{
					Clipboard.SetText(this.currentPage.browser.SelectedText);
				}
			}
			catch (Exception)
			{
			}
		}
		private void MainForm_Shown(object sender, EventArgs e)
		{
			this.ActivateBrowser();
		}
		private void ActivateBrowser()
		{
			if (this.currentPage.browser.CanFocus)
			{
				this.currentPage.browser.Focus();
			}
		}
		private void closeTabToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TabPage tabPage = this.currentPage;
			this.tabControl.Controls.Remove(tabPage);
			tabPage.Dispose();
			if (this.tabControl.Controls.Count == 0)
			{
				Application.Exit();
			}
		}
		private void testPageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.currentPage.browser.DocumentText = "<html><head><title>Test Page</title></head><body><p id=\"testelement\" style=\"color: red\">Hello, World!</p><div><p>A</p><p>B</p><p>C</p></div><script type=\"text/javascript\">function f() { window.open('http://www.google.com', 'myWindow'); }</script></body></html>";
		}
		private void tToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.currentPage.browser.StringByEvaluatingJavaScriptFromString("f()");
		}
		private void printToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.currentPage.browser.ShowPrintDialog();
		}
		private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.currentPage.browser.ShowPageSetupDialog();
		}
		private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("This is likely to cause a crash. Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				Thread thread = new Thread(new ThreadStart(this.MyThread));
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
			}
		}
		private void MyThread()
		{
			Application.Run(new MainForm());
		}
		private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.currentPage.browser.ShowPrintPreviewDialog();
		}
		private void printImmediatelyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.currentPage.browser.Print();
		}
	}
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebKit;
namespace WebKitBrowserTest
{
	public class SourceViewForm : Form
	{
		private IContainer components;
		private TextBox textBox;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem saveToolStripMenuItem;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem closeToolStripMenuItem;
		private WebKitBrowser current;
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
			this.textBox = new TextBox();
			this.menuStrip1 = new MenuStrip();
			this.fileToolStripMenuItem = new ToolStripMenuItem();
			this.saveToolStripMenuItem = new ToolStripMenuItem();
			this.closeToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this.menuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.textBox.AcceptsReturn = true;
			this.textBox.AcceptsTab = true;
			this.textBox.AllowDrop = true;
			this.textBox.Dock = DockStyle.Fill;
			this.textBox.Font = new Font("Courier New", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.textBox.Location = new Point(0, 24);
			this.textBox.Multiline = true;
			this.textBox.Name = "textBox";
			this.textBox.Size = new Size(578, 415);
			this.textBox.TabIndex = 0;
			this.textBox.WordWrap = false;
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.fileToolStripMenuItem
			});
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new Size(578, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.saveToolStripMenuItem,
				this.toolStripMenuItem1,
				this.closeToolStripMenuItem
			});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new Size(152, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new EventHandler(this.saveToolStripMenuItem_Click);
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new Size(152, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new EventHandler(this.closeToolStripMenuItem_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new Size(149, 6);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(578, 439);
			base.Controls.Add(this.textBox);
			base.Controls.Add(this.menuStrip1);
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "SourceViewForm";
			this.Text = "Page Source";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		public SourceViewForm(string text, WebKitBrowser current)
		{
			this.InitializeComponent();
			this.textBox.Text = text;
			this.current = current;
		}
		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.Close();
		}
		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.current.DocumentText = this.textBox.Text;
		}
	}
}

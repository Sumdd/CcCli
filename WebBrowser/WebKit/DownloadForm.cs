using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebKit;
namespace WebKitBrowserTest
{
	public class DownloadForm : Form
	{
		private IContainer components;
		private ProgressBar progressBar1;
		private Label label1;
		private Label label2;
		private Button button1;
		private WebKitDownload Download;
		private long size;
		private long recv;
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
			this.progressBar1 = new ProgressBar();
			this.label1 = new Label();
			this.label2 = new Label();
			this.button1 = new Button();
			base.SuspendLayout();
			this.progressBar1.Location = new Point(12, 61);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new Size(328, 44);
			this.progressBar1.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(35, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(12, 45);
			this.label2.Name = "label2";
			this.label2.Size = new Size(35, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "label2";
			this.button1.Location = new Point(134, 115);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 39);
			this.button1.TabIndex = 3;
			this.button1.Text = "Cancel";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(355, 162);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.progressBar1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.Name = "DownloadForm";
			this.Text = "DownloadForm";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		public DownloadForm(WebKitDownload Download)
		{
			this.Download = Download;
			this.InitializeComponent();
			base.Visible = false;
			Download.DownloadStarted += new DownloadStartedEventHandler(this.Download_DownloadStarted);
			Download.DownloadReceiveData += new DownloadReceiveDataEventHandler(this.Download_DownloadReceiveData);
			Download.DownloadFinished += new DownloadFinishedEventHandler(this.Download_DownloadFinished);
		}
		private void Download_DownloadFinished(object sender, EventArgs args)
		{
			this.progressBar1.Value = this.progressBar1.Maximum;
			this.label2.Text = "Finished!";
		}
		private void Download_DownloadReceiveData(object sender, DownloadReceiveDataEventArgs args)
		{
			this.recv += (long)((ulong)args.Length);
			this.label2.Text = this.recv.ToString() + " / " + this.size.ToString() + " bytes downloaded";
			this.progressBar1.Value = (int)((float)this.recv / (float)this.size * (float)this.progressBar1.Maximum);
		}
		private void Download_DownloadStarted(object sender, DownloadStartedEventArgs args)
		{
			if (MessageBox.Show("Download file " + args.SuggestedFileName + "?", "Download", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				this.size = args.FileSize;
				this.label1.Text = args.SuggestedFileName;
				this.Text = "Download " + args.SuggestedFileName;
				this.label2.Text = "0";
				base.Show();
				return;
			}
			this.Download.Cancel();
			base.Close();
		}
		private void button1_Click(object sender, EventArgs e)
		{
			this.Download.Cancel();
			base.Close();
		}
	}
}
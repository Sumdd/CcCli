using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CenoCC
{
	public partial class Tab_Title : UserControl
	{
		public delegate void ClickBrowserTitle(Tab_Title bt, bool Flag);
		public event ClickBrowserTitle _ClickBrowserTitle;

		public Tab_Title()
		{
			InitializeComponent();

		}

		public string TitleName
		{
			set { this.label1.Text = value; }
			get { return this.label1.Text; }
		}

		public Image TitlePic
		{
			set { this.TabPic_Pan.BackgroundImage = value; }
		}

		private void panel1_MouseClick(object sender, MouseEventArgs e)
		{
			_ClickBrowserTitle(this, false);
		}

		private void Tab_Title_Click(object sender, EventArgs e)
		{
			_ClickBrowserTitle(this, true);
		}

		private void label1_DoubleClick(object sender, EventArgs e)
		{
			_ClickBrowserTitle(this, false);
		}

		public void ShowActive(bool ActiveFlag)
		{
			if (!ActiveFlag)
				this.BackgroundImage = global::CenoCC.Properties.Resources.Top_Tab_1;
			else
				this.BackgroundImage = global::CenoCC.Properties.Resources.Top_Tab;
		}

		private void label1_Click(object sender, EventArgs e)
		{
			_ClickBrowserTitle(this, true);
		}

	}
}

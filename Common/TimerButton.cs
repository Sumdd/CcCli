using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common
{
	public partial class TimerButton : Button
	{
		public System.Timers.Timer _Timers;
		int WaitSec;
		public TimerButton()
		{
			InitializeComponent();
			_Timers = new System.Timers.Timer();
			_Timers.Interval = 1000;
			_Timers.Elapsed += new System.Timers.ElapsedEventHandler(_Timers_Elapsed);
		}

		public static void RunComplete(TimerButton rbtn)
		{
			rbtn.Enabled = true;
			rbtn._Timers.Stop();
			rbtn._Timers.Enabled = false;
			rbtn.Text = "查询";
		}

		void _Timers_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (this.InvokeRequired)
				this.Invoke(new MethodInvoker(delegate()
				{
					this.Text = "请稍后 (" + WaitSec++.ToString() + ")";
				}));
			else
				this.Text = "请稍后 (" + WaitSec++.ToString() + ")";
		}

		protected void SetWidth(int BWidth = 120, int BHeight = 50)
		{
			this.Height = BHeight;
			this.Width = BWidth;
		}

		protected void SetText(string BText)
		{
			this.Text = BText;
			this.CurrentText = BText;
		}

		protected override void OnClick(EventArgs e)
		{
			this.Text = "请稍后 (0)";
			this.Enabled = false;
			WaitSec = 1;
			this._Timers.Start();
			base.OnClick(e);
		}

		protected string CurrentText
		{
			get;
			set;
		}
	}
}

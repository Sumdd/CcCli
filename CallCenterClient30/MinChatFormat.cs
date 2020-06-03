using System.Windows.Forms;
using System;
using Common;
using System.IO;
namespace CenoCC
{
	static partial class MinChat
	{
		private static System.ComponentModel.IContainer components = null;

		private static void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MinChat));
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			HangUp_TMSI = new System.Windows.Forms.ToolStripMenuItem();
			Call_TMSI = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			OpenBrowser_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
			CallRecord_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
			LoadRecord_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
			Report_TMSI = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			ServerList_TMSI = new System.Windows.Forms.ToolStripMenuItem();
			电话服务器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			sip服务器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ParamSet_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
			UsualSet_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
			ServerSet_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
			DeviceSet_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			Help_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
			LogFile_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
			版本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			系统升级ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			System_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
			Exit_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
			Reset_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
			AgentInfo_TT = new System.Windows.Forms.ToolTip(components);
			Sys_NoIc = new System.Windows.Forms.NotifyIcon(components);
			CallInfo_Pnl = new System.Windows.Forms.Panel();
			DialTime_Lbl = new System.Windows.Forms.Label();
			PhoneAddress_Lbl = new System.Windows.Forms.Label();
			PhoneNum_Contact_Lbl = new System.Windows.Forms.Label();
			CallInfo_Pnl = new System.Windows.Forms.Panel();
			NoAnswer_flag = new System.Windows.Forms.Panel();
			contextMenuStrip1.SuspendLayout();
			CallInfo_Pnl.SuspendLayout();
			SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            HangUp_TMSI,
            Call_TMSI,
            toolStripSeparator1,
            OpenBrowser_Tsmi,
            CallRecord_Tsmi,
            LoadRecord_Tsmi,
            Report_TMSI,
            toolStripSeparator2,
            ServerList_TMSI,
            ParamSet_Tsmi,
            toolStripSeparator3,
            Help_Tsmi,
            System_Tsmi});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(153, 264);
			// 
			// HangUp_TMSI
			// 
			HangUp_TMSI.Enabled = false;
			HangUp_TMSI.Image = global::CenoCC.Properties.Resources.HangUp;
			HangUp_TMSI.Name = "HangUp_TMSI";
			HangUp_TMSI.Size = new System.Drawing.Size(152, 22);
			HangUp_TMSI.Text = "挂机(&Z)";
			HangUp_TMSI.DisplayStyleChanged += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// Call_TMSI
			// 
			Call_TMSI.Enabled = false;
			Call_TMSI.Image = global::CenoCC.Properties.Resources.PickUp;
			Call_TMSI.Name = "Call_TMSI";
			Call_TMSI.Size = new System.Drawing.Size(152, 22);
			Call_TMSI.Text = "拨号(&P)";
			Call_TMSI.Click += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// OpenBrowser_Tsmi
			// 
			OpenBrowser_Tsmi.Image = global::CenoCC.Properties.Resources.ie;
			OpenBrowser_Tsmi.Name = "OpenBrowser_Tsmi";
			OpenBrowser_Tsmi.Size = new System.Drawing.Size(152, 22);
			OpenBrowser_Tsmi.Text = "浏览器(&B)";
			OpenBrowser_Tsmi.Click += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// CallRecord_Tsmi
			// 
			CallRecord_Tsmi.Image = global::CenoCC.Properties.Resources.CallRecord;
			CallRecord_Tsmi.Name = "CallRecord_Tsmi";
			CallRecord_Tsmi.Size = new System.Drawing.Size(152, 22);
			CallRecord_Tsmi.Text = "通话记录(&C)";
			CallRecord_Tsmi.Click += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// LoadRecord_Tsmi
			// 
			LoadRecord_Tsmi.Image = global::CenoCC.Properties.Resources.RecordLoad;
			LoadRecord_Tsmi.Name = "LoadRecord_Tsmi";
			LoadRecord_Tsmi.Size = new System.Drawing.Size(152, 22);
			LoadRecord_Tsmi.Text = "录音下载(&L)";
			LoadRecord_Tsmi.Click += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// Report_TMSI
			// 
			Report_TMSI.Image = global::CenoCC.Properties.Resources.Report;
			Report_TMSI.Name = "Report_TMSI";
			Report_TMSI.Size = new System.Drawing.Size(152, 22);
			Report_TMSI.Text = "统计表(&R)";
			Report_TMSI.Click += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
			// 
			// ServerList_TMSI
			// 
			ServerList_TMSI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            电话服务器ToolStripMenuItem,
            sip服务器ToolStripMenuItem});
			ServerList_TMSI.Enabled = false;
			ServerList_TMSI.Image = global::CenoCC.Properties.Resources.fuwuqi;
			ServerList_TMSI.Name = "ServerList_TMSI";
			ServerList_TMSI.Size = new System.Drawing.Size(152, 22);
			ServerList_TMSI.Text = "服务器(&F)";
			// 
			// 电话服务器ToolStripMenuItem
			// 
			电话服务器ToolStripMenuItem.Name = "电话服务器ToolStripMenuItem";
			电话服务器ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			电话服务器ToolStripMenuItem.Text = "电话服务器";
			// 
			// sip服务器ToolStripMenuItem
			// 
			sip服务器ToolStripMenuItem.Name = "sip服务器ToolStripMenuItem";
			sip服务器ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			sip服务器ToolStripMenuItem.Text = "sip服务器";
			// 
			// ParamSet_Tsmi
			// 
			ParamSet_Tsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            UsualSet_Tsmi,
            ServerSet_Tsmi,
            DeviceSet_Tsmi});
			ParamSet_Tsmi.Image = global::CenoCC.Properties.Resources.shezhi;
			ParamSet_Tsmi.Name = "ParamSet_Tsmi";
			ParamSet_Tsmi.Size = new System.Drawing.Size(152, 22);
			ParamSet_Tsmi.Text = "设置(&S)";
			ParamSet_Tsmi.Click += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// UsualSet_Tsmi
			// 
			UsualSet_Tsmi.Name = "UsualSet_Tsmi";
			UsualSet_Tsmi.Size = new System.Drawing.Size(152, 22);
			UsualSet_Tsmi.Text = "常用设置";
			UsualSet_Tsmi.Click += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// ServerSet_Tsmi
			// 
			ServerSet_Tsmi.Name = "ServerSet_Tsmi";
			ServerSet_Tsmi.Size = new System.Drawing.Size(152, 22);
			ServerSet_Tsmi.Text = "服务器设置";
			ServerSet_Tsmi.Click += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// DeviceSet_Tsmi
			// 
			DeviceSet_Tsmi.Name = "DeviceSet_Tsmi";
			DeviceSet_Tsmi.Size = new System.Drawing.Size(152, 22);
			DeviceSet_Tsmi.Text = "设备";
			DeviceSet_Tsmi.Click += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
			// 
			// Help_Tsmi
			// 
			Help_Tsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            LogFile_Tsmi,
            版本ToolStripMenuItem,
            系统升级ToolStripMenuItem});
			Help_Tsmi.Image = global::CenoCC.Properties.Resources.help;
			Help_Tsmi.Name = "Help_Tsmi";
			Help_Tsmi.Size = new System.Drawing.Size(152, 22);
			Help_Tsmi.Text = "帮助(&H)";
			// 
			// LogFile_Tsmi
			// 
			LogFile_Tsmi.Name = "LogFile_Tsmi";
			LogFile_Tsmi.Size = new System.Drawing.Size(152, 22);
			LogFile_Tsmi.Text = "日志";
			LogFile_Tsmi.Click += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// 版本ToolStripMenuItem
			// 
			版本ToolStripMenuItem.Name = "版本ToolStripMenuItem";
			版本ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			版本ToolStripMenuItem.Text = "版本";
			// 
			// 系统升级ToolStripMenuItem
			// 
			系统升级ToolStripMenuItem.Name = "系统升级ToolStripMenuItem";
			系统升级ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			系统升级ToolStripMenuItem.Text = "系统升级";
			// 
			// System_Tsmi
			// 
			System_Tsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            Exit_Tsmi,
            Reset_Tsmi});
			System_Tsmi.Image = global::CenoCC.Properties.Resources.exit;
			System_Tsmi.Name = "System_Tsmi";
			System_Tsmi.Size = new System.Drawing.Size(152, 22);
			System_Tsmi.Text = "退出";
			System_Tsmi.Click += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// Exit_Tsmi
			// 
			Exit_Tsmi.Image = global::CenoCC.Properties.Resources.exit;
			Exit_Tsmi.Name = "Exit_Tsmi";
			Exit_Tsmi.Size = new System.Drawing.Size(152, 22);
			Exit_Tsmi.Text = "退出";
			Exit_Tsmi.Click += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// Reset_Tsmi
			// 
			Reset_Tsmi.Name = "Reset_Tsmi";
			Reset_Tsmi.Size = new System.Drawing.Size(152, 22);
			Reset_Tsmi.Text = "重启";
			Reset_Tsmi.Click += new System.EventHandler(ToolStripMenuItem_Click);
			// 
			// AgentInfo_TT
			// 
			AgentInfo_TT.AutoPopDelay = 5000;
			AgentInfo_TT.InitialDelay = 3000;
			AgentInfo_TT.IsBalloon = true;
			AgentInfo_TT.ReshowDelay = 100;
			AgentInfo_TT.ShowAlways = true;
			AgentInfo_TT.UseFading = false;
			// 
			// Sys_NoIc
			// 
			Sys_NoIc.Icon = ((System.Drawing.Icon)(resources.GetObject("Sys_NoIc.Icon")));
			Sys_NoIc.Visible = true;
			// 
			// panel1
			// 
			CallInfo_Pnl.Controls.Add(DialTime_Lbl);
			CallInfo_Pnl.Controls.Add(PhoneAddress_Lbl);
			CallInfo_Pnl.Controls.Add(PhoneNum_Contact_Lbl);
			CallInfo_Pnl.Location = new System.Drawing.Point(40, 0);
			CallInfo_Pnl.Name = "panel1";
			CallInfo_Pnl.Size = new System.Drawing.Size(140, 40);
			CallInfo_Pnl.TabIndex = 3;
			// 
			// DialTime_Lbl
			// 
			DialTime_Lbl.AutoSize = true;
			DialTime_Lbl.Location = new System.Drawing.Point(82, 24);
			DialTime_Lbl.Name = "DialTime_Lbl";
			DialTime_Lbl.Size = new System.Drawing.Size(53, 12);
			DialTime_Lbl.TabIndex = 2;
			DialTime_Lbl.Text = "00:00:00";
			// 
			// PhoneAddress_Lbl
			// 
			PhoneAddress_Lbl.Location = new System.Drawing.Point(5, 23);
			PhoneAddress_Lbl.Name = "PhoneAddress_Lbl";
			PhoneAddress_Lbl.Size = new System.Drawing.Size(75, 12);
			PhoneAddress_Lbl.TabIndex = 1;
			PhoneAddress_Lbl.Text = "山东  青岛市";
			PhoneAddress_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PhoneNum_Address_Lbl
			// 
			PhoneNum_Contact_Lbl.AutoSize = true;
			PhoneNum_Contact_Lbl.Location = new System.Drawing.Point(0, 5);
			PhoneNum_Contact_Lbl.MaximumSize = new System.Drawing.Size(137, 12);
			PhoneNum_Contact_Lbl.MinimumSize = new System.Drawing.Size(137, 12);
			PhoneNum_Contact_Lbl.Name = "PhoneNum_Address_Lbl";
			PhoneNum_Contact_Lbl.Size = new System.Drawing.Size(137, 12);
			PhoneNum_Contact_Lbl.TabIndex = 0;
			PhoneNum_Contact_Lbl.Text = "18561591960(孙中冠)";
			PhoneNum_Contact_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CallIn_Flag
			// 
			CallIn_Flag.BackColor = System.Drawing.Color.Green;
			CallIn_Flag.Location = new System.Drawing.Point(34, 34);
			CallIn_Flag.Name = "CallIn_Flag";
			CallIn_Flag.Size = new System.Drawing.Size(5, 5);
			CallIn_Flag.TabIndex = 2;
			CallIn_Flag.Visible = false;
			// 
			// NoAnswer_flag
			// 
			NoAnswer_flag.BackColor = System.Drawing.Color.Red;
			NoAnswer_flag.Location = new System.Drawing.Point(1, 34);
			NoAnswer_flag.Name = "NoAnswer_flag";
			NoAnswer_flag.Size = new System.Drawing.Size(5, 5);
			NoAnswer_flag.TabIndex = 1;
			NoAnswer_flag.Visible = false;
			// 
			// MinChat
			// 
			AllowDrop = true;
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackgroundImage = global::CenoCC.Properties.Resources.Tubiao;
			ClientSize = new System.Drawing.Size(180, 40);
			ContextMenuStrip = contextMenuStrip1;
			Controls.Add(CallInfo_Pnl);
			Controls.Add(CallIn_Flag);
			Controls.Add(NoAnswer_flag);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
			MaximumSize = new System.Drawing.Size(180, 40);
			MinimumSize = new System.Drawing.Size(40, 40);
			Name = "MinChat";
			ShowInTaskbar = false;
			TopMost = true;
			Load += new System.EventHandler(MinChat_Load);
			DragDrop += new System.Windows.Forms.DragEventHandler(MinChat_DragDrop);
			DragEnter += new System.Windows.Forms.DragEventHandler(MinChat_DragDrop);
			MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MinChat_MouseDoubleClick);
			contextMenuStrip1.ResumeLayout(false);
			CallInfo_Pnl.ResumeLayout(false);
			CallInfo_Pnl.PerformLayout();
			ResumeLayout(false);

		}

		public static System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		public static System.Windows.Forms.ToolStripMenuItem HangUp_TMSI;
		public static System.Windows.Forms.ToolStripMenuItem Call_TMSI;
		public static System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		public static System.Windows.Forms.ToolStripMenuItem OpenBrowser_Tsmi;
		public static System.Windows.Forms.ToolStripMenuItem CallRecord_Tsmi;
		public static System.Windows.Forms.ToolStripMenuItem Report_TMSI;
		public static System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		public static System.Windows.Forms.ToolStripMenuItem ParamSet_Tsmi;
		public static System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		public static System.Windows.Forms.ToolStripMenuItem System_Tsmi;
		public static System.Windows.Forms.ToolStripMenuItem ServerList_TMSI;
		public static System.Windows.Forms.ToolStripMenuItem 电话服务器ToolStripMenuItem;
		public static System.Windows.Forms.ToolStripMenuItem sip服务器ToolStripMenuItem;
		public static System.Windows.Forms.Panel NoAnswer_flag;
		public static System.Windows.Forms.ToolStripMenuItem LoadRecord_Tsmi;
		public static System.Windows.Forms.ToolTip AgentInfo_TT;
		public static System.Windows.Forms.NotifyIcon Sys_NoIc;
		public static System.Windows.Forms.Panel CallInfo_Pnl;
		public static System.Windows.Forms.Panel CallIn_Flag;
		public static System.Windows.Forms.Label PhoneAddress_Lbl;
		public static System.Windows.Forms.Label PhoneNum_Contact_Lbl;
		public static System.Windows.Forms.Label DialTime_Lbl;
		public static System.Timers.Timer DragTimer;
		public static ToolStripMenuItem Help_Tsmi;
		public static ToolStripMenuItem UsualSet_Tsmi;
		public static ToolStripMenuItem ServerSet_Tsmi;
		public static ToolStripMenuItem DeviceSet_Tsmi;
		public static ToolStripMenuItem 系统升级ToolStripMenuItem;
		public static ToolStripMenuItem LogFile_Tsmi;
		public static ToolStripMenuItem 版本ToolStripMenuItem;
		public static ToolStripMenuItem Reset_Tsmi;
		public static ToolStripMenuItem Exit_Tsmi;


		private static void MinChat_DragDrop(object sender, DragEventArgs e)
		{
			string DropString = (e.Data.GetData(DataFormats.StringFormat)).ToString();
			DropString = PhoneNum.GetNumber(DropString).ToString();
			if (18 > DropString.Length && DropString.Length > 3)
			{
				_DropDialData.DialNum = DropString;
				_DropDialData.ContentName = "未知";
				_DropDialData.PhoneAddress = "未知";
				Width = 180;
				CallInfo_Pnl.Visible = true;
				PhoneNum_Contact_Lbl.Text = "双击拨打，三秒消失";
				PhoneAddress_Lbl.Text = DropString;
				DragTimer = new System.Timers.Timer();
				DragTimer.AutoReset = false;
				DragTimer.Interval = 3000;
				DragTimer.Elapsed += new System.Timers.ElapsedEventHandler(DragTimer_Elapsed);
				DragTimer.Start();
			}
		}

		static void DragTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			_DropDialData.DialNum = "";
			_DropDialData.ContentName = "";
			_DropDialData.PhoneAddress = "";
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(delegate()
				{
					Width = 40;
					CallInfo_Pnl.Visible = false;
					PhoneNum_Contact_Lbl.Text = "";
					PhoneAddress_Lbl.Text = "";
				}));
			}
		}

		private void MinChat_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.StringFormat))
				e.Effect = DragDropEffects.All;
			else
				e.Effect = DragDropEffects.None;
		}

		private static void MinChat_Load(object sender, EventArgs e)
		{
			MinChat.Left = Screen.PrimaryScreen.Bounds.Width - 200;
			Top = 50;
			Width = 40;

			FormClass.AnimateWindow(Handle, 1000, FormClass.AW_BLEND);
		}
		private static void ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			switch (((ToolStripMenuItem)sender).Name)
			{
				case "ShowMainForm_Tsmi":
					CallChat mf = new CallChat();
					FormClass.FormShow(mf);
					break;
				case "Exit_Tsmi":
				case "System_Tsmi":
					Application.Exit();
					break;
				case "Reset_Tsmi":
					Application.Restart();
					break;
				case "LoadRecord_Tsmi":
					if (MinChat.MainBrowserForm == null)
					{
						MinChat.MainBrowserForm = new Main_Frm();
						MinChat.MainBrowserForm.Show();
					}
					MinChat.MainBrowserForm.AddTab(new LoadRecord());
					break;
				case "OpenBrowser_Tsmi":
					if (MinChat.MainBrowserForm == null)
					{
						MinChat.MainBrowserForm = new Main_Frm();
						MinChat.MainBrowserForm.Show();
					}
					MinChat.MainBrowserForm.AddTab("http://192.168.1.7/lx4.0");
					break;
				case "ServerSet_Tsmi":
				case "DeviceSet_Tsmi":
				case "UsualSet_Tsmi":
					SettingFrm sf = new SettingFrm("Server");
					sf.Show();
					break;
				case "CallRecord_Tsmi":
					if (MinChat.MainBrowserForm == null)
					{
						MinChat.MainBrowserForm = new Main_Frm();
						MinChat.MainBrowserForm.Show();
					}
					MinChat.MainBrowserForm.AddTab(new CallRecord());
					break;
				case "LogFile_Tsmi":
					string logpath = Application.StartupPath + "\\AppLog\\" + DateTime.Now.ToString("yyyy-MM-dd");
					string file = DateTime.Now.ToString("yyyy-MM-dd") + "_0.log";
					if (File.Exists(logpath + "\\" + file))
						System.Diagnostics.Process.Start("notepad.exe", logpath + "\\" + file);
					else
						System.Diagnostics.Process.Start("notepad.exe", logpath);
					break;
			}
		}

		private static void SessionTimeLenTimer_Timer_Tick(object sender, EventArgs e)
		{
			DialTime_Lbl.Text = (new DateTime().AddSeconds(CallTimeLength++)).ToString("HH:mm:ss");
		}

		private static void SessionFlagTimer_Tick(object sender, EventArgs e)
		{
			if (CallIn_Flag.Visible)
				CallIn_Flag.Visible = false;
			else
				CallIn_Flag.Visible = true;
		}
	}
}

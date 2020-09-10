using System.Windows.Forms;
using System;
using Common;
using System.IO;
using DataBaseUtil;
using CenoSocket;
using CenoSip;
using WebSocket_v1;
using Core_v1;
using Cmn_v1;
using Model_v1;
using System.Collections.Generic;
using WebBrowser;

namespace CenoCC {
    partial class MinChat {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MinChat));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AgentInfo_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.CurrentStatus_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.RecentNoanswerCalls_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.Call_Tmsi = new System.Windows.Forms.ToolStripMenuItem();
            this.HangUp_Tmsi = new System.Windows.Forms.ToolStripMenuItem();
            this.Hold_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.Transfer_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.OpenBrowser_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.Report_Tmsi = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRecReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMultiPhone = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShareArea = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGateway = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_route = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_wblist = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Power = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowDialPad_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ParamSet_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Help_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowHelp_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.LogFile_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.CheckUpdate_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutUs_TSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.System_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.Register_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.Exit_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.Reset_Tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.CallInfo_Pnl = new System.Windows.Forms.Panel();
            this.DialTime_Lbl = new System.Windows.Forms.Label();
            this.CallStatus_Lbl = new System.Windows.Forms.Label();
            this.PhoneNum_Contact_Lbl = new System.Windows.Forms.Label();
            this.Call_Flag_Pnl = new System.Windows.Forms.Panel();
            this.NoAnswer_Flag_Pnl = new System.Windows.Forms.Panel();
            this.PhoneAddress_TT = new System.Windows.Forms.ToolTip(this.components);
            this.GlobleContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.number_GlobelContextMenu_MI = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.dial_GlobelContextMenu_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddZeroDial = new System.Windows.Forms.ToolStripMenuItem();
            this.cancel_GlobelContextMenu_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.CallInfo_Pnl.SuspendLayout();
            this.GlobleContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AgentInfo_TSMI,
            this.toolStripSeparator5,
            this.CurrentStatus_TSMI,
            this.RecentNoanswerCalls_TSMI,
            this.toolStripSeparator4,
            this.Call_Tmsi,
            this.HangUp_Tmsi,
            this.Hold_TSMI,
            this.Transfer_TSMI,
            this.toolStripSeparator1,
            this.OpenBrowser_Tsmi,
            this.Report_Tmsi,
            this.tsmiRecReport,
            this.tsmiMultiPhone,
            this.tsmiShareArea,
            this.tsmiUser,
            this.tsmiGateway,
            this.tsmi_route,
            this.tsmi_wblist,
            this.tsmi_Power,
            this.ShowDialPad_TSMI,
            this.toolStripSeparator2,
            this.ParamSet_Tsmi,
            this.toolStripSeparator3,
            this.Help_Tsmi,
            this.System_Tsmi});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(201, 496);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // AgentInfo_TSMI
            // 
            this.AgentInfo_TSMI.Name = "AgentInfo_TSMI";
            this.AgentInfo_TSMI.Size = new System.Drawing.Size(200, 22);
            this.AgentInfo_TSMI.Text = "张三：法务部  管理员  ";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(197, 6);
            // 
            // CurrentStatus_TSMI
            // 
            this.CurrentStatus_TSMI.Name = "CurrentStatus_TSMI";
            this.CurrentStatus_TSMI.Size = new System.Drawing.Size(200, 22);
            this.CurrentStatus_TSMI.Text = "当前状态 : 空闲中";
            // 
            // RecentNoanswerCalls_TSMI
            // 
            this.RecentNoanswerCalls_TSMI.ForeColor = System.Drawing.Color.Black;
            this.RecentNoanswerCalls_TSMI.Name = "RecentNoanswerCalls_TSMI";
            this.RecentNoanswerCalls_TSMI.Size = new System.Drawing.Size(200, 22);
            this.RecentNoanswerCalls_TSMI.Text = "未接来电 (0)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(197, 6);
            // 
            // Call_Tmsi
            // 
            this.Call_Tmsi.Enabled = false;
            this.Call_Tmsi.Image = global::CenoCC.Properties.Resources.PickUp;
            this.Call_Tmsi.Name = "Call_Tmsi";
            this.Call_Tmsi.Size = new System.Drawing.Size(200, 22);
            this.Call_Tmsi.Text = "拨号(&Enter)";
            this.Call_Tmsi.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // HangUp_Tmsi
            // 
            this.HangUp_Tmsi.Enabled = false;
            this.HangUp_Tmsi.Image = global::CenoCC.Properties.Resources.HangUp;
            this.HangUp_Tmsi.Name = "HangUp_Tmsi";
            this.HangUp_Tmsi.Size = new System.Drawing.Size(200, 22);
            this.HangUp_Tmsi.Text = "挂机(&Z)";
            this.HangUp_Tmsi.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Hold_TSMI
            // 
            this.Hold_TSMI.Enabled = false;
            this.Hold_TSMI.Image = global::CenoCC.Properties.Resources.sound_off;
            this.Hold_TSMI.Name = "Hold_TSMI";
            this.Hold_TSMI.Size = new System.Drawing.Size(200, 22);
            this.Hold_TSMI.Text = "保持(&K)";
            this.Hold_TSMI.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Transfer_TSMI
            // 
            this.Transfer_TSMI.Enabled = false;
            this.Transfer_TSMI.Name = "Transfer_TSMI";
            this.Transfer_TSMI.Size = new System.Drawing.Size(200, 22);
            this.Transfer_TSMI.Text = "转接";
            this.Transfer_TSMI.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
            // 
            // OpenBrowser_Tsmi
            // 
            this.OpenBrowser_Tsmi.Image = global::CenoCC.Properties.Resources.ie;
            this.OpenBrowser_Tsmi.Name = "OpenBrowser_Tsmi";
            this.OpenBrowser_Tsmi.Size = new System.Drawing.Size(200, 22);
            this.OpenBrowser_Tsmi.Tag = "browser";
            this.OpenBrowser_Tsmi.Text = "浏览器(&B)";
            this.OpenBrowser_Tsmi.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Report_Tmsi
            // 
            this.Report_Tmsi.Image = global::CenoCC.Properties.Resources.CallRecord;
            this.Report_Tmsi.Name = "Report_Tmsi";
            this.Report_Tmsi.Size = new System.Drawing.Size(200, 22);
            this.Report_Tmsi.Tag = "phonerecords";
            this.Report_Tmsi.Text = "通话记录";
            this.Report_Tmsi.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsmiRecReport
            // 
            this.tsmiRecReport.Image = global::CenoCC.Properties.Resources.Report;
            this.tsmiRecReport.Name = "tsmiRecReport";
            this.tsmiRecReport.Size = new System.Drawing.Size(200, 22);
            this.tsmiRecReport.Tag = "phonestatistical";
            this.tsmiRecReport.Text = "统计表";
            this.tsmiRecReport.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsmiMultiPhone
            // 
            this.tsmiMultiPhone.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMultiPhone.Image")));
            this.tsmiMultiPhone.Name = "tsmiMultiPhone";
            this.tsmiMultiPhone.Size = new System.Drawing.Size(200, 22);
            this.tsmiMultiPhone.Tag = "diallimit";
            this.tsmiMultiPhone.Text = "拨号限制";
            this.tsmiMultiPhone.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsmiShareArea
            // 
            this.tsmiShareArea.Image = global::CenoCC.Properties.Resources.fuwuqi;
            this.tsmiShareArea.Name = "tsmiShareArea";
            this.tsmiShareArea.Size = new System.Drawing.Size(200, 22);
            this.tsmiShareArea.Tag = "share_area";
            this.tsmiShareArea.Text = "共享域";
            this.tsmiShareArea.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsmiUser
            // 
            this.tsmiUser.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUser.Image")));
            this.tsmiUser.Name = "tsmiUser";
            this.tsmiUser.Size = new System.Drawing.Size(200, 22);
            this.tsmiUser.Tag = "user";
            this.tsmiUser.Text = "用户管理";
            this.tsmiUser.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsmiGateway
            // 
            this.tsmiGateway.Image = global::CenoCC.Properties.Resources._40;
            this.tsmiGateway.Name = "tsmiGateway";
            this.tsmiGateway.Size = new System.Drawing.Size(200, 22);
            this.tsmiGateway.Tag = "gateway";
            this.tsmiGateway.Text = "网关管理";
            this.tsmiGateway.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsmi_route
            // 
            this.tsmi_route.Image = global::CenoCC.Properties.Resources.arrow_switch;
            this.tsmi_route.Name = "tsmi_route";
            this.tsmi_route.Size = new System.Drawing.Size(200, 22);
            this.tsmi_route.Tag = "route";
            this.tsmi_route.Text = "路由管理";
            this.tsmi_route.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsmi_wblist
            // 
            this.tsmi_wblist.Image = global::CenoCC.Properties.Resources.wb;
            this.tsmi_wblist.Name = "tsmi_wblist";
            this.tsmi_wblist.Size = new System.Drawing.Size(200, 22);
            this.tsmi_wblist.Tag = "wblist";
            this.tsmi_wblist.Text = "黑白名单管理";
            this.tsmi_wblist.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tsmi_Power
            // 
            this.tsmi_Power.Image = global::CenoCC.Properties.Resources.cog;
            this.tsmi_Power.Name = "tsmi_Power";
            this.tsmi_Power.Size = new System.Drawing.Size(200, 22);
            this.tsmi_Power.Tag = "power";
            this.tsmi_Power.Text = "权限";
            this.tsmi_Power.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // ShowDialPad_TSMI
            // 
            this.ShowDialPad_TSMI.Image = ((System.Drawing.Image)(resources.GetObject("ShowDialPad_TSMI.Image")));
            this.ShowDialPad_TSMI.Name = "ShowDialPad_TSMI";
            this.ShowDialPad_TSMI.Size = new System.Drawing.Size(200, 22);
            this.ShowDialPad_TSMI.Tag = "0";
            this.ShowDialPad_TSMI.Text = "显示拨号盘";
            this.ShowDialPad_TSMI.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(197, 6);
            this.toolStripSeparator2.Tag = "args";
            // 
            // ParamSet_Tsmi
            // 
            this.ParamSet_Tsmi.Image = global::CenoCC.Properties.Resources.shezhi;
            this.ParamSet_Tsmi.Name = "ParamSet_Tsmi";
            this.ParamSet_Tsmi.Size = new System.Drawing.Size(200, 22);
            this.ParamSet_Tsmi.Tag = "args";
            this.ParamSet_Tsmi.Text = "参数选项";
            this.ParamSet_Tsmi.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(197, 6);
            // 
            // Help_Tsmi
            // 
            this.Help_Tsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowHelp_TSMI,
            this.LogFile_Tsmi,
            this.toolStripSeparator7,
            this.CheckUpdate_TSMI,
            this.AboutUs_TSMI});
            this.Help_Tsmi.Image = ((System.Drawing.Image)(resources.GetObject("Help_Tsmi.Image")));
            this.Help_Tsmi.Name = "Help_Tsmi";
            this.Help_Tsmi.Size = new System.Drawing.Size(200, 22);
            this.Help_Tsmi.Text = "帮助(&H)";
            // 
            // ShowHelp_TSMI
            // 
            this.ShowHelp_TSMI.Enabled = false;
            this.ShowHelp_TSMI.Name = "ShowHelp_TSMI";
            this.ShowHelp_TSMI.ShortcutKeyDisplayString = "Ctrl + F1";
            this.ShowHelp_TSMI.Size = new System.Drawing.Size(182, 22);
            this.ShowHelp_TSMI.Text = "查看帮助";
            // 
            // LogFile_Tsmi
            // 
            this.LogFile_Tsmi.Name = "LogFile_Tsmi";
            this.LogFile_Tsmi.Size = new System.Drawing.Size(182, 22);
            this.LogFile_Tsmi.Text = "系统日志";
            this.LogFile_Tsmi.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(179, 6);
            // 
            // CheckUpdate_TSMI
            // 
            this.CheckUpdate_TSMI.Enabled = false;
            this.CheckUpdate_TSMI.Name = "CheckUpdate_TSMI";
            this.CheckUpdate_TSMI.Size = new System.Drawing.Size(182, 22);
            this.CheckUpdate_TSMI.Text = "检查更新";
            // 
            // AboutUs_TSMI
            // 
            this.AboutUs_TSMI.Name = "AboutUs_TSMI";
            this.AboutUs_TSMI.Size = new System.Drawing.Size(182, 22);
            this.AboutUs_TSMI.Text = "关于";
            this.AboutUs_TSMI.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // System_Tsmi
            // 
            this.System_Tsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Register_Tsmi,
            this.Exit_Tsmi,
            this.Reset_Tsmi});
            this.System_Tsmi.Image = global::CenoCC.Properties.Resources.exit;
            this.System_Tsmi.Name = "System_Tsmi";
            this.System_Tsmi.Size = new System.Drawing.Size(200, 22);
            this.System_Tsmi.Text = "退出";
            // 
            // Register_Tsmi
            // 
            this.Register_Tsmi.Name = "Register_Tsmi";
            this.Register_Tsmi.Size = new System.Drawing.Size(100, 22);
            this.Register_Tsmi.Text = "注册";
            this.Register_Tsmi.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Exit_Tsmi
            // 
            this.Exit_Tsmi.Image = global::CenoCC.Properties.Resources.exit;
            this.Exit_Tsmi.Name = "Exit_Tsmi";
            this.Exit_Tsmi.Size = new System.Drawing.Size(100, 22);
            this.Exit_Tsmi.Text = "退出";
            this.Exit_Tsmi.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // Reset_Tsmi
            // 
            this.Reset_Tsmi.Image = ((System.Drawing.Image)(resources.GetObject("Reset_Tsmi.Image")));
            this.Reset_Tsmi.Name = "Reset_Tsmi";
            this.Reset_Tsmi.Size = new System.Drawing.Size(100, 22);
            this.Reset_Tsmi.Text = "重启";
            this.Reset_Tsmi.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // CallInfo_Pnl
            // 
            this.CallInfo_Pnl.Controls.Add(this.DialTime_Lbl);
            this.CallInfo_Pnl.Controls.Add(this.CallStatus_Lbl);
            this.CallInfo_Pnl.Controls.Add(this.PhoneNum_Contact_Lbl);
            this.CallInfo_Pnl.Location = new System.Drawing.Point(40, 0);
            this.CallInfo_Pnl.Name = "CallInfo_Pnl";
            this.CallInfo_Pnl.Size = new System.Drawing.Size(140, 40);
            this.CallInfo_Pnl.TabIndex = 3;
            // 
            // DialTime_Lbl
            // 
            this.DialTime_Lbl.AutoSize = true;
            this.DialTime_Lbl.Location = new System.Drawing.Point(82, 24);
            this.DialTime_Lbl.Name = "DialTime_Lbl";
            this.DialTime_Lbl.Size = new System.Drawing.Size(53, 12);
            this.DialTime_Lbl.TabIndex = 2;
            this.DialTime_Lbl.Text = "00:00:00";
            // 
            // CallStatus_Lbl
            // 
            this.CallStatus_Lbl.Location = new System.Drawing.Point(5, 23);
            this.CallStatus_Lbl.Name = "CallStatus_Lbl";
            this.CallStatus_Lbl.Size = new System.Drawing.Size(75, 12);
            this.CallStatus_Lbl.TabIndex = 1;
            this.CallStatus_Lbl.Text = "空闲中";
            this.CallStatus_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PhoneNum_Contact_Lbl
            // 
            this.PhoneNum_Contact_Lbl.AutoSize = true;
            this.PhoneNum_Contact_Lbl.Location = new System.Drawing.Point(0, 5);
            this.PhoneNum_Contact_Lbl.MaximumSize = new System.Drawing.Size(137, 12);
            this.PhoneNum_Contact_Lbl.MinimumSize = new System.Drawing.Size(137, 12);
            this.PhoneNum_Contact_Lbl.Name = "PhoneNum_Contact_Lbl";
            this.PhoneNum_Contact_Lbl.Size = new System.Drawing.Size(137, 12);
            this.PhoneNum_Contact_Lbl.TabIndex = 0;
            this.PhoneNum_Contact_Lbl.Text = "00000000000(某某某)";
            this.PhoneNum_Contact_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Call_Flag_Pnl
            // 
            this.Call_Flag_Pnl.BackColor = System.Drawing.Color.Green;
            this.Call_Flag_Pnl.Location = new System.Drawing.Point(34, 34);
            this.Call_Flag_Pnl.Name = "Call_Flag_Pnl";
            this.Call_Flag_Pnl.Size = new System.Drawing.Size(5, 5);
            this.Call_Flag_Pnl.TabIndex = 2;
            this.Call_Flag_Pnl.Visible = false;
            // 
            // NoAnswer_Flag_Pnl
            // 
            this.NoAnswer_Flag_Pnl.BackColor = System.Drawing.Color.Red;
            this.NoAnswer_Flag_Pnl.Location = new System.Drawing.Point(1, 34);
            this.NoAnswer_Flag_Pnl.Name = "NoAnswer_Flag_Pnl";
            this.NoAnswer_Flag_Pnl.Size = new System.Drawing.Size(5, 5);
            this.NoAnswer_Flag_Pnl.TabIndex = 1;
            this.NoAnswer_Flag_Pnl.Visible = false;
            // 
            // PhoneAddress_TT
            // 
            this.PhoneAddress_TT.AutoPopDelay = 30000;
            this.PhoneAddress_TT.InitialDelay = 500;
            this.PhoneAddress_TT.ReshowDelay = 100;
            this.PhoneAddress_TT.ShowAlways = true;
            // 
            // GlobleContextMenu
            // 
            this.GlobleContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.number_GlobelContextMenu_MI,
            this.toolStripSeparator6,
            this.dial_GlobelContextMenu_MI,
            this.tsmiAddZeroDial,
            this.cancel_GlobelContextMenu_MI});
            this.GlobleContextMenu.Name = "GlobleContextMenu";
            this.GlobleContextMenu.Size = new System.Drawing.Size(161, 101);
            // 
            // number_GlobelContextMenu_MI
            // 
            this.number_GlobelContextMenu_MI.Name = "number_GlobelContextMenu_MI";
            this.number_GlobelContextMenu_MI.Size = new System.Drawing.Size(100, 23);
            this.number_GlobelContextMenu_MI.Text = "00000000000";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(157, 6);
            // 
            // dial_GlobelContextMenu_MI
            // 
            this.dial_GlobelContextMenu_MI.Image = global::CenoCC.Properties.Resources.PickUp;
            this.dial_GlobelContextMenu_MI.Name = "dial_GlobelContextMenu_MI";
            this.dial_GlobelContextMenu_MI.Size = new System.Drawing.Size(160, 22);
            this.dial_GlobelContextMenu_MI.Text = "拨号";
            this.dial_GlobelContextMenu_MI.Click += new System.EventHandler(this.dial_GlobelContextMenu_MI_Click);
            // 
            // tsmiAddZeroDial
            // 
            this.tsmiAddZeroDial.Image = global::CenoCC.Properties.Resources.PickUp;
            this.tsmiAddZeroDial.Name = "tsmiAddZeroDial";
            this.tsmiAddZeroDial.Size = new System.Drawing.Size(160, 22);
            this.tsmiAddZeroDial.Text = "加0拨号";
            this.tsmiAddZeroDial.Click += new System.EventHandler(this.dial_GlobelContextMenu_MI_Click);
            // 
            // cancel_GlobelContextMenu_MI
            // 
            this.cancel_GlobelContextMenu_MI.Name = "cancel_GlobelContextMenu_MI";
            this.cancel_GlobelContextMenu_MI.Size = new System.Drawing.Size(160, 22);
            this.cancel_GlobelContextMenu_MI.Text = "取消";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "CenoCC";
            this.notifyIcon.Visible = true;
            // 
            // MinChat
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImage = global::CenoCC.Properties.Resources.Tubiao;
            this.ClientSize = new System.Drawing.Size(180, 40);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.CallInfo_Pnl);
            this.Controls.Add(this.Call_Flag_Pnl);
            this.Controls.Add(this.NoAnswer_Flag_Pnl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(180, 170);
            this.MinimumSize = new System.Drawing.Size(40, 40);
            this.Name = "MinChat";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MinChat_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MinChat_MouseDoubleClick);
            this.contextMenuStrip1.ResumeLayout(false);
            this.CallInfo_Pnl.ResumeLayout(false);
            this.CallInfo_Pnl.PerformLayout();
            this.GlobleContextMenu.ResumeLayout(false);
            this.GlobleContextMenu.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.ToolStripMenuItem HangUp_Tmsi;
        public System.Windows.Forms.ToolStripMenuItem Call_Tmsi;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripMenuItem OpenBrowser_Tsmi;
        public System.Windows.Forms.ToolStripMenuItem Report_Tmsi;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripMenuItem ParamSet_Tsmi;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripMenuItem System_Tsmi;
        public System.Windows.Forms.Panel NoAnswer_Flag_Pnl;
        public System.Windows.Forms.Panel CallInfo_Pnl;
        public System.Windows.Forms.Label CallStatus_Lbl;
        public System.Windows.Forms.Label PhoneNum_Contact_Lbl;
        public System.Windows.Forms.Label DialTime_Lbl;
        public System.Timers.Timer DragTimer;
        public ToolStripMenuItem Help_Tsmi;
        public ToolStripMenuItem CheckUpdate_TSMI;
        public ToolStripMenuItem LogFile_Tsmi;
        public ToolStripMenuItem AboutUs_TSMI;
        public ToolStripMenuItem Reset_Tsmi;
        public ToolStripMenuItem Exit_Tsmi;

        private void MinChat_Load(object sender, EventArgs e) {
            this.Left = Screen.PrimaryScreen.Bounds.Width - 200;
            this.Top = 50;
            this.Width = 40;

            FormClass.AnimateWindow(this.Handle, 1000, FormClass.AW_BLEND);
        }
        private void ToolStripMenuItem_Click(object sender, EventArgs e) {
            switch(((ToolStripMenuItem)sender).Name) {
                case "HangUp_Tmsi":
                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_HUNGUP, (IntPtr)0, (IntPtr)1);
                    break;
                case "Call_Tmsi":
                    //修正,来电显示拨号键,可以接听电话
                    switch (CCFactory.ChInfo[CCFactory.CurrentCh].chStatus)
                    {
                        case ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING:
                            {
                                Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_PICKUP, (IntPtr)0, (IntPtr)1);
                            }
                            break;
                        case ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE:
                            {
                                this.Dial();
                            }
                            break;
                    }
                    break;
                case "Hold_TSMI":
                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_HOLD, (IntPtr)0, (IntPtr)0);
                    break;
                case "Exit_Tsmi":
                    if(CCFactory.IsInCall) {
                        if(Cmn.MsgQ("正在通话中,确定要退出吗"))
                            Application.Restart();
                    } else
                        Application.Exit();
                    break;
                case "Reset_Tsmi":
                    if(CCFactory.IsInCall) {
                        if(Cmn.MsgQ("正在通话中,确定重新启动吗"))
                            Application.Restart();
                    } else
                        Application.Restart();
                    break;
                case "OpenBrowser_Tsmi":
                    this.OpenBrowser();
                    break;
                case "ShowDialPad_TSMI": {
                        if(CCFactory.IsInCall) {
                            if(Convert.ToInt32(ShowDialPad_TSMI.Tag) == 1) {
                                ShowDialPad_TSMI.Tag = 0;
                                ShowDialPad_TSMI.Text = "显示拨号盘";
                                ShowDialPad_TSMI.Image = global::CenoCC.Properties.Resources.calculator_accept;
                                this.Height = 40;
                                this.Width = 180;
                            } else {
                                ShowDialPad_TSMI.Tag = 1;
                                ShowDialPad_TSMI.Text = "关闭拨号盘";
                                ShowDialPad_TSMI.Image = global::CenoCC.Properties.Resources.calculator_remove;
                                if (m_PhoneNumPanel == null) {
                                    m_PhoneNumPanel = new PhoneNumPanel();
                                    m_PhoneNumPanel.Location = new System.Drawing.Point(0, 41);
                                    m_PhoneNumPanel.PhoneNumDown += PhoneNumPanel_PhoneNumDown;
                                    m_PhoneNumPanel.PhoneNumDel += PhoneNumPanel_PhoneNumDel;
                                    m_PhoneNumPanel.PhoneNumDial += PhoneNumPanel_PhoneNumDial;
                                    m_PhoneNumPanel.PhoneNumHungUp += PhoneNumPanel_PhoneNumHungUp;
                                    this.Controls.Add(m_PhoneNumPanel);
                                }
                                this.Size = MaximumSize;
                            }
                            return;
                        }
                        if(CCFactory.ChInfo[CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE) {
                            CallStatus_Lbl.Text = "空闲中";
                            /* 界面显隐 */
                            CallInfo_Pnl.Visible = true;
                            NoAnswer_Flag_Pnl.Visible = false;
                            Call_Flag_Pnl.Visible = false;
                            if(Convert.ToInt32(ShowDialPad_TSMI.Tag) == 1) {
                                ShowDialPad_TSMI.Tag = 0;
                                ShowDialPad_TSMI.Text = "显示拨号盘";
                                ShowDialPad_TSMI.Image = global::CenoCC.Properties.Resources.calculator_accept;
                                if (PhoneNum_Contact_Lbl.Tag == null) {
                                    PhoneNum_Contact_Lbl.Text = "请拨号";
                                    this.Size = MinimumSize;
                                } else if(PhoneNum_Contact_Lbl.Tag != null && PhoneNum_Contact_Lbl.Tag.ToString() == "") {
                                    PhoneNum_Contact_Lbl.Text = "请拨号";
                                    this.Size = MinimumSize;
                                } else {
                                    this.Size = MinimumSize;
                                }
                            } else {
                                ShowDialPad_TSMI.Tag = 1;
                                ShowDialPad_TSMI.Text = "关闭拨号盘";
                                ShowDialPad_TSMI.Image = global::CenoCC.Properties.Resources.calculator_remove;
                                if (m_PhoneNumPanel == null) {
                                    m_PhoneNumPanel = new PhoneNumPanel();
                                    m_PhoneNumPanel.Location = new System.Drawing.Point(0, 41);
                                    m_PhoneNumPanel.PhoneNumDown += PhoneNumPanel_PhoneNumDown;
                                    m_PhoneNumPanel.PhoneNumDel += PhoneNumPanel_PhoneNumDel;
                                    m_PhoneNumPanel.PhoneNumDial += PhoneNumPanel_PhoneNumDial;
                                    m_PhoneNumPanel.PhoneNumHungUp += PhoneNumPanel_PhoneNumHungUp;
                                    this.Controls.Add(m_PhoneNumPanel);
                                }
                                if(PhoneNum_Contact_Lbl.Tag == null)
                                    PhoneNum_Contact_Lbl.Text = "请拨号";
                                else if(PhoneNum_Contact_Lbl.Tag != null && PhoneNum_Contact_Lbl.Tag.ToString() == "")
                                    PhoneNum_Contact_Lbl.Text = "请拨号";
                                this.Size = MaximumSize;
                            }
                        }
                    }
                    break;
                case "ParamSet_Tsmi": {
                        ArgsfrmSimple dialog = new ArgsfrmSimple();
                        dialog.ShowDialog(this);
                    }
                    break;
                case "LogFile_Tsmi":
                    string logpath = Application.StartupPath + "\\Log";
                    string file = DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                    if(File.Exists(logpath + "\\" + file))
                        System.Diagnostics.Process.Start("notepad.exe", logpath + "\\" + file);
                    else
                        System.Diagnostics.Process.Start("notepad.exe", logpath);
                    break;
                case "AboutUs_TSMI":
                    About aboutEntity = new About();
                    aboutEntity.ShowDialog(this);
                    break;
                case "Report_Tmsi":
                    this.m_fOpenWebBrowser(new Report(true));
                    break;
                case "tsmiRecReport":
                    this.m_fOpenWebBrowser(new RecReport(true));
                    break;
                case "tsmiMultiPhone":
                    this.m_fOpenWebBrowser(new diallimit(true));
                    break;
                case "tsmiShareArea":
                    this.m_fOpenWebBrowser(new sharelist(true));
                    break;
                case "tsmi_Power":
                    this.m_fOpenWebBrowser(new powerall(true));
                    break;
                case "tsmiUser":
                    this.m_fOpenWebBrowser(new user(true));
                    break;
                case "tsmiGateway":
                    this.m_fOpenWebBrowser(new gateway(true));
                    break;
                case "tsmi_route":
                    this.m_fOpenWebBrowser(new route(true));
                    break;
                case "tsmi_wblist":
                    this.m_fOpenWebBrowser(new wblist(true));
                    break;
                case "Register_Tsmi":
                    Log.Instance.Success($"[CenoCC][MinChat][ToolStripMenuItem_Click][SIP与WebSocket注册]");
                    InWebSocketMain.SetIsCanLogin(true);
                    InWebSocketMain.ReStart();
                    ///发送一次强断信号
                    InWebSocketMain.Send(M_Send._bhzt__hang("X"));
                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_SIP_RESET, (IntPtr)0, (IntPtr)0);
                    break;
            }
        }

        #region 打开浏览器
        public void OpenBrowser(string _url = null) {

            if (!m_cPower.Has(m_mOperate.browser))
                return;

            if(MinChat.MainBrowserForm == null) {
                MinChat.MainBrowserForm = new Main_Frm();
                MinChat.MainBrowserForm.Show();
            } else {
                MinChat.MainBrowserForm.WindowState = FormWindowState.Normal;
                MinChat.MainBrowserForm.Visible = true;
            }
            MinChat.MainBrowserForm.BringToFront();
            MinChat.MainBrowserForm.AddTab(string.IsNullOrWhiteSpace(_url) ? WebBrowser.BrowserParam.HomeUrl : _url);
        }
        #endregion

        #region 打开窗体
        public void m_fOpenWebBrowser(Form m_fForm)
        {
            if (MinChat.MainBrowserForm == null)
            {
                MinChat.MainBrowserForm = new Main_Frm();
                MinChat.MainBrowserForm.Show();
            }
            else
            {
                MinChat.MainBrowserForm.WindowState = FormWindowState.Normal;
                MinChat.MainBrowserForm.Visible = true;
            }
            MinChat.MainBrowserForm.BringToFront();
            MinChat.MainBrowserForm.AddTab(m_fForm);
        }
        #endregion

        #region 通用操作
        public void Dial(string _number = null, bool _again = false) {
            try {

                /*
                 * 需求一:拨号的时候需要打开浏览器
                 */

                if (BrowserParam.AutoOpenPage == "1" && (!string.IsNullOrEmpty(BrowserParam.HomeUrl)))
                {
                    if (MinChat.MainBrowserForm == null)
                    {
                        MinChat.MainBrowserForm = new Main_Frm();
                        MinChat.MainBrowserForm.Show();
                        MinChat.MainBrowserForm.AddTab(WebBrowser.BrowserParam.HomeUrl);
                    }
                    else
                    {
                        MinChat.MainBrowserForm.WindowState = FormWindowState.Normal;
                        MinChat.MainBrowserForm.Visible = true;
                    }
                }

                this.Focus();

                ///<![CDATA[
                /// 增加号码隐藏逻辑
                /// ]]>

                bool m_bHasSecretNumber = !string.IsNullOrWhiteSpace(MinChat.m_sSecretNumber);

                #region 防止直接拨打,先弹出拨号板
                ///<![CDATA[
                /// 如果未展开的话,弹出号码,状态上面写上提示
                /// 以180为界定
                /// ]]>
                if (string.IsNullOrWhiteSpace(_number))
                {
                    if (this.Width != 180)
                    {
                        if (!string.IsNullOrWhiteSpace(MinChat.m_PhoneNumber))
                        {
                            this.BeginInvoke(new MethodInvoker(() =>
                            {
                                PhoneNum_Contact_Lbl.Tag += MinChat.m_PhoneNumber;
                                if (m_bHasSecretNumber)
                                    PhoneNum_Contact_Lbl.Text = MinChat.m_sSecretNumber;
                                else
                                    PhoneNum_Contact_Lbl.Text = PhoneNum_Contact_Lbl.Tag.ToString();
                                this.CallStatus_Lbl.Text = "重拨";
                            }));
                        }
                        else
                        {
                            this.BeginInvoke(new MethodInvoker(() =>
                            {
                                PhoneNum_Contact_Lbl.Tag = null;
                                PhoneNum_Contact_Lbl.Text = "请拨号";
                                this.CallStatus_Lbl.Text = "空闲中";
                            }));
                        }

                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            this.Width = 180;
                            this.CallInfo_Pnl.Visible = true;
                            CallTimeLength = 0;
                            this.DialTime_Lbl.Text = "00:00:00";
                        }));

                        return;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(MinChat.m_PhoneNumber))
                        {
                            if (string.IsNullOrWhiteSpace(PhoneNum_Contact_Lbl.Tag?.ToString()))
                            {
                                this.BeginInvoke(new MethodInvoker(() =>
                                {
                                    PhoneNum_Contact_Lbl.Tag = null;
                                    PhoneNum_Contact_Lbl.Text = "请拨号";
                                    this.CallStatus_Lbl.Text = "空闲中";
                                }));

                                return;
                            }
                        }
                    }
                }
                #endregion

                this.dailInvokeRequired(_number, _again, m_bHasSecretNumber);
                
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][MinChat][Dial][拨号错误:{ex.Message}]");
            }
        }

        /// <summary>
        /// 拨号,追加一个参数,是否完整无需做任何处理的号码
        /// </summary>
        /// <param name="_number"></param>
        /// <param name="_again"></param>
        /// <param name="m_bComplete"></param>
        private void dailInvokeRequired(string _number = null, bool _again = false, bool m_bHasSecretNumber = false) {

            string _m_sUseNumber = MinChat.m_sUseNumber;
            MinChat.m_sUseNumber = string.Empty;

            if (CCFactory.IsInCall || CCFactory.ChInfo[CurrentCh].chStatus != ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE) {
                if (CCFactory.ChInfo[CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_AUTOCALLING && _again) {

                } else {
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(_number))
            {
                //优先使用输入
                _number = this.PhoneNum_Contact_Lbl.Tag?.ToString()?.Trim();
            }
            if (string.IsNullOrWhiteSpace(_number))
            {
                //没有使用上次拨号缓存
                _number = MinChat.m_PhoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(_number)) {

                this.BeginInvoke(new MethodInvoker(() => {

                    ///<![CDATA[
                    /// 增加号码隐藏逻辑
                    /// ]]>
                    if (m_bHasSecretNumber)
                        this.PhoneNum_Contact_Lbl.Text = MinChat.m_sSecretNumber;
                    else
                        this.PhoneNum_Contact_Lbl.Text = _number;
                    this.PhoneNum_Contact_Lbl.Tag = _number;
                    if (!this.CallInfo_Pnl.Visible)
                        this.CallInfo_Pnl.Visible = true;
                }));

            } else {
                return;
            }

            if (!InWebSocketMain.IsOpen()) {

                this.BeginInvoke(new MethodInvoker(() => {
                    CallStatus_Lbl.Text = "未注册服务器";
                }));

                return;
            }

            if (_number.Length < 3 || _number.Length > 20) {

                if (Call_ClientParamUtil.m_bQNRegexNumber && _number.Length == 32)
                {
                    ///兼容32位加密号码
                }
                else
                {
                    ///弹出错误提示
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        this.CallStatus_Lbl.Text = "号码有误";
                    }));
                    return;
                }
            }

            if (!SipRegister.IsRegister()) {
                if (_again) {
                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_DIALFAIL, (IntPtr)0, Cmn.Sti("二次拨号失败,稍后重试即可"));
                    return;
                }

                Log.Instance.Success($"[CenoCC][MinChat][Dial][拨号:{_number}前重新注册,请稍后...]");
                CCFactory.ChInfo[CurrentCh].uCallType = 1;
                CCFactory.ChInfo[CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_AUTOCALLING;

                this.BeginInvoke(new MethodInvoker(() => {
                    this.CallInfo_Pnl.Visible = true;
                    ShowDialPad_TSMI.Enabled = true;
                    this.Width = 180;
                    CallTimeLength = 0;
                    this.DialTime_Lbl.Text = "00:00:00";
                    SessionNoAnswerFlagTimer.Start();
                    SessionFlagTimer.Stop();
                    Call_Flag_Pnl.Visible = false;
                    SessionTimeLenTimer.Start();
                    CallStatus_Lbl.Text = "请稍后...";
                    this.Call_Tmsi.Enabled = false;
                    this.HangUp_Tmsi.Enabled = true;
                }));
               
                Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_LOAD_STATUS_UNAVAILABLE, Cmn.Sti(_number), (IntPtr)1000);
                return;
            }

            Log.Instance.Success($"[CenoCC][MinChat][Dial][拨号:{_number}]");
            CCFactory.ChInfo[CurrentCh].uCallType = 1;
            CCFactory.ChInfo[CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_AUTOCALLING;

            this.BeginInvoke(new MethodInvoker(() => {
                this.CallInfo_Pnl.Visible = true;
                ShowDialPad_TSMI.Enabled = true;
                this.Width = 180;
                CallStatus_Lbl.Text = "拨号中";
                CallTimeLength = 0;
                this.DialTime_Lbl.Text = "00:00:00";
                SessionNoAnswerFlagTimer.Start();
                SessionFlagTimer.Stop();
                Call_Flag_Pnl.Visible = false;
                SessionTimeLenTimer.Start();
                this.Call_Tmsi.Enabled = false;
                this.HangUp_Tmsi.Enabled = true;
            }));

            SipMain.Stop();
            SipMain.Play("Audio\\a_dial.wav", 1);

            /*
             * 修改一下这里
             * 尽量不要乱处理什么内容
             * 把特殊的外呼也包含在内
             * 解决一:这里只判断是内呼还是外呼
             * 其他一:得到的真实号码对应着判断电话归属地
             */

            bool m_bIsNeedGetContact = false;
            string m_sDt = string.Empty;
            string m_sCardType = string.Empty;
            string m_sZipCode = string.Empty;

            List<string> m_lStrings = m_cPhone.m_fGetPhoneNumberMemo(_number, out m_bIsNeedGetContact, out m_sDt, out m_sCardType, out m_sZipCode);
            m_lStrings.Insert(0, AgentInfo.AgentID);

            ///<![CDATA[
            /// UsrData,自定义的字段的添加,以后就往这里放,尽量不动Socket命令表
            /// 增加录音的关联字段UUID
            /// ]]>
            string m_sUsrData = $"{(Call_ClientParamUtil.m_bQNRegexNumber ? "1" : "0")}|{m_sDt}&{m_sCardType}&{m_sZipCode}|";

            string m_sPhoneAddressStr = m_lStrings[4];
            MinChat.m_PhoneNumber = m_lStrings[2];
            m_cPhone.m_fSetShow(m_lStrings, m_bIsNeedGetContact);

            ///是否自己查询联系人姓名
            int m_uName = Call_ClientParamUtil.m_bName ? 1 : 0;

            /**
             * 增加号码池概念
             * 增加俩个CMD的参数
             * 如果是内线
             */

            string m_sNumberType = Special.Common;
            bool m_bStar = (m_lStrings[3] == Special.Star);
            if (m_bStar || !string.IsNullOrWhiteSpace(_m_sUseNumber))
            {
                //单号码
                if (_m_sUseNumber == Special.LOCAL_1_ || m_bStar)
                {
                    m_lStrings.Add(m_sNumberType);
                    m_lStrings.Add(string.Empty);
                }
                else if (_m_sUseNumber.StartsWith(Special.ADD_LOCAL_))
                {
                    m_lStrings.Add(m_sNumberType);
                    m_lStrings.Add(_m_sUseNumber.Substring(Special.ADD_LOCAL_.Length));
                }
                else if (_m_sUseNumber.StartsWith(Special.ADD_SHARE_))
                {
                    m_lStrings.Add(Special.Share);
                    m_lStrings.Add(_m_sUseNumber.Substring(Special.ADD_SHARE_.Length));
                }

                ///如果是独立服务,处理出来
                if (_m_sUseNumber.StartsWith(Special.ADD_APISHARE_))
                {
                    m_lStrings.Add(Special.ApiShare);
                    ///分割
                    string[] m_lApiShare = _m_sUseNumber.Substring(Special.ADD_APISHARE_.Length).Split('&');
                    m_lStrings.Add(m_lApiShare[0]);
                    m_sUsrData += $"|{m_lApiShare[0]}&{m_lApiShare[1]}|{m_uName}";
                }
                else
                {
                    ///追加自定义参数,全部补齐即可
                    m_sUsrData += $"|&|{m_uName}";
                }

                ///<![CDATA[
                /// 追加UsrData
                /// ]]>
                m_lStrings.Add(m_sUsrData);

                InWebSocketMain.Send(Call_SocketCommandUtil.GetDialInfoStr(m_lStrings.ToArray()));
                return;
            }

            /**
             * 如果是外线
             */
            string m_sTypeUUID = string.Empty;

            ///<![CDATA[
            /// 如果不使用共享号码,并且开启自动专线拨号
            /// ]]>
            if (!Call_ClientParamUtil.m_bIsUseShare && Call_ClientParamUtil.m_bIsUseSpRandom)
            {
                m_sNumberType = Special.Common;
                m_lStrings.Add(m_sNumberType);
                m_lStrings.Add(string.Empty);

                ///追加自定义参数,全部补齐即可
                m_sUsrData += $"|&|{m_uName}";
                m_lStrings.Add(m_sUsrData);

                InWebSocketMain.Send(Call_SocketCommandUtil.GetDialInfoStr(m_lStrings.ToArray()));
                return;
            }

            bool m_bCancel = true;
            if (MinChat.m_pShareNumber != null && !MinChat.m_pShareNumber.IsDisposed)
            {
                MinChat.m_pShareNumber.Close();
                MinChat.m_pShareNumber = null;
            }
            MinChat.m_pShareNumber = new ShareNumber();
            MinChat.m_pShareNumber.TransfEvent += (_m_sNumberType, _m_sTypeUUID, _m_sTag, _m_bCancel) =>
            {
                m_sNumberType = _m_sNumberType;
                m_sTypeUUID = _m_sTypeUUID;
                m_bCancel = _m_bCancel;
                if (m_bCancel)
                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_DIALFAIL, (IntPtr)0, Cmn.Sti("拨号取消"));
                else
                {
                    m_lStrings.Add(m_sNumberType);
                    m_lStrings.Add(m_sTypeUUID);

                    ///追加自定义参数,全部补齐即可
                    m_sUsrData += $"|{_m_sTypeUUID}&{_m_sTag}|{m_uName}";
                    m_lStrings.Add(m_sUsrData);

                    InWebSocketMain.Send(Call_SocketCommandUtil.GetDialInfoStr(m_lStrings.ToArray()));
                }
            };
            MinChat.m_pShareNumber.Show(this);  
        }

        private void HungUp() {
            if(CCFactory.ChInfo[CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE)
                return;
            SipMain.Stop();
            SessionControl.Phone_Temminate("A");
            CCFactory.ChInfo[CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
            ShowDialPad_TSMI.Enabled = true;
            SessionTimeLenTimer.Stop();
            SessionNoAnswerFlagTimer.Stop();
            SessionFlagTimer.Stop();
            Call_Flag_Pnl.Visible = false;
            NoAnswer_Flag_Pnl.Visible = false;
            PhoneNum_Contact_Lbl.Text = "";
            PhoneNum_Contact_Lbl.Tag = null;
            ShowDialPad_TSMI.Tag = 0;
            ShowDialPad_TSMI.Text = "显示拨号盘";
            ShowDialPad_TSMI.Image = global::CenoCC.Properties.Resources.calculator_accept;
            CallStatus_Lbl.Text = "空闲中";
            CallStatus_Lbl.Text = "";
            CallTimeLength = 0;
            DialTime_Lbl.Text = "00:00:00";
            Width = 40;
            Height = 40;
            CallInfo_Pnl.Visible = false;
        }

        public void PhoneNumPanel_PhoneNumHungUp() {
            Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_HUNGUP, (IntPtr)0, (IntPtr)1);
        }

        public void PhoneNumPanel_PhoneNumDial() {
            //修正,来电显示拨号键,可以接听电话
            switch (CCFactory.ChInfo[CCFactory.CurrentCh].chStatus)
            {
                case ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING:
                    {
                        Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_PICKUP, (IntPtr)0, (IntPtr)1);
                    }
                    break;
                case ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE:
                    {
                        this.Dial();
                    }
                    break;
            }
        }

        public void PhoneNumPanel_PhoneNumDel() {
            if (CCFactory.ChInfo[CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE)
            {
                ///<![CDATA[
                /// 增加号码隐藏逻辑
                /// ]]>

                if (!string.IsNullOrWhiteSpace(MinChat.m_sSecretNumber))
                {
                    MinChat.m_PhoneNumber = string.Empty;
                    MinChat.m_sSecretNumber = string.Empty;
                    this.PhoneNum_Contact_Lbl.Tag = null;
                }

                var v1 = this.PhoneNum_Contact_Lbl.Tag?.ToString();
                var v2 = string.Empty;
                if (!string.IsNullOrWhiteSpace(v1))
                {
                    v2 = v1.ToString();
                    v2 = v2.Remove(v2.Length - 1);
                    PhoneNum_Contact_Lbl.Tag = v2;
                }
                PhoneNum_Contact_Lbl.Text = v2;
                if (string.IsNullOrWhiteSpace(PhoneNum_Contact_Lbl.Text.Trim()))
                    PhoneNum_Contact_Lbl.Text = "请拨号";

                this.Width = 180;
                this.CallStatus_Lbl.Text = "空闲中";
                this.CallInfo_Pnl.Visible = true;
                CallTimeLength = 0;
                this.DialTime_Lbl.Text = "00:00:00";
            }
        }

        public bool PhoneNumPanel_PhoneNumDown(string phoneNum)
        {
            if (CCFactory.ChInfo[CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING)
            {
                if (phoneNum != null && phoneNum.Length == 1)
                {
                    if (!CCFactory.m_bIsEnterNumber)
                    {
                        CCFactory.m_bIsEnterNumber = true;
                        PhoneNum_Contact_Lbl.Text = phoneNum;
                    }
                    else
                    {
                        if (PhoneNum_Contact_Lbl.Text.Length >= 22)
                            PhoneNum_Contact_Lbl.Text = PhoneNum_Contact_Lbl.Text.Trim().Substring(1);
                        PhoneNum_Contact_Lbl.Text += phoneNum;
                    }

                    //dtmf发送方式
                    switch (Call_ParamUtil.m_sDTMFSendMethod)
                    {
                        case Call_ParamUtil.clientSignal:
                            {
                                SipMain.Play("Audio\\cmn.wav", 1);
                                SipMain.SendDTMF(phoneNum);
                            }
                            break;
                        case Call_ParamUtil.inbound:
                            {
                                SipParam.m_pCall.DtmfMsg(phoneNum);
                            }
                            break;
                        case Call_ParamUtil.bothSignal:
                            {
                                SipMain.Play("Audio\\cmn.wav", 1);
                                SipParam.m_pCall.DtmfMsg(phoneNum);
                            }
                            break;
                    }
                }
                return true;
            }

            if (CCFactory.ChInfo[CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE)
            {
                this.Width = 180;
                this.CallStatus_Lbl.Text = "空闲中";
                this.CallInfo_Pnl.Visible = true;
                CallTimeLength = 0;
                this.DialTime_Lbl.Text = "00:00:00";
                if (this.PhoneNum_Contact_Lbl.Tag == null) this.PhoneNum_Contact_Lbl.Tag = "";

                ///<![CDATA[
                /// 增加号码隐藏逻辑
                /// ]]>
                bool m_bHasSecretNumber = !string.IsNullOrWhiteSpace(MinChat.m_sSecretNumber);
                if (m_bHasSecretNumber)
                {
                    if (phoneNum?.Length == 1)
                    {
                        MinChat.m_PhoneNumber = string.Empty;
                        MinChat.m_sSecretNumber = string.Empty;
                        this.PhoneNum_Contact_Lbl.Tag = phoneNum;
                        this.PhoneNum_Contact_Lbl.Text = this.PhoneNum_Contact_Lbl.Tag.ToString();
                    }
                    else
                    {
                        this.PhoneNum_Contact_Lbl.Tag += phoneNum;
                        this.PhoneNum_Contact_Lbl.Text = MinChat.m_sSecretNumber;
                    }
                }
                else
                {
                    this.PhoneNum_Contact_Lbl.Tag += phoneNum;
                    this.PhoneNum_Contact_Lbl.Text = this.PhoneNum_Contact_Lbl.Tag.ToString();
                }
            }
            return true;
        }
        #endregion

        private void SessionNoAnswerFlagTimer_Tick(object sender, EventArgs e) {
            if(this.NoAnswer_Flag_Pnl.InvokeRequired)
                this.NoAnswer_Flag_Pnl.BeginInvoke(new MethodInvoker(delegate () {
                    if(this.NoAnswer_Flag_Pnl.Visible)
                        this.NoAnswer_Flag_Pnl.Visible = false;
                    else
                        this.NoAnswer_Flag_Pnl.Visible = true;
                }));
            else {
                if(this.NoAnswer_Flag_Pnl.Visible)
                    this.NoAnswer_Flag_Pnl.Visible = false;
                else
                    this.NoAnswer_Flag_Pnl.Visible = true;
            }
        }

        private void SessionTimeLenTimer_Timer_Tick(object sender, EventArgs e) {
            if(this.DialTime_Lbl.InvokeRequired)
                this.DialTime_Lbl.BeginInvoke(new MethodInvoker(delegate () {
                    this.DialTime_Lbl.Text = (new DateTime().AddSeconds(CallTimeLength++)).ToString("HH:mm:ss");
                }));
            else
                this.DialTime_Lbl.Text = (new DateTime().AddSeconds(CallTimeLength++)).ToString("HH:mm:ss");

        }

        private void SessionFlagTimer_Tick(object sender, EventArgs e) {
            if(this.Call_Flag_Pnl.InvokeRequired)
                this.Call_Flag_Pnl.BeginInvoke(new MethodInvoker(delegate () {
                    if(this.Call_Flag_Pnl.Visible)
                        this.Call_Flag_Pnl.Visible = false;
                    else
                        this.Call_Flag_Pnl.Visible = true;
                }));
            else {
                if(this.Call_Flag_Pnl.Visible)
                    this.Call_Flag_Pnl.Visible = false;
                else
                    this.Call_Flag_Pnl.Visible = true;
            }
        }

        public Panel Call_Flag_Pnl;
        public ToolTip PhoneAddress_TT;
        public ToolStripMenuItem CurrentStatus_TSMI;
        public ToolStripMenuItem ShowDialPad_TSMI;
        private ToolStripSeparator toolStripSeparator4;
        public ToolStripMenuItem Hold_TSMI;
        public ToolStripMenuItem Transfer_TSMI;
        private ToolStripMenuItem RecentNoanswerCalls_TSMI;
        private ToolStripMenuItem AgentInfo_TSMI;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem ShowHelp_TSMI;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem Register_Tsmi;
        private ContextMenuStrip GlobleContextMenu;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem dial_GlobelContextMenu_MI;
        private ToolStripMenuItem cancel_GlobelContextMenu_MI;
        private ToolStripTextBox number_GlobelContextMenu_MI;
        private ToolStripMenuItem tsmiMultiPhone;
        private ToolStripMenuItem tsmiUser;
        private ToolStripMenuItem tsmiAddZeroDial;
        private ToolStripMenuItem tsmiRecReport;
        private ToolStripMenuItem tsmiShareArea;
        private ToolStripMenuItem tsmi_Power;
        private ToolStripMenuItem tsmiGateway;
        private ToolStripMenuItem tsmi_route;
        public NotifyIcon notifyIcon;
        private ToolStripMenuItem tsmi_wblist;
    }
}

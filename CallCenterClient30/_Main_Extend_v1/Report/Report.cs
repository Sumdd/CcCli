using Cmn_v1;
using Common;
using Core_v1;
using LumiSoft.Net.FTP;
using Query_v1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataBaseUtil;
using Model_v1;

namespace CenoCC {
    public partial class Report : _index {
        private QueryPager qop;
        private DataSet m_ds;
        private bool m_downLoadEnd = true;
        private bool m_bDoing = false;
        private bool m_bName = false;
        /// <summary>
        /// 统计表构造函数
        /// </summary>
        public Report(bool uc = false) {
            InitializeComponent();
            if(uc) {
                this.FormBorderStyle = FormBorderStyle.None;
            }

            ///直接赋值即可
            this.m_bName = Call_ClientParamUtil.m_bName;

            this.m_bResetArgs = false;
            this.SearchEvent += new EventHandler(GetListBody);
            this.ucPager.PageSkipEvent += new EventHandler(GetListBody);
            this.LoadListHeader();
            this.defaultArgs(this, null);
            this.GetListBody(this, null);

            ///操作权限
            this.m_fLoadOperatePower(this.Controls);
        }

        #region ***操作权限
        private void m_fLoadOperatePower(Control.ControlCollection m_lControls)
        {
            foreach (var item in m_lControls)
            {
                if (item.GetType() == typeof(Button))
                {
                    Button m_pButton = (Button)item;
                    if (m_pButton.Tag == null)
                        continue;
                    if (string.IsNullOrWhiteSpace(m_pButton.Tag.ToString()))
                        continue;
                    if (m_cPower.Has(m_pButton.Tag.ToString()))
                        m_pButton.Enabled = true;
                    else
                        m_pButton.Enabled = false;
                }
                else if (item.GetType() == typeof(Panel))
                {
                    Panel m_pPanel = (Panel)item;
                    this.m_fLoadOperatePower(m_pPanel.Controls);
                }
            }
        }
        #endregion

        /// <summary>
        /// 打开查询条件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchOpen_Click(object sender, EventArgs e) {
            if(this.searchEntity == null) {
                this.searchEntity = new ReportSearch(this);
                this.searchEntity.SearchEvent += new EventHandler(GetListBody);
                this.searchEntity.defaultArgsEvent += new EventHandler(defaultArgs);
                this.searchEntity.SetQueryMark();
                this.searchEntity.Show(this);
            }
        }
        private void defaultArgs(object sender, EventArgs e) {
            this.args = new Dictionary<string, object>();
            var _time = DateTime.Now.ToString("yyyy-MM-dd");
            this.args.Add("useStartDateTime", "true");
            this.args.Add("startDateTimeMark", ">=");
            this.args.Add("startDateTime", $"{_time} 00:00:00");
            this.args.Add("useEndDateTime", "true");
            this.args.Add("endDateTimeMark", "<=");
            this.args.Add("endDateTime", $"{_time} 23:59:59");
            this.args.Add("isshare", "0");
        }
        /// <summary>
        /// 下载所有查询出的文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downloadSearch_Click(object sender, EventArgs e) {
            if(!this.m_downLoadEnd) {
                Cmn.MsgWran("已有下载任务,请等待或手动关闭");
                return;
            }

            if (this.m_bDoing)
            {
                MessageBox.Show(this, "有查询任务正在执行,请稍后");
                return;
            }

            if (DialogResult.Yes == Cmn.MsgQuestion("确定要下载全部录音")) {
                this.m_downLoadEnd = false;
                if(this.qop != null) {
                    this.qop.isGetTotal = true;
                    DataSet _ds = this.qop.QdataSet();
                    if(_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0) {
                        DownLoad downloadEntity = new DownLoad(_ds);
                        downloadEntity.downloadExitEvent += new DownLoad.downloadExit(this.downloadExit);
                        downloadEntity.Show(this);
                    } else {
                        Cmn.MsgWran("无任何查询数据");
                    }
                } else {
                    string msg = $"查询错误,请重新尝试";
                    Log.Instance.Fail($"[CenoCC][Report][downloadSearch_Click][{msg}]");
                    Cmn.MsgError(msg);
                }
            }
        }
        /// <summary>
        /// 下载当前页录音
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downloadPage_Click(object sender, EventArgs e) {
            if(!this.m_downLoadEnd) {
                Cmn.MsgWran("已有下载任务,请等待或手动关闭");
                return;
            }

            if (this.m_bDoing)
            {
                MessageBox.Show(this, "有查询任务正在执行,请稍后");
                return;
            }

            if (DialogResult.Yes == Cmn.MsgQuestion("确定要下载当前页录音")) {
                this.m_downLoadEnd = false;
                if(this.m_ds != null && this.m_ds.Tables.Count > 0 && this.m_ds.Tables[0].Rows.Count > 0) {
                    DownLoad downloadEntity = new DownLoad(this.m_ds);
                    downloadEntity.downloadExitEvent += new DownLoad.downloadExit(this.downloadExit);
                    downloadEntity.Show(this);
                } else {
                    Cmn.MsgWran("无任何查询数据");
                }
            }
        }
        /// <summary>
        /// 下载任务结束
        /// </summary>
        /// <param name="_downLoadEnd"></param>
        private void downloadExit() {
            this.m_downLoadEnd = true;
        }
        /// <summary>
        /// 加载列表表头
        /// </summary>
        private void LoadListHeader() {
            this.list.BeginUpdate();
            this.list.Columns.Add(new ColumnHeader() { Text = "序号", Width = 50 });
            this.list.Columns.Add(new ColumnHeader() { Name = "b.TypeName", Text = "通话类型", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "c.AgentName", Text = "业务员", Width = 200, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.LocalNum", Text = "坐席电话", Width = 90, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.T_PhoneNum", Text = "电话", Width = 90, ImageIndex = 0 });

            if (this.m_bName)
            {
                this.list.Columns.Add(new ColumnHeader() { Name = "realname", Text = "联系人姓名", Width = 125, ImageIndex = 0 });
            }

            this.list.Columns.Add(new ColumnHeader() { Name = "a.PhoneAddress", Text = "归属地", Width = 90, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.C_StartTime", Text = "拨打时间", Width = 130, ImageIndex = 2, Tag = "desc" });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.C_SpeakTime", Text = "通话时长", Width = 90, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "d.R_Description", Text = "通话结果", Width = 350, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.RecordFile", Text = "录音", Width = 575, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.Remark", Text = "备注", Width = 240, ImageIndex = 0 });
            this.list.EndUpdate();
            this.ucPager.pager.field = "a.C_StartTime";
            this.ucPager.pager.type = "desc";
        }
        /// <summary>
        /// 加载列表内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetListBody(object sender, EventArgs e)
        {

            if (this.m_bDoing)
            {
                MessageBox.Show(this, "有查询任务正在执行,请稍后");
                return;
            }

            string m_sFieldString = string.Empty;
            if (this.m_bName)
            {
                m_sFieldString = $@"
    (
	    SELECT
		    `exp_contact`.`realname` 
	    FROM
		    `exp_contact` 
	    WHERE
		    ( CASE WHEN IFNULL( `a`.`tnumber`, '' ) = '' THEN `a`.`T_PhoneNum` ELSE `a`.`T_PhoneNum` END ) = `exp_contact`.`number` 
	    ORDER BY
		    `exp_contact`.`updatetime` 
		    LIMIT 1 
	) AS `realname`, 
";
            }

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                this.m_bDoing = true;

                try
                {
                    this.qop = new QueryPager();
                    this.qop.FieldsSqlPart = $@"SELECT 
	CONCAT(b.Remark,'(',b.TypeName,')') as CallTypeName,
    case when ifnull(a.tnumber,'') = '' then a.LocalNum
         else a.tnumber end as LocalNum,
    case when a.isshare > 0 then concat(ifnull(e.aname,''),' ',ifnull(a.fromagentname,''))
                            else c.AgentName end as AgentName,
    a.FreeSWITCHIPv4 as m_sFreeSWITCHIPv4,
    a.AgentID as m_uAgentID,
	a.T_PhoneNum,
    {m_sFieldString}
    a.PhoneAddress,
	a.C_StartTime,
	SEC_TO_TIME(a.C_SpeakTime) as C_SpeakTime,
	a.CallResultID, 
	d.R_Description,
	a.RecordFile,
    a.Remark,
    a.ID,
    a.C_PhoneNum";
                    this.qop.FromSqlPart = @"from call_record as a
left join call_calltype b on a.CallType = b.id
left join call_agent c on c.id = a.AgentID
left join call_callresult d on d.id = a.callresultid
left join dial_area e on e.aip = a.FreeSWITCHIPv4
-- left join call_agent f on f.id = a.fromagentid 
";
                    this.qop.SumSqlPart = "IFNULL(COUNT(1),0) AS totalCount, IFNULL(SUM(a.C_SpeakTime),0) AS totalTime";
                    this.qop.pager = this.ucPager.pager;
                    this.qop.setQuerySample(args);
                    //坐席号码(原)
                    if (args != null && args.ContainsKey("localPhone"))
                    {
                        string localPhoneMark = args["localPhoneMark"].ToString();
                        switch (localPhoneMark)
                        {
                            case "Like":
                                this.qop.appQuery($" AND ( ( IFNULL(a.tnumber,'') = '' AND a.LocalNum {localPhoneMark} CONCAT('%','{args["localPhone"]}','%') ) OR ( IFNULL(a.tnumber,'') != '' AND a.tnumber {localPhoneMark} CONCAT('%','{args["localPhone"]}','%') ) ) ");
                                break;
                            case "=":
                                this.qop.appQuery($" AND ( ( IFNULL(a.tnumber,'') = '' AND a.LocalNum {localPhoneMark} '{args["localPhone"]}' ) OR ( IFNULL(a.tnumber,'') != '' AND a.tnumber {localPhoneMark} '{args["localPhone"]}' ) ) ");
                                break;
                        }
                    }
                    //电话
                    this.qop.setQuery("a.T_PhoneNum", "phone");
                    //拨打时间起
                    if (args != null && args.ContainsKey("useStartDateTime") && Convert.ToBoolean(args["useStartDateTime"]))
                        this.qop.setQuery("a.C_StartTime", "startDateTime");
                    //拨打时间止
                    if (args != null && args.ContainsKey("useEndDateTime") && Convert.ToBoolean(args["useEndDateTime"]))
                        this.qop.setQuery("a.C_StartTime", "endDateTime");
                    //通话时长起
                    if (args != null && args.ContainsKey("useStartSpeakTime") && Convert.ToBoolean(args["useStartSpeakTime"]))
                        this.qop.setQuery("a.C_SpeakTime", "startSpeakTime", "TIME_TO_SEC");
                    //通话时长止
                    if (args != null && args.ContainsKey("useEndSpeakTime") && Convert.ToBoolean(args["useEndSpeakTime"]))
                        this.qop.setQuery("a.C_SpeakTime", "endSpeakTime", "TIME_TO_SEC");
                    //处理电话类型
                    if (args != null && args.ContainsKey("callArgs"))
                        this.qop.appQuery($" AND a.CallType IN ({args["callArgs"]}) ");
                    //业务员
                    if (args != null && args.ContainsKey("agent"))
                        this.qop.appQuery($" AND a.isshare = 0 and c.ID = {args["agent"]} ");
                    //号码类别
                    this.qop.setQuery("a.isshare", "isshare");
                    ///权限
                    PopedomArgs popedomArgs = new PopedomArgs();
                    popedomArgs.type = DataPowerType._data_phonerecords_search;
                    popedomArgs.left.Add("c.ID");
                    this.qop.appQuery($" AND ( a.isshare > 0 OR ( a.isshare = 0 {m_cPower.m_fPopedomSQL(popedomArgs)} ) ) ");
                    ///姓名
                    if (args != null && args.ContainsKey("agentName"))
                    {
                        string agentNameMark = args["agentNameMark"].ToString();
                        switch (agentNameMark)
                        {
                            case "Like":
                                this.qop.appQuery($" AND ( ( a.isshare = 0 AND c.AgentName {agentNameMark} CONCAT('%','{args["agentName"]}','%') ) OR ( a.isshare > 0 AND a.fromagentname {agentNameMark} CONCAT('%','{args["agentName"]}','%') ) ) ");
                                break;
                            case "=":
                                this.qop.appQuery($" AND ( ( a.isshare = 0 AND c.AgentName {agentNameMark} CONCAT('','{args["agentName"]}','') ) OR ( a.isshare > 0 AND a.fromagentname {agentNameMark} CONCAT('','{args["agentName"]}','') ) ) ");
                                break;
                        }
                    }
                    ///登录名
                    if (args != null && args.ContainsKey("loginName"))
                    {
                        string loginNameMark = args["loginNameMark"].ToString();
                        switch (loginNameMark)
                        {
                            case "Like":
                                this.qop.appQuery($" AND ( ( a.isshare = 0 AND c.LoginName {loginNameMark} CONCAT('%','{args["loginName"]}','%') ) OR ( a.isshare > 0 AND a.fromloginname {loginNameMark} CONCAT('%','{args["loginName"]}','%') ) ) ");
                                break;
                            case "=":
                                this.qop.appQuery($" AND ( ( a.isshare = 0 AND c.LoginName {loginNameMark} CONCAT('','{args["loginName"]}','') ) OR ( a.isshare > 0 AND a.fromloginname {loginNameMark} CONCAT('','{args["loginName"]}','') ) ) ");
                                break;
                        }
                    }
                    ///查询
                    DataSet ds = this.qop.QdataSet(60 * 60);
                    //缓存当页数据,以便进行当前页录音下载
                    if (this.m_ds != null)
                    {
                        this.m_ds.Clear();
                        this.m_ds.Dispose();
                        this.m_ds = null;
                    }
                    this.m_ds = new DataSet();
                    this.m_ds.Tables.Add(ds.Tables[1].Copy());
                    int pageIndexStart = this.ucPager.PageIndexStart;
                    this.list.BeginUpdate();
                    this.list.Items.Clear();
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        ListViewItem listViewItem = new ListViewItem($"{pageIndexStart++}");
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "CallTypeName", Text = dr["CallTypeName"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "AgentName", Text = dr["AgentName"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "LocalNum", Text = dr["LocalNum"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "T_PhoneNum", Text = dr["T_PhoneNum"].ToString() });

                        if (this.m_bName)
                        {
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "realname", Text = dr["realname"].ToString() });
                        }

                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "PhoneAddress", Text = dr["PhoneAddress"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "C_StartTime", Text = dr["C_StartTime"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "C_SpeakTime", Text = dr["C_SpeakTime"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "R_Description", Text = dr["R_Description"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "RecordFile", Text = dr["RecordFile"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "Remark", Text = dr["Remark"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "ID", Text = dr["ID"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "C_PhoneNum", Text = dr["C_PhoneNum"].ToString() });
                        this.list.Items.Add(listViewItem);
                    }
                    this.list.EndUpdate();
                    this.totalCount.Text = ds.Tables[2].Rows[0]["totalCount"].ToString();
                    try
                    {
                        var time = Convert.ToInt32(ds.Tables[2].Rows[0]["totalTime"]);
                        TimeSpan ts = new TimeSpan(0, 0, time);
                        this.totalTime.Text = string.Format("{0}{1}:{2}:{3}",
                            ts.Days > 0 ? ts.Days.ToString("00 ") : "",
                            ts.Hours > 0 ? ts.Hours.ToString("00") : "00",
                            ts.Minutes > 0 ? ts.Minutes.ToString("00") : "00",
                            ts.Seconds > 0 ? ts.Seconds.ToString("00") : "00");
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][Report][GetListBody][Exception][{ex.Message}]");
                    }
                    this.ucPager.Set(Convert.ToInt32(ds.Tables[0].Rows[0]["total"]));
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][Report][GetListBody][Exception][{ex.Message}]");
                }
                finally
                {
                    this.m_bDoing = false;
                }
            })).Start();
        }

        /// <summary>
        /// 列表鼠标右键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_MouseClick(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Right) {
                if(this.list.SelectedItems == null) {
                    this.callNumber.Enabled = false;
                    this.callNumber.Visible = false;
                    this.callNow.Enabled = false;
                    this.callNow.Visible = false;
                    this.tsmiAddZeroDial.Enabled = false;
                    this.tsmiAddZeroDial.Visible = false;
                    this.recordUpload.Enabled = false;
                    this.recordUpload.ToolTipText = "无选中项";
                    this.tsmiRecordListenTest.Enabled = false;
                    this.tsmiRecordListenTest.Visible = false;
                } else if(this.list.SelectedItems.Count == 1) {
                    var _recordPath = this.list.SelectedItems[0].SubItems["RecordFile"].Text;
                    if(!string.IsNullOrWhiteSpace(_recordPath)) {
                        var _callResult = this.list.SelectedItems[0].SubItems["R_Description"].Text;
                        if(string.IsNullOrWhiteSpace(_callResult)) {
                            this.recordUpload.Enabled = false;
                            this.recordUpload.ToolTipText = "录音未完成";
                            this.tsmiRecordListenTest.Enabled = false;
                            this.tsmiRecordListenTest.ToolTipText = "录音未完成";
                        } else {
                            this.recordUpload.Enabled = true;
                            this.recordUpload.ToolTipText = "录音下载";
                            this.tsmiRecordListenTest.Enabled = true;
                            this.tsmiRecordListenTest.ToolTipText = "录音试听";
                        }
                    } else {
                        this.recordUpload.Enabled = false;
                        this.recordUpload.ToolTipText = "无录音";
                        this.tsmiRecordListenTest.Enabled = false;
                        this.tsmiRecordListenTest.ToolTipText = "无录音";
                    }
                    string m_sColumnNameStr = Call_ClientParamUtil.m_bAutoAddNumDialFlag ? "T_PhoneNum" : "C_PhoneNum";
                    var _t_PhoneNum = this.list.SelectedItems[0].SubItems[m_sColumnNameStr].Text;
                    if(!string.IsNullOrWhiteSpace(_t_PhoneNum)) {
                        if(CCFactory.IsInCall || CCFactory.ChInfo[CCFactory.CurrentCh].chStatus != ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE) {
                            this.callNumber.Text = _t_PhoneNum;
                            this.callNumber.Enabled = true;
                            this.callNumber.Visible = true;
                            if (CCFactory.ChInfo[CCFactory.CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_UNAVAILABLE)
                            {
                                this.callNow.Text = "请检测状态...";
                                this.tsmiAddZeroDial.Text = "请检测状态...";
                            }
                            else
                            {
                                this.callNow.Text = "通话中,请稍后...";
                                this.tsmiAddZeroDial.Text = "通话中,请稍后...";
                            }
                            this.callNow.Enabled = false;
                            this.callNow.Visible = true;
                            this.tsmiAddZeroDial.Enabled = false;
                            this.tsmiAddZeroDial.Visible = true;
                        } else {
                            this.callNumber.Text = _t_PhoneNum;
                            this.callNumber.Enabled = true;
                            this.callNumber.Visible = true;
                            this.callNow.Text = "拨号";
                            this.callNow.Enabled = true;
                            this.callNow.Visible = true;
                            this.tsmiAddZeroDial.Text = "加0拨号";
                            this.tsmiAddZeroDial.Enabled = true;
                            this.tsmiAddZeroDial.Visible = true;
                        }

                        //是否显示加零拨打
                        if (Call_ClientParamUtil.m_bAutoAddNumDialFlag)
                            this.tsmiAddZeroDial.Visible = false;
                        else
                            this.tsmiAddZeroDial.Visible = true;

                    } else {
                        this.callNumber.Text = string.Empty;
                        this.callNumber.Enabled = false;
                        this.callNumber.Visible = false;
                        this.callNow.Text = "拨号";
                        this.callNow.Enabled = false;
                        this.callNow.Visible = false;
                        this.tsmiAddZeroDial.Text = "加0拨号";
                        this.tsmiAddZeroDial.Enabled = false;
                        this.tsmiAddZeroDial.Visible = false;
                    }
                } else {
                    this.callNumber.Text = string.Empty;
                    this.callNumber.Enabled = false;
                    this.callNumber.Visible = false;
                    this.callNow.Enabled = false;
                    this.callNow.Visible = false;
                    this.tsmiAddZeroDial.Enabled = false;
                    this.tsmiAddZeroDial.Visible = false;
                    this.recordUpload.Enabled = true;
                    this.recordUpload.ToolTipText = "录音下载";
                    this.tsmiRecordListenTest.Enabled = false;
                    this.tsmiRecordListenTest.ToolTipText = "录音试听";
                }

                ///操作权限
                if (!m_cPower.Has(m_mOperate.phonerecords_listen))
                {
                    this.tsmiRecordListenTest.Enabled = false;
                    this.tsmiRecordListenTest.ToolTipText = "无权限";
                }
                if (!m_cPower.Has(m_mOperate.phonerecords_download))
                {
                    this.recordUpload.Enabled = false;
                    this.recordUpload.ToolTipText = "无权限";
                }

                this.contextListMenu.Show(list, e.Location);
            }
        }
        /// <summary>
        /// 列表右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextListMenu_Click(object sender, EventArgs e) {
            try {
                switch(((ToolStripMenuItem)sender).Name) {
                    case "callNow": {
                            if(this.list.SelectedItems != null && this.list.SelectedItems.Count > 0) {
                                var _ = this.callNumber.Text.Trim();
                                if(!string.IsNullOrWhiteSpace(_)) {
                                    Log.Instance.Success($"[CenoCC][Report][metroListMenu_Click][callNow][拨号:{_}]");
                                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_DAIL, (IntPtr)0, Cmn.Sti(_));
                                }
                            }
                        }
                        break;
                    case "tsmiAddZeroDial": {
                            if(this.list.SelectedItems != null && this.list.SelectedItems.Count > 0) {
                                var _ = this.callNumber.Text.Trim();
                                if(!string.IsNullOrWhiteSpace(_)) {
                                    Log.Instance.Success($"[CenoCC][Report][metroListMenu_Click][tsmiAddZeroDial][加0拨号:0{_}]");
                                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_DAIL, (IntPtr)0, Cmn.Sti($"0{_}"));
                                }
                            }
                        }
                        break;
                    case "recordUpload": {
                            if(!this.m_downLoadEnd) {
                                Cmn.MsgWran("已有下载任务,请等待或手动关闭");
                                return;
                            }
                            if(DialogResult.Yes == Cmn.MsgQuestion("确定要下载此录音")) {
                                this.m_downLoadEnd = false;
                                if(this.list.SelectedItems != null && this.list.SelectedItems.Count > 0) {
                                    DataTable dt = new DataTable();
                                    dt.Columns.Add("ID", typeof(int));
                                    dt.Columns.Add("RecordFile", typeof(string));
                                    lock(this.list) {
                                        foreach(ListViewItem listViewItem in this.list.SelectedItems) {
                                            DataRow dr = dt.NewRow();
                                            dr["ID"] = listViewItem.SubItems["ID"].Text;
                                            dr["RecordFile"] = listViewItem.SubItems["RecordFile"].Text;
                                            dt.Rows.Add(dr);
                                        }
                                    }
                                    DataSet ds = new DataSet();
                                    ds.Tables.Add(dt);
                                    DownLoad downloadEntity = new DownLoad(ds);
                                    downloadEntity.downloadExitEvent += new DownLoad.downloadExit(this.downloadExit);
                                    downloadEntity.Show(this);
                                }
                            }
                        }
                        break;
                    case "tsmiRecordListenTest":
                        if (Cmn.MsgQ("确定要试听此录音"))
                        {
                            if (this.list.SelectedItems.Count == 1)
                            {
                                string m_sRecordFileString = this.list.SelectedItems[0].SubItems["RecordFile"].Text;
                                if (!string.IsNullOrWhiteSpace(m_sRecordFileString))
                                {
                                    if (!string.IsNullOrWhiteSpace(Call_ParamUtil.m_sDialTaskRecDownLoadHTTP))
                                    {
                                        string m_sDialTaskRecDownLoadHTTP = Call_ParamUtil.m_sDialTaskRecDownLoadHTTP.TrimEnd('/');
                                        string m_sFilePathString = Call_ParamUtil.ReplacePath(m_sRecordFileString);
                                        string m_sSureHttpPath = $"{m_sDialTaskRecDownLoadHTTP}/{Cmn.PathFmt(m_sFilePathString, "/").TrimStart('/')}";
                                        MediaPlayerFrm m_pMediaPlayer = new MediaPlayerFrm(m_sSureHttpPath);
                                        m_pMediaPlayer.BeginSearchFile();
                                        m_pMediaPlayer.Show(this);
                                    }
                                    else
                                    {
                                        DataTable dt = Call_ServerListUtil.GetFtpServerInfo();
                                        string m_Host = dt.Rows[0]["ServerIP"].ToString();
                                        int m_Port = Convert.ToInt32(dt.Rows[0]["ServerPort"]);
                                        string m_LoginName = dt.Rows[0]["LoginName"].ToString();
                                        string m_Password = dt.Rows[0]["Password"].ToString();
                                        string m_sFtpFilePathString = Call_ParamUtil.ReplacePath(m_sRecordFileString);
                                        string m_sListenTestFtpFilePathString = $@"ftp://{m_LoginName}:{m_Password}@{m_Host}:{m_Port}/{m_sFtpFilePathString}";
                                        MediaPlayerFrm m_pMediaPlayer = new MediaPlayerFrm(m_sListenTestFtpFilePathString);
                                        m_pMediaPlayer.BeginSearchFile();
                                        m_pMediaPlayer.Show(this);
                                    }
                                }
                                else
                                {
                                    Cmn.MsgWran("录音路径为空");
                                }
                            }
                            else
                            {
                                Cmn.MsgWran("请选择单个文件进行试听");
                            }
                        }
                        break;
                    default:
                        break;
                }

            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][Report][metroListMenu_Click][Exception][{ex.Message}]");
            }
        }

        #region 重写,这里可以不要,但是影响设计器显示
        protected override void btnSearch_Click(object sender, EventArgs e) {
            base.btnSearch_Click(sender, e);
        }
        protected override void btnReset_Click(object sender, EventArgs e) {
            this.defaultArgs(sender, e);
            if (this.searchEntity != null)
                base.btnReset_Click(sender, e);
        }
        #endregion

        #region 单字段排序
        private void list_ColumnClick(object sender, ColumnClickEventArgs e) {
            if(e.Column == 0)
                return;
            ColumnHeader columnHeader = this.list.Columns[e.Column];

            if (columnHeader.Name == "realname")
                return;

            this.ucPager.pager.field = columnHeader.Name;
            this.list.BeginUpdate();
            if(columnHeader.Tag == null) {
                this.ucPager.pager.type = "asc";
                columnHeader.Tag = "asc";
                columnHeader.ImageIndex = 1;
            } else if(columnHeader.Tag.ToString() == "asc") {
                this.ucPager.pager.type = "desc";
                columnHeader.Tag = "desc";
                columnHeader.ImageIndex = 2;
            } else if(columnHeader.Tag.ToString() == "desc") {
                this.ucPager.pager.type = "asc";
                columnHeader.Tag = "asc";
                columnHeader.ImageIndex = 1;
            }
            foreach(ColumnHeader item in this.list.Columns) {
                if(
                    item.DisplayIndex != 0
                    &&
                    item.Name != this.ucPager.pager.field
                    &&
                    item.Name != "ID"
                    ) {
                    item.ImageIndex = 0;
                    Tag = null;
                }
            }
            this.list.EndUpdate();
            this.GetListBody(this, null);
        }
        #endregion

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
            {
                MessageBox.Show(this, "有查询任务正在执行,请稍后");
                return;
            }

            try
            {
                this.m_bDoing = true;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel文件(*.xls)|*.xls";
                sfd.FileName = "通话记录-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                sfd.AddExtension = true;
                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    this.m_bDoing = false;
                    return;
                }
                new System.Threading.Thread(() =>
                {
                    try
                    {
                        this.qop.isGetTotal = true;
                        DataSet ds = this.qop.QdataSet();
                        DataSet m_pDataSet = new DataSet();
                        DataTable m_pDataTable = ds.Tables[0].Copy();
                        m_pDataSet.Tables.Add(m_pDataTable);
                        m_pDataTable.Columns["CallTypeName"].ColumnName = "通话类型";
                        m_pDataTable.Columns["AgentName"].ColumnName = "业务员";
                        m_pDataTable.Columns["LocalNum"].ColumnName = "坐席电话";
                        m_pDataTable.Columns["T_PhoneNum"].ColumnName = "电话";

                        if (this.m_bName)
                        {
                            m_pDataTable.Columns["realname"].ColumnName = "联系人姓名";
                        }

                        m_pDataTable.Columns["PhoneAddress"].ColumnName = "归属地";
                        m_pDataTable.Columns["C_StartTime"].ColumnName = "拨打时间";
                        m_pDataTable.Columns["C_SpeakTime"].ColumnName = "通话时长";
                        m_pDataTable.Columns["R_Description"].ColumnName = "通话结果";
                        m_pDataTable.Columns["RecordFile"].ColumnName = "录音";
                        m_pDataTable.Columns["Remark"].ColumnName = "备注";
                        m_pDataTable.Columns.Remove("CallResultID");
                        m_pDataTable.Columns.Remove("ID");
                        m_pDataTable.Columns.Remove("C_PhoneNum");
                        m_pDataTable.Columns.Remove("m_sFreeSWITCHIPv4");
                        m_pDataTable.Columns.Remove("m_uAgentID");
                        m_cExcel.m_fExport(m_pDataSet, sfd.FileName);
                        this.m_bDoing = false;
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][Report][btnExport_Click][Thread][Exception][{ex.Message}]");
                        this.m_bDoing = false;
                    }

                }).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Report][btnExport_Click][Exception][{ex.Message}]");
                this.m_bDoing = false;
            }
        }
    }
}

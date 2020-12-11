using Common;
using Core_v1;
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
using Cmn_v1;
using WebSocket_v1;
using CenoSocket;
using Model_v1;

namespace CenoCC {
    public partial class user : _index {
        private bool _resetpwd_ = false;
        private bool _setsipch_ = false;
        private bool _setautoch_ = false;
        private bool _notRegister_ = false;
        private bool _register_ = false;
        private bool _webRegister_ = false;
        private bool _export_ = false;
        public userBaseInfo m_pUserBaseInfo = null;
        /// <![CDATA[
        /// 权限问题：这里还是要以全部为准,嵌套写法有点麻烦
        /// ]]>
        private QueryPager qop;
        /// <summary>
        /// 统计表构造函数
        /// </summary>
        public user(bool uc = false) {
            InitializeComponent();
            if(uc) {
                this.FormBorderStyle = FormBorderStyle.None;
            }
            this.SearchEvent += new EventHandler(GetListBody);
            this.ucPager.PageSkipEvent += new EventHandler(GetListBody);
            this.LoadListHeader();
            this.defaultArgs(this, null);
            this.HandleCreated += new EventHandler((o, e) => {
                this.GetListBody(this, null);
            });

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
                this.searchEntity = new userSearch(this);
                this.searchEntity.SearchEvent += new EventHandler(GetListBody);
                this.searchEntity.defaultArgsEvent += new EventHandler(defaultArgs);
                this.searchEntity.SetQueryMark();
                this.searchEntity.StartPosition = FormStartPosition.CenterScreen;
                this.searchEntity.Show(this);
            }
        }
        private void defaultArgs(object sender, EventArgs e) {
            this.args = new Dictionary<string, object>();
        }

        /// <summary>
        /// 加载列表表头
        /// </summary>
        private void LoadListHeader() {
            this.list.BeginUpdate();
            this.list.Columns.Add(new ColumnHeader() { Text = "序号", Width = 50 });
            this.list.Columns.Add(new ColumnHeader() { Name = "d.teamname", Text = "部门", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "b.rolename", Text = "角色", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.agentname", Text = "姓名", Width = 100, ImageIndex = 1, Tag = "asc" });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.loginname", Text = "登录名", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "`T0`.`numberstate`", Text = "线路状态", Width = 160, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "c.chtype", Text = "通道类型", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "c.domainname", Text = "分机SIP注册域", Width = 120, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "c.sipserverip", Text = "分机SIP注册地址", Width = 145, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "c.showname", Text = "分机显示名", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "c.chnum", Text = "分机号", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "c.chpassword", Text = "分机密码", Width = 110, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "c.sipport", Text = "SIP端口", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "c.regtime", Text = "SIP注册时间", Width = 110, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "c.isregister", Text = "PC注册方式", Width = 100, ImageIndex = 0 });
            this.list.EndUpdate();
            this.ucPager.pager.field = "a.agentname";
            this.ucPager.pager.type = "asc";
        }
        /// <summary>
        /// 加载列表内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetListBody(object sender, EventArgs e) {
            new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                try {
                    this.qop = new QueryPager();
                    this.qop.FieldsSqlPart = @"select 
	a.id,
    a.teamid,
    d.teamname,
    a.roleid,
	b.rolename,
	a.agentname,
	a.loginname,
    ifnull(`T0`.`numberstate`,'【启:0】【禁:0】【共:0】') as numberstate,
    c.id as chid,
	case when c.chtype = 16 then 'SIP通道'
			 when c.chtype = 256 then '自动外呼通道'
			 when c.chtype is null then '未设置'
			 else '未知通道' end as chtypename,
    c.domainname,    
    c.sipserverip,
    c.showname,
	c.chnum,
    c.chpassword,
    c.sipport,
    c.regtime,
	c.isregister ";
                    this.qop.FromSqlPart = @"from call_agent a
left join call_role b
on b.id = a.roleid
left join call_channel c
on c.id = a.channelid
left join call_team d
on a.TeamID = d.ID
LEFT JOIN (
SELECT
	concat(
		'【启:',
		( ifnull( sum( CASE WHEN isuse = 1 and isshare = 0 THEN 1 ELSE 0 END ), 0 ) ),
		'】【禁:',
		( ifnull( sum( CASE WHEN isuse = 0 and isshare = 0 THEN 1 ELSE 0 END ), 0 ) ),
		'】【共:',
		ifnull( sum( 1 ), 0 ),
		'】' 
	) AS numberstate,
	useuser 
FROM
	dial_limit 
WHERE
	isshare = 0 
GROUP BY
useuser 
) `T0` ON `T0`.`useuser` = a.ID
";
                    this.qop.pager = this.ucPager.pager;
                    //this.qop.PrimaryKey = "rownum";
                    this.qop.setQuerySample(args);
                    this.qop.setQuery("b.id", "role");
                    this.qop.setQuery("c.chtype", "chType");
                    this.qop.setQuery("c.chnum", "chNum");
                    this.qop.setQuery("a.agentname", "agentName");
                    this.qop.setQuery("a.loginname", "loginName");
                    ///数据权限
                    PopedomArgs popedomArgs = new PopedomArgs();
                    popedomArgs.type = DataPowerType._data_user_search;
                    popedomArgs.left.Add("a.ID");
                    this.qop.appQuery($"{m_cPower.m_fPopedomSQL(popedomArgs)}");
                    ///查询
                    DataSet ds = this.qop.QdataSet();
                    int pageIndexStart = this.ucPager.PageIndexStart;
                    this.BeginInvoke(new MethodInvoker(() => {
                        this.list.BeginUpdate();
                        this.list.Items.Clear();
                        foreach(DataRow dr in ds.Tables[1].Rows) {
                            ListViewItem listViewItem = new ListViewItem($"{pageIndexStart++}");
                            listViewItem.UseItemStyleForSubItems = false;
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "teamname", Text = dr["teamname"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "rolename", Text = dr["rolename"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "agentname", Text = dr["agentname"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "loginname", Text = dr["loginname"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "numberstate", Text = dr["numberstate"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "chtypename", Text = dr["chtypename"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "domainname", Text = dr["domainname"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "sipserverip", Text = dr["sipserverip"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "showname", Text = dr["showname"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "chnum", Text = dr["chnum"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "chpassword", Text = dr["chpassword"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "sipport", Text = dr["sipport"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "regtime", Text = dr["regtime"].ToString() });
                            var m_pSubItem = new ListViewItem.ListViewSubItem();
                            m_pSubItem.Name = "isregister";
                            if (dr["isregister"].ToString() == "1")
                            {
                                m_pSubItem.Text = "PC注册";
                                m_pSubItem.ForeColor = Color.Green;
                            }
                            else if (dr["isregister"].ToString() == "-1")
                            {
                                m_pSubItem.Text = "Web调用";
                                m_pSubItem.ForeColor = Color.Red;
                            }
                            else
                            {
                                m_pSubItem.Text = "PC非注册";
                                m_pSubItem.ForeColor = Color.Red;
                            }
                            listViewItem.SubItems.Add(m_pSubItem);
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "id", Text = dr["id"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "chid", Text = dr["chid"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "teamid", Text = dr["teamid"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "roleid", Text = dr["roleid"].ToString() });
                            this.list.Items.Add(listViewItem);
                        }
                        this.list.EndUpdate();
                        this.ucPager.Set(Convert.ToInt32(ds.Tables[0].Rows[0]["total"]));
                    }));
                } catch(Exception ex) {
                    Log.Instance.Error($"diallimit GetListBody:{ex.Message}");
                }
            })).Start();
        }

        #region 重写,这里可以不要,但是影响设计器显示
        protected override void btnSearch_Click(object sender, EventArgs e) {
            base.btnSearch_Click(sender, e);
        }
        protected override void btnReset_Click(object sender, EventArgs e) {
            if (this.searchEntity != null)
                base.btnReset_Click(sender, e);
            else
                this.defaultArgs(sender, e);
        }
        #endregion

        #region 单字段排序
        private void list_ColumnClick(object sender, ColumnClickEventArgs e) {
            if(e.Column == 0)
                return;
            ColumnHeader columnHeader = this.list.Columns[e.Column];
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

        private void btnResetPwd_Click(object sender, EventArgs e)
        {
            if (this._resetpwd_)
                return;
            try
            {
                if (this.list.SelectedItems.Count > 0)
                {
                    if (!Cmn.MsgQ("确定要重置选中用户的密码为0000吗?"))
                        return;
                    this._resetpwd_ = true;
                    new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                    {
                        try
                        {
                            List<string> idlist = new List<string>();
                            foreach (ListViewItem item in this.list.SelectedItems)
                            {
                                int m_sID = Convert.ToInt32(item.SubItems["id"].Text);
                                idlist.Add(m_sID.ToString());
                                ///如果含有超级管理员ID,而操作者非超级管理员,提示即可
                                if (m_sID == 1000 && AgentInfo.AgentID != 1000.ToString())
                                {
                                    MessageBox.Show(this, "含有超级管理员项,不可重置,请重新选择!");
                                    return;
                                }
                            }
                            int i = Call_AgentUtil.m_fResetPwd(idlist);
                            if (i > 0)
                            {
                                this.btnSearch.PerformClick();
                                var msg = $"重置选中用户的密码完成";
                                MessageBox.Show(this, msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                            else
                            {
                                var msg = $"重置选中用户的密码失败";
                                MessageBox.Show(this, msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"user btnResetPwd_Click error:{ex.Message}");
                        }
                        finally
                        {
                            this._resetpwd_ = false;
                        }
                    })).Start();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"user btnResetPwd_Click all error:{ex.Message}");
                this._resetpwd_ = false;
            }
        }

        private void btnSetSIPCh_Click(object sender, EventArgs e)
        {
            if (_setsipch_)
                return;
            try
            {
                if (this.list.SelectedItems.Count > 0)
                {
                    if (!Cmn.MsgQ("确定将选中设置为SIP通道吗?"))
                        return;
                    this._setsipch_ = true;
                    new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                        try
                        {
                            List<string> idlist = new List<string>();
                            foreach (ListViewItem item in this.list.SelectedItems)
                            {
                                idlist.Add(item.SubItems["chid"].Text);
                            }
                            int i = Call_ChannelUtil.m_fSetChannelType(idlist, 16);
                            if (i > 0)
                            {
                                this.btnSearch.PerformClick();
                                var msg = $"将选中设置为SIP通道成功";
                                MessageBox.Show(msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                            else
                            {
                                var msg = $"将选中设置为SIP通道失败";
                                MessageBox.Show(msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                            //激活
                            InWebSocketMain.Send(M_Send._zdwh());
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"user btnSetSIPCh_Click error:{ex.Message}");
                        }
                        finally
                        {
                            this._setsipch_ = false;
                        }
                    })).Start();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"user btnSetSIPCh_Click all error:{ex.Message}");
                this._setsipch_ = false;
            }
        }

        private void btnSetAutoCh_Click(object sender, EventArgs e)
        {
            if (_setautoch_)
                return;
            try
            {
                if (this.list.SelectedItems.Count > 0)
                {
                    if (!Cmn.MsgQ("确定将选中设置为自动外呼通道吗?"))
                        return;
                    this._setautoch_ = true;
                    new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                        try
                        {
                            List<string> idlist = new List<string>();
                            foreach (ListViewItem item in this.list.SelectedItems)
                            {
                                idlist.Add(item.SubItems["chid"].Text);
                            }
                            int i = Call_ChannelUtil.m_fSetChannelType(idlist, 256);
                            if (i > 0)
                            {
                                this.btnSearch.PerformClick();
                                var msg = $"将选中设置为自动外呼通道成功";
                                MessageBox.Show(msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                            else
                            {
                                var msg = $"将选中设置为自动通道失败";
                                MessageBox.Show(msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                            //激活
                            InWebSocketMain.Send(M_Send._zdwh());
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"user btnSetAutoCh_Click error:{ex.Message}");
                        }
                        finally
                        {
                            this._setautoch_ = false;
                        }
                    })).Start();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"user btnSetAutoCh_Click all error:{ex.Message}");
                this._setautoch_ = false;
            }
        }

        private void btnNotRegister_Click(object sender, EventArgs e)
        {
            if (_notRegister_)
                return;
            try
            {
                if (this.list.SelectedItems.Count > 0)
                {
                    if (!Cmn.MsgQ("确定将选中设置为PC非注册吗?(仅对SIP通道有效)"))
                        return;
                    this._notRegister_ = true;
                    new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                        try
                        {
                            List<string> idlist = new List<string>();
                            foreach (ListViewItem item in this.list.SelectedItems)
                            {
                                idlist.Add(item.SubItems["chid"].Text);
                            }
                            int i = Call_ChannelUtil.m_fSetRegister(idlist, 0);
                            if (i > 0)
                            {
                                this.btnSearch.PerformClick();
                                var msg = $"将选中设置为PC非注册成功";
                                MessageBox.Show(msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                            else
                            {
                                var msg = $"将选中设置为PC非注册失败";
                                MessageBox.Show(msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                            //通知通道PC注册变化
                            InWebSocketMain.Send(M_Send._zdwh("PCR"));
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"user btnNotRegister_Click error:{ex.Message}");
                        }
                        finally
                        {
                            this._notRegister_ = false;
                        }
                    })).Start();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"user btnNotRegister_Click all error:{ex.Message}");
                this._notRegister_ = false;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (_register_)
                return;
            try
            {
                if (this.list.SelectedItems.Count > 0)
                {
                    if (!Cmn.MsgQ("确定将选中设置为PC注册吗?(仅对SIP通道有效)"))
                        return;
                    this._register_ = true;
                    new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                        try
                        {
                            List<string> idlist = new List<string>();
                            foreach (ListViewItem item in this.list.SelectedItems)
                            {
                                idlist.Add(item.SubItems["chid"].Text);
                            }
                            int i = Call_ChannelUtil.m_fSetRegister(idlist, 1);
                            if (i > 0)
                            {
                                this.btnSearch.PerformClick();
                                var msg = $"将选中设置为PC注册成功";
                                MessageBox.Show(msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                            else
                            {
                                var msg = $"将选中设置为PC注册失败";
                                MessageBox.Show(msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                            //通知通道PC注册变化
                            InWebSocketMain.Send(M_Send._zdwh("PCR"));
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"user btnRegister_Click error:{ex.Message}");
                        }
                        finally
                        {
                            this._register_ = false;
                        }
                    })).Start();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"user btnRegister_Click all error:{ex.Message}");
                this._register_ = false;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_export_)
                return;
            try
            {
                _export_ = true;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel文件(*.xls)|*.xls";
                sfd.FileName = "坐席及号码-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                sfd.AddExtension = true;
                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    _export_ = false;
                    return;
                }
                new System.Threading.Thread(() =>
                {
                    try
                    {
                        string m_sAsSQL = $@"
SELECT
	CONCAT( '\'', `call_agent`.`LoginName` ) AS `登录名`,
	CONCAT( '\'', `call_agent`.`AgentName` ) AS `真实姓名`,
	CONCAT( '\'', `dial_limit`.`number` ) AS `号码`,
	CONCAT( '\'', `dial_limit`.`tnumber` ) AS `真实号码`,
	CONCAT( '\'', `call_channel`.`ChNum` ) AS `分机号`,
	CONCAT( '\'', `call_channel`.`SipServerIp` ) AS `分机SIP注册地址`,
	CONCAT( '\'', `call_channel`.`ChPassword` ) AS `分机密码` 
FROM
	`call_agent`
	LEFT JOIN `dial_limit` ON `call_agent`.`ID` = `dial_limit`.`useuser`
	LEFT JOIN `call_channel` ON `call_channel`.`ID` = `call_agent`.`ChannelID` 
ORDER BY
	`call_agent`.`LoginName`
";
                        DataSet m_pDataSet = DataBaseUtil.MySQL_Method.ExecuteDataSet(m_sAsSQL);
                        m_cExcel.m_fExport(m_pDataSet, sfd.FileName);
                        _export_ = false;
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][Report][btnExport_Click][Thread][Exception][{ex.Message}]");
                        _export_ = false;
                    }

                }).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Report][btnExport_Click][Exception][{ex.Message}]");
                _export_ = false;
            }
        }

        private void btnWeb_Click(object sender, EventArgs e)
        {
            if (this._webRegister_)
                return;
            try
            {
                if (this.list.SelectedItems.Count > 0)
                {
                    if (!Cmn.MsgQ("确定将选中设置为Web调用吗?(仅对SIP通道有效)"))
                        return;
                    this._webRegister_ = true;
                    new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                        try
                        {
                            List<string> idlist = new List<string>();
                            foreach (ListViewItem item in this.list.SelectedItems)
                            {
                                idlist.Add(item.SubItems["chid"].Text);
                            }
                            int i = Call_ChannelUtil.m_fSetRegister(idlist, -1);
                            if (i > 0)
                            {
                                this.btnSearch.PerformClick();
                                var msg = $"将选中设置为Web调用成功";
                                MessageBox.Show(msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                            else
                            {
                                var msg = $"将选中设置为Web调用失败";
                                MessageBox.Show(msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                            //通知通道PC注册变化
                            InWebSocketMain.Send(M_Send._zdwh("PCR"));
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"user btnNotRegister_Click error:{ex.Message}");
                        }
                        finally
                        {
                            this._webRegister_ = false;
                        }
                    })).Start();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"user btnWeb_Click all error:{ex.Message}");
                this._webRegister_ = false;
            }
        }

        private void btnSIPEdit_Click(object sender, EventArgs e)
        {
            userEdit entity = new userEdit();
            entity._entity = this;
            entity.SearchEvent += new EventHandler(this.GetListBody);
            entity.Show(this);
        }

        private void btnBaseInfo_Click(object sender, EventArgs e)
        {
            int m_uID = -1;
            if (this.list?.SelectedItems?.Count == 1)
            {
                m_uID = Convert.ToInt32(this.list?.SelectedItems[0]?.SubItems["id"].Text);
            }

            if (m_pUserBaseInfo != null && !m_pUserBaseInfo.IsDisposed)
            {
                m_pUserBaseInfo.TopMost = true;
                m_pUserBaseInfo.WindowState = FormWindowState.Normal;
                m_pUserBaseInfo.StartPosition = FormStartPosition.CenterScreen;
                return;
            }

            m_pUserBaseInfo = new userBaseInfo(m_uID);
            m_pUserBaseInfo._entity = this;
            m_pUserBaseInfo.SearchEvent = new EventHandler(this.GetListBody);
            m_pUserBaseInfo.Show(this);
        }

        private void list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_pUserBaseInfo != null && !m_pUserBaseInfo.IsDisposed && this.list?.SelectedItems?.Count == 1)
            {
                var m_pSelectItem = this.list?.SelectedItems[0];
                this.m_pUserBaseInfo.m_fSetSelectInfo(m_pSelectItem.SubItems["agentname"].Text, m_pSelectItem.SubItems["loginname"].Text, m_pSelectItem.SubItems["teamid"].Text, m_pSelectItem.SubItems["roleid"].Text, Convert.ToInt32(m_pSelectItem.SubItems["id"].Text));
            }
        }

        private void btnUpdUa_Click(object sender, EventArgs e)
        {
            ///重载用户信息,无需重启服务端
            InWebSocketMain.Send(M_Send._zdwh("UpdUa"));
            MessageBox.Show(this, "发送用户信息重载命令完成");
        }
    }
}

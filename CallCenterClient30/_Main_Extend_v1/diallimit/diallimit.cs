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
using Model_v1;

namespace CenoCC {
    public partial class diallimit : _index {

        private bool m_bUaListLoading = false;

        /// <![CDATA[
        /// 权限问题：这里还是要以全部为准,嵌套写法有点麻烦
        /// ]]>
        private QueryPager qop;
        public update _update;
        private bool _import_ = false;
        private bool _delete_ = false;
        private bool _use_ = false;
        private bool _unuse_ = false;
        private bool _quick_ = false;
        private bool _gateway_ = false;
        /// <summary>
        /// 统计表构造函数
        /// </summary>
        public diallimit(bool uc = false) {
            InitializeComponent();
            if(uc) {
                this.FormBorderStyle = FormBorderStyle.None;
            }
            this.m_bResetArgs = false;
            this.SearchEvent += new EventHandler(GetListBody);
            this.ucPager.PageSkipEvent += new EventHandler(GetListBody);
            this.LoadListHeader();
            this.defaultArgs(this, null);
            this.HandleCreated += new EventHandler((o, e) => {
                this.GetListBody(this, null);
                this.m_fFlushGateway(null, null);
                this.m_fFlushUa();
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

        private void m_fFlushUa(int m_uUa = -1)
        {
            if (this.m_bUaListLoading) return;

            this.m_bUaListLoading = true;

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {

                    PopedomArgs popedomArgs = new PopedomArgs();
                    popedomArgs.type = DataPowerType._data_diallimit_limit_allot;
                    popedomArgs.left.Add("a.ID");
                    string m_sPopedomSQL = m_cPower.m_fPopedomSQL(popedomArgs);

                    var dt = Call_AgentUtil.m_fGetAgentList(m_sPopedomSQL, m_uUa);
                    var dr = dt.NewRow();
                    dr["EmpID"] = -1;
                    dr["lr"] = "取消分配";
                    dt.Rows.InsertAt(dr, 0);

                    ///<![CDATA[
                    /// 增加一个批量分配的逻辑
                    /// ]]>

                    var drp = dt.NewRow();
                    drp["EmpID"] = -2;
                    drp["lr"] = "批量分配";
                    dt.Rows.InsertAt(drp, 1);

                    ///方便分配线路
                    {
                        ///<![CDATA[
                        /// 切换为无启用线路
                        /// ]]>

                        var druse = dt.NewRow();
                        druse["EmpID"] = -12;
                        druse["lr"] = "切换无启用线路坐席";
                        dt.Rows.InsertAt(druse, 2);

                        ///<![CDATA[
                        /// 切换为分配线路大于0
                        /// ]]>

                        var drset = dt.NewRow();
                        drset["EmpID"] = -11;
                        drset["lr"] = "切换无分配线路坐席";
                        dt.Rows.InsertAt(drset, 3);

                        ///<![CDATA[
                        /// 切换为默认模式
                        /// ]]>

                        var drdef = dt.NewRow();
                        drdef["EmpID"] = -10;
                        drdef["lr"] = "切换全部坐席";
                        dt.Rows.InsertAt(drdef, 4);
                    }

                    if (!this.IsDisposed)
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            this.userListValue.BeginUpdate();
                            this.userListValue.DataSource = dt;
                            this.userListValue.DisplayMember = "lr";
                            this.userListValue.ValueMember = "EmpID";
                            this.userListValue.EndUpdate();
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"diallimit fill agent:{ex.Message}");
                }
                finally
                {
                    this.m_bUaListLoading = false;
                }

            })).Start();
        }

        /// <summary>
        /// 打开查询条件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchOpen_Click(object sender, EventArgs e) {
            if(this.searchEntity == null) {
                this.searchEntity = new diallimitSearch(this);
                this.searchEntity.SearchEvent += new EventHandler(GetListBody);
                this.searchEntity.defaultArgsEvent += new EventHandler(defaultArgs);
                this.searchEntity.SetQueryMark();
                this.searchEntity.StartPosition = FormStartPosition.CenterScreen;
                this.searchEntity.Show(this);
            }
        }
        private void defaultArgs(object sender, EventArgs e) {
            this.args = new Dictionary<string, object>();
            this.args.Add("isshare", "0");
        }

        /// <summary>
        /// 加载列表表头
        /// </summary>
        private void LoadListHeader() {
            this.list.BeginUpdate();
            this.list.Columns.Add(new ColumnHeader() { Text = "序号", Width = 50 });
            this.list.Columns.Add(new ColumnHeader() { Name = "b.loginname", Text = "登录名", Width = 160, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "b.agentname", Text = "真实姓名", Width = 90, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.number", Text = "号码", Width = 100, ImageIndex = 1, Tag = "asc" });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.tnumber", Text = "真实号码", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "c.gw_name", Text = "网关", Width = 245, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.areacode", Text = "区号", Width = 70, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.areaname", Text = "归属地", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.dialprefix", Text = "外地加拨", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.diallocalprefix", Text = "本地加拨", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.prefixdealflag", Text = "自动根据区号加拨前缀", Width = 160, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.ordernum", Text = "排序", Width = 70, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.dtmf", Text = "dtmf(按键)", Width = 170, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.LimitCallRule", Text = "呼入规则", Width = 125, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.isuse", Text = "状态", Width = 90, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.isusedial", Text = "禁止呼出", Width = 90, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.isusecall", Text = "禁止呼入", Width = 90, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.limitthedial", Text = "同号码限呼", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.limitcount", Text = "总限制次数", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.usecount", Text = "总使用次数", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.limitduration", Text = "总限制时长", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.useduration", Text = "总使用时长", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.usethetime", Text = "当日", Width = 128, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.limitthecount", Text = "当日限制次数", Width = 115, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.usethecount", Text = "当日使用次数", Width = 115, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.limittheduration", Text = "当日限制时长", Width = 115, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.usetheduration", Text = "当日使用时长", Width = 115, ImageIndex = 0 });
            this.list.EndUpdate();
            this.ucPager.pager.field = "a.number";
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
	a.number,
	a.tnumber,
    a.LimitCallRule,
    a.ordernum,
	a.useuser,
	a.usecount,
    a.dtmf,
	a.useduration,
    a.limitthedial,
    a.limitcount,
    a.limitduration,
    date_format(a.usethetime,'%Y-%m-%d') as usethetime,
	a.usethecount,
	a.usetheduration,
    a.limitthecount,
    a.limittheduration,
	a.adduser,
	a.addtime,
	a.isdel,
	a.isuse,
    case a.isshare when 0 then b.loginname
                   when -2 then concat(b.loginname,'(',b.agentname,')')
                   when 1 then '共享号码'
                   when 2 then '申请式'
                   else        '未知'
    end as loginname,
    case a.isshare when 0 then b.agentname
                   when -2 then '呼叫内转号码'
                   when 1 then '共享号码'
                   when 2 then '申请式'
                   else        '未知'
    end as realname,
	-- b.loginname,
	-- b.agentname as realname,
    a.dialprefix,
    a.diallocalprefix,
    a.prefixdealflag,
    ifnull(d.AutoAddNumDialFlag,-1) as zflag,
    a.areacode,
    a.areaname,
    case when c.remark is null then c.gw_name
         when c.remark =  ''   then c.gw_name
         else concat(c.remark,' ',c.gw_name) end as gw,
    -- c.gw_name as gw,
    a.isusedial,
    a.isusecall,
    a.isshare";
                    this.qop.FromSqlPart = @"from dial_limit as a
left join call_agent as b
on a.useuser = b.id
left join call_clientparam as d
on b.clientparamid = d.id
left join call_gateway as c
on c.uniqueid = a.gwuid";
                    this.qop.pager = this.ucPager.pager;
                    //this.qop.PrimaryKey = "rownum";
                    this.qop.setQuerySample(args);
                    this.qop.setQuery("a.useuser", "agent");
                    this.qop.setQuery("a.number", "number");
                    this.qop.setQuery("c.id", "gateway");
                    this.qop.setQuery("a.areacode", "areacode");
                    this.qop.setQuery("a.areaname", "areaname");
                    this.qop.setQuery("a.dialprefix", "dialprefix");
                    this.qop.setQuery("a.diallocalprefix", "diallocalprefix");
                    if (args != null && args.ContainsKey("dtmf"))
                        this.qop.appQuery($" AND IFNULL( IF ( a.dtmf = '', NULL, a.dtmf ), '{Call_ParamUtil.m_sDTMFSendMethod}' ) = '{args["dtmf"]}' ");
                    this.qop.setQuery("a.isuse", "isuse");
                    this.qop.setQuery("a.isusedial", "isusedial");
                    this.qop.setQuery("a.isusecall", "isusecall");
                    this.qop.setQuery("a.isshare", "isshare");
                    ///数据权限
                    PopedomArgs popedomArgs = new PopedomArgs();
                    popedomArgs.type = DataPowerType._data_diallimit_limit_search;
                    popedomArgs.left.Add("a.useuser");
                    popedomArgs.left.Add("a.adduser");
                    popedomArgs.TSQL = "OR a.adduser = -1";
                    ///将将号码分配给了自己,自己是号码的添加人
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
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "loginname", Text = dr["loginname"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "realname", Text = dr["realname"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "number", Text = dr["number"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "tnumber", Text = dr["tnumber"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "gw", Text = dr["gw"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "areacode", Text = dr["areacode"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "areaname", Text = dr["areaname"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "dialprefix", Text = dr["dialprefix"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "diallocalprefix", Text = dr["diallocalprefix"].ToString() });
                            //prefixdealflag
                            int prefixdealflag = Convert.ToInt32(dr["prefixdealflag"]);
                            ListViewItem.ListViewSubItem _prefixdealflag = new ListViewItem.ListViewSubItem();
                            _prefixdealflag.Name = "prefixdealflag";
                            switch (prefixdealflag)
                            {
                                case -1:
                                    {
                                        int zflag = Convert.ToInt32(dr["zflag"]);
                                        _prefixdealflag.Text = (zflag == 1 ? "默认(启用)" : (zflag == 0 ? "默认(禁用)" : "-"));
                                    }
                                    break;
                                case 0:
                                    {
                                        _prefixdealflag.Text = $"禁用";
                                        _prefixdealflag.ForeColor = Color.Red;
                                    }
                                    break;
                                case 1:
                                    {
                                        _prefixdealflag.Text = $"启用";
                                        _prefixdealflag.ForeColor = Color.Green;
                                    }
                                    break;
                                default:
                                    {
                                        _prefixdealflag.Text = $"未知";
                                    }
                                    break;
                            }
                            listViewItem.SubItems.Add(_prefixdealflag);
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "ordernum", Text = dr["ordernum"].ToString() });
                            //dtmf
                            if (dr["dtmf"].ToString() == Call_ParamUtil.inbound)
                            {
                                ListViewItem.ListViewSubItem dtmf = new ListViewItem.ListViewSubItem();
                                dtmf.Name = "dtmf";
                                dtmf.Text = $"{Call_ParamUtil.inbound}(带内)";
                                listViewItem.SubItems.Add(dtmf);
                            }
                            else if (dr["dtmf"].ToString() == Call_ParamUtil.clientSignal)
                            {
                                ListViewItem.ListViewSubItem dtmf = new ListViewItem.ListViewSubItem();
                                dtmf.Name = "dtmf";
                                dtmf.Text = $"{Call_ParamUtil.clientSignal}(客户端Signal)";
                                listViewItem.SubItems.Add(dtmf);
                            }
                            else if (dr["dtmf"].ToString() == Call_ParamUtil.bothSignal)
                            {
                                ListViewItem.ListViewSubItem dtmf = new ListViewItem.ListViewSubItem();
                                dtmf.Name = "dtmf";
                                dtmf.Text = $"{Call_ParamUtil.bothSignal}(服务端Signal)";
                                listViewItem.SubItems.Add(dtmf);
                            }
                            else
                            {
                                ListViewItem.ListViewSubItem dtmf = new ListViewItem.ListViewSubItem();
                                dtmf.Name = "dtmf";
                                dtmf.Text = $"默认({Call_ParamUtil.m_sDTMFSendMethod})";
                                listViewItem.SubItems.Add(dtmf);
                            }
                            //呼入规则
                            int m_uLimitCallRule = Convert.ToInt32(dr["LimitCallRule"]);
                            ListViewItem.ListViewSubItem LimitCallRule = new ListViewItem.ListViewSubItem();
                            switch (m_uLimitCallRule)
                            {
                                case 1:
                                    LimitCallRule.ForeColor = Color.Black;
                                    LimitCallRule.Name = "LimitCallRule";
                                    LimitCallRule.Text = "查拨号限制";
                                    break;
                                case 2:
                                    LimitCallRule.ForeColor = Color.Black;
                                    LimitCallRule.Name = "LimitCallRule";
                                    LimitCallRule.Text = "查通话记录";
                                    break;
                                case 3:
                                    LimitCallRule.ForeColor = Color.Green;
                                    LimitCallRule.Name = "LimitCallRule";
                                    LimitCallRule.Text = "智能";
                                    break;
                                default:
                                    LimitCallRule.ForeColor = Color.Red;
                                    LimitCallRule.Name = "LimitCallRule";
                                    LimitCallRule.Text = "参数错误";
                                    break;
                            }
                            listViewItem.SubItems.Add(LimitCallRule);
                            //启用禁用
                            if (Convert.ToInt32(dr["isuse"]) == 1) {
                                ListViewItem.ListViewSubItem isuse = new ListViewItem.ListViewSubItem();
                                isuse.ForeColor = Color.Green;
                                isuse.Name = "isuse";
                                isuse.Text = "启用";
                                listViewItem.SubItems.Add(isuse);
                            } else {
                                ListViewItem.ListViewSubItem isuse = new ListViewItem.ListViewSubItem();
                                isuse.ForeColor = Color.Red;
                                isuse.Name = "isuse";
                                isuse.Text = "禁用";
                                listViewItem.SubItems.Add(isuse);
                            }
                            if(Convert.ToInt32(dr["isusedial"]) == 1) {
                                ListViewItem.ListViewSubItem isuse = new ListViewItem.ListViewSubItem();
                                isuse.ForeColor = Color.Green;
                                isuse.Name = "isusedial";
                                isuse.Text = "否";
                                listViewItem.SubItems.Add(isuse);
                            } else {
                                ListViewItem.ListViewSubItem isuse = new ListViewItem.ListViewSubItem();
                                isuse.ForeColor = Color.Red;
                                isuse.Name = "isusedial";
                                isuse.Text = "禁止";
                                listViewItem.SubItems.Add(isuse);
                            }
                            if (Convert.ToInt32(dr["isusecall"]) == 1) {
                                ListViewItem.ListViewSubItem isuse = new ListViewItem.ListViewSubItem();
                                isuse.ForeColor = Color.Green;
                                isuse.Name = "isusecall";
                                isuse.Text = "否";
                                listViewItem.SubItems.Add(isuse);
                            } else {
                                ListViewItem.ListViewSubItem isuse = new ListViewItem.ListViewSubItem();
                                isuse.ForeColor = Color.Red;
                                isuse.Name = "isusecall";
                                isuse.Text = "禁止";
                                listViewItem.SubItems.Add(isuse);
                            }
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "limitthedial", Text = Convert.ToInt32(dr["limitthedial"]) == 0 ? "不限制" : dr["limitthedial"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "limitcount", Text = Convert.ToInt32(dr["limitcount"]) == 0 ? "不限制" : dr["limitcount"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "usecount", Text = dr["usecount"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "limitduration", Text = Convert.ToInt32(dr["limitduration"]) == 0 ? "不限制" : dr["limitduration"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "useduration", Text = dr["useduration"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "usethetime", Text = dr["usethetime"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "limitthecount", Text = Convert.ToInt32(dr["limitthecount"]) == 0 ? "不限制" : dr["limitthecount"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "usethecount", Text = dr["usethecount"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "limittheduration", Text = Convert.ToInt32(dr["limittheduration"]) == 0 ? "不限制" : dr["limittheduration"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "usetheduration", Text = dr["usetheduration"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "id", Text = dr["id"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "isshare", Text = dr["isshare"].ToString() });
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

        /// <summary>
        /// 电话限制号码表导入
        /// <![CDATA[
        /// 由于语句不是很好修改
        /// 这里的导入功能暂时先禁用
        /// ]]>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void diallimitImport_Click(object sender, EventArgs e) {
            if(this._import_)
                return;
            try {
                this._import_ = true;
                if(openFileDialog.ShowDialog() == DialogResult.OK) {
                    var filename = openFileDialog.FileName;
                    var content = h_txt.read(filename);
                    m_deal _m_deal = h_deal.ifmt(content);
                    if(_m_deal.haserr) {
                        MessageBox.Show("电话批量导入的txt文件有错误", "错误");
                        if(folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                            h_txt.write(_m_deal, folderBrowserDialog.SelectedPath);
                        }
                    } else {
                        if(this.userListValue.SelectedValue.ToString() == "-1") {
                            var _repeat_ds = d_multi.repeat(_m_deal);
                            if(_repeat_ds != null && _repeat_ds.Tables.Count == 3) {
                                if(_repeat_ds.Tables[1].Rows.Count > 0) {
                                    if(DialogResult.Yes == MessageBox.Show("电话批量导入的txt文件有重复,是否系统自动去重后导入?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
                                        if(d_multi.iu(_repeat_ds.Tables[0], AgentInfo.AgentID, "-1") > 0) {
                                            this.btnSearch.PerformClick();
                                            MessageBox.Show($"电话批量导入完成");
                                        } else
                                            MessageBox.Show($"电话批量导入0条");
                                    } else {
                                        if(folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                                            h_txt.write(_repeat_ds.Tables[2], folderBrowserDialog.SelectedPath);
                                        }
                                    }
                                } else {
                                    if(d_multi.iu(_repeat_ds.Tables[0], AgentInfo.AgentID, "-1") > 0) {
                                        this.btnSearch.PerformClick();
                                        MessageBox.Show($"电话批量导入完成");
                                    } else
                                        MessageBox.Show($"电话批量导入0条");
                                }
                            } else {
                                MessageBox.Show("电话批量导入的txt查询重复时错误");
                            }
                        } else {
                            var _check_ds = d_multi.check(_m_deal);
                            if(_check_ds != null && _check_ds.Tables.Count == 3) {
                                if(Convert.ToInt32(_check_ds.Tables[1].Rows[0]["count"]) < _check_ds.Tables[0].Rows.Count) {
                                    MessageBox.Show($"坐席{this.userListValue.SelectedText},电话批量导入的txt文件有错误", "错误");
                                    if(folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                                        h_txt.write(_check_ds.Tables[2], folderBrowserDialog.SelectedPath);
                                    }
                                } else {
                                    if(d_multi.iu(_check_ds.Tables[0], AgentInfo.AgentID, this.userListValue.SelectedValue.ToString()) > 0) {
                                        this.btnSearch.PerformClick();
                                        MessageBox.Show($"坐席{this.userListValue.SelectedText},电话批量导入完成");
                                    } else
                                        MessageBox.Show($"坐席{this.userListValue.SelectedText},电话批量导入0条");
                                }
                            } else {
                                MessageBox.Show($"坐席{this.userListValue.SelectedText},电话批量导入错误");
                            }
                        }
                    }
                }
            } catch(Exception ex) {
                Log.Instance.Error($"diallimit diallimitImport_Click error:{ex.Message}");
                MessageBox.Show($"电话批量导入错误:{ex.Message}", "错误");
            } finally {
                this._import_ = false;
            }
        }

        private void btnDel_Click(object sender, EventArgs e) {
            if(this._delete_)
                return;
            try {
                if(this.list.SelectedItems.Count > 0) {
                    this._delete_ = true;
                    var idlist = new List<string>();
                    var numberlist = new List<string>();
                    foreach(ListViewItem item in this.list.SelectedItems) {
                        idlist.Add(item.SubItems["id"].Text);
                        numberlist.Add(item.SubItems["number"].Text);
                    }
                    var numbers = string.Join(",", numberlist.ToArray());
                    if(DialogResult.Yes != MessageBox.Show(this, $"确定要删除选中的电话吗?{numbers}", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
                        this._delete_ = false;
                        return;
                    }
                    new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                        try {
                            if(d_multi.del(idlist) > 0) {
                                this.btnSearch.PerformClick();
                                var msg = $"删除电话:{numbers},完成";
                                Log.Instance.Success(msg);
                                MessageBox.Show(this, msg);
                            } else {
                                var msg = $"删除电话:{numbers},失败";
                                Log.Instance.Success(msg);
                                MessageBox.Show(this, msg);
                            }
                        } catch(Exception ex) {
                            Log.Instance.Success($"diallimit btnDel_Click error:{ex.Message}");
                        } finally {
                            this._delete_ = false;
                        }
                    })).Start();
                }
            } catch(Exception ex) {
                Log.Instance.Success($"diallimit btnDel_Click all error:{ex.Message}");
                this._delete_ = false;
            }
        }

        private void btnConfig_Click(object sender, EventArgs e) {
            parameter _ = new parameter();
            _.SearchEvent += new EventHandler(this.GetListBody);
            _._entity = this;
            _.Show(this);
        }

        private void btnUse_Click(object sender, EventArgs e) {
            if(this._use_)
                return;
            try {
                if(this.list.SelectedItems.Count > 0) {
                    this._use_ = true;
                    var idlist = new List<string>();
                    var numberlist = new List<string>();
                    foreach(ListViewItem item in this.list.SelectedItems) {
                        idlist.Add(item.SubItems["id"].Text);
                        numberlist.Add(item.SubItems["number"].Text);
                    }
                    var numbers = string.Join(",", numberlist.ToArray());
                    if(DialogResult.Yes != MessageBox.Show(this, $"确定要启用选中的电话吗?{numbers}", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
                        this._use_ = false;
                        return;
                    }
                    new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                        try {
                            if(d_multi.use(idlist) > 0) {
                                this.btnSearch.PerformClick();
                                var msg = $"启用电话:{numbers},完成";
                                Log.Instance.Success(msg);
                                MessageBox.Show(this, msg);
                            } else {
                                var msg = $"启用电话:{numbers},失败";
                                Log.Instance.Success(msg);
                                MessageBox.Show(this, msg);
                            }
                        } catch(Exception ex) {
                            Log.Instance.Error($"diallimit btnUse_Click error:{ex.Message}");
                        } finally {
                            this._use_ = false;
                        }
                    })).Start();
                }
            } catch(Exception ex) {
                Log.Instance.Error($"diallimit btnUse_Click all error:{ex.Message}");
                this._use_ = false;
            }
        }

        private void btnUnuse_Click(object sender, EventArgs e) {
            if(this._unuse_)
                return;
            try {
                if(this.list.SelectedItems.Count > 0) {
                    this._unuse_ = true;
                    var idlist = new List<string>();
                    var numberlist = new List<string>();
                    foreach(ListViewItem item in this.list.SelectedItems) {
                        idlist.Add(item.SubItems["id"].Text);
                        numberlist.Add(item.SubItems["number"].Text);
                    }
                    var numbers = string.Join(",", numberlist.ToArray());
                    if(DialogResult.Yes != MessageBox.Show(this, $"确定要禁用选中的电话吗?{numbers}", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
                        this._unuse_ = false;
                        return;
                    }
                    new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                        try {
                            if(d_multi.unuse(idlist) > 0) {
                                this.btnSearch.PerformClick();
                                var msg = $"禁用电话:{numbers},完成";
                                Log.Instance.Success(msg);
                                MessageBox.Show(this, msg);
                            } else {
                                var msg = $"禁用电话:{numbers},失败";
                                Log.Instance.Success(msg);
                                MessageBox.Show(this, msg);
                            }
                        } catch(Exception ex) {
                            Log.Instance.Error($"diallimit btnUnuse_Click error:{ex.Message}");
                        } finally {
                            this._unuse_ = false;
                        }
                    })).Start();
                }
            } catch(Exception ex) {
                Log.Instance.Error($"diallimit btnUnuse_Click all error:{ex.Message}");
                this._unuse_ = false;
            }
        }

        private void btnQuick_Click(object sender, EventArgs e) {
            ///<![CDATA[
            /// 步骤1:批量选中
            /// 步骤2:全部是取消分配
            /// 步骤3:分配即可
            /// ]]>
            if(this._quick_)
                return;
            try {
                if(this.list.SelectedItems.Count > 0) {
                    this._quick_ = true;
                    var idlist = new List<string>();
                    var numberlist = new List<string>();
                    var useuser = Convert.ToInt32(this.userListValue.SelectedValue);
                    var useuser_name = ((System.Data.DataRowView)this.userListValue.SelectedItem).Row["lr"];
                    if(useuser == -1) {
                        useuser_name = "-(未分配)";
                    }

                    ///<![CDATA[
                    /// 追加批量分配逻辑
                    /// ]]>

                    if (useuser == -2)
                    {
                        this._quick_ = false;
                        this.m_fMultiAssign();
                        return;
                    }

                    ///<![CDATA[
                    /// 追加导入真实号码功能
                    /// 这里以绑定号码做基准机可
                    /// ]]>
                    if (useuser == -3)
                    {
                        this._quick_ = false;
                        Cmn_v1.Cmn.MsgWran("导入真实号码正在开发中...");
                        return;
                    }

                    foreach(ListViewItem item in this.list.SelectedItems) {
                        idlist.Add(item.SubItems["id"].Text);
                        var loginname = item.SubItems["loginname"].Text;
                        var realname = item.SubItems["realname"].Text;
                        numberlist.Add($"{item.SubItems["number"].Text}  {(string.IsNullOrEmpty(loginname) ? "-" : loginname)}({(string.IsNullOrEmpty(realname) ? "未分配" : realname)}) -> {useuser_name}");
                    }
                    var numbers = string.Join("\r\n", numberlist.ToArray());
                    if(DialogResult.Yes != MessageBox.Show(this, $"确定要重新分配选中的电话吗?\r\n{numbers}", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
                        this._quick_ = false;
                        return;
                    }
                    new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                        try {
                            if(d_multi.quick(idlist, useuser) > 0) {
                                this.btnSearch.PerformClick();
                                var msg = $"重新分配电话:\r\n{numbers}\r\n完成";
                                MessageBox.Show(this, msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            } else {
                                var msg = $"重新分配电话:\r\n{numbers}\r\n失败";
                                MessageBox.Show(this, msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                        } catch(Exception ex) {
                            Log.Instance.Error($"diallimit btnQuick_Click error:{ex.Message}");
                        } finally {
                            this._quick_ = false;
                        }
                    })).Start();
                }
                else {
                    Cmn_v1.Cmn.MsgWranThat(this, "请选择要分配的号码");
                }
            } catch(Exception ex) {
                Log.Instance.Error($"diallimit btnQuick_Click all error:{ex.Message}");
                this._quick_ = false;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e) {
            diallimitCreate _ = new diallimitCreate();
            _.SearchEvent = new EventHandler(this.GetListBody);
            _.Show(this);
        }

        private void btnCmnset_Click(object sender, EventArgs e)
        {
            cmnset _ = new cmnset();
            _._entity = this;
            _.SearchEvent = new EventHandler(this.GetListBody);
            _.Show(this);
        }

        private void btnGateway_Click(object sender, EventArgs e)
        {
            if (this._gateway_)
                return;
            try
            {
                if (string.IsNullOrWhiteSpace(this.cboGateway.SelectedValue.ToString()))
                {
                    if (!(this.list.SelectedItems.Count > 0))
                    {
                        gatewayCreate _ = new gatewayCreate();
                        _.m_fFlushGateway = new EventHandler(this.m_fFlushGateway);
                        _.Show(this);
                        return;
                    }
                }
                if (this.list.SelectedItems.Count > 0)
                {
                    this._gateway_ = true;
                    var idlist = new List<string>();
                    var gwuid = this.cboGateway.SelectedValue.ToString();
                    var gwname = this.cboGateway.Text;
                    var nameList = new List<string>();
                    foreach (ListViewItem item in this.list.SelectedItems)
                    {
                        idlist.Add(item.SubItems["id"].Text);
                        nameList.Add(item.SubItems["number"].Text);
                    }
                    string m_sID = string.Join(",", nameList);
                    if (DialogResult.Yes != MessageBox.Show(this, $"确定要将选中号码:{m_sID}设置为\"{gwname}\"网关吗?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        this._gateway_ = false;
                        return;
                    }
                    new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                    {
                        try
                        {
                            if (m_cGateway.m_fSetGateway(gwuid, idlist) > 0)
                            {
                                this.btnSearch.PerformClick();
                                var msg = $"将选中号码:{m_sID}设置为\"{gwname}\"网关成功";
                                MessageBox.Show(this, msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                            else
                            {
                                var msg = $"将选中号码:{m_sID}设置为\"{gwname}\"网关失败";
                                MessageBox.Show(this, msg);
                                Log.Instance.Success(msg.Replace("\r\n", ";"));
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"diallimit btnGateway_Click error:{ex.Message}");
                        }
                        finally
                        {
                            this._gateway_ = false;
                        }
                    })).Start();
                }
                else
                {
                    //可以编辑网关
                    gatewayCreate _ = new gatewayCreate(this.cboGateway.SelectedValue.ToString());
                    _.m_fFlushGateway = new EventHandler(this.m_fFlushGateway);
                    _.Show(this);
                    return;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"diallimit btnGateway_Click all error:{ex.Message}");
                this._gateway_ = false;
            }
        }

        private void m_fFlushGateway(object sender, EventArgs e)
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                try
                {

                    PopedomArgs popedomArgs = new PopedomArgs();
                    popedomArgs.type = DataPowerType._data_gateway_allot;
                    popedomArgs.left.Add("`call_gateway`.`adduser`");
                    string m_sPopedomSQL = m_cPower.m_fPopedomSQL(popedomArgs);

                    var dt = m_cGateway.m_fGatewayList(null, m_sPopedomSQL);
                    var dr = dt.NewRow();
                    dr["uid"] = "";
                    dr["rgw"] = "请选择网关";
                    dt.Rows.InsertAt(dr, 0);

                    if (!this.IsDisposed)
                    {
                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            this.cboGateway.BeginUpdate();
                            this.cboGateway.DataSource = dt;
                            this.cboGateway.DisplayMember = "rgw";
                            this.cboGateway.ValueMember = "uid";
                            this.cboGateway.EndUpdate();
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"diallimit m_fFlushGateway gateway:{ex.Message}");
                }
            })).Start();
        }

        private void btnIMS_Click(object sender, EventArgs e)
        {
            IMS _ = new IMS();
            _._entity = this;
            _.m_fFlushGateway = new EventHandler(this.m_fFlushGateway);
            _.SearchEvent = new EventHandler(this.GetListBody);
            _.Show(this);
        }

        private void btnUpdateNumber_Click(object sender, EventArgs e)
        {
            if (this.list?.SelectedItems?.Count != 1)
            {
                MessageBox.Show(this, "请选择一项进行修改");
                return;
            }

            if (_update != null && !_update.IsDisposed)
            {
                _update.TopMost = true;
                _update.WindowState = FormWindowState.Normal;
                _update.StartPosition = FormStartPosition.CenterScreen;
                return;
            }

            _update = new update();
            _update._entity = this;
            _update.SearchEvent = new EventHandler(this.GetListBody);
            _update.Show(this);

            var m_pSelectItem = this.list?.SelectedItems[0];
            _update.m_fSetNumber(m_pSelectItem.SubItems["number"].Text, m_pSelectItem.SubItems["tnumber"].Text, m_pSelectItem.SubItems["ordernum"].Text);
        }

        private void list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_update != null && !_update.IsDisposed && this.list?.SelectedItems?.Count == 1)
            {
                var m_pSelectItem = this.list?.SelectedItems[0];
                _update.m_fSetNumber(m_pSelectItem.SubItems["number"].Text, m_pSelectItem.SubItems["tnumber"].Text, m_pSelectItem.SubItems["ordernum"].Text);
            }
        }

        public string m_fQueryList()
        {
            try
            {
                this.qop.isGetTotal = true;
                DataSet ds = this.qop.QdataSet();
                if (ds != null && ds.Tables?.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable m_pDataTable = ds.Tables[0];
                    return $" AND id in ('{string.Join("','", m_pDataTable.AsEnumerable().Select(x => x.Field<object>("id")))}') ";
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][diallimit][m_fQueryList][Exception][{ex.Message}]");
            }
            return " AND 1=2 ";
        }

        public void m_fMultiAssign()
        {
            multiAssign _ = new multiAssign();
            _._entity = this;
            _.SearchEvent = new EventHandler(this.GetListBody);
            _.Show(this);
        }

        private void userListValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_bUaListLoading) return;

            int m_uUa = Convert.ToInt32(this.userListValue.SelectedValue);
            ///特定值操作
            if (m_uUa == -10 || m_uUa == -11 || m_uUa == -12)
            {
                this.m_fFlushUa(m_uUa);
            }
        }

        private void list_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.list.SelectedItems.Count == 1)
                {
                    int m_uShare = Convert.ToInt32(this.list.SelectedItems[0].SubItems["isshare"].Text);
                    if (m_uShare == -2)
                    {
                        ///呼叫内转号码配置
                        this.tsmiShare_2.Visible = true;
                        this.tsmiShare2.Visible = false;
                        this.contextListMenu.Show(list, e.Location);
                    }
                    else if (m_uShare == 2)
                    {
                        ///申请式号码配置
                        this.tsmiShare_2.Visible = false;
                        this.tsmiShare2.Visible = false;
                        this.contextListMenu.Show(list, e.Location);
                    }
                }
            }
        }

        private void tsmiShare_Click(object sender, EventArgs e)
        {
            try
            {
                switch (((ToolStripMenuItem)sender).Name)
                {
                    case "tsmiShare_2":
                        ///呼叫内转号码配置
                        int m_uID = Convert.ToInt32(this.list.SelectedItems[0].SubItems["id"].Text);
                        inlimit_2 _inlimit_2 = new inlimit_2(m_uID);
                        _inlimit_2.Show(this);
                        break;
                    case "tsmiShare2":
                        ///申请式号码配置
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][diallimit][tsmiShare_Click][Exception][{ex.Message}]");
            }
        }
    }
}

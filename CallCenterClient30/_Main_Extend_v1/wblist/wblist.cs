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

namespace CenoCC
{
    public partial class wblist : _index
    {
        private QueryPager qop;
        private DataSet m_ds;
        private bool m_bDoing = false;
        /// <summary>
        /// 统计表构造函数
        /// </summary>
        public wblist(bool uc = false)
        {
            InitializeComponent();
            if (uc)
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }
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
        private void btnSearchOpen_Click(object sender, EventArgs e)
        {
            if (this.searchEntity == null)
            {
                this.searchEntity = new wblistSearch(this);
                this.searchEntity.SearchEvent += new EventHandler(GetListBody);
                this.searchEntity.defaultArgsEvent += new EventHandler(defaultArgs);
                this.searchEntity.SetQueryMark();
                this.searchEntity.Show(this);
            }
        }
        private void defaultArgs(object sender, EventArgs e)
        {
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
        /// 加载列表表头
        /// </summary>
        private void LoadListHeader()
        {
            this.list.BeginUpdate();
            this.list.Columns.Add(new ColumnHeader() { Text = "序号", Width = 50 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.rname", Text = "路由名称", Width = 165, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.rnumber", Text = "号码表达式", Width = 135, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.ctype", Text = "路由方式", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.rtype", Text = "作用类型", Width = 120, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.rtext", Text = "作用范围枚举", Width = 360, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.ordernum", Text = "唯一索引", Width = 90, ImageIndex = 1, Tag = "asc" });
            this.list.EndUpdate();
            this.ucPager.pager.field = "T0.ordernum";
            this.ucPager.pager.type = "asc";
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

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                this.m_bDoing = true;

                try
                {
                    this.qop = new QueryPager();
                    this.qop.FieldsSqlPart = @"SELECT
	`ID`,
	`rname`,
	( CASE WHEN `ctype` = 1 THEN '正序取闲' WHEN `ctype` = 2 THEN '倒序取闲' WHEN `ctype` = 3 THEN '随机取闲' ELSE '未知' END ) AS `ctype`,
	( CASE WHEN `rtype` = 0 THEN '无限制' WHEN `rtype` = 1 THEN '使用作用范围枚举' ELSE '未知' END ) AS `rtype`,
	`rtext`,
	`rnumber`,
	`ordernum` ";
                    this.qop.FromSqlPart = @"FROM
	(
	SELECT
		`call_route`.`ID`,
		`call_route`.`rname`,
		`call_route`.`ctype`,
		`call_route`.`rtype`,
		`call_route`.`adduser`,
		`call_route`.`rnumber`,
		`call_route`.`ordernum`,
	CASE
			
			WHEN `call_route`.`rtype` = 1 THEN
			group_concat( DISTINCT IFNULL( `call_agent`.`LoginName`, IFNULL( `call_team`.`TeamName`, `call_role`.`RoleName` ) ) SEPARATOR ',' ) ELSE '-' 
		END AS `rtext` 
	FROM
		`call_route`
		LEFT JOIN `call_routeua` ON `call_route`.`ID` = `call_routeua`.`rid`
		LEFT JOIN `call_team` ON ( `call_routeua`.`muuid` = `call_team`.`ID` AND `call_routeua`.`mtype` = 'T' )
		LEFT JOIN `call_role` ON ( `call_routeua`.`muuid` = `call_role`.`ID` AND `call_routeua`.`mtype` = 'R' )
		LEFT JOIN `call_agent` ON ( `call_routeua`.`muuid` = `call_agent`.`ID` AND `call_routeua`.`mtype` = 'A' ) 
	GROUP BY
		`call_route`.`ID`,
		`call_route`.`rname`,
		`call_route`.`ctype`,
	`call_route`.`rtype` 
	) AS T0
";
                    this.qop.pager = this.ucPager.pager;
                    this.qop.setQuerySample(args);
                    ///查询条件
                    PopedomArgs popedomArgs = new PopedomArgs();
                    popedomArgs.type = DataPowerType._data_route_search;
                    popedomArgs.left.Add("T0.adduser");
                    this.qop.appQuery($"{m_cPower.m_fPopedomSQL(popedomArgs)}");
                    this.qop.setQuery("T0.rname", "rname");
                    this.qop.setQuery("T0.ctype", "ctype");
                    this.qop.setQuery("T0.rtype", "rtype");
                    this.qop.setQuery("T0.rtext", "rtext");
                    this.qop.setQuery("T0.rnumber", "rnumber");
                    this.qop.setQuery("T0.ordernum", "ordernum");
                    ///查询
                    DataSet ds = this.qop.QdataSet();
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
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "rname", Text = dr["rname"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "rnumber", Text = dr["rnumber"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "ctype", Text = dr["ctype"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "rtype", Text = dr["rtype"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "rtext", Text = dr["rtext"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "ordernum", Text = dr["ordernum"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "ID", Text = dr["ID"].ToString() });
                        this.list.Items.Add(listViewItem);
                    }
                    this.list.EndUpdate();
                    this.ucPager.Set(Convert.ToInt32(ds.Tables[0].Rows[0]["total"]));
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][route][GetListBody][Exception][{ex.Message}]");
                }
                finally
                {
                    this.m_bDoing = false;
                }
            })).Start();
        }

        #region 重写,这里可以不要,但是影响设计器显示
        protected override void btnSearch_Click(object sender, EventArgs e)
        {
            base.btnSearch_Click(sender, e);
        }
        protected override void btnReset_Click(object sender, EventArgs e)
        {
            this.defaultArgs(sender, e);
            if (this.searchEntity != null)
                base.btnReset_Click(sender, e);
        }
        #endregion

        #region 单字段排序
        private void list_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
                return;
            ColumnHeader columnHeader = this.list.Columns[e.Column];
            this.ucPager.pager.field = columnHeader.Name;
            this.list.BeginUpdate();
            if (columnHeader.Tag == null)
            {
                this.ucPager.pager.type = "asc";
                columnHeader.Tag = "asc";
                columnHeader.ImageIndex = 1;
            }
            else if (columnHeader.Tag.ToString() == "asc")
            {
                this.ucPager.pager.type = "desc";
                columnHeader.Tag = "desc";
                columnHeader.ImageIndex = 2;
            }
            else if (columnHeader.Tag.ToString() == "desc")
            {
                this.ucPager.pager.type = "asc";
                columnHeader.Tag = "asc";
                columnHeader.ImageIndex = 1;
            }
            foreach (ColumnHeader item in this.list.Columns)
            {
                if (
                    item.DisplayIndex != 0
                    &&
                    item.Name != this.ucPager.pager.field
                    &&
                    item.Name != "ID"
                    )
                {
                    item.ImageIndex = 0;
                    Tag = null;
                }
            }
            this.list.EndUpdate();
            this.GetListBody(this, null);
        }
        #endregion

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ///添加
            routeOp m = new routeOp(-1);
            m.SearchEvent = this.GetListBody;
            m.ShowDialog(this);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.list.SelectedItems.Count == 1)
            {
                routeOp m = new routeOp(Convert.ToInt32(this.list.SelectedItems[0].SubItems["ID"].Text));
                m.SearchEvent = this.GetListBody;
                m.ShowDialog(this);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.m_bDoing)
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "有任务正在执行,请稍后");
                    return;
                }

                List<int> m_lID = new List<int>();
                List<string> m_lName = new List<string>();
                if (this.list.SelectedItems.Count > 0)
                {
                    foreach (ListViewItem item in this.list.SelectedItems)
                    {
                        m_lID.Add(Convert.ToInt32(item.SubItems["ID"].Text));
                        m_lName.Add(item.SubItems["rname"].Text);
                    }
                }

                if (DialogResult.Yes != MessageBox.Show(this, $"确定要删除选中路由{(string.Join(",", m_lName))}吗?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    return;
                }

                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    try
                    {
                        this.m_bDoing = true;
                        string m_sID = string.Join("','", m_lID);
                        string m_sSQL = $@"
DELETE 
FROM
	`call_route` 
WHERE
	ID IN ('{m_sID}');
DELETE 
FROM
	`call_routeua` 
WHERE
	rid IN ('{m_sID}');
";
                        int m_uCount = MySQL_Method.ExecuteNonQuery(m_sSQL, true);
                        this.m_bDoing = false;
                        MessageBox.Show(this, "路由删除完成");
                        this.GetListBody(null, null);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][route][btnDelete_Click][Thread][Exception][{ex.Message}]");
                        Cmn_v1.Cmn.MsgWranThat(this, $"路由删除错误:{ex.Message}");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }

                })).Start();

            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][route][btnDelete_Click][Exception][{ex.Message}]");
                Cmn_v1.Cmn.MsgWranThat(this, $"路由删除错误:{ex.Message}");
                this.m_bDoing = false;
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ///让服务器加载上即可,直接把路由加载到服务器缓存中,提高查询速度
            WebSocket_v1.InWebSocketMain.Send(CenoSocket.M_Send._zdwh("ReloadRoute"));
            MessageBox.Show(this, "发送用户信息重载命令完成");
        }
    }
}

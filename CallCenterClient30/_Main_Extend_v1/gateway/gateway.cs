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
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace CenoCC
{
    public partial class gateway : _index
    {
        private bool m_bDoing = false;
        /// <![CDATA[
        /// 权限问题：这里还是要以全部为准,嵌套写法有点麻烦
        /// ]]>
        private QueryPager qop;
        /// <summary>
        /// 统计表构造函数
        /// </summary>
        public gateway(bool uc = false)
        {
            InitializeComponent();
            if (uc)
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }
            this.SearchEvent += new EventHandler(GetListBody);
            this.ucPager.PageSkipEvent += new EventHandler(GetListBody);
            this.LoadListHeader();
            this.defaultArgs(this, null);
            this.HandleCreated += new EventHandler((o, e) =>
            {
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
        private void btnSearchOpen_Click(object sender, EventArgs e)
        {
            if (this.searchEntity == null)
            {
                this.searchEntity = new gatewaySearch(this);
                this.searchEntity.SearchEvent += new EventHandler(GetListBody);
                this.searchEntity.defaultArgsEvent += new EventHandler(defaultArgs);
                this.searchEntity.SetQueryMark();
                this.searchEntity.StartPosition = FormStartPosition.CenterScreen;
                this.searchEntity.Show(this);
            }
        }
        private void defaultArgs(object sender, EventArgs e)
        {
            this.args = new Dictionary<string, object>();
        }

        /// <summary>
        /// 加载列表表头
        /// </summary>
        private void LoadListHeader()
        {
            this.list.BeginUpdate();
            this.list.Columns.Add(new ColumnHeader() { Text = "序号", Width = 50 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.gwtype", Text = "网关类型", Width = 160, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.remark", Text = "网关名称", Width = 160, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.gw_name", Text = "网关IP及端口", Width = 160, ImageIndex = 1, Tag = "asc" });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.isinlimit_2", Text = "支持呼叫转移", Width = 120, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "a.inlimit_2caller", Text = "转移规则前缀", Width = 135, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "gwstate", Text = "网关时态", Width = 160, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "gwstatus", Text = "网关状态", Width = 160, ImageIndex = 0 });
            this.list.EndUpdate();
            this.ucPager.pager.field = "a.gw_name";
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
                MessageBox.Show(this, "有统计任务正在执行,请稍后");
                return;
            }

            this.m_bDoing = true;

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    ///先查询出所有网关状态,导入到数据库中进行处理,
                    ///发送命令,查看gateway注册状态,并载入至列表中
                    List<m_mGateway> m_lGateway = new List<m_mGateway>();
                    string m_sErrMsg = string.Empty;
                    m_mResponseJSON _m_mResponseJSON = InWebSocketMain.SendAsyncObject(m_cFSCmd.m_sCmd_sofia_xmlstatus_gateway, m_cFSCmdType._m_sFSCmd);
                    if (_m_mResponseJSON.status == 0 && _m_mResponseJSON.result != null)
                    {
                        string m_sResultMessage = _m_mResponseJSON.result.ToString();
                        if (!m_sResultMessage.StartsWith("-ERR"))
                        {
                            using (StringReader m_pTextReader = new StringReader(m_sResultMessage))
                            {
                                ///XML稳定处理得到对应的信息
                                XDocument m_pXDocument = XDocument.Load(m_pTextReader);
                                m_lGateway = m_pXDocument.Descendants("gateway").Select(x =>
                                new m_mGateway
                                {
                                    name = x.Element("name").Value,
                                    state = x.Element("state").Value,
                                    status = x.Element("status").Value

                                }).ToList();
                            }
                        }
                        else
                        {
                            m_sErrMsg = m_sResultMessage;
                        }
                    }
                    if (m_lGateway == null || (m_lGateway != null && m_lGateway.Count <= 0))
                    {
                        m_lGateway.Add(new m_mGateway
                        {
                            name = string.Empty,
                            state = string.Empty,
                            status = string.Empty
                        });
                    }

                    string m_sGatewayFieldSQL = $" (CASE WHEN `a`.`gwtype` = 'gateway' THEN IFNULL(`b`.`gwstate`,'{(string.IsNullOrWhiteSpace(m_sErrMsg) ? "-Err Not Found" : m_sErrMsg)}') ELSE '-' END) AS `gwstate`,(CASE WHEN `a`.`gwtype` = 'gateway' THEN IFNULL(`b`.`gwstatus`,'{(string.IsNullOrWhiteSpace(m_sErrMsg) ? "-Err Not Found" : m_sErrMsg)}') ELSE '-' END) AS `gwstatus` ";
                    string m_sGatewayFromSQL = $" LEFT JOIN ( {string.Join(" UNION ", m_lGateway.Select(x => $" SELECT '{x.name}' AS `name`,'{x.state}' AS `gwstate`,'{x.status}' AS `gwstatus` "))} ) AS `b` ON `a`.`gw_name` = `b`.`name` ";

                    this.qop = new QueryPager();
                    this.qop.FieldsSqlPart = $@"SELECT
    *
";
                    this.qop.FromSqlPart = $@"FROM (SELECT
	`a`.`ID`,
    `a`.`adduser`,
	`a`.`gw_name`,
	`a`.`remark`,
	`a`.`gwtype`,
	`a`.`isinlimit_2`,
	`a`.`inlimit_2caller`,
	`a`.`UniqueID`,
	{m_sGatewayFieldSQL}
FROM
	`call_gateway` AS `a`
    {m_sGatewayFromSQL}) AS `a`
";
                    this.qop.pager = this.ucPager.pager;
                    this.qop.setQuerySample(args);
                    this.qop.setQuery("a.gw_name", "gw_name");
                    this.qop.setQuery("a.remark", "remark");
                    this.qop.setQuery("a.gwtype", "gwtype");
                    this.qop.setQuery("a.gwstate", "gwstate");
                    this.qop.setQuery("a.gwstatus", "gwstatus");
                    ///数据权限
                    PopedomArgs popedomArgs = new PopedomArgs();
                    popedomArgs.type = DataPowerType._data_user_search;
                    popedomArgs.left.Add("a.adduser");
                    popedomArgs.TSQL = "OR a.adduser = -1";
                    this.qop.appQuery($"{m_cPower.m_fPopedomSQL(popedomArgs)}");
                    ///查询
                    DataSet ds = this.qop.QdataSet();

                    int pageIndexStart = this.ucPager.PageIndexStart;
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        this.list.BeginUpdate();
                        this.list.Items.Clear();
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            ListViewItem listViewItem = new ListViewItem($"{pageIndexStart++}");
                            listViewItem.UseItemStyleForSubItems = false;
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "gwtype", Text = dr["gwtype"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "remark", Text = dr["remark"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "gw_name", Text = dr["gw_name"].ToString() });
                            ///呼叫转移
                            ListViewItem.ListViewSubItem isinlimit_2 = new ListViewItem.ListViewSubItem();
                            int m_uisinlimit_2 = Convert.ToInt32(dr["isinlimit_2"]);
                            switch (m_uisinlimit_2)
                            {
                                case 1:
                                    isinlimit_2.Name = "isinlimit_2";
                                    isinlimit_2.Text = $"是";
                                    isinlimit_2.ForeColor = Color.Green;
                                    break;
                                default:
                                    isinlimit_2.Name = "isinlimit_2";
                                    isinlimit_2.Text = $"否";
                                    isinlimit_2.ForeColor = Color.Red;
                                    break;
                            }
                            listViewItem.SubItems.Add(isinlimit_2);
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "inlimit_2caller", Text = dr["inlimit_2caller"].ToString() });
                            ListViewItem.ListViewSubItem gwstate = new ListViewItem.ListViewSubItem();
                            string m_sGwstate = dr["gwstate"].ToString();
                            switch (m_sGwstate)
                            {
                                case "REGED":
                                    gwstate.Name = "gwstate";
                                    gwstate.Text = m_sGwstate;
                                    gwstate.ForeColor = Color.Green;
                                    break;
                                case "TRYING":
                                    gwstate.Name = "gwstate";
                                    gwstate.Text = m_sGwstate;
                                    gwstate.ForeColor = Color.Orange;
                                    break;
                                default:
                                    gwstate.Name = "gwstate";
                                    gwstate.Text = m_sGwstate;
                                    gwstate.ForeColor = Color.Red;
                                    break;

                            }
                            listViewItem.SubItems.Add(gwstate);
                            ListViewItem.ListViewSubItem gwstatus = new ListViewItem.ListViewSubItem();
                            string m_sGwstatus = dr["gwstatus"].ToString();
                            switch (m_sGwstatus)
                            {
                                case "UP":
                                    gwstatus.Name = "gwstatus";
                                    gwstatus.Text = m_sGwstatus;
                                    gwstatus.ForeColor = Color.Green;
                                    break;
                                default:
                                    gwstatus.Name = "gwstatus";
                                    gwstatus.Text = m_sGwstatus;
                                    gwstatus.ForeColor = Color.Red;
                                    break;

                            }
                            listViewItem.SubItems.Add(gwstatus);
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "id", Text = dr["ID"].ToString() });
                            listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "UniqueID", Text = dr["UniqueID"].ToString() });
                            this.list.Items.Add(listViewItem);
                        }
                        this.list.EndUpdate();
                        this.ucPager.Set(Convert.ToInt32(ds.Tables[0].Rows[0]["total"]));
                    }));
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"gateway GetListBody:{ex.Message} {ex.StackTrace}");
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
            if (this.searchEntity != null)
                base.btnReset_Click(sender, e);
            else
                this.defaultArgs(sender, e);
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

        private void list_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.m_pUserBaseInfo != null && !m_pUserBaseInfo.IsDisposed && this.list?.SelectedItems?.Count == 1)
            //{
            //    var m_pSelectItem = this.list?.SelectedItems[0];
            //    this.m_pUserBaseInfo.m_fSetSelectInfo(m_pSelectItem.SubItems["agentname"].Text, m_pSelectItem.SubItems["loginname"].Text, m_pSelectItem.SubItems["teamid"].Text, m_pSelectItem.SubItems["roleid"].Text);
            //}
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
            {
                MessageBox.Show(this, "有任务正在执行,请稍后");
                return;
            }

            this.m_bDoing = true;

            try
            {
                gatewayCreate m_frm = new gatewayCreate();
                m_frm.m_fFlushGateway = new EventHandler(this.GetListBody);
                m_frm.Show(this);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][gateway][btnAdd_Click][Exception][{ex.Message}]");
            }
            finally
            {
                this.m_bDoing = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
            {
                MessageBox.Show(this, "有任务正在执行,请稍后");
                return;
            }

            if (this.list.SelectedItems.Count != 1)
            {
                MessageBox.Show(this, "请选择一个网关进行编辑");
                return;
            }

            this.m_bDoing = true;

            try
            {
                gatewayCreate m_frm = new gatewayCreate(this.list.SelectedItems[0].SubItems["UniqueID"].Text);
                m_frm.m_fFlushGateway = new EventHandler(this.GetListBody);
                m_frm.Show(this);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][gateway][btnEdit_Click][Exception][{ex.Message}]");
            }
            finally
            {
                this.m_bDoing = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
            {
                MessageBox.Show(this, "有任务正在执行,请稍后");
                return;
            }

            if (this.list.SelectedItems.Count <= 0)
            {
                MessageBox.Show(this, "请选择一个网关进行删除");
                return;
            }

            if (!Cmn.MsgQ("确定要删除该网关吗?")) return;

            this.m_bDoing = true;

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    ///首先判断是否有号码正在使用该网关,正在使用的网关无法删除
                    List<string> m_lID = new List<string>();
                    foreach (ListViewItem item in this.list.SelectedItems)
                    {
                        m_lID.Add(item.SubItems["id"].Text);
                    }
                    bool m_bUsing = false;
                    DataTable m_pDataTable = h_xml.m_fGatewayUsing(m_lID, out m_bUsing);
                    if (m_bUsing)
                    {
                        Cmn.MsgWran("有号码正在使用对应网关,请先确认已处理");
                        return;
                    }

                    StringBuilder m_sb = new StringBuilder();
                    ///换行
                    m_sb.AppendLine("一:删除网关文件");

                    m_mResponseJSON _m_mResponseJSON = new m_mResponseJSON();
                    List<string> m_lName = new List<string>();
                    if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
                    {
                        m_lName = m_pDataTable.AsEnumerable().Select(x => x.Field<string>("gw_name")).ToList();
                    }
                    ///删除网关文件,防止再次注册
                    _m_mResponseJSON = InWebSocketMain.SendAsyncObject($"{string.Join(",", m_lName)}", m_cFSCmdType._m_sDeleteGateway);
                    if (_m_mResponseJSON.status != 0)
                    {
                        Cmn.MsgWran($"删除网关文件失败:{_m_mResponseJSON.result ?? _m_mResponseJSON.msg},删除停止.");
                        return;
                    }
                    else
                    {
                        m_sb.Append($"{_m_mResponseJSON.result ?? _m_mResponseJSON.msg}");
                    }

                    ///换行
                    m_sb.AppendLine("\r\n二:杀死网关");
                    ///循环杀死网关文件的现有注册
                    foreach (string m_sName in m_lName)
                    {
                        _m_mResponseJSON = InWebSocketMain.SendAsyncObject($"{m_cFSCmd.m_sCmd_sofia_profile_external_killgw_}{m_sName}", m_cFSCmdType._m_sFSCmd);
                        if (_m_mResponseJSON.status == 0)
                            m_sb.Append($"杀死网关[{m_sName}]完成:{_m_mResponseJSON.result ?? _m_mResponseJSON.msg};");
                        else
                            m_sb.Append($"杀死网关[{m_sName}]失败:{_m_mResponseJSON.result ?? _m_mResponseJSON.msg};");
                    }

                    ///换行
                    m_sb.AppendLine("\r\n三:删除数据库中的网关数据");
                    ///最后删除数据
                    m_sb.AppendLine(h_xml.m_fDeleteGateway(m_lID));

                    MessageBox.Show(this, m_sb.ToString().TrimEnd(new char[] { ',' }));
                    this.m_bDoing = false;
                    this.GetListBody(null, null);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][gateway][btnReload_Click][Thread][Exception][{ex.Message}]");
                }
                finally
                {
                    this.m_bDoing = false;
                }
            })).Start();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
            {
                MessageBox.Show(this, "有任务正在执行,请稍后");
                return;
            }

            if (!Cmn.MsgQ("确定要重载网关吗?")) return;

            this.m_bDoing = true;

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    m_mResponseJSON _m_mResponseJSON = InWebSocketMain.SendAsyncObject(m_cFSCmd.m_sCmd_sofia_profile_external_rescan, m_cFSCmdType._m_sFSCmd);
                    if (_m_mResponseJSON.status == 0)
                    {
                        MessageBox.Show(this, $"网关重载成功");
                        this.m_bDoing = false;
                        this.GetListBody(null, null);
                    }
                    else
                    {
                        MessageBox.Show(this, $"{_m_mResponseJSON.msg}");
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][gateway][btnReload_Click][Thread][Exception][{ex.Message}]");
                }
                finally
                {
                    this.m_bDoing = false;
                }
            })).Start();
        }

        private void btnKill_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
            {
                MessageBox.Show(this, "有任务正在执行,请稍后");
                return;
            }

            if (this.list.SelectedItems.Count <= 0)
            {
                MessageBox.Show(this, "请选择网关进行杀死");
                return;
            }

            if (!Cmn.MsgQ("确定要杀死网关吗?")) return;

            this.m_bDoing = true;

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    m_mResponseJSON _m_mResponseJSON = new m_mResponseJSON();
                    StringBuilder m_sb = new StringBuilder();
                    ///循环杀死网关文件的现有注册
                    foreach (ListViewItem item in this.list.SelectedItems)
                    {
                        string m_sName = item.SubItems["gw_name"].Text;
                        _m_mResponseJSON = InWebSocketMain.SendAsyncObject($"{m_cFSCmd.m_sCmd_sofia_profile_external_killgw_}{m_sName}", m_cFSCmdType._m_sFSCmd);
                        if (_m_mResponseJSON.status == 0)
                            m_sb.Append($"杀死网关[{m_sName}]完成:{_m_mResponseJSON.result ?? _m_mResponseJSON.msg},");
                        else
                            m_sb.Append($"杀死网关[{m_sName}]失败:{_m_mResponseJSON.result ?? _m_mResponseJSON.msg},");
                    }

                    MessageBox.Show(this, $"{m_sb.ToString().TrimEnd(new char[] { ',' })}");
                    this.m_bDoing = false;
                    this.GetListBody(null, null);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][gateway][btnRestart_Click][Thread][Exception][{ex.Message}]");
                }
                finally
                {
                    this.m_bDoing = false;
                }
            })).Start();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
            {
                MessageBox.Show(this, "有任务正在执行,请稍后");
                return;
            }

            if (!Cmn.MsgQ("确定要重启网关吗?")) return;

            this.m_bDoing = true;

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    m_mResponseJSON _m_mResponseJSON = InWebSocketMain.SendAsyncObject(m_cFSCmd.m_sCmd_sofia_profile_external_restart, m_cFSCmdType._m_sFSCmd);
                    if (_m_mResponseJSON.status == 0)
                    {
                        MessageBox.Show(this, $"网关重启成功");
                        this.m_bDoing = false;
                        this.GetListBody(null, null);
                    }
                    else
                    {
                        MessageBox.Show(this, $"{_m_mResponseJSON.msg}");
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][gateway][btnRestart_Click][Thread][Exception][{ex.Message}]");
                }
                finally
                {
                    this.m_bDoing = false;
                }
            })).Start();
        }
    }
}

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
    public partial class inrule : _index
    {
        private QueryPager qop;
        private DataSet m_ds;
        private bool m_bDoing = false;
        /// <summary>
        /// 统计表构造函数
        /// </summary>
        public inrule(bool uc = false)
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
                this.searchEntity = new inruleSearch(this);
                this.searchEntity.SearchEvent += new EventHandler(GetListBody);
                this.searchEntity.defaultArgsEvent += new EventHandler(defaultArgs);
                this.searchEntity.SetQueryMark();
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
            this.list.Columns.Add(new ColumnHeader() { Name = "inrulename", Text = "内呼规则名称", Width = 165, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "inruleip", Text = "内呼规则IP", Width = 135, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "inruleport", Text = "内呼规则端口", Width = 135, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "inruleua", Text = "内呼规则ua", Width = 135, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "inrulesuffix", Text = "内呼规则前缀", Width = 135, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "inrulemain", Text = "是否为本机规则", Width = 135, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "ordernum", Text = "唯一索引", Width = 90, ImageIndex = 1, Tag = "asc" });
            this.list.EndUpdate();
            this.ucPager.pager.field = "ordernum";
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
	`call_inrule`.`inruleid` AS `ID`,
	`call_inrule`.`inrulename`,
	`call_inrule`.`inruleip`,
	`call_inrule`.`inruleport`,
	`call_inrule`.`inruleua`,
	`call_inrule`.`inrulesuffix`,
	`call_inrule`.`inrulemain`,
	`call_inrule`.`ordernum` ";
                    this.qop.FromSqlPart = @"FROM
	`call_inrule`";
                    this.qop.pager = this.ucPager.pager;
                    this.qop.setQuerySample(args);
                    ///查询条件
                    PopedomArgs popedomArgs = new PopedomArgs();
                    popedomArgs.type = DataPowerType._data_inrule_search;
                    popedomArgs.left.Add("adduser");
                    this.qop.appQuery($"{m_cPower.m_fPopedomSQL(popedomArgs)}");
                    this.qop.setQuery("inrulename", "inrulename");
                    this.qop.setQuery("inruleip", "inruleip");
                    this.qop.setQuery("inruleport", "inruleport");
                    this.qop.setQuery("inruleua", "inruleua");
                    this.qop.setQuery("inrulesuffix", "inrulesuffix");
                    this.qop.setQuery("inrulemain", "inrulemain");
                    this.qop.setQuery("ordernum", "ordernum");
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
                        listViewItem.UseItemStyleForSubItems = false;
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "inrulename", Text = dr["inrulename"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "inruleip", Text = dr["inruleip"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "inruleport", Text = dr["inruleport"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "inruleua", Text = dr["inruleua"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "inrulesuffix", Text = dr["inrulesuffix"].ToString() });

                        ///是否为本机规则
                        int m_uMain = Convert.ToInt32(dr["inrulemain"]);
                        var m_pSubItem = new ListViewItem.ListViewSubItem();
                        m_pSubItem.Name = "inrulemain";
                        if (m_uMain == 1)
                        {
                            m_pSubItem.Text = "是";
                            m_pSubItem.ForeColor = Color.Green;
                        }
                        else
                        {
                            m_pSubItem.Text = "否";
                            m_pSubItem.ForeColor = Color.Red;
                        }
                        listViewItem.SubItems.Add(m_pSubItem);

                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "ordernum", Text = dr["ordernum"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "ID", Text = dr["ID"].ToString() });
                        this.list.Items.Add(listViewItem);
                    }
                    this.list.EndUpdate();
                    this.ucPager.Set(Convert.ToInt32(ds.Tables[0].Rows[0]["total"]));
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][inrule][GetListBody][Exception][{ex.Message}]");
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
            inruleOp m = new inruleOp(-1);
            m.SearchEvent = this.GetListBody;
            m.ShowDialog(this);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.list.SelectedItems.Count == 1)
            {
                inruleOp m = new inruleOp(Convert.ToInt32(this.list.SelectedItems[0].SubItems["ID"].Text));
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
                        m_lName.Add(item.SubItems["inrulename"].Text);
                    }
                }
                else return;

                if (DialogResult.Yes != MessageBox.Show(this, $"确定要删除选中内呼规则{(string.Join(",", m_lName))}吗?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
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
	`call_inrule` 
WHERE
	`inruleid` IN ('{m_sID}');
";
                        int m_uCount = MySQL_Method.ExecuteNonQuery(m_sSQL, true);
                        this.m_bDoing = false;
                        MessageBox.Show(this, "内呼规则删除完成");
                        this.GetListBody(null, null);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][inrule][btnDelete_Click][Thread][Exception][{ex.Message}]");
                        Cmn_v1.Cmn.MsgWranThat(this, $"内呼规则删除错误:{ex.Message}");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }

                })).Start();

            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][inrule][btnDelete_Click][Exception][{ex.Message}]");
                Cmn_v1.Cmn.MsgWranThat(this, $"内呼规则删除错误:{ex.Message}");
                this.m_bDoing = false;
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ///让服务器加载上即可,直接把黑白名单加载到服务器缓存中,提高查询速度
            WebSocket_v1.InWebSocketMain.Send(CenoSocket.M_Send._zdwh("ReloadInrule"));
            MessageBox.Show(this, "发送内呼规则重载命令完成");
        }
    }
}

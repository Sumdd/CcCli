using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataBaseUtil;
using Core_v1;
using Cmn_v1;
using Common;
using WebSocket_v1;
using CenoSocket;

namespace CenoCC {
    public partial class cmnset : Form {
        private bool _init_ = false;
        private bool _update_ = false;
        private bool _using_ = false;
        private bool _dtmf_ = false;
        private bool _common_ = false;

        public EventHandler SearchEvent;
        public diallimit _entity;
        public cmnset() {
            InitializeComponent();
            this.init();

            this.cbxDtmf.DataSource = new string[3] { "", $"{Call_ParamUtil.inbound}", $"{Call_ParamUtil.bothSignal}" };

            #region ***号码类别
            {
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                ///增加一个类别,支持呼叫内转
                DataRow m_pDataRow_2 = m_pDataTable.NewRow();
                m_pDataRow_2["ID"] = -2;
                m_pDataRow_2["Name"] = "呼叫内转号码";
                m_pDataTable.Rows.Add(m_pDataRow_2);
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = 0;
                m_pDataRow1["Name"] = "专线号码";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 1;
                m_pDataRow2["Name"] = "共享号码";
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = 2;
                m_pDataRow3["Name"] = "申请式";
                m_pDataTable.Rows.Add(m_pDataRow3);
                this.cbxCommon.BeginUpdate();
                this.cbxCommon.DataSource = m_pDataTable;
                this.cbxCommon.ValueMember = "ID";
                this.cbxCommon.DisplayMember = "Name";
                this.cbxCommon.EndUpdate();

                ///设定选中项
                this.cbxCommon.SelectedValue = 0;
            }
            #endregion

            #region ***呼入规则
            {
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = 1;
                m_pDataRow1["Name"] = "查拨号限制";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 2;
                m_pDataRow2["Name"] = "查通话记录";
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = 3;
                m_pDataRow3["Name"] = "智能";
                m_pDataTable.Rows.Add(m_pDataRow3);
                this.cbxLimitCallRule.BeginUpdate();
                this.cbxLimitCallRule.DataSource = m_pDataTable;
                this.cbxLimitCallRule.ValueMember = "ID";
                this.cbxLimitCallRule.DisplayMember = "Name";
                this.cbxLimitCallRule.EndUpdate();
            }
            #endregion

            #region ***自动根据区号加拨前缀
            {
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = -1;
                m_pDataRow1["Name"] = "来源客户端";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 1;
                m_pDataRow2["Name"] = "启用";
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = 0;
                m_pDataRow3["Name"] = "禁用";
                m_pDataTable.Rows.Add(m_pDataRow3);
                this.cbxPrefixDealFlag.BeginUpdate();
                this.cbxPrefixDealFlag.DataSource = m_pDataTable;
                this.cbxPrefixDealFlag.ValueMember = "ID";
                this.cbxPrefixDealFlag.DisplayMember = "Name";
                this.cbxPrefixDealFlag.EndUpdate();
            }
            #endregion

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

        public void init() {
            if(this._init_)
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                this._init_ = true;
                try {
                    var as_sql = $@"select ifnull((select v from dial_parameter where k='areacode' limit 1),'') as areacode,
	                                       ifnull((select v from dial_parameter where k='areaname' limit 1),'') as areaname,
                                           ifnull((select v from dial_parameter where k='dialprefix' limit 1),'') as dialprefix,
                                           ifnull((select v from dial_parameter where k='diallocalprefix' limit 1),'') as diallocalprefix;";
                    var dt = MySQL_Method.BindTable(as_sql);
                    if(dt.Rows.Count > 0) {
                        this.txtAreaCode.Text = dt.Rows[0]["areacode"].ToString();
                        this.txtAreaName.Text = dt.Rows[0]["areaname"].ToString();
                        this.txtDialPrefix.Text = dt.Rows[0]["dialprefix"].ToString();
                        this.txtDialLocalPrefix.Text = dt.Rows[0]["diallocalprefix"].ToString();
                    }
                } catch(Exception ex) {
                    Log.Instance.Error($"cmnset init error:{ex.Message}");
                } finally {
                    this._init_ = false;
                }
            })).Start();
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if(this._update_)
                return;
            if (!Cmn.MsgQ("确定修改默认配置吗?"))
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                this._update_ = true;
                try {
                    var as_sql = ""
                        + $"update dial_parameter set v='{this.txtAreaCode.Text}' where k='areacode';\r\n"
                        + $"update dial_parameter set v='{this.txtAreaName.Text}' where k='areaname';\r\n"
                        + $"update dial_parameter set v='{this.txtDialPrefix.Text}' where k='dialprefix';\r\n"
                        + $"update dial_parameter set v='{this.txtDialLocalPrefix.Text}' where k='diallocalprefix';\r\n"
                        ;
                    this._do_invoke(MySQL_Method.ExecuteNonQuery(as_sql), "修改");
                } catch(Exception ex) {
                    Log.Instance.Error($"cmnset btnOK_Click error:{ex.Message}");
                } finally {
                    this._update_ = false;
                }
            })).Start();
        }

        private void btnReset_Click(object sender, EventArgs e) {
            this.init();
        }

        private void btnUsing_Click(object sender, EventArgs e) {
            if(this._using_)
                return;
            Button btn = (Button)sender;
            if (btn.Name == "btnUsingSelect" && this._entity?.list?.SelectedItems?.Count <= 0)
            {
                MessageBox.Show("没有任何选中项");
                return;
            }
            string m_sMsgBodyStr = btn.Name == "btnUsingSelect" ? "确定执行选中生效吗?" : "确定执行全部生效吗?";
            if (!Cmn.MsgQ(m_sMsgBodyStr))
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                this._using_ = true;
                try {
                    var as_sql_append = string.Empty;
                    if(btn.Name == "btnUsingSelect" && this._entity != null && this._entity.list.SelectedItems.Count > 0) {
                        var idlist = new List<string>();
                        foreach(ListViewItem item in this._entity.list.SelectedItems) {
                            idlist.Add(item.SubItems["id"].Text);
                        }
                        as_sql_append = $"and id in ({string.Join(",", idlist.ToArray())})";
                    }

                    ///根据查询条件查询出所有ID
                    if (string.IsNullOrWhiteSpace(as_sql_append))
                    {
                        as_sql_append = this._entity.m_fQueryList();
                    }

                    var as_sql = ""
                        + $"update dial_limit set\r\n"
                        + $"areacode='{this.txtAreaCode.Text}',\r\n"
                        + $"areaname='{this.txtAreaName.Text}',\r\n"
                        + $"dialprefix='{this.txtDialPrefix.Text}',\r\n"
                        + $"diallocalprefix='{this.txtDialLocalPrefix.Text}'\r\n"
                        + $"where isdel=0\r\n"
                        + $"{as_sql_append}";
                    this._do_invoke(MySQL_Method.ExecuteNonQuery(as_sql), "设置立即生效");
                    if(this.SearchEvent != null)
                        this.SearchEvent(this, null);
                } catch(Exception ex) {
                    Log.Instance.Error($"cmnset btnUsing_Click error:{ex.Message}");
                } finally {
                    this._using_ = false;
                }
            })).Start();
        }

        private void _do_invoke(int i, string t) {
            this.BeginInvoke(new MethodInvoker(() => {
                if(i > 0)
                    MessageBox.Show($"通用限制参数{t}完成");
                else
                    MessageBox.Show($"通用限制参数{t}失败");
            }));
        }

        private void btnUsingSelect_Click(object sender, EventArgs e) {
            if(this._entity != null && this._entity.list.SelectedItems.Count > 0) {
                this.btnUsing_Click(sender, e);
            } else {
                MessageBox.Show("没有任何选中项");
            }
        }

        private void btnDtmf_Click(object sender, EventArgs e)
        {
            if (this._dtmf_)
                return;
            Button btn = (Button)sender;
            if (btn.Name == "btnSelectDtmf" && this._entity?.list?.SelectedItems?.Count <= 0)
            {
                MessageBox.Show("没有任何选中项");
                return;
            }
            string m_sMsgBodyStr = btn.Name == "btnSelectDtmf" ? "确定修改选中项的按键发送方式吗?" : "确定修改全部的按键发送方式吗?";
            if (!Cmn.MsgQ(m_sMsgBodyStr))
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                this._dtmf_ = true;
                try
                {
                    var as_sql_append = string.Empty;
                    if (btn.Name == "btnSelectDtmf" && this._entity != null && this._entity.list.SelectedItems.Count > 0)
                    {
                        var idlist = new List<string>();
                        foreach (ListViewItem item in this._entity.list.SelectedItems)
                        {
                            idlist.Add(item.SubItems["id"].Text);
                        }
                        as_sql_append = $"and id in ({string.Join(",", idlist.ToArray())})";
                    }

                    ///根据查询条件查询出所有ID
                    if (string.IsNullOrWhiteSpace(as_sql_append))
                    {
                        as_sql_append = this._entity.m_fQueryList();
                    }

                    var as_sql = $"update dial_limit set dtmf='{this.cbxDtmf.Text}' where isdel=0 {as_sql_append};";
                    this._do_invoke(MySQL_Method.ExecuteNonQuery(as_sql), "设置按键发送方式");
                    if (this.SearchEvent != null)
                        this.SearchEvent(this, null);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"cmnset btnDtmf_Click error:{ex.Message}");
                }
                finally
                {
                    this._dtmf_ = false;
                }
            })).Start();
        }

        private void btnCommon_Click(object sender, EventArgs e)
        {
            if (this._common_)
                return;
            Button btn = (Button)sender;
            if (btn.Name == "btnSelectCommon" && this._entity?.list?.SelectedItems?.Count <= 0)
            {
                MessageBox.Show("没有任何选中项");
                return;
            }
            string m_sMsgBodyStr = btn.Name == "btnSelectCommon" ? "确定修改选中项的号码类别吗?" : "确定修改全部的号码类别吗?";
            if (!Cmn.MsgQ(m_sMsgBodyStr))
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                this._common_ = true;
                try
                {
                    var as_sql_append = string.Empty;
                    if (btn.Name == "btnSelectCommon" && this._entity != null && this._entity.list.SelectedItems.Count > 0)
                    {
                        var idlist = new List<string>();
                        foreach (ListViewItem item in this._entity.list.SelectedItems)
                        {
                            idlist.Add(item.SubItems["id"].Text);
                        }
                        as_sql_append = $"and id in ({string.Join(",", idlist.ToArray())})";
                    }

                    ///根据查询条件查询出所有ID
                    if (string.IsNullOrWhiteSpace(as_sql_append))
                    {
                        as_sql_append = this._entity.m_fQueryList();
                    }

                    var as_sql = $"update dial_limit set isshare={this.cbxCommon.SelectedValue} where isdel=0 {as_sql_append};";
                    this._do_invoke(MySQL_Method.ExecuteNonQuery(as_sql), "修改号码类别");
                    if (this.SearchEvent != null)
                        this.SearchEvent(this, null);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"cmnset btnCommon_Click error:{ex.Message}");
                }
                finally
                {
                    this._common_ = false;
                }
            })).Start();
        }

        private void btnUpdateShare_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cmn.MsgQ("确定要更新共享号码池吗?"))
                {
                    //激活
                    InWebSocketMain.Send(M_Send._zdwh("SHARE"));
                    Log.Instance.Success($"[CenoCC][cmnset][Exception][执行更新共享号码池]");
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][cmnset][Exception][{ex.Message}]");
            }
        }

        private void btnLimitCallRule_Click(object sender, EventArgs e)
        {
            if (this._common_)
                return;
            Button btn = (Button)sender;
            if (btn.Name == "btnLimitCallRuleSelect" && this._entity?.list?.SelectedItems?.Count <= 0)
            {
                MessageBox.Show("没有任何选中项");
                return;
            }
            string m_sMsgBodyStr = btn.Name == "btnLimitCallRuleSelect" ? "确定修改选中项的呼入规则吗?" : "确定修改全部的呼入规则吗?";
            if (!Cmn.MsgQ(m_sMsgBodyStr))
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                this._common_ = true;
                try
                {
                    var as_sql_append = string.Empty;
                    if (btn.Name == "btnLimitCallRuleSelect" && this._entity != null && this._entity.list.SelectedItems.Count > 0)
                    {
                        var idlist = new List<string>();
                        foreach (ListViewItem item in this._entity.list.SelectedItems)
                        {
                            idlist.Add(item.SubItems["id"].Text);
                        }
                        as_sql_append = $"and id in ({string.Join(",", idlist.ToArray())})";
                    }

                    ///根据查询条件查询出所有ID
                    if (string.IsNullOrWhiteSpace(as_sql_append))
                    {
                        as_sql_append = this._entity.m_fQueryList();
                    }

                    var as_sql = $"update dial_limit set LimitCallRule={this.cbxLimitCallRule.SelectedValue} where isdel=0 {as_sql_append};";
                    this._do_invoke(MySQL_Method.ExecuteNonQuery(as_sql), "修改呼入规则");
                    if (this.SearchEvent != null)
                        this.SearchEvent(this, null);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"cmnset btnLimitCallRule_Click error:{ex.Message}");
                }
                finally
                {
                    this._common_ = false;
                }
            })).Start();
        }

        private void btnPrefixDealFlag_Click(object sender, EventArgs e)
        {
            if (this._common_)
                return;
            Button btn = (Button)sender;
            if (btn.Name == "btnPrefixDealFlagSelect" && this._entity?.list?.SelectedItems?.Count <= 0)
            {
                MessageBox.Show("没有任何选中项");
                return;
            }
            string m_sMsgBodyStr = btn.Name == "btnPrefixDealFlagSelect" ? "确定修改选中项的自动根据区号加拨前缀参数吗?" : "确定修改全部的自动根据区号加拨前缀参数吗?";
            if (!Cmn.MsgQ(m_sMsgBodyStr))
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                this._common_ = true;
                try
                {
                    var as_sql_append = string.Empty;
                    if (btn.Name == "btnPrefixDealFlagSelect" && this._entity != null && this._entity.list.SelectedItems.Count > 0)
                    {
                        var idlist = new List<string>();
                        foreach (ListViewItem item in this._entity.list.SelectedItems)
                        {
                            idlist.Add(item.SubItems["id"].Text);
                        }
                        as_sql_append = $"and id in ({string.Join(",", idlist.ToArray())})";
                    }

                    ///根据查询条件查询出所有ID
                    if (string.IsNullOrWhiteSpace(as_sql_append))
                    {
                        as_sql_append = this._entity.m_fQueryList();
                    }

                    var as_sql = $"update dial_limit set prefixdealflag={this.cbxPrefixDealFlag.SelectedValue} where isdel=0 {as_sql_append};";
                    this._do_invoke(MySQL_Method.ExecuteNonQuery(as_sql), "修改自动根据区号加拨前缀");
                    if (this.SearchEvent != null)
                        this.SearchEvent(this, null);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"cmnset btnPrefixDealFlag_Click error:{ex.Message}");
                }
                finally
                {
                    this._common_ = false;
                }
            })).Start();
        }

        private void btnInlimit_2Reload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cmn.MsgQ("确定要重载呼叫内转配置吗?"))
                {
                    //激活
                    bool m_bSended = InWebSocketMain.Send(CenoSocket.M_Send._zdwh("ReloadInlimit_2"));
                    if (m_bSended)
                    {
                        Log.Instance.Success($"[CenoCC][cmnset][btnInlimit_2Reload_Click][Exception][执行重载呼叫内转配置]");
                    }
                    else
                    {
                        Cmn.MsgWranThat(this, "发送重载呼叫内转配置命令失败,请检查WebSocket是否连接等后重试");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][cmnset][btnInlimit_2Reload_Click][Exception][{ex.Message}]");
            }
        }

        private void btnF99d999OK_Click(object sender, EventArgs e)
        {
            if (this._common_)
                return;
            Button btn = (Button)sender;
            if (this._entity?.list?.SelectedItems?.Count <= 0)
            {
                MessageBox.Show("没有任何选中项");
                return;
            }
            string m_sMsgBodyStr = "确定要将选中项设置为首发吗?";
            if (!Cmn.MsgQ(m_sMsgBodyStr))
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                this._common_ = true;
                try
                {
                    var as_sql_append = string.Empty;
                    if (this._entity != null && this._entity.list.SelectedItems.Count > 0)
                    {
                        var idlist = new List<string>();
                        foreach (ListViewItem item in this._entity.list.SelectedItems)
                        {
                            idlist.Add(item.SubItems["id"].Text);
                        }
                        as_sql_append = $"and id in ({string.Join(",", idlist.ToArray())})";
                    }

                    if (!string.IsNullOrWhiteSpace(as_sql_append))
                    {
                        var as_sql = $"update dial_limit set ordernum = -99.999 where isdel=0 {as_sql_append};";
                        this._do_invoke(MySQL_Method.ExecuteNonQuery(as_sql), "将选中项设置为首发");
                        if (this.SearchEvent != null)
                            this.SearchEvent(this, null);
                    }
                    else this._do_invoke(0, "首发确定时必须选定行");
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"cmnset btnPrefixDealFlag_Click error:{ex.Message}");
                }
                finally
                {
                    this._common_ = false;
                }
            })).Start();
        }

        private void btnF99d999Reset_Click(object sender, EventArgs e)
        {
            if (this._common_)
                return;
            Button btn = (Button)sender;
            if (this._entity?.list?.SelectedItems?.Count <= 0)
            {
                MessageBox.Show("没有任何选中项");
                return;
            }
            string m_sMsgBodyStr = "确定要重置选中项的首发状态吗?";
            if (!Cmn.MsgQ(m_sMsgBodyStr))
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                this._common_ = true;
                try
                {
                    var as_sql_append = string.Empty;
                    if (this._entity != null && this._entity.list.SelectedItems.Count > 0)
                    {
                        var idlist = new List<string>();
                        foreach (ListViewItem item in this._entity.list.SelectedItems)
                        {
                            idlist.Add(item.SubItems["id"].Text);
                        }
                        as_sql_append = $"and id in ({string.Join(",", idlist.ToArray())})";
                    }

                    if (!string.IsNullOrWhiteSpace(as_sql_append))
                    {
                        var as_sql = $"update dial_limit set ordernum = 0 where isdel=0 {as_sql_append};";
                        this._do_invoke(MySQL_Method.ExecuteNonQuery(as_sql), "重置选中项的首发状态");
                        if (this.SearchEvent != null)
                            this.SearchEvent(this, null);
                    }
                    else this._do_invoke(0, "首发重置时必须选定行");
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"cmnset btnPrefixDealFlag_Click error:{ex.Message}");
                }
                finally
                {
                    this._common_ = false;
                }
            })).Start();
        }
    }
}

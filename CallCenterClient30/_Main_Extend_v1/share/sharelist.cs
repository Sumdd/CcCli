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
using WebSocket_v1;
using CenoSocket;

namespace CenoCC
{
    public partial class sharelist : _index
    {
        private QueryPager qop;
        private DataSet m_ds;
        private bool _export_ = false;
        private bool m_bDelete = false;
        private bool m_bIn = false;
        private bool m_bOut = false;
        /// <summary>
        /// 统计表构造函数
        /// </summary>
        public sharelist(bool uc = false)
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
            this.GetListBody(this, null);

            switch (Call_ParamUtil.m_uShareNumSetting)
            {
                case 0:
                    {
                        this.btnAreaSet.Enabled = false;
                        this.btnAreaDelete.Enabled = false;
                        this.btnAreaIn.Enabled = false;
                        this.btnAreaOut.Enabled = false;
                        this.btnAreaReload.Enabled = false;
                    }
                    break;
                case 1:
                    {
                        this.btnAreaSet.Enabled = true;
                        this.btnAreaDelete.Enabled = true;
                        this.btnAreaIn.Text = "加入域";
                        this.btnAreaIn.Enabled = true;
                        this.btnAreaOut.Text = "退出域";
                        this.btnAreaOut.Enabled = true;
                        this.btnAreaReload.Enabled = true;

                        ///操作权限
                        this.m_fLoadOperatePower(this.Controls);
                    }
                    break;
                case 2:
                    {
                        this.btnAreaSet.Enabled = true;
                        this.btnAreaDelete.Enabled = true;
                        this.btnAreaIn.Enabled = true;
                        this.btnAreaOut.Enabled = true;

                        ///操作权限
                        this.m_fLoadOperatePower(this.Controls);

                        this.btnAreaReload.Enabled = false;
                    }
                    break;
            }
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
                this.searchEntity = new shareSearch(this);
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
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.aname", Text = "域名称", Width = 300, ImageIndex = 2, Tag = "asc" });
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.aip", Text = "域IP", Width = 120, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.aport", Text = "域端口", Width = 85, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.adb", Text = "域数据库", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.auid", Text = "域用户名", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.apwd", Text = "域密码", Width = 100, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.amain", Text = "是否为主域", Width = 115, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T0.astate", Text = "状态", Width = 105, ImageIndex = 0 });
            this.list.EndUpdate();
            this.ucPager.pager.field = "T0.aname";
            this.ucPager.pager.type = "asc";
        }
        /// <summary>
        /// 加载列表内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetListBody(object sender, EventArgs e)
        {
            this.qop = new QueryPager();
            this.qop.FieldsSqlPart = $@"SELECT *";
            this.qop.FromSqlPart = $@"FROM `dial_area` AS T0";
            this.qop.pager = this.ucPager.pager;
            this.qop.setQuerySample(args);
            //域名称
            this.qop.setQuery("T0.aname", "name");
            //域IP
            this.qop.setQuery("T0.aip", "ip");
            //是否为主域
            this.qop.setQuery("T0.amain", "main");
            //状态
            this.qop.setQuery("T0.astate", "state");
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
            this.list.Items.Clear();
            this.list.BeginUpdate();
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                ListViewItem listViewItem = new ListViewItem($"{pageIndexStart++}");
                listViewItem.UseItemStyleForSubItems = false;
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "aname", Text = dr["aname"].ToString() });
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "aip", Text = dr["aip"].ToString() });
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "aport", Text = dr["aport"].ToString() });
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "adb", Text = dr["adb"].ToString() });
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "auid", Text = dr["auid"].ToString() });
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "apwd", Text = dr["apwd"].ToString() });
                var m_uShareNumSetting = Call_ParamUtil.m_uShareNumSetting;
                var m_pMain = new ListViewItem.ListViewSubItem();
                m_pMain.Name = "amainc";
                var m_pState = new ListViewItem.ListViewSubItem();
                m_pMain.Name = "astatec";
                //状态
                switch (Convert.ToInt32(dr["astate"]))
                {
                    case 0:
                        m_pState.Text = "未加入域";
                        m_pState.ForeColor = Color.DarkGray;
                        break;
                    case 1:
                        m_pState.Text = "加入域申请中...";
                        m_pState.ForeColor = Color.Orange;
                        break;
                    case 2:
                        m_pState.Text = "加入域成功";
                        m_pState.ForeColor = Color.Green;
                        break;
                    case 3:
                        m_pState.Text = "取消加入域";
                        m_pState.ForeColor = Color.Red;
                        break;
                    case 4:
                        m_pState.Text = "退域申请中...";
                        m_pState.ForeColor = Color.Orange;
                        break;
                }
                //域类型
                switch (Convert.ToInt32(dr["amain"]))
                {
                    case 0:
                        m_pMain.Text = "否";
                        m_pMain.ForeColor = Color.DarkGray;
                        break;
                    case 1:
                        m_pMain.Text = "是";
                        m_pMain.ForeColor = Color.Green;
                        break;
                    case 2:
                        m_pMain.Text = "本机";
                        m_pMain.ForeColor = Color.Green;
                        //m_pState.Text = "正常";
                        //m_pState.ForeColor = Color.Green;
                        break;
                }
                listViewItem.SubItems.Add(m_pMain);
                listViewItem.SubItems.Add(m_pState);
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "id", Text = dr["id"].ToString() });
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "amain", Text = dr["amain"].ToString() });
                listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "astate", Text = dr["astate"].ToString() });
                this.list.Items.Add(listViewItem);
            }
            this.list.EndUpdate();
            this.ucPager.Set(Convert.ToInt32(ds.Tables[0].Rows[0]["total"]));
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_export_)
                return;
            try
            {
                _export_ = true;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel文件(*.xls)|*.xls";
                sfd.FileName = "统计表-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
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
                        this.qop.isGetTotal = true;
                        DataSet ds = this.qop.QdataSet();
                        DataSet m_pDataSet = new DataSet();
                        DataTable m_pDataTable = ds.Tables[0].Copy();
                        m_pDataSet.Tables.Add(m_pDataTable);
                        m_pDataTable.Columns["agentname"].ColumnName = "姓名";
                        m_pDataTable.Columns["loginname"].ColumnName = "登录名";
                        m_pDataTable.Columns["createtime"].ColumnName = "日期";
                        m_pDataTable.Columns["totalcount"].ColumnName = "总通话量";
                        m_pDataTable.Columns["totaltime"].ColumnName = "总通话时长";
                        m_pDataTable.Columns["totaldialcount"].ColumnName = "总呼出量";
                        m_pDataTable.Columns["totaldialtime"].ColumnName = "总呼出时长";
                        m_pDataTable.Columns["totalcallcount"].ColumnName = "总呼入量";
                        m_pDataTable.Columns["totalcalltime"].ColumnName = "总呼入时长";
                        m_pDataTable.Columns["totalnocount"].ColumnName = "未接量";
                        m_pDataTable.Columns["totalhascount"].ColumnName = "有效通话量";
                        m_pDataTable.Columns["totalhastime"].ColumnName = "有效通话时长";
                        m_pDataTable.Columns["totalhasper"].ColumnName = "接通率";
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

        private void btnAreaSet_Click(object sender, EventArgs e)
        {
            int m_uCount = this.list.SelectedItems.Count;
            if (m_uCount > 1)
            {
                Cmn_v1.Cmn.MsgWran("请选择一个域进行编辑");
                return;
            }
            shareset m_pFrm = new shareset();
            m_pFrm._entity = this;
            m_pFrm.SearchEvent = new EventHandler(this.GetListBody);
            m_pFrm.Show(this);
        }

        private void btnAreaDelete_Click(object sender, EventArgs e)
        {
            if (this.m_bDelete)
                return;

            int m_uCount = this.list.SelectedItems.Count;
            if (m_uCount != 1)
            {
                Cmn_v1.Cmn.MsgWran("请选择一个域进行删除");
                return;
            }

            if (!Cmn_v1.Cmn.MsgQ("确定要删除该域吗"))
            {
                return;
            }

            int status = 0;
            string msg = "删除域失败";
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    this.m_bDelete = true;
                    DataTable m_pDataTable = DataBaseUtil.m_cEsyMySQL.m_fDelDialArea(Convert.ToInt32(this.list.SelectedItems[0].SubItems["id"].Text));
                    if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
                    {
                        status = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                        msg = m_pDataTable.Rows[0]["msg"].ToString();
                        if (status == 1)
                            Cmn_v1.Cmn.MsgOK(msg);
                        else
                            Cmn_v1.Cmn.MsgWran(msg);
                    }
                    else
                    {
                        Cmn_v1.Cmn.MsgWran(msg);
                    }
                    Log.Instance.Success($"[CenoCC][sharelist][btnAreaDelete_Click][Thread][{msg}]");
                    this.GetListBody(sender, e);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][sharelist][btnAreaDelete_Click][Thread][Exception][{ex.Message}]");
                }
                finally
                {
                    this.m_bDelete = false;
                }

            })).Start();
        }

        private void btnAreaIn_Click(object sender, EventArgs e)
        {
            if (this.m_bIn)
                return;

            int m_uCount = this.list.SelectedItems.Count;
            if (m_uCount != 1)
            {
                Cmn_v1.Cmn.MsgWran("请选择一项执行加入域申请");
                return;
            }

            if (!Cmn_v1.Cmn.MsgQ("确定执行加入域申请"))
            {
                return;
            }

            int status = 0;
            string msg = "加入域申请失败";
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    this.m_bIn = true;

                    Action m_pAction = () =>
                    {
                        DataTable m_pDataTable = DataBaseUtil.m_cEsyMySQL.m_fReqDialArea(Convert.ToInt32(this.list.SelectedItems[0].SubItems["id"].Text), 1);
                        this.m_fEndDo(status, msg, m_pDataTable);
                        Log.Instance.Success($"[CenoCC][sharelist][btnAreaIn_Click][Thread][{msg}]");
                        this.GetListBody(sender, e);
                    };
                    Invoke(m_pAction);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][sharelist][btnAreaIn_Click][Thread][Exception][{ex.Message}]");
                }
                finally
                {
                    this.m_bIn = false;
                }

            })).Start();
        }

        private void btnAreaOut_Click(object sender, EventArgs e)
        {
            if (this.m_bOut)
                return;

            int m_uCount = this.list.SelectedItems.Count;
            if (m_uCount != 1)
            {
                Cmn_v1.Cmn.MsgWran("请选择一项执行退出域申请");
                return;
            }

            if (!Cmn_v1.Cmn.MsgQ("确定执行退出域申请"))
            {
                return;
            }

            int status = 0;
            string msg = "退出域申请失败";
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    this.m_bOut = true;

                    Action m_pAction = () =>
                    {
                        DataTable m_pDataTable = DataBaseUtil.m_cEsyMySQL.m_fReqDialArea(Convert.ToInt32(this.list.SelectedItems[0].SubItems["id"].Text), 4);
                        this.m_fEndDo(status, msg, m_pDataTable);
                        Log.Instance.Success($"[CenoCC][sharelist][btnAreaOut_Click][Thread][{msg}]");
                        this.GetListBody(sender, e);
                    };
                    Invoke(m_pAction);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][sharelist][btnAreaOut_Click][Thread][Exception][{ex.Message}]");
                }
                finally
                {
                    this.m_bOut = false;
                }

            })).Start();
        }

        private void btnAreaReload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cmn.MsgQ("确定要重载域信息吗"))
                {
                    //激活,其实这里和号码更新一样即可,无需多出一个分支
                    InWebSocketMain.Send(M_Send._zdwh("AREA"));
                    Log.Instance.Success($"[CenoCC][cmnset][Exception][域重载]");
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][cmnset][Exception][{ex.Message}]");
            }
        }

        #region ***同步域操作
        private void m_fEndDo(int status, string msg, DataTable m_pDataTable)
        {
            if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
            {
                int m_uStatus = 0;
                status = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                msg = m_pDataTable.Rows[0]["msg"].ToString();

                Log.Instance.Debug(status);
                Log.Instance.Debug(msg);

                ///<![CDATA[
                /// 逻辑追加
                /// ]]>

                switch (status)
                {
                    case 10://主服务器退其它加入域
                    case 12://主域服务器通过其它加入域
                    case 20://取消加入域申请
                    case 21://加入域申请中,特殊
                    case 22://取消退出域申请
                    case 24://退出域申请中
                        {
                            DataRow m_pDataRow = m_pDataTable.Rows[0];
                            Model_v1.dial_area m_pDialArea = new Model_v1.dial_area();
                            //所选,做对端MySQL连接字符串
                            m_pDialArea.aname = m_pDataRow["@m_sName"].ToString();
                            m_pDialArea.aip = m_pDataRow["@m_sIP"].ToString();
                            m_pDialArea.aport = Convert.ToInt32(m_pDataRow["@m_uPort"].ToString());
                            m_pDialArea.adb = m_pDataRow["@m_sDb"].ToString();
                            m_pDialArea.auid = m_pDataRow["@m_sUid"].ToString();
                            m_pDialArea.apwd = m_pDataRow["@m_sPwd"].ToString();
                            //本机,对端数据库数据
                            string m_sName = m_pDataRow["@m_sName"].ToString();
                            string m_sIP = m_pDataRow["@_m_sIP"].ToString();
                            int m_uPort = Convert.ToInt32(m_pDataRow["@_m_uPort"].ToString());
                            string m_sDb = m_pDataRow["@_m_sDb"].ToString();
                            string m_sUid = m_pDataRow["@_m_sUid"].ToString();
                            string m_sPwd = m_pDataRow["@_m_sPwd"].ToString();
                            //执行存储过程
                            m_cEsyMySQL.m_fConnSetDialArea(m_sName, m_sIP, m_uPort, m_sDb, m_sUid, m_sPwd, status, m_pDialArea, out m_uStatus, ref msg);
                        }
                        break;
                    case 0:
                    default:
                        Cmn_v1.Cmn.MsgWran(msg);
                        return;
                }

                ///<![CDATA[
                /// 再根据status继续下一步,可能用不到,直接错误处理
                /// ]]>
                switch (m_uStatus)
                {
                    default:
                        Cmn_v1.Cmn.MsgOK(msg);
                        break;
                }
            }
            else
            {
                Cmn_v1.Cmn.MsgWran(msg);
            }
        }
        #endregion
    }
}

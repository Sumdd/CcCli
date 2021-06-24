using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core_v1;
using DataBaseUtil;
using Common;
using Model_v1;

namespace CenoCC
{
    public partial class diallimitSearch : _search
    {
        /// <summary>
        /// 统计表查询条件构造函数
        /// </summary>
        /// <param name="_senderEntity">统计表实体</param>
        public diallimitSearch(_index _senderEntity)
        {
            InitializeComponent();
            this._searchpanel = this.searchpanel;
            this.senderEntity = _senderEntity;
            this.SetArgsEvent += new EventHandler(this.SetArgs);
            this.LoadQueryKey();
            this.delayTimer.Tick += new EventHandler(delay_Tick);
            this.HandleCreated += new EventHandler((o, e) =>
            {
                this.LoadQueryValue();
                this.delayTimer.Start();
            });
        }
        /// <summary>
        /// 计时器,原因后期解释,暂时使用此方式解决
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delay_Tick(object sender, EventArgs e)
        {
            this.SetQueryValue();
            this.delayTimer.Stop();
        }

        /// <summary>
        /// 加载查询条件名称,查询符号
        /// </summary>
        private void LoadQueryKey()
        {
            this.agentKey.thisDefult("业务员", this.agentKey.Name, "=", false);
            this.numberKey.thisDefult("号码", this.numberKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.gatewayKey.thisDefult("网关", this.gatewayKey.Name, "=", false);
            this.areacodeKey.thisDefult("区号", this.areacodeKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.areanameKey.thisDefult("归属地", this.areanameKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.dialprefixKey.thisDefult("外地加拨", this.dialprefixKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.dtmfKey.thisDefult("dtmf(按键)", this.dtmfKey.Name, "=", false);
            this.isuseKey.thisDefult("状态", this.isuseKey.Name, "=", false);
            this.isusedialKey.thisDefult("禁止呼出", this.isusedialKey.Name, "=", false);
            this.isusecallKey.thisDefult("禁止呼入", this.isusecallKey.Name, "=", false);
            this.isshareKey.thisDefult("号码类别", this.isshareKey.Name, "=", false);
            this.tnumberKey.thisDefult("真实号码", this.tnumberKey.Name, "Like", true, ">", ">=", "<", "<=");
        }
        /// <summary>
        /// 加载查询参数默认值
        /// </summary>
        private void LoadQueryValue()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {

                    PopedomArgs popedomArgs = new PopedomArgs();
                    popedomArgs.type = DataPowerType._data_diallimit_limit_search;
                    popedomArgs.left.Add("a.ID");
                    string m_sPopedomSQL = m_cPower.m_fPopedomSQL(popedomArgs);

                    var dt = Call_AgentUtil.m_fGetAgentList(m_sPopedomSQL);
                    var dr = dt.NewRow();
                    dr["EmpID"] = -1;
                    dr["lr"] = "全部";
                    dt.Rows.InsertAt(dr, 0);

                    if (!this.IsDisposed)
                    {
                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            this.agentValue.BeginUpdate();
                            this.agentValue.DataSource = dt;
                            this.agentValue.DisplayMember = "lr";
                            this.agentValue.ValueMember = "EmpID";
                            this.agentValue.EndUpdate();

                            //业务员
                            this.argsKey = "";
                            if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("agent"))
                                this.agentValue.SelectedValue = this.senderEntity.args["agent"];
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"diallimit FillSeatUser:{ex.Message}");
                }

                #region ***网关
                try
                {

                    PopedomArgs popedomArgs = new PopedomArgs();
                    popedomArgs.type = DataPowerType._data_gateway_search;
                    popedomArgs.left.Add("`call_gateway`.`adduser`");
                    string m_sPopedomSQL = m_cPower.m_fPopedomSQL(popedomArgs);

                    var dt = m_cGateway.m_fGatewayList(null, m_sPopedomSQL);
                    var dr = dt.NewRow();
                    dr["id"] = -1;
                    dr["rgw"] = "全部";
                    dt.Rows.InsertAt(dr, 0);

                    if (!this.IsDisposed)
                    {
                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            this.gatewayValue.BeginUpdate();
                            this.gatewayValue.DataSource = dt;
                            this.gatewayValue.DisplayMember = "rgw";
                            this.gatewayValue.ValueMember = "id";
                            this.gatewayValue.EndUpdate();

                            //网关
                            if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("gateway"))
                                this.gatewayValue.SelectedValue = this.senderEntity.args["gateway"];
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"diallimit fill gateway:{ex.Message}");
                }
                #endregion

            })).Start();
            this.numberValue.Text = string.Empty;
            this.areacodeValue.Text = string.Empty;
            this.areanameValue.Text = string.Empty;
            this.dialprefixValue.Text = string.Empty;

            #region ***dtmf
            try
            {
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(string));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow0 = m_pDataTable.NewRow();
                m_pDataRow0["ID"] = "";
                m_pDataRow0["Name"] = "全部";
                m_pDataTable.Rows.Add(m_pDataRow0);
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = Call_ParamUtil.inbound;
                m_pDataRow1["Name"] = Call_ParamUtil.inbound;
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = Call_ParamUtil.clientSignal;
                m_pDataRow2["Name"] = Call_ParamUtil.clientSignal;
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = Call_ParamUtil.bothSignal;
                m_pDataRow3["Name"] = Call_ParamUtil.bothSignal;
                m_pDataTable.Rows.Add(m_pDataRow3);
                this.dtmfValue.BeginUpdate();
                this.dtmfValue.DataSource = m_pDataTable;
                this.dtmfValue.ValueMember = "ID";
                this.dtmfValue.DisplayMember = "Name";
                this.dtmfValue.EndUpdate();

                //按键
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("dtmf"))
                    this.dtmfValue.SelectedValue = this.senderEntity.args["dtmf"];
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"diallimit fill dtmf:{ex.Message}");
            }
            #endregion

            #region ***启用禁用
            try
            {
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = -1;
                m_pDataRow1["Name"] = "全部";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 1;
                m_pDataRow2["Name"] = "启用";
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = 0;
                m_pDataRow3["Name"] = "禁用";
                m_pDataTable.Rows.Add(m_pDataRow3);
                this.isuseValue.BeginUpdate();
                this.isuseValue.DataSource = m_pDataTable;
                this.isuseValue.ValueMember = "ID";
                this.isuseValue.DisplayMember = "Name";
                this.isuseValue.EndUpdate();

                //状态
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("isuse"))
                    this.isuseValue.SelectedValue = this.senderEntity.args["isuse"];
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"diallimit fill isuse:{ex.Message}");
            }
            #endregion

            #region ***禁止呼出
            try
            {
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = -1;
                m_pDataRow1["Name"] = "全部";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 0;
                m_pDataRow2["Name"] = "是";
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = 1;
                m_pDataRow3["Name"] = "否";
                m_pDataTable.Rows.Add(m_pDataRow3);
                this.isusedialValue.BeginUpdate();
                this.isusedialValue.DataSource = m_pDataTable;
                this.isusedialValue.ValueMember = "ID";
                this.isusedialValue.DisplayMember = "Name";
                this.isusedialValue.EndUpdate();

                //禁止呼出
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("isusedial"))
                    this.isusedialValue.SelectedValue = this.senderEntity.args["isusedial"];
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"diallimit fill isuse:{ex.Message}");
            }
            #endregion

            #region ***禁止呼入
            try
            {
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = -1;
                m_pDataRow1["Name"] = "全部";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 0;
                m_pDataRow2["Name"] = "是";
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = 1;
                m_pDataRow3["Name"] = "否";
                m_pDataTable.Rows.Add(m_pDataRow3);
                this.isusecallValue.BeginUpdate();
                this.isusecallValue.DataSource = m_pDataTable;
                this.isusecallValue.ValueMember = "ID";
                this.isusecallValue.DisplayMember = "Name";
                this.isusecallValue.EndUpdate();

                //禁止呼入
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("isusecall"))
                    this.isusecallValue.SelectedValue = this.senderEntity.args["isusecall"];
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"diallimit fill isuse:{ex.Message}");
            }
            #endregion

            #region ***号码类别
            try
            {
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow_1 = m_pDataTable.NewRow();
                m_pDataRow_1["ID"] = -1;
                m_pDataRow_1["Name"] = "全部";
                m_pDataTable.Rows.Add(m_pDataRow_1);
                ///增加一个类别,支持呼叫内转
                DataRow m_pDataRow_2 = m_pDataTable.NewRow();
                m_pDataRow_2["ID"] = -2;
                m_pDataRow_2["Name"] = "呼叫内转号码";
                m_pDataTable.Rows.Add(m_pDataRow_2);
                DataRow m_pDataRow0 = m_pDataTable.NewRow();
                m_pDataRow0["ID"] = 0;
                m_pDataRow0["Name"] = "专线号码";
                m_pDataTable.Rows.Add(m_pDataRow0);
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = 1;
                m_pDataRow1["Name"] = "共享号码";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 2;
                m_pDataRow2["Name"] = "申请式";
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = 3;
                m_pDataRow3["Name"] = "新呼出式续联";
                m_pDataTable.Rows.Add(m_pDataRow3);
                this.isshareValue.BeginUpdate();
                this.isshareValue.DataSource = m_pDataTable;
                this.isshareValue.ValueMember = "ID";
                this.isshareValue.DisplayMember = "Name";
                this.isshareValue.EndUpdate();

                //号码类别
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("isshare"))
                    this.isshareValue.SelectedValue = this.senderEntity.args["isshare"];
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"diallimit fill isshare:{ex.Message}");
            }
            #endregion

            this.tnumberValue.Text = string.Empty;
        }
        /// <summary>
        /// 从缓存中提取查询参数
        /// </summary>
        private void SetQueryValue()
        {
            if (this.senderEntity.args != null)
            {
                var args = this.senderEntity.args;
                //号码
                this.argsKey = "number";
                if (args.ContainsKey(argsKey))
                {
                    this.numberValue.Text = args[argsKey].ToString();
                }
                //区号
                this.argsKey = "areacode";
                if (args.ContainsKey(argsKey))
                {
                    this.areacodeValue.Text = args[argsKey].ToString();
                }
                //归属地
                this.argsKey = "areaname";
                if (args.ContainsKey(argsKey))
                {
                    this.areanameValue.Text = args[argsKey].ToString();
                }
                //外地加拨
                this.argsKey = "dialprefix";
                if (args.ContainsKey(argsKey))
                {
                    this.dialprefixValue.Text = args[argsKey].ToString();
                }
                ///真实号码
                this.argsKey = "tnumber";
                if (args.ContainsKey(argsKey))
                {
                    this.tnumberValue.Text = args[argsKey].ToString();
                }
            }
            ///权限项:查询共享号码通话记录
            if (m_cPower.Has(m_mOperate.diallimit_search_share))
            {
                this.isshareKey.Enabled = true;
                this.isshareValue.Enabled = true;
            }
            else
            {
                this.isshareKey.Enabled = false;
                this.isshareValue.Enabled = false;
            }
        }
        /// <summary>
        /// 将查询参数放入缓存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetArgs(object sender, EventArgs e)
        {
            if (this.IsReset)
            {
                this.LoadQueryKey();
                this.LoadQueryValue();
                this.delayTimer.Start();
                return;
            }
            this.senderEntity.args = new Dictionary<string, object>();
            this.SetArgsMark();
            //业务员
            var agent = Convert.ToInt32(this.agentValue.SelectedValue);
            if (agent != 0 && agent != -1)
            {
                this.senderEntity.args.Add("agent", agent);
            }
            //号码
            var number = this.numberValue.Text;
            if (!string.IsNullOrWhiteSpace(number))
            {
                this.senderEntity.args.Add("number", number);
            }
            //网关
            var gateway = Convert.ToInt32(this.gatewayValue.SelectedValue);
            if (gateway != -1)
            {
                this.senderEntity.args.Add("gateway", gateway);
            }
            //区号
            var areacode = this.areacodeValue.Text;
            if (!string.IsNullOrWhiteSpace(areacode))
            {
                this.senderEntity.args.Add("areacode", areacode);
            }
            //归属地
            var areaname = this.areanameValue.Text;
            if (!string.IsNullOrWhiteSpace(areaname))
            {
                this.senderEntity.args.Add("areaname", areaname);
            }
            //外地加拨
            var dialprefix = this.dialprefixValue.Text;
            if (!string.IsNullOrWhiteSpace(dialprefix))
            {
                this.senderEntity.args.Add("dialprefix", dialprefix);
            }
            //按键
            var dtmf = this.dtmfValue.SelectedValue?.ToString();
            if (!string.IsNullOrWhiteSpace(dtmf))
            {
                this.senderEntity.args.Add("dtmf", dtmf);
            }
            //状态
            var isuse = Convert.ToInt32(this.isuseValue.SelectedValue);
            if (isuse != -1)
            {
                this.senderEntity.args.Add("isuse", isuse);
            }
            //禁止呼出
            var isusedial = Convert.ToInt32(this.isusedialValue.SelectedValue);
            if (isusedial != -1)
            {
                this.senderEntity.args.Add("isusedial", isusedial);
            }
            //禁止呼入
            var isusecall = Convert.ToInt32(this.isusecallValue.SelectedValue);
            if (isusecall != -1)
            {
                this.senderEntity.args.Add("isusecall", isusecall);
            }
            //号码类别
            var isshare = Convert.ToInt32(this.isshareValue.SelectedValue);
            if (isshare != -1)
            {
                this.senderEntity.args.Add("isshare", isshare);
            }
            ///真实号码
            var tnumber = this.tnumberValue.Text;
            if (!string.IsNullOrWhiteSpace(tnumber))
            {
                this.senderEntity.args.Add("tnumber", tnumber);
            }
        }

        #region 重写,这里可以不要,但是影响设计器显示
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnSearch_Click(object sender, EventArgs e)
        {
            base.btnSearch_Click(sender, e);
        }
        /// <summary>
        /// 重置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnReset_Click(object sender, EventArgs e)
        {
            base.btnReset_Click(sender, e);
        }
        /// <summary>
        /// 查询后关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void btnCloseAfterSearch_Click(object sender, EventArgs e)
        {
            base.btnCloseAfterSearch_Click(sender, e);
        }
        #endregion
    }
}

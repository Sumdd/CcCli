using Common;
using Core_v1;
using DataBaseUtil;
using Model_v1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CenoCC
{
    public partial class RecReportSearch : _search
    {
        /// <summary>
        /// 统计表查询条件构造函数
        /// </summary>
        /// <param name="_senderEntity">统计表实体</param>
        public RecReportSearch(RecReport _senderEntity)
        {
            InitializeComponent();
            this._searchpanel = searchpanel;
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
            this.startDateTimeValue.Checked = true;
            this.endDateTimeValue.Checked = true;
            this.startSpeakTimeValue.Checked = false;
            this.endSpeakTimeValue.Checked = false;
            if (this.senderEntity.args != null)
            {
                var args = this.senderEntity.args;
                //是否使用开始日期时间
                this.argsKey = "useStartDateTime";
                if (args.ContainsKey(argsKey))
                {
                    this.startDateTimeValue.Checked = Convert.ToBoolean(args[argsKey]);
                }
                //是否使用结束日期时间
                this.argsKey = "useEndDateTime";
                if (args.ContainsKey(argsKey))
                {
                    this.endDateTimeValue.Checked = Convert.ToBoolean(args[argsKey]);
                }
                //是否使用通话时长起
                this.argsKey = "useStartSpeakTime";
                if (args.ContainsKey(argsKey))
                {
                    this.startSpeakTimeValue.Checked = Convert.ToBoolean(args[argsKey]);
                }
                //是否使用通话时长止
                this.argsKey = "useEndSpeakTime";
                if (args.ContainsKey(argsKey))
                {
                    this.endSpeakTimeValue.Checked = Convert.ToBoolean(args[argsKey]);
                }
            }
            this.delayTimer.Stop();
        }

        /// <summary>
        /// 加载查询条件名称,查询符号
        /// </summary>
        private void LoadQueryKey()
        {
            this.agentKey.thisDefult("业务员", this.agentKey.Name, "=", false);
            this.startDateTimeKey.thisDefult("拨打时间起", this.startDateTimeKey.Name, ">=", false);
            this.endDateTimeKey.thisDefult("拨打时间止", this.endDateTimeKey.Name, "<=", false);
            this.startSpeakTimeKey.thisDefult("通话时长起", this.startSpeakTimeKey.Name, ">=", false);
            this.endSpeakTimeKey.thisDefult("通话时长止", this.endSpeakTimeKey.Name, "<=", false);
            this.reportTypeKey.thisDefult("统计类型", this.reportTypeKey.Name, "=", false);
            this.reportSumAreaKey.thisDefult("统计范围类别", this.reportSumAreaKey.Name, "=", false);
            this.teamKey.thisDefult("部门", this.teamKey.Name, "=", false);
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
                    popedomArgs.type = DataPowerType._data_phonestatistical_search;
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
                            if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("agent"))
                                this.agentValue.SelectedValue = this.senderEntity.args["agent"];
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"RecReportSearch FillSeatUser:{ex.Message}");
                }
            })).Start();

            this.startDateTimeValue.Checked = true;
            this.startDateTimeValue.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            this.endDateTimeValue.Checked = true;
            this.endDateTimeValue.Text = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            this.startSpeakTimeValue.Checked = true;
            this.startSpeakTimeValue.Text = "00:00:00";
            this.endSpeakTimeValue.Checked = true;
            this.endSpeakTimeValue.Text = "00:00:00";
            //统计类型
            {
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(string));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = "typeSum";
                m_pDataRow1["Name"] = "汇总";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = "typeMonth";
                m_pDataRow2["Name"] = "每月";
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = "typeDay";
                m_pDataRow3["Name"] = "每天";
                m_pDataTable.Rows.Add(m_pDataRow3);
                this.reportTypeValue.BeginUpdate();
                this.reportTypeValue.DataSource = m_pDataTable;
                this.reportTypeValue.ValueMember = "ID";
                this.reportTypeValue.DisplayMember = "Name";
                this.reportTypeValue.EndUpdate();

                ///统计方式
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("reportType"))
                    this.reportTypeValue.SelectedValue = this.senderEntity.args["reportType"];
            }

            //统计范围类别
            {
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(string));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = "typeUa";
                m_pDataRow1["Name"] = "每坐席";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = "typeTeam";
                m_pDataRow2["Name"] = "每部门";
                m_pDataTable.Rows.Add(m_pDataRow2);
                this.reportSumAreaValue.BeginUpdate();
                this.reportSumAreaValue.DataSource = m_pDataTable;
                this.reportSumAreaValue.ValueMember = "ID";
                this.reportSumAreaValue.DisplayMember = "Name";
                this.reportSumAreaValue.EndUpdate();

                ///统计范围类别
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("reportSumArea"))
                    this.reportSumAreaValue.SelectedValue = this.senderEntity.args["reportSumArea"];
            }
            //部门
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    var dt = m_cEsyMySQL.m_fGetTeam();
                    var dr = dt.NewRow();
                    dr["ID"] = -1;
                    dr["n"] = "全部";
                    dt.Rows.InsertAt(dr, 0);

                    if (!this.IsDisposed)
                    {
                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            this.teamValue.BeginUpdate();
                            this.teamValue.DataSource = dt;
                            this.teamValue.DisplayMember = "n";
                            this.teamValue.ValueMember = "ID";
                            this.teamValue.EndUpdate();

                            //部门
                            if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("team"))
                                this.agentValue.SelectedValue = this.senderEntity.args["team"];
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"RecReportSearch FillTeam:{ex.Message}");
                }
            })).Start();
        }
        /// <summary>
        /// 从缓存中提取查询参数
        /// </summary>
        private void SetQueryValue()
        {
            if (this.senderEntity.args != null)
            {
                var args = this.senderEntity.args;
                //开始日期时间
                this.argsKey = "startDateTime";
                if (args.ContainsKey(argsKey))
                {
                    this.startDateTimeValue.Checked = true;
                    this.startDateTimeValue.Text = args[argsKey].ToString();
                }
                //结束日期时间
                this.argsKey = "endDateTime";
                if (args.ContainsKey(argsKey))
                {
                    this.endDateTimeValue.Checked = true;
                    this.endDateTimeValue.Text = args[argsKey].ToString();
                }
                //通话时长起
                this.argsKey = "startSpeakTime";
                if (args.ContainsKey(argsKey))
                {
                    this.startSpeakTimeValue.Checked = true;
                    this.startSpeakTimeValue.Text = args[argsKey].ToString();
                }
                //通话时长止
                this.argsKey = "endSpeakTime";
                if (args.ContainsKey(argsKey))
                {
                    this.endSpeakTimeValue.Checked = true;
                    this.endSpeakTimeValue.Text = args[argsKey].ToString();
                }
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
                this.SetQueryValue();
                this.delayTimer.Start();
                return;
            }
            this.senderEntity.args = new Dictionary<string, object>();
            this.SetArgsMark();
            //业务员
            var agent = Convert.ToInt32(this.agentValue.SelectedValue);
            this.senderEntity.args.Add("agent", agent);
            //是否使用开始日期时间
            var useStartDateTime = this.startDateTimeValue.Checked;
            this.senderEntity.args.Add("useStartDateTime", useStartDateTime);
            //开始日期时间
            var startDateTime = this.startDateTimeValue.Text;
            if (!string.IsNullOrWhiteSpace(startDateTime))
            {
                this.senderEntity.args.Add("startDateTime", startDateTime);
            }
            //是否使用结束日期时间
            var useEndDateTime = this.endDateTimeValue.Checked;
            this.senderEntity.args.Add("useEndDateTime", useEndDateTime);
            //结束日期时间
            var endDateTime = this.endDateTimeValue.Text;
            if (!string.IsNullOrWhiteSpace(endDateTime))
            {
                this.senderEntity.args.Add("endDateTime", endDateTime);
            }
            //是否使用通话时长起
            var useStartSpeakTime = this.startSpeakTimeValue.Checked;
            this.senderEntity.args.Add("useStartSpeakTime", useStartSpeakTime);
            //通话时间起
            var startSpeakTime = this.startSpeakTimeValue.Text;
            if (!string.IsNullOrWhiteSpace(startSpeakTime))
            {
                this.senderEntity.args.Add("startSpeakTime", startSpeakTime);
            }
            //是否使用通话时长止
            var useEndSpeakTime = this.endSpeakTimeValue.Checked;
            this.senderEntity.args.Add("useEndSpeakTime", useEndSpeakTime);
            //通话时间止
            var endSpeakTime = this.endSpeakTimeValue.Text;
            if (!string.IsNullOrWhiteSpace(endSpeakTime))
            {
                this.senderEntity.args.Add("endSpeakTime", endSpeakTime);
            }
            //统计类型
            var reportType = this.reportTypeValue.SelectedValue;
            this.senderEntity.args.Add("reportType", reportType);
            //统计范围类别
            var reportSumArea = this.reportSumAreaValue.SelectedValue;
            this.senderEntity.args.Add("reportSumArea", reportSumArea);
            //部门
            var team = this.teamValue.SelectedValue;
            this.senderEntity.args.Add("team", team);
            this.delayTimer.Start();
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

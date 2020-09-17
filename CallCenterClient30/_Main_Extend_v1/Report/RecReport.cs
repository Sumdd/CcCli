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
    public partial class RecReport : _index
    {
        private QueryPager qop;
        private DataSet m_ds;
        private bool m_bDoing = false;
        /// <summary>
        /// 统计表构造函数
        /// </summary>
        public RecReport(bool uc = false)
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
                this.searchEntity = new RecReportSearch(this);
                this.searchEntity.SearchEvent += new EventHandler(GetListBody);
                this.searchEntity.defaultArgsEvent += new EventHandler(defaultArgs);
                this.searchEntity.SetQueryMark();
                this.searchEntity.Show(this);
            }
        }
        private void defaultArgs(object sender, EventArgs e)
        {
            this.args = new Dictionary<string, object>();
            var etime = DateTime.Now;
            var _time = etime.ToString("yyyy-MM-dd");
            var stime = etime.AddDays(-1).ToString("yyyy-MM-dd");
            this.args.Add("useStartDateTime", "true");
            this.args.Add("startDateTimeMark", ">=");
            this.args.Add("startDateTime", $"{stime} 00:00:00");
            this.args.Add("useEndDateTime", "true");
            this.args.Add("endDateTimeMark", "<=");
            this.args.Add("endDateTime", $"{_time} 23:59:59");
            this.args.Add("useStartSpeakTime", "false");
            this.args.Add("startSpeakTimeMark", ">=");
            this.args.Add("startSpeakTime", $"00:00:00");
            this.args.Add("useEndSpeakTime", "false");
            this.args.Add("endSpeakTimeMark", "<=");
            this.args.Add("endSpeakTime", $"23:59:59");
            this.args.Add("reportType", "typeSum");
            this.args.Add("reportSumArea", "typeUa");
            this.args.Add("agent", "-1");
            this.args.Add("team", "-1");
        }

        /// <summary>
        /// 加载列表表头
        /// </summary>
        private void LoadListHeader()
        {
            this.list.BeginUpdate();
            this.list.Columns.Add(new ColumnHeader() { Text = "序号", Width = 50 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.teamname", Text = "部门", Width = 75, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.agentname", Text = "姓名", Width = 75, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.loginname", Text = "登录名", Width = 90, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.createtime", Text = "日期", Width = 90, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.totalcount", Text = "总通话量", Width = 90, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.totaltime", Text = "总通话时长", Width = 115, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.totaldialcount", Text = "总呼出量", Width = 90, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.totaldialtime", Text = "总呼出时长", Width = 115, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.totalcallcount", Text = "总呼入量", Width = 90, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.totalcalltime", Text = "总呼入时长", Width = 115, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.totalnocount", Text = "未接量", Width = 90, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.totalhascount", Text = "有效通话量", Width = 115, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.totalhastime", Text = "有效通话时长", Width = 125, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.totalhasctime", Text = "总拨打时长", Width = 125, ImageIndex = 0 });
            this.list.Columns.Add(new ColumnHeader() { Name = "T2.totalhasper", Text = "接通率", Width = 90, ImageIndex = 2, Tag = "desc" });
            this.list.EndUpdate();
            this.ucPager.pager.field = "T2.totalhasper";
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
                MessageBox.Show(this, "有统计任务正在执行,请稍后");
                return;
            }

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                this.m_bDoing = true;

                try
                {
                    this.qop = new QueryPager();

                    //汇总方式
                    var reportType = args["reportType"].ToString();
                    string m_sGroupBy = string.Empty;
                    string m_sColumn = string.Empty;
                    switch (reportType)
                    {
                        case "typeSum":
                            {
                                m_sColumn = $"'-' AS createtime,";
                            }
                            break;
                        case "typeMonth":
                            {
                                m_sColumn = $"date_format( createtime, '%Y-%m' ) AS createtime,";
                                m_sGroupBy = $",date_format( createtime, '%Y-%m' )";
                            }
                            break;
                        case "typeDay":
                            {
                                m_sColumn = $"date_format( createtime, '%Y-%m-%d' ) AS createtime,";
                                m_sGroupBy = $",date_format( createtime, '%Y-%m-%d' )";
                            }
                            break;
                    }

                    //统计范围类别
                    var reportSumArea = args["reportSumArea"].ToString();

                    ///字段的变化
                    string _m_sFiled = string.Empty;
                    ///分组的变化
                    string _m_sGroupBy = @"`call_agent`.`ID` AS `UserID`,`call_agent`.`AgentName` AS `RealName`,";

                    switch (reportSumArea)
                    {
                        case "typeUa":
                            {
                                _m_sFiled = @"`call_agent`.`ID` AS `UserID`,`call_agent`.`AgentName` AS `RealName`,`call_agent`.`LoginName` AS `LoginName`,-1 AS `TeamID`,'-' AS `TeamName`,";
                                _m_sGroupBy = @"UserID,RealName,LoginName,TeamID,TeamName";
                            }
                            break;
                        case "typeTeam":
                            {
                                _m_sFiled = @"-1 AS `UserID`,'-' AS `RealName`,'-' AS `LoginName`,`call_team`.`ID` AS `TeamID`,`call_team`.`TeamName` AS `TeamName`,";
                                _m_sGroupBy = @"UserID,RealName,LoginName,TeamID,TeamName";
                            }
                            break;
                    }

                    PopedomArgs popedomArgs = new PopedomArgs();
                    popedomArgs.type = DataPowerType._data_phonestatistical_search;
                    popedomArgs.left.Add("`call_record`.`AgentID`");
                    string m_sPopedomSQL = m_cPower.m_fPopedomSQL(popedomArgs);

                    this.qop.FieldsSqlPart = $@"SELECT 
    TeamName AS teamname,
    agentname AS agentname,
    LoginName AS loginname,
    {m_sColumn}
    CAST( totalcount AS SIGNED ) AS totalcount,
    CONVERT(SEC_TO_TIME(totaltime), CHAR(8)) AS totaltime,
    CAST( totaldialcount AS SIGNED ) AS totaldialcount,
    CONVERT (SEC_TO_TIME(totaldialtime), CHAR(8) ) AS totaldialtime,
    CAST( totalcallcount AS SIGNED ) AS totalcallcount,
    CONVERT (SEC_TO_TIME(totalcalltime), CHAR(8) ) AS totalcalltime,
    CAST( totalnocount AS SIGNED ) AS totalnocount,
    CAST( totalhascount AS SIGNED ) AS totalhascount,
    CONVERT (SEC_TO_TIME(totalhastime), CHAR(8) ) AS totalhastime,
    CONVERT (SEC_TO_TIME(totalhasctime), CHAR(8) ) AS totalhasctime,
    totalhasper";
                    this.qop.FromSqlPart = $@"FROM
                (
                SELECT
                    TeamName,
                    RealName as agentname,
                    LoginName,
                    createtime,
                    SUM(1) AS totalcount,
                    SUM(tlong) AS totaltime,
                    SUM(CASE WHEN Type = 2 THEN 1 ELSE 0 END) AS totaldialcount,
                    SUM(CASE WHEN Type = 2 THEN tlong ELSE 0 END) AS totaldialtime,
                    SUM(CASE WHEN Type = 1 THEN 1 ELSE 0 END) AS totalcallcount,
                    SUM(CASE WHEN Type = 1 THEN tlong ELSE 0 END) AS totalcalltime,
                    SUM(CASE WHEN Type IN(1, 2) THEN 0 ELSE 1 END) AS totalnocount,
                    SUM(CASE WHEN Type IN(1, 2) AND tlong > 0 THEN 1 ELSE 0 END) AS totalhascount,
                    SUM(CASE WHEN Type IN(1, 2) AND tlong > 0 THEN tlong ELSE 0 END) AS totalhastime,
                    SUM(CASE WHEN Type IN(1, 2) THEN tlong+wlong ELSE 0 END) AS totalhasctime,
                    CONCAT(ROUND((SUM(CASE WHEN Type IN(1, 2) AND tlong > 0 THEN 1 ELSE 0 END) * 1.0) / SUM(1) * 100, 2), '%' ) AS totalhasper
                FROM
                    (
                        SELECT 
                            {_m_sFiled}
                            `call_record`.`C_StartTime` AS `createtime`,
            	            `call_record`.`C_SpeakTime` AS `tlong`,
            	            `call_record`.`C_WaitTime` AS `wlong`,
                            (CASE WHEN( `call_record`.`CallType` = 3) THEN 1 WHEN( `call_record`.`CallType` = 1) THEN 2 ELSE 3 END) AS `Type` 

                        FROM
                            `call_record` LEFT JOIN `call_agent` ON `call_record`.`AgentID` = `call_agent`.`ID` 
                            LEFT JOIN `call_team` ON `call_team`.`ID` = `call_agent`.`TeamID`
            WHERE (( `call_record`.`AgentID` <> -(1)) AND( `call_record`.`CallType` IN(1, 3, 4)) )
            AND `call_record`.`isshare` = 0 
            {m_sPopedomSQL}
            AND ( ?agent = 0 OR ?agent = -1 OR `call_record`.`AgentID` = ?agent )
            AND ( ?useStartDateTime = 'false' OR `call_record`.`C_StartTime` >= ?startDateTime )
            AND ( ?useEndDateTime = 'false' OR `call_record`.`C_StartTime` <= ?endDateTime )
            AND ( ?useStartSpeakTime = 'false' OR `call_record`.`C_SpeakTime` >= TIME_TO_SEC(?startSpeakTime) )
            AND ( ?useEndSpeakTime = 'false' OR `call_record`.`C_SpeakTime` <= TIME_TO_SEC(?endSpeakTime) )
            AND ( ?team = 0 OR ?team = -1 OR `call_team`.`ID` = ?team )
        ) AS T1
    GROUP BY
        {_m_sGroupBy}
        {m_sGroupBy}
	) AS T2";

                    this.qop.pager = this.ucPager.pager;
                    this.qop.setQuerySample(args);
                    //业务员
                    this.qop.existQuery("agent");
                    //拨打时间起
                    this.qop.existQuery("useStartDateTime");
                    this.qop.existQuery("startDateTime");
                    //拨打时间止
                    this.qop.existQuery("useEndDateTime");
                    this.qop.existQuery("endDateTime");
                    //通话时长起
                    this.qop.existQuery("useStartSpeakTime");
                    this.qop.existQuery("startSpeakTime");
                    //通话时长止
                    this.qop.existQuery("useEndSpeakTime");
                    this.qop.existQuery("endSpeakTime");
                    //部门
                    this.qop.existQuery("team");
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
                    this.list.Items.Clear();
                    this.list.BeginUpdate();
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        ListViewItem listViewItem = new ListViewItem($"{pageIndexStart++}");
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "teamname", Text = dr["teamname"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "agentname", Text = dr["agentname"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "loginname", Text = dr["loginname"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "createtime", Text = dr["createtime"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "totalcount", Text = dr["totalcount"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "totaltime", Text = dr["totaltime"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "totaldialcount", Text = dr["totaldialcount"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "totaldialtime", Text = dr["totaldialtime"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "totalcallcount", Text = dr["totalcallcount"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "totalcalltime", Text = dr["totalcalltime"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "totalnocount", Text = dr["totalnocount"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "totalhascount", Text = dr["totalhascount"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "totalhastime", Text = dr["totalhastime"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "totalhasctime", Text = dr["totalhasctime"].ToString() });
                        listViewItem.SubItems.Add(new ListViewItem.ListViewSubItem() { Name = "totalhasper", Text = dr["totalhasper"].ToString() });
                        this.list.Items.Add(listViewItem);
                    }
                    this.list.EndUpdate();
                    this.ucPager.Set(Convert.ToInt32(ds.Tables[0].Rows[0]["total"]));
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][RecReport][GetListBody][Exception][{ex.Message}]");
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
            {
                MessageBox.Show(this, "有统计任务正在执行,请稍后");
                return;
            }

            try
            {
                this.m_bDoing = true;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel文件(*.xls)|*.xls";
                sfd.FileName = "统计表-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
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
                        m_pDataTable.Columns["teamname"].ColumnName = "部门";
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
                        m_pDataTable.Columns["totalhasctime"].ColumnName = "总拨打时长";
                        m_pDataTable.Columns["totalhasper"].ColumnName = "接通率";
                        m_cExcel.m_fExport(m_pDataSet, sfd.FileName);
                        this.m_bDoing = false;
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][RecReport][btnExport_Click][Thread][Exception][{ex.Message}]");
                        this.m_bDoing = false;
                    }

                }).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][RecReport][btnExport_Click][Exception][{ex.Message}]");
                this.m_bDoing = false;
            }
        }
    }
}

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
    public partial class ReportSearch : _search
    {
        /// <summary>
        /// 统计表查询条件构造函数
        /// </summary>
        /// <param name="_senderEntity">统计表实体</param>
        public ReportSearch(Report _senderEntity)
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
            this.localPhoneKey.thisDefult("坐席电话", this.localPhoneKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.phoneKey.thisDefult("电话", this.phoneKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.startDateTimeKey.thisDefult("拨打时间起", this.startDateTimeKey.Name, ">=", false);
            this.endDateTimeKey.thisDefult("拨打时间止", this.endDateTimeKey.Name, "<=", false);
            this.startSpeakTimeKey.thisDefult("通话时长起", this.startSpeakTimeKey.Name, ">=", false);
            this.endSpeakTimeKey.thisDefult("通话时长止", this.endSpeakTimeKey.Name, "<=", false);
            this.callKey.thisDefult("电话类型", this.callKey.Name, "=", false);
            this.isshareKey.thisDefult("号码类别", this.isshareKey.Name, "=", false);
            this.agentNameKey.thisDefult("姓名", this.agentNameKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.loginNameKey.thisDefult("登录名", this.loginNameKey.Name, "Like", true, ">", ">=", "<", "<=");
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
                    popedomArgs.type = DataPowerType._data_phonerecords_search;
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
                    Log.Instance.Error($"diallimit FillSeatUser:{ex.Message}");
                }
            })).Start();

            this.localPhoneValue.Text = string.Empty;
            this.phoneValue.Text = string.Empty;
            this.startDateTimeValue.Checked = true;
            this.startDateTimeValue.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            this.endDateTimeValue.Checked = true;
            this.endDateTimeValue.Text = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            this.startSpeakTimeValue.Checked = true;
            this.startSpeakTimeValue.Text = "00:00:00";
            this.endSpeakTimeValue.Checked = true;
            this.endSpeakTimeValue.Text = "00:00:00";
            this.ckc_callOutValue.Checked = true;
            this.ckc_callInValue.Checked = true;
            this.ck_callQValue.Checked = true;
            this.ck_callLValue.Checked = true;
            this.ck_callNoAnswerValue.Checked = true;
            this.ck_callAutoDialValue.Checked = true;
            //号码类别
            {
                this.isshareValue.BeginUpdate();
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow0 = m_pDataTable.NewRow();
                m_pDataRow0["ID"] = -1;
                m_pDataRow0["Name"] = "全部";
                m_pDataTable.Rows.Add(m_pDataRow0);
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = 0;
                m_pDataRow1["Name"] = "专线号码";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 1;
                m_pDataRow2["Name"] = "共享号码";
                m_pDataTable.Rows.Add(m_pDataRow2);
                this.isshareValue.DataSource = m_pDataTable;
                this.isshareValue.ValueMember = "ID";
                this.isshareValue.DisplayMember = "Name";
                this.isshareValue.EndUpdate();

                //号码类别
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("isshare"))
                    this.isshareValue.SelectedValue = this.senderEntity.args["isshare"];
            }
            this.agentNameValue.Text = string.Empty;
            this.loginNameValue.Text = string.Empty;
        }
        /// <summary>
        /// 从缓存中提取查询参数
        /// </summary>
        private void SetQueryValue()
        {
            if (this.senderEntity.args != null)
            {
                var args = this.senderEntity.args;
                //坐席电话
                this.argsKey = "localPhone";
                if (args.ContainsKey(argsKey))
                {
                    this.localPhoneValue.Text = args[argsKey].ToString();
                }
                //电话
                this.argsKey = "phone";
                if (args.ContainsKey(argsKey))
                {
                    this.phoneValue.Text = args[argsKey].ToString();
                }
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
                //外线
                this.argsKey = "callOut";
                if (args.ContainsKey(argsKey))
                {
                    this.ckc_callOutValue.Checked = Convert.ToBoolean(args[argsKey]);
                }
                //内线
                this.argsKey = "callIn";
                if (args.ContainsKey(argsKey))
                {
                    this.ckc_callInValue.Checked = Convert.ToBoolean(args[argsKey]);
                }
                //去电
                this.argsKey = "callQ";
                if (args.ContainsKey(argsKey))
                {
                    this.ck_callQValue.Checked = Convert.ToBoolean(args[argsKey]);
                }
                //来电
                this.argsKey = "callL";
                if (args.ContainsKey(argsKey))
                {
                    this.ck_callLValue.Checked = Convert.ToBoolean(args[argsKey]);
                }
                //未接来电
                this.argsKey = "callNoAnswer";
                if (args.ContainsKey(argsKey))
                {
                    this.ck_callNoAnswerValue.Checked = Convert.ToBoolean(args[argsKey]);
                }
                //自动拨号
                this.argsKey = "callAutoDial";
                if (args.ContainsKey(argsKey))
                {
                    this.ck_callAutoDialValue.Checked = Convert.ToBoolean(args[argsKey]);
                }
                //姓名
                this.argsKey = "agentName";
                if (args.ContainsKey(argsKey))
                {
                    this.agentNameValue.Text = args[argsKey].ToString();
                }
                //登录名
                this.argsKey = "loginName";
                if (args.ContainsKey(argsKey))
                {
                    this.loginNameValue.Text = args[argsKey].ToString();
                }
            }
            ///权限项:查询共享号码通话记录
            if (m_cPower.Has(m_mOperate.phonerecords_search_share))
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
                this.SetQueryValue();
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
            //坐席电话
            var localPhone = this.localPhoneValue.Text;
            if (!string.IsNullOrWhiteSpace(localPhone))
            {
                this.senderEntity.args.Add("localPhone", localPhone);
            }
            //电话
            var phone = this.phoneValue.Text;
            if (!string.IsNullOrWhiteSpace(phone))
            {
                this.senderEntity.args.Add("phone", phone);
            }
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
            this.delayTimer.Start();
            //外线
            var callOut = this.ckc_callOutValue.Checked;
            this.senderEntity.args.Add("callOut", callOut);
            //内线
            var callIn = this.ckc_callInValue.Checked;
            this.senderEntity.args.Add("callIn", callIn);
            //去电
            var callQ = this.ck_callQValue.Checked;
            this.senderEntity.args.Add("callQ", callQ);
            //来电
            var callL = this.ck_callLValue.Checked;
            this.senderEntity.args.Add("callL", callL);
            //未接来电
            var callNoAnswer = this.ck_callNoAnswerValue.Checked;
            this.senderEntity.args.Add("callNoAnswer", callNoAnswer);
            //自动拨号
            var callAutoDial = this.ck_callAutoDialValue.Checked;
            this.senderEntity.args.Add("callAutoDial", callAutoDial);
            //拼接电话类型参数
            this.H_Args_Ct();
            //号码类别
            var isshare = Convert.ToInt32(this.isshareValue.SelectedValue);
            if (isshare != -1)
            {
                this.senderEntity.args.Add("isshare", isshare);
            }
            //姓名
            var agentName = this.agentNameValue.Text;
            if (!string.IsNullOrWhiteSpace(agentName))
            {
                this.senderEntity.args.Add("agentName", agentName);
            }
            //登录名
            var loginName = this.loginNameValue.Text;
            if (!string.IsNullOrWhiteSpace(loginName))
            {
                this.senderEntity.args.Add("loginName", loginName);
            }
        }

        #region 拼接电话类型参数
        /// <summary>
        /// 拼接电话类型参数
        /// </summary>
        private void H_Args_Ct()
        {
            bool hasCkc = false;
            List<int?> _intList = new List<int?>() { -1 };
            foreach (Control control in this.searchpanel.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    if (checkBox.Name.StartsWith("ckc_call") && checkBox.Checked)
                    {
                        hasCkc = true;
                        if (checkBox.Tag != null)
                        {
                            string tag = checkBox.Tag.ToString();
                            string[] _ids = tag.Split(',');
                            foreach (string _id in _ids)
                            {
                                int id = Convert.ToInt32(_id);
                                int? find = _intList.FirstOrDefault(x => x == id);
                                if (find == null)
                                {
                                    _intList.Add(id);
                                }
                            }
                        }
                    }
                }
            }
            if (hasCkc)
            {
                bool hasCk = false;
                foreach (Control control in this.searchpanel.Controls)
                {
                    if (control is CheckBox)
                    {
                        CheckBox checkBox = (CheckBox)control;
                        if (checkBox.Name.StartsWith("ck_call") && checkBox.Checked)
                        {
                            hasCk = true;
                            break;
                        }
                    }
                }
                if (hasCk)
                    this.cutCallCkargs(_intList);
                else
                    this.senderEntity.args.Add("callArgs", string.Join(",", _intList));
            }
            else
                this.appendCallCkargs(_intList);
        }
        private void appendCallCkargs(List<int?> _intList)
        {
            foreach (Control control in this.searchpanel.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    if (checkBox.Name.StartsWith("ck_call") && checkBox.Checked)
                    {
                        if (checkBox.Tag != null)
                        {
                            string tag = checkBox.Tag.ToString();
                            string[] _ids = tag.Split(',');
                            foreach (string _id in _ids)
                            {
                                int id = Convert.ToInt32(_id);
                                int? find = _intList.FirstOrDefault(x => x == id);
                                if (find == null)
                                {
                                    _intList.Add(id);
                                }
                            }
                        }
                    }
                }
            }
            var callArgs = string.Join(",", _intList);
            this.senderEntity.args.Add("callArgs", callArgs);
        }
        private void cutCallCkargs(List<int?> _intList)
        {
            foreach (Control control in this.searchpanel.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    if (checkBox.Name.StartsWith("ck_call") && !checkBox.Checked)
                    {
                        if (checkBox.Tag != null)
                        {
                            string tag = checkBox.Tag.ToString();
                            string[] _ids = tag.Split(',');
                            foreach (string _id in _ids)
                            {
                                int id = Convert.ToInt32(_id);
                                int? find = _intList.FirstOrDefault(x => x == id);
                                if (find != null)
                                {
                                    _intList.Remove(find);
                                }
                            }
                        }
                    }
                }
            }
            var callArgs = string.Join(",", _intList);
            this.senderEntity.args.Add("callArgs", callArgs);
        }
        #endregion

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

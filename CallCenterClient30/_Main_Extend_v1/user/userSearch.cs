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

namespace CenoCC
{
    public partial class userSearch : _search
    {
        /// <summary>
        /// 统计表查询条件构造函数
        /// </summary>
        /// <param name="_senderEntity">统计表实体</param>
        public userSearch(_index _senderEntity)
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
            this.roleKey.thisDefult("角色", this.roleKey.Name, "=", false);
            this.chTypeKey.thisDefult("通道类型", this.chTypeKey.Name, "=", false);
            this.chNumKey.thisDefult("分机号", this.chNumKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.agentNameKey.thisDefult("姓名", this.agentNameKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.loginNameKey.thisDefult("登陆名", this.loginNameKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.IPsKey.thisDefult("IP变化单位", this.IPsKey.Name, "=", false);
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
                    var dt = Call_RoleUtil.m_fGetRoleList();
                    var dr = dt.NewRow();
                    dr["ID"] = -1;
                    dr["RoleName"] = "全部";
                    dt.Rows.InsertAt(dr, 0);

                    if (!this.IsDisposed)
                    {
                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            this.roleValue.BeginUpdate();
                            this.roleValue.DataSource = dt;
                            this.roleValue.DisplayMember = "RoleName";
                            this.roleValue.ValueMember = "ID";
                            this.roleValue.EndUpdate();

                            //角色
                            if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("role"))
                                this.roleValue.SelectedValue = this.senderEntity.args["role"];
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"userSearch LoadQueryValue:{ex.Message}");
                }
            })).Start();
            //通道类型
            {
                var dt = new DataTable();
                dt.Columns.Add(new DataColumn("id", typeof(string)));
                dt.Columns.Add(new DataColumn("name", typeof(string)));
                var dr0 = dt.NewRow();
                dr0["id"] = -1;
                dr0["name"] = "全部";
                dt.Rows.Add(dr0);
                var dr1 = dt.NewRow();
                dr1["id"] = 16;
                dr1["name"] = "SIP通道";
                dt.Rows.Add(dr1);
                var dr2 = dt.NewRow();
                dr2["id"] = 256;
                dr2["name"] = "自动外呼通道";
                dt.Rows.Add(dr2);
                this.chTypeValue.BeginUpdate();
                this.chTypeValue.DataSource = dt;
                this.chTypeValue.DisplayMember = "name";
                this.chTypeValue.ValueMember = "id";
                this.chTypeValue.EndUpdate();

                //通道类型
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("chType"))
                    this.chTypeValue.SelectedValue = this.senderEntity.args["chType"];
            }

            //IP变化单位
            {
                var dt = new DataTable();
                dt.Columns.Add(new DataColumn("id", typeof(string)));
                dt.Columns.Add(new DataColumn("name", typeof(string)));
                var dr0 = dt.NewRow();
                dr0["id"] = -1;
                dr0["name"] = "不查询IP变化";
                dt.Rows.Add(dr0);
                var dr1 = dt.NewRow();
                dr1["id"] = 1;
                dr1["name"] = "1小时内";
                dt.Rows.Add(dr1);
                var dr2 = dt.NewRow();
                dr2["id"] = 2;
                dr2["name"] = "2小时内";
                dt.Rows.Add(dr2);
                var dr24 = dt.NewRow();
                dr24["id"] = 24;
                dr24["name"] = "1天内";
                dt.Rows.Add(dr24);
                this.IPsValue.BeginUpdate();
                this.IPsValue.DataSource = dt;
                this.IPsValue.DisplayMember = "name";
                this.IPsValue.ValueMember = "id";
                this.IPsValue.EndUpdate();

                //IP变化单位
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("IPs"))
                    this.IPsValue.SelectedValue = this.senderEntity.args["IPs"];
            }

            this.chNumValue.Text = string.Empty;
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
                //分机号
                this.argsKey = "chNum";
                if (args.ContainsKey(argsKey))
                {
                    this.chNumValue.Text = args[argsKey].ToString();
                }
                //用户名
                this.argsKey = "agentName";
                if (args.ContainsKey(argsKey))
                {
                    this.agentNameValue.Text = args[argsKey].ToString();
                }
                //登陆名
                this.argsKey = "loginName";
                if (args.ContainsKey(argsKey))
                {
                    this.loginNameValue.Text = args[argsKey].ToString();
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
                this.delayTimer.Start();
                return;
            }
            this.senderEntity.args = new Dictionary<string, object>();
            this.SetArgsMark();
            //角色
            var role = Convert.ToInt32(this.roleValue.SelectedValue);
            if (role != -1)
            {
                this.senderEntity.args.Add("role", role);
            }
            //通道类型
            var chType = Convert.ToInt32(this.chTypeValue.SelectedValue);
            if (chType != -1)
            {
                this.senderEntity.args.Add("chType", chType);
            }
            //分机号
            var chNum = this.chNumValue.Text;
            if (!string.IsNullOrWhiteSpace(chNum))
            {
                this.senderEntity.args.Add("chNum", chNum);
            }
            //用户名
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
            //IP变化单位
            var IPs = Convert.ToInt32(this.IPsValue.SelectedValue);
            if (IPs != -1)
            {
                this.senderEntity.args.Add("IPs", IPs);
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

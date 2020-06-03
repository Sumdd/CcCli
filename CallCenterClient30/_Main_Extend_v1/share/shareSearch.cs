using Common;
using DataBaseUtil;
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
    public partial class shareSearch : _search
    {
        /// <summary>
        /// 统计表查询条件构造函数
        /// </summary>
        /// <param name="_senderEntity">统计表实体</param>
        public shareSearch(sharelist _senderEntity)
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
            this.delayTimer.Stop();
        }

        /// <summary>
        /// 加载查询条件名称,查询符号
        /// </summary>
        private void LoadQueryKey()
        {
            this.nameKey.thisDefult("域名称", this.nameKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.ipKey.thisDefult("域IP", this.ipKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.mainKey.thisDefult("是否为主域", this.mainKey.Name, "=", false);
            this.stateKey.thisDefult("状态", this.stateKey.Name, "=", false);
        }
        /// <summary>
        /// 加载查询参数默认值
        /// </summary>
        private void LoadQueryValue()
        {
            this.nameValue.Text = string.Empty;
            this.ipValue.Text = string.Empty;
            //是否为主域
            {
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow0 = m_pDataTable.NewRow();
                m_pDataRow0["ID"] = -1;
                m_pDataRow0["Name"] = "全部";
                m_pDataTable.Rows.Add(m_pDataRow0);
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = 0;
                m_pDataRow1["Name"] = "否";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 1;
                m_pDataRow2["Name"] = "是";
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = 2;
                m_pDataRow3["Name"] = "本机";
                m_pDataTable.Rows.Add(m_pDataRow3);
                this.mainValue.BeginUpdate();
                this.mainValue.DataSource = m_pDataTable;
                this.mainValue.ValueMember = "ID";
                this.mainValue.DisplayMember = "Name";
                this.mainValue.EndUpdate();

                //是否为主域
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("main"))
                    this.mainValue.SelectedValue = this.senderEntity.args["main"];
            }
            //状态
            {
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow0 = m_pDataTable.NewRow();
                m_pDataRow0["ID"] = -1;
                m_pDataRow0["Name"] = "全部";
                m_pDataTable.Rows.Add(m_pDataRow0);
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = 0;
                m_pDataRow1["Name"] = "未加入域";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 1;
                m_pDataRow2["Name"] = "加入域申请中...";
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = 2;
                m_pDataRow3["Name"] = "加入域成功";
                m_pDataTable.Rows.Add(m_pDataRow3);
                DataRow m_pDataRow4 = m_pDataTable.NewRow();
                m_pDataRow4["ID"] = 3;
                m_pDataRow4["Name"] = "取消加入域";
                m_pDataTable.Rows.Add(m_pDataRow4);
                DataRow m_pDataRow5 = m_pDataTable.NewRow();
                m_pDataRow5["ID"] = 4;
                m_pDataRow5["Name"] = "取消加入域...";
                m_pDataTable.Rows.Add(m_pDataRow5);
                this.stateValue.BeginUpdate();
                this.stateValue.DataSource = m_pDataTable;
                this.stateValue.ValueMember = "ID";
                this.stateValue.DisplayMember = "Name";
                this.stateValue.EndUpdate();

                //状态
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("state"))
                    this.stateValue.SelectedValue = this.senderEntity.args["state"];
            }
        }
        /// <summary>
        /// 从缓存中提取查询参数
        /// </summary>
        private void SetQueryValue()
        {
            if (this.senderEntity.args != null)
            {
                var args = this.senderEntity.args;
                //域名称
                this.argsKey = "name";
                if (args.ContainsKey(argsKey))
                {
                    this.nameValue.Text = args[argsKey].ToString();
                }
                //域IP
                this.argsKey = "ip";
                if (args.ContainsKey(argsKey))
                {
                    this.ipValue.Text = args[argsKey].ToString();
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
            //域名称
            var name = this.nameValue.Text;
            if (!string.IsNullOrWhiteSpace(name))
            {
                this.senderEntity.args.Add("name", name);
            }
            //域IP
            var ip = this.ipValue.Text;
            if (!string.IsNullOrWhiteSpace(ip))
            {
                this.senderEntity.args.Add("ip", ip);
            }
            //是否为主域
            var main = Convert.ToInt32(this.mainValue.SelectedValue);
            if (main != -1)
            {
                this.senderEntity.args.Add("main", main);
            }
            //状态
            var state = Convert.ToInt32(this.stateValue.SelectedValue);
            if (state != -1)
            {
                this.senderEntity.args.Add("state", state);
            }
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

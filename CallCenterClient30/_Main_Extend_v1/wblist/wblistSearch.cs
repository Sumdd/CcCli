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
    public partial class wblistSearch : _search
    {
        /// <summary>
        /// 统计表查询条件构造函数
        /// </summary>
        /// <param name="_senderEntity">统计表实体</param>
        public wblistSearch(wblist _senderEntity)
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
            this.wbnameKey.thisDefult("名称", this.wbnameKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.wbtypeKey.thisDefult("类型", this.wbtypeKey.Name, "=", false);
            this.wblimittypeKey.thisDefult("限制类型", this.wblimittypeKey.Name, "=", false);
            this.wbnumberKey.thisDefult("号码表达式", this.wbnumberKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.ordernumKey.thisDefult("唯一索引", this.ordernumKey.Name, "Like", true, ">", ">=", "<", "<=");
        }
        /// <summary>
        /// 加载查询参数默认值
        /// </summary>
        private void LoadQueryValue()
        {
            this.wbnameValue.Text = string.Empty;
            ///黑白名单类型
            {
                this.wbtypeValue.BeginUpdate();
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = 1;
                m_pDataRow1["Name"] = "白名单";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 2;
                m_pDataRow2["Name"] = "黑名单";
                m_pDataTable.Rows.Add(m_pDataRow2);
                this.wbtypeValue.DataSource = m_pDataTable;
                this.wbtypeValue.ValueMember = "ID";
                this.wbtypeValue.DisplayMember = "Name";
                this.wbtypeValue.EndUpdate();

                ///黑白名单类型
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("wbtype"))
                    this.wbtypeValue.SelectedValue = this.senderEntity.args["wbtype"];
                else
                    this.wbtypeValue.SelectedValue = 2;
            }
            ///黑白名单限制类型
            {
                this.wblimittypeValue.BeginUpdate();
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow4 = m_pDataTable.NewRow();
                m_pDataRow4["ID"] = 3;
                m_pDataRow4["Name"] = "呼入呼出";
                m_pDataTable.Rows.Add(m_pDataRow4);
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = 1;
                m_pDataRow1["Name"] = "呼入";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 2;
                m_pDataRow2["Name"] = "呼出";
                m_pDataTable.Rows.Add(m_pDataRow2);
                this.wblimittypeValue.DataSource = m_pDataTable;
                this.wblimittypeValue.ValueMember = "ID";
                this.wblimittypeValue.DisplayMember = "Name";
                this.wblimittypeValue.EndUpdate();

                ///黑白名单限制类型
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("wblimittype"))
                    this.wbtypeValue.SelectedValue = this.senderEntity.args["wblimittype"];
            }
            this.wbnumberValue.Text = string.Empty;
            this.ordernumValue.Text = string.Empty;
        }
        /// <summary>
        /// 从缓存中提取查询参数
        /// </summary>
        private void SetQueryValue()
        {
            if (this.senderEntity.args != null)
            {
                var args = this.senderEntity.args;
                //路由名称
                this.argsKey = "wbname";
                if (args.ContainsKey(argsKey))
                {
                    this.wbnameValue.Text = args[argsKey].ToString();
                }
                //号码表达式
                this.argsKey = "wbnumber";
                if (args.ContainsKey(argsKey))
                {
                    this.wbnumberValue.Text = args[argsKey].ToString();
                }
                //唯一索引
                this.argsKey = "ordernum";
                if (args.ContainsKey(argsKey))
                {
                    this.ordernumValue.Text = args[argsKey].ToString();
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
                //this.delayTimer.Start();
                return;
            }
            this.senderEntity.args = new Dictionary<string, object>();
            this.SetArgsMark();
            //路由名称
            var wbname = this.wbnameValue.Text;
            if (!string.IsNullOrWhiteSpace(wbname))
            {
                this.senderEntity.args.Add("wbname", wbname);
            }
            //类型
            var wbtype = Convert.ToInt32(this.wbtypeValue.SelectedValue);
            if (wbtype != -1)
            {
                this.senderEntity.args.Add("wbtype", wbtype);
            }
            //限制类型
            var wblimittype = Convert.ToInt32(this.wblimittypeValue.SelectedValue);
            if (wblimittype != -1)
            {
                this.senderEntity.args.Add("wblimittype", wblimittype);
            }
            //号码表达式
            var wbnumber = this.wbnumberValue.Text;
            if (!string.IsNullOrWhiteSpace(wbnumber))
            {
                this.senderEntity.args.Add("wbnumber", wbnumber);
            }
            //唯一索引
            var ordernum = this.ordernumValue.Text;
            if (!string.IsNullOrWhiteSpace(ordernum))
            {
                this.senderEntity.args.Add("ordernum", ordernum);
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

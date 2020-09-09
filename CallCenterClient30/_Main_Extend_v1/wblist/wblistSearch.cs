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
            this.rnameKey.thisDefult("路由名称", this.rnameKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.ctypeKey.thisDefult("路由方式", this.ctypeKey.Name, "=", false);
            this.rtypeKey.thisDefult("作用范围", this.rtypeKey.Name, "=", false);
            this.rtextKey.thisDefult("作用范围枚举", this.rtextKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.rnumberKey.thisDefult("号码表达式", this.rnumberKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.ordernumKey.thisDefult("唯一索引", this.ordernumKey.Name, "Like", true, ">", ">=", "<", "<=");
        }
        /// <summary>
        /// 加载查询参数默认值
        /// </summary>
        private void LoadQueryValue()
        {
            this.rnameValue.Text = string.Empty;
            ///路由方式
            {
                this.ctypeValue.BeginUpdate();
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow0 = m_pDataTable.NewRow();
                m_pDataRow0["ID"] = -1;
                m_pDataRow0["Name"] = "全部";
                m_pDataTable.Rows.Add(m_pDataRow0);
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = 1;
                m_pDataRow1["Name"] = "正序取闲";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 2;
                m_pDataRow2["Name"] = "倒序取闲";
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = 3;
                m_pDataRow3["Name"] = "随机取闲";
                m_pDataTable.Rows.Add(m_pDataRow3);
                this.ctypeValue.DataSource = m_pDataTable;
                this.ctypeValue.ValueMember = "ID";
                this.ctypeValue.DisplayMember = "Name";
                this.ctypeValue.EndUpdate();

                //路由方式
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("ctype"))
                    this.ctypeValue.SelectedValue = this.senderEntity.args["ctype"];
            }
            ///作用范围
            {
                this.rtypeValue.BeginUpdate();
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow0 = m_pDataTable.NewRow();
                m_pDataRow0["ID"] = -1;
                m_pDataRow0["Name"] = "全部";
                m_pDataTable.Rows.Add(m_pDataRow0);
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = 0;
                m_pDataRow1["Name"] = "无限制";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 1;
                m_pDataRow2["Name"] = "使用作用范围枚举";
                m_pDataTable.Rows.Add(m_pDataRow2);
                this.rtypeValue.DataSource = m_pDataTable;
                this.rtypeValue.ValueMember = "ID";
                this.rtypeValue.DisplayMember = "Name";
                this.rtypeValue.EndUpdate();

                //路由方式
                if (this.senderEntity.args != null && this.senderEntity.args.ContainsKey("rtype"))
                    this.ctypeValue.SelectedValue = this.senderEntity.args["rtype"];
            }
            this.rtextValue.Text = string.Empty;
            this.rnumberValue.Text = string.Empty;
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
                this.argsKey = "rname";
                if (args.ContainsKey(argsKey))
                {
                    this.rnameValue.Text = args[argsKey].ToString();
                }
                //作用范围枚举
                this.argsKey = "rtext";
                if (args.ContainsKey(argsKey))
                {
                    this.rtextValue.Text = args[argsKey].ToString();
                }
                //号码表达式
                this.argsKey = "rnumber";
                if (args.ContainsKey(argsKey))
                {
                    this.rnumberValue.Text = args[argsKey].ToString();
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
            var rname = this.rnameValue.Text;
            if (!string.IsNullOrWhiteSpace(rname))
            {
                this.senderEntity.args.Add("rname", rname);
            }
            //路由类型
            var ctype = Convert.ToInt32(this.ctypeValue.SelectedValue);
            if (ctype != -1)
            {
                this.senderEntity.args.Add("ctype", ctype);
            }
            //作用范围
            var rtype = Convert.ToInt32(this.rtypeValue.SelectedValue);
            if (rtype != -1)
            {
                this.senderEntity.args.Add("rtype", rtype);
            }
            //作用范围枚举
            var rtext = this.rtextValue.Text;
            if (!string.IsNullOrWhiteSpace(rtext))
            {
                this.senderEntity.args.Add("rtext", rtext);
            }
            //号码表达式
            var rnumber = this.rnumberValue.Text;
            if (!string.IsNullOrWhiteSpace(rnumber))
            {
                this.senderEntity.args.Add("rnumber", rnumber);
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

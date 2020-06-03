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
    public partial class gatewaySearch : _search
    {
        /// <summary>
        /// 统计表查询条件构造函数
        /// </summary>
        /// <param name="_senderEntity">统计表实体</param>
        public gatewaySearch(_index _senderEntity)
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
            this.gwtypeKey.thisDefult("网关类型", this.gwtypeKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.remarkKey.thisDefult("网关名称", this.remarkKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.gw_nameKey.thisDefult("网关IP及端口", this.gw_nameKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.gwstateKey.thisDefult("网关时态", this.gwstateKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.gwstatusKey.thisDefult("网关状态", this.gwstatusKey.Name, "Like", true, ">", ">=", "<", "<=");
        }
        /// <summary>
        /// 加载查询参数默认值
        /// </summary>
        private void LoadQueryValue()
        {
            this.gwtypeValue.Text = string.Empty;
            this.remarkValue.Text = string.Empty;
            this.gw_nameValue.Text = string.Empty;
            this.gwstateValue.Text = string.Empty;
            this.gwstatusValue.Text = string.Empty;
        }
        /// <summary>
        /// 从缓存中提取查询参数
        /// </summary>
        private void SetQueryValue()
        {
            if (this.senderEntity.args != null)
            {
                var args = this.senderEntity.args;
                //网关类型
                this.argsKey = "gwtype";
                if (args.ContainsKey(argsKey))
                {
                    this.gwtypeValue.Text = args[argsKey].ToString();
                }
                //网关名称
                this.argsKey = "remark";
                if (args.ContainsKey(argsKey))
                {
                    this.remarkValue.Text = args[argsKey].ToString();
                }
                //网关IP及端口
                this.argsKey = "gw_name";
                if (args.ContainsKey(argsKey))
                {
                    this.gw_nameValue.Text = args[argsKey].ToString();
                }
                //网关时态
                this.argsKey = "gwstate";
                if (args.ContainsKey(argsKey))
                {
                    this.gwstateValue.Text = args[argsKey].ToString();
                }
                //网关状态
                this.argsKey = "gwstatus";
                if (args.ContainsKey(argsKey))
                {
                    this.gwstatusValue.Text = args[argsKey].ToString();
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
            //网关类型
            var gwtype = this.gwtypeValue.Text;
            if (!string.IsNullOrWhiteSpace(gwtype))
            {
                this.senderEntity.args.Add("gwtype", gwtype);
            }
            //网关名称
            var remark = this.remarkValue.Text;
            if (!string.IsNullOrWhiteSpace(remark))
            {
                this.senderEntity.args.Add("remark", remark);
            }
            //网关IP及端口
            var gw_name = this.gw_nameValue.Text;
            if (!string.IsNullOrWhiteSpace(gw_name))
            {
                this.senderEntity.args.Add("gw_name", gw_name);
            }
            //网关时态
            var gwstate = this.gwstateValue.Text;
            if (!string.IsNullOrWhiteSpace(gwstate))
            {
                this.senderEntity.args.Add("gwstate", gwstate);
            }
            //网关状态
            var gwstatus = this.gwstatusValue.Text;
            if (!string.IsNullOrWhiteSpace(gwstatus))
            {
                this.senderEntity.args.Add("gwstatus", gwstatus);
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

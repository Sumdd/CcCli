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
    public partial class inruleSearch : _search
    {
        /// <summary>
        /// 统计表查询条件构造函数
        /// </summary>
        /// <param name="_senderEntity">统计表实体</param>
        public inruleSearch(inrule _senderEntity)
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
            this.inrulenameKey.thisDefult("内呼规则名称", this.inrulenameKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.inruleipKey.thisDefult("内呼规则IP", this.inruleipKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.inrulesuffixKey.thisDefult("内呼规则前缀", this.inrulesuffixKey.Name, "Like", true, ">", ">=", "<", "<=");
            this.ordernumKey.thisDefult("唯一索引", this.ordernumKey.Name, "Like", true, ">", ">=", "<", "<=");
        }
        /// <summary>
        /// 加载查询参数默认值
        /// </summary>
        private void LoadQueryValue()
        {
            this.inrulenameValue.Text = string.Empty;
            this.inruleipValue.Text = string.Empty;
            this.inrulesuffixValue.Text = string.Empty;
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
                //内呼规则名称
                this.argsKey = "inrulename";
                if (args.ContainsKey(argsKey))
                {
                    this.inrulenameValue.Text = args[argsKey].ToString();
                }
                //内呼规则IP
                this.argsKey = "inruleip";
                if (args.ContainsKey(argsKey))
                {
                    this.inruleipValue.Text = args[argsKey].ToString();
                }
                //内呼规则前缀
                this.argsKey = "inrulesuffix";
                if (args.ContainsKey(argsKey))
                {
                    this.inrulesuffixValue.Text = args[argsKey].ToString();
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
            //内呼规则名称
            var inrulename = this.inrulenameValue.Text;
            if (!string.IsNullOrWhiteSpace(inrulename))
            {
                this.senderEntity.args.Add("inrulename", inrulename);
            }
            //内呼规则IP
            var inruleip = this.inruleipValue.Text;
            if (!string.IsNullOrWhiteSpace(inruleip))
            {
                this.senderEntity.args.Add("inruleip", inruleip);
            }
            //内呼规则前缀
            var inrulesuffix = this.inrulesuffixValue.Text;
            if (!string.IsNullOrWhiteSpace(inrulesuffix))
            {
                this.senderEntity.args.Add("inrulesuffix", inrulesuffix);
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

using Cmn_v1;
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

namespace CenoCC
{
    public partial class userEdit : _form
    {
        private bool _using_ = false;

        public EventHandler SearchEvent;
        public user _entity;
        public userEdit()
        {
            InitializeComponent();

            this.HandleCreated += (a, b) =>
            {
                if (this._entity != null && this._entity.list.Items.Count > 0)
                {
                    ListViewItem m_pListViewItem;
                    if (this._entity.list.SelectedItems.Count > 0)
                        m_pListViewItem = this._entity.list.SelectedItems[0];
                    else
                        m_pListViewItem = this._entity.list.Items[0];
                    this.txtDomainName.Text = m_pListViewItem.SubItems["domainname"].Text;
                    this.txtSipServerIp.Text = m_pListViewItem.SubItems["sipserverip"].Text;
                    this.txtChPassword.Text = m_pListViewItem.SubItems["chpassword"].Text;
                    this.txtSipPort.Text = m_pListViewItem.SubItems["sipport"].Text;
                    this.txtRegTime.Text = m_pListViewItem.SubItems["regtime"].Text;
                }
            };

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

        private void btnUsing_Click(object sender, EventArgs e)
        {
            if (this._using_)
                return;
            Button btn = (Button)sender;
            if (btn.Name == "btnUsingSelect" && this._entity?.list?.SelectedItems?.Count <= 0)
            {
                MessageBox.Show("没有任何选中项");
                return;
            }
            if (
                string.IsNullOrWhiteSpace(this.txtDomainName.Text) ||
                string.IsNullOrWhiteSpace(this.txtSipServerIp.Text) ||
                string.IsNullOrWhiteSpace(this.txtChPassword.Text) ||
                string.IsNullOrWhiteSpace(this.txtSipPort.Text) ||
                string.IsNullOrWhiteSpace(this.txtRegTime.Text)
                )
            {
                MessageBox.Show("参数请填写完整");
                return;
            }
            string m_sMsgBodyStr = btn.Name == "btnUsingSelect" ? "确定执行选中生效吗?" : "确定执行全部生效吗?";
            if (!Cmn.MsgQ(m_sMsgBodyStr))
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                this._using_ = true;
                try
                {
                    var as_sql_append = string.Empty;
                    if (btn.Name == "btnUsingSelect" && this._entity != null && this._entity.list.SelectedItems.Count > 0)
                    {
                        var idlist = new List<string>();
                        foreach (ListViewItem item in this._entity.list.SelectedItems)
                        {
                            idlist.Add(item.SubItems["id"].Text);
                        }
                        as_sql_append = $"and id in ({string.Join(",", idlist.ToArray())})";
                    }
                    string m_sSQL = $@"
UPDATE `call_channel` 
SET `call_channel`.`DomainName` = '{this.txtDomainName.Text}',
`call_channel`.`SipServerIp` = '{this.txtSipServerIp.Text}',
`call_channel`.`ChPassword` = '{this.txtChPassword.Text}',
`call_channel`.`SipPort` = {this.txtSipPort.Text},
`call_channel`.`RegTime` = {this.txtRegTime.Text} 
WHERE
	1 =1
	{as_sql_append}
";
                    this._do_invoke(MySQL_Method.ExecuteNonQuery(m_sSQL), "用户SIP编辑");

                    if (this.SearchEvent != null)
                        this.SearchEvent(this, null);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"cmnset btnUsing_Click error:{ex.Message}");
                }
                finally
                {
                    this._using_ = false;
                }
            })).Start();
        }

        private void _do_invoke(int i, string t)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (i > 0)
                    MessageBox.Show($"{t}完成");
                else
                    MessageBox.Show($"{t}失败");
            }));
        }
    }
}

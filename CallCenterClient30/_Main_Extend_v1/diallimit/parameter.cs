using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataBaseUtil;
using Core_v1;
using Cmn_v1;

namespace CenoCC {
    public partial class parameter : Form {
        private bool _init_ = false;
        private bool _update_ = false;
        private bool _using_ =false;

        public EventHandler SearchEvent;
        public diallimit _entity;
        public parameter() {
            InitializeComponent();
            this.init();

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

        public void init() {
            if(this._init_)
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                this._init_ = true;
                try {
                    var as_sql = $@"select ifnull((select v from dial_parameter where k='limitcount' limit 1),0) as limitcount,
	                                       ifnull((select v from dial_parameter where k='limitduration' limit 1),0) as limitduration,
	                                       ifnull((select v from dial_parameter where k='limitthecount' limit 1),0) as limitthecount,
	                                       ifnull((select v from dial_parameter where k='limittheduration' limit 1),3600) as limittheduration,
	                                       ifnull((select v from dial_parameter where k='limitthedial' limit 1),5) as limitthedial,
	                                       ifnull((select v from dial_parameter where k='inlimit_2starttime' limit 1),'00:00:00') as inlimit_2starttime,
	                                       ifnull((select v from dial_parameter where k='inlimit_2endtime' limit 1),'00:00:00') as inlimit_2endtime,
	                                       ifnull((select v from dial_parameter where k='inlimit_2whatday' limit 1),127) as inlimit_2whatday
;";
                    var dt = MySQL_Method.BindTable(as_sql);
                    if(dt.Rows.Count > 0) {
                        this.limitcountValue.Value = Convert.ToDecimal(dt.Rows[0]["limitcount"]);
                        this.limitdurationValue.Value = Convert.ToDecimal(dt.Rows[0]["limitduration"]);
                        this.limitthecountValue.Value = Convert.ToDecimal(dt.Rows[0]["limitthecount"]);
                        this.limitthedurationValue.Value = Convert.ToDecimal(dt.Rows[0]["limittheduration"]);
                        this.limitthedialValue.Value = Convert.ToDecimal(dt.Rows[0]["limitthedial"]);
                        this.inlimit_2starttime.Text = dt.Rows[0]["inlimit_2starttime"]?.ToString();
                        this.inlimit_2endtime.Text = dt.Rows[0]["inlimit_2endtime"]?.ToString();

                        ///设定星期几
                        int m_uWhatDay = Convert.ToInt32(dt.Rows[0]["inlimit_2whatday"]);
                        for (int i = 0; i < this.inlimit_2whatday.Items.Count; i++)
                        {
                            this.inlimit_2whatday.SetItemChecked(i, (m_uWhatDay & (int)(Math.Pow(2, i))) > 0 ? true : false);
                        }

                    } else {
                        this.limitcountValue.Value = 0;
                        this.limitdurationValue.Value = 0;
                        this.limitthecountValue.Value = 0;
                        this.limitthedurationValue.Value = 3600;
                        this.limitthedialValue.Value = 5;

                        this.inlimit_2starttime.Text = "00:00:00";
                        this.inlimit_2endtime.Text = "00:00:00";

                        ///默认勾选所有星期
                        for (int i = 0; i < this.inlimit_2whatday.Items.Count; i++)
                        {
                            this.inlimit_2whatday.SetItemChecked(i, true);
                        }
                    }
                } catch(Exception ex) {
                    Log.Instance.Error($"parameter init error:{ex.Message}");
                } finally {
                    this._init_ = false;
                }
            })).Start();
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if(this._update_)
                return;
            if (!Cmn.MsgQ("确定修改默认配置吗?"))
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                this._update_ = true;

                ///得到选中的星期的代数和
                int m_uWhatDay = 0;
                for (int i = 0; i < this.inlimit_2whatday.Items.Count; i++)
                {
                    if (this.inlimit_2whatday.GetItemChecked(i))
                    {
                        m_uWhatDay += (int)(Math.Pow(2, i));
                    }
                }

                try {
                    var as_sql = ""
                        + $"update dial_parameter set v={this.limitcountValue.Value} where k='limitcount';\r\n"
                        + $"update dial_parameter set v={this.limitdurationValue.Value} where k='limitduration';\r\n"
                        + $"update dial_parameter set v={this.limitthecountValue.Value} where k='limitthecount';\r\n"
                        + $"update dial_parameter set v={this.limitthedurationValue.Value} where k='limittheduration';\r\n"
                        + $"update dial_parameter set v={this.limitthedialValue.Value} where k='limitthedial';\r\n"
                        + $"update dial_parameter set v='{this.inlimit_2starttime.Text}' where k='inlimit_2starttime';\r\n"
                        + $"update dial_parameter set v='{this.inlimit_2endtime.Text}' where k='inlimit_2endtime';\r\n"
                        + $"update dial_parameter set v={m_uWhatDay} where k='inlimit_2whatday';\r\n"
                        ;
                    this._do_invoke(MySQL_Method.ExecuteNonQuery(as_sql), "修改");
                } catch(Exception ex) {
                    Log.Instance.Error($"parameter btnOK_Click error:{ex.Message}");
                } finally {
                    this._update_ = false;
                }
            })).Start();
        }

        private void btnReset_Click(object sender, EventArgs e) {
            this.init();
        }

        private void btnUsing_Click(object sender, EventArgs e) {
            if(this._using_)
                return;
            Button btn = (Button)sender;
            string m_sMsgBodyStr = btn.Name == "btnUsingSelect" ? "确定执行选中生效吗?" : "确定执行全部生效吗?";
            if (!Cmn.MsgQ(m_sMsgBodyStr))
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                this._using_ = true;
                try {
                    var as_sql_append = string.Empty;
                    if(btn.Name == "btnUsingSelect" && this._entity != null && this._entity.list.SelectedItems.Count > 0) {
                        var idlist = new List<string>();
                        foreach(ListViewItem item in this._entity.list.SelectedItems) {
                            idlist.Add(item.SubItems["id"].Text);
                        }
                        as_sql_append = $"and id in ({string.Join(",", idlist.ToArray())})";
                    }

                    ///根据查询条件查询出所有ID
                    if (string.IsNullOrWhiteSpace(as_sql_append))
                    {
                        as_sql_append = this._entity.m_fQueryList();
                    }

                    var as_sql = ""
                        //+ $"update dial_parameter set v={this.limitcountValue.Value} where k='limitcount';\r\n"
                        //+ $"update dial_parameter set v={this.limitdurationValue.Value} where k='limitduration';\r\n"
                        //+ $"update dial_parameter set v={this.limitthecountValue.Value} where k='limitthecount';\r\n"
                        //+ $"update dial_parameter set v={this.limitthedurationValue.Value} where k='limittheduration';\r\n"
                        //+ $"update dial_parameter set v={this.limitthedialValue.Value} where k='limitthedial';\r\n"
                        + $"update dial_limit set\r\n"
                        + $"limitthedial={this.limitthedialValue.Value},\r\n"
                        + $"limitcount={this.limitcountValue.Value},\r\n"
                        + $"limitduration={this.limitdurationValue.Value},\r\n"
                        + $"limitthecount={this.limitthecountValue.Value},\r\n"
                        + $"limittheduration={this.limitthedurationValue.Value}\r\n"
                        + $"where isdel=0\r\n"
                        + $"{as_sql_append}";
                    this._do_invoke(MySQL_Method.ExecuteNonQuery(as_sql), "设置立即生效");
                    if(this.SearchEvent != null)
                        this.SearchEvent(this, null);
                } catch(Exception ex) {
                    Log.Instance.Error($"parameter btnUsing_Click error:{ex.Message}");
                } finally {
                    this._using_ = false;
                }
            })).Start();
        }

        private void _do_invoke(int i, string t) {
            this.BeginInvoke(new MethodInvoker(() => {
                if(i > 0)
                    MessageBox.Show($"通话限制参数{t}完成");
                else
                    MessageBox.Show($"通话限制参数{t}失败");
            }));
        }

        private void btnUsingSelect_Click(object sender, EventArgs e) {
            if(this._entity != null && this._entity.list.SelectedItems.Count > 0) {
                this.btnUsing_Click(sender, e);
            } else {
                MessageBox.Show("没有任何选中项");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Core_v1;
using Common;
using DataBaseUtil;
using Model_v1;

namespace CenoCC {
    public partial class diallimitCreate : Form {
        private bool _create_ = false;
        public EventHandler SearchEvent;
        public diallimitCreate()  {
            InitializeComponent();

            ///权限项:添加共享号码
            if (m_cPower.Has(m_mOperate.diallimit_number_add_share))
            {
                this.cbxShare.Enabled = true;
            }
            else
            {
                this.cbxShare.Enabled = false;
            }

            this.HandleCreated += (a, b) =>
            {
                #region ***号码类别
                {
                    DataTable m_pDataTable = new DataTable();
                    m_pDataTable.Columns.Add("ID", typeof(int));
                    m_pDataTable.Columns.Add("Name", typeof(string));
                    DataRow m_pDataRow1 = m_pDataTable.NewRow();
                    m_pDataRow1["ID"] = 0;
                    m_pDataRow1["Name"] = "专线号码";
                    m_pDataTable.Rows.Add(m_pDataRow1);
                    DataRow m_pDataRow2 = m_pDataTable.NewRow();
                    m_pDataRow2["ID"] = 1;
                    m_pDataRow2["Name"] = "共享号码";
                    m_pDataTable.Rows.Add(m_pDataRow2);
                    this.cbxShare.BeginUpdate();
                    this.cbxShare.DataSource = m_pDataTable;
                    this.cbxShare.ValueMember = "ID";
                    this.cbxShare.DisplayMember = "Name";
                    this.cbxShare.EndUpdate();
                }
                #endregion

                #region ***网关
                {
                    new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                    {
                        try
                        {

                            PopedomArgs popedomArgs = new PopedomArgs();
                            popedomArgs.type = DataPowerType._data_gateway_allot;
                            popedomArgs.left.Add("`call_gateway`.`adduser`");
                            string m_sPopedomSQL = m_cPower.m_fPopedomSQL(popedomArgs);

                            var dt = m_cGateway.m_fGatewayList(null, m_sPopedomSQL);
                            var dr = dt.NewRow();
                            dr["uid"] = "";
                            dr["rgw"] = "请选择网关";
                            dt.Rows.InsertAt(dr, 0);
                            this.BeginInvoke(new MethodInvoker(() => {
                                this.cbxGateway.BeginUpdate();
                                this.cbxGateway.DataSource = dt;
                                this.cbxGateway.DisplayMember = "rgw";
                                this.cbxGateway.ValueMember = "uid";
                                this.cbxGateway.EndUpdate();
                            }));
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"diallimit fill gateway:{ex.Message}");
                        }

                    })).Start();
                }
                #endregion
            };
        }

        private void btnOk_Click(object sender, EventArgs e) {
            if(this._create_)
                return;
            try {
                Regex _regex_ = new Regex("[0-9]{0,}");
                if(
                    !string.IsNullOrEmpty(this.startNumberValue.Text.Trim()) &&
                    _regex_.IsMatch(this.startNumberValue.Text) &&
                    _regex_.IsMatch(this.endNumberValue.Text)
                    ) {
                    if(string.IsNullOrEmpty(this.endNumberValue.Text.Trim()))
                        this.endNumberValue.Text = this.startNumberValue.Text;

                    double _s_ = Convert.ToDouble(this.startNumberValue.Text);
                    double _e_ = Convert.ToDouble(this.endNumberValue.Text);
                    if(_e_ >= _s_) {
                        this._create_ = true;
                        /// <![CDATA[
                        /// 添加号码,所有不存在与数据库中的数据均要添加
                        /// 这里可以给一个判断
                        /// ]]>
                        new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                            try {
                                DataTable _dt_ = new DataTable();
                                _dt_.Columns.Add("number", typeof(string));
                                for(double i = _s_; i <= _e_; i++) {
                                    DataRow _dr_ = _dt_.NewRow();
                                    _dr_["number"] = $"{this.txtPrefix.Text.Trim()}{i}";
                                    _dt_.Rows.Add(_dr_);
                                }
                                int j = d_multi.iu(_dt_, AgentInfo.AgentID, "-1", Convert.ToInt32(this.cbxShare.SelectedValue), this.cbxGateway.SelectedValue.ToString());
                                if(j > 0) { 
                                    if(this.SearchEvent != null)
                                        this.SearchEvent(this, null);
                                }
                                string msg = $"号码{this.startNumberValue.Text}-{this.endNumberValue.Text}添加完成,成功{j}条";
                                Log.Instance.Success($"diallimitCreate btnOk_Click:{msg}");
                                MessageBox.Show($"{msg}");
                            } catch(Exception ex) {
                                Log.Instance.Error($"diallimitCreate btnOk_Click thread error:{ex.Message}");
                            } finally {
                                this._create_ = false;
                            }
                        })).Start();
                    } else {
                        MessageBox.Show("结束号码需大于开始号码");
                    }
                } else {
                    MessageBox.Show("号码段必须为数字");
                }
            } catch(Exception ex) {
                Log.Instance.Error($"diallimitCreate btnOk_Click error:{ex.Message}");
                this._create_ = false;
            }
        }
    }
}

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

        private bool m_bLoad = true;

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
                    ///增加一个类别,支持呼叫内转
                    DataRow m_pDataRow_2 = m_pDataTable.NewRow();
                    m_pDataRow_2["ID"] = -2;
                    m_pDataRow_2["Name"] = "呼叫内转号码";
                    m_pDataTable.Rows.Add(m_pDataRow_2);
                    DataRow m_pDataRow1 = m_pDataTable.NewRow();
                    m_pDataRow1["ID"] = 0;
                    m_pDataRow1["Name"] = "专线号码";
                    m_pDataTable.Rows.Add(m_pDataRow1);
                    DataRow m_pDataRow2 = m_pDataTable.NewRow();
                    m_pDataRow2["ID"] = 1;
                    m_pDataRow2["Name"] = "共享号码";
                    m_pDataTable.Rows.Add(m_pDataRow2);
                    DataRow m_pDataRow3 = m_pDataTable.NewRow();
                    m_pDataRow3["ID"] = 2;
                    m_pDataRow3["Name"] = "申请式";
                    m_pDataTable.Rows.Add(m_pDataRow3);
                    this.cbxShare.BeginUpdate();
                    this.cbxShare.DataSource = m_pDataTable;
                    this.cbxShare.ValueMember = "ID";
                    this.cbxShare.DisplayMember = "Name";
                    this.cbxShare.EndUpdate();

                    ///设定选中项
                    this.cbxShare.SelectedValue = 0;
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

                this.m_bLoad = false;
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

        private void btnOk_Click(object sender, EventArgs e) {
            if(this._create_)
                return;
            try {
                Regex _regex_ = new Regex("[0-9]{0,}");
                if(
                    !string.IsNullOrWhiteSpace(this.startNumberValue.Text) &&
                    _regex_.IsMatch(this.startNumberValue.Text) &&
                    _regex_.IsMatch(this.endNumberValue.Text)
                    ) {

                    ///前导变量
                    string m_sPrefix = string.Empty;

                    string endNumberValue = string.Empty;
                    if (string.IsNullOrWhiteSpace(this.endNumberValue.Text))
                        endNumberValue = this.startNumberValue.Text;
                    else
                        endNumberValue = this.endNumberValue.Text;

                    ///处理前缀0问题,防止出错
                    int m_uSld = this.startNumberValue.Text.TrimStart('0').Length;
                    int m_uSly = this.startNumberValue.Text.Length;
                    int m_uSlx = m_uSly - m_uSld;
                    int m_uEld = endNumberValue.TrimStart('0').Length;
                    int m_uEly = endNumberValue.Length;
                    int m_uElx = m_uEly - m_uEld;
                    ///判断有没有0前缀,有的话加入即可
                    if (m_uSlx == m_uElx)
                    {
                        m_sPrefix = m_f0(m_uSlx);
                    }
                    else
                    {
                        Cmn_v1.Cmn.MsgWranThat(this, "开始号码起与结束号码止格式不对应");
                        return;
                    }

                    double _s_ = Convert.ToDouble(this.startNumberValue.Text);
                    double _e_ = Convert.ToDouble(endNumberValue);
                    if(_e_ >= _s_) {

                        ///如果为重复确定
                        bool m_bSame = ((Button)sender).Name == "btnOkSame";
                        if (m_bSame)
                        {
                            if (!Cmn_v1.Cmn.MsgQ("该操作会越过判重直接添加号码，请再次确认号码是否必须重复添加？")) return;
                        }

                        this._create_ = true;
                        int m_uShare = Convert.ToInt32(this.cbxShare.SelectedValue);
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
                                    if (m_uShare == -2)
                                    {
                                        _dr_["number"] = $"";
                                    }
                                    else
                                    {
                                        _dr_["number"] = $"{m_sPrefix}{i}";
                                    }
                                    _dt_.Rows.Add(_dr_);
                                }
                                int j = d_multi.iu(_dt_, AgentInfo.AgentID, "-1", Convert.ToInt32(this.cbxShare.SelectedValue), this.cbxGateway.SelectedValue.ToString(), m_bSame);
                                if(j > 0) { 
                                    if(this.SearchEvent != null)
                                        this.SearchEvent(this, null);
                                }
                                string msg = $"号码{this.startNumberValue.Text}-{endNumberValue}添加完成,去重后成功添加{j}条";
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

        private string m_f0(int m_uInt)
        {
            if (m_uInt > 0)
            {
                string m_sResult = "";
                for (int i = 0; i < m_uInt; i++)
                {
                    m_sResult += "0";
                }
                return m_sResult;
            }
            return "";
        }

        private void cbxShare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_bLoad) return;

            if (this.cbxShare.SelectedValue?.ToString() == "-2")
            {
                //this.startNumberValue.Enabled = false;
                //this.endNumberValue.Enabled = false;
                this.btnOk.Enabled = false;
            }
            else
            {
                //this.startNumberValue.Enabled = true;
                //this.endNumberValue.Enabled = true;
                this.btnOk.Enabled = true;
            }
        }
    }
}

using Core_v1;
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
    public partial class shareset : Form
    {
        private bool _ok_ = false;
        private int m_uID = -1;

        public EventHandler SearchEvent;
        public sharelist _entity;
        public shareset()
        {
            InitializeComponent();

            //下拉加载
            {
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(string));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = "0";
                m_pDataRow1["Name"] = "否";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = "1";
                m_pDataRow2["Name"] = "是";
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = "2";
                m_pDataRow3["Name"] = "本机";
                m_pDataTable.Rows.Add(m_pDataRow3);
                this.cbxIsMain.BeginUpdate();
                this.cbxIsMain.DataSource = m_pDataTable;
                this.cbxIsMain.ValueMember = "ID";
                this.cbxIsMain.DisplayMember = "Name";
                this.cbxIsMain.EndUpdate();
            }

            this.HandleCreated += (a, b) =>
            {
                int? m_uCount = this?._entity?.list?.SelectedItems?.Count;
                if (m_uCount == 0)
                {
                    this.Text = "新增域";
                    this.btnOk.Text = "新增";
                }
                else if (m_uCount >= 1)
                {
                    this.Text = "编辑域";
                    this.btnOk.Text = "编辑";
                    //加载
                    var entity = this._entity.list.SelectedItems[0];
                    this.m_uID = Convert.ToInt32(entity.SubItems["id"].Text);
                    this.txtName.Text = entity.SubItems["aname"].Text;
                    this.txtIP.Text = entity.SubItems["aip"].Text;
                    this.txtPort.Text = entity.SubItems["aport"].Text;
                    this.txtDb.Text = entity.SubItems["adb"].Text;
                    this.txtUid.Text = entity.SubItems["auid"].Text;
                    this.txtPwd.Text = Common.Encrypt.DecryptString(entity.SubItems["apwd"].Text);
                    this.cbxIsMain.SelectedValue = entity.SubItems["amain"].Text;
                }
            };
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this._ok_)
                return;

            int m_uPort = 3306;
            bool m_bPort = int.TryParse(this.txtPort.Text, out m_uPort);
            if (!m_bPort)
            {
                MessageBox.Show("域端口填写有误");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                MessageBox.Show("域名称非空");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtIP.Text))
            {
                MessageBox.Show("域IP非空");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtDb.Text))
            {
                MessageBox.Show("域数据库非空");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtUid.Text))
            {
                MessageBox.Show("域用户名非空");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtUid.Text))
            {
                MessageBox.Show("域密码非空");
                return;
            }

            int status = 0;
            string msg = "设置域失败";
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    this._ok_ = true;
                    DataTable m_pDataTable = DataBaseUtil.m_cEsyMySQL.m_fSetDialArea(m_uID, this.txtName.Text, this.txtIP.Text, m_uPort, this.txtDb.Text, this.txtUid.Text, Common.Encrypt.EncryptString(this.txtPwd.Text), Convert.ToInt32(this.cbxIsMain.SelectedValue));
                    if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
                    {
                        status = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                        msg = m_pDataTable.Rows[0]["msg"].ToString();
                        if (status == 1)
                            Cmn_v1.Cmn.MsgOK(msg);
                        else
                            Cmn_v1.Cmn.MsgWran(msg);
                    }
                    else
                    {
                        Cmn_v1.Cmn.MsgWran(msg);
                    }
                    Log.Instance.Success($"[CenoCC][shareset][btnOk_Click][Thread][{msg}]");

                    if (this.SearchEvent != null)
                        this.SearchEvent(sender, e);
                }
                catch (Exception ex)
                {
                    status = 0;
                    msg = $"域操作失败:{ex.Message}";
                    Cmn_v1.Cmn.MsgError(msg);
                    Log.Instance.Error($"[CenoCC][shareset][btnOk_Click][Thread][Exception][{ex.Message}]");
                }
                finally
                {
                    this._ok_ = false;
                }
            })).Start();
        }
    }
}

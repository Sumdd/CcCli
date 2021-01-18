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
    public partial class wblistOp : Form
    {
        private bool m_bLoad = true;
        private bool m_bDoing = false;
        private int m_uID = -1;
        public EventHandler SearchEvent;

        public wblistOp(int _m_uID)
        {
            InitializeComponent();

            m_uID = _m_uID;

            if (m_uID == -1)
            {
                this.Text = "黑白名单添加";
                this.btnSave.Text = "添加";
            }
            else
            {
                this.Text = "黑白名单编辑";
                this.btnSave.Text = "编辑";
            }

            ///黑白名单方式
            {
                this.cbxWbtype.BeginUpdate();
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
                this.cbxWbtype.DataSource = m_pDataTable;
                this.cbxWbtype.ValueMember = "ID";
                this.cbxWbtype.DisplayMember = "Name";
                this.cbxWbtype.EndUpdate();

                ///添加时默认值
                if (m_uID == -1)
                {
                    this.cbxWbtype.SelectedValue = 2;
                }

                ///支持白名单的添加
                ///this.cbxWbtype.Enabled = false;
            }

            ///限制类型
            {
                this.cbxWblimittype.BeginUpdate();
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
                this.cbxWblimittype.DataSource = m_pDataTable;
                this.cbxWblimittype.ValueMember = "ID";
                this.cbxWblimittype.DisplayMember = "Name";
                this.cbxWblimittype.EndUpdate();

                ///添加时默认值
                if (m_uID == -1)
                {
                    this.cbxWblimittype.SelectedValue = 3;
                }
            }

            ///只把组织架构加载上可勾选即可
            this.HandleCreated += (a, b) =>
            {
                if (m_uID != -1)
                {
                    new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                    {

                        ///查询出所有的信息,并赋值

                        string m_sSQL = $@"
SELECT
	* 
FROM
	`call_wblist` 
WHERE
	`wbid` = {m_uID};
";
                        DataSet ds = DataBaseUtil.MySQL_Method.ExecuteDataSet(m_sSQL);
                        if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow m_pDataRow = ds.Tables[0].Rows[0];
                            this.txtWbname.Text = m_pDataRow["wbname"].ToString();
                            this.txtWbnumber.Text = m_pDataRow["wbnumber"].ToString();
                            this.txtOrdernum.Text = m_pDataRow["ordernum"].ToString();
                            this.cbxWbtype.SelectedValue = m_pDataRow["wbtype"].ToString();
                            this.cbxWblimittype.SelectedValue = m_pDataRow["wblimittype"].ToString();
                        }

                    })).Start();
                }
            };

            this.m_bLoad = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.m_bDoing)
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "有任务正在执行,请稍后");
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.txtWbname.Text))
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "黑白名单名称非空");
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.txtWbnumber.Text))
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "号码表达式非空");
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.txtOrdernum.Text))
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "唯一索引非空且不能重复");
                    return;
                }

                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    try
                    {
                        this.m_bDoing = true;
                        int m_uCtype = Convert.ToInt32(this.cbxWbtype.SelectedValue);
                        List<string> m_lSQL = new List<string>();

                        string m_sNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        ///直接删除ID,再添加即可
                        string m_sSQL = $@"
DELETE 
FROM
	`call_wblist` 
WHERE
	`wbid` = {m_uID};
SELECT
	(
	CASE
			
			WHEN {m_uID} = - 1 THEN
			( IFNULL( ( SELECT MAX( `wbid` ) FROM `call_wblist` ), 0 ) + 1 ) ELSE {m_uID} 
	END 
	) INTO @m_uMaxID;
INSERT INTO `call_wblist` ( `wbid`, `wbname`, `wbnumber`, `wbtype`, `addtime`, `adduser`, `ordernum`, `wblimittype` )
VALUES
	( @m_uMaxID, '{this.txtWbname.Text}', '{this.txtWbnumber.Text}', {this.cbxWbtype.SelectedValue}, '{m_sNow}', {Common.AgentInfo.AgentID}, {this.txtOrdernum.Text}, {this.cbxWblimittype.SelectedValue} );
{(string.Join("\r\n", m_lSQL))}
";
                        int m_uCount = DataBaseUtil.MySQL_Method.ExecuteNonQuery(m_sSQL, true);
                        MessageBox.Show(this, $"黑白名单{(m_uID == -1 ? "添加" : "编辑")}成功");
                        this.Close();
                        if (this.SearchEvent != null)
                            this.SearchEvent(null, null);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][wblistOp][btnSave_Click][Exception][{ex.Message}]");
                        Cmn_v1.Cmn.MsgWranThat(this, $"黑白名单{(m_uID == -1 ? "添加" : "编辑")}错误:{ex.Message}");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][wblistOp][btnSave_Click][Exception][{ex.Message}]");
                Cmn_v1.Cmn.MsgWranThat(this, $"黑白名单{(m_uID == -1 ? "添加" : "编辑")}错误:{ex.Message}");
                this.m_bDoing = false;
            }
        }
    }
}

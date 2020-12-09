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
    public partial class inruleOp : Form
    {
        private bool m_bLoad = true;
        private bool m_bDoing = false;
        private int m_uID = -1;
        public EventHandler SearchEvent;

        public inruleOp(int _m_uID)
        {
            InitializeComponent();

            m_uID = _m_uID;

            if (m_uID == -1)
            {
                this.Text = "内呼规则添加";
                this.btnSave.Text = "添加";
            }
            else
            {
                this.Text = "内呼规则编辑";
                this.btnSave.Text = "编辑";
            }

            if (m_uID == -1)
            {
                ///默认端口
                this.txtInruleport.Text = "5080";
                ///默认ua
                this.txtInruleua.Text = "external";
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
	`call_inrule` 
WHERE
	`inruleid` = {m_uID};
";
                        DataSet ds = DataBaseUtil.MySQL_Method.ExecuteDataSet(m_sSQL);
                        if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow m_pDataRow = ds.Tables[0].Rows[0];
                            this.txtInruleName.Text = m_pDataRow["inrulename"].ToString();
                            this.txtInruleIP.Text = m_pDataRow["inruleip"].ToString();
                            this.txtInruleport.Text = m_pDataRow["inruleport"].ToString();
                            this.txtInruleua.Text = m_pDataRow["inruleua"].ToString();
                            this.txtInruleSuffix.Text = m_pDataRow["inrulesuffix"].ToString();
                            this.txtOrdernum.Text = m_pDataRow["ordernum"].ToString();
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

                if (string.IsNullOrWhiteSpace(this.txtInruleName.Text))
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "内呼规则名称非空");
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.txtInruleIP.Text))
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "内呼规则IP非空");
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.txtInruleSuffix.Text))
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "内呼规则前缀非空");
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
                        List<string> m_lSQL = new List<string>();

                        string m_sNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        ///直接删除ID,再添加即可
                        string m_sSQL = $@"
DELETE 
FROM
	`call_inrule` 
WHERE
	`inruleid` = {m_uID};
SELECT
	(
	CASE
			
			WHEN {m_uID} = - 1 THEN
			( IFNULL( ( SELECT MAX( `inruleid` ) FROM `call_inrule` ), 0 ) + 1 ) ELSE {m_uID} 
	END 
	) INTO @m_uMaxID;
INSERT INTO `call_inrule` ( `inruleid`, `inrulename`, `inruleip`, `inrulesuffix`, `addtime`, `adduser`, `ordernum`, `inruleport`, `inruleua` )
VALUES
	( @m_uMaxID, '{this.txtInruleName.Text}', '{this.txtInruleIP.Text}', '{this.txtInruleSuffix.Text}', '{m_sNow}', {Common.AgentInfo.AgentID}, {this.txtOrdernum.Text}, {this.txtInruleport.Text}, '{this.txtInruleua.Text}' );
{(string.Join("\r\n", m_lSQL))}
";
                        int m_uCount = DataBaseUtil.MySQL_Method.ExecuteNonQuery(m_sSQL, true);
                        MessageBox.Show(this, $"内呼规则{(m_uID == -1 ? "添加" : "编辑")}成功");
                        this.Close();
                        if (this.SearchEvent != null)
                            this.SearchEvent(null, null);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][inruleOp][btnSave_Click][Exception][{ex.Message}]");
                        Cmn_v1.Cmn.MsgWranThat(this, $"内呼规则{(m_uID == -1 ? "添加" : "编辑")}错误:{ex.Message}");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][inruleOp][btnSave_Click][Exception][{ex.Message}]");
                Cmn_v1.Cmn.MsgWranThat(this, $"黑白名单{(m_uID == -1 ? "添加" : "编辑")}错误:{ex.Message}");
                this.m_bDoing = false;
            }
        }
    }
}

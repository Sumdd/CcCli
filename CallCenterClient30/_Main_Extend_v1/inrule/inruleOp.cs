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

            ///是否为本机规则
            {
                this.cbxInrulemain.BeginUpdate();
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = 0;
                m_pDataRow1["Name"] = "否";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 1;
                m_pDataRow2["Name"] = "是";
                m_pDataTable.Rows.Add(m_pDataRow2);
                this.cbxInrulemain.DataSource = m_pDataTable;
                this.cbxInrulemain.ValueMember = "ID";
                this.cbxInrulemain.DisplayMember = "Name";
                this.cbxInrulemain.EndUpdate();

                ///添加时默认值
                if (m_uID == -1)
                {
                    this.cbxInrulemain.SelectedValue = 0;
                }
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
                        try
                        {
                            ///查询出所有的信息,并赋值

                            string m_sSQL = $@"
SELECT
	* 
FROM
	`call_inrule` 
WHERE
	`inruleid` = {m_uID};
SELECT
	* 
FROM
	`call_inrulebook` 
WHERE
	`call_inrulebook`.`inruleid` = {m_uID} 
ORDER BY
	`call_inrulebook`.`inrulebookordernum` ASC;
";
                            DataSet ds = DataBaseUtil.MySQL_Method.ExecuteDataSet(m_sSQL);
                            if (ds != null && ds.Tables.Count == 2)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    DataRow m_pDataRow = ds.Tables[0].Rows[0];
                                    this.txtInruleName.Text = m_pDataRow["inrulename"].ToString();
                                    this.txtInruleIP.Text = m_pDataRow["inruleip"].ToString();
                                    this.txtInruleport.Text = m_pDataRow["inruleport"].ToString();
                                    this.txtInruleua.Text = m_pDataRow["inruleua"].ToString();
                                    this.txtInruleSuffix.Text = m_pDataRow["inrulesuffix"].ToString();
                                    this.cbxInrulemain.SelectedValue = m_pDataRow["inrulemain"].ToString();
                                    this.txtOrdernum.Text = m_pDataRow["ordernum"].ToString();
                                }
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    ///拼接显示
                                    this.txtInrulebook.Text = string.Join("\r\n", ds.Tables[1].AsEnumerable().Select(x => $"{x.Field<object>("inrulebookordernum")} {x.Field<string>("inrulebookname")} {x.Field<string>("inrulebookfkey")} {x.Field<string>("inrulebooktkey")}"));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][inruleOp][inruleOp][Exception][{ex.Message}]");
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

                List<Model_v1.m_mBook> m_lBookTable = new List<Model_v1.m_mBook>();
                ///验证电话薄规则
                if (!string.IsNullOrWhiteSpace(this.txtInrulebook.Text))
                {
                    string m_sBooks = this.txtInrulebook.Text;
                    string[] m_lBooks = m_sBooks.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                    int m_uRow = 0;
                    foreach (string item in m_lBooks)
                    {
                        m_uRow++;
                        string[] m_lPages = item.Split(' ');
                        if (m_lPages.Length == 4)
                        {
                            Model_v1.m_mBook _m_mBook = new Model_v1.m_mBook();
                            _m_mBook.inrulebookordernum = float.Parse(m_lPages[0]);
                            _m_mBook.inrulebookname = m_lPages[1];
                            _m_mBook.inrulebookfkey = m_lPages[2];
                            _m_mBook.inrulebooktkey = m_lPages[3];
                            m_lBookTable.Add(_m_mBook);
                        }
                        else throw new Exception($"行:{m_uRow};数据:{item},空格分割后列数为{m_lPages.Length},需4列");
                    }
                }

                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    try
                    {
                        this.m_bDoing = true;

                        ///增加
                        List<string> m_lSQL = new List<string>();
                        if (m_lBookTable.Count > 0)
                        {
                            foreach (Model_v1.m_mBook item in m_lBookTable)
                            {
                                string m_sInsertSQL = $@"
INSERT INTO `call_inrulebook` ( `inruleid`, `inrulebookname`, `inrulebookfkey`, `inrulebooktkey`, `inrulebookordernum` )
VALUES
	( {m_uID}, '{item.inrulebookname}', '{item.inrulebookfkey}', '{item.inrulebooktkey}', {item.inrulebookordernum} );
";
                                m_lSQL.Add(m_sInsertSQL);
                            }
                        }

                        string m_sNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        ///直接删除ID,再添加即可
                        string m_sSQL = $@"
DELETE 
FROM
	`call_inrule` 
WHERE
	`inruleid` = {m_uID};
DELETE 
FROM
	`call_inrulebook` 
WHERE
	`call_inrulebook`.`inruleid` = {m_uID};
SELECT
	(
	CASE
			
			WHEN {m_uID} = - 1 THEN
			( IFNULL( ( SELECT MAX( `inruleid` ) FROM `call_inrule` ), 0 ) + 1 ) ELSE {m_uID} 
	END 
	) INTO @m_uMaxID;
INSERT INTO `call_inrule` ( `inruleid`, `inrulename`, `inruleip`, `inrulesuffix`, `addtime`, `adduser`, `ordernum`, `inruleport`, `inruleua`, `inrulemain` )
VALUES
	( @m_uMaxID, '{this.txtInruleName.Text}', '{this.txtInruleIP.Text}', '{this.txtInruleSuffix.Text}', '{m_sNow}', {Common.AgentInfo.AgentID}, {this.txtOrdernum.Text}, {this.txtInruleport.Text}, '{this.txtInruleua.Text}', {this.cbxInrulemain.SelectedValue} );
{(string.Join("\r\n", m_lSQL))}
";
                        int m_uCount = DataBaseUtil.MySQL_Method.ExecuteNonQuery(m_sSQL, true);
                        MessageBox.Show(this, $"内呼规则{(m_uID == -1 ? "添加" : "编辑")}成功");

                        ///不先关闭
                        if (m_uID == -1)
                        {
                            this.txtInruleName.Text = string.Empty;
                            this.txtInruleIP.Text = string.Empty;
                            this.txtInruleSuffix.Text = string.Empty;
                            this.cbxInrulemain.SelectedValue = 0;
                            this.txtOrdernum.Text = string.Empty;
                        }
                        else
                        {
                            this.Close();
                        }

                        if (this.SearchEvent != null)
                            this.SearchEvent(null, null);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][inruleOp][btnSave_Click][try][Exception][{ex.Message}]");
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
                Cmn_v1.Cmn.MsgWranThat(this, $"内呼规则{(m_uID == -1 ? "添加" : "编辑")}错误:{ex.Message}");
                this.m_bDoing = false;
            }
        }
    }
}

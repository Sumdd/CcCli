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
    public partial class inruleOpText : Form
    {
        private bool m_bLoad = true;
        private bool m_bDoing = false;
        private int m_uID = -1;
        public EventHandler SearchEvent;

        public inruleOpText(int _m_uID)
        {
            InitializeComponent();

            ///直接加载文本格式
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    string m_sSQL = $@"
SELECT
	`call_inrule`.`inruleid`,
	`call_inrule`.`ordernum`,
	`call_inrule`.`inrulename`,
	`call_inrule`.`inruleip`,
	`call_inrule`.`inruleport`,
	`call_inrule`.`inruleua`,
	`call_inrule`.`inrulesuffix`,
	( CASE WHEN `call_inrule`.`inrulemain` = 1 THEN '*' ELSE '0' END ) AS `inrulemain` 
FROM
	`call_inrule` 
ORDER BY
	`call_inrule`.`ordernum` DESC;
SELECT
	`call_inrulebook`.`inruleid`,
	`call_inrulebook`.`inrulebookordernum`,
	`call_inrulebook`.`inrulebookname`,
	`call_inrulebook`.`inrulebookfkey`,
	`call_inrulebook`.`inrulebooktkey` 
FROM
	`call_inrulebook` 
WHERE
	`call_inrulebook`.`inruleid` 
ORDER BY
	`call_inrulebook`.`inrulebookordernum` DESC;
";
                    DataSet m_pDataSet = DataBaseUtil.MySQL_Method.ExecuteDataSet(m_sSQL);
                    if (m_pDataSet != null && m_pDataSet.Tables.Count == 2)
                    {
                        if (m_pDataSet.Tables[0].Rows.Count > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (DataRow item1 in m_pDataSet.Tables[0].Rows)
                            {
                                ///内呼规则
                                sb.AppendLine($"{item1["inrulemain"]} {item1["ordernum"]} {item1["inrulename"]} {item1["inruleip"]} {item1["inruleport"]} {item1["inruleua"]} {item1["inrulesuffix"]}");
                                DataRow[] m_lItem2 = m_pDataSet.Tables[1]?.Select($" [inruleid] = {item1["inruleid"]} ");
                                if (m_lItem2?.Count() > 0)
                                {
                                    ///内呼规则快捷电话簿
                                    foreach (DataRow item2 in m_lItem2)
                                    {
                                        sb.AppendLine($"{item2["inrulebookordernum"]} {item2["inrulebookname"]} {item2["inrulebookfkey"]} {item2["inrulebooktkey"]}");
                                    }
                                }
                            }
                            m_fInvokeSetText($"{sb.ToString()}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    m_fInvokeSetText($"-Err {ex.Message}");
                }
                finally
                {
                    this.m_bLoad = false;
                }
            })).Start();
        }

        private void m_fInvokeSetText(string m_sMsg)
        {
            if (this.txtText.InvokeRequired) this.BeginInvoke(new MethodInvoker(() => this.m_fInvokeSetText(m_sMsg)));
            else this.txtText.Text = m_sMsg;
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

                string m_sBooks = this.txtText.Text;

                ///添加列表
                List<Model_v1.m_mInrule> m_lInruleList = new List<Model_v1.m_mInrule>();

                ///验证电话薄规则
                if (!string.IsNullOrWhiteSpace(m_sBooks))
                {
                    string[] m_lInrules = m_sBooks.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    ///设置索引
                    int m_uIndex = -1;
                    int m_uRow = 0;
                    foreach (string item in m_lInrules)
                    {
                        m_uRow++;
                        string[] m_lPages = item.Split(' ');
                        if (m_lPages.Length == 7)
                        {
                            ///缓存
                            Model_v1.m_mInrule _m_mInrule = new Model_v1.m_mInrule();
                            _m_mInrule.m_lBook = new List<Model_v1.m_mBook>();
                            ///赋值
                            _m_mInrule.inruleid = m_uRow;
                            _m_mInrule.inrulemain = m_lPages[0] == "*" ? 1 : 0;
                            _m_mInrule.ordernum = float.Parse(m_lPages[1]);
                            _m_mInrule.inrulename = m_lPages[2];
                            _m_mInrule.inruleip = m_lPages[3];
                            _m_mInrule.inruleport = int.Parse(m_lPages[4]);
                            _m_mInrule.inruleua = m_lPages[5];
                            _m_mInrule.inrulesuffix = m_lPages[6];
                            m_lInruleList.Add(_m_mInrule);
                            m_uIndex++;
                        }
                        else if (m_lPages.Length == 4)
                        {
                            if (m_uIndex == -1) throw new Exception($"行:{m_uRow};数据:{item},需定义在内呼规则下");
                            Model_v1.m_mBook _m_mBook = new Model_v1.m_mBook();
                            _m_mBook.inrulebookordernum = float.Parse(m_lPages[0]);

                            ///索引不可重复
                            if (m_lInruleList[m_uIndex].m_lBook.Where(x => x.inrulebookordernum == _m_mBook.inrulebookordernum)?.Count() > 0) throw new Exception($"行:{m_uRow};数据:{item},索引{_m_mBook.inrulebookordernum}已存在");

                            _m_mBook.inrulebookname = m_lPages[1];
                            _m_mBook.inrulebookfkey = m_lPages[2];
                            _m_mBook.inrulebooktkey = m_lPages[3];
                            m_lInruleList[m_uIndex].m_lBook.Add(_m_mBook);
                        }
                        else throw new Exception($"行:{m_uRow};数据:{item},空格分割后列数为{m_lPages.Length},仅7列活4列");
                    }

                    ///校验是否有且仅有一个本地内呼规则
                    if (m_lInruleList.Where(x => x.inrulemain == 1)?.Count() != 1) throw new Exception("必须有且仅有一个本机规则");
                }

                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    try
                    {
                        this.m_bDoing = true;

                        string m_sNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        ///增加
                        List<string> m_lSQL = new List<string>();
                        if (m_lInruleList.Count > 0)
                        {
                            for (int i = 0; i < m_lInruleList.Count; i++)
                            {
                                Model_v1.m_mInrule item1 = m_lInruleList[i];
                                string m_sInsertSQL = $@"
INSERT INTO `call_inrule` ( `inrulename`, `inruleip`, `inrulesuffix`, `addtime`, `adduser`, `ordernum`, `inruleport`, `inruleua`, `inrulemain` )
VALUES
	( '{item1.inrulename}', '{item1.inruleip}', '{item1.inrulesuffix}', '{m_sNow}', {Common.AgentInfo.AgentID}, {item1.ordernum}, {item1.inruleport}, '{item1.inruleua}', {item1.inrulemain} );
SET @m_sID{i} = LAST_INSERT_ID( );
";
                                m_lSQL.Add(m_sInsertSQL);
                                if (item1.m_lBook != null && item1.m_lBook.Count > 0)
                                {
                                    foreach (Model_v1.m_mBook item2 in item1.m_lBook)
                                    {
                                        string m_sBookSQL = $@"
INSERT INTO `call_inrulebook` ( `inruleid`, `inrulebookname`, `inrulebookfkey`, `inrulebooktkey`, `inrulebookordernum` )
VALUES
	( @m_sID{i}, '{item2.inrulebookname}', '{item2.inrulebookfkey}', '{item2.inrulebooktkey}', {item2.inrulebookordernum} );
";
                                        m_lSQL.Add(m_sBookSQL);
                                    }
                                }
                            }
                        }

                        ///直接删除ID,再添加即可
                        string m_sSQL = $@"
TRUNCATE TABLE `call_inrule`;
TRUNCATE TABLE `call_inrulebook`;
{(string.Join("\r\n", m_lSQL))}
";
                        int m_uCount = DataBaseUtil.MySQL_Method.ExecuteNonQuery(m_sSQL, true);
                        MessageBox.Show(this, $"内呼规则添加成功");
                        this.Close();
                        if (this.SearchEvent != null)
                            this.SearchEvent(null, null);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][inruleOpText][btnSave_Click][Exception][{ex.Message}]");
                        Cmn_v1.Cmn.MsgWranThat(this, $"内呼规则添加错误:{ex.Message}");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][inruleOpText][btnSave_Click][Exception][{ex.Message}]");
                Cmn_v1.Cmn.MsgWranThat(this, $"内呼规则添加错误:{ex.Message}");
                this.m_bDoing = false;
            }
        }
    }
}

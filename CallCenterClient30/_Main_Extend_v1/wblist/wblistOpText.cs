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
    public partial class wblistOpText : Form
    {
        private bool m_bLoad = true;
        private bool m_bDoing = false;
        private int m_uID = -1;
        public EventHandler SearchEvent;

        public wblistOpText(int _m_uID)
        {
            InitializeComponent();

            ///操作权限
            this.m_fLoadOperatePower(this.Controls);

            ///直接加载文本格式
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    string m_sSQL = $@"
SELECT
	`call_wblist`.`ordernum`, 
	`call_wblist`.`wbname`,
	`call_wblist`.`wbnumber`,
	`call_wblist`.`wblimittype`
FROM
	`call_wblist` 
ORDER BY
	`call_wblist`.`ordernum` DESC;
";
                    DataTable m_pDataTable = DataBaseUtil.MySQL_Method.BindTable(m_sSQL);
                    if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
                    {
                        this.txtText.Text = string.Join("\r\n", m_pDataTable.AsEnumerable().Select(x => $"{x.Field<object>("ordernum")} {x.Field<object>("wbname")} {x.Field<object>("wbnumber")} {x.Field<object>("wblimittype")}"));
                    }
                }
                catch (Exception ex)
                {
                    this.txtText.Text = $"-Err {ex.Message}";
                }
                finally
                {
                    this.m_bLoad = false;
                }
            })).Start();
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

                List<m_mWblist> m_lWblistTable = new List<m_mWblist>();

                ///验证电话薄规则
                if (!string.IsNullOrWhiteSpace(m_sBooks))
                {
                    string[] m_lBooks = m_sBooks.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                    int m_uRow = 0;
                    foreach (string item in m_lBooks)
                    {
                        m_uRow++;
                        string[] m_lPages = item.Split(' ');
                        if (m_lPages.Length == 4)
                        {
                            m_mWblist _m_mWblist = new m_mWblist();
                            _m_mWblist.ordernum = float.Parse(m_lPages[0]);
                            _m_mWblist.wbname = m_lPages[1];
                            _m_mWblist.wbnumber = m_lPages[2];
                            _m_mWblist.wblimittype = int.Parse(m_lPages[3]);
                            if (_m_mWblist.wblimittype > 3 || _m_mWblist.wblimittype < 1) throw new Exception($"行:{m_uRow};数据:{item},第4列值仅可为1呼入2呼出3呼入呼出");
                            m_lWblistTable.Add(_m_mWblist);
                        }
                        else throw new Exception($"行:{m_uRow};数据:{item},空格分割后列数为{m_lPages.Length},需4列");
                    }
                }

                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    try
                    {
                        this.m_bDoing = true;

                        string m_sNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        ///增加
                        List<string> m_lSQL = new List<string>();
                        if (m_lWblistTable.Count > 0)
                        {
                            foreach (m_mWblist item in m_lWblistTable)
                            {
                                string m_sInsertSQL = $@"
INSERT INTO `call_wblist` ( `wbname`, `wbnumber`, `wbtype`, `addtime`, `adduser`, `ordernum`, `wblimittype` )
VALUES
	( '{item.wbname}', '{item.wbnumber}', 2, '{m_sNow}', {Common.AgentInfo.AgentID}, {item.ordernum}, {item.wblimittype} );
";
                                m_lSQL.Add(m_sInsertSQL);
                            }
                        }

                        ///直接删除ID,再添加即可
                        string m_sSQL = $@"
DELETE 
FROM
	`call_wblist`;
{(string.Join("\r\n", m_lSQL))}
";
                        int m_uCount = DataBaseUtil.MySQL_Method.ExecuteNonQuery(m_sSQL, true);
                        MessageBox.Show(this, $"黑白名单添加成功");
                        this.Close();
                        if (this.SearchEvent != null)
                            this.SearchEvent(null, null);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][wblistOpText][btnSave_Click][Exception][{ex.Message}]");
                        Cmn_v1.Cmn.MsgWranThat(this, $"黑白名单添加错误:{ex.Message}");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][wblistOpText][btnSave_Click][Exception][{ex.Message}]");
                Cmn_v1.Cmn.MsgWranThat(this, $"黑白名单添加错误:{ex.Message}");
                this.m_bDoing = false;
            }
        }
    }

    public class m_mWblist
    {
        public float ordernum { get; set; }
        public string wbname { get; set; }
        public string wbnumber { get; set; }
        public int wblimittype { get; set; }
    }
}

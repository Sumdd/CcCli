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

namespace CenoCC
{
    public partial class multiAssign : Form
    {
        private bool m_bDoing = false;

        public EventHandler SearchEvent;
        public diallimit _entity;
        public multiAssign()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
                return;

            try
            {
                //所有号码量
                List<KeyValuePair<string, string>> m_lList = new List<KeyValuePair<string, string>>();
                foreach (ListViewItem item in _entity.list.SelectedItems)
                {
                    m_lList.Add(new KeyValuePair<string, string>(item.SubItems["id"].Text, item.SubItems["number"].ToString()));
                }
                if (m_lList.Count <= 0)
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "请选则要分配的号码(多选:Ctrl+左键或Shift+左键)");
                    return;
                }
                //登录名起
                string m_sStartLoginValue = this.startLoginValue.Text?.Trim();
                //登录名止
                string m_sEndLoginValue = this.endLoginValue.Text?.Trim();
                if (string.IsNullOrWhiteSpace(m_sEndLoginValue))
                {
                    this.endLoginValue.Text = m_sStartLoginValue;
                    m_sEndLoginValue = m_sStartLoginValue;
                }
                //处理得到前缀
                Regex english = new Regex("[^(a-zA-Z)]+");
                Regex number = new Regex("[^(0-9)]+");
                //登录名起前缀
                string m_sStartLoginPrefix = english.Replace(m_sStartLoginValue, "");
                //登录名起后缀
                int m_uStartLoginSuffix = Convert.ToInt32(number.Replace(m_sStartLoginValue, ""));
                //登录名止前缀
                string m_sEndLoginPrefix = english.Replace(m_sEndLoginValue, "");
                //登录名止后缀
                int m_uEndLoginSuffix = Convert.ToInt32(number.Replace(m_sEndLoginValue, ""));
                //判断前缀
                if (m_sStartLoginPrefix != m_sEndLoginPrefix)
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "登录名前缀不一致");
                    return;
                }
                //判断后缀
                if (m_uStartLoginSuffix > m_uEndLoginSuffix)
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "登录名止小于登录名起");
                    return;
                }
                //循环
                List<string> m_lUa = new List<string>();
                for (int i = m_uStartLoginSuffix; i <= m_uEndLoginSuffix; i++)
                {
                    m_lUa.Add($"{m_sStartLoginPrefix}{i}");
                }
                //数量是否一致
                if (m_lUa.Count != m_lList.Count)
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "号码数量和账户数量不一致");
                    return;
                }
                //开始执行操作
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    try
                    {
                        this.m_bDoing = true;

                        //查询数据库真实的账户数量
                        string m_sSQL1 = $@"
SELECT
	`call_agent`.`ID` 
FROM
	`call_agent` 
WHERE
	`call_agent`.`LoginName` IN ( '{string.Join("','", m_lUa)}' ) 
ORDER BY
	`call_agent`.`LoginName`;
";

                        DataTable m_pDt1 = MySQL_Method.BindTable(m_sSQL1);

                        if (m_pDt1 == null || (m_pDt1 != null && m_pDt1.Rows.Count != m_lUa.Count))
                        {
                            Cmn_v1.Cmn.MsgWranThat(this, "未找到对应的账户数量");
                            return;
                        }
                        //排序号码,升序分配即可
                        List<KeyValuePair<string, string>> _m_lList = m_lList.OrderBy(x => x.Value).ToList();
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < _m_lList.Count; i++)
                        {
                            KeyValuePair<string, string> item = _m_lList[i];
                            sb.AppendLine($@"
UPDATE `dial_limit` 
SET `dial_limit`.`useuser` = {m_pDt1.Rows[i]["ID"]} 
WHERE
	`dial_limit`.`ID` = {item.Key} ;
");
                        }

                        int m_uCount = MySQL_Method.ExecuteNonQuery(sb.ToString());
                        //提示即可
                        MessageBox.Show(this, $"批量分配号码完成{m_uCount}条,请核实数据");
                        //刷新数据即可
                        if (this.SearchEvent != null)
                        {
                            this.SearchEvent(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][multiAssign][btnOk_Click][Thread][Exception][{ex.Message}]");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }

                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][multiAssign][btnOk_Click][Exception][{ex.Message}]");
                Log.Instance.Debug(ex);
                Cmn_v1.Cmn.MsgWranThat(this, ex.Message);
                this.m_bDoing = false;
            }
        }
    }
}

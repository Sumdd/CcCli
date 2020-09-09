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

        //private string _m_sRname = string.Empty;
        //private string _m_sRnumber = string.Empty;
        //private string _m_sOrdernum = string.Empty;
        //private string _m_sCtype = string.Empty;
        //private string _m_sRtype = string.Empty;
        //private DataTable _m_pDataTable = null;

        public wblistOp(int _m_uID)
        {
            InitializeComponent();

            m_uID = _m_uID;

            if (m_uID == -1)
            {
                this.Text = "路由添加";
                this.btnSave.Text = "添加";
            }
            else
            {
                this.Text = "路由编辑";
                this.btnSave.Text = "编辑";
            }

            ///路由方式
            {
                this.cbxCtype.BeginUpdate();
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = 1;
                m_pDataRow1["Name"] = "正序取闲";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 2;
                m_pDataRow2["Name"] = "倒序取闲";
                m_pDataTable.Rows.Add(m_pDataRow2);
                DataRow m_pDataRow3 = m_pDataTable.NewRow();
                m_pDataRow3["ID"] = 3;
                m_pDataRow3["Name"] = "随机取闲";
                m_pDataTable.Rows.Add(m_pDataRow3);
                this.cbxCtype.DataSource = m_pDataTable;
                this.cbxCtype.ValueMember = "ID";
                this.cbxCtype.DisplayMember = "Name";
                this.cbxCtype.EndUpdate();
                this.cbxCtype.SelectedValue = 3;
            }
            ///作用范围
            {
                this.cbxRtype.BeginUpdate();
                DataTable m_pDataTable = new DataTable();
                m_pDataTable.Columns.Add("ID", typeof(int));
                m_pDataTable.Columns.Add("Name", typeof(string));
                DataRow m_pDataRow1 = m_pDataTable.NewRow();
                m_pDataRow1["ID"] = 0;
                m_pDataRow1["Name"] = "无限制";
                m_pDataTable.Rows.Add(m_pDataRow1);
                DataRow m_pDataRow2 = m_pDataTable.NewRow();
                m_pDataRow2["ID"] = 1;
                m_pDataRow2["Name"] = "使用作用范围枚举";
                m_pDataTable.Rows.Add(m_pDataRow2);
                this.cbxRtype.DataSource = m_pDataTable;
                this.cbxRtype.ValueMember = "ID";
                this.cbxRtype.DisplayMember = "Name";
                this.cbxRtype.EndUpdate();
            }
            ///只把组织架构加载上可勾选即可
            this.HandleCreated += (a, b) =>
            {
                if (m_uID == -1)
                {
                    this.m_fFill();
                }
                else
                {
                    new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                    {

                        ///查询出所有的信息,并赋值

                        string m_sSQL = $@"
SELECT
	* 
FROM
	`call_route` 
WHERE
	ID = {m_uID};
SELECT
	`call_routeua`.`muuid` AS `ID`,
	`call_routeua`.`mtype` AS `t` 
FROM
	`call_routeua` 
WHERE
	rid = {m_uID};
";
                        DataSet ds = DataBaseUtil.MySQL_Method.ExecuteDataSet(m_sSQL);
                        if (ds != null && ds.Tables.Count == 2 && ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow m_pDataRow = ds.Tables[0].Rows[0];
                            this.txtRname.Text = m_pDataRow["rname"].ToString();
                            this.txtRnumber.Text = m_pDataRow["rnumber"].ToString();
                            this.txtOrdernum.Text = m_pDataRow["ordernum"].ToString();
                            this.cbxCtype.SelectedValue = m_pDataRow["ctype"].ToString();
                            this.cbxRtype.SelectedValue = m_pDataRow["rtype"].ToString();

                            this.m_fFill(new MethodInvoker(() =>
                            {
                                this.m_fDoChecked(this.treeView.Nodes, ds.Tables[1]);
                            }));

                            ///编辑
                            if (m_pDataRow["rtype"].ToString() == "1") this.treeView.Enabled = true;
                            else this.treeView.Enabled = false;
                        }

                    })).Start();
                }
            };

            ///加载
            this.treeView.Enabled = false;
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

                if (string.IsNullOrWhiteSpace(this.txtRname.Text))
                {
                    Cmn_v1.Cmn.MsgWranThat(this, "路由名称非空");
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.txtRnumber.Text))
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
                        int m_uCtype = Convert.ToInt32(this.cbxCtype.SelectedValue);
                        int m_uRtype = Convert.ToInt32(this.cbxRtype.SelectedValue);
                        List<string> m_lSQL = new List<string>();

                        if (m_uRtype == 1)
                        {
                            List<Model_v1.m_mTree> m_lTree = new List<Model_v1.m_mTree>();
                            this.m_fFillCheckedList(m_lTree, this.treeView.Nodes);
                            if (m_lTree.Count <= 0)
                            {
                                Cmn_v1.Cmn.MsgWranThat(this, "请勾选作用范围");
                                return;
                            }

                            foreach (Model_v1.m_mTree item in m_lTree)
                            {
                                string _m_sSQL = $@"
INSERT INTO `call_routeua` ( `rid`, `mtype`, `muuid` )
VALUES
	( @m_uMaxID, '{item.t}', {item.ID} );
";
                                m_lSQL.Add(_m_sSQL);
                            }
                        }

                        string m_sNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        ///直接删除ID,再添加即可
                        string m_sSQL = $@"
DELETE 
FROM
	`call_route` 
WHERE
	ID = {m_uID};
DELETE 
FROM
	`call_routeua` 
WHERE
	rid = {m_uID};
SELECT
	(
	CASE
			
			WHEN {m_uID} = - 1 THEN
			( IFNULL( ( SELECT MAX( ID ) FROM `call_route` ), 0 ) + 1 ) ELSE {m_uID} 
	END 
	) INTO @m_uMaxID;
INSERT INTO `call_route` ( `ID`, `rname`, `rnumber`, `ctype`, `rtype`, `addtime`, `adduser`, `ordernum` )
VALUES
	( @m_uMaxID, '{this.txtRname.Text}', '{this.txtRnumber.Text}', {this.cbxCtype.SelectedValue}, {this.cbxRtype.SelectedValue}, '{m_sNow}', {Common.AgentInfo.AgentID}, {this.txtOrdernum.Text} );
{(string.Join("\r\n", m_lSQL))}
";
                        int m_uCount = DataBaseUtil.MySQL_Method.ExecuteNonQuery(m_sSQL, true);
                        MessageBox.Show(this, $"路由{(m_uID == -1 ? "添加" : "编辑")}成功");
                        this.Close();
                        if (this.SearchEvent != null)
                            this.SearchEvent(null, null);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][routeOp][btnSave_Click][Exception][{ex.Message}]");
                        Cmn_v1.Cmn.MsgWranThat(this, $"路由{(m_uID == -1 ? "添加" : "编辑")}错误:{ex.Message}");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][routeOp][btnSave_Click][Exception][{ex.Message}]");
                Cmn_v1.Cmn.MsgWranThat(this, $"路由{(m_uID == -1 ? "添加" : "编辑")}错误:{ex.Message}");
                this.m_bDoing = false;
            }
        }

        private void m_fFill(MethodInvoker m_pMethodInvoker = null)
        {
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            ///填充部门树及账户,支持无限极
                            this.treeView.Nodes.Clear();
                            DataTable m_pAccount = d_multi.m_fTreeAccount();
                            if (m_pAccount != null && m_pAccount.Rows.Count > 0)
                                this.m_fFillTreeAccount(m_pAccount, this.treeView);
                            this.treeView.ExpandAll();
                            ///执行委托
                            if (m_pMethodInvoker != null)
                                m_pMethodInvoker();
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][routeOp][m_fFill][Thread][Exception][{ex.Message}]");
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][routeOp][m_fFill][Exception][{ex.Message}]");
            }
        }

        private void m_fFillTreeAccount(DataTable m_pDataTable, TreeView m_pTreeView, TreeNode m_pTreeNode = null, string ID = "-1")
        {
            try
            {
                DataRow[] m_lDataRow;

                if (string.IsNullOrWhiteSpace(ID) || ID == "-1")
                    m_lDataRow = m_pDataTable.Select($" ISNULL([fID],(-1)) = -1 AND [t] = 'T' ");
                else
                    m_lDataRow = m_pDataTable.Select($" [fID] = '{ID}' ");

                foreach (var m_pDataRow in m_lDataRow)
                {
                    TreeNode _m_pTreeNode = new TreeNode();
                    _m_pTreeNode = new TreeNode();
                    _m_pTreeNode.Text = $"{m_pDataRow["n"]}";
                    string m_uID = m_pDataRow["ID"]?.ToString();
                    string m_sT = m_pDataRow["t"]?.ToString();
                    _m_pTreeNode.Tag = new Model_v1.m_mTree()
                    {
                        ID = m_uID,
                        fID = m_pDataRow["fID"]?.ToString(),
                        n = _m_pTreeNode.Text,
                        t = m_sT
                    };

                    if (m_pTreeNode == null)
                        m_pTreeView.Nodes.Add(_m_pTreeNode);
                    else
                        m_pTreeNode.Nodes.Add(_m_pTreeNode);

                    if (m_sT == "T")
                    {
                        this.m_fFillTreeAccount(m_pDataTable, m_pTreeView, _m_pTreeNode, m_uID);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][routeOp][m_fFillTreeAccount][Exception][{ex.Message}]");
            }
        }

        private void m_fFillCheckedList(List<Model_v1.m_mTree> m_lTree, TreeNodeCollection m_lTreeNode, bool? m_bDoChecked = null, Model_v1.m_mTree _m_pTree = null)
        {
            foreach (TreeNode item in m_lTreeNode)
            {
                Model_v1.m_mTree m_pTree = (Model_v1.m_mTree)item.Tag;
                if (item.Checked)
                {
                    if (m_lTree != null)
                        m_lTree.Add(m_pTree);
                }
                if (m_bDoChecked != null)
                {
                    item.Checked = Convert.ToBoolean(m_bDoChecked);
                }
                if (_m_pTree != null)
                {
                    if (m_pTree.ID == _m_pTree.ID && m_pTree.t == _m_pTree.t)
                    {
                        item.Checked = true;
                        break;
                    }
                }
                if (item?.Nodes?.Count > 0)
                {
                    this.m_fFillCheckedList(m_lTree, item?.Nodes, m_bDoChecked, _m_pTree);
                }
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Bounds.Contains(e.Location))
            {
                if (e.Node.Checked)
                {
                    e.Node.Checked = false;
                    this.m_fFillCheckedList(null, e.Node.Nodes, false);
                }
                else
                {
                    e.Node.Checked = true;
                    this.m_fFillCheckedList(null, e.Node.Nodes, true);
                }
            }
        }


        private void m_fDoChecked(TreeNodeCollection m_lTreeNode, DataTable m_pDataTable)
        {
            if (m_lTreeNode != null && m_lTreeNode.Count > 0 && m_pDataTable != null && m_pDataTable.Rows.Count > 0)
            {
                foreach (TreeNode m_pTreeNode in m_lTreeNode)
                {
                    foreach (DataRow m_pDataRow in m_pDataTable.Rows)
                    {
                        Model_v1.m_mTree m_pTree = (Model_v1.m_mTree)m_pTreeNode.Tag;
                        if (m_pTree.ID == m_pDataRow["ID"]?.ToString() && m_pTree.t == m_pDataRow["t"]?.ToString())
                        {
                            m_pTreeNode.Checked = true;
                            break;
                        }
                        else if (m_pTreeNode.Checked)
                        {
                            m_pTreeNode.Checked = false;
                        }
                    }
                    if (m_pTreeNode.Nodes?.Count > 0)
                    {
                        this.m_fDoChecked(m_pTreeNode.Nodes, m_pDataTable);
                    }
                }
            }
            else
            {
                foreach (TreeNode m_pTreeNode in m_lTreeNode)
                {
                    if (m_pTreeNode.Checked)
                    {
                        m_pTreeNode.Checked = false;
                    }
                    if (m_pTreeNode.Nodes?.Count > 0)
                    {
                        this.m_fDoChecked(m_pTreeNode.Nodes, null);
                    }
                }
            }
        }

        private void cbxRtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bLoad) return;
            if (this.cbxRtype.SelectedValue.ToString() == "1") this.treeView.Enabled = true;
            else this.treeView.Enabled = false;
        }
    }
}

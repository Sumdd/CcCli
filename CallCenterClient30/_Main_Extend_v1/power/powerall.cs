using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cmn_v1;
using Core_v1;

namespace CenoCC
{
    public partial class powerall : _form
    {
        private bool m_bDoing = false;
        private bool m_bDataTabLoad = false;
        private bool m_bBaseControlTabLoad = false;
        public powerall(bool uc = false)
        {
            InitializeComponent();
            if (uc)
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }

            ///操作权限
            this.m_fLoadOperatePower(this.Controls);

            this.HandleCreated += (a, b) =>
            {
                this.m_fFill();
            };
        }

        #region ***操作权限
        private void m_fLoadOperatePower(Control.ControlCollection m_lControls)
        {
            foreach (var item in m_lControls)
            {
                var m_pGetType = item.GetType();
                if (m_pGetType == typeof(Button) || m_pGetType == typeof(CheckBox))
                {
                    Control m_pButton = (Control)item;
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
                else if (item.GetType() == typeof(TabControl))
                {
                    TabControl m_pTabControl = (TabControl)item;
                    this.m_fLoadOperatePower(m_pTabControl.Controls);
                }
                else if (item.GetType() == typeof(GroupBox))
                {
                    GroupBox m_pGroupBox = (GroupBox)item;
                    this.m_fLoadOperatePower(m_pGroupBox.Controls);
                }
                else if (item.GetType() == typeof(TabPage))
                {
                    TabPage m_pTabPage = (TabPage)item;
                    this.m_fLoadOperatePower(m_pTabPage.Controls);
                }
                else if (item.GetType() == typeof(TableLayoutPanel))
                {
                    TableLayoutPanel m_pTableLayoutPanel = (TableLayoutPanel)item;
                    this.m_fLoadOperatePower(m_pTableLayoutPanel.Controls);
                }
            }
            if (Common.AgentInfo.AgentID == "1000")
            {
                this.btnSave.Enabled = true;
            }
        }
        #endregion

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
                            this.treeAccount.Nodes.Clear();
                            DataTable m_pAccount = d_multi.m_fTreeAccount();
                            if (m_pAccount != null && m_pAccount.Rows.Count > 0)
                                this.m_fFillTreeAccount(m_pAccount, this.treeAccount);
                            this.treeAccount.ExpandAll();
                            ///填充角色
                            this.treeRole.Nodes.Clear();
                            DataTable m_pRole = d_multi.m_fTreeRole();
                            if (m_pRole != null && m_pRole.Rows.Count > 0)
                                this.m_fFillTreeRole(m_pRole, this.treeRole);
                            this.treeRole.ExpandAll();
                            ///填充操作
                            this.treeOperate.Nodes.Clear();
                            DataTable m_pOperate = d_multi.m_fTreeOperate();
                            if (m_pOperate != null && m_pOperate.Rows.Count > 0)
                                this.m_fFillTreeOperate(m_pOperate);
                            this.treeOperate.ExpandAll();
                            ///执行委托
                            if (m_pMethodInvoker != null)
                                m_pMethodInvoker();
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][powerall][m_fFill][Thread][Exception][{ex.Message}]");
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][m_fFill][Exception][{ex.Message}]");
            }
        }

        private void m_fFillData(MethodInvoker m_pMethodInvoker = null)
        {
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            ///权限类别
                            this.treeDataPowerType.Nodes.Clear();
                            DataTable m_pDataPowerType = d_multi.m_fTreeDataPowerType();
                            if (m_pDataPowerType != null && m_pDataPowerType.Rows.Count > 0)
                                this.m_fFillTreeDataPowerType(m_pDataPowerType, this.treeDataPowerType);
                            ///填充部门树及账户,支持无限极
                            this.treeDataAccount.Nodes.Clear();
                            this.treeData.Nodes.Clear();
                            DataTable m_pAccount = d_multi.m_fTreeAccount();
                            if (m_pAccount != null && m_pAccount.Rows.Count > 0)
                            {
                                this.m_fFillTreeAccount(m_pAccount, this.treeDataAccount);
                                this.m_fFillTreeAccount(m_pAccount, this.treeData);
                            }
                            this.treeDataAccount.ExpandAll();
                            this.treeData.ExpandAll();
                            ///填充角色
                            this.treeDataRole.Nodes.Clear();
                            DataTable m_pRole = d_multi.m_fTreeRole();
                            if (m_pRole != null && m_pRole.Rows.Count > 0)
                                this.m_fFillTreeRole(m_pRole, this.treeDataRole);
                            this.treeDataRole.ExpandAll();
                            ///执行委托
                            if (m_pMethodInvoker != null)
                                m_pMethodInvoker();
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][powerall][m_fFill][Thread][Exception][{ex.Message}]");
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][m_fFill][Exception][{ex.Message}]");
            }
        }

        private void m_fFillBaseControlTeam(MethodInvoker m_pMethodInvoker = null)
        {
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            ///部门
                            this.treeBaseTeam.Nodes.Clear();
                            DataTable m_pBaseTeam = d_multi.m_fTreeBaseTeam();
                            if (m_pBaseTeam != null && m_pBaseTeam.Rows.Count > 0)
                            {
                                this.m_fFillTreeBaseTeam(m_pBaseTeam, this.treeBaseTeam);
                                ///增加无上级
                                DataRow m_pDataRow = m_pBaseTeam.NewRow();
                                m_pDataRow["ID"] = -1;
                                m_pDataRow["n"] = "无";
                                m_pBaseTeam.Rows.InsertAt(m_pDataRow, 0);
                                ///填充下拉框
                                this.cmbTfid.BeginUpdate();
                                this.cmbTfid.DataSource = m_pBaseTeam;
                                this.cmbTfid.ValueMember = "ID";
                                this.cmbTfid.DisplayMember = "n";
                                this.cmbTfid.EndUpdate();
                            }
                            ///清空所有信息
                            this.lblTeamID.Text = "-1";
                            this.txtTeamName.Text = string.Empty;
                            this.nudOrderNum.Value = 0;
                            ///执行委托
                            if (m_pMethodInvoker != null)
                                m_pMethodInvoker();
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][powerall][m_fFillBaseControlTeam][Thread][Exception][{ex.Message}]");
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][m_fFillBaseControlTeam][Exception][{ex.Message}]");
            }
        }

        private void m_fFillBaseControlRole(MethodInvoker m_pMethodInvoker = null)
        {
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            ///角色
                            this.treeBaseRole.Nodes.Clear();
                            DataTable m_pBaseRole = d_multi.m_fTreeRole();
                            if (m_pBaseRole != null && m_pBaseRole.Rows.Count > 0)
                                this.m_fFillTreeRole(m_pBaseRole, this.treeBaseRole);
                            ///清空所有信息
                            this.lblRoleID.Text = "-1";
                            this.txtRoleName.Text = string.Empty;
                            this.txtRoleNo.Text = string.Empty;
                            this.txtRoleDesc.Text = string.Empty;
                            ///执行委托
                            if (m_pMethodInvoker != null)
                                m_pMethodInvoker();
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][powerall][m_fFillBaseControlRole][Thread][Exception][{ex.Message}]");
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][m_fFillBaseControlRole][Exception][{ex.Message}]");
            }
        }
        private void m_fFillTreeBaseTeam(DataTable m_pDataTable, TreeView m_pTreeView, TreeNode m_pTreeNode = null, string ID = "-1")
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
                Log.Instance.Error($"[CenoCC][powerall][m_fFillTreeBaseTeam][Exception][{ex.Message}]");
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
                Log.Instance.Error($"[CenoCC][powerall][m_fFillTreeAccount][Exception][{ex.Message}]");
            }
        }

        private void m_fFillTreeRole(DataTable m_pDataTable, TreeView m_pTreeView)
        {
            try
            {
                foreach (DataRow m_pDataRow in m_pDataTable.Rows)
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
                    m_pTreeView.Nodes.Add(_m_pTreeNode);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][m_fFillTreeRole][Exception][{ex.Message}]");
            }
        }

        private void m_fFillTreeDataPowerType(DataTable m_pDataTable, TreeView m_pTreeView)
        {
            try
            {
                foreach (DataRow m_pDataRow in m_pDataTable.Rows)
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
                    m_pTreeView.Nodes.Add(_m_pTreeNode);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][m_fFillTreeDataPowerType][Exception][{ex.Message}]");
            }
        }

        private void m_fFillTreeOperate(DataTable m_pDataTable, TreeNode m_pTreeNode = null, string ID = "-1")
        {
            try
            {
                DataRow[] m_lDataRow;

                if (string.IsNullOrWhiteSpace(ID) || ID == "-1")
                    m_lDataRow = m_pDataTable.Select($" ISNULL([fID],'-1') = '-1' AND [t] = 'M' ");
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
                        this.treeOperate.Nodes.Add(_m_pTreeNode);
                    else
                        m_pTreeNode.Nodes.Add(_m_pTreeNode);
                    this.m_fFillTreeOperate(m_pDataTable, _m_pTreeNode, m_uID);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][m_fFillTreeAccount][Exception][{ex.Message}]");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                Invoke(new MethodInvoker(() =>
                {
                    try
                    {
                        this.m_bDoing = true;
                        List<Model_v1.m_mTree> m_lTree = new List<Model_v1.m_mTree>();
                        this.m_fFillCheckedList(m_lTree, this.treeAccount.Nodes);
                        this.m_fFillCheckedList(m_lTree, this.treeRole.Nodes);
                        if (m_lTree.Count > 0)
                        {
                            if (d_multi.m_fClearOperate(m_lTree))
                            {
                                MessageBox.Show(this, "操作权限清除成功");
                            }
                            else
                            {
                                MessageBox.Show(this, "操作权限清除完成");
                            }
                        }
                        else
                        {
                            MessageBox.Show("请勾选要清除的部门及账号、角色");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][powerall][btnClear_Click][Thread][Exception][{ex.Message}]");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }
                }));
            })).Start();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                Invoke(new MethodInvoker(() =>
                {
                    try
                    {
                        this.m_bDoing = true;
                        List<Model_v1.m_mTree> m_lTree = new List<Model_v1.m_mTree>();
                        this.m_fFillCheckedList(m_lTree, this.treeAccount.Nodes);
                        this.m_fFillCheckedList(m_lTree, this.treeRole.Nodes);
                        if (m_lTree.Count > 0)
                        {
                            List<Model_v1.m_mTree> m_lOperateTree = new List<Model_v1.m_mTree>();
                            this.m_fFillCheckedList(m_lOperateTree, this.treeOperate.Nodes);
                            if (d_multi.m_fSaveOperate(m_lTree, m_lOperateTree))
                            {
                                MessageBox.Show(this, "操作权限保存成功");
                            }
                            else
                            {
                                MessageBox.Show(this, "操作权限保存完成");
                            }
                        }
                        else
                        {
                            MessageBox.Show("请勾选要保存的部门及账号、角色");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][powerall][btnSave_Click][Thread][Exception][{ex.Message}]");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }
                }));
            })).Start();
        }

        /// <summary>
        /// 获取选中项
        /// </summary>
        /// <param name="m_lTree"></param>
        /// <param name="m_lTreeNode"></param>
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

        private void m_fCheckedOperate(Model_v1.m_mTree m_pTree)
        {
            if (this.m_bDoing)
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                Invoke(new MethodInvoker(() =>
                {
                    try
                    {
                        if (m_pTree == null)
                            throw new ArgumentNullException("m_pTree");
                        this.m_bDoing = true;
                        DataTable m_pDataTable = d_multi.m_fCheckedOperate(m_pTree);
                        this.m_fDoChecked(this.treeOperate.Nodes, m_pDataTable);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][powerall][m_fCheckedOperate][Thread][Exception][{ex.Message}]");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }
                }));
            })).Start();
        }

        private void m_fCheckedData(Model_v1.m_mTree m_pDataPowerTypeTree, Model_v1.m_mTree m_pTree)
        {
            if (this.m_bDoing)
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                Invoke(new MethodInvoker(() =>
                {
                    try
                    {
                        if (m_pDataPowerTypeTree == null)
                            throw new ArgumentNullException("m_pDataPowerTypeTree");
                        if (m_pTree == null)
                            throw new ArgumentNullException("m_pTree");
                        this.m_bDoing = true;
                        DataTable m_pDataTable = d_multi.m_fCheckedData(m_pDataPowerTypeTree, m_pTree);
                        this.m_fDoChecked(this.treeData.Nodes, m_pDataTable);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][powerall][m_fCheckedData][Thread][Exception][{ex.Message}]");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }
                }));
            })).Start();
        }

        private void treeAccount_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Bounds.Contains(e.Location))
            {
                if (this.chkUpdate.Checked)
                {
                    if (e.Node.Checked)
                    {
                        e.Node.Checked = false;
                        if (this.chkLinkage.Checked)
                        {
                            this.m_fFillCheckedList(null, e.Node.Nodes, false);
                        }
                    }
                    else
                    {
                        e.Node.Checked = true;
                        if (this.chkLinkage.Checked)
                        {
                            this.m_fFillCheckedList(null, e.Node.Nodes, true);
                        }
                    }
                }
                else
                {
                    this.m_fFillCheckedList(null, this.treeAccount.Nodes, false);
                    this.m_fFillCheckedList(null, this.treeRole.Nodes, false);
                    e.Node.Checked = true;
                    ///查询出实时权限并勾选
                    this.m_fCheckedOperate((Model_v1.m_mTree)e.Node.Tag);
                }
            }
        }

        private void treeRole_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Bounds.Contains(e.Location))
            {
                if (this.chkUpdate.Checked)
                {
                    if (e.Node.Checked)
                    {
                        e.Node.Checked = false;
                        if (this.chkLinkage.Checked)
                        {
                            this.m_fFillCheckedList(null, e.Node.Nodes, false);
                        }
                    }
                    else
                    {
                        e.Node.Checked = true;
                        if (this.chkLinkage.Checked)
                        {
                            this.m_fFillCheckedList(null, e.Node.Nodes, true);
                        }
                    }
                }
                else
                {
                    this.m_fFillCheckedList(null, this.treeAccount.Nodes, false);
                    this.m_fFillCheckedList(null, this.treeRole.Nodes, false);
                    e.Node.Checked = true;
                    ///查询出实时权限并勾选
                    this.m_fCheckedOperate((Model_v1.m_mTree)e.Node.Tag);
                }
            }
        }

        private void treeOperate_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Bounds.Contains(e.Location))
            {
                if (e.Node.Checked)
                {
                    e.Node.Checked = false;
                    if (this.chkLinkage.Checked)
                    {
                        this.m_fFillCheckedList(null, e.Node.Nodes, false);
                    }
                }
                else
                {
                    e.Node.Checked = true;
                    if (this.chkLinkage.Checked)
                    {
                        this.m_fFillCheckedList(null, e.Node.Nodes, true);
                    }
                }
            }
        }

        private void chkUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.chkUpdate.Checked)
            {
                this.m_fFillCheckedList(null, this.treeAccount.Nodes, false);
                this.m_fFillCheckedList(null, this.treeRole.Nodes, false);
                this.treeAccount.SelectedNode = null;
                this.treeRole.SelectedNode = null;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            List<Model_v1.m_mTree> m_lTree = new List<Model_v1.m_mTree>();
            if (!this.chkUpdate.Checked)
            {
                this.m_fFillCheckedList(m_lTree, this.treeAccount.Nodes);
                this.m_fFillCheckedList(m_lTree, this.treeRole.Nodes);
            }
            MethodInvoker m_pMethodInvoker = new MethodInvoker(() =>
            {
                ///重新选择权限
                if (m_lTree.Count == 1 && !this.chkUpdate.Checked)
                {
                    Model_v1.m_mTree m_pTree = m_lTree.FirstOrDefault();
                    switch (m_pTree.t)
                    {
                        case "R":
                            this.m_fFillCheckedList(null, this.treeRole.Nodes, null, m_pTree);
                            break;
                        default:
                            this.m_fFillCheckedList(null, this.treeAccount.Nodes, null, m_pTree);
                            break;
                    }
                    this.m_fCheckedOperate(m_pTree);
                }
            });
            ///重载
            this.m_fFill(m_pMethodInvoker);
            ///重新加载操作权限
            m_cPower.m_fGetOperatePower();
            ///菜单显隐
            MinChat.m_fLoadOperatePower();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl.SelectedIndex == 1 && !this.m_bDataTabLoad)
            {
                this.m_bDataTabLoad = true;
                ///加载数据权限信息
                this.m_fFillData(null);
            }
            else if (this.tabControl.SelectedIndex == 2 && !this.m_bBaseControlTabLoad)
            {
                this.m_bBaseControlTabLoad = true;
                ///加载基本信息控管之部门
                this.m_fFillBaseControlTeam(null);
                ///加载基本信息控管之角色
                this.m_fFillBaseControlRole(null);
            }
        }

        private void treeDataPowerType_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Bounds.Contains(e.Location))
            {
                if (this.chkDataUpdate.Checked)
                {
                    if (e.Node.Checked)
                    {
                        e.Node.Checked = false;
                        if (this.chkDataLinkage.Checked)
                        {
                            this.m_fFillCheckedList(null, e.Node.Nodes, false);
                        }
                    }
                    else
                    {
                        e.Node.Checked = true;
                        if (this.chkDataLinkage.Checked)
                        {
                            this.m_fFillCheckedList(null, e.Node.Nodes, true);
                        }
                    }
                }
                else
                {
                    ///取消其它选中
                    this.m_fFillCheckedList(null, this.treeDataPowerType.Nodes, false);
                    e.Node.Checked = true;
                    ///部门及账户与角色有唯一选中时
                    List<Model_v1.m_mTree> m_lTree = new List<Model_v1.m_mTree>();
                    this.m_fFillCheckedList(m_lTree, this.treeDataAccount.Nodes);
                    this.m_fFillCheckedList(m_lTree, this.treeDataRole.Nodes);
                    if (m_lTree.Count == 1)
                    {
                        ///查询出对应的数据权限及赋值
                        this.m_fCheckedData((Model_v1.m_mTree)e.Node.Tag, m_lTree.FirstOrDefault());
                    }
                }
            }
        }

        private void treeDataAccount_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Bounds.Contains(e.Location))
            {
                if (this.chkDataUpdate.Checked)
                {
                    if (e.Node.Checked)
                    {
                        e.Node.Checked = false;
                        if (this.chkDataLinkage.Checked)
                        {
                            this.m_fFillCheckedList(null, e.Node.Nodes, false);
                        }
                    }
                    else
                    {
                        e.Node.Checked = true;
                        if (this.chkDataLinkage.Checked)
                        {
                            this.m_fFillCheckedList(null, e.Node.Nodes, true);
                        }
                    }
                }
                else
                {
                    ///取消其它选中
                    this.m_fFillCheckedList(null, this.treeDataAccount.Nodes, false);
                    this.m_fFillCheckedList(null, this.treeDataRole.Nodes, false);
                    e.Node.Checked = true;
                    ///权限类型有选中时
                    List<Model_v1.m_mTree> m_lTree = new List<Model_v1.m_mTree>();
                    this.m_fFillCheckedList(m_lTree, this.treeDataPowerType.Nodes);
                    if (m_lTree.Count == 1)
                    {
                        ///查询出对应的数据权限及赋值
                        this.m_fCheckedData(m_lTree.FirstOrDefault(), (Model_v1.m_mTree)e.Node.Tag);
                    }
                }
            }
        }

        private void treeDataRole_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Bounds.Contains(e.Location))
            {
                if (this.chkDataUpdate.Checked)
                {
                    if (e.Node.Checked)
                    {
                        e.Node.Checked = false;
                        if (this.chkDataLinkage.Checked)
                        {
                            this.m_fFillCheckedList(null, e.Node.Nodes, false);
                        }
                    }
                    else
                    {
                        e.Node.Checked = true;
                        if (this.chkDataLinkage.Checked)
                        {
                            this.m_fFillCheckedList(null, e.Node.Nodes, true);
                        }
                    }
                }
                else
                {
                    ///取消其它选中
                    this.m_fFillCheckedList(null, this.treeDataAccount.Nodes, false);
                    this.m_fFillCheckedList(null, this.treeDataRole.Nodes, false);
                    e.Node.Checked = true;
                    ///权限类型有选中时
                    List<Model_v1.m_mTree> m_lTree = new List<Model_v1.m_mTree>();
                    this.m_fFillCheckedList(m_lTree, this.treeDataPowerType.Nodes);
                    if (m_lTree.Count == 1)
                    {
                        ///查询出对应的数据权限及赋值
                        this.m_fCheckedData(m_lTree.FirstOrDefault(), (Model_v1.m_mTree)e.Node.Tag);
                    }
                }
            }
        }

        private void treeData_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Bounds.Contains(e.Location))
            {
                if (e.Node.Checked)
                {
                    e.Node.Checked = false;
                    if (this.chkDataLinkage.Checked)
                    {
                        this.m_fFillCheckedList(null, e.Node.Nodes, false);
                    }
                }
                else
                {
                    e.Node.Checked = true;
                    if (this.chkDataLinkage.Checked)
                    {
                        this.m_fFillCheckedList(null, e.Node.Nodes, true);
                    }
                }
            }
        }

        private void chkDataUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.chkDataUpdate.Checked)
            {
                this.m_fFillCheckedList(null, this.treeDataPowerType.Nodes, false);
                this.m_fFillCheckedList(null, this.treeDataAccount.Nodes, false);
                this.m_fFillCheckedList(null, this.treeDataRole.Nodes, false);
                this.treeDataPowerType.SelectedNode = null;
                this.treeDataAccount.SelectedNode = null;
                this.treeDataRole.SelectedNode = null;
            }
        }

        private void btnDataRefresh_Click(object sender, EventArgs e)
        {
            List<Model_v1.m_mTree> m_lDataPowerTypeTree = new List<Model_v1.m_mTree>();
            if (!this.chkDataUpdate.Checked)
            {
                this.m_fFillCheckedList(m_lDataPowerTypeTree, this.treeDataPowerType.Nodes);
            }

            List<Model_v1.m_mTree> m_lTree = new List<Model_v1.m_mTree>();
            if (!this.chkDataUpdate.Checked)
            {
                this.m_fFillCheckedList(m_lTree, this.treeDataAccount.Nodes);
                this.m_fFillCheckedList(m_lTree, this.treeDataRole.Nodes);
            }

            MethodInvoker m_pMethodInvoker = new MethodInvoker(() =>
            {
                ///重新选择权限
                if (m_lDataPowerTypeTree.Count == 1 && m_lTree.Count == 1 && !this.chkUpdate.Checked)
                {
                    Model_v1.m_mTree m_pDataPowerTypeTree = m_lDataPowerTypeTree.FirstOrDefault();
                    this.m_fFillCheckedList(null, this.treeDataPowerType.Nodes, null, m_pDataPowerTypeTree);

                    Model_v1.m_mTree m_pTree = m_lTree.FirstOrDefault();
                    switch (m_pTree.t)
                    {
                        case "R":
                            this.m_fFillCheckedList(null, this.treeDataRole.Nodes, null, m_pTree);
                            break;
                        default:
                            this.m_fFillCheckedList(null, this.treeDataAccount.Nodes, null, m_pTree);
                            break;
                    }
                    this.m_fCheckedData(m_pDataPowerTypeTree, m_pTree);
                }
            });
            ///重载
            this.m_fFillData(m_pMethodInvoker);
            ///重新加载操作权限
            m_cPower.m_fGetDataPower();
        }

        private void btnDataClear_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                Invoke(new MethodInvoker(() =>
                {
                    try
                    {
                        this.m_bDoing = true;
                        List<Model_v1.m_mTree> m_lDataPowerTypeTree = new List<Model_v1.m_mTree>();
                        this.m_fFillCheckedList(m_lDataPowerTypeTree, this.treeDataPowerType.Nodes);
                        if (m_lDataPowerTypeTree.Count > 0)
                        {
                            List<Model_v1.m_mTree> m_lTree = new List<Model_v1.m_mTree>();
                            this.m_fFillCheckedList(m_lTree, this.treeDataAccount.Nodes);
                            this.m_fFillCheckedList(m_lTree, this.treeDataRole.Nodes);
                            if (m_lTree.Count > 0)
                            {
                                List<Model_v1.m_mTree> m_lDataTree = new List<Model_v1.m_mTree>();
                                if (d_multi.m_fSaveDataPower(m_lDataPowerTypeTree, m_lTree, m_lDataTree))
                                {
                                    MessageBox.Show(this, "数据权限清除成功");
                                }
                                else
                                {
                                    MessageBox.Show(this, "数据权限清除完成");
                                }
                            }
                            else
                            {
                                MessageBox.Show("请勾选部门及账号、角色");
                            }
                        }
                        else
                        {
                            MessageBox.Show("请勾选数据权限类别");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][powerall][btnDataClear_Click][Thread][Exception][{ex.Message}]");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }
                }));
            })).Start();
        }

        private void btnDataSave_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                Invoke(new MethodInvoker(() =>
                {
                    try
                    {
                        this.m_bDoing = true;
                        List<Model_v1.m_mTree> m_lDataPowerTypeTree = new List<Model_v1.m_mTree>();
                        this.m_fFillCheckedList(m_lDataPowerTypeTree, this.treeDataPowerType.Nodes);
                        if (m_lDataPowerTypeTree.Count > 0)
                        {
                            List<Model_v1.m_mTree> m_lTree = new List<Model_v1.m_mTree>();
                            this.m_fFillCheckedList(m_lTree, this.treeDataAccount.Nodes);
                            this.m_fFillCheckedList(m_lTree, this.treeDataRole.Nodes);
                            if (m_lTree.Count > 0)
                            {
                                List<Model_v1.m_mTree> m_lDataTree = new List<Model_v1.m_mTree>();
                                this.m_fFillCheckedList(m_lDataTree, this.treeData.Nodes);
                                if (d_multi.m_fSaveDataPower(m_lDataPowerTypeTree, m_lTree, m_lDataTree))
                                {
                                    MessageBox.Show(this, "数据权限保存成功");
                                }
                                else
                                {
                                    MessageBox.Show(this, "数据权限保存完成");
                                }
                            }
                            else
                            {
                                MessageBox.Show("请勾选部门及账号、角色");
                            }
                        }
                        else
                        {
                            MessageBox.Show("请勾选数据权限类别");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][powerall][btnDataSave_Click][Thread][Exception][{ex.Message}]");
                    }
                    finally
                    {
                        this.m_bDoing = false;
                    }
                }));
            })).Start();
        }

        private void treeBaseTeam_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Bounds.Contains(e.Location))
            {
                this.m_fFillCheckedList(null, this.treeBaseTeam.Nodes, false);
                e.Node.Checked = true;
                ///赋值
                DataTable m_pDataTable = d_multi.m_fGetBaseTeamByID(((Model_v1.m_mTree)(e.Node.Tag)).ID);
                if (m_pDataTable != null && m_pDataTable.Rows.Count == 1)
                {
                    DataRow m_pDataRow = m_pDataTable.Rows[0];
                    this.lblTeamID.Text = m_pDataRow["ID"].ToString();
                    this.txtTeamName.Text = m_pDataRow["n"].ToString();
                    this.cmbTfid.SelectedValue = m_pDataRow["fID"];
                    this.nudOrderNum.Value = Convert.ToDecimal(m_pDataRow["ordernum"]);
                }
            }
        }

        private void btnTeamAdd_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
                return;
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            this.m_bDoing = true;
                            if (Convert.ToInt32(this.lblTeamID.Text.Trim()) != -1)
                            {
                                MessageBox.Show(this, "新增部门时ID必须为-1,请先重置");
                                return;
                            }
                            if (string.IsNullOrWhiteSpace(this.txtTeamName.Text))
                            {
                                MessageBox.Show(this, "新增时请输入部门名称");
                                return;
                            }
                            if (!Cmn.MsgQ("确定要新增该部门吗?"))
                            {
                                return;
                            }
                            int m_uStatus = 0;
                            string m_sMsg = d_multi.m_fAddTeam(this.txtTeamName.Text.Trim(), this.cmbTfid.SelectedValue.ToString(), this.nudOrderNum.Value, out m_uStatus);
                            MessageBox.Show(this, m_sMsg);
                            if (m_uStatus == 1) this.m_fFillBaseControlTeam(null);
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][powerall][btnTeamAdd_Click][Thread][Exception][{ex.Message}]");
                        }
                        finally
                        {
                            this.m_bDoing = false;
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][btnTeamAdd_Click][Exception][{ex.Message}]");
            }
        }

        private void btnTeamEdit_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
                return;
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            this.m_bDoing = true;
                            if (Convert.ToInt32(this.lblTeamID.Text.Trim()) == -1)
                            {
                                MessageBox.Show(this, "编辑时ID不能为-1,请先选择要编辑的部门");
                                return;
                            }
                            if (string.IsNullOrWhiteSpace(this.txtTeamName.Text))
                            {
                                MessageBox.Show(this, "编辑时请输入部门名称");
                                return;
                            }
                            if (!Cmn.MsgQ("确定要编辑该部门吗?"))
                            {
                                return;
                            }
                            int m_uStatus = 0;
                            string m_sMsg = d_multi.m_fEditTeam(this.lblTeamID.Text.Trim(), this.txtTeamName.Text.Trim(), this.cmbTfid.SelectedValue.ToString(), this.nudOrderNum.Value, out m_uStatus);
                            MessageBox.Show(this, m_sMsg);
                            if (m_uStatus == 1) this.m_fFillBaseControlTeam(null);
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][powerall][btnTeamEdit_Click][Thread][Exception][{ex.Message}]");
                        }
                        finally
                        {
                            this.m_bDoing = false;
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][btnTeamEdit_Click][Exception][{ex.Message}]");
            }
        }

        private void btnTeamDelete_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
                return;
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            this.m_bDoing = true;
                            if (Convert.ToInt32(this.lblTeamID.Text.Trim()) == -1)
                            {
                                MessageBox.Show(this, "删除时ID不能为-1,请先选择要删除的部门");
                                return;
                            }
                            if (!Cmn.MsgQ("确定要删除该部门吗?"))
                            {
                                return;
                            }
                            int m_uStatus = 0;
                            string m_sMsg = d_multi.m_fDeleteTeam(this.lblTeamID.Text.Trim(), out m_uStatus);
                            MessageBox.Show(this, m_sMsg);
                            if (m_uStatus == 1) this.m_fFillBaseControlTeam(null);
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][powerall][btnTeamDelete_Click][Thread][Exception][{ex.Message}]");
                        }
                        finally
                        {
                            this.m_bDoing = false;
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][btnTeamDelete_Click][Exception][{ex.Message}]");
            }
        }

        private void btnTeamReset_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
                return;
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            this.m_bDoing = true;
                            this.m_fFillBaseControlTeam(null);
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][powerall][btnTeamReset_Click][Thread][Exception][{ex.Message}]");
                        }
                        finally
                        {
                            this.m_bDoing = false;
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][btnTeamReset_Click][Exception][{ex.Message}]");
            }
        }

        private void btnRoleAdd_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
                return;
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            this.m_bDoing = true;
                            if (Convert.ToInt32(this.lblRoleID.Text.Trim()) != -1)
                            {
                                MessageBox.Show(this, "新增角色时ID必须为-1,请先重置");
                                return;
                            }
                            if (string.IsNullOrWhiteSpace(this.txtRoleName.Text))
                            {
                                MessageBox.Show(this, "新增时请输入角色名称");
                                return;
                            }
                            if (!Cmn.MsgQ("确定要新增该角色吗?"))
                            {
                                return;
                            }
                            int m_uStatus = 0;
                            string m_sMsg = d_multi.m_fAddRole(this.txtRoleName.Text.Trim(), this.txtRoleDesc.Text, out m_uStatus);
                            MessageBox.Show(this, m_sMsg);
                            if (m_uStatus == 1) this.m_fFillBaseControlRole(null);
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][powerall][btnRoleAdd_Click][Thread][Exception][{ex.Message}]");
                        }
                        finally
                        {
                            this.m_bDoing = false;
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][btnRoleAdd_Click][Exception][{ex.Message}]");
            }
        }

        private void btnRoleEdit_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
                return;
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            this.m_bDoing = true;
                            if (Convert.ToInt32(this.lblRoleID.Text.Trim()) == -1)
                            {
                                MessageBox.Show(this, "编辑时ID不能为-1,请先选择要编辑的角色");
                                return;
                            }
                            if (string.IsNullOrWhiteSpace(this.txtRoleName.Text))
                            {
                                MessageBox.Show(this, "编辑时请输入角色名称");
                                return;
                            }
                            if (!Cmn.MsgQ("确定要编辑该角色吗?"))
                            {
                                return;
                            }
                            int m_uStatus = 0;
                            string m_sMsg = d_multi.m_fEditRole(this.lblRoleID.Text.Trim(), this.txtRoleName.Text.Trim(), this.txtRoleDesc.Text, out m_uStatus);
                            MessageBox.Show(this, m_sMsg);
                            if (m_uStatus == 1) this.m_fFillBaseControlRole(null);
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][powerall][btnRoleEdit_Click][Thread][Exception][{ex.Message}]");
                        }
                        finally
                        {
                            this.m_bDoing = false;
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][btnRoleEdit_Click][Exception][{ex.Message}]");
            }
        }

        private void btnRoleDelete_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
                return;
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            this.m_bDoing = true;
                            if (Convert.ToInt32(this.lblRoleID.Text.Trim()) == -1)
                            {
                                MessageBox.Show(this, "删除时ID不能为-1,请先选择要删除的角色");
                                return;
                            }
                            if (!Cmn.MsgQ("确定要删除该角色吗?"))
                            {
                                return;
                            }
                            int m_uStatus = 0;
                            string m_sMsg = d_multi.m_fDeleteRole(this.lblRoleID.Text.Trim(), out m_uStatus);
                            MessageBox.Show(this, m_sMsg);
                            if (m_uStatus == 1) this.m_fFillBaseControlRole(null);
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][powerall][btnRoleDelete_Click][Thread][Exception][{ex.Message}]");
                        }
                        finally
                        {
                            this.m_bDoing = false;
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][btnRoleDelete_Click][Exception][{ex.Message}]");
            }
        }

        private void btnRoleReset_Click(object sender, EventArgs e)
        {
            if (this.m_bDoing)
                return;
            try
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        try
                        {
                            this.m_bDoing = true;
                            this.m_fFillBaseControlRole(null);
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][powerall][btnRoleReset_Click][Thread][Exception][{ex.Message}]");
                        }
                        finally
                        {
                            this.m_bDoing = false;
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][powerall][btnRoleReset_Click][Exception][{ex.Message}]");
            }
        }

        private void treeBaseRole_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Bounds.Contains(e.Location))
            {
                this.m_fFillCheckedList(null, this.treeBaseRole.Nodes, false);
                e.Node.Checked = true;
                ///赋值
                DataTable m_pDataTable = d_multi.m_fGetBaseRoleByID(((Model_v1.m_mTree)(e.Node.Tag)).ID);
                if (m_pDataTable != null && m_pDataTable.Rows.Count == 1)
                {
                    DataRow m_pDataRow = m_pDataTable.Rows[0];
                    this.lblRoleID.Text = m_pDataRow["ID"].ToString();
                    this.txtRoleName.Text = m_pDataRow["n"].ToString();
                    this.txtRoleNo.Text = m_pDataRow["rno"].ToString();
                    this.txtRoleDesc.Text = m_pDataRow["rdesc"].ToString();
                }
            }
        }
    }
}

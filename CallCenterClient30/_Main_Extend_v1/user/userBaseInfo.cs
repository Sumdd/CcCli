using Cmn_v1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core_v1;
using DataBaseUtil;
using WebSocket_v1;
using CenoSocket;
using Common;

namespace CenoCC
{
    public partial class userBaseInfo : _form
    {
        private bool m_bDoing = false;

        public EventHandler SearchEvent;
        public user _entity;
        private int m_uID = -1;
        public userBaseInfo(int _m_uID)
        {
            this.m_uID = _m_uID;

            InitializeComponent();

            ///加载
            this.HandleCreated += (a, b) =>
            {
                this.m_fFill();
            };

            ///设置模式提示标识
            if (this.m_uID == -1)
            {
                this.lblTips.Text = "当前模式:批量编辑";
            }
            else
            {
                this.lblTips.Text = $"当前模式:单项编辑;ID:{m_uID}";
            }
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
                    if (!m_cPower.Has(m_pButton.Tag.ToString()))
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

        private void m_fFill()
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                Invoke(new MethodInvoker(() =>
                {
                    try
                    {
                        string m_sTeam = string.Empty;
                        string m_sRole = string.Empty;
                        int m_sID = -1;
                        int m_uLimitTheDial = 0;
                        int m_uF99d999 = 0;
                        if (this._entity?.list?.SelectedItems?.Count == 1)
                        {
                            ListViewItem m_pListViewItem = this._entity?.list?.SelectedItems[0];
                            this.txtUa.Text = m_pListViewItem.SubItems["agentname"].Text;
                            this.txtLoginName.Text = m_pListViewItem.SubItems["loginname"].Text;
                            m_sTeam = m_pListViewItem.SubItems["teamid"].Text;
                            m_sRole = m_pListViewItem.SubItems["roleid"].Text;
                            m_sID = Convert.ToInt32(m_pListViewItem.SubItems["id"].Text);
                            m_uLimitTheDial = Convert.ToInt32(m_pListViewItem.SubItems["limitthedial"].Text);
                            m_uF99d999 = Convert.ToInt32(m_pListViewItem.SubItems["f99d999"].Text);
                        }

                        ///查询出当前默认的同号码限呼设定值
                        int m_uDefLimittTheDial = 0;
                        {
                            string m_sSQL = $@"
SELECT
	IFNULL( ( SELECT `dial_parameter`.`v` FROM `dial_parameter` WHERE `dial_parameter`.`k` = 'limitthedial' LIMIT 1 ), 0 ) AS `limitthedial`;
";
                            DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                            if (m_pDataTable != null && m_pDataTable.Rows.Count > 0) m_uDefLimittTheDial = Convert.ToInt32(m_pDataTable.Rows[0]["limitthedial"]);
                        }

                        ///加载首发模式
                        {
                            this.cbxF99d999.BeginUpdate();
                            DataTable m_pDataTable = new DataTable();
                            m_pDataTable.Columns.Add("ID", typeof(int));
                            m_pDataTable.Columns.Add("Name", typeof(string));
                            DataRow m_pDataRow1 = m_pDataTable.NewRow();
                            m_pDataRow1["ID"] = 0;
                            m_pDataRow1["Name"] = "优先";
                            m_pDataTable.Rows.Add(m_pDataRow1);
                            DataRow m_pDataRow2 = m_pDataTable.NewRow();
                            m_pDataRow2["ID"] = 1;
                            m_pDataRow2["Name"] = "仅首发";
                            m_pDataTable.Rows.Add(m_pDataRow2);
                            this.cbxF99d999.DataSource = m_pDataTable;
                            this.cbxF99d999.ValueMember = "ID";
                            this.cbxF99d999.DisplayMember = "Name";
                            this.cbxF99d999.EndUpdate();

                            ///设置首发模式选中项
                            this.cbxF99d999.SelectedValue = m_uF99d999;
                        }

                        if (m_uID == -1)
                        {
                            ///姓名不可批量更改
                            this.btnUa.Enabled = false;
                            ///登录名不可批量更改
                            this.btnLoginName.Enabled = false;
                            ///密码不可批量更改
                            this.btnPassword.Enabled = false;
                            ///赋值默认
                            this.nudLimitTheDial.Value = m_uDefLimittTheDial;
                        }
                        else
                        {
                            this.nudLimitTheDial.Value = m_uLimitTheDial;

                            if (m_sID == 1000)
                            {
                                ///姓名不可更改
                                this.btnUa.Enabled = false;
                                ///登录名不可更改
                                this.btnLoginName.Enabled = false;
                                ///密码不可更改
                                this.btnPassword.Enabled = false;
                            }

                            ///如果当前为超级管理员
                            if (AgentInfo.AgentID == 1000.ToString())
                            {
                                ///姓名可更改
                                this.btnUa.Enabled = true;
                                ///密码可修改
                                this.btnPassword.Enabled = true;
                            }
                        }

                        this.Invoke(new MethodInvoker(() =>
                        {
                            this.cboTeam.BeginUpdate();
                            this.cboTeam.DataSource = m_cEsyMySQL.m_fGetTeam();
                            this.cboTeam.DisplayMember = "n";
                            this.cboTeam.ValueMember = "ID";

                            if (m_uID == -1)
                            {
                                this.btnTeam.Enabled = true;
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(m_sTeam))
                                    this.cboTeam.SelectedValue = m_sTeam;
                                ///部门不可修改
                                if (m_sID == 1000)
                                {
                                    this.btnTeam.Enabled = false;
                                }
                                ///超级管理员可修改
                                if (AgentInfo.AgentID == 1000.ToString())
                                {
                                    this.btnTeam.Enabled = true;
                                }
                            }

                            this.cboTeam.EndUpdate();
                        }));

                        this.Invoke(new MethodInvoker(() =>
                        {
                            this.cboRole.BeginUpdate();
                            this.cboRole.DataSource = m_cEsyMySQL.m_fGetRole();
                            this.cboRole.DisplayMember = "n";
                            this.cboRole.ValueMember = "ID";

                            if (m_uID == -1)
                            {
                                this.btnRole.Enabled = true;
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(m_sRole))
                                    this.cboRole.SelectedValue = m_sRole;
                                ///角色不可修改
                                if (m_sID == 1000)
                                {
                                    this.btnRole.Enabled = false;
                                }
                            }

                            this.cboRole.EndUpdate();
                        }));

                        ///操作权限
                        this.m_fLoadOperatePower(this.Controls);
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][userBaseInfo][m_fFill][Thread][Exception][{ex.Message}]");
                    }
                }));
            })).Start();
        }

        public void m_fSetSelectInfo(string m_sUa, string m_sLoginName, string m_sTeamID, string m_sRoleID, int m_sID)
        {
            ///如果模式为直接进入,可以批量编辑部门与角色
            if (this.m_uID == -1) return;

            this.lblTips.Text = $"当前模式:单项编辑;ID:{m_sID}";
            this.m_uID = m_sID;

            this.txtUa.Text = m_sUa;
            this.txtLoginName.Text = m_sLoginName;
            this.cboTeam.SelectedValue = m_sTeamID;
            this.cboRole.SelectedValue = m_sRoleID;

            ///根据ID以及当前登录人来判断是否可以更改超级管理员的信息
            if (m_sID == 1000)
            {
                ///姓名不可更改
                this.btnUa.Enabled = false;
                ///登录名不可更改
                this.btnLoginName.Enabled = false;
                ///部门不可修改
                this.btnTeam.Enabled = false;
                ///角色不可修改
                this.btnRole.Enabled = false;
                ///密码不可更改
                this.btnPassword.Enabled = false;
            }
            else
            {
                ///姓名可更改
                this.btnUa.Enabled = true;
                ///登录名可更改
                this.btnLoginName.Enabled = true;
                ///部门可修改
                this.btnTeam.Enabled = true;
                ///角色可修改
                this.btnRole.Enabled = true;
                ///密码可更改
                this.btnPassword.Enabled = true;
            }

            ///如果当前为超级管理员
            if (AgentInfo.AgentID == 1000.ToString())
            {
                ///姓名可更改
                this.btnUa.Enabled = true;
                ///部门可修改
                this.btnTeam.Enabled = true;
                ///密码可修改
                this.btnPassword.Enabled = true;
            }

            ///操作权限
            this.m_fLoadOperatePower(this.Controls);
        }

        private void btnUa_Click(object sender, EventArgs e)
        {
            if (m_uID == -1)
            {
                MessageBox.Show(this, "请选择一行进行姓名修改");
                return;
            }
            string m_sID = m_uID.ToString();
            string m_sUa = this.txtUa.Text.Trim();
            if (string.IsNullOrWhiteSpace(m_sUa))
            {
                MessageBox.Show(this, "请输入姓名");
                return;
            }
            string m_sCurrentPassword = this.txtCurrentPassword.Text;
            if (string.IsNullOrWhiteSpace(m_sCurrentPassword))
            {
                MessageBox.Show(this, "请输入当前密码");
                return;
            }
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
                            string m_sSQL = $@"
SET @m_uCount = IFNULL( ( SELECT COUNT( 1 ) FROM `call_agent` AS `T0` WHERE `T0`.`ID` = '{Common.AgentInfo.AgentID}' AND `T0`.`LoginPassWord` = '{Common.Encrypt.EncryptString(m_sCurrentPassword)}' ), 0 );
UPDATE `call_agent` 
SET `call_agent`.`AgentName` = '{m_sUa}' 
WHERE
	`call_agent`.`ID` = {m_sID} 
	AND @m_uCount = 1;
SELECT
	@m_uCount AS `status`,
	ROW_COUNT( ) AS `count`;
";
                            DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                            if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
                            {
                                int m_uStatus = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                                int m_uCount = Convert.ToInt32(m_pDataTable.Rows[0]["count"]);
                                if (m_uStatus == 1 && m_uCount > 0)
                                {
                                    if (this.SearchEvent != null)
                                        this.SearchEvent(sender, e);
                                    MessageBox.Show(this, "修改姓名成功");
                                }
                                else if (m_uStatus != 1)
                                {
                                    MessageBox.Show(this, "当前密码错误");
                                }
                                else
                                {
                                    MessageBox.Show(this, "修改姓名完成");
                                }
                                return;
                            }
                            MessageBox.Show(this, "修改姓名失败");
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][userBaseInfo][btnUa_Click][Thread][Exception][{ex.Message}]");
                            MessageBox.Show(this, $"修改姓名时出错:{ex.Message}");
                        }
                        finally
                        {
                            //this.txtCurrentPassword.Clear();
                            this.m_bDoing = false;
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][userBaseInfo][btnUa_Click][Exception][{ex.Message}]");
            }
        }

        private void btnLoginName_Click(object sender, EventArgs e)
        {
            if (m_uID == -1)
            {
                MessageBox.Show(this, "请选择一行进行登录名修改");
                return;
            }
            string m_sID = m_uID.ToString();
            string m_sLoginName = this.txtLoginName.Text.Trim();
            if (string.IsNullOrWhiteSpace(m_sLoginName))
            {
                MessageBox.Show(this, "请输入登录名");
                return;
            }
            string m_sCurrentPassword = this.txtCurrentPassword.Text;
            if (string.IsNullOrWhiteSpace(m_sCurrentPassword))
            {
                MessageBox.Show(this, "请输入当前密码");
                return;
            }
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
                            string m_sSQL = $@"
SET @m_uCount = IFNULL( ( SELECT COUNT( 1 ) FROM `call_agent` AS `T0` WHERE `T0`.`ID` = '{Common.AgentInfo.AgentID}' AND `T0`.`LoginPassWord` = '{Common.Encrypt.EncryptString(m_sCurrentPassword)}' ), 0 );
SET @m_uRepeat = IFNULL( ( SELECT COUNT( 1 ) FROM `call_agent` AS `T0` WHERE `T0`.`LoginName` = '{m_sLoginName}' ), 0 );
UPDATE `call_agent` 
SET `call_agent`.`LoginName` = '{m_sLoginName}' 
WHERE
	`call_agent`.`ID` = {m_sID} 
	AND @m_uCount = 1 
	AND @m_uRepeat = 0;
SELECT
	@m_uCount AS `status`,
	ROW_COUNT( ) AS `count`,
	@m_uRepeat AS `repeat`;
";
                            DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                            if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
                            {
                                int m_uStatus = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                                int m_uCount = Convert.ToInt32(m_pDataTable.Rows[0]["count"]);
                                int m_uRepeat = Convert.ToInt32(m_pDataTable.Rows[0]["repeat"]);
                                if (m_uStatus == 1 && m_uRepeat == 0 && m_uCount > 0)
                                {
                                    if (this.SearchEvent != null)
                                        this.SearchEvent(sender, e);
                                    MessageBox.Show(this, "修改登录名成功");
                                    ///修改服务端缓存
                                    InWebSocketMain.Send(M_Send._zdwh("UpdLoginName"));
                                }
                                else if (m_uStatus != 1)
                                {
                                    MessageBox.Show(this, "当前密码错误");
                                }
                                else if (m_uRepeat != 0)
                                {
                                    MessageBox.Show(this, "登录名重复");
                                }
                                else
                                {
                                    MessageBox.Show(this, "修改登录名完成");
                                }
                                return;
                            }
                            MessageBox.Show(this, "修改登录名失败");
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][userBaseInfo][btnLoginName_Click][Thread][Exception][{ex.Message}]");
                            MessageBox.Show(this, $"修改登录名时出错:{ex.Message}");
                        }
                        finally
                        {
                            //this.txtCurrentPassword.Clear();
                            this.m_bDoing = false;
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][userBaseInfo][btnLoginName_Click][Exception][{ex.Message}]");
            }
        }

        private void btnTeam_Click(object sender, EventArgs e)
        {
            List<string> m_lID = new List<string>();
            if (this.m_uID == -1)
            {
                if (this._entity?.list?.SelectedItems?.Count > 0)
                {
                    foreach (ListViewItem item in this._entity?.list?.SelectedItems)
                    {
                        m_lID.Add(item?.SubItems["id"]?.Text);
                    }
                }
            }
            else
            {
                m_lID.Add(this.m_uID.ToString());
            }

            if (m_lID.Count <= 0)
            {
                MessageBox.Show(this, "请至少选择一行进行部门修改");
                return;
            }

            if (m_lID.Contains(1000.ToString()) && AgentInfo.AgentID != 1000.ToString())
            {
                MessageBox.Show(this, "批量编辑部门时不可包含默认的超级管理员admin");
                return;
            }

            string m_sTeam = this.cboTeam.SelectedValue.ToString();
            if (string.IsNullOrWhiteSpace(m_sTeam))
            {
                MessageBox.Show(this, "请选择部门");
                return;
            }
            string m_sCurrentPassword = this.txtCurrentPassword.Text;
            if (string.IsNullOrWhiteSpace(m_sCurrentPassword))
            {
                MessageBox.Show(this, "请输入当前密码");
                return;
            }
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
                            string m_sSQL = $@"
SET @m_uCount = IFNULL( ( SELECT COUNT( 1 ) FROM `call_agent` AS `T0` WHERE `T0`.`ID` = '{Common.AgentInfo.AgentID}' AND `T0`.`LoginPassWord` = '{Common.Encrypt.EncryptString(m_sCurrentPassword)}' ), 0 );
UPDATE `call_agent` 
SET `call_agent`.`TeamID` = {m_sTeam} 
WHERE
	`call_agent`.`ID` IN ( {string.Join(",", m_lID)} )
	AND @m_uCount = 1;
SELECT
	@m_uCount AS `status`,
	ROW_COUNT( ) AS `count`;
";
                            DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                            if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
                            {
                                int m_uStatus = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                                int m_uCount = Convert.ToInt32(m_pDataTable.Rows[0]["count"]);
                                if (m_uStatus == 1 && m_uCount > 0)
                                {
                                    if (this.SearchEvent != null)
                                        this.SearchEvent(sender, e);
                                    MessageBox.Show(this, "修改部门成功");
                                }
                                else if (m_uStatus != 1)
                                {
                                    MessageBox.Show(this, "当前密码错误");
                                }
                                else
                                {
                                    MessageBox.Show(this, "修改部门完成");
                                }
                                return;
                            }
                            MessageBox.Show(this, "修改部门失败");
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][userBaseInfo][btnTeam_Click][Thread][Exception][{ex.Message}]");
                            MessageBox.Show(this, $"修改部门时出错:{ex.Message}");
                        }
                        finally
                        {
                            //this.txtCurrentPassword.Clear();
                            this.m_bDoing = false;
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][userBaseInfo][btnTeam_Click][Exception][{ex.Message}]");
            }
        }

        private void btnRole_Click(object sender, EventArgs e)
        {
            List<string> m_lID = new List<string>();
            if (this.m_uID == -1)
            {
                if (this._entity?.list?.SelectedItems?.Count > 0)
                {
                    foreach (ListViewItem item in this._entity?.list?.SelectedItems)
                    {
                        m_lID.Add(item?.SubItems["id"]?.Text);
                    }
                }
            }
            else
            {
                m_lID.Add(this.m_uID.ToString());
            }

            if (m_lID.Count <= 0)
            {
                MessageBox.Show(this, "请至少选择一行进行部门修改");
                return;
            }

            if (m_lID.Contains(1000.ToString()))
            {
                MessageBox.Show(this, "批量编辑角色时不可包含默认的超级管理员admin");
                return;
            }

            string m_sRole = this.cboRole.SelectedValue.ToString();
            if (string.IsNullOrWhiteSpace(m_sRole))
            {
                MessageBox.Show(this, "请选择角色");
                return;
            }
            string m_sCurrentPassword = this.txtCurrentPassword.Text;
            if (string.IsNullOrWhiteSpace(m_sCurrentPassword))
            {
                MessageBox.Show(this, "请输入当前密码");
                return;
            }
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
                            string m_sSQL = $@"
SET @m_uCount = IFNULL( ( SELECT COUNT( 1 ) FROM `call_agent` AS `T0` WHERE `T0`.`ID` = '{Common.AgentInfo.AgentID}' AND `T0`.`LoginPassWord` = '{Common.Encrypt.EncryptString(m_sCurrentPassword)}' ), 0 );
UPDATE `call_agent` 
SET `call_agent`.`RoleID` = {m_sRole} 
WHERE
	`call_agent`.`ID` IN ( {string.Join(",", m_lID)} )
	AND @m_uCount = 1;
SELECT
	@m_uCount AS `status`,
	ROW_COUNT( ) AS `count`;
";
                            DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                            if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
                            {
                                int m_uStatus = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                                int m_uCount = Convert.ToInt32(m_pDataTable.Rows[0]["count"]);
                                if (m_uStatus == 1 && m_uCount > 0)
                                {
                                    if (this.SearchEvent != null)
                                        this.SearchEvent(sender, e);
                                    MessageBox.Show(this, "修改角色成功");
                                }
                                else if (m_uStatus != 1)
                                {
                                    MessageBox.Show(this, "当前密码错误");
                                }
                                else
                                {
                                    MessageBox.Show(this, "修改角色完成");
                                }
                                return;
                            }
                            MessageBox.Show(this, "修改角色失败");
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][userBaseInfo][btnRole_Click][Thread][Exception][{ex.Message}]");
                            MessageBox.Show(this, $"修改角色时出错:{ex.Message}");
                        }
                        finally
                        {
                            //this.txtCurrentPassword.Clear();
                            this.m_bDoing = false;
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][userBaseInfo][btnRole_Click][Exception][{ex.Message}]");
            }
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            if (m_uID == -1)
            {
                MessageBox.Show(this, "请选择一行进行密码修改");
                return;
            }
            string m_sID = m_uID.ToString();
            string m_sCurrentPassword = this.txtCurrentPassword.Text;
            if (string.IsNullOrWhiteSpace(m_sCurrentPassword))
            {
                MessageBox.Show(this, "请输入当前密码");
                return;
            }
            string m_sPassword = this.txtPassword.Text;
            if (string.IsNullOrWhiteSpace(m_sPassword))
            {
                MessageBox.Show(this, "请输入密码");
                return;
            }
            string m_sConfirmPassword = this.txtConfirmPassword.Text;
            if (m_sConfirmPassword != m_sPassword)
            {
                MessageBox.Show(this, "俩次输入的密码不一致");
                return;
            }
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
                            string m_sSQL = $@"
SET @m_uCount = IFNULL( ( SELECT COUNT( 1 ) FROM `call_agent` AS `T0` WHERE `T0`.`ID` = '{Common.AgentInfo.AgentID}' AND `T0`.`LoginPassWord` = '{Common.Encrypt.EncryptString(m_sCurrentPassword)}' ), 0 );
UPDATE `call_agent` 
SET `call_agent`.`LoginPassWord` = '{(Common.Encrypt.EncryptString(m_sPassword))}' 
WHERE
	`call_agent`.`ID` = {m_sID} 
	AND @m_uCount = 1;
SELECT
	@m_uCount AS `status`,
	ROW_COUNT( ) AS `count`;
";
                            DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                            if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
                            {
                                int m_uStatus = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                                int m_uCount = Convert.ToInt32(m_pDataTable.Rows[0]["count"]);
                                if (m_uStatus == 1 && m_uCount > 0)
                                {
                                    if (this.SearchEvent != null)
                                        this.SearchEvent(sender, e);
                                    MessageBox.Show(this, "修改密码成功");
                                }
                                else if (m_uStatus != 1)
                                {
                                    MessageBox.Show(this, "当前密码错误");
                                }
                                else
                                {
                                    MessageBox.Show(this, "修改密码完成");
                                }
                                return;
                            }
                            MessageBox.Show(this, "修改密码失败");
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][userBaseInfo][btnPassword_Click][Thread][Exception][{ex.Message}]");
                            MessageBox.Show(this, $"修改密码时出错:{ex.Message}");
                        }
                        finally
                        {
                            //不清除密码
                            //this.txtCurrentPassword.Clear();
                            this.m_bDoing = false;
                        }
                    }));
                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][userBaseInfo][btnPassword_Click][Exception][{ex.Message}]");
            }
        }

        private void btnLimitTheDial_Click(object sender, EventArgs e)
        {
            List<string> m_lID = new List<string>();
            if (this.m_uID == -1)
            {
                if (this._entity?.list?.SelectedItems?.Count > 0)
                {
                    foreach (ListViewItem item in this._entity?.list?.SelectedItems)
                    {
                        m_lID.Add(item?.SubItems["id"]?.Text);
                    }
                }
            }
            else
            {
                m_lID.Add(this.m_uID.ToString());
            }

            if (m_lID.Count <= 0)
            {
                MessageBox.Show(this, "请至少选择一行进行同号码限呼修改");
                return;
            }

            ///同号码限呼次数
            int m_uLimitTheDial = Convert.ToInt32(this.nudLimitTheDial.Value);

            string m_sCurrentPassword = this.txtCurrentPassword.Text;
            if (string.IsNullOrWhiteSpace(m_sCurrentPassword))
            {
                MessageBox.Show(this, "请输入当前密码");
                return;
            }
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
                            string m_sSQL = $@"
SET @m_uCount = IFNULL( ( SELECT COUNT( 1 ) FROM `call_agent` AS `T0` WHERE `T0`.`ID` = '{Common.AgentInfo.AgentID}' AND `T0`.`LoginPassWord` = '{Common.Encrypt.EncryptString(m_sCurrentPassword)}' ), 0 );
UPDATE `call_clientparam` 
SET `call_clientparam`.`limitthedial` = {m_uLimitTheDial} 
WHERE
	`call_clientparam`.`ID` IN ( SELECT `call_agent`.`ClientParamID` FROM `call_agent` WHERE `call_agent`.`ID` IN ( {string.Join(",", m_lID)} ) ) 
	AND @m_uCount = 1;
SELECT
	@m_uCount AS `status`,
	ROW_COUNT( ) AS `count`;
";
                            DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                            if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
                            {
                                int m_uStatus = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                                int m_uCount = Convert.ToInt32(m_pDataTable.Rows[0]["count"]);
                                if (m_uStatus == 1 && m_uCount > 0)
                                {
                                    if (this.SearchEvent != null)
                                        this.SearchEvent(sender, e);
                                    MessageBox.Show(this, "修改同号码限呼成功");
                                }
                                else if (m_uStatus != 1)
                                {
                                    MessageBox.Show(this, "当前密码错误");
                                }
                                else
                                {
                                    MessageBox.Show(this, "修改同号码限呼完成");
                                }
                                return;
                            }
                            MessageBox.Show(this, "修改同号码限呼失败");
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][userBaseInfo][btnLimitTheDial_Click][Thread][Exception][{ex.Message}]");
                            MessageBox.Show(this, $"修改同号码限呼时出错:{ex.Message}");
                        }
                        finally
                        {
                            //this.txtCurrentPassword.Clear();
                            this.m_bDoing = false;
                        }
                    }));

                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][userBaseInfo][btnLimitTheDial_Click][Exception][{ex.Message}]");
            }
        }

        private void btnF99d999OK_Click(object sender, EventArgs e)
        {
            List<string> m_lID = new List<string>();
            if (this.m_uID == -1)
            {
                if (this._entity?.list?.SelectedItems?.Count > 0)
                {
                    foreach (ListViewItem item in this._entity?.list?.SelectedItems)
                    {
                        m_lID.Add(item?.SubItems["id"]?.Text);
                    }
                }
            }
            else
            {
                m_lID.Add(this.m_uID.ToString());
            }

            if (m_lID.Count <= 0)
            {
                MessageBox.Show(this, "请至少选择一行进行首发模式修改");
                return;
            }

            ///首发模式
            int m_uF99d999 = Convert.ToInt32(this.cbxF99d999.SelectedValue);

            string m_sCurrentPassword = this.txtCurrentPassword.Text;
            if (string.IsNullOrWhiteSpace(m_sCurrentPassword))
            {
                MessageBox.Show(this, "请输入当前密码");
                return;
            }
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
                            string m_sSQL = $@"
SET @m_uCount = IFNULL( ( SELECT COUNT( 1 ) FROM `call_agent` AS `T0` WHERE `T0`.`ID` = '{Common.AgentInfo.AgentID}' AND `T0`.`LoginPassWord` = '{Common.Encrypt.EncryptString(m_sCurrentPassword)}' ), 0 );
UPDATE `call_clientparam` 
SET `call_clientparam`.`f99d999` = {m_uF99d999} 
WHERE
	`call_clientparam`.`ID` IN ( SELECT `call_agent`.`ClientParamID` FROM `call_agent` WHERE `call_agent`.`ID` IN ( {string.Join(",", m_lID)} ) ) 
	AND @m_uCount = 1;
SELECT
	@m_uCount AS `status`,
	ROW_COUNT( ) AS `count`;
";
                            DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                            if (m_pDataTable != null && m_pDataTable.Rows.Count > 0)
                            {
                                int m_uStatus = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                                int m_uCount = Convert.ToInt32(m_pDataTable.Rows[0]["count"]);
                                if (m_uStatus == 1 && m_uCount > 0)
                                {
                                    if (this.SearchEvent != null)
                                        this.SearchEvent(sender, e);
                                    MessageBox.Show(this, "修改首发模式成功");
                                }
                                else if (m_uStatus != 1)
                                {
                                    MessageBox.Show(this, "当前密码错误");
                                }
                                else
                                {
                                    MessageBox.Show(this, "修改首发模式完成");
                                }
                                return;
                            }
                            MessageBox.Show(this, "修改首发模式失败");
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][userBaseInfo][btnF99d999OK_Click][Thread][Exception][{ex.Message}]");
                            MessageBox.Show(this, $"修改首发模式时出错:{ex.Message}");
                        }
                        finally
                        {
                            //this.txtCurrentPassword.Clear();
                            this.m_bDoing = false;
                        }
                    }));

                })).Start();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][userBaseInfo][btnF99d999OK_Click][Exception][{ex.Message}]");
            }
        }
    }
}

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

namespace CenoCC
{
    public partial class userBaseInfo : _form
    {
        private bool m_bDoing = false;

        public EventHandler SearchEvent;
        public user _entity;
        public userBaseInfo()
        {
            InitializeComponent();

            ///操作权限
            this.m_fLoadOperatePower(this.Controls);

            ///加载
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
                        int m_sID = 1000;
                        if (this._entity?.list?.SelectedItems?.Count == 1)
                        {
                            ListViewItem m_pListViewItem = this._entity?.list?.SelectedItems[0];
                            this.txtUa.Text = m_pListViewItem.SubItems["agentname"].Text;
                            this.txtLoginName.Text = m_pListViewItem.SubItems["loginname"].Text;
                            m_sTeam = m_pListViewItem.SubItems["teamid"].Text;
                            m_sRole = m_pListViewItem.SubItems["roleid"].Text;
                            m_sID = Convert.ToInt32(m_pListViewItem.SubItems["id"].Text);
                        }

                        if (m_sID == 1000)
                        {
                            this.txtLoginName.Enabled = false;
                            this.btnLoginName.Enabled = false;
                        }

                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            this.cboTeam.BeginUpdate();
                            this.cboTeam.DataSource = m_cEsyMySQL.m_fGetTeam();
                            this.cboTeam.DisplayMember = "n";
                            this.cboTeam.ValueMember = "ID";
                            if (!string.IsNullOrWhiteSpace(m_sTeam))
                                this.cboTeam.SelectedValue = m_sTeam;
                            this.cboTeam.EndUpdate();
                        }));

                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            this.cboRole.BeginUpdate();
                            this.cboRole.DataSource = m_cEsyMySQL.m_fGetRole();
                            this.cboRole.DisplayMember = "n";
                            this.cboRole.ValueMember = "ID";
                            if (!string.IsNullOrWhiteSpace(m_sRole))
                                this.cboRole.SelectedValue = m_sRole;
                            if (m_sID == 1000)
                            {
                                this.cboRole.Enabled = false;
                                this.btnRole.Enabled = false;
                            }
                            this.cboRole.EndUpdate();
                        }));
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[CenoCC][userBaseInfo][m_fFill][Thread][Exception][{ex.Message}]");
                    }
                }));
            })).Start();
        }

        public void m_fSetSelectInfo(string m_sUa, string m_sLoginName, string m_sTeamID, string m_sRoleID)
        {
            this.txtUa.Text = m_sUa;
            this.txtLoginName.Text = m_sLoginName;
            this.cboTeam.SelectedValue = m_sTeamID;
            this.cboRole.SelectedValue = m_sRoleID;
        }

        private void btnUa_Click(object sender, EventArgs e)
        {
            if (this._entity?.list?.SelectedItems?.Count != 1)
            {
                MessageBox.Show(this, "请选择一行进行修改基本信息");
                return;
            }
            string m_sID = this._entity?.list?.SelectedItems[0]?.SubItems["id"]?.Text;
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
            if (this._entity?.list?.SelectedItems?.Count != 1)
            {
                MessageBox.Show(this, "请选择一行进行修改基本信息");
                return;
            }
            string m_sID = this._entity?.list?.SelectedItems[0]?.SubItems["id"]?.Text;
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
            if (this._entity?.list?.SelectedItems?.Count != 1)
            {
                MessageBox.Show(this, "请选择一行进行修改基本信息");
                return;
            }
            string m_sID = this._entity?.list?.SelectedItems[0]?.SubItems["id"]?.Text;
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
            if (this._entity?.list?.SelectedItems?.Count != 1)
            {
                MessageBox.Show(this, "请选择一行进行修改基本信息");
                return;
            }
            string m_sID = this._entity?.list?.SelectedItems[0]?.SubItems["id"]?.Text;
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
            if (this._entity?.list?.SelectedItems?.Count != 1)
            {
                MessageBox.Show(this, "请选择一行进行修改基本信息");
                return;
            }
            string m_sID = this._entity?.list?.SelectedItems[0]?.SubItems["id"]?.Text;
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
    }
}

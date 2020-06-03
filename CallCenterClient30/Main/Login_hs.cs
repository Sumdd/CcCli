using Common;
using Core_v1;
using DataBaseUtil;
using MetroFramework.Forms;
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
    public partial class Login_hs : MetroForm
    {
        private bool m_bDoLogin = false;
        public Login_hs()
        {
            InitializeComponent();

            {
                this.Select();
                this.txtAccount.Text = ParamInfo.RememberUserName;
                if (string.IsNullOrWhiteSpace(this.txtAccount.Text.Trim()))
                {
                    this.txtAccount.Select();
                    this.txtAccount.Focus();
                }
                else
                {
                    this.txtPwd.Select();
                    this.txtPwd.Focus();
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_fDoLogin();
        }

        private void m_fDoLogin()
        {
            if (this.m_bDoLogin)
                return;
            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {

                try
                {
                    this.m_bDoLogin = true;

                    this.btnOK.Enabled = false;
                    this.btnOK.Text = "登陆中...";

                    DataTable m_pDataTable = Call_AgentUtil.CheckLogin(this.txtAccount.Text, this.txtPwd.Text);
                    if (m_pDataTable.Rows.Count <= 0)
                    {
                        this.lblTip.Text = "用户名或密码错误";
                        this.m_fReLogin();
                        return;
                    }

                    if (m_pDataTable.Rows[0]["Usable"].ToString() == "0")
                    {
                        this.lblTip.Text = "账户被冻结";
                        this.m_fReLogin();
                        return;
                    }

                    try
                    {
                        AgentInfo.AgentID = m_pDataTable.Rows[0]["ID"].ToString();
                        AgentInfo.AgentName = m_pDataTable.Rows[0]["AgentName"].ToString();
                        AgentInfo.LoginName = m_pDataTable.Rows[0]["LoginName"].ToString();
                        AgentInfo.LoginPassword = m_pDataTable.Rows[0]["LoginPassword"].ToString();
                        AgentInfo.ChannelID = int.Parse(string.IsNullOrEmpty(m_pDataTable.Rows[0]["ChannelID"].ToString()) ? "-1" : m_pDataTable.Rows[0]["ChannelID"].ToString());
                        AgentInfo.RoleID = Convert.ToInt32(m_pDataTable.Rows[0]["RoleID"]);
                        AgentInfo.RoleName = m_pDataTable.Rows[0]["RoleName"].ToString();
                        AgentInfo.TeamName = m_pDataTable.Rows[0]["TeamName"].ToString();
                        AgentInfo.LinkUser = m_pDataTable.Rows[0]["LinkUser"].ToString();
                        AgentInfo.LU_LoginName = m_pDataTable.Rows[0]["LU_LoginName"].ToString();
                        AgentInfo.LU_Password = m_pDataTable.Rows[0]["LU_Password"].ToString();

                        Call_AgentUtil.InsertLoginLog(m_pDataTable.Rows[0]["ID"].ToString(), CommonParam.GetLocalIpAddress, "1");
                        Call_AgentUtil.UpdateLoginState(m_pDataTable.Rows[0]["ID"].ToString(), "1");

                        ParamInfo.RememberUserName = AgentInfo.LoginName;

                        this.DialogResult = DialogResult.OK;
                    }
                    catch
                    {
                        this.lblTip.Text = "账户信息错误";
                        this.m_fReLogin();
                        return;
                    }

                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][Login_hs][m_fDoLogin][Exception][{ex.Message}]");

                    this.m_fReLogin();
                }

            })).Start();

        }

        private void txtAccount_TextChanged(object sender, EventArgs e)
        {
            this.lblTip.Text = "";
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            this.lblTip.Text = "";
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.m_fDoLogin();
            }
        }

        private void m_fReLogin()
        {
            this.m_bDoLogin = false;

            this.btnOK.Enabled = true;
            this.btnOK.Text = "登陆";
        }
    }
}

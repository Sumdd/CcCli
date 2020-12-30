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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CenoCC
{
    public partial class Login : MetroForm
    {
        private Timer m_pTimer = null;
        private int m_uSecond = 0;
        private bool m_bDoLogin = false;
        private bool m_bIsLoad = true;
        private Model_v1.M_kv m_pIPv4 = null;
        public Login(string m_sWhere = "")
        {
            //485323
            InitializeComponent();

            #region ***自定义登陆界面
            switch (m_sWhere)
            {
                case "hs":
                    this.pictureBox1.Image = global::CenoCC.Properties.Resources.微信图片_20191011171918;
                    this.pictureBox1.Size = new Size(76, 50);
                    this.pictureBox1.Left = this.pictureBox1.Left - 22;
                    break;
            }
            #endregion

            this.btnSetting_Click(null, null);

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

            //下拉框值,可变动
            {
                this.cbxServer.DataSource = m_cProfile.m_lServerIP;

                ///绑定
                this.cbxKvp.BeginUpdate();
                this.cbxKvp.DataSource = m_cProfile.m_lServerName?.Select(x => { return new Model_v1.M_kv { key = x.Key, value = x.Value.Value }; })?.ToList();
                this.cbxKvp.ValueMember = "key";
                this.cbxKvp.DisplayMember = "value";
                this.cbxKvp.EndUpdate();

                if (m_cProfile.m_lServerIP != null && m_cProfile.m_lServerIP.Length > 0)
                {
                    if (!string.IsNullOrWhiteSpace(m_cProfile.server) && m_cProfile.m_lServerIP.Contains(m_cProfile.server))
                    {
                        this.cbxServer.SelectedItem = m_cProfile.server;
                    }
                    else
                    {
                        this.cbxServer.SelectedIndex = 0;
                    }

                    ///对绑
                    this.cbxKvp.SelectedValue = this.cbxServer.Text;
                }
                this.txtDatabase.Text = m_cProfile.database;
                this.txtDuid.Text = m_cProfile.uid;
                this.txtDpwd.Text = Encrypt.DecryptString(m_cProfile.pwd);
            }

            //多网卡
            {
                List<Model_v1.M_kv> m_lKv = CommonParam.GetAllNetwork();
                this.cbxNetwork.DataSource = m_lKv;
                this.cbxNetwork.ValueMember = "key";
                this.cbxNetwork.DisplayMember = "value";
                int m_uIndex = -1;
                //是否有保存了本地IP
                string m_sLocalIPv4 = m_cProfile.localip;
                if (!string.IsNullOrWhiteSpace(m_sLocalIPv4) && m_lKv.Count > 0)
                {
                    for (int i = 0; i < m_lKv.Count; i++)
                    {
                        if (m_lKv[i]?.tag?.ToString() == m_sLocalIPv4)
                        {
                            m_uIndex = i;
                            break;
                        }
                    }
                    if (m_uIndex > -1)
                    {
                        this.cbxNetwork.SelectedIndex = m_uIndex;
                    }
                    else
                    {
                        this.cbxNetwork.SelectedIndex = 0;
                    }
                }
                if (this.cbxNetwork.SelectedIndex >= 0)
                {
                    this.m_pIPv4 = m_lKv[this.cbxNetwork.SelectedIndex];
                    Common.CommonParam.IP4 = this.m_pIPv4;
                }
            }

            if (m_pTimer == null)
            {
                m_pTimer = new Timer();
                m_pTimer.Interval = 1000;
                m_pTimer.Tick += (a, b) =>
                {
                    this.btnOK.Text = $"登陆中({this.m_uSecond})...";
                    this.m_uSecond++;
                };
            }

            //加载完毕
            this.m_bIsLoad = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_fDoLogin();
        }

        private void m_fDoLogin()
        {
            if (this.m_bDoLogin)
                return;

            ///缓存
            string account = this.txtAccount.Text;
            string pwd = this.txtPwd.Text;
            string server = this.cbxServer.Text;
            string database = this.txtDatabase.Text;
            string duid = this.txtDuid.Text;
            string dpwd = this.txtDpwd.Text;

            new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                try
                {
                    #region ***委托
                    Action m_pAction = () =>
                    {
                        this.m_fReLogin("", true);

                        //正则验证
                        {
                            Regex m_pRegex = new Regex(@"^(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)$");
                            if (m_pRegex.IsMatch(server))
                            {
                                m_cProfile.server = server;
                                m_cProfile.database = database;
                                m_cProfile.uid = duid;
                                m_cProfile.pwd = dpwd;
                            }
                            else
                            {
                                this.m_fReLogin("服务器无效");
                                return;
                            }
                        }

                        //用户名密码必填
                        {
                            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
                            {
                                this.m_fReLogin("请填写用户名和密码");
                                return;
                            }
                        }

                        DataTable m_pDataTable = Call_AgentUtil.CheckLogin(account, pwd);
                        if (m_pDataTable.Rows.Count <= 0)
                        {
                            this.m_fReLogin("用户名或密码错误");
                            return;
                        }

                        if (m_pDataTable.Rows[0]["Usable"].ToString() == "0")
                        {
                            this.m_fReLogin("账户被冻结");
                            return;
                        }

                        try
                        {
                            AgentInfo.AgentID = m_pDataTable.Rows[0]["ID"].ToString();
                            AgentInfo.UniqueID = m_pDataTable.Rows[0]["UniqueID"].ToString();
                            AgentInfo.AgentName = m_pDataTable.Rows[0]["AgentName"].ToString();
                            AgentInfo.LoginName = m_pDataTable.Rows[0]["LoginName"].ToString();
                            AgentInfo.LoginPassword = m_pDataTable.Rows[0]["LoginPassword"].ToString();
                            AgentInfo.ChannelID = int.Parse(string.IsNullOrEmpty(m_pDataTable.Rows[0]["ChannelID"].ToString()) ? "-1" : m_pDataTable.Rows[0]["ChannelID"].ToString());
                            AgentInfo.RoleID = Convert.ToInt32(m_pDataTable.Rows[0]["RoleID"]);
                            AgentInfo.RoleName = m_pDataTable.Rows[0]["RoleName"].ToString();
                            AgentInfo.TeamID = m_pDataTable.Rows[0]["TeamID"].ToString();
                            AgentInfo.TeamName = m_pDataTable.Rows[0]["TeamName"].ToString();
                            AgentInfo.LinkUser = m_pDataTable.Rows[0]["LinkUser"].ToString();
                            AgentInfo.LU_LoginName = m_pDataTable.Rows[0]["LU_LoginName"].ToString();
                            AgentInfo.LU_Password = m_pDataTable.Rows[0]["LU_Password"].ToString();

                            Call_AgentUtil.InsertLoginLog(m_pDataTable.Rows[0]["ID"].ToString(), CommonParam.GetLocalIpAddress, "1");
                            Call_AgentUtil.UpdateLoginState(m_pDataTable.Rows[0]["ID"].ToString(), "1");

                            ParamInfo.RememberUserName = AgentInfo.LoginName;

                            ///成功后保存IP
                            m_cProfile.m_fSetServerIP(Common.Encrypt.EncryptString(dpwd));

                            ///保存网卡IP
                            CommonParam.IP4 = this.m_pIPv4;
                            m_cProfile.localip = CommonParam.IP4?.tag?.ToString();

                            ///加载操作权限
                            m_cPower.m_fGetOperatePower();

                            ///直接加载是否全好显示
                            Model_v1.m_mOperate.m_bSeeNumber = m_cPower.Has(Model_v1.m_mOperate.noanswer_number_show);

                            ///加载数据权限
                            m_cPower.m_fGetDataPower();

                            this.DialogResult = DialogResult.OK;
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"[CenoCC][Login][m_fDoLogin][Action][Exception][{ex.Message}]");
                            Log.Instance.Debug(ex);

                            this.m_fReLogin("账户信息错误");
                            return;
                        }
                    };
                    #endregion

                    m_pAction();
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoCC][Login][m_fDoLogin][Exception][{ex.Message}]");
                    this.m_fReLogin("登陆错误请重试");
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

        private void m_fReLogin(string m_sMsg, bool m_bOn = false)
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                if (!m_bOn)
                {
                    this.m_uSecond = 1;
                    this.m_pTimer?.Stop();

                    this.lblTip.Text = m_sMsg;
                    this.m_bDoLogin = false;
                    this.btnOK.Enabled = true;
                    this.btnOK.Text = "登陆";
                }
                else
                {
                    this.m_bDoLogin = true;
                    this.btnOK.Enabled = false;
                    this.btnOK.Text = "登陆中...";
                    this.m_uSecond = 1;
                    this.m_pTimer?.Start();
                }

            }));
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (int.Parse(this.btnSetting.Tag.ToString()) == 0)
            {
                this.lblServer.Visible = true;
                this.lblDatabase.Visible = true;
                this.lblDuid.Visible = true;
                this.lblDpwd.Visible = true;
                this.cbxServer.Visible = true;
                this.txtDatabase.Visible = true;
                this.txtDuid.Visible = true;
                this.txtDpwd.Visible = true;

                this.btnSetting.Text = "精简";
                this.btnSetting.Tag = 1;
                this.Height = 515;
                this.CenterToScreen();
            }
            else
            {
                this.lblServer.Visible = false;
                this.lblDatabase.Visible = false;
                this.lblDuid.Visible = false;
                this.lblDpwd.Visible = false;
                this.cbxServer.Visible = false;
                this.txtDatabase.Visible = false;
                this.txtDuid.Visible = false;
                this.txtDpwd.Visible = false;

                this.btnSetting.Text = "高级";
                this.btnSetting.Tag = 0;
                this.Height = 323;
                this.CenterToScreen();
            }
        }

        private void cbxNetwork_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.m_bIsLoad) return;

                ComboBox combobox = (ComboBox)sender;
                List<Model_v1.M_kv> m_lKv = (List<Model_v1.M_kv>)(combobox.DataSource);
                this.m_pIPv4 = m_lKv[combobox.SelectedIndex];
                Common.CommonParam.IP4 = this.m_pIPv4;

                Log.Instance.Success($"[CenoCC][Login][cbxNetwork_SelectedIndexChanged][{this.m_pIPv4?.tag}]");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Login][cbxNetwork_SelectedIndexChanged][Exception][{ex.Message}]");
            }
        }

        private void cbxKvp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.m_bIsLoad) return;

                ///联动选择
                this.cbxServer.SelectedItem = this.cbxKvp.SelectedValue;

                Log.Instance.Success($"[CenoCC][Login][cbxKvp_SelectedIndexChanged][{this.m_pIPv4?.tag}]");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Login][cbxKvp_SelectedIndexChanged][Exception][{ex.Message}]");
            }
        }
    }
}

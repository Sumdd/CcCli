using DataBaseUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core_v1;
using Cmn_v1;
using Model_v1;
using Common;
using CenoSip;

namespace CenoCC {
    public partial class Args_Safe : _no {
        private bool m_IsLoad = true;
        public Args_Safe() {
            InitializeComponent();
            this.Init();
            this.m_IsLoad = false;
        }

        private void Init() {
            this.agentNameValue.Text = AgentInfo.AgentName;
            this.agentNameValue.Tag = AgentInfo.AgentName;
            this.loginNameValue.Text = AgentInfo.LoginName;
            this.loginNameValue.Tag = AgentInfo.LoginName;
        }

        private void btnYes_Click(object sender, EventArgs e) {
            if(this.agentNameValue.Tag.ToString() != this.agentNameValue.Text.Trim()) {
                var agentName = this.agentNameValue.Text.Trim();
                Call_AgentUtil.SetParamValueByName("AgentName", agentName);
                AgentInfo.AgentName = agentName;
            }
            if(this.loginNameValue.Tag.ToString() != this.loginNameValue.Text.Trim()) {
                var loginName = this.loginNameValue.Text.Trim();
                if(!Call_AgentUtil.Has("LoginName", loginName)) {
                    Call_AgentUtil.SetParamValueByName("LoginName", loginName);
                    AgentInfo.LoginName = loginName;
                } else {
                    Cmn.MsgError("登录名已存在");
                    this.loginNameValue.Text = this.loginNameValue.Tag.ToString();
                    return;
                }
            }
            if(this.updatePwdValue.Checked) {
                if(Encrypt.EncryptString(this.oldPwdValue.Text) == AgentInfo.LoginPassword) {
                    if(this.newPwdValue.Text == this.confirmNewPwdValue.Text) {
                        Call_AgentUtil.SetParamValueByName("LoginPassWord", Encrypt.EncryptString(this.newPwdValue.Text));
                    } else {
                        Cmn.MsgError("两次密码不一致");
                        return;
                    }
                } else {
                    Cmn.MsgError("原密码输入不一致");
                    return;
                }
            }
            Cmn.MsgOK("安全信息修改完成");
        }
    }
}

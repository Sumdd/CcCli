using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Common;
using DataBaseUtil;
using Core_v1;

namespace CenoCC {
    public partial class Login_yx : Form {

        Point mouseOff;
        bool leftFlag;

        public Login_yx() {
            InitializeComponent();
            InitForm();
        }

        private void InitForm() {
            if(ParamInfo.RememberUserNameFlag == "0") {
                this.RemindFlag_CB.Checked = false;
            } else {
                this.Login_Name_Txt.Text = ParamInfo.RememberUserName;
                int TabIndex = this.Login_Name_Txt.TabIndex;
                this.Login_Name_Txt.TabIndex = this.Login_Password_Txt.TabIndex;
                this.Login_Password_Txt.TabIndex = TabIndex;
                this.Login_Password_Txt.Focus();
                this.RemindFlag_CB.Checked = true;
            }
        }


        private void Cancel_Btn_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void Confirm_Btn_Click(object sender, EventArgs e) {
            BackgroundWorker CheckLoginBw = new BackgroundWorker();
            CheckLoginBw.DoWork += new DoWorkEventHandler(CheckLoginBw_DoWork);
            CheckLoginBw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CheckLoginBw_RunWorkerCompleted);
            CheckLoginBw.RunWorkerAsync();
        }

        void CheckLoginBw_DoWork(object sender, DoWorkEventArgs e) {
            DataTable UserInfoDT = Call_AgentUtil.CheckLogin(this.Login_Name_Txt.Text, this.Login_Password_Txt.Text);
            if(UserInfoDT.Rows.Count <= 0) {
                e.Result = "用户名或密码错误";
                return;
            }
            if(UserInfoDT.Rows[0]["Usable"].ToString() == "0") {
                e.Result = "账户被冻结，请联系管理员！！！";
                return;
            }
            try {
                AgentInfo.AgentID = UserInfoDT.Rows[0]["ID"].ToString();
                AgentInfo.AgentName = UserInfoDT.Rows[0]["AgentName"].ToString();
                AgentInfo.LoginName = UserInfoDT.Rows[0]["LoginName"].ToString();
                AgentInfo.LoginPassword = UserInfoDT.Rows[0]["LoginPassword"].ToString();
                //_AgentData.CallOutNum = UserInfoDT.Rows[0]["LoginPassword"].ToString();
                AgentInfo.ChannelID = int.Parse(string.IsNullOrEmpty(UserInfoDT.Rows[0]["ChannelID"].ToString()) ? "-1" : UserInfoDT.Rows[0]["ChannelID"].ToString());
                AgentInfo.RoleID = Convert.ToInt32(UserInfoDT.Rows[0]["RoleID"]);
                AgentInfo.RoleName = UserInfoDT.Rows[0]["RoleName"].ToString();
                AgentInfo.TeamName = UserInfoDT.Rows[0]["TeamName"].ToString();
                AgentInfo.LinkUser = UserInfoDT.Rows[0]["LinkUser"].ToString();
                AgentInfo.LU_LoginName = UserInfoDT.Rows[0]["LU_LoginName"].ToString();
                AgentInfo.LU_Password = UserInfoDT.Rows[0]["LU_Password"].ToString();
            } catch {
                e.Result = "账户信息错误，请联系管理员！！！";
                return;
            } finally {
                Call_AgentUtil.InsertLoginLog(UserInfoDT.Rows[0]["ID"].ToString(), CommonParam.GetLocalIpAddress, "1");
                Call_AgentUtil.UpdateLoginState(UserInfoDT.Rows[0]["ID"].ToString(), "1");
            }
            e.Result = "";
        }

        void CheckLoginBw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if((e.Result).ToString() == "") {
                if(this.RemindFlag_CB.Checked)
                    ParamInfo.RememberUserName = this.Login_Name_Txt.Text;

                this.DialogResult = DialogResult.OK;
                Log.Instance.Success($"[CenoCC][Login_Frm][CheckLoginBw_RunWorkerCompleted][{this.Login_Name_Txt.Text}登录成功]");
                this.Close();
                return;
            }
            ParamInfo.RememberUserName = this.Login_Name_Txt.Text;
            this.ErrorInfo_Lab.Text = (e.Result).ToString();

            this.Confirm_Btn._Timers.Enabled = false;
            this.Confirm_Btn.Enabled = true;
            this.Confirm_Btn.Text = "登录";
        }


        private void ResetPsw_Lbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if(string.IsNullOrEmpty(this.Login_Name_Txt.Text)) {
                this.ErrorInfo_Lab.Text = "请输入用户名";
                return;
            }

            if(MessageBox.Show("确认将密码重置为初始密码吗？？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                Call_AgentUtil.ResetUserPsw(this.Login_Name_Txt.Text);
            }
        }

        private void RemindFlag_CB_CheckedChanged(object sender, EventArgs e) {
            ParamInfo.RememberUserNameFlag = this.RemindFlag_CB.Checked ? "1" : "0";
        }

        private void Login_Name_Txt_TextChanged(object sender, EventArgs e) {
            this.ErrorInfo_Lab.Text = "";
        }

        private void Login_Password_Txt_TextChanged(object sender, EventArgs e) {
            this.ErrorInfo_Lab.Text = "";
        }

        private void Login_Password_Txt_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == '\r') {
                BackgroundWorker CheckLoginBw = new BackgroundWorker();
                CheckLoginBw.DoWork += new DoWorkEventHandler(CheckLoginBw_DoWork);
                CheckLoginBw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CheckLoginBw_RunWorkerCompleted);
                CheckLoginBw.RunWorkerAsync();

                this.Confirm_Btn.Text = "登录中,请稍后...";
                this.Confirm_Btn.Enabled = false;

                e.Handled = true;
            }
        }
    }
}

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

namespace CenoCC
{
    public partial class Args_CallerDisplay : _no
    {
        private bool m_bFirst = true;
        public Args_CallerDisplay()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            List<string> m_lDefaultStrings = new List<string>() { "1", "0", "1" };
            List<string> m_lStrings = null;
            try
            {
                m_lStrings = Call_ClientParamUtil.ShowStyleString.Split(',').ToList();
                if (m_lStrings.Count != 3)
                {
                    m_lStrings = m_lDefaultStrings;
                }
            }
            catch (Exception ex)
            {
                m_lStrings = m_lDefaultStrings;
                Log.Instance.Error($"[CenoCC][Args_CallerDisplay][Init][Exception][{ex.Message}]");
            }

            this.ckbShowNumber.Checked = m_lStrings[0] == "1";
            this.ckbShowRealName.Checked = m_lStrings[1] == "1";
            this.ckbShowAddress.Checked = m_lStrings[2] == "1";

            if (!Call_ParamUtil.m_bUseHomeSearch)
            {
                this.ckbShowRealName.Text += "(该功能已禁用)";
            }

            this.cbxSysMsgCall.Checked = Call_ClientParamUtil.m_bIsSysMsgCall;

            ///呼叫转移
            this.cbxIsinlimit_2.Checked = Call_ClientParamUtil.m_bisinlimit_2;
            this.txtInlimit_2number.Text = Call_ClientParamUtil.m_sinlimit_2number;
            this.inlimit_2starttime.Text = Call_ClientParamUtil.m_sinlimit_2starttime;
            this.inlimit_2endtime.Text = Call_ClientParamUtil.m_sinlimit_2endtime;

            ///设定星期几
            int m_uWhatDay = Call_ClientParamUtil.m_uinlimit_2whatday;
            for (int i = 0; i < this.inlimit_2whatday.Items.Count; i++)
            {
                this.inlimit_2whatday.SetItemChecked(i, (m_uWhatDay & (int)(Math.Pow(2, i))) > 0 ? true : false);
            }

            m_bFirst = false;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            List<string> m_lStrings = new List<string>();
            m_lStrings.Add(this.ckbShowNumber.Checked ? "1" : "0");
            m_lStrings.Add(this.ckbShowRealName.Checked ? "1" : "0");
            m_lStrings.Add(this.ckbShowAddress.Checked ? "1" : "0");

            IEnumerable<string> m_aStrings = m_lStrings.Where(q => q == "1");

            if (m_aStrings != null && m_aStrings.Count() > 0)
            {
                string m_sStrings = string.Join(",", m_lStrings.ToArray());
                Call_ClientParamUtil.ShowStyleString = m_sStrings;

                ///设定值,重新查询
                Call_ParamUtil._m_sUseHomeSearch = null;

                ///修改显示文字
                if (Call_ParamUtil.m_bUseHomeSearch)
                {
                    this.ckbShowRealName.Text = this.ckbShowRealName.Text.Replace("(该功能已禁用)", "");
                }
                else
                {
                    if (!this.ckbShowRealName.Text.Contains("(该功能已禁用)"))
                        this.ckbShowRealName.Text += "(该功能已禁用)";
                }

                ///载入缓存中
                if (Call_ParamUtil.m_bUseHomeSearch && this.ckbShowRealName.Checked) Call_ClientParamUtil.m_bName = true;
                else Call_ClientParamUtil.m_bName = false;

                ///来电的系统提示
                Call_ClientParamUtil.m_bIsSysMsgCall = this.cbxSysMsgCall.Checked;

                ///呼叫转移
                Call_ClientParamUtil.m_bisinlimit_2 = this.cbxIsinlimit_2.Checked;
                Call_ClientParamUtil.m_sinlimit_2number = this.txtInlimit_2number.Text;
                Call_ClientParamUtil.m_sinlimit_2starttime = this.inlimit_2starttime.Text;
                Call_ClientParamUtil.m_sinlimit_2endtime = this.inlimit_2endtime.Text;

                ///得到选中的星期的代数和
                int m_uWhatDay = 0;
                for (int i = 0; i < this.inlimit_2whatday.Items.Count; i++)
                {
                    if (this.inlimit_2whatday.GetItemChecked(i))
                    {
                        m_uWhatDay += (int)(Math.Pow(2, i));
                    }
                }
                Call_ClientParamUtil.m_uinlimit_2whatday = m_uWhatDay;

                ///需要修改对应人员的内容内转缓存
                WebSocket_v1.InWebSocketMain.Send(CenoSocket.M_Send._zdwh($"UpdUa_{Common.AgentInfo.AgentID}"));

                Cmn.MsgOK($"修改及发送内转缓存更新命令成功");
                Log.Instance.Success($"[CenoCC][Args_CallerDisplay][btnYes_Click][修改及发送内转缓存更新命令成功:{m_sStrings}]");
            }
            else
            {
                Log.Instance.Fail($"[CenoCC][Args_CallerDisplay][btnYes_Click][请至少选择一项作为显示]");
                Cmn.MsgWran("请至少选择一项作为显示");
            }
        }

        private void ckbShowNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bFirst)
                return;
            List<string> m_lStrings = new List<string>();
            m_lStrings.Add(this.ckbShowNumber.Checked ? "1" : "0");
            m_lStrings.Add(this.ckbShowRealName.Checked ? "1" : "0");
            m_lStrings.Add(this.ckbShowAddress.Checked ? "1" : "0");
            Call_ClientParamUtil.ShowStyleString = string.Join(",", m_lStrings.ToArray());
        }

        private void ckbShowRealName_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bFirst)
                return;
            List<string> m_lStrings = new List<string>();
            m_lStrings.Add(this.ckbShowNumber.Checked ? "1" : "0");
            m_lStrings.Add(this.ckbShowRealName.Checked ? "1" : "0");
            m_lStrings.Add(this.ckbShowAddress.Checked ? "1" : "0");
            Call_ClientParamUtil.ShowStyleString = string.Join(",", m_lStrings.ToArray());
        }

        private void ckbShowAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bFirst)
                return;
            List<string> m_lStrings = new List<string>();
            m_lStrings.Add(this.ckbShowNumber.Checked ? "1" : "0");
            m_lStrings.Add(this.ckbShowRealName.Checked ? "1" : "0");
            m_lStrings.Add(this.ckbShowAddress.Checked ? "1" : "0");
            Call_ClientParamUtil.ShowStyleString = string.Join(",", m_lStrings.ToArray());
        }

        private void cbxSysMsgCall_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bFirst)
                return;
            Call_ClientParamUtil.m_bIsSysMsgCall = this.cbxSysMsgCall.Checked;
        }
    }
}

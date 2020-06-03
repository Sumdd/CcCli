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

namespace CenoCC
{
    public partial class Args_DialSet : _no
    {
        private bool m_bFirst = true;
        public Args_DialSet()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            this.ckbAutoAddDialNumFlag.Checked = Call_ClientParamUtil.m_bAutoAddNumDialFlag;
            this.cbxIsUseCopy.Checked = Call_ClientParamUtil.m_bIsUseCopy;
            this.cbxIsUseCopyNumber.Checked = Call_ClientParamUtil.m_bIsUseCopyNumber;
            this.cbxIsUseShare.Checked = Call_ClientParamUtil.m_bIsUseShare;
            this.cbxIsUseSpRandom.Checked = Call_ClientParamUtil.m_bIsUseSpRandom;
            this.cbxIsUseSpRandomTimeout.Checked = Call_ClientParamUtil.m_bIsUseSpRandomTimeout;
            this.nupWait.Value = Call_ClientParamUtil.m_uShareWait;
            this.ckbQNRegexNumber.Checked = Call_ClientParamUtil.m_bQNRegexNumber;
            m_bFirst = false;
        }

        private void ckbAutoAddDialNumFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bFirst) return;
            try
            {
                Call_ClientParamUtil.m_bAutoAddNumDialFlag = this.ckbAutoAddDialNumFlag.Checked;
                Log.Instance.Success($"[CenoCC][Args_DialSet][ckbAutoAddDialNumFlag_CheckedChanged][{(Call_ClientParamUtil.m_bAutoAddNumDialFlag ? "启用" : "禁用")}自动根据区号加拨前缀]");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Args_DialSet][ckbAutoAddDialNumFlag_CheckedChanged][Exception][{ex.Message}]");
            }
        }

        private void cbxIsUseCopy_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bFirst) return;
            try
            {
                Call_ClientParamUtil.m_bIsUseCopy = this.cbxIsUseCopy.Checked;
                Log.Instance.Success($"[CenoCC][Args_DialSet][cbxIsUseCopy_CheckedChanged][{(Call_ClientParamUtil.m_bIsUseCopy ? "启用" : "禁用")}复制拨号]");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Args_DialSet][cbxIsUseCopy_CheckedChanged][Exception][{ex.Message}]");
            }
        }

        private void cbxIsUseShare_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bFirst) return;
            try
            {
                Call_ClientParamUtil.m_bIsUseShare = this.cbxIsUseShare.Checked;
                Log.Instance.Success($"[CenoCC][Args_DialSet][cbxIsUseShare_CheckedChanged][{(Call_ClientParamUtil.m_bIsUseShare ? "启用" : "禁用")}使用共享号码]");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Args_DialSet][cbxIsUseShare_CheckedChanged][Exception][{ex.Message}]");
            }
        }

        private void cbxIsUseSpRandom_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bFirst) return;
            try
            {
                Call_ClientParamUtil.m_bIsUseSpRandom = this.cbxIsUseSpRandom.Checked;
                Log.Instance.Success($"[CenoCC][Args_DialSet][cbxIsUseSpRandom_CheckedChanged][{(Call_ClientParamUtil.m_bIsUseSpRandom ? "启用" : "禁用")}使用专线号码轮呼]");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Args_DialSet][cbxIsUseSpRandom_CheckedChanged][Exception][{ex.Message}]");
            }
        }

        private void nupWait_Leave(object sender, EventArgs e)
        {

            try
            {
                Call_ClientParamUtil.m_uShareWait = Convert.ToInt32(this.nupWait.Value);
                Log.Instance.Success($"[CenoCC][Args_DialSet][cbxIsUseSpRandom_CheckedChanged][号码池选择等待时间设置为{Call_ClientParamUtil.m_uShareWait}]");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Args_DialSet][cbxIsUseSpRandom_CheckedChanged][Exception][{ex.Message}]");
            }
        }

        private void cbxIsUseSpRandomTimeout_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bFirst) return;
            try
            {
                Call_ClientParamUtil.m_bIsUseSpRandomTimeout = this.cbxIsUseSpRandomTimeout.Checked;
                Log.Instance.Success($"[CenoCC][Args_DialSet][cbxIsUseSpRandomTimeout_CheckedChanged][{(Call_ClientParamUtil.m_bIsUseSpRandomTimeout ? "启用" : "禁用")}超时后自动专线轮呼]");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Args_DialSet][cbxIsUseSpRandomTimeout_CheckedChanged][Exception][{ex.Message}]");
            }
        }

        private void cbxIsUseCopyNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bFirst) return;
            try
            {
                Call_ClientParamUtil.m_bIsUseCopyNumber = this.cbxIsUseCopyNumber.Checked;
                Log.Instance.Success($"[CenoCC][Args_DialSet][cbxIsUseCopyNumber_CheckedChanged][{(Call_ClientParamUtil.m_bIsUseCopyNumber ? "启用" : "禁用")}加载号码至复制弹出框]");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Args_DialSet][cbxIsUseCopyNumber_CheckedChanged][Exception][{ex.Message}]");
            }
        }

        private void ckbQNRegexNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bFirst) return;
            try
            {
                Call_ClientParamUtil.m_bQNRegexNumber = this.ckbQNRegexNumber.Checked;
                Log.Instance.Success($"[CenoCC][Args_DialSet][ckbQNRegexNumber_CheckedChanged][{(Call_ClientParamUtil.m_bQNRegexNumber ? "启用" : "禁用")}兼容32位加密号码]");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Args_DialSet][ckbQNRegexNumber_CheckedChanged][Exception][{ex.Message}]");
            }
        }
    }
}

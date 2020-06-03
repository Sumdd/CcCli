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
using WebBrowser;

namespace CenoCC {
    public partial class Args_Web : _no {
        public Args_Web() {
            InitializeComponent();
            this.Init();
        }

        private void Init() {
            var _homeUrl = Call_ClientParamUtil.GetParamValueByName("HomeUrl");
            this.HomeUrlValue.Text = _homeUrl;
            var _extendUrl = Call_ClientParamUtil.GetParamValueByName("ExtendUrl");
            this.ExtendUrlValue.Text = _extendUrl;
            var _OpenHomeUrl = Call_ClientParamUtil.GetParamValueByName("AutoOpenPage");
            this.OpenHomeUrlValue.Checked = Cmn.IntToBoolean(_OpenHomeUrl);
            var _OpenExtendUrl = Call_ClientParamUtil.GetParamValueByName("AutoOpenDial");
            this.OpenExtendUrlValue.Checked = Cmn.IntToBoolean(_OpenExtendUrl);
            var _quickWebsite = Call_ClientParamUtil.GetParamValueByName("QuickWebsite");
            this.QuickWebsiteValue.Text = _quickWebsite;
        }

        private void btnReset_Click(object sender, EventArgs e) {
            this.Init();
        }

        private void btnYes_Click(object sender, EventArgs e) {
            try {
                BrowserParam.HomeUrl = this.HomeUrlValue.Text;
                Call_ClientParamUtil.SetParamValueByName("HomeUrl", this.HomeUrlValue.Text);
                BrowserParam.ExtendUrl = this.ExtendUrlValue.Text;
                Call_ClientParamUtil.SetParamValueByName("ExtendUrl", this.ExtendUrlValue.Text);
                BrowserParam.AutoOpenPage = Cmn.BooleanToInt(this.OpenHomeUrlValue.Checked).ToString();
                Call_ClientParamUtil.SetParamValueByName("AutoOpenPage", BrowserParam.AutoOpenPage);
                Call_ClientParamUtil.SetParamValueByName("AutoOpenDial", Cmn.BooleanToInt(this.OpenExtendUrlValue.Checked));
                MinChat.isRightNeedLoad = true;
                Call_ClientParamUtil.SetParamValueByName("QuickWebsite", this.QuickWebsiteValue.Text);
                Call_ParamUtil._m_bIsUseHttpShare = null;
                Call_ParamUtil._m_sHttpShareUrl = null;
                Log.Instance.Success($"[CenoCC][Args_Web][btnYes_Click][修改催收系统配置成功]");
                Cmn.MsgOK("修改成功!");
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][Args_Web][btnYes_Click][修改催收系统配置错误:{ex.Message}]");
            }
        }
    }
}

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

namespace CenoCC {
    public partial class Args_NoAnswer : _no {
        public Args_NoAnswer() {
            InitializeComponent();
            this.GetConf();
        }

        private void GetConf() {
            try {
                string _noAnswerConf = Call_ClientParamUtil.GetParamValueByName("NoAnswerConf");
                if(!string.IsNullOrWhiteSpace(_noAnswerConf) && _noAnswerConf.Contains(',')) {
                    var s_str = _noAnswerConf.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    Call_ClientParamUtil.noAnswerDay = Convert.ToInt32(s_str[0]);
                    Call_ClientParamUtil.noAnswerUse = Convert.ToBoolean(s_str[1]);
                } else {
                    Call_ClientParamUtil.noAnswerDay = 2;
                    Call_ClientParamUtil.noAnswerUse = true;
                }
            } catch(Exception ex) {
                Call_ClientParamUtil.noAnswerDay = 2;
                Call_ClientParamUtil.noAnswerUse = true;
                Log.Instance.Error($"[CenoCC][Args_Answer][GetConf][Exception][{ex.Message}]");
            }
            this.noAnswerDayValue.Value = Convert.ToDecimal(Call_ClientParamUtil.noAnswerDay);
            this.noAnswerUseValue.Checked = Call_ClientParamUtil.noAnswerUse;
        }

        private void btnYes_Click(object sender, EventArgs e) {
            try {
                Call_ClientParamUtil.noAnswerDay = Convert.ToInt32(this.noAnswerDayValue.Value);
                Call_ClientParamUtil.noAnswerUse = this.noAnswerUseValue.Checked;
                Call_ClientParamUtil.SetParamValueByName("NoAnswerConf", $"{Call_ClientParamUtil.noAnswerDay},{Call_ClientParamUtil.noAnswerUse}");
                Log.Instance.Success($"[CenoCC][Args_Web][btnYes_Click][修改未接来电配置成功]");
                Cmn_v1.Cmn.MsgOK("修改成功!");
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][Args_Answer][btnYes_Click][Exception][{ex.Message}]");
            }
        }
    }
}

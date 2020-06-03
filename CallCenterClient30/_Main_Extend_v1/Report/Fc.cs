using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataBaseUtil;

namespace CenoCC
{
    public partial class Fc : _metorform
    {
        public Fc()
        {
            InitializeComponent();
            this.m_fInit();
        }

        private void m_fInit()
        {
            Call_ClientParamUtil.m_fRecSetting();
            this.cbxSwitch.DataSource = Call_ClientParamUtil.m_lSwitch;
            this.cbxSwitch.SelectedItem = Call_ClientParamUtil.m_sSwitch;
            this.ckbNever.Checked = false;
        }

        private void btnOKOnce_Click(object sender, EventArgs e)
        {
            Call_ClientParamUtil.m_sSwitch = this.cbxSwitch.Text;
            Call_ClientParamUtil.m_bSwitch = !this.ckbNever.Checked;
            this.DialogResult = DialogResult.OK;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Call_ClientParamUtil.m_sSwitch = this.cbxSwitch.Text;
            Call_ClientParamUtil.m_bSwitch = !this.ckbNever.Checked;
            Call_ClientParamUtil.m_sRecSetting = $"{(Call_ClientParamUtil.m_bSwitch ? "1" : "0")},{Call_ClientParamUtil.m_sSwitch},{string.Join("|", Call_ClientParamUtil.m_lSwitch.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray())}";
            this.DialogResult = DialogResult.OK;
        }
    }
}

using CenoSip;
using Core_v1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CenoCC {
    public partial class Args_Device : _no {
        private bool m_IsLoad = true;
        public Args_Device() {
            InitializeComponent();
            this.AudioInBinding();
            this.AudioOutBinding();
            this.m_IsLoad = false;
        }
        private void AudioInBinding() {
            ysqcombo.DataSource = SipParam.AudioOutDevices;
            ysqcombo.ValueMember = "Channels";
            ysqcombo.DisplayMember = "Name";
        }
        private void AudioOutBinding() {
            maccombo.DataSource = SipParam.AudioInDevices;
            maccombo.ValueMember = "Channels";
            maccombo.DisplayMember = "Name";
        }
        private void ysqcombo_SelectedIndexChanged(object sender, EventArgs e) {
            if(this.m_IsLoad)
                return;
            try {
                ComboBox combobox = (ComboBox)sender;
                SipParam.m_pAudioOutDevice = SipParam.AudioOutDevices[combobox.SelectedIndex];
                Log.Instance.Error($"[CenoCC][ArgsfrmSimple][ysqcombo_SelectedIndexChanged][设置扬声器设备:{combobox.Text}]");
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][ArgsfrmSimple][ysqcombo_SelectedIndexChanged][{ex.Message}]");
            }
        }
        private void maccombo_SelectedIndexChanged(object sender, EventArgs e) {
            if(this.m_IsLoad)
                return;
            try {
                ComboBox combobox = (ComboBox)sender;
                SipParam.m_pAudioInDevice = SipParam.AudioInDevices[combobox.SelectedIndex];
                Log.Instance.Error($"[CenoCC][ArgsfrmSimple][ysqcombo_SelectedIndexChanged][设置麦克风设备:{combobox.Text}]");
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][ArgsfrmSimple][ysqcombo_SelectedIndexChanged][{ex.Message}]");
            }
        }
    }
}

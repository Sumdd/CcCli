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

namespace CenoCC
{
    public partial class Args_MultiNetwork : _no
    {
        private bool m_IsLoad = true;
        public Args_MultiNetwork()
        {
            InitializeComponent();
            this.Init();
            this.m_IsLoad = false;
        }

        private void Init()
        {
            var _list = CommonParam.GetAllNetwork();
            this.networkValue.DataSource = _list;
            this.networkValue.ValueMember = "key";
            this.networkValue.DisplayMember = "value";
            var _index = 0;
            if (CommonParam.IP4 != null)
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    if (_list[i].key.ToString() == CommonParam.IP4.key.ToString())
                    {
                        _index = i;
                        break;
                    }
                }
                this.networkValue.SelectedIndex = _index;
            }
        }

        private void networkValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_IsLoad)
                return;
            ComboBox combobox = (ComboBox)sender;
            BackgroundWorker _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += (object o2, DoWorkEventArgs e2) =>
            {
                var _list = (List<M_kv>)(combobox.DataSource);
                if (CommonParam.IP4 != null && CommonParam.IP4.key.ToString() == _list[combobox.SelectedIndex].key.ToString())
                {
                    Log.Instance.Success($"[CenoCC][Args_MultiNetwork][networkValue_SelectedIndexChanged][多网卡设置成功,未变动,{CommonParam.IP4.value}]");
                }
                else
                {
                    CommonParam.IP4 = _list[combobox.SelectedIndex];
                    m_cProfile.localip = CommonParam.IP4?.tag?.ToString();
                    Log.Instance.Success($"[CenoCC][Args_MultiNetwork][networkValue_SelectedIndexChanged][多网卡设置成功,{CommonParam.IP4.value}]");
                    //if(this.reRegNowValue.Checked) {
                    //Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_LOAD_STATUS_UNAVAILABLE, (IntPtr)0, (IntPtr)2000);
                    //}
                }
            };
            _backgroundWorker.RunWorkerAsync();
        }
    }
}

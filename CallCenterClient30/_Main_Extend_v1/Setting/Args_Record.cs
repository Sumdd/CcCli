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
    public partial class Args_Record : _no
    {
        private bool m_fLoad = true;
        public Args_Record()
        {
            InitializeComponent();
            this.GetSaveRecordPath();

            //录音格式转换
            Call_ClientParamUtil.m_fRecSetting();
            this.ckbSwitch.Checked = Call_ClientParamUtil.m_bSwitch;
            this.cbxSwitch.DataSource = Call_ClientParamUtil.m_lSwitch;
            this.cbxSwitch.SelectedItem = Call_ClientParamUtil.m_sSwitch;

            //线程数
            this.nudThread.Value = Call_ClientParamUtil.m_uHttpLoadThread;

            this.m_fLoad = false;
        }

        private void GetSaveRecordPath()
        {
            try
            {
                string _recordSavePath = Call_ClientParamUtil.GetParamValueByName("SaveRecordPath");
                if (!string.IsNullOrWhiteSpace(_recordSavePath))
                {
                    this.recordSavePath.Text = _recordSavePath;
                }
                else
                {
                    this.recordSavePath.Text = "未设置默认录音下载路径,请选择";
                }
            }
            catch (Exception ex)
            {
                this.recordSavePath.Text = "获取默认录音保存路径失败";
                Log.Instance.Error($"[CenoCC][Args_Record][GetRecordPath][Exception][{ex.Message}]");
            }
        }

        private void choosePathbtn_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.folderBrowserDialog.ShowDialog())
            {
                string _recordSavePath = this.folderBrowserDialog.SelectedPath.Replace("\\", "/");
                this.recordSavePath.Text = _recordSavePath;
                Call_ClientParamUtil.SetParamValueByName("SaveRecordPath", _recordSavePath);
                Log.Instance.Success($"[CenoCC][Args_Record][choosePathbtn_Click][修改默认录音下载路径:{_recordSavePath}]");
            }
        }

        private void btnPathNull_Click(object sender, EventArgs e)
        {
            if (Cmn_v1.Cmn.MsgQ("确定要清空默认录音下载路径吗"))
            {
                Call_ClientParamUtil.SetParamValueByName("SaveRecordPath", "");
                this.recordSavePath.Text = "未设置默认录音下载路径,请选择";
                Log.Instance.Success($"[CenoCC][Args_Record][choosePathbtn_Click][清空默认录音下载路径]");
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            convert m_cConvert = new convert();
            m_cConvert.TopMost = true;
            m_cConvert.Show();
        }

        private void cbxSwitch_DropDownClosed(object sender, EventArgs e)
        {
            this.m_fSetSwitch();
        }

        private void ckbSwitch_Click(object sender, EventArgs e)
        {
            this.m_fSetSwitch();
        }

        private void m_fSetSwitch()
        {
            try
            {
                Call_ClientParamUtil.m_bSwitch = this.ckbSwitch.Checked;
                Call_ClientParamUtil.m_sSwitch = this.cbxSwitch.Text;
                Call_ClientParamUtil.m_sRecSetting = $"{(Call_ClientParamUtil.m_bSwitch ? "1" : "0")},{Call_ClientParamUtil.m_sSwitch},{string.Join("|", Call_ClientParamUtil.m_lSwitch.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray())}";
                Log.Instance.Success($"[CenoCC][Args_Record][m_fSetSwitch][{Call_ClientParamUtil.m_sRecSetting}]");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Args_Record][m_fSetSwitch][Exception][{ex.Message}]");
            }
        }

        private void nudThread_ValueChanged(object sender, EventArgs e)
        {
            if (this.m_fLoad) return;
            try
            {
                Call_ClientParamUtil.m_uHttpLoadThread = this.nudThread.Value;
                Log.Instance.Success($"[CenoCC][Args_Record][nudThread_ValueChanged][{Call_ClientParamUtil.m_uHttpLoadThread}]");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Args_Record][nudThread_ValueChanged][Exception][{ex.Message}]");
            }
        }
    }
}

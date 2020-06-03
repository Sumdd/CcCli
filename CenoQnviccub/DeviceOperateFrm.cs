using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Common;
using CenoQnviccub;

namespace CenoCC
{
	public partial class DeviceOperateFrm : Form
	{


		private const short Def_Ch = 0;
		private bool CurrentDeviceIsEnabled;

		public DeviceOperateFrm()
		{
			InitializeComponent();
			//InitForm();
		}

		private void InitForm()
		{
			//if (this.CurrentMainFormObj.GetCurrentPhoneDeviceIsEnable)
			//{
			//    this.button1.Text = "关闭设备";
			//}


			////初始化设备状态
			//InitDeviceInfo();
		}

		private void InitDeviceInfo()
		{
			//#region  //加载音量选项
			//try
			//{
			//    int valueOfVoice = BriSDKLib.QNV_GetParam(Def_Ch, BriSDKLib.QNV_PARAM_AM_MIC);
			//    if (valueOfVoice < 0)
			//    {
			//        LogFile.Write("获取电话盒设备参数 麦克风音量 失败！错误代码：" + valueOfVoice, "DeviceError");
			//    }
			//    else
			//    {
			//        this.Mic_Voice_TrackB.Value = valueOfVoice;
			//        this.Mic_Voice_TrackB.ValueChanged += new EventHandler(Set_Voice_TrackB_ValueChanged);
			//    }

			//    valueOfVoice = BriSDKLib.QNV_GetParam(Def_Ch, BriSDKLib.QNV_PARAM_AM_SPKOUT);
			//    if (valueOfVoice < 0)
			//    {
			//        LogFile.Write("获取电话盒设备参数 耳机音量 失败！错误代码：" + valueOfVoice, "DeviceError");
			//    }
			//    else
			//    {
			//        this.SpaOUT_Voice_TrackB.Value = valueOfVoice;
			//        this.SpaOUT_Voice_TrackB.ValueChanged += new EventHandler(Set_Voice_TrackB_ValueChanged);
			//    }

			//    valueOfVoice = BriSDKLib.QNV_GetParam(Def_Ch, BriSDKLib.QNV_PARAM_AM_LINEIN);
			//    if (valueOfVoice < 0)
			//    {
			//        LogFile.Write("获取电话盒设备参数 线路录音音量 失败！错误代码：" + valueOfVoice, "DeviceError");
			//    }
			//    else
			//    {
			//        this.Record_Voice_TrackB.Value = valueOfVoice;
			//        this.Record_Voice_TrackB.ValueChanged += new EventHandler(Set_Voice_TrackB_ValueChanged);
			//    }

			//    valueOfVoice = BriSDKLib.QNV_GetParam(Def_Ch, BriSDKLib.QNV_PARAM_AM_LINEOUT);
			//    if (valueOfVoice < 0)
			//    {
			//        LogFile.Write("获取电话盒设备参数 播放到线路语音音量 失败！错误代码：" + valueOfVoice, "DeviceError");
			//    }
			//    else
			//    {
			//        this.OnLine_Voice_TrackB.Value = valueOfVoice;
			//        this.OnLine_Voice_TrackB.ValueChanged += new EventHandler(Set_Voice_TrackB_ValueChanged);
			//    }

			//    valueOfVoice = BriSDKLib.QNV_GetParam(Def_Ch, BriSDKLib.QNV_PARAM_AM_DOPLAY);
			//    if (valueOfVoice < 0)
			//    {
			//        LogFile.Write("获取电话盒设备参数 喇叭音量 失败！错误代码：" + valueOfVoice, "DeviceError");
			//    }
			//    else
			//    {
			//        this.Speaker_Voice_TrackB.Value = valueOfVoice;
			//        this.Speaker_Voice_TrackB.ValueChanged += new EventHandler(Set_Voice_TrackB_ValueChanged);
			//    }
			//}
			//catch (Exception ex)
			//{
			//    LogFile.Write("获取电话盒音量相关参数时报错 " + ex.Message, "Error");
			//}


			//#endregion
		}

		private void Set_Voice_TrackB_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				TrackBar bar = (TrackBar)sender;
				string flag = bar.Tag.ToString();
				int valueOfVoice = bar.Value;
				int result = -1;
				switch (flag)
				{
					case "mic":
						result = BriSDKLib.QNV_SetParam(Def_Ch, BriSDKLib.QNV_PARAM_AM_MIC, valueOfVoice);
						DeviceParam.D_MICVoiceValue = valueOfVoice.ToString();
						break;
					case "speaker":
						result = BriSDKLib.QNV_SetParam(Def_Ch, BriSDKLib.QNV_PARAM_AM_SPKOUT, valueOfVoice);
						DeviceParam.D_SpkOutVoiceValue = valueOfVoice.ToString();
						break;
					case "doplay":
						result = BriSDKLib.QNV_SetParam(Def_Ch, BriSDKLib.QNV_PARAM_AM_DOPLAY, valueOfVoice);
						DeviceParam.D_DoPlayVoiceValue = valueOfVoice.ToString();
						break;
					case "lineout":
						result = BriSDKLib.QNV_SetParam(Def_Ch, BriSDKLib.QNV_PARAM_AM_LINEOUT, valueOfVoice);
						DeviceParam.D_LineOutVoiceValue = valueOfVoice.ToString();
						break;
					case "linein":
						result = BriSDKLib.QNV_SetParam(Def_Ch, BriSDKLib.QNV_PARAM_AM_LINEIN, valueOfVoice);
						DeviceParam.D_LineInVoiceValue = valueOfVoice.ToString();
						break;
					default: break;
				}
				if (result <= 0)
				{
					LogFile.Write(typeof(DeviceOperateFrm), LOGLEVEL.ERROR, "set device param " + flag + " error", new Exception("error code：" + result));
				}
			}
			catch (Exception ex)
			{
				LogFile.Write(typeof(DeviceOperateFrm), LOGLEVEL.ERROR, "set device param error " + ex.Message, ex);
			}

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			if (btn.Text == "打开设备")
			{
				//this.CurrentMainFormObj.OpenDevInfo();
				btn.Text = "关闭设备";
			}
			else if (btn.Text == "关闭设备")
			{
				//this.CurrentMainFormObj.CloseDevInfo();
				btn.Text = "打开设备";
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			int value = this.checkBox1.Checked ? 1 : 0;
			int result = BriSDKLib.QNV_SetDevCtrl(Def_Ch, BriSDKLib.QNV_CTRL_DOMICTOLINE, value);
			if (result <= 0)
			{
				LogFile.Write(typeof(DeviceOperateFrm), LOGLEVEL.ERROR, "set device mic error", new Exception("error code：" + result));
			}
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			int value = this.checkBox2.Checked ? 1 : 0;
			int result = BriSDKLib.QNV_SetDevCtrl(Def_Ch, BriSDKLib.QNV_CTRL_DOLINETOSPK, value);
			if (result <= 0)
			{
				LogFile.Write(typeof(DeviceOperateFrm), LOGLEVEL.ERROR, "set device sound to mic error", new Exception("error code：" + result));
			}
		}

		private void checkBox3_CheckedChanged(object sender, EventArgs e)
		{
			int value = this.checkBox3.Checked ? 1 : 0;
			int result = BriSDKLib.QNV_SetDevCtrl(Def_Ch, BriSDKLib.QNV_CTRL_DOLINETOSPK, value);
			if (result <= 0)
			{
				LogFile.Write(typeof(DeviceOperateFrm), LOGLEVEL.ERROR, "set device speaker operate error", new Exception("error code：" + result));
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			int result = BriSDKLib.QNV_SetDevCtrl(Def_Ch, BriSDKLib.QNV_CTRL_DOHOOK, 1);
			if (result <= 0)
			{
				LogFile.Write(typeof(DeviceOperateFrm), LOGLEVEL.ERROR, "device pickup error", new Exception("error code：" + result));
				return;
			}

			PhoneDeviceLib.PD_SetHeadState(Def_Ch, true);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			int result = BriSDKLib.QNV_SetDevCtrl(Def_Ch, BriSDKLib.QNV_CTRL_DOHOOK, 0);
			if (result <= 0)
			{
				LogFile.Write(typeof(DeviceOperateFrm), LOGLEVEL.ERROR, "device hangup error", new Exception("error code：" + result));
			}

			PhoneDeviceLib.PD_SetHeadState(Def_Ch, false);
		}


	}
}

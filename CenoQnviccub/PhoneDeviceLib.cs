using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common;
using CenoQnviccub;

namespace CenoCC
{
	public class PhoneDeviceLib
	{
		/// <summary>
		/// 启动硬件设备
		/// </summary>
		/// <returns></returns>
		public static ChannelInfo.CH_INFO[] OpenDevInfo(IntPtr MainHandle)
		{
			ChannelInfo.CH_INFO[] _ChInfo = null;
			try
			{
				if (BriSDKLib.QNV_OpenDevice(BriSDKLib.ODT_LBRIDGE, 0, "") <= 0 || BriSDKLib.QNV_DevInfo(0, BriSDKLib.QNV_DEVINFO_GETCHANNELS) <= 0)
				{
					LogFile.Write(typeof(PhoneDeviceLib), LOGLEVEL.ERROR, "open device failed");
					return _ChInfo;
				}

				//打开虚拟声卡设备
				if (true)
				{
					int result_Audio = BriSDKLib.QNV_Audrv(BriSDKLib.QNV_AUDRV_ISINSTALL, 0, null, null, 0);
					if (result_Audio > 0)
					{
						if (BriSDKLib.QNV_OpenDevice(BriSDKLib.ODT_AUDRV, 0, "0") > 0)
						{
							int inid = BriSDKLib.QNV_Audrv(BriSDKLib.QNV_AUDRV_SETWAVEINID, 0, null, null, 0);
							int outid = BriSDKLib.QNV_Audrv(BriSDKLib.QNV_AUDRV_SETWAVEOUTID, 0, null, null, 0);
						}
					}
					else
					{
						LogFile.Write(typeof(PhoneDeviceLib), LOGLEVEL.ERROR, "not install audio driver");
					}
				}

				//检测可用通道
				int channelCount = BriSDKLib.QNV_DevInfo(-1, BriSDKLib.QNV_DEVINFO_GETCHANNELS);
				if (channelCount > 0)
				{//有可用通道
					_ChInfo = new ChannelInfo.CH_INFO[channelCount];
					for (Int16 i = 0; i < channelCount; i++)//获取设备通道
					{
						//在windowproc处理接收到的消息
						BriSDKLib.QNV_Event(i, BriSDKLib.QNV_EVENT_REGWND, (Int32)MainHandle, "", new StringBuilder(0), 0);
						//初始化通道信息
						_ChInfo[i].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
						_ChInfo[i].lPlayFileHandle = -1;
						_ChInfo[i].lRecFileHandle = -1;
						_ChInfo[i].uChannelID = (short)i;
						_ChInfo[i].szCalleeId = new StringBuilder(20);
						_ChInfo[i].szCallerId = new StringBuilder(20);
						_ChInfo[i].CallInRingCount = -1;
						_ChInfo[i].lRecInfo.Caller = "";
						_ChInfo[i].lRecInfo.IsUsable = false;
						_ChInfo[i].lRecInfo.PhoneNumber = "";
						_ChInfo[i].lRecInfo.UserID = "-1";
						_ChInfo[i].lRecInfo.CallType = "1";
						_ChInfo[i].IsSoftDial = false;

						short Def_ChID = i;
						//初始化音量设置
						if (!string.IsNullOrEmpty(DeviceParam.D_MICVoiceValue))
							BriSDKLib.QNV_SetParam(Def_ChID, BriSDKLib.QNV_PARAM_AM_MIC, Convert.ToInt32(DeviceParam.D_MICVoiceValue));

						if (!string.IsNullOrEmpty(DeviceParam.D_SpkOutVoiceValue))
							BriSDKLib.QNV_SetParam(Def_ChID, BriSDKLib.QNV_PARAM_AM_SPKOUT, Convert.ToInt32(DeviceParam.D_SpkOutVoiceValue));

						if (!string.IsNullOrEmpty(DeviceParam.D_DoPlayVoiceValue))
							BriSDKLib.QNV_SetParam(Def_ChID, BriSDKLib.QNV_PARAM_AM_DOPLAY, Convert.ToInt32(DeviceParam.D_DoPlayVoiceValue));

						if (!string.IsNullOrEmpty(DeviceParam.D_LineOutVoiceValue))
							BriSDKLib.QNV_SetParam(Def_ChID, BriSDKLib.QNV_PARAM_AM_LINEOUT, Convert.ToInt32(DeviceParam.D_LineOutVoiceValue));

						if (!string.IsNullOrEmpty(DeviceParam.D_LineInVoiceValue))
							BriSDKLib.QNV_SetParam(Def_ChID, BriSDKLib.QNV_PARAM_AM_LINEIN, Convert.ToInt32(DeviceParam.D_LineInVoiceValue));


					}
				}
				LogFile.Write(typeof(PhoneDeviceLib), LOGLEVEL.INFO, "open device success");
			}
			catch (Exception ex)
			{
				LogFile.Write(typeof(PhoneDeviceLib), LOGLEVEL.ERROR, "open device failed", ex);
				return _ChInfo;
			}
			return _ChInfo;
		}

		/// <summary>
		/// 关闭硬件设备 
		/// </summary>
		/// <returns></returns>
		public static void CloseDevInfo()
		{
			int result = BriSDKLib.QNV_CloseDevice(BriSDKLib.ODT_ALL, 0);
			if (result <= 0)
			{
				LogFile.Write(typeof(PhoneDeviceLib), LOGLEVEL.ERROR, "close device failed", new Exception("error code:" + result));
			}
		}


		/// <summary>
		/// 打开或关闭耳机
		/// </summary>
		/// <param name="nch"></param>
		/// <param name="isopenhead"></param>
		public static void PD_SetHeadState(short nch, bool isopenhead)
		{
			int flag = 0;
			if (isopenhead)
			{
				flag = 1;
			}
			if (BriSDKLib.QNV_SetDevCtrl(nch, BriSDKLib.QNV_CTRL_DOLINETOSPK, flag) <= 0)
			{
				LogFile.Write(typeof(PhoneDeviceLib), LOGLEVEL.ERROR, "device open or close speaker failed");
			}

			if (BriSDKLib.QNV_SetDevCtrl(nch, BriSDKLib.QNV_CTRL_DOPLAYTOSPK, flag) <= 0)
			{
				LogFile.Write(typeof(PhoneDeviceLib), LOGLEVEL.ERROR, "device open or close play to speaker failed");
			}
		}

		/// <summary>
		/// 打开或关闭麦克风
		/// </summary>
		/// <param name="nch"></param>
		/// <param name="isopenflag"></param>
		public static void PD_SetMacState(short nch, bool isopenflag)
		{
			int flag = 0;
			if (isopenflag)
			{
				flag = 1;
				if (BriSDKLib.QNV_SetParam(nch, BriSDKLib.QNV_PARAM_AM_MIC, 5) <= 0)
				{
					LogFile.Write(typeof(PhoneDeviceLib), LOGLEVEL.ERROR, "device open or close mic failed");
				}
			}
			if (BriSDKLib.QNV_SetDevCtrl(nch, BriSDKLib.QNV_CTRL_DOMICTOLINE, flag) <= 0)//使用麦克风
			{
				LogFile.Write(typeof(PhoneDeviceLib), LOGLEVEL.ERROR, "device open or close play to mic failed");
			}

		}

		/// <summary>
		/// 打开或关闭喇叭
		/// </summary>
		/// <param name="nch"></param>
		/// <param name="isopenflag"></param>
		public static void PD_SetDoPlay(short nch, bool isopenflag)
		{
			int flag = 0;
			if (isopenflag)
			{
				flag = 1;
			}
			int result = BriSDKLib.QNV_SetDevCtrl(nch, BriSDKLib.QNV_CTRL_DOPLAY, flag);
			if (result <= 0)
			{
				LogFile.Write(typeof(PhoneDeviceLib), LOGLEVEL.ERROR, "device open or close play to mic failed", new Exception("error code:" + result));
			}
		}

		/// <summary>
		/// 播放指定语音文件
		/// </summary>
		/// <param name="nch"></param>
		/// <param name="wavfile">文件路径</param>
		/// <param name="playmode">播放模式</param>
		/// <returns></returns>
		public static int PD_StartPlayFile(short nch, string wavfile, int playmode)
		{
			int result = BriSDKLib.QNV_PlayFile(nch, BriSDKLib.QNV_PLAY_FILE_START, 0, playmode, wavfile);
			if (result <= 0)
			{
				LogFile.Write(typeof(PhoneDeviceLib), LOGLEVEL.ERROR, "device play audio(" + wavfile + ") failed", new Exception("error code:" + result));
			}
			return result;
		}

		/// <summary>
		/// 停止播放指定语音文件
		/// </summary>
		/// <param name="nch"></param>
		/// <param name="playHandle">播放语音句柄</param>
		/// <returns></returns>
		public static void PD_StopPlayFile(short nch, int playHandle)
		{
			if (playHandle > 0)
			{
				int result = BriSDKLib.QNV_PlayFile(nch, BriSDKLib.QNV_PLAY_FILE_STOPALL, playHandle, 0, "");
				if (result <= 0)
				{
					LogFile.Write(typeof(PhoneDeviceLib), LOGLEVEL.ERROR, "device stop play audio failed", new Exception("error code:" + result));
				}
			}
		}


	}
}

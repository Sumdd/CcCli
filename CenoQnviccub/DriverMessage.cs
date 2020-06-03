using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using CenoCC;
using System.Runtime.InteropServices;

namespace CenoQnviccub
{
	public class DriverMessage
	{
		public static void PhoneMessage(Message m)
		{
			BriSDKLib.TBriEvent_Data EventData = (BriSDKLib.TBriEvent_Data)Marshal.PtrToStructure(m.LParam, typeof(BriSDKLib.TBriEvent_Data));
			string strValue = "";
			short nCh = EventData.uChannelID;
			switch (EventData.lEventType)
			{
				#region BriSDKLib.BriEvent_PhoneHook   电话机摘机
				case BriSDKLib.BriEvent_PhoneHook:
					{
						LogFile.Write(typeof(DriverMessage), LOGLEVEL.INFO, string.Format("channel(ch={0}) pickup", nCh.ToString()));
						switch (CCFactory.ChInfo[nCh].chStatus)
						{
							#region ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING
							case ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING:
								{
									string callInPhone = CCFactory.ChInfo[nCh].szCallerId.ToString();
									string str = "来电 " + callInPhone + ",通话中";
									LogFile.Write(typeof(DriverMessage), LOGLEVEL.INFO, "we get a phone incoming");
									CCFactory.ChInfo[nCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING;
									CCFactory.ChInfo[nCh].lRecInfo.SpeakeTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

									//关闭喇叭
									PhoneDeviceLib.PD_SetDoPlay(nCh, false);
									//停止喇叭播发响铃语音
									PhoneDeviceLib.PD_StopPlayFile(nCh, CCFactory.ChInfo[nCh].lPlayFileHandle);
									CCFactory.ChInfo[nCh].lPlayFileHandle = 0;

									//#region  ======更新通话详情内容======

									//try
									//{
									//    //更新通话详情内容
									//    this.Main_DialShow_PhoneNumber_LL.Text = callInPhone;

									//    ToolTip tooltip = new ToolTip();
									//    tooltip.SetToolTip(this.Main_DialShow_PhoneNumber_LL, callInPhone);
									//    this.Main_DialShow_Linker_Lab.Text = "";
									//    this.Main_DialShow_Address_Lab.Text = "";
									//    this.Main_DialShow_Status_Lab.Text = "通话中";
									//    if (this.Main_DialShow_HungDown_Btn.Tag.ToString() == "redial")
									//    {
									//        this.Main_DialShow_Trans_Btn.Text = "转  接";
									//        this.Main_DialShow_Trans_Btn.Tag = "trans";

									//        this.Main_DialShow_HungDown_Btn.Text = "挂  断";
									//        this.Main_DialShow_HungDown_Btn.Tag = "hungdown";
									//    }
									//    hh = 0;
									//    mm = 0;
									//    ss = 0;
									//    this.CallTime_First.Enabled = true;
									//    this.MainDialShow_Panel.Visible = true;

									//    //查询电话信息    查询电话归属地以及联系人信息
									//    BackgroundWorker bw = new BackgroundWorker();
									//    bw.DoWork += new DoWorkEventHandler(bw_DoWork);
									//    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
									//    bw.RunWorkerAsync(callInPhone);

									//    //BackgroundWorker bwForLinker = new BackgroundWorker();
									//    //bwForLinker.DoWork += new DoWorkEventHandler(bwForLinker_DoWork);
									//    //bwForLinker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwForLinker_RunWorkerCompleted);
									//    //bwForLinker.RunWorkerAsync(callInPhone);

									//    if (ChInfo[nCh].CurrentCallRemindFrm != (IntPtr)0)
									//    {
									//        Win32API.SendMessage(ChInfo[nCh].CurrentCallRemindFrm, WM_USER + (int)EventMsg.E_US_RELEASE, 0, 0);
									//    }

									//}
									//catch { }

									//#endregion

								}
								break;
							#endregion

							#region ChannelInfo.ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE
							case ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE:
								{
									CCFactory.ChInfo[nCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_PICKUP;
								}
								break;
							#endregion

							case ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING:
								{

								}
								break;
							default: break;
						}
					} break;
				#endregion

				#region  BriSDKLib.BriEvent_PhoneHang   电话机挂机
				case BriSDKLib.BriEvent_PhoneHang:
					{
						LogFile.Write(typeof(DriverMessage), LOGLEVEL.INFO, string.Format("channel(ch={0}) hangup", nCh.ToString()));

						//检测软摘挂机状态
						int res = BriSDKLib.QNV_GetDevCtrl(nCh, BriSDKLib.QNV_CTRL_DOHOOK);
						if (res < 0)
						{
							LogFile.Write(typeof(DriverMessage), LOGLEVEL.ERROR, "get device hook state error ");
						}
						else if (res == 1)
						{//软摘机状态
							LogFile.Write(typeof(DriverMessage), LOGLEVEL.WARN, "get device hook state is pickup");
							break;
						}

						switch (CCFactory.ChInfo[nCh].chStatus)
						{
							#region ChannelInfo.APP_USER_STATUS.US_STATUS_DIALOUTTIME 拨号超时

							case ChannelInfo.APP_USER_STATUS.US_STATUS_DIALOUTTIME:
								{

								}
								break;

							#endregion

							#region ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING 正在通话中

							case ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING:
								{
									CCFactory.ChInfo[nCh].lRecInfo.EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

								}
								break;

							#endregion

							#region ChannelInfo.APP_USER_STATUS.US_STATUS_RINGBACK 回铃中
							case ChannelInfo.APP_USER_STATUS.US_STATUS_RINGBACK:
								{
									CCFactory.ChInfo[nCh].lRecInfo.EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
									CCFactory.ChInfo[nCh].lRecInfo.SpeakeTime = CCFactory.ChInfo[nCh].lRecInfo.EndTime;

									////保存通话记录
									//Local_RecordData.AddNewRecord(ChInfo[nCh].lRecInfo);
								}
								break;
							#endregion

							case ChannelInfo.APP_USER_STATUS.US_WAIT_LOCAL_HUNGDOW:
								{

								}
								break;
							case ChannelInfo.APP_USER_STATUS.US_STATUS_PICKUP:
								{

								}
								break;
						}

						//初始化通道
						CCFactory.ChInfo[nCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
						CCFactory.ChInfo[nCh].CallInRingCount = 0;
						CCFactory.ChInfo[nCh].uCallType = -1;
						CCFactory.ChInfo[nCh].lRecInfo.IsUsable = false;
						CCFactory.ChInfo[nCh].lRecInfo.PhoneNumber = "";
						CCFactory.ChInfo[nCh].lRecInfo.RecordPath = "";
						CCFactory.ChInfo[nCh].lRecInfo.RecordName = "";
						CCFactory.ChInfo[nCh].szCalleeId = new StringBuilder(20);
						CCFactory.ChInfo[nCh].szCallerId = new StringBuilder(20);



					} break;
				#endregion

				#region BriSDKLib.BriEvent_CallIn      来电响铃

				case BriSDKLib.BriEvent_CallIn:
					{
						try
						{
							if (CCFactory.ChInfo[nCh].CallInRingCount < 1)
							{
								strValue = "通道" + nCh.ToString() + "：来电响铃 " + EventData.lResult.ToString();
								LogFile.Write(typeof(DriverMessage), LOGLEVEL.INFO, "channel(ch=" + nCh.ToString() + ") is ring" + EventData.lResult.ToString());

								CCFactory.ChInfo[nCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING;
							}
							CCFactory.ChInfo[nCh].CallInRingCount = EventData.lResult;//记录响铃次数

						}
						catch (Exception ex)
						{
							LogFile.Write(typeof(DriverMessage), LOGLEVEL.INFO, "channel(ch=" + nCh.ToString() + ")  ring error", ex);
						}

					} break;

				#endregion

				#region  BriSDKLib.BriEvent_PhoneDial   本地电话机拨号

				case BriSDKLib.BriEvent_PhoneDial:
					{
						// 只有在本地话机摘机，没有调用软摘机时，检测到DTMF拨号

						string number = (new ASCIIEncoding()).GetString(EventData.szData).Trim(new char[1] { '\0' });

						strValue = "通道" + nCh.ToString() + "：电话机拨号 " + number;
						LogFile.Write(typeof(DriverMessage), LOGLEVEL.INFO, string.Format("channel(ch={0}) is dial number:{1}", nCh, number));

						//if (!this.MainDialPanInfo_Panel.Visible)
						//{
						//    this.MainDialPanInfo_Panel.Visible = true;
						//}
						//this.PhoneNubmer_Pan_Txt.Text = number;

						CCFactory.ChInfo[nCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_GETDTMF;
						CCFactory.ChInfo[nCh].szCalleeId = new StringBuilder(20);
						CCFactory.ChInfo[nCh].szCalleeId.Append(number);
					}
					break;

				#endregion

				#region ===========无需使用========
				/*
 * 
    #region BriSDKLib.BriEvent_GetDTMFChar 接受到按键DTMF字符

                    case BriSDKLib.BriEvent_GetDTMFChar:
                        {
                            // 线路接通时收到DTMF码事件
                            // 该事件不能区分通话中是本地话机按键还是对方话机按键触发

                            char[] charlist = new char[1] { '\0' };
                            string keyValue = FromASCIIByteArray(EventData.szData).Trim(charlist);
                            strValue = "通道" + (EventData.uChannelID + 1).ToString() + "：接收到按键 " + keyValue;
                            LogFile.Write(strValue, "CallInfo");
                            switch (ChInfo[EventData.uChannelID].chStatus)
                            {
                                #region   通话中接受到远程的DTMF字符

                                case ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING:
                                    {
                                        
                                    }
                                    break;

                                #endregion
                            }
                        }
                        break;

                    #endregion
                    #region BriSDKLib.BriEvent_GetCallID   获取来电号码

                    case BriSDKLib.BriEvent_GetCallID:
                        {
                            // 得到来电号码
                            // BRI_EVENT.lResult		来电号码模式(CALLIDMODE_FSK/CALLIDMODE_DTMF
                            // BRI_EVENT.szData			保存的来电号码
                            // 该事件可能在响铃前,也可能在响铃后

                            //来电号码
                            string phonenumber = FromASCIIByteArray(EventData.szData).Trim(new char[1] { '\0' });
                            strValue = "通道" + nCh.ToString() + "：接收到来电号码 " + phonenumber;
                            LogFile.Write(strValue, "Call");

                            string ringtype = CustomParams.RingRemindType;
                            if (ringtype == "1" || ringtype == "2")
                            {
                                if (ringtype == "1")
                                {
                                    //禁用电话响铃
                                }
                                //打开喇叭
                                PhoneDeviceLib.PD_SetDoPlay(nCh, true);
                                ChInfo[nCh].lPlayFileHandle = PhoneDeviceLib.PD_StartPlayFile(nCh, CustomParams.RingRemindWavFile, BriSDKLib.PLAYFILE_MASK_REPEAT);
                            }

                            //添加到通话结构体
                            ChInfo[nCh].szCallerId = new StringBuilder(20);
                            ChInfo[nCh].szCallerId.Append(phonenumber);

                            ChInfo[nCh].lRecInfo.PhoneNumber = string.Empty;
                            ChInfo[nCh].lRecInfo.PhoneNumber = phonenumber;

                            //开始录音
                            B_StartRecord(nCh);

                            if (!this._IsConnectCallServer)
                            {
                                //来电弹窗显示
                                CallRemindFrm crf = new CallRemindFrm(this);
                                crf.ShowActivity(phonenumber);

                                ChInfo[nCh].CurrentCallRemindFrm = (IntPtr)0;
                                ChInfo[nCh].CurrentCallRemindFrm = crf.Handle;

                                Win32API.SendMessage(this.ChInfo[Def_ChID].CurrentCallRemindFrm, WM_USER + (int)EventMsg.E_US_PICKUP, (IntPtr)0, Marshal.StringToHGlobalAnsi(ChInfo[nCh].WavRecName));
                            }

                        } break;

                    #endregion

                    #region BriSDKLib.BriEvent_StopCallIn  产生一个未接电话
                    //来电，本地未接，远程挂机，产生一个未接电话
                    case BriSDKLib.BriEvent_StopCallIn:
                        {
                            strValue = "通道" + nCh.ToString() + "：停止呼入，产生一个未接电话 ";
                            LogFile.Write(strValue, "CallInfo");

                            string ringtype = CustomParams.RingRemindType;
                            if (ringtype == "1" || ringtype == "2")
                            {
                                //关闭喇叭
                                PhoneDeviceLib.PD_SetDoPlay(nCh, false);
                                //停止喇叭播发响铃语音
                                PhoneDeviceLib.PD_StopPlayFile(nCh, ChInfo[nCh].lPlayFileHandle);
                                ChInfo[nCh].lPlayFileHandle = 0;

                            }

                            //添加提醒列表

                            if (ChInfo[nCh].CurrentCallRemindFrm != (IntPtr)0)
                            {
                                Win32API.SendMessage(ChInfo[nCh].CurrentCallRemindFrm, WM_USER + (int)EventMsg.E_US_SEIZURE, 0, 0);
                            }

                        } break;

                    #endregion

                  

                    #region BriSDKLib.BriEvent_RemoteHook  远程摘机
                    // 该事件只适用于拨打是标准信号音的号码时，也就是拨号后带有标准回铃音的号码。
                    // 如：当拨打的对方号码是彩铃(彩铃手机号)或系统提示音(179xx)都不是标准回铃音时该事件无效。
                    case BriSDKLib.BriEvent_RemoteHook:
                        {
                            LogFile.Write("通道" + (EventData.uChannelID + 1).ToString() + "：检测到远程摘机", "CallInfo");

                            ChInfo[nCh].lRecInfo.SpeakeTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            ChInfo[nCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING;


                            this.Main_DialShow_Status_Lab.Text = "通话中";
                            this.CallTime_First.Enabled = true;

                            switch (ChInfo[EventData.uChannelID].chStatus)
                            {
                                case ChannelInfo.APP_USER_STATUS.US_STATUS_RINGBACK:
                                    {

                                    }
                                    break;
                                case ChannelInfo.APP_USER_STATUS.US_STATUS_DIALOUTTIME:
                                    {

                                    }
                                    break;
                            }

                            #region 自动拨号播放预设语音文件

                            if (ChInfo[EventData.uChannelID].uCallType == 10)
                            {

                                //if (!string.IsNullOrEmpty(this.AutoCall_Info.speakContend))
                                //{
                                //    string savefilepath = "d:/cenosoft/CallCenterClient/temp/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_TTS.wav";
                                //    savefilepath = HelperClass.SaveConvertTxtToSound(this.AutoCall_Info.speakContend, savefilepath);
                                //    PhoneDeviceHelper.PD_StartPlayFile(savefilepath, ref ChInfo[EventData.uChannelID]);
                                //    int playlength = PhoneDeviceHelper.PD_GetFilePlayTime(ref ChInfo[EventData.uChannelID]);

                                //}


                            }

                            #endregion

                            #region  显示客户详细信息页
                            if (CustomParams.ShowScreenInfoUsingDefaultFlag != "1")
                            {
                                ModuleForms.C_Customer.CustomerInfoFrm cif = new CallCenterClient.ModuleForms.C_Customer.CustomerInfoFrm(this);
                                cif.SearchLinkerInfo(ChInfo[nCh].szCalleeId.ToString(), "", 0);
                                cif.Show();
                            }
                            if (CustomParams.ShowScreenInfoUsingURLFlag == "1")
                            {
                                StringBuilder arg = new StringBuilder();
                                arg.Append("TelNumber=" + ChInfo[nCh].szCalleeId.ToString());
                                arg.Append("&RecID=" + ChInfo[Def_ChID].WavRecName);

                                string mainurl = CustomParams.ExtentPageOfInterfaceWithURL;
                                if (!mainurl.Contains("?"))
                                {
                                    mainurl += "?";
                                }
                                string url = mainurl + arg.ToString();
                                this.CreateNewWebPage(url, true);
                            }
                            #endregion


                        }
                        break;

                    #endregion

                    #region BriSDKLib.BriEvent_RemoteHang  远程挂机

                    case BriSDKLib.BriEvent_RemoteHang:
                        {
                            LogFile.Write("通道 " + nCh + " 检测到远程挂机或无响应", "Call");

                            this.Main_DialShow_Status_Lab.Text = "空闲";
                            switch (ChInfo[nCh].chStatus)
                            {
                                case ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING:
                                    {
                                        LogFile.Write("本地响铃,远程终端挂机", "Call");
                                        //停止录音
                                        B_StopRecord(nCh);

                                    }
                                    break;
                                case ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING:
                                    {
                                        LogFile.Write("通话中，远程终端挂机", "Call");

                                        //停止录音
                                        B_StopRecord(nCh);

                                        this.CallTime_First.Enabled = false;

                                        //检测软摘挂机状态
                                        int res = BriSDKLib.QNV_GetDevCtrl(nCh, BriSDKLib.QNV_CTRL_DOHOOK);
                                        if (res < 0)
                                        {
                                            LogFile.Write("设备执行获取软摘挂机状态失败 ", "DeviceError");
                                        }
                                        else if (res == 1)
                                        {//软摘机

                                            LogFile.Write("设备执行挂机命令");
                                            if (BriSDKLib.QNV_SetDevCtrl(Def_ChID, BriSDKLib.QNV_CTRL_DOHOOK, 0) <= 0)
                                            {
                                                LogFile.Write("设备执行挂机命令失败", "DeviceError");
                                            }
                                            else
                                            {
                                                PhoneDeviceLib.PD_SetHeadState(nCh, false);
                                                PhoneDeviceLib.PD_SetMacState(nCh, false);
                                            }
                                        }
                                        else
                                        {
                                            this.Main_DialShow_Status_Lab.Text = "请挂话机";
                                        }

                                        this.Main_DialShow_Trans_Btn.Text = "关  闭";
                                        this.Main_DialShow_Trans_Btn.Tag = "close";

                                        this.Main_DialShow_HungDown_Btn.Text = "重  拨";
                                        this.Main_DialShow_HungDown_Btn.Tag = "redial";
                                    }
                                    break;
                                default: break;
                            }
                        } break;

                    #endregion

                    #region BriSDKLib.BriEvent_Busy        忙音，线路断开

                    case BriSDKLib.BriEvent_Busy:
                        {
                            strValue = "通道" + nCh.ToString() + "：接收到忙音,线路已经断开 ";
                            LogFile.Write(strValue, "CallInfo");
                            this.Main_DialShow_Status_Lab.Text = "线路忙";
                        } break;

                    #endregion

                    #region  BriSDKLib.BriEvent_DialTone    检测到拨号音
                    case BriSDKLib.BriEvent_DialTone:
                        {
                            strValue = "通道" + nCh.ToString() + "：检测到拨号音 ";
                            LogFile.Write(strValue, "CallInfo");
                            this.Main_DialShow_Status_Lab.Text = "拨号中";
                        }
                        break;
                    #endregion

                  

                    #region BriSDKLib.BriEvent_DialEnd    开始拨号后，全部号码拨号结束
                    case (int)BriSDKLib.BriEvent_DialEnd:
                        {
                            LogFile.Write("通道 " + nCh + "开始拨号后，全部号码拨号结束", "Call");
                        }
                        break;
                    #endregion

                    #region   BriSDKLib.BriEvent_RingBack   拨号后接受到回铃音

                    case BriSDKLib.BriEvent_RingBack:
                        {
                            if (ChInfo[nCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE)
                            {
                                break;
                            }

                            strValue = "通道 " + nCh.ToString() + "：拨号后接收到回铃音 ";
                            LogFile.Write(strValue, "CallInfo");

                            ChInfo[nCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_RINGBACK;
                            ChInfo[nCh].uCallType = 1;
                            ChInfo[nCh].lRecInfo.RingTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            ChInfo[nCh].lRecInfo.PhoneNumber = ChInfo[nCh].szCalleeId.ToString();
                            ChInfo[nCh].lRecInfo.IsUsable = true;

                            //开始录音
                            B_StartRecord(nCh);

                            try
                            {
                                if (this.MainDialPanInfo_Panel.Visible)
                                {
                                    this.MainDialPanInfo_Panel.Visible = false;
                                }

                                //更新通话详情内容
                                this.Main_DialShow_PhoneNumber_LL.Text = ChInfo[nCh].szCalleeId.ToString();
                                ToolTip tooltip = new ToolTip();
                                tooltip.SetToolTip(this.Main_DialShow_PhoneNumber_LL, ChInfo[nCh].szCalleeId.ToString());
                                this.Main_DialShow_Linker_Lab.Text = "";
                                this.Main_DialShow_Address_Lab.Text = "";
                                if (this.Main_DialShow_HungDown_Btn.Tag.ToString() == "redial")
                                {
                                    this.Main_DialShow_Trans_Btn.Text = "转  接";
                                    this.Main_DialShow_Trans_Btn.Tag = "trans";

                                    this.Main_DialShow_HungDown_Btn.Text = "挂  断";
                                    this.Main_DialShow_HungDown_Btn.Tag = "hungdown";
                                }
                                hh = 0;
                                mm = 0;
                                ss = 0;
                                this.MainDialShow_Panel.Visible = true;
                                //查询电话信息    查询电话归属地以及联系人信息
                                BackgroundWorker bw = new BackgroundWorker();
                                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                                bw.RunWorkerAsync(ChInfo[nCh].szCalleeId.ToString());
                            }
                            catch { }

                            switch (EventData.szData[0].ToString())
                            {
                                case "0":              //  电话机拨号中回铃了
                                    {
                                        LogFile.Write("电话机拨号后回铃", "Call");
                                    }
                                    break;
                                case "1":              //  软摘机拨号结束后回铃了
                                    {
                                        LogFile.Write("软拨号后回铃", "Call");
                                    }
                                    break;
                            }

                            switch (EventData.lResult)
                            {
                                case 0:      //检测到回铃音      //注意：如果线路是彩铃是不会触发该类型
                                    //TODO:检测到回铃音后开始录音
                                    LogFile.Write("检测到回铃音", "CallInfo");
                                    this.Main_DialShow_Status_Lab.Text = "等待接听";
                                    break;

                                #region 1 拨号超时

                                case 1:      //拨号超时
                                    ChInfo[EventData.uChannelID].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_DIALOUTTIME;
                                    strValue = "拨号超时";
                                    LogFile.Write(strValue, "CallInfo");
                                    this.Main_DialShow_Status_Lab.Text = "拨号超时";

                                    break;

                                #endregion

                                case 2:      //动态检测拨号码结束(根据中国大陆的号码规则进行智能分析，仅做参考)
                                    //TODO:动态检测拨号码结束后开始录音
                                    LogFile.Write("动态检测拨号结束", "CallInfo");

                                    break;
                            }
                        } break;
                    #endregion
                        */
				#endregion

				#region  BriSDKLib.BriEvent_PSTNFree PSTN线路断开，线路进入空闲状态
				case BriSDKLib.BriEvent_PSTNFree:
					{
						try
						{

							CCFactory.ChInfo[nCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
							CCFactory.ChInfo[nCh].CallInRingCount = 0;
							CCFactory.ChInfo[nCh].uCallType = -1;
							CCFactory.ChInfo[nCh].lRecInfo.IsUsable = false;
							CCFactory.ChInfo[nCh].lRecInfo.PhoneNumber = "";
							CCFactory.ChInfo[nCh].lRecInfo.RecordPath = "";
							CCFactory.ChInfo[nCh].lRecInfo.RecordName = "";
							CCFactory.ChInfo[nCh].szCalleeId = new StringBuilder(20);
							CCFactory.ChInfo[nCh].szCallerId = new StringBuilder(20);

							BriSDKLib.QNV_SetDevCtrl(nCh, BriSDKLib.QNV_CTRL_DOPHONE, 1);
						}
						catch (Exception ex)
						{
							LogFile.Write(typeof(DriverMessage), LOGLEVEL.ERROR, "channel is idle when pstn disconnect catch an excption ", ex);
						}

					}
					break;
				#endregion

				#region BriSDKLib.BriEvent_PlayMultiFileEnd 多文件连播结束
				case (int)BriSDKLib.BriEvent_PlayMultiFileEnd:
					break;
				#endregion

				#region BriSDKLib.BriEvent_PlayFileEnd 播放文件结束事件
				case (int)BriSDKLib.BriEvent_PlayFileEnd:
					{
						// BRI_EVENT.lEventHandle	   播放文件时返回的句柄ID 
					}
					break;
				#endregion

				#region BriSDKLib.BriEvent_SpeechResult 语音识别结果
				case (int)BriSDKLib.BriEvent_SpeechResult:
					{
						string result = "";
						BriSDKLib.QNV_Speech(EventData.uChannelID, BriSDKLib.QNV_SPEECH_GETRESULT, 0, result);
						string temp = result;
					}
					break;
				#endregion

				#region BriSDKLib.BriEvent_FlashEnd  拍叉簧完成
				case BriSDKLib.BriEvent_FlashEnd:
					{
						switch (EventData.lResult)
						{
							case BriSDKLib.TEL_FLASH://用户使用电话机进行拍叉簧完成
								{

								}
								break;
							case BriSDKLib.SOFT_FLASH://调用函数startflash进行拍叉簧完成
								{

								}
								break;
						}
					}
					break;
				#endregion

				#region BriSDKLib.BriEvent_RefuseEnd 拒接完成
				case BriSDKLib.BriEvent_RefuseEnd:
					{

					}
					break;
				#endregion

				#region  BriSDKLib.BriEvent_PSTNFree 产生一个PSTN呼入/呼出日志
				case BriSDKLib.BriEvent_CallLog:
					{
						CCFactory.ChInfo[nCh].szCalleeId = new StringBuilder(20);
						CCFactory.ChInfo[nCh].szCallerId = new StringBuilder(20);
					}
					break;
				#endregion

				/*******************设备状态****************************/

				#region BriSDKLib.BriEvent_DevErr  设备可能被拔掉了
				case BriSDKLib.BriEvent_DevErr:
					{
						if (EventData.lResult == 3)
						{
							LogFile.Write(typeof(DriverMessage), LOGLEVEL.INFO, "device is disconnect");
						}

					}
					break;

				#endregion

				#region BriSDKLib.BriEvent_EnableHook 软摘挂机成功事件
				case BriSDKLib.BriEvent_EnableHook:
					{
						// BRI_EVENT.lResult=1 软摘机
						// BRI_EVENT.lResult=0 软挂机	
						LogFile.Write(typeof(DriverMessage), LOGLEVEL.INFO, string.Format("channel(ch={0} pickup success event)", nCh));
						if (EventData.lResult == 1)//软摘机
						{
							switch (CCFactory.ChInfo[nCh].chStatus)
							{
								#region  ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING

								case ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING:
									{
										//关闭喇叭
										PhoneDeviceLib.PD_SetDoPlay(nCh, false);
										//停止喇叭播发响铃语音
										PhoneDeviceLib.PD_StopPlayFile(nCh, CCFactory.ChInfo[nCh].lPlayFileHandle);
										CCFactory.ChInfo[nCh].lPlayFileHandle = 0;
									}
									break;
								#endregion

								default: break;
							}
						}
						else if (EventData.lResult == 0)//软挂机
						{
							switch (CCFactory.ChInfo[nCh].chStatus)
							{
								case ChannelInfo.APP_USER_STATUS.US_STATUS_AUTOCALLING:
									{

									}
									break;
								case ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING:
								case ChannelInfo.APP_USER_STATUS.US_STATUS_TALKING:
									{
										CCFactory.ChInfo[nCh].lRecInfo.EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

									}
									break;
							}

						}

					}
					break;
				#endregion

				default:
					break;
			}

		}
	}
}

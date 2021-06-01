using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumiSoft.Net.UPnP.NAT;
using LumiSoft.Net.STUN.Client;
using LumiSoft.SIP.UA;
using LumiSoft.Net.SDP;
using LumiSoft.Net.RTP;
using LumiSoft.Net.Media.Codec.Audio;
using LumiSoft.Net.Media;
using LumiSoft.Net.SIP.Stack;
using System.Net;
using LumiSoft.Net;
using LumiSoft.Net.RTP.Debug;
using System.Runtime.InteropServices;
using Common;
using LumiSoft.Net.SIP;
using System.Runtime.CompilerServices;
using LumiSoft.Net.SIP.Message;
using System.IO;
using Core_v1;
using Model_v1;
using Cmn_v1;

namespace CenoSip {
    /// <summary>
    /// SIP协议栈
    /// </summary>
    public class SipStack : SipMain {

        #region 委托

        /* MARK 软电话进度 找CenoCC -> SessionControl 这里不要了 */
        //public delegate bool Sip_Incoming(string PhoneNum);
        //public static event Sip_Incoming Sip_Incoming_Event;

        /* MARK SIP挂断 没有使用 */
        public static bool m_pReStarting = false;
        public static DateTime m_pReStartingTime = DateTime.Now;
        public static int m_pResetCount = 0;

        public delegate bool Sip_Terminated();
        public static event Sip_Terminated Sip_Terminated_Event;

        /* MARK 软电话进度 找CenoCC -> SessionControl */

        public delegate void Sip_Call_Progress(string Call_Progress);
        public static event Sip_Call_Progress Sip_Call_Progress_Event;

        public static bool CallResponseSeen = false;
        public static List<SIP_Dialog_Invite> earlyDialogs = new List<SIP_Dialog_Invite>();
        #endregion

        #region 初始化
        /// <summary>
        /// Initializes SIP stack.
        /// </summary>
        public static bool InitStack() {
            try {

                #region ***兼容性:IP话机,Web调用
                ///<![CDATA[
                /// IP话机,与客户端连用,但是不进行注册SIP
                /// ]]>
                if (CCFactory.IsRegister == 0)
                {
                    Log.Instance.Warn($"[CenoSip][SipStack][InitStack][IP话机,不进行客户端的电话注册,直接注册成功即可]");
                    CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_IDLE;
                    return true;
                }
                if (CCFactory.IsRegister == -1)
                {
                    CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_WEB_NOTREGISTER;
                    Log.Instance.Warn($"[CenoSip][SipStack][InitStack][IP话机Web调用,不进行客户端的电话注册]");
                    return false;
                }
                #endregion

                if (SipParam.m_pAudioOutDevice == null)
                {
                    CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_ERR_NODEVICEOUT;
                    throw new Exception("未找到音频输出设备");
                }

                if (SipParam.m_pAudioInDevice == null)
                {
                    CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_ERR_NODEVICEIN;
                    throw new Exception("未找到音频输入设备");
                }

                SipParam.m_pPlayer = new WavePlayer(SipParam.m_pAudioOutDevice);

                SipParam.m_pStack = new SIP_Stack();
                SipParam.m_pStack.UserAgent = $"CenoCC {H_Json.updateJsonModel?.version}";
                SipParam.m_pStack.BindInfo = new IPBindInfo[] { new IPBindInfo("", BindInfoProtocol.UDP, IPAddress.Parse(CommonParam.GetLocalIpAddress), Account.LocalSipPort) };
                SipParam.m_pStack.Credentials.Add(new NetworkCredential(Account.AccountID, Account.Password, Account.DomainName));
                SipParam.m_pStack.Error += new EventHandler<ExceptionEventArgs>(m_pStack_Error);
                SipParam.m_pStack.RequestReceived += new EventHandler<SIP_RequestReceivedEventArgs>(m_pStack_RequestReceived);
                SipParam.m_pStack.ValidateRequest += new EventHandler<SIP_ValidateRequestEventArgs>(m_pStack_ValidateRequest);
                SipParam.m_pStack.ResponseReceived += new EventHandler<SIP_ResponseReceivedEventArgs>(m_pStack_ResponseReceived);
                SipStack.Sip_Call_Progress_Event += new SipStack.Sip_Call_Progress(Call_Progress_Event);
                SipParam.m_pStack.Start();

                SipDebug.Debug_Sip(SipParam.m_pStack);
                return true;
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoSip][SipStack][InitStack][Exception][{ex.Message}]");
                return false;
            }
        }
        #endregion

        #region 呼叫进度事件
        /// <summary>
        /// is called when call state change.
        /// </summary>
        /// <param name="Call_Progress"></param>
        private static void Call_Progress_Event(string Call_Progress) {
            switch(CCFactory._PhoneType) {
                case GlobalData.PhoneType.SIP_SOFT_PHONE:
                    switch(Call_Progress) {
                        case "Calling":
                            break;
                        case "Active":
                            break;
                        case "Ring":
                            break;
                        case "Disposed":
                            break;
                        case "Terminated":
                            Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_HUNGUP, (IntPtr)1, (IntPtr)2);
                            break;
                        case "Terminating":
                            break;
                    }
                    break;
            }
        }
        #endregion

        #region 关闭
        public static void Dispose() {
            try {
                if(SipParam.m_pStack != null) {
                    Log.Instance.Error($"[CenoSip][SipStack][Dispose][释放SIP协议栈,开始]");
                    SipParam.m_pStack.Dispose();
                    SipParam.m_pStack = null;
                    Log.Instance.Error($"[CenoSip][SipStack][Dispose][释放SIP协议栈,结束]");
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoSip][SipStack][Dispose][{ex.Message}]");
            }
        }
        #endregion

        #region 数据验证
        static void m_pStack_ValidateRequest(object sender, SIP_ValidateRequestEventArgs e) {
            bool m_bReturn = false;
            if (m_bReturn)
            {
                try
                {
                    string m_sLogString = $@"{e?.Request?.ContentType},{e?.Request?.RequestLine?.Method},{e?.Request?.ContentLength},{e?.Request?.ToString()?.Length}";
                    Log.Instance.Debug($"[CenoSip][SipStack][m_pStack_ValidateRequest][{m_sLogString}]");
                    if (e?.Request?.ErrorInfo?.FieldName != "Error-Info:")
                    {
                        Log.Instance.Debug($"[CenoSip][SipStack][m_pStack_ValidateRequest][{e?.Request?.ErrorInfo?.FieldName}]");
                        Log.Instance.Debug($"[CenoSip][SipStack][m_pStack_ValidateRequest][{e?.Request?.ToString()}]");
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoSip][SipStack][m_pStack_ValidateRequest][Exception][{ex.Message}]");
                }
            }
        }
        #endregion

        #region 数据错误
        /// <summary>
        /// Is called when sip stack has unhandled error.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private static void m_pStack_Error(object sender, ExceptionEventArgs e) {
            try {
                switch(e?.Exception?.Message) {
                    case "Socket error 'ConnectionReset'.": {
                            Log.Instance.Success($"[CenoSip][SipStack][m_pStack_Error][ConnectionReset时,协议栈状态:{SipParam.m_pStack?.State}]");
                        }
                        break;
                    case "通常每个套接字地址(协议/网络地址/端口)只允许使用一次。":
                        break;
                    default:
                        Log.Instance.Debug($"[CenoSip][SipStack][m_pStack_Error][{e?.Exception?.Message}]");
                        break;
                }
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoSip][SipStack][m_pStack_Error][{e?.Exception?.Message}][Exception][{ex.Message}]");
            } finally {
                if(SipStack.m_pResetCount <= 10) {
                    if(!SipStack.m_pReStarting && ((TimeSpan)(DateTime.Now - SipStack.m_pReStartingTime)).Seconds >= 15) {
                        SipStack.m_pReStarting = true;
                        Log.Instance.Success($"[CenoSip][SipStack][m_pStack_Error][{e?.Exception?.Message},SIP重置]");
                        CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_UNAVAILABLE;
                        Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_DO_SIP_RESET, (IntPtr)0, (IntPtr)0);
                    }
                } else {
                    SipStack.m_pResetCount++;
                    SipRegister.Registration = null;
                    Log.Instance.Success($"[CenoSip][SipStack][m_pStack_Error][{e?.Exception?.Message},SIP重新注册,次数:{SipStack.m_pResetCount}]");
                    CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_UNAVAILABLE;
                    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_LOAD_STATUS_UNAVAILABLE, (IntPtr)0, (IntPtr)1000);
                }

                #region finally all
                //if(!SipRegister.IsRegister()) {
                //    Log.Instance.Success($"[CenoSip][SipStack][m_pStack_Error][{e.Exception.Message},SIP重新注册]");
                //    CCFactory.ChInfo[CCFactory.CurrentCh].chStatus = ChannelInfo.APP_USER_STATUS.US_STATUS_UNAVAILABLE;
                //    Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_LOAD_STATUS_UNAVAILABLE, (IntPtr)0, (IntPtr)1000);
                //} else {
                //    Log.Instance.Success($"[CenoSip][SipStack][m_pStack_Error][{e.Exception.Message},SIP重新注册成功,无需再次注册]");
                //}
                #endregion
            }
        }
        #endregion

        #region 数据接收
        /// <summary>
        /// Is called when sip stack has received request.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private static void m_pStack_RequestReceived(object sender, SIP_RequestReceivedEventArgs e) {
            try {
                switch(e.Request.RequestLine.Method) {
                    case SIP_Methods.CANCEL:
                        /* RFC 3261 9.2.
						If the UAS did not find a matching transaction for the CANCEL
						according to the procedure above, it SHOULD respond to the CANCEL
						with a 481 (Call Leg/Transaction Does Not Exist).
						Regardless of the method of the original request, as long as the
						CANCEL matched an existing transaction, the UAS answers the CANCEL
						request itself with a 200 (OK) response.
						*/


                        SIP_ServerTransaction trToCancel = SipParam.m_pStack.TransactionLayer.MatchCancelToTransaction(e.Request);
                        if(trToCancel != null) {
                            trToCancel.Cancel();
                            e.ServerTransaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x200_Ok, e.Request));
                        } else {
                            e.ServerTransaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x481_Call_Transaction_Does_Not_Exist, e.Request));
                        }
                        if (CCFactory.ChInfo[CCFactory.CurrentCh].chStatus == ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING)
                            Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_HUNGUP, (IntPtr)1, (IntPtr)1);
                        break;
                    case SIP_Methods.INVITE:

                        #region Incoming call
                        if(e.Dialog == null) {
                            // We don't accept more than 1 call at time.
                            //if (m_pIncomingCallUI != null || m_pCall != null)
                            //{
                            //    e.ServerTransaction.SendResponse(m_pStack.CreateResponse(SIP_ResponseCodes.x600_Busy_Everywhere, e.Request));

                            //    return;
                            //}

                            string m_sCaller = "Unknown";
                            string m_sCallee = "Unknown";
                            SipStack.m_fGetErEe(e, out m_sCaller, out m_sCallee);

                            if (CCFactory.IsInCall) {
                                e.ServerTransaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x600_Busy_Everywhere, e.Request));
                                return;
                            }

                            // We don't accept SDP offerless calls.
                            if(e.Request.ContentType == null || e.Request.ContentType.ToLower().IndexOf("application/sdp") == -1) {
                                Log.Instance.Fail($"[CenoSip][SipStack][m_pStack_RequestReceived][INVITE,无效报文]");
                                e.ServerTransaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x606_Not_Acceptable + ": We don't accpet SDP offerless calls.", e.Request));
                                return;
                            }

                            SDP_Message sdpOffer = SDP_Message.Parse(Encoding.UTF8.GetString(e.Request.Data));

                            // Check if we can accept any media stream.
                            bool canAccept = false;
                            foreach(SDP_MediaDescription media in sdpOffer.MediaDescriptions) {
                                if(RtpStack.CanSupportMedia(media)) {
                                    canAccept = true;
                                    break;
                                }
                            }
                            if(!canAccept) {
                                Log.Instance.Fail($"[CenoSip][SipStack][m_pStack_RequestReceived][媒体协商失败]");
                                e.ServerTransaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x606_Not_Acceptable, e.Request));
                                Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_DIALFAIL, (IntPtr)0, Cmn.Sti("媒体协商失败"));
                                return;
                            }

                            /* 只允许一个电话进入 */
                            CCFactory.IsInCall = true;

                            SIP_Response responseRinging = SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x180_Ringing, e.Request, e.Flow);
                            responseRinging.To.Tag = SIP_Utils.CreateTag();
                            e.ServerTransaction.SendResponse(responseRinging);

                            SIP_Dialog_Invite dialog = (SIP_Dialog_Invite)SipParam.m_pStack.TransactionLayer.GetOrCreateDialog(e.ServerTransaction, responseRinging);

                            RTP_MultimediaSession rtpMultimediaSession = new RTP_MultimediaSession(RTP_Utils.GenerateCNAME());
                            // Build local SDP template
                            SDP_Message sdpLocal = new SDP_Message();
                            sdpLocal.Version = "0";
                            sdpLocal.Origin = new SDP_Origin("-", sdpLocal.GetHashCode(), 1, "IN", "IP4", Common.CommonParam.GetLocalIpAddress);
                            sdpLocal.SessionName = "SIP Call";
                            sdpLocal.Times.Add(new SDP_Time(0, 0));

                            // Create call.
                            SipParam.m_pCall = new SIP_Call(SipParam.m_pStack, dialog, rtpMultimediaSession, sdpLocal, e.ServerTransaction, sdpOffer);
                            SipParam.m_pCall.StateChanged += new EventHandler(SipStack.m_pCall_StateChanged);

                            SipStack.m_pCall_StateChanged(SipParam.m_pCall, new EventArgs());

                            Log.Instance.Success($"[CenoSip][SipStack][m_pStack_RequestReceived][媒体协商成功]");

                            Log.Instance.Debug(e.Request.ToString());
                            //是否自动接听逻辑
                            string m_sAutoAccept = "N";
                            SIP_HeaderField X_ALegAutoAccept = e.Request.Header.GetFirst("X_ALegAutoAccept:");
                            if (X_ALegAutoAccept != null) m_sAutoAccept = X_ALegAutoAccept.Value;

                            if(CCFactory.ChInfo[CCFactory.CurrentCh].uCallType == -1) {
                                CCFactory.ChInfo[CCFactory.CurrentCh].uCallType = 2;
                                Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_RINGING, Marshal.StringToHGlobalAnsi($"{m_sCaller}|{m_sCallee}"), Marshal.StringToHGlobalAnsi(m_sAutoAccept));
                            } else {
                                Win32API.SendMessage(CCFactory.MainHandle, CCFactory.WM_USER + (int)ChannelInfo.APP_USER_STATUS.US_STATUS_RINGBACK, (IntPtr)0, (IntPtr)0);
                            }
                            // We need invoke here, otherwise we block SIP stack RequestReceived event while incoming call UI showed.
                        }
                        #endregion

                        #region Re-INVITE

                        else {
                            try {
                                // Remote-party provided SDP offer.
                                if(e.Request.ContentType != null && e.Request.ContentType.ToLower().IndexOf("application/sdp") > -1) {
                                    ////////SipStack.ProcessMediaOffer(SipParam.m_pCall.Dialog, e.ServerTransaction, SipParam.m_pCall.RtpMultimediaSession, SDP_Message.Parse(Encoding.UTF8.GetString(e.Request.Data)), SipParam.m_pCall.LocalSDP);
                                    ////////// We are holding a call.
                                    ////////if (m_pToggleOnHold.Text == "Unhold")
                                    ////////{
                                    ////////    // We don't need to do anything here.
                                    ////////}
                                    ////////// Remote-party is holding a call.
                                    ////////else if (IsRemotePartyHolding(SDP_Message.Parse(Encoding.UTF8.GetString(e.Request.Data))))
                                    ////////{
                                    ////////    // We need invoke here, we are running on thread pool thread.
                                    ////////    this.BeginInvoke(new MethodInvoker(delegate()
                                    ////////    {
                                    ////////        m_pStatusBar.Items["text"].Text = "Remote party holding a call";
                                    ////////    }));

                                    ////////    SipParam.m_pPlayer.Play(ResManager.GetStream("onhold.wav"), 20);
                                    ////////}
                                    ////////// Call is active.
                                    ////////else
                                    ////////{
                                    ////////    // We need invoke here, we are running on thread pool thread.
                                    ////////    this.BeginInvoke(new MethodInvoker(delegate()
                                    ////////    {
                                    ////////        m_pStatusBar.Items["text"].Text = "Call established";
                                    ////////    }));

                                    ////////    SipParam.m_pPlayer.Stop();
                                    ////////}
                                }
                                // Error: Re-INVITE can't be SDP offerless.
                                else {
                                    Log.Instance.Fail($"[CenoSip][SipStack][m_pStack_RequestReceived][INVITE,无效报文]");
                                    e.ServerTransaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x500_Server_Internal_Error + ": Re-INVITE must contain SDP offer.", e.Request));
                                }
                            } catch(Exception ex) {
                                Log.Instance.Error($"[CenoSip][SipStack][m_pStack_RequestReceived][Exception][Re-INVITE错误:{ex.Message}]");
                                e.ServerTransaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x500_Server_Internal_Error + ": " + ex.Message, e.Request));
                            }
                        }

                        #endregion
                        break;
                    case SIP_Methods.ACK:
                        break;
                    case SIP_Methods.BYE:
                        /* RFC 3261 15.1.2.
						If the BYE does not match an existing dialog, the UAS core SHOULD generate a 481
						(Call/Transaction Does Not Exist) response and pass that to the server transaction.
						*/
                        // Currently we match BYE to dialog and it processes it,
                        // so BYE what reaches here doesnt match to any dialog.
                        e.ServerTransaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x481_Call_Transaction_Does_Not_Exist, e.Request));
                        break;
                    case SIP_Methods.REGISTER:
                        break;
                    default:
                        e.ServerTransaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x501_Not_Implemented, e.Request));
                        break;
                }
            } catch {
                e.ServerTransaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x500_Server_Internal_Error, e.Request));
            }
        }
        #endregion

        #region SIP协议栈数据响应
        /// <summary>
        /// Is called when sip stack has received response.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private static void m_pStack_ResponseReceived(object sender, SIP_ResponseReceivedEventArgs e) {
            Log.Instance.Success($"[CenoSip][SipStack][m_pStack_ResponseReceived][协议栈响应数据接收,{e.Response.ToString()}]");
        }
        #endregion

        #region 电话状态变化
        /// <summary>
        /// Is called when call state changed
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        public static void m_pCall_StateChanged(object sender, EventArgs e) {
            if(SipParam.m_pCall == null)
                return;
            switch(SipParam.m_pCall.State) {
                case SIP_CallState.Active:
                    Sip_Call_Progress_Event("Active");
                    break;
                case SIP_CallState.Terminated:
                    Sip_Call_Progress_Event("Terminated");
                    break;
                case SIP_CallState.Terminating:
                    Sip_Call_Progress_Event("Terminating");
                    break;
                case SIP_CallState.Disposed:
                    Sip_Call_Progress_Event("Disposed");
                    break;
                case SIP_CallState.Calling:
                    Sip_Call_Progress_Event("Calling");
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 电话响应???
        /// <summary>
        /// Is called when sip request received response.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        /* 这里没用,不应该加到这里,收不到180 */
        public static void m_pCall_ResponseReceived(object sender, SIP_ResponseReceivedEventArgs e) {

            LogFile.Write(typeof(SipStack), LOGLEVEL.INFO, "180?" + e.Response.ToString());

            // Skip 2xx retransmited response.
            if(CallResponseSeen)
                return;

            if(e.Response.StatusCode >= 200)
                CallResponseSeen = true;

            switch(e.Response.StatusCodeType) {
                case SIP_StatusCodeType.Provisional:
                    /* RFC 3261 13.2.2.1.
						Zero, one or multiple provisional responses may arrive before one or
						more final responses are received.  Provisional responses for an
						INVITE request can create "early dialogs".  If a provisional response
						has a tag in the To field, and if the dialog ID of the response does
						not match an existing dialog, one is constructed using the procedures
						defined in Section 12.1.2.
					*/
                    if(e.Response.StatusCode > 100 && e.Response.To.Tag != null) {
                        earlyDialogs.Add((SIP_Dialog_Invite)e.GetOrCreateDialog);
                    }

                    // 180_Ringing.
                    if(e.Response.StatusCode == 180 || e.Response.StatusCode == 183) {
                        SipParam.m_pPlayer.Play(new FileStream("Audio\\ringing.wav", FileMode.Open), 10);

                        Sip_Call_Progress_Event("Ring");
                        // We need BeginInvoke here, otherwise we block client transaction.
                        ////////////////SipParam.m_pStatusBar.BeginInvoke(new MethodInvoker(delegate()
                        ////////////////{
                        ////////////////    m_pStatusBar.Items["text"].Text = "Ringing";
                        ////////////////}));
                    }
                    break;
                case SIP_StatusCodeType.Success:
                    SIP_Dialog dialog = e.GetOrCreateDialog;

                    /* Exit all all other dialogs created by this call (due to forking).
					   That is not defined in RFC but, since UAC can send BYE to early and confirmed dialogs, 
					   all this is 100% valid.
					*/
                    foreach(SIP_Dialog_Invite d in earlyDialogs.ToArray()) {
                        if(!d.Equals(dialog)) {
                            d.Terminate("Another forking leg accepted.", true);
                        }
                    }

                    SipParam.m_pCall.InitCalling(dialog, SipParam.m_pCall.LocalSDP);

                    // Remote-party provided SDP.
                    if(e.Response.ContentType != null && e.Response.ContentType.ToLower().IndexOf("application/sdp") > -1) {
                        try {
                            // SDP offer. We sent offerless INVITE, we need to send SDP answer in ACK request.'
                            if(e.ClientTransaction.Request.ContentType == null || e.ClientTransaction.Request.ContentType.ToLower().IndexOf("application/sdp") == -1) {
                                // Currently we never do it, so it never happens. This is place holder, if we ever support it.
                            }
                            // SDP answer to our offer.
                            else {
                                // This method takes care of ACK sending and 2xx response retransmission ACK sending.
                                SipStack.HandleAck(SipParam.m_pCall.Dialog, e.ClientTransaction);

                                RtpStack.ProcessMediaAnswer(SipParam.m_pCall, SipParam.m_pCall.LocalSDP, SDP_Message.Parse(Encoding.UTF8.GetString(e.Response.Data)));
                            }
                        } catch {
                            SipParam.m_pCall.Terminate("SDP answer parsing/processing failed.");
                        }
                    } else {
                        // If we provided SDP offer, there must be SDP answer.
                        if(e.ClientTransaction.Request.ContentType != null && e.ClientTransaction.Request.ContentType.ToLower().IndexOf("application/sdp") > -1) {
                            SipParam.m_pCall.Terminate("Invalid 2xx response, required SDP answer is missing.");
                        }
                    }

                    // Stop ringing.
                    SipParam.m_pPlayer.Stop();

                    break;
                case SIP_StatusCodeType.RequestFailure:
                case SIP_StatusCodeType.GlobalFailure:
                case SIP_StatusCodeType.Redirection:
                case SIP_StatusCodeType.ServerFailure:
                    /* RFC 3261 13.2.2.3.
					All early dialogs are considered terminated upon reception of the non-2xx final response.
					*/
                    foreach(SIP_Dialog_Invite Failuredialog in earlyDialogs.ToArray()) {
                        Failuredialog.Terminate("All early dialogs are considered terminated upon reception of the non-2xx final response. (RFC 3261 13.2.2.3)", false);
                    }

                    // We need BeginInvoke here, otherwise we block client transaction while message box open.
                    if(SipParam.m_pCall.State != SIP_CallState.Terminating) {
                        //////////////////this.BeginInvoke(new MethodInvoker(delegate()
                        //////////////////{
                        //////////////////    m_pCall_HangUp.Image = ResManager.GetIcon("call.ico", new Size(24, 24)).ToBitmap();
                        //////////////////    MessageBox.Show("Calling failed: " + e.Response.StatusCode_ReasonPhrase, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //////////////////}));
                    }

                    // We need BeginInvoke here, otherwise we block client transaction.
                    //////////////////m_pStatusBar.BeginInvoke(new MethodInvoker(delegate()
                    //////////////////{
                    //////////////////    m_pStatusBar.Items["text"].Text = "";
                    //////////////////}));
                    // Stop calling or ringing.
                    SipParam.m_pPlayer.Stop();

                    // Terminate call.
                    SipParam.m_pCall.Terminate("Remote party rejected a call.", false);

                    break;
            }

        }
        #endregion

        #region 200接收

        /// <summary>
        /// This method takes care of INVITE 2xx response retransmissions while ACK received.
        /// </summary>
        /// <param name="dialog">SIP dialog.</param>
        /// <param name="transaction">INVITE server transaction.</param>
        /// <exception cref="ArgumentException">Is raised when <b>dialog</b>,<b>transaction</b> is null reference.</exception>
        public static void Handle2xx(SIP_Dialog dialog, SIP_ServerTransaction transaction) {
            if(dialog == null) {
                throw new ArgumentNullException("dialog");
            }
            if(transaction == null) {
                throw new ArgumentException("transaction");
            }

            /* RFC 6026 8.1.
				Once the response has been constructed, it is passed to the INVITE
				server transaction.  In order to ensure reliable end-to-end
				transport of the response, it is necessary to periodically pass
				the response directly to the transport until the ACK arrives.  The
				2xx response is passed to the transport with an interval that
				starts at T1 seconds and doubles for each retransmission until it
				reaches T2 seconds (T1 and T2 are defined in Section 17).
				Response retransmissions cease when an ACK request for the
				response is received.  This is independent of whatever transport
				protocols are used to send the response.
             
				If the server retransmits the 2xx response for 64*T1 seconds without
				receiving an ACK, the dialog is confirmed, but the session SHOULD be
				terminated.  This is accomplished with a BYE, as described in Section
				15.
              
				 T1 - 500
				 T2 - 4000
			*/

            TimerEx timer = null;

            EventHandler<SIP_RequestReceivedEventArgs> callback = delegate (object s1, SIP_RequestReceivedEventArgs e) {
                try {
                    if(e.Request.RequestLine.Method == SIP_Methods.ACK) {
                        // ACK for INVITE 2xx response received, stop retransmitting INVITE 2xx response.
                        if(transaction.Request.CSeq.SequenceNumber == e.Request.CSeq.SequenceNumber) {
                            if(timer != null) {
                                timer.Dispose();
                            }
                        }
                    }
                } catch {
                    // We don't care about errors here.
                }
            };
            dialog.RequestReceived += callback;

            // Create timer and sart retransmitting INVITE 2xx response.
            timer = new TimerEx(500);
            timer.AutoReset = false;
            timer.Elapsed += delegate (object s, System.Timers.ElapsedEventArgs e) {
                try {
                    lock(transaction.SyncRoot) {
                        if(transaction.State == SIP_TransactionState.Accpeted) {
                            transaction.SendResponse(transaction.FinalResponse);
                        } else {
                            timer.Dispose();

                            return;
                        }
                    }
                    timer.Interval = Math.Min(timer.Interval * 2, 4000);
                    timer.Enabled = true;
                } catch {
                    // We don't care about errors here.
                }
            };
            timer.Disposed += delegate (object s1, EventArgs e1) {
                try {
                    dialog.RequestReceived -= callback;
                } catch {
                    // We don't care about errors here.
                }
            };
            timer.Enabled = true;
        }
        #endregion

        #region 200发送
        /// <summary>
        /// This method takes care of ACK sending and 2xx response retransmission ACK sending.
        /// </summary>
        /// <param name="dialog">SIP dialog.</param>
        /// <param name="transaction">SIP client transaction.</param>
        public static void HandleAck(SIP_Dialog dialog, SIP_ClientTransaction transaction) {
            if(dialog == null) {
                throw new ArgumentNullException("dialog");
            }
            if(transaction == null) {
                throw new ArgumentNullException("transaction");
            }

            /* RFC 3261 6.
				The ACK for a 2xx response to an INVITE request is a separate transaction.
              
			   RFC 3261 13.2.2.4.
				The UAC core MUST generate an ACK request for each 2xx received from
				the transaction layer.  The header fields of the ACK are constructed
				in the same way as for any request sent within a dialog (see Section
				12) with the exception of the CSeq and the header fields related to
				authentication.  The sequence number of the CSeq header field MUST be
				the same as the INVITE being acknowledged, but the CSeq method MUST
				be ACK.  The ACK MUST contain the same credentials as the INVITE.  If
				the 2xx contains an offer (based on the rules above), the ACK MUST
				carry an answer in its body.
			*/

            SIP_t_ViaParm via = new SIP_t_ViaParm();
            via.ProtocolName = "SIP";
            via.ProtocolVersion = "2.0";
            via.ProtocolTransport = transaction.Flow.Transport;
            via.SentBy = new HostEndPoint(transaction.Flow.LocalEP);
            via.Branch = SIP_t_ViaParm.CreateBranch();
            via.RPort = 0;

            SIP_Request ackRequest = dialog.CreateRequest(SIP_Methods.ACK);
            ackRequest.Via.AddToTop(via.ToStringValue());
            ackRequest.CSeq = new SIP_t_CSeq(transaction.Request.CSeq.SequenceNumber, SIP_Methods.ACK);
            // Authorization
            foreach(SIP_HeaderField h in transaction.Request.Authorization.HeaderFields) {
                ackRequest.Authorization.Add(h.Value);
            }
            // Proxy-Authorization 
            foreach(SIP_HeaderField h in transaction.Request.ProxyAuthorization.HeaderFields) {
                ackRequest.Authorization.Add(h.Value);
            }

            // Send ACK.
            SendAck(dialog, ackRequest);

            // Start receive 2xx retransmissions.
            transaction.ResponseReceived += delegate (object sender, SIP_ResponseReceivedEventArgs e) {
                if(dialog.State == SIP_DialogState.Disposed || dialog.State == SIP_DialogState.Terminated) {
                    return;
                }

                // Don't send ACK for forked 2xx, our sent BYE(to all early dialogs) or their early timer will kill these dialogs.
                // Send ACK only to our accepted dialog 2xx response retransmission.
                if(e.Response.From.Tag == ackRequest.From.Tag && e.Response.To.Tag == ackRequest.To.Tag) {
                    SendAck(dialog, ackRequest);
                }
            };
        }
        #endregion

        #region 发送ACK
        /// <summary>
        /// Sends ACK to remote-party.
        /// </summary>
        /// <param name="dialog">SIP dialog.</param>
        /// <param name="ack">SIP ACK request.</param>
        public static void SendAck(SIP_Dialog dialog, SIP_Request ack) {
            if(dialog == null) {
                throw new ArgumentNullException("dialog");
            }
            if(ack == null) {
                throw new ArgumentNullException("ack");
            }

            try {
                // Try existing flow.
                dialog.Flow.Send(ack);

                // Log
                if(dialog.Stack.Logger != null) {
                    byte[] ackBytes = ack.ToByteData();

                    dialog.Stack.Logger.AddWrite(
                        dialog.ID,
                        null,
                        ackBytes.Length,
                        "Request [DialogID='" + dialog.ID + "';" + "method='" + ack.RequestLine.Method + "'; cseq='" + ack.CSeq.SequenceNumber + "'; " +
                        "transport='" + dialog.Flow.Transport + "'; size='" + ackBytes.Length + "'] sent '" + dialog.Flow.LocalEP + "' -> '" + dialog.Flow.RemoteEP + "'.",
                        dialog.Flow.LocalEP,
                        dialog.Flow.RemoteEP,
                        ackBytes
                    );
                }
            } catch {
                /* RFC 3261 13.2.2.4.
					Once the ACK has been constructed, the procedures of [4] are used to
					determine the destination address, port and transport.  However, the
					request is passed to the transport layer directly for transmission,
					rather than a client transaction.
				*/
                try {
                    dialog.Stack.TransportLayer.SendRequest(ack);
                } catch(Exception x) {
                    // Log
                    if(dialog.Stack.Logger != null) {
                        dialog.Stack.Logger.AddText("Dialog [id='" + dialog.ID + "'] ACK send for 2xx response failed: " + x.Message + ".");
                    }
                }
            }
        }
        #endregion

        #region ***处理主叫被叫
        private static void m_fGetErEe(SIP_RequestReceivedEventArgs e, out string m_sCaller, out string m_sCallee)
        {
            m_sCaller = "Unknown";
            m_sCallee = "Unknown";
            try
            {
                SIP_t_From from = e.Request.From;
                if (from.Address.DisplayName.Contains("To"))
                {
                    string[] m_lCallErEe = from.Address.DisplayName.Split(new string[] { "To" }, StringSplitOptions.None);
                    m_sCaller = m_lCallErEe[0];
                    m_sCallee = m_lCallErEe[1];
                }
                else
                {
                    string m_sCallerUri = from.Address.Uri.ToString();
                    if (from.Address.IsSipOrSipsUri) m_sCaller = m_sCallerUri.Split(':')[1].Split('@')[0];
                    else m_sCaller = m_sCallerUri;

                    SIP_t_To to = e.Request.To;
                    string m_sCalleeUri = to.Address.Uri.ToString();
                    if (to.Address.IsSipOrSipsUri) m_sCallee = m_sCalleeUri.Split(':')[1].Split('@')[0];
                    else m_sCallee = m_sCalleeUri;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoSip][SipStack][m_fGetErEe][Exception][{ex.Message}]");
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Common;
using DataBaseUtil;
using DataBaseModel;
using LumiSoft.Net.SIP.Message;
using LumiSoft.Net.RTP;
using LumiSoft.Net.SDP;
using LumiSoft.Net.Media;
using LumiSoft.Net.Media.Codec.Audio;
using LumiSoft.Net.SIP.Stack;
using LumiSoft.SIP.UA;
using System.IO;
using Core_v1;

namespace CenoSip {
    public class SipMain {

        #region UnKnow 内呼

        /* UnKnow 这个大体看了一样,好像是直接呼叫内部的点话,这里还是打算直接走服务器,根据前缀进行内呼外呼 */

        public static Dictionary<bool, string> Dial(string PhoneNumber) {
            Dictionary<bool, string> ResultData = new Dictionary<bool, string>();
            try {
                //create to address
                SIP_t_NameAddress to = new SIP_t_NameAddress("sip:" + PhoneNumber + "@" + Account.HostAddess);
                if(!to.IsSipOrSipsUri)
                    throw new ArgumentException("to: is not sip uri.");

                //create from address
                SIP_t_NameAddress from = new SIP_t_NameAddress("sip:" + Account.AccountID + "@" + Account.HostAddess);
                if(!from.IsSipOrSipsUri)
                    throw new ArgumentException("From: is not SIP URI.");

                // Setup RTP session
                RTP_MultimediaSession rtpMultimediaSession = new RTP_MultimediaSession(RTP_Utils.GenerateCNAME());
                RTP_Session rtpSession = RtpStack.CreateRtpSession(rtpMultimediaSession);

                // Port search failed.
                if(rtpSession == null)
                    throw new Exception("Calling not possible, RTP session failed to allocate IP end points.");

                SipDebug.Debug_Rtp(rtpMultimediaSession);

                // Create SDP offer
                SDP_Message sdpOffer = new SDP_Message();
                sdpOffer.Version = "0";
                sdpOffer.Origin = new SDP_Origin("-", sdpOffer.GetHashCode(), 1, "IN", "IP4", System.Net.Dns.GetHostAddresses("")[0].ToString());
                sdpOffer.SessionName = "SIP Call";
                sdpOffer.Times.Add(new SDP_Time(0, 0));

                // Add 1 audio stream
                SDP_MediaDescription mediaStream = new SDP_MediaDescription(SDP_MediaTypes.audio, 0, 1, "RTP/AVP", null);

                rtpSession.NewReceiveStream += delegate (object s, RTP_ReceiveStreamEventArgs e) {
                    AudioOut_RTP audioOut = new AudioOut_RTP(SipParam.m_pAudioOutDevice, e.Stream, SipParam.m_pAudioCodecs);
                    audioOut.Start();
                    mediaStream.Tags["rtp_audio_out"] = audioOut;
                };

                if(!NATHandle.HandleNAT(mediaStream, rtpSession)) {
                    throw new Exception("Calling not possible, because of NAT or firewall restrictions.");
                }

                foreach(KeyValuePair<int, AudioCodec> entry in SipParam.m_pAudioCodecs) {
                    mediaStream.Attributes.Add(new SDP_Attribute("rtpmap", entry.Key + " " + entry.Value.Name + "/" + entry.Value.CompressedAudioFormat.SamplesPerSecond));
                    mediaStream.MediaFormats.Add(entry.Key.ToString());
                }
                mediaStream.Attributes.Add(new SDP_Attribute("ptime", "20"));
                mediaStream.Attributes.Add(new SDP_Attribute("sendrecv", ""));
                mediaStream.Tags["rtp_session"] = rtpSession;
                mediaStream.Tags["audio_codecs"] = SipParam.m_pAudioCodecs;
                sdpOffer.MediaDescriptions.Add(mediaStream);

                // Create INVITE request.
                SIP_Request invite = SipParam.m_pStack.CreateRequest(SIP_Methods.INVITE, to, from);
                invite.ContentType = "application/sdp";
                invite.Data = sdpOffer.ToByte();

                SIP_RequestSender sender = SipParam.m_pStack.CreateRequestSender(invite);
                /* z这里没用 */
                sender.ResponseReceived += new EventHandler<SIP_ResponseReceivedEventArgs>(SipStack.m_pCall_ResponseReceived);

                // Create call.
                SipParam.m_pCall = new SIP_Call(SipParam.m_pStack, sender, rtpMultimediaSession);
                SipParam.m_pCall.LocalSDP = sdpOffer;
                SipParam.m_pCall.StateChanged += new EventHandler(SipStack.m_pCall_StateChanged);

                // Start calling.
                sender.Start();

                SipParam.m_pPlayer.Stop();
                SipParam.m_pPlayer.Play(new FileStream("Audio\\Incalling.wav", FileMode.Open), 1);
                ResultData.Add(true, string.Empty);

            } catch(Exception ex) {
                LogFile.Write(typeof(SipMain), LOGLEVEL.ERROR, "dial fail", ex);
                ResultData.Add(false, ex.Message);
            }
            return ResultData;
        }
        #endregion

        #region 发送DTMF
        /// <summary>
        /// 不知道第二个参数什么用
        /// </summary>
        /// <param name="eventNo"></param>
        /// <param name="duration"></param>
        public static void SendDTMF(string eventNo, int duration = 160) {
            try
            {
                if (SipParam.m_pCall == null)
                {
                    Log.Instance.Fail($"[CenoSip][SipMain][SendDTMF][Exception][dtmf send fail:no m_pCall]");
                    return;
                }
                Log.Instance.Success($"[CenoSip][SipMain][SendDTMF][dtmf:{eventNo}]");
                SipParam.m_pCall.SendDTMF(eventNo, duration);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoSip][SipMain][SendDTMF][Exception][{ex.Message}]");
            }
        }
        #endregion

        #region 挂断
        public static void Teminate() {
            /* wwd需要添加 */
            if(SipParam.m_pCall == null) {
                LogFile.Write(typeof(SipMain), LOGLEVEL.INFO, "主动挂断,m_pCall不存在");
                return;
            }
            SipParam.m_pCall.Terminate("{C}hung up", true);
        }
        #endregion

        #region 媒体播放
        public static void Play(string AudioFile, int p_Count) {
            try {
                //Log.Instance.Success($"[CenoSip][SipMain][Play(string AudioFile, int p_Count)][{AudioFile}]");
                SipParam.m_pPlayer.Play(AudioFile, p_Count);
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoSip][SipMain][Play(string AudioFile, int p_Count)][Exception][绕过:{ex.Message}]");
            }
        }
        [Obsolete("请使用Play(string AudioFile, int p_Count),该方法会出现线程问题")]
        public static void Play(FileStream stream, int p_Count) {
            try {
                SipParam.m_pPlayer.Play(stream, p_Count);
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoSip][SipMain][Play(FileStream stream, int p_Count)][Exception][绕过:{ex.Message}]");
            }
        }
        #endregion

        #region 媒体停止
        public static void Stop() {
            try {
                //Log.Instance.Success($"[CenoSip][SipMain][Stop]");
                SipParam.m_pPlayer.Stop();
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoSip][SipMain][Stop][Exception][绕过:{ex.Message}]");
            }
        }
        #endregion

        #region ***获取账户信息,有追加内容
        public static void GetAccountInfo() {
            int? AgentID = Common.AgentInfo.ChannelID;
            Call_ChannelModel _Call_ChannelModel = Call_ChannelUtil.GetChannelSipInfo(AgentID.Value);
            Account.AccountID = _Call_ChannelModel.ChNum;
            Account.DisplayName = _Call_ChannelModel.ShowName;
            Account.DomainName = _Call_ChannelModel.DomainName;
            Account.Express = _Call_ChannelModel.RegTime;
            Account.Password = _Call_ChannelModel.ChPassword;
            if (_Call_ChannelModel.SipServerIp.Contains(':')) {
                Account.HostAddess = _Call_ChannelModel.SipServerIp.Split(':')[0];
                Account.ServerSipPort = CommonClassLib.StringIsNumber(_Call_ChannelModel.SipServerIp.Split(':')[1]) ? int.Parse(_Call_ChannelModel.SipServerIp.Split(':')[1]) : 5060;
            }
            Account.LocalSipPort = _Call_ChannelModel.SipPort;

            ///<![CDATA[
            /// IP话机,添加字段,是否注册客户端
            /// ]]>
            CCFactory.IsRegister = Call_ChannelUtil.m_fGetIsRegister(AgentID);

            ///<![CDATA[
            /// Redis配置,如何获取共享号码
            /// ]]>
            switch (Call_ParamUtil.m_uShareNumSetting)
            {
                case 1:
                    //如果为主域,那么直接加载本机即可
                    m_cEsyMySQL.m_fGetDialArea();
                    //Redis
                    SipMain.m_fLoadRedis(true);
                    break;
                case 2:
                    //如果是副域,那么直接找已加入的
                    m_cEsyMySQL.m_fGetDialArea();
                    //Redis
                    SipMain.m_fLoadRedis(false);
                    break;
                default:
                    Redis2.use = false;
                    break;
            }
        }
        #endregion

        #region ***Redis
        private static void m_fLoadRedis(bool m_bMain)
        {
            new Thread(new ThreadStart(() =>
            {
                try
                {
                    string m_sRedisConfig = string.Empty;
                    if (m_bMain)
                    {
                        Redis2.use = Call_ParamUtil.m_bIsHasRedis;
                        m_sRedisConfig = Call_ParamUtil.GetParamValueByName("RedisConfig");
                    }
                    else
                    {
                        string m_sConnStr = MySQLDBConnectionString.m_fConnStr(Redis2.m_EsyMainDialArea);
                        Redis2.use = Call_ParamUtil.GetParamValueByName("IsHasRedis", "0", m_sConnStr) == "1";
                        m_sRedisConfig = Call_ParamUtil.GetParamValueByName("RedisConfig", $"{Redis2.m_EsyMainDialArea.aip}:{Redis2.defaultPort};123456;15", m_sConnStr)?.Replace(Redis2.defaultHost, Redis2.m_EsyMainDialArea.aip);
                    }

                    if (Redis2.use)
                    {
                        string[] m_lRedisConfig = m_sRedisConfig.Split(';');
                        if (m_lRedisConfig.Count() >= 2)
                        {
                            string password = m_lRedisConfig[1];
                            Redis2.password = string.IsNullOrWhiteSpace(password) ? null : password;
                        }
                        if (m_lRedisConfig.Count() >= 3)
                        {
                            int m_uDb = Redis2.defaultDb;
                            int.TryParse(m_lRedisConfig[2], out m_uDb);
                            Redis2.db = m_uDb;
                        }
                        string[] m_lHostPort = m_lRedisConfig[0].Split(':');
                        Redis2.host = m_lHostPort[0];
                        int.TryParse(m_lHostPort[1], out Redis2.port);
                        Log.Instance.Success($"[CenoSip][SipMain][m_fLoadRedis][{Redis2.host}:{Redis2.port}]");
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[CenoSip][SipMain][m_fLoadRedis][Exception][{ex.Message}]");
                    Redis2.use = false;
                }
            })).Start();
        }
        #endregion
    }
}

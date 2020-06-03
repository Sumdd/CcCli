using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumiSoft.Net.Media;
using LumiSoft.Net.Media.Codec.Audio;
using LumiSoft.Net.SDP;
using LumiSoft.Net.UPnP.NAT;
using LumiSoft.Net.SIP.Stack;
using LumiSoft.SIP.UA;
using DataBaseUtil;

namespace CenoSip {
    public class SipParam {

        public static UPnP_NAT_Client m_pUPnP;
        public static SIP_Stack m_pStack;
        public static WavePlayer m_pPlayer;
        public static SIP_Call m_pCall {
            get; set;
        }
        public static bool m_Sip_IsDebug = false;
        public static bool m_Rtp_IsDebug = false;
        public static bool m_IsClosing;


        public static int m_RtpBasePort = 21240;

        #region 扬声器设备
        private static AudioOutDevice _m_pAudioOutDevice;
        public static AudioOutDevice m_pAudioOutDevice {
            get {
                if(_m_pAudioOutDevice != null)
                    return _m_pAudioOutDevice;
                else {
                    if(AudioOut.Devices.Length > 0)
                        return AudioOut.Devices[0];
                    else
                        return null;
                }
            }
            set {
                _m_pAudioOutDevice = value;
            }
        }
        public static AudioOutDevice[] AudioOutDevices {
            get {
                return AudioOut.Devices;
            }
        }
        #endregion

        #region 麦克风设备
        private static AudioInDevice _m_pAudioInDevice;
        public static AudioInDevice m_pAudioInDevice {
            get {
                if(_m_pAudioInDevice != null)
                    return _m_pAudioInDevice;
                else {
                    if(AudioIn.Devices.Length > 0)
                        return AudioIn.Devices[0];
                    else
                        return null;
                }
            }
            set {
                _m_pAudioInDevice = value;
            }
        }
        public static AudioInDevice[] AudioInDevices {
            get {
                return AudioIn.Devices;
            }
        }
        #endregion

        private static Dictionary<int, AudioCodec> _m_pAudioCodecs;
        public static Dictionary<int, AudioCodec> m_pAudioCodecs {
            get {
                if(_m_pAudioCodecs != null)
                    return _m_pAudioCodecs;
                else {
                    _m_pAudioCodecs = new Dictionary<int, AudioCodec>();
                    _m_pAudioCodecs.Add(0, new PCMU());
                    _m_pAudioCodecs.Add(8, new PCMA());
                    return _m_pAudioCodecs;
                }
            }
            set {
                _m_pAudioCodecs = value;
            }
        }

        private static string _m_StunServer = Call_ParamUtil.GetStunServer;
        public static string m_StunServer {
            get {
                return _m_StunServer;
            }
            set {
                _m_StunServer = value;
            }
        }

        private static int? _m_StunServerPort = Call_ParamUtil.GetStunPort;
        public static int m_StunServerPort {
            get {
                if(!_m_StunServerPort.HasValue)
                    return 3478;
                return _m_StunServerPort.Value;
            }
            set {
                _m_StunServerPort = value;
            }
        }

        private static string _m_NatHandlingType = Call_ParamUtil.GetNat;
        public static string m_NatHandlingType {
            get {
                if(string.IsNullOrEmpty(_m_NatHandlingType)) {
                    _m_NatHandlingType = "no_nat";
                    return _m_NatHandlingType;
                } else {
                    return _m_NatHandlingType;
                }
            }
            set {
                _m_NatHandlingType = value;
            }
        }
    }
}

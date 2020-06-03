using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumiSoft.Net.SDP;
using LumiSoft.Net.RTP;
using System.Net;
using LumiSoft.Net;
using LumiSoft.Net.UPnP.NAT;
using Core_v1;

namespace CenoSip {
    class NATHandle {
        /// <summary>
        /// Handles NAT and stores RTP data to <b>mediaStream</b>.
        /// </summary>
        /// <param name="mediaStream">SDP media stream.</param>
        /// <param name="rtpSession">RTP session.</param>
        /// <returns>Returns true if NAT handled ok, otherwise false.</returns>
        public static bool HandleNAT(SDP_MediaDescription mediaStream, RTP_Session rtpSession) {
            if(mediaStream == null) {
                throw new ArgumentNullException("mediaStream");
            }
            if(rtpSession == null) {
                throw new ArgumentNullException("rtpSession");
            }

            IPEndPoint rtpPublicEP = null;
            IPEndPoint rtcpPublicEP = null;

            // We have public IP.
            if(!Net_Utils.IsPrivateIP(rtpSession.LocalEP.IP)) {
                rtpPublicEP = rtpSession.LocalEP.RtpEP;
                rtcpPublicEP = rtpSession.LocalEP.RtcpEP;
            } else {
                Log.Instance.Success($"[CenoSip][NATHandle][HandleNAT][{SipParam.m_NatHandlingType}]");

                switch(SipParam.m_NatHandlingType) {
                    case "no_nat":
                        rtpPublicEP = rtpSession.LocalEP.RtpEP;
                        rtcpPublicEP = rtpSession.LocalEP.RtcpEP;
                        Log.Instance.Success($"[CenoSip][NATHandle][HandleNAT][no_nat][{rtpPublicEP}]");
                        Log.Instance.Success($"[CenoSip][NATHandle][HandleNAT][no_nat][{rtcpPublicEP}]");
                        break;
                    case "stun":
                        rtpSession.StunPublicEndPoints(SipParam.m_StunServer, SipParam.m_StunServerPort, out rtpPublicEP, out rtcpPublicEP);
                        Log.Instance.Success($"[CenoSip][NATHandle][HandleNAT][stun][{rtpPublicEP}]");
                        Log.Instance.Success($"[CenoSip][NATHandle][HandleNAT][stun][{rtcpPublicEP}]");
                        break;
                    case "upnp":
                        if(!SipParam.m_pUPnP.IsSupported)
                            break;
                        int rtpPublicPort = rtpSession.LocalEP.RtpEP.Port;
                        int rtcpPublicPort = rtpSession.LocalEP.RtcpEP.Port;

                        try {
                            UPnP_NAT_Map[] maps = SipParam.m_pUPnP.GetPortMappings();
                            while(true) {
                                bool conficts = false;
                                // Check that some other application doesn't use that port.
                                foreach(UPnP_NAT_Map map in maps) {
                                    // Existing map entry conflicts.
                                    if(Convert.ToInt32(map.ExternalPort) == rtpPublicPort || Convert.ToInt32(map.ExternalPort) == rtcpPublicPort) {
                                        rtpPublicPort += 2;
                                        rtcpPublicPort += 2;
                                        conficts = true;

                                        break;
                                    }
                                }
                                if(!conficts) {
                                    break;
                                }
                            }

                            SipParam.m_pUPnP.AddPortMapping(true, "LS RTP", "UDP", null, rtpPublicPort, rtpSession.LocalEP.RtpEP, 0);
                            SipParam.m_pUPnP.AddPortMapping(true, "LS RTCP", "UDP", null, rtcpPublicPort, rtpSession.LocalEP.RtcpEP, 0);

                            IPAddress publicIP = SipParam.m_pUPnP.GetExternalIPAddress();

                            rtpPublicEP = new IPEndPoint(publicIP, rtpPublicPort);
                            rtcpPublicEP = new IPEndPoint(publicIP, rtcpPublicPort);

                            mediaStream.Tags.Add("upnp_rtp_map", new UPnP_NAT_Map(true, "UDP", "", rtpPublicPort.ToString(), rtpSession.LocalEP.IP.ToString(), rtpSession.LocalEP.RtpEP.Port, "LS RTP", 0));
                            mediaStream.Tags.Add("upnp_rtcp_map", new UPnP_NAT_Map(true, "UDP", "", rtcpPublicPort.ToString(), rtpSession.LocalEP.IP.ToString(), rtpSession.LocalEP.RtcpEP.Port, "LS RTCP", 0));
                        } catch {

                        }
                        Log.Instance.Success($"[CenoSip][NATHandle][HandleNAT][upnp][{rtpPublicEP}]");
                        Log.Instance.Success($"[CenoSip][NATHandle][HandleNAT][upnp][{rtcpPublicEP}]");
                        break;
                    default:
                        break;
                }
            }

            if(rtpPublicEP != null && rtcpPublicEP != null) {
                mediaStream.Port = rtpPublicEP.Port;
                if((rtpPublicEP.Port + 1) != rtcpPublicEP.Port) {
                    // Remove old rport attribute, if any.
                    for(int i = 0; i < mediaStream.Attributes.Count; i++) {
                        if(string.Equals(mediaStream.Attributes[i].Name, "rport", StringComparison.InvariantCultureIgnoreCase)) {
                            mediaStream.Attributes.RemoveAt(i);
                            i--;
                        }
                    }
                    mediaStream.Attributes.Add(new SDP_Attribute("rport", rtcpPublicEP.Port.ToString()));
                }
                mediaStream.Connection = new SDP_Connection("IN", "IP4", rtpPublicEP.Address.ToString());

                return true;
            }

            return false;
        }

    }
}

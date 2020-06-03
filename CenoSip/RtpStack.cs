using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumiSoft.Net.RTP;
using System.Net;
using LumiSoft.Net.Media.Codec.Audio;
using LumiSoft.Net.SDP;
using LumiSoft.Net.SIP.Stack;
using LumiSoft.Net.Media;
using LumiSoft.SIP.UA;
using LumiSoft.Net.UPnP.NAT;
using Core_v1;

namespace CenoSip
{
	public class RtpStack
	{
        public static E_Hold DO_E_Hold_ON = null;
        public static E_Hold DO_E_Hold_OFF = null;
        public delegate void E_Hold();

        public static void DO_F_Hold_ON()
        {
            DO_E_Hold_ON?.Invoke();
        }

        public static void DO_F_Hold_OFF()
        {
            DO_E_Hold_OFF?.Invoke();
        }

        /// <summary>
        /// Creates new RTP session.
        /// </summary>
        /// <param name="rtpMultimediaSession">RTP multimedia session.</param>
        /// <returns>Returns created RTP session or null if failed to create RTP session.</returns>
        /// <exception cref="ArgumentNullException">Is raised <b>rtpMultimediaSession</b> is null reference.</exception>
        public static RTP_Session CreateRtpSession(RTP_MultimediaSession rtpMultimediaSession)
		{
			if (rtpMultimediaSession == null)
			{
				throw new ArgumentNullException("rtpMultimediaSession");
			}

			//--- Search RTP IP -------------------------------------------------------//
			IPAddress rtpIP = null;

            //foreach (IPAddress ip in Dns.GetHostAddresses(""))
            //{
            //    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            //    {
            //        rtpIP = ip;
            //        break;
            //    }
            //}

            //修正媒体流IP地址
            rtpIP = IPAddress.Parse(Common.CommonParam.GetLocalIpAddress);

            if (rtpIP == null)
			{
				throw new Exception("None of the network connection is available.");
			}
			//------------------------------------------------------------------------//

			// Search free ports for RTP session.
			for (int i = 0; i < 100; i += 2)
			{
				try
				{
					return rtpMultimediaSession.CreateSession(new RTP_Address(rtpIP, SipParam.m_RtpBasePort, SipParam.m_RtpBasePort + 1), new RTP_Clock(1, 8000));
				}
				catch
				{
                    SipParam.m_RtpBasePort += 2;

                    //Log.Instance.Debug($"端口号递增{SipParam.m_RtpBasePort}");
				}
			}

			return null;
		}

		/// <summary>
		/// Processes media answer.
		/// </summary>
		/// <param name="call">SIP call.</param>
		/// <param name="offer">SDP media offer.</param>
		/// <param name="answer">SDP remote-party meida answer.</param>
		/// <exception cref="ArgumentNullException">Is raised when <b>call</b>,<b>offer</b> or <b>answer</b> is null reference.</exception>
		public static void ProcessMediaAnswer(SIP_Call call, SDP_Message offer, SDP_Message answer)
		{
			if (call == null)
			{
				throw new ArgumentNullException("call");
			}
			if (offer == null)
			{
				throw new ArgumentNullException("offer");
			}
			if (answer == null)
			{
				throw new ArgumentNullException("answer");
			}

			try
			{
				#region SDP basic validation

				// Version field must exist.
				if (offer.Version == null)
				{
					call.Terminate("Invalid SDP answer: Required 'v'(Protocol Version) field is missing.");

					return;
				}

				// Origin field must exist.
				if (offer.Origin == null)
				{
					call.Terminate("Invalid SDP answer: Required 'o'(Origin) field is missing.");

					return;
				}

				// Session Name field.

				// Check That global 'c' connection attribute exists or otherwise each enabled media stream must contain one.
				if (offer.Connection == null)
				{
					for (int i = 0; i < offer.MediaDescriptions.Count; i++)
					{
						if (offer.MediaDescriptions[i].Connection == null)
						{
							call.Terminate("Invalid SDP answer: Global or per media stream no: " + i + " 'c'(Connection) attribute is missing.");

							return;
						}
					}
				}


				// Check media streams count.
				if (offer.MediaDescriptions.Count != answer.MediaDescriptions.Count)
				{
					call.Terminate("Invalid SDP answer, media descriptions count in answer must be equal to count in media offer (RFC 3264 6.).");

					return;
				}

				#endregion

				// Process media streams info.
				for (int i = 0; i < offer.MediaDescriptions.Count; i++)
				{
					SDP_MediaDescription offerMedia = offer.MediaDescriptions[i];
					SDP_MediaDescription answerMedia = answer.MediaDescriptions[i];

					// Remote-party disabled this stream.
					if (answerMedia.Port == 0)
					{

						#region Cleanup active RTP stream and it's resources, if it exists

						// Dispose existing RTP session.
						if (offerMedia.Tags.ContainsKey("rtp_session"))
						{
							((RTP_Session)offerMedia.Tags["rtp_session"]).Dispose();
							offerMedia.Tags.Remove("rtp_session");
						}

						// Release UPnPports if any.
						if (offerMedia.Tags.ContainsKey("upnp_rtp_map"))
						{
							try
							{
								SipParam.m_pUPnP.DeletePortMapping((UPnP_NAT_Map)offerMedia.Tags["upnp_rtp_map"]);
							}
							catch
							{
							}
							offerMedia.Tags.Remove("upnp_rtp_map");
						}
						if (offerMedia.Tags.ContainsKey("upnp_rtcp_map"))
						{
							try
							{
								SipParam.m_pUPnP.DeletePortMapping((UPnP_NAT_Map)offerMedia.Tags["upnp_rtcp_map"]);
							}
							catch
							{
							}
							offerMedia.Tags.Remove("upnp_rtcp_map");
						}

						#endregion
					}
					// Remote-party accepted stream.
					else
					{
						Dictionary<int, AudioCodec> audioCodecs = (Dictionary<int, AudioCodec>)offerMedia.Tags["audio_codecs"];

						#region Validate stream-mode disabled,inactive,sendonly,recvonly

						/* RFC 3264 6.1.
                            If a stream is offered as sendonly, the corresponding stream MUST be
                            marked as recvonly or inactive in the answer.  If a media stream is
                            listed as recvonly in the offer, the answer MUST be marked as
                            sendonly or inactive in the answer.  If an offered media stream is
                            listed as sendrecv (or if there is no direction attribute at the
                            media or session level, in which case the stream is sendrecv by
                            default), the corresponding stream in the answer MAY be marked as
                            sendonly, recvonly, sendrecv, or inactive.  If an offered media
                            stream is listed as inactive, it MUST be marked as inactive in the
                            answer.
                        */

						// If we disabled this stream in offer and answer enables it (no allowed), terminate call.
						if (offerMedia.Port == 0)
						{
							call.Terminate("Invalid SDP answer, you may not enable sdp-offer disabled stream no: " + i + " (RFC 3264 6.).");

							return;
						}

						RTP_StreamMode offerStreamMode = GetRtpStreamMode(offer, offerMedia);
						RTP_StreamMode answerStreamMode = GetRtpStreamMode(answer, answerMedia);
						if (offerStreamMode == RTP_StreamMode.Send && answerStreamMode != RTP_StreamMode.Receive)
						{
							call.Terminate("Invalid SDP answer, sdp stream no: " + i + " stream-mode must be 'recvonly' (RFC 3264 6.).");

							return;
						}
						if (offerStreamMode == RTP_StreamMode.Receive && answerStreamMode != RTP_StreamMode.Send)
						{
							call.Terminate("Invalid SDP answer, sdp stream no: " + i + " stream-mode must be 'sendonly' (RFC 3264 6.).");

							return;
						}
						if (offerStreamMode == RTP_StreamMode.Inactive && answerStreamMode != RTP_StreamMode.Inactive)
						{
							call.Terminate("Invalid SDP answer, sdp stream no: " + i + " stream-mode must be 'inactive' (RFC 3264 6.).");

							return;
						}

						#endregion

						#region Create/modify RTP session

						RTP_Session rtpSession = (RTP_Session)offerMedia.Tags["rtp_session"];
						rtpSession.Payload = Convert.ToInt32(answerMedia.MediaFormats[0]);
						rtpSession.StreamMode = (answerStreamMode == RTP_StreamMode.Inactive ? RTP_StreamMode.Inactive : offerStreamMode);
						rtpSession.RemoveTargets();
						if (GetSdpHost(answer, answerMedia) != "0.0.0.0")
						{
							rtpSession.AddTarget(GetRtpTarget(answer, answerMedia));
						}
						rtpSession.Start();

						#endregion

						#region Create/modify audio-in source

						if (!offerMedia.Tags.ContainsKey("rtp_audio_in"))
						{
							AudioIn_RTP rtpAudioIn = new AudioIn_RTP(SipParam.m_pAudioInDevice, 20, audioCodecs, rtpSession.CreateSendStream());
							rtpAudioIn.Start();
							offerMedia.Tags.Add("rtp_audio_in", rtpAudioIn);
						}

						#endregion
					}
				}

				call.LocalSDP = offer;
				call.RemoteSDP = answer;
			}
			catch (Exception x)
			{
				call.Terminate("Error processing SDP answer: " + x.Message);
			}
		}

		/// <summary>
		/// Processes media offer.
		/// </summary>
		/// <param name="dialog">SIP dialog.</param>
		/// <param name="transaction">Server transaction</param>
		/// <param name="rtpMultimediaSession">RTP multimedia session.</param>
		/// <param name="offer">Remote-party SDP offer.</param>
		/// <param name="localSDP">Current local SDP.</param>
		/// <exception cref="ArgumentNullException">Is raised <b>dialog</b>,<b>transaction</b>,<b>rtpMultimediaSession</b>,<b>offer</b> or <b>localSDP</b> is null reference.</exception>
		public static void ProcessMediaOffer(SIP_Dialog dialog, SIP_ServerTransaction transaction, RTP_MultimediaSession rtpMultimediaSession, SDP_Message offer, SDP_Message localSDP)
		{
			if (dialog == null)
			{
				throw new ArgumentNullException("dialog");
			}
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (rtpMultimediaSession == null)
			{
				throw new ArgumentNullException("rtpMultimediaSession");
			}
			if (offer == null)
			{
				throw new ArgumentNullException("offer");
			}
			if (localSDP == null)
			{
				throw new ArgumentNullException("localSDP");
			}

			try
			{
				//bool onHold = m_pToggleOnHold.Text == "Unhold";

				#region SDP basic validation

				// Version field must exist.
				if (offer.Version == null)
				{
					transaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x500_Server_Internal_Error + ": Invalid SDP answer: Required 'v'(Protocol Version) field is missing.", transaction.Request));

					return;
				}

				// Origin field must exist.
				if (offer.Origin == null)
				{
					transaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x500_Server_Internal_Error + ": Invalid SDP answer: Required 'o'(Origin) field is missing.", transaction.Request));

					return;
				}

				// Session Name field.

				// Check That global 'c' connection attribute exists or otherwise each enabled media stream must contain one.
				if (offer.Connection == null)
				{
					for (int i = 0; i < offer.MediaDescriptions.Count; i++)
					{
						if (offer.MediaDescriptions[i].Connection == null)
						{
							transaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x500_Server_Internal_Error + ": Invalid SDP answer: Global or per media stream no: " + i + " 'c'(Connection) attribute is missing.", transaction.Request));

							return;
						}
					}
				}

				#endregion

				// Re-INVITE media streams count must be >= current SDP streams.
				if (localSDP.MediaDescriptions.Count > offer.MediaDescriptions.Count)
				{
					transaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x500_Server_Internal_Error + ": re-INVITE SDP offer media stream count must be >= current session stream count.", transaction.Request));

					return;
				}

				bool audioAccepted = false;
				// Process media streams info.
				for (int i = 0; i < offer.MediaDescriptions.Count; i++)
				{
					SDP_MediaDescription offerMedia = offer.MediaDescriptions[i];
					SDP_MediaDescription answerMedia = (localSDP.MediaDescriptions.Count > i ? localSDP.MediaDescriptions[i] : null);

					// Disabled stream.
					if (offerMedia.Port == 0)
					{
						// Remote-party offered new disabled stream.
						if (answerMedia == null)
						{
							// Just copy offer media stream data to answer and set port to zero.
							localSDP.MediaDescriptions.Add(offerMedia);
							localSDP.MediaDescriptions[i].Port = 0;
						}
						// Existing disabled stream or remote party disabled it.
						else
						{
							answerMedia.Port = 0;

							#region Cleanup active RTP stream and it's resources, if it exists

							// Dispose existing RTP session.
							if (answerMedia.Tags.ContainsKey("rtp_session"))
							{
								((RTP_Session)offerMedia.Tags["rtp_session"]).Dispose();
								answerMedia.Tags.Remove("rtp_session");
							}

							// Release UPnPports if any.
							if (answerMedia.Tags.ContainsKey("upnp_rtp_map"))
							{
								try
								{
									SipParam.m_pUPnP.DeletePortMapping((UPnP_NAT_Map)answerMedia.Tags["upnp_rtp_map"]);
								}
								catch
								{
								}
								answerMedia.Tags.Remove("upnp_rtp_map");
							}
							if (answerMedia.Tags.ContainsKey("upnp_rtcp_map"))
							{
								try
								{
									SipParam.m_pUPnP.DeletePortMapping((UPnP_NAT_Map)answerMedia.Tags["upnp_rtcp_map"]);
								}
								catch
								{
								}
								answerMedia.Tags.Remove("upnp_rtcp_map");
							}

							#endregion
						}
					}
					// Remote-party wants to communicate with this stream.
					else
					{
						// See if we can support this stream.
						if (!audioAccepted && CanSupportMedia(offerMedia))
						{
							// New stream.
							if (answerMedia == null)
							{
								answerMedia = new SDP_MediaDescription(SDP_MediaTypes.audio, 0, 2, "RTP/AVP", null);
								localSDP.MediaDescriptions.Add(answerMedia);
							}

							#region Build audio codec map with codecs which we support

							Dictionary<int, AudioCodec> audioCodecs = GetOurSupportedAudioCodecs(offerMedia);
							answerMedia.MediaFormats.Clear();
							answerMedia.Attributes.Clear();
							foreach (KeyValuePair<int, AudioCodec> entry in audioCodecs)
							{
								answerMedia.Attributes.Add(new SDP_Attribute("rtpmap", entry.Key + " " + entry.Value.Name + "/" + entry.Value.CompressedAudioFormat.SamplesPerSecond));
								answerMedia.MediaFormats.Add(entry.Key.ToString());
							}

                            //answerMedia.MediaFormats.Add("101");
                            //answerMedia.Attributes.Add(new SDP_Attribute("rtpmap", "101 telephone-event/8000"));
                            //answerMedia.Attributes.Add(new SDP_Attribute("fmtp", "101 0-15"));

                            answerMedia.Attributes.Add(new SDP_Attribute("ptime", "20"));
							answerMedia.Tags["audio_codecs"] = audioCodecs;

							#endregion

							#region Create/modify RTP session

							// RTP session doesn't exist, create it.
							if (!answerMedia.Tags.ContainsKey("rtp_session"))
							{
								RTP_Session rtpSess = CreateRtpSession(rtpMultimediaSession);
								// RTP session creation failed,disable this stream.
								if (rtpSess == null)
								{
									answerMedia.Port = 0;

									break;
								}
								answerMedia.Tags.Add("rtp_session", rtpSess);

								rtpSess.NewReceiveStream += delegate(object s, RTP_ReceiveStreamEventArgs e)
								{
                                    AudioOut_RTP audioOut = new AudioOut_RTP(SipParam.m_pAudioOutDevice, e.Stream, audioCodecs);
                                    audioOut.Start();
                                    answerMedia.Tags["rtp_audio_out"] = audioOut;
                                };

								// NAT
								if (!NATHandle.HandleNAT(answerMedia, rtpSess))
								{
									// NAT handling failed,disable this stream.
									answerMedia.Port = 0;

									break;
								}
							}

							RTP_StreamMode offerStreamMode = GetRtpStreamMode(offer, offerMedia);
							switch (offerStreamMode)
							{
								case RTP_StreamMode.Inactive:
									answerMedia.SetStreamMode("inactive");
									break;
								case RTP_StreamMode.Receive:
									answerMedia.SetStreamMode("sendonly");
									break;
								case RTP_StreamMode.Send:
									//we only have rtcp data sent when we holding
									//if (onHold)
									//    answerMedia.SetStreamMode("inactive");
									//else
									answerMedia.SetStreamMode("recvonly");
									break;
								case RTP_StreamMode.SendReceive:
									//we only have rtcp data sent when we holding
									//if (onHold)
									//    answerMedia.SetStreamMode("inactive");
									//else
									answerMedia.SetStreamMode("sendrecv");
									break;
							}

							RTP_Session rtpSession = (RTP_Session)answerMedia.Tags["rtp_session"];
							rtpSession.Payload = Convert.ToInt32(answerMedia.MediaFormats[0]);
							rtpSession.StreamMode = GetRtpStreamMode(localSDP, answerMedia);
							rtpSession.RemoveTargets();
							if (GetSdpHost(offer, offerMedia) != "0.0.0.0")
							{
								rtpSession.AddTarget(GetRtpTarget(offer, offerMedia));
							}
							rtpSession.Start();

							#endregion

							#region Create/modify audio-in source

							if (!answerMedia.Tags.ContainsKey("rtp_audio_in"))
							{
								AudioIn_RTP rtpAudioIn = new AudioIn_RTP(SipParam.m_pAudioInDevice, 20, audioCodecs, rtpSession.CreateSendStream());
								rtpAudioIn.Start();
								answerMedia.Tags.Add("rtp_audio_in", rtpAudioIn);

                                RtpStack.DO_E_Hold_ON += () =>
                                {
                                    Log.Instance.Success($"hold on rtp_audio_in,{WaveReset.WaveInResetFlag = true},start");
                                    rtpAudioIn?.Stop();
                                    Log.Instance.Success($"hold on rtp_audio_in,{WaveReset.WaveInResetFlag = false},end");
                                    SipMain.Play("Audio\\a_keep.wav", 20);
                                };
                                RtpStack.DO_E_Hold_OFF += () =>
                                {
                                    //必须重写实例
                                    rtpAudioIn = new AudioIn_RTP(SipParam.m_pAudioInDevice, 20, audioCodecs, rtpSession.CreateSendStream());
                                    rtpAudioIn.Start();
                                    SipMain.Stop();
                                };
                            }
							else
							{
                                AudioIn_RTP rtpAudioIn = ((AudioIn_RTP)answerMedia.Tags["rtp_audio_in"]);
                                rtpAudioIn.AudioCodecs = audioCodecs;

                                RtpStack.DO_E_Hold_ON += () =>
                                {
                                    Log.Instance.Success($"else hold on rtp_audio_in,{WaveReset.WaveInResetFlag = true},start");
                                    rtpAudioIn?.Stop();
                                    Log.Instance.Success($"else hold on rtp_audio_in,{WaveReset.WaveInResetFlag = false},end");
                                    SipMain.Play("Audio\\a_keep.wav", 20);
                                };
                                RtpStack.DO_E_Hold_OFF += () =>
                                {
                                    //必须重写实例
                                    rtpAudioIn = new AudioIn_RTP(SipParam.m_pAudioInDevice, 20, audioCodecs, rtpSession.CreateSendStream());
                                    rtpAudioIn.Start();
                                    SipMain.Stop();
                                };
                            }

							#endregion

							audioAccepted = true;
						}
						// We don't accept this stream, so disable it.
						else
						{
							// Just copy offer media stream data to answer and set port to zero.

							// Delete exisiting media stream.
							if (answerMedia != null)
							{
								localSDP.MediaDescriptions.RemoveAt(i);
							}
							localSDP.MediaDescriptions.Add(offerMedia);
							localSDP.MediaDescriptions[i].Port = 0;
						}
					}
				}

				#region Create and send 2xx response

				SIP_Response response = SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x200_Ok, transaction.Request, transaction.Flow);
				//response.Contact = SIP stack will allocate it as needed;
				response.ContentType = "application/sdp";
				response.Data = localSDP.ToByte();

				transaction.SendResponse(response);

				// Start retransmitting 2xx response, while ACK receives.
				SipStack.Handle2xx(dialog, transaction);

				// REMOVE ME: 27.11.2010
				// Start retransmitting 2xx response, while ACK receives.
				//m_pInvite2xxMgr.Add(dialog,transaction);

				#endregion
			}
			catch (Exception x)
			{
				transaction.SendResponse(SipParam.m_pStack.CreateResponse(SIP_ResponseCodes.x500_Server_Internal_Error + ": " + x.Message, transaction.Request));
			}
		}

		/// <summary>
		/// Checks if we can support the specified media.
		/// </summary>
		/// <param name="media">SDP media.</param>
		/// <returns>Returns true if we can support this media, otherwise false.</returns>
		/// <exception cref="ArgumentNullException">Is raised when <b>media</b> is null reference.</exception>
		public static bool CanSupportMedia(SDP_MediaDescription media)
		{
			if (media == null)
			{
				throw new ArgumentNullException("media");
			}

			if (!string.Equals(media.MediaType, SDP_MediaTypes.audio, StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}

            if (!string.Equals(media.Protocol, "RTP/AVP", StringComparison.InvariantCultureIgnoreCase))
            {
                //return false;

                if (!string.Equals(media.Protocol, "UDP/TLS/RTP/SAVPF", StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }

            }

            if (GetOurSupportedAudioCodecs(media).Count > 0)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Gets audio codecs which we can support from SDP media stream.
		/// </summary>
		/// <param name="media">SDP media stream.</param>
		/// <exception cref="ArgumentNullException">Is raised when <b>media</b> is null reference.</exception>
		/// <returns>Returns audio codecs which support.</returns>
		private static Dictionary<int, AudioCodec> GetOurSupportedAudioCodecs(SDP_MediaDescription media)
		{
			if (media == null)
			{
				throw new ArgumentNullException("media");
			}

			Dictionary<int, AudioCodec> codecs = new Dictionary<int, AudioCodec>();

			// Check for IANA registered payload. Custom range is 96-127 and always must have rtpmap attribute.
			foreach (string format in media.MediaFormats)
			{
				int payload = Convert.ToInt32(format);
				if (payload < 96 && SipParam.m_pAudioCodecs.ContainsKey(payload))
				{
					if (!codecs.ContainsKey(payload))
					{
						codecs.Add(payload, SipParam.m_pAudioCodecs[payload]);
					}
				}
			}
			// Check rtpmap payloads.
			foreach (SDP_Attribute a in media.Attributes)
			{
				if (string.Equals(a.Name, "rtpmap", StringComparison.InvariantCultureIgnoreCase))
				{
					// Example: 0 PCMU/8000
					string[] parts = a.Value.Split(' ');
					int payload = Convert.ToInt32(parts[0]);
					string codecName = parts[1].Split('/')[0];

					foreach (AudioCodec codec in SipParam.m_pAudioCodecs.Values)
					{
						if (string.Equals(codec.Name, codecName, StringComparison.InvariantCultureIgnoreCase))
						{
							if (!codecs.ContainsKey(payload))
							{
								codecs.Add(payload, codec);
							}
						}
					}
				}
			}

			return codecs;
		}

		/// <summary>
		/// Gets RTP stream mode.
		/// </summary>
		/// <param name="sdp">SDP message.</param>
		/// <param name="media">SDP media.</param>
		/// <returns>Returns RTP stream mode.</returns>
		/// <exception cref="ArgumentNullException">Is raised when <b>sdp</b> or <b>media</b> is null reference.</exception>
		private static RTP_StreamMode GetRtpStreamMode(SDP_Message sdp, SDP_MediaDescription media)
		{
			if (sdp == null)
			{
				throw new ArgumentNullException("sdp");
			}
			if (media == null)
			{
				throw new ArgumentNullException("media");
			}

			// Try to get per media stream mode.
			foreach (SDP_Attribute a in media.Attributes)
			{
				if (string.Equals(a.Name, "sendrecv", StringComparison.InvariantCultureIgnoreCase))
				{
					return RTP_StreamMode.SendReceive;
				}
				else if (string.Equals(a.Name, "sendonly", StringComparison.InvariantCultureIgnoreCase))
				{
					return RTP_StreamMode.Send;
				}
				else if (string.Equals(a.Name, "recvonly", StringComparison.InvariantCultureIgnoreCase))
				{
					return RTP_StreamMode.Receive;
				}
				else if (string.Equals(a.Name, "inactive", StringComparison.InvariantCultureIgnoreCase))
				{
					return RTP_StreamMode.Inactive;
				}
			}

			// No per media stream mode, try to get per session stream mode.
			foreach (SDP_Attribute a in sdp.Attributes)
			{
				if (string.Equals(a.Name, "sendrecv", StringComparison.InvariantCultureIgnoreCase))
				{
					return RTP_StreamMode.SendReceive;
				}
				else if (string.Equals(a.Name, "sendonly", StringComparison.InvariantCultureIgnoreCase))
				{
					return RTP_StreamMode.Send;
				}
				else if (string.Equals(a.Name, "recvonly", StringComparison.InvariantCultureIgnoreCase))
				{
					return RTP_StreamMode.Receive;
				}
				else if (string.Equals(a.Name, "inactive", StringComparison.InvariantCultureIgnoreCase))
				{
					return RTP_StreamMode.Inactive;
				}
			}

			return RTP_StreamMode.SendReceive;
		}

		/// <summary>
		/// Gets SDP per media or global connection host.
		/// </summary>
		/// <param name="sdp">SDP message.</param>
		/// <param name="mediaStream">SDP media stream.</param>
		/// <returns>Returns SDP per media or global connection host.</returns>
		/// <exception cref="ArgumentNullException">Is raised when <b>sdp</b> or <b>mediaStream</b> is null reference.</exception>
		private static string GetSdpHost(SDP_Message sdp, SDP_MediaDescription mediaStream)
		{
			if (sdp == null)
			{
				throw new ArgumentNullException("sdp");
			}
			if (mediaStream == null)
			{
				throw new ArgumentNullException("mediaStream");
			}

			// We must have SDP global or per media connection info.
			string host = mediaStream.Connection != null ? mediaStream.Connection.Address : null;
			if (host == null)
			{
				host = sdp.Connection.Address != null ? sdp.Connection.Address : null;

				if (host == null)
				{
					throw new ArgumentException("Invalid SDP message, global or per media 'c'(Connection) attribute is missing.");
				}
			}

			return host;
		}

		/// <summary>
		/// Gets RTP target for SDP media stream.
		/// </summary>
		/// <param name="sdp">SDP message.</param>
		/// <param name="mediaStream">SDP media stream.</param>
		/// <returns>Return RTP target.</returns>
		/// <exception cref="ArgumentNullException">Is raised when <b>sdp</b> or <b>mediaStream</b> is null reference.</exception>
		private static RTP_Address GetRtpTarget(SDP_Message sdp, SDP_MediaDescription mediaStream)
		{
			if (sdp == null)
			{
				throw new ArgumentNullException("sdp");
			}
			if (mediaStream == null)
			{
				throw new ArgumentNullException("mediaStream");
			}

			// We must have SDP global or per media connection info.
			string host = mediaStream.Connection != null ? mediaStream.Connection.Address : null;
			if (host == null)
			{
				host = sdp.Connection.Address != null ? sdp.Connection.Address : null;

				if (host == null)
				{
					throw new ArgumentException("Invalid SDP message, global or per media 'c'(Connection) attribute is missing.");
				}
			}

			int remoteRtcpPort = mediaStream.Port + 1;
			// Use specified RTCP port, if specified.
			foreach (SDP_Attribute attribute in mediaStream.Attributes)
			{
				if (string.Equals(attribute.Name, "rtcp", StringComparison.InvariantCultureIgnoreCase))
				{
					remoteRtcpPort = Convert.ToInt32(attribute.Value);

					break;
				}
			}

			return new RTP_Address(System.Net.Dns.GetHostAddresses(host)[0], mediaStream.Port, remoteRtcpPort);
		}

	}
}

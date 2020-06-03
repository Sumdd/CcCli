using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LumiSoft.Net.RTP.Debug;
using LumiSoft.Net.RTP;
using LumiSoft.Net.SIP;
using LumiSoft.Net.SIP.Debug;

namespace CenoSip
{
	public class SipDebug
	{
		public static void Debug_Rtp(RTP_MultimediaSession rtpMultimediaSession)
		{
			if (SipParam.m_Rtp_IsDebug)
			{
				wfrm_RTP_Debug rtpDebug = new wfrm_RTP_Debug(rtpMultimediaSession);
				rtpDebug.Show();
			}
		}

		public static void Debug_Sip(LumiSoft.Net.SIP.Stack.SIP_Stack stack)
		{
			if (SipParam.m_Sip_IsDebug)
			{
				wfrm_SIP_Debug sipDebug = new wfrm_SIP_Debug(stack);
				sipDebug.Show();
			}
		}
	}
}

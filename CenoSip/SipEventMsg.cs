using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace CenoSip
{
	public class SipEventMsg
	{
		public const int SIP_EVENT_MESSAGE = (int)Win32API.WinMsg.WM_USER + 1000;

		public enum EventCode
		{
			SIP_INCOMING
		}
		
	}
}

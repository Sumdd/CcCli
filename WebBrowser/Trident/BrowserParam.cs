using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataBaseUtil;

namespace WebBrowser
{
	public class BrowserParam
	{
		private static string _HomeUrl;
		public static string HomeUrl
		{
			get
			{
				if (!string.IsNullOrEmpty(_HomeUrl))
					return _HomeUrl;
				return _HomeUrl = Call_ClientParamUtil.GetParamValueByName("HomeUrl");
			}
			set
			{
				Call_ClientParamUtil.SetParamValueByName("HomeUrl", _HomeUrl = value);
			}
		}

		private static string _ExtendUrl;
		public static string ExtendUrl
		{
			get
			{
				if (!string.IsNullOrEmpty(_ExtendUrl))
					return _ExtendUrl;
				return _ExtendUrl = Call_ClientParamUtil.GetParamValueByName("ExtendUrl");
			}
			set
			{
				Call_ClientParamUtil.SetParamValueByName("ExtendUrl", _ExtendUrl = value);
			}
		}

		private static string _AutoOpenPage;
		public static string AutoOpenPage
		{
			get
			{
				if (!string.IsNullOrEmpty(_AutoOpenPage))
					return _AutoOpenPage;
				return _AutoOpenPage = Call_ClientParamUtil.GetParamValueByName("AutoOpenPage");
			}
			set
			{
				Call_ClientParamUtil.SetParamValueByName("AutoOpenPage", _AutoOpenPage = value);
			}
		}
	}
}

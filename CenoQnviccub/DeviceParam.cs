using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataBaseUtil;

namespace CenoQnviccub
{
	class DeviceParam
	{

		private static string _D_MICVoiceValue;
		public static string D_MICVoiceValue
		{
			get
			{
				if (!string.IsNullOrEmpty(_D_MICVoiceValue))
					return _D_MICVoiceValue;
				return _D_MICVoiceValue = Call_ClientParamUtil.GetParamValueByName("D_MICVoiceValue");
			}
			set
			{
				Call_ClientParamUtil.SetParamValueByName("D_MICVoiceValue", _D_MICVoiceValue = value);
			}
		}

		private static string _D_SpkOutVoiceValue;
		public static string D_SpkOutVoiceValue
		{
			get
			{
				if (!string.IsNullOrEmpty(_D_SpkOutVoiceValue))
					return _D_SpkOutVoiceValue;
				return _D_SpkOutVoiceValue = Call_ClientParamUtil.GetParamValueByName("D_SpkOutVoiceValue");
			}
			set
			{
				Call_ClientParamUtil.SetParamValueByName("D_SpkOutVoiceValue", _D_SpkOutVoiceValue = value);
			}
		}

		private static string _D_DoPlayVoiceValue;
		public static string D_DoPlayVoiceValue
		{
			get
			{
				if (!string.IsNullOrEmpty(_D_DoPlayVoiceValue))
					return _D_DoPlayVoiceValue;
				return _D_DoPlayVoiceValue = Call_ClientParamUtil.GetParamValueByName("D_DoPlayVoiceValue");
			}
			set
			{
				Call_ClientParamUtil.SetParamValueByName("D_DoPlayVoiceValue", _D_DoPlayVoiceValue = value);
			}
		}

		private static string _D_LineOutVoiceValue;
		public static string D_LineOutVoiceValue
		{
			get
			{
				if (!string.IsNullOrEmpty(_D_LineOutVoiceValue))
					return _D_LineOutVoiceValue;
				return _D_LineOutVoiceValue = Call_ClientParamUtil.GetParamValueByName("D_LineOutVoiceValue");
			}
			set
			{
				Call_ClientParamUtil.SetParamValueByName("D_LineOutVoiceValue", _D_LineOutVoiceValue = value);
			}
		}

		private static string _D_LineInVoiceValue;
		public static string D_LineInVoiceValue
		{
			get
			{
				if (!string.IsNullOrEmpty(_D_LineInVoiceValue))
					return _D_LineInVoiceValue;
				return _D_LineInVoiceValue = Call_ClientParamUtil.GetParamValueByName("D_LineInVoiceValue");
			}
			set
			{
				Call_ClientParamUtil.SetParamValueByName("D_LineInVoiceValue", _D_LineInVoiceValue = value);
			}
		}
	}
}

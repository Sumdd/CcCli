using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CenoSip
{
	public static class Account
	{
		//账号ID  注册电话号码
		public static string AccountID { get; set; }
		//显示名
		public static string DisplayName { get; set; }
		//域名
		public static string DomainName { get; set; }
		//服务器地址
		public static string HostAddess { get; set; }
		//密码
		public static string Password { get; set; }

		public static int Express { get; set; }
		//待定
		public static bool Enabled { get; set; }

		public static int LocalSipPort { get; set; }

		public static int ServerSipPort { get; set; }
	}
}

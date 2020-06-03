///////////////////////////////////////////////////////////////////////////////////////
// 文件名   : C:\Users\zhongguan\Desktop\11111\NhibernateBag\Models\Call_ServerList.cs
// 类名     : Call_ServerList
// 中文名   : 
// 创建描述 : 
// 创建人   : 
// 创建时间 : 2015-11-10 16:54:14
// 版权信息 : 青岛天路信息技术有限责任公司  www.topdigi.com.cn
///////////////////////////////////////////////////////////////////////////////////////

using System;
namespace DataBaseModel
{
	/// <summary>
	/// 
	/// </summary>

	public class Call_ServerListModel
	{
		public  Call_ServerListModel()
		{
			    DateTime initDatetime = new DateTime(1900, 1, 1);
		}


		/// <summary>
		/// ID
		/// </summary>
		public virtual int ID { get; set; }
		/// <summary>
		/// 服务器序号
		/// </summary>
		public virtual int ServerIndex { get; set; }
		/// <summary>
		/// 服务器名称
		/// </summary>
		public virtual string ServerName { get; set; }
		/// <summary>
		/// 服务器IP
		/// </summary>
		public virtual string ServerIP { get; set; }
		/// <summary>
		/// 服务器端口
		/// </summary>
		public virtual int ServerPort { get; set; }
		/// <summary>
		/// 服务器域名
		/// </summary>
		public virtual string DomainName { get; set; }
		/// <summary>
		/// 服务器登录帐号
		/// </summary>
		public virtual string LoginName { get; set; }
		/// <summary>
		/// 服务器登录密码
		/// </summary>
		public virtual string Password { get; set; }
		/// <summary>
		/// 是否默认
		/// </summary>
		public virtual string IsDefault { get; set; }
		/// <summary>
		/// 连接时间
		/// </summary>
		public virtual int ConnectTime { get; set; }
		/// <summary>
		/// 系统参数表ID
		/// </summary>
		public virtual int ParamID { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public virtual string Remark { get; set; }
	}
 }

///////////////////////////////////////////////////////////////////////////////////////
// 文件名   : C:\Users\zhongguan\Desktop\11111\NhibernateBag\Models\Call_SocketCommand.cs
// 类名     : Call_PhoneAddressModel
// 中文名   : 
// 创建描述 : 
// 创建人   : 
// 创建时间 : 2015-11-10 11:43:14
// 版权信息 : 青岛新生代软件有限公司  www.ceno-soft.net
///////////////////////////////////////////////////////////////////////////////////////

using System;
namespace DataBaseModel
{
	/// <summary>
	/// 
	/// </summary>

	public class Call_AgentModel
	{
		public  Call_AgentModel()
		{
			    DateTime initDatetime = new DateTime(1900, 1, 1);
		}


		/// <summary>
		/// ID
		/// </summary>
		public virtual int ID { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>
		public virtual string AgentName { get; set; }
		/// <summary>
		/// 登录帐号
		/// </summary>
		public virtual string LoginName { get; set; }
		/// <summary>
		/// 登录密码
		/// </summary>
		public virtual string LoginPassWord { get; set; }
		/// <summary>
		/// 登录状态（0：未登录，1：已登录）
		/// </summary>
		public virtual int LoginState { get; set; }
		/// <summary>
		/// 坐席号码
		/// </summary>
		public virtual string AgentNumber { get; set; }
		/// <summary>
		/// 坐席密码
		/// </summary>
		public virtual string AgentPassword { get; set; }
		/// <summary>
		/// 最后登录IP地址
		/// </summary>
		public virtual string LastLoginIp { get; set; }
		/// <summary>
		/// 团队编号
		/// </summary>
		public virtual int TeamID { get; set; }
		/// <summary>
		/// 坐席当前状态
		/// </summary>
		public virtual int StateID { get; set; }
		/// <summary>
		/// 权限ID
		/// </summary>
		public virtual int RoleID { get; set; }
		/// <summary>
		/// 通道ID
		/// </summary>
		public virtual int ChannelID { get; set; }
		/// <summary>
		/// 客户端参数ID
		/// </summary>
		public virtual int ClientParamID { get; set; }
		/// <summary>
		/// 是否可用
		/// </summary>
		public virtual int Usable { get; set; }
		/// <summary>
		/// 是否关联坐席（0否，1是）
		/// </summary>
		public virtual int LinkUser { get; set; }
		/// <summary>
		/// 关联坐席登录名
		/// </summary>
		public virtual string LU_LoginName { get; set; }
		/// <summary>
		/// 关联坐席登录密码
		/// </summary>
		public virtual string LU_Password { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public virtual string Remark { get; set; }
	}
 }

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

	public class Call_ClientParamModel
	{
		public  Call_ClientParamModel()
		{
			    DateTime initDatetime = new DateTime(1900, 1, 1);
		}


		/// <summary>
		/// ID
		/// </summary>
		public virtual int ID { get; set; }
		/// <summary>
		/// 录音保存路径
		/// </summary>
		public virtual string SaveRecordPath { get; set; }
		/// <summary>
		/// 主页地址
		/// </summary>
		public virtual string HomeUrl { get; set; }
		/// <summary>
		/// 弹屏页地址
		/// </summary>
		public virtual string ExtendUrl { get; set; }
		/// <summary>
		/// IIS地址
		/// </summary>
		public virtual string IisPath { get; set; }
		/// <summary>
		/// 自动打开浏览器主页
		/// </summary>
		public virtual string AutoOpenPage { get; set; }
		/// <summary>
		/// 长途自动加拨号码标志
		/// </summary>
		public virtual string AutoAddNumDialFlag { get; set; }
		/// <summary>
		/// 长途自动加拨号码
		/// </summary>
		public virtual string AutoAddNumDial { get; set; }
		/// <summary>
		/// 本地自动加拨号码标志
		/// </summary>
		public virtual string AutoAddNumLocalDialFlag { get; set; }
		/// <summary>
		/// 本地自动加拨号码
		/// </summary>
		public virtual string AutoAddNumLocalDial { get; set; }
		/// <summary>
		/// 本地城市名称
		/// </summary>
		public virtual string LocalCity { get; set; }
		/// <summary>
		/// 本地区号
		/// </summary>
		public virtual string LocalCityCode { get; set; }
		/// <summary>
		/// 扩展数据库名
		/// </summary>
		public virtual string ExtendDataBase { get; set; }
		/// <summary>
		/// 扩展数据库登录名
		/// </summary>
		public virtual string EDB_UserID { get; set; }
		/// <summary>
		/// 扩展数据库登录密码
		/// </summary>
		public virtual string EDB_Password { get; set; }
		/// <summary>
		/// 硬盘检测服务器IP
		/// </summary>
		public virtual string DiskChkServer { get; set; }
		/// <summary>
		/// 硬盘检测服务器登录名
		/// </summary>
		public virtual string DiskChkLoginName { get; set; }
		/// <summary>
		/// 硬盘检测服务器登录密码
		/// </summary>
		public virtual string DiskChkPassword { get; set; }
		/// <summary>
		/// 硬盘检测服务器盘符
		/// </summary>
		public virtual string DiskChkSrc { get; set; }
		/// <summary>
		/// 硬盘检测提示大小
		/// </summary>
		public virtual string DiskChkRemindSize { get; set; }
		/// <summary>
		/// Remark
		/// </summary>
		public virtual string Remark { get; set; }
	}
 }

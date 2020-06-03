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

	public class Call_PhoneAddressModel
	{
		public  Call_PhoneAddressModel()
		{
			    DateTime initDatetime = new DateTime(1900, 1, 1);
		}


		/// <summary>
		/// ID
		/// </summary>
		public virtual int ID { get; set; }
		/// <summary>
		/// 号码（手机号前7位）
		/// </summary>
		public virtual string PhoneNum { get; set; }
		/// <summary>
		/// 手机卡类型
		/// </summary>
		public virtual string CardType { get; set; }
		/// <summary>
		/// 区号
		/// </summary>
		public virtual string CityCode { get; set; }
		/// <summary>
		/// 城市名称
		/// </summary>
		public virtual string CityName { get; set; }
		/// <summary>
		/// 邮编
		/// </summary>
		public virtual string ZipCode { get; set; }
		/// <summary>
		/// Remark
		/// </summary>
		public virtual string Remark { get; set; }
	}
 }

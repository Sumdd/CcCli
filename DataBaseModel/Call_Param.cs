///////////////////////////////////////////////////////////////////////////////////////
// 文件名   : C:\Users\zhongguan\Desktop\11111\NhibernateBag\Models\Call_Param.cs
// 类名     : Call_Param
// 中文名   : 
// 创建描述 : 
// 创建人   : 
// 创建时间 : 2015-11-10 16:54:04
// 版权信息 : 青岛天路信息技术有限责任公司  www.topdigi.com.cn
///////////////////////////////////////////////////////////////////////////////////////

using System;
namespace DataBaseModel
{
	/// <summary>
	/// 
	/// </summary>

	public class Call_ParamModel
	{
		public  Call_ParamModel()
		{
			    DateTime initDatetime = new DateTime(1900, 1, 1);
			    CreateTime = initDatetime ;
			    LoseTime = initDatetime ;
		}


		/// <summary>
		/// ID
		/// </summary>
		public virtual int ID { get; set; }
		/// <summary>
		/// 参数名称
		/// </summary>
		public virtual string P_Name { get; set; }
		/// <summary>
		/// 参数值
		/// </summary>
		public virtual string P_Value { get; set; }
		/// <summary>
		/// 参数描述
		/// </summary>
		public virtual string P_Description { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public virtual DateTime CreateTime { get; set; }
		/// <summary>
		/// 失效时间
		/// </summary>
		public virtual DateTime LoseTime { get; set; }
		/// <summary>
		/// 所属组
		/// </summary>
		public virtual string P_Group { get; set; }
		/// <summary>
		/// 标识
		/// </summary>
		public virtual string Remark { get; set; }
	}
 }

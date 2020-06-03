///////////////////////////////////////////////////////////////////////////////////////
// 文件名   : C:\Users\zhongguan\Desktop\11111\NhibernateBag\Models\Call_SocketCommand.cs
// 类名     : Call_SocketCommand
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

	public class Call_SocketCommandModel
	{
		public  Call_SocketCommandModel()
		{
			    DateTime initDatetime = new DateTime(1900, 1, 1);
		}


		/// <summary>
		/// ID
		/// </summary>
		public virtual int ID { get; set; }
		/// <summary>
		/// S_NO
		/// </summary>
		public virtual string S_NO { get; set; }
		/// <summary>
		/// S_Name
		/// </summary>
		public virtual string S_Name { get; set; }
		/// <summary>
		/// S_Value
		/// </summary>
		public virtual string S_Value { get; set; }
		/// <summary>
		/// S_Description
		/// </summary>
		public virtual string S_Description { get; set; }
		/// <summary>
		/// S_Type
		/// </summary>
		public virtual string S_Type { get; set; }
		/// <summary>
		/// S_StartChar
		/// </summary>
		public virtual string S_StartChar { get; set; }
		/// <summary>
		/// S_EndChar
		/// </summary>
		public virtual string S_EndChar { get; set; }
		/// <summary>
		/// Rep_OldChar
		/// </summary>
		public virtual string Rep_OldChar { get; set; }
		/// <summary>
		/// Rep_NewChar
		/// </summary>
		public virtual string Rep_NewChar { get; set; }
		/// <summary>
		/// S_ParentID
		/// </summary>
		public virtual int S_ParentID { get; set; }
		/// <summary>
		/// S_Order
		/// </summary>
		public virtual int S_Order { get; set; }
		/// <summary>
		/// Remark
		/// </summary>
		public virtual string Remark { get; set; }
	}
 }

///////////////////////////////////////////////////////////////////////////////////////
// 文件名   : C:\Users\zhongguan\Desktop\11111\NhibernateBag\Models\Call_Channel.cs
// 类名     : Call_Channel
// 中文名   : 
// 创建描述 : 
// 创建人   : 
// 创建时间 : 2015-11-10 16:43:38
// 版权信息 : 青岛天路信息技术有限责任公司  www.topdigi.com.cn
///////////////////////////////////////////////////////////////////////////////////////

using System;
namespace DataBaseModel
{
	/// <summary>
	/// 
	/// </summary>

	public class Call_ChannelModel
	{
		public Call_ChannelModel()
		{
			DateTime initDatetime = new DateTime(1900, 1, 1);
		}


		/// <summary>
		/// ID
		/// </summary>
		public virtual int ID { get; set; }
		/// <summary>
		/// 通道号
		/// </summary>
		public virtual int ChNo { get; set; }
		/// <summary>
		/// 通道类型
		/// </summary>
		public virtual int ChType { get; set; }
		/// <summary>
		/// 通道号码
		/// </summary>
		public virtual string ChNum { get; set; }
		/// <summary>
		/// 通道号码密码
		/// </summary>
		public virtual string ChPassword { get; set; }
		/// <summary>
		/// 通道显示号码
		/// </summary>
		public virtual string ShowName { get; set; }
		/// <summary>
		/// 通道SIP服务器
		/// </summary>
		public virtual string SipServerIp { get; set; }
		/// <summary>
		/// 通道SIP域名
		/// </summary>
		public virtual string DomainName { get; set; }
		/// <summary>
		/// 通道SIP端口
		/// </summary>
		public virtual int SipPort { get; set; }
		/// <summary>
		/// 通道SIP注册间隔
		/// </summary>
		public virtual int RegTime { get; set; }
		/// <summary>
		/// 通道名称
		/// </summary>
		public virtual string ChName { get; set; }
		/// <summary>
		/// 通道电压值
		/// </summary>
		public virtual int ChVad { get; set; }
		/// <summary>
		/// 板卡名称
		/// </summary>
		public virtual string BoardName { get; set; }
		/// <summary>
		/// 板卡序列号
		/// </summary>
		public virtual int BoardNo { get; set; }
		/// <summary>
		/// 所属组
		/// </summary>
		public virtual int GroupID { get; set; }
		/// <summary>
		/// 组优先级（数值小优先级大）
		/// </summary>
		public virtual int GroupLevel { get; set; }
		/// <summary>
		/// 通道呼叫类型（0呼入，1呼出）
		/// </summary>
		public virtual int CallType { get; set; }
		/// <summary>
		/// 拨号原则
		/// </summary>
		public virtual string CallRole { get; set; }
		/// <summary>
		/// 是否锁定
		/// </summary>
		public virtual int IsLock { get; set; }
		/// <summary>
		/// 是否可用
		/// </summary>
		public virtual int Usable { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public virtual string Remark { get; set; }
	}
}

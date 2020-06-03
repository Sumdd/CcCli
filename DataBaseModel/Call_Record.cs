///////////////////////////////////////////////////////////////////////////////////////
// 文件名   : C:\Users\sunhome\Desktop\1\\NhibernateBag\Models\Call_Record.cs
// 类名     : Call_Record
// 中文名   : 
// 创建描述 : 
// 创建人   : 
// 创建时间 : 2015/12/30 16:45:55
// 版权信息 : 青岛天路信息技术有限责任公司  www.topdigi.com.cn
///////////////////////////////////////////////////////////////////////////////////////

using System;
namespace DataBaseModel
{
	/// <summary>
	/// 
	/// </summary>

	public class Call_RecordModel
	{
		public  Call_RecordModel()
		{
			    DateTime initDatetime = new DateTime(1900, 1, 1);
		}


		/// <summary>
		/// ID
		/// </summary>
		public virtual int ID { get; set; }
		/// <summary>
		/// CallType
		/// </summary>
		public virtual string CallType { get; set; }
		/// <summary>
		/// ChannelID
		/// </summary>
		public virtual int ChannelID { get; set; }
		/// <summary>
		/// LinkChannelID
		/// </summary>
		public virtual int LinkChannelID { get; set; }
		/// <summary>
		/// LocalNum
		/// </summary>
		public virtual string LocalNum { get; set; }
		/// <summary>
		/// T_PhoneNum
		/// </summary>
		public virtual string T_PhoneNum { get; set; }
		/// <summary>
		/// C_PhoneNum
		/// </summary>
		public virtual string C_PhoneNum { get; set; }
		/// <summary>
		/// PhoneAddress
		/// </summary>
		public virtual string PhoneAddress { get; set; }
		/// <summary>
		/// DtmfNum
		/// </summary>
		public virtual string DtmfNum { get; set; }
		/// <summary>
		/// PhoneTypeID
		/// </summary>
		public virtual int PhoneTypeID { get; set; }
		/// <summary>
		/// PhoneListID
		/// </summary>
		public virtual int PhoneListID { get; set; }
		/// <summary>
		/// PriceTypeID
		/// </summary>
		public virtual int PriceTypeID { get; set; }
		/// <summary>
		/// CallPrice
		/// </summary>
		public virtual double CallPrice { get; set; }
		/// <summary>
		/// AgnetID
		/// </summary>
		public virtual int AgnetID { get; set; }
		/// <summary>
		/// CusID
		/// </summary>
		public virtual int CusID { get; set; }
		/// <summary>
		/// ContactID
		/// </summary>
		public virtual int ContactID { get; set; }
		/// <summary>
		/// RecordFile
		/// </summary>
		public virtual string RecordFile { get; set; }
		/// <summary>
		/// C_Date
		/// </summary>
		public virtual string C_Date { get; set; }
		/// <summary>
		/// C_StartTime
		/// </summary>
		public virtual string C_StartTime { get; set; }
		/// <summary>
		/// C_RingTime
		/// </summary>
		public virtual string C_RingTime { get; set; }
		/// <summary>
		/// C_AnswerTime
		/// </summary>
		public virtual string C_AnswerTime { get; set; }
		/// <summary>
		/// C_EndTime
		/// </summary>
		public virtual string C_EndTime { get; set; }
		/// <summary>
		/// C_WaitTime
		/// </summary>
		public virtual int C_WaitTime { get; set; }
		/// <summary>
		/// C_SpeakTime
		/// </summary>
		public virtual int C_SpeakTime { get; set; }
		/// <summary>
		/// CallResultID
		/// </summary>
		public virtual int CallResultID { get; set; }
		/// <summary>
		/// CallForwordFlag
		/// </summary>
		public virtual int CallForwordFlag { get; set; }
		/// <summary>
		/// CallForwordChannelID
		/// </summary>
		public virtual string CallForwordChannelID { get; set; }
		/// <summary>
		/// SerOp_ID
		/// </summary>
		public virtual int SerOp_ID { get; set; }
		/// <summary>
		/// SerOp_DTMF
		/// </summary>
		public virtual string SerOp_DTMF { get; set; }
		/// <summary>
		/// SerOp_LeaveRec
		/// </summary>
		public virtual string SerOp_LeaveRec { get; set; }
		/// <summary>
		/// Detail
		/// </summary>
		public virtual string Detail { get; set; }
        /// <summary>
        /// 已处理
        /// </summary>
        public virtual int Uhandler { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 录音主键
        /// </summary>
        public virtual string recordName { get; set; }
    }
 }

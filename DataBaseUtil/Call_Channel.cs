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
using DataBaseModel;
using System.Data;
using Common;
using System.Collections.Generic;
using Core_v1;

namespace DataBaseUtil
{
	/// <summary>
	/// 
	/// </summary>
	public class Call_ChannelUtil
	{
		public Call_ChannelUtil()
		{
			DateTime initDatetime = new DateTime(1900, 1, 1);
		}

		public static string GetChTypeByCh(string nCh)
		{
			string SqlStr = "select ChType from Channel wehre ChNo=" + nCh;
			DataTable dt = MySQL_Method.BindTable(SqlStr);
			if (dt.Rows.Count > 0)
			{
				return dt.Rows[0]["ChType"].ToString();
			}
			return "-1";
		}

		public static int GetIDByCh(string nCh)
		{
			string SqlStr = "select ID from Channel wehre ChNo=" + nCh;
			object ChID = MySQL_Method.ExecuteScalar(SqlStr);
			return CommonClassLib.StringIsNumber(ChID.ToString()) ? Convert.ToInt32(ChID.ToString()) : -1;
		}

		public static Call_ChannelModel GetChannelSipInfo(int? ChannelID)
		{
			Call_ChannelModel _Call_ChannelModel = new Call_ChannelModel();

			if (!ChannelID.HasValue)
				return _Call_ChannelModel;
			try
			{
				string SqlStr = string.Format("SELECT Cal_Cnl.ChNum,Cal_Cnl.ChPassword,Cal_Cnl.ShowName,Cal_Cnl.SipServerIp,Cal_Cnl.DomainName,Cal_Cnl.SipPort,Cal_Cnl.RegTime from `call_channel` Cal_Cnl where Cal_Cnl.ID={0}", ChannelID.Value);

				DataTable dt = MySQL_Method.BindTable(SqlStr);
				if (dt.Rows.Count > 0)
				{
					_Call_ChannelModel.ChNum = dt.Rows[0]["ChNum"].ToString();
					_Call_ChannelModel.ChPassword = dt.Rows[0]["ChPassword"].ToString();
					_Call_ChannelModel.ShowName = dt.Rows[0]["ShowName"].ToString();
					_Call_ChannelModel.SipServerIp = dt.Rows[0]["SipServerIp"].ToString();
					_Call_ChannelModel.DomainName = dt.Rows[0]["DomainName"].ToString();
					_Call_ChannelModel.SipPort = CommonClassLib.StringIsNumber(dt.Rows[0]["SipPort"].ToString()) ? int.Parse(dt.Rows[0]["SipPort"].ToString()) : 5060;
					_Call_ChannelModel.RegTime = CommonClassLib.StringIsNumber(dt.Rows[0]["RegTime"].ToString()) ? int.Parse(dt.Rows[0]["RegTime"].ToString()) : 3600;
				}
			}
			catch (Exception ex)
			{
				LogFile.Write(typeof(MySQL_Method), LOGLEVEL.ERROR, "get a error when query sip account information", ex);
			}
			return _Call_ChannelModel;
		}

        public static int m_fSetChannelType(List<string> m_lStrings,int m_uChannelType)
        {
            try
            {
                string m_sInsertSQLStr = $@"
update call_channel
set chtype = {m_uChannelType}
where 1=1
and id in ({string.Join(",", m_lStrings.ToArray())})
";
                return MySQL_Method.ExecuteNonQuery(m_sInsertSQLStr);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[DataBaseUtil][Call_ChannelUtil][m_fSetChannelType][{ex.Message}]");
                return 0;
            }
        }

        public static int m_fGetIsRegister(int? m_uInt)
        {
            try
            {
                string m_sAsSQL = $@"
SELECT
	`call_channel`.`IsRegister` 
FROM
	`call_channel`
	LEFT JOIN `call_agent` ON `call_channel`.`ID` = `call_agent`.`ID` 
WHERE
	`call_agent`.`ID` = {m_uInt}
";
                return Convert.ToInt32(MySQL_Method.ExecuteScalar(m_sAsSQL));
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[DataBaseUtil][Call_ChannelUtil][m_fGetIsRegister][{ex.Message}]");
            }
            return -1;
        }

        public static int m_fSetRegister(List<string> m_lString, int m_uIsRegister)
        {
            try
            {
                string m_sAsSQL = $@"
UPDATE `call_channel` 
SET `call_channel`.`IsRegister` = {m_uIsRegister} 
WHERE
	`call_channel`.`ID` IN ( {string.Join(",", m_lString.ToArray())} )
";
                return MySQL_Method.ExecuteNonQuery(m_sAsSQL);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[DataBaseUtil][Call_ChannelUtil][m_fSetNotRegister][{ex.Message}]");
                return 0;
            }
        }
	}
}

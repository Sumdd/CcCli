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
using System.Collections.Generic;
using System.Data;
using Common;
using MySql.Data.MySqlClient;
using Model_v1;
using Cmn_v1;
using Core_v1;

namespace DataBaseUtil {
    /// <summary>
    /// 
    /// </summary>
    public class Call_Record {
        public Call_Record() {
            DateTime initDatetime = new DateTime(1900, 1, 1);
        }
        public static List<Call_RecordModel> GetData1() {
            List<Call_RecordModel> Models = new List<Call_RecordModel>();
            string sqlstr = "select * from call_record";

            DataTable dt = MySQL_Method.BindTable(sqlstr);
            if(dt.Rows.Count <= 0)
                return Models;

            foreach(DataRow dr in dt.Rows) {
                Call_RecordModel mod = new Call_RecordModel();
                mod.AgnetID = CommonClassLib.StringIsNumber(dr["AgnetID"].ToString()) ? int.Parse(dr["AgnetID"].ToString()) : -1;
                mod.C_AnswerTime = dr["C_AnswerTime"].ToString();
                mod.C_Date = dr["C_Date"].ToString();
                mod.C_EndTime = dr["C_EndTime"].ToString();
                mod.C_PhoneNum = dr["C_PhoneNum"].ToString();
                mod.C_RingTime = dr["C_RingTime"].ToString();
                mod.C_SpeakTime = CommonClassLib.StringIsNumber(dr["C_SpeakTime"].ToString()) ? int.Parse(dr["C_SpeakTime"].ToString()) : -1;
                mod.C_StartTime = dr["C_StartTime"].ToString();
                mod.C_WaitTime = CommonClassLib.StringIsNumber(dr["C_WaitTime"].ToString()) ? int.Parse(dr["C_WaitTime"].ToString()) : -1;
                mod.CallForwordChannelID = dr["CallForwordChannelID"].ToString();
                mod.CallForwordFlag = CommonClassLib.StringIsNumber(dr["CallForwordFlag"].ToString()) ? int.Parse(dr["CallForwordFlag"].ToString()) : -1;
                mod.CallPrice = CommonClassLib.StringIsNumber(dr["CallPrice"].ToString()) ? int.Parse(dr["CallPrice"].ToString()) : -1;
                mod.CallResultID = CommonClassLib.StringIsNumber(dr["CallResultID"].ToString()) ? int.Parse(dr["CallResultID"].ToString()) : -1;
                mod.CallType = dr["CallType"].ToString();
                mod.ChannelID = CommonClassLib.StringIsNumber(dr["ChannelID"].ToString()) ? int.Parse(dr["ChannelID"].ToString()) : -1;
                mod.ContactID = CommonClassLib.StringIsNumber(dr["ContactID"].ToString()) ? int.Parse(dr["ContactID"].ToString()) : -1;
                mod.CusID = CommonClassLib.StringIsNumber(dr["CusID"].ToString()) ? int.Parse(dr["CusID"].ToString()) : -1;
                mod.Detail = dr["Detail"].ToString();
                mod.DtmfNum = dr["DtmfNum"].ToString();
                mod.ID = CommonClassLib.StringIsNumber(dr["ID"].ToString()) ? int.Parse(dr["ID"].ToString()) : -1;
                mod.LinkChannelID = CommonClassLib.StringIsNumber(dr["LinkChannelID"].ToString()) ? int.Parse(dr["LinkChannelID"].ToString()) : -1;
                mod.LocalNum = dr["LocalNum"].ToString();
                mod.PhoneAddress = dr["PhoneAddress"].ToString();
                mod.PhoneListID = CommonClassLib.StringIsNumber(dr["PhoneListID"].ToString()) ? int.Parse(dr["PhoneListID"].ToString()) : -1;
                mod.PhoneTypeID = CommonClassLib.StringIsNumber(dr["PhoneTypeID"].ToString()) ? int.Parse(dr["PhoneTypeID"].ToString()) : -1;
                mod.PriceTypeID = CommonClassLib.StringIsNumber(dr["PriceTypeID"].ToString()) ? int.Parse(dr["PriceTypeID"].ToString()) : -1;
                mod.RecordFile = dr["RecordFile"].ToString();
                mod.Remark = dr["Remark"].ToString();
                mod.SerOp_DTMF = dr["SerOp_DTMF"].ToString();
                mod.SerOp_ID = CommonClassLib.StringIsNumber(dr["SerOp_ID"].ToString()) ? int.Parse(dr["SerOp_ID"].ToString()) : -1;
                mod.SerOp_LeaveRec = dr["SerOp_LeaveRec"].ToString();
                mod.T_PhoneNum = dr["T_PhoneNum"].ToString();
                mod.recordName = dr["recordName"].ToString();
                mod.Uhandler = CommonClassLib.StringIsNumber(dr["Uhandler"].ToString()) ? int.Parse(dr["Uhandler"].ToString()) : -1;

                Models.Add(mod);
            }

            return Models;
        }

        public static DataSet GetData(Dictionary<string, object> _QueryParam) {
            MySqlParameter[] _Param = new MySqlParameter[13]{
                        new MySqlParameter("?T_PhoneNum",MySqlDbType.VarChar,200),
                        new MySqlParameter("?T_PhoneNum_Like",MySqlDbType.Int32),
                        new MySqlParameter("?LimitParam",MySqlDbType.VarChar,20),
                        new MySqlParameter("?MaxDate",MySqlDbType.VarChar,20),
                        new MySqlParameter("?MinDate",MySqlDbType.VarChar,20),
                        new MySqlParameter("?MaxTime",MySqlDbType.VarChar,20),
                        new MySqlParameter("?MinTime",MySqlDbType.VarChar,20),
                        new MySqlParameter("?Speaks",MySqlDbType.Int32),
                        new MySqlParameter("?Speake",MySqlDbType.Int32),
                        new MySqlParameter("?Waits",MySqlDbType.Int32),
                        new MySqlParameter("?Waite",MySqlDbType.Int32),
                        new MySqlParameter("?AgentID",MySqlDbType.Int32),
                        new MySqlParameter("?OrderBy",MySqlDbType.VarChar,50)
                        };
            foreach(KeyValuePair<string, object> kv in _QueryParam) {
                if(kv.Key == "T_PhoneNum")
                    _Param[0].Value = kv.Value.ToString();
                if(kv.Key == "T_PhoneNum_Like")
                    _Param[1].Value = kv.Value.ToString();
                if(kv.Key == "LimitParam")
                    _Param[2].Value = kv.Value.ToString();
                if(kv.Key == "MaxDate")
                    _Param[3].Value = kv.Value.ToString();
                if(kv.Key == "MinDate")
                    _Param[4].Value = kv.Value.ToString();
                if(kv.Key == "MaxTime")
                    _Param[5].Value = kv.Value.ToString();
                if(kv.Key == "MinTime")
                    _Param[6].Value = kv.Value.ToString();
                if(kv.Key == "Speaks")
                    _Param[7].Value = kv.Value.ToString();
                if(kv.Key == "Speake")
                    _Param[8].Value = kv.Value.ToString();
                if(kv.Key == "Waits")
                    _Param[9].Value = kv.Value.ToString();
                if(kv.Key == "Waite")
                    _Param[10].Value = kv.Value.ToString();
                if(kv.Key == "AgentID")
                    _Param[11].Value = kv.Value.ToString();
                if(kv.Key == "OrderBy")
                    _Param[12].Value = kv.Value.ToString();
            }
            return MySQL_Method.ExecuteDataSetByProcedure("proc_get_call_record", _Param);
        }

        public static List<M_kv> GetRecentNoanswerData(int AgentID, bool m_bShow = false) {
            List<M_kv> list = new List<M_kv>();
            string asMySQL = $@"
SELECT * FROM(
SELECT 
    T_PhoneNum as phone,
    NULLIF(PhoneAddress,'未知') as address,
    COUNT(1) as total,
	MAX(C_StartTime) as time
FROM call_record 
WHERE (CallType = 4 OR CallType = 8)
AND AgentID = ?agentid { (Call_ClientParamUtil.noAnswerUse ? " AND C_StartTime >= ?stime " : "")}
AND C_StartTime <= ?etime
AND IFNULL(Uhandler,0)=0
GROUP BY AgentID,T_PhoneNum
ORDER BY C_StartTime DESC) AS B
ORDER BY b.time desc
";
            MySqlParameter[] _Param = new MySqlParameter[3]{
                        new MySqlParameter("?agentid",MySqlDbType.Int32),
                        new MySqlParameter("?stime",MySqlDbType.DateTime),
                        new MySqlParameter("?etime",MySqlDbType.DateTime)
                        };
            _Param[0].Value = AgentID;
            _Param[1].Value = DateTime.Now.AddDays(-Call_ClientParamUtil.noAnswerDay).ToString("yyyy-MM-dd 00:00:00");
            _Param[2].Value = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
            DataTable result = MySQL_Method.BindTable(asMySQL, _Param);
            if(result != null && result.Rows.Count > 0) {
                foreach(DataRow item in result.Rows) {
                    M_kv _ = new M_kv();
                    if (m_bShow)
                    {
                        _.key = string.Empty;
                        _.value = $"【时间】 {item[3]}\r\n【次数】 {item[2]}次\r\n【电话】 {item[0]}({item[1]})";
                    }
                    else
                    {
                        string m_sNumber = item[0]?.ToString();
                        string m_sSecretNumber = m_sNumber;
                        if (!string.IsNullOrWhiteSpace(m_sNumber))
                        {
                            int m_uInt = m_sNumber.Length;
                            if (m_uInt > 7)
                            {
                                m_sSecretNumber = m_sSecretNumber.Substring(0, 3) + "****" + m_sSecretNumber.Substring(m_uInt - 4);
                            }
                            else
                            {
                                m_sSecretNumber = "*******";
                            }
                        }
                        else
                        {
                            m_sSecretNumber = "*******";
                        }
                        _.key = m_sSecretNumber;
                        _.value = $"【时间】 {item[3]}\r\n【次数】 {item[2]}次\r\n【电话】 {m_sSecretNumber}({item[1]})";
                    }
                    _.tag = item[0].ToString();
                    list.Add(_);
                }
            }
            return list;
        }

        public static int UpdateHandler(string _phoneNumber) {
            MySqlParameter[] _Param = new MySqlParameter[2]{
                new MySqlParameter("?agentid",MySqlDbType.Int32),
                new MySqlParameter("?phonenumber",MySqlDbType.VarChar)
            };
            _Param[0].Value = AgentInfo.AgentID;
            _Param[1].Value = _phoneNumber;
            return MySQL_Method.ExecuteNonQuery("update call_record set Uhandler = 1 where IFNULL(Uhandler,0)=0 and AgentID = ?agentid and T_PhoneNum = ?phonenumber; ", _Param);
        }

        public static DataSet GetDownLoadRecord(List<string> list) {
            try {
                var as_sql = $@"select ID,RecordFile from call_record where recordname in ('{string.Join("','", list.ToArray())}')";
                var as_ds = MySQL_Method.ExecuteDataSet(as_sql);
                return as_ds;
            } catch(Exception ex) {
                Log.Instance.Error($"[DataBaseUtil][Call_Record][GetDownLoadRecord][{ex.Message}]");
            }
            return null;
        }
    }
}

using Core_v1;
using DataBaseUtil;
using Model_v1;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using WebSocket_v1;

namespace CenoCC
{
    public class h_xml
    {
        public static string read(string path)
        {
            var content = string.Empty;
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                content = sr.ReadToEnd();
            }
            return content;
        }

        public static void write(DataTable m_pDataTable, string content, out string m_sErrMsg)
        {
            m_sErrMsg = string.Empty;
            int m_uSuccess = 0;
            List<string> m_lErr = new List<string>();
            foreach (DataRow m_pDataRow in m_pDataTable.Rows)
            {
                string m_sNumber = m_pDataRow["$number$"].ToString();
                string m_sPassword = m_pDataRow["$password$"].ToString();

                ///IP可空
                string m_sIP = string.Empty;
                if (m_pDataTable.Columns.Contains("$ip$")) m_sIP = m_pDataRow["$ip$"].ToString();
                ///端口可空
                string m_sPort = string.Empty;
                if (m_pDataTable.Columns.Contains("$port$")) m_sPort = m_pDataRow["$port$"].ToString();

                string m_sSeconds = "75";
                if (m_pDataTable.Columns.Contains("$seconds$")) m_sSeconds = m_pDataRow["$seconds$"].ToString();
                string m_sAccount = string.Empty;
                string m_stNumber = string.Empty;

                if (!string.IsNullOrWhiteSpace(m_sNumber) && !string.IsNullOrWhiteSpace(m_sPassword))
                {
                    string m_sContent = content
                        .Replace("$number$", m_sNumber)
                        .Replace("$password$", m_sPassword)
                        .Replace("$seconds$", m_sSeconds);

                    ///替换IP
                    if (!string.IsNullOrWhiteSpace(m_sIP)) m_sContent = m_sContent.Replace("$ip$", m_sIP);
                    ///替换端口
                    if (!string.IsNullOrWhiteSpace(m_sPort)) m_sContent = m_sContent.Replace("$port$", m_sPort);

                    ///发送命令至服务器让服务器直接添加网关XML文件即可
                    object m_oObject = new
                    {
                        m_sName = m_sNumber,
                        m_sXML = m_sContent
                    };
                    m_mResponseJSON _m_mResponseJSON = InWebSocketMain.SendAsyncObject(H_Json.ToJson(m_oObject), m_cFSCmdType._m_sCreateGateway);
                    if (_m_mResponseJSON.result != null)
                    {
                        string m_sResult = _m_mResponseJSON.result.ToString();
                        if (m_sResult.StartsWith("+OK"))
                        {
                            m_uSuccess++;
                        }
                        else
                        {
                            m_lErr.Add($"添加网关[{m_sNumber}]XML文件失败:{m_sResult}");
                        }
                    }
                    else
                    {
                        m_lErr.Add($"添加网关[{m_sNumber}]XML文件结果未知:{_m_mResponseJSON?.msg},请核实");
                    }
                }

                //账号绑定导入
                if (m_pDataTable.Columns.Contains("$account$"))
                {
                    m_sAccount = m_pDataRow["$account$"].ToString();
                }

                //真实号码导入
                if (m_pDataTable.Columns.Contains("$tnumber$"))
                {
                    m_stNumber = m_pDataRow["$tnumber$"].ToString();
                }

                //写循环写入网关数据
                string m_sUUID = Guid.NewGuid().ToString();
                string m_dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (gatewayadd(m_sNumber, m_sUUID, m_dtNow) > 0)
                {
                    Log.Instance.Success($"{m_sNumber},gateway,add,success,uuid='{m_sUUID}'");
                }
                else
                {
                    m_sUUID = gatewayuuid(m_sNumber);
                    Log.Instance.Fail($"{m_sNumber},gateway,add,fail,uuid='{m_sUUID}'");
                }
                //插入拨号限制
                if (limitadd(m_sNumber, m_sAccount, m_sUUID, m_dtNow, m_stNumber) > 0)
                {
                    Log.Instance.Success($"{m_sNumber},dial limit,add,success");
                }
                else
                {
                    Log.Instance.Fail($"{m_sNumber},dial limit,add,fail");
                }
                ///处理结果返回
                m_sErrMsg = $"XML成功数量:{m_uSuccess}条;失败情况:{(m_lErr.Count > 0 ? string.Join(";", m_lErr) : "0条")}";
            }
        }

        public static int gatewayadd(string gwName, string uuid, string m_dtNow)
        {
            int i = 0;
            try
            {
                var as_count_sql = $"SELECT count( 1 ) as count FROM call_gateway WHERE gw_name = '{gwName}'";
                int count = Convert.ToInt32(MySQL_Method.ExecuteScalar(as_count_sql));
                if (count <= 0)
                {
                    var as_sql = $"INSERT INTO `call_gateway` VALUES (DEFAULT(id), '{uuid}', '{gwName}', '', '', '', null, null, null, null, null, '3600', '1', 'udp', '30', '0', 'tport=tcp', '25', '1', '1', 'IMS', 'gateway', '{Common.AgentInfo.AgentID}', '{m_dtNow}');";
                    i = MySQL_Method.ExecuteNonQuery(as_sql);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"d_multi gatewayadd:{ex.Message}");
            }
            return i;
        }

        public static string gatewayuuid(string gwName)
        {
            try
            {
                var as_select_sql = $"SELECT UniqueID as m_sUUID FROM call_gateway WHERE gw_name = '{gwName}'";
                return MySQL_Method.ExecuteScalar(as_select_sql).ToString();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"d_multi gatewayadd:{ex.Message}");
            }
            return string.Empty;
        }

        public static int limitadd(string number, string account, string uuid, string m_dtNow, string tnumber)
        {
            var as_parameter_sql = $@"  select ifnull((select v from dial_parameter where k='limitcount' limit 1),0) as limitcount,
	                                           ifnull((select v from dial_parameter where k='limitduration' limit 1),0) as limitduration,
	                                           ifnull((select v from dial_parameter where k='limitthecount' limit 1),0) as limitthecount,
	                                           ifnull((select v from dial_parameter where k='limittheduration' limit 1),0) as limittheduration,
	                                           ifnull((select v from dial_parameter where k='limitthedial' limit 1),0) as limitthedial,
	                                           ifnull((select v from dial_parameter where k='areacode' limit 1),'') as areacode,
	                                           ifnull((select v from dial_parameter where k='areaname' limit 1),'') as areaname,
	                                           ifnull((select v from dial_parameter where k='dialprefix' limit 1),'') as dialprefix;";
            DataTable as_parameter_dt = MySQL_Method.BindTable(as_parameter_sql);
            if (as_parameter_dt != null && as_parameter_dt.Rows.Count > 0)
            {
                DataRow dr = as_parameter_dt.Rows[0];
                var as_list = new List<string>();
                as_list.Add($@"select '{number}' as number,ifnull((select id from call_agent where loginname='{account}' limit 1),-1) as useuser,
                                      '{Common.AgentInfo.AgentID}',
                                      '{m_dtNow}',
                                       {(string.IsNullOrWhiteSpace(tnumber) ? "NULL" : $"'{tnumber}'")} as tnumber,  
                                       {dr["limitcount"]} as limitcount,
                                       {dr["limitduration"]} as limitduration,
                                       {dr["limitthecount"]} as limitthecount,
                                       {dr["limittheduration"]} as limittheduration,
                                       {dr["limitthedial"]} as limitthedial,
                                      '{dr["areacode"]}' as areacode,
                                      '{dr["areaname"]}' as areaname,
                                      '{dr["dialprefix"]}' as dialprefix,
                                      '{uuid}' as gwuid");
                var as_insert_sql = $@"insert into dial_limit (number,useuser,adduser,addtime,tnumber,limitcount,limitduration,limitthecount,limittheduration,limitthedial,areacode,areaname,dialprefix,gwuid)
                                           select * from ({string.Join("\r\nunion\r\n", as_list.ToArray())}) as a 
                                           where cast(a.number as char(20)) not in (select number from dial_limit as b where b.isdel = 0);";
                return MySQL_Method.ExecuteNonQuery(as_insert_sql);
            }
            return 0;
        }

        public static DataTable m_fGatewayUsing(List<string> m_lID, out bool m_bUsing)
        {
            m_bUsing = false;
            string m_sID = string.Join(",", m_lID);
            string m_sSQL = $@"
SELECT
	`call_gateway`.`ID`,
	`call_gateway`.`UniqueID`,
	`call_gateway`.`gw_name`,
	`call_gateway`.`gwtype` 
FROM
	`call_gateway` 
WHERE
	`call_gateway`.`ID` IN ( {m_sID} );
SELECT
	COUNT( 1 ) AS usetotal 
FROM
	`dial_limit` 
WHERE
	`dial_limit`.`gwuid` IN ( SELECT `call_gateway`.`UniqueID` FROM `call_gateway` WHERE `call_gateway`.`ID` IN ( {m_sID} ) ) 
	AND IFNULL( `dial_limit`.`isdel`, 0 ) = 0;
";
            DataSet m_pDataSet = MySQL_Method.ExecuteDataSet(m_sSQL);
            if (m_pDataSet != null && m_pDataSet.Tables.Count == 2)
            {
                m_bUsing = Convert.ToInt32(m_pDataSet.Tables[1].Rows[0]["usetotal"]) > 0;
                return m_pDataSet.Tables[0];
            }
            m_bUsing = true;
            return null;
        }

        public static string m_fDeleteGateway(List<string> m_lID)
        {
            string m_sErrMsg = string.Empty;
            try
            {
                string m_sID = string.Join(",", m_lID);
                string m_sSQL = $@"
DELETE 
FROM
	`call_gateway` 
WHERE
	`call_gateway`.`ID` IN ( {m_sID} );
";
                int m_uCount = MySQL_Method.ExecuteNonQuery(m_sSQL);
                if (m_uCount > 0)
                    m_sErrMsg = "网关数据删除成功";
                else
                    m_sErrMsg = "网关数据删除完成";
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][h_xml][m_fDeleteGateway][Exception][{ex.Message}]");
                m_sErrMsg = $"网关数据删除错误:{ex.Message}";
            }
            return m_sErrMsg;
        }
    }
}

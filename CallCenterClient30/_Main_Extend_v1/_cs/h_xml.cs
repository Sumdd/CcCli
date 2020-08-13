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

            ///<![CDATA[
            /// 延申导入含义,可以导入重复
            /// 1.鉴权网关
            /// 2.非鉴权网关
            /// 4.号码可重复
            /// 8.根据号码列替换其它信息
            /// ]]>

            m_sErrMsg = string.Empty;
            int m_uSuccess = 0;
            List<string> m_lErr = new List<string>();
            foreach (DataRow m_pDataRow in m_pDataTable.Rows)
            {
                string m_sErr = string.Empty;
                string _m_sErr = string.Empty;

                string m_sNumber = m_pDataRow["$number$"].ToString();
                string m_sPassword = m_pDataRow["$password$"].ToString();

                ///类型拓展
                int m_uType = 1;
                if (m_pDataTable.Columns.Contains("$type$"))
                {
                    string m_sType = m_pDataRow["$type$"].ToString();
                    if (!string.IsNullOrWhiteSpace(m_sType)) int.TryParse(m_sType, out m_uType);
                }
                ///IP可空
                string m_sIP = string.Empty;
                if (m_pDataTable.Columns.Contains("$ip$")) m_sIP = m_pDataRow["$ip$"].ToString();
                ///端口可空
                string m_sPort = string.Empty;
                if (m_pDataTable.Columns.Contains("$port$")) m_sPort = m_pDataRow["$port$"].ToString();
                ///注册间隔可空,最小60秒
                string m_sSeconds = string.Empty;
                if (m_pDataTable.Columns.Contains("$seconds$")) m_sSeconds = m_pDataRow["$seconds$"].ToString();
                int m_uSeconds = 75;
                int.TryParse(m_sSeconds, out m_uSeconds);
                if (m_uSeconds <= 60) m_uSeconds = 60;
                m_sSeconds = m_uSeconds.ToString();

                string m_sAccount = string.Empty;
                string m_stNumber = string.Empty;

                ///网关名称可空
                string m_sName = string.Empty;
                if (m_pDataTable.Columns.Contains("$name$")) m_sName = m_pDataRow["$name$"].ToString();

                ///号码、密码、类型含1
                if (!string.IsNullOrWhiteSpace(m_sNumber) && !string.IsNullOrWhiteSpace(m_sPassword) && (m_uType & 1) > 0)
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
                        if (!m_sResult.StartsWith("+OK"))
                        {
                            m_sErr += $"添加网关[{m_sNumber}]XML文件失败:{m_sResult};";
                        }
                    }
                    else
                    {
                        m_sErr += $"添加网关[{m_sNumber}]XML文件结果未知:{_m_mResponseJSON?.msg},请核实;";
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
                string m_sUUID = string.Empty;
                string m_dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                ///类型含1或2
                if ((m_uType & 1) > 0 || (m_uType & 2) > 0 || (m_uType & 4) > 0)
                {
                    m_sUUID = Guid.NewGuid().ToString();
                    if (gatewayadd(m_sNumber, m_sUUID, m_dtNow, m_uType, m_sIP, m_sPort, m_sName, out _m_sErr) > 0)
                    {
                        Log.Instance.Success($"{m_sNumber},gateway,add,success,uuid='{m_sUUID}'");
                    }
                    else
                    {
                        m_sUUID = gatewayuuid(m_sNumber, m_uType, m_sIP, m_sPort);
                        if (!string.IsNullOrWhiteSpace(m_sUUID))
                        {
                            Log.Instance.Warn($"{m_sNumber},gateway,add,fail,already exits,uuid='{m_sUUID}'");
                        }
                        else
                        {
                            string _m_sLog = $@"{m_sNumber},网关处理错误;";
                            m_sErr += _m_sLog;
                            Log.Instance.Fail(_m_sLog);
                        }
                    }
                }

                //插入拨号限制
                if (limitadd(m_sNumber, m_sAccount, m_sUUID, m_dtNow, m_stNumber, m_uType, out _m_sErr) > 0)
                {
                    Log.Instance.Success($"{m_sNumber},dial limit,add,success");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(_m_sErr))
                    {
                        m_sErr += _m_sErr;
                        Log.Instance.Fail(_m_sErr);
                    }
                    else
                    {
                        string _m_sLog = $@"{m_sNumber},拨号限制处理错误;";
                        m_sErr += _m_sLog;
                        Log.Instance.Fail(_m_sLog);
                    }
                }

                if (!string.IsNullOrWhiteSpace(m_sErr))
                    m_lErr.Add(m_sErr);
                else
                    m_uSuccess++;
            }

            ///处理结果返回
            m_sErrMsg = $"成功:{m_uSuccess}条;失败情况:{(m_lErr.Count > 0 ? string.Join("", m_lErr) : "0条")}";
        }

        public static int gatewayadd(string gwName, string uuid, string m_dtNow, int m_uType, string m_sIP, string m_sPort, string m_sName, out string _m_sErr)
        {
            int i = 0;
            _m_sErr = string.Empty;
            try
            {
                string m_sSQL1 = string.Empty;
                ///非鉴权网关
                if ((m_uType & 2) > 0)
                {
                    ///IP非空
                    if (string.IsNullOrWhiteSpace(m_sIP))
                    {
                        _m_sErr = $"{gwName},非鉴权网关IP非空";
                        return 0;
                    }

                    m_sSQL1 = $@"
SELECT
	count( 1 ) AS count 
FROM
	`call_gateway` 
WHERE
	`call_gateway`.`gwtype` != 'gateway' 
	AND ( `call_gateway`.`gw_name` = '{m_sIP}' OR `call_gateway`.`gw_name` = '{m_sIP}:{m_sPort}' );
";
                    int m_uCount = Convert.ToInt32(MySQL_Method.ExecuteScalar(m_sSQL1));
                    if (m_uCount <= 0)
                    {
                        var as_sql = $"INSERT INTO `call_gateway` VALUES (DEFAULT(id), '{uuid}', '{m_sIP}:{m_sPort}', '', '', '', null, null, null, null, null, '3600', '1', 'udp', '30', '0', 'tport=tcp', '25', '1', '1', '{(string.IsNullOrWhiteSpace(m_sName) ? "非鉴权网关" : m_sName)}', 'external', '{Common.AgentInfo.AgentID}', '{m_dtNow}');";
                        i = MySQL_Method.ExecuteNonQuery(as_sql);
                    }
                }
                else
                {
                    m_sSQL1 = $@"
SELECT
	count( 1 ) AS count 
FROM
	`call_gateway` 
WHERE
	`call_gateway`.`gwtype` = 'gateway' 
	AND `call_gateway`.`gw_name` = '{gwName}' ;
";
                    int m_uCount = Convert.ToInt32(MySQL_Method.ExecuteScalar(m_sSQL1));
                    if (m_uCount <= 0)
                    {
                        var as_sql = $"INSERT INTO `call_gateway` VALUES (DEFAULT(id), '{uuid}', '{gwName}', '', '', '', null, null, null, null, null, '3600', '1', 'udp', '30', '0', 'tport=tcp', '25', '1', '1', '{(string.IsNullOrWhiteSpace(m_sName) ? "IMS" : m_sName)}', 'gateway', '{Common.AgentInfo.AgentID}', '{m_dtNow}');";
                        i = MySQL_Method.ExecuteNonQuery(as_sql);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Instance.Error($"d_multi gatewayadd:{ex.Message}");
            }
            return i;
        }

        public static string gatewayuuid(string gwName, int m_uType, string m_sIP, string m_sPort)
        {
            try
            {
                string m_sSQL = string.Empty;
                if ((m_uType & 2) > 0)
                {
                    m_sSQL = $@"
SELECT
	`call_gateway`.`UniqueID` AS `m_sUUID` 
FROM
	`call_gateway` 
WHERE
	`call_gateway`.`gwtype` != 'gateway' 
	AND ( `call_gateway`.`gw_name` = '{m_sIP}' OR `call_gateway`.`gw_name` = '{m_sIP}:{m_sPort}' );
";
                }
                else
                {
                    m_sSQL = $@"
SELECT
	`call_gateway`.`UniqueID` AS `m_sUUID` 
FROM
	`call_gateway` 
WHERE
	`call_gateway`.`gwtype` = 'gateway' 
	AND `call_gateway`.`gw_name` = '{gwName}' ;
";
                }
                return MySQL_Method.ExecuteScalar(m_sSQL)?.ToString();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"d_multi gatewayadd:{ex.Message}");
            }
            return string.Empty;
        }

        public static int limitadd(string number, string account, string uuid, string m_dtNow, string tnumber, int m_uType, out string _m_sErr)
        {
            _m_sErr = string.Empty;

            if ((m_uType & 8) > 0)
            {
                ///编辑
                string m_sSQL = $@"
UPDATE `dial_limit` 
SET `dial_limit`.`useuser` = IFNULL( ( SELECT `call_agent`.`ID` FROM `call_agent` WHERE `call_agent`.`LoginName` = '{account}' LIMIT 1 ), `dial_limit`.`useuser` ),
`dial_limit`.`tnumber` = IFNULL( {(string.IsNullOrWhiteSpace(tnumber) ? "NULL" : $"'{tnumber}'")}, `dial_limit`.`tnumber` ) 
WHERE
	`dial_limit`.`number` = '{number}';
";
                return MySQL_Method.ExecuteNonQuery(m_sSQL);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(uuid))
                {
                    _m_sErr = $"{number},网关UUID非空;";
                    return 0;
                }

                ///新增
                var as_parameter_sql = $@"  select ifnull((select v from dial_parameter where k='limitcount' limit 1),0) as limitcount,
	                                               ifnull((select v from dial_parameter where k='limitduration' limit 1),0) as limitduration,
	                                               ifnull((select v from dial_parameter where k='limitthecount' limit 1),0) as limitthecount,
	                                               ifnull((select v from dial_parameter where k='limittheduration' limit 1),0) as limittheduration,
	                                               ifnull((select v from dial_parameter where k='limitthedial' limit 1),0) as limitthedial,
	                                               ifnull((select v from dial_parameter where k='areacode' limit 1),'') as areacode,
	                                               ifnull((select v from dial_parameter where k='areaname' limit 1),'') as areaname,
	                                               ifnull((select v from dial_parameter where k='dialprefix' limit 1),'') as dialprefix,
	                                               ifnull((select v from dial_parameter where k='diallocalprefix' limit 1),'') as diallocalprefix;";
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
                                          '{dr["diallocalprefix"]}' as diallocalprefix,
                                          '{uuid}' as gwuid");
                    var as_insert_sql = $@"insert into dial_limit (number,useuser,adduser,addtime,tnumber,limitcount,limitduration,limitthecount,limittheduration,limitthedial,areacode,areaname,dialprefix,diallocalprefix,gwuid)
                                               select * from ({string.Join("\r\nunion\r\n", as_list.ToArray())}) as a 
                                               {((m_uType & 4) > 0 ? "" : " where cast(a.number as char(20)) not in (select number from dial_limit as b where b.isdel = 0) ")};";
                    return MySQL_Method.ExecuteNonQuery(as_insert_sql);
                }
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

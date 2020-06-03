using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataBaseUtil;
using Core_v1;
using Model_v1;
using WebSocket_v1;

namespace CenoCC {
    public class d_multi {
        public static int iu(DataTable _not, string adduser, string useuser, int isshare = 0, string gwuid = "") {
            var as_list = new List<string>();
            if(useuser == "-1") {
                /*
                 * 问题一:MySQL把@当参数
                 * 解决一:走俩次数据库,先把东西查出来
                 */
                var as_parameter_sql = $@"  select ifnull((select v from dial_parameter where k='limitcount' limit 1),0) as limitcount,
	                                               ifnull((select v from dial_parameter where k='limitduration' limit 1),0) as limitduration,
	                                               ifnull((select v from dial_parameter where k='limitthecount' limit 1),0) as limitthecount,
	                                               ifnull((select v from dial_parameter where k='limittheduration' limit 1),0) as limittheduration,
	                                               ifnull((select v from dial_parameter where k='limitthedial' limit 1),0) as limitthedial,
	                                               ifnull((select v from dial_parameter where k='areacode' limit 1),'') as areacode,
	                                               ifnull((select v from dial_parameter where k='areaname' limit 1),'') as areaname,
	                                               ifnull((select v from dial_parameter where k='dialprefix' limit 1),'') as dialprefix;";

                DataTable as_parameter_dt = MySQL_Method.BindTable(as_parameter_sql);
                string m_dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (as_parameter_dt != null && as_parameter_dt.Rows.Count > 0)
                {
                    DataRow dr = as_parameter_dt.Rows[0];
                    int i = 1;
                    foreach(DataRow _n in _not.Rows) {
                        as_list.Add($@"select (SELECT ifnull( max( id ), 0 ) + {i++} FROM dial_limit) as id,
                                              '{_n[0]}' as number,{adduser} as adduser,
                                              '{m_dtNow}',
                                               {dr["limitcount"]} as limitcount,{dr["limitduration"]} as limitduration,
                                               {dr["limitthecount"]} as limitthecount,{dr["limittheduration"]} as limittheduration,
                                               {dr["limitthedial"]} as limitthedial,
                                              '{dr["areacode"]}' as areacode,
                                              '{dr["areaname"]}' as areaname,
                                              '{gwuid}' as gwuid,
                                              '{dr["dialprefix"]}' as dialprefix,
                                               {isshare} as isshare");
                    }
                    var as_insert_sql = $@"insert into dial_limit (id,number,adduser,addtime,limitcount,limitduration,limitthecount,limittheduration,limitthedial,areacode,areaname,gwuid,dialprefix,isshare)
                                           select * from ({string.Join("\r\nunion\r\n", as_list.ToArray())}) as a 
                                           where cast(a.number as char(20)) not in (select number from dial_limit as b where b.isdel = 0);";
                    return MySQL_Method.ExecuteNonQuery(as_insert_sql);
                }
                else { return 0; }
            } else {
                //移除
                Log.Instance.Warn($"warn:d_multi iu not -1 is removed,return 0");
                return 0;
            }
        }

        public static int del(List<string> idlist) {
            var as_list = new List<string>();
            foreach(var id in idlist) {
                as_list.Add($"select {id} as id");
            }
            var as_del_sql = $"delete from dial_limit\r\nwhere id in ({string.Join("\r\nunion\r\n", as_list.ToArray())}) ";
            return MySQL_Method.ExecuteNonQuery(as_del_sql);
        }

        public static int use(List<string> idlist) {
            var as_list = new List<string>();
            foreach(var id in idlist) {
                as_list.Add($"select {id} as id");
            }
            var as_del_sql = $@"update dial_limit set
                                       isuse = 1
                                       where id in ({string.Join("\r\nunion\r\n", as_list.ToArray())}) ";
            return MySQL_Method.ExecuteNonQuery(as_del_sql);
        }

        public static int unuse(List<string> idlist) {
            var as_list = new List<string>();
            foreach(var id in idlist) {
                as_list.Add($"select {id} as id");
            }
            var as_del_sql = $@"update dial_limit set
                                       isuse = 0
                                       where id in ({string.Join("\r\nunion\r\n", as_list.ToArray())}) ";
            return MySQL_Method.ExecuteNonQuery(as_del_sql);
        }

        public static int quick(List<string> idlist, int useuser) {
            var as_list = new List<string>();
            foreach(var id in idlist) {
                as_list.Add($"select {id} as id");
            }
            var as_del_sql = $@"update dial_limit set
                                       useuser = {useuser}
                                       where id in ({string.Join("\r\nunion\r\n", as_list.ToArray())}) ";
            return MySQL_Method.ExecuteNonQuery(as_del_sql);
        }

        public static int gatewayadd(string gwName, string gwType, string gwOName, out string m_sErrMsg, string m_sUUID = "", string m_sRemark = "", string m_sXML = "")
        {
            m_sErrMsg = string.Empty;
            int i = 0;
            try
            {
                bool m_bIsHas = !string.IsNullOrWhiteSpace(m_sUUID);
                var as_count_sql = $"SELECT count( 1 ) as count FROM call_gateway WHERE gw_name = '{gwName}' {(m_bIsHas ? $" AND `uniqueid` != '{m_sUUID}' " : "")} ";

                int count = Convert.ToInt32(MySQL_Method.ExecuteScalar(as_count_sql));
                if (count <= 0)
                {
                    if (m_bIsHas)
                    {
                        ///追加gateway需添加XML的逻辑
                        if (gwType == Model_v1.m_mGatewayType._m_sGateway)
                        {
                            ///向服务器发送写入XML的命令,如果有直接修改,如果没有直接创建
                            object m_oObject = new
                            {
                                m_sName = gwName,
                                m_sXML = m_sXML
                            };
                            Model_v1.m_mResponseJSON _m_mResponseJSON = InWebSocketMain.SendAsyncObject(H_Json.ToJson(m_oObject), m_cFSCmdType._m_sWriteGateway);
                            if (_m_mResponseJSON.status != 0)
                            {
                                m_sErrMsg = $"修改网关XML文件失败:{_m_mResponseJSON.result ?? _m_mResponseJSON.msg}";
                                return 0;
                            }
                        }

                        string m_sSQL = $@"
UPDATE `call_gateway` 
SET `call_gateway`.`gw_name` = '{gwName}',
`call_gateway`.`gwtype` = '{(!string.IsNullOrWhiteSpace(gwOName) ? gwOName : gwType)}', 
`call_gateway`.`remark` = '{m_sRemark}'  
WHERE
	`UniqueID` = '{m_sUUID}';
";
                        i = MySQL_Method.ExecuteNonQuery(m_sSQL);
                        m_sErrMsg = "编辑成功";
                    }
                    else
                    {
                        ///追加gateway需添加XML的逻辑
                        if (gwType == Model_v1.m_mGatewayType._m_sGateway)
                        {
                            ///向服务器发送添加XML的命令,如果有则报错
                            object m_oObject = new
                            {
                                m_sName = gwName,
                                m_sXML = m_sXML
                            };
                            Model_v1.m_mResponseJSON _m_mResponseJSON = InWebSocketMain.SendAsyncObject(H_Json.ToJson(m_oObject), m_cFSCmdType._m_sCreateGateway);
                            if (_m_mResponseJSON.status != 0)
                            {
                                m_sErrMsg = $"添加网关XML文件失败:{_m_mResponseJSON.result ?? _m_mResponseJSON.msg}";
                                return 0;
                            }
                        }

                        var as_sql = $"INSERT INTO `call_gateway` VALUES (DEFAULT(id), uuid(), '{gwName}', '', '', '', null, null, null, null, null, '3600', '1', 'udp', '30', '0', 'tport=tcp', '25', '1', '1', '{m_sRemark}', '{(!string.IsNullOrWhiteSpace(gwOName) ? gwOName : gwType)}', '{Common.AgentInfo.AgentID}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}');";
                        i = MySQL_Method.ExecuteNonQuery(as_sql);
                        m_sErrMsg = "添加成功";
                    }
                }
                else
                {
                    m_sErrMsg = $"已存在同名网关";
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"d_multi gatewayadd:{ex.Message}");
                m_sErrMsg = $"{ex.Message}";
            }
            return i;
        }

        public static string m_fUpdateNumber(string m_sID, string m_sNumber, bool m_bSame)
        {
            string m_sResult = string.Empty;
            try
            {
                string m_sAsSELECT = $@"
SELECT
	count( 1 ) AS `num` 
FROM
	`dial_limit` 
WHERE
	`number` = '{m_sNumber}' 
	AND `id` <> {m_sID};
";
                if (Convert.ToInt32(MySQL_Method.ExecuteScalar(m_sAsSELECT)) <= 0 || m_bSame)
                {
                    string m_sAsUpdateSQL = $@"
UPDATE `dial_limit` 
SET `number` = '{m_sNumber}' 
WHERE
	`id` = {m_sID};
";
                    if (MySQL_Method.ExecuteNonQuery(m_sAsUpdateSQL) > 0)
                    {
                        m_sResult = $"修改号码成功:id:{m_sID}->number:{m_sNumber}";
                    }
                    else
                    {
                        m_sResult = "修改号码成功,但0行受影响!";
                    }
                }
                else
                {
                    m_sResult = "该号码已存在!";
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"d_multi m_fUpdateNumber:{ex.Message}");
                m_sResult = $"修改号码失败:{ex.Message}!";
            }
            return m_sResult;
        }

        public static string m_fUpdateTNumber(string m_sID, string m_sNumber)
        {
            string m_sResult = string.Empty;
            try
            {
                string m_sAsUpdateSQL = $@"
UPDATE `dial_limit` 
SET `tnumber` = '{m_sNumber}' 
WHERE
	`id` = {m_sID};
";
                if (MySQL_Method.ExecuteNonQuery(m_sAsUpdateSQL) > 0)
                {
                    m_sResult = $"修改真实号码成功:id:{m_sID}->tnumber:{m_sNumber}";
                }
                else
                {
                    m_sResult = "修改真实号码成功,但0行受影响!";
                }

            }
            catch (Exception ex)
            {
                Log.Instance.Error($"d_multi m_fUpdateTNumber:{ex.Message}");
                m_sResult = $"修改真实号码失败:{ex.Message}!";
            }
            return m_sResult;
        }

        public static string m_fUpdateOrderNum(string m_sID, string m_sOrderNum)
        {
            string m_sResult = string.Empty;
            try
            {
                string m_sAsUpdateSQL = $@"
UPDATE `dial_limit` 
SET `ordernum` = '{m_sOrderNum}' 
WHERE
	`id` = {m_sID};
";
                if (MySQL_Method.ExecuteNonQuery(m_sAsUpdateSQL) > 0)
                {
                    m_sResult = $"修改排序成功:id:{m_sID}->ordernum:{m_sOrderNum}";
                }
                else
                {
                    m_sResult = "修改排序成功,但0行受影响!";
                }

            }
            catch (Exception ex)
            {
                Log.Instance.Error($"d_multi m_fUpdateOrderNum:{ex.Message}");
                m_sResult = $"修改排序失败:{ex.Message}!";
            }
            return m_sResult;
        }

        public static DataSet repeat(m_deal _m_deal) {
            try {
                _m_deal.haserr = false;
                var as_sql_part_list = new List<string>();
                var type_varchar_first = true;
                foreach(var item in _m_deal.list) {
                    if(type_varchar_first) {
                        type_varchar_first = false;
                        as_sql_part_list.Add($"select convert(varchar(500),'{item}') as number");
                    } else {
                        as_sql_part_list.Add($"select '{item}' as number");
                    }
                }
                var _m_deal_arr = as_sql_part_list.ToArray();
                var as_sql_all_table = string.Join("\r\nunion all\r\n", _m_deal_arr);
                var as_sql_count = $@"
select number into #ta from (\r\n{as_sql_all_table}\r\n) as a;
select * from #ta;\r\nselect distinct number into #tr from #ta t1
where (select count(1) from #ta t2 where t1.number=t2.number)+
(select count(1) from diallimit t3 where t1.number=t3.number and t3.isdel=0)>1;
select * from #tr;
update #ta set
number=number+' [-Err 号码重复'+number+']'
where number in(select number from #tr);
select * from #ta;
drop table #ta;
drop table #tr";
                var as_ds = MySQL_Method.ExecuteDataSet(as_sql_count);
                return as_ds;
            } catch(Exception ex) {
                Log.Instance.Error($"d_multi repeat:{ex.Message}");
            }
            return null;
        }

        public static DataSet check(m_deal _m_deal) {
            try {
                _m_deal.haserr = false;
                var as_sql_part_list = new List<string>();
                var type_varchar_first = true;
                foreach(var item in _m_deal.list) {
                    if(type_varchar_first) {
                        type_varchar_first = false;
                        as_sql_part_list.Add($"select convert(varchar(500),'{item}') as number");
                    } else {
                        as_sql_part_list.Add($"select '{item}' as number");
                    }
                }
                var _m_deal_arr = as_sql_part_list.ToArray();
                var as_sql_all_table = string.Join("\r\nunion all\r\n", _m_deal_arr);
                var as_sql_count = $"select number into #ta from (\r\n{as_sql_all_table}\r\n) as a;\r\n"
                    + "select * from #ta;\r\n"
                    + "select count(1) as [count] from #ta t1\r\n"
                    + "where (select count(1) from #ta t2 where t2.number=t1.number)=1 and (select count(1) from diallimit t2 where t2.isdel=0 and t2.number=t1.number and t2.useuser=-1)=1"
                    + "select case\r\n"
                    + "when (select count(1) from diallimit t2 where t2.number=t1.number and t2.isdel=0)=0 then t1.number+' [-Err 号码未导入'+t1.number+']'\r\n"
                    + "when (select count(1) from #ta t2 where t2.number=t1.number)=1 and (select count(1) from diallimit t2 where t2.isdel=0 and t2.number=t1.number and t2.useuser=-1)=1 then t1.number\r\n"
                    + "when (select count(1) from #ta t2 where t2.number=t1.number)>1 or (select count(1) from diallimit t2 where t2.isdel=0 and t2.number=t1.number and t2.useuser=-1)>1 then t1.number+' [-Err 号码重复'+t1.number+']'\r\n"
                    + "when (select count(1) from diallimit t2 where t2.isdel=0 and t2.useuser!=-1 and t2.number=t1.number)=1 then t1.number+' [-Err 号码已分配'+t1.number+']'\r\n"
                    + "else t1.number+ ' [-Err 未知错误'+t1.number+']' end as number\r\n"
                    + "from #ta t1;\r\n"
                    + "drop table #ta;";
                var as_ds = MySQL_Method.ExecuteDataSet(as_sql_count);
                return as_ds;
            } catch(Exception ex) {
                Log.Instance.Error($"d_multi check:{ex.Message}");
            }
            return null;
        }

        public static string multi_phone_count(string _useuser) {
            try {
                var as_sql = $@"select
                                    count(1) as [count]
                                from diallimit
                                where isdel=0
                                and isuse=1
                                and useuser={_useuser};";
                var as_dt = MySQL_Method.BindTable(as_sql);
                if(as_dt != null && as_dt.Rows.Count > 0) {
                    var as_count = Convert.ToInt32(as_dt.Rows[0]["count"]);
                    if(as_count > 0) {
                        return $"{as_count}个电话号";
                    }
                }
            } catch(Exception ex) {
                Log.Instance.Error($"d_multi multi_phone_count:{ex.Message}");
            }
            return "不可用";
        }

        public static DataTable m_fTreeAccount()
        {
            try
            {
                string m_sSQL = $@"
SELECT
	ID,
	IFNULL( tfid, ( - 1 ) ) AS fID,
	TeamName AS n,
	'T' AS t 
FROM
	call_team UNION
SELECT
	ID,
	TeamID AS fID,
	CONCAT( LoginName, '(', AgentName, ')-用户' ) AS n,
	'A' AS t 
FROM
	call_agent;
";
                return MySQL_Method.BindTable(m_sSQL);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fTreeAccount][Exception][{ex.Message}]");
            }
            return null;
        }

        public static DataTable m_fTreeRole()
        {
            try
            {
                string m_sSQL = $@"
SELECT
	ID,
	'-1' AS fID,
	RoleName AS n,
	'R' AS t 
FROM
	call_role;
";
                return MySQL_Method.BindTable(m_sSQL);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fTreeRole][Exception][{ex.Message}]");
            }
            return null;
        }

        public static DataTable m_fTreeOperate()
        {
            try
            {
                string m_sSQL = $@"
SELECT
	ocode AS ID,
	IFNULL( ofcode, '-1' ) AS fID,
	oname AS n,
	otype AS t,
	odescription AS d 
FROM
	call_operate 
WHERE
	IFNULL( isdel, 0 ) = 0 
ORDER BY
	ordernum ASC;
";
                return MySQL_Method.BindTable(m_sSQL);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fTreeOperate][Exception][{ex.Message}]");
            }
            return null;
        }

        public static bool m_fSaveOperate(List<Model_v1.m_mTree> m_lTree, List<Model_v1.m_mTree> m_lOperateTree)
        {
            try
            {
                List<string> m_lSQL = new List<string>();

                foreach (Model_v1.m_mTree m_pTree in m_lTree)
                {
                    string _m_sSQL = $@"
DELETE 
FROM
	call_operatepower 
WHERE
	mtype = '{m_pTree.t}' 
	AND muuid = {m_pTree.ID};
";
                    m_lSQL.Add(_m_sSQL);
                }

                foreach (Model_v1.m_mTree m_pOperateTree in m_lOperateTree)
                {
                    foreach (Model_v1.m_mTree m_pTree in m_lTree)
                    {

                        string _m_sSQL = $@"
INSERT INTO call_operatepower ( ocode, mtype, muuid )
VALUES
	( '{m_pOperateTree.ID}', '{m_pTree.t}', {m_pTree.ID} );
";
                        m_lSQL.Add(_m_sSQL);
                    }
                }
                string m_sSQL = string.Join(string.Empty, m_lSQL);
                return MySQL_Method.ExecuteNonQuery(m_sSQL) > 0;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fSaveOperate][Exception][{ex.Message}]");
                return false;
            }
        }

        public static bool m_fClearOperate(List<Model_v1.m_mTree> m_lTree)
        {
            try
            {
                List<string> m_lSQL = new List<string>();

                foreach (Model_v1.m_mTree m_pTree in m_lTree)
                {
                    string _m_sSQL = $@"
DELETE 
FROM
	call_operatepower 
WHERE
	mtype = '{m_pTree.t}' 
	AND muuid = {m_pTree.ID};
";
                    m_lSQL.Add(_m_sSQL);
                }

                string m_sSQL = string.Join(string.Empty, m_lSQL);
                return MySQL_Method.ExecuteNonQuery(m_sSQL) > 0;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fClearOperate][Exception][{ex.Message}]");
                return false;
            }
        }

        public static DataTable m_fCheckedOperate(Model_v1.m_mTree m_pTree)
        {
            try
            {
                string m_sSQL = $@"
SELECT
	call_operatepower.ocode AS ID,
	call_operate.otype AS t 
FROM
	call_operatepower
	LEFT JOIN call_operate ON call_operatepower.ocode = call_operate.ocode 
WHERE
	call_operatepower.mtype = '{m_pTree.t}' 
	AND call_operatepower.muuid = '{m_pTree.ID}';
";
                return MySQL_Method.BindTable(m_sSQL);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fCheckedOperate][Exception][{ex.Message}]");
            }
            return null;
        }

        public static DataTable m_fTreeDataPowerType()
        {
            try
            {
                string m_sSQL = $@"
SELECT
	dcode AS ID,
	'-1' AS fID,
	dname AS n,
	'D' AS t 
FROM
	call_datapowertype 
ORDER BY
	ordernum ASC;
";
                return MySQL_Method.BindTable(m_sSQL);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fTreeDataPowerType][Exception][{ex.Message}]");
            }
            return null;
        }

        public static bool m_fSaveDataPower(List<Model_v1.m_mTree> m_lDataPowerTypeTree, List<Model_v1.m_mTree> m_lTree, List<Model_v1.m_mTree> m_lDataTree)
        {
            try
            {
                List<string> m_lSQL = new List<string>();

                foreach (Model_v1.m_mTree m_pDataPowerTypeTree in m_lDataPowerTypeTree)
                {
                    foreach (Model_v1.m_mTree m_pTree in m_lTree)
                    {
                        string _m_sSQL = $@"
DELETE 
FROM
	call_datapower 
WHERE
	call_datapower.dcode = '{m_pDataPowerTypeTree.ID}' 
	AND call_datapower.mtype = '{m_pTree.t}' 
	AND call_datapower.muuid = '{m_pTree.ID}';
";
                        m_lSQL.Add(_m_sSQL);
                    }
                }

                foreach (Model_v1.m_mTree m_pDataTree in m_lDataTree)
                {
                    foreach (Model_v1.m_mTree m_pDataPowerTypeTree in m_lDataPowerTypeTree)
                    {
                        foreach (Model_v1.m_mTree m_pTree in m_lTree)
                        {

                            string _m_sSQL = $@"
INSERT INTO call_datapower ( mtype, muuid, dtype, duuid, dcode )
VALUES
	( '{m_pTree.t}', '{m_pTree.ID}', '{m_pDataTree.t}', '{m_pDataTree.ID}', '{m_pDataPowerTypeTree.ID}' );
";
                            m_lSQL.Add(_m_sSQL);
                        }
                    }
                }
                string m_sSQL = string.Join(string.Empty, m_lSQL);
                return MySQL_Method.ExecuteNonQuery(m_sSQL) > 0;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fSaveDataPower][Exception][{ex.Message}]");
                return false;
            }
        }

        public static DataTable m_fCheckedData(Model_v1.m_mTree m_pDataPowerTypeTree, Model_v1.m_mTree m_pTree)
        {
            try
            {
                string m_sSQL = $@"
SELECT
	call_datapower.duuid AS ID,
	call_datapower.dtype AS t 
FROM
	call_datapower 
WHERE
	call_datapower.dcode = '{m_pDataPowerTypeTree.ID}' 
	AND call_datapower.mtype = '{m_pTree.t}' 
	AND call_datapower.muuid = '{m_pTree.ID}';
";
                return MySQL_Method.BindTable(m_sSQL);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fCheckedData][Exception][{ex.Message}]");
            }
            return null;
        }

        public static DataTable m_fTreeBaseTeam()
        {
            try
            {
                string m_sSQL = $@"
SELECT
	ID,
	IFNULL( tfid, ( - 1 ) ) AS fID,
	TeamName AS n,
	'T' AS t 
FROM
	call_team
";
                return MySQL_Method.BindTable(m_sSQL);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fTreeBaseTeam][Exception][{ex.Message}]");
            }
            return null;
        }

        public static DataTable m_fGetBaseTeamByID(object m_oID)
        {
            try
            {
                string m_sSQL = $@"
SELECT
	ID,
	IFNULL( tfid, ( - 1 ) ) AS fID,
	TeamName AS n,
	IFNULL( ordernum, 0 ) AS ordernum 
FROM
	call_team 
WHERE
	ID = {m_oID}
";
                return MySQL_Method.BindTable(m_sSQL);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fGetBaseTeamByID][Exception][{ex.Message}]");
            }
            return null;
        }

        public static DataTable m_fGetBaseRoleByID(object m_oID)
        {
            try
            {
                string m_sSQL = $@"
SELECT
	ID,
	RoleName AS n,
	RoleNO AS rno,
	RoleDescription AS rdesc 
FROM
	call_role 
WHERE
	ID = {m_oID}
";
                return MySQL_Method.BindTable(m_sSQL);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fGetBaseRoleByID][Exception][{ex.Message}]");
            }
            return null;
        }

        public static string m_fAddTeam(string m_sTeamName, string m_sTfID, decimal m_dOrderNum, out int m_uStatus)
        {
            m_uStatus = 0;
            try
            {
                string m_sSQL = $@"
SELECT
	count( 1 ) INTO @m_uCount 
FROM
	`call_team` 
WHERE
	`TeamName` = '{m_sTeamName}';
INSERT INTO `call_team` ( `ID`, `TeamName`, `tfid`, `ordernum` ) SELECT
* 
FROM
	(
	SELECT
		( SELECT IFNULL( MAX( `ID` ), 0 ) + 1 FROM `call_team` ) AS `ID`,
		'{m_sTeamName}' AS `TeamName`,
		{m_sTfID} AS `tfid`,
		{m_dOrderNum} AS `ordernum` 
	) AS T0 
WHERE
	@m_uCount = 0;
SELECT
	( CASE WHEN @m_uCount = 0 THEN 1 ELSE 0 END ) AS `status`,
	( CASE WHEN @m_uCount = 0 THEN '添加部门成功' ELSE '部门名称重复' END ) AS `msg`;
";
                DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                if (m_pDataTable != null && m_pDataTable.Rows.Count == 1)
                {
                    m_uStatus = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                    return m_pDataTable.Rows[0]["msg"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fAddTeam][Exception][{ex.Message}]");
                return $"添加部门失败:{ex.Message}";
            }
            return $"添加部门完成";
        }

        public static string m_fEditTeam(string m_sID, string m_sTeamName, string m_sTfID, decimal m_dOrderNum, out int m_uStatus)
        {
            m_uStatus = 0;
            try
            {
                string m_sSQL = $@"
SELECT
	count( 1 ) INTO @m_uCount 
FROM
	`call_team` 
WHERE
	`TeamName` = '{m_sTeamName}' 
	AND `ID` != {m_sID};
UPDATE `call_team` 
SET `TeamName` = '{m_sTeamName}',
tfid = {m_sTfID},
ordernum = {m_dOrderNum} 
WHERE
	`ID` = {m_sID} 
	AND @m_uCount = 0;
SELECT
	( CASE WHEN @m_uCount = 0 THEN 1 ELSE 0 END ) AS `status`,
	( CASE WHEN @m_uCount = 0 THEN '编辑部门成功' ELSE '部门名称重复' END ) AS `msg`;
";
                DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                if (m_pDataTable != null && m_pDataTable.Rows.Count == 1)
                {
                    m_uStatus = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                    return m_pDataTable.Rows[0]["msg"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fEditTeam][Exception][{ex.Message}]");
                return $"添加部门失败:{ex.Message}";
            }
            return $"添加部门完成";
        }

        public static string m_fDeleteTeam(string m_sID, out int m_uStatus)
        {
            m_uStatus = 0;
            try
            {
                string m_sSQL = $@"
SELECT
	count( 1 ) INTO @m_uCount1 
FROM
	`call_agent` 
WHERE
	`TeamID` = {m_sID};
SELECT
	count( 1 ) INTO @m_uCount2 
FROM
	`call_team` 
WHERE
	`tfid` = {m_sID};
SELECT
	@m_uCount1 + @m_uCount2 INTO @m_uCount;
DELETE 
FROM
	`call_team` 
WHERE
	`ID` = {m_sID} 
	AND @m_uCount = 0;
SELECT
	( CASE WHEN @m_uCount = 0 THEN 1 ELSE 0 END ) AS `status`,
	( CASE WHEN @m_uCount = 0 THEN '删除部门成功' ELSE '该部门有相关信息无法删除' END ) AS `msg`;
";
                DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                if (m_pDataTable != null && m_pDataTable.Rows.Count == 1)
                {
                    m_uStatus = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                    return m_pDataTable.Rows[0]["msg"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fDeleteTeam][Exception][{ex.Message}]");
                return $"删除部门失败:{ex.Message}";
            }
            return $"删除部门完成";
        }

        public static string m_fAddRole(string m_sRoleName, string m_sRoleDesc, out int m_uStatus)
        {
            m_uStatus = 0;
            try
            {
                string m_sSQL = $@"
SELECT
	count( 1 ) INTO @m_uCount 
FROM
	`call_role` 
WHERE
	`RoleName` = '{m_sRoleName}';
SELECT
	IFNULL( MAX( `ID` ), 0 ) + 1 INTO @m_uID 
FROM
	`call_role`;
INSERT INTO `call_role` ( `ID`, `RoleName`, `RoleNO`, `RoleDescription` ) SELECT
* 
FROM
	( SELECT @m_uID AS `ID`, '{m_sRoleName}' AS `RoleName`, @m_uID AS `RoleNO`, '{m_sRoleDesc}' AS `ordernum` ) AS T0 
WHERE
	@m_uCount = 0;
SELECT
	( CASE WHEN @m_uCount = 0 THEN 1 ELSE 0 END ) AS `status`,
	( CASE WHEN @m_uCount = 0 THEN '添加角色成功' ELSE '角色名称重复' END ) AS `msg`;
";
                DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                if (m_pDataTable != null && m_pDataTable.Rows.Count == 1)
                {
                    m_uStatus = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                    return m_pDataTable.Rows[0]["msg"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fAddRole][Exception][{ex.Message}]");
                return $"添加角色失败:{ex.Message}";
            }
            return $"添加角色完成";
        }

        public static string m_fEditRole(string m_sID, string m_sRoleName, string m_sRoleDesc, out int m_uStatus)
        {
            m_uStatus = 0;
            try
            {
                string m_sSQL = $@"
SELECT
	count( 1 ) INTO @m_uCount 
FROM
	`call_role` 
WHERE
	`RoleName` = '{m_sRoleName}' 
	AND `ID` != {m_sID};
UPDATE `call_role` 
SET `RoleName` = '{m_sRoleName}',
`RoleDescription` = '{m_sRoleDesc}' 
WHERE
	`ID` = {m_sID} 
	AND @m_uCount = 0;
SELECT
	( CASE WHEN @m_uCount = 0 THEN 1 ELSE 0 END ) AS `status`,
	( CASE WHEN @m_uCount = 0 THEN '编辑角色成功' ELSE '角色名称重复' END ) AS `msg`;
";
                DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                if (m_pDataTable != null && m_pDataTable.Rows.Count == 1)
                {
                    m_uStatus = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                    return m_pDataTable.Rows[0]["msg"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fEditRole][Exception][{ex.Message}]");
                return $"编辑角色失败:{ex.Message}";
            }
            return $"编辑角色完成";
        }

        public static string m_fDeleteRole(string m_sID, out int m_uStatus)
        {
            m_uStatus = 0;
            try
            {
                string m_sSQL = $@"
SELECT
	count( 1 ) INTO @m_uCount 
FROM
	`call_agent` 
WHERE
	`RoleID` = {m_sID};
DELETE 
FROM
	`call_role` 
WHERE
	`ID` = {m_sID} 
	AND @m_uCount = 0;
SELECT
	( CASE WHEN @m_uCount = 0 THEN 1 ELSE 0 END ) AS `status`,
	( CASE WHEN @m_uCount = 0 THEN '删除角色成功' ELSE '该角色有相关信息无法删除' END ) AS `msg`;
";
                DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                if (m_pDataTable != null && m_pDataTable.Rows.Count == 1)
                {
                    m_uStatus = Convert.ToInt32(m_pDataTable.Rows[0]["status"]);
                    return m_pDataTable.Rows[0]["msg"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][d_multi][m_fDeleteRole][Exception][{ex.Message}]");
                return $"删除角色失败:{ex.Message}";
            }
            return $"删除角色完成";
        }
    }
}

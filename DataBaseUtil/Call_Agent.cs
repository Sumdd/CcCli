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
using Common;
using System.Data;
using System.Collections.Generic;
using Core_v1;
using Model_v1;

namespace DataBaseUtil {
    /// <summary>
    /// 
    /// </summary>
    public class Call_AgentUtil {
        public Call_AgentUtil() {
            DateTime initDatetime = new DateTime(1900, 1, 1);
        }

        public static DataTable CheckLogin(string UserName, string UserPsw) {

            /*
             * 修正一:目前只支持16 SIP通道登陆
             * 拓展二:256为自动外呼通道,像往常一样配置即可,只是使用其来自动外呼
             */

            DataTable dt = new DataTable();
            string SqlStr = string.Format("select a.ID,a.UniqueID, a.agentname,a.loginname,a.loginpassword,a.loginstate,a.agentnumber,a.agentpassword,a.lastloginip,a.ChannelID,a.usable,a.LinkUser,a.LU_LoginName,a.LU_Password,t.id as TeamID,t.teamname,ags.statetype,r.id as RoleID,r.rolename,c.ChNo"
            + " from Call_Agent a left join Call_AgentState ags on a.stateid=ags.id left join Call_Role r on a.roleid=r.id left join Call_Team t on a.teamid=t.id left join Call_Channel c on a.ChannelID=c.ID"
                + " where c.ChType = 16 and a.LoginName='{0}' and a.LoginPassWord='{1}'", UserName, Encrypt.EncryptString(UserPsw));
            dt = MySQL_Method.BindTable(SqlStr);
            return dt;
        }

        public static void ResetUserPsw(string UserName) {
            string SqlStr = string.Format("update Call_Agent set LoginPassWord='" + Encrypt.EncryptString("0000") + "' where LoginName='{0}'", UserName);
            MySQL_Method.BindTable(SqlStr);
        }

        public static void InsertLoginLog(string AgentID, string Ip, string Flag) {
            string SqlStr = string.Format("insert Call_LoginLog (AgentID,LoginFlag,ThisLoginIp,Time) values ({0},{1},'{2}','{3}')", AgentID, Flag, Ip, CommonParam.GetNowDateTime);
            MySQL_Method.ExecuteNonQuery(SqlStr);
        }

        public static void UpdateLoginState(string AgentID, string Flag) {
            string SqlStr = string.Format("update Call_Agent set LoginState='{0}',LastLoginIp='{1}' where ID={2}", Flag, CommonParam.GetLocalIpAddress, AgentID);
            MySQL_Method.ExecuteNonQuery(SqlStr);
        }

        public static GlobalData.PhoneType GetPhoneType(string AgentID) {
            string SqlStr = "select ChType from Call_Agent left join Call_Channel on Call_Agent.ChannelID=Call_Channel.ID where Call_Agent.ID=" + AgentID;
            DataTable dt = MySQL_Method.BindTable(SqlStr);
            if(dt.Rows.Count > 0) {
                switch(dt.Rows[0]["ChType"].ToString()) {
                    case "2":
                        return GlobalData.PhoneType.TELEPHONE;
                        break;
                    case "16":
                        return GlobalData.PhoneType.SIP_SOFT_PHONE;
                        break;
                    default:
                        return GlobalData.PhoneType.OTHER;
                        break;
                }
            }
            return GlobalData.PhoneType.OTHER;
        }

        public static bool SetParamValueByName(string P_Name, object P_Value) {
            string SqlStr = "update Call_Agent set " + P_Name + "='" + P_Value + "' where Call_Agent.ID=" + AgentInfo.AgentID;
            DataTable dt = MySQL_Method.BindTable(SqlStr);
            return dt.Rows.Count > 0;
        }

        public static bool Has(string P_Name, object P_Value) {
            string SqlStr = "select count(*) from Call_Agent where " + P_Name + "='" + P_Value + "' and Call_Agent.ID !=" + AgentInfo.AgentID;
            int i = 0;
            int.TryParse(MySQL_Method.ExecuteScalar(SqlStr).ToString(), out i);
            return i > 0;
        }

        [Obsolete("请使用m_fGetAgentList")]
        public static DataTable GetList() {
            string asSQL = "select 0 as ID, '全部' as AgentName Union all select ID,AgentName from Call_Agent";
            DataTable dt = MySQL_Method.BindTable(asSQL);
            return dt;
        }

        public static DataTable m_fGetAgentList(string m_sPopedomSQL, int m_uUa = -1)
        {
            string asSQL = $@"
SELECT
	a.id AS EmpID,
	concat( a.loginname, '(', a.AgentName, ')' ) AS lr 
FROM
	call_agent AS a 
    {(m_uUa == -11 || m_uUa == -12 ? $@"
	LEFT JOIN (
	SELECT
		`dial_limit`.`useuser`,
		ifnull( sum( CASE WHEN isuse = 1 THEN 1 ELSE 0 END ), 0 ) AS usecount,
		sum( 1 ) AS allcount 
	FROM
		`dial_limit` 
	GROUP BY
		`dial_limit`.`useuser` 
	) AS `b` ON `b`.`useuser` = `a`.`ID`" : "")}
WHERE
	1 = 1
    {(m_uUa == -11 ? " AND ifnull(`allcount`,0) <= 0 " : "")}
    {(m_uUa == -12 ? " AND ifnull(`usecount`,0) <= 0 " : "")}
    {m_sPopedomSQL};
";
            DataTable dt = MySQL_Method.BindTable(asSQL);
            return dt;
        }

        public static int m_fResetPwd(List<string> idlist)
        {
            var as_list = new List<string>();
            foreach (var id in idlist)
            {
                as_list.Add($"select {id} as id");
            }
            var as_del_sql = $@"update call_agent set
                                       LoginPassWord = '{Encrypt.EncryptString("0000")}'
                                       where id in ({string.Join("\r\nunion\r\n", as_list.ToArray())}) ";
            return MySQL_Method.ExecuteNonQuery(as_del_sql);
        }

        public static string m_fGetAgentName(string m_sChannelNumberString)
        {
            string m_sAgentNameString = "未知";
            try
            {
                string m_sSelectSQL = $@"
SELECT
	AgentName 
FROM
	call_agent
	LEFT JOIN call_channel ON call_agent.ChannelID = call_channel.ID 
WHERE
	call_channel.ChNum = '{m_sChannelNumberString}';
";
                m_sAgentNameString = MySQL_Method.ExecuteScalar(m_sSelectSQL).ToString();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[DataBaseUtil][Call_AgentUtil][m_fGetAgentName][Exception][{ex.Message}]");
            }
            return m_sAgentNameString;
        }
    }
}

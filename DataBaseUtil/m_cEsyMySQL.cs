using Core_v1;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataBaseUtil
{
    public class m_cEsyMySQL
    {
        public static DataTable m_fGetLocalNumberList(string _useuser)
        {
            List<MySqlParameter> m_pMySqlParameter = new List<MySqlParameter>();
            m_pMySqlParameter.Add(new MySqlParameter("?_useuser", _useuser));
            m_pMySqlParameter.Add(new MySqlParameter("?m_sLoginName", string.Empty));
            DataSet ds = MySQL_Method.ExecuteDataSetByProcedure("proc_get_dial_limit_list", m_pMySqlParameter.ToArray());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public static void m_fGetDialArea()
        {
            try
            {
                string m_sSQL = $@"
SELECT
	* 
FROM
	`dial_area` 
WHERE
	( `dial_area`.`amain` = 2 AND `dial_area`.`astate` = 2 ) 
	OR ( `dial_area`.`amain` = 1 AND `dial_area`.`astate` IN ( 2, 4 ) );
";
                DataTable dt = MySQL_Method.BindTable(m_sSQL);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Model_v1.dial_area m_pDialArea = new Model_v1.dial_area();
                        m_pDialArea.id = Convert.ToInt32(item["id"]);
                        m_pDialArea.aname = item["aname"].ToString();
                        m_pDialArea.aip = item["aip"].ToString();
                        m_pDialArea.aport = Convert.ToInt32(item["aport"]);
                        m_pDialArea.adb = item["adb"].ToString();
                        m_pDialArea.auid = item["auid"].ToString();
                        m_pDialArea.apwd = item["apwd"].ToString();
                        m_pDialArea.amain = Convert.ToInt32(item["amain"]);
                        m_pDialArea.astate = Convert.ToInt32(item["astate"]);

                        ///<![CDATA[
                        /// 本机域或是主域,一起加入
                        ///]]>

                        if (m_pDialArea.amain == 2 && m_pDialArea.astate == 2)
                            Redis2.m_EsyDialArea = m_pDialArea;
                        else if (m_pDialArea.amain == 1 && (m_pDialArea.astate == 2 || m_pDialArea.astate == 4))
                            Redis2.m_EsyMainDialArea = m_pDialArea;

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[DataBaseUtil][m_cEsyMySQL][m_fGetDialArea][Exception][{ex.Message}]");
            }
        }

        public static DataTable m_fSetDialArea(int m_uID, string m_sName, string m_sIP, int m_uPort, string m_sDb, string m_sUid, string m_sPwd, int m_uMain)
        {
            List<MySqlParameter> m_pMySqlParameter = new List<MySqlParameter>();
            m_pMySqlParameter.Add(new MySqlParameter("?m_uID", m_uID));
            m_pMySqlParameter.Add(new MySqlParameter("?m_sName", m_sName));
            m_pMySqlParameter.Add(new MySqlParameter("?m_sIP", m_sIP));
            m_pMySqlParameter.Add(new MySqlParameter("?m_uPort", m_uPort));
            m_pMySqlParameter.Add(new MySqlParameter("?m_sDb", m_sDb));
            m_pMySqlParameter.Add(new MySqlParameter("?m_sUid", m_sUid));
            m_pMySqlParameter.Add(new MySqlParameter("?m_sPwd", m_sPwd));
            m_pMySqlParameter.Add(new MySqlParameter("?m_uMain", m_uMain));
            DataSet ds = MySQL_Method.ExecuteDataSetByProcedure("proc_set_dial_area", m_pMySqlParameter.ToArray());
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public static DataTable m_fDelDialArea(int m_uID)
        {
            List<MySqlParameter> m_pMySqlParameter = new List<MySqlParameter>();
            m_pMySqlParameter.Add(new MySqlParameter("?m_uID", m_uID));
            DataSet ds = MySQL_Method.ExecuteDataSetByProcedure("proc_del_dial_area", m_pMySqlParameter.ToArray());
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public static DataTable m_fReqDialArea(int m_uID, int m_uState)
        {
            List<MySqlParameter> m_pMySqlParameter = new List<MySqlParameter>();
            m_pMySqlParameter.Add(new MySqlParameter("?m_uID", m_uID));
            m_pMySqlParameter.Add(new MySqlParameter("?m_uState", m_uState));
            DataSet ds = MySQL_Method.ExecuteDataSetByProcedure("proc_req_dial_area", m_pMySqlParameter.ToArray());
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public static void m_fConnSetDialArea(string m_sName, string m_sIP, int m_uPort, string m_sDb, string m_sUid, string m_sPwd, int m_uState, Model_v1.dial_area m_pDialArea, out int m_uStatus, ref string m_sMsg)
        {
            m_uStatus = 0;
            try
            {
                string m_sConnStr = DataBaseUtil.MySQLDBConnectionString.m_fConnStr(m_pDialArea);
                List<MySqlParameter> m_pMySqlParameter = new List<MySqlParameter>();
                m_pMySqlParameter.Add(new MySqlParameter("?m_sName", m_sName));
                m_pMySqlParameter.Add(new MySqlParameter("?m_sIP", m_sIP));
                m_pMySqlParameter.Add(new MySqlParameter("?m_uPort", m_uPort));
                m_pMySqlParameter.Add(new MySqlParameter("?m_sDb", m_sDb));
                m_pMySqlParameter.Add(new MySqlParameter("?m_sUid", m_sUid));
                m_pMySqlParameter.Add(new MySqlParameter("?m_sPwd", m_sPwd));
                m_pMySqlParameter.Add(new MySqlParameter("?m_uState", m_uState));
                DataSet ds = MySQL_Method.ExecuteDataSetByProcedure("proc_iou_dial_area", m_pMySqlParameter.ToArray(), m_sConnStr);
                if (ds?.Tables?[0].Rows?.Count > 0)
                {
                    DataRow m_pDataRow = ds.Tables[0].Rows[0];
                    m_uStatus = Convert.ToInt32(m_pDataRow["status"]);
                    m_sMsg += $",{m_pDataRow["msg"].ToString()}";
                }
                else
                {
                    m_sMsg += $",同步操作无结果";
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[DataBaseUtil][m_cEsyMySQL][m_fConnSetDialArea][Exception][{ex.Message}]");
                m_sMsg += $",同步操作结果时出错:{ex.Message}";
            }
        }

        public static DataTable m_fGetFileType()
        {
            try
            {
                string m_sSQL = $@"
SELECT
	'' AS `ID`,
	'全部' AS `Name` UNION
SELECT
	ftype AS `ID`,
	ftype AS `Name` 
FROM
	( SELECT call_file.ftype FROM call_file GROUP BY call_file.ftype ORDER BY call_file.ftype ) T0;
";
                DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                return m_pDataTable;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[DataBaseUtil][m_cEsyMySQL][m_fGetType][Exception][{ex.Message}]");
            }
            return null;
        }

        public static DataTable m_fGetTeam()
        {
            try
            {
                string m_sSQL = $@"
SELECT
	`call_team`.`ID`,
	`call_team`.`TeamName` AS `n` 
FROM
	`call_team` 
ORDER BY
	`call_team`.`ID`;
";
                DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                return m_pDataTable;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[DataBaseUtil][m_cEsyMySQL][m_fGetTeam][Exception][{ex.Message}]");
            }
            return null;
        }

        public static DataTable m_fGetRole()
        {
            try
            {
                string m_sSQL = $@"
SELECT
	`call_role`.`ID`,
	`call_role`.`RoleName` AS `n` 
FROM
	`call_role`;
";
                DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                return m_pDataTable;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[DataBaseUtil][m_cEsyMySQL][m_fGetRole][Exception][{ex.Message}]");
            }
            return null;
        }


        private static string _m_sFreeSWITCHIPv4;
        public static string m_sFreeSWITCHIPv4
        {
            get
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(_m_sFreeSWITCHIPv4)) return _m_sFreeSWITCHIPv4;

                    ///首次加载
                    string m_sSQL = $@"
SELECT
	`dial_area`.`aip` 
FROM
	`dial_area` 
WHERE
	`dial_area`.`amain` = 2 
	LIMIT 1;
";
                    DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                    ///赋值
                    if (m_pDataTable != null && m_pDataTable.Rows.Count > 0) _m_sFreeSWITCHIPv4 = m_pDataTable.Rows[0]["aip"].ToString();
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[DataBaseUtil][m_cEsyMySQL][m_sFreeSWITCHIPv4][Exception][{ex.Message}]");
                }

                ///未得到赋值
                if (string.IsNullOrWhiteSpace(_m_sFreeSWITCHIPv4)) _m_sFreeSWITCHIPv4 = m_cProfile.server;

                return _m_sFreeSWITCHIPv4;
            }
        }
    }
}

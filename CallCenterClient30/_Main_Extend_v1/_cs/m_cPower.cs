using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseUtil;
using Core_v1;
using System.Data;
using Model_v1;

namespace CenoCC
{
    public class m_cPower
    {
        public static List<string> m_lOperatePower = new List<string>();
        public static void m_fGetOperatePower()
        {
            try
            {
                string m_sSQL = $@"
SELECT
	ocode,
	mtype,
	muuid 
FROM
	call_operatepower 
WHERE
	( mtype = 'T' AND muuid = {Common.AgentInfo.TeamID} ) 
	OR ( mtype = 'R' AND muuid = {Common.AgentInfo.RoleID} ) 
	OR ( mtype = 'A' AND muuid = {Common.AgentInfo.AgentID} );
";
                ///直接查出所有录音
                DataTable m_pDataTable = MySQL_Method.BindTable(m_sSQL);
                ///账号权限最大
                DataRow[] m_lAccountDataRow = m_pDataTable.Select(" [mtype] = 'A' ");
                if (m_lAccountDataRow?.Count() > 0)
                {
                    m_lOperatePower.Clear();
                    m_lOperatePower.AddRange((from r in m_lAccountDataRow.AsEnumerable()
                                              select r["ocode"].ToString()));
                    return;
                }
                ///部门其次
                DataRow[] m_lTeamDataRow = m_pDataTable.Select(" [mtype] = 'T' ");
                if (m_lTeamDataRow?.Count() > 0)
                {
                    m_lOperatePower.Clear();
                    m_lOperatePower.AddRange((from r in m_lTeamDataRow.AsEnumerable()
                                              select r["ocode"].ToString()));
                    return;
                }
                ///角色最小
                DataRow[] m_lRoleDataRow = m_pDataTable.Select(" [mtype] = 'R' ");
                if (m_lRoleDataRow?.Count() > 0)
                {
                    m_lOperatePower.Clear();
                    m_lOperatePower.AddRange((from r in m_lRoleDataRow.AsEnumerable()
                                              select r["ocode"].ToString()));
                    return;
                }
                ///结束
                {
                    m_lOperatePower.Clear();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][m_cPower][m_fGetOperatePower][Exception][{ex.Message}]");
            }
            finally
            {
                ///由于全号显示的引入,让一键拨号可以受到此操作权限的控制
                WebSocket_v1.InWebSocketMain.Send(CenoSocket.M_Send._zdwh("ReloadO"));
            }
        }

        public static bool Has(string m_sOperate)
        {
            try
            {
                if (m_lOperatePower == null)
                    throw new ArgumentException("m_lOperatePower");

                if (m_sOperate == null)
                    throw new ArgumentException("m_sOperate");

                if (m_cPower.m_lOperatePower.Contains(m_sOperate))
                    return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][m_cPower][Has][Exception][{ex.Message}]");
            }
            return false;
        }

        public static List<DataPower> m_lDataPower = new List<DataPower>();

        public static void m_fGetDataPower()
        {
            try
            {
                string m_sSQL = $@"
SELECT
	call_datapowertype.dcode,
	call_datapowertype.dsetting 
FROM
	call_datapowertype;
SELECT
	call_datapower.dcode,
    call_datapower.mtype,
	call_datapower.dtype,
	call_datapower.duuid 
FROM
	call_datapower 
WHERE
	( call_datapower.mtype = 'T' AND call_datapower.muuid = '{Common.AgentInfo.TeamID}' ) 
	OR ( call_datapower.mtype = 'R' AND call_datapower.muuid = '{Common.AgentInfo.RoleID}' ) 
	OR ( call_datapower.mtype = 'A' AND call_datapower.muuid = '{Common.AgentInfo.AgentID}' );
SELECT
	ID,
	TeamID 
FROM
	call_agent;
";
                ///移除
                m_lDataPower.Clear();
                ///查询
                DataSet m_pDataSet = MySQL_Method.ExecuteDataSet(m_sSQL);
                ///处理各个权限,然后放入
                if (m_pDataSet != null && m_pDataSet.Tables.Count == 3)
                {
                    foreach (DataRow m_pDataRow in m_pDataSet.Tables[0].Rows)
                    {
                        string m_sDataPowerType = m_pDataRow["dcode"]?.ToString();
                        ///直接查出所有录音
                        DataTable m_pDataTable = m_pDataSet.Tables[1];
                        ///账号权限最大
                        DataRow[] m_lAccountDataRow = m_pDataTable.Select($" [mtype] = 'A' AND [dcode] = '{m_sDataPowerType}' ");
                        DataRow[] m_lAAccountDataRow = m_pDataTable.Select($" [mtype] = 'A' AND [dtype] = 'A' AND [dcode] = '{m_sDataPowerType}' ");
                        DataRow[] m_lATeamDataRow = m_pDataTable.Select($" [mtype] = 'A' AND [dtype] = 'T' AND [dcode] = '{m_sDataPowerType}' ");
                        ///如果账户有对应权限
                        if (m_lAccountDataRow?.Count() > 0)
                        {
                            ///如果有A
                            if (m_lAAccountDataRow.Count() > 0)
                            {
                                m_lDataPower.AddRange((from r in m_lAAccountDataRow.AsEnumerable()
                                                       select new DataPower()
                                                       {
                                                           dcode = m_sDataPowerType,
                                                           duuid = r["duuid"].ToString()
                                                       }));
                            }
                            else if (m_lATeamDataRow?.Count() > 0)
                            {
                                DataRow[] _m_lAccountDataRow = m_pDataSet.Tables[2].Select($" [TeamID] IN ('{string.Join("','", m_lATeamDataRow.Select(x => x.Field<object>("duuid")))}') ");
                                m_lDataPower.AddRange((from r in _m_lAccountDataRow.AsEnumerable()
                                                       select new DataPower()
                                                       {
                                                           dcode = m_sDataPowerType,
                                                           duuid = r["ID"].ToString()
                                                       }));
                            }
                            continue;
                        }
                        ///部门权限次之
                        DataRow[] m_lTeamDataRow = m_pDataTable.Select($" [mtype] = 'T' AND [dcode] = '{m_sDataPowerType}' ");
                        DataRow[] m_lTAccountDataRow = m_pDataTable.Select($" [mtype] = 'T' AND [dtype] = 'A' AND [dcode] = '{m_sDataPowerType}' ");
                        DataRow[] m_lTTeamDataRow = m_pDataTable.Select($" [mtype] = 'T' AND [dtype] = 'T' AND [dcode] = '{m_sDataPowerType}' ");
                        ///如果部门有对应权限
                        if (m_lTeamDataRow?.Count() > 0)
                        {
                            ///如果有A
                            if (m_lTAccountDataRow.Count() > 0)
                            {
                                m_lDataPower.AddRange((from r in m_lTAccountDataRow.AsEnumerable()
                                                       select new DataPower()
                                                       {
                                                           dcode = m_sDataPowerType,
                                                           duuid = r["duuid"].ToString()
                                                       }));
                            }
                            else if (m_lTTeamDataRow?.Count() > 0)
                            {
                                DataRow[] _m_lAccountDataRow = m_pDataSet.Tables[2].Select($" [TeamID] IN ('{string.Join("','", m_lTTeamDataRow.Select(x => x.Field<object>("duuid")))}') ");
                                m_lDataPower.AddRange((from r in _m_lAccountDataRow.AsEnumerable()
                                                       select new DataPower()
                                                       {
                                                           dcode = m_sDataPowerType,
                                                           duuid = r["ID"].ToString()
                                                       }));
                            }
                            continue;
                        }
                        ///角色最后
                        DataRow[] m_lRoleDataRow = m_pDataTable.Select($" [mtype] = 'R' AND [dcode] = '{m_sDataPowerType}' ");
                        DataRow[] m_lRAccountDataRow = m_pDataTable.Select($" [mtype] = 'R' AND [dtype] = 'A' AND [dcode] = '{m_sDataPowerType}' ");
                        DataRow[] m_lRTeamDataRow = m_pDataTable.Select($" [mtype] = 'R' AND [dtype] = 'T' AND [dcode] = '{m_sDataPowerType}' ");
                        ///如果角色有对应权限
                        if (m_lRoleDataRow?.Count() > 0)
                        {
                            ///如果有A
                            if (m_lRAccountDataRow.Count() > 0)
                            {
                                m_lDataPower.AddRange((from r in m_lRAccountDataRow.AsEnumerable()
                                                       select new DataPower()
                                                       {
                                                           dcode = m_sDataPowerType,
                                                           duuid = r["duuid"].ToString()
                                                       }));
                            }
                            else if (m_lRTeamDataRow?.Count() > 0)
                            {
                                DataRow[] _m_lAccountDataRow = m_pDataSet.Tables[2].Select($" [TeamID] IN ('{string.Join("','", m_lRTeamDataRow.Select(x => x.Field<object>("duuid")))}') ");
                                m_lDataPower.AddRange((from r in _m_lAccountDataRow.AsEnumerable()
                                                       select new DataPower()
                                                       {
                                                           dcode = m_sDataPowerType,
                                                           duuid = r["ID"].ToString()
                                                       }));
                            }
                            continue;
                        }
                        ///是否将自己的ID写入
                        if (Convert.ToInt32(m_pDataRow["dsetting"]) * 1 > 0)
                        {
                            m_lDataPower.Add(new DataPower()
                            {
                                dcode = m_sDataPowerType,
                                duuid = Common.AgentInfo.AgentID
                            });
                        }
                    }
                }
                else
                {
                    Log.Instance.Error($"[CenoCC][m_cPower][m_fGetDataPower][[获取数据权限时出错:数据集数量不对]");
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][m_cPower][m_fGetDataPower][Exception][{ex.Message}]");
            }
        }


        public static DataTable ToDataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return null;
            DataTable tmp = rows[0].Table.Clone();  // 复制DataRow的表结构
            foreach (DataRow row in rows)
                tmp.Rows.Add(row.ItemArray);  // 将DataRow添加到DataTable中
            return tmp;
        }

        public static string m_fPopedomSQL(PopedomArgs m_pPopedomArgs)
        {
            try
            {
                List<DataPower> _m_lDataPower = m_lDataPower.Where(x => x.dcode == m_pPopedomArgs.type)?.ToList();
                if (_m_lDataPower != null && _m_lDataPower.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    if (!m_pPopedomArgs.part)
                        sb.Append(" AND ( 1=2 ");
                    else
                        sb.Append(" 1=2 ");
                    foreach (DataPower item in _m_lDataPower)
                    {
                        foreach (string left in m_pPopedomArgs.left)
                        {
                            sb.Append($" OR {left} = '{item.duuid}' ");
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(m_pPopedomArgs.TSQL))
                        sb.Append($" {m_pPopedomArgs.TSQL} ");
                    if (!m_pPopedomArgs.part)
                        sb.Append(" ) ");
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][m_cPower][m_fPopedomSQL][Exception][{ex.Message}]");
            }
            return " AND 1=2 ";
        }
    }
}

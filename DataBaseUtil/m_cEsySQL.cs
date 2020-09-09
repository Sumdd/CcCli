using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using Core_v1;

namespace DataBaseUtil
{
    public class m_cEsySQL
    {
        private static ConnectionConfig m_pConfig
        {
            get
            {
                return new ConnectionConfig()
                {
                    ConnectionString = Call_ParamUtil.HomeConnString,
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true
                };
            }
        }

        public static string m_fGetContact(string m_sPhoneNumberString)
        {
            try
            {
                SqlSugarClient esyClient = new SqlSugarClient(m_cEsySQL.m_pConfig);

                ///修正传往数据库的查询语句
                string m_sSQL = Call_ParamUtil.HomeSelectString;
                if (!string.IsNullOrWhiteSpace(m_sSQL))
                {
                    if (m_sSQL.Contains("@args"))
                    {
                        object m_oObject = esyClient.Ado.GetScalar(Call_ParamUtil.HomeSelectString, new { args = m_sPhoneNumberString });
                        if (m_oObject != null)
                            return m_oObject.ToString();
                    }
                    else if (m_sSQL.Contains("@where"))
                    {
                        ///非空,不查
                        if (m_sPhoneNumberString == null) return null;

                        ///可能1
                        string _1 = m_sPhoneNumberString.TrimStart('0');

                        ///过短,不查
                        if (_1.Length < 6) return null;

                        ///可能2
                        string _2 = $"0{_1}";

                        ///可能3
                        string _3 = $"00{_2}";

                        ///条件
                        string m_sWhere = $" Phone IN ( @_1, @_2, @_3 ) ";

                        object m_oObject = esyClient.Ado.GetScalar(Call_ParamUtil.HomeSelectString.Replace("@where", m_sWhere), new { _1 = _1, _2 = _2, _3 = _3 });
                        if (m_oObject != null)
                            return m_oObject.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[DataBaseUtil][m_cEsySQL][m_fGetContact][Exception][{ex.Message}]");
            }
            return null;
        }
    }
}

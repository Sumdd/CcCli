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
                object m_oObject = esyClient.Ado.GetScalar(Call_ParamUtil.HomeSelectString, new { args = m_sPhoneNumberString });
                if (m_oObject != null)
                    return m_oObject.ToString();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[DataBaseUtil][m_cEsySQL][m_fGetContact][Exception][{ex.Message}]");
            }
            return null;
        }
    }
}

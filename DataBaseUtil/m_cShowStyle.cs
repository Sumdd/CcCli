using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Core_v1;

namespace DataBaseUtil
{
    public class m_cShowStyle
    {

        public class m_mContact
        {
            public string m_sRealNameString
            {
                get;
                set;
            }

            public DateTime m_dtUpdateTime
            {
                get;
                set;
            }
        }

        public static m_mContact m_fGetContact(string m_sPhoneNumberString)
        {
            try
            {
                List<MySqlParameter> m_pMySqlParameter = new List<MySqlParameter>();
                m_pMySqlParameter.Add(new MySqlParameter("?args_number", m_sPhoneNumberString));
                DataSet ds = MySQL_Method.ExecuteDataSetByProcedure("proc_get_realname_by_phone", m_pMySqlParameter.ToArray());
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return new m_mContact()
                    {
                        m_sRealNameString = ds.Tables[0].Rows[0]["realname"].ToString(),
                        m_dtUpdateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["updatetime"])
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[DataBaseUtil][m_cShowStyle][m_fGetContact][Exception][{ex.Message}]");
            }
            return null;
        }

        public static void m_fSetContact(string m_sPhoneNumberString, string m_sRealNameString)
        {
            try
            {
                List<MySqlParameter> m_pMySqlParameter = new List<MySqlParameter>();
                m_pMySqlParameter.Add(new MySqlParameter("?args_number", m_sPhoneNumberString));
                m_pMySqlParameter.Add(new MySqlParameter("?args_realname", m_sRealNameString));
                MySQL_Method.ExecuteDataSetByProcedure("proc_set_realname_by_phone", m_pMySqlParameter.ToArray());
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[DataBaseUtil][m_cShowStyle][m_fSetContact][Exception][{ex.Message}]");
            }
        }
    }
}

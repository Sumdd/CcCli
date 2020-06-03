using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using Core_v1;

namespace DataBaseUtil {
    public class MySQLDBConnectionString {
        public MySqlConnection ConnectionString {
            get {
                return MySQLDBConnectionString.GetNewConnectionString();
            }
        }

        public static string m_fConnStr(Model_v1.dial_area m_pDialArea)
        {
            if (m_pDialArea == null)
                throw new ArgumentNullException("m_pDialArea");

            if (m_pDialArea.aip == null)
                throw new ArgumentNullException("aip");

            if (m_pDialArea.adb == null)
                throw new ArgumentNullException("adb");

            if (m_pDialArea.auid == null)
                throw new ArgumentNullException("auid");

            if (m_pDialArea.apwd == null)
                throw new ArgumentNullException("apwd");

            return $"server={m_pDialArea.aip};database={m_pDialArea.adb};user id={m_pDialArea.auid};password={Common.Encrypt.DecryptString(m_pDialArea.apwd)};Charset=utf8;Allow User Variables=True";
        }

        public static MySqlConnection GetNewConnectionString() {
            string M_str_sqlcon;
            //the database name must same as mysql server
            if (string.IsNullOrEmpty(m_cProfile.server) || string.IsNullOrEmpty(m_cProfile.database) || string.IsNullOrEmpty(m_cProfile.uid) || string.IsNullOrEmpty(m_cProfile.pwd))
                M_str_sqlcon = "server=192.168.0.220;database=cmcp10;user id=root;password=123;Charset=utf8;Allow User Variables=True";
            else
                M_str_sqlcon = "server=" + m_cProfile.server + ";database=" + m_cProfile.database + ";user id=" + m_cProfile.uid + ";password=" + Common.Encrypt.DecryptString(m_cProfile.pwd) + ";Charset=utf8;Allow User Variables=True";

            MySqlConnection myCon = new MySqlConnection(M_str_sqlcon);
            return myCon;
        }
    }
}

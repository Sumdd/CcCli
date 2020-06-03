using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Core_v1
{
    public class m_cProfile
    {
        #region ***前置
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);  //获取ini文件中的数据
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);  //写入ini文件数据

        private static string IniFilePath = Environment.CurrentDirectory + "\\client.ini";
        #endregion

        #region ***ini读取
        private static string m_fGet(string m_sArea, string m_sName, string m_sDefault = "")
        {
            try
            {
                StringBuilder ParamStrBul = new StringBuilder(2000);
                m_cProfile.GetPrivateProfileString(m_sArea, m_sName, m_sDefault, ParamStrBul, 2000, IniFilePath);
                return ParamStrBul.ToString();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[Core_v1][m_cProfile][m_fGet][Exception][{ex.Message}]");
                return m_sDefault;
            }
        }
        #endregion

        #region ***ini写入
        private static void m_fSet(string m_sArea, string m_sName, string m_sVal)
        {
            try
            {
                m_cProfile.WritePrivateProfileString(m_sArea, m_sName, m_sVal, IniFilePath);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[Core_v1][m_cProfile][m_fSet][Exception][{ex.Message}]");
            }
        }
        #endregion

        #region ***用户名
        private static string _loginname;
        public static string loginname
        {
            get
            {
                if (_loginname == null)
                {
                    _loginname = m_cProfile.m_fGet("M", "loginname");
                }
                return _loginname;
            }
            set
            {
                _loginname = value;
                m_cProfile.m_fSet("M", "loginname", value);
            }
        }
        #endregion

        #region ***记住用户名
        private static string _rloginname;
        public static string rloginname
        {
            get
            {
                if (_rloginname == null)
                {
                    _rloginname = m_cProfile.m_fGet("M", "rloginname");
                }
                return _rloginname;
            }
            set
            {
                _rloginname = value;
                m_cProfile.m_fSet("M", "rloginname", value);
            }
        }
        #endregion

        #region ***登陆界面
        private static string _login;
        public static string login
        {
            get
            {
                if (_login == null)
                {
                    _login = m_cProfile.m_fGet("M", "login");
                }
                return _login;
            }
            set
            {
                _login = value;
                m_cProfile.m_fSet("M", "login", value);
            }
        }
        #endregion

        #region ***本地IP
        private static string _localip;
        public static string localip
        {
            get
            {
                if (_localip == null)
                {
                    _localip = m_cProfile.m_fGet("M", "localip");
                }
                return _localip;
            }
            set
            {
                _localip = value;
                m_cProfile.m_fSet("M", "localip", value);
            }
        }
        #endregion

        #region ***服务器IP列表
        private static string _serverip;
        public static string serverip
        {
            get
            {
                if (_serverip == null)
                {
                    _serverip = m_cProfile.m_fGet("M", "serverip");
                }
                return _serverip;
            }
            set
            {
                _serverip = value;
                m_cProfile.m_fSet("M", "serverip", value);
            }
        }
        #endregion

        #region ***域名反查IP
        private static string _realm;
        public static string realm
        {
            get
            {
                if (_realm == null)
                {
                    _realm = m_cProfile.m_fGet("M", "realm");
                }
                return _realm;
            }
            set
            {
                _realm = value;
                m_cProfile.m_fSet("M", "realm", value);
            }
        }
        #endregion

        #region ***处理后的服务器IP列表
        private static string[] _m_lServerIP;
        public static string[] m_lServerIP
        {
            get
            {
                try
                {
                    if (_m_lServerIP == null)
                    {
                        if (!string.IsNullOrWhiteSpace(m_cProfile.serverip))
                        {
                            SortedDictionary<string, string> m_pSortedDictionary = new SortedDictionary<string, string>();
                            string[] m_lString = m_cProfile.serverip.Split(',');
                            foreach (var item in m_lString)
                            {
                                if (!string.IsNullOrWhiteSpace(item) && !m_pSortedDictionary.ContainsKey(item))
                                {
                                    m_pSortedDictionary.Add(item, item);
                                }
                            }
                            _m_lServerIP = m_pSortedDictionary.Select(x => x.Value).ToArray();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[Core_v1][m_cProfile][m_lServerIP][Exception][{ex.Message}]");
                }
                return _m_lServerIP;
            }
        }
        #endregion

        #region ***将不重复的IP地址写入列表并排序
        public static void m_fSetServerIP(string m_sPwd)
        {
            try
            {
                SortedDictionary<string, string> m_pSortedDictionary = new SortedDictionary<string, string>();
                string[] m_lString = m_cProfile.serverip.Split(',');
                foreach (var item in m_lString)
                {
                    if (!string.IsNullOrWhiteSpace(item) && !m_pSortedDictionary.ContainsKey(item))
                    {
                        m_pSortedDictionary.Add(item, item);
                    }
                }
                if (!m_pSortedDictionary.ContainsKey(m_cProfile.server))
                {
                    m_pSortedDictionary.Add(m_cProfile.server, m_cProfile.server);
                }
                m_cProfile.serverip = string.Join(",", m_pSortedDictionary.Select(x => x.Value).ToArray());
                m_cProfile.m_fSet("D", "server", m_cProfile.server);
                m_cProfile.m_fSet("D", "database", m_cProfile.database);
                m_cProfile.m_fSet("D", "uid", m_cProfile.uid);
                m_cProfile.m_fSet("D", "pwd", m_sPwd);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[Core_v1][m_cProfile][m_fSetServerIP][Exception][{ex.Message}]");
            }
        }
        #endregion

        #region ***数据库地址
        private static string _server;
        public static string server
        {
            get
            {
                if (_server == null)
                {
                    _server = m_cProfile.m_fGet("D", "server");
                }
                return _server;
            }
            set
            {
                _server = value;
            }
        }
        #endregion

        #region ***数据库实例名
        private static string _database;
        public static string database
        {
            get
            {
                if (_database == null)
                {
                    _database = m_cProfile.m_fGet("D", "database");
                }
                return _database;
            }
            set
            {
                _database = value;
            }
        }
        #endregion

        #region ***数据库登录名
        private static string _uid;
        public static string uid
        {
            get
            {
                if (_uid == null)
                {
                    _uid = m_cProfile.m_fGet("D", "uid");
                }
                return _uid;
            }
            set
            {
                _uid = value;
            }
        }
        #endregion

        #region ***数据库登陆密码
        private static string _pwd;
        public static string pwd
        {
            get
            {
                if (_pwd == null)
                {
                    _pwd = m_cProfile.m_fGet("D", "pwd");
                }
                return _pwd;
            }
            set
            {
                _pwd = value;
            }
        }
        #endregion

        #region ***替换IP地址
        public static string m_fReplaceIPv4(string m_sString, string m_sIPv4 = null)
        {
            if (m_sIPv4 == null)
            {
                m_sIPv4 = m_cProfile.server;
            }
            return Cmn_v1.Cmn.m_fReplaceIPv4(m_sString, m_sIPv4);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseUtil;
using Core_v1;

namespace CenoCC {
    class ParamInfo {
        public static string RememberUserNameFlag {
            get {
                return m_cProfile.rloginname;
            }
            set {
                m_cProfile.rloginname = value;
            }
        }

        public static string RememberUserName {
            get {
                return m_cProfile.loginname;
            }
            set {
                m_cProfile.loginname = value;
            }
        }


        private static string _SaveRecordPath;
        public static string SaveRecordPath {
            get {
                if(!string.IsNullOrEmpty(_SaveRecordPath))
                    return _SaveRecordPath;
                return _SaveRecordPath = Call_ClientParamUtil.GetParamValueByName("SaveRecordPath");
            }
            set {
                Call_ClientParamUtil.SetParamValueByName("SaveRecordPath", _SaveRecordPath = value);
            }
        }
        private static string _AutoAddNumDialFlag;
        public static string AutoAddNumDialFlag {
            get {
                if(!string.IsNullOrEmpty(_AutoAddNumDialFlag))
                    return _AutoAddNumDialFlag;
                return _AutoAddNumDialFlag = Call_ClientParamUtil.GetParamValueByName("AutoAddNumDialFlag");
            }
            set {
                Call_ClientParamUtil.SetParamValueByName("AutoAddNumDialFlag", _AutoAddNumDialFlag = value);
            }
        }

        private static string _AutoAddNumDial;
        public static string AutoAddNumDial {
            get {
                if(!string.IsNullOrEmpty(_AutoAddNumDial))
                    return _AutoAddNumDial;
                return _AutoAddNumDial = Call_ClientParamUtil.GetParamValueByName("AutoAddNumDial");
            }
            set {
                Call_ClientParamUtil.SetParamValueByName("AutoAddNumDial", _AutoAddNumDial = value);
            }
        }

        private static string _AutoAddNumLoadDialFlag;
        public static string AutoAddNumLoadDialFlag {
            get {
                if(!string.IsNullOrEmpty(_AutoAddNumLoadDialFlag))
                    return _AutoAddNumLoadDialFlag;
                return _AutoAddNumLoadDialFlag = Call_ClientParamUtil.GetParamValueByName("AutoAddNumLoadDialFlag");
            }
            set {
                Call_ClientParamUtil.SetParamValueByName("AutoAddNumLoadDialFlag", _AutoAddNumLoadDialFlag = value);
            }
        }

        private static string _LocalCityCode;
        public static string LocalCityCode {
            get {
                if(!string.IsNullOrEmpty(_LocalCityCode))
                    return _LocalCityCode;
                return _LocalCityCode = Call_ClientParamUtil.GetParamValueByName("LocalCityCode");
            }
            set {
                Call_ClientParamUtil.SetParamValueByName("LocalCityCode", _LocalCityCode = value);
            }
        }
        private static string _IisPath;
        public static string IisPath {
            get {
                if(!string.IsNullOrEmpty(_IisPath))
                    return _IisPath;
                return _IisPath = Call_ClientParamUtil.GetParamValueByName("IisPath");
            }
            set {
                Call_ClientParamUtil.SetParamValueByName("IisPath", _IisPath = value);
            }
        }

        private static string _DiskChkServer;
        public static string DiskChkServer {
            get {
                if(!string.IsNullOrEmpty(_DiskChkServer))
                    return _DiskChkServer;
                return _DiskChkServer = Call_ClientParamUtil.GetParamValueByName("DiskChkServer");
            }
            set {
                Call_ClientParamUtil.SetParamValueByName("DiskChkServer", _DiskChkServer = value);
            }
        }

        private static string _DiskChkLoginName;
        public static string DiskChkLoginName {
            get {
                if(!string.IsNullOrEmpty(_DiskChkLoginName))
                    return _DiskChkLoginName;
                return _DiskChkLoginName = Call_ClientParamUtil.GetParamValueByName("DiskChkLoginName");
            }
            set {
                Call_ClientParamUtil.SetParamValueByName("DiskChkLoginName", _DiskChkLoginName = value);
            }
        }

        private static string _DiskChkPassword;
        public static string DiskChkPassword {
            get {
                if(!string.IsNullOrEmpty(_DiskChkPassword))
                    return _DiskChkPassword;
                return _DiskChkPassword = Call_ClientParamUtil.GetParamValueByName("DiskChkPassword");
            }
            set {
                Call_ClientParamUtil.SetParamValueByName("DiskChkPassword", _DiskChkPassword = value);
            }
        }

        private static string _DiskChkSrc;
        public static string DiskChkSrc {
            get {
                if(!string.IsNullOrEmpty(_DiskChkSrc))
                    return _DiskChkSrc;
                return _DiskChkSrc = Call_ClientParamUtil.GetParamValueByName("DiskChkSrc");
            }
            set {
                Call_ClientParamUtil.SetParamValueByName("DiskChkSrc", _DiskChkSrc = value);
            }
        }

        private static string _DiskChkRemindSize;
        public static string DiskChkRemindSize {
            get {
                if(!string.IsNullOrEmpty(_DiskChkRemindSize))
                    return _DiskChkRemindSize;
                return _DiskChkRemindSize = Call_ClientParamUtil.GetParamValueByName("DiskChkRemindSize");
            }
            set {
                Call_ClientParamUtil.SetParamValueByName("DiskChkRemindSize", _DiskChkRemindSize = value);
            }
        }
    }
}

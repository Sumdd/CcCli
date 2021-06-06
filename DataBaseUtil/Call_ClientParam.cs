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
using System.Data;
using Common;
using Cmn_v1;
using System.Collections.Generic;
using System.Linq;
using Core_v1;

namespace DataBaseUtil {
    /// <summary>
    /// 
    /// </summary>
    public class Call_ClientParamUtil {
        public Call_ClientParamUtil() {
            DateTime initDatetime = new DateTime(1900, 1, 1);
        }

        public static string GetMainUrlAddress() {
            string SqlStr = "select P_Value from Call_Param where P_Name='MainUrl'";
            DataTable dt = MySQL_Method.BindTable(SqlStr);
            if(dt.Rows.Count > 0) {
                return dt.Rows[0]["P_Value"].ToString();
            }
            return "";
        }

        public static string GetParamValueByName(string P_Name) {
            string SqlStr = "select " + P_Name + " from Call_ClientParam a left join Call_Agent b on b.ClientParamID=a.ID where a.ID=" + AgentInfo.AgentID;
            DataTable dt = MySQL_Method.BindTable(SqlStr);
            if(dt.Rows.Count > 0) {
                return dt.Rows[0][P_Name].ToString();
            }
            return "";
        }

        public static bool SetParamValueByName(string P_Name, object P_Value) {
            string SqlStr = "update Call_ClientParam left join Call_Agent on call_clientparam.id = call_agent.ClientParamID set " + P_Name + "='" + P_Value + "' where call_clientparam.ID=" + AgentInfo.AgentID;
            DataTable dt = MySQL_Method.BindTable(SqlStr);
            return dt.Rows.Count > 0;
        }

        public static string GetSaveRecordPath() {
            try {
                string _recordSavePath = Call_ClientParamUtil.GetParamValueByName("SaveRecordPath");
                return Cmn.PathFmt(_recordSavePath);
            } catch(Exception ex) {
                throw new Exception($"获取默认录音保存路径失败,原因:{ex.Message}");
            }
        }

        #region 未接来电配置
        private static int? _noAnswerDay;
        private static bool? _noAnswerUse;
        public static int noAnswerDay {
            get {
                if(_noAnswerDay == null) {
                    string _noAnswerConf = Call_ClientParamUtil.GetParamValueByName("NoAnswerConf");
                    if(!string.IsNullOrWhiteSpace(_noAnswerConf) && _noAnswerConf.Contains(",")) {
                        var s_str = _noAnswerConf.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        Call_ClientParamUtil.noAnswerDay = Convert.ToInt32(s_str[0]);
                        Call_ClientParamUtil.noAnswerUse = Convert.ToBoolean(s_str[1]);
                    } else {
                        Call_ClientParamUtil.noAnswerDay = 2;
                        Call_ClientParamUtil.noAnswerUse = true;
                    }
                }
                return Convert.ToInt32(_noAnswerDay);
            }
            set {
                _noAnswerDay = value;
            }
        }
        public static bool noAnswerUse {
            get {
                if(_noAnswerUse == null) {
                    string _noAnswerConf = Call_ClientParamUtil.GetParamValueByName("NoAnswerConf");
                    if(!string.IsNullOrWhiteSpace(_noAnswerConf) && _noAnswerConf.Contains(",")) {
                        var s_str = _noAnswerConf.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        Call_ClientParamUtil.noAnswerDay = Convert.ToInt32(s_str[0]);
                        Call_ClientParamUtil.noAnswerUse = Convert.ToBoolean(s_str[1]);
                    } else {
                        Call_ClientParamUtil.noAnswerDay = 2;
                        Call_ClientParamUtil.noAnswerUse = true;
                    }
                }
                return Convert.ToBoolean(_noAnswerUse);
            }
            set {
                _noAnswerUse = value;
            }
        }
        #endregion

        #region 是否联网
        private static bool? _IsLinkNet;
        public static bool IsLinkNet
        {
            get
            {
                try
                {
                    if (_IsLinkNet != null)
                    {
                        return Convert.ToBoolean(_IsLinkNet);
                    }
                    var _value = GetParamValueByName("IsLinkNet");
                    if (!string.IsNullOrWhiteSpace(_value))
                    {
                        _IsLinkNet = _value == "1";
                    }
                    else
                    {
                        _IsLinkNet = false;
                    }
                    return Convert.ToBoolean(_IsLinkNet);
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                _IsLinkNet = value;
                SetParamValueByName("IsLinkNet", value ? 1 : 0);
            }
        }
        #endregion

        #region 是否自动添加拨号标识
        public static bool? _m_bAutoAddNumDialFlag;
        public static bool m_bAutoAddNumDialFlag
        {
            get
            {
                try
                {
                    if (_m_bAutoAddNumDialFlag != null)
                    {
                        return Convert.ToBoolean(_m_bAutoAddNumDialFlag);
                    }
                    var _value = GetParamValueByName("AutoAddNumDialFlag");
                    if (!string.IsNullOrWhiteSpace(_value))
                    {
                        _m_bAutoAddNumDialFlag = _value == "1";
                    }
                    else
                    {
                        _m_bAutoAddNumDialFlag = false;
                    }
                    return Convert.ToBoolean(_m_bAutoAddNumDialFlag);
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                SetParamValueByName("AutoAddNumDialFlag", value ? 1 : 0);
                _m_bAutoAddNumDialFlag = value;
            }
        }
        #endregion

        #region 来电显示样式
        /// <summary>
        /// 默认不显示联系人姓名
        /// </summary>
        public static bool m_bName = false;

        private static string m_sShowStyleString;

        public static string ShowStyleString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(m_sShowStyleString))
                {
                    m_sShowStyleString = GetParamValueByName("ShowStyle");
                }
                return m_sShowStyleString;
            }
            set
            {
                m_sShowStyleString = value;
                SetParamValueByName("ShowStyle", m_sShowStyleString);
            }
        }
        #endregion

        #region ***录音格式配置

        public static bool m_bSwitch;
        public static string m_sSwitch;
        public static List<string> m_lSwitch = new List<string>() { "" };

        private static string _m_sRecSetting;

        public static string m_sRecSetting
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_m_sRecSetting))
                {
                    _m_sRecSetting = GetParamValueByName("RecSetting");
                }
                return _m_sRecSetting;
            }
            set
            {
                _m_sRecSetting = value;
                SetParamValueByName("RecSetting", _m_sRecSetting);
            }
        }

        private static bool m_bRecSetting = false;
        public static void m_fRecSetting()
        {
            try
            {
                if (!Call_ClientParamUtil.m_bRecSetting)
                {
                    Call_ClientParamUtil.m_bRecSetting = true;
                    string[] m_lRecSetting = Call_ClientParamUtil.m_sRecSetting.Split(',');
                    Call_ClientParamUtil.m_bSwitch = (m_lRecSetting[0] == "1");
                    Call_ClientParamUtil.m_sSwitch = m_lRecSetting[1];
                    foreach (var item in m_lRecSetting[2].Split('|'))
                    {
                        if (!string.IsNullOrWhiteSpace(item) && !m_lSwitch.Contains(item, new Eq()))
                        {
                            Call_ClientParamUtil.m_lSwitch.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[CenoCC][Args_Record][Args_Record][Exception][{ex.Message}]");
                Call_ClientParamUtil.m_bSwitch = false;
                Call_ClientParamUtil.m_sSwitch = "";
                Call_ClientParamUtil.m_lSwitch = new List<string>() { "", ".mp3", ".wav", ".wma", ".amr" };
            }
        }

        private class Eq : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return x.Equals(y, StringComparison.OrdinalIgnoreCase);
            }

            public int GetHashCode(string obj)
            {
                return base.GetHashCode();
            }
        }
        #endregion

        #region ***下载线程
        public static decimal? _m_uHttpLoadThread;
        public static decimal m_uHttpLoadThread
        {
            get
            {
                try
                {
                    if (_m_uHttpLoadThread == null)
                    {
                        _m_uHttpLoadThread = Convert.ToDecimal(Call_ClientParamUtil.GetParamValueByName("_m_uHttpLoadThread".Replace("_m_u", "").Replace("m_u", "")));
                    }
                    return Convert.ToInt32(_m_uHttpLoadThread);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[DataBaseUtil][Call_ClientParamUtil][m_uHttpLoadThread][Exception][{ex.Message}]");
                    return 1;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("_m_uHttpLoadThread".Replace("_m_u", "").Replace("m_u", ""), Convert.ToDecimal(value));
                _m_uHttpLoadThread = value;
            }
        }
        #endregion

        #region ***是否启用复制,默认开启
        public static bool? _m_bIsUseCopy;
        public static bool m_bIsUseCopy
        {
            get
            {
                try
                {
                    if (_m_bIsUseCopy == null)
                    {
                        _m_bIsUseCopy = Call_ClientParamUtil.GetParamValueByName("_m_bIsUseCopy".Replace("_m_b", "").Replace("m_b", "")) == "1";
                    }
                    return Convert.ToBoolean(_m_bIsUseCopy);
                }
                catch (Exception ex)
                {
                    return true;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("_m_bIsUseCopy".Replace("_m_b", "").Replace("m_b", ""), value ? "1" : "0");
                _m_bIsUseCopy = value;
            }
        }
        #endregion

        #region ***是否使用共享号码
        public static bool? _m_bIsUseShare;
        public static bool m_bIsUseShare
        {
            get
            {
                try
                {
                    if (_m_bIsUseShare == null)
                    {
                        _m_bIsUseShare = Call_ClientParamUtil.GetParamValueByName("_m_bIsUseShare".Replace("_m_b", "").Replace("m_b", "")) == "1";
                    }
                    return Convert.ToBoolean(_m_bIsUseShare);
                }
                catch (Exception ex)
                {
                    return true;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("_m_bIsUseShare".Replace("_m_b", "").Replace("m_b", ""), value ? "1" : "0");
                _m_bIsUseShare = value;
            }
        }
        #endregion

        #region ***号码池等待时长(秒),0表示不关闭
        private static int? _m_uShareWait;

        public static int m_uShareWait
        {
            get
            {
                try
                {
                    if (_m_uShareWait == null)
                    {
                        _m_uShareWait = Convert.ToInt32(Call_ClientParamUtil.GetParamValueByName("ShareWait"));
                        if (_m_uShareWait != 0 && _m_uShareWait < 5)
                            _m_uShareWait = 5;
                    }
                    return Convert.ToInt32(_m_uShareWait);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("ShareWait", value);
                _m_uShareWait = value;
            }
        }
        #endregion

        #region ***是否启用专线轮呼(当不使用共享号码生效)
        public static bool? _m_bIsUseSpRandom;
        public static bool m_bIsUseSpRandom
        {
            get
            {
                try
                {
                    if (_m_bIsUseSpRandom == null)
                    {
                        _m_bIsUseSpRandom = Call_ClientParamUtil.GetParamValueByName("_m_bIsUseSpRandom".Replace("_m_b", "").Replace("m_b", "")) == "1";
                    }
                    return Convert.ToBoolean(_m_bIsUseSpRandom);
                }
                catch (Exception ex)
                {
                    return true;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("_m_bIsUseSpRandom".Replace("_m_b", "").Replace("m_b", ""), value ? "1" : "0");
                _m_bIsUseSpRandom = value;
            }
        }
        #endregion

        #region ***是否启用超时后自动专线轮呼
        public static bool? _m_bIsUseSpRandomTimeout;
        public static bool m_bIsUseSpRandomTimeout
        {
            get
            {
                try
                {
                    if (_m_bIsUseSpRandomTimeout == null)
                    {
                        _m_bIsUseSpRandomTimeout = Call_ClientParamUtil.GetParamValueByName("_m_bIsUseSpRandomTimeout".Replace("_m_b", "").Replace("m_b", "")) == "1";
                    }
                    return Convert.ToBoolean(_m_bIsUseSpRandomTimeout);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("_m_bIsUseSpRandomTimeout".Replace("_m_b", "").Replace("m_b", ""), value ? "1" : "0");
                _m_bIsUseSpRandomTimeout = value;
            }
        }
        #endregion

        #region ***是否复制后加载号码池下拉
        public static bool? _m_bIsUseCopyNumber;
        public static bool m_bIsUseCopyNumber
        {
            get
            {
                try
                {
                    if (_m_bIsUseCopyNumber == null)
                    {
                        _m_bIsUseCopyNumber = Call_ClientParamUtil.GetParamValueByName("_m_bIsUseCopyNumber".Replace("_m_b", "").Replace("m_b", "")) == "1";
                    }
                    return Convert.ToBoolean(_m_bIsUseCopyNumber);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("_m_bIsUseCopyNumber".Replace("_m_b", "").Replace("m_b", ""), value ? "1" : "0");
                _m_bIsUseCopyNumber = value;
            }
        }
        #endregion

        #region ***兼容加密号码
        public static bool? _m_bQNRegexNumber;
        public static bool m_bQNRegexNumber
        {
            get
            {
                try
                {
                    if (_m_bQNRegexNumber == null)
                    {
                        _m_bQNRegexNumber = Call_ClientParamUtil.GetParamValueByName("_m_bQNRegexNumber".Replace("_m_b", "").Replace("m_b", "")) == "1";
                    }
                    return _m_bQNRegexNumber.Value;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("_m_bQNRegexNumber".Replace("_m_b", "").Replace("m_b", ""), value ? "1" : "0");
                _m_bQNRegexNumber = value;
            }
        }
        #endregion

        #region ***是否开启追加独立服务中的共享号码,申请式
        public static bool? _m_bUseApply;
        public static bool m_bUseApply
        {
            get
            {
                try
                {
                    if (_m_bUseApply == null)
                    {
                        _m_bUseApply = Call_ClientParamUtil.GetParamValueByName("_m_bUseApply".Replace("_m_b", "").Replace("m_b", "")) == "1";
                    }
                    return Convert.ToBoolean(_m_bUseApply);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("_m_bUseApply".Replace("_m_b", "").Replace("m_b", ""), value ? "1" : "0");
                _m_bUseApply = value;
            }
        }
        #endregion

        #region ***系统通知来电
        public static bool? _m_bIsSysMsgCall;
        public static bool m_bIsSysMsgCall
        {
            get
            {
                try
                {
                    if (_m_bIsSysMsgCall == null)
                    {
                        _m_bIsSysMsgCall = Call_ClientParamUtil.GetParamValueByName("_m_bIsSysMsgCall".Replace("_m_b", "").Replace("m_b", "")) == "1";
                    }
                    return Convert.ToBoolean(_m_bIsSysMsgCall);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("_m_bIsSysMsgCall".Replace("_m_b", "").Replace("m_b", ""), value ? "1" : "0");
                _m_bIsSysMsgCall = value;
            }
        }
        #endregion

        #region ***是否开启呼叫转移
        public static bool? _m_bisinlimit_2;
        public static bool m_bisinlimit_2
        {
            get
            {
                try
                {
                    if (_m_bisinlimit_2 == null)
                    {
                        _m_bisinlimit_2 = Call_ClientParamUtil.GetParamValueByName("_m_bisinlimit_2".Replace("_m_b", "").Replace("m_b", "")) == "1";
                    }
                    return _m_bisinlimit_2.Value;
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("_m_bisinlimit_2".Replace("_m_b", "").Replace("m_b", ""), value ? "1" : "0");
                _m_bisinlimit_2 = value;
            }
        }
        #endregion

        #region ***呼叫转移号码
        public static string _m_sinlimit_2number;

        public static string m_sinlimit_2number
        {
            get
            {
                try
                {
                    if (_m_sinlimit_2number == null)
                    {
                        _m_sinlimit_2number = Call_ClientParamUtil.GetParamValueByName("inlimit_2number");
                    }
                    return _m_sinlimit_2number;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("inlimit_2number", value);
                _m_sinlimit_2number = value;
            }
        }
        #endregion

        #region ***呼叫转移开始时间
        public static string _m_sinlimit_2starttime;

        public static string m_sinlimit_2starttime
        {
            get
            {
                try
                {
                    if (_m_sinlimit_2starttime == null)
                    {
                        _m_sinlimit_2starttime = Call_ClientParamUtil.GetParamValueByName("inlimit_2starttime");
                    }
                    return _m_sinlimit_2starttime;
                }
                catch
                {
                    return "19:00:00";
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("inlimit_2starttime", value);
                _m_sinlimit_2starttime = value;
            }
        }
        #endregion

        #region ***呼叫转移结束时间
        public static string _m_sinlimit_2endtime;

        public static string m_sinlimit_2endtime
        {
            get
            {
                try
                {
                    if (_m_sinlimit_2endtime == null)
                    {
                        _m_sinlimit_2endtime = Call_ClientParamUtil.GetParamValueByName("inlimit_2endtime");
                    }
                    return _m_sinlimit_2endtime;
                }
                catch
                {
                    return "08:00:00";
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("inlimit_2endtime", value);
                _m_sinlimit_2endtime = value;
            }
        }
        #endregion

        #region ***呼叫转移星期
        public static string _m_sinlimit_2whatday;

        public static int m_uinlimit_2whatday
        {
            get
            {
                try
                {
                    if (_m_sinlimit_2whatday == null)
                    {
                        _m_sinlimit_2whatday = Call_ClientParamUtil.GetParamValueByName("inlimit_2whatday");
                    }
                    return int.Parse(_m_sinlimit_2whatday);
                }
                catch
                {
                    return 127;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("inlimit_2whatday", value);
                _m_sinlimit_2whatday = value.ToString();
            }
        }
        #endregion

        #region ***是否开启一键拨号自动接听
        public static bool? _m_bApiAutoAccept;
        public static bool m_bApiAutoAccept
        {
            get
            {
                try
                {
                    if (_m_bApiAutoAccept == null)
                    {
                        _m_bApiAutoAccept = Call_ClientParamUtil.GetParamValueByName("_m_bApiAutoAccept".Replace("_m_b", "").Replace("m_b", "")) == "1";
                    }
                    return _m_bApiAutoAccept.Value;
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                Call_ClientParamUtil.SetParamValueByName("_m_bApiAutoAccept".Replace("_m_b", "").Replace("m_b", ""), value ? "1" : "0");
                _m_bApiAutoAccept = value;
            }
        }
        #endregion
    }
}

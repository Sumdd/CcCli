///////////////////////////////////////////////////////////////////////////////////////
// 文件名   : C:\Users\zhongguan\Desktop\11111\NhibernateBag\Models\Call_Param.cs
// 类名     : Call_Param
// 中文名   : 
// 创建描述 : 
// 创建人   : 
// 创建时间 : 2015-11-10 16:54:04
// 版权信息 : 青岛天路信息技术有限责任公司  www.topdigi.com.cn
///////////////////////////////////////////////////////////////////////////////////////

using Cmn_v1;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace DataBaseUtil {
    /// <summary>
    /// 
    /// </summary>

    public class Call_ParamUtil {
        public Call_ParamUtil() {
            DateTime initDatetime = new DateTime(1900, 1, 1);
        }

        public static string GetParamValueByName(string P_Name, string defaultValue = "", string m_sConnStr = null) {
            try {
                string SqlStr = "select P_Value from Call_Param where P_Name='" + P_Name + "'";
                DataTable dt = MySQL_Method.BindTable(SqlStr, m_sConnStr);
                if(dt.Rows.Count > 0) {

                    //替换对象
                    List<string> m_lString = new List<string>();
                    m_lString.Add("InWebSocket");//内
                    m_lString.Add("OutWebSocket");//外
                    m_lString.Add("SystemUpgradelPath");//更新
                    m_lString.Add("RedisConfig");//Redis
                    m_lString.Add("DialTaskRecDownLoadHTTP");//下载地址
                    m_lString.Add("HttpShareUrl");//共享
                    string m_sValue = dt.Rows[0]["P_Value"].ToString();
                    //替换逻辑
                    if (m_lString.Contains(P_Name, StringComparer.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(m_sConnStr))
                    {
                        string _m_sValue = Core_v1.m_cProfile.m_fReplaceIPv4(m_sValue);
                        if (string.Equals(P_Name, "RedisConfig", StringComparison.OrdinalIgnoreCase))
                            Core_v1.Log.Instance.Warn($"[DataBaseUtil][Call_ParamUtil][GetParamValueByName][置换IPv4 {P_Name}]");
                        else
                            Core_v1.Log.Instance.Warn($"[DataBaseUtil][Call_ParamUtil][GetParamValueByName][置换IPv4 {P_Name}:{m_sValue} -> {_m_sValue}]");
                        return _m_sValue;
                    }
                    return m_sValue;

                } else {
                    return defaultValue;

                }
            } catch {
                return defaultValue;
            }
        }

        public static bool Update(string P_Name, object P_Value) {
            try {
                if(MySQL_Method.ExecuteNonQuery("update Call_Param set P_Value='" + P_Value + "' where P_Name='" + P_Name + "'") > 0)
                    return true;
                return false;
            } catch {
                return false;
            }
        }

        private static string _SaveRecordPath;
        public static string SaveRecordPath {
            get {
                if(string.IsNullOrWhiteSpace(_SaveRecordPath))
                    _SaveRecordPath = Cmn.PathFmt(Call_ParamUtil.GetParamValueByName("RecordFilePath"));
                return _SaveRecordPath;
            }
            set {
                _SaveRecordPath = value;
            }
        }
        /// <summary>
        /// 获取真实路径
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static string ReplacePath(string _path) {
            if (!string.IsNullOrWhiteSpace(m_sDialTaskRecPath))
            {
                return Cmn.PathFmt(_path, "/").Replace(m_sDialTaskRecPath, "");
            }
            else
            {
                return Cmn.PathFmt(_path).Replace(SaveRecordPath, "");
            }
        }

        #region 获取Stun服务器地址
        public static string GetStunServer {
            get {
                try {
                    return Call_ParamUtil.GetParamValueByName("StunServer");
                } catch(Exception) {
                    return "stun.freeswitch.org";
                }
            }
        }
        #endregion
        #region 获取Stun服务器端口
        public static int GetStunPort {
            get {
                try {
                    return Convert.ToInt32(Call_ParamUtil.GetParamValueByName("StunPort"));
                } catch(Exception) {
                    return 3478;
                }
            }
        }
        #endregion
        #region 获取Nat设置
        public static string GetNat {
            get {
                try {
                    return Call_ParamUtil.GetParamValueByName("Nat");
                } catch(Exception) {
                    return "no_nat";
                }
            }
        }
        #endregion
        #region 客户端更新地址
        public static string SystemUpgradelPath {
            get {
                return Call_ParamUtil.GetParamValueByName("SystemUpgradelPath");
            }
        }
        #endregion
        #region 拨号号码简易处理
        private static string _DialDealMethod;
        [Obsolete("该变量尽量不再使用,统一处理所有电话,如果有多个网关,这里就需要判断了,该字段就不行了")]
        public static string DialDealMethod {
            get {
                if(string.IsNullOrWhiteSpace(_DialDealMethod))
                    _DialDealMethod = Call_ParamUtil.GetParamValueByName("DialDealMethod", "no");
                return _DialDealMethod;
            }
            set {
                Call_ParamUtil.Update("DialDealMethod", value);
                _DialDealMethod = value;
            }
        }
        #endregion
        #region 催收系统数据库连接字符串
        private static string m_sHomeConnString;
        public static string HomeConnString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(m_sHomeConnString))
                {
                    m_sHomeConnString = GetParamValueByName("HomeConnString");
                }
                return m_sHomeConnString;
            }
            set
            {
                m_sHomeConnString = value;
                Update("HomeConnString", m_sHomeConnString);
            }
        }
        #endregion
        #region 查询联系人名称语句
        private static string m_sHomeSelectString;
        public static string HomeSelectString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(m_sHomeSelectString))
                {
                    m_sHomeSelectString = GetParamValueByName("HomeSelectString");
                }
                return m_sHomeSelectString;
            }
            set
            {
                m_sHomeSelectString = value;
                Update("HomeSelectString", m_sHomeSelectString);
            }
        }
        #endregion
        #region 是否使用催收查询联系人姓名
        public static string _m_sUseHomeSearch;

        public static bool m_bUseHomeSearch
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_m_sUseHomeSearch))
                {
                    _m_sUseHomeSearch = GetParamValueByName("UseHomeSearch");
                }
                return _m_sUseHomeSearch == "1";
            }
            set
            {
                _m_sUseHomeSearch = value ? "1" : "0";
                Update("UseHomeSearch", _m_sUseHomeSearch);
            }
        }
        #endregion
        #region 自动拨号录音路径分离配置
        private static string _m_sDialTaskRecPath;
        public static string m_sDialTaskRecPath
        {
            get
            {
                try
                {
                    if (_m_sDialTaskRecPath == null)
                    {
                        _m_sDialTaskRecPath = Call_ParamUtil.GetParamValueByName("_m_sDialTaskRecPath".Replace("_m_s", "").Replace("m_s", ""));
                    }
                    return _m_sDialTaskRecPath;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    Call_ParamUtil.Update("_m_sDialTaskRecPath".Replace("_m_s", "").Replace("m_s", ""), value);
                    _m_sDialTaskRecPath = value;
                }
                catch (Exception ex)
                { }
            }
        }
        #endregion
        #region 使用HTTP下载录音,替换FTP
        private static string _m_sDialTaskRecDownLoadHTTP;
        public static string m_sDialTaskRecDownLoadHTTP
        {
            get
            {
                try
                {
                    if (_m_sDialTaskRecDownLoadHTTP == null)
                    {
                        _m_sDialTaskRecDownLoadHTTP = Call_ParamUtil.GetParamValueByName("_m_sDialTaskRecDownLoadHTTP".Replace("_m_s", "").Replace("m_s", ""));
                    }
                    return _m_sDialTaskRecDownLoadHTTP;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    Call_ParamUtil.Update("_m_sDialTaskRecDownLoadHTTP".Replace("_m_s", "").Replace("m_s", ""), value);
                    _m_sDialTaskRecDownLoadHTTP = value;
                }
                catch (Exception ex)
                { }
            }
        }
        #endregion
        #region 登陆界面
        private static string _m_sLoginType;
        public static string m_sLoginType
        {
            get
            {
                try
                {
                    if (_m_sLoginType == null)
                    {
                        _m_sLoginType = Call_ParamUtil.GetParamValueByName("_m_sLoginType".Replace("_m_s", "").Replace("m_s", ""));
                    }
                    return _m_sLoginType;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    Call_ParamUtil.Update("_m_sLoginType".Replace("_m_s", "").Replace("m_s", ""), value);
                    _m_sLoginType = value;
                }
                catch (Exception ex)
                { }
            }
        }
        #endregion
        #region dtmf发送方式

        public const string inbound = "inbound";
        //public const string rfc2833 = "rfc2833";
        public const string clientSignal = "clientSignal";
        public const string bothSignal = "bothSignal";

        private static string _m_sDTMFSendMethod;
        public static string m_sDTMFSendMethod
        {
            get
            {
                string m_sValue = clientSignal;
                try
                {
                    if (_m_sDTMFSendMethod == null)
                    {
                        _m_sDTMFSendMethod = Call_ParamUtil.GetParamValueByName("_m_sDTMFSendMethod".Replace("_m_s", "").Replace("m_s", ""), m_sValue);
                    }
                    return _m_sDTMFSendMethod;
                }
                catch (Exception ex)
                {
                    return m_sValue;
                }
            }
            set
            {
                try
                {
                    Call_ParamUtil.Update("_m_sDTMFSendMethod".Replace("_m_s", "").Replace("m_s", ""), value);
                    _m_sDTMFSendMethod = value;
                }
                catch (Exception ex)
                { }
            }
        }
        #endregion
        #region ***默认录音扩展名称
        private static string _m_srec_t;
        public static string m_srec_t
        {
            get
            {
                try
                {
                    if (_m_srec_t == null)
                    {
                        _m_srec_t = Call_ParamUtil.GetParamValueByName("_m_srec_t".Replace("_m_s", "").Replace("m_s", ""));
                    }
                    return _m_srec_t;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    Call_ParamUtil.Update("_m_srec_t".Replace("_m_s", "").Replace("m_s", ""), value);
                    _m_srec_t = value;
                }
                catch (Exception ex)
                { }
            }
        }
        #endregion
        #region ***是否启用HasRedis
        public static bool? _m_bIsHasRedis;
        public static bool m_bIsHasRedis
        {
            get
            {
                try
                {
                    if (_m_bIsHasRedis == null)
                    {
                        _m_bIsHasRedis = Call_ParamUtil.GetParamValueByName("IsHasRedis") == "1";
                    }
                    return Convert.ToBoolean(_m_bIsHasRedis);
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    Call_ParamUtil.Update("IsHasRedis", value ? "1" : "0");
                    _m_bIsHasRedis = value;
                }
                catch { }
            }
        }
        #endregion
        #region 共享号码配置:0.无;1.主录音服务器;2.副录音服务器;
        public static int? _m_uShareNumSetting;
        public static int m_uShareNumSetting
        {
            get
            {
                try
                {
                    if (_m_uShareNumSetting == null)
                    {
                        Call_ParamUtil._m_uShareNumSetting = Convert.ToInt32(Call_ParamUtil.GetParamValueByName("_m_uShareNumSetting".Replace("_m_u", "").Replace("m_u", "")));
                    }
                    return Convert.ToInt32(_m_uShareNumSetting);
                }
                catch
                {
                    return Convert.ToInt32(_m_uShareNumSetting = 0);
                }
            }
            set
            {
                try
                {
                    Call_ParamUtil.Update("_m_uShareNumSetting".Replace("_m_u", "").Replace("m_u", ""), value.ToString());
                    _m_uShareNumSetting = value;
                }
                catch { }
            }
        }
        #endregion
        #region ***共享文件夹HTTP
        public static bool? _m_bIsUseHttpShare;
        public static bool m_bIsUseHttpShare
        {
            get
            {
                try
                {
                    if (_m_bIsUseHttpShare == null)
                    {
                        _m_bIsUseHttpShare = Call_ParamUtil.GetParamValueByName("_m_bIsUseHttpShare".Replace("_m_b", "").Replace("m_b", "")) == "1";
                    }
                    return Convert.ToBoolean(_m_bIsUseHttpShare);
                }
                catch (Exception ex)
                {
                    Core_v1.Log.Instance.Error($"[DB.Basic][Call_ParamUtil][m_bIsUseHttpShare][get][Exception][{ex.Message}]");
                    return false;
                }
            }
            set
            {
                try
                {
                    Call_ParamUtil.Update("_m_bIsUseHttpShare".Replace("_m_b", "").Replace("m_b", ""), value ? "1" : "0");
                    _m_bIsUseHttpShare = value;
                }
                catch (Exception ex)
                {
                    Core_v1.Log.Instance.Error($"[DB.Basic][Call_ParamUtil][m_bIsUseHttpShare][set][Exception][{ex.Message}]");
                }
            }
        }
        #endregion
        #region ***共享文件夹HTTP路径
        public static string _m_sHttpShareUrl;
        public static string m_sHttpShareUrl
        {
            get
            {
                try
                {
                    if (_m_sHttpShareUrl == null)
                    {
                        _m_sHttpShareUrl = Call_ParamUtil.GetParamValueByName("_m_sHttpShareUrl".Replace("_m_s", "").Replace("m_s", ""));
                    }
                    return _m_sHttpShareUrl;
                }
                catch (Exception ex)
                {
                    Core_v1.Log.Instance.Error($"[DB.Basic][Call_ParamUtil][m_sHttpShareUrl][get][Exception][{ex.Message}]");
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    Call_ParamUtil.Update("_m_sHttpShareUrl".Replace("_m_s", "").Replace("m_s", ""), value);
                    _m_sHttpShareUrl = value;
                }
                catch (Exception ex)
                {
                    Core_v1.Log.Instance.Error($"[DB.Basic][Call_ParamUtil][m_sHttpShareUrl][set][Exception][{ex.Message}]");
                }
            }
        }
        #endregion
        #region ***FreeSwitch所在目录
        private static string _m_sFreeSWITCHPath;
        public static string m_sFreeSWITCHPath
        {
            get
            {
                try
                {
                    if (_m_sFreeSWITCHPath == null)
                    {
                        Call_ParamUtil._m_sFreeSWITCHPath = Call_ParamUtil.GetParamValueByName("_m_sFreeSWITCHPath".Replace("_m_s", "").Replace("m_s", ""));
                    }
                    return _m_sFreeSWITCHPath;
                }
                catch
                {
                    return _m_sFreeSWITCHPath = "C:/Program Files/FreeSWITCH";
                }
            }
            set
            {
                try
                {
                    Call_ParamUtil.Update("_m_sFreeSWITCHPath".Replace("_m_s", "").Replace("m_s", ""), value);
                    _m_sFreeSWITCHPath = value;
                }
                catch { }
            }
        }
        #endregion
        #region ***FreeSwitch网关写入Ua文件夹名
        private static string _m_sFreeSWITCHUaPath;
        public static string m_sFreeSWITCHUaPath
        {
            get
            {
                try
                {
                    if (_m_sFreeSWITCHUaPath == null)
                    {
                        Call_ParamUtil._m_sFreeSWITCHUaPath = Call_ParamUtil.GetParamValueByName("_m_sFreeSWITCHUaPath".Replace("_m_s", "").Replace("m_s", ""));
                    }
                    return _m_sFreeSWITCHUaPath;
                }
                catch
                {
                    return _m_sFreeSWITCHUaPath = "external";
                }
            }
            set
            {
                try
                {
                    Call_ParamUtil.Update("_m_sFreeSWITCHUaPath".Replace("_m_s", "").Replace("m_s", ""), value);
                    _m_sFreeSWITCHUaPath = value;
                }
                catch { }
            }
        }
        #endregion
        #region ***转码后最终扩展名
        private static string _m_sEndExt;
        public static string m_sEndExt
        {
            get
            {
                try
                {
                    if (_m_sEndExt == null)
                    {
                        Call_ParamUtil._m_sEndExt = Call_ParamUtil.GetParamValueByName("_m_sEndExt".Replace("_m_s", "").Replace("m_s", ""));
                    }
                    return _m_sEndExt;
                }
                catch
                {
                    return _m_sEndExt = "";
                }
            }
            set
            {
                try
                {
                    Call_ParamUtil.Update("_m_sEndExt".Replace("_m_s", "").Replace("m_s", ""), value);
                    _m_sEndExt = value;
                }
                catch { }
            }
        }
        #endregion
        #region ***客户端是否打印WebSocket Debug
        private static bool? _m_bWsDebug;
        public static bool m_bWsDebug
        {
            get
            {
                if (_m_bWsDebug == null)
                {
                    _m_bWsDebug = false;
                }
                return _m_bWsDebug.Value;
            }
            set
            {
                _m_bWsDebug = value;
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
                        _m_bUseApply = Call_ParamUtil.GetParamValueByName("UseApply") == "1";
                    }
                    return Convert.ToBoolean(_m_bUseApply);
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    Call_ParamUtil.Update("UseApply", value ? "1" : "0");
                    _m_bUseApply = value;
                }
                catch { }
            }
        }
        #endregion
        #region ***B Leg 超时时间
        private static int? _m_uBLegTimeout;
        public static int m_uBLegTimeout
        {
            get
            {
                try
                {
                    if (_m_uBLegTimeout == null)
                    {
                        _m_uBLegTimeout = int.Parse(GetParamValueByName("__timeout_seconds", "120"));
                    }
                    return _m_uBLegTimeout.Value;
                }
                catch
                {
                    return 120;
                }
            }
        }
        #endregion
    }
}

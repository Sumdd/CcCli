using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Model_v1;
using Newtonsoft.Json;

namespace Core_v1
{
    public class Redis2
    {
        public static dial_area m_EsyDialArea;
        public static dial_area m_EsyMainDialArea;

        public static bool use = false;
        public static string host = "127.0.0.1";
        public const string defaultHost = "127.0.0.1";
        public static int port = 6379;
        public const int defaultPort = 6379;
        public static string password = null;
        public static int db = 15;
        public const int defaultDb = 15;

        /// <summary>
        /// 共享号码JSON单条
        /// </summary>
        public const string m_sJSONPrefix = "SHARE-JSON-SIMPLE-DATA";
        /// <summary>
        /// 共享号码JSON单条锁
        /// </summary>
        public const string m_sLockPrefix = "SHARE-JSON-SIMPLE-LOCK";
        /// <summary>
        /// 号码域名称
        /// </summary>
        public const string m_sDialAreaName = "SHARE-DIAL-AREA";

        //读数据(无锁)
        public static List<share_number> m_fGetShareNumberList(string m_sLoginName)
        {
            try
            {
                if (Redis2.use)
                {
                    ///将其写到接口获取,方便调整    
                    string m_sResultString = H_Web.Get(m_cProfile.m_fGetShare1(m_sLoginName));

                    ///解析
                    if (!string.IsNullOrWhiteSpace(m_sResultString))
                    {
                        m_mResponseJSON _m_mResponseJSON = JsonConvert.DeserializeObject<m_mResponseJSON>(m_sResultString);
                        if (_m_mResponseJSON.status == 0)
                        {
                            ///返回结果
                            return JsonConvert.DeserializeObject<List<share_number>>(_m_mResponseJSON.result.ToString());
                        }
                        else
                        {
                            Log.Instance.Warn($"[Core_v1][Redis2][m_fGetShareNumberList][{_m_mResponseJSON.msg}:{_m_mResponseJSON.result}]");
                        }
                    }
                    else
                    {
                        Log.Instance.Warn($"[Core_v1][Redis2][m_fGetShareNumberList][无返回]");
                    }
                }
                else
                {
                    Log.Instance.Warn($"[Core_v1][Redis2][m_fGetShareNumberList][not use redis]");
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[Core_v1][Redis2][m_fGetShareNumberList][Exception][{ex.Message}]");
                Log.Instance.Debug(ex);
            }
            return null;
        }
    }
}
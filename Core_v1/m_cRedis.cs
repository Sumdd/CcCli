using ServiceStack.Redis;
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

        #region ***Redis多例模式
        public static RedisClient Instance
        {
            get
            {
                try
                {
                    return new RedisClient(host, port, password, db);
                }
                catch (Exception ex)
                {
                    Log.Instance.Fail($"[Core_v1][Redis2][Instance][get][{ex.Message}]");
                }
                return null;
            }
        }
        #endregion

        //读数据(无锁)
        public static List<share_number> m_fGetShareNumberList()
        {
            try
            {
                if (Redis2.use)
                {
                    ///有无配置本机域
                    if (Redis2.m_EsyDialArea == null)
                    {
                        Log.Instance.Warn($"[Core_v1][Redis2][m_fGetShareNumberList][not find my area in mysql]");
                        return null;
                    }

                    ///是否已加入域
                    List<dial_area> m_lDialArea = JsonConvert.DeserializeObject<List<dial_area>>(Redis2.Instance.Get<string>(Redis2.m_sDialAreaName));
                    var m_uCount = m_lDialArea.Where(x => x.aip == Redis2.m_EsyDialArea?.aip && (x.astate == 2 || x.astate == 4))?.Count();
                    if (m_uCount <= 0)
                    {
                        Log.Instance.Warn($"[Core_v1][Redis2][m_fGetShareNumberList][no find my area in redis]");
                        return null;
                    }

                    string[] m_lDataKeys = Redis2.Instance.GetAllKeys().Where(x => x.StartsWith(m_sJSONPrefix))?.ToArray();
                    string[] m_lLockKeys = Redis2.Instance.GetAllKeys().Where(x => x.StartsWith(m_sLockPrefix))?.ToArray();
                    if (m_lLockKeys == null) m_lLockKeys = new string[0];
                    if (m_lDataKeys != null && m_lDataKeys.Count() > 0)
                    {
                        List<share_number> m_lShareNumber = (from r in Redis2.Instance.GetAll<string>(m_lDataKeys.ToList())
                                                             .Select(x => { return JsonConvert.DeserializeObject<share_number>(x.Value); })
                                                             where
                                                             //电话状态为空闲
                                                             r.state == SHARE_NUM_STATUS.IDLE
                                                             //限制配置设置
                                                             &&
                                                             (
                                                                //总次数
                                                                (r.limitcount == 0 || r.limitcount > r.usecount)
                                                                &&
                                                                //总时长
                                                                (r.limitduration == 0 || r.limitduration > r.useduration)
                                                                &&
                                                                (
                                                                    //当日次数
                                                                    ((r.limitthecount == 0 || r.limitthecount > r.usethecount) && Cmn_v1.Cmn.m_fEqualsDate(r.usethetime)) || Cmn_v1.Cmn.m_fLessDate(r.usethetime)
                                                                    &&
                                                                    //当日时长
                                                                    ((r.limittheduration == 0 || r.limittheduration > r.usetheduration) && Cmn_v1.Cmn.m_fEqualsDate(r.usethetime)) || Cmn_v1.Cmn.m_fLessDate(r.usethetime)
                                                                )
                                                             )
                                                             //没有锁
                                                             &&
                                                             !m_lLockKeys.Contains($"{Redis2.m_sLockPrefix}:{r.uuid}")
                                                             //排序
                                                             orderby r.ordernum ascending, r.areaname ascending, r.number ascending
                                                             //暂时去掉同号码限呼逻辑
                                                             select r)?.ToList();

                        return m_lShareNumber;
                    }
                    else
                    {
                        Log.Instance.Warn($"[Core_v1][Redis2][m_fGetShareNumberList][no share number]");
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
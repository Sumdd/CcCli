using Model_v1;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Query_v1
{
    /// <summary>
    /// 查询字符串转换帮助类
    /// </summary>
    public class QuerySwitch
    {
        /// <summary>
        /// 将查询字符串解析成QueryModel数组
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static QuerySample ToQuerySample(string queryString)
        {
            List<QueryModel> queryList = new List<QueryModel>();

            //无次参数直接返回
            if (string.IsNullOrWhiteSpace(queryString))
            {
                return new QuerySample() { QueryList = new List<QueryModel>() };
            }

            var IList = JsonConvert.DeserializeObject<IDictionary<string, object>>(queryString);

            foreach (KeyValuePair<string, object> item in IList)
            {
                List<QueryModel> list = queryList.Where(q => q.Key == item.Key).ToList();

                //存在则移除
                if (list.Count > 0)
                {
                    list.RemoveAll(q => q.Key == item.Key);
                }

                //添加非空字段
                if (item.Value != null)
                {
                    //然后添加,保证参数唯一
                    queryList.Add(
                        new QueryModel()
                        {
                            Key = item.Key,
                            Object = item.Value
                        });
                }
            }

            return new QuerySample() { QueryList = queryList };
        }
    }
}

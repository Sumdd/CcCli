using System.Collections.Generic;

namespace Model_v1 {
    /// <summary>
    /// 查询简类
    /// </summary>
    public class QuerySample {
        public List<QueryModel> QueryList { get; set; } = new List<QueryModel>();
        public object Object {
            get; set;
        }
    }
}

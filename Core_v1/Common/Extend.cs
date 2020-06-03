using Model_v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core_v1 {
    public static class Extend {
        /// <summary>
        /// 判断键值是否存在
        /// </summary>
        /// <param name="querySample">查询简类</param>
        /// <param name="Key">键</param>
        /// <returns></returns>
        public static bool ExistKey(this QuerySample querySample, string Key) {
            foreach(QueryModel queryModel in querySample.QueryList) {
                if(queryModel.Key == Key) {
                    queryModel.Exist = true;
                    querySample.Object = queryModel.Object;
                    return true;
                }
            }
            querySample.Object = null;
            return false;
        }
    }
}

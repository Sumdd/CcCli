namespace Model_v1 {
    /// <summary>
    /// 分页
    /// </summary>
    public class Pager {
        /// <summary>
        /// 第几页
        /// </summary>
        public int page {
            get; set;
        }
        /// <summary>
        /// 每页多少条
        /// </summary>
        public int limit {
            get; set;
        }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string field {
            get; set;
        }
        /// <summary>
        /// 私人排序类型变量
        /// </summary>
        private string _type;
        /// <summary>
        /// 排序类型
        /// </summary>
        public string type {
            get {
                return _type == null ? "asc" : _type;
            }
            set {
                _type = value;
            }
        }
        /// <summary>
        /// 公开查询类型变量
        /// </summary>
        public string eqli {
            get; set;
        }
    }
}

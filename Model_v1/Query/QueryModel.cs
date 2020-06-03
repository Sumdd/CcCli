namespace Model_v1 {
    /// <summary>
    /// 查询模型
    /// </summary>
    public class QueryModel {
        /// <summary>
        /// 查询键
        /// </summary>
        public string Key {
            get; set;
        }
        /// <summary>
        /// 查询值
        /// </summary>
        public object Object {
            get; set;
        }
        /// <summary>
        /// 被证明存在
        /// </summary>
        public bool Exist { get; set; } = false;
    }
}

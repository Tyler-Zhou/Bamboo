namespace Bamboo.Entities
{
    /// <summary>
    /// 查询参数
    /// </summary>
    public class QueryParameter
    {
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string Search { get; set; }
    }
}

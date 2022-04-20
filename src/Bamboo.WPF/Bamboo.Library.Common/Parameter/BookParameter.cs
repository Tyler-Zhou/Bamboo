using Bamboo.Common.Parameter;

namespace Bamboo.Library.Common.Parameter
{
    /// <summary>
    /// 书籍查询参数
    /// </summary>
    public class BookParameter : QueryParameter
    {
        /// <summary>
        /// 书籍状态
        /// </summary>
        public int? Status { get; set; }
    }
}

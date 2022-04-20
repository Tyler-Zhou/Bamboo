using Bamboo.Common.Parameter;

namespace Bamboo.Library.Common.Parameter
{
    /// <summary>
    /// 章节查询参数
    /// </summary>
    public class ChapterParameter : QueryParameter
    {
        /// <summary>
        /// 书籍唯一键
        /// </summary>
        public int BookKey { get; set; }
    }
}

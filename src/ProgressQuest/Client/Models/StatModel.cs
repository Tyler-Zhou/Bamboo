using Client.Enums;

namespace Client.Models
{
    /// <summary>
    /// 属性模型
    /// </summary>
    public class StatModel
    {
        /// <summary>
        /// 属性
        /// </summary>
        public EnumStat StatName { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public int StatValue { get; set; }
    }
}

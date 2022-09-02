using Client.Enums;

namespace Client.Models
{
    /// <summary>
    /// 属性模型
    /// </summary>
    public class CharacterStat:BaseModel
    {
        /// <summary>
        /// 属性
        /// </summary>
        public EnumStat StatType { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public int Value { get; set; }
    }
}

namespace Client.Models
{
    /// <summary>
    /// 怪物
    /// </summary>
    public class MonsterModel : BaseModel
    {
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 物品
        /// </summary>
        public string Item { get; set; }

    }
}

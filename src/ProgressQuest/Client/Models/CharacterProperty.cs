namespace Client.Models
{
    /// <summary>
    /// 人物属性
    /// </summary>
    public class CharacterProperty
    {
        /// <summary>
        /// 力量
        /// </summary>
        public int Strength { get; set; }
        /// <summary>
        /// 体质
        /// </summary>
        public int Constitution { get; set; }
        /// <summary>
        /// 敏捷
        /// </summary>
        public int Dexterity { get; set; }
        /// <summary>
        /// 魔法攻击力
        /// </summary>
        public int Intelligence { get; set; }
        /// <summary>
        /// 智力(智慧能力数值)
        /// </summary>
        public int Wisdom { get; set; }
        /// <summary>
        /// 魅力
        /// </summary>
        public int Charisma { get; set; }
        /// <summary>
        /// 最大生命值
        /// </summary>
        public int HpMax { get; set; }
        /// <summary>
        /// 最大魔法值
        /// </summary>
        public int MpMax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsValid => Validate();
        /// <summary>
        /// 
        /// </summary>
        private bool Validate()
        {
            return ValueFallsInRange(Strength)
                && ValueFallsInRange(Constitution)
                && ValueFallsInRange(Dexterity)
                && ValueFallsInRange(Intelligence)
                && ValueFallsInRange(Wisdom)
                && ValueFallsInRange(Charisma)
                && HpMax > 0
                && MpMax > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool ValueFallsInRange(int value)
        {
            return value > 0 && value < 16;
        }
    }
}

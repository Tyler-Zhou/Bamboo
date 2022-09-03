using Client.Enums;
using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 人物装备
    /// </summary>
    public class CharacterEquipment : BaseModel
    {
        /// <summary>
        /// 装备类型
        /// </summary>
        public EnumEquipment EquipmentType { get; set; }
        /// <summary>
        /// 装备 Key
        /// </summary>
        public string EquipmentKey { get; set; }
        /// <summary>
        /// 修饰符 Key 1
        /// </summary>
        public string ModifierKey1 { get; set; }
        /// <summary>
        /// 修饰符 Key 2
        /// </summary>
        public string ModifierKey2 { get; set; }
        /// <summary>
        /// 加成
        /// </summary>
        public int Plus { get; set; } = 0;
        /// <summary>
        /// 描述
        /// </summary>
        [JsonIgnore]
        public string Description
        {
            get
            {
                string description = "";

                if (!string.IsNullOrWhiteSpace(EquipmentKey))
                {
                    if (Plus != 0)
                    {
                        description += $"{(Plus > 0 ? "+" : "-")}{Plus} ";
                    }
                    if (!string.IsNullOrWhiteSpace(ModifierKey1))
                    {
                        description += $"M1:{ModifierKey1.FindResourceDictionary()} ";
                    }
                    if (!string.IsNullOrWhiteSpace(ModifierKey2))
                    {
                        description += $"M2:{ModifierKey2.FindResourceDictionary()} ";
                    }
                    description += $"E:{EquipmentKey.FindResourceDictionary()} ";
                }
                return description;
            }
        }
    }
}

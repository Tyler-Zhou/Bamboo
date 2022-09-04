using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 货物实体
    /// </summary>
    public class CharacterItem : BaseModel
    {
        /// <summary>
        /// Item Key 1
        /// </summary>
        public string ItemKey1 { get; set; }

        /// <summary>
        /// Item Key 1
        /// </summary>
        public string ItemKey2 { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonIgnore]
        public string Description
        {
            get
            {
                string description = "";
                if ("CharacterGold".Equals(Key))
                {
                    description = Name;
                }else
                {
                    //NPC处随机获取物品 货物特征 + 特价 + 货物名称
                    if (!string.IsNullOrWhiteSpace(Key))
                    {
                        if (!string.IsNullOrWhiteSpace(ItemKey1))
                        {
                            description += $"IA:{ItemKey1.FindResourceDictionary()} ";
                        }
                        if (!string.IsNullOrWhiteSpace(ItemKey2))
                        {
                            description += $"S:{ItemKey2.FindResourceDictionary()} ";
                        }
                        description += $"I:{Key.FindResourceDictionary()} ";
                    }
                    else
                    {
                        //战斗获取装备 怪物名称 + 怪物装备
                        if (!string.IsNullOrWhiteSpace(ItemKey1))
                        {
                            description += $"MN:{ItemKey1.FindResourceDictionary()} ";
                        }
                        if (!string.IsNullOrWhiteSpace(ItemKey2))
                        {
                            description += $"I:{ItemKey2.FindResourceDictionary()} ";
                        }
                    }
                }
                return description;
            }
        }
    }
}

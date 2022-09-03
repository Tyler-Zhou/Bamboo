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
        /// ItemAttributeKey
        /// </summary>
        public string ItemAttributeKey { get; set; }

        /// <summary>
        /// SpecialKey
        /// </summary>
        public string SpecialKey { get; set; }
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

                if (!string.IsNullOrWhiteSpace(Key))
                {
                    if ("CharacterGold".Equals(Key))
                    {
                        description = Name;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(ItemAttributeKey))
                        {
                            description += $"{ItemAttributeKey.FindResourceDictionary()} ";
                        }
                        if (!string.IsNullOrWhiteSpace(SpecialKey))
                        {
                            description += $" {SpecialKey.FindResourceDictionary()} ";
                        }
                        description += $"Of {Key.FindResourceDictionary()} ";
                    }
                }
                return description;
            }
        }
    }
}

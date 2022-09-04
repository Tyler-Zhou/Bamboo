using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 法术书
    /// </summary>
    public class CharacterSpellBook : BaseModel
    {
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonIgnore]
        public string Description
        {
            get
            {
                return Level.ToRomanNumber();
            }
        }
    }
}

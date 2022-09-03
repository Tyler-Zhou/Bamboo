using Client.Extensions;
using Client.Helpers;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 任务进度
    /// </summary>
    public class ProgressRateQuest : ProgressRateBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [JsonIgnore]
        public string Name
        {
            get
            {
                string name = Key.FindResourceDictionary();
                name = name.Replace($"^Percent$", CharacterHelper.Percent(Position, MaxValue));
                return name;
            }
            set
            {
            }
        }
    }
}

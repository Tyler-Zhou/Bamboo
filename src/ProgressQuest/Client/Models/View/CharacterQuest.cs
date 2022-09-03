using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 任务模型
    /// </summary>
    public class CharacterQuest
    {
        /// <summary>
        /// 名称
        /// </summary>
        [JsonIgnore]
        public string Name
        {
            get
            {
                string key = "PlotPrologue";
                if (Index > 0)
                {
                    key = "PlotAct";
                }
                string name = key.FindResourceDictionary();
                name = name.Replace($"^RomanNumber$", Index.ToRomanNumber());
                return name;
            }
        }

        /// <summary>
        /// 剧幕索引
        /// </summary>
        public int Index { get; set; } = 0;

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsCommplete { get; set; } = false;
    }
}

using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 剧幕实体
    /// </summary>
    public class CharacterAct
    {
        /// <summary>
        /// 描述
        /// </summary>
        [JsonIgnore]
        public string Description
        {
            get
            {
                string key = "DataGridPlotPrologue";
                if (Index > 0)
                {
                    key = "DataGridPlotAct";
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

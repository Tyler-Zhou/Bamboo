using Client.Extensions;
using Newtonsoft.Json;
using System;

namespace Client.Models
{
    /// <summary>
    /// 剧情进度
    /// </summary>
    public class ProgressRatePlot : ProgressRateBase
    {
        /// <summary>
        /// 时间
        /// </summary>
        public int CommpleteNeedTime { get; set; } = 1;

        /// <summary>
        /// 名称
        /// </summary>
        [JsonIgnore]
        public string Name
        {
            get
            {
                string name = Key.FindResourceDictionary();
                name = name.Replace($"^Time$", new TimeSpan(0, 0, CommpleteNeedTime).ToString(@"hh\:mm\:ss"));
                return name;
            }
            set
            {
            }
        }
    }
}

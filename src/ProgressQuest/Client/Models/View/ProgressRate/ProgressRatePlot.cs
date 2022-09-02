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
        /// 名称
        /// </summary>
        [JsonIgnore]
        public string Name
        {
            get
            {
                string name = Key;
                try
                {
                    name = System.Windows.Application.Current.FindResource(Key).ToString();
                    name = name.Replace($"^Time$", new TimeSpan(0, 0,CommpleteNeedTime).ToString(@"hh\:mm\:ss"));
                }
                catch
                {
                    name = Key;
                }
                return name;
            }
            set
            {
            }
        }

        /// <summary>
        /// 时间
        /// </summary>
        public int CommpleteNeedTime { get; set; } = 1;
    }
}

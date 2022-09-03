﻿using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 经验进度
    /// </summary>
    public class ProgressRateExperience : ProgressRateBase
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
                name = name.Replace($"^Remaining$", "" + (MaxValue - Position));
                return name;
            }
            set
            {
            }
        }
    }
}

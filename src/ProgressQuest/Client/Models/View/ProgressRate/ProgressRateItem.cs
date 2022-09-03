using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 货物进度
    /// </summary>
    public class ProgressRateItem : ProgressRateBase
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
                name = name.Replace($"^Position$", "" + Position);
                name = name.Replace($"^MaxValue$", "" + MaxValue);
                return name;
            }
            set
            {

            }
        }

    }
}

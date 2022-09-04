using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 任务基类
    /// </summary>
    public class BaseTask
    {
        /// <summary>
        /// 任务 Key
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// 时间间隔(秒)
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonIgnore]
        public virtual string Description 
        {
            get
            {
                return Key.FindResourceDictionary();
            }
            set
            {

            }
        }
    }
}

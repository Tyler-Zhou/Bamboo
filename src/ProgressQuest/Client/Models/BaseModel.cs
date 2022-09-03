using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 键值
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [JsonIgnore]
        public virtual string Name
        {
            get
            {
                return Key.FindResourceDictionary();
            }
        }
    }
}

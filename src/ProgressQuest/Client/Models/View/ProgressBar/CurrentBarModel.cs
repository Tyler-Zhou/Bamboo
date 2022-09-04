using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 当前任务进度条
    /// </summary>
    public class CurrentBarModel: BaseBarModel
    {
        /// <summary>
        /// 工具栏提示
        /// </summary>
        [JsonIgnore]
        public string ToolTip { get; set; }
    }
}

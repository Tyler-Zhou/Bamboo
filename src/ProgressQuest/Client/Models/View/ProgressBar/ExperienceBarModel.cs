using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 经验进度条
    /// </summary>
    public class ExperienceBarModel : BaseBarModel
    {
        /// <summary>
        /// 工具栏提示
        /// </summary>
        [JsonIgnore]
        public string ToolTip
        {
            get
            {
                string name = "ProgressBarToolTipExperience".FindResourceDictionary();
                name = name.Replace($"^Remaining$", "" + (MaxValue - Position));
                return name;
            }
            set
            {
            }
        }
    }
}

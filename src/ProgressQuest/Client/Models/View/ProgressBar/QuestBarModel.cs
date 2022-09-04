using Client.Extensions;
using Client.Helpers;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 任务进度条
    /// </summary>
    public class QuestBarModel : BaseBarModel
    {
       
        
        /// <summary>
        /// 名称
        /// </summary>
        [JsonIgnore]
        public string ToolTip
        {
            get
            {
                string name = "ProgressBarToolTipQuest".FindResourceDictionary();
                name = name.Replace($"^Percent$", CharacterHelper.Percent(Position, MaxValue));
                return name;
            }
            set
            {
            }
        }
    }
}

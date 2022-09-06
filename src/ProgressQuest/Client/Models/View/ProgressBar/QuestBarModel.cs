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
        #region 工具栏提示
        /// <summary>
        /// 工具栏提示
        /// </summary>
        [JsonIgnore]
        public override string ToolTip
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
        #endregion
    }
}

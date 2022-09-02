using Client.Helpers;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 任务进度
    /// </summary>
    public class ProgressRateQuest : ProgressRateBase
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
                    name = name.Replace($"^Percent$", CharacterHelper.Percent(Position,MaxValue));
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
    }
}

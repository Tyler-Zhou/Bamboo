using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 任务
    /// </summary>
    public class ProgressBarQuest : ProgressBarBase
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
                    name = name.Replace($"^Percent$", "");
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

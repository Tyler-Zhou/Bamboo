using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 经验进度条
    /// </summary>
    public class ProgressBarExperience:ProgressBarBase
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
                    name = name.Replace($"^Remaining$", "" +(MaxValue-Position));
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

using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 剧情
    /// </summary>
    public class ProgressBarPlot : ProgressBarBase
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
                    name = name.Replace($"^Time$", "" + Position);
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

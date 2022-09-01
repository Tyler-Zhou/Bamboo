using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 详细目录
    /// </summary>
    public class ProgressBarInventory: ProgressBarBase
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
                    name = name.Replace($"^Position$",""+Position);
                    name = name.Replace($"^MaxValue$",""+ MaxValue);
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

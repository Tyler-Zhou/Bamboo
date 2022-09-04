using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 货物进度条
    /// </summary>
    public class ItemBarModel : BaseBarModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [JsonIgnore]
        public string ToolTip
        {
            get
            {
                string name = "ProgressBarToolTipInventory".FindResourceDictionary();
                name = name.Replace($"^Position$", "" + Position);
                name = name.Replace($"^MaxValue$", "" + MaxValue);
                return name;
            }
            set
            {

            }
        }

    }
}

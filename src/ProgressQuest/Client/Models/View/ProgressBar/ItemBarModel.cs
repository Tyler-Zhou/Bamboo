using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 货物进度条
    /// </summary>
    public class ItemBarModel : BaseBarModel
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
                string name = "ProgressBarToolTipInventory".FindResourceDictionary();
                name = name.Replace($"^Position$", "" + Position);
                name = name.Replace($"^MaxValue$", "" + MaxValue);
                return name;
            }
            set
            {

            }
        } 
        #endregion

    }
}

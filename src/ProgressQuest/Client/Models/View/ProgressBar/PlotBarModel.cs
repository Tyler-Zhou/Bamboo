using Client.Extensions;
using Newtonsoft.Json;
using System;

namespace Client.Models
{
    /// <summary>
    /// 剧情进度条
    /// </summary>
    public class PlotBarModel : BaseBarModel
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
                string name = "ProgressBarToolTipPlot".FindResourceDictionary();
                name = name.Replace($"^Time$", new TimeSpan(0, 0, (int)(MaxValue - Position)).ToString(@"hh\:mm\:ss"));
                return name;
            }
            set
            {
            }
        }
        #endregion
    }
}

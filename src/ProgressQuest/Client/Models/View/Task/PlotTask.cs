using Client.Extensions;
using Newtonsoft.Json;

namespace Client.Models
{
    /// <summary>
    /// 剧情任务
    /// </summary>
    public class PlotTask : BaseTask
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public override EnumTask TaskType
        {
            get
            {
                return EnumTask.Plot;
            }
        }
        /// <summary>
        /// SellTaskKey
        /// </summary>
        public override string Key
        {
            get
            {
                return "TaskPlot";
            }
        }
        /// <summary>
        /// 剧幕索引
        /// </summary>
        public int ActIndex { get; set; } = 0;

        /// <summary>
        /// 描述
        /// </summary>
        [JsonIgnore]
        public override string Description
        {
            get
            {
                string name = Key.FindResourceDictionary();
                //Loading ^ActIndex$
                name = name.Replace($"^ActIndex$", ActIndex.ToRomanNumber());
                return name;
            }
            set
            {

            }
        }
    }
}

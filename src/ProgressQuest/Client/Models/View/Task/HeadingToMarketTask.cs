namespace Client.Models
{
    /// <summary>
    /// 走向市场
    /// </summary>
    public class HeadingToMarketTask : BaseTask
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public override EnumTask TaskType
        {
            get
            {
                return EnumTask.HeadingToMarket;
            }
        }
        /// <summary>
        /// SellTaskKey
        /// </summary>
        public override string Key
        {
            get
            {
                return "TaskHeadingToMarket";
            }
        }
    }
}

namespace Client.Models
{
    /// <summary>
    /// 购买任务
    /// </summary>
    public class BuyTask : BaseTask
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public override EnumTask TaskType
        {
            get
            {
                return EnumTask.Buy;
            }
        }
        /// <summary>
        /// SellTaskKey
        /// </summary>
        public override string Key
        {
            get
            {
                return "TaskBuy";
            }
        }
    }
}

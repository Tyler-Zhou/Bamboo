namespace Client.Models
{
    /// <summary>
    /// 购买任务
    /// </summary>
    public class BuyTask : BaseTask
    {
        /// <summary>
        /// SellTaskKey
        /// </summary>
        public override string Key
        {
            get
            {
                return "TaskBuyKey";
            }
        }
    }
}

namespace Client.Models
{
    /// <summary>
    /// 走向市场
    /// </summary>
    public class HeadingToMarketTask: BaseTask
    {
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

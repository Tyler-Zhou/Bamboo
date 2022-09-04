namespace Client.Models
{
    /// <summary>
    /// 前往杀戮场
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
                return "TaskHeadingToMarketKey";
            }
        }
    }
}

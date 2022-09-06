namespace Client.Models
{
    /// <summary>
    /// 前往杀戮战场
    /// </summary>
    public class HeadingToKillingFieldsTask : BaseTask
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public override EnumTask TaskType
        {
            get
            {
                return EnumTask.HeadingToKillingFields;
            }
        }
        /// <summary>
        /// SellTaskKey
        /// </summary>
        public override string Key
        {
            get
            {
                return "HeadingToKillingFields";
            }
        }
    }
}

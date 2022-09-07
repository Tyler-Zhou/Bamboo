namespace Client.Models
{
    /// <summary>
    /// 常规任务
    /// </summary>
    public class RegularTask : BaseTask
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public override EnumTask TaskType
        {
            get
            {
                return EnumTask.Regular;
            }
        }
    }
}

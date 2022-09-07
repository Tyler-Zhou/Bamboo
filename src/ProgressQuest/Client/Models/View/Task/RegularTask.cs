using Client.Extensions;

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

        /// <summary>
        /// 描述
        /// </summary>
        public override string Description
        {
            get
            {
                return Key.FindResourceDictionary();
            }
            set
            {

            }
        }
    }
}

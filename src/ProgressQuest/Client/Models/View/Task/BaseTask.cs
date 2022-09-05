using Client.Extensions;

namespace Client.Models
{
    /// <summary>
    /// 任务基类
    /// </summary>
    public class BaseTask
    {
        /// <summary>
        /// 任务 Key
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// 时间间隔(秒)
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description 
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

namespace Client.Models
{
    /// <summary>
    /// 任务进程
    /// </summary>
    public class TaskProgressModel:BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxValue { get; set; }
    }
}

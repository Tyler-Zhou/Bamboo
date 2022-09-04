namespace Client.Models
{
    /// <summary>
    /// 常规任务
    /// </summary>
    public class RegularTask: BaseTask
    {
        /// <summary>
        /// 怪物 Key
        /// </summary>
        public string MonsterKey { get; set; }

        /// <summary>
        /// 怪物名字
        /// </summary>
        public string MonsterName { get; set; }
    }
}

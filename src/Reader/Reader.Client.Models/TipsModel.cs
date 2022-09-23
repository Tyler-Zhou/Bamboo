namespace Reader.Client.Models
{
    /// <summary>
    /// 提示(信息)模型
    /// </summary>
    public class TipsModel
    {
        /// <summary>
        /// 筛选
        /// </summary>
        public string Filter { get; set; } = "";
        /// <summary>
        /// 消息
        /// </summary>
        public TipsInfo Tips { get; set; }
    }
}

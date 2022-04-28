namespace Bamboo.Client.Core.Models
{
    /// <summary>
    /// 客户端接收响应对象
    /// </summary>
    public class ReceiveResponse
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = "";
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; } = false;
        /// <summary>
        /// 结果对象
        /// </summary>
        public object Result { get; set; }
    }
    /// <summary>
    /// 客户端接收响应对象
    /// </summary>
    /// <typeparam name="T">响应结果类型</typeparam>
    public class ReceiveResponse<T>
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = "";
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; } = false;
        /// <summary>
        /// 结果对象
        /// </summary>
        public T Result { get; set; }
    }
}

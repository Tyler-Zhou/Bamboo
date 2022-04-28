namespace Bamboo.Server.Entities
{
    /// <summary>
    /// 服务器响应对象
    /// </summary>
    public class ServerResponse
    {
        /// <summary>
        /// 服务器响应对象(提示错误/异常调用)
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="status">状态</param>
        public ServerResponse(string message, bool status = false)
        {
            Message = message;
            Status = status;
        }
        /// <summary>
        /// 服务器响应对象(存在数据调用)
        /// </summary>
        /// <param name="status">消息</param>
        /// <param name="result">结果对象</param>
        public ServerResponse(bool status, object result)
        {
            Status = status;
            Result = result;
        }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 结果对象
        /// </summary>
        public object Result { get; set; }
    }
}

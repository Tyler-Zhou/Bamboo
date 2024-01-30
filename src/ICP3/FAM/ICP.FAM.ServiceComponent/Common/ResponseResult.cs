namespace ICP.FAM.ServiceComponent.Common
{
    /// <summary>
    /// 响应结果JSON
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 结果状态,默认值false
        /// </summary>
        public bool success { set; get; }

        /// <summary>
        /// 消息 默认值空字符串
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 详细消息 默认值空字符串
        /// </summary>
        public string message_detail { get; set; }

        /// <summary>
        /// 代码 默认值0
        /// </summary>
        public int code { set; get; }

        /// <summary>
        /// 返回结果对象,默认值Null
        /// </summary>
        public object data { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ResponseResult()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_success">是否成功</param>
        public ResponseResult(bool _success)
        {
            success = _success;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_success">是否成功</param>
        /// <param name="_message">消息内容</param>
        public ResponseResult(bool _success, string _message)
        {
            success = _success;
            message = _message;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_success">是否成功</param>
        /// <param name="_message">消息内容</param>
        /// <param name="_data">消息数据</param>
        public ResponseResult(bool _success, string _message, object _data)
        {
            success = _success;
            message = _message;
            data = _data;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_success">是否成功</param>
        /// <param name="_message">消息内容</param>
        /// <param name="_code">消息代码</param>
        /// <param name="_data">消息数据</param>
        public ResponseResult(bool _success, string _message, int _code, object _data)
        {
            success = _success;
            message = _message;
            code = _code;
            data = _data;
        }
    }
}

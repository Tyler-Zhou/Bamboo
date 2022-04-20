using RestSharp;

namespace Bamboo.Client.Core.Models
{
    /// <summary>
    /// 客户端请求基类
    /// </summary>
    public class BaseRequest
    {
        /// <summary>
        /// 请求方式
        /// </summary>
        public Method Method { get; set; }
        /// <summary>
        /// 路由
        /// </summary>
        public string Route { get; set; } = "";
        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; } = "application/json;charset=utf-8";
        /// <summary>
        /// 参数
        /// </summary>
        public object Parameter { get; set; } = "";
    }
}

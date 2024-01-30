#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/6/20 17:10:02
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using Cityocean.Crawl.CommonLibrary;

namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 网站参数类型
    /// </summary>
    [Serializable]
    public sealed class CrawlConfigParam
    {
        /// <summary>
        /// 参数键
        /// </summary>
        public Guid KeyID { get; set; }
        /// <summary>
        /// 参数键
        /// </summary>
        public string KeyValue { get; set; }
        /// <summary>
        /// 参数键类型
        /// </summary>
        public Website_KeyType KeyType { get; set; }
        /// <summary>
        /// 参数键值类型
        /// </summary>
        public Website_KeyValueType KeyValueType { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public Website_ParamType ParamType { get; set; }
        /// <summary>
        /// 参数值:类属性名称/常量值
        /// </summary>
        public string ParamValue { get; set; }
        /// <summary>
        /// 排序索引
        /// </summary>
        public int SortIndex { get; set; }

        private int _Timeout;
        /// <summary>
        /// 超时秒数(s)
        /// </summary>
        public int Timeout { get { return _Timeout; } set { _Timeout = value == 0 ? 10 : value; } }

        /// <summary>
        /// 通过Key获取网站参数值
        /// </summary>
        /// <returns></returns>
        public string GetParamValueByKey()
        {
            return ParamValue;
        }

        /// <summary>
        /// 通过Key获取网站参数值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetParamValueByKey<T>(T obj)
        {
            string paramValue = string.Empty;
            paramValue = ParamType == Website_ParamType.Constants ? ParamValue
                                : obj.GetPropertyValue(ParamValue).TryToString();
            return paramValue;
        }
    }
}

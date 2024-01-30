#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/10 16:44:52
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 返回结果
    /// </summary>
    [Serializable]
    public class EResultInfo
    {
        /// <summary>
        /// 方法返回值
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ExceptionMessage { get; set; }
    }
}

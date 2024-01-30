#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/12/20 星期三 11:21:56
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
    /// 流水号帮助类
    /// </summary>
    public sealed class SerialnumberHelper
    {
        /// <summary>
        /// 流水号生成
        /// </summary>
        /// <param name="paramCodePrefix">前缀</param>
        /// <returns></returns>
        public static string GenerateNO(string paramCodePrefix = "")
        {
            return paramCodePrefix + DateTime.Now.ToString("yyyyMMddHHmmssms");;
        }
    }
}

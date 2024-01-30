#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/5/27 11:49:23
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
    #region 浏览器类型
    /// <summary>
    /// 浏览器类型
    /// </summary>
    [Serializable]
    public enum Browsers
    {
        /// <summary>
        /// PhantomJS
        /// </summary>
        PhantomJS = 1,
        /// <summary>
        /// 谷歌
        /// </summary>
        Chrome = 2,
        /// <summary>
        /// IE
        /// </summary>
        IE = 3,
        /// <summary>
        /// 火狐
        /// </summary>
        Firefox = 4,
        /// <summary>
        /// Safari
        /// </summary>
        Safari = 5,
    }
    #endregion
}

#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/6/20 16:21:20
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
    /// 网站箱状态配置
    /// </summary>
    [Serializable]
    public sealed class WebsiteContainerStatus
    {
        /// <summary>
        /// 网站ID
        /// </summary>
        public Guid WebsiteID { get; set; }
        /// <summary>
        /// 提空柜(Empty Pick-up)
        /// </summary>
        public string EmptyPickUp { get; set; }
        /// <summary>
        /// 提重柜(Full Pick-up)
        /// </summary>
        public string FullPickUp { get; set; }
        /// <summary>
        /// 装船(Loaded on board)
        /// </summary>
        public string LOBD { get; set; }
        /// <summary>
        /// 卸船(Un Loaded on board)
        /// </summary>
        public string UNLOBD { get; set; }
        /// <summary>
        /// 还空柜(Return Empty Container)
        /// </summary>
        public string REC { get; set; }
    }
}

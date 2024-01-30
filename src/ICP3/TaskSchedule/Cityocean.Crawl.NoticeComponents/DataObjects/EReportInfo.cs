#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/7/27 15:34:07
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace Cityocean.Crawl.NoticeComponents
{
    /// <summary>
    /// 报告信息对象
    /// </summary>
    [Serializable]
    public sealed class EReportInfo
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string[] AttachmentPaths { get; set; }
        /// <summary>
        /// 报告文本
        /// </summary>
        public string Context { get; set; }
        /// <summary>
        /// 报告信息对象
        /// </summary>
        /// <param name="paramName"></param>
        public EReportInfo(string paramName)
        {
            Name = paramName;
        }
    }
}

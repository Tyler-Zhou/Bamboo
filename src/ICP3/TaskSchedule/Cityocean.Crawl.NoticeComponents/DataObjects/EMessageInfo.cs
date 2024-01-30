#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/24 16:46:25
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
    /// 消息
    /// </summary>
    [Serializable]
    public sealed class EMessageInfo
    {
        /// <summary>
        /// 消息类型
        /// 1:Info
        /// 2.Warn
        /// 3:Error
        /// </summary>
        public int MType { get; set; }
        /// <summary>
        /// 消息所属任务
        /// </summary>
        public string MOwerJob { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MContent { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string[] AttachmentPath { get; set; }
    }
}

#region Comment

/*
 * 
 * FileName:    PostInfo.cs
 * CreatedOn:   2015/12/1 15:27:17
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using ICP.FileSystem.ServiceInterface;
using System;
using System.Diagnostics;

namespace ICP.Business.Common.UI.Document
{
    /// <summary>
    /// Post请求信息
    /// </summary>
    [Serializable]
    public partial class PostInfo
    {
        /// <summary>
        /// 保存状态
        /// </summary>
        public bool[] Saved { get; set; }
        /// <summary>
        /// 文档类型
        /// </summary>
        public DocumentType? DocumentType { get; set; }

        /// <summary>
        /// 操作日志ID
        /// </summary>
        public Guid OperationLogID { get; set; }

        /// <summary>
        /// 计时器
        /// </summary>
        public Stopwatch StopWatch { get; set; }
    }
}

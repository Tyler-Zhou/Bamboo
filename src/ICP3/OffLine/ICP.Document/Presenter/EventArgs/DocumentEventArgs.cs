#region Comment

/*
 * 
 * FileName:    DocumentEventArgs.cs
 * CreatedOn:   2014/5/14 星期三 16:58:01
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->文档事件自定义传入参数
 *      ->1.DocumentID:文档GUID主键
 *      ->2.OperationID:业务数据对应GUID主键
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;

namespace ICP.Document
{
    /// <summary>
    /// 文档事件自定义传入参数
    /// </summary>
    public class DocEventArgs : EventArgs
    {
        /// <summary>
        /// 文档GUID主键
        /// </summary>
        public Guid DocumentID { get; set; }
        /// <summary>
        /// 业务操作GUID主键
        /// </summary>
        public Guid OperationID { get; set; }
    }
}

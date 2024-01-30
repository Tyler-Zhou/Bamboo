#region Comment

/*
 * 
 * FileName:    DocumentInfo.cs
 * CreatedOn:   2014/5/14 星期三 14:25:37
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->文档实体类
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
    /// 文档实体类
    /// </summary>
    [Serializable]
    public class DocumentInfo
    {
        /// <summary>
        ///文档Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public String DName
        {
            get;
            set;
        }
        /// <summary>
        /// 单证类型
        /// </summary>
        public DocumentType? DType { get; set; }
        /// <summary>
        /// HTML 内容
        /// </summary>
        public Byte[] HtmlContent { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public Byte[] Content { get; set; }
    }
}

#region Comment

/*
 * 
 * FileName:    IDocumentView.cs
 * CreatedOn:   2014/5/14 星期三 16:57:19
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->文档呈现数据的接口
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;

namespace ICP.Document
{
    /// <summary>
    /// 文档呈现数据的接口
    /// </summary>
    public interface IVDocument : IViewBase
    {
        /// <summary>
        /// 填充文档列表信息
        /// </summary>
        /// <param name="docList">文档集合</param>
        void FillDocumentInfo(List<DocumentInfo> docList);
        /// <summary>
        /// 预览文件
        /// </summary>
        /// <param name="paramFilePath">文件路径</param>
        void Document_Preview(string paramFilePath);
        /// <summary>
        /// 获取文档信息
        /// </summary>
        event EventHandler<DocEventArgs> GetDocument;
        /// <summary>
        /// 以进程方式打开文档
        /// </summary>
        event EventHandler<DocEventArgs> OpenDocument_Process;
        /// <summary>
        /// 选择打开方式
        /// </summary>
        event EventHandler<DocEventArgs> OpenDocument_OpenWith;
        /// <summary>
        /// 预览文档
        /// </summary>
        event EventHandler<DocEventArgs> OpenDocument_Preview;
    }
}

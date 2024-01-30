#region Comment

/*
 * 
 * FileName:    PDocument.cs
 * CreatedOn:   2014/5/14 星期三 17:00:16
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->文档数据逻辑处理类
 *      ->1.GetDocument:通过OperationID获取所有文档信息
 *      ->2.OpenDocument_Process:通过启动进程打开文档
 *      ->3.OpenDocument_OpenWith:调用API通过选择打开文件方式打开文件
 *      ->4.OpenDocument_Preview:预览文档
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System.Collections.Generic;
using System.Diagnostics;

namespace ICP.Document
{
    /// <summary>
    /// 文档数据逻辑处理类
    /// </summary>
    public class PDocument : Presenter<IVDocument>
    {
        /// <summary>
        /// 文档DB处理类
        /// </summary>
        public DocumentModel Model { get; private set; }

        public PDocument(IVDocument view)
            : base(view)
        {
            this.Model = new DocumentModel();
        }
  
        protected override void OnViewSet()
        {
            #region 通过OperationID获取所有文档信息
            this.View.GetDocument += (sender, args) =>
                    {
                        //1.查询列表
                        List<DocumentInfo> docList = this.Model.GetDocumentListByOperationID(args.OperationID);
                        this.View.FillDocumentInfo(docList);
                    }; 
            #endregion

            #region 启动进程打开文件
            this.View.OpenDocument_Process += (sender, args) =>
                    {
                        //1.查询Content
                        DocumentInfo docItem = this.Model.GetDocumentContentByID(args.DocumentID);
                        //2.保存文件到磁盘
                        string filePath = ClientUtility.SaveFileContentToDisk(docItem);
                        //3.验证文件是否存在
                        if (string.IsNullOrEmpty(filePath))
                            return;
                        //4.启动进程
                        using (Process proc = System.Diagnostics.Process.Start(filePath))
                        {
                            if (proc != null)
                                proc.Dispose();
                        }
                    }; 
            #endregion

            #region 选择打开文件方式
            this.View.OpenDocument_OpenWith += (sender, args) =>
                {
                    //1.查询Content
                    DocumentInfo docItem = this.Model.GetDocumentContentByID(args.DocumentID);
                    //2.保存文件到磁盘
                    string filePath = ClientUtility.SaveFileContentToDisk(docItem);
                    //3.验证文件是否存在
                    if (string.IsNullOrEmpty(filePath))
                        return;
                    //4.启动选择打开方式进程
                    using (Process proc = System.Diagnostics.Process.Start("rundll32.exe", @"shell32,OpenAs_RunDLL " + filePath))
                    {
                        if (proc != null)
                        {
                            proc.Dispose();
                        }
                    }
                }; 
            #endregion

            #region 预览文档
            this.View.OpenDocument_Preview += (sender, args) =>
                {
                    //1.查询Content
                    DocumentInfo docItem = this.Model.GetDocumentContentByID(args.DocumentID);
                    //2.保存文件到磁盘
                    string filePath = ClientUtility.SaveFileContentToDisk(docItem);
                    //3.预览文件
                    this.View.Document_Preview(filePath);
                }; 
            #endregion
        }
    }
}

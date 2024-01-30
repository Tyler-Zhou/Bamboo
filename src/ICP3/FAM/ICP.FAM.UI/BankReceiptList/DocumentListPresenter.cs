using System.Reflection;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ICP.FileSystem.ServiceInterface;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace ICP.FAM.UI.BankReceiptList
{
    /// <summary>
    /// 文档列表呈现类
    /// </summary>
    public sealed class DocumentListPresenter:IDisposable
    {
        #region Fields & Property & Services & Delegate
        /// <summary>
        /// 操作日志ID
        /// </summary>
        private Guid _OperationLogID;
        /// <summary>
        /// 前次预览文件ID
        /// </summary>
        private Guid _previousFileId;
        /// <summary>
        /// 前次预览文件路径
        /// </summary>
        private string _previousFilePath;
        /// <summary>
        /// 位置对象
        /// </summary>
        Point _location;
        /// <summary>
        /// 大小
        /// </summary>
        Size _size;
        /// <summary>
        /// 是否已经设置
        /// </summary>
        bool _isSet = false;
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        public List<DocumentInfo> DocumentList
        {
            get
            {
                if (PartDocumentList == null || PartDocumentList.DataSource == null)
                {
                    return new List<DocumentInfo>();
                }
                return PartDocumentList.DataSource;
            }
            set
            {
                if (PartDocumentList == null)
                {
                    return;
                }
                else
                {
                    PartDocumentList.DataSource = value;
                }
            }
        }
        /// <summary>
        /// 获取文档列表文件名集合
        /// </summary>
        public List<string> FilesName
        {
            get
            {
                return DocumentList.Select(item => item.Name).ToList();
            }
            set
            {
                FilesName = value;
            }
        }

        /// <summary>
        /// 文档列表面板
        /// </summary>
        public CustomerDocumentList PartDocumentList
        {
            get;
            set;
        }

        /// <summary>
        /// 业务上下文
        /// </summary>
        public BusinessOperationContext BusinessContext { get; set; }

        /// <summary>
        /// 客户端上传文件服务
        /// </summary>
        public IClientFileService ClientFileService
        {
            get
            {
                return ServiceClient.GetService<IClientFileService>();
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="context"></param>
        private delegate void BindDataDelegate(BusinessOperationContext context);
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DocumentListPresenter() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ucList">文档列表面板</param>
        public DocumentListPresenter(CustomerDocumentList ucList)
        {
            PartDocumentList = null;
            PartDocumentList = ucList;
        } 
        #endregion

        #region Delegate & Method
        /// <summary>
        /// 重新上传
        /// </summary>
        /// <param name="documentLocalIds"></param>
        public void Reupload(List<Guid> documentLocalIds)
        {
            WaitCallback callback = data =>
            {
                try
                {
                    ClientFileService.ReUploadCustomerDocument(documentLocalIds);
                }
                catch (Exception ex)
                {
                    throw new ICPException(ex.Message);
                }
            };
            ThreadPool.QueueUserWorkItem(callback);
        }
        /// <summary>
        /// 上传
        /// 根据文件路径等信息构建文档实体对象列表
        /// </summary>
        /// <param name="filePaths">文件路径</param>
        /// <param name="strDocumentType">文档类型</param>
        /// <returns>文档实体列表</returns>
        public void Upload(string[] filePaths,string strDocumentType)
        {
            DocumentType documentType = GetDocumentTypeByCaption(strDocumentType);
            
            int count = filePaths.Length;
            DocumentInfo[] documents = new DocumentInfo[count];
            string docIDs = "";
            for (int i = 0; i < count; i++)
            {
                documents[i] = InitDocumentInfo(documentType);
                documents[i].Name = Path.GetFileName(filePaths[i]);
                documents[i].OperationID = BusinessContext.CompanyID;
                documents[i].CustomerID = BusinessContext.CustomerID;
                docIDs += "[" + documents[i].Id + "],";
            }
            WaitCallback callback = data =>
            {
                try
                {
                    InnerUpload(documents, filePaths);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
                }
            };
            ThreadPool.QueueUserWorkItem(callback);
        }

        /// <summary>
        /// 验证文件名是否有重复
        /// </summary>
        /// <param name="sourceNames">源文件</param>
        /// <param name="validateNames">验证的文件名集合</param>
        /// <returns>True:重复 False:不重复</returns>
        bool ValidateFileNameDuplicate(List<string> sourceNames, String[] validateNames)
        {
            foreach (string fileName in validateNames)
            {
                string fileName2 = Path.GetFileName(fileName);
                if (sourceNames.Contains(fileName2))
                {

                    string errorMsg = LocalData.IsEnglish ? string.Format("Document :{0} exists.", fileName2) : string.Format("文档:{0}已经存在。", fileName2);
                    throw new ICPException(errorMsg);
                }
            }
            return false;
        }

        /// <summary>
        /// 通过文本获取文档类型
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        private DocumentType GetDocumentTypeByCaption(string caption)
        {
            return DocumentType.BR;
        }

        DocumentInfo InitDocumentInfo(DocumentType? documentType)
        {
            DocumentInfo info = new DocumentInfo();
            info.Id = Guid.NewGuid();
            info.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            info.CreateBy = LocalData.UserInfo.LoginID;
            info.CreateByName = LocalData.IsEnglish ? LocalData.UserInfo.LoginName : LocalData.UserInfo.UserName;
            info.OperationID = BusinessContext.CompanyID;
            info.Type = BusinessContext.OperationType;
            info.FormType = BusinessContext.FormType;
            info.DocumentType = documentType.HasValue ? documentType.Value : DocumentType.Other;
            info.UpdateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            info.State = UploadState.LocalProcessing;
            info.FileSources = FileSource.FDocument;
            return info;
        }

        /// <summary>
        /// 上传文档到服务器
        /// </summary>
        /// <param name="documents">文档列表</param>
        /// <param name="filePaths"></param>
        private void InnerUpload(DocumentInfo[] documents, string[] filePaths)
        {
            ClientFileService.UplaodCustomerDocument(documents, filePaths);
        }

        /// <summary>
        /// 从服务端删除文档
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="updateDates"></param>
        public void Delete(List<Guid> ids, List<DateTime?> updateDates)
        {
            WaitCallback callback = data =>
            {
                List<Guid> documentIds = data as List<Guid>;
                ClientFileService.DeleteCustomerDocumentList(documentIds, updateDates,LocalData.UserInfo.LoginID);
            };
            ThreadPool.QueueUserWorkItem(callback, ids);
        }

        #region Open
        /// <summary>
        /// 根据文档ID打开文件
        /// </summary>
        /// <param name="id">文档ID</param>
        public void Open(Guid id)
        {
            ContentInfo info = ClientFileService.GetCustomerDocumentContent(id);
            string filePath = DataCacheUtility.SaveFileContentToDisk(info);
            using (Process proc = Process.Start(filePath))
            {
                if (proc != null)
                    proc.Dispose();
            }
        } 
        #endregion

        #region OpenWith
        /// <summary>
        /// 根据文档ID以选择打开方式打开文件
        /// </summary>
        /// <param name="id">文档ID</param>
        public void OpenWith(Guid id)
        {
            ContentInfo info = ClientFileService.GetCustomerDocumentContent(id);
            string filePath = DataCacheUtility.SaveFileContentToDisk(info);
            OpenWith(filePath);
        }
        /// <summary>
        /// 根据文件名选择打开方式
        /// </summary>
        /// <param name="filePath"></param>
        public static void OpenWith(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }
            using (Process proc = Process.Start("rundll32.exe", @"shell32,OpenAs_RunDLL " + filePath))
            {
                if (proc != null)
                {
                    proc.Dispose();
                }
            }
        } 
        #endregion

        /// <summary>
        /// 获取预览窗格位置及其窗体大小
        /// </summary>
        private void GetPositionAndSize()
        {
            if (!_isSet)
            {
                Screen scr = null;
                scr = Screen.FromPoint(PartDocumentList.Location);
                _location = new Point((int)(scr.WorkingArea.Width * 0.4), LocalData.Height);
                int height = scr.WorkingArea.Height - LocalData.Height;
                int width = (int)(scr.WorkingArea.Width * 0.6);
                _size = new Size(width, height);
                _isSet = true;
            }
        }
        /// <summary>
        /// 根据文档ID预览文档
        /// </summary>
        /// <param name="id">文档ID</param>
        public void Preview(Guid id)
        {
            String filePath = string.Empty;
            try
            {
                if (id != _previousFileId)
                {
                    ContentInfo info = ClientFileService.GetCustomerDocumentContent(id);
                    filePath = DataCacheUtility.SaveFileHtmlContentToDisk(info);
                    info = null;
                }
                else
                {
                    filePath = _previousFilePath;
                }
                GetPositionAndSize();
                ServiceClient.FilePreviewService.Preview(filePath, _location, _size, true);
                _previousFileId = id;
                _previousFilePath = filePath;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(PartDocumentList, ex);
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="document"></param>
        public void Download(DocumentInfo document)
        {
            string fileSavePath = GetFileSavePath(document);
            if (!string.IsNullOrEmpty(fileSavePath))
            {
                ContentInfo info = ClientFileService.GetCustomerDocumentContent(document.Id);
                DataCacheUtility.SaveFileContentToDisk(info, fileSavePath);
            }
        }

        /// <summary>
        /// 打开保存文件对话框，获取文件保存路径，如果未选择，则返回空字符串
        /// </summary>
        /// <param name="document">文档信息</param>
        /// <returns></returns>
        string GetFileSavePath(DocumentInfo document)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = false;
            dialog.CheckPathExists = true;
            dialog.FileName = document.Name;
            dialog.OverwritePrompt = true;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;

            }
            else
                return string.Empty;

        }

        /// <summary>
        /// 弹出选择文件对话框
        /// </summary>
        /// <returns>选择的文件路径集合</returns>
        string[] SelectFilesToUpload()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Filter = GetFilterString();
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String[] filePaths = dialog.FileNames;
                return filePaths;
            }
            return null;
        }
        /// <summary>
        /// 获取筛字符串
        /// </summary>
        /// <returns></returns>
        string GetFilterString()
        {
            return string.Format(LocalData.IsEnglish ? "Customer Files{0}" : "客户文件{0}", GetFileExtensions());
        }
        /// <summary>
        /// 支持上传文件扩展名
        /// </summary>
        /// <returns></returns>
        string GetFileExtensions()
        {
            return "(*.txt,*.pdf,*.doc,*.docx,*.rtf,*.xls,*.xlsx,*.ppt,*.pptx,*.jpg,*.jpeg,*.gif,*.png,*.bmp,*.tif,*.tiff,*.msg,*.html,*.htm,*.mht)|*.txt;*.pdf;*.doc;*.docx;*.rtf;*.xls;*.xlsx;*.ppt;*.pptx;*.jpg;*.jpeg;*.gif;*.png;*.bmp;*.tif;*.msg;*.html;*.htm;*.mht";
        }

        /// <summary>
        /// 通过文档ID保存内容到磁盘
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>文档磁盘路径</returns>
        public String SaveContentToDisk(Guid id)
        {
            ContentInfo info = ClientFileService.GetCustomerDocumentContent(id);
            return DataCacheUtility.SaveFileContentToDisk(info);
        }
        /// <summary>
        /// 根据业务操作上下文绑定数据
        /// 获取文档列表
        /// </summary>
        /// <param name="context">业务操作上下文</param>
        public void BindData(BusinessOperationContext context)
        {
            if (context == null)
            {
                return;
            }
            if (PartDocumentList.Presenter == null)
            {
                PartDocumentList.Presenter = this;
            }
            WaitCallback callback = (data) =>
            {
                try
                {
                    if (PartDocumentList.IsHandleCreated)
                    {
                        BusinessOperationContext parameter = data as BusinessOperationContext;
                        var bindDelegate = new BindDataDelegate(InnerBindData);
                        PartDocumentList.BeginInvoke(bindDelegate, parameter);
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(PartDocumentList, ex);
                }
            };
            ThreadPool.QueueUserWorkItem(callback, context);
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="context">业务操作上下文</param>
        public void InnerBindData(BusinessOperationContext context)
        {
            BusinessContext = context;

            DocumentList = ClientFileService.GetCustomerDocumentList(context);
            //DocumentList = new List<DocumentInfo>();

        }
        
        #endregion

        #region IDisposable 成员
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
          
            BusinessContext = null;
         
            if (PartDocumentList != null)
            {
                PartDocumentList.Dispose();
                PartDocumentList = null;
            }
            
            WorkItem = null;

        }

        #endregion
    }
}

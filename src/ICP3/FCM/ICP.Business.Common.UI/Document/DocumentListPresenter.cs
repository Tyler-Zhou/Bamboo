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

namespace ICP.Business.Common.UI.Document
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
                if (ucList == null || ucList.DataSource == null)
                {
                    return new List<DocumentInfo>();
                }
                return ucList.DataSource;
            }
            set
            {
                if (ucList == null)
                {
                    return;
                }
                else
                {
                    ucList.DataSource = value;
                }
            }
        }

        /// <summary>
        /// 文档列表面板
        /// </summary>
        public UCDocumentList ucList
        {
            get;
            set;
        }

        /// <summary>
        /// 业务上下文
        /// </summary>
        public BusinessOperationContext BusinessContext { get; set; }
        /// <summary>
        /// 程序集名称
        /// </summary>
        private string AssamblyName
        {
            get
            {
                MethodBase methodother = MethodBase.GetCurrentMethod();
                if (methodother.DeclaringType != null)
                    return methodother.DeclaringType.FullName;
                return "ICP.Business.Common.UI";
            }
        } 

        /// <summary>
        /// 客户端上传文件服务
        /// </summary>
        public IClientFileService BusinessFileService
        {
            get
            {
                return ServiceClient.GetService<IClientFileService>();
            }
        }
        /// <summary>
        /// 文件操作服务
        /// </summary>
        public IFileService FileService
        {
            get
            {
                return ServiceClient.GetService<IFileService>();
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
        public DocumentListPresenter(UCDocumentList ucList)
        {
            this.ucList = null;
            this.ucList = ucList;
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
                    BusinessFileService.Reupload(documentLocalIds);
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
        /// <param name="context">业务操作上下文</param>
        /// <param name="documentType">文档类型</param>
        /// <returns>文档实体列表</returns>
        public DocumentInfo[] Upload(String[] filePaths, BusinessOperationContext context, DocumentType? documentType)
        {
            int count = filePaths.Length;
            _OperationLogID = Guid.NewGuid();
            Stopwatch stopwatch = Stopwatch.StartNew();
            StopwatchHelper.CustomRecordStopwatch(stopwatch, _OperationLogID, DateTime.Now, AssamblyName, "UPLOAD-FILE",
                string.Format("上传文件;OperationID[{0}]",context.OperationID));
            DocumentInfo[] documents = new DocumentInfo[count];
            string docIDs = "";
            for (int i = 0; i < count; i++)
            {
                documents[i] = CommonUIUtility.InitDocumentInfo(context, documentType);
                docIDs += "[" + documents[i].Id + "],";
                documents[i].Name = Path.GetFileName(filePaths[i]);
            }
            StopwatchHelper.CustomUpdateStopwatchLog(stopwatch, _OperationLogID, true, string.Format("上传文档ID列表{0}",docIDs));
            // AsyncEnumerator async = new AsyncEnumerator();
            //async.BeginExecute(InnerUpload(documents, filePaths), async.EndExecute);
            WaitCallback callback = data =>
            {
                try
                {
                    InnerUpload(documents, filePaths);
                }
                catch (Exception ex)
                {
                    //throw new ICPException(ex.Message);
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
                }
            };
            ThreadPool.QueueUserWorkItem(callback);
            StopwatchHelper.CustomUpdateStopwatchLog(stopwatch, _OperationLogID, false, string.Empty,"上传完成");
            return documents;
        }
        /// <summary>
        /// 上传文档到服务器
        /// </summary>
        /// <param name="documents">文档列表</param>
        /// <param name="filePaths"></param>
        private void InnerUpload(DocumentInfo[] documents, string[] filePaths)
        {
            BusinessFileService.Upload(documents, filePaths);
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
                BusinessFileService.Delete(documentIds, updateDates);
            };
            ThreadPool.QueueUserWorkItem(callback, ids);
        }
        /// <summary>
        /// 根据文档ID打开文件
        /// </summary>
        /// <param name="id">文档ID</param>
        public void Open(Guid id)
        {
            CommonUIUtility.OpenFile(id);
        }
        /// <summary>
        /// 根据文档ID打开文件
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <param name="dataSearchType">数据查询类型</param>
        public void Open(Guid id, DataSearchType dataSearchType)
        {
            CommonUIUtility.OpenFile(id, dataSearchType);
        }

        /// <summary>
        /// 根据文档ID以选择打开方式打开文件
        /// </summary>
        /// <param name="id">文档ID</param>
        public void OpenWith(Guid id)
        {
            CommonUIUtility.OpenWith(id);
        }
        /// <summary>
        /// 根据文档ID以选择打开方式打开文件
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <param name="dataSearchType">数据查询类型</param>
        public void OpenWith(Guid id, DataSearchType dataSearchType)
        {
            CommonUIUtility.OpenWith(id, dataSearchType);
        }
        /// <summary>
        /// 获取预览窗格位置及其窗体大小
        /// </summary>
        private void GetPositionAndSize()
        {
            if (!_isSet)
            {
                Screen scr = null;
                scr = Screen.FromPoint(ucList.Location);
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
            InnerPreview(id);
        }
        /// <summary>
        /// 根据文档ID预览文档
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <param name="dataSearchType">数据查询类型</param>
        public void Preview(Guid id, DataSearchType dataSearchType)
        {
            InnerPreview(id, dataSearchType);
        }

        /// <summary>
        /// 根据文档ID预览文档
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <param name="dataSearchType">数据查询类型</param>
        private void InnerPreview(Guid id, DataSearchType dataSearchType=DataSearchType.Local)
        {
            String filePath = string.Empty;
            try
            {
                if (id != _previousFileId)
                {
                    ContentInfo info = BusinessFileService.GetDocumentHtmlContent(id, dataSearchType);
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
                LocalCommonServices.ErrorTrace.SetErrorInfo(ucList, ex);
            }

        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="document"></param>
        public void Download(DocumentInfo document)
        {
            CommonUIUtility.DownLoad(document);
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="document"></param>
        /// <param name="dataSearchType"></param>
        public void Download(DocumentInfo document,DataSearchType dataSearchType)
        {
            CommonUIUtility.DownLoad(document, dataSearchType);
        }
        /// <summary>
        /// 通过文档ID保存内容到磁盘
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>文档磁盘路径</returns>
        public String SaveContentToDisk(Guid id)
        {
            ContentInfo info = BusinessFileService.GetDocumentContent(id);
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
            if (ucList.Presenter == null)
            {
                ucList.Presenter = this;
            }
            WaitCallback callback = (data) =>
            {
                try
                {
                    if (ucList.IsHandleCreated)
                    {
                        BusinessOperationContext parameter = data as BusinessOperationContext;
                        var bindDelegate = new BindDataDelegate(InnerBindData);
                        ucList.BeginInvoke(bindDelegate, parameter);
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(ucList, ex);
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

            DocumentList = BusinessFileService.GetBusinessDocumentList(context);

        }

        /// <summary>
        /// 根据业务操作上下文及其分发记录ID绑定分发文档列表
        /// </summary>
        /// <param name="context">业务操作上下文</param>
        /// <param name="FileLogsID">分发记录ID</param>
        public void InnerBindDataNew(BusinessOperationContext context, Guid FileLogsID)
        {
            BusinessContext = context;
            DocumentList = FileService.GetDispatchFiles(FileLogsID);
        }
        #endregion

        #region IDisposable 成员
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
          
            BusinessContext = null;
         
            if (ucList != null)
            {
                ucList.Dispose();
                ucList = null;
            }
            
            WorkItem = null;

        }

        #endregion

        #region Comment Code
        //public DocumentInfo Upload(String filePath,BusinessOperationContext context)
        //{
        //    DocumentInfo info = InitDocumentInfo(context);
        //    BusinessFileService.Upload(info, filePath);
        //    return info;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Synchorize(BusinessOperationContext context)
        {

        }
        #endregion
    }
}

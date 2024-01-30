using System;
using System.Collections.Generic;
using System.Linq;
using ICP.DataCache.ServiceInterface;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Wintellect.Threading.AsyncProgModel;
using System.IO;
using ICP.Framework.CommonLibrary.Common;
using System.Threading;
using ICP.Framework.CommonLibrary.Helper;
using ICP.DataCache.FileSystem;
using GhostscriptSharp;
using System.ServiceModel;
using ICP.Common.ServiceInterface;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using ICP.Framework.CommonLibrary.Server;
using ICP.Framework.CommonLibrary;

namespace ICP.DataCache.BusinessOperation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class ClientFileService : IClientFileService
    {

        /// <summary>
        /// 系统错误日志服务
        /// </summary>
        public ISystemErrorLogService SystemErrorLogService
        {
            get { return ServiceClient.GetService<ISystemErrorLogService>(); }
        }

        bool isEnglish = LocalData.IsEnglish;

        public static List<UpLoadInfo> UpLoadInfoList = new List<UpLoadInfo>();
        public List<string> TempFilePath = new List<string>();

        //合并文件流缓存文件路劲
        string FilePathTemp = string.Empty;

        /// <summary>
        /// 上传文件本地使用信息
        /// </summary>
        public class UpLoadInfo
        {
            /// <summary>
            /// 文件信息对象（不含文件流部分）
            /// </summary>
            public DocumentInfo documentinfo { set; get; }
            /// <summary>
            /// 总的文件尺寸
            /// </summary>
            public int FileSize { get; set; }
            /// <summary>
            /// 是否已上传完毕
            /// </summary>
            public bool UploadOver { get; set; }
            /// <summary>
            /// 上传进度
            /// </summary>
            public decimal Progess { get; set; }
            /// <summary>
            /// 上传文件流第一部分流大小
            /// </summary>
            public int FirstFileStreamSize { get; set; }
            /// <summary>
            /// 客户端文件合并流缓存路径
            /// </summary>
            public string FileTempStorePath { get; set; }
            /// <summary>
            /// 上传合并文件流
            /// </summary>
            public Stream MergeStream { get; set; }
            /// <summary>
            /// 上传文件流
            /// </summary>
            public DocumentStream uploaddocumentstream { get; set; }

        }


        public ILocalBusinessCacheDataOperation LocalOperation
        {
            get
            {
                return ServiceClient.GetClientService<ILocalBusinessCacheDataOperation>();
            }
        }

        public IFileService FileService
        {
            get
            {
                return ServiceClient.GetService<IFileService>();
            }
        }

        public IFileSystemService FileServiceWCF
        {
            get
            {
                return ServiceClient.GetService<IFileSystemService>();
            }
        }

        public IFileStoreOperation FileStoreOperation
        {
            get
            {
                return ServiceClient.GetClientService<IFileStoreOperation>();
            }
        }

        public ConverterPreviewFactory PreviewFactory
        {
            get
            {
                return ClientHelper.Get<ConverterPreviewFactory, ConverterPreviewFactory>();

            }
        }
        public DocumentNotifyClientService DocumentNotifyService
        {
            get
            {
                return ServiceClient.GetClientService<DocumentNotifyClientService>();
            }
        }

        /// <summary>
        /// 下载PDF副本来预览
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContentInfo GetDocumentHtmlContent(Guid id)
        {
            return GetDocumentHtmlContent(id, DataSearchType.Local);
        }

        /// <summary>
        /// 下载PDF副本来预览
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataSearchType">数据查询类型</param>
        /// <returns></returns>
        public ContentInfo GetDocumentHtmlContent(Guid id, DataSearchType dataSearchType)
        {
            bool downloadFromServer = false;
            ContentInfo info = new ContentInfo();
            try
            {
                info = LocalOperation.GetDocumentHtmlContent(id);
                if (info == null)
                {
                    info = new ContentInfo();
                }
                if (info.Content == null)
                {
                    if(FileServiceWCF==null)
                    {
                        throw new Exception(LocalData.IsEnglish ? "Not connected to document service" : "未连接到文档服务");
                    }
                    //obama modify
                    //info = FileService.GetDocumentHtmlContent(id);

                    //obama add 
                    //传输对象参数
                    DocumentStream infoData = new DocumentStream();
                    infoData.Id = id;
                    infoData.IsDownCopy = true;
                    infoData.DataSearchTypeCode = dataSearchType;
                    byte[] Contentbyte = new byte[1];
                    infoData.Content = new MemoryStream(Contentbyte);//字节转换为流，WCF消息契约中Body不能为空
                    //返回文件对象信息
                    DocumentStream inforeturn = FileServiceWCF.ServiceTransferFileToClint(infoData);
                    if (inforeturn == null || inforeturn.Content == null)
                    {
                        throw new NullReferenceException(String.Format(isEnglish ?
                            "Download the file failed, please contact IT Department." :
                            "下载文件失败，请联系IT部门。", id));
                    }
                    info.Id = id;
                    info.Name = inforeturn.Name;
                    info.OperationID = inforeturn.OperationID;
                    Stream DataCacheStream = new FileStream(inforeturn.Name, FileMode.Create, FileAccess.Write, FileShare.Write);
                    //定义文件缓冲区
                    const int bufferLen = 1024 * 10;//10KB
                    byte[] buffer = new byte[bufferLen];
                    int count = 0;
                    while ((count = inforeturn.Content.Read(buffer, 0, bufferLen)) > 0)
                    {
                        DataCacheStream.Write(buffer, 0, count);
                    }
                    DataCacheStream.Dispose();
                    //流转换为字节
                    Stream ContentStream = new FileStream(inforeturn.Name, FileMode.Open, FileAccess.Read, FileShare.Read);
                    byte[] ContentFromStream = ArrayHelper.ReadAllBytesFromStream(ContentStream);
                    ContentStream.Dispose();

                    if (File.Exists(inforeturn.Name))
                        File.Delete(inforeturn.Name);

                    info.Content = ContentFromStream;
                    //obama add end

                    downloadFromServer = true;
                }
                if (info == null || info.Content == null)
                {
                    throw new NullReferenceException(String.Format(isEnglish ? "Download the file failed, please contact IT Department." : "下载文件失败，请联系IT部门。", id));
                }
                //如果客户端缓存不存在，则将下载的数据保存到本地
                if (downloadFromServer)
                {
                    //AsyncEnumerator async = new AsyncEnumerator();
                    //async.BeginExecute(Save(async,info,ContentType.Html), async.EndExecute);
                    LocalOperation.SaveHtmlDocument(info);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return info;
        }
        public List<ContentInfo> GetDocumentCopyContents(List<Guid> ids)
        {
            List<ContentInfo> localCopies = LocalOperation.GetDocumentCopyContent(ids);
            List<Guid> localExistsIds = (from copy in localCopies
                                         select copy.Id).ToList();
            List<Guid> localMissedIds = ids.Except(localExistsIds).ToList();
            List<ContentInfo> remoteCopies = new List<ContentInfo>();
            List<Guid> remoteIds = new List<Guid>();
            if (localMissedIds.Count > 0)
            {
                remoteCopies = FileService.GetDocumentCopyContents(localMissedIds);
                foreach (ContentInfo copy in remoteCopies)
                {
                    remoteIds.Add(copy.Id);
                    LocalOperation.SaveHtmlDocument(copy);
                }
            }
            List<Guid> totalMissingIds = ids.Except(localExistsIds.Union(remoteIds)).ToList();
            if (totalMissingIds.Count > 0)
            {
                throw new ICPException(string.Format("Failed to get document copies of {0} failed", totalMissingIds.ToArray().Join()));
            }
            return localCopies.Union(remoteCopies).ToList();

        }
        private void TriggerUploadStateChangeEvent(List<Guid> documentIds, UploadState state)
        {
            if (DocumentNotifyService.DocumentStateChanged_New != null)
            {
                DocumentNotifyService.DocumentStateChanged_New(documentIds, state);
            }
        }

        private void TriggerUploadProgressChangeEvent(DocumentInfo[] documents, decimal[] progress)
        {
            if (DocumentNotifyService.DocumentUploadProgress != null)
            {
                DocumentNotifyService.DocumentUploadProgress(documents, progress);
            }
        }
        /// <summary>
        /// OA文件上传通知事件
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="progress"></param>
        private void TriggerUploadProgressChangeEventOfOaFile(DocumentInfo[] documents, decimal[] progress)
        {
            if (DocumentNotifyService.DocumentUploadProgressOfOaFile != null)
            {
                DocumentNotifyService.DocumentUploadProgressOfOaFile(documents, progress);
            }
        }

        private void TriggerUploadUploadSucessedEvent(DocumentInfo[] documents)
        {
            if (DocumentNotifyService.DocumentUploadSucessed_New != null)
            {
                DocumentNotifyService.DocumentUploadSucessed_New(documents);
            }
        }

        public void Upload(DocumentInfo[] documents, String[] filePaths)
        {
            string exceptionstr = string.Empty;
            try
            {
                int count = filePaths.Length;
                decimal[] progressValue = new decimal[count];
                exceptionstr += "判断文件是否还在上传中";
                //判断文件是否还在上传中（保存于DocumentMemoryCache.documentMemoryCache字典中）
                for (int i = 0; i < count; i++)
                {
                    if (DocumentMemoryCache.FindFileName(documents[i].OperationID, documents[i].Name) != null)
                    {
                        String errorMessage = isEnglish ? string.Format(
                            "Documents:{0} is being uploaded, please do not upload repeatedly .",
                            documents[i].Name) : string.Format("文档:{0}正在上传中，请不要重复上传。", documents[i].Name);
                        throw new ICPException(errorMessage);
                    }
                }

                //判断重名覆盖
                //验证当前业务下是否包含传入的文件名集合
                List<DocumentInfo> documentlist = BusinessOperationHelper.IsExistFileNames(new List<DocumentInfo>(documents));

                for (int i = 0; i < count; i++)
                {
                    String filePath = filePaths[i];
                    String fileName = Path.GetFileName(filePath);
                    string fileExtension = filePath.GetExtension();
                    bool isNeedBackupPDF = !(documents[i].DocumentType == DocumentType.NRAS
                                           && !string.IsNullOrEmpty(fileExtension)
                                           && ".MSG".Equals(fileExtension.ToUpper()));
                    documents[i].State = UploadState.LocalProcessing;
                    TriggerUploadStateChangeEvent(new List<Guid> { documents[i].Id }, UploadState.LocalProcessing);
                    documents[i].Content = IOHelper.ReadFileContentFromDisk(filePath);
                    documents[i].HtmlContent = isNeedBackupPDF ? PreviewFactory.GetConvertedContent(filePath) : documents[i].Content;

                    documents[i].Name = fileName;
                    documents[i].FileSources = FileSource.FDocument;
                    documents[i].State = UploadState.LocalSaving;
                    documents[i].UpdateBy = LocalData.UserInfo.LoginID;
                    documents[i].OriginalPath = filePaths[i];

                    TriggerUploadStateChangeEvent(new List<Guid> { documents[i].Id }, UploadState.LocalSaving);
                    LocalOperation.SaveDocument(documents[i]);
                    documents[i].State = UploadState.LocalSaved;
                    TriggerUploadStateChangeEvent(new List<Guid> { documents[i].Id }, UploadState.LocalSaved);
                    DocumentMemoryCache.Add(documents[i]);
                }

                #region  //打印保存文件列表覆盖
                if (documentlist != null && documentlist.Count > 0)
                {
                    DocumentInfo[] _documents = new DocumentInfo[documentlist.Count];
                    List<Guid> deleteids = new List<Guid>();
                    List<DateTime?> deleteUpdates = new List<DateTime?>();
                    Guid userID = documents[0].CreateBy;

                    int j = 0;
                    documentlist.ForEach(i =>
                    {
                        _documents[j] = i;
                        _documents[j].Content = null;
                        _documents[j].HtmlContent = null;
                        j++;
                        deleteids.Add(i.Id);
                        deleteUpdates.Add(i.UpdateDate);
                    });
                    //删除本地缓存文件
                    LocalOperation.DeleteDocument(deleteids);
                    DocumentMemoryCache.Remove(deleteids);
                    //obama modify
                    //FileService.Save(documents.ToList(), deleteids, deleteUpdates);
                    //obama add
                    try
                    {
                        //文件覆盖之前，先把服务器数据库里的同名文件删除
                        FileServiceWCF.DeleteFileBeforeSave(deleteids, deleteUpdates, userID);
                    }
                    catch (Exception ex)
                    {
                        string exc = ex.Message;
                        throw ex;
                    }
                    //覆盖文件
                    InnerUpLoad(documents);
                    //obama add end
                }
                else if (documentlist == null)
                {
                    DocumentMemoryCache.Remove(documents);
                    return;
                }
                else
                {
                    InnerUpLoad(documents);
                }
                #endregion
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 轮训线程
        /// </summary>
        Thread ShowProgressTd = null;
        /// <summary>
        /// 轮训线程
        /// </summary>
        ThreadStart tdsart = null;
        /// <summary>
        /// 正在轮训？
        /// </summary>
        public bool IsCheck = false;

        private void InnerUpLoad(DocumentInfo[] documents)
        {
            List<UpLoadInfo> UpLoadInfoListTemp = new List<UpLoadInfo>();
            int count = documents.Length;
            List<Guid> ids = new List<Guid>();
            string strIDs = string.Empty;
            DocumentInfo[] info = documents;
            if (info == null) return;
            Array.ForEach(info, document =>
            {
                strIDs = "[" + document.Id + "]";
                ids.Add(document.Id);
            });
            TriggerUploadStateChangeEvent(ids, UploadState.Uploading);
            ClientHelper.SetApplicationContext();
            #region 上传文件流

            #region  //合并上传文件流
            FilePathTemp = AppDomain.CurrentDomain.BaseDirectory;
            if (!FilePathTemp.EndsWith("\\"))
            {
                FilePathTemp += "\\";
            }
            FilePathTemp += "filetemp\\";
            if (!Directory.Exists(FilePathTemp))
            {
                Directory.CreateDirectory(FilePathTemp);
            }
            try
            {
                DocumentInfo SendFileDocumentInfo = null;
                for (int i = 0; i < count; i++)
                {
                    UpLoadInfo UploadInfoTemp = new UpLoadInfo();
                    UploadInfoTemp.documentinfo = info[i];
                    UploadInfoTemp.UploadOver = false;
                    SendFileDocumentInfo = info[i];
                    //运用程序目录下创建合并文件流（有则覆盖）
                    try
                    {
                        UploadInfoTemp.MergeStream = new FileStream(FilePathTemp + SendFileDocumentInfo.Name, FileMode.Create,
                           FileAccess.Write, FileShare.ReadWrite);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //字节转换为文件流
                    UploadInfoTemp.FileTempStorePath = SendFileDocumentInfo.Name;
                    Stream UploadStreamContent = new MemoryStream(SendFileDocumentInfo.Content);
                    UploadInfoTemp.FirstFileStreamSize = (int)UploadStreamContent.Length;
                    Stream UploadStreamHtmlContent = new MemoryStream(SendFileDocumentInfo.HtmlContent);
                    //合并文件流
                    byte[] bytes = new byte[1024 * 10];//10K
                    int readfilecount;
                    while (true)
                    {
                        readfilecount = UploadStreamContent.Read(bytes, 0, bytes.Length);
                        if (readfilecount > 0)
                        {
                            UploadInfoTemp.MergeStream.Write(bytes, 0, readfilecount);
                        }
                        else
                        {
                            UploadStreamContent.Dispose();
                            break;
                        }
                    }
                    //移动目标文件指针位置到末尾
                    UploadInfoTemp.MergeStream.Seek(0, SeekOrigin.End);
                    while (true)
                    {
                        readfilecount = UploadStreamHtmlContent.Read(bytes, 0, bytes.Length);
                        if (readfilecount > 0)
                        {
                            UploadInfoTemp.MergeStream.Write(bytes, 0, readfilecount);
                        }
                        else
                        {
                            UploadStreamHtmlContent.Dispose();
                            break;
                        }
                    }
                    UploadInfoTemp.FileSize = (int)UploadInfoTemp.MergeStream.Length;
                    UploadInfoTemp.MergeStream.Close();
                    UploadInfoTemp.MergeStream.Dispose();
                    UploadInfoTemp.MergeStream = null;

                    //初始化传输对象
                    DocumentStream uploaddocumentstream = new DocumentStream();
                    //使用Id来作为文件名，使用Name来获取文件扩展名
                    uploaddocumentstream.Id = SendFileDocumentInfo.Id;
                    uploaddocumentstream.Name = SendFileDocumentInfo.Name;
                    uploaddocumentstream.IncludePDF = true;
                    uploaddocumentstream.FirstFileStreamSize = UploadInfoTemp.FirstFileStreamSize;
                    uploaddocumentstream.Content = new FileStream(FilePathTemp + SendFileDocumentInfo.Name, FileMode.Open,
                            FileAccess.Read, FileShare.ReadWrite);
                    UploadInfoTemp.uploaddocumentstream = uploaddocumentstream;
                    UploadInfoTemp.MergeStream = uploaddocumentstream.Content;
                    //记录文件信息
                    UpLoadInfoListTemp.Add(UploadInfoTemp);
                    TempFilePath.Add(UploadInfoTemp.FileTempStorePath);
                }
                UpLoadInfoList = UpLoadInfoList.Concat(UpLoadInfoListTemp).ToList();
            #endregion
                //通知进度(使用线程)
                if (!IsCheck)
                {
                    ShowProgressTd = new Thread(new ThreadStart(_CkeckUploadFileProgress));
                    ShowProgressTd.Start();
                }
                #region  //轮流上传文件
                for (int i = 0; i < count; i++)
                {
                    //发送文件流至服务器临时目录
                    Stopwatch StopwatchStep;
                    string LogState = string.Empty;
                    string CreateByName = UpLoadInfoListTemp[i].documentinfo.CreateByName;
                    string FileName = UpLoadInfoListTemp[i].documentinfo.Name;
                    try
                    {
                        //开始记录时间日志
                        StopwatchStep = StopwatchHelper.StartStopwatch();
                        FileServiceWCF.UploadOperationFileByStream(UpLoadInfoListTemp[i].uploaddocumentstream);
                        //记录日志
                        LogState = string.Format("{0}:  [{1}]:  {2}K  :服务器", CreateByName, FileName,
                            (UpLoadInfoListTemp[i].uploaddocumentstream.Content.Length / 1024F).ToString("0.00"));
                        Guid _OtherPartLogID = Guid.NewGuid();
                        MethodBase methodother = MethodBase.GetCurrentMethod();
                        StopwatchHelper.CustomRecordStopwatch(StopwatchStep, _OtherPartLogID,
                            DateTime.Now, methodother.DeclaringType.FullName, "WcfUploadFile", LogState);
                    }
                    catch (Exception ex)
                    {
                        LogState = string.Format("{0}上传文件[{1}]失败，失败原因为:", CreateByName, FileName);
                        string exceptionstr = LogState + ex.Message;
                        SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                            LocalData.SessionId, new byte[0], exceptionstr,
                            DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                    }
                    //从临时目录读取文件流存入数据库
                    try
                    {
                        StopwatchStep = StopwatchHelper.StartStopwatch();
                        FileServiceWCF.UploadOperationFileByInfo(UpLoadInfoListTemp[i].documentinfo);
                        UpLoadInfoListTemp[i].UploadOver = true;
                        //记录日志
                        LogState = string.Format("{0}:  [{1}]  :数据库", CreateByName, FileName);
                        Guid _OtherPartLogID = Guid.NewGuid();
                        MethodBase methodother = MethodBase.GetCurrentMethod();
                        StopwatchHelper.CustomRecordStopwatch(StopwatchStep, _OtherPartLogID,
                            DateTime.Now, methodother.DeclaringType.FullName, "WcfUploadFile", LogState);
                    }
                    catch (Exception ex)
                    {
                        LogState = string.Format("{0}上传文件[{1}]失败，失败原因为:", CreateByName, FileName);
                        string exceptionstr = LogState + ex.Message;
                        SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                            LocalData.SessionId, new byte[0], exceptionstr,
                            DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                    }
                    //单个文件上传完成
                    //Thread.Sleep(2000);
                    DocumentMemoryCache.Remove(UpLoadInfoListTemp[i].documentinfo);
                }
                #endregion
            #endregion
            }
            catch (Exception ex)
            {
                if (ShowProgressTd != null) { ShowProgressTd.Abort(); }
                //WriteLog(ex.Message + "\r\n");
                string exceptionstr = "InnerUpLoad上传文件失败，失败原因为:" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }

        /// <summary>
        /// 轮训检查文件上传进度
        /// </summary>
        public void _CkeckUploadFileProgress()
        {
            string ExceptionString = string.Empty; Thread.Sleep(250);
            IsCheck = true;
            List<DocumentInfo> documentsList = new List<DocumentInfo>();
            List<decimal> progressList = new List<decimal>();
            try
            {
            Start:
                #region //检查上传进度
                double UploadSize = 0;
                //获取已上传字节数 
                foreach (UpLoadInfo obj in UpLoadInfoList)
                {
                    if (obj.MergeStream == null)
                        UploadSize = 0;
                    else
                        UploadSize = obj.MergeStream.Position;
                    int filesize = obj.FileSize;
                    decimal Progress = (decimal)(UploadSize / obj.FileSize) * 100;
                    if (Progress == 100 && obj.UploadOver == false)
                        Progress = (decimal)99.99;
                    progressList.Add(Progress);
                    documentsList.Add(obj.documentinfo);
                }
                //通知显示窗体
                TriggerUploadProgressChangeEvent(documentsList.ToArray(), progressList.ToArray());

                //检测列表中有完成的选项就删除对应文件
                for (int i = 0; i < UpLoadInfoList.Count; i++)
                {
                    if (UpLoadInfoList[i].UploadOver == true)
                    {
                        TriggerUploadProgressChangeEvent(new DocumentInfo[] { UpLoadInfoList[i].documentinfo }, new decimal[] { 100 });
                        TriggerUploadUploadSucessedEvent(new DocumentInfo[] { UpLoadInfoList[i].documentinfo });
                        UpLoadInfoList[i].MergeStream.Close();
                        File.Delete(FilePathTemp + UpLoadInfoList[i].FileTempStorePath);
                        UpLoadInfoList.RemoveAt(i);
                        i--;
                        Thread.Sleep(500);
                    }
                }
                //有没有全部上传完成，若还有文件还没完成则重新轮训
                //decimal OverValue = progressList.Average();
                //是否上传文件流完成
                if (UpLoadInfoList.Count != 0)
                {
                    //每隔800毫秒轮训一次
                    Thread.Sleep(250);
                    documentsList.Clear();
                    progressList.Clear();
                    goto Start;
                }
                IsCheck = false;
                #endregion
            }
            catch (Exception ex)
            {

                UpLoadInfoList.Clear();
                documentsList.Clear();
                progressList.Clear();
                WriteLog("_CkeckUploadFileProgres\r\n" + ExceptionString + ex.Message);
                string LogState = string.Format("{0}上传文件失败在[{1}]，失败原因为:", "",
                 "_CkeckUploadFileProgress");
                string exceptionstr = LogState + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                IsCheck = false;
            }
        }
        string LogPath = null;
        public DateTime dt = DateTime.Now;
        public readonly object SYNC = new object();
        /// <summary>
        /// 写日志文件
        /// </summary>
        /// <param name="Log"></param>
        public void WriteLog(string Log)
        {
            lock (SYNC)
            {
                try
                {
                    if (String.IsNullOrEmpty(LogPath))
                    {
                        LogPath = AppDomain.CurrentDomain.BaseDirectory;
                        if (!LogPath.EndsWith("\\"))
                        {
                            LogPath += "\\";
                        }
                        LogPath += "LogFiles\\";
                        if (!Directory.Exists(LogPath))
                        {
                            Directory.CreateDirectory(LogPath);
                        }
                        LogPath += "WcfMailCenter" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + ".txt";
                        StreamWriter sw = new StreamWriter(LogPath, true, Encoding.UTF8);
                        sw.WriteLine(Log);
                        sw.Close();
                    }
                    else
                    {
                        StreamWriter sw = new StreamWriter(LogPath, true, Encoding.UTF8);
                        sw.WriteLine(Log);
                        sw.Close();
                    }
                }
                catch (Exception ex) { throw ex; }
            }
        }

        private IEnumerator<Int32> Save(AsyncEnumerator ae, ContentInfo info, ContentType type)
        {
            if (type == ContentType.Html)
            {
                LocalOperation.SaveHtmlDocument(info);
            }
            else if (type == ContentType.Content)
            {
                LocalOperation.SaveDocumentContent(info);
            }
            yield return 1;
        }
        public List<ContentInfo> GetDocumentContents(List<Guid> ids)
        {
            throw new ICPException("此方法暂未实现");
        }

        public ContentInfo GetDocumentContent(Guid id)
        {
            return GetDocumentContent(id, DataSearchType.Local);
        }

        public ContentInfo GetDocumentContent(Guid id, DataSearchType dataSearchType)
        {
            ContentInfo info = new ContentInfo();
            try
            {
                bool downloadFromServer = false;
                //先查询本地数据库中有没有，没有则从服务器下载
                info = LocalOperation.GetDocumentContent(id);
                if (info == null)
                {
                    info = new ContentInfo();
                }
                if (info.Content == null)
                {
                    //obama Modify
                    //info = FileService.GetDocumentContent(id);

                    //obama add 
                    //传输对象参数
                    DocumentStream infoData = new DocumentStream();
                    infoData.Id = id;
                    infoData.IsDownCopy = false;
                    infoData.DataSearchTypeCode = dataSearchType;
                    byte[] Contentbyte = new byte[1];
                    infoData.Content = new MemoryStream(Contentbyte);//字节转换为流，WCF消息契约中Body不能为空
                    //返回文件对象信息
                    DocumentStream inforeturn = FileServiceWCF.ServiceTransferFileToClint(infoData);
                    if (inforeturn == null || inforeturn.Content == null)
                    {
                        throw new NullReferenceException(String.Format(isEnglish ?
                            "Download the file failed, please contact IT Department." :
                            "下载文件失败，请联系IT部门。", id));
                    }
                    info.Id = id;
                    info.Name = inforeturn.Name;
                    info.OperationID = inforeturn.OperationID;
                    Stream DataCacheStream = new FileStream(inforeturn.Name, FileMode.Create, FileAccess.Write, FileShare.Write);
                    //定义文件缓冲区
                    const int bufferLen = 1024 * 10;//10KB
                    byte[] buffer = new byte[bufferLen];
                    int count = 0;
                    while ((count = inforeturn.Content.Read(buffer, 0, bufferLen)) > 0)
                    {
                        DataCacheStream.Write(buffer, 0, count);
                    }
                    DataCacheStream.Dispose();
                    //流转换为字节
                    Stream ContentStream = new FileStream(inforeturn.Name, FileMode.Open, FileAccess.Read, FileShare.Read);
                    byte[] ContentFromStream = ArrayHelper.ReadAllBytesFromStream(ContentStream);
                    ContentStream.Dispose();

                    if (File.Exists(inforeturn.Name))
                        File.Delete(inforeturn.Name);

                    info.Content = ContentFromStream;
                    //obama add end

                    downloadFromServer = true;
                }
                if (info == null || info.Content == null)
                {
                    throw new NullReferenceException(String.Format(isEnglish ? "Download File: {0} failed." : "下载文件: {0}失败。", id));
                }
                //如果客户端缓存不存在，则将下载的数据保存到本地
                if (downloadFromServer)
                {
                    //AsyncEnumerator async = new AsyncEnumerator();
                    //async.BeginExecute(Save(async, info,ContentType.Content), async.EndExecute);
                    LocalOperation.SaveDocumentContent(info);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return info;
        }

        public void Delete(List<Guid> ids, List<DateTime?> updateDates)
        {
            try
            {
                AsyncEnumerator async = new AsyncEnumerator();
                async.BeginExecute(DeleteDocument(async, ids, updateDates), async.EndExecute);
            }
            catch (Exception ex)
            {
                throw new ICPException(ex.Message);
            }
        }

        public void Delete(List<string> fileNames, Guid operationID)
        {
            AsyncEnumerator async = new AsyncEnumerator();
            async.BeginExecute(DeleteDocument(async, fileNames, operationID), async.EndExecute);
        }

        private IEnumerator<Int32> DeleteDocument(AsyncEnumerator ae, List<string> fileNames, Guid operationID)
        {

            FileService.Delete(fileNames, operationID);
            yield return 1;
        }


        private IEnumerator<Int32> DeleteDocument(AsyncEnumerator ae, List<Guid> ids, List<DateTime?> updateDates)
        {
            LocalOperation.DeleteDocument(ids);
            DocumentMemoryCache.Remove(ids);
            //yield return 1;
            FileService.Delete(ids, updateDates);
            yield return 1;
        }

        public string GenerateThumbImages(List<Guid> documentIds, Guid faxId)
        {
            List<ContentInfo> contentCopies = LocalOperation.GetDocumentCopyContent(documentIds);
            string basePath = Path.Combine(IOHelper.ThumbImageRootPath, faxId.ToString());
            if (Directory.Exists(basePath))
            {
                Directory.Delete(basePath, true);
            }
            DataCacheUtility.SaveFileContentToDisk(contentCopies, basePath);
            List<string> pdfFileNames = (from copy in contentCopies
                                         select copy.Name).ToList();
            List<string> pdfFileFullPaths = new List<string>();
            pdfFileNames.ForEach(fileName => pdfFileFullPaths.Add(Path.Combine(basePath, fileName)));

            GhostscriptSettings setting = GetGhostscriptSettings();
            string outputFileName;

            foreach (string pdfFile in pdfFileFullPaths)
            {
                outputFileName = string.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(pdfFile), "%d", ".jpg");
                GhostscriptWrapper.GenerateOutput(pdfFile, outputFileName, setting);
            }
            contentCopies = null;
            return basePath;
        }

        private static GhostscriptSettings GetGhostscriptSettings()
        {
            GhostscriptSettings setting = new GhostscriptSettings();
            setting.Device = GhostscriptSharp.Settings.GhostscriptDevices.jpeg;
            GhostscriptSharp.Settings.GhostscriptPages pages = new GhostscriptSharp.Settings.GhostscriptPages();
            pages.AllPages = true;
            setting.Page = pages;
            setting.Resolution = new System.Drawing.Size(96, 96);
            setting.Size = new GhostscriptSharp.Settings.GhostscriptPageSize { Manual = new System.Drawing.Size(96, 96) };
            return setting;
        }

        public bool Accepted(AgentDispatchParam param)
        {
            try
            {
                return FileService.Accepted(param);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AssignTo(AgentDispatchParam param)
        {
            try
            {
                return FileService.AssignTo(param);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region IClientFileService 成员

        public void Upload(DocumentInfo document, String filePath)
        {
            Upload(new DocumentInfo[] { document }, new string[] { filePath });
        }

        public String SaveHtmlContentToDisk(Guid id)
        {
            ContentInfo info = GetDocumentHtmlContent(id);
            return DataCacheUtility.SaveFileContentToDisk(info);
        }

        /// <summary>
        /// 获取业务下的文档
        /// </summary>
        /// <param name="context">业务操作上下文类</param>
        /// <returns></returns>
        public List<DocumentInfo> GetBusinessDocumentList(BusinessOperationContext context)
        {
            //return LocalOperation.GetBusinessDocumentList(context);
            List<DocumentInfo> docList = new List<DocumentInfo>();
            docList.AddRange(FileService.GetBusinessDocumentList(context));
            List<Guid> docIdlist = new List<Guid>();
            docList.ForEach(e => docIdlist.Add(e.Id));

            List<DocumentInfo> docCache = DocumentMemoryCache.FindOperationDocuments(context.OperationID);
            List<DocumentInfo> docListCache = new List<DocumentInfo>();
            if (docCache != null)
            {
                docListCache.AddRange(docCache.Where(item => !docIdlist.Contains(item.Id)));
                docList.AddRange(docListCache);
            }
            return docList;
        }
        /// <summary>
        /// 获取业务下的文档
        /// </summary>
        /// <param name="context">业务操作上下文类</param>
        /// <param name="dataSearchType">数据查询类型</param>
        /// <returns></returns>
        public List<DocumentInfo> GetBusinessDocumentList(BusinessOperationContext context, DataSearchType dataSearchType)
        {
            List<DocumentInfo> docList = new List<DocumentInfo>();
            docList.AddRange(FileService.GetBusinessDocumentList(context, dataSearchType));
            List<Guid> docIdlist = new List<Guid>();
            docList.ForEach(e => docIdlist.Add(e.Id));

            List<DocumentInfo> docCache = DocumentMemoryCache.FindOperationDocuments(context.OperationID);
            List<DocumentInfo> docListCache = new List<DocumentInfo>();
            if (docCache != null)
            {
                docListCache.AddRange(docCache.Where(item => !docIdlist.Contains(item.Id)));
                docList.AddRange(docListCache);
            }
            return docList;
        }


        public List<DocumentInfo> GetBusinessDocumentList(List<BusinessOperationContext> contextlist)
        {
            List<DocumentInfo> docList = new List<DocumentInfo>();
            docList.AddRange(FileService.GetBusinessDocumentList(contextlist));
            return docList;
        }


        public void Reupload(List<Guid> documentLocalIds)
        {
            List<DocumentInfo> documents = new List<DocumentInfo>();
            foreach (Guid id in documentLocalIds)
            {
                DocumentInfo document = LocalOperation.GetDocumentDetailInfo(id);
                documents.Add(document);
            }
            InnerUpLoad(documents.ToArray());

        }
        public void SetDispatchState(DispatchStateParam param)
        {
            try
            {
                FileService.Dispatch(param);
            }
            catch (Exception ex)
            {
                String errorMessageTemplate = isEnglish ? "File dispatch failed.because of :{0}" : "文件分发失败，由于:{0}";
                throw new ICPException(String.Format(errorMessageTemplate, ex.Message));
            }
        }
        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="newDocuments">新上传的文档</param>
        /// <param name="filePaths">新上传文档的路径</param>
        /// <param name="listDeleteIds">需删除文档的Id列表</param>
        /// <param name="updateDates">需删除文档的更新时间列表</param>
        /// <returns></returns>
        public void Save(List<DocumentInfo> newDocuments, List<string> newfilePaths, List<Guid> listDeleteIds, List<DateTime?> updateDates)
        {
            SaveEntity entity = new SaveEntity(newDocuments, newfilePaths, listDeleteIds, updateDates);

            List<DocumentInfo> documentlist = BusinessOperationHelper.IsExistFileNames(new List<DocumentInfo>(entity.NewDocuments), listDeleteIds);

            DocumentInfo[] documents = entity.NewDocuments.ToArray();

            int count = documents.Length;
            for (int i = 0; i < count; i++)
            {
                String filePath = newfilePaths == null ? documents[i].OriginalPath : newfilePaths[i];

                if (documents[i].Content == null)
                {
                    documents[i].Content = IOHelper.ReadFileContentFromDisk(filePath);
                }
                if (documents[i].HtmlContent == null)
                {
                    documents[i].HtmlContent = PreviewFactory.GetConvertedContent(filePath);
                }
                if (documents[i].UpdateBy == null)
                {
                    documents[i].UpdateBy = documents[i].CreateBy;
                    if (documents[i].UpdateByName == null)
                    {
                        documents[i].UpdateByName = documents[i].CreateByName;
                    }
                }
                //上传SI附件可以重命名文件名称后上传
                if (string.IsNullOrEmpty(documents[i].Name) || string.IsNullOrEmpty(Path.GetExtension(documents[i].Name)))
                    documents[i].Name = System.IO.Path.GetFileName(filePath);

                documents[i].FileSources = FileSource.FDocument;
                documents[i].State = UploadState.LocalSaving;
            }

            if (documentlist != null && documentlist.Count > 0)
            {
                List<Guid> deleteids = new List<Guid>();
                List<DateTime?> deleteUpdates = new List<DateTime?>();

                documentlist.ForEach(i =>
                {
                    deleteids.Add(i.Id);
                    deleteUpdates.Add(i.UpdateDate);
                });

                entity.DeleteIds.AddRange(deleteids);
                entity.UpdateDates.AddRange(deleteUpdates);
            }
            //保存到本地缓存
            LocalOperation.Save(documents.ToList(), entity.DeleteIds);
            ClientHelper.SetApplicationContext();
            //obama modify
            //FileService.Save(documents.ToList(), entity.DeleteIds, entity.UpdateDates);

            //obama add
            try
            {
                //文件覆盖之前，先把服务器数据库里的同名文件删除
                Guid userID = documents[0].CreateBy;
                FileServiceWCF.DeleteFileBeforeSave(entity.DeleteIds, entity.UpdateDates, userID);
            }
            catch (Exception ex)
            {
                String errorMessage = isEnglish ? "FileServiceWCF.DeleteFileBeforeSave" : "覆盖前删除原有数据失败";
                throw new ICPException(errorMessage + ex.Message);
            }
            try
            {
                //保存文件
                InnerUpLoad(documents);
            }
            catch (Exception ex)
            {
                String errorMessage = isEnglish ? "SmartEmaill InnerUpLoad(documents)" : "邮件智能上传失败";
                throw new ICPException(errorMessage + ex.Message);
            }
            //obama add end
        }
        public void Save(List<DocumentInfo> newDocuments, List<string> deleteFileNames, List<Guid> operationIds)
        {

            SaveEntity entity = new SaveEntity { DeleteFileNames = deleteFileNames, OperationIds = operationIds, NewDocuments = newDocuments };

            List<DocumentInfo> documentlist = BusinessOperationHelper.IsExistFileNames(new List<DocumentInfo>(entity.NewDocuments), deleteFileNames);

            //DocumentInfo[] documents = entity.NewDocuments.ToArray();

            int count = newDocuments.Count;
            for (int i = 0; i < count; i++)
            {
                String filePath = newDocuments[i].OriginalPath;
                String fileName = System.IO.Path.GetFileName(filePath);
                newDocuments[i].Content = IOHelper.ReadFileContentFromDisk(filePath);
                newDocuments[i].HtmlContent = PreviewFactory.GetConvertedContent(filePath);
                newDocuments[i].Name = fileName;
                //将覆盖的文档重新赋值实体去更新
                if (documentlist != null && documentlist.Count > 0)
                {
                    DocumentInfo info = documentlist.Find(item => item.Name.Equals(fileName));
                    if (info != null)
                    {
                        newDocuments[i].Id = info.Id;
                        newDocuments[i].UpdateDate = info.UpdateDate;
                    }
                }
                newDocuments[i].FileSources = FileSource.FDocument;
                newDocuments[i].State = UploadState.LocalSaving;
            }

            //if (documentlist != null && documentlist.Count > 0)
            //{
            //    List<Guid> deleteids = new List<Guid>();
            //    List<DateTime?> deleteUpdates = new List<DateTime?>();

            //    documentlist.ForEach(i =>
            //    {
            //        deleteids.Add(i.Id);
            //        deleteUpdates.Add(i.UpdateDate);
            //    });           
            //    entity.DeleteIds.AddRange(deleteids);
            //    entity.UpdateDates.AddRange(deleteUpdates);
            //}

            LocalOperation.SaveDocument(newDocuments.ToArray());
            ClientHelper.SetApplicationContext();
            FileService.Save(newDocuments, entity.DeleteFileNames, entity.OperationIds);

        }



        public void DeleteDocument(List<Guid> ids)
        {
            LocalOperation.DeleteDocument(ids);
        }

        /// <summary>
        /// 文档保存到远程数据库成功后,更新本地文档Id,UpdateDate
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="results"></param>
        public void UpdateDocumentRelation(DocumentInfo[] documents, ManyResult results)
        {
            DocumentMemoryCache.Remove(documents);
            LocalOperation.UpdateDocumentRelation(documents, results);
        }

        public void ChangeDocumentUploadState(Guid[] ids, UploadState state)
        {
            LocalOperation.ChangeDocumentUploadState(ids, state);
        }

        /// <summary>
        ///验证当前业务下是否包含传入的文件名集合
        /// </summary>
        /// <param name="fileNames">文件名集合</param>
        /// <param name="operationId">业务号</param>
        /// <returns></returns>
        public List<DocumentInfo> IsExistFileNames(List<string> fileNames, Guid operationId)
        {
            return FileService.IsExistFileNames(fileNames, operationId);
        }


       

        [Serializable]
        private class SaveEntity
        {
            public List<DocumentInfo> NewDocuments { get; set; }
            public List<string> NewFilePaths { get; set; }
            public List<Guid> DeleteIds { get; set; }
            public List<DateTime?> UpdateDates { get; set; }
            public List<string> DeleteFileNames { get; set; }
            public List<Guid> OperationIds { get; set; }
            public SaveEntity() { }
            public SaveEntity(List<DocumentInfo> documents, List<string> filePaths, List<Guid> deleteIds, List<DateTime?> updateDates)
            {
                this.NewDocuments = documents;
                this.NewFilePaths = filePaths;
                this.DeleteIds = deleteIds;
                this.UpdateDates = updateDates;
            }
        }

        /// <summary>
        /// 得到上传文件类型列表
        /// </summary>
        /// <param name="operateType">业务类型</param>
        /// <returns></returns>
        public Dictionary<string, UploadColumnType> GetUploadColumnType(int operateType)
        {
            return FileService.GetUploadColumnType(operateType);
        }

        #endregion

        /// <summary>
        /// 获取指定业务号下的同属于所指定文档类型的所有文档
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="documentType">文档类型</param>
        /// <returns></returns>
        public List<ContentInfo> GetDocumentListByDocumentType(Guid operationId, DocumentType documentType)
        {
            return FileService.GetDocumentListByDocumentType(operationId, documentType);
        }
        public static bool OAFileUpStart = true;
        public Thread OAShowProgressTd = null;
        /// <summary>
        /// 上传OA文档
        /// </summary>
        /// <param name="documents"></param>
        public void UplaodOADocument(DocumentInfo[] documents)
        {
            int count = documents.Length;
            List<UpLoadInfo> UpLoadInfoListTemp = new List<UpLoadInfo>();
            //缓存路径
            FilePathTemp = string.Empty;
            FilePathTemp = AppDomain.CurrentDomain.BaseDirectory;
            if (!FilePathTemp.EndsWith("\\"))
            {
                FilePathTemp += "\\";
            }
            FilePathTemp += "filetemp\\";
            if (!Directory.Exists(FilePathTemp))
            {
                Directory.CreateDirectory(FilePathTemp);
            }
            for (int i = 0; i < documents.Length; i++)
            {
                if (DocumentMemoryCache.FindFileName(documents[i].OperationID, documents[i].Name) != null)
                {
                    String errorMessage = isEnglish ? string.Format("Documents:{0} is being uploaded, please do not upload repeatedly .",
                        documents[i].Name) : string.Format("文档:{0}正在上传中，请不要重复上传。", documents[i].Name);
                    throw new ICPException(errorMessage);
                }
                List<Guid> ids = new List<Guid>();
            }

            try
            {
                Stopwatch StopwatchStep;
                string LogState = string.Empty;
                DocumentMemoryCache.Add(documents);
                //FileService.SaveDocumentToOADoc(documents);


                //记录文件信息
                for (int i = 0; i < count; i++)
                {
                    //上传OA文件流到服务器
                    UpLoadInfo UploadInfoTemp = new UpLoadInfo();

                    UploadInfoTemp.documentinfo = documents[i];
                    UploadInfoTemp.UploadOver = false;

                    UploadInfoTemp.FileTempStorePath = documents[i].Name;

                    //字节转换为文件流
                    Stream UploadStreamContent = new MemoryStream(documents[i].Content);
                    UploadInfoTemp.MergeStream = new FileStream(FilePathTemp + documents[i].Name, FileMode.Create,
                                  FileAccess.Write, FileShare.ReadWrite);
                    byte[] bytes = new byte[1024 * 10];//10K
                    int readfilecount;
                    while (true)
                    {
                        readfilecount = UploadStreamContent.Read(bytes, 0, bytes.Length);
                        if (readfilecount > 0)
                        {
                            UploadInfoTemp.MergeStream.Write(bytes, 0, readfilecount);
                        }
                        else
                        {
                            UploadStreamContent.Dispose();
                            break;
                        }
                    }
                    UploadInfoTemp.FileSize = (int)UploadInfoTemp.MergeStream.Length;
                    UploadInfoTemp.MergeStream.Close();
                    UploadInfoTemp.MergeStream.Dispose();

                    //初始化传输对象
                    DocumentStream uploaddocumentstream = new DocumentStream();
                    //使用Id来作为文件名，使用Name来获取文件扩展名
                    uploaddocumentstream.Id = documents[i].Id;
                    uploaddocumentstream.Name = documents[i].Name;
                    uploaddocumentstream.IncludePDF = true;
                    uploaddocumentstream.FirstFileStreamSize = UploadInfoTemp.FirstFileStreamSize;
                    uploaddocumentstream.Content = new FileStream(FilePathTemp + documents[i].Name, FileMode.Open,
                            FileAccess.Read, FileShare.ReadWrite);
                    UploadInfoTemp.uploaddocumentstream = uploaddocumentstream;
                    UploadInfoTemp.MergeStream = uploaddocumentstream.Content;
                    //记录文件信息
                    UpLoadInfoListTemp.Add(UploadInfoTemp);
                }
                UpLoadInfoList = UpLoadInfoList.Concat(UpLoadInfoListTemp).ToList();
                //通知进度(使用线程)
                if (!IsCheck)
                {
                    ShowProgressTd = new Thread(new ThreadStart(_CkeckUploadFileProgress));
                    ShowProgressTd.Start();
                }
                //轮流上传文件        
                for (int i = 0; i < count; i++)
                {
                    //发送文件流至服务器临时目录
                    //开始记录时间日志
                    StopwatchStep = StopwatchHelper.StartStopwatch();
                    FileServiceWCF.UploadOperationFileByStream(UpLoadInfoListTemp[i].uploaddocumentstream);
                    FileServiceWCF.SaveDocumentToOADoc(documents[i]);
                    LogState = string.Format("{0}上传文件[{1}]成功", documents[i].CreateByName, documents[i].Name);
                    //记录日志
                    Guid _OtherPartLogID = Guid.NewGuid();
                    MethodBase methodother = MethodBase.GetCurrentMethod();
                    StopwatchHelper.CustomRecordStopwatch(StopwatchStep, _OtherPartLogID,
                        DateTime.Now, methodother.DeclaringType.FullName, "WcfUploadFile", LogState);
                    //单个文件上传完成
                    //Thread.Sleep(20000);
                    //不可以使用foreach遍历，foreach遍历时不支持修改安全
                    UpLoadInfoListTemp[i].UploadOver = true;
                }
                //移除缓存
                DocumentMemoryCache.Remove(documents);
            }
            catch (Exception ex)
            {
                if (ShowProgressTd != null) { ShowProgressTd.Abort(); }
                String errorMessageTemplate = isEnglish ? "OA File upload failed.because of :{0}" : "OA文件上传失败，由于:{0}";
                DocumentMemoryCache.Remove(documents);

                if (ShowProgressTd != null) { ShowProgressTd.Abort(); }
                string exceptionstr = "UplaodOADocument上传文件失败，失败原因为:" + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
            }
        }
        /// <summary>
        /// 上传OA文档
        /// </summary>
        /// <param name="documents"></param>
        public void UplaodOADocument(DocumentInfo documents)
        {
            if (DocumentMemoryCache.FindFileName(documents.OperationID, documents.Name) != null)
            {
                String errorMessage = isEnglish ? string.Format("Documents:{0} is being uploaded, please do not upload repeatedly .", documents.Name) : string.Format("文档:{0}正在上传中，请不要重复上传。", documents.Name);
                throw new ICPException(errorMessage);
            }
            List<Guid> ids = new List<Guid>();
            try
            {
                DocumentMemoryCache.Add(documents);
                FileService.SaveDocumentToOADoc(documents);
                //移除缓存
                DocumentMemoryCache.Remove(documents);
            }
            catch (Exception ex)
            {
                String errorMessageTemplate = isEnglish ? "OA File upload failed.because of :{0}" : "OA文件上传失败，由于:{0}";
                //DocumentMemoryCache.Remove(documents.Id);
                throw new ICPException(String.Format(errorMessageTemplate, ex.Message));
            }
        }
        /// <summary>
        /// 下载OA文档
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContentInfo DownloadOADocument(Guid id)
        {
            ContentInfo info = null;
            try
            {
                bool downloadFromServer = false;
                info = LocalOperation.GetDocumentContent(id);
                if (info == null)
                {
                    info = new ContentInfo();
                }
                if (info.Content == null)
                {
                    //info = FileService.GetOADocumentContent(id);

                    //缓存路径
                    FilePathTemp = AppDomain.CurrentDomain.BaseDirectory;
                    if (!FilePathTemp.EndsWith("\\"))
                    {
                        FilePathTemp += "\\";
                    }
                    FilePathTemp += "filetemp\\";
                    if (!Directory.Exists(FilePathTemp))
                    {
                        Directory.CreateDirectory(FilePathTemp);
                    }
                    //传输对象参数
                    DocumentStream infoData = new DocumentStream();
                    infoData.Id = id;
                    infoData.IsDownCopy = false;
                    byte[] Contentbyte = new byte[1];
                    infoData.Content = new MemoryStream(Contentbyte);//字节转换为流，WCF消息契约中Body不能为空
                    //返回文件对象信息
                    DocumentStream inforeturn = FileServiceWCF.GetOADocumentContent(infoData);
                    if (inforeturn == null || inforeturn.Content == null)
                    {
                        throw new NullReferenceException(String.Format(isEnglish ?
                            "Download the file failed, please contact IT Department." :
                            "下载文件失败，请联系IT部门。", id));
                    }
                    info.Id = id;
                    info.Name = inforeturn.Name;
                    info.OperationID = inforeturn.OperationID;
                    Stream DataCacheStream = new FileStream(FilePathTemp + inforeturn.Name, FileMode.Create, FileAccess.Write, FileShare.Write);
                    //定义文件缓冲区
                    const int bufferLen = 1024 * 10;//10KB
                    byte[] buffer = new byte[bufferLen];
                    int count = 0;
                    while ((count = inforeturn.Content.Read(buffer, 0, bufferLen)) > 0)
                    {
                        DataCacheStream.Write(buffer, 0, count);
                    }
                    DataCacheStream.Dispose();
                    //流转换为字节
                    Stream ContentStream = new FileStream(FilePathTemp + inforeturn.Name, FileMode.Open, FileAccess.Read, FileShare.Read);
                    byte[] ContentFromStream = ArrayHelper.ReadAllBytesFromStream(ContentStream);
                    ContentStream.Dispose();

                    if (File.Exists(FilePathTemp + inforeturn.Name))
                        File.Delete(FilePathTemp + inforeturn.Name);

                    info.Content = ContentFromStream;

                    downloadFromServer = true;
                }
                if (info == null || info.Content == null)
                {
                    throw new NullReferenceException(String.Format(isEnglish ? "Download File: {0} failed." : "下载文件: {0}失败。", id));
                }
                //如果客户端缓存不存在，则将下载的数据保存到本地
                if (downloadFromServer)
                {
                    //LocalOperation.SaveDocumentContent(info);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return info;
        }

        #region Customer Document
        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentLocalIds"></param>
        public void ReUploadCustomerDocument(List<Guid> documentLocalIds)
        {
            List<DocumentInfo> documents = new List<DocumentInfo>();
            foreach (Guid id in documentLocalIds)
            {
                DocumentInfo document = LocalOperation.GetDocumentDetailInfo(id);
                documents.Add(document);
            }
            InnerUpLoad4Customer(documents.ToArray());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContentInfo GetCustomerDocumentContent(Guid id)
        {
            ContentInfo info = new ContentInfo();
            try
            {
                bool downloadFromServer = false;
                //先查询本地数据库中有没有，没有则从服务器下载
                info = LocalOperation.GetDocumentContent(id);
                if (info == null)
                {
                    info = new ContentInfo();
                }
                if (info.Content == null)
                {
                    DocumentStream infoData = new DocumentStream();
                    infoData.Id = id;
                    infoData.IsDownCopy = false;
                    byte[] Contentbyte = new byte[1];
                    infoData.Content = new MemoryStream(Contentbyte);//字节转换为流，WCF消息契约中Body不能为空
                    //返回文件对象信息
                    DocumentStream inforeturn = FileServiceWCF.GetCustomerDocumentContent(infoData);
                    if (inforeturn == null || inforeturn.Content == null)
                    {
                        throw new NullReferenceException(String.Format(isEnglish ?
                            "Download the file failed, please contact IT Department." :
                            "下载文件失败，请联系IT部门。", id));
                    }
                    info.Id = id;
                    info.Name = inforeturn.Name;
                    info.OperationID = inforeturn.OperationID;
                    Stream DataCacheStream = new FileStream(inforeturn.Name, FileMode.Create, FileAccess.Write, FileShare.Write);
                    //定义文件缓冲区
                    const int bufferLen = 1024 * 10;//10KB
                    byte[] buffer = new byte[bufferLen];
                    int count = 0;
                    while ((count = inforeturn.Content.Read(buffer, 0, bufferLen)) > 0)
                    {
                        DataCacheStream.Write(buffer, 0, count);
                    }
                    DataCacheStream.Dispose();
                    //流转换为字节
                    Stream ContentStream = new FileStream(inforeturn.Name, FileMode.Open, FileAccess.Read, FileShare.Read);
                    byte[] ContentFromStream = ArrayHelper.ReadAllBytesFromStream(ContentStream);
                    ContentStream.Dispose();

                    if (File.Exists(inforeturn.Name))
                        File.Delete(inforeturn.Name);

                    info.Content = ContentFromStream;
                    //obama add end

                    downloadFromServer = true;
                }
                if (info == null || info.Content == null)
                {
                    throw new NullReferenceException(String.Format(isEnglish ? "Download File: {0} failed." : "下载文件: {0}失败。", id));
                }
                if (downloadFromServer)
                {
                    LocalOperation.SaveDocumentContent(info);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return info;
        }
        /// <summary>
        /// 上传客户文档
        /// </summary>
        /// <param name="documentInfos"></param>
        public void UplaodCustomerDocument(DocumentInfo[] documentInfos, String[] filePaths)
        {
            string exceptionstr = string.Empty;
            try
            {
                int count = filePaths.Length;
                decimal[] progressValue = new decimal[count];
                exceptionstr += "判断文件是否还在上传中";
                for (int i = 0; i < count; i++)
                {
                    if (DocumentMemoryCache.FindFileName(documentInfos[i].OperationID, documentInfos[i].Name) != null)
                    {
                        String errorMessage = isEnglish ? string.Format(
                            "Documents:{0} is being uploaded, please do not upload repeatedly .",
                            documentInfos[i].Name) : string.Format("文档:{0}正在上传中，请不要重复上传。", documentInfos[i].Name);
                        throw new ICPException(errorMessage);
                    }
                }

                for (int i = 0; i < count; i++)
                {
                    String filePath = filePaths[i];
                    String fileName = Path.GetFileName(filePath);
                    string fileExtension = filePath.GetExtension();
                    documentInfos[i].State = UploadState.LocalProcessing;
                    documentInfos[i].HtmlContent = documentInfos[i].Content = IOHelper.ReadFileContentFromDisk(filePath);

                    documentInfos[i].Name = fileName;
                    documentInfos[i].FileSources = FileSource.FDocument;
                    documentInfos[i].State = UploadState.LocalSaving;
                    documentInfos[i].UpdateBy = LocalData.UserInfo.LoginID;
                    documentInfos[i].OriginalPath = filePaths[i];
                    LocalOperation.SaveDocument(documentInfos[i]);
                    documentInfos[i].State = UploadState.LocalSaved;
                    DocumentMemoryCache.Add(documentInfos[i]);
                }

                InnerUpLoad4Customer(documentInfos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取客户文档
        /// </summary>
        /// <param name="context">业务操作上下文类</param>
        /// <returns></returns>
        public List<DocumentInfo> GetCustomerDocumentList(BusinessOperationContext context)
        {
            List<DocumentInfo> docList = new List<DocumentInfo>();
            docList.AddRange(FileServiceWCF.GetCustomerDocumentList(context));
            List<Guid> docIdlist = new List<Guid>();
            docList.ForEach(e => docIdlist.Add(e.Id));

            List<DocumentInfo> docCache = DocumentMemoryCache.FindOperationDocuments(context.OperationID);
            List<DocumentInfo> docListCache = new List<DocumentInfo>();
            if (docCache != null)
            {
                docListCache.AddRange(docCache.Where(item => !docIdlist.Contains(item.Id)));
                docList.AddRange(docListCache);
            }
            return docList;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="updateDates"></param>
        /// <param name="UserId"></param>
        public void DeleteCustomerDocumentList(List<Guid> ids, List<DateTime?> updateDates, Guid UserId)
        {
            FileServiceWCF.DeleteCustomerDocumentList(ids, updateDates, UserId);
        }

        private void InnerUpLoad4Customer(DocumentInfo[] documents)
        {
            int count = documents.Length;
            List<UpLoadInfo> UpLoadInfoListTemp = new List<UpLoadInfo>();
            List<Guid> ids = new List<Guid>();
            string strIDs = string.Empty;
            DocumentInfo[] info = documents;
            if (info == null) return;
            Array.ForEach(info, document =>
            {
                strIDs = "[" + document.Id + "]";
                ids.Add(document.Id);
            });
            ClientHelper.SetApplicationContext();
            #region 上传文件流

            #region  //合并上传文件流
            FilePathTemp = AppDomain.CurrentDomain.BaseDirectory;
            if (!FilePathTemp.EndsWith("\\"))
            {
                FilePathTemp += "\\";
            }
            FilePathTemp += "filetemp\\";
            if (!Directory.Exists(FilePathTemp))
            {
                Directory.CreateDirectory(FilePathTemp);
            }
            try
            {
                DocumentInfo SendFileDocumentInfo = null;
                for (int i = 0; i < count; i++)
                {
                    UpLoadInfo UploadInfoTemp = new UpLoadInfo();
                    UploadInfoTemp.documentinfo = info[i];
                    UploadInfoTemp.UploadOver = false;
                    SendFileDocumentInfo = info[i];
                    //运用程序目录下创建合并文件流（有则覆盖）
                    try
                    {
                        UploadInfoTemp.MergeStream = new FileStream(FilePathTemp + SendFileDocumentInfo.Name, FileMode.Create,
                           FileAccess.Write, FileShare.ReadWrite);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    //字节转换为文件流
                    UploadInfoTemp.FileTempStorePath = SendFileDocumentInfo.Name;
                    Stream UploadStreamContent = new MemoryStream(SendFileDocumentInfo.Content);
                    UploadInfoTemp.FirstFileStreamSize = (int)UploadStreamContent.Length;
                    Stream UploadStreamHtmlContent = new MemoryStream(SendFileDocumentInfo.HtmlContent);
                    //合并文件流
                    byte[] bytes = new byte[1024 * 10];//10K
                    int readfilecount;
                    while (true)
                    {
                        readfilecount = UploadStreamContent.Read(bytes, 0, bytes.Length);
                        if (readfilecount > 0)
                        {
                            UploadInfoTemp.MergeStream.Write(bytes, 0, readfilecount);
                        }
                        else
                        {
                            UploadStreamContent.Dispose();
                            break;
                        }
                    }
                    //移动目标文件指针位置到末尾
                    UploadInfoTemp.MergeStream.Seek(0, SeekOrigin.End);
                    while (true)
                    {
                        readfilecount = UploadStreamHtmlContent.Read(bytes, 0, bytes.Length);
                        if (readfilecount > 0)
                        {
                            UploadInfoTemp.MergeStream.Write(bytes, 0, readfilecount);
                        }
                        else
                        {
                            UploadStreamHtmlContent.Dispose();
                            break;
                        }
                    }
                    UploadInfoTemp.FileSize = (int)UploadInfoTemp.MergeStream.Length;
                    UploadInfoTemp.MergeStream.Close();
                    UploadInfoTemp.MergeStream.Dispose();
                    UploadInfoTemp.MergeStream = null;

                    //初始化传输对象
                    DocumentStream uploaddocumentstream = new DocumentStream();
                    //使用Id来作为文件名，使用Name来获取文件扩展名
                    uploaddocumentstream.Id = SendFileDocumentInfo.Id;
                    uploaddocumentstream.Name = SendFileDocumentInfo.Name;
                    uploaddocumentstream.IncludePDF = true;
                    uploaddocumentstream.FirstFileStreamSize = UploadInfoTemp.FirstFileStreamSize;
                    uploaddocumentstream.Content = new FileStream(FilePathTemp + SendFileDocumentInfo.Name, FileMode.Open,
                            FileAccess.Read, FileShare.ReadWrite);
                    UploadInfoTemp.uploaddocumentstream = uploaddocumentstream;
                    UploadInfoTemp.MergeStream = uploaddocumentstream.Content;
                    //记录文件信息
                    UpLoadInfoListTemp.Add(UploadInfoTemp);
                    TempFilePath.Add(UploadInfoTemp.FileTempStorePath);
                }
                UpLoadInfoList = UpLoadInfoList.Concat(UpLoadInfoListTemp).ToList();
                #endregion

                #region  //轮流上传文件
                for (int i = 0; i < count; i++)
                {
                    string LogState = string.Empty;
                    string CreateByName = UpLoadInfoListTemp[i].documentinfo.CreateByName;
                    string FileName = UpLoadInfoListTemp[i].documentinfo.Name;
                    try
                    {
                        //开始记录时间日志
                        FileServiceWCF.UploadOperationFileByStream(UpLoadInfoListTemp[i].uploaddocumentstream);
                        //记录日志
                        LogState = string.Format("{0}:  [{1}]:  {2}K  :服务器", CreateByName, FileName,
                            (UpLoadInfoListTemp[i].uploaddocumentstream.Content.Length / 1024F).ToString("0.00"));
                    }
                    catch (Exception ex)
                    {
                        LogHelper.SaveLog("UplaodCustomerDocument 上传文件失败，失败原因为:" + ex.Message);
                    }
                    //从临时目录读取文件流存入数据库
                    try
                    {
                        FileServiceWCF.SaveDocumentToCustomerDoc(UpLoadInfoListTemp[i].documentinfo);
                        UpLoadInfoListTemp[i].UploadOver = true;
                        //记录日志
                        LogState = string.Format("{0}:  [{1}]  :数据库", CreateByName, FileName);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.SaveLog(string.Format("UplaodCustomerDocument {0}上传文件[{1}]失败，失败原因为:{2}", CreateByName, FileName, ex.Message));
                    }
                    //单个文件上传完成
                    //Thread.Sleep(2000);
                    DocumentMemoryCache.Remove(UpLoadInfoListTemp[i].documentinfo);
                }
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                if (ShowProgressTd != null) { ShowProgressTd.Abort(); }
                LogHelper.SaveLog("UplaodCustomerDocument 上传文件失败，失败原因为:" + ex.Message);
            }
        }
        #endregion
    }
}

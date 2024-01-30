using ICP.DataCache.ServiceInterface;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace ICP.DataCache.BusinessOperation
{
    public class FileUploadEngine
    {
        /// <summary>
        /// 文件本地处理通知服务
        /// </summary>
        public DocumentNotifyClientService DocumentNotifyService
        {
            get
            {
                return ServiceClient.GetClientService<DocumentNotifyClientService>();
            }
        }
        /// <summary>
        /// 文件上传进度事件
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="progress"></param>
        private void TriggerUploadProgressChangeEvent(DocumentInfo[] documents, decimal[] progress)
        {
            if (DocumentNotifyService.DocumentUploadProgress != null)
            {
                DocumentNotifyService.DocumentUploadProgress(documents, progress);
            }
        }
        /// <summary>
        /// 文件处理状态事件
        /// </summary>
        /// <param name="documentIds"></param>
        /// <param name="state"></param>
        private void TriggerUploadStateChangeEvent(List<Guid> documentIds, UploadState state)
        {
            if (DocumentNotifyService.DocumentStateChanged_New != null)
            {
                DocumentNotifyService.DocumentStateChanged_New(documentIds, state);
            }
        }
        /// <summary>
        /// 文件上传成功事件
        /// </summary>
        /// <param name="documents"></param>
        private void TriggerUploadUploadSucessedEvent(DocumentInfo[] documents)
        {
            if (DocumentNotifyService.DocumentUploadSucessed_New != null)
            {
                DocumentNotifyService.DocumentUploadSucessed_New(documents);
            }
        }
        /// <summary>
        /// WCF文件服务
        /// </summary>
        public IFileSystemService FileServiceWCF
        {
            get
            {
                return ServiceClient.GetService<IFileSystemService>();
            }
        }
        /// <summary>
        /// 系统错误日志服务
        /// </summary>
        public ISystemErrorLogService SystemErrorLogService
        {
            get { return ServiceClient.GetService<ISystemErrorLogService>(); }
        }
        //合并文件流缓存文件路劲
        string FilePathTemp = string.Empty;
        //上传文件列表
        public static List<UpLoadInfo> UpLoadInfoList = new List<UpLoadInfo>();
        //检查线程已工作
        public bool IsCheck = false;
        //检查线程
        Thread ShowProgressTd = null;
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="IsIncludePDF"></param>
        public void UploadFileToDocumentList(DocumentInfo[] documents)
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
                    UpLoadInfoList = UpLoadInfoList.Concat(UpLoadInfoListTemp).ToList();
                }
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
                    Thread.Sleep(20000);
                    UpLoadInfoListTemp[i].UploadOver = true;
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
            string ExceptionString = string.Empty; Thread.Sleep(100);
            IsCheck = true;
            ExceptionString = string.Empty;
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
                //有没有全部上传完成，若还有文件还没完成则重新轮训
                decimal OverValue = progressList.Average();
                //是否上传文件流完成
                if (OverValue < 100)
                {
                    //通知显示窗体
                    TriggerUploadProgressChangeEvent(documentsList.ToArray(), progressList.ToArray());
                    //每隔250毫秒轮训一次
                    Thread.Sleep(250);
                    documentsList.Clear();
                    progressList.Clear();
                    goto Start;
                }
                //文件流上传完成，上传到数据库完成
                if (OverValue == 100)
                {
                    TriggerUploadProgressChangeEvent(documentsList.ToArray(), progressList.ToArray());
                    TriggerUploadUploadSucessedEvent(documentsList.ToArray());
                    UpLoadInfoList.Clear();
                    documentsList.Clear();
                    progressList.Clear();
                }
                foreach (UpLoadInfo obj in UpLoadInfoList)//处理现场事宜
                {
                    //释放文件流
                    obj.MergeStream.Close();
                    obj.MergeStream.Dispose();
                    //删除合并文件流的缓存
                    if (File.Exists(FilePathTemp + obj.FileTempStorePath))
                    {
                        File.Delete(FilePathTemp + obj.FileTempStorePath);
                    }
                }
                IsCheck = false;
                #endregion
            }
            catch (Exception ex)
            {
                foreach (UpLoadInfo obj in UpLoadInfoList)//处理现场事宜
                {
                    //释放文件流
                    obj.MergeStream.Close();
                    obj.MergeStream.Dispose();
                    //删除合并文件流的缓存
                    if (File.Exists(FilePathTemp + obj.FileTempStorePath))
                    {
                        File.Delete(FilePathTemp + obj.FileTempStorePath);
                    }
                }
                UpLoadInfoList.Clear();
                documentsList.Clear();
                progressList.Clear();
                //WriteLog("_CkeckUploadFileProgres\r\n" + ExceptionString + ex.Message);
                string LogState = string.Format("{0}上传文件失败在[{1}]，失败原因为:", "",
                 "_CkeckUploadFileProgress");
                string exceptionstr = LogState + ex.Message;
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName,
                    LocalData.SessionId, new byte[0], exceptionstr,
                    DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                IsCheck = false;
            }
        }

    }
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
}

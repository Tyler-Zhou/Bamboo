using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1;
using ICP.DataCache.LocalOperation1;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Wintellect.Threading.AsyncProgModel;
using System.IO;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using System.Threading;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.CommonLibrary.Helper;
using ICP.DataCache.FileSystem;
using GhostscriptSharp;
using System.ServiceModel;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
namespace ICP.DataCache.BusinessOperation1
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ClientFileService : IClientFileService
    {
        bool isEnglish = LocalData.IsEnglish;
        [ServiceDependency]
        public ILocalBusinessCacheDataOperation LocalOperation { get; set; }
        [ServiceDependency]
        public IFileService ServiceOperation { get; set; }
        [ServiceDependency]
        public IFileStoreOperation FileStoreOperation { get; set; }
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ConverterPreviewFactory PreviewFactory { get; set; }
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public DocumentNotifyClientService DocumentNotifyService { get; set; }
        public ContentInfo GetDocumentHtmlContent(Guid id)
        {
            bool downloadFromServer = false;
            ContentInfo info = LocalOperation.GetDocumentHtmlContent(id);
            if (info == null || info.Content == null)
            {
                info = ServiceOperation.GetDocumentHtmlContent(id);
                downloadFromServer = true;
            }
            if (info == null || info.Content == null)
            {
                throw new NullReferenceException(String.Format(isEnglish ? "Download copy: {0} failed." : "下载副本: {0}失败。", id));
            }
            //如果客户端缓存不存在，则将下载的数据保存到本地
            if (downloadFromServer)
            {
                //AsyncEnumerator async = new AsyncEnumerator();
                //async.BeginExecute(Save(async,info,ContentType.Html), async.EndExecute);
                LocalOperation.SaveHtmlDocument(info);
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
                remoteCopies = ServiceOperation.GetDocumentCopyContents(localMissedIds);
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
            if (DocumentNotifyService.DocumentStateChanged != null)
            {
                DocumentNotifyService.DocumentStateChanged(documentIds, state);
            }
        }
        public void Upload(DocumentInfo[] documents, String[] filePaths)
        {

            int count = filePaths.Length;
            for (int i = 0; i < count; i++)
            {
                String filePath = filePaths[i];

                String fileName = System.IO.Path.GetFileName(filePath);
                documents[i].State = UploadState.LocalProcessing;
                TriggerUploadStateChangeEvent(new List<Guid> { documents[i].Id }, UploadState.LocalProcessing);
                documents[i].Content = DataCacheUtility.ReadFileContentFromDisk(filePath);
                documents[i].HtmlContent = PreviewFactory.GetConvertedContent(filePath);
                documents[i].Name = fileName;
                documents[i].FileSources = FileSource.FDocument;
                documents[i].State = UploadState.LocalSaving;
                TriggerUploadStateChangeEvent(new List<Guid> { documents[i].Id }, UploadState.LocalSaving);
                // DocumentMemoryCache.Add(documents[i]);
                LocalOperation.SaveDocument(documents[i]);
                documents[i].State = UploadState.LocalSaved;
                TriggerUploadStateChangeEvent(new List<Guid> { documents[i].Id }, UploadState.LocalSaved);

            }
            InnerUpLoad(documents);


        }
        private void InnerUpLoad(DocumentInfo[] documents)
        {
            WaitCallback callback = data =>
            {

                try
                {
                    DocumentInfo[] info = data as DocumentInfo[];
                    List<Guid> ids = new List<Guid>();
                    Array.ForEach(info, document => ids.Add(document.Id));
                    TriggerUploadStateChangeEvent(ids, UploadState.Uploading);
                    ClientHelper.SetApplicationContext();
                    ServiceOperation.Upload(documents);
                }
                catch (Exception ex)
                {
                    String errorMessageTemplate = isEnglish ? "File upload failed.because of :{0}" : "文件上传失败，由于:{0}";
                    throw new ICPException(String.Format(errorMessageTemplate, ex.Message));
                }
            };
            ThreadPool.QueueUserWorkItem(callback, documents);
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
            bool downloadFromServer = false;
            ContentInfo info = LocalOperation.GetDocumentContent(id);
            if (info == null || info.Content == null)
            {
                info = ServiceOperation.GetDocumentContent(id);
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
            return info;
        }

        public void Delete(List<Guid> ids, List<DateTime?> updateDates)
        {
            AsyncEnumerator async = new AsyncEnumerator();
            async.BeginExecute(DeleteDocument(async, ids, updateDates), async.EndExecute);
        }

        public void Delete(List<string> fileNames, Guid operationID)
        {
            AsyncEnumerator async = new AsyncEnumerator();
            async.BeginExecute(DeleteDocument(async, fileNames, operationID), async.EndExecute);
        }

        private IEnumerator<Int32> DeleteDocument(AsyncEnumerator ae, List<string> fileNames, Guid operationID)
        {

            ServiceOperation.Delete(fileNames, operationID);
            yield return 1;
        }


        private IEnumerator<Int32> DeleteDocument(AsyncEnumerator ae, List<Guid> ids, List<DateTime?> updateDates)
        {
            LocalOperation.DeleteDocument(ids);
            //yield return 1;
            ServiceOperation.Delete(ids, updateDates);
            yield return 1;
        }

        public string GenerateThumbImages(List<Guid> documentIds, Guid faxId)
        {
            List<ContentInfo> contentCopies = LocalOperation.GetDocumentCopyContent(documentIds);
            string basePath = Path.Combine(DataCacheUtility.ThumbImageRootPath, faxId.ToString());
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


        public List<DocumentInfo> GetBusinessDocumentList(BusinessOperationContext context)
        {
            //return LocalOperation.GetBusinessDocumentList(context);
            return ServiceOperation.GetBusinessDocumentList(context);
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
                ServiceOperation.Dispatch(param);
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
            WaitCallback callback = (data) =>
            {
                try
                {
                    SaveEntity entity = data as SaveEntity;
                    DocumentInfo[] documents = entity.NewDocuments.ToArray();

                    int count = documents.Length;
                    for (int i = 0; i < count; i++)
                    {
                        String filePath = newfilePaths == null ? documents[i].OriginalPath : newfilePaths[i];
                        String fileName = System.IO.Path.GetFileName(filePath);
                        documents[i].Content = DataCacheUtility.ReadFileContentFromDisk(filePath);
                        documents[i].HtmlContent = PreviewFactory.GetConvertedContent(filePath);
                        documents[i].Name = fileName;
                        documents[i].FileSources = FileSource.FDocument;
                        documents[i].State = UploadState.LocalSaving;
                    }
                    LocalOperation.Save(documents.ToList(), entity.DeleteIds);
                    ClientHelper.SetApplicationContext();
                    ServiceOperation.Save(documents.ToList(), entity.DeleteIds, entity.UpdateDates);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            };
            SaveEntity saveEntity = new SaveEntity(newDocuments, newfilePaths, listDeleteIds, updateDates);
            ThreadPool.QueueUserWorkItem(callback, saveEntity);

        }
        public void Save(List<DocumentInfo> newDocuments, List<string> deleteFileNames, List<Guid> operationIds)
        {
            WaitCallback callback = (data) =>
            {
                try
                {
                    SaveEntity entity = data as SaveEntity;
                    DocumentInfo[] documents = entity.NewDocuments.ToArray();

                    int count = documents.Length;
                    for (int i = 0; i < count; i++)
                    {
                        String filePath =  documents[i].OriginalPath;
                        String fileName = System.IO.Path.GetFileName(filePath);
                        documents[i].Content = DataCacheUtility.ReadFileContentFromDisk(filePath);
                        documents[i].HtmlContent = PreviewFactory.GetConvertedContent(filePath);
                        documents[i].Name = fileName;
                        documents[i].FileSources = FileSource.FDocument;
                        documents[i].State = UploadState.LocalSaving;
                    }
                    LocalOperation.SaveDocument(documents);
                    ClientHelper.SetApplicationContext();
                    ServiceOperation.Save(documents.ToList(),entity.DeleteFileNames, entity.OperationIds);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            };
            SaveEntity saveEntity = new SaveEntity { DeleteFileNames=deleteFileNames,OperationIds=operationIds,NewDocuments=newDocuments };
            ThreadPool.QueueUserWorkItem(callback, saveEntity);
        }

      public  void DeleteDocument(List<Guid> ids) 
        {
            LocalOperation.DeleteDocument(ids);
        }

        /// <summary>
        /// 文档保存到远程数据库成功后,更新本地文档Id,UpdateDate
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="results"></param>
     public   void UpdateDocumentRelation(DocumentInfo[] documents, ManyResult results) 
        {
            LocalOperation.UpdateDocumentRelation(documents, results);
        }

     /// <summary>
     /// 得到上传文件类型列表
     /// </summary>
     /// <param name="operateType">业务类型</param>
     /// <returns></returns>
     public Dictionary<string, UploadColumnType> GetUploadColumnType(int operateType)
     {
           return ServiceOperation.GetUploadColumnType(operateType);
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
        #endregion



    }

}

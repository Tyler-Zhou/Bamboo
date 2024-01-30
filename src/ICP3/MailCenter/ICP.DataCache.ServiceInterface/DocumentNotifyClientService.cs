using System;
using System.Collections.Generic;
using System.Linq;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using ICP.FileSystem.ServiceInterface;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 文档通知服务
    /// </summary>
    public class DocumentNotifyClientService
    {
        /// <summary>
        /// 文件上传失败
        /// </summary>
        public Action<UploadFailedMessage> DocumentUploadFailed;
        /// <summary>
        /// 文件上传状态
        /// </summary>
        public Action<List<Guid>, UploadState> DocumentStateChanged;
        /// <summary>
        /// 文件上传状态
        /// </summary>
        public Action<List<Guid>, UploadState> DocumentStateChanged_New;
        /// <summary>
        /// 文件上传成功
        /// </summary>
        public Action<DocumentInfo[]> DocumentUploadSucessed;
        /// <summary>
        /// 文件上传成功
        /// </summary>
        public Action<DocumentInfo[]> DocumentUploadSucessed_New;

        /// <summary>
        /// 文件分发成功
        /// </summary>
        public Action<List<Guid>> DocumentDispatched;

        //public Action<DocumentInfo[]> DocumentDownloaded;
        /// <summary>
        /// 文件删除成功
        /// </summary>
        public Action<List<Guid>> DocumentDeleteSucessed;
        /// <summary>
        /// DocumentAccepted
        /// </summary>
        public Action<List<Guid>, string> DocumentAccepted;
        /// <summary>
        /// DocumentUnAccepted
        /// </summary>
        public Action<List<Guid>> DocumentUnAccepted;
        /// <summary>
        /// DocumentAssignTo
        /// </summary>
        public Action<List<Guid>, string> DocumentAssignTo;
        /// <summary>
        /// DocumentError
        /// </summary>
        public Action<List<Guid>, string> DocumentError;

        /// <summary>
        /// 文件列表上传进度
        /// </summary>
        public Action<DocumentInfo[], decimal[]> DocumentUploadProgress;

        /// <summary>
        /// Oa文件上传进度
        /// </summary>
        public Action<DocumentInfo[], decimal[]> DocumentUploadProgressOfOaFile;

        public IClientFileService ClientFileService
        {
            get
            {
                return ServiceClient.GetService<IClientFileService>();
            }
        }
        public void Notify(NotifyType type, object data, object manyResult)
        {
            if (data == null)
                return;
            switch (type)
            {
                case NotifyType.Failed:
                    Fail(data as Guid[], manyResult as string);
                    break;
                case NotifyType.Sucessed:
                    Success(data as DocumentInfo[], manyResult as ManyResult);
                    break;
                case NotifyType.Dispatched:
                    Dispatched(data as Guid[], manyResult as DateTime?[]);
                    break;
                    //case NotifyType.Download:
                    //    Download(data as DocumentInfo[], manyResult as ManyResult);
                    break;
                case NotifyType.Accepted:
                    Accepted(data as Guid[], manyResult as string);
                    break;
                case NotifyType.UnAccepted:
                    UnAccepted(data as Guid[]);
                    break;
                case NotifyType.AssignTo:
                    AssignTo(data as Guid[], manyResult as string);
                    break;
                case NotifyType.Error:
                    Error(data as Guid[], manyResult as string);
                    break;
                case NotifyType.Delete:
                    Delete(data as Guid[], manyResult as ManyResult);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void Error(Guid[] oceanAgentDispatchId, string errorMessage)
        {
            if (DocumentError != null)
            {
                DocumentError(oceanAgentDispatchId.ToList(), errorMessage);
            }
        }

        private void Delete(Guid[] documentIds, ManyResult results)
        {
            if (DocumentDeleteSucessed != null)
            {
                DocumentDeleteSucessed(documentIds.ToList());
            }

        }

        private void Delete(object documentName, ManyResult results) 
        {
            documentName.GetType();
        
        }


        private void AssignTo(Guid[] assignToId, string assignToName)
        {
            if (DocumentAssignTo != null)
            {
                DocumentAssignTo(assignToId.ToList(), assignToName);
            }
        }

        private void Accepted(Guid[] oceanAgentDispatchIDs, string acceptByName)
        {
            if (DocumentAccepted != null)
            {
                DocumentAccepted(oceanAgentDispatchIDs.ToList(), acceptByName);
            }
        }

        private void UnAccepted(Guid[] oceanAgentDispatchIDs)
        {
            if (DocumentUnAccepted != null)
            {
                DocumentUnAccepted(oceanAgentDispatchIDs.ToList());
            }
        }

        private void Dispatched(Guid[] documentIds, DateTime?[] updateDates)
        {
            //处理本地缓存
            //if (documentIds != null && documentIds.Count > 0)
            //{   //更改文档状态
            //    LocalOperation.SetDocumentState(documentIds.ToList(), updateDates);
            //}
            if (DocumentDispatched != null)
            {
                DocumentDispatched(documentIds.ToList());
            }
        }

        void Success(DocumentInfo[] documents, ManyResult results)
        {
            ClientFileService.UpdateDocumentRelation(documents, results);
            
            if (DocumentUploadSucessed != null)
            {
                DocumentUploadSucessed(documents);
            }
        }
        void Fail(Guid[] ids, string errorMessage)
        {
            List<Guid> documentIds = ids.ToList();
            ClientFileService.DeleteDocument(ids.ToList());
            //LocalOperation.ChangeDocumentUploadState(ids, UploadState.Failed);
            if (DocumentUploadFailed != null)
            {
                UploadFailedMessage message = new UploadFailedMessage();
                message.DocumentIds = documentIds;
                message.ErrorMessage = errorMessage;
                DocumentUploadFailed(message);
            }
        }
    }
    public class UploadFailedMessage
    {
        public List<Guid> DocumentIds { get; set; }
        public string ErrorMessage { get; set; }
    }
}

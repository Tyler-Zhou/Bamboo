using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;
//using ICP.DataCache.BusinessOperation;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.DataCache.ServiceInterface1
{
    public class DocumentNotifyClientService
    {
        public Action<UploadFailedMessage> DocumentUploadFailed;
        public Action<List<Guid>, UploadState> DocumentStateChanged;
        public Action<DocumentInfo[]> DocumentUploadSucessed;
        public Action<List<Guid>> DocumentDispatched;
        //public Action<DocumentInfo[]> DocumentDownloaded;
        public Action<List<Guid>, string> DocumentAccepted;
        public Action<List<Guid>> DocumentUnAccepted;
        public Action<List<Guid>, string> DocumentAssignTo;
        public Action<List<Guid>, string> DocumentError;


        public IClientFileService ClientFileService
        {
            get
            {
                return ServiceClient.GetClientService<IClientFileService>();
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
          //  DocumentMemoryCache.Add(documents);
            ClientFileService.UpdateDocumentRelation(documents, results);
           // DocumentMemoryCache.Remove(documents);
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

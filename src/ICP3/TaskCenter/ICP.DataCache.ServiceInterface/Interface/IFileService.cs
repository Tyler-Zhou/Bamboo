using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.ServiceInterface;
using ICP.DataCache.ServiceInterface1;
using Microsoft.Practices.CompositeUI.Utility;

namespace ICP.DataCache.ServiceInterface1
{
    [ServiceContract]
    public interface IFileService
    {
        [OperationContract]
        ContentInfo GetDocumentHtmlContent(Guid id);
        [OperationContract]
        ContentInfo GetDocumentContent(Guid id);
        [OperationContract(Name = "DocumentDelete", IsOneWay = true)]
        void Delete(List<Guid> ids, List<DateTime?> updateDates);
        [OperationContract(Name = "DeleteDocument", IsOneWay = true)]
        void Delete(List<string> fileNames, Guid operationID);
        [OperationContract]
        List<DocumentInfo> GetBusinessDocumentList(BusinessOperationContext context);
        [OperationContract]
        List<DocumentInfo> GetDocumentDispatchHistoryList(Guid OperationID);
        [OperationContract]
        ListDictionary<Guid, string> GetBusinessDocumentsName(BusinessOperationContext context);
        [OperationContract(Name = "Upload", IsOneWay = true)]
        void Upload(DocumentInfo[] documents);
        [OperationContract]
        List<ContentInfo> GetDocumentCopyContents(List<Guid> ids);
        [OperationContract(Name = "DocumentDispatch", IsOneWay = true)]
        void Dispatch(DispatchStateParam param);
        [OperationContract]
        List<ContentInfo> GetDocumentContents(List<Guid> ids);

        [OperationContract]
        bool Accepted(AgentDispatchParam param);
        [OperationContract]
        bool AssignTo(AgentDispatchParam param);
        [OperationContract]
        AgentDispatchInfo GetAgentDispatchInfo(Guid oceanAgentDispatchID);
        [OperationContract(IsOneWay = true)]
        void Save(List<DocumentInfo> newDocuments, List<Guid> listDeleteIds, List<DateTime?> updateDates);
        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="newDocuments">新上传的文档</param>
        /// <param name="deleteFileNames">需要删除的文档名称列表</param>
        /// <param name="operationId">需要删除的文档所属的业务Id</param>
        [OperationContract(IsOneWay = true,Name="SaveAndDeleteByFileName")]
        void Save(List<DocumentInfo> newDocuments, List<string> deleteFileNames,List<Guid> operationIds);

        /// <summary>
        /// 得到上传文件类型列表
        /// </summary>
        /// <param name="operateType">业务类型</param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string, UploadColumnType> GetUploadColumnType(int operateType);
    }
}

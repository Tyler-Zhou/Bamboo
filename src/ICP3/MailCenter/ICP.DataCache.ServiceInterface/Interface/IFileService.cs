using System;
using System.Collections.Generic;
using System.ServiceModel;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI.Utility;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 文件服务
    /// </summary>
    [ServiceContract]
    public interface IFileService
    {
        /// <summary>
        /// 获取文档的Html副本
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>文档内容实体</returns>
        [OperationContract]
        ContentInfo GetDocumentHtmlContent(Guid id);

        /// <summary>
        /// 获取文档的内容
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>文档内容实体</returns>
        [OperationContract]
        ContentInfo GetDocumentContent(Guid id);

        /// <summary>
        /// 删除文档(根据文档ID)
        /// </summary>
        /// <param name="ids">文档ID列表</param>
        /// <param name="updateDates">更新时间列表</param>
        [OperationContract(Name = "DocumentDelete", IsOneWay = true)]
        void Delete(List<Guid> ids, List<DateTime?> updateDates);

        /// <summary>
        /// 删除文档(根据业务ID及其文件名)
        /// </summary>
        /// <param name="fileNames">文件名列表</param>
        /// <param name="operationID">业务号列表</param>
        [OperationContract(Name = "DeleteDocument", IsOneWay = true)]
        void Delete(List<string> fileNames, Guid operationID);

        /// <summary>
        /// 获取某票业务下的文档
        /// </summary>
        /// <param name="context">业务查询参数实体</param>
        /// <returns>文档列表</returns>
        [OperationContract]
        List<DocumentInfo> GetBusinessDocumentList(BusinessOperationContext context);

        /// <summary>
        /// 获取某票业务下的文档
        /// </summary>
        /// <param name="context">业务查询参数实体</param>
        /// <param name="dataSearchType">数据查询类型</param>
        /// <returns>文档列表</returns>
        [OperationContract(Name = "GetBusinessDocumentListByDataSearchType")]
        List<DocumentInfo> GetBusinessDocumentList(BusinessOperationContext context, DataSearchType dataSearchType);

        /// <summary>
        /// 获取多票业务下的文档
        /// </summary>
        /// <param name="contextlist">多票业务查询参数实体</param>
        /// <returns>文档列表</returns>
        [OperationContract(Name = "GetDocumentList")]
        List<DocumentInfo> GetBusinessDocumentList(List<BusinessOperationContext> contextlist);

        /// <summary>
        /// 获取文档分发历史列表
        /// </summary>
        /// <param name="OperationID">业务ID</param>
        /// <returns>文档列表</returns>
        [OperationContract]
        List<DocumentInfo> GetDocumentDispatchHistoryList(Guid OperationID);

        /// <summary>
        /// 获取分发文档
        /// </summary>
        /// <param name="FileLogsID">分发记录ID</param>
        /// <returns>文档列表</returns>
        [OperationContract]
        List<DocumentInfo> GetDispatchFiles(Guid FileLogsID);

        /// <summary>
        /// 获取业务文档名列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns>文档名列表</returns>
        [OperationContract]
        ListDictionary<Guid, string> GetBusinessDocumentsName(BusinessOperationContext context);

        /// <summary>
        /// 上传文档列表
        /// </summary>
        /// <param name="documents">文档列表</param>
        [OperationContract(Name = "Upload", IsOneWay = true)]
        void Upload(DocumentInfo[] documents);

        /// <summary>
        /// 获取文档内容列表(PDF)
        /// </summary>
        /// <param name="ids">文档ID</param>
        /// <returns>文档内容列表</returns>
        [OperationContract]
        List<ContentInfo> GetDocumentCopyContents(List<Guid> ids);

        /// <summary>
        /// 分发文档：更改分发记录状态
        /// </summary>
        /// <param name="param"></param>
        [OperationContract(Name = "DocumentDispatch", IsOneWay = true)]
        void Dispatch(DispatchStateParam param);

        /// <summary>
        /// 获取文档内容列表(PDF)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [OperationContract]
        List<ContentInfo> GetDocumentContents(List<Guid> ids);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [OperationContract]
        bool Accepted(AgentDispatchParam param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [OperationContract]
        bool AssignTo(AgentDispatchParam param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oceanAgentDispatchID"></param>
        /// <returns></returns>
        [OperationContract]
        AgentDispatchInfo GetAgentDispatchInfo(Guid oceanAgentDispatchID);

        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="newDocuments">文档列表</param>
        /// <param name="listDeleteIds">需移除文档ID列表</param>
        /// <param name="updateDates">更新时间列表</param>
        [OperationContract]
        void Save(List<DocumentInfo> newDocuments, List<Guid> listDeleteIds, List<DateTime?> updateDates);
        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="newDocuments">新上传的文档</param>
        /// <param name="deleteFileNames">需要删除的文档名称列表</param>
        /// <param name="operationIds">需要删除的文档所属的业务Id</param>
        [OperationContract(Name="SaveAndDeleteByFileName")]
        void Save(List<DocumentInfo> newDocuments, List<string> deleteFileNames,List<Guid> operationIds);

        /// <summary>
        /// 改变文档绑定
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="FormIds"></param>
        /// <param name="saveby"></param>
        /// <returns></returns>
        [OperationContract]
        bool ChangeBind(Guid[] Ids, Guid?[] FormIds, Guid saveby);

        /// <summary>
        /// 保存邮件到MessageDoc
        /// </summary>
        /// <param name="documents"></param>
        [OperationContract]
        void SaveMailItemToMessageDoc(DocumentInfo documents);

        /// <summary>
        /// 保存文档到OADoc
        /// </summary>
        /// <param name="documents"></param>
        [OperationContract]
        void SaveDocumentToOADoc(DocumentInfo documents);

        /// <summary>
        /// 获取OA文档
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ContentInfo GetOADocumentContent(Guid id);

        /// <summary>
        ///验证当前业务下是否包含传入的文件名列表
        /// </summary>
        /// <param name="fileNames">文件名列表</param>
        /// <param name="operationId">业务号</param>
        /// <returns></returns>
        [OperationContract(Name = "ExistFileNames")]
        List<DocumentInfo> IsExistFileNames(List<string> fileNames, Guid operationId);


        //joe 2013-06-07 添加
        /// <summary>
        /// 得到上传文件类型列表
        /// </summary>
        /// <param name="operateType">业务类型</param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string, UploadColumnType> GetUploadColumnType(int operateType);
        /// <summary>
        /// 获取指定业务号下的同属于所指定文档类型的所有文档
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="documentType">文档类型</param>
        /// <returns></returns>
        [OperationContract]
        List<ContentInfo> GetDocumentListByDocumentType(Guid operationId, DocumentType documentType);
    }
}

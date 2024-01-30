using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.FileSystem.ServiceInterface;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 客户端文件服务
    /// </summary>
    [ServiceContract]
    [ICPServiceHost]
    public interface IClientFileService
    {
        /// <summary>
        /// 客户端上传文件
        /// </summary>
        /// <param name="document"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [OperationContract]
        void Upload(DocumentInfo document, String filePath);

        /// <summary>
        /// 客户端批量上传文件
        /// </summary>
        /// <param name="document"></param>
        /// <param name="filePaths"></param>
        [OperationContract(Name = "BatchUpload")]
        void Upload(DocumentInfo[] document, String[] filePaths);

        /// <summary>
        /// 重新上传
        /// </summary>
        /// <param name="documentLocalIds"></param>
        [OperationContract]
        void Reupload(List<Guid> documentLocalIds);
        
        /// <summary>
        /// 保存HTML格式文档到本地
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        String SaveHtmlContentToDisk(Guid id);

        /// <summary>
        /// 获取业务下的文档
        /// </summary>
        /// <param name="context">业务操作上下文类</param>
        /// <returns></returns>
        [OperationContract]
        List<DocumentInfo> GetBusinessDocumentList(BusinessOperationContext context);

        /// <summary>
        /// 获取业务下的文档
        /// </summary>
        /// <param name="context">业务操作上下文类</param>
        /// <param name="dataSearchType">数据查询类型</param>
        /// <returns></returns>
        [OperationContract(Name = "GetBusinessDocumentListByDataSearchType")]
        List<DocumentInfo> GetBusinessDocumentList(BusinessOperationContext context,DataSearchType dataSearchType);

        /// <summary>
        /// 获取多票业务下的文档
        /// </summary>
        /// <param name="contextlist"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDocumentList")]
        List<DocumentInfo> GetBusinessDocumentList(List<BusinessOperationContext> contextlist);

        /// <summary>
        /// 获取文档HTML格式内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ContentInfo GetDocumentHtmlContent(Guid id);

        /// <summary>
        /// 获取文档HTML格式内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataSearchType">数据查询类型</param>
        /// <returns></returns>
        [OperationContract(Name = "GetDocumentHtmlContentByDataSearchType")]
        ContentInfo GetDocumentHtmlContent(Guid id, DataSearchType dataSearchType);

        /// <summary>
        /// 批量获取文档HTML格式内容
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [OperationContract]
        List<ContentInfo> GetDocumentCopyContents(List<Guid> ids);

        /// <summary>
        /// 获取文档内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ContentInfo GetDocumentContent(Guid id);

        /// <summary>
        /// 获取文档内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataSearchType">数据查询类型</param>
        /// <returns></returns>
        [OperationContract(Name = "GetDocumentContentByDataSearchType")]
        ContentInfo GetDocumentContent(Guid id,DataSearchType dataSearchType);

        /// <summary>
        /// 批量删除文档
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="updateDates"></param>
        [OperationContract]
        void Delete(List<Guid> ids, List<DateTime?> updateDates);

        /// <summary>
        /// 根据文件名和业务ID批量删除
        /// </summary>
        /// <param name="fileNames"></param>
        /// <param name="operationIDs"></param>
        [OperationContract(Name = "DeleteByFileNames")]
        void Delete(List<string> fileNames, Guid operationIDs);
        /// <summary>
        /// 产生缩略图
        /// </summary>
        /// <param name="documentIds"></param>
        /// <param name="faxId"></param>
        /// <returns></returns>
        [OperationContract]
        string GenerateThumbImages(List<Guid> documentIds, Guid faxId);

        /// <summary>
        /// 更改分发文档状态
        /// </summary>
        /// <param name="param"></param>
        [OperationContract]
        void SetDispatchState(DispatchStateParam param);

        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="newDocuments">新上传的文档</param>
        /// <param name="filePaths">新上传文档的路径</param>
        /// <param name="listDeleteIds">需删除文档的Id列表</param>
        /// <param name="updateDates">需删除文档的更新时间列表</param>
        /// <returns></returns>
        [OperationContract]
        void Save(List<DocumentInfo> newDocuments, List<string> filePaths, List<Guid> listDeleteIds, List<DateTime?> updateDates);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newDocuments"></param>
        /// <param name="deleteFileNames"></param>
        /// <param name="operationIds"></param>
        [OperationContract(Name = "SaveAndDeleteByFileNames")]
        void Save(List<DocumentInfo> newDocuments, List<string> deleteFileNames, List<Guid> operationIds);
        /// <summary>
        /// 删除多个文档
        /// </summary> 
        /// <param name="ids"></param>
        [OperationContract]
        void DeleteDocument(List<Guid> ids);

        /// <summary>
        /// 文档保存到远程数据库成功后,更新本地文档Id,UpdateDate
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="results"></param>
        [OperationContract]
        void UpdateDocumentRelation(DocumentInfo[] documents, ManyResult results);

        /// <summary>
        ///验证当前业务下是否包含传入的文件名集合
        /// </summary>
        /// <param name="fileNames">文件名集合</param>
        /// <param name="operationId">业务号</param>
        /// <returns></returns>
        [OperationContract(Name = "IsExist")]
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
        /// 更改本地缓存文档状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="state"></param>
        [OperationContract]
        void ChangeDocumentUploadState(Guid[] ids, UploadState state);

        /// <summary>
        /// 签收文件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [OperationContract]
        bool Accepted(AgentDispatchParam param);

        /// <summary>
        /// 指派给
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [OperationContract]
        bool AssignTo(AgentDispatchParam param);
        /// <summary>
        /// 获取指定业务号下的同属于所指定文档类型的所有文档
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="documentType">文档类型</param>
        /// <returns></returns>
        [OperationContract]
        List<ContentInfo> GetDocumentListByDocumentType(Guid operationId, DocumentType documentType);

        #region OA Document
        /// <summary>
        /// 上传OA文档
        /// </summary>
        /// <param name="documents"></param>
        [OperationContract]
        void UplaodOADocument(DocumentInfo documents);        /// <summary>
        /// 上传OA文档
        /// </summary>
        /// <param name="documents"></param>
        [OperationContract]
        void UplaodOADocument(DocumentInfo[] documents);
        /// <summary>
        /// 下载OA文档
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ContentInfo DownloadOADocument(Guid id);
        #endregion

        #region Customer Document
        /// <summary>
        /// 重新上传
        /// </summary>
        /// <param name="documentLocalIds"></param>
        [OperationContract]
        void ReUploadCustomerDocument(List<Guid> documentLocalIds);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        ContentInfo GetCustomerDocumentContent(Guid id);
        /// <summary>
        /// 上传客户文档
        /// </summary>
        /// <param name="documentInfos"></param>
        /// <param name="filePaths"></param>
        [OperationContract]
        void UplaodCustomerDocument(DocumentInfo[] documentInfos, string[] filePaths);
        /// <summary>
        /// 获取客户文档
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCustomerDocumentList")]
        List<DocumentInfo> GetCustomerDocumentList(BusinessOperationContext context);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="updateDates"></param>
        /// <param name="UserId"></param>
        [OperationContract(Name = "DeleteCustomerDocumentList")]
        void DeleteCustomerDocumentList(List<Guid> ids, List<DateTime?> updateDates, Guid UserId);
        #endregion
    }
}

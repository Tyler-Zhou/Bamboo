using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.DataCache.ServiceInterface1
{
    [ServiceContract]
    [ICPServiceHost]
    public interface IClientFileService
    {
        /// <summary>
        /// 客户端上传文件
        /// </summary>
        /// <param name="businessContext"></param>
        /// <param name="id"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [OperationContract]
        void Upload(DocumentInfo document, String filePath);
        [OperationContract(Name="BatchUpload")]
        void Upload(DocumentInfo[] document, String[] filePaths);
        [OperationContract]
        void Reupload(List<Guid> documentLocalIds);
        [OperationContract]
        String SaveHtmlContentToDisk(Guid id);
        [OperationContract]
        List<DocumentInfo> GetBusinessDocumentList(BusinessOperationContext context);
        [OperationContract]
        ContentInfo GetDocumentHtmlContent(Guid id);
        [OperationContract]
        List<ContentInfo> GetDocumentCopyContents(List<Guid> ids);
        [OperationContract]
        ContentInfo GetDocumentContent(Guid id);
        [OperationContract]
        void Delete(List<Guid> ids, List<DateTime?> updateDates);
        /// <summary>
        /// 根据文件名和业务ID批量删除
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="operationID"></param>
        [OperationContract(Name="DeleteByFileNames")]
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
        /// <param name="documentIds"></param>
        /// <param name="updateDates"></param>
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
        void Save(List<DocumentInfo> newDocuments,List<string> filePaths, List<Guid> listDeleteIds, List<DateTime?> updateDates);
        [OperationContract(Name="SaveAndDeleteByFileNames")]
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
        /// 得到上传文件类型列表
        /// </summary>
        /// <param name="operateType">业务类型</param>
        /// <returns></returns>
        [OperationContract]
        Dictionary<string,UploadColumnType> GetUploadColumnType(int operateType);
    }
}

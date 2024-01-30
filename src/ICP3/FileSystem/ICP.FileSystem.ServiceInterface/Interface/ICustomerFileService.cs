using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;
using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FileSystem.ServiceInterface
{
    /// <summary>
    /// 客户文件服务
    /// </summary>
    [ServiceContract]
    [StreamTransportService]
    public interface ICustomerFileService
    {
        /// <summary>
        /// 客户端从服务端下载文件
        /// </summary>
        /// <param name="DownloadFileInfo"></param>
        /// <returns></returns>
        [OperationContract(Action = "GetCustomerDocumentContent")]
        DocumentStream GetCustomerDocumentContent(DocumentStream DownloadFileInfo);//文件传输
        /// <summary>
        /// 保存文档到Customer Doc
        /// </summary>
        /// <param name="document"></param>
        [OperationContract(Name = "SaveDocumentToCustomerDoc")]
        void SaveDocumentToCustomerDoc(DocumentInfo document);

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
    }
}

using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;
using System;
using System.Collections.Generic;

namespace ICP.FileSystem.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    [StreamTransportService]
    public interface IOperationFileService
    {
        /// <summary>
        /// 客户端从服务端下载文件
        /// </summary>
        /// <param name="DownloadFileInfo"></param>
        /// <returns></returns>
        [OperationContract(Action = "DownloadFile")]
        DocumentStream ServiceTransferFileToClint(DocumentStream DownloadFileInfo);//文件传输

        /// <summary>
        /// 客户端发送上传文件到FCMDoc(通过拆分文件流)
        /// </summary>
        /// <param name="documentInfo">文档实体</param>
        [OperationContract(Action = "UploadOperationFileBySplit")]
        void UploadOperationFileByInfo(DocumentInfo documentInfo);

        /// <summary>
        ///验证当前业务下是否包含传入的文件名集合
        /// </summary>
        /// <param name="fileNames">文件名集合</param>
        /// <param name="operationId">业务号</param>
        /// <returns></returns>D:\SourcesCode\Visual Studio 2013\Projects\ICP3\FileSystem\ICP.FileSystem.ServiceInterface\Interface\IOperationFileService.cs
        [OperationContract(Name = "IsExist")]
        List<DocumentInfo> IsExistFileNames(List<string> fileNames, Guid operationId);

        /// <summary>
        /// 保存覆盖文件时提前删除数据库数据
        /// </summary>
        /// <param name="listDeleteIds"></param>
        /// <param name="updateDates"></param>
        /// <param name="UserId"></param>
        [OperationContract(Action = "SaveDelete")]
        void DeleteFileBeforeSave(List<Guid> listDeleteIds, List<DateTime?> updateDates, Guid UserId);
    }
}

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
    public interface IFileSystemService : IOperationFileService, IMailItemFileService, IOAFileService, ICustomerFileService
    {
        /// <summary>
        /// 客户端发送上传文件到临时目录（通过合并的文件流）
        /// </summary>
        /// <param name="documentInfo">文档实体</param>
        [OperationContract(Action = "UploadOperationFile")]
        void UploadOperationFileByStream(DocumentStream documentInfo);

       

        
    }
}

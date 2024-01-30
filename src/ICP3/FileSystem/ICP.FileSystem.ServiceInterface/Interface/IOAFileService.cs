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
    public interface IOAFileService
    {
        /// <summary>
        /// 保存文档到OADoc
        /// </summary>
        /// <param name="document"></param>
        [OperationContract(Name = "SaveDocumentToOA")]
        void SaveDocumentToOADoc(DocumentInfo document);
        /// <summary>
        /// 获取OA文档
        /// </summary>
        /// <param name="infostream"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetOADocument")]
        DocumentStream GetOADocumentContent(DocumentStream infostream);
    }
}

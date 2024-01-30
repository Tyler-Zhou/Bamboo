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
    public interface IMailItemFileService
    {
        /// <summary>
        /// 保存邮件到MessageDoc
        /// </summary>
        /// <param name="document"></param>
        [OperationContract(Name = "SaveMail")]
        void SaveMailItemToMessageDoc(DocumentInfo document);
    }
}

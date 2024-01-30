using System;
using System.Collections.Generic;
using System.ServiceModel;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    [ServiceKnownType(typeof(DocumentInfo[]))]
    [ServiceKnownType(typeof(ManyResult))]
    [ServiceKnownType(typeof(SingleResult))]
    [ServiceKnownType(typeof(List<SingleResult>))]
    [ServiceKnownType(typeof(Guid[]))]
    public interface IDocumentNotifyService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="generateIds"></param>
        [OperationContract(IsOneWay = true)]
        void Upload(NotifyType type, object data, object generateIds);
    }
}

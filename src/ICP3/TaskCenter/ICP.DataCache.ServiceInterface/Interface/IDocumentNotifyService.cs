using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;
namespace ICP.DataCache.ServiceInterface1
{  
    [ServiceContract]
    [ServiceKnownType(typeof(DocumentInfo[]))]
    [ServiceKnownType(typeof(ManyResult))]
    [ServiceKnownType(typeof(SingleResult))]
    [ServiceKnownType(typeof(List<SingleResult>))]
    [ServiceKnownType(typeof(Guid[]))]
   public interface IDocumentNotifyService
    {
       [OperationContract(IsOneWay = true)]
       void Upload(NotifyType type, object data, object generateIds);
    }
}

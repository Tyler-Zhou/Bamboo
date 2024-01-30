using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.DataCache.ServiceInterface1
{  
   [ServiceContract]
   public interface IFileQueueService
    {  
       [OperationContract(IsOneWay=true)]
       void Upload(DocumentInfo[] documents,ApplicationContext context);
    }
}

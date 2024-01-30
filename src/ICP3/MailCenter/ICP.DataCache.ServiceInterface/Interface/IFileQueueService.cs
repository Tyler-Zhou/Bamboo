using System.ServiceModel;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.DataCache.ServiceInterface
{  
    /// <summary>
    /// 
    /// </summary>
   [ServiceContract]
   public interface IFileQueueService
    {  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="context"></param>
       [OperationContract(IsOneWay=true)]
       void Upload(DocumentInfo[] documents,ApplicationContext context);
    }
}

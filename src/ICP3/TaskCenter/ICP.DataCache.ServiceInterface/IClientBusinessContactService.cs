using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.DataCache.ServiceInterface1
{ 
    /// <summary>
    /// 业务联系信息服务客户端接口
    /// </summary>
    [ServiceContract]
    [ICPServiceHost]
   public interface IClientBusinessContactService:IBusinessContactService
    { 
        [OperationContract]
       EmailSourceType Get(string senderAddress);
        [OperationContract]
        void Save(string senderAddress, EmailSourceType sourceType);
    }
}

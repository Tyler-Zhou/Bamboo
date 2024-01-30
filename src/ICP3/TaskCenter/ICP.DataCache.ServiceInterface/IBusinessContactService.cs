using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ICP.Message.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.DataCache.ServiceInterface1
{   
    /// <summary>
    /// 业务联系信息服务接口
    /// </summary>
    [ServiceContract]
   public interface IBusinessContactService
    {   
        /// <summary>
        /// 判断联系人类型
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        [OperationContract]
        EmailSourceType GetContactPersonType(string senderAddress);
    }
}

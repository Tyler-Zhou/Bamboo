using System;
using System.Collections.Generic;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Message.ServiceInterface
{   
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public interface IMailBeeService
    {
        [OperationContract]
        SingleResult Send(Message message);

        [OperationContract]
        string SaveAstMsgFile(string messageId);

        [OperationContract]
        void Transfer(List<ConfigureObjects> userCompanyList,Guid defaultCompanyID);
    }
}

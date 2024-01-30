


namespace ICP.FCM.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using Framework.CommonLibrary.Common;

    /// <summary>
    /// 代理文档分发策略接口
    /// </summary>
    [ServiceContract]
   public  interface IAgentDocumentDispatchService
    {
        /// <summary>
        /// 向代理分发文件
        /// </summary>
        [OperationContract]
        void Send(Object agent,BusinessOperationContext businessoperationcontext,List<Guid> documentIds,bool isAgain);

    }
}

using System;
using System.Collections.Generic;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// 内部代理分发文件实现类
    /// </summary>
    public class InternalAgentDocumentDispatchService : IInternalAgentDocumentDispatchService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="businessoperationcontext"></param>
        /// <param name="documentIds"></param>
        /// <param name="isAgain"></param>
        public void Send(object agent, BusinessOperationContext businessoperationcontext, List<Guid> documentIds, bool isAgain)
        {
            Guid gAgent = (Guid)agent;
        }
    }
}

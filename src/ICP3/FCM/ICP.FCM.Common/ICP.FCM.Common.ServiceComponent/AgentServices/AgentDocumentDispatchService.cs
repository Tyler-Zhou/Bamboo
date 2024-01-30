using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using ServiceInterface;

    /// <summary>
    /// 文档分发服务实现类
    /// </summary>
    public class AgentDocumentDispatchService : IAgentDocumentDispatchService
    {
        private IInternalAgentDocumentDispatchService internalAgentDocumentDispatchService;
        private IExternalAgentDocumentDispatchService externalAgentDocumentDispatchService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="internalAgentDocumentDispatchService"></param>
        /// <param name="externalAgentDocumentDispatchService"></param>
        public AgentDocumentDispatchService(IInternalAgentDocumentDispatchService internalAgentDocumentDispatchService, IExternalAgentDocumentDispatchService externalAgentDocumentDispatchService)
        {
            this.internalAgentDocumentDispatchService = internalAgentDocumentDispatchService;
            this.externalAgentDocumentDispatchService = externalAgentDocumentDispatchService;
        }

        /// <summary>
        /// 向代理分发文件
        /// </summary>
        public void Send(Object agent, BusinessOperationContext context, List<Guid> documentIds, bool isAgain)
        {
            bool isInternal = true;
            //如果是向外部代理或者海外部客服分发文档，则agent参数为接收人的Email地址，否则为内部代理的组织Id
            string agentEmailAddress = Convert.ToString(agent);
            if (agentEmailAddress.Contains("@"))
            {
                isInternal = false;
            }
            if (isInternal)
            {
                internalAgentDocumentDispatchService.Send(agent, context, documentIds, isAgain);
            }
            else
            {
                externalAgentDocumentDispatchService.Send(agent, context, documentIds, isAgain);
            }
        }
    }
}

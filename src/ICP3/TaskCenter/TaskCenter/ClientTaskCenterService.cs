using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.TaskCenter.ServiceInterface;
using ICP.TaskCenter.UI.MainWork;
using Microsoft.Practices.CompositeUI;

namespace ICP.TaskCenter.UI
{
    /// <summary>
    /// 任务中心客户端公开服务
    /// </summary>
    public class ClientTaskCenterService : IClientTaskCenterService
    {
        /// <summary>
        /// 打开任务中心
        /// </summary>
        /// <param name="operationContext">业务操作上下文</param>
        public void OpenTaskCenter(BusinessOperationContext operationContext)
        {
            WorkItem rootWorkItem = ServiceClient.GetClientService<WorkItem>();
          
            ViewListSmartPart viewSmartPart = rootWorkItem.Items.Get<ViewListSmartPart>(TaskCenterCommandConstants.ViewListSmartPartName);
            if (viewSmartPart == null)
            {

                rootWorkItem.Commands[TaskCenterCommandConstants.TaskCenter].Execute();
                viewSmartPart = rootWorkItem.Items.Get<ViewListSmartPart>(TaskCenterCommandConstants.ViewListSmartPartName);
            }
            else
                rootWorkItem.Workspaces[ClientConstants.MainWorkspace].Activate(rootWorkItem.Items.Get<MainWorkSpace>("MainWorkSpacePart"));
            
            viewSmartPart.AddBusinessNode(operationContext);
            viewSmartPart = null;
        }
    }
}

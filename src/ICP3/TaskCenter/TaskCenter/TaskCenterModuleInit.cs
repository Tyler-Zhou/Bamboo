using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary;
using ICP.FRM.ServiceInterface;
using ICP.FRM.UI;
using ICP.TaskCenter.ServiceInterface;
using ICP.TaskCenter.UI.MainWork;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;

namespace ICP.TaskCenter.UI
{
    /// <summary>
    /// 任务中心模块初始化类
    /// </summary>
    public class TaskCenterModuleInit : ModuleInit
    {
        /// <summary>
        /// 根WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }

        /// <summary>
        /// 添加服务
        /// </summary>
        public override void AddServices()
        {
            RootWorkItem.Services.AddNew<ClientTaskCenterService, IClientTaskCenterService>();
            RootWorkItem.Services.AddNew<InquireRateClientService, IInquireRateClientService>();
        }
        /// <summary>
        /// 添加任务中心模块
        /// </summary>
        [CommandHandler(TaskCenterCommandConstants.TaskCenter)]
        public void Command_TaskCenter(object sender, EventArgs e)
        {

            TaskCenterWorkitem taskCenterWorkItem = null;
            int tokenID = 0;
            try
            {
              tokenID= LoadingServce.ShowLoadingForm();

              taskCenterWorkItem = RootWorkItem.WorkItems.Get<TaskCenterWorkitem>(TaskCenterCommandConstants.TaskCenterWorkitemName);
                if (taskCenterWorkItem != null)
                {
                    taskCenterWorkItem.Dispose();
                }
                    taskCenterWorkItem = RootWorkItem.WorkItems.AddNew<TaskCenterWorkitem>(TaskCenterCommandConstants.TaskCenterWorkitemName);
                    taskCenterWorkItem.Run();
                
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
                if (taskCenterWorkItem != null)
                {
                    taskCenterWorkItem.Dispose();
                    taskCenterWorkItem = null;
                } 
                 
            }
            finally
            {
                LoadingServce.CloseLoadingForm(tokenID);
            }
 
        }

        /// <summary>
        /// 快捷方式打开任务中心
        /// </summary>
        [CommandHandler(ClientCommandConstants.TASKCENTER_QUICKOPEN)]
        public void QuickOpenTaskCenter(object sender, EventArgs e)
        {
            try
            {
                ViewListSmartPart viewSmartPart = RootWorkItem.Items.Get<ViewListSmartPart>(TaskCenterCommandConstants.ViewListSmartPartName);
                if (viewSmartPart == null)
                {
                    RootWorkItem.Commands[TaskCenterCommandConstants.TaskCenter].Execute();
                    viewSmartPart = RootWorkItem.Items.Get<ViewListSmartPart>(TaskCenterCommandConstants.ViewListSmartPartName);
                }
                else
                    RootWorkItem.Workspaces[ClientConstants.MainWorkspace].Activate(RootWorkItem.Items.Get<MainWorkSpace>("MainWorkSpacePart"));
                BusinessOperationContext operationContext = RootWorkItem.State["BusinessOperationContext"] as BusinessOperationContext;
                if(operationContext!=null)
                {
                    viewSmartPart.AddBusinessNode(operationContext);
                }
                viewSmartPart = null;
            }
            catch(Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
            }
        }
    }
}

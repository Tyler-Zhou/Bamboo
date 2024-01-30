using System;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.WF.ServiceInterface;
using ICP.WF.Controls;
using ICP.WF.ServiceInterface.DataObject;

namespace ICP.WF.UI
{
    /// <summary>
    /// 工作流客户端模块初始化入口
    /// </summary>
    public class WorkListModuleInit : ModuleInit
    {



        #region 服务

        public static IServiceContainerManager serviceContainers;
        internal static WorkItem _rootWorkItem;
        WorkListWorkItem _workflowItem;
        IDataFinderFactory _datafinderFactory;

        public WorkListModuleInit([ServiceDependency]WorkItem rootWorkItem
            , [ServiceDependency]IDataFinderFactory dataFinderFactory
            , [ServiceDependency]IDataFindClientService dataFindClientService 
            , [ServiceDependency]IWorkflowService workflowService
            , [ServiceDependency]IWorkFlowExtendService workflowExtendService
            , [ServiceDependency]IWorkFlowConfigService workFlowConfigService)
        {
            _rootWorkItem = rootWorkItem;
            _workflowItem = _rootWorkItem.WorkItems.AddNew<WorkListWorkItem>();
            _datafinderFactory = dataFinderFactory;
            if (serviceContainers == null) serviceContainers = new ServiceContainerManager();

            _rootWorkItem.Services.AddNew<TaskWorkClientService, ITaskWorkClientService>();
            _rootWorkItem.Services.AddNew<WorkListFlowChatPart, IWorkListFlowChatService>();
            

            //加载服务到容器中..用于控件实现IContaienrService的,提供服务实现
            serviceContainers.Add(typeof(WorkItem), _rootWorkItem);
            serviceContainers.Add(typeof(IDataFinderFactory), dataFinderFactory);
            serviceContainers.Add(typeof(IDataFindClientService), dataFindClientService);
            serviceContainers.Add(typeof(IWorkflowService), workflowService);
            serviceContainers.Add(typeof(IWorkFlowExtendService), workflowExtendService);
            serviceContainers.Add(typeof(IWorkFlowConfigService), workFlowConfigService);

            _datafinderFactory.Register<ICP.WF.Controls.Form.Commission.CommissionOperactionFinder>(WWFConstants.CommissionFinder);

            _datafinderFactory.Register<ICP.WF.Controls.Form.CustomerExpense.CustomerExpenseFinder>(WWFConstants.CustomerExpenseTouchFinder);
        }
        public override void AddServices()
        {
            _rootWorkItem.Services.AddNew<WorkListWorkItem, IWorkflowClientService>();
            base.AddServices();
        }
        #endregion


        #region 菜单事件

        /// <summary>
        /// 打开发起流程窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.Command_OpenWorkList)]
        public void CreateWorkCommandHandler(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");
            WorkListWorkItem workItem = _rootWorkItem.WorkItems.AddNew<WorkListWorkItem>();
            IWorkspace mainWorkspace = _rootWorkItem.Workspaces[ClientConstants.MainWorkspace];

            string title = LocalData.IsEnglish ? "Task List" : "任务列表";

            workItem.Show(mainWorkspace, title);

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
            //ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
        }

        #endregion
    }
}
